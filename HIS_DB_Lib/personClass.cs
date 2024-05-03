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
        藥師證字號,
        權限等級,
        顏色,
        卡號,
        一維條碼,
        識別圖案,
        指紋辨識,
        指紋ID,
        開門權限
    }
    public class personPageClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonPropertyName("name")]
        public string 姓名 { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        [JsonPropertyName("gender")]
        public string 性別 { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [JsonPropertyName("password")]
        public string 密碼 { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [JsonPropertyName("employer")]
        public string 單位 { get; set; }
        /// <summary>
        /// 藥師證字號
        /// </summary>
        [JsonPropertyName("license")]
        public string 藥師證字號 { get; set; }
        /// <summary>
        /// 權限等級
        /// </summary>
        [JsonPropertyName("level")]
        public string 權限等級 { get; set; }
        /// <summary>
        /// 顏色
        /// </summary>
        [JsonPropertyName("color")]
        public string 顏色 { get; set; }
        /// <summary>
        /// 卡號
        /// </summary>
        [JsonPropertyName("UID")]
        public string 卡號 { get; set; }
        /// <summary>
        /// 一維條碼
        /// </summary>
        [JsonPropertyName("barcode")]
        public string 一維條碼 { get; set; }
        /// <summary>
        /// 識別圖案
        /// </summary>
        [JsonPropertyName("face_image")]
        public string 識別圖案 { get; set; }
        /// <summary>
        /// 指紋辨識
        /// </summary>
        [JsonPropertyName("finger_feature")]
        public string 指紋辨識 { get; set; }
        /// <summary>
        /// 指紋ID
        /// </summary>
        [JsonPropertyName("finger_ID")]
        public string 指紋ID { get; set; }
        /// <summary>
        /// 開門權限
        /// </summary>
        [JsonPropertyName("open_access")]
        public string 開門權限 { get; set; }
    }
}
