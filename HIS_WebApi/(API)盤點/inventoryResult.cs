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
    public class inventoryResult : Controller
    {
        static public string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
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
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                string msg = "";
                return msg;
            }
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {


            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_inventoryResult()));
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_inventoryResult_content()));
            return tables.JsonSerializationt(true);
        }

        /// <summary>
        /// 取得盤點結果
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "ValueAry" : 
        ///     [
        ///         "[StartDateTime]",
        ///         "[EndDateTime]"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_inventoryResult_by_st_end_time")]
        [HttpPost]
        public string POST_get_inventoryResult(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "get_inventoryResult";
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
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 為 null";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[StartDateTime][EndDateTime]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry[0].Check_Date_String() == false || returnData.ValueAry[1].Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入參數為非法日期";
                    return returnData.JsonSerializationt(true);
                }

                SQLControl sQLControl_inventoryResult = new SQLControl(Server, DB, new enum_inventoryResult().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inventoryResult_content = new SQLControl(Server, DB, new enum_inventoryResult_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                string StartDateTime = returnData.ValueAry[0];
                string EndDateTime = returnData.ValueAry[1];

                List<object[]> list_inventoryResult = sQLControl_inventoryResult.GetRowsByBetween(null, (int)enum_inventoryResult.加入時間, StartDateTime, EndDateTime);

                List<inventoryResultClass> inventoryResults = list_inventoryResult.SQLToClass<inventoryResultClass, enum_inventoryResult>();

                for (int i = 0; i < inventoryResults.Count; i++)
                {
                    string GUID = inventoryResults[i].GUID;
                    List<object[]> list_inventoryResult_content = sQLControl_inventoryResult_content.GetRowsByDefult(null, (int)enum_inventoryResult_content.Master_GUID, GUID);
                    inventoryResults[i].Contents = list_inventoryResult_content.SQLToClass<inventoryResult_content, enum_inventoryResult_content>();

                }
                returnData.Data = inventoryResults;
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功取得<{inventoryResults.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"inventoryResult");
                Logger.Log($"inventoryResult", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"inventoryResult");
                return returnData.JsonSerializationt(true);
                
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"inventoryResult", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 紀錄盤點結果
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "Data": 
        ///     {
        ///         [inventoryResultClass]
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
        [Route("add_inventoryResult")]
        [HttpPost]
        public string POST_add_inventoryResult(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "add_inventoryResult";
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


                SQLControl sQLControl_inventoryResult = new SQLControl(Server, DB, new enum_inventoryResult().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inventoryResult_content = new SQLControl(Server, DB, new enum_inventoryResult_content().GetEnumDescription(), UserName, Password, Port, SSLMode);
                inventoryResultClass inventoryResultClass = returnData.Data.ObjToClass<inventoryResultClass>();
                if (inventoryResultClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[retuen.Data] 須為[inventoryResultClass]";
                    return returnData.JsonSerializationt();
                }
                DateTime dateTime = DateTime.Now;
                inventoryResultClass.GUID = Guid.NewGuid().ToString();
                inventoryResultClass.加入時間 = dateTime.ToDateTimeString_6();
                for (int i = 0; i < inventoryResultClass.Contents.Count; i++)
                {
                    inventoryResultClass.Contents[i].GUID = Guid.NewGuid().ToString();
                    inventoryResultClass.Contents[i].Master_GUID = inventoryResultClass.GUID;
                    int 庫存量 = inventoryResultClass.Contents[i].盤點日庫存.StringToInt32();
                    int 盤點量 = inventoryResultClass.Contents[i].盤點量.StringToInt32();
                    int 異動量 = 盤點量 - 庫存量;
                    inventoryResultClass.Contents[i].異動量 = 異動量.ToString();
                    inventoryResultClass.Contents[i].加入時間 = dateTime.ToDateTimeString_6();
                }

                object[] list_inventoryResult = inventoryResultClass.ClassToSQL<inventoryResultClass , enum_inventoryResult>();
                List<object[]> list_inventoryResult_content = inventoryResultClass.Contents.ClassToSQL<inventoryResult_content, enum_inventoryResult_content>();
                sQLControl_inventoryResult.AddRow(null, list_inventoryResult);
                sQLControl_inventoryResult_content.AddRows(null, list_inventoryResult_content);
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功新增<{list_inventoryResult_content.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"inventoryResult");
                Logger.Log($"inventoryResult", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"inventoryResult");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"inventoryResult", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 刪除盤點結果
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///        "GUID"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("delete_inventoryResult_by_GUID")]
        [HttpPost]
        public string POST_delete_inventoryResult_by_GUID(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "delete_inventoryResult_by_GUID";
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
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 為 null";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];

                SQLControl sQLControl_inventoryResult = new SQLControl(Server, DB, new enum_inventoryResult().GetEnumDescription(), UserName, Password, Port, SSLMode);
                SQLControl sQLControl_inventoryResult_content = new SQLControl(Server, DB, new enum_inventoryResult_content().GetEnumDescription(), UserName, Password, Port, SSLMode);

                List<object[]> list_inventoryResult = sQLControl_inventoryResult.GetRowsByDefult(null, (int)enum_inventoryResult.GUID, GUID);
                if(list_inventoryResult.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }

                sQLControl_inventoryResult.DeleteByDefult(null, (int)enum_inventoryResult.GUID, GUID);
                sQLControl_inventoryResult_content.DeleteByDefult(null, (int)enum_inventoryResult_content.Master_GUID, GUID);
                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 刪除成功";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"inventoryResult");
                Logger.Log($"inventoryResult", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"inventoryResult");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"inventoryResult", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }

    }
}
