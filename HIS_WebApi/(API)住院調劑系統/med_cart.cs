﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using MySql.Data.MySqlClient;
using SQLUI;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class med_cart : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [HttpPost("init")]
        public string POST_init([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0]);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_藥車資訊());
            return table.JsonSerializationt(true);
        }
        [HttpPost("update_bed_list")]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_bed_list";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                Table table = new Table(new enum_藥車資訊());
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, table.TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_medCart = sQLControl_med_carInfo.GetAllRows(null);

                List<medCarInfoClass> medCart_sql = list_medCart.SQLToClass<medCarInfoClass, enum_藥車資訊>();
                List<medCarInfoClass> medCart_sql_buf = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_add = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_replace = new List<medCarInfoClass>();
                List<medCarInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<medCarInfoClass>>();

                if (input_medCarInfo == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                for (int i = 0; i < input_medCarInfo.Count; i++)
                {
                    string 住院藥局 = input_medCarInfo[i].住院藥局;
                    string 護理站 = input_medCarInfo[i].護理站;
                    string 床號 = input_medCarInfo[i].床號;
                    string 病歷號 = input_medCarInfo[i].病歷號;
                    medCart_sql_buf = (from temp in medCart_sql
                                       where temp.住院藥局 == 住院藥局
                                       where temp.護理站 == 護理站
                                       where temp.床號 == 床號
                                       select temp).ToList();
                    if (medCart_sql_buf.Count == 0)
                    {
                        string GUID = Guid.NewGuid().ToString();
                        medCarInfoClass medCarInfoClass = input_medCarInfo[i];
                        medCarInfoClass.GUID = GUID;
                        medCart_sql_add.Add(medCarInfoClass);
                    }
                    else
                    {
                        medCarInfoClass sql_medCart = medCart_sql_buf[0];
                        if (sql_medCart.病歷號 != 病歷號)
                        {

                            medCarInfoClass medCarInfoClass = input_medCarInfo[i];

                            medCart_sql_replace.Add(input_medCarInfo[i]);
                        }

                    }
                }
                List<object[]> list_medCart_add = new List<object[]>();
                List<object[]> list_medCart_repalce = new List<object[]>();

                list_medCart_add = medCart_sql_add.ClassToSQL<medCarInfoClass, enum_藥車資訊>();
                list_medCart_repalce = medCart_sql_replace.ClassToSQL<medCarInfoClass, enum_藥車資訊>();

                if (list_medCart_add.Count > 0) sQLControl_med_carInfo.AddRows(null, list_medCart_add);
                if (list_medCart_repalce.Count > 0) sQLControl_med_carInfo.UpdateByDefulteExtra(null, list_medCart_repalce);
                string 占床狀態 = "已占床";
                List<object[]> list_bedList = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_藥車資訊.占床狀態, 占床狀態);
                List<medCarInfoClass> bedList = list_bedList.ObjToClass<List<medCarInfoClass>>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = "";
                returnData.Result = $"病床清單共{bedList}筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }

        [HttpPost("get_all")]
        public string POST_get_all([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_all";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = _serverSettingClasses[0].Server;
                string DB = _serverSettingClasses[0].DBName;
                string UserName = _serverSettingClasses[0].User;
                string Password = _serverSettingClasses[0].Password;
                uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "med_carInfo";
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetAllRows(null);

                List<medCarInfoClass> medCarInfoClasses = list_med_carInfo.SQLToClass<medCarInfoClass, enum_藥車資訊>();
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"取得藥車資訊共{medCarInfoClasses.Count}筆";
                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt();
            }
        }

        [HttpPost("delete_by_GUID")]
        public string delete_by_guid([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "delete_by_guid";
            try
            {

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind("Main", "一般資料", "網頁");
                if (_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[GUID]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];

                string Server = _serverSettingClasses[0].Server;
                string DB = _serverSettingClasses[0].DBName;
                string UserName = _serverSettingClasses[0].User;
                string Password = _serverSettingClasses[0].Password;
                uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                string TableName = "med_carInfo";
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_藥車資訊.GUID, GUID);
                if (list_value.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }

                sQLControl_med_carInfo.DeleteExtra(null, list_value);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"刪除藥品設定成功,共刪除<{list_value.Count}>筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }

    }

}
