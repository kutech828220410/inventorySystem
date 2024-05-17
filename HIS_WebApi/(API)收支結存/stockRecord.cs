using System;
using Microsoft.AspNetCore.Mvc;
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

namespace HIS_WebApi
{
    /// <summary>
    /// 庫存紀錄
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class stockRecordController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化庫存紀錄資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
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
        public string POST_init([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);

            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

                returnData.Method = "POST_init";
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 紀錄庫存資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",  
        ///     "Data": 
        ///     {
        ///         [medClass]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///        "庫別",
        ///        "名稱"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("add_record")]
        [HttpPost]
        public string POST_add_record(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add_record";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                if (returnData.ValueAry == null)
                {
                    returnData.ValueAry = new List<string>();
                    returnData.ValueAry.Add($"{returnData.ServerType}");
                    returnData.ValueAry.Add($"{returnData.ServerName}");
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.ValueAry = new List<string>();
                    returnData.ValueAry.Add($"{returnData.ServerType}");
                    returnData.ValueAry.Add($"{returnData.ServerName}");
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[庫別][名稱]";
                    return returnData.JsonSerializationt(true);
                }

                SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, new enum_stockRecord().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_stockRecord_content = new SQLControl(Server, DB, new enum_stockRecord_content().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<medClass> medClasses = returnData.Data.ObjToClass<List<medClass>>();
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[retuen.Data] 須為[List<medClass>]";
                    return returnData.JsonSerializationt();
                }
                if (medClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[retuen.Data] 須為[List<medClass>]";
                    return returnData.JsonSerializationt();
                }
                string 庫別 = returnData.ValueAry[0];
                string 庫名 = returnData.ValueAry[1];
                DateTime dateTime = DateTime.Now;
                object[] value = new object[new enum_stockRecord().GetLength()];
                value[(int)enum_stockRecord.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_stockRecord.庫別] = 庫別;
                value[(int)enum_stockRecord.庫名] = 庫名;
                value[(int)enum_stockRecord.加入時間] = dateTime.ToDateTimeString_6();

                string Master_GUID = value[(int)enum_stockRecord.GUID].ObjectToString();

                List<object[]> list_contents = new List<object[]>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    object[] value_ontent = new object[new enum_stockRecord_content().GetLength()];
                    value_ontent[(int)enum_stockRecord_content.GUID] = Guid.NewGuid().ToString();
                    value_ontent[(int)enum_stockRecord_content.Master_GUID] = Master_GUID;
                    value_ontent[(int)enum_stockRecord_content.藥碼] = medClasses[i].藥品碼;
                    value_ontent[(int)enum_stockRecord_content.藥名] = medClasses[i].藥品名稱;
                    value_ontent[(int)enum_stockRecord_content.庫存] = medClasses[i].總庫存;
                    value_ontent[(int)enum_stockRecord_content.加入時間] = dateTime.ToDateTimeString_6();
                    list_contents.Add(value_ontent);
                }

                sQLControl_stockRecord.AddRow(null, value);
                sQLControl_stockRecord_content.AddRows(null, list_contents);
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功新增<{list_contents.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"stockRecord");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"stockRecord");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"stockRecord", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得全部庫存列表
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",  
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_all_record_menu")]
        [HttpPost]
        public string POST_get_all_record_menu(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "POST_get_all_record_menu";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();



                SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, new enum_stockRecord().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_stockRecord_content = new SQLControl(Server, DB, new enum_stockRecord_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                List<object[]> list_stockRecord = sQLControl_stockRecord.GetAllRows(null);
                List<stockRecord> stockRecords = list_stockRecord.SQLToClass<stockRecord, enum_stockRecord>();

                returnData.Data = stockRecords;
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功取得<{stockRecords.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"stockRecord");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"stockRecord");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"stockRecord", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得所有庫存紀錄(含細節)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",  
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_all_record")]
        [HttpPost]
        public string POST_get_all_record(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_all_record";
            string temp = "";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");

                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }


                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                CheckCreatTable(serverSettingClass);

                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, new enum_stockRecord().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_stockRecord_content = new SQLControl(Server, DB, new enum_stockRecord_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                List<object[]> list_stockRecord = sQLControl_stockRecord.GetAllRows(null);
                List<object[]> list_stockRecord_content = sQLControl_stockRecord_content.GetAllRows(null);
                List<object[]> list_stockRecord_content_buf = new List<object[]>();
                List<stockRecord> stockRecords = list_stockRecord.SQLToClass<stockRecord, enum_stockRecord>();
                if (stockRecords.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < stockRecords.Count; i++)
                {
                    list_stockRecord_content_buf = list_stockRecord_content.GetRows((int)enum_stockRecord_content.Master_GUID, stockRecords[i].GUID);
                    List<stockRecord_content> stockRecord_Contents = list_stockRecord_content_buf.SQLToClass<stockRecord_content, enum_stockRecord_content>();
                    stockRecords[i].Contents = stockRecord_Contents;
                }

                returnData.Data = stockRecords;
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功取得<{stockRecords.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"stockRecord");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"stockRecord");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"stockRecord", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得所有庫存紀錄(不含細節)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",  
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_all_record_simple")]
        [HttpPost]
        public string POST_get_all_record_simple(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_all_record_simple";
            string temp = "";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");

                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }


                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                CheckCreatTable(serverSettingClass);

                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, new enum_stockRecord().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_stockRecord_content = new SQLControl(Server, DB, new enum_stockRecord_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                List<object[]> list_stockRecord = sQLControl_stockRecord.GetAllRows(null);
                List<object[]> list_stockRecord_content = sQLControl_stockRecord_content.GetAllRows(null);
                List<object[]> list_stockRecord_content_buf = new List<object[]>();
                List<stockRecord> stockRecords = list_stockRecord.SQLToClass<stockRecord, enum_stockRecord>();
                if (stockRecords.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }

                returnData.Data = stockRecords;
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功取得<{stockRecords.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"stockRecord");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"stockRecord");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"stockRecord", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID刪除庫存紀錄
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",  
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "GUID"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("delete_record_by_guid")]
        [HttpPost]
        public string POST_delete_record_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_record_by_guid";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");

                if (serverSettingClasses.Count == 0)
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
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID]";
                    return returnData.JsonSerializationt(true);
                }

                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                CheckCreatTable(serverSettingClass);

                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                string GUID = returnData.ValueAry[0];


                SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, new enum_stockRecord().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_stockRecord_content = new SQLControl(Server, DB, new enum_stockRecord_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                List<object[]> list_stockRecord = sQLControl_stockRecord.GetRowsByDefult(null, (int)enum_stockRecord.GUID, GUID);
                List<object[]> list_stockRecord_content = sQLControl_stockRecord_content.GetRowsByDefult(null, (int)enum_stockRecord_content.Master_GUID, GUID);
                sQLControl_stockRecord.DeleteExtra(null, list_stockRecord);
                sQLControl_stockRecord_content.DeleteExtra(null, list_stockRecord_content);
                returnData.Data = "";
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功刪除資料,stockRecord<{list_stockRecord.Count}>筆,stockRecord_content<{list_stockRecord_content.Count}>筆,";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"stockRecord");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"stockRecord");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"stockRecord", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID取得庫存紀錄
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",  
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "GUID"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_record_by_guid")]
        [HttpPost]
        public string POST_get_record_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_record_by_guid";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");

                if (serverSettingClasses.Count == 0)
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
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID]";
                    return returnData.JsonSerializationt(true);
                }

                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                CheckCreatTable(serverSettingClass);

                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                string GUID = returnData.ValueAry[0];


                SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, new enum_stockRecord().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_stockRecord_content = new SQLControl(Server, DB, new enum_stockRecord_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                List<object[]> list_stockRecord = sQLControl_stockRecord.GetRowsByDefult(null, (int)enum_stockRecord.GUID, GUID);
                List<object[]> list_stockRecord_content = sQLControl_stockRecord_content.GetRowsByDefult(null, (int)enum_stockRecord_content.Master_GUID, GUID);
                List<stockRecord> stockRecords = list_stockRecord.SQLToClass<stockRecord, enum_stockRecord>();
                if (stockRecords.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }

                List<stockRecord_content> stockRecord_Contents = list_stockRecord_content.SQLToClass<stockRecord_content, enum_stockRecord_content>();
                stockRecords[0].Contents = stockRecord_Contents;
                returnData.Data = stockRecords[0];
                returnData.Result = $"[{Basic.Reflection.GetAsyncMethodName()}] 成功取得<{stockRecords.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"stockRecord");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"stockRecord");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"stockRecord", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID取得庫存紀錄(SheetClass)(多台合併)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "GUID1,GUID2",
        ///       "ds01,ds01",
        ///       "藥庫,藥庫"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_datas_record_by_guid")]
        [HttpPost]
        public string POST_get_datas_record_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_datas_record_by_guid";
            try
            {
                List<stockRecord> StockRecords = new List<stockRecord>();
                List<Task> tasks = new List<Task>();
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID1,GUID2..][ds01,ds01..][藥庫,藥局..]";
                    return returnData.JsonSerializationt(true);
                }
                string[] GUIDs = returnData.ValueAry[0].Split(",");
                string[] serverNames = returnData.ValueAry[1].Split(",");
                string[] serverTypes = returnData.ValueAry[2].Split(",");
                int temp = GUIDs.Length;
                if ((GUIDs.Length != temp) && (serverNames.Length != temp) && (serverTypes.Length != temp))
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容長度異常";
                    return returnData.JsonSerializationt(true);
                }
                List<medClass> medClasses = medClass.get_med_cloud("http://127.0.0.1:4433");

                Dictionary<string, List<medClass>> keyValuePairs_medClass = medClass.CoverToDictionaryByCode(medClasses);
                for (int i = 0; i < serverNames.Length; i++)
                {
                    string serverName = serverNames[i];
                    string serverType = serverTypes[i];
                    string GUID = GUIDs[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "一般資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();

                        SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, new enum_stockRecord().GetEnumDescription(), UserName, Password, Port, SSLMode);
                        SQLControl sQLControl_stockRecord_content = new SQLControl(Server, DB, new enum_stockRecord_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                        List<object[]> list_stockRecord = sQLControl_stockRecord.GetRowsByDefult(null, (int)enum_stockRecord.GUID, GUID);
                        List<object[]> list_stockRecord_content = sQLControl_stockRecord_content.GetRowsByDefult(null, (int)enum_stockRecord_content.Master_GUID, GUID);
                        List<stockRecord> stockRecords = list_stockRecord.SQLToClass<stockRecord, enum_stockRecord>();
                        if (stockRecords.Count == 0)
                        {
                            return;
                        }

                        List<stockRecord_content> stockRecord_Contents = list_stockRecord_content.SQLToClass<stockRecord_content, enum_stockRecord_content>();
                        List<medClass> medClasses_buf = new List<medClass>();
                        for (int i = 1; i < stockRecord_Contents.Count; i++)
                        {
                            medClasses_buf = medClass.SortDictionaryByCode(keyValuePairs_medClass, stockRecord_Contents[i].藥碼);
                            if (medClasses_buf.Count != 0)
                            {
                                stockRecord_Contents[i].藥名 = medClasses_buf[0].藥品名稱;
                            }
                        }

                        stockRecords[0].Contents = stockRecord_Contents;
                        StockRecords.LockAdd(stockRecords[0]);

                    })));
                }
                Task.WhenAll(tasks).Wait();
                returnData.Data = StockRecords;
                returnData.Result = $"成功取得庫存紀錄, 共<{StockRecords.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"stockRecord");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"stockRecord");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"stockRecord", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID取得庫存紀錄(Excel)(多台合併)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "GUID1,GUID2",
        ///       "ds01,ds01",
        ///       "藥庫,藥庫"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("download_excel_datas_record_by_guid")]
        [HttpPost]
        public async Task<ActionResult> POST_download_excel_datas_record_by_guid(returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                returnData = POST_get_datas_record_by_guid(returnData).JsonDeserializet<returnData>();
                if (returnData.Code != 200)
                {
                    return null;
                }
                string jsondata = returnData.Data.JsonSerializationt();

                List<stockRecord> stockRecords = jsondata.JsonDeserializet<List<stockRecord>>();


                List<string> colNames = new List<string>();
                colNames.Add("藥碼");
                colNames.Add("藥名");
                for (int i = 0; i < stockRecords.Count; i++)
                {
                    colNames.Add($"{stockRecords[i].庫別}庫存");
                }
                List<object[]> list_rows = new List<object[]>();
                List<object[]> list_rows_buf = new List<object[]>();
                for (int i = 0; i < stockRecords.Count; i++)
                {
                    for (int k = 0; k < stockRecords[i].Contents.Count; k++)
                    {
                        list_rows_buf = list_rows.GetRows(0, stockRecords[i].Contents[k].藥碼);
                        if(list_rows_buf.Count == 0)
                        {
                            object[] value = new object[colNames.Count];
                            value[0] = stockRecords[i].Contents[k].藥碼;
                            value[1] = stockRecords[i].Contents[k].藥名;
                            value[i + 2] = stockRecords[i].Contents[k].庫存;
                            list_rows.Add(value);
                        }
                        else
                        {
                            object[] value = list_rows_buf[0];
                            value[i + 2] = stockRecords[i].Contents[k].庫存;
                        }
                    }
                }

                System.Data.DataTable dataTable = list_rows.ToDataTable(colNames.ToArray());

                string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string xls_command = "application/vnd.ms-excel";

                byte[] excelData = dataTable.NPOI_GetBytes(Excel_Type.xlsx);
                Stream stream = new MemoryStream(excelData);
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_庫存紀錄.xlsx"));
            }
            catch
            {
                return null;
            }

        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_stockRecord()));
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_stockRecord_content()));
            return tables.JsonSerializationt(true);
        }
      
    }
}
