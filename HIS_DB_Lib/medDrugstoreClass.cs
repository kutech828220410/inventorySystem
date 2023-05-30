using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_藥庫_藥品資料
    {
        GUID,
        藥品碼,
        料號,
        中文名稱,
        藥品名稱,
        藥品學名,
        藥品群組,
        健保碼,
        包裝單位,
        包裝數量,
        最小包裝單位,
        最小包裝數量,
        藥局庫存,
        藥庫庫存,
        總庫存,
        基準量,
        安全庫存,
        藥品條碼1,
        藥品條碼2,
        警訊藥品,
        管制級別,
        類別,
    }

    public class medDrugstoreClass : medClass
    {
       

        static public object[] ClassToSQL(medClass _class)
        {
            object[] value = new object[new enum_藥庫_藥品資料().GetLength()];
            value[(int)enum_藥庫_藥品資料.GUID] = _class.GUID;
            value[(int)enum_藥庫_藥品資料.藥品碼] = _class.藥品碼;
            value[(int)enum_藥庫_藥品資料.料號] = _class.料號;
            value[(int)enum_藥庫_藥品資料.中文名稱] = _class.中文名稱;
            value[(int)enum_藥庫_藥品資料.藥品名稱] = _class.藥品名稱;
            value[(int)enum_藥庫_藥品資料.藥品學名] = _class.藥品學名;
            value[(int)enum_藥庫_藥品資料.健保碼] = _class.健保碼;
            value[(int)enum_藥庫_藥品資料.包裝單位] = _class.包裝單位;
            value[(int)enum_藥庫_藥品資料.包裝數量] = _class.包裝數量;
            value[(int)enum_藥庫_藥品資料.最小包裝單位] = _class.最小包裝單位;
            value[(int)enum_藥庫_藥品資料.最小包裝數量] = _class.最小包裝數量;
            value[(int)enum_藥庫_藥品資料.藥品條碼1] = _class.藥品條碼1;
            value[(int)enum_藥庫_藥品資料.藥品條碼2] = _class.藥品條碼2;
            value[(int)enum_藥庫_藥品資料.警訊藥品] = _class.警訊藥品;
            value[(int)enum_藥庫_藥品資料.管制級別] = _class.管制級別;
            value[(int)enum_藥庫_藥品資料.藥局庫存] = _class.藥局庫存;
            value[(int)enum_藥庫_藥品資料.藥庫庫存] = _class.藥庫庫存;
            value[(int)enum_藥庫_藥品資料.總庫存] = _class.總庫存;
            value[(int)enum_藥庫_藥品資料.基準量] = _class.基準量;
            value[(int)enum_藥庫_藥品資料.安全庫存] = _class.安全庫存;

            return value;
        }
        static public medClass SQLToClass(object[] value)
        {

            medClass _class = new medClass();
            _class.GUID = value[(int)enum_藥庫_藥品資料.GUID].ObjectToString();
            _class.藥品碼 = value[(int)enum_藥庫_藥品資料.藥品碼].ObjectToString();
            _class.料號 = value[(int)enum_藥庫_藥品資料.料號].ObjectToString();
            _class.中文名稱 = value[(int)enum_藥庫_藥品資料.中文名稱].ObjectToString();
            _class.藥品名稱 = value[(int)enum_藥庫_藥品資料.藥品名稱].ObjectToString();
            _class.藥品學名 = value[(int)enum_藥庫_藥品資料.藥品學名].ObjectToString();
            _class.健保碼 = value[(int)enum_藥庫_藥品資料.健保碼].ObjectToString();
            _class.包裝單位 = value[(int)enum_藥庫_藥品資料.包裝單位].ObjectToString();
            _class.包裝數量 = value[(int)enum_藥庫_藥品資料.包裝數量].ObjectToString();
            _class.最小包裝單位 = value[(int)enum_藥庫_藥品資料.最小包裝單位].ObjectToString();
            _class.最小包裝數量 = value[(int)enum_藥庫_藥品資料.最小包裝數量].ObjectToString();
            _class.藥品條碼1 = value[(int)enum_藥庫_藥品資料.藥品條碼1].ObjectToString();
            _class.藥品條碼2 = value[(int)enum_藥庫_藥品資料.藥品條碼2].ObjectToString();
            _class.警訊藥品 = value[(int)enum_藥庫_藥品資料.警訊藥品].ObjectToString();
            _class.管制級別 = value[(int)enum_藥庫_藥品資料.管制級別].ObjectToString();
            return _class;
        }
        static public List<medClass> SQLToClass(List<object[]> values)
        {
            List<medClass> medClasses = new List<medClass>();
            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                medClass _class = new medDrugstoreClass();
                _class.GUID = value[(int)enum_藥庫_藥品資料.GUID].ObjectToString();
                _class.藥品碼 = value[(int)enum_藥庫_藥品資料.藥品碼].ObjectToString();
                _class.料號 = value[(int)enum_藥庫_藥品資料.料號].ObjectToString();
                _class.中文名稱 = value[(int)enum_藥庫_藥品資料.中文名稱].ObjectToString();
                _class.藥品名稱 = value[(int)enum_藥庫_藥品資料.藥品名稱].ObjectToString();
                _class.藥品學名 = value[(int)enum_藥庫_藥品資料.藥品學名].ObjectToString();
                _class.健保碼 = value[(int)enum_藥庫_藥品資料.健保碼].ObjectToString();
                _class.包裝單位 = value[(int)enum_藥庫_藥品資料.包裝單位].ObjectToString();
                _class.包裝數量 = value[(int)enum_藥庫_藥品資料.包裝數量].ObjectToString();
                _class.最小包裝單位 = value[(int)enum_藥庫_藥品資料.最小包裝單位].ObjectToString();
                _class.最小包裝數量 = value[(int)enum_藥庫_藥品資料.最小包裝數量].ObjectToString();
                _class.藥品條碼1 = value[(int)enum_藥庫_藥品資料.藥品條碼1].ObjectToString();
                _class.藥品條碼2 = value[(int)enum_藥庫_藥品資料.藥品條碼2].ObjectToString();
                _class.警訊藥品 = value[(int)enum_藥庫_藥品資料.警訊藥品].ObjectToString();
                _class.管制級別 = value[(int)enum_藥庫_藥品資料.管制級別].ObjectToString();
                medClasses.Add(_class);

            }
  
            return medClasses;
        }

    }
}
