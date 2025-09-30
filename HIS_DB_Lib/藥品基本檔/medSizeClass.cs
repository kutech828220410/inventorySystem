using Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HIS_DB_Lib
{
    [EnumDescription("medSize")]
    public enum enum_medSize
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,

        [Description("藥碼,VARCHAR,50,INDEX")]
        藥碼,

        [Description("貨品長,VARCHAR,50,NONE")]
        貨品長,

        [Description("貨品高,VARCHAR,50,NONE")]
        貨品高,

        [Description("貨品深,VARCHAR,50,NONE")]
        貨品深,

        [Description("數量,VARCHAR,50,NONE")]
        數量,
    }
    public class medSizeClass
    {
        /// <summary>
        /// GUID (唯一識別碼)
        /// </summary>
        [JsonPropertyName("GUID")]
        [Description("GUID,VARCHAR,50,PRIMARY")]
        public string GUID { get; set; }

        /// <summary>
        /// 藥品碼
        /// </summary>
        [JsonPropertyName("code")]
        [Description("藥碼,VARCHAR,50,INDEX")]
        public string 藥碼 { get; set; }

        /// <summary>
        /// 貨品長
        /// </summary>
        [JsonPropertyName("length")]
        [Description("貨品長,VARCHAR,50,NONE")]
        public string 貨品長 { get; set; }

        /// <summary>
        /// 貨品高
        /// </summary>
        [JsonPropertyName("height")]
        [Description("貨品高,VARCHAR,50,NONE")]
        public string 貨品高 { get; set; }

        /// <summary>
        /// 貨品深
        /// </summary>
        [JsonPropertyName("depth")]
        [Description("貨品深,VARCHAR,50,NONE")]
        public string 貨品深 { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("quantity")]
        [Description("數量,VARCHAR,50,NONE")]
        public string 數量 { get; set; }
    }
}
