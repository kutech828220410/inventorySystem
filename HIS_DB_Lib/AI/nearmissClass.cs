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
    public enum enum_nearmiss_status
    {
        無異狀,
        未更改,
        已更改
    }
    public enum enum_nearmiss_errorType
    {
        A藥名錯誤,
        B途徑錯誤,
        C劑量錯誤,
        D頻率錯誤,
        E劑型錯誤,
        F數量錯誤,
        G多種藥物組合,
        H重複用藥,
        Z其他,
    }
    [EnumDescription("nearmiss")]
    public enum enum_nearmiss
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("PRI_KEY,VARCHAR,50,INDEX")]
        PRI_KEY,
        [Description("ORDER_PRI_KEY,VARCHAR,200,INDEX")]
        ORDER_PRI_KEY,
        [Description("病歷號,VARCHAR,20,NONE")]
        病歷號,
        [Description("科別,VARCHAR,20,NONE")]
        科別,
        [Description("醫生姓名,VARCHAR,30,NONE")]
        醫生姓名,
        [Description("開方時間,DATETIME,30,NONE")]
        開方時間,
        [Description("加入時間,DATETIME,30,NONE")]
        加入時間,
        [Description("藥袋類型,VARCHAR,20,NONE")]
        藥袋類型,
        [Description("錯誤類別,VARCHAR,20,NONE")]
        錯誤類別,
        [Description("簡述事件,VARCHAR,2000,NONE")]
        簡述事件,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("調劑人員,VARCHAR,30,NONE")]
        調劑人員,
        [Description("調劑時間,DATETIME,200,INDEX")]
        調劑時間,
        [Description("提報人員,VARCHAR,30,NONE")]
        提報人員,
        [Description("提報等級,VARCHAR,20,NONE")]
        提報等級,
        [Description("提報時間,DATETIME,200,INDEX")]
        提報時間,
        [Description("處理人員,VARCHAR,30,NONE")]
        處理人員,
        [Description("處理時間,DATETIME,200,INDEX")]
        處理時間,
        [Description("通報TPR,VARCHAR,20,NONE")]
        通報TPR,
        [Description("備註,VARCHAR,500,NONE")]
        備註,

    }
    public class nearmissClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }

        [JsonPropertyName("PRI_KEY")]
        public string PRI_KEY { get; set; }

        [JsonPropertyName("ORDER_PRI_KEY")]
        public string ORDER_PRI_KEY { get; set; }

        [JsonPropertyName("PATCODE")]
        public string 病歷號 { get; set; }

        [JsonPropertyName("SECTNO")]
        public string 科別 { get; set; }

        [JsonPropertyName("DOCTOR")]
        public string 醫生姓名 { get; set; }

        [JsonPropertyName("ORDER_TIME")]
        public string 開方時間 { get; set; }

        [JsonPropertyName("CREATE_TIME")]
        public string 加入時間 { get; set; }

        [JsonPropertyName("BRYPE")]
        public string 藥袋類型 { get; set; }

        [JsonPropertyName("ERROR_TYPE_STRING")]
        public string 錯誤類別 { get; set; }

        [JsonPropertyName("EVENT_DESC")]
        public string 簡述事件 { get; set; }

        [JsonPropertyName("STATUS")]
        public string 狀態 { get; set; }

        [JsonPropertyName("OPERATOR")]
        public string 調劑人員 { get; set; }

        [JsonPropertyName("OPERATE_TIME")]
        public string 調劑時間 { get; set; }

        [JsonPropertyName("REPORTER")]
        public string 提報人員 { get; set; }
        
        [JsonPropertyName("REPORT_LEVEL")]
        public string 提報等級 { get; set; }

        [JsonPropertyName("REPORT_TIME")]
        public string 提報時間 { get; set; }

        [JsonPropertyName("HANDLER")]
        public string 處理人員 { get; set; }

        [JsonPropertyName("HANDLE_TIME")]
        public string 處理時間 { get; set; }

        [JsonPropertyName("TPR_NOTIFY")]
        public string 通報TPR { get; set; }

        [JsonPropertyName("REMARK")]
        public string 備註 { get; set; }

        [JsonPropertyName("identified")]
        public string  辨識註記{ get; set; }

        public string MED_BAG_SN { get; set; }
        public string error { get; set; }
        public List<string> error_type { get; set; }
        public string response { get; set; }
        static public nearmissClass add(string API_Server, nearmissClass nearmissClasses)
        {
            string url = $"{API_Server}/api/nearmiss/add";

            returnData returnData = new returnData();
            returnData.Data = nearmissClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData = json_out.JsonDeserializet<returnData>();
            nearmissClass nearmissClass = returnData.Data.ObjToClass<nearmissClass>();
            return nearmissClass;
        }
        static public List<nearmissClass> get_by_order_PRI_KEY(string API_Server, string 藥袋條碼)
        {
            string url = $"{API_Server}/api/nearmiss/get_by_order_PRI_KEY";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(藥袋條碼);

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData = json_out.JsonDeserializet<returnData>();
            List<nearmissClass> nearmissClasses = returnData.Data.ObjToClass<List<nearmissClass>>();
            return nearmissClasses;
        }
        static public nearmissClass Excute(string API, PrescriptionSet PrescriptionSet)
        {
            returnData returnData = new returnData();
            returnData.Data = PrescriptionSet;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(API, json_in);
            nearmissClass nearmissClass = json_out.JsonDeserializet<nearmissClass>();

            return nearmissClass;
        }
        static public nearmissClass medGPT(string API_Server, List<OrderClass> orderClasses)
        {
            (int code, string resuult, nearmissClass nearmissClass) = medGPT_full(API_Server, orderClasses);
            return nearmissClass;
        }
        static public (int code , string resuult ,nearmissClass nearmissClass) medGPT_full(string API_Server, List<OrderClass> orderClasses)
        {
            string url = $"{API_Server}/api/nearmiss/medGPT";
            returnData returnData = new returnData();
            returnData.Data = orderClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }
            Console.WriteLine($"{returnData_out}");
            nearmissClass nearmissClass = json_out.JsonDeserializet<nearmissClass>();
            return (returnData_out.Code, returnData_out.Result, nearmissClass);

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
