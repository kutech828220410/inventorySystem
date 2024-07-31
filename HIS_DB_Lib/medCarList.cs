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
    [EnumDescription("med_carList")]
    public enum enum_護理站清單
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥局,VARCHAR,30,INDEX")]
        藥局,
        [Description("護理站,VARCHAR,30,INDEX")]
        護理站
    }
    public class medCarListClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("phar")]
        public string 藥局 { get; set; }
        [JsonPropertyName("hnursta")]
        public string 護理站 { get; set; }
    }
}
