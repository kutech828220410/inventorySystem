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
using MyOffice;
using NPOI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;
using System.Data;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class nursingStation : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        ///初始化dbvm.nursingStation資料庫
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[""]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "nursingStation物件", typeof(nursingStationClass))]
        public string init([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "init";
            returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);
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
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得全部護理站列表
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
        [Route("get_all")]
        [HttpPost]
        public string get_all([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_all";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_nursingStation().GetEnumDescription();
                SQLControl sQLControl_nursingStation = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_nursingStation = sQLControl_nursingStation.GetAllRows(null);

                List<nursingStationClass> nursingStationClasses = list_nursingStation.SQLToClass<nursingStationClass, enum_nursingStation>();

                nursingStationClasses = nursingStationClasses.SortByOrder();
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = nursingStationClasses;
                returnData.Result = $"取得護理站列表成功,共<{nursingStationClasses.Count}>筆資料";
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
        /// 新增或修改護理站
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [nursingStation陣列]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                List<nursingStationClass> nursingStationClasses = returnData.Data.ObjToClass<List<nursingStationClass>>();
                if (nursingStationClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_nursingStation().GetEnumDescription();
                SQLControl sQLControl_nursingStation = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_nursingStation = sQLControl_nursingStation.GetAllRows(null);
                List<object[]> list_nursingStation_buf = new List<object[]>();
                List<object[]> list_nursingStation_add = new List<object[]>();
                List<object[]> list_nursingStation_replace = new List<object[]>();
                for (int i = 0; i < nursingStationClasses.Count; i++)
                {
                    string Code = nursingStationClasses[i].代碼;
                    list_nursingStation_buf = list_nursingStation.GetRows((int)enum_nursingStation.代碼, Code);
                    if (list_nursingStation_buf.Count > 0)
                    {
                        object[] value = new object[new enum_nursingStation().GetLength()];
                        nursingStationClasses[i].GUID = list_nursingStation_buf[0][(int)enum_nursingStation.GUID].ObjectToString();
                        value = nursingStationClasses[i].ClassToSQL<nursingStationClass, enum_nursingStation>();
                        list_nursingStation_replace.Add(value);
                    }
                    else
                    {
                        object[] value = new object[new enum_nursingStation().GetLength()];
                        nursingStationClasses[i].GUID = Guid.NewGuid().ToString();
                        value = nursingStationClasses[i].ClassToSQL<nursingStationClass, enum_nursingStation>();
                        list_nursingStation_add.Add(value);
                    }
                }
                sQLControl_nursingStation.AddRows(null, list_nursingStation_add);
                sQLControl_nursingStation.UpdateByDefulteExtra(null, list_nursingStation_replace);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = nursingStationClasses;
                returnData.Result = $"新增及修改護理站列表成功,共新增<{list_nursingStation_add.Count}>筆資料,共修改<{list_nursingStation_replace.Count}>筆";
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
        /// 以代碼刪除護理站
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "ValueAry":["code1,code2,code3"]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("delete_by_codes")]
        [HttpPost]
        public string delete_by_codes([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "delete_by_codes";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[codes]";
                    return returnData.JsonSerializationt(true);
                }
                string[] codes = returnData.ValueAry[0].Split(",");
                if (codes.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[codes]";
                    return returnData.JsonSerializationt(true);
                }
                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_nursingStation().GetEnumDescription();
                SQLControl sQLControl_nursingStation = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string sqlList = string.Join(", ", codes.Select(code => $"'{code}'"));
                string command = $"select * from {DB}.{TableName} where UPPER({enum_nursingStation.代碼.GetEnumName()}) in ({sqlList});";
                DataTable dataTable = sQLControl_nursingStation.WtrteCommandAndExecuteReader(command);

                list_value = dataTable.DataTableToRowList();
                sQLControl_nursingStation.DeleteExtra(null, list_value);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"刪除護理站成功,共刪除<{list_value.Count}>筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
            string[] temp = new enum_nursingStation().GetEnumNames();
            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_nursingStation());
            return table.JsonSerializationt(true);
        }
    }
}
