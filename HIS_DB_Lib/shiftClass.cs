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
    [EnumDescription("shift")]

    public enum enum_shift
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("班別,VARCHAR,200,INDEX")]
        班別,
        [Description("開始時間,VARCHAR,20,NONE")]
        開始時間,
        [Description("結束時間,VARCHAR,30,NONE")]
        結束時間,
    }

    public class shiftClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 班別
        /// </summary>
        [JsonPropertyName("shift_name")]
        public string 班別 { get; set; }
        /// <summary>
        /// 開始時間
        /// </summary>
        [JsonPropertyName("start_time")]
        public string 開始時間 { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        [JsonPropertyName("end_time")]
        public string 結束時間 { get; set; }
        public class ICP_By_name : IComparer<shiftClass>
        {
            private Dictionary<string, int> shiftOrder = new Dictionary<string, int>
            {
                { "早班", 0 },
                { "小夜班", 1 },
                { "大夜班", 2 }
            };

            public int Compare(shiftClass x, shiftClass y)
            {              
                return shiftOrder[x.班別].CompareTo(shiftOrder[y.班別]);
            }
        }
        static public List<shiftClass> get_all(string API_Server)
        {
            string url = $"{API_Server}/api/shift/get_all";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<shiftClass> shiftClasses = returnData.Data.ObjToClass<List<shiftClass>>();
            return shiftClasses;
        }
        static public List<shiftClass> update(string API_Server,string 班別, string 開始時間, string 結束時間)
        {
            string url = $"{API_Server}/api/shift/update";
            returnData returnData = new returnData();
            returnData.ValueAry.Add(班別);
            returnData.ValueAry.Add(開始時間);
            returnData.ValueAry.Add(結束時間);
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<shiftClass> shiftClasses = returnData.Data.ObjToClass<List<shiftClass>>();
            return shiftClasses;
        }
        static public shiftClass get_shift_name_by_name(string API_Server ,string 現在時間)
        {
            string url = $"{API_Server}/api/shift/get_shift_name_by_name";
            returnData returnData = new returnData();
            returnData.ValueAry.Add(現在時間);
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null) return null;
            if (returnData_out.Code != 200) return null;
            shiftClass shiftClasses = returnData_out.Data.ObjToClass<shiftClass>();
            return shiftClasses;
        }
    }
    
}
