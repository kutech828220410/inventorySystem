using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using System.Collections;

namespace 智能藥庫系統
{
    public partial class Form1 : Form
    {
        public enum enum_藥庫_驗收入庫_過帳明細
        {
            GUID,
            請購單號,
            藥品碼,
            料號,
            藥品名稱,
            包裝單位,
            應收數量,
            實收數量,
            驗收時間,
            入庫時間,
            請購時間,
            狀態,
            來源,
            備註,
        }
        public enum enum_驗收入庫效期批號
        {
            GUID,
            Master_GUID,
            請購單號,
            藥品碼,
            料號,
            包裝單位,
            效期,
            批號,
            數量,
            驗收時間,

        }
        public enum enum_藥庫_驗收入庫_過帳明細_狀態
        {
            等待過帳,
            庫存不足,
            未建立儲位,
            過帳完成,
            找無此藥品,
            無效期可入帳,
            忽略過帳,
        }

        private void sub_Program_藥庫_驗收入庫_過帳明細_Init()
        {
            this.sqL_DataGridView_驗收入庫資料.Init();
            
            this.sqL_DataGridView_驗收入庫資料.DataGridRefreshEvent += sqL_DataGridView_藥庫_過帳明細_驗收入庫明細_DataGridRefreshEvent;

            this.sqL_DataGridView_驗收內容.Init();
            if(this.sqL_DataGridView_驗收內容.SQL_IsTableCreat() ==false)
            {
                this.sqL_DataGridView_驗收內容.SQL_CreateTable();
            }
            else
            {
                this.sqL_DataGridView_驗收內容.SQL_CheckAllColumnName(true);
            }

            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_全部顯示.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_全部顯示_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料過帳.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料過帳_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料設定未過帳.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料設定未過帳_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料設定忽略未過帳.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料設定忽略未過帳_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料刪除.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料刪除_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_顯示異常過帳.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_顯示異常過帳_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_顯示等待過帳.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_顯示等待過帳_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_藥品碼篩選.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_藥品碼篩選_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_藥品名稱篩選.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_藥品名稱篩選_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_新增資料.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_新增資料_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_設定請購單號.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_設定請購單號_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_設定請購時間.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_設定請購時間_MouseDownEvent;
            this.plC_RJ_Button_藥庫_驗收入庫_過帳明細_設定驗收時間.MouseDownEvent += PlC_RJ_Button_藥庫_驗收入庫_過帳明細_設定驗收時間_MouseDownEvent;

            this.plC_UI_Init.Add_Method(sub_Program_藥庫_驗收入庫_過帳明細);
        }

       

        private bool flag_藥庫_驗收入庫_過帳明細 = false;
        private void sub_Program_藥庫_驗收入庫_過帳明細()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥庫" && this.plC_ScreenPage_藥庫.PageText == "驗收入庫" && this.plC_ScreenPage_藥庫_驗收入庫.PageText == "過帳明細")
            {
                if (!this.flag_藥庫_驗收入庫_過帳明細)
                {

                    this.flag_藥庫_驗收入庫_過帳明細 = true;
                }

            }
            else
            {
                this.flag_藥庫_驗收入庫_過帳明細 = false;
            }
        }

        #region Function
        private void Function_藥庫_驗收入庫_過帳明細_選取資料過帳(List<object[]> list_藥庫_驗收入庫_過帳明細)
        {
            List<object[]> list_交易紀錄_add = new List<object[]>();
            List<object[]> list_藥庫_驗收入庫_過帳明細_replace = new List<object[]>();
            List<object[]> list_補給驗收入庫_buf = new List<object[]>();
            List<object[]> list_補給驗收入庫_replace = new List<object[]>();

            List<DeviceBasic> deviceBasics = this.DeviceBasicClass_藥庫.SQL_GetAllDeviceBasic();
            List<DeviceBasic> deviceBasics_replace = new List<DeviceBasic>();
            Dialog_Prcessbar dialog_Prcessbar = new Dialog_Prcessbar(deviceBasics.Count);
            for (int i = 0; i < list_藥庫_驗收入庫_過帳明細.Count; i++)
            {
                if (list_藥庫_驗收入庫_過帳明細[i][(int)enum_藥庫_驗收入庫_過帳明細.狀態].ObjectToString() != enum_藥庫_驗收入庫_過帳明細_狀態.等待過帳.GetEnumName()) continue;
                string 藥品碼 = list_藥庫_驗收入庫_過帳明細[i][(int)enum_藥庫_驗收入庫_過帳明細.藥品碼].ObjectToString();
   
                string 實收數量 = list_藥庫_驗收入庫_過帳明細[i][(int)enum_藥庫_驗收入庫_過帳明細.實收數量].ObjectToString();

                object[] value = list_藥庫_驗收入庫_過帳明細[i];
                List<DeviceBasic> deviceBasics_buf = deviceBasics.SortByCode(藥品碼);

                if (deviceBasics_buf.Count > 0)
                {
                    string 庫存量 = deviceBasics_buf[0].Inventory.ToString();

                    deviceBasics_replace.Add(deviceBasics_buf[0]);
                    value[(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.過帳完成.GetEnumName();
                    list_藥庫_驗收入庫_過帳明細[i][(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.過帳完成.GetEnumName();
                    list_藥庫_驗收入庫_過帳明細_replace.Add(list_藥庫_驗收入庫_過帳明細[i]);



                    string GUID = Guid.NewGuid().ToString();
                    string 動作 = enum_交易記錄查詢動作.驗收入庫.GetEnumName();
                    string 藥品名稱 = list_藥庫_驗收入庫_過帳明細[i][(int)enum_藥庫_驗收入庫_過帳明細.藥品名稱].ObjectToString();
                    string 交易量 = 實收數量;
                    string 結存量 = deviceBasics_buf[0].Inventory.ToString();
                    string 操作人 = this.登入者名稱;
                    string 操作時間 = DateTime.Now.ToDateTimeString_6();
                    string 開方時間 = DateTime.Now.ToDateTimeString_6();
                    object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                    value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                    value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                    value_trading[(int)enum_交易記錄查詢資料.庫別] = enum_庫別.藥庫.GetEnumName();
                    value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                    value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                    value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                    value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                    value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                    value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                    value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;

                    list_交易紀錄_add.Add(value_trading);
                }
                else
                {
                    value[(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.未建立儲位.GetEnumName();
                    list_藥庫_驗收入庫_過帳明細[i][(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.未建立儲位.GetEnumName();
                    list_藥庫_驗收入庫_過帳明細_replace.Add(list_藥庫_驗收入庫_過帳明細[i]);
                }
                list_補給驗收入庫_replace.Add(value);
            }
            dialog_Prcessbar.State = "上傳儲位資料...";
            this.DeviceBasicClass_藥庫.SQL_ReplaceDeviceBasic(deviceBasics_replace);
            this.sqL_DataGridView_驗收入庫資料.SQL_ReplaceExtra(list_補給驗收入庫_replace, false);
            this.sqL_DataGridView_驗收入庫資料.ReplaceExtra(list_藥庫_驗收入庫_過帳明細_replace, true);
            this.sqL_DataGridView_交易記錄查詢.SQL_AddRows(list_交易紀錄_add, false);
            dialog_Prcessbar.Close();
        }
        #endregion
        #region Event
        private void sqL_DataGridView_藥庫_過帳明細_驗收入庫明細_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].Cells[enum_藥庫_驗收入庫_過帳明細.狀態.GetEnumName()].Value.ToString();
                if (狀態 == enum_藥庫_驗收入庫_過帳明細_狀態.過帳完成.GetEnumName())
                {
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                if (狀態 == enum_藥庫_驗收入庫_過帳明細_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                if (狀態 == enum_藥庫_驗收入庫_過帳明細_狀態.未建立儲位.GetEnumName())
                {
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                if (狀態 == enum_藥庫_驗收入庫_過帳明細_狀態.忽略過帳.GetEnumName())
                {
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.HotPink;
                    this.sqL_DataGridView_驗收入庫資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_設定請購時間_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            Dialog_寫入藥品碼 dialog_寫入藥品碼 = new Dialog_寫入藥品碼();
            DialogResult dialogResult = dialog_寫入藥品碼.ShowDialog();
            if (dialogResult != DialogResult.Yes) return;
            for (int i = 0; i < list_value.Count; i++)
            {
                list_value[i][(int)enum_藥庫_驗收入庫_過帳明細.請購時間] = dialog_寫入藥品碼.Value;
            }
           
            
            this.sqL_DataGridView_驗收入庫資料.SQL_ReplaceExtra(list_value, false);
            this.sqL_DataGridView_驗收入庫資料.ReplaceExtra(list_value, true);
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_設定驗收時間_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            Dialog_寫入藥品碼 dialog_寫入藥品碼 = new Dialog_寫入藥品碼();
            DialogResult dialogResult = dialog_寫入藥品碼.ShowDialog();
            if (dialogResult != DialogResult.Yes) return;
            for (int i = 0; i < list_value.Count; i++)
            {
                list_value[i][(int)enum_藥庫_驗收入庫_過帳明細.驗收時間] = dialog_寫入藥品碼.Value;
            }


            this.sqL_DataGridView_驗收入庫資料.SQL_ReplaceExtra(list_value, false);
            this.sqL_DataGridView_驗收入庫資料.ReplaceExtra(list_value, true);
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_設定請購單號_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            Dialog_寫入藥品碼 dialog_寫入藥品碼 = new Dialog_寫入藥品碼();
            DialogResult dialogResult = dialog_寫入藥品碼.ShowDialog();
            if (dialogResult != DialogResult.Yes) return;
            list_value[0][(int)enum_藥庫_驗收入庫_過帳明細.請購單號] = dialog_寫入藥品碼.Value;
            this.sqL_DataGridView_驗收入庫資料.SQL_ReplaceExtra(list_value, false);
            this.sqL_DataGridView_驗收入庫資料.ReplaceExtra(list_value, true);
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_新增資料_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_寫入藥品碼 dialog_寫入藥品碼 = new Dialog_寫入藥品碼();
            DialogResult dialogResult = dialog_寫入藥品碼.ShowDialog();
            if (dialogResult != DialogResult.Yes) return;
            string 藥品碼 = dialog_寫入藥品碼.Value;
            string 藥品名稱 = "";
            string 效期 = "";
            string 批號 = "";
            string 料號 = "";
            string 包裝單位 = "";
            string 應收數量 = "";
            string 實收數量 = "";
            if (藥品碼.StringIsEmpty()) return;
            List<object[]> list_藥檔資料 = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_GetAllRows(false);
            list_藥檔資料 = list_藥檔資料.GetRows((int)enum_藥品資料_資料維護_雲端藥檔.藥品碼, 藥品碼);
            if (list_藥檔資料.Count == 0) return;
            料號 = list_藥檔資料[0][(int)enum_藥品資料_資料維護_雲端藥檔.料號].ObjectToString();
            包裝單位 = list_藥檔資料[0][(int)enum_藥品資料_資料維護_雲端藥檔.包裝單位].ObjectToString();
            藥品名稱 = list_藥檔資料[0][(int)enum_藥品資料_資料維護_雲端藥檔.藥品名稱].ObjectToString();

            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            應收數量 = dialog_NumPannel.Value.ToString();
            實收數量 = "0";

    

            object[] value = new object[new enum_藥庫_驗收入庫_過帳明細().GetLength()];
            value[(int)enum_藥庫_驗收入庫_過帳明細.GUID] = Guid.NewGuid().ToString();
            value[(int)enum_藥庫_驗收入庫_過帳明細.藥品碼] = 藥品碼;
            value[(int)enum_藥庫_驗收入庫_過帳明細.料號] = 料號;
            value[(int)enum_藥庫_驗收入庫_過帳明細.藥品名稱] = 藥品名稱;
            value[(int)enum_藥庫_驗收入庫_過帳明細.包裝單位] = 包裝單位;
            value[(int)enum_藥庫_驗收入庫_過帳明細.應收數量] = 應收數量;
            value[(int)enum_藥庫_驗收入庫_過帳明細.實收數量] = 實收數量;

            value[(int)enum_藥庫_驗收入庫_過帳明細.驗收時間] = DateTime.Now.ToDateTimeString();
            value[(int)enum_藥庫_驗收入庫_過帳明細.入庫時間] = DateTime.MinValue.ToDateTimeString();
            value[(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.等待過帳.GetEnumName();
            value[(int)enum_藥庫_驗收入庫_過帳明細.來源] = "";
            value[(int)enum_藥庫_驗收入庫_過帳明細.備註] = "";

            this.sqL_DataGridView_驗收入庫資料.SQL_AddRow(value, false);
            this.sqL_DataGridView_驗收入庫資料.AddRow(value, true);
       
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_全部顯示_MouseDownEvent(MouseEventArgs mevent)
        {
            sqL_DataGridView_驗收入庫資料.SQL_GetAllRows(true);
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("確認刪除選取資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<object[]> list_驗收入庫明細 = this.sqL_DataGridView_驗收入庫資料.Get_All_Checked_RowsValues();
            if (list_驗收入庫明細.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            this.sqL_DataGridView_驗收入庫資料.SQL_DeleteExtra(list_驗收入庫明細, false);
            this.sqL_DataGridView_驗收入庫資料.DeleteExtra(list_驗收入庫明細, true);
            MyMessageBox.ShowDialog("刪除完成!");
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料過帳_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收入庫明細 = this.sqL_DataGridView_驗收入庫資料.Get_All_Checked_RowsValues();
            if (list_驗收入庫明細.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            this.Function_藥庫_驗收入庫_過帳明細_選取資料過帳(list_驗收入庫明細);
            MyMessageBox.ShowDialog("過帳完成!");
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料設定未過帳_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否將選取資料設定為[未過帳]?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<object[]> list_驗收入庫明細 = this.sqL_DataGridView_驗收入庫資料.Get_All_Checked_RowsValues();
            List<object[]> list_驗收入庫明細_replace = new List<object[]>();
            List<object[]> list_補給驗收入庫_replace = new List<object[]>();

            for (int i = 0; i < list_驗收入庫明細.Count; i++)
            {
                object[] value = list_驗收入庫明細[i];
                string 藥品碼 = list_驗收入庫明細[i][(int)enum_藥庫_驗收入庫_過帳明細.藥品碼].ObjectToString();
                value[(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.等待過帳.GetEnumName();
                list_補給驗收入庫_replace.Add(value);

                list_驗收入庫明細[i][(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.等待過帳.GetEnumName();
                list_驗收入庫明細_replace.Add(list_驗收入庫明細[i]);
            }
            this.sqL_DataGridView_驗收入庫資料.SQL_ReplaceExtra(list_補給驗收入庫_replace, false);
            this.sqL_DataGridView_驗收入庫資料.ReplaceExtra(list_驗收入庫明細_replace, true);
            MyMessageBox.ShowDialog("設定完成!");
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_選取資料設定忽略未過帳_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否將選取資料設定為[忽略過帳]?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<object[]> list_驗收入庫明細 = this.sqL_DataGridView_驗收入庫資料.Get_All_Checked_RowsValues();
            List<object[]> list_驗收入庫明細_replace = new List<object[]>();
            List<object[]> list_補給驗收入庫_replace = new List<object[]>();

            for (int i = 0; i < list_驗收入庫明細.Count; i++)
            {
                object[] value = list_驗收入庫明細[i];
                string 藥品碼 = list_驗收入庫明細[i][(int)enum_藥庫_驗收入庫_過帳明細.藥品碼].ObjectToString();
                value[(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.等待過帳.GetEnumName();
                list_補給驗收入庫_replace.Add(value);

                list_驗收入庫明細[i][(int)enum_藥庫_驗收入庫_過帳明細.狀態] = enum_藥庫_驗收入庫_過帳明細_狀態.忽略過帳.GetEnumName();
                list_驗收入庫明細_replace.Add(list_驗收入庫明細[i]);
            }
            this.sqL_DataGridView_驗收入庫資料.SQL_ReplaceExtra(list_補給驗收入庫_replace, false);
            this.sqL_DataGridView_驗收入庫資料.ReplaceExtra(list_驗收入庫明細_replace, true);
            MyMessageBox.ShowDialog("設定完成!");
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_顯示異常過帳_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫資料.SQL_GetAllRows(false);
            list_value = (from value in list_value
                          where value[(int)enum_藥庫_驗收入庫_過帳明細.狀態].ObjectToString() == enum_藥庫_驗收入庫_過帳明細_狀態.庫存不足.GetEnumName()
                          || value[(int)enum_藥庫_驗收入庫_過帳明細.狀態].ObjectToString() == enum_藥庫_驗收入庫_過帳明細_狀態.找無此藥品.GetEnumName()
                          || value[(int)enum_藥庫_驗收入庫_過帳明細.狀態].ObjectToString() == enum_藥庫_驗收入庫_過帳明細_狀態.未建立儲位.GetEnumName()
                          || value[(int)enum_藥庫_驗收入庫_過帳明細.狀態].ObjectToString() == enum_藥庫_驗收入庫_過帳明細_狀態.無效期可入帳.GetEnumName()
                          || value[(int)enum_藥庫_驗收入庫_過帳明細.狀態].ObjectToString() == enum_藥庫_驗收入庫_過帳明細_狀態.庫存不足.GetEnumName()
                          select value).ToList();
            this.sqL_DataGridView_驗收入庫資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_顯示等待過帳_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫資料.SQL_GetAllRows(false);
            list_value = (from value in list_value
                          where value[(int)enum_藥庫_驗收入庫_過帳明細.狀態].ObjectToString() == enum_藥庫_驗收入庫_過帳明細_狀態.等待過帳.GetEnumName()
                          select value).ToList();
            this.sqL_DataGridView_驗收入庫資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_藥品名稱篩選_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫資料.GetAllRows();
            list_value = list_value.GetRowsByLike((int)enum_藥庫_驗收入庫_過帳明細.藥品名稱, this.rJ_TextBox_藥庫_驗收入庫_過帳明細_藥品名稱篩選.Text);
            this.sqL_DataGridView_驗收入庫資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥庫_驗收入庫_過帳明細_藥品碼篩選_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫資料.GetAllRows();
            list_value = list_value.GetRowsByLike((int)enum_藥庫_驗收入庫_過帳明細.藥品碼, this.rJ_TextBox_藥庫_驗收入庫_過帳明細_藥品碼篩選.Text);
            this.sqL_DataGridView_驗收入庫資料.RefreshGrid(list_value);
        }
        #endregion
        private class ICP_驗收入庫_過帳明細 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                DateTime temp0 = x[(int)enum_藥庫_驗收入庫_過帳明細.驗收時間].ToDateString().StringToDateTime();
                DateTime temp1 = y[(int)enum_藥庫_驗收入庫_過帳明細.驗收時間].ToDateString().StringToDateTime();
                int cmp = temp0.CompareTo(temp1);
                return cmp;
            }
        }
    }
}
