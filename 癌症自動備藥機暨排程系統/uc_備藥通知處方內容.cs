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

namespace 癌症自動備藥機暨排程系統
{
    public partial class uc_備藥通知處方內容 : UserControl
    {
        public udnoectc udnoectc = null;
        public uc_備藥通知處方內容()
        {
            InitializeComponent();
            this.Load += Uc_備藥通知內容_Load;

        }
        public List<object[]> GetSelectedRows()
        {
            List<object[]> list_value = this.sqL_DataGridView_服藥順序.Get_All_Checked_RowsValuesEx();
            return list_value;
        }
        public void Init(udnoectc udnoectc)
        {
            this.udnoectc = udnoectc;
            rJ_Pannel_處方內容.Paint += RJ_Pannel_處方內容_Paint;
            rJ_Pannel_備藥內容.Paint += RJ_Pannel_備藥內容_Paint;

            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 200, Table.IndexType.None);

            this.sqL_DataGridView_服藥順序.RowsHeight = 250;
            this.sqL_DataGridView_服藥順序.Init(table);
            this.sqL_DataGridView_服藥順序.Set_ColumnWidth(sqL_DataGridView_服藥順序.Width - 20, DataGridViewContentAlignment.MiddleLeft, "GUID");
            this.sqL_DataGridView_服藥順序.AutoScroll = false;
            this.sqL_DataGridView_服藥順序.RowEnterEvent += SqL_DataGridView_服藥順序_RowEnterEvent;
            this.sqL_DataGridView_服藥順序.RowPostPaintingEvent += SqL_DataGridView_服藥順序_RowPostPaintingEvent;
            this.sqL_DataGridView_服藥順序.RowClickEvent += SqL_DataGridView_服藥順序_RowClickEvent;
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < udnoectc.藥囑資料.Count; i++)
            {
                object[] value = new object[] { udnoectc.藥囑資料[i].JsonSerializationt() };
                this.sqL_DataGridView_服藥順序.AddRow(value, false);
                list_value.Add(value);
            }
            this.sqL_DataGridView_服藥順序.RefreshGrid(list_value);
        }
        private void Uc_備藥通知內容_Load(object sender, EventArgs e)
        {

        }
        private void SqL_DataGridView_服藥順序_RowClickEvent(object[] RowValue)
        {
            int index = sqL_DataGridView_服藥順序.GetSelectRow();
            if (index >= 0)
            {
                sqL_DataGridView_服藥順序.Checked[index] = !sqL_DataGridView_服藥順序.Checked[index];
            }
        }
        private void SqL_DataGridView_服藥順序_RowEnterEvent(object[] RowValue)
        {

        }
        private void SqL_DataGridView_服藥順序_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            object[] value = this.sqL_DataGridView_服藥順序.GetRowsList()[e.RowIndex];
            List<udnoectc_orders> udnoectc_Orders = value[0].ToString().JsonDeserializet<List<udnoectc_orders>>();
            bool flag_未備藥 = true;
            Color row_Backcolor = Color.White;
            Color row_Forecolor = Color.Black;
            string 備藥藥師 = "";
            for (int i = 0; i < udnoectc_Orders.Count; i++)
            {
                if (udnoectc_Orders[i].備藥藥師.StringIsEmpty() == false)
                {
                    備藥藥師 = $"({udnoectc_Orders[i].備藥藥師})";
                    flag_未備藥 = false;
                }
            }
            if (flag_未備藥)
            {
                if (sqL_DataGridView_服藥順序.Checked[e.RowIndex])
                {
                    row_Backcolor = Color.GreenYellow;
                }
            }
            else
            {
                row_Backcolor = Color.Silver;
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
                string 服藥順序 = "";
                for (int i = 0; i < udnoectc_Orders.Count; i++)
                {
                    服藥順序 = $"{udnoectc_Orders[i].服藥順序}.";
                    string 藥名 = $"({udnoectc_Orders[i].藥碼}){udnoectc_Orders[i].警示}{ udnoectc_Orders[i].藥名}";
                    DrawingClass.Draw.文字左上繪製(藥名, new Rectangle(80, y + 20 + i * 100, 480, 40), new Font("標楷體", 12, FontStyle.Bold), row_Forecolor, e.Graphics);
                    string 備註 = $"註:{udnoectc_Orders[i].備註}";
                    DrawingClass.Draw.文字左上繪製(備註, new Rectangle(80, y + 60 + i * 100, 480, 40), new Font("標楷體", 12, FontStyle.Italic), Color.DimGray, e.Graphics);
                    string 劑量及用法 = $"{udnoectc_Orders[i].劑量} {udnoectc_Orders[i].單位} {udnoectc_Orders[i].途徑} {udnoectc_Orders[i].頻次}";
                    size = 劑量及用法.MeasureText(new Font("標楷體", 14, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(劑量及用法, new Rectangle(550, y + i * 100 + (100 - size.Height) / 2, 200, 40), new Font("標楷體", 14, FontStyle.Bold), row_Forecolor, e.Graphics);
                    string 數量 = $"{udnoectc_Orders[i].數量}";
                    size = 數量.MeasureText(new Font("標楷體", 18, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(數量, new Rectangle(750, y + i * 100 + (100 - size.Height) / 2, 70, 40), new Font("標楷體", 18, FontStyle.Bold), row_Forecolor, e.Graphics);
                    DateTime dateTime_st = udnoectc_Orders[i].處方開始時間.StringToDateTime();
                    DateTime dateTime_end = udnoectc_Orders[i].處方結束時間.StringToDateTime();
                    string 期限 = $"{dateTime_st.Month.ToString("00")}/{dateTime_st.Day.ToString("00")}-{dateTime_end.Month.ToString("00")}/{dateTime_end.Day.ToString("00")}";
                    size = 期限.MeasureText(new Font("標楷體", 12, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(期限, new Rectangle(810, y + i * 100 + (100 - size.Height) / 2, 200, 40), new Font("標楷體", 12, FontStyle.Bold), row_Forecolor, e.Graphics);

                }

                size = 服藥順序.MeasureText(new Font("標楷體", 26, FontStyle.Bold));
                pointF = new PointF(10, e.RowBounds.Y + (e.RowBounds.Height - size.Height) / 2);
                DrawingClass.Draw.文字左上繪製(服藥順序, pointF, new Font("標楷體", 26, FontStyle.Bold), row_Forecolor, e.Graphics);

                if (備藥藥師.StringIsEmpty() == false)
                {
                    size = 備藥藥師.MeasureText(new Font("標楷體", 16, FontStyle.Bold));
                    pointF = new PointF(e.RowBounds.X + e.RowBounds.Width - size.Width - 10, e.RowBounds.Y + (e.RowBounds.Height - size.Height - 10));
                    DrawingClass.Draw.文字左上繪製(備藥藥師, pointF, new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);

                }


                this.sqL_DataGridView_服藥順序.dataGridView.Rows[e.RowIndex].Height = udnoectc_Orders.Count * 105;
            }
        }
        private void RJ_Pannel_備藥內容_Paint(object sender, PaintEventArgs e)
        {
            this.rJ_Pannel_備藥內容.Height = ((udnoectc.labdatas.Count / 3) * 40) + 90 + 50;
            int y = 0;
            string RegimenName = $"RegimenName : {udnoectc.RegimenName}";
            string 天數順序 = $"天數順序 : {udnoectc.天數順序}";
            string 化療前檢核項目_title = $"化療前檢核項目 :";
            DrawingClass.Draw.文字左上繪製(RegimenName, new PointF(10, y + 10), new Font("標楷體", 16, FontStyle.Bold), Color.Black, e.Graphics);
            Size size = new Size();
            size = 天數順序.MeasureText(new Font("標楷體", 14));
            DrawingClass.Draw.文字左上繪製(天數順序, new PointF(rJ_Pannel_備藥內容.Width - size.Width - 10, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(化療前檢核項目_title, new PointF(10, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);

            for (int i = 0; i < udnoectc.labdatas.Count; i++)
            {
                float tempX = (rJ_Pannel_備藥內容.Width / 3) * (i % 3);
                float tempY = (40) * (i / 3);

                DrawingClass.Draw.文字左上繪製(udnoectc.labdatas[i], new PointF(10 + tempX, y + 90 + tempY), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            }

        }
        private void RJ_Pannel_處方內容_Paint(object sender, PaintEventArgs e)
        {
            int y = 0;
            string 加入時間 = $"{udnoectc.加入時間}";
            string 診別 = "【住院】";
            string 病床號 = $"{udnoectc.病房}-{udnoectc.床號}";
            if (udnoectc.病房.ToUpper().Contains("OPD"))
            {
                診別 = "【門診】";
                病床號 = "-------";
            }

            string 病人姓名 = $"{udnoectc.病人姓名}";
            string 病歷號 = $"{udnoectc.病歷號}";
            string 生日 = $"生日:{udnoectc.生日}";
            string 性別 = $"{udnoectc.性別}";
            string 身高 = $"身高:{udnoectc.身高}cm";
            string 體重 = $"體重:{udnoectc.體重}kg";


            string 診斷 = $"診斷:{udnoectc.診斷}";
            string 科別 = $"{udnoectc.科別}";
            string 開立醫師 = $"開立醫師:{udnoectc.開立醫師}";

            DrawingClass.Draw.文字左上繪製(診別, new PointF(10, y + 10), new Font("標楷體", 14), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(加入時間, new PointF(130, y + 10), new Font("標楷體", 14, FontStyle.Italic), Color.Black, e.Graphics);

            DrawingClass.Draw.文字左上繪製(病人姓名, 300, new PointF(20 * 0.8F, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(病歷號, new PointF(180 * 0.8F, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(病床號, new PointF(280 * 0.8F, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(生日, new PointF(420 * 0.8F, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(性別, new PointF(640 * 0.8F, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(身高, new PointF(690 * 0.8F, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(體重, new PointF(840 * 0.8F, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);

            DrawingClass.Draw.文字左上繪製(診斷, new PointF(20 * 0.8F, y + 100), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(科別, new PointF(600 * 0.8F, y + 100), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
            DrawingClass.Draw.文字左上繪製(開立醫師, new PointF(840 * 0.8F, y + 100), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);

        }
    }
}
