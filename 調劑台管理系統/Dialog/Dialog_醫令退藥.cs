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
using HIS_DB_Lib;
using SQLUI;
using H_Pannel_lib;
namespace 調劑台管理系統
{
    public partial class Dialog_醫令退藥 : MyDialog
    {
        private List<object[]> list_醫令資料_buf = new List<object[]>();
        private SQL_DataGridView _sqL_DataGridView_醫令資料;
  
        private object[] value;
        public object[] Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public List<OrderClass> orderClasses = new List<OrderClass>();
        public Dialog_醫令退藥(List<OrderClass> orderClasses)
        {
            if (form == null)
            {
                InitializeComponent();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    InitializeComponent();
                }));
            }
            this.list_醫令資料_buf = orderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
            this._sqL_DataGridView_醫令資料 = Main_Form._sqL_DataGridView_醫令資料;
            //InitializeComponent();
        }
        public Dialog_醫令退藥(List<object[]> list_醫令資料)
        {
            if (form == null)
            {
                InitializeComponent();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    InitializeComponent();
                }));
            }
            this.list_醫令資料_buf = list_醫令資料;
            this._sqL_DataGridView_醫令資料 = Main_Form._sqL_DataGridView_醫令資料;
            this.LoadFinishedEvent += Dialog_醫令退藥_LoadFinishedEvent;
        }

    

        private void Dialog_醫令退藥_Load(object sender, EventArgs e)
        {
            rJ_Button_選擇.MouseDownEvent += RJ_Button_選擇_MouseDownEvent;
            this.sqL_DataGridView_醫令資料.RowsHeight = 80;
            this.sqL_DataGridView_醫令資料.Init(_sqL_DataGridView_醫令資料);
            this.sqL_DataGridView_醫令資料.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(120, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(450, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.交易量);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.實際調劑量);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.備註);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(200, enum_醫囑資料.開方日期);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(200, enum_醫囑資料.過帳時間);
            //this.sqL_DataGridView_醫令資料.Set_ColumnWidth(200, enum_醫囑資料.備註);


            this.sqL_DataGridView_醫令資料.RowDoubleClickEvent += SqL_DataGridView_醫令資料_RowDoubleClickEvent;
            this.sqL_DataGridView_醫令資料.DataGridRefreshEvent += SqL_DataGridView_醫令資料_DataGridRefreshEvent;
            this.sqL_DataGridView_醫令資料.RefreshGrid(this.list_醫令資料_buf);

            rJ_Lable_處方資訊_藥局.Text = this.list_醫令資料_buf[0][(int)enum_醫囑資料.藥局代碼].ObjectToString();
            rJ_Lable_處方資訊_病歷號.Text = this.list_醫令資料_buf[0][(int)enum_醫囑資料.病歷號].ObjectToString();
            rJ_Lable_處方資訊_姓名.Text = this.list_醫令資料_buf[0][(int)enum_醫囑資料.病人姓名].ObjectToString();
            rJ_Lable_處方資訊_領藥號.Text = this.list_醫令資料_buf[0][(int)enum_醫囑資料.領藥號].ObjectToString();

            rJ_Button_確認送出.MouseDownEvent += RJ_Button_確認送出_MouseDownEvent;
        }
        private void Dialog_醫令退藥_LoadFinishedEvent(EventArgs e)
        {
            if (this.list_醫令資料_buf.Count == 1)
            {
                this.sqL_DataGridView_醫令資料.SetSelectRow(0);
                RJ_Button_選擇_MouseDownEvent(null);
            }
        }


        private void RJ_Button_選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("請選擇資料!");
                return;
            }
            object[] value = list_value[0];
            string order_guid = value[(int)enum_醫囑資料.GUID].ObjectToString();
            string Code = value[(int)enum_醫囑資料.藥品碼].ObjectToString();
            string 實調量 = value[(int)enum_醫囑資料.實際調劑量].ObjectToString();
            List<transactionsClass> transactionsClasses = transactionsClass.get_datas_by_order_guid(Main_Form.API_Server, order_guid, Main_Form.ServerName, Main_Form.ServerType);
            List<string> list_val = new List<string>();
            List<string> list_lot = new List<string>();

            List<StockClass> stockClasses = new List<StockClass>();
            if(transactionsClasses.Count == 0)
            {
                stockClasses = Main_Form.Funnction_交易記錄查詢_取得指定藥碼批號期效期(Code);
            }
            else
            {
                stockClasses = Main_Form.Funnction_交易記錄查詢_取得指定藥碼批號期效期(transactionsClasses);
            }
            StockClass stockClass = null;
            if (stockClasses.Count >= 2 || stockClasses.Count == 0)
            {
                Dialog_效期批號選擇 dialog_效期批號選擇 = new Dialog_效期批號選擇(stockClasses);
                if (dialog_效期批號選擇.ShowDialog() != DialogResult.Yes) return;
                stockClass = dialog_效期批號選擇.StockClass;
            }
            else
            {
                stockClass = stockClasses[0];
            }
     
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"{stockClass.Validity_period}({stockClass.Lot_number})");
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            double num = dialog_NumPannel.Value;
            if (Main_Form.PLC_Device_退藥不檢查是否掃碼領藥過.Bool == false)
            {
                if (實調量.StringToDouble() + num > 0)
                {
                    MyMessageBox.ShowDialog("退藥數量不可大於實際調劑量!");
                    return;
                }
            }
            value[(int)enum_醫囑資料.備註] = $"[效期]:{stockClass.Validity_period},[批號]:{stockClass.Lot_number},[數量]:{num}";
            if (num == 0) value[(int)enum_醫囑資料.備註] = "";
            this.sqL_DataGridView_醫令資料.ReplaceExtra(value, true);
            if(list_醫令資料_buf.Count == 1)
            {
                RJ_Button_確認送出_MouseDownEvent(null);
            }
        }
        private void SqL_DataGridView_醫令資料_DataGridRefreshEvent()
        {

        }

        private void RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.GetAllRows();
            List<OrderClass> orderClasses = list_value.SQLToClass<OrderClass, enum_醫囑資料>();
            
            for(int i = 0 ; i < orderClasses.Count; i++)
            {
                if (Main_Form.convert_note(orderClasses[i].備註) != null)
                {
                    this.orderClasses.Add(orderClasses[i]);
                }
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

        private void SqL_DataGridView_醫令資料_RowDoubleClickEvent(object[] RowValue)
        {
            RJ_Button_選擇_MouseDownEvent(null);
        }

        private void RJ_Button_退出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }

    }
}
