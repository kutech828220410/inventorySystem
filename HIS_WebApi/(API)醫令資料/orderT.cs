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
    public class orderT : Controller
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
        /// 以開方時間取得中藥醫令
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
                string TableName = "ordert_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByBetween(null, (int)enum_OrderT.開方日期, date_st.ToDateTimeString(), date_end.ToDateTimeString());
                List<OrderTClass> orderTClasses = list_value_buf.SQLToClass<OrderTClass, enum_OrderT>();

                orderTClasses.sort(OrderTClassMethod.SortType.產出時間);

                returnData.Code = 200;
                returnData.Result = $"取得中藥醫令!共<{orderTClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = orderTClasses;
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
        /// 以過帳時間取得中藥醫令
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
                string TableName = "ordert_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByBetween(null, (int)enum_OrderT.產出時間, date_st.ToDateTimeString(), date_end.ToDateTimeString());
                List<OrderTClass> orderTClasses = list_value_buf.SQLToClass<OrderTClass, enum_OrderT>();
                orderTClasses.sort(OrderTClassMethod.SortType.產出時間);
                returnData.Code = 200;
                returnData.Result = $"取得中藥醫令!共<{orderTClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = orderTClasses;
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
        /// 以PRI_KEY取得中藥醫令
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
                string TableName = "ordert_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_OrderT.PRI_KEY, PRI_KEY);
                List<OrderTClass> orderTClasses = list_value_buf.SQLToClass<OrderTClass, enum_OrderT>();
                orderTClasses.sort(OrderTClassMethod.SortType.批序);
                returnData.Code = 200;
                returnData.Result = $"取得中藥醫令!共<{orderTClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = orderTClasses;
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
        /// 以GUID取得中藥醫令
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
                    returnData.Result = $"returnData.ValueAry 內容應為[PRI_KEY]";
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
                string TableName = "ordert_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_OrderT.GUID, GUID);
                List<OrderTClass> orderTClasses = list_value_buf.SQLToClass<OrderTClass, enum_OrderT>();
                if(orderTClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得中藥醫令!共<{orderTClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = orderTClasses[0];
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
        /// 以GUID取得中藥醫令
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///       [OrderTClass(陣列)]
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
                string TableName = "ordert_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<OrderTClass> orderTClasses = returnData.Data.ObjToClass<List<OrderTClass>>();
                List<object[]> list_value = orderTClasses.ClassToSQL<OrderTClass, enum_OrderT>();
                sQLControl_醫令資料.UpdateByDefulteExtra(null, list_value);

                if (orderTClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"取得中藥醫令!共<{orderTClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = orderTClasses[0];
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

            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_OrderT());
            return table.JsonSerializationt(true);
        }


      
    }
}
