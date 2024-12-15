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
    [EnumDescription("voxVerify")]
    public enum enum_voxVerify
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("程式名稱,VARCHAR,50,NONE")]
        程式名稱,
        [Description("內容,VARCHAR,50,NONE")]
        內容,
        [Description("信心分數,VARCHAR,50,NONE")]
        信心分數,
        [Description("ServerName,VARCHAR,50,NONE")]
        ServerName,
        [Description("ServerType,VARCHAR,50,NONE")]
        ServerType,
    }
    public class voxVerifyClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 程式名稱
        /// </summary>
        [JsonPropertyName("program_name")]
        public string 程式名稱 { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        [JsonPropertyName("value")]
        public string 內容 { get; set; }
        /// <summary>
        /// 信心分數
        /// </summary>
        [JsonPropertyName("conf")]
        public string 信心分數 { get; set; }
        /// <summary>
        /// ServerName
        /// </summary>
        [JsonPropertyName("ServerName")]
        public string ServerName { get; set; }
        /// <summary>
        /// ServerType
        /// </summary>
        [JsonPropertyName("ServerType")]
        public string ServerType { get; set; }
    }
}
