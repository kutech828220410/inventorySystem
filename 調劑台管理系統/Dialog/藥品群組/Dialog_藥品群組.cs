using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
using H_Pannel_lib;
using NPOI.SS.Formula.Functions;

namespace 調劑台管理系統
{
    public partial class Dialog_藥品群組 : MyDialog
    { 
        private medGroupClass medGroupClass = new medGroupClass();
        public Dialog_藥品群組()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();

                Table table = medClass.init(Main_Form.API_Server);
                sqL_DataGridView_藥品資料.Init(table);
                this.sqL_DataGridView_藥品資料.Init(table);
                this.sqL_DataGridView_藥品資料.RowsHeight = 60;
                this.sqL_DataGridView_藥品資料.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
                this.sqL_DataGridView_藥品資料.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品碼);
                this.sqL_DataGridView_藥品資料.Set_ColumnWidth(600, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
                //this.sqL_DataGridView_藥品資料.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);

                this.sqL_DataGridView_藥品資料.Set_ColumnText("藥碼", enum_雲端藥檔.藥品碼);
                this.sqL_DataGridView_藥品資料.Set_ColumnText("藥名", enum_雲端藥檔.藥品名稱);
                this.sqL_DataGridView_藥品資料.RowPostPaintingFinishedEvent += SqL_DataGridView_藥品資料_RowPostPaintingFinishedEvent;
                this.sqL_DataGridView_藥品資料.RowEnterEvent += SqL_DataGridView_藥品資料_RowEnterEvent;
                this.sqL_DataGridView_藥品資料.DataGridClearGridEvent += SqL_DataGridView_藥品資料_DataGridClearGridEvent;
                this.sqL_DataGridView_藥品資料.DataGridRowsChangeRefEvent += SqL_DataGridView_藥品資料_DataGridRowsChangeRefEvent;
                this.LoadFinishedEvent += Dialog_藥品群組_LoadFinishedEvent;
                comboBox_藥品資料_搜尋條件.SelectedIndex = 0;
                comboBox_藥品資料_搜尋條件.SelectedIndexChanged += ComboBox_藥品資料_搜尋條件_SelectedIndexChanged;
            }));
            this.rJ_Button_藥品群組_確認.MouseDownEvent += RJ_Button_藥品群組_確認_MouseDownEvent;
            this.rJ_Button_藥品群組_新增.MouseDownEvent += RJ_Button_藥品群組_新增_MouseDownEvent;
            this.rJ_Button_藥品群組_刪除.MouseDownEvent += RJ_Button_藥品群組_刪除_MouseDownEvent;
            comboBox_藥品群組.SelectedIndexChanged += ComboBox_藥品群組_SelectedIndexChanged;
            this.rJ_Button_藥品搜尋.MouseDownEvent += RJ_Button_藥品搜尋_MouseDownEvent;
            this.plC_RJ_Button_群組設定.MouseDownEvent += PlC_RJ_Button_群組設定_MouseDownEvent;
            this.plC_RJ_Button_新增品項.MouseDownEvent += PlC_RJ_Button_新增品項_MouseDownEvent;
            this.plC_RJ_Button_刪除品項.MouseDownEvent += PlC_RJ_Button_刪除品項_MouseDownEvent;
        }

    

        private void SqL_DataGridView_藥品資料_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            List<object[]> RowsList_buf = new List<object[]>();
            if (checkBox_只顯示調劑台品項.Checked)
            {
                List<medClass> medClasses_dps = medClass.get_dps_medClass(Main_Form.API_Server, Main_Form.ServerName);
                List<medClass> medClasses_dps_buf = new List<medClass>();
                medClasses_dps = (from temp in medClasses_dps
                                  where temp.DeviceBasics.Count > 0
                                  select temp).ToList();
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_dps = medClasses_dps.CoverToDictionaryByCode();
                for (int i = 0; i < RowsList.Count; i++)
                {
                    medClasses_dps_buf = keyValuePairs_medClasses_dps.SortDictionaryByCode(RowsList[i][(int)enum_雲端藥檔.藥品碼].ObjectToString());
                    if (medClasses_dps_buf.Count > 0)
                    {
                        RowsList_buf.Add(RowsList[i]);
                    }
                }

                RowsList = RowsList_buf;
            }
        }

        #region Function
        private void Function_更新群組列表()
        {
            this.Invoke(new Action(delegate
            {
                List<medGroupClass> medGroupClasses = medGroupClass.get_all_group(Main_Form.API_Server);
                List<string> list_str = new List<string>();
                for (int i = 0; i < medGroupClasses.Count; i++)
                {
                    if(medGroupClasses[i].顯示資訊.StringIsEmpty() || medGroupClasses[i].顯示資訊.Contains(Main_Form.ServerName))
                    {
                        list_str.Add(medGroupClasses[i].名稱);
                    }
                  
                }
                this.comboBox_藥品群組.DataSource = list_str.ToArray();
                this.Refresh();

            }));
        }
        #endregion
        #region Event
        private void ComboBox_藥品資料_搜尋條件_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "管制級別")
                {
                    comboBox_藥品資料_搜尋內容.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox_藥品資料_搜尋內容.DataSource = new string[] { "1", "2", "3", "4", "N", };
                    comboBox_藥品資料_搜尋內容.SelectedIndex = 0;
                    comboBox_藥品資料_搜尋內容.Invalidate();
                }
                else
                {
                    comboBox_藥品資料_搜尋內容.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox_藥品資料_搜尋內容.Invalidate();
                }
            }));

        }
        private void SqL_DataGridView_藥品資料_DataGridClearGridEvent()
        {
            this.sqL_DataGridView_藥品資料.ClearDataKeys();
        }
        private void SqL_DataGridView_藥品資料_RowPostPaintingFinishedEvent(DataGridViewRowPostPaintEventArgs e)
        {
      
        }
        private void SqL_DataGridView_藥品資料_RowEnterEvent(object[] RowValue)
        {
       
        }
        private void Dialog_藥品群組_LoadFinishedEvent(EventArgs e)
        {
            Function_更新群組列表();
        }
        private void PlC_RJ_Button_群組設定_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                Dialog_藥品群組設定 dialog_藥品群組設定 = new Dialog_藥品群組設定();
                if (dialog_藥品群組設定.ShowDialog() != DialogResult.Yes) return;
            }
            catch
            {

            }
            finally
            {
                Function_更新群組列表();
            }
            
        }
        private void RJ_Button_藥品群組_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            DialogResult dialogResult = DialogResult.None;
            try
            {
                IndexedDictionary<medClass> drugDict = dragDropListBox_已選藥品.GetBoundDictionary<medClass>();

                List<medClass> sortedList = drugDict.Items
                    .OrderBy(e => e.Index)
                    .Select((entry, idx) =>
                    {
                        entry.Content.排列號 = (idx + 1).ToString(); // 從 1 開始
                        return entry.Content;
                    })
                    .ToList();

                dialogResult = MyMessageBox.ShowDialog($"是否儲存群組資料,共<{sortedList.Count}>筆?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel);
                if (dialogResult != DialogResult.Yes) return;
                string text = comboBox_藥品群組.GetComboBoxText();  
                medGroupClass _medGroupClass = medGroupClass.get_all_group(Main_Form.API_Server, text);
                _medGroupClass.MedClasses = sortedList;
                medGroupClass.add_group(Main_Form.API_Server, _medGroupClass);
            }
            catch
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
                if (dialogResult == DialogResult.Yes)
                {
                    MyMessageBox.ShowDialog("儲存成功");
                }
            }            
        }
        private void RJ_Button_藥品群組_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_藥品群組_新增群組 dialog_藥品群組_新增群組 = new Dialog_藥品群組_新增群組();
            if (dialog_藥品群組_新增群組.ShowDialog() != DialogResult.Yes) return;
            Function_更新群組列表();

        }
        private void RJ_Button_藥品群組_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            try
            {
                if (this.comboBox_藥品群組.GetComboBoxText().StringIsEmpty())
                {
                    MyMessageBox.ShowDialog("未選擇群組");
                    return;
                }

                if (MyMessageBox.ShowDialog("是否刪除選取群組?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                medGroupClass _medGroupClass = medGroupClass.get_all_group(Main_Form.API_Server, this.comboBox_藥品群組.GetComboBoxText());
                medGroupClass.delete_group_by_guid(Main_Form.API_Server, _medGroupClass);
                Function_更新群組列表();

            }
            catch
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
        }
        private void RJ_Button_藥品搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            try
            {

            
                List<medClass> medClasses = new List<medClass>();

                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "全部顯示")
                {
                    medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                }
                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "藥碼")
                {
                    medClass medClass = medClass.get_med_clouds_by_code(Main_Form.API_Server, comboBox_藥品資料_搜尋內容.GetComboBoxText());
                    medClasses.Add(medClass);
                }
                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "藥名")
                {
                    medClasses = medClass.get_med_clouds_by_name(Main_Form.API_Server, comboBox_藥品資料_搜尋內容.GetComboBoxText());
                }
                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "中文名")
                {
                    medClasses = medClass.get_med_clouds_by_chtname(Main_Form.API_Server, comboBox_藥品資料_搜尋內容.GetComboBoxText());
                }
                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "管制級別")
                {
                    medClasses = medClass.get_med_clouds_by_durgkind(Main_Form.API_Server, comboBox_藥品資料_搜尋內容.GetComboBoxText());
                }
                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "有儲位藥品")
                {
                    medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                    List<medClass> medClasses_dps = medClass.get_dps_medClass(Main_Form.API_Server, Main_Form.ServerName);
                    medClasses_dps = (from temp in medClasses_dps
                                      where temp.DeviceBasics.Count > 0
                                      select temp).ToList();
                    Dictionary<string, List<medClass>> keyValuePairs_medClasses_dps = medClasses_dps.CoverToDictionaryByCode();

                    medClasses = (from temp in medClasses
                                  where keyValuePairs_medClasses_dps.SortDictionaryByCode(temp.藥品碼).Count > 0
                                  select temp).ToList();
                }
                if (comboBox_藥品資料_搜尋條件.GetComboBoxText() == "已選藥品")
                {
                    medGroupClass medGroupClass = medGroupClass.get_all_group(Main_Form.API_Server, comboBox_藥品群組.GetComboBoxText());
                    medClasses = medGroupClass.MedClasses;
                }

                List<object[]> list_med = medClasses.ClassToSQL<medClass, enum_雲端藥檔>();
                this.sqL_DataGridView_藥品資料.RefreshGrid(list_med);
            }
            catch (Exception ex)
            {
      
            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
        }
        private void PlC_RJ_Button_新增品項_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                List<object[]> list_value = sqL_DataGridView_藥品資料.Get_All_Select_RowsValues();
                if (list_value.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選擇品項");
                    return;
                }
                List<medClass> medClasses_選擇品項 = list_value.SQLToClass<medClass, enum_雲端藥檔>();
                IndexedDictionary<medClass> drugDict = dragDropListBox_已選藥品.GetBoundDictionary<medClass>();

                // 取得已存在於 dragDropListBox 的藥品碼清單
                var existingCodes = drugDict.Items.Select(x => x.Content.藥品碼).ToHashSet();

                // 挑出 medClasses_選擇品項 中不存在於 dragDropListBox 的藥品
                var 未存在品項 = medClasses_選擇品項
                    .Where(m => !existingCodes.Contains(m.藥品碼))
                    .ToList();

                // 測試輸出或顯示
                if (未存在品項.Count == 0)
                {
                    MyMessageBox.ShowDialog("所有選擇品項皆已存在！");
                }
                else
                {
                    // 加入 dragDropListBox 或做其他處理
                    foreach (var med in 未存在品項)
                    {
                        drugDict.Add($"({med.藥品碼}){med.藥品名稱}", med);
                    }

                    dragDropListBox_已選藥品.Bind(drugDict); // 重新綁定以顯示更新
                }
            }));
           
        }
        private void ComboBox_藥品群組_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadingForm.ShowLoadingForm();
            try
            {
                medGroupClass medGroupClass = medGroupClass.get_all_group(Main_Form.API_Server, comboBox_藥品群組.GetComboBoxText());
                this.sqL_DataGridView_藥品資料.SetDataKeys(medGroupClass.MedClasses.ClassToSQL<medClass, enum_雲端藥檔>(), true);
                this.sqL_DataGridView_藥品資料.RefreshGrid();
                IndexedDictionary<medClass> drugDict = new IndexedDictionary<medClass>();
                for (int i = 0; i < medGroupClass.MedClasses.Count; i++)
                {
                    medClass medClass = medGroupClass.MedClasses[i];
                    drugDict.Add($"({medClass.藥品碼}){medClass.藥品名稱}", medClass);
                }
                dragDropListBox_已選藥品.Bind(drugDict);

            }
            catch
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
          

        }
        private void PlC_RJ_Button_刪除品項_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                List<medClass> medClasses = this.dragDropListBox_已選藥品.GetSelectedContent<medClass>();
                if (medClasses.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選擇品項");
                    return;
                }
                dragDropListBox_已選藥品.RemoveSelectedItems<medClass>();
            }));
       
        }
        #endregion
    }
}
