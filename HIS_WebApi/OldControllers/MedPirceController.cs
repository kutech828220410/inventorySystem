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
using System.ComponentModel;
using System.Reflection;
using System.Configuration;
using IBM.Data.DB2.Core;
using MyOffice;
using NPOI;
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedPriceController : ControllerBase
    {

        /// <summary>
        /// (屏榮)取得藥品採購資料
        /// </summary>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpGet]
        public string Get()
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
            List<medPriceClass> class_MedPrices = new List<medPriceClass>();
            while (reader.Read())
            {
                medPriceClass class_MedPrice = new medPriceClass();
                class_MedPrice.藥品碼 = reader["UDDRGNO"].ToString();
                class_MedPrice.售價 = reader["UDPRICE"].ToString();
                class_MedPrice.成本價 = reader["UDWCOST"].ToString();
                class_MedPrice.最近一次售價 = reader["UDOLDPRI"].ToString();
                class_MedPrice.最近一次成本價 = reader["UDOLDWCO"].ToString();
                class_MedPrices.Add(class_MedPrice);
            }
            reader.Close();
           
            return class_MedPrices.JsonSerializationt();
        }
    }
}
