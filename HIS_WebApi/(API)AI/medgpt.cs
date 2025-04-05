using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using MySql.Data.MySqlClient;
using SQLUI;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using H_Pannel_lib;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace HIS_WebApi._API_AI
{

    [Route("api/[controller]")]
    [ApiController]
    public class medgpt : ControllerBase
    {
        static string API_Server = Method.GetServerAPI("Main", "網頁", "API01");
        static private MySqlSslMode SSLMode = MySqlSslMode.None;


        [HttpGet("analyze")]
        public string analyze(string? BarCode)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData returnData = new returnData();
            returnData.Method = "api/medgpt/analyze?barcode=";
            try
            {
                if (BarCode.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "Barcode空白";
                    return returnData.JsonSerializationt(true);
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "藥檔資料");
                SQLControl sQLControl = new SQLControl(Server, DB, "order_list", UserName, Password, Port, SSLMode);


                List<object[]> list_pha_order = sQLControl.GetRowsByDefult(null, (int)enum_醫囑資料.藥袋條碼, BarCode);
                List<OrderClass> orders = list_pha_order.SQLToClass<OrderClass, enum_醫囑資料>();
                orders = (from temp in orders
                          where temp.產出時間.StringToDateTime() >= DateTime.Now.AddDays(-1).GetStartDate()
                          select temp).ToList();
                List<Prescription> eff_cpoe = GroupOrderList(orders);


                string 病歷號 = orders[0].病歷號;
                List<object[]> list_order = sQLControl.GetRowsByDefult(null, (int)enum_醫囑資料.病歷號, 病歷號);
                List<OrderClass> history_order = list_order.SQLToClass<OrderClass, enum_醫囑資料>();
                history_order = (from temp in history_order
                                 where temp.產出時間.StringToDateTime() >= DateTime.Now.AddDays(-1).AddMonths(-3).GetStartDate()
                                 where temp.產出時間.StringToDateTime() >= DateTime.Now.AddDays(-1).AddMonths(-3).GetStartDate()
                                 select temp).ToList();
                List<Prescription> old_cpoe = GroupOrderList(history_order);


                PrescriptionSet result = new PrescriptionSet
                {
                    有效處方 = eff_cpoe,
                    歷史處方 = old_cpoe
                };
                string url = @"https://www.kutech.tw:3000/medgpt";
                medGPTClass medGPT = medGPTClass.Excute(url, result);

                returnData.Data = medGPT;
                returnData.Code = 200;
                returnData.Result = $"AI辨識處方成功";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception:{ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        private List<Prescription> GroupOrderList(List<OrderClass> OrderClasses)
        {
            List<medClass> medClasses = medClass.get_med_cloud(API_Server);
            Dictionary<string, List<medClass>> medClassDict = medClasses.CoverToDictionaryByCode();
            List<Prescription> cpoeList = OrderClasses
                .GroupBy(temp => temp.藥袋條碼)
                .Select(group =>
                {
                    OrderClass orderClass = group.First();
                    List<DrugOrder> drugOrders = group
                    .Select(value =>
                    {
                        medClass med = medClassDict.SortDictionaryByCode(value.藥品碼).FirstOrDefault();
                        if (med == null) return null;

                        return new DrugOrder
                        {
                            藥品名稱 = value.藥品名稱,
                            費用別 = value.費用別,
                            交易量 = value.交易量.Replace("-", ""),
                            頻次 = value.頻次,
                            天數 = value.天數,
                            健保碼 = med.健保碼,
                            ATC = med.ATC,
                            藥品學名 = med.藥品學名,
                            藥品許可證號 = med.藥品許可證號,
                            管制級別 = med.管制級別,
                        };

                    })
                    .Where(value => value != null)
                    .ToList();
                    return new Prescription
                    {
                        藥袋條碼 = group.Key,
                        產出時間 = orderClass.產出時間,
                        醫師代碼 = group.Any(item => item.醫師代碼 == item.病人姓名).ToString(),
                        處方 = drugOrders
                    };
                }).ToList();
            return cpoeList;

        }
        //private class identify
        //{
        //    [JsonPropertyName("eff_order")]
        //    public List<cpoe> 有效處方 { get; set; }
        //    [JsonPropertyName("old_order")]
        //    public List<cpoe> 歷史處方 { get; set; }

        //}
        //private class order
        //{
        //    [JsonPropertyName("CTYPE")]
        //    public string 費用別 { get; set; }
        //    [JsonPropertyName("NAME")]
        //    public string 藥品名稱 { get; set; }
        //    [JsonPropertyName("HI_CODE")]
        //    public string 健保碼 { get; set; }
        //    [JsonPropertyName("ATC")]
        //    public string ATC { get; set; }
        //    [JsonPropertyName("LICENSE")]
        //    public string 藥品許可證號 { get; set; }
        //    [JsonPropertyName("DIANAME")]
        //    public string 藥品學名 { get; set; }
        //    [JsonPropertyName("DRUGKIND")]
        //    public string 管制級別 { get; set; }
        //    [JsonPropertyName("TXN_QTY")]
        //    public string 交易量 { get; set; }
        //    [JsonPropertyName("FREQ")]
        //    public string 頻次 { get; set; }
        //    [JsonPropertyName("DAYS")]
        //    public string 天數 { get; set; }
        //}
        //private class cpoe
        //{
        //    [JsonPropertyName("MED_BAG_SN")]
        //    public string 藥袋條碼 { get; set; }
        //    [JsonPropertyName("DOC")]
        //    public string 醫師代碼 { get; set; }
        //    [JsonPropertyName("CT_TIME")]
        //    public string 產出時間 { get; set; }
        //    [JsonPropertyName("order")]
        //    public List<order> 處方 { get; set; }
        //    [JsonPropertyName("SECTNO")]
        //    public string 科別 { get; set; }
        //}
        //private class medGPTClass
        //{
        //    public string MED_BAG_SN { get; set; }
        //    public string error { get; set; }
        //    public List<string> error_type { get; set; }
        //    public string response { get; set; }
        //}
        
    }
}
