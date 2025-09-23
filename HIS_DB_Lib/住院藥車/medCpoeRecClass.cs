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
        [Description("更新時間,DATETIME,10,INDEX")]
        更新時間,
        [Description("藥局,VARCHAR,10,INDEX")]
        藥局,
        [Description("護理站,VARCHAR,10,INDEX")]
        護理站,
        [Description("床號,VARCHAR,10,NONE")]
        床號,
        [Description("住院號,VARCHAR,50,INDEX")]
        住院號,
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
        [Description("退藥數量,VARCHAR,10,NONE")]
        退藥數量,
        [Description("劑量,VARCHAR,10,NONE")]
        劑量,
        [Description("單位,VARCHAR,10,NONE")]
        單位,
        [Description("頻次代碼,VARCHAR,10,NONE")]
        頻次代碼,
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
        /// 退藥數量
        /// </summary>
        [JsonPropertyName("returnQty")]
        public string 退藥數量 { get; set; }
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
        /// 頻次代碼
        /// </summary>
        [JsonPropertyName("freqn")]
        public string 頻次 { get; set; }

        public class ICP_By_UP_Time : IComparer<medCpoeRecClass>
        {

            public int Compare(medCpoeRecClass x, medCpoeRecClass y)
            {
                return x.更新時間.CompareTo(y.更新時間) * -1;
            }
        }
        public class ICP_By_UP_BedNum : IComparer<medCpoeRecClass>
        {
            public int Compare(medCpoeRecClass x, medCpoeRecClass y)
            {
                int result = (x.床號.StringToInt32()).CompareTo(y.床號.StringToInt32());
                if (result == 0)
                {
                    result = string.Compare(x.更新時間, y.更新時間) * -1;
                }
                return result;
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
        static public List<patientInfoClass> get_medChange_by_GUID(string API_Server, List<string> Info)
        {
            List<patientInfoClass> out_medCarInfoClass = new List<patientInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_medChange_by_GUID";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_medCarInfoClass = returnData.Data.ObjToClass<List<patientInfoClass>>();
            return out_medCarInfoClass;

        }
        static public Dictionary<string, List<medCpoeRecClass>> ToDictByMasterGUID(List<medCpoeRecClass> medCpoeRecClasses)
        {
            Dictionary<string, List<medCpoeRecClass>> dictionary = new Dictionary<string, List<medCpoeRecClass>>();
            foreach (var item in medCpoeRecClasses)
            {
                if (dictionary.TryGetValue(item.Master_GUID, out List<medCpoeRecClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.Master_GUID] = new List<medCpoeRecClass> { item };
                }
            }
            return dictionary;
        }
        static public List<medCpoeRecClass> GetDictByMasterGUID(Dictionary<string, List<medCpoeRecClass>> dict, string master_GUID)
        {
            if (dict.TryGetValue(master_GUID, out List<medCpoeRecClass> medCpoeRecClasses))
            {
                return medCpoeRecClasses;
            }
            else
            {
                return new List<medCpoeRecClass>();
            }
        }
    }
}