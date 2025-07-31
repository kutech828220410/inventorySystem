using Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace HIS_DB_Lib
{
    [EnumDescription("labResult")]
    public enum enum_labResult
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("病歷號,VARCHAR,50,INDEX")]
        病歷號,
        [Description("檢驗項目代碼,VARCHAR,50,NONE")]
        檢驗項目代碼,
        [Description("檢驗醫令代碼,VARCHAR,50,NONE")]
        檢驗醫令代碼,
        [Description("檢驗項目,VARCHAR,50,NONE")]
        檢驗項目,
        [Description("檢驗結果,VARCHAR,50,NONE")]
        檢驗結果,
        [Description("檢驗時間,DATETIME,50,NONE")]
        檢驗時間,
        [Description("加入時間,DATETIME,50,NONE")]
        加入時間
    }
    public class labResultClass
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]

        public string GUID { get; set; }
        /// <summary>
        /// 病歷號
        /// </summary>
        [JsonPropertyName("PATCODE")]
        public string 病歷號 { get; set; }
        /// <summary>
        /// 檢驗項目代碼
        /// </summary>
        [JsonPropertyName("DIACODE")]
        public string 檢驗項目代碼 { get; set; }
        /// <summary>
        /// 檢驗醫令代碼
        /// </summary>
        [JsonPropertyName("INSCODE")]
        public string 檢驗醫令代碼 { get; set; }
        /// <summary>
        /// 檢驗項目
        /// </summary>
        [JsonPropertyName("EGNAME")]
        public string 檢驗項目 { get; set; }
        /// <summary>
        /// 檢驗結果
        /// </summary>
        [JsonPropertyName("STATE")]
        public string 檢驗結果 { get; set; }
        /// <summary>
        /// 檢驗時間
        /// </summary>
        [JsonPropertyName("REPTIME")]
        public string 檢驗時間 { get; set; }
        /// <summary>
        /// 加入時間
        /// </summary>
        [JsonPropertyName("ADDTIME")]
        public string 加入時間 { get; set; }
        public returnData add(string API, List<labResultClass> labResultClasses)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData returnData = new returnData();
            returnData.Data = labResultClasses;
            string url = $"{API}/api/labResult/add";
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            return returnData_out;
        }
        public returnData get_by_PATCODE(string API, string 病歷號)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData returnData = new returnData();
            returnData.ValueAry.Add(病歷號);
            string url = $"{API}/api/labResult/get_by_PATCODE";
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            return returnData_out;
        }
    }
}
