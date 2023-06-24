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
    public class transactionsController : Controller
    {
       

        static private string API_Server = ConfigurationManager.AppSettings["API_Server"];
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("serch_med_information_by_code")]
        [HttpPost]
        public string POST_serch_med_information_by_code([FromBody] returnData returnData)
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

                if (returnData.Value .StringIsEmpty())
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
                List<medClass> medClasses = medClass.ObjToListClass(returnData_med.Data);
                List<medClass> medClasses_buf = new List<medClass>();
                medClasses_buf = (from value in medClasses
                                  where value.藥品碼.ToUpper() == 藥品碼.ToUpper()
                                  select value).ToList();
                if(medClasses_buf.Count == 0)
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
                if(input_value.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "輸入資訊錯誤!";
                    returnData.Data = "";
                    return returnData.JsonSerializationt();
                }
                if(input_value.Length == 1)
                {
                    string 藥品碼 = input_value[0];
                    List<object[]> list_value = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥品碼, 藥品碼);
                    list_value.Sort(new transactionsClass.ICP_By_OP_Time());
                    List<transactionsClass> transactionsClasses = transactionsClass.SQLToClass(list_value);
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
                    List<transactionsClass> transactionsClasses = transactionsClass.SQLToClass(list_value);
                    returnData.Code = 200;
                    returnData.Result = $"交易紀錄搜尋成功共<{transactionsClasses.Count}>筆";
                    returnData.Data = transactionsClasses;
                    return returnData.JsonSerializationt(true);
                }
              
            }
            catch(Exception e)
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
                List<medClass> medClasses = medClass.ObjToListClass(returnData_med.Data);
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
                List<transactionsClass> transactionsClasses = transactionsClass.ObjToListClass(returnData.Data);



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
                        sheetClass.ReplaceCell(2, 1, $"{medClasses_buf[0].藥品名稱}");
                        sheetClass.ReplaceCell(1, 3, $"{medClasses_buf[0].包裝單位}");
                        sheetClass.ReplaceCell(1, 7, $"{起始時間}");
                        sheetClass.ReplaceCell(2, 7, $"{結束時間}");
                        sheetClasses.Add(sheetClass);
                        NumOfRow = 0;
                    }

                    消耗量 += transactionsClasses[i].交易量.StringToInt32();
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 0, $"{i + 1}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 1, $"{transactionsClasses[i].操作時間}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 2, $"{transactionsClasses[i].床號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 3, $"{transactionsClasses[i].類別}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 4, $"{transactionsClasses[i].病人姓名}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 5, $"{transactionsClasses[i].病歷號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 6, $"{transactionsClasses[i].操作人}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 7, $"{transactionsClasses[i].交易量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 8, $"{transactionsClasses[i].結存量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 9, $"{transactionsClasses[i].盤點量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 10, $"{transactionsClasses[i].收支原因}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    sheetClass.AddNewCell_Webapi(NumOfRow + 4, 11, $"{transactionsClasses[i].備註}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                    NumOfRow++;
                }
                for(int i = 0; i < sheetClasses.Count; i++)
                {
                    sheetClasses[i].ReplaceCell(1, 5, $"{消耗量}");
                }
 

                Console.WriteLine($"寫入Sheet {myTimer.ToString()}");
                returnData.Code = 200;
                returnData.Result ="Sheet取得成功!";
                returnData.Data = sheetClasses;
                return returnData.JsonSerializationt();
            }
            catch(Exception e)
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
                if(returnData.Code != 200)
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

    }
}
