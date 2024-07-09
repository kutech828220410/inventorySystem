using Microsoft.AspNetCore.Mvc;
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
using HIS_DB_Lib;
using H_Pannel_lib;
namespace HIS_WebApi
{


    [Route("api/[controller]")]
    [ApiController]
    public class MED_pageController : Controller
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(OrderClass))]
        [HttpPost]
        public string POST_init([FromBody] returnData returnData)
        {
            try
            {
                //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
                return CheckCreatTable(returnData);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [HttpPost]
        public string Get([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string Server = returnData.Server;
                string DbName = returnData.DbName;
                string TableName = returnData.TableName;
                string UserName = returnData.UserName;
                string Password = returnData.Password;
                uint Port = returnData.Port;
                SQLControl sQLControl_med = new SQLControl(Server, DbName, TableName, UserName, Password, Port, SSLMode);
                string[] colName = sQLControl_med.GetAllColumn_Name(TableName);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);

                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_雲端藥檔()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_雲端藥檔>();
                    returnData.Code = 200;
                    returnData.Result = "雲端藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_medDrugstore()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_medDrugstore>();
                    returnData.Code = 200;
                    returnData.Result = "藥庫藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_medDrugstore()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_medDrugstore>();
                    returnData.Code = 200;
                    returnData.Result = "藥局藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                if (sQLControl_med.ColumnName_Enum_IsEqual(new enum_藥品資料_藥檔資料()))
                {
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    returnData.Code = 200;
                    returnData.Result = "調劑台藥檔取得成功!";
                    return returnData.JsonSerializationt(true);
                }
                returnData.Code = -5;
                returnData.Result = "藥檔取得失敗!";

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
        /// 取得雲端藥檔資料
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
        ///
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_med_cloud")]
        [HttpPost]
        public string POST_get_med_cloud(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_med_cloud";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                if(serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }

                returnData.ServerName = "Main";
                returnData.ServerType = "網頁";
                returnData.TableName = "medicine_page_cloud";
                POST_init(returnData);
                List<medClass> medClasses = Get_med_cloud(serverSettingClasses_buf[0]);
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"藥檔取得失敗!";
                    return returnData.JsonSerializationt();
                }
               
                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = "雲端藥檔取得成功!";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以藥碼取得雲端藥檔資料
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
        ///         [藥碼1,藥碼2]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_med_clouds_by_codes")]
        [HttpPost]
        public string POST_get_med_clouds_by_codes(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_med_clouds_by_codes";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼1,藥碼2,藥碼3]";
                    return returnData.JsonSerializationt(true);
                }
                string[] Codes = returnData.ValueAry[0].Split(",");

                //returnData.ServerName = "Main";
                //returnData.ServerType = "網頁";
                //returnData.TableName = "medicine_page_cloud";
                //POST_init(returnData);
                List<medClass> medClasses = Get_med_cloud(serverSettingClasses_buf[0], Codes);
                List<medClass> medClasses_temp = new List<medClass>();
                List<medClass> medClasses_buf = new List<medClass>();
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"藥檔取得失敗!";
                    return returnData.JsonSerializationt();
                }
                Dictionary<string, List<medClass>> keyValuePairs_medClasses = medClass.CoverToDictionaryByCode(medClasses);
                for (int i = 0; i < Codes.Length; i++)
                {
                    medClasses_temp = keyValuePairs_medClasses.SortDictionaryByCode(Codes[i]);
                    if (medClasses_temp.Count == 0) continue;
                    medClasses_buf.Add(medClasses_temp[0]);
                }

                returnData.Data = medClasses_buf;
                returnData.Code = 200;
                returnData.Result = $"雲端藥檔取得成功,共<{medClasses_buf.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以藥名取得雲端藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[前綴]",
        ///     "ValueAry" : 
        ///     [
        ///         [藥名]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_med_clouds_by_name")]
        [HttpPost]
        public string POST_get_med_clouds_by_name(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_med_clouds_by_name";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥名]";
                    return returnData.JsonSerializationt(true);
                }
                ServerSettingClass serverSettingClass = serverSettingClasses_buf[0];
                string text = returnData.ValueAry[0];

       
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();
                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_cloud", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_med.GetAllRows(null);
                if (returnData.Value == "前綴")
                {
                    list_value = list_value.GetRowsStartWithByLike((int)enum_雲端藥檔.藥品名稱, text);
                }
                else
                {
                    list_value = list_value.GetRowsByLike((int)enum_雲端藥檔.藥品名稱, text);
                }

                List<medClass> medClasses = list_value.SQLToClass<medClass, enum_雲端藥檔>();

                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"藥檔取得失敗!";
                    return returnData.JsonSerializationt();
                }
          

                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"雲端藥檔取得成功,共<{medClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以商品名取得雲端藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[前綴]",
        ///     "ValueAry" : 
        ///     [
        ///         [商品名]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_med_clouds_by_dianame")]
        [HttpPost]
        public string POST_get_med_clouds_by_dianame(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_med_clouds_by_dianame";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥名]";
                    return returnData.JsonSerializationt(true);
                }
                ServerSettingClass serverSettingClass = serverSettingClasses_buf[0];
                string text = returnData.ValueAry[0];


                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();
                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_cloud", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_med.GetAllRows(null);
                if (returnData.Value == "前綴")
                {
                    list_value = list_value.GetRowsStartWithByLike((int)enum_雲端藥檔.藥品學名, text);
                }
                else
                {
                    list_value = list_value.GetRowsByLike((int)enum_雲端藥檔.藥品學名, text);
                }

                List<medClass> medClasses = list_value.SQLToClass<medClass, enum_雲端藥檔>();

                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"藥檔取得失敗!";
                    return returnData.JsonSerializationt();
                }


                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"雲端藥檔取得成功,共<{medClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以中文名取得雲端藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[前綴]",
        ///     "ValueAry" : 
        ///     [
        ///         [中文名]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_med_clouds_by_chtname")]
        [HttpPost]
        public string POST_get_med_clouds_by_chtname(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_med_clouds_by_dianame";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥名]";
                    return returnData.JsonSerializationt(true);
                }
                ServerSettingClass serverSettingClass = serverSettingClasses_buf[0];
                string text = returnData.ValueAry[0];


                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();
                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_cloud", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_med.GetAllRows(null);
                if (returnData.Value == "前綴")
                {
                    list_value = list_value.GetRowsStartWithByLike((int)enum_雲端藥檔.中文名稱, text);
                }
                else
                {
                    list_value = list_value.GetRowsByLike((int)enum_雲端藥檔.中文名稱, text);
                }

                List<medClass> medClasses = list_value.SQLToClass<medClass, enum_雲端藥檔>();

                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"藥檔取得失敗!";
                    return returnData.JsonSerializationt();
                }

                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"藥檔取得失敗!";
                    return returnData.JsonSerializationt();
                }


                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"雲端藥檔取得成功,共<{medClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }

        /// <summary>
        /// 新增雲端藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        [medclass陣列]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///         
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("add_med_clouds")]
        [HttpPost]
        public string POST_add_med_clouds(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add_med_clouds";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                List<medClass> medClasses = returnData.Data.ObjToClass<List<medClass>>();
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤";
                    return returnData.JsonSerializationt();
                }
                //returnData.ServerName = "Main";
                //returnData.ServerType = "網頁";
                //returnData.TableName = "medicine_page_cloud";
                //POST_init(returnData);
                List<object[]> list_value_add = medClasses.ClassToSQL<medClass, enum_雲端藥檔>();
                List<object[]> list_value_add_buf = new List<object[]>();

                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();

                SQLControl sQLControl = new SQLControl(Server, DB, "medicine_page_cloud", UserName, Password, Port, SSLMode);

                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();

                for (int i = 0; i < list_value_add.Count; i++)
                {
                    string 藥碼 = list_value_add[i][(int)enum_雲端藥檔.藥品碼].ObjectToString();
                    list_value_buf = list_value.GetRows((int)enum_雲端藥檔.藥品碼, 藥碼);
                    if(list_value_buf.Count == 0)
                    {
                        list_value_add_buf.Add(list_value_add[i]);
                    }
                }


                sQLControl.AddRows(null, list_value_add);

                returnData.Code = 200;
                returnData.Result = $"新增雲端藥檔成功,共<{list_value_add.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID更新雲端藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        [medclass陣列]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///         
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("update_med_clouds_by_guid")]
        [HttpPost]
        public string POST_update_med_clouds_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "update_med_clouds_by_guid";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                List<medClass> medClasses = returnData.Data.ObjToClass<List<medClass>>();
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤";
                    return returnData.JsonSerializationt();
                }
                //returnData.ServerName = "Main";
                //returnData.ServerType = "網頁";
                //returnData.TableName = "medicine_page_cloud";
                //POST_init(returnData);
                List<object[]> list_value_replace = medClasses.ClassToSQL<medClass,enum_雲端藥檔>();


                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();

                SQLControl sQLControl = new SQLControl(Server, DB, "medicine_page_cloud", UserName, Password, Port, SSLMode);

                sQLControl.UpdateByDefulteExtra(null, list_value_replace);

                returnData.Code = 200;
                returnData.Result = $"更新雲端藥檔成功,共<{list_value_replace.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }


        /// <summary>
        /// 從線上藥檔更新藥庫系統的藥局藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "ServerName" : "ds01",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("update_ds_pharma_med_from_medcloud")]
        [HttpPost]
        public string update_ds_pharma_med_from_medcloud(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_ds_pharma_med";
            returnData.TableName = "medicine_page_phar";
            returnData.ServerType = "藥庫";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, "藥庫", "本地端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                CheckCreatTable(returnData);
                string TaskTime_藥局 = "";
                string TaskTime_藥庫 = "";
                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];
                List<medClass> medClasses_cloud = Get_med_cloud(serverSettingClasses_med);
                List<medClass> medClasses_cloud_buf = new List<medClass>();


          


                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_phar", UserName, Password, Port, SSLMode);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_medPharmacy>();
                List<medClass> medClasses_buf = new List<medClass>();
                List<medClass> medClasses_add = new List<medClass>();
                List<medClass> medClasses_replace = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses = medClasses.CoverToDictionaryByCode();

                for (int i = 0; i < medClasses_cloud.Count; i++)
                {
                    string 藥碼 = medClasses_cloud[i].藥品碼;
                    medClasses_buf = keyValuePairs_medClasses.SortDictionaryByCode(藥碼);
                    if (medClasses_buf.Count == 0)
                    {
                        medClass medClass = medClasses_cloud[i];
                        medClass.GUID = Guid.NewGuid().ToString();
                        medClasses_add.Add(medClass);
                    }
                    else
                    {
                        if (medClasses_cloud[i].IsEqual(medClasses_buf[0]) == false)
                        {
                            medClass medClass = medClasses_buf[0];
                            medClass.Format(medClasses_cloud[i]);
                            medClasses_replace.Add(medClass);
                        }

                    }
                }
                List<object[]> list_medclass_add = medClasses_add.ClassToSQL<medClass, enum_medPharmacy>();
                List<object[]> list_medclass_replace = medClasses_replace.ClassToSQL<medClass, enum_medPharmacy>();
                if (list_medclass_add.Count > 0) sQLControl_med.AddRows(null, list_medclass_add);
                if (list_medclass_replace.Count > 0) sQLControl_med.UpdateByDefulteExtra(null, list_medclass_replace);



                SQLControl sQLControl_device = new SQLControl(Server, DB, "sd0_device_jsonstring", UserName, Password, Port, SSLMode);
                DeviceBasicMethod.SQL_Init(sQLControl_device);
                List<DeviceBasic> deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);
                List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
                List<DeviceBasic> deviceBasics_add = new List<DeviceBasic>();

                Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                for (int i = 0; i < medClasses_cloud.Count; i++)
                {
                    string 藥碼 = medClasses_cloud[i].藥品碼;
                    deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                    if (deviceBasics_buf.Count == 0)
                    {
                        DeviceBasic deviceBasic = new DeviceBasic();
                        deviceBasic.Code = medClasses_cloud[i].藥品碼;
                        deviceBasic.Name = medClasses_cloud[i].藥品名稱;
                        deviceBasic.Scientific_Name = medClasses_cloud[i].藥品學名;
                        deviceBasic.ChineseName = medClasses_cloud[i].中文名稱;
                        deviceBasic.Package = medClasses_cloud[i].包裝單位;
                        deviceBasics_add.Add(deviceBasic);

                    }

                }
   
             
                DeviceBasicMethod.SQL_AddDeviceBasic(sQLControl_device, deviceBasics_add);
                returnData.Data = "";
                returnData.Code = 200;
                returnData.Result = $"藥局更新成功,藥檔更新<{list_medclass_replace.Count}>筆,新增<{list_medclass_add.Count}>筆,新增[DeviceBasic]共<{deviceBasics_add.Count}>筆";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得藥庫系統的藥局藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "ServerName" : "ds01",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_ds_pharma_med")]
        [HttpPost]
        public string POST_get_ds_pharma_med(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_ds_pharma_med";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, "藥庫", "本地端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }

                string TaskTime_藥局 = "";
                string TaskTime_藥庫 = "";
                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();
                List<medClass> medClasses_cloud = new List<medClass>();
                List<medClass> medClasses_cloud_buf = new List<medClass>();


                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_phar", UserName, Password, Port, SSLMode);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);
                List<object[]> list_med_buf = new List<object[]>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_藥局 = new List<H_Pannel_lib.DeviceBasic>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_藥庫 = new List<H_Pannel_lib.DeviceBasic>();
                Dictionary<string, List<H_Pannel_lib.DeviceBasic>> dictionary_藥局 = new Dictionary<string, List<DeviceBasic>>();
                Dictionary<string, List<H_Pannel_lib.DeviceBasic>> dictionary_藥庫 = new Dictionary<string, List<DeviceBasic>>();
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    medClasses_cloud = Get_med_cloud(serverSettingClasses_med);
                    keyValuePairs_medClasses_cloud = medClasses_cloud.CoverToDictionaryByCode();

                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                    deviceBasics_藥局 = deviceController.Function_Get_device(serverSettingClasses_buf[0], "sd0_device_jsonstring");
                    dictionary_藥局 = deviceBasics_藥局.CoverToDictionaryByCode();                
                    TaskTime_藥局 = myTimerBasic1.ToString();

                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                    deviceBasics_藥庫 = deviceController.Function_Get_device(serverSettingClasses_buf[0], "firstclass_device_jsonstring");
                    dictionary_藥庫 = deviceBasics_藥庫.CoverToDictionaryByCode();          
                    TaskTime_藥庫 = myTimerBasic1.ToString();
                })));
                Task.WhenAll(tasks.ToArray()).Wait();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();
                string 藥碼 = "";
                int inventory = 0;        
                List<medClassBasic> medClasses = list_med.SQLToClass<medClassBasic, enum_medPharmacy>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    藥碼 = medClasses[i].藥品碼;
                    inventory = 0;
                    medClasses_cloud_buf = keyValuePairs_medClasses_cloud.SortDictionaryByCode(medClasses[i].藥品碼);
                    if (medClasses_cloud_buf.Count > 0)
                    {
                        medClasses[i].藥品名稱 = medClasses_cloud_buf[0].藥品名稱;
                        medClasses[i].藥品學名 = medClasses_cloud_buf[0].藥品學名;
                        medClasses[i].藥品許可證號 = medClasses_cloud_buf[0].藥品許可證號;
                        medClasses[i].開檔狀態 = medClasses_cloud_buf[0].開檔狀態;
                        medClasses[i].類別 = medClasses_cloud_buf[0].類別;
                    }
                    deviceBasics_buf = new List<DeviceBasic>();
                    if (dictionary_藥局.ContainsKey(藥碼))
                    {
                        deviceBasics_buf = dictionary_藥局[藥碼];
                    }
                    if (deviceBasics_buf.Count == 0) continue;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].DeviceBasics = deviceBasics_buf;
                    medClasses[i].藥局庫存 = inventory.ToString();

                    inventory = 0;
                    deviceBasics_buf = new List<DeviceBasic>();
                    if (dictionary_藥庫.ContainsKey(藥碼))
                    {
                        deviceBasics_buf = dictionary_藥庫[藥碼];
                    }
                    if (deviceBasics_buf.Count == 0) continue;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].藥庫庫存 = inventory.ToString();
                    medClasses[i].總庫存 = (medClasses[i].藥局庫存.StringToInt32() + medClasses[i].藥庫庫存.StringToInt32()).ToString();                  
                }

                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"藥局藥檔取得成功,藥局庫存取得耗時[{TaskTime_藥局}],藥庫庫存取得耗時[{TaskTime_藥庫}]";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID更新藥庫系統的藥局藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        [medclass陣列]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///         
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("update_ds_pharma_by_guid")]
        [HttpPost]
        public string POST_update_ds_pharma_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "update_ds_pharma_by_guid";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, "藥庫", "本地端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                List<medClass> medClasses = returnData.Data.ObjToClass<List<medClass>>();
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤";
                    return returnData.JsonSerializationt();
                }
 
                List<object[]> list_value_replace = medClasses.ClassToSQL<medClass, enum_medPharmacy>();


                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();

                SQLControl sQLControl = new SQLControl(Server, DB, "medicine_page_phar", UserName, Password, Port, SSLMode);

                sQLControl.UpdateByDefulteExtra(null, list_value_replace);

                returnData.Code = 200;
                returnData.Result = $"更新藥庫系統的藥局藥檔成功,共<{list_value_replace.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }


        /// <summary>
        /// 從線上藥檔更新藥庫系統的藥庫藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "ServerName" : "ds01",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("update_ds_drugstore_med_from_medcloud")]
        [HttpPost]
        public string update_ds_drugstore_med_from_medcloud(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_ds_pharma_med";
            returnData.TableName = "medicine_page_firstclass";
            returnData.ServerType = "藥庫";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, "藥庫", "本地端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                CheckCreatTable(returnData);
                string TaskTime_藥局 = "";
                string TaskTime_藥庫 = "";
                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];
                List<medClass> medClasses_cloud = Get_med_cloud(serverSettingClasses_med);
                List<medClass> medClasses_cloud_buf = new List<medClass>();





                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_firstclass", UserName, Password, Port, SSLMode);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_medDrugstore>();
                List<medClass> medClasses_buf = new List<medClass>();
                List<medClass> medClasses_add = new List<medClass>();
                List<medClass> medClasses_replace = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses = medClasses.CoverToDictionaryByCode();

                for (int i = 0; i < medClasses_cloud.Count; i++)
                {
                    string 藥碼 = medClasses_cloud[i].藥品碼;
                    medClasses_buf = keyValuePairs_medClasses.SortDictionaryByCode(藥碼);
                    if (medClasses_buf.Count == 0)
                    {
                        medClass medClass = medClasses_cloud[i];
                        medClass.GUID = Guid.NewGuid().ToString();
                        medClasses_add.Add(medClass);
                    }
                    else
                    {
                        if (medClasses_cloud[i].IsEqual(medClasses_buf[0]) == false)
                        {
                            medClass medClass = medClasses_buf[0];
                            medClass.Format(medClasses_cloud[i]);
                            medClasses_replace.Add(medClass);
                        }

                    }
                }
                List<object[]> list_medclass_add = medClasses_add.ClassToSQL<medClass, enum_medDrugstore>();
                List<object[]> list_medclass_replace = medClasses_replace.ClassToSQL<medClass, enum_medDrugstore>();
                if (list_medclass_add.Count > 0) sQLControl_med.AddRows(null, list_medclass_add);
                if (list_medclass_replace.Count > 0) sQLControl_med.UpdateByDefulteExtra(null, list_medclass_replace);



                SQLControl sQLControl_device = new SQLControl(Server, DB, "firstclass_device_jsonstring", UserName, Password, Port, SSLMode);
                DeviceBasicMethod.SQL_Init(sQLControl_device);
                List<DeviceBasic> deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);
                List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
                List<DeviceBasic> deviceBasics_add = new List<DeviceBasic>();

                Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                for (int i = 0; i < medClasses_cloud.Count; i++)
                {
                    string 藥碼 = medClasses_cloud[i].藥品碼;
                    deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                    if (deviceBasics_buf.Count == 0)
                    {
                        DeviceBasic deviceBasic = new DeviceBasic();
                        deviceBasic.Code = medClasses_cloud[i].藥品碼;
                        deviceBasic.Name = medClasses_cloud[i].藥品名稱;
                        deviceBasic.Scientific_Name = medClasses_cloud[i].藥品學名;
                        deviceBasic.ChineseName = medClasses_cloud[i].中文名稱;
                        deviceBasic.Package = medClasses_cloud[i].包裝單位;
                        deviceBasics_add.Add(deviceBasic);

                    }

                }


                DeviceBasicMethod.SQL_AddDeviceBasic(sQLControl_device, deviceBasics_add);
                returnData.Data = "";
                returnData.Code = 200;
                returnData.Result = $"藥庫更新成功,藥檔更新<{list_medclass_replace.Count}>筆,新增<{list_medclass_add.Count}>筆,新增[DeviceBasic]共<{deviceBasics_add.Count}>筆";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得藥庫系統的藥庫藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "ServerName" : "ds01",
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("get_ds_drugstore_med")]
        [HttpPost]
        public string POST_get_ds_drugstore_med(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_ds_drugstore_med";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, "藥庫", "本地端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }

                string TaskTime_藥局 = "";
                string TaskTime_藥庫 = "";
                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();
                List<medClass> medClasses_cloud = new List<medClass>();
                List<medClass> medClasses_cloud_buf = new List<medClass>();


                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_firstclass", UserName, Password, Port, SSLMode);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);
                List<object[]> list_med_buf = new List<object[]>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_藥局 = new List<H_Pannel_lib.DeviceBasic>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_藥庫 = new List<H_Pannel_lib.DeviceBasic>();
                Dictionary<string, List<H_Pannel_lib.DeviceBasic>> dictionary_藥局 = new Dictionary<string, List<DeviceBasic>>();
                Dictionary<string, List<H_Pannel_lib.DeviceBasic>> dictionary_藥庫 = new Dictionary<string, List<DeviceBasic>>();
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    medClasses_cloud = Get_med_cloud(serverSettingClasses_med);
                    keyValuePairs_medClasses_cloud = medClasses_cloud.CoverToDictionaryByCode();

                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                    deviceBasics_藥局 = deviceController.Function_Get_device(serverSettingClasses_buf[0], "sd0_device_jsonstring");
                    dictionary_藥局 = deviceBasics_藥局.CoverToDictionaryByCode();
                    TaskTime_藥局 = myTimerBasic1.ToString();

                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                    deviceBasics_藥庫 = deviceController.Function_Get_device(serverSettingClasses_buf[0], "firstclass_device_jsonstring");
                    dictionary_藥庫 = deviceBasics_藥庫.CoverToDictionaryByCode();
                    TaskTime_藥庫 = myTimerBasic1.ToString();
                })));
                Task.WhenAll(tasks.ToArray()).Wait();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();
                string 藥碼 = "";
                int inventory = 0;
                List<medClassBasic> medClasses = list_med.SQLToClass<medClassBasic, enum_medDrugstore>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    藥碼 = medClasses[i].藥品碼;
                    inventory = 0;
                    medClasses_cloud_buf = keyValuePairs_medClasses_cloud.SortDictionaryByCode(medClasses[i].藥品碼);
                    if (medClasses_cloud_buf.Count > 0)
                    {
                        medClasses[i].藥品名稱 = medClasses_cloud_buf[0].藥品名稱;
                        medClasses[i].藥品學名 = medClasses_cloud_buf[0].藥品學名;
                        medClasses[i].藥品許可證號 = medClasses_cloud_buf[0].藥品許可證號;
                        medClasses[i].開檔狀態 = medClasses_cloud_buf[0].開檔狀態;
                        medClasses[i].類別 = medClasses_cloud_buf[0].類別;
                    }
                    deviceBasics_buf = new List<DeviceBasic>();
                    if (dictionary_藥局.ContainsKey(藥碼))
                    {
                        deviceBasics_buf = dictionary_藥局[藥碼];
                    }
                    if (deviceBasics_buf.Count == 0) continue;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
          
                    medClasses[i].藥局庫存 = inventory.ToString();

                    deviceBasics_buf = new List<DeviceBasic>();
                    if (dictionary_藥庫.ContainsKey(藥碼))
                    {
                        deviceBasics_buf = dictionary_藥庫[藥碼];
                    }

                    inventory = 0;
                    if (deviceBasics_buf.Count == 0) continue;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].DeviceBasics = deviceBasics_buf;
                    medClasses[i].藥庫庫存 = inventory.ToString();
                    medClasses[i].總庫存 = (medClasses[i].藥局庫存.StringToInt32() + medClasses[i].藥庫庫存.StringToInt32()).ToString();
                }

                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"藥局藥檔取得成功,藥局庫存取得耗時[{TaskTime_藥局}],藥庫庫存取得耗時[{TaskTime_藥庫}]";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID更新藥庫系統的藥庫藥檔資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        /// {
        ///     "Data": 
        ///     {
        ///        [medclass陣列]
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///         
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [Route("update_ds_drugstore_by_guid")]
        [HttpPost]
        public string POST_update_ds_store_by_guid(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "update_ds_drugstore_by_guid";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, "藥庫", "本地端");
                if (serverSettingClasses_buf.Count == 0)
                {
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                }
                List<medClass> medClasses = returnData.Data.ObjToClass<List<medClass>>();
                if (medClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料錯誤";
                    return returnData.JsonSerializationt();
                }

                List<object[]> list_value_replace = medClasses.ClassToSQL<medClass, enum_medDrugstore>();


                string Server = serverSettingClasses_buf[0].Server;
                string DB = serverSettingClasses_buf[0].DBName;
                string UserName = serverSettingClasses_buf[0].User;
                string Password = serverSettingClasses_buf[0].Password;
                uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();

                SQLControl sQLControl = new SQLControl(Server, DB, "medicine_page_firstclass", UserName, Password, Port, SSLMode);

                sQLControl.UpdateByDefulteExtra(null, list_value_replace);

                returnData.Code = 200;
                returnData.Result = $"更新藥庫系統的藥庫藥檔成功,共<{list_value_replace.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();

                return returnData.JsonSerializationt(false);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                Logger.Log($"MED_page", $"[異常] { returnData.Result}");
                return returnData.JsonSerializationt(true);
            }
        }

        /// <summary>
        /// 查詢藥品資料JSON格式範例
        /// </summary>
        /// <remarks>
        /// [必要輸入參數說明]<br/> 
        ///  1.[returnData.ServerName] : 填入記憶的伺服器名稱(medicine_page_cloud可預設為"Main")<br/> 
        ///  2.[returnData.ServerType] : 填入記憶的種類(medicine_page_cloud可預設為"網頁")<br/> 
        ///  3.[returnData.TableName] : 選擇其中一種資料表名稱:medicine_page_cloud、medicine_page_firstclass、medicine_page_phar、medicine_page<br/> 
        ///  --------------------------------------------
        /// <code>
        /// {
        ///     "ServerName" : "Main",
        ///     "ServerType" : "網頁",  
        ///     "TableName" : "medicine_page_cloud",  
        ///     "Data": 
        ///     {
        ///         
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///     
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [Route("get_by_apiserver")]
        [HttpPost]
        public string POST_get_by_apiserver([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string TableName = returnData.TableName;
            returnData.Method = "get_by_apiserver";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {        
                if (TableName == "medicine_page_cloud")
                {                  
                    string json_out = POST_get_med_cloud(returnData);
                    return json_out;
                }
                //藥庫藥品資料
                if (TableName == "medicine_page_firstclass")
                {
                    string TaskTime_藥局 = "";
                    string TaskTime_藥庫 = "";
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetAllRows(null);
                    List<object[]> list_med_buf = new List<object[]>();
                    List<H_Pannel_lib.DeviceSimple> deviceBasics_藥局 = new List<H_Pannel_lib.DeviceSimple>();
                    List<H_Pannel_lib.DeviceSimple> deviceBasics_藥庫 = new List<H_Pannel_lib.DeviceSimple>();

                    List<Task> tasks = new List<Task>();

                    tasks.Add(Task.Run(new Action(delegate
                    {
                        MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                        deviceBasics_藥局 = deviceController.Function_Get_deviceSimple(serverSettingClasses[0], "sd0_device_jsonstring");
                        List<H_Pannel_lib.DeviceSimple> deviceBasics_buf = new List<H_Pannel_lib.DeviceSimple>();
                        string 藥碼 = "";
                        int inventory = 0;
                        for (int i = 0; i < list_med.Count; i++)
                        {
                            inventory = 0;
                            藥碼 = list_med[i][(int)enum_medDrugstore.藥品碼].ObjectToString();
                            deviceBasics_buf = (from temp in deviceBasics_藥局
                                                where temp.Code == 藥碼
                                                select temp).ToList();
                            for (int k = 0; k < deviceBasics_buf.Count; k++)
                            {
                                inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                            }
                            list_med[i][(int)enum_medDrugstore.藥局庫存] = inventory.ToString();
                        }
                        TaskTime_藥局 = myTimerBasic1.ToString();

                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                        deviceBasics_藥庫 = deviceController.Function_Get_deviceSimple(serverSettingClasses[0], "firstclass_device_jsonstring");
                        List<H_Pannel_lib.DeviceSimple> deviceBasics_buf = new List<H_Pannel_lib.DeviceSimple>();
                        string 藥碼 = "";
                        int inventory = 0;
                        for (int i = 0; i < list_med.Count; i++)
                        {
                            inventory = 0;
                            藥碼 = list_med[i][(int)enum_medDrugstore.藥品碼].ObjectToString();
                            deviceBasics_buf = (from temp in deviceBasics_藥庫
                                                where temp.Code == 藥碼
                                                select temp).ToList();
                            for (int k = 0; k < deviceBasics_buf.Count; k++)
                            {
                                inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                            }
                            list_med[i][(int)enum_medDrugstore.藥庫庫存] = inventory.ToString();
                        }
                        TaskTime_藥庫 = myTimerBasic1.ToString();
                    })));
                    Task.WhenAll(tasks).Wait();

                    returnData.Data = list_med.SQLToClass<medClass, enum_medDrugstore>();
                    returnData.Code = 200;
                    returnData.Result = $"藥庫藥檔取得成功,藥局庫存取得耗時[{TaskTime_藥局}],藥庫庫存取得耗時[{TaskTime_藥庫}]";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                //藥局藥品資料
                if (TableName == "medicine_page_phar")
                {
                   
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");

                    string TaskTime_藥局 = "";
                    string TaskTime_藥庫 = "";
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetAllRows(null);
                    List<object[]> list_med_buf = new List<object[]>();
                    List<H_Pannel_lib.DeviceSimple> deviceBasics_藥局 = new List<H_Pannel_lib.DeviceSimple>();
                    List<H_Pannel_lib.DeviceSimple> deviceBasics_藥庫 = new List<H_Pannel_lib.DeviceSimple>();
           
                    List<Task> tasks = new List<Task>();
    
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                        deviceBasics_藥局 = deviceController.Function_Get_deviceSimple(serverSettingClasses[0], "sd0_device_jsonstring");
                        Dictionary<string, List<H_Pannel_lib.DeviceSimple>> dictionary = ConvertToDictionary(deviceBasics_藥局);
                        List<H_Pannel_lib.DeviceSimple> deviceBasics_buf = new List<H_Pannel_lib.DeviceSimple>();
                        string 藥碼 = "";
                        int inventory = 0;
                        for (int i = 0; i < list_med.Count; i++)
                        {
                            inventory = 0;
                            藥碼 = list_med[i][(int)enum_medDrugstore.藥品碼].ObjectToString();
                            deviceBasics_buf = new List<H_Pannel_lib.DeviceSimple>();
                            if (dictionary.ContainsKey(藥碼))
                            {
                                deviceBasics_buf = dictionary[藥碼];
                            }
                            if (deviceBasics_buf == null) continue;
                            for (int k = 0; k < deviceBasics_buf.Count; k++)
                            {
                                inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                            }
                            list_med[i][(int)enum_medDrugstore.藥局庫存] = inventory.ToString();
                        }
                        TaskTime_藥局 = myTimerBasic1.ToString();

                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        MyTimerBasic myTimerBasic1 = new MyTimerBasic();
                        deviceBasics_藥庫 = deviceController.Function_Get_deviceSimple(serverSettingClasses[0], "firstclass_device_jsonstring");
                        List<H_Pannel_lib.DeviceSimple> deviceBasics_buf = new List<H_Pannel_lib.DeviceSimple>();
                        Dictionary<string,List<H_Pannel_lib.DeviceSimple>> dictionary = ConvertToDictionary(deviceBasics_藥庫);
                        string 藥碼 = "";
                        int inventory = 0;
                
                        for (int i = 0; i < list_med.Count; i++)
                        {
                            inventory = 0;
                            藥碼 = list_med[i][(int)enum_medDrugstore.藥品碼].ObjectToString();
                            //deviceBasics_buf = (from temp in deviceBasics_藥庫
                            //                    where temp.Code == 藥碼
                            //                    select temp).ToList();
                            deviceBasics_buf = new List<H_Pannel_lib.DeviceSimple>();
                            if (dictionary.ContainsKey(藥碼))
                            {
                                deviceBasics_buf = dictionary[藥碼];
                            }                              
                            if (deviceBasics_buf == null) continue;
                            for (int k = 0; k < deviceBasics_buf.Count; k++)
                            {
                                inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                            }
                            list_med[i][(int)enum_medDrugstore.藥庫庫存] = inventory.ToString();
                        }
                        TaskTime_藥庫 = myTimerBasic1.ToString();
                    })));
                    Task.WhenAll(tasks.ToArray()).Wait();


                                    
                    returnData.Data = list_med.SQLToClass<medClassBasic, enum_medDrugstore>();
                    returnData.Code = 200;
                    returnData.Result = $"藥局藥檔取得成功,藥局庫存取得耗時[{TaskTime_藥局}],藥庫庫存取得耗時[{TaskTime_藥庫}]";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                //調劑台藥品資料
                if (TableName == "medicine_page")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    //取得雲端藥檔資料
                    List<medClass> medClasses_cloud = new List<medClass>();
                    List<medClass> medClasses_cloud_buf = new List<medClass>();
                    List<medClass> medClasses_src = new List<medClass>();
                    List<medClass> medClasses_src_buf = new List<medClass>();
                    Dictionary<string, List<medClass>> keyValuePairs_medClasses_src = new Dictionary<string, List<medClass>>();
                    Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();

                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = new List<object[]>();
                    List<object[]> list_med_add = new List<object[]>();

                    List<H_Pannel_lib.DeviceBasic> deviceBasics = new List<H_Pannel_lib.DeviceBasic>();
                    List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();

                    Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = new Dictionary<string, List<DeviceBasic>>();

                    List<Task> tasks = new List<Task>();
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        medClasses_cloud = Get_med_cloud(serverSettingClasses_med);
                        keyValuePairs_medClasses_cloud = medClasses_cloud.CoverToDictionaryByCode();
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        list_med = sQLControl_med.GetAllRows(null);
                        medClasses_src = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                        keyValuePairs_medClasses_src = medClasses_src.CoverToDictionaryByCode();
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        deviceBasics = deviceController.Function_Get_device(serverSettingClasses[0]);
                        keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                 

                    })));
                    Task.WhenAll(tasks).Wait();



                    string 藥碼 = "";
                    for (int i = 0; i < medClasses_cloud.Count; i++)
                    {
                        藥碼 = medClasses_cloud[i].藥品碼;
                        medClasses_src = keyValuePairs_medClasses_src.SortDictionaryByCode(藥碼);
                        if (medClasses_src.Count == 0)
                        {
                            object[] value = medClasses_cloud[i].ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                            value[(int)enum_藥品資料_藥檔資料.GUID] = Guid.NewGuid().ToString();
                            list_med_add.Add(value);
                        }
                    }
                    sQLControl_med.AddRows(null, list_med_add);
                    list_med.LockAdd(list_med_add);
                    list_med.Sort(new Icp_藥品資料_藥檔資料());
                    List<medClass> medClasses = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    for (int i = 0; i < medClasses.Count; i++)
                    {
                        藥碼 = medClasses[i].藥品碼;
                        deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                        int inventory = 0;
                        medClasses[i].DeviceBasics = deviceBasics_buf;
                        for (int k = 0; k < deviceBasics_buf.Count; k++)
                        {
                            inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                        }
                        medClasses[i].庫存 = inventory.ToString();
                    }

               
          
                    returnData.Data = medClasses;
                    returnData.Code = 200;
                    returnData.Result = $"調劑台藥檔取得成功!新增<{list_med_add.Count}>筆資料!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    string json_out = returnData.JsonSerializationt(false);
                    return json_out;
                }

                returnData.Code = -200;
                returnData.Result = "藥檔取得失敗!";
                return returnData.JsonSerializationt();
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
          
        }
        /// <summary>
        /// 取得[調劑台藥檔]
        /// </summary>
        /// <remarks>
        /// <code>
        /// {
        ///     "ServerName" : "A5",
        ///     "Data": 
        ///     {
        ///         
        ///     },
        ///     "ValueAry" : 
        ///     [
        ///        
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [Route("get_dps_medClass")]
        [HttpPost]
        public string POST_get_dps_medclass([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string TableName = "medicine_page";
            returnData.ServerType = "調劑台";
            returnData.Method = "get_dps_medclass";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                //取得雲端藥檔資料
                List<medClass> medClasses_cloud = new List<medClass>();
                List<medClass> medClasses_cloud_buf = new List<medClass>();
                List<medClass> medClasses_src = new List<medClass>();
                List<medClass> medClasses_src_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_src = new Dictionary<string, List<medClass>>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();

                SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med = new List<object[]>();
                List<object[]> list_med_add = new List<object[]>();

                List<H_Pannel_lib.DeviceBasic> deviceBasics = new List<H_Pannel_lib.DeviceBasic>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();

                Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = new Dictionary<string, List<DeviceBasic>>();

                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    medClasses_cloud = Get_med_cloud(serverSettingClasses_med);
                    keyValuePairs_medClasses_cloud = medClasses_cloud.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    list_med = sQLControl_med.GetAllRows(null);
                    medClasses_src = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    keyValuePairs_medClasses_src = medClasses_src.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    deviceBasics = deviceController.Function_Get_device(serverSettingClasses[0]);
                    keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {


                })));
                Task.WhenAll(tasks).Wait();



                string 藥碼 = "";
                for (int i = 0; i < medClasses_cloud.Count; i++)
                {
                    藥碼 = medClasses_cloud[i].藥品碼;
                    medClasses_src = keyValuePairs_medClasses_src.SortDictionaryByCode(藥碼);
                    if (medClasses_src.Count == 0)
                    {
                        object[] value = medClasses_cloud[i].ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                        value[(int)enum_藥品資料_藥檔資料.GUID] = Guid.NewGuid().ToString();
                        list_med_add.Add(value);
                    }
                }
                sQLControl_med.AddRows(null, list_med_add);
                list_med.LockAdd(list_med_add);
                list_med.Sort(new Icp_藥品資料_藥檔資料());
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    藥碼 = medClasses[i].藥品碼;
                    deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                    int inventory = 0;
                    medClasses[i].DeviceBasics = deviceBasics_buf;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].庫存 = inventory.ToString();
                }



                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"[調劑台]藥檔取得成功!新增<{list_med_add.Count}>筆資料!";
                returnData.TimeTaken = myTimerBasic.ToString();
                string json_out = returnData.JsonSerializationt(false);
                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 以藥碼搜尋[調劑台藥檔]
        /// </summary>
        /// <remarks>
        /// <code>
        /// {
        ///     "ServerName" : "A5",
        ///     "Data": 
        ///     {
        ///         
        ///     },
        ///     "Value" : "[前綴,模糊]",
        ///     "ValueAry" : 
        ///     [
        ///        [藥碼1,藥碼2,藥碼3]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [Route("get_dps_medClass_by_code")]
        [HttpPost]
        public string POST_get_dps_medClass_by_code([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string TableName = "medicine_page";
            returnData.ServerType = "調劑台";
            returnData.Method = "get_dps_medClass_by_code";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼1,藥碼2,藥碼3]";
                    return returnData.JsonSerializationt(true);
                }

                //取得雲端藥檔資料
                List<medClass> medClasses_cloud = new List<medClass>();
                List<medClass> medClasses_cloud_buf = new List<medClass>();
                List<medClass> medClasses_src = new List<medClass>();
                List<medClass> medClasses_src_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_src = new Dictionary<string, List<medClass>>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();

                SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_src = new List<object[]>();
                List<object[]> list_med = new List<object[]>();
                List<object[]> list_med_buf = new List<object[]>();

                List<H_Pannel_lib.DeviceBasic> deviceBasics = new List<H_Pannel_lib.DeviceBasic>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();

                Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = new Dictionary<string, List<DeviceBasic>>();

                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    medClasses_cloud = Get_med_cloud(serverSettingClasses_med);
                    keyValuePairs_medClasses_cloud = medClasses_cloud.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    list_med_src = sQLControl_med.GetAllRows(null);

                    string[] Codes = returnData.ValueAry[0].Split(",");
                    for (int i = 0; i < Codes.Length; i++)
                    {
                        if (returnData.Value == "前綴")
                        {
                            list_med_buf = list_med_src.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.藥品碼, Codes[i]);
                        }
                        else if (returnData.Value == "標準")
                        {
                            list_med_buf = list_med_src.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, Codes[i]);
                        }
                        else
                        {
                            list_med_buf = list_med_src.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品碼, Codes[i]);
                        }

                    }
                    list_med = list_med_buf;
                    medClasses_src = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    keyValuePairs_medClasses_src = medClasses_src.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    deviceBasics = deviceController.Function_Get_device(serverSettingClasses[0]);
                    keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {


                })));
                Task.WhenAll(tasks).Wait();



                string 藥碼 = "";
                list_med.Sort(new Icp_藥品資料_藥檔資料());
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    藥碼 = medClasses[i].藥品碼;
                    deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                    int inventory = 0;
                    medClasses[i].DeviceBasics = deviceBasics_buf;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].庫存 = inventory.ToString();
                }



                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"[調劑台] ServerName : {returnData.ServerName} , 藥碼 : {returnData.ValueAry[0]} ,藥檔取得成功 ,共<{medClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                string json_out = returnData.JsonSerializationt(false);
                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 以藥名搜尋[調劑台藥檔]
        /// </summary>
        /// <remarks>
        /// <code>
        /// {
        ///     "ServerName" : "A5",
        ///     "Data": 
        ///     {
        ///         
        ///     },
        ///     "Value" : "[前綴]",
        ///     "ValueAry" : 
        ///     [
        ///        [藥名]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [Route("get_dps_medClass_by_name")]
        [HttpPost]
        public string POST_get_dps_medClass_by_name([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string TableName = "medicine_page";
            returnData.ServerType = "調劑台";
            returnData.Method = "get_dps_medClass_by_name";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥名]";
                    return returnData.JsonSerializationt(true);
                }

                //取得雲端藥檔資料

                List<medClass> medClasses_src = new List<medClass>();
                List<medClass> medClasses_src_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_src = new Dictionary<string, List<medClass>>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();

                SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_src = new List<object[]>();
                List<object[]> list_med = new List<object[]>();
                List<object[]> list_med_buf = new List<object[]>();

                List<H_Pannel_lib.DeviceBasic> deviceBasics = new List<H_Pannel_lib.DeviceBasic>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();

                Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = new Dictionary<string, List<DeviceBasic>>();

                List<Task> tasks = new List<Task>();
      
                tasks.Add(Task.Run(new Action(delegate
                {
                    list_med_src = sQLControl_med.GetAllRows(null);

                    if (returnData.Value == "前綴")
                    {
                        list_med_buf = list_med_src.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.藥品名稱, returnData.ValueAry[0]);
                    }
                    else if (returnData.Value == "標準")
                    {
                        list_med_buf = list_med_src.GetRows((int)enum_藥品資料_藥檔資料.藥品名稱, returnData.ValueAry[0]);
                    }
                    else
                    {
                        list_med_buf = list_med_src.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品名稱, returnData.ValueAry[0]);
                    }


                    list_med = list_med_buf;
                    medClasses_src = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    keyValuePairs_medClasses_src = medClasses_src.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    deviceBasics = deviceController.Function_Get_device(serverSettingClasses[0]);
                    keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {


                })));
                Task.WhenAll(tasks).Wait();



                string 藥碼 = "";
                list_med.Sort(new Icp_藥品資料_藥檔資料());
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    藥碼 = medClasses[i].藥品碼;
                    deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                    int inventory = 0;
                    medClasses[i].DeviceBasics = deviceBasics_buf;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].庫存 = inventory.ToString();
                }



                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"[調劑台] ServerName : {returnData.ServerName} , 藥碼 : {returnData.ValueAry[0]} ,藥檔取得成功 ,共<{medClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                string json_out = returnData.JsonSerializationt(false);
                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 以中文名搜尋[調劑台藥檔]
        /// </summary>
        /// <remarks>
        /// <code>
        /// {
        ///     "ServerName" : "A5",
        ///     "Data": 
        ///     {
        ///         
        ///     },
        ///     "Value" : "[前綴]",
        ///     "ValueAry" : 
        ///     [
        ///        [中文名]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [Route("get_dps_medClass_by_chtname")]
        [HttpPost]
        public string POST_get_dps_medClass_by_chtname([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string TableName = "medicine_page";
            returnData.ServerType = "調劑台";
            returnData.Method = "get_dps_medClass_by_chtname";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[中文名]";
                    return returnData.JsonSerializationt(true);
                }

                //取得雲端藥檔資料

                List<medClass> medClasses_src = new List<medClass>();
                List<medClass> medClasses_src_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_src = new Dictionary<string, List<medClass>>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();

                SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_src = new List<object[]>();
                List<object[]> list_med = new List<object[]>();
                List<object[]> list_med_buf = new List<object[]>();

                List<H_Pannel_lib.DeviceBasic> deviceBasics = new List<H_Pannel_lib.DeviceBasic>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();

                Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = new Dictionary<string, List<DeviceBasic>>();

                List<Task> tasks = new List<Task>();

                tasks.Add(Task.Run(new Action(delegate
                {
                    list_med_src = sQLControl_med.GetAllRows(null);

                    if (returnData.Value == "前綴")
                    {
                        list_med_buf = list_med_src.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.中文名稱, returnData.ValueAry[0]);
                    }
                    else if (returnData.Value == "標準")
                    {
                        list_med_buf = list_med_src.GetRows((int)enum_藥品資料_藥檔資料.中文名稱, returnData.ValueAry[0]);
                    }
                    else
                    {
                        list_med_buf = list_med_src.GetRowsByLike((int)enum_藥品資料_藥檔資料.中文名稱, returnData.ValueAry[0]);
                    }


                    list_med = list_med_buf;
                    medClasses_src = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    keyValuePairs_medClasses_src = medClasses_src.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    deviceBasics = deviceController.Function_Get_device(serverSettingClasses[0]);
                    keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {


                })));
                Task.WhenAll(tasks).Wait();



                string 藥碼 = "";
                list_med.Sort(new Icp_藥品資料_藥檔資料());
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    藥碼 = medClasses[i].藥品碼;
                    deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                    int inventory = 0;
                    medClasses[i].DeviceBasics = deviceBasics_buf;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].庫存 = inventory.ToString();
                }



                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"[調劑台] ServerName : {returnData.ServerName} , 藥碼 : {returnData.ValueAry[0]} ,藥檔取得成功 ,共<{medClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                string json_out = returnData.JsonSerializationt(false);
                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        /// <summary>
        /// 以中文名搜尋[調劑台藥檔]
        /// </summary>
        /// <remarks>
        /// <code>
        /// {
        ///     "ServerName" : "A5",
        ///     "Data": 
        ///     {
        ///         
        ///     },
        ///     "Value" : "[前綴]",
        ///     "ValueAry" : 
        ///     [
        ///        [商品名]
        ///     ]
        ///     
        /// }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [Route("get_dps_medClass_by_dianame")]
        [HttpPost]
        public string POST_get_dps_medClass_by_dianame([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string TableName = "medicine_page";
            returnData.ServerType = "調劑台";
            returnData.Method = "get_dps_medClass_by_dianame";
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];

                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[中文名]";
                    return returnData.JsonSerializationt(true);
                }

                //取得雲端藥檔資料

                List<medClass> medClasses_src = new List<medClass>();
                List<medClass> medClasses_src_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_src = new Dictionary<string, List<medClass>>();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = new Dictionary<string, List<medClass>>();

                SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                List<object[]> list_med_src = new List<object[]>();
                List<object[]> list_med = new List<object[]>();
                List<object[]> list_med_buf = new List<object[]>();

                List<H_Pannel_lib.DeviceBasic> deviceBasics = new List<H_Pannel_lib.DeviceBasic>();
                List<H_Pannel_lib.DeviceBasic> deviceBasics_buf = new List<H_Pannel_lib.DeviceBasic>();

                Dictionary<string, List<DeviceBasic>> keyValuePairs_deviceBasics = new Dictionary<string, List<DeviceBasic>>();

                List<Task> tasks = new List<Task>();

                tasks.Add(Task.Run(new Action(delegate
                {
                    list_med_src = sQLControl_med.GetAllRows(null);

                    if (returnData.Value == "前綴")
                    {
                        list_med_buf = list_med_src.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.藥品學名, returnData.ValueAry[0]);
                    }
                    else if (returnData.Value == "標準")
                    {
                        list_med_buf = list_med_src.GetRows((int)enum_藥品資料_藥檔資料.藥品學名, returnData.ValueAry[0]);
                    }
                    else
                    {
                        list_med_buf = list_med_src.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品學名, returnData.ValueAry[0]);
                    }


                    list_med = list_med_buf;
                    medClasses_src = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    keyValuePairs_medClasses_src = medClasses_src.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    deviceBasics = deviceController.Function_Get_device(serverSettingClasses[0]);
                    keyValuePairs_deviceBasics = deviceBasics.CoverToDictionaryByCode();
                })));
                tasks.Add(Task.Run(new Action(delegate
                {


                })));
                Task.WhenAll(tasks).Wait();



                string 藥碼 = "";
                list_med.Sort(new Icp_藥品資料_藥檔資料());
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                for (int i = 0; i < medClasses.Count; i++)
                {
                    藥碼 = medClasses[i].藥品碼;
                    deviceBasics_buf = keyValuePairs_deviceBasics.SortDictionaryByCode(藥碼);
                    int inventory = 0;
                    medClasses[i].DeviceBasics = deviceBasics_buf;
                    for (int k = 0; k < deviceBasics_buf.Count; k++)
                    {
                        inventory += deviceBasics_buf[k].Inventory.StringToInt32();
                    }
                    medClasses[i].庫存 = inventory.ToString();
                }



                returnData.Data = medClasses;
                returnData.Code = 200;
                returnData.Result = $"[調劑台] ServerName : {returnData.ServerName} , 藥碼 : {returnData.ValueAry[0]} ,藥檔取得成功 ,共<{medClasses.Count}>筆資料";
                returnData.TimeTaken = myTimerBasic.ToString();
                string json_out = returnData.JsonSerializationt(false);
                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }


        [Route("get_by_code")]
        [HttpPost]
        public string POST_get_by_code([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string TableName = returnData.TableName;
                returnData.Method = "get_by_code";
                string Code = returnData.Value;
                if(Code.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "Code空白!";
                    return returnData.JsonSerializationt();
                }
                if (TableName == "medicine_page_cloud")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    List<ServerSettingClass> serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    if (serverSettingClasses_buf.Count == 0)
                    {
                        serverSettingClasses_buf = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "VM端");
                    }
                    string Server = serverSettingClasses_buf[0].Server;
                    string DB = serverSettingClasses_buf[0].DBName;
                    string UserName = serverSettingClasses_buf[0].User;
                    string Password = serverSettingClasses_buf[0].Password;
                    uint Port = (uint)serverSettingClasses_buf[0].Port.StringToInt32();
                    if (serverSettingClasses_buf.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_雲端藥檔.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_雲端藥檔>();
                    returnData.Code = 200;
                    returnData.Result = "雲端藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_firstclass")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_medDrugstore.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_medDrugstore>();
                    returnData.Code = 200;
                    returnData.Result = "藥庫藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_phar")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_medDrugstore.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_medDrugstore>();
                    returnData.Code = 200;
                    returnData.Result = "藥局藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }

                if (TableName == "medicine_page")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_med = sQLControl_med.GetRowsByDefult(null, (int)enum_藥品資料_藥檔資料.藥品碼, Code);
                    returnData.Data = list_med.SQLToClass<medClass, enum_藥品資料_藥檔資料>();
                    returnData.Code = 200;
                    returnData.Result = "調劑台藥檔取得成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }

                returnData.Code = -200;
                returnData.Result = "藥檔取得失敗!";
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }
        [Route("upadte_by_guid")]
        [HttpPost]
        public string POST_upadte_by_guid([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string TableName = returnData.TableName;
                returnData.Method = "upadte_by_guid";
     
      
                List<medClass> medClasses = new List<medClass>();
                medClasses = returnData.Data.ObjToListClass<medClass>();
                if (medClasses == null)
                {
                    medClass medClass = returnData.Data.ObjToClass<medClass>();
                    if (medClass != null)
                    {
                        medClasses = new List<medClass>();
                        medClasses.Add(medClass);
                    }
                }
                if (medClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "反序列化失敗!";
                    return returnData.JsonSerializationt();
                }
                if (TableName == "medicine_page_cloud")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);
                    List<object[]> list_value = sQLControl_med.GetAllRows(null);
                    List<object[]> list_value_buf = new List<object[]>();
                    List<object[]> list_value_add = new List<object[]>();
                    List<object[]> list_value_replace = new List<object[]>();
                    for (int i = 0; i < medClasses.Count; i++)
                    {
                        string Code = medClasses[i].藥品碼;
                        list_value_buf = list_value.GetRows((int)enum_雲端藥檔.藥品碼, Code);
                        if(list_value_buf.Count == 0)
                        {
                            object[] value = medClasses[i].ClassToSQL<medClass, enum_雲端藥檔>();
                            value[(int)enum_雲端藥檔.GUID] = Guid.NewGuid().ToString();
                            list_value_add.Add(value);
                        }
                        else
                        {
                            object[] value = medClasses[i].ClassToSQL<medClass, enum_雲端藥檔>();
                            value[(int)enum_雲端藥檔.GUID] = list_value_buf[0][(int)enum_雲端藥檔.GUID].ObjectToString();
                            list_value_replace.Add(value);
                        }    

                    }

                    sQLControl_med.UpdateByDefulteExtra(null, list_value_replace);
                    sQLControl_med.AddRows(null, list_value_add);
                    returnData.Code = 200;
                    returnData.Result = $"雲端藥檔更新成功!新增<{list_value_add.Count}>筆,修改<{list_value_replace.Count}>筆";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_firstclass")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                    List<object[]> list_replace = medClasses.ClassToSQL<medClass, enum_medDrugstore>();
                    sQLControl_med.UpdateByDefulteExtra(null, list_replace);
                    returnData.Code = 200;
                    returnData.Result = "藥庫藥檔更新成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page_phar")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                    List<object[]> list_replace = medClasses.ClassToSQL<medClass, enum_medDrugstore>();
                    sQLControl_med.UpdateByDefulteExtra(null, list_replace);
                    returnData.Code = 200;
                    returnData.Result = "藥局藥檔更新成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                if (TableName == "medicine_page")
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                    string Server = serverSettingClasses[0].Server;
                    string DB = serverSettingClasses[0].DBName;
                    string UserName = serverSettingClasses[0].User;
                    string Password = serverSettingClasses[0].Password;
                    uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                    if (serverSettingClasses.Count == 0)
                    {
                        returnData.Code = -200;
                        returnData.Result = $"找無Server資料!";
                        return returnData.JsonSerializationt();
                    }

                    CheckCreatTable(returnData);
                    SQLControl sQLControl_med = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);

                    List<object[]> list_replace = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                    sQLControl_med.UpdateByDefulteExtra(null, list_replace);
                    returnData.Code = 200;
                    returnData.Result = "調劑台藥檔更新成功!";
                    returnData.TimeTaken = myTimerBasic.ToString();
                    return returnData.JsonSerializationt(true);
                }
                returnData.Code = -200;
                returnData.Result = "更新藥檔失敗!";

                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
         
        }

        [Route("serch_by_BarCode")]
        [HttpPost]
        public string POST_serch_by_BarCode([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                List<medClass> medClasses = returnData.Data.ObjToListClass<medClass>();
                if (medClasses == null || medClasses.Count == 0)
                {
                    List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                    ServerSettingClass serverSettingClasses_med = serverSettingClasses.MyFind("Main", "網頁", "藥檔資料")[0];
                    medClasses = Get_med_cloud(serverSettingClasses_med);
                }

                returnData.Method = "serch_by_BarCode";
                string BarCode = returnData.Value;
                if(BarCode.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "BarCode空白!";
                    return returnData.JsonSerializationt();
                }
    
                List<medClass> medClasses_buf = new List<medClass>();

                for(int i = 0; i < medClasses.Count; i++)
                {
                    if (BarCode == medClasses[i].藥品碼)
                    {
                        medClasses_buf.Add(medClasses[i]);
                        continue;
                    }
                    if (BarCode == medClasses[i].料號)
                    {
                        medClasses_buf.Add(medClasses[i]);
                        continue;
                    }
                    foreach (string barcode in medClasses[i].Barcode)
                    {
                        if (barcode == BarCode)
                        {
                            medClasses_buf.Add(medClasses[i]);
                            continue;
                        }
                    }
                }
                if(medClasses_buf.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.Data = medClasses_buf;
                returnData.Code = 200;
                returnData.Result = "搜尋BARCODE完成!";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }

        static public Dictionary<string, List<H_Pannel_lib.DeviceSimple>> ConvertToDictionary(List<H_Pannel_lib.DeviceSimple>  deviceSimples)
        {
            Dictionary<string, List<H_Pannel_lib.DeviceSimple>> dictionary = new Dictionary<string, List<H_Pannel_lib.DeviceSimple>>();

            foreach (var item in deviceSimples)
            {
                string _key = item.Code;

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(_key))
                {
                    dictionary[_key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<H_Pannel_lib.DeviceSimple> values = new List<H_Pannel_lib.DeviceSimple> { item };
                    dictionary[_key] = values;
                }
            }

            return dictionary;
        }
        private string CheckCreatTable(returnData returnData)
        {
            string TableName = returnData.TableName;
            Table table = new Table(TableName);
            if (TableName == "medicine_page_cloud")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "藥檔資料");
                returnData.Method = "get_init";
                
                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                table = MethodClass.CheckCreatTable(serverSettingClasses[0], new enum_雲端藥檔());        
            }
            if (TableName == "medicine_page_firstclass")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                returnData.Method = "get_init";

                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }


                table = MethodClass.CheckCreatTable(serverSettingClasses[0], new enum_medDrugstore());            
            }
            if (TableName == "medicine_page_phar")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                returnData.Method = "get_init";

                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }

                table = MethodClass.CheckCreatTable(serverSettingClasses[0], new enum_medPharmacy());         
            }

            if (TableName == "medicine_page")
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "本地端");
                returnData.Method = "get_init";

                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }

                table = MethodClass.CheckCreatTable(serverSettingClasses[0], new enum_藥品資料_藥檔資料());            
            }


            return table.JsonSerializationt(true);
        }
        static public medClass Get_med_cloud(ServerSettingClass serverSettingClass, string code)
        {
            List<medClass> medClasses = Get_med_cloud(serverSettingClass, new string[] { code });
            if (medClasses.Count == 0) return null;
            return medClasses[0];
        }
        static public List<medClass> Get_med_cloud(ServerSettingClass serverSettingClass , string[] Codes)
        {
            try
            {
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();
                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_cloud", UserName, Password, Port, SSLMode);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);
                List<object[]> list_med_buf = new List<object[]>();
                List<object[]> list_med_temp = new List<object[]>();
                for (int i = 0; i < Codes.Length; i++)
                {
                    list_med_temp = list_med.GetRows((int)enum_雲端藥檔.藥品碼, Codes[i]);
                    if(list_med_temp.Count > 0)
                    {
                        list_med_buf.Add(list_med_temp[0]);
                    }
                }
                List<medClass> medClasses = list_med_buf.SQLToClass<medClass, enum_雲端藥檔>();
                return medClasses;
            }
            catch
            {
                return null;
            }
        }
        private List<medClass> Get_med_cloud(ServerSettingClass serverSettingClass)
        {
            try
            {
                string Server = serverSettingClass.Server;
                string DB = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();
                SQLControl sQLControl_med = new SQLControl(Server, DB, "medicine_page_cloud", UserName, Password, Port, SSLMode);
                List<object[]> list_med = sQLControl_med.GetAllRows(null);
                List<medClass> medClasses = list_med.SQLToClass<medClass, enum_雲端藥檔>();
                return medClasses;
            }
            catch
            {
                return null;
            }  
        }
      
        public class Icp_藥品資料_藥檔資料 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 藥品碼_0 = x[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                string 藥品碼_1 = y[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                return 藥品碼_0.CompareTo(藥品碼_1);
            }
        }
    }
}
