using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;
using MyUI;
using SQLUI;
using DrawingClass;
using H_Pannel_lib;
namespace 智能藥庫系統
{
    public partial class Dialog_盤點單覆盤建議設定 : MyDialog
    {
        public inv_combinelistClass Value
        {
            get
            {
                return inv_CombinelistClass;
            }
        }
        private inv_combinelistClass inv_CombinelistClass = null;
        private string _SN;

        public Dialog_盤點單覆盤建議設定(string SN)
        {
            InitializeComponent();
            this.LoadFinishedEvent += Dialog_盤點單覆盤建議設定_LoadFinishedEvent;
            this.FormClosing += Dialog_盤點單覆盤建議設定_FormClosing;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            _SN = SN;
        }

        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult = DialogResult.Yes;

            this.inv_CombinelistClass.誤差總金額致能 = plC_CheckBox_誤差總金額計算.Checked.ToString();
            this.inv_CombinelistClass.誤差百分率致能 = plC_CheckBox_誤差百分率計算.Checked.ToString();
            this.inv_CombinelistClass.誤差數量致能 = plC_CheckBox_誤差數量計算.Checked.ToString();

            inv_CombinelistClass.誤差總金額下限 = numTextBox_誤差總金額下限.Text;
            inv_CombinelistClass.誤差總金額上限 = numTextBox_誤差總金額上限.Text;

            inv_CombinelistClass.誤差百分率下限 = numTextBox_誤差百分率下限.Text;
            inv_CombinelistClass.誤差百分率上限 = numTextBox_誤差百分率上限.Text;

            inv_CombinelistClass.誤差數量下限 = numTextBox_誤差數量下限.Text;
            inv_CombinelistClass.誤差數量上限 = numTextBox_誤差數量上限.Text;

            this.Close();
        }

        private void Dialog_盤點單覆盤建議設定_FormClosing(object sender, FormClosingEventArgs e)
        {
 
        }
        private void Dialog_盤點單覆盤建議設定_LoadFinishedEvent(EventArgs e)
        {
            LoadingForm.ShowLoadingForm();

            inv_CombinelistClass = inv_combinelistClass.get_full_inv_by_SN(Main_Form.API_Server, _SN);
            plC_CheckBox_誤差總金額計算.Checked = inv_CombinelistClass.誤差總金額致能.StringToBool();
            plC_CheckBox_誤差百分率計算.Checked = inv_CombinelistClass.誤差百分率致能.StringToBool();
            plC_CheckBox_誤差數量計算.Checked = inv_CombinelistClass.誤差數量致能.StringToBool();


            numTextBox_誤差總金額下限.Text = inv_CombinelistClass.誤差總金額下限;
            numTextBox_誤差總金額上限.Text = inv_CombinelistClass.誤差總金額上限;

            numTextBox_誤差百分率下限.Text = inv_CombinelistClass.誤差百分率下限;
            numTextBox_誤差百分率上限.Text = inv_CombinelistClass.誤差百分率上限;

            numTextBox_誤差數量下限.Text = inv_CombinelistClass.誤差數量下限;
            numTextBox_誤差數量上限.Text = inv_CombinelistClass.誤差數量上限;
            LoadingForm.CloseLoadingForm();
        }
    }
}
