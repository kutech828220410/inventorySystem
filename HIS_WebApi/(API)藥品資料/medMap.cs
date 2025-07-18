﻿using Basic;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_藥品資料
{
    [Route("api/[controller]")]
    [ApiController]
    public class medMap : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();

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
        ///         "GUID":
        ///         "Master_GUID"
        ///         "department_type": "",
        ///         "name":"A7",
        ///         "type":"調劑台",
        ///         "position":"0,1"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["設備名稱","類別","位置"]
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
        public string add_medMap([FromBody] returnData returnData)
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
                if(returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"設備名稱\",\"類別\",\"位置\"]";
                    return returnData.JsonSerializationt();
                }
                string 設備名稱 = returnData.ValueAry[0];
                string 類別 = returnData.ValueAry[1];
                string 位置 = returnData .ValueAry[2];
                if(位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                sys_serverSettingClass sys_ServerSetting = sys_serverSettingClasses.myFind(設備名稱, 類別, "一般資料");
                if (sys_ServerSetting == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = sys_ServerSetting.GUID;

                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap.GetRowsByDefult(null, (int)enum_medMap.Master_GUID, Master_GUID);
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
                medMapClass.sys_ServerSettingClass = sys_ServerSetting;
                object[] add = medMapClass.ClassToSQL<medMapClass, enum_medMap>();
                sQLControl_medMap.AddRow(null, add);

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
        ///     {
        ///         "GUID":
        ///         "Master_GUID"
        ///         "department_type": "",
        ///         "name":"A7",
        ///         "type":"調劑台",
        ///         "position":"0,1"
        ///     },
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
        public string update_medMap([FromBody] returnData returnData)
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
                if(returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"GUID\",\"Master_GUID\",\"位置\"]";
                    return returnData.JsonSerializationt();
                }
                string GUID = returnData.ValueAry[0];
                string Master_GUID = returnData.ValueAry[1];
                string 位置 = returnData .ValueAry[2];
                if(位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap.GetRowsByDefult(null, (int)enum_medMap.GUID, GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMapClass medMapClass = objects.SQLToClass<medMapClass, enum_medMap>()[0];
                
                sys_serverSettingClass sys_ServerSetting = sys_serverSettingClasses.myFind(Master_GUID);
                if (sys_ServerSetting == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                medMapClass.位置 = 位置;
                medMapClass.sys_ServerSettingClass = sys_ServerSetting;

                object[] update = medMapClass.ClassToSQL<medMapClass, enum_medMap>();
                sQLControl_medMap.UpdateByDefulteExtra(null, update);

                returnData.Code = 200;
                returnData.Data = medMapClass;
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
        [HttpPost("get_medMap_by_name_type")]
        public string get_medMap_by_name_type([FromBody] returnData returnData)
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
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                sys_serverSettingClass sys_ServerSetting = sys_serverSettingClasses.myFind(設備名稱, 類別, "一般資料");
                if (sys_ServerSetting == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Master_GUID = sys_ServerSetting.GUID;

                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap.GetRowsByDefult(null, (int)enum_medMap.Master_GUID, Master_GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMapClass medMapClasses = objects.SQLToClass<medMapClass, enum_medMap>()[0];
                medMapClasses.sys_ServerSettingClass = sys_ServerSetting;

                returnData.Code = 200;
                returnData.Data = medMapClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_by_name_type";
                returnData.Result = $"取得父容器資料成功!";
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
        public string get_medMap_by_GUID([FromBody] returnData returnData)
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
                SQLControl sQLControl_medMap = new SQLControl(Server, DB, "medMap", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap.GetRowsByDefult(null, (int)enum_medMap.GUID, GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMapClass medMapClasses = objects.SQLToClass<medMapClass, enum_medMap>()[0];
                
                sys_serverSettingClass sys_ServerSetting = sys_serverSettingClasses.myFind(medMapClasses.Master_GUID);
                if (sys_ServerSetting == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }

                string jsonString = get_medMap_section_by_Master_GUID(GUID);
                returnData returnData_get_medMap_section_by_Master_GUID = jsonString.JsonDeserializet<returnData>();
                if(returnData_get_medMap_section_by_Master_GUID.Code != 200)
                {
                    return returnData_get_medMap_section_by_Master_GUID.JsonSerializationt(true);
                }
                List<medMap_sectionClass> medMap_SectionClasses = returnData_get_medMap_section_by_Master_GUID.Data.ObjToClass<List<medMap_sectionClass>>();

                medMapClasses.sys_ServerSettingClass = sys_ServerSetting;
                medMapClasses.medMap_SectionClasses = medMap_SectionClasses;

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
        ///     "Value": "",
        ///     "ValueAry":["子容器GUID","子容器Master_GUID","位置"]
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
        public string update_medMap_section([FromBody] returnData returnData)
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
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"子容器GUID\",\"子容器Master_GUID\",\"位置\"]";
                    return returnData.JsonSerializationt();
                }
                string GUID = returnData.ValueAry[0];
                string Master_GUID = returnData.ValueAry[1];
                string 位置 = returnData.ValueAry[2];
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
                if (medMap_sectionClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                List<medMap_sectionClass> medMap_sectionClass_buff = medMap_sectionClasses.Where(item => item.位置 == 位置).ToList();
                if (medMap_sectionClass_buff.Count > 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置已重複!";
                    return returnData.JsonSerializationt();
                }
                medMap_sectionClass medMap_sectionClass = medMap_sectionClasses.Where(item => item.GUID == GUID).FirstOrDefault();
                if (medMap_sectionClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMap_sectionClass.位置 = 位置;

     
                object[] update = medMap_sectionClass.ClassToSQL<medMap_sectionClass, enum_medMap_section>();
                sQLControl_medMap_section.UpdateByDefulteExtra(null, update);

                returnData.Code = 200;
                returnData.Data = medMap_sectionClass;
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
        /// 以GUID取得子容器資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Value": "",
        ///     "ValueAry":["子容器GUID"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medMap_section_by_GUID")]
        public string get_medMap_section_by_GUID([FromBody] returnData returnData)
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
                SQLControl sQLControl_medMap_section = new SQLControl(Server, DB, "medMap_section", UserName, Password, Port, SSLMode);
                
                List<object[]> objects = sQLControl_medMap_section.GetRowsByDefult(null, (int)enum_medMap_section.GUID, GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMap_sectionClass medMap_sectionClass = objects.SQLToClass<medMap_sectionClass, enum_medMap_section>()[0];

                string jsonString = get_medMap_shelf_by_Master_GUID(GUID);
                returnData returnData_get_medMap_shelf_by_Master_GUID = jsonString.JsonDeserializet<returnData>();
                if (returnData_get_medMap_shelf_by_Master_GUID.Code != 200) return returnData_get_medMap_shelf_by_Master_GUID.JsonSerializationt(true);
                List<medMap_shelfClass> medMap_ShelfClasses = returnData_get_medMap_shelf_by_Master_GUID.Data.ObjToClass<List<medMap_shelfClass>>();

                jsonString = get_medMap_drawer_by_Master_GUID(GUID);
                returnData returnData_get_medMap_drawer_by_Master_GUID = jsonString.JsonDeserializet<returnData>();
                if (returnData_get_medMap_drawer_by_Master_GUID.Code != 200) return returnData_get_medMap_drawer_by_Master_GUID.JsonSerializationt(true);
                List<medMap_drawerClass> medMap_drawerClasses = returnData_get_medMap_drawer_by_Master_GUID.Data.ObjToClass<List<medMap_drawerClass>>();
                medMap_sectionClass.medMap_ShelfClasses = medMap_ShelfClasses;
                medMap_sectionClass.medMap_drawerClasses = medMap_drawerClasses;


                returnData.Code = 200;
                returnData.Data = medMap_sectionClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_medMap_section_by_GUID";
                returnData.Result = $"子容器資料取得成功!";
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
        public string get_medMap_section_by_Master_GUID([FromBody] returnData returnData)
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
                if (medMap_ShelfClass.位置.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料為空，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                if (medMap_ShelfClass.位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
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
        /// 更新層架資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     {
        ///         "GUID":"",
        ///         "Master_GUID":"子容器GUID",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_light":"燈條IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["子容器GUID","子容器Master_GUID","位置"]
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
        public string update_medMap_shelf([FromBody] returnData returnData)
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
                if (medMap_ShelfClass.Master_GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"Master_GUID資料為空";
                    return returnData.JsonSerializationt();
                }
                if (medMap_ShelfClass.位置.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料為空，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
                if (medMap_ShelfClass.位置.Split(",").Count() != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置資料錯誤，須為 \"0,1\"";
                    return returnData.JsonSerializationt();
                }
             
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_shelf = new SQLControl(Server, DB, "medMap_shelf", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap_shelf.GetRowsByDefult(null, (int)enum_medMap_shelf.Master_GUID, medMap_ShelfClass.Master_GUID);
                List<medMap_shelfClass> medMap_shelfClasses = objects.SQLToClass<medMap_shelfClass, enum_medMap_shelf>();
                if (medMap_shelfClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                List<medMap_shelfClass> medMap_sectionClass_buff = medMap_shelfClasses.Where(item => item.位置 == medMap_ShelfClass.位置).ToList();
                if (medMap_sectionClass_buff.Count > 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置已重複!";
                    return returnData.JsonSerializationt();
                }
                medMap_shelfClass medMap_shelfClass_sql = medMap_shelfClasses.Where(item => item.GUID == medMap_ShelfClass.GUID).FirstOrDefault();
                if (medMap_shelfClass_sql == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMap_shelfClass_sql.位置 = medMap_ShelfClass.位置;
                if (medMap_ShelfClass.寬度.StringIsEmpty() == false) medMap_shelfClass_sql.寬度 = medMap_ShelfClass.寬度;
                if (medMap_ShelfClass.高度.StringIsEmpty() == false) medMap_shelfClass_sql.高度 = medMap_ShelfClass.高度;
                if (medMap_ShelfClass.燈條IP.StringIsEmpty() == false) medMap_shelfClass_sql.燈條IP = medMap_ShelfClass.燈條IP;



                object[] update = medMap_shelfClass_sql.ClassToSQL<medMap_shelfClass, enum_medMap_shelf>();
                sQLControl_medMap_shelf.UpdateByDefulteExtra(null, update);

                returnData.Code = 200;
                returnData.Data = medMap_shelfClass_sql;
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
                medMap_shelfClass.medMap_BoxClasses = medMap_boxClassses;



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
        /// 更新抽屜資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data":
        ///     {
        ///         "GUID":"",
        ///         "Master_GUID":"子容器GUID",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_light":"抽屜IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["子容器GUID","子容器Master_GUID","位置"]
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
        public string update_medMap_drawer([FromBody] returnData returnData)
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
                if (medMap_drawerClass.Master_GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"Master_GUID資料為空";
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

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_drawer = new SQLControl(Server, DB, "medMap_drawer", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap_drawer.GetRowsByDefult(null, (int)enum_medMap_drawer.Master_GUID, medMap_drawerClass.Master_GUID);
                List<medMap_drawerClass> medMap_drawerClasses = objects.SQLToClass<medMap_drawerClass, enum_medMap_drawer>();
                if (medMap_drawerClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                List<medMap_drawerClass> medMap_drawer_buff = medMap_drawerClasses.Where(item => item.位置 == medMap_drawerClass.位置).ToList();
                if (medMap_drawer_buff.Count > 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置已重複!";
                    return returnData.JsonSerializationt();
                }
                medMap_drawerClass medMap_drawerClass_sql = medMap_drawerClasses.Where(item => item.GUID == medMap_drawerClass.GUID).FirstOrDefault();
                if (medMap_drawerClass_sql == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMap_drawerClass_sql.位置 = medMap_drawerClass.位置;
                if (medMap_drawerClass.寬度.StringIsEmpty() == false) medMap_drawerClass_sql.寬度 = medMap_drawerClass.寬度;
                if (medMap_drawerClass.高度.StringIsEmpty() == false) medMap_drawerClass_sql.高度 = medMap_drawerClass.高度;
                if (medMap_drawerClass.抽屜IP.StringIsEmpty() == false) medMap_drawerClass_sql.抽屜IP = medMap_drawerClass.抽屜IP;

                object[] update = medMap_drawerClass_sql.ClassToSQL<medMap_drawerClass, enum_medMap_drawer>();
                sQLControl_medMap_drawer.UpdateByDefulteExtra(null, update);

                returnData.Code = 200;
                returnData.Data = medMap_drawerClass_sql;
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
        ///     {
        ///         "GUID":"",
        ///         "Master_GUID":"子容器GUID",
        ///         "position":"位置",
        ///         "width":"寬度",
        ///         "height":"高度",
        ///         "ip_light":"抽屜IP"
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["子容器GUID","子容器Master_GUID","位置"]
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
        public string update_medMap_box([FromBody] returnData returnData)
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
                if (medMap_boxClass.Master_GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"Master_GUID資料為空";
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

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_medMap_box = new SQLControl(Server, DB, "medMap_box", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl_medMap_box.GetRowsByDefult(null, (int)enum_medMap_box.Master_GUID, medMap_boxClass.Master_GUID);
                List<medMap_boxClass> medMap_boxClasses = objects.SQLToClass<medMap_boxClass, enum_medMap_box>();
                if (medMap_boxClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                List<medMap_boxClass> medMap_boxClass_buff = medMap_boxClasses.Where(item => item.位置 == medMap_boxClass.位置).ToList();
                if (medMap_boxClass_buff.Count > 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"位置已重複!";
                    return returnData.JsonSerializationt();
                }
                medMap_boxClass medMap_drawerClass_sql = medMap_boxClasses.Where(item => item.GUID == medMap_boxClass.GUID).FirstOrDefault();
                if (medMap_drawerClass_sql == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料不存在!";
                    return returnData.JsonSerializationt();
                }
                medMap_drawerClass_sql.位置 = medMap_boxClass.位置;
                if (medMap_boxClass.寬度.StringIsEmpty() == false) medMap_drawerClass_sql.寬度 = medMap_boxClass.寬度;
                if (medMap_boxClass.高度.StringIsEmpty() == false) medMap_drawerClass_sql.高度 = medMap_boxClass.高度;
                if (medMap_boxClass.藥盒IP.StringIsEmpty() == false) medMap_drawerClass_sql.藥盒IP = medMap_boxClass.藥盒IP;

                object[] update = medMap_drawerClass_sql.ClassToSQL<medMap_boxClass, enum_medMap_box>();
                sQLControl_medMap_box.UpdateByDefulteExtra(null, update);

                returnData.Code = 200;
                returnData.Data = medMap_drawerClass_sql;
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
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_shelf()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_drawer()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_medMap_box()));


            return tables.JsonSerializationt(true);
        }
        private string get_medMap_by_GUID(string GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(GUID);
            return get_medMap_by_GUID(returnData);
        }
        private string get_medMap_section_by_Master_GUID(string Master_GUID)
        {
            returnData returnData = new returnData();
            returnData.ValueAry.Add(Master_GUID);
            return get_medMap_section_by_Master_GUID(returnData);
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

    }
    
}
