using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

using System.ComponentModel;

using System.Reflection;

namespace HIS_DB_Lib
{
    [EnumDescription("stockRecord")]
    public enum enum_stockRecord
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("庫別,VARCHAR,50,None")]
        庫別,
        [Description("庫名,VARCHAR,50,None")]
        庫名,
        [Description("加入時間,DATETIME,50,None")]
        加入時間,
    }

    [EnumDescription("stockRecord_content")]
    public enum enum_stockRecord_content
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("藥碼,VARCHAR,15,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,None")]
        藥名,
        [Description("庫存,VARCHAR,15,None")]
        庫存,
        [Description("加入時間,DATETIME,50,INDEX")]
        加入時間,

    }
    public class stockRecord
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("add_time")]
        public string 加入時間 { get; set; }
        [JsonPropertyName("store_type")]
        public string 庫別 { get; set; }
        [JsonPropertyName("store_name")]
        public string 庫名 { get; set; }
        [JsonPropertyName("contents")]
        public List<stockRecord_content> Contents { get => contents; set => contents = value; }
        private List<stockRecord_content> contents = new List<stockRecord_content>();
        

    }
    public class stockRecord_content
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        [JsonPropertyName("stock_qty")]
        public string 庫存 { get; set; }
        [JsonPropertyName("add_time")]
        public string 加入時間 { get; set; }

    }

}
