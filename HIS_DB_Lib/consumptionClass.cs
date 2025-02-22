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
    [EnumDescription("consumption")]
    public enum enum_consumption
    {
        [Description("藥碼,VARCHAR,20,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("類別,VARCHAR,20,NONE")]
        類別,
        [Description("消耗量,VARCHAR,20,NONE")]
        消耗量,
        [Description("結存量,VARCHAR,20,NONE")]
        結存量,
        [Description("庫存量,VARCHAR,20,NONE")]
        庫存量,
    }

    public class consumptionClass
    {
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        [JsonPropertyName("TYPE")]
        public string 類別 { get; set; }
        [JsonPropertyName("TXN_QTY")]
        public string 消耗量 { get; set; }
        [JsonPropertyName("EBQ_QTY")]
        public string 結存量 { get; set; }
        [JsonPropertyName("PXC_QTY")]
        public string 實調量 { get; set; }
        [JsonPropertyName("INV_QTY")]
        public string 庫存量 { get; set; }
    }
}
