using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace HIS_DB_Lib
{


    [EnumDescription("bed_status")]
    public enum enum_bed_status
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("PRI_KEY,VARCHAR,50,INDEX")]
        PRI_KEY,
        [Description("轉床時間,DATETIME,10,NONE")]
        轉床時間,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("姓名,VARCHAR,50,NONE")]
        姓名,
        [Description("住院號,VARCHAR,50,NONE")]
        住院號,
        [Description("病歷號,VARCHAR,50,NONE")]
        病歷號,
        [Description("轉床前護理站床號,VARCHAR,50,NONE")]
        轉床前護理站床號,
        [Description("轉床後護理站床號,VARCHAR,50,NONE")]
        轉床後護理站床號,


    }
    public class bedStatusClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// PRI_KEY
        /// </summary>
        [JsonPropertyName("PRI_KEY")]
        public string PRI_KEY { get; set; }
        /// <summary>
        /// 轉床時間
        /// </summary>
        [JsonPropertyName("move_time")]
        public string 轉床時間 { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("status")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonPropertyName("name")]
        public string 姓名 { get; set; }
        /// <summary>
        /// 住院號
        /// </summary>
        [JsonPropertyName("caseno")]
        public string 住院號 { get; set; }
        /// <summary>
        /// 病歷號
        /// </summary>
        [JsonPropertyName("histno")]
        public string 病歷號 { get; set; }
        /// <summary>
        /// 轉床前護理站床號
        /// </summary>
        [JsonPropertyName("bed_old")]
        public string 轉床前護理站床號 { get; set; }
        /// <summary>
        /// 轉床後護理站床號
        /// </summary>
        [JsonPropertyName("bed_new")]
        public string 轉床後護理站床號 { get; set; }
        public class ICP_By_UP_Time : IComparer<bedStatusClass>
        {

            public int Compare(bedStatusClass x, bedStatusClass y)
            {
                return x.轉床時間.CompareTo(y.轉床時間) * -1;
            }
        }
        static public Dictionary<string, List<bedStatusClass>> ToDictByMasterGUID(List<bedStatusClass> bedStatusClasses)
        {
            Dictionary<string, List<bedStatusClass>> dictionary = new Dictionary<string, List<bedStatusClass>>();
            foreach (var item in bedStatusClasses)
            {
                if (dictionary.TryGetValue(item.Master_GUID, out List<bedStatusClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.Master_GUID] = new List<bedStatusClass> { item };
                }
            }
            return dictionary;
        }
        static public List<bedStatusClass> GetByMasterGUID(Dictionary<string, List<bedStatusClass>> dict, string master_GUID)
        {
            if (dict.TryGetValue(master_GUID, out List<bedStatusClass> bedStatusClasses))
            {
                return bedStatusClasses;
            }
            else
            {
                return new List<bedStatusClass>();
            }
        }
        static public Dictionary<string, List<bedStatusClass>> ToDictByID(List<bedStatusClass> bedStatusClasses)
        {
            Dictionary<string, List<bedStatusClass>> dictionary = new Dictionary<string, List<bedStatusClass>>();
            foreach (var item in bedStatusClasses)
            {
                if (dictionary.TryGetValue(item.病歷號, out List<bedStatusClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.病歷號] = new List<bedStatusClass> { item };
                }
            }
            return dictionary;
        }
        static public List<bedStatusClass> GetByID(Dictionary<string, List<bedStatusClass>> dict, string ID)
        {
            if (dict.TryGetValue(ID, out List<bedStatusClass> bedStatusClasses))
            {
                return bedStatusClasses;
            }
            else
            {
                return new List<bedStatusClass>();
            }
        }
        static public List<bedStatusClass> update_bed_status(string API_Server, List<bedStatusClass> bedStatusClasses)
        {
            List<bedStatusClass> out_bedStatusClass = new List<bedStatusClass>();
            string url = $"{API_Server}/api/med_cart/update_bed_status";

            returnData returnData = new returnData();
            returnData.Data = bedStatusClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_bedStatusClass = returnData.Data.ObjToClass<List<bedStatusClass>>();
            return out_bedStatusClass;
        }

        static public List<bedStatusClass> get_bed_status_all(string API_Server)
        {
            List<bedStatusClass> out_bedStatusClass = new List<bedStatusClass>();
            string url = $"{API_Server}/api/med_cart/get_bed_status_all";

            returnData returnData = new returnData();

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_bedStatusClass = returnData.Data.ObjToClass<List<bedStatusClass>>();
            return out_bedStatusClass;
        }

    }

}