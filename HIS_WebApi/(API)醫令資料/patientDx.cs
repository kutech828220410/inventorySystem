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
using System.Text.Json.Serialization;
using H_Pannel_lib;
using Org.BouncyCastle.Bcpg.OpenPgp;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using System.Text.RegularExpressions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_醫令資料
{
    [Route("api/[controller]")]
    [ApiController]
    public class patientDx : ControllerBase
    {
        static string API_Server = Method.GetServerAPI("Main", "網頁", "API01");
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(patientDxClass))]
        [HttpPost]
        public string init()
        {
            try
            {
                return CheckCreatTable(new enum_patientDx());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [patientDxClass陣列]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add")]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                patientDxClass patientDxClasses = returnData.Data.ObjToClass<patientDxClass>();
                if (patientDxClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                string TableName = "patient_dx";
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> add_patientDx = new List<patientDxClass>() { patientDxClasses }.ClassToSQL<patientDxClass, enum_patientDx>();
                sQLControl.AddRows(null, add_patientDx);
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = patientDxClasses;
                returnData.Result = $"新增一筆資料";
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
        /// 以藥袋條碼取得資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "ValueAry":["藥袋條碼"]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_by_barcode")]
        public string get_by_barcode([FromBody] returnData returnData)
        {
            init();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_barcode";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);
                init();
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry應該為[\"藥袋條碼\"]";
                    return returnData.JsonSerializationt();
                }
                string 藥袋條碼 = returnData.ValueAry[0];

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                string TableName = "patient_dx";
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_patientDx = sQLControl.GetRowsByDefult(null, (int)enum_patientDx.藥袋條碼, 藥袋條碼);
                List<patientDxClass> patientDxClasses = list_patientDx.SQLToClass<patientDxClass, enum_patientDx>();
                if(patientDxClasses.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Data = patientDxClasses;
                    returnData.Result = $"取得{patientDxClasses.Count}筆資料";
                    return returnData.JsonSerializationt(true);
                }
                List<MedicalCodeItem> 診斷紀錄 = new List<MedicalCodeItem>();
                List<MedicalCodeItem> 過敏紀錄 = new List<MedicalCodeItem>();
                List<MedicalCodeItem> 交互作用紀錄 = new List<MedicalCodeItem>();

                List<string> list_診斷碼 = patientDxClasses[0].診斷碼.Split(";").ToList();
                List<string> list_診斷疾病 = patientDxClasses[0].診斷內容.Split(";").ToList();
                List<string> list_過敏藥碼 = patientDxClasses[0].過敏藥碼.Split(";").ToList();
                List<string> list_過敏藥名 = patientDxClasses[0].過敏藥名.Split(";").ToList();
                List<string> list_交互作用藥碼 = patientDxClasses[0].交互作用藥碼.Split(";").ToList();
                List<string> list_交互作用 = patientDxClasses[0].交互作用.Split(";").ToList();
                for(int i = 0; i < list_診斷碼.Count; i++)
                {
                    診斷紀錄.Add(new MedicalCodeItem
                    {
                        code = list_診斷碼[i],
                        name = list_診斷疾病[i]
                    }) ;
                }
                for (int i = 0; i < list_過敏藥碼.Count; i++)
                {
                    過敏紀錄.Add(new MedicalCodeItem
                    {
                        code = list_過敏藥碼[i],
                        name = list_過敏藥名[i]
                    });
                }
                for (int i = 0; i < list_交互作用藥碼.Count; i++)
                {
                    交互作用紀錄.Add(new MedicalCodeItem
                    {
                        code = list_交互作用藥碼[i],
                        name = list_交互作用[i]
                    });
                }
                patientDxClasses[0].診斷紀錄 = 診斷紀錄;
                patientDxClasses[0].過敏紀錄 = 過敏紀錄;
                patientDxClasses[0].交互作用紀錄 = 交互作用紀錄;

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = patientDxClasses;
                returnData.Result = $"取得{patientDxClasses.Count}筆資料";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        private string CheckCreatTable(Enum Enum)
        {
            SQLUI.Table table = new SQLUI.Table(Enum.GetEnumDescription());
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
            if (sys_serverSettingClasses.Count == 0)
            {
                return $"找無Server資料!";
            }
            table = MethodClass.CheckCreatTable(sys_serverSettingClasses[0], Enum);

            return table.JsonSerializationt(true);
        }
    }
}
