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
    public enum enum_batch_inventory_import_Excel
    {
        庫別,
        藥碼,
        藥名,
        數量,
        效期,
        批號,
        收支原因,

    }

    [EnumDescription("batch_inventory_import")]
    public enum enum_batch_inventory_import
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
        [Description("入庫人員,VARCHAR,50,INDEX")]
        入庫人員,
        [Description("入庫時間,DATETIME,50,INDEX")]
        入庫時間,
        [Description("收支原因,DATETIME,200,NONE")]
        收支原因,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
    }
    public class batch_inventory_importClass
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
        /// 出庫人員
        /// </summary>
        [JsonPropertyName("QTY")]
        public string 數量 { get; set; }
        /// <summary>
        /// 入庫人員
        /// </summary>
        [JsonPropertyName("VAL")]
        public string 效期 { get; set; }
        /// <summary>
        /// 出庫庫別
        /// </summary>
        [JsonPropertyName("LOT")]
        public string 批號 { get; set; }
        /// <summary>
        /// 入庫庫別
        /// </summary>
        [JsonPropertyName("CT_OP")]
        public string 建表人員 { get; set; }
        /// <summary>
        /// 出庫庫存
        /// </summary>
        [JsonPropertyName("CT_TIME")]
        public string 建表時間 { get; set; }
        /// <summary>
        /// 入庫人員
        /// </summary>
        [JsonPropertyName("RECEIVER")]
        public string 入庫人員 { get; set; }
        /// <summary>
        /// 入庫時間
        /// </summary>
        [JsonPropertyName("RECEIVE_TIME")]
        public string 入庫時間 { get; set; }
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
            string url = $"{API_Server}/api/batch_inventory_import/init";

            returnData returnData = new returnData();

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }


    }
}
