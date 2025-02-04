using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HIS_DB_Lib
{
    [EnumDescription("settingPage")]

    public enum enum_settingPage
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("頁面名稱,VARCHAR,200,INDEX")]
        頁面名稱,
        [Description("欄位名稱,VARCHAR,20,NONE")]
        欄位名稱,
        [Description("欄位代碼,VARCHAR,30,NONE")]
        欄位代碼,
        [Description("欄位種類,VARCHAR,20,NONE")]
        欄位種類,
        [Description("選項,VARCHAR,50,NONE")]
        選項,
        [Description("設定值,VARCHAR,100,NONE")]
        設定值,

    }
    public class settingPageClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 頁面名稱
        /// </summary>
        [JsonPropertyName("page_name")]
        public string 頁面名稱 { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        [JsonPropertyName("cht")]
        public string 欄位名稱 { get; set; }
        /// <summary>
        /// 欄位代碼
        /// </summary>
        [JsonPropertyName("name")]
        public string 欄位代碼 { get; set; }
        /// <summary>
        /// 欄位種類
        /// </summary>
        [JsonPropertyName("value_type")]
        public string 欄位種類 { get; set; }
        /// <summary>
        /// 選項
        /// </summary>
        [JsonPropertyName("option_str")]
        public string 選項 { get; set; }
        /// <summary>
        /// value_db
        /// </summary>
        [JsonPropertyName("value_db")]
        public string 設定值 { get; set; }
        public List<string> option { get; set; }
        public object value { get; set; }

    }
    public class uiConfig
    {
        [JsonPropertyName("cht")]
        public string 欄位名稱 { get; set; }
        [JsonPropertyName("name")]
        public string 欄位代碼 { get; set; }
        [JsonPropertyName("value")]
        public string 設定值 { get; set; }
    }
}
