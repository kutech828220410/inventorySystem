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
    [EnumDescription("med_carList")]
    public enum enum_med_carList
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥局,VARCHAR,30,INDEX")]
        藥局,
        [Description("藥局名,VARCHAR,30,NONE")]
        藥局名,
        [Description("護理站,VARCHAR,10,NONE")]
        護理站,
        [Description("排序,VARCHAR,10,NONE")]
        排序,
        [Description("交車時間,DATETIME,10,NONE")]
        交車時間,
        [Description("交車狀態,VARCHAR,10,NONE")]
        交車狀態,
    }
    public class medCarListClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("phar")]
        public string 藥局 { get; set; }
        [JsonPropertyName("phar_name")]
        public string 藥局名 { get; set; }
        [JsonPropertyName("hnursta")]
        public string 護理站 { get; set; }
        [JsonPropertyName("sequence")]
        public string 排序 { get; set; }
        [JsonPropertyName("hand_time")]
        public string 交車時間 { get; set; }
        [JsonPropertyName("hand_status")]
        public string 交車狀態 { get; set; }
        [JsonPropertyName("note")]
        public string 備註 { get; set; }
        public class ICP_By_phar_name : IComparer<medCarListClass>
        {
            public int Compare(medCarListClass x, medCarListClass y)
            {
                return (x.護理站).CompareTo(y.護理站);
            }
        }
        static public List<medCarListClass> get_medcar_by_phar(string API_Server, String 藥局)
        {
            string url = $"{API_Server}/api/medCarList/get_medcar_by_phar";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(藥局);

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medCarListClass> out_medCarListClass = returnData.Data.ObjToClass<List<medCarListClass>>();
            Console.WriteLine($"{returnData}");
            return out_medCarListClass;
        }

        static public List<medCarListClass> get_all(string API_Server)
        {
            string url = $"{API_Server}/api/medCarList/get_all";

            returnData returnData = new returnData();

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medCarListClass> out_medCarListClass = returnData.Data.ObjToClass<List<medCarListClass>>();
            Console.WriteLine($"{returnData}");
            return out_medCarListClass;
        }
    }
    

}
