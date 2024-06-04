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
    [EnumDescription("drugDispatch")]
    public enum enum_drugDispatch
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("藥碼,VARCHAR,50,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,50,NONE")]
        藥名,
        [Description("單位,VARCHAR,50,NONE")]
        單位,
        [Description("調出人員,VARCHAR,50,INDEX")]
        調出人員,
        [Description("調入人員,VARCHAR,50,INDEX")]
        調入人員,
        [Description("調出庫別,VARCHAR,15,NONE")]
        調出庫別,
        [Description("調入庫別,VARCHAR,15,NONE")]
        調入庫別,
        [Description("調出庫存,VARCHAR,15,NONE")]
        調出庫存,
        [Description("調出結存,VARCHAR,15,NONE")]
        調出結存,
        [Description("調出量,VARCHAR,15,NONE")]
        調出量,
        [Description("調入庫存,VARCHAR,15,NONE")]
        調入庫存,
        [Description("調入結存,VARCHAR,15,NONE")]
        調入結存,
        [Description("產出時間,DATETIME,50,INDEX")]
        產出時間,
        [Description("過帳時間,DATETIME,50,INDEX")]
        過帳時間,
        [Description("收支原因,VARCHAR,400,NONE")]
        收支原因,
        [Description("狀態,VARCHAR,30,NONE")]
        狀態,
        [Description("備註,VARCHAR,400,NONE")]
        備註,
    }
    public class drugDispatchClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [JsonPropertyName("PAKAGE")]
        public string 單位 { get; set; }
        /// <summary>
        /// 調出人員
        /// </summary>
        [JsonPropertyName("DISPATCHER")]
        public string 調出人員 { get; set; }
        /// <summary>
        /// 調入人員
        /// </summary>
        [JsonPropertyName("RECEIVER")]
        public string 調入人員 { get; set; }
        /// <summary>
        /// 調出庫別
        /// </summary>
        [JsonPropertyName("SOURCE_STORE")]
        public string 調出庫別 { get; set; }
        /// <summary>
        /// 調入庫別
        /// </summary>
        [JsonPropertyName("DESTINATION_STORE")]
        public string 調入庫別 { get; set; }
        /// <summary>
        /// 調出庫存
        /// </summary>
        [JsonPropertyName("SOURCE_INVENTORY")]
        public string 調出庫存 { get; set; }
        /// <summary>
        /// 調出結存
        /// </summary>
        [JsonPropertyName("SOURCE_END_INV")]
        public string 調出結存 { get; set; }
        /// <summary>
        /// 調出量
        /// </summary>
        [JsonPropertyName("DISPATCHED_QTY")]
        public string 調出量 { get; set; }
        /// <summary>
        /// 調入庫存
        /// </summary>
        [JsonPropertyName("DESTINATION_INVENTORY")]
        public string 調入庫存 { get; set; }
        /// <summary>
        /// 調入結存
        /// </summary>
        [JsonPropertyName("DESTINATION_END_INV")]
        public string 調入結存 { get; set; }
        /// <summary>
        /// 產出時間
        /// </summary>
        [JsonPropertyName("PRODUCTION_TIME")]
        public string 產出時間 { get; set; }
        /// <summary>
        /// 過帳時間
        /// </summary>
        [JsonPropertyName("POSTING_TIME")]
        public string 過帳時間 { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("STATUS")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 收支原因
        /// </summary>
        [JsonPropertyName("RSN")]
        public string 收支原因 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("REMARKS")]
        public string 備註 { get; set; }

        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/drugDispatch/init";

            returnData returnData = new returnData();

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        static public void add(string API_Server, List<drugDispatchClass> drugDispatchClasses)
        {
            string url = $"{API_Server}/api/drugDispatch/add";
            string str_serverNames = "";
            string str_serverTypes = "";

            returnData returnData = new returnData();
            returnData.ServerName = "";
            returnData.ServerType = "";
            returnData.Data = drugDispatchClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null)
            {
                return;
            }
            if (returnData_out.Data == null)
            {
                return;
            }
            if (returnData_out.Code != 200) return;
            personPageClass personPageClass = returnData_out.Data.ObjToClass<personPageClass>();

            Console.WriteLine($"[{returnData_out.Method}]:{returnData_out.Result}");
            return;
        }
    }
}
