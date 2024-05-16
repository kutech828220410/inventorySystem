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
    [Route("api/[controller]")]
    [ApiController]
    public class drugstotreDistribution : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化紀錄資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
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
        public string POST_init([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);

            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

                returnData.Method = "POST_init";
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 紀錄庫存資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "Data": 
        ///     {
        ///         [drugstotreDistributionClass]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public string POST_add(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add";
            try
            {
                POST_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();

                List<drugstotreDistribution> drugstotreDistributions = returnData.Data.ObjToClass<List<drugstotreDistribution>>();

                SQLControl sQLControl_drugstotreDistribution = new SQLControl(Server, DB, new enum_drugstotreDistribution().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_drugstotreDistributions = drugstotreDistributions.ClassToSQL<drugstotreDistribution, enum_drugstotreDistribution>();
                for (int i = 0; i < list_drugstotreDistributions.Count; i++)
                {
                    list_drugstotreDistributions[i][(int)enum_drugstotreDistribution.GUID] = Guid.NewGuid().ToString();
                    list_drugstotreDistributions[i][(int)enum_drugstotreDistribution.加入時間] = DateTime.Now.ToDateTimeString_6();
                    list_drugstotreDistributions[i][(int)enum_drugstotreDistribution.報表生成時間] = DateTime.MinValue.ToDateTimeString_6();
                    list_drugstotreDistributions[i][(int)enum_drugstotreDistribution.撥發時間] = DateTime.MinValue.ToDateTimeString_6();
                }


                returnData.Result = $"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 成功新增<{list_drugstotreDistributions.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                Logger.LogAddLine($"drugstotreDistribution");
                Logger.Log($"stockRecord", $"{ returnData.JsonSerializationt(true)}");
                Logger.LogAddLine($"drugstotreDistribution");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"drugstotreDistribution", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }


        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(serverSettingClass, new enum_drugstotreDistribution()));
            return tables.JsonSerializationt(true);
        }
    }
}
