using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Reflection;
using SQLUI;
using H_Pannel_lib;
using System.Text.Json;
namespace HIS_DB_Lib
{
    [EnumDescription("common_space_setup")]
    public enum enum_commonSpaceSetup
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("共用區名稱,VARCHAR,200,NONE")]
        共用區名稱,
        [Description("共用區類型,VARCHAR,50,NONE")]
        共用區類型,
        [Description("是否共用,VARCHAR,20,NONE")]
        是否共用,
        [Description("設置時間,DATETIME,20,NONE")]
        設置時間,
    }
    public class commonSpaceSetup
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("name")]
        public string 共用區名稱 { get; set; }
        [JsonPropertyName("type")]
        public string 共用區類型 { get; set; }
        [JsonPropertyName("enable")]
        public string 是否共用 { get; set; }
        [JsonPropertyName("setup_time")]
        public string 設置時間 { get; set; }
    }
}
