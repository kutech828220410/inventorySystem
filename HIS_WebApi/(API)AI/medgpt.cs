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
using NPOI.HPSF;
using NPOI.HSSF.Util;

namespace HIS_WebApi._API_AI
{

    [Route("api/[controller]")]
    [ApiController]
    public class medgpt : ControllerBase
    {
        static string API_Server = Method.GetServerAPI("Main", "網頁", "API01");
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(shiftClass))]
        [HttpPost]
        public string init()
        {
            try
            {
                //returnData.TableName = $"{enum_medGpt}";
                return CheckCreatTable(new enum_medGpt());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 新增或修改藥品設定
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [medConfigClass陣列]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);
              
                medGPTClass medGPTClasses = returnData.Data.ObjToClass<medGPTClass>();
                if (medGPTClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                string TableName = $"{new enum_medGpt().GetEnumName()}";
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> add_medGpt = new List<medGPTClass>(){ medGPTClasses }.ClassToSQL<medGPTClass, enum_medGpt>();
                sQLControl.AddRows(null, add_medGpt);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medGPTClasses;
                returnData.Result = $"新增一筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        [HttpPost("analyze")]
        public string analyze(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            //returnData returnData = new returnData();
            returnData.Method = "api/medgpt/analyze";
            try
            {
                //if (BarCode.StringIsEmpty())
                //{
                //    returnData.Code = -200;
                //    returnData.Result = "Barcode空白";
                //    return returnData.JsonSerializationt(true);
                //}
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "藥檔資料");
                SQLControl sQLControl = new SQLControl(Server, DB, "order_list", UserName, Password, Port, SSLMode);


                List<OrderClass> orders = returnData.Data.ObjToClass<List<OrderClass>>();
                if (orders == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data空白";
                    return returnData.JsonSerializationt(true);
                }

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
                medGPTClass medGPTClasses = new medGPTClass();
                medGPTClass medGPT = medGPTClass.Excute(url, result);
                if(medGPT.error == true.ToString())
                {
                    medGPTClasses = new medGPTClass()
                    {
                        GUID = Guid.NewGuid().ToString(),
                        病歷號 = 病歷號,
                        醫生姓名 = orders[0].醫師代碼,
                        開方時間 = orders[0].開方日期,
                        藥袋類型 = orders[0].藥袋類型,
                        錯誤類別 = string.Join(",", medGPT.error_type),
                        簡述事件 = medGPT.response,
                        狀態 = "未更改",
                        操作人員 = orders[0].藥師姓名,
                        操作時間 = DateTime.Now.ToDateTimeString(),
                    };
                    init();
                    medGPTClass.add(API_Server, medGPTClasses);
                }
                else
                {
                    medGPTClasses = medGPT;
                }





                returnData.Data = medGPTClasses;
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
        private string CheckCreatTable(Enum Enum)
        {
            //string TableName = returnData.TableName;
            SQLUI.Table table = new SQLUI.Table(Enum.GetEnumName());
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
            if (sys_serverSettingClasses.Count == 0)
            {
                return $"找無Server資料!";
            }
            table = MethodClass.CheckCreatTable(sys_serverSettingClasses[0], Enum);

            return table.JsonSerializationt(true);
        }
        

    }
}
