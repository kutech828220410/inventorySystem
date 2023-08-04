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
        中文名稱,
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
        基準量,
        圖片網址,
        警訊藥品,
        高價藥品,
        生物製劑,
        管制級別,
        類別,
        廠牌,
        藥品許可證號,
        開檔狀態,
    }
}
