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
        private void Function_取得本地儲位()
        {
            this.List_本地儲位 = storageUI_EPD_266.SQL_GetAllStorage();
        }
        private List<object[]> Function_取得備藥通知(DateTime dt_start, DateTime dt_end)
        {
            string url = $"{API_Server}/api/ChemotherapyRxScheduling/get_udnoectc_by_ctdate_st_end";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = $"{dt_start.ToDateString()} 00:00:00,{dt_end.ToDateString()} 23:59:59";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json.JsonDeserializet<returnData>();

            List<udnoectc> udnoectcs = returnData.Data.ObjToListClass<udnoectc>();
            List<object[]> list_udnoectc = udnoectcs.ClassToSQL<udnoectc, enum_udnoectc>();
            return list_udnoectc;
        }

    }
}
