using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text; 
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;
using MyOffice;
using System.IO;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;
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
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0]);
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

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();


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
        public string POST_creat_get_by_CT_TIME_ST_END([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
                returnData = Function_Get_inspection_creat(serverSettingClasses[0], returnData.TableName, list_inspection_creat, false);
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
        public string POST_creat_get_by_CT_TIME([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
                    returnData = Function_Get_inspection_creat(serverSettingClasses[0], returnData.TableName, list_inspection_creat, false);
                }
                else
                {
                    returnData = Function_Get_inspection_creat(serverSettingClasses[0], returnData.TableName, list_inspection_creat);
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
        public string POST_creat_update_startime_by_IC_SN([FromBody] returnData returnData)
        {
            try
            {

                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();
                string json_out = POST_creat_get_by_IC_SN(returnData);
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
        ///     "Value" : "20231026-0"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為驗收單結構</returns>
        [Route("creat_get_by_IC_SN")]
        [HttpPost]
        public string POST_creat_get_by_IC_SN([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
                inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();

                sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);

                List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
                list_inspection_creat = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, creat.驗收單號);
                if (list_inspection_creat.Count == 0)
                {
                    returnData.Code = -5;
                    returnData.Result = $"查無此單號資料[{returnData.Value}]!";
                    return returnData.JsonSerializationt();
                }
                MED_pageController mED_PageController = new MED_pageController();

                returnData = Function_Get_inspection_creat(serverSettingClasses[0], returnData.TableName, list_inspection_creat);
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
        public string POST_creat_add([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            inspectionClass.creat creat = returnData.Data.ObjToClass<inspectionClass.creat>();

            List<object[]> list_inspection_creat = sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();


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
            }
            sQLControl_inspection_creat.AddRows(null, list_inspection_creat_add);
            sQLControl_inspection_content.AddRows(null, list_inspection_content_add);
            returnData.Data = creat;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "creat_add";

            returnData.Result = $"成功加入新驗收單! 共{list_inspection_content_add.Count}筆資料";
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
        public string POST_creat_auto_add([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();

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

                returnData_med = mED_PageController.POST_get_by_apiserver(returnData_med).JsonDeserializet<returnData>();
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

                return POST_creat_add(returnData);
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
        public string POST_creat_lock([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
        public string POST_creat_unlock([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
        public string POST_creat_delete_by_IC_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
        public string POST_contents_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
        public string POST_content_get_by_content_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);

            inspectionClass.content content = returnData.Data.ObjToClass<inspectionClass.content>();;
            //if (returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = "輸入資料不得空白!";
            //    return returnData.JsonSerializationt();
            //}

            string GUID = content.GUID;
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
                string json_med = mED_PageController.POST_get_by_code(returnData_med);
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

                content.實收數量 = 實收數量.ToString();
                content.Sub_content.Sort(new ICP_sub_content());
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
        public string POST_sub_content_get_by_content_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            inspectionClass.content content = returnData.Data.ObjToClass<inspectionClass.content>();;
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
                inspectionClass.sub_content sub_Content = list_inspection_sub_content_buf[i].SQLToClass< inspectionClass.sub_content , enum_驗收明細>();
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
        public string POST_sub_content_add_single([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
        [Route("sub_content_add")]
        [HttpPost]
        public string POST_sub_content_add([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);
            List<object[]> list_inspection_content = sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_add = new List<object[]>();
            inspectionClass.sub_content sub_content = returnData.Data.ObjToClass<inspectionClass.sub_content>();
            string Master_GUID = sub_content.Master_GUID;
            list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.GUID, Master_GUID);
            if (list_inspection_content_buf.Count == 0)
            {
                returnData.Code = -5;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"找無資料!";
                returnData.Method = "sub_content_add";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }

            object[] value = new object[new enum_驗收明細().GetLength()];
            value[(int)enum_驗收明細.GUID] = Guid.NewGuid().ToString();
            value[(int)enum_驗收明細.藥品碼] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品碼];
            value[(int)enum_驗收明細.料號] = list_inspection_content_buf[0][(int)enum_驗收內容.料號];
            value[(int)enum_驗收明細.驗收單號] = list_inspection_content_buf[0][(int)enum_驗收內容.驗收單號];
            value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼1];
            value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼2];

            value[(int)enum_驗收明細.Master_GUID] = Master_GUID;
            value[(int)enum_驗收明細.效期] = sub_content.效期;
            value[(int)enum_驗收明細.批號] = sub_content.批號;
            value[(int)enum_驗收明細.實收數量] = sub_content.實收數量;
            value[(int)enum_驗收明細.操作人] = sub_content.操作人;
            value[(int)enum_驗收明細.操作時間] = DateTime.Now.ToDateTimeString();
            value[(int)enum_驗收明細.狀態] = "未鎖定";

            list_add.Add(value);

            sQLControl_inspection_sub_content.AddRows(null, list_add);

            inspectionClass.content content = new inspectionClass.content();
            content.GUID = Master_GUID;
            returnData.Data = content;
            string json_content = POST_content_get_by_content_GUID(returnData);
            returnData = json_content.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                returnData.Code = -5;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"搜尋content資料錯誤!";
                returnData.Method = "sub_content_add";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }
            if (returnData.Code < 0)
            {
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "sub_content_add";
                returnData.Data = null;
                return returnData.JsonSerializationt();
            }

            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"新增批效成功!";
            returnData.Method = "sub_content_add";
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
        public string POST_sub_contents_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
            string json_content = POST_content_get_by_content_GUID(returnData);
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
        public async Task<ActionResult> Post_download_excel_by_IC_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return null;
            }
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

            string server = Server;
            string dbName = DB;
            string json = POST_creat_get_by_IC_SN(returnData);
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
                    sheetClass.AddNewCell_Webapi(row_start, row_end , 0, 0, $"{creat.Contents[i].藥品碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end , 1, 1, $"{creat.Contents[i].料號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end , 2, 2, $"{creat.Contents[i].藥品名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end , 3, 3, $"{creat.Contents[i].中文名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end , 4, 4, $"{creat.Contents[i].包裝單位}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(row_start, row_end , 5, 5, $"{creat.Contents[i].應收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    for (int k = 0; k < creat.Contents[i].Sub_content.Count; k++)
                    {
                        row_end = NumOfRow + 6;
                        sheetClass.AddNewCell_Webapi(row_start + k, row_end + k, 6, 6, $"{creat.Contents[i].Sub_content[k].實收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Center, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(row_start + k, row_end + k, 7, 7, $"{creat.Contents[i].Sub_content[k].效期.StringToDateTime().ToDateString()}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Center, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(row_start + k, row_end + k, 8, 8, $"{creat.Contents[i].Sub_content[k].批號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Center, NPOI.SS.UserModel.BorderStyle.Thin);

                    }
                }

                NumOfRow+= sub_num;
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
        public async Task<string> POST_excel([FromForm] IFormFile file, [FromForm] string IC_NAME, [FromForm] string PON, [FromForm] string CT)
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile == null)
            {
                throw new Exception("文件不能為空");
            }
            string extension = Path.GetExtension(formFile.FileName); // 获取文件的扩展名
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "VM端")[0];

            MED_pageController mED_PageController = new MED_pageController();
            returnData returnData_med = new returnData();
            returnData_med.Server = serverSettingClasses_med.Server;
            returnData_med.DbName = serverSettingClasses_med.DBName;
            returnData_med.TableName = "medicine_page_cloud";
            returnData_med.UserName = serverSettingClasses_med.User;
            returnData_med.Password = serverSettingClasses_med.Password;
            returnData_med.Port = serverSettingClasses_med.Port.StringToUInt32();
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
                for(int i = 0; i < list_value.Count; i++)
                {
                    inspectionClass.content content = new inspectionClass.content();
                    content.藥品碼 = list_value[i][(int)enum_驗收單匯入.藥碼].ObjectToString();
                    medClasses_buf = (from temp in medClasses
                                      where (temp.藥品碼 == content.藥品碼  || temp.料號 == content.藥品碼)
                                      select temp).ToList();
                    if(medClasses_buf.Count > 0)
                    {
                        content.藥品碼 = medClasses_buf[0].藥品碼;
                        content.料號 = medClasses_buf[0].料號;
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

            return POST_creat_add(returnData);
        }


        private returnData Function_Get_inspection_creat(ServerSettingClass serverSettingClass, string MED_TableName, List<object[]> list_inspection_creat)
        {
            return Function_Get_inspection_creat(serverSettingClass, MED_TableName, list_inspection_creat, true);
        }
        private returnData Function_Get_inspection_creat(ServerSettingClass serverSettingClass, string MED_TableName, List<object[]> list_inspection_creat, bool allData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "VM端")[0];

            MED_pageController mED_PageController = new MED_pageController();
            returnData returnData_med = new returnData();
            returnData_med.Server = serverSettingClasses_med.Server;
            returnData_med.DbName = serverSettingClasses_med.DBName;
            returnData_med.TableName = "medicine_page_cloud";
            returnData_med.UserName = serverSettingClasses_med.User;
            returnData_med.Password = serverSettingClasses_med.Password;
            returnData_med.Port = serverSettingClasses_med.Port.StringToUInt32();
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
            string 藥品名稱 = "";
            string 中文名稱 = "";
            string 包裝單位 = "";
            List<inspectionClass.creat> creats = new List<inspectionClass.creat>();
            for (int i = 0; i < list_inspection_creat.Count; i++)
            {
                inspectionClass.creat creat = list_inspection_creat[i].SQLToClass<inspectionClass.creat, enum_驗收單號>();
                if (allData)
                {
                    list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.Master_GUID, creat.GUID);
                    for (int k = 0; k < list_inspection_content_buf.Count; k++)
                    {
                        inspectionClass.content content = list_inspection_content_buf[k].SQLToClass<inspectionClass.content, enum_驗收內容>();
                        藥品碼 = content.藥品碼;
                        if (medClasses != null)
                        {
                            medClasses_buf = (from value in medClasses
                                              where value.藥品碼 == 藥品碼
                                              select value).ToList();
                            if (medClasses_buf.Count > 0)
                            {
                                藥品名稱 = medClasses_buf[0].藥品名稱;
                                中文名稱 = medClasses_buf[0].中文名稱;
                                包裝單位 = medClasses_buf[0].包裝單位;
                            }
                        }
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
      


        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl_inspection_creat = new SQLControl(Server, DB, "inspection_creat", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_content = new SQLControl(Server, DB, "inspection_content", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inspection_sub_content = new SQLControl(Server, DB, "inspection_sub_content", UserName, Password, Port, SSLMode);

            List<Table> tables = new List<Table>();
            Table table_inspection_creat;
            table_inspection_creat = new Table("inspection_creat");
            table_inspection_creat.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_inspection_creat.AddColumnList("驗收名稱", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_inspection_creat.AddColumnList("請購單號", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_inspection_creat.AddColumnList("驗收單號", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_inspection_creat.AddColumnList("建表人", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_inspection_creat.AddColumnList("建表時間", Table.DateType.DATETIME, 50, Table.IndexType.None);
            table_inspection_creat.AddColumnList("驗收開始時間", Table.DateType.DATETIME, 50, Table.IndexType.None);
            table_inspection_creat.AddColumnList("驗收結束時間", Table.DateType.DATETIME, 50, Table.IndexType.None);
            table_inspection_creat.AddColumnList("驗收狀態", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_inspection_creat.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            if (!sQLControl_inspection_creat.IsTableCreat()) sQLControl_inspection_creat.CreatTable(table_inspection_creat);
            else sQLControl_inspection_creat.CheckAllColumnName(table_inspection_creat, true);
            tables.Add(table_inspection_creat);

            Table table_inspection_content;
            table_inspection_content = new Table("inspection_content");
            table_inspection_content.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_inspection_content.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_inspection_content.AddColumnList("請購單號", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
            table_inspection_content.AddColumnList("驗收單號", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
            table_inspection_content.AddColumnList("藥品碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_inspection_content.AddColumnList("廠牌", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_inspection_content.AddColumnList("料號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_inspection_content.AddColumnList("藥品條碼1", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_inspection_content.AddColumnList("藥品條碼2", Table.StringType.TEXT, 30, Table.IndexType.None);
            table_inspection_content.AddColumnList("應收數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_inspection_content.AddColumnList("新增時間", Table.DateType.DATETIME, 30, Table.IndexType.None);
            table_inspection_content.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            if (!sQLControl_inspection_content.IsTableCreat()) sQLControl_inspection_content.CreatTable(table_inspection_content);
            else sQLControl_inspection_content.CheckAllColumnName(table_inspection_content, true);
            tables.Add(table_inspection_content);

            Table table_inspection_sub_content;
            table_inspection_sub_content = new Table("inspection_sub_content");
            table_inspection_sub_content.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_inspection_sub_content.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_inspection_sub_content.AddColumnList("驗收單號", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
            table_inspection_sub_content.AddColumnList("藥品碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_inspection_sub_content.AddColumnList("料號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_inspection_sub_content.AddColumnList("藥品條碼1", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("藥品條碼2", Table.StringType.TEXT, 30, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("實收數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("效期", Table.DateType.DATETIME, 30, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("批號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("操作時間", Table.DateType.DATETIME, 30, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("操作人", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("狀態", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table_inspection_sub_content.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);

            if (!sQLControl_inspection_sub_content.IsTableCreat()) sQLControl_inspection_sub_content.CreatTable(table_inspection_sub_content);
            else sQLControl_inspection_sub_content.CheckAllColumnName(table_inspection_sub_content, true);
            tables.Add(table_inspection_sub_content);

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
    }
}
