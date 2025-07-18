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
    public enum enum_bed_status_string
    {
        已佔床,
        已出院,
    }
    [EnumDescription("patient_info")]
    public enum enum_patient_info
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("PRI_KEY,VARCHAR,200,INDEX")]
        PRI_KEY,
        [Description("更新時間,DATETIME,10,NONE")]
        更新時間,
        [Description("調劑時間,DATETIME,10,NONE")]
        調劑時間,
        [Description("異動,VARCHAR,10,NONE")]
        異動,
        [Description("姓名,VARCHAR,50,NONE")]
        姓名,
        [Description("住院號,VARCHAR,50,INDEX")]
        住院號,
        [Description("病歷號,VARCHAR,100,NONE")]
        病歷號,
        [Description("藥局,VARCHAR,10,INDEX")]
        藥局,
        [Description("護理站,VARCHAR,10,NONE")]
        護理站,
        [Description("床號,VARCHAR,10,NONE")]
        床號,
        [Description("占床狀態,VARCHAR,10,NONE")]
        占床狀態,
        [Description("調劑狀態,VARCHAR,10,NONE")]
        調劑狀態,
        [Description("處方異動狀態,VARCHAR,10,NONE")]
        處方異動狀態,
        [Description("性別,VARCHAR,10,NONE")]
        性別,
        [Description("出生日期,VARCHAR,10,NONE")]
        出生日期,
        [Description("年齡,VARCHAR,30,NONE")]
        年齡,
        [Description("科別,VARCHAR,20,NONE")]
        科別,
        [Description("財務,VARCHAR,10,NONE")]
        財務,
        [Description("入院日期,DATETIME,30,INDEX")]
        入院日期,
        [Description("主治醫師代碼,VARCHAR,10,NONE")]
        主治醫師代碼,
        [Description("住院醫師代碼,VARCHAR,10,NONE")]
        住院醫師代碼,
        [Description("主治醫師,VARCHAR,10,NONE")]
        主治醫師,
        [Description("住院醫師,VARCHAR,10,NONE")]
        住院醫師,
        [Description("身高,VARCHAR,10,NONE")]
        身高,
        [Description("體重,VARCHAR,10,NONE")]
        體重,
        [Description("體表面積,VARCHAR,10,NONE")]
        體表面積,
        [Description("疾病代碼,VARCHAR,500,NONE")]
        疾病代碼,
        [Description("疾病說明,VARCHAR,400,NONE")]
        疾病說明,
        [Description("鼻胃管使用狀況,VARCHAR,10,NONE")]
        鼻胃管使用狀況,
        [Description("其他管路使用狀況,VARCHAR,10,NONE")]
        其他管路使用狀況,
        [Description("過敏史,VARCHAR,10,NONE")]
        過敏史,
        [Description("白蛋白,VARCHAR,10,NONE")]
        白蛋白,
        [Description("肌酸酐,VARCHAR,10,NONE")]
        肌酸酐,
        [Description("估算腎小球過濾率,VARCHAR,10,NONE")]
        估算腎小球過濾率,
        [Description("丙氨酸氨基轉移酶,VARCHAR,10,NONE")]
        丙氨酸氨基轉移酶,
        [Description("鉀離子,VARCHAR,10,NONE")]
        鉀離子,
        [Description("鈣離子,VARCHAR,10,NONE")]
        鈣離子,
        [Description("總膽紅素,VARCHAR,10,NONE")]
        總膽紅素,
        [Description("鈉離子,VARCHAR,10,NONE")]
        鈉離子,
        [Description("白血球,VARCHAR,10,NONE")]
        白血球,
        [Description("血紅素,VARCHAR,10,NONE")]
        血紅素,
        [Description("血小板,VARCHAR,10,NONE")]
        血小板,
        [Description("國際標準化比率,VARCHAR,10,NONE")]
        國際標準化比率,
        [Description("檢驗數值異常,VARCHAR,150,NONE")]
        檢驗數值異常,
        [Description("覆核狀態,VARCHAR,10,NONE")]
        覆核狀態

    }
    /// <summary>
    /// 住院藥車病人資訊
    /// </summary>
    public class patientInfoClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// PRI_KEY
        /// </summary>
        [JsonPropertyName("PRI_KEY")]
        public string PRI_KEY { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        [JsonPropertyName("update_time")]
        public string 更新時間 { get; set; }
        /// <summary>
        /// 調劑時間
        /// </summary>
        [JsonPropertyName("dispens_time")]
        public string 調劑時間 { get; set; }
        /// <summary>
        /// 異動
        /// </summary>
        [JsonPropertyName("change")]
        public string 異動 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonPropertyName("pnamec")]
        public string 姓名 { get; set; }
        /// <summary>
        /// 住院號
        /// </summary>
        [JsonPropertyName("caseno")]
        public string 住院號 { get; set; }
        /// <summary>
        /// 病歷號
        /// </summary>
        [JsonPropertyName("histno")]
        public string 病歷號 { get; set; }
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
        /// 占床狀態
        /// </summary>
        [JsonPropertyName("bed_status")]
        public string 占床狀態 { get; set; }
        /// <summary>
        /// 調劑狀態
        /// </summary>
        [JsonPropertyName("dispens_status")]
        public string 調劑狀態 { get; set; }
        /// <summary>
        /// 處方異動狀態
        /// </summary>
        [JsonPropertyName("cpoe_change_status")]
        public string 處方異動狀態 { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        [JsonPropertyName("hsexc")]
        public string 性別 { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonPropertyName("birthday")]
        public string 出生日期 { get; set; }
        /// <summary>
        /// 年齡
        /// </summary>
        [JsonPropertyName("age")]
        public string 年齡 { get; set; }
        /// <summary>
        /// 科別
        /// </summary>
        [JsonPropertyName("psectc")]
        public string 科別 { get; set; }
        /// <summary>
        /// 財務
        /// </summary>
        [JsonPropertyName("pfinc")]
        public string 財務 { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        [JsonPropertyName("padmdt")]
        public string 入院日期 { get; set; }
        /// <summary>
        /// 主治醫師代碼
        /// </summary>
        [JsonPropertyName("pvsdno")]
        public string 主治醫師代碼 { get; set; }
        /// <summary>
        /// 住院醫師代碼
        /// </summary>
        [JsonPropertyName("prdno")]
        public string 住院醫師代碼 { get; set; }
        /// <summary>
        /// 診所名稱
        /// </summary>
        [JsonPropertyName("pvsdno_name")]
        public string 主治醫師 { get; set; }
        /// <summary>
        /// 醫生姓名
        /// </summary>
        [JsonPropertyName("prdno_name")]
        public string 住院醫師 { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        [JsonPropertyName("hight")]
        public string 身高 { get; set; }
        /// <summary>
        /// 體重
        /// </summary>
        [JsonPropertyName("weight")]
        public string 體重 { get; set; }
        /// <summary>
        /// 體表面積
        /// </summary>
        [JsonPropertyName("pbbsa")]
        public string 體表面積 { get; set; }
        /// <summary>
        /// 疾病代碼
        /// </summary>
        [JsonPropertyName("disease_code")]
        public string 疾病代碼 { get; set; }
        /// <summary>
        /// 疾病說明
        /// </summary>
        [JsonPropertyName("disease_descrip")]
        public string 疾病說明 { get; set; }
        /// <summary>
        /// 鼻胃管使用狀況
        /// </summary>
        [JsonPropertyName("ngtube")]
        public string 鼻胃管使用狀況 { get; set; }
        /// <summary>
        /// 其他管路使用狀況
        /// </summary>
        [JsonPropertyName("tube")]
        public string 其他管路使用狀況 { get; set; }
        /// <summary>
        /// 過敏史
        /// </summary>
        [JsonPropertyName("hallergy")]
        public string 過敏史 { get; set; }
        /// <summary>
        /// 白蛋白
        /// </summary>
        [JsonPropertyName("alb")]
        public string 白蛋白 { get; set; }
        /// <summary>
        /// 肌酸酐
        /// </summary>
        [JsonPropertyName("scr")]
        public string 肌酸酐 { get; set; }
        /// <summary>
        /// 估算腎小球過濾率
        /// </summary>
        [JsonPropertyName("egfr")]
        public string 估算腎小球過濾率 { get; set; }
        /// <summary>
        /// 丙氨酸氨基轉移酶
        /// </summary>
        [JsonPropertyName("alt")]
        public string 丙氨酸氨基轉移酶 { get; set; }
        /// <summary>
        /// 鉀離子
        /// </summary>
        [JsonPropertyName("k")]
        public string 鉀離子 { get; set; }
        /// <summary>
        /// 鈣離子
        /// </summary>
        [JsonPropertyName("ca")]
        public string 鈣離子 { get; set; }
        /// <summary>
        /// 總膽紅素
        /// </summary>
        [JsonPropertyName("tb")]
        public string 總膽紅素 { get; set; }
        /// <summary>
        /// 鈉離子
        /// </summary>
        [JsonPropertyName("na")]
        public string 鈉離子 { get; set; }
        /// <summary>
        /// 白血球
        /// </summary>
        [JsonPropertyName("wbc")]
        public string 白血球 { get; set; }
        /// <summary>
        /// 血紅素
        /// </summary>
        [JsonPropertyName("hgb")]
        public string 血紅素 { get; set; }
        /// <summary>
        /// 血小板
        /// </summary>
        [JsonPropertyName("plt")]
        public string 血小板 { get; set; }
        /// <summary>
        /// 國際標準化比率
        /// </summary>
        [JsonPropertyName("inr")]
        public string 國際標準化比率 { get; set; }
        /// <summary>
        /// 檢驗數值異常
        /// </summary>
        [JsonPropertyName("abnormal")]
        public string 檢驗數值異常 { get; set; }
        /// <summary>
        /// 覆核狀態
        /// </summary>
        [JsonPropertyName("check_status")]
        public string 覆核狀態 { get; set; }
        /// <summary>
        /// 診斷病名
        /// </summary>
        [JsonPropertyName("disease")]
        public List<diseaseOut> 診斷病名 { get; set; }
        /// <summary>
        /// 處方
        /// </summary>
        [JsonPropertyName("cpoe")]
        public List<medCpoeClass> 處方 { get; set; }
        /// <summary>
        /// 處方異動
        /// </summary>
        [JsonPropertyName("cpoe_change")]
        public List<medCpoeRecClass> 處方異動 { get; set; }
        [JsonPropertyName("bedStatus")]
        public bedStatusClass 轉床狀態 { get; set; }

        public class ICP_By_cart : IComparer<patientInfoClass>
        {
            public int Compare(patientInfoClass x, patientInfoClass y)
            {
                int result = string.Compare(x.護理站, y.護理站);

                // 如果 排序 相同，則依照 藥名 排序
                if (result == 0)
                {
                    result = string.Compare(x.床號, y.床號);
                }

                return result;
            }
        }
        public class ICP_By_bedNum : IComparer<patientInfoClass>
        {
            public int Compare(patientInfoClass x, patientInfoClass y)
            {
                //x.床號 = x.床號.Split('-')[0].Trim();
                //y.床號 = y.床號.Split('-')[0].Trim();
                int result = (x.床號.Split('-')[0].Trim().StringToInt32()).CompareTo(y.床號.Split('-')[0].Trim().StringToInt32());
                if (result == 0)
                {
                    result = string.Compare(x.更新時間, y.更新時間) * -1;
                }
                return result;
            }
        }
        static public returnData update_patientInfo(string API_Server, List<patientInfoClass> patientInfoClasses)
        {
            string url = $"{API_Server}/api/med_cart/update_patientInfo";

            returnData returnData = new returnData();
            returnData.Data = patientInfoClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            //if (returnData == null) return null;
            //if (returnData.Code != 200) return null;
            List<patientInfoClass> out_medCarInfoClass = returnData.Data.ObjToClass<List<patientInfoClass>>();
            Console.WriteLine($"{returnData}");
            return returnData;
        }

        static public List<patientInfoClass> get_bed_list_by_cart(string API_Server, List<string> Info)
        {
            string url = $"{API_Server}/api/med_cart/get_bed_list_by_cart";

            returnData returnData = new returnData();
            returnData.ValueAry = Info;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<patientInfoClass> out_medCarInfoClass = returnData.Data.ObjToClass<List<patientInfoClass>>();
            Console.WriteLine($"{returnData}");
            return out_medCarInfoClass;
        }
        static public List<patientInfoClass> get_bed_list_by_cart_total(string API_Server, List<string> Info)
        {
            string url = $"{API_Server}/api/med_cart/get_bed_list_by_cart_total";

            returnData returnData = new returnData();
            returnData.ValueAry = Info;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<patientInfoClass> out_medCarInfoClass = returnData.Data.ObjToClass<List<patientInfoClass>>();
            Console.WriteLine($"{returnData}");
            return out_medCarInfoClass;
        }
        static public List<patientInfoClass> get_patient_by_bedNum(string API_Server, List<string> Info)
        {
            List<patientInfoClass> out_medCarInfoClass = new List<patientInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_patient_by_bedNum";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_medCarInfoClass = returnData.Data.ObjToClass<List<patientInfoClass>>();
            return out_medCarInfoClass;

        }
        static public patientInfoClass get_patient_by_GUID(string API_Server, string value, List<string> Info)
        {
            //List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_patient_by_GUID";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            returnData.Value = value;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            patientInfoClass out_medCarInfoClass = returnData.Data.ObjToClass<patientInfoClass>();
            return out_medCarInfoClass;

        }
        static public patientInfoClass get_patient_by_GUID_brief(string API_Server, List<string> Info)
        {
            string url = $"{API_Server}/api/med_cart/get_patient_by_GUID_brief";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            patientInfoClass out_medCarInfoClass = returnData.Data.ObjToClass<patientInfoClass>();
            return out_medCarInfoClass;

        }
        static public List<patientInfoClass> get_all(string API_Server)
        {
            List<patientInfoClass> out_medCarInfoClass = new List<patientInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_all";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_medCarInfoClass = returnData.Data.ObjToClass<List<patientInfoClass>>();
            out_medCarInfoClass.Sort(new patientInfoClass.ICP_By_bedNum());
            return out_medCarInfoClass;
        }
        static public Dictionary<string, List<patientInfoClass>> ToDictByGUID(List<patientInfoClass> patientInfoClasses)
        {
            Dictionary<string, List<patientInfoClass>> dictionary = new Dictionary<string, List<patientInfoClass>>();
            foreach (var item in patientInfoClasses)
            {
                if (dictionary.TryGetValue(item.GUID, out List<patientInfoClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.GUID] = new List<patientInfoClass>() { item };
                }
            }
            return dictionary;
        }
        static public List<patientInfoClass> GetDictByGUID(Dictionary<string, List<patientInfoClass>> dict, string GUID)
        {
            if (dict.TryGetValue(GUID, out List<patientInfoClass> patientInfoClasses))
            {
                return patientInfoClasses;
            }
            else
            {
                return new List<patientInfoClass>();
            }
        }
        static public Dictionary<string, List<patientInfoClass>> CoverToDictByMedCart(List<patientInfoClass> medCarInfoClasses)
        {
            Dictionary<string, List<patientInfoClass>> dictionary = new Dictionary<string, List<patientInfoClass>>();
            foreach (var item in medCarInfoClasses)
            {
                if (dictionary.TryGetValue(item.護理站, out List<patientInfoClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.護理站] = new List<patientInfoClass>() { item };
                }
            }
            return dictionary;
        }
        static public List<patientInfoClass> SortDictByMedCart(Dictionary<string, List<patientInfoClass>> dict, string 護理站)
        {
            if (dict.TryGetValue(護理站, out List<patientInfoClass> medCarInfoClasses))
            {
                return medCarInfoClasses;
            }
            else
            {
                return new List<patientInfoClass>();
            }
        }
        static public Dictionary<string, List<patientInfoClass>> ToDictByBedNum(List<patientInfoClass> patientInfoClasses)
        {
            Dictionary<string, List<patientInfoClass>> dictionary = new Dictionary<string, List<patientInfoClass>>();
            foreach (var item in patientInfoClasses)
            {
                if (dictionary.TryGetValue(item.床號, out List<patientInfoClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.床號] = new List<patientInfoClass>() { item };
                }
            }
            return dictionary;
        }
        static public List<patientInfoClass> GetDictByBedNum(Dictionary<string, List<patientInfoClass>> dict, string 床號)
        {
            if (dict.TryGetValue(床號, out List<patientInfoClass> patientInfoClasses))
            {
                return patientInfoClasses;
            }
            else
            {
                return new List<patientInfoClass>();
            }
        }
    }
    public class diseaseOut
    {
        /// <summary>
        /// 疾病代碼
        /// </summary>
        [JsonPropertyName("disease_code")]
        public string 疾病代碼 { get; set; }
        /// <summary>
        /// 疾病說明
        /// </summary>
        [JsonPropertyName("disease_descrip")]
        public string 疾病說明 { get; set; }

    }

}
