using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_交易記錄查詢動作
    {
        掃碼領藥,
        手輸領藥,
        批次領藥,
        系統領藥,
        掃碼退藥,
        手輸退藥,
        重複領藥,
        自動過帳,
        人臉識別登入,
        RFID登入,
        一維碼登入,
        密碼登入,
        登出,
        操作工程模式,
        效期庫存異動,
        入庫作業,
        出庫作業,
        調入作業,
        調出作業,
        管制抽屜開啟,
        管制抽屜關閉,
        交班對點,
        None,
    }
    public enum enum_交易記錄查詢資料
    {
        GUID,
        動作,
        診別,
        //庫別,
        藥品碼,
        藥品名稱,
        藥袋序號,
        類別,
        庫存量,
        交易量,
        結存量,
        盤點量,
        操作人,
        病人姓名,
        床號,
        病歷號,
        操作時間,
        開方時間,
        收支原因,
        備註,
    }



    public class transactionsClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("ACTION")]
        public string 動作 { get; set; }
        [JsonPropertyName("MEDKND")]
        public string 診別 { get; set; }
        [JsonPropertyName("STTREHOUSE")]
        public string 庫別 { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("MED_BAG_SN")]
        public string 藥袋序號 { get; set; }
        [JsonPropertyName("TYPE")]
        public string 類別 { get; set; }
        [JsonPropertyName("INV_QTY")]
        public string 庫存量 { get; set; }
        [JsonPropertyName("TXN_QTY")]
        public string 交易量 { get; set; }
        [JsonPropertyName("EBQ_QTY")]
        public string 結存量 { get; set; }
        [JsonPropertyName("PHY_QTY")]
        public string 盤點量 { get; set; }
        [JsonPropertyName("OP")]
        public string 操作人 { get; set; }
        [JsonPropertyName("PAT")]
        public string 病人姓名 { get; set; }
        [JsonPropertyName("BED")]
        public string 床號 { get; set; }
        [JsonPropertyName("MRN")]
        public string 病歷號 { get; set; }
        [JsonPropertyName("OP_TIME")]
        public string 操作時間 { get; set; }
        [JsonPropertyName("RX_TIME")]
        public string 開方時間 { get; set; }
        [JsonPropertyName("RSN")]
        public string 收支原因 { get; set; }
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }


        static public object[] ClassToSQL(transactionsClass _class)
        {
            object[] value = new object[new enum_交易記錄查詢資料().GetLength()];
            value[(int)enum_交易記錄查詢資料.GUID] = _class.GUID;
            value[(int)enum_交易記錄查詢資料.動作] = _class.動作;
            value[(int)enum_交易記錄查詢資料.診別] = _class.診別;
            //value[(int)enum_交易記錄查詢資料.庫別] = _class.庫別;
            value[(int)enum_交易記錄查詢資料.藥品碼] = _class.藥品碼;
            value[(int)enum_交易記錄查詢資料.藥品名稱] = _class.藥品名稱;
            value[(int)enum_交易記錄查詢資料.藥品碼] = _class.藥品碼;
            value[(int)enum_交易記錄查詢資料.藥袋序號] = _class.藥袋序號;
            value[(int)enum_交易記錄查詢資料.類別] = _class.類別;
            value[(int)enum_交易記錄查詢資料.庫存量] = _class.庫存量;
            value[(int)enum_交易記錄查詢資料.交易量] = _class.交易量;
            value[(int)enum_交易記錄查詢資料.結存量] = _class.結存量;
            value[(int)enum_交易記錄查詢資料.盤點量] = _class.盤點量;
            value[(int)enum_交易記錄查詢資料.操作人] = _class.操作人;
            value[(int)enum_交易記錄查詢資料.病人姓名] = _class.病人姓名;
            value[(int)enum_交易記錄查詢資料.床號] = _class.床號;
            value[(int)enum_交易記錄查詢資料.病歷號] = _class.病歷號;
            value[(int)enum_交易記錄查詢資料.操作時間] = _class.操作時間;
            value[(int)enum_交易記錄查詢資料.開方時間] = _class.開方時間;
            value[(int)enum_交易記錄查詢資料.備註] = _class.備註;
            value[(int)enum_交易記錄查詢資料.收支原因] = _class.收支原因;
            return value;
        }
        static public List<object[]> ClassToSQL(List<transactionsClass> _classes)
        {
            List<object[]> list_value = new List<object[]>();
            for(int i = 0; i < _classes.Count; i++)
            {
                object[] value = ClassToSQL(_classes[i]);
                list_value.Add(value);
            }
          
            return list_value;
        }
        static public transactionsClass SQLToClass(object[] value)
        {
            transactionsClass _class = new transactionsClass();
            _class.GUID = value[(int)enum_交易記錄查詢資料.GUID].ObjectToString();
            _class.動作 = value[(int)enum_交易記錄查詢資料.動作].ObjectToString();
            _class.診別 = value[(int)enum_交易記錄查詢資料.診別].ObjectToString();
            //_class.庫別 = value[(int)enum_交易記錄查詢資料.庫別].ObjectToString();
            _class.藥品碼 = value[(int)enum_交易記錄查詢資料.藥品碼].ObjectToString();
            _class.藥品名稱 = value[(int)enum_交易記錄查詢資料.藥品名稱].ObjectToString();
            _class.藥袋序號 = value[(int)enum_交易記錄查詢資料.藥袋序號].ObjectToString();
            _class.類別 = value[(int)enum_交易記錄查詢資料.類別].ObjectToString();
            _class.庫存量 = value[(int)enum_交易記錄查詢資料.庫存量].ObjectToString();
            _class.交易量 = value[(int)enum_交易記錄查詢資料.交易量].ObjectToString();
            _class.結存量 = value[(int)enum_交易記錄查詢資料.結存量].ObjectToString();
            _class.盤點量 = value[(int)enum_交易記錄查詢資料.盤點量].ObjectToString();
            _class.操作人 = value[(int)enum_交易記錄查詢資料.操作人].ObjectToString();
            _class.病人姓名 = value[(int)enum_交易記錄查詢資料.病人姓名].ObjectToString();
            _class.床號 = value[(int)enum_交易記錄查詢資料.床號].ObjectToString();
            _class.病歷號 = value[(int)enum_交易記錄查詢資料.病歷號].ObjectToString();
            _class.操作時間 = value[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString();
            _class.開方時間 = value[(int)enum_交易記錄查詢資料.開方時間].ToDateTimeString();
            _class.備註 = value[(int)enum_交易記錄查詢資料.備註].ObjectToString();
            _class.收支原因 = value[(int)enum_交易記錄查詢資料.收支原因].ObjectToString();

            return _class;
        }
        static public List<transactionsClass> SQLToClass(List<object[]> values)
        {
            List<transactionsClass> transactionsClasses = new List<transactionsClass>();
            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                transactionsClass _class = new transactionsClass();
                _class.GUID = value[(int)enum_交易記錄查詢資料.GUID].ObjectToString();
                _class.動作 = value[(int)enum_交易記錄查詢資料.動作].ObjectToString();
                _class.藥品碼 = value[(int)enum_交易記錄查詢資料.藥品碼].ObjectToString();
                _class.藥品名稱 = value[(int)enum_交易記錄查詢資料.藥品名稱].ObjectToString();
                _class.藥袋序號 = value[(int)enum_交易記錄查詢資料.藥袋序號].ObjectToString();
                _class.類別 = value[(int)enum_交易記錄查詢資料.類別].ObjectToString();
                _class.庫存量 = value[(int)enum_交易記錄查詢資料.庫存量].ObjectToString();
                _class.交易量 = value[(int)enum_交易記錄查詢資料.交易量].ObjectToString();
                _class.結存量 = value[(int)enum_交易記錄查詢資料.結存量].ObjectToString();
                _class.盤點量 = value[(int)enum_交易記錄查詢資料.盤點量].ObjectToString();
                _class.操作人 = value[(int)enum_交易記錄查詢資料.操作人].ObjectToString();
                _class.病人姓名 = value[(int)enum_交易記錄查詢資料.病人姓名].ObjectToString();
                _class.床號 = value[(int)enum_交易記錄查詢資料.床號].ObjectToString();
                _class.病歷號 = value[(int)enum_交易記錄查詢資料.病歷號].ObjectToString();
                _class.操作時間 = value[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString();
                _class.開方時間 = value[(int)enum_交易記錄查詢資料.開方時間].ToDateTimeString();
                _class.收支原因 = value[(int)enum_交易記錄查詢資料.收支原因].ObjectToString();
                _class.備註 = value[(int)enum_交易記錄查詢資料.備註].ObjectToString();
                transactionsClasses.Add(_class);
            }

            return transactionsClasses;
        }
        static public transactionsClass ObjToClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<transactionsClass>();
        }
        static public List<transactionsClass> ObjToListClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<List<transactionsClass>>();
        }

        public class ICP_By_OP_Time : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                DateTime datetime2 = y[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                if (compare != 0) return compare;
                int 結存量1 = x[(int)enum_交易記錄查詢資料.結存量].StringToInt32();
                int 結存量2 = y[(int)enum_交易記錄查詢資料.結存量].StringToInt32();
                if (結存量1 > 結存量2)
                {
                    return -1;
                }
                else if (結存量1 < 結存量2)
                {
                    return 1;
                }
                else if (結存量1 == 結存量2) return 0;
                return 0;

            }
        }
    }
}
