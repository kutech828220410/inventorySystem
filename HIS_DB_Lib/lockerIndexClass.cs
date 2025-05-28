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
    [EnumDescription("locker_index_table")]
    public enum enum_lockerIndex
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("IP,VARCHAR,20,INDEX")]
        IP,
        [Description("Num,VARCHAR,20,NONE")]
        Num,
        [Description("輸入位置,VARCHAR,20,NONE")]
        輸入位置,
        [Description("輸入狀態,VARCHAR,20,NONE")]
        輸入狀態,
        [Description("輸出位置,VARCHAR,20,NONE")]
        輸出位置,
        [Description("輸出狀態,VARCHAR,20,NONE")]
        輸出狀態,
        [Description("同步輸出,VARCHAR,20,NONE")]
        同步輸出,
        [Description("Master_GUID,VARCHAR,50,NONE")]
        Master_GUID,
        [Description("Slave_GUID,VARCHAR,50,NONE")]
        Slave_GUID,
        [Description("Device_GUID,VARCHAR,50,NONE")]
        Device_GUID,
    }
    public class lockerIndexClass
    {
        /// <summary>
        /// 唯一識別碼
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }

        /// <summary>
        /// 裝置 IP 位址
        /// </summary>
        [JsonPropertyName("IP")]
        public string IP { get; set; }

        /// <summary>
        /// 編號
        /// </summary>
        [JsonPropertyName("Num")]
        public string Num { get; set; }

        /// <summary>
        /// 輸入位置編號
        /// </summary>
        [JsonPropertyName("輸入位置")]
        public string 輸入位置 { get; set; }

        /// <summary>
        /// 輸入狀態
        /// </summary>
        [JsonPropertyName("輸入狀態")]
        public string 輸入狀態 { get; set; }

        /// <summary>
        /// 輸出位置編號
        /// </summary>
        [JsonPropertyName("輸出位置")]
        public string 輸出位置 { get; set; }

        /// <summary>
        /// 輸出狀態
        /// </summary>
        [JsonPropertyName("輸出狀態")]
        public string 輸出狀態 { get; set; }

        /// <summary>
        /// 是否為同步輸出模式
        /// </summary>
        [JsonPropertyName("同步輸出")]
        public string 同步輸出 { get; set; }

        /// <summary>
        /// 主控端 GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }

        /// <summary>
        /// 從屬端 GUID
        /// </summary>
        [JsonPropertyName("Slave_GUID")]
        public string Slave_GUID { get; set; }

        /// <summary>
        /// 裝置對應的 GUID
        /// </summary>
        [JsonPropertyName("Device_GUID")]
        public string Device_GUID { get; set; }


    }
}
