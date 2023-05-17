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
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inventoryController : Controller
    {
     

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

       
       
        [Route("creat")]
        [HttpGet]
        public string Get_creat()
        {
            returnData _returnData = new returnData();
            List<object[]> list_盤點單號 = sQLControl_inventory.GetAllRows(null);
            for  (int i = 0; i < list_盤點單號.Count; i++)
            {
                inventoryClass.creat_OUT inventory_Creat_OUT = inventoryClass.creat_OUT.SQLToClass(list_盤點單號[i]);
                _returnData.Data.Add(inventory_Creat_OUT);
            }
            _returnData.Code = 200;
            _returnData.Result = "取得盤點單號成功!";
            _returnData.Data.Sort(new ICP_inventory_Creat_OUT());
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
            inventoryClass.creat_OUT inventory_Creat_OUT = inventoryClass.creat_OUT.ObjToData(returnData.Data[0]);
            if(inventory_Creat_OUT == null)
            {
                returnData.Code = -2;
                returnData.Result = "輸入資料錯誤!";
            }
            List<object[]> list_盤點單號 = sQLControl_inventory.GetAllRows(null);
            list_盤點單號 = list_盤點單號.GetRowsInDate((int)enum_盤點單號.建表時間, DateTime.Now);
            inventory_Creat_OUT.GUID = Guid.NewGuid().ToString();
            inventory_Creat_OUT.盤點單號 = $"{DateTime.Now.ToDateTinyString()}_{list_盤點單號.Count}";
            inventory_Creat_OUT.建表時間 = DateTime.Now.ToDateTimeString();
            inventory_Creat_OUT.盤點開始時間 = DateTime.MinValue.ToDateTimeString();
            inventory_Creat_OUT.盤點結束時間 = DateTime.MinValue.ToDateTimeString();
            inventory_Creat_OUT.盤點狀態 = "等待開始盤點";
            object[] value = inventoryClass.creat_OUT.ClassToSQL(inventory_Creat_OUT);
            sQLControl_inventory.AddRow(null, value);
            string str_Get_creat = this.Get_creat();
            return str_Get_creat;
        }
        [Route("delete")]
        [HttpPost]
        public string POST_delete([FromBody] returnData returnData)
        {
            List<object[]> list_盤點單號 = sQLControl_inventory.GetAllRows(null);
            List<object[]> list_盤點單號_buf = new List<object[]>();
            List<object[]> list_盤點單號_delete = new List<object[]>();
            if (returnData.Data.Count == 0)
            {
                returnData.Code = -1;
                returnData.Result = "輸入Data長度錯誤!";
            }
          
            for (int i = 0; i < returnData.Data.Count; i++)
            {
                inventoryClass.creat_OUT inventory_Creat_OUT = inventoryClass.creat_OUT.ObjToData(returnData.Data[i]);
                string GUID = inventory_Creat_OUT.GUID;
                list_盤點單號_buf = list_盤點單號.GetRows((int)enum_盤點單號.GUID, GUID);
                if(list_盤點單號_buf.Count > 0)
                {
                    list_盤點單號_delete.Add(list_盤點單號_buf[0]);
                }
            }
            sQLControl_inventory.DeleteExtra(null, list_盤點單號_delete);

   
            string str_Get_creat = this.Get_creat();
            return str_Get_creat;
        }
        private class ICP_inventory_Creat_OUT : IComparer<object>
        {
            public int Compare(object x, object y)
            {
                return ((inventoryClass.creat_OUT)y).建表時間.CompareTo(((inventoryClass.creat_OUT)x).建表時間);
            }
        }

    }
}
