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
    public class med_interaction : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        ///初始化dbvm.med_Inter資料庫
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
        [HttpPost("init_med_Inter")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "medInterClass物件", typeof(medInterClass))]
        public string init_med_Inter([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
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
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_med_inter());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("init_med_inter_dtl")]
        public string init_med_inter_dtl([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
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
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_med_inter_dtl());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass, Enum enumInstance)
        {
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, enumInstance);
            return table.JsonSerializationt(true);
        }
        [HttpPost("get_med_inter")]
        public string get_med_inter([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if(sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_med_inter = new SQLControl(Server, DB, "med_inter", UserName, Password, Port, SSLMode);
                List<object[]> list_med_inter = sQLControl_med_inter.GetAllRows(null);
                List<medInterClass> sql_medInter = list_med_inter.SQLToClass<medInterClass, enum_med_inter>();


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medInter;
                returnData.Result = $"取得Table {DB}.med_inter";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("get_med_inter_dtl")]
        public string get_med_inter_dtl([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_med_inter_dtl = new SQLControl(Server, DB, "med_inter_dtl", UserName, Password, Port, SSLMode);
                List<object[]> list_med_inter_dtl = sQLControl_med_inter_dtl.GetAllRows(null);
                List<medInterDtlClass> sql_medInter = list_med_inter_dtl.SQLToClass<medInterDtlClass, enum_med_inter_dtl>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medInter;
                returnData.Result = $"取得Table {DB}.med_inter";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("update_med_inter")]
        public string update_med_inter ([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_med_inter = new SQLControl(Server, DB, "med_inter", UserName, Password, Port, SSLMode);
                List<object[]> list_med_inter_sql = sQLControl_med_inter.GetAllRows(null);
                List<medInterClass> current_medInterClass = list_med_inter_sql.SQLToClass <medInterClass, enum_med_inter> ();
                List<medInterClass> add_medInterClass = new List<medInterClass>();
                List<medInterClass> update_medInterClass = new List<medInterClass>();


                List<medInterClass> input_medInterClass = returnData.Data.ObjToClass<List<medInterClass>>();
                if (input_medInterClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                foreach(var medInterClass in input_medInterClass)
                {
                    List<medInterClass> target_medInterClass = current_medInterClass.Where(temp => temp.ATC == medInterClass.ATC).ToList();
                    if(target_medInterClass.Count == 0)
                    {
                        medInterClass.GUID = Guid.NewGuid().ToString();
                        add_medInterClass.Add(medInterClass);
                    }
                    else
                    {
                        medInterClass.GUID = target_medInterClass[0].GUID;
                        update_medInterClass.Add(medInterClass);
                    }
                }

                List<object[]> add_med_inter = add_medInterClass.ClassToSQL<medInterClass, enum_med_inter>();
                List<object[]> update_med_inter = update_medInterClass.ClassToSQL<medInterClass, enum_med_inter>();
                if (add_med_inter.Count > 0) sQLControl_med_inter.AddRows(null, add_med_inter);
                if (update_med_inter.Count > 0) sQLControl_med_inter.UpdateByDefulteExtra(null, update_med_inter);


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = current_medInterClass;
                returnData.Result = $"更新Table {DB}.med_inter 共{input_medInterClass.Count}筆";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("update_med_inter_dtl")]
        public string updatemed_inter_dtl([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_med_inter_dtl = new SQLControl(Server, DB, "med_inter_dtl", UserName, Password, Port, SSLMode);
                List<object[]> list_med_inter_dtl_sql = sQLControl_med_inter_dtl.GetAllRows(null);
                List<medInterDtlClass> current_medInterDtlClass = list_med_inter_dtl_sql.SQLToClass<medInterDtlClass, enum_med_inter_dtl>();
                List<medInterDtlClass> add_medInterDtlClass = new List<medInterDtlClass>();
                List<medInterDtlClass> update_medInterDtlClass = new List<medInterDtlClass>();


                List<medInterDtlClass> input_medInterDtlClass = returnData.Data.ObjToClass<List<medInterDtlClass>>();
                if (input_medInterDtlClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                foreach (var medInterDtlClass in input_medInterDtlClass)
                {
                    List<medInterDtlClass> target_medInterClass = current_medInterDtlClass
                        .Where(temp => temp.ATC1 == medInterDtlClass.ATC1 && temp.ATC2 == medInterDtlClass.ATC2).ToList();
                    if (target_medInterClass.Count == 0)
                    {
                        medInterDtlClass.GUID = Guid.NewGuid().ToString();
                        add_medInterDtlClass.Add(medInterDtlClass);
                    }
                    else
                    {
                        medInterDtlClass.GUID = target_medInterClass[0].GUID;
                        update_medInterDtlClass.Add(medInterDtlClass);
                    }
                }

                List<object[]> add_med_inter_dtl = add_medInterDtlClass.ClassToSQL<medInterDtlClass, enum_med_inter_dtl>();
                List<object[]> update_med_inter_dtl = update_medInterDtlClass.ClassToSQL<medInterDtlClass, enum_med_inter_dtl>();
                if (add_med_inter_dtl.Count > 0) sQLControl_med_inter_dtl.AddRows(null, add_med_inter_dtl);
                if (update_med_inter_dtl.Count > 0) sQLControl_med_inter_dtl.UpdateByDefulteExtra(null, update_med_inter_dtl);


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = input_medInterDtlClass;
                returnData.Result = $"更新Table {DB}.med_inter 共{input_medInterDtlClass.Count}筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }


    }
}
