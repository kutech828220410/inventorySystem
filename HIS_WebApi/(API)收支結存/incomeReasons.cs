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
    [Route("api/[controller]")]
    [ApiController]
    public class incomeReasonsController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "A5",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///     
        ///     }
        ///   }
        ///   
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [HttpPost]
        public string POST_init(returnData returnData)
        {
            try
            {
                returnData.Method = "init";
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch(Exception e)
            {
                return e.Message;
            }
        
        }
        /// <summary>
        /// 取得收支原因
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "A5",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///     
        ///     }
        ///   }
        ///   
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("data")]
        [HttpPost]
        public string POST_data(returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "data";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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

                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "incomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<incomeReasonsClass> list_incomeReasonsClass = list_value.SQLToClass<incomeReasonsClass, enum_incomeReasons>();
                list_incomeReasonsClass.Sort(new ICP_by_CT_TIME());
                returnData.Code = 200;
                returnData.Data = list_incomeReasonsClass;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"取得收支原因成功!共<{list_value.Count}>筆";
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = e.Message;
            }
            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 新增收支原因
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "A5",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        [incomeReasonsClassAry]
        ///     }
        ///   }
        ///   
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "add";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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

                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "incomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = returnData.Data.ObjToListSQL<incomeReasonsClass, enum_incomeReasons>();
                for (int i = 0; i < list_value.Count; i++)
                {
                    list_value[i][(int)enum_incomeReasons.GUID] = Guid.NewGuid().ToString();
                    list_value[i][(int)enum_incomeReasons.新增時間] = DateTime.Now.ToDateTimeString_6();
                }
                sQLControl.AddRows(null, list_value);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"新增收支原因成功!共<{list_value.Count}>筆";
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = e.Message;
            }
            finally
            {
                
            }

            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以GUID更新收支原因
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "A5",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        [incomeReasonsClassAry]
        ///     }
        ///   }
        ///   
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        public string POST_update([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "update";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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

                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "incomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = returnData.Data.ObjToListSQL<incomeReasonsClass, enum_incomeReasons>();
                sQLControl.UpdateByDefulteExtra(null, list_value);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"更新收支原因成功!共<{list_value.Count}>筆";
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = e.Message;
            }
            finally
            {

            }

            return returnData.JsonSerializationt(true);
        }
        /// <summary>
        /// 以GUID刪除收支原因
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "A5",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        [incomeReasonsClassAry]
        ///     }
        ///   }
        ///   
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public string POST_delete([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "delete";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "incomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = returnData.Data.ObjToListSQL<incomeReasonsClass, enum_incomeReasons>();
                sQLControl.DeleteExtra(null, list_value);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"刪除收支原因成功!共<{list_value.Count}>筆";
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = e.Message;
            }
            finally
            {

            }

            return returnData.JsonSerializationt(true);
        }


        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_incomeReasons());
            return table.JsonSerializationt(true);
        }
        private class ICP_by_CT_TIME : IComparer<incomeReasonsClass>
        {
            public int Compare(incomeReasonsClass x, incomeReasonsClass y)
            {
                string Code0 = y.新增時間;
                string Code1 = x.新增時間;
                return Code1.CompareTo(Code0);
            }
        }
    }
}
