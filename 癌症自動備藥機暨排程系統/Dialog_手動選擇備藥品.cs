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

namespace 癌症備藥機
{
    public partial class Dialog_手動選擇備藥品 : MyDialog
    {
        public List<StockClass> Value = new List<StockClass>();
        private SQL_DataGridView _sQL_DataGridView_藥品資料;
        public Dialog_手動選擇備藥品(SQL_DataGridView sQL_DataGridView_藥品資料)
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();

                this._sQL_DataGridView_藥品資料 = sQL_DataGridView_藥品資料;

                this.Load += Dialog_手動選擇備藥_Load;
                this.LoadFinishedEvent += Dialog_手動選擇備藥_LoadFinishedEvent;


                this.rJ_Button_藥品選擇_顯示全部.MouseDownEvent += RJ_Button_藥品選擇_顯示全部_MouseDownEvent;
                this.rJ_Button_藥名搜尋.MouseDownEvent += RJ_Button_藥名搜尋_MouseDownEvent;
                this.rJ_Button_藥碼搜尋.MouseDownEvent += RJ_Button_藥碼搜尋_MouseDownEvent;

                this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
                this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
                this.rJ_Button_確認選擇.MouseDownEvent += RJ_Button_確認選擇_MouseDownEvent;
            }));

        }
        #region Event
        private void Dialog_手動選擇備藥_Load(object sender, EventArgs e)
        {
            this.sqL_DataGridView_藥品選擇.RowsHeight = 40;
            this.sqL_DataGridView_藥品選擇.Init(_sQL_DataGridView_藥品資料);
            this.sqL_DataGridView_藥品選擇.RowPostPaintingEvent += SqL_DataGridView_藥品選擇_RowPostPaintingEvent;

            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            this.sqL_DataGridView_已選藥品.RowsHeight = 40;
            this.sqL_DataGridView_已選藥品.Init(table);
            this.sqL_DataGridView_已選藥品.RowPostPaintingEvent += SqL_DataGridView_已選藥品_RowPostPaintingEvent;


            List<medClass> medClasses = Main_Form.Function_取得有儲位藥檔資料();
            List<object[]> list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
            this.sqL_DataGridView_藥品選擇.RefreshGrid(list_value);
        }
        private void Dialog_手動選擇備藥_LoadFinishedEvent(EventArgs e)
        {
            
        }
        private void SqL_DataGridView_藥品選擇_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            object[] value = this.sqL_DataGridView_藥品選擇.GetRowValues(e.RowIndex);
            if (value != null)
            {
               
                Color row_Backcolor = Color.White;
                Color row_Forecolor = Color.Black;

                if (this.sqL_DataGridView_藥品選擇.dataGridView.Rows[e.RowIndex].Selected)
                {
                    row_Backcolor = Color.Blue;
                    row_Forecolor = Color.White;
                }

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

                    string 藥碼 = $"({value[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString()})";
                    DrawingClass.Draw.文字左上繪製(藥碼, new PointF(10, y + 10), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                    size = 藥碼.MeasureText(new Font("標楷體", 14, FontStyle.Bold));

                    string 藥名 = $"{value[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString()}";
                    DrawingClass.Draw.文字左上繪製(藥名, new PointF(10 + size.Width, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);

                    string 單位 = $"[{value[(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString()}]";
                    DrawingClass.Draw.文字左上繪製(單位, new PointF(10 + 650, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);

                    string 庫存 = $"庫存:{value[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString()}";
                    size = 庫存.MeasureText(new Font("標楷體", 14, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(庫存, new PointF(e.RowBounds.Width - 150 - 10, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);


                }
            }
        }
        private void SqL_DataGridView_已選藥品_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            object[] value = this.sqL_DataGridView_已選藥品.GetRowValues(e.RowIndex);
            if (value != null)
            {
                StockClass stockClass = value[0].ObjectToString().JsonDeserializet<StockClass>();
                if (stockClass == null) return;
                Color row_Backcolor = Color.White;
                Color row_Forecolor = Color.Black;

                if (this.sqL_DataGridView_已選藥品.dataGridView.Rows[e.RowIndex].Selected)
                {
                    row_Backcolor = Color.Blue;
                    row_Forecolor = Color.White;
                }

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

                    string 藥碼 = $"({stockClass.Code})";
                    DrawingClass.Draw.文字左上繪製(藥碼, new PointF(10, y + 10), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                    size = 藥碼.MeasureText(new Font("標楷體", 14, FontStyle.Bold));

                    string 藥名 = $"{stockClass.Name}";
                    DrawingClass.Draw.文字左上繪製(藥名, new PointF(10 + size.Width, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);



                    string 數量 = $"數量:{stockClass.Qty}";
                    size = 數量.MeasureText(new Font("標楷體", 14, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(數量, new PointF(e.RowBounds.Width - 150 - 10, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);


                }
            }
        }
        private void RJ_Button_藥碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<medClass> medClasses = Main_Form.Function_取得有儲位藥檔資料();
            List<object[]> list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
            string 藥碼 = rJ_TextBox_藥碼搜尋.Texts;
            if (藥碼.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品碼, 藥碼);
            }

            this.sqL_DataGridView_藥品選擇.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_藥名搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<medClass> medClasses = Main_Form.Function_取得有儲位藥檔資料();
            List<object[]> list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
            string 藥名 = rJ_TextBox_藥名搜尋.Texts;
            if (藥名.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品名稱, 藥名);
            }

            this.sqL_DataGridView_藥品選擇.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_藥品選擇_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<medClass> medClasses = Main_Form.Function_取得有儲位藥檔資料();
            List<object[]> list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
            
            this.sqL_DataGridView_藥品選擇.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_確認選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_藥品選擇.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            int 庫存 = list_value[0][(int)enum_藥品資料_藥檔資料.庫存].StringToInt32();
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"請輸入取藥數量,庫存[{庫存}]");
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;

            if (dialog_NumPannel.Value > list_value[0][(int)enum_藥品資料_藥檔資料.庫存].StringToInt32())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("輸入數量不得大於庫存", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<StockClass> stockClasses_buf = new List<StockClass>();
            stockClasses_buf = (from temp in Value
                                where temp.Code == list_value[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString()
                                select temp).ToList();
            StockClass stockClass = new StockClass();
            if (stockClasses_buf.Count > 0)
            {
                stockClass = stockClasses_buf[0];
            }
            stockClass.Code = list_value[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            stockClass.Name = list_value[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
            stockClass.Qty = dialog_NumPannel.Value.ToString();
            if (stockClasses_buf.Count == 0) Value.Add(stockClass);

            List<object[]> list_已選藥品 = new List<object[]>();
            for (int i = 0; i < Value.Count; i++)
            {
                object[] value = new object[] {Value[i].JsonSerializationt() };
                list_已選藥品.Add(value);
            }
            this.sqL_DataGridView_已選藥品.RefreshGrid(list_已選藥品);

        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                for (int i = 0; i < Value.Count; i++)
                {
                    Value[i].Qty = (Value[i].Qty.StringToInt32() * -1).ToString();
                }
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
        #endregion
    }
}
