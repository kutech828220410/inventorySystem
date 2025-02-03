using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        private void Program_交易記錄查詢_結存量_Init()
        {
            Table table = new Table(new enum_consumption());
            this.sqL_DataGridView_交易紀錄_結存量.Init(table);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnVisible(false, new enum_consumption().GetEnumNames());
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_consumption.藥品碼);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_consumption.藥品名稱);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_consumption.交易量);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_consumption.庫存量);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_consumption.結存量);

            this.plC_RJ_Button_交易紀錄_結存量_顯示全部.MouseDownEvent += PlC_RJ_Button_交易紀錄_結存量_顯示全部_MouseDownEvent;
            this.plC_RJ_Button_交易紀錄_結存量_匯出資料.MouseDownEvent += PlC_RJ_Button_交易紀錄_結存量_匯出資料_MouseDownEvent;

        }

        private void PlC_RJ_Button_交易紀錄_結存量_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            DateTime dateTime_startTime = rJ_DatePicker_交易紀錄_結存量_操作時間_開始時間.Value;
            dateTime_startTime = new DateTime(dateTime_startTime.Year, dateTime_startTime.Month, dateTime_startTime.Day, 00, 00, 00);
            DateTime dateTime_endTime = rJ_DatePicker_交易紀錄_結存量_操作時間_結束時間.Value;
            dateTime_endTime = new DateTime(dateTime_endTime.Year, dateTime_endTime.Month, dateTime_endTime.Day, 23, 59, 59);

            returnData.Value = $"{dateTime_startTime.ToDateTimeString()},{dateTime_endTime.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt(true);
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/consumption/serch_by_ST_END", json_in);
            Console.WriteLine(json);
            returnData = json.JsonDeserializet<returnData>();

            List<consumptionClass> consumptionClasses = returnData.Data.ObjToListClass<consumptionClass>();
            List<object[]> list_consumption = consumptionClasses.ClassToSQL<consumptionClass, enum_consumption>();

            this.sqL_DataGridView_交易紀錄_結存量.RefreshGrid(list_consumption);

            if (list_consumption.Count == 0)
            {
                MyMessageBox.ShowDialog("查無資料!");
            }
        }
        private void PlC_RJ_Button_交易紀錄_結存量_匯出資料_MouseDownEvent(MouseEventArgs mevent)
        {
            DateTime dateTime_start = rJ_DatePicker_交易紀錄_結存量_操作時間_開始時間.Value;
            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 00, 00, 00);
            DateTime dateTime_end = rJ_DatePicker_交易紀錄_結存量_操作時間_結束時間.Value;
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);

            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            returnData.Value = $"{dateTime_start.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/consumption/serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/consumption/get_sheet_by_serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            string jsondata = returnData.Data.JsonSerializationt();
            List<SheetClass> sheetClass = jsondata.JsonDeserializet<List<SheetClass>>();
            if (sheetClass.Count == 0)
            {
                MyMessageBox.ShowDialog("無資料可匯出");
                return;
            }
            this.Invoke(new Action(delegate
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
                {
                    sheetClass.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);
                }
            }));
            MyMessageBox.ShowDialog("完成");

        }
    }
}
