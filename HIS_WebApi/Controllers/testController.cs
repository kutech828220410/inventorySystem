using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Reflection;
using System.Configuration;
using IBM.Data.DB2.Core;
using MyOffice;
using NPOI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIS_DB_Lib;
using H_Pannel_lib;
using System.Drawing;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        static string Version = "Ver1.0.0";
        [HttpGet]
        public string Get()
        {
            var localIpAddress = HttpContext.Connection.LocalIpAddress?.ToString();
            var localPort = HttpContext.Connection.LocalPort;
            var protocol = HttpContext.Request.IsHttps ? "https" : "http";
            returnData returnData = new returnData();
            returnData.Code = 200;
            returnData.Result = $"Api test sucess!{protocol}://{localIpAddress}:{localPort}";

            string DB = ConfigurationManager.AppSettings["DB"];
            string Server = ConfigurationManager.AppSettings["Server"];
            string VM_Server = ConfigurationManager.AppSettings["VM_Server"];
            string VM_DB = ConfigurationManager.AppSettings["VM_DB"];

            List<string> strs = new List<string>();
            strs.Add($"local Server : {Server}");
            strs.Add($"local Database : {DB}");
            strs.Add($"VM Server : {VM_Server}");
            strs.Add($"VM Database : {VM_DB}");
            //strs.Add($"uDP_Class PORT: {Startup.uDP_Class.Port}");
            strs.Add($"Version : {Version}");


            returnData.Data = strs;


            return returnData.JsonSerializationt(true);
        }
        [Route("price")]
        [HttpGet]
        public string Get_price()
        {
            String MyDb2ConnectionString = "server=DBGW1.VGHKS.GOV.TW:50000;database=DBDSNP;uid=APUD07;pwd=UD07AP;";
            IBM.Data.DB2.Core.DB2Connection MyDb2Connection = new IBM.Data.DB2.Core.DB2Connection(MyDb2ConnectionString);
            Console.Write($"開啟DB2連線....");
            MyDb2Connection.Open();
            Console.WriteLine($"DB2連線成功!");
            IBM.Data.DB2.Core.DB2Command MyDB2Command = MyDb2Connection.CreateCommand();
            MyDB2Command.CommandText = "SELECT * FROM UD.UDDRGVWA WHERE HID = '2A0'";

            var reader = MyDB2Command.ExecuteReader();
            List<string> list_colname_UDDRGVWA = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                list_colname_UDDRGVWA.Add(name);
            }
            while (reader.Read())
            {

            }
            reader.Close();
            MyDB2Command.CommandText = "SELECT * FROM UD.UDPRDPF WHERE HID = '2A0'";
            List<string> list_colname_UDPRDPF = new List<string>();
            var reader_UDPRDPF = MyDB2Command.ExecuteReader();
            for (int i = 0; i < reader_UDPRDPF.FieldCount; i++)
            {
                string name = reader_UDPRDPF.GetName(i);
                list_colname_UDPRDPF.Add(name);
            }
            return list_colname_UDDRGVWA.JsonSerializationt() + list_colname_UDPRDPF.JsonSerializationt();
        }

        [Route("OrderLastWriteTime")]
        [HttpGet]
        public string Get_OrderLastWriteTime()
        {

            DateTime dateTime = System.IO.File.GetLastWriteTime(@"C:\itinvd0304.txt");
            return dateTime.ToDateTimeString();
        }

   
    }

 
}
