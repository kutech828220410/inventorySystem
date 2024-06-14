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
    [EnumDescription("drug_stotre_Distribution")]
    public enum enum_drugStotreDistributionCategory
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥碼,VARCHAR,20,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("安全量,VARCHAR,20,NONE")]
        安全量,
        [Description("基準量,VARCHAR,20,NONE")]
        基準量,
        [Description("分類名稱,VARCHAR,50,NONE")]
        分類名稱,
        [Description("加入時間,DATETIME,20,NONE")]
        加入時間,
    }
}
