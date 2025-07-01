using Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HIS_DB_Lib.系統
{
    [EnumDescription("webTraffic")]
    public enum enum_webTraffic
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("頁面,VARCHAR,200,INDEX")]
        頁面,
        [Description("參數,VARCHAR,20,NONE")]
        參數,
        [Description("裝置IP,VARCHAR,20,INDEX")]
        裝置IP,
        [Description("使用者ID,VARCHAR,20,NONE")]
        使用者ID,
        [Description("使用者姓名,VARCHAR,50,NONE")]
        使用者姓名,
        [Description("加入時間,DATETIME,50,NONE")]
        加入時間
    }
    public class webTrafficClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }

        [JsonPropertyName("page")]
        public string 頁面 { get; set; }

        [JsonPropertyName("parameter")]
        public string 參數 { get; set; }

        [JsonPropertyName("ip")]
        public string 裝置IP { get; set; }

        [JsonPropertyName("user_id")]
        public string 使用者ID { get; set; }

        [JsonPropertyName("user_name")]
        public string 使用者姓名 { get; set; }

        [JsonPropertyName("add_time")]
        public string 加入時間 { get; set; }
    }
}
