using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_udnoectc
    {
        GUID,
        病房,
        床號,
        病人姓名,
        病歷號,
        生日,
        性別,
        身高,
        體重,
        診斷,
        科別,
        開立醫師,
        過敏記錄,
        RegimenName,
        天數順序,
        診別,
        就醫序號,
        醫囑序號,
        化學治療前檢核項目,
        醫囑確認藥師,
        醫囑確認時間,
        調劑藥師,
        調劑完成時間,
        核對藥師,
        核對時間,
        加入時間,

    }
    /// <summary>
    /// 化療配藥通知處方
    /// </summary>
    public class udnoectc
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]     
        public string GUID { get; set; }
        /// <summary>
        /// 病房
        /// </summary>
        [JsonPropertyName("hnursta")]
        public string 病房 { get; set; }
        /// <summary>
        /// 床號
        /// </summary>
        [JsonPropertyName("hbed")]
        public string 床號 { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        [JsonPropertyName("hnamec")]
        public string 病人姓名 { get; set; }
        /// <summary>
        /// 病歷號
        /// </summary>
        [JsonPropertyName("hhisnum")]//key
        public string 病歷號 { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [JsonPropertyName("hbirthdt")]
        public string 生日 { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        [JsonPropertyName("hsexc")]
        public string 性別 { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        [JsonPropertyName("hheight")]
        public string 身高 { get; set; }
        /// <summary>
        /// 體重
        /// </summary>
        [JsonPropertyName("hweight")]
        public string 體重 { get; set; }
        /// <summary>
        /// 診斷
        /// </summary>
        [JsonPropertyName("hdiagtxt")]
        public string 診斷 { get; set; }
        /// <summary>
        /// 科別
        /// </summary>
        [JsonPropertyName("csect")]
        public string 科別 { get; set; }
        /// <summary>
        /// 開立醫師
        /// </summary>
        [JsonPropertyName("udoename")]
        public string 開立醫師 { get; set; }
        /// <summary>
        /// 過敏記錄
        /// </summary>
        [JsonPropertyName("halergy")]
        public string 過敏記錄 { get; set; }
        /// <summary>
        /// RegimenName
        /// </summary>
        [JsonPropertyName("udregnam")]
        public string RegimenName { get; set; }
        /// <summary>
        /// 天數順序
        /// </summary>
        [JsonPropertyName("uddaytxt")]
        public string 天數順序 { get; set; }
        /// <summary>
        /// 診別
        /// </summary>
        [JsonPropertyName("hcasetyp")]
        public string 診別 { get; set; }
        /// <summary>
        /// 就醫序號
        /// </summary>
        [JsonPropertyName("hcaseno")]
        public string 就醫序號 { get; set; }
        /// <summary>
        /// 醫囑序號
        /// </summary>
        [JsonPropertyName("udrelseq")]
        public string 醫囑序號 { get; set; }

        /// <summary>
        /// 化學治療前檢核項目
        /// </summary>
        [JsonPropertyName("labdatas")]
        public List<string> labdatas
        { 
            get
            {
                return 化學治療前檢核項目.JsonDeserializet<List<string>>();
            }
            set
            {
                化學治療前檢核項目 = value.JsonSerializationt();
            }
        }
        [JsonIgnore]
        public string 化學治療前檢核項目 { get; set; }


        /// <summary>
        /// 醫囑確認藥師
        /// </summary>
        [JsonPropertyName("confirm_ph")]
        public string 醫囑確認藥師 { get; set; }
        /// <summary>
        /// 醫囑確認藥師
        /// </summary>
        [JsonPropertyName("confirm_time")]
        public string 醫囑確認時間 { get; set; }
        /// <summary>
        /// 調劑藥師
        /// </summary>
        [JsonPropertyName("disp_ph")]
        public string 調劑藥師 { get; set; }
        /// <summary>
        /// 調劑完成時間
        /// </summary>
        [JsonPropertyName("disp_time")]
        public string 調劑完成時間 { get; set; }
        /// <summary>
        /// 核對藥師
        /// </summary>
        [JsonPropertyName("check_ph")]
        public string 核對藥師 { get; set; }
        /// <summary>
        /// 核對時間
        /// </summary>
        [JsonPropertyName("check_time")]
        public string 核對時間 { get; set; }
        /// <summary>
        /// 加入時間
        /// </summary>
        [JsonPropertyName("ctdate")]
        public string 加入時間 { get; set; }


        /// <summary>
        /// 藥囑資料(請參閱 udnoectc_ordersClass)
        /// </summary>
        [JsonPropertyName("orders")]
        public List<udnoectc_orders> ordersAry { get; set; }
        /// <summary>
        /// 變異紀錄(請參閱 udnoectc_ctcvarsClass)
        /// </summary>
        [JsonPropertyName("ctcvars")]
        public List<udnoectc_ctcvars> ctcvarsAry { get; set; }

        

    }

    public enum enum_udnoectc_orders
    {
        GUID,
        Master_GUID,
        藥囑序號,
        服藥順序,
        藥碼,
        藥名,
        警示,
        劑量,
        單位,
        途徑,
        頻次,
        儲位1,
        儲位2,
        備註,
        數量,
        處方開始時間,
        處方結束時間,
        已備藥完成,
        備藥藥師,
        備藥完成時間,
        調劑藥師,
        調劑完成時間,
        核對藥師,
        核對時間,

    }
    /// <summary>
    /// 藥囑資料(配藥通知子項目)
    /// </summary>
    public class udnoectc_orders
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 上層目錄索引鍵
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 藥囑序號
        /// </summary>
        [JsonPropertyName("udordseq")]
        public string 藥囑序號 { get; set; }
        /// <summary>
        /// 服藥順序
        /// </summary>
        [JsonPropertyName("serno")]
        public string 服藥順序 { get; set; }
        /// <summary>
        /// 上層目錄索引鍵
        /// </summary>
        [JsonPropertyName("uddrgno")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("udrpname")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 警示
        /// </summary>
        [JsonPropertyName("udhamsg")]
        public string 警示 { get; set; }
        /// <summary>
        /// 劑量
        /// </summary>
        [JsonPropertyName("uddosage")]
        public string 劑量 { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [JsonPropertyName("uddosuni")]
        public string 單位 { get; set; }
        /// <summary>
        /// 途徑
        /// </summary>
        [JsonPropertyName("udroute")]
        public string 途徑 { get; set; }
        /// <summary>
        /// 頻次
        /// </summary>
        [JsonPropertyName("udfreqn")]
        public string 頻次 { get; set; }
        /// <summary>
        /// 儲位1
        /// </summary>
        [JsonPropertyName("udstorn1")]
        public string 儲位1 { get; set; }
        /// <summary>
        /// 儲位2
        /// </summary>
        [JsonPropertyName("udstorn2")]
        public string 儲位2 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("udothdes")]
        public string 備註 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("dspqty")]
        public string 數量 { get; set; }
        /// <summary>
        /// 處方開始時間
        /// </summary>
        [JsonPropertyName("udbgndt")]
        public string 處方開始時間 { get; set; }
        /// <summary>
        /// 處方結束時間
        /// </summary>
        [JsonPropertyName("udenddt")]
        public string 處方結束時間 { get; set; }
        /// <summary>
        /// 已備藥完成
        /// </summary>
        [JsonPropertyName("is_comp")]
        public string 已備藥完成 { get; set; }
        /// <summary>
        /// 備藥藥師
        /// </summary>
        [JsonPropertyName("comp_ph")]
        public string 備藥藥師 { get; set; }
        /// <summary>
        /// 備藥完成時間
        /// </summary>
        [JsonPropertyName("comp_time")]
        public string 備藥完成時間 { get; set; }
        /// <summary>
        /// 調劑藥師
        /// </summary>
        [JsonPropertyName("disp_ph")]
        public string 調劑藥師 { get; set; }
        /// <summary>
        /// 調劑完成時間
        /// </summary>
        [JsonPropertyName("disp_time")]
        public string 調劑完成時間 { get; set; }
        /// <summary>
        /// 核對藥師
        /// </summary>
        [JsonPropertyName("check_ph")]
        public string 核對藥師 { get; set; }
        /// <summary>
        /// 核對時間
        /// </summary>
        [JsonPropertyName("check_time")]
        public string 核對時間 { get; set; }
    }


    public enum enum_udnoectc_ctcvars
    {
        GUID,
        Master_GUID,
        藥名,
        變異時間,
        變異原因,
        變異內容,
        說明,
    }
    /// <summary>
    /// 變異紀錄(配藥通知子項目)
    /// </summary>
    public class udnoectc_ctcvars
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 上層目錄索引鍵
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("udmdpnam")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 變異時間
        /// </summary>
        [JsonPropertyName("zudctcva")]
        public string 變異時間 { get; set; }
        /// <summary>
        /// 變異原因
        /// </summary>
        [JsonPropertyName("typedesc")]
        public string 變異原因 { get; set; }
        /// <summary>
        /// 變異內容
        /// </summary>
        [JsonPropertyName("varrsn")]
        public string 變異內容 { get; set; }
        /// <summary>
        /// 說明
        /// </summary>
        [JsonPropertyName("vardata")]
        public string 說明 { get; set; }
    }
}
