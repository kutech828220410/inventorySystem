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
    public class computeConfigController : ControllerBase
    {

        static private string API_Server = ConfigurationManager.AppSettings["API_Server"];
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("init")]
        [HttpGet]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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
        [Route("add_device")]
        [HttpPost]
        public string POST_add_device([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Method = "post_add_device";
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                computerConfigClass datas_input = returnData.Data.ObjToClass<computerConfigClass>();
                if (datas_input == null)
                {
                    returnData.Code = -200;
                    returnData.Method = "post_add_device";
                    returnData.Result = $"傳入資料資訊錯誤!";
                    return returnData.JsonSerializationt(true);
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string result = "";
                if(SetValue(serverSettingClass , datas_input , ref result) == false)
                {
                    returnData.Code = -200;
                    returnData.Method = "post_add_device";
                    returnData.Result = $"設定device參數失敗!";
                    return returnData.JsonSerializationt(true);
                }
             
             
                returnData.Code = 200;
                returnData.Data = datas_input;
                returnData.Method = "post_add_device";
                returnData.Result = $"{result}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Method = "post_add_device";
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
        }

        [Route("get_parameter")]
        [HttpPost]
        public string POST_parameter([FromBody] returnData returnData)
        {
            try
            {
                GET_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"get_parameter";
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string deviceName = returnData.Value;

                computerConfigClass computerConfigClass = GetValue(serverSettingClass, deviceName);
                if(computerConfigClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"get_parameter";
                    returnData.Result = $"取得參數錯誤!";
                    return returnData.JsonSerializationt();
                }
                returnData.Code = 200;
                returnData.Data = computerConfigClass;
                returnData.Method = "get_parameter";
                returnData.Result = $"";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"get_parameter";
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
        }
        private computerConfigClass GetValue(ServerSettingClass serverSettingClass, string deviceName)
        {
            computerConfigClass computerConfigClass = new computerConfigClass();
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            try
            {
                SQLControl sQLControl_devicelist = new SQLControl(Server, DB, "devicelist", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_sub_devicelist = new SQLControl(Server, DB, "sub_devicelist", UserName, Password, Port, SSLMode);
                List<object[]> list_devicelist = sQLControl_devicelist.GetRowsByDefult(null, (int)enum_computerConfig.名稱, deviceName);
                if (list_devicelist.Count == 0)
                {
                    string result = "";
                    computerConfigClass.名稱 = deviceName;
                    SetValue(serverSettingClass, computerConfigClass, ref result);
                    return computerConfigClass;
                }
                computerConfigClass = list_devicelist[0].SQLToClass<computerConfigClass, enum_computerConfig>();
                string Master_GUID = computerConfigClass.GUID;
                List<object[]> list_sub_devicelist = sQLControl_sub_devicelist.GetRowsByDefult(null, (int)enum_sub_computerConfig.Master_GUID, Master_GUID);
                List<sub_computerConfigClass> parameters = list_sub_devicelist.SQLToClass<sub_computerConfigClass, enum_sub_computerConfig>();
                computerConfigClass.Parameters = parameters;
                return computerConfigClass;
            }
            catch
            {
                return null;
            }
      
        }
        private bool SetValue(ServerSettingClass serverSettingClass, computerConfigClass data_input, ref string result)
        {
            List<computerConfigClass> datas_input = new List<computerConfigClass>();
            datas_input.Add(data_input);
            return SetValue(serverSettingClass, datas_input, ref result);
        }
        private bool SetValue(ServerSettingClass serverSettingClass, List<computerConfigClass> datas_input , ref string result)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl_devicelist = new SQLControl(Server, DB, "devicelist", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_sub_devicelist = new SQLControl(Server, DB, "sub_devicelist", UserName, Password, Port, SSLMode);
            List<object[]> list_devicelist_add = new List<object[]>();
            List<object[]> list_devicelist_replace = new List<object[]>();
            List<object[]> list_sub_devicelist_add = new List<object[]>();
            List<object[]> list_sub_devicelist_replace = new List<object[]>();
            try
            {
                for (int i = 0; i < datas_input.Count; i++)
                {
                    string Master_GUID = datas_input[i].GUID;
                    string deviceName = datas_input[i].名稱;
                    List<object[]> list_devicelist = sQLControl_devicelist.GetRowsByDefult(null, (int)enum_computerConfig.名稱, deviceName);
                    List<object[]> list_sub_devicelist = sQLControl_sub_devicelist.GetRowsByDefult(null, (int)enum_sub_computerConfig.Master_GUID, Master_GUID);
                    List<object[]> list_sub_devicelist_buf = new List<object[]>();
                    if (list_devicelist.Count == 0)
                    {
                        computerConfigClass computerConfigClass = datas_input[i];
                        computerConfigClass.GUID = Guid.NewGuid().ToString();
                        computerConfigClass.顏色 = System.Drawing.Color.Red.ToColorString();
                        list_devicelist_add.Add(computerConfigClass.ClassToSQL<computerConfigClass, enum_computerConfig>());
                    }
                    else
                    {
                        computerConfigClass computerConfigClass = datas_input[i];
                        list_devicelist_replace.Add(computerConfigClass.ClassToSQL<computerConfigClass, enum_computerConfig>());
                    }
                    for (int k = 0; k < datas_input[i].Parameters.Count; k++)
                    {
                        object[] value = datas_input[i].Parameters[k].ClassToSQL<sub_computerConfigClass, enum_sub_computerConfig>();
                        list_sub_devicelist_buf = list_sub_devicelist.GetRows((int)enum_sub_computerConfig.name, datas_input[i].Parameters[k].name);
                        list_sub_devicelist_buf = list_sub_devicelist_buf.GetRows((int)enum_sub_computerConfig.type, datas_input[i].Parameters[k].type);
                        if (list_sub_devicelist_buf.Count == 0)
                        {
                            value[(int)enum_sub_computerConfig.GUID] = Guid.NewGuid().ToString();
                            value[(int)enum_sub_computerConfig.Master_GUID] = Master_GUID;
                            list_sub_devicelist_add.Add(value);
                        }
                        else
                        {
                            list_sub_devicelist_replace.Add(value);
                        }
                    }
                }
                if (list_devicelist_add.Count > 0) sQLControl_devicelist.AddRows(null, list_devicelist_add);
                if (list_devicelist_replace.Count > 0) sQLControl_devicelist.UpdateByDefulteExtra(null, list_devicelist_replace);
                if (list_sub_devicelist_add.Count > 0) sQLControl_sub_devicelist.AddRows(null, list_sub_devicelist_add);
                if (list_sub_devicelist_replace.Count > 0) sQLControl_sub_devicelist.UpdateByDefulteExtra(null, list_sub_devicelist_replace);
            }
            catch
            {
                return false;
            }
            result = "";
            result += $"新增 devicelist 資訊成功! 新增 <{ list_devicelist_add.Count}> 筆,修改 <{ list_devicelist_replace.Count}> 筆\n";
            result += $"新增 sub_devicelist 資訊成功! 新增 <{ list_sub_devicelist_add.Count}> 筆,修改 <{ list_sub_devicelist_replace.Count}> 筆\n";
            return true;
        }
        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            SQLControl sQLControl;
            List<Table> tables = new List<Table>();
            sQLControl = new SQLControl(Server, DB, "devicelist", UserName, Password, Port, SSLMode);
            Table table_devicelist = new Table("devicelist");
            table_devicelist.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_devicelist.AddColumnList("名稱", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_devicelist.AddColumnList("顏色", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_devicelist.AddColumnList("備註", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table_devicelist);
            else sQLControl.CheckAllColumnName(table_devicelist, true);
            tables.Add(table_devicelist);

            sQLControl = new SQLControl(Server, DB, "sub_devicelist", UserName, Password, Port, SSLMode);
            Table table_sub_devicelist = new Table("sub_devicelist");
            table_sub_devicelist.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_sub_devicelist.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_sub_devicelist.AddColumnList("type", Table.StringType.VARCHAR, 100, Table.IndexType.INDEX);
            table_sub_devicelist.AddColumnList("name", Table.StringType.VARCHAR, 100, Table.IndexType.None);
            table_sub_devicelist.AddColumnList("value", Table.StringType.VARCHAR, 500, Table.IndexType.None);
            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table_sub_devicelist);
            else sQLControl.CheckAllColumnName(table_sub_devicelist, true);
            tables.Add(table_sub_devicelist);

            return tables.JsonSerializationt(true);
        }
    }
}
