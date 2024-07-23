using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;
using MyOffice;
using NPOI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class drugStotreDistribution : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化撥補紀錄資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [HttpPost]
        public string POST_init([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);

            try
            {
                //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

                returnData.Method = "POST_init";
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 新增撥補資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "Data": 
        ///     {
        ///         [drugStotreDistributionClass]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string POST_add(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                List<drugStotreDistributionClass> drugstotreDistributions = returnData.Data.ObjToClass<List<drugStotreDistributionClass>>();

                SQLControl sQLControl_drugstotreDistribution = new SQLControl(Server, DB, new enum_drugStotreDistribution().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_drugstotreDistributions = drugstotreDistributions.ClassToSQL<drugStotreDistributionClass, enum_drugStotreDistribution>();
                for (int i = 0; i < list_drugstotreDistributions.Count; i++)
                {
                    list_drugstotreDistributions[i][(int)enum_drugStotreDistribution.GUID] = Guid.NewGuid().ToString();
                    list_drugstotreDistributions[i][(int)enum_drugStotreDistribution.加入時間] = DateTime.Now.ToDateTimeString_6();
                    list_drugstotreDistributions[i][(int)enum_drugStotreDistribution.報表生成時間] = DateTime.Now.ToDateTimeString_6();
                    list_drugstotreDistributions[i][(int)enum_drugStotreDistribution.撥發時間] = DateTime.MinValue.ToDateTimeString_6();
                }

                sQLControl_drugstotreDistribution.AddRows(null, list_drugstotreDistributions);

                returnData.Result = $"新增撥補資料成功,共<{list_drugstotreDistributions.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"drugstotreDistribution");
                Logger.Log($"drugstotreDistribution", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"drugstotreDistribution");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新撥補資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "Data": 
        ///     {
        ///         [drugStotreDistributionClass]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("update_by_guid")]
        [HttpPost]
        public string POST_update_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "update_by_guid";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                List<drugStotreDistributionClass> drugstotreDistributions = returnData.Data.ObjToClass<List<drugStotreDistributionClass>>();

                SQLControl sQLControl_drugstotreDistribution = new SQLControl(Server, DB, new enum_drugStotreDistribution().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_drugstotreDistributions = drugstotreDistributions.ClassToSQL<drugStotreDistributionClass, enum_drugStotreDistribution>();

                sQLControl_drugstotreDistribution.UpdateByDefulteExtra(null, list_drugstotreDistributions);

                returnData.Result = $"更新撥補資料,共<{list_drugstotreDistributions.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
            
                Logger.LogAddLine($"drugstotreDistribution");
                Logger.Log($"drugstotreDistribution", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"drugstotreDistribution");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得撥補資料(新增時間範圍)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [drugStotreDistributionClass]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///        "起始時間",
        ///        "結束時間"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_by_addedTime")]
        [HttpPost]
        public string get_by_addedTime(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_by_addedTime";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];
                if (起始時間.Check_Date_String() == false || 結束時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"時間範圍格式錯誤";
                    return returnData.JsonSerializationt(true);
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

              

                SQLControl sQLControl_drugstotreDistribution = new SQLControl(Server, DB, new enum_drugStotreDistribution().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_drugstotreDistributions = new List<object[]>();

                list_drugstotreDistributions =  sQLControl_drugstotreDistribution.GetRowsByBetween(null, (int)enum_drugStotreDistribution.加入時間, 起始時間, 結束時間);

                list_drugstotreDistributions.Sort((x, y) => y[(int)enum_drugStotreDistribution.加入時間].StringToDateTime().CompareTo(x[(int)enum_drugStotreDistribution.加入時間].StringToDateTime()));

                List<drugStotreDistributionClass> drugstotreDistributions = list_drugstotreDistributions.SQLToClass<drugStotreDistributionClass , enum_drugStotreDistribution>();


                returnData.Result = $"取得撥補資料(新增時間範圍),共<{list_drugstotreDistributions.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = drugstotreDistributions;
                returnData.Code = 200;
                Logger.LogAddLine($"drugstotreDistribution");
                Logger.Log($"drugstotreDistribution", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"drugstotreDistribution");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得撥補資料(GUID)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [drugStotreDistributionClass]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///        "GUID",
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_by_guid")]
        [HttpPost]
        public string get_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_by_guid";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
         
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();



                SQLControl sQLControl_drugstotreDistribution = new SQLControl(Server, DB, new enum_drugStotreDistribution().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_drugstotreDistributions = new List<object[]>();

                list_drugstotreDistributions = sQLControl_drugstotreDistribution.GetRowsByDefult(null, (int)enum_drugStotreDistribution.GUID, GUID);

                list_drugstotreDistributions.Sort((x, y) => y[(int)enum_drugStotreDistribution.加入時間].StringToDateTime().CompareTo(x[(int)enum_drugStotreDistribution.加入時間].StringToDateTime()));

                List<drugStotreDistributionClass> drugstotreDistributions = list_drugstotreDistributions.SQLToClass<drugStotreDistributionClass, enum_drugStotreDistribution>();
                if(drugstotreDistributions.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Result = $"取得撥補資料(GUID),共<{list_drugstotreDistributions.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = drugstotreDistributions[0];
                returnData.Code = 200;
                Logger.LogAddLine($"drugstotreDistribution");
                Logger.Log($"drugstotreDistribution", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"drugstotreDistribution");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        [Route("excel_upload")]
        [HttpPost]
        public async Task<string> POST_excel_upload([FromForm] IFormFile file, [FromForm] string IC_NAME, [FromForm] string CT, [FromForm] string DEFAULT_OP)
        {
            returnData returnData = new returnData();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");

                List<medClass> medClasses = medClass.get_med_cloud("http://127.0.0.1:4433");
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "ServerSetting VM端設定異常!";
                    return returnData.JsonSerializationt(true);
                }

                returnData.Method = "POST_excel_upload";
                var formFile = Request.Form.Files.FirstOrDefault();

                if (formFile == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "文件不得為空";
                    return returnData.JsonSerializationt(true);
                }

                string extension = Path.GetExtension(formFile.FileName); // 获取文件的扩展名
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                inventoryClass.creat creat = new inventoryClass.creat();
                string error = "";
                List<distribution_excel> distributionList = new List<distribution_excel>();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    System.Data.DataTable dt = ExcelClass.NPOI_LoadFile(memoryStream.ToArray(), extension);
                    dt = dt.ReorderTable(new enum_撥補單上傳_Excel());
                    if (dt == null)
                    {
                        returnData.Code = -200;
                        returnData.Result = "上傳文件表頭無效!";
                        return returnData.JsonSerializationt(true);
                    }
                    List<object[]> list_value = dt.DataTableToRowList();

                    if (IC_NAME.StringIsEmpty())
                    {
                        IC_NAME = Path.GetFileNameWithoutExtension(file.FileName);
                    }
                    for (int i = 0; i < list_value.Count; i++)
                    {

                        distribution_excel rowvalue = new distribution_excel
                        {
                            藥碼 = list_value[i][(int)enum_撥補單上傳_Excel.藥碼].ObjectToString(),
                            藥名 = list_value[i][(int)enum_撥補單上傳_Excel.藥名].ObjectToString(),
                            撥發量 = list_value[i][(int)enum_撥補單上傳_Excel.撥發量].ObjectToString(),
                            效期 = list_value[i][(int)enum_撥補單上傳_Excel.效期].ObjectToString(),
                            批號 = list_value[i][(int)enum_撥補單上傳_Excel.批號].ObjectToString(),
                        };
                        distributionList.Add(rowvalue);


                    }
                }
                returnData.Data = distributionList;
                returnData.Code = 200;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Result = "接收上傳文件成功";
                return returnData.JsonSerializationt(true);
            }

            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                return returnData.JsonSerializationt(true);
            }
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_drugStotreDistribution()));
            return tables.JsonSerializationt(true);
        }
    }
}
