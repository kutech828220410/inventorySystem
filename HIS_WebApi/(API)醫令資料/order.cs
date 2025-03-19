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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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

                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
        public string POST_get_by_post_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_post_time_st_end";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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

                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByBetween(null, (int)enum_醫囑資料.過帳時間, date_st.ToDateTimeString(), date_end.ToDateTimeString());
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
        /// 以產出時間取得西藥醫令
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
        [Route("get_by_creat_time_st_end")]
        [HttpPost]
        public string POST_get_by_creat_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_creat_time_st_end";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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

                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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



                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
        /// 以藥袋條碼取得西藥醫令
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
        ///       "藥袋條碼",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_barcode")]
        [HttpPost]
        public string POST_get_by_barcode([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_barcode";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                    returnData.Result = $"returnData.ValueAry 內容應為[藥袋條碼]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_醫囑資料.藥袋條碼, returnData.ValueAry[0]);
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
        /// 以藥碼取得西藥醫令
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
        ///       "藥碼",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_drugcode")]
        [HttpPost]
        public string POST_get_by_drugcode([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_drugcode";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_醫囑資料.藥品碼, returnData.ValueAry[0]);
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
        /// 以藥名取得西藥醫令
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
        ///       "藥名",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_drugname")]
        [HttpPost]
        public string POST_get_by_drugname([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_drugname";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                    returnData.Result = $"returnData.ValueAry 內容應為[藥名]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByLike(null, (int)enum_醫囑資料.藥品名稱, $"{returnData.ValueAry[0]}%");
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
        /// <summary>
        /// 以病房號取得西藥醫令
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
        ///       "病房號",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        public string POST_get_by_PATNAME([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_PATNAME";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
        [Route("get_by_ward")]
        [HttpPost]
        public string POST_get_by_ward([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_ward";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                    returnData.Result = $"returnData.ValueAry 內容應為[病房號]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_醫囑資料.病房, returnData.ValueAry[0]);
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
        /// 以領藥號和日期取得西藥醫令
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
        ///       "領藥號","日期",
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_BagNum_day")]
        [HttpPost]
        public string POST_get_by_BagNum_day([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_BagNum_day";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                    returnData.Result = $"returnData.ValueAry 內容應為[領藥號,日期]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_醫囑資料.領藥號, returnData.ValueAry[0]);
                List<OrderClass> OrderClasses = list_value_buf.SQLToClass<OrderClass, enum_醫囑資料>();
                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Data = new List<OrderClass>();
                    returnData.Result = $"查無此領藥號";
                    return returnData.JsonSerializationt();
                }
                OrderClasses = OrderClasses.Where(temp => temp.產出時間 == returnData.ValueAry[1]).ToList();
                returnData.Code = 200;
                returnData.Result = $"取得西藥醫令 {returnData.ValueAry[1]} 領藥號: {returnData.ValueAry[0]}!共<{OrderClasses.Count}>筆資料";
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
        /// 以GUID刪除西藥醫令
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
        [Route("delete_by_guid")]
        [HttpPost]
        public string POST_delete_by_guid([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "delete_by_guid";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<OrderClass> OrderClasses = returnData.Data.ObjToClass<List<OrderClass>>();
                List<object[]> list_value = OrderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
                sQLControl_醫令資料.DeleteExtra(null, list_value);

                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Result = $"刪除西藥醫令!共<{OrderClasses.Count}>筆資料";
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
        /// 更新西藥醫令
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

        [HttpPost("update_UDorder_list")]
        public string update_order_list([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_UDorder_list";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                List<OrderClass> input_orderClass = returnData.Data.ObjToClass<List<OrderClass>>();
                string priKey = input_orderClass[0].PRI_KEY;
                if (input_orderClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string 病房 = input_orderClass[0].病房;

                DateTime today = DateTime.Now;
                string starttime = today.GetStartDate().ToDateString();
                string endtime = today.GetEndDate().ToDateString();
                SQLControl sQLControl_order_list = new SQLControl(Server, DB, "order_list", UserName, Password, Port, SSLMode);

                List<object[]> list_order_list = sQLControl_order_list.GetRowsByBetween(null, (int)enum_醫囑資料.展藥時間, starttime, endtime);
                List<OrderClass> sql_order_list = list_order_list.SQLToClass<OrderClass, enum_醫囑資料>();
                sql_order_list = sql_order_list.Where(temp => temp.病房 == 病房).ToList();
                List<OrderClass> add_order_list = new List<OrderClass>();
                List<OrderClass> update_order_list = new List<OrderClass>();
                List<OrderClass> result_order_list = new List<OrderClass>();
                List<OrderClass> delete_order_list = new List<OrderClass>();

                Dictionary<string, List<OrderClass>> new_orderDicPriKey = OrderClass.ToDictByPriKey(input_orderClass);
                Dictionary<string, List<OrderClass>> old_orderDicPriKey = OrderClass.ToDictByPriKey(sql_order_list);

                foreach (string PRI_KEY in new_orderDicPriKey.Keys)
                {
                    List<OrderClass> orderClass_new = OrderClass.GetByPriKey(new_orderDicPriKey, PRI_KEY);
                    List<OrderClass> orderClass_old = OrderClass.GetByPriKey(old_orderDicPriKey, PRI_KEY);              
                    foreach(OrderClass item in orderClass_new)
                    {
                        OrderClass order = orderClass_old.Where(temp => temp.批序 == item.批序).FirstOrDefault();
                        if(order == null)
                        {
                            item.GUID = Guid.NewGuid().ToString();
                            item.產出時間 = DateTime.Now.ToDateTimeString_6();
                            item.過帳時間 = DateTime.MinValue.ToDateTimeString_6();
                            item.展藥時間 = DateTime.MinValue.ToDateTimeString_6();
                            item.狀態 = "未過帳";
                            add_order_list.Add(item);
                        }
                    }
                    foreach(OrderClass item in orderClass_old)
                    {
                        OrderClass order = orderClass_new.Where(temp => temp.批序 == item.批序).FirstOrDefault();
                        if (order == null)
                        {
                            if (item.批序.Contains("DC") == false)
                            {
                                item.批序 += "-[DC]";
                                update_order_list.Add(item);
                            }
                        }
                    }
                }
                
                List<object[]> list_add_order_list = add_order_list.ClassToSQL<OrderClass, enum_醫囑資料>();
                List<object[]> list_update_order_list = add_order_list.ClassToSQL<OrderClass, enum_醫囑資料>();

                if (list_add_order_list.Count > 0) sQLControl_order_list.AddRows(null, list_add_order_list);
                if (list_update_order_list.Count > 0) sQLControl_order_list.UpdateByDefulteExtra(null, list_add_order_list);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                //returnData.Data = result_order_list;
                returnData.Result = $"取得醫令成功,共<{input_orderClass.Count}>筆,新增<{list_add_order_list.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);

            }
        }
        /// <summary>
        /// 更新西藥醫令
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

        [HttpPost("update_order_list_new")]
        public string update_order_list_new([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_order_list_new";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                List<OrderClass> input_orderClass = returnData.Data.ObjToClass<List<OrderClass>>();
                string priKey = input_orderClass[0].PRI_KEY;
                if (input_orderClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                SQLControl sQLControl_order_list = new SQLControl(Server, DB, "order_list", UserName, Password, Port, SSLMode);
                List<object[]> list_order_list = sQLControl_order_list.GetRowsByDefult(null, (int)enum_醫囑資料.PRI_KEY, priKey);
                List<OrderClass> sql_order_list = list_order_list.SQLToClass<OrderClass, enum_醫囑資料>();
                List<OrderClass> add_order_list = new List<OrderClass>();
                List<OrderClass> update_order_list = new List<OrderClass>();
                List<OrderClass> result_order_list = new List<OrderClass>();

                foreach(var orderClass in input_orderClass)
                {
                    string 批序 = orderClass.批序.Split("-")[0];
                    OrderClass orderClass_db = sql_order_list.Where(temp => temp.批序.StartsWith(批序)).FirstOrDefault();
                    if (orderClass.批序.Contains("[NEW]"))
                    {
                        if(orderClass_db == null)
                        {
                            orderClass.GUID = Guid.NewGuid().ToString();
                            orderClass.產出時間 = DateTime.Now.ToDateTimeString_6();
                            orderClass.過帳時間 = DateTime.MinValue.ToDateTimeString_6();
                            orderClass.展藥時間 = DateTime.MinValue.ToDateTimeString_6();
                            orderClass.狀態 = "未過帳";
                            if(orderClass.備註.IndexOf("[NEW]") == -1) orderClass.備註 = $"[NEW]{orderClass.備註}";
                            add_order_list.Add(orderClass);
                        }
                        else
                        {
                            result_order_list.Add(orderClass_db);
                        }
                    }
                    else if(orderClass.批序.Contains("[DC]"))
                    {
                        if(orderClass_db == null)
                        {
                            orderClass.GUID = Guid.NewGuid().ToString();
                            orderClass.產出時間 = DateTime.Now.ToDateTimeString_6();
                            orderClass.過帳時間 = DateTime.MinValue.ToDateTimeString_6();
                            orderClass.展藥時間 = DateTime.MinValue.ToDateTimeString_6();
                            orderClass.狀態 = "未過帳";
                            if (orderClass.備註.IndexOf("[DC]") == -1) orderClass.備註 = $"[DC]{orderClass.備註}";
                            add_order_list.Add(orderClass);
                        }
                        else
                        {
                            orderClass_db.批序 = orderClass.批序;
                            if (orderClass_db.備註.IndexOf("[DC]") == -1) orderClass_db.備註 = $"[DC]{orderClass_db.備註}";
                            update_order_list.Add(orderClass_db);
                            result_order_list.Add(orderClass_db);
                        }
                    }
                }

                
                List<object[]> list_add_order_list = add_order_list.ClassToSQL<OrderClass, enum_醫囑資料>();
                List<object[]> list_update_order_list = update_order_list.ClassToSQL<OrderClass, enum_醫囑資料>();

                if (list_add_order_list.Count > 0) sQLControl_order_list.AddRows(null, list_add_order_list);
                if (list_update_order_list.Count > 0) sQLControl_order_list.UpdateByDefulteExtra(null, list_add_order_list);
                result_order_list.AddRange(add_order_list);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = result_order_list;
                returnData.Result = $"取得醫令成功,共<{input_orderClass.Count}>筆,新增<{list_add_order_list.Count}>筆,更新<{list_update_order_list.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);

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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
        [Route("add_and_updete_by_guid")]
        [HttpPost]
        public string POST_add_and_updete_by_guid([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add_and_updete_by_guid";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                string serverName = returnData.ServerName;
                string serverType = returnData.ServerType;
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "order_list";
                SQLControl sQLControl_醫令資料 = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<OrderClass> OrderClasses = returnData.Data.ObjToClass<List<OrderClass>>();
                if (OrderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入醫令資料為空值";
                    return returnData.JsonSerializationt();
                }
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                for (int i = 0; i < OrderClasses.Count; i++)
                {
                    string GUID = OrderClasses[i].GUID;
                    object[] value = OrderClasses[i].ClassToSQL<OrderClass, enum_醫囑資料>();
                    list_value_buf = sQLControl_醫令資料.GetRowsByDefult(null, (int)enum_醫囑資料.GUID, GUID);
                    if(list_value_buf.Count == 0)
                    {
                        list_value_add.Add(value);
                    }
                    else
                    {
                        list_value_replace.Add(value);
                    }
                }

                sQLControl_醫令資料.AddRows(null, list_value_add);
                sQLControl_醫令資料.UpdateByDefulteExtra(null, list_value_replace);



                returnData.Code = 200;
                returnData.Result = $"新增及更新西藥醫令,共新增<{list_value_add.Count}>筆資料,修改<{list_value_replace.Count}>筆資料";
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                if (returnData.ServerName.StringIsEmpty() || returnData.ServerType.StringIsEmpty())
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                }
                else
                {
                    sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
                }
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
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
        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_醫囑資料());               
            return table.JsonSerializationt(true);
        }
      
    }
}
