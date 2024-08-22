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
    public class medGeneralConfig : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        /// 
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(medGeneralConfigClass))]
        [Route("init")]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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
                string msg = $"Exception : {e.Message}";
                return msg;
            }
        }
        /// <summary>
        /// 取得全部藥品通用設定
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("get_all")]
        [HttpPost]
        public string get_all([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_all";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
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
                string TableName = new enum_medGeneralConfig().GetEnumDescription();
                SQLControl sQLControl_med_config = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_config = sQLControl_med_config.GetAllRows(null);

                List<medGeneralConfigClass> medConfigClasses = list_med_config.SQLToClass<medGeneralConfigClass, enum_medGeneralConfig>();
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medConfigClasses;
                returnData.Result = $"取得通用藥品設定成功,共<{medConfigClasses.Count}>筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 更新藥品通用設定
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [medGeneralConfigClass陣列]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        public string update([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update";
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> _serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
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
                string TableName = new enum_medGeneralConfig().GetEnumDescription();
                SQLControl sQLControl_med_config = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_config = sQLControl_med_config.GetAllRows(null);

                List<medGeneralConfigClass> medConfigClassesData = returnData.Data.ObjToClass<List<medGeneralConfigClass>>();
                if (medConfigClassesData == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data 資料錯誤";
                    return returnData.JsonSerializationt();
                }
                List<medGeneralConfigClass> medConfigClasses = list_med_config.SQLToClass<medGeneralConfigClass, enum_medGeneralConfig>();
                List<medGeneralConfigClass> medConfigClasses_buf = new List<medGeneralConfigClass>();
                List<medGeneralConfigClass> medConfigClasses_replace = new List<medGeneralConfigClass>();

                for (int i = 0; i < medConfigClassesData.Count; i++)
                {
                    string code = medConfigClassesData[i].代號;

                    medConfigClasses_buf = (from temp in medConfigClasses
                                            where temp.代號 == code
                                            select temp).ToList();
                    if(medConfigClasses_buf.Count > 0)
                    {
                        medConfigClassesData[i].GUID = medConfigClasses_buf[0].GUID;
                        if (medConfigClassesData[i].複盤.StringToBool()) medConfigClassesData[i].盲盤 = false.ToString();
                        if (medConfigClassesData[i].盲盤.StringToBool()) medConfigClassesData[i].複盤 = false.ToString();
                        medConfigClasses_replace.Add(medConfigClassesData[i]);
                    }
                }
                List<object[]> list_replace = medConfigClasses_replace.ClassToSQL<medGeneralConfigClass, enum_medGeneralConfig>();
                sQLControl_med_config.UpdateByDefulteExtra(null, list_replace);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medConfigClasses_replace;
                returnData.Result = $"修改通用藥品設定成功,共<{list_replace.Count}>筆資料";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }


        private void Function_CheckTable(ServerSettingClass serverSettingClass , Enum _enum)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            SQLControl sQLControl = new SQLControl(Server, DB, _enum.GetEnumDescription(), UserName, Password, Port, SSLMode);

            List<object[]> list_value = sQLControl.GetAllRows(null);
            List<object[]> list_value_buf = new List<object[]>();
            List<object[]> list_value_delete = new List<object[]>();
            List<object[]> list_value_add = new List<object[]>();
            list_value_delete = (from value in list_value
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "N"
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "1"
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "2"
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "3"
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "4"
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "警訊"
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "高價"
                                 where value[(int)enum_medGeneralConfig.代號].ObjectToString() != "生物製劑"
                                 select value).ToList();
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "N");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "N";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                value[(int)enum_medGeneralConfig.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "1");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "1";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                value[(int)enum_medGeneralConfig.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "2");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "2";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                value[(int)enum_medGeneralConfig.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "3");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "3";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "4");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "4";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                value[(int)enum_medGeneralConfig.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "警訊");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "警訊";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                value[(int)enum_medGeneralConfig.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "高價");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "高價";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                value[(int)enum_medGeneralConfig.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_medGeneralConfig.代號, "生物製劑");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_medGeneralConfig().GetLength()];
                value[(int)enum_medGeneralConfig.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_medGeneralConfig.代號] = "生物製劑";
                value[(int)enum_medGeneralConfig.效期管理] = false.ToString();
                value[(int)enum_medGeneralConfig.盲盤] = false.ToString();
                value[(int)enum_medGeneralConfig.複盤] = false.ToString();
                value[(int)enum_medGeneralConfig.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            sQLControl.DeleteExtra(null, list_value_delete);
            sQLControl.AddRows(null, list_value_add);
        }
        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_medGeneralConfig()));
            Function_CheckTable(serverSettingClass, new enum_medGeneralConfig());
            return tables.JsonSerializationt(true);
        }
    }
}
