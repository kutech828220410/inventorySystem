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
    public class deviceController : Controller
    {
        public enum enum_儲位資訊
        {
            IP,
            TYPE,
            效期,
            批號,
            庫存,
            異動量,
            Value,
        }

        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";

        static private string device_Server = ConfigurationManager.AppSettings["device_Server"];
        static private string device_DB = ConfigurationManager.AppSettings["device_DB"];
        static private string device_TableName = ConfigurationManager.AppSettings["device_TableName"];

        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("test")]
        [HttpGet]
        public string GET_test(string Code)
        {
            if (Code.StringIsEmpty()) return null;
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<DeviceBasic> deviceBasics = Function_Get_device(device_Server, device_DB, device_TableName, UserName, Password, Port);
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics_buf = deviceBasics.SortByCode($"{Code}");
            for (int i = 0; i < deviceBasics_buf.Count; i++)
            {
                DeviceType deviceType = deviceBasics_buf[i].DeviceType;
                if (deviceType == DeviceType.EPD290 || deviceType == DeviceType.EPD290_lock)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics_buf[i].IP, 0, Get_EPD290_LEDBytes(Color.Red));
                }
                if (deviceType == DeviceType.EPD266 || deviceType == DeviceType.EPD266_lock)
                {

                    H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics_buf[i].IP, 0, Get_EPD266_LEDBytes(Color.Red));
                }
                if (deviceType == DeviceType.RowsLED)
                {
                    SQLControl sQLControl_RowsLED_serialize = new SQLControl(device_Server, device_DB, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
                    RowsLED rowsLED = RowsLEDMethod.SQL_GetDevice(sQLControl_RowsLED_serialize, deviceBasics_buf[i].IP);
                    if (rowsLED == null) continue;
                    RowsDevice rowsDevice = rowsLED.SortByGUID(deviceBasics_buf[i].GUID);
                    if (rowsDevice == null) continue;
                    int maxLED = rowsLED.Maximum;
                    byte[] LED_Bytes = new byte[maxLED];
                    Get_Rows_LEDBytes(ref LED_Bytes, rowsDevice.StartLED, rowsDevice.EndLED, Color.Red);
                    H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics_buf[i].IP, 0, LED_Bytes);

                }
                if (deviceType == DeviceType.EPD583 || deviceType == DeviceType.EPD583_lock)
                {
                    SQLControl sQLControl_EPD583_serialize = new SQLControl(device_Server, device_DB, "epd583_jsonstring", UserName, Password, Port, SSLMode);
                    Drawer drawer = DrawerMethod.SQL_GetDevice(sQLControl_EPD583_serialize, deviceBasics_buf[i].IP);
                    if (drawer == null) continue;
                    Box box = drawer.GetBox(deviceBasics_buf[i].GUID);
                    if (box == null) continue;
                    WS2812_Init();
                    Set_LED_UDP(Startup.uDP_Class, drawer, box, Color.Red);

                }
            }
            returnData returnData = new returnData();
            returnData.Code = 200;
            returnData.Result = $"Device取得成功!TableName : {device_TableName}";
            returnData.TimeTaken = myTimer.ToString();
            returnData.Data = deviceBasics_buf;
            return returnData.JsonSerializationt(true);
        }

        [Route("list/{value}")]
        [HttpGet]
        public string GET_list(string value)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            returnData.Method = "GET_list";
            try
            {

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses.MyFind(value, enum_sys_serverSetting_Type.調劑台, enum_sys_serverSetting_調劑台.儲位資料);
                if(sys_serverSettingClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無伺服器資料";
                    return returnData.JsonSerializationt(true);
                }
                List<DeviceBasic> deviceBasics = Function_Get_device(sys_serverSettingClass);
                deviceBasics = (from temp in deviceBasics
                                    where temp.Code.StringIsEmpty() == false
                                    select temp).ToList();
                returnData.Data = deviceBasics;
                returnData.TimeTaken = myTimer.ToString();
                returnData.Code = 200;
                returnData.Result = $"取得儲位資訊成功";

                return returnData.JsonSerializationt(true);

            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                return returnData.JsonSerializationt(true);
            }
        }

        [Route("all")]
        [HttpGet]
        public string GET_all()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<DeviceBasic> deviceBasics = Function_Get_device(device_Server, device_DB, device_TableName, UserName, Password, Port);
            returnData.Code = 200;
            returnData.Result = $"Device取得成功!TableName : {device_TableName}";
            returnData.TimeTaken = myTimer.ToString();
            returnData.Data = deviceBasics;
            return returnData.JsonSerializationt(true);
        }


        /// <summary>
        /// 查詢儲位資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "Data": 
        ///     {
        ///     
        ///     }
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("all")]
        [HttpPost]
        public string POST_all(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "all";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }

                List<DeviceBasic> deviceBasics = Function_Get_device(sys_serverSettingClasses[0], returnData.TableName);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"Device取得成功!,共<{deviceBasics.Count}>筆資料,TableName : {returnData.TableName}";
                returnData.Data = deviceBasics;
    
                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }
        /// <summary>
        /// 以Code查詢庫儲系統儲位資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "ValueAry" : 
        ///     [
        ///       "藥碼1,藥碼2,藥碼3"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("get_form_storehouse_by_codes")]
        [HttpPost]
        public string POST_get_form_storehouse_by_codes(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

                returnData.Method = "get_form_storehouse_by_codes";
            }
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
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

                string[] Codes = returnData.ValueAry[0].Split(",");
                List<DeviceBasic> deviceBasics = Function_Get_device(sys_serverSettingClasses[0], returnData.TableName);
                Dictionary<string, List<DeviceBasic>> keyValuePairs = deviceBasics.CoverToDictionaryByCode();


                List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
                List<DeviceBasic> deviceBasics_result = new List<DeviceBasic>();

                for (int i = 0; i < Codes.Length; i++)
                {
                    deviceBasics_buf = keyValuePairs.SortDictionaryByCode(Codes[i]);
                    if (deviceBasics_buf.Count > 0)
                    {
                        deviceBasics_result.LockAdd(deviceBasics_buf);
                    }
                }


                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"Device取得成功!,共<{deviceBasics_result.Count}>筆資料,TableName : {returnData.TableName}";
                returnData.Data = deviceBasics_result;

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }
        /// <summary>
        /// 以Code查詢藥局系統儲位資料
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "口服2",
        ///     "ServerType" : "調劑台",
        ///     "ValueAry" : 
        ///     [
        ///       "藥碼1"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("get_from_pharma_by_code")]
        [HttpPost]
        public string POST_get_from_pharma_by_code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            try
            {
                returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            }
            catch
            {

            }
         
            returnData.Method = "get_from_pharma_by_code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                if(returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼1]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                List<DeviceBasic> deviceBasics = Function_讀取儲位_By_Code(Server, DB, UserName, Password, Port, Code);
              
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"Device取得成功!,共<{deviceBasics.Count}>筆資料,TableName : {returnData.TableName}";
                returnData.Data = deviceBasics;

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }
        /// <summary>
        /// 設定儲位資料(DeviceBasic)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ds01",
        ///     "ServerType" : "藥庫",
        ///     "Data" : 
        ///     {
        ///       [deviceBasic陣列]
        ///     }
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("update_deviceBasic")]
        [HttpPost]
        public string POST_update_deviceBasic(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "update_deviceBasic";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                List<DeviceBasic> deviceBasics = returnData.Data.ObjToClass<List<DeviceBasic>>();

                if (deviceBasics == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"Data資料錯誤";
                    return returnData.JsonSerializationt();
                }

                Function_Set_device(sys_serverSettingClasses[0], returnData.TableName, deviceBasics);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"Device取得成功!,共<{deviceBasics.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }
        /// <summary>
        /// 取得儲位資料(Device)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "ServerName",
        ///     "ServerType" : "ServerType",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_device")]
        [HttpPost]
        public string POST_get_device(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_device";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string tableName = returnData.ValueAry[0];

                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<Device> devices = DeviceMethod.SQL_GetAllDevice(sQLControl_device);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = devices;
                returnData.Result = $"Device取得成功!,共<{devices.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 取得儲位資料(RowLED)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "中藥台",
        ///     "ServerType": "中藥調劑系統",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_rowLEDs")]
        [HttpPost]
        public string POST_get_rowLEDs(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_rowLEDs";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string tableName = "RowsLED_Jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<RowsLED> rowsLEDs = RowsLEDMethod.SQL_GetAllRowsLED(sQLControl_device);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = rowsLEDs;
                returnData.Result = $"rowsLEDs 取得成功!,共<{rowsLEDs.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(RowLED)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "中藥台",
        ///     "ServerType": "中藥調劑系統",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///        "藥碼"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_rowLED_By_Code")]
        [HttpPost]
        public string POST_get_rowLED_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_rowLED_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "RowsLED_Jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<RowsLED> rowsLEDs = RowsLEDMethod.SQL_GetRowsLEDByCode(sQLControl_device, Code);

                if (rowsLEDs == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = rowsLEDs;
                returnData.Result = $"取得成功共<{rowsLEDs.Count}>筆資料!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(RowLED.DeviceBasics)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "中藥台",
        ///     "ServerType": "中藥調劑系統",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///        "藥碼"
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_rowLED_DeviceBasics_By_Code")]
        [HttpPost]
        public string POST_get_rowLED_DeviceBasics_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_rowLED_DeviceBasics_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "RowsLED_Jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<DeviceBasic> deviceBasics = RowsLEDMethod.GetDeviceBasicByCode(sQLControl_device, Code);

                if (deviceBasics == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = deviceBasics;
                returnData.Result = $"取得成功共<{deviceBasics.Count}>筆資料!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以IP取得儲位資料(RowLED)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "中藥台",
        ///     "ServerType": "中藥調劑系統",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///        "IP",
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_rowLED_ByIP")]
        [HttpPost]
        public string POST_get_rowLED_ByIP(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_rowLED_ByIP";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[IP]";
                    return returnData.JsonSerializationt(true);
                }
                string IP = returnData.ValueAry[0];
                string tableName = "RowsLED_Jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<RowsLED> rowsLEDs = RowsLEDMethod.SQL_GetAllRowsLED(sQLControl_device);

                RowsLED rowsLED = rowsLEDs.SortByIP(IP);
                if (rowsLED == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = rowsLED;
                returnData.Result = $"jsonStr取得成功!,TableName : {returnData.TableName} , IP : {IP}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 更新儲位資料(rowsLED)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "中藥台",
        ///     "ServerType": "中藥調劑系統",
        ///     "Data" : 
        ///     {
        ///       [rowsLED陣列]
        ///     }
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("update_rowsLEDs")]
        [HttpPost]
        public string POST_update_rowsLED(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "update_rowsLED";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string tableName = "RowsLED_Jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                List<RowsLED> rowsLEDs = returnData.Data.ObjToClass<List<RowsLED>>();
                if (rowsLEDs.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data無資料";
                    return returnData.JsonSerializationt();
                }
                if (rowsLEDs == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料錯誤";
                    return returnData.JsonSerializationt();
                }
                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_device.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_replace = new List<object[]>();
                for (int i = 0; i < rowsLEDs.Count; i++)
                {
                    string IP = rowsLEDs[i].IP;
                    list_value_buf = list_value.GetRows((int)enum_DeviceTable.IP, IP);
                    if(list_value_buf.Count > 0)
                    {
                        list_value_buf[0][(int)enum_DeviceTable.Value] = rowsLEDs[i].JsonSerializationt();
                        list_replace.Add(list_value_buf[0]);
                    }
                }

                sQLControl_device.UpdateByDefulteExtra(null, list_replace);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"更新rowsLED成功!,共<{list_replace.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }

        /// <summary>
        /// 取得儲位資料(Drawer)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd583_Drawers")]
        [HttpPost]
        public string POST_get_epd583_Drawers(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_Drawers";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string tableName = "epd583_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<Drawer> drawers = DrawerMethod.SQL_GetAllDrawers(sQLControl_device);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = drawers;
                returnData.Result = $"drawers 取得成功!,共<{drawers.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(Drawer)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       [藥碼]
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd583_Drawer_By_Code")]
        [HttpPost]
        public string POST_get_epd583_Drawer_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "epd583_Drawer_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "epd583_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<Drawer> drawers = DrawerMethod.SQL_GetDrawersByCode(sQLControl_device, Code);
                if (drawers.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = drawers;
                returnData.Result = $"取得成功共<{drawers.Count}>筆資料!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(Drawer.DeviceBasics)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       [藥碼]
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd583_DeviceBasics_By_Code")]
        [HttpPost]
        public string POST_get_epd583_DeviceBasics_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_epd583_DeviceBasics_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "epd583_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<DeviceBasic> deviceBasics = DrawerMethod.GetDeviceBasicByCode(sQLControl_device, Code);
                if (deviceBasics.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = deviceBasics;
                returnData.Result = $"取得成功共<{deviceBasics.Count}>筆資料!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以IP取得儲位資料(Drawer)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd583_Drawer_ByIP")]
        [HttpPost]
        public string POST_get_epd583_Drawer_ByIP(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_Drawer_ByIP";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[IP]";
                    return returnData.JsonSerializationt(true);
                }
                string IP = returnData.ValueAry[0];
                string tableName = "epd583_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                Drawer drawer = DrawerMethod.SQL_GetDrawerByIP(sQLControl_device , IP);
                if (drawer == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = drawer;
                returnData.Result = $"jsonStr取得成功!,TableName : {returnData.TableName} , IP : {IP}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 更新儲位資料(Drawers)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "Data" : 
        ///     {
        ///       [rowsLED陣列]
        ///     }
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("update_epd583_Drawers")]
        [HttpPost]
        public string POST_update_epd583_Drawers(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "update_Drawers";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string tableName = "epd583_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                List<Drawer> drawers = returnData.Data.ObjToClass<List<Drawer>>();
                if (drawers.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data無資料";
                    return returnData.JsonSerializationt();
                }
                if (drawers == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料錯誤";
                    return returnData.JsonSerializationt();
                }
                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_device.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_replace = new List<object[]>();
                for (int i = 0; i < drawers.Count; i++)
                {
                    string IP = drawers[i].IP;
                    list_value_buf = list_value.GetRows((int)enum_DeviceTable.IP, IP);
                    if (list_value_buf.Count > 0)
                    {
                        list_value_buf[0][(int)enum_DeviceTable.Value] = drawers[i].JsonSerializationt();
                        list_replace.Add(list_value_buf[0]);
                    }
                }

                sQLControl_device.UpdateByDefulteExtra(null, list_replace);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"更新Drawers成功!,共<{list_replace.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }
        /// <summary>
        /// 取得儲位資料(EPD266)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd266_storage")]
        [HttpPost]
        public string POST_get_epd266_storage(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_epd266_storage";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string tableName = "epd266_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<Storage> storages = StorageMethod.SQL_GetAllStorage(sQLControl_device);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = storages;
                returnData.Result = $"storages 取得成功!,共<{storages.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(EPD266)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///        [藥碼]
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd266_storage_By_Code")]
        [HttpPost]
        public string POST_get_epd266_storage_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_epd266_storage_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "epd266_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<Storage> storages = StorageMethod.SQL_GetStorageByCode(sQLControl_device, Code);
                if (storages == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = storages;
                returnData.Result = $"已取得資料共<{storages.Count}>筆!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(EPD266.DeviceBasics)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///        [藥碼]
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd266_DeviceBasics_By_Code")]
        [HttpPost]
        public string POST_get_epd266_DeviceBasics_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_epd266_storage_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "epd266_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<DeviceBasic> deviceBasics = StorageMethod.GetDeviceBasicByCode(sQLControl_device, Code);
                if (deviceBasics == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = deviceBasics;
                returnData.Result = $"已取得資料共<{deviceBasics.Count}>筆!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以IP取得儲位資料(EPD266)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_epd266_storage_ByIP")]
        [HttpPost]
        public string POST_get_epd266_storage_ByIP(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_epd266_storage_ByIP";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[IP]";
                    return returnData.JsonSerializationt(true);
                }
                string IP = returnData.ValueAry[0];
                string tableName = "epd266_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                Storage storage = StorageMethod.SQL_GetStorageByIP(sQLControl_device, IP);
                if (storage == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = storage;
                returnData.Result = $"jsonStr取得成功!,TableName : {returnData.TableName} , IP : {IP}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 更新儲位資料(EPD266)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "Data" : 
        ///     {
        ///       [rowsLED陣列]
        ///     }
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("update_epd266_storages")]
        [HttpPost]
        public string POST_update_epd266_storages(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "update_epd266_storages";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string tableName = "epd266_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                List<Storage> storages = returnData.Data.ObjToClass<List<Storage>>();
                if (storages.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data無資料";
                    return returnData.JsonSerializationt();
                }
                if (storages == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料錯誤";
                    return returnData.JsonSerializationt();
                }
                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_device.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_replace = new List<object[]>();
                for (int i = 0; i < storages.Count; i++)
                {
                    string IP = storages[i].IP;
                    list_value_buf = list_value.GetRows((int)enum_DeviceTable.IP, IP);
                    if (list_value_buf.Count > 0)
                    {
                        list_value_buf[0][(int)enum_DeviceTable.Value] = storages[i].JsonSerializationt();
                        list_replace.Add(list_value_buf[0]);
                    }
                }

                sQLControl_device.UpdateByDefulteExtra(null, list_replace);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"更新storages成功!,共<{list_replace.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }
        /// <summary>
        /// 取得儲位資料(Panel35)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_panel35_storage")]
        [HttpPost]
        public string POST_get_panel35_storage(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_panel35_storage";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string tableName = "wt32_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<Storage> storages = StorageMethod.SQL_GetAllStorage(sQLControl_device);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = storages;
                returnData.Result = $"storages 取得成功!,共<{storages.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(Panel35)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///        [藥碼]
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_Panel35_storage_By_Code")]
        [HttpPost]
        public string POST_get_Panel35_storage_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_Panel35_storage_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "Panel35_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<Storage> storages = StorageMethod.SQL_GetStorageByCode(sQLControl_device, Code);
                if (storages == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = storages;
                returnData.Result = $"已取得資料共<{storages.Count}>筆!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以藥碼取得儲位資料(Panel35.DeviceBasics)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///        [藥碼]
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_Panel35_DeviceBasics_By_Code")]
        [HttpPost]
        public string POST_get_Panel35_DeviceBasics_By_Code(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_Panel35_storage_By_Code";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼]";
                    return returnData.JsonSerializationt(true);
                }
                string Code = returnData.ValueAry[0];
                string tableName = "Panel35_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<DeviceBasic> deviceBasics = StorageMethod.GetDeviceBasicByCode(sQLControl_device, Code);
                if (deviceBasics == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = deviceBasics;
                returnData.Result = $"已取得資料共<{deviceBasics.Count}>筆!,TableName : {returnData.TableName} , Code : {Code}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 以IP取得儲位資料(Panel35)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "TableName" : "",
        ///     "ValueAry" : 
        ///     [
        ///       
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[Device]陣列結構</returns>
        [Route("get_panel35_storage_ByIP")]
        [HttpPost]
        public string POST_get_panel35_storage_ByIP(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "get_panel35_storage_ByIP";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[IP]";
                    return returnData.JsonSerializationt(true);
                }
                string IP = returnData.ValueAry[0];
                string tableName = "wt32_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                Storage storage = StorageMethod.SQL_GetStorageByIP(sQLControl_device, IP);
                if (storage == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
                }
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Data = storage;
                returnData.Result = $"jsonStr取得成功!,TableName : {returnData.TableName} , IP : {IP}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }
        /// <summary>
        /// 更新儲位資料(Panel35)
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "口服2",
        ///     "ServerType": "調劑台",
        ///     "Data" : 
        ///     {
        ///       [rowsLED陣列]
        ///     }
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]為[DeviceBasic]陣列結構</returns>
        [Route("update_panel35_storages")]
        [HttpPost]
        public string POST_update_panel35_storages(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            //returnData.RequestUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            returnData.Method = "update_panel35_storages";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");

                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string tableName = "wt32_jsonstring";
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();

                List<Storage> storages = returnData.Data.ObjToClass<List<Storage>>();
                if (storages.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data無資料";
                    return returnData.JsonSerializationt();
                }
                if (storages == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料錯誤";
                    return returnData.JsonSerializationt();
                }
                SQLControl sQLControl_device = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_device.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_replace = new List<object[]>();
                for (int i = 0; i < storages.Count; i++)
                {
                    string IP = storages[i].IP;
                    list_value_buf = list_value.GetRows((int)enum_DeviceTable.IP, IP);
                    if (list_value_buf.Count > 0)
                    {
                        list_value_buf[0][(int)enum_DeviceTable.Value] = storages[i].JsonSerializationt();
                        list_replace.Add(list_value_buf[0]);
                    }
                }

                sQLControl_device.UpdateByDefulteExtra(null, list_replace);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Code = 200;
                returnData.Result = $"更新storages成功!,共<{list_replace.Count}>筆資料,TableName : {returnData.TableName}";

                string json_out = returnData.JsonSerializationt();

                return json_out;
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
        }

        /// <summary>
        /// 以藥碼進行亮燈
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName": "ds01",
        ///     "ServerType": "藥庫",
        ///     "ValueAry" : 
        ///     [
        ///       [藥碼1,藥碼2....],
        ///       [顏色],
        ///       [亮燈時間(s)]
        ///     ]
        ///     
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("light_on_by_code")]
        [HttpPost]
        public string POST_light_on_by_code(returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "light_on_by_code";
            try
            {
                List<object[]> list_add = new List<object[]>();
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> sys_serverSettingClasses_buf = new List<sys_serverSettingClass>();
                sys_serverSettingClass sys_serverSettingClass_儲位資料 = null;
                sys_serverSettingClass sys_serverSettingClass_堆疊資料 = null;
                sys_serverSettingClasses_buf = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");
                if (sys_serverSettingClasses_buf.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass_儲位資料 = sys_serverSettingClasses_buf[0];

                sys_serverSettingClasses_buf = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (sys_serverSettingClasses_buf.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                sys_serverSettingClass_堆疊資料 = sys_serverSettingClasses_buf[0];

                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[藥碼1,藥碼2....][顏色][亮燈時間(s)]";
                    return returnData.JsonSerializationt(true);
                }
                
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                List<Task> tasks = new List<Task>();
                string str_codes = returnData.ValueAry[0];
                string str_color = returnData.ValueAry[1];
                string str_time = returnData.ValueAry[2];
                if (str_time.StringIsDouble() == false)
                {
                    returnData.Code = -200;
                    returnData.Result = $"亮燈時間(s) 輸入內容錯誤";
                    return returnData.JsonSerializationt(true);
                }
                string Server = sys_serverSettingClass_儲位資料.Server;
                string DB = sys_serverSettingClass_儲位資料.DBName;
                string UserName = sys_serverSettingClass_儲位資料.User;
                string Password = sys_serverSettingClass_儲位資料.Password;
                uint Port = (uint)sys_serverSettingClass_儲位資料.Port.StringToInt32();

                string[] codes = str_codes.Split(",");

                if (codes.Length == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料無可用藥碼";
                    return returnData.JsonSerializationt(true);
                }
           

                Color color = str_color.ToColor();
                for (int i = 0; i < codes.Length; i++)
                {
                    object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                    value[(int)enum_取藥堆疊母資料.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_取藥堆疊母資料.顏色] = color.ToColorString();
                    value[(int)enum_取藥堆疊母資料.藥品碼] = codes[i];
                    value[(int)enum_取藥堆疊母資料.調劑台名稱] = "儲位亮燈";
                    value[(int)enum_取藥堆疊母資料.狀態] = "None";
                    value[(int)enum_取藥堆疊母資料.總異動量] = str_time;
                    value[(int)enum_取藥堆疊母資料.操作時間] = DateTime.Now.ToDateTimeString();
                    list_add.Add(value);
                }
                SQLControl sQLControl_take_medicine_stack_new = new SQLControl(sys_serverSettingClass_堆疊資料.Server, sys_serverSettingClass_堆疊資料.DBName, "take_medicine_stack_new", 
                    sys_serverSettingClass_堆疊資料.User, sys_serverSettingClass_堆疊資料.Password, sys_serverSettingClass_堆疊資料.Port.StringToUInt32(), SSLMode);

                sQLControl_take_medicine_stack_new.AddRows(null, list_add);

                returnData.Code = 200;
                returnData.Result = $"設備更新亮燈完成,共<{codes.Length}>筆";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();

            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                returnData.TimeTaken = myTimer.ToString();
                returnData.Data = null;
                return returnData.JsonSerializationt(true);
            }
            finally
            {

            }
        }

        [Route("light_web")]
        [HttpPost]
        public string POST_light_web(returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "light_web";
            try
            {
                List<object[]> list_add = new List<object[]>();
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string device_Server = sys_serverSettingClasses[0].Server;
                string device_DB = sys_serverSettingClasses[0].DBName;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Color color = returnData.Value.ToColor();
                string json_in = returnData.Data.JsonSerializationt();
                List<DeviceBasic> deviceBasics = json_in.JsonDeserializet<List<DeviceBasic>>();
                for (int i = 0; i < deviceBasics.Count; i++)
                {
                    string IP = deviceBasics[i].IP;
                    object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                    value[(int)enum_取藥堆疊母資料.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_取藥堆疊母資料.顏色] = color.ToColorString();
                    value[(int)enum_取藥堆疊母資料.IP] = IP;
                    value[(int)enum_取藥堆疊母資料.藥品碼] = deviceBasics[i].Code;
                    value[(int)enum_取藥堆疊母資料.調劑台名稱] = "更新亮燈";
                    list_add.Add(value);
                }
                SQLControl sQLControl_take_medicine_stack_new = new SQLControl(device_Server, device_DB, "take_medicine_stack_new", UserName, Password, Port, SSLMode);
                sQLControl_take_medicine_stack_new.AddRows(null, list_add);

                returnData.Code = 200;
                returnData.Result = $"設備更新亮燈完成!";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();

            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                returnData.TimeTaken = myTimer.ToString();
                returnData.Data = null;
                return returnData.JsonSerializationt(true);
            }
            finally
            {

            }
        }
        [Route("light")]
        [HttpPost]
        public string POST_light(returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "light";
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string device_Server = sys_serverSettingClasses[0].Server;
                string device_DB = sys_serverSettingClasses[0].DBName;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Color color = returnData.Value.ToColor();
                string json_in = returnData.Data.JsonSerializationt();
                List<DeviceBasic> deviceBasics = json_in.JsonDeserializet<List<DeviceBasic>>();
                for (int i = 0; i < deviceBasics.Count; i++)
                {
                    DeviceType deviceType = deviceBasics[i].DeviceType;
                    if (deviceType == DeviceType.EPD290 || deviceType == DeviceType.EPD290_lock)
                    {
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, Get_EPD290_LEDBytes(color));
                    }
                    if (deviceType == DeviceType.EPD266 || deviceType == DeviceType.EPD266_lock)
                    {
                      
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, Get_EPD266_LEDBytes(color));
                    }
                    if (deviceType == DeviceType.RowsLED)
                    {
                        SQLControl sQLControl_RowsLED_serialize = new SQLControl(device_Server, device_DB, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
                        RowsLED rowsLED = RowsLEDMethod.SQL_GetDevice(sQLControl_RowsLED_serialize, deviceBasics[i].IP);
                        if (rowsLED == null) continue;
                        RowsDevice rowsDevice = rowsLED.SortByGUID(deviceBasics[i].GUID);
                        if (rowsDevice == null) continue;
                        int maxLED = rowsLED.Maximum;
                        byte[] LED_Bytes = new byte[maxLED];
                        Get_Rows_LEDBytes(ref LED_Bytes, rowsDevice.StartLED, rowsDevice.EndLED, color);
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, LED_Bytes);

                    }
                    if (deviceType == DeviceType.EPD583 || deviceType == DeviceType.EPD583_lock)
                    {
                        SQLControl sQLControl_EPD583_serialize = new SQLControl(device_Server, device_DB, "epd583_jsonstring", UserName, Password, Port, SSLMode);
                        Drawer drawer = DrawerMethod.SQL_GetDevice(sQLControl_EPD583_serialize, deviceBasics[i].IP);
                        if (drawer == null) continue;
                        Box box = drawer.GetBox(deviceBasics[i].GUID);
                        if (box == null) continue;
                        WS2812_Init();
                        Set_LED_UDP(Startup.uDP_Class, drawer, box, color);

                    }
                }
                returnData.Code = 200;
                returnData.Result = $"設備亮燈完成!";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();

            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }

        [Route("refresh_canvas")]
        [HttpPost]
        public string POST_refresh_canvas(returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "refresh_canvas";
            try
            {
                List<object[]> list_add = new List<object[]>();
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string device_Server = sys_serverSettingClasses[0].Server;
                string device_DB = sys_serverSettingClasses[0].DBName;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Color color = returnData.Value.ToColor();
                string json_in = returnData.Data.JsonSerializationt();
                List<DeviceBasic> deviceBasics = json_in.JsonDeserializet<List<DeviceBasic>>();
                for (int i = 0; i < deviceBasics.Count; i++)
                {
                    string IP = deviceBasics[i].IP;
                    object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                    value[(int)enum_取藥堆疊母資料.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_取藥堆疊母資料.IP] = IP;
                    value[(int)enum_取藥堆疊母資料.藥品碼] = deviceBasics[i].Code;
                    value[(int)enum_取藥堆疊母資料.調劑台名稱] = "更新面板";
                    list_add.Add(value);
                }
                SQLControl sQLControl_take_medicine_stack_new = new SQLControl(device_Server, device_DB, "take_medicine_stack_new", UserName, Password, Port, SSLMode);
                sQLControl_take_medicine_stack_new.AddRows(null, list_add);

                returnData.Code = 200;
                returnData.Result = $"設備刷新上傳完成!";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();

            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                returnData.TimeTaken = myTimer.ToString();
                returnData.Data = null;
                return returnData.JsonSerializationt(true);
            }
            finally
            {

            }
        }

        [Route("sort_by_ip")]
        [HttpPost]
        public string POST_sort_by_ip(returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "sort_by_ip";
            try
            {
                List<object[]> list_add = new List<object[]>();
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "儲位資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Storage_IP = "";
                Storage_IP = $"192.168.{returnData.Value}";
                if($"192.168.{returnData.Value}".Check_IP_Adress() == true)
                {
                    Storage_IP = $"192.168.{returnData.Value}";
                }
                if ($"{returnData.Value}".Check_IP_Adress() == true)
                {
                    Storage_IP = $"{returnData.Value}";
                }
                if(Storage_IP.StringIsEmpty() == true)
                {
                    returnData.Code = -200;
                    returnData.Result = $"IP 值無效! {returnData.Value}";
                    return returnData.JsonSerializationt();
                }
             

                string device_Server = sys_serverSettingClasses[0].Server;
                string device_DB = sys_serverSettingClasses[0].DBName;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Color color = returnData.Value.ToColor();
                string json_in = returnData.Data.JsonSerializationt();
                List<DeviceBasic> deviceBasics = Function_Get_device_by_ip(sys_serverSettingClasses[0], Storage_IP);
                returnData.Data = deviceBasics;

                returnData.Code = 200;
                returnData.Result = $"搜尋儲位IP完成,共<{deviceBasics.Count}>筆!";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();

            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                returnData.TimeTaken = myTimer.ToString();
                returnData.Data = null;
                return returnData.JsonSerializationt(true);
            }
            finally
            {

            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<DeviceBasic> Function_Get_device()
        {
            return Function_Get_device(device_Server, device_DB, device_TableName, UserName, Password, Port);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<DeviceBasic> Function_Get_device(returnData returnData)
        {
            return Function_Get_device(returnData.Server, returnData.DbName, returnData.TableName, returnData.UserName, returnData.Password, returnData.Port);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<DeviceBasic> Function_Get_device(sys_serverSettingClass sys_serverSettingClass)
        {
            return Function_Get_device(sys_serverSettingClass, "");
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<DeviceBasic> Function_Get_device(sys_serverSettingClass sys_serverSettingClass , string TableName)
        {
            string Server = sys_serverSettingClass.Server;
            string DBName = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            if (sys_serverSettingClass.類別 == "藥庫" && (TableName == "firstclass_device_jsonstring" || TableName == "sd0_device_jsonstring"))
            {
                SQLControl sQLControl_device = new SQLControl(Server, DBName, TableName, UserName, Password, Port, SSLMode);
                deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);

            }
            else
            {
                deviceBasics = Function_讀取儲位(Server, DBName, UserName, Password, Port);
            }
       
            return deviceBasics;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<DeviceSimple> Function_Get_deviceSimple(sys_serverSettingClass sys_serverSettingClass, string TableName)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string Server = sys_serverSettingClass.Server;
            string DBName = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
            List<DeviceSimple> deviceBasics = new List<DeviceSimple>();
            if (sys_serverSettingClass.類別 == "藥庫" && (TableName == "firstclass_device_jsonstring" || TableName == "sd0_device_jsonstring"))
            {
                SQLControl sQLControl_device = new SQLControl(Server, DBName, TableName, UserName, Password, Port, SSLMode);
                deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceSimple(sQLControl_device);
                Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 類別 : {sys_serverSettingClass.類別} , TableName {TableName} , {myTimerBasic.ToString()}");
            }
            return deviceBasics;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        static public void Function_Set_device(sys_serverSettingClass sys_serverSettingClass, string TableName , List<DeviceBasic> deviceBasics)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            string Server = sys_serverSettingClass.Server;
            string DBName = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
            if (sys_serverSettingClass.類別 == "藥庫" && (TableName == "firstclass_device_jsonstring" || TableName == "sd0_device_jsonstring"))
            {
                SQLControl sQLControl_device = new SQLControl(Server, DBName, TableName, UserName, Password, Port, SSLMode);
                DeviceBasicMethod.SQL_ReplaceDeviceBasic(sQLControl_device , deviceBasics);
                Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] 類別 : {sys_serverSettingClass.類別} , TableName {TableName} , {myTimerBasic.ToString()}");
            }
        }
        static private List<DeviceBasic> Function_Get_device(string IP, string DBName, string TableName, string UserName, string Password, uint Port)
        {
            SQLControl sQLControl_device = new SQLControl(IP, DBName, TableName, UserName, Password, Port, SSLMode);
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            if (TableName.StringIsEmpty() == false)
            {
                deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);
            }
            else
            {
                deviceBasics = Function_讀取儲位(IP, DBName, UserName, Password, Port);
            }

            return deviceBasics;
        }
        static private List<DeviceBasic> Function_Get_device_by_ip(sys_serverSettingClass sys_serverSettingClass, string storageIP)
        {
            string IP = sys_serverSettingClass.Server;
            string DBName = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            List<DeviceBasic> deviceBasics = Function_讀取儲位_by_ip(IP, DBName, UserName, Password, Port, storageIP);

            return deviceBasics;
        }

        static private List<DeviceBasic> Function_讀取儲位_by_ip(string IP, string DBName, string UserName, string Password, uint Port, string storageIP)
        {
            SQLControl sQLControl_EPD583_serialize = new SQLControl(IP, DBName, "epd583_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DBName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DBName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(IP, DBName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_WT32_serialize = new SQLControl(IP, DBName, "WT32_Jsonstring", UserName, Password, Port, SSLMode);


            List<object[]> list_EPD583 = new List<object[]>();
            List<object[]> list_EPD266 = new List<object[]>();
            List<object[]> list_RowsLED = new List<object[]>();
            List<object[]> list_RFID_Device = new List<object[]>();
            List<object[]> list_WT32 = new List<object[]>();
            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() =>
            {
              
                if (sQLControl_EPD583_serialize.IsTableCreat()) list_EPD583 = sQLControl_EPD583_serialize.GetRowsByDefult(null,(int)H_Pannel_lib.enum_DeviceTable.IP, storageIP);
            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_EPD266_serialize.IsTableCreat()) list_EPD266 = sQLControl_EPD266_serialize.GetRowsByDefult(null, (int)H_Pannel_lib.enum_DeviceTable.IP, storageIP);
            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_RowsLED_serialize.IsTableCreat()) list_RowsLED = sQLControl_RowsLED_serialize.GetRowsByDefult(null, (int)H_Pannel_lib.enum_DeviceTable.IP, storageIP);
            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_RFID_Device_serialize.IsTableCreat()) list_RFID_Device = sQLControl_RFID_Device_serialize.GetRowsByDefult(null, (int)H_Pannel_lib.enum_DeviceTable.IP, storageIP);
            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_WT32_serialize.IsTableCreat()) list_WT32 = sQLControl_WT32_serialize.GetRowsByDefult(null, (int)H_Pannel_lib.enum_DeviceTable.IP, storageIP);
            }));
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics.LockAdd(DrawerMethod.GetAllDeviceBasic(list_EPD583));
            deviceBasics.LockAdd(StorageMethod.GetAllDeviceBasic(list_EPD266));
            deviceBasics.LockAdd(RowsLEDMethod.GetAllDeviceBasic(list_RowsLED));
            deviceBasics.LockAdd(RFIDMethod.GetAllDeviceBasic(list_RFID_Device));
            deviceBasics.LockAdd(StorageMethod.GetAllDeviceBasic(list_WT32));
            deviceBasics_buf = deviceBasics;
            //deviceBasics_buf = (from value in deviceBasics
            //                    where value.Code.StringIsEmpty() == false
            //                    select value).ToList();

            return deviceBasics_buf;
        }
        static private List<DeviceBasic> Function_讀取儲位(string IP, string DBName, string UserName, string Password, uint Port)
        {
    
            SQLControl sQLControl_EPD583_serialize = new SQLControl(IP, DBName, "epd583_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DBName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DBName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(IP, DBName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_WT32_serialize = new SQLControl(IP, DBName, "WT32_Jsonstring", UserName, Password, Port, SSLMode);

            
            List<object[]> list_EPD583 = new List<object[]>();
            List<object[]> list_EPD266 = new List<object[]>();
            List<object[]> list_RowsLED = new List<object[]>();
            List<object[]> list_RFID_Device = new List<object[]>();
            List<object[]> list_WT32 = new List<object[]>();

            List<DeviceBasic> deviceBasics_EPD583 = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_EPD266 = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_RowsLED = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_RFID_Device = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_WT32 = new List<DeviceBasic>();
            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_EPD583_serialize.IsTableCreat()) list_EPD583 = sQLControl_EPD583_serialize.GetAllRows(null);
                if (list_EPD583.Count > 0)
                {
                    deviceBasics_EPD583 = DrawerMethod.GetAllDeviceBasic(list_EPD583);
                }
            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_EPD266_serialize.IsTableCreat()) list_EPD266 = sQLControl_EPD266_serialize.GetAllRows(null);
                if (list_EPD266.Count > 0)
                {
                    deviceBasics_EPD266 = StorageMethod.GetAllDeviceBasic(list_EPD266);
                }

            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_RowsLED_serialize.IsTableCreat()) list_RowsLED = sQLControl_RowsLED_serialize.GetAllRows(null);

                if (list_RowsLED.Count > 0)
                {
                    deviceBasics_RowsLED = RowsLEDMethod.GetAllDeviceBasic(list_RowsLED);
                }

            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_RFID_Device_serialize.IsTableCreat()) list_RFID_Device = sQLControl_RFID_Device_serialize.GetAllRows(null);

                if (list_RFID_Device.Count > 0)
                {
                    deviceBasics_RFID_Device = RFIDMethod.GetAllDeviceBasic(list_RFID_Device);
                }

            }));
            taskList.Add(Task.Run(() =>
            {
                if (sQLControl_WT32_serialize.IsTableCreat()) list_WT32 = sQLControl_WT32_serialize.GetAllRows(null);


                if (list_WT32.Count > 0)
                {
                    deviceBasics_WT32 = StorageMethod.GetAllDeviceBasic(list_WT32);
                }
            }));
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics.LockAdd(deviceBasics_EPD583);
            deviceBasics.LockAdd(deviceBasics_EPD266);
            deviceBasics.LockAdd(deviceBasics_RowsLED);
            deviceBasics.LockAdd(deviceBasics_RFID_Device);
            deviceBasics.LockAdd(deviceBasics_WT32);

            deviceBasics_buf = deviceBasics;
            //deviceBasics_buf = (from value in deviceBasics
            //                    where value.Code.StringIsEmpty() == false
            //                    select value).ToList();

            return deviceBasics_buf;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<DeviceBasic> Function_讀取儲位_By_Code(sys_serverSettingClass sys_serverSettingClass, string Code)
        {
            string IP = sys_serverSettingClass.Server;
            string DBName = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();
            return Function_讀取儲位_By_Code(IP, DBName, UserName, Password, Port, Code);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        static public List<DeviceBasic> Function_讀取儲位_By_Code(string IP, string DBName, string UserName, string Password, uint Port , string Code)
        {
            SQLControl sQLControl_EPD583_serialize = new SQLControl(IP, DBName, "epd583_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DBName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DBName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(IP, DBName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_WT32_serialize = new SQLControl(IP, DBName, "WT32_Jsonstring", UserName, Password, Port, SSLMode);

            List<DeviceBasic> deviceBasics_EPD583 = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_EPD266 = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_RowsLED = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_RFID_Device = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_WT32 = new List<DeviceBasic>();

            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() =>
            {
                deviceBasics_EPD583 = DrawerMethod.GetDeviceBasicByCode(sQLControl_EPD583_serialize, Code);
            }));
            taskList.Add(Task.Run(() =>
            {
                deviceBasics_EPD266 = StorageMethod.GetDeviceBasicByCode(sQLControl_EPD266_serialize, Code);
            }));
            taskList.Add(Task.Run(() =>
            {
                deviceBasics_RowsLED = RowsLEDMethod.GetDeviceBasicByCode(sQLControl_RowsLED_serialize, Code);
            }));
            taskList.Add(Task.Run(() =>
            {
                //deviceBasics_RFID_Device = DrawerMethod.GetDeviceBasicByCode(sQLControl_RFID_Device_serialize, Code);
            }));
            taskList.Add(Task.Run(() =>
            {          
                deviceBasics_WT32 = StorageMethod.GetDeviceBasicByCode(sQLControl_WT32_serialize, Code);
            }));
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics.LockAdd(deviceBasics_EPD583);
            deviceBasics.LockAdd(deviceBasics_EPD266);
            deviceBasics.LockAdd(deviceBasics_RowsLED);
            deviceBasics.LockAdd(deviceBasics_RFID_Device);
            deviceBasics.LockAdd(deviceBasics_WT32);

            deviceBasics_buf = deviceBasics;
            //deviceBasics_buf = (from value in deviceBasics
            //                    where value.Code.StringIsEmpty() == false
            //                    select value).ToList();

            return deviceBasics_buf;
        }

        static private byte[] Get_EPD290_LEDBytes(Color color)
        {
            byte[] LED_Bytes = new byte[10 * 3];

            for (int i = 0; i < 10; i++)
            {
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }
        static private byte[] Get_EPD266_LEDBytes(Color color)
        {
            byte[] LED_Bytes = new byte[10 * 3];

            for (int i = 0; i < 10; i++)
            {
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }
        static private byte[] Get_Rows_LEDBytes(ref byte[] LED_Bytes, int startNum, int EndNum, Color color)
        {
            for (int i = startNum; i < EndNum; i++)
            {
                if (i > LED_Bytes.Length) break;
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }

        #region Drawer Function
        static private int NumOfLED = 450;
        static private int Drawer_NumOf_H_Line = 4;
        static private int Drawer_NumOf_V_Line = 8;
        static private int NumOfLED_Pannel = 42;
        static private int NumOfLED_Drawer = 450 - NumOfLED_Pannel;

        static private int Pannel_Width
        {
            get
            {
                return 648;
            }
        }
        static private int Pannel_Height
        {
            get
            {
                return 480;
            }
        }

        static private bool Set_LED_UDP(UDP_Class uDP_Class, string IP, byte[] LED_Bytes)
        {
            if (uDP_Class != null)
            {
                return Communication.Set_WS2812_Buffer(uDP_Class, IP, 0, LED_Bytes);
            }
            return false;
        }
        static private bool Set_LED_UDP(UDP_Class uDP_Class, Drawer drawer, Box box, Color color)
        {
            List<Box> boxes = new List<Box>();
            boxes.Add(box);
            return Set_LED_UDP(uDP_Class, drawer, boxes, color);
        }
        static private bool Set_LED_UDP(UDP_Class uDP_Class, Drawer drawer, List<Box> boxes, Color color)
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].IP == drawer.IP)
                {
                    drawer.LED_Bytes = Set_LEDBytes(drawer, boxes[i], color);
                }
            }
            drawer.LED_Bytes = Set_Pannel_LEDBytes(drawer, color);
            return Set_LED_UDP(uDP_Class, drawer.IP, drawer.LED_Bytes);
        }
        static private byte[] Set_Pannel_LEDBytes(Drawer drawer, Color color)
        {
            if (drawer.IsAllLight == false)
            {
                return Set_LEDBytes(drawer, color);
            }
            return Set_Pannel_LEDBytes(ref drawer.LED_Bytes, color);
        }
        static private byte[] Set_Pannel_LEDBytes(ref byte[] LED_Bytes, Color color)
        {
            for (int i = NumOfLED_Drawer; i < NumOfLED; i++)
            {
                if (i * 3 > LED_Bytes.Length) return LED_Bytes;
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }
        static private byte[] Set_LEDBytes(Drawer drawer, Color color)
        {
            for (int i = 0; i < NumOfLED; i++)
            {
                drawer.LED_Bytes[i * 3 + 0] = color.R;
                drawer.LED_Bytes[i * 3 + 1] = color.G;
                drawer.LED_Bytes[i * 3 + 2] = color.B;
            }
            return drawer.LED_Bytes;
        }
        static private byte[] Set_LEDBytes(Drawer drawer, Box box, Color color)
        {
            return Set_LEDBytes(drawer, box.Column, box.Row, color);
        }
        static private byte[] Set_LEDBytes(Drawer drawer, int col, int row, Color color)
        {
            if (col >= drawer.Boxes.Count) return drawer.LED_Bytes;
            for (int i = 0; i < drawer.Boxes.Count; i++)
            {
                if (row >= drawer.Boxes[i].Length) return drawer.LED_Bytes;
            }
            Rectangle rect = Get_Box_Combine(drawer, col, row);
            int width = rect.Width / (Pannel_Width / Drawer_NumOf_H_Line);
            int height = rect.Height / (Pannel_Height / Drawer_NumOf_V_Line);
            return Set_LEDBytes(col, row, width, height, ref drawer.LED_Bytes, color);
        }
        static private byte[] Set_LEDBytes(int col_x, int row_y, int width, int height, ref byte[] LEDBytes, Color color)
        {
            for (int i = 0; i < width; i++)
            {
                Set_Drawer_H_Leds(col_x + i + row_y * Drawer_NumOf_H_Line, ref LEDBytes, color);
                Set_Drawer_H_Leds(col_x + i + (height + row_y) * Drawer_NumOf_H_Line, ref LEDBytes, color);

            }
            for (int k = 0; k < height; k++)
            {
                Set_Drawer_V_Leds(row_y + k + col_x * Drawer_NumOf_V_Line, ref LEDBytes, color);
                Set_Drawer_V_Leds(row_y + k + (width + col_x) * Drawer_NumOf_V_Line, ref LEDBytes, color);
            }
            return LEDBytes;
        }
        static private void Set_Drawer_H_Leds(int col, ref byte[] LEDBytes, Color color)
        {
            for (int i = 0; i < List_Drawer_H_Line_Leds[col].Length; i++)
            {
                LEDBytes[List_Drawer_H_Line_Leds[col][i] * 3 + 0] = color.R;
                LEDBytes[List_Drawer_H_Line_Leds[col][i] * 3 + 1] = color.G;
                LEDBytes[List_Drawer_H_Line_Leds[col][i] * 3 + 2] = color.B;
            }
        }
        static private void Set_Drawer_V_Leds(int row, ref byte[] LEDBytes, Color color)
        {
            for (int i = 0; i < List_Drawer_V_Line_Leds[row].Length; i++)
            {
                LEDBytes[List_Drawer_V_Line_Leds[row][i] * 3 + 0] = color.R;
                LEDBytes[List_Drawer_V_Line_Leds[row][i] * 3 + 1] = color.G;
                LEDBytes[List_Drawer_V_Line_Leds[row][i] * 3 + 2] = color.B;
            }
        }
        static private Rectangle Get_Box_Combine(Drawer drawer, int col, int row)
        {
            if (col > drawer.Boxes.Count) return new Rectangle();
            if (row > drawer.Boxes[col].Length) return new Rectangle();
            return Get_Box_Combine(drawer, drawer.Boxes[col][row]);
        }
        static private Rectangle Get_Box_Combine(Drawer drawer, Box box)
        {
            Box _box;
            if (box.Slave)
            {
                _box = drawer.Boxes[box.MasterBox_Column][box.MasterBox_Row];
            }
            else
            {
                _box = box;
            }
            int X0 = _box.X;
            int Y0 = _box.Y;
            int X1 = X0 + _box.Width;
            int Y1 = Y0 + _box.Height;
            int X_temp = 0;
            int Y_temp = 0;
            int SlaveBox_Column = _box.SlaveBox_Column;
            int SlaveBox_Row = _box.SlaveBox_Row;
            int SlaveBox_Column_temp = SlaveBox_Column;
            int SlaveBox_Row_temp = SlaveBox_Row;
            while (true)
            {
                if (SlaveBox_Column_temp == -1 && SlaveBox_Row_temp == -1) break;
                SlaveBox_Column = SlaveBox_Column_temp;
                SlaveBox_Row = SlaveBox_Row_temp;
                X_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].X + drawer.Boxes[SlaveBox_Column][SlaveBox_Row].Width;
                Y_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].Y + drawer.Boxes[SlaveBox_Column][SlaveBox_Row].Height;
                if (X_temp > X1) X1 = X_temp;
                if (Y_temp > Y1) Y1 = Y_temp;
                SlaveBox_Column_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].SlaveBox_Column;
                SlaveBox_Row_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].SlaveBox_Row;
            }
            Rectangle rectangle = new Rectangle(X0, Y0, X1 - X0, Y1 - Y0);
            return rectangle;


        }
        #region LED_Line

        private static List<int[]> List_Drawer_H_Line_Leds = new List<int[]>();
        private static List<int[]> List_Drawer_V_Line_Leds = new List<int[]>();
        private static int[] Line_H_00 = new int[] { 376 + 0, 376 + 1, 376 + 2, 376 + 3, 376 + 4, 376 + 5, 376 + 6, 376 + 7 };
        private static int[] Line_H_01 = new int[] { 384 + 0, 384 + 1, 384 + 2, 384 + 3, 384 + 4, 384 + 5, 384 + 6, 384 + 7 };
        private static int[] Line_H_02 = new int[] { 392 + 0, 392 + 1, 392 + 2, 392 + 3, 392 + 4, 392 + 5, 392 + 6, 392 + 7 };
        private static int[] Line_H_03 = new int[] { 400 + 0, 400 + 1, 400 + 2, 400 + 3, 400 + 4, 400 + 5, 400 + 6, 400 + 7 };

        private static int[] Line_H_04 = new int[] { 368 + 0, 368 + 1, 368 + 2, 368 + 3, 368 + 4, 368 + 5, 368 + 6, 368 + 7 };
        private static int[] Line_H_05 = new int[] { 360 + 0, 360 + 1, 360 + 2, 360 + 3, 360 + 4, 360 + 5, 360 + 6, 360 + 7 };
        private static int[] Line_H_06 = new int[] { 352 + 0, 352 + 1, 352 + 2, 352 + 3, 352 + 4, 352 + 5, 352 + 6, 352 + 7 };
        private static int[] Line_H_07 = new int[] { 344 + 0, 344 + 1, 344 + 2, 344 + 3, 344 + 4, 344 + 5, 344 + 6, 344 + 7 };

        private static int[] Line_H_08 = new int[] { 312 + 0, 312 + 1, 312 + 2, 312 + 3, 312 + 4, 312 + 5, 312 + 6, 312 + 7 };
        private static int[] Line_H_09 = new int[] { 320 + 0, 320 + 1, 320 + 2, 320 + 3, 320 + 4, 320 + 5, 320 + 6, 320 + 7 };
        private static int[] Line_H_10 = new int[] { 328 + 0, 328 + 1, 328 + 2, 328 + 3, 328 + 4, 328 + 5, 328 + 6, 328 + 7 };
        private static int[] Line_H_11 = new int[] { 336 + 0, 336 + 1, 336 + 2, 336 + 3, 336 + 4, 336 + 5, 336 + 6, 336 + 7 };

        private static int[] Line_H_12 = new int[] { 304 + 0, 304 + 1, 304 + 2, 304 + 3, 304 + 4, 304 + 5, 304 + 6, 304 + 7 };
        private static int[] Line_H_13 = new int[] { 296 + 0, 296 + 1, 296 + 2, 296 + 3, 296 + 4, 296 + 5, 296 + 6, 296 + 7 };
        private static int[] Line_H_14 = new int[] { 288 + 0, 288 + 1, 288 + 2, 288 + 3, 288 + 4, 288 + 5, 288 + 6, 288 + 7 };
        private static int[] Line_H_15 = new int[] { 280 + 0, 280 + 1, 280 + 2, 280 + 3, 280 + 4, 280 + 5, 280 + 6, 280 + 7 };

        private static int[] Line_H_16 = new int[] { 248 + 0, 248 + 1, 248 + 2, 248 + 3, 248 + 4, 248 + 5, 248 + 6, 248 + 7 };
        private static int[] Line_H_17 = new int[] { 256 + 0, 256 + 1, 256 + 2, 256 + 3, 256 + 4, 256 + 5, 256 + 6, 256 + 7 };
        private static int[] Line_H_18 = new int[] { 264 + 0, 264 + 1, 264 + 2, 264 + 3, 264 + 4, 264 + 5, 264 + 6, 264 + 7 };
        private static int[] Line_H_19 = new int[] { 272 + 0, 272 + 1, 272 + 2, 272 + 3, 272 + 4, 272 + 5, 272 + 6, 272 + 7 };

        private static int[] Line_H_20 = new int[] { 240 + 0, 240 + 1, 240 + 2, 240 + 3, 240 + 4, 240 + 5, 240 + 6, 240 + 7 };
        private static int[] Line_H_21 = new int[] { 232 + 0, 232 + 1, 232 + 2, 232 + 3, 232 + 4, 232 + 5, 232 + 6, 232 + 7 };
        private static int[] Line_H_22 = new int[] { 224 + 0, 224 + 1, 224 + 2, 224 + 3, 224 + 4, 224 + 5, 224 + 6, 224 + 7 };
        private static int[] Line_H_23 = new int[] { 216 + 0, 216 + 1, 216 + 2, 216 + 3, 216 + 4, 216 + 5, 216 + 6, 216 + 7 };

        private static int[] Line_H_24 = new int[] { 184 + 0, 184 + 1, 184 + 2, 184 + 3, 184 + 4, 184 + 5, 184 + 6, 184 + 7 };
        private static int[] Line_H_25 = new int[] { 192 + 0, 192 + 1, 192 + 2, 192 + 3, 192 + 4, 192 + 5, 192 + 6, 192 + 7 };
        private static int[] Line_H_26 = new int[] { 200 + 0, 200 + 1, 200 + 2, 200 + 3, 200 + 4, 200 + 5, 200 + 6, 200 + 7 };
        private static int[] Line_H_27 = new int[] { 208 + 0, 208 + 1, 208 + 2, 208 + 3, 208 + 4, 208 + 5, 208 + 6, 208 + 7 };

        private static int[] Line_H_28 = new int[] { 176 + 0, 176 + 1, 176 + 2, 176 + 3, 176 + 4, 176 + 5, 176 + 6, 176 + 7 };
        private static int[] Line_H_29 = new int[] { 168 + 0, 168 + 1, 168 + 2, 168 + 3, 168 + 4, 168 + 5, 168 + 6, 168 + 7 };
        private static int[] Line_H_30 = new int[] { 160 + 0, 160 + 1, 160 + 2, 160 + 3, 160 + 4, 160 + 5, 160 + 6, 160 + 7 };
        private static int[] Line_H_31 = new int[] { 152 + 0, 152 + 1, 152 + 2, 152 + 3, 152 + 4, 152 + 5, 152 + 6, 152 + 7 };

        private static int[] Line_H_32 = new int[] { 120 + 0, 120 + 1, 120 + 2, 120 + 3, 120 + 4, 120 + 5, 120 + 6, 120 + 7 };
        private static int[] Line_H_33 = new int[] { 128 + 0, 128 + 1, 128 + 2, 128 + 3, 128 + 4, 128 + 5, 128 + 6, 128 + 7 };
        private static int[] Line_H_34 = new int[] { 136 + 0, 136 + 1, 136 + 2, 136 + 3, 136 + 4, 136 + 5, 136 + 6, 136 + 7 };
        private static int[] Line_H_35 = new int[] { 144 + 0, 144 + 1, 144 + 2, 144 + 3, 144 + 4, 144 + 5, 144 + 6, 144 + 7 };


        private static int[] Line_V_00 = new int[] { 96 + (0 * 3) + 0, 96 + (0 * 3) + 1, 96 + (0 * 3) + 2 };
        private static int[] Line_V_01 = new int[] { 96 + (1 * 3) + 0, 96 + (1 * 3) + 1, 96 + (1 * 3) + 2 };
        private static int[] Line_V_02 = new int[] { 96 + (2 * 3) + 0, 96 + (2 * 3) + 1, 96 + (2 * 3) + 2 };
        private static int[] Line_V_03 = new int[] { 96 + (3 * 3) + 0, 96 + (3 * 3) + 1, 96 + (3 * 3) + 2 };
        private static int[] Line_V_04 = new int[] { 96 + (4 * 3) + 0, 96 + (4 * 3) + 1, 96 + (4 * 3) + 2 };
        private static int[] Line_V_05 = new int[] { 96 + (5 * 3) + 0, 96 + (5 * 3) + 1, 96 + (5 * 3) + 2 };
        private static int[] Line_V_06 = new int[] { 96 + (6 * 3) + 0, 96 + (6 * 3) + 1, 96 + (6 * 3) + 2 };
        private static int[] Line_V_07 = new int[] { 96 + (7 * 3) + 0, 96 + (7 * 3) + 1, 96 + (7 * 3) + 2 };

        private static int[] Line_V_08 = new int[] { 95 - (0 * 3) - 0, 95 - (0 * 3) - 1, 95 - (0 * 3) - 2 };
        private static int[] Line_V_09 = new int[] { 95 - (1 * 3) - 0, 95 - (1 * 3) - 1, 95 - (1 * 3) - 2 };
        private static int[] Line_V_10 = new int[] { 95 - (2 * 3) - 0, 95 - (2 * 3) - 1, 95 - (2 * 3) - 2 };
        private static int[] Line_V_11 = new int[] { 95 - (3 * 3) - 0, 95 - (3 * 3) - 1, 95 - (3 * 3) - 2 };
        private static int[] Line_V_12 = new int[] { 95 - (4 * 3) - 0, 95 - (4 * 3) - 1, 95 - (4 * 3) - 2 };
        private static int[] Line_V_13 = new int[] { 95 - (5 * 3) - 0, 95 - (5 * 3) - 1, 95 - (5 * 3) - 2 };
        private static int[] Line_V_14 = new int[] { 95 - (6 * 3) - 0, 95 - (6 * 3) - 1, 95 - (6 * 3) - 2 };
        private static int[] Line_V_15 = new int[] { 95 - (7 * 3) - 0, 95 - (7 * 3) - 1, 95 - (7 * 3) - 2 };

        private static int[] Line_V_16 = new int[] { 48 + (0 * 3) + 0, 48 + (0 * 3) + 1, 48 + (0 * 3) + 2 };
        private static int[] Line_V_17 = new int[] { 48 + (1 * 3) + 0, 48 + (1 * 3) + 1, 48 + (1 * 3) + 2 };
        private static int[] Line_V_18 = new int[] { 48 + (2 * 3) + 0, 48 + (2 * 3) + 1, 48 + (2 * 3) + 2 };
        private static int[] Line_V_19 = new int[] { 48 + (3 * 3) + 0, 48 + (3 * 3) + 1, 48 + (3 * 3) + 2 };
        private static int[] Line_V_20 = new int[] { 48 + (4 * 3) + 0, 48 + (4 * 3) + 1, 48 + (4 * 3) + 2 };
        private static int[] Line_V_21 = new int[] { 48 + (5 * 3) + 0, 48 + (5 * 3) + 1, 48 + (5 * 3) + 2 };
        private static int[] Line_V_22 = new int[] { 48 + (6 * 3) + 0, 48 + (6 * 3) + 1, 48 + (6 * 3) + 2 };
        private static int[] Line_V_23 = new int[] { 48 + (7 * 3) + 0, 48 + (7 * 3) + 1, 48 + (7 * 3) + 2 };

        private static int[] Line_V_24 = new int[] { 47 - (0 * 3) - 0, 47 - (0 * 3) - 1, 47 - (0 * 3) - 2 };
        private static int[] Line_V_25 = new int[] { 47 - (1 * 3) - 0, 47 - (1 * 3) - 1, 47 - (1 * 3) - 2 };
        private static int[] Line_V_26 = new int[] { 47 - (2 * 3) - 0, 47 - (2 * 3) - 1, 47 - (2 * 3) - 2 };
        private static int[] Line_V_27 = new int[] { 47 - (3 * 3) - 0, 47 - (3 * 3) - 1, 47 - (3 * 3) - 2 };
        private static int[] Line_V_28 = new int[] { 47 - (4 * 3) - 0, 47 - (4 * 3) - 1, 47 - (4 * 3) - 2 };
        private static int[] Line_V_29 = new int[] { 47 - (5 * 3) - 0, 47 - (5 * 3) - 1, 47 - (5 * 3) - 2 };
        private static int[] Line_V_30 = new int[] { 47 - (6 * 3) - 0, 47 - (6 * 3) - 1, 47 - (6 * 3) - 2 };
        private static int[] Line_V_31 = new int[] { 47 - (7 * 3) - 0, 47 - (7 * 3) - 1, 47 - (7 * 3) - 2 };

        private static int[] Line_V_32 = new int[] { 00 + (0 * 3) + 0, 00 + (0 * 3) + 1, 00 + (0 * 3) + 2 };
        private static int[] Line_V_33 = new int[] { 00 + (1 * 3) + 0, 00 + (1 * 3) + 1, 00 + (1 * 3) + 2 };
        private static int[] Line_V_34 = new int[] { 00 + (2 * 3) + 0, 00 + (2 * 3) + 1, 00 + (2 * 3) + 2 };
        private static int[] Line_V_35 = new int[] { 00 + (3 * 3) + 0, 00 + (3 * 3) + 1, 00 + (3 * 3) + 2 };
        private static int[] Line_V_36 = new int[] { 00 + (4 * 3) + 0, 00 + (4 * 3) + 1, 00 + (4 * 3) + 2 };
        private static int[] Line_V_37 = new int[] { 00 + (5 * 3) + 0, 00 + (5 * 3) + 1, 00 + (5 * 3) + 2 };
        private static int[] Line_V_38 = new int[] { 00 + (6 * 3) + 0, 00 + (6 * 3) + 1, 00 + (6 * 3) + 2 };
        private static int[] Line_V_39 = new int[] { 00 + (7 * 3) + 0, 00 + (7 * 3) + 1, 00 + (7 * 3) + 2 };
        static private void WS2812_Init()
        {
            List_Drawer_H_Line_Leds.Clear();
            List_Drawer_V_Line_Leds.Clear();

            List_Drawer_H_Line_Leds.Add(Line_H_00);
            List_Drawer_H_Line_Leds.Add(Line_H_01);
            List_Drawer_H_Line_Leds.Add(Line_H_02);
            List_Drawer_H_Line_Leds.Add(Line_H_03);

            List_Drawer_H_Line_Leds.Add(Line_H_04);
            List_Drawer_H_Line_Leds.Add(Line_H_05);
            List_Drawer_H_Line_Leds.Add(Line_H_06);
            List_Drawer_H_Line_Leds.Add(Line_H_07);

            List_Drawer_H_Line_Leds.Add(Line_H_08);
            List_Drawer_H_Line_Leds.Add(Line_H_09);
            List_Drawer_H_Line_Leds.Add(Line_H_10);
            List_Drawer_H_Line_Leds.Add(Line_H_11);

            List_Drawer_H_Line_Leds.Add(Line_H_12);
            List_Drawer_H_Line_Leds.Add(Line_H_13);
            List_Drawer_H_Line_Leds.Add(Line_H_14);
            List_Drawer_H_Line_Leds.Add(Line_H_15);

            List_Drawer_H_Line_Leds.Add(Line_H_16);
            List_Drawer_H_Line_Leds.Add(Line_H_17);
            List_Drawer_H_Line_Leds.Add(Line_H_18);
            List_Drawer_H_Line_Leds.Add(Line_H_19);

            List_Drawer_H_Line_Leds.Add(Line_H_20);
            List_Drawer_H_Line_Leds.Add(Line_H_21);
            List_Drawer_H_Line_Leds.Add(Line_H_22);
            List_Drawer_H_Line_Leds.Add(Line_H_23);

            List_Drawer_H_Line_Leds.Add(Line_H_24);
            List_Drawer_H_Line_Leds.Add(Line_H_25);
            List_Drawer_H_Line_Leds.Add(Line_H_26);
            List_Drawer_H_Line_Leds.Add(Line_H_27);

            List_Drawer_H_Line_Leds.Add(Line_H_28);
            List_Drawer_H_Line_Leds.Add(Line_H_29);
            List_Drawer_H_Line_Leds.Add(Line_H_30);
            List_Drawer_H_Line_Leds.Add(Line_H_31);

            List_Drawer_H_Line_Leds.Add(Line_H_32);
            List_Drawer_H_Line_Leds.Add(Line_H_33);
            List_Drawer_H_Line_Leds.Add(Line_H_34);
            List_Drawer_H_Line_Leds.Add(Line_H_35);

            List_Drawer_V_Line_Leds.Add(Line_V_00);
            List_Drawer_V_Line_Leds.Add(Line_V_01);
            List_Drawer_V_Line_Leds.Add(Line_V_02);
            List_Drawer_V_Line_Leds.Add(Line_V_03);
            List_Drawer_V_Line_Leds.Add(Line_V_04);

            List_Drawer_V_Line_Leds.Add(Line_V_05);
            List_Drawer_V_Line_Leds.Add(Line_V_06);
            List_Drawer_V_Line_Leds.Add(Line_V_07);
            List_Drawer_V_Line_Leds.Add(Line_V_08);
            List_Drawer_V_Line_Leds.Add(Line_V_09);

            List_Drawer_V_Line_Leds.Add(Line_V_10);
            List_Drawer_V_Line_Leds.Add(Line_V_11);
            List_Drawer_V_Line_Leds.Add(Line_V_12);
            List_Drawer_V_Line_Leds.Add(Line_V_13);
            List_Drawer_V_Line_Leds.Add(Line_V_14);

            List_Drawer_V_Line_Leds.Add(Line_V_15);
            List_Drawer_V_Line_Leds.Add(Line_V_16);
            List_Drawer_V_Line_Leds.Add(Line_V_17);
            List_Drawer_V_Line_Leds.Add(Line_V_18);
            List_Drawer_V_Line_Leds.Add(Line_V_19);

            List_Drawer_V_Line_Leds.Add(Line_V_20);
            List_Drawer_V_Line_Leds.Add(Line_V_21);
            List_Drawer_V_Line_Leds.Add(Line_V_22);
            List_Drawer_V_Line_Leds.Add(Line_V_23);
            List_Drawer_V_Line_Leds.Add(Line_V_24);

            List_Drawer_V_Line_Leds.Add(Line_V_25);
            List_Drawer_V_Line_Leds.Add(Line_V_26);
            List_Drawer_V_Line_Leds.Add(Line_V_27);
            List_Drawer_V_Line_Leds.Add(Line_V_28);
            List_Drawer_V_Line_Leds.Add(Line_V_29);

            List_Drawer_V_Line_Leds.Add(Line_V_30);
            List_Drawer_V_Line_Leds.Add(Line_V_31);
            List_Drawer_V_Line_Leds.Add(Line_V_32);
            List_Drawer_V_Line_Leds.Add(Line_V_33);
            List_Drawer_V_Line_Leds.Add(Line_V_34);

            List_Drawer_V_Line_Leds.Add(Line_V_35);
            List_Drawer_V_Line_Leds.Add(Line_V_36);
            List_Drawer_V_Line_Leds.Add(Line_V_37);
            List_Drawer_V_Line_Leds.Add(Line_V_38);
            List_Drawer_V_Line_Leds.Add(Line_V_39);
        }
        #endregion
        #endregion


        static public List<object[]> Function_取得異動儲位資訊(List<DeviceBasic> deviceBasics, string 藥品碼, double 異動量)
        {
            List<object> 儲位 = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            deviceBasics = deviceBasics.SortByCode(藥品碼);

            for (int i = 0; i < deviceBasics.Count; i++)
            {
                儲位.Add(deviceBasics[i]);
                儲位_TYPE.Add(deviceBasics[i].DeviceType.GetEnumName());

            }

            List<object[]> 儲位資訊_buf = new List<object[]>();
            List<object[]> 儲位資訊 = new List<object[]>();
            if (儲位.Count == 0) return 儲位資訊_buf;


            for (int k = 0; k < 儲位.Count; k++)
            {
                object value_device = 儲位[k];
                if (value_device is Device)
                {
                    Device device = (Device)value_device;
                    for (int i = 0; i < device.List_Validity_period.Count; i++)
                    {
                        object[] value = new object[new enum_儲位資訊().GetLength()];
                        value[(int)enum_儲位資訊.IP] = device.IP;
                        value[(int)enum_儲位資訊.TYPE] = 儲位_TYPE[k];
                        value[(int)enum_儲位資訊.效期] = device.List_Validity_period[i];
                        value[(int)enum_儲位資訊.批號] = device.List_Lot_number[i];
                        value[(int)enum_儲位資訊.庫存] = device.List_Inventory[i];
                        value[(int)enum_儲位資訊.異動量] = "0";
                        value[(int)enum_儲位資訊.Value] = value_device;
                        儲位資訊.Add(value);
                    }
                }
            }
            儲位資訊 = 儲位資訊.OrderBy(r => DateTime.Parse(r[(int)enum_儲位資訊.效期].ToDateString())).ToList();

            if (異動量 == 0) return 儲位資訊;
            double 使用數量 = 異動量;
            double 庫存數量 = 0;
            double 剩餘庫存數量 = 0;
            for (int i = 0; i < 儲位資訊.Count; i++)
            {
                庫存數量 = 儲位資訊[i][(int)enum_儲位資訊.庫存].ObjectToString().StringToInt32();
                if ((使用數量 < 0 && 庫存數量 > 0) || (使用數量 > 0 && 庫存數量 >= 0))
                {
                    剩餘庫存數量 = 庫存數量 + 使用數量;
                    if (剩餘庫存數量 >= 0)
                    {
                        儲位資訊[i][(int)enum_儲位資訊.異動量] = (使用數量).ToString();
                        儲位資訊_buf.Add(儲位資訊[i]);
                        break;
                    }
                    else
                    {
                        儲位資訊[i][(int)enum_儲位資訊.異動量] = (庫存數量 * -1).ToString();
                        使用數量 = 剩餘庫存數量;
                        儲位資訊_buf.Add(儲位資訊[i]);
                    }
                }
            }

            return 儲位資訊_buf;
        }
        static public object Function_庫存異動(object[] 儲位資訊 , sys_serverSettingClass sys_serverSettingClass)
        {
            SQLControl sQLControl_EPD583_serialize = new SQLControl(sys_serverSettingClass.Server, sys_serverSettingClass.DBName, "epd583_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(sys_serverSettingClass.Server, sys_serverSettingClass.DBName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(sys_serverSettingClass.Server, sys_serverSettingClass.DBName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(sys_serverSettingClass.Server, sys_serverSettingClass.DBName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_WT32_serialize = new SQLControl(sys_serverSettingClass.Server, sys_serverSettingClass.DBName, "WT32_Jsonstring", UserName, Password, Port, SSLMode);

            object Value = 儲位資訊[(int)enum_儲位資訊.Value];
            string 效期 = 儲位資訊[(int)enum_儲位資訊.效期].ObjectToString();
            string 異動量 = 儲位資訊[(int)enum_儲位資訊.異動量].ObjectToString();
            string TYPE = 儲位資訊[(int)enum_儲位資訊.TYPE].ObjectToString();
            if (Value is Storage)
            {
                if (TYPE == DeviceType.EPD266.GetEnumName() || TYPE == DeviceType.EPD266_lock.GetEnumName() || TYPE == DeviceType.EPD290.GetEnumName() || TYPE == DeviceType.EPD290_lock.GetEnumName())
                {
                  
                    Storage storage = (Storage)Value;
                    storage = StorageMethod.SQL_GetStorageByIP(sQLControl_EPD266_serialize ,storage.IP);
                    if (storage != null)
                    {
                        storage.效期庫存異動(效期, 異動量, false);
                        StorageMethod.SQL_ReplaceByIP(sQLControl_EPD266_serialize ,storage);
                        return storage;
                    }
                }
                if (TYPE == DeviceType.Pannel35.GetEnumName() || TYPE == DeviceType.Pannel35_lock.GetEnumName())
                {
                    Storage storage = (Storage)Value;
                    storage = StorageMethod.SQL_GetStorageByIP(sQLControl_WT32_serialize ,storage.IP);
                    if (storage != null)
                    {
                        storage.效期庫存異動(效期, 異動量, false);
                        StorageMethod.SQL_ReplaceByIP(sQLControl_WT32_serialize, storage);
                        return storage;
                    }
                }
            }
            else if (Value is Box)
            {
                if (TYPE == DeviceType.EPD583.GetEnumName() || TYPE == DeviceType.EPD583_lock.GetEnumName())
                {
                    Box box = (Box)Value;
                    Drawer drawer = DrawerMethod.SQL_GetDrawerByIP(sQLControl_EPD583_serialize, box.IP);                   
                    box.效期庫存異動(效期, 異動量, false);
                    drawer.ReplaceBox(box);
                    DrawerMethod.SQL_ReplaceByIP(sQLControl_EPD583_serialize, drawer);
                    return drawer;
                }
          
            }
            else if (Value is RowsDevice)
            {
                if (TYPE == DeviceType.RowsLED.GetEnumName())
                {
                    RowsDevice rowsDevice = Value as RowsDevice;
                    RowsLED rowsLED = RowsLEDMethod.SQL_GetRowsLEDByIP(sQLControl_RFID_Device_serialize, rowsDevice.IP);
                    rowsDevice.效期庫存異動(效期, 異動量, false);
                    RowsLEDMethod.SQL_ReplaceByIP(sQLControl_RowsLED_serialize, rowsLED);
                    return rowsLED;
                }

            }
      
            return null;
        }


    }
}
