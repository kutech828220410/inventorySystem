using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_藥品資料_藥檔資料
    {
        GUID,
        藥品碼,
        料號,
        藥品中文名稱,
        藥品名稱,
        藥品學名,
        藥品群組,
        健保碼,
        藥品條碼,
        藥品條碼1,
        藥品條碼2,
        包裝單位,
        庫存,
        安全庫存,
        圖片網址,
        警訊藥品,
        管制級別,
    }
    public class medSMDSClass : medClass
    {
        static public List<object[]> ClassToSQL(List<medClass> _class)
        {
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < _class.Count; i++)
            {
                object[] value = ClassToSQL(_class[i]);
                list_value.Add(value);
            }
            return list_value;
        }
        static public object[] ClassToSQL(medClass _class)
        {
            object[] value = new object[new enum_藥品資料_藥檔資料().GetLength()];
            value[(int)enum_藥品資料_藥檔資料.GUID] = _class.GUID;
            value[(int)enum_藥品資料_藥檔資料.藥品碼] = _class.藥品碼;
            value[(int)enum_藥品資料_藥檔資料.料號] = _class.料號;
            value[(int)enum_藥品資料_藥檔資料.藥品中文名稱] = _class.中文名稱;
            value[(int)enum_藥品資料_藥檔資料.藥品名稱] = _class.藥品名稱;
            value[(int)enum_藥品資料_藥檔資料.藥品學名] = _class.藥品學名;
            value[(int)enum_藥品資料_藥檔資料.健保碼] = _class.健保碼;
            value[(int)enum_藥品資料_藥檔資料.包裝單位] = _class.包裝單位;
            value[(int)enum_藥品資料_藥檔資料.藥品條碼1] = _class.藥品條碼1;
            value[(int)enum_藥品資料_藥檔資料.藥品條碼2] = _class.藥品條碼2;
            value[(int)enum_藥品資料_藥檔資料.警訊藥品] = _class.警訊藥品;
            value[(int)enum_藥品資料_藥檔資料.管制級別] = _class.管制級別;
            value[(int)enum_藥品資料_藥檔資料.庫存] = _class.總庫存;


            return value;
        }

        static public medSMDSClass SQLToClass(object[] value)
        {
            medSMDSClass _class = new medSMDSClass();
            _class.GUID = value[(int)enum_藥品資料_藥檔資料.GUID].ObjectToString();
            _class.藥品碼 = value[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            _class.料號 = value[(int)enum_藥品資料_藥檔資料.料號].ObjectToString();
            _class.中文名稱 = value[(int)enum_藥品資料_藥檔資料.藥品中文名稱].ObjectToString();
            _class.藥品名稱 = value[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
            _class.藥品學名 = value[(int)enum_藥品資料_藥檔資料.藥品學名].ObjectToString();
            _class.健保碼 = value[(int)enum_藥品資料_藥檔資料.健保碼].ObjectToString();
            _class.包裝單位 = value[(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
            _class.藥品條碼1 = value[(int)enum_藥品資料_藥檔資料.藥品條碼1].ObjectToString();
            _class.藥品條碼2 = value[(int)enum_藥品資料_藥檔資料.藥品條碼2].ObjectToString();
            _class.警訊藥品 = value[(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString();
            _class.管制級別 = value[(int)enum_藥品資料_藥檔資料.管制級別].ObjectToString();
            return _class;
        }
        static public List<medClass> SQLToClass(List<object[]> values)
        {
            List<medClass> medClasses = new List<medClass>();
            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                medClass _class = new medClass();
                _class.GUID = value[(int)enum_藥品資料_藥檔資料.GUID].ObjectToString();
                _class.藥品碼 = value[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                _class.料號 = value[(int)enum_藥品資料_藥檔資料.料號].ObjectToString();
                _class.中文名稱 = value[(int)enum_藥品資料_藥檔資料.藥品中文名稱].ObjectToString();
                _class.藥品名稱 = value[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                _class.藥品學名 = value[(int)enum_藥品資料_藥檔資料.藥品學名].ObjectToString();
                _class.健保碼 = value[(int)enum_藥品資料_藥檔資料.健保碼].ObjectToString();
                _class.包裝單位 = value[(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                _class.藥品條碼1 = value[(int)enum_藥品資料_藥檔資料.藥品條碼1].ObjectToString();
                _class.藥品條碼2 = value[(int)enum_藥品資料_藥檔資料.藥品條碼2].ObjectToString();
                _class.警訊藥品 = value[(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString();
                _class.管制級別 = value[(int)enum_藥品資料_藥檔資料.管制級別].ObjectToString();
                medClasses.Add(_class);
            }

            return medClasses;
        }

    }
}
