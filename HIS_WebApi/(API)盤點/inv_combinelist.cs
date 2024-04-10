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


            SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_inv_sub_combinelist = new SQLControl(Server, DB, "inv_sub_combinelist", UserName, Password, Port, SSLMode);

            List<object[]> list_inv_combinelist = sQLControl_inv_combinelist.GetAllRows(null);
            List<object[]> list_inv_combinelist_buf = new List<object[]>();

            list_inv_combinelist_buf = list_inv_combinelist.GetRowsInDate((int)enum_合併總單.建表時間, DateTime.Now);


            string 單號 = "";
            int index = 0;
            while (true)
            {
                單號 = $"I{DateTime.Now.ToDateTinyString()}-{index}";
                index++;
                list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_合併總單.合併單號, 單號);
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
                if(returnData_buf.Code != 200)
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
            list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_合併總單.合併單號, inv_CombinelistClass.合併單號);
            list_inv_sub_combinelist_buf = list_inv_sub_combinelist.GetRows((int)enum_合併單明細.合併單號, inv_CombinelistClass.合併單號);

            if (list_inv_combinelist_buf.Count > 0) sQLControl_inv_combinelist.DeleteExtra(null, list_inv_combinelist_buf);
            if (list_inv_sub_combinelist_buf.Count > 0) sQLControl_inv_sub_combinelist.DeleteExtra(null, list_inv_sub_combinelist_buf);


            inv_CombinelistClass.GUID = Guid.NewGuid().ToString();
            inv_CombinelistClass.建表時間 = DateTime.Now.ToDateTimeString();


            List<object[]> list_inv_Combinelist_add = new List<object[]>();
            List<object[]> list_inv_sub_combinelist_add = new List<object[]>();

            object[] value;
            value = new object[new enum_合併總單().GetLength()];
            value[(int)enum_合併總單.GUID] = inv_CombinelistClass.GUID;
            value[(int)enum_合併總單.合併單號] = inv_CombinelistClass.合併單號;
            value[(int)enum_合併總單.合併單名稱] = inv_CombinelistClass.合併單名稱;
            value[(int)enum_合併總單.建表人] = inv_CombinelistClass.建表人;
            value[(int)enum_合併總單.建表時間] = inv_CombinelistClass.建表時間;
            value[(int)enum_合併總單.備註] = inv_CombinelistClass.備註;
            list_inv_Combinelist_add.Add(value);

            for (int i = 0; i < inv_CombinelistClass.Records_Ary.Count; i++)
            {
                value = new object[new enum_合併單明細().GetLength()];
                value[(int)enum_合併單明細.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_合併單明細.Master_GUID] = inv_CombinelistClass.GUID;
                value[(int)enum_合併單明細.合併單號] = inv_CombinelistClass.合併單號;
                value[(int)enum_合併單明細.單號] = inv_CombinelistClass.Records_Ary[i].單號;
                value[(int)enum_合併單明細.類型] = inv_CombinelistClass.Records_Ary[i].類型;
                value[(int)enum_合併單明細.新增時間] = DateTime.Now.ToDateTimeString();
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
                合併單號 = list_inv_combinelist[i][(int)enum_合併總單.合併單號].ObjectToString();
                list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_合併總單.合併單號, 合併單號);
                list_inv_sub_combinelist_buf = list_inv_sub_combinelist.GetRows((int)enum_合併單明細.合併單號, 合併單號);
                inv_combinelistClass inv_CombinelistClass = list_inv_combinelist_buf[0].SQLToClass<inv_combinelistClass, enum_合併總單>();
                List<inv_records_Class> inv_Sub_CombinelistClasses = list_inv_sub_combinelist_buf.SQLToClass<inv_records_Class, enum_合併單明細>();
                List<inv_records_Class> inv_Sub_CombinelistClasses_buf = new List<inv_records_Class>();
                for (int k = 0; k < inv_Sub_CombinelistClasses.Count; k++)
                {
                    string 單號 = inv_Sub_CombinelistClasses[k].單號;
                    string 類型 = inv_Sub_CombinelistClasses[k].類型;
                    if(類型 == "盤點單")
                    {
                        list_inventory_creat_buf = list_inventory_creat.GetRows((int)enum_盤點單號.盤點單號, 單號);
                        if(list_inventory_creat_buf.Count > 0)
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

            SQLControl sQLControl_inventory_creat = new SQLControl(Server, DB, "inventory_creat", UserName, Password, Port, SSLMode);


            List<object[]> list_inventory_creat = sQLControl_inventory_creat.GetAllRows(null);

            List<inv_records_Class> inv_Records_Classes = new List<inv_records_Class>();
            for(int i = 0; i < list_inventory_creat.Count; i++)
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

            list_inv_combinelist_buf = list_inv_combinelist.GetRows((int)enum_合併總單.合併單號, 合併單號);
            list_inv_sub_combinelist_buf = list_inv_sub_combinelist.GetRows((int)enum_合併單明細.合併單號, 合併單號);
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

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "API01");
            if (serverSettingClasses.Count == 0)
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
            string api01_url = serverSettingClasses[0].Server;
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
            string json_get_all_inv = POST_get_all_inv(returnData);
            returnData returnData_get_all_inv = json_get_all_inv.JsonDeserializet<returnData>();
            if(returnData_get_all_inv == null)
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
            if(inv_CombinelistClasses_buf.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無此合併單號! {returnData.Value} ";
                return returnData.JsonSerializationt();
            }
            inv_combinelistClass inv_CombinelistClass = inv_CombinelistClasses_buf[0];
            List<inventoryClass.creat> creats = new List<inventoryClass.creat>();
            for (int i = 0; i < inv_CombinelistClass.Records_Ary.Count; i++)
            {
                string 單號 = inv_CombinelistClass.Records_Ary[i].單號;
                string 類型 = inv_CombinelistClass.Records_Ary[i].類型;
                if (類型 == "盤點單")
                {
                    string url = $"{api01_url}/api/inventory/creat_get_by_IC_SN";
                    returnData returnData_post_in = new returnData();
                    returnData_post_in.Value = 單號;
                    string json_post_out = Basic.Net.WEBApiPostJson(url, returnData_post_in.JsonSerializationt());
                    returnData returnData_post_out = json_post_out.JsonDeserializet<returnData>();
                    List<inventoryClass.creat> creats_buf = returnData_post_out.Data.ObjToClass<List<inventoryClass.creat>>();
                    if (creats_buf.Count > 0)
                    {
                        creats.Add(creats_buf[0]);
                    }
                }
            }
            List<inventoryClass.content> contents = new List<inventoryClass.content>();
            List<inventoryClass.content> contents_buf = new List<inventoryClass.content>();
            string 藥品碼 = "";
            for (int i = 0; i < creats.Count; i++)
            {
                for (int k = 0; k < creats[i].Contents.Count; k++)
                {
               
                    藥品碼 = creats[i].Contents[k].藥品碼;
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
            }
            returnData.Data = contents;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Method = "get_full_inv_by_SN";
            returnData.Result = $"成功取得盤點單合併完成資料";
            return returnData.JsonSerializationt(true);
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
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            GET_init(returnData);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "API01");
            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return null;
            }
            if (returnData.Value.StringIsEmpty() == true)
            {
                returnData.Code = -200;
                returnData.Result = "returnData.Value 空白,請輸入合併單號!";
                return null;
            }
            string api01_url = serverSettingClasses[0].Server;
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
            string json_get_all_inv = POST_get_all_inv(returnData);
            returnData returnData_get_all_inv = json_get_all_inv.JsonDeserializet<returnData>();
            if (returnData_get_all_inv == null)
            {
                returnData.Code = -200;
                return null;
            }
            List<inv_combinelistClass> inv_CombinelistClasses = returnData_get_all_inv.Data.ObjToClass<List<inv_combinelistClass>>();
            if (returnData_get_all_inv == null)
            {
                returnData.Code = -200;
                returnData.Result = $"資料初始化失敗!";
                return null;
            }
            List<inv_combinelistClass> inv_CombinelistClasses_buf = (from temp in inv_CombinelistClasses
                                                                     where temp.合併單號 == returnData.Value
                                                                     select temp).ToList();
            if (inv_CombinelistClasses_buf.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無此合併單號! {returnData.Value} ";
                return null;
            }
          
            inv_combinelistClass inv_CombinelistClass = inv_CombinelistClasses_buf[0];
            List<inventoryClass.creat> creats = new List<inventoryClass.creat>();
            for (int i = 0; i < inv_CombinelistClass.Records_Ary.Count; i++)
            {
                string 單號 = inv_CombinelistClass.Records_Ary[i].單號;
                string 類型 = inv_CombinelistClass.Records_Ary[i].類型;
                if (類型 == "盤點單")
                {
                    string url = $"{api01_url}/api/inventory/creat_get_by_IC_SN";
                    returnData returnData_post_in = new returnData();
                    returnData_post_in.Value = 單號;
                    string json_post_out = Basic.Net.WEBApiPostJson(url, returnData_post_in.JsonSerializationt());
                    returnData returnData_post_out = json_post_out.JsonDeserializet<returnData>();
                    List<inventoryClass.creat> creats_buf = returnData_post_out.Data.ObjToClass<List<inventoryClass.creat>>();
                    if (creats_buf.Count > 0)
                    {
                        creats.Add(creats_buf[0]);
                    }
                }
            }
            List<inventoryClass.content> contents = new List<inventoryClass.content>();
            List<inventoryClass.content> contents_buf = new List<inventoryClass.content>();
            List<System.Data.DataTable> dataTables_creat = new List<System.Data.DataTable>();
            string 藥品碼 = "";
            for (int i = 0; i < creats.Count; i++)
            {
                List<object[]> list_creat_buf = new List<object[]>();
                System.Data.DataTable dataTable_buf = new System.Data.DataTable();
                for (int k = 0; k < creats[i].Contents.Count; k++)
                {
                    object[] value = new object[new enum_盤點定盤_Excel().GetLength()];
                    value[(int)enum_盤點定盤_Excel.藥碼] = creats[i].Contents[k].藥品碼;
                    value[(int)enum_盤點定盤_Excel.料號] = creats[i].Contents[k].料號;
                    value[(int)enum_盤點定盤_Excel.藥名] = creats[i].Contents[k].藥品名稱;
                    value[(int)enum_盤點定盤_Excel.單位] = creats[i].Contents[k].包裝單位;
                    value[(int)enum_盤點定盤_Excel.庫存量] = creats[i].Contents[k].理論值;
                    value[(int)enum_盤點定盤_Excel.盤點量] = creats[i].Contents[k].盤點量;
                    list_creat_buf.Add(value);

                    藥品碼 = creats[i].Contents[k].藥品碼;
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
                dataTable_buf.TableName = $"{creats[i].盤點名稱}";
                dataTables_creat.Add(dataTable_buf);
            }


   
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
                value[(int)enum_盤點定盤_Excel.單位] = contents[i].包裝單位;
                value[(int)enum_盤點定盤_Excel.庫存量] = contents[i].理論值;
                value[(int)enum_盤點定盤_Excel.盤點量] = contents[i].盤點量;
                list_value.Add(value);

            }
            List<System.Data.DataTable> dataTables = new List<System.Data.DataTable>();
            dataTable = list_value.ToDataTable(new enum_盤點定盤_Excel());
            dataTable.TableName = inv_CombinelistClass.合併單名稱;
            dataTables.Add(dataTable);


            for (int i = 0; i < dataTables_creat.Count; i++)
            {
                dataTables.Add(dataTables_creat[i]);
            }
            if(returnData.ValueAry != null)
            {
                for (int i = 0; i < returnData.ValueAry.Count; i++)
                {
                    foreach (System.Data.DataTable dt in dataTables)
                    {
                        dt.Columns.Remove(returnData.ValueAry[i]);
                    }
                }
            }
  

            Console.WriteLine($"NewCell_Webapi_Buffer_Caculate {myTimer.ToString()}");

            string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string xls_command = "application/vnd.ms-excel";

            List<int> int_col_ary = new List<int>();
            if (dataTables.Count != 0)
            {
                int index = 0;
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.庫存量.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.盤點量.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.單價.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.庫存差異量.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.庫存金額.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.消耗量.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.異動後結存量.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.結存金額.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.誤差量.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
                index = dataTables[0].GetColumnIndex(enum_盤點定盤_Excel.誤差金額.GetEnumName());
                if (index > 0) int_col_ary.Add(index);
          
            }
            byte[] excelData =   ExcelClass.NPOI_GetBytes(dataTables, Excel_Type.xlsx, int_col_ary.ToArray());
           
            Stream stream = new MemoryStream(excelData);
            return await Task.FromResult(File(stream, xlsx_command, $"{returnData.Value}_合併總表.xlsx"));
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

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "API01");
            if (serverSettingClasses.Count == 0)
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
            if(str_ary.Length != 2)
            {
                return null;
            }
            string 單號 = str_ary[0];
            string 類別 = str_ary[1];
            string api01_url = serverSettingClasses[0].Server;
            string Server = serverSettingClasses[0].Server;
            string DB = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

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
                value[(int)enum_盤點定盤_Excel.單位] = contents[i].包裝單位;
                value[(int)enum_盤點定盤_Excel.庫存量] = contents[i].理論值;
                value[(int)enum_盤點定盤_Excel.盤點量] = contents[i].盤點量;
                list_value.Add(value);

            }
            dataTable = list_value.ToDataTable(new enum_盤點定盤_Excel());
            sheetClass = dataTable.NPOI_GetSheetClass(new int[] {(int)enum_盤點定盤_Excel.庫存量, (int)enum_盤點定盤_Excel.盤點量, (int)enum_盤點定盤_Excel.單價
            ,(int)enum_盤點定盤_Excel.庫存差異量, (int)enum_盤點定盤_Excel.庫存金額, (int)enum_盤點定盤_Excel.消耗量, (int)enum_盤點定盤_Excel.異動後結存量
            ,(int)enum_盤點定盤_Excel.結存金額 , (int)enum_盤點定盤_Excel.誤差量 , (int)enum_盤點定盤_Excel.誤差金額 });
            sheetClass.Name = "盤點總表";


            Console.WriteLine($"NewCell_Webapi_Buffer_Caculate {myTimer.ToString()}");

            string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string xls_command = "application/vnd.ms-excel";

            byte[] excelData = sheetClass.NPOI_GetBytes(Excel_Type.xls);
            Stream stream = new MemoryStream(excelData);
            return await Task.FromResult(File(stream, xlsx_command, $"{單號}_盤點單.xls"));
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {


            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_合併總單()));
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_合併單明細()));
            return tables.JsonSerializationt(true);
        }
        //private string CheckCreatTable(ServerSettingClass serverSettingClass)
        //{

        //    string Server = serverSettingClass.Server;
        //    string DB = serverSettingClass.DBName;
        //    string UserName = serverSettingClass.User;
        //    string Password = serverSettingClass.Password;
        //    uint Port = (uint)serverSettingClass.Port.StringToInt32();

        //    SQLControl sQLControl_inv_combinelist = new SQLControl(Server, DB, "inv_combinelist", UserName, Password, Port, SSLMode);
        //    SQLControl sQLControl_inv_sub_combinelist = new SQLControl(Server, DB, "inv_sub_combinelist", UserName, Password, Port, SSLMode);

        //    List<Table> tables = new List<Table>();
        //    Table table_inv_combinelist;        
        //    table_inv_combinelist = new Table("inv_combinelist");
        //    table_inv_combinelist.Server = Server;
        //    table_inv_combinelist.DBName = DB;
        //    table_inv_combinelist.Username = UserName;
        //    table_inv_combinelist.Password = Password;
        //    table_inv_combinelist.Port = Port.ToString();
        //    table_inv_combinelist.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
        //    table_inv_combinelist.AddColumnList("合併單名稱", Table.StringType.VARCHAR, 200, Table.IndexType.None);
        //    table_inv_combinelist.AddColumnList("合併單號", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
        //    table_inv_combinelist.AddColumnList("建表人", Table.StringType.VARCHAR, 30, Table.IndexType.None);
        //    table_inv_combinelist.AddColumnList("建表時間", Table.DateType.DATETIME, 50, Table.IndexType.INDEX);
        //    table_inv_combinelist.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);
        //    if (!sQLControl_inv_combinelist.IsTableCreat()) sQLControl_inv_combinelist.CreatTable(table_inv_combinelist);
        //    else sQLControl_inv_combinelist.CheckAllColumnName(table_inv_combinelist, true);
        //    tables.Add(table_inv_combinelist);

        //    Table table_inv_sub_combinelist;
        //    table_inv_sub_combinelist = new Table("inv_sub_combinelist");
        //    table_inv_sub_combinelist.Server = Server;
        //    table_inv_sub_combinelist.DBName = DB;
        //    table_inv_sub_combinelist.Username = UserName;
        //    table_inv_sub_combinelist.Password = Password;
        //    table_inv_sub_combinelist.Port = Port.ToString();
        //    table_inv_sub_combinelist.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
        //    table_inv_sub_combinelist.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
        //    table_inv_sub_combinelist.AddColumnList("合併單號", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
        //    table_inv_sub_combinelist.AddColumnList("單號", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
        //    table_inv_sub_combinelist.AddColumnList("類型", Table.StringType.VARCHAR, 50, Table.IndexType.None);    
        //    table_inv_sub_combinelist.AddColumnList("新增時間", Table.DateType.DATETIME, 30, Table.IndexType.None);
        //    table_inv_sub_combinelist.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);
        //    if (!sQLControl_inv_sub_combinelist.IsTableCreat()) sQLControl_inv_sub_combinelist.CreatTable(table_inv_sub_combinelist);
        //    else sQLControl_inv_sub_combinelist.CheckAllColumnName(table_inv_sub_combinelist, true);
        //    tables.Add(table_inv_sub_combinelist);

        //    return tables.JsonSerializationt(true);

        //}
    }
}
