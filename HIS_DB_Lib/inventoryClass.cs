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
        一維條碼,
        理論值,
        盤點量,
        操作人,
        盤點時間,
    }
    public class returnData
    {
        private List<object> _data = new List<object>();
        private int _code = 0;
        private string _result = "";

        public List<object> Data { get => _data; set => _data = value; }
        public int Code { get => _code; set => _code = value; }
        public string Result { get => _result; set => _result = value; }
    }
    public class inventoryClass
    {

        public class creat_OUT
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("INV_SN_L")]
            public string 盤點單號 { get; set; }
            [JsonPropertyName("CREAT_OP")]
            public string 建表人 { get; set; }
            [JsonPropertyName("CREAT_TIME")]
            public string 建表時間 { get; set; }
            [JsonPropertyName("START_TIME")]
            public string 盤點開始時間 { get; set; }
            [JsonPropertyName("END_TIME")]
            public string 盤點結束時間 { get; set; }
            [JsonPropertyName("STATE")]
            public string 盤點狀態 { get; set; }

            static public object[] ClassToSQL(creat_OUT _class)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
                value[(int)enum_盤點單號.GUID] = _class.GUID;
                value[(int)enum_盤點單號.盤點單號] = _class.盤點單號;
                value[(int)enum_盤點單號.建表人] = _class.建表人;
                value[(int)enum_盤點單號.建表時間] = _class.建表時間;
                value[(int)enum_盤點單號.盤點開始時間] = _class.盤點開始時間;
                value[(int)enum_盤點單號.盤點結束時間] = _class.盤點結束時間;
                value[(int)enum_盤點單號.盤點狀態] = _class.盤點狀態;

                return value;
            }
            static public creat_OUT SQLToClass(object[] value)
            {
                creat_OUT _class = new creat_OUT();
                _class.GUID = value[(int)enum_盤點單號.GUID].ObjectToString();
                _class.盤點單號 = value[(int)enum_盤點單號.盤點單號].ObjectToString();
                _class.建表人 = value[(int)enum_盤點單號.建表人].ObjectToString();
                _class.建表時間 = value[(int)enum_盤點單號.建表時間].ToDateTimeString();
                _class.盤點開始時間 = value[(int)enum_盤點單號.盤點開始時間].ToDateTimeString();
                _class.盤點結束時間 = value[(int)enum_盤點單號.盤點結束時間].ToDateTimeString();
                _class.盤點狀態 = value[(int)enum_盤點單號.盤點狀態].ObjectToString();
                return _class;
            }
            static public creat_OUT ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<creat_OUT>();
            }
        }
        public class Inv_OUT
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("Master_GUID")]
            public string Master_GUID { get; set; }
            [JsonPropertyName("INV_SN_L")]
            public string 盤點單號 { get; set; }
            [JsonPropertyName("CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("SKDIACODE")]
            public string 料號 { get; set; }
            [JsonPropertyName("START_TIME")]
            public string 一維條碼 { get; set; }
            [JsonPropertyName("THEOR_VAL")]
            public string 理論值 { get; set; }
            [JsonPropertyName("QTY")]
            public string 盤點量 { get; set; }
            [JsonPropertyName("OPERATOR")]
            public string 操作人 { get; set; }
            [JsonPropertyName("INV_TIME")]
            public string 盤點時間 { get; set; }

            static public object[] ClassToSQL(Inv_OUT _class)
            {
                object[] value = new object[new enum_盤點內容().GetLength()];
                value[(int)enum_盤點內容.GUID] = _class.GUID;
                value[(int)enum_盤點內容.Master_GUID] = _class.Master_GUID;
                value[(int)enum_盤點內容.盤點單號] = _class.盤點單號;
                value[(int)enum_盤點內容.藥品碼] = _class.藥品碼;
                value[(int)enum_盤點內容.料號] = _class.料號;
                value[(int)enum_盤點內容.理論值] = _class.理論值;
                value[(int)enum_盤點內容.盤點量] = _class.盤點量;
                value[(int)enum_盤點內容.操作人] = _class.操作人;
                value[(int)enum_盤點內容.盤點時間] = _class.盤點時間;
                return value;
            }
            static public Inv_OUT SQLToClass(object[] value)
            {
                Inv_OUT _class = new Inv_OUT();
                _class.GUID = value[(int)enum_盤點單號.GUID].ObjectToString();
                _class.Master_GUID = value[(int)enum_盤點內容.盤點單號].ObjectToString();
                _class.盤點單號 = value[(int)enum_盤點內容.盤點單號].ObjectToString();
                _class.藥品碼 = value[(int)enum_盤點內容.藥品碼].ToDateTimeString();
                _class.料號 = value[(int)enum_盤點內容.料號].ToDateTimeString();
                _class.理論值 = value[(int)enum_盤點內容.理論值].ToDateTimeString();
                _class.盤點量 = value[(int)enum_盤點內容.盤點量].ObjectToString();
                _class.操作人 = value[(int)enum_盤點內容.操作人].ObjectToString();
                _class.盤點時間 = value[(int)enum_盤點內容.盤點時間].ObjectToString();
                return _class;
            }
            static public Inv_OUT ObjToClass(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<Inv_OUT>();
            }
        }
    }
}
