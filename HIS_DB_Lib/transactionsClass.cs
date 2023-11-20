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
        藥袋刷入,
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
        系統入庫,
        入庫作業,
        出庫作業,
        調入作業,
        調出作業,
        管制抽屜開啟,
        管制抽屜關閉,
        門片未關閉異常,
        關閉門片,
        開啟門片,
        交班對點,
        取消作業,
        盤點調整,
        批次過帳,
        驗收作業,
        驗收入庫,
        自動撥補,
        緊急申領,
        新增效期,
        修正庫存,
        修正批號,
        盤存盈虧,
        None,
    }
    public enum enum_交易記錄查詢資料
    {
        GUID,
        動作,
        診別,
        庫別,
        藥品碼,
        藥品名稱,
        藥袋序號,
        領藥號,
        類別,
        庫存量,
        交易量,
        結存量,
        盤點量,
        操作人,
        領用人,
        藥師證字號,
        病人姓名,
        頻次,
        病房號,
        床號,
        病歷號,
        操作時間,
        領用時間,
        開方時間,
        收支原因,
        備註,
    }

    public enum enum_傳送櫃領用紀錄動作
    {
        藥袋刷入,     
        人臉識別登入,
        RFID登入,
        一維碼登入,
        密碼登入,
        登出,
        操作工程模式,       
        門片未關閉異常,
        關閉門片,
        開啟門片,
        交班對點,
        取消作業,
        盤點量更正,
        批次過帳,
        驗收作業,
        驗收入庫,
        自動撥補,
        緊急申領,
        新增效期,
        修正庫存,
        修正批號,
        盤存盈虧,
        None,
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
        [JsonPropertyName("MED_BAG_NUM")]
        public string 領藥號 { get; set; }
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
        [JsonPropertyName("RECV")]
        public string 領用人 { get; set; }
        [JsonPropertyName("LICENSE")]
        public string 藥師證字號 { get; set; }
        [JsonPropertyName("PAT")]
        public string 病人姓名 { get; set; }
        [JsonPropertyName("WARD_NAME")]
        public string 病房號 { get; set; }
        [JsonPropertyName("BED")]
        public string 床號 { get; set; }
        [JsonPropertyName("FREQ")]
        public string 頻次 { get; set; }
        [JsonPropertyName("MRN")]
        public string 病歷號 { get; set; }
        [JsonPropertyName("OP_TIME")]
        public string 操作時間 { get; set; }
        [JsonPropertyName("RX_TIME")]
        public string 開方時間 { get; set; }
        [JsonPropertyName("RECV_TIME")]
        public string 領用時間 { get; set; }
        [JsonPropertyName("RSN")]
        public string 收支原因 { get; set; }
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }



        
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
