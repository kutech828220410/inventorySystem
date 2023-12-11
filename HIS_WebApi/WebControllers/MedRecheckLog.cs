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
    public class MedRecheckLog: Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化覆盤異常資料庫
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
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
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
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 新增資料至覆盤異常資料庫
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
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "POST_add";
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                GET_init(returnData);
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();
                MedRecheckLogClass medRecheckLogClass = returnData.Data.ObjToClass<MedRecheckLogClass>();
                if(medRecheckLogClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常!";
                    return returnData.JsonSerializationt();
                }
                if (medRecheckLogClass.GUID.StringIsEmpty() == true) medRecheckLogClass.GUID = Guid.NewGuid().ToString();
                if (medRecheckLogClass.系統理論值.StringIsEmpty() == true) medRecheckLogClass.系統理論值 = "0";
                if (medRecheckLogClass.覆盤理論值.StringIsEmpty() == true) medRecheckLogClass.覆盤理論值 = "0";
                if (medRecheckLogClass.校正庫存值.StringIsEmpty() == true) medRecheckLogClass.校正庫存值 = "0";
                medRecheckLogClass.發生時間 = DateTime.Now.ToDateTimeString_6();
                medRecheckLogClass.排除時間 = DateTime.MinValue.ToDateTimeString();
                medRecheckLogClass.狀態 = enum_MedRecheckLog_State.未排除.GetEnumName();
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log", UserName, Password, Port, SSLMode);
                object[] value = medRecheckLogClass.ClassToSQL<MedRecheckLogClass , enum_MedRecheckLog>();
                sQLControl.AddRow(null, value);
                returnData.Code = 200;
                returnData.Result = $"新增資料成功!";
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
        /// 更新資料至覆盤異常資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
        ///     "Value" : "c301bce4-d865-474c-8faf-23fef4451869",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("replace_by_guid")]
        [HttpPost]
        public string POST_replace_by_guid([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "POST_replace_by_guid";
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
                MedRecheckLogClass medRecheckLogClass = returnData.Data.ObjToClass<MedRecheckLogClass>();
                if (medRecheckLogClass == null)
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
                medRecheckLogClass.GUID = returnData.Value;
                if (medRecheckLogClass.系統理論值.StringIsEmpty() == true) medRecheckLogClass.系統理論值 = "0";
                if (medRecheckLogClass.覆盤理論值.StringIsEmpty() == true) medRecheckLogClass.覆盤理論值 = "0";
                if (medRecheckLogClass.校正庫存值.StringIsEmpty() == true) medRecheckLogClass.校正庫存值 = "0";
                medRecheckLogClass.發生時間 = DateTime.Now.ToDateTimeString_6();
                medRecheckLogClass.排除時間 = DateTime.MinValue.ToDateTimeString();
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log", UserName, Password, Port, SSLMode);
                object[] value = medRecheckLogClass.ClassToSQL<MedRecheckLogClass, enum_MedRecheckLog>();
                List<object[]> list_value = new List<object[]>();
                list_value.Add(value);
                sQLControl.UpdateByDefulteExtra(null, list_value);
                returnData.Code = 200;
                returnData.Result = $"更新資料成功!";
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
        /// 取得指定發生時間範圍覆盤異常資料庫
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
        public string POST_get_by_occurrence_time_st_end([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "get_by_occurrence_time_st_end";
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
                if (date_ary[0].Check_Date_String() == false || date_ary[1].Check_Date_String())
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 格式錯誤!";
                    return returnData.JsonSerializationt();
                }
    
                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByBetween(null, (int)enum_MedRecheckLog.發生時間, date_ary[0], date_ary[1]);
                List<MedRecheckLogClass> medRecheckLogClasses = list_value.SQLToClass<MedRecheckLogClass, enum_MedRecheckLog>();
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
        /// 取得指定排除時間範圍覆盤異常資料庫
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
        public string POST_get_by_troubleshooting_time_st_end([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "get_by_occurrence_time_st_end";
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
                if (date_ary[0].Check_Date_String() == false || date_ary[1].Check_Date_String())
                {
                    returnData.Code = -200;
                    returnData.Result = $"[eturnData.Value] 日期範圍 格式錯誤!";
                    return returnData.JsonSerializationt();
                }

                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByBetween(null, (int)enum_MedRecheckLog.排除時間, date_ary[0], date_ary[1]);
                List<MedRecheckLogClass> medRecheckLogClasses = list_value.SQLToClass<MedRecheckLogClass, enum_MedRecheckLog>();
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
        /// 取得未排除覆盤異常資料庫
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
        public string POST_get_ng_state_data([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "get_ng_state_data";
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

              

                SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetRowsByDefult(null, (int)enum_MedRecheckLog.狀態, "未排除");
                List<MedRecheckLogClass> medRecheckLogClasses = list_value.SQLToClass<MedRecheckLogClass, enum_MedRecheckLog>();
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

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "med_recheck_log", UserName, Password, Port, SSLMode);
            Table table = new Table("med_recheck_log");
            table.DBName = DB;
            table.Server = Server;
            table.Username = UserName;
            table.Password = Password;
            table.Port = Port.ToString();

            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("藥碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table.AddColumnList("藥名", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table.AddColumnList("系統理論值", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("覆盤理論值", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("校正庫存值", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("批號", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("效期", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("醫令_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table.AddColumnList("交易紀錄_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table.AddColumnList("操作人", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
            table.AddColumnList("發生時間", Table.DateType.DATETIME, 20, Table.IndexType.INDEX);
            table.AddColumnList("排除時間", Table.DateType.DATETIME, 20, Table.IndexType.INDEX);
            table.AddColumnList("狀態", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);


            if (!sQLControl.IsTableCreat())
            {
                sQLControl.CreatTable(table);
            }
            else
            {
                sQLControl.CheckAllColumnName(table, true);
            }
            return table.JsonSerializationt(true);
        }
    }
}
