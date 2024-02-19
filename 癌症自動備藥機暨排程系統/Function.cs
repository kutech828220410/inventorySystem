using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using H_Pannel_lib;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private List<Storage> Function_取得本地儲位()
        {
            return this.List_本地儲位 = storageUI_EPD_266.SQL_GetAllStorage();
        }
        private List<medClass> Function_取得藥檔資料()
        {
            string url = $"{API_Server}/api/MED_page/get_by_apiserver";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData returnData_result = json.JsonDeserializet<returnData>();
            List<medClass> medClasses = returnData_result.Data.ObjToClass<List<medClass>>();

            return medClasses;
        }
        private List<medClass> Function_取得有儲位藥檔資料()
        {
            List<medClass> medClasses = Function_取得藥檔資料();
            List<medClass> medClasses_buf = new List<medClass>();
            List<medClass> medClasses_result = new List<medClass>();
            List<Storage> storages = Function_取得本地儲位();
            for (int i = 0; i < storages.Count; i++)
            {
                medClasses_buf = (from temp in medClasses
                                  where temp.藥品碼 == storages[i].Code
                                  select temp).ToList();
                if(medClasses_buf.Count > 0)
                {
                    medClasses_result.Add(medClasses_buf[0]);
                }
            }

            return medClasses_result;
        }
    }
}
