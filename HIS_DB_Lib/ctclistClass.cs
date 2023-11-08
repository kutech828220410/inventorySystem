using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public class ctclistClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Hhisnum")]
        public string 病歷號 { get; set; }
        [JsonPropertyName("hcasetyp")]
        public string 藥局代碼 { get; set; }
        [JsonPropertyName("hcaseno")]
        public string 就醫序號 { get; set; }
        [JsonPropertyName("udrelseq")]
        public string 處方箋序號 { get; set; }
        [JsonPropertyName("udTitle")]
        public string 處方狀態 { get; set; }
        [JsonPropertyName("age")]
        public string 年齡 { get; set; }
        [JsonPropertyName("Regname")]
        public string Regimen名稱 { get; set; }
        [JsonPropertyName("Canmname")]
        public string 主分類 { get; set; }
        [JsonPropertyName("cansname")]
        public string 次分類 { get; set; }
        [JsonPropertyName("Hbirthdt")]
        public string 生日 { get; set; }
        [JsonPropertyName("Hsex")]
        public string 性別 { get; set; }
        [JsonPropertyName("Hnursta")]
        public string 病房 { get; set; }
        [JsonPropertyName("Hbedno")]
        public string 病床 { get; set; }
        [JsonPropertyName("udbgndtRage")]
        public string 藥囑開始日期 { get; set; }
        [JsonPropertyName("udenddt")]
        public string 藥囑結束日期 { get; set; }
        [JsonPropertyName("Udoename")]
        public string 開立醫師 { get; set; }
        [JsonPropertyName("Udptvsnam")]
        public string 審核醫師 { get; set; }
        [JsonPropertyName("hnursvcl")]
        public string 科別 { get; set; }
        [JsonPropertyName("Hheight")]
        public string 身高 { get; set; }
        [JsonPropertyName("Hweight")]
        public string 體重 { get; set; }
        [JsonPropertyName("Bsa")]
        public string BSA { get; set; }
        [JsonPropertyName("Perfstat")]
        public string Performance_status { get; set; }
        [JsonPropertyName("Celltype")]
        public string 細胞型態 { get; set; }
        [JsonPropertyName("labdata")]
        public string 檢查驗資料 { get; set; }
        [JsonPropertyName("cxrdata")]
        public string XRAY文字 { get; set; }
        [JsonPropertyName("ekgdata")]
        public string EKG文字 { get; set; }
        [JsonPropertyName("Pulmdata")]
        public string Pulmdata { get; set; }
        [JsonPropertyName("Hdiagcod")]
        public string 診斷 { get; set; }
        [JsonPropertyName("injroute")]
        public string 癌症用藥途徑 { get; set; }

        [JsonPropertyName("udAry")]
        public List<udAryClass> udAry = new List<udAryClass>();
        [JsonPropertyName("changeAry")]
        public List<udAryClass> changeAry = new List<udAryClass>();
        [JsonPropertyName("ctcAry")]
        public List<udAryClass> ctcAry = new List<udAryClass>();
        [JsonPropertyName("noteAry")]
        public List<udAryClass> noteAry = new List<udAryClass>();

        public class udAryClass
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("Hhisnum")]
            public string 病歷號 { get; set; }
            [JsonPropertyName("hcasetyp")]
            public string 藥局代碼 { get; set; }
            [JsonPropertyName("hcaseno")]
            public string 醫囑序號 { get; set; }
            [JsonPropertyName("udordseq")]
            public string 藥囑序號 { get; set; }
            [JsonPropertyName("Udseqseq")]
            public string 服藥順序 { get; set; }
            [JsonPropertyName("Udmdpnam")]
            public string 藥囑名稱 { get; set; }
            [JsonPropertyName("Udselbuy")]
            public string 自費 { get; set; }
            [JsonPropertyName("Uddosage")]
            public string 劑量 { get; set; }
            [JsonPropertyName("Udfreqn")]
            public string 頻次 { get; set; }
            [JsonPropertyName("Udroute")]
            public string 途徑 { get; set; }
            [JsonPropertyName("Uddurat")]
            public string 天數 { get; set; }
            [JsonPropertyName("Udbgndt")]
            public string 開始日 { get; set; }
            [JsonPropertyName("Udbgntm")]
            public string 開始時間 { get; set; }
            [JsonPropertyName("Udenddt")]
            public string 結束日 { get; set; }
            [JsonPropertyName("Udendtm")]
            public string 結束時間 { get; set; }
            [JsonPropertyName("Coldesch")]
            public string 藥品外型外觀 { get; set; }
            [JsonPropertyName("aprdesch")]
            public string 藥品使用 { get; set; }
            [JsonPropertyName("udwords")]
            public string 藥品下方註記 { get; set; }
            [JsonPropertyName("udpicUrl")]
            public string 藥品圖片網址 { get; set; }
        }
        public class changeAryClass
        {
            [JsonPropertyName("Udmdpnam")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("Usdate")]
            public string 變異日期 { get; set; }
            [JsonPropertyName("Ustime")]
            public string 變異時間 { get; set; }
            [JsonPropertyName("Varrsn")]
            public string 變異原因 { get; set; }
            [JsonPropertyName("vardata")]
            public string 變異內容 { get; set; }
        }
        public class ctcAryClass
        {
            [JsonPropertyName("ctcseq")]
            public string 歷次第幾次化療 { get; set; }
            [JsonPropertyName("Udbgndt")]
            public string 開立日期 { get; set; }
            [JsonPropertyName("regname")]
            public string Regimen名稱 { get; set; }
        }
        public class noteAryClass
        {
            [JsonPropertyName("Dgname")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("dgnote")]
            public string 調整與注意事項 { get; set; }

        }
    }
}
