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
    public class MED_pageController : Controller
    {
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [HttpPost]
        public string Get(returnData returnData)
        {
 
            string Server = returnData.Server;
            string DbName = returnData.DbName;
            string TableName = returnData.TableName;
            SQLControl sQLControl_med = new SQLControl(Server, DbName, TableName, UserName, Password, Port, SSLMode);
            string[] colName = sQLControl_med.GetAllColumn_Name(TableName);
            List<object[]> list_med = sQLControl_med.GetAllRows(null);

            if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_雲端藥檔()))
            {
                returnData.Data = medCouldClass.SQLToClass(list_med);
                returnData.Code = 200;
                returnData.Result = "雲端藥檔取得成功!";
                return returnData.JsonSerializationt(true);
            }
            if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_藥庫_藥品資料()))
            {
                returnData.Data = medDrugstoreClass.SQLToClass(list_med);
                returnData.Code = 200;
                returnData.Result = "藥庫藥檔取得成功!";
                return returnData.JsonSerializationt(true);
            }
            if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_藥局_藥品資料()))
            {
                returnData.Data = medPharmacyClass.SQLToClass(list_med);
                returnData.Code = 200;
                returnData.Result = "藥局藥檔取得成功!";
                return returnData.JsonSerializationt(true);
            }
            if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_藥品資料_藥檔資料()))
            {
                returnData.Data = medSMDSClass.SQLToClass(list_med);
                returnData.Code = 200;
                returnData.Result = "調劑台藥檔取得成功!";
                return returnData.JsonSerializationt(true);
            }
            returnData.Code = -5;
            returnData.Result = "藥檔取得失敗!";

            return returnData.JsonSerializationt();

        }
    }
}
