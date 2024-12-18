using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Basic;
using System.Text.Json.Serialization;

namespace HIS_DB_Lib
{
    [EnumDescription("textVision")]
    public enum enum_textVision
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("批次ID,VARCHAR,50,INDEX")]
        批次ID,
        [Description("操作者姓名,VARCHAR,30,INDEX")]
        操作者姓名,
        [Description("操作者ID,VARCHAR,30,INDEX")]
        操作者ID,
        [Description("圖片,LONGTEXT,10,NONE")]
        圖片,
        [Description("操作時間,DATETIME,50,INDEX")]
        操作時間,
        [Description("Log,LONGTEXT,10,NONE")]
        Log,
        [Description("批號,VARCHAR,50,NONE")]
        批號,
        [Description("單號,VARCHAR,50,NONE")]
        單號,
        [Description("藥名,VARCHAR,50,NONE")]
        藥名,
        [Description("中文名,VARCHAR,50,NONE")]
        中文名,
        [Description("數量,VARCHAR,10,NONE")]
        數量,
        [Description("效期,DATETIME,50,NONE")]
        效期,
        [Description("藥品碼,VARCHAR,30,INDEX")]
        藥品碼,
        [Description("批號信心分數,VARCHAR,20,NONE")]
        批號信心分數,
        [Description("效期信心分數,VARCHAR,20,NONE")]
        效期信心分數,
        [Description("單號信心分數,VARCHAR,20,NONE")]
        單號信心分數,
        [Description("藥名信心分數,VARCHAR,20,NONE")]
        藥名信心分數,
        [Description("中文名信心分數,VARCHAR,20,NONE")]
        中文名信心分數,
        [Description("數量信心分數,VARCHAR,20,NONE")]
        數量信心分數,
        [Description("批號位置,VARCHAR,20,NONE")]
        批號位置,
        [Description("效期位置,VARCHAR,20,NONE")]
        效期位置,
        [Description("單號位置,VARCHAR,20,NONE")]
        單號位置,
        [Description("藥名位置,VARCHAR,20,NONE")]
        藥名位置,
        [Description("中文名位置,VARCHAR,20,NONE")]
        中文名位置,
        [Description("數量位置,VARCHAR,20,NONE")]
        數量位置,

    }
    [EnumDescription("med_code_srch")]
    public enum enum_med_code_srch
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("辨識中文名,VARCHAR,50,INDEX")]
        辨識中文名,
        [Description("辨識藥名,VARCHAR,50,INDEX")]
        辨識藥名,
        [Description("藥品碼,VARCHAR,30,NONE")]
        藥品碼,
        [Description("藥名,VARCHAR,50,NONE")]
        藥名, 
        [Description("中文名,VARCHAR,50,NONE")]
        中文名,
        [Description("操作時間,DATETIME,50,NONE")]
        操作時間
    }
    /// <summary>
    /// TextVision 資料
    /// </summary>
    public class textVisionClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 批次ID
        /// </summary>
        [JsonPropertyName("batch_id")]
        public string 批次ID { get; set; }
        /// <summary>
        /// 操作者姓名
        /// </summary>
        [JsonPropertyName("op_name")]
        public string 操作者姓名 { get; set; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        [JsonPropertyName("op_id")]
        public string 操作者ID { get; set; }
        /// <summary>
        /// base64
        /// </summary>
        [JsonPropertyName("base64")]
        public string 圖片 { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [JsonPropertyName("op_time")]
        public string 操作時間 { get; set; }
        /// <summary>
        /// Log
        /// </summary>
        [JsonPropertyName("logs")]
        public string Log { get; set; }
        /// <summary>
        /// 批號
        /// </summary>
        [JsonPropertyName("batch_num")]
        public string 批號 { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [JsonPropertyName("po_num")]
        public string 單號 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("name")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        [JsonPropertyName("cht_name")]
        public string 中文名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("qty")]
        public string 數量 { get; set; }
        /// <summary>
        /// 效期
        /// </summary>
        [JsonPropertyName("expirydate")]
        public string 效期 { get; set; }
        /// <summary>
        /// 藥品碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥品碼 { get; set; }
        /// <summary>
        /// 批號信心分數
        /// </summary>
        [JsonPropertyName("batch_num_conf")]
        public string 批號信心分數 { get; set; }
        /// <summary>
        /// 效期信心分數
        /// </summary>
        [JsonPropertyName("expirydate_conf")]
        public string 效期信心分數 { get; set; }
        /// <summary>
        /// 單號信心分數
        /// </summary>
        [JsonPropertyName("po_num_conf")]
        public string 單號信心分數 { get; set; }
        /// <summary>
        /// 藥名信心分數
        /// </summary>
        [JsonPropertyName("name_conf")]
        public string 藥名信心分數 { get; set; }
        /// <summary>
        /// 中文名信心分數
        /// </summary>
        [JsonPropertyName("cht_name_conf")]
        public string 中文名信心分數 { get; set; }
        /// <summary>
        /// 數量信心分數
        /// </summary>
        [JsonPropertyName("qty_conf")]
        public string 數量信心分數 { get; set; }
        /// <summary>
        /// 批號位置
        /// </summary>
        [JsonPropertyName("batch_num_coord")]
        public string 批號位置 { get; set; }
        /// <summary>
        /// 中文名位置
        /// </summary>
        [JsonPropertyName("cht_name_coord")]
        public string 中文名位置 { get; set; }
        /// <summary>
        /// 效期位置
        /// </summary>
        [JsonPropertyName("expirydate_coord")]
        public string 效期位置 { get; set; }
        /// <summary>
        /// 單號位置
        /// </summary>
        [JsonPropertyName("po_num_coord")]
        public string 單號位置 { get; set; }
        /// <summary>
        /// 數量位置
        /// </summary>
        [JsonPropertyName("qty_coord")]
        public string 數量位置 { get; set; }
        /// <summary>
        /// 藥名位置
        /// </summary>
        [JsonPropertyName("name_coord")]
        public string 藥名位置 { get; set; }
        /// <summary>
        /// 識別位置
        /// </summary>
        [JsonPropertyName("value")]
        public List<positionClass> 識別位置 { get; set; }
        static public returnData analyze(string API_Server, string GUID)
        {
            string url = $"{API_Server}/api/pcmpo/analyze";
            returnData returnData = new returnData();
            returnData.ValueAry[0] = GUID;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            return returnData;
        }
        static public Dictionary<string, List<textVisionClass>> ToDicByBatchID(List<textVisionClass> textVisionClasses)
        {
            Dictionary<string, List<textVisionClass>> dictionary = new Dictionary<string, List<textVisionClass>>();
            foreach(textVisionClass item in textVisionClasses)
            {
                if(dictionary.TryGetValue(item.批次ID, out List<textVisionClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.批次ID] = new List<textVisionClass>() { item };
                }
            }
            return dictionary;
        }
        static public List<textVisionClass> GetValueByBatchID(Dictionary<string, List<textVisionClass>> dict, string batchID)
        {
            if(dict.TryGetValue(batchID,out List<textVisionClass> list))
            {
                return list;
            }
            else
            {
                return new List<textVisionClass>();
            }

        }     

        static public returnData ai_analyze(string API,List<textVisionClass> textVisionClasses)
        {
            //string url = $"{API}/PO_Vision";
            string url = API;

            returnData returnData = new returnData();
            returnData.Data = textVisionClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_AI = json_out.JsonDeserializet<returnData>();
            if (returnData_AI == null) return null;
            //if (returnData_AI.Result == "False") return null;
            //List<textVisionClass> out_textVisionClass = returnData.Data.ObjToClass<List<textVisionClass>>();
            Console.WriteLine($"{returnData}");
            return returnData_AI;
        }
        public enum enum_point_type
        {
            批號位置,
            中文名位置,
            效期位置,
            單號位置,
            數量位置,
            藥名位置,
        }
        public List<System.Drawing.Point> GetPoints(enum_point_type enum_Point_Type)
        {
            return GetPoints(enum_Point_Type.GetEnumName());
        }
        public List<System.Drawing.Point> GetPoints(string enum_Point_Type_temp)
        {
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            string[] strs = null;
            if (enum_Point_Type_temp == enum_point_type.批號位置.GetEnumName())
            {
                strs = 批號位置.Split(';');
            }
            if (enum_Point_Type_temp == enum_point_type.中文名位置.GetEnumName())
            {
                strs = 中文名位置.Split(';');
            }
            if (enum_Point_Type_temp == enum_point_type.效期位置.GetEnumName())
            {
                strs = 效期位置.Split(';');
            }
            if (enum_Point_Type_temp == enum_point_type.單號位置.GetEnumName())
            {
                strs = 單號位置.Split(';');
            }
            if (enum_Point_Type_temp == enum_point_type.數量位置.GetEnumName())
            {
                strs = 數量位置.Split(';');
            }
            if (enum_Point_Type_temp == enum_point_type.藥名位置.GetEnumName())
            {
                strs = 藥名位置.Split(';');
            }
            if (strs.Length == 4)
            {
                for (int i = 0; i < strs.Length; i++)
                {
                    string[] strs_temp = strs[i].Split(',');
                    System.Drawing.Point point = new System.Drawing.Point(strs_temp[0].StringToInt32(), strs_temp[1].StringToInt32());

                    points.Add(point);
                }
            }
            return points;
        }

        //[JsonIgnore]
        //public List<System.Drawing.Point> batch_num_coord_points
        //{
        //    get
        //    {
        //        List<System.Drawing.Point> points = new List<System.Drawing.Point>();
        //        string[] strs = 批號位置.Split(';');
        //        if(strs.Length == 4)
        //        {
        //            for (int i = 0; i < strs.Length; i++)
        //            {
        //                string[] strs_temp = strs[i].Split(',');
        //                System.Drawing.Point point = new System.Drawing.Point(strs_temp[0].StringToInt32(), strs_temp[1].StringToInt32());

        //                points.Add(point);
        //            }
        //        }

        //        return points;
        //    }
        //}
    }
    public class medCodeSrchClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 辨識單GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 辨識中文名
        /// </summary>
        [JsonPropertyName("recog_cht_name")]
        public string 辨識中文名 { get; set; }
        /// <summary>
        /// 辨識藥名
        /// </summary>
        [JsonPropertyName("recog_name")]
        public string 辨識藥名 { get; set; }
        /// <summary>
        /// 藥品碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥品碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("name")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        [JsonPropertyName("cht_name")]
        public string 中文名 { get; set; }
        /// <summary>
        /// 操作時間
        /// </summary>
        [JsonPropertyName("op_time")]
        public string 操作時間 { get; set; }
    }
}
