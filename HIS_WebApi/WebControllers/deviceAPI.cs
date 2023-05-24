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
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("getall")]
        [HttpPost]
        public string POST_device_get(returnData returnData)
        {
            try
            {
                List<DeviceBasic> deviceBasics = Function_Get_device(returnData.Server, returnData.DbName, returnData.TableName);
                returnData.Code = 200;
                returnData.Value = $"Device取得成功!TableName : {returnData.TableName}";
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
            List<object[]> list_EPD583 = sQLControl_EPD583_serialize.GetAllRows(null);
            List<object[]> list_EPD266 = sQLControl_EPD266_serialize.GetAllRows(null);
            List<object[]> list_RowsLED = sQLControl_RowsLED_serialize.GetAllRows(null);
            List<object[]> list_RFID_Device = sQLControl_RFID_Device_serialize.GetAllRows(null);
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
    }
}
