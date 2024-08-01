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
    [EnumDescription("med_qty")]
    public enum enum_藥品總量
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥局,VARCHAR,10,NONE")]
        藥局,
        [Description("護理站,VARCHAR,10,NONE")]
        護理站,
        [Description("藥碼,VARCHAR,10,NONE")]
        藥碼,
        [Description("藥品名,VARCHAR,10,INDEX")]
        藥品名,
        [Description("已調劑數量,VARCHAR,10,NONE")]
        已調劑數量, 
        [Description("總數量,VARCHAR,10,NONE")]
        總數量,
        [Description("已調劑劑量,VARCHAR,10,NONE")]
        已調劑劑量,
        [Description("總劑量,VARCHAR,10,NONE")]
        總劑量,
        [Description("單位,VARCHAR,10,NONE")]
        單位,
        [Description("病床清單,LONGTEXT,10,NONE")]
        病床清單
    }
    public class medQtyClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("phar")]
        public string 藥局 { get; set; }
        [JsonPropertyName("hnursta")]
        public string 護理站 { get; set; }
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        [JsonPropertyName("name")]
        public string 藥品名 { get; set; }
        [JsonPropertyName("dis_lqnty")]
        public string 已調劑數量 { get; set; }
        [JsonPropertyName("t_lqnty")]
        public string 總數量 { get; set; }
        [JsonPropertyName("dis_dosage")]
        public string 已調劑劑量 { get; set; }
        [JsonPropertyName("t_dosage")]
        public string 總劑量 { get; set; }
        [JsonPropertyName("dunit")]
        public string 單位 { get; set; }
        [JsonPropertyName("bed_list")]
        public object 病床清單 { get; set; }
    }
    public class bedListClass
    {
        [JsonPropertyName("hbedno")]
        public string 床號 { get; set; }
        [JsonPropertyName("lqnty")]
        public string 數量 { get; set; }
        [JsonPropertyName("dosage")]
        public string 劑量 { get; set; }
        [JsonPropertyName("dispens_status")]
        public string 調劑狀態 { get; set; }
    }
}

