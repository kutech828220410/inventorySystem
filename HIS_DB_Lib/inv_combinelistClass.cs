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
    public class inv_combinelistClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("INV_NAME")]
        public string 合併單名稱 { get; set; }
        [JsonPropertyName("INV_SN")]
        public string 合併單號 { get; set; }
        [JsonPropertyName("CT")]
        public string 建表人 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 建表時間 { get; set; }
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }

        [JsonPropertyName("records_Ary")]
        public List<inv_sub_combinelistClass> Records_Ary { get => records_Ary; set => records_Ary = value; }
        private List<inv_sub_combinelistClass> records_Ary = new List<inv_sub_combinelistClass>();
    }
    public class inv_sub_combinelistClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        [JsonPropertyName("REF_GUID")]
        public string REF_GUID { get; set; }
        [JsonPropertyName("INV_SN")]
        public string 合併單號 { get; set; }
        [JsonPropertyName("SN")]
        public string 單號 { get; set; }
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        [JsonPropertyName("TYPE")]
        public string 類型 { get; set; }
        [JsonPropertyName("ADD_TIME")]
        public string 新增時間 { get; set; }
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }
    }
    public class inv_records_Class
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("SN")]
        public string 單號 { get; set; }
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        [JsonPropertyName("TYPE")]
        public string 類型 { get; set; }
    }
}
