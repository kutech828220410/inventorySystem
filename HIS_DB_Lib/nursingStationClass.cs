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
    [EnumDescription("nursingStation")]
    public enum enum_nursingStation
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("排序,VARCHAR,20,NONE")]
        排序,
        [Description("代碼,VARCHAR,30,INDEX")]
        代碼,
        [Description("名稱,VARCHAR,200,NONE")]
        名稱,
    }
    /// <summary>
    /// 護理站資料模型
    /// </summary>
    public class nursingStationClass
    {
        /// <summary>
        /// 護理站唯一識別碼
        /// </summary>
        [JsonPropertyName("guid")]
        public string GUID { get; set; }
        /// <summary>
        /// 護理站代碼
        /// </summary>
        [JsonPropertyName("sortOrder")]
        public string 排序 { get; set; }
        /// <summary>
        /// 護理站代碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 代碼 { get; set; }
        /// <summary>
        /// 護理站名稱
        /// </summary>
        [JsonPropertyName("name")]
        public string 名稱 { get; set; }


        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/nursingStation/init";

            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            List<SQLUI.Table> tables = json_out.JsonDeserializet<List<SQLUI.Table>>();
            SQLUI.Table table = SQLUI.TableMethod.GetTable(tables, new enum_nursingStation());
            return table;
        }
        static public List<nursingStationClass> get_all(string API_Server)
        {
            var (code, result, list) = get_all_full(API_Server);
            return list;
        }
        static public (int code, string result, List<nursingStationClass>) get_all_full(string API_Server)
        {
            string url = $"{API_Server}/api/nursingStation/get_all";

            returnData returnData = new returnData();


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }
            Console.WriteLine($"{returnData_out}");
            List<nursingStationClass> nursingStationClasses = returnData_out.Data.ObjToClass<List<nursingStationClass>>();
            return (returnData_out.Code, returnData_out.Result, nursingStationClasses);
        }


        static public List<nursingStationClass> add(string API_Server, nursingStationClass nursingStationClass)
        {
            var (code, result, list) = add_full(API_Server, new List<nursingStationClass> { nursingStationClass });
            return list;
        }
        static public List<nursingStationClass> add(string API_Server, List<nursingStationClass> nursingStationClasses)
        {
            var (code, result, list) = add_full(API_Server, nursingStationClasses);
            return list;
        }
        static public (int code, string result, List<nursingStationClass>) add_full(string API_Server, List<nursingStationClass> nursingStationClasses)
        {
            string url = $"{API_Server}/api/nursingStation/add";

            returnData returnData = new returnData();
            returnData.Data = nursingStationClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }
            Console.WriteLine($"{returnData_out}");
            nursingStationClasses = returnData_out.Data.ObjToClass<List<nursingStationClass>>();
            return (returnData_out.Code, returnData_out.Result, nursingStationClasses);
        }

        static public List<nursingStationClass> delete_by_code(string API_Server, string code)
        {
            List<string> codes = new List<string>();
            codes.Add(code);
            var (_code, result, list) = delete_by_codes_full(API_Server, codes);
            return list;
        }
        static public List<nursingStationClass> delete_by_codes(string API_Server, List<string> codes)
        {
            var (code, result, list) = delete_by_codes_full(API_Server, codes);
            return list;
        }
        static public (int code, string result, List<nursingStationClass>) delete_by_codes_full(string API_Server, List<string> codes)
        {
            string url = $"{API_Server}/api/nursingStation/delete_by_codes";
            string sqlList = string.Join(", ", codes.Select(code => $"{code}"));
            returnData returnData = new returnData();
            returnData.ValueAry.Add(sqlList);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }
            Console.WriteLine($"{returnData_out}");
            List<nursingStationClass> nursingStationClasses = returnData_out.Data.ObjToClass<List<nursingStationClass>>();
            return (returnData_out.Code, returnData_out.Result, nursingStationClasses);
        }
    }
    public static class nursingStationClassMethod
    {

        /// <summary>
        /// 依照 SortOrder 排序護理站列表
        /// </summary>
        /// <param name="stations">未排序的護理站列表</param>
        /// <returns>已排序的護理站列表</returns>
        public static List<nursingStationClass> SortByOrder(this List<nursingStationClass> stations)
        {
            return stations
                .OrderBy(s => s.排序.StringToInt32())
                .ThenBy(s => s.名稱) // 若排序值相同，再以名稱排序
                .ToList();
        }
    }
}
