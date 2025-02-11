using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;


namespace HIS_DB_Lib
{
    public enum enum_bed_status
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("轉床時間,DATETIME,10,NONE")]
        轉床時間,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("姓名,VARCHAR,50,NONE")]
        姓名,
        [Description("住院號,VARCHAR,50,NONE")]
        住院號,
        [Description("病歷號,VARCHAR,50,NONE")]
        病歷號,
        [Description("轉床前護理站床號,VARCHAR,50,NONE")]
        轉床前護理站床號,
        [Description("轉床後護理站床號,VARCHAR,50,NONE")]
        轉床後護理站床號,


    }
    public class bedStatusClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 轉床時間
        /// </summary>
        [JsonPropertyName("move_time")]
        public string 轉床時間 { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("status")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonPropertyName("name")]
        public string 姓名 { get; set; }
        /// <summary>
        /// 住院號
        /// </summary>
        [JsonPropertyName("caseno")]
        public string 住院號 { get; set; }
        /// <summary>
        /// 病歷號
        /// </summary>
        [JsonPropertyName("histno")]
        public string 病歷號 { get; set; }
        /// <summary>
        /// 轉床前護理站床號
        /// </summary>
        [JsonPropertyName("bed_old")]
        public string 轉床前護理站床號 { get; set; }
        /// <summary>
        /// 轉床後護理站床號
        /// </summary>
        [JsonPropertyName("bed_new")]
        public string 轉床後護理站床號 { get; set; }
        
    }
}