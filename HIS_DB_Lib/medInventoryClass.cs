﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace HIS_DB_Lib
{
    [EnumDescription("med_inventory_log")]
    public enum enum_med_inventory_log
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("操作行為,VARCHAR,10,INDEX")]
        操作行為,
        [Description("藥局,VARCHAR,10,INDEX")]
        藥局,
        [Description("護理站,VARCHAR,10,INDEX")]
        護理站,
        [Description("床號,VARCHAR,10,NONE")]
        床號,
        [Description("操作者代號,VARCHAR,20,NONE")]
        操作者代號,
        [Description("操作者姓名,VARCHAR,20,NONE")]
        操作者姓名,
        [Description("操作時間,DATETIME,20,NONE")]
        操作時間,
        [Description("藥碼,VARCHAR,10,NONE")]
        藥碼,
        [Description("藥品名,VARCHAR,150,NONE")]
        藥品名,
        [Description("中文名,VARCHAR,150,NONE")]
        中文名,
        [Description("數量,VARCHAR,10,NONE")]
        數量,
        [Description("劑量,VARCHAR,10,NONE")]
        劑量,
        [Description("單位,VARCHAR,10,NONE")]
        單位,

    }
    [EnumDescription("med_inventory")]
    public enum enum_med_inventory
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥局,VARCHAR,10,INDEX")]
        藥局,
        [Description("護理站,VARCHAR,10,INDEX")]
        護理站,
        [Description("操作者代號,VARCHAR,20,NONE")]
        操作者代號,
        [Description("操作者姓名,VARCHAR,20,NONE")]
        操作者姓名,
        [Description("操作時間,DATETIME,20,NONE")]
        操作時間
    }

    /// <summary>
    /// medInventoryLogClass 資料
    /// </summary>
    public class medInventoryLogClass
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 操作行為
        /// </summary>
        [JsonPropertyName("op_act")]
        public string 操作行為 { get; set; }
        /// <summary>
        /// 藥局
        /// </summary>
        [JsonPropertyName("pharm")]
        public string 藥局 { get; set; }
        /// <summary>
        /// 護理站
        /// </summary>
        [JsonPropertyName("nurnum")]
        public string 護理站 { get; set; }
        /// <summary>
        /// 床號
        /// </summary>
        [JsonPropertyName("bednum")]
        public string 床號 { get; set; }
        /// <summary>
        /// 操作者代號
        /// </summary>
        [JsonPropertyName("op_id")]
        public string 操作者代號 { get; set; }
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
        /// 藥碼
        /// </summary>
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥品名
        /// </summary>
        [JsonPropertyName("name")]
        public string 藥品名 { get; set; }
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
        /// 劑量
        /// </summary>
        [JsonPropertyName("dosage")]
        public string 劑量 { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [JsonPropertyName("dunit")]
        public string 單位 { get; set; }
        public class ICP_By_optime : IComparer<medInventoryLogClass>
        {
            public int Compare(medInventoryLogClass x, medInventoryLogClass y)
            {
                int result = string.Compare(x.操作時間, y.操作時間) * -1;
                return result;
            }
        }
        static public List<medInventoryLogClass> get_logtime_by_master_GUID(string API_Server, string Master_GUID)
        {
            string url = $"{API_Server}/api/med_inventory/get_logtime_by_master_GUID";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(Master_GUID);
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medInventoryLogClass> out_medInventoryLogClass = returnData.Data.ObjToClass<List<medInventoryLogClass>>();
            Console.WriteLine($"{returnData}");
            return out_medInventoryLogClass;
        }
        static public Dictionary<string, List<medInventoryLogClass>> CoverToDictionaryMasterGUID(List<medInventoryLogClass> medInventoryLogClasses)
        {
            Dictionary<string, List<medInventoryLogClass>> dictionary = new Dictionary<string, List<medInventoryLogClass>>();
            if(medInventoryLogClasses != null)
            {
                foreach (var item in medInventoryLogClasses)
                {
                    if (dictionary.TryGetValue(item.Master_GUID, out List<medInventoryLogClass> list))
                    {
                        list.Add(item);
                    }
                    else
                    {
                        dictionary[item.Master_GUID] = new List<medInventoryLogClass> { item };
                    }
                }
            }
            else
            {
                dictionary = new Dictionary<string, List<medInventoryLogClass>>();
            }
            return dictionary;
        }
        static public List<medInventoryLogClass> SortDictByMasterGUID(Dictionary<string, List<medInventoryLogClass>> dict, string MasterGUID)
        {
            if (dict.TryGetValue(MasterGUID, out List<medInventoryLogClass> medInventoryLogClasses))
            {
                return medInventoryLogClasses;
            }
            else
            {
                return new List<medInventoryLogClass>();
            }
        }


    }
    /// <summary>
    ///  medInventoryClass資料
    /// </summary>
    public class medInventoryClass
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 藥局
        /// </summary>
        [JsonPropertyName("pharm")]
        public string 藥局 { get; set; }
        /// <summary>
        /// 護理站
        /// </summary>
        [JsonPropertyName("nurnum")]
        public string 護理站 { get; set; }
        /// <summary>
        /// 操作者代號
        /// </summary>
        [JsonPropertyName("op_id")]
        public string 操作者代號 { get; set; }
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
    }

    public class medInventoryLogResult
    {
        [JsonPropertyName("pharm")]
        public string 藥局 { get; set; }

        [JsonPropertyName("nurnum")]
        public string 護理站 { get; set; }

        [JsonPropertyName("bednum")]
        public string 床號 { get; set; }

        [JsonPropertyName("op_id")]
        public string 操作者代號 { get; set; }

        [JsonPropertyName("dispense_med")]
        public List<medInventoryLogMed> 調劑藥品 { get; set; }
    }
    public class medInventoryLogMed
    {
        [JsonPropertyName("op_time")]
        public string 操作時間 { get; set; }
        [JsonPropertyName("code")]
        public string 藥碼 { get; set; }
        [JsonPropertyName("name")]
        public string 藥品名 { get; set; }
        [JsonPropertyName("cht_name")]
        public string 中文名 { get; set; }
        [JsonPropertyName("qty")]
        public string 數量 { get; set; }
        [JsonPropertyName("dosage")]
        public string 劑量 { get; set; }
        [JsonPropertyName("dunit")]
        public string 單位 { get; set; }
    }
}
