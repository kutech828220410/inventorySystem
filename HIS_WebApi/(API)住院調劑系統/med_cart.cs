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

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class med_cart : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        ///初始化dbvm.med_carinfo資料庫
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
        [HttpPost("init_med_carinfo")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "medCarInfoClass物件", typeof(medCarInfoClass))]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "medCpoeClass物件", typeof(medCpoeClass))]
        public string init_med_carinfo([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0], new enum_med_carInfo());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }        
        /// <summary>
        ///初始化dbvm.med_qty資料庫
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
        [HttpPost("init_med_cpoe")]
        public string init_med_cpoe([FromBody] returnData returnData)
        {

            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0], new enum_med_cpoe());
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
        /// <summary>
        ///更新病床資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[medCarInfoClass]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_med_carinfo")]
        public string update_med_carinfo([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_carinfo";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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

                

                List<medCarInfoClass> medCart_sql_buf = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_add = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_replace = new List<medCarInfoClass>();
                List<medCarInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<medCarInfoClass>>();

                string 藥局 = input_medCarInfo[0].藥局;
                string 護理站 = input_medCarInfo[0].護理站;

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);

                List<medCarInfoClass> sql_medCar = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();


                if (input_medCarInfo == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                
                for(int i = 0; i< input_medCarInfo.Count; i++)
                {
                    string 床號 = input_medCarInfo[i].床號;
                    
                    medCarInfoClass targetPatient = sql_medCar.FirstOrDefault(temp => temp.護理站 == 護理站 && temp.床號 == 床號);
                    
                    if (targetPatient == null)
                    {
                        string GUID = Guid.NewGuid().ToString();
                        input_medCarInfo[i].GUID = GUID;
                        medCart_sql_add.Add(input_medCarInfo[i]);
                    }
                    else
                    {                       
                        medCarInfoClass medCarInfoClass = input_medCarInfo[i];
                        medCarInfoClass.GUID = targetPatient.GUID;
                        string 住院號 = medCarInfoClass.住院號;
                        List<medCpoeClass> targetCpoes = (from temp in sql_medCpoe
                                                          where temp.住院號 == 住院號
                                                          select temp).ToList();
                            
                        bool allDispensed = targetCpoes.All(med => med.調劑狀態 == "已調劑");
                        if (allDispensed) medCarInfoClass.調劑狀態 = "已全部調劑";
                        medCart_sql_replace.Add(medCarInfoClass);                                                                                   
                    }
                }

                List<object[]> list_medCart_add = new List<object[]>();
                List<object[]> list_medCart_replace = new List<object[]>();

                list_medCart_add = medCart_sql_add.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                list_medCart_replace = medCart_sql_replace.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                if (list_medCart_add.Count > 0) sQLControl_med_carInfo.AddRows(null, list_medCart_add);
                if (list_medCart_replace.Count > 0) sQLControl_med_carInfo.UpdateByDefulteExtra(null, list_medCart_replace);

                string 占床狀態 = "已佔床";
                List<object[]> list_bedList = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<medCarInfoClass> bedList = list_bedList.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCarInfoClass> medCarInfoClasses = new List<medCarInfoClass>();
                medCarInfoClasses = (from temp in bedList
                                     where temp.護理站 == 護理站
                                     where temp.占床狀態 == 占床狀態
                                     select temp).ToList();
                medCarInfoClasses.Sort(new medCarInfoClass.ICP_By_bedNum());


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"病床清單共筆";
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
        ///更新處方資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[medCpoeClass]
        ///         "ValueAry":["藥局","護理站"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_med_cpoe")]
        public string update_med_cpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_cpoe";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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

                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                List<medCpoeClass> medCpoe_sql_add = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_delete = new List<medCpoeClass>();

                List<medCpoeClass> input_medCpoe = returnData.Data.ObjToClass<List<medCpoeClass>>();

                if (input_medCpoe == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                List<medCpoeClass> medCpoe_sql_buf = input_medCpoe
                    .GroupBy(medCart => medCart.住院號)
                    .Select(group => group.First())
                    .ToList();
                

                for (int i = 0; i < medCpoe_sql_buf.Count; i++)
                {
                    string 住院號 = medCpoe_sql_buf[i].住院號;
                    medCarInfoClass targetPatient = sql_medCarInfo.FirstOrDefault(temp => temp.住院號 == 住院號);

                    if (targetPatient == null)
                    {
                        returnData.Code = -200;
                        returnData.Result = "處方資料錯誤，請更新病床資訊";
                        return returnData.JsonSerializationt(true);
                    }
                    else
                    {
                        List<medCpoeClass> medCpoeClasses_new = (from temp in input_medCpoe
                                                                 where temp.住院號 == 住院號
                                                                 select temp).ToList();
                        List<medCpoeClass> medCpoeClasses_current = (from temp in sql_medCpoe
                                                                     where temp.住院號 == 住院號
                                                                     select temp).ToList();
                        if(medCpoeClasses_current == null) medCpoeClasses_current = new List<medCpoeClass>();                   
                
                        foreach (var newMed in medCpoeClasses_new)
                        {
                            var Med_buf = medCpoeClasses_current.FirstOrDefault(temp => temp.藥品名 == newMed.藥品名);
                            if (Med_buf == null)
                            {
                                newMed.GUID = Guid.NewGuid().ToString();
                                newMed.Master_GUID = targetPatient.GUID;
                                medCpoe_sql_add.Add(newMed);
                            }
                            else
                            {
                                if (Med_buf.劑量 == newMed.劑量)
                                {
                                    newMed.GUID = Med_buf.GUID;
                                    newMed.Master_GUID = Med_buf.GUID;
                                    newMed.調劑狀態 = Med_buf.調劑狀態;                                  
                                }                                   
                                medCpoe_sql_replace.Add(newMed);
                            }
                        }
                        //medCpoe_sql_delete = medCpoeClasses_current.Where(current => !medCpoeClasses_new.Any(@new => @new.藥品名 == current.藥品名)).ToList();
                        medCpoe_sql_delete = (from temp in medCpoeClasses_current
                                              where !(from @new in medCpoeClasses_new
                                                      select @new.藥品名)
                                                      .Contains(temp.藥品名)
                                              select temp).ToList();


                    }       
                }

                List<object[]> list_medCpoe_add = new List<object[]>();
                List<object[]> list_medCpoe_replace = new List<object[]>();
                List<object[]> list_medCpoe_delete = new List<object[]>();

                list_medCpoe_add = medCpoe_sql_add.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                list_medCpoe_replace = medCpoe_sql_delete.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_add.Count > 0) sQLControl_med_cpoe.AddRows(null, list_medCpoe_add);
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_replace);

                List<object[]> list_med_cpoe_new = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                List<medCpoeClass> medCpoeClass_new = list_med_cpoe_new.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> medCpoeClasses = (from temp in medCpoeClass_new
                                                     where temp.護理站 == 護理站
                                                     select temp).ToList();
                                                 
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCpoeClasses;
                returnData.Result = $"更新處方資料表成功";
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
        ///以藥局、護理站和床號取得病人資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[藥局, 護理站, 床號]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_patient_by_bedNum")]
        public string get_patient_by_bedNum([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥局, 護理站, 床號]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];
                string 床號 = returnData.ValueAry[2];

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();


                var targetPatient = sql_medCarInfo.FirstOrDefault(temp => temp.護理站 == 護理站 && temp.床號 == 床號);
                if (targetPatient == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "無對應的病人資料";
                    return returnData.JsonSerializationt(true);
                }
                string 住院號 = targetPatient.住院號;
                List<medCpoeClass> targetPatientCpoe = (from temp in sql_medCpoe
                                                        where temp.住院號 == 住院號
                                                        select temp).ToList();
                targetPatient.處方 = targetPatientCpoe;              
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = targetPatient;
                returnData.Result = $"取得{藥局} {護理站} 第{床號}病床的資訊";
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
        ///以藥局和護理站取得資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[藥局, 護理站]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_patient_by_hnursta")]
        public string get_patient_by_hnursta([FromBody] returnData returnData)
        {
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
                    returnData.Result = $"returnData.ValueAry 內容應為[藥局, 護理站]";
                    return returnData.JsonSerializationt(true);
                }
               

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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

                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                List<medCarInfoClass> medCarInfoClasses = (from temp in sql_medCarInfo
                                 where temp.護理站 == 護理站
                                 select temp).ToList();
                foreach(var medCarInfoClass in medCarInfoClasses)
                {
                    string 住院號 = medCarInfoClass.住院號;
                    List<medCpoeClass> targetPatientCpoe = (from temp in sql_medCpoe
                                                            where temp.住院號 == 住院號
                                                            select temp).ToList();
                    medCarInfoClass.處方 = targetPatientCpoe;

                }

                medCarInfoClasses.Sort(new medCarInfoClass.ICP_By_bedNum());
              

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"取得{藥局} {護理站} 所有病人資訊";
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
        ///取得所有資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_all_med_carInfo")]
        public string get_all([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<medCarInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<medCarInfoClass>>();
                if (input_medCarInfo == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetAllRows(null);
                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCarInfo;
                returnData.Result = $"";
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
        ///確認藥品調劑
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[
        ///             {
        ///                 "phar": "藥車",
        ///                 "hnursta": "護理站",
        ///                 "hbedno": "床號"
        ///             }
        ///         ]
        ///         "ValueAry":["藥品名"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("check_dispense")]
        public string check_dispense([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "check_dispense";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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
                Table table = new Table(new enum_med_carInfo());
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, table.TableName, UserName, Password, Port, SSLMode);
                List<medCarInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<medCarInfoClass>>();

                if (input_medCarInfo == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                if (input_medCarInfo.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"無調劑藥品";
                    return returnData.JsonSerializationt(true);
                }

                string 藥局 = input_medCarInfo[0].藥局;
                string 護理站 = input_medCarInfo[0].護理站;
                string 床號 = input_medCarInfo[0].床號;

                List<object[]> list_medCart = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);

                List<medCarInfoClass> sql_medCar = list_medCart.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                var targetPatient = sql_medCar.FirstOrDefault(temp => temp.護理站 == 護理站 && temp.床號 == 床號);
                
                if (targetPatient == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"資料錯誤";
                    return returnData.JsonSerializationt(true);
                }
                //List<medCpoeClass> 處方 = targetPatient.處方.ObjToClass<List<medCpoeClass>>();
                List<medCpoeClass> medCpoeClasses = new List<medCpoeClass>();

                //int 處方數量 = 處方.Count;
                //int 處方已調劑數量 = 0;
                //for (int i = 0; i < 處方.Count; i++)
                //{
                //    medCpoeClass value = 處方[i];
                //    if (returnData.ValueAry.Contains(value.藥品名))
                //    {
                //        value.調劑狀態 = "已調劑";
                //        處方已調劑數量 += 1;
                //    }
                //    else
                //    {
                //        value.調劑狀態 = "";
                //    }
                //    medCpoeClasses.Add(value);
                //}
                //if (處方已調劑數量 == 處方數量)
                //{
                //    targetPatient.調劑狀態 = "已完成";
                //}
                //else
                //{
                //    targetPatient.調劑狀態 = "";
                //}

                //targetPatient.處方 = medCpoeClasses;
                //List<medCarInfoClass> medCarInfoClasses = new List<medCarInfoClass> { targetPatient };
                //List<object[]> list_medCart_replace = new List<object[]>();
                //list_medCart_replace = medCarInfoClasses.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                
                //if (list_medCart_replace.Count > 0)
                //{
                //    foreach (var medCarInfo in list_medCart_replace)
                //    {
                //        object[] value = medCarInfo;
                //        //value[(int)enum_med_carInfo.處方] = value[(int)enum_med_carInfo.處方].JsonSerializationt();
                //    }
                //    sQLControl_med_carInfo.UpdateByDefulteExtra(null, list_medCart_replace);
                //}

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = "list_medCart_replace";
                returnData.Result = $"更新{藥局} {護理站} 第{床號}床的處方紀錄";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception:{ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        ///以護理站取得藥品總量
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[藥局, 護理站]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_med_qty")]
        public string get_med_qty([FromBody] returnData returnData)
        {
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
                    returnData.Result = $"returnData.ValueAry 內容應為[藥局, 護理站]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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
                Table table_med_carInfo = new Table(new enum_med_carInfo());
                Table table_med_qty = new Table(new enum_藥品總量());
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, table_med_carInfo.TableName, UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_qty = new SQLControl(Server, DB, table_med_qty.TableName, UserName, Password, Port, SSLMode);

                List<object[]> list_medCart = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                //List<object[]> list_medCart = sQLControl_med_carInfo.GetAllRows(null);
                List<object[]> list_medQty = sQLControl_med_qty.GetRowsByDefult(null, (int)enum_藥品總量.藥局, 藥局);

                List<medCarInfoClass> sql_medCar = list_medCart.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medQtyClass> sql_medQty = list_medQty.SQLToClass<medQtyClass, enum_藥品總量>();
                List<medCarInfoClass> medCarInfoClasses = new List<medCarInfoClass>();
                List<medQtyClass> medQtyClasses = new List<medQtyClass>();
                List<medQtyClass> medQtyClass_sql_update = new List<medQtyClass>();
                List<medQtyClass> medQtyClass_sql_add = new List<medQtyClass>();


                medCarInfoClasses = (from temp in sql_medCar
                                 where temp.護理站 == 護理站
                                 select temp).ToList();
                medQtyClasses = (from temp in sql_medQty
                                 where temp.護理站 == 護理站
                                     select temp).ToList();

                //for (int i = 0; i < medCarInfoClasses.Count; i++)
                //{
                    
                //    string 床號 = medCarInfoClasses[i].床號;
                //    string 處方json = (string)(medCarInfoClasses[i].處方);
                //    if (string.IsNullOrEmpty(處方json)) continue;
                //    List<medCpoeClass> 處方;
                //    try
                //    {
                //        處方 = 處方json.JsonDeserializet<List<medCpoeClass>>();
                //    }
                //    catch
                //    {
                //        // 如果反序列化失敗，則繼續下一個迴圈
                //        continue;
                //    }

                //    // 檢查處方是否為null或空清單
                //    if (處方 == null || 處方.Count == 0) continue;
                //    for (int j = 0; j < 處方.Count; j++)
                //    {
                        
                //        string 藥品名 = 處方[j].藥品名;
                //        string 藥碼 = 處方[j].藥碼;
                //        string 單位 = 處方[j].單位;
                //        string 劑量 = 處方[j].劑量;
                //        string 數量 = 處方[j].數量;
                //        string 調劑狀態 = 處方[j].調劑狀態;

                //        List<medQtyClass> target_med = new List<medQtyClass>();
                //        target_med = (from temp in medQtyClasses
                //                      where temp.藥品名 == 藥品名
                //                      select temp).ToList();
                        


                //        //如果總量表裡沒有這個藥
                //        if (target_med.Count == 0)
                //        {
                //            string GUID = Guid.NewGuid().ToString();
                //            bedListClass bedListClass = new bedListClass
                //            {
                //                床號 = 床號,
                //                數量 = 數量,
                //                劑量 = 劑量,
                //                調劑狀態 = 調劑狀態
                //            };
                //            int 已調劑數量 = 0;
                //            int 已調劑劑量 = 0;
                //            if (調劑狀態 == "已調劑") 已調劑數量 += 數量.StringToInt32();
                //            if (調劑狀態 == "已調劑") 已調劑劑量 += 劑量.StringToInt32();
                //            medQtyClass medQtyClass = new medQtyClass
                //            {
                //                GUID = GUID,
                //                藥局 = 藥局,
                //                護理站 = 護理站,
                //                藥品名 = 藥品名,
                //                藥碼 = 藥碼,
                //                總數量 = 數量,
                //                已調劑數量 = 已調劑數量.ToString(),
                //                總劑量 = 劑量,
                //                已調劑劑量 = 已調劑劑量.ToString(),
                //                單位 = 單位,
                //                病床清單 = bedListClass
                //            };
                //            medQtyClass_sql_add.Add(medQtyClass);
                //        }
                //        //藥品清單有藥
                //        else
                //        {
                //            if (target_med.Count != 1)
                //            {
                //                returnData.Code = -200;
                //                returnData.Result = "藥品資料錯誤";
                //                return returnData.JsonSerializationt();
                //            }
                //            medQtyClass medQtyClass = target_med[0];
                //            List<bedListClass> bedListClasses = ((string)medQtyClass.病床清單).JsonDeserializet<List<bedListClass>>();
                //            List<bedListClass> target_bed = new List<bedListClass>();
                //            target_bed = (from temp in bedListClasses
                //                          where temp.床號 == 床號
                //                          select temp).ToList();
                //            if (target_bed.Count == 0)
                //            {
                //                bedListClass bedListClass = new bedListClass
                //                {
                //                    床號 = 床號,
                //                    數量 = 數量,
                //                    劑量 = 劑量,
                //                    調劑狀態 = 調劑狀態
                //                };                  
                //            }
                //            else
                //            {
                //                bedListClass bedListClass = target_bed[0];
                //                bedListClass.數量 = 數量;
                //                bedListClass.劑量 = 劑量;
                //                bedListClass.調劑狀態 = 調劑狀態;
                //            }
                //            medQtyClass_sql_update.Add(medQtyClass);
                //        }
                //    }
                //    if (medQtyClass_sql_add.Count > 0)
                //    {
                //        List<object[]> list_medQty_add = new List<object[]>();
                //        list_medQty_add = medQtyClass_sql_add.ClassToSQL<medQtyClass, enum_藥品總量>();
                //        sQLControl_med_qty.AddRows(null, list_medQty_add);
                //        list_medQty = sQLControl_med_qty.GetRowsByDefult(null, (int)enum_藥品總量.藥局, 藥局);
                //        sql_medQty = list_medQty.SQLToClass<medQtyClass, enum_藥品總量>();
                //        medQtyClasses = new List<medQtyClass>();
                //        medQtyClasses = (from temp in sql_medQty
                //                         where temp.護理站 == 護理站
                //                         select temp).ToList();
                //    }
                //}
                //for (int i =0; i < medQtyClass_sql_update.Count; i++)
                //{
                //    medQtyClass medQtyClass = medQtyClass_sql_update[i];
                //    List<bedListClass> bedListClasses = ((string)medQtyClass.病床清單).JsonDeserializet<List<bedListClass>>();
                //    int 已調劑劑量 = 0;
                //    int 已調劑數量 = 0;
                //    int 總劑量 = 0;
                //    int 總數量 = 0;

                //    for(int j = 0; j < bedListClasses.Count; j++)
                //    {
                //        總劑量 += (bedListClasses[j].劑量).StringToInt32();
                //        總數量 += (bedListClasses[j].數量).StringToInt32();
                //        if (bedListClasses[j].調劑狀態 == "已調劑")
                //        {
                //            已調劑劑量 += (bedListClasses[j].劑量).StringToInt32();
                //            已調劑數量 += (bedListClasses[j].數量).StringToInt32();
                //        }
                //    }
                //    medQtyClass_sql_update[i].總劑量 = 總劑量.ToString();
                //    medQtyClass_sql_update[i].已調劑劑量 = 已調劑劑量.ToString();
                //    medQtyClass_sql_update[i].總數量 = 總數量.ToString();
                //    medQtyClass_sql_update[i].已調劑數量 = 已調劑數量.ToString();
                //}

                //List<object[]> list_medQty_update = new List<object[]>();
                //list_medQty_update = medQtyClass_sql_update.ClassToSQL<medQtyClass, enum_藥品總量>();
                //sQLControl_med_qty.UpdateByDefulteExtra(null, list_medQty_update);

                //returnData.Code = 200;
                //returnData.TimeTaken = $"{myTimerBasic}";
                //returnData.Data = "";
                //returnData.Result = $"";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("ttt")]
        public string ttt([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥局, 護理站, 床號]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];
                string 床號 = returnData.ValueAry[2];

                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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
                Table table = new Table(new enum_med_carInfo());
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, table.TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_medCart = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                foreach( var row in list_medCart)
                {
                    //List<medCpoeClass> medCpoeClasses = ((string)row[(int)enum_med_carInfo.處方]).JsonDeserializet<List<medCpoeClass>>();
                    //row[(int)enum_med_carInfo.處方] = medCpoeClasses;
                }
                List<medCarInfoClass> sql_medCar = list_medCart.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                var targetPatient = sql_medCar.FirstOrDefault(temp => temp.護理站 == 護理站 && temp.床號 == 床號);
                if (targetPatient == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "無對應的病人資料";
                    return returnData.JsonSerializationt(true);
                }
                //targetPatient.檢驗結果 = targetPatient.檢驗結果.
                //targetPatient.處方 = targetPatient.處方.JsonSerializationt();
                //targetPatient.處方 = (targetPatient.處方).ObjToClass<List<medCpoeClass>>();
                //targetPatient.檢驗結果 = (targetPatient.檢驗結果).ObjToClass<List<testResult>>();

                //targetPatient.處方 = ((string)targetPatient.處方).JsonDeserializet<List<medCpoeClass>>();

                //targetPatient.檢驗結果 = ((string)targetPatient.檢驗結果).JsonDeserializet<List<testResult>>();
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = targetPatient;
                returnData.Result = $"取得{藥局} {護理站} 第{床號}病床的資訊";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("add")]
        public string add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {               
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "VM端");
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
                Table table = new Table(new enum_med_carInfo());
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, table.TableName, UserName, Password, Port, SSLMode);

                List<medCarInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<medCarInfoClass>>();
                foreach (var medCarInfoClass in input_medCarInfo)
                {
                    string GUID = Guid.NewGuid().ToString();
                    medCarInfoClass.GUID = GUID;
                }
                List<object[]> obj_medCartInfo = new List<object[]>();
                obj_medCartInfo = input_medCarInfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                //foreach (var medCarInfo in obj_medCartInfo)
                //{
                //    object[] value = medCarInfo;
                //    value[(int)enum_med_carInfo.處方] = value[(int)enum_med_carInfo.處方].JsonSerializationt();
                //}
               
                sQLControl_med_carInfo.AddRows(null, obj_medCartInfo);

                List<object[]> list_medCart = sQLControl_med_carInfo.GetAllRows(null);
               

                List<medCarInfoClass> medCarInfoClasses = list_medCart.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Data = "";
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }

    }

}
