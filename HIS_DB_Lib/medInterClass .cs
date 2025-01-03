﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HIS_DB_Lib
{
    [EnumDescription("med_inter")]
    public enum enum_med_inter
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("ATC,VARCHAR,10,NONE")]
        ATC,
        [Description("非專利藥品名,VARCHAR,100,NONE")]
        非專利藥品名,
        [Description("植入,VARCHAR,20,NONE")]
        植入,
        [Description("吸入,VARCHAR,20,NONE")]
        吸入,
        [Description("滴入,VARCHAR,20,NONE")]
        滴入,
        [Description("鼻用,VARCHAR,20,NONE")]
        鼻用,
        [Description("口服,VARCHAR,20,NONE")]
        口服,
        [Description("非經消化道,VARCHAR,20,NONE")]
        非經消化道,
        [Description("直腸用,VARCHAR,20,NONE")]
        直腸用,
        [Description("舌下口腔口腔黏膜,VARCHAR,20,NONE")]
        舌下口腔口腔黏膜,
        [Description("經皮膚,VARCHAR,20,NONE")]
        經皮膚,
        [Description("陰道用,VARCHAR,20,NONE")]
        陰道用,


    }
    [EnumDescription("med_inter_dtl")]
    public enum enum_med_inter_dtl
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥名1,VARCHAR,100,NONE")]
        藥名1,
        [Description("ATC1,VARCHAR,10,NONE")]
        ATC1,
        [Description("藥名2,VARCHAR,100,NONE")]
        藥名2,
        [Description("ATC2,VARCHAR,10,NONE")]
        ATC2,
        [Description("網址1,VARCHAR,250,NONE")]
        網址1,
        [Description("網址2,VARCHAR,250,NONE")]
        網址2,        

    }
    /// <summary>
    /// medInterClass 資料
    /// </summary>
    public class medInterClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// ATC
        /// </summary>
        [JsonPropertyName("ATC")]
        public string ATC { get; set; }
        /// <summary>
        /// 非專利藥品名
        /// </summary>
        [JsonPropertyName("name")]
        public string 非專利藥品名 { get; set; }
        /// <summary>
        /// 植入
        /// </summary>
        [JsonPropertyName("impl")]
        public string 植入 { get; set; }
        /// <summary>
        /// 吸入
        /// </summary>
        [JsonPropertyName("inh")]
        public string 吸入 { get; set; }
        /// <summary>
        /// 滴入
        /// </summary>
        [JsonPropertyName("inst")]
        public string 滴入 { get; set; }
        /// <summary>
        /// 鼻用
        /// </summary>
        [JsonPropertyName("nas")]
        public string 鼻用 { get; set; }
        /// <summary>
        /// 口服
        /// </summary>
        [JsonPropertyName("oral")]
        public string 口服 { get; set; }
        /// <summary>
        /// 非經消化道
        /// </summary>
        [JsonPropertyName("par")]
        public string 非經消化道 { get; set; }
        /// <summary>
        /// 直腸用
        /// </summary>
        [JsonPropertyName("rect")]
        public string 直腸用 { get; set; }
        /// <summary>
        /// 舌下口腔口腔黏膜
        /// </summary>
        [JsonPropertyName("sub_buc_oro")]
        public string 舌下口腔口腔黏膜 { get; set; }
        /// <summary>
        /// 經皮膚
        /// </summary>
        [JsonPropertyName("trans")]
        public string 經皮膚 { get; set; }
        /// <summary>
        /// 陰道用
        /// </summary>
        [JsonPropertyName("vag")]
        public string 陰道用 { get; set; }

        static public List<medInterClass> update_med_inter(string API, List<medInterClass> medInterClasses)
        {
            string url = $"{API}/api/med_interaction/update_med_inter";

            returnData returnData = new returnData();
            returnData.Data = medInterClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medInterClass> out_medInterClass = returnData.Data.ObjToClass<List<medInterClass>>();
            Console.WriteLine($"{returnData}");
            return out_medInterClass;
        }
        
        
    }
    /// <summary>
    /// medInterDtlClass 資料
    /// </summary>
    public class medInterDtlClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 藥名1
        /// </summary>
        [JsonPropertyName("name_1")]
        public string 藥名1 { get; set; }
        /// <summary>
        /// ATC_1
        /// </summary>
        [JsonPropertyName("ATC_1")]
        public string ATC1 { get; set; }
        /// <summary>
        /// 藥名2
        /// </summary>
        [JsonPropertyName("name_2")]
        public string 藥名2 { get; set; }
        /// <summary>
        /// ATC_2
        /// </summary>
        [JsonPropertyName("ATC_2")]
        public string ATC2 { get; set; }
        /// <summary>
        /// 網址1
        /// </summary>
        [JsonPropertyName("medspace")]
        public string 網址1 { get; set; }
        /// <summary>
        /// 網址2
        /// </summary>
        [JsonPropertyName("drugcom")]
        public string 網址2 { get; set; }

    }
}
