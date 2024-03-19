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
    public class medShiftConfigController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [Route("init")]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
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

        [Route("get_all")]
        [HttpPost]
        public string POST_get_all([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                CheckCreatTable(serverSettingClasses[0]);
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl = new SQLControl(Server, DB, "med_shift_config", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<medShiftConfigClass> medShiftConfigClasses = list_value.SQLToClass<medShiftConfigClass, enum_交班藥品設定>();
                returnData.Code = 200;
                returnData.Result = $"取得藥品交班資訊成功!";
                returnData.Data = medShiftConfigClasses;
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
        }
        [Route("update_by_code")]
        [HttpPost]
        public string POST_update_by_code([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                CheckCreatTable(serverSettingClasses[0]);
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl = new SQLControl(Server, DB, "med_shift_config", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                List<medShiftConfigClass> medShiftConfigClasses = returnData.Data.ObjToListClass<medShiftConfigClass>();
                for (int i = 0; i < medShiftConfigClasses.Count; i++)
                {
                    string 藥品碼 = medShiftConfigClasses[i].藥品碼;
                    list_value_buf = list_value.GetRows((int)enum_交班藥品設定.藥品碼, 藥品碼);
                    if(list_value_buf.Count > 0)
                    {
                        medShiftConfigClass medShiftConfigClass = list_value_buf[0].SQLToClass<medShiftConfigClass, enum_交班藥品設定>();
                        medShiftConfigClass.是否交班 = medShiftConfigClasses[i].是否交班;
                        list_value_replace.Add(medShiftConfigClass.ClassToSQL<medShiftConfigClass, enum_交班藥品設定>());
                    }
                    else
                    {
                        medShiftConfigClass medShiftConfigClass = medShiftConfigClasses[i];
                        medShiftConfigClass.GUID = Guid.NewGuid().ToString();
                        list_value_add.Add(medShiftConfigClass.ClassToSQL<medShiftConfigClass, enum_交班藥品設定>());
                    }
                }
                if (list_value_add.Count > 0) sQLControl.AddRows(null, list_value_add);
                if (list_value_replace.Count > 0) sQLControl.UpdateByDefulteExtra(null, list_value_replace);
                returnData.Code = 200;
                returnData.Result = $"修正藥品交班資訊成功! 新增<{list_value_add.Count}>筆 , 修改<{list_value_replace.Count}>筆";
                returnData.Data = "";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();

            }
        }
        [Route("GET_SheetClass")]
        [HttpPost]
        public string GET_SheetClass([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<medShiftListClass> medShiftListClasses = returnData.Data.ObjToListClass<medShiftListClass>();
            int page_num = 0;
            int current_row = 0;      
            int max_row = 40;
            string loadText = Basic.MyFileStream.LoadFileAllText(@"./excel_MedShift.txt", "utf-8");
            List<SheetClass> sheetClasses = new List<SheetClass>();
            SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();
            sheetClass.ReplaceCell(0, 0, returnData.Value);
            while (true)
            {
                if(current_row > max_row)
                {
                    sheetClasses.Add(sheetClass);
                    sheetClass = loadText.JsonDeserializet<SheetClass>();
                    sheetClass.ReplaceCell(0, 0, returnData.Value);
                    current_row = 0;
                    page_num++;
                }
                int index = page_num * max_row + current_row;
                if (index >= medShiftListClasses.Count)
                {
                    sheetClasses.Add(sheetClass);
                    break;
                }
                medShiftListClass medShiftListClass = medShiftListClasses[index];
                sheetClass.AddNewCell_Webapi(current_row + 3, 0, $"{index + 1}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(current_row + 3, 1, $"{medShiftListClass.藥碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(current_row + 3, 2, $"{medShiftListClass.藥名}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(current_row + 3, 3, $"{medShiftListClass.現有庫存}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(current_row + 3, 4, $"{medShiftListClass.處方支出}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(current_row + 3, 5, $"{medShiftListClass.處方數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(current_row + 3, 6, $"{medShiftListClass.實際庫存}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);

                current_row++;

            }








            returnData.Data = sheetClasses;
            returnData.Code = 200;
            returnData.Result = "取得交班表SheetClass成功!";
            return returnData.JsonSerializationt();
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "med_shift_config", UserName, Password, Port, SSLMode);


            Table table = new Table("med_shift_config");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("藥碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table.AddColumnList("是否交班", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            
            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
            else sQLControl.CheckAllColumnName(table, true);
            return table.JsonSerializationt(true);
        }
    }
}
