using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
