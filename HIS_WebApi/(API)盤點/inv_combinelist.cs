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
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;
using MyOffice;
using NPOI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;
using System.Text;
using System.Text.RegularExpressions;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inv_combinelist : Controller
    {
        private class SheetTemp
        {
            public SheetTemp(string name)
            {
                Name = name;
            }
            public string Name = "";
            public List<object[]> list_value = new List<object[]>();
        }

        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化合併單資料庫
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
        /// 取得可建立今日最新合併單號
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
        /// <returns>[returnData.Value]為建立合併單號</returns>
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


            SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inv_sub_combinelist = new SQLControl(Server, DB, "inv_sub_combinelist", UserName, Password, Port, SSLMode);

            List<object[]> list_inv_combinelist = sQLControl_inv_combinelist.GetAllRows(null);
            List<object[]> list_inv_combinelist_buf = new List<object[]>();

            list_inv_combinelist_buf = list_inv_combinelist.GetRowsInDate((int)enum_inv_combinelist.建表時間, DateTime.Now);


            string 單號 = "";
            int index = 0;
            while (true)
            {
                單號 = $"I{DateTime.Now.ToDateTinyString()}-{index}";
                index++;
                list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_inv_combinelist.合併單號, 單號);
                if (list_inv_combinelist_buf.Count == 0) break;
            }
            returnData.Value = 單號;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "new_IC_SN";
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 創建合併單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單名稱 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///        "INV_NAME": "測試合併",
        ///        "INV_SN": "I20240103-14",
        ///        "CT": "王曉明",
        ///        "NOTE": "",
        ///        "records_Ary" :
        ///        [
        ///          {
        ///             "SN" : "20240103-1",
        ///             "TYPE" : "盤點單"
        ///          },
        ///          {
        ///             "SN" : "20240102-0",
        ///             "TYPE" : "盤點單"
        ///          }
        ///        ]
        ///     }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("inv_creat_update")]
        [HttpPost]
        public string POST_inv_creat_update([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);

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

            SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inv_sub_combinelist = new SQLControl(Server, DB, "inv_sub_combinelist", UserName, Password, Port, SSLMode);
            inv_combinelistClass inv_CombinelistClass = returnData.Data.ObjToClass<inv_combinelistClass>();

            List<object[]> list_inv_combinelist = sQLControl_inv_combinelist.GetAllRows(null);
            List<object[]> list_inv_combinelist_buf = new List<object[]>();

            List<object[]> list_inv_sub_combinelist = sQLControl_inv_sub_combinelist.GetAllRows(null);
            List<object[]> list_inv_sub_combinelist_buf = new List<object[]>();
            if (inv_CombinelistClass.合併單號.StringIsEmpty() == true)
            {
                returnData returnData_buf = new returnData();
                returnData_buf = GET_new_IC_SN(returnData).JsonDeserializet<returnData>();
                if (returnData_buf.Code != 200)
                {
                    returnData.Code = -200;
                    returnData.Result = "inv_CombinelistClass.合併單號, 空白,請輸入合併單號!";
                    return returnData.JsonSerializationt();
                }
                inv_CombinelistClass.合併單號 = returnData.Value;
            }
            if (inv_CombinelistClass == null)
            {
                returnData.Code = -200;
                returnData.Result += $"Data 資料錯誤 \n";
                return returnData.JsonSerializationt();
            }
            //returnData returnData_SN = GET_new_IC_SN(returnData).JsonDeserializet<returnData>();
            //inv_CombinelistClass.合併單號 = returnData_SN.Value;
            list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_inv_combinelist.合併單號, inv_CombinelistClass.合併單號);
            list_inv_sub_combinelist_buf = list_inv_sub_combinelist.GetRows((int)enum_inv_sub_combinelist.合併單號, inv_CombinelistClass.合併單號);

            if (list_inv_combinelist_buf.Count > 0) sQLControl_inv_combinelist.DeleteExtra(null, list_inv_combinelist_buf);
            if (list_inv_sub_combinelist_buf.Count > 0) sQLControl_inv_sub_combinelist.DeleteExtra(null, list_inv_sub_combinelist_buf);


            inv_CombinelistClass.GUID = Guid.NewGuid().ToString();
            inv_CombinelistClass.建表時間 = DateTime.Now.ToDateTimeString();


            List<object[]> list_inv_Combinelist_add = new List<object[]>();
            List<object[]> list_inv_sub_combinelist_add = new List<object[]>();
            inv_CombinelistClass.消耗量起始時間 = DateTime.MinValue.ToDateTimeString();
            inv_CombinelistClass.消耗量結束時間 = DateTime.MinValue.ToDateTimeString();
            object[] value;
            value = inv_CombinelistClass.ClassToSQL<inv_combinelistClass, enum_inv_combinelist>();

            list_inv_Combinelist_add.Add(value);

            for (int i = 0; i < inv_CombinelistClass.Records_Ary.Count; i++)
            {
                value = new object[new enum_inv_sub_combinelist().GetLength()];
                value[(int)enum_inv_sub_combinelist.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_inv_sub_combinelist.Master_GUID] = inv_CombinelistClass.GUID;
                value[(int)enum_inv_sub_combinelist.合併單號] = inv_CombinelistClass.合併單號;
                value[(int)enum_inv_sub_combinelist.單號] = inv_CombinelistClass.Records_Ary[i].單號;
                value[(int)enum_inv_sub_combinelist.類型] = inv_CombinelistClass.Records_Ary[i].類型;
                value[(int)enum_inv_sub_combinelist.新增時間] = DateTime.Now.ToDateTimeString();
                list_inv_sub_combinelist_add.Add(value);
            }

            sQLControl_inv_combinelist.AddRows(null, list_inv_Combinelist_add);
            sQLControl_inv_sub_combinelist.AddRows(null, list_inv_sub_combinelist_add);
            returnData.Data = inv_CombinelistClass;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "inv_creat_update";

            returnData.Result = $"成功加入新合併單! 共{list_inv_sub_combinelist_add.Count}筆明細資料";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以GUID更新 盤點日庫存單號
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///  
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "GUID",
        ///       "StockRecord_GUID"
        ///       "StockRecord_ServerName"
        ///       "StockRecord_ServerType"
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("inv_stockrecord_update_by_GUID")]
        [HttpPost]
        public string POST_inv_stockrecord_update_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 4)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID],[StockRecord_GUID],[StockRecord_ServerName],[StockRecord_ServerType]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                string StockRecord_GUID = returnData.ValueAry[1];
                string StockRecord_ServerName = returnData.ValueAry[2];
                string StockRecord_ServerType = returnData.ValueAry[3];
                if (GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Value [GUID]不得為空白";
                    return returnData.JsonSerializationt(true);
                }



                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);

                List<object[]> list_inv_combinelist = sQLControl_inv_combinelist.GetRowsByDefult(null, (int)enum_inv_combinelist.GUID, GUID);
                if (list_inv_combinelist.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<object[]> list_inv_combinelist_buf = new List<object[]>();


                object[] value;
                value = list_inv_combinelist[0];
                value[(int)enum_inv_combinelist.StockRecord_GUID] = StockRecord_GUID;
                value[(int)enum_inv_combinelist.StockRecord_ServerName] = StockRecord_ServerName;
                value[(int)enum_inv_combinelist.StockRecord_ServerType] = StockRecord_ServerType;

                sQLControl_inv_combinelist.UpdateByDefulteExtra(null, value);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "inv_stockrecord_update_by_GUID";
                returnData.Result = $"更新盤點日庫存單號成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 以GUID更新 消耗量計算起始及結束日期
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///  
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "GUID",
        ///       "startTime"
        ///       "endTime"
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("inv_consumption_time_update_by_GUID")]
        [HttpPost]
        public string POST_inv_consumption_time_update_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID],[startTime],[endTime]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                string startTime = returnData.ValueAry[1];
                string endTime = returnData.ValueAry[2];
                if (GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Value [GUID]不得為空白";
                    return returnData.JsonSerializationt(true);
                }
                if (startTime.Check_Date_String() == false || endTime.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Value [startTime]或[endTime] 日期格式錯誤";
                    return returnData.JsonSerializationt(true);
                }


                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);

                List<object[]> list_inv_combinelist = sQLControl_inv_combinelist.GetRowsByDefult(null, (int)enum_inv_combinelist.GUID, GUID);
                if (list_inv_combinelist.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<object[]> list_inv_combinelist_buf = new List<object[]>();


                object[] value;
                value = list_inv_combinelist[0];
                value[(int)enum_inv_combinelist.消耗量起始時間] = startTime;
                value[(int)enum_inv_combinelist.消耗量結束時間] = endTime;

                sQLControl_inv_combinelist.UpdateByDefulteExtra(null, value);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "inv_stockrecord_update_by_GUID";
                returnData.Result = $"更新消耗量計算起始及結束日期成功! 起始時間[{startTime}] 結束時間[{endTime}]";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 取得所有合併單資料
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單名稱 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///    
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_all_inv")]
        [HttpPost]
        public string POST_get_all_inv([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);

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

            SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inv_sub_combinelist = new SQLControl(Server, DB, "inv_sub_combinelist", UserName, Password, Port, SSLMode);

            List<object[]> list_inv_combinelist = sQLControl_inv_combinelist.GetAllRows(null);
            List<object[]> list_inv_combinelist_buf = new List<object[]>();

            List<object[]> list_inv_sub_combinelist = sQLControl_inv_sub_combinelist.GetAllRows(null);
            List<object[]> list_inv_sub_combinelist_buf = new List<object[]>();


            SQLControl sQLControl_inventory_creat = new SQLControl(Server, DB, "inventory_creat", UserName, Password, Port, SSLMode);
            List<object[]> list_inventory_creat = sQLControl_inventory_creat.GetAllRows(null);
            List<object[]> list_inventory_creat_buf = new List<object[]>();

            List<inv_combinelistClass> inv_CombinelistClasses = new List<inv_combinelistClass>();
            string 合併單號 = "";

            for (int i = 0; i < list_inv_combinelist.Count; i++)
            {
                合併單號 = list_inv_combinelist[i][(int)enum_inv_combinelist.合併單號].ObjectToString();
                list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_inv_combinelist.合併單號, 合併單號);
                list_inv_sub_combinelist_buf = list_inv_sub_combinelist.GetRows((int)enum_inv_sub_combinelist.合併單號, 合併單號);
                inv_combinelistClass inv_CombinelistClass = list_inv_combinelist_buf[0].SQLToClass<inv_combinelistClass, enum_inv_combinelist>();
                List<inv_records_Class> inv_Sub_CombinelistClasses = list_inv_sub_combinelist_buf.SQLToClass<inv_records_Class, enum_inv_sub_combinelist>();
                List<inv_records_Class> inv_Sub_CombinelistClasses_buf = new List<inv_records_Class>();
                for (int k = 0; k < inv_Sub_CombinelistClasses.Count; k++)
                {
                    string 單號 = inv_Sub_CombinelistClasses[k].單號;
                    string 類型 = inv_Sub_CombinelistClasses[k].類型;
                    if (類型 == "盤點單")
                    {
                        list_inventory_creat_buf = list_inventory_creat.GetRows((int)enum_盤點單號.盤點單號, 單號);
                        if (list_inventory_creat_buf.Count > 0)
                        {
                            inv_Sub_CombinelistClasses[k].名稱 = list_inventory_creat_buf[0][(int)enum_盤點單號.盤點名稱].ObjectToString();
                            inv_Sub_CombinelistClasses[k].單號 = list_inventory_creat_buf[0][(int)enum_盤點單號.盤點單號].ObjectToString();
                            inv_Sub_CombinelistClasses[k].類型 = "盤點單";
                            inv_Sub_CombinelistClasses_buf.Add(inv_Sub_CombinelistClasses[k]);
                        }
                    }
                }
                inv_CombinelistClass.Records_Ary = inv_Sub_CombinelistClasses_buf;
                inv_CombinelistClasses.Add(inv_CombinelistClass);
            }


            returnData.Data = inv_CombinelistClasses;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "get_all_inv";

            returnData.Result = $"成功取得資料";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 取得所有可合併單
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Data": 
        ///    {                 
        ///        
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_all_records")]
        [HttpPost]
        public string POST_get_all_records([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);

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

            SQLControl sQLControl_inventory_creat = new SQLControl(Server, DB, "inventory_creat", UserName, Password, Port, SSLMode);


            List<object[]> list_inventory_creat = sQLControl_inventory_creat.GetAllRows(null);

            List<inv_records_Class> inv_Records_Classes = new List<inv_records_Class>();
            for (int i = 0; i < list_inventory_creat.Count; i++)
            {
                inv_records_Class inv_Records_Class = new inv_records_Class();
                inv_Records_Class.GUID = list_inventory_creat[i][(int)enum_盤點單號.GUID].ObjectToString();
                inv_Records_Class.名稱 = list_inventory_creat[i][(int)enum_盤點單號.盤點名稱].ObjectToString();
                inv_Records_Class.單號 = list_inventory_creat[i][(int)enum_盤點單號.盤點單號].ObjectToString();
                inv_Records_Class.類型 = "盤點單";
                inv_Records_Classes.Add(inv_Records_Class);
            }

            returnData.Data = inv_Records_Classes;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "get_all_records";

            returnData.Result = $"取得所有單資料共<{list_inventory_creat.Count}>筆";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以合併單號刪除合併單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240102-0",
        ///    "Data": 
        ///    {                 
        ///    
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("inv_delete_by_SN")]
        [HttpPost]
        public string POST_inv_delete_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);

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

            if (returnData.Value.StringIsEmpty() == true)
            {
                returnData.Code = -200;
                returnData.Result = "returnData.Value 空白,請輸入合併單號!";
                return returnData.JsonSerializationt();
            }

            SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inv_sub_combinelist = new SQLControl(Server, DB, "inv_sub_combinelist", UserName, Password, Port, SSLMode);

            List<object[]> list_inv_combinelist = sQLControl_inv_combinelist.GetAllRows(null);
            List<object[]> list_inv_combinelist_buf = new List<object[]>();

            List<object[]> list_inv_sub_combinelist = sQLControl_inv_sub_combinelist.GetAllRows(null);
            List<object[]> list_inv_sub_combinelist_buf = new List<object[]>();


            List<inv_combinelistClass> inv_CombinelistClasses = new List<inv_combinelistClass>();
            string 合併單號 = returnData.Value;

            list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_inv_combinelist.合併單號, 合併單號);
            list_inv_sub_combinelist_buf = list_inv_sub_combinelist.GetRows((int)enum_inv_sub_combinelist.合併單號, 合併單號);
            if (list_inv_combinelist_buf.Count > 0) sQLControl_inv_combinelist.DeleteExtra(null, list_inv_combinelist_buf);
            if (list_inv_sub_combinelist_buf.Count > 0) sQLControl_inv_sub_combinelist.DeleteExtra(null, list_inv_sub_combinelist_buf);

            returnData.Data = inv_CombinelistClasses;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "inv_delete_by_SN";
            returnData.Result = $"成功刪除資料";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以合併單號取得完整合併單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///    
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_full_inv_by_SN")]
        [HttpPost]
        public string POST_get_full_inv_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "API01");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            if (returnData.Value.StringIsEmpty() == true)
            {
                returnData.Code = -200;
                returnData.Result = "returnData.Value 空白,請輸入合併單號!";
                return returnData.JsonSerializationt();
            }
            string api01_url = sys_serverSettingClasses[0].Server;
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
            string json_get_all_inv = POST_get_all_inv(returnData);
            returnData returnData_get_all_inv = json_get_all_inv.JsonDeserializet<returnData>();
            if (returnData_get_all_inv == null)
            {
                returnData.Code = -200;
                return returnData.JsonSerializationt();
            }
            List<inv_combinelistClass> inv_CombinelistClasses = returnData_get_all_inv.Data.ObjToClass<List<inv_combinelistClass>>();
            if (returnData_get_all_inv == null)
            {
                returnData.Code = -200;
                returnData.Result = $"資料初始化失敗!";
                return returnData.JsonSerializationt();
            }
            List<inv_combinelistClass> inv_CombinelistClasses_buf = (from temp in inv_CombinelistClasses
                                                                     where temp.合併單號 == returnData.Value
                                                                     select temp).ToList();
            if (inv_CombinelistClasses_buf.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無此合併單號! {returnData.Value} ";
                return returnData.JsonSerializationt();
            }
            inv_combinelistClass inv_CombinelistClass = inv_CombinelistClasses_buf[0];
            inv_CombinelistClass.get_all_full_creat("http://127.0.0.1:4433");

            List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
            List<medClass> medClasses_cloud_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_med_cloud = medClasses_cloud.CoverToDictionaryByCode();

            List<inventoryClass.content> contents = new List<inventoryClass.content>();
            List<inventoryClass.content> contents_buf = new List<inventoryClass.content>();

            List<inv_combinelist_stock_Class> _inv_Combinelist_Stock_Classes = inv_combinelistClass.get_stocks_by_SN("http://127.0.0.1:4433", returnData.Value);
            List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes_buf = new List<inv_combinelist_stock_Class>();
            Dictionary<string, List<inv_combinelist_stock_Class>> keyValuePairs_inv_Combinelist_Stock_Classes = _inv_Combinelist_Stock_Classes.CoverToDictionaryByCode();

            List<inv_combinelist_price_Class> _inv_Combinelist_price_Classes = inv_combinelistClass.get_medPrices_by_SN("http://127.0.0.1:4433", returnData.Value);
            List<inv_combinelist_price_Class> inv_Combinelist_price_Classes_buf = new List<inv_combinelist_price_Class>();
            Dictionary<string, List<inv_combinelist_price_Class>> keyValuePairs_inv_Combinelist_price_Classes = _inv_Combinelist_price_Classes.CoverToDictionaryByCode();

            List<inv_combinelist_note_Class> _inv_Combinelist_note_Classes = inv_combinelistClass.get_medNote_by_SN("http://127.0.0.1:4433", returnData.Value);
            List<inv_combinelist_note_Class> inv_Combinelist_note_Classes_buf = new List<inv_combinelist_note_Class>();
            Dictionary<string, List<inv_combinelist_note_Class>> keyValuePairs_inv_Combinelist_note_Classes = _inv_Combinelist_note_Classes.CoverToDictionaryByCode();

            List<inv_combinelist_review_Class> _inv_Combinelist_review_Classes = inv_combinelistClass.get_medReview_by_SN("http://127.0.0.1:4433", returnData.Value);
            List<inv_combinelist_review_Class> inv_Combinelist_review_Classes_buf = new List<inv_combinelist_review_Class>();
            Dictionary<string, List<inv_combinelist_review_Class>> keyValuePairs_inv_Combinelist_review_Classes = _inv_Combinelist_review_Classes.CoverToDictionaryByCode();
            string 藥品碼 = "";
            for (int i = 0; i < inv_CombinelistClass.Records_Ary.Count; i++)
            {
                inventoryClass.creat creat = inv_CombinelistClass.Records_Ary[i].Creat;
                for (int k = 0; k < creat.Contents.Count; k++)
                {

                    藥品碼 = creat.Contents[k].藥品碼;
                    contents_buf = (from temp in contents
                                    where temp.藥品碼 == 藥品碼
                                    select temp).ToList();
                    if (contents_buf.Count == 0)
                    {
                        inventoryClass.content content = new inventoryClass.content();
                        content.藥品碼 = creat.Contents[k].藥品碼;
                        content.料號 = creat.Contents[k].料號;
                        content.廠牌 = creat.Contents[k].廠牌;
                        content.藥品名稱 = creat.Contents[k].藥品名稱;
                        content.中文名稱 = creat.Contents[k].中文名稱;
                        content.盤點量 = creat.Contents[k].盤點量;
                        contents.Add(content);
                    }
                    else
                    {
                        contents_buf[0].盤點量 = (creat.Contents[k].盤點量.StringToInt32() + contents_buf[0].盤點量.StringToInt32()).ToString();
                    }
                }
            }
            for (int i = 0; i < contents.Count; i++)
            {
                inv_Combinelist_Stock_Classes_buf = keyValuePairs_inv_Combinelist_Stock_Classes.SortDictionaryByCode(contents[i].藥品碼);
                inv_Combinelist_price_Classes_buf = keyValuePairs_inv_Combinelist_price_Classes.SortDictionaryByCode(contents[i].藥品碼);
                inv_Combinelist_note_Classes_buf = keyValuePairs_inv_Combinelist_note_Classes.SortDictionaryByCode(contents[i].藥品碼);
                inv_Combinelist_review_Classes_buf = keyValuePairs_inv_Combinelist_review_Classes.SortDictionaryByCode(contents[i].藥品碼);
                medClasses_cloud_buf = keyValuePairs_med_cloud.SortDictionaryByCode(contents[i].藥品碼);
                if (medClasses_cloud_buf.Count > 0)
                {
                    contents[i].藥品名稱 = medClasses_cloud_buf[0].藥品名稱;
                    contents[i].中文名稱 = medClasses_cloud_buf[0].中文名稱;
                    contents[i].廠牌 = medClasses_cloud_buf[0].廠牌;
                    contents[i].料號 = medClasses_cloud_buf[0].料號;
                }


                if (inv_Combinelist_review_Classes_buf.Count > 0)
                {
                    //contents[i].盤點量 = inv_Combinelist_review_Classes_buf[0].數量;
                    inv_CombinelistClass.MedReviews.Add(inv_Combinelist_review_Classes_buf[0]);
                }

                if (inv_Combinelist_Stock_Classes_buf.Count > 0)
                {
                    inv_CombinelistClass.Stocks.Add(inv_Combinelist_Stock_Classes_buf[0]);
                }
                else
                {
                    inv_combinelist_stock_Class inv_Combinelist_Stock_Class = new inv_combinelist_stock_Class();
                    inv_Combinelist_Stock_Class.GUID = Guid.NewGuid().ToString();
                    inv_Combinelist_Stock_Class.藥碼 = contents[i].藥品碼;
                    inv_Combinelist_Stock_Class.藥名 = contents[i].藥品名稱;
                    inv_Combinelist_Stock_Class.數量 = "0";
                    inv_CombinelistClass.Stocks.Add(inv_Combinelist_Stock_Class);
                }
                if (inv_Combinelist_price_Classes_buf.Count > 0)
                {
                    inv_CombinelistClass.MedPrices.Add(inv_Combinelist_price_Classes_buf[0]);
                }
                else
                {
                    inv_combinelist_price_Class inv_Combinelist_Price_Class = new inv_combinelist_price_Class();
                    inv_Combinelist_Price_Class.GUID = Guid.NewGuid().ToString();
                    inv_Combinelist_Price_Class.藥碼 = contents[i].藥品碼;
                    inv_Combinelist_Price_Class.藥名 = contents[i].藥品名稱;
                    inv_Combinelist_Price_Class.單價 = "0";
                    inv_CombinelistClass.MedPrices.Add(inv_Combinelist_Price_Class);

                }

                if (inv_Combinelist_note_Classes_buf.Count > 0)
                {
                    inv_CombinelistClass.MedNotes.Add(inv_Combinelist_note_Classes_buf[0]);
                }
                else
                {
                    inv_combinelist_note_Class inv_Combinelist_note_Class = new inv_combinelist_note_Class();
                    inv_Combinelist_note_Class.GUID = Guid.NewGuid().ToString();
                    inv_Combinelist_note_Class.藥碼 = contents[i].藥品碼;
                    inv_Combinelist_note_Class.藥名 = contents[i].藥品名稱;
                    inv_Combinelist_note_Class.備註 = "-";
                    inv_CombinelistClass.MedNotes.Add(inv_Combinelist_note_Class);

                }
            }

            inv_CombinelistClass.Contents = contents;

            if (inv_CombinelistClass.誤差總金額致能.StringIsEmpty()) inv_CombinelistClass.誤差總金額致能 = true.ToString();
            if (inv_CombinelistClass.誤差百分率致能.StringIsEmpty()) inv_CombinelistClass.誤差百分率致能 = true.ToString();
            if (inv_CombinelistClass.誤差數量致能.StringIsEmpty()) inv_CombinelistClass.誤差數量致能 = true.ToString();


            if (inv_CombinelistClass.誤差總金額上限.StringIsDouble() == false) inv_CombinelistClass.誤差總金額上限 = "500";
            if (inv_CombinelistClass.誤差總金額下限.StringIsDouble() == false) inv_CombinelistClass.誤差總金額下限 = "0";

            if (inv_CombinelistClass.誤差百分率上限.StringIsDouble() == false) inv_CombinelistClass.誤差百分率上限 = "10";
            if (inv_CombinelistClass.誤差百分率下限.StringIsDouble() == false) inv_CombinelistClass.誤差百分率下限 = "0";

            if (inv_CombinelistClass.誤差數量上限.StringIsDouble() == false) inv_CombinelistClass.誤差數量上限 = "100";
            if (inv_CombinelistClass.誤差數量下限.StringIsDouble() == false) inv_CombinelistClass.誤差數量下限 = "0";

            returnData.Data = inv_CombinelistClass;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "get_full_inv_by_SN";
            returnData.Result = $"成功取得盤點單合併完成資料";
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以合併單號新增參考庫存
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///       [inv_combinelist_stock_Class]
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("add_stocks_by_SN")]
        [HttpPost]
        public string POST_add_stocks_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "add_stocks_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }
                List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes = returnData.Data.ObjToClass<List<inv_combinelist_stock_Class>>();
                List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes_buf = new List<inv_combinelist_stock_Class>();
                List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes_add = new List<inv_combinelist_stock_Class>();
                Table table = new Table(new enum_inv_combinelist_stock());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_Stock = new SQLControl(table);

                if (inv_Combinelist_Stock_Classes == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data 傳入資料無效";
                    return returnData.JsonSerializationt();
                }
                if (inv_Combinelist_Stock_Classes.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data 傳入資料無效";
                    return returnData.JsonSerializationt();
                }
                sQLControl_inv_Combinelist_Stock.DeleteByDefult(null, (int)enum_inv_combinelist_stock.合併單號, returnData.Value);
                List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
                medClass medclass = null;

                for (int i = 0; i < inv_Combinelist_Stock_Classes.Count; i++)
                {
                    string code = inv_Combinelist_Stock_Classes[i].藥碼;
                    medclass = medClasses_cloud.SerchByBarcode(code);
                    if (medclass == null) continue;
                    string 料號 = medclass.料號;
                    string 藥碼 = medclass.藥品碼;
                    string 藥名 = medclass.藥品名稱;
                    inv_Combinelist_Stock_Classes_buf = (from temp in inv_Combinelist_Stock_Classes
                                                         where temp.藥碼 == 藥碼 || temp.藥碼 == 料號
                                                         select temp).ToList();
                    inv_combinelist_stock_Class inv_Combinelist_Stock_Class = new inv_combinelist_stock_Class();
                    inv_Combinelist_Stock_Class.GUID = Guid.NewGuid().ToString();
                    inv_Combinelist_Stock_Class.合併單號 = returnData.Value;
                    inv_Combinelist_Stock_Class.藥碼 = 藥碼;
                    inv_Combinelist_Stock_Class.藥名 = 藥名;
                    inv_Combinelist_Stock_Class.數量 = "0";
                    inv_Combinelist_Stock_Class.加入時間 = DateTime.Now.ToDateTimeString_6();
                    for (int k = 0; k < inv_Combinelist_Stock_Classes_buf.Count; k++)
                    {
                        if (inv_Combinelist_Stock_Classes_buf[k].數量.StringToInt32() > 0)
                        {
                            inv_Combinelist_Stock_Class.數量 = (inv_Combinelist_Stock_Class.數量.StringToInt32() + inv_Combinelist_Stock_Classes_buf[k].數量.StringToInt32()).ToString();
                        }
                    }
                    inv_Combinelist_Stock_Classes_add.Add(inv_Combinelist_Stock_Class);
                }
                List<object[]> list_vlaue = inv_Combinelist_Stock_Classes_add.ClassToSQL<inv_combinelist_stock_Class, enum_inv_combinelist_stock>();
                sQLControl_inv_Combinelist_Stock.AddRows(null, list_vlaue);
                returnData.Data = inv_Combinelist_Stock_Classes_add;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"新增參考庫存資料,共<{inv_Combinelist_Stock_Classes_add.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }
        /// <summary>
        /// 以合併單號取得參考庫存
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///      
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_stocks_by_SN")]
        [HttpPost]
        public string POST_get_stocks_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "get_stocks_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }

                Table table = new Table(new enum_inv_combinelist_stock());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_Stock = new SQLControl(table);
                List<object[]> list_value = sQLControl_inv_Combinelist_Stock.GetRowsByDefult(null, (int)enum_inv_combinelist_stock.合併單號, returnData.Value);
                List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes = list_value.SQLToClass<inv_combinelist_stock_Class, enum_inv_combinelist_stock>();
                returnData.Data = inv_Combinelist_Stock_Classes;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得參考庫存資料,共<{inv_Combinelist_Stock_Classes.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }

        /// <summary>
        /// 以合併單號新增藥品單價
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///       [inv_combinelist_price_Class]
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("add_medPrices_by_SN")]
        [HttpPost]
        public string POST_add_medPrices_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "add_medPrices_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }
                List<inv_combinelist_price_Class> inv_Combinelist_Price_Classes = returnData.Data.ObjToClass<List<inv_combinelist_price_Class>>();
                List<inv_combinelist_price_Class> inv_Combinelist_Price_Classes_buf = new List<inv_combinelist_price_Class>();
                List<inv_combinelist_price_Class> inv_Combinelist_Price_Classes_add = new List<inv_combinelist_price_Class>();
                Table table = new Table(new enum_inv_combinelist_price());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_Price = new SQLControl(table);

                if (inv_Combinelist_Price_Classes == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data 傳入資料無效";
                    return returnData.JsonSerializationt();
                }
                if (inv_Combinelist_Price_Classes.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data 傳入資料無效";
                    return returnData.JsonSerializationt();
                }
                sQLControl_inv_Combinelist_Price.DeleteByDefult(null, (int)enum_inv_combinelist_price.合併單號, returnData.Value);
                List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
                medClass medclass = null;

                for (int i = 0; i < inv_Combinelist_Price_Classes.Count; i++)
                {
                    string code = inv_Combinelist_Price_Classes[i].藥碼;
                    medclass = medClasses_cloud.SerchByBarcode(code);
                    if (medclass == null) continue;
                    string 藥碼 = medclass.藥品碼;
                    string 藥名 = medclass.藥品名稱;

                    inv_combinelist_price_Class inv_Combinelist_Price_Class = inv_Combinelist_Price_Classes[i];
                    inv_Combinelist_Price_Class.GUID = Guid.NewGuid().ToString();
                    inv_Combinelist_Price_Class.合併單號 = returnData.Value;
                    inv_Combinelist_Price_Class.藥碼 = 藥碼;
                    inv_Combinelist_Price_Class.藥名 = 藥名;
                    inv_Combinelist_Price_Class.加入時間 = DateTime.Now.ToDateTimeString_6();

                    inv_Combinelist_Price_Classes_add.Add(inv_Combinelist_Price_Class);
                }
                List<object[]> list_vlaue = inv_Combinelist_Price_Classes_add.ClassToSQL<inv_combinelist_price_Class, enum_inv_combinelist_price>();
                sQLControl_inv_Combinelist_Price.AddRows(null, list_vlaue);
                returnData.Data = inv_Combinelist_Price_Classes_add;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"新增藥品單價,共<{inv_Combinelist_Price_Classes_add.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }
        /// <summary>
        /// 以合併單號取得藥品單價
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///      
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_medPrices_by_SN")]
        [HttpPost]
        public string POST_get_medPrices_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "get_medPrices_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }

                Table table = new Table(new enum_inv_combinelist_price());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_Price = new SQLControl(table);
                List<object[]> list_value = sQLControl_inv_Combinelist_Price.GetRowsByDefult(null, (int)enum_inv_combinelist_price.合併單號, returnData.Value);
                List<inv_combinelist_price_Class> inv_Combinelist_Price_Classes = list_value.SQLToClass<inv_combinelist_price_Class, enum_inv_combinelist_price>();
                returnData.Data = inv_Combinelist_Price_Classes;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得藥品單價,共<{inv_Combinelist_Price_Classes.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }

        /// <summary>
        /// 以合併單號新增藥品備註
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///       [inv_combinelist_price_Class]
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("add_medNote_by_SN")]
        [HttpPost]
        public string POST_add_medNote_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "add_medNote_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }
                List<inv_combinelist_note_Class> inv_Combinelist_note_Classes = returnData.Data.ObjToClass<List<inv_combinelist_note_Class>>();
                List<inv_combinelist_note_Class> inv_Combinelist_note_Classes_buf = new List<inv_combinelist_note_Class>();
                List<inv_combinelist_note_Class> inv_Combinelist_note_Classes_add = new List<inv_combinelist_note_Class>();
                Table table = new Table(new enum_inv_combinelist_note());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_note = new SQLControl(table);

                if (inv_Combinelist_note_Classes == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data 傳入資料無效";
                    return returnData.JsonSerializationt();
                }
                if (inv_Combinelist_note_Classes.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data 傳入資料無效";
                    return returnData.JsonSerializationt();
                }
                sQLControl_inv_Combinelist_note.DeleteByDefult(null, (int)enum_inv_combinelist_note.合併單號, returnData.Value);
                List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
                medClass medclass = null;

                for (int i = 0; i < inv_Combinelist_note_Classes.Count; i++)
                {
                    string code = inv_Combinelist_note_Classes[i].藥碼;
                    medclass = medClasses_cloud.SerchByBarcode(code);
                    if (medclass == null) continue;
                    string 藥碼 = medclass.藥品碼;
                    string 藥名 = medclass.藥品名稱;

                    inv_combinelist_note_Class inv_Combinelist_note_Class = inv_Combinelist_note_Classes[i];
                    inv_Combinelist_note_Class.GUID = Guid.NewGuid().ToString();
                    inv_Combinelist_note_Class.合併單號 = returnData.Value;
                    inv_Combinelist_note_Class.藥碼 = 藥碼;
                    inv_Combinelist_note_Class.藥名 = 藥名;
                    inv_Combinelist_note_Class.加入時間 = DateTime.Now.ToDateTimeString_6();

                    inv_Combinelist_note_Classes_add.Add(inv_Combinelist_note_Class);
                }
                List<object[]> list_vlaue = inv_Combinelist_note_Classes_add.ClassToSQL<inv_combinelist_note_Class, enum_inv_combinelist_note>();
                sQLControl_inv_Combinelist_note.AddRows(null, list_vlaue);
                returnData.Data = inv_Combinelist_note_Classes_add;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"新增藥品備註,共<{inv_Combinelist_note_Classes_add.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }
        /// <summary>
        /// 以合併單號取得藥品備註
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///      
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_medNote_by_SN")]
        [HttpPost]
        public string POST_get_medNote_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "get_medNote_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }

                Table table = new Table(new enum_inv_combinelist_note());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_note = new SQLControl(table);
                List<object[]> list_value = sQLControl_inv_Combinelist_note.GetRowsByDefult(null, (int)enum_inv_combinelist_note.合併單號, returnData.Value);
                List<inv_combinelist_note_Class> inv_Combinelist_note_Classes = list_value.SQLToClass<inv_combinelist_note_Class, enum_inv_combinelist_note>();
                returnData.Data = inv_Combinelist_note_Classes;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得藥品備註,共<{inv_Combinelist_note_Classes.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }

        /// <summary>
        /// 以合併單號新增藥品覆盤量
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///       inv_combinelist_price_Class
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("add_medReview_by_SN")]
        [HttpPost]
        public string POST_add_medReview_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "add_medReview_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }
                inv_combinelist_review_Class inv_Combinelist_Review_Class = returnData.Data.ObjToClass<inv_combinelist_review_Class>();
                List<inv_combinelist_review_Class> inv_Combinelist_review_Classes_buf = new List<inv_combinelist_review_Class>();
                List<inv_combinelist_review_Class> inv_Combinelist_review_Classes_add = new List<inv_combinelist_review_Class>();
                Table table = new Table(new enum_inv_combinelist_review());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_review = new SQLControl(table);

                if (inv_Combinelist_Review_Class == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data 傳入資料無效";
                    return returnData.JsonSerializationt();
                }

                sQLControl_inv_Combinelist_review.DeleteByDefult(null, new string[] { enum_inv_combinelist_review.合併單號.GetEnumName(), enum_inv_combinelist_review.藥碼.GetEnumName() }
                , new string[] { returnData.Value, inv_Combinelist_Review_Class.藥碼 });

                if (inv_Combinelist_Review_Class.數量.StringIsEmpty())
                {
                    returnData.Data = inv_Combinelist_Review_Class;
                    returnData.Code = 200;
                    returnData.TimeTaken = myTimer.ToString();
                    returnData.Result = $"刪除資料成功";
                    return returnData.JsonSerializationt(true);
                }

                List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
                medClass medclass = null;

                string code = inv_Combinelist_Review_Class.藥碼;
                medclass = medClasses_cloud.SerchByBarcode(code);
                if (medclass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"線上藥檔找無資料";
                    return returnData.JsonSerializationt();
                }
                string 藥碼 = medclass.藥品碼;
                string 藥名 = medclass.藥品名稱;

                inv_Combinelist_Review_Class.GUID = Guid.NewGuid().ToString();
                inv_Combinelist_Review_Class.合併單號 = returnData.Value;
                inv_Combinelist_Review_Class.藥碼 = 藥碼;
                inv_Combinelist_Review_Class.藥名 = 藥名;
                inv_Combinelist_Review_Class.加入時間 = DateTime.Now.ToDateTimeString_6();

                inv_Combinelist_review_Classes_add.Add(inv_Combinelist_Review_Class);
                List<object[]> list_vlaue = inv_Combinelist_review_Classes_add.ClassToSQL<inv_combinelist_review_Class, enum_inv_combinelist_review>();
                sQLControl_inv_Combinelist_review.AddRows(null, list_vlaue);
                returnData.Data = inv_Combinelist_Review_Class;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"新增藥品覆盤量,共<{inv_Combinelist_review_Classes_add.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }
        /// <summary>
        /// 以合併單號取得藥品覆盤量
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///      
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_medReview_by_SN")]
        [HttpPost]
        public string POST_get_medReview_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "get_medReview_by_SN";

            try
            {
                GET_init(returnData);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Value 空白,請輸入合併單號";
                    return returnData.JsonSerializationt();
                }

                Table table = new Table(new enum_inv_combinelist_review());
                table.DBName = sys_serverSettingClasses[0].DBName;
                table.Server = sys_serverSettingClasses[0].Server;
                table.Port = sys_serverSettingClasses[0].Port;
                table.Username = sys_serverSettingClasses[0].User;
                table.Password = sys_serverSettingClasses[0].Password;
                SQLControl sQLControl_inv_Combinelist_review = new SQLControl(table);
                List<object[]> list_value = sQLControl_inv_Combinelist_review.GetRowsByDefult(null, (int)enum_inv_combinelist_review.合併單號, returnData.Value);
                List<inv_combinelist_review_Class> inv_Combinelist_review_Classes = list_value.SQLToClass<inv_combinelist_review_Class, enum_inv_combinelist_review>();
                returnData.Data = inv_Combinelist_review_Classes;
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得藥品覆盤量,共<{inv_Combinelist_review_Classes.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
            finally
            {

            }

        }

        /// <summary>
        /// 以合併單號取得完整合併單DataTable
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///    
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_full_inv_DataTable_by_SN")]
        [HttpPost]
        public string POST_get_full_inv_DataTable_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);
            returnData.Method = "get_full_inv_DataTable_by_SN";
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "API01");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt(true);
            }
            if (returnData.Value.StringIsEmpty() == true)
            {
                returnData.Code = -200;
                returnData.Result = "returnData.Value 空白,請輸入合併單號!";
                return returnData.JsonSerializationt(true);
            }
            string api01_url = sys_serverSettingClasses[0].Server;
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
            string json_get_all_inv = POST_get_all_inv(returnData);
            returnData returnData_get_all_inv = json_get_all_inv.JsonDeserializet<returnData>();
            if (returnData_get_all_inv == null)
            {
                returnData.Code = -200;
                return returnData.JsonSerializationt(true);
            }
            List<inv_combinelistClass> inv_CombinelistClasses = returnData_get_all_inv.Data.ObjToClass<List<inv_combinelistClass>>();
            if (returnData_get_all_inv == null)
            {
                returnData.Code = -200;
                returnData.Result = $"資料初始化失敗!";
                return returnData.JsonSerializationt(true);
            }
            List<inv_combinelistClass> inv_CombinelistClasses_buf = (from temp in inv_CombinelistClasses
                                                                     where temp.合併單號 == returnData.Value
                                                                     select temp).ToList();
            if (inv_CombinelistClasses_buf.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無此合併單號! {returnData.Value} ";
                return returnData.JsonSerializationt(true);
            }

            inv_combinelistClass inv_CombinelistClass = inv_combinelistClass.get_full_inv_by_SN("http://127.0.0.1:4433", returnData.Value);
            //inv_CombinelistClass.get_all_full_creat("http://127.0.0.1:4433");
            List<inventoryClass.creat> creats = inv_CombinelistClass.Creats;
            List<inventoryClass.content> contents = new List<inventoryClass.content>();
            List<inventoryClass.content> contents_buf = new List<inventoryClass.content>();
            List<System.Data.DataTable> dataTables_creat = new List<System.Data.DataTable>();
            List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
            List<medClass> medClasses_cloud_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_med_cloud = medClasses_cloud.CoverToDictionaryByCode();
            string 藥品碼 = "";
            for (int i = 0; i < creats.Count; i++)
            {
                List<object[]> list_creat_buf = new List<object[]>();
                System.Data.DataTable dataTable_buf = new System.Data.DataTable();
                for (int k = 0; k < creats[i].Contents.Count; k++)
                {
                    if (creats[i].Contents[k].Sub_content.Count == 0) continue;
                    藥品碼 = creats[i].Contents[k].藥品碼;
                    medClasses_cloud_buf = keyValuePairs_med_cloud.SortDictionaryByCode(藥品碼);
                    if (medClasses_cloud_buf.Count > 0)
                    {
                        creats[i].Contents[k].料號 = medClasses_cloud_buf[0].料號;
                        creats[i].Contents[k].藥品名稱 = medClasses_cloud_buf[0].藥品名稱;
                    }

                    object[] value = new object[new enum_盤點定盤_Excel().GetLength()];
                    value[(int)enum_盤點定盤_Excel.藥碼] = creats[i].Contents[k].藥品碼;
                    value[(int)enum_盤點定盤_Excel.料號] = creats[i].Contents[k].料號;
                    value[(int)enum_盤點定盤_Excel.藥名] = creats[i].Contents[k].藥品名稱;
                    value[(int)enum_盤點定盤_Excel.庫存量] = creats[i].Contents[k].理論值;
                    value[(int)enum_盤點定盤_Excel.盤點量] = creats[i].Contents[k].盤點量;



                    list_creat_buf.Add(value);


                    contents_buf = (from temp in contents
                                    where temp.藥品碼 == 藥品碼
                                    select temp).ToList();
                    if (contents_buf.Count == 0)
                    {
                        inventoryClass.content content = creats[i].Contents[k];
                        content.GUID = "";
                        content.Master_GUID = "";
                        content.理論值 = "";
                        content.新增時間 = "";
                        content.盤點單號 = "";
                        content.Sub_content.Clear();
                        contents.Add(content);
                    }
                    else
                    {
                        contents_buf[0].盤點量 = (creats[i].Contents[k].盤點量.StringToInt32() + contents_buf[0].盤點量.StringToInt32()).ToString();
                    }
                }
                dataTable_buf = list_creat_buf.ToDataTable(new enum_盤點定盤_Excel());

                string tableName = $"{i}.{creats[i].盤點名稱}";

                // 移除或替換非法字元
                string safeFileName = Regex.Replace(tableName, @"[\\/:*?""<>|]", "_");

                // 指定為合法的檔案名稱
                dataTable_buf.TableName = safeFileName;

                dataTables_creat.Add(dataTable_buf);
            }



            List<object[]> list_value = new List<object[]>();
            System.Data.DataTable dataTable;
            SheetClass sheetClass;
            Console.WriteLine($"取得creats {myTimer.ToString()}");



            for (int i = 0; i < contents.Count; i++)
            {
                bool flag_覆盤 = false;
                string 藥碼 = contents[i].藥品碼;

                object[] value = new object[new enum_盤點定盤_Excel().GetLength()];
                value[(int)enum_盤點定盤_Excel.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_盤點定盤_Excel.藥碼] = contents[i].藥品碼;
                value[(int)enum_盤點定盤_Excel.料號] = contents[i].料號;
                value[(int)enum_盤點定盤_Excel.藥名] = contents[i].藥品名稱;
                value[(int)enum_盤點定盤_Excel.盤點量] = contents[i].盤點量;
                value[(int)enum_盤點定盤_Excel.單價] = "0";
                value[(int)enum_盤點定盤_Excel.誤差百分率] = "0";
                value[(int)enum_盤點定盤_Excel.消耗量] = "0";
                inv_combinelist_stock_Class inv_Combinelist_Stock_Class = inv_CombinelistClass.GetStockByCode(藥碼);
                inv_combinelist_price_Class inv_Combinelist_Price_Class = inv_CombinelistClass.GetMedPriceByCode(藥碼);
                inv_combinelist_note_Class inv_Combinelist_Note_Class = inv_CombinelistClass.GetMedNoteByCode(藥碼);
                inv_combinelist_review_Class inv_Combinelist_Review_Class = inv_CombinelistClass.GetMedReviewByCode(藥碼);
                if (inv_Combinelist_Stock_Class != null)
                {
                    value[(int)enum_盤點定盤_Excel.庫存量] = inv_Combinelist_Stock_Class.數量;
                }
                if (inv_Combinelist_Note_Class != null)
                {
                    value[(int)enum_盤點定盤_Excel.別名] = inv_Combinelist_Note_Class.備註;
                }

                if (inv_Combinelist_Review_Class != null)
                {
                    value[(int)enum_盤點定盤_Excel.覆盤量] = inv_Combinelist_Review_Class.數量;
                }

                if (inv_Combinelist_Price_Class != null)
                {
                    if (inv_Combinelist_Price_Class.單價.StringIsDouble()) value[(int)enum_盤點定盤_Excel.單價] = inv_Combinelist_Price_Class.單價;
                }
                value[(int)enum_盤點定盤_Excel.庫存金額] = value[(int)enum_盤點定盤_Excel.庫存量].StringToDouble() * value[(int)enum_盤點定盤_Excel.單價].StringToDouble();

                if (value[(int)enum_盤點定盤_Excel.覆盤量].ObjectToString().StringIsEmpty())
                {
                    value[(int)enum_盤點定盤_Excel.結存金額] = value[(int)enum_盤點定盤_Excel.盤點量].StringToDouble() * value[(int)enum_盤點定盤_Excel.單價].StringToDouble();
                    value[(int)enum_盤點定盤_Excel.誤差量] = value[(int)enum_盤點定盤_Excel.盤點量].StringToDouble() - value[(int)enum_盤點定盤_Excel.庫存量].StringToDouble();
                }
                else
                {
                    value[(int)enum_盤點定盤_Excel.結存金額] = value[(int)enum_盤點定盤_Excel.覆盤量].StringToDouble() * value[(int)enum_盤點定盤_Excel.單價].StringToDouble();
                    value[(int)enum_盤點定盤_Excel.誤差量] = value[(int)enum_盤點定盤_Excel.覆盤量].StringToDouble() - value[(int)enum_盤點定盤_Excel.庫存量].StringToDouble();
                }

                value[(int)enum_盤點定盤_Excel.誤差金額] = value[(int)enum_盤點定盤_Excel.誤差量].StringToDouble() * value[(int)enum_盤點定盤_Excel.單價].StringToDouble();

                value[(int)enum_盤點定盤_Excel.庫存金額] = value[(int)enum_盤點定盤_Excel.庫存金額].StringToDouble().ToString("0.00");
                value[(int)enum_盤點定盤_Excel.結存金額] = value[(int)enum_盤點定盤_Excel.結存金額].StringToDouble().ToString("0.00");
                value[(int)enum_盤點定盤_Excel.誤差金額] = value[(int)enum_盤點定盤_Excel.誤差金額].StringToDouble().ToString("0.00");

                if (value[(int)enum_盤點定盤_Excel.消耗量].StringToInt32() > 0)
                {
                    value[(int)enum_盤點定盤_Excel.誤差百分率] = ((value[(int)enum_盤點定盤_Excel.誤差量].StringToDouble() / value[(int)enum_盤點定盤_Excel.消耗量].StringToDouble()) * 100).ToString("0.00");
                }

                if (inv_CombinelistClass.誤差總金額致能.StringToBool())
                {
                    double 上限 = inv_CombinelistClass.誤差總金額上限.StringToDouble();
                    double 下限 = inv_CombinelistClass.誤差總金額下限.StringToDouble();
                    double temp = value[(int)enum_盤點定盤_Excel.誤差金額].StringToDouble();
                    if (temp < 下限 || temp > 上限)
                    {
                        flag_覆盤 = true;
                    }
                }

                if (inv_CombinelistClass.誤差數量致能.StringToBool())
                {
                    double 上限 = inv_CombinelistClass.誤差數量上限.StringToDouble();
                    double 下限 = inv_CombinelistClass.誤差數量下限.StringToDouble();
                    double temp = value[(int)enum_盤點定盤_Excel.誤差量].StringToDouble();
                    if (temp < 下限 || temp > 上限)
                    {
                        flag_覆盤 = true;
                    }
                }
                if (inv_CombinelistClass.誤差百分率致能.StringToBool())
                {
                    double 上限 = inv_CombinelistClass.誤差百分率上限.StringToDouble();
                    double 下限 = inv_CombinelistClass.誤差百分率下限.StringToDouble();
                    double temp = value[(int)enum_盤點定盤_Excel.誤差百分率].StringToDouble();
                    if (temp < 下限 || temp > 上限)
                    {
                        flag_覆盤 = true;
                    }
                }
                if (flag_覆盤) value[(int)enum_盤點定盤_Excel.註記] = "覆盤";
                list_value.Add(value);
            }
            List<System.Data.DataTable> dataTables = new List<System.Data.DataTable>();
            dataTable = list_value.ToDataTable(new enum_盤點定盤_Excel());
            dataTable.TableName = "盤點總表";
            dataTables.Add(dataTable);


            for (int i = 0; i < dataTables_creat.Count; i++)
            {
                dataTables.Add(dataTables_creat[i]);
            }
            if (returnData.ValueAry != null)
            {
                for (int i = 0; i < returnData.ValueAry.Count; i++)
                {
                    foreach (System.Data.DataTable dt in dataTables)
                    {
                        dt.Columns.Remove(returnData.ValueAry[i]);
                    }
                }
            }
      
            returnData.Data = dataTables.JsonSerializeDataTable();
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"成功轉換表單<{dataTables.Count}>張";
            return returnData.JsonSerializationt();
        }
        /// <summary>
        /// 以合併單號取得完整合併單Excel
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 合併單號 <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "I20240103-14",
        ///    "Data": 
        ///    {                 
        ///    
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_full_inv_Excel_by_SN")]
        [HttpPost]
        public async Task<ActionResult> POST_get_full_inv_Excel_by_SN([FromBody] returnData returnData)
        {
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "API01");
            List<System.Data.DataTable> dataTables = inv_combinelistClass.get_full_inv_DataTable_by_SN("http://127.0.0.1:4433", returnData.Value);
            if (dataTables == null)
            {
                return null;
            }

            string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string xls_command = "application/vnd.ms-excel";

            Enum[] enums = new Enum[] {  enum_盤點定盤_Excel.庫存量, enum_盤點定盤_Excel .盤點量 ,enum_盤點定盤_Excel .單價 ,enum_盤點定盤_Excel .庫存金額 ,enum_盤點定盤_Excel .消耗量 ,
                enum_盤點定盤_Excel.結存金額, enum_盤點定盤_Excel .誤差量 ,enum_盤點定盤_Excel.誤差金額,enum_盤點定盤_Excel.覆盤量 };
            byte[] excelData = ExcelClass.NPOI_GetBytes(dataTables, Excel_Type.xlsx, enums);

            Stream stream = new MemoryStream(excelData);
            return await Task.FromResult(File(stream, xlsx_command, $"{returnData.Value}_InventorySummary.xlsx"));
        }
        /// <summary>
        /// 以單號取得(盤點單/消耗單)Excel
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 單號,(盤點單/消耗單) <br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///  {
        ///    "Value" : "20240103-1,盤點單",
        ///    "Data": 
        ///    {                 
        ///    
        ///    }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為合併單結構</returns>
        [Route("get_record_Excel_by_SN")]
        [HttpPost]
        public async Task<ActionResult> POST_get_record_Excel_by_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "API01");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return null;
            }
            if (returnData.Value.StringIsEmpty() == true)
            {
                returnData.Code = -200;
                returnData.Result = "returnData.Value 空白,請輸入單號!";
                return null;
            }
            string[] str_ary = returnData.Value.Split(",");
            if (str_ary.Length != 2)
            {
                return null;
            }
            string 單號 = str_ary[0];
            string 類別 = str_ary[1];
            string api01_url = sys_serverSettingClasses[0].Server;
            string Server = sys_serverSettingClasses[0].Server;
            string DB = sys_serverSettingClasses[0].DBName;
            string UserName = sys_serverSettingClasses[0].User;
            string Password = sys_serverSettingClasses[0].Password;
            uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

            string url = $"{api01_url}/api/inventory/creat_get_by_IC_SN";
            returnData returnData_post_in = new returnData();
            returnData_post_in.Value = 單號;
            string json_post_out = Basic.Net.WEBApiPostJson(url, returnData_post_in.JsonSerializationt());
            returnData returnData_post_out = json_post_out.JsonDeserializet<returnData>();
            List<inventoryClass.creat> creats_buf = returnData_post_out.Data.ObjToClass<List<inventoryClass.creat>>();
            if (creats_buf.Count == 0)
            {
                return null;
            }


            List<inventoryClass.content> contents = creats_buf[0].Contents;
            List<object[]> list_value = new List<object[]>();
            System.Data.DataTable dataTable;
            SheetClass sheetClass;
            Console.WriteLine($"取得creats {myTimer.ToString()}");

            for (int i = 0; i < contents.Count; i++)
            {

                object[] value = new object[new enum_盤點定盤_Excel().GetLength()];
                value[(int)enum_盤點定盤_Excel.藥碼] = contents[i].藥品碼;
                value[(int)enum_盤點定盤_Excel.料號] = contents[i].料號;
                value[(int)enum_盤點定盤_Excel.藥名] = contents[i].藥品名稱;
                value[(int)enum_盤點定盤_Excel.庫存量] = contents[i].理論值;
                value[(int)enum_盤點定盤_Excel.盤點量] = contents[i].盤點量;
                list_value.Add(value);

            }
            dataTable = list_value.ToDataTable(new enum_盤點定盤_Excel());
            sheetClass = dataTable.NPOI_GetSheetClass(new int[] {(int)enum_盤點定盤_Excel.庫存量, (int)enum_盤點定盤_Excel.盤點量, (int)enum_盤點定盤_Excel.單價
            , (int)enum_盤點定盤_Excel.庫存金額, (int)enum_盤點定盤_Excel.消耗量
            ,(int)enum_盤點定盤_Excel.結存金額 , (int)enum_盤點定盤_Excel.誤差量 , (int)enum_盤點定盤_Excel.誤差金額 });
            sheetClass.Name = "盤點總表";


            Console.WriteLine($"NewCell_Webapi_Buffer_Caculate {myTimer.ToString()}");

            string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string xls_command = "application/vnd.ms-excel";

            byte[] excelData = sheetClass.NPOI_GetBytes(Excel_Type.xls);
            Stream stream = new MemoryStream(excelData);
            return await Task.FromResult(File(stream, xlsx_command, $"{單號}_盤點單.xlsx"));
        }

        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {

            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_inv_combinelist()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_inv_sub_combinelist()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_inv_combinelist_stock()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_inv_combinelist_consumption()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_inv_combinelist_price()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_inv_combinelist_note()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_inv_combinelist_review()));
            return tables.JsonSerializationt(true);
        }

    }
}
