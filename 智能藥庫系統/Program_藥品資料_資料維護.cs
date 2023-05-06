using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using System.Text.RegularExpressions;
namespace 智能藥庫系統
{

    public partial class Form1 : Form
    {
        public enum enum_藥品資料_資料維護_雲端藥檔
        {
            GUID,
            藥品碼,
            料號,
            中文名稱,
            藥品名稱,
            藥品學名,
            健保碼,
            包裝單位,
            包裝數量,
            最小包裝單位,
            最小包裝數量,
            藥品條碼1,
            藥品條碼2,
            警訊藥品,
            管制級別,
            類別
        }
        public enum enum_藥品資料_資料維護_雲端藥檔_匯出
        {
            藥碼,
            料號,
            中文名稱,
            藥名,
            商品名,
            健保碼,
            包裝單位,
            包裝數量,
            最小包裝單位,
            最小包裝數量,
            藥品條碼1,
            藥品條碼2,
        }
        public enum enum_藥品資料_資料維護_雲端藥檔_匯入
        {
            [Description("藥品碼")]
            藥碼,
            料號,
            中文名稱,
            [Description("藥品名稱")]
            藥名,
            [Description("藥品學名")]
            商品名,
            健保碼,
            包裝單位,
            包裝數量,
            最小包裝單位,
            最小包裝數量,
            藥品條碼1,
            藥品條碼2,
        }
        public enum ContextMenuStrip_藥品資料_資料維護_本地藥檔
        {         
            列出DC藥品,
            列出異動藥品,
            搜尋選取藥品,
            刪除選取藥品,
            藥品群組設定,
        }
        public enum enum_藥品資料_資料維護_本地藥檔
        {
            GUID,
            藥品碼,
            中文名稱,
            藥品名稱,
            藥品學名,
            藥品群組,
            健保碼,
            包裝單位,
            包裝數量,
            最小包裝單位,
            最小包裝數量,
            藥品條碼1,
            藥品條碼2,
        }

  
        private void sub_Program_藥品資料_資料維護_Init()
        {
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔, dBConfigClass.DB_Medicine_Cloud);

            this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.Init();
            
            if (!this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_IsTableCreat())
            {
                this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_CreateTable();
            }
            else this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_CheckAllColumnName(true);
            this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.DataGridRefreshEvent += SqL_DataGridView_藥品資料_資料維護_雲端藥檔_DataGridRefreshEvent;

   

        


            this.plC_RJ_Button_藥品資料_資料維護_雲端藥檔_搜尋.MouseDownEvent += PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_資料維護_雲端藥檔_選取資料刪除.MouseDownEvent += PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_選取資料刪除_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_資料維護_雲端藥檔_匯入.MouseDownEvent += PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_匯入_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_資料維護_雲端藥檔_匯出.MouseDownEvent += PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_匯出_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_資料維護_雲端藥檔_全部顯示.MouseDownEvent += PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_全部顯示_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_資料維護_雲端藥檔_藥品條碼1設定.MouseDownEvent += PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_藥品條碼1設定_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_資料維護_雲端藥檔_藥品條碼2設定.MouseDownEvent += PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_藥品條碼2設定_MouseDownEvent;

       


            this.plC_UI_Init.Add_Method(sub_Program_藥品資料_資料維護);
        }

        private bool flag_藥品資料_藥品群組_資料維護 = false;
        private void sub_Program_藥品資料_資料維護()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥品資料" && this.plC_ScreenPage_藥品資料.PageText == "資料維護")
            {
                if (!this.flag_藥品資料_藥品群組_資料維護)
                {
                    this.flag_藥品資料_藥品群組_資料維護 = true;
                }
            }
            else
            {
                this.flag_藥品資料_藥品群組_資料維護 = false;
            }
        }
        #region Function

        #region 雲端藥檔
       
        #endregion

        #endregion
        #region Event

        #region 雲端藥檔
        private void SqL_DataGridView_藥品資料_資料維護_雲端藥檔_DataGridRefreshEvent()
        {
     
        }
        private void sqL_DataGridView_藥品資料_資料維護_雲端藥檔_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
            }
        }
        private void PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_選取資料刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (MyMessageBox.ShowDialog("是否刪除選取資料", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    List<object[]> list_value = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.Get_All_Select_RowsValues();
                    List<object> list_delete_serchValue = new List<object>();
                    for (int i = 0; i < list_value.Count; i++)
                    {
                        string GUID = list_value[i][(int)enum_藥品資料_資料維護_雲端藥檔.GUID].ObjectToString();
                        list_delete_serchValue.Add(GUID);
                    }
                    this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_DeleteExtra(enum_藥品資料_資料維護_雲端藥檔.GUID.GetEnumName(), list_delete_serchValue, true);
                    this.Cursor = Cursors.Default;
                }
            }));
   
        }
        private void PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult dialogResult = DialogResult.None;
            this.Invoke(new Action(delegate
            {
                dialogResult = this.saveFileDialog_SaveExcel.ShowDialog();
            }));
            if (dialogResult == DialogResult.OK)
            {
                this.Invoke(new Action(delegate
                {
                    this.Cursor = Cursors.WaitCursor;
                }));
                DataTable dataTable = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.GetDataTable();
                dataTable = dataTable.ReorderTable(new enum_藥品資料_資料維護_雲端藥檔_匯出());
                string Extension = System.IO.Path.GetExtension(this.saveFileDialog_SaveExcel.FileName);
                if (Extension == ".txt")
                {
                    CSVHelper.SaveFile(dataTable, this.saveFileDialog_SaveExcel.FileName);
                }
                else if (Extension == ".xls")
                {
                    MyOffice.ExcelClass.NPOI_SaveFile(dataTable, this.saveFileDialog_SaveExcel.FileName);
                }
                this.Invoke(new Action(delegate
                {
                    this.Cursor = Cursors.Default;
                }));
              
                MyMessageBox.ShowDialog("匯出完成");

            }
        }
        private void PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult dialogResult = DialogResult.None;
            this.Invoke(new Action(delegate
            {
                dialogResult = this.openFileDialog_LoadExcel.ShowDialog();
            }));
            if (dialogResult == DialogResult.OK)
            {
                this.Invoke(new Action(delegate
                {
                    this.Cursor = Cursors.WaitCursor;
                }));
         
                DataTable dataTable = new DataTable();

                string Extension = System.IO.Path.GetExtension(this.openFileDialog_LoadExcel.FileName);
                if (Extension == ".txt")
                {
                    CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                }
                else if (Extension == ".xls")
                {
                    dataTable = MyOffice.ExcelClass.NPOI_LoadFile(this.openFileDialog_LoadExcel.FileName);
                }

                DataTable datatable_buf = dataTable.ReorderTable(new enum_藥品資料_資料維護_雲端藥檔_匯入());
                if (datatable_buf == null)
                {
                    this.Invoke(new Action(delegate
                    {
                        MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                        this.Cursor = Cursors.Default;
                        
                    }));
                    return;
                }
                List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();
                List<object[]> list_SQL_Value = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_GetAllRows(false);
                List<object[]> list_Add = new List<object[]>();
                List<object[]> list_Add_buf = new List<object[]>();
                List<object[]> list_Delete_ColumnName = new List<object[]>();
                List<object[]> list_Delete_SerchValue = new List<object[]>();
                List<string> list_Replace_SerchValue = new List<string>();
                List<object[]> list_Replace_Value = new List<object[]>();
                List<object[]> list_SQL_Value_buf = new List<object[]>();
                for (int i = 0; i < list_LoadValue.Count; i++)
                {
                    object[] value_load = list_LoadValue[i];
                    value_load = value_load.CopyRow(new enum_藥品資料_資料維護_雲端藥檔_匯入().GetEnumDescriptions(), new enum_藥品資料_資料維護_雲端藥檔().GetEnumNames());
                    value_load[(int)enum_藥品資料_資料維護_雲端藥檔.藥品碼] = this.Function_藥品碼檢查(value_load[(int)enum_藥品資料_資料維護_雲端藥檔.藥品碼].ObjectToString());
                    list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_藥品資料_資料維護_雲端藥檔.藥品碼, value_load[(int)enum_藥品資料_資料維護_雲端藥檔.藥品碼].ObjectToString());
                    if (list_SQL_Value_buf.Count > 0)
                    {
                        object[] value_SQL = list_SQL_Value_buf[0];
                        value_load[(int)enum_藥品資料_資料維護_雲端藥檔.GUID] = value_SQL[(int)enum_藥品資料_資料維護_雲端藥檔.GUID];
                        value_load[(int)enum_藥品資料_資料維護_雲端藥檔.包裝數量] = Regex.Replace(value_load[(int)enum_藥品資料_資料維護_雲端藥檔.包裝數量].ObjectToString(), "[^0-9]", "");
                        bool flag_Equal = value_load.IsEqual(value_SQL);
                        if (!flag_Equal)
                        {
                            list_Replace_SerchValue.Add(value_load[(int)enum_藥品資料_資料維護_雲端藥檔.GUID].ObjectToString());
                            list_Replace_Value.Add(value_load);
                        }
                    }
                    else
                    {
                        value_load[(int)enum_藥品資料_資料維護_雲端藥檔.GUID] = Guid.NewGuid().ToString();
                        value_load[(int)enum_藥品資料_資料維護_雲端藥檔.包裝數量] = Regex.Replace(value_load[(int)enum_藥品資料_資料維護_雲端藥檔.包裝數量].ObjectToString(), "[^0-9]", "");
                        list_Add_buf = list_Add.GetRows((int)enum_藥品資料_資料維護_雲端藥檔.藥品碼, value_load[(int)enum_藥品資料_資料維護_雲端藥檔.藥品碼].ObjectToString());

                        if (list_Add_buf.Count == 0)
                        {
                            list_Add.Add(value_load);
                        }
                       
                    }
                }
                this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_AddRows(list_Add, false);
                this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_ReplaceExtra(list_Replace_Value, false);
         
                this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_GetAllRows(true);
                this.Invoke(new Action(delegate
                {
                    this.Cursor = Cursors.Default;
                    MyMessageBox.ShowDialog("匯入完成!");
                }));
            }
        }
        private void PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_GetAllRows(false);
            if (!this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_藥品碼.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_資料維護_雲端藥檔.藥品碼, this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_藥品碼.Text);
            }
            if (!this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_中文名稱.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_資料維護_雲端藥檔.中文名稱, this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_中文名稱.Text);
            }
            if (!this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_中文名稱.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_資料維護_雲端藥檔.中文名稱, this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_中文名稱.Text);
            }
            if (!this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_藥品名稱.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_資料維護_雲端藥檔.藥品名稱, this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_藥品名稱.Text);
            }
            if (!this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_藥品學名.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_資料維護_雲端藥檔.藥品學名, this.rJ_TextBox_藥品資料_資料維護_雲端藥檔_藥品學名.Text);
            }
         
            this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.RefreshGrid(list_value);
        }
    
  
        private void PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_全部顯示_MouseDownEvent(MouseEventArgs mevent)
        {
            sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_GetAllRows(true);
        }
        private void PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_藥品條碼1設定_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                this.Invoke(new Action(delegate
                {
                    MyMessageBox.ShowDialog("未選取資料!");
                }));
                return;
            }
            this.Invoke(new Action(delegate
            {
                Dialog_TextBox dialog_TextBox = new Dialog_TextBox("藥品條碼1");
                if (dialog_TextBox.ShowDialog() == DialogResult.Yes)
                {
                    list_value[0][(int)enum_藥品資料_資料維護_雲端藥檔.藥品條碼1] = dialog_TextBox.Value;
                    this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_Replace(list_value[0], true);
                }
            }));


        }
        private void PlC_RJ_Button_藥品資料_資料維護_雲端藥檔_藥品條碼2設定_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                this.Invoke(new Action(delegate
                {
                    MyMessageBox.ShowDialog("未選取資料!");
                }));
                return;
            }
            this.Invoke(new Action(delegate
            {
                Dialog_TextBox dialog_TextBox = new Dialog_TextBox("藥品條碼2");
                if (dialog_TextBox.ShowDialog() == DialogResult.Yes)
                {
                    list_value[0][(int)enum_藥品資料_資料維護_雲端藥檔.藥品條碼2] = dialog_TextBox.Value;
                    this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_Replace(list_value[0], true);
                }
            }));
        }
        #endregion
        
        #endregion
    }


}
