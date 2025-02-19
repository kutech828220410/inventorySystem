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

        public List<List<udnoectc_orders>> 藥囑資料
        {
            get
            {
                ordersAry.Sort(new ICP_udnoectc_orders());
                List<List<udnoectc_orders>> udnoectc_Orders = new List<List<udnoectc_orders>>();
                List<string> str_temp = (from temp in ordersAry
                                         select temp.服藥順序).Distinct().ToList();
                for (int i = 0; i < str_temp.Count; i++)
                {
                    List<udnoectc_orders> udnoectc_Orders_buf = (from temp in ordersAry
                                                                 where temp.服藥順序 == str_temp[i]
                                                                 select temp).ToList();
                    if(udnoectc_Orders_buf.Count > 0)
                    {
                        udnoectc_Orders.LockAdd(udnoectc_Orders_buf);
                    }
                }
                
                return udnoectc_Orders;
            }
        }
        private class ICP_udnoectc_orders : IComparer<udnoectc_orders>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(udnoectc_orders x, udnoectc_orders y)
            {

                int compare = x.服藥順序.CompareTo(y.服藥順序);
                if(compare == 0)
                {
                    return x.藥囑序號.CompareTo(y.藥囑序號);
                }
                else return compare;
            }
        }

        /// <summary>
        /// 根據 GUID 從 API 伺服器獲取化療配藥通知處方。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="GUID">唯一標識符 GUID。</param>
        /// <returns>返回具有指定 GUID 的化療配藥通知處方對象。如果未找到或發生錯誤，返回 null。</returns>
        /// <example>
        /// 以下是使用 `get_udnoectc_by_GUID` 方法的範例：
        /// <code>
        /// string apiServer = "http://example.com";
        /// string guid = "unique-guid";
        /// var prescription = udnoectc.get_udnoectc_by_GUID(apiServer, guid);
        /// if (prescription != null)
        /// {
        ///     Console.WriteLine($"病人姓名: {prescription.病人姓名}");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("未找到指定 GUID 的處方。");
        /// }
        /// </code>
        /// </example>
        static public udnoectc get_udnoectc_by_GUID(string API_Server, string ServerName, string ServerType, string GUID)
        {
            string url = $"{API_Server}/api/ChemotherapyRxScheduling/get_udnoectc_by_GUID";

            returnData returnData = new returnData();
            returnData.Value = GUID;
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }
            List<udnoectc> udnoectcs = returnData_out.Data.ObjToClass<List<udnoectc>>();
            if (udnoectcs.Count == 0) return null;

            return udnoectcs[0];
        }
        /// <summary>
        /// 更新指定操作員的化療配藥通知子項目（已備藥完成）。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="ServerName">伺服器名稱。</param>
        /// <param name="ServerType">伺服器類型。</param>
        /// <param name="replace_datas">需要更新的化療配藥通知子項目列表。</param>
        /// <param name="op_name">操作員名稱。</param>
        /// <returns>返回已更新的化療配藥通知子項目列表。如果未找到或發生錯誤，返回 null。</returns>
        /// <example>
        /// 以下是使用 `update_udnoectc_orders_comp` 方法的範例：
        /// <code>
        /// string apiServer = "http://example.com";
        /// string serverName = "MainServer";
        /// string serverType = "WebServer";
        /// string operatorName = "JohnDoe";
        /// List<udnoectc_orders> replaceData = new List<udnoectc_orders>();
        /// // 添加需要更新的訂單資料到 replaceData 列表中
        /// var updatedOrders = udnoectc.update_udnoectc_orders_comp(apiServer, serverName, serverType, replaceData, operatorName);
        /// if (updatedOrders != null)
        /// {
        ///     Console.WriteLine($"更新的訂單數量: {updatedOrders.Count}");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("未找到指定操作員的訂單或發生錯誤。");
        /// }
        /// </code>
        /// </example>
        static public List<udnoectc_orders> update_udnoectc_orders_comp(string API_Server, string ServerName, string ServerType, List<udnoectc_orders> replace_datas , string op_name)
        {
            string url = $"{API_Server}/api/ChemotherapyRxScheduling/update_udnoectc_orders_comp";

            returnData returnData = new returnData();
            returnData.Value = op_name;
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;
            returnData.Data = replace_datas;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();

            if (returnData_out == null || returnData_out.Code != 200)
            {
                return null;
            }
            List<udnoectc_orders> udnoectc_Orders = returnData_out.Data.ObjToClass<List<udnoectc_orders>>();

            return udnoectc_Orders;
        }
        /// <summary>
        ///  更新指定操作員的化療配藥通知子項目為醫令確認狀態。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="ServerName">伺服器名稱。</param>
        /// <param name="ServerType">伺服器類型。</param>
        /// <param name="loginName">操作員名稱。</param>
        /// <param name="udnoectcData">需要確認的化療配藥通知子項目。</param>
        /// <returns>返回更新的化療配藥通知子項目列表。如果未找到或發生錯誤，返回 null。</returns>
        /// <example>
        /// 以下是使用 `ConfirmChemotherapyRxOrders` 方法的範例：
        /// <code>
        /// string apiServer = "http://example.com";
        /// string serverName = "cheom";
        /// string serverType = "癌症備藥機";
        /// string loginName = "JohnDoe";
        /// var udnoectcData = new udnoectc();
        /// // 設置需要確認的訂單資料到 udnoectcData 中
        /// var confirmedOrders = ChemotherapyRxOrders.ConfirmChemotherapyRxOrders(apiServer, serverName, serverType, loginName, udnoectcData);
        /// if (confirmedOrders != null)
        /// {
        ///     Console.WriteLine("確認成功");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("未找到指定操作員的訂單或發生錯誤。");
        /// }
        /// </code>
        /// </example>
        public static List<udnoectc_orders> update_udnoectc_confirm_ph(string API_Server, string ServerName, string ServerType, string loginName, udnoectc udnoectcData)
        {
            try
            {
                string url = $"{API_Server}/api/ChemotherapyRxScheduling/update_udnoectc_confirm_ph";

                returnData returnData = new returnData();
                returnData.ServerName = ServerName;
                returnData.ServerType = ServerType;
                returnData.Value = loginName;
                returnData.Data = udnoectcData;

                string json_in = returnData.JsonSerializationt();
                string json_out = Basic.Net.WEBApiPostJson(url, json_in);
                returnData returnData_out = json_out.JsonDeserializet<returnData>();

                if (returnData_out == null || returnData_out.Code != 200)
                {
                    return null;
                }

                List<udnoectc_orders> confirmedOrders = returnData_out.Data.ObjToClass<List<udnoectc_orders>>();
                return confirmedOrders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"內部伺服器錯誤: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// 更新指定操作員的化療配藥通知子項目為調配完成狀態。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="ServerName">伺服器名稱。</param>
        /// <param name="ServerType">伺服器類型。</param>
        /// <param name="loginName">操作員名稱。</param>
        /// <param name="udnoectcData">需要更新的化療配藥通知子項目。</param>
        /// <returns>返回更新的化療配藥通知子項目列表。如果未找到或發生錯誤，返回 null。</returns>
        /// <example>
        /// 以下是使用 `UpdateDispensingComplete` 方法的範例：
        /// <code>
        /// string apiServer = "http://example.com";
        /// string serverName = "cheom";
        /// string serverType = "癌症備藥機";
        /// string loginName = "JohnDoe";
        /// var udnoectcData = new udnoectc();
        /// // 設置需要更新的訂單資料到 udnoectcData 中
        /// var updatedOrders = ChemotherapyRxOrders.UpdateDispensingComplete(apiServer, serverName, serverType, loginName, udnoectcData);
        /// if (updatedOrders != null)
        /// {
        ///     Console.WriteLine("更新成功");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("未找到指定操作員的訂單或發生錯誤。");
        /// }
        /// </code>
        /// </example>
        public static List<udnoectc_orders> update_udnoectc_disp_ph(string API_Server, string ServerName, string ServerType, string loginName, udnoectc udnoectcData)
        {
            try
            {
                string url = $"{API_Server}/api/ChemotherapyRxScheduling/update_udnoectc_disp_ph";

                returnData returnData = new returnData();
                returnData.ServerName = ServerName;
                returnData.ServerType = ServerType;
                returnData.Value = loginName;
                returnData.Data = udnoectcData;

                string json_in = returnData.JsonSerializationt();
                string json_out = Basic.Net.WEBApiPostJson(url, json_in);
                returnData returnData_out = json_out.JsonDeserializet<returnData>();

                if (returnData_out == null || returnData_out.Code != 200)
                {
                    return null;
                }

                List<udnoectc_orders> updatedOrders = returnData_out.Data.ObjToClass<List<udnoectc_orders>>();
                return updatedOrders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"內部伺服器錯誤: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// 更新指定操作員的化療配藥通知子項目為處方核對狀態。
        /// </summary>
        /// <param name="API_Server">API 伺服器地址。</param>
        /// <param name="ServerName">伺服器名稱。</param>
        /// <param name="ServerType">伺服器類型。</param>
        /// <param name="loginName">操作員名稱。</param>
        /// <param name="udnoectcData">需要更新的化療配藥通知子項目。</param>
        /// <returns>返回更新的化療配藥通知子項目列表。如果未找到或發生錯誤，返回 null。</returns>
        /// <example>
        /// 以下是使用 `UpdatePrescriptionCheck` 方法的範例：
        /// <code>
        /// string apiServer = "http://example.com";
        /// string serverName = "cheom";
        /// string serverType = "癌症備藥機";
        /// string loginName = "JohnDoe";
        /// var udnoectcData = new udnoectc();
        /// // 設置需要更新的訂單資料到 udnoectcData 中
        /// var updatedOrders = ChemotherapyRxOrders.UpdatePrescriptionCheck(apiServer, serverName, serverType, loginName, udnoectcData);
        /// if (updatedOrders != null)
        /// {
        ///     Console.WriteLine("更新成功");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("未找到指定操作員的訂單或發生錯誤。");
        /// }
        /// </code>
        /// </example>
        public static List<udnoectc_orders> update_udnoectc_check_ph(string API_Server, string ServerName, string ServerType, string loginName, udnoectc udnoectcData)
        {
            try
            {
                string url = $"{API_Server}/api/ChemotherapyRxScheduling/update_udnoectc_check_ph";

                returnData returnData = new returnData();
                returnData.ServerName = ServerName;
                returnData.ServerType = ServerType;
                returnData.Value = loginName;
                returnData.Data = udnoectcData;

                string json_in = returnData.JsonSerializationt();
                string json_out = Basic.Net.WEBApiPostJson(url, json_in);
                returnData returnData_out = json_out.JsonDeserializet<returnData>();

                if (returnData_out == null || returnData_out.Code != 200)
                {
                    return null;
                }

                List<udnoectc_orders> updatedOrders = returnData_out.Data.ObjToClass<List<udnoectc_orders>>();
                return updatedOrders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"內部伺服器錯誤: {ex.Message}");
                return null;
            }
        }
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


        static public System.Collections.Generic.Dictionary<string, List<udnoectc_orders>> CoverToDictionaryByMGUID(List<udnoectc_orders> udnoectc_Orders)
        {
            Dictionary<string, List<udnoectc_orders>> dictionary = new Dictionary<string, List<udnoectc_orders>>();

            foreach (var item in udnoectc_Orders)
            {
                string key = item.Master_GUID;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<udnoectc_orders> values = new List<udnoectc_orders> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<udnoectc_orders> SortDictionaryByMGUID(System.Collections.Generic.Dictionary<string, List<udnoectc_orders>> dictionary, string Master_GUID)
        {
            if (dictionary.ContainsKey(Master_GUID))
            {
                return dictionary[Master_GUID];
            }
            return new List<udnoectc_orders>();
        }
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
    [Serializable]
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

        static public System.Collections.Generic.Dictionary<string, List<udnoectc_ctcvars>> CoverToDictionaryByMGUID(List<udnoectc_ctcvars> udnoectc_Ctcvars)
        {
            Dictionary<string, List<udnoectc_ctcvars>> dictionary = new Dictionary<string, List<udnoectc_ctcvars>>();

            foreach (var item in udnoectc_Ctcvars)
            {
                string key = item.Master_GUID;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<udnoectc_ctcvars> values = new List<udnoectc_ctcvars> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }
        static public List<udnoectc_ctcvars> SortDictionaryByMGUID(System.Collections.Generic.Dictionary<string, List<udnoectc_ctcvars>> dictionary, string Master_GUID)
        {
            if (dictionary.ContainsKey(Master_GUID))
            {
                return dictionary[Master_GUID];
            }
            return new List<udnoectc_ctcvars>();
        }
    }
}
