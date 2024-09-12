using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Basic;
using System.Text.Json.Serialization;

namespace HIS_DB_Lib
{
    [EnumDescription("textVision")]
    public enum enum_textVision
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("操作者姓名,VARCHAR,30,INDEX")]
        操作者姓名,
        [Description("操作者ID,VARCHAR,30,INDEX")]
        操作者ID,
        [Description("圖片,LONGTEXT,10,NONE")]
        圖片,
        [Description("操作時間,DATETIME,50,INDEX")]
        操作時間,
        [Description("Log,LONGTEXT,10,NONE")]
        Log,
        [Description("座標,VARCHAR,50,NONE")]
        座標,
        [Description("op_keyword,VARCHAR,100,NONE")]
        op_keyword,
        [Description("批號,VARCHAR,50,NONE")]
        批號,
        [Description("單號,VARCHAR,50,NONE")]
        單號,
        [Description("藥名,VARCHAR,50,NONE")]
        藥名,
        [Description("中文名,VARCHAR,50,NONE")]
        中文名,
        [Description("數量,VARCHAR,10,NONE")]
        數量,
        [Description("效期,DATETIME,50,NONE")]
        效期,
        [Description("藥品碼,VARCHAR,30,INDEX")]
        藥品碼,
        [Description("批號信心分數,VARCHAR,20,NONE")]
        批號信心分數,
        [Description("效期信心分數,VARCHAR,20,NONE")]
        效期信心分數,
        [Description("單號信心分數,VARCHAR,20,NONE")]
        單號信心分數,
        [Description("藥名信心分數,VARCHAR,20,NONE")]
        藥名信心分數,
        [Description("中文名信心分數,VARCHAR,20,NONE")]
        中文名信心分數,
        [Description("數量信心分數,VARCHAR,20,NONE")]
        數量信心分數,
    }
    [EnumDescription("med_code_srch")]
    public enum enum_med_code_srch
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("辨識中文名,VARCHAR,50,INDEX")]
        辨識中文名,
        [Description("辨識藥名,VARCHAR,50,INDEX")]
        辨識藥名,
        [Description("藥品碼,VARCHAR,30,NONE")]
        藥品碼,
        [Description("藥名,VARCHAR,50,NONE")]
        藥名, 
        [Description("中文名,VARCHAR,50,NONE")]
        中文名,
        [Description("操作時間,DATETIME,50,NONE")]
        操作時間
    }
    /// <summary>
    /// TextVision 資料
    /// </summary>
    public class textVisionClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 操作者姓名
        /// </summary>
        [JsonPropertyName("op_name")]
        public string 操作者姓名 { get; set; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        [JsonPropertyName("op_id")]
        public string 操作者ID { get; set; }
        /// <summary>
        /// base64
        /// </summary>
        [JsonPropertyName("base64")]
        public string 圖片 { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [JsonPropertyName("op_time")]
        public string 操作時間 { get; set; }
        /// <summary>
        /// Log
        /// </summary>
        [JsonPropertyName("logs")]
        public object Log { get; set; }
        /// <summary>
        /// 座標
        /// </summary>
        [JsonPropertyName("roi")]
        public string 座標 { get; set; }
        /// <summary>
        /// keyword
        /// </summary>
        [JsonPropertyName("op_keywords")]
        public string op_keyword { get; set; }
        /// <summary>
        /// 批號
        /// </summary>
        [JsonPropertyName("batch_num")]
        public string 批號 { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [JsonPropertyName("po_num")]
        public string 單號 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("name")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        [JsonPropertyName("cht_name")]
        public string 中文名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("qty")]
        public string 數量 { get; set; }
        /// <summary>
        /// 效期
        /// </summary>
        [JsonPropertyName("expirydate")]
        public string 效期 { get; set; }
        /// <summary>
        /// 藥品碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥品碼 { get; set; }
        /// <summary>
        /// 批號信心分數
        /// </summary>
        [JsonPropertyName("batch_num_conf")]
        public string 批號信心分數 { get; set; }
        /// <summary>
        /// 效期信心分數
        /// </summary>
        [JsonPropertyName("expirydate_conf")]
        public string 效期信心分數 { get; set; }
        /// <summary>
        /// 單號信心分數
        /// </summary>
        [JsonPropertyName("po_num_conf")]
        public string 單號信心分數 { get; set; }
        /// <summary>
        /// 藥名信心分數
        /// </summary>
        [JsonPropertyName("name_conf")]
        public string 藥名信心分數 { get; set; }
        /// <summary>
        /// 中文名信心分數
        /// </summary>
        [JsonPropertyName("cht_name_conf")]
        public string 中文名信心分數 { get; set; }
        /// <summary>
        /// 數量信心分數
        /// </summary>
        [JsonPropertyName("qty_conf")]
        public string 數量信心分數 { get; set; }
        static public returnData ai_analyze(List<textVisionClass> textVisionClasses)
        {
            string url = $"http://127.0.0.1:3000/PO_Vision";

            returnData returnData = new returnData();
            returnData.Data = textVisionClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_AI = json_out.JsonDeserializet<returnData>();
            if (returnData_AI == null) return null;
            if (returnData_AI.Code != 200) return null;
            //List<textVisionClass> out_textVisionClass = returnData.Data.ObjToClass<List<textVisionClass>>();
            Console.WriteLine($"{returnData}");
            return returnData_AI;
        }
    }
    public class medCodeSrchClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 辨識單GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 辨識中文名
        /// </summary>
        [JsonPropertyName("recog_cht_name")]
        public string 辨識中文名 { get; set; }
        /// <summary>
        /// 辨識藥名
        /// </summary>
        [JsonPropertyName("recog_name")]
        public string 辨識藥名 { get; set; }
        /// <summary>
        /// 藥品碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥品碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("name")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        [JsonPropertyName("cht_name")]
        public string 中文名 { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [JsonPropertyName("op_time")]
        public string 操作時間 { get; set; }
    }
}
