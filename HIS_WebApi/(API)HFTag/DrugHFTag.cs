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
    public class DrugHFTag : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        ///初始化dbvm.DrugHFTag資料庫
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
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "DrugHFTag物件", typeof(DrugHFTagClass))]
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
        /// 新增標籤資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [DrugHFTagClass陣列]
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
                List<DrugHFTagClass> drugHFTagClasses = returnData.Data.ObjToClass<List<DrugHFTagClass>>();
                if (drugHFTagClasses == null)
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
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_drugHFTag_buf = new List<object[]>();
                List<object[]> list_drugHFTag_add = new List<object[]>();
                List<object[]> list_drugHFTag_replace = new List<object[]>();
                for (int i = 0; i < drugHFTagClasses.Count; i++)
                {
                    DrugHFTagClass drugHFTagClass = drugHFTagClasses[i];
                    if (drugHFTagClass.TagSN.StringIsEmpty()) continue;
                    if(drugHFTagClass.效期.StringIsEmpty()) continue;
                    drugHFTagClass.GUID = Guid.NewGuid().ToString();
                    drugHFTagClass.更新時間 = DateTime.Now.ToDateTimeString_6();
                    list_drugHFTag_add.Add(drugHFTagClass.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>());
                }
                sQLControl_drugHFTag.AddRows(null, list_drugHFTag_add);
                sQLControl_drugHFTag.UpdateByDefulteExtra(null, list_drugHFTag_replace);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"新增標籤資料成功,共新增<{list_drugHFTag_add.Count}>筆資料";
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
        /// 以TagSN取得最新標籤資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [tagSN1,tagSN2...]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_tag_ByTagSN")]
        [HttpPost]
        public string get_latest_tag_ByTagSN([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_tag_ByTagSN";
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

                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[tagsSN]";
                    return returnData.JsonSerializationt(true);
                }
                string[] codes = returnData.ValueAry[0].Split(",");
                if (codes.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[tagsSN]";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < codes.Length; i++)
                {
                    codes[i] = codes[i].Trim();
                }
                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string sqlList = string.Join(",", codes.Select(code => $"'{code}'"));
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();

                string command = $@"
                                SELECT * FROM {DB}.{TableName} t1
                                WHERE UPPER(t1.{tagCol}) IN ({sqlList})
                                AND t1.{timeCol} = (
                                    SELECT MAX(t2.{timeCol})
                                    FROM {DB}.{TableName} t2
                                    WHERE UPPER(t2.{tagCol}) = UPPER(t1.{tagCol})
                                )";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"取得標籤資料成功,共<{drugHFTagClasses.Count}>筆資料";
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
            string[] temp = new enum_DrugHFTag().GetEnumNames();
            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_DrugHFTag());
            return table.JsonSerializationt(true);
        }
    }
}
