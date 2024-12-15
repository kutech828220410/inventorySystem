using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLUI;
using Basic;
using HIS_DB_Lib;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Drawing;
using SkiaSharp;
using System.IO;
using System.Reflection;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class voxVerify : ControllerBase
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
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(voxVerifyClass))]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
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
            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_voxVerify());
            return table.JsonSerializationt(true);
        }
    }
}
