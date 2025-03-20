using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Reflection;

namespace HIS_DB_Lib
{
    [EnumDescription("medList_public")]
    public enum enum_medList_public
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥品碼,VARCHAR,20,INDEX")]
        藥品碼,
    }
    [EnumDescription("medList_selfControl")]
    public enum enum_medList_selfControl
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥品碼,VARCHAR,20,INDEX")]
        藥品碼,
    }

    public class medListClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("med_cloud")]
        public medClass 雲端藥檔 { get; set; }
        static public returnData get_all(string API_Server, string tableName)
        {
            string url = $"{API_Server}/api/medList/get_all";
            returnData returnData = new returnData();
            returnData.TableName = tableName;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            return returnData_out;
        }
    }
}
