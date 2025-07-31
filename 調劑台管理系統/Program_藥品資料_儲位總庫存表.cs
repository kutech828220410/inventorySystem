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
using H_Pannel_lib;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using SQLUI;
using HIS_DB_Lib;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        public enum enum_儲位總庫存表
        {
            藥碼,
            藥名,
            庫存,
        }

        private void Program_藥品資料_儲位總庫存表_Init()
        {
            Table table = medClass.init(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, medClass.StoreType.調劑台);
            this.sqL_DataGridView_藥品資料_儲位總庫存表.Init(table);
            this.sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
            this.sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品碼);
            this.sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品名稱);
            this.sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.中文名稱);
            this.sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_藥品資料_藥檔資料.包裝單位);
            this.sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_藥品資料_藥檔資料.庫存);

            sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnText("藥碼", enum_藥品資料_藥檔資料.藥品碼);
            sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnText("藥名", enum_藥品資料_藥檔資料.藥品名稱);
            sqL_DataGridView_藥品資料_儲位總庫存表.Set_ColumnText("中文名", enum_藥品資料_藥檔資料.中文名稱);

            this.sqL_DataGridView_藥品資料_儲位總庫存表.Init();
            this.sqL_DataGridView_藥品資料_儲位總庫存表.DataGridRowsChangeRefEvent += SqL_DataGridView_藥品資料_儲位總庫存表_DataGridRowsChangeRefEvent;
            this.comboBox_藥品資料_儲位總庫存表_搜尋條件.SelectedIndex = 0;
            this.comboBox_藥品資料_儲位總庫存表_搜尋條件.SelectedIndexChanged += ComboBox_藥品資料_儲位總庫存表_搜尋條件_SelectedIndexChanged;
            this.rJ_Button_藥品資料_儲位總庫存表_搜尋.MouseDownEvent += RJ_Button_藥品資料_儲位總庫存表_搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_儲位總庫存表_匯出資料.MouseDownEvent += PlC_RJ_Button_藥品資料_儲位總庫存表_匯出資料_MouseDownEvent;

        }

     

        bool flag_藥品資料_儲位總庫存表_頁面更新 = false;
        private void sub_Program_藥品資料_儲位總庫存表()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥品資料" && this.plC_ScreenPage_藥品資料.PageText == "儲位總庫存表")
            {
                if (!this.flag_藥品資料_儲位總庫存表_頁面更新)
                {
                    this.flag_藥品資料_儲位總庫存表_頁面更新 = true;
                }
            }
            else
            {
                this.flag_藥品資料_儲位總庫存表_頁面更新 = false;
            }
 
        }

        #region Function
   
        #endregion
        #region Event
        private void SqL_DataGridView_藥品資料_儲位總庫存表_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
      
        }
        private void ComboBox_藥品資料_儲位總庫存表_搜尋條件_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_藥品資料_儲位總庫存表_搜尋條件.Text == "全部顯示")
            {
                //comboBox_藥品資料_儲位總庫存表_搜尋內容.Enabled = false;
            }
            else if (this.comboBox_藥品資料_儲位總庫存表_搜尋條件.Text == "管制級別")
            {
                //comboBox_藥品資料_儲位總庫存表_搜尋內容.Enabled = true;
                //comboBox_藥品資料_儲位總庫存表_搜尋內容.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox_藥品資料_儲位總庫存表_搜尋內容.Items.Clear();
                string[] strs = new string[] { "1", "2", "3", "4", "N" };
                for (int i = 0; i < strs.Length; i++)
                {
                    comboBox_藥品資料_儲位總庫存表_搜尋內容.Items.Add(strs[i]);
                }
                if (comboBox_藥品資料_儲位總庫存表_搜尋內容.Items.Count > 0) comboBox_藥品資料_儲位總庫存表_搜尋內容.SelectedIndex = 0;
            }
            else if (this.comboBox_藥品資料_儲位總庫存表_搜尋條件.Text == "藥品群組")
            {
                //comboBox_藥品資料_儲位總庫存表_搜尋內容.Enabled = true;
                //comboBox_藥品資料_儲位總庫存表_搜尋內容.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox_藥品資料_儲位總庫存表_搜尋內容.Items.Clear();
                string[] strs = medGroupClass.get_all_group_name(API_Server).ToArray();
                for (int i = 0; i < strs.Length; i++)
                {
                    comboBox_藥品資料_儲位總庫存表_搜尋內容.Items.Add(strs[i]);
                }
                if (comboBox_藥品資料_儲位總庫存表_搜尋內容.Items.Count > 0) comboBox_藥品資料_儲位總庫存表_搜尋內容.SelectedIndex = 0;
            }
            else
            {
                //comboBox_藥品資料_儲位總庫存表_搜尋內容.Enabled = true;
                //comboBox_藥品資料_儲位總庫存表_搜尋內容.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox_藥品資料_儲位總庫存表_搜尋內容.Items.Clear();
                comboBox_藥品資料_儲位總庫存表_搜尋內容.Text = "";
            }
        }

        private void RJ_Button_藥品資料_儲位總庫存表_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            try
            {
                List<object[]> list_value = new List<object[]>();
                List<medClass> medClasses = medClass.get_dps_medClass(API_Server, ServerName);
                medClasses = (from temp in medClasses
                              where temp.DeviceBasics.Count > 0
                              select temp).ToList();
                if (comboBox_藥品資料_儲位總庫存表_搜尋條件.GetComboBoxText() == "全部顯示")
                {
                    list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                }
                if (comboBox_藥品資料_儲位總庫存表_搜尋條件.GetComboBoxText() == "藥碼")
                {
                    if(comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("查無資料");
                        return;
                    }
                    medClasses = (from temp in medClasses
                                  where temp.藥品碼.ToUpper().Contains(comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().ToUpper())
                                  select temp).ToList();
                    list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                }
                if (comboBox_藥品資料_儲位總庫存表_搜尋條件.GetComboBoxText() == "藥名")
                {
                    if (comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("搜尋內容空白");
                        return;
                    }
                    medClasses = (from temp in medClasses
                                  where temp.藥品名稱.ToUpper().Contains(comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().ToUpper())
                                  select temp).ToList();
                    list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                }
                if (comboBox_藥品資料_儲位總庫存表_搜尋條件.GetComboBoxText() == "中文名")
                {
                    if (comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("搜尋內容空白");
                        return;
                    }
                    medClasses = (from temp in medClasses
                                  where temp.中文名稱.ToUpper().Contains(comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().ToUpper())
                                  select temp).ToList();
                    list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                }
                if (comboBox_藥品資料_儲位總庫存表_搜尋條件.GetComboBoxText() == "管制級別")
                {
                    if (comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("搜尋內容空白");
                        return;
                    }
                    medClasses = (from temp in medClasses
                                  where temp.管制級別.ToUpper().Contains(comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().ToUpper())
                                  select temp).ToList();
                    list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                }
                if (comboBox_藥品資料_儲位總庫存表_搜尋條件.GetComboBoxText() == "藥品群組")
                {
                    if (comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("搜尋內容空白");
                        return;
                    }
                    medGroupClass medGroupClass =  medGroupClass.get_group_by_name(API_Server, comboBox_藥品資料_儲位總庫存表_搜尋內容.GetComboBoxText());
                    Dictionary<string, List<medClass>> keyValuePairs_medGroup = medGroupClass.MedClasses.CoverToDictionaryByCode();

                    medClasses = (from temp in medClasses
                                  where (keyValuePairs_medGroup.SortDictionaryByCode(temp.藥品碼).Count > 0)
                                  select temp).ToList();
                    list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
                }
                sqL_DataGridView_藥品資料_儲位總庫存表.RefreshGrid(list_value);
            
                if (list_value.Count == 0)
                {
                    MyMessageBox.ShowDialog("查無資料");
                }
            }
            catch
            {
             
            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
        
        }
        private void PlC_RJ_Button_藥品資料_儲位總庫存表_匯出資料_MouseDownEvent(MouseEventArgs mevent)
        {
            saveFileDialog_SaveExcel.OverwritePrompt = false;
            this.Invoke(new Action(delegate
            {
                if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
                {
                    DataTable datatable = new DataTable();
                    datatable = sqL_DataGridView_藥品資料_儲位總庫存表.GetDataTable(false);
                    MyOffice.ExcelClass.NPOI_SaveFile(datatable, saveFileDialog_SaveExcel.FileName, new Enum[] { enum_藥品資料_藥檔資料.庫存 });
                    MyMessageBox.ShowDialog("匯出完成!");
                }
            }));
        }

        #endregion
    }
}
