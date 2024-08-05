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

namespace 癌症備藥機
{
    public partial class Dialog_變異紀錄 : MyDialog
    {
        public static Form form;
 
        private string GUID = "";
        private udnoectc udnoectc = null;
        public Dialog_變異紀錄(string guid)
        {
            InitializeComponent();
            this.LoadFinishedEvent += Dialog_變異紀錄_LoadFinishedEvent;
            this.GUID = guid;
        }

        private void Dialog_變異紀錄_LoadFinishedEvent(EventArgs e)
        {
            LoadingForm.ShowLoadingForm();

            udnoectc = udnoectc.get_udnoectc_by_GUID(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, udnoectc.GUID);


            Dialog_備藥清單.form = this.ParentForm;
            this.Load += Dialog_變異紀錄_Load;
            if (udnoectc == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("異常:查無資料", 2000);
                this.Invoke(new Action(delegate
                {
                    this.Close();
                    this.DialogResult = DialogResult.No;

                }));
                return;
            }
            this.sqL_DataGridView_變異紀錄.RowPostPaintingEvent += SqL_DataGridView_變異紀錄_RowPostPaintingEvent;
            LoadingForm.CloseLoadingForm();
        }

        private void Dialog_變異紀錄_Load(object sender, EventArgs e)
        {
            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            this.sqL_DataGridView_變異紀錄.RowsHeight = 150;
            this.sqL_DataGridView_變異紀錄.Init(table);
            this.sqL_DataGridView_變異紀錄.Set_ColumnWidth(sqL_DataGridView_變異紀錄.Width - 20, DataGridViewContentAlignment.MiddleLeft, "GUID");
            List<object[]> list_value = new List<object[]>();
            for(int i = 0; i < udnoectc.ctcvarsAry.Count; i++)
            {
                object[] value = new object[] { udnoectc.ctcvarsAry[i] };
                list_value.Add(value);
             
            }
            this.sqL_DataGridView_變異紀錄.RefreshGrid(list_value);
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
        }
        #region Event
        private void SqL_DataGridView_變異紀錄_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(Color.White))
            {
                int x = e.RowBounds.Left;
                int y = e.RowBounds.Top;
                int width = e.RowBounds.Width;
                int height = e.RowBounds.Height;
                Size size = new Size();
                PointF pointF = new PointF();

                e.Graphics.FillRectangle(brush, e.RowBounds);
                DrawingClass.Draw.DrawRoundShadow(e.Graphics, new RectangleF(x - 1, y - 1, width, height), Color.DarkGray, 5, 5);
                object[] value = this.sqL_DataGridView_變異紀錄.GetRowsList()[e.RowIndex];
                udnoectc_ctcvars udnoectc_ctcvars = value[0].ObjToClass<udnoectc_ctcvars>();
                string 序號 = $"{e.RowIndex + 1}.";
                string 藥名 = udnoectc_ctcvars.藥名;
                string 變異時間 = udnoectc_ctcvars.變異時間;
                string 變異原因 = $"變異原因:{udnoectc_ctcvars.變異原因}";
                string 變異內容 = $"變異內容:{udnoectc_ctcvars.變異內容}";
                string 說明 = $"說明:{udnoectc_ctcvars.說明}";

                DrawingClass.Draw.文字左上繪製(序號, new PointF(10, y + 10), new Font("標楷體", 16), Color.Black, e.Graphics);
                DrawingClass.Draw.文字左上繪製(藥名, new PointF(50, y + 10), new Font("標楷體", 16 , FontStyle.Bold), Color.Black, e.Graphics);
                size = 變異時間.MeasureText(new Font("標楷體", 16, FontStyle.Italic));
                DrawingClass.Draw.文字左上繪製(變異時間, new PointF(width - size.Width - 10, y + 10), new Font("標楷體", 16, FontStyle.Bold), Color.Black, e.Graphics);

                DrawingClass.Draw.文字左上繪製(變異內容, new PointF(20, y + 50), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                DrawingClass.Draw.文字左上繪製(變異原因, new PointF(20, y + 80), new Font("標楷體", 14, FontStyle.Bold), Color.Black, e.Graphics);
                DrawingClass.Draw.文字左上繪製(說明, new PointF(20, y + 110), new Font("標楷體", 14, FontStyle.Italic), Color.DimGray, e.Graphics);

            }

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
