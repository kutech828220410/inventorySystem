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
   
    public class medCouldClass : medClass
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
            object[] value = new object[new enum_雲端藥檔().GetLength()];
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
        static public medClass SQLToClass(object[] value)
        {
            medClass _class = new medClass();
            _class.GUID = value[(int)enum_雲端藥檔.GUID].ObjectToString();
            _class.藥品碼 = value[(int)enum_雲端藥檔.藥品碼].ObjectToString();
            _class.料號 = value[(int)enum_雲端藥檔.料號].ObjectToString();
            _class.中文名稱 = value[(int)enum_雲端藥檔.中文名稱].ObjectToString();
            _class.藥品名稱 = value[(int)enum_雲端藥檔.藥品名稱].ObjectToString();
            _class.藥品學名 = value[(int)enum_雲端藥檔.藥品學名].ObjectToString();
            _class.健保碼 = value[(int)enum_雲端藥檔.健保碼].ObjectToString();
            _class.包裝單位 = value[(int)enum_雲端藥檔.包裝單位].ObjectToString();
            _class.包裝數量 = value[(int)enum_雲端藥檔.包裝數量].ObjectToString();
            _class.最小包裝單位 = value[(int)enum_雲端藥檔.最小包裝單位].ObjectToString();
            _class.最小包裝數量 = value[(int)enum_雲端藥檔.最小包裝數量].ObjectToString();
            _class.藥品條碼1 = value[(int)enum_雲端藥檔.藥品條碼1].ObjectToString();
            _class.藥品條碼2 = value[(int)enum_雲端藥檔.藥品條碼2].ObjectToString();
            _class.警訊藥品 = value[(int)enum_雲端藥檔.警訊藥品].ObjectToString();
            _class.管制級別 = value[(int)enum_雲端藥檔.管制級別].ObjectToString();
            return _class;
        }
        static public List<medClass> SQLToClass(List<object[]> values)
        {
            List<medClass> medClasses = new List<medClass>();
            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                medClass _class = new medClass();
                _class.GUID = value[(int)enum_雲端藥檔.GUID].ObjectToString();
                _class.藥品碼 = value[(int)enum_雲端藥檔.藥品碼].ObjectToString();
                _class.料號 = value[(int)enum_雲端藥檔.料號].ObjectToString();
                _class.中文名稱 = value[(int)enum_雲端藥檔.中文名稱].ObjectToString();
                _class.藥品名稱 = value[(int)enum_雲端藥檔.藥品名稱].ObjectToString();
                _class.藥品學名 = value[(int)enum_雲端藥檔.藥品學名].ObjectToString();
                _class.健保碼 = value[(int)enum_雲端藥檔.健保碼].ObjectToString();
                _class.包裝單位 = value[(int)enum_雲端藥檔.包裝單位].ObjectToString();
                _class.包裝數量 = value[(int)enum_雲端藥檔.包裝數量].ObjectToString();
                _class.最小包裝單位 = value[(int)enum_雲端藥檔.最小包裝單位].ObjectToString();
                _class.最小包裝數量 = value[(int)enum_雲端藥檔.最小包裝數量].ObjectToString();
                _class.藥品條碼1 = value[(int)enum_雲端藥檔.藥品條碼1].ObjectToString();
                _class.藥品條碼2 = value[(int)enum_雲端藥檔.藥品條碼2].ObjectToString();
                _class.警訊藥品 = value[(int)enum_雲端藥檔.警訊藥品].ObjectToString();
                _class.管制級別 = value[(int)enum_雲端藥檔.管制級別].ObjectToString();
                medClasses.Add(_class);
            }

            return medClasses;
        }
     

    }
}
