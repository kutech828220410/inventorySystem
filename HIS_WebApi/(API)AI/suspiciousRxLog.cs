using Microsoft.AspNetCore.Mvc;
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
                return CheckCreatTable("suspiciousRxLog_rule_local",new enum_suspiciousRxLog_rule());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 新增或修改藥品設定
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
                foreach(var item in suspiciousRxLogRule_locals)
                {
                    item.GUID = Guid.NewGuid().ToString();
                    item.類別 = "local";
                }
                List<object[]> list_suspiciousRxLogRule = suspiciousRxLogRule_locals.ClassToSQL<suspiciousRxLog_ruleClass, enum_suspiciousRxLog_rule>();
                sQLControl.AddRows(null, list_suspiciousRxLogRule);               

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = suspiciousRxLogRule_locals;
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
        /// <summary>
        /// 以order_PRI_KEY(藥袋條碼)取得資料
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
                if (suspiciousRxLogClasses.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data筆數超過1筆";
                    return returnData.JsonSerializationt();
                }
                string GUID = suspiciousRxLogClasses[0].GUID;

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
        [HttpPost("medGPT")]
        public string medGPT(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "api/suspiciousRxLog/medGPT";
            try
            {
                List<OrderClass> orders = returnData.Data.ObjToClass<List<OrderClass>>();
                if(orders.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data應為List<OrderClass>";
                    return returnData.JsonSerializationt(true);
                }

                string 藥袋條碼 = orders[0].藥袋條碼;
                List<suspiciousRxLogClass> suspiciousRxLoges = suspiciousRxLogClass.get_by_barcode(API_Server, 藥袋條碼);
                if (suspiciousRxLoges.Count > 0)
                {
                    suspiciousRxLoges[0].辨識註記 = "Y";
                    returnData.Code = 200;
                    returnData.Data = suspiciousRxLoges;
                    returnData.Result = $"條碼{藥袋條碼}已辨識過";
                    return returnData.JsonSerializationt(true);
                }
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
                    old_cpoe = GroupOrderList(history_order);
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    eff_cpoe = GroupOrderList(orders);
                })));
                Task.WhenAll(tasks).Wait();

                PrescriptionSet result = new PrescriptionSet
                {
                    有效處方 = eff_cpoe,
                    歷史處方 = old_cpoe
                };
                string url = Method.GetServerAPI("Main", "網頁", "medgpt_api");
                //string url = @"https://www.kutech.tw:3000/medgpt";
                suspiciousRxLogClass suspiciousRxLogClasses = new suspiciousRxLogClass();
                
                suspiciousRxLogClass suspiciousRxLog = suspiciousRxLogClass.Excute(url, result);
                returnData.Data = suspiciousRxLog;
                Logger.LogAddLine();
                Logger.Log("suspiciousRxLog", returnData.Data.JsonSerializationt(true));
                if (suspiciousRxLog.error == true.ToString())
                {
                    if (suspiciousRxLog.response.Contains("None科別"))
                    {
                        suspiciousRxLog.response = suspiciousRxLog.response.Replace("None科別", $"{orders[0].科別}");
                    }
                    suspiciousRxLogClasses = new suspiciousRxLogClass()
                    {
                        GUID = Guid.NewGuid().ToString(),
                        藥袋條碼 = orders[0].藥袋條碼,
                        加入時間 = DateTime.Now.ToDateTimeString(),
                        病歷號 = 病歷號,
                        科別 = orders[0].科別,
                        醫生姓名 = orders[0].醫師代碼,
                        開方時間 = orders[0].開方日期,
                        藥袋類型 = orders[0].藥袋類型,
                        錯誤類別 = string.Join(",", suspiciousRxLog.error_type),
                        簡述事件 = suspiciousRxLog.response,
                        狀態 = enum_suspiciousRxLog_status.未更改.GetEnumName(),
                        調劑人員 = orders[0].藥師姓名,
                        調劑時間 = orders[0].過帳時間,
                        提報等級 = enum_suspiciousRxLog_ReportLevel.Normal.GetEnumName(),
                        提報時間 = DateTime.MinValue.ToDateTimeString(),
                        處理時間 = DateTime.MinValue.ToDateTimeString(),
                    };
                    suspiciousRxLogClass.add(API_Server, suspiciousRxLogClasses);
                }
                else
                {
                    suspiciousRxLogClasses = new suspiciousRxLogClass()
                    {
                        GUID = Guid.NewGuid().ToString(),
                        藥袋條碼 = orders[0].藥袋條碼,
                        加入時間 = DateTime.Now.ToDateTimeString(),
                        病歷號 = 病歷號,
                        科別 = orders[0].科別,
                        醫生姓名 = orders[0].醫師代碼,
                        開方時間 = orders[0].開方日期,
                        藥袋類型 = orders[0].藥袋類型,
                        錯誤類別 = string.Join(",", suspiciousRxLog.error_type),
                        簡述事件 = suspiciousRxLog.response,
                        狀態 = enum_suspiciousRxLog_status.無異狀.GetEnumName(),
                        調劑人員 = orders[0].藥師姓名,
                        調劑時間 = orders[0].過帳時間,
                        提報時間 = DateTime.MinValue.ToDateTimeString(),
                        處理時間 = DateTime.MinValue.ToDateTimeString(),
                    };
                    suspiciousRxLogClass.add(API_Server, suspiciousRxLogClasses);
                }

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

        private List<Prescription> GroupOrderList(List<OrderClass> OrderClasses)
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
    }
}
