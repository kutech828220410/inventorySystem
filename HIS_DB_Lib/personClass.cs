using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Basic;
namespace HIS_DB_Lib
{
    public enum enum_人員資料
    {
        GUID,
        ID,
        姓名,
        性別,
        密碼,
        單位,
        權限等級,
        顏色,
        卡號,
        一維條碼,
        識別圖案,
        指紋辨識,
        指紋ID,
    }
    public class personPageClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("name")]
        public string 姓名 { get; set; }
        [JsonPropertyName("gender")]
        public string 性別 { get; set; }
        [JsonPropertyName("password")]
        public string 密碼 { get; set; }
        [JsonPropertyName("employer")]
        public string 單位 { get; set; }
        [JsonPropertyName("level")]
        public string 權限等級 { get; set; }
        [JsonPropertyName("color")]
        public string 顏色 { get; set; }
        [JsonPropertyName("UID")]
        public string 卡號 { get; set; }
        [JsonPropertyName("barcode")]
        public string 一維條碼 { get; set; }
        [JsonPropertyName("face_image")]
        public string 識別圖案 { get; set; }
        [JsonPropertyName("finger_feature")]
        public string 指紋辨識 { get; set; }
        [JsonPropertyName("finger_ID")]
        public string 指紋ID { get; set; }
    }
}
