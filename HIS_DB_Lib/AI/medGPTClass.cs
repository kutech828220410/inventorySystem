using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Web;
using NPOI.SS.Formula.Functions;

namespace HIS_DB_Lib
{
    [EnumDescription("medGpt")]

    public enum enum_medGpt
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("病歷號,VARCHAR,20,NONE")]
        病歷號,
        [Description("醫生姓名,VARCHAR,30,NONE")]
        醫生姓名,
        [Description("開方時間,DATETIME,30,NONE")]
        開方時間,
        [Description("藥袋類型,VARCHAR,20,NONE")]
        藥袋類型,
        [Description("錯誤類別,VARCHAR,20,NONE")]
        錯誤類別,
        [Description("簡述事件,VARCHAR,500,NONE")]
        簡述事件,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("操作人員,VARCHAR,20,NONE")]
        操作人員,
        [Description("處理人員,VARCHAR,20,NONE")]
        處理人員,
        [Description("操作時間,DATETIME,200,INDEX")]
        操作時間,
        [Description("通報TPR,VARCHAR,20,NONE")]
        通報TPR,
    }
    public class medGPTClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 病歷號
        /// </summary>
        [JsonPropertyName("PATCODE")]
        public string 病歷號 { get; set; }
        /// <summary>
        /// 醫生姓名
        /// </summary>
        [JsonPropertyName("DOCTOR")]
        public string 醫生姓名 { get; set; }
        /// <summary>
        /// 開方時間
        /// </summary>
        [JsonPropertyName("ORDER_TIME")]
        public string 開方時間 { get; set; }
        /// <summary>
        /// 藥袋類型
        /// </summary>
        [JsonPropertyName("BRYPE")]
        public string 藥袋類型 { get; set; }
        /// <summary>
        /// 錯誤類別
        /// </summary>
        [JsonPropertyName("ERROR_TYPE_STRING")]
        public string 錯誤類別 { get; set; }
        /// <summary>
        /// 簡述事件
        /// </summary>
        [JsonPropertyName("EVENT_DESC")]
        public string 簡述事件 { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("STATUS")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 操作人員
        /// </summary>
        [JsonPropertyName("OPERATOR")]
        public string 操作人員 { get; set; }
        /// <summary>
        /// 處理人員
        /// </summary>
        [JsonPropertyName("HANDLER")]
        public string 處理人員 { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [JsonPropertyName("OPERATE_TIME")]
        public string 操作時間 { get; set; }
        /// <summary>
        /// 通報TPR
        /// </summary>
        [JsonPropertyName("TPR_NOTIFY")]
        public string 通報TPR { get; set; }

        public string MED_BAG_SN { get; set; }
        public string error { get; set; }
        public List<string> error_type { get; set; }
        public string response { get; set; }
        static public medGPTClass add(string API_Server, medGPTClass medGPTClasses)
        {
            string url = $"{API_Server}/api/medgpt/add";

            returnData returnData = new returnData();
            returnData.Data = medGPTClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData = json_out.JsonDeserializet<returnData>();
            medGPTClass medGPTClass = returnData.Data.ObjToClass<medGPTClass>();
            return medGPTClass;
        }
        static public medGPTClass Excute(string API, PrescriptionSet PrescriptionSet)
        {
            returnData returnData = new returnData();
            returnData.Data = PrescriptionSet;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(API, json_in);
            medGPTClass medGPTClass = json_out.JsonDeserializet<medGPTClass>();

            return medGPTClass;
        }
        static public medGPTClass analyze(string API_Server, List<OrderClass> orderClasses)
        {
            string url = $"{API_Server}/api/medgpt/analyze";

            returnData returnData = new returnData();
            returnData.Data = orderClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            medGPTClass medGPTClass = json_out.JsonDeserializet<medGPTClass>();
            return medGPTClass;
        }
    }
    public class PrescriptionSet
    {
        [JsonPropertyName("eff_order")]
        public List<Prescription> 有效處方 { get; set; }
        [JsonPropertyName("old_order")]
        public List<Prescription> 歷史處方 { get; set; }

    }
    public class DrugOrder
    {
        [JsonPropertyName("CTYPE")]
        public string 費用別 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("HI_CODE")]
        public string 健保碼 { get; set; }
        [JsonPropertyName("ATC")]
        public string ATC { get; set; }
        [JsonPropertyName("LICENSE")]
        public string 藥品許可證號 { get; set; }
        [JsonPropertyName("DIANAME")]
        public string 藥品學名 { get; set; }
        [JsonPropertyName("DRUGKIND")]
        public string 管制級別 { get; set; }
        [JsonPropertyName("TXN_QTY")]
        public string 交易量 { get; set; }
        [JsonPropertyName("FREQ")]
        public string 頻次 { get; set; }
        [JsonPropertyName("DAYS")]
        public string 天數 { get; set; }
    }
    public class Prescription
    {
        [JsonPropertyName("MED_BAG_SN")]
        public string 藥袋條碼 { get; set; }
        [JsonPropertyName("DOC")]
        public string 醫師代碼 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 產出時間 { get; set; }
        [JsonPropertyName("SECTNO")]
        public string 科別 { get; set; }
        [JsonPropertyName("order")]
        public List<DrugOrder> 處方 { get; set; }
    }
}
