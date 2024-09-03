using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Reflection;
using SQLUI;

namespace HIS_DB_Lib
{
    [EnumDescription("formulary")]
    public enum enum_formulary
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("名稱,VARCHAR,300,NONE")]
        名稱,
        [Description("批序,VARCHAR,10,NONE")]
        批序,
        [Description("藥碼,VARCHAR,20,NONE")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("數量,VARCHAR,10,NONE")]
        數量,
        [Description("單位,VARCHAR,15,NONE")]
        單位,
        [Description("中西藥,VARCHAR,15,NONE")]
        中西藥,
        [Description("新增時間,DATETIME,15,NONE")]
        新增時間,
        [Description("備註,VARCHAR,200,NONE")]
        備註,
    }
    public class formularyClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [JsonPropertyName("formulary_name")]
        public string 名稱 { get; set; }
        /// <summary>
        /// 批序
        /// </summary>
        [JsonPropertyName("index")]
        public string 批序 { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("med_name")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("qty")]
        public string 數量 { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [JsonPropertyName("package")]
        public string 單位 { get; set; }
        /// <summary>
        /// 中西藥
        /// </summary>
        [JsonPropertyName("TORW")]
        public string 中西藥 { get; set; }
        /// <summary>
        /// 新增時間
        /// </summary>
        [JsonPropertyName("add_time")]
        public string 新增時間 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("note")]
        public string 備註 { get; set; }

        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/formulary/init";

            returnData returnData = new returnData();
            string tableName = "";

            returnData.TableName = tableName;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        static public List<formularyClass> get_all(string API_Server)
        {
            string url = $"{API_Server}/api/formulary/get_all";

            returnData returnData = new returnData();


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<formularyClass> formularyClasses = returnData_out.Data.ObjToClass<List<formularyClass>>();
            return formularyClasses;
        }
        static public void add(string API_Server, formularyClass formularyClass)
        {
            List<formularyClass> formularyClasses = new List<formularyClass>();
            formularyClasses.Add(formularyClass);
            add(API_Server, formularyClass);
        }
        static public void add(string API_Server, List<formularyClass> formularyClasses)
        {
            string url = $"{API_Server}/api/formulary/add";

            returnData returnData = new returnData();
            returnData.Data = formularyClasses;


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {

            }
            if (returnData_out.Data == null)
            {

            }
            Console.WriteLine($"{returnData_out}");
            OrderTClass orderTClass = returnData_out.Data.ObjToClass<OrderTClass>();

        }
    }
}
