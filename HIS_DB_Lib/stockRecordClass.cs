using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

using System.ComponentModel;

using System.Reflection;

namespace HIS_DB_Lib
{
    public enum enum_stockRecord
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥碼,VARCHAR,15,None")]
        藥碼,
        [Description("庫存,VARCHAR,15,None")]
        庫存,
        [Description("加入時間,DATETIME,50,INDEX")]
        加入時間,
    }
    public class stockRecordClass
    {
    }
}
