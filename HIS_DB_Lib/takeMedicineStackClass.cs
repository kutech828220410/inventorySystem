﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;


namespace HIS_DB_Lib
{
    public enum enum_取藥堆疊母資料_狀態
    {
        庫存不足,
        無儲位,
        輸入新效期,
        選擇效期,
        新增效期,
        等待刷新,
        等待作業,
        作業完成,
        等待入帳,
        入賬完成,
        取消作業,
        刪除資料,

    }
    public enum enum_取藥堆疊母資料_作業模式
    {
        效期管控,
        複盤,
        盲盤,
        庫存不足語音提示,
    }
    public enum enum_取藥堆疊母資料
    {
        GUID,
        序號,
        調劑台名稱,
        IP,
        操作人,
        動作,
        作業模式,
        藥袋序號,
        類別,
        藥品碼,
        藥品名稱,
        單位,
        病歷號,
        病人姓名,
        床號,
        開方時間,
        操作時間,
        顏色,
        狀態,
        庫存量,
        總異動量,
        結存量,
        盤點量,
        效期,
        批號,
        備註,
        收支原因,
        診別
    }
    public enum enum_取藥堆疊子資料
    {
        GUID,
        Master_GUID,
        Device_GUID,
        序號,
        調劑台名稱,
        藥品碼,
        IP,
        Num,
        TYPE,
        效期,
        批號,
        異動量,
        致能,
        流程作業完成,
        配藥完成,
        調劑結束,
        已入帳,
    }
    public class takeMedicineStackClass
    {
        public string GUID { get; set; }
        public string 序號 { get; set; }
        public string 調劑台名稱 { get; set; }
        public string IP { get; set; }
        public string 操作人 { get; set; }
        public enum_交易記錄查詢動作 動作 { get; set; }
        public string 作業模式 { get; set; }
        public string 藥袋序號 { get; set; }
        public string 類別 { get; set; }
        public string 藥品碼 { get; set; }
        public string 藥品名稱 { get; set; }
        public string 單位 { get; set; }
        public string 病歷號 { get; set; }
        public string 病人姓名 { get; set; }
        public string 床號 { get; set; }
        public string 開方時間 { get; set; }
        public string 操作時間 { get; set; }
        public string 顏色 { get; set; }
        public enum_取藥堆疊母資料_狀態 狀態 { get; set; }
        public string 庫存量 { get; set; }
        public string 總異動量 { get; set; }
        public string 結存量 { get; set; }
        public string 盤點量 { get; set; }
        public string 效期 { get; set; }
        public string 批號 { get; set; }
        public string 備註 { get; set; }
        public string 收支原因 { get; set; }
        public string 診別 { get; set; }
    }
}
