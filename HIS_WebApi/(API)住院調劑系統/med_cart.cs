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
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "medCpoeClass物件", typeof(medCpoeClass))]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "medCarInfoClass物件", typeof(medCarInfoClass))]
        public string init_med_carinfo([FromBody] returnData returnData)
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
        [HttpPost("init_med_cpoe_rec")]
        public string init_med_cpoe_rec([FromBody] returnData returnData)
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
                return CheckCreatTable(serverSettingClasses[0], new enum_med_cpoe_rec());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("init_med_info")]
        public string init_med_info([FromBody] returnData returnData)
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
                return CheckCreatTable(serverSettingClasses[0], new enum_med_info());
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
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
              
                List<medCarInfoClass> medCart_sql_add = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_replace = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_delete = new List<medCarInfoClass>();


                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                DateTime lestweek = DateTime.Now.AddDays(-7);
                DateTime yesterday = DateTime.Now.AddDays(-1);
                string starttime = lestweek.GetStartDate().ToDateString();
                string endtime = yesterday.GetEndDate().ToDateString();
                sQLControl_med_carInfo.DeleteByBetween(null, (int)enum_med_carInfo.更新時間, starttime, endtime);
               
                List<medCarInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<medCarInfoClass>>();              

                if (input_medCarInfo == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string 藥局 = input_medCarInfo[0].藥局;
                string 護理站 = input_medCarInfo[0].護理站;

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<medCarInfoClass> sql_medCar = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();


                for (int i = 0; i< input_medCarInfo.Count; i++)
                {
                    string 床號 = input_medCarInfo[i].床號;
                    
                    medCarInfoClass targetPatient = sql_medCar.FirstOrDefault(temp => temp.護理站 == 護理站 && temp.床號 == 床號);
                    
                    if (targetPatient == null)
                    {
                        input_medCarInfo[i].GUID = Guid.NewGuid().ToString();
                        medCart_sql_add.Add(input_medCarInfo[i]);
                    }
                    else
                    {                       
                        if (input_medCarInfo[i].病歷號 != targetPatient.病歷號)
                        {
                            medCarInfoClass medCarInfoClass = input_medCarInfo[i];
                            medCarInfoClass.GUID = Guid.NewGuid().ToString();
                            medCarInfoClass.異動 = "Y";
                            medCart_sql_add.Add(medCarInfoClass);
                            medCart_sql_delete.Add(targetPatient);
                        }
                        else
                        {
                            medCarInfoClass medCarInfoClass = input_medCarInfo[i];
                            medCarInfoClass.GUID = targetPatient.GUID;
                            if(string.IsNullOrWhiteSpace(medCarInfoClass.調劑狀態)) medCarInfoClass.調劑狀態 = targetPatient.調劑狀態;
                            medCart_sql_replace.Add(medCarInfoClass);
                        }
                    }
                }

                List<object[]> list_medCart_add = new List<object[]>();
                List<object[]> list_medCart_replace = new List<object[]>();
                List<object[]> list_medCart_delete = new List<object[]>();
                list_medCart_add = medCart_sql_add.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                list_medCart_replace = medCart_sql_replace.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                list_medCart_delete = medCart_sql_delete.ClassToSQL<medCarInfoClass, enum_med_carInfo>();

                if (list_medCart_add.Count > 0) sQLControl_med_carInfo.AddRows(null, list_medCart_add);
                if (list_medCart_replace.Count > 0) sQLControl_med_carInfo.UpdateByDefulteExtra(null, list_medCart_replace);
                if(list_medCart_delete.Count > 0)
                {
                    sQLControl_med_carInfo.DeleteExtra(null, list_medCart_delete);
                    List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                    List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                    List<medCpoeClass> filterCpoe = sql_medCpoe
                        .Where(cpoe => medCart_sql_delete.Any(medCart => medCart.GUID == cpoe.Master_GUID))
                        .ToList();
                    List<object[]> list_medCpoe_delete = filterCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);
                }

                List<object[]> list_bedList = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<medCarInfoClass> bedList = list_bedList.SQLToClass<medCarInfoClass, enum_med_carInfo>();

                List<medCarInfoClass>  medCarInfoClasses = (from temp in bedList
                                     where temp.護理站 == 護理站
                                     select temp).ToList();
                medCarInfoClasses.Sort(new medCarInfoClass.ICP_By_bedNum());

                //string API = $"http://{Server}:4436";
                //List<medCarInfoClass> update_medCarInfoClass = medCarInfoClass.update_med_carinfo(API, medCart_sql_replace);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"病床清單共{medCarInfoClasses.Count}筆";
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
            
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                DateTime lestweek = DateTime.Now.AddDays(-7);
                DateTime yesterday = DateTime.Now.AddDays(-1);
                string starttime = lestweek.GetStartDate().ToDateString();
                string endtime = yesterday.GetEndDate().ToDateString();
                sQLControl_med_cpoe.DeleteByBetween(null, (int)enum_med_cpoe.更新時間, starttime, endtime);

                List<medCpoeClass> input_medCpoe = returnData.Data.ObjToClass<List<medCpoeClass>>();

                if (input_medCpoe == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                string 藥局 = input_medCpoe[0].藥局;
                string 護理站 = input_medCpoe[0].護理站;

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();        

                List<medCpoeClass> medCpoe_sql_add = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_delete_buf = new List<medCpoeClass>();

                List<medCpoeClass> medCpoe_sql_buf = input_medCpoe
                    .GroupBy(medCart => medCart.Master_GUID)
                    .Select(group => group.First())
                    .ToList();
                
                for (int i = 0; i < medCpoe_sql_buf.Count; i++)
                {
                    string Master_GUID = medCpoe_sql_buf[i].Master_GUID;
                    medCarInfoClass targetPatient = sql_medCarInfo.FirstOrDefault(temp => temp.GUID == Master_GUID);

                    if (targetPatient == null)
                    {
                        returnData.Code = -200;
                        returnData.Result = "處方資料錯誤，請更新病床資訊";
                        return returnData.JsonSerializationt(true);
                    }
                    else
                    {
                        List<medCpoeClass> medCpoeClasses_new = (from temp in input_medCpoe
                                                                 where temp.Master_GUID == Master_GUID
                                                                 select temp).ToList();
                        List<medCpoeClass> medCpoeClasses_current = (from temp in sql_medCpoe
                                                                     where temp.Master_GUID == Master_GUID
                                                                     select temp).ToList();

                        if(medCpoeClasses_current == null) medCpoeClasses_current = new List<medCpoeClass>();
                        DateTime today = DateTime.Now.Date;                
                        foreach (var newMed in medCpoeClasses_new)
                        {
                            medCpoeClass Med_buf = medCpoeClasses_current.FirstOrDefault(temp => temp.序號 == newMed.序號);
                            if (Med_buf == null)
                            {
                                newMed.GUID = Guid.NewGuid().ToString();
                                medCpoe_sql_add.Add(newMed);
                            }                           
                        }
                        medCpoe_sql_delete_buf = (from temp in medCpoeClasses_current
                                              where !(from @new in medCpoeClasses_new
                                                      select @new.序號)
                                                      .Contains(temp.序號)
                                              select temp).ToList();
                        foreach(var medcpoe in medCpoe_sql_delete_buf)
                        {                         
                            medcpoe.數量 = "0";
                            medcpoe.劑量 = "0";
                            medcpoe.調劑狀態 = "";
                            medcpoe.排序 = "Z";
                            medCpoe_sql_replace.Add(medcpoe);                                                       
                        }
                    }       
                }
                List<object[]> list_medCpoe_add = new List<object[]>();
                List<object[]> list_medCpoe_replace = new List<object[]>();
                List<object[]> list_medCpoe_delete = new List<object[]>();

                list_medCpoe_add = medCpoe_sql_add.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_add.Count > 0) sQLControl_med_cpoe.AddRows(null, list_medCpoe_add);
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);

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
        ///更新處方大瓶點滴紀錄
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         
        ///         "ValueAry":[處方GUID]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_large_in_med_cpoe")]
        public string update_large_in_med_cpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_large_in_med_cpoe";
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
 
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];

                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, GUID);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                sql_medCpoe[0].大瓶點滴 = "L";
                        
                List<object[]>  list_medCpoe_replace = sql_medCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);

                List<object[]> list_med_cpoe_new = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, GUID);
                List<medCpoeClass> medCpoeClass_new = list_med_cpoe_new.SQLToClass<medCpoeClass, enum_med_cpoe>();
             
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCpoeClass_new;
                returnData.Result = $"更新處方中大瓶點滴紀錄成功";
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
        ///更新處方DC/NEW紀錄資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[medCpoeRecClass]
        ///         "ValueAry":["藥局","護理站"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_med_cpoe_rec")]
        public string update_med_cpoe_rec([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_cpoe_rec";
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

                List<medCpoeRecClass> input_medCpoe_rec = returnData.Data.ObjToClass<List<medCpoeRecClass>>();

                if (input_medCpoe_rec == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                DateTime lestweek = DateTime.Now.AddDays(-7);
                DateTime yesterday = DateTime.Now.AddDays(-1);
                string starttime = lestweek.GetStartDate().ToDateString();
                string endtime = yesterday.GetEndDate().ToDateString();
                SQLControl sQLControl_med_cpoe_rec = new SQLControl(Server, DB, "med_cpoe_rec", UserName, Password, Port, SSLMode);
                sQLControl_med_cpoe_rec.DeleteByBetween(null, (int)enum_med_cpoe_rec.更新時間, starttime, endtime);
             
                List<medCpoeClass> medCpoe_sql_add = new List<medCpoeClass>();
                List<object[]> list_medCpoe_add = input_medCpoe_rec.ClassToSQL<medCpoeRecClass, enum_med_cpoe_rec>();
                sQLControl_med_cpoe_rec.AddRows(null, list_medCpoe_add);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sQLControl_med_cpoe_rec;
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
        ///更新藥品資訊資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[medInfoClass]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_med_info")]
        public string update_med_info([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_info";
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

                List<medInfoClass> input_medInfoClass = returnData.Data.ObjToClass<List<medInfoClass>>();

                if (input_medInfoClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }                           
                SQLControl sQLControl_med_info = new SQLControl(Server, DB, "med_info", UserName, Password, Port, SSLMode);
                List<object[]> list_medInfo = sQLControl_med_info.GetAllRows(null);
                List<medInfoClass> sql_medInfo = list_medInfo.SQLToClass<medInfoClass, enum_med_info>();
                List<medInfoClass> add_medInfo = new List<medInfoClass>();
                List<medInfoClass> update_medInfo = new List<medInfoClass>();

                foreach (var medInfoClass in input_medInfoClass)
                {
                    List<medInfoClass> target = sql_medInfo
                        .Where(temp => temp.藥碼 == medInfoClass.藥碼)
                        .ToList();
                    if(target.Count == 0)
                    {
                        medInfoClass.GUID = Guid.NewGuid().ToString();
                        add_medInfo.Add(medInfoClass);
                    }
                    else if(target.Count == 1)
                    {
                        medInfoClass.GUID = target[0].GUID;
                        update_medInfo.Add(medInfoClass);
                    }           
                    else
                    {
                        returnData.Code = 200;
                        returnData.Result = $"資料重複，請確認資料庫";
                        return returnData.JsonSerializationt(true);
                    }
                }
                List<object[]> list_add_medInfo = add_medInfo.ClassToSQL<medInfoClass, enum_med_info>();
                List<object[]> list_update_medInfo = update_medInfo.ClassToSQL<medInfoClass, enum_med_info>();

                if (list_add_medInfo.Count > 0) sQLControl_med_info.AddRows(null, list_add_medInfo);
                if (list_update_medInfo.Count > 0) sQLControl_med_info.UpdateByDefulteExtra(null, list_add_medInfo);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = input_medInfoClass;
                returnData.Result = $"更新藥品資訊成功";
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
        ///以藥局和護理站取得占床資料
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
        [HttpPost("get_bed_list_by_cart")]
        public string get_bed_list_by_cart([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_bed_list_by_cart";
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

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();

                List<medCarInfoClass> medCarInfoClasses = (from temp in sql_medCarInfo
                                                           where temp.護理站 == 護理站
                                                           select temp).ToList();
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
        ///以藥局、護理站和床號取得病人詳細資料
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

                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                    returnData.Code = 200;
                    returnData.Result = "無對應的病人資料";
                    return returnData.JsonSerializationt(true);
                }
                string 住院號 = targetPatient.住院號;
                List<medCpoeClass> targetPatientCpoe = (from temp in sql_medCpoe
                                                        where temp.住院號 == 住院號
                                                        select temp).ToList();
                targetPatientCpoe.Sort(new medCpoeClass.ICP_By_Rank());
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
        ///以GUID取得病人詳細資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":"調劑台"
        ///         "ValueAry":[GUID]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_patient_by_GUID")]
        public string get_patient_by_GUID([FromBody] returnData returnData)
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
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];

                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.GUID, GUID);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, GUID);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                //string API = $"http://{Server}:4436";
                //medCarInfoClass targetPatient = HIS_DB_Lib.medCarInfoClass.get_patient_by_GUID(API, returnData.ValueAry);


                if (sql_medCarInfo == null)
                {
                    returnData.Code = 200;
                    returnData.Result = "無對應的病人資料";
                    return returnData.JsonSerializationt(true);
                }
                

                sql_medCpoe.Sort(new medCpoeClass.ICP_By_Rank());

                string 藥局 = sql_medCarInfo[0].藥局;
                string 護理站 = sql_medCarInfo[0].護理站;
                string 床號 = sql_medCarInfo[0].床號;

                
                if (!string.IsNullOrWhiteSpace(returnData.Value) && sql_medCpoe != null && returnData.Value != "all")
                {
                    //List<string> Codes = new List<string>();
                    //for (int i = 0; i < sql_medCpoe.Count; i++) Codes.Add(sql_medCpoe[i].藥碼);
                    List<string> Codes = sql_medCpoe.Select(temp => temp.藥碼).Distinct().ToList();
                    string API = $"http://{Server}:4436";
                    List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, returnData.Value, Codes);
            
                    foreach (var medCpoeClass in sql_medCpoe)
                    {
                        if (medClasses.Any(@new => @new.藥品碼 == medCpoeClass.藥碼))
                        {
                            medCpoeClass.調劑台 = "Y";
                        }
                    }

                }
                if(returnData.Value == "all")
                {
                    foreach (var medCpoeClass in sql_medCpoe) medCpoeClass.調劑台 = "Y";
                   
                }

                sql_medCarInfo[0].處方 = sql_medCpoe;
                var medCarInfoClass = new medCarInfoClass();
                medCarInfoClass = sql_medCarInfo[0];

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClass;
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
                

                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                //foreach(var medCarInfoClass in sql_medCarInfo)
                //{
                //    string abnorString = abnormal(medCarInfoClass);
                //}

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
        ///取得所有資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["病床GUID"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medChange_by_GUID")]
        public string get_medChange_by_GUID([FromBody] returnData returnData)
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
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                SQLControl sQLControl_med_cpoe_rec = new SQLControl(Server, DB, "med_cpoe_rec", UserName, Password, Port, SSLMode);
                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.GUID, GUID);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe_rec.GetRowsByDefult(null, (int)enum_med_cpoe_rec.Master_GUID, GUID);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeRecClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeRecClass, enum_med_cpoe_rec>();
                sql_medCpoe.Sort(new medCpoeRecClass.ICP_By_UP_Time());

                sql_medCarInfo[0].處方異動 = sql_medCpoe;

                string 藥局 = sql_medCarInfo[0].藥局;
                string 護理站 = sql_medCarInfo[0].護理站;
                string 床號 = sql_medCarInfo[0].床號;

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCarInfo;
                returnData.Result = $"取得{藥局} {護理站} 第{床號}床 處方異動資料共{sql_medCpoe.Count}筆";
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
        ///由藥碼取得藥品資訊
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["code"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medInfo_by_code")]
        public string get_medInfo_by_code([FromBody] returnData returnData)
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
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                string code = returnData.ValueAry[0];
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_info", UserName, Password, Port, SSLMode);
                List<object[]> list_med_info = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_info.藥碼, code);
                List<medInfoClass> sql_med_info = list_med_info.SQLToClass<medInfoClass, enum_med_info>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_med_info;
                returnData.Result = $"取得藥碼 {code} 的藥品資訊";
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
        ///         "Value":"病床GUID"
        ///             
        ///         "ValueAry":["藥品GUID;藥品GUID;藥品GUID"]
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
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
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
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"無調劑藥品";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"藥品GUID; 藥品GUID; 藥品GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.Value == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"請指定GUID";
                    return returnData.JsonSerializationt(true);
                }
                string Master_GUID = returnData.Value;
                string[] GUID = returnData.ValueAry[0].Split(";");
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_carinfo = new SQLControl(Server, DB, "med_carinfo", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                foreach(var medCpoeClass in sql_medCpoe)
                {
                    if (GUID.Contains(medCpoeClass.GUID))
                    {
                        medCpoeClass.調劑狀態 = "Y";
                        medCpoe_sql_replace.Add(medCpoeClass);
                    }
                    else
                    {
                        medCpoeClass.調劑狀態 = "";
                        medCpoe_sql_replace.Add(medCpoeClass);
                    }
                }
                List<object[]> list_medCpoe_replace = new List<object[]>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);

                bool allDispensed = medCpoe_sql_replace.All(med => med.調劑狀態 == "Y");
                List<object[]> list_med_carinfo = sQLControl_med_carinfo.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, Master_GUID);
                List<medCarInfoClass> sql_medCarinfo = list_med_carinfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                if (allDispensed) sql_medCarinfo[0].調劑狀態 = "Y";
                if(!allDispensed) sql_medCarinfo[0].調劑狀態 = "";
                    
                List<object[]> list_medCarInfo_replace = new List<object[]>();
                list_medCarInfo_replace = sql_medCarinfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                sQLControl_med_carinfo.UpdateByDefulteExtra(null, list_medCarInfo_replace);
                
                

                string API = $"http://{Server}:4436";
                List<string> ValueAry = new List<string> { Master_GUID };
                medCarInfoClass targetPatient = medCarInfoClass.get_patient_by_GUID(API, ValueAry);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = targetPatient;
                returnData.Result = $"更新處方紀錄";
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
        ///         "Value":"長青樓U1"
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
                if (returnData.Value == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Value 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                List<medCpoeClass> medCpoeClasses = (from temp in sql_medCpoe
                                                     where temp.護理站 == 護理站
                                                     select temp).ToList();
             
                List<medQtyClass> medQtyClasses = medCpoeClasses
                    .GroupBy(temp => temp.藥品名)
                    .Select(grouped => new medQtyClass
                    {
                        藥品名 = grouped.Key,
                        藥碼 = grouped.First().藥碼,
                        單位 = grouped.First().單位,
                        病床清單 = grouped.Select(value => new bedListClass
                        {
                            床號 = value.床號,
                            數量 = value.數量,
                            劑量 = value.劑量,
                            大瓶點滴 = value.大瓶點滴,
                            調劑狀態 = value.調劑狀態
                        }).ToList()
                    })
                    .ToList();
                List<string> codes = medQtyClasses.Select(temp => temp.藥碼).Distinct().ToList();
                string API = $"http://{Server}:4436";
                if(returnData.Value == "all")
                {
                    foreach (var medQtyClass in medQtyClasses)
                    {
                        medQtyClass.調劑台 = "Y";
                    }
                }
                else
                {
                    List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, returnData.Value, codes);
                    foreach (var medQtyClass in medQtyClasses)
                    {
                        if (medClasses.Any(@new => @new.藥品碼 == medQtyClass.藥碼))
                        {
                            medQtyClass.調劑台 = "Y";
                        }
                    }
                }
                foreach (var medQtyClass in medQtyClasses)
                {
                    List<bedListClass> bedListClasses = medQtyClass.病床清單;
                    bedListClasses.Sort(new bedListClass.ICP_By_bedNum());
                }
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medQtyClasses;
                returnData.Result = $"{藥局} {護理站} 的藥品清單";
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
        ///以藥局、護理站確認是否可交車
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
        [HttpPost("handover")]
        public string handover([FromBody] returnData returnData)
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

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_carlist = new SQLControl(Server, DB, "med_carlist", UserName, Password, Port, SSLMode);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetAllRows(null);
                List<object[]> list_med_carlist = sQLControl_med_carlist.GetRowsByDefult(null, (int)enum_med_carList.藥局, 藥局);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCarListClass> sql_medCarList = list_med_carlist.SQLToClass<medCarListClass, enum_med_carList>();

                List<medCpoeClass> medCpoeClasses = (from temp in sql_medCpoe
                                                     where temp.護理站 == 護理站
                                                     where temp.調劑狀態 ==""
                                                     select temp).ToList();
                var medCarList = sql_medCarList.FirstOrDefault(temp => temp.護理站 == 護理站);
                if (medCpoeClasses.Count != 0)
                {
                    int groupCount = medCpoeClasses
                        .GroupBy(temp => temp.床號)
                        .Count();
                    medCarList.備註 = $"{藥局} {護理站} 尚有{groupCount}床調劑未完成";
                    returnData.Code = 200;
                    returnData.Data = medCarList;
                    returnData.Result = $"未交車";
                    return returnData.JsonSerializationt(true);
                }
                medCarList.交車時間 = DateTime.Now.ToDateTimeString();
                medCarList.交車狀態 = "已交車";
                List<medCarListClass> medCarListClasses = new List<medCarListClass> { medCarList };
                List<object[]> medCarList_sql_update = new List<object[]>();
                medCarList_sql_update = medCarListClasses.ClassToSQL<medCarListClass, enum_med_carList>();
                sQLControl_med_carlist.UpdateByDefulteExtra(null, medCarList_sql_update);



                
                returnData.Code = 200;
                returnData.Data = medCarList;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"{藥局} {護理站} 交車完成";
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
        ///以藥碼搜尋存在所屬的調劑台
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":"code"
        ///         "ValueAry":["長青樓U1;長青樓U2;長青樓U3"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_dispens_by_code")]
        public string get_dispens_by_code([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "API01");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"長青樓U1;長青樓U2;長青樓U3;....\"]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"長青樓U1;長青樓U2;長青樓U3;....\"]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.Value == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Value 內容應為\"code\"";
                    return returnData.JsonSerializationt(true);
                }
                List<Task> tasks = new List<Task>();
                List<dispensClass> dispensClasses = new List<dispensClass>();
                List<string> code = new List<string> {returnData.Value};
                List<string> dispens = returnData.ValueAry[0].Split(";").ToList();
                string API = $"http://127.0.0.1:4436";
                object lockObj = new object();
                foreach (var disp in dispens)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, disp, code);
                        if (medClasses != null)
                        {
                            dispensClass dispensClass = new dispensClass
                            {
                                藥碼 = code[0],
                                ServerName = disp,
                                ServerType = "調劑台"
                            };
                            lock (lockObj)
                            {
                                dispensClasses.Add(dispensClass);
                            }
                        }
                    })));
                }
                Task.WhenAll(tasks).Wait();
                returnData.Data = dispensClasses;
                returnData.Code = 200;
                returnData.Result = $"藥碼{code[0]} 在{dispensClasses.Count}個調劑台裡有";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt(true);

            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }




    }

}
