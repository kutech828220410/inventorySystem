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
    public enum enum_suspiciousRxLog_ReportLevel
    {
        [Description("None")]
        None,       // 一般
        [Description("Normal")]
        Normal,       // 一般
        [Description("Important")]
        Important,    // 重要
        [Description("Critical")]
        Critical      // 緊急
    }
    public enum enum_suspiciousRxLog_status
    {
        無異狀,
        未更改,
        已更改
    }
    public enum enum_suspiciousRxLog_errorType
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
    public enum enum_suspiciousRxLog_export
    {            
        病歷號,
        科別,
        醫生姓名,
        開方時間,
        加入時間,
        藥袋類型,
        錯誤類別,
        簡述事件,
        狀態,
        調劑人員,
        調劑時間,
        提報人員,
        提報等級,
        提報時間,
        處理人員,
        處理時間,
        通報TPR,
        備註,
    }
    /// <summary>
    /// 醫師處方疑義紀錄表
    /// </summary>
    [EnumDescription("suspiciousRxLog")]
    public enum enum_suspiciousRxLog
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥袋條碼,VARCHAR,200,INDEX")]
        藥袋條碼,
        [Description("病歷號,VARCHAR,20,NONE")]
        病歷號,
        [Description("科別,VARCHAR,20,NONE")]
        科別,
        [Description("醫生姓名,VARCHAR,30,NONE")]
        醫生姓名,
        [Description("診斷碼,VARCHAR,30,NONE")]
        診斷碼,
        [Description("診斷內容,VARCHAR,1000,NONE")]
        診斷內容,
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
    public enum enum_suspiciousRxLog_rule
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("索引,VARCHAR,50,NONE")]
        索引,
        [Description("群組,VARCHAR,50,NONE")]
        群組,
        [Description("規則,VARCHAR,100,NONE")]
        規則,
        [Description("規則描述,VARCHAR,200,NONE")]
        規則描述,
        [Description("軟體,VARCHAR,20,NONE")]
        軟體,
        [Description("類別,VARCHAR,20,NONE")]
        類別,
        [Description("狀態,VARCHAR,10,NONE")]
        狀態,
    }
    public class suspiciousRxLogClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }

        [JsonPropertyName("Barcode")]
        public string 藥袋條碼 { get; set; }

        [JsonPropertyName("PATCODE")]
        public string 病歷號 { get; set; }

        [JsonPropertyName("SECTNO")]
        public string 科別 { get; set; }

        [JsonPropertyName("DOCTOR")]
        public string 醫生姓名 { get; set; }

        [JsonPropertyName("ICD_CODE")]
        public string 診斷碼 { get; set; }

        [JsonPropertyName("ICD_DESC")]
        public string 診斷內容 { get; set; }

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
        public string 辨識註記 { get; set; }

        public string MED_BAG_SN { get; set; }
        public string error { get; set; }
        public List<string> error_type { get; set; }
        public string response { get; set; }
        public class ICP_By_OP_Time : IComparer<suspiciousRxLogClass>
        {
            public int Compare(suspiciousRxLogClass x, suspiciousRxLogClass y)
            {
                return x.加入時間.CompareTo(y.加入時間);
            }
        }
        static public suspiciousRxLogClass add(string API_Server, suspiciousRxLogClass suspiciousRxLogClasses)
        {
            string url = $"{API_Server}/api/suspiciousRxLog/add";

            returnData returnData = new returnData();
            returnData.Data = suspiciousRxLogClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData = json_out.JsonDeserializet<returnData>();
            suspiciousRxLogClass suspiciousRxLogClass = returnData.Data.ObjToClass<suspiciousRxLogClass>();
            return suspiciousRxLogClass;
        }
        static public List<suspiciousRxLogClass> get_by_barcode(string API_Server, string 藥袋條碼)
        {
            string url = $"{API_Server}/api/suspiciousRxLog/get_by_barcode";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(藥袋條碼);

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData = json_out.JsonDeserializet<returnData>();
            List<suspiciousRxLogClass> suspiciousRxLogClasses = returnData.Data.ObjToClass<List<suspiciousRxLogClass>>();
            return suspiciousRxLogClasses;
        }
        static public suspiciousRxLogClass Excute(string API, PrescriptionSet PrescriptionSet)
        {
            returnData returnData = new returnData();
            returnData.Data = PrescriptionSet;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(API, json_in);
            suspiciousRxLogClass suspiciousRxLogClass = json_out.JsonDeserializet<suspiciousRxLogClass>();
            Logger.LogAddLine();
            Logger.Log("MedGPT", returnData.Data.JsonSerializationt(true));
            return suspiciousRxLogClass;
        }
        static public suspiciousRxLogClass medGPT(string API_Server, List<OrderClass> orderClasses)
        {
            (int code, string resuult, suspiciousRxLogClass suspiciousRxLogClass) = medGPT_full(API_Server, orderClasses);
            return suspiciousRxLogClass;
        }
        static public (int code, string resuult, suspiciousRxLogClass suspiciousRxLogClass) medGPT_full(string API_Server, List<OrderClass> orderClasses)
        {
            string url = $"{API_Server}/api/suspiciousRxLog/medGPT";
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
            List<suspiciousRxLogClass> suspiciousRxLogClasses = returnData_out.Data.ObjToClass<List<suspiciousRxLogClass>>();
            if (suspiciousRxLogClasses == null) return (0, "suspiciousRxLogClasses == null", null);
            if (suspiciousRxLogClasses.Count == 0) return (0, "suspiciousRxLogClasses.Count == 0", null);
            return (returnData_out.Code, returnData_out.Result, suspiciousRxLogClasses[0]);

        }
        static public suspiciousRxLogClass update(string API_Server, suspiciousRxLogClass suspiciousRxLogClass)
        {
            List<suspiciousRxLogClass> suspiciousRxLogClasses = new List<suspiciousRxLogClass>();
            suspiciousRxLogClasses.Add(suspiciousRxLogClass);

            (int code, string resuult, List<suspiciousRxLogClass> _suspiciousRxLogClasses) = update_full(API_Server, suspiciousRxLogClasses);
            if (_suspiciousRxLogClasses == null) return null;
            if (_suspiciousRxLogClasses.Count == 0) return null;
            return _suspiciousRxLogClasses[0];
        }
        static public List<suspiciousRxLogClass> update(string API_Server, List<suspiciousRxLogClass> suspiciousRxLogClasses)
        {
            (int code, string resuult, List<suspiciousRxLogClass> _suspiciousRxLogClasses) = update_full(API_Server, suspiciousRxLogClasses);
            return suspiciousRxLogClasses;
        }
        static public (int code, string resuult, suspiciousRxLogClass suspiciousRxLogClass) update_full(string API_Server, suspiciousRxLogClass suspiciousRxLogClass)
        {
            List<suspiciousRxLogClass> suspiciousRxLogClasses = new List<suspiciousRxLogClass>();
            suspiciousRxLogClasses.Add(suspiciousRxLogClass);
            (int code, string resuult, List<suspiciousRxLogClass> _suspiciousRxLogClasses) = update_full(API_Server, suspiciousRxLogClasses);
            return (code, resuult, suspiciousRxLogClass);
        }
        static public (int code, string resuult, List<suspiciousRxLogClass> suspiciousRxLogClass) update_full(string API_Server, List<suspiciousRxLogClass> suspiciousRxLogClasses)
        {
            string url = $"{API_Server}/api/suspiciousRxLog/update";
            returnData returnData = new returnData();
            returnData.Data = suspiciousRxLogClasses;

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
            suspiciousRxLogClasses = returnData_out.Data.ObjToClass<List<suspiciousRxLogClass>>();
            return (returnData_out.Code, returnData_out.Result, suspiciousRxLogClasses);

        }
    }
    public class suspiciousRxLog_ruleClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("index")]
        public string 索引 { get; set; }
        [JsonPropertyName("group")]
        public string 群組 { get; set; }
        [JsonPropertyName("rule")]
        public string 規則 { get; set; }
        [JsonPropertyName("rule_detail")]
        public string 規則描述 { get; set; }
        [JsonPropertyName("software")]
        public string 軟體 { get; set; }
        [JsonPropertyName("type")]
        public string 類別 { get; set; }
        [JsonPropertyName("state")]
        public string 狀態 { get; set; }
        public class ICP_By_index : IComparer<suspiciousRxLog_ruleClass>
        {
            public int Compare(suspiciousRxLog_ruleClass x, suspiciousRxLog_ruleClass y)
            {
                int result = (x.索引).CompareTo(y.索引);
                return result;
            }
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
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("HI_CODE")]
        public string 健保碼 { get; set; }
        [JsonPropertyName("TYPE")]
        public string 類別 { get; set; }
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
        [JsonPropertyName("SD")]
        public string 單次劑量 { get; set; }
        [JsonPropertyName("DUNIT")]
        public string 劑量單位 { get; set; }
    }
    public class Prescription
    {
        [JsonPropertyName("MED_BAG_SN")]
        public string 藥袋條碼 { get; set; }
        [JsonPropertyName("DOC")]
        public string 醫師代碼 { get; set; }
        [JsonPropertyName("PATNAME")]
        public string 病人姓名 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 產出時間 { get; set; }
        [JsonPropertyName("SECTNO")]
        public string 科別 { get; set; }
        [JsonPropertyName("order")]
        public List<DrugOrder> 處方 { get; set; }

    }
}
