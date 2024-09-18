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
    public partial class Dialog_盤點單合併_單價設定 : MyDialog
    {
        private inv_combinelistClass inv_CombinelistClass = null;
        private string _SN;
        public Dialog_盤點單合併_單價設定(string SN)
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
                Table table = new Table(new enum_inv_combinelist_price());
                this.sqL_DataGridView_單價設定表.RowsHeight = 50;
                this.sqL_DataGridView_單價設定表.Init(table);
                this.sqL_DataGridView_單價設定表.Set_ColumnVisible(false, new enum_inv_combinelist_price().GetEnumNames());
                this.sqL_DataGridView_單價設定表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_inv_combinelist_price.藥碼);
                this.sqL_DataGridView_單價設定表.Set_ColumnWidth(550, DataGridViewContentAlignment.MiddleLeft, enum_inv_combinelist_price.藥名);
                this.sqL_DataGridView_單價設定表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_inv_combinelist_price.單價);
            }));

            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_匯出.MouseDownEvent += PlC_RJ_Button_匯出_MouseDownEvent;
            this.plC_RJ_Button_匯入.MouseDownEvent += PlC_RJ_Button_匯入_MouseDownEvent;
            this.LoadFinishedEvent += Dialog_盤點單合併_單價設定_LoadFinishedEvent;
            _SN = SN;
        }

        private void RefreshUI()
        {
            LoadingForm.ShowLoadingForm();

            inv_CombinelistClass = inv_combinelistClass.get_full_inv_by_SN(Main_Form.API_Server, _SN);
            List<object[]> list_inv_combinelist_price_Class = inv_CombinelistClass.MedPrices.ClassToSQL<inv_combinelist_price_Class, enum_inv_combinelist_price>();
            this.sqL_DataGridView_單價設定表.RefreshGrid(list_inv_combinelist_price_Class);
            LoadingForm.CloseLoadingForm();
        }

        private void Dialog_盤點單合併_單價設定_LoadFinishedEvent(EventArgs e)
        {
            RefreshUI();
        }

        private void PlC_RJ_Button_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;

                DataTable dataTable = sqL_DataGridView_單價設定表.GetAllRows().ToDataTable(new enum_inv_combinelist_price());
                dataTable.ColumnRename("數量", "單價");
                dataTable.RemoveColumn(enum_inv_combinelist_price.GUID);
                dataTable.RemoveColumn(enum_inv_combinelist_price.合併單號);
                dataTable.RemoveColumn(enum_inv_combinelist_price.加入時間);
                dataTable.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName, new string[] { "單價" });
                MyMessageBox.ShowDialog("匯出完成");
            }));

        }
        private void PlC_RJ_Button_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            string filename = "";
            this.Invoke(new Action(delegate
            {
                if (this.openFileDialog_LoadExcel.ShowDialog() != DialogResult.OK) return;
                filename = this.openFileDialog_LoadExcel.FileName;          
            }));
            DataTable dataTable = filename.NPOI_LoadFile();
            dataTable = dataTable.ReorderTable(new enum_inv_combinelist_price().GetEnumNames());
            List<object[]> list_value = dataTable.DataTableToRowList();
            List<inv_combinelist_price_Class> inv_combinelist_price_Classes = list_value.SQLToClass<inv_combinelist_price_Class, enum_inv_combinelist_price>();
            inv_combinelistClass.add_medPrices_by_SN(Main_Form.API_Server, inv_CombinelistClass.合併單號, inv_combinelist_price_Classes);
            RefreshUI();
            MyMessageBox.ShowDialog("匯入完成");
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Close();
        }
    }
}
