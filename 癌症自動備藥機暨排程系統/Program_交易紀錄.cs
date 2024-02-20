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
using DrawingClass;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Program_交易紀錄_Init()
        {
            string url = $"{API_Server}/api/transactions/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"交易紀錄表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }
            this.sqL_DataGridView_交易記錄查詢.Init(table);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnVisible(false, new enum_交易記錄查詢資料().GetEnumNames());
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.動作);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.診別);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品碼);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品名稱);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.庫存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.交易量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.結存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作人);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病人姓名);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.頻次);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.床號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病歷號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作時間);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.備註);

            plC_UI_Init.Add_Method(Program_交易紀錄);
        }
        private void Program_交易紀錄()
        {

        }
    }
}
