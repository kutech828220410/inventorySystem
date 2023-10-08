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
using SQLUI;
using H_Pannel_lib;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace 勤務傳送櫃
{
    public partial class Form1 : Form
    {
        #region Box_Index_Table
        enum enum_Box_Index_Table
        {
            GUID,
            Number,
            IP,
            RFID_num,
            Lock_output_num,
            Lock_input_num,
            Sensor_input_num,
            Led_output_num,
        }
        enum enum_Box_Index_Table_匯出
        {
            Number,
            IP,
            RFID_num,
            Lock_output_num,
            Lock_input_num,
            Sensor_input_num,
            Led_output_num,
        }
        enum enum_Box_Index_Table_匯入
        {
            Number,
            IP,
            RFID_num,
            Lock_output_num,
            Lock_input_num,
            Sensor_input_num,
            Led_output_num,
        }

        #endregion

        private void sub_Program_系統頁面_Init()
        {
            this.sqL_DataGridView_Box_Index_Table.Init();
            if (!this.sqL_DataGridView_Box_Index_Table.SQL_IsTableCreat())
            {
                this.sqL_DataGridView_Box_Index_Table.SQL_CreateTable();
            }
            this.sqL_DataGridView_Box_Index_Table.DataGridRowsChangeEvent += SqL_DataGridView_Box_Index_Table_DataGridRowsChangeEvent;

            this.plC_RJ_Button_Box_Index_Table_匯出.MouseDownEvent += PlC_RJ_Button_Box_Index_Table_匯出_MouseDownEvent;
            this.plC_RJ_Button_Box_Index_Table_匯入.MouseDownEvent += PlC_RJ_Button_Box_Index_Table_匯入_MouseDownEvent;
            this.plC_RJ_Button_Box_Index_Table_刪除.MouseDownEvent += PlC_RJ_Button_Box_Index_Table_刪除_MouseDownEvent;
            this.plC_RJ_Button_Box_Index_Table_更新.MouseDownEvent += PlC_RJ_Button_Box_Index_Table_更新_MouseDownEvent;
            this.plC_UI_Init.Add_Method(sub_Program_系統頁面);
        }

   

        private void sub_Program_系統頁面()
        {

        }
        #region Event
        private void SqL_DataGridView_Box_Index_Table_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_Box_Index_Table());
        }
        private void PlC_RJ_Button_Box_Index_Table_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (openFileDialog_LoadExcel.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dataTable = new DataTable();
                    CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                    DataTable datatable_buf = dataTable.ReorderTable(new enum_Box_Index_Table_匯入());
                    if (datatable_buf == null)
                    {
                        MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                        return;
                    }
                    List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();
                    List<object[]> list_SQL_Value = this.sqL_DataGridView_Box_Index_Table.SQL_GetAllRows(false);
                    List<object[]> list_Add = new List<object[]>();
                    List<object[]> list_Delete_ColumnName = new List<object[]>();
                    List<object[]> list_Delete_SerchValue = new List<object[]>();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();
                    List<object[]> list_SQL_Value_buf = new List<object[]>();

                    Dialog_Prcessbar dialog_Prcessbar = new Dialog_Prcessbar(list_LoadValue.Count);
                    dialog_Prcessbar.State = "開始匯入資料...";
                    for (int i = 0; i < list_LoadValue.Count; i++)
                    {
                        dialog_Prcessbar.Value = i;
                        object[] value_load = list_LoadValue[i];
                        value_load = value_load.CopyRow(new enum_Box_Index_Table_匯入(), new enum_Box_Index_Table());
                        list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_Box_Index_Table.IP, value_load[(int)enum_Box_Index_Table.IP].ObjectToString());
                        if (list_SQL_Value_buf.Count > 0)
                        {
                            object[] value_SQL = list_SQL_Value_buf[0];
                            bool flag_Equal = value_load.IsEqual(value_SQL);
                            if (!flag_Equal)
                            {
                                list_Replace_Value.Add(value_load);
                            }
                        }
                        else
                        {
                            value_load[(int)enum_Box_Index_Table.GUID] = Guid.NewGuid().ToString();
                            list_Add.Add(value_load);
                        }
                    }
                    dialog_Prcessbar.State = "上傳資料...";
                    this.sqL_DataGridView_Box_Index_Table.SQL_AddRows(list_Add, false);
                    this.sqL_DataGridView_Box_Index_Table.SQL_ReplaceExtra(list_Replace_Value, false);
                    this.sqL_DataGridView_Box_Index_Table.SQL_GetAllRows(true);
                    this.Cursor = Cursors.Default;
                    dialog_Prcessbar.Close();
                }
            
            }));
         
        }
        private void PlC_RJ_Button_Box_Index_Table_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                saveFileDialog_SaveExcel.OverwritePrompt = false;
                if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
                {
                    DataTable datatable = new DataTable();
                    datatable = sqL_DataGridView_Box_Index_Table.GetDataTable();
                    datatable = datatable.ReorderTable(new enum_Box_Index_Table_匯出());
                    CSVHelper.SaveFile(datatable, saveFileDialog_SaveExcel.FileName);
                    MyMessageBox.ShowDialog("匯出完成!");
                }
            }));

        }
        private void PlC_RJ_Button_Box_Index_Table_更新_MouseDownEvent(MouseEventArgs mevent)
        {
            this.sqL_DataGridView_Box_Index_Table.SQL_GetAllRows(true);
        }

        private void PlC_RJ_Button_Box_Index_Table_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_Box_Index_Table.Get_All_Select_RowsValues();
            this.sqL_DataGridView_Box_Index_Table.SQL_DeleteExtra(list_value, true);
        }
        #endregion
        private class ICP_Box_Index_Table : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {

                string Num_0 = x[(int)enum_Box_Index_Table.Number].ObjectToString();
                string Num_1 = y[(int)enum_Box_Index_Table.Number].ObjectToString();
                return Num_0.CompareTo(Num_1);

            }
        }
    }
}
