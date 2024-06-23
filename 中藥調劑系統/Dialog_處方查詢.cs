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
    public partial class Dialog_處方查詢 : MyDialog
    {
        public enum enum_處方內容
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("應調,VARCHAR,15,NONE")]
            應調,
            [Description("實調,VARCHAR,15,NONE")]
            實調,
            [Description("天,VARCHAR,15,NONE")]
            天,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
            [Description("服用方法,VARCHAR,15,NONE")]
            服用方法,
        }

        private string pri_key = "";

        public Dialog_處方查詢(string _pri_key)
        {
            InitializeComponent();
            this.pri_key = _pri_key;

            this.Load += Dialog_處方查詢_Load;
            this.LoadFinishedEvent += Dialog_處方查詢_LoadFinishedEvent;
            this.rJ_Button_返回.MouseDownEvent += RJ_Button_返回_MouseDownEvent;
        }

        #region Function
        private object[] Funtion_orderTClassesToObject(OrderTClass orderTClass)
        {
            object[] value = new object[new enum_處方內容().GetLength()];
            if (orderTClass.交易量.StringIsDouble())
            {
                orderTClass.交易量 = (orderTClass.交易量.StringToDouble() * -1).ToString("0.00");
            }
            if (orderTClass.實際調劑量.StringIsEmpty()) orderTClass.實際調劑量 = "-";
            value[(int)enum_處方內容.GUID] = orderTClass.GUID;
            value[(int)enum_處方內容.藥名] = orderTClass.藥品名稱;
            value[(int)enum_處方內容.應調] = orderTClass.交易量;
            value[(int)enum_處方內容.實調] = orderTClass.實際調劑量;
            value[(int)enum_處方內容.天] = orderTClass.天數;
            value[(int)enum_處方內容.單位] = orderTClass.劑量單位;
            value[(int)enum_處方內容.服用方法] = orderTClass.頻次;
            return value;
        }
        #endregion

        #region Event
        private void Dialog_處方查詢_Load(object sender, EventArgs e)
        {
            this.sqL_DataGridView_處方查詢.Init();

            Table table_處方內容 = new Table(new enum_處方內容());
            this.sqL_DataGridView_處方查詢.Init(table_處方內容);
            this.sqL_DataGridView_處方查詢.Set_ColumnVisible(false, new enum_處方內容().GetEnumNames());
            this.sqL_DataGridView_處方查詢.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, "藥名");
            this.sqL_DataGridView_處方查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, "應調");
            this.sqL_DataGridView_處方查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, "實調");
            this.sqL_DataGridView_處方查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, "天");
            this.sqL_DataGridView_處方查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, "單位");
            this.sqL_DataGridView_處方查詢.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_處方內容.藥名);


            this.sqL_DataGridView_處方查詢.DataGridRefreshEvent += SqL_DataGridView_處方內容_DataGridRefreshEvent;
        }
        private void Dialog_處方查詢_LoadFinishedEvent(EventArgs e)
        {
            //LoadingForm.ShowLoadingForm();

            List<OrderTClass> orderTClasses = OrderTClass.get_by_pri_key(Main_Form.API_Server, pri_key);
            List<object[]> list_value = new List<object[]>();
            if (orderTClasses.Count > 0)
            {
                this.rJ_Lable_處方資訊_姓名_性別_病歷號.Text = $"{orderTClasses[0].病人姓名}({orderTClasses[0].性別}) {orderTClasses[0].病歷號}";
                this.rJ_Lable_處方資訊_年齡.Text = $"{orderTClasses[0].年齡}歲";
                this.rJ_Lable_處方資訊_處方日期.Text = $"{orderTClasses[0].開方日期.StringToDateTime().ToDateString()}";
                for(int i = 0; i < orderTClasses.Count; i++)
                {
                    object[] value = Funtion_orderTClassesToObject(orderTClasses[i]);
                    list_value.Add(value);
                }
                this.sqL_DataGridView_處方查詢.RefreshGrid(list_value);
            }

            //LoadingForm.CloseLoadingForm();
        }

        private void SqL_DataGridView_處方內容_DataGridRefreshEvent()
        {
            string 實調 = "";
            for (int i = 0; i < this.sqL_DataGridView_處方查詢.dataGridView.Rows.Count; i++)
            {
                實調 = this.sqL_DataGridView_處方查詢.dataGridView.Rows[i].Cells[enum_處方內容.實調.GetEnumName()].Value.ToString();
                if (實調.StringIsDouble())
                {
                    this.sqL_DataGridView_處方查詢.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                    this.sqL_DataGridView_處方查詢.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.Close();
            }));
        }
        #endregion
    }
}
