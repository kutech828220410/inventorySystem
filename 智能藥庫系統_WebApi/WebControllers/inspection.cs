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
        private SQLControl sQLControl_medicine_page_cloud = new SQLControl(MDC_IP, MDC_DataBaseName, "medicine_page_cloud", UserName, Password, Port, SSLMode);


        public class returnData
        {
            private List<class_output_inspection_data> _data = new List<class_output_inspection_data>();
            private int _code = 0;
            private string _result = "";

            public List<class_output_inspection_data> Data { get => _data; set => _data = value; }
            public int Code { get => _code; set => _code = value; }
            public string Result { get => _result; set => _result = value; }
        }
        public class class_output_inspection_data
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
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
            [JsonPropertyName("VAL_DATE")]
            public string 效期 { get; set; }
            [JsonPropertyName("LOT_NUMBER")]
            public string 批號 { get; set; }
            [JsonPropertyName("MIS_CREATEDTTM")]
            public string 驗收時間 { get; set; }
        }
        public enum enum_藥庫_驗收入庫_過帳明細
        {
            GUID,
            藥品碼,
            料號,
            藥品名稱,
            包裝單位,
            應收數量,
            實收數量,
            效期,
            批號,
            驗收時間,
            入庫時間,
            狀態,
            來源,
            備註,
        }
        [HttpGet]
        public string Get(string? name)
        {
            returnData returnData = new returnData();
            List<object[]> list_inspection = this.sQLControl_inspection.GetAllRows(null);
      
            List<object[]> list_medecine = this.sQLControl_medicine_page_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
            for (int i = 0; i < list_inspection.Count; i++)
            {
                class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
                string GUID = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.GUID].ObjectToString();
                string 藥品碼 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.藥品碼].ObjectToString();
                list_medecine_buf = list_medecine.GetRows((int)enum_medicine_page_cloud.藥品碼, 藥品碼);
                if (list_medecine_buf.Count == 0) continue;
                string 藥品名稱 = list_medecine_buf[0][(int)enum_medicine_page_cloud.藥品名稱].ObjectToString();
                string 料號 = list_medecine_buf[0][(int)enum_medicine_page_cloud.料號].ObjectToString();
                string 中文名稱 = list_medecine_buf[0][(int)enum_medicine_page_cloud.中文名稱].ObjectToString();
                string 應收數量 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.應收數量].ObjectToString();
                string 實收數量 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.實收數量].ObjectToString();
                string 效期 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.效期].ToDateString();
                string 批號 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.批號].ObjectToString();
                string 驗收時間 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.驗收時間].ToDateTimeString();

                class_Output_Inspection_Data.GUID = GUID;
                class_Output_Inspection_Data.藥品碼 = 藥品碼;
                class_Output_Inspection_Data.料號 = 料號;
                class_Output_Inspection_Data.藥品名稱 = 藥品名稱;
                class_Output_Inspection_Data.中文名稱 = 中文名稱;
                class_Output_Inspection_Data.應收數量 = 應收數量;
                class_Output_Inspection_Data.實收數量 = 實收數量;
                class_Output_Inspection_Data.效期 = 效期;
                class_Output_Inspection_Data.批號 = 批號;
                class_Output_Inspection_Data.驗收時間 = 驗收時間;
                returnData.Data.Add(class_Output_Inspection_Data);
            }
            returnData.Code = 200;
            returnData.Result = "成功!";
            return returnData.JsonSerializationt(true);
        }
        [Route("update")]
        [HttpPost]
        public string Post([FromBody] returnData data)
        {
            List<object[]> list_value_replace = new List<object[]>();
            List<object[]> list_inspection = this.sQLControl_inspection.GetAllRows(null);
            List<object[]> list_inspection_buf = new List<object[]>();
            for (int i = 0; i < data.Data.Count; i++)
            {
                class_output_inspection_data class_Output_Inspection_Data = data.Data[i] as class_output_inspection_data;
                list_inspection_buf = list_inspection.GetRows((int)enum_藥庫_驗收入庫_過帳明細.GUID, class_Output_Inspection_Data.GUID);
                if (list_inspection_buf.Count == 0) continue;

                object[] value = list_inspection_buf[0];

                value[(int)enum_藥庫_驗收入庫_過帳明細.應收數量] = class_Output_Inspection_Data.應收數量;
                value[(int)enum_藥庫_驗收入庫_過帳明細.實收數量] = class_Output_Inspection_Data.實收數量;

                list_value_replace.Add(value);
            }
            this.sQLControl_inspection.UpdateByDefulteExtra(null, list_value_replace);
            data.Result = "Inspection data update 成功!";
            return data.JsonSerializationt();
        }

    }
}
