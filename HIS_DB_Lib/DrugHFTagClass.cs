using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;

namespace HIS_DB_Lib
{
    [EnumDescription("DrugHFTag")]
    public enum enum_DrugHFTag
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("TagSN,VARCHAR,50,INDEX")]
        標籤序號,
        [Description("藥品代碼,VARCHAR,30,INDEX")]
        藥碼,
        [Description("藥品名稱,VARCHAR,200,NONE")]
        藥名,
        [Description("效期,DATETIME,0,NONE")]
        效期,
        [Description("批號,VARCHAR,50,NONE")]
        批號,
        [Description("數量,INT,0,NONE")]
        數量,
        [Description("存放位置,VARCHAR,100,NONE")]
        存放位置,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("更新時間,DATETIME,0,NONE")]
        更新時間
    }
    public enum enum_DrugHFTagStatus
    {
        入庫註記,
        出庫註記,
        進入儲位,
        離開儲位,
        已重置
    }
    public class DrugHFTagClass
    {
        /// <summary>唯一識別碼</summary>
        [JsonPropertyName("guid")]
        public string GUID { get; set; }

        /// <summary>電子標籤序號</summary>
        [JsonPropertyName("tag_sn")]
        public string TagSN { get; set; }

        /// <summary>藥品代碼</summary>
        [JsonPropertyName("drug_code")]
        public string 藥碼 { get; set; }

        /// <summary>藥品名稱</summary>
        [JsonPropertyName("drug_name")]
        public string 藥名 { get; set; }

        /// <summary>效期</summary>
        [JsonPropertyName("expiry_date")]
        public string 效期 { get; set; }

        /// <summary>批號</summary>
        [JsonPropertyName("lot_number")]
        public string 批號 { get; set; }

        /// <summary>數量</summary>
        [JsonPropertyName("quantity")]
        public string 數量 { get; set; }

        /// <summary>存放位置</summary>
        [JsonPropertyName("location")]
        public string 存放位置 { get; set; }

        /// <summary>狀態（例如：入庫註記,出庫註記,進入儲位,離開儲位,已重置）</summary>
        [JsonPropertyName("status")]
        public string 狀態 { get; set; }

        /// <summary>最後更新時間</summary>
        [JsonPropertyName("updated_time")]
        public string 更新時間 { get; set; }
    }

    public static class DrugHFTagMethod
    {
        /// <summary>
        /// 依更新時間排序 HFTag 清單
        /// </summary>
        /// <param name="hfTags">HFTag 清單</param>
        /// <param name="descending">是否為遞減排序（預設為 true：由新到舊）</param>
        /// <returns>排序後的清單</returns>
        public static List<DrugHFTagClass> SortHFTagByUpdatedTime(this List<DrugHFTagClass> hfTags, bool descending = true)
        {
            if (hfTags == null) return new List<DrugHFTagClass>();

            return descending
                ? hfTags.OrderByDescending(tag => tag.更新時間).ToList()
                : hfTags.OrderBy(tag => tag.更新時間).ToList();
        }
    }
}
