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
    public class order : Controller
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
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(OrderClass))]
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
        /// 以開方時間取得西藥醫令
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
        ///       "起始時間",
        ///       "結束時間"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_rx_time_st_end")]
        [HttpPost]
        public string POST_get_by_rx_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_rx_time_st_end";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];

                if (!起始時間.Check_Date_String() || !結束時間.Check_Date_String())
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                DateTime date_st = 起始時間.StringToDateTime();
                DateTime date_end = 結束時間.StringToDateTime();

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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByBetween(null, (int)enum_醫囑資料.開方日期, date_st.ToDateTimeString(), date_end.ToDateTimeString());
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();

                OrderClasses.sort(OrderClassMethod.SortType.產出時間);

                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses;
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
        /// 以過帳時間取得西藥醫令
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
        ///       "起始時間",
        ///       "結束時間"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_post_time_st_end")]
        [HttpPost]
        public string POST_get_by_op_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_op_time_st_end";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];

                if (!起始時間.Check_Date_String() || !結束時間.Check_Date_String())
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                DateTime date_st = 起始時間.StringToDateTime();
                DateTime date_end = 結束時間.StringToDateTime();

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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByBetween(null, (int)enum_醫囑資料.產出時間, date_st.ToDateTimeString(), date_end.ToDateTimeString());
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                OrderClasses.sort(OrderClassMethod.SortType.產出時間);
                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses;
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
        /// 以PRI_KEY取得西藥醫令
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
        ///       "PRI_KEY",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_pri_key")]
        [HttpPost]
        public string POST_get_by_pri_key([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_pri_key";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[PRI_KEY]";
                    return returnData.JsonSerializationt(true);
                }
                string PRI_KEY = returnData.ValueAry[0];



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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_醫囑資料.PRI_KEY, PRI_KEY);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                OrderClasses.sort(OrderClassMethod.SortType.批序);
                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses;
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
        /// 以GUID取得西藥醫令
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
        ///       "GUID",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_guid")]
        [HttpPost]
        public string POST_get_by_guid([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_guid";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


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
                string GUID = returnData.ValueAry[0];



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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_醫囑資料.GUID, GUID);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses[0];
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
        /// 以領藥號取得西藥醫令
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
        ///       "領藥號",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_MED_BAG_NUM")]
        [HttpPost]
        public string POST_get_by_MED_BAG_NUM([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_MED_BAG_NUM";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[領藥號]";
                    return returnData.JsonSerializationt(true);
                }
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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByLike(null, (int)enum_醫囑資料.領藥號, returnData.ValueAry[0]);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    returnData.Data = new List<OrderClass>();
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses;
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
        /// 以病歷號取得西藥醫令
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
        ///       "病歷號",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_PATCODE")]
        [HttpPost]
        public string POST_get_by_PATCODE([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_PATCODE";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[領藥號]";
                    return returnData.JsonSerializationt(true);
                }
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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByLike(null, (int)enum_醫囑資料.病歷號, returnData.ValueAry[0]);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    returnData.Data = new List<OrderClass>();
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses;
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
        /// 以病人姓名取得西藥醫令
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
        ///       "病人姓名",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_PATNAME")]
        [HttpPost]
        public string POST_get_by_PATNAME([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_PATNAME";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[領藥號]";
                    return returnData.JsonSerializationt(true);
                }
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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByLike(null, (int)enum_醫囑資料.病人姓名, returnData.ValueAry[0]);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Data = new List<OrderClass>();
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses;
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
        /// 以GUID更新西藥醫令
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///       [OrderClass(陣列)]
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
        [Route("updete_by_guid")]
        [HttpPost]
        public string POST_updete_by_guid([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "updete_by_guid";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<OrderClass> OrderClasses = returnData.Data.ObjToClass<List<OrderClass>>();
                List<object[]> list_value = OrderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
                sQLControl_醫令資料.UpdateByDefulteExtra(null, list_value);

                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"更新西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses[0];
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
        /// 新增西藥醫令
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///       [OrderClass(陣列)]
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
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<OrderClass> OrderClasses = returnData.Data.ObjToClass<List<OrderClass>>();
                for (int i = 0; i < OrderClasses.Count; i++)
                {
                    OrderClasses[i].GUID = Guid.NewGuid().ToString();
                    OrderClasses[i].產出時間 = DateTime.Now.ToDateTimeString_6();
                    OrderClasses[i].過帳時間 = DateTime.MinValue.ToDateTimeString();
                    OrderClasses[i].狀態 = "未過帳";
 
                }
                List<object[]> list_value = OrderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
                sQLControl_醫令資料.AddRows(null, list_value);

                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"新增西藥醫令!共<{OrderClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses;
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
        /// 以領藥號取得西藥醫令病患列表
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
        ///       "領藥號",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_header_by_MED_BAG_NUM")]
        [HttpPost]
        public string POST_header_by_MED_BAG_NUM([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "header_by_MED_BAG_NUM";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[領藥號]";
                    return returnData.JsonSerializationt(true);
                }
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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByLike(null, (int)enum_醫囑資料.領藥號, returnData.ValueAry[0]);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                List<OrderClass> OrderClasses_buf = new List<OrderClass>();
                List<OrderClass> OrderClasses_result = new List<OrderClass>();
                List<string> list_PRI_KEY = (from temp in OrderClasses
                                             select temp.PRI_KEY).Distinct().ToList();
                var keyValuePairs = OrderClasses.CoverToDictionaryBy_PRI_KEY();

                for (int i = 0; i < list_PRI_KEY.Count; i++)
                {
                    string PRI_KEY = list_PRI_KEY[i];
                    string 調劑完成 = "Y";
                    OrderClasses_buf = keyValuePairs.SortDictionaryBy_PRI_KEY(list_PRI_KEY[i]);
                    for (int k = 0; k < OrderClasses_buf.Count; k++)
                    {
                        if (OrderClasses_buf[k].實際調劑量.StringIsDouble() == false)
                        {
                            調劑完成 = "N";
                        }
                    }
                    if (OrderClasses_buf.Count > 0)
                    {
                        OrderClasses_buf[0].住院序號 = "";
                        OrderClasses_buf[0].備註 = "";
                        OrderClasses_buf[0].劑量單位 = "";
                        OrderClasses_buf[0].單次劑量 = "";
                        OrderClasses_buf[0].天數 = "";
                        OrderClasses_buf[0].實際調劑量 = "";
                        OrderClasses_buf[0].就醫序號 = "";
                        OrderClasses_buf[0].就醫類別 = "";
                        OrderClasses_buf[0].展藥時間 = "";
                        OrderClasses_buf[0].床號 = "";
                        OrderClasses_buf[0].批序 = "";
                        OrderClasses_buf[0].核對ID = "";
                        OrderClasses_buf[0].核對姓名 = "";
                        OrderClasses_buf[0].藥品名稱 = "";
                        OrderClasses_buf[0].藥品碼 = "";
                        OrderClasses_buf[0].藥局代碼 = "";
                        OrderClasses_buf[0].藥師ID = "";
                        OrderClasses_buf[0].藥師姓名 = "";
                        OrderClasses_buf[0].藥袋條碼 = "";
                        OrderClasses_buf[0].藥袋類型 = "";
                        OrderClasses_buf[0].費用別 = "";
                        OrderClasses_buf[0].途徑 = "";
                        OrderClasses_buf[0].醫師代碼 = "";
                        OrderClasses_buf[0].狀態 = 調劑完成;
                        OrderClasses_result.Add(OrderClasses_buf[0]);
                    }

                }


                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    returnData.Data = new List<OrderClass>();
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses_result.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses_result;
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
        /// 以病歷號取得西藥醫令病患列表
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
        ///       "病歷號",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_header_by_PATCODE")]
        [HttpPost]
        public string POST_get_header_by_PATCODE([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_header_by_PATCODE";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;


                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[病歷號]";
                    return returnData.JsonSerializationt(true);
                }
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
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByLike(null, (int)enum_醫囑資料.病歷號, returnData.ValueAry[0]);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                List<OrderClass> OrderClasses_buf = new List<OrderClass>();
                List<OrderClass> OrderClasses_result = new List<OrderClass>();
                List<string> list_PRI_KEY = (from temp in OrderClasses
                                             select temp.PRI_KEY).Distinct().ToList();
                var keyValuePairs = OrderClasses.CoverToDictionaryBy_PRI_KEY();

                for (int i = 0; i < list_PRI_KEY.Count; i++)
                {
                    string PRI_KEY = list_PRI_KEY[i];
                    string 調劑完成 = "Y";
                    OrderClasses_buf = keyValuePairs.SortDictionaryBy_PRI_KEY(list_PRI_KEY[i]);
                    for (int k = 0; k < OrderClasses_buf.Count; k++)
                    {
                        if (OrderClasses_buf[k].實際調劑量.StringIsDouble() == false)
                        {
                            調劑完成 = "N";
                        }
                    }
                    if (OrderClasses_buf.Count > 0)
                    {
                        OrderClasses_buf[0].住院序號 = "";
                        OrderClasses_buf[0].備註 = "";
                        OrderClasses_buf[0].劑量單位 = "";
                        OrderClasses_buf[0].單次劑量 = "";
                        OrderClasses_buf[0].天數 = "";
                        OrderClasses_buf[0].實際調劑量 = "";
                        OrderClasses_buf[0].就醫序號 = "";
                        OrderClasses_buf[0].就醫類別 = "";
                        OrderClasses_buf[0].展藥時間 = "";
                        OrderClasses_buf[0].床號 = "";
                        OrderClasses_buf[0].批序 = "";
                        OrderClasses_buf[0].核對ID = "";
                        OrderClasses_buf[0].核對姓名 = "";
                        OrderClasses_buf[0].藥品名稱 = "";
                        OrderClasses_buf[0].藥品碼 = "";
                        OrderClasses_buf[0].藥局代碼 = "";
                        OrderClasses_buf[0].藥師ID = "";
                        OrderClasses_buf[0].藥師姓名 = "";
                        OrderClasses_buf[0].藥袋條碼 = "";
                        OrderClasses_buf[0].藥袋類型 = "";
                        OrderClasses_buf[0].費用別 = "";
                        OrderClasses_buf[0].途徑 = "";
                        OrderClasses_buf[0].醫師代碼 = "";
                        OrderClasses_buf[0].狀態 = 調劑完成;
                        OrderClasses_result.Add(OrderClasses_buf[0]);
                    }

                }


                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    returnData.Data = new List<OrderClass>();
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令!共<{OrderClasses_result.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = OrderClasses_result;
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

            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_醫囑資料());               
            return table.JsonSerializationt(true);
        }
    }
}
