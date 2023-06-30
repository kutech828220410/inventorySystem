using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_藥局_藥品資料
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

}
