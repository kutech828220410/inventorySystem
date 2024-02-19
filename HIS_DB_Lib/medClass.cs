using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.Text.Json;
namespace HIS_DB_Lib
{
    public enum enum_藥品管制方式設定
    {
        GUID,
        代號,
        效期管理,
        盲盤,
        複盤,
        結存報表,
        雙人覆核,
    }
  
    public class medClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("SKDIACODE")]
        public string 料號 { get; set; }
        [JsonPropertyName("CHT_NAME")]
        public string 中文名稱 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("DIANAME")]
        public string 藥品學名 { get; set; }
        [JsonPropertyName("GROUP")]
        public string 藥品群組 { get; set; }
        [JsonPropertyName("HI_CODE")]
        public string 健保碼 { get; set; }
        [JsonPropertyName("PAKAGE")]
        public string 包裝單位 { get; set; }
        [JsonPropertyName("PAKAGE_VAL")]
        public string 包裝數量 { get; set; }
        [JsonPropertyName("MIN_PAKAGE")]
        public string 最小包裝單位 { get; set; }
        [JsonPropertyName("MIN_PAKAGE_VAL")]
        public string 最小包裝數量 { get; set; }
        [JsonPropertyName("BARCODE1")]
        public string 藥品條碼1 { get; set; }
        [JsonPropertyName("BARCODE2")]
        public string 藥品條碼2 { get; set; }
        [JsonPropertyName("IS_WARRING")]
        public string 警訊藥品 { get; set; }   
        [JsonPropertyName("IS_H_COST")]
        public string 高價藥品 { get; set; }
        [JsonPropertyName("IS_BIO")]
        public string 生物製劑 { get; set; }
        [JsonPropertyName("DRUG_QTY")]
        public string 藥局庫存 { get; set; }
        [JsonPropertyName("PHAR_QTY")]
        public string 藥庫庫存 { get; set; }
        [JsonPropertyName("DRUGKIND")]
        public string 管制級別 { get; set; }
        [JsonPropertyName("TOLTAL_QTY")]
        public string 總庫存 { get; set; }
        [JsonPropertyName("QTY")]
        public string 庫存 { get; set; }
        [JsonPropertyName("REF_QTY")]
        public string 基準量 { get; set; }
        [JsonPropertyName("SAFE_QTY")]
        public string 安全庫存 { get; set; }
        [JsonPropertyName("BRD")]
        public string 廠牌 { get; set; }
        [JsonPropertyName("LICENSE")]
        public string 藥品許可證號 { get; set; }
        [JsonPropertyName("FILE_STATUS")]
        public string 開檔狀態 { get; set; }

        //[JsonPropertyName("BARCODE")]
        //public string Barcode_Json
        //{
        //    get
        //    {
        //        return 藥品條碼2;
        //    }
        //    set
        //    {
        //        藥品條碼2 = value;
        //    }
        //}
        [JsonIgnore]
        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
          // PropertyNameCaseInsensitive = true,
        };

        [JsonPropertyName("BARCODE")]
        public List<string> Barcode
        {
            get
            {
                if (藥品條碼2.StringIsEmpty()) return new List<string>(); ;
                List<string> temp = JsonSerializer.Deserialize<List<string>>(藥品條碼2, jsonSerializerOptions);
    

                List<string> temp_buf = new List<string>();
                for (int i = 0; i < temp.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(temp[i]))
                    {
                        temp_buf.Add(temp[i]);
                    }
                }
                return temp_buf;
            }
            set
            {
                藥品條碼2 = JsonSerializer.Serialize(value, jsonSerializerOptions);
            }
        }
        public void Add_BarCode(string barcode)
        {
            List<string> barcodes = 藥品條碼2.JsonDeserializet<List<string>>();
            if (barcodes == null) barcodes = new List<string>();
            barcodes.Add(barcode);
            Barcode = barcodes;
        }
        public void Delete_BarCode(string barcode)
        {
            List<string> barcodes = 藥品條碼2.JsonDeserializet<List<string>>();
            if (barcodes == null) barcodes = new List<string>();
            barcodes.Remove(barcode);
            Barcode = barcodes;
        }

    }
}
