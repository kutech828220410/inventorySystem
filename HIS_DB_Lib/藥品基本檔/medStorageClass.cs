using Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HIS_DB_Lib
{
    [EnumDescription("medStorage")]
    public enum enum_medStroage
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥品碼,VARCHAR,50,INDEX")]
        藥品碼,
        [Description("儲位描述,VARCHAR,50,NONE")]
        儲位描述,
    
    }
    public class medStorageClass
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 藥品碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥品碼 { get; set; }
        /// <summary>
        /// 儲位描述
        /// </summary>
        [JsonPropertyName("storage")]
        public string 儲位描述 { get; set; }
        static public Dictionary<string, List<medStorageClass>> ToDictByCode(List<medStorageClass> medStorageClasses)
        {
            Dictionary<string, List<medStorageClass>> dictionary = new Dictionary<string, List<medStorageClass>>();
            foreach (var item in medStorageClasses)
            {
                if (dictionary.TryGetValue(item.藥品碼, out List<medStorageClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.藥品碼] = new List<medStorageClass>() { item };
                }
            }
            return dictionary;
        }
        static public List<medStorageClass> GetDictByCode(Dictionary<string, List<medStorageClass>> dict, string 藥品碼)
        {
            if (dict.TryGetValue(藥品碼, out List<medStorageClass> medStorageClass))
            {
                return medStorageClass;
            }
            else
            {
                return new List<medStorageClass>();
            }
        }
        static public returnData add_autoUpdate(string API_Server, List<medStorageClass> medStorageClass)
        {
            string url = $"{API_Server}/api/medStorage/add_autoUpdate";
            returnData returnData = new returnData();
            returnData.Data = medStorageClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            return returnData;
        }
        static public returnData add(string API_Server, List<medStorageClass> medStorageClass)
        {
            string url = $"{API_Server}/api/medStorage/add";
            returnData returnData = new returnData();
            returnData.Data = medStorageClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            return returnData;
        }

    }
}
