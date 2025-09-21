using Basic;
using Google.Protobuf.WellKnownTypes;
using H_Pannel_lib;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOffice;
using MySql.Data.MySqlClient;
using MyUI;
using SQLUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Basic.Net;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inspectionController : Controller
    {
        private IHostingEnvironment _environment;
        public inspectionController(IHostingEnvironment env)
        {
            _environment = env;

        }
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        static private string API = "http://127.0.0.1:4433";


        /// <summary>
        /// 初始化驗收單資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                string msg = "";
                return msg;
            }
        }
        /// <summary>
        /// 取得可建立今日最新驗收單號
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Value]為建立驗收單號</returns>       
        [Route("new_IC_SN")]
        [HttpPost]
        public string GET_new_IC_SN([FromBody] returnData returnData)
        {
            GET_init(returnData);
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();


            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);

            List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();

            list_inspection_creat_buf = list_inspection_creat.GetRowsInDate((int)enum_驗收單號.建表時間, DateTime.Now);


            string 驗收單號 = "";
            int index = 0;
            while (true)
            {
                驗收單號 = $"{DateTime.Now.ToDateTinyString()}-{index}";
                index++;
                list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, 驗收單號);
                if (list_inspection_creat_buf.Count == 0) break;
            }
            returnData.Value = 驗收單號;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "new_IC_SN";
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以建表日區間搜尋驗收單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 起始日期,結束日期 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "2023/10/26,2023/10/27"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_get_by_CT_TIME_ST_END")]
        [HttpPost]
        public string creat_get_by_CT_TIME_ST_END([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                string[] date_ary = returnData.Value.Split(',');
                if (date_ary.Length != 2)
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                else
                {
                    if (!date_ary[0].Check_Date_String() || !date_ary[1].Check_Date_String())
                    {
                        returnData.Code = -5;
                        returnData.Result = "輸入日期格式錯誤!";
                        return returnData.JsonSerializationt();
                    }
                }
                DateTime date_st = date_ary[0].StringToDateTime();
                DateTime date_end = date_ary[1].StringToDateTime();
                sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
                list_inspection_creat = list_inspection_creat.GetRowsInDateEx((int)enum_驗收單號.建表時間, date_st, date_end);
                returnData = Function_Get_inspection_creat(sys_serverSettingClasses[0], returnData.TableName, list_inspection_creat, false);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "creat_get_by_CT_TIME_ST_END";
                returnData.Result = $"取得驗收資料成功!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 以建表日搜尋盤點單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 建表日期 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "2023/10/26"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為盤點單結構</returns>
        [Route("creat_get_by_CT_TIME")]
        [HttpPost]
        public string creat_get_by_CT_TIME([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
                if (creat.建表時間.Check_Date_String() == false)
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
                list_inspection_creat = list_inspection_creat.GetRowsInDate((int)enum_驗收單號.建表時間, creat.建表時間.StringToDateTime());
                if (returnData.Value == "1")
                {
                    returnData = Function_Get_inspection_creat(sys_serverSettingClasses[0], returnData.TableName, list_inspection_creat, false);
                }
                else
                {
                    returnData = Function_Get_inspection_creat(sys_serverSettingClasses[0], returnData.TableName, list_inspection_creat);
                }
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得驗收資料成功!";
                returnData.Method = "creat_get_by_CT_TIME";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 以驗收單號更新驗收單開始時間
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "20231026-0"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_update_startime_by_IC_SN")]
        [HttpPost]
        public string creat_update_startime_by_IC_SN([FromBody] returnData returnData)
        {
            try
            {

                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
                string json_out = creat_get_by_IC_SN(returnData);
                returnData = json_out.JsonDeserializet<returnData>();
                if (returnData.Code < 0)
                {
                    returnData.Method = "creat_update_startime_by_CT_TIME";
                    return returnData.JsonSerializationt();
                }
                List<inspectionClass.creat> creats = returnData.Data.ObjToListClass<inspectionClass.creat>();
                if (creats.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.TimeTaken = myTimer.ToString();
                    returnData.Result = $"取得驗收資料失敗!";
                    returnData.Method = "creat_update_startime_by_CT_TIME";

                    return returnData.JsonSerializationt(true);
                }
                creat = creats[0];
                if (creat.驗收開始時間 == DateTime.MaxValue.ToDateTimeString())
                {
                    creat.驗收開始時間 = DateTime.Now.ToDateTimeString();
                }

                creat.驗收狀態 = "驗收中";
                object[] value = creat.ClassToSQL<inspectionClass.creat, enum_驗收單號>();
                List<object[]> list_value = new List<object[]>();
                list_value.Add(value);
                sQLControl_inspection_creat.UpdateByDefulteExtra(null, list_value);
                returnData.Code = 200;
                returnData.Data = creat;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"更新驗收開始時間成功!";
                returnData.Method = "creat_update_startime_by_CT_TIME";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }


        }
        /// <summary>
        /// 以驗收單號搜尋驗收單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : ["驗收單號", "請購單號"]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_get_by_IC_SN")]
        [HttpPost]
        public string creat_get_by_IC_SN([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
                string IC_SN = "";
                if (creat != null)
                {
                    IC_SN = creat.驗收單號;
                }
                if(IC_SN.StringIsEmpty())
                {
                    IC_SN = returnData.Value;
                }
                if(IC_SN.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"驗收單號空白";
                    return returnData.JsonSerializationt();
                }
                sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);

                List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
                list_inspection_creat = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, IC_SN);
                if (list_inspection_creat.Count == 0)
                {
                    returnData.Code = -5;
                    returnData.Result = $"查無此單號資料[{returnData.Value}]!";
                    return returnData.JsonSerializationt();
                }
                MED_pageController mED_PageController = new MED_pageController();

                returnData = Function_Get_inspection_creat(sys_serverSettingClasses[0], returnData.TableName, list_inspection_creat);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得驗收資料成功!";
                returnData.Method = "creat_get_by_IC_SN";

                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        [Route("content_get_by_PON")]
        [HttpPost]
        public string content_get_by_PON([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                //SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                //inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>()
                //if (creat != null)
                //{
                //    PON = creat.請購單號;
                //}
                //if (PON.StringIsEmpty())
                //{
                //    PON = returnData.Value;
                //}
                //if (PON.StringIsEmpty())
                //{
                //    returnData.Code = -200;
                //    returnData.Result = $"請購單號空白";
                //    return returnData.JsonSerializationt();
                //}
                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"驗收單號\",\"請購單號\"]";
                    return returnData.JsonSerializationt(true);
                }
                string 驗收單號 = returnData.ValueAry[0];
                string 請購單號 = returnData.ValueAry[1];

                List<object[]> list_inspection_content = sQLControl_inspection_content.GetRowsByDefult(null, (int)enum_驗收單號.驗收單號, 驗收單號);
                List<inspectionClass.content> contents = list_inspection_content.SQLToClass<inspectionClass.content, enum_驗收內容>();
                if (contents.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無此單驗收單號[{驗收單號}]!";
                    return returnData.JsonSerializationt(true);
                }
                List<inspectionClass.content> contents_buff = contents.Where(temp => temp.請購單號 == 請購單號).ToList();
                if (contents_buff.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無此單請購單號[{請購單號}]!";
                    return returnData.JsonSerializationt(true);
                }
                //單號可能找到兩種以上GUID，用不同GUID去搜尋詳細資料
                //bool flag = false;
                //for (int i = 0; i < contents.Count; i++)
                //{
                //    string GUID = contents[i].GUID;
                //    List<object[]> list_inspection_sub_content = sQLControl_inspection_sub_content.GetRowsByDefult(null, (int)enum_驗收明細.Master_GUID, GUID);
                //    List<inspectionClass.sub_content> sub_Contents = list_inspection_sub_content.SQLToClass<inspectionClass.sub_content, enum_驗收明細>();
                //    if (sub_Contents.Count > 0)
                //    {
                //        flag = true;
                //        contents[i].Sub_content.Add(sub_Contents[0]);
                //        break;
                //    }
                //}
                //List<inspectionClass.content> contents_buff = new List<inspectionClass.content>();
                //if (flag)
                //{
                //    contents = contents.Where(temp => temp.Sub_content.Count > 0).ToList();
                //}
                //else
                //{
                //    contents[0].Sub_content = new List<inspectionClass.sub_content>();
                //}


                returnData.Data = contents_buff[0];
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得驗收資料成功!";
                returnData.Method = "content_get_by_PON";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);
            }

        }
        [HttpPost("content_get_by_addTime")]
        public async Task<string> content_get_by_addTime([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"開始時間\",\"結束時間\"]";
                    return returnData.JsonSerializationt(true);
                }
                (string Server, string DB, string UserName, string Password, uint Port) = await HIS_WebApi.Method.GetServerInfoAsync("Main", "網頁", "VM端");
                string tableName_inspection_content = "inspection_content";
                string tableName_inspection_sub_content = "inspection_sub_content";

                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, tableName_inspection_content, UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                                     
                string 開始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];
                string command = $"SELECT * FROM dbvm.{tableName_inspection_content} WHERE 新增時間 >= @startTime AND 新增時間 < @endTime";
                object param = new { startTime = 開始時間, endTime = 結束時間 };
                List<object[]> list_inspection_content = await sQLControl_inspection_content.WriteCommandAsync(command, param);
                List<inspectionClass.content> contents = list_inspection_content.SQLToClass<inspectionClass.content, enum_驗收內容>();
                if (contents.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無此時間區域{開始時間}-{結束時間}資料!";
                    return returnData.JsonSerializationt(true);
                }
                List<string> guids = contents.Select(x => x.GUID).ToList();
                string command_sub_content = $"SELECT * FROM dbvm.{tableName_inspection_sub_content} WHERE Master_GUID IN @guidList";
                object param_sub_content = new { guidList = guids };
                List<object[]> list_inspection_sub_content = await sQLControl_inspection_sub_content.WriteCommandAsync(command_sub_content, param_sub_content);
                List<inspectionClass.sub_content> sub_Contents = list_inspection_sub_content.SQLToClass<inspectionClass.sub_content, enum_驗收明細>();

                if (sub_Contents.Count > 0)
                {
                    Dictionary<string, List<inspectionClass.sub_content>> dic = sub_Contents.ToDictByMasterGUID();
                    foreach (var item in contents)
                    {
                        List<inspectionClass.sub_content> subs = dic.GetByMasterGUID(item.GUID);
                        item.Sub_content = subs;
                    }
                }

                returnData.Data = contents;
                returnData.Code = 200;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Result = $"此時間區域{開始時間}-{結束時間}資料，共{contents.Count}筆";
                returnData.Method = "content_get_by_addTime";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);
            }

        }
        [Route("sub_content_get_by_PON")]
        [HttpPost]
        public string sub_content_get_by_PON([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
                string PON = "";
                if (creat != null)
                {
                    PON = creat.請購單號;
                }
                if (PON.StringIsEmpty())
                {
                    PON = returnData.Value;
                }
                if (PON.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"請購單號空白";
                    return returnData.JsonSerializationt();
                }
                //if (returnData.ValueAry == null)
                //{
                //    returnData.Data = -200;
                //    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                //    return returnData.JsonSerializationt();
                //}
                //if (returnData.ValueAry.Count != 2)
                //{
                //    returnData.Code = -200;
                //    returnData.Result = $"returnData.ValueAry 內容應為[\"驗收單號\",\"請購單號\"]";
                //    return returnData.JsonSerializationt(true);
                //}
                //string 驗收單號 = returnData.ValueAry[0];
                string 請購單號 = PON;

                List<object[]> list_inspection_content = sQLControl_inspection_content.GetRowsByDefult(null, (int)enum_驗收單號.請購單號, 請購單號);
                List<inspectionClass.content> contents = list_inspection_content.SQLToClass<inspectionClass.content, enum_驗收內容>();
                if(contents.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無此單請購單號[{請購單號}]!";
                    return returnData.JsonSerializationt(true);
                }
                //List<inspectionClass.content> contents_buff = contents.Where(temp => temp.請購單號 == 請購單號).ToList();
                //if(contents_buff.Count == 0)
                //{
                //    returnData.Code = -200;
                //    returnData.Result = $"查無此單請購單號[{請購單號}]!";
                //    return returnData.JsonSerializationt(true);
                //}
                ///單號可能找到兩種以上GUID，用不同GUID去搜尋詳細資料
                bool flag = false;
                for (int i = 0; i < contents.Count; i++)
                {
                    string GUID = contents[i].GUID;
                    List<object[]> list_inspection_sub_content = sQLControl_inspection_sub_content.GetRowsByDefult(null, (int)enum_驗收明細.Master_GUID, GUID);
                    List<inspectionClass.sub_content> sub_Contents = list_inspection_sub_content.SQLToClass<inspectionClass.sub_content, enum_驗收明細>();
                    if (sub_Contents.Count > 0)
                    {
                        flag = true;
                        contents[i].Sub_content.Add(sub_Contents[0]);
                        break;
                    }
                }
                if (flag)
                {
                    contents = contents.Where(temp => temp.Sub_content.Count > 0).ToList();
                }
                else
                {
                    contents[0].Sub_content = new List<inspectionClass.sub_content>();
                }


                returnData.Data = contents[0];
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得驗收資料成功!";
                returnData.Method = "sub_content_get_by_PON";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);
            }

        }
        /// <summary>
        /// 創建驗收單(驗收單號自訂)
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單名稱 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///        "IC_NAME": "測試驗收",
        ///        "IC_SN": "Q202230101-測試驗收單",
        ///        "CT": "",
        ///        "NOTE": "",
        ///        "Contents": 
        ///         [
        ///            {
        ///                "CODE": "220302IHYA",
        ///                "SKDIACODE": "",
        ///                "CHT_NAME": "Hyaluronate Sodium",
        ///                "NAME": "(申報)Hyalgan膝爾康 關節腔注射劑",
        ///                "PAKAGE": "Syri",
        ///                "BARCODE1": "",
        ///                "BARCODE2": "[]",
        ///                "START_QTY": "0",
        ///                "END_QTY": "0",
        ///                "ADD_TIME": "2023/10/30 20:41:53",
        ///                "NOTE": "",
        ///                "Sub_content": []
        ///            },
        ///            {
        ///                "CODE": "220IHYA",
        ///                "SKDIACODE": "",
        ///                "CHT_NAME": "Hyaluronate Sodium",
        ///                "NAME": "(申報)Hyalgan膝爾康 關節腔注射劑",
        ///                "PAKAGE": "",
        ///                "BARCODE1": "",
        ///                "BARCODE2": "[]",
        ///                "START_QTY": "0",
        ///                "END_QTY": "0",
        ///                "ADD_TIME": "2023/10/30 20:41:53",
        ///                "NOTE": "",
        ///                "Sub_content": []
        ///             }
        ///         ]       
        ///     }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_add")]
        [HttpPost]
        public string creat_add([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();

            List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();


            if (creat == null)
            {
                returnData.Code = -5;
                returnData.Result += $"Data 資料錯誤 \n";
                return returnData.JsonSerializationt();
            }

            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, creat.驗收單號);
            if (list_inspection_creat_buf.Count > 0)
            {
                returnData.Code = -6;
                returnData.Result += $"驗收單號: {creat.驗收單號} 已存在,請刪除後再建立! \n";
                return returnData.JsonSerializationt();
            }


            if (creat.驗收單號.StringIsEmpty())
            {
                string IC_SN_json = GET_new_IC_SN(returnData);
                returnData returnData_IC_SN = IC_SN_json.JsonDeserializet<returnData>();
                creat.驗收單號 = returnData_IC_SN.Value;
            }

            creat.GUID = Guid.NewGuid().ToString();
            creat.建表時間 = DateTime.Now.ToDateTimeString();
            creat.驗收開始時間 = DateTime.MaxValue.ToDateTimeString();
            creat.驗收結束時間 = DateTime.MaxValue.ToDateTimeString();
            creat.驗收狀態 = "等待驗收";
            List<object[]> list_inspection_creat_add = new List<object[]>();
            List<object[]> list_inspection_content_add = new List<object[]>();
            List<object[]> list_inspection_sub_content_add = new List<object[]>();
            object[] value;
            value = new object[new enum_驗收單號().GetLength()];

            value = creat.ClassToSQL<inspectionClass.creat, enum_驗收單號>();
            list_inspection_creat_add.Add(value);



            for (int i = 0; i < creat.Contents.Count; i++)
            {
                creat.Contents[i].GUID = Guid.NewGuid().ToString();
                creat.Contents[i].新增時間 = DateTime.Now.ToDateTimeString();
                creat.Contents[i].Master_GUID = creat.GUID;
                creat.Contents[i].驗收單號 = creat.驗收單號;


                value = new object[new enum_驗收內容().GetLength()];
                value = creat.Contents[i].ClassToSQL<inspectionClass.content, enum_驗收內容>();
                list_inspection_content_add.Add(value);

                if(creat.Contents[i].Sub_content != null)
                {
                    for (int k = 0; k < creat.Contents[i].Sub_content.Count; k++)
                    {
                        creat.Contents[i].Sub_content[k].GUID = Guid.NewGuid().ToString();
                        creat.Contents[i].Sub_content[k].Master_GUID = creat.Contents[i].GUID;
                        creat.Contents[i].Sub_content[k].操作時間 = DateTime.Now.ToDateTimeString_6();
                        creat.Contents[i].Sub_content[k].藥品碼 = creat.Contents[i].藥品碼;
                        creat.Contents[i].Sub_content[k].藥品名稱 = creat.Contents[i].藥品名稱;
                        creat.Contents[i].Sub_content[k].中文名稱 = creat.Contents[i].中文名稱;
                        creat.Contents[i].Sub_content[k].驗收單號 = creat.Contents[i].驗收單號;
                        value = creat.Contents[i].Sub_content[k].ClassToSQL<inspectionClass.sub_content, enum_驗收明細>();

                        list_inspection_sub_content_add.Add(value);
                    }
                }
              
            }
            sQLControl_inspection_creat.AddRows(null, list_inspection_creat_add);
            sQLControl_inspection_content.AddRows(null, list_inspection_content_add);
            sQLControl_inspection_sub_content.AddRows(null, list_inspection_sub_content_add);
            returnData.Data = creat;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "creat_add";

            returnData.Result = $"成功加入新驗收單! 共{list_inspection_content_add.Count}筆資料";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 更新驗收單
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///        "IC_NAME": "測試驗收",
        ///        "IC_SN": "Q202230101-測試驗收單",
        ///        "CT": "",
        ///        "NOTE": "",
        ///        "Contents": 
        ///         [
        ///            {
        ///                "CODE": "220302IHYA",
        ///                "SKDIACODE": "",
        ///                "CHT_NAME": "Hyaluronate Sodium",
        ///                "NAME": "(申報)Hyalgan膝爾康 關節腔注射劑",
        ///                "PAKAGE": "Syri",
        ///                "BARCODE1": "",
        ///                "BARCODE2": "[]",
        ///                "START_QTY": "0",
        ///                "END_QTY": "0",
        ///                "ADD_TIME": "2023/10/30 20:41:53",
        ///                "NOTE": "",
        ///                "Sub_content": []
        ///            },
        ///            {
        ///                "CODE": "220IHYA",
        ///                "SKDIACODE": "",
        ///                "CHT_NAME": "Hyaluronate Sodium",
        ///                "NAME": "(申報)Hyalgan膝爾康 關節腔注射劑",
        ///                "PAKAGE": "",
        ///                "BARCODE1": "",
        ///                "BARCODE2": "[]",
        ///                "START_QTY": "0",
        ///                "END_QTY": "0",
        ///                "ADD_TIME": "2023/10/30 20:41:53",
        ///                "NOTE": "",
        ///                "Sub_content": []
        ///             }
        ///         ]       
        ///     }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_update")]
        [HttpPost]
        public string creat_update([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();



            sQLControl_inspection_creat.DeleteByDefult(null, (int)enum_驗收單號.驗收單號, creat.驗收單號);
            sQLControl_inspection_content.DeleteByDefult(null, (int)enum_驗收內容.驗收單號, creat.驗收單號);
            sQLControl_inspection_sub_content.DeleteByDefult(null, (int)enum_驗收明細.驗收單號, creat.驗收單號);
            if (creat == null)
            {
                returnData.Code = -200;
                returnData.Result += $"Data 資料錯誤 \n";
                return returnData.JsonSerializationt();
            }

    
            if (creat.驗收單號.StringIsEmpty())
            {
                string IC_SN_json = GET_new_IC_SN(returnData);
                returnData returnData_IC_SN = IC_SN_json.JsonDeserializet<returnData>();
                creat.驗收單號 = returnData_IC_SN.Value;
            }

            creat.GUID = Guid.NewGuid().ToString();
            creat.建表時間 = DateTime.Now.ToDateTimeString();
            creat.驗收開始時間 = DateTime.MaxValue.ToDateTimeString();
            creat.驗收結束時間 = DateTime.MaxValue.ToDateTimeString();
            creat.驗收狀態 = "等待驗收";
            List<object[]> list_inspection_creat_add = new List<object[]>();
            List<object[]> list_inspection_content_add = new List<object[]>();
            List<object[]> list_inspection_sub_content_add = new List<object[]>();
            object[] value;
            value = new object[new enum_驗收單號().GetLength()];

            value = creat.ClassToSQL<inspectionClass.creat, enum_驗收單號>();
            list_inspection_creat_add.Add(value);



            for (int i = 0; i < creat.Contents.Count; i++)
            {
                creat.Contents[i].GUID = Guid.NewGuid().ToString();
                creat.Contents[i].新增時間 = DateTime.Now.ToDateTimeString();
                creat.Contents[i].Master_GUID = creat.GUID;
                creat.Contents[i].驗收單號 = creat.驗收單號;


                value = new object[new enum_驗收內容().GetLength()];
                value = creat.Contents[i].ClassToSQL<inspectionClass.content, enum_驗收內容>();
                list_inspection_content_add.Add(value);

                if (creat.Contents[i].Sub_content != null)
                {
                    for (int k = 0; k < creat.Contents[i].Sub_content.Count; k++)
                    {
                        creat.Contents[i].Sub_content[k].GUID = Guid.NewGuid().ToString();
                        creat.Contents[i].Sub_content[k].Master_GUID = creat.Contents[i].GUID;
                        creat.Contents[i].Sub_content[k].操作時間 = DateTime.Now.ToDateTimeString_6();
                        creat.Contents[i].Sub_content[k].藥品碼 = creat.Contents[i].藥品碼;
                        creat.Contents[i].Sub_content[k].藥品名稱 = creat.Contents[i].藥品名稱;
                        creat.Contents[i].Sub_content[k].中文名稱 = creat.Contents[i].中文名稱;
                        creat.Contents[i].Sub_content[k].驗收單號 = creat.Contents[i].驗收單號;
                        value = creat.Contents[i].Sub_content[k].ClassToSQL<inspectionClass.sub_content, enum_驗收明細>();

                        list_inspection_sub_content_add.Add(value);
                    }
                }

            }
            sQLControl_inspection_creat.AddRows(null, list_inspection_creat_add);
            sQLControl_inspection_content.AddRows(null, list_inspection_content_add);
            sQLControl_inspection_sub_content.AddRows(null, list_inspection_sub_content_add);
            returnData.Data = creat;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "creat_update";

            returnData.Result = $"成功更新驗收單! 共{list_inspection_content_add.Count}筆資料";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 創建驗收單(自動填入驗收單號)
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單名稱 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///        "IC_NAME": "測試驗收",
        ///        "CT": "",
        ///        "NOTE": "",
        ///        "Contents": 
        ///         [
        ///            {
        ///                "CODE": "220302IHYA",
        ///                "SKDIACODE": "",
        ///                "CHT_NAME": "Hyaluronate Sodium",
        ///                "NAME": "(申報)Hyalgan膝爾康 關節腔注射劑",
        ///                "PAKAGE": "Syri",
        ///                "BARCODE1": "",
        ///                "BARCODE2": "[]",
        ///                "START_QTY": "0",
        ///                "END_QTY": "0",
        ///                "ADD_TIME": "2023/10/30 20:41:53",
        ///                "NOTE": "",
        ///                "Sub_content": []
        ///            },
        ///            {
        ///                "CODE": "220IHYA",
        ///                "SKDIACODE": "",
        ///                "CHT_NAME": "Hyaluronate Sodium",
        ///                "NAME": "(申報)Hyalgan膝爾康 關節腔注射劑",
        ///                "PAKAGE": "",
        ///                "BARCODE1": "",
        ///                "BARCODE2": "[]",
        ///                "START_QTY": "0",
        ///                "END_QTY": "0",
        ///                "ADD_TIME": "2023/10/30 20:41:53",
        ///                "NOTE": "",
        ///                "Sub_content": []
        ///             }
        ///         ]       
        ///     }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_auto_add")]
        [HttpPost]
        public string creat_auto_add([FromBody] returnData returnData)
        {
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> sys_serverSettingClasses_buf = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses_buf.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses_buf[0].Server;
                string DB = sys_serverSettingClasses_buf[0].DBName;
                string UserName = sys_serverSettingClasses_buf[0].User;
                string Password = sys_serverSettingClasses_buf[0].Password;
                uint Port = (uint)sys_serverSettingClasses_buf[0].Port.StringToInt32();

                returnData returnData_GET_new_IC_SN = this.GET_new_IC_SN(returnData).JsonDeserializet<returnData>();
                string str_IC_SN = returnData_GET_new_IC_SN.Value;

                MED_pageController mED_PageController = new MED_pageController();
                returnData returnData_med = new returnData();
                returnData_med.ServerName = returnData.ServerName;
                returnData_med.ServerType = returnData.ServerType;
                returnData_med.Server = Server;
                returnData_med.DbName = DB;
                returnData_med.TableName = "medicine_page_cloud";
                returnData_med.Port = Port;
                returnData_med.UserName = UserName;
                returnData_med.Password = Password;

                returnData_med = mED_PageController.get_by_apiserver(returnData_med).JsonDeserializet<returnData>();
                List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();

                inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
                creat.驗收單號 = str_IC_SN;
                for (int i = 0; i < medClasses.Count; i++)
                {
                    inspectionClass.content content = new inspectionClass.content();
                    content.藥品碼 = medClasses[i].藥品碼;
                    content.藥品名稱 = medClasses[i].藥品名稱;
                    content.中文名稱 = medClasses[i].中文名稱;
                    content.廠牌 = medClasses[i].廠牌;
                    content.料號 = medClasses[i].料號;
                    content.包裝單位 = medClasses[i].包裝單位;
                    content.應收數量 = "0";

                    creat.Contents.Add(content);
                }
                if (creat.Contents.Count == 0)
                {
                    returnData.Code = -6;
                    returnData.Value = "無驗收資料可新增!";
                    return returnData.JsonSerializationt();
                }
                returnData.Data = creat;
                returnData.Method = "creat_auto_add";

                return creat_add(returnData);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 以驗收單號鎖定驗收單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "20231026-0"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_lock_by_IC_SN")]
        [HttpPost]
        public string creat_lock([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
            if (creat == null)
            {
                returnData.Code = -5;
                returnData.Result += $"Data資料錯誤!";
                return returnData.JsonSerializationt();
            }
            List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, creat.驗收單號);
            if (list_inspection_creat_buf.Count == 0)
            {
                returnData.Code = -5;
                returnData.Result += $"找無此驗收單號!";
                return returnData.JsonSerializationt();
            }
            list_inspection_creat_buf[0][(int)enum_驗收單號.驗收狀態] = "鎖定";
            list_inspection_creat_buf[0][(int)enum_驗收單號.驗收結束時間] = DateTime.Now.ToDateTimeString();
            sQLControl_inspection_creat.UpdateByDefulteExtra(null, list_inspection_creat_buf);
            creat.GUID = list_inspection_creat_buf[0][(int)enum_驗收單號.GUID].ObjectToString();
            creat.驗收狀態 = list_inspection_creat_buf[0][(int)enum_驗收單號.驗收狀態].ObjectToString();
            creat.建表人 = list_inspection_creat_buf[0][(int)enum_驗收單號.建表人].ObjectToString();
            creat.建表時間 = list_inspection_creat_buf[0][(int)enum_驗收單號.建表時間].ToDateTimeString();
            creat.驗收開始時間 = list_inspection_creat_buf[0][(int)enum_驗收單號.驗收開始時間].ToDateTimeString();
            creat.驗收結束時間 = list_inspection_creat_buf[0][(int)enum_驗收單號.驗收結束時間].ObjectToString();
            returnData.Code = 200;
            returnData.Value = "驗收單鎖定成功!";
            returnData.Data = creat;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "creat_lock_by_IC_SN";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以驗收單號解鎖驗收單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "20231026-0"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_unlock_by_IC_SN")]
        [HttpPost]
        public string creat_unlock([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
            if (creat == null)
            {
                returnData.Code = -5;
                returnData.Result += $"Data資料錯誤!";
                return returnData.JsonSerializationt();
            }
            List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, creat.驗收單號);
            if (list_inspection_creat_buf.Count == 0)
            {
                returnData.Code = -5;
                returnData.Result += $"找無此驗收單號!";
                return returnData.JsonSerializationt();
            }
            list_inspection_creat_buf[0][(int)enum_驗收單號.驗收狀態] = "等待驗收";
            sQLControl_inspection_creat.UpdateByDefulteExtra(null, list_inspection_creat_buf);
            creat.GUID = list_inspection_creat_buf[0][(int)enum_驗收單號.GUID].ObjectToString();
            creat.驗收狀態 = list_inspection_creat_buf[0][(int)enum_驗收單號.驗收狀態].ObjectToString();
            creat.建表人 = list_inspection_creat_buf[0][(int)enum_驗收單號.建表人].ObjectToString();
            creat.建表時間 = list_inspection_creat_buf[0][(int)enum_驗收單號.建表時間].ToDateTimeString();
            creat.驗收開始時間 = list_inspection_creat_buf[0][(int)enum_驗收單號.驗收開始時間].ToDateTimeString();
            creat.驗收結束時間 = list_inspection_creat_buf[0][(int)enum_驗收單號.驗收結束時間].ToDateTimeString();
            returnData.Code = 200;
            returnData.Value = "驗收單解鎖成功!";
            returnData.Data = creat;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "creat_unlock_by_IC_SN";

            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以驗收單號刪除驗收單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "20231026-0"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>       
        [Route("creat_delete_by_IC_SN")]
        [HttpPost]
        public string creat_delete_by_IC_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();

            //if(returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = $"請購單號不得為空白!";
            //    return returnData.JsonSerializationt();
            //}

            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, creat.驗收單號);
            list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.驗收單號, creat.驗收單號);
            list_inspection_sub_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收明細.驗收單號, creat.驗收單號);

            sQLControl_inspection_creat.DeleteExtra(null, list_inspection_creat_buf);
            sQLControl_inspection_content.DeleteExtra(null, list_inspection_content_buf);
            sQLControl_inspection_sub_content.DeleteExtra(null, list_inspection_sub_content_buf);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已將[{ creat.驗收單號}]刪除!";
            returnData.Method = "creat_delete_by_IC_SN";

            return returnData.JsonSerializationt();


        }
        /// <summary>
        /// 以GUID刪除驗收單內驗收藥品
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    [                 
        ///        {
        ///            "GUID": "a6f58a75-094a-411c-8f87-9804d46b78ea"
        ///        },
        ///        {
        ///            "GUID": "024e17eb-fa68-4495-92fa-d78ed753c23b"
        ///        }
        ///    ]
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>無</returns>
        [Route("contents_delete_by_GUID")]
        [HttpPost]
        public string contents_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            if (returnData.Data == null)
            {
                returnData.Code = -5;
                returnData.Result = $"Data資料長度錯誤!";
                return returnData.JsonSerializationt();
            }
            List<inspectionClass.content> contents = returnData.Data.ObjToListClass<inspectionClass.content>();
            List<object> list_GUID = new List<object>();
            for (int i = 0; i < contents.Count; i++)
            {
                list_GUID.Add(contents[i].GUID);
            }
            sQLControl_inspection_content.DeleteExtra(null, enum_驗收內容.GUID.GetEnumName(), list_GUID);
            sQLControl_inspection_sub_content.DeleteExtra(null, enum_驗收明細.Master_GUID.GetEnumName(), list_GUID);
            returnData.Data = null;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已刪除驗收內容共<{list_GUID.Count}>筆資料!";
            returnData.Method = "contents_delete_by_GUID";

            return returnData.JsonSerializationt();
        }
        /// <summary>
        /// 以GUID取得驗收單內驗收藥品
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///        "GUID": "a6f58a75-094a-411c-8f87-9804d46b78ea"         
        ///    }
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data] :content資料結構 </returns>
        [Route("content_get_by_content_GUID")]
        [HttpPost]
        public string content_get_by_content_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);

            inspectionClass.content content = returnData.Data.ObjToClass<inspectionClass.content>(); ;
            //if (returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = "輸入資料不得空白!";
            //    return returnData.JsonSerializationt();
            //}

            string GUID = content.GUID;
            List<Task> tasks = new List<Task>();
            List<textVisionClass> textVisionClasses = new List<textVisionClass>();
            tasks.Add(Task.Run(new Action(delegate 
            {
                returnData returnData_textVision = textVisionClass.get_by_MasterGUID(API, GUID);
                textVisionClasses = returnData_textVision.Data.ObjToClass<List<textVisionClass>>();
            })));
            returnData.Data = null;
            List<object[]> list_inspection_content = sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.GUID, GUID);
            if (list_inspection_content_buf.Count == 0)
            {
                returnData.Code = -1;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"查無驗收內容!";
                returnData.Method = "content_get_by_content_GUID";

                return returnData.JsonSerializationt();
            }
            else
            {
                MED_pageController mED_PageController = new MED_pageController();
                returnData returnData_med = new returnData();
                returnData_med.TableName = "medicine_page_cloud";
                returnData_med.ServerType = "網頁";
                returnData_med.ServerName = "Main";
                returnData_med.Value = list_inspection_content_buf[0][(int)enum_驗收內容.藥品碼].ObjectToString();
                string json_med = mED_PageController.get_by_code(returnData_med);
                returnData_med = json_med.JsonDeserializet<returnData>();

                content = list_inspection_content_buf[0].SQLToClass<inspectionClass.content, enum_驗收內容>();
                int 實收數量 = 0;
                list_inspection_sub_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收明細.Master_GUID, content.GUID);
                for (int m = 0; m < list_inspection_sub_content_buf.Count; m++)
                {
                    inspectionClass.sub_content sub_Content = list_inspection_sub_content_buf[m].SQLToClass<inspectionClass.sub_content, enum_驗收明細>();

                    if (sub_Content.實收數量.StringIsInt32())
                    {

                        實收數量 += sub_Content.實收數量.StringToInt32();
                    }
                    content.Sub_content.Add(sub_Content);
                }
                if (returnData_med != null)
                {
                    if (returnData_med.Code == 200)
                    {
                        List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();
                        if (medClasses.Count > 0)
                        {
                            string 藥品名稱 = medClasses[0].藥品名稱;
                            string 中文名稱 = medClasses[0].中文名稱;
                            string 包裝單位 = medClasses[0].包裝單位;
                            content.藥品名稱 = 藥品名稱;
                            content.中文名稱 = 中文名稱;
                            content.包裝單位 = 包裝單位;
                            for (int i = 0; i < content.Sub_content.Count; i++)
                            {
                                content.Sub_content[i].藥品名稱 = 藥品名稱;
                                content.Sub_content[i].中文名稱 = 中文名稱;
                                content.Sub_content[i].包裝單位 = 包裝單位;
                                content.Sub_content[i].總量 = 實收數量.ToString();
                            }
                        }


                    }
                }
                Task.WhenAll(tasks).Wait();
                content.實收數量 = 實收數量.ToString();
                content.Sub_content.Sort(new ICP_sub_content());
                content.textVision = textVisionClasses;
                returnData.Data = content;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得驗收內容成功!";
                returnData.Method = "content_get_by_content_GUID";

                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 以GUID取得驗收單內驗收藥品中的明細
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///       "GUID": "4710df0c-5bfe-4c98-ac0e-46453ab8b043"
        ///    }
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data] :sub_content陣列資料結構 </returns>
        [Route("sub_content_get_by_content_GUID")]
        [HttpPost]
        public string sub_content_get_by_content_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            inspectionClass.content content = returnData.Data.ObjToClass<inspectionClass.content>(); ;
            //if (returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = "輸入資料不得空白!";
            //    return returnData.JsonSerializationt();
            //}

            string GUID = content.GUID;
            returnData.Data = null;
            List<object[]> list_inspection_sub_content = sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            list_inspection_sub_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收明細.Master_GUID, GUID);
            List<inspectionClass.sub_content> sub_Contents = new List<inspectionClass.sub_content>();
            for (int i = 0; i < list_inspection_sub_content_buf.Count; i++)
            {
                inspectionClass.sub_content sub_Content = list_inspection_sub_content_buf[i].SQLToClass<inspectionClass.sub_content, enum_驗收明細>();
                sub_Contents.Add(sub_Content);
            }
            returnData.Data = sub_Contents;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"取得驗收明細成功!";
            returnData.Method = "sub_content_get_by_content_GUID";

            return returnData.JsonSerializationt();
        }
        /// <summary>
        /// 新增單筆驗收藥品中的明細且刪除原本明細
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///       "Master_GUID": "13a6625b-b7b2-43b0-ba59-7c451a4912e0",
        ///       "OP": "測試者",
        ///       "END_QTY": "56"
        ///    }
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data] :sub_content資料結構 </returns>
        [Route("sub_content_add_single")]
        [HttpPost]
        public string sub_content_add_single([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            List<object[]> list_inspection_content = sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_add = new List<object[]>();
            inspectionClass.sub_content sub_content = returnData.Data.ObjToClass<inspectionClass.sub_content>();
            string Master_GUID = sub_content.Master_GUID;

            sQLControl_inspection_sub_content.DeleteByDefult(null, enum_驗收明細.Master_GUID.GetEnumName(), Master_GUID);

            list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.GUID, Master_GUID);
            if (list_inspection_content_buf.Count > 0)
            {
                object[] value = new object[new enum_驗收明細().GetLength()];
                value[(int)enum_驗收明細.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_驗收明細.藥品碼] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品碼];
                value[(int)enum_驗收明細.料號] = list_inspection_content_buf[0][(int)enum_驗收內容.料號];
                value[(int)enum_驗收明細.驗收單號] = list_inspection_content_buf[0][(int)enum_驗收內容.驗收單號];
                value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼1];
                value[(int)enum_驗收明細.藥品條碼2] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼2];

                value[(int)enum_驗收明細.Master_GUID] = Master_GUID;
                value[(int)enum_驗收明細.效期] = DateTime.MaxValue.ToDateString();
                value[(int)enum_驗收明細.批號] = "";
                value[(int)enum_驗收明細.實收數量] = sub_content.實收數量;
                value[(int)enum_驗收明細.操作人] = sub_content.操作人;
                value[(int)enum_驗收明細.操作時間] = DateTime.Now.ToDateTimeString();
                value[(int)enum_驗收明細.狀態] = "未鎖定";

                sub_content.藥品碼 = value[(int)enum_驗收明細.藥品碼].ObjectToString();
                sub_content.料號 = value[(int)enum_驗收明細.料號].ObjectToString();
                sub_content.驗收單號 = value[(int)enum_驗收明細.驗收單號].ObjectToString();
                sub_content.藥品條碼1 = value[(int)enum_驗收明細.藥品條碼1].ObjectToString();
                sub_content.藥品條碼2 = value[(int)enum_驗收明細.藥品條碼1].ObjectToString();
                sub_content.Master_GUID = value[(int)enum_驗收明細.Master_GUID].ObjectToString();
                sub_content.效期 = value[(int)enum_驗收明細.效期].ObjectToString();
                sub_content.操作人 = value[(int)enum_驗收明細.操作人].ObjectToString();
                sub_content.操作時間 = value[(int)enum_驗收明細.操作時間].ObjectToString();
                sub_content.狀態 = value[(int)enum_驗收明細.狀態].ObjectToString();

                list_add.Add(value);
            }
            sQLControl_inspection_sub_content.AddRows(null, list_add);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"新增批效成功!";
            returnData.Data = sub_content;
            returnData.Method = "sub_content_add_single";

            return returnData.JsonSerializationt();
        }
        /// <summary>
        /// 新增單筆驗收藥品中的明細
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///       "Master_GUID": "13a6625b-b7b2-43b0-ba59-7c451a4912e0",
        ///       "OP": "測試者",
        ///       "END_QTY": "56"
        ///    }
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data] :sub_content資料結構 </returns>
        //[Route("sub_content_add")]
        //[HttpPost]
        //public string sub_content_add([FromBody] returnData returnData)
        //{
        //    MyTimer myTimer = new MyTimer();
        //    myTimer.StartTickTime(50000);

        //    List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
        //    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
        //    if (sys_serverSettingClasses.Count == 0)
        //    {
        //        returnData.Code = -200;
        //        returnData.Result = $"找無Server資料!";
        //        return returnData.JsonSerializationt();
        //    }
        //    string Server = sys_serverSettingClasses[0].Server;
        //    string DB = sys_serverSettingClasses[0].DBName;
        //    string UserName = sys_serverSettingClasses[0].User;
        //    string Password = sys_serverSettingClasses[0].Password;
        //    uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

        //    SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
        //    SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
        //    SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
        //    List<object[]> list_inspection_content = sQLControl_inspection_content.GetAllRows(null);
        //    List<object[]> list_inspection_content_buf = new List<object[]>();
        //    List<object[]> list_add = new List<object[]>();
        //    inspectionClass.sub_content sub_content = returnData.Data.ObjToClass<inspectionClass.sub_content>();
        //    string Master_GUID = sub_content.Master_GUID;
        //    list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.GUID, Master_GUID);
        //    if (list_inspection_content_buf.Count == 0)
        //    {
        //        returnData.Code = -5;
        //        returnData.TimeTaken = myTimer.ToString();
        //        returnData.Result = $"找無資料!";
        //        returnData.Method = "sub_content_add";
        //        returnData.Data = null;
        //        return returnData.JsonSerializationt();
        //    }

        //    object[] value = new object[new enum_驗收明細().GetLength()];
        //    value[(int)enum_驗收明細.GUID] = Guid.NewGuid().ToString();
        //    value[(int)enum_驗收明細.藥品碼] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品碼];
        //    value[(int)enum_驗收明細.料號] = list_inspection_content_buf[0][(int)enum_驗收內容.料號];
        //    value[(int)enum_驗收明細.驗收單號] = list_inspection_content_buf[0][(int)enum_驗收內容.驗收單號];
        //    value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼1];
        //    value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼2];

        //    value[(int)enum_驗收明細.Master_GUID] = Master_GUID;
        //    value[(int)enum_驗收明細.效期] = sub_content.效期;
        //    value[(int)enum_驗收明細.批號] = sub_content.批號;
        //    value[(int)enum_驗收明細.實收數量] = sub_content.實收數量;
        //    value[(int)enum_驗收明細.操作人] = sub_content.操作人;
        //    value[(int)enum_驗收明細.操作時間] = DateTime.Now.ToDateTimeString();
        //    value[(int)enum_驗收明細.狀態] = "未鎖定";

        //    list_add.Add(value);

        //    sQLControl_inspection_sub_content.AddRows(null, list_add);

        //    inspectionClass.content content = new inspectionClass.content();
        //    content.GUID = Master_GUID;
        //    returnData.Data = content;
        //    string json_content = content_get_by_content_GUID(returnData);
        //    returnData = json_content.JsonDeserializet<returnData>();
        //    if (returnData == null)
        //    {
        //        returnData.Code = -5;
        //        returnData.TimeTaken = myTimer.ToString();
        //        returnData.Result = $"搜尋content資料錯誤!";
        //        returnData.Method = "sub_content_add";
        //        returnData.Data = null;
        //        return returnData.JsonSerializationt();
        //    }
        //    if (returnData.Code < 0)
        //    {
        //        returnData.TimeTaken = myTimer.ToString();
        //        returnData.Method = "sub_content_add";
        //        returnData.Data = null;
        //        return returnData.JsonSerializationt();
        //    }

        //    returnData.Code = 200;
        //    returnData.TimeTaken = myTimer.ToString();
        //    returnData.Result = $"新增批效成功!";
        //    returnData.Method = "sub_content_add";
        //    return returnData.JsonSerializationt();
        //}
        /// <summary>
        /// 新增單筆驗收藥品中的明細
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///       "Master_GUID": "13a6625b-b7b2-43b0-ba59-7c451a4912e0",
        ///       "OP": "測試者",
        ///       "END_QTY": "56"
        ///    }
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data] :sub_content資料結構 </returns>
        [HttpPost("sub_content_add")]
        public string sub_content_add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {    
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.TimeTaken = myTimerBasic.ToString();
                    returnData.Result = $"Data資料不可為空";
                    return returnData.JsonSerializationt();
                }
                List<inspectionClass.sub_content> sub_contents = returnData.Data.ObjToClass< List < inspectionClass.sub_content >> ();
                inspectionClass.sub_content sub_Content = new inspectionClass.sub_content();
                if (sub_contents == null)
                {
                    sub_Content = returnData.Data.ObjToClass<inspectionClass.sub_content>();
                    if(sub_Content != null) sub_contents = new List<inspectionClass.sub_content> { sub_Content };
                }
                if (sub_contents == null)
                {
                    returnData.Code = -200;
                    returnData.TimeTaken = myTimerBasic.ToString();
                    returnData.Result = $"Data資料應為[inspectionClass.sub_content]";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                List<string> GUIDs = sub_contents.Select(x => x.Master_GUID).Distinct().ToList();
                string inClause = string.Join(",", GUIDs.Select(g => $"'{g}'"));  // 加上單引號包起來
                string command = $"SELECT * FROM {DB}.inspection_content WHERE GUID IN ({inClause});";
                DataTable dataTable_inspection_content = sQLControl_inspection_content.WtrteCommandAndExecuteReader(command);
                List<object[]> list_inspection_content = dataTable_inspection_content.DataTableToRowList();
                if (list_inspection_content.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.TimeTaken = myTimerBasic.ToString();
                    returnData.Result = $"找無資料!";
                    returnData.Method = "sub_contents_add";
                    returnData.Data = null;
                    return returnData.JsonSerializationt();
                }
                List<object[]> list_add = new List<object[]>();
                foreach (var item in sub_contents)
                {
                    List<object[]> list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.GUID, item.Master_GUID);
                    object[] value = new object[new enum_驗收明細().GetLength()];
                    value[(int)enum_驗收明細.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_驗收明細.藥品碼] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品碼];
                    value[(int)enum_驗收明細.料號] = list_inspection_content_buf[0][(int)enum_驗收內容.料號];
                    value[(int)enum_驗收明細.驗收單號] = list_inspection_content_buf[0][(int)enum_驗收內容.驗收單號];
                    value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼1];
                    value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼2];

                    value[(int)enum_驗收明細.Master_GUID] = item.Master_GUID;
                    value[(int)enum_驗收明細.效期] = item.效期;
                    value[(int)enum_驗收明細.批號] = item.批號;
                    value[(int)enum_驗收明細.實收數量] = item.實收數量;
                    value[(int)enum_驗收明細.操作人] = item.操作人;
                    value[(int)enum_驗收明細.操作時間] = DateTime.Now.ToDateTimeString();
                    value[(int)enum_驗收明細.狀態] = "未鎖定";

                    list_add.Add(value);
                }
                sQLControl_inspection_sub_content.AddRows(null, list_add);
                if(GUIDs.Count == 1)
                {
                    inspectionClass.content content = new inspectionClass.content();
                    content.GUID = sub_Content.Master_GUID;
                    returnData.Data = content;
                    string json_content = content_get_by_content_GUID(returnData);
                    returnData = json_content.JsonDeserializet<returnData>();
                    if (returnData == null)
                    {
                        returnData.Code = -5;
                        returnData.TimeTaken = myTimerBasic.ToString();
                        returnData.Result = $"搜尋content資料錯誤!";
                        returnData.Method = "sub_content_add";
                        returnData.Data = null;
                        return returnData.JsonSerializationt();
                    }
                    if (returnData.Code < 0)
                    {
                        returnData.TimeTaken = myTimerBasic.ToString();
                        returnData.Method = "sub_content_add";
                        returnData.Data = null;
                        return returnData.JsonSerializationt();
                    } 
                }
                returnData.Code = 200;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Result = $"新增批效成功!";
                returnData.Method = "sub_content_add";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = 200;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Result = $"{ex.Message}";
                returnData.Method = "sub_content_add";
                return returnData.JsonSerializationt(true);
            }          
            
        }
        /// <summary>
        /// 更新單筆驗收藥品中的明細
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    [
        ///       {  
        ///        "GUID": "fad3bbfcfa61",
        ///        "Master_GUID": "13a6625b-b7b2-43b0-ba59-7c451a4912e0"
        ///       }
        ///    ]
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data] :sub_content資料結構 </returns>
        [Route("sub_content_update")]
        [HttpPost]
        public string sub_content_update([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            List<inspectionClass.sub_content> sub_Contents = returnData.Data.ObjToClass<List<inspectionClass.sub_content>>();
            List<object[]> list_sub_Contents = sub_Contents.ClassToSQL<inspectionClass.sub_content, enum_驗收明細>();
          


            sQLControl_inspection_sub_content.UpdateByDefulteExtra(null, list_sub_Contents);

    
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"更新批效成功,共{sQLControl_inspection_sub_content}筆資料";
            returnData.Method = "sub_content_update";
            return returnData.JsonSerializationt();
        }
        /// <summary>
        /// 以GUID刪除驗收藥品中的明細
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    [                 
        ///        {
        ///            "GUID": "a6f58a75-094a-411c-8f87-9804d46b78ea",
        ///            "Master_GUID": "13a6625b-b7b2-43b0-ba59-7c451a4912e0"     
        ///        },
        ///        {
        ///            "GUID": "024e17eb-fa68-4495-92fa-d78ed753c23b",
        ///            "Master_GUID": "13a6625b-b7b2-43b0-ba59-7c451a4912e0"
        ///        }
        ///    ]
        ///  }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data] :content資料結構 </returns>
        [Route("sub_contents_delete_by_GUID")]
        [HttpPost]
        public string sub_contents_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            List<inspectionClass.sub_content> sub_contents = returnData.Data.ObjToListClass<inspectionClass.sub_content>();
            List<object> list_GUID = new List<object>();
            if (sub_contents.Count == 0)
            {
                returnData.Code = -5;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"搜尋sub_content資料錯誤!";
                returnData.Method = "sub_contents_delete_by_GUID";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }
            for (int i = 0; i < sub_contents.Count; i++)
            {
                list_GUID.Add(sub_contents[i].GUID);
            }
            string Master_GUID = sub_contents[0].Master_GUID;
            sQLControl_inspection_sub_content.DeleteExtra(null, enum_驗收明細.GUID.GetEnumName(), list_GUID);

            inspectionClass.content content = new inspectionClass.content();
            content.GUID = Master_GUID;
            returnData.Data = content;
            string json_content = content_get_by_content_GUID(returnData);
            returnData = json_content.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                returnData.Code = -5;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"搜尋content資料錯誤!";
                returnData.Method = "sub_contents_delete_by_GUID";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }
            if (returnData.Code < 0)
            {
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "sub_contents_delete_by_GUID";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }


            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已刪除驗收明細共<{list_GUID.Count}>筆資料!";
            returnData.Method = "sub_contents_delete_by_GUID";

            return returnData.JsonSerializationt();
        }
        /// <summary>
        /// 依藥碼 (CODE) 查詢驗收子明細 (inspection_sub_content)。
        /// </summary>
        /// <remarks>
        /// 本 API 會：  
        /// 1. 從 <c>returnData.Data</c> 或 <c>ValueAry</c> 解析藥碼清單 (欄位 CODE)。  
        /// 2. 至資料表 <c>inspection_sub_content</c> 查詢符合的子明細資料。  
        /// 3. 將結果依 <c>OP_TIME</c> 排序後回傳。  
        ///
        /// ✅ 支援輸入格式：  
        /// - <c>Data</c>：可為單筆或多筆 <c>inspectionClass.sub_content</c>，取其 <c>CODE</c> 欄位。  
        /// - <c>ValueAry</c>：可直接傳入多個藥碼字串。  
        ///
        /// 🔄 請求範例 (單筆)：
        /// <code>
        /// {
        ///   "Data": {
        ///     "CODE": "OARC2"
        ///   },
        ///   "Value": "",
        ///   "ValueAry": [],
        ///   "TableName": "inspection_sub_content",
        ///   "ServerName": "Main",
        ///   "ServerType": "網頁",
        ///   "TimeTaken": ""
        /// }
        /// </code>
        ///
        /// 🔄 請求範例 (多筆)：
        /// <code>
        /// {
        ///   "Data": null,
        ///   "Value": "",
        ///   "ValueAry": ["OARC2", "OPER7", "OTEST1"],
        ///   "TableName": "inspection_sub_content",
        ///   "ServerName": "Main",
        ///   "ServerType": "網頁",
        ///   "TimeTaken": ""
        /// }
        /// </code>
        ///
        /// 🟩 成功回傳範例：
        /// <code>
        /// {
        ///   "Code": 200,
        ///   "Result": "查詢驗收明細&lt;1&gt;筆成功!",
        ///   "Method": "sub_contents_get_by_code",
        ///   "Data": [
        ///     {
        ///       "GUID": "8258d773-b642-4dc6-83a5-ec33d3853e90",
        ///       "Master_GUID": "f6109304-9c41-4741-bc28-cdf3725fc2c2",
        ///       "ACPT_SN": "20250911-0",
        ///       "CODE": "OARC2",
        ///       "NAME": "",
        ///       "SKDIACODE": "ETOO10",
        ///       "BARCODE1": "",
        ///       "BARCODE2": "",
        ///       "END_QTY": "50",
        ///       "VAL": "2025/09/16 00:00:00",
        ///       "LOT": "gasdgasd",
        ///       "OP_TIME": "2025/09/15 16:27:17",
        ///       "OP": "鴻森智能科技",
        ///       "STATE": "未鎖定",
        ///       "NOTE": ""
        ///     }
        ///   ],
        ///   "TimeTaken": "00:00:00.712"
        /// }
        /// </code>
        ///
        /// 🟥 失敗回傳範例：
        /// <code>
        /// {
        ///   "Code": -5,
        ///   "Result": "請提供至少一個藥碼 (CODE)!",
        ///   "Data": null,
        ///   "TimeTaken": "00:00:00.003",
        ///   "Method": "sub_contents_get_by_code"
        /// }
        /// </code>
        ///
        /// 📝 備註：  
        /// - 若輸入無 CODE，回傳 Code = -5。  
        /// - 查詢時會自動去重並去除空白。  
        /// - 結果依 OP_TIME 排序。  
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構，包含 Data/ValueAry 等輸入。</param>
        /// <returns>
        /// JSON 字串：  
        /// - <c>Code</c>：200 成功，-200 系統錯誤，-5 輸入錯誤  
        /// - <c>Result</c>：執行結果描述  
        /// - <c>Data</c>：查詢結果 (sub_content 清單，依 OP_TIME 排序)  
        /// - <c>TimeTaken</c>：執行耗時  
        /// - <c>Method</c>：API 方法名稱 (sub_contents_get_by_code)  
        /// </returns>
        [HttpPost("sub_contents_get_by_code")]
        public string sub_contents_get_by_code([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            // 1) 取連線設定
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            // 2) 建立資料表控制
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);

            // 3) 解析輸入，收集要查的「藥碼」
            List<string> codes = new List<string>();

            // 3-1) 從 Data 嘗試解析成 sub_content（可單筆或多筆）
            List<inspectionClass.sub_content> sub_contents_from_data = returnData.Data.ObjToListClass<inspectionClass.sub_content>();
            if (sub_contents_from_data != null && sub_contents_from_data.Count > 0)
            {
                foreach (var sc in sub_contents_from_data)
                {
                    // 依你的類別命名，JSON 常用 "code" 對應到中文屬性「藥碼」
                    if (sc.藥品碼.StringIsEmpty() == false) codes.Add(sc.藥品碼);
                }
            }
            else
            {
                // 若 Data 不是清單，再試單筆
                var sc = returnData.Data.ObjToClass<inspectionClass.sub_content>();
                if (sc != null && sc.藥品碼.StringIsEmpty() == false) codes.Add(sc.藥品碼);
            }

            // 3-2) 也支援從 ValueAry 傳入多個 code
            if (returnData.ValueAry != null && returnData.ValueAry.Count > 0)
            {
                foreach (var v in returnData.ValueAry)
                {
                    if (v.StringIsEmpty() == false) codes.Add(v);
                }
            }

            // 去重與清理
            codes = codes.Where(x => x.StringIsEmpty() == false).Select(x => x.Trim()).Distinct().ToList();

            if (codes.Count == 0)
            {
                returnData.Code = -5;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"請提供至少一個藥碼 (code)!";
                returnData.Method = "sub_contents_get_by_code";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }

            try
            {
                // 4) 依「藥碼」查詢 inspection_sub_content
                string codeStr = string.Join(",", codes.Select(x => $"'{x}'"));
                string command = $@"
                    SELECT * 
                    FROM {DB}.{sQLControl_inspection_sub_content.TableName} 
                    WHERE 藥品碼 IN ({codeStr});";
                DataTable dt = sQLControl_inspection_sub_content.WtrteCommandAndExecuteReader(command);
                List<object[]> rows = dt.DataTableToRowList();
                List<inspectionClass.sub_content> sub_contents = rows.SQLToClass<inspectionClass.sub_content, enum_驗收明細>();

                // 5) 依操作時間排序
                sub_contents = sub_contents.OrderByDescending(sc => DateTime.Parse(sc.操作時間)).ToList();

                // 6) 組回傳
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"查詢驗收明細<{sub_contents.Count}>筆成功!";
                returnData.Method = "sub_contents_get_by_code";
                returnData.Data = sub_contents;

                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = ex.Message;
                returnData.Method = "sub_contents_get_by_code";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }
        }

        /// <summary>
        /// 以驗收單號下載
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 驗收單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "20231026-0"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>Excel</returns>
        [Route("download_excel_by_IC_SN")]
        [HttpPost]
        public async Task<ActionResult> download_excel_by_IC_SN([FromBody] returnData returnData)
        {
            string VM_API = Method.GetServerAPI("DS01", "藥庫", "API_inspection_excel_download");
            if (VM_API.StringIsEmpty() == false)
            {
                string json_in = returnData.JsonSerializationt();

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(json_in, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(VM_API, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var fileBytes = await response.Content.ReadAsByteArrayAsync();
                        var contentType = response.Content.Headers.ContentType?.MediaType ??
                                          "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                        var fileName = $"{DateTime.Now.ToDateString("-")}_驗收表.xlsx";

                        return File(fileBytes, contentType, fileName);
                    }
                    else
                    {
                        return Content($"下載失敗：{response.StatusCode}");
                    }
                }
            }
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return null;
            }
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            string server = Server;
            string dbName = DB;
            string json = creat_get_by_IC_SN(returnData);
            returnData = json.JsonDeserializet<returnData>();
            Server = server;
            DB = dbName;
            if (returnData.Code != 200)
            {
                return null;
            }
            List<inspectionClass.creat> creats = returnData.Data.ObjToListClass<inspectionClass.creat>();
            inspectionClass.creat creat = creats[0];
            List<SheetClass> sheetClasses = new List<SheetClass>();
            string loadText = Basic.MyFileStream.LoadFileAllText(@"./excel_inspection.txt", "utf-8");
            Console.WriteLine($"取得creats {myTimer.ToString()}");
            SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();
            sheetClass.ReplaceCell(1, 1, $"{creat.驗收單號}");
            sheetClass.ReplaceCell(1, 5, $"{creat.建表人}");
            sheetClass.ReplaceCell(2, 2, $"{creat.請購單號}");
            sheetClass.ReplaceCell(3, 1, $"{creat.驗收開始時間}");
            sheetClass.ReplaceCell(4, 1, $"{creat.驗收結束時間}");
            int NumOfRow = 0;
            for (int i = 0; i < creat.Contents.Count; i++)
            {
                int sub_num = creat.Contents[i].Sub_content.Count;
                int row_start = NumOfRow + 6;
                int row_end = NumOfRow + 6;
                if (sub_num > 0)
                {
                    row_end = NumOfRow + 6 + sub_num - 1;
                    sheetClass.AddNewCell_Webapi(row_start, row_end, 0, 0, $"{creat.Contents[i].藥品碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end, 1, 1, $"{creat.Contents[i].料號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end, 2, 2, $"{creat.Contents[i].藥品名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end, 3, 3, $"{creat.Contents[i].中文名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end, 4, 4, $"{creat.Contents[i].包裝單位}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end, 5, 5, $"{creat.Contents[i].應收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    for (int k = 0; k < creat.Contents[i].Sub_content.Count; k++)
                    {
                        row_end = NumOfRow + 6;
                        sheetClass.AddNewCell_Webapi(row_start + k, row_end + k, 6, 6, $"{creat.Contents[i].Sub_content[k].實收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Center, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(row_start + k, row_end + k, 7, 7, $"{creat.Contents[i].Sub_content[k].效期.StringToDateTime().ToDateString()}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Center, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(row_start + k, row_end + k, 8, 8, $"{creat.Contents[i].Sub_content[k].批號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Center, NPOI.SS.UserModel.BorderStyle.Thin);

                    }
                }

                NumOfRow += sub_num;
            }
            Console.WriteLine($"寫入Sheet {myTimer.ToString()}");

            // sheetClass.NewCell_Webapi_Buffer_Caculate();
            Console.WriteLine($"NewCell_Webapi_Buffer_Caculate {myTimer.ToString()}");

            string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string xls_command = "application/vnd.ms-excel";

            byte[] excelData = sheetClass.NPOI_GetBytes(Excel_Type.xlsx);
            Stream stream = new MemoryStream(excelData);
            return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_驗收表.xlsx"));
        }


        [HttpPost]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<string> PostExcel([FromForm] IFormFile file)
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile == null)
            {
                throw new Exception("文件不能為空");
            }
            string extension = Path.GetExtension(formFile.FileName); // 获取文件的扩展名
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string json = "";
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                json = ExcelClass.NPOI_LoadSheetToJson(memoryStream.ToArray(), extension);
                // 在这里可以对 memoryStream 进行操作，例如读取或写入数据
            }

            return json;
        }

        /// <summary>
        /// 上傳Excel表單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[file] : 上傳xls/xlsx <br/> 
        ///  1.[IC_NAME] : 驗收單名稱 <br/> 
        ///  2.[PON] : 請購單號名稱 <br/> 
        ///  3.[CT] : 操作者 <br/> 
        /// </remarks>
        /// <returns>Excel</returns>
        [Route("excel_upload")]
        [HttpPost]
        public async Task<string> excel([FromForm] IFormFile file, [FromForm] string IC_NAME, [FromForm] string PON, [FromForm] string CT)
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile == null)
            {
                throw new Exception("文件不能為空");
            }
            string extension = Path.GetExtension(formFile.FileName); // 获取文件的扩展名
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClasses_med = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端")[0];

            MED_pageController mED_PageController = new MED_pageController();
            returnData returnData_med = new returnData();
            returnData_med.Server = sys_serverSettingClasses_med.Server;
            returnData_med.DbName = sys_serverSettingClasses_med.DBName;
            returnData_med.TableName = "medicine_page_cloud";
            returnData_med.UserName = sys_serverSettingClasses_med.User;
            returnData_med.Password = sys_serverSettingClasses_med.Password;
            returnData_med.Port = sys_serverSettingClasses_med.Port.StringToUInt32();
            string json_med = Basic.Net.WEBApiPostJson("http://127.0.0.1:4433/api/MED_page", returnData_med.JsonSerializationt());
            returnData_med = json_med.JsonDeserializet<returnData>();
            List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();
            List<medClass> medClasses_buf = new List<medClass>();

            string json = "";
            inspectionClass.creat creat = new inspectionClass.creat();
            creat.驗收名稱 = IC_NAME;
            creat.請購單號 = PON;
            creat.建表人 = CT;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                System.Data.DataTable dt = ExcelClass.NPOI_LoadFile(memoryStream.ToArray(), extension);
                List<object[]> list_value = dt.DataTableToRowList();
                for (int i = 0; i < list_value.Count; i++)
                {
                    inspectionClass.content content = new inspectionClass.content();
                    content.藥品碼 = list_value[i][(int)enum_驗收單匯入.藥碼].ObjectToString();
                    medClasses_buf = (from temp in medClasses
                                      where (temp.藥品碼 == content.藥品碼 || temp.料號 == content.藥品碼)
                                      select temp).ToList();
                    //medClasses_buf = medClasses_buf.Where(temp => temp.開檔狀態.StringIsEmpty() || temp.開檔狀態 == "開檔中").ToList();
                    medClasses_buf = medClasses_buf.Where(temp => temp.開檔狀態.Contains("關檔中") == false).ToList();

                    if (medClasses_buf.Count > 0)
                    {
                        content.藥品碼 = medClasses_buf[0].藥品碼;
                        content.料號 = medClasses_buf[0].料號;
                    }
                    else
                    {
                        content.藥品碼 = "無";
                        content.料號 = "無";
                    }
                    content.藥品名稱 = list_value[i][(int)enum_驗收單匯入.名稱].ObjectToString();
                    content.廠牌 = list_value[i][(int)enum_驗收單匯入.供應商名稱].ObjectToString();
                    content.請購單號 = list_value[i][(int)enum_驗收單匯入.請購單號].ObjectToString();
                    content.應收數量 = list_value[i][(int)enum_驗收單匯入.未交量].ObjectToString();
                    content.實收數量 = list_value[i][(int)enum_驗收單匯入.已交貨數量].ObjectToString();
                    creat.Contents.Add(content);
                }

            }
            returnData returnData = new returnData();

            returnData.Data = creat;

            return creat_add(returnData);
        }
        /// <summary>
        /// 上傳Excel表單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[file] : 上傳xls/xlsx <br/> 
        ///  1.[IC_SN] : 驗收單號 <br/> 
        ///  2.[CT] : 操作者 <br/> 
        /// </remarks>
        /// <returns>Excel</returns>
        [HttpPost("excel_upload_sub_content")]
        public async Task<string> excel([FromForm] IFormFile file, [FromForm] string IC_SN, [FromForm] string CT)
        {
            returnData returnData = new returnData();
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile == null)
            {
                returnData.Code = -200;
                returnData.Result = "文件不可為空";
                return returnData.JsonSerializationt(true);
            }
            if (IC_SN.StringIsEmpty())
            {
                returnData.Code = -200;
                returnData.Result = "IC_SN(驗收單號)不可為空";
                return returnData.JsonSerializationt(true);
            }
            if (CT.StringIsEmpty())
            {
                returnData.Code = -200;
                returnData.Result = "CT(操作人)不可為空";
                return returnData.JsonSerializationt(true);
            }
            List<Task> tasks = new List<Task>();
            inspectionClass.creat creat = new inspectionClass.creat();
            tasks.Add(Task.Run(new Action(delegate 
            {
                returnData.Value = IC_SN;
                string create_string = creat_get_by_IC_SN(returnData);
                returnData = create_string.JsonDeserializet<returnData>();
                creat = returnData.Data.ObjToClass<List<inspectionClass.creat>>()[0];
            })));

            string extension = Path.GetExtension(formFile.FileName); // 获取文件的扩展名
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<object[]> list_value = new List<object[]>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                System.Data.DataTable dt = ExcelClass.NPOI_LoadFile(memoryStream.ToArray(), extension);
                dt = dt.ReorderTable(new enum_出貨單匯入());
                if (dt == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "上傳文件表頭應為請購單號、品名、出貨數量、效期!";
                    return returnData.JsonSerializationt(true);
                }
                list_value = dt.DataTableToRowList();
            }
            if (list_value.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = "無傳入資料!";
                return returnData.JsonSerializationt(true);
            }

            Task.WhenAll(tasks).Wait();
            tasks.Clear();

            if (creat == null)
            {
                returnData.Code = -200;
                returnData.Result = $"查無此驗收單號{IC_SN}";
                return returnData.JsonSerializationt(true);
            }
            List<inspectionClass.content> content = creat.Contents;           
            
            List<inspectionClass.sub_content> list_sub_content = new List<inspectionClass.sub_content>();
            foreach(var item in list_value)
            {
                
                string 請購單號 = item[(int)enum_出貨單匯入.請購單號].ObjectToString();
                inspectionClass.content content_buff = content.Where(item => item.請購單號 == 請購單號).FirstOrDefault();
                if (content_buff == null) continue;
                inspectionClass.sub_content sub_content = new inspectionClass.sub_content();
                sub_content.Master_GUID = content_buff.GUID;
                string 效期 = EditExpirydate(item[(int)enum_出貨單匯入.效期].ObjectToString());
                sub_content.效期 = 效期;
                sub_content.批號 = item[(int)enum_出貨單匯入.批號].ObjectToString();
                sub_content.實收數量 = content_buff.應收數量;
                sub_content.操作人 = CT;               
                list_sub_content.Add(sub_content);                
            }
            returnData.Data = list_sub_content;
            string reslut = sub_content_add(returnData);       
            return reslut;
        }

        private returnData Function_Get_inspection_creat(sys_serverSettingClass sys_serverSettingClass, string MED_TableName, List<object[]> list_inspection_creat)
        {
            return Function_Get_inspection_creat(sys_serverSettingClass, MED_TableName, list_inspection_creat, true);
        }
        private returnData Function_Get_inspection_creat(sys_serverSettingClass sys_serverSettingClass, string MED_TableName, List<object[]> list_inspection_creat, bool allData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClasses_med = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端")[0];

            MED_pageController mED_PageController = new MED_pageController();
            returnData returnData_med = new returnData();
            returnData_med.Server = sys_serverSettingClasses_med.Server;
            returnData_med.DbName = sys_serverSettingClasses_med.DBName;
            returnData_med.TableName = "medicine_page_cloud";
            returnData_med.UserName = sys_serverSettingClasses_med.User;
            returnData_med.Password = sys_serverSettingClasses_med.Password;
            returnData_med.Port = sys_serverSettingClasses_med.Port.StringToUInt32();
            returnData_med = mED_PageController.Get(returnData_med).JsonDeserializet<returnData>();
            List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();
            List<medClass> medClasses_buf = new List<medClass>();

            returnData returnData = new returnData();
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            List<object[]> list_sub_inspection = sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_sub_inspection_buf = new List<object[]>();
            string 藥品碼 = "";
            string 料號 = "";
            string 藥品名稱 = "";
            string 中文名稱 = "";
            string 包裝單位 = "";
            List<inspectionClass.creat> creats = new List<inspectionClass.creat>();
            for (int i = 0; i < list_inspection_creat.Count; i++)
            {
                inspectionClass.creat creat = list_inspection_creat[i].SQLToClass<inspectionClass.creat, enum_驗收單號>();
                if (allData)
                {
                    料號 = "";
                    藥品名稱 = "";
                    中文名稱 = "";
                    包裝單位 = "";

                    list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.Master_GUID, creat.GUID);
                    for (int k = 0; k < list_inspection_content_buf.Count; k++)
                    {
                        inspectionClass.content content = list_inspection_content_buf[k].SQLToClass<inspectionClass.content, enum_驗收內容>();
                        藥品碼 = content.藥品碼;
                        if (medClasses != null)
                        {
                            medClasses_buf = (from value in medClasses
                                              where (value.藥品碼 == content.藥品碼 || value.料號 == content.藥品碼)
                                              select value).ToList();
                            if (medClasses_buf.Count > 0)
                            {
                                料號 = medClasses_buf[0].料號;
                                藥品名稱 = medClasses_buf[0].藥品名稱;
                                中文名稱 = medClasses_buf[0].中文名稱;
                                包裝單位 = medClasses_buf[0].包裝單位;
                            }
                        }
                        content.料號 = 料號;
                        content.藥品名稱 = 藥品名稱;
                        content.中文名稱 = 中文名稱;
                        content.包裝單位 = 包裝單位;

                        int 實收數量 = 0;
                        list_inspection_sub_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收明細.Master_GUID, content.GUID);
                        for (int m = 0; m < list_inspection_sub_content_buf.Count; m++)
                        {
                            inspectionClass.sub_content sub_Content = list_inspection_sub_content_buf[m].SQLToClass<inspectionClass.sub_content, enum_驗收明細>();
                            sub_Content.藥品名稱 = content.藥品名稱;
                            sub_Content.中文名稱 = content.中文名稱;
                            sub_Content.包裝單位 = content.包裝單位;
                            if (sub_Content.實收數量.StringIsInt32())
                            {

                                實收數量 += sub_Content.實收數量.StringToInt32();
                            }
                            content.Sub_content.Add(sub_Content);
                        }
                        content.實收數量 = 實收數量.ToString();
                        content.Sub_content.Sort(new ICP_sub_content());
                        creat.Contents.Add(content);
                    }
                }
                creat.Contents.Sort(new ICP_Contents());
                creats.Add(creat);
            }
            creats.Sort(new ICP_creat_by_CT_TIME());
            returnData.Data = creats;
            returnData.Code = 200;
            returnData.Server = Server;
            returnData.DbName = DB;



            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData;
        }



        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {
            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_驗收單號()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_驗收內容()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_驗收明細()));
            return tables.JsonSerializationt(true);

        }

        private class ICP_creat_by_CT_TIME : IComparer<inspectionClass.creat>
        {
            public int Compare(inspectionClass.creat x, inspectionClass.creat y)
            {
                string Code0 = x.建表時間;
                string Code1 = y.建表時間;
                return Code1.CompareTo(Code0);
            }
        } 
        private class ICP_Contents : IComparer<inspectionClass.content>
        {
            public int Compare(inspectionClass.content x, inspectionClass.content y)
            {
                if (x.應收數量 == "0" && y.應收數量 == "0")
                {
                    return x.藥品碼.CompareTo(y.藥品碼);
                }
                else if (x.應收數量 != "0" && y.應收數量 != "0")
                {
                    // 先按照應收數量进行排序
                    int theoryComparison = x.應收數量.CompareTo(y.應收數量);

                    if (theoryComparison == 0)
                    {
                        // 如果應收數量相同，则按照藥品碼进行排序
                        return x.藥品碼.CompareTo(y.藥品碼);
                    }
                    else
                    {
                        return theoryComparison;
                    }
                }
                else
                {
                    // 如果只有一个项的應收數量为0，则将具有非零應收數量的项排在前面
                    return y.應收數量.CompareTo(x.應收數量);
                }
            }
        }
        private class ICP_sub_content : IComparer<inspectionClass.sub_content>
        {
            public int Compare(inspectionClass.sub_content x, inspectionClass.sub_content y)
            {
                return x.操作時間.CompareTo(y.操作時間);
            }
        }
        private string EditExpirydate(string 效期)
        {
            if (Regex.IsMatch(效期, @"^\d{3}/\d{2}/\d{2}$")) // 範例：025/08/16
            {
                效期 = "2" + 效期; // 轉為 2025/08/16
            }
            string[] formats = { "MM/dd/yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "yyyy.MM.dd", "yyyy/MM/dd HH:mm:ss", "yyyy/MM/dd", "yyyyMMdd" }; // 可擴展格式

            if (DateTime.TryParseExact(效期, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                效期 = date.ToDateTimeString();
            }
            else
            {
                效期 = DateTime.MinValue.ToDateTimeString();
            }
            return 效期;
        }

    }
}
