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
    [EnumDescription("locker_access")]
    public enum enum_lockerAccess
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("ID,VARCHAR,50,INDEX")]
        ID,
        [Description("鎖控名稱,VARCHAR,200,NONE")]
        鎖控名稱,
        [Description("鎖控可開啟時段,VARCHAR,100,NONE")]
        鎖控可開啟時段,
    }
    public class lockerAccessClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 鎖控名稱
        /// </summary>
        [JsonPropertyName("lc_name")]
        public string 鎖控名稱 { get; set; }
        /// <summary>
        /// 鎖控可開啟時段
        /// </summary>
        [JsonPropertyName("lc_time_period")]
        public string 鎖控可開啟時段 { get; set; }
        static public void add(string API_Server, lockerAccessClass lockerAccessClass)
        {
            List<lockerAccessClass> lockerAccessClasses = new List<lockerAccessClass>();
            lockerAccessClasses.Add(lockerAccessClass);

            add(API_Server, lockerAccessClasses);
        }
        static public void add(string API_Server, List<lockerAccessClass> lockerAccessClasses)
        {
            string url = $"{API_Server}/api/lockerAccess/add";

            returnData returnData = new returnData();
            returnData.Data = lockerAccessClasses;
            string json_in = returnData.JsonSerializationt();
            //Console.WriteLine($"[api/lockerAccess/add] json_in : {json_in}");
            string json_out = Net.WEBApiPostJson(url, json_in);
            //Console.WriteLine($"[api/lockerAccess/add] json_out : {json_out}");
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return;
            }

        }
        static public lockerAccessClass get_by_id_and_lcname(string API_Server,string id , string lc_name)
        {
            string url = $"{API_Server}/api/lockerAccess/get_by_id_and_lcname";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(id);
            returnData.ValueAry.Add(lc_name);

            string json_in = returnData.JsonSerializationt();
            //Console.WriteLine($"[api/lockerAccess/get_by_id_and_lcname] json_in : {json_in}");
            string json_out = Net.WEBApiPostJson(url, json_in);
            //Console.WriteLine($"[api/lockerAccess/get_by_id_and_lcname] json_out : {json_out}");
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }

            return returnData_out.Data.ObjToClass<lockerAccessClass>();
        }


        /// <summary>
        /// 將時間時段轉成字串
        /// </summary>
        /// <param name="startTime">開始時間</param>
        /// <param name="endTime">結束時間</param>
        /// <returns>格式化的時段字串 (HH:mm:ss-HH:mm:ss)</returns>
        public static string TimePeriodToString(TimeSpan startTime, TimeSpan endTime)
        {
            return $"{startTime:hh\\:mm\\:ss}-{endTime:hh\\:mm\\:ss}";
        }
        /// <summary>
        /// 將字串轉為時間時段
        /// </summary>
        /// <param name="timePeriodStr">時段字串 (HH:mm:ss-HH:mm:ss)</param>
        /// <param name="startTime">輸出的開始時間</param>
        /// <param name="endTime">輸出的結束時間</param>
        /// <returns>成功解析則返回 true，否則返回 false</returns>
        public static bool StringToTimePeriod(string timePeriodStr, out TimeSpan startTime, out TimeSpan endTime)
        {
            startTime = TimeSpan.Zero;
            endTime = TimeSpan.Zero;

            try
            {
                var times = timePeriodStr.Split('-');
                if (times.Length == 2)
                {
                    startTime = TimeSpan.Parse(times[0]);
                    endTime = TimeSpan.Parse(times[1]);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 檢查指定時間是否在時間時段內
        /// </summary>
        /// <param name="time">要檢查的時間</param>
        /// <param name="startTime">開始時間</param>
        /// <param name="endTime">結束時間</param>
        /// <returns>若時間在範圍內則返回 true，否則返回 false</returns>
        public static bool IsTimeInPeriod(TimeSpan time, TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime <= endTime)
            {
                // 時段不跨午夜
                return time >= startTime && time <= endTime;
            }
            else
            {
                // 時段跨午夜
                return time >= startTime || time <= endTime;
            }
        }
        /// <summary>
        /// 檢查指定時間是否在時間時段內（以時間區段字串為參數）
        /// </summary>
        /// <param name="time">要檢查的時間</param>
        /// <param name="timePeriodStr">時間區段字串 (HH:mm:ss-HH:mm:ss)</param>
        /// <returns>若時間在範圍內則返回 true，否則返回 false</returns>
        public static bool IsTimeInPeriod(TimeSpan time, string timePeriodStr)
        {
            if (StringToTimePeriod(timePeriodStr, out TimeSpan startTime, out TimeSpan endTime))
            {
                if (startTime <= endTime)
                {
                    // 時段不跨午夜
                    return time >= startTime && time <= endTime;
                }
                else
                {
                    // 時段跨午夜
                    return time >= startTime || time <= endTime;
                }
            }
            else
            {
                throw new ArgumentException("時間區段字串格式不正確，無法解析！");
            }
        }
        /// <summary>
        /// 檢查時間區段字串是否合法
        /// </summary>
        /// <param name="timePeriodStr">時間區段字串 (HH:mm:ss-HH:mm:ss)</param>
        /// <returns>合法返回 true，不合法返回 false</returns>
        public static bool IsValidTimePeriodString(string timePeriodStr)
        {
            if (string.IsNullOrWhiteSpace(timePeriodStr))
                return false;

            var times = timePeriodStr.Split('-');
            if (times.Length != 2)
                return false;

            try
            {
                // 嘗試解析每個時間片段
                TimeSpan startTime = TimeSpan.Parse(times[0]);
                TimeSpan endTime = TimeSpan.Parse(times[1]);
                return true; // 能解析則視為合法
            }
            catch
            {
                return false; // 任一時間片段解析失敗則不合法
            }
        }

    }


}
