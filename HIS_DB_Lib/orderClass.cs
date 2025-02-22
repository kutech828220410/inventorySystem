using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Reflection;

namespace HIS_DB_Lib
{
    public enum enum_醫囑資料_狀態
    {
        未過帳,
        已過帳,
        庫存不足,
        無儲位,
    }

    [EnumDescription("order_list")]
    public enum enum_醫囑資料
    {
        [Description("GUID,VARCHAR,100,PRIMARY")]
        GUID,
        [Description("PRI_KEY,VARCHAR,200,INDEX")]
        PRI_KEY,
        [Description("藥局代碼,VARCHAR,15,NONE")]
        藥局代碼,
        [Description("藥袋條碼,VARCHAR,200,NONE")]
        藥袋條碼,
        [Description("藥袋類型,VARCHAR,15,NONE")]
        藥袋類型,
        [Description("藥品碼,VARCHAR,10,INDEX")]
        藥品碼,
        [Description("藥品名稱,VARCHAR,200,NONE")]
        藥品名稱,
        [Description("住院序號,VARCHAR,15,INDEX")]
        住院序號,
        [Description("領藥號,VARCHAR,15,INDEX")]
        領藥號,
        [Description("批序,VARCHAR,15,NONE")]
        批序,
        [Description("天數,VARCHAR,15,NONE")]
        天數,
        [Description("單次劑量,VARCHAR,10,NONE")]
        單次劑量,
        [Description("劑量單位,VARCHAR,10,NONE")]
        劑量單位,
        [Description("途徑,VARCHAR,10,NONE")]
        途徑,
        [Description("頻次,VARCHAR,10,NONE")]
        頻次,
        [Description("費用別,VARCHAR,10,NONE")]
        費用別,
        [Description("病房,VARCHAR,10,INDEX")]
        病房,
        [Description("床號,VARCHAR,10,NONE")]
        床號,
        [Description("病人姓名,VARCHAR,50,NONE")]
        病人姓名,
        [Description("病歷號,VARCHAR,20,INDEX")]
        病歷號,
        [Description("醫師代碼,VARCHAR,20,NONE")]
        醫師代碼,
        [Description("科別,VARCHAR,20,NONE")]
        科別,
        [Description("交易量,VARCHAR,15,NONE")]
        交易量,
        [Description("實際調劑量,VARCHAR,15,NONE")]
        實際調劑量,
        [Description("開方日期,DATETIME,20,INDEX")]
        開方日期,
        [Description("結方日期,DATETIME,20,NONE")]
        結方日期,
        [Description("展藥時間,DATETIME,20,NONE")]
        展藥時間,
        [Description("產出時間,DATETIME,20,INDEX")]
        產出時間,
        [Description("過帳時間,DATETIME,20,INDEX")]
        過帳時間,
        [Description("就醫時間,DATETIME,20,INDEX")]
        就醫時間,
        [Description("藥師姓名,VARCHAR,50,INDEX")]
        藥師姓名,
        [Description("藥師ID,VARCHAR,20,NONE")]
        藥師ID,
        [Description("核對姓名,VARCHAR,50,INDEX")]
        核對姓名,
        [Description("核對ID,VARCHAR,15,INDEX")]
        核對ID,
        [Description("領藥姓名,VARCHAR,50,INDEX")]
        領藥姓名,
        [Description("領藥ID,VARCHAR,20,NONE")]
        領藥ID,
        [Description("狀態,VARCHAR,15,NONE")]
        狀態,
        [Description("備註,VARCHAR,300,NONE")]
        備註,
    }
    public class OrderClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("PRI_KEY")]
        public string PRI_KEY { get; set; }
        [JsonPropertyName("PHARM_CODE")]
        public string 藥局代碼 { get; set; }
        [JsonPropertyName("MED_BAG_SN")]
        public string 藥袋條碼 { get; set; }
        [JsonPropertyName("BRYPE")]
        public string 藥袋類型 { get; set; }
        [JsonPropertyName("CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("NAME")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("MED_BAG_NUM")]
        public string 領藥號 { get; set; }
        [JsonPropertyName("HCASENO")]
        public string 住院序號 { get; set; }
        [JsonPropertyName("UDORDSEQ")]
        public string 就醫序號 { get; set; }
        [JsonPropertyName("HCASETYP")]
        public string 就醫類別 { get; set; }
        [JsonPropertyName("DOS")]
        public string 批序 { get; set; }
        /// <summary>
        /// 天數
        /// </summary>
        [JsonPropertyName("DAYS")]
        public string 天數 { get; set; }
        [JsonPropertyName("SD")]
        public string 單次劑量 { get; set; }
        [JsonPropertyName("DUNIT")]
        public string 劑量單位 { get; set; }
        [JsonPropertyName("RROUTE")]
        public string 途徑 { get; set; }
        [JsonPropertyName("FREQ")]
        public string 頻次 { get; set; }
        [JsonPropertyName("CTYPE")]
        public string 費用別 { get; set; }
        [JsonPropertyName("WARD")]
        public string 病房 { get; set; }
        [JsonPropertyName("BEDNO")]
        public string 床號 { get; set; }
        [JsonPropertyName("PATNAME")]
        public string 病人姓名 { get; set; }
        [JsonPropertyName("PATCODE")]
        public string 病歷號 { get; set; }
        [JsonPropertyName("TXN_QTY")]
        public string 交易量 { get; set; }
        /// <summary>
        /// 實際調劑量
        /// </summary>
        [JsonPropertyName("DISP_QTY")]
        public string 實際調劑量 { get; set; }
        [JsonPropertyName("DOCID")]
        public string 醫師代碼 { get; set; }
        [JsonPropertyName("SECTNO")]
        public string 科別 { get; set; }
        [JsonPropertyName("ORD_START")]
        public string 開方日期 { get; set; }
        [JsonPropertyName("ORD_END")]
        public string 結方日期 { get; set; }
        [JsonPropertyName("EXT_TIME")]
        public string 展藥時間 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 產出時間 { get; set; }
        [JsonPropertyName("POST_TIME")]
        public string 過帳時間 { get; set; }
        [JsonPropertyName("VISITDT_TIME")]
        public string 就醫時間 { get; set; }
        [JsonPropertyName("PHARER_NAME")]
        public string 藥師姓名 { get; set; }
        [JsonPropertyName("PHARER_ID")]
        public string 藥師ID { get; set; }
        /// <summary>
        /// 核對姓名
        /// </summary>
        [JsonPropertyName("CHK_NAME")]
        public string 核對姓名 { get; set; }
        /// <summary>
        /// 核對ID
        /// </summary>
        [JsonPropertyName("CHK_ID")]
        public string 核對ID { get; set; }
        [JsonPropertyName("TAKER_NAME")]
        public string 領藥姓名 { get; set; }
        [JsonPropertyName("TAKER_ID")]
        public string 領藥ID { get; set; }
        [JsonPropertyName("STATE")]
        public string 狀態 { get; set; }
        [JsonPropertyName("NOTE")]
        public string 備註 { get; set; }

        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/order/init";

            returnData returnData = new returnData();
            string tableName = "";

            returnData.TableName = tableName;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        static public SQLUI.Table init(string API_Server, string ServerName, string ServerType)
        {
            string url = $"{API_Server}/api/order/init";

            returnData returnData = new returnData();
            string tableName = "";

            returnData.TableName = tableName;
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        static public List<OrderClass> get_by_rx_time_st_end(string API_Server, DateTime dateTime_st, DateTime dateTime_end)
        {
            string url = $"{API_Server}/api/order/get_by_rx_time_st_end";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(dateTime_st.ToDateTimeString_6());
            returnData.ValueAry.Add(dateTime_end.ToDateTimeString_6());


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            // Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public List<OrderClass> get_by_post_time_st_end(string API_Server, DateTime dateTime_st, DateTime dateTime_end)
        {
            string url = $"{API_Server}/api/order/get_by_post_time_st_end";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(dateTime_st.ToDateTimeString_6());
            returnData.ValueAry.Add(dateTime_end.ToDateTimeString_6());


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public List<OrderClass> get_by_pri_key(string API_Server, string PRI_KEY)
        {
            string url = $"{API_Server}/api/order/get_by_pri_key";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(PRI_KEY);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public OrderClass get_by_guid(string API_Server, string value)
        {
            string url = $"{API_Server}/api/order/get_by_guid";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(value);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            OrderClass OrderClass = returnData_out.Data.ObjToClass<OrderClass>();
            return OrderClass;
        }
        static public List<OrderClass> get_by_MED_BAG_NUM(string API_Server, string value)
        {
            string url = $"{API_Server}/api/order/get_by_MED_BAG_NUM";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(value);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public List<OrderClass> get_by_PATCODE(string API_Server, string value)
        {
            string url = $"{API_Server}/api/order/get_by_PATCODE";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(value);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public List<OrderClass> get_by_PATCODE(string API_Server, string value, DateTime st_dateTime, DateTime end_dateTime)
        {
            List<OrderClass> OrderClasses = get_by_PATCODE(API_Server, value);
            OrderClasses = (from temp in OrderClasses
                            where temp.開方日期.StringToDateTime() > st_dateTime && temp.開方日期.StringToDateTime() < end_dateTime
                            select temp).ToList();
            return OrderClasses;
        }
        static public List<OrderClass> get_by_PATNAME(string API_Server, string value)
        {
            string url = $"{API_Server}/api/order/get_by_PATNAME";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(value);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public List<OrderClass> get_by_PATNAME(string API_Server, string value, DateTime st_dateTime, DateTime end_dateTime)
        {
            List<OrderClass> OrderClasses = get_by_PATNAME(API_Server, value);
            OrderClasses = (from temp in OrderClasses
                            where temp.開方日期.StringToDateTime() > st_dateTime && temp.開方日期.StringToDateTime() < end_dateTime
                            select temp).ToList();
            return OrderClasses;
        }


        static public List<OrderClass> get_header_by_MED_BAG_NUM(string API_Server, string value, DateTime dateTime)
        {

            return get_header_by_MED_BAG_NUM(API_Server, value, dateTime.GetStartDate(), dateTime.GetEndDate());
        }
        static public List<OrderClass> get_header_by_MED_BAG_NUM(string API_Server, string value, DateTime st_dateTime, DateTime end_dateTime)
        {
            List<OrderClass> OrderClasses = get_header_by_MED_BAG_NUM(API_Server, value);
            OrderClasses = (from temp in OrderClasses
                             where temp.開方日期.StringToDateTime() > st_dateTime && temp.開方日期.StringToDateTime() < end_dateTime
                             select temp).ToList();
            return OrderClasses;
        }
        static public List<OrderClass> get_header_by_MED_BAG_NUM(string API_Server, string value)
        {
            string url = $"{API_Server}/api/order/get_header_by_MED_BAG_NUM";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(value);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }

        static public List<OrderClass> get_header_by_PATCODE(string API_Server, string value, DateTime dateTime)
        {

            return get_header_by_PATCODE(API_Server, value, dateTime.GetStartDate(), dateTime.GetEndDate());
        }
        static public List<OrderClass> get_header_by_PATCODE(string API_Server, string value, DateTime st_dateTime, DateTime end_dateTime)
        {
            List<OrderClass> OrderClasses = get_header_by_PATCODE(API_Server, value);
            OrderClasses = (from temp in OrderClasses
                             where temp.開方日期.StringToDateTime() > st_dateTime && temp.開方日期.StringToDateTime() < end_dateTime
                             select temp).ToList();
            return OrderClasses;
        }
        static public List<OrderClass> get_header_by_PATCODE(string API_Server, string value)
        {
            string url = $"{API_Server}/api/order/get_header_by_PATCODE";

            returnData returnData = new returnData();
            returnData.ValueAry.Add(value);


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }

        static public List<OrderClass> get_API_by_MRN(string url, string mrn , DateTime dt_st, DateTime dt_end)
        {

            returnData returnData = new returnData();
            returnData.ValueAry.Add(mrn);
            returnData.ValueAry.Add(dt_st.ToDateTimeString_6());
            returnData.ValueAry.Add(dt_end.ToDateTimeString_6());


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public List<OrderClass> get_API_by_MRN(string url, string mrn)
        {

            returnData returnData = new returnData();
            returnData.ValueAry.Add(mrn);


            string json_out = Net.WEBApiGet($"{url}{mrn}");
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }
        static public List<OrderClass> get_API_by_BAG_NUM(string url, string BAG_NUM ,DateTime dateTime)
        {

            returnData returnData = new returnData();
            returnData.ValueAry.Add(BAG_NUM);
            returnData.ValueAry.Add(dateTime.ToDateTimeString());



            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");
            List<OrderClass> OrderClasses = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses;
        }

        static public void updete_by_guid(string API_Server, OrderClass OrderClass)
        {
            List<OrderClass> OrderClasse = new List<OrderClass>();
            OrderClasse.Add(OrderClass);
            updete_by_guid(API_Server, OrderClasse);
        }
        static public void updete_by_guid(string API_Server, List<OrderClass> OrderClasses)
        {
            string url = $"{API_Server}/api/order/updete_by_guid";

            returnData returnData = new returnData();
            returnData.Data = OrderClasses;


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {

            }
            if (returnData_out.Data == null)
            {

            }
            Console.WriteLine($"{returnData_out}");
            OrderClass OrderClass = returnData_out.Data.ObjToClass<OrderClass>();

        }
        static public void delete_by_guid(string API_Server, OrderClass OrderClass)
        {
            List<OrderClass> OrderClasse = new List<OrderClass>();
            OrderClasse.Add(OrderClass);
            delete_by_guid(API_Server, OrderClasse);
        }
        static public void delete_by_guid(string API_Server, List<OrderClass> OrderClasses)
        {
            string url = $"{API_Server}/api/order/delete_by_guid";

            returnData returnData = new returnData();
            returnData.Data = OrderClasses;


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {

            }
            if (returnData_out.Data == null)
            {

            }
            Console.WriteLine($"{returnData_out}");
            OrderClass OrderClass = returnData_out.Data.ObjToClass<OrderClass>();

        }

        static public List<OrderClass> add(string API_Server, OrderClass OrderClass)
        {
            List<OrderClass> OrderClasse = new List<OrderClass>();
            OrderClasse.Add(OrderClass);
            return add(API_Server, OrderClasse);
        }
        static public List<OrderClass> add(string API_Server, List<OrderClass> OrderClasses)
        {
            string url = $"{API_Server}/api/order/add";

            returnData returnData = new returnData();
            returnData.Data = OrderClasses;


            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {

            }
            if (returnData_out.Data == null)
            {

            }
            Console.WriteLine($"{returnData_out}");
            OrderClass OrderClass = returnData_out.Data.ObjToClass<OrderClass>();
            List<OrderClass> OrderClasses_out = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return OrderClasses_out;
        }

        static public List<OrderClass> update_order_list(string API_Server, List<OrderClass> OrderClasses)
        {
            string url = $"{API_Server}/api/order/update_order_list";

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

        static public (int code,string result ,List<OrderClass> orderClasses) add_and_updete_by_guid(string API_Server, string ServerName, string ServerType, List<OrderClass> OrderClasses)
        {
            string url = $"{API_Server}/api/order/add_and_updete_by_guid";

            returnData returnData = new returnData();
            returnData.Data = OrderClasses;
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {

            }
            if (returnData_out.Data == null)
            {

            }
            List<OrderClass> OrderClasses_out = returnData_out.Data.ObjToClass<List<OrderClass>>();
            return (returnData_out.Code, returnData_out.Result, OrderClasses_out);
        }

    }

    static public class OrderClassMethod
    {
        public enum SortType
        {
            批序,
            開方日期,
            產出時間,
            領藥號
        }

        static public void sort(this List<OrderClass> OrderClasses, SortType sortType)
        {
            if (OrderClasses == null) return;
            if (sortType == SortType.開方日期)
            {
                OrderClasses.Sort(new ICP_By_rx_time());
            }
            if (sortType == SortType.產出時間)
            {
                OrderClasses.Sort(new ICP_By_op_time());
            }
            if (sortType == SortType.領藥號)
            {
                OrderClasses.Sort(new ICP_By_MED_BAG_NUM());
            }
            if (sortType == SortType.批序)
            {
                OrderClasses.Sort(new ICP_By_DOS());
            }
        }

        public class ICP_By_rx_time : IComparer<OrderClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(OrderClass x, OrderClass y)
            {
                DateTime datetime1 = x.開方日期.StringToDateTime();
                DateTime datetime2 = y.開方日期.StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                return compare;

            }
        }
        public class ICP_By_op_time : IComparer<OrderClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(OrderClass x, OrderClass y)
            {
                DateTime datetime1 = x.產出時間.StringToDateTime();
                DateTime datetime2 = y.產出時間.StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                return compare;

            }
        }
        public class ICP_By_MED_BAG_NUM : IComparer<OrderClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(OrderClass x, OrderClass y)
            {
                string temp0 = x.領藥號;
                string temp1 = y.領藥號;
                int compare = temp0.CompareTo(temp1);
                return compare;

            }
        }
        public class ICP_By_de_MED_BAG_NUM : IComparer<OrderClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(OrderClass x, OrderClass y)
            {
                string temp0 = x.領藥號;
                string temp1 = y.領藥號;
                int compare = temp1.CompareTo(temp0);
                return compare;

            }
        }
        public class ICP_By_DOS : IComparer<OrderClass>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(OrderClass x, OrderClass y)
            {
                int temp0 = x.批序.StringToInt32();
                int temp1 = y.批序.StringToInt32();
                int compare = temp0.CompareTo(temp1);
                return compare;

            }
        }
        static public bool GetFreqIsDone(this List<OrderClass> OrderClasses, string freq)
        {
            List<OrderClass> OrderClasses_buf = (from temp in OrderClasses
                                                   where temp.頻次 == freq
                                                   where temp.實際調劑量.StringIsDouble() == false
                                                   select temp).ToList();
            return (OrderClasses_buf.Count == 0);
        }
        static public bool GetIsDone(this List<OrderClass> OrderClasses)
        {
            List<OrderClass> OrderClasses_buf = (from temp in OrderClasses
                                                   where temp.實際調劑量.StringIsDouble() == false
                                                   select temp).ToList();
            return (OrderClasses_buf.Count == 0);
        }
        static public string GetCurrentFreq(this List<OrderClass> OrderClasses)
        {
            List<string> freqs = (from temp in OrderClasses
                                  select temp.頻次).Distinct().ToList();
            List<OrderClass> OrderClasses_buf = new List<OrderClass>();
            for (int i = 0; i < freqs.Count; i++)
            {
                if (OrderClasses.GetFreqIsDone(freqs[i]) == false)
                {
                    OrderClasses_buf = (from temp in OrderClasses
                                         where temp.頻次 == freqs[i]
                                         where temp.實際調劑量.StringIsDouble() == true
                                         select temp).ToList();
                    if (OrderClasses_buf.Count != 0)
                    {
                        return OrderClasses_buf[0].頻次;
                    }
                }
            }
            return null;

        }
        static public List<string> GetFreqs(this List<OrderClass> OrderClasses)
        {
            List<string> freqs = (from temp in OrderClasses
                                  select temp.頻次).Distinct().ToList();

            return freqs;
        }
        static public List<string> GetPackages(this List<OrderClass> OrderClasses)
        {
            List<string> Packages = (from temp in OrderClasses
                                     select temp.劑量單位).Distinct().ToList();

            return Packages;
        }
        static public System.Collections.Generic.Dictionary<string, List<OrderClass>> CoverToDictionaryBy_PRI_KEY(this List<OrderClass> OrderClasses)
        {
            Dictionary<string, List<OrderClass>> dictionary = new Dictionary<string, List<OrderClass>>();

            foreach (var item in OrderClasses)
            {
                string key = item.PRI_KEY;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<OrderClass> values = new List<OrderClass> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<OrderClass> SortDictionaryBy_PRI_KEY(this System.Collections.Generic.Dictionary<string, List<OrderClass>> dictionary, string value)
        {
            if (dictionary.ContainsKey(value))
            {
                return dictionary[value];
            }
            return new List<OrderClass>();
        }

        static public System.Collections.Generic.Dictionary<string, List<OrderClass>> CoverToDictionaryBy_Code(this List<OrderClass> OrderClasses)
        {
            Dictionary<string, List<OrderClass>> dictionary = new Dictionary<string, List<OrderClass>>();

            foreach (var item in OrderClasses)
            {
                string key = item.藥品碼;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<OrderClass> values = new List<OrderClass> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<OrderClass> SerchDictionary(this System.Collections.Generic.Dictionary<string, List<OrderClass>> dictionary, string value)
        {
            if (dictionary.ContainsKey(value))
            {
                return dictionary[value];
            }
            return new List<OrderClass>();
        }

    }
}
