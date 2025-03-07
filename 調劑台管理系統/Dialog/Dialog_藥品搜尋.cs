﻿using System;
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

namespace 調劑台管理系統
{
    public partial class Dialog_藥品搜尋 : MyDialog
    {
        public medClass Value = new medClass();
        public bool IsDeviceSerch = false;
        public Dialog_藥品搜尋()
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
                this.Load += Dialog_藥品搜尋_Load;
                this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
                this.rJ_Button_返回.MouseDownEvent += RJ_Button_返回_MouseDownEvent;
                this.rJ_Button_搜尋.MouseDownEvent += RJ_Button_搜尋_MouseDownEvent;

                this.LoadFinishedEvent += Dialog_藥品搜尋_LoadFinishedEvent;
            }));

        }

 
        private void Dialog_藥品搜尋_Load(object sender, EventArgs e)
        {
          
            Table table = medClass.init(Main_Form.API_Server);
            sqL_DataGridView_藥品搜尋.Init(table);
            sqL_DataGridView_藥品搜尋.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
            sqL_DataGridView_藥品搜尋.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.藥品碼);
            sqL_DataGridView_藥品搜尋.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
            sqL_DataGridView_藥品搜尋.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.中文名稱);
            sqL_DataGridView_藥品搜尋.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);
            sqL_DataGridView_藥品搜尋.Set_ColumnText("藥碼", enum_雲端藥檔.藥品碼);
            sqL_DataGridView_藥品搜尋.Set_ColumnText("藥名", enum_雲端藥檔.藥品名稱);
            sqL_DataGridView_藥品搜尋.Set_ColumnText("中文名", enum_雲端藥檔.中文名稱);
            sqL_DataGridView_藥品搜尋.Set_ColumnText("單位", enum_雲端藥檔.包裝單位);
        }
        private void Dialog_藥品搜尋_LoadFinishedEvent(EventArgs e)
        {
            this.comboBox_搜尋條件.SelectedIndex = 0;
        }

        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品搜尋.Get_All_Select_RowsValues();

            List<medClass> medClasses = list_value.SQLToClass<medClass, enum_雲端藥檔>();
            if(medClasses.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;                    
            }
            this.Value = medClasses[0];

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void RJ_Button_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                string text = textBox_搜尋內容.Texts;
                string cmb_text = "";
                this.Invoke(new Action(delegate
                {
                    cmb_text = this.comboBox_搜尋條件.Text;
                }));

                List<medClass> medClasses = new List<medClass>();
                LoadingForm.ShowLoadingForm();
                if (cmb_text == "全部顯示")
                {
                    medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                }
                if (cmb_text == "藥碼")
                {
                    if (text.StringIsEmpty())
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    medClass medClass = medClass.get_med_clouds_by_code(Main_Form.API_Server, text);
                    medClasses.Add(medClass);
                }
                if (cmb_text == "藥名")
                {
                    if (text.StringIsEmpty())
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    medClasses = medClass.get_med_clouds_by_name(Main_Form.API_Server, text);
                }
                if (cmb_text == "中文名")
                {
                    if (text.StringIsEmpty())
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    medClasses = medClass.get_med_clouds_by_chtname(Main_Form.API_Server, text);
                }
                if(IsDeviceSerch)
                {
                    medClasses = (from temp in medClasses
                                  where temp.DeviceBasics.Count > 0
                                  select temp).ToList();
                }


                if (medClasses.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                medClasses = (from temp in medClasses
                              where temp.開檔狀態 == enum_開檔狀態.開檔中.GetEnumName() || temp.開檔狀態.StringIsEmpty()
                              select temp).ToList();

                List<object[]> list_value = medClasses.ClassToSQL<medClass, enum_雲端藥檔>();



                this.sqL_DataGridView_藥品搜尋.RefreshGrid(list_value);

            }
            catch
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
            
        }
    }
}
