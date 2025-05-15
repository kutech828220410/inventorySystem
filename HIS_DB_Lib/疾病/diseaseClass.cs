using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Web;
using NPOI.SS.Formula.Functions;

namespace HIS_DB_Lib
{
    public enum enum_disease_EXCEL
    {
        code,
        英文名稱,
        中文名稱,
    }
    [EnumDescription("disease")]
    public enum enum_disease
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("疾病代碼,VARCHAR,50,INDEX")]
        疾病代碼,
        [Description("中文說明,VARCHAR,200,NONE")]
        中文說明,
        [Description("英文說明,VARCHAR,400,NONE")]
        英文說明,
    }
    

    public class diseaseClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("ICD")]
        public string 疾病代碼 { get; set; }
        [JsonPropertyName("CHT")]
        public string 中文說明 { get; set; }
        [JsonPropertyName("EN")]
        public string 英文說明 { get; set; }
        static public List<diseaseClass> get_by_ICD(string API_Server, List<string> ICDs)
        {
            string disease = string.Join(";", ICDs);
            return get_by_ICD(API_Server, disease);
        }
        static public List<diseaseClass> get_by_ICD(string API_Server, string ICD)
        {
            string url = $"{API_Server}/api/disease/get_by_ICD";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(ICD);
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<diseaseClass> out_diseaseClassClass = returnData.Data.ObjToClass<List<diseaseClass>>();
            Console.WriteLine($"{returnData}");
            return out_diseaseClassClass;
        }
        static public Dictionary<string, List<diseaseClass>> ToDictByICD(List<diseaseClass> diseaseClasses)
        {
            Dictionary<string, List<diseaseClass>> dictionary = new Dictionary<string, List<diseaseClass>>();
            foreach (var item in diseaseClasses)
            {
                if (dictionary.TryGetValue(item.疾病代碼, out List<diseaseClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.疾病代碼] = new List<diseaseClass> { item };
                }
            }
            return dictionary;
        }
        static public List<diseaseClass> GetByICD(Dictionary<string, List<diseaseClass>> dict, string master_GUID)
        {
            if (dict.TryGetValue(master_GUID, out List<diseaseClass> diseaseClasses))
            {
                return diseaseClasses;
            }
            else
            {
                return new List<diseaseClass>();
            }
        }
    }
}
