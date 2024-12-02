using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using System.Text.Json.Serialization;


namespace HIS_DB_Lib
{
    public enum enum_medCount
    {
        GUID,
        圖片,
        藥名,
        數量,
        種類,
    }
    public class medCountClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("base64")]
        public string 圖片 { get; set; }
        [JsonPropertyName("img_stream")]
        public byte[] img_stream { get; set; }
        [JsonPropertyName("name")]
        public string 藥名 { get; set; }
        [JsonPropertyName("qty")]
        public string 數量 { get; set; }
        [JsonPropertyName("type")]
        public string 種類 { get; set; }
        [JsonPropertyName("ai_value")]
        public List<aiCountResult> AI結果 { get; set; }
        [JsonPropertyName("value")]
        public List<positionClass> 識別位置 { get; set; }
        static public List<medCountClass> ai_medCount(string API, List<medCountClass> medCountClasses)
        {
            string url = $"{API}/Pill_recognition";
            returnData returnData = new returnData();
            returnData.Data = medCountClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            if(json_out.StringIsEmpty())
            {
                url = "http://127.0.0.1:3200/Pill_recognition";
                json_out = Net.WEBApiPostJson(url, json_in);
            }
            returnData = json_out.JsonDeserializet<returnData>();
            if(returnData == null) return null;
            if (returnData.Result == "false") return null;
            //if (returnData.Code != 200) return null;
            List<medCountClass> out_medCountClass = returnData.Data.ObjToClass<List<medCountClass>>();
            return out_medCountClass;
        }
    }
    public class aiCountResult
    {
        [JsonPropertyName("conf")]
        public string 信心分數 { get; set; }
        [JsonPropertyName("coord")]
        public string 座標 { get; set; } 
    }
    public class positionClass
    {
        [JsonPropertyName("key_word")]
        public string keyWord { get; set; }
        [JsonPropertyName("height")]
        public string 高 { get; set; }
        [JsonPropertyName("width")]
        public string 寬 { get; set; }
        [JsonPropertyName("center")]
        public string 中心 { get; set; }
        [JsonPropertyName("conf")]
        public string 信心分數 { get; set; }

    }

}
