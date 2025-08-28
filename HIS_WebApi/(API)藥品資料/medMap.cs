using Basic;
using H_Pannel_lib;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MyUI;
using SQLUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_藥品資料
{
    [Route("api/[controller]")]
    [ApiController]
    public class medMap : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        static private string API_server = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "藥品地圖物件", typeof(medMapClass))]


        /// <summary>
        ///初始化藥品地圖資料庫
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[""]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("init")]
        public string init([FromBody] returnData returnData)
        {
            try
            {
                return CheckCreatTable();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"{ex.Message}";
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 新增父容器
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["設備名稱","類別","位置", "絕對位置"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add_medMap")]
        public async Task<string> add_medMap([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if(returnData.ValueAry.Count != 4)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"設備名稱\",\"類別\",\"位置\",\"絕對位置\"]";
                    return returnData.JsonSerializationt();
                }
                string 設備名稱 = returnData.ValueAry[0];
                string 類別 = returnData.ValueAry[1];
                string 位置 = returnData .ValueAry[2];
                string 絕對位置 = returnData.ValueAry[3];

                if (位置.Split(",").Count() != 2 || 絕對位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                List<sys_serverSettingClass> sys_ServerSettingClasses = await ServerSettingController.GetAllServerSettingasync(設備名稱, 類別, "一般資料");
                if (sys_ServerSettingClasses == null || sys_ServerSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = sys_ServerSettingClasses[0].GUID;

                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
                
                List<object[]> objects = await sQLControl_medMap.GetRowsByDefultAsync(null, (int)enum_medMap.Master_GUID, Master_GUID);
                if(objects.Count > 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料已存在!";
                    return returnData.JsonSerializationt();
                }
                medMapClass medMapClass = new medMapClass();
                medMapClass.GUID = Guid.NewGuid().ToString();
                medMapClass.Master_GUID = Master_GUID;
                medMapClass.位置 = 位置;
                medMapClass.絕對位置 = 位置;
                medMapClass.type = "container";

                object[] add = medMapClass.ClassToSQL<medMapClass, enum_medMap>();
                await sQLControl_medMap.AddRowAsync(null, add);
                medMapClass.sys_ServerSetting = sys_ServerSettingClasses[0];

                returnData.Code = 200;
                returnData.Data = medMapClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add_group";
                returnData.Result = $"父容器資料寫入成功!";
                return returnData.JsonSerializationt(true);
               
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 修改父容器(位置)
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     [
        ///         {
        ///             "GUID":
        ///             "position":"0,1",
        ///             "absolute_position":"10,10"
        ///         }
        ///     ],
        ///     "Value": "",
        ///     "ValueAry":["GUID","Master_GUID","位置"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_medMap")]
        public async Task<string> update_medMap([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                List<medMapClass> medMapClasses = returnData.Data.ObjToClass<List<medMapClass>>();
                string[] GUID = medMapClasses.Select(x => x.GUID).ToArray();
                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl_medMap.GetRowsByDefultAsync(null, (int)enum_medMap.GUID, GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                List<medMapClass> medMaps = objects.SQLToClass<medMapClass, enum_medMap>();
                foreach (var item in medMaps)
                {
                    medMapClass medMap_buff = medMapClasses.Where(x => x.GUID == item.GUID).FirstOrDefault();
                    if (medMap_buff == null) continue;
                    if (medMap_buff.位置.StringIsEmpty() == false && medMap_buff.位置.Split(",").Count() == 2) item.位置 = medMap_buff.位置;
                    if (medMap_buff.絕對位置.StringIsEmpty() == false && medMap_buff.絕對位置.Split(",").Count() == 2) 
                        item.絕對位置 = medMap_buff.絕對位置;
                }
                List<object[]> update = medMaps.ClassToSQL<medMapClass, enum_medMap>();
                await sQLControl_medMap.UpdateRowsAsync(null, update);
                
                returnData.Code = 200;
                returnData.Data = medMaps;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "update_medMap";
                returnData.Result = $"父容器資料更新成功!";
                return returnData.JsonSerializationt(true);
               
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以設備名稱和類別取得父容器資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["設備名稱","類別"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_by_department")]
        public async Task<string> get_medMap_by_department([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[department type]";
                    return returnData.JsonSerializationt(true);
                }
                ServerSettingController controller = new ServerSettingController();
                string result = await controller.POST_get_serversetting_by_department_type(returnData);
                returnData returnData_get_serversetting_by_type = result.JsonDeserializet<returnData>();

                List<sys_serverSettingClass> sys_serverSettingClasses = returnData_get_serversetting_by_type.Data.ObjToClass<List<sys_serverSettingClass>>();
                if (sys_serverSettingClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"POST_get_serversetting_by_type 回傳為空";
                    return returnData.JsonSerializationt(true);
                }
                List<Task<medMapClass>> tasks = new List<Task<medMapClass>>();
                List<medMapClass> medMapClasses = new List<medMapClass>();

                foreach (var item in sys_serverSettingClasses)
                {                    
                    returnData returnData_get_medMap_by_name_type = await get_medMap_by_name_type(item.設備名稱, item.類別);
                    medMapClass medMapClass = returnData_get_medMap_by_name_type.Data.ObjToClass<medMapClass>();
                    medMapClasses.Add(medMapClass);
                }
                

                returnData.Code = 200;
                returnData.Data = medMapClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_by_department";
                returnData.Result = $"取得{returnData.ValueAry[0]}單位藥品地圖資料，共{medMapClasses.Count}!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Index was outside the bounds of the array.") init(returnData);
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以設備名稱和類別取得父容器資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["設備名稱","類別"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_by_name_type")]
        public async Task<string> get_medMap_by_name_type([FromBody] returnData returnData, CancellationToken ct = default)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"設備名稱\",\"類別\"]";
                    return returnData.JsonSerializationt();
                }
                string 設備名稱 = returnData.ValueAry[0];
                string 類別 = returnData.ValueAry[1];
                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                sys_serverSettingClass sys_ServerSetting = await Method.GetServerAsync(設備名稱, 類別, "一般資料");

                if (sys_ServerSetting == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = sys_ServerSetting.GUID;
                string tableName = "medMap";
                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
       
                List<object[]> objects = await sQLControl_medMap.GetRowsByDefultAsync(null, (int)enum_medMap.Master_GUID, Master_GUID);

                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMapClass medMapClasses = objects.SQLToClass<medMapClass, enum_medMap>()[0];
                medMapClasses.sys_ServerSetting = sys_ServerSetting;

                returnData returnData_get_medMap_section_by_Master_GUID = await get_medMap_section_by_Master_GUID(medMapClasses.GUID);
                if (returnData_get_medMap_section_by_Master_GUID == null || returnData_get_medMap_section_by_Master_GUID.Code != 200)
                {
                    return returnData_get_medMap_section_by_Master_GUID.JsonSerializationt(true);
                }
                List<medMap_sectionClass> medMap_SectionClasses = returnData_get_medMap_section_by_Master_GUID.Data.ObjToClass<List<medMap_sectionClass>>();
                medMapClasses.medMap_Section = medMap_SectionClasses;

                returnData.Code = 200;
                returnData.Data = medMapClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_by_name_type";
                returnData.Result = $"取得父容器資料成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID取得父容器資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["設備名稱","類別"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_by_GUID")]
        public async Task<string> get_medMap_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\"]";
                    return returnData.JsonSerializationt();
                }
                string GUID = returnData.ValueAry[0];
                Task< List<sys_serverSettingClass>> task_sys_serverSettingClasses = ServerSettingController.GetAllServerSettingasync();

                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl_medMap.GetRowsByDefultAsync(null, (int)enum_medMap.GUID, GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMapClass medMapClasses = objects.SQLToClass<medMapClass, enum_medMap>()[0];
                returnData returnData_get_by_GUID = await new ServerSettingController().get_by_GUID(medMapClasses.Master_GUID);
                if(returnData_get_by_GUID == null || returnData_get_by_GUID.Code != 200)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                List<sys_serverSettingClass> sys_serverSettingClasses = returnData_get_by_GUID.Data.ObjToClass<List<sys_serverSettingClass>>();
               

             
                returnData returnData_get_medMap_section_by_Master_GUID = await get_medMap_section_by_Master_GUID(GUID);
                if (returnData_get_medMap_section_by_Master_GUID.Code != 200)
                {
                    return returnData_get_medMap_section_by_Master_GUID.JsonSerializationt(true);
                }
                List<medMap_sectionClass> medMap_SectionClasses = returnData_get_medMap_section_by_Master_GUID.Data.ObjToClass<List<medMap_sectionClass>>();

                if(sys_serverSettingClasses.Count > 0) medMapClasses.sys_ServerSetting = sys_serverSettingClasses[0];
                medMapClasses.medMap_Section = medMap_SectionClasses;

                returnData.Code = 200;
                returnData.Data = medMapClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_by_GUID";
                returnData.Result = $"取得父容器資料成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 新增子容器
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Value": "",
        ///     "ValueAry":["所屬父容器GUID","位置"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add_medMap_section")]
        public string add_medMap_section([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"所屬父容器GUID\",\"位置\"]";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = returnData.ValueAry[0];
                string 位置 = returnData.ValueAry[1];
                if (位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_section = new SQLControl(Server, DB, "medMap_section", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap_section.GetRowsByDefult(null, (int)enum_medMap_section.Master_GUID, Master_GUID);
                List<medMap_sectionClass> medMap_sectionClasses = objects.SQLToClass<medMap_sectionClass, enum_medMap_section>();
                if(medMap_sectionClasses.Count > 0)
                {
                    List<medMap_sectionClass> medMap_sectionClass_buff = medMap_sectionClasses.Where(item => item.位置 == 位置).ToList();
                    if (medMap_sectionClass_buff.Count > 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"位置已重複!";
                        return returnData.JsonSerializationt(true);
                    }
                }
                
                medMap_sectionClass medMap_sectionClass = new medMap_sectionClass();
                medMap_sectionClass.GUID = Guid.NewGuid().ToString();
                medMap_sectionClass.Master_GUID = Master_GUID;
                medMap_sectionClass.位置 = 位置;

                object[] add = medMap_sectionClass.ClassToSQL<medMap_sectionClass, enum_medMap_section>();
                sQLControl_medMap_section.AddRow(null, add);

                returnData.Code = 200;
                returnData.Data = medMap_sectionClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add_medMap_section";
                returnData.Result = $"子容器資料寫入成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新子容器位置
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     [
        ///         {
        ///             "GUID":
        ///             "position":"0,1",
        ///         }
        ///     ]
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_medMap_section")]
        public async Task<string> update_medMap_section([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                List<medMap_sectionClass> medMap_sectionClasses = returnData.Data.ObjToClass<List<medMap_sectionClass>>();
                string[] GUID = medMap_sectionClasses.Select(x => x.GUID).ToArray();
                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_section = new SQLControl(Server, DB, "medMap_section", UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl_medMap_section.GetRowsByDefultAsync(null, (int)enum_medMap_section.GUID, GUID);
                List<medMap_sectionClass> medMaps_section = objects.SQLToClass<medMap_sectionClass, enum_medMap_section>();
                if (medMaps_section.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                foreach (var item in medMaps_section)
                {
                    medMap_sectionClass medMap_section_buff = medMap_sectionClasses.Where(x => x.GUID == item.GUID).FirstOrDefault();
                    if (medMap_section_buff == null) continue;
                    if (medMap_section_buff.位置.StringIsEmpty() == false && medMap_section_buff.位置.Split(",").Count() == 2) item.位置 = medMap_section_buff.位置;
                }
                
                List<object[]> update = medMaps_section.ClassToSQL<medMap_sectionClass, enum_medMap_section>();
                await sQLControl_medMap_section.UpdateRowsAsync(null, update);

                returnData.Code = 200;
                returnData.Data = medMaps_section;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "update_medMap_section";
                returnData.Result = $"子容器資料更新成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        
        /// <summary>
        /// 以Master_GUID取得子容器資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["Master_GUID"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_section_by_Master_GUID")]
        public async Task< string> get_medMap_section_by_Master_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\"]";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "medMap_section", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl.GetRowsByDefult(null, (int)enum_medMap_section.Master_GUID, Master_GUID);
                List<medMap_sectionClass> medMap_sectionClass = objects.SQLToClass<medMap_sectionClass, enum_medMap_section>();
                List<Task> tasks = new List<Task>();
                foreach(var item in medMap_sectionClass)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        string subSection = get_medMap_sub_section_by_Master_GUID(item.GUID);
                        returnData returnData_get_medMap_sub_section_by_Master_GUID = subSection.JsonDeserializet<returnData>();
                        if (returnData_get_medMap_sub_section_by_Master_GUID.Code != 200) return;   
                        List<medMap_sub_sectionClass> medMap_sub_sectionClass = returnData_get_medMap_sub_section_by_Master_GUID.Data.ObjToClass<List<medMap_sub_sectionClass>>();
                        if (medMap_sub_sectionClass != null) item.sub_section = medMap_sub_sectionClass;
                    })));
                   
                    
                }
                Task.WhenAll(tasks).Wait();
                
                returnData.Code = 200;
                returnData.Data = medMap_sectionClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_section_by_Master_GUID";
                returnData.Result = $"取得子容器資料成功，共{medMap_sectionClass.Count}筆!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以Master_GUID取得子容器資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["Master_GUID"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_sub_section_by_Master_GUID")]
        public string get_medMap_sub_section_by_Master_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\"]";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "medMap_sub_section", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl.GetRowsByDefult(null, (int)enum_medMap_sub_section.Master_GUID, Master_GUID);
                List<medMap_sub_sectionClass> medMap_Sub_Sections = objects.SQLToClass<medMap_sub_sectionClass, enum_medMap_sub_section>();
                List<Task> tasks = new List<Task>();
                foreach (var item in medMap_Sub_Sections)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        string shelf_jsonString = get_medMap_shelf_by_Master_GUID(item.GUID);
                        returnData returnData_get_medMap_shelf_by_Master_GUID = shelf_jsonString.JsonDeserializet<returnData>();
                        if (returnData_get_medMap_shelf_by_Master_GUID.Code != 200) return;
                        List<medMap_shelfClass> medMap_ShelfClasses = returnData_get_medMap_shelf_by_Master_GUID.Data.ObjToClass<List<medMap_shelfClass>>();
                        if (medMap_ShelfClasses != null) item.shelf = medMap_ShelfClasses;
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        string shelf_jsonString = get_medMap_drawer_by_Master_GUID(item.GUID);
                        returnData returnData_get_medMap_drawer_by_Master_GUID = shelf_jsonString.JsonDeserializet<returnData>();
                        if (returnData_get_medMap_drawer_by_Master_GUID.Code != 200) returnData_get_medMap_drawer_by_Master_GUID.JsonSerializationt(true);
                        List<medMap_drawerClass> medMap_drawerClasses = returnData_get_medMap_drawer_by_Master_GUID.Data.ObjToClass<List<medMap_drawerClass>>();
                        if (medMap_drawerClasses != null) item.drawer = medMap_drawerClasses;
                    })));

                }
                Task.WhenAll(tasks).Wait();

                returnData.Code = 200;
                returnData.Data = medMap_Sub_Sections;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_section_by_Master_GUID";
                returnData.Result = $"取得子容器資料成功，共{medMap_Sub_Sections.Count}筆!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 新增層架
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     {
        ///         "Master_GUID":"子容器GUID",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_light":"燈條IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add_medMap_shelf")]
        public string add_medMap_shelf([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data不得為空";
                    return returnData.JsonSerializationt();
                }
                medMap_shelfClass medMap_ShelfClass = returnData.Data.ObjToClass<medMap_shelfClass>();
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤，須為 medMap_shelfClass";
                    return returnData.JsonSerializationt();
                }
                if (medMap_ShelfClass.位置.StringIsEmpty() || medMap_ShelfClass.位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料為空，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                
                if (medMap_ShelfClass.Master_GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"Master_GUID資料為空";
                    return returnData.JsonSerializationt();
                }
                if (medMap_ShelfClass.寬度.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"寬度資料為空";
                    return returnData.JsonSerializationt();
                }
                if (medMap_ShelfClass.高度.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"高度資料為空";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_shelf = new SQLControl(Server, DB, "medMap_shelf", UserName, Password, Port, SSLMode);

                List<object[]> objects = sQLControl_medMap_shelf.GetRowsByDefult(null, (int)enum_medMap_shelf.Master_GUID, medMap_ShelfClass.Master_GUID);
                List<medMap_shelfClass> medMap_ShelfClasses = objects.SQLToClass<medMap_shelfClass, enum_medMap_shelf>();
                if(medMap_ShelfClasses.Count > 0)
                {
                    List<medMap_shelfClass> medMap_shelfClass_buff = medMap_ShelfClasses.Where(item => item.位置 == medMap_ShelfClass.位置).ToList();
                    if (medMap_shelfClass_buff.Count > 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"位置已重複!";
                        return returnData.JsonSerializationt();
                    }
                }
                medMap_ShelfClass.GUID = Guid.NewGuid().ToString();
                medMap_ShelfClass.type = "shelf";


                object[] add = medMap_ShelfClass.ClassToSQL<medMap_shelfClass, enum_medMap_shelf>();
                sQLControl_medMap_shelf.AddRow(null, add);

                returnData.Code = 200;
                returnData.Data = medMap_ShelfClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add_medMap_shelf";
                returnData.Result = $"層架資料寫入成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新層架資料(位置、寬度、高度、燈條IP)
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     {
        ///         "GUID":"",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_light":"燈條IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_medMap_shelf")]
        public async Task<string> update_medMap_shelf([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data不得為空";
                    return returnData.JsonSerializationt();
                }
                List<medMap_shelfClass> medMap_ShelfClass = returnData.Data.ObjToClass<List<medMap_shelfClass>>();
                string[] GUID = medMap_ShelfClass.Select(x => x.GUID).ToArray();

                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_shelf = new SQLControl(Server, DB, "medMap_shelf", UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl_medMap_shelf.GetRowsByDefultAsync(null, (int)enum_medMap_shelf.GUID, GUID);
                List<medMap_shelfClass> medMap_shelfClasses = objects.SQLToClass<medMap_shelfClass, enum_medMap_shelf>();
                if (medMap_shelfClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                foreach (var item in medMap_shelfClasses)
                {
                    medMap_shelfClass medMap_shelfClass_buff = medMap_ShelfClass.Where(x => x.GUID == item.GUID).FirstOrDefault();
                    if (medMap_shelfClass_buff == null) continue;
                    if (medMap_shelfClass_buff.位置.StringIsEmpty() == false&& medMap_shelfClass_buff.位置.Split(",").Count() == 2) item.位置 = medMap_shelfClass_buff.位置;
                    if (medMap_shelfClass_buff.寬度.StringIsEmpty() == false) item.寬度 = medMap_shelfClass_buff.寬度;
                    if (medMap_shelfClass_buff.高度.StringIsEmpty() == false) item.高度 = medMap_shelfClass_buff.高度;
                    if (medMap_shelfClass_buff.燈條IP.StringIsEmpty() == false) item.燈條IP = medMap_shelfClass_buff.燈條IP;
                    if (medMap_shelfClass_buff.Master_GUID.StringIsEmpty() == false && medMap_shelfClass_buff.serverName.StringIsEmpty() == false && medMap_shelfClass_buff.serverType.StringIsEmpty() == false) 
                    {
                        item.Master_GUID = medMap_shelfClass_buff.Master_GUID;
                        item.serverName = medMap_shelfClass_buff.serverName;
                        item.serverType = medMap_shelfClass_buff.serverType;

                    }



                }
                List<object[]> update = medMap_shelfClasses.ClassToSQL<medMap_shelfClass, enum_medMap_shelf>();

                await sQLControl_medMap_shelf.UpdateRowsAsync(null, update);

                returnData.Code = 200;
                returnData.Data = medMap_shelfClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "update_medMap_section";
                returnData.Result = $"層架資料更新成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID取得層架資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["GUID"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_shelf_by_GUID")]
        public string get_medMap_shelf_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\"]";
                    return returnData.JsonSerializationt();
                }
                string GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap_shelf", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap.GetRowsByDefult(null, (int)enum_medMap_shelf.GUID, GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMap_shelfClass medMap_shelfClass = objects.SQLToClass<medMap_shelfClass, enum_medMap_shelf>()[0];
                string jsonString = get_medMap_box_by_Master_GUID(GUID);
                returnData returnData_get_medMap_box_by_Master_GUID = jsonString.JsonDeserializet<returnData>();
                if (returnData_get_medMap_box_by_Master_GUID.Code != 200) return returnData_get_medMap_box_by_Master_GUID.JsonSerializationt(true);
                List<medMap_boxClass> medMap_boxClassses = returnData_get_medMap_box_by_Master_GUID.Data.ObjToClass<List<medMap_boxClass>>();
                medMap_shelfClass.medMapBox = medMap_boxClassses;



                returnData.Code = 200;
                returnData.Data = medMap_shelfClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_shelf_by_GUID";
                returnData.Result = $"取得層架資料成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以Master_GUID取得層架資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["Master_GUID"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_shelf_by_Master_GUID")]
        public string get_medMap_shelf_by_Master_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\"]";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "medMap_shelf", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl.GetRowsByDefult(null, (int)enum_medMap_shelf.Master_GUID, Master_GUID);
                List<medMap_shelfClass> medMap_shelfClass = objects.SQLToClass<medMap_shelfClass, enum_medMap_shelf>();
                List<Task> tasks = new List<Task>();
                foreach (var item in medMap_shelfClass)
                {
                    tasks.Add(Task.Run(new Action(delegate 
                    {
                        RowsLED rowsLED = deviceApiClass.GetRowsLED_ByIP(API_server, item.serverName, item.serverType, item.燈條IP);
                        if(rowsLED != null) item.rowsLED = rowsLED;
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        string jsonString = get_medMap_box_by_Master_GUID(item.GUID);
                        returnData returnData_get_medMap_box_by_Master_GUID = jsonString.JsonDeserializet<returnData>();
                        if (returnData_get_medMap_box_by_Master_GUID.Code != 200) return;
                        List<medMap_boxClass> medMap_boxClassses = returnData_get_medMap_box_by_Master_GUID.Data.ObjToClass<List<medMap_boxClass>>();
                        item.medMapBox = medMap_boxClassses;
                    })));
                }
                Task.WhenAll(tasks).Wait();
                returnData.Code = 200;
                returnData.Data = medMap_shelfClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_shelf_by_GUID";
                returnData.Result = $"取得層架資料成功，共{medMap_shelfClass.Count}筆!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 新增抽屜
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     {
        ///         "Master_GUID":"子容器GUID",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_drawer":"抽屜IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add_medMap_drawer")]
        public string add_medMap_drawer([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data不得為空";
                    return returnData.JsonSerializationt();
                }
                medMap_drawerClass medMap_drawerClass = returnData.Data.ObjToClass<medMap_drawerClass>();
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤，須為 medMap_shelfClass";
                    return returnData.JsonSerializationt();
                }
                if (medMap_drawerClass.位置.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料為空，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                if (medMap_drawerClass.位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                if (medMap_drawerClass.Master_GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"Master_GUID資料為空";
                    return returnData.JsonSerializationt();
                }
                if (medMap_drawerClass.寬度.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"寬度資料為空";
                    return returnData.JsonSerializationt();
                }
                if (medMap_drawerClass.高度.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"高度資料為空";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_drawer = new SQLControl(Server, DB, "medMap_drawer", UserName, Password, Port, SSLMode);

                List<object[]> objects = sQLControl_medMap_drawer.GetRowsByDefult(null, (int)enum_medMap_drawer.Master_GUID, medMap_drawerClass.Master_GUID);
                List<medMap_drawerClass> medMap_drawerClasses = objects.SQLToClass<medMap_drawerClass, enum_medMap_drawer>();
                if (medMap_drawerClasses.Count > 0)
                {
                    List<medMap_drawerClass> medMap_drawerClass_buff = medMap_drawerClasses.Where(item => item.位置 == medMap_drawerClass.位置).ToList();
                    if (medMap_drawerClass_buff.Count > 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"位置已重複!";
                        return returnData.JsonSerializationt();
                    }
                }
                medMap_drawerClass.GUID = Guid.NewGuid().ToString();

                object[] add = medMap_drawerClass.ClassToSQL<medMap_drawerClass, enum_medMap_drawer>();
                sQLControl_medMap_drawer.AddRow(null, add);

                returnData.Code = 200;
                returnData.Data = medMap_drawerClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add_medMap_drawer";
                returnData.Result = $"抽屜資料寫入成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新抽屜資料(位置、寬度、高度、抽屜IP)
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     {
        ///         "GUID":"",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_light":"抽屜IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_medMap_drawer")]
        public async Task<string> update_medMap_drawer([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data不得為空";
                    return returnData.JsonSerializationt();
                }
                List<medMap_drawerClass> medMap_drawerClass = returnData.Data.ObjToClass<List<medMap_drawerClass>>();
                string[] GUID = medMap_drawerClass.Select(x => x.GUID).ToArray();

                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_drawer = new SQLControl(Server, DB, "medMap_drawer", UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl_medMap_drawer.GetRowsByDefultAsync(null, (int)enum_medMap_drawer.GUID, GUID);
                List<medMap_drawerClass> medMap_drawerClasses = objects.SQLToClass<medMap_drawerClass, enum_medMap_drawer>();
                if (medMap_drawerClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                foreach (var item in medMap_drawerClasses)
                {
                    medMap_drawerClass medMap_shelfClass_buff = medMap_drawerClass.Where(x => x.GUID == item.GUID).FirstOrDefault();
                    if (medMap_shelfClass_buff == null) continue;
                    if (medMap_shelfClass_buff.位置.StringIsEmpty() == false && medMap_shelfClass_buff.位置.Split(",").Count() == 2) item.位置 = medMap_shelfClass_buff.位置;
                    if (medMap_shelfClass_buff.寬度.StringIsEmpty() == false) item.寬度 = medMap_shelfClass_buff.寬度;
                    if (medMap_shelfClass_buff.高度.StringIsEmpty() == false) item.高度 = medMap_shelfClass_buff.高度;
                    if (medMap_shelfClass_buff.抽屜IP.StringIsEmpty() == false) item.抽屜IP = medMap_shelfClass_buff.抽屜IP;
                    if (medMap_shelfClass_buff.Master_GUID.StringIsEmpty() == false && medMap_shelfClass_buff.serverName.StringIsEmpty() == false && medMap_shelfClass_buff.serverType.StringIsEmpty() == false) 
                    {
                        item.Master_GUID = medMap_shelfClass_buff.Master_GUID;
                        item.serverName = medMap_shelfClass_buff.serverName;
                        item.serverType = medMap_shelfClass_buff.serverType;
                    }

                }

                List<object[]> update = medMap_drawerClasses.ClassToSQL<medMap_drawerClass, enum_medMap_drawer>();
                await sQLControl_medMap_drawer.UpdateRowsAsync(null, update);

                returnData.Code = 200;
                returnData.Data = medMap_drawerClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "update_medMap_drawer";
                returnData.Result = $"抽屜資料更新成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以Master_GUID取得抽屜資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["Master_GUID"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_drawer_by_Master_GUID")]
        public string get_medMap_drawer_by_Master_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\"]";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "medMap_drawer", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl.GetRowsByDefult(null, (int)enum_medMap_drawer.Master_GUID, Master_GUID);
                List<medMap_drawerClass> medMap_DrawerClasses = objects.SQLToClass<medMap_drawerClass, enum_medMap_drawer>();
                List<Task> tasks = new List<Task>();    
                foreach (var item in medMap_DrawerClasses)
                {
                    tasks.Add(Task.Run(new Action(delegate 
                    {
                        Drawer drawer = deviceApiClass.Get_EPD583_Drawer_ByIP(API_server, item.serverName, item.serverType, item.抽屜IP);
                        if (drawer != null) item.drawer = drawer;
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        Storage storage = deviceApiClass.Get_EPD266_Storage_ByIP(API_server, item.serverName, item.serverType, item.抽屜IP);
                        if (storage != null) item.storage = storage;
                    })));
                }
                Task.WhenAll(tasks).Wait();
                returnData.Code = 200;
                returnData.Data = medMap_DrawerClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_shelf_by_GUID";
                returnData.Result = $"取得抽屜資料成功，共{medMap_DrawerClasses.Count}筆!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 新增藥盒
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     {
        ///         "Master_GUID":"子容器GUID",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_drawer":"抽屜IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add_medMap_box")]
        public string add_medMap_box([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data不得為空";
                    return returnData.JsonSerializationt();
                }
                medMap_boxClass medMap_boxClass = returnData.Data.ObjToClass<medMap_boxClass>();
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤，須為medMap_boxClass";
                    return returnData.JsonSerializationt();
                }
                if (medMap_boxClass.位置.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料為空，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                if (medMap_boxClass.位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                if (medMap_boxClass.Master_GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"Master_GUID資料為空";
                    return returnData.JsonSerializationt();
                }
                if (medMap_boxClass.寬度.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"寬度資料為空";
                    return returnData.JsonSerializationt();
                }
                if (medMap_boxClass.高度.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"高度資料為空";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_box = new SQLControl(Server, DB, "medMap_box", UserName, Password, Port, SSLMode);

                List<object[]> objects = sQLControl_medMap_box.GetRowsByDefult(null, (int)enum_medMap_box.Master_GUID, medMap_boxClass.Master_GUID);
                List<medMap_boxClass> medMap_boxClasses = objects.SQLToClass<medMap_boxClass, enum_medMap_box>();
                if (medMap_boxClasses.Count > 0)
                {
                    List<medMap_boxClass> medMap_boxClass_buff = medMap_boxClasses.Where(item => item.位置 == medMap_boxClass.位置).ToList();
                    if (medMap_boxClass_buff.Count > 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"位置已重複!";
                        return returnData.JsonSerializationt();
                    }
                }
                medMap_boxClass.GUID = Guid.NewGuid().ToString();

                object[] add = medMap_boxClass.ClassToSQL<medMap_boxClass, enum_medMap_box>();
                sQLControl_medMap_box.AddRow(null, add);

                returnData.Code = 200;
                returnData.Data = medMap_boxClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add_medMap_drawer";
                returnData.Result = $"抽屜資料寫入成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新藥盒資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     [
        ///         {
        ///             "GUID":"",
        ///             "Master_GUID":"",
        ///             "position":"位置",
        ///             "width":"寬度",
        ///             "height":"高度",
        ///             "ip_light":"藥盒IP",
        ///             "serverName":"",
        ///             "serverType":"",
        ///         }
        ///     ],
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_medMap_box")]
        public async Task<string> update_medMap_box([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data不得為空";
                    return returnData.JsonSerializationt();
                }
                List<medMap_boxClass> medMap_boxClass = returnData.Data.ObjToClass<List<medMap_boxClass>>();
                string[] GUID = medMap_boxClass.Select(x => x.GUID).ToArray();


                (string Server, string DB, string UserName, string Password, uint Port) = await Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_box = new SQLControl(Server, DB, "medMap_box", UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl_medMap_box.GetRowsByDefultAsync(null, (int)enum_medMap_box.GUID, GUID);
                List<medMap_boxClass> medMap_boxClasses = objects.SQLToClass<medMap_boxClass, enum_medMap_box>();
                if (medMap_boxClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                foreach (var item in medMap_boxClasses)
                {
                    medMap_boxClass medMap_box_buff = medMap_boxClass.Where(x => x.GUID == item.GUID).FirstOrDefault();
                    if (medMap_box_buff == null) continue;
                    if (medMap_box_buff.位置.StringIsEmpty() == false && medMap_box_buff.位置.Split(",").Count() == 2) item.位置 = medMap_box_buff.位置;
                    if (medMap_box_buff.寬度.StringIsEmpty() == false && medMap_box_buff.寬度.Split(",").Count() == 2) item.寬度 = medMap_box_buff.寬度;
                    if (medMap_box_buff.高度.StringIsEmpty() == false) item.高度 = medMap_box_buff.高度;
                    if (medMap_box_buff.藥盒IP.StringIsEmpty() == false) item.藥盒IP = medMap_box_buff.藥盒IP;
                    if (medMap_box_buff.Master_GUID.StringIsEmpty() == false && medMap_box_buff.serverName.StringIsEmpty() == false && medMap_box_buff.serverType.StringIsEmpty() == false)
                    {
                        item.Master_GUID = medMap_box_buff.Master_GUID;
                        item.serverName = medMap_box_buff.serverName;
                        item.serverType = medMap_box_buff.serverType;
                    }
                }
                
                List<object[]> update = medMap_boxClasses.ClassToSQL<medMap_boxClass, enum_medMap_box>();
                await sQLControl_medMap_box.UpdateRowsAsync(null, update);

                returnData.Code = 200;
                returnData.Data = medMap_boxClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "update_medMap_box";
                returnData.Result = $"藥盒資料更新成功!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以Master_GUID取得藥盒資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["Master_GUID"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_box_by_Master_GUID")]
        public string get_medMap_box_by_Master_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\"]";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "medMap_box", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl.GetRowsByDefult(null, (int)enum_medMap_box.Master_GUID, Master_GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                List<medMap_boxClass> medMap_BoxClasses = objects.SQLToClass<medMap_boxClass, enum_medMap_box>();
                List<Task> tasks = new List<Task>();
                foreach (var item in medMap_BoxClasses)
                {
                    tasks.Add(Task.Run(new Action(delegate 
                    {
                        Storage storage = deviceApiClass.Get_EPD266_Storage_ByIP(API_server, item.serverName, item.serverType, item.藥盒IP);
                        if (storage != null) item.storage = storage;
                    })));
                }
                Task.WhenAll(tasks).Wait();

                returnData.Code = 200;
                returnData.Data = medMap_BoxClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_shelf_by_GUID";
                returnData.Result = $"取得藥盒資料成功，共{medMap_BoxClasses.Count}筆!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        
        private string CheckCreatTable()
        {
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData returnData = new returnData();
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }

            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_section()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_sub_section()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_shelf()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_drawer()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_box()));


            return tables.JsonSerializationt(true);
        }
        private async Task<string> get_medMap_by_GUID(string GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(GUID);
            return await get_medMap_by_GUID(returnData);
        }
        private async Task<returnData> get_medMap_section_by_Master_GUID(string Master_GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(Master_GUID);
            string result= await get_medMap_section_by_Master_GUID(returnData);
            return result.JsonDeserializet<returnData>();
        }
        private string get_medMap_sub_section_by_Master_GUID(string Master_GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(Master_GUID);
            return get_medMap_sub_section_by_Master_GUID(returnData);
        }
        private string get_medMap_shelf_by_Master_GUID(string Master_GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(Master_GUID);
            return get_medMap_shelf_by_Master_GUID(returnData);
        }
        private string get_medMap_drawer_by_Master_GUID(string Master_GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(Master_GUID);
            return get_medMap_drawer_by_Master_GUID(returnData);
        }
        private string get_medMap_box_by_Master_GUID(string Master_GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(Master_GUID);
            return get_medMap_box_by_Master_GUID(returnData);
        }
        private async Task<returnData> get_medMap_by_name_type(string serverName, string serverType)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(serverName);
            returnData.ValueAry.Add(serverType);
            string result = await get_medMap_by_name_type(returnData);
            return result.JsonDeserializet<returnData>();
        }
        private string Getcommand(SQLControl sQLControl, string colunnName, string value)
        {
            string db = sQLControl.Server;
            string tableName = sQLControl.TableName;
            string command = $"SELECT * FROM {db}.{tableName} WHERE {colunnName} = {value}";
            return command;
        }



    }
    
}
