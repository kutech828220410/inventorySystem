using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Web;
using NPOI.SS.Formula.Functions;

namespace HIS_DB_Lib
{
    [EnumDescription("patient_dx")]
    public enum enum_patientDx
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("藥袋條碼,VARCHAR,200,INDEX")]
        藥袋條碼,
        [Description("病歷號,VARCHAR,20,NONE")]
        病歷號,
        [Description("診斷碼,VARCHAR,100,NONE")]
        診斷碼,
        [Description("診斷內容,VARCHAR,1000,NONE")]
        診斷內容,
        [Description("過敏藥碼,VARCHAR,30,NONE")]
        過敏藥碼,
        [Description("過敏藥名,VARCHAR,100,NONE")]
        過敏藥名,
        [Description("交互作用藥碼,VARCHAR,30,NONE")]
        交互作用藥碼,
        [Description("交互作用,VARCHAR,1000,NONE")]
        交互作用,
        [Description("加入時間,DATETIME,30,NONE")]
        加入時間,
        [Description("備註,VARCHAR,500,NONE")]
        備註
    }

    public class patientDxClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥袋條碼 { get; set; }
        [JsonPropertyName("PATCODE")]
        public string 病歷號 { get; set; }

        [JsonPropertyName("ICD_CODE")]
        public string 診斷碼 { get; set; }

        [JsonPropertyName("ICD_DESC")]
        public string 診斷內容 { get; set; }

        [JsonPropertyName("ALLERGY_CODE")]
        public string 過敏藥碼 { get; set; }

        [JsonPropertyName("ALLERGY_NAME")]
        public string 過敏藥名 { get; set; }

        [JsonPropertyName("INTERACT_DRUG_CODE")]
        public string 交互作用藥碼 { get; set; }

        [JsonPropertyName("INTERACTION_DESC")]
        public string 交互作用 { get; set; }

        [JsonPropertyName("CREATE_TIME")]
        public string 加入時間 { get; set; }

        [JsonPropertyName("REMARK")]
        public string 備註 { get; set; }
    }
}
