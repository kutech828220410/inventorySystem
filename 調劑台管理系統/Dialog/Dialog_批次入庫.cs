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
using MyOffice;
namespace 調劑台管理系統
{
    public partial class Dialog_批次入庫 : MyDialog
    {
        public List<batch_inventory_importClass> Value = new List<batch_inventory_importClass>();
        public Dialog_批次入庫()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
            dateTimeIntervelPicker_建表日期.SetDateTime(DateTime.Now.GetStartDate(), DateTime.Now.GetEndDate());
            this.LoadFinishedEvent += Dialog_批次入庫_LoadFinishedEvent;
            this.rJ_Button_搜尋.MouseDownEvent += RJ_Button_搜尋_MouseDownEvent;
            this.plC_RJ_Button_確認送出.MouseDownEvent += PlC_RJ_Button_確認送出_MouseDownEvent;
            this.plC_RJ_Button_範例下載.MouseDownEvent += PlC_RJ_Button_範例下載_MouseDownEvent;
            this.plC_RJ_Button_匯入.MouseDownEvent += PlC_RJ_Button_匯入_MouseDownEvent;
        }

      

        private void Dialog_批次入庫_LoadFinishedEvent(EventArgs e)
        {
            SQLUI.Table table = batch_inventory_importClass.init(Main_Form.API_Server);

            sqL_DataGridView_批次入庫.InitEx(table);
            sqL_DataGridView_批次入庫.Set_ColumnVisible(false, new enum_batch_inventory_import().GetEnumNames());
            sqL_DataGridView_批次入庫.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_batch_inventory_import.藥碼);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_batch_inventory_import.藥名);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_batch_inventory_import.單位);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_batch_inventory_import.數量);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_batch_inventory_import.效期);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_batch_inventory_import.批號);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_batch_inventory_import.建表人員);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_batch_inventory_import.建表時間);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_batch_inventory_import.狀態);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_batch_inventory_import.入庫人員);
            sqL_DataGridView_批次入庫.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_batch_inventory_import.入庫時間);

            sqL_DataGridView_批次入庫.DataGridRefreshEvent += SqL_DataGridView_批次入庫_DataGridRefreshEvent;
            sqL_DataGridView_批次入庫.RowDoubleClickEvent += SqL_DataGridView_批次入庫_RowDoubleClickEvent;
            sqL_DataGridView_批次入庫.DataGridRowsChangeRefEvent += SqL_DataGridView_批次入庫_DataGridRowsChangeRefEvent;
            this.Invoke(new Action(delegate 
            {
                comboBox_搜尋條件.SelectedIndex = 0;
            }));
      

            LoadingForm.ShowLoadingForm();

            List<batch_inventory_importClass> batch_Inventory_ImportClasses = batch_inventory_importClass.get_by_CT_TIME(Main_Form.API_Server, DateTime.Now.GetStartDate(), DateTime.Now.GetEndDate());
            batch_Inventory_ImportClasses = (from temp in batch_Inventory_ImportClasses
                                             where temp.庫別 == Main_Form.ServerName
                                             where temp.數量.StringToDouble() > 0
                                             where Main_Form.Function_從本地資料取得儲位(temp.藥碼).Count > 0
                                             select temp).ToList();
     
            List<object[]> list_value = batch_Inventory_ImportClasses.ClassToSQL<batch_inventory_importClass, enum_batch_inventory_import>();
            sqL_DataGridView_批次入庫.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }

        private void SqL_DataGridView_批次入庫_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            List<object[]> RowsList_buf = new List<object[]>();

            RowsList_buf.LockAdd(RowsList.GetRows((int)enum_batch_inventory_import.狀態, "等待過帳"));
            RowsList_buf.LockAdd(RowsList.GetRows((int)enum_batch_inventory_import.狀態, "過帳完成"));

            RowsList = RowsList_buf;

        }
        private void SqL_DataGridView_批次入庫_DataGridRefreshEvent()
        {
            for (int i = 0; i < this.sqL_DataGridView_批次入庫.dataGridView.Rows.Count; i++)
            {
                if (this.sqL_DataGridView_批次入庫.dataGridView.Rows[i].Cells[enum_batch_inventory_import.入庫人員.GetEnumName()].Value.ObjectToString().StringIsEmpty())
                {
                    this.sqL_DataGridView_批次入庫.dataGridView.Rows[i].Cells[enum_batch_inventory_import.入庫人員.GetEnumName()].Value = "-";
                }
                if (this.sqL_DataGridView_批次入庫.dataGridView.Rows[i].Cells[enum_batch_inventory_import.狀態.GetEnumName()].Value.ObjectToString() == "過帳完成")
                {
                    this.sqL_DataGridView_批次入庫.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                    this.sqL_DataGridView_批次入庫.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    this.sqL_DataGridView_批次入庫.dataGridView.Rows[i].ReadOnly = true;
                }
            }
        }
        private void SqL_DataGridView_批次入庫_RowDoubleClickEvent(object[] RowValue)
        {
            if (RowValue[(int)enum_batch_inventory_import.狀態].ObjectToString() == "過帳完成") return;
            int 數量 = RowValue[(int)enum_batch_inventory_import.數量].StringToInt32();

            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入入庫數量");

            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;

            數量 = dialog_NumPannel.Value;
            RowValue[(int)enum_batch_inventory_import.數量] = 數量;
            this.sqL_DataGridView_批次入庫.SQL_ReplaceExtra(RowValue, false);
            this.sqL_DataGridView_批次入庫.ReplaceExtra(RowValue, true);
        }
        private void RJ_Button_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            try
            {
                DateTime dateTime_st = dateTimeIntervelPicker_建表日期.StartTime;
                DateTime dateTime_end = dateTimeIntervelPicker_建表日期.EndTime;
                List<batch_inventory_importClass> batch_Inventory_ImportClasses = new List<batch_inventory_importClass>();
      

                List<object[]> list_value = new List<object[]>();
                if (rJ_RatioButton_建表時間.Checked)
                {
                    batch_Inventory_ImportClasses = batch_inventory_importClass.get_by_CT_TIME(Main_Form.API_Server, dateTime_st, dateTime_end);
                }
                else if (rJ_RatioButton_入庫時間.Checked)
                {
                    batch_Inventory_ImportClasses = batch_inventory_importClass.get_by_RECEIVE_TIME(Main_Form.API_Server, dateTime_st, dateTime_end);
                }
                if (comboBox_搜尋條件.GetComboBoxText() == "全部顯示")
                {
                    list_value = batch_Inventory_ImportClasses.ClassToSQL<batch_inventory_importClass, enum_batch_inventory_import>();
                }
                if (comboBox_搜尋條件.GetComboBoxText() == "藥碼")
                {
                    batch_Inventory_ImportClasses = (from temp in batch_Inventory_ImportClasses
                                                     where temp.藥碼.ToUpper().Contains(comboBox_搜尋內容.GetComboBoxText().ToUpper())
                                                     select temp).ToList();

                    list_value = batch_Inventory_ImportClasses.ClassToSQL<batch_inventory_importClass, enum_batch_inventory_import>();
                }
                if (comboBox_搜尋條件.GetComboBoxText() == "藥名")
                {
                    batch_Inventory_ImportClasses = (from temp in batch_Inventory_ImportClasses
                                                     where temp.藥名.ToUpper().Contains(comboBox_搜尋內容.GetComboBoxText().ToUpper())
                                                     select temp).ToList();

                }

                batch_Inventory_ImportClasses = (from temp in batch_Inventory_ImportClasses
                                                 where temp.庫別 == Main_Form.ServerName
                                                 where Main_Form.Function_從本地資料取得儲位(temp.藥碼).Count > 0
                                                 where temp.數量.StringToDouble() > 0
                                                 select temp).ToList();
        
                list_value = batch_Inventory_ImportClasses.ClassToSQL<batch_inventory_importClass, enum_batch_inventory_import>();

                if (list_value.Count == 0)
                {
                    MyMessageBox.ShowDialog("查無資料");
                    return;
                }
  


                sqL_DataGridView_批次入庫.RefreshGrid(list_value);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
        }

        private void PlC_RJ_Button_範例下載_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;

                byte[] bytes = batch_inventory_importClass.download_sample_excel(Main_Form.API_Server);

                bytes.SaveFileStream(this.saveFileDialog_SaveExcel.FileName);

                MyMessageBox.ShowDialog("下載成功");

            }));
        }
        private void PlC_RJ_Button_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.openFileDialog_LoadExcel.ShowDialog() != DialogResult.OK) return;

                byte[] bytes = ExcelClass.NPOI_LoadToBytes(this.openFileDialog_LoadExcel.FileName);
                List<batch_inventory_importClass> batch_Inventory_ImportClasses = batch_inventory_importClass.excel_upload(Main_Form.API_Server, bytes, Main_Form._登入者名稱);

                MyMessageBox.ShowDialog($"新增批次入庫資料成功共<{batch_Inventory_ImportClasses.Count}>筆");
                List<object[]> list_value = batch_Inventory_ImportClasses.ClassToSQL<batch_inventory_importClass, enum_batch_inventory_import>();

                sqL_DataGridView_批次入庫.RefreshGrid(list_value);
            }));
        }
        private void PlC_RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                try
                {
                    List<object[]> list_value = sqL_DataGridView_批次入庫.Get_All_Checked_RowsValues();
                    list_value = list_value.GetRows((int)enum_batch_inventory_import.狀態, "等待過帳");
                    if (list_value.Count == 0)
                    {
                        MyMessageBox.ShowDialog("未選取資料");
                        return;
                    }
                    if (MyMessageBox.ShowDialog($"是否將<{list_value.Count}>筆資料進行批次入庫?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                    LoadingForm.ShowLoadingForm();
                    this.Value = list_value.SQLToClass<batch_inventory_importClass, enum_batch_inventory_import>();
                    this.DialogResult = DialogResult.Yes;

                    batch_inventory_importClass.update_state_done_by_GUID(Main_Form.API_Server, this.Value, Main_Form._登入者名稱);
                    LoadingForm.CloseLoadingForm();
                    this.Close();
                }
                catch
                {

                }
                finally
                {
                
                  
                }
            }));
        }
  
    }
}
