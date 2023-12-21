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
    /// <summary>
    /// 化療藥局癌症備藥機-出料馬達輸出索引表
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CPMP_StorageConfig : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化出料馬達輸出索引表
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "cheom",
        ///     "ServerType" : "癌症備藥機",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [HttpPost]
        public string GET_init_storage_config(returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                return CheckCreatTable_storage_config(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        private string CheckCreatTable_storage_config(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            SQLControl sQLControl_storage_config = new SQLControl(Server, DB, "storage_config", UserName, Password, Port, SSLMode);
        
            Table table_storage_config = new Table("storage_config");

            table_storage_config.Server = Server;
            table_storage_config.DBName = DB;
            table_storage_config.Username = UserName;
            table_storage_config.Password = Password;
            table_storage_config.Port = Port.ToString();


            table_storage_config.SetTableConfig(sQLControl_storage_config);
            table_storage_config.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_storage_config.AddColumnList("IP", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_storage_config.AddColumnList("鎖控輸出索引", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("鎖控輸出觸發", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("鎖控輸入索引", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("鎖控輸入狀態", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("出料馬達輸出索引", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("出料馬達輸出觸發", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("出料馬達輸入索引", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("出料馬達輸入狀態", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("出料馬達輸入延遲時間", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("出料位置X", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("出料位置Y", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_storage_config.AddColumnList("藥盒方位", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            if (!sQLControl_storage_config.IsTableCreat()) sQLControl_storage_config.CreatTable(table_storage_config);
            else sQLControl_storage_config.CheckAllColumnName(table_storage_config, true);

  
            return table_storage_config.JsonSerializationt(true);
        }
    }
}
