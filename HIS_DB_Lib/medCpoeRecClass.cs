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
    //病床處方
    //Computerized Physician Order Entry
    [EnumDescription("med_cpoe_rec")]
    public enum enum_med_cpoe_rec
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("更新時間,DATETIME,10,NONE")]
        更新時間,
        [Description("藥局,VARCHAR,10,INDEX")]
        藥局,
        [Description("護理站,VARCHAR,10,INDEX")]
        護理站,
        [Description("床號,VARCHAR,10,NONE")]
        床號,
        [Description("住院號,VARCHAR,50,INDEX")]
        住院號,
        [Description("調劑台,VARCHAR,30,NONE")]
        調劑台,
        [Description("序號,VARCHAR,10,NONE")]
        序號,
        [Description("狀態,VARCHAR,10,NONE")]
        狀態,
        [Description("開始時間,DATETIME,20,NONE")]
        開始時間,
        [Description("結束時間,DATETIME,20,NONE")]
        結束時間,
        [Description("藥碼,VARCHAR,10,NONE")]
        藥碼,
        [Description("藥品名,VARCHAR,150,NONE")]
        藥品名,
        [Description("中文名,VARCHAR,150,NONE")]
        中文名,
        [Description("數量,VARCHAR,10,NONE")]
        數量,
        [Description("劑量,VARCHAR,10,NONE")]
        劑量,
        [Description("單位,VARCHAR,10,NONE")]
        單位,
        [Description("處方醫師,VARCHAR,10,NONE")]
        處方醫師,
        [Description("處方醫師姓名,VARCHAR,10,NONE")]
        處方醫師姓名,
        [Description("操作人員,VARCHAR,10,NONE")]
        操作人員,
        [Description("藥局代碼,VARCHAR,10,NONE")]
        藥局代碼,
        [Description("藥局名稱,VARCHAR,20,NONE")]
        藥局名稱,
        [Description("大瓶點滴,VARCHAR,10,NONE")]
        大瓶點滴,
    }
    /// <summary>
    /// medCpoeClass資料
    /// </summary>
    public class medCpoeRecClass
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("MAster_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        [JsonPropertyName("update_time")]
        public string 更新時間 { get; set; }
        /// <summary>
        /// 藥局
        /// </summary>
        [JsonPropertyName("pharm")]
        public string 藥局 { get; set; }
        /// <summary>
        /// 護理站
        /// </summary>
        [JsonPropertyName("nurnum")]
        public string 護理站 { get; set; }
        /// <summary>
        /// 床號
        /// </summary>
        [JsonPropertyName("bednum")]
        public string 床號 { get; set; }
        /// <summary>
        /// 住院號
        /// </summary>
        [JsonPropertyName("caseno")]
        public string 住院號 { get; set; }
        /// <summary>
        /// 調劑台
        /// </summary>
        [JsonPropertyName("dispens_name")]
        public string 調劑台 { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [JsonPropertyName("ordseq")]
        public string 序號 { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("status")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 開始時間
        /// </summary>
        [JsonPropertyName("starttm")]
        public string 開始時間 { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        [JsonPropertyName("endtm")]
        public string 結束時間 { get; set; }
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
        /// 劑量
        /// </summary>
        [JsonPropertyName("dosage")]
        public string 劑量 { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [JsonPropertyName("dunit")]
        public string 單位 { get; set; }
        /// <summary>
        /// 處方醫師
        /// </summary>
        [JsonPropertyName("orsign")]
        public string 處方醫師 { get; set; }
        /// <summary>
        /// 處方醫師姓名
        /// </summary>
        [JsonPropertyName("signam")]
        public string 處方醫師姓名 { get; set; }
        /// <summary>
        /// 操作人員
        /// </summary>
        [JsonPropertyName("luser")]
        public string 操作人員 { get; set; }
        /// <summary>
        /// 藥局代碼
        /// </summary>
        [JsonPropertyName("pharm_code")]
        public string 藥局代碼 { get; set; }
        /// <summary>
        /// 藥局名稱
        /// </summary>
        [JsonPropertyName("pharm_name")]
        public string 藥局名稱 { get; set; }
        /// <summary>
        /// 大瓶點滴
        /// </summary>
        [JsonPropertyName("cnt02")]
        public string 大瓶點滴 { get; set; }

        public class ICP_By_UP_Time : IComparer<medCpoeRecClass>
        {

            public int Compare(medCpoeRecClass x, medCpoeRecClass y)
            {
                return x.更新時間.CompareTo(y.更新時間) * -1;
            }
        }
        static public List<medCpoeRecClass> update_med_CpoeRec(string API_Server, List<medCpoeRecClass> medCpoeRecClasses)
        {
            List<medCpoeRecClass> out_medCpoeRecClass = new List<medCpoeRecClass>();
            string url = $"{API_Server}/api/med_cart/update_med_cpoe_rec";

            returnData returnData = new returnData();
            returnData.Data = medCpoeRecClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_medCpoeRecClass = returnData.Data.ObjToClass<List<medCpoeRecClass>>();
            Console.WriteLine($"{returnData}");
            return out_medCpoeRecClass;
        }
        static public List<medCarInfoClass> get_medChange_by_GUID(string API_Server, List<string> Info)
        {
            List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_medChange_by_GUID";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
            return out_medCarInfoClass;

        }
    }
}
