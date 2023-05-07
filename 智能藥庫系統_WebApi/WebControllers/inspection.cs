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
            [JsonPropertyName("VAL_DATE")]
            public string 效期 { get; set; }
            [JsonPropertyName("LOT_NUMBER")]
            public string 批號 { get; set; }
            [JsonPropertyName("MIS_CREATEDTTM")]
            public string 驗收時間 { get; set; }
            [JsonPropertyName("OD_CREATEDTTM")]
            public string 請購時間 { get; set; }

            public class_output_inspection_data ObjToData(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<class_output_inspection_data>();
            }
        }
        public enum enum_藥庫_驗收入庫_過帳明細
        {
            GUID,
            請購單號,
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
            請購時間,
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
                string 請購單號 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.請購單號].ObjectToString();
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
                string 請購時間 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.請購時間].ToDateTimeString();

                class_Output_Inspection_Data.GUID = GUID;
                class_Output_Inspection_Data.請購單號 = 請購單號;

                class_Output_Inspection_Data.藥品碼 = 藥品碼;
                class_Output_Inspection_Data.料號 = 料號;
                class_Output_Inspection_Data.藥品名稱 = 藥品名稱;
                class_Output_Inspection_Data.中文名稱 = 中文名稱;
                class_Output_Inspection_Data.應收數量 = 應收數量;
                class_Output_Inspection_Data.實收數量 = 實收數量;
                class_Output_Inspection_Data.效期 = 效期;
                class_Output_Inspection_Data.批號 = 批號;
                class_Output_Inspection_Data.驗收時間 = 驗收時間;
                class_Output_Inspection_Data.請購時間 = 請購時間;
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

                class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
                class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(data.Data[i]);
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
        [Route("get_od_Date")]
        [HttpGet]
        public string Get_OD_Date()
        {
            returnData returnData = new returnData();
            List<object[]> list_inspection = this.sQLControl_inspection.GetAllRows(null);

            list_inspection = list_inspection.Distinct(new Distinct_inspection_date()).ToList();
            returnData.Code = 200;
            returnData.Result = "取得請購日期表成功!";
            for(int i = 0; i < list_inspection.Count; i++)
            {
                class_output_inspection_date class_Output_Inspection_Date = new class_output_inspection_date();
                class_Output_Inspection_Date.請購單號 = Function_解析請購單號(list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.請購單號].ObjectToString());
                class_Output_Inspection_Date.驗收時間 = list_inspection[i][(int)enum_藥庫_驗收入庫_過帳明細.驗收時間].ToDateString();


                returnData.Data.Add(class_Output_Inspection_Date);
            }
            return returnData.JsonSerializationt(true);
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
                bool flag_驗收時間 = (x[(int)enum_藥庫_驗收入庫_過帳明細.驗收時間].ToDateString() == y[(int)enum_藥庫_驗收入庫_過帳明細.驗收時間].ToDateString());
                string 請購單號_x = x[(int)enum_藥庫_驗收入庫_過帳明細.請購單號].ObjectToString();
                請購單號_x = Function_解析請購單號(請購單號_x);
                string 請購單號_y = y[(int)enum_藥庫_驗收入庫_過帳明細.請購單號].ObjectToString();
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
