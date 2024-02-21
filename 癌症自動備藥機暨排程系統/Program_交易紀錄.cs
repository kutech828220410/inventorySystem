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
            this.sqL_DataGridView_交易記錄查詢.顯示首列 = false;
            this.sqL_DataGridView_交易記錄查詢.顯示首行 = false;
            this.sqL_DataGridView_交易記錄查詢.Init(table);
            
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnVisible(false, new enum_交易記錄查詢資料().GetEnumNames());
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(sqL_DataGridView_交易記錄查詢.Width - 20, DataGridViewContentAlignment.MiddleLeft, "GUID");

            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.動作);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.診別);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品碼);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品名稱);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.庫存量);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.交易量);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.結存量);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作人);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病人姓名);
            ////this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.頻次);
            ////this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.床號);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病歷號);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作時間);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.備註);

            this.sqL_DataGridView_交易記錄查詢.RowPostPaintingEvent += SqL_DataGridView_交易記錄查詢_RowPostPaintingEvent;

            this.plC_RJ_Button_交易紀錄查詢_操作時間_搜尋.MouseDownEvent += PlC_RJ_Button_交易紀錄查詢_操作時間_搜尋_MouseDownEvent;

            plC_UI_Init.Add_Method(Program_交易紀錄);
        }

       

        private void Program_交易紀錄()
        {
            
        }

        #region Event
        private void SqL_DataGridView_交易記錄查詢_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            Color row_Backcolor = Color.White;
            Color row_Forecolor = Color.Black;
            using (Brush brush = new SolidBrush(row_Backcolor))
            {
                int x = e.RowBounds.Left;
                int y = e.RowBounds.Top;
                int width = e.RowBounds.Width;
                int height = e.RowBounds.Height;
                e.Graphics.FillRectangle(brush, e.RowBounds);
                DrawingClass.Draw.DrawRoundShadow(e.Graphics, new RectangleF(x - 1, y - 1, width, height), Color.DarkGray, 5, 5);
                int tempX = 0;
                Size size = new Size();
                PointF pointF = new PointF();
                object[] value = this.sqL_DataGridView_交易記錄查詢.GetRowsList()[e.RowIndex];

                string 序號 = $"{e.RowIndex + 1}.";
                string 動作 = $"{value[(int)enum_交易記錄查詢資料.動作]}";
                string 藥碼 = $"({value[(int)enum_交易記錄查詢資料.藥品碼]})";
                string 藥名 = $"{value[(int)enum_交易記錄查詢資料.藥品名稱]}";
                string 庫存量 = $"庫:{value[(int)enum_交易記錄查詢資料.庫存量]}";
                string 交易量 = $"交:{value[(int)enum_交易記錄查詢資料.交易量]}";
                string 結存量 = $"結:{value[(int)enum_交易記錄查詢資料.結存量]}";

                string 備註 = $"{value[(int)enum_交易記錄查詢資料.備註]}";


                string 操作人 = $"{value[(int)enum_交易記錄查詢資料.操作人]}";
                string 操作時間 = $"{value[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString()}";
                if (操作時間.StringIsEmpty()) 操作時間 = $"{value[(int)enum_交易記錄查詢資料.操作時間].ObjectToString()}";
                string 病人姓名 = $"{value[(int)enum_交易記錄查詢資料.病人姓名]}";
                string 病歷號 = $"{value[(int)enum_交易記錄查詢資料.病歷號]}";
                string 病人資訊 = $"{病人姓名}[{病歷號}]";
                if (病歷號.StringIsEmpty()) 病人資訊 = "";
                string 操作資訊 = $"{操作人} {操作時間}";
                DrawingClass.Draw.文字左上繪製(序號, new PointF(10, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);
                DrawingClass.Draw.文字左上繪製(動作, new PointF(50, y + 10), new Font("標楷體", 14, FontStyle.Regular), Color.Black, e.Graphics);
                DrawingClass.Draw.文字左上繪製(藥碼, new PointF(150, y + 10), new Font("標楷體", 14, FontStyle.Regular), Color.Black, e.Graphics);
                size = 藥碼.MeasureText(new Font("標楷體", 14, FontStyle.Bold));
                DrawingClass.Draw.文字左上繪製(藥名, new PointF(150 + size.Width, y + 10), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                size = 備註.MeasureText(new Font("標楷體", 12, FontStyle.Bold));
                DrawingClass.Draw.文字左上繪製(備註, new PointF(e.RowBounds.Width - size.Width - 20, y + 10), new Font("標楷體", 12, FontStyle.Regular), Color.Black, e.Graphics);



                DrawingClass.Draw.文字左上繪製(庫存量, new PointF(20, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                if(value[(int)enum_交易記錄查詢資料.交易量].StringToInt32() > 0)
                {
                    DrawingClass.Draw.文字左上繪製(交易量, new PointF(150, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Green, e.Graphics);
                }
                else if (value[(int)enum_交易記錄查詢資料.交易量].StringToInt32() == 0)
                {
                    DrawingClass.Draw.文字左上繪製(交易量, new PointF(150, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                }
                else
                {
                    DrawingClass.Draw.文字左上繪製(交易量, new PointF(150, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Red, e.Graphics);
                }
                DrawingClass.Draw.文字左上繪製(結存量, new PointF(280, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);

                DrawingClass.Draw.文字左上繪製(病人資訊, new PointF(320, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                size = 操作資訊.MeasureText(new Font("標楷體", 12, FontStyle.Bold));
                DrawingClass.Draw.文字左上繪製(操作資訊, new PointF(e.RowBounds.Width - size.Width - 20, y + 50), new Font("標楷體", 12, FontStyle.Italic), Color.Black, e.Graphics);

            }
        }
        private void PlC_RJ_Button_交易紀錄查詢_操作時間_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            string url = $"{API_Server}/api/transactions/get_by_op_time_st_end";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.Value = $"{dateTimeIntervelPicker_交易紀錄查詢_操作時間.StartTime.ToDateTimeString()},{dateTimeIntervelPicker_交易紀錄查詢_操作時間.EndTime.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData returnData_result = json.JsonDeserializet<returnData>();
            List<transactionsClass> transactionsClasses = returnData_result.Data.ObjToClass<List<transactionsClass>>();
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass,enum_交易記錄查詢資料>();
            this.sqL_DataGridView_交易記錄查詢.RefreshGrid(list_value);
        }
        #endregion
    }
}
