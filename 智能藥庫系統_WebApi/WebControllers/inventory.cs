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
namespace 智慧藥庫系統_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inventoryController : Controller
    {
        public enum enum_盤點單號
        {
            GUID,
            盤點單號,
            建表人,
            建表時間,
            盤點開始時間,
            盤點結束時間,
            盤點狀態
        }

        static private string DataBaseName = ConfigurationManager.AppSettings["database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        static private string MDC_DataBaseName = ConfigurationManager.AppSettings["medicine_page_cloud_database"];
        static private string MDC_IP = ConfigurationManager.AppSettings["medicine_page_cloud_IP"];
        private SQLControl sQLControl_medicine_page_cloud = new SQLControl(MDC_IP, MDC_DataBaseName, "medicine_page_cloud", UserName, Password, Port, SSLMode);

        private SQLControl sQLControl_inventory = new SQLControl(IP, DataBaseName, "inventory_creat", UserName, Password, Port, SSLMode);

        public class returnData
        {
            private List<object> _data = new List<object>();
            private int _code = 0;
            private string _result = "";

            public List<object> Data { get => _data; set => _data = value; }
            public int Code { get => _code; set => _code = value; }
            public string Result { get => _result; set => _result = value; }
        }
        public class inventory_creat_OUT
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("INV_SN_L")]
            public string 盤點單號 { get; set; }
            [JsonPropertyName("CREAT_OP")]
            public string 建表人 { get; set; }
            [JsonPropertyName("CREAT_TIME")]
            public string 建表時間 { get; set; }
            [JsonPropertyName("START_TIME")]
            public string 盤點開始時間 { get; set; }
            [JsonPropertyName("END_TIME")]
            public string 盤點結束時間 { get; set; }
            [JsonPropertyName("STATE")]
            public string 盤點狀態 { get; set; }

            static public object[] ClassToSQL(inventory_creat_OUT _inventory_creat_OUT)
            {
                object[] value = new object[new enum_盤點單號().GetLength()];
       
                return value;
            }
            static public inventory_creat_OUT SQLToClass(object[] value)
            {
                inventory_creat_OUT inventory_Creat_OUT = new inventory_creat_OUT();
                inventory_Creat_OUT.GUID = value[(int)enum_盤點單號.GUID].ObjectToString();
                inventory_Creat_OUT.盤點單號 = value[(int)enum_盤點單號.盤點單號].ObjectToString();
                inventory_Creat_OUT.建表人 = value[(int)enum_盤點單號.建表人].ObjectToString();
                inventory_Creat_OUT.建表時間 = value[(int)enum_盤點單號.建表時間].ToDateString();
                inventory_Creat_OUT.盤點開始時間 = value[(int)enum_盤點單號.盤點開始時間].ToDateString();
                inventory_Creat_OUT.盤點結束時間 = value[(int)enum_盤點單號.盤點結束時間].ToDateString();
                inventory_Creat_OUT.盤點狀態 = value[(int)enum_盤點單號.盤點狀態].ObjectToString();


                return inventory_Creat_OUT;
            }
            static public inventory_creat_OUT ObjToData(object data)
            {
                string jsondata = data.JsonSerializationt();
                return jsondata.JsonDeserializet<inventory_creat_OUT>();
            }
        }
        [Route("creat")]
        [HttpGet]
        public string Get_creat()
        {
            returnData _returnData = new returnData();
            List<object[]> list_盤點單號 = sQLControl_inventory.GetAllRows(null);
            for  (int i = 0; i < list_盤點單號.Count; i++)
            {
                inventory_creat_OUT inventory_Creat_OUT = inventory_creat_OUT.SQLToClass(list_盤點單號[i]);
                _returnData.Data.Add(inventory_Creat_OUT);
            }
            _returnData.Code = 200;
            _returnData.Result = "取得盤點單號成功!";
            return _returnData.JsonSerializationt();
        }
        [Route("creat")]
        [HttpPost]
        public string POST_creat([FromBody] returnData returnData)
        {
            if (returnData.Data.Count == 0)
            {
                returnData.Code = -1;
                returnData.Result = "輸入Data長度錯誤!";
            }
            inventory_creat_OUT inventory_Creat_OUT = inventory_creat_OUT.ObjToData(returnData.Data[0]);
            if(inventory_Creat_OUT == null)
            {
                returnData.Code = -2;
                returnData.Result = "輸入資料錯誤!";
            }
            inventory_Creat_OUT.GUID = Guid.NewGuid().ToString();
            inventory_Creat_OUT.盤點單號 = $"{DateTime.Now.ToShortDateString()}";



        }

    }
}
