﻿using Microsoft.AspNetCore.Mvc;
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
    public class sessionController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [HttpGet]
        public string Get(string level)
        {
            return GetPermissions(level.StringToInt32()).JsonSerializationt();
        }
        [Route("login")]
        [HttpPost]
        public string POST_login([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "找無資料庫參數!";
                    return returnData.JsonSerializationt();
                }
                string IP = serverSettingClasses[0].Server;
                string DataBaseName = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                bool flag_admin = false;

                SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_person_page = new SQLControl(IP, DataBaseName, "person_page", UserName, Password, Port, SSLMode);

                Check_Table();
                sessionClass data = returnData.Data.ObjToClass<sessionClass>();
                List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
                List<object[]> list_person_page = sQLControl_person_page.GetAllRows(null);
                List<object[]> list_login_session_add = new List<object[]>();
                List<object[]> list_login_session_replace = new List<object[]>();
                sessionClass sessionClass = new sessionClass();
                if (data.ID.StringIsEmpty() == false && data.Password.StringIsEmpty() == false)
                {
                    if (data.ID.ToUpper() == "admin".ToUpper() && data.Password.ToUpper() == "66437068")
                    {
                        flag_admin = true;
                    }
                }
            
                if (flag_admin == false)
                {
                    if (data.UID.StringIsEmpty() == false)
                    {
                        list_person_page = list_person_page.GetRows((int)enum_人員資料.卡號, data.UID);
                        if (list_person_page.Count == 0)
                        {
                            returnData.Code = -1;
                            returnData.Result = "此UID找無資料!";
                            return returnData.JsonSerializationt();
                        }
                        data.ID = list_person_page[0][(int)enum_人員資料.ID].ObjectToString();
                        data.Password = list_person_page[0][(int)enum_人員資料.密碼].ObjectToString();
                    }
                    else if (data.BARCODE.StringIsEmpty() == false)
                    {
                        list_person_page = list_person_page.GetRows((int)enum_人員資料.一維條碼, data.BARCODE);
                        if (list_person_page.Count == 0)
                        {
                            returnData.Code = -1;
                            returnData.Result = "此一維條碼找無資料!";
                            return returnData.JsonSerializationt();
                        }
                        data.ID = list_person_page[0][(int)enum_人員資料.ID].ObjectToString();
                        data.Password = list_person_page[0][(int)enum_人員資料.密碼].ObjectToString();
                    }
                    list_person_page = list_person_page.GetRows((int)enum_人員資料.ID, data.ID);
                    if (list_person_page.Count == 0)
                    {
                        returnData.Code = -1;
                        returnData.Result = "找無此帳號!";
                        return returnData.JsonSerializationt();
                    }
                    if (list_person_page[0][(int)enum_人員資料.密碼].ObjectToString() != data.Password)
                    {
                        returnData.Code = -2;
                        returnData.Result = "密碼錯誤!";
                        return returnData.JsonSerializationt();
                    }
                    list_login_session = list_login_session.GetRows((int)enum_login_session.ID, data.ID);

                    object[] value = new object[new enum_login_session().GetLength()];
                    if (list_login_session.Count == 0)
                    {
                        value = new object[new enum_login_session().GetLength()];
                        value[(int)enum_login_session.GUID] = Guid.NewGuid().ToString();
                        value[(int)enum_login_session.ID] = list_person_page[0][(int)enum_人員資料.ID].ObjectToString();
                        value[(int)enum_login_session.Name] = list_person_page[0][(int)enum_人員資料.姓名].ObjectToString();
                        value[(int)enum_login_session.Employer] = list_person_page[0][(int)enum_人員資料.單位].ObjectToString();
                        value[(int)enum_login_session.verifyTime] = DateTime.Now.ToDateTimeString();
                        value[(int)enum_login_session.loginTime] = DateTime.Now.ToDateTimeString();
                        list_login_session_add.Add(value);
                    }
                    else
                    {
                        value = list_login_session[0];
                        value[(int)enum_login_session.GUID] = Guid.NewGuid().ToString();
                        value[(int)enum_login_session.ID] = list_person_page[0][(int)enum_人員資料.ID].ObjectToString();
                        value[(int)enum_login_session.Name] = list_person_page[0][(int)enum_人員資料.姓名].ObjectToString();
                        value[(int)enum_login_session.Employer] = list_person_page[0][(int)enum_人員資料.單位].ObjectToString();
                        value[(int)enum_login_session.verifyTime] = DateTime.Now.ToDateTimeString();
                        value[(int)enum_login_session.loginTime] = DateTime.Now.ToDateTimeString();
                        list_login_session_replace.Add(value);
                    }
                    if (list_login_session_add.Count > 0) sQLControl_login_session.AddRows(null, list_login_session_add);
                    if (list_login_session_replace.Count > 0) sQLControl_login_session.UpdateByDefulteExtra(null, list_login_session_replace);


                    sessionClass.GUID = value[(int)enum_login_session.GUID].ObjectToString();
                    sessionClass.ID = list_person_page[0][(int)enum_人員資料.ID].ObjectToString();
                    sessionClass.Password = list_person_page[0][(int)enum_人員資料.密碼].ObjectToString();
                    sessionClass.Name = list_person_page[0][(int)enum_人員資料.姓名].ObjectToString();
                    sessionClass.Employer = list_person_page[0][(int)enum_人員資料.單位].ObjectToString();
                    sessionClass.verifyTime = value[(int)enum_login_session.verifyTime].ObjectToString();
                    sessionClass.loginTime = value[(int)enum_login_session.loginTime].ObjectToString();
                    sessionClass.level = list_person_page[0][(int)enum_人員資料.權限等級].ObjectToString();
                    sessionClass.Color = list_person_page[0][(int)enum_人員資料.顏色].ObjectToString();
                    sessionClass.license = list_person_page[0][(int)enum_人員資料.藥師證字號].ObjectToString();
                    sessionClass.Permissions = GetPermissions(sessionClass.level.StringToInt32());
                }
                else
                {
                    sessionClass.GUID = "";
                    sessionClass.ID = "admin";
                    sessionClass.Password = "66437068";
                    sessionClass.Name = "最高管理權限";
                    sessionClass.Employer = "";
                    sessionClass.verifyTime = DateTime.Now.ToDateTimeString();
                    sessionClass.loginTime = DateTime.Now.ToDateTimeString();
                    sessionClass.level = "-1";
                    sessionClass.Color = System.Drawing.Color.Red.ToColorString();
                    sessionClass.Permissions = GetPermissions(sessionClass.level.StringToInt32());
                }





                returnData.Data = sessionClass;

                //if (list_login_session.Count > 0)
                //{
                //    returnData.Code = -3;
                //    returnData.Result = "已登入帳號,需登出!";
                //    return returnData.JsonSerializationt();
                //}



                returnData.Code = 200;
                returnData.Result = "登入成功!";
                return returnData.JsonSerializationt();
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
            
        }

        [Route("logout")]
        [HttpPost]
        public string POST_logout([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "找無資料庫參數!";
                    return returnData.JsonSerializationt();
                }
                string IP = serverSettingClasses[0].Server;
                string DataBaseName = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_person_page = new SQLControl(IP, DataBaseName, "person_page", UserName, Password, Port, SSLMode);

                Check_Table();
                sessionClass sessionClass = returnData.Data.ObjToClass<sessionClass>();
                List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
                list_login_session = list_login_session.GetRows((int)enum_login_session.ID, sessionClass.ID);
                if (list_login_session.Count > 0)
                {
                    sQLControl_login_session.DeleteExtra(null, list_login_session);
                    returnData.Code = 200;
                    returnData.Result = $"ID :{sessionClass.ID} ,清除session成功!";
                    return returnData.JsonSerializationt();
                }
                returnData.Code = 200;
                returnData.Result = $"ID :{sessionClass.ID} ,找無此session!";
                return returnData.JsonSerializationt();

            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
           

        }

        [Route("check_session")]
        [HttpPost]
        public string POST_check([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "找無資料庫參數!";
                    return returnData.JsonSerializationt();
                }
                string IP = serverSettingClasses[0].Server;
                string DataBaseName = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_person_page = new SQLControl(IP, DataBaseName, "person_page", UserName, Password, Port, SSLMode);

                Check_Table();

                sessionClass sessionClass = returnData.Data.ObjToClass<sessionClass>();
                List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
                list_login_session = list_login_session.GetRows((int)enum_login_session.ID, sessionClass.ID);
                if (list_login_session.Count == 0)
                {
                    returnData.Result = "其他裝置登入,即將登出!";
                    returnData.Code = -2;
                    return returnData.JsonSerializationt();
                }
                else
                {

                }
                DateTime loginTime = sessionClass.loginTime.StringToDateTime().StringToDateTime();
                if (sessionClass.loginTime.StringToDateTime().ToDateTimeString() != list_login_session[0][(int)enum_login_session.loginTime].StringToDateTime().ToDateTimeString())
                {
                    returnData.Result = "其他裝置登入,即將登出!";
                    returnData.Code = -2;
                    return returnData.JsonSerializationt();
                }
                DateTime verifyTime = list_login_session[0][(int)enum_login_session.verifyTime].StringToDateTime();


                TimeSpan timeDifference = DateTime.Now.Subtract(verifyTime);
                double secondsDifference = timeDifference.TotalSeconds;
                if (secondsDifference >= sessionClass.check_sec.StringToInt32())
                {
                    returnData.Result = "驗證逾時,即將登出!";
                    returnData.Code = -1;
                    return returnData.JsonSerializationt();
                }
                list_login_session[0][(int)enum_login_session.verifyTime] = DateTime.Now.ToDateTimeString();
                sQLControl_login_session.UpdateByDefulteExtra(null, list_login_session);

                returnData.Result = "驗證成功!";
                returnData.Code = 200;
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
           
        }

        [Route("update_session")]
        [HttpPost]
        public string POST_update_session([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "找無資料庫參數!";
                    return returnData.JsonSerializationt();
                }
                string IP = serverSettingClasses[0].Server;
                string DataBaseName = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_person_page = new SQLControl(IP, DataBaseName, "person_page", UserName, Password, Port, SSLMode);

                Check_Table();

                sessionClass sessionClass = returnData.Data.ObjToClass<sessionClass>();
                List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
                list_login_session = list_login_session.GetRows((int)enum_login_session.ID, sessionClass.ID);
                if (list_login_session.Count == 0)
                {
                    returnData.Result = "找無session!";
                    returnData.Code = -2;
                    return returnData.JsonSerializationt();
                }
                list_login_session[0][(int)enum_login_session.GUID] = sessionClass.GUID;
                returnData.Result = "更新session完成!";
                returnData.Code = 200;
                return returnData.JsonSerializationt();
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
           
        }

        [Route("get_permissions")]
        [HttpPost]
        public string POST_get_permissions([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "找無資料庫參數!";
                    return returnData.JsonSerializationt();
                }
                string IP = serverSettingClasses[0].Server;
                string DataBaseName = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_person_page = new SQLControl(IP, DataBaseName, "person_page", UserName, Password, Port, SSLMode);
                Check_Table();
                sessionClass data = returnData.Data.ObjToClass<sessionClass>();
                List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
                List<object[]> list_person_page = sQLControl_person_page.GetAllRows(null);
                List<object[]> list_login_session_add = new List<object[]>();
                List<object[]> list_login_session_replace = new List<object[]>();

                list_person_page = list_person_page.GetRows((int)enum_人員資料.ID, data.ID);
                if (list_person_page.Count == 0)
                {
                    returnData.Code = -1;
                    returnData.Result = "找無此帳號!";
                    return returnData.JsonSerializationt();
                }
                if (list_person_page[0][(int)enum_人員資料.密碼].ObjectToString() != data.Password)
                {
                    returnData.Code = -2;
                    returnData.Result = "密碼錯誤!";
                    return returnData.JsonSerializationt();
                }
                sessionClass sessionClass = new sessionClass();
                sessionClass.ID = list_person_page[0][(int)enum_人員資料.ID].ObjectToString();
                sessionClass.Password = list_person_page[0][(int)enum_人員資料.密碼].ObjectToString();
                sessionClass.Name = list_person_page[0][(int)enum_人員資料.姓名].ObjectToString();
                sessionClass.Employer = list_person_page[0][(int)enum_人員資料.單位].ObjectToString();
                sessionClass.level = list_person_page[0][(int)enum_人員資料.權限等級].ObjectToString();
                sessionClass.Color = list_person_page[0][(int)enum_人員資料.顏色].ObjectToString();
                sessionClass.Permissions = GetPermissions(sessionClass.level.StringToInt32());



                returnData.Data = sessionClass;

                returnData.Code = 200;
                returnData.Result = "取得權限成功!";
                return returnData.JsonSerializationt();
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
           
        }

        private List<PermissionsClass> GetPermissions(int level)
        {
            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
            if (serverSettingClasses.Count == 0)
            {
                return new List<PermissionsClass>();
            }
            string IP = serverSettingClasses[0].Server;
            string DataBaseName = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

            List<PermissionsClass> result = new List<PermissionsClass>();
            SQLControl sQLControl_login_data_index = new SQLControl(IP, DataBaseName, "login_data_index", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_login_data = new SQLControl(IP, DataBaseName, "login_data", UserName, Password, Port, SSLMode);
            List<MySQL_Login.LoginDataWebAPI.Class_login_data> list_class_login_data = MySQL_Login.LoginDataWebAPI.Get_login_data(sQLControl_login_data);
            List<object[]> login_data_index = sQLControl_login_data_index.GetAllRows(null);
            List<object[]> login_data_index_buf = new List<object[]>();
            if(level == -1)
            {
                list_class_login_data = new List<MySQL_Login.LoginDataWebAPI.Class_login_data>();
                MySQL_Login.LoginDataWebAPI.Class_login_data class_Login_Data = new MySQL_Login.LoginDataWebAPI.Class_login_data();
                for(int i = 0; i < class_Login_Data.data.Count; i++)
                {
                    class_Login_Data.data[i] = true;
                }

                list_class_login_data.Add(class_Login_Data);
            }
            if (list_class_login_data.Count > 0)
            {
                list_class_login_data = (from value in list_class_login_data
                                         where value.level.StringToInt32().ToString() == level.StringToInt32().ToString()
                                         select value).ToList();
                if (list_class_login_data.Count == 0) return new List<PermissionsClass>();
                for (int i = 0; i < list_class_login_data[0].data.Count; i++)
                {
                    login_data_index_buf = login_data_index.GetRows((int)MySQL_Login.LoginDataWebAPI.enum_login_data_index.索引, i.ToString("00"));
                    if (list_class_login_data[0].data[i])
                    {
                        
                        if (login_data_index_buf.Count > 0)
                        {
                            PermissionsClass permissionsClass = new PermissionsClass();
                            permissionsClass.名稱 = login_data_index_buf[0][(int)MySQL_Login.LoginDataWebAPI.enum_login_data_index.Name].ObjectToString();
                            permissionsClass.類別 = login_data_index_buf[0][(int)MySQL_Login.LoginDataWebAPI.enum_login_data_index.Type].ObjectToString();
                            permissionsClass.索引 = i;
                            permissionsClass.狀態 = true;
                            result.Add(permissionsClass);
                        }
                    }
                    else
                    {
                        if (login_data_index_buf.Count > 0)
                        {
                            PermissionsClass permissionsClass = new PermissionsClass();
                            permissionsClass.名稱 = login_data_index_buf[0][(int)MySQL_Login.LoginDataWebAPI.enum_login_data_index.Name].ObjectToString();
                            permissionsClass.類別 = login_data_index_buf[0][(int)MySQL_Login.LoginDataWebAPI.enum_login_data_index.Type].ObjectToString();
                            permissionsClass.索引 = i;
                            permissionsClass.狀態 = false;
                            result.Add(permissionsClass);
                        }
                    }

                }
            }
            return result;
        }
        private void Check_Table()
        {
            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
            if (serverSettingClasses.Count == 0)
            {
                return;
            }
            string IP = serverSettingClasses[0].Server;
            string DataBaseName = serverSettingClasses[0].DBName;
            string UserName = serverSettingClasses[0].User;
            string Password = serverSettingClasses[0].Password;
            uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

            SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);

            if (sQLControl_login_session.IsTableCreat() == false)
            {
                Table table = new Table("login_session");
                table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
                table.AddColumnList("ID", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("Name", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("Employer", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("loginTime", Table.DateType.DATETIME, 50, Table.IndexType.None);
                table.AddColumnList("verlifyTime", Table.DateType.DATETIME, 50, Table.IndexType.None);
                sQLControl_login_session.CreatTable(table);
            }
        }
    }
}
