using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_交班藥品設定
    {
        GUID,
        藥品碼,
        是否交班,
    }


    public class medShiftConfigClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("IS_SHIFT")]
        public string 是否交班 { get; set; }
    }

    public enum enum_交班表_交班明細
    {
        GUID,
        藥碼,
        藥名,
        現有庫存,
        處方支出,
        處方數量,
        實際庫存,
        起始時間,
        結束時間,
        管制級別,
    }
    public class medShiftListClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        [JsonPropertyName("TOTAL_VALUE")]
        public string 現有庫存 { get; set; }
        [JsonPropertyName("PRESC_COST")]
        public string 處方支出 { get; set; }
        [JsonPropertyName("PRESC_NUM")]
        public string 處方數量 { get; set; }
        [JsonPropertyName("CURRENT__VALUE")]
        public string 實際庫存 { get; set; }
        [JsonPropertyName("ST_TIME")]
        public string 起始時間 { get; set; }
        [JsonPropertyName("END_TIME")]
        public string 結束時間 { get; set; }
        [JsonPropertyName("DRUGKIND")]
        public string 管制級別 { get; set; }
    }
}
