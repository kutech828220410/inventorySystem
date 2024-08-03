using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.Text.Json;
using H_Pannel_lib;
namespace HIS_DB_Lib
{
    public class storagelistClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("storage_name")]
        public string 儲位名稱 { get; set; }
        [JsonPropertyName("Code")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("Neme")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("package")]
        public string 單位 { get; set; }
        [JsonPropertyName("validity_date")]
        public string 效期 { get; set; }
        [JsonPropertyName("inventory")]
        public string 庫存 { get; set; }
        [JsonPropertyName("storage_type")]
        public string 儲位型式 { get; set; }
    }
}
