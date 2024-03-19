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
    [Route("api/[controller]")]
    [ApiController]
    public class medConfigController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [Route("init")]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "med_config", UserName, Password, Port, SSLMode);


            Table table = new Table("med_config");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("藥碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table.AddColumnList("效期管理", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("盲盤", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("複盤", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("結存報表", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("雙人覆核", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("麻醉藥品", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("形狀相似", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("發音相似", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("自定義", Table.StringType.VARCHAR, 10, Table.IndexType.None);

            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
            else sQLControl.CheckAllColumnName(table, true);
            return table.JsonSerializationt(true);
        }
    }
}
