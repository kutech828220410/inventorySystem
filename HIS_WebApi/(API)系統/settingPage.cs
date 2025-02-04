using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using MySql.Data.MySqlClient;
using SQLUI;
using System.Collections.Concurrent;

namespace HIS_WebApi._API_系統
{
    [Route("api/[controller]")]
    [ApiController]
    public class settingPage : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [HttpPost("init_settingPage")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "settingPageClass物件", typeof(settingPageClass))]
        public string init_settingPage([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "init_settingPage";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0], new enum_settingPage());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        ///以page_name取得資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["page_name"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_by_page_name")]
        public string get_by_page_name([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_page_name";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"page_name\"]";
                    return returnData.JsonSerializationt(true);
                }
                string 頁面名稱 = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "settingPage", UserName, Password, Port, SSLMode);
                List<object[]> list_settingPage = sQLControl.GetRowsByDefult(null, (int)enum_settingPage.頁面名稱, 頁面名稱);
                if (list_settingPage.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<settingPageClass> settingPageClasses = list_settingPage.SQLToClass<settingPageClass, enum_settingPage>();
                for(int i = 0; i < settingPageClasses.Count; i++)
                {
                    if (!settingPageClasses[i].選項.StringIsEmpty())
                    {
                        List<string> option = settingPageClasses[i].選項.Split(";").ToList();
                        settingPageClasses[i].option = option;
                    }
                    if(settingPageClasses[i].欄位代碼 == "display_block")
                    {
                        List<uiConfig> uiConfigs = Convert(settingPageClasses[i].設定值);
                        settingPageClasses[i].value = uiConfigs;
                    }
                    else
                    {
                        settingPageClasses[i].value = settingPageClasses[i].設定值;

                    }
                }
                returnData.Code = 200;
                returnData.Data = settingPageClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得頁面 : {頁面名稱}的資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 增加資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":
        ///         [
        ///             {
        ///                 settingPage
        ///             }
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add")]
        public string add([FromBody] returnData returnData)
        {
            returnData.Method = "add";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<settingPageClass> input_settingPageClass = returnData.Data.ObjToClass<List<settingPageClass>>();
                if (input_settingPageClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "settingPage", UserName, Password, Port, SSLMode);
                foreach(var item in input_settingPageClass)
                {
                    item.GUID = Guid.NewGuid().ToString();
                }
                List<object[]> list_textVision = input_settingPageClass.ClassToSQL<settingPageClass, enum_settingPage>();
                sQLControl_textVision.AddRows(null, list_textVision);

                returnData.Code = 200;
                returnData.Data = input_settingPageClass;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"資料儲存共{input_settingPageClass.Count}筆 成功";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }


        }
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["GUID", "value"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update")]
        public string update([FromBody] returnData returnData)
        {
            returnData.Method = "update";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\",\"value\"]";
                    return returnData.JsonSerializationt(true);
                }

                string GUID = returnData.ValueAry[0];
                string value_db = returnData.ValueAry[1];

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "settingPage", UserName, Password, Port, SSLMode);
                List<object[]> list_settingPage = sQLControl.GetRowsByDefult(null, (int)enum_settingPage.GUID, GUID);
                list_settingPage[0][(int)enum_settingPage.設定值] = value_db;
                sQLControl.UpdateByDefulteExtra(null, list_settingPage);

                returnData.Code = 200;
                returnData.Data = list_settingPage;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"資料變更共{list_settingPage.Count}筆 成功";
                return returnData.JsonSerializationt(true);
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
        private (string Server, string DB, string UserName, string Password, uint Port) GetServerInfo(string Name, string Type, string Content)
        {
            List<ServerSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            ServerSettingClass serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return (serverSettingClass.Server, serverSettingClass.DBName, serverSettingClass.User, serverSettingClass.Password, (uint)serverSettingClass.Port.StringToInt32());
        }
        private string GetServerAPI(string Name, string Type, string Content)
        {
            List<ServerSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            ServerSettingClass serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return serverSettingClass.Server;
        }
        private Dictionary<string, string> dicColumnName = new Dictionary<string, string>
        {
            { "ordseq", "序號" },
            { "dosage", "劑量" },
            { "dunit", "單位" },
            { "freqn", "頻次" },
            { "route", "途徑" },
            { "code", "藥碼" },
            { "storage", "儲位" }
        };
        private List<uiConfig> Convert(string value_db)
        {
            List<uiConfig> uiConfigs = new List<uiConfig>();
            List<string> list_value = value_db.Split(";").ToList();
            foreach(string key in dicColumnName.Keys)
            {
                uiConfig uiConfig = new uiConfig();
                uiConfig.欄位代碼 = key;
                uiConfig.欄位名稱 = dicColumnName[key];

                if (list_value.Contains(key))
                {
                    uiConfig.設定值 = "True";
                }
                else
                {
                    uiConfig.設定值 = "False";
                }
                uiConfigs.Add(uiConfig);
            }
            return uiConfigs;
        }
        
    }
}
