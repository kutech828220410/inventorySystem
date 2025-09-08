using Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HIS_DB_Lib
{
    public enum enum_bbs_ReportLevel
    {
        [Description("Normal")]
        Normal,       // 一般
        [Description("Important")]
        Important,    // 重要
        [Description("Critical")]
        Critical      // 緊急
    }
    [EnumDescription("bbs")]
    public enum enum_bbs
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("主旨,VARCHAR,100,NONE")]
        主旨,
        [Description("內容,VARCHAR,200,NONE")]
        內容,
        [Description("重要程度,VARCHAR,20,NONE")]
        重要程度,
        [Description("建立人員科別,VARCHAR,20,NONE")]
        建立人員科別,
        [Description("建立人員姓名,VARCHAR,20,NONE")]
        建立人員姓名,
        [Description("建立時間,DATETIME,20,NONE")]
        建立時間,
        [Description("公告開始時間,DATETIME,20,INDEX")]
        公告開始時間,
        [Description("公告結束時間,DATETIME,20,INDEX")]
        公告結束時間,
    }
    public class bbsClass
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }

        /// <summary>
        /// 主旨
        /// </summary>
        [JsonPropertyName("title")]
        public string 主旨 { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [JsonPropertyName("content")]
        public string 內容 { get; set; }

        /// <summary>
        /// 重要程度
        /// </summary>
        [JsonPropertyName("priority")]
        public string 重要程度 { get; set; }

        /// <summary>
        /// 建立人員科別
        /// </summary>
        [JsonPropertyName("creator_dept")]
        public string 建立人員科別 { get; set; }

        /// <summary>
        /// 建立人員姓名
        /// </summary>
        [JsonPropertyName("creator_name")]
        public string 建立人員姓名 { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [JsonPropertyName("created_time")]
        public string 建立時間 { get; set; }

        /// <summary>
        /// 公告開始時間
        /// </summary>
        [JsonPropertyName("start_time")]
        public string 公告開始時間 { get; set; }

        /// <summary>
        /// 公告結束時間
        /// </summary>
        [JsonPropertyName("end_time")]
        public string 公告結束時間 { get; set; }

        public class ICP_By_ct_time : IComparer<bbsClass>
        {
            public int Compare(bbsClass x, bbsClass y)
            {
                return x.建立時間.CompareTo(y.建立時間) * -1;
            }
        }
    }
}
