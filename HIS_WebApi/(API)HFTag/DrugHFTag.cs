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
using System.Net.Http.Json;
using System.Diagnostics;

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

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

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

                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

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
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
            }
        }
        /// <summary>
        /// 設定標籤銷毀
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
        [Route("set_tag_broken")]
        [HttpPost]
        public string set_tag_broken([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "set_tag_broken";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();

                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

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
                    if (drugHFTagClass.效期.StringIsEmpty()) continue;
                    drugHFTagClass.GUID = Guid.NewGuid().ToString();
                    drugHFTagClass.更新時間 = DateTime.Now.ToDateTimeString_6();
                    drugHFTagClass.狀態 = enum_DrugHFTagStatus.已銷毀.GetEnumName();
                    list_drugHFTag_add.Add(drugHFTagClass.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>());
                }
                sQLControl_drugHFTag.AddRows(null, list_drugHFTag_add);
                sQLControl_drugHFTag.UpdateByDefulteExtra(null, list_drugHFTag_replace);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"新增標籤資料成功,共新增<{list_drugHFTag_add.Count}>筆資料";
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
            }
        }
        /// <summary>
        /// 設定標籤銷毀
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
        [Route("set_tag_stockin")]
        [HttpPost]
        public string set_tag_stockin([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "set_tag_stockin";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

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
                    if (drugHFTagClass.效期.StringIsEmpty()) continue;
                    drugHFTagClass.GUID = Guid.NewGuid().ToString();
                    drugHFTagClass.更新時間 = DateTime.Now.ToDateTimeString_6();
                    drugHFTagClass.狀態 = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                    list_drugHFTag_add.Add(drugHFTagClass.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>());
                }
                sQLControl_drugHFTag.AddRows(null, list_drugHFTag_add);
                sQLControl_drugHFTag.UpdateByDefulteExtra(null, list_drugHFTag_replace);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"新增標籤資料成功,共新增<{list_drugHFTag_add.Count}>筆資料";
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
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
                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

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
        /// <summary>
        /// 取得所有Tag中最新一筆且狀態為「已重置」的標籤資料
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_reset_tag")]
        [HttpPost]
        public string get_latest_reset_tag([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_reset_tag";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string command = $@"
                                SELECT * FROM {DB}.{TableName} t1
                                WHERE t1.{statusCol} = '已重置'
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
                returnData.Result = $"取得最新'已重置'標籤資料成功，共<{drugHFTagClasses.Count}>筆資料";
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
        /// 取得所有Tag中最新一筆且狀態為「入庫註記」的標籤資料
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_stockin_tag")]
        [HttpPost]
        public string get_latest_stockin_tag([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_stockin_tag";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();

                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string command = $@"
                                                SELECT t1.*
                        FROM {DB}.{TableName} t1
                        INNER JOIN (
                            SELECT {tagCol}, MAX({timeCol}) AS 最新時間
                            FROM {DB}.{TableName}
                            WHERE {statusCol} = '入庫註記'
                            GROUP BY {tagCol}
                        ) t2
                        ON t1.{tagCol} = t2.{tagCol} 
                        AND t1.{timeCol} = t2.最新時間
                        WHERE t1.{statusCol} = '入庫註記';";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"取得最新'入庫註記'標籤資料成功，共<{drugHFTagClasses.Count}>筆資料";
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
        /// 取得所有Tag中最新一筆且狀態為「出庫註記」的標籤資料
        /// </summary>
        [Route("get_latest_stockout_tag")]
        [HttpPost]
        public string get_latest_stockout_tag([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_stockout_tag";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                var db = _sys_serverSettingClasses[0];
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sql = new SQLControl(db.Server, db.DBName, TableName, db.User, db.Password, (uint)db.Port.StringToInt32(), SSLMode);

                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string query = $@"
            SELECT * FROM {db.DBName}.{TableName} t1
            WHERE t1.{statusCol} = '出庫註記'
              AND t1.{timeCol} = (
                  SELECT MAX(t2.{timeCol})
                  FROM {db.DBName}.{TableName} t2
                  WHERE t2.{tagCol} = t1.{tagCol}
              )";

                var resultTable = sql.WtrteCommandAndExecuteReader(query);
                var resultList = resultTable.DataTableToRowList().SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.Data = resultList;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得最新『出庫註記』標籤資料成功，共<{resultList.Count}>筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception: {ex.Message}";
                return returnData.JsonSerializationt();
            }
        }

        /// <summary>
        /// 取得所有Tag中最新一筆的標籤資料
        /// </summary>
        [Route("get_latest_tags")]
        [HttpPost]
        public string get_latest_tags([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_tags";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                var db = _sys_serverSettingClasses[0];
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sql = new SQLControl(db.Server, db.DBName, TableName, db.User, db.Password, (uint)db.Port.StringToInt32(), SSLMode);

                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string query = $@"
            SELECT * FROM {db.DBName}.{TableName} t1
            WHERE t1.{timeCol} = (
                  SELECT MAX(t2.{timeCol})
                  FROM {db.DBName}.{TableName} t2
                  WHERE t2.{tagCol} = t1.{tagCol}
              )";

                var resultTable = sql.WtrteCommandAndExecuteReader(query);
                var resultList = resultTable.DataTableToRowList().SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.Data = resultList;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得最新標籤資料成功，共<{resultList.Count}>筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception: {ex.Message}";
                return returnData.JsonSerializationt();
            }
        }


        /// <summary>
        /// 取得所有Tag中最新一筆且「可入庫」的標籤資料（狀態為「出庫註記」或「已重置」且曾為「已重置」）
        /// </summary>
        [Route("get_latest_stockin_eligible_tags")]
        [HttpPost]
        public string get_latest_stockin_eligible_tags([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_stockin_eligible_tags";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");
                
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                var db = _sys_serverSettingClasses[0];
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sql = new SQLControl(db.Server, db.DBName, TableName, db.User, db.Password, (uint)db.Port.StringToInt32(), SSLMode);

                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                // 撈出曾為已重置的標籤
                string resetQuery = $"SELECT DISTINCT {tagCol} FROM {db.DBName}.{TableName} WHERE {statusCol} = '已重置'";
                var resetTable = sql.WtrteCommandAndExecuteReader(resetQuery);
                var resetTags = resetTable.DataTableToRowList().Select(r => r[0].ToString()).Distinct().ToList();
                if (resetTags.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Data = new List<DrugHFTagClass>();
                    returnData.Result = "無符合條件標籤";
                    return returnData.JsonSerializationt();
                }

                string inClause = string.Join(",", resetTags.Select(tag => $"'{tag}'"));
                string mainQuery = $@"
            SELECT * FROM {db.DBName}.{TableName} t1
            WHERE t1.{tagCol} IN ({inClause})
              AND t1.{statusCol} IN ('出庫註記', '已重置')
              AND t1.{timeCol} = (
                  SELECT MAX(t2.{timeCol})
                  FROM {db.DBName}.{TableName} t2
                  WHERE t2.{tagCol} = t1.{tagCol}
              )";

                var resultTable = sql.WtrteCommandAndExecuteReader(mainQuery);
                var resultList = resultTable.DataTableToRowList().SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.Data = resultList;
                returnData.Result = $"取得最新可入庫標籤資料成功，共<{resultList.Count}>筆資料";
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
        /// 取得所有Tag中最新一筆且狀態為「入庫註記」的標籤資料
        /// </summary>
        [Route("get_latest_stockout_eligible_tags")]
        [HttpPost]
        public string get_latest_stockout_eligible_tags([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_stockout_eligible_tags";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = new List<sys_serverSettingClass>();

                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                else _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM_DB");

                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                var db = _sys_serverSettingClasses[0];
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sql = new SQLControl(db.Server, db.DBName, TableName, db.User, db.Password, (uint)db.Port.StringToInt32(), SSLMode);

                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string mainQuery = $@"
        SELECT * FROM {db.DBName}.{TableName} t1
        WHERE t1.{statusCol} = '入庫註記'
          AND t1.{timeCol} = (
              SELECT MAX(t2.{timeCol})
              FROM {db.DBName}.{TableName} t2
              WHERE t2.{tagCol} = t1.{tagCol}
          )";

                Logger.Log("DrugHFTag_Debug", $"【MainQuery】\n{mainQuery}");

                var resultTable = sql.WtrteCommandAndExecuteReader(mainQuery);
                var resultList = resultTable.DataTableToRowList().SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                var tagLog = string.Join("\n", resultList.Select(x => $"{x.TagSN} => {x.狀態}").Take(30));
                Logger.Log("DrugHFTag_Debug", $"【FinalResult Count】{resultList.Count}\n{tagLog}");

                returnData.Code = 200;
                returnData.Data = resultList;
                returnData.Result = $"取得最新可出庫標籤資料成功，共<{resultList.Count}>筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                Logger.Log("DrugHFTag_Debug", $"【Exception】{e}");
                return returnData.JsonSerializationt();
            }
        }
        [Route("get_stockin_status_detail_summary_by_codes")]
        [HttpPost]
        public async Task<string> get_stockin_status_detail_summary_by_codes([FromBody] returnData returnData)
        {
            returnData.Method = "get_stockin_status_detail_summary_by_codes";
            Stopwatch swTotal = Stopwatch.StartNew(); // 總計時間
            List<string> logTimes = new();            // 紀錄每步驟耗時

            try
            {
                // 計算單一 API 耗時的封裝方法
                async Task<(string name, string result, long elapsedMs)> CallApiWithTime(string apiUrl, string body, string name)
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    var result = await Basic.Net.WEBApiPostJsonAsync(apiUrl, body, false);
                    sw.Stop();
                    return (name, result, sw.ElapsedMilliseconds);
                }

                Stopwatch sw = Stopwatch.StartNew();

                // 解析藥碼
                List<string> drugCodes = returnData.Data.ObjToClass<List<string>>();
                logTimes.Add($"解析藥碼: {sw.ElapsedMilliseconds} ms");

                string API_Server = "http://127.0.0.1:4433";

                // 建立 HttpClient
                sw.Restart();
                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(API_Server);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                logTimes.Add($"建立 HttpClient: {sw.ElapsedMilliseconds} ms");

                // 建立 RequestBody
                sw.Restart();
                var requestBody = new returnData().JsonSerializationt(); // 空參數即可
                logTimes.Add($"建立 RequestBody: {sw.ElapsedMilliseconds} ms");

                // 四個 API 平行呼叫
                var stockinEligibleTask = CallApiWithTime($"{API_Server}/api/DrugHFTag/get_latest_stockin_eligible_tags", requestBody, "get_latest_stockin_eligible_tags");
                var stockoutEligibleTask = CallApiWithTime($"{API_Server}/api/DrugHFTag/get_latest_stockout_eligible_tags", requestBody, "get_latest_stockout_eligible_tags");
                var stockinTagTask = CallApiWithTime($"{API_Server}/api/DrugHFTag/get_latest_stockin_tag", requestBody, "get_latest_stockin_tag");
                var stockoutTagTask = CallApiWithTime($"{API_Server}/api/DrugHFTag/get_latest_stockout_tag", requestBody, "get_latest_stockout_tag");

                await Task.WhenAll(stockinEligibleTask, stockoutEligibleTask, stockinTagTask, stockoutTagTask);

                // 取得結果
                var stockinEligibleResult = await stockinEligibleTask;
                var stockoutEligibleResult = await stockoutEligibleTask;
                var stockinTagResult = await stockinTagTask;
                var stockoutTagResult = await stockoutTagTask;

                // 記錄各自耗時
                logTimes.Add($"API {stockinEligibleResult.name}: {stockinEligibleResult.elapsedMs} ms");
                logTimes.Add($"API {stockoutEligibleResult.name}: {stockoutEligibleResult.elapsedMs} ms");
                logTimes.Add($"API {stockinTagResult.name}: {stockinTagResult.elapsedMs} ms");
                logTimes.Add($"API {stockoutTagResult.name}: {stockoutTagResult.elapsedMs} ms");

                // 反序列化
                sw.Restart();
                var stockinEligible = stockinEligibleResult.result.JsonDeserializet<returnData>().Data.ObjToClass<List<DrugHFTagClass>>();
                var stockoutEligible = stockoutEligibleResult.result.JsonDeserializet<returnData>().Data.ObjToClass<List<DrugHFTagClass>>();
                var stockinTag = stockinTagResult.result.JsonDeserializet<returnData>().Data.ObjToClass<List<DrugHFTagClass>>();
                var stockoutTag = stockoutTagResult.result.JsonDeserializet<returnData>().Data.ObjToClass<List<DrugHFTagClass>>();
                logTimes.Add($"資料解析完成: {sw.ElapsedMilliseconds} ms");

                // 統計
                sw.Restart();
                List<DrugHFTagStatusSummaryByCode> result = new();
                foreach (var code in drugCodes)
                {
                    double 可入庫數量 = stockinEligible.Count(t => t.藥碼 == code);
                    double 可出庫數量 = stockoutEligible.Count(t => t.藥碼 == code);
                    double 已入庫數量 = stockinTag.Count(t => t.藥碼 == code);
                    double 已出庫數量 = stockoutTag.Count(t => t.藥碼 == code);

                    result.Add(new DrugHFTagStatusSummaryByCode
                    {
                        藥碼 = code,
                        可入庫數量 = 可入庫數量,
                        可出庫數量 = 可出庫數量,
                        已入庫數量 = 已入庫數量,
                        已出庫數量 = 已出庫數量,
                        總數量 = 可入庫數量 + 可出庫數量
                    });
                }
                logTimes.Add($"統計計算完成: {sw.ElapsedMilliseconds} ms");

                swTotal.Stop();
                returnData.Code = 200;
                returnData.Data = result;
                returnData.Result = $"成功統計共 {result.Count} 筆藥碼資料, 總耗時 {swTotal.ElapsedMilliseconds} ms\n" +
                                    string.Join("\n", logTimes);
                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                swTotal.Stop();
                returnData.Code = -200;
                returnData.Result = $"錯誤: {ex.Message}, 總耗時 {swTotal.ElapsedMilliseconds} ms\n" +
                                    string.Join("\n", logTimes);
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
