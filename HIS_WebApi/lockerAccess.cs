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
namespace HIS_WebApi
{
    public class lockerAccess : Controller
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
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(lockerAccessClass))]
        [Route("init")]
        [HttpPost]
        public string POST_init([FromBody] returnData returnData)
        {
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
        /// 新增鎖控權限資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [lockerAccess]
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
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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



                List<lockerAccessClass> lockerAccessClasses = returnData.Data.ObjToClass<List<lockerAccessClass>>();
                List<lockerAccessClass> lockerAccessClasses_add = new List<lockerAccessClass>();
                List<lockerAccessClass> lockerAccessClasses_update = new List<lockerAccessClass>();

                if (lockerAccessClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤";
                    return returnData.JsonSerializationt();
                }
                SQLControl sQLControl_lockerAccess = new SQLControl(Server, DB, new enum_lockerAccess().GetEnumDescription(), UserName, Password, Port, SSLMode);
                for (int i = 0; i < lockerAccessClasses.Count; i++)
                {
                    lockerAccessClass lockerAccessClass = lockerAccessClasses[i];
                    string ID = lockerAccessClass.ID;
                    string lock_name = lockerAccessClass.鎖控名稱;
                    string lock_period = lockerAccessClass.鎖控可開啟時段;
                    string[] serch_name = new string[] { enum_lockerAccess.ID.GetEnumName(), enum_lockerAccess.鎖控名稱.GetEnumName() };
                    string[] serch_value = new string[] { ID, lock_name };
                    if (lockerAccessClass.IsValidTimePeriodString(lock_period) == false) continue;
                    if (lock_name.StringIsEmpty()) continue;
                    if (lock_period.StringIsEmpty()) continue;

                    List<object[]> list_value = sQLControl_lockerAccess.GetRowsByDefult(null, serch_name, serch_value);
       
                    if (list_value.Count == 0)
                    {
                        lockerAccessClass.GUID = Guid.NewGuid().ToString();
                        lockerAccessClass.ID = ID;
                        lockerAccessClass.鎖控名稱 = lock_name;
                        lockerAccessClass.鎖控可開啟時段 = lock_name;

                        lockerAccessClasses_add.Add(lockerAccessClass);
                    }
                    else
                    {
                        lockerAccessClass.ID = ID;
                        lockerAccessClass.鎖控名稱 = lock_name;
                        lockerAccessClass.鎖控可開啟時段 = lock_name;

                        lockerAccessClasses_update.Add(lockerAccessClass);

                    }
                }
                List<object[]> list_value_add = lockerAccessClasses_add.ClassToSQL<lockerAccessClass, enum_lockerAccess>();
                List<object[]> list_value_update = lockerAccessClasses_update.ClassToSQL<lockerAccessClass, enum_lockerAccess>();
                sQLControl_lockerAccess.AddRows(null, list_value_add);
                sQLControl_lockerAccess.UpdateByDefulteExtra(null, list_value_update);
                returnData.Result = $"成功,共新增<{lockerAccessClasses_add.Count}>筆資料,共更新<{lockerAccessClasses_update.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"lockerAccessClass");
                Logger.Log($"lockerAccessClass", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"lockerAccessClass");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"lockerAccessClass", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }

        /// <summary>
        /// 以ID及名稱取得鎖控權限
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
        ///        "ID",
        ///        "lc_name"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_id_and_lcname")]
        [HttpPost]
        public string POST_get_by_id_and_lcname([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_by_id_and_lcname";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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

                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[ID][鎖控名稱]";
                    return returnData.JsonSerializationt(true);
                }

                string ID = returnData.ValueAry[0];
                string 鎖控名稱 = returnData.ValueAry[1];

                string[] serch_name = new string[] { enum_lockerAccess.ID.GetEnumName(), enum_lockerAccess.鎖控名稱.GetEnumName() };
                string[] serch_value = new string[] { ID, 鎖控名稱 };
                SQLControl sQLControl_lockerAccess = new SQLControl(Server, DB, new enum_lockerAccess().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_lockerAccess.GetRowsByDefult(null, serch_name, serch_value);

                lockerAccessClass lockerAccessClass = null;

                if(list_value.Count > 0)
                {
                    lockerAccessClass = list_value[0].SQLToClass<lockerAccessClass, enum_lockerAccess>();
                }


                if (lockerAccessClass != null) returnData.Result = $"成功取得資料";
                else returnData.Result = $"找無資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = lockerAccessClass;
                returnData.Code = 200;
                Logger.LogAddLine($"lockerAccessClass");
                Logger.Log($"lockerAccessClass", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"lockerAccessClass");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"lockerAccessClass", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_lockerAccess());
            return table.JsonSerializationt(true);
        }
    }
}
