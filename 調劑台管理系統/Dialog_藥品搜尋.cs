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

namespace 調劑台管理系統
{
    public partial class Dialog_藥品搜尋 : MyDialog
    {
        public Dialog_藥品搜尋()
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
                this.Load += Dialog_藥品搜尋_Load;
                this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
                this.rJ_Button_返回.MouseDownEvent += RJ_Button_返回_MouseDownEvent;
                this.rJ_Button_搜尋.MouseDownEvent += RJ_Button_搜尋_MouseDownEvent;
            }));

        }

     

        private void Dialog_藥品搜尋_Load(object sender, EventArgs e)
        {
            this.comboBox_搜尋條件.SelectedIndex = 0;
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
            sqL_DataGridView_藥品搜尋.Set_ColumnText("單位", enum_雲端藥檔.包裝數量);
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
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
            LoadingForm.ShowLoadingForm();


            LoadingForm.CloseLoadingForm();
        }
    }
}
