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
    public class ServerSettingController : Controller
    {
        static private string Server = ConfigurationManager.AppSettings["server"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string DB = "dbvm";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 取得所有連線資訊
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpGet]
        public string GET()
        {
            returnData returnData = new returnData();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "get";

            try
            {
                this.CheckCreatTable();
                SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);

                List<ServerSettingClass> serverSettingClasses = list_value.SQLToClass<ServerSettingClass, enum_ServerSetting>();

                returnData.Code = 200;
                returnData.Data = serverSettingClasses;
                returnData.Result = $"取得伺服器設定成功!共<{serverSettingClasses.Count}>筆";
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        /// 
        [Route("init")]
        [HttpGet]
        public string GET_init()
        {
            returnData returnData = new returnData();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "GET_init";
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

            }
            try
            {
                return CheckCreatTable();
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
        [Route("post_init")]
        [HttpPost]
        public string POST_init([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);

            try
            {
                try
                {
                    returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
                }
                catch
                {

                }
                returnData.Method = "POST_init";
         
                return CheckCreatTable();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得Type(調劑台、藥庫...)
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("type")]
        [HttpGet]
        public string GET_type()
        {
            return new enum_ServerSetting_Type().GetEnumNames().JsonSerializationt();
        }
        /// <summary>
        /// 取得程式類別(SQLServer、API、WEB...)
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("program")]
        [HttpGet]
        public string GET_program()
        {
            return new enum_ServerSetting_ProgramType().GetEnumNames().JsonSerializationt();
        }
        /// <summary>
        /// 新增連線資訊
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [List<serverSettingClasses>]
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
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

            }
            returnData.Method = "add";
            try
            {
                this.CheckCreatTable();
                SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_returnData = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();
      
                List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
                list_value_returnData = serverSettingClasses.ClassToSQL<ServerSettingClass, enum_ServerSetting>();
                for (int i = 0; i < list_value_returnData.Count; i++)
                {
                    string 名稱 = list_value_returnData[i][(int)enum_ServerSetting.設備名稱].ObjectToString();
                    string 類別 = list_value_returnData[i][(int)enum_ServerSetting.類別].ObjectToString();
                    string 程式類別 = list_value_returnData[i][(int)enum_ServerSetting.程式類別].ObjectToString();
                    string 內容 = list_value_returnData[i][(int)enum_ServerSetting.內容].ObjectToString();

                    list_value_buf = list_value.GetRows((int)enum_ServerSetting.設備名稱, 名稱);
                    list_value_buf = list_value_buf.GetRows((int)enum_ServerSetting.類別, 類別);
                    list_value_buf = list_value_buf.GetRows((int)enum_ServerSetting.程式類別, 程式類別);
                    list_value_buf = list_value_buf.GetRows((int)enum_ServerSetting.內容, 內容);
                    if (list_value_buf.Count == 0)
                    {
                        object[] value = list_value_returnData[i];
                        value[(int)enum_ServerSetting.GUID] = Guid.NewGuid().ToString();
                        list_value_add.Add(value);
                    }
                    else
                    {
                        object[] value = list_value_returnData[i];
                        value[(int)enum_ServerSetting.GUID] = list_value_buf[0][(int)enum_ServerSetting.GUID].ObjectToString();
                        list_value_replace.Add(value);
                    }
                }
                sQLControl.AddRows(null, list_value_add);
                sQLControl.UpdateByDefulteExtra(null, list_value_replace);

                returnData.Code = 200;
                returnData.Result = "新增伺服器資料成功!";
                returnData.Data = serverSettingClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            
        }
        /// <summary>
        /// 刪除連線資訊
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [List<serverSettingClasses>]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public string POST_delete([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

            }
            returnData.Method = "delete";
            try
            {
                this.CheckCreatTable();
                SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_returnData = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();
                List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
                list_value_returnData = serverSettingClasses.ClassToSQL<ServerSettingClass, enum_ServerSetting>();

                sQLControl.DeleteExtra(null, list_value_returnData);

                returnData.Code = 200;
                returnData.Result = "刪除伺服器資料成功!";
                returnData.Data = serverSettingClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }      
        }
        /// <summary>
        /// 以Type取得連線資訊(調劑台、藥庫...)
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
        ///       "調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_serversetting_by_type")]
        [HttpPost]
        public string POST_get_serversetting_by_type([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

            }
            returnData.Method = "get_serversetting_by_type";
            try
            {
                this.CheckCreatTable();
                SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_returnData = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[Type]";
                    return returnData.JsonSerializationt(true);
                }
                string Type = returnData.ValueAry[0];

                List<ServerSettingClass> serverSettingClasses = GetAllServerSetting();
                serverSettingClasses = (from temp in serverSettingClasses
                                        where temp.類別 == Type
                                        where temp.內容 == "一般資料"
                                        select temp).ToList();
        

                returnData.Code = 200;
                returnData.Result = $"取得連線資訊,共<{serverSettingClasses.Count}>筆";
                returnData.Data = serverSettingClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }

        }
        /// <summary>
        /// 取得指定Type(調劑台、藥庫...)伺服器資訊的服務單位
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
        ///       "調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_department_type")]
        [HttpPost]
        public string POST_gget_department_type([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);

            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

            }
        
            returnData.Method = "get_department_type";
            try
            {
                this.CheckCreatTable();
                SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_returnData = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[Type]";
                    return returnData.JsonSerializationt(true);
                }
                string Type = returnData.ValueAry[0];

                List<ServerSettingClass> serverSettingClasses = GetAllServerSetting();
                List<string> department_types = (from temp in serverSettingClasses
                                                 where temp.類別 == Type
                                                 where temp.內容 == "一般資料"
                                                 select temp.單位).Distinct().ToList();

                department_types.Remove("");
                returnData.Code = 200;
                returnData.Result = $"取得伺服器服務端未,共<{department_types.Count}>筆種類";
                returnData.Data = department_types;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }

        }
        /// <summary>
        /// 以單位別取得連線資訊(門診、急診、住院、藥庫...)
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
        ///       "門診"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_serversetting_by_department_type")]
        [HttpPost]
        public string POST_get_serversetting_by_department_type([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

            }
            returnData.Method = "get_serversetting_by_department_type";
            try
            {
                this.CheckCreatTable();
                SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_returnData = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[Type]";
                    return returnData.JsonSerializationt(true);
                }
                string department_type = returnData.ValueAry[0];

                List<ServerSettingClass> serverSettingClasses = GetAllServerSetting();
                serverSettingClasses = (from temp in serverSettingClasses
                                        where temp.單位 == department_type
                                        where temp.內容 == "一般資料"
                                        select temp).ToList();


                returnData.Code = 200;
                returnData.Result = $"取得連線資訊,共<{serverSettingClasses.Count}>筆";
                returnData.Data = serverSettingClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }

        }

        private string CheckCreatTable()
        {
            ServerSettingClass serverSettingClass = new ServerSettingClass();
            serverSettingClass.Server = Server;
            serverSettingClass.Port = Port.ToString();
            serverSettingClass.User = UserName;
            serverSettingClass.Password = Password;
            serverSettingClass.DBName = DB;

            return CheckCreatTable(serverSettingClass);
        }
        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_ServerSetting());
            return table.JsonSerializationt(true);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<ServerSettingClass> GetAllServerSetting()
        {
            SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
            List<object[]> list_value = sQLControl.GetAllRows(null);

            List<ServerSettingClass> serverSettingClasses = list_value.SQLToClass<ServerSettingClass, enum_ServerSetting>();
            return serverSettingClasses;
        }
    }
}
