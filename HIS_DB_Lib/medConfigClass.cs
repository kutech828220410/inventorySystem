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
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 藥品碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        /// <summary>
        /// 效期管理
        /// </summary>
        [JsonPropertyName("expiry")]
        public string 效期管理 { get; set; }
        /// <summary>
        /// 盲盤
        /// </summary>
        [JsonPropertyName("blind")]
        public string 盲盤 { get; set; }
        /// <summary>
        /// 複盤
        /// </summary>
        [JsonPropertyName("recheck")]
        public string 複盤 { get; set; }
        /// <summary>
        /// 結存報表
        /// </summary>
        [JsonPropertyName("inventoryReport")]
        public string 結存報表 { get; set; }
        /// <summary>
        /// 雙人覆核
        /// </summary>
        [JsonPropertyName("dual_verification")]
        public string 雙人覆核 { get; set; }
        /// <summary>
        /// 麻醉藥品
        /// </summary>
        [JsonPropertyName("isAnesthetic")]
        public string 麻醉藥品 { get; set; }
        /// <summary>
        /// 形狀相似
        /// </summary>
        [JsonPropertyName("isShapeSimilar")]
        public string 形狀相似 { get; set; }
        /// <summary>
        /// 發音相似
        /// </summary>
        [JsonPropertyName("isSoundSimilar")]
        public string 發音相似 { get; set; }
        /// <summary>
        /// 自定義
        /// </summary>
        [JsonPropertyName("customVar")]
        public string 自定義 { get; set; }
    }
}
