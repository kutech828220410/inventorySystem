using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Drawing;
using System.Text;
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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using DrawingClass;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class materialRequisition : Controller
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
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(materialRequisitionClass))]
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

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [materialRequisitionClass]
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "add";
            try
            {
                GET_init(returnData);
                List<ServerSettingClass> serverSettingClasses =ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                List<materialRequisitionClass> materialRequisitionClasses = returnData.Data.ObjToClass<List<materialRequisitionClass>>();
                List<materialRequisitionClass> materialRequisitionClasses_buf = new List<materialRequisitionClass>();
                if (materialRequisitionClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料異常!";
                    return returnData.JsonSerializationt();
                }

                for (int i = 0; i < materialRequisitionClasses.Count; i++)
                {
                    if (materialRequisitionClasses[i].藥碼.StringIsEmpty()) continue;
                    materialRequisitionClasses[i].GUID = Guid.NewGuid().ToString();
                    materialRequisitionClasses[i].申領時間 = DateTime.Now;
                    materialRequisitionClasses[i].核撥時間 = DateTime.MinValue;
                    materialRequisitionClasses[i].申領庫庫存 = "";
                    materialRequisitionClasses[i].申領庫結存 = "";
                    materialRequisitionClasses[i].實撥庫庫存 = "";
                    materialRequisitionClasses[i].實撥庫結存 = "";
                    materialRequisitionClasses[i].狀態 = "等待過帳";
                    materialRequisitionClasses_buf.Add(materialRequisitionClasses[i]);
                }
                List<object[]> list_value = materialRequisitionClasses_buf.ClassToSQL<materialRequisitionClass, enum_materialRequisition>();
                Table table = new Table(new enum_materialRequisition());
                SQLControl sQLControl_materialRequisition = new SQLControl(Server, DB, table.TableName, UserName, Password, Port, SSLMode);
                sQLControl_materialRequisition.AddRows(null, list_value);
                returnData.Code = 200;
                returnData.Result = $"新增申領資料共<{list_value.Count}>筆";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = materialRequisitionClasses_buf;
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
        /// 根據申領時間獲取申領單
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [materialRequisitionClass]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///        "起始時間",
        ///        "結束時間"
        ///     ]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("get_by_requestTime")]
        [HttpPost]
        public string POST_get_by_requestTime([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_requestTime";
            try
            {
                GET_init(returnData);
                List<ServerSettingClass> serverSettingClasses =ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[起始時間][結束時間]";
                    return returnData.JsonSerializationt(true);
                }
                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];
                if (起始時間.Check_Date_String() == false || 結束時間.Check_Date_String() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"時間範圍格式錯誤";
                    return returnData.JsonSerializationt(true);
                }
                DateTime dateTime_st = 起始時間.StringToDateTime();
                DateTime dateTime_end = 結束時間.StringToDateTime();

                Table table = new Table(new enum_materialRequisition());
                SQLControl sQLControl_materialRequisition = new SQLControl(Server, DB, table.TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_materialRequisition.GetRowsByBetween(null, (int)enum_materialRequisition.申領時間, 起始時間, 結束時間);
                List<materialRequisitionClass> materialRequisitionClasses = list_value.SQLToClass<materialRequisitionClass, enum_materialRequisition>();
                List<materialRequisitionClass> materialRequisitionClasses_buf = new List<materialRequisitionClass>();
                if (materialRequisitionClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料取得異常";
                    return returnData.JsonSerializationt();
                }
                if (materialRequisitionClasses.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"查無資料!";
                    returnData.Data = materialRequisitionClasses;
                    return returnData.JsonSerializationt();
                }


                returnData.Code = 200;
                returnData.Result = $"取得申領資料共<{materialRequisitionClasses.Count}>筆";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Data = materialRequisitionClasses;
                return returnData.JsonSerializationt();
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
            Table table = MethodClass.CheckCreatTable(serverSettingClass, new enum_materialRequisition());
            return table.JsonSerializationt(true);
        }
    }
}
