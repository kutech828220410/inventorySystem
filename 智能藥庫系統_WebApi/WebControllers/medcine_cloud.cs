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

namespace HIS_WebApi
{
    enum enum_medicine_page_cloud
    {
        GUID,
        藥品碼,
        料號,
        中文名稱,
        藥品名稱,
        藥品學名,
        健保碼,
        包裝單位,
        包裝數量,
        最小包裝單位,
        最小包裝數量,
        藥品條碼1,
        藥品條碼2,
        警訊藥品,
        管制級別,
        類別
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MED_page_cloudController : Controller
    {
        static private string DataBaseName = ConfigurationManager.AppSettings["medicine_page_cloud_database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["medicine_page_cloud_IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        private SQLControl sQLControl_medicine_page_cloud = new SQLControl(IP, DataBaseName, "medicine_page_cloud", UserName, Password, Port, SSLMode);

       
        public class class_medicine_page_firstclass_data
        {
            public string GUID { get; set; }
            public string 藥品碼 { get; set; }
            public string 料號 { get; set; }
            public string 中文名稱 { get; set; }
            public string 藥品名稱 { get; set; }
            public string 藥品學名 { get; set; }
            public string 健保碼 { get; set; }
            public string 包裝單位 { get; set; }
            public string 包裝數量 { get; set; }
            public string 最小包裝單位 { get; set; }
            public string 最小包裝數量 { get; set; }
            public string 藥品條碼1 { get; set; }
            public string 藥品條碼2 { get; set; }
            public string 警訊藥品 { get; set; }
            public string 管制級別 { get; set; }
            public string 類別 { get; set; }
        }

        [HttpGet]
        public string Get()
        {
            List<object[]> list_MED_cloud = this.sQLControl_medicine_page_cloud.GetAllRows(null);
            List<class_medicine_page_firstclass_data> class_Medicine_Page_Firstclass_Datas = new List<class_medicine_page_firstclass_data>();
            for (int i = 0; i < list_MED_cloud.Count; i++)
            {
                class_medicine_page_firstclass_data class_Medicine_Page_Firstclass_Data = new class_medicine_page_firstclass_data();

                class_Medicine_Page_Firstclass_Data.GUID = list_MED_cloud[i][(int)enum_medicine_page_cloud.GUID].ObjectToString();
                class_Medicine_Page_Firstclass_Data.藥品碼 = list_MED_cloud[i][(int)enum_medicine_page_cloud.藥品碼].ObjectToString();
                class_Medicine_Page_Firstclass_Data.料號 = list_MED_cloud[i][(int)enum_medicine_page_cloud.料號].ObjectToString();
                class_Medicine_Page_Firstclass_Data.中文名稱 = list_MED_cloud[i][(int)enum_medicine_page_cloud.中文名稱].ObjectToString();
                class_Medicine_Page_Firstclass_Data.藥品名稱 = list_MED_cloud[i][(int)enum_medicine_page_cloud.藥品名稱].ObjectToString();
                class_Medicine_Page_Firstclass_Data.藥品學名 = list_MED_cloud[i][(int)enum_medicine_page_cloud.藥品學名].ObjectToString();
                class_Medicine_Page_Firstclass_Data.健保碼 = list_MED_cloud[i][(int)enum_medicine_page_cloud.健保碼].ObjectToString();
                class_Medicine_Page_Firstclass_Data.包裝單位 = list_MED_cloud[i][(int)enum_medicine_page_cloud.包裝單位].ObjectToString();
                class_Medicine_Page_Firstclass_Data.包裝數量 = list_MED_cloud[i][(int)enum_medicine_page_cloud.包裝數量].ObjectToString();
                class_Medicine_Page_Firstclass_Data.最小包裝單位 = list_MED_cloud[i][(int)enum_medicine_page_cloud.最小包裝單位].ObjectToString();
                class_Medicine_Page_Firstclass_Data.最小包裝數量 = list_MED_cloud[i][(int)enum_medicine_page_cloud.最小包裝數量].ObjectToString();
                class_Medicine_Page_Firstclass_Data.藥品條碼1 = list_MED_cloud[i][(int)enum_medicine_page_cloud.藥品條碼1].ObjectToString();
                class_Medicine_Page_Firstclass_Data.藥品條碼2 = list_MED_cloud[i][(int)enum_medicine_page_cloud.藥品條碼2].ObjectToString();
                class_Medicine_Page_Firstclass_Data.警訊藥品 = list_MED_cloud[i][(int)enum_medicine_page_cloud.警訊藥品].ObjectToString();
                class_Medicine_Page_Firstclass_Data.管制級別 = list_MED_cloud[i][(int)enum_medicine_page_cloud.管制級別].ObjectToString();
                class_Medicine_Page_Firstclass_Data.類別 = list_MED_cloud[i][(int)enum_medicine_page_cloud.類別].ObjectToString();
                class_Medicine_Page_Firstclass_Datas.Add(class_Medicine_Page_Firstclass_Data);
            }
            return class_Medicine_Page_Firstclass_Datas.JsonSerializationt();

        }
    }
}
