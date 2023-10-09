using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using HIS_DB_Lib;
namespace 勤務傳送櫃
{
    public partial class Form1 : Form
    {
        public string Function_藥品碼檢查(string Code)
        {

            return Code;
        }
        public List<medClass> Function_搜尋Barcode(string barcode)
        {
            string url = $"{dBConfigClass.Api_URL}/api/MED_page/serch_by_BarCode";
            returnData returnData = new returnData(url);
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page_cloud";
            returnData.Value = barcode;
            string json = returnData.ApiPostJson();
            if (returnData.ResultData == null)
            {
                MyMessageBox.ShowDialog("API連結失敗,請檢查網路或設定!");
                return new List<medClass>();
            }
            List<medClass> medClasses = returnData.ResultData.Data.ObjToListClass<medClass>();
            return medClasses;
        }
    }
}
