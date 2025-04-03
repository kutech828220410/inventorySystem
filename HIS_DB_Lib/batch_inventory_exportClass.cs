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
    public enum enum_batch_inventory_export_Excel
    {
        庫別,
        藥碼,
        藥名,
        數量,
        效期,
        批號,
        收支原因,

    }

    [EnumDescription("batch_inventory_export")]
    public enum enum_batch_inventory_export
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("庫別,VARCHAR,50,INDEX")]
        庫別,
        [Description("藥碼,VARCHAR,50,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("單位,VARCHAR,50,NONE")]
        單位,
        [Description("數量,VARCHAR,50,NONE")]
        數量,
        [Description("效期,VARCHAR,50,NONE")]
        效期,
        [Description("批號,VARCHAR,50,NONE")]
        批號,
        [Description("建表人員,VARCHAR,50,INDEX")]
        建表人員,
        [Description("建表時間,DATETIME,50,INDEX")]
        建表時間,
        [Description("出庫人員,VARCHAR,50,INDEX")]
        出庫人員,
        [Description("出庫時間,DATETIME,50,INDEX")]
        出庫時間,
        [Description("收支原因,VARCHAR,200,NONE")]
        收支原因,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("備註,VARCHAR,200,NONE")]
        備註,
    }
    public class batch_inventory_exportClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 庫別
        /// </summary>
        [JsonPropertyName("STORE_NAME")]
        public string 庫別 { get; set; }
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
        /// 數量
        /// </summary>
        [JsonPropertyName("QTY")]
        public string 數量 { get; set; }
        /// <summary>
        /// 效期
        /// </summary>
        [JsonPropertyName("VAL")]
        public string 效期 { get; set; }
        /// <summary>
        /// 批號
        /// </summary>
        [JsonPropertyName("LOT")]
        public string 批號 { get; set; }
        /// <summary>
        /// 建表人員
        /// </summary>
        [JsonPropertyName("CT_OP")]
        public string 建表人員 { get; set; }
        /// <summary>
        /// 建表時間
        /// </summary>
        [JsonPropertyName("CT_TIME")]
        public string 建表時間 { get; set; }
        /// <summary>
        /// 出庫人員
        /// </summary>
        [JsonPropertyName("RECEIVER")]
        public string 出庫人員 { get; set; }
        /// <summary>
        /// 出庫時間
        /// </summary>
        [JsonPropertyName("RECEIVE_TIME")]
        public string 出庫時間 { get; set; }
        /// <summary>
        /// 收支原因
        /// </summary>
        [JsonPropertyName("RSN")]
        public string 收支原因 { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("STATUS")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("REMARKS")]
        public string 備註 { get; set; }

        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/batch_inventory_export/init";

            returnData returnData = new returnData();

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }

        static public List<batch_inventory_exportClass> get_by_CT_TIME(string API_Server, DateTime st_datetime, DateTime end_datetime)
        {
            string url = $"{API_Server}/api/batch_inventory_export/get_by_CT_TIME";

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

            return returnData_out.Data.ObjToClass<List<batch_inventory_exportClass>>();
        }
        static public List<batch_inventory_exportClass> get_by_RECEIVE_TIME(string API_Server, DateTime st_datetime, DateTime end_datetime)
        {
            string url = $"{API_Server}/api/batch_inventory_export/get_by_RECEIVE_TIME";

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

            return returnData_out.Data.ObjToClass<List<batch_inventory_exportClass>>();
        }
        static public List<batch_inventory_exportClass> update_QTY_by_GUID(string API_Server, List<batch_inventory_exportClass> batch_Inventory_exportClasses)
        {
            string url = $"{API_Server}/api/batch_inventory_export/update_QTY_by_GUID";

            returnData returnData = new returnData();
            returnData.Data = batch_Inventory_exportClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }

            return returnData_out.Data.ObjToClass<List<batch_inventory_exportClass>>();
        }
        static public List<batch_inventory_exportClass> add(string API_Server, List<batch_inventory_exportClass> batch_Inventory_exportClasses, string CT_NAME)
        {
            string url = $"{API_Server}/api/batch_inventory_export/add";

            returnData returnData = new returnData();
            returnData.Data = batch_Inventory_exportClasses;
            returnData.ValueAry.Add(CT_NAME);

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }

            return returnData_out.Data.ObjToClass<List<batch_inventory_exportClass>>();
        }
        static public List<batch_inventory_exportClass> update_state_done_by_GUID(string API_Server, List<batch_inventory_exportClass> batch_Inventory_exportClasses, string RECEIVER)
        {
            string url = $"{API_Server}/api/batch_inventory_export/update_state_done_by_GUID";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(RECEIVER);
            returnData.Data = batch_Inventory_exportClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }

            return returnData_out.Data.ObjToClass<List<batch_inventory_exportClass>>();
        }
        static public byte[] download_sample_excel(string API_Server)
        {
            string url = $"{API_Server}/api/batch_inventory_export/download_sample_excel";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            byte[] bytes = Basic.Net.WEBApiPostDownloaFile(url, json_in);
            return bytes;
        }


        static public List<batch_inventory_exportClass> excel_upload(string API_Server, byte[] bytes, string CT_NAME)
        {
            string api_url_server = sys_serverSettingClass.get_api_url(API_Server, "Main", "網頁", "batch_inventory_export_excel_upload");
            string url = $"{API_Server}/api/batch_inventory_export/excel_upload";
            if (api_url_server.StringIsEmpty() == false) url = api_url_server;
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            names.Add("CT_NAME");
            values.Add(CT_NAME);

            string json_out = Basic.Net.WEBApiPost(url, "batch_inventory_export.xlsx", bytes, names, values);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }
            List<batch_inventory_exportClass> batch_Inventory_exportClasses = returnData_out.Data.ObjToClass<List<batch_inventory_exportClass>>();
            if (batch_Inventory_exportClasses == null) return new List<batch_inventory_exportClass>();
            return batch_Inventory_exportClasses;
        }
    }
}
