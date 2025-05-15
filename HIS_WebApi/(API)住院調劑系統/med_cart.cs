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
using HIS_WebApi._API_藥品資料;
using K4os.Compression.LZ4.Internal;
using Google.Protobuf.WellKnownTypes;
using System.Data;
using NPOI.SS.Formula.Functions;





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
                return CheckCreatTable(sys_serverSettingClasses[0], new HIS_DB_Lib.enum_patient_info());
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
        //[HttpPost("init_med_info")]
        //public string init_med_info([FromBody] returnData returnData)
        //{
        //    MyTimerBasic myTimerBasic = new MyTimerBasic();
        //    returnData.Method = "med_cart/init_med_info";
        //    try
        //    {
        //        List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
        //        sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
        //        if (sys_serverSettingClasses.Count == 0)
        //        {
        //            returnData.Code = -200;
        //            returnData.Result = $"找無Server資料!";
        //            return returnData.JsonSerializationt();
        //        }
        //        return CheckCreatTable(sys_serverSettingClasses[0], new enum_med_info());
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData.Code = -200;
        //        returnData.Result = $"Exception : {ex.Message}";
        //        return returnData.JsonSerializationt(true);
        //    }
        //}
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
        [HttpPost("update_patientInfo")]
        public string update_patientInfo([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_patientInfo";
            try
            {
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");

                List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                settingPageClass 切帳設定 = settingPageClasses.myFind("medicine_cart", "切帳時間");
                settingPageClass 交車設定 = settingPageClasses.myFind("medicine_cart", "交車時間");
                if (IsInCutoffRange(切帳設定.設定值, 交車設定.設定值))
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Result = $"已超過切帳時間";
                    return returnData.JsonSerializationt(true);
                }                

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "藥檔資料");

                List<patientInfoClass> medCart_sql_add = new List<patientInfoClass>();
                List<patientInfoClass> medCart_sql_replace = new List<patientInfoClass>();
                List<patientInfoClass> medCart_sql_delete_buff = new List<patientInfoClass>();
                
                //sQLControl_patient_info.DeleteByBetween(null, (int)enum_patient_info.更新時間, starttime, endtime);

                List<patientInfoClass> input_patInfo = returnData.Data.ObjToClass<List<patientInfoClass>>();

                if (input_patInfo == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string 藥局 = input_patInfo[0].藥局;
                string 護理站 = input_patInfo[0].護理站;

                (string StartTime, string Endtime) = GetToday();

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                
                sql_patinfo = sql_patinfo.Where(temp => temp.護理站 == 護理站).ToList();
                Dictionary<string, List<patientInfoClass>> patInfoDictBedNum = patientInfoClass.ToDictByBedNum(sql_patinfo);

                List<Task> tasks = new List<Task>();

                foreach (patientInfoClass patientInfoClass in input_patInfo)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        patientInfoClass targetPatient = new patientInfoClass();
                        string 床號 = patientInfoClass.床號;

                        List<patientInfoClass> patientInfos = patientInfoClass.GetDictByBedNum(patInfoDictBedNum, 床號);
                        if (patientInfos.Count == 1)
                        {
                            targetPatient = patientInfos[0];
                        }
                        else
                        {
                            //List<patientInfoClass> patient = patientInfos.Where(temp => temp.占床狀態 == enum_bed_status_string.已佔床.GetEnumName()).ToList();
                            List<patientInfoClass> patient = patientInfos.OrderByDescending(p => DateTime.Parse(p.更新時間)).ToList();

                            if (patient.Count > 0) targetPatient = patient[0];

                        }

                        //if (patientInfoClass.GetDictByBedNum(patInfoDictBedNum, 床號).Count != 0)
                        //{
                        //    targetPatient = patientInfoClass.GetDictByBedNum(patInfoDictBedNum, 床號)[0];
                        //}
                        if (targetPatient.GUID.StringIsEmpty() == true)
                        {
                            patientInfoClass.GUID = Guid.NewGuid().ToString();
                            medCart_sql_add.LockAdd(patientInfoClass);
                        }
                        else
                        {
                            if (patientInfoClass.PRI_KEY != targetPatient.PRI_KEY)
                            {                         
                                if (targetPatient.PRI_KEY.StringIsEmpty()) //原本是空床，後來有人進來
                                {
                                    patientInfoClass.GUID = targetPatient.GUID;
                                    patientInfoClass.異動 = "Y";
                                    medCart_sql_replace.LockAdd(patientInfoClass);
                                }
                                else //原本有人，但已出院，又有新的人或是空床
                                {
                                    patientInfoClass.GUID = Guid.NewGuid().ToString();
                                    if(patientInfoClass.PRI_KEY.StringIsEmpty() ==false) patientInfoClass.異動 = "Y";
                                    targetPatient.占床狀態 = enum_bed_status_string.已出院.GetEnumName();
                                    medCart_sql_delete_buff.LockAdd(targetPatient);
                                    medCart_sql_add.LockAdd(patientInfoClass);
                                }   
                            }
                            else
                            {
                                patientInfoClass.GUID = targetPatient.GUID;
                                patientInfoClass.調劑時間 = targetPatient.調劑時間;
                                patientInfoClass.調劑狀態 = targetPatient.調劑狀態;
                                medCart_sql_replace.LockAdd(patientInfoClass);
                            }
                        }
                    })));
                }
                Task.WhenAll(tasks).Wait();




               
                List<object[]> list_medCart_add = medCart_sql_add.ClassToSQL<patientInfoClass, enum_patient_info>();
                List<object[]> list_medCart_replace = medCart_sql_replace.ClassToSQL<patientInfoClass, enum_patient_info>();
                List<object[]> list_medCart_delete_buff = medCart_sql_delete_buff.ClassToSQL<patientInfoClass, enum_patient_info>();

                if (list_medCart_add.Count > 0) sQLControl_patient_info.AddRows(null, list_medCart_add);
                if (list_medCart_replace.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCart_replace);
                if (list_medCart_delete_buff.Count > 0)
                {
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_medCart_delete_buff);
                    //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                    //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                    List<medCpoeClass> filterCpoe = sql_medCpoe
                        .Where(cpoe => medCart_sql_delete_buff.Any(medCart => medCart.GUID == cpoe.Master_GUID) && cpoe.公藥.StringIsEmpty()).ToList();
                    foreach (var item in filterCpoe)
                    {
                        if(item.PRI_KEY.Contains($"-[{enum_bed_status_string.已出院.GetEnumName()}]") == false)
                        {
                            item.PRI_KEY = item.PRI_KEY + $"-[{enum_bed_status_string.已出院.GetEnumName()}]";
                            if (item.調劑狀態 == "Y")
                            {
                                item.數量 = $"-{item.數量}";
                                item.劑量 = "--";
                                //item.頻次 = "--";
                                item.途徑 = "--";
                                item.單位 = "--";
                                item.調劑狀態 = "";
                                item.狀態 = "DC";
                                item.調劑異動 = "Y";
                                item.PRI_KEY += "-[DC]";
                            }
                            
                        }
                    }
                    List<object[]> list_medCpoe_delete_buff = filterCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    if (list_medCpoe_delete_buff.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_delete_buff);
                }
                list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);
                sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();

                List<patientInfoClass> patientInfoClasses = sql_patinfo.Where(temp => temp.護理站 == 護理站).ToList();

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
        ///更新處方資料
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
        [HttpPost("update_med_cpoe")]
        public string update_med_cpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_cpoe";
            try
            {
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");

                List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                settingPageClass 切帳設定 = settingPageClasses.myFind("medicine_cart", "切帳時間");
                settingPageClass 交車設定 = settingPageClasses.myFind("medicine_cart", "交車時間");
                if (IsInCutoffRange(切帳設定.設定值, 交車設定.設定值))
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Result = $"已超過切帳時間";
                    return returnData.JsonSerializationt(true);
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                List<medCpoeClass> input_medCpoe = returnData.Data.ObjToClass<List<medCpoeClass>>();
                if (input_medCpoe == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string 藥局 = input_medCpoe[0].藥局;
                string 護理站 = input_medCpoe[0].護理站;

                (string StartTime, string Endtime) = GetToday();

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();


                sql_patinfo = sql_patinfo.Where(temp => temp.護理站 == 護理站 && temp.占床狀態 != enum_bed_status_string.已出院.GetEnumName()).ToList();
                sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站).ToList();
                List<medCpoeClass> medCpoe_sql_add = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_delete_buf = new List<medCpoeClass>();
                List<medCpoeClass> medCpoe_sql_delete = new List<medCpoeClass>();
                //List<patientInfoClass> update_patInfo = new List<patientInfoClass>();

                Dictionary<string, List<patientInfoClass>> patInfoDict = patientInfoClass.ToDictByGUID(sql_patinfo);
                Dictionary<string, List<medCpoeClass>> sqlMedCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);
                Dictionary<string, List<medCpoeClass>> inputMedCpoeDict = medCpoeClass.ToDictByMasterGUID(input_medCpoe);
                foreach (string Master_GUID in patInfoDict.Keys)
                {
                    List<medCpoeClass> medCpoe_sql_add_buff = new List<medCpoeClass>();
                    List<patientInfoClass> patientInfoClasses = patientInfoClass.GetDictByGUID(patInfoDict, Master_GUID);
                    DateTime 調劑時間 = patientInfoClasses[0].調劑時間.StringToDateTime();
                    DateTime 現在時間 = DateTime.Now;
                    DateTime 預設時間 = DateTime.Parse("2001-01-01 00:00:00");

                    List<medCpoeClass> Cpoe_new = medCpoeClass.GetByMasterGUID(inputMedCpoeDict, Master_GUID);
                    List<medCpoeClass> Cpoe_old = medCpoeClass.GetByMasterGUID(sqlMedCpoeDict, Master_GUID);
                    List<medCpoeClass> Cpoe_public_new = Cpoe_new.Where(temp => temp.公藥 == "Y").ToList();
                    List<medCpoeClass> Cpoe_public_old = Cpoe_old.Where(temp => temp.公藥 == "Y").ToList();
                    Cpoe_new = Cpoe_new.Where(temp => temp.公藥.StringIsEmpty()).ToList();
                    Cpoe_old = Cpoe_old.Where(temp => temp.公藥.StringIsEmpty()).ToList();

                    
                    List<Task> tasks = new List<Task>();
                    tasks.Add(Task.Run(new Action(delegate 
                    {
                        foreach (medCpoeClass medCpoeClass in Cpoe_public_new)
                        {
                            medCpoeClass cpoe = Cpoe_public_old.Where(temp => temp.PRI_KEY.Contains(medCpoeClass.PRI_KEY)).FirstOrDefault();
                            if (cpoe == null) medCpoe_sql_add.LockAdd(medCpoeClass);
                        }
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        foreach (medCpoeClass medCpoeClass in Cpoe_public_old)
                        {
                            medCpoeClass cpoe = Cpoe_public_new.Where(temp => temp.PRI_KEY.Contains(medCpoeClass.PRI_KEY)).FirstOrDefault();
                            if (cpoe == null) medCpoe_sql_delete.LockAdd(medCpoeClass);
                        }

                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        foreach (medCpoeClass medCpoeClass in Cpoe_new)
                        {
                            medCpoeClass cpoe = Cpoe_old.Where(temp => temp.PRI_KEY.Contains(medCpoeClass.PRI_KEY)).FirstOrDefault();
                            if (cpoe == null)
                            {
                                if (調劑時間 != 預設時間 && 現在時間 > 調劑時間)
                                {
                                    medCpoeClass.調劑異動 = "Y";
                                    medCpoeClass.狀態 = "NEW";
                                }
                                medCpoe_sql_add.LockAdd(medCpoeClass);
                            }
                        }
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        
                    })));
                    Task.WhenAll(tasks).Wait();
                    tasks.Clear();
                    List<string> replace_list = new List<string>();
                    foreach (var add in medCpoe_sql_add)
                    {
                        medCpoeClass doubleCpoe = Cpoe_old
                            .Where(temp => temp.藥碼 == add.藥碼 && temp.途徑 == add.途徑 && temp.頻次 == add.頻次 && temp.數量 == add.數量)
                            .FirstOrDefault();
                        if (doubleCpoe != null)
                        {
                            add.調劑狀態 = doubleCpoe.調劑狀態;
                            replace_list.Add(doubleCpoe.PRI_KEY);
                        }
                    }
                    foreach (medCpoeClass medCpoeClass in Cpoe_old)
                    {
                        medCpoeClass cpoe = Cpoe_new.Where(temp => temp.PRI_KEY.Contains(medCpoeClass.PRI_KEY)).FirstOrDefault();
                        if (cpoe == null)
                        {
                            if (medCpoeClass.PRI_KEY.Contains("[DC]") == false)
                            {
                                if (replace_list.Contains(medCpoeClass.PRI_KEY))
                                {
                                    medCpoeClass.數量 = $"-{medCpoeClass.數量}";
                                    medCpoeClass.劑量 = "--";
                                    //medCpoeClass.頻次 = "--";
                                    //medCpoeClass.途徑 = "--";
                                    medCpoeClass.單位 = "--";
                                    medCpoeClass.調劑狀態 = "";
                                    medCpoeClass.狀態 = "DC";
                                    medCpoeClass.調劑異動 = "Y";
                                    medCpoeClass.PRI_KEY += "-[DC]系統";
                                    medCpoeClass.DC確認 = "Y";
                                    medCpoe_sql_replace.Add(medCpoeClass);
                                }
                                else
                                {
                                    if (medCpoeClass.調劑狀態 == "Y")
                                    {
                                        medCpoeClass.數量 = $"-{medCpoeClass.數量}";
                                        medCpoeClass.劑量 = "--";
                                        //medCpoeClass.頻次 = "--";
                                        //medCpoeClass.途徑 = "--";
                                        medCpoeClass.單位 = "--";
                                        medCpoeClass.調劑狀態 = "";
                                        medCpoeClass.狀態 = "DC";
                                        medCpoeClass.調劑異動 = "Y";
                                        medCpoeClass.PRI_KEY += "-[DC]";
                                        medCpoe_sql_replace.Add(medCpoeClass);
                                    }
                                    else
                                    {
                                        medCpoeClass.數量 = $"-{medCpoeClass.數量}";
                                        medCpoeClass.劑量 = "--";
                                        //medCpoeClass.頻次 = "--";
                                        //medCpoeClass.途徑 = "--";
                                        medCpoeClass.單位 = "--";
                                        medCpoeClass.調劑狀態 = "Y";
                                        medCpoeClass.狀態 = "DC";
                                        medCpoeClass.調劑異動 = "";
                                        medCpoeClass.PRI_KEY += "-[DC]系統";
                                        medCpoeClass.DC確認 = "Y";
                                        medCpoe_sql_replace.Add(medCpoeClass);
                                    }
                                }
                            }
                        }
                    }     
                }

                

                List<object[]> list_medCpoe_add = medCpoe_sql_add.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_medCpoe_delete = medCpoe_sql_delete.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                //List<object[]> list_patInfo_add = update_patInfo.ClassToSQL<patientInfoClass, enum_patient_info>();


                if (list_medCpoe_add.Count > 0) sQLControl_med_cpoe.AddRows(null, list_medCpoe_add);
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
                if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);

                list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<medCpoeClass> medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                medCpoe = medCpoe.Where(temp => temp.護理站 == 護理站).ToList();

                UpdateStatus(sql_patinfo, medCpoe);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCpoe;
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

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
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");

                List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                settingPageClass 切帳設定 = settingPageClasses.myFind("medicine_cart", "切帳時間");
                settingPageClass 交車設定 = settingPageClasses.myFind("medicine_cart", "交車時間");
                if (IsInCutoffRange(切帳設定.設定值, 交車設定.設定值))
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Result = $"已超過切帳時間";
                    return returnData.JsonSerializationt(true);
                }
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");

                List<medCpoeRecClass> input_medCpoe_rec = returnData.Data.ObjToClass<List<medCpoeRecClass>>();
                string 藥局 = input_medCpoe_rec[0].藥局;
                string 護理站 = input_medCpoe_rec[0].護理站;
                if (input_medCpoe_rec == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                (string StartTime, string Endtime) = GetToday();

                SQLControl sQLControl_med_cpoe_rec = new SQLControl(Server, DB, "med_cpoe_rec", UserName, Password, Port, SSLMode);

                List<object[]> sql_medCpoeRecClass = sQLControl_med_cpoe_rec.GetRowsByBetween(null, (int)enum_med_cpoe_rec.更新時間, StartTime, Endtime);
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
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");

                List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                settingPageClass 切帳設定 = settingPageClasses.myFind("medicine_cart", "切帳時間");
                settingPageClass 交車設定 = settingPageClasses.myFind("medicine_cart", "交車時間");
                if (IsInCutoffRange(切帳設定.設定值, 交車設定.設定值))
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Result = $"已超過切帳時間";
                    return returnData.JsonSerializationt(true);
                }

                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");

                List<bedStatusClass> input_bedStatus = returnData.Data.ObjToClass<List<bedStatusClass>>();
                if (input_bedStatus == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                SQLControl sQLControl = new SQLControl(Server, DB, "bed_status", UserName, Password, Port, SSLMode);
                (string StartTime, string Endtime) = GetToday();
                List<object[]> sql_bedStatusClass = sQLControl.GetRowsByBetween(null, (int)enum_bed_status.轉床時間, StartTime, Endtime);
                List<bedStatusClass> list_bedStatusClass = sql_bedStatusClass.SQLToClass<bedStatusClass, enum_bed_status>();

                Dictionary<string, List<bedStatusClass>> inputBedStatusDict = bedStatusClass.ToDictByID(input_bedStatus);
                Dictionary<string, List<bedStatusClass>> sqlBedStatusDict = bedStatusClass.ToDictByID(list_bedStatusClass);
                List<bedStatusClass> add_bedStatusClass = new List<bedStatusClass>();
                foreach (var Master_GUID in inputBedStatusDict.Keys)
                {
                    List<bedStatusClass> input = bedStatusClass.GetByID(inputBedStatusDict, Master_GUID);
                    List<bedStatusClass> sql = bedStatusClass.GetByID(sqlBedStatusDict, Master_GUID);

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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "藥檔資料");

                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                            
                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                sql_patinfo = sql_patinfo.Where(temp => temp.護理站 == 護理站).ToList();

                sql_patinfo = UpdateStatus(sql_patinfo, sql_medCpoe);               
                sql_patinfo.Sort(new patientInfoClass.ICP_By_bedNum());

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_patinfo;
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
                string API =HIS_WebApi.Method. GetServerAPI("Main", "網頁", "API01");

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
                    List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                    settingPageClass settingPage = settingPageClasses.myFind("medicine_cart", "DC處方確認後取消顯示");

                    List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, GUID);
                    sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                    if(settingPage.設定值 == true.ToString())
                    {
                        sql_medCpoe = sql_medCpoe.Where(temp => temp.DC確認.StringIsEmpty()).ToList();
                    }
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
                        List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, returnData.Value, Codes); //我誤會你了
                        Dictionary<string, List<medClass>> medClassDict = medClass.CoverToDictionaryByCode(medClasses);
                        //List<DeviceBasic> deviceBasics = deviceApiClass.Get_dps_med(API, returnData.Value);
                        foreach (medCpoeClass medCpoeClass in sql_medCpoe)
                        {
                            //DeviceBasic deviceBasic = deviceBasics.Where(temp => temp.BarCode == medCpoeClass.藥碼).FirstOrDefault();

                            if (medClassDict.ContainsKey(medCpoeClass.藥碼)) 
                            {
                                medClass medClass_buff = medClassDict[medCpoeClass.藥碼].FirstOrDefault();
                                if (medClass_buff.DeviceBasics.Count != 0) 
                                {
                                    medCpoeClass.調劑台 = "Y";
                                }
                                else
                                {
                                    medCpoeClass.調劑台 = "";
                                }

                            } 
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
        /// <summary>
        ///以藥局取得已出院護理站
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":"調劑台"
        ///         "ValueAry":["藥局"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_cart_discharge")]
        public string get_cart_discharge([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_Cart/get_cart_discharge";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"藥局\"]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");
                string tableName_patient_info = "patient_info";
                string tableName_med_cpoe = "med_cpoe";

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, tableName_patient_info, UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, tableName_med_cpoe, UserName, Password, Port, SSLMode);               

                (string StartTime, string Endtime) = GetToday();
               
                string command = $"SELECT * FROM {DB}.{tableName_patient_info} WHERE 更新時間 BETWEEN '{StartTime}' AND '{Endtime}' AND 藥局 = '{藥局}' AND 占床狀態 = '{enum_bed_status_string.已出院.GetEnumName()}';";
                DataTable dataTable_patient_info = sQLControl_patient_info.WtrteCommandAndExecuteReader(command);
                List<object[]> list_pat_carInfo = dataTable_patient_info.DataTableToRowList();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                sql_patinfo = sql_patinfo.Where(temp => temp.調劑狀態.StringIsEmpty() == true).ToList();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);

                List<Task> tasks = new List<Task>();
                if (sql_patinfo.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Data = sql_patinfo;
                    returnData.Result = $"取得{藥局} 共{sql_patinfo.Count}床 已出院";
                    return returnData.JsonSerializationt(true);
                }
                foreach (var item in sql_patinfo)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<medCpoeClass> targetCpoe = medCpoeClass.GetByMasterGUID(medCpoeDict, item.GUID);
                        targetCpoe = targetCpoe.Where(temp => temp.PRI_KEY.Contains("[DC]")).ToList();
                        item.處方 = targetCpoe;
                    })));
                }
                sql_patinfo = sql_patinfo.Where(temp => temp.處方 == null).ToList();
                List<string> cart = sql_patinfo.Select(temp => temp.護理站).Distinct().ToList();
                cart.Sort();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = cart;
                returnData.Result = $"取得{藥局} 共{cart.Count}個護理站有出院病人，其中{sql_patinfo.Count}床 已出院";
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
        ///以藥局、護理站取得已出院資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":"調劑台"
        ///         "ValueAry":["藥局","護理站"]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_patient_discharge")]
        public string get_patient_discharge([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_Cart/get_patient_discharge";
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);


                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                sql_patinfo = sql_patinfo.Where(temp => temp.護理站 == 護理站 && temp.占床狀態 == enum_bed_status_string.已出院.GetEnumName()).ToList();

                Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);

                List<Task> tasks = new List<Task>();
                if (sql_patinfo.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Data = sql_patinfo;
                    returnData.Result = $"取得{藥局} {護理站} 共{sql_patinfo.Count}床 已出院";
                    return returnData.JsonSerializationt(true);
                }
                
                foreach (var item in sql_patinfo)
                {
                    //if (item.調劑狀態.StringIsEmpty() == true) continue;
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<medCpoeClass> targetCpoe = medCpoeClass.GetByMasterGUID(medCpoeDict, item.GUID);
                        targetCpoe = targetCpoe.Where(temp => temp.PRI_KEY.Contains("[DC]")).ToList();
                        item.處方 = targetCpoe;
                    })));
                }
                Task.WhenAll(tasks).Wait();
                sql_patinfo.Sort(new patientInfoClass.ICP_By_bedNum());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_patinfo;
                returnData.Result = $"取得{藥局} {護理站} 共{sql_patinfo.Count}床 已出院";
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
        ///以藥局取得未調劑藥車
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[藥局]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_cart_with_NOdispense")]
        public string get_cart_with_NOdispense([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_Cart/get_patient_with_NOdispense";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"藥局\"]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");
                string tableName_patient_info = "patient_info";
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, tableName_patient_info, UserName, Password, Port, SSLMode);
                (string StartTime, string Endtime) = GetToday();
                
                string command = $"SELECT * FROM {DB}.{tableName_patient_info} WHERE 更新時間 BETWEEN '{StartTime}' AND '{Endtime}' AND 藥局 = '{藥局}' AND 占床狀態 != '{enum_bed_status_string.已出院.GetEnumName()}'  AND 調劑狀態 = '';";
                DataTable dataTable_patient_info = sQLControl_patient_info.WtrteCommandAndExecuteReader(command);
                List<object[]> list_pat_carInfo = dataTable_patient_info.DataTableToRowList();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                              
                List<string> cart = sql_patinfo.Select(temp => temp.護理站).Distinct().ToList();
                cart.Sort();
                sql_patinfo.Sort(new patientInfoClass.ICP_By_bedNum());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = cart;
                returnData.Result = $"取得{藥局} 共{cart.Count}個藥局未完成調劑，其中{sql_patinfo.Count}床 未調劑";
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
        ///以藥局、護理站取得未調劑處方資料
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");
                string tableName_patient_info = "patient_info";
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, tableName_patient_info, UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                settingPageClass settingPage_DC = settingPageClasses.myFind("medicine_cart", "DC處方確認後取消顯示");
                settingPageClass settingPage_public = settingPageClasses.myFind("medicine_cart", "顯示公藥");

                bool flag_DC = settingPage_DC.設定值 == true.ToString() ? true : false;
                bool flag_public = settingPage_public.設定值 == true.ToString() ? false : true;

                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
               
                sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站 && temp.公藥.StringIsEmpty() == flag_public && temp.PRI_KEY.Contains(enum_bed_status_string.已出院.GetEnumName()) == false && temp.DC確認.StringIsEmpty() == flag_DC).ToList();
                Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);

                //string command = $"SELECT * FROM {DB}.{tableName_patient_info} WHERE 更新時間 BETWEEN '{StartTime}' AND '{Endtime}' AND 藥局 = '{藥局}' AND 護理站 = '{護理站}' AND 占床狀態 != '{enum_bed_status_string.已出院.GetEnumName()}';";
                string command = $"SELECT * FROM {DB}.{tableName_patient_info} WHERE 更新時間 BETWEEN '{StartTime}' AND '{Endtime}' AND 藥局 = '{藥局}' AND 護理站 = '{護理站}' ;";

                DataTable dataTable_patient_info = sQLControl_patient_info.WtrteCommandAndExecuteReader(command);
                List<object[]> list_pat_carInfo = dataTable_patient_info.DataTableToRowList();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<Task> tasks = new List<Task>();
                
                sql_patinfo = UpdateStatus(sql_patinfo, sql_medCpoe);
                sql_patinfo = sql_patinfo.Where(temp => temp.調劑狀態.StringIsEmpty() && temp.占床狀態 != enum_bed_status_string.已出院.GetEnumName()).ToList();

                            
                foreach ( var item in sql_patinfo)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<medCpoeClass> targetCpoe = medCpoeClass.GetByMasterGUID(medCpoeDict, item.GUID);
                        targetCpoe = targetCpoe.Where(temp => temp.調劑狀態 == "").ToList();
                        item.處方 = targetCpoe;
                    })));  
                }
                Task.WhenAll(tasks).Wait();
                tasks.Clear();
                
                sql_patinfo.Sort(new patientInfoClass.ICP_By_bedNum());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_patinfo;
                returnData.Result = $"取得{藥局} {護理站} 共{sql_patinfo.Count}床 未調劑";
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
        ///以藥局取得未覆核藥車資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[藥局]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_cart_with_NOcheck")]
        public string get_cart_with_NOcheck([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_Cart/get_cart_with_NOcheck";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"藥局\"]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");
                string tableName_patient_info = "patient_info";
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, tableName_patient_info, UserName, Password, Port, SSLMode);
                (string StartTime, string Endtime) = GetToday();

                string command = $"SELECT * FROM {DB}.{tableName_patient_info} WHERE 更新時間 BETWEEN '{StartTime}' AND '{Endtime}' AND 藥局 = '{藥局}' AND 占床狀態 != '{enum_bed_status_string.已出院.GetEnumName()}'  AND 覆核狀態 = '';";
                DataTable dataTable_patient_info = sQLControl_patient_info.WtrteCommandAndExecuteReader(command);
                List<object[]> list_pat_carInfo = dataTable_patient_info.DataTableToRowList();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();

                List<string> cart = sql_patinfo.Select(temp => temp.護理站).Distinct().ToList();
                cart.Sort();
                sql_patinfo.Sort(new patientInfoClass.ICP_By_bedNum());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = cart;
                returnData.Result = $"取得{藥局} 共{cart.Count}個藥局未完成調劑，其中{sql_patinfo.Count}床 未覆核";
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
        ///以GUID取得未覆核處方資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Value":"調劑台"
        ///         "ValueAry":["藥局",護理站]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_patient_with_NOcheck")]
        public string get_patient_with_NOcheck([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_Cart/get_patient_with_NOcheck";
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                sql_patinfo = sql_patinfo.Where(temp => temp.護理站 == 護理站).ToList();

                Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.ToDictByMasterGUID(sql_medCpoe);

                List<Task> tasks = new List<Task>();
                sql_patinfo = UpdateStatus(sql_patinfo, sql_medCpoe);
                sql_patinfo = sql_patinfo.Where(temp => temp.覆核狀態.StringIsEmpty() == false && temp.占床狀態 != enum_bed_status_string.已出院.GetEnumName()).ToList();
                foreach (var item in sql_patinfo)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        List<medCpoeClass> targetCpoe = medCpoeClass.GetByMasterGUID(medCpoeDict, item.GUID);
                        targetCpoe = targetCpoe.Where(temp => temp.覆核狀態 == "").ToList();
                        item.處方 = targetCpoe;
                    })));
                }
                Task.WhenAll(tasks).Wait();
                sql_patinfo.Sort(new patientInfoClass.ICP_By_bedNum());
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_patinfo;
                returnData.Result = $"取得{藥局} {護理站} 共{sql_patinfo.Count}床 未覆核";
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
        [HttpPost("get_medCpoe_by_cart")]
        public string get_medCpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_medCpoe_by_cart";
            try
            {
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");
                
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];

                (string StartTime, string Endtime) = GetToday();

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站).ToList();
                sql_medCpoe.Sort(new medCpoeClass.ICP_By_bedNum());

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
        [HttpPost("get_medCpoe_by_MasterGUID")]
        public string get__medCpoe_by_MasterGUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_medCpoe_by_MasterGUID";
            try
            {
                if(returnData.ValueAry.Count != 1)
                {
                    returnData.Result = "ValueAry應為[\"Master_GUID\"]";
                    returnData.Code = -200;
                    return returnData.JsonSerializationt(true);
                }
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");
                string Master_GUID = returnData.ValueAry[0];
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null,(int)enum_med_cpoe.Master_GUID, Master_GUID);
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
                if (returnData.ValueAry.Count !=  2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 應為[\"藥局\",\"護理站\"]";
                    return returnData.JsonSerializationt(true);
                }
                string 護理站 = returnData.ValueAry[1];
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl = new SQLControl(Server, DB, "bed_status", UserName, Password, Port, SSLMode);

                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_bed_status = sQLControl.GetRowsByBetween(null, (int)enum_bed_status.轉床時間, StartTime, Endtime);
                List<bedStatusClass> bedStatusClasses = list_bed_status.SQLToClass<bedStatusClass, enum_bed_status>();
                bedStatusClasses = bedStatusClasses.Where(temp => temp.轉床前護理站床號.Contains(護理站) || temp.轉床後護理站床號.Contains(護理站)).ToList();
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");
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
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");

                string Master_GUID = returnData.Value;
                string[] GUID = returnData.ValueAry[0].Split(";");
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> medCpoe_sql_replace = new List<medCpoeClass>();
                List<medCpoeClass> debit_medcpoe = new List<medCpoeClass> ();
                List<medCpoeClass> refund_medcpoe = new List<medCpoeClass>();
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
                        if (medCpoeClass.調劑狀態.StringIsEmpty())
                        {
                            medCpoeClass.調劑狀態 = "Y";
                            if (medCpoeClass.PRI_KEY.Contains("DC"))
                            {
                                refund_medcpoe.Add(medCpoeClass);
                            }
                            else
                            {
                                debit_medcpoe.Add(medCpoeClass);
                            }
                        }
                        
                        if (medCpoeClass.狀態 == "DC") medCpoeClass.DC確認 = "Y";
                        //medCpoeClass.狀態 = "";      
                        //medCpoeClass.調劑異動 = "";
                    }
                    else
                    {
                        if(medCpoeClass.調劑狀態.StringIsEmpty() == false)
                        {
                            medCpoeClass.調劑狀態 = "";
                            refund_medcpoe.Add(medCpoeClass);
                        }                  
                    }
                    medCpoe_sql_replace.Add(medCpoeClass);
                }
                

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
                    List<object[]> list_patInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_patInfo_replace);
                })));
                Task.WhenAll(tasks).Wait();
                tasks.Clear();
                //扣帳
                returnData returnData_debit = new returnData();
                returnData returnData_refund = new returnData();
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_debit = ExcuteTrade(returnData, debit_medcpoe, "系統領藥");
                    //Logger.Log("debit", $"{returnData_debit.JsonSerializationt(true)}");
                })));
                //退帳
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_refund = ExcuteTrade(returnData, refund_medcpoe, "系統退藥");
                    //Logger.Log("refund", $"{returnData_refund.JsonSerializationt(true)}");
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
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"病人GUID\",\"藥品GUID\", \"Y/ \"]";
                    return returnData.JsonSerializationt(true);
                }

                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

                string Master_GUID = returnData.ValueAry[0];
                string GUID = returnData.ValueAry[1];
                string 調劑狀態 = returnData.ValueAry[2];
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> medCpoe_sql_replace = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> debit_medcpoe = new List<medCpoeClass>();
                List<medCpoeClass> refund_medcpoe = new List<medCpoeClass>();
                if (medCpoe_sql_replace.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                medCpoe_sql_replace = medCpoe_sql_replace.Where(temp => temp.GUID == GUID).ToList();
                medCpoe_sql_replace[0].調劑狀態 = 調劑狀態;

                if (調劑狀態.StringIsEmpty() == true)
                {
                    refund_medcpoe.Add(medCpoe_sql_replace[0]);
                }
                else
                {
                    if (medCpoe_sql_replace[0].PRI_KEY.Contains("DC")) 
                    {
                        medCpoe_sql_replace[0].DC確認 = "Y";
                        refund_medcpoe.Add(medCpoe_sql_replace[0]);
                    }
                    else
                    {
                        debit_medcpoe.Add(medCpoe_sql_replace[0]);
                    }
                }
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
                    if (調劑狀態.StringIsEmpty() == false) sql_patinfo[0].調劑時間 = DateTime.Now.ToDateString();
                    //sql_patinfo = EditpatInfo(unDispensed, DCNew, sql_patinfo);
                    List<object[]> list_patInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_patInfo_replace);
                    
                })));
                returnData returnData_debit = new returnData();
                returnData returnData_refund = new returnData();
                //扣帳
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_debit = ExcuteTrade(returnData, debit_medcpoe, "系統領藥");
                    //Logger.Log("debit", $"{returnData_debit.JsonSerializationt(true)}");

                })));
                //退帳
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_refund = ExcuteTrade(returnData, refund_medcpoe, "系統退藥");
                    //Logger.Log("refund", $"{returnData_refund.JsonSerializationt(true)}");
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

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
                List<medCpoeClass> debit_medcpoe = new List<medCpoeClass>();
                List<medCpoeClass> refund_medcpoe = new List<medCpoeClass>();

                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < sql_medCpoe.Count; i++)
                {
                    if (GUIDs.Contains(sql_medCpoe[i].GUID))
                    {
                        sql_medCpoe[i].調劑狀態 = "Y";
                        if (sql_medCpoe[i].PRI_KEY.Contains("DC"))
                        {
                            sql_medCpoe[i].DC確認 = "Y";
                            refund_medcpoe.Add(sql_medCpoe[i]);
                        }
                        else
                        {
                            debit_medcpoe.Add(sql_medCpoe[i]);
                        }

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
                    SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                    List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.GUID, Master_GUID);
                    List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                    sql_patinfo[0].調劑時間 = DateTime.Now.ToDateString();
                    //sql_patinfo = EditpatInfo(unDispensed, DCNew, sql_patinfo);
                    List<object[]> list_patInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_patInfo_replace);
                    //string 護理站 = list_pat_carInfo[0][(int)enum_patient_info.護理站].ObjectToString();
                    //string 床號 = list_pat_carInfo[0][(int)enum_patient_info.床號].ObjectToString();
                    //str += $"更新{護理站} 第{床號}床 調劑狀態、調劑時間和處方異動狀態";
                    
                })));
                returnData returnData_debit = new returnData();
                returnData returnData_refund = new returnData();
                //扣帳
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_debit = ExcuteTrade(returnData, debit_medcpoe, "系統領藥");
                    //Logger.Log("debit", $"{returnData_debit.JsonSerializationt(true)}");
                })));
                //退帳
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_refund = ExcuteTrade(returnData, refund_medcpoe, "系統退藥");
                    //Logger.Log("refund", $"{returnData_refund.JsonSerializationt(true)}");

                })));
                Task.WhenAll(tasks).Wait();
                
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"更新處方紀錄共{GUIDs.Count}筆";
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

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
                string[] GUIDs = returnData.ValueAry[0].Split(";");
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                //List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.護理站, 護理站);
                //List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                //List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByDefult(null, (int)enum_patient_info.護理站, 護理站);
                //List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                //List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                //List<patientInfoClass> sql_patinfo = GetPatInfo(sQLControl_patient_info);

                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();

                List<medCpoeClass> debit_medcpoe = new List<medCpoeClass>();
                List<medCpoeClass> refund_medcpoe = new List<medCpoeClass>();

                List<string> carinfo_GUID = new List<string>();
                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                sql_medCpoe = sql_medCpoe.Where(temp => GUIDs.Contains(temp.GUID)).ToList();
                List<string> patientGUID = new List<string>();
                foreach (var item in sql_medCpoe)
                {
                    patientGUID.Add(item.Master_GUID);
                    if(item.PRI_KEY.Contains("DC"))
                    {
                        item.DC確認 = "Y";
                        refund_medcpoe.Add(item);
                    }
                    else
                    {
                        debit_medcpoe.Add(item);
                    }
                    item.調劑狀態 = "Y";
                }
                
                int buff = 0;
                //for (int i = 0; i < sql_medCpoe.Count; i++)
                //{
                //    if (GUIDs.Contains(sql_medCpoe[i].GUID))
                //    {
                //        debit_medcpoe.Add(sql_medCpoe[i]);
                //        sql_medCpoe[i].調劑狀態 = "Y";
                //        sql_medCpoe[i].狀態 = "";
                //        sql_medCpoe[i].調劑異動 = "";
                //        buff += 1;
                //        //if (!carinfo_GUID.Contains(sql_medCpoe[i].Master_GUID)) carinfo_GUID.Add(sql_medCpoe[i].Master_GUID);
                //    }
                //}
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<object[]> update_medCpoe = sql_medCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    sQLControl_med_cpoe.UpdateByDefulteExtra(null, update_medCpoe);
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    sql_patinfo = sql_patinfo.Where(temp => patientGUID.Contains(temp.GUID)).ToList();
                    foreach (var item in sql_patinfo)
                    {
                        item.調劑時間 = DateTime.Now.ToDateString();
                    }
                    List<object[]> list_patInfo_replace = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                    sQLControl_patient_info.UpdateByDefulteExtra(null, list_patInfo_replace);

                })));
                returnData returnData_debit = new returnData();
                returnData returnData_refund = new returnData();
                //扣帳
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_debit = ExcuteTrade(returnData, debit_medcpoe, "系統領藥");
                    //Logger.Log("debit", $"{returnData_debit.JsonSerializationt(true)}");
                })));
                //退帳
                tasks.Add(Task.Run(new Action(delegate
                {
                    returnData_refund = ExcuteTrade(returnData, refund_medcpoe, "系統退藥");
                    //Logger.Log("refund", $"{returnData_refund.JsonSerializationt(true)}");
                })));

                Task.WhenAll(tasks).Wait();
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"更新藥車: {護理站}處方紀錄共{buff}筆";
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
        ///逐床確認藥品覆核狀態
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
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");

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
                foreach (var medCpoeClass in sql_medCpoe)
                {
                    if (GUID.Contains(medCpoeClass.GUID))
                    {                        
                        medCpoeClass.覆核狀態 = "Y";                       
                    }
                    else
                    {
                        medCpoeClass.覆核狀態 = "";
                    }
                    medCpoe_sql_replace.Add(medCpoeClass);
                }
                List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
         
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCpoe_sql_replace;
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
        /// <summary>
        ///以GUID調整藥品覆核狀態
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
        [HttpPost("double_check_by_GUID")]
        public string double_check_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "check_dispense_by_GUID";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"病人GUID\",\"藥品GUID\", \"Y/ \"]";
                    return returnData.JsonSerializationt(true);
                }

                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

                string Master_GUID = returnData.ValueAry[0];
                string GUID = returnData.ValueAry[1];
                string 覆核狀態 = returnData.ValueAry[2];
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, GUID);
                List<medCpoeClass> medCpoe_sql_replace = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                
                if (medCpoe_sql_replace.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }

                medCpoe_sql_replace[0].覆核狀態 = 覆核狀態;
                
                List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCpoe_sql_replace;
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
        ///以GUID確認藥品覆核
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["處方GUID;處方GUID", "處方Master_GUID"]";
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("check_by_GUID")]
        public string check_by_GUID([FromBody] returnData returnData)
        {
            ///未調藥品的全部覆核
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "check_by_GUID";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

                if (returnData.ValueAry == null)
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
                List<medCpoeClass> debit_medcpoe = new List<medCpoeClass>();

                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = 200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }
                for (int i = 0; i < sql_medCpoe.Count; i++)
                {
                    if (GUIDs.Contains(sql_medCpoe[i].GUID))
                    {
                        sql_medCpoe[i].覆核狀態 = "Y";
                    }
                }

                List<object[]> update_medCpoe = sql_medCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                sQLControl_med_cpoe.UpdateByDefulteExtra(null, update_medCpoe);
                
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"更新處方紀錄共{GUIDs.Count}筆";
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
        ///以GUID確認藥品覆核
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["處方GUID;處方GUID", "護理站"]";
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("check_by_cart")]
        public string check_by_cart([FromBody] returnData returnData)
        {
            //針對整個藥車調劑(只能確認)
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "dispensed_by_cart";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");

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
                string[] GUIDs = returnData.ValueAry[0].Split(";");
                string 護理站 = returnData.ValueAry[1];

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetRowsByBetween(null, (int)enum_patient_info.更新時間, StartTime, Endtime);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                if (sql_medCpoe.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"無對應處方";
                    return returnData.JsonSerializationt(true);
                }

                List<string> carinfo_GUID = new List<string>();
                
                sql_medCpoe = sql_medCpoe.Where(temp => GUIDs.Contains(temp.GUID)).ToList();
                List<string> patientGUID = new List<string>();
                foreach (var item in sql_medCpoe)
                {
                    patientGUID.Add(item.Master_GUID);
                    item.覆核狀態 = "Y";
                }

                List<object[]> update_medCpoe = sql_medCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                sQLControl_med_cpoe.UpdateByDefulteExtra(null, update_medCpoe);
 
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
                returnData.Result = $"更新藥車: {護理站}處方紀錄共{GUIDs.Length}筆";
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
               
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
                string API = Method.GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                //List<medCpoeClass> sql_medCpoe = GetCpoe(sQLControl_med_cpoe);
                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                settingPageClass settingPage_DC = settingPageClasses.myFind("medicine_cart", "DC處方確認後取消顯示");
                settingPageClass settingPage_public = settingPageClasses.myFind("medicine_cart", "顯示公藥");

                bool flag_DC = settingPage_DC.設定值 == true.ToString() ? true : false;
                bool flag_public = settingPage_public.設定值 == true.ToString() ? false : true;
                sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站 && temp.公藥.StringIsEmpty() == flag_public　&& temp.PRI_KEY.Contains(enum_bed_status_string.已出院.GetEnumName()) == false).ToList();
                if(flag_DC == true)
                {
                    sql_medCpoe = sql_medCpoe.Where(temp => temp.DC確認.StringIsEmpty() == true).ToList();
                }
                
                //sql_medCpoe = sql_medCpoe.Where(temp => temp.公藥.StringIsEmpty() == flag_public).ToList();
                //sql_medCpoe = sql_medCpoe.Where(temp => temp.PRI_KEY.Contains(enum_bed_status_string.已出院.GetEnumName()) == false).ToList();
                //sql_medCpoe = sql_medCpoe.Where(temp => temp.DC確認.StringIsEmpty() == flag_DC).ToList();

                //if (settingPage_DC.設定值 == true.ToString())
                //{
                //    sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站 && temp.公藥.StringIsEmpty() && temp.PRI_KEY.Contains(enum_bed_status_string.已出院.GetEnumName()) == false&& temp.DC確認.StringIsEmpty()).ToList();
                //}
                //else
                //{
                //    sql_medCpoe = sql_medCpoe.Where(temp => temp.護理站 == 護理站 && temp.公藥.StringIsEmpty() && temp.PRI_KEY.Contains(enum_bed_status_string.已出院.GetEnumName()) == false).ToList();
                //}

                List<medClass> medClasses = new List<medClass>();
                Dictionary<string, List<medClass>> medClassDict = new Dictionary<string, List<medClass>>();
                if(returnData.Value != "all")
                {
                    List<string> codes = sql_medCpoe.Select(temp => temp.藥碼).Distinct().ToList();
                    medClasses = medClass.get_dps_medClass_by_code(API, returnData.Value, codes);
                    medClassDict = medClass.CoverToDictionaryByCode(medClasses);
                }
                
                
               
                List<medQtyClass> medQtyClasses = sql_medCpoe
                    .GroupBy(temp => temp.藥碼)
                    .Select(grouped =>
                    {
                        medCpoeClass medCpoe = grouped.First();
                        string 調劑台 = "";
                        string 大瓶點滴 = "";
                        if (returnData.Value == "all")
                        {
                            調劑台 = "Y";
                        }
                        else
                        {
                            medClass meds = medClass.SortDictionaryByCode(medClassDict, grouped.Key).FirstOrDefault();
                            if(meds != null && meds.DeviceBasics.Count > 0) 調劑台 = "Y";                           
                        }
                        List<bedListClass> bedLists = grouped.Select(value =>
                        {
                            string 自費PRN = "";
                            if (value.頻次.ToLower().Contains("prn") && value.自購 == "Y") 自費PRN = "Y";                           
                            return new bedListClass
                            {
                                GUID = value.GUID,
                                Master_GUID = value.Master_GUID,
                                床號 = value.床號,
                                數量 = value.數量,
                                劑量 = value.劑量,
                                大瓶點滴 = value.大瓶點滴,
                                調劑狀態 = value.調劑狀態,
                                覆核狀態 = value.覆核狀態,
                                頻次 = value.頻次,
                                自費 = value.自購,
                                自費PRN = 自費PRN
                            };

                        }).OrderBy(bedlist => bedlist, new bedListClass.ICP_By_bedNum()).ToList();
                        if(bedLists.Any(item => item.大瓶點滴 == "L")) 大瓶點滴 = "L";
                        return new medQtyClass
                        {
                            藥品名 = medCpoe.藥品名,
                            藥碼 = grouped.Key,
                            單位 = medCpoe.單位,
                            針劑 = medCpoe.針劑,
                            口服 = medCpoe.口服,
                            冷儲 = medCpoe.冷儲,
                            儲位 = medCpoe.儲位,
                            調劑台 = 調劑台,
                            大瓶點滴 = 大瓶點滴,
                            病床清單 = bedLists
                        };
                    }).ToList();
                
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
                string API = Method.GetServerAPI("Main", "網頁", "API01");
                List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
                settingPageClass 切帳設定 = settingPageClasses.myFind("medicine_cart", "切帳時間");
                if (IsAfterCutoff(切帳設定.設定值) == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"執行失敗：目前時間尚未超過{切帳設定.設定值}。";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥局, 護理站]";
                    return returnData.JsonSerializationt(true);
                }
                string 藥局 = returnData.ValueAry[0];
                string 護理站 = returnData.ValueAry[1];
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_carlist = new SQLControl(Server, DB, "med_carlist", UserName, Password, Port, SSLMode);
                List<object[]> list_med_carlist = sQLControl_med_carlist.GetRowsByDefult(null, (int)enum_med_carList.藥局, 藥局);
                List<medCarListClass> sql_medCarList = list_med_carlist.SQLToClass<medCarListClass, enum_med_carList>();

                (string StartTime, string Endtime) = GetToday();
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByBetween(null, (int)enum_med_cpoe.更新時間, StartTime, Endtime);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

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
                string API = Method.GetServerAPI("Main", "網頁", "API01");              

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
                        if (medClasses.Count > 0 && medClasses[0].DeviceBasics.Count > 0)
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
        [HttpPost("debit")]
        public string debit([FromBody] returnData returnData)
        {
            try
            {
                returnData.Method = "debit";
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                if(returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "ValueAry不得為空";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = "ValueAry不得為空";
                    return returnData.JsonSerializationt(true);
                }
                if(returnData.UserName == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "UserName應為 \"操作人\"";
                    return returnData.JsonSerializationt(true);
                }
                string 操作人 = returnData.UserName;
                string 調劑台 = returnData.ServerName;
                string[] GUIDs = returnData.ValueAry[0].Split(";");
                string API_Server = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");
                List<OrderClass> orderClasses = new List<OrderClass>();
                for (int i =0; i < GUIDs.Length; i++)
                {
                    string GUID = GUIDs[i].Trim();
                    OrderClass orderClass = OrderClass.get_by_pri_key(API_Server, GUID);
                    
                    if(orderClass != null && orderClass.狀態 == "未過帳") orderClasses.Add(orderClass);
                    if(orderClass.狀態 == "已過帳" && orderClass.實際調劑量 == "0") orderClasses.Add(orderClass);
                }
                if (orderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "order資料狀態不符";
                    return returnData.JsonSerializationt(true);
                }
                List<class_OutTakeMed_data> outTakeMed_Datas = new List<class_OutTakeMed_data>();
                List<string> Codes = orderClasses.Select(temp => temp.藥品碼).Distinct().ToList();
                if (Codes.Count == 1) Codes[0] = Codes[0] + ",";
                List<medClass> medClasses = medClass.get_dps_medClass_by_code(API_Server, 調劑台, Codes);
                Dictionary<string, List<medClass>> medClassDict = medClass.CoverToDictionaryByCode(medClasses);

                foreach (var item in orderClasses)
                {
                    medClass medClass_buff = medClassDict[item.藥品碼].FirstOrDefault();
                    if (medClass_buff.DeviceBasics.Count != 0)
                    {
                        class_OutTakeMed_data outTakeMed_Data = new class_OutTakeMed_data()
                        {
                            //PRI_KEY = item.GUID,
                            Order_GUID = item.GUID,
                            護理站 = item.病房,
                            成本中心 = "住院藥車",
                            藥品碼 = item.藥品碼,
                            藥名 = item.藥品名稱,
                            操作人 = 操作人,
                            類別 = item.藥袋類型,
                            病人姓名 = item.病人姓名,
                            病歷號 = item.病歷號,
                            交易量 = item.交易量,
                            開方時間 = item.開方日期,
                            電腦名稱 = 調劑台,
                            功能類型 = "-1" //掃碼領藥
                        };
                        outTakeMed_Datas.Add(outTakeMed_Data);
                    }
                }
                if(outTakeMed_Datas.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "藥品不在調劑台內";
                    return returnData.JsonSerializationt(true);
                }
                returnData returnData_OutTakeMed = class_OutTakeMed_data.OutTakeMed(API_Server,調劑台, outTakeMed_Datas);
                if (returnData_OutTakeMed == null || returnData_OutTakeMed.Code != 200)
                {
                    returnData.Result = "扣帳失敗";
                    returnData.Code = -200;
                    returnData.Data = outTakeMed_Datas;
                    returnData.Value = returnData_OutTakeMed.JsonSerializationt();
                    Logger.Log("debit", $"{returnData.JsonSerializationt(true)}");
                    return returnData.JsonSerializationt(true);
                }
               
                returnData.Code = 200;
                returnData.Result = $"扣帳成功 共<{outTakeMed_Datas.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                Logger.Log("debit", $"{returnData.JsonSerializationt(true)}");
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex) 
            {
                returnData.Code = -200; 
                returnData.Result = ex.Message; 
                return returnData.JsonSerializationt(true);    
            }
        }
        [HttpPost("refund")]
        public string refund([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                returnData.Method = "refund";
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "ValueAry不得為空";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = "ValueAry不得為空";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.UserName.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "UserName應為 \"操作人\"";
                    return returnData.JsonSerializationt(true);
                }
                string 操作人 = returnData.UserName;
                string 調劑台 = returnData.ServerName;
                string[] GUIDs = returnData.ValueAry[0].Split(";");
                string API_Server = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");
                List<OrderClass> orderClasses = new List<OrderClass>();
                for (int i = 0; i < GUIDs.Length; i++)
                {
                    string GUID = GUIDs[i].Trim();
                    OrderClass orderClass = OrderClass.get_by_pri_key(API_Server, GUID);
                    if (orderClass != null && orderClass.實際調劑量 == orderClass.交易量) orderClasses.Add(orderClass);
                }
                if (orderClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "order資料狀態不符";
                    return returnData.JsonSerializationt(true);
                }
                List<class_OutTakeMed_data> outTakeMed_Datas = new List<class_OutTakeMed_data>();
                List<string> Codes = orderClasses.Select(temp => temp.藥品碼).Distinct().ToList();
                if (Codes.Count == 1) Codes[0] = Codes[0] + ",";
                List<medClass> medClasses = medClass.get_dps_medClass_by_code(API_Server, 調劑台, Codes);
                Dictionary<string, List<medClass>> medClassDict = medClass.CoverToDictionaryByCode(medClasses);



                foreach (var item in orderClasses)
                {
                    medClass medClass_buff = medClassDict[item.藥品碼].FirstOrDefault();
                    if (medClass_buff.DeviceBasics.Count != 0)
                    {
                        class_OutTakeMed_data outTakeMed_Data = new class_OutTakeMed_data()
                        {
                            //PRI_KEY = item.GUID,
                            Order_GUID = item.GUID,
                            成本中心 = "住院藥車",
                            護理站 = item.病房,
                            藥品碼 = item.藥品碼,
                            藥名 = item.藥品名稱,
                            操作人 = 操作人,
                            類別 = item.藥袋類型,
                            病人姓名 = item.病人姓名,
                            病歷號 = item.病歷號,
                            交易量 = item.交易量.Replace("-", ""),
                            開方時間 = item.開方日期,
                            電腦名稱 = 調劑台,
                            功能類型 = "-4" //退藥
                        };
                        outTakeMed_Datas.Add(outTakeMed_Data);
                    }
                        
                }
                if (outTakeMed_Datas.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "藥品不在調劑台內";
                    return returnData.JsonSerializationt(true);
                }
                returnData returnData_OutTakeMed = class_OutTakeMed_data.OutTakeMed(API_Server, 調劑台, outTakeMed_Datas);
                if (returnData_OutTakeMed == null || returnData_OutTakeMed.Code != 200)
                {
                    returnData.Result = "退帳失敗";
                    returnData.Code = -200;
                    returnData.Data = outTakeMed_Datas;
                    returnData.Value = returnData_OutTakeMed.JsonSerializationt();
                    Logger.Log("refund", $"{returnData.JsonSerializationt(true)}");
                    return returnData.JsonSerializationt(true);
                }
                returnData.Data = outTakeMed_Datas;
                returnData.Code = 200;
                returnData.Result = $"退帳成功 共<{outTakeMed_Datas.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                Logger.Log("refund", $"{returnData.JsonSerializationt(true)}");
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");

                //string 藥局 = returnData.ValueAry[0];

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);

                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetAllRows(null);
                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<patientInfoClass> update_patInfo = new List<patientInfoClass>();
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
                        update_patInfo.Add(patientInfoClass);
                    }                  
                }

                List<object[]> update_med_carInfo = update_patInfo.ClassToSQL<patientInfoClass, enum_patient_info>();
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
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");

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
        /// <summary>
        /// 清洗資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_data")]
        public string update_data([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_data";
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");

                //string 藥局 = returnData.ValueAry[0];

                SQLControl sQLControl_patient_info = new SQLControl(Server, DB, "patient_info", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);


                List<object[]> list_pat_carInfo = sQLControl_patient_info.GetAllRows(null);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetAllRows(null);

                List<patientInfoClass> sql_patinfo = list_pat_carInfo.SQLToClass<patientInfoClass, enum_patient_info>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate 
                {
                    foreach(var item in sql_patinfo)
                    {
                        item.更新時間 = DateTime.Now.ToDateTimeString();
                        if(item.入院日期.StringIsEmpty()) item.入院日期 = DateTime.MinValue.ToDateTimeString();
                    }
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    foreach(var item in sql_medCpoe)
                    {
                        item.更新時間 = DateTime.Now.ToDateTimeString();
                        if (item.PRI_KEY.Contains("[DC]") && item.調劑狀態 == "Y")
                        {
                            item.DC確認 = "Y";
                        }
                        else
                        {
                            item.DC確認 = "";
                        }
                    }
                })));
                Task.WhenAll(tasks).Wait();

                List<object[]> update_med_carInfo = sql_patinfo.ClassToSQL<patientInfoClass, enum_patient_info>();
                List<object[]> update_medcpoe = sql_medCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();

                if (update_med_carInfo.Count > 0) sQLControl_patient_info.UpdateByDefulteExtra(null, update_med_carInfo);
                if (update_medcpoe.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, update_medcpoe);

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

        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass, System.Enum enumInstance)
        {
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, enumInstance);
            return table.JsonSerializationt(true);
        }
       
        private (string StartTime, string Endtime) GetToday()
        {
            string API = Method.GetServerAPI("Main", "網頁", "API01");
            List<settingPageClass> settingPageClasses = settingPageClass.get_all(API);
            settingPageClass settingPage = settingPageClasses.myFind("medicine_cart", "交車時間");

            DateTime startTime_datetime = new DateTime();
            DateTime endTime_datetime = new DateTime();

            if (settingPage != null)
            {
                string 交車 = settingPage.設定值;
                TimeSpan 交車時間 = TimeSpan.Parse(交車);

                DateTime 現在 = DateTime.Now;
                TimeSpan 現在時間 = 現在.TimeOfDay;
                if (現在時間 >= 交車時間)
                {
                    // 現在時間已經過了交車時間：今天~明天
                    startTime_datetime = new DateTime(現在.Year, 現在.Month, 現在.Day, 交車時間.Hours, 交車時間.Minutes, 0);
                    endTime_datetime = startTime_datetime.AddDays(1);
                }
                else
                {
                    // 現在時間還沒到交車時間：昨天~今天
                    endTime_datetime = new DateTime(現在.Year, 現在.Month, 現在.Day, 交車時間.Hours, 交車時間.Minutes, 0);
                    startTime_datetime = endTime_datetime.AddDays(-1);
                }
            }
            string startTime = startTime_datetime.ToDateTimeString_6().Replace("/","-");
            string endTime = endTime_datetime.ToDateTimeString_6().Replace("/", "-");
            return (startTime, endTime);
        }
        private returnData ExcuteTrade(returnData returnData, List<medCpoeClass> medCpoeClasses, string action)
        {
            returnData returnData_result = new returnData();
            if (returnData.ServerName.StringIsEmpty() == false && returnData.UserName.StringIsEmpty() == false)
            {
                if (medCpoeClasses.Count > 0)
                {
                    string 操作人 = returnData.UserName;
                    string 調劑台 = returnData.ServerName;
                    List<string> guidList = new List<string>();
                    foreach (var item in medCpoeClasses)
                    {
                        guidList.Add(item.GUID);
                    }
                    string guidString = string.Join(";", guidList);
                    string API = Method.GetServerAPI("Main", "網頁", "API01");
                    if (action == enum_交易記錄查詢動作.系統領藥.GetEnumName())
                    {
                        returnData_result = medCpoeClass.debit(API, 操作人, 調劑台, guidString);
                    }
                    else if (action == enum_交易記錄查詢動作.系統退藥.GetEnumName())
                    {
                        returnData_result = medCpoeClass.refund(API, 操作人, 調劑台, guidString);
                    }
                }
            }
            return returnData_result;
        }
        private bool IsInCutoffRange(string 切帳時間字串, string 交車時間字串)
        {
            if (string.IsNullOrWhiteSpace(切帳時間字串) || string.IsNullOrWhiteSpace(交車時間字串))
                return false;

            TimeSpan 現在時間 = DateTime.Now.TimeOfDay;
            TimeSpan 切帳時間 = TimeSpan.Parse(切帳時間字串);
            TimeSpan 交車時間 = TimeSpan.Parse(交車時間字串);

            // 若兩者時間相等，不存在區間，直接回 false
            if (切帳時間 == 交車時間)
                return false;

            return 現在時間 > 切帳時間 && 現在時間 < 交車時間;
        }
        public static bool IsAfterCutoff(string 切帳時間字串)
        {
            if (切帳時間字串.StringIsEmpty()) return false;

            TimeSpan 現在時間 = DateTime.Now.TimeOfDay;
            TimeSpan 切帳時間 = TimeSpan.Parse(切帳時間字串);

            return 現在時間 > 切帳時間;
        }
        private List<patientInfoClass> UpdateStatus(List<patientInfoClass> patientInfoClasses, List<medCpoeClass> medCpoeClasses)
        {
            Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.ToDictByMasterGUID(medCpoeClasses);
            (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "VM端");
            string tableName_patient_info = "patient_info";
            SQLControl sQLControl_patient_info = new SQLControl(Server, DB, tableName_patient_info, UserName, Password, Port, SSLMode);
            List<Task> tasks = new List<Task>();
            foreach (var item in patientInfoClasses)
            {
                tasks.Add(Task.Run(new Action(delegate
                {
                    List<medCpoeClass> medCpoes = medCpoeClass.GetByMasterGUID(medCpoeDict, item.GUID);
                    if (item.占床狀態 == enum_bed_status_string.已出院.GetEnumName()) medCpoes = medCpoes.Where(temp => temp.PRI_KEY.Contains("[DC]")).ToList();
                    if (medCpoes.Count > 0)
                    {
                        bool mecCpoeDispenseFalg = medCpoes.Any(temp => temp.調劑狀態.StringIsEmpty());
                        bool mecCpoeCheckFalg = medCpoes.Any(temp => temp.覆核狀態.StringIsEmpty());
                        if (mecCpoeDispenseFalg)
                        {
                            item.調劑狀態 = "";
                            item.處方異動狀態 = "Y";
                        }
                        else
                        {
                            item.調劑狀態 = "Y";
                            item.處方異動狀態 = "";
                        }
                        if (mecCpoeCheckFalg)
                        {
                            item.覆核狀態 = "";
                        }
                        else
                        {
                            item.覆核狀態 = "Y";
                        }
                    }
                    else
                    {
                        item.調劑狀態 = "Y";
                        item.處方異動狀態 = "";
                        item.覆核狀態 = "Y";
                    }
                })));
            }
            Task.WhenAll(tasks).Wait();
            tasks.Clear();
            tasks.Add(Task.Run(new Action(delegate
            {
                List<object[]> update = patientInfoClasses.ClassToSQL<patientInfoClass, enum_patient_info>();
                sQLControl_patient_info.UpdateByDefulteExtra(null, update);
            })));
            Task.WhenAll(tasks).Wait();
            tasks.Clear();
            return patientInfoClasses;
        }


    }

}
