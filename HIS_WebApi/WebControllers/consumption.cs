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
        static private string API_Server = ConfigurationManager.AppSettings["API_Server"];
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("serch_by_ST_END")]
        [HttpPost]
        public string POST_serch_consumption_by_ST_END([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
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

                List<object[]> list_tradding = sQLControl_trading.GetRowsByBetween(null, (int)enum_交易記錄查詢資料.操作時間, date_ary[0], date_ary[1]);
                List<object[]> list_tradding_buf = new List<object[]>();
                List<object> Code_LINQ = (from value in list_tradding
                                          select value[(int)enum_交易記錄查詢資料.藥品碼]).Distinct().ToList();

                List<object[]> list_consumption = new List<object[]>();
                for (int i = 0; i < Code_LINQ.Count; i++)
                {
                    object[] value = new object[new enum_consumption().GetLength()];
                    string Code = Code_LINQ[i].ObjectToString();
                    if (Code.StringIsEmpty()) continue;
                    list_tradding_buf = list_tradding.GetRows((int)enum_交易記錄查詢資料.藥品碼, Code);
                    list_tradding_buf.Sort(new ICP_交易記錄查詢());
                    int 交易量 = 0;
                    if (list_tradding_buf.Count > 0)
                    {
                        value[(int)enum_consumption.藥品碼] = list_tradding_buf[0][(int)enum_交易記錄查詢資料.藥品碼].ObjectToString();
                        value[(int)enum_consumption.藥品名稱] = list_tradding_buf[0][(int)enum_交易記錄查詢資料.藥品名稱].ObjectToString();
                        for (int k = 0; k < list_tradding_buf.Count; k++)
                        {
                            交易量 += list_tradding_buf[k][(int)enum_交易記錄查詢資料.交易量].StringToInt32();
                        }
                        value[(int)enum_consumption.交易量] = 交易量;
                        value[(int)enum_consumption.結存量] = list_tradding_buf[0][(int)enum_交易記錄查詢資料.結存量].ObjectToString(); ;
                        list_consumption.Add(value);
                    }
                }

                List<consumptionClass> consumptionClasses = list_consumption.SQLToClass<consumptionClass , enum_consumption>();
                returnData.Code = 200;
                returnData.Result = "取得交易量成功!";
                returnData.Data = consumptionClasses;

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);

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
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
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

      

                string json = POST_serch_consumption_by_ST_END(returnData);
                returnData = json.JsonDeserializet<returnData>();

                if (returnData.Code != 200)
                {
                    return null;
                }
                List<consumptionClass> consumptionClasses = returnData.Data.ObjToListClass<consumptionClass>();



                string loadText = Basic.MyFileStream.LoadFileAllText(@"./excle_consumption.txt", "utf-8");
                Console.WriteLine($"取得creats {myTimer.ToString()}");
                int row_max = 5000;
                List<SheetClass> sheetClasses = new List<SheetClass>();
                SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();

                string[] date_ary = returnData.Value.Split(',');
                int 消耗量 = 0;
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

                    消耗量 += consumptionClasses[i].交易量.StringToInt32();
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 0, $"{i + 1}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 1, $"{consumptionClasses[i].藥品碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 2, $"{consumptionClasses[i].藥品名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 3, $"{consumptionClasses[i].交易量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
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
    }
}
