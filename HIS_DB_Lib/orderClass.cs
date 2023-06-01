using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_醫囑資料_狀態
    {
        未過帳,
        已過帳,
        庫存不足,
        無儲位,
    }
    public enum enum_醫囑資料
    {
        GUID,
        PRI_KEY,
        藥局代碼,
        藥袋條碼,
        藥品碼,
        藥品名稱,
        病人姓名,
        病歷號,
        交易量,
        開方日期,
        產出時間,
        過帳時間,
        狀態,
    }
}
