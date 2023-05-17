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

            static public object[] ClassToSQL(creat_OUT _inventory_creat_OUT)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
                value[(int)enum_盤點單號.GUID] = _inventory_creat_OUT.GUID;
                value[(int)enum_盤點單號.盤點單號] = _inventory_creat_OUT.盤點單號;
                value[(int)enum_盤點單號.建表人] = _inventory_creat_OUT.建表人;
                value[(int)enum_盤點單號.建表時間] = _inventory_creat_OUT.建表時間;
                value[(int)enum_盤點單號.盤點開始時間] = _inventory_creat_OUT.盤點開始時間;
                value[(int)enum_盤點單號.盤點結束時間] = _inventory_creat_OUT.盤點結束時間;
                value[(int)enum_盤點單號.盤點狀態] = _inventory_creat_OUT.盤點狀態;

                return value;
            }
            static public creat_OUT SQLToClass(object[] value)
            {
                creat_OUT inventory_Creat_OUT = new creat_OUT();
                inventory_Creat_OUT.GUID = value[(int)enum_盤點單號.GUID].ObjectToString();
                inventory_Creat_OUT.盤點單號 = value[(int)enum_盤點單號.盤點單號].ObjectToString();
                inventory_Creat_OUT.建表人 = value[(int)enum_盤點單號.建表人].ObjectToString();
                inventory_Creat_OUT.建表時間 = value[(int)enum_盤點單號.建表時間].ToDateString();
                inventory_Creat_OUT.盤點開始時間 = value[(int)enum_盤點單號.盤點開始時間].ToDateString();
                inventory_Creat_OUT.盤點結束時間 = value[(int)enum_盤點單號.盤點結束時間].ToDateString();
                inventory_Creat_OUT.盤點狀態 = value[(int)enum_盤點單號.盤點狀態].ObjectToString();


                return inventory_Creat_OUT;
            }
            static public creat_OUT ObjToData(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<creat_OUT>();
            }
        }

    }
}
