using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Drawing;
using System.Text;
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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using DrawingClass;


namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class controlController : Controller
    {

        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";

        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;


        [Route("unlock/{value}")]
        [HttpGet]
        public string GET_unlock(string value)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            returnData.Method = "GET_unlock";
            try
            {

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                ServerSettingClass serverSettingClass = serverSettingClasses.MyFind(value, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.本地端);
                if (serverSettingClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無伺服器資料";
                    return returnData.JsonSerializationt(true);
                }
                SQLControl sQLControl_unlock = new SQLControl(serverSettingClass.Server, serverSettingClass.DBName, "locker_index_table", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_unlock.GetAllRows(null);
                for(int i = 0; i < list_value.Count; i++) 
                {
                    list_value[i][(int)enum_Locker_Index_Table.輸出狀態] = true.ToString();
                }
                returnData.TimeTaken = myTimer.ToString();
                returnData.Code = 200;
                returnData.Result = $"解鎖所有儲位成功";
                sQLControl_unlock.UpdateByDefulteExtra(null, list_value);
                return returnData.JsonSerializationt(true);

            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
    }
}
