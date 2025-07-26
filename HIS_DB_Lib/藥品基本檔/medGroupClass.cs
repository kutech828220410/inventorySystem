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
    [EnumDescription("med_group")]
    public enum enum_medGroup
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("名稱,VARCHAR,200,NONE")]
        名稱,
        [Description("顯示資訊,VARCHAR,500,NONE")]
        顯示資訊,
        [Description("建立時間,DATETIME,200,NONE")]
        建立時間,        
    }
    [EnumDescription("med_sub_group")]
    public enum enum_sub_medGroup
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,200,INDEX")]
        Master_GUID,
        [Description("排列號,VARCHAR,10,None")]
        排列號,
        [Description("藥品碼,VARCHAR,20,INDEX")]
        藥品碼,
    }
    public class medGroupClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        [JsonPropertyName("VIS_INFO")]
        public string 顯示資訊 { get; set; } = "";
        [JsonPropertyName("CT_TIME")]
        public string 建立時間 { get; set; }

        private List<medClass> medClasses = new List<medClass>();
        public List<medClass> MedClasses { get => medClasses; set => medClasses = value; }

        /// <summary>
        /// 初始化藥品群組資料表
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <returns>藥品群組資料表列表</returns>
        static public List<SQLUI.Table> init(string API_Server)
        {
            string url = $"{API_Server}/api/medGroup/init";

            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            List<SQLUI.Table> tables = json_out.JsonDeserializet<List<SQLUI.Table>>();
            return tables;
        }


        /// <summary>
        /// 獲取所有藥品群組名稱列表
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <returns>藥品群組名稱列表</returns>
        static public List<string> get_medGroupList(string API_Server)
        {
            List<medGroupClass> medGroupClasses = get_all_group(API_Server);
            List<string> vs = (from temp in medGroupClasses
                               select temp.名稱).Distinct().ToList();
            return vs;
        }
        /// <summary>
        /// 根據名稱獲取藥品群組
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="name">藥品群組名稱</param>
        /// <returns>藥品群組</returns>
        static public medGroupClass get_medGroup(string API_Server, string name)
        {
            List<medGroupClass> medGroupClasses = get_all_group(API_Server);
            medGroupClasses = (from temp in medGroupClasses
                               where temp.名稱 == name
                               select temp).ToList();
            if (medGroupClasses.Count == 0) return null;
            return medGroupClasses[0];
        }       
        /// <summary>
        /// 根據名稱獲取藥品群組
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="name">藥品群組名稱</param>
        /// <returns>藥品群組</returns>
        static public medGroupClass get_all_group(string API_Server, string name)
        {
            List<medGroupClass> medGroupClasses = get_all_group(API_Server);
            medGroupClasses = (from temp in medGroupClasses
                               where temp.名稱 == name
                               select temp).ToList();
            if (medGroupClasses.Count == 0) return null;
            return medGroupClasses[0];
        }

        /// <summary>
        /// 獲取所有藥品群組
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <returns>藥品群組列表</returns>
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

        /// <summary>
        /// 獲取所有藥品群組名稱
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <returns>藥品群組名稱列表</returns>
        static public List<string> get_all_group_name(string API_Server)
        {
            List<medGroupClass> medGroupClasses = get_all_group(API_Server);
            List<string> vs = (from temp in medGroupClasses
                               select temp.名稱).Distinct().ToList();
            return vs;
        }

        /// <summary>
        /// 新增藥品群組
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="medGroupClass">藥品群組</param>
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

        /// <summary>
        /// 根據GUID刪除藥品群組
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="medGroupClass">藥品群組</param>
        static public void delete_group_by_guid(string API_Server, medGroupClass medGroupClass)
        {
            delete_group_by_guid(API_Server, medGroupClass.GUID);
        }

        /// <summary>
        /// 根據GUID刪除藥品群組
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="GUID">藥品群組GUID</param>
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

        /// <summary>
        /// 刪除藥品群組中的藥品
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="GUID">藥品群組GUID</param>
        /// <param name="medClasses">藥品列表</param>
        static public void delete_meds_in_group(string API_Server, string GUID, List<medClass> medClasses)
        {
            medGroupClass medGroupClass = new medGroupClass();
            medGroupClass.GUID = GUID;
            medGroupClass.medClasses = medClasses;
            delete_meds_in_group(API_Server, medGroupClass);
        }

        /// <summary>
        /// 刪除藥品群組中的藥品
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="medGroupClass">藥品群組</param>
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

        /// <summary>
        /// 新增藥品到藥品群組中
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="medGroupClass">藥品群組</param>
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
        /// <summary>
        /// 修改指定藥品群組顯示資訊
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="GUID">藥品群組GUID</param>
        /// <param name="visibleDevices">顯示設備列表</param>
        /// <returns>更新結果，包含狀態碼和結果訊息</returns>
        static public (int code, string result) update_visible_info(string API_Server, string GUID, List<string> visibleDevices)
        {
            string url = $"{API_Server}/api/medGroup/visible_set_by_guid";
            returnData returnData = new returnData();
            returnData.ValueAry = new List<string> { GUID };
            returnData.ValueAry.AddRange(visibleDevices);
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null) return (-200, "更新失敗");
            return (returnData_out.Code, returnData_out.Result);
        }
        /// <summary>
        /// 根據GUID獲取藥品群組
        /// </summary>
        /// <param name="API_Server">API伺服器地址</param>
        /// <param name="GUID">藥品群組GUID</param>
        /// <returns>藥品群組</returns>
        static public (int code, string result, medGroupClass medGroupClass) get_group_by_guid(string API_Server, string GUID)
        {
            string url = $"{API_Server}/api/medGroup/get_group_by_guid";
            returnData returnData = new returnData();
            returnData.ValueAry = new List<string> { GUID };
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null) return (-200, "取得失敗", null);
            if (returnData_out.Code != 200) return (returnData_out.Code, returnData_out.Result, null);
            List<medGroupClass> medGroupClasses = returnData_out.Data.ObjToClass<List<medGroupClass>>();
            if (medGroupClasses.Count == 0) return (returnData_out.Code, returnData_out.Result, null);
            return (returnData_out.Code, returnData_out.Result, medGroupClasses[0]); 
        }

        
    }
  
}
