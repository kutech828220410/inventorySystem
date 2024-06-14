using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Basic;
using System.Text.Json.Serialization;
using HIS_DB_Lib;

namespace LoggerClassLibrary
{
    [EnumDescription("logger")]
    public enum enum_logger
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("網址,VARCHAR,250,NONE")]
        網址,
        [Description("項目,VARCHAR,100,INDEX")]
        項目,
        [Description("操作類型,VARCHAR,50,INDEX")]
        操作類型,
        [Description("操作者ID,VARCHAR,100,NONE")]
        操作者ID,
        [Description("操作者姓名,VARCHAR,100,INDEX")]
        操作者姓名,
        [Description("操作時間,DATETIME,50,INDEX")]
        操作時間,
        [Description("回傳結果,VARCHAR,500,NONE")]
        回傳結果
    }
    /// <summary>
    /// logger 資料
    /// </summary>
    public class loggerClass
    {

        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 網址
        /// </summary>
        [JsonPropertyName("url")]
        public string 網址 { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        [JsonPropertyName("item")]
        public string 項目 { get; set; }
        /// <summary>
        /// 操作類型
        /// </summary>
        [JsonPropertyName("op_type")]
        public string 操作類型 { get; set; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        [JsonPropertyName("op_id")]
        public string 操作者ID { get; set; }
        /// <summary>
        /// 操作者姓名
        /// </summary>
        [JsonPropertyName("op_name")]
        public string 操作者姓名 { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [JsonPropertyName("op_time")]
        public string 操作時間 { get; set; }
        /// <summary>
        /// 時間
        /// </summary>
        [JsonPropertyName("result")]
        public string 回傳結果 { get; set; }


        public class ICP_By_OP_Time : IComparer<loggerClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(loggerClass x, loggerClass y)
            {
                return x.操作時間.CompareTo(y.操作時間);
            }
        }
        static public SQLUI.Table Init(string API_Server, string ServerName, string ServerType)
        {
            string url = $"{API_Server}/api/Logger/init";

            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        static public List<loggerClass> get_all(string API_Server, string serverName, string serverType)
        {
            string url = $"{API_Server}/api/Logger/get_all";

            returnData returnData = new returnData();
            returnData.ServerName = serverName;
            returnData.ServerType = serverType;


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"[{returnData_out.Method}]:{returnData_out.Result}");
            List<loggerClass> loggerClasses = returnData_out.Data.ObjToClass<List<loggerClass>>();
            return loggerClasses;
        }
    }       
}
