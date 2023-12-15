using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;

namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        public void Program_系統_Init()
        {
            string url = $"{API_Server}/api/CPMP_StorageConfig/init";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);

            Table table = json.JsonDeserializet<Table>();
            this.sqL_DataGridView_馬達輸出索引表.Init(table);
            this.sqL_DataGridView_馬達輸出索引表.Server = table.Server;
            this.sqL_DataGridView_馬達輸出索引表.DataBaseName = table.DBName;
            this.sqL_DataGridView_馬達輸出索引表.UserName = table.Username;
            this.sqL_DataGridView_馬達輸出索引表.Password = table.Password;
            this.sqL_DataGridView_馬達輸出索引表.Port = table.Port.StringToUInt32();
            this.sqL_DataGridView_馬達輸出索引表.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;


            this.storageUI_EPD_266.Init(dBConfigClass.DB_Basic);


            this.plC_UI_Init.Add_Method(Program_系統);
        }
        public void Program_系統()
        {

        }
    }
}
