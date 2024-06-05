using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Drawing;
using System.Text;
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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using DrawingClass;


namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class drugDispatch : Controller
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
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(drugDispatchClass))]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
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
        /// 新增調撥資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "A5",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        [drugDispatchClass]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
           
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                GET_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
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

                List<drugDispatchClass> drugDispatchClasses = returnData.Data.ObjToClass<List<drugDispatchClass>>();
                List<drugDispatchClass> drugDispatchClasses_buf = new List<drugDispatchClass>();
                if (drugDispatchClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常!";
                    return returnData.JsonSerializationt();
                }

                for (int i = 0; i < drugDispatchClasses.Count; i++)
                {
                    if (drugDispatchClasses[i].藥碼.StringIsEmpty()) continue;
                    drugDispatchClasses[i].GUID = Guid.NewGuid().ToString();
                    drugDispatchClasses[i].產出時間 = DateTime.Now.ToDateTimeString();
                    drugDispatchClasses[i].過帳時間 = DateTime.MinValue.ToDateTimeString();
                    drugDispatchClasses[i].出庫庫存 = "";
                    drugDispatchClasses[i].入庫庫存 = "";
                    drugDispatchClasses[i].出庫結存 = "";
                    drugDispatchClasses[i].入庫結存 = "";
                    drugDispatchClasses[i].狀態 = "等待過帳";
                    drugDispatchClasses_buf.Add(drugDispatchClasses[i]);
                }
                List<object[]> list_value = drugDispatchClasses_buf.ClassToSQL<drugDispatchClass, enum_drugDispatch>();
                Table table = new Table(new enum_drugDispatch());
                SQLControl sQLControl_drugDispatch = new SQLControl(Server, DB, table.TableName, UserName, Password, Port, SSLMode);
                sQLControl_drugDispatch.AddRows(null, list_value);
                returnData.Code = 200;
                returnData.Result = $"新增調撥資料共<{list_value.Count}>筆";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = drugDispatchClasses_buf;
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
            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_drugDispatch());
            return table.JsonSerializationt(true);
        }
    }
}
