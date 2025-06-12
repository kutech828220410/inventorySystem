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
    public enum enum_notifyException
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("類別,VARCHAR,50,INDEX")]
        類別,
        [Description("內容,VARCHAR,50,NONE")]
        內容,
        [Description("發生時間,VARCHAR,50,NONE")]
        發生時間,
    }
    public class notifyExceptionClass
    {
        /// <summary>
        /// 唯一KEY.
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 唯一KEY.
        /// </summary>
        [JsonPropertyName("type")]
        public string 類別 { get; set; }
        /// <summary>
        /// 唯一KEY.
        /// </summary>
        [JsonPropertyName("content")]
        public string 內容 { get; set; }
        /// <summary>
        /// 唯一KEY.
        /// </summary>
        [JsonPropertyName("occur_time")]
        public string 發生時間 { get; set; }
    }
    public static class notifyExceptionMethod
    {
        public static List<notifyExceptionClass> SortByOccurTimeDesc(this List<notifyExceptionClass> list)
        {
            var sortedList = list.OrderByDescending(item =>
            {
                DateTime dt;
                if (DateTime.TryParse(item.發生時間, out dt))
                    return dt;
                else
                    return DateTime.MinValue; // 無法解析的放最後
            }).ToList();

            return sortedList;
        }
    }
}
