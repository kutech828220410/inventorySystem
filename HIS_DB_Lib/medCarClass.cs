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
    [EnumDescription("med_car")]
    public enum enum_med_car
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("姓名,VARCHAR,30,NONE")]
        姓名,
        [Description("住院號,VARCHAR,50,INDEX")]
        住院號,
        [Description("病歷號,VARCHAR,50,NONE")]
        病歷號,
        [Description("藥局,VARCHAR,10,NONE")]
        藥局,
        [Description("護理站,VARCHAR,10,NONE")]
        護理站,
        [Description("床號,VARCHAR,10,NONE")]
        床號,
        [Description("占床狀態,VARCHAR,10,NONE")]
        占床狀態,
        [Description("調劑狀態,VARCHAR,10,NONE")]
        調劑狀態,
        [Description("性別,VARCHAR,10,NONE")]
        性別,
        [Description("出生日期,VARCHAR,10,NONE")]
        出生日期,
        [Description("科別,VARCHAR,20,NONE")]
        科別,
        [Description("財務,VARCHAR,10,NONE")]
        財務,
        [Description("入院日期,DATETIME,50,INDEX")]
        入院日期,
        [Description("訪視號碼,VARCHAR,10,NONE")]
        訪視號碼,
        [Description("診所名稱,VARCHAR,10,NONE")]
        診所名稱,
        [Description("醫生姓名,VARCHAR,10,NONE")]
        醫生姓名,
        [Description("身高,VARCHAR,10,NONE")]
        身高,
        [Description("體重,VARCHAR,10,NONE")]
        體重,
        [Description("體表面積,VARCHAR,10,NONE")]
        體表面積,
        [Description("國際疾病分類代碼1,VARCHAR,100,NONE")]
        國際疾病分類代碼1,
        [Description("疾病說明1,VARCHAR,100,NONE")]
        疾病說明1,
        [Description("國際疾病分類代碼2,VARCHAR,100,NONE")]
        國際疾病分類代碼2,
        [Description("疾病說明2,VARCHAR,100,NONE")]
        疾病說明2,
        [Description("國際疾病分類代碼3,VARCHAR,100,NONE")]
        國際疾病分類代碼3,
        [Description("疾病說明3,VARCHAR,100,NONE")]
        疾病說明3,
        [Description("國際疾病分類代碼4,VARCHAR,100,NONE")]
        國際疾病分類代碼4,
        [Description("疾病說明4,VARCHAR,100,NONE")]
        疾病說明4,
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
    }
    [EnumDescription("med_order")]
    public enum enum_med_order
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("調劑狀態,VARCHAR,10,NONE")]
        調劑狀態,
        [Description("住院號,VARCHAR,50,INDEX")]
        住院號,
        [Description("序號,VARCHAR,10,NONE")]
        序號,
        [Description("狀態,VARCHAR,10,NONE")]
        狀態,
        [Description("開始日期,VARCHAR,10,NONE")]
        開始日期,
        [Description("開始時間,VARCHAR,10,NONE")]
        開始時間,
        [Description("結束日期,VARCHAR,10,NONE")]
        結束日期,
        [Description("結束時間,VARCHAR,10,NONE")]
        結束時間,
        [Description("藥碼,VARCHAR,10,NONE")]
        藥碼,
        [Description("頻次代碼,VARCHAR,10,NONE")]
        頻次代碼,
        [Description("頻次屬性,VARCHAR,10,NONE")]
        頻次屬性,
        [Description("藥品名,VARCHAR,10,NONE")]
        藥品名,
        [Description("途徑,VARCHAR,10,NONE")]
        途徑,
        [Description("數量,VARCHAR,10,NONE")]
        數量,
        [Description("劑量,VARCHAR,10,NONE")]
        劑量,
        [Description("單位,VARCHAR,10,NONE")]
        單位,
        [Description("期限,VARCHAR,10,NONE")]
        期限,
        [Description("自動包藥機,VARCHAR,10,NONE")]
        自動包藥機,
        [Description("化癌分類,VARCHAR,10,NONE")]
        化癌分類,
        [Description("自購,VARCHAR,10,NONE")]
        自購,
        [Description("血液製劑註記,VARCHAR,10,NONE")]
        血液製劑註記,
        [Description("處方醫師,VARCHAR,10,NONE")]
        處方醫師,
        [Description("處方醫師姓名,VARCHAR,10,NONE")]
        處方醫師姓名,
        [Description("操作人員,VARCHAR,10,NONE")]
        操作人員,
        [Description("藥局代碼,VARCHAR,10,NONE")]
        藥局代碼,
        [Description("大瓶點滴,VARCHAR,10,NONE")]
        大瓶點滴,
        [Description("LKFLAG,VARCHAR,10,NONE")]
        LKFLAG,
        [Description("排序,VARCHAR,10,NONE")]
        排序,
        [Description("判讀藥師代碼,VARCHAR,10,NONE")]
        判讀藥師代碼,
        [Description("判讀FLAG,VARCHAR,10,NONE")]
        判讀FLAG,
        [Description("勿磨,VARCHAR,10,NONE")]
        勿磨,
        [Description("抗生素等級,VARCHAR,10,NONE")]
        抗生素等級,
        [Description("重複用藥,VARCHAR,10,NONE")]
        重複用藥,
        [Description("配藥天數,VARCHAR,10,NONE")]
        配藥天數,
        [Description("交互作用,VARCHAR,10,NONE")]
        交互作用,
        [Description("交互作用等級,VARCHAR,10,NONE")]
        交互作用等級
    }
    public class medCartInfoClass
    {
        public class medCartClass
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("pnamec")]
            public string 姓名 { get; set; }
            [JsonPropertyName("caseno")]
            public string 住院號 { get; set; }
            [JsonPropertyName("histno")]
            public string 病歷號 { get; set; }
            [JsonPropertyName("pharm_code")]
            public string 藥局 { get; set; }
            [JsonPropertyName("hnursta")]
            public string 護理站 { get; set; }
            [JsonPropertyName("hbedno")]
            public string 床號 { get; set; }
            [JsonPropertyName("bed_status")]
            public string 占床狀態 { get; set; }
            [JsonPropertyName("dispens_status")]
            public string 調劑狀態 { get; set; }
            [JsonPropertyName("hsexc")]
            public string 性別 { get; set; }
            [JsonPropertyName("pbirth8")]
            public string 出生日期 { get; set; }
            [JsonPropertyName("psectc")]
            public string 科別 { get; set; }
            [JsonPropertyName("pfinc")]
            public string 財務 { get; set; }
            [JsonPropertyName("padmdt")]
            public string 入院日期 { get; set; }
            [JsonPropertyName("pvsdno")]
            public string 訪視號碼 { get; set; }
            [JsonPropertyName("pvsnam")]
            public string 診所名稱 { get; set; }
            [JsonPropertyName("prnam")]
            public string 醫生姓名 { get; set; }
            [JsonPropertyName("pbhight")]
            public string 身高 { get; set; }
            [JsonPropertyName("pbweight")]
            public string 體重 { get; set; }
            [JsonPropertyName("pbbsa")]
            public string 體表面積 { get; set; }
            [JsonPropertyName("hicd1")]
            public string 國際疾病分類代碼1 { get; set; }
            [JsonPropertyName("hicdtx1")]
            public string 疾病說明1 { get; set; }
            [JsonPropertyName("hicd2")]
            public string 國際疾病分類代碼2 { get; set; }
            [JsonPropertyName("hicdtx2")]
            public string 疾病說明2 { get; set; }
            [JsonPropertyName("hicd3")]
            public string 國際疾病分類代碼3 { get; set; }
            [JsonPropertyName("hicdtx3")]
            public string 疾病說明3 { get; set; }
            [JsonPropertyName("hicd4")]
            public string 國際疾病分類代碼4 { get; set; }
            [JsonPropertyName("hicdtx4")]
            public string 疾病說明4 { get; set; }
            [JsonPropertyName("ngtube")]
            public string 鼻胃管使用狀況 { get; set; }
            [JsonPropertyName("tube")]
            public string 其他管路使用狀況 { get; set; }
            [JsonPropertyName("hallergy")]
            public string 過敏史 { get; set; }
            [JsonPropertyName("rtalb")]
            public string 白蛋白 { get; set; }
            [JsonPropertyName("rtcrea")]
            public string 肌酸酐 { get; set; }
            [JsonPropertyName("rtegfrm")]
            public string 估算腎小球過濾率 { get; set; }
            [JsonPropertyName("rtalt")]
            public string 丙氨酸氨基轉移酶 { get; set; }
            [JsonPropertyName("rtk")]
            public string 鉀離子 { get; set; }
            [JsonPropertyName("rtca")]
            public string 鈣離子 { get; set; }
            [JsonPropertyName("rttb")]
            public string 總膽紅素 { get; set; }
            [JsonPropertyName("rtna")]
            public string 鈉離子 { get; set; }
            [JsonPropertyName("rtwbc")]
            public string 白血球 { get; set; }
            [JsonPropertyName("rthgb")]
            public string 血紅素 { get; set; }
            [JsonPropertyName("rtptl")]
            public string 血小板 { get; set; }
            [JsonPropertyName("rtinr")]
            public string 國際標準化比率 { get; set; }


            public class ICP_By_bedNum : IComparer<medCarInfoClass>
            {
                public int Compare(medCarInfoClass x, medCarInfoClass y)
                {
                    return (x.床號.StringToInt32()).CompareTo(y.床號.StringToInt32());
                }
            }
            static public List<medCarInfoClass> update_bed_list(string API_Server, List<medCarInfoClass> medCarInfoClasses)
            {
                List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
                string url = $"{API_Server}/api/med_cart/update_bed_list";

                returnData returnData = new returnData();
                returnData.Data = medCarInfoClasses;
                string json_in = returnData.JsonSerializationt();
                string json_out = Net.WEBApiPostJson(url, json_in);
                returnData = json_out.JsonDeserializet<returnData>();
                if (returnData == null) return null;
                if (returnData.Code != 200) return null;
                out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
                out_medCarInfoClass.Sort(new medCarInfoClass.ICP_By_bedNum());
                Console.WriteLine($"{returnData}");
                return out_medCarInfoClass;
            }
            static public List<medCarInfoClass> get_patient_by_bedNum(string API_Server, List<string> Info)
            {
                List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
                string url = $"{API_Server}/api/med_cart/get_patient_by_bedNum";
                string str = "";
                for (int i = 0; i < Info.Count; i++)
                {
                    str += Info[i];
                    if (i != Info.Count - 1) str += ",";
                }
                returnData returnData = new returnData();
                returnData.ValueAry.Add(str);
                string json_in = returnData.JsonSerializationt();
                string json_out = Net.WEBApiPostJson(url, json_in);
                returnData = json_out.JsonDeserializet<returnData>();
                if (returnData == null) return null;
                if (returnData.Code != 200) return null;
                out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
                out_medCarInfoClass.Sort(new medCarInfoClass.ICP_By_bedNum());
                return out_medCarInfoClass;

            }
            static public List<medCarInfoClass> get_patient_by_hnursta(string API_Server, List<string> Info)
            {
                List<medCarInfoClass> out_medCarInfoClass = new List<medCarInfoClass>();
                string url = $"{API_Server}/api/med_cart/get_patient_by_hnursta";
                string str = "";
                for (int i = 0; i < Info.Count; i++)
                {
                    str += Info[i];
                    if (i != Info.Count - 1) str += ",";
                }
                returnData returnData = new returnData();
                returnData.ValueAry.Add(str);
                string json_in = returnData.JsonSerializationt();
                string json_out = Net.WEBApiPostJson(url, json_in);
                returnData = json_out.JsonDeserializet<returnData>();
                if (returnData == null) return null;
                if (returnData.Code != 200) return null;
                out_medCarInfoClass = returnData.Data.ObjToClass<List<medCarInfoClass>>();
                out_medCarInfoClass.Sort(new medCarInfoClass.ICP_By_bedNum());
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
        }
    }
    
}
