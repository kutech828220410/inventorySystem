using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public class class_sub_inspection_data
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        [JsonPropertyName("VAL_DATE")]
        public string 效期 { get; set; }
        [JsonPropertyName("LOT_NUMBER")]
        public string 批號 { get; set; }
        [JsonPropertyName("QTY")]
        public string 數量 { get; set; }
        [JsonPropertyName("MIS_CREATEDTTM")]
        public string 驗收時間 { get; set; }
        [JsonPropertyName("OPERATOR")]
        public string 操作人 { get; set; }
        [JsonPropertyName("LOCK")]
        public string 已鎖定 { get; set; }
        [JsonPropertyName("Update")]
        public string 更新 { get; set; }
    }
    public enum enum_驗收單號
    {
        GUID,
        驗收單號,
        請購單號,
        建表人,
        建表時間,
        驗收開始時間,
        驗收結束時間,
        驗收狀態,
       
    }
    public enum enum_驗收內容
    {
        GUID,
        Master_GUID,
        驗收單號,
        請購單號,
        藥品碼,
        料號,
        藥品條碼1,
        藥品條碼2,
        新增時間,
    }
    public enum enum_驗收明細
    {
        GUID,
        Master_GUID,
        驗收單號,
        請購單號,
        藥品碼,
        料號,
        藥品條碼1,
        藥品條碼2,
        應收數量,
        實收數量,
        驗收時間,
        狀態,
    }
    public class inspectionClass
    {
        public class creat
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("ACPT_SN")]
            public string 驗收單號 { get; set; }
            [JsonPropertyName("PO_SN")]
            public string 請購單號 { get; set; }
            [JsonPropertyName("CT")]
            public string 建表人 { get; set; }
            [JsonPropertyName("CT_TIME")]
            public string 建表時間 { get; set; }
            [JsonPropertyName("ACPT_START_TIME")]
            public string 驗收開始時間 { get; set; }
            [JsonPropertyName("ACPT_END_TIME")]
            public string 驗收結束時間 { get; set; }
            [JsonPropertyName("ACPT_END_STATE")]
            public string 驗收狀態 { get; set; }
           

            private List<content> _contents = new List<content>();
            public List<content> Contents { get => _contents; set => _contents = value; }

            static public object[] ClassToSQL(creat _class)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
                value[(int)enum_驗收單號.GUID] = _class.GUID;
                value[(int)enum_驗收單號.驗收單號] = _class.驗收單號;
                value[(int)enum_驗收單號.請購單號] = _class.請購單號;
                value[(int)enum_驗收單號.建表人] = _class.建表人;
                value[(int)enum_驗收單號.建表時間] = _class.建表時間;
                value[(int)enum_驗收單號.驗收開始時間] = _class.驗收開始時間;
                value[(int)enum_驗收單號.驗收結束時間] = _class.驗收結束時間;
                value[(int)enum_驗收單號.驗收狀態] = _class.驗收狀態;

                return value;
            }
            static public creat SQLToClass(object[] value)
            {
                creat _class = new creat();
                _class.GUID = value[(int)enum_驗收單號.GUID].ObjectToString();
                _class.驗收單號 = value[(int)enum_驗收單號.驗收單號].ObjectToString();
                _class.請購單號 = value[(int)enum_驗收單號.請購單號].ObjectToString();
                _class.建表人 = value[(int)enum_驗收單號.建表人].ObjectToString();
                _class.建表時間 = value[(int)enum_驗收單號.建表時間].ToDateTimeString();
                _class.驗收開始時間 = value[(int)enum_驗收單號.驗收開始時間].ToDateTimeString();
                _class.驗收結束時間 = value[(int)enum_驗收單號.驗收結束時間].ToDateTimeString();
                _class.驗收狀態 = value[(int)enum_驗收單號.驗收狀態].ObjectToString();
                return _class;
            }
            static public creat ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<creat>();
            }

        }
        public class content
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("Master_GUID")]
            public string Master_GUID { get; set; }
            [JsonPropertyName("ACPT_SN")]
            public string 驗收單號 { get; set; }
            [JsonPropertyName("PO_SN")]
            public string 請購單號 { get; set; }
            [JsonPropertyName("CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("SKDIACODE")]
            public string 料號 { get; set; }
            [JsonPropertyName("BARCODE1")]
            public string 藥品條碼1 { get; set; }
            [JsonPropertyName("BARCODE2")]
            public string 藥品條碼2 { get; set; }
            [JsonPropertyName("ADD_TIME")]
            public string 新增時間 { get; set; }

            private List<sub_content> _sub_content = new List<sub_content>();
            public List<sub_content> Sub_content { get => _sub_content; set => _sub_content = value; }

            static public object[] ClassToSQL(content _class)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
                value[(int)enum_驗收內容.GUID] = _class.GUID;
                value[(int)enum_驗收內容.Master_GUID] = _class.Master_GUID;
                value[(int)enum_驗收內容.驗收單號] = _class.驗收單號;
                value[(int)enum_驗收內容.請購單號] = _class.請購單號;
                value[(int)enum_驗收內容.藥品碼] = _class.藥品碼;
                value[(int)enum_驗收內容.料號] = _class.料號;
                value[(int)enum_驗收內容.藥品條碼1] = _class.藥品條碼1;
                value[(int)enum_驗收內容.藥品條碼2] = _class.藥品條碼2;
                value[(int)enum_驗收內容.新增時間] = _class.新增時間;

                return value;
            }
            static public content SQLToClass(object[] value)
            {
                content _class = new content();
                _class.GUID = value[(int)enum_驗收內容.GUID].ObjectToString();
                _class.Master_GUID = value[(int)enum_驗收內容.Master_GUID].ObjectToString();
                _class.驗收單號 = value[(int)enum_驗收內容.驗收單號].ObjectToString();
                _class.請購單號 = value[(int)enum_驗收內容.請購單號].ObjectToString();
                _class.藥品碼 = value[(int)enum_驗收內容.藥品碼].ObjectToString();
                _class.料號 = value[(int)enum_驗收內容.料號].ObjectToString();
                _class.藥品條碼1 = value[(int)enum_驗收內容.藥品條碼1].ObjectToString();
                _class.藥品條碼2 = value[(int)enum_驗收內容.藥品條碼2].ObjectToString();
                _class.新增時間 = value[(int)enum_驗收內容.新增時間].ToDateTimeString();
                return _class;
            }
            static public content ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<content>();
            }

        }
        public class sub_content
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("Master_GUID")]
            public string Master_GUID { get; set; }
            [JsonPropertyName("ACPT_SN")]
            public string 驗收單號 { get; set; }
            [JsonPropertyName("PO_SN")]
            public string 請購單號 { get; set; }
            [JsonPropertyName("CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("SKDIACODE")]
            public string 料號 { get; set; }
            [JsonPropertyName("BARCODE1")]
            public string 藥品條碼1 { get; set; }
            [JsonPropertyName("BARCODE2")]
            public string 藥品條碼2 { get; set; }
            [JsonPropertyName("START_QTY")]
            public string 應收數量 { get; set; }
            [JsonPropertyName("END_QTY")]
            public string 實收數量 { get; set; }
            [JsonPropertyName("ACPT_TIME")]
            public string 驗收時間 { get; set; }
            [JsonPropertyName("STATE")]
            public string 狀態 { get; set; }

            static public object[] ClassToSQL(sub_content _class)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
                value[(int)enum_驗收明細.GUID] = _class.GUID;
                value[(int)enum_驗收明細.Master_GUID] = _class.Master_GUID;
                value[(int)enum_驗收明細.驗收單號] = _class.驗收單號;
                value[(int)enum_驗收明細.請購單號] = _class.請購單號;
                value[(int)enum_驗收明細.藥品碼] = _class.藥品碼;
                value[(int)enum_驗收明細.料號] = _class.料號;
                value[(int)enum_驗收明細.藥品條碼1] = _class.藥品條碼1;
                value[(int)enum_驗收明細.藥品條碼2] = _class.藥品條碼2;
                value[(int)enum_驗收明細.應收數量] = _class.應收數量;
                value[(int)enum_驗收明細.實收數量] = _class.實收數量;
                value[(int)enum_驗收明細.驗收時間] = _class.驗收時間;
                value[(int)enum_驗收明細.狀態] = _class.狀態;

                return value;
            }
            static public sub_content SQLToClass(object[] value)
            {
                sub_content _class = new sub_content();
                _class.GUID = value[(int)enum_驗收明細.GUID].ObjectToString();
                _class.Master_GUID = value[(int)enum_驗收明細.Master_GUID].ObjectToString();
                _class.驗收單號 = value[(int)enum_驗收明細.驗收單號].ObjectToString();
                _class.請購單號 = value[(int)enum_驗收明細.請購單號].ObjectToString();
                _class.藥品碼 = value[(int)enum_驗收明細.藥品碼].ObjectToString();
                _class.料號 = value[(int)enum_驗收明細.料號].ObjectToString();
                _class.藥品條碼1 = value[(int)enum_驗收明細.藥品條碼1].ObjectToString();
                _class.藥品條碼2 = value[(int)enum_驗收明細.藥品條碼2].ObjectToString();
                _class.應收數量 = value[(int)enum_驗收明細.應收數量].ObjectToString();
                _class.實收數量 = value[(int)enum_驗收明細.實收數量].ObjectToString();
                _class.驗收時間 = value[(int)enum_驗收明細.驗收時間].ToDateTimeString();

                return _class;
            }
            static public sub_content ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<sub_content>();
            }
        }
    }
}
