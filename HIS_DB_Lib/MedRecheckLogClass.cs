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
    public enum enum_medRecheckLog_State
    {
        未排除,
        已排除
    }
    [EnumDescription("med_recheck_log_new")] 
    public enum enum_medRecheckLog
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("發生類別,VARCHAR,50,INDEX")]
        發生類別,
        [Description("藥碼,VARCHAR,20,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("庫存值,VARCHAR,20,NONE")]
        庫存值,
        [Description("盤點值,VARCHAR,20,NONE")]
        盤點值,
        [Description("差異值,VARCHAR,20,NONE")]
        差異值,
        [Description("批號,VARCHAR,200,NONE")]
        批號,
        [Description("效期,VARCHAR,200,NONE")]
        效期,
        [Description("醫令_GUID,VARCHAR,50,INDEX")]
        醫令_GUID,
        [Description("交易紀錄_GUID,VARCHAR,50,INDEX")]
        交易紀錄_GUID,
        [Description("盤點藥師1,VARCHAR,50,INDEX")]
        盤點藥師1,
        [Description("盤點藥師ID1,VARCHAR,50,INDEX")]
        盤點藥師ID1,
        [Description("盤點藥師2,VARCHAR,50,INDEX")]
        盤點藥師2,
        [Description("盤點藥師ID2,VARCHAR,50,INDEX")]
        盤點藥師ID2,
        [Description("排除藥師,VARCHAR,50,INDEX")]
        排除藥師,
        [Description("發生時間,DATETIME,300,INDEX")]
        發生時間,
        [Description("排除時間,DATETIME,300,INDEX")]
        排除時間,
        [Description("異常原因,VARCHAR,500,NONE")]
        異常原因,
        [Description("狀態,VARCHAR,20,INDEX")]
        狀態,
    }
    /// <summary>
    /// 表示藥品覆盤日志的數據類別。
    /// </summary>
    public class medRecheckLogClass
    {
        /// <summary>
        /// 獨特的標識符。
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 發生類別。
        /// </summary>
        [JsonPropertyName("ICDT_TYPE")]
        public string 發生類別 { get; set; }
        /// <summary>
        /// 藥品的代碼。
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥品的名稱。
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 庫存值。
        /// </summary>
        [JsonPropertyName("INV_QTY")]
        public string 庫存值 { get; set; }
        /// <summary>
        /// 盤點值。
        /// </summary>
        [JsonPropertyName("CHECK_QTY")]
        public string 盤點值 { get; set; }
        /// <summary>
        /// 差異值。
        /// </summary>
        [JsonPropertyName("UPDATE_QTY")]
        public string 差異值 { get; set; }
        /// <summary>
        /// 批號。
        /// </summary>
        [JsonPropertyName("LOT")]
        public string 批號 { get; set; }
        /// <summary>
        /// 效期。
        /// </summary>
        [JsonPropertyName("VAL")]
        public string 效期 { get; set; }
        /// <summary>
        /// 醫令的唯一標識符。
        /// </summary>
        [JsonPropertyName("ORDER_GUID")]
        public string 醫令_GUID { get; set; }
        /// <summary>
        /// 交易紀錄的唯一標識符。
        /// </summary>
        [JsonPropertyName("TRADING_GUID")]
        public string 交易紀錄_GUID { get; set; }
        /// <summary>
        /// 盤點藥師。
        /// </summary>
        [JsonPropertyName("CHECK_OP1")]
        public string 盤點藥師1 { get; set; }
        /// <summary>
        /// 盤點藥師ID2。
        /// </summary>
        [JsonPropertyName("CHECK_OP_ID1")]
        public string 盤點藥師ID1 { get; set; }
        /// <summary>
        /// 盤點藥師。
        /// </summary>
        [JsonPropertyName("CHECK_OP2")]
        public string 盤點藥師2 { get; set; }
        /// <summary>
        /// 盤點藥師ID2。
        /// </summary>
        [JsonPropertyName("CHECK_OP_ID2")]
        public string 盤點藥師ID2 { get; set; }
        /// <summary>
        /// 排除藥師。
        /// </summary>
        [JsonPropertyName("TROUBLE_OP")]
        public string 排除藥師 { get; set; }
        /// <summary>
        /// 發生時間。
        /// </summary>
        [JsonPropertyName("occurrence_time")]
        public string 發生時間 { get; set; }
        /// <summary>
        /// 排除時間。
        /// </summary>
        [JsonPropertyName("troubleshooting_time")]
        public string 排除時間 { get; set; }
        /// <summary>
        /// 異常原因。
        /// </summary>
        [JsonPropertyName("ALRM_REASON")]
        public string 異常原因 { get; set; }
        /// <summary>
        /// 狀態。
        /// </summary>
        [JsonPropertyName("state")]
        public string 狀態 { get; set; }



        static public SQLUI.Table init(string API_Server, string ServerName, string ServerType)
        {
            string url = $"{API_Server}/api/medRecheckLog/init";

            returnData returnData = new returnData();
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        static public void add(string API_Server, string ServerName, string ServerType, medRecheckLogClass medRecheckLogClass)
        {
            List<medRecheckLogClass> medRecheckLogClasses = new List<medRecheckLogClass>();
            medRecheckLogClasses.Add(medRecheckLogClass);
            add(API_Server, ServerName, ServerType, medRecheckLogClasses);
        }
        static public void add(string API_Server, string ServerName, string ServerType, List<medRecheckLogClass> medRecheckLogClasses)
        {

            string url = $"{API_Server}/api/medRecheckLog/add";
            returnData returnData = new returnData();
            returnData.Data = medRecheckLogClasses;
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return;
            }
            if (returnData_out.Data == null)
            {
                return;
            }
            Console.WriteLine($"{returnData_out}");

        }

        static public void replace_by_guid(string API_Server, string ServerName, string ServerType, medRecheckLogClass medRecheckLogClass)
        {
            List<medRecheckLogClass> medRecheckLogClasses = new List<medRecheckLogClass>();
            medRecheckLogClasses.Add(medRecheckLogClass);
            replace_by_guid(API_Server, ServerName, ServerType, medRecheckLogClasses);
        }
        static public void replace_by_guid(string API_Server, string ServerName, string ServerType, List<medRecheckLogClass> medRecheckLogClasses)
        {

            string url = $"{API_Server}/api/medRecheckLog/replace_by_guid";
            returnData returnData = new returnData();
            returnData.Data = medRecheckLogClasses;
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return;
            }
            if (returnData_out.Data == null)
            {
                return;
            }
            Console.WriteLine($"{returnData_out}");

        }
        static public List<medRecheckLogClass> get_by_occurrence_time_st_end(string API_Server, string ServerName, string ServerType, DateTime dateTime_start, DateTime dateTime_end)
        {

            string url = $"{API_Server}/api/medRecheckLog/get_by_occurrence_time_st_end";

            returnData returnData = new returnData();
            returnData.Value = $"{dateTime_start.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;

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
            Console.WriteLine($"{returnData_out}");

            List<medRecheckLogClass> medRecheckLogClasses = returnData_out.Data.ObjToClass<List<medRecheckLogClass>>();
            medRecheckLogClasses.Sort(new ICP_By_occurrence_time());
            return medRecheckLogClasses;
        }
        static public List<medRecheckLogClass> get_by_troubleshooting_time_st_end(string API_Server, string ServerName, string ServerType, DateTime dateTime_start, DateTime dateTime_end)
        {

            string url = $"{API_Server}/api/medRecheckLog/get_by_troubleshooting_time_st_end";

            returnData returnData = new returnData();
            returnData.Value = $"{dateTime_start.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;
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
            Console.WriteLine($"{returnData_out}");

            List<medRecheckLogClass> medRecheckLogClasses = returnData_out.Data.ObjToClass<List<medRecheckLogClass>>();
            medRecheckLogClasses.Sort(new ICP_By_troubleshooting_time());
            return medRecheckLogClasses;
        }
        static public List<medRecheckLogClass> get_ng_state_data(string API_Server, string ServerName, string ServerType)
        {

            string url = $"{API_Server}/api/medRecheckLog/get_ng_state_data";

            returnData returnData = new returnData();
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;
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
            //Console.WriteLine($"{returnData_out}");

            List<medRecheckLogClass> medRecheckLogClasses = returnData_out.Data.ObjToClass<List<medRecheckLogClass>>();
            medRecheckLogClasses.Sort(new ICP_By_occurrence_time());
            return medRecheckLogClasses;
        }
        static public int get_unresolved_qty_by_code(string API_Server, string ServerName, string ServerType, string code)
        {

            string url = $"{API_Server}/api/medRecheckLog/get_unresolved_qty_by_code";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(code);
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return 0;
            }
            if (returnData_out.Data == null)
            {
                return 0;
            }
            Console.WriteLine($"{returnData_out}");

            medRecheckLogClass medRecheckLogClass = returnData_out.Data.ObjToClass<medRecheckLogClass>();
            return medRecheckLogClass.差異值.StringToInt32();
        }
        static public void set_unresolved_data_by_code(string API_Server, string ServerName, string ServerType, string code , string TROUBLE_OP)
        {

            string url = $"{API_Server}/api/medRecheckLog/set_unresolved_data_by_code";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(code);
            returnData.ValueAry.Add(TROUBLE_OP);
            returnData.ServerType = ServerType;
            returnData.ServerName = ServerName;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return;
            }
            if (returnData_out.Data == null)
            {
                return;
            }
            Console.WriteLine($"{returnData_out}");

        }

        public class ICP_By_occurrence_time : IComparer<medRecheckLogClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(medRecheckLogClass x, medRecheckLogClass y)
            {
                DateTime temp0 = x.發生時間.StringToDateTime();
                DateTime temp1 = y.發生時間.StringToDateTime();
                int compare = temp0.CompareTo(temp1);
                return compare;

            }
        }
        public class ICP_By_troubleshooting_time : IComparer<medRecheckLogClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(medRecheckLogClass x, medRecheckLogClass y)
            {
                DateTime temp0 = x.排除時間.StringToDateTime();
                DateTime temp1 = y.排除時間.StringToDateTime();
                int compare = temp0.CompareTo(temp1);
                return compare;

            }
        }
    }


}
