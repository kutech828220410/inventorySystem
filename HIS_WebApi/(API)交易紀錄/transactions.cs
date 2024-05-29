﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
    /// <summary>
    /// 交易紀錄
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class transactions : Controller
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
        [SwaggerResponse(1, "", typeof(transactionsClass))]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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
        /// 新增單筆交易紀錄
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        [transactionsClass]
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
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                transactionsClass transactionsClass = returnData.Data.ObjToClass<transactionsClass>();

                if (transactionsClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常!";
                    return returnData.JsonSerializationt();
                }
                transactionsClass.GUID = Guid.NewGuid().ToString();
                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                object[] value = transactionsClass.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
                sQLControl_trading.AddRow(null, value);
                returnData.Code = 200;
                returnData.Result = $"新增交易紀錄成功!";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClass;
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
        /// 新增單筆交易紀錄
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        [transactionsClass陣列]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("add_datas")]
        [HttpPost]
        public string add_datas([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add_datas";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                List<transactionsClass> transactionsClasses = returnData.Data.ObjToClass<List<transactionsClass>>();

                if (transactionsClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常!";
                    return returnData.JsonSerializationt();
                }
                for (int i = 0; i < transactionsClasses.Count; i++)
                {
                    transactionsClasses[i].GUID = Guid.NewGuid().ToString();
                }
                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
                sQLControl_trading.AddRows(null, list_value);
                returnData.Code = 200;
                returnData.Result = $"新增交易紀錄成功!共<{list_value.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 以藥碼搜尋交易紀錄
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[藥碼]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_by_code")]
        [HttpPost]
        public string POST_get_by_code([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_code";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入參數異常!";
                    return returnData.JsonSerializationt();
                }


                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥品碼, returnData.Value);
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 以藥名搜尋交易紀錄
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[藥名]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_by_name")]
        [HttpPost]
        public string POST_get_by_name([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_name";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入參數異常!";
                    return returnData.JsonSerializationt();
                }


                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_trading.GetRowsByLike(null, (int)enum_交易記錄查詢資料.藥品名稱, $"%{returnData.Value}%");
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 指定操作時間範圍取得交易紀錄
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[起始時間],[結束時間]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_by_op_time_st_end")]
        [HttpPost]
        public string POST_get_by_op_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_op_time_st_end";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                string[] date_ary = returnData.Value.Split(',');
                if (date_ary.Length != 2)
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                else
                {
                    if (!date_ary[0].Check_Date_String() || !date_ary[1].Check_Date_String())
                    {
                        returnData.Code = -5;
                        returnData.Result = "輸入日期格式錯誤!";
                        return returnData.JsonSerializationt();
                    }
                }
                DateTime date_st = date_ary[0].StringToDateTime();
                DateTime date_end = date_ary[1].StringToDateTime();


                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_trading.GetRowsByBetween(null, (int)enum_交易記錄查詢資料.操作時間, date_st.ToDateTimeString(), date_end.ToDateTimeString());
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 指定開方時間範圍取得交易紀錄
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///       
        ///     },
        ///     "Value" : "[起始時間],[結束時間]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_by_rx_time_st_end")]
        [HttpPost]
        public string POST_get_by_rx_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_rx_time_st_end";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                string[] date_ary = returnData.Value.Split(',');
                if (date_ary.Length != 2)
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                else
                {
                    if (!date_ary[0].Check_Date_String() || !date_ary[1].Check_Date_String())
                    {
                        returnData.Code = -5;
                        returnData.Result = "輸入日期格式錯誤!";
                        return returnData.JsonSerializationt();
                    }
                }
                DateTime date_st = date_ary[0].StringToDateTime();
                DateTime date_end = date_ary[1].StringToDateTime();


                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_trading.GetRowsByBetween(null, (int)enum_交易記錄查詢資料.開方時間, date_st.ToDateTimeString(), date_end.ToDateTimeString());
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 取得指定藥碼,效期批號資訊
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[藥碼]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data][StockClass]</returns>
        [Route("get_stock_by_code")]
        [HttpPost]
        public string POST_get_stock_by_code([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_stock_by_code";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                if (returnData.Value.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入參數異常!";
                    return returnData.JsonSerializationt();
                }



                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥品碼, returnData.Value);
                List<string> list_效期 = new List<string>();
                List<string> list_批號 = new List<string>();
                List<string> list_效期_buf = new List<string>();
                List<string> list_操作時間 = new List<string>();
                string 備註 = "";
                string 操作時間 = "";
                for (int i = 0; i < list_value.Count; i++)
                {
                    備註 = list_value[i][(int)enum_交易記錄查詢資料.備註].ObjectToString();
                    string[] temp_ary = 備註.Split('\n');
                    for (int k = 0; k < temp_ary.Length; k++)
                    {
                        string 效期 = temp_ary[k].GetTextValue("效期");
                        string 批號 = temp_ary[k].GetTextValue("批號");
                        操作時間 = list_value[i][(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString();
                        if (效期.StringIsEmpty() == true) continue;
                        list_效期_buf = (from temp in list_效期
                                       where temp == 效期
                                       select temp).ToList();
                        if (list_效期_buf.Count > 0) continue;
                        list_效期.Add(效期);
                        list_批號.Add(批號);
                        list_操作時間.Add(操作時間);
                    }
                }
                // 組合效期、批號和操作時間
                List<Tuple<string, string, DateTime>> combinedList = new List<Tuple<string, string, DateTime>>();
                for (int i = 0; i < list_效期.Count; i++)
                {
                    combinedList.Add(new Tuple<string, string, DateTime>(list_效期[i], list_批號[i], DateTime.Parse(list_操作時間[i])));
                }

                // 根據操作時間排序
                combinedList.Sort((x, y) => DateTime.Compare(y.Item3, x.Item3));

                // 更新list_效期、list_批號和list_操作時間
                list_效期.Clear();
                list_批號.Clear();
                list_操作時間.Clear();
                foreach (var item in combinedList)
                {
                    list_效期.Add(item.Item1);
                    list_批號.Add(item.Item2);
                    list_操作時間.Add(item.Item3.ToString());
                }
                List<StockClass> stockClasses = new List<StockClass>();
                for (int i = 0; i < list_效期.Count; i++)
                {
                    StockClass stockClass = new StockClass();
                    stockClass.Code = returnData.Value;
                    stockClass.Validity_period = list_效期[i];
                    stockClass.Lot_number = list_批號[i];
                    stockClass.Qty = "0";
                    stockClasses.Add(stockClass);

                }
                List<StockClass> sortedStockClasses = stockClasses.OrderBy(s => DateTime.Parse(s.Validity_period)).ToList();


                returnData.Code = 200;
                returnData.Result = $"({returnData.Value})取得效期成功!共<{stockClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = sortedStockClasses;
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
        }
        [Route("serch_med_information_by_code")]
        [HttpPost]
        public string POST_serch_med_information_by_code([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                if (returnData.Value.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "輸入資訊不得為空白!";
                    return returnData.JsonSerializationt();
                }
                string 藥品碼 = returnData.Value;
                MED_pageController mED_PageController = new MED_pageController();
                returnData returnData_med = new returnData();
                returnData_med.Server = Server;
                returnData_med.DbName = DB;
                returnData_med.TableName = returnData.TableName;
                returnData_med.UserName = UserName;
                returnData_med.Password = Password;
                returnData_med.Port = Port;

                returnData_med = mED_PageController.Get(returnData_med).JsonDeserializet<returnData>();
                List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();
                List<medClass> medClasses_buf = new List<medClass>();
                medClasses_buf = (from value in medClasses
                                  where value.藥品碼.ToUpper() == 藥品碼.ToUpper()
                                  select value).ToList();
                if (medClasses_buf.Count == 0)
                {
                    if (returnData.Value.StringIsEmpty())
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無此藥品碼 {藥品碼}!";
                        return returnData.JsonSerializationt();
                    }
                }
                returnData.Code = 200;
                returnData.Result = $"藥品資訊搜尋成功!";
                returnData.Data = medClasses_buf[0];
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }


        }
        [Route("serch")]
        [HttpPost]
        public string POST_serch([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                string TableName = "trading";
                SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                string[] input_value = returnData.Value.Split(",");
                if (input_value.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "輸入資訊錯誤!";
                    returnData.Data = "";
                    return returnData.JsonSerializationt();
                }
                if (input_value.Length == 1)
                {
                    string 藥品碼 = input_value[0];
                    List<object[]> list_value = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥品碼, 藥品碼);
                    list_value.Sort(new transactionsClass.ICP_By_OP_Time());
                    List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                    returnData.Code = 200;
                    returnData.Result = $"交易紀錄搜尋成功共<{transactionsClasses.Count}>筆";
                    returnData.Data = transactionsClasses;
                    return returnData.JsonSerializationt(true);
                }
                else
                {
                    if (input_value.Length != 3)
                    {
                        returnData.Code = -200;
                        returnData.Result = "輸入資訊錯誤!";
                        returnData.Data = "";
                        return returnData.JsonSerializationt();
                    }
                    string 藥品碼 = input_value[0];
                    string str_start_time = input_value[1];
                    string str_end_time = input_value[2];
                    if (str_start_time.Check_Date_String() == false || str_end_time.Check_Date_String() == false)
                    {
                        returnData.Code = -200;
                        returnData.Result = "輸入資訊錯誤!";
                        returnData.Data = "";
                        return returnData.JsonSerializationt();
                    }
                    DateTime start_time = str_start_time.StringToDateTime();
                    DateTime end_time = str_end_time.StringToDateTime();

                    List<object[]> list_value = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥品碼, 藥品碼);
                    list_value = list_value.GetRowsInDateEx((int)enum_交易記錄查詢資料.操作時間, start_time, end_time);
                    list_value.Sort(new transactionsClass.ICP_By_OP_Time());
                    List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                    returnData.Code = 200;
                    returnData.Result = $"交易紀錄搜尋成功共<{transactionsClasses.Count}>筆";
                    returnData.Data = transactionsClasses;
                    return returnData.JsonSerializationt(true);
                }

            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }


        }
        [Route("get_sheet_by_serch")]
        [HttpPost]
        public string POST_get_sheet_by_serch([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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

                string[] input_value = returnData.Value.Split(",");
                string 藥品碼 = input_value[0];
                string 起始時間 = "";
                string 結束時間 = "";

                if (input_value.Length == 3)
                {
                    起始時間 = input_value[1];
                    結束時間 = input_value[2];
                }
                MED_pageController mED_PageController = new MED_pageController();
                returnData returnData_med = new returnData();
                returnData_med.Server = Server;
                returnData_med.DbName = DB;
                returnData_med.TableName = returnData.TableName;
                returnData_med.UserName = UserName;
                returnData_med.Password = Password;
                returnData_med.Port = Port;

                returnData_med = mED_PageController.Get(returnData_med).JsonDeserializet<returnData>();

                List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();
                List<medClass> medClasses_buf = new List<medClass>();
                medClasses_buf = (from value in medClasses
                                  where value.藥品碼.ToUpper() == 藥品碼.ToUpper()
                                  select value).ToList();
                if (medClasses_buf.Count == 0)
                {
                    if (returnData.Value.StringIsEmpty())
                    {
                        return null;
                    }
                }

                string json = POST_serch(returnData);
                returnData = json.JsonDeserializet<returnData>();

                if (returnData.Code != 200)
                {
                    return null;
                }
                List<transactionsClass> transactionsClasses = returnData.Data.ObjToListClass<transactionsClass>();



                string loadText = Basic.MyFileStream.LoadFileAllText(@"./excel_emg_tradding.txt", "utf-8");
                Console.WriteLine($"取得creats {myTimer.ToString()}");
                int row_max = 50;
                List<SheetClass> sheetClasses = new List<SheetClass>();
                SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();


                int 消耗量 = 0;
                int NumOfRow = -1;
                for (int i = 0; i < transactionsClasses.Count; i++)
                {
                    if (NumOfRow >= row_max || NumOfRow == -1)
                    {
                        sheetClass = loadText.JsonDeserializet<SheetClass>();
                        sheetClass.Name = $"{i}";
                        sheetClass.ReplaceCell(1, 1, $"{medClasses_buf[0].藥品碼}");
                        sheetClass.ReplaceCell(1, 3, $"{medClasses_buf[0].包裝單位}");
                        sheetClass.ReplaceCell(1, 7, $"{medClasses_buf[0].管制級別}");
                        sheetClass.ReplaceCell(1, 10, $"{起始時間}");

                        sheetClass.ReplaceCell(2, 1, $"{medClasses_buf[0].藥品名稱}");
                        sheetClass.ReplaceCell(2, 7, $"{medClasses_buf[0].藥品許可證號}");
                        sheetClass.ReplaceCell(2, 10, $"{結束時間}");

                        sheetClass.ReplaceCell(3, 1, $"{medClasses_buf[0].藥品學名}");
                        sheetClass.ReplaceCell(3, 7, $"{medClasses_buf[0].廠牌}");
                        sheetClasses.Add(sheetClass);
                        NumOfRow = 0;
                    }

                    消耗量 += transactionsClasses[i].交易量.StringToInt32();
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 0, $"{i + 1}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 1, $"{transactionsClasses[i].操作時間}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 2, $"{transactionsClasses[i].床號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 3, $"{transactionsClasses[i].類別}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 4, $"{transactionsClasses[i].病人姓名}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 5, $"{transactionsClasses[i].病歷號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 6, $"{transactionsClasses[i].操作人}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 7, $"{transactionsClasses[i].藥師證字號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 8, $"{transactionsClasses[i].交易量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 9, $"{transactionsClasses[i].結存量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 10, $"{transactionsClasses[i].收支原因}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 5, 11, $"{transactionsClasses[i].備註}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    NumOfRow++;
                }
                for (int i = 0; i < sheetClasses.Count; i++)
                {
                    sheetClasses[i].ReplaceCell(1, 5, $"{消耗量}");
                }


                Console.WriteLine($"寫入Sheet {myTimer.ToString()}");
                returnData.Code = 200;
                returnData.Result = "Sheet取得成功!";
                returnData.Data = sheetClasses;
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        [Route("download_excel_by_serch")]
        [HttpPost]
        public async Task<ActionResult> Post_download_excel_by_serch([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                returnData = POST_get_sheet_by_serch(returnData).JsonDeserializet<returnData>();
                if (returnData.Code != 200)
                {
                    return null;
                }
                string jsondata = returnData.Data.JsonSerializationt();

                List<SheetClass> sheetClasses = jsondata.JsonDeserializet<List<SheetClass>>();

                string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string xls_command = "application/vnd.ms-excel";

                byte[] excelData = sheetClasses.NPOI_GetBytes(Excel_Type.xlsx);
                Stream stream = new MemoryStream(excelData);
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_管制結存表.xlsx"));
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 取得交易紀錄明細(Excel)(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        List(transactionsClasses)
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("download_datas_excel")]
        [HttpPost]
        public async Task<ActionResult> Post_download_datas_excel([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                List<transactionsClass> transactionsClasses = returnData.Data.ObjToClass<List<transactionsClass>>();

                List<object[]> list_transactionsClasses = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
                System.Data.DataTable dataTable = list_transactionsClasses.ToDataTable(new enum_交易記錄查詢資料());
                dataTable = dataTable.ReorderTable(new enum_交易記錄查詢資料_匯出());
                string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string xls_command = "application/vnd.ms-excel";
                List<System.Data.DataTable> dataTables = new List<System.Data.DataTable>();
                dataTables.Add(dataTable);
                byte[] excelData = MyOffice.ExcelClass.NPOI_GetBytes(dataTable, Excel_Type.xlsx, (int)enum_交易記錄查詢資料_匯出.交易量, (int)enum_交易記錄查詢資料_匯出.庫存量, (int)enum_交易記錄查詢資料_匯出.盤點量, (int)enum_交易記錄查詢資料_匯出.結存量);
                Stream stream = new MemoryStream(excelData);
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_交易紀錄明細.xlsx"));
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 取得收支結存報表(Excel)(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "起始時間",
        ///       "結束時間",
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("download_cdmis_datas_excel")]
        [HttpPost]
        public async Task<ActionResult> Post_download_cdmis_datas_excel([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                returnData = POST_get_datas_sheet(returnData).JsonDeserializet<returnData>();
                if (returnData.Code != 200)
                {
                    return null;
                }
                string jsondata = returnData.Data.JsonSerializationt();

                List<SheetClass> sheetClasses = jsondata.JsonDeserializet<List<SheetClass>>();

                string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string xls_command = "application/vnd.ms-excel";

                byte[] excelData = sheetClasses.NPOI_GetBytes(Excel_Type.xlsx);
                Stream stream = new MemoryStream(excelData);
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_收支結存簿冊.xlsx"));
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 取得收支結存報表(SheetClass)JSON(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "藥碼1,藥碼2,藥碼3",
        ///       "起始時間",
        ///       "結束時間",
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_cdmis_datas_sheet")]
        [HttpPost]
        public string POST_get_datas_sheet([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_sheet";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 5)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼][起始時間][結束時間][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string[] 藥碼Ary = returnData.ValueAry[0].Split(",");
                string 起始時間 = returnData.ValueAry[1];
                string 結束時間 = returnData.ValueAry[2];
                string serverName = returnData.ValueAry[3];
                string serverType = returnData.ValueAry[4];

                string[] ServerNames = serverName.Split(',');
                string[] ServerTypes = serverType.Split(',');
                if (藥碼Ary.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[藥碼] 欄位異常 ,請用','分隔需搜尋藥碼";
                    return returnData.JsonSerializationt(true);

                }
                if (起始時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[起始時間] 為非法格式";
                    return returnData.JsonSerializationt(true);
                }
                if (結束時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"[結束時間] 為非法格式";
                    return returnData.JsonSerializationt(true);
                }
                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                DateTime dateTime_st = 起始時間.StringToDateTime();
                DateTime dateTime_end = 結束時間.StringToDateTime();
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                MED_pageController mED_PageController = new MED_pageController();
                returnData returnData_med = new returnData();
                returnData_med.ServerName = "Main";
                returnData_med.ServerType = "網頁";
                returnData_med.Server = serverSettingClasses_med.Server;
                returnData_med.DbName = serverSettingClasses_med.DBName;
                returnData_med.TableName = "medicine_page_cloud";
                returnData_med.UserName = serverSettingClasses_med.User;
                returnData_med.Password = serverSettingClasses_med.Password;
                returnData_med.Port = serverSettingClasses_med.Port.StringToUInt32();
                returnData_med = mED_PageController.POST_get_by_apiserver(returnData_med).JsonDeserializet<returnData>();
                List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();

                List<SheetClass> sheetClasses = new List<SheetClass>();
                List<List<transactionsClass>> list_transactionsClasses = new List<List<transactionsClass>>();
                for (int k = 0; k < 藥碼Ary.Length; k++)
                {
                    string 藥碼 = 藥碼Ary[k];
                    returnData returnData_get_datas_by_code = new returnData();
                    returnData_get_datas_by_code.ValueAry.Add(藥碼Ary[k]);
                    returnData_get_datas_by_code.ValueAry.Add(serverName);
                    returnData_get_datas_by_code.ValueAry.Add(serverType);
                    string json_get_datas_by_code = POST_get_datas_by_code(returnData_get_datas_by_code);
                    returnData_get_datas_by_code = json_get_datas_by_code.JsonDeserializet<returnData>();
                    if (returnData_get_datas_by_code.Code != 200)
                    {
                        return returnData_get_datas_by_code.JsonSerializationt(true);
                    }

                    List<transactionsClass> transactionsClasses = returnData_get_datas_by_code.Data.ObjToListClass<transactionsClass>();

                    transactionsClasses = (from temp in transactionsClasses
                                           where temp.操作時間.StringToDateTime() >= dateTime_st
                                           where temp.操作時間.StringToDateTime() <= dateTime_end
                                           select temp).ToList();

                    List<medClass> medClasses_buf = new List<medClass>();

                    medClasses_buf = (from value in medClasses
                                      where value.藥品碼.ToUpper() == 藥碼.ToUpper()
                                      select value).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        if (returnData.Value.StringIsEmpty())
                        {
                            return null;
                        }
                    }

                    string loadText = Basic.MyFileStream.LoadFileAllText(@"./excel_emg_tradding.txt", "utf-8");
                    Console.WriteLine($"取得creats {myTimerBasic.ToString()}");
                    int row_max = 60000;
       
                    SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();

                    int 消耗量 = 0;
                    int NumOfRow = -1;
                    for (int i = 0; i < transactionsClasses.Count; i++)
                    {
                        if (NumOfRow >= row_max || NumOfRow == -1)
                        {
                            sheetClass = loadText.JsonDeserializet<SheetClass>();
                            sheetClass.Name = $"{藥碼}";
                            sheetClass.ReplaceCell(1, 1, $"{medClasses_buf[0].藥品碼}");
                            sheetClass.ReplaceCell(1, 3, $"{medClasses_buf[0].包裝單位}");
                            sheetClass.ReplaceCell(1, 7, $"{medClasses_buf[0].管制級別}");
                            sheetClass.ReplaceCell(1, 10, $"{起始時間}");

                            sheetClass.ReplaceCell(2, 1, $"{medClasses_buf[0].藥品名稱}");
                            sheetClass.ReplaceCell(2, 7, $"{medClasses_buf[0].藥品許可證號}");
                            sheetClass.ReplaceCell(2, 10, $"{結束時間}");

                            sheetClass.ReplaceCell(3, 1, $"{medClasses_buf[0].藥品學名}");
                            sheetClass.ReplaceCell(3, 7, $"{medClasses_buf[0].廠牌}");
                            sheetClasses.Add(sheetClass);
                            NumOfRow = 0;
                        }

                        消耗量 += transactionsClasses[i].交易量.StringToInt32();
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 0, $"{i + 1}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 1, $"{transactionsClasses[i].操作時間}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 2, $"{transactionsClasses[i].床號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 3, $"{transactionsClasses[i].類別}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 4, $"{transactionsClasses[i].病人姓名}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 5, $"{transactionsClasses[i].病歷號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 6, $"{transactionsClasses[i].操作人}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 7, $"{transactionsClasses[i].藥師證字號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 8, $"{transactionsClasses[i].交易量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 9, $"{transactionsClasses[i].結存量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 10, $"{transactionsClasses[i].收支原因}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        sheetClass.AddNewCell_Webapi(NumOfRow + 5, 11, $"{transactionsClasses[i].備註}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                        NumOfRow++;
                    }
                    for (int i = 0; i < sheetClasses.Count; i++)
                    {
                        sheetClasses[i].ReplaceCell(1, 5, $"{消耗量}");
                    }

                }
          




                

            


             

                returnData.Code = 200;
                returnData.Result = "Sheet取得成功!";
                returnData.Data = sheetClasses;
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
        /// 以藥碼搜尋交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_code")]
        [HttpPost]
        public string POST_get_datas_by_code([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_code";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥碼 = returnData.ValueAry[0];
                string[] ServerNames = returnData.ValueAry[1].Split(',');
                string[] ServerTypes = returnData.ValueAry[2].Split(',');
                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥品碼, 藥碼);
                        list_value_buf = tradingData(list_value_buf);

                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 以藥名搜尋交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_name")]
        [HttpPost]
        public string POST_get_datas_by_name([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_name";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string serchValue = returnData.ValueAry[0];
                string[] ServerNames = returnData.ValueAry[1].Split(',');
                string[] ServerTypes = returnData.ValueAry[2].Split(',');
                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByLike(null, (int)enum_交易記錄查詢資料.藥品名稱, $"%{serchValue}%");
                        list_value_buf = tradingData(list_value_buf);


                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 以病歷號搜尋交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_mrn")]
        [HttpPost]
        public string POST_get_datas_by_mrn([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_mrn";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[病歷號][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string serchValue = returnData.ValueAry[0];
                string[] ServerNames = returnData.ValueAry[1].Split(',');
                string[] ServerTypes = returnData.ValueAry[2].Split(',');
                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByLike(null, (int)enum_交易記錄查詢資料.病歷號, $"%{serchValue}%");
                        list_value_buf = tradingData(list_value_buf);


                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 以操作人搜尋交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "操作人",
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_op")]
        [HttpPost]
        public string POST_get_datas_by_op([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_op";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[操作人][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string serchValue = returnData.ValueAry[0];
                string[] ServerNames = returnData.ValueAry[1].Split(',');
                string[] ServerTypes = returnData.ValueAry[2].Split(',');
                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByLike(null, (int)enum_交易記錄查詢資料.操作人, $"%{serchValue}%");
                        list_value_buf = tradingData(list_value_buf);


                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 以領藥號搜尋交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_med_bag_num")]
        [HttpPost]
        public string POST_get_datas_by_med_bag_num([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_med_bag_num";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[操作人][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string serchValue = returnData.ValueAry[0];
                string[] ServerNames = returnData.ValueAry[1].Split(',');
                string[] ServerTypes = returnData.ValueAry[2].Split(',');
                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByLike(null, (int)enum_交易記錄查詢資料.領藥號, $"%{serchValue}%");
                        list_value_buf = tradingData(list_value_buf);


                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 以領藥號搜尋交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_pat")]
        [HttpPost]
        public string POST_get_datas_by_pat([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_pat";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[操作人][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string serchValue = returnData.ValueAry[0];
                string[] ServerNames = returnData.ValueAry[1].Split(',');
                string[] ServerTypes = returnData.ValueAry[2].Split(',');
                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByLike(null, (int)enum_交易記錄查詢資料.病人姓名, $"%{serchValue}%");
                        list_value_buf = tradingData(list_value_buf);


                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 指定操作時間範圍取得交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "結束時間",
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_op_time_st_end")]
        [HttpPost]
        public string POST_get_datas_by_op_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_op_time_st_end";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 4)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];
                string[] ServerNames = returnData.ValueAry[2].Split(',');
                string[] ServerTypes = returnData.ValueAry[3].Split(',');


                if (!起始時間.Check_Date_String() || !結束時間.Check_Date_String())
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                DateTime date_st = 起始時間.StringToDateTime();
                DateTime date_end = 結束時間.StringToDateTime();

                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByBetween(null, (int)enum_交易記錄查詢資料.操作時間, date_st.ToDateTimeString(), date_end.ToDateTimeString());
                        list_value_buf = tradingData(list_value_buf);

                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
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
        /// 指定開方時間範圍取得交易紀錄(多台合併)
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
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
        ///       "結束時間",
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [Route("get_datas_by_rx_time_st_end")]
        [HttpPost]
        public string POST_get_datas_by_rx_time_st_end([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_datas_by_rx_time_st_end";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<Task> tasks = new List<Task>();
                List<object[]> list_value = new List<object[]>();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }

                if (returnData.ValueAry.Count != 4)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間][ServerName1,ServerName2][ServerType1,ServerType2]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];
                string[] ServerNames = returnData.ValueAry[2].Split(',');
                string[] ServerTypes = returnData.ValueAry[3].Split(',');


                if (!起始時間.Check_Date_String() || !結束時間.Check_Date_String())
                {
                    returnData.Code = -5;
                    returnData.Result = "輸入日期格式錯誤!";
                    return returnData.JsonSerializationt();
                }
                DateTime date_st = 起始時間.StringToDateTime();
                DateTime date_end = 結束時間.StringToDateTime();

                if (ServerNames.Length != ServerTypes.Length)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ServerNames及ServerTypes長度不同";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_serverSettingClasses.Count == 0) return;
                        string Server = _serverSettingClasses[0].Server;
                        string DB = _serverSettingClasses[0].DBName;
                        string UserName = _serverSettingClasses[0].User;
                        string Password = _serverSettingClasses[0].Password;
                        uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                        List<object[]> list_value_buf = sQLControl_trading.GetRowsByBetween(null, (int)enum_交易記錄查詢資料.開方時間, date_st.ToDateTimeString(), date_end.ToDateTimeString());

                        list_value_buf = tradingData(list_value_buf);

                        list_value.LockAdd(list_value_buf);

                    })));



                }
                Task.WhenAll(tasks).Wait();
                list_value.Sort(new ICP_交易記錄查詢());
                List<transactionsClass> transactionsClasses = list_value.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                returnData.Code = 200;
                returnData.Result = $"取得交易紀錄成功!共<{transactionsClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = transactionsClasses;
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
        }
        private List<object[]> tradingData(List<object[]> list_value)
        {
            list_value = (from temp in list_value
                          where FilterByAction(temp)
                          select temp).ToList();
            return list_value;
        }

        private bool FilterByAction(object[] temp)
        {
            string str_action = temp[(int)enum_交易記錄查詢資料.動作].ObjectToString();
            if (str_action == enum_交易記錄查詢動作.自動過帳.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.掃碼領藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.手輸領藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.批次領藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.重複領藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統領藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.盤點調整.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.掃碼退藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.手輸退藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統入庫.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統加藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統退藥.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統調入.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統調出.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統撥入.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.系統撥出.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.入庫作業.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.出庫作業.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.撥入作業.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.撥出作業.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.調入作業.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.調出作業.GetEnumName()) return true;
            if (str_action == enum_交易記錄查詢動作.效期庫存異動.GetEnumName()) return true;
            return false;
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "trading", UserName, Password, Port, SSLMode);


            Table table = new Table("trading");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("動作", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("診別", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("庫別", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 15, Table.IndexType.INDEX);
            table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 200, Table.IndexType.INDEX);
            table.AddColumnList("藥袋序號", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("領藥號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("類別", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("庫存量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("交易量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("結存量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("盤點量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("操作人", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
            table.AddColumnList("領用人", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
            table.AddColumnList("藥師證字號", Table.StringType.VARCHAR, 15, Table.IndexType.INDEX);
            table.AddColumnList("病人姓名", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table.AddColumnList("頻次", Table.StringType.VARCHAR, 15, Table.IndexType.None);
            table.AddColumnList("病房號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("床號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("病歷號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("操作時間", Table.DateType.DATETIME, 50, Table.IndexType.INDEX);
            table.AddColumnList("領用時間", Table.DateType.DATETIME, 50, Table.IndexType.INDEX);
            table.AddColumnList("開方時間", Table.DateType.DATETIME, 50, Table.IndexType.INDEX);
            table.AddColumnList("收支原因", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("備註", Table.StringType.VARCHAR, 500, Table.IndexType.None);
            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
            else sQLControl.CheckAllColumnName(table, true);
            return table.JsonSerializationt(true);
        }
        public class ICP_交易記錄查詢 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                DateTime datetime2 = y[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                if (compare != 0) return compare;
                int 結存量1 = x[(int)enum_交易記錄查詢資料.結存量].StringToInt32();
                int 結存量2 = y[(int)enum_交易記錄查詢資料.結存量].StringToInt32();
                if (結存量1 > 結存量2)
                {
                    return -1;
                }
                else if (結存量1 < 結存量2)
                {
                    return 1;
                }
                else if (結存量1 == 結存量2) return 0;
                return 0;

            }
        }
    }
}
