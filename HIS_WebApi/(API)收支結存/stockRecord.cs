using System;
using Microsoft.AspNetCore.Mvc;
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
    /// <summary>
    /// 庫存紀錄
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class stockRecord : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化庫存紀錄資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "管藥",
        ///     "ServerType" : "調劑台",
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
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
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

            SQLControl sQLControl_stockRecord = new SQLControl(Server, DB, "stockRecord", UserName, Password, Port, SSLMode);

            List<Table> tables = new List<Table>();

            Table table_stockRecord = new Table("stockRecord", new enum_stockRecord());
            table_stockRecord.Server = Server;
            table_stockRecord.DBName = DB;
            table_stockRecord.Username = UserName;
            table_stockRecord.Password = Password;
            table_stockRecord.Port = Port.ToString();
            tables.Add(table_stockRecord);
            if (!sQLControl_stockRecord.IsTableCreat()) sQLControl_stockRecord.CreatTable(table_stockRecord);
            else sQLControl_stockRecord.CheckAllColumnName(table_stockRecord, true);
            return table_stockRecord.JsonSerializationt(true);
        }
    }
}
