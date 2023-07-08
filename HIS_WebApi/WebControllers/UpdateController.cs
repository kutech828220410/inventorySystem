using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
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
using Ionic.Zip;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : Controller
    {
        static private string API_Server = ConfigurationManager.AppSettings["API_Server"];
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        static private string ServerName = "update";
        static private string ServerType = "更新資訊";


        [Route("init")]
        [HttpGet]
        public string GET_init()
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(ServerName, ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [Route("")]
        [HttpGet]
        public string GET()
        {
            returnData returnData = new returnData();
            try
            {
                GET_init();
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(ServerName, ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Method = "all";
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                List<updateVersionClass> updateVersionClasses = GetAllUpdateVersion(serverSettingClasses[0]);
                returnData.Code = 200;
                returnData.Data = updateVersionClasses;
                returnData.Method = "all";
                returnData.Result = $"取得 update version 資訊成功! 共<{updateVersionClasses.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Method = "all";
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            try
            {
                GET_init();
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(ServerName, ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Method = "add";
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt(true);
                }
                List<updateVersionClass> updateVersionClasses = GetAllUpdateVersion(serverSettingClasses[0]);
                List<updateVersionClass> updateVersionClasses_input = returnData.Data.ObjToListClass<updateVersionClass>();
                if(updateVersionClasses_input == null || updateVersionClasses_input.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Method = "add";
                    returnData.Result = $"傳入資料資訊錯誤!";
                    return returnData.JsonSerializationt(true);
                }
                List<object[]> list_value = updateVersionClasses.ClassToSQL<updateVersionClass, enum_updateVersion>();
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                List<object[]> list_value_input = updateVersionClasses_input.ClassToSQL<updateVersionClass, enum_updateVersion>();
                for (int i = 0; i < list_value_input.Count; i++)
                {
                    object[] value = list_value_input[i];
                    string program_name = value[(int)enum_updateVersion.program_name].ObjectToString();
                    list_value_buf = list_value.GetRows((int)enum_updateVersion.program_name, program_name);
                    if(list_value_buf.Count == 0)
                    {
                        value[(int)enum_updateVersion.GUID] = Guid.NewGuid().ToString();
                        list_value_add.Add(value);
                    }
                    else
                    {
                        value[(int)enum_updateVersion.GUID] = list_value_buf[0][(int)enum_updateVersion.GUID];
                        list_value_replace.Add(value);
                    }
                    value[(int)enum_updateVersion.update_time] = DateTime.Now.ToDateTimeString();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl = new SQLControl(Server, DB, "update_version", UserName, Password, Port, SSLMode);
                if (list_value_add.Count > 0) sQLControl.AddRows(null, list_value_add);
                if (list_value_replace.Count > 0) sQLControl.UpdateByDefulteExtra(null, list_value_replace);
                returnData.Code = 200;
                returnData.Data = updateVersionClasses_input;
                returnData.Method = "all";
                returnData.Result = $"新增 update version 資訊成功! 新增<{list_value_add.Count}>筆,修改<{list_value_replace.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Method = "add";
                returnData.Result = e.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [Route("download/{value}")]
        [HttpGet]
        public async Task<ActionResult> GET_download(string value)
        {
            MemoryStream outputStream = new MemoryStream();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(ServerName, ServerType, "一般資料");
                if (serverSettingClasses.Count == 0) return null;
                List<updateVersionClass> updateVersionClasses = GetAllUpdateVersion(serverSettingClasses[0]);
                updateVersionClasses = (from temp in updateVersionClasses
                                        where temp.program_name == value
                                        select temp).ToList();
                if (updateVersionClasses.Count == 0) return null;
                string filepath = updateVersionClasses[0].filepath;

                using (ZipFile zip = new ZipFile(System.Text.Encoding.GetEncoding("big5")))
                {
                    string folderPath = Path.GetDirectoryName(filepath);
                    zip.AddDirectory(folderPath);
                    zip.Save(outputStream);
                }
                outputStream.Seek(0, SeekOrigin.Begin); // 将内存流的位置重置为开头

                return File(outputStream, "application/zip", "download.zip");
            }
            catch
            {
                return null;
            }
            finally
            {
                //outputStream.Dispose(); // 释放 outputStream 资源
            }
        }
        [Route("version/{value}")]
        [HttpGet]
        public string GET_version(string value)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(ServerName, ServerType, "一般資料");
                if (serverSettingClasses.Count == 0) return null;
                List<updateVersionClass> updateVersionClasses = GetAllUpdateVersion(serverSettingClasses[0]);
                updateVersionClasses = (from temp in updateVersionClasses
                                        where temp.program_name == value
                                        select temp).ToList();
                if (updateVersionClasses.Count == 0) return "";
                string filepath = updateVersionClasses[0].filepath;
                Assembly assembly = Assembly.LoadFrom(filepath);
                string version = assembly.GetName().Version.ToString();

                return version;
            }
            catch
            {
                return "";
            }

        }

        [Route("extension/{value}")]
        [HttpGet]
        public string GET_extension(string value)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(ServerName, ServerType, "一般資料");
                if (serverSettingClasses.Count == 0) return null;
                List<updateVersionClass> updateVersionClasses = GetAllUpdateVersion(serverSettingClasses[0]);
                updateVersionClasses = (from temp in updateVersionClasses
                                        where temp.program_name == value
                                        select temp).ToList();
                if (updateVersionClasses.Count == 0) return "";
                string filepath = updateVersionClasses[0].filepath;
                return Path.GetExtension(filepath); 
            }
            catch
            {
                return "";
            }

        }
        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "update_version", UserName, Password, Port, SSLMode);

            Table table = new Table("update_version");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("program_name", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("version", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("filepath", Table.StringType.VARCHAR, 500, Table.IndexType.None);
            table.AddColumnList("update_time", Table.DateType.DATETIME, 50, Table.IndexType.None);
            table.AddColumnList("enable", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
            else sQLControl.CheckAllColumnName(table, true);
            return table.JsonSerializationt(true);
        }
        private List<updateVersionClass> GetAllUpdateVersion(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "update_version", UserName, Password, Port, SSLMode);
            List<object[]> list_value = sQLControl.GetAllRows(null);
            List<updateVersionClass> updateVersionClasses = list_value.SQLToClass<updateVersionClass, enum_updateVersion>();
            return updateVersionClasses;
        }

        private void GetZip(string folderPath)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(folderPath, Path.GetFileName(folderPath));
                    zip.Save(outputStream);
                }
            }
        }
    }
}
