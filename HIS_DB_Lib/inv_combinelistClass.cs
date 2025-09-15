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
    public enum enum_inv_combinelist_stock_Excel
    {
        藥碼,
        庫存,
    }
    [EnumDescription("inv_combinelist")]
    public enum enum_inv_combinelist
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("合併單名稱,VARCHAR,200,None")]
        合併單名稱,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("StockRecord_GUID,VARCHAR,50,None")]
        StockRecord_GUID,
        [Description("StockRecord_ServerName,VARCHAR,30,None")]
        StockRecord_ServerName,
        [Description("StockRecord_ServerType,VARCHAR,30,None")]
        StockRecord_ServerType,
        [Description("誤差總金額上限,VARCHAR,30,None")]
        誤差總金額上限,
        [Description("誤差總金額下限,VARCHAR,30,None")]
        誤差總金額下限,
        [Description("誤差總金額致能,VARCHAR,30,None")]
        誤差總金額致能,
        [Description("誤差百分率上限,VARCHAR,30,None")]
        誤差百分率上限,
        [Description("誤差百分率下限,VARCHAR,30,None")]
        誤差百分率下限,
        [Description("誤差百分率致能,VARCHAR,30,None")]
        誤差百分率致能,
        [Description("誤差數量上限,VARCHAR,30,None")]
        誤差數量上限,
        [Description("誤差數量下限,VARCHAR,30,None")]
        誤差數量下限,
        [Description("誤差數量致能,VARCHAR,30,None")]
        誤差數量致能,
        [Description("建表人,VARCHAR,30,None")]
        建表人,
        [Description("建表時間,DATETIME,50,INDEX")]
        建表時間,
        [Description("消耗量起始時間,DATETIME,50,None")]
        消耗量起始時間,
        [Description("消耗量結束時間,DATETIME,50,None")]
        消耗量結束時間,
        [Description("備註,VARCHAR,200,None")]
        備註,
    }
    [EnumDescription("inv_sub_combinelist")]
    public enum enum_inv_sub_combinelist
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("單號,VARCHAR,30,INDEX")]
        單號,
        [Description("類型,VARCHAR,50,None")]
        類型,
        [Description("新增時間,DATETIME,200,None")]
        新增時間,
        [Description("備註,VARCHAR,200,None")]
        備註,
    }
    [EnumDescription("inv_combinelist_stock")]
    public enum enum_inv_combinelist_stock
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("藥碼,VARCHAR,30,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("數量,VARCHAR,30,NONE")]
        數量,
        [Description("加入時間,DATETIME,50,INDEX")]
        加入時間,
    }
    [EnumDescription("inv_combinelist_note")]
    public enum enum_inv_combinelist_note
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("藥碼,VARCHAR,30,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("備註,VARCHAR,300,NONE")]
        備註,
        [Description("加入時間,DATETIME,50,INDEX")]
        加入時間,
    }

    [EnumDescription("inv_combinelist_consumption")]
    public enum enum_inv_combinelist_consumption
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("藥碼,VARCHAR,30,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("數量,VARCHAR,30,NONE")]
        數量,
        [Description("加入時間,DATETIME,50,INDEX")]
        加入時間,
    }
    [EnumDescription("inv_combinelist_review")]
    public enum enum_inv_combinelist_review
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("藥碼,VARCHAR,30,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("數量,VARCHAR,30,NONE")]
        數量,
        [Description("加入時間,DATETIME,50,INDEX")]
        加入時間,
    }

    [EnumDescription("inv_combinelist_price")]
    public enum enum_inv_combinelist_price
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("藥碼,VARCHAR,30,INDEX")]
        藥碼,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("單價,VARCHAR,30,NONE")]
        單價,
        [Description("加入時間,DATETIME,50,INDEX")]
        加入時間,
    }
    [EnumDescription("inv_combinelist_report")]
    public enum enum_inv_combinelist_report
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("合併單號,VARCHAR,30,INDEX")]
        合併單號,
        [Description("藥碼,VARCHAR,30,NONE")]
        藥碼,
        [Description("料號,VARCHAR,30,NONE")]
        料號,
        [Description("藥名,VARCHAR,300,NONE")]
        藥名,
        [Description("備註,VARCHAR,300,NONE")]
        備註,
        [Description("單價,VARCHAR,30,NONE")]
        單價,
        [Description("庫存量,VARCHAR,30,NONE")]
        庫存量,
        [Description("盤點量,VARCHAR,30,NONE")]
        盤點量,
        [Description("消耗量,VARCHAR,30,NONE")]
        消耗量,
        [Description("覆盤量,VARCHAR,30,NONE")]
        覆盤量,
        [Description("庫存金額,VARCHAR,30,NONE")]
        庫存金額,
        [Description("結存金額,VARCHAR,30,NONE")]
        結存金額,
        [Description("誤差量,VARCHAR,30,NONE")]
        誤差量,
        [Description("誤差金額,VARCHAR,30,NONE")]
        誤差金額,
        [Description("誤差百分率,VARCHAR,30,NONE")]
        誤差百分率,
        [Description("註記,VARCHAR,30,NONE")]
        註記
    }
    /// <summary>
    /// 合併總單
    /// </summary>
    public class inv_combinelistClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單名稱
        /// </summary>
        [JsonPropertyName("INV_NAME")]
        public string 合併單名稱 { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("INV_SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 建表人
        /// </summary>
        [JsonPropertyName("CT")]
        public string 建表人 { get; set; }
        /// <summary>
        /// 建表時間
        /// </summary>
        [JsonPropertyName("CT_TIME")]
        public string 建表時間 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }
        /// <summary>
        /// StockRecord_GUID
        /// </summary>
        [JsonPropertyName("StockRecord_GUID")]
        public string StockRecord_GUID { get; set; }
        /// <summary>
        /// StockRecord_ServerName
        /// </summary>
        [JsonPropertyName("StockRecord_ServerName")]
        public string StockRecord_ServerName { get; set; }
        /// <summary>
        /// StockRecord_ServerType
        /// </summary>
        [JsonPropertyName("StockRecord_ServerType")]
        public string StockRecord_ServerType { get; set; }
        /// <summary>
        /// 消耗量起始時間
        /// </summary>
        [JsonPropertyName("consumption_startTime")]
        public string 消耗量起始時間 { get; set; }
        /// <summary>
        /// 消耗量起始時間
        /// </summary>
        [JsonPropertyName("consumption_endTime")]
        public string 消耗量結束時間 { get; set; }

        /// <summary>
        /// 誤差總金額上限
        /// </summary>
        [JsonPropertyName("MaxTotalErrorAmount")]
        public string 誤差總金額上限 { get; set; }
        /// <summary>
        /// 誤差總金額下限
        /// </summary>
        [JsonPropertyName("MinTotalErrorAmount")]
        public string 誤差總金額下限 { get; set; }
        /// <summary>
        /// 誤差總金額致能
        /// </summary>
        [JsonPropertyName("TotalErrorAmountEnable")]
        public string 誤差總金額致能 { get; set; }

        /// <summary>
        /// 誤差百分率上限
        /// </summary>
        [JsonPropertyName("MaxErrorPercentage")]
        public string 誤差百分率上限 { get; set; }
        /// <summary>
        /// 誤差百分率下限
        /// </summary>
        [JsonPropertyName("MinErrorPercentage")]
        public string 誤差百分率下限 { get; set; }
        /// <summary>
        /// 誤差百分率致能
        /// </summary>
        [JsonPropertyName("ErrorPercentageEnable")]
        public string 誤差百分率致能 { get; set; }


        /// <summary>
        /// 誤差數量上限
        /// </summary>
        [JsonPropertyName("MaxErrorCount")]
        public string 誤差數量上限 { get; set; }
        /// <summary>
        /// 誤差數量下限
        /// </summary>
        [JsonPropertyName("MinErrorCount")]
        public string 誤差數量下限 { get; set; }
        /// <summary>
        /// 誤差數量致能
        /// </summary>
        [JsonPropertyName("ErrorCountEnable")]
        public string 誤差數量致能 { get; set; }

        /// <summary>
        /// 合併單明細
        /// </summary>
        [JsonPropertyName("records_Ary")]
        public List<inv_records_Class> Records_Ary { get => records_Ary; set => records_Ary = value; }     
        private List<inv_records_Class> records_Ary = new List<inv_records_Class>();

        [JsonIgnore]
        public List<inventoryClass.creat> Creats
        {
            get
            {
                List<inventoryClass.creat> creats = new List<inventoryClass.creat>();
                for (int i = 0; i < Records_Ary.Count; i++)
                {
                    creats.Add(Records_Ary[i].Creat);
                }
                return creats;
            }
        }

        /// <summary>
        /// 盤點藥品總表
        /// </summary>
        [JsonPropertyName("contents")]
        public List<inventoryClass.content> Contents { get => contents; set => contents = value; }
        private List<inventoryClass.content> contents = new List<inventoryClass.content>();

        /// <summary>
        /// 參考庫存
        /// </summary>
        [JsonPropertyName("stocks")]
        public List<inv_combinelist_stock_Class> Stocks { get => stocks; set => stocks = value; }
        private List<inv_combinelist_stock_Class> stocks = new List<inv_combinelist_stock_Class>();

        /// <summary>
        /// 參考消耗
        /// </summary>
        [JsonPropertyName("consumptions")]
        public List<inv_combinelist_consumption_Class> Consumptions { get => consumptions; set => consumptions = value; }
        private List<inv_combinelist_consumption_Class> consumptions = new List<inv_combinelist_consumption_Class>();

        /// <summary>
        /// 參考單價
        /// </summary>
        [JsonPropertyName("medPrices")]
        public List<inv_combinelist_price_Class> MedPrices { get => medPrices; set => medPrices = value; }
        private List<inv_combinelist_price_Class> medPrices = new List<inv_combinelist_price_Class>();

        /// <summary>
        /// 參考單價
        /// </summary>
        [JsonPropertyName("medNotes")]
        public List<inv_combinelist_note_Class> MedNotes { get => medNotes; set => medNotes = value; }
        private List<inv_combinelist_note_Class> medNotes = new List<inv_combinelist_note_Class>();

        /// <summary>
        /// 覆盤品項
        /// </summary>
        [JsonPropertyName("medReviews")]
        public List<inv_combinelist_review_Class> MedReviews { get => medReviews; set => medReviews = value; }
        private List<inv_combinelist_review_Class> medReviews = new List<inv_combinelist_review_Class>();
        public class ICP_By_CT_time : IComparer<inv_combinelistClass>
        {
            public int Compare(inv_combinelistClass x, inv_combinelistClass y)
            {
                int result = string.Compare(x.建表時間, y.建表時間) * -1;
                return result;
            }
        }
        public bool IsHaveRecord(inventoryClass.creat creat)
        {
            List<inv_records_Class> records_Ary_buf = (from temp in records_Ary
                                                       where temp.單號 == creat.盤點單號
                                                       select temp).ToList();
            return (records_Ary_buf.Count != 0);
        }
        public void AddRecord(inventoryClass.creat creat)
        {
   
            List<inv_records_Class> records_Ary_buf = (from temp in records_Ary
                                                       where temp.單號 == creat.盤點單號
                                                       select temp).ToList();
            if(records_Ary_buf.Count == 0)
            {
                inv_records_Class inv_Records_Class = new inv_records_Class();
                inv_Records_Class.GUID = Guid.NewGuid().ToString();
                inv_Records_Class.名稱 = creat.盤點名稱;
                inv_Records_Class.單號 = creat.盤點單號;
                inv_Records_Class.類型 = "盤點單";
                inv_Records_Class.Creat = creat;
                records_Ary.Add(inv_Records_Class);
            }
        }
        public void DeleteRecord(string SN)
        {
            List<inv_records_Class> records_Ary_buf = (from temp in records_Ary
                                                       where temp.單號 != SN
                                                       select temp).ToList();
            records_Ary = records_Ary_buf;
        }
        public inv_combinelist_stock_Class GetStockByCode(string code, string stockCode = null)
        {
            return stocks.FirstOrDefault(temp => temp.藥碼 == code || temp.藥碼 == stockCode);
        }
        public inv_combinelist_price_Class GetMedPriceByCode(string code, string stockCode = null)
        {
            return medPrices.FirstOrDefault(temp => temp.藥碼 == code || temp.藥碼 == stockCode);
        }
        public inv_combinelist_note_Class GetMedNoteByCode(string code, string stockCode = null)
        {
            return medNotes.FirstOrDefault(temp => temp.藥碼 == code || temp.藥碼 == stockCode);
        }
        public inv_combinelist_review_Class GetMedReviewByCode(string code)
        {
            List<inv_combinelist_review_Class> medReview_buf = (from temp in medReviews
                                                            where temp.藥碼 == code
                                                            select temp).ToList();
            if (medReview_buf.Count == 0) return null;
            return medReview_buf[0];
        }
        public inv_combinelist_consumption_Class GetConsumptionsByCode(string code)
        {
            List<inv_combinelist_consumption_Class> consumptions_buf = (from temp in consumptions
                                                                        where temp.藥碼 == code
                                                            select temp).ToList();
            if (consumptions_buf.Count == 0) return null;
            return consumptions_buf[0];
        }
        public void get_all_full_creat(string API_Server)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < Records_Ary.Count; i++)
            {
                inv_records_Class inv_Records_Class = Records_Ary[i];
                tasks.Add(Task.Run(new Action(delegate 
                {
                    inventoryClass.creat creat =  inventoryClass.creat_get_by_IC_SN(API_Server, inv_Records_Class.單號);
                    inv_Records_Class.Creat = creat;
                })));
            }
            Task.WhenAll(tasks).Wait();
        }

        static public List<inv_records_Class> get_all_records(string API_Server)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_all_records";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_records_Class> inv_Records_Classes = returnData.Data.ObjToClass<List<inv_records_Class>>();
            if (inv_Records_Classes == null) return null;
            if (inv_Records_Classes.Count == 0) return null;
            return inv_Records_Classes;
        }
        static public inv_combinelistClass get_all_inv(string API_Server, string SN)
        {
            List<inv_combinelistClass> inv_CombinelistClasses = get_all_inv(API_Server);
            inv_CombinelistClasses = (from temp in inv_CombinelistClasses
                                      where temp.合併單號 == SN
                                      select temp).ToList();
            if (inv_CombinelistClasses.Count == 0) return null;
            return inv_CombinelistClasses[0];
        }
        static public List<inv_combinelistClass> get_all_inv(string API_Server, DateTime dateTime_st, DateTime dateTime_end)
        {
            List<inv_combinelistClass> inv_CombinelistClasses = get_all_inv(API_Server);
            inv_CombinelistClasses = (from temp in inv_CombinelistClasses
                                      where (temp.建表時間.StringToDateTime() >= dateTime_st) && temp.建表時間.StringToDateTime() <= dateTime_end
                                      select temp).ToList();
            return inv_CombinelistClasses;
        }
        static public List<inv_combinelistClass> get_all_inv(string API_Server)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_all_inv";
            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_combinelistClass> inv_CombinelistClasses = returnData.Data.ObjToClass<List<inv_combinelistClass>>();
            if (inv_CombinelistClasses == null) return null;
            if (inv_CombinelistClasses.Count == 0) return null;
            return inv_CombinelistClasses;
        }
        static public inv_combinelistClass inv_creat_update(string API_Server, inv_combinelistClass inv_CombinelistClass)
        {
            string url = $"{API_Server}/api/inv_combinelist/inv_creat_update";
            returnData returnData = new returnData();
            returnData.Data = inv_CombinelistClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            inv_CombinelistClass = returnData.Data.ObjToClass<inv_combinelistClass>();
            if (inv_CombinelistClass == null) return null;
            return inv_CombinelistClass;
        }
        static public inv_combinelistClass get_full_inv_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_full_inv_by_SN";
            returnData returnData = new returnData();
            returnData.Value = SN;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            inv_combinelistClass inv_CombinelistClass = returnData.Data.ObjToClass<inv_combinelistClass>();
            return inv_CombinelistClass;
        }
        static public List<System.Data.DataTable> get_full_inv_DataTable_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_full_inv_DataTable_by_SN";
            returnData returnData = new returnData();
            returnData.Value = SN;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            string dts_xml = returnData.Data.ObjToClass<string>();
            List<System.Data.DataTable> dataTables = dts_xml.JsonDeserializeToDataTables();
            return dataTables;
        }
        static public List<System.Data.DataTable> get_dbvm_full_inv_DataTable_by_SN(string url, string SN)
        {
            returnData returnData = new returnData();
            returnData.Value = SN;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            string dts_xml = returnData.Data.ObjToClass<string>();
            List<System.Data.DataTable> dataTables = dts_xml.JsonDeserializeToDataTables();
            return dataTables;
        }
        
        static public byte[] get_full_inv_Excel_by_SN(string API_Server, string SN , params string[] remove_col_name)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_full_inv_Excel_by_SN";
            returnData returnData = new returnData();
            returnData.Value = SN;
            if(remove_col_name != null) returnData.ValueAry = remove_col_name.ToList();
            string json_in = returnData.JsonSerializationt();
            byte[] bytes = Basic.Net.WEBApiPostDownloaFile(url, json_in);
            return bytes;
        }
        static public void inv_stockrecord_update_by_GUID(string API_Server, string GUID, string StockRecord_GUID, string StockRecord_ServerName, string StockRecord_ServerType)
        {
            string url = $"{API_Server}/api/inv_combinelist/inv_stockrecord_update_by_GUID";
            returnData returnData = new returnData();
            returnData.ValueAry.Add(GUID);
            returnData.ValueAry.Add(StockRecord_GUID);
            returnData.ValueAry.Add(StockRecord_ServerName);
            returnData.ValueAry.Add(StockRecord_ServerType);

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }
        static public void inv_consumption_time_update_by_GUID(string API_Server, string GUID, DateTime dateTime_start , DateTime dateTime_end)
        {
            string url = $"{API_Server}/api/inv_combinelist/inv_consumption_time_update_by_GUID";
            returnData returnData = new returnData();
            returnData.ValueAry.Add(GUID);
            returnData.ValueAry.Add(dateTime_start.ToDateTimeString_6());
            returnData.ValueAry.Add(dateTime_end.ToDateTimeString_6());

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }
        static public void inv_delete_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/inv_delete_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }

        static public void add_stocks_by_SN(string API_Server, string SN , List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes)
        {
            string url = $"{API_Server}/api/inv_combinelist/add_stocks_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";
            returnData.Data = inv_Combinelist_Stock_Classes;

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }
        static public List<inv_combinelist_stock_Class> get_stocks_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_stocks_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes = returnData.Data.ObjToClass<List<inv_combinelist_stock_Class>>();
            return inv_Combinelist_Stock_Classes;
        }

        static public void add_medPrices_by_SN(string API_Server, string SN, List<inv_combinelist_price_Class> inv_Combinelist_Price_Classes)
        {
            string url = $"{API_Server}/api/inv_combinelist/add_medPrices_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";
            returnData.Data = inv_Combinelist_Price_Classes;

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }
        static public List<inv_combinelist_price_Class> get_medPrices_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_medPrices_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_combinelist_price_Class> inv_Combinelist_Stock_Classes = returnData.Data.ObjToClass<List<inv_combinelist_price_Class>>();
            return inv_Combinelist_Stock_Classes;
        }

        static public void add_medNote_by_SN(string API_Server, string SN, List<inv_combinelist_note_Class> inv_Combinelist_Note_Classes)
        {
            string url = $"{API_Server}/api/inv_combinelist/add_medNote_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";
            returnData.Data = inv_Combinelist_Note_Classes;

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }
        static public List<inv_combinelist_note_Class> get_medNote_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_medNote_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_combinelist_note_Class> inv_Combinelist_note_Classes = returnData.Data.ObjToClass<List<inv_combinelist_note_Class>>();
            return inv_Combinelist_note_Classes;
        }

        static public void add_medReview_by_SN(string API_Server, string SN, inv_combinelist_review_Class inv_Combinelist_Review_Class)
        {
            string url = $"{API_Server}/api/inv_combinelist/add_medReview_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";
            returnData.Data = inv_Combinelist_Review_Class;

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }
        static public List<inv_combinelist_review_Class> get_medReview_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_medReview_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_combinelist_review_Class> inv_Combinelist_review_Classes = returnData.Data.ObjToClass<List<inv_combinelist_review_Class>>();
            return inv_Combinelist_review_Classes;
        }
    }
    public class ScheduledCountClass
    {
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 料號
        /// </summary>
        [JsonPropertyName("SKDIACODE")]
        public string 料號 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 庫存量
        /// </summary>
        [JsonPropertyName("QTY")]
        public string 庫存量 { get; set; }
        /// <summary>
        /// 節存量
        /// </summary>
        [JsonPropertyName("INV")]
        public string 結存量 { get; set; }
        /// <summary>
        /// 盤差量
        /// </summary>
        [JsonPropertyName("Discrepancy")]
        public string 盤差量 { get; set; }
        /// <summary>
        /// 批號
        /// </summary>
        [JsonPropertyName("List_Lot_number")]
        public List<string> 批號 { get; set; }
        /// <summary>
        /// 效期
        /// </summary>
        [JsonPropertyName("List_Validity_period")]
        public List<string> 效期 { get; set; }



    }
    public static class inv_combinelistClassMethod
    {
        static public System.Collections.Generic.Dictionary<string, List<inv_combinelist_stock_Class>> CoverToDictionaryByCode(this List<inv_combinelist_stock_Class> inv_Combinelist_Stock_Classes)
        {
            Dictionary<string, List<inv_combinelist_stock_Class>> dictionary = new Dictionary<string, List<inv_combinelist_stock_Class>>();

            foreach (var item in inv_Combinelist_Stock_Classes)
            {
                string key = item.藥碼;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<inv_combinelist_stock_Class> values = new List<inv_combinelist_stock_Class> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<inv_combinelist_stock_Class> SortDictionaryByCode(this System.Collections.Generic.Dictionary<string, List<inv_combinelist_stock_Class>> dictionary, string code)
        {
            if (dictionary.ContainsKey(code))
            {
                return dictionary[code];
            }
            return new List<inv_combinelist_stock_Class>();
        }

        static public System.Collections.Generic.Dictionary<string, List<inv_combinelist_consumption_Class>> CoverToDictionaryByCode(this List<inv_combinelist_consumption_Class> inv_Combinelist_Consumption_Classes)
        {
            Dictionary<string, List<inv_combinelist_consumption_Class>> dictionary = new Dictionary<string, List<inv_combinelist_consumption_Class>>();

            foreach (var item in inv_Combinelist_Consumption_Classes)
            {
                string key = item.藥碼;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<inv_combinelist_consumption_Class> values = new List<inv_combinelist_consumption_Class> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<inv_combinelist_consumption_Class> SortDictionaryByCode(this System.Collections.Generic.Dictionary<string, List<inv_combinelist_consumption_Class>> dictionary, string code)
        {
            if (dictionary.ContainsKey(code))
            {
                return dictionary[code];
            }
            return new List<inv_combinelist_consumption_Class>();
        }

        static public System.Collections.Generic.Dictionary<string, List<inv_combinelist_review_Class>> CoverToDictionaryByCode(this List<inv_combinelist_review_Class> inv_Combinelist_review_Classes)
        {
            Dictionary<string, List<inv_combinelist_review_Class>> dictionary = new Dictionary<string, List<inv_combinelist_review_Class>>();

            foreach (var item in inv_Combinelist_review_Classes)
            {
                string key = item.藥碼;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<inv_combinelist_review_Class> values = new List<inv_combinelist_review_Class> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<inv_combinelist_review_Class> SortDictionaryByCode(this System.Collections.Generic.Dictionary<string, List<inv_combinelist_review_Class>> dictionary, string code)
        {
            if (dictionary.ContainsKey(code))
            {
                return dictionary[code];
            }
            return new List<inv_combinelist_review_Class>();
        }

        static public System.Collections.Generic.Dictionary<string, List<inv_combinelist_price_Class>> CoverToDictionaryByCode(this List<inv_combinelist_price_Class> inv_Combinelist_price_Classes)
        {
            Dictionary<string, List<inv_combinelist_price_Class>> dictionary = new Dictionary<string, List<inv_combinelist_price_Class>>();

            foreach (var item in inv_Combinelist_price_Classes)
            {
                string key = item.藥碼;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<inv_combinelist_price_Class> values = new List<inv_combinelist_price_Class> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<inv_combinelist_price_Class> SortDictionaryByCode(this System.Collections.Generic.Dictionary<string, List<inv_combinelist_price_Class>> dictionary, string code)
        {
            if (dictionary.ContainsKey(code))
            {
                return dictionary[code];
            }
            return new List<inv_combinelist_price_Class>();
        }

        static public System.Collections.Generic.Dictionary<string, List<inv_combinelist_note_Class>> CoverToDictionaryByCode(this List<inv_combinelist_note_Class> inv_Combinelist_note_Classes)
        {
            Dictionary<string, List<inv_combinelist_note_Class>> dictionary = new Dictionary<string, List<inv_combinelist_note_Class>>();

            foreach (var item in inv_Combinelist_note_Classes)
            {
                string key = item.藥碼;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<inv_combinelist_note_Class> values = new List<inv_combinelist_note_Class> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<inv_combinelist_note_Class> SortDictionaryByCode(this System.Collections.Generic.Dictionary<string, List<inv_combinelist_note_Class>> dictionary, string code)
        {
            if (dictionary.ContainsKey(code))
            {
                return dictionary[code];
            }
            return new List<inv_combinelist_note_Class>();
        }

        static public void add_medNote_by_SN(string API_Server, string SN, List<inv_combinelist_note_Class> inv_Combinelist_Note_Classes)
        {
            string url = $"{API_Server}/api/inv_combinelist/add_medNote_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";
            returnData.Data = inv_Combinelist_Note_Classes;

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return;
            }
        }
        static public List<inv_combinelist_note_Class> get_medNote_by_SN(string API_Server, string SN)
        {
            string url = $"{API_Server}/api/inv_combinelist/get_medNote_by_SN";
            returnData returnData = new returnData();
            returnData.Value = $"{SN}";

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            if (returnData == null) return null;
            if (returnData.Code != 200)
            {
                Console.WriteLine($"-----------------------------------------------");
                Console.WriteLine($"url : {url}");
                Console.WriteLine($"Result : {returnData.Result}");
                Console.WriteLine($"-----------------------------------------------");
                return null;
            }
            List<inv_combinelist_note_Class> inv_Combinelist_note_Classes = returnData.Data.ObjToClass<List<inv_combinelist_note_Class>>();
            return inv_Combinelist_note_Classes;
        }
    }
    /// <summary>
    /// 合併單明細
    /// </summary>
    public class inv_records_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 單號 { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 名稱 { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        [JsonPropertyName("TYPE")]
        public string 類型 { get; set; }

        /// <summary>
        /// 盤點表(完整)
        /// </summary>
        [JsonPropertyName("creat")]
        public inventoryClass.creat Creat { get => creat; set => creat = value; }
        private inventoryClass.creat creat = new inventoryClass.creat();
    }

    /// <summary>
    /// 合併單庫存明細
    /// </summary>
    public class inv_combinelist_stock_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("QTY")]
        public string 數量 { get; set; }
        /// <summary>
        /// 加入時間
        /// </summary>
        [JsonPropertyName("ADD_TIME")]
        public string 加入時間 { get; set; }

    }

    /// <summary>
    /// 合併單消耗明細
    /// </summary>
    public class inv_combinelist_consumption_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("QTY")]
        public string 數量 { get; set; }
        /// <summary>
        /// 加入時間
        /// </summary>
        [JsonPropertyName("ADD_TIME")]
        public string 加入時間 { get; set; }

    }

    /// <summary>
    /// 合併單覆盤量明細
    /// </summary>
    public class inv_combinelist_review_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("QTY")]
        public string 數量 { get; set; }
        /// <summary>
        /// 加入時間
        /// </summary>
        [JsonPropertyName("ADD_TIME")]
        public string 加入時間 { get; set; }

    }

    /// <summary>
    /// 合併單藥品單價
    /// </summary>
    public class inv_combinelist_price_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("price")]
        public string 單價 { get; set; }
        /// <summary>
        /// 加入時間
        /// </summary>
        [JsonPropertyName("ADD_TIME")]
        public string 加入時間 { get; set; }
    }

    /// <summary>
    /// 合併單藥品備註
    /// </summary>
    public class inv_combinelist_note_Class
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [JsonPropertyName("note")]
        public string 備註 { get; set; }
        /// <summary>
        /// 加入時間
        /// </summary>
        [JsonPropertyName("ADD_TIME")]
        public string 加入時間 { get; set; }
    }

    /// <summary>
    /// 合併單總表
    /// </summary>
    public class inv_combinelist_report_Class
    {
        /// <summary>
        /// GUID
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// 合併單號
        /// </summary>
        [JsonPropertyName("SN")]
        public string 合併單號 { get; set; }
        /// <summary>
        /// 藥碼
        /// </summary>
        [JsonPropertyName("CODE")]
        public string 藥碼 { get; set; }
        /// <summary>
        /// 料號
        /// </summary>
        [JsonPropertyName("SKDIACODE")]
        public string 料號 { get; set; }
        /// <summary>
        /// 藥名
        /// </summary>
        [JsonPropertyName("NAME")]
        public string 藥名 { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("ALIAS")]
        public string 備註 { get; set; }
        /// <summary>
        /// 單價
        /// </summary>
        [JsonPropertyName("PRICE")]
        public string 單價 { get; set; }
        /// <summary>
        /// 庫存量
        /// </summary>
        [JsonPropertyName("QTY")]
        public string 庫存量 { get; set; }
        /// <summary>
        /// 盤點量
        /// </summary>
        [JsonPropertyName("COUNT")]
        public string 盤點量 { get; set; }
        /// <summary>
        /// 消耗量
        /// </summary>
        [JsonPropertyName("consumption")]
        public string 消耗量 { get; set; }
        /// <summary>
        /// 覆盤量
        /// </summary>
        [JsonPropertyName("REVIEW")]
        public string 覆盤量 { get; set; }
        /// <summary>
        /// 庫存金額
        /// </summary>
        [JsonPropertyName("STOCK")]
        public string 庫存金額 { get; set; }
        /// <summary>
        /// 結存金額
        /// </summary>
        [JsonPropertyName("BALANCE")]
        public string 結存金額 { get; set; }
        /// <summary>
        /// 誤差量
        /// </summary>
        [JsonPropertyName("ERROR")]
        public string 誤差量 { get; set; }
        /// <summary>
        /// 誤差金額
        /// </summary>
        [JsonPropertyName("ERROR_MONEY")]
        public string 誤差金額 { get; set; }
        /// <summary>
        /// 誤差百分率
        /// </summary>
        [JsonPropertyName("ERROR_PERCENT")]
        public string 誤差百分率 { get; set; }
        /// <summary>
        /// 註記
        /// </summary>
        [JsonPropertyName("COMMENT")]
        public string 註記 { get; set; }
    }
}
