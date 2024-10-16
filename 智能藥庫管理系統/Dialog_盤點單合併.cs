﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using MyUI;
using SQLUI;
using HIS_DB_Lib;
using MyOffice;

namespace 智能藥庫系統
{
    public partial class Dialog_盤點單合併 : MyDialog
    {
        public static bool IsShown = false;
        private List<Panel> panels = new List<Panel>();
        public inv_combinelistClass Inv_CombinelistClass = new inv_combinelistClass();
        public DateTime DateTime_st = new DateTime();
        public DateTime DateTime_end = new DateTime();

        static public Dialog_盤點單合併 myDialog;
        static public Dialog_盤點單合併 GetForm()
        {
            if (myDialog != null)
            {
                return myDialog;
            }
            else
            {
                myDialog = new Dialog_盤點單合併();
                return myDialog;
            }
        }

        public Dialog_盤點單合併()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));


            this.LoadFinishedEvent += Dialog_盤點單合併_LoadFinishedEvent;
            this.ShowDialogEvent += Dialog_盤點單合併_ShowDialogEvent;
            this.FormClosing += Dialog_盤點單合併_FormClosing;

            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_新建.MouseDownEvent += PlC_RJ_Button_新建_MouseDownEvent;
            this.plC_RJ_Button_庫存設定.MouseDownEvent += PlC_RJ_Button_庫存設定_MouseDownEvent;
            this.plC_RJ_Button_單價設定.MouseDownEvent += PlC_RJ_Button_單價設定_MouseDownEvent;

            this.plC_RJ_Button_覆盤設定.MouseDownEvent += PlC_RJ_Button_覆盤設定_MouseDownEvent;

            this.plC_RJ_Button_選擇.MouseDownEvent += PlC_RJ_Button_選擇_MouseDownEvent;
            this.plC_RJ_Button_刪除.MouseDownEvent += PlC_RJ_Button_刪除_MouseDownEvent;
            this.plC_RJ_Button_下載報表.MouseDownEvent += PlC_RJ_Button_下載報表_MouseDownEvent;
            this.plC_RJ_Button_生成總表.MouseDownEvent += PlC_RJ_Button_生成總表_MouseDownEvent;

            dateTimeIntervelPicker_建表日期.SureClick += DateTimeIntervelPicker_建表日期_SureClick;
            this.comboBox_inv_Combinelist.SelectedIndexChanged += ComboBox_inv_Combinelist_SelectedIndexChanged;
        }



        private void Function_RereshUI(inv_combinelistClass Inv_CombinelistClass, stockRecord stockRecord)
        {
            this.Inv_CombinelistClass = Inv_CombinelistClass;
           
        }

        #region Event

        private void ComboBox_inv_Combinelist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void PlC_RJ_Button_下載報表_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {


                string text = "";
                text = this.comboBox_inv_Combinelist.Text;
                string SN = RemoveParentheses(text);
                if (this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
                {
                    string fileName = this.saveFileDialog_SaveExcel.FileName;
                    LoadingForm.ShowLoadingForm();
                    LoadingForm.Set_Description("下載報表...");

                    byte[] bytes = inv_combinelistClass.get_full_inv_Excel_by_SN(Main_Form.API_Server, SN);
                    LoadingForm.Set_Description($"儲存檔案...");

                    bytes.SaveFileStream(saveFileDialog_SaveExcel.FileName);
                    LoadingForm.CloseLoadingForm();
                    MyMessageBox.ShowDialog("完成");

                }

            }));

        }
        private void PlC_RJ_Button_選擇_MouseDownEvent(MouseEventArgs mevent)
        {

            string text = "";
            this.Invoke(new Action(delegate { text = this.comboBox_inv_Combinelist.Text; }));
            if (text.StringIsEmpty()) return;
            string SN = RemoveParentheses(text);
            Dialog_盤點單合併_選擇 dialog_盤點單合併_選擇 = new Dialog_盤點單合併_選擇(SN);
            dialog_盤點單合併_選擇.ShowDialog();
            inv_combinelistClass inv_CombinelistClass = inv_combinelistClass.get_all_inv(Main_Form.API_Server, SN);
            if (inv_CombinelistClass == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"查無資料[{SN}]", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
      
            inv_combinelistClass.inv_creat_update(Main_Form.API_Server, dialog_盤點單合併_選擇.Value);
        }
        private void Dialog_盤點單合併_FormClosing(object sender, FormClosingEventArgs e)
        {
            myDialog = null;
            IsShown = false;
        }
        private void Dialog_盤點單合併_ShowDialogEvent()
        {
            if (myDialog != null)
            {
                form.Invoke(new Action(delegate
                {
                    myDialog.WindowState = FormWindowState.Normal;
                    myDialog.BringToFront();
                    this.DialogResult = DialogResult.Cancel;
                }));
            }
        }
        private void Dialog_盤點單合併_LoadFinishedEvent(EventArgs e)
        {
            sqL_DataGridView_盤點總表.RowsHeight = 35;
            sqL_DataGridView_盤點總表.Init(new Table(new enum_盤點定盤_Excel()));
            sqL_DataGridView_盤點總表.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleLeft, enum_盤點定盤_Excel.藥碼);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleLeft, enum_盤點定盤_Excel.料號);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_盤點定盤_Excel.藥名);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleLeft, enum_盤點定盤_Excel.誤差百分率);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.單價);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.庫存量);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.庫存金額);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.消耗量);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.盤點量);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.結存金額);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.覆盤量);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.誤差量);
            sqL_DataGridView_盤點總表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_盤點定盤_Excel.誤差金額);

            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.藥碼);
            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.庫存金額);
            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.結存金額);
            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.覆盤量);
            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.誤差量);
            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.誤差金額);
            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.註記);
            sqL_DataGridView_盤點總表.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_盤點定盤_Excel.盤點量);

            dateTimeIntervelPicker_建表日期.StartTime = DateTime.Now.GetStartDate().AddMonths(-1);
            dateTimeIntervelPicker_建表日期.EndTime = DateTime.Now.GetEndDate().AddMonths(0);
            dateTimeIntervelPicker_建表日期.OnSureClick();
            IsShown = true;

        }
        private void DateTimeIntervelPicker_建表日期_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            DateTime dateTime_st = dateTimeIntervelPicker_建表日期.StartTime;
            DateTime dateTime_end = dateTimeIntervelPicker_建表日期.EndTime;
            List<inv_combinelistClass> inv_CombinelistClasses = inv_combinelistClass.get_all_inv($"{Main_Form.API_Server}", dateTime_st, dateTime_end);
            if (inv_CombinelistClasses.Count == 0) return;
            List<string> list_str = new List<string>();
            for (int i = 0; i < inv_CombinelistClasses.Count; i++)
            {
                string str = $"{inv_CombinelistClasses[i].合併單號}({inv_CombinelistClasses[i].合併單名稱})";
                list_str.Add(str);
            }
            this.Invoke(new Action(delegate
            {
                comboBox_inv_Combinelist.Items.Clear();
                for (int i = 0; i < list_str.Count; i++)
                {
                    comboBox_inv_Combinelist.Items.Add(list_str[i]);
                }
                comboBox_inv_Combinelist.SelectedIndex = 0;
            }));

        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Close();
            DialogResult = DialogResult.No;
        }
        private void PlC_RJ_Button_新建_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_AlarmForm dialog_AlarmForm;
            Dialog_新建合併盤點單 dialog_新建合併盤點單 = new Dialog_新建合併盤點單();
            dialog_新建合併盤點單.ShowDialog();
            if (dialog_新建合併盤點單.DialogResult != DialogResult.Yes) return;

            string name = dialog_新建合併盤點單.Value;
            inv_combinelistClass inv_CombinelistClass = new inv_combinelistClass();
            inv_CombinelistClass.合併單名稱 = name;
            inv_CombinelistClass = inv_combinelistClass.inv_creat_update(Main_Form.API_Server, inv_CombinelistClass);
            if (inv_CombinelistClass == null)
            {
                dialog_AlarmForm = new Dialog_AlarmForm("新建失敗", 1500);
                dialog_AlarmForm.ShowDialog();
            }
            dialog_AlarmForm = new Dialog_AlarmForm("新建成功", 1500, Color.Green);
            dialog_AlarmForm.ShowDialog();
            dateTimeIntervelPicker_建表日期.OnSureClick();

        }
        private void PlC_RJ_Button_庫存設定_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.comboBox_inv_Combinelist.GetComboBoxText();
            if (text.StringIsEmpty()) return;
            string SN = RemoveParentheses(text);

            Dialog_盤點單合併_庫存設定 dialog_盤點單合併_庫存設定 = new Dialog_盤點單合併_庫存設定(SN);
            dialog_盤點單合併_庫存設定.ShowDialog();
        }
        private void PlC_RJ_Button_單價設定_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.comboBox_inv_Combinelist.GetComboBoxText();
            if (text.StringIsEmpty()) return;
            string SN = RemoveParentheses(text);

            Dialog_盤點單合併_單價設定 dialog_盤點單合併_單價設定 = new Dialog_盤點單合併_單價設定(SN);
            dialog_盤點單合併_單價設定.ShowDialog();
        }
        private void PlC_RJ_Button_覆盤設定_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.comboBox_inv_Combinelist.GetComboBoxText();
            if (text.StringIsEmpty()) return;
            string SN = RemoveParentheses(text);
            Dialog_盤點單覆盤建議設定 dialog_盤點單覆盤建議設定 = new Dialog_盤點單覆盤建議設定(SN);
            if (dialog_盤點單覆盤建議設定.ShowDialog() != DialogResult.Yes) return;
            inv_combinelistClass.inv_creat_update(Main_Form.API_Server, dialog_盤點單覆盤建議設定.Value);
        }
        private void PlC_RJ_Button_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("確認刪除?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            string text = "";
            this.Invoke(new Action(delegate { text = this.comboBox_inv_Combinelist.Text; }));
            if (text.StringIsEmpty()) return;
            text = RemoveParentheses(text);
            inv_combinelistClass.inv_delete_by_SN(Main_Form.API_Server, text);
            dateTimeIntervelPicker_建表日期.OnSureClick();

        }
        private void PlC_RJ_Button_生成總表_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                LoadingForm.ShowLoadingForm();
                string text = this.comboBox_inv_Combinelist.GetComboBoxText();
                if (text.StringIsEmpty()) return;
                string SN = RemoveParentheses(text);
                List<DataTable> dataTables = inv_combinelistClass.get_full_inv_DataTable_by_SN(Main_Form.API_Server, SN);
                if(dataTables == null)
                {
                    MyMessageBox.ShowDialog("取得總表失敗");
                    return;
                }
                if (dataTables.Count == 0)
                {
                    MyMessageBox.ShowDialog("取得總表失敗");
                    return;
                }

                List<object[]> list_value = dataTables[0].DataTableToRowList();
                this.sqL_DataGridView_盤點總表.RefreshGrid(list_value);
            }
            catch
            {
                
            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }

        }

        #endregion
        static string RemoveParentheses(string input)
        {
            string pattern = @"^([^\(]+)";
            string result = "";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(input, pattern);

            if (match.Success)
            {
                result = match.Groups[1].Value;
            }

            return result;
        }
    }
}
