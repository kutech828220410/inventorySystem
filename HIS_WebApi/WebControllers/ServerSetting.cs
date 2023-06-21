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
    public class ServerSettingController : Controller
    {
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string Server = ConfigurationManager.AppSettings["Server"];
        static private string DB = "DBVM";
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("init")]
        [HttpGet]
        public string GET_init()
        {
            try
            {
                return CheckCreatTable();
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        [Route("type")]
        [HttpGet]
        public string GET_type()
        {
            return new enum_ServerSetting_Type().GetEnumNames().JsonSerializationt();
        }

        [Route("program")]
        [HttpGet]
        public string GET_program()
        {
            return new enum_ServerSetting_ProgramType().GetEnumNames().JsonSerializationt();
        }

        [HttpGet]
        public string GET()
        {
            this.CheckCreatTable();
            SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
            List<object[]> list_value = sQLControl.GetAllRows(null);

            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.SQLToClass(list_value);
            returnData returnData = new returnData();
            returnData.Code = 200;
            returnData.Data = serverSettingClasses;
            returnData.Result = "取得伺服器設定成功!";
            return returnData.JsonSerializationt(true);
        }
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            this.CheckCreatTable();
            SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
            List<object[]> list_value = sQLControl.GetAllRows(null);
            List<object[]> list_value_returnData = new List<object[]>();
            List<object[]> list_value_add = new List<object[]>();
            List<object[]> list_value_replace = new List<object[]>();
            List<object[]> list_value_buf = new List<object[]>();
            returnData.Method = "add";
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);
            list_value_returnData = ServerSettingClass.ClassToListSQL(serverSettingClasses);
            for (int i = 0; i < list_value_returnData.Count; i++)
            {
                string 名稱 = list_value_returnData[i][(int)enum_ServerSetting.設備名稱].ObjectToString();
                string 類別 = list_value_returnData[i][(int)enum_ServerSetting.類別].ObjectToString();
                string 程式類別 = list_value_returnData[i][(int)enum_ServerSetting.程式類別].ObjectToString();
                string 內容 = list_value_returnData[i][(int)enum_ServerSetting.內容].ObjectToString();

                list_value_buf = list_value.GetRows((int)enum_ServerSetting.設備名稱, 名稱);
                list_value_buf = list_value_buf.GetRows((int)enum_ServerSetting.類別, 類別);
                list_value_buf = list_value_buf.GetRows((int)enum_ServerSetting.程式類別, 程式類別);
                list_value_buf = list_value_buf.GetRows((int)enum_ServerSetting.內容, 內容);
                if (list_value_buf.Count == 0)
                {
                    object[] value = list_value_returnData[i];
                    value[(int)enum_ServerSetting.GUID] = Guid.NewGuid().ToString();
                    list_value_add.Add(value);
                }
                else
                {
                    object[] value = list_value_returnData[i];
                    value[(int)enum_ServerSetting.GUID] = list_value_buf[0][(int)enum_ServerSetting.GUID].ObjectToString();
                    list_value_replace.Add(value);
                }
            }
            sQLControl.AddRows(null, list_value_add);
            sQLControl.UpdateByDefulteExtra(null, list_value_replace);

            returnData.Code = 200;
            returnData.Result = "新增伺服器資料成功!";
            returnData.Data = serverSettingClasses;
            return returnData.JsonSerializationt();
        }
        [Route("delete")]
        [HttpPost]
        public string POST_delete([FromBody] returnData returnData)
        {
            this.CheckCreatTable();
            SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
            List<object[]> list_value = sQLControl.GetAllRows(null);
            List<object[]> list_value_returnData = new List<object[]>();
            List<object[]> list_value_add = new List<object[]>();
            List<object[]> list_value_replace = new List<object[]>();
            List<object[]> list_value_buf = new List<object[]>();
            returnData.Method = "add";
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);
            list_value_returnData = ServerSettingClass.ClassToListSQL(serverSettingClasses);
            
            sQLControl.DeleteExtra(null, list_value_returnData);

            returnData.Code = 200;
            returnData.Result = "刪除伺服器資料成功!";
            returnData.Data = serverSettingClasses;
            return returnData.JsonSerializationt();
        }
        private string CheckCreatTable()
        {
            SQLControl sQLControl = new SQLControl(Server, DB, "ServerSetting", UserName, Password, Port, SSLMode);
            Table table = new Table("ServerSetting");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("單位", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("設備名稱", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("類別", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("程式類別", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("內容", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("Server", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("Port", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("DBName", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("TableName", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("User", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("Password", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            if (!sQLControl.IsTableCreat())
            {
                sQLControl.CreatTable(table);
            }
            else
            {
                sQLControl.CheckAllColumnName(table, true);
            }
            return table.JsonSerializationt(true);
        }
    }
}
