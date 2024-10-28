using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HIS_DB_Lib
{
    [EnumDescription("med_carInfo")]
    public enum enum_med_carInfo
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("更新時間,DATETIME,10,NONE")]
        更新時間,
        [Description("異動,VARCHAR,10,NONE")]
        異動,
        [Description("姓名,VARCHAR,50,NONE")]
        姓名,
        [Description("住院號,VARCHAR,50,INDEX")]
        住院號,
        [Description("病歷號,VARCHAR,50,NONE")]
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
    /// medCarInfoClass 資料
    /// </summary>
    public class medCarInfoClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        [JsonPropertyName("update_time")]
        public string 更新時間 { get; set; }
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

        public class ICP_By_bedNum : IComparer<medCarInfoClass>
        {
            public int Compare(medCarInfoClass x, medCarInfoClass y)
            {
                return (x.床號.StringToInt32()).CompareTo(y.床號.StringToInt32());
            }
        }
        static public List<medCarInfoClass> update_med_carinfo(string API_Server, List<medCarInfoClass> medCarInfoClasses)
        {
            string url = $"{API_Server}/api/med_cart/update_med_carinfo";

            returnData returnData = new returnData();
            returnData.Data = medCarInfoClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medCarInfoClass>  out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
            Console.WriteLine($"{returnData}");
            return out_medCarInfoClass;
        }
        static public List<medClass> update_med_page_cloud(string API_Server, List<medClass> medClasses)
        {
            string url = $"{API_Server}/api/med_cart/update_med_page_cloud";

            returnData returnData = new returnData();
            returnData.Data = medClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medClass> out_medClass = returnData.Data.ObjToClass<List<medClass>>();
            Console.WriteLine($"{returnData}");
            return out_medClass;
        }
        static public List<OrderClass> update_order_list(string API_Server, List<OrderClass> OrderClasses)
        {
            string url = $"{API_Server}/api/med_cart/update_order_list";

            returnData returnData = new returnData();
            returnData.Data = OrderClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<OrderClass> out_OrderClass = returnData.Data.ObjToClass<List<OrderClass>>();
            Console.WriteLine($"{returnData}");
            return out_OrderClass;
        }        
        static public List<medCarInfoClass> get_bed_list_by_cart(string API_Server, List<string> Info)
        {
            string url = $"{API_Server}/api/med_cart/get_bed_list_by_cart";

            returnData returnData = new returnData();
            returnData.ValueAry = Info;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            List<medCarInfoClass>  out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
            Console.WriteLine($"{returnData}");
            return out_medCarInfoClass;
        }
        static public List<medCarInfoClass> get_patient_by_bedNum(string API_Server, List<string> Info)
        {
            List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_patient_by_bedNum";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
            return out_medCarInfoClass;

        }
        static public medCarInfoClass get_patient_by_GUID(string API_Server, List<string> Info)
        {
            //List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_patient_by_GUID";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            medCarInfoClass out_medCarInfoClass = returnData.Data.ObjToClass<medCarInfoClass>();
            return out_medCarInfoClass;

        }
        static public medCarInfoClass get_patient_by_GUID_brief(string API_Server, List<string> Info)
        {
            string url = $"{API_Server}/api/med_cart/get_patient_by_GUID_brief";
            returnData returnData = new returnData();
            returnData.ValueAry = Info;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            medCarInfoClass out_medCarInfoClass = returnData.Data.ObjToClass<medCarInfoClass>();
            return out_medCarInfoClass;

        }
        static public List<medCarInfoClass> get_all(string API_Server)
        {
            List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
            string url = $"{API_Server}/api/med_cart/get_all";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200) return null;
            out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
            out_medCarInfoClass.Sort(new medCarInfoClass.ICP_By_bedNum());
            return out_medCarInfoClass;
        }
        static public Dictionary<string, List<medCarInfoClass>> CoverToDictByGUID(List<medCarInfoClass> medCarInfoClasses)
        {
            Dictionary<string, List<medCarInfoClass>> dictionary = new Dictionary<string, List<medCarInfoClass>>();
            foreach(var item in medCarInfoClasses)
            {
                if(dictionary.TryGetValue(item.GUID, out List<medCarInfoClass> list))
                {
                    list.Add(item);
                }
                else
                {
                    dictionary[item.GUID] = new List<medCarInfoClass>();
                }
            }
            return dictionary;
        }
        static public List<medCarInfoClass> SortDictByGUID (Dictionary<string, List<medCarInfoClass>> dict, string GUID)
        {
            if (dict.TryGetValue(GUID, out List<medCarInfoClass> medCarInfoClasses))
            {
                return medCarInfoClasses;
            }
            else
            {
                return new List<medCarInfoClass>();
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
