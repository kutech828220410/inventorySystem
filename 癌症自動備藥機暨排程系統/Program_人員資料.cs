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

namespace 癌症備藥機
{
    public partial class Main_Form : Form
    {
        public static string 人員資料_UID = "";
        private void Program_人員資料_Init()
        {
            string url = $"{API_Server}/api/person_page/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"人員資料表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }
            this.sqL_DataGridView_人員資料.Server = table.Server;
            this.sqL_DataGridView_人員資料.DataBaseName = table.DBName;
            this.sqL_DataGridView_人員資料.UserName = table.Username;
            this.sqL_DataGridView_人員資料.Password = table.Password;
            this.sqL_DataGridView_人員資料.Port = table.Port.StringToUInt32();
            this.sqL_DataGridView_人員資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_人員資料.顯示首列 = false;
            this.sqL_DataGridView_人員資料.顯示首行 = false;
            this.sqL_DataGridView_人員資料.RowsHeight = 60;
            this.sqL_DataGridView_人員資料.Init(table);
            this.sqL_DataGridView_人員資料.Set_ColumnVisible(false, new enum_人員資料().GetEnumNames());
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(sqL_DataGridView_人員資料.Width - 20, DataGridViewContentAlignment.MiddleLeft, "GUID");
            //this.sqL_DataGridView_人員資料.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.ID);
            //this.sqL_DataGridView_人員資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.姓名);
            //this.sqL_DataGridView_人員資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_人員資料.性別);
            //this.sqL_DataGridView_人員資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_人員資料.權限等級);
            //this.sqL_DataGridView_人員資料.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.卡號);
            //this.sqL_DataGridView_人員資料.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.一維條碼);
            this.sqL_DataGridView_人員資料.MouseDown += SqL_DataGridView_人員資料_MouseDown;
            this.sqL_DataGridView_人員資料.RowDoubleClickEvent += SqL_DataGridView_人員資料_RowDoubleClickEvent;
            this.sqL_DataGridView_人員資料.RowPostPaintingEvent += SqL_DataGridView_人員資料_RowPostPaintingEvent;

            this.plC_RJ_Button_人員資料_資料搜尋_姓名.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_姓名_MouseDownEvent;
            this.plC_RJ_Button_人員資料_資料搜尋_ID.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_ID_MouseDownEvent;
            this.plC_RJ_Button_人員資料_資料搜尋_卡號.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_卡號_MouseDownEvent;
            this.plC_RJ_Button_人員資料_資料搜尋_一維條碼.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_一維條碼_MouseDownEvent;
            this.plC_RJ_Button_人員資料_存檔.MouseDownEvent += PlC_RJ_Button_人員資料_存檔_MouseDownEvent;
            this.plC_RJ_Button_人員資料_顯示全部.MouseDownEvent += PlC_RJ_Button_人員資料_顯示全部_MouseDownEvent;
            this.plC_RJ_Button_人員資料_刪除.MouseDownEvent += PlC_RJ_Button_人員資料_刪除_MouseDownEvent;
            this.plC_RJ_Button_人員資料_卡號建置.MouseDownEventEx += PlC_RJ_Button_人員資料_卡號建置_MouseDownEventEx;

            plC_UI_Init.Add_Method(Program_人員資料);
        }

      

        private void Program_人員資料()
        {

           
        }

        #region Function

        #endregion
        #region Event
        private void SqL_DataGridView_人員資料_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            Color row_Backcolor = Color.White;
    
            Color row_Forecolor = Color.Black;

            if (this.sqL_DataGridView_人員資料.GetSelectRow() == e.RowIndex)
            {
                row_Backcolor = this.sqL_DataGridView_人員資料.selectedRowBackColor;
                row_Forecolor = this.sqL_DataGridView_人員資料.selectedRowForeColor;
            }
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
                object[] value = this.sqL_DataGridView_人員資料.GetRowsList()[e.RowIndex];

                string 序號 = $"{e.RowIndex + 1}.";
                string 姓名 = $"{value[(int)enum_人員資料.姓名]}";
                string ID = $"{value[(int)enum_人員資料.ID]}";
                string 權限等級 = $"權限等級:{value[(int)enum_人員資料.權限等級]}";
                string 卡號 = $"卡號:{(value[(int)enum_人員資料.卡號].ObjectToString().StringIsEmpty() ? "未註冊" : $"{value[(int)enum_人員資料.卡號]}")}";



                DrawingClass.Draw.文字左上繪製(序號, new PointF(10, y + 10), new Font("標楷體", 14), row_Forecolor, e.Graphics);
                DrawingClass.Draw.文字左上繪製(姓名, new PointF(50, y + 10), new Font("標楷體", 14, FontStyle.Regular), row_Forecolor, e.Graphics);
                DrawingClass.Draw.文字左上繪製(ID, new PointF(150, y + 10), new Font("標楷體", 14, FontStyle.Regular), row_Forecolor, e.Graphics);
                DrawingClass.Draw.文字左上繪製(權限等級, new PointF(300, y + 10), new Font("標楷體", 14, FontStyle.Regular), row_Forecolor, e.Graphics);
                DrawingClass.Draw.文字左上繪製(卡號, new PointF(450, y + 10), new Font("標楷體", 14, FontStyle.Regular), row_Forecolor, e.Graphics);

            }
        }
        private void SqL_DataGridView_人員資料_MouseDown(object sender, MouseEventArgs e)
        {
            //this.sqL_DataGridView_人員資料.SQL_GetAllRows(true);
        }
        private void PlC_RJ_Button_人員資料_卡號建置_MouseDownEventEx(RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            try
            {
                if (rJ_TextBox_人員資料_ID.Text.StringIsEmpty())
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選擇人員", 1500, 0, 0);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                人員資料_UID = "";
                Dialog_等待RFID感應 dialog_等待RFID感應 = new Dialog_等待RFID感應(this.rfiD_FX600_UI);
                if (dialog_等待RFID感應.ShowDialog() != DialogResult.Yes) return;
                string UID = dialog_等待RFID感應.Value;
                List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetRows((int)enum_人員資料.ID, rJ_TextBox_人員資料_ID.Text, false);
                if (list_value.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找無人員資料", 1500, 0, 0);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                list_value[0][(int)enum_人員資料.卡號] = UID;
                rJ_TextBox_人員資料_卡號.Text = UID;
                this.sqL_DataGridView_人員資料.SQL_ReplaceExtra(list_value[0], false);
                this.sqL_DataGridView_人員資料.ReplaceExtra(list_value[0], true);
                Dialog_AlarmForm _dialog_AlarmForm = new Dialog_AlarmForm("成功", 1500, 0, 0, Color.Green);
                _dialog_AlarmForm.ShowDialog();

            }
            finally
            {
            
            }
        }
        private void PlC_RJ_Button_人員資料_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_人員資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500, 0, -0);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (MyMessageBox.ShowDialog("是否刪除選取資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            LoadingForm.ShowLoadingForm();


            this.sqL_DataGridView_人員資料.SQL_DeleteExtra(list_value, false);
            this.sqL_DataGridView_人員資料.DeleteExtra(list_value, true);

            System.Threading.Thread.Sleep(1000);
            LoadingForm.CloseLoadingForm();
        }
        private void PlC_RJ_Button_人員資料_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            this.sqL_DataGridView_人員資料.SQL_GetAllRows(true);
        }
        private void SqL_DataGridView_人員資料_RowDoubleClickEvent(object[] RowValue)
        {
            string 姓名 = RowValue[(int)enum_人員資料.姓名].ObjectToString();
            string ID = RowValue[(int)enum_人員資料.ID].ObjectToString();
            string 卡號 = RowValue[(int)enum_人員資料.卡號].ObjectToString();
            string 一維條碼 = RowValue[(int)enum_人員資料.一維條碼].ObjectToString();
            string 權限等級 = RowValue[(int)enum_人員資料.權限等級].ObjectToString();

            this.rJ_TextBox_人員資料_姓名.Texts = 姓名;
            this.rJ_TextBox_人員資料_ID.Texts = ID;
            this.rJ_TextBox_人員資料_卡號.Texts = 卡號;
            this.rJ_TextBox_人員資料_一維條碼.Texts = 一維條碼;
            this.plC_ComboBox_人員資料_權限等級.Text = 權限等級;
        }
        private void PlC_RJ_Button_人員資料_資料搜尋_一維條碼_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.rJ_TextBox_人員資料_資料搜尋_一維條碼.Texts;
            if(text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500 , 0 , -250);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_value_buf = list_value.GetRows((int)enum_人員資料.一維條碼, text);
            this.sqL_DataGridView_人員資料.RefreshGrid(list_value_buf);            
        }
        private void PlC_RJ_Button_人員資料_資料搜尋_卡號_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.rJ_TextBox_人員資料_資料搜尋_卡號.Texts;
            if (text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500, 0, -250);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_value_buf = list_value.GetRows((int)enum_人員資料.卡號, text);
            this.sqL_DataGridView_人員資料.RefreshGrid(list_value_buf);
        }
        private void PlC_RJ_Button_人員資料_資料搜尋_ID_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.rJ_TextBox_人員資料_資料搜尋_ID.Texts;
            if (text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500, 0, -250);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_value_buf = list_value.GetRows((int)enum_人員資料.ID, text);
            this.sqL_DataGridView_人員資料.RefreshGrid(list_value_buf);
        }
        private void PlC_RJ_Button_人員資料_資料搜尋_姓名_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.rJ_TextBox_人員資料_資料搜尋_姓名.Texts;
            if (text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500, 0, -250);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_value_buf = list_value.GetRows((int)enum_人員資料.姓名, text);
            this.sqL_DataGridView_人員資料.RefreshGrid(list_value_buf);
        }
        private void PlC_RJ_Button_人員資料_存檔_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                string 姓名 = this.rJ_TextBox_人員資料_姓名.Texts;
                string ID = this.rJ_TextBox_人員資料_ID.Texts;
                string 卡號 = this.rJ_TextBox_人員資料_卡號.Texts;
                string 一維條碼 = this.rJ_TextBox_人員資料_一維條碼.Texts;
                string 權限等級 = this.plC_ComboBox_人員資料_權限等級.Text;
                List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
                List<object[]> list_value_buf = list_value.GetRows((int)enum_人員資料.ID, ID);
                if (list_value_buf.Count == 0)
                {
                    object[] value = new object[new enum_人員資料().GetLength()];
                    value[(int)enum_人員資料.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_人員資料.姓名] = 姓名;
                    value[(int)enum_人員資料.ID] = ID;
                    value[(int)enum_人員資料.卡號] = 卡號;
                    value[(int)enum_人員資料.一維條碼] = 一維條碼;
                    value[(int)enum_人員資料.權限等級] = 權限等級;
                    sqL_DataGridView_人員資料.SQL_AddRow(value, false);
                    sqL_DataGridView_人員資料.AddRow(value, true);
                }
                else
                {
                    object[] value = list_value_buf[0];
                    value[(int)enum_人員資料.姓名] = 姓名;
                    value[(int)enum_人員資料.ID] = ID;
                    value[(int)enum_人員資料.卡號] = 卡號;
                    value[(int)enum_人員資料.一維條碼] = 一維條碼;
                    value[(int)enum_人員資料.權限等級] = 權限等級;
                    sqL_DataGridView_人員資料.SQL_ReplaceExtra(value, false);
                    sqL_DataGridView_人員資料.ReplaceExtra(value, true);
                }
            }));

        }
        #endregion
    }
}
