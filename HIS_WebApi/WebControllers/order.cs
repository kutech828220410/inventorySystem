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
    public class orderController : Controller
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
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "醫囑資料");
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

        public string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "order_list", UserName, Password, Port, SSLMode);
            Table table = new Table("order_list");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("PRI_KEY", Table.StringType.VARCHAR, 200, Table.IndexType.INDEX);
            table.AddColumnList("藥局代碼", Table.StringType.VARCHAR, 15, Table.IndexType.None);
            table.AddColumnList("藥袋條碼", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("藥袋類型", Table.StringType.VARCHAR, 15, Table.IndexType.None);
            table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("領藥號", Table.StringType.VARCHAR, 15, Table.IndexType.None);
            table.AddColumnList("批序", Table.StringType.VARCHAR, 15, Table.IndexType.None);
            table.AddColumnList("單次劑量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("劑量單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("途徑", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("頻次", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("費用別", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("病房", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("床號", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("病人姓名", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table.AddColumnList("病歷號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("交易量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("開方日期", Table.DateType.DATETIME, 15, Table.IndexType.None);
            table.AddColumnList("結方日期", Table.DateType.DATETIME, 15, Table.IndexType.None);
            table.AddColumnList("展藥時間", Table.DateType.DATETIME, 15, Table.IndexType.None);
            table.AddColumnList("產出時間", Table.DateType.DATETIME, 15, Table.IndexType.None);
            table.AddColumnList("過帳時間", Table.DateType.DATETIME, 15, Table.IndexType.None);
            table.AddColumnList("狀態", Table.StringType.VARCHAR, 15, Table.IndexType.None);
            table.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);


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
