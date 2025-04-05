using Microsoft.AspNetCore.Mvc;
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
using HIS_DB_Lib;
using H_Pannel_lib;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using MyOffice;
using OfficeOpenXml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_藥品資料
{

    [Route("api/[controller]")]
    [ApiController]
    public class medList : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     "TableName":"medList_selfControl or medList_public"
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(medListClass))]
        [HttpPost]
        public string POST_init([FromBody] returnData returnData)
        {
            try
            {
                if (returnData.TableName == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"請傳入TableName";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(returnData);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// 取得藥品清單內容
        /// </summary>
        /// <remarks>
        /// {
        ///     "TableName":"medList_selfControl or medList_public"
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[medGroupClasses]</returns>
        [Route("get_all")]
        [HttpPost]
        public string get_all([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }

                List<medListClass> medListClasses = Function_Get_medListClass(returnData, sys_serverSettingClasses[0]);

                returnData.Code = 200;
                returnData.Data = medListClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "get_all";
                returnData.Result = $"取得藥品清單資料成功!";

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
        /// 新增指定群組內多個藥品
        /// </summary>
        /// <remarks>
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "ValueAry":["Code1;Code2"]
        ///     "Value": "",
        ///     "TableName": "medList_selfControl or medList_public",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add_meds_in_list")]
        [HttpPost]
        public string add_meds_in_list([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                if (returnData.TableName == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"請傳入TableName";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ValueAry輸入資料空白";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ValueAry應該為[\"code1;code2\"]";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                string tableName = returnData.TableName;
                SQLControl sQLControl = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                string[] codes = returnData.ValueAry[0].Split(";");

                List<object[]> list_medList = sQLControl.GetAllRows(null);

                List<object[]> list_medList_buf = new List<object[]>();
                List<object[]> list_medList_add = new List<object[]>();
                for (int i = 0; i < codes.Length; i++)
                {
                    string 藥品碼 = codes[i];
                    list_medList_buf = list_medList.GetRows((int)enum_medList_public.藥品碼, 藥品碼);
                    if (list_medList_buf.Count == 0)
                    {
                        object[] value = new object[new enum_medList_public().GetLength()];
                        value[(int)enum_medList_public.GUID] = Guid.NewGuid().ToString();
                        value[(int)enum_medList_public.藥品碼] = 藥品碼;
                        list_medList_add.Add(value);
                    }
                }
                if (list_medList_add.Count > 0) sQLControl.AddRows(null, list_medList_add);
                returnData returnData_get_all = medListClass.get_all(API, tableName);
                if (returnData_get_all == null || returnData_get_all.Code != 200) return returnData_get_all.JsonSerializationt(true);
                List<medListClass> medListClasses = returnData_get_all.Data.ObjToClass<List<medListClass>>();

                returnData.Code = 200;
                returnData.Data = medListClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "add_meds_in_list";
                returnData.Result = $"寫入藥品清單資料成功!共新增<{list_medList_add.Count}>筆藥品!";
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
        /// 刪除指定群組內多個藥品
        /// </summary>
        /// <remarks>
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "GUID": "ef2f3e3e-a58d-4ac0-8878-d29aa1ccebdf",
        ///         "NAME": "TEST123",
        ///         "MedClasses": 
        ///          [
        ///             {
        ///                 "CODE": "IDIP1"
        ///             },
        ///             {
        ///                 "CODE": "OOXY"
        ///             }
        ///          ]
        ///     },
        ///     "Value": "",
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("delete_meds_in_list")]
        [HttpPost]
        public string delete_meds_in_list([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();

                if (returnData.TableName == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"請傳入TableName";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ValueAry輸入資料空白";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ValueAry應該為[\"code1;code2\"]";
                    return returnData.JsonSerializationt();
                }

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                string tableName = returnData.TableName;
                SQLControl sQLControl = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                string[] codes = returnData.ValueAry[0].Split(";");

                List<object[]> list_medList = sQLControl.GetAllRows(null);

                List<object[]> list_medList_buf = new List<object[]>();
                List<object[]> list_medList_delete = new List<object[]>();
                for (int i = 0; i < codes.Length; i++)
                {
                    string 藥品碼 = codes[i];
                    list_medList_buf = list_medList.GetRows((int)enum_medList_public.藥品碼, 藥品碼);
                    if (list_medList_buf.Count >= 0) list_medList_delete.Add(list_medList_buf[0]);
                }
                if (list_medList_delete.Count > 0) sQLControl.DeleteExtra(null, list_medList_delete);


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "delete_meds_in_group";
                returnData.Result = $"藥品資料刪除指定清單成功!共刪除<{list_medList_delete.Count}>筆藥品!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        private List<medListClass> Function_Get_medListClass(returnData returnData, sys_serverSettingClass sys_serverSettingClass)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            CheckCreatTable(returnData);
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
            string table = returnData.TableName;
            SQLControl sQLControl = new SQLControl(Server, DB, table, UserName, Password, Port, SSLMode);
            string API = GetServerAPI("Main", "網頁", "API01");

            List<medClass> medClasses = medClass.get_med_cloud("http://127.0.0.1:4433");
            List<medClass> medClasses_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_medClass = medClasses.CoverToDictionaryByCode();
            List<object[]> list_medList = sQLControl.GetAllRows(null);
            List<object[]> list_med_sub_group_buf = new List<object[]>();

            List<medListClass> medListClasses = list_medList.SQLToClass<medListClass, enum_medList_public>();
            for (int i = 0; i < medListClasses.Count; i++)
            {
                medClasses_buf = keyValuePairs_medClass.SortDictionaryByCode(medListClasses[i].藥品碼);
                if (medClasses_buf.Count > 0)
                {
                    medListClasses[i].雲端藥檔 = medClasses_buf[0];
                }
            }
            return medListClasses;
        }
        private string CheckCreatTable(returnData returnData)
        {
            string TableName = returnData.TableName;
            Table table = new Table(TableName);
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
            if (sys_serverSettingClasses.Count == 0)
            {
                return $"找無Server資料!";
            }
            if (TableName == "medList_public")
            {
                table = MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medList_public());
            }
            if (TableName == "medList_selfControl")
            {
                table = MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medList_selfControl());
            }
            return table.JsonSerializationt(true);
        }
        private string GetServerAPI(string Name, string Type, string Content)
        {
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return sys_serverSettingClass.Server;
        }
    }
}
