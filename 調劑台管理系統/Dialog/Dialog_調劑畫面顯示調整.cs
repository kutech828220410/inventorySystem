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
using HIS_DB_Lib;
using SQLUI;
namespace 調劑台管理系統
{
    public partial class Dialog_調劑畫面顯示調整 : MyDialog
    {
        private SQL_DataGridView sQL_DataGridView;
        private Panel panel;
        private int index = -1;
        public Dialog_調劑畫面顯示調整(int index)
        {
            InitializeComponent();
            this.LoadFinishedEvent += Dialog_調劑畫面顯示調整_LoadFinishedEvent;
            this.plC_RJ_Button_確認送出.MouseDownEvent += PlC_RJ_Button_確認送出_MouseDownEvent;
            this.button_批序字型.Click += Button_批序字型_Click;
            this.button_藥碼字型.Click += Button_藥碼字型_Click;
            this.button_藥名字型.Click += Button_藥名字型_Click;
            this.button_單位字型.Click += Button_單位字型_Click;
            this.button_庫存量字型.Click += Button_庫存量字型_Click;
            this.button_數量字型.Click += Button_數量字型_Click;
            this.button_結存量字型.Click += Button_結存量字型_Click;
            this.button_盤點量字型.Click += Button_盤點量字型_Click;
            this.button_狀態字型.Click += Button_狀態字型_Click;
            this.index = index;
        }
   
        private void Dialog_調劑畫面顯示調整_LoadFinishedEvent(EventArgs e)
        {
            rJ_Lable_標題.Text = $"({index + 1})調劑畫面";
            this.Refresh();
            if (index == 0)
            {
                sQL_DataGridView = Main_Form._sqL_DataGridView_領藥台_01_領藥內容;
                panel = Main_Form._panel_領藥台_01_藥品資訊;
            }
            if (index == 1)
            {
                sQL_DataGridView = Main_Form._sqL_DataGridView_領藥台_02_領藥內容;
                panel = Main_Form._panel_領藥台_02_藥品資訊;
            }
            if (index == 2)
            {
                sQL_DataGridView = Main_Form._sqL_DataGridView_領藥台_03_領藥內容;
            }
            if (index == 3)
            {
                sQL_DataGridView = Main_Form._sqL_DataGridView_領藥台_04_領藥內容;
            }

            if (sQL_DataGridView != null)
            {
                plC_CheckBox_批序顯示.Checked = sQL_DataGridView.Get_ColumnVisible(enum_取藥堆疊母資料.序號.GetEnumName());
                plC_CheckBox_藥碼顯示.Checked = sQL_DataGridView.Get_ColumnVisible(enum_取藥堆疊母資料.藥品碼.GetEnumName());
                plC_CheckBox_單位顯示.Checked = sQL_DataGridView.Get_ColumnVisible(enum_取藥堆疊母資料.單位.GetEnumName());
                plC_CheckBox_庫存量顯示.Checked = sQL_DataGridView.Get_ColumnVisible(enum_取藥堆疊母資料.庫存量.GetEnumName());
                plC_CheckBox_結存量顯示.Checked = sQL_DataGridView.Get_ColumnVisible(enum_取藥堆疊母資料.結存量.GetEnumName());
                plC_CheckBox_盤點量顯示.Checked = sQL_DataGridView.Get_ColumnVisible(enum_取藥堆疊母資料.盤點量.GetEnumName());
                plC_CheckBox_狀態顯示.Checked = sQL_DataGridView.Get_ColumnVisible(enum_取藥堆疊母資料.狀態.GetEnumName());

                numTextBox_批序欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.序號.GetEnumName()).ToString();
                numTextBox_藥碼欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.藥品碼.GetEnumName()).ToString();
                numTextBox_藥名欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.藥品名稱.GetEnumName()).ToString();
                numTextBox_單位欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.單位.GetEnumName()).ToString();
                numTextBox_數量欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.總異動量.GetEnumName()).ToString();
                numTextBox_庫存量欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.庫存量.GetEnumName()).ToString();
                numTextBox_結存量欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.結存量.GetEnumName()).ToString();
                numTextBox_盤點量欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.盤點量.GetEnumName()).ToString();
                numTextBox_狀態欄位寬度.Text = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.狀態.GetEnumName()).ToString();
            }
            if (panel != null)
            {
                numTextBox_藥物辨識圖片大小.Text = panel.Height.ToString();
            }
        }
        private void PlC_RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {            
                sQL_DataGridView.Set_ColumnWidth(numTextBox_批序欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.序號.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_藥碼欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.藥品碼.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_藥名欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.藥品名稱.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_單位欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.單位.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_數量欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.總異動量.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_庫存量欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.庫存量.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_結存量欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.結存量.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_盤點量欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.盤點量.GetEnumName());
                sQL_DataGridView.Set_ColumnWidth(numTextBox_狀態欄位寬度.Text.StringToInt32(), enum_取藥堆疊母資料.狀態.GetEnumName());

                sQL_DataGridView.Set_ColumnVisible(plC_CheckBox_批序顯示.Checked, enum_取藥堆疊母資料.序號.GetEnumName());
                sQL_DataGridView.Set_ColumnVisible(plC_CheckBox_藥碼顯示.Checked, enum_取藥堆疊母資料.藥品碼.GetEnumName());
                sQL_DataGridView.Set_ColumnVisible(plC_CheckBox_單位顯示.Checked, enum_取藥堆疊母資料.單位.GetEnumName());
                sQL_DataGridView.Set_ColumnVisible(plC_CheckBox_庫存量顯示.Checked, enum_取藥堆疊母資料.庫存量.GetEnumName());
                sQL_DataGridView.Set_ColumnVisible(plC_CheckBox_結存量顯示.Checked, enum_取藥堆疊母資料.結存量.GetEnumName());
                sQL_DataGridView.Set_ColumnVisible(plC_CheckBox_盤點量顯示.Checked, enum_取藥堆疊母資料.盤點量.GetEnumName());
                sQL_DataGridView.Set_ColumnVisible(plC_CheckBox_狀態顯示.Checked, enum_取藥堆疊母資料.狀態.GetEnumName());

                panel.Height = numTextBox_藥物辨識圖片大小.Text.StringToInt32();
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));   
        }
        private void Button_狀態字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.狀態.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.狀態.GetEnumName());
        }
        private void Button_盤點量字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.盤點量.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.盤點量.GetEnumName());
        }
        private void Button_結存量字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.結存量.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.結存量.GetEnumName());
        }
        private void Button_數量字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.總異動量.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.總異動量.GetEnumName());
        }
        private void Button_庫存量字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.庫存量.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.庫存量.GetEnumName());
        }
        private void Button_單位字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.單位.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.單位.GetEnumName());
        }
        private void Button_藥名字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.藥品名稱.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.藥品名稱.GetEnumName());
        }
        private void Button_藥碼字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.藥品碼.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.藥品碼.GetEnumName());
        }
        private void Button_批序字型_Click(object sender, EventArgs e)
        {
            this.fontDialog.Font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.序號.GetEnumName());
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;
            sQL_DataGridView.Set_ColumnFont(this.fontDialog.Font, enum_取藥堆疊母資料.序號.GetEnumName());
        }
    }
}
