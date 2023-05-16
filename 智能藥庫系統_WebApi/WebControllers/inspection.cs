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
namespace 智慧藥庫系統_WebApi
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


        static private string MDC_DataBaseName = ConfigurationManager.AppSettings["medicine_page_cloud_database"];
        static private string MDC_IP = ConfigurationManager.AppSettings["medicine_page_cloud_IP"];


        private SQLControl sQLControl_inspection = new SQLControl(IP, DataBaseName, "inspection", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_sub_inspection = new SQLControl(IP, DataBaseName, "sub_inspection", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_medicine_page_cloud = new SQLControl(MDC_IP, MDC_DataBaseName, "medicine_page_cloud", UserName, Password, Port, SSLMode);


        public class returnData
        {
            private List<object> _data = new List<object>();
            private int _code = 0;
            private string _result = "";

            public List<object> Data { get => _data; set => _data = value; }
            public int Code { get => _code; set => _code = value; }
            public string Result { get => _result; set => _result = value; }
        }
        public class class_output_inspection_date
        {
            [JsonPropertyName("OD_SN_S")]
            public string 請購單號 { get; set; }
            [JsonPropertyName("MIS_CREATEDTTM")]
            public string 驗收時間 { get; set; }

            public class_output_inspection_date ObjToData(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<class_output_inspection_date>();
            }
        }
        public class class_output_inspection_data
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("OD_SN_L")]
            public string 請購單號 { get; set; }
            [JsonPropertyName("CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("SKDIACODE")]
            public string 料號 { get; set; }
            [JsonPropertyName("NAME")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("CHT_NAME")]
            public string 中文名稱 { get; set; }
            [JsonPropertyName("START_QTY")]
            public string 應收數量 { get; set; }
            [JsonPropertyName("END_QTY")]
            public string 實收數量 { get; set; }
    
            [JsonPropertyName("MIS_CREATEDTTM")]
            public string 驗收時間 { get; set; }
            [JsonPropertyName("OD_CREATEDTTM")]
            public string 請購時間 { get; set; }

            private List<class_sub_inspection_data> _lot_date_datas = new List<class_sub_inspection_data>();
            public List<class_sub_inspection_data> Lot_date_datas { get => _lot_date_datas; set => _lot_date_datas = value; }

            static public object[] Get_SQL_DATA(class_sub_inspection_data class_Sub_Inspection_Data)
            {
                object[] value = new object[new enum_驗收入庫效期批號().GetLength()];
                value[(int)enum_驗收入庫效期批號.GUID] = class_Sub_Inspection_Data.GUID;
                value[(int)enum_驗收入庫效期批號.Master_GUID] = class_Sub_Inspection_Data.Master_GUID;
                value[(int)enum_驗收入庫效期批號.效期] = class_Sub_Inspection_Data.效期;
                value[(int)enum_驗收入庫效期批號.批號] = class_Sub_Inspection_Data.批號;
                value[(int)enum_驗收入庫效期批號.數量] = class_Sub_Inspection_Data.數量;
                value[(int)enum_驗收入庫效期批號.驗收時間] = class_Sub_Inspection_Data.驗收時間;
                value[(int)enum_驗收入庫效期批號.操作人] = class_Sub_Inspection_Data.操作人;
                value[(int)enum_驗收入庫效期批號.已鎖定] = class_Sub_Inspection_Data.已鎖定;

                return value;
            }

            public class_output_inspection_data ObjToData(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<class_output_inspection_data>();
            }

           
        }
        public class class_sub_inspection_data
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("Master_GUID")]
            public string Master_GUID { get; set; }
            [JsonPropertyName("VAL_DATE")]
            public string 效期 { get; set; }
            [JsonPropertyName("LOT_NUMBER")]
            public string 批號 { get; set; }
            [JsonPropertyName("QTY")]
            public string 數量 { get; set; }
            [JsonPropertyName("MIS_CREATEDTTM")]
            public string 驗收時間 { get; set; }
            [JsonPropertyName("OPERATOR")]
            public string 操作人 { get; set; }
            [JsonPropertyName("LOCK")]
            public string 已鎖定 { get; set; }
            [JsonPropertyName("Update")]
            public string 更新 { get; set; }
        }
        public enum enum_驗收入庫資料
        {
            GUID,
            請購單號,
            藥品碼,
            料號,
            藥品名稱,
            包裝單位,
            應收數量,
            實收數量,
            驗收時間,
            入庫時間,
            請購時間,
            狀態,
            來源,
            備註,
        }
        public enum enum_驗收入庫效期批號
        {
            GUID,
            Master_GUID,
            請購單號,
            藥品碼,
            料號,
            效期,
            批號,
            數量,
            驗收時間,
            操作人,
            已鎖定,
        }
        [HttpGet]
        public string Get()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inspection = this.sQLControl_inspection.GetAllRows(null);
            List<object[]> list_sub_inspection = this.sQLControl_sub_inspection.GetAllRows(null);
            List<object[]> list_sub_inspection_buf = new List<object[]>();


            List<object[]> list_medecine = this.sQLControl_medicine_page_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
            for (int i = 0; i < list_inspection.Count; i++)
            {
                class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
                string GUID = list_inspection[i][(int)enum_驗收入庫資料.GUID].ObjectToString();
                string 藥品碼 = list_inspection[i][(int)enum_驗收入庫資料.藥品碼].ObjectToString();
                string 請購單號 = list_inspection[i][(int)enum_驗收入庫資料.請購單號].ObjectToString();
                list_medecine_buf = list_medecine.GetRows((int)enum_medicine_page_cloud.藥品碼, 藥品碼);
                if (list_medecine_buf.Count == 0) continue;
                string 藥品名稱 = list_medecine_buf[0][(int)enum_medicine_page_cloud.藥品名稱].ObjectToString();
                string 料號 = list_medecine_buf[0][(int)enum_medicine_page_cloud.料號].ObjectToString();
                string 中文名稱 = list_medecine_buf[0][(int)enum_medicine_page_cloud.中文名稱].ObjectToString();
                string 應收數量 = list_inspection[i][(int)enum_驗收入庫資料.應收數量].ObjectToString();
                int 實收數量 = 0;
  
                string 驗收時間 = list_inspection[i][(int)enum_驗收入庫資料.驗收時間].ToDateTimeString();
                string 請購時間 = list_inspection[i][(int)enum_驗收入庫資料.請購時間].ToDateTimeString();

                class_Output_Inspection_Data.GUID = GUID;
                class_Output_Inspection_Data.請購單號 = 請購單號;

                class_Output_Inspection_Data.藥品碼 = 藥品碼;
                class_Output_Inspection_Data.料號 = 料號;
                class_Output_Inspection_Data.藥品名稱 = 藥品名稱;
                class_Output_Inspection_Data.中文名稱 = 中文名稱;
                class_Output_Inspection_Data.應收數量 = 應收數量;

                class_Output_Inspection_Data.驗收時間 = 驗收時間;
                class_Output_Inspection_Data.請購時間 = 請購時間;
                list_sub_inspection_buf = list_sub_inspection.GetRows((int)enum_驗收入庫效期批號.Master_GUID, GUID);
                for (int k = 0; k < list_sub_inspection_buf.Count; k++)
                {
                    class_sub_inspection_data class_Sub_Inspection_Data = new class_sub_inspection_data();
                    class_Sub_Inspection_Data.GUID = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.GUID].ObjectToString();
                    class_Sub_Inspection_Data.Master_GUID = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.Master_GUID].ObjectToString();
                    class_Sub_Inspection_Data.效期 = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.效期].ToDateString("-");
                    class_Sub_Inspection_Data.批號 = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.批號].ObjectToString();
                    class_Sub_Inspection_Data.數量 = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.數量].ObjectToString();
                    class_Sub_Inspection_Data.數量 = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.數量].ObjectToString();
                    class_Sub_Inspection_Data.操作人 = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.操作人].ObjectToString();

                    if (class_Sub_Inspection_Data.數量.StringIsInt32()) 實收數量 += class_Sub_Inspection_Data.數量.StringToInt32();
                    class_Sub_Inspection_Data.驗收時間 = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.驗收時間].ToDateTimeString();
                    class_Sub_Inspection_Data.已鎖定 = list_sub_inspection_buf[k][(int)enum_驗收入庫效期批號.已鎖定].ObjectToString();
                    if (class_Sub_Inspection_Data.已鎖定 != "True") class_Sub_Inspection_Data.已鎖定 = "False";

                    class_Output_Inspection_Data.Lot_date_datas.Add(class_Sub_Inspection_Data);
                }

                class_Output_Inspection_Data.實收數量 = 實收數量.ToString();


                returnData.Data.Add(class_Output_Inspection_Data);
            }
            returnData.Code = 200;
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData.JsonSerializationt(true);
        }
        [Route("update")]
        [HttpPost]
        public string Post([FromBody] returnData data)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_inspection_replace = new List<object[]>();
            List<object[]> list_inspection_add = new List<object[]>();

            List<object[]> list_sub_inspection_replace = new List<object[]>();
            List<object[]> list_sub_inspection_add = new List<object[]>();
            List<object[]> list_sub_inspection_delete = new List<object[]>();

            List<object[]> list_inspection = this.sQLControl_inspection.GetAllRows(null);
            List<object[]> list_inspection_buf = new List<object[]>();
            List<object[]> list_sub_inspection = this.sQLControl_sub_inspection.GetAllRows(null);
            List<object[]> list_sub_inspection_buf = new List<object[]>();

            for (int i = 0; i < data.Data.Count; i++)
            {
                //取得母資料
                class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
                class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(data.Data[i]);
                list_inspection_buf = list_inspection.GetRows((int)enum_驗收入庫資料.GUID, class_Output_Inspection_Data.GUID);
                if (list_inspection_buf.Count == 0) continue;

                object[] value = list_inspection_buf[0];
                string Mater_GUID = value[(int)enum_驗收入庫資料.GUID].ObjectToString();
                string 藥品碼 = value[(int)enum_驗收入庫資料.藥品碼].ObjectToString();
                string 料號 = value[(int)enum_驗收入庫資料.料號].ObjectToString();
                string 請購單號 = value[(int)enum_驗收入庫資料.請購單號].ObjectToString();


                //刪除相關子資料
                list_sub_inspection_buf = list_sub_inspection.GetRows((int)enum_驗收入庫效期批號.Master_GUID, Mater_GUID);
                list_sub_inspection_delete.LockAdd(list_sub_inspection_buf);

                //重新新增子資料
                int 實收數量 = 0;
                for (int k = 0; k < class_Output_Inspection_Data.Lot_date_datas.Count; k++)
                {
                    class_Output_Inspection_Data.Lot_date_datas[k].GUID = Guid.NewGuid().ToString();
            
                    if (class_Output_Inspection_Data.Lot_date_datas[k].更新 == "True")
                    {
                        class_Output_Inspection_Data.Lot_date_datas[k].驗收時間 = DateTime.Now.ToDateTimeString();
                    }
                    object[] value_sub_inspection = class_output_inspection_data.Get_SQL_DATA(class_Output_Inspection_Data.Lot_date_datas[k]);
                    value_sub_inspection[(int)enum_驗收入庫效期批號.Master_GUID] = Mater_GUID;
                    value_sub_inspection[(int)enum_驗收入庫效期批號.藥品碼] = 藥品碼;
                    value_sub_inspection[(int)enum_驗收入庫效期批號.料號] = 料號;
                    value_sub_inspection[(int)enum_驗收入庫效期批號.請購單號] = 請購單號;
                    list_sub_inspection_add.Add(value_sub_inspection);
                    if (class_Output_Inspection_Data.Lot_date_datas[k].數量.StringIsInt32()) 實收數量 += class_Output_Inspection_Data.Lot_date_datas[k].數量.StringToInt32();
                }

                class_Output_Inspection_Data.實收數量 = 實收數量.ToString();

                if (class_Output_Inspection_Data.實收數量 != value[(int)enum_驗收入庫資料.實收數量].ObjectToString())
                {
                    value[(int)enum_驗收入庫資料.實收數量] = class_Output_Inspection_Data.實收數量;
                    list_sub_inspection_replace.Add(value);
                }
                data.Data[i] = class_Output_Inspection_Data;
            }
            if (list_sub_inspection_replace.Count > 0) this.sQLControl_inspection.UpdateByDefulteExtra(null, list_sub_inspection_replace);
            if (list_sub_inspection_delete.Count > 0) this.sQLControl_sub_inspection.DeleteExtra(null, list_sub_inspection_delete);
            if (list_sub_inspection_add.Count > 0) this.sQLControl_sub_inspection.AddRows(null, list_sub_inspection_add);
            data.Result = $"Inspection data update 成功! {myTimer.ToString()}";
            return data.JsonSerializationt();
        }
        [Route("get_od_Date")]
        [HttpGet]
        public string Get_OD_Date()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inspection = this.sQLControl_inspection.GetAllRows(null);
  
            list_inspection = list_inspection.Distinct(new Distinct_inspection_date()).ToList();
            returnData.Code = 200;
            returnData.Result = $"取得請購日期表成功! {myTimer.ToString()}";
            for(int i = 0; i < list_inspection.Count; i++)
            {
                class_output_inspection_date class_Output_Inspection_Date = new class_output_inspection_date();
                string GUID = list_inspection[i][(int)enum_驗收入庫資料.GUID].ObjectToString();
                class_Output_Inspection_Date.請購單號 = Function_解析請購單號(list_inspection[i][(int)enum_驗收入庫資料.請購單號].ObjectToString());
                class_Output_Inspection_Date.驗收時間 = list_inspection[i][(int)enum_驗收入庫資料.驗收時間].ToDateString("-");


                returnData.Data.Add(class_Output_Inspection_Date);
            }
            return returnData.JsonSerializationt(true);
        }
        [Route("get_od_Date")]
        [HttpPost]
        public string Post_OD_Date([FromBody] returnData data)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();

            string jsonstr = this.Get();
            returnData returnData_Output_Inspection = jsonstr.JsonDeserializet<returnData>();
            List<class_output_inspection_data> list_class_output_inspection_data = new List<class_output_inspection_data>();
            List<class_output_inspection_data> list_class_output_inspection_data_buf = new List<class_output_inspection_data>();
           

            for (int i = 0; i < returnData_Output_Inspection.Data.Count; i++)
            {
                class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
                class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(returnData_Output_Inspection.Data[i]);
                list_class_output_inspection_data.Add(class_Output_Inspection_Data);
            }


            for (int i = 0; i < data.Data.Count; i++)
            {
                class_output_inspection_date class_Output_Inspection_Date = new class_output_inspection_date();
                class_Output_Inspection_Date = class_Output_Inspection_Date.ObjToData(data.Data[i]);


                List<class_output_inspection_data> list_class_output_inspection_data_temp = (from value in list_class_output_inspection_data
                                                                                             where value.驗收時間.StringToDateTime().ToDateString("-") == class_Output_Inspection_Date.驗收時間
                                                                                             where value.請購單號.ToUpper().Contains(class_Output_Inspection_Date.請購單號.ToUpper())
                                                                                             select value).ToList();
                list_class_output_inspection_data_buf.LockAdd(list_class_output_inspection_data_temp);
            }
            for (int i = 0; i < list_class_output_inspection_data_buf.Count; i++)
            {
                returnData.Data.Add(list_class_output_inspection_data_buf[i]);
            }
            returnData.Result = $"取得指定請購細節表成功! {myTimer.ToString()}";
            return returnData.JsonSerializationt(true);
        }
        [Route("download_excel")]
        [HttpPost]
        public async Task<ActionResult> Post_download_excel([FromBody] returnData data)
        {
            string jsonstr = Post_OD_Date(data);
            string 請購單號 = "";
            string 驗收時間 = "";
            int NumOfRow = 0;
            for (int i = 0; i < data.Data.Count; i++)
            {
                class_output_inspection_date class_Output_Inspection_Date = new class_output_inspection_date();
                class_Output_Inspection_Date = class_Output_Inspection_Date.ObjToData(data.Data[i]);

                驗收時間 += class_Output_Inspection_Date.驗收時間;
                請購單號 += class_Output_Inspection_Date.請購單號;
                if (i != data.Data.Count - 1)
                {
                    驗收時間 += ",";
                    請購單號 += ",";
                }
            }


            returnData returnData = jsonstr.JsonDeserializet<returnData>();
            returnData.Code = 200;
            List<SheetClass> sheetClasses = new List<SheetClass>();
            string loadText = Basic.MyFileStream.LoadFileAllText(@"C:\excel.txt", "utf-8");
            SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();
            sheetClass.ReplaceCell(1, 2, $"{驗收時間}");
            sheetClass.ReplaceCell(1, 6, $"{請購單號}");
            NumOfRow = 0;
            for (int i = 0; i < returnData.Data.Count; i++)
            {
                class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
                class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(returnData.Data[i]);
                sheetClass.AddNewCell_Webapi(NumOfRow + 3, 0, $"{class_Output_Inspection_Data.請購單號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(NumOfRow + 3, 1, $"{class_Output_Inspection_Data.藥品碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(NumOfRow + 3, 2, $"{class_Output_Inspection_Data.料號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(NumOfRow + 3, NumOfRow + 3, 3, 4, $"{class_Output_Inspection_Data.藥品名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                //sheetClass.AddNewCell_Webapi(NumOfRow + 3, 5, $"{class_Output_Inspection_Data.效期}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                //sheetClass.AddNewCell_Webapi(NumOfRow + 3, 6, $"{class_Output_Inspection_Data.批號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(NumOfRow + 3, 7, $"{class_Output_Inspection_Data.應收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
                sheetClass.AddNewCell_Webapi(NumOfRow + 3, 8, $"{class_Output_Inspection_Data.實收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);

                NumOfRow++;
            }


            byte[] excelData = sheetClass.NPOI_GetBytes();
            Stream stream = new MemoryStream(excelData);
            return await Task.FromResult(File(stream, "application/vnd.ms-excel", $"{DateTime.Now.ToDateString("-")}_驗收入庫清單.xls"));
        }

        static public string Function_解析請購單號(string ODSN)
        {
            string[] ODSN_Ary = ODSN.Split('-');
            if (ODSN_Ary.Length != 2) return ODSN;
            return ODSN_Ary[0];
        }
        public class Distinct_inspection_date : IEqualityComparer<object[]>
        {
            public bool Equals(object[] x, object[] y)
            {
                bool flag_驗收時間 = (x[(int)enum_驗收入庫資料.驗收時間].ToDateString("-") == y[(int)enum_驗收入庫資料.驗收時間].ToDateString("-"));
                string 請購單號_x = x[(int)enum_驗收入庫資料.請購單號].ObjectToString();
                請購單號_x = Function_解析請購單號(請購單號_x);
                string 請購單號_y = y[(int)enum_驗收入庫資料.請購單號].ObjectToString();
                請購單號_y = Function_解析請購單號(請購單號_y);
                bool flag_請購單號 = (請購單號_x == 請購單號_y);
                return (flag_請購單號 && flag_驗收時間);
            }

            public int GetHashCode(object[] obj)
            {
                return 1;
            }
        }
    }
}
