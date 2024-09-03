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

namespace HIS_WebApi._API_藥品交互作用
{
    [Route("api/[controller]")]
    [ApiController]
    public class med_interaction : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        ///初始化dbvm.med_Inter資料庫
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
        [HttpPost("init_med_Inter")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "medInterClass物件", typeof(medInterClass))]
        public string init_med_Inter([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
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
                return CheckCreatTable(serverSettingClasses[0], new enum_med_Inter());
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
        [HttpPost("get_med_inter")]
        public string get_med_inter([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if(serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_med_inter = new SQLControl(Server, DB, "med_inter", UserName, Password, Port, SSLMode);
                List<object[]> list_med_inter = sQLControl_med_inter.GetAllRows(null);
                List<medInterClass> sql_medInter = list_med_inter.SQLToClass<medInterClass, enum_med_Inter>();


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medInter;
                returnData.Result = $"取得Table {DB}.med_inter";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("update_med_inter")]
        public string update_med_inter ([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_med_inter = new SQLControl(Server, DB, "med_inter", UserName, Password, Port, SSLMode);
                List<object[]> list_med_inter_sql = sQLControl_med_inter.GetAllRows(null);
                List<medInterClass> current_medInterClass = list_med_inter_sql.SQLToClass <medInterClass, enum_med_Inter> ();
                List<medInterClass> add_medInterClass = new List<medInterClass>();
                List<medInterClass> update_medInterClass = new List<medInterClass>();


                List<medInterClass> input_medInterClass = returnData.Data.ObjToClass<List<medInterClass>>();
                if (input_medInterClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                foreach(var medInterClass in input_medInterClass)
                {
                    List<medInterClass> target_medInterClass = current_medInterClass.Where(temp => temp.ATC == medInterClass.ATC).ToList();
                    if(target_medInterClass.Count == 0)
                    {
                        medInterClass.GUID = Guid.NewGuid().ToString();
                        add_medInterClass.Add(medInterClass);
                    }
                    else
                    {
                        medInterClass.GUID = target_medInterClass[0].GUID;
                        update_medInterClass.Add(medInterClass);
                    }
                }

                List<object[]> add_med_inter = add_medInterClass.ClassToSQL<medInterClass, enum_med_Inter>();
                List<object[]> update_med_inter = update_medInterClass.ClassToSQL<medInterClass, enum_med_Inter>();
                if (add_med_inter.Count > 0) sQLControl_med_inter.AddRows(null, add_med_inter);
                if (update_med_inter.Count > 0) sQLControl_med_inter.UpdateByDefulteExtra(null, update_med_inter);


                returnData.Code = -200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = current_medInterClass;
                returnData.Result = $"更新Table {DB}.med_inter 共{input_medInterClass.Count}筆";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }

        [HttpPost("add_med_inter")]
        public string add_med_inter([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_med_inter = new SQLControl(Server, DB, "med_inter", UserName, Password, Port, SSLMode);

                List<medInterClass> input_medInterClass = returnData.Data.ObjToClass<List<medInterClass>>();
                if (input_medInterClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                List<object[]> list_med_inter = input_medInterClass.ClassToSQL<medInterClass, enum_med_Inter>();
                sQLControl_med_inter.AddRows(null, list_med_inter);

                returnData.Code = -200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = input_medInterClass;
                returnData.Result = $"更新Table {DB}.med_inter 共{input_medInterClass.Count}筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
    }
}
