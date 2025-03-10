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
    public partial class Dialog_醫令選擇 : MyDialog
    {

        private List<object[]> _list_orders = new List<object[]>();
        public List<object[]> Value = new List<object[]>();
        public List<OrderClass> OrderClasses = new List<OrderClass>();
        public Dialog_醫令選擇(List<object[]> list_orders)
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
            _list_orders = list_orders;
            this.LoadFinishedEvent += Dialog_醫令選擇_LoadFinishedEvent;
           
        }

        public Dialog_醫令選擇(List<OrderClass> list_orders)
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
            _list_orders = list_orders.ClassToSQL<OrderClass,enum_醫囑資料>();
            this.LoadFinishedEvent += Dialog_醫令選擇_LoadFinishedEvent;

        }
        private void Dialog_醫令選擇_LoadFinishedEvent(EventArgs e)
        {
            Table table = OrderClass.init(Main_Form.API_Server);
            this.sqL_DataGridView_醫令資料.RowsHeight = 80;
            this.sqL_DataGridView_醫令資料.InitEx(table);
  

            this.sqL_DataGridView_醫令資料.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(150, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(500, DataGridViewContentAlignment.BottomLeft, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, enum_醫囑資料.交易量);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, enum_醫囑資料.實際調劑量);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(150, enum_醫囑資料.狀態);

            this.sqL_DataGridView_醫令資料.RefreshGrid(_list_orders);
            this.sqL_DataGridView_醫令資料.RowClickEvent += SqL_DataGridView_醫令資料_RowClickEvent;

            rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
        }

     
        private void SqL_DataGridView_醫令資料_RowClickEvent(object[] RowValue)
        {
            List<object[]> list_value = new List<object[]>();
            if (RowValue[(int)enum_醫囑資料.狀態].ObjectToString() == enum_醫囑資料_狀態.已過帳.GetEnumName())
            {
                list_value.Add(RowValue);
            }
            this.sqL_DataGridView_醫令資料.SetDataKeys(list_value, false);
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.DialogResult = DialogResult.Yes;
            this.Value = this.sqL_DataGridView_醫令資料.Get_All_Checked_RowsValuesEx();
        }


    }
}
