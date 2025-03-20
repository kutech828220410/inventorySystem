using Microsoft.AspNetCore.Mvc;
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

namespace HIS_WebApi
{
    /// <summary>
    /// 藥品群組
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class medGroup : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 查詢藥品群組JSON格式範例
        /// </summary>
        /// <remarks>None</remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>藥品群組JSON格式</returns>
        [Route("json_sample")]
        [HttpPost]
        public string GET_json_sample([FromBody] returnData returnData)
        {
            medGroupClass medGroupClass = new medGroupClass();
            medGroupClass.MedClasses.Add(new medClass());
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = false,
            };
            string jsonString = JsonSerializer.Serialize<object>(medGroupClass, options);
            return jsonString;
        }
        /// <summary>
        /// 初始化藥品群組資料庫結構
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.ServerName] : 伺服器名稱<br/> 
        ///  2.[returnData.ServerType] : 伺服器種類<br/> 
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>資料庫表單JsonString</returns>
        [Route("init")]
        [HttpPost]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(medGroupClass))]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM端");
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
        /// 取得所有藥品群組內容
        /// </summary>
        /// <remarks>
        /// 無
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[medGroupClasses]</returns>
        [Route("get_all_group")]
        [HttpPost]
        public string POST_get_all_group([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();


                List<medGroupClass> medGroupClasses = Function_Get_medGroupClass(sys_serverSettingClasses[0]);

                returnData.Code = 200;
                returnData.Data = medGroupClasses;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "get_all_group";
                returnData.Result = $"取得藥品群組資料成功!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 以GUID取得藥品群組內容
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" :
        ///     [
        ///        "GUID"
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_group_by_guid")]
        [HttpPost]
        public string POST_get_group_by_guid([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                List<medGroupClass> medGroupClasses = Function_Get_medGroupClass(sys_serverSettingClasses[0]);
                medGroupClasses = (from temp in medGroupClasses
                                   where temp.GUID == GUID
                                   select temp).ToList();
                if(medGroupClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }

                returnData.Code = 200;
                returnData.Data = medGroupClasses;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "get_group_by_guid";
                returnData.Result = $"取得藥品群組資料成功";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 新增或修改藥品群組
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON,若Data內GUID為空值時為[新增新群組],不為空值時為[原群組修改]
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "GUID": "ef2f3e3e-a58d-4ac0-8878-d29aa1ccebdf",
        ///         "NAME": "TEST123",
        ///         "MedClasses": 
        ///          [
        ///             {
        ///                 "CODE": "IDIP1"
        ///             },
        ///             {
        ///                 "CODE": "OOXY"
        ///             }
        ///          ]
        ///     },
        ///     "Value": "",
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add_group")]
        [HttpPost]
        public string POST_add_group([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);

                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料錯誤!須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                medGroupClass medGroupClass = returnData.Data.ObjToClass<medGroupClass>();
                if (medGroupClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料空白!須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                object[] value_medGroup = medGroupClass.ClassToSQL<medGroupClass, enum_medGroup>();
                List<object[]> list_sub_medGroup = medGroupClass.MedClasses.ClassToSQL<medClass, enum_sub_medGroup>();
        
                if (value_medGroup == null || list_sub_medGroup == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料異常!須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass sys_serverSettingClasses_med = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端")[0];

                MED_pageController mED_PageController = new MED_pageController();
                returnData returnData_med = new returnData();
                returnData_med.Server = sys_serverSettingClasses_med.Server;
                returnData_med.DbName = sys_serverSettingClasses_med.DBName;
                returnData_med.TableName = "medicine_page_cloud";
                returnData_med.UserName = sys_serverSettingClasses_med.User;
                returnData_med.Password = sys_serverSettingClasses_med.Password;
                returnData_med.Port = sys_serverSettingClasses_med.Port.StringToUInt32();
                returnData_med = mED_PageController.Get(returnData_med).JsonDeserializet<returnData>();
                List<medClass> medClasses = returnData_med.Data.ObjToListClass<medClass>();
                List<medClass> medClasses_buf = new List<medClass>();

           

                if (value_medGroup[(int)enum_medGroup.GUID].ObjectToString().StringIsEmpty() == true)
                {
                    List<object[]> list_med_group = sQLControl_med_group.GetAllRows(null);
                    List<object[]> list_med_group_buf = new List<object[]>();
                    List<object[]> list_sub_group = sQLControl_med_sub_group.GetAllRows(null);
                    List<object[]> list_sub_group_buf = new List<object[]>();
                    List<object[]> list_sub_group_add = new List<object[]>();

                    string Master_GUID = Guid.NewGuid().ToString();
                    value_medGroup[(int)enum_medGroup.GUID] = Master_GUID;
                    value_medGroup[(int)enum_medGroup.建立時間] = DateTime.Now.ToDateTimeString_6();

                    for (int i = 0; i < list_sub_medGroup.Count; i++)
                    {
                        string 藥品碼 = list_sub_medGroup[i][(int)enum_sub_medGroup.藥品碼].ObjectToString();
                        medClasses_buf = (from temp in medClasses
                                          where temp.藥品碼 == 藥品碼
                                          select temp).ToList();
                        if (medClasses_buf.Count > 0)
                        {
                            list_sub_medGroup[i][(int)enum_sub_medGroup.GUID] = Guid.NewGuid().ToString();
                            list_sub_medGroup[i][(int)enum_sub_medGroup.Master_GUID] = Master_GUID;
                            list_sub_group_add.Add(list_sub_medGroup[i]);
                        }
                    }

                    sQLControl_med_group.AddRow(null, value_medGroup);
                    sQLControl_med_sub_group.AddRows(null, list_sub_group_add);

                    returnData.Code = 200;
                    returnData.TimeTaken = myTimer.ToString();
                    returnData.Method = "add_group";
                    returnData.Result = $"寫入藥品群組資料成功!共新增<{list_sub_group_add.Count}>筆藥品";

                    return returnData.JsonSerializationt(true);

                }
                else
                {
                    string Master_GUID = value_medGroup[(int)enum_medGroup.GUID].ObjectToString();
                    List<object[]> list_med_group = sQLControl_med_group.GetRowsByDefult(null, (int)enum_medGroup.GUID, Master_GUID);
                    List<object[]> list_med_group_buf = new List<object[]>();
                    List<object[]> list_sub_group = sQLControl_med_sub_group.GetRowsByDefult(null, (int)enum_sub_medGroup.Master_GUID, Master_GUID);
                    List<object[]> list_sub_group_buf = new List<object[]>();
                    List<object[]> list_sub_group_add = new List<object[]>();
                    List<object[]> list_sub_group_replace = new List<object[]>();
                    sQLControl_med_sub_group.DeleteExtra(null, list_sub_group);
                    if (list_med_group.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"輸入資料異常! 找無此GUID藥品群組!";
                        return returnData.JsonSerializationt();
                    }

                    for (int i = 0; i < list_sub_medGroup.Count; i++)
                    {
                        string 藥品碼 = list_sub_medGroup[i][(int)enum_sub_medGroup.藥品碼].ObjectToString();
                        medClasses_buf = (from temp in medClasses
                                          where temp.藥品碼 == 藥品碼
                                          select temp).ToList();
                        if (medClasses_buf.Count > 0)
                        {
                            list_sub_medGroup[i][(int)enum_sub_medGroup.GUID] = Guid.NewGuid().ToString();
                            list_sub_medGroup[i][(int)enum_sub_medGroup.Master_GUID] = Master_GUID;
                            list_sub_group_add.Add(list_sub_medGroup[i]);
                        }
                    }
                    sQLControl_med_sub_group.AddRows(null, list_sub_group_add);

                    returnData.Code = 200;
                    returnData.TimeTaken = myTimer.ToString();
                    returnData.Method = "add_group";
                    returnData.Result = $"寫入藥品群組資料成功!共新增<{list_sub_group_add.Count}>筆藥品,共刪除<{list_sub_group.Count}>筆藥品!";
                    return returnData.JsonSerializationt(true);
                }
              
     

       
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 新增群組
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Value": "",
        ///     "ValueAry": ["群組名"],
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add_group_name")]
        [HttpPost]
        public string POST_add_group_name([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料錯誤!returnData.ValueAry 不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料錯誤!returnData.ValueAry應為 [\"群組名稱\"]";
                    return returnData.JsonSerializationt();
                }
           
                string 群組名 = returnData.ValueAry[0];
                if (群組名.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"群組名稱不可為空白";
                    return returnData.JsonSerializationt();
                }
                
                List<object[]> list_med_group = sQLControl_med_group.GetRowsByDefult(null,(int)enum_medGroup.名稱, 群組名);
                List<object[]> list_med_group_add = new List<object[]>();
                object[] value = new object[new enum_medGroup().GetLength()];
                if (list_med_group.Count == 0)
                {
                    value[(int)enum_medGroup.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_medGroup.名稱] = 群組名;
                    value[(int)enum_medGroup.建立時間] = DateTime.Now.ToDateTimeString();
                    list_med_group_add.Add(value);
                }
                else
                {                 
                    returnData.Code = -200;
                    returnData.Result = $"此群組名稱已存在";
                    return returnData.JsonSerializationt();                                  
                }
                
                sQLControl_med_group.AddRows(null, list_med_group_add);            
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "add_group_name";
                returnData.Result = $"新增{群組名}群組 成功";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 修改指定藥品群組名稱
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 藥品群組GUID<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        "NAME": "測試"
        ///     },
        ///     "Value": "f2563e24-9e54-4f47-9aa9-bb1f42e1bc34"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        /// <summary>
        /// 修改指定藥品群組名稱
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 藥品群組GUID<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        "NAME": "測試"
        ///     },
        ///     "Value": "f2563e24-9e54-4f47-9aa9-bb1f42e1bc34"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("group_rename_by_guid")]
        [HttpPost]
        public string POST_group_rename_by_guid([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                if(returnData.Value.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"搜尋條件[GUID]空白!";
                    return returnData.JsonSerializationt();
                }
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料錯誤!須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                medGroupClass medGroupClass = returnData.Data.ObjToClass<medGroupClass>();
                if (medGroupClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料空白!須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                object[] value_medGroup = medGroupClass.ClassToSQL<medGroupClass, enum_medGroup>();
                string GUID = returnData.Value;
                SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);
                List<object[]> list_medGroup = sQLControl_med_group.GetRowsByDefult(null, (int)enum_medGroup.GUID, GUID);
                if(list_medGroup.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無可更動名稱群組!";
                    return returnData.JsonSerializationt();
                }
                list_medGroup[0][(int)enum_medGroup.名稱] = value_medGroup[(int)enum_medGroup.名稱].ObjectToString();
                sQLControl_med_group.UpdateByDefulteExtra(null, list_medGroup);

                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "group_rename_by_guid";
                returnData.Result = $"藥品群組重新命名成功!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 刪除指定藥品群組
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.Value] : 藥品群組GUID<br/> 
        ///  --------------------------------------------<br/> 
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("delete_group_by_guid")]
        [HttpPost]
        public string POST_delete_group_by_guid([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                if (returnData.Value.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"搜尋條件[GUID]空白!";
                    return returnData.JsonSerializationt();
                }
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
         
                string Master_GUID = returnData.Value;
                SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);
                List<object[]> list_medGroup = sQLControl_med_group.GetRowsByDefult(null, (int)enum_medGroup.GUID, Master_GUID);
                List<object[]> list_med_sub_group = sQLControl_med_sub_group.GetRowsByDefult(null, (int)enum_sub_medGroup.Master_GUID, Master_GUID);
                if (list_medGroup.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無可刪除群組!";
                    return returnData.JsonSerializationt();
                }

                sQLControl_med_group.DeleteExtra(null, list_medGroup);
                sQLControl_med_sub_group.DeleteExtra(null, list_med_sub_group);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "group_rename_by_guid";
                returnData.Result = $"藥品群組刪除群組成功!共刪除<{list_med_sub_group.Count}>筆藥品!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 刪除指定群組內多個藥品
        /// </summary>
        /// <remarks>
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "GUID": "ef2f3e3e-a58d-4ac0-8878-d29aa1ccebdf",
        ///         "NAME": "TEST123",
        ///         "MedClasses": 
        ///          [
        ///             {
        ///                 "CODE": "IDIP1"
        ///             },
        ///             {
        ///                 "CODE": "OOXY"
        ///             }
        ///          ]
        ///     },
        ///     "Value": "",
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("delete_meds_in_group")]
        [HttpPost]
        public string POST_delete_meds_in_group([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
       
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料錯誤!須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                medGroupClass medGroupClass = returnData.Data.ObjToClass<medGroupClass>();
                if (medGroupClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料空白!須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }

                string Master_GUID = medGroupClass.GUID;
                SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);
                List<object[]> list_medGroup = sQLControl_med_group.GetRowsByDefult(null, (int)enum_medGroup.GUID, Master_GUID);
                List<object[]> list_med_sub_group = sQLControl_med_sub_group.GetRowsByDefult(null, (int)enum_sub_medGroup.Master_GUID, Master_GUID);
                List<object[]> list_med_sub_group_buf = new List<object[]>();
                List<object[]> list_med_sub_group_delete = new List<object[]>();
                if (list_medGroup.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無可異動群組!";
                    return returnData.JsonSerializationt();
                }
                for(int i = 0; i < medGroupClass.MedClasses.Count; i++)
                {
                    string 藥品碼 = medGroupClass.MedClasses[i].藥品碼;
                    list_med_sub_group_buf = list_med_sub_group.GetRows((int)enum_sub_medGroup.藥品碼, 藥品碼);
                    if(list_med_sub_group_buf.Count > 0)
                    {
                        list_med_sub_group_delete.Add(list_med_sub_group_buf[0]);
                    }
                }
                sQLControl_med_sub_group.DeleteExtra(null, list_med_sub_group_delete);
                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "delete_meds_in_group";
                returnData.Result = $"藥品資料刪除指定群組藥品成功!共刪除<{list_med_sub_group_delete.Count}>筆藥品!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 新增指定群組內多個藥品
        /// </summary>
        /// <remarks>
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "GUID": "ef2f3e3e-a58d-4ac0-8878-d29aa1ccebdf",
        ///         "NAME": "TEST123",
        ///         "MedClasses": 
        ///          [
        ///             {
        ///                 "CODE": "IDIP1"
        ///             },
        ///             {
        ///                 "CODE": "OOXY"
        ///             }
        ///          ]
        ///     },
        ///     "Value": "",
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add_meds_in_group")]
        [HttpPost]
        public string POST_add_meds_in_group([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);

                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料錯誤,須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                medGroupClass medGroupClass = returnData.Data.ObjToClass<medGroupClass>();
                if (medGroupClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"輸入資料空白,須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                if(medGroupClass.GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"medGroupClass.GUID,須為[medGroupClass]";
                    return returnData.JsonSerializationt();
                }
                
                sys_serverSettingClass sys_serverSettingClasses_med = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端")[0];
               
                List<object[]> list_med_group = sQLControl_med_group.GetRowsByDefult(null, (int)enum_medGroup.GUID, medGroupClass.GUID);
                if(list_med_group.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料,找無此GUID藥品群組";
                    return returnData.JsonSerializationt();
                }
                List<object[]> list_sub_group = sQLControl_med_sub_group.GetRowsByDefult(null, (int)enum_sub_medGroup.Master_GUID, medGroupClass.GUID);
                List<object[]> list_sub_group_buf = new List<object[]>();
                List<object[]> list_sub_group_add = new List<object[]>();
                for (int i = 0; i < medGroupClass.MedClasses.Count; i++) 
                {
                    string 藥品碼 = medGroupClass.MedClasses[i].藥品碼;
                    list_sub_group_buf = list_sub_group.GetRows((int)enum_sub_medGroup.藥品碼, 藥品碼);
                    if (list_sub_group_buf.Count == 0)
                    {
                        object[] value = new object[new enum_sub_medGroup().GetLength()];
                        value[(int)enum_sub_medGroup.GUID] = Guid.NewGuid().ToString();
                        value[(int)enum_sub_medGroup.藥品碼] = 藥品碼;
                        value[(int)enum_sub_medGroup.Master_GUID] = medGroupClass.GUID;
                        list_sub_group_add.Add(value);
                    }
                }
                
                sQLControl_med_sub_group.AddRows(null, list_sub_group_add);
                string API = GetServerAPI("Main", "網頁", "API01");

                (int code, string result, medGroupClass medGroupClass1) = medGroupClass.get_group_by_guid(API, medGroupClass.GUID);

                returnData.Code = 200;
                returnData.Data = medGroupClass1;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Method = "add_meds_in_group";
                returnData.Result = $"寫入藥品群組資料成功!共新增<{list_sub_group_add.Count}>筆藥品!";
                return returnData.JsonSerializationt(true);




            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 修改指定藥品群組顯示資訊
        /// </summary>
        /// <remarks>
        /// 以下為傳入範例資料結構
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        "NAME": "測試"
        ///     },
        ///     "ValueAry":
        ///     [
        ///       "GUID",
        ///       "顯示設備1",
        ///       "顯示設備2"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("visible_set_by_guid")]
        [HttpPost]
        public string POST_visible_set_by_guid([FromBody] returnData returnData)
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
      
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                if(returnData.ValueAry.Count < 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入ValueAry資訊錯誤";
                    return returnData.JsonSerializationt();
                }
               
                string GUID = returnData.ValueAry[0];
                List<string> visible_device = returnData.ValueAry.GetRange(1, returnData.ValueAry.Count - 1);


                SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);
                List<object[]> list_medGroup = sQLControl_med_group.GetRowsByDefult(null, (int)enum_medGroup.GUID, GUID);
                if (list_medGroup.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無可更動名稱群組!";
                    return returnData.JsonSerializationt();
                }
                string visible_device_str = string.Join(",", visible_device);
                list_medGroup[0][(int)enum_medGroup.顯示資訊] = visible_device_str;
                sQLControl_med_group.UpdateByDefulteExtra(null, list_medGroup);

                returnData.Code = 200;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Result = $"藥品群組顯示資訊更新成功 {visible_device_str}";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }

        private List<medGroupClass> Function_Get_medGroupClass(sys_serverSettingClass sys_serverSettingClass)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            CheckCreatTable(sys_serverSettingClass);
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl_med_group = new SQLControl(Server, DB, "med_group", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_med_sub_group = new SQLControl(Server, DB, "med_sub_group", UserName, Password, Port, SSLMode);

            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();

            List<medClass> medClasses = medClass.get_med_cloud("http://127.0.0.1:4433");
            List<medClass> medClasses_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_medClass = medClasses.CoverToDictionaryByCode();
            List<object[]> list_med_group = sQLControl_med_group.GetAllRows(null);
            List<object[]> list_med_sub_group = sQLControl_med_sub_group.GetAllRows(null);
            List<object[]> list_med_sub_group_buf = new List<object[]>();

            List<medGroupClass> medGroupClasses = list_med_group.SQLToClass<medGroupClass, enum_medGroup>();
            for (int i = 0; i < medGroupClasses.Count; i++)
            {
                string GUID = medGroupClasses[i].GUID;
                list_med_sub_group_buf = list_med_sub_group.GetRows((int)enum_sub_medGroup.Master_GUID, GUID);
                medGroupClasses[i].MedClasses = list_med_sub_group_buf.SQLToClass<medClass, enum_sub_medGroup>();
                for (int k = 0; k < medGroupClasses[i].MedClasses.Count; k++)
                {
                    medClasses_buf = keyValuePairs_medClass.SortDictionaryByCode(medGroupClasses[i].MedClasses[k].藥品碼);
                    if (medClasses_buf.Count > 0)
                    {
                        medGroupClasses[i].MedClasses[k] = medClasses_buf[0];
                    }
                }
   
            }
            return medGroupClasses;
        }
        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {

            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_medGroup()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClass, new enum_sub_medGroup()));
            return tables.JsonSerializationt(true);
        }
        private string GetServerAPI(string Name, string Type, string Content)
        {
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return sys_serverSettingClass.Server;
        }

    }
}
