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
    public partial class Dialog_申領 : MyDialog
    {
        static public Dialog_申領 myDialog;
        static public Dialog_申領 GetForm()
        {
            if (myDialog != null)
            {
                return myDialog;
            }
            else
            {
                myDialog = new Dialog_申領();
                return myDialog;
            }
        }
        public Dialog_申領()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
          

            this.Load += Dialog_申領_Load;
            this.LoadFinishedEvent += Dialog_申領_LoadFinishedEvent;
            this.ShowDialogEvent += Dialog_申領_ShowDialogEvent;
            this.FormClosing += Dialog_申領_FormClosing;

        }
        #region Event
        private void Dialog_申領_ShowDialogEvent()
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
        private void Dialog_申領_LoadFinishedEvent(EventArgs e)
        {
           
        }
        private void Dialog_申領_Load(object sender, EventArgs e)
        {
            Table table = materialRequisitionClass.init(Main_Form.API_Server);
            sqL_DataGridView_申領品項.RowsHeight = 50;
            sqL_DataGridView_申領品項.Init(table);
            sqL_DataGridView_申領品項.Set_ColumnVisible(false, new enum_materialRequisition().GetEnumNames());
            sqL_DataGridView_申領品項.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.申領類別);
            sqL_DataGridView_申領品項.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.藥碼);
            sqL_DataGridView_申領品項.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.藥名);
            sqL_DataGridView_申領品項.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_materialRequisition.包裝量);
            sqL_DataGridView_申領品項.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.包裝單位);
            sqL_DataGridView_申領品項.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_materialRequisition.申領量);
            sqL_DataGridView_申領品項.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_materialRequisition.申領人員);
            sqL_DataGridView_申領品項.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_materialRequisition.申領時間);
            sqL_DataGridView_申領品項.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.申領單位);
            //sqL_DataGridView_申領品項.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.核撥單位);
            sqL_DataGridView_申領品項.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_materialRequisition.實撥庫庫存);
            sqL_DataGridView_申領品項.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_materialRequisition.狀態);
            sqL_DataGridView_申領品項.Set_ColumnText("單位", enum_materialRequisition.包裝單位);
            sqL_DataGridView_申領品項.Set_ColumnText("藥庫庫存", enum_materialRequisition.實撥庫庫存);
            dateTimeIntervelPicker_報表日期.SetDateTime(DateTime.Now.GetStartDate(), DateTime.Now.GetEndDate());
            this.rJ_Button_搜尋.MouseDownEvent += RJ_Button_搜尋_MouseDownEvent;
            this.rJ_Button_緊急申領語音上傳.MouseDownEvent += RJ_Button_緊急申領語音上傳_MouseDownEvent;
            this.rJ_Button_一般申領語音上傳.MouseDownEvent += RJ_Button_一般申領語音上傳_MouseDownEvent;
            this.comboBox_搜尋條件.SelectedIndex = 0;
        }

        private void RJ_Button_一般申領語音上傳_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                // 建立 OpenFileDialog 實例
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    // 設定篩選條件只允許 .wav 檔案
                    Filter = "WAV 檔案 (*.wav)|*.wav",
                    Title = "請選擇一個 WAV 檔案"
                };

                // 顯示對話框，並判斷是否選擇了檔案
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 取得所選擇的檔案路徑
                    string filePath = openFileDialog.FileName;
                    materialRequisitionClass.emg_voice_upload(Main_Form.API_Server, filePath);
                    MyMessageBox.ShowDialog("上傳完成");
                }

            }));
        }

        private void RJ_Button_緊急申領語音上傳_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                // 建立 OpenFileDialog 實例
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    // 設定篩選條件只允許 .wav 檔案
                    Filter = "WAV 檔案 (*.wav)|*.wav",
                    Title = "請選擇一個 WAV 檔案"
                };

                // 顯示對話框，並判斷是否選擇了檔案
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 取得所選擇的檔案路徑
                    string filePath = openFileDialog.FileName;
                    materialRequisitionClass.normal_voice_upload(Main_Form.API_Server, filePath);
                    MyMessageBox.ShowDialog("上傳完成");
                }
               
            }));
        }
        private void Dialog_申領_FormClosing(object sender, FormClosingEventArgs e)
        {
            myDialog = null;
        }

        #endregion
        private void RJ_Button_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<materialRequisitionClass> materialRequisitionClasses = new List<materialRequisitionClass>();
            LoadingForm.ShowLoadingForm();
            try
            {
                DateTime dateTime_st = dateTimeIntervelPicker_報表日期.StartTime;
                DateTime dateTime_end = dateTimeIntervelPicker_報表日期.EndTime;

                 materialRequisitionClasses = materialRequisitionClass.get_by_requestTime(Main_Form.API_Server, dateTime_st, dateTime_end);
                if (this.comboBox_搜尋條件.GetComboBoxText() == "藥碼")
                {
                    if(comboBox_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("未輸入搜尋條件");
                        return;
                    }
                    materialRequisitionClasses = (from temp in materialRequisitionClasses
                                                  where temp.藥碼.StartsWith(comboBox_搜尋內容.GetComboBoxText())
                                                  select temp).ToList();
                }
                if (this.comboBox_搜尋條件.GetComboBoxText() == "藥名")
                {
                    if (comboBox_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("未輸入搜尋條件");
                        return;
                    }
                    materialRequisitionClasses = (from temp in materialRequisitionClasses
                                                  where temp.藥名.StartsWith(comboBox_搜尋內容.GetComboBoxText())
                                                  select temp).ToList();
                }
                if (this.comboBox_搜尋條件.GetComboBoxText() == "申領單位")
                {
                    if (comboBox_搜尋內容.GetComboBoxText().StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("未輸入搜尋條件");
                        return;
                    }
                    materialRequisitionClasses = (from temp in materialRequisitionClasses
                                                  where temp.申領單位.StartsWith(comboBox_搜尋內容.GetComboBoxText())
                                                  select temp).ToList();
                }
                materialRequisitionClasses.Sort(new materialRequisitionClass.ICP_By_requestTime());
                List<object[]> list_value = materialRequisitionClasses.ClassToSQL<materialRequisitionClass, enum_materialRequisition>();
                sqL_DataGridView_申領品項.RefreshGrid(list_value);
            }
            catch
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
                if (materialRequisitionClasses.Count == 0)
                {
                    MyMessageBox.ShowDialog("查無資料");
                }
            }

        }
        private void comboBox_搜尋條件_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
