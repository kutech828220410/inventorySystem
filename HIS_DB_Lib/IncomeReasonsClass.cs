using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_收支原因
    {
        GUID,
        類別,
        原因,
        新增時間
    }
    public class IncomeReasonsClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("TYPE")]
        public string 類別 { get; set; }
        [JsonPropertyName("RSN")]
        public string 原因 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 新增時間 { get; set; }
        static public object[] ClassToSQL(IncomeReasonsClass _class)
        {
            object[] value = new object[new enum_收支原因().GetLength()];
            value[(int)enum_收支原因.GUID] = _class.GUID;
            value[(int)enum_收支原因.類別] = _class.類別;
            value[(int)enum_收支原因.原因] = _class.原因;
            value[(int)enum_收支原因.新增時間] = _class.新增時間;

            return value;
        }
        static public List<object[]> ClassToListSQL(List<IncomeReasonsClass> _class)
        {
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < _class.Count; i++)
            {
                object[] value = ClassToSQL(_class[i]);
                list_value.Add(value);
            }
            return list_value;
        }
        static public IncomeReasonsClass SQLToClass(object[] value)
        {
            IncomeReasonsClass _class = new IncomeReasonsClass();
            _class.GUID = value[(int)enum_收支原因.GUID].ObjectToString();
            _class.類別 = value[(int)enum_收支原因.類別].ObjectToString();
            _class.原因 = value[(int)enum_收支原因.原因].ObjectToString();
            _class.新增時間 = value[(int)enum_收支原因.新增時間].ToDateTimeString_6();

            return _class;
        }

        static public List<IncomeReasonsClass> SQLToListClass(List<object[]> list_value)
        {
            List<IncomeReasonsClass> incomeReasonsClasses = new List<IncomeReasonsClass>();
            for (int i = 0; i < list_value.Count; i++)
            {
                IncomeReasonsClass incomeReasonsClass = SQLToClass(list_value[i]);
                incomeReasonsClasses.Add(incomeReasonsClass);
            }
            return incomeReasonsClasses;
        }


        static public object[] ObjToSQL(object data)
        {
            IncomeReasonsClass incomeReasonsClass = ObjToClass(data);
            return ClassToSQL(incomeReasonsClass);
        }
        static public List<object[]> ObjToListSQL(object data)
        {
            List<IncomeReasonsClass> incomeReasonsClasses = ObjToListClass(data);
            return ClassToListSQL(incomeReasonsClasses);
        }
        static public IncomeReasonsClass ObjToClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<IncomeReasonsClass>();
        }
        static public List<IncomeReasonsClass> ObjToListClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<List<IncomeReasonsClass>>();
        }

    }
}
