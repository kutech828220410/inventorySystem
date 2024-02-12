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
    public partial class Dialog_備藥清單 : Form
    {
        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }

        private string GUID = "";
        private udnoectc udnoectc = null;
        public Dialog_備藥清單(string guid)
        {
            InitializeComponent();

            this.GUID = guid;
            string url = $"{Main_Form.API_Server}/api/ChemotherapyRxScheduling/get_udnoectc_by_GUID";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = GUID;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            List<udnoectc> udnoectcs = returnData.Data.ObjToClass<List<udnoectc>>();


            Dialog_備藥清單.form = this.ParentForm;
            this.Load += Dialog_備藥清單_Load;
           

            if(udnoectcs.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("異常:查無資料", 2000);
                this.Invoke(new Action(delegate
                {
                    this.Close();
                    this.DialogResult = DialogResult.No;
                }));
            }
            udnoectc = udnoectcs[0];

          
        }

      

        private void Dialog_備藥清單_Load(object sender, EventArgs e)
        {
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
            for (int i = 0; i < udnoectc.藥囑資料.Count; i++)
            {
                object[] value = new object[] { udnoectc.藥囑資料[i].JsonSerializationt() };
                this.sqL_DataGridView_服藥順序.AddRow(value, false);
            }
            this.sqL_DataGridView_服藥順序.RefreshGrid();
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.plC_RJ_Button_變異紀錄.MouseDownEvent += PlC_RJ_Button_變異紀錄_MouseDownEvent;
        }

       
        #region Event
        private void PlC_RJ_Button_變異紀錄_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Dialog_變異紀錄 dialog_變異紀錄 = new Dialog_變異紀錄(this.GUID);
                dialog_變異紀錄.ShowDialog();
            }));
        }
        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            string url = $"{Main_Form.API_Server}/api/ChemotherapyRxScheduling/update_udnoectc_orders_comp";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = "TEST";

            List<object[]> list_value = this.sqL_DataGridView_服藥順序.Get_All_Checked_RowsValuesEx();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取備藥藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<udnoectc_orders> list_udnoectc_orders = new List<udnoectc_orders>();
            List<udnoectc_orders> list_udnoectc_orders_replace = new List<udnoectc_orders>();
            for (int i = 0; i < list_value.Count; i++)
            {
                list_udnoectc_orders = list_value[i][0].ObjectToString().JsonDeserializet<List<udnoectc_orders>>();
                list_udnoectc_orders_replace.LockAdd(list_udnoectc_orders);
            }
            returnData.Data = list_udnoectc_orders_replace;

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
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
            Color color = Color.White;
           
            for (int i = 0; i < udnoectc_Orders.Count; i++)
            {
                if (udnoectc_Orders[i].備藥藥師.StringIsEmpty() == false) flag_未備藥 = false;
            }
            if(flag_未備藥)
            {
                if (sqL_DataGridView_服藥順序.Checked[e.RowIndex])
                {
                    color = Color.GreenYellow;
                }
            }
            else
            {
                color = Color.DarkSlateGray; 
            }
            using (Brush brush = new SolidBrush(color))
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
                    DrawingClass.Draw.文字左上繪製(藥名, new Rectangle(80, y + 20 + i * 100, 480, 40), new Font("標楷體", 12, FontStyle.Bold), Color.Black, e.Graphics);
                    string 備註 = $"註:{udnoectc_Orders[i].備註}";
                    DrawingClass.Draw.文字左上繪製(備註, new Rectangle(80, y + 60 + i * 100, 480, 40), new Font("標楷體", 12, FontStyle.Italic), Color.DimGray, e.Graphics);
                    string 劑量及用法 = $"{udnoectc_Orders[i].劑量} {udnoectc_Orders[i].單位} {udnoectc_Orders[i].途徑} {udnoectc_Orders[i].頻次}";
                    size = 劑量及用法.MeasureText(new Font("標楷體", 14, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(劑量及用法, new Rectangle(550, y + i * 100 + (100 - size.Height) / 2, 200, 40), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                    string 數量 = $"{udnoectc_Orders[i].數量}";
                    size = 數量.MeasureText(new Font("標楷體", 18, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(數量, new Rectangle(750, y + i * 100 + (100 - size.Height) / 2, 70, 40), new Font("標楷體", 18, FontStyle.Bold), Color.Black, e.Graphics);
                    DateTime dateTime_st = udnoectc_Orders[i].處方開始時間.StringToDateTime();
                    DateTime dateTime_end = udnoectc_Orders[i].處方結束時間.StringToDateTime();
                    string 期限 = $"{dateTime_st.Month.ToString("00")}/{dateTime_st.Day.ToString("00")}-{dateTime_end.Month.ToString("00")}/{dateTime_end.Day.ToString("00")}";
                    size = 期限.MeasureText(new Font("標楷體", 12, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(期限, new Rectangle(810, y + i * 100 + (100 - size.Height) / 2, 200, 40), new Font("標楷體", 12, FontStyle.Bold), Color.Black, e.Graphics);

                }

                size = 服藥順序.MeasureText(new Font("標楷體", 26 , FontStyle.Bold));

                pointF = new PointF(10, e.RowBounds.Y + (e.RowBounds.Height - size.Height) / 2);

                DrawingClass.Draw.文字左上繪製(服藥順序, pointF, new Font("標楷體", 26, FontStyle.Bold), Color.Black, e.Graphics);
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
            DrawingClass.Draw.文字左上繪製(RegimenName, new PointF(10, y + 10), new Font("標楷體", 16 , FontStyle.Bold), Color.Black, e.Graphics);
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
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Close();
                this.DialogResult = DialogResult.No;
            }));
        }
        #endregion
    }
}
