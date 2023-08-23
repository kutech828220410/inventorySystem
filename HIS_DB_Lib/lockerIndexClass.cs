using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
namespace HIS_DB_Lib
{
    public enum enum_Locker_Index_Table
    {
        GUID,
        IP,
        Num,
        輸入位置,
        輸入狀態,
        輸出位置,
        輸出狀態,
        同步輸出,
        Master_GUID,
        Slave_GUID,
        Device_GUID,
    }
}
