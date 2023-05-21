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
        static private string DataBaseName = ConfigurationManager.AppSettings["database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("getall")]
        [HttpGet]
        public string POST_firstclass_device_get(string TableName)
        {
            returnData returnData = new returnData();
            try
            {
                List<DeviceBasic> deviceBasics = Function_Get_device(TableName);
                returnData.Code = 200;
                returnData.Value = $"Device取得成功!TableName : {TableName}";
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

        public List<DeviceBasic> Function_Get_device(string TableName)
        {
            SQLControl sQLControl_device = new SQLControl(IP, DataBaseName, TableName, UserName, Password, Port, SSLMode);
            List<DeviceBasic> deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            for (int i = 0; i < deviceBasics.Count; i++)
            {
                deviceBasics_buf.Add(deviceBasics[i]);
            }
            return deviceBasics_buf;
        }
    }
}
