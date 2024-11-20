using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using MyUI;
using SQLUI;
using HIS_DB_Lib;
using MyOffice;

namespace 智能藥庫系統
{
    public partial class Dialog_盤點單合併_別名設定 : MyDialog
    {
        private inv_combinelistClass inv_CombinelistClass = null;
        private string _SN;
        public Dialog_盤點單合併_別名設定(string SN)
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
                Table table = new Table(new enum_inv_combinelist_note());
                this.sqL_DataGridView_別名設定表.RowsHeight = 50;
                this.sqL_DataGridView_別名設定表.Init(table);
                this.sqL_DataGridView_別名設定表.Set_ColumnVisible(false, new enum_inv_combinelist_note().GetEnumNames());
                this.sqL_DataGridView_別名設定表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_inv_combinelist_note.藥碼);
                this.sqL_DataGridView_別名設定表.Set_ColumnWidth(550, DataGridViewContentAlignment.MiddleLeft, enum_inv_combinelist_note.藥名);
                this.sqL_DataGridView_別名設定表.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleCenter, enum_inv_combinelist_note.備註);
            }));

            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_匯出.MouseDownEvent += PlC_RJ_Button_匯出_MouseDownEvent;
            this.plC_RJ_Button_匯入.MouseDownEvent += PlC_RJ_Button_匯入_MouseDownEvent;
            this.LoadFinishedEvent += Dialog_盤點單合併_別名設定_LoadFinishedEvent;
            _SN = SN;
        }
        private void RefreshUI()
        {
            LoadingForm.ShowLoadingForm();

            inv_CombinelistClass = inv_combinelistClass.get_full_inv_by_SN(Main_Form.API_Server, _SN);
            List<object[]> list_inv_combinelist_note_Class = inv_CombinelistClass.MedNotes.ClassToSQL<inv_combinelist_note_Class, enum_inv_combinelist_note>();
            this.sqL_DataGridView_別名設定表.RefreshGrid(list_inv_combinelist_note_Class);
            LoadingForm.CloseLoadingForm();
        }
        private void Dialog_盤點單合併_別名設定_LoadFinishedEvent(EventArgs e)
        {
            RefreshUI();
        }
        private void PlC_RJ_Button_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;

                DataTable dataTable = sqL_DataGridView_別名設定表.GetAllRows().ToDataTable(new enum_inv_combinelist_note());
                dataTable.RemoveColumn(enum_inv_combinelist_note.GUID);
                dataTable.RemoveColumn(enum_inv_combinelist_note.合併單號);
                dataTable.RemoveColumn(enum_inv_combinelist_note.加入時間);
                dataTable.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);
                MyMessageBox.ShowDialog("匯出完成");
            }));

        }
        private void PlC_RJ_Button_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.openFileDialog_LoadExcel.ShowDialog() != DialogResult.OK) return;
                string filename = this.openFileDialog_LoadExcel.FileName;
                DataTable dataTable = filename.NPOI_LoadFile();
                dataTable = dataTable.ReorderTable(new enum_inv_combinelist_note().GetEnumNames());
                List<object[]> list_value = dataTable.DataTableToRowList();
                List<inv_combinelist_note_Class> inv_Combinelist_note_Classes = list_value.SQLToClass<inv_combinelist_note_Class, enum_inv_combinelist_note>();

                List<medClass> medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                List<medClass> medClasses_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs = medClasses.CoverToDictionaryByCode();
                for (int i = 0; i < inv_Combinelist_note_Classes.Count; i++)
                {
                    medClass medClass = medClasses.SerchByBarcode(inv_Combinelist_note_Classes[i].藥碼);
                    if (medClass != null)
                    {
                        inv_Combinelist_note_Classes[i].藥碼 = medClass.料號;
                    }
                }

                inv_combinelistClass.add_medNote_by_SN(Main_Form.API_Server, inv_CombinelistClass.合併單號, inv_Combinelist_note_Classes);
                RefreshUI();
                MyMessageBox.ShowDialog("匯入完成");
            }));
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Close();
        }

    }
}
