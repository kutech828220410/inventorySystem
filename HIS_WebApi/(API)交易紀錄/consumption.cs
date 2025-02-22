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
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class consumptionController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 取得庫存及消耗總量
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "[起始日期],[結束日期]"
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[consumption]陣列結構</returns>
        [Route("serch_by_ST_END")]
        [HttpPost]
        public string POST_serch_consumption_by_ST_END([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "serch_by_ST_END";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClass sys_ServerSetting_trading = sys_serverSettingClasses.myFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
                if (sys_ServerSetting_trading == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_ServerSetting_trading.Server;
                string DB = sys_ServerSetting_trading.DBName;
                string UserName = sys_ServerSetting_trading.User;
                string Password = sys_ServerSetting_trading.Password;
                uint Port = (uint)sys_ServerSetting_trading.Port.StringToInt32();

                SQLControl sQLControl_trading = new SQLControl(Server, DB, "trading", UserName, Password, Port, SSLMode);

                string[] input_value = returnData.Value.Split(",");
                if (input_value.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "輸入資訊錯誤!";
                    returnData.Data = "";
                    return returnData.JsonSerializationt();
                }
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
                DateTime date_st = date_ary[0].StringToDateTime().GetStartDate();
                DateTime date_end = date_ary[1].StringToDateTime().GetEndDate();

                List<object[]> list_tradding = sQLControl_trading.GetRowsByBetween(null, (int)enum_交易記錄查詢資料.操作時間, date_ary[0], date_ary[1]);
                List<object[]> list_tradding_buf = new List<object[]>();
                List<object> Code_LINQ = (from value in list_tradding
                                          select value[(int)enum_交易記錄查詢資料.藥品碼]).Distinct().ToList();


                List<medClass> medClasses_dps = medClass.get_dps_medClass("http://127.0.0.1:4433", returnData.ServerName);
                List<medClass> medClasses_dps_buf = new List<medClass>();
                System.Collections.Generic.Dictionary<string, List<medClass>> keyValuePairs = medClass.CoverToDictionaryByCode(medClasses_dps);
                medClasses_dps = (from temp in medClasses_dps
                                  where (temp.DeviceBasics.Count > 0)
                                  where (temp.藥品碼.StringIsEmpty() == false)
                                  select temp).ToList();
                List<object[]> list_consumption = new List<object[]>();
                for (int i = 0; i < medClasses_dps.Count; i++)
                {
                    string 藥碼 = medClasses_dps[i].藥品碼;
                    string 藥名 = medClasses_dps[i].藥品名稱;
                    string 庫存 = medClasses_dps[i].庫存;
                    double 交易量 = 0;
                    list_tradding_buf = list_tradding.GetRows((int)enum_交易記錄查詢資料.藥品碼, 藥碼);
                    for (int k = 0; k < list_tradding_buf.Count; k++)
                    {
                        交易量 += list_tradding_buf[k][(int)enum_交易記錄查詢資料.交易量].StringToDouble();
                    }
                    object[] value = new object[new enum_consumption().GetLength()];
                    value[(int)enum_consumption.藥碼] = 藥碼;
                    value[(int)enum_consumption.藥名] = 藥名;
                    value[(int)enum_consumption.庫存量] = 庫存;
                    value[(int)enum_consumption.消耗量] = 交易量;
                    list_consumption.Add(value);
                }


                List<consumptionClass> consumptionClasses = list_consumption.SQLToClass<consumptionClass, enum_consumption>();
                returnData.Code = 200;
                returnData.Result = $"取得消耗量表成功,共<{consumptionClasses.Count}>筆";
                returnData.Data = consumptionClasses;
                returnData.TimeTaken = $"{myTimer}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);

            }
        }
        /// <summary>
        /// 取得庫存及消耗總量報表結構(SheetClass)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "[起始日期],[結束日期]"
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[SheetClass]陣列結構</returns>
        [Route("get_sheet_by_serch")]
        [HttpPost]
        public string POST_get_sheet_by_serch([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "交易紀錄資料");
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



                string json = POST_serch_consumption_by_ST_END(returnData);
                returnData = json.JsonDeserializet<returnData>();

                if (returnData.Code != 200)
                {
                    return null;
                }
                List<consumptionClass> consumptionClasses = returnData.Data.ObjToListClass<consumptionClass>();



                string loadText = Basic.MyFileStream.LoadFileAllText(@"./excel_consumption.txt", "utf-8");
                Console.WriteLine($"取得creats {myTimer.ToString()}");
                int row_max = 5000;
                List<SheetClass> sheetClasses = new List<SheetClass>();
                SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();

                string[] date_ary = returnData.Value.Split(',');
                double 消耗量 = 0;
                int NumOfRow = -1;
                for (int i = 0; i < consumptionClasses.Count; i++)
                {
                    if (NumOfRow >= row_max || NumOfRow == -1)
                    {
                        sheetClass = loadText.JsonDeserializet<SheetClass>();
                        sheetClass.Name = $"{i}";
                        sheetClass.ReplaceCell(1, 2, $"{date_ary[0]}");
                        sheetClass.ReplaceCell(2, 2, $"{date_ary[1]}");

                        sheetClasses.Add(sheetClass);
                        NumOfRow = 0;
                    }

                    消耗量 += consumptionClasses[i].消耗量.StringToDouble();
                    消耗量 *= -1;
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 0, $"{i + 1}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 1, $"{consumptionClasses[i].藥碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 2, $"{consumptionClasses[i].藥名}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 3, $"{consumptionClasses[i].消耗量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 4, $"{consumptionClasses[i].結存量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);

                    NumOfRow++;
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
        /// <summary>
        /// 取得庫存及消耗總量Excel(xlxs)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///     
        ///     },
        ///     "Value" : "[起始日期],[結束日期]"
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[SheetClass]陣列結構</returns>
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
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_消耗量表.xlsx"));
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 取得庫存及消耗總量(多台合併)
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
        ///       "結束時間",
        ///       "口服1,口服2",
        ///       "調劑台,調劑台"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[consumption]陣列結構</returns>
        [Route("serch_datas_by_ST_END")]
        [HttpPost]
        public string POST_serch_datas_by_ST_END([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "serch_datas_by_ST_END";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();

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

                List<Task> tasks = new List<Task>();
                List<List<consumptionClass>> list_consumptionClasses = new List<List<consumptionClass>>();
                for (int i = 0; i < ServerNames.Length; i++)
                {
                    string serverName = ServerNames[i];
                    string serverType = ServerTypes[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<consumptionClass> consumptionClasses = new List<consumptionClass>();
                        List<sys_serverSettingClass> _sys_serverSettingClasses = sys_serverSettingClasses.MyFind(serverName, serverType, "交易紀錄資料");
                        if (_sys_serverSettingClasses.Count == 0) return;
                        string Server = _sys_serverSettingClasses[0].Server;
                        string DB = _sys_serverSettingClasses[0].DBName;
                        string UserName = _sys_serverSettingClasses[0].User;
                        string Password = _sys_serverSettingClasses[0].Password;
                        uint Port = (uint)_sys_serverSettingClasses[0].Port.StringToInt32();
                        string TableName = "trading";
                        SQLControl sQLControl_trading = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                        List<object[]> list_tradding = sQLControl_trading.GetRowsByBetween(null, (int)enum_交易記錄查詢資料.操作時間, 起始時間, 結束時間);
                        List<object> Code_LINQ = (from value in list_tradding
                                                  select value[(int)enum_交易記錄查詢資料.藥品碼]).Distinct().ToList();

                        List<medClass> medClasses_dps = medClass.get_dps_medClass("http://127.0.0.1:4433", serverName);
                        System.Collections.Generic.Dictionary<string, List<medClass>> keyValuePairs_med_dps = medClass.CoverToDictionaryByCode(medClasses_dps);

                        List<medClass> medClasses_dps_buf = new List<medClass>();

                        List<transactionsClass> transactionsClasses = list_tradding.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                        transactionsClasses = (from temp in transactionsClasses
                                               where temp.動作.Contains("撥入")
                                               || temp.動作.Contains("撥出")
                                               || temp.動作.Contains("調入")
                                               || temp.動作.Contains("調出")
                                               || temp.動作.Contains("退藥")
                                               || temp.動作.Contains("加藥")
                                               || temp.動作.Contains("補藥")
                                               || temp.動作.Contains("領藥")
                                               || temp.動作.Contains("入庫")
                                               || temp.動作.Contains("出庫")
                                               select temp).ToList();
                        List<transactionsClass> transactionsClasses_buf = new List<transactionsClass>();
                        System.Collections.Generic.Dictionary<string, List<transactionsClass>> keyValues_transactionsClass = transactionsClass.CoverToDictionaryByCode(transactionsClasses);


                        for (int i = 0; i < Code_LINQ.Count; i++)
                        {
                            object[] value = new object[new enum_consumption().GetLength()];
                            string Code = Code_LINQ[i].ObjectToString();
                            if (Code.StringIsEmpty()) continue;
                            transactionsClasses_buf = transactionsClass.SortDictionaryByCode(keyValues_transactionsClass, Code);

                            double 實調量 = 0;
                            if (transactionsClasses_buf.Count > 0)
                            {
                                transactionsClasses_buf.Sort(new ICP_transactionsClass());
                                consumptionClass consumptionClass = new consumptionClass();
                                consumptionClass.藥碼 = transactionsClasses_buf[0].藥品碼;
                                consumptionClass.藥名 = transactionsClasses_buf[0].藥品名稱;
                                for (int k = 0; k < transactionsClasses_buf.Count; k++)
                                {
                                    實調量 += transactionsClasses_buf[0].交易量.StringToDouble();
                                }
                                實調量 = 實調量 * -1;
                                consumptionClass.實調量 = 實調量.ToString();




                                medClasses_dps_buf = medClass.SortDictionaryByCode(keyValuePairs_med_dps, Code);
                                if (medClasses_dps_buf.Count > 0) consumptionClass.庫存量 = medClasses_dps_buf[0].庫存;


                                consumptionClasses.Add(consumptionClass);
                            }
                        }
                        list_consumptionClasses.Add(consumptionClasses);
                    })));
                }
                Task.WhenAll(tasks).Wait();
                sys_serverSettingClass sys_ServerSetting_order = sys_serverSettingClasses.myFind("Main", "網頁", "VM端");
                List<OrderClass> orderClasses = OrderClass.get_by_rx_time_st_end("http://127.0.0.1:4433", date_st, date_end);
                Dictionary<string, List<OrderClass>> keyValuePairs_orders = orderClasses.CoverToDictionaryBy_Code();
                List<OrderClass> orderClasses_buf = new List<OrderClass>();


                List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
                List<medClass> medClasses_cloud_buf = new List<medClass>();
                System.Collections.Generic.Dictionary<string, List<medClass>> keyValuePairs_med_cloud = medClass.CoverToDictionaryByCode(medClasses_cloud);


                List<consumptionClass> consumptionClasses = new List<consumptionClass>();
                List<consumptionClass> consumptionClasse_buf = new List<consumptionClass>();
                for (int i = 0; i < list_consumptionClasses.Count; i++)
                {
                    foreach (consumptionClass value in list_consumptionClasses[i])
                    {

                        consumptionClasse_buf = (from temp in consumptionClasses
                                                 where temp.藥碼 == value.藥碼
                                                 select temp).ToList();
                        if (consumptionClasse_buf.Count > 0)
                        {

                            consumptionClasse_buf[0].實調量 = (consumptionClasse_buf[0].實調量.StringToDouble() + value.實調量.StringToDouble()).ToString();
                            consumptionClasse_buf[0].庫存量 = (consumptionClasse_buf[0].庫存量.StringToDouble() + value.庫存量.StringToDouble()).ToString();
                        }
                        else
                        {
                            consumptionClasses.Add(value);
                        }
                    }
                }

                for (int i = 0; i < consumptionClasses.Count; i++)
                {
                    medClasses_cloud_buf = medClass.SortDictionaryByCode(keyValuePairs_med_cloud, consumptionClasses[i].藥碼);
                    if (medClasses_cloud_buf.Count > 0) consumptionClasses[i].類別 = medClasses_cloud_buf[0].類別;
                    orderClasses_buf = keyValuePairs_orders.SerchDictionary(consumptionClasses[i].藥碼);

                    double 消耗量 = 0;
                    for (int k = 0; k < orderClasses_buf.Count; k++)
                    {
                        消耗量 += orderClasses_buf[k].交易量.StringToDouble() * -1;
                    }
                    consumptionClasses[i].消耗量 = 消耗量.ToString();
                }


                returnData.Code = 200;
                returnData.Result = $"取得總消耗量表成功,{returnData.ValueAry[2]}";
                returnData.Data = consumptionClasses;
                returnData.TimeTaken = $"{myTimer}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);

            }
        }
        /// <summary>
        /// 取得庫存及消耗總量報表結構(SheetClass)(多台合併)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
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
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[SheetClass]陣列結構</returns>
        [Route("get_datas_sheet_by_serch")]
        [HttpPost]
        public string POST_get_datas_sheet_by_serch([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "get_datas_sheet_by_serch";
            try
            {
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


                string json = POST_serch_datas_by_ST_END(returnData);
                returnData = json.JsonDeserializet<returnData>();

                if (returnData.Code != 200)
                {
                    return null;
                }
                List<consumptionClass> consumptionClasses = returnData.Data.ObjToListClass<consumptionClass>();



                string loadText = Basic.MyFileStream.LoadFileAllText(@"./excel_consumption.txt", "utf-8");
                Console.WriteLine($"取得creats {myTimer.ToString()}");
                int row_max = 5000;
                List<SheetClass> sheetClasses = new List<SheetClass>();
                SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();

                string[] date_ary = returnData.Value.Split(',');
                double 消耗量 = 0;
                int NumOfRow = -1;
                for (int i = 0; i < consumptionClasses.Count; i++)
                {
                    if (NumOfRow >= row_max || NumOfRow == -1)
                    {
                        sheetClass = loadText.JsonDeserializet<SheetClass>();
                        sheetClass.Name = $"{i}";
                        sheetClass.ReplaceCell(1, 2, $"{起始時間}");
                        sheetClass.ReplaceCell(2, 2, $"{結束時間}");

                        sheetClasses.Add(sheetClass);
                        NumOfRow = 0;
                    }

                    消耗量 += consumptionClasses[i].消耗量.StringToDouble();
                    消耗量 *= -1;
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 0, $"{i + 1}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 1, $"{consumptionClasses[i].藥碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 2, $"{consumptionClasses[i].藥名}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 3, $"{consumptionClasses[i].消耗量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 4, $"{consumptionClasses[i].結存量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);

                    NumOfRow++;
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
        /// <summary>
        /// 取得庫存及消耗總量Excel(xlxs)(多台合併)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
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
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[SheetClass]陣列結構</returns>
        [Route("download_datas_excel_by_serch")]
        [HttpPost]
        public async Task<ActionResult> Post_download_datas_excel_by_serch([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "download_datas_excel_by_serch";

            try
            {


                returnData = POST_get_datas_sheet_by_serch(returnData).JsonDeserializet<returnData>();
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
                return await Task.FromResult(File(stream, xlsx_command, $"{DateTime.Now.ToDateString("-")}_消耗量表.xlsx"));
            }
            catch
            {
                return null;
            }

        }

        public class ICP_交易記錄查詢 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                DateTime datetime2 = y[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                int compare = DateTime.Compare(datetime2, datetime1);
                return compare;

            }
        }
        public class ICP_transactionsClass : IComparer<transactionsClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(transactionsClass x, transactionsClass y)
            {
                DateTime datetime1 = x.操作時間.StringToDateTime();
                DateTime datetime2 = y.操作時間.StringToDateTime();
                int compare = DateTime.Compare(datetime2, datetime1);
                return compare;

            }
        }
    }
}
