using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.Text;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class medPic : Controller
    {
        static public string API_Server = "http://127.0.0.1:4433/api/serversetting";
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
        ///   
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(medPicClass))]
        [Route("init")]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
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
                string msg = "";
                return msg;
            }
        }
        /// <summary>
        /// 新增藥品圖片資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data":
        ///     [
        ///       {
        ///         "GUID": "unique-guid",
        ///         "code": "med-code",
        ///         "name": "med-name",
        ///         "pic_base64": "base64-image-string"
        ///         "pic1_base64": "base64-image1-string"
        ///       }
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [HttpPost("add")]
        public string POST_add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                GET_init(returnData);
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
                serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                List<medPicClass> medPicClasses = returnData.Data.ObjToClass<List<medPicClass>>();
                if (medPicClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass_VM = serverSettingClasses_buf[0];

                string Server = serverSettingClass_VM.Server;
                string DB = serverSettingClass_VM.DBName;
                string UserName = serverSettingClass_VM.User;
                string Password = serverSettingClass_VM.Password;
                uint Port = (uint)serverSettingClass_VM.Port.StringToInt32();

                List<medPicClass> medPicClasses_add = new List<medPicClass>();
                List<medPicClass> medPicClasses_replace = new List<medPicClass>();
                List<medPicClass> medPicClasses_buf = new List<medPicClass>();
                SQLControl sQLControl_medPic = new SQLControl(Server, DB, new enum_medPic().GetEnumDescription() ,UserName, Password, Port, SSLMode);

                for (int i = 0; i < medPicClasses.Count; i++)
                {
                    List<object[]> list_value = sQLControl_medPic.GetRowsByDefult(null, (int)enum_medPic.藥碼, medPicClasses[i].藥碼);
                    if (list_value.Count == 0)
                    {
                        medPicClass medPicClass = new medPicClass();
                        medPicClass.GUID = Guid.NewGuid().ToString();
                        medPicClass.藥碼 = medPicClasses[i].藥碼;
                        medPicClass.藥名 = medPicClasses[i].藥名;
                        medPicClass.副檔名 = medPicClasses[i].副檔名;
                        medPicClass.副檔名1 = medPicClasses[i].副檔名1;
                        medPicClass.pic_base64 = medPicClasses[i].pic_base64;
                        medPicClass.pic1_base64 = medPicClasses[i].pic1_base64;
                        medPicClasses_add.Add(medPicClass);
                    }
                    else
                    {
                        medPicClass medPicClass = list_value[0].SQLToClass<medPicClass, enum_medPic>();
                        medPicClass.藥碼 = medPicClasses[i].藥碼;
                        medPicClass.藥名 = medPicClasses[i].藥名;
                        medPicClass.副檔名 = medPicClasses[i].副檔名;
                        medPicClass.副檔名1 = medPicClasses[i].副檔名1;
                        medPicClass.pic_base64 = medPicClasses[i].pic_base64;
                        medPicClass.pic1_base64 = medPicClasses[i].pic1_base64;
                        medPicClasses_replace.Add(medPicClass);
                    }

                }
                List<object[]> list_add = medPicClasses_add.ClassToSQL<medPicClass, enum_medPic>();
                List<object[]> list_replace = medPicClasses_replace.ClassToSQL<medPicClass, enum_medPic>();

                sQLControl_medPic.AddRows(null, list_add);
                sQLControl_medPic.UpdateByDefulteExtra(null, list_replace);

                returnData.Code = 200;
                returnData.Result = $"更新藥品圖片資料成功,新增<{list_add.Count}>筆,修改<{list_replace.Count}>筆";
                returnData.Data = "";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -500;
                returnData.Result = $"內部伺服器錯誤: {ex.Message}";
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 根據藥碼獲取藥品圖片資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" :
        ///     [
        ///        "code"
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [HttpPost("get_by_code")]
        public string POST_get_by_code([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_code";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
                serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[code]";
                    return returnData.JsonSerializationt(true);
                }
                ServerSettingClass serverSettingClass_VM = serverSettingClasses_buf[0];

                string Server = serverSettingClass_VM.Server;
                string DB = serverSettingClass_VM.DBName;
                string UserName = serverSettingClass_VM.User;
                string Password = serverSettingClass_VM.Password;
                uint Port = (uint)serverSettingClass_VM.Port.StringToInt32();
                string Code = returnData.ValueAry[0];
                SQLControl sQLControl_medPic = new SQLControl(Server, DB, new enum_medPic().GetEnumDescription(), UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_medPic.GetRowsByDefult(null, (int)enum_medPic.藥碼, Code);
                if (list_value.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt(true);
                }
                medPicClass medPicClass = list_value[0].SQLToClass<medPicClass , enum_medPic>();
                if (medPicClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常";
                    return returnData.JsonSerializationt(true);
                }
                returnData.Result = $"根據藥碼獲取藥品圖片資料";
                returnData.Code = 200;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = medPicClass;
                return returnData.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"內部伺服器錯誤: {ex.Message}";
                return returnData.JsonSerializationt();
            }
        }

        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_medPic());
            return table.JsonSerializationt(true);
        }
    }
}
