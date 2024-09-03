using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HIS_DB_Lib
{
    [EnumDescription("med_Inter")]
    public enum enum_med_Inter
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("ATC,VARCHAR,10,NONE")]
        ATC,
    }
    /// <summary>
    /// medInterClass 資料
    /// </summary>
    public class medInterClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// ATC
        /// </summary>
        [JsonPropertyName("ATC")]
        public string ATC { get; set; }
        static public List<medInterClass> update_med_inter(string API, List<medInterClass> medInterClasses)
        {
            string url = $"{API}/api/med_interaction/update_med_inter";

            returnData returnData = new returnData();
            returnData.Data = medInterClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medInterClass> out_medInterClass = returnData.Data.ObjToClass<List<medInterClass>>();
            Console.WriteLine($"{returnData}");
            return out_medInterClass;
        }
    }
}
