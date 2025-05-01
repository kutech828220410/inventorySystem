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
    /// <summary>
    /// 盤點異常LOG
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class medRecheckLog: Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化盤點異常資料庫
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
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(medRecheckLogClass))]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
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
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 新增資料至盤點異常資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///         [medRecheckLogClassAry]
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
            try
            {
                returnData.Method = "add";
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                GET_init(returnData);
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
                List<medRecheckLogClass> medRecheckLogClasses = returnData.Data.ObjToClass<List<medRecheckLogClass>>();
                if(medRecheckLogClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常!";
                    return returnData.JsonSerializationt();
                }
                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < medRecheckLogClasses.Count; i++)
                {
                    medRecheckLogClass medRecheckLogClass = medRecheckLogClasses[i];
                    if (medRecheckLogClass.GUID.StringIsEmpty() == true) medRecheckLogClass.GUID = Guid.NewGuid().ToString();
                    if (medRecheckLogClass.庫存值.StringIsEmpty() == true) medRecheckLogClass.庫存值 = "0";
                    if (medRecheckLogClass.盤點值.StringIsEmpty() == true) medRecheckLogClass.盤點值 = "0";
                    if (medRecheckLogClass.差異值.StringIsEmpty() == true) medRecheckLogClass.差異值 = "0";
                    medRecheckLogClass.差異值 = (medRecheckLogClass.盤點值.StringToInt32() - medRecheckLogClass.庫存值.StringToInt32()).ToString();

                    medRecheckLogClass.發生時間 = DateTime.Now.ToDateTimeString_6();
                    medRecheckLogClass.排除時間 = DateTime.MinValue.ToDateTimeString();
                    medRecheckLogClass.狀態 = enum_medRecheckLog_State.未排除.GetEnumName();
                    object[] value = medRecheckLogClass.ClassToSQL<medRecheckLogClass, enum_medRecheckLog>();
                    list_value.Add(value);
                }
              
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log_new", UserName, Password, Port, SSLMode);
          
                sQLControl.AddRows(null, list_value);
                returnData.Code = 200;
                returnData.Result = $"新增資料成功!共<{list_value.Count}>筆";
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
        /// 更新資料至盤點異常資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///         [medRecheckLogClassAry]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("replace_by_guid")]
        [HttpPost]
        public string replace_by_guid([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "replace_by_guid";
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
                List<medRecheckLogClass> medRecheckLogClasses = returnData.Data.ObjToClass<List<medRecheckLogClass>>();
                if (medRecheckLogClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] GUID 空白!";
                    return returnData.JsonSerializationt();
                }
                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < medRecheckLogClasses.Count; i++)
                {
                    medRecheckLogClass medRecheckLogClass = medRecheckLogClasses[i];
                    if (medRecheckLogClass.庫存值.StringIsEmpty() == true) medRecheckLogClass.庫存值 = "0";
                    if (medRecheckLogClass.盤點值.StringIsEmpty() == true) medRecheckLogClass.盤點值 = "0";
                    if (medRecheckLogClass.差異值.StringIsEmpty() == true) medRecheckLogClass.差異值 = "0";
                    medRecheckLogClass.差異值 = (medRecheckLogClass.盤點值.StringToInt32() - medRecheckLogClass.庫存值.StringToInt32()).ToString();
                    //medRecheckLogClass.發生時間 = DateTime.Now.ToDateTimeString_6();
                    //medRecheckLogClass.排除時間 = DateTime.MinValue.ToDateTimeString();
                    object[] value = medRecheckLogClass.ClassToSQL<medRecheckLogClass, enum_medRecheckLog>();
                    list_value.Add(value);
                }
                
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log_new", UserName, Password, Port, SSLMode);
         
                sQLControl.UpdateByDefulteExtra(null, list_value);
                returnData.Code = 200;
                returnData.Result = $"更新資料成功,共<{list_value.Count}>筆";
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
        /// 取得指定發生時間範圍盤點異常資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     "Value" : "2023-12-11 00:00:00,2023-12-11 23:59:59",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_occurrence_time_st_end")]
        [HttpPost]
        public string get_by_occurrence_time_st_end([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "get_by_occurrence_time_st_end";
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
          
                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 空白!";
                    return returnData.JsonSerializationt();
                }

                string[] date_ary = returnData.Value.Split(",");
                if (date_ary.Length != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                if (date_ary[0].Check_Date_String() == false || date_ary[1].Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 格式錯誤!";
                    return returnData.JsonSerializationt();
                }
    
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log_new", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByBetween(null, (int)enum_medRecheckLog.發生時間, date_ary[0], date_ary[1]);
                List<medRecheckLogClass> medRecheckLogClasses = list_value.SQLToClass<medRecheckLogClass, enum_medRecheckLog>();
                returnData.Data = medRecheckLogClasses;
                returnData.Code = 200;
                returnData.Result = $"取得資料成功,共{medRecheckLogClasses.Count}筆!";
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
        /// 取得指定排除時間範圍盤點異常資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     "Value" : "2023-12-11 00:00:00,2023-12-11 23:59:59",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_troubleshooting_time_st_end")]
        [HttpPost]
        public string get_by_troubleshooting_time_st_end([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "get_by_occurrence_time_st_end";
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 空白!";
                    return returnData.JsonSerializationt();
                }

                string[] date_ary = returnData.Value.Split(",");
                if (date_ary.Length != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                if (date_ary[0].Check_Date_String() == false || date_ary[1].Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 格式錯誤!";
                    return returnData.JsonSerializationt();
                }

                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log_new", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByBetween(null, (int)enum_medRecheckLog.排除時間, date_ary[0], date_ary[1]);
                List<medRecheckLogClass> medRecheckLogClasses = list_value.SQLToClass<medRecheckLogClass, enum_medRecheckLog>();
                returnData.Data = medRecheckLogClasses;
                returnData.Code = 200;
                returnData.Result = $"取得資料成功,共{medRecheckLogClasses.Count}筆!";
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
        /// 取得未排除盤點異常資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     "Value" : "2023-12-11 00:00:00,2023-12-11 23:59:59",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_ng_state_data")]
        [HttpPost]
        public string get_ng_state_data([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "get_ng_state_data";
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

              

                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log_new", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByDefult(null, (int)enum_medRecheckLog.狀態, "未排除");
                List<medRecheckLogClass> medRecheckLogClasses = list_value.SQLToClass<medRecheckLogClass, enum_medRecheckLog>();
                returnData.Data = medRecheckLogClasses;
                returnData.Code = 200;
                returnData.Result = $"取得資料成功,共{medRecheckLogClasses.Count}筆!";
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
        /// 取得未排除盤點異常資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     
        ///     "Data": 
        ///     {
        ///  
        ///     },
        ///     "ValueAry" :
        ///     [
        ///        "code"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_unresolved_qty_by_code")]
        [HttpPost]
        public string get_unresolved_qty_by_code([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "get_unresolved_qty_by_code";
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[code]";
                    return returnData.JsonSerializationt(true);
                }

                string code = returnData.ValueAry[0];
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log_new", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByDefult(null, (int)enum_medRecheckLog.狀態, "未排除");
                list_value = list_value.GetRows((int)enum_medRecheckLog.藥碼, code);
                medRecheckLogClass medRecheckLogClass = new medRecheckLogClass();
                medRecheckLogClass.藥碼 = code;
                medRecheckLogClass.差異值 = "0";
                for (int i = 0; i < list_value.Count; i++)
                {
                    medRecheckLogClass.藥名 = list_value[i][(int)enum_medRecheckLog.藥名].ObjectToString();
                    medRecheckLogClass.差異值 = (medRecheckLogClass.差異值.StringToInt32() + list_value[i][(int)enum_medRecheckLog.差異值].StringToInt32()).ToString();
                }

                returnData.Data = medRecheckLogClass;
                returnData.Code = 200;
                returnData.Result = $"取得藥品差異值成功";
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
        /// 設定未排除盤點異常資料庫,已排除
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     
        ///     "Data": 
        ///     {
        ///  
        ///     },
        ///     "ValueAry" :
        ///     [
        ///        "code",
        ///        "排除藥師姓名"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("set_unresolved_data_by_code")]
        [HttpPost]
        public string set_unresolved_data_by_code([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "set_unresolved_data_by_code";
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[code][排除藥師姓名]";
                    return returnData.JsonSerializationt(true);
                }

                string code = returnData.ValueAry[0];
                string 排除藥師姓名 = returnData.ValueAry[1];
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log_new", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByDefult(null, (int)enum_medRecheckLog.狀態, "未排除");
                list_value = list_value.GetRows((int)enum_medRecheckLog.藥碼, code);
                for (int i = 0; i < list_value.Count; i++)
                {
                    list_value[i][(int)enum_medRecheckLog.狀態] = "已排除";
                    list_value[i][(int)enum_medRecheckLog.排除時間] = DateTime.Now.ToDateTimeString_6();
                    list_value[i][(int)enum_medRecheckLog.排除藥師] = 排除藥師姓名;
                }
                sQLControl.UpdateByDefulteExtra(null, list_value);
                returnData.Code = 200;
                returnData.Result = $"設定未排除盤點異常資料庫,已排除成功共<{list_value.Count}>筆";
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
        /// 設定指定 GUID 的盤點異常紀錄為已排除
        /// </summary>
        /// <remarks>
        /// 以下為範例 JSON：
        ///
        /// <code>
        /// {
        ///   "ServerName": "管藥",
        ///   "ServerType": "調劑台",
        ///   "ValueAry": [
        ///     "guid",
        ///     "排除藥師姓名"
        ///   ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>API 回應 JSON</returns>
        [Route("set_unresolved_data_by_guid")]
        [HttpPost]
        public string set_unresolved_data_by_guid([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "set_unresolved_data_by_guid";

                var settings = ServerSettingController.GetAllServerSetting()
                                .MyFind(returnData.ServerName, returnData.ServerType, "一般資料");

                if (settings.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }

                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ValueAry 內容應為 [GUID, 排除藥師姓名]";
                    return returnData.JsonSerializationt();
                }

                string guid = returnData.ValueAry[0];
                string pharmacist = returnData.ValueAry[1];

                var db = settings[0];
                SQLControl sql = new SQLControl(db.Server, db.DBName, "med_recheck_log_new", db.User, db.Password, (uint)db.Port.StringToInt32(), SSLMode);

                List<object[]> rows = sql.GetRowsByDefult(null, (int)enum_medRecheckLog.GUID, guid);
                if (rows.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找不到對應 GUID: {guid}";
                    return returnData.JsonSerializationt();
                }

                rows[0][(int)enum_medRecheckLog.狀態] = enum_medRecheckLog_State.已排除.GetEnumName();
                rows[0][(int)enum_medRecheckLog.排除時間] = DateTime.Now.ToDateTimeString_6();
                rows[0][(int)enum_medRecheckLog.排除藥師] = pharmacist;

                sql.UpdateByDefulteExtra(null, rows);

                returnData.Code = 200;
                returnData.Result = $"成功排除紀錄 GUID: {guid}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt();
            }
        }

        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {
            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_medRecheckLog());
            return table.JsonSerializationt(true);
        }


     
    }
}
