using Basic;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MyUI;
using SQLUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_系統
{
    [Route("api/[controller]")]
    [ApiController]
    public class temperature : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "溫度物件", typeof(temperatureClass))]

        /// <summary>
        ///初始化溫度資料庫
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[""]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("init")]
        public string init()
        {
            returnData returnData = new returnData();
            try
            {
                return CheckCreatTable();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"{ex.Message}";
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 新增溫度資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "GUID":
        ///         "IP"
        ///         "name": "",
        ///         "temp":"",
        ///         "humidity":"",
        ///         "add_time":""
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add")]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if(returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤!";
                    return returnData.JsonSerializationt();
                }
                temperatureClass temperatureClass = returnData.Data.ObjToClass<temperatureClass>();
                if(temperatureClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data資料錯誤，須為 {temperatureClass}!";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");               

                SQLControl sQLControl = new SQLControl(Server, DB, "temperature", UserName, Password, Port, SSLMode);

                temperatureClass.GUID = Guid.NewGuid().ToString();
                if (temperatureClass.IP.StringIsEmpty()) Logger.Log("temperature", $"IP資料為空 \n{returnData.JsonSerializationt(true)}");
                if (temperatureClass.溫度.StringIsEmpty()) Logger.Log("temperature", $"溫度資料為空 \n{returnData.JsonSerializationt(true)}");
                if (temperatureClass.濕度.StringIsEmpty()) Logger.Log("temperature", $"濕度資料為空 \n{returnData.JsonSerializationt(true)}");
                if (temperatureClass.新增時間.StringIsEmpty()) temperatureClass.新增時間 = DateTime.Now.ToDateTimeString();

                object[] add = temperatureClass.ClassToSQL<temperatureClass, enum_temperature>();
                sQLControl.AddRow(null, add);

                returnData.Code = 200;
                returnData.Data = temperatureClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add";
                returnData.Result = $"資料寫入成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Index was outside the bounds of the array.") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以別名和新增時間取得資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["開始時間","結束時間"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_temp_by_time")]
        public string get_temp_by_time([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry不得為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤，須為 [\"開始時間,\"結束時間\"]";
                    return returnData.JsonSerializationt();
                }
                string 開始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];
                if (開始時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"開始時間格式錯誤";
                    return returnData.JsonSerializationt();
                }
                if (結束時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"結束時間格式錯誤";
                    return returnData.JsonSerializationt();
                }
                //List<temperature_setClass> temperature_SetClasses = get_set(returnData).;
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                SQLControl sQLControl = new SQLControl(Server, DB, "temperature", UserName, Password, Port, SSLMode);
                string command = $"SELECT * FROM {DB}.temperature WHERE 新增時間 BETWEEN '{開始時間}' AND '{結束時間}';";
                DataTable dataTable = sQLControl.WtrteCommandAndExecuteReader(command);
                List<object[]> objects = dataTable.DataTableToRowList();
                List<temperatureClass> temperatureClasses = objects.SQLToClass<temperatureClass, enum_temperature>();
                temperatureClasses.Sort(new temperatureClass.ICP_By_time());
                returnData.Code = 200;
                returnData.Data = temperatureClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_temp_by_name";
                returnData.Result = $"取得{開始時間}~{結束時間}溫度資料成功，共{temperatureClasses.Count()}!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Table 'dbvm.temperature' doesn't exist") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得最新資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_latest_today")]
        public string get_latest_today([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                DateTime now = DateTime.Now;
                string 開始時間 = now.GetStartDate().ToDateTimeString();
                string 結束時間 = now.GetEndDate().ToDateTimeString();

                
                if (開始時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"開始時間格式錯誤";
                    return returnData.JsonSerializationt();
                }
                if (結束時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"結束時間格式錯誤";
                    return returnData.JsonSerializationt();
                }

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                SQLControl sQLControl = new SQLControl(Server, DB, "temperature", UserName, Password, Port, SSLMode);
                string command = $"SELECT * FROM {DB}.temperature WHERE 新增時間 BETWEEN '{開始時間}' AND '{結束時間}';";
                DataTable dataTable = sQLControl.WtrteCommandAndExecuteReader(command);
                List<object[]> objects = dataTable.DataTableToRowList();
                List<temperatureClass> temperatureClasses = objects.SQLToClass<temperatureClass, enum_temperature>();
                List<temperatureClass> temperatureClasses_buff = temperatureClasses
                    .GroupBy(g => g.IP)
                    .Select(grouped =>
                    {
                        temperatureClass temperatureClass = grouped.OrderByDescending(item => item.新增時間).First();
                        return temperatureClass;
                    }).ToList();
                returnData.Code = 200;
                returnData.Data = temperatureClasses_buff;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_latest_today";
                returnData.Result = $"取得最新溫度資料成功，共{temperatureClasses_buff.Count()}!";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Table 'dbvm.temperature' doesn't exist") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 新增設定資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "GUID":
        ///         "IP"
        ///         "name": "",
        ///         "temp_max":"",
        ///         "temp_min":"",
        ///         "humidity_max":"",
        ///         "humidity_min":"",
        ///         "alert":"",
        ///         "mail":"",   
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add_set")]
        public string add_set([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤!";
                    return returnData.JsonSerializationt();
                }
                temperature_setClass temperature_setClass = returnData.Data.ObjToClass<temperature_setClass>();
                if (temperature_setClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data資料錯誤，須為 {temperature_setClass}!";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                SQLControl sQLControl = new SQLControl(Server, DB, "temperature_set", UserName, Password, Port, SSLMode);

                temperature_setClass.GUID = Guid.NewGuid().ToString();
                if (temperature_setClass.警報.StringIsEmpty() == false) temperature_setClass.警報 = temperature_setClass.警報.StringToBool().ToString();
                if (temperature_setClass.發信.StringIsEmpty() == false) temperature_setClass.發信 = temperature_setClass.發信.StringToBool().ToString();




                object[] add = temperature_setClass.ClassToSQL<temperature_setClass, enum_temperature_set>();
                sQLControl.AddRow(null, add);

                returnData.Code = 200;
                returnData.Data = temperature_setClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add_set";
                returnData.Result = $"資料寫入成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Index was outside the bounds of the array.") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新溫度資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "GUID":
        ///         "IP"
        ///         "name": "",
        ///         "temp_max":"",
        ///         "temp_min":"",
        ///         "humidity_max":"",
        ///         "humidity_min":"",
        ///         "alert":"",
        ///         "mail":""  
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_set")]
        public string update_set([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤!";
                    return returnData.JsonSerializationt();
                }
                temperature_setClass temperature_setClass = returnData.Data.ObjToClass<temperature_setClass>();
                if (temperature_setClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data資料錯誤，須為 {temperature_setClass}!";
                    return returnData.JsonSerializationt();
                }
                if (temperature_setClass.GUID.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data.GUID資料錯誤，須為 {temperature_setClass.GUID}!";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                SQLControl sQLControl = new SQLControl(Server, DB, "temperature_set", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl.GetRowsByDefult(null, (int)enum_temperature_set.GUID, temperature_setClass.GUID);
                if (objects.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找不到對應的資料，請確認GUID是否正確!";
                    return returnData.JsonSerializationt();
                }
                temperature_setClass temperature_set_Class_buff = objects[0].SQLToClass<temperature_setClass, enum_temperature_set>();
                if(temperature_setClass.IP.StringIsEmpty() == false) temperature_set_Class_buff.IP = temperature_setClass.IP;
                if (temperature_setClass.別名.StringIsEmpty() == false) temperature_set_Class_buff.別名 = temperature_setClass.別名;
                if (temperature_setClass.溫度上限.StringIsEmpty() == false) temperature_set_Class_buff.溫度上限 = temperature_setClass.溫度上限;
                if (temperature_setClass.溫度下限.StringIsEmpty() == false) temperature_set_Class_buff.溫度下限 = temperature_setClass.溫度下限;
                if (temperature_setClass.濕度上限.StringIsEmpty() == false) temperature_set_Class_buff.濕度上限 = temperature_setClass.濕度上限;
                if (temperature_setClass.濕度下限.StringIsEmpty() == false) temperature_set_Class_buff.濕度下限 = temperature_setClass.濕度下限;
                if (temperature_setClass.警報.StringIsEmpty() == false) temperature_set_Class_buff.警報 = temperature_setClass.警報.StringToBool().ToString();
                if (temperature_setClass.發信.StringIsEmpty() == false) temperature_set_Class_buff.發信 = temperature_setClass.發信.StringToBool().ToString();

                object[] update = temperature_set_Class_buff.ClassToSQL<temperature_setClass, enum_temperature_set>();
                sQLControl.UpdateByDefulteExtra(null, update);

                returnData.Code = 200;
                returnData.Data = temperature_set_Class_buff;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "update_set";
                returnData.Result = $"資料寫入成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Index was outside the bounds of the array.") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新溫度資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_set")]
        public string get_set([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
              
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                SQLControl sQLControl = new SQLControl(Server, DB, "temperature_set", UserName, Password, Port, SSLMode);
                List<object[]> objects = sQLControl.GetAllRows(null);
                List<temperature_setClass> temperature_SetClasses = objects.SQLToClass<temperature_setClass, enum_temperature_set>();
       
                returnData.Code = 200;
                returnData.Data = temperature_SetClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_set";
                returnData.Result = $"資料寫入成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Index was outside the bounds of the array.") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }


        private string CheckCreatTable()
        {
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData returnData = new returnData();
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }

            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_temperature()));
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_temperature_set()));

            return tables.JsonSerializationt(true);
        }
        private string GetRandomTemp()
        {
            Random rand = new Random();
            int chance = rand.Next(100);
            int value = (chance < 90) ? rand.Next(22, 29) : rand.Next(29, 33);
            return value.ToString();
        }
        private string GetRandomHumidity()
        {
            Random rand = new Random();
            int chance = rand.Next(100);
            int value = (chance < 90) ? rand.Next(55, 65) : rand.Next(65, 75);
            return value.ToString();
        }
    }
}
    
