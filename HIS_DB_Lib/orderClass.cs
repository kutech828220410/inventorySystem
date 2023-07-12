using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_醫囑資料_狀態
    {
        未過帳,
        已過帳,
        庫存不足,
        無儲位,
    }
    public enum enum_醫囑資料
    {
        GUID,
        PRI_KEY,
        藥局代碼,
        藥袋條碼,
        藥品碼,
        藥品名稱,
        病人姓名,
        病歷號,
        交易量,
        開方日期,
        產出時間,
        過帳時間,
        狀態,
    }
    public class OrderClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("PRI_KEY")]
        public string PRI_KEY { get; set; }
        [JsonPropertyName("PHARM_CODE")]
        public string 藥局代碼 { get; set; }
        [JsonPropertyName("MED_BAG_SN")]
        public string 藥袋條碼 { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("PAT")]
        public string 病人姓名 { get; set; }
        [JsonPropertyName("MRN")]
        public string 病歷號 { get; set; }
        [JsonPropertyName("TXN_QTY")]
        public string 交易量 { get; set; }
        [JsonPropertyName("RX_TIME")]
        public string 開方日期 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 產出時間 { get; set; }
        [JsonPropertyName("POST_TIME")]
        public string 過帳時間 { get; set; }
        [JsonPropertyName("STATE")]
        public string 狀態 { get; set; }
      
    }
}
