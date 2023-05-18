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
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inspectionController : Controller
    {
        static private string DataBaseName = ConfigurationManager.AppSettings["database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;


        static private string MDC_DataBaseName = ConfigurationManager.AppSettings["MED_cloud_DB"];
        static private string MDC_IP = ConfigurationManager.AppSettings["MED_cloud_IP"];

        private SQLControl sQLControl_inspection_creat = new SQLControl(IP, DataBaseName, "inspection_creat", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_inspection_content = new SQLControl(IP, DataBaseName, "inspection_content", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_inspection_sub_content = new SQLControl(IP, DataBaseName, "inspection_sub_content", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_MED_cloud = new SQLControl(MDC_IP, MDC_DataBaseName, "medicine_page_cloud", UserName, Password, Port, SSLMode);

 
       
        [HttpGet]
        public string Get()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = this.sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            List<object[]> list_sub_inspection = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_sub_inspection_buf = new List<object[]>();


            List<object[]> list_medecine = this.sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
          
            returnData.Code = 200;
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData.JsonSerializationt(true);
        }
        
        [Route("creat")]
        [HttpPost]
        public string POST_creat([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            if (returnData.Data.Count == 0)
            {
                returnData.Code = -1;
                returnData.Result = "輸入Data長度錯誤!";
                return returnData.JsonSerializationt();
            }
            returnData.Result = "";
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = this.sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            List<object[]> list_sub_inspection = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_sub_inspection_buf = new List<object[]>();

            List<object[]> list_medecine = this.sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
            inspectionClass.creat creat = inspectionClass.creat.ObjToClass(returnData.Data[0]);
            if (creat == null)
            {
                returnData.Code = -5;
                returnData.Result += $"Data 資料錯誤 \n";
                return returnData.JsonSerializationt();
            }
            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.請購單號, creat.請購單號);
            if (list_inspection_creat_buf.Count > 0)
            {
                returnData.Code = -6;
                returnData.Result += $"請購單號: {creat.請購單號} 已存在,請刪除後再建立! \n";
                return returnData.JsonSerializationt();
            }
            for (int k = 0; k < creat.Contents.Count; k++)
            {
                
                list_medecine_buf = list_medecine.GetRows((int)enum_驗收內容.藥品碼, creat.Contents[k].藥品碼);
                if (list_medecine_buf.Count > 0)
                {

                }
            }
            return "";
        }
        [Route("creat_delete")]
        [HttpPost]
        public string POST_creat_delete([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = this.sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            List<object[]> list_sub_inspection = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_sub_inspection_buf = new List<object[]>();
            inspectionClass.creat creat = inspectionClass.creat.ObjToClass(returnData.Data[0]);
            if (creat == null)
            {
                returnData.Code = -5;
                returnData.Result += $"Data 資料錯誤 \n";
                return returnData.JsonSerializationt();
            }

            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.請購單號, creat.請購單號);
            list_inspection_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收內容.請購單號, creat.請購單號);
            list_inspection_sub_content_buf = list_inspection_content.GetRows((int)enum_驗收明細.請購單號, creat.請購單號);

            this.sQLControl_inspection_creat.DeleteExtra(null, list_inspection_creat_buf);
            this.sQLControl_inspection_content.DeleteExtra(null, list_inspection_content_buf);
            this.sQLControl_inspection_sub_content.DeleteExtra(null, list_inspection_sub_content_buf);
            returnData.Code = 200;
            returnData.Result = $"已將[{creat.請購單號}刪除!]";
            return returnData.JsonSerializationt();



        }



        //[Route("update")]
        //[HttpPost]
        //public string Post_update([FromBody] returnData data)
        //{
        //    MyTimer myTimer = new MyTimer();
        //    myTimer.StartTickTime(50000);
        //    List<object[]> list_inspection_replace = new List<object[]>();
        //    List<object[]> list_inspection_add = new List<object[]>();

        //    List<object[]> list_sub_inspection_replace = new List<object[]>();
        //    List<object[]> list_sub_inspection_add = new List<object[]>();
        //    List<object[]> list_sub_inspection_delete = new List<object[]>();

        //    List<object[]> list_inspection = this.sQLControl_inspection_content.GetAllRows(null);
        //    List<object[]> list_inspection_buf = new List<object[]>();
        //    List<object[]> list_sub_inspection = this.sQLControl_inspection_sub_content.GetAllRows(null);
        //    List<object[]> list_sub_inspection_buf = new List<object[]>();

        //    for (int i = 0; i < data.Data.Count; i++)
        //    {
        //        //取得母資料
        //        class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
        //        class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(data.Data[i]);
        //        list_inspection_buf = list_inspection.GetRows((int)enum_驗收入庫資料.GUID, class_Output_Inspection_Data.GUID);
        //        if (list_inspection_buf.Count == 0) continue;

        //        object[] value = list_inspection_buf[0];
        //        string Mater_GUID = value[(int)enum_驗收入庫資料.GUID].ObjectToString();
        //        string 藥品碼 = value[(int)enum_驗收入庫資料.藥品碼].ObjectToString();
        //        string 料號 = value[(int)enum_驗收入庫資料.料號].ObjectToString();
        //        string 請購單號 = value[(int)enum_驗收入庫資料.請購單號].ObjectToString();


        //        //刪除相關子資料
        //        list_sub_inspection_buf = list_sub_inspection.GetRows((int)enum_驗收入庫效期批號.Master_GUID, Mater_GUID);
        //        list_sub_inspection_delete.LockAdd(list_sub_inspection_buf);

        //        //重新新增子資料
        //        int 實收數量 = 0;
        //        for (int k = 0; k < class_Output_Inspection_Data.Lot_date_datas.Count; k++)
        //        {
        //            class_Output_Inspection_Data.Lot_date_datas[k].GUID = Guid.NewGuid().ToString();
            
        //            if (class_Output_Inspection_Data.Lot_date_datas[k].更新 == "True")
        //            {
        //                class_Output_Inspection_Data.Lot_date_datas[k].驗收時間 = DateTime.Now.ToDateTimeString();
        //            }
        //            object[] value_sub_inspection = class_output_inspection_data.Get_SQL_DATA(class_Output_Inspection_Data.Lot_date_datas[k]);
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.Master_GUID] = Mater_GUID;
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.藥品碼] = 藥品碼;
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.料號] = 料號;
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.請購單號] = 請購單號;
        //            list_sub_inspection_add.Add(value_sub_inspection);
        //            if (class_Output_Inspection_Data.Lot_date_datas[k].數量.StringIsInt32()) 實收數量 += class_Output_Inspection_Data.Lot_date_datas[k].數量.StringToInt32();
        //        }

        //        class_Output_Inspection_Data.實收數量 = 實收數量.ToString();

        //        if (class_Output_Inspection_Data.實收數量 != value[(int)enum_驗收入庫資料.實收數量].ObjectToString())
        //        {
        //            value[(int)enum_驗收入庫資料.實收數量] = class_Output_Inspection_Data.實收數量;
        //            list_sub_inspection_replace.Add(value);
        //        }
        //        data.Data[i] = class_Output_Inspection_Data;
        //    }
        //    if (list_sub_inspection_replace.Count > 0) this.sQLControl_inspection_content.UpdateByDefulteExtra(null, list_sub_inspection_replace);
        //    if (list_sub_inspection_delete.Count > 0) this.sQLControl_inspection_sub_content.DeleteExtra(null, list_sub_inspection_delete);
        //    if (list_sub_inspection_add.Count > 0) this.sQLControl_inspection_sub_content.AddRows(null, list_sub_inspection_add);
        //    data.Result = $"Inspection data update 成功! {myTimer.ToString()}";
        //    return data.JsonSerializationt();
        //}
       
        //[Route("download_excel")]
        //[HttpPost]
        //public async Task<ActionResult> Post_download_excel([FromBody] returnData data)
        //{
        //    string jsonstr = Post_OD_Date(data);
        //    string 請購單號 = "";
        //    string 驗收時間 = "";
        //    int NumOfRow = 0;
        //    for (int i = 0; i < data.Data.Count; i++)
        //    {
        //        class_output_inspection_date class_Output_Inspection_Date = new class_output_inspection_date();
        //        class_Output_Inspection_Date = class_Output_Inspection_Date.ObjToData(data.Data[i]);

        //        驗收時間 += class_Output_Inspection_Date.驗收時間;
        //        請購單號 += class_Output_Inspection_Date.請購單號;
        //        if (i != data.Data.Count - 1)
        //        {
        //            驗收時間 += ",";
        //            請購單號 += ",";
        //        }
        //    }


        //    returnData returnData = jsonstr.JsonDeserializet<returnData>();
        //    returnData.Code = 200;
        //    List<SheetClass> sheetClasses = new List<SheetClass>();
        //    string loadText = Basic.MyFileStream.LoadFileAllText(@"C:\excel.txt", "utf-8");
        //    SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();
        //    sheetClass.ReplaceCell(1, 2, $"{驗收時間}");
        //    sheetClass.ReplaceCell(1, 6, $"{請購單號}");
        //    NumOfRow = 0;
        //    for (int i = 0; i < returnData.Data.Count; i++)
        //    {
        //        class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
        //        class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(returnData.Data[i]);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 0, $"{class_Output_Inspection_Data.請購單號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 1, $"{class_Output_Inspection_Data.藥品碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 2, $"{class_Output_Inspection_Data.料號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, NumOfRow + 3, 3, 4, $"{class_Output_Inspection_Data.藥品名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        //sheetClass.AddNewCell_Webapi(NumOfRow + 3, 5, $"{class_Output_Inspection_Data.效期}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        //sheetClass.AddNewCell_Webapi(NumOfRow + 3, 6, $"{class_Output_Inspection_Data.批號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 7, $"{class_Output_Inspection_Data.應收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 8, $"{class_Output_Inspection_Data.實收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);

        //        NumOfRow++;
        //    }


        //    byte[] excelData = sheetClass.NPOI_GetBytes();
        //    Stream stream = new MemoryStream(excelData);
        //    return await Task.FromResult(File(stream, "application/vnd.ms-excel", $"{DateTime.Now.ToDateString("-")}_驗收入庫清單.xls"));
        //}

    }
}
