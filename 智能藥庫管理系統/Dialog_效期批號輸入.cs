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

namespace 智能藥庫系統
{


    public partial class Dialog_效期批號輸入 : MyDialog
    {

        public string 效期 = "";
        public string 批號 = "";
        private string _藥碼;


        public Dialog_效期批號輸入(string 藥碼)
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
            }));
   

            this._藥碼 = 藥碼;

            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.None);

            this.sqL_DataGridView_效期批號.RowsHeight = 50;
            this.sqL_DataGridView_效期批號.Init(table);
            this.sqL_DataGridView_效期批號.Set_ColumnWidth(sqL_DataGridView_效期批號.Width - 20, DataGridViewContentAlignment.MiddleLeft, "GUID");

            this.sqL_DataGridView_效期批號.RowPostPaintingEvent += SqL_DataGridView_效期批號_RowPostPaintingEvent;
            this.sqL_DataGridView_效期批號.RowEnterEvent += SqL_DataGridView_效期批號_RowEnterEvent;
            this.Load += Dialog_效期批號輸入_Load;

            this.rJ_Button_確認選擇.MouseDownEvent += RJ_Button_確認選擇_MouseDownEvent;
        }



        private void Dialog_效期批號輸入_Load(object sender, EventArgs e)
        {
            List<StockClass> stockClasses = transactionsClass.get_stock_by_code(Main_Form.API_Server, _藥碼, "ds01", "藥庫");
            if (stockClasses == null) stockClasses = new List<StockClass>();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < stockClasses.Count; i++)
            {
                object[] value = new object[] { stockClasses[i].JsonSerializationt() };
                list_value.Add(value);
            }
            this.sqL_DataGridView_效期批號.RefreshGrid(list_value);
        }
        private void SqL_DataGridView_效期批號_RowEnterEvent(object[] RowValue)
        {
            StockClass stockClass = RowValue[0].ObjectToString().JsonDeserializet<StockClass>();
            rJ_DatePicker_效期.Value = stockClass.Validity_period.StringToDateTime();
            rJ_TextBox_批號.Texts = stockClass.Lot_number;
        }
        private void SqL_DataGridView_效期批號_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            Color row_Backcolor = Color.LightGray;
            Color row_Forecolor = Color.Black;
            using (Brush brush = new SolidBrush(row_Backcolor))
            {
                int x = e.RowBounds.Left;
                int y = e.RowBounds.Top;
                int width = e.RowBounds.Width;
                int height = e.RowBounds.Height;
                e.Graphics.FillRectangle(brush, e.RowBounds);
                DrawingClass.Draw.DrawRoundShadow(e.Graphics, new RectangleF(x - 1, y - 1, width, height), Color.DarkGray, 5, 5);

                Size size = new Size();
                PointF pointF = new PointF();
                object[] value = this.sqL_DataGridView_效期批號.GetRowsList()[e.RowIndex];
                StockClass stockClass = value[0].ObjectToString().JsonDeserializet<StockClass>();
                string 序號 = $"{e.RowIndex + 1}.";
                string 效期 = $"{stockClass.Validity_period}";
                string 批號 = $"{stockClass.Lot_number}";
                string str = $"{序號} 效期 : {效期} 批號 {((批號.StringIsEmpty()) ? "無" : $"{批號}")}";
                DrawingClass.Draw.文字左上繪製(str, new PointF(10, y + 10), new Font("標楷體", 16), Color.Black, e.Graphics);

            }
        }
        private void RJ_Button_確認選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.效期 = rJ_DatePicker_效期.Value.ToDateString();
                this.批號 = rJ_TextBox_批號.Texts;
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }));
        }
    }
}
