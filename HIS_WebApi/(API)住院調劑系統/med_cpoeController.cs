using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using MySql.Data.MySqlClient;
using SQLUI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class med_cpoeController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class IHDSController : ControllerBase
        {
            static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
            static private MySqlSslMode SSLMode = MySqlSslMode.None;

            [HttpPost("init")]
            public string POST_init([FromBody] returnData returnData)
            {

                MyTimerBasic myTimerBasic = new MyTimerBasic();
                try
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    serverSettingClasses = serverSettingClasses.MyFind("Main", "一般資料", "網頁");
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    return CheckCreatTable(serverSettingClasses[0]);

                }
                catch (Exception ex)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Exception : {ex.Message}";
                    return returnData.JsonSerializationt(true);
                }
            }
            private string CheckCreatTable(ServerSettingClass serverSettingClass)
            {
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_病床處方());
                return table.JsonSerializationt(true);
            }
            [HttpPost("get_all")]
            public string POST_get_all([FromBody] returnData returnData)
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                returnData.Method = "get_all";
                try
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind("Main", "一般資料", "網頁");
                    if (_serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    string Server = _serverSettingClasses[0].Server;
                    string DB = _serverSettingClasses[0].DBName;
                    string UserName = _serverSettingClasses[0].User;
                    string Password = _serverSettingClasses[0].Password;
                    uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                    string TableName = "med_cpoe";
                    SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med_carInfo = sQLControl_med_cpoe.GetAllRows(null);

                    List<medCpoeClass> medCarInfoClasses = list_med_carInfo.SQLToClass<medCpoeClass, enum_病床處方>();
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Data = medCarInfoClasses;
                    returnData.Result = $"取得病床資訊共{medCarInfoClasses.Count}筆";
                    return returnData.JsonSerializationt();
                }
                catch (Exception ex)
                {
                    returnData.Code = -200;
                    returnData.Result = ex.Message;
                    return returnData.JsonSerializationt();
                }
            }
            [HttpPost("get_by_bednum")]
            public string POST_get_by_bednum([FromBody] returnData returnData)
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                returnData.Method = "get_all";
                try
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                    List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind("Main", "一般資料", "網頁");
                    if (_serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    if (returnData.ValueAry == null)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"returnData.ValueAry 無傳入資料";
                        return returnData.JsonSerializationt(true);
                    }
                    if (returnData.ValueAry.Count != 3)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"returnData.ValueAry 內容應為[住院藥局, 藥車, 床號]";
                        return returnData.JsonSerializationt(true);
                    }
                    string 床號 = returnData.ValueAry[2];
                    string Server = _serverSettingClasses[0].Server;
                    string DB = _serverSettingClasses[0].DBName;
                    string UserName = _serverSettingClasses[0].User;
                    string Password = _serverSettingClasses[0].Password;
                    uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();
                    string TableName = "med_cpoe";
                    SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med_carInfo = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_病床處方.床號, 床號);

                    List<medCpoeClass> medCpoeClasses = list_med_carInfo.SQLToClass<medCpoeClass, enum_病床處方>();
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Data = medCpoeClasses;
                    returnData.Result = $"取得病床資訊共{medCpoeClasses.Count}筆";
                    return returnData.JsonSerializationt();
                }
                catch (Exception ex)
                {
                    returnData.Code = -200;
                    returnData.Result = ex.Message;
                    return returnData.JsonSerializationt();
                }
            }
        }
    }
}
