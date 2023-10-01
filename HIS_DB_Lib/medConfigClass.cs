using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_藥品設定表
    {
        GUID,
        藥品碼,
        效期管理,
        盲盤,
        複盤,
        結存報表,
        雙人覆核,
        麻醉藥品,
        形狀相似,
        發音相似,
        自定義,
    }

    public class medConfigClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("expiry")]
        public string 效期管理 { get; set; }
        [JsonPropertyName("blind")]
        public string 盲盤 { get; set; }
        [JsonPropertyName("recheck")]
        public string 複盤 { get; set; }
        [JsonPropertyName("inventoryReport")]
        public string 結存報表 { get; set; }
        [JsonPropertyName("isAnesthetic")]
        public string 麻醉藥品 { get; set; }
        [JsonPropertyName("isShapeSimilar")]
        public string 形狀相似 { get; set; }
        [JsonPropertyName("isSoundSimilar")]
        public string 發音相似 { get; set; }
        [JsonPropertyName("customVar")]
        public string 自定義 { get; set; }
    }
}
