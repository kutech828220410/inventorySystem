﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using MySql.Data.MySqlClient;
using SQLUI;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using H_Pannel_lib;
using Org.BouncyCastle.Bcpg.OpenPgp;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using System.Text.RegularExpressions;
using System.Data;
using MyOffice;
using MyUI;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_AI
{
    [Route("api/[controller]")]
    [ApiController]
    public class suspiciousRxLog : ControllerBase
    {
        static string API_Server = Method.GetServerAPI("Main", "網頁", "API01");
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(suspiciousRxLogClass))]
        [HttpPost("init")]
        public string init()
        {
            try
            {
                return CheckCreatTable(null, new enum_suspiciousRxLog());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("init_suspiciousRxLog_rule")]
        public string init_suspiciousRxLog_rule()
        {
            try
            {
                return CheckCreatTable(null, new enum_suspiciousRxLog_rule());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("init_suspiciousRxLog_rule_local")]
        public string init_suspiciousRxLog_rule_local()
        {
            try
            {
                return CheckCreatTable("suspiciousRxLog_rule_local", new enum_suspiciousRxLog_rule());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [suspiciousRxLogClass陣列]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add")]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                suspiciousRxLogClass suspiciousRxLogClasses = returnData.Data.ObjToClass<suspiciousRxLogClass>();
                if (suspiciousRxLogClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                string TableName = "suspiciousRxLog";
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> add_suspiciousRxLog = new List<suspiciousRxLogClass>() { suspiciousRxLogClasses }.ClassToSQL<suspiciousRxLogClass, enum_suspiciousRxLog>();
                sQLControl.AddRows(null, add_suspiciousRxLog);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = suspiciousRxLogClasses;
                returnData.Result = $"新增一筆資料";
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
        /// 更新
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [suspiciousRxLogClass陣列]
        ///     },
        ///     "ValueAry":[""]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update")]
        public string update([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<suspiciousRxLogClass> suspiciousRxLogClasses = returnData.Data.ObjToClass<List<suspiciousRxLogClass>>();
                if (suspiciousRxLogClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }


                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                string TableName = "suspiciousRxLog";
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> update_suspiciousRxLog = suspiciousRxLogClasses.ClassToSQL<suspiciousRxLogClass, enum_suspiciousRxLog>();
                sQLControl.UpdateByDefulteExtra(null, update_suspiciousRxLog);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = suspiciousRxLogClasses;
                returnData.Result = $"更新{suspiciousRxLogClasses.Count}筆資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 取得規則
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "ValueAry":[""]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_rule")]
        public string get_rule([FromBody] returnData returnData)
        {
            init();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_rule";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);
                init_suspiciousRxLog_rule();
                init_suspiciousRxLog_rule_local();

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                List<Task> tasks = new List<Task>();
                List<suspiciousRxLog_ruleClass> suspiciousRxLogRule = new List<suspiciousRxLog_ruleClass>();
                List<suspiciousRxLog_ruleClass> suspiciousRxLogRule_locals = new List<suspiciousRxLog_ruleClass>();

                tasks.Add(Task.Run(new Action(delegate
                {
                    string TableName = "suspiciousRxLog_rule";
                    SQLControl sQLControl = new SQLControl(Server, DB, "suspiciousRxLog_rule", UserName, Password, Port, SSLMode);
                    List<object[]> list_suspiciousRxLog = sQLControl.GetAllRows(null);
                    suspiciousRxLogRule = list_suspiciousRxLog.SQLToClass<suspiciousRxLog_ruleClass, enum_suspiciousRxLog_rule>();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    string TableName = "suspiciousRxLog_rule_local";
                    SQLControl sQLControl = new SQLControl(Server, DB, "suspiciousRxLog_rule_local", UserName, Password, Port, SSLMode);
                    List<object[]> list_suspiciousRxLog = sQLControl.GetAllRows(null);
                    suspiciousRxLogRule_locals = list_suspiciousRxLog.SQLToClass<suspiciousRxLog_ruleClass, enum_suspiciousRxLog_rule>();
                })));
                Task.WhenAll(tasks).Wait();
                suspiciousRxLogRule.AddRange(suspiciousRxLogRule_locals);
                suspiciousRxLogRule.Sort(new suspiciousRxLog_ruleClass.ICP_By_index());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = suspiciousRxLogRule;
                returnData.Result = $"取得{suspiciousRxLogRule.Count}筆資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 取得規則
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "ValueAry":[""]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_rule_by_index")]
        public string get_rule_by_index([FromBody] returnData returnData)
        {
            init();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_rule_by_index";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry應該為[\"索引值\"]";
                    return returnData.JsonSerializationt();
                }
                List<string> 索引值 = returnData.ValueAry[0].Split(';').ToList();
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);
                init_suspiciousRxLog_rule();
                init_suspiciousRxLog_rule_local();

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                List<Task> tasks = new List<Task>();
                List<suspiciousRxLog_ruleClass> suspiciousRxLogRule = new List<suspiciousRxLog_ruleClass>();
                List<suspiciousRxLog_ruleClass> suspiciousRxLogRule_locals = new List<suspiciousRxLog_ruleClass>();

                tasks.Add(Task.Run(new Action(delegate
                {
                    string TableName = "suspiciousRxLog_rule";
                    SQLControl sQLControl = new SQLControl(Server, DB, "suspiciousRxLog_rule", UserName, Password, Port, SSLMode);
                    List<object[]> list_suspiciousRxLog = sQLControl.GetAllRows(null);
                    suspiciousRxLogRule = list_suspiciousRxLog.SQLToClass<suspiciousRxLog_ruleClass, enum_suspiciousRxLog_rule>();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    string TableName = "suspiciousRxLog_rule_local";
                    SQLControl sQLControl = new SQLControl(Server, DB, "suspiciousRxLog_rule_local", UserName, Password, Port, SSLMode);
                    List<object[]> list_suspiciousRxLog = sQLControl.GetAllRows(null);
                    suspiciousRxLogRule_locals = list_suspiciousRxLog.SQLToClass<suspiciousRxLog_ruleClass, enum_suspiciousRxLog_rule>();
                })));
                Task.WhenAll(tasks).Wait();
                suspiciousRxLogRule.AddRange(suspiciousRxLogRule_locals);
                suspiciousRxLogRule = suspiciousRxLogRule.Where(temp => 索引值.Contains(temp.索引)).ToList();
                suspiciousRxLogRule.Sort(new suspiciousRxLog_ruleClass.ICP_By_index());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = suspiciousRxLogRule;
                returnData.Result = $"取得{suspiciousRxLogRule.Count}筆資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 取得規則
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "ValueAry":[""]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add_rule_local")]
        public string add_rule_local([FromBody] returnData returnData)
        {
            init_suspiciousRxLog_rule_local();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add_rule_local";
            try
            {
                List<suspiciousRxLog_ruleClass> suspiciousRxLogRule_locals = returnData.Data.ObjToClass<List<suspiciousRxLog_ruleClass>>();
                if (suspiciousRxLogRule_locals == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "suspiciousRxLog_rule_local", UserName, Password, Port, SSLMode);
                List<object[]> sql_ruleLocal = sQLControl.GetAllRows(null);
                List<suspiciousRxLog_ruleClass> sql_ruleLocals = sql_ruleLocal.SQLToClass<suspiciousRxLog_ruleClass, enum_suspiciousRxLog_rule>();
                Dictionary<string, List<suspiciousRxLog_ruleClass>> dic_ruleLocals = suspiciousRxLog_ruleClass.ToDictByGroup(sql_ruleLocals);
                string alarm = string.Empty;
                List<suspiciousRxLog_ruleClass> add = new List<suspiciousRxLog_ruleClass>();
                List<suspiciousRxLog_ruleClass> error = new List<suspiciousRxLog_ruleClass>();

                foreach (var item in suspiciousRxLogRule_locals)
                {
                    List<suspiciousRxLog_ruleClass> suspiciousRxLog_RuleClasses = suspiciousRxLog_ruleClass.GetByGroup(dic_ruleLocals, item.群組);
                    if (suspiciousRxLog_RuleClasses.Count > 0)
                    {
                        suspiciousRxLog_ruleClass suspiciousRxLog_Rule = suspiciousRxLog_RuleClasses.Where(temp => temp.索引 == item.索引).FirstOrDefault();
                        if (suspiciousRxLog_Rule == null)
                        {
                            item.GUID = Guid.NewGuid().ToString();
                            item.類別 = "local";
                            add.Add(item);
                        }
                        else
                        {
                            error.Add(item);
                        }
                    }
                    else
                    {
                        item.GUID = Guid.NewGuid().ToString();
                        item.類別 = "local";
                        add.Add(item);
                    }

                }
                if (error.Count > 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"已存在重複索引值";
                    returnData.Data = error;
                    return returnData.JsonSerializationt();
                }
                List<object[]> list_suspiciousRxLogRule = add.ClassToSQL<suspiciousRxLog_ruleClass, enum_suspiciousRxLog_rule>();
                sQLControl.AddRows(null, list_suspiciousRxLogRule);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = add;
                returnData.Result = $"增加{suspiciousRxLogRule_locals.Count}筆資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 以order_PRI_KEY(藥袋條碼)取得資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "ValueAry":["藥袋條碼"]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_by_barcode")]
        public string get_by_barcode([FromBody] returnData returnData)
        {
            init();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_barcode";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry應該為[\"藥袋條碼\"]";
                    return returnData.JsonSerializationt();
                }
                string 藥袋條碼 = returnData.ValueAry[0];

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                string TableName = "suspiciousRxLog";
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_suspiciousRxLog = sQLControl.GetRowsByDefult(null, (int)enum_suspiciousRxLog.藥袋條碼, 藥袋條碼);
                List<suspiciousRxLogClass> suspiciousRxLogClasses = list_suspiciousRxLog.SQLToClass<suspiciousRxLogClass, enum_suspiciousRxLog>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = suspiciousRxLogClasses;
                returnData.Result = $"取得{suspiciousRxLogClasses.Count}筆資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 以操作時間取得資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "ValueAry":["起始時間","結束時間"]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_by_op_time")]
        public string get_by_op_time([FromBody] returnData returnData)
        {
            init();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_op_time";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry應該為[\"起始時間\",\"結束時間\"]";
                    return returnData.JsonSerializationt();
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];
                DateTime date_st = 起始時間.StringToDateTime();
                DateTime date_end = 結束時間.StringToDateTime();

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                string TableName = "suspiciousRxLog";
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                string command = $"SELECT * FROM {DB}.{TableName} WHERE 加入時間 BETWEEN '{date_st.ToDateTimeString()}' AND '{date_end.ToDateTimeString()}' AND 狀態 != '{enum_suspiciousRxLog_status.無異狀.GetEnumName()}';";
                DataTable dataTable = sQLControl.WtrteCommandAndExecuteReader(command);

                List<object[]> list_suspiciousRxLog = dataTable.DataTableToRowList();
                List<suspiciousRxLogClass> suspiciousRxLogClasses = list_suspiciousRxLog.SQLToClass<suspiciousRxLogClass, enum_suspiciousRxLog>();
                suspiciousRxLogClasses.Sort(new suspiciousRxLogClass.ICP_By_OP_Time());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = suspiciousRxLogClasses;
                returnData.Result = $"取得{suspiciousRxLogClasses.Count}筆資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        [HttpPost("medGPT")]
        public string medGPT(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "api/suspiciousRxLog/medGPT";
            try
            {
                List<OrderClass> orders = returnData.Data.ObjToClass<List<OrderClass>>();
                if (orders.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data應為List<OrderClass>";
                    return returnData.JsonSerializationt(true);
                }

                string 藥袋條碼 = orders[0].藥袋條碼;
                AddsuspiciousRxLog(orders);
                List<suspiciousRxLogClass> suspiciousRxLoges = suspiciousRxLogClass.get_by_barcode(API_Server, 藥袋條碼);
                if (suspiciousRxLoges.Count > 0 & suspiciousRxLoges[0].狀態 != enum_suspiciousRxLog_status.未辨識.GetEnumName())
                {
                    suspiciousRxLoges[0].辨識註記 = "Y";
                    returnData.Code = 200;
                    returnData.Data = suspiciousRxLoges;
                    returnData.Result = $"條碼{藥袋條碼}已辨識過";
                    return returnData.JsonSerializationt(true);
                }

                suspiciousRxLogClass suspiciousRxLogClasses = suspiciousRxLoges[0];

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "藥檔資料");
                SQLControl sQLControl = new SQLControl(Server, DB, "order_list", UserName, Password, Port, SSLMode);

                string 病歷號 = orders[0].病歷號;

                if (orders == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data空白";
                    return returnData.JsonSerializationt(true);
                }

                List<Task> tasks = new List<Task>();
                List<Prescription> eff_cpoe = new List<Prescription>();
                List<Prescription> old_cpoe = new List<Prescription>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> list_order = sQLControl.GetRowsByDefult(null, (int)enum_醫囑資料.病歷號, 病歷號);
                    List<OrderClass> history_order = list_order.SQLToClass<OrderClass, enum_醫囑資料>();
                    history_order = (from temp in history_order
                                     where temp.產出時間.StringToDateTime() >= DateTime.Now.AddDays(-1).AddMonths(-3).GetStartDate()
                                     where temp.產出時間.StringToDateTime() >= DateTime.Now.AddDays(-1).AddMonths(-3).GetStartDate()
                                     select temp).ToList();
                    old_cpoe = GroupOrderList(history_order, suspiciousRxLogClasses);
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    eff_cpoe = GroupOrderList(orders, suspiciousRxLogClasses);
                })));
                Task.WhenAll(tasks).Wait();

                PrescriptionSet result = new PrescriptionSet
                {
                    有效處方 = eff_cpoe,
                    歷史處方 = old_cpoe
                };
                //if (true)
                //{
                //    returnData.Data = result;
                //    Logger.Log("suspiciousRxLog_input", returnData.JsonSerializationt(true));
                //    return "OK";
                //}
                string url = Method.GetServerAPI("Main", "網頁", "medgpt_api");

                suspiciousRxLogClass suspiciousRxLog = suspiciousRxLogClass.Excute(url, result);
                if (suspiciousRxLog == null)
                {
                    returnData.Url = url;
                    returnData.Data = result;
                    returnData.Code = -200;
                    returnData.Result = $"MEDGPT呼叫失敗";
                    Logger.Log("suspiciousRxLog", returnData.JsonSerializationt(true));
                    return returnData.JsonSerializationt(true);
                }

                if (suspiciousRxLog.rule_type == null || suspiciousRxLog.rule_type.Count == 0)
                {
                    returnData.Url = url;
                    returnData.Data = result;
                    returnData.Code = -200;
                    returnData.Result = $"規則回傳不得為空";
                    Logger.Log("suspiciousRxLog", returnData.JsonSerializationt(true));
                    return returnData.JsonSerializationt(true);
                }
                List<suspiciousRxLog_ruleClass> suspiciousRxLog_ruleClasses = suspiciousRxLog_ruleClass.get_rule_by_index(API_Server, suspiciousRxLog.rule_type);
                suspiciousRxLog_ruleClass buff_suspiciousRxLog_ruleClass = new suspiciousRxLog_ruleClass();
                buff_suspiciousRxLog_ruleClass = suspiciousRxLog_ruleClasses.Where(temp => temp.提報等級 == enum_suspiciousRxLog_ReportLevel.Critical.GetEnumName()).FirstOrDefault();
                if (buff_suspiciousRxLog_ruleClass == null) buff_suspiciousRxLog_ruleClass = suspiciousRxLog_ruleClasses.Where(temp => temp.提報等級 == enum_suspiciousRxLog_ReportLevel.Important.GetEnumName()).FirstOrDefault();
                if (buff_suspiciousRxLog_ruleClass == null) buff_suspiciousRxLog_ruleClass = suspiciousRxLog_ruleClasses.Where(temp => temp.提報等級 == enum_suspiciousRxLog_ReportLevel.Normal.GetEnumName()).FirstOrDefault();

                if (suspiciousRxLog.error == true.ToString())
                {
                    suspiciousRxLogClasses.錯誤類別 = string.Join(",", suspiciousRxLog.error_type);
                    suspiciousRxLogClasses.簡述事件 = suspiciousRxLog.response;
                    suspiciousRxLogClasses.狀態 = enum_suspiciousRxLog_status.未更改.GetEnumName();
                    suspiciousRxLogClasses.提報等級 = buff_suspiciousRxLog_ruleClass.提報等級;
                    suspiciousRxLogClasses.rule_type = suspiciousRxLog.rule_type;
                    suspiciousRxLogClass.update(API_Server, suspiciousRxLogClasses);
                }
                else
                {
                    suspiciousRxLogClasses.狀態 = enum_suspiciousRxLog_status.無異狀.GetEnumName();
                    suspiciousRxLogClass.update(API_Server, suspiciousRxLogClasses);
                }
                suspiciousRxLogClasses.suspiciousRxLog_ruleClasses = suspiciousRxLog_ruleClasses;
                returnData.Data = new List<suspiciousRxLogClass>() { suspiciousRxLogClasses };
                returnData.Code = 200;
                returnData.Result = $"AI辨識處方成功";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception:{ex.Message}";
                return returnData.JsonSerializationt(true);
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
        ///        List(suspiciousRxLogClass)
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為交易紀錄結構</returns>
        [HttpPost("download_datas_excel")]
        public async Task<ActionResult> download_datas_excel([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                List<suspiciousRxLogClass> suspiciousRxLogClasses = returnData.Data.ObjToClass<List<suspiciousRxLogClass>>();

                List<object[]> list_transactionsClasses = suspiciousRxLogClasses.ClassToSQL<suspiciousRxLogClass, enum_suspiciousRxLog>();
                System.Data.DataTable dataTable = list_transactionsClasses.ToDataTable(new enum_suspiciousRxLog());
                dataTable = dataTable.ReorderTable(new enum_suspiciousRxLog_export());
                string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string xls_command = "application/vnd.ms-excel";
                List<System.Data.DataTable> dataTables = new List<System.Data.DataTable>();
                dataTables.Add(dataTable);
                byte[] excelData = MyOffice.ExcelClass.NPOI_GetBytes(dataTable, Excel_Type.xlsx);
                Stream stream = new MemoryStream(excelData);
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_醫師處方疑義紀錄表.xlsx"));
            }
            catch
            {
                return null;
            }

        }
        [HttpPost("download_document")]
        public async Task<ActionResult> download_document([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry應該為[\"GUID\"]";
                    //return returnData.JsonSerializationt();
                }
                string GUID = returnData.ValueAry[0];
                string TableName = "suspiciousRxLog";
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_suspiciousRxLog = sQLControl.GetRowsByDefult(null, (int)enum_suspiciousRxLog.GUID, GUID);
                suspiciousRxLogClass suspiciousRxLogClasses = list_suspiciousRxLog.SQLToClass<suspiciousRxLogClass, enum_suspiciousRxLog>().FirstOrDefault();
                if (suspiciousRxLogClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"無此GUID資料";
                }
                string 藥袋類型 = suspiciousRxLogClasses.藥袋類型;
                List<string> 錯誤類別 = suspiciousRxLogClasses.錯誤類別.Split(",").ToList();


                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "處方疑義紀錄表.xlsx");
                string loadText = MyOffice.ExcelClass.NPOI_LoadSheetsToJson_PreserveStyle(path);
                List<SheetClass> sheetClasslist = loadText.JsonDeserializet<List<SheetClass>>();
                SheetClass sheetClass = sheetClasslist[0];
                returnData.Data = sheetClass;
                Logger.Log("sheetClass", $"{returnData.JsonSerializationt(true)}");
                string today = DateTime.Now.ToDateString();
                sheetClass.Rows[1].Cell[2].Text = DateTime.Now.ToDateString();
                sheetClass.Rows[1].Cell[6].Text = suspiciousRxLogClasses.開方時間;
                if (藥袋類型 == enum_medBag_type.OPD.GetEnumName()) sheetClass.Rows[6].Cell[2].Text = "■";
                if (藥袋類型 == enum_medBag_type.ER.GetEnumName()) sheetClass.Rows[7].Cell[2].Text = "■";
                if (藥袋類型 == enum_medBag_type.ST.GetEnumName()) sheetClass.Rows[8].Cell[2].Text = "■";
                if (藥袋類型 == enum_medBag_type.MBD.GetEnumName()) sheetClass.Rows[9].Cell[2].Text = "■";
                if (藥袋類型 == enum_medBag_type.UD.GetEnumName()) sheetClass.Rows[10].Cell[2].Text = "■";

                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.A藥名錯誤.GetEnumName())) sheetClass.Rows[5].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.B途徑錯誤.GetEnumName())) sheetClass.Rows[6].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.C劑量錯誤.GetEnumName())) sheetClass.Rows[7].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.D頻率錯誤.GetEnumName())) sheetClass.Rows[8].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.E劑型錯誤.GetEnumName())) sheetClass.Rows[9].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.F數量錯誤.GetEnumName())) sheetClass.Rows[10].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.G多種藥物組合.GetEnumName())) sheetClass.Rows[11].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.H重複用藥.GetEnumName())) sheetClass.Rows[12].Cell[6].Text = "■";
                if (錯誤類別.Contains(enum_suspiciousRxLog_errorType.I其他.GetEnumName())) sheetClass.Rows[13].Cell[6].Text = "■";
                sheetClass.Rows[15].Cell[2].Text = suspiciousRxLogClasses.醫生姓名;
                sheetClass.Rows[15].Cell[6].Text = suspiciousRxLogClasses.病歷號;
                sheetClass.Rows[18].Cell[2].Text = suspiciousRxLogClasses.簡述事件;
                foreach (var item in sheetClass.CellValues)
                {
                    if (item.RowStart == 6 && item.RowStart == 6 && item.ColStart == 2 && item.ColStart == 2) item.Text = sheetClass.Rows[6].Cell[2].Text;
                    if (item.RowStart == 7 && item.RowStart == 7 && item.ColStart == 2 && item.ColStart == 2) item.Text = sheetClass.Rows[7].Cell[2].Text;
                    if (item.RowStart == 8 && item.RowStart == 8 && item.ColStart == 2 && item.ColStart == 2) item.Text = sheetClass.Rows[8].Cell[2].Text;
                    if (item.RowStart == 9 && item.RowStart == 9 && item.ColStart == 2 && item.ColStart == 2) item.Text = sheetClass.Rows[9].Cell[2].Text;
                    if (item.RowStart == 10 && item.RowStart == 10 && item.ColStart == 2 && item.ColStart == 2) item.Text = sheetClass.Rows[10].Cell[2].Text;

                    if (item.RowStart == 5 && item.RowStart == 5 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[5].Cell[6].Text;
                    if (item.RowStart == 6 && item.RowStart == 6 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[6].Cell[6].Text;
                    if (item.RowStart == 7 && item.RowStart == 7 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[7].Cell[6].Text;
                    if (item.RowStart == 8 && item.RowStart == 8 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[8].Cell[6].Text;
                    if (item.RowStart == 9 && item.RowStart == 9 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[9].Cell[6].Text;
                    if (item.RowStart == 10 && item.RowStart == 10 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[10].Cell[6].Text;
                    if (item.RowStart == 11 && item.RowStart == 11 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[11].Cell[6].Text;
                    if (item.RowStart == 12 && item.RowStart == 12 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[12].Cell[6].Text;
                    if (item.RowStart == 13 && item.RowStart == 13 && item.ColStart == 6 && item.ColStart == 6) item.Text = sheetClass.Rows[13].Cell[6].Text;

                    if (item.RowStart == 15 && item.RowStart == 16 && item.ColStart == 2 && item.ColStart == 3) item.Text = sheetClass.Rows[15].Cell[2].Text;
                    if (item.RowStart == 15 && item.RowStart == 16 && item.ColStart == 6 && item.ColStart == 7) item.Text = sheetClass.Rows[15].Cell[6].Text;
                    if (item.RowStart == 18 && item.RowStart == 37 && item.ColStart == 2 && item.ColStart == 9) item.Text = sheetClass.Rows[18].Cell[2].Text;
                }
                string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string xls_command = "application/vnd.ms-excel";
                sheetClasslist[0] = sheetClass;
                byte[] excelData = sheetClasslist.NPOI_GenerateExcelWithPreservedStyle(Excel_Type.xlsx);

                Stream stream = new MemoryStream(excelData);
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_醫師處方疑義紀錄表(個別).xlsx"));
            }
            catch
            {
                return null;
            }

        }
        private List<Prescription> GroupOrderList(List<OrderClass> OrderClasses, suspiciousRxLogClass suspiciousRxLogClass)
        {
            List<medClass> medClasses = medClass.get_med_cloud(API_Server);
            Dictionary<string, List<medClass>> medClassDict = medClasses.CoverToDictionaryByCode();
            List<Prescription> cpoeList = OrderClasses
                .GroupBy(temp => temp.藥袋條碼)
                .Select(group =>
                {
                    OrderClass orderClass = group.First();
                    List<DrugOrder> drugOrders = group
                    .Select(value =>
                    {
                        medClass med = medClassDict.SortDictionaryByCode(value.藥品碼).FirstOrDefault();
                        if (med == null) return null;

                        return new DrugOrder
                        {
                            藥品名稱 = value.藥品名稱,
                            藥品碼 = value.藥品碼,
                            費用別 = value.費用別,
                            交易量 = value.交易量.Replace("-", ""),
                            頻次 = value.頻次,
                            天數 = value.天數,
                            單次劑量 = value.單次劑量,
                            劑量單位 = value.劑量單位,
                            類別 = med.類別,
                            健保碼 = med.健保碼,
                            ATC = med.ATC,
                            藥品學名 = med.藥品學名,
                            藥品許可證號 = med.藥品許可證號,
                            管制級別 = med.管制級別,
                        };

                    })
                    .Where(value => value != null)
                    .ToList();
                    bool result = group.Any(item =>
                     item.病人姓名.StartsWith("金") ||
                     item.病人姓名.StartsWith("朴") ||
                     item.病人姓名.StartsWith("崔") ||
                      Regex.IsMatch(item.病人姓名, @"^[A-Za-z]+$"));
                    return new Prescription
                    {

                        藥袋條碼 = group.Key,
                        產出時間 = orderClass.產出時間,
                        醫師代碼 = group.Any(item => item.醫師代碼 == item.病人姓名).ToString(),
                        病人姓名 = result.ToString(),
                        處方 = drugOrders,
                        科別 = orderClass.科別,
                        診斷碼 = suspiciousRxLogClass.診斷碼,
                        診斷內容 = suspiciousRxLogClass.診斷內容
                    };
                }).ToList();
            return cpoeList;

        }
        private string CheckCreatTable(string tableName, Enum Enum)
        {

            if (tableName == null)
            {
                tableName = Enum.GetEnumDescription();
            }
            SQLUI.Table table = new SQLUI.Table(tableName);
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
            if (sys_serverSettingClasses.Count == 0)
            {
                return $"找無Server資料!";
            }
            table = MethodClass.CheckCreatTable(sys_serverSettingClasses[0], Enum, tableName);

            return table.JsonSerializationt(true);
        }
        private void AddsuspiciousRxLog(List<OrderClass> orderClasses, string ICD1 = "C93.30", string ICD2 = "", string ICD3 = "")
        {
            if (orderClasses.Count == 0) return;
            List<suspiciousRxLogClass> suspiciousRxLoges = suspiciousRxLogClass.get_by_barcode(API_Server, orderClasses[0].藥袋條碼);
            suspiciousRxLogClass suspiciousRxLogClasses = new suspiciousRxLogClass();

            if (suspiciousRxLoges.Count == 0)
            {
                List<string> disease_list = new List<string>();
                List<diseaseClass> diseaseClasses = new List<diseaseClass>();
                if (ICD1.StringIsEmpty() == false) disease_list.Add(ICD1);
                if (ICD2.StringIsEmpty() == false) disease_list.Add(ICD2);
                if (ICD3.StringIsEmpty() == false) disease_list.Add(ICD3);
                if (disease_list.Count > 0) diseaseClasses = diseaseClass.get_by_ICD(API_Server, disease_list);

                suspiciousRxLogClasses = new suspiciousRxLogClass()
                {
                    GUID = Guid.NewGuid().ToString(),
                    藥袋條碼 = orderClasses[0].藥袋條碼,
                    加入時間 = DateTime.Now.ToDateTimeString(),
                    病歷號 = orderClasses[0].病歷號,
                    科別 = orderClasses[0].科別,
                    醫生姓名 = orderClasses[0].醫師代碼,
                    開方時間 = orderClasses[0].開方日期,
                    藥袋類型 = orderClasses[0].藥袋類型,
                    //錯誤類別 = string.Join(",", suspiciousRxLog.error_type),
                    //簡述事件 = suspiciousRxLog.response,
                    狀態 = enum_suspiciousRxLog_status.未辨識.GetEnumName(),
                    //調劑人員 = orderClasses[0].藥師姓名,
                    調劑時間 = DateTime.Now.ToDateTimeString(),
                    //提報等級 = enum_suspiciousRxLog_ReportLevel.Normal.GetEnumName(),
                    提報時間 = DateTime.MinValue.ToDateTimeString(),
                    處理時間 = DateTime.MinValue.ToDateTimeString(),
                    diseaseClasses = diseaseClasses
                };
                suspiciousRxLogClass.add(API_Server, suspiciousRxLogClasses);
            }
        }
    }
}