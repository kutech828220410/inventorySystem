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
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;
namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        private void Program_交易紀錄_Init()
        {
            Table table = transactionsClass.Init(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType);

            this.comboBox_交易紀錄_搜尋條件.SelectedIndex = 0;

            this.sqL_DataGridView_交易紀錄.RowsHeight = 50;
            this.sqL_DataGridView_交易紀錄.Init(table);
            this.sqL_DataGridView_交易紀錄.Set_ColumnVisible(false, new enum_交易記錄查詢資料().GetEnumNames());
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.動作);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.領藥號);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.藥品碼);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品名稱);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.交易量);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.開方時間);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作人);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病人姓名);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作時間);
            this.sqL_DataGridView_交易紀錄.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病歷號);

            this.sqL_DataGridView_交易紀錄.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_交易記錄查詢資料.動作);
            this.sqL_DataGridView_交易紀錄.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_交易記錄查詢資料.領藥號);
            this.sqL_DataGridView_交易紀錄.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_交易記錄查詢資料.藥品碼);
            this.sqL_DataGridView_交易紀錄.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_交易記錄查詢資料.操作人);
            this.sqL_DataGridView_交易紀錄.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_交易記錄查詢資料.開方時間);
            this.sqL_DataGridView_交易紀錄.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_交易記錄查詢資料.操作時間);
            this.sqL_DataGridView_交易紀錄.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_交易記錄查詢資料.病歷號);

            this.dateTimeIntervelPicker_交易紀錄_操作時間.SetDateTime(DateTime.Now.AddMonths(-1).GetStartDate(), DateTime.Now.GetEndDate());
            this.dateTimeIntervelPicker_交易紀錄_操作時間.SureClick += DateTimeIntervelPicker_交易紀錄_操作時間_SureClick;
            this.dateTimeIntervelPicker_交易紀錄_開方時間.SetDateTime(DateTime.Now.AddMonths(-1).GetStartDate(), DateTime.Now.GetEndDate());
            this.dateTimeIntervelPicker_交易紀錄_開方時間.SureClick += DateTimeIntervelPicker_交易紀錄_開方時間_SureClick;

            this.rJ_Button_交易紀錄_搜尋.MouseDownEvent += RJ_Button_交易紀錄_搜尋_MouseDownEvent;

            this.plC_UI_Init.Add_Method(Program_交易紀錄);
        }

 

        private void Program_交易紀錄()
        {

        }


        #region Event
        private void DateTimeIntervelPicker_交易紀錄_開方時間_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            LoadingForm.ShowLoadingForm();
            List<transactionsClass> transactionsClasses = transactionsClass.get_datas_by_rx_time_st_end(Main_Form.API_Server, start, end, Main_Form.ServerName, Main_Form.ServerType);
            if (transactionsClasses.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
            this.sqL_DataGridView_交易紀錄.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void DateTimeIntervelPicker_交易紀錄_操作時間_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            LoadingForm.ShowLoadingForm();
            List<transactionsClass> transactionsClasses = transactionsClass.get_datas_by_op_time_st_end(Main_Form.API_Server, start, end, Main_Form.ServerName, Main_Form.ServerType);
            if (transactionsClasses.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
            this.sqL_DataGridView_交易紀錄.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_交易紀錄_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<transactionsClass> transactionsClasses = new List<transactionsClass>();
            string cmb_text = "";
            string text = textBox_交易紀錄_搜尋內容.Text;
            this.Invoke(new Action(delegate 
            {
                cmb_text = comboBox_交易紀錄_搜尋條件.Text;
            }));
            if (cmb_text == "藥碼")
            {
                transactionsClasses = transactionsClass.get_datas_by_code(Main_Form.API_Server, text, Main_Form.ServerName, Main_Form.ServerType);
            }
            if (cmb_text == "藥名")
            {
                transactionsClasses = transactionsClass.get_datas_by_name(Main_Form.API_Server, text, Main_Form.ServerName, Main_Form.ServerType);
            }
            if (cmb_text == "病歷號")
            {
                transactionsClasses = transactionsClass.get_datas_by_mrn(Main_Form.API_Server, text, Main_Form.ServerName, Main_Form.ServerType);
            }
            if (cmb_text == "領藥號")
            {
                transactionsClasses = transactionsClass.get_datas_by_med_bag_num(Main_Form.API_Server, text, Main_Form.ServerName, Main_Form.ServerType);
            }
            if (cmb_text == "操作人")
            {
                transactionsClasses = transactionsClass.get_datas_by_op(Main_Form.API_Server, text, Main_Form.ServerName, Main_Form.ServerType);
            }
            if (cmb_text == "病人姓名")
            {
                transactionsClasses = transactionsClass.get_datas_by_pat(Main_Form.API_Server, text, Main_Form.ServerName, Main_Form.ServerType);
            }
            if (transactionsClasses.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
            this.sqL_DataGridView_交易紀錄.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        #endregion
    }
}
