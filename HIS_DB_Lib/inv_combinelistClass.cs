using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
namespace HIS_DB_Lib
{
    public enum enum_合併總單
    {
        GUID,
        合併單名稱,
        合併單號,
        建表人,
        建表時間,
        備註,
    }
    public enum enum_合併單明細
    {
        GUID,
        Master_GUID,
        合併單號,
        單號,
        類型,
        新增時間,
        備註,
    }
    /// <summary>
    /// 合併總單
    /// </summary>
    public class inv_combinelistClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單名稱
        /// </summary>
        [JsonPropertyName("INV_NAME")]
        public string 合併單名稱 { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("INV_SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 建表人
        /// </summary>
        [JsonPropertyName("CT")]
        public string 建表人 { get; set; }
        /// <summary>
        /// 建表時間
        /// </summary>
        [JsonPropertyName("CT_TIME")]
        public string 建表時間 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }

        /// <summary>
        /// 合併單明細
        /// </summary>
        [JsonPropertyName("records_Ary")]
        public List<inv_sub_combinelistClass> Records_Ary { get => records_Ary; set => records_Ary = value; }
        private List<inv_sub_combinelistClass> records_Ary = new List<inv_sub_combinelistClass>();


        static public List<inv_records_Class> get_all_records(string API_Server)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_all_records";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_records_Class> inv_Records_Classes = returnData.Data.ObjToClass<List<inv_records_Class>>();
            if (inv_Records_Classes == null) return null;
            if (inv_Records_Classes.Count == 0) return null;
            return inv_Records_Classes;
        }
        static public inv_combinelistClass get_all_inv(string API_Server, string SN)
        {
            List<inv_combinelistClass> inv_CombinelistClasses = get_all_inv(API_Server);
            inv_CombinelistClasses = (from temp in inv_CombinelistClasses
                                      where temp.合併單號 == SN
                                      select temp).ToList();
            if (inv_CombinelistClasses.Count == 0) return null;
            return inv_CombinelistClasses[0];
        }
        static public List<inv_combinelistClass> get_all_inv(string API_Server, DateTime dateTime_st, DateTime dateTime_end)
        {
            List<inv_combinelistClass> inv_CombinelistClasses = get_all_inv(API_Server);
            inv_CombinelistClasses = (from temp in inv_CombinelistClasses
                                      where (temp.建表時間.StringToDateTime() >= dateTime_st) && temp.建表時間.StringToDateTime() <= dateTime_end
                                      select temp).ToList();
            return inv_CombinelistClasses;
        }
        static public List<inv_combinelistClass> get_all_inv(string API_Server)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_all_inv";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_combinelistClass> inv_CombinelistClasses = returnData.Data.ObjToClass<List<inv_combinelistClass>>();
            if (inv_CombinelistClasses == null) return null;
            if (inv_CombinelistClasses.Count == 0) return null;
            return inv_CombinelistClasses;
        }
        static public inv_combinelistClass inv_creat_update(string API_Server, inv_combinelistClass inv_CombinelistClass)
        {
            string url = $"{API_Server}/api/inv_combinelist/inv_creat_update";
            returnData returnData = new returnData();
            returnData.Data = inv_CombinelistClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            inv_CombinelistClass = returnData.Data.ObjToClass<inv_combinelistClass>();
            if (inv_CombinelistClass == null) return null;
            return inv_CombinelistClass;
        }
        static public List<inventoryClass.content> get_full_inv_by_SN(string API_Server , string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_full_inv_by_SN";
            returnData returnData = new returnData();
            returnData.Value = SN;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inventoryClass.content> contents = returnData.Data.ObjToClass<List<inventoryClass.content>>();
            if (contents == null) return null;
            if (contents.Count == 0) return null;
            return contents;
        }

        
    }
    /// <summary>
    /// 合併單明細
    /// </summary>
    public class inv_sub_combinelistClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// REF_GUID
        /// </summary>
        [JsonPropertyName("REF_GUID")]
        public string REF_GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("INV_SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 單號 { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        [JsonPropertyName("TYPE")]
        public string 類型 { get; set; }
        /// <summary>
        /// 新增時間
        /// </summary>
        [JsonPropertyName("ADD_TIME")]
        public string 新增時間 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }
    }
    public class inv_records_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 單號 { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        [JsonPropertyName("TYPE")]
        public string 類型 { get; set; }
    }
}
