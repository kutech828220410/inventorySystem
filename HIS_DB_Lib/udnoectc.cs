using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_udnoectc
    {
        GUID,
        病房,
        床號,
        病人姓名,
        病歷號,
        生日,
        性別,
        身高,
        體重,
        診斷,
        科別,
        開立醫師,
        過敏記錄,
        RegimenName,
        天數順序,
        診別,
        就醫序號,
        醫囑序號,
        化學治療前檢核項目,
        加入時間,
    }
    public class udnoectc
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("hnursta")]
        public string 病房 { get; set; }
        [JsonPropertyName("hbed")]
        public string 床號 { get; set; }
        [JsonPropertyName("hnamec")]
        public string 病人姓名 { get; set; }
        [JsonPropertyName("hhisnum")]//key
        public string 病歷號 { get; set; }
        [JsonPropertyName("hbirthdt")]
        public string 生日 { get; set; }
        [JsonPropertyName("hsexc")]
        public string 性別 { get; set; }
        [JsonPropertyName("hheight")]
        public string 身高 { get; set; }
        [JsonPropertyName("hweight")]
        public string 體重 { get; set; }
        [JsonPropertyName("hdiagtxt")]
        public string 診斷 { get; set; }
        [JsonPropertyName("csect")]
        public string 科別 { get; set; }
        [JsonPropertyName("udoename")]
        public string 開立醫師 { get; set; }
        [JsonPropertyName("halergy")]
        public string 過敏記錄 { get; set; }
        [JsonPropertyName("udregnam")]
        public string RegimenName { get; set; }
        [JsonPropertyName("uddaytxt")]
        public string 天數順序 { get; set; }
        [JsonPropertyName("hcasetyp")]//key
        public string 診別 { get; set; }
        [JsonPropertyName("hcaseno")]//key
        public string 就醫序號 { get; set; }
        [JsonPropertyName("udrelseq")]//key
        public string 醫囑序號 { get; set; }
        [JsonPropertyName("labdatas")]
        public List<string> labdatas 
        { 
            get
            {
                return 化學治療前檢核項目.JsonDeserializet<List<string>>();
            }
            set
            {
                化學治療前檢核項目 = value.JsonSerializationt();
            }
        }
        [JsonPropertyName("ctdate")]
        public string 加入時間 { get; set; }

        public string 化學治療前檢核項目 { get; set; }

        [JsonPropertyName("orders")]
        public List<udnoectc_ordersClass> ordersAry { get; set; }

        [JsonPropertyName("ctcvars")]
        public List<udnoectc_ctcvarsClass> ctcvarsAry { get; set; }

        

    }

    public enum enum_udnoectc_orders
    {
        GUID,
        Master_GUID,
        藥囑序號,
        服藥順序,
        藥碼,
        藥名,
        警示,
        劑量,
        單位,
        途徑,
        頻次,
        儲位1,
        儲位2,
        備註,
        數量,
        處方開始時間,
        處方結束時間,
    }
    public class udnoectc_ordersClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        [JsonPropertyName("udordseq")]
        public string 藥囑序號 { get; set; }
        [JsonPropertyName("serno")]
        public string 服藥順序 { get; set; }
        [JsonPropertyName("uddrgno")]
        public string 藥碼 { get; set; }
        [JsonPropertyName("udrpname")]
        public string 藥名 { get; set; }
        [JsonPropertyName("udhamsg")]
        public string 警示 { get; set; }
        [JsonPropertyName("uddosage")]
        public string 劑量 { get; set; }
        [JsonPropertyName("uddosuni")]
        public string 單位 { get; set; }
        [JsonPropertyName("udroute")]
        public string 途徑 { get; set; }
        [JsonPropertyName("udfreqn")]
        public string 頻次 { get; set; }
        [JsonPropertyName("udstorn1")]
        public string 儲位1 { get; set; }
        [JsonPropertyName("udstorn2")]
        public string 儲位2 { get; set; }
        [JsonPropertyName("udothdes")]
        public string 備註 { get; set; }
        [JsonPropertyName("dspqty")]
        public string 數量 { get; set; }
        [JsonPropertyName("udbgndt")]
        public string 處方開始時間 { get; set; }
        [JsonPropertyName("udenddt")]
        public string 處方結束時間 { get; set; }
    }


    public enum enum_udnoectc_ctcvars
    {
        GUID,
        Master_GUID,
        藥名,
        變異時間,
        變異原因,
        變異內容,
        說明,
    }
    public class udnoectc_ctcvarsClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        [JsonPropertyName("udmdpnam")]
        public string 藥名 { get; set; }
        [JsonPropertyName("zudctcva")]
        public string 變異時間 { get; set; }
        [JsonPropertyName("typedesc")]
        public string 變異原因 { get; set; }
        [JsonPropertyName("varrsn")]
        public string 變異內容 { get; set; }
        [JsonPropertyName("vardata")]
        public string 說明 { get; set; }
    }
}
