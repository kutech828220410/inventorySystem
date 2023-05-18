using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_雲端藥檔
    {
        GUID,
        藥品碼,
        料號,
        中文名稱,
        藥品名稱,
        藥品學名,
        健保碼,
        包裝單位,
        包裝數量,
        最小包裝單位,
        最小包裝數量,
        藥品條碼1,
        藥品條碼2,
        警訊藥品,
        管制級別,
        類別,
    }
   
    public class medCouldClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
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
        [JsonPropertyName("DRUGKIND")]
        public string 管制級別 { get; set; }

        static public object[] ClassToSQL(medCouldClass _class)
        {
            object[] value = new object[new enum_盤點單號().GetLength()];
            value[(int)enum_雲端藥檔.GUID] = _class.GUID;
            value[(int)enum_雲端藥檔.藥品碼] = _class.藥品碼;
            value[(int)enum_雲端藥檔.料號] = _class.料號;
            value[(int)enum_雲端藥檔.中文名稱] = _class.中文名稱;
            value[(int)enum_雲端藥檔.藥品名稱] = _class.藥品名稱;
            value[(int)enum_雲端藥檔.藥品學名] = _class.藥品學名;
            value[(int)enum_雲端藥檔.健保碼] = _class.健保碼;
            value[(int)enum_雲端藥檔.包裝單位] = _class.包裝單位;
            value[(int)enum_雲端藥檔.包裝數量] = _class.包裝數量;
            value[(int)enum_雲端藥檔.最小包裝單位] = _class.最小包裝單位;
            value[(int)enum_雲端藥檔.最小包裝數量] = _class.最小包裝數量;
            value[(int)enum_雲端藥檔.藥品條碼1] = _class.藥品條碼1;
            value[(int)enum_雲端藥檔.藥品條碼2] = _class.藥品條碼2;
            value[(int)enum_雲端藥檔.警訊藥品] = _class.警訊藥品;
            value[(int)enum_雲端藥檔.管制級別] = _class.管制級別;

            return value;
        }
        static public medCouldClass SQLToClass(object[] value)
        {
            medCouldClass _class = new medCouldClass();
            _class.GUID = value[(int)enum_雲端藥檔.GUID].ObjectToString();
            _class.藥品碼 = value[(int)enum_雲端藥檔.藥品碼].ObjectToString();
            _class.料號 = value[(int)enum_雲端藥檔.料號].ObjectToString();
            _class.中文名稱 = value[(int)enum_雲端藥檔.中文名稱].ToDateTimeString();
            _class.藥品名稱 = value[(int)enum_雲端藥檔.藥品名稱].ToDateTimeString();
            _class.藥品學名 = value[(int)enum_雲端藥檔.藥品學名].ToDateTimeString();
            _class.健保碼 = value[(int)enum_雲端藥檔.健保碼].ObjectToString();
            _class.包裝單位 = value[(int)enum_雲端藥檔.包裝單位].ObjectToString();
            _class.包裝數量 = value[(int)enum_雲端藥檔.包裝數量].ObjectToString();
            _class.最小包裝單位 = value[(int)enum_雲端藥檔.最小包裝單位].ToDateTimeString();
            _class.最小包裝數量 = value[(int)enum_雲端藥檔.最小包裝數量].ToDateTimeString();
            _class.藥品條碼1 = value[(int)enum_雲端藥檔.藥品條碼1].ToDateTimeString();
            _class.藥品條碼2 = value[(int)enum_雲端藥檔.藥品條碼2].ObjectToString();
            _class.警訊藥品 = value[(int)enum_雲端藥檔.警訊藥品].ObjectToString();
            _class.管制級別 = value[(int)enum_雲端藥檔.管制級別].ObjectToString();
            return _class;
        }
        static public medCouldClass ObjToClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<medCouldClass>();
        }


    }
}
