using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
namespace HIS_DB_Lib
{
    public enum enum_合併總單
    {
        GUID,
        合併單名稱,
        合併單號,
        建表人,
        建表時間,
        備註,
    }
    public enum enum_合併單明細
    {
        GUID,
        Master_GUID,
        合併單號,
        單號,
        類型,
        新增時間,
        備註,
    }
    /// <summary>
    /// 合併總單
    /// </summary>
    public class inv_combinelistClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單名稱
        /// </summary>
        [JsonPropertyName("INV_NAME")]
        public string 合併單名稱 { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("INV_SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 建表人
        /// </summary>
        [JsonPropertyName("CT")]
        public string 建表人 { get; set; }
        /// <summary>
        /// 建表時間
        /// </summary>
        [JsonPropertyName("CT_TIME")]
        public string 建表時間 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }

        /// <summary>
        /// 合併單明細
        /// </summary>
        [JsonPropertyName("records_Ary")]
        public List<inv_sub_combinelistClass> Records_Ary { get => records_Ary; set => records_Ary = value; }
        private List<inv_sub_combinelistClass> records_Ary = new List<inv_sub_combinelistClass>();
    }
    /// <summary>
    /// 合併單明細
    /// </summary>
    public class inv_sub_combinelistClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// REF_GUID
        /// </summary>
        [JsonPropertyName("REF_GUID")]
        public string REF_GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("INV_SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 單號 { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        [JsonPropertyName("TYPE")]
        public string 類型 { get; set; }
        /// <summary>
        /// 新增時間
        /// </summary>
        [JsonPropertyName("ADD_TIME")]
        public string 新增時間 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }
    }
    public class inv_records_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 單號 { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        [JsonPropertyName("TYPE")]
        public string 類型 { get; set; }
    }
}
