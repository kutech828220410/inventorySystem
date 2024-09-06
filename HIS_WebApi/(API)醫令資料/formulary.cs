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
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class formulary : Controller
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
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(formularyClass))]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
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
        /// 新增處方集
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///       [formularyClass(陣列)]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            GET_init(returnData);
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;
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
                SQLControl sQLControl_處方集資料 = new SQLControl(Server, DB, new enum_formulary().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<medClass> medClasses = medClass.get_med_cloud("http://127.0.0.1:4433");
                List<medClass> medClasses_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_med_cloud =  medClasses.CoverToDictionaryByCode();
                List<formularyClass> formularyClasses = returnData.Data.ObjToClass<List<formularyClass>>();
                List<formularyClass> formularyClasses_buf = new List<formularyClass>();
                for (int i = 0; i < formularyClasses.Count; i++)
                {
                    formularyClasses[i].GUID = Guid.NewGuid().ToString();
                    medClasses_buf = keyValuePairs_med_cloud.SortDictionaryByCode(formularyClasses[i].藥碼);
                    if(medClasses_buf.Count > 0)
                    {
                        formularyClasses[i].新增時間 = DateTime.Now.ToDateTimeString_6();
                        formularyClasses[i].藥名 = medClasses_buf[0].藥品名稱;
                        formularyClasses[i].單位 = medClasses_buf[0].包裝單位;
                        formularyClasses[i].中西藥 = medClasses_buf[0].中西藥;

                        if (formularyClasses[i].中西藥.StringIsEmpty()) formularyClasses[i].中西藥 = "西藥";
                        if (formularyClasses[i].數量.StringIsDouble() == false) continue;
                        if (formularyClasses[i].名稱.StringIsEmpty() == true) continue;
                        formularyClasses_buf.Add(formularyClasses[i]);

                    }
                }
                List<object[]> list_value = formularyClasses_buf.ClassToSQL<formularyClass, enum_formulary>();
  

                sQLControl_處方集資料.AddRows(null, list_value);

              

                returnData.Code = 200;
                returnData.Result = $"新增處方集資料,共<{formularyClasses_buf.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = formularyClasses_buf;
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
        /// 取得所有處方集資料
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
        ///       
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_all")]
        [HttpPost]
        public string POST_get_all([FromBody] returnData returnData)
        {
            GET_init(returnData);
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_all";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;
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
                SQLControl sQLControl_處方集資料 = new SQLControl(Server, DB, new enum_formulary().GetEnumDescription(), UserName, Password, Port, SSLMode);
            
                List<object[]> list_value = sQLControl_處方集資料.GetAllRows(null);

                List<formularyClass> formularyClasses = list_value.SQLToClass<formularyClass,enum_formulary>();



                returnData.Code = 200;
                returnData.Result = $"取得處方集資料,共<{formularyClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = formularyClasses;
                return returnData.JsonSerializationt();
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

            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_formulary());
            return table.JsonSerializationt(true);
        }

    }
}
