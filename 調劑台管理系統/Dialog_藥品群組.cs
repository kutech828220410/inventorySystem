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

namespace 調劑台管理系統
{
    public partial class Dialog_藥品群組 : MyDialog
    {
        private class KeysClass
        {
            public string GUID = "";
            public bool check = false;
        }
        private List<KeysClass> keysClasses = new List<KeysClass>();
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
                this.sqL_DataGridView_藥品資料.Set_ColumnWidth(1000, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
                this.sqL_DataGridView_藥品資料.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);
                this.sqL_DataGridView_藥品資料.RowPostPaintingEvent += SqL_DataGridView_藥品資料_RowPostPaintingEvent;
                this.LoadFinishedEvent += Dialog_藥品群組_LoadFinishedEvent;
                comboBox_藥品資料_搜尋條件.SelectedIndex = 0;
            }));
            this.rJ_Button_藥品群組_選擇.MouseDownEvent += RJ_Button_藥品群組_選擇_MouseDownEvent;
            this.rJ_Button_藥品群組_新增.MouseDownEvent += RJ_Button_藥品群組_新增_MouseDownEvent;
            this.rJ_Button_藥品群組_刪除.MouseDownEvent += RJ_Button_藥品群組_刪除_MouseDownEvent;

            this.rJ_Button_藥品搜尋.MouseDownEvent += RJ_Button_藥品搜尋_MouseDownEvent;
        }

        #region Event
        private void SqL_DataGridView_藥品資料_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            if (sqL_DataGridView_藥品資料.Checked[e.RowIndex])
            {
                (int Id, string Name, int Age) person = (1, "John Doe", 30);
            }
            Color row_Backcolor = Color.GreenYellow;
            using (Brush brush = new SolidBrush(row_Backcolor))
            {
                e.Graphics.FillRectangle(brush, e.RowBounds);
            }
        }
        private void Dialog_藥品群組_LoadFinishedEvent(EventArgs e)
        {
            List<medGroupClass> medGroupClasses = medGroupClass.get_all_group(Main_Form.API_Server);
            List<string> list_str = new List<string>();
            for (int i = 0; i < medGroupClasses.Count; i++)
            {
                list_str.Add(medGroupClasses[i].名稱);
            }
            this.comboBox_藥品群組.DataSource = list_str.ToArray();
            this.Refresh();
        }
        private void RJ_Button_藥品群組_選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            try 
            {
                string text = "";
                this.Invoke(new Action(delegate
                {
                    text = comboBox_藥品群組.Text;
                }));
                medGroupClass medGroupClass = medGroupClass.get_all_group(Main_Form.API_Server, text);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
           
        }
        private void RJ_Button_藥品群組_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            
        }
        private void RJ_Button_藥品群組_新增_MouseDownEvent(MouseEventArgs mevent)
        {
           
        }
        private void RJ_Button_藥品搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            try
            {
                List<medClass> medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                List<object[]> list_med = medClasses.ClassToSQL<medClass , enum_雲端藥檔>();
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
        #endregion
    }
}
