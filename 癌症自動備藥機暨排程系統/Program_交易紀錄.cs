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
namespace 癌症備藥機
{
    public partial class Main_Form : Form
    {
        private void Program_交易紀錄_Init()
        {
            string url = $"{API_Server}/api/transactions/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_sys_serverSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"交易紀錄表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_交易記錄查詢, dBConfigClass.DB_Basic);

            dateTimeIntervelPicker_交易紀錄查詢_操作時間.SetDateTime(DateTime.Now.GetStartDate(), DateTime.Now.GetEndDate());

            this.sqL_DataGridView_交易記錄查詢.顯示首列 = true;
            this.sqL_DataGridView_交易記錄查詢.顯示首行 = true;
            this.sqL_DataGridView_交易記錄查詢.Init(table);
            
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnVisible(false, new enum_交易記錄查詢資料().GetEnumNames());
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(sqL_DataGridView_交易記錄查詢.Width - 20, DataGridViewContentAlignment.MiddleLeft, "GUID");

            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.動作);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.診別);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品碼);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品名稱);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.庫存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.交易量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.結存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作人);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病人姓名);
            ////this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.頻次);
            ////this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.床號);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病歷號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作時間);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.備註);

            //this.sqL_DataGridView_交易記錄查詢.RowPostPaintingEvent += SqL_DataGridView_交易記錄查詢_RowPostPaintingEvent;
            this.sqL_DataGridView_交易記錄查詢.DataGridRowsChangeRefEvent += SqL_DataGridView_交易記錄查詢_DataGridRowsChangeRefEvent;


            this.plC_RJ_Button_交易紀錄查詢_操作時間_搜尋.MouseDownEvent += PlC_RJ_Button_交易紀錄查詢_操作時間_搜尋_MouseDownEvent;
            this.plC_RJ_Button_交易紀錄查詢_藥碼搜尋.MouseDownEvent += PlC_RJ_Button_交易紀錄查詢_藥碼搜尋_MouseDownEvent;
            this.plC_RJ_Button_交易紀錄查詢_藥名搜尋.MouseDownEvent += PlC_RJ_Button_交易紀錄查詢_藥名搜尋_MouseDownEvent;

            this.plC_RJ_Button_交易紀錄查詢_匯出.MouseDownEvent += PlC_RJ_Button_交易紀錄查詢_匯出_MouseDownEvent;
            plC_UI_Init.Add_Method(Program_交易紀錄);
        }

      

        private void Program_交易紀錄()
        {
            
        }


        #region Function
        private void Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作 enum_交易記錄查詢動作, string 操作人, string 備註)
        {
            if (操作人.StringIsEmpty()) return;

            transactionsClass transactionsClass = new transactionsClass();
            transactionsClass.GUID = Guid.NewGuid().ToString();
            transactionsClass.動作 = enum_交易記錄查詢動作.GetEnumName();
            transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
            transactionsClass.開方時間 = DateTime.Now.ToDateTimeString_6();
            string url = $"{Main_Form.API_Server}/api/transactions/add";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Data = transactionsClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);

         
        }
        #endregion
        #region Event
        private void SqL_DataGridView_交易記錄查詢_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            List<object[]> list_value_buf = new List<object[]>();
            List<object[]> list_temp = new List<object[]>();
            if (this.checkBox_交易紀錄_領藥.Checked)
            {
                list_temp = (from temp in RowsList
                             where temp[(int)enum_交易記錄查詢資料.動作].ObjectToString().Contains("領藥")
                             select temp).ToList();

                list_value_buf.LockAdd(list_temp);
            }
            if (this.checkBox_交易紀錄_入庫.Checked)
            {
                list_temp = (from temp in RowsList
                             where temp[(int)enum_交易記錄查詢資料.動作].ObjectToString().Contains("入庫")
                             select temp).ToList();
                list_value_buf.LockAdd(list_temp);
            }
            if (this.checkBox_交易紀錄_庫存修正.Checked)
            {
                list_temp = (from temp in RowsList
                             where temp[(int)enum_交易記錄查詢資料.動作].ObjectToString().Contains("修正")
                             select temp).ToList();
                list_value_buf.LockAdd(list_temp);
            }
            if (this.checkBox_交易紀錄_一般操作.Checked)
            {
                list_temp = (from temp in RowsList
                             where temp[(int)enum_交易記錄查詢資料.動作].ObjectToString().Contains("登入")
                             where temp[(int)enum_交易記錄查詢資料.動作].ObjectToString().Contains("登出")
                             select temp).ToList();
                list_value_buf.LockAdd(list_temp);
            }
            list_value_buf.Sort(new ICP_交易記錄查詢());
            RowsList = list_value_buf;

        
        }
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
            returnData.ServerType = enum_sys_serverSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.Value = $"{dateTimeIntervelPicker_交易紀錄查詢_操作時間.StartTime.ToDateTimeString()},{dateTimeIntervelPicker_交易紀錄查詢_操作時間.EndTime.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData returnData_result = json.JsonDeserializet<returnData>();
            List<transactionsClass> transactionsClasses = returnData_result.Data.ObjToClass<List<transactionsClass>>();
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass,enum_交易記錄查詢資料>();
            this.sqL_DataGridView_交易記錄查詢.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_交易紀錄查詢_藥名搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if(rJ_TextBox_交易紀錄查詢_藥名搜尋.Texts.StringIsEmpty() == true)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未輸入藥名", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string url = $"{API_Server}/api/transactions/get_by_name";
            returnData returnData = new returnData();
            returnData.ServerType = enum_sys_serverSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.Value = $"{rJ_TextBox_交易紀錄查詢_藥名搜尋.Texts}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData returnData_result = json.JsonDeserializet<returnData>();
            List<transactionsClass> transactionsClasses = returnData_result.Data.ObjToClass<List<transactionsClass>>();
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
            this.sqL_DataGridView_交易記錄查詢.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_交易紀錄查詢_藥碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_交易紀錄查詢_藥碼搜尋.Texts.StringIsEmpty() == true)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未輸入藥碼", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string url = $"{API_Server}/api/transactions/get_by_code";
            returnData returnData = new returnData();
            returnData.ServerType = enum_sys_serverSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.Value = $"{rJ_TextBox_交易紀錄查詢_藥碼搜尋.Texts}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData returnData_result = json.JsonDeserializet<returnData>();
            List<transactionsClass> transactionsClasses = returnData_result.Data.ObjToClass<List<transactionsClass>>();
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
            this.sqL_DataGridView_交易記錄查詢.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_交易紀錄查詢_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Dialog_AlarmForm dialog_AlarmForm;
                if (saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;
                List<object[]> list_value = this.sqL_DataGridView_交易記錄查詢.GetAllRows();
                if (list_value.Count == 0)
                {
                    dialog_AlarmForm = new Dialog_AlarmForm("無資料可匯出", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                DataTable dataTable = list_value.ToDataTable(new enum_交易記錄查詢資料());
                MyOffice.ExcelClass.NPOI_SaveFile(dataTable, this.saveFileDialog_SaveExcel.FileName);
                dialog_AlarmForm = new Dialog_AlarmForm("匯出成功", 1500, Color.Green);
                dialog_AlarmForm.ShowDialog();
            }));
          

        }
        #endregion

        public class ICP_交易記錄查詢 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_交易記錄查詢資料.操作時間].StringToDateTime();
                DateTime datetime2 = y[(int)enum_交易記錄查詢資料.操作時間].StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                if (compare != 0) return compare;
                int 結存量1 = x[(int)enum_交易記錄查詢資料.結存量].StringToInt32();
                int 結存量2 = y[(int)enum_交易記錄查詢資料.結存量].StringToInt32();
                if (結存量1 > 結存量2)
                {
                    return -1;
                }
                else if (結存量1 < 結存量2)
                {
                    return 1;
                }
                else if (結存量1 == 結存量2) return 0;
                return 0;

            }
        }
    }
}
