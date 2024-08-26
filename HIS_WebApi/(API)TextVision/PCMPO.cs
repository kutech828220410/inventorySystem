using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLUI;
using Basic;
using HIS_DB_Lib;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_TextVision
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCMPO : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [HttpPost("init_pcmpo")]
        public string init_pcmpo([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0], new enum_textVision());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        private string CheckCreatTable(ServerSettingClass serverSettingClass, Enum enumInstance)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(serverSettingClass, enumInstance);
            return table.JsonSerializationt(true);
        }
        [HttpPost("analyze")]
        public string analyze([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "analyze";
            try 
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<textVisionClass> input_textVision = returnData.Data.ObjToClass<List<textVisionClass>>();
                input_textVision[0].GUID = Guid.NewGuid().ToString();
                input_textVision[0].操作時間 = DateTime.Now.ToDateTimeString();

                List<object[]> sql_textVision = input_textVision.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.AddRows(null, sql_textVision);

                List<textVisionClass> aiResult = textVisionClass.ai_analyze(input_textVision);



                returnData.Code = 200;
                returnData.Data = aiResult;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "";
                returnData.Result = $"";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
            

        }



    }
}
