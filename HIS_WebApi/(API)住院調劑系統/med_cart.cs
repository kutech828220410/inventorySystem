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





// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class med_cart : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        
        /// <summary>
        ///初始化dbvm.patient_info資料庫
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
        [HttpPost("init_patient_info")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "medCpoeClass物件", typeof(medCpoeClass))]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "patientInfoClass物件", typeof(patientInfoClass))]
        public string init_med_carinfo([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_cart/init_patient_info";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_patient_info());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }        
        [HttpPost("init_med_cpoe")]
        public string init_med_cpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_cart/init_med_cpoe";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_med_cpoe());
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
            returnData.Method = "med_cart/init_med_cpoe_rec";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_med_cpoe_rec());
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
            returnData.Method = "med_cart/init_med_info";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_med_info());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("init_bed_status")]
        public string init_bed_status([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_cart/init_bed_status";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_bed_status());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        ///更新病床病人資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[patientInfoClass]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        //[HttpPost("update_patientInfo")]
        //public string update_patientInfo([FromBody] returnData returnData)
        //{
        //    MyTimerBasic myTimerBasic = new MyTimerBasic();
        //    returnData.Method = "update_patientInfo";
        //    try
        //    {
        //        (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
        //        string API = GetServerAPI("Main", "網頁", "API01");

        //        List<patientInfoClass> patInfo_sql_add = new List<patientInfoClass>();
        //        List<patientInfoClass> patInfo_sql_update = new List<patientInfoClass>();
        //        List<patientInfoClass> patInfo_sql_delete = new List<patientInfoClass>();


        //        SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
        //        SQLControl sQLControl_order = new SQLControl(Server, DB, "order_list", UserName, Password, Port, SSLMode);

        //        DateTime lestweek = DateTime.Now.AddDays(-30);
        //        DateTime yesterday = DateTime.Now.AddDays(-0);
        //        string starttime = lestweek.GetStartDate().ToDateString();
        //        string endtime = yesterday.GetEndDate().ToDateString();
        //        sQLControl_patient_info.DeleteByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);

        //        List<patientInfoClass> input_patientInfo = returnData.Data.ObjToClass<List<patientInfoClass>>();

        //        if (input_patientInfo == null)
        //        {
        //            returnData.Code = -200;
        //            returnData.Result = $"傳入Data資料異常";
        //            return returnData.JsonSerializationt();
        //        }
        //        string 藥局 = input_patientInfo[0].藥局;
        //        string 護理站 = input_patientInfo[0].護理站;

        //        List<object[]> list_patient_info = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.護理站, 護理站);
        //        List<patientInfoClass> patientInfo = list_patient_info.SQLToClass<patientInfoClass, enum_patient_info>();
        //        //List<patientInfoClass> medCarInfo = sql_medCar.Where(temp => temp.護理站 == 護理站).ToList();
        //        Dictionary<string, List<patientInfoClass>> patientInfoDictBedNum = patientInfoClass.ToDictByBedNum(patientInfo);



        //        List<Task> tasks = new List<Task>();

        //        foreach (patientInfoClass patientInfoClass in input_patientInfo)
        //        {
        //            tasks.Add(Task.Run(new Action(delegate
        //            {
        //                string 床號 = patientInfoClass.床號;
        //                List<patientInfoClass> patientInfoClass_buff = patientInfoClass.GetDictByBedNum(patientInfoDictBedNum, 床號);
        //                if(patientInfoClass_buff != null)
        //                {
        //                    patientInfoClass targetPatient = patientInfoClass_buff[0];
        //                    if (patientInfoClass.PRI_KEY != targetPatient.PRI_KEY)
        //                    {
        //                        patientInfoClass.GUID = Guid.NewGuid().ToString();
        //                        patientInfoClass.異動 = "Y";

        //                        patInfo_sql_add.LockAdd(patientInfoClass);
        //                        patInfo_sql_delete.LockAdd(targetPatient);
        //                    }
        //                    else
        //                    {
        //                        patientInfoClass.GUID = targetPatient.GUID;
        //                        patientInfoClass.調劑狀態 = targetPatient.調劑狀態;
        //                        patientInfoClass.調劑時間 = targetPatient.調劑時間;
        //                        patientInfoClass.覆核狀態 = targetPatient.覆核狀態;

        //                        patInfo_sql_update.LockAdd(patientInfoClass);
        //                    }
        //                }
        //                else
        //                {
        //                    patientInfoClass.GUID = Guid.NewGuid().ToString();
        //                    patInfo_sql_add.LockAdd(patientInfoClass);
        //                }                                             
        //            })));
        //        }
        //        Task.WhenAll(tasks).Wait();




        //        List<object[]> list_patInfo_add = new List<object[]>();
        //        List<object[]> list_patInfo_replace = new List<object[]>();
        //        List<object[]> list_patInfo_delete = new List<object[]>();
        //        list_patInfo_add = patInfo_sql_add.ClassToSQL<patientInfoClass, enum_patient_info>();
        //        list_patInfo_replace = patInfo_sql_update.ClassToSQL<patientInfoClass, enum_patient_info>();
        //        list_patInfo_delete = patInfo_sql_delete.ClassToSQL<patientInfoClass, enum_patient_info>();

        //        if (list_patInfo_add.Count > 0) sQLControl_patient_info.AddRows(null, list_patInfo_add);
        //        if (list_patInfo_replace.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, list_patInfo_replace);
        //        if (list_patInfo_delete.Count > 0)
        //        {
        //            sQLControl_patient_info.DeleteExtra(null, list_patInfo_delete);
        //            List<object[]> list_order = sQLControl_order.GetRowsByDefult(null, (int)enum_醫囑資料.病房, 護理站);
        //            List<OrderClass> sql_order = list_order.SQLToClass<OrderClass, enum_醫囑資料>();
        //            List<OrderClass> filterOrder = sql_order
        //                .Where(order => patInfo_sql_delete.Any(patInfo => patInfo.GUID == order.PRI_KEY)).ToList();
        //            List<object[]> list_order_delete = filterOrder.ClassToSQL<OrderClass, enum_醫囑資料>();
        //            if (list_order_delete.Count > 0) sQLControl_order.DeleteExtra(null, list_order_delete);
        //        }

        //        List<object[]> list_patInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.護理站, 護理站);
        //        List<patientInfoClass> patientInfoClasses = list_patInfo.SQLToClass<patientInfoClass, enum_patient_info>();
        //        patientInfoClasses.Sort(new patientInfoClass.ICP_By_bedNum());

        //        returnData.Code = 200;
        //        returnData.TimeTaken = $"{myTimerBasic}";
        //        returnData.Data = patientInfoClasses;
        //        returnData.Result = $"病床清單共{patientInfoClasses.Count}筆";
        //        return returnData.JsonSerializationt(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData.Code = -200;
        //        returnData.Result = ex.Message;
        //        return returnData.JsonSerializationt(true);
        //    }
        //}
        [HttpPost("update_patientInfo")]
        public string update_patientInfo([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_carinfo";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string API = GetServerAPI("Main", "網頁", "API01");

                List<patientInfoClass> medCart_sql_add = new List<patientInfoClass>();
                List<patientInfoClass> medCart_sql_replace = new List<patientInfoClass>();
                List<patientInfoClass> medCart_sql_delete = new List<patientInfoClass>();


                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                
                //sQLControl_patient_info.DeleteByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);

                List<patientInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<patientInfoClass>>();

                if (input_medCarInfo == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string 藥局 = input_medCarInfo[0].藥局;
                string 護理站 = input_medCarInfo[0].護理站;

                DateTime today = DateTime.Now;
                string starttime = today.GetStartDate().ToDateString();
                string endtime = today.GetEndDate().ToDateString();

                //List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.藥局, 藥局);
                //List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);
                //List<object[]> list_pat_info = sQLControl_patient_info.GetAllRows(null);

                //List<patientInfoClass> sql_patInfo = list_pat_info.SQLToClass<patientInfoClass, enum_patient_info>();
                //sql_patInfo = (from temp in sql_patInfo
                //               where temp.更新時間.StringToDateTime() >= DateTime.Now.GetStartDate()
                //               where temp.更新時間.StringToDateTime() <= DateTime.Now.GetEndDate()
                //               select temp).ToList();
                List<patientInfoClass> sql_patInfo = GetPatInfo(sQLControl_patient_info);
                List<patientInfoClass> medCarInfo = sql_patInfo.Where(temp => temp.護理站 == 護理站).ToList();
                Dictionary<string, List<patientInfoClass>> medCarInfoDictBedNum = patientInfoClass.ToDictByBedNum(medCarInfo);



                List<Task> tasks = new List<Task>();

                foreach (patientInfoClass patientInfoClass in input_medCarInfo)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        patientInfoClass targetPatient = new patientInfoClass();

                        string 床號 = patientInfoClass.床號;
                        if (patientInfoClass.GetDictByBedNum(medCarInfoDictBedNum, 床號).Count != 0)
                        {
                            targetPatient = patientInfoClass.GetDictByBedNum(medCarInfoDictBedNum, 床號)[0];
                        }
                        if (targetPatient.GUID.StringIsEmpty() == true)
                        {
                            patientInfoClass.GUID = Guid.NewGuid().ToString();
                            medCart_sql_add.LockAdd(patientInfoClass);
                        }
                        else
                        {
                            if (patientInfoClass.PRI_KEY != targetPatient.PRI_KEY)
                            {
                                patientInfoClass.GUID = Guid.NewGuid().ToString();
                                patientInfoClass.異動 = "Y";
                                medCart_sql_add.LockAdd(patientInfoClass);
                                medCart_sql_delete.LockAdd(targetPatient);
                            }
                            else
                            {
                                patientInfoClass.GUID = targetPatient.GUID;
                                patientInfoClass.調劑狀態 = targetPatient.調劑狀態;
                                medCart_sql_replace.LockAdd(patientInfoClass);
                            }
                        }
                    })));
                }
                Task.WhenAll(tasks).Wait();




                List<object[]> list_medCart_add = new List<object[]>();
                List<object[]> list_medCart_replace = new List<object[]>();
                List<object[]> list_medCart_delete = new List<object[]>();
                list_medCart_add = medCart_sql_add.ClassToSQL<patientInfoClass, enum_patient_info>();
                list_medCart_replace = medCart_sql_replace.ClassToSQL<patientInfoClass, enum_patient_info>();
                list_medCart_delete = medCart_sql_delete.ClassToSQL<patientInfoClass, enum_patient_info>();

                if (list_medCart_add.Count > 0) sQLControl_patient_info.AddRows(null, list_medCart_add);
                if (list_medCart_replace.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCart_replace);
                if (list_medCart_delete.Count > 0)
                {
                    sQLControl_patient_info.DeleteExtra(null, list_medCart_delete);
                    List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                    List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                    List<medCpoeClass> filterCpoe = sql_medCpoe
                        .Where(cpoe => medCart_sql_delete.Any(medCart => medCart.GUID == cpoe.Master_GUID)).ToList();
                    List<object[]> list_medCpoe_delete = filterCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);
                }

                List<object[]> list_bedList = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.藥局, 藥局);
                List<patientInfoClass> bedList = list_bedList.SQLToClass<patientInfoClass, enum_patient_info>();
                List<patientInfoClass> patientInfoClasses = bedList.Where(temp => temp.護理站 == 護理站).ToList();
                patientInfoClasses.Sort(new patientInfoClass.ICP_By_bedNum());

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = patientInfoClasses;
                returnData.Result = $"病床清單共{patientInfoClasses.Count}筆";
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
        ///更新病床資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[patientInfoClass]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        //[HttpPost("update_med_carinfo")]
        //public string update_med_carinfo([FromBody] returnData returnData)
        //{
        //    MyTimerBasic myTimerBasic = new MyTimerBasic();
        //    returnData.Method = "update_med_carinfo";
        //    try
        //    {
        //        (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
        //        string API = GetServerAPI("Main", "網頁", "API01");

        //        List<patientInfoClass> medCart_sql_add = new List<patientInfoClass>();
        //        List<patientInfoClass> medCart_sql_replace = new List<patientInfoClass>();
        //        List<patientInfoClass> medCart_sql_delete = new List<patientInfoClass>();


        //        SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
        //        SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

        //        DateTime lestweek = DateTime.Now.AddDays(-30);
        //        DateTime yesterday = DateTime.Now.AddDays(-0);
        //        string starttime = lestweek.GetStartDate().ToDateString();
        //        string endtime = yesterday.GetEndDate().ToDateString();
        //        sQLControl_patient_info.DeleteByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);

        //        List<patientInfoClass> input_medCarInfo = returnData.Data.ObjToClass<List<patientInfoClass>>();

        //        if (input_medCarInfo == null)
        //        {
        //            returnData.Code = -200;
        //            returnData.Result = $"傳入Data資料異常";
        //            return returnData.JsonSerializationt();
        //        }
        //        string 藥局 = input_medCarInfo[0].藥局;
        //        string 護理站 = input_medCarInfo[0].護理站;

        //        List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.藥局, 藥局);
        //        List<patientInfoClass> sql_medCar = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
        //        List<patientInfoClass> medCarInfo = sql_medCar.Where(temp => temp.護理站 == 護理站).ToList();
        //        Dictionary<string, List<patientInfoClass>> medCarInfoDictBedNum = patientInfoClass.ToDictByBedNum(medCarInfo);



        //        List<Task> tasks = new List<Task>();

        //        foreach (patientInfoClass patientInfoClass in input_medCarInfo)
        //        {
        //            tasks.Add(Task.Run(new Action(delegate
        //            {
        //                patientInfoClass targetPatient = new patientInfoClass();

        //                string 床號 = patientInfoClass.床號;
        //                if (patientInfoClass.GetDictByBedNum(medCarInfoDictBedNum, 床號).Count != 0)
        //                {
        //                    targetPatient = patientInfoClass.GetDictByBedNum(medCarInfoDictBedNum, 床號)[0];
        //                }
        //                if (targetPatient.GUID.StringIsEmpty() == true)
        //                {
        //                    patientInfoClass.GUID = Guid.NewGuid().ToString();
        //                    medCart_sql_add.LockAdd(patientInfoClass);
        //                }
        //                else
        //                {
        //                    if (patientInfoClass.PRI_KEY != targetPatient.PRI_KEY)
        //                    {
        //                        patientInfoClass.GUID = Guid.NewGuid().ToString();
        //                        patientInfoClass.異動 = "Y";
        //                        medCart_sql_add.LockAdd(patientInfoClass);
        //                        medCart_sql_delete.LockAdd(targetPatient);
        //                    }
        //                    else
        //                    {
        //                        patientInfoClass.GUID = targetPatient.GUID;
        //                        patientInfoClass.調劑狀態 = targetPatient.調劑狀態;
        //                        medCart_sql_replace.LockAdd(patientInfoClass);
        //                    }
        //                }
        //            })));
        //        }
        //        Task.WhenAll(tasks).Wait();




        //        List<object[]> list_medCart_add = new List<object[]>();
        //        List<object[]> list_medCart_replace = new List<object[]>();
        //        List<object[]> list_medCart_delete = new List<object[]>();
        //        list_medCart_add = medCart_sql_add.ClassToSQL<patientInfoClass, enum_patient_info>();
        //        list_medCart_replace = medCart_sql_replace.ClassToSQL<patientInfoClass, enum_patient_info>();
        //        list_medCart_delete = medCart_sql_delete.ClassToSQL<patientInfoClass, enum_patient_info>();

        //        if (list_medCart_add.Count > 0) sQLControl_patient_info.AddRows(null, list_medCart_add);
        //        if (list_medCart_replace.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCart_replace);
        //        if (list_medCart_delete.Count > 0)
        //        {
        //            sQLControl_patient_info.DeleteExtra(null, list_medCart_delete);
        //            List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
        //            List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
        //            List<medCpoeClass> filterCpoe = sql_medCpoe
        //                .Where(cpoe => medCart_sql_delete.Any(medCart => medCart.GUID == cpoe.Master_GUID)).ToList();
        //            List<object[]> list_medCpoe_delete = filterCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
        //            if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);
        //        }

        //        List<object[]> list_bedList = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.藥局, 藥局);
        //        List<patientInfoClass> bedList = list_bedList.SQLToClass<patientInfoClass, enum_patient_info>();
        //        List<patientInfoClass> patientInfoClasses = bedList.Where(temp => temp.護理站 == 護理站).ToList();
        //        patientInfoClasses.Sort(new patientInfoClass.ICP_By_bedNum());

        //        returnData.Code = 200;
        //        returnData.TimeTaken = $"{myTimerBasic}";
        //        returnData.Data = patientInfoClasses;
        //        returnData.Result = $"病床清單共{patientInfoClasses.Count}筆";
        //        return returnData.JsonSerializationt(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData.Code = -200;
        //        returnData.Result = ex.Message;
        //        return returnData.JsonSerializationt(true);
        //    }
        //}
        /// <summary>
        ///更新處方資料(針對同一藥局和護理站)
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":[medCpoeClass]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        /// [HttpPost("update_med_cpoe")]
        [HttpPost("update_med_cpoe")]
        public string update_med_cpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_cpoe";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                List<medCpoeClass> input_medCpoe = returnData.Data.ObjToClass<List<medCpoeClass>>();
                if (input_medCpoe == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                DateTime today = DateTime.Now;
                string starttime = today.GetStartDate().ToDateString();
                string endtime = today.GetEndDate().ToDateString();                

                string 藥局 = input_medCpoe[0].藥局;
                string 護理站 = input_medCpoe[0].護理站;
                //List<object[]> list_pat_info = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);

                //List<patientInfoClass> sql_patInfo = list_pat_info.SQLToClass<patientInfoClass, enum_patient_info>();
                //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();



                //List<object[]> list_pat_info = sQLControl_patient_info.GetAllRows(null);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetAllRows(null);
                //List<patientInfoClass> sql_patInfo = list_pat_info.SQLToClass<patientInfoClass, enum_patient_info>();
                //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                //sql_patInfo = (from temp in sql_patInfo
                //               where temp.更新時間.StringToDateTime() >= DateTime.Now.GetStartDate()
                //               where temp.更新時間.StringToDateTime() <= DateTime.Now.GetEndDate()
                //               select temp).ToList();
                //sql_medCpoe = (from temp in sql_medCpoe
                //               where temp.更新時間.StringToDateTime() >= DateTime.Now.GetStartDate()
                //               where temp.更新時間.StringToDateTime() <= DateTime.Now.GetEndDate()
                //               select temp).ToList();
                List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                List<patientInfoClass> sql_patInfo = GetPatInfo(sQLControl_patient_info);
                sql_patInfo = sql_patInfo.Where(temp => temp.護理站 == 護理站).ToList();
                sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站).ToList();
                List<medCpoeClass> medCpoe_sql_add = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_delete_buf = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_delete = new List<medCpoeClass>();
                List<patientInfoClass> update_patInfo = new List<patientInfoClass>();

                Dictionary<string, List<patientInfoClass>> patInfoDict = patientInfoClass.ToDictByGUID(sql_patInfo);
                Dictionary<string, List<medCpoeClass>> sqlMedCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);
                Dictionary<string, List<medCpoeClass>> inputMedCpoeDict = medCpoeClass.ToDictByMasterGUID(input_medCpoe);
                foreach (string Master_GUID in patInfoDict.Keys)
                {
                    List<medCpoeClass> Cpoe_new = medCpoeClass.GetByMasterGUID(inputMedCpoeDict, Master_GUID);
                    List<medCpoeClass> Cpoe_old = medCpoeClass.GetByMasterGUID(sqlMedCpoeDict, Master_GUID);
                    foreach(medCpoeClass medCpoeClass in Cpoe_new)
                    {
                        medCpoeClass cpoe = Cpoe_old.Where(temp => temp.PRI_KEY.Contains(medCpoeClass.PRI_KEY)).FirstOrDefault();
                        if (cpoe == null)
                        {
                            medCpoeClass.調劑異動 = "Y";
                            medCpoeClass.調劑狀態 = "NEW";
                            medCpoe_sql_add.Add(medCpoeClass);
                        }
                    }
                    foreach(medCpoeClass medCpoeClass in Cpoe_old)
                    {
                        medCpoeClass cpoe = Cpoe_new.Where(temp => temp.PRI_KEY.Contains(medCpoeClass.PRI_KEY)).FirstOrDefault();
                        if(cpoe == null)
                        {
                            if (medCpoeClass.PRI_KEY.Contains("[DC]") == false)
                            {
                                if (medCpoeClass.調劑狀態 == "Y")
                                {
                                    double 數量 = medCpoeClass.數量.StringToInt32() * -1;
                                    medCpoeClass.數量 = 數量.ToString();
                                    medCpoeClass.劑量 = "--";
                                    medCpoeClass.頻次 = "--";
                                    medCpoeClass.途徑 = "--";
                                    medCpoeClass.單位 = "--";
                                    medCpoeClass.調劑狀態 = "";
                                    medCpoeClass.狀態 = "DC";
                                    medCpoeClass.調劑異動 = "Y";
                                    medCpoeClass.PRI_KEY += "-[DC]";
                                    medCpoe_sql_replace.Add(medCpoeClass);
                                }
                                else
                                {
                                    medCpoe_sql_delete.Add(medCpoeClass);
                                }
                            }
                            
                        }
                    }
                    foreach( var add in medCpoe_sql_add)
                    {
                        medCpoeClass doubleCpoe = medCpoe_sql_delete
                            .Where(temp => temp.藥碼 == add.藥碼 && temp.途徑 == add.途徑 && temp.頻次 == add.頻次)
                            .FirstOrDefault();
                        if(doubleCpoe != null)
                        {
                            add.調劑狀態 = doubleCpoe.調劑狀態;
                        }
                    }

                }

                //foreach (string GUID in patInfoDict.Keys)
                //{
                //    List<medCpoeClass> medCpoeClasses_old = medCpoeClass.GetByMasterGUID(sqlMedCpoeDict, GUID);
                //    List<medCpoeClass> medCpoeClasses_new = medCpoeClass.GetByMasterGUID(inputMedCpoeDict, GUID);
                //    List<patientInfoClass> patientInfoClasses = patientInfoClass.GetDictByGUID(patInfoDict, GUID);

                //    if (medCpoeClasses_old.Count == 0 && medCpoeClasses_new.Count == 0)
                //    {
                //        patientInfoClasses[0].調劑狀態 = "已調劑";
                //        continue;
                //    }
                //    List<medCpoeClass> onlyInOld = medCpoeClasses_old.Where(oldItem => !medCpoeClasses_new.Any(newItem => newItem.PRI_KEY == oldItem.PRI_KEY)).ToList(); //DC
                //    List<medCpoeClass> onlyInNew = medCpoeClasses_new.Where(newItem => !medCpoeClasses_old.Any(oldItem => oldItem.PRI_KEY == newItem.PRI_KEY)).ToList(); //NEW
                //    for (int i = 0; i < onlyInOld.Count; i++)
                //    {
                //        if (onlyInOld[i].調劑狀態.StringIsEmpty() && onlyInOld[i].狀態.StringIsEmpty() && onlyInOld[i].DC確認.StringIsEmpty())
                //        {
                //            onlyInOld[i].調劑異動 = "Y";
                //            medCpoe_sql_delete.Add(onlyInOld[i]);
                //        }
                //        else
                //        {
                //            //找出onlyInOld有沒有和onlyInNew一樣的
                //            for (int j = 0; j < onlyInNew.Count; j++)
                //            {
                //                if (onlyInOld[i].藥碼 == onlyInNew[j].藥碼 &&
                //                    onlyInOld[i].途徑 == onlyInNew[j].途徑 &&
                //                    onlyInOld[i].頻次 == onlyInNew[j].頻次)
                //                {
                //                    medCpoe_sql_delete.Add(onlyInOld[i]);
                //                    onlyInNew[j].調劑狀態 = onlyInOld[i].調劑狀態;
                //                    onlyInOld[i].調劑異動 = "Y";
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //    foreach (var oldItem in onlyInOld.Where(o => o.調劑異動.StringIsEmpty()))
                //    {
                //        double 數量 = oldItem.數量.StringToInt32() * -1;
                //        oldItem.數量 = 數量.ToString();
                //        oldItem.劑量 = "--";
                //        oldItem.頻次 = "--";
                //        oldItem.途徑 = "--";
                //        oldItem.單位 = "--";
                //        oldItem.調劑狀態 = "";
                //        oldItem.狀態 = "DC";
                //        oldItem.調劑異動 = "Y";
                //        medCpoe_sql_replace.Add(oldItem);
                //    }
                    //DateTime 調劑時間 = patientInfoClasses[0].調劑時間.StringToDateTime();
                    //DateTime 現在時間 = DateTime.Now;
                    //if (調劑時間 != DateTime.MaxValue && 現在時間 > 調劑時間)
                    //{
                    //    foreach (var item in onlyInNew)
                    //    {
                    //        item.狀態 = "New";
                    //        item.調劑異動 = "Y";
                    //    }
                    //}
                    //medCpoe_sql_add.AddRange(onlyInNew);
                //}

                List<object[]> list_medCpoe_add = medCpoe_sql_add.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_medCpoe_delete = medCpoe_sql_delete.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_patInfo_add = update_patInfo.ClassToSQL<patientInfoClass, enum_patient_info>();


                if (list_medCpoe_add.Count > 0) sQLControl_med_cpoe.AddRows(null, list_medCpoe_add);
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
                if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);
                if (list_patInfo_add.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, list_patInfo_add);
            
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = input_medCpoe;
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
        //[HttpPost("update_med_cpoe")]
        //public string update_med_cpoe([FromBody] returnData returnData)
        //{
        //    MyTimerBasic myTimerBasic = new MyTimerBasic();
        //    returnData.Method = "update_med_cpoe";
        //    try
        //    {
        //        (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
        //        string API = GetServerAPI("Main", "網頁", "API01");

        //        SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
        //        SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

        //        DateTime today = DateTime.Now;
        //        string starttime = today.GetStartDate().ToDateString();
        //        string endtime = today.GetEndDate().ToDateString();
        //        //sQLControl_med_cpoe.DeleteByBetween(null, (int)enum_med_cpoe.更新時間, starttime, endtime);

        //        List<medCpoeClass> input_medCpoe = returnData.Data.ObjToClass<List<medCpoeClass>>();
        //        if (input_medCpoe == null)
        //        {
        //            returnData.Code = -200;
        //            returnData.Result = $"傳入Data資料異常";
        //            return returnData.JsonSerializationt();
        //        }

        //        string 藥局 = input_medCpoe[0].藥局;
        //        string 護理站 = input_medCpoe[0].護理站;
        //        List<object[]> list_pat_info = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);
        //        List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);

        //        //List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.護理站, 護理站);
        //        //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.護理站, 護理站);

        //        List<patientInfoClass> sql_patInfo = list_pat_info.SQLToClass<patientInfoClass, enum_patient_info>();
        //        List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
        //        sql_patInfo = sql_patInfo.Where(temp => temp.護理站 == 護理站).ToList();
        //        sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站).ToList();

        //        List<medCpoeClass> medCpoe_sql_add = new List<medCpoeClass>();
        //        List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
        //        List<medCpoeClass> medCpoe_sql_delete_buf = new List<medCpoeClass>();
        //        List<medCpoeClass> medCpoe_sql_delete = new List<medCpoeClass>();
        //        List<patientInfoClass> update_patInfo = new List<patientInfoClass>();

        //        Dictionary<string, List<patientInfoClass>> patInfoDict = patientInfoClass.ToDictByGUID(sql_patInfo);
        //        Dictionary<string, List<medCpoeClass>> sqlMedCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);
        //        Dictionary<string, List<medCpoeClass>> inputMedCpoeDict = medCpoeClass.ToDictByMasterGUID(input_medCpoe);

        //        foreach (string GUID in patInfoDict.Keys)
        //        {
        //            List<medCpoeClass> medCpoeClasses_old = medCpoeClass.GetByMasterGUID(sqlMedCpoeDict, GUID);
        //            List<medCpoeClass> medCpoeClasses_new = medCpoeClass.GetByMasterGUID(inputMedCpoeDict, GUID);
        //            List<patientInfoClass> patientInfoClasses = patientInfoClass.GetDictByGUID(patInfoDict, GUID);

        //            if (medCpoeClasses_old.Count == 0 && medCpoeClasses_new.Count == 0)
        //            {
        //                patientInfoClasses[0].調劑狀態 = "已調劑";
        //                continue;
        //            }                  
        //            List<medCpoeClass> onlyInOld = medCpoeClasses_old.Where(oldItem => !medCpoeClasses_new.Any(newItem => newItem.PRI_KEY == oldItem.PRI_KEY)).ToList(); //DC
        //            List<medCpoeClass> onlyInNew = medCpoeClasses_new.Where(newItem => !medCpoeClasses_old.Any(oldItem => oldItem.PRI_KEY == newItem.PRI_KEY)).ToList(); //NEW
        //            for(int i = 0; i < onlyInOld.Count; i++ )
        //            {
        //                if(onlyInOld[i].調劑狀態.StringIsEmpty() && onlyInOld[i].狀態.StringIsEmpty())
        //                {
        //                    onlyInOld[i].調劑異動 = "Y";
        //                    medCpoe_sql_delete.Add(onlyInOld[i]);
        //                }
        //                else
        //                {
        //                    //找出onlyInOld有沒有和onlyInNew一樣的
        //                    for (int j = 0; j < onlyInNew.Count; j++)
        //                    {
        //                        if (onlyInOld[i].藥碼 == onlyInNew[j].藥碼 &&
        //                            onlyInOld[i].途徑 == onlyInNew[j].途徑 &&
        //                            onlyInOld[i].頻次 == onlyInNew[j].頻次)
        //                        {
        //                            medCpoe_sql_delete.Add(onlyInOld[i]);
        //                            onlyInNew[j].調劑狀態 = onlyInOld[i].調劑狀態;
        //                            onlyInOld[i].調劑異動 = "Y";
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            foreach (var oldItem in onlyInOld.Where(o => o.調劑異動.StringIsEmpty()))
        //            {
        //                double 數量 = oldItem.數量.StringToInt32() * -1;
        //                oldItem.數量 = 數量.ToString();
        //                oldItem.劑量 = "--";
        //                oldItem.頻次 = "--";
        //                oldItem.途徑 = "--";
        //                oldItem.單位 = "--";
        //                oldItem.調劑狀態 = "";
        //                oldItem.狀態 = "DC";
        //                oldItem.調劑異動 = "Y";
        //                medCpoe_sql_replace.Add(oldItem);
        //            }
        //            DateTime 調劑時間 = patientInfoClasses[0].調劑時間.StringToDateTime();
        //            DateTime 現在時間 = DateTime.Now;
        //            if (調劑時間 != DateTime.MaxValue && 現在時間 > 調劑時間)
        //            {
        //                foreach (var item in onlyInNew)
        //                {                          
        //                    item.狀態 = "New";
        //                    item.調劑異動 = "Y";
        //                }
        //            }                 
        //            medCpoe_sql_add.AddRange(onlyInNew);
        //        }

        //        List<object[]> list_medCpoe_add = medCpoe_sql_add.ClassToSQL<medCpoeClass, enum_med_cpoe>();
        //        List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
        //        List<object[]> list_medCpoe_delete = medCpoe_sql_delete.ClassToSQL<medCpoeClass, enum_med_cpoe>();
        //        List<object[]> list_medCart_add = update_medCarInfo.ClassToSQL<patientInfoClass, enum_patient_info>();


        //        if (list_medCpoe_add.Count > 0) sQLControl_med_cpoe.AddRows(null, list_medCpoe_add);
        //        if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
        //        if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);
        //        if (list_medCart_add.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCart_add);

        //        //List<object[]> list_med_cpoe_new = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
        //        //List<medCpoeClass> medCpoeClass_new = list_med_cpoe_new.SQLToClass<medCpoeClass, enum_med_cpoe>();
        //        //List<medCpoeClass> results = new List<medCpoeClass>();
        //        //Dictionary<string, List<medCpoeClass>> sqlMedCpoeDict_new = medCpoeClass.ToDictByMasterGUID(medCpoeClass_new);
        //        //foreach (string Master_GUID in inputMedCpoeDict.Keys)
        //        //{
        //        //    List<medCpoeClass> result = medCpoeClass.GetByMasterGUID(sqlMedCpoeDict_new, Master_GUID);
        //        //    if (result.Count > 0) results.AddRange(result);                 
        //        //}
        //        returnData.Code = 200;
        //        returnData.TimeTaken = $"{myTimerBasic}";
        //        returnData.Data = input_medCpoe;
        //        returnData.Result = $"更新處方資料表成功";
        //        return returnData.JsonSerializationt(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData.Code = -200;
        //        returnData.Result = ex.Message;
        //        return returnData.JsonSerializationt(true);

        //    }
        //}
        /// <summary>
        ///更新處方大瓶點滴紀錄
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         
        ///         "ValueAry":["處方GUID","L"]
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                if (returnData.ValueAry == null ||returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\",\"L\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                string 大瓶藥標記 = returnData.ValueAry[1];

                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, GUID);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                sql_medCpoe[0].大瓶點滴 = 大瓶藥標記;
                        
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                List<medCpoeRecClass> input_medCpoe_rec = returnData.Data.ObjToClass<List<medCpoeRecClass>>();
                string 藥局 = input_medCpoe_rec[0].藥局;
                string 護理站 = input_medCpoe_rec[0].護理站;
                if (input_medCpoe_rec == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                DateTime lestweek = DateTime.Now.AddDays(-7);
                DateTime yesterday = DateTime.Now.AddDays(-0);
                string starttime = lestweek.GetStartDate().ToDateString();
                string endtime = yesterday.GetEndDate().ToDateString();
                SQLControl sQLControl_med_cpoe_rec = new SQLControl(Server, DB, "med_cpoe_rec", UserName, Password, Port, SSLMode);
                sQLControl_med_cpoe_rec.DeleteByBetween(null, (int)enum_med_cpoe_rec.更新時間, starttime, endtime);

                List<object[]> sql_medCpoeRecClass = sQLControl_med_cpoe_rec.GetRowsByDefult(null, (int)enum_med_cpoe_rec.藥局, 藥局);
                List<medCpoeRecClass> list_medCpoeRecClass = sql_medCpoeRecClass.SQLToClass<medCpoeRecClass, enum_med_cpoe_rec>();
                list_medCpoeRecClass = list_medCpoeRecClass.Where(temp => temp.護理站 == 護理站).ToList();
                Dictionary<string, List<medCpoeRecClass>> inputMedCpoeRecDict = medCpoeRecClass.ToDictByMasterGUID(input_medCpoe_rec);
                Dictionary<string, List<medCpoeRecClass>> sqlMedCpoeRecDict = medCpoeRecClass.ToDictByMasterGUID(list_medCpoeRecClass);
                List<medCpoeRecClass> add_medCpoeRecClass = new List<medCpoeRecClass>();
                foreach (var Master_GUID in inputMedCpoeRecDict.Keys)
                {
                    List<medCpoeRecClass> input_medCpoeRec = medCpoeRecClass.GetDictByMasterGUID(inputMedCpoeRecDict, Master_GUID);
                    List<medCpoeRecClass> sql_medCpoeRec = medCpoeRecClass.GetDictByMasterGUID(sqlMedCpoeRecDict, Master_GUID);

                    List<medCpoeRecClass> result = input_medCpoeRec
                        .Where(inputItem => !sql_medCpoeRec.Any(sqlItem => sqlItem.序號 == inputItem.序號))
                        .ToList();
                    if(result.Count > 0)add_medCpoeRecClass.AddRange(result);                   
                }
                List<object[]> list_medCpoe_add = add_medCpoeRecClass.ClassToSQL<medCpoeRecClass, enum_med_cpoe_rec>();
                sQLControl_med_cpoe_rec.AddRows(null, list_medCpoe_add);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = list_medCpoe_add;
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
        ///更新病床異動紀錄資料
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
        [HttpPost("update_bed_status")]
        public string update_bed_status([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_bed_status";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                List<bedStatusClass> input_bedStatus = returnData.Data.ObjToClass<List<bedStatusClass>>();
                if (input_bedStatus == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                DateTime lestweek = DateTime.Now.AddDays(-7);
                DateTime yesterday = DateTime.Now.AddDays(-0);
                string starttime = lestweek.GetStartDate().ToDateString();
                string endtime = yesterday.GetEndDate().ToDateString();
                SQLControl sQLControl = new SQLControl(Server, DB, "bed_status", UserName, Password, Port, SSLMode);
                sQLControl.DeleteByBetween(null, (int)enum_bed_status.轉床時間, starttime, endtime);

                List<object[]> sql_bedStatusClass = sQLControl.GetAllRows(null);
                List<bedStatusClass> list_bedStatusClass = sql_bedStatusClass.SQLToClass<bedStatusClass, enum_bed_status>();

                Dictionary<string, List<bedStatusClass>> inputBedStatusDict = bedStatusClass.ToDictByMasterGUID(input_bedStatus);
                Dictionary<string, List<bedStatusClass>> sqlBedStatusDict = bedStatusClass.ToDictByMasterGUID(list_bedStatusClass);
                List<bedStatusClass> add_bedStatusClass = new List<bedStatusClass>();
                foreach (var Master_GUID in inputBedStatusDict.Keys)
                {
                    List<bedStatusClass> input = bedStatusClass.GetByMasterGUID(inputBedStatusDict, Master_GUID);
                    List<bedStatusClass> sql = bedStatusClass.GetByMasterGUID(sqlBedStatusDict, Master_GUID);

                    List<bedStatusClass> result = input
                        .Where(inputItem => !sql.Any(sqlItem => sqlItem.PRI_KEY == inputItem.PRI_KEY))
                        .ToList();
                    if (result.Count > 0) add_bedStatusClass.AddRange(result);
                }
                List<object[]> list_bedStatus_add = add_bedStatusClass.ClassToSQL<bedStatusClass, enum_bed_status>();
                sQLControl.AddRows(null, list_bedStatus_add);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = list_bedStatus_add;
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
        //[HttpPost("update_med_info")]
        //public string update_med_info([FromBody] returnData returnData)
        //{
        //    MyTimerBasic myTimerBasic = new MyTimerBasic();
        //    returnData.Method = "update_med_info";
        //    try
        //    {
        //        (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
        //        string API = GetServerAPI("Main", "網頁", "API01");

        //        List<medInfoClass> input_medInfoClass = returnData.Data.ObjToClass<List<medInfoClass>>();

        //        if (input_medInfoClass == null)
        //        {
        //            returnData.Code = -200;
        //            returnData.Result = $"傳入Data資料異常";
        //            return returnData.JsonSerializationt();
        //        }                           
        //        SQLControl sQLControl_med_info = new SQLControl(Server, DB, "med_info", UserName, Password, Port, SSLMode);
        //        List<object[]> list_medInfo = sQLControl_med_info.GetAllRows(null);
        //        List<medInfoClass> sql_medInfo = list_medInfo.SQLToClass<medInfoClass, enum_med_info>();
        //        Dictionary<string, List<medInfoClass>> sqlMedInfoDict = medInfoClass.CoverToDictionaryByCode(sql_medInfo);
        //        List<medInfoClass> add_medInfo = new List<medInfoClass>();
        //        List<medInfoClass> update_medInfo = new List<medInfoClass>();

        //        foreach (var medInfoClass in input_medInfoClass)
        //        {
        //            List<medInfoClass> target = medInfoClass.SortDictByCode(sqlMedInfoDict, medInfoClass.藥碼);
        //            if (target.Count == 0)
        //            {
        //                medInfoClass.GUID = Guid.NewGuid().ToString();
        //                add_medInfo.Add(medInfoClass);
        //            }
        //            else if(target.Count == 1)
        //            {
        //                medInfoClass.GUID = target[0].GUID;
        //                update_medInfo.Add(medInfoClass);
        //            }           
        //            else
        //            {
        //                returnData.Code = 200;
        //                returnData.Result = $"資料重複，請確認資料庫";
        //                return returnData.JsonSerializationt(true);
        //            }
        //        }
        //        List<object[]> list_add_medInfo = add_medInfo.ClassToSQL<medInfoClass, enum_med_info>();
        //        List<object[]> list_update_medInfo = update_medInfo.ClassToSQL<medInfoClass, enum_med_info>();

        //        if (list_add_medInfo.Count > 0) sQLControl_med_info.AddRows(null, list_add_medInfo);
        //        if (list_update_medInfo.Count > 0) sQLControl_med_info.UpdateByDefulteExtra(null, list_update_medInfo);

        //        returnData.Code = 200;
        //        returnData.TimeTaken = $"{myTimerBasic}";
        //        returnData.Data = input_medInfoClass;
        //        returnData.Result = $"更新藥品資訊成功";
        //        return returnData.JsonSerializationt(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData.Code = -200;
        //        returnData.Result = ex.Message;
        //        return returnData.JsonSerializationt(true);

        //    }
        //}
        //[HttpPost("get_bed_list_by_cart")]
        //public string get_bed_list_by_cart([FromBody] returnData returnData)
        //{
        //    MyTimerBasic myTimerBasic = new MyTimerBasic();
        //    returnData.Method = "med_cart/get_bed_list_by_cart";
        //    try
        //    {
        //        if (returnData.ValueAry == null)
        //        {
        //            returnData.Code = -200;
        //            returnData.Result = $"returnData.ValueAry 無傳入資料";
        //            return returnData.JsonSerializationt(true);
        //        }
        //        if (returnData.ValueAry.Count != 2)
        //        {
        //            returnData.Code = -200;
        //            returnData.Result = $"returnData.ValueAry 內容應為[藥局, 護理站]";
        //            return returnData.JsonSerializationt(true);
        //        }
        //        (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");

        //        string 藥局 = returnData.ValueAry[0];
        //        string 護理站 = returnData.ValueAry[1];

        //        SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);

        //        List<object[]> list_pat_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_patient_info.藥局, 藥局);
        //        List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
        //        List<patientInfoClass> patientInfoClasses = sql_patinfo.Where(temp => temp.護理站 == 護理站).ToList();
        //        patientInfoClasses.Sort(new patientInfoClass.ICP_By_bedNum());

        //        returnData.Code = 200;
        //        returnData.TimeTaken = $"{myTimerBasic}";
        //        returnData.Data = patientInfoClasses;
        //        returnData.Result = $"取得{藥局} {護理站} 所有病人資訊";
        //        return returnData.JsonSerializationt(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData.Code = -200;
        //        returnData.Result = ex.Message;
        //        return returnData.JsonSerializationt(true);
        //    }
        //}
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
            returnData.Method = "med_cart/get_bed_list_by_cart";
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");

                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                //List<object[]> list_patInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.護理站, 護理站);
                //List<patientInfoClass> sql_patinfo = list_patInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<patientInfoClass> sql_patinfo = GetPatInfo(sQLControl_patient_info);
                List<patientInfoClass> patientInfoClasses = sql_patinfo.Where(temp => temp.護理站 == 護理站).ToList();
                patientInfoClasses.Sort(new patientInfoClass.ICP_By_bedNum());

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = patientInfoClasses;
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
            returnData.Method = "get_patient_by_GUID";
            string str_result_temp = "";
            try
            {
                List<Task> tasks = new List<Task>();
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.Value == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Value 內容應為[\"all or 調劑台名稱\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<patientInfoClass> sql_patInfo = new List<patientInfoClass>();
                List<medCpoeClass> sql_medCpoe = new List<medCpoeClass>();


                tasks.Add(Task.Run(new Action(delegate
                {
                    //取得配車病人資料
                    List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.GUID, GUID);
                    sql_patInfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                    str_result_temp += $"取得配車病人資料 , {myTimerBasic}ms \n";
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    //取得處方資料
                    List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, GUID);
                    sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                    str_result_temp += $"取得處方資料 , {myTimerBasic}ms \n";
                })));
                Task.WhenAll(tasks).Wait();
              

                if (sql_patInfo == null)
                {
                    returnData.Code = 200;
                    returnData.Result = "無對應的病人資料";
                    return returnData.JsonSerializationt(true);
                }
                if(sql_medCpoe.Count > 0)
                {
                    sql_medCpoe.Sort(new medCpoeClass.ICP_By_Rank());

                    List<string> Codes = sql_medCpoe.Select(temp => temp.藥碼).Distinct().ToList();
                    if (Codes.Count == 1) Codes[0] = Codes[0] + ",";

                    if (returnData.Value != "all")
                    {
                        //取得調劑台內藥品資訊
                        List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, returnData.Value, Codes);
                        Dictionary<string, List<medClass>> medClassDict = medClass.CoverToDictionaryByCode(medClasses);
                        foreach (medCpoeClass medCpoeClass in sql_medCpoe)
                        {
                            if (medClassDict.ContainsKey(medCpoeClass.藥碼)) medCpoeClass.調劑台 = "Y";
                        }
                    }
                    str_result_temp += $"取得調劑台內藥品資訊 , {myTimerBasic}ms \n";

                    if (returnData.Value == "all")
                    {
                        foreach (var medCpoeClass in sql_medCpoe) medCpoeClass.調劑台 = "Y";
                    }
                    List<string> GUIDs = sql_medCpoe.Select(temp => temp.GUID).ToList();
                    string valueAry = string.Join(";",GUIDs);
                    //取得線上藥檔資料
                    List<medClass> med_cloud = new List<medClass>();
                    List<medPriceClass> med_price = new List<medPriceClass>();
                    List<medInventoryLogClass> med_InvenLog = new List<medInventoryLogClass>();
                    tasks.Clear();
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        med_cloud = medClass.get_med_clouds_by_codes(API, Codes);
                        str_result_temp += $"取得線上藥檔資料 , {myTimerBasic}ms \n";
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        //取得藥品價格等等資訊
                        med_price = medPriceClass.get_by_codes(API, Codes);
                        str_result_temp += $"取得藥品價格等等資訊 , {myTimerBasic}ms \n";
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        //取得調劑紀錄
                        med_InvenLog = medInventoryLogClass.get_logtime_by_master_GUID(API, valueAry);
                        str_result_temp += $"取得調劑紀錄 , {myTimerBasic}ms \n";
                    })));
                    Task.WhenAll(tasks).Wait();
                    //轉換字典搜尋
                    Dictionary<string, List<medClass>> medCloudDict = medClass.CoverToDictionaryByCode(med_cloud);
                    Dictionary<string, List<medPriceClass>> medPriceDict = medPriceClass.CoverToDicByCode(med_price);
                    Dictionary<string, List<medInventoryLogClass>> medInvenDict = medInventoryLogClass.CoverToDictionaryMasterGUID(med_InvenLog);      
                    foreach (var cpoe in sql_medCpoe)
                    {    
                        

                        cpoe.雲端藥檔 = medClass.SortDictionaryByCode(medCloudDict, cpoe.藥碼);
                        cpoe.藥品價格 = medPriceClass.GetByCode(medPriceDict, cpoe.藥碼);
                        cpoe.調劑紀錄 = medInventoryLogClass.SortDictByMasterGUID(medInvenDict, cpoe.GUID);
                    }
                    sql_patInfo[0].處方 = sql_medCpoe;
                    str_result_temp += $"轉換字典搜尋 , {myTimerBasic}ms \n";

                }
                else
                {
                    sql_patInfo[0].處方 = new List<medCpoeClass>();
                }
                //patientInfoClass patientInfoClasses = new patientInfoClass();
                //patientInfoClasses = sql_patinfo[0];

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_patInfo[0];
                returnData.Result = $"取得{sql_patInfo[0].藥局} {sql_patInfo[0].護理站} 第{sql_patInfo[0].床號}病床的資訊\n{str_result_temp}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }       
        [HttpPost("get_patient_with_NOdispense")]
        public string get_patient_with_NOdispense([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_Cart/get_patient_with_NOdispense";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"藥局\", \"護理站\"]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                //List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.藥局, 藥局);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);

                //List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                List<patientInfoClass> sql_patinfo = GetPatInfo(sQLControl_patient_info);

                List<patientInfoClass> sql_medCar = sql_patinfo.Where(temp => temp.調劑狀態 == "" && temp.護理站 == 護理站).ToList();
                Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);

                foreach ( var medcar in sql_medCar)
                {
                    List<medCpoeClass> targetCpoe = medCpoeClass.GetByMasterGUID(medCpoeDict, medcar.GUID);
                    targetCpoe = targetCpoe.Where(temp => temp.調劑狀態 == "").ToList();
                    medcar.處方 = targetCpoe;
                }
                sql_medCar.Sort(new patientInfoClass.ICP_By_bedNum());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCar;
                returnData.Result = $"取得{藥局} {護理站} 共{sql_medCar.Count}床 未調劑";
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
        ///取得所有處方資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":""
        ///         "ValueAry":[]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medCpoe")]
        public string get_medCpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_medCpoe";
            try
            {
                string GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetAllRows(null);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();                

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"取得處方資料共{sql_medCpoe.Count}";
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
        ///取得病床更動紀錄
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
        [HttpPost("get_bed_status")]
        public string get_bed_status([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_cart/get_bed_status";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 無傳入資料";
                    return returnData.JsonSerializationt(true);
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl = new SQLControl(Server, DB, "bed_status", UserName, Password, Port, SSLMode);

                List<object[]> list_bed_status = sQLControl.GetAllRows(null);
                List<bedStatusClass> bedStatusClasses = list_bed_status.SQLToClass<bedStatusClass, enum_bed_status>();
                bedStatusClasses.Sort(new bedStatusClass.ICP_By_UP_Time());

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = bedStatusClasses;
                returnData.Result = $"取得病床異動資料共{bedStatusClasses.Count}筆";
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
        ///以GUID取得藥品更動紀錄
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
            returnData.Method = "med_cart/get_medChange_by_GUID";
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe_rec = new SQLControl(Server, DB, "med_cpoe_rec", UserName, Password, Port, SSLMode);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.GUID, GUID);
                List<object[]> list_med_cpoe_rec = sQLControl_med_cpoe_rec.GetRowsByDefult(null, (int)enum_med_cpoe_rec.Master_GUID, GUID);

                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<medCpoeRecClass> sql_medCpoeRec = list_med_cpoe_rec.SQLToClass<medCpoeRecClass, enum_med_cpoe_rec>();
                sql_medCpoeRec.Sort(new medCpoeRecClass.ICP_By_UP_Time());

                sql_patinfo[0].處方異動 = sql_medCpoeRec;

                string 藥局 = sql_patinfo[0].藥局;
                string 護理站 = sql_patinfo[0].護理站;
                string 床號 = sql_patinfo[0].床號;

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_patinfo;
                returnData.Result = $"取得{藥局} {護理站} 第{床號}床 處方異動資料共{sql_medCpoeRec.Count}筆";
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
        ///逐床確認藥品調劑狀態
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                string Master_GUID = returnData.Value;
                string[] GUID = returnData.ValueAry[0].Split(";");
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
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
                        if (medCpoeClass.狀態 == "DC") medCpoeClass.DC確認 = "Y";
                        medCpoeClass.狀態 = "";      
                        medCpoeClass.調劑異動 = "";
                    }
                    else
                    {
                        medCpoeClass.調劑狀態 = "";

                    }
                    medCpoe_sql_replace.Add(medCpoeClass);

                }

                (bool unDispensed, bool DCNew) = GetDispensFlags(medCpoe_sql_replace);

                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.GUID, Master_GUID);
                    List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                    sql_patinfo[0].調劑時間 = DateTime.Now.ToDateString();
                    sql_patinfo = EditMedCarInfo(unDispensed, DCNew, sql_patinfo);
                    List<object[]> list_medCarInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCarInfo_replace);
                })));
                Task.WhenAll(tasks).Wait();
                            

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCpoe_sql_replace;
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
        ///以GUID調整藥品調劑狀態
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["病人GUID","藥品GUID", "Y/ "]";
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("check_dispense_by_GUID")]
        public string check_dispense_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "check_dispense_by_GUID";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                if (returnData.ValueAry == null || returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"病人GUID\",\"藥品GUID\", \"Y/ \"]";
                    return returnData.JsonSerializationt(true);
                }
                string Master_GUID = returnData.ValueAry[0];
                string GUID = returnData.ValueAry[1];
                string 調劑狀態 = returnData.ValueAry[2];
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> medCpoe_sql_replace = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();                
                if (medCpoe_sql_replace.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                            
                for (int i = 0; i < medCpoe_sql_replace.Count; i++)
                {
                    if (medCpoe_sql_replace[i].GUID == GUID) 
                    {
                        medCpoe_sql_replace[i].調劑狀態 = 調劑狀態;
                        if (調劑狀態.StringIsEmpty() == false && medCpoe_sql_replace[i].狀態 == "DC")
                        {                            
                            medCpoe_sql_replace[i].DC確認 = "Y";
                            medCpoe_sql_replace[i].狀態 = "";
                            medCpoe_sql_replace[i].調劑異動 = "";                            
                        }
                    }
                }
                (bool unDispensed, bool DCNew) = GetDispensFlags(medCpoe_sql_replace);
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.GUID, Master_GUID);
                    List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                    if (!調劑狀態.StringIsEmpty()) sql_patinfo[0].調劑時間 = DateTime.Now.ToDateString();
                    sql_patinfo = EditMedCarInfo(unDispensed, DCNew, sql_patinfo);
                    List<object[]> list_medCarInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCarInfo_replace);
                    
                })));
                Task.WhenAll(tasks).Wait();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCpoe_sql_replace;
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
        ///以GUID確認藥品調劑
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["處方GUID","處方GUID", "處方Master_GUID"]";
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("dispensed_by_GUID")]
        public string dispensed_by_GUID([FromBody] returnData returnData)
        {
            ///未調藥品的全部調劑
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "dispensed_by_GUID";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                if (returnData.ValueAry == null )
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 空白，請輸入對應欄位資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"處方GUID1;處方GUID2\",\"處方Master_GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                List<string> GUIDs = returnData.ValueAry[0].Split(";").ToList();
                string Master_GUID = returnData.ValueAry[1];
             
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                int buff_dispenseStatus = 0;
                for (int i = 0; i < sql_medCpoe.Count; i++)
                {
                    if (GUIDs.Contains(sql_medCpoe[i].GUID))
                    {
                        sql_medCpoe[i].調劑狀態 = "Y";
                        sql_medCpoe[i].狀態 = "";
                        sql_medCpoe[i].調劑異動 = "";
                        buff_dispenseStatus += 1;
                    }
                }
                (bool unDispensed, bool DCNew) = GetDispensFlags(sql_medCpoe);

                List<Task> tasks = new List<Task>();
                string str = "";
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> update_medCpoe = sql_medCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    sQLControl_med_cpoe.UpdateByDefulteExtra(null, update_medCpoe);
                })));
                tasks.Add(Task.Run(new Action(delegate 
                {
                    SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                    List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.GUID, Master_GUID);
                    List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                    sql_patinfo[0].調劑時間 = DateTime.Now.ToDateString();
                    sql_patinfo = EditMedCarInfo(unDispensed, DCNew, sql_patinfo);
                    List<object[]> list_medCarInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCarInfo_replace);
                    string 護理站 = list_pat_carInfo[0][(int)enum_patient_info.護理站].ObjectToString();
                    string 床號 = list_pat_carInfo[0][(int)enum_patient_info.床號].ObjectToString();
                    str += $"更新{護理站} 第{床號}床 調劑狀態、調劑時間和處方異動狀態";
                    
                })));
                Task.WhenAll(tasks).Wait();
                
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"更新處方紀錄共{buff_dispenseStatus}筆，{str}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception:{ex.Message}";
                Logger.Log(returnData.JsonSerializationt(true));
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        ///以GUID確認藥品調劑
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["處方GUID";"處方GUID", "護理站"]";
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("dispensed_by_cart")]
        public string dispensed_by_cart([FromBody] returnData returnData)
        {
            //針對整個藥車調劑(只能確認)
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "dispensed_by_cart";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 空白，請輸入對應欄位資料";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"處方GUID1;處方GUID2\",\"護理站\"]";
                    return returnData.JsonSerializationt(true);
                }
                List<string> GUIDs = returnData.ValueAry[0].Split(";").ToList();
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.護理站, 護理站);
                //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                //List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.護理站, 護理站);
                //List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                List<patientInfoClass> sql_patinfo = GetPatInfo(sQLControl_patient_info);
                List<string> carinfo_GUID = new List<string>();
                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                int buff = 0;
                for (int i = 0; i < sql_medCpoe.Count; i++)
                {
                    if (GUIDs.Contains(sql_medCpoe[i].GUID))
                    {
                        sql_medCpoe[i].調劑狀態 = "Y";
                        sql_medCpoe[i].狀態 = "";
                        sql_medCpoe[i].調劑異動 = "";
                        buff += 1;
                        //if (!carinfo_GUID.Contains(sql_medCpoe[i].Master_GUID)) carinfo_GUID.Add(sql_medCpoe[i].Master_GUID);
                    }
                }
                List<Task> tasks = new List<Task>();
                string str = "";
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> update_medCpoe = sql_medCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    sQLControl_med_cpoe.UpdateByDefulteExtra(null, update_medCpoe);
                })));
                tasks.Add(Task.Run(new Action(delegate 
                {
                    CheckDispenseStatus(sql_medCpoe, sql_patinfo, sQLControl_patient_info);
                })));
                Task.WhenAll(tasks).Wait();


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"更新藥車: {護理站}處方紀錄共{buff}筆，{str}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception:{ex.Message}";
                Logger.Log(returnData.JsonSerializationt(true));
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        ///覆核藥品調劑
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
        [HttpPost("double_check")]
        public string double_check([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "double_check";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                string Master_GUID = returnData.Value;
                string[] GUID = returnData.ValueAry[0].Split(";");
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                //List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                foreach (var medCpoeClass in sql_medCpoe)
                {
                    medCpoeClass.覆核狀態 = GUID.Contains(medCpoeClass.GUID) ? "Y" : "";                  
                    medCpoe_sql_replace.Add(medCpoeClass);
                }
                List<object[]> list_medCpoe_replace = new List<object[]>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);

                bool allDispensed = medCpoe_sql_replace.All(med => med.覆核狀態 == "Y");
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, Master_GUID);
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                sql_patinfo[0].覆核狀態 = allDispensed ? "Y" : "";
               

                List<object[]> list_medCarInfo_replace = new List<object[]>();
                list_medCarInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCarInfo_replace);

                //List<string> ValueAry = new List<string> { Master_GUID };
                //patientInfoClass targetPatient = patientInfoClass.get_patient_by_GUID(API, ValueAry);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_patinfo;
                returnData.Result = $"更新覆核紀錄";

                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception:{ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        [HttpPost("double_check_by_GUID")]
        public string double_check_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "double_check_by_GUID";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"病人GUID\",\"藥品GUID\", \"Y/ \"]";
                    return returnData.JsonSerializationt(true);
                }
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                string Master_GUID = returnData.ValueAry[0];
                string GUID = returnData.ValueAry[1];
                string 覆核狀態 = returnData.ValueAry[2];
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                //List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                List<medCpoeClass> medCpoe_sql_replace = sql_medCpoe.Where(temp => temp.GUID == GUID).ToList();
                sql_medCpoe.RemoveAll(temp => temp.GUID == GUID);
                if (medCpoe_sql_replace.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                medCpoe_sql_replace[0].覆核狀態 = 覆核狀態;

                List<object[]> list_medCpoe_replace = new List<object[]>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);


                sql_medCpoe.Add(medCpoe_sql_replace[0]);
                bool allDispensed = sql_medCpoe.All(med => med.覆核狀態 == "Y");
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, Master_GUID);
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                sql_patinfo[0].覆核狀態 = allDispensed ? "Y" : "";

                List<object[]> list_medCarInfo_replace = new List<object[]>();
                list_medCarInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCarInfo_replace);


                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"更新覆核紀錄";

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
            returnData.Method = "med_cart/get_med_qty";
            try
            {
                if (returnData.Value == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Value 無傳入資料";
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> medCpoeClasses = sql_medCpoe.Where(temp => temp.護理站 == 護理站).ToList();

                List<medQtyClass> medQtyClasses = medCpoeClasses
                    .GroupBy(temp => temp.藥品名)
                    .Select(grouped => new medQtyClass
                    {
                        藥品名 = grouped.Key,
                        藥碼 = grouped.First().藥碼,
                        單位 = grouped.First().單位,
                        針劑 = grouped.First().針劑,
                        口服 = grouped.First().口服,
                        冷儲 = grouped.First().冷儲,
                        調劑台 = "",
                        大瓶點滴 = "",
                        病床清單 = grouped.Select(value => new bedListClass
                        {
                            GUID = value.GUID,
                            Master_GUID = value.Master_GUID,
                            床號 = value.床號,
                            數量 = value.數量,
                            劑量 = value.劑量,
                            大瓶點滴 = value.大瓶點滴,
                            調劑狀態 = value.調劑狀態,
                            覆核狀態 = value.覆核狀態,
                            頻次 = value.頻次
                        }).ToList()
                    })
                    .ToList();
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate 
                {
                    for(int i = 0; i < medQtyClasses.Count; i++)
                    {
                        bool flag = true;
                        for (int j = 0; j < medQtyClasses[i].病床清單.Count; j++)
                        {
                            if (medQtyClasses[i].病床清單[j].大瓶點滴 != "L") flag = false;
                        }
                        if (flag)
                        {
                            medQtyClasses[i].大瓶點滴 = "L";
                        }
                    }
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<string> codes = medQtyClasses.Select(temp => temp.藥碼).Distinct().ToList();

                    if (returnData.Value == "all")
                    {
                        foreach (var medQtyClass in medQtyClasses)
                        {
                            medQtyClass.調劑台 = "Y";
                        }
                    }
                    else
                    {
                        List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, returnData.Value, codes);
                        Dictionary<string, List<medClass>> medClassDict = medClass.CoverToDictionaryByCode(medClasses);
                        foreach (var medQtyClass in medQtyClasses)
                        {
                            if (medClassDict.ContainsKey(medQtyClass.藥碼)) medQtyClass.調劑台 = "Y";
                        }
                    }
                    
                })));
                Task.WhenAll(tasks).Wait();
                foreach (var medQtyClass in medQtyClasses)
                {
                    medQtyClass.病床清單.Sort(new bedListClass.ICP_By_bedNum());
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
            returnData.Method = "med_cart/handover";
            try
            {             
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥局, 護理站]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_carlist = new SQLControl(Server, DB, "med_carlist", UserName, Password, Port, SSLMode);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                List<object[]> list_med_carlist = sQLControl_med_carlist.GetRowsByDefult(null, (int)enum_med_carList.藥局, 藥局);
                //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCarListClass> sql_medCarList = list_med_carlist.SQLToClass<medCarListClass, enum_med_carList>();
                List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                List<medCpoeClass> medCpoeClasses = sql_medCpoe.Where(temp => temp.護理站 == 護理站 && temp.覆核狀態 == "").ToList();
                medCarListClass medCarList = sql_medCarList.FirstOrDefault(temp => temp.護理站 == 護理站);
                if (medCpoeClasses.Count != 0)
                {
                    int groupCount = medCpoeClasses
                        .GroupBy(temp => temp.Master_GUID)
                        .Count();
                    medCarList.備註 = $"{藥局} {護理站} 尚有{groupCount}床覆核未完成";
                    returnData.Code = 200;
                    returnData.Data = medCarList;
                    returnData.Result = $"未交車";
                    return returnData.JsonSerializationt(true);
                }
                medCarList.交車時間 = DateTime.Now.ToDateTimeString();
                medCarList.交車狀態 = "已交車";
                List<medCarListClass> medCarListClasses = new List<medCarListClass> { medCarList };
                List<object[]> medCarList_sql_update = medCarListClasses.ClassToSQL<medCarListClass, enum_med_carList>();
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
            returnData.Method = "med_cart/get_dispens_by_code";
            try
            {               
                string API = GetServerAPI("Main", "網頁", "API01");              

                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
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
                List<dispensClass> dispensClasses = new List<dispensClass>();
                string codes = $"{returnData.Value},";
                List<string> code = new List<string> { codes };
                List<string> dispens = returnData.ValueAry[0].Split(";").ToList();              

                List<Task> tasks = new List<Task>();
                object lockObj = new object();
                foreach (var disp in dispens)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, disp, code);
                        if (medClasses.Count > 0)
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
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 清洗資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":""
        ///         "ValueAry":["UC02"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("clear_data")]
        public string clear_data([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "clear_data";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");

                //string 藥局 = returnData.ValueAry[0];

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetAllRows(null);
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<patientInfoClass> update_medCarInfo = new List<patientInfoClass>();
                foreach (var patientInfoClass in sql_patinfo)
                {
                    if(patientInfoClass.占床狀態 == "已佔床")
                    {
                        patientInfoClass.姓名 = "XXX";
                        patientInfoClass.PRI_KEY = "33445566";
                        patientInfoClass.住院號 = "31766666";
                        patientInfoClass.病歷號 = "33445566";
                        patientInfoClass.住院醫師 = "王志明";
                        patientInfoClass.住院醫師代碼 = "UDC7777";
                        patientInfoClass.主治醫師 = "陳春嬌";
                        patientInfoClass.主治醫師代碼 = "UDC8888";
                        update_medCarInfo.Add(patientInfoClass);
                    }                  
                }

                List<object[]> update_med_carInfo = update_medCarInfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                if (update_med_carInfo.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, update_med_carInfo);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                //returnData.Data = patientInfoClasses;
                returnData.Result = $"更改所有病人資訊";
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
        /// 清洗資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":""
        ///         "ValueAry":["UC02"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("edit_data")]
        public string edit_data([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "edit_data";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");

                //string 藥局 = returnData.ValueAry[0];

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe_rec = new SQLControl(Server, DB, "med_cpoe_rec", UserName, Password, Port, SSLMode);


                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetAllRows(null);
                List<object[]> list_med_cpoe_rec = sQLControl_med_cpoe_rec.GetAllRows(null);

                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<medCpoeRecClass> sql_medCpoeRec = list_med_cpoe_rec.SQLToClass<medCpoeRecClass, enum_med_cpoe_rec>();

                List<medCpoeRecClass> update_medCpoeRec = new List<medCpoeRecClass>();


                for (int i = 0; i < sql_patinfo.Count; i++)
                {
                    string 護理站 = sql_patinfo[i].護理站;
                    string 床號 = sql_patinfo[i].床號;
                    string GUID = sql_patinfo[i].GUID;
                    List<medCpoeRecClass> medCpoeRec_buff = sql_medCpoeRec.Where(temp => temp.護理站 == 護理站 && temp.床號 == 床號).ToList();
                    if(medCpoeRec_buff.Count > 0)
                    {
                        foreach(var item in medCpoeRec_buff)
                        {
                            item.Master_GUID = GUID;
                        }
                        update_medCpoeRec.AddRange(medCpoeRec_buff);
                    }
                }

                

                List<object[]> update = update_medCpoeRec.ClassToSQL<medCpoeRecClass, enum_med_cpoe_rec>();
                if (update_medCpoeRec.Count > 0) sQLControl_med_cpoe_rec.UpdateByDefulteExtra(null, update);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                //returnData.Data = patientInfoClasses;
                returnData.Result = $"更改所有病人資訊";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }

        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass, Enum enumInstance)
        {
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, enumInstance);
            return table.JsonSerializationt(true);
        }

        private (string Server, string DB, string UserName, string Password, uint Port) GetServerInfo(string Name, string Type, string Content)
        {
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return (sys_serverSettingClass.Server, sys_serverSettingClass.DBName, sys_serverSettingClass.User, sys_serverSettingClass.Password, (uint)sys_serverSettingClass.Port.StringToInt32());
        }
        private string GetServerAPI(string Name, string Type, string Content)
        {
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return sys_serverSettingClass.Server;
        }
        private void CheckDispenseStatus(List<medCpoeClass> medCpoeClasses, List<patientInfoClass> patientInfoClasses, SQLControl sQLControl_med_carinfo)
        {
            Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.ToDictByMasterGUID(medCpoeClasses);
            List<patientInfoClass> update_medCarInfo = new List<patientInfoClass>();
            foreach (string Master_GUID in medCpoeDict.Keys)
            {
                List<medCpoeClass> medCpoe = medCpoeClass.GetByMasterGUID(medCpoeDict, Master_GUID);
                (bool unDispensed, bool DCNew) = GetDispensFlags(medCpoe);
                List<patientInfoClass> buff_medCarInfo = patientInfoClasses.Where(temp => temp.GUID == Master_GUID).ToList();
                buff_medCarInfo[0].調劑時間 = DateTime.Now.ToDateString();
                buff_medCarInfo = EditMedCarInfo(unDispensed, DCNew, buff_medCarInfo);
                update_medCarInfo.Add(buff_medCarInfo[0]);
            }
            List<object[]> list_medCarInfo_update = update_medCarInfo.ClassToSQL<patientInfoClass, enum_patient_info>();
            sQLControl_med_carinfo.UpdateByDefulteExtra(null, list_medCarInfo_update);
        }
        private (bool unDispensed, bool DCNew) GetDispensFlags(List<medCpoeClass> medCpoeClasses)
        {
            // unDispensed 是true 表示 有處方還沒有調劑
            // DCNew 是 true 表示 還有DC/New 還沒有被確認
            bool unDispensed = false;
            bool DCNew = false;
            for (int i = 0; i < medCpoeClasses.Count; i++)
            {
                if (medCpoeClasses[i].調劑狀態 == "") unDispensed = true;
                if (!medCpoeClasses[i].調劑異動.StringIsEmpty()) DCNew = true;
            }
            return (unDispensed, DCNew);
        }
        private List<patientInfoClass> EditMedCarInfo(bool unDispensed, bool DCNew, List<patientInfoClass> patientInfoClasses)
        {
            if (patientInfoClasses.Count != 1) return null;
            if (unDispensed)
            {
                patientInfoClasses[0].調劑狀態 = "";
            }
            else
            {
                patientInfoClasses[0].調劑狀態 = "Y";
            }
            if (DCNew)
            {
                patientInfoClasses[0].處方異動狀態 = "Y";
            }
            else
            {
                patientInfoClasses[0].處方異動狀態 = "";
            }
            return patientInfoClasses;
        }
        private List<patientInfoClass> GetPatInfo(SQLControl sQLControl_patient_info)
        {
            List<object[]> list_pat_info = sQLControl_patient_info.GetAllRows(null);
            List<patientInfoClass> sql_patInfo = list_pat_info.SQLToClass<patientInfoClass, enum_patient_info>();
            sql_patInfo = (from temp in sql_patInfo
                           where temp.更新時間.StringToDateTime() >= DateTime.Now.GetStartDate()
                           where temp.更新時間.StringToDateTime() <= DateTime.Now.GetEndDate()
                           select temp).ToList();
            return sql_patInfo;
        }
        private List<medCpoeClass> GetCpoe(SQLControl sQLControl_med_cpoe)
        {
            List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetAllRows(null);
            List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
            
            sql_medCpoe = (from temp in sql_medCpoe
                           where temp.更新時間.StringToDateTime() >= DateTime.Now.GetStartDate()
                           where temp.更新時間.StringToDateTime() <= DateTime.Now.GetEndDate()
                           select temp).ToList();
            return sql_medCpoe;
        }

    }

}
