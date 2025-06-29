using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using H_Pannel_lib;
using System.Data;
namespace HIS_DB_Lib
{
    [EnumDescription("DrugHFTag")]
    public enum enum_DrugHFTag
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("TagSN,VARCHAR,50,INDEX")]
        TagSN,
        [Description("藥碼,VARCHAR,30,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,200,NONE")]
        藥名,
        [Description("效期,VARCHAR,20,NONE")]
        效期,
        [Description("批號,VARCHAR,50,NONE")]
        批號,
        [Description("數量,VARCHAR,50,NONE")]
        數量,
        [Description("存放位置,VARCHAR,100,NONE")]
        存放位置,
        [Description("操作人員,VARCHAR,100,INDEX")]
        操作人員,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("更新時間,DATETIME,20,INDEX")]
        更新時間
    }
    public enum enum_DrugHFTagStatus
    {
        入庫註記,
        出庫註記,
        進入儲位,
        離開儲位,
        已重置,
        已銷毀,
    }
    public class DrugHFTagStatusSummaryByCode
    {
        public string 藥碼 { get; set; }

        public double 已入庫數量 { get; set; }

        public double 已出庫數量 { get; set; }

        public double 可出庫數量 { get; set; } // 新增：狀態為「入庫註記」

        public double 可入庫數量 { get; set; } // 新增：狀態為「出庫註記」或「已重置」

        public double 總數量 { get; set; }
    }

    /// <summary>
    /// 標籤狀態分類統計結果類別（數量型別為 double）
    /// </summary>
    public class DrugHFTagStatusSummary
    {
        public double 已重置數量 { get; set; }
        public double 入庫註記數量 { get; set; }
        public double 出庫註記數量 { get; set; }
        public double 進入儲位數量 { get; set; }
        public double 離開儲位數量 { get; set; }
        public double 已入庫數量 { get; set; }
        public double 未入庫數量 { get; set; }
        public double 可出庫數量 { get; set; }
        public double 總數量 { get; set; }
    }
    public class DrugHFTagClass
    {
        /// <summary>唯一識別碼</summary>
        [JsonPropertyName("GUID")]
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

        /// <summary>操作人員</summary>
        [JsonPropertyName("operator")]
        public string 操作人員 { get; set; }

        /// <summary>狀態（例如：入庫註記,出庫註記,進入儲位,離開儲位,已重置）</summary>
        [JsonPropertyName("status")]
        public string 狀態 { get; set; }

        /// <summary>最後更新時間</summary>
        [JsonPropertyName("updated_time")]
        public string 更新時間 { get; set; }

        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/DrugHFTag/init";

            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }

        static public List<DrugHFTagClass> add(string API_Server, DrugHFTagClass DrugHFTagClass)
        {
            var (code, result, list) = add_full(API_Server, new List<DrugHFTagClass> { DrugHFTagClass });
            return list;
        }
        static public List<DrugHFTagClass> add(string API_Server, List<DrugHFTagClass> DrugHFTagClasses)
        {
            var (code, result, list) = add_full(API_Server, DrugHFTagClasses);
            return list;
        }
        static public (int code, string result, List<DrugHFTagClass>) add_full(string API_Server, List<DrugHFTagClass> DrugHFTagClasses)
        {
            string url = $"{API_Server}/api/DrugHFTag/add";

            returnData returnData = new returnData();
            returnData.Data = DrugHFTagClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }
            Console.WriteLine($"{returnData_out}");
            DrugHFTagClasses = returnData_out.Data.ObjToClass<List<DrugHFTagClass>>();
            return (returnData_out.Code, returnData_out.Result, DrugHFTagClasses);
        }
        static public List<DrugHFTagClass> set_tag_broken(string API_Server, DrugHFTagClass DrugHFTagClass)
        {
            var (code, result, list) = set_tag_broken_full(API_Server, new List<DrugHFTagClass> { DrugHFTagClass });
            return list;
        }
        static public List<DrugHFTagClass> set_tag_broken(string API_Server, List<DrugHFTagClass> DrugHFTagClasses)
        {
            var (code, result, list) = set_tag_broken_full(API_Server, DrugHFTagClasses);
            return list;
        }
        static public (int code, string result, List<DrugHFTagClass>) set_tag_broken_full(string API_Server, List<DrugHFTagClass> DrugHFTagClasses)
        {
            string url = $"{API_Server}/api/DrugHFTag/set_tag_broken";

            returnData returnData = new returnData();
            returnData.Data = DrugHFTagClasses;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }
            Console.WriteLine($"{returnData_out}");
            DrugHFTagClasses = returnData_out.Data.ObjToClass<List<DrugHFTagClass>>();
            return (returnData_out.Code, returnData_out.Result, DrugHFTagClasses);
        }

        static public List<DrugHFTagClass> get_latest_tag_ByTagSN(string API_Server, string tagSN)
        {
            var (code, result, list) = get_latest_tag_ByTagSN_full(API_Server, new List<string>() { tagSN });
            return list;
        }
        static public List<DrugHFTagClass> get_latest_tag_ByTagSN(string API_Server, List<string> tagsSN)
        {
            var (code, result, list) = get_latest_tag_ByTagSN_full(API_Server, tagsSN);
            return list;
        }
        static public (int code, string result, List<DrugHFTagClass>) get_latest_tag_ByTagSN_full(string API_Server, List<string> tagsSN)
        {
            string url = $"{API_Server}/api/DrugHFTag/get_latest_tag_ByTagSN";

            returnData returnData = new returnData();
            string sqlList = string.Join(", ", tagsSN.Select(tagSN => $"{tagSN}"));
            returnData.ValueAry.Add(sqlList);

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }
            Console.WriteLine($"{returnData_out}");
            List<DrugHFTagClass> DrugHFTagClasses = returnData_out.Data.ObjToClass<List<DrugHFTagClass>>();
            return (returnData_out.Code, returnData_out.Result, DrugHFTagClasses);
        }

        static public List<DrugHFTagClass> get_latest_reset_tag(string API_Server)
        {
            var (code, result, list) = get_latest_reset_tag_full(API_Server);
            return list;
        }
        static public (int code, string result, List<DrugHFTagClass>) get_latest_reset_tag_full(string API_Server)
        {
            string url = $"{API_Server}/api/DrugHFTag/get_latest_reset_tag";

            returnData returnData = new returnData(); // 無需傳 tagSN，只需空殼傳入

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }

            List<DrugHFTagClass> DrugHFTagClasses = returnData_out.Data.ObjToClass<List<DrugHFTagClass>>();
            return (returnData_out.Code, returnData_out.Result, DrugHFTagClasses);
        }

        /// <summary>
        /// 取得所有Tag中最新一筆且狀態為「入庫註記」的標籤資料（僅回傳資料）
        /// </summary>
        static public List<DrugHFTagClass> get_latest_stockin_tag(string API_Server)
        {
            var (code, result, list) = get_latest_stockin_tag_full(API_Server);
            return list;
        }
        /// <summary>
        /// 取得所有Tag中最新一筆且狀態為「入庫註記」的標籤資料（完整資訊）
        /// </summary>
        static public (int code, string result, List<DrugHFTagClass>) get_latest_stockin_tag_full(string API_Server)
        {
            string url = $"{API_Server}/api/DrugHFTag/get_latest_stockin_tag";

            returnData returnData = new returnData(); // 空殼傳入即可
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }

            List<DrugHFTagClass> DrugHFTagClasses = returnData_out.Data.ObjToClass<List<DrugHFTagClass>>();
            return (returnData_out.Code, returnData_out.Result, DrugHFTagClasses);
        }

        static public List<DrugHFTagClass> get_latest_stockin_eligible_tags(string API_Server)
        {
            var (code, result, list) = get_latest_stockin_eligible_tags_full(API_Server);
            return list;
        }
        static public (int code, string result, List<DrugHFTagClass>) get_latest_stockin_eligible_tags_full(string API_Server)
        {
            string url = $"{API_Server}/api/DrugHFTag/get_latest_stockin_eligible_tags";

            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }

            List<DrugHFTagClass> drugHFTagClasses = returnData_out.Data.ObjToClass<List<DrugHFTagClass>>();
            return (returnData_out.Code, returnData_out.Result, drugHFTagClasses);
        }

        static public List<DrugHFTagClass> get_latest_stockout_eligible_tags(string API_Server)
        {
            var (code, result, list) = get_latest_stockout_eligible_tags_full(API_Server);
            return list;
        }
        static public (int code, string result, List<DrugHFTagClass>) get_latest_stockout_eligible_tags_full(string API_Server)
        {
            string url = $"{API_Server}/api/DrugHFTag/get_latest_stockout_eligible_tags";

            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }

            List<DrugHFTagClass> drugHFTagClasses = returnData_out.Data.ObjToClass<List<DrugHFTagClass>>();
            return (returnData_out.Code, returnData_out.Result, drugHFTagClasses);
        }

        /// <summary>
        /// 呼叫 API，取得HF標籤統計（傳回完整三件組：code, result, summary）
        /// </summary>
        public static (int code, string result, DrugHFTagStatusSummary summary) GetStockinStatusDetailCountInRangeWithFilter(
            string API_Server,
            DateTime startDate,
            DateTime endDate,
            string filterDrugCode = "",
            string filterDrugName = "")
        {
            string url = $"{API_Server}/api/DrugHFTag/get_stockin_status_detail_count_in_range_with_filter";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(startDate.ToString("yyyy-MM-dd HH:mm:ss"));
            returnData.ValueAry.Add(endDate.ToString("yyyy-MM-dd HH:mm:ss"));
            returnData.ValueAry.Add(filterDrugCode);
            returnData.ValueAry.Add(filterDrugName);

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }

            DrugHFTagStatusSummary summary = returnData_out.Data.ObjToClass<DrugHFTagStatusSummary>();
            return (returnData_out.Code, returnData_out.Result, summary);
        }
        /// <summary>
        /// 呼叫 API，取得HF標籤統計（只回傳 summary，失敗回傳 null）
        /// </summary>
        public static DrugHFTagStatusSummary GetStockinStatusDetailSummary(
            string API_Server,
            DateTime startDate,
            DateTime endDate,
            string filterDrugCode = "",
            string filterDrugName = "")
        {
            var (code, result, summary) = GetStockinStatusDetailCountInRangeWithFilter(API_Server, startDate, endDate, filterDrugCode, filterDrugName);

            if (code == 200)
                return summary;
            else
                return null;
        }

        /// <summary>
        /// 依一批藥碼陣列，一次取得統計結果 (快速版)
        /// </summary>
        public static (int code, string result, List<DrugHFTagStatusSummaryByCode> list) GetStockinStatusSummariesByCodes(
            string API_Server,
            DateTime startDate,
            DateTime endDate,
            List<string> drugCodes)
        {
            if (drugCodes == null || drugCodes.Count == 0)
            {
                return (0, "drugCodes 為空", null);
            }

            string url = $"{API_Server}/api/DrugHFTag/get_stockin_status_detail_summary_by_codes";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(startDate.ToString("yyyy-MM-dd HH:mm:ss"));
            returnData.ValueAry.Add(endDate.ToString("yyyy-MM-dd HH:mm:ss"));
            returnData.Data = drugCodes;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);

            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return (0, "returnData_out == null", null);
            }
            if (returnData_out.Data == null)
            {
                return (0, "returnData_out.Data == null", null);
            }

            List<DrugHFTagStatusSummaryByCode> summaries = returnData_out.Data.ObjToClass<List<DrugHFTagStatusSummaryByCode>>();
            return (returnData_out.Code, returnData_out.Result, summaries);
        }


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
        public static DrugHFTagClass SerchByTagSN(this List<DrugHFTagClass> hfTags, string tagSN)
        {
            if (hfTags == null) return null;
            return hfTags.FirstOrDefault(tag => tag.TagSN.Equals(tagSN, StringComparison.OrdinalIgnoreCase));
        }
        public static List<StockClass> GetStockClasses(this List<DrugHFTagClass> hfTags)
        {
            if (hfTags == null) return new List<StockClass>();

            return hfTags
                .GroupBy(tag => new { tag.藥碼, tag.藥名, tag.效期, tag.批號 })
                .Select(group => new StockClass
                {
                    Code = group.Key.藥碼,
                    Name = group.Key.藥名,
                    Validity_period = group.Key.效期,
                    Lot_number = group.Key.批號,
                    Qty = group.Sum(tag => tag.數量.StringToDouble()).ToString()
                }).ToList();
        }
     
        /// <summary>
        /// List<DrugHFTagClass> 轉 List<object[]> (依 enum_DrugHFTag 順序)
        /// </summary>
        public static List<object[]> ToObjectList(this List<DrugHFTagClass> hfTags)
        {
            if (hfTags == null) return new List<object[]>();

            var enumFields = Enum.GetValues(typeof(enum_DrugHFTag)).Cast<enum_DrugHFTag>().ToList();
            List<object[]> result = new List<object[]>();

            foreach (var tag in hfTags)
            {
                List<object> row = new List<object>();

                foreach (var field in enumFields)
                {
                    switch (field)
                    {
                        case enum_DrugHFTag.GUID: row.Add(tag.GUID ?? ""); break;
                        case enum_DrugHFTag.TagSN: row.Add(tag.TagSN ?? ""); break;
                        case enum_DrugHFTag.藥碼: row.Add(tag.藥碼 ?? ""); break;
                        case enum_DrugHFTag.藥名: row.Add(tag.藥名 ?? ""); break;
                        case enum_DrugHFTag.效期: row.Add(tag.效期 ?? ""); break;
                        case enum_DrugHFTag.批號: row.Add(tag.批號 ?? ""); break;
                        case enum_DrugHFTag.數量: row.Add(tag.數量 ?? ""); break;
                        case enum_DrugHFTag.存放位置: row.Add(tag.存放位置 ?? ""); break;
                        case enum_DrugHFTag.操作人員: row.Add(tag.操作人員 ?? ""); break;
                        case enum_DrugHFTag.狀態: row.Add(tag.狀態 ?? ""); break;
                        case enum_DrugHFTag.更新時間: row.Add(tag.更新時間 ?? ""); break;
                    }
                }
                result.Add(row.ToArray());
            }

            return result;
        }

        /// <summary>
        /// List<object[]> 轉 List<DrugHFTagClass> (依 enum_DrugHFTag 順序)
        /// </summary>
        public static List<DrugHFTagClass> ToDrugHFTagClassList(this List<object[]> rows)
        {
            if (rows == null) return new List<DrugHFTagClass>();

            List<DrugHFTagClass> list = new List<DrugHFTagClass>();

            foreach (var row in rows)
            {
                var tag = row.ToDrugHFTagClass();
                if (tag != null) list.Add(tag);
            }

            return list;
        }

        /// <summary>
        /// object[] 轉 DrugHFTagClass (單筆轉換)
        /// </summary>
        public static DrugHFTagClass ToDrugHFTagClass(this object[] row)
        {
            if (row == null) return null;

            var enumFields = Enum.GetValues(typeof(enum_DrugHFTag)).Cast<enum_DrugHFTag>().ToList();
            if (row.Length < enumFields.Count) return null;

            DrugHFTagClass tag = new DrugHFTagClass();

            for (int i = 0; i < enumFields.Count; i++)
            {
                string value = row[i]?.ToString() ?? "";

                switch (enumFields[i])
                {
                    case enum_DrugHFTag.GUID: tag.GUID = value; break;
                    case enum_DrugHFTag.TagSN: tag.TagSN = value; break;
                    case enum_DrugHFTag.藥碼: tag.藥碼 = value; break;
                    case enum_DrugHFTag.藥名: tag.藥名 = value; break;
                    case enum_DrugHFTag.效期: tag.效期 = value; break;
                    case enum_DrugHFTag.批號: tag.批號 = value; break;
                    case enum_DrugHFTag.數量: tag.數量 = value; break;
                    case enum_DrugHFTag.存放位置: tag.存放位置 = value; break;
                    case enum_DrugHFTag.操作人員: tag.操作人員 = value; break;
                    case enum_DrugHFTag.狀態: tag.狀態 = value; break;
                    case enum_DrugHFTag.更新時間: tag.更新時間 = value; break;
                }
            }

            return tag;
        }

    }

    [EnumDescription("DrugHFTag_IncomeOutcomeList")]
    public enum enum_DrugHFTag_IncomeOutcomeList
    {
        [Description("GUID,VARCHAR,300,NONE")]
        GUID,
        [Description("藥碼,VARCHAR,300,NONE")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("報表數量,VARCHAR,300,NONE")]
        報表數量,
        [Description("收支數量,VARCHAR,300,NONE")]
        收支數量,
    }
    public class DrugHFTag_IncomeOutcomeListClass
    {
        /// <summary>唯一識別碼</summary>
        [JsonPropertyName("guid")]
        public string GUID { get; set; }

        /// <summary>藥品代碼</summary>
        [JsonPropertyName("drug_code")]
        public string 藥碼 { get; set; }

        /// <summary>藥品名稱</summary>
        [JsonPropertyName("drug_name")]
        public string 藥名 { get; set; }

        /// <summary>報表數量</summary>
        [JsonPropertyName("report_qty")]
        public string 報表數量 { get; set; }

        /// <summary>收支數量</summary>
        [JsonPropertyName("income_outcome_qty")]
        public string 收支數量 { get; set; }
    }
    public enum IncomeOutcomeMode
    {
        收入,
        支出
    }

    public static class DrugHFTagIncomeOutcomeListMethod
    {
        /// <summary>
        /// 依指定模式（收入或支出）分類標籤並生成收支清單
        /// </summary>
        /// <param name="hfTags">標籤清單</param>
        /// <param name="mode">收入或支出模式</param>
        /// <returns>收支清單</returns>
        public static List<DrugHFTag_IncomeOutcomeListClass> ToIncomeOutcomeList(this List<DrugHFTagClass> hfTags, IncomeOutcomeMode mode)
        {
            if (hfTags == null) return new List<DrugHFTag_IncomeOutcomeListClass>();

            var grouped = hfTags
                .GroupBy(tag => new { tag.藥碼, tag.藥名 })
                .Select(group =>
                {
                    double report_qty = 0;
                    double income_outcome_qty = 0;

                    foreach (var tag in group)
                    {
                        double qty = tag.數量.StringToDouble();
                        if (mode == IncomeOutcomeMode.收入)
                        {
                            // 收入模式
                            if (tag.狀態 == enum_DrugHFTagStatus.已重置.ToString() || tag.狀態 == enum_DrugHFTagStatus.入庫註記.ToString())
                            {
                                report_qty += qty;
                            }
                            if (tag.狀態 == enum_DrugHFTagStatus.入庫註記.ToString())
                            {
                                income_outcome_qty += qty;
                            }
                        }
                        else if (mode == IncomeOutcomeMode.支出)
                        {
                            // 支出模式
                            if (tag.狀態 == enum_DrugHFTagStatus.入庫註記.ToString() || tag.狀態 == enum_DrugHFTagStatus.出庫註記.ToString())
                            {
                                report_qty += qty;
                            }
                            if (tag.狀態 == enum_DrugHFTagStatus.出庫註記.ToString())
                            {
                                income_outcome_qty += qty;
                            }
                        }
                    }

                    return new DrugHFTag_IncomeOutcomeListClass
                    {
                        GUID = "", // 分組後無單一GUID
                        藥碼 = group.Key.藥碼,
                        藥名 = group.Key.藥名,
                        報表數量 = report_qty.ToString("0.###"),
                        收支數量 = income_outcome_qty.ToString("0.###")
                    };
                }).ToList();

            return grouped;
        }
        /// <summary>
        /// 同時取得「收入清單」與「支出清單」
        /// </summary>
        /// <param name="hfTags">標籤清單</param>
        /// <returns>收入清單, 支出清單</returns>
        public static (List<DrugHFTag_IncomeOutcomeListClass> incomeList, List<DrugHFTag_IncomeOutcomeListClass> outcomeList) GetIncomeAndOutcomeLists(this List<DrugHFTagClass> hfTags)
        {
            if (hfTags == null)
                return (new List<DrugHFTag_IncomeOutcomeListClass>(), new List<DrugHFTag_IncomeOutcomeListClass>());

            var incomeList = hfTags.ToIncomeOutcomeList(IncomeOutcomeMode.收入);
            var outcomeList = hfTags.ToIncomeOutcomeList(IncomeOutcomeMode.支出);
            return (incomeList, outcomeList);
        }
        /// <summary>
        /// 將收入或支出清單轉成 DataTable
        /// </summary>
        public static DataTable ToDataTable(this List<DrugHFTag_IncomeOutcomeListClass> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GUID", typeof(string));
            dt.Columns.Add("藥碼", typeof(string));
            dt.Columns.Add("藥名", typeof(string));
            dt.Columns.Add("報表數量", typeof(string));
            dt.Columns.Add("收支數量", typeof(string));

            foreach (var item in list)
            {
                var row = dt.NewRow();
                row["GUID"] = item.GUID ?? "";
                row["藥碼"] = item.藥碼 ?? "";
                row["藥名"] = item.藥名 ?? "";
                row["報表數量"] = item.報表數量 ?? "0";
                row["收支數量"] = item.收支數量 ?? "0";
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// 將收入或支出清單轉成 List<object[]>
        /// </summary>
        public static List<object[]> ToObjectList(this List<DrugHFTag_IncomeOutcomeListClass> list)
        {
            List<object[]> result = new List<object[]>();

            foreach (var item in list)
            {
                object[] row = new object[]
                {
                item.GUID ?? "",
                item.藥碼 ?? "",
                item.藥名 ?? "",
                item.報表數量 ?? "0",
                item.收支數量 ?? "0"
                };
                result.Add(row);
            }

            return result;
        }
        // List<object[]> 轉 List<DrugHFTag_IncomeOutcomeListClass>
        public static List<DrugHFTag_IncomeOutcomeListClass> ToIncomeOutcomeClassList(this List<object[]> objectList)
        {
            List<DrugHFTag_IncomeOutcomeListClass> list = new List<DrugHFTag_IncomeOutcomeListClass>();

            foreach (var obj in objectList)
            {
                if (obj.Length < Enum.GetValues(typeof(enum_DrugHFTag_IncomeOutcomeList)).Length)
                    continue; // 欄位數量不足，不轉換

                var item = obj.ToIncomeOutcomeClass(); // 這裡直接用下面的單筆轉換
                list.Add(item);
            }

            return list;
        }

        // object[] 轉 DrugHFTag_IncomeOutcomeListClass (單筆)
        public static DrugHFTag_IncomeOutcomeListClass ToIncomeOutcomeClass(this object[] obj)
        {
            if (obj == null || obj.Length < Enum.GetValues(typeof(enum_DrugHFTag_IncomeOutcomeList)).Length)
                return null; // 不合法，回傳 null

            DrugHFTag_IncomeOutcomeListClass item = new DrugHFTag_IncomeOutcomeListClass();

            for (int i = 0; i < obj.Length; i++)
            {
                string value = obj[i]?.ToString() ?? "";

                switch ((enum_DrugHFTag_IncomeOutcomeList)i)
                {
                    case enum_DrugHFTag_IncomeOutcomeList.GUID:
                        item.GUID = value;
                        break;
                    case enum_DrugHFTag_IncomeOutcomeList.藥碼:
                        item.藥碼 = value;
                        break;
                    case enum_DrugHFTag_IncomeOutcomeList.藥名:
                        item.藥名 = value;
                        break;
                    case enum_DrugHFTag_IncomeOutcomeList.報表數量:
                        item.報表數量 = value;
                        break;
                    case enum_DrugHFTag_IncomeOutcomeList.收支數量:
                        item.收支數量 = value;
                        break;
                }
            }

            return item;
        }

    }



}
