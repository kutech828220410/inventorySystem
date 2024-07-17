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

namespace HIS_WebApi._AIPI_住院調劑系統
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
            catch(Exception ex)
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

            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_藥車資訊());
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
                if(_serverSettingClasses.Count == 0)
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
                string TableName = "med_carInfo";
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetAllRows(null);

                List<medCarInfoClass> medCarInfoClasses = list_med_carInfo.SQLToClass<medCarInfoClass, enum_藥車資訊>();
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"取得藥車資訊共{medCarInfoClasses.Count}筆";
                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt();
            }
        }
        [HttpPost("add")]
        public string POST_add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind("Main", "一般資料", "網頁");
                if (_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                List<medCarInfoClass> medCarInfoClasses = returnData.Data.ObjToClass<List<medCarInfoClass>>();
                List<object[]> list_藥車資訊 = medCarInfoClasses.ClassToSQL<medCarInfoClass, enum_藥車資訊>();
                if (medCarInfoClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string Server = _serverSettingClasses[0].Server;
                string DB = _serverSettingClasses[0].DBName;
                string UserName = _serverSettingClasses[0].User;
                string Password = _serverSettingClasses[0].Password;
                uint Port = (uint)_serverSettingClasses[0].Port.StringToInt32();

                string TableName = "med_carInfo";
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                sQLControl_med_carInfo.DropTable(null);
                sQLControl_med_carInfo.AddRows(null, list_藥車資訊);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"取得藥車資訊共{medCarInfoClasses.Count}筆";
                return returnData.JsonSerializationt();
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt();
            }
        }
    }

}
