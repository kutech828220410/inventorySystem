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
    public class firstclass_deviceController : Controller
    {
        static private string DataBaseName = ConfigurationManager.AppSettings["database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        static private string MDC_DataBaseName = ConfigurationManager.AppSettings["MED_cloud_DB"];
        static private string MDC_IP = ConfigurationManager.AppSettings["MED_cloud_IP"];

        private SQLControl sQLControl_firstclass_device = new SQLControl(IP, DataBaseName, "firstclass_device_jsonstring", UserName, Password, Port, SSLMode);

        [Route("getall")]
        [HttpGet]
        public string POST_firstclass_device_get()
        {
            returnData returnData = new returnData();
            try
            {       
                List<DeviceBasic> deviceBasics = Function_Get_firstclass_device();
                returnData.Code = 200;
                returnData.Value = "藥庫庫存Device取得成功!";
                returnData.Data = deviceBasics;
                return returnData.JsonSerializationt();
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }
      
     
        }

        public List<DeviceBasic> Function_Get_firstclass_device()
        {
            List<DeviceBasic> deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_firstclass_device);
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            for (int i = 0; i < deviceBasics.Count; i++)
            {
                deviceBasics_buf.Add(deviceBasics[i]);
            }
            return deviceBasics_buf;
        }
    }
}
