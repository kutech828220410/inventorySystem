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
    public enum enum_藥品群組
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("名稱,VARCHAR,200,NONE")]
        名稱,
        [Description("建立時間,DATETIME,200,NONE")]
        建立時間,        
    }
    public enum enum_藥品群組明細
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,200,INDEX")]
        Master_GUID,
        [Description("藥品碼,VARCHAR,20,INDEX")]
        藥品碼,
    }
    public class medGroupClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 建立時間 { get; set; }
       
        private List<medClass> medClasses = new List<medClass>();
        public List<medClass> MedClasses { get => medClasses; set => medClasses = value; }


        static public List<SQLUI.Table> init(string API_Server)
        {
            string url = $"{API_Server}/api/medGroup/init";

            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            List<SQLUI.Table> tables = json_out.JsonDeserializet<List<SQLUI.Table>>();
            return tables;
        }
        static public medGroupClass get_all_group(string API_Server, string name)
        {
            List<medGroupClass> medGroupClasses = get_all_group(API_Server);
            medGroupClasses = (from temp in medGroupClasses
                               where temp.名稱 == name
                               select temp).ToList();
            if (medGroupClasses.Count == 0) return null;
            return medGroupClasses[0];
        }
        static public List<medGroupClass> get_all_group(string API_Server)
        {
            string url = $"{API_Server}/api/medGroup/get_all_group";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null) return null;
            if (returnData_out.Code != 200) return null;
            Console.WriteLine($"{returnData}");
            List<medGroupClass> medGroupClasses = returnData_out.Data.ObjToClass<List<medGroupClass>>();

            return medGroupClasses;
        }
        
        static public void add_group(string API_Server, medGroupClass medGroupClass)
        {
            string url = $"{API_Server}/api/medGroup/add_group";
            returnData returnData = new returnData();
            returnData.Data = medGroupClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();       
            if (returnData_out == null) return;
            if (returnData_out.Code != 200) return;
            Console.WriteLine($"{returnData}");
        }
        static public void delete_group_by_guid(string API_Server, medGroupClass medGroupClass)
        {
            delete_group_by_guid(API_Server, medGroupClass);
        }
        static public void delete_group_by_guid(string API_Server, string GUID)
        {
            string url = $"{API_Server}/api/medGroup/delete_group_by_guid";
            returnData returnData = new returnData();
            returnData.Value = GUID;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null) return;
            if (returnData_out.Code != 200) return;
            Console.WriteLine($"{returnData}");
        }
        static public void delete_meds_in_group(string API_Server, string GUID, List<medClass> medClasses)
        {
            medGroupClass medGroupClass = new medGroupClass();
            medGroupClass.GUID = GUID;
            medGroupClass.medClasses = medClasses;
            delete_meds_in_group(API_Server, medGroupClass);
        }
        static public void delete_meds_in_group(string API_Server, medGroupClass medGroupClass)
        {
            string url = $"{API_Server}/api/medGroup/delete_meds_in_group";
            returnData returnData = new returnData();
            returnData.Data = medGroupClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null) return;
            if (returnData_out.Code != 200) return;
            Console.WriteLine($"{returnData}");
        }
        static public void add_meds_in_group(string API_Server, medGroupClass medGroupClass)
        {
            string url = $"{API_Server}/api/medGroup/add_meds_in_group";
            returnData returnData = new returnData();
            returnData.Data = medGroupClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null) return;
            if (returnData_out.Code != 200) return;
            Console.WriteLine($"{returnData}");
        }


    }
  
}
