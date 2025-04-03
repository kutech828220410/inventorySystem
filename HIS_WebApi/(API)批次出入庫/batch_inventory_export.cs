using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
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
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Text;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class batch_inventory_export : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;


        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
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
        [SwaggerResponse(1, "", typeof(batch_inventory_exportClass))]
        [HttpPost]
        public string POST_init([FromBody] returnData returnData)
        {
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 下載範例Excel
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("download_sample_excel")]
        [HttpPost]
        public async Task<ActionResult> Post_download_sample_excel([FromBody] returnData returnData)
        {
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return null;
            }


            string xlsx_command = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string xls_command = "application/vnd.ms-excel";
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "download_sample_excel";
            List<object[]> list_value = new List<object[]>();
            System.Data.DataTable dataTable = list_value.ToDataTable(new enum_batch_inventory_export_Excel());
            byte[] bytes = ExcelClass.NPOI_GetBytes(dataTable, Excel_Type.xlsx);
            return await Task.FromResult(File(bytes, xlsx_command, $"{DateTime.Now.ToDateString("-")}_批次出庫範例.xlsx"));

        }
        /// <summary>
        /// 上傳Excel表單
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[file] : 上傳xls/xlsx <br/> 
        ///  1.[CT_NAME] : 建表人員 <br/> 
        /// </remarks>
        /// <returns>Excel</returns>
        [Route("excel_upload")]
        [HttpPost]
        public async Task<string> POST_excel([FromForm] IFormFile file, [FromForm] string CT_NAME)
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile == null)
            {
                throw new Exception("文件不能為空");
            }
            string extension = Path.GetExtension(formFile.FileName); // 获取文件的扩展名
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();

            List<batch_inventory_exportClass> batch_Inventory_exportClasses = new List<batch_inventory_exportClass>();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                System.Data.DataTable dt = ExcelClass.NPOI_LoadFile(memoryStream.ToArray(), extension);
                List<object[]> list_value = dt.DataTableToRowList();
                for (int i = 0; i < list_value.Count; i++)
                {
                    batch_inventory_exportClass batch_Inventory_exportClass = new batch_inventory_exportClass();

                    batch_Inventory_exportClass.庫別 = list_value[i][(int)enum_batch_inventory_export_Excel.庫別].ObjectToString();
                    batch_Inventory_exportClass.藥碼 = list_value[i][(int)enum_batch_inventory_export_Excel.藥碼].ObjectToString();
                    batch_Inventory_exportClass.效期 = list_value[i][(int)enum_batch_inventory_export_Excel.效期].ObjectToString();
                    batch_Inventory_exportClass.批號 = list_value[i][(int)enum_batch_inventory_export_Excel.批號].ObjectToString();
                    batch_Inventory_exportClass.數量 = list_value[i][(int)enum_batch_inventory_export_Excel.數量].ObjectToString();
                    batch_Inventory_exportClass.收支原因 = list_value[i][(int)enum_batch_inventory_export_Excel.收支原因].ObjectToString();

                    batch_Inventory_exportClasses.Add(batch_Inventory_exportClass);
                }

            }
            returnData returnData = new returnData();
            returnData.Data = batch_Inventory_exportClasses;
            returnData.ValueAry.Add(CT_NAME);
            return POST_add(returnData);
        }
        /// <summary>
        /// 新增批次出庫資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [batch_inventory_exportClassAry]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///        "建表人員"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add";
            try
            {
                POST_init(returnData);
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[建表人員]";
                    return returnData.JsonSerializationt(true);
                }

                string 建表人員 = returnData.ValueAry[0];
                List<medClass> medClasses_cloud = medClass.get_med_cloud("http://127.0.0.1:4433");
                List<medClass> medClasses_cloud_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_med_cloud = medClasses_cloud.CoverToDictionaryByCode();
                List<batch_inventory_exportClass> batch_Inventory_exportClasses = returnData.Data.ObjToClass<List<batch_inventory_exportClass>>();

                SQLControl sQLControl_batch_inventory_export = new SQLControl(Server, DB, new enum_batch_inventory_export().GetEnumDescription(), UserName, Password, Port, SSLMode);

                List<batch_inventory_exportClass> batch_Inventory_exportClasses_add = new List<batch_inventory_exportClass>();
                for (int i = 0; i < batch_Inventory_exportClasses.Count; i++)
                {
                    string 藥碼 = batch_Inventory_exportClasses[i].藥碼;
                    string 數量 = batch_Inventory_exportClasses[i].數量;
                    string 效期 = batch_Inventory_exportClasses[i].效期;
                    if (藥碼.StringIsEmpty() || 數量.StringIsInt32() == false) continue;
                    if (效期.Check_Date_String() == false) continue;
                    medClasses_cloud_buf = keyValuePairs_med_cloud.SortDictionaryByCode(藥碼);
                    if (medClasses_cloud_buf.Count == 0) continue;
                    batch_Inventory_exportClasses[i].GUID = Guid.NewGuid().ToString();
                    batch_Inventory_exportClasses[i].建表時間 = DateTime.Now.ToDateTimeString();
                    batch_Inventory_exportClasses[i].建表人員 = 建表人員;
                    batch_Inventory_exportClasses[i].出庫時間 = DateTime.MinValue.ToDateTimeString();
                    batch_Inventory_exportClasses[i].藥名 = medClasses_cloud_buf[0].藥品名稱;
                    batch_Inventory_exportClasses[i].單位 = medClasses_cloud_buf[0].包裝單位;
                    batch_Inventory_exportClasses[i].狀態 = "等待過帳";
                    batch_Inventory_exportClasses_add.Add(batch_Inventory_exportClasses[i]);
                }
                List<object[]> list_batch_Inventory_exportClasses = batch_Inventory_exportClasses_add.ClassToSQL<batch_inventory_exportClass, enum_batch_inventory_export>();
                sQLControl_batch_inventory_export.AddRows(null, list_batch_Inventory_exportClasses);

                returnData.Data = batch_Inventory_exportClasses_add;
                returnData.Result = $"新增批次出庫資料成功,共<{list_batch_Inventory_exportClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"batch_inventory_export");
                Logger.Log($"batch_inventory_export", $"{returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"batch_inventory_export");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] {returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 刪除批次出庫資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [batch_inventory_exportClassAry]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///      
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("delete_by_GUID")]
        [HttpPost]
        public string POST_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "delete_by_GUID";
            try
            {
                POST_init(returnData);
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();




                List<batch_inventory_exportClass> batch_Inventory_exportClasses = returnData.Data.ObjToClass<List<batch_inventory_exportClass>>();
                List<object[]> list_batch_Inventory_exportClasses = batch_Inventory_exportClasses.ClassToSQL<batch_inventory_exportClass, enum_batch_inventory_export>();

                SQLControl sQLControl_batch_inventory_export = new SQLControl(Server, DB, new enum_batch_inventory_export().GetEnumDescription(), UserName, Password, Port, SSLMode);


                sQLControl_batch_inventory_export.DeleteExtra(null, list_batch_Inventory_exportClasses);

                returnData.Data = batch_Inventory_exportClasses;
                returnData.Result = $"刪除批次出庫資料成功,共<{list_batch_Inventory_exportClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"batch_inventory_export");
                Logger.Log($"batch_inventory_export", $"{returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"batch_inventory_export");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] {returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新批次出庫資料數量
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [batch_inventory_exportClassAry]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///      
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("update_QTY_by_GUID")]
        [HttpPost]
        public string POST_update_QTY_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "update_QTY_by_GUID";
            try
            {
                POST_init(returnData);
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();




                List<batch_inventory_exportClass> batch_Inventory_exportClasses = returnData.Data.ObjToClass<List<batch_inventory_exportClass>>();

                SQLControl sQLControl_batch_inventory_export = new SQLControl(Server, DB, new enum_batch_inventory_export().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_replace = new List<object[]>();
                for (int i = 0; i < batch_Inventory_exportClasses.Count; i++)
                {
                    List<object[]> list_value = sQLControl_batch_inventory_export.GetRowsByDefult(null, (int)enum_batch_inventory_export.GUID, batch_Inventory_exportClasses[i].GUID);
                    if (list_value.Count > 0)
                    {
                        if (batch_Inventory_exportClasses[i].數量.StringIsDouble() == false) continue;
                        list_value[0][(int)enum_batch_inventory_export.數量] = batch_Inventory_exportClasses[i].數量;
                        list_replace.Add(list_value[0]);
                    }
                }
                sQLControl_batch_inventory_export.UpdateByDefulteExtra(null, list_replace);
                List<batch_inventory_exportClass> batch_Inventory_exportClasses_replace = list_replace.SQLToClass<batch_inventory_exportClass, enum_batch_inventory_export>();
                returnData.Data = batch_Inventory_exportClasses_replace;
                returnData.Result = $"更新批次出庫資料成功,共<{list_replace.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"batch_inventory_export");
                Logger.Log($"batch_inventory_export", $"{returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"batch_inventory_export");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] {returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新批次出庫資料為"過帳完成"
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         [batch_inventory_exportClassAry]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///         "出庫人員"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("update_state_done_by_GUID")]
        [HttpPost]
        public string POST_update_state_done_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "update_state_done_by_GUID";
            try
            {
                POST_init(returnData);
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[出庫人員]";
                    return returnData.JsonSerializationt(true);
                }

                string 出庫人員 = returnData.ValueAry[0];

                List<batch_inventory_exportClass> batch_Inventory_exportClasses = returnData.Data.ObjToClass<List<batch_inventory_exportClass>>();

                SQLControl sQLControl_batch_inventory_export = new SQLControl(Server, DB, new enum_batch_inventory_export().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_replace = new List<object[]>();
                for (int i = 0; i < batch_Inventory_exportClasses.Count; i++)
                {
                    List<object[]> list_value = sQLControl_batch_inventory_export.GetRowsByDefult(null, (int)enum_batch_inventory_export.GUID, batch_Inventory_exportClasses[i].GUID);
                    if (list_value.Count > 0)
                    {
                        list_value[0][(int)enum_batch_inventory_export.出庫人員] = 出庫人員;
                        list_value[0][(int)enum_batch_inventory_export.出庫時間] = DateTime.Now.ToDateTimeString_6();
                        list_value[0][(int)enum_batch_inventory_export.狀態] = "過帳完成";
                        list_replace.Add(list_value[0]);
                    }
                }
                sQLControl_batch_inventory_export.UpdateByDefulteExtra(null, list_replace);
                List<batch_inventory_exportClass> batch_Inventory_exportClasses_replace = list_replace.SQLToClass<batch_inventory_exportClass, enum_batch_inventory_export>();
                returnData.Data = batch_Inventory_exportClasses_replace;
                returnData.Result = $"更新批次出庫資料成功,共<{list_replace.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"batch_inventory_export");
                Logger.Log($"batch_inventory_export", $"{returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"batch_inventory_export");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] {returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以建表時間取得批次出庫資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "起始時間",
        ///       "結束時間",
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_CT_TIME")]
        [HttpPost]
        public string POST_get_by_CT_TIME([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_by_CT_TIME";
            try
            {
                POST_init(returnData);
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();


                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];



                SQLControl sQLControl_batch_inventory_export = new SQLControl(Server, DB, new enum_batch_inventory_export().GetEnumDescription(), UserName, Password, Port, SSLMode);


                List<object[]> list_batch_Inventory_exportClasses = sQLControl_batch_inventory_export.GetRowsByBetween(null, (int)enum_batch_inventory_export.建表時間, 起始時間, 結束時間);
                List<batch_inventory_exportClass> batch_Inventory_exportClasses = list_batch_Inventory_exportClasses.SQLToClass<batch_inventory_exportClass, enum_batch_inventory_export>();
                returnData.Data = batch_Inventory_exportClasses;
                returnData.Result = $"取得批次出庫資料成功,共<{list_batch_Inventory_exportClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"batch_inventory_export");
                Logger.Log($"batch_inventory_export", $"{returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"batch_inventory_export");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] {returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以出庫時間取得批次出庫資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///       "起始時間",
        ///       "結束時間",
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_by_RECEIVE_TIME")]
        [HttpPost]
        public string POST_get_by_RECEIVE_TIME([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_by_RECEIVE_TIME";
            try
            {
                POST_init(returnData);
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses[0];
                string Server = sys_serverSettingClass.Server;
                string DB = sys_serverSettingClass.DBName;
                string UserName = sys_serverSettingClass.User;
                string Password = sys_serverSettingClass.Password;
                uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();


                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];



                SQLControl sQLControl_batch_inventory_export = new SQLControl(Server, DB, new enum_batch_inventory_export().GetEnumDescription(), UserName, Password, Port, SSLMode);


                List<object[]> list_batch_Inventory_exportClasses = sQLControl_batch_inventory_export.GetRowsByBetween(null, (int)enum_batch_inventory_export.出庫時間, 起始時間, 結束時間);
                List<batch_inventory_exportClass> batch_Inventory_exportClasses = list_batch_Inventory_exportClasses.SQLToClass<batch_inventory_exportClass, enum_batch_inventory_export>();
                returnData.Data = batch_Inventory_exportClasses;
                returnData.Result = $"取得批次出庫資料成功,共<{list_batch_Inventory_exportClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"batch_inventory_export");
                Logger.Log($"batch_inventory_export", $"{returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"batch_inventory_export");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] {returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }

        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {
            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_batch_inventory_export());
            return table.JsonSerializationt(true);
        }
    }
}
