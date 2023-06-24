using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_consumption
    {
        藥品碼,
        藥品名稱,
        交易量,
        結存量
    }

    public class consumptionClass
    {
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("TXN_QTY")]
        public string 交易量 { get; set; }
        [JsonPropertyName("EBQ_QTY")]
        public string 結存量 { get; set; }

        static public object[] ClassToSQL(consumptionClass _class)
        {
            object[] value = new object[new enum_consumption().GetLength()];
            value[(int)enum_consumption.藥品碼] = _class.藥品碼;
            value[(int)enum_consumption.藥品名稱] = _class.藥品名稱;
            value[(int)enum_consumption.交易量] = _class.交易量;
            value[(int)enum_consumption.結存量] = _class.結存量;       
            return value;
        }
        static public List<object[]> ClassToSQL(List<consumptionClass> _classes)
        {
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < _classes.Count; i++)
            {
                object[] value = ClassToSQL(_classes[i]);
                list_value.Add(value);
            }

            return list_value;
        }
        static public consumptionClass SQLToClass(object[] value)
        {
            consumptionClass _class = new consumptionClass();
            _class.藥品碼 = value[(int)enum_consumption.藥品碼].ObjectToString();
            _class.藥品名稱 = value[(int)enum_consumption.藥品名稱].ObjectToString();       
            _class.交易量 = value[(int)enum_consumption.交易量].ObjectToString();
            _class.結存量 = value[(int)enum_consumption.結存量].ObjectToString();      
            return _class;
        }
        static public List<consumptionClass> SQLToClass(List<object[]> values)
        {
            List<consumptionClass> list_value = new List<consumptionClass>();
            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                consumptionClass _class = SQLToClass(values[i]);
                list_value.Add(_class);
            }

            return list_value;
        }
        static public consumptionClass ObjToClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<consumptionClass>();
        }
        static public List<consumptionClass> ObjToListClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<List<consumptionClass>>();
        }
    }
}
