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
using HIS_DB_Lib;
namespace HIS_WebApi
{


    [Route("api/[controller]")]
    [ApiController]
    public class MED_pageController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [Route("init")]
        [HttpPost]
        public string POST_init([FromBody] returnData returnData)
        {
            try
            {
         
                return CheckCreatTable(returnData);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [HttpPost]
        public string Get([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string Server = returnData.Server;
                string DbName = returnData.DbName;
                string TableName = returnData.TableName;
                string UserName = returnData.UserName;
                string Password = returnData.Password;
                uint Port = returnData.Port;
                SQLControl sQLControl_med = new SQLControl(Server, DbName, TableName, UserName, Password, Port, SSLMode);
                string[] colName = sQLControl_med.GetAllColumn_Name(TableName);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);

                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_雲端藥檔()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_雲端藥檔>();
                    returnData.Code = 200;
                    returnData.Result = "雲端藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_藥庫_藥品資料()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥庫_藥品資料>();
                    returnData.Code = 200;
                    returnData.Result = "藥庫藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_藥局_藥品資料()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥局_藥品資料>();
                    returnData.Code = 200;
                    returnData.Result = "藥局藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_藥品資料_藥檔資料()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    returnData.Code = 200;
                    returnData.Result = "調劑台藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                returnData.Code = -5;
                returnData.Result = "藥檔取得失敗!";

                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
           

        }


        /// <summary>
        /// 查詢藥品資料JSON格式範例
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.ServerName] : 填入記憶的伺服器名稱(medicine_page_cloud可預設為"Main")<br/> 
        ///  2.[returnData.ServerType] : 填入記憶的種類(medicine_page_cloud可預設為"網頁")<br/> 
        ///  3.[returnData.TableName] : 選擇其中一種資料表名稱:medicine_page_cloud、medicine_page_firstclass、medicine_page_phar、medicine_page<br/> 
        ///  --------------------------------------------<br/> 
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [Route("get_by_apiserver")]
        [HttpPost]
        public string POST_get_by_apiserver([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string TableName = returnData.TableName;
                returnData.Method = "get_by_apiserver";

                if (TableName == "medicine_page_cloud")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
                    serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");   
                    if(serverSettingClasses_buf.Count == 0)
                    {
                        serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM端");
                    }
                    string Server = serverSettingClasses_buf[0].Server;
                    string DB = serverSettingClasses_buf[0].DBName;
                    string UserName = serverSettingClasses_buf[0].User;
                    string Password = serverSettingClasses_buf[0].Password;
                    uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetAllRows(null);
                    returnData.Data = list_med.SQLToClass<medClass, enum_雲端藥檔>();
                    returnData.Code = 200;
                    returnData.Result = "雲端藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    string json_out = returnData.JsonSerializationt(false);
                    return json_out;
                }
                if (TableName == "medicine_page_firstclass")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetAllRows(null);
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥庫_藥品資料>();
                    returnData.Code = 200;
                    returnData.Result = "藥庫藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_phar")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetAllRows(null);
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥局_藥品資料>();
                    returnData.Code = 200;
                    returnData.Result = "藥局藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }

                if (TableName == "medicine_page")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    //取得雲端藥檔資料
                   
                    MED_pageController mED_PageController = new MED_pageController();
                    returnData returnData_med = new returnData();
                    returnData_med.ServerName = "Main";
                    returnData_med.ServerType = "網頁";
                    returnData_med.Server = serverSettingClasses_med.Server;
                    returnData_med.DbName = serverSettingClasses_med.DBName;
                    returnData_med.TableName = "medicine_page_cloud";
                    returnData_med.UserName = serverSettingClasses_med.User;
                    returnData_med.Password = serverSettingClasses_med.Password;
                    returnData_med.Port = serverSettingClasses_med.Port.StringToUInt32();
                    returnData_med = mED_PageController.POST_get_by_apiserver(returnData_med).JsonDeserializet<returnData>();
                    List<medClass> medClasses_cloud = returnData_med.Data.ObjToListClass<medClass>();
                    List<medClass> medClasses_cloud_buf = new List<medClass>();

                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetAllRows(null);
                    List<object[]> list_med_buf = new List<object[]>();
                    List<object[]> list_med_add = new List<object[]>();
          
                    deviceController _deviceController = new deviceController();
                    string json_devices = _deviceController.POST_all(returnData);
                    returnData returnData_devices = json_devices.JsonDeserializet<returnData>();
                    List<H_Pannel_lib.DeviceBasic> deviceBasics = returnData_devices.Data.ObjToClass<List<H_Pannel_lib.DeviceBasic>>();
                    List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();

                    string 藥碼 = "";
                    for (int i = 0; i < medClasses_cloud.Count; i++)
                    {
                        藥碼 = medClasses_cloud[i].藥品碼;
                        list_med_buf = list_med.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥碼);
                        if(list_med_buf.Count == 0)
                        {
                            list_med_add.Add(medClasses_cloud[i].ClassToSQL<medClass, enum_藥品資料_藥檔資料>());
                        }
                    }
                    sQLControl_med.AddRows(null, list_med_add);
                    list_med.LockAdd(list_med_add);
                    for (int i = 0; i < list_med.Count; i++)
                    {
                        藥碼 = list_med[i][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                        deviceBasics_buf = (from temp in deviceBasics
                                            where temp.Code == 藥碼
                                            select temp).ToList();
                        int inventory = 0;
                        for (int k = 0; k < deviceBasics_buf.Count; k++)
                        {
                            inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                        }
                        list_med[i][(int)enum_藥品資料_藥檔資料.庫存] = inventory.ToString();
                    }


                    List<medClass> medClasses = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    returnData.Data = medClasses;
                    returnData.Code = 200;
                    returnData.Result = $"本地藥檔取得成功!新增<{list_med_add.Count}>筆資料!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    string json_out = returnData.JsonSerializationt(false);
                    return json_out;
                }

                returnData.Code = -200;
                returnData.Result = "藥檔取得失敗!";
                return returnData.JsonSerializationt();
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
          
        }

        [Route("get_by_code")]
        [HttpPost]
        public string POST_get_by_code([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string TableName = returnData.TableName;
                returnData.Method = "get_by_code";
                string Code = returnData.Value;
                if(Code.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "Code空白!";
                    return returnData.JsonSerializationt();
                }
                if (TableName == "medicine_page_cloud")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    if (serverSettingClasses_buf.Count == 0)
                    {
                        serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM端");
                    }
                    string Server = serverSettingClasses_buf[0].Server;
                    string DB = serverSettingClasses_buf[0].DBName;
                    string UserName = serverSettingClasses_buf[0].User;
                    string Password = serverSettingClasses_buf[0].Password;
                    uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();
                    if (serverSettingClasses_buf.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_雲端藥檔.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_雲端藥檔>();
                    returnData.Code = 200;
                    returnData.Result = "雲端藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_firstclass")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_藥庫_藥品資料.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥庫_藥品資料>();
                    returnData.Code = 200;
                    returnData.Result = "藥庫藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_phar")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_藥局_藥品資料.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥局_藥品資料>();
                    returnData.Code = 200;
                    returnData.Result = "藥局藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }

                if (TableName == "medicine_page")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_藥品資料_藥檔資料.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    returnData.Code = 200;
                    returnData.Result = "調劑台藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }

                returnData.Code = -200;
                returnData.Result = "藥檔取得失敗!";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }

        [Route("upadte_by_guid")]
        [HttpPost]
        public string POST_upadte_by_guid([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string TableName = returnData.TableName;
                returnData.Method = "upadte_by_guid";
     
      
                List<medClass> medClasses = new List<medClass>();
                medClasses = returnData.Data.ObjToListClass<medClass>();
                if (medClasses == null)
                {
                    medClass medClass = returnData.Data.ObjToClass<medClass>();
                    if (medClass != null)
                    {
                        medClasses = new List<medClass>();
                        medClasses.Add(medClass);
                    }
                }
                if (medClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "反序列化失敗!";
                    return returnData.JsonSerializationt();
                }
                if (TableName == "medicine_page_cloud")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_replace = medClasses.ClassToSQL<medClass, enum_雲端藥檔>();
                    sQLControl_med.UpdateByDefulteExtra(null, list_replace);
                    returnData.Code = 200;
                    returnData.Result = "雲端藥檔更新成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_firstclass")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                    List<object[]> list_replace = medClasses.ClassToSQL<medClass, enum_藥庫_藥品資料>();
                    sQLControl_med.UpdateByDefulteExtra(null, list_replace);
                    returnData.Code = 200;
                    returnData.Result = "藥庫藥檔更新成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_phar")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                    List<object[]> list_replace = medClasses.ClassToSQL<medClass, enum_藥局_藥品資料>();
                    sQLControl_med.UpdateByDefulteExtra(null, list_replace);
                    returnData.Code = 200;
                    returnData.Result = "藥局藥檔更新成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                    List<object[]> list_replace = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                    sQLControl_med.UpdateByDefulteExtra(null, list_replace);
                    returnData.Code = 200;
                    returnData.Result = "調劑台藥檔更新成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                returnData.Code = -200;
                returnData.Result = "更新藥檔失敗!";

                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
         
        }

        [Route("serch_by_BarCode")]
        [HttpPost]
        public string POST_serch_by_BarCode([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                List<medClass> medClasses = returnData.Data.ObjToListClass<medClass>();
                if (medClasses == null)
                {
                    string json_result = POST_get_by_apiserver(returnData);
                    returnData = json_result.JsonDeserializet<returnData>();
                    medClasses = returnData.Data.ObjToListClass<medClass>();
                }

                returnData.Method = "serch_by_BarCode";
                string BarCode = returnData.Value;
                if(BarCode.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "BarCode空白!";
                    return returnData.JsonSerializationt();
                }
    
                List<medClass> medClasses_buf = new List<medClass>();

                for(int i = 0; i < medClasses.Count; i++)
                {
                    if (BarCode == medClasses[i].藥品碼)
                    {
                        medClasses_buf.Add(medClasses[i]);
                        continue;
                    }
                    if (BarCode == medClasses[i].料號)
                    {
                        medClasses_buf.Add(medClasses[i]);
                        continue;
                    }
                    foreach (string barcode in medClasses[i].Barcode)
                    {
                        if (barcode == BarCode)
                        {
                            medClasses_buf.Add(medClasses[i]);
                            continue;
                        }
                    }
                }
                returnData.Data = medClasses_buf;
                returnData.Code = 200;
                returnData.Result = "搜尋BARCODE完成!";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }


        private string CheckCreatTable(returnData returnData)
        {
            string TableName = returnData.TableName;
            Table table = new Table(TableName);
            if (TableName == "medicine_page_cloud")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                returnData.Method = "get_init";
                
                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
               
                table = new Table("medicine_page_cloud");
                table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
                table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("料號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("中文名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品學名", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品群組", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("健保碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("包裝單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("包裝數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("最小包裝單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("最小包裝數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("藥品條碼1", Table.StringType.VARCHAR, 200, Table.IndexType.None);
                table.AddColumnList("藥品條碼2", Table.StringType.TEXT, 200, Table.IndexType.None);
                table.AddColumnList("警訊藥品", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("高價藥品", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("生物製劑", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("管制級別", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("類別", Table.StringType.VARCHAR, 500, Table.IndexType.None);
                table.AddColumnList("廠牌", Table.StringType.VARCHAR, 200, Table.IndexType.None);
                table.AddColumnList("藥品許可證號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("開檔狀態", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.Server = Server;
                table.DBName = DB;
                table.Username = UserName;
                table.Password = Password;
                table.Port = Port.ToString();
                if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
                else sQLControl.CheckAllColumnName(table, true);
            }
            if (TableName == "medicine_page_firstclass")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                returnData.Method = "get_init";

                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
               
                table = new Table("medicine_page_firstclass");
                table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
                table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("料號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("中文名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品學名", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品群組", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("健保碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("包裝單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("包裝數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("最小包裝單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("最小包裝數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("藥局庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("藥庫庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("總庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("基準量", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("安全庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("藥品條碼1", Table.StringType.VARCHAR, 200, Table.IndexType.None);
                table.AddColumnList("藥品條碼2", Table.StringType.TEXT, 200, Table.IndexType.None);
                table.AddColumnList("開檔狀態", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.Server = Server;
                table.DBName = DB;
                table.Username = UserName;
                table.Password = Password;
                table.Port = Port.ToString();
                if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
                else sQLControl.CheckAllColumnName(table, true);
            }
            if (TableName == "medicine_page_phar")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                returnData.Method = "get_init";

                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
             
                table = new Table("medicine_page_phar");
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
            
                table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
                table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("料號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("中文名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品學名", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品群組", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("健保碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("包裝單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("包裝數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("最小包裝單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("最小包裝數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("藥局庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("藥庫庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("總庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("基準量", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("安全庫存", Table.StringType.VARCHAR, 15, Table.IndexType.None);
                table.AddColumnList("藥品條碼1", Table.StringType.VARCHAR, 200, Table.IndexType.None);
                table.AddColumnList("藥品條碼2", Table.StringType.TEXT, 200, Table.IndexType.None);
                table.AddColumnList("開檔狀態", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.Server = Server;
                table.DBName = DB;
                table.Username = UserName;
                table.Password = Password;
                table.Port = Port.ToString();
                if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
                else sQLControl.CheckAllColumnName(table, true);
            }

            if (TableName == "medicine_page")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                returnData.Method = "get_init";

                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
              
                table = new Table("medicine_page");
                table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
                table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("料號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
                table.AddColumnList("中文名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品學名", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("藥品群組", Table.StringType.VARCHAR, 300, Table.IndexType.None);
                table.AddColumnList("健保碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("藥品條碼", Table.StringType.VARCHAR, 20, Table.IndexType.None);
                table.AddColumnList("藥品條碼1", Table.StringType.VARCHAR, 20, Table.IndexType.None);
                table.AddColumnList("藥品條碼2", Table.StringType.TEXT, 200, Table.IndexType.None);
                table.AddColumnList("包裝單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("庫存", Table.StringType.VARCHAR, 20, Table.IndexType.None);
                table.AddColumnList("安全庫存", Table.StringType.VARCHAR, 20, Table.IndexType.None);
                table.AddColumnList("基準量", Table.StringType.VARCHAR, 20, Table.IndexType.None);
                table.AddColumnList("圖片網址", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("警訊藥品", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("高價藥品", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("生物製劑", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("管制級別", Table.StringType.VARCHAR, 10, Table.IndexType.None);
                table.AddColumnList("類別", Table.StringType.VARCHAR, 500, Table.IndexType.None);
                table.AddColumnList("廠牌", Table.StringType.VARCHAR, 200, Table.IndexType.None);
                table.AddColumnList("藥品許可證號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("開檔狀態", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.Server = Server;
                table.DBName = DB;
                table.Username = UserName;
                table.Password = Password;
                table.Port = Port.ToString();
                if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
                else sQLControl.CheckAllColumnName(table, true);
            }


            return table.JsonSerializationt(true);
        }
    }
}
