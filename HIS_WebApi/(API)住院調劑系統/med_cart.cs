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



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class med_cart : ControllerBase
    {
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
            returnData.Method = "med_cart/init_med_carinfo";
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
        [HttpPost("init_med_cpoe")]
        public string init_med_cpoe([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_cart/init_med_cpoe";
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
            returnData.Method = "med_cart/init_med_cpoe_rec";
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
            returnData.Method = "med_cart/init_med_info";
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
                return CheckCreatTable(serverSettingClasses[0], new enum_med_info());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string API = GetServerAPI("Main", "網頁", "API01");

                List<medCarInfoClass> medCart_sql_add = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_replace = new List<medCarInfoClass>();
                List<medCarInfoClass> medCart_sql_delete = new List<medCarInfoClass>();


                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                DateTime lestweek = DateTime.Now.AddDays(-30);
                DateTime yesterday = DateTime.Now.AddDays(-0);
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
                List<medCarInfoClass> medCarInfo = sql_medCar.Where(temp => temp.護理站 == 護理站).ToList();
                Dictionary<string, List<medCarInfoClass>> medCarInfoDictBedNum = medCarInfoClass.CoverToDictByBedNum(medCarInfo);

                ConcurrentBag<medCarInfoClass> localList_add = new ConcurrentBag<medCarInfoClass>();
                ConcurrentBag<medCarInfoClass> localList_delete = new ConcurrentBag<medCarInfoClass>();
                ConcurrentBag<medCarInfoClass> localList_replace = new ConcurrentBag<medCarInfoClass>();
                //foreach (medCarInfoClass medCarInfoClass in input_medCarInfo)
                //{
                //    medCarInfoClass targetPatient = new medCarInfoClass();

                //    string 床號 = medCarInfoClass.床號;
                //    if (medCarInfoClass.SortDictByBedNum(medCarInfoDictBedNum, 床號).Count != 0)
                //    {
                //        targetPatient = medCarInfoClass.SortDictByBedNum(medCarInfoDictBedNum, 床號)[0];
                //    }

                //    if (targetPatient.GUID.StringIsEmpty() == true)
                //    {
                //        medCarInfoClass.GUID = Guid.NewGuid().ToString();
                //        medCart_sql_add.Add(medCarInfoClass);
                //    }
                //    else
                //    {
                //        if (medCarInfoClass.病歷號 != targetPatient.病歷號)
                //        {
                //            medCarInfoClass.GUID = Guid.NewGuid().ToString();
                //            medCarInfoClass.異動 = "Y";
                //            medCart_sql_add.Add(medCarInfoClass);
                //            medCart_sql_delete.Add(targetPatient);
                //        }
                //        else
                //        {
                //            medCarInfoClass.GUID = targetPatient.GUID;
                //            medCarInfoClass.調劑狀態 = targetPatient.調劑狀態;
                //            //if ((medCarInfoClass.調劑狀態).StringIsEmpty() == false) medCarInfoClass.調劑狀態 = targetPatient.調劑狀態;
                //            //if (string.IsNullOrWhiteSpace(medCarInfoClass.調劑狀態)) medCarInfoClass.調劑狀態 = targetPatient.調劑狀態;
                //            medCart_sql_replace.Add(medCarInfoClass);
                //        }
                //    }
                //}



                Parallel.ForEach(input_medCarInfo, new ParallelOptions { MaxDegreeOfParallelism = 10 }, medCarInfoClass =>
                {
                    medCarInfoClass targetPatient = new medCarInfoClass();

                    string 床號 = medCarInfoClass.床號;
                    if (medCarInfoClass.SortDictByBedNum(medCarInfoDictBedNum, 床號).Count != 0)
                    {
                        targetPatient = medCarInfoClass.SortDictByBedNum(medCarInfoDictBedNum, 床號)[0];
                    }

                    if (targetPatient.GUID.StringIsEmpty() == true)
                    {
                        //medCarInfoClass.GUID = Guid.NewGuid().ToString();
                        localList_add.Add(medCarInfoClass);
                    }
                    else
                    {
                        if (medCarInfoClass.病歷號 != targetPatient.病歷號)
                        {
                            //medCarInfoClass.GUID = Guid.NewGuid().ToString();
                            medCarInfoClass.異動 = "Y";
                            localList_add.Add(medCarInfoClass);
                            localList_delete.Add(targetPatient);
                        }
                        else
                        {
                            medCarInfoClass.GUID = targetPatient.GUID;
                            medCarInfoClass.調劑狀態 = targetPatient.調劑狀態;
                            localList_replace.Add(medCarInfoClass);
                        }
                    }
                });
                lock (medCart_sql_add) medCart_sql_add.AddRange(localList_add);
                lock (medCart_sql_replace) medCart_sql_replace.AddRange(localList_replace);
                lock (medCart_sql_delete) medCart_sql_delete.AddRange(localList_delete);                

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
                    //Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.CoverToDictByMasterGUID(sql_medCpoe);
                    //List<medCpoeClass> filterCpoe = new List<medCpoeClass>();
                    //for (int i = 0; medCart_sql_delete.Count > 0; i++)
                    //{
                    //    List<medCpoeClass> result = medCpoeClass.SortDictByMasterGUID(medCpoeDict, medCart_sql_delete[i].GUID);
                    //    filterCpoe.AddRange(result);
                    //}
                    List<medCpoeClass> filterCpoe = sql_medCpoe
                        .Where(cpoe => medCart_sql_delete.Any(medCart => medCart.GUID == cpoe.Master_GUID)).ToList();
                    List<object[]> list_medCpoe_delete = filterCpoe.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                    if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);
                }

                List<object[]> list_bedList = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<medCarInfoClass> bedList = list_bedList.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCarInfoClass> medCarInfoClasses = bedList.Where(temp => temp.護理站 == 護理站).ToList();
                medCarInfoClasses.Sort(new medCarInfoClass.ICP_By_bedNum());

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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

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
                List<medCpoeClass> medCpoe_sql_delete = new List<medCpoeClass>();
                List<medCarInfoClass> update_medCarInfo = new List<medCarInfoClass>();

                Dictionary<string, List<medCarInfoClass>> medCarInfoDict = medCarInfoClass.CoverToDictByGUID(sql_medCarInfo);
                Dictionary<string, List<medCpoeClass>> sqlMedCpoeDict = medCpoeClass.CoverToDictByMasterGUID(sql_medCpoe);
                Dictionary<string, List<medCpoeClass>> inputMedCpoeDict = medCpoeClass.CoverToDictByMasterGUID(input_medCpoe);


                foreach(string Master_GUID in inputMedCpoeDict.Keys)
                {
                    if (!medCarInfoDict.ContainsKey(Master_GUID))
                    {
                        returnData.Code = -200;
                        returnData.Result = "處方資料錯誤，請更新病床資訊";
                        return returnData.JsonSerializationt(true);
                    }
                    else
                    {
                        List<medCpoeClass> medCpoeClasses_current = medCpoeClass.SortDictByMasterGUID(sqlMedCpoeDict, Master_GUID);
                        List<medCpoeClass> medCpoeClasses_new = medCpoeClass.SortDictByMasterGUID(inputMedCpoeDict, Master_GUID);
                        if(medCpoeClasses_current.Count == 0 && medCpoeClasses_new.Count == 0)
                        {
                            List<medCarInfoClass> medCarInfoClasses = medCarInfoClass.SortDictByGUID(medCarInfoDict, Master_GUID);
                            if (medCarInfoClasses[0].占床狀態 == "已佔床")
                            {
                                medCarInfoClasses[0].調劑狀態 = "Y";
                                update_medCarInfo.Add(medCarInfoClasses[0]);
                            }
                        }
                        else
                        {
                            List<medCpoeClass> onlyInOld = medCpoeClasses_current.Where(o => !medCpoeClasses_new.Any(n => n.序號 == o.序號)).ToList();
                            List<medCpoeClass> onlyInNew = medCpoeClasses_new.Where(o => !medCpoeClasses_current.Any(n => n.序號 == o.序號)).ToList();
                            foreach (var oldItem in onlyInOld)
                            {

                                if (oldItem.調劑狀態.StringIsEmpty() && oldItem.調劑異動.StringIsEmpty())
                                {
                                    medCpoe_sql_delete.Add(oldItem);
                                }
                                else
                                {
                                    foreach (var newItem in onlyInNew)
                                    {
                                        if (oldItem.藥碼 == newItem.藥碼 &&
                                            oldItem.途徑 == newItem.途徑 &&
                                            oldItem.頻次代碼 == newItem.頻次代碼)
                                        {
                                            medCpoe_sql_delete.Add(oldItem);
                                            oldItem.調劑異動 = "Y";
                                            newItem.調劑狀態 = "Y";
                                        }
                                    }
                                    if (oldItem.調劑異動.StringIsEmpty())
                                    {
                                        oldItem.數量 = "0";
                                        oldItem.劑量 = "0";
                                        oldItem.調劑狀態 = "";
                                        oldItem.頻次代碼 = "--";
                                        oldItem.狀態 = "DC";
                                        oldItem.調劑異動 = "Y";
                                        medCpoe_sql_replace.Add(oldItem);
                                    }
                                }
                            }
                            medCpoe_sql_add.AddRange(onlyInNew);
                        }                                               
                    }       
                }
          

                List<object[]> list_medCpoe_add = medCpoe_sql_add.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_medCpoe_delete = medCpoe_sql_delete.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                List<object[]> list_medCart_add = update_medCarInfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();


                if (list_medCpoe_add.Count > 0) sQLControl_med_cpoe.AddRows(null, list_medCpoe_add);
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
                if (list_medCpoe_delete.Count > 0) sQLControl_med_cpoe.DeleteExtra(null, list_medCpoe_delete);
                if (list_medCart_add.Count > 0) sQLControl_med_carInfo.UpdateByDefulteExtra(null, list_medCart_add);

                List<object[]> list_med_cpoe_new = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                List<medCpoeClass> medCpoeClass_new = list_med_cpoe_new.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> results = new List<medCpoeClass>();
                Dictionary<string, List<medCpoeClass>> sqlMedCpoeDict_new = medCpoeClass.CoverToDictByMasterGUID(medCpoeClass_new);
                foreach (string Master_GUID in inputMedCpoeDict.Keys)
                {
                    List<medCpoeClass> result = medCpoeClass.SortDictByMasterGUID(sqlMedCpoeDict_new, Master_GUID);
                    if (result.Count > 0) results.AddRange(result);                 
                }
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = results;
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

                //List<medCpoeRecClass> filter_medCpoeRec = input_medCpoe_rec
                //    .GroupBy(medCpoeRec => medCpoeRec.Master_GUID)
                //    .Select(group => group.First())
                //    .ToList();
             

                List<object[]> sql_medCpoeRecClass = sQLControl_med_cpoe_rec.GetRowsByDefult(null, (int)enum_med_cpoe_rec.藥局, 藥局);
                List<medCpoeRecClass> list_medCpoeRecClass = sql_medCpoeRecClass.SQLToClass<medCpoeRecClass, enum_med_cpoe_rec>();
                Dictionary<string, List<medCpoeRecClass>> inputMedCpoeRecDict = medCpoeRecClass.CoverToDictByMasterGUID(input_medCpoe_rec);
                Dictionary<string, List<medCpoeRecClass>> sqlMedCpoeRecDict = medCpoeRecClass.CoverToDictByMasterGUID(list_medCpoeRecClass);
                List<medCpoeRecClass> add_medCpoeRecClass = new List<medCpoeRecClass>();
                foreach (var Master_GUID in inputMedCpoeRecDict.Keys)
                {
                    List<medCpoeRecClass> input_medCpoeRec = medCpoeRecClass.SortDictByMasterGUID(inputMedCpoeRecDict, Master_GUID);
                    List<medCpoeRecClass> sql_medCpoeRec = medCpoeRecClass.SortDictByMasterGUID(sqlMedCpoeRecDict, Master_GUID);

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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

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
                Dictionary<string, List<medInfoClass>> sqlMedInfoDict = medInfoClass.CoverToDictionaryByCode(sql_medInfo);
                List<medInfoClass> add_medInfo = new List<medInfoClass>();
                List<medInfoClass> update_medInfo = new List<medInfoClass>();

                foreach (var medInfoClass in input_medInfoClass)
                {
                    List<medInfoClass> target = medInfoClass.SortDictByCode(sqlMedInfoDict, medInfoClass.藥碼);
                    if (target.Count == 0)
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
                if (list_update_medInfo.Count > 0) sQLControl_med_info.UpdateByDefulteExtra(null, list_update_medInfo);

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

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCarInfoClass> medCarInfoClasses = sql_medCarInfo.Where(temp => temp.護理站 == 護理站).ToList();
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
        [HttpPost("get_patient_by_GUID_brief")]
        public string get_patient_by_GUID_brief([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "med_cart/get_patient_by_GUID_brief";
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.GUID, GUID);
                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();

                if (sql_medCarInfo == null)
                {
                    returnData.Code = 200;
                    returnData.Result = "無對應的病人資料";
                    return returnData.JsonSerializationt(true);
                }
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCarInfo[0];
                returnData.Result = $"取得{sql_medCarInfo[0].藥局} {sql_medCarInfo[0].護理站} 第{sql_medCarInfo[0].床號}床的資訊";
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
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.GUID, GUID);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, GUID);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();              

                if (sql_medCarInfo == null)
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

                    if ((returnData.Value).StringIsEmpty() == false && returnData.Value != "all")
                    {
                        List<medClass> medClasses = medClass.get_dps_medClass_by_code(API, returnData.Value, Codes);
                        Dictionary<string, List<medClass>> medClassDict = medClass.CoverToDictionaryByCode(medClasses);
                        foreach (medCpoeClass medCpoeClass in sql_medCpoe)
                        {
                            if (medClassDict.ContainsKey(medCpoeClass.藥碼)) medCpoeClass.調劑台 = "Y";
                        }
                    }
                    if (returnData.Value == "all")
                    {
                        foreach (var medCpoeClass in sql_medCpoe) medCpoeClass.調劑台 = "Y";
                    }
                    List<string> GUIDs = sql_medCpoe.Select(temp => temp.GUID).ToList();
                    string valueAry = string.Join(";",GUIDs);
                    List<medClass> med_cloud = medClass.get_med_clouds_by_codes(API, Codes);
                    List<medInfoClass> med_Info = medInfoClass.get_medInfo_by_codes(API, Codes);
                    List<medInventoryLogClass> med_InvenLog = medInventoryLogClass.get_logtime_by_master_GUID(API, valueAry);
                    Dictionary<string, List<medClass>> medCloudDict = medClass.CoverToDictionaryByCode(med_cloud);
                    Dictionary<string, List<medInfoClass>> medInfoDict = medInfoClass.CoverToDictionaryByCode(med_Info);
                    Dictionary<string, List<medInventoryLogClass>> medInvenDict = medInventoryLogClass.CoverToDictionaryMasterGUID(med_InvenLog);      
                    foreach (var cpoe in sql_medCpoe)
                    {          

                        cpoe.雲端藥檔 = medClass.SortDictionaryByCode(medCloudDict, cpoe.藥碼);
                        cpoe.藥品資訊 = medInfoClass.SortDictByCode(medInfoDict, cpoe.藥碼);
                        cpoe.調劑紀錄 = medInventoryLogClass.SortDictByMasterGUID(medInvenDict, cpoe.GUID);
                    }
                    sql_medCarInfo[0].處方 = sql_medCpoe;    
                }
                else
                {
                    sql_medCarInfo[0].處方 = new List<medCpoeClass>();
                }
                medCarInfoClass medCarInfoClasses = new medCarInfoClass();
                medCarInfoClasses = sql_medCarInfo[0];

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = medCarInfoClasses;
                returnData.Result = $"取得{sql_medCarInfo[0].藥局} {sql_medCarInfo[0].護理站} 第{sql_medCarInfo[0].床號}病床的資訊";
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

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();

                List<medCarInfoClass> sql_medCar = sql_medCarInfo.Where(temp => temp.調劑狀態 == "" && temp.護理站 == 護理站).ToList();
                Dictionary<string, List<medCpoeClass>> medCpoeDict = medCpoeClass.CoverToDictByMasterGUID(sql_medCpoe);

                foreach ( var medcar in sql_medCar)
                {
                    List<medCpoeClass> targetCpoe = medCpoeClass.SortDictByMasterGUID(medCpoeDict, medcar.GUID);
                    targetCpoe = targetCpoe.Where(temp => temp.調劑狀態 == "").ToList();
                    medcar.處方 = targetCpoe;
                }
                sql_medCar.Sort(new medCarInfoClass.ICP_By_bedNum());
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
                List<object[]> list_med_cpoe_rec = sQLControl_med_cpoe_rec.GetRowsByDefult(null, (int)enum_med_cpoe_rec.Master_GUID, GUID);

                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCpoeRecClass> sql_medCpoeRec = list_med_cpoe_rec.SQLToClass<medCpoeRecClass, enum_med_cpoe_rec>();
                sql_medCpoeRec.Sort(new medCpoeRecClass.ICP_By_UP_Time());

                sql_medCarInfo[0].處方異動 = sql_medCpoeRec;

                string 藥局 = sql_medCarInfo[0].藥局;
                string 護理站 = sql_medCarInfo[0].護理站;
                string 床號 = sql_medCarInfo[0].床號;

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCarInfo;
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
        ///由藥碼取得藥品資訊
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":["code1,code2,code3..."]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_medInfo_by_codes")]
        public string get_medInfo_by_codes([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"code1,code2,code3\"]";
                    return returnData.JsonSerializationt(true);
                }
                string[] codes = returnData.ValueAry[0].Split(",");
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
                List<object[]> list_med_info = sQLControl_med_carInfo.GetAllRows(null);
                List<medInfoClass> sql_med_info = list_med_info.SQLToClass<medInfoClass, enum_med_info>();
                List<medInfoClass> result = new List<medInfoClass>();
                Dictionary<string, medInfoClass> medInfoDict = sql_med_info.ToDictionary(m => m.藥碼);
                foreach(var code in codes)
                {
                    if (medInfoDict.ContainsKey(code)) result.Add(medInfoDict[code]);
                }

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = result;
                returnData.Result = $"取得{result.Count}筆 藥品資訊";
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
                List<ServerSettingClass> serverSettingClass_main = serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (serverSettingClass_main.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = serverSettingClass_main[0].Server;
                string DB = serverSettingClass_main[0].DBName;
                string UserName = serverSettingClass_main[0].User;
                string Password = serverSettingClass_main[0].Password;
                uint Port = (uint)serverSettingClass_main[0].Port.StringToInt32();
                List<ServerSettingClass> serverSettingClass_API = serverSettingClasses.MyFind("Main", "網頁", "API01");
                string API = serverSettingClass_API[0].Server;
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
                        if(medCpoeClass.調劑異動 == "Y")
                        {
                            medCpoeClass.DC確認 = "Y";
                        }                      
                        medCpoe_sql_replace.Add(medCpoeClass);
                    }
                    else
                    {
                        medCpoeClass.調劑狀態 = "";
                        if(medCpoeClass.調劑異動 == "Y")
                        {
                            medCpoeClass.DC確認 = "";
                        }
                        medCpoe_sql_replace.Add(medCpoeClass);
                    }
                }
                List<object[]> list_medCpoe_replace = new List<object[]>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);

                bool allDispensed = medCpoe_sql_replace.All(med => med.調劑狀態 == "Y");
                List<object[]> list_med_carinfo = sQLControl_med_carinfo.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, Master_GUID);
                List<medCarInfoClass> sql_medCarinfo = list_med_carinfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                if (allDispensed) 
                {
                    sql_medCarinfo[0].調劑狀態 = "Y";
                    sql_medCarinfo[0].處方異動狀態 = "";
                }
                else
                {
                    sql_medCarinfo[0].調劑狀態 = "";
                }
                    
                List<object[]> list_medCarInfo_replace = sql_medCarinfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                sQLControl_med_carinfo.UpdateByDefulteExtra(null, list_medCarInfo_replace);             

                //List<string> ValueAry = new List<string> { Master_GUID };
                //medCarInfoClass targetPatient = medCarInfoClass.get_patient_by_GUID(API, ValueAry);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCarinfo;
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
                SQLControl sQLControl_med_carinfo = new SQLControl(Server, DB, "med_carinfo", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();                
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
                medCpoe_sql_replace[0].調劑狀態 = 調劑狀態;
                if (!medCpoe_sql_replace[0].調劑異動.StringIsEmpty()) medCpoe_sql_replace[0].調劑異動 = "";


                List<object[]> list_medCpoe_replace = new List<object[]>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);
                sql_medCpoe.Add(medCpoe_sql_replace[0]);

                bool allDispensed = sql_medCpoe.All(med => med.調劑狀態 == "Y");
                bool dispensStstus = sql_medCpoe.All(med => med.調劑異動 == "");
                List<object[]> list_med_carinfo = sQLControl_med_carinfo.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, Master_GUID);
                List<medCarInfoClass> sql_medCarinfo = list_med_carinfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                if (allDispensed && dispensStstus)
                {
                    sql_medCarinfo[0].調劑狀態 = "Y";
                    sql_medCarinfo[0].處方異動狀態 = "";
                }
                else
                {
                    sql_medCarinfo[0].調劑狀態 = "";
                }
                List<object[]> list_medCarInfo_replace = new List<object[]>();
                list_medCarInfo_replace = sql_medCarinfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                sQLControl_med_carinfo.UpdateByDefulteExtra(null, list_medCarInfo_replace);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCpoe;
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
                foreach (var medCpoeClass in sql_medCpoe)
                {
                    medCpoeClass.覆核狀態 = GUID.Contains(medCpoeClass.GUID) ? "Y" : "";                  
                    medCpoe_sql_replace.Add(medCpoeClass);
                }
                List<object[]> list_medCpoe_replace = new List<object[]>();
                list_medCpoe_replace = medCpoe_sql_replace.ClassToSQL<medCpoeClass, enum_med_cpoe>();
                if (list_medCpoe_replace.Count > 0) sQLControl_med_cpoe.UpdateByDefulteExtra(null, list_medCpoe_replace);

                bool allDispensed = medCpoe_sql_replace.All(med => med.覆核狀態 == "Y");
                List<object[]> list_med_carinfo = sQLControl_med_carinfo.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, Master_GUID);
                List<medCarInfoClass> sql_medCarinfo = list_med_carinfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                sql_medCarinfo[0].覆核狀態 = allDispensed ? "Y" : "";
               

                List<object[]> list_medCarInfo_replace = new List<object[]>();
                list_medCarInfo_replace = sql_medCarinfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                sQLControl_med_carinfo.UpdateByDefulteExtra(null, list_medCarInfo_replace);

                //List<string> ValueAry = new List<string> { Master_GUID };
                //medCarInfoClass targetPatient = medCarInfoClass.get_patient_by_GUID(API, ValueAry);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = sql_medCarinfo;
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
                SQLControl sQLControl_med_carinfo = new SQLControl(Server, DB, "med_carinfo", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.Master_GUID, Master_GUID);

                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
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
                List<object[]> list_med_carinfo = sQLControl_med_carinfo.GetRowsByDefult(null, (int)enum_med_cpoe.GUID, Master_GUID);
                List<medCarInfoClass> sql_medCarinfo = list_med_carinfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                sql_medCarinfo[0].覆核狀態 = allDispensed ? "Y" : "";

                List<object[]> list_medCarInfo_replace = new List<object[]>();
                list_medCarInfo_replace = sql_medCarinfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                sQLControl_med_carinfo.UpdateByDefulteExtra(null, list_medCarInfo_replace);


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
               
                if (returnData.ValueAry == null || returnData.ValueAry.Count != 2)
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_med_cpoe = new SQLControl(Server, DB, "med_cpoe", UserName, Password, Port, SSLMode);
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCpoeClass> medCpoeClasses = sql_medCpoe.Where(temp => temp.護理站 == 護理站).ToList();

                List<medQtyClass> medQtyClasses = medCpoeClasses
                    .GroupBy(temp => temp.藥品名)
                    .Select(grouped => new medQtyClass
                    {
                        藥品名 = grouped.Key,
                        藥碼 = grouped.First().藥碼,
                        單位 = grouped.First().單位,
                        調劑台 = "",
                        病床清單 = grouped.Select(value => new bedListClass
                        {
                            GUID = value.GUID,
                            Master_GUID = value.Master_GUID,
                            床號 = value.床號,
                            數量 = value.數量,
                            劑量 = value.劑量,
                            大瓶點滴 = value.大瓶點滴,
                            調劑狀態 = value.調劑狀態,
                            覆核狀態 = value.覆核狀態
                        }).ToList()
                    })
                    .ToList();
                List<string> codes = medQtyClasses.Select(temp => temp.藥碼).Distinct().ToList();

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
                    Dictionary<string, List<medClass>> medClassDict = medClass.CoverToDictionaryByCode(medClasses);
                    foreach (var medQtyClass in medQtyClasses)
                    {
                        if (medClassDict.ContainsKey(medQtyClass.藥碼)) medQtyClass.調劑台 = "Y";
                    }
                }
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
                List<object[]> list_med_cpoe = sQLControl_med_cpoe.GetRowsByDefult(null, (int)enum_med_cpoe.藥局, 藥局);
                List<object[]> list_med_carlist = sQLControl_med_carlist.GetRowsByDefult(null, (int)enum_med_carList.藥局, 藥局);
                List<medCpoeClass> sql_medCpoe = list_med_cpoe.SQLToClass<medCpoeClass, enum_med_cpoe>();
                List<medCarListClass> sql_medCarList = list_med_carlist.SQLToClass<medCarListClass, enum_med_carList>();

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

                string 藥局 = returnData.ValueAry[0];

                SQLControl sQLControl_med_carInfo = new SQLControl(Server, DB, "med_carInfo", UserName, Password, Port, SSLMode);

                List<object[]> list_med_carInfo = sQLControl_med_carInfo.GetRowsByDefult(null, (int)enum_med_carInfo.藥局, 藥局);
                List<medCarInfoClass> sql_medCarInfo = list_med_carInfo.SQLToClass<medCarInfoClass, enum_med_carInfo>();
                List<medCarInfoClass> update_medCarInfo = new List<medCarInfoClass>();
                foreach (var medCarInfoClass in sql_medCarInfo)
                {
                    if(medCarInfoClass.占床狀態 == "已佔床")
                    {
                        medCarInfoClass.姓名 = "XXX";
                        medCarInfoClass.住院號 = "31766666";
                        medCarInfoClass.病歷號 = "33445566";
                        medCarInfoClass.住院醫師 = "王志明";
                        medCarInfoClass.住院醫師代碼 = "UDC7777";
                        medCarInfoClass.主治醫師 = "陳春嬌";
                        medCarInfoClass.主治醫師代碼 = "UDC8888";
                        update_medCarInfo.Add(medCarInfoClass);
                    }                  
                }

                List<object[]> update_med_carInfo = update_medCarInfo.ClassToSQL<medCarInfoClass, enum_med_carInfo>();
                if (update_med_carInfo.Count > 0) sQLControl_med_carInfo.UpdateByDefulteExtra(null, update_med_carInfo);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                //returnData.Data = medCarInfoClasses;
                returnData.Result = $"更改{藥局} 所有病人資訊";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
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

        private (string Server, string DB, string UserName, string Password, uint Port) GetServerInfo(string Name, string Type, string Content)
        {
            List<ServerSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            ServerSettingClass serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return (serverSettingClass.Server, serverSettingClass.DBName, serverSettingClass.User, serverSettingClass.Password, (uint)serverSettingClass.Port.StringToInt32());
        }
        private string GetServerAPI(string Name, string Type, string Content)
        {
            List<ServerSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            ServerSettingClass serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return serverSettingClass.Server;
        }
    }

}
