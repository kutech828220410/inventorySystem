using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
namespace HIS_DB_Lib
{
    public enum enum_盤點單號
    {
        GUID,
        盤點名稱,
        盤點單號,
        建表人,
        建表時間,
        盤點開始時間,
        盤點結束時間,
        盤點狀態
    }
    public enum enum_盤點內容
    {
        GUID,
        Master_GUID,
        盤點單號,
        藥品碼,
        料號,
        藥品條碼1,
        藥品條碼2,
        理論值,
        新增時間,
    }
    public enum enum_盤點明細
    {
        GUID,
        Master_GUID,
        盤點單號,
        藥品碼,
        料號,
        藥品條碼1,
        藥品條碼2,
        盤點量,
        效期,
        批號,
        操作時間,
        操作人,
        狀態,
    }
    public class inventoryClass
    {
        public class creat
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("IC_NAME")]
            public string 盤點名稱 { get; set; }
            [JsonPropertyName("IC_SN")]
            public string 盤點單號 { get; set; }
            [JsonPropertyName("CT")]
            public string 建表人 { get; set; }
            [JsonPropertyName("CT_TIME")]
            public string 建表時間 { get; set; }
            [JsonPropertyName("START_TIME")]
            public string 盤點開始時間 { get; set; }
            [JsonPropertyName("END_TIME")]
            public string 盤點結束時間 { get; set; }
            [JsonPropertyName("STATE")]
            public string 盤點狀態 { get; set; }

            private List<content> _contents = new List<content>();
            public List<content> Contents { get => _contents; set => _contents = value; }

            static public object[] ClassToSQL(creat _class)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
                value[(int)enum_盤點單號.GUID] = _class.GUID;
                value[(int)enum_盤點單號.盤點單號] = _class.盤點單號;
                value[(int)enum_盤點單號.盤點名稱] = _class.盤點名稱;
                value[(int)enum_盤點單號.建表人] = _class.建表人;
                value[(int)enum_盤點單號.建表時間] = _class.建表時間;
                value[(int)enum_盤點單號.盤點開始時間] = _class.盤點開始時間;
                value[(int)enum_盤點單號.盤點結束時間] = _class.盤點結束時間;
                value[(int)enum_盤點單號.盤點狀態] = _class.盤點狀態;

                return value;
            }
            static public creat SQLToClass(object[] value)
            {
                creat _class = new creat();
                _class.GUID = value[(int)enum_盤點單號.GUID].ObjectToString();
                _class.盤點單號 = value[(int)enum_盤點單號.盤點單號].ObjectToString();
                _class.盤點名稱 = value[(int)enum_盤點單號.盤點名稱].ObjectToString();
                _class.建表人 = value[(int)enum_盤點單號.建表人].ObjectToString();
                _class.建表時間 = value[(int)enum_盤點單號.建表時間].ToDateTimeString();
                _class.盤點開始時間 = value[(int)enum_盤點單號.盤點開始時間].ToDateTimeString();
                _class.盤點結束時間 = value[(int)enum_盤點單號.盤點結束時間].ToDateTimeString();
                _class.盤點狀態 = value[(int)enum_盤點單號.盤點狀態].ObjectToString();
                return _class;
            }
            static public creat ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<creat>();
            }
            static public List<creat> ObjToListClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<List<creat>>();
            }
        }
        public class content
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("Master_GUID")]
            public string Master_GUID { get; set; }
            [JsonPropertyName("IC_SN")]
            public string 盤點單號 { get; set; }
            [JsonPropertyName("CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("SKDIACODE")]
            public string 料號 { get; set; }
            [JsonPropertyName("CHT_NAME")]
            public string 中文名稱 { get; set; }
            [JsonPropertyName("NAME")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("PAKAGE")]
            public string 包裝單位 { get; set; }
            [JsonPropertyName("BARCODE1")]
            public string 藥品條碼1 { get; set; }
            [JsonPropertyName("BARCODE2")]
            public string 藥品條碼2 { get; set; }
            [JsonPropertyName("START_QTY")]
            public string 理論值 { get; set; }
            [JsonPropertyName("END_QTY")]
            public string 盤點量 { get; set; }
            [JsonPropertyName("ADD_TIME")]
            public string 新增時間 { get; set; }

            private List<sub_content> _sub_content = new List<sub_content>();
            public List<sub_content> Sub_content { get => _sub_content; set => _sub_content = value; }

            static public object[] ClassToSQL(content _class)
            {
                object[] value = new object[new enum_盤點內容().GetLength()];
                value[(int)enum_盤點內容.GUID] = _class.GUID;
                value[(int)enum_盤點內容.Master_GUID] = _class.Master_GUID;
                value[(int)enum_盤點內容.盤點單號] = _class.盤點單號;
                value[(int)enum_盤點內容.藥品碼] = _class.藥品碼;
                value[(int)enum_盤點內容.料號] = _class.料號;
                value[(int)enum_盤點內容.藥品條碼1] = _class.藥品條碼1;
                value[(int)enum_盤點內容.藥品條碼2] = _class.藥品條碼2;
                value[(int)enum_盤點內容.理論值] = _class.理論值;
                value[(int)enum_盤點內容.新增時間] = _class.新增時間;
                return value;
            }
            static public content SQLToClass(object[] value)
            {
                content _class = new content();
                _class.GUID = value[(int)enum_盤點單號.GUID].ObjectToString();
                _class.Master_GUID = value[(int)enum_盤點內容.Master_GUID].ObjectToString();
                _class.盤點單號 = value[(int)enum_盤點內容.盤點單號].ObjectToString();
                _class.藥品碼 = value[(int)enum_盤點內容.藥品碼].ObjectToString();
                _class.料號 = value[(int)enum_盤點內容.料號].ObjectToString();
                _class.藥品條碼1 = value[(int)enum_盤點內容.藥品條碼1].ObjectToString();
                _class.藥品條碼2 = value[(int)enum_盤點內容.藥品條碼2].ObjectToString();
                _class.理論值 = value[(int)enum_盤點內容.理論值].ObjectToString();
                _class.新增時間 = value[(int)enum_盤點內容.新增時間].ToDateTimeString();
                return _class;
            }
            static public content ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<content>();
            }
            static public List<content> ObjToListClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<List<content>>();
            }
        }
        public class sub_content
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("Master_GUID")]
            public string Master_GUID { get; set; }
            [JsonPropertyName("ACPT_SN")]
            public string 盤點單號 { get; set; }
            [JsonPropertyName("CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("SKDIACODE")]
            public string 料號 { get; set; }
            [JsonPropertyName("CHT_NAME")]
            public string 中文名稱 { get; set; }
            [JsonPropertyName("NAME")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("PAKAGE")]
            public string 包裝單位 { get; set; }
            [JsonPropertyName("BARCODE1")]
            public string 藥品條碼1 { get; set; }
            [JsonPropertyName("BARCODE2")]
            public string 藥品條碼2 { get; set; }
            [JsonPropertyName("END_QTY")]
            public string 盤點量 { get; set; }
            [JsonPropertyName("VAL")]
            public string 效期 { get; set; }
            [JsonPropertyName("LOT")]
            public string 批號 { get; set; }
            [JsonPropertyName("OP")]
            public string 操作人 { get; set; }
            [JsonPropertyName("OP_TIME")]
            public string 操作時間 { get; set; }
            [JsonPropertyName("STATE")]
            public string 狀態 { get; set; }

            static public object[] ClassToSQL(sub_content _class)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
                value[(int)enum_盤點明細.GUID] = _class.GUID;
                value[(int)enum_盤點明細.Master_GUID] = _class.Master_GUID;
                value[(int)enum_盤點明細.盤點單號] = _class.盤點單號;
                value[(int)enum_盤點明細.藥品碼] = _class.藥品碼;
                value[(int)enum_盤點明細.料號] = _class.料號;
                value[(int)enum_盤點明細.藥品條碼1] = _class.藥品條碼1;
                value[(int)enum_盤點明細.藥品條碼2] = _class.藥品條碼2;
                value[(int)enum_盤點明細.盤點量] = _class.盤點量;
                value[(int)enum_盤點明細.操作時間] = _class.操作時間;
                value[(int)enum_盤點明細.操作人] = _class.操作人;
                value[(int)enum_盤點明細.效期] = _class.效期;
                value[(int)enum_盤點明細.批號] = _class.批號;
                value[(int)enum_盤點明細.狀態] = _class.狀態;

                return value;
            }
            static public sub_content SQLToClass(object[] value)
            {
                sub_content _class = new sub_content();
                _class.GUID = value[(int)enum_盤點明細.GUID].ObjectToString();
                _class.Master_GUID = value[(int)enum_盤點明細.Master_GUID].ObjectToString();
                _class.盤點單號 = value[(int)enum_盤點明細.盤點單號].ObjectToString();
                _class.藥品碼 = value[(int)enum_盤點明細.藥品碼].ObjectToString();
                _class.料號 = value[(int)enum_盤點明細.料號].ObjectToString();
                _class.藥品條碼1 = value[(int)enum_盤點明細.藥品條碼1].ObjectToString();
                _class.藥品條碼2 = value[(int)enum_盤點明細.藥品條碼2].ObjectToString();
                _class.盤點量 = value[(int)enum_盤點明細.盤點量].ObjectToString();
                _class.操作人 = value[(int)enum_盤點明細.操作人].ObjectToString();
                _class.操作時間 = value[(int)enum_盤點明細.操作時間].ToDateTimeString();
                _class.效期 = value[(int)enum_盤點明細.效期].ObjectToString();
                _class.批號 = value[(int)enum_盤點明細.批號].ObjectToString();
                _class.狀態 = value[(int)enum_盤點明細.狀態].ObjectToString();

                return _class;
            }
            static public sub_content ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<sub_content>();
            }
            static public List<sub_content> ObjToListClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<List<sub_content>>();
            }
        }
    }
}
