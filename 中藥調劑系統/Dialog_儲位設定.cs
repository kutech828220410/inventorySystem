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

namespace 中藥調劑系統
{
    public enum enum_儲位列表
    {
        [Description("IP,VARCHAR,15,NONE")]
        IP,
        [Description("名稱,VARCHAR,15,NONE")]
        名稱,
    }
    public partial class Dialog_儲位設定 : MyDialog
    {
        public Dialog_儲位設定()
        {
            InitializeComponent();
            this.Load += Dialog_儲位設定_Load;
            this.Shown += Dialog_儲位設定_Shown;
        }

        private void Dialog_儲位設定_Shown(object sender, EventArgs e)
        {
            comboBox_藥品資料_搜尋條件.SelectedIndex = 0;
            this.Refresh();
        }

        private void Dialog_儲位設定_Load(object sender, EventArgs e)
        {
         

            Table table_藥品資料 = medClass.Init(Main_Form.API_Server);
            this.sqL_DataGridView_藥品資料.RowsHeight = 40;
            this.sqL_DataGridView_藥品資料.Init(table_藥品資料);
            this.sqL_DataGridView_藥品資料.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
            this.sqL_DataGridView_藥品資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_藥品資料.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_藥品資料.Set_ColumnText("藥碼", enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_藥品資料.Set_ColumnText("藥名", enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_藥品資料.Set_ColumnText("單位", enum_雲端藥檔.包裝單位);

            Table table_儲位列表 = new Table(new enum_儲位列表());
            this.sqL_DataGridView_層架列表.RowsHeight = 40;
            this.sqL_DataGridView_層架列表.Init(table_儲位列表);
            this.sqL_DataGridView_層架列表.Set_ColumnVisible(false, new enum_儲位列表().GetEnumNames());
            this.sqL_DataGridView_層架列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.IP);
            this.sqL_DataGridView_層架列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.名稱);
            this.sqL_DataGridView_層架列表.MouseDown += SqL_DataGridView_層架列表_MouseDown;

            this.rJ_Button_藥品資料_搜尋.MouseDownEvent += RJ_Button_藥品資料_搜尋_MouseDownEvent;

            this.Function_層架列表_Refresh();
        }

  
        #region Function
        private void Function_層架列表_Refresh()
        {
            List<RowsLED> rowsLEDs = deviceApiClass.GetRowsLED(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType);
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < rowsLEDs.Count; i++)
            {
                object[] value = new object[new enum_儲位列表().GetLength()];
                value[(int)enum_儲位列表.IP] = rowsLEDs[i].IP;
                value[(int)enum_儲位列表.名稱] = rowsLEDs[i].Name;
                list_value.Add(value);
            }
            this.sqL_DataGridView_層架列表.RefreshGrid(list_value);
        }
        #endregion
        #region Event
        private void SqL_DataGridView_層架列表_MouseDown(object sender, MouseEventArgs e)
        {
            Function_層架列表_Refresh();
        }
        private void RJ_Button_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                string text = textBox_處方搜尋_搜尋內容.Text;
                string cmb_text = "";
                this.Invoke(new Action(delegate { cmb_text = comboBox_藥品資料_搜尋條件.Text; }));
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
                    this.sqL_DataGridView_藥品資料.RefreshGrid(list_value);

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
                    this.sqL_DataGridView_藥品資料.RefreshGrid(list_value);
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
                    this.sqL_DataGridView_藥品資料.RefreshGrid(list_value);
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
