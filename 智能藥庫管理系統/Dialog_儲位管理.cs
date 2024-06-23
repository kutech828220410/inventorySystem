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

namespace 智能藥庫管理系統
{
    public partial class Dialog_儲位管理 : MyDialog
    {
        public enum enum_儲架電子紙列表
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("IP,VARCHAR,15,NONE")]
            IP,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
        }

        public Dialog_儲位管理()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
                
            }));
            this.Load += Dialog_儲位管理_Load;
            this.LoadFinishedEvent += Dialog_儲位管理_LoadFinishedEvent;
            this.rJ_Button_儲架電子紙_藥品資料_搜尋.MouseDownEvent += RJ_Button_儲架電子紙_藥品資料_搜尋_MouseDownEvent;
       
        }

        private void Dialog_儲位管理_LoadFinishedEvent(EventArgs e)
        {
            this.Refresh();
        }

        #region Event
        private void SqL_DataGridView_儲架電子紙_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {

        }
        private void Dialog_儲位管理_Load(object sender, EventArgs e)
        {
            this.comboBox_儲架電子紙_藥品資料_搜尋條件.SelectedIndex = 0;

            Table table_藥品資料 = medClass.init(Main_Form.API_Server);
            this.sqL_DataGridView_儲架電子紙_藥品資料.RowsHeight = 40;
            this.sqL_DataGridView_儲架電子紙_藥品資料.Init(table_藥品資料);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnText("藥碼", enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnText("藥名", enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnText("單位", enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_儲架電子紙_藥品資料.RowDoubleClickEvent += SqL_DataGridView_儲架電子紙_藥品資料_RowDoubleClickEvent;

            Table table_儲架電子紙列表 = new Table(new enum_儲架電子紙列表());
            this.sqL_DataGridView_儲架電子紙列表.Init(table_儲架電子紙列表);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnVisible(false, new enum_儲架電子紙列表().GetEnumNames());
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_儲架電子紙列表.IP);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.藥碼);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(600, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.藥名);

        }
        private void RJ_Button_儲架電子紙_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                string text = textBox_儲架電子紙_藥品資料_搜尋內容.Text;
                string cmb_text = "";
                this.Invoke(new Action(delegate { cmb_text = comboBox_儲架電子紙_藥品資料_搜尋條件.Text; }));
                LoadingForm.ShowLoadingForm();
                List<medClass> medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                List<medClass> medClasses_buf = new List<medClass>();
                if (cmb_text == "藥碼")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品碼.ToUpper().Contains(text)
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_儲架電子紙_藥品資料.RefreshGrid(list_value);

                }
                if (cmb_text == "藥名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品名稱.ToUpper().Contains(text)
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_儲架電子紙_藥品資料.RefreshGrid(list_value);
                }
                if (cmb_text == "中文名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.中文名稱.ToUpper().Contains(text)
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_儲架電子紙_藥品資料.RefreshGrid(list_value);
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
       
        #endregion
    }
}
