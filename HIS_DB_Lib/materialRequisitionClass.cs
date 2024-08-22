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
using H_Pannel_lib;
using System.Text.Json;

namespace HIS_DB_Lib
{
    [EnumDescription("materialRequisition")]
    public enum enum_materialRequisition
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("申領類別,VARCHAR,50,INDEX")]
        申領類別,
        [Description("申領單號,VARCHAR,50,INDEX")]
        申領單號,
        [Description("藥碼,VARCHAR,50,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("包裝單位,VARCHAR,50,NONE")]
        包裝單位,
        [Description("包裝量,VARCHAR,10,NONE")]
        包裝量,
        [Description("申領庫庫存,VARCHAR,10,NONE")]
        申領庫庫存,
        [Description("申領量,VARCHAR,10,NONE")]
        申領量,
        [Description("申領庫結存,VARCHAR,10,NONE")]
        申領庫結存,
        [Description("實撥庫庫存,VARCHAR,10,NONE")]
        實撥庫庫存,
        [Description("實撥量,VARCHAR,10,NONE")]
        實撥量,
        [Description("實撥庫結存,VARCHAR,10,NONE")]
        實撥庫結存,
        [Description("申領單位,VARCHAR,10,NONE")]
        申領單位,
        [Description("申領人員,VARCHAR,50,NONE")]
        申領人員,
        [Description("申領人員ID,VARCHAR,10,NONE")]
        申領人員ID,
        [Description("申領時間,DATETIME,10,INDEX")]
        申領時間,
        [Description("核撥單位,VARCHAR,10,NONE")]
        核撥單位,
        [Description("核撥人員,VARCHAR,50,NONE")]
        核撥人員,
        [Description("核撥人員ID,VARCHAR,10,NONE")]
        核撥人員ID,
        [Description("核撥時間,DATETIME,10,INDEX")]
        核撥時間,
        [Description("申領細節,VARCHAR,500,NONE")]
        申領細節,
        [Description("核撥細節,VARCHAR,500,NONE")]
        核撥細節,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("備註,VARCHAR,200,NONE")]
        備註,
    }
    public class materialRequisitionClass
    {
        /// <summary>
        /// 獨特標識符。
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 申領類別。
        /// </summary>
        [JsonPropertyName("actionType")]
        public string 申領類別 { get; set; }
        /// <summary>
        /// 申領單號。
        /// </summary>
        [JsonPropertyName("requisition_sn")]
        public string 申領單號 { get; set; }
        /// <summary>
        /// 藥品代碼。
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 料號。
        /// </summary>
        [JsonPropertyName("skdiacode")]
        public string 料號 { get; set; }
        /// <summary>
        /// 藥品名稱。
        /// </summary>
        [JsonPropertyName("name")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 中文名。
        /// </summary>
        [JsonPropertyName("cht_name")]
        public string 中文名 { get; set; }
        /// <summary>
        /// 包裝單位。
        /// </summary>
        [JsonPropertyName("packageUnit")]
        public string 包裝單位 { get; set; }
        /// <summary>
        /// 包裝量。
        /// </summary>
        [JsonPropertyName("packageQuantity")]
        public string 包裝量 { get; set; }
        /// <summary>
        /// 申領庫庫存。
        /// </summary>
        [JsonPropertyName("requestStoreInventory")]
        public string 申領庫庫存 { get; set; }
        /// <summary>
        /// 申領量。
        /// </summary>
        [JsonPropertyName("requestedQuantity")]
        public string 申領量 { get; set; }
        /// <summary>
        /// 申領庫結存。
        /// </summary>
        [JsonPropertyName("requestStoreBalance")]
        public string 申領庫結存 { get; set; }
        /// <summary>
        /// 實撥庫庫存。
        /// </summary>
        [JsonPropertyName("actualStoreInventory")]
        public string 實撥庫庫存 { get; set; }
        /// <summary>
        /// 實撥量。
        /// </summary>
        [JsonPropertyName("actualQuantity")]
        public string 實撥量 { get; set; }
        /// <summary>
        /// 實撥庫結存。
        /// </summary>
        [JsonPropertyName("actualStoreBalance")]
        public string 實撥庫結存 { get; set; }
        /// <summary>
        /// 申領單位。
        /// </summary>
        [JsonPropertyName("requestingUnit")]
        public string 申領單位 { get; set; }
        /// <summary>
        /// 申領人員。
        /// </summary>
        [JsonPropertyName("requestingPerson")]
        public string 申領人員 { get; set; }
        /// <summary>
        /// 申領人員ID。
        /// </summary>
        [JsonPropertyName("requestingPersonID")]
        public string 申領人員ID { get; set; }
        /// <summary>
        /// 申領時間。
        /// </summary>
        [JsonPropertyName("requestTime")]
        public string 申領時間 { get; set; }
        /// <summary>
        /// 核撥單位。
        /// </summary>
        [JsonPropertyName("issuingUnit")]
        public string 核撥單位 { get; set; }
        /// <summary>
        /// 核撥人員。
        /// </summary>
        [JsonPropertyName("issuingPerson")]
        public string 核撥人員 { get; set; }
        /// <summary>
        /// 核撥人員ID。
        /// </summary>
        [JsonPropertyName("issuingPersonID")]
        public string 核撥人員ID { get; set; }
        /// <summary>
        /// 核撥時間。
        /// </summary>
        [JsonPropertyName("issueTime")]
        public string 核撥時間 { get; set; }
        /// <summary>
        /// 申領細節。
        /// </summary>
        [JsonPropertyName("requestDetails")]
        public string 申領細節 { get; set; }
        /// <summary>
        /// 核撥細節。
        /// </summary>
        [JsonPropertyName("issueDetails")]
        public string 核撥細節 { get; set; }
        /// <summary>
        /// 狀態。
        /// </summary>
        [JsonPropertyName("state")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 備註。
        /// </summary>
        [JsonPropertyName("remarks")]
        public string 備註 { get; set; }


        [JsonIgnore]
        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            // PropertyNameCaseInsensitive = true,
        };

        /// <summary>
        /// 申領細節列表。
        /// </summary>
        [JsonPropertyName("requestDetailsList")]
        public List<StockClass> requestDetailsList
        {
            get
            {
                if (申領細節.StringIsEmpty()) return new List<StockClass>();
                return JsonSerializer.Deserialize<List<StockClass>>(申領細節, jsonSerializerOptions);
            }
            set
            {
                申領細節 = JsonSerializer.Serialize(value, jsonSerializerOptions);
            }
        }
        /// <summary>
        /// 核撥細節列表。
        /// </summary>
        [JsonPropertyName("issueDetailsList")]
        public List<StockClass> issueDetailsList
        {
            get
            {
                if (核撥細節.StringIsEmpty()) return new List<StockClass>();
                return JsonSerializer.Deserialize<List<StockClass>>(核撥細節, jsonSerializerOptions);
            }
            set
            {
                核撥細節 = JsonSerializer.Serialize(value, jsonSerializerOptions);
            }
        }

        /// <summary>
        /// 初始化數據表。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <returns>初始化後的數據表。</returns>
        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/materialRequisition/init";

            returnData returnData = new returnData();
            returnData.ServerName = "Main";
            returnData.ServerType = "網頁";

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        /// <summary>
        /// 添加申領單。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="materialRequisitions">申領單列表。</param>
        static public void add(string API_Server, List<materialRequisitionClass> materialRequisitions)
        {
            string url = $"{API_Server}/api/materialRequisition/add";

            returnData returnData = new returnData();
            returnData.ServerName = "";
            returnData.ServerType = "";
            returnData.Data = materialRequisitions;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Data == null || returnData_out.Code != 200)
            {
                return;
            }

            Console.WriteLine($"[{returnData_out.Method}]:{returnData_out.Result}");
        }
        /// <summary>
        /// 根據 GUID 更新申領單。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="materialRequisitions">申領單列表。</param>
        static public void update_by_guid(string API_Server, List<materialRequisitionClass> materialRequisitions)
        {
            string url = $"{API_Server}/api/materialRequisition/update_by_guid";

            returnData returnData = new returnData();
            returnData.ServerName = "";
            returnData.ServerType = "";
            returnData.Data = materialRequisitions;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Data == null || returnData_out.Code != 200)
            {
                return;
            }

            Console.WriteLine($"[{returnData_out.Method}]:{returnData_out.Result}");
        }
        /// <summary>
        /// 根據申領時間獲取申領單。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="st_datetime">開始時間。</param>
        /// <param name="end_datetime">結束時間。</param>
        /// <returns>申領單列表。</returns>
        static public List<materialRequisitionClass> get_by_requestTime(string API_Server, DateTime st_datetime, DateTime end_datetime)
        {
            string url = $"{API_Server}/api/materialRequisition/get_by_requestTime";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(st_datetime.ToString("yyyy-MM-ddTHH:mm:ss"));
            returnData.ValueAry.Add(end_datetime.ToString("yyyy-MM-ddTHH:mm:ss"));

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }

            return returnData_out.Data.ObjToClass<List<materialRequisitionClass>>();
        }
        /// <summary>
        /// 根據 GUID 獲取申領單。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="GUID">申領單 GUID。</param>
        /// <returns>申領單。</returns>
        static public materialRequisitionClass get_by_guid(string API_Server, string GUID)
        {
            string url = $"{API_Server}/api/materialRequisition/get_by_guid";
            returnData returnData = new returnData();
            returnData.ServerName = "";
            returnData.ServerType = "";
            returnData.ValueAry.Add(GUID);
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Data == null || returnData_out.Code != 200)
            {
                return null;
            }

            return returnData_out.Data.ObjToClass<materialRequisitionClass>();
        }
        /// <summary>
        /// 修改實撥量。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="materialRequisition">materialRequisitionClass實例。</param>
        static public void update_actual_quantity(string API_Server, materialRequisitionClass materialRequisition)
        {
            update_actual_quantity(API_Server, materialRequisition.GUID, materialRequisition.實撥量);
        }
        /// <summary>
        /// 修改實撥量。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="guid">資料的 GUID。</param>
        /// <param name="newActualQuantity">新的實撥量。</param>
        static public void update_actual_quantity(string API_Server, string guid, string newActualQuantity)
        {
            string url = $"{API_Server}/api/materialRequisition/update_actual_quantity";

            var data = new
            {
                GUID = guid,
                actualQuantity = newActualQuantity
            };

            returnData returnData = new returnData();
            returnData.Data = data;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Data == null || returnData_out.Code != 200)
            {
                return;
            }

            Console.WriteLine($"[{returnData_out.Method}]:{returnData_out.Result}");
        }
        /// <summary>
        /// 修改狀態為等待過帳。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="materialRequisition">materialRequisitionClass實例。</param>
        static public void update_status_pending(string API_Server, materialRequisitionClass materialRequisition)
        {
            update_status_pending(API_Server, materialRequisition.GUID);
        }
        /// <summary>
        /// 修改狀態為等待過帳。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="guid">資料的 GUID。</param>
        static public void update_status_pending(string API_Server, string guid)
        {
            string url = $"{API_Server}/api/materialRequisition/update_status_pending";

            var data = new
            {
                GUID = guid
            };

            returnData returnData = new returnData();
            returnData.Data = data;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Data == null || returnData_out.Code != 200)
            {
                return;
            }

            Console.WriteLine($"[{returnData_out.Method}]:{returnData_out.Result}");
        }
        /// <summary>
        /// 修改狀態為已過帳。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="materialRequisition">materialRequisitionClass實例。</param>
        static public void update_status_posted(string API_Server, materialRequisitionClass materialRequisition)
        {
            update_status_posted(API_Server, materialRequisition.GUID);
        }
        /// <summary>
        /// 修改狀態為已過帳。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="guid">資料的 GUID。</param>
        static public void update_status_posted(string API_Server, string guid)
        {
            string url = $"{API_Server}/api/materialRequisition/update_status_posted";

            var data = new
            {
                GUID = guid
            };

            returnData returnData = new returnData();
            returnData.Data = data;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Data == null || returnData_out.Code != 200)
            {
                return;
            }

            Console.WriteLine($"[{returnData_out.Method}]:{returnData_out.Result}");
        }



        /// <summary>
        /// 比較藥碼的比較器。
        /// </summary>
        public class ICP_By_Code : IComparer<materialRequisitionClass>
        {
            /// <summary>
            /// 比較兩個申領單的藥碼。
            /// </summary>
            /// <param name="x">申領單 x。</param>
            /// <param name="y">申領單 y。</param>
            /// <returns>比較結果。</returns>
            public int Compare(materialRequisitionClass x, materialRequisitionClass y)
            {
                return x.藥碼.CompareTo(y.藥碼);
            }
        }
        /// <summary>
        /// 比較申領時間的比較器。
        /// </summary>
        public class ICP_By_requestTime : IComparer<materialRequisitionClass>
        {
            /// <summary>
            /// 比較兩個申領單的申領時間。
            /// </summary>
            /// <param name="x">申領單 x。</param>
            /// <param name="y">申領單 y。</param>
            /// <returns>比較結果。</returns>
            public int Compare(materialRequisitionClass x, materialRequisitionClass y)
            {
                return x.申領時間.CompareTo(y.申領時間);
            }
        }
    }
}
