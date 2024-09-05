using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace HIS_DB_Lib
{
    [EnumDescription("med_info")]
    public enum enum_med_info
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥碼,VARCHAR,10,NONE")]
        藥碼,
        [Description("藥品名,VARCHAR,150,NONE")]
        藥品名,
        [Description("頻次代碼,VARCHAR,10,NONE")]
        頻次代碼,
        [Description("劑量,VARCHAR,10,NONE")]
        劑量,
        [Description("序號,VARCHAR,10,NONE")]
        序號,
        [Description("分類號,VARCHAR,10,NONE")]
        分類號,
        [Description("類別名,VARCHAR,10,NONE")]
        類別名,
        [Description("藥品通名,VARCHAR,10,NONE")]
        藥品通名,
        [Description("藥品商品名,VARCHAR,10,NONE")]
        藥品商品名,
        [Description("藥品分類,VARCHAR,10,NONE")]
        藥品分類,
        [Description("藥品治療分類,VARCHAR,10,NONE")]
        藥品治療分類,
        [Description("適應症,VARCHAR,10,NONE")]
        適應症,
        [Description("用法劑量,VARCHAR,10,NONE")]
        用法劑量,
        [Description("備註,VARCHAR,10,NONE")]
        備註,
        [Description("售價,VARCHAR,10,NONE")]
        售價,
        [Description("健保價,VARCHAR,10,NONE")]
        健保價,
        [Description("更新時間,DATETIME,10,NONE")]
        更新時間,
    }
    public class medInfoClass
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥品名
        /// </summary>
        [JsonPropertyName("name")]
        public string 藥品名 { get; set; }
        /// <summary>
        /// 頻次代碼
        /// </summary>
        [JsonPropertyName("freqn")]
        public string 頻次代碼 { get; set; }
        /// <summary>
        /// 劑量
        /// </summary>
        [JsonPropertyName("dosage")]
        public string 劑量 { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [JsonPropertyName("id")]
        public string 序號 { get; set; }
        /// <summary>
        /// 分類號
        /// </summary>
        [JsonPropertyName("classno")]
        public string 分類號 { get; set; }
        /// <summary>
        /// 類別名
        /// </summary>
        [JsonPropertyName("classname")]
        public string 類別名 { get; set; }
        /// <summary>
        /// 藥品通名
        /// </summary>
        [JsonPropertyName("generic")]
        public string 藥品通名 { get; set; }
        /// <summary>
        /// 藥品商品名
        /// </summary>
        [JsonPropertyName("trade")]
        public string 藥品商品名 { get; set; }
        /// <summary>
        /// 藥品分類
        /// </summary>
        [JsonPropertyName("class")]
        public string 藥品分類 { get; set; }
        /// <summary>
        /// 藥品治療分類
        /// </summary>
        [JsonPropertyName("txclass")]
        public string 藥品治療分類 { get; set; }
        /// <summary>
        /// 適應症
        /// </summary>
        [JsonPropertyName("indic")]
        public string 適應症 { get; set; }
        /// <summary>
        /// 用法劑量
        /// </summary>
        [JsonPropertyName("dosage_info")]
        public string 用法劑量 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("note")]
        public string 備註 { get; set; }
        /// <summary>
        /// 售價
        /// </summary>
        [JsonPropertyName("cost")]
        public string 售價 { get; set; }
        /// <summary>
        /// 健保價
        /// </summary>
        [JsonPropertyName("price")]
        public string 健保價 { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        [JsonPropertyName("update_time")]
        public string 更新時間 { get; set; }
        static public List<medInfoClass> update_med_info(string API_Server, List<medInfoClass> medInfoClass)
        {
            string url = $"{API_Server}/api/med_cart/update_med_info";

            returnData returnData = new returnData();
            returnData.Data = medInfoClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medInfoClass> out_medInfoClass = returnData.Data.ObjToClass<List<medInfoClass>>();
            Console.WriteLine($"{returnData}");
            return out_medInfoClass;
        }
        static public List<medInfoClass> get_medInfo_by_code(string API_Server, List<string> code)
        {
            string url = $"{API_Server}/api/med_cart/get_medInfo_by_code";

            returnData returnData = new returnData ();
            returnData.ValueAry = code;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medInfoClass> out_medInfoClass = returnData.Data.ObjToClass<List<medInfoClass>>();
            Console.WriteLine($"{returnData}");
            return out_medInfoClass;
        }

    }
}
