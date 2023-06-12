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

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class deviceController : Controller
    {
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

            List<DeviceBasic> deviceBasics = Function_Get_device(device_Server, device_DB, device_TableName);
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
            }
            returnData returnData = new returnData();
            returnData.Code = 200;
            returnData.Result = $"Device取得成功!TableName : {device_TableName}";
            returnData.TimeTaken = myTimer.ToString();
            returnData.Data = deviceBasics_buf;
            return returnData.JsonSerializationt(true);
        }

        [Route("all")]
        [HttpGet]
        public string GET_all()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<DeviceBasic> deviceBasics = Function_Get_device(device_Server, device_DB, device_TableName);
            returnData.Code = 200;
            returnData.Result = $"Device取得成功!TableName : {device_TableName}";
            returnData.TimeTaken = myTimer.ToString();
            returnData.Data = deviceBasics;
            return returnData.JsonSerializationt(true);
        }
        [Route("all")]
        [HttpPost]
        public string POST_all(returnData returnData)
        {
            try
            {


                List<DeviceBasic> deviceBasics = Function_Get_device(returnData.Server, returnData.DbName, returnData.TableName);
                returnData.Code = 200;
                returnData.Result = $"Device取得成功!TableName : {device_TableName}";
                returnData.Data = deviceBasics;
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
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
              
                Color color = returnData.Value.ToColor();
                string json_in = returnData.Data.JsonSerializationt();
                List<DeviceBasic> deviceBasics = json_in.JsonDeserializet<List<DeviceBasic>>();
                for (int i = 0; i < deviceBasics.Count; i++)
                {
                    DeviceType deviceType = deviceBasics[i].DeviceType;
                    if (deviceType == DeviceType.EPD290 || deviceType == DeviceType.EPD290_lock)
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, Get_EPD290_LEDBytes(color));
                    }
                    if (deviceType == DeviceType.EPD266 || deviceType == DeviceType.EPD266_lock)
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, Get_EPD266_LEDBytes(color));
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


        public List<DeviceBasic> Function_Get_device()
        {
            return Function_Get_device(device_Server, device_DB, device_TableName);
        }
        public List<DeviceBasic> Function_Get_device(returnData returnData)
        {
            return Function_Get_device(returnData.Server, returnData.DbName, returnData.TableName);
        }
        public List<DeviceBasic> Function_Get_device(string IP, string DBName, string TableName)
        {
            SQLControl sQLControl_device = new SQLControl(IP, DBName, TableName, UserName, Password, Port, SSLMode);
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            if (TableName.StringIsEmpty() == false)
            {
                deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);
            }
            else
            {
                deviceBasics = Function_讀取儲位(IP, DBName);
            }
       
            return deviceBasics;
        }

       
        private List<DeviceBasic> Function_讀取儲位(string IP, string DBName)
        {
    
            SQLControl sQLControl_EPD583_serialize = new SQLControl(IP, DBName, "epd583_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DBName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DBName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(IP, DBName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);
            List<object[]> list_EPD583 = new List<object[]>();
            List<object[]> list_EPD266 = new List<object[]>();
            List<object[]> list_RowsLED = new List<object[]>();
            List<object[]> list_RFID_Device = new List<object[]>();
            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() =>
            {
                list_EPD583 = sQLControl_EPD583_serialize.GetAllRows(null);
            }));
            taskList.Add(Task.Run(() =>
            {
               list_EPD266 = sQLControl_EPD266_serialize.GetAllRows(null);
            }));
            taskList.Add(Task.Run(() =>
            {
               list_RowsLED = sQLControl_RowsLED_serialize.GetAllRows(null);
            }));
            taskList.Add(Task.Run(() =>
            {
               list_RFID_Device = sQLControl_RFID_Device_serialize.GetAllRows(null);
            }));
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics.LockAdd(DrawerMethod.GetAllDeviceBasic(list_EPD583));
            deviceBasics.LockAdd(StorageMethod.GetAllDeviceBasic(list_EPD266));
            deviceBasics.LockAdd(RowsLEDMethod.GetAllDeviceBasic(list_RowsLED));
            deviceBasics.LockAdd(RFIDMethod.GetAllDeviceBasic(list_RFID_Device));
            deviceBasics_buf = (from value in deviceBasics
                                where value.Code.StringIsEmpty() == false
                                select value).ToList();

            return deviceBasics_buf;
        }

        static public byte[] Get_EPD290_LEDBytes(Color color)
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
        static public byte[] Get_EPD266_LEDBytes(Color color)
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
    }
}
