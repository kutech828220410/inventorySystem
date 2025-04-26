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
using System.Data;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugHFTag : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        ///初始化dbvm.DrugHFTag資料庫
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[""]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "DrugHFTag物件", typeof(DrugHFTagClass))]
        public string init([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "init";
            returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0]);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }

        /// <summary>
        /// 新增標籤資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [DrugHFTagClass陣列]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                List<DrugHFTagClass> drugHFTagClasses = returnData.Data.ObjToClass<List<DrugHFTagClass>>();
                if (drugHFTagClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_drugHFTag_buf = new List<object[]>();
                List<object[]> list_drugHFTag_add = new List<object[]>();
                List<object[]> list_drugHFTag_replace = new List<object[]>();
                for (int i = 0; i < drugHFTagClasses.Count; i++)
                {
                    DrugHFTagClass drugHFTagClass = drugHFTagClasses[i];
                    if (drugHFTagClass.TagSN.StringIsEmpty()) continue;
                    if(drugHFTagClass.效期.StringIsEmpty()) continue;
                    drugHFTagClass.GUID = Guid.NewGuid().ToString();
                    drugHFTagClass.更新時間 = DateTime.Now.ToDateTimeString_6();
                    list_drugHFTag_add.Add(drugHFTagClass.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>());
                }
                sQLControl_drugHFTag.AddRows(null, list_drugHFTag_add);
                sQLControl_drugHFTag.UpdateByDefulteExtra(null, list_drugHFTag_replace);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"新增標籤資料成功,共新增<{list_drugHFTag_add.Count}>筆資料";
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                string json = returnData.JsonSerializationt(true);
                Logger.Log("DrugHFTag", json);
                return json;
            }
        }

        /// <summary>
        /// 以TagSN取得最新標籤資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [tagSN1,tagSN2...]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_tag_ByTagSN")]
        [HttpPost]
        public string get_latest_tag_ByTagSN([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_tag_ByTagSN";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[tagsSN]";
                    return returnData.JsonSerializationt(true);
                }
                string[] codes = returnData.ValueAry[0].Split(",");
                if (codes.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[tagsSN]";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < codes.Length; i++)
                {
                    codes[i] = codes[i].Trim();
                }
                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string sqlList = string.Join(",", codes.Select(code => $"'{code}'"));
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();

                string command = $@"
                                SELECT * FROM {DB}.{TableName} t1
                                WHERE UPPER(t1.{tagCol}) IN ({sqlList})
                                AND t1.{timeCol} = (
                                    SELECT MAX(t2.{timeCol})
                                    FROM {DB}.{TableName} t2
                                    WHERE UPPER(t2.{tagCol}) = UPPER(t1.{tagCol})
                                )";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"取得標籤資料成功,共<{drugHFTagClasses.Count}>筆資料";
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
        /// 取得所有Tag中最新一筆且狀態為「已重置」的標籤資料
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_reset_tag")]
        [HttpPost]
        public string get_latest_reset_tag([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_reset_tag";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string command = $@"
                                SELECT * FROM {DB}.{TableName} t1
                                WHERE t1.{statusCol} = '已重置'
                                  AND t1.{timeCol} = (
                                    SELECT MAX(t2.{timeCol})
                                    FROM {DB}.{TableName} t2
                                    WHERE UPPER(t2.{tagCol}) = UPPER(t1.{tagCol})
                                  )";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"取得最新'已重置'標籤資料成功，共<{drugHFTagClasses.Count}>筆資料";
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
        /// 取得所有Tag中最新一筆且狀態為「入庫註記」的標籤資料
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_stockin_tag")]
        [HttpPost]
        public string get_latest_stockin_tag([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_stockin_tag";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string command = $@"
                        SELECT * FROM {DB}.{TableName} t1
                        WHERE t1.{statusCol} = '入庫註記'
                          AND t1.{timeCol} = (
                            SELECT MAX(t2.{timeCol})
                            FROM {DB}.{TableName} t2
                            WHERE UPPER(t2.{tagCol}) = UPPER(t1.{tagCol})
                          )";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"取得最新'入庫註記'標籤資料成功，共<{drugHFTagClasses.Count}>筆資料";
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
        /// 取得所有Tag中最新一筆且「可入庫」的標籤資料（狀態為「出庫註記」或「已重置」）
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_stockin_eligible_tags")]
        [HttpPost]
        public string get_latest_stockin_eligible_tags([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_stockin_eligible_tags";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string command = $@"
                SELECT * FROM {DB}.{TableName} t1
                WHERE t1.{statusCol} IN ('出庫註記', '已重置')
                  AND t1.{timeCol} = (
                    SELECT MAX(t2.{timeCol})
                    FROM {DB}.{TableName} t2
                    WHERE UPPER(t2.{tagCol}) = UPPER(t1.{tagCol})
                  )";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"取得最新「可入庫」標籤資料成功，共<{drugHFTagClasses.Count}>筆資料";
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
        /// 取得所有Tag中最新一筆且「可出庫」的標籤資料（狀態為「入庫註記」）
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_latest_stockout_eligible_tags")]
        [HttpPost]
        public string get_latest_stockout_eligible_tags([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_latest_stockout_eligible_tags";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_value = new List<object[]>();
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();
                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string statusCol = enum_DrugHFTag.狀態.GetEnumName();

                string command = $@"
                            SELECT * FROM {DB}.{TableName} t1
                            WHERE t1.{statusCol} = '入庫註記'
                              AND t1.{timeCol} = (
                                SELECT MAX(t2.{timeCol})
                                FROM {DB}.{TableName} t2
                                WHERE UPPER(t2.{tagCol}) = UPPER(t1.{tagCol})
              )";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = drugHFTagClasses;
                returnData.Result = $"取得最新「可出庫」標籤資料成功，共<{drugHFTagClasses.Count}>筆資料";
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
        /// 依指定日期範圍，並可選擇指定藥碼或藥名關鍵字，統計HF標籤的入庫/未入庫/狀態分類數量（數量型別為double）
        /// </summary>
        /// <remarks>
        /// <para>ValueAry 傳入資料說明：</para>
        /// <list type="bullet">
        /// <item><description>ValueAry[0]: 起始時間 (必填，例如："2025-04-01 00:00:00")</description></item>
        /// <item><description>ValueAry[1]: 結束時間 (必填，例如："2025-04-30 23:59:59")</description></item>
        /// <item><description>ValueAry[2]: 指定藥碼 (可選填)</description></item>
        /// <item><description>ValueAry[3]: 藥名關鍵字 (可選填)</description></item>
        /// </list>
        /// </remarks>
        /// <param name="returnData">共用資料結構，包含 ValueAry 參數</param>
        /// <returns>回傳統計結果（double型態數量）</returns>
        [Route("get_stockin_status_detail_count_in_range_with_filter")]
        [HttpPost]
        public string get_stockin_status_detail_count_in_range_with_filter([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_stockin_status_detail_count_in_range_with_filter";

            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                if (returnData.ValueAry.Count < 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤，至少包含 start_date, end_date";
                    return returnData.JsonSerializationt();
                }

                string start_date = returnData.ValueAry[0];
                string end_date = returnData.ValueAry[1];

                string filterDrugCode = returnData.ValueAry.Count > 2 ? returnData.ValueAry[2] : "";
                string filterDrugName = returnData.ValueAry.Count > 3 ? returnData.ValueAry[3] : "";

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string command = $@"
            SELECT * FROM {DB}.{TableName}
            WHERE {timeCol} >= '{start_date}'
              AND {timeCol} <= '{end_date}'
        ";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                List<object[]> list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> allTags = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                if (!string.IsNullOrEmpty(filterDrugCode))
                {
                    allTags = allTags.Where(t => t.藥碼 != null && t.藥碼.Contains(filterDrugCode, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(filterDrugName))
                {
                    allTags = allTags.Where(t => t.藥名 != null && t.藥名.Contains(filterDrugName, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var grouped = allTags.GroupBy(tag => tag.TagSN);

                double 已重置數量 = 0;
                double 入庫註記數量 = 0;
                double 出庫註記數量 = 0;
                double 進入儲位數量 = 0;
                double 離開儲位數量 = 0;
                double 已入庫數量 = 0;
                double 未入庫數量 = 0;

                foreach (var group in grouped)
                {
                    var tags = group.OrderByDescending(t => t.更新時間.StringToDateTime()).ToList();

                    bool hasReset = tags.Any(t => t.狀態 == enum_DrugHFTagStatus.已重置.ToString());
                    if (!hasReset) continue;

                    var latestTag = tags.FirstOrDefault();
                    if (latestTag != null)
                    {
                        double qty = latestTag.數量.StringToDouble();

                        switch (latestTag.狀態)
                        {
                            case "已重置":
                                已重置數量 += qty;
                                break;
                            case "入庫註記":
                                入庫註記數量 += qty;
                                break;
                            case "出庫註記":
                                出庫註記數量 += qty;
                                break;
                            case "進入儲位":
                                進入儲位數量 += qty;
                                break;
                            case "離開儲位":
                                離開儲位數量 += qty;
                                break;
                        }

                        if (latestTag.狀態 == enum_DrugHFTagStatus.入庫註記.ToString())
                        {
                            已入庫數量 += qty;
                        }
                        else
                        {
                            未入庫數量 += qty;
                        }
                    }
                }

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = new
                {
                    已重置數量 = 已重置數量,
                    入庫註記數量 = 入庫註記數量,
                    出庫註記數量 = 出庫註記數量,
                    進入儲位數量 = 進入儲位數量,
                    離開儲位數量 = 離開儲位數量,
                    已入庫數量 = 已入庫數量,
                    未入庫數量 = 未入庫數量,
                    總數量 = 已入庫數量 + 未入庫數量
                };
                returnData.Result = $"條件細分統計完成";
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
        /// 依藥碼陣列，統計各藥碼的標籤入庫/未入庫/狀態分類數量（數量型別為double）
        /// </summary>
        /// <remarks>
        /// ValueAry[0]：起始時間 (yyyy-MM-dd HH:mm:ss)
        /// ValueAry[1]：結束時間 (yyyy-MM-dd HH:mm:ss)
        /// Data：藥碼陣列 List&lt;string&gt;
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>回傳各藥碼對應的 DrugHFTagStatusSummaryByCode 列表</returns>
        [Route("get_stockin_status_detail_summary_by_codes")]
        [HttpPost]
        public string get_stockin_status_detail_summary_by_codes([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_stockin_status_detail_summary_by_codes";

            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                if (returnData.ValueAry.Count < 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤，至少需包含 startDate 與 endDate";
                    return returnData.JsonSerializationt();
                }

                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤，需包含藥碼陣列";
                    return returnData.JsonSerializationt();
                }

                string startDate = returnData.ValueAry[0];
                string endDate = returnData.ValueAry[1];
                List<string> drugCodes = returnData.Data.ObjToClass<List<string>>();
                if (drugCodes == null || drugCodes.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入藥碼陣列為空";
                    return returnData.JsonSerializationt();
                }

                string Server = _sys_serverSettingClasses[0].Server;
                string DB = _sys_serverSettingClasses[0].DBName;
                string UserName = _sys_serverSettingClasses[0].User;
                string Password = _sys_serverSettingClasses[0].Password;
                uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                string TableName = new enum_DrugHFTag().GetEnumDescription();
                SQLControl sQLControl_drugHFTag = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                string timeCol = enum_DrugHFTag.更新時間.GetEnumName();
                string drugCodeCol = enum_DrugHFTag.藥碼.GetEnumName();
                string tagCol = enum_DrugHFTag.TagSN.GetEnumName();

                string sqlList = string.Join(",", drugCodes.Select(code => $"'{code}'"));

                string command = $@"
            SELECT * FROM {DB}.{TableName}
            WHERE {timeCol} >= '{startDate}'
              AND {timeCol} <= '{endDate}'
              AND {drugCodeCol} IN ({sqlList})
        ";

                DataTable dataTable = sQLControl_drugHFTag.WtrteCommandAndExecuteReader(command);
                List<object[]> list_value = dataTable.DataTableToRowList();
                List<DrugHFTagClass> allTags = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();

                var groupedByDrugCode = allTags.GroupBy(t => t.藥碼);
                List<DrugHFTagStatusSummaryByCode> summaryList = new List<DrugHFTagStatusSummaryByCode>();

                foreach (var group in groupedByDrugCode)
                {
                    var groupedByTagSN = group.GroupBy(t => t.TagSN);

                    double 已重置數量 = 0;
                    double 入庫註記數量 = 0;
                    double 出庫註記數量 = 0;
                    double 進入儲位數量 = 0;
                    double 離開儲位數量 = 0;
                    double 已入庫數量 = 0;
                    double 未入庫數量 = 0;

                    foreach (var tagGroup in groupedByTagSN)
                    {
                        var latestTag = tagGroup.OrderByDescending(t => t.更新時間.StringToDateTime()).FirstOrDefault();
                        if (latestTag == null) continue;

                        bool hasReset = tagGroup.Any(t => t.狀態 == enum_DrugHFTagStatus.已重置.ToString());
                        if (!hasReset) continue;

                        double qty = latestTag.數量.StringToDouble();

                        switch (latestTag.狀態)
                        {
                            case "已重置":
                                已重置數量 += qty;
                                break;
                            case "入庫註記":
                                入庫註記數量 += qty;
                                break;
                            case "出庫註記":
                                出庫註記數量 += qty;
                                break;
                            case "進入儲位":
                                進入儲位數量 += qty;
                                break;
                            case "離開儲位":
                                離開儲位數量 += qty;
                                break;
                        }

                        if (latestTag.狀態 == enum_DrugHFTagStatus.入庫註記.ToString())
                        {
                            已入庫數量 += qty;
                        }
                        else
                        {
                            未入庫數量 += qty;
                        }
                    }

                    summaryList.Add(new DrugHFTagStatusSummaryByCode
                    {
                        藥碼 = group.Key,
                        已重置數量 = 已重置數量,
                        入庫註記數量 = 入庫註記數量,
                        出庫註記數量 = 出庫註記數量,
                        進入儲位數量 = 進入儲位數量,
                        離開儲位數量 = 離開儲位數量,
                        已入庫數量 = 已入庫數量,
                        未入庫數量 = 未入庫數量,
                        總數量 = 已入庫數量 + 未入庫數量
                    });
                }

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = summaryList;
                returnData.Result = $"取得各藥碼統計成功，共<{summaryList.Count}>筆資料";
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
            string[] temp = new enum_DrugHFTag().GetEnumNames();
            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_DrugHFTag());
            return table.JsonSerializationt(true);
        }
    }
}
