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
    [EnumDescription("pharmacyDistribution")]
    public enum enum_drugstotreDistribution
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("來源庫別,VARCHAR,20,NONE")]
        來源庫別,
        [Description("目的庫別,VARCHAR,20,NONE")]
        目的庫別,
        [Description("藥碼,VARCHAR,20,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("庫存,VARCHAR,20,NONE")]
        庫存,
        [Description("撥發量,VARCHAR,20,NONE")]
        撥發量,
        [Description("實撥量,VARCHAR,20,NONE")]
        實撥量,
        [Description("撥發人員,VARCHAR,50,NONE")]
        撥發人員,
        [Description("撥發單位,VARCHAR,100,NONE")]
        撥發單位,
        [Description("加入時間,DATETIME,20,INDEX")]
        加入時間,
        [Description("撥發時間,DATETIME,20,INDEX")]
        撥發時間,
        [Description("報表生成時間,DATETIME,20,INDEX")]
        報表生成時間,
        [Description("備註,VARCHAR,300,NONE")]
        備註,

    }
    public class drugstotreDistributionClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("sourceStoreType")]
        public string 來源庫別 { get; set; }
        [JsonPropertyName("destinationStoreType")]
        public string 目的庫別 { get; set; }
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        [JsonPropertyName("name")]
        public string 藥名 { get; set; }
        [JsonPropertyName("inventory")]
        public string 庫存 { get; set; }
        [JsonPropertyName("issuedQuantity")]
        public string 撥發量 { get; set; }
        [JsonPropertyName("actualIssuedQuantity")]
        public string 實撥量 { get; set; }
        [JsonPropertyName("issuer")]
        public string 撥發人員 { get; set; }
        [JsonPropertyName("issuingUnit")]
        public string 撥發單位 { get; set; }
        [JsonPropertyName("addedTime")]
        public string 加入時間 { get; set; }
        [JsonPropertyName("issuanceTime")]
        public string 撥發時間 { get; set; }
        [JsonPropertyName("reportGenerationTime")]
        public string 報表生成時間 { get; set; }
        [JsonPropertyName("remarks")]
        public string 備註 { get; set; }
    }
}
