using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;
using DeltaMotor485;
using DrawingClass;

namespace 癌症備藥機
{
    public partial class uc_備藥通知處方 : UserControl
    {
        public uc_備藥通知處方()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.Load += Uc_備藥通知處方_Load;
        
        }

        private void Uc_備藥通知處方_Load(object sender, EventArgs e)
        {
            string url = $"{Main_Form.API_Server}/api/ChemotherapyRxScheduling/init_udnoectc";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);

            List<Table> tables = json.JsonDeserializet<List<Table>>();

            this.sqL_DataGridView_備藥通知.RowsHeight = 150;
            this.sqL_DataGridView_備藥通知.Init(tables[0]);
            this.sqL_DataGridView_備藥通知.Set_ColumnVisible(false, new enum_udnoectc().GetEnumNames());

            this.sqL_DataGridView_備藥通知.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_udnoectc.病房);
            this.sqL_DataGridView_備藥通知.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_udnoectc.病歷號);
            this.sqL_DataGridView_備藥通知.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_udnoectc.診別);
            this.sqL_DataGridView_備藥通知.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_udnoectc.RegimenName);

            this.sqL_DataGridView_備藥通知.DataGridRowsChangeRefEvent += SqL_DataGridView_備藥通知_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_備藥通知.RowPostPaintingEvent += SqL_DataGridView_備藥通知_RowPostPaintingEvent;
            ToolStripMenuItem_取得即時備藥通知.Click += ToolStripMenuItem_取得即時備藥通知_Click;
        }

        public void RefreshGrid()
        {
            this.Function_取得備藥通知(DateTime.Now.AddDays(Main_Form.PLC_Device_更新往前第幾天醫令.Value * -1), DateTime.Now, true);
        }

        public List<object[]> GetSelectedRows()
        {
            List<object[]> list_value = this.sqL_DataGridView_備藥通知.Get_All_Select_RowsValues();
            return list_value;
        }

        public List<object[]> Function_取得備藥通知(DateTime dt_start, DateTime dt_end, bool flag_refresh_grid)
        {
            List<object[]> list_udnoectc = Function_取得備藥通知(dt_start, dt_end);
            List<object[]> list_udnoectc_buf = new List<object[]>();
            if (flag_refresh_grid)
            {
                List<object[]> list_備藥通知 = this.sqL_DataGridView_備藥通知.GetAllRows();
                List<object[]> list_備藥通知_buf = new List<object[]>();
                List<object[]> list_備藥通知_add = new List<object[]>();
                List<object[]> list_備藥通知_delete = new List<object[]>();

                for (int i = 0; i < list_udnoectc.Count; i++)
                {
                    string GUID = list_udnoectc[i][(int)enum_udnoectc.GUID].ObjectToString();
                    list_備藥通知_buf = (from temp in list_備藥通知
                                     where temp[(int)enum_udnoectc.GUID].ObjectToString() == GUID
                                     select temp).ToList();
                    if(list_備藥通知_buf.Count == 0)
                    {
                        list_備藥通知_add.Add(list_udnoectc[i]);
                    }
                }
                for (int i = 0; i < list_備藥通知.Count; i++)
                {
                    string GUID = list_備藥通知[i][(int)enum_udnoectc.GUID].ObjectToString();
                    list_udnoectc_buf = (from temp in list_udnoectc
                                         where temp[(int)enum_udnoectc.GUID].ObjectToString() == GUID
                                         select temp).ToList();
                    if (list_udnoectc_buf.Count == 0)
                    {
                        list_備藥通知_delete.Add(list_備藥通知[i]);
                    }
                }
                this.sqL_DataGridView_備藥通知.AddRows(list_備藥通知_add, false);
                this.sqL_DataGridView_備藥通知.DeleteExtra(list_備藥通知_delete, false);
                this.sqL_DataGridView_備藥通知.RefreshGrid();
            }
            return list_udnoectc;
        }
        static public List<object[]> Function_取得備藥通知(DateTime dt_start, DateTime dt_end)
        {
            string url = $"{Main_Form.API_Server}/api/ChemotherapyRxScheduling/get_udnoectc_by_ctdate_st_end";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = $"{dt_start.ToDateString()} 00:00:00,{dt_end.ToDateString()} 23:59:59";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json.JsonDeserializet<returnData>();

            List<udnoectc> udnoectcs = returnData.Data.ObjToListClass<udnoectc>();
            List<object[]> list_udnoectc = udnoectcs.ClassToSQL<udnoectc, enum_udnoectc>();

            return list_udnoectc;
        }
  
        private void SqL_DataGridView_備藥通知_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_備藥通知());

            List<object[]> RowsList_已調劑完成 = new List<object[]>();
            List<object[]> RowsList_未調劑完成 = new List<object[]>();
            List<object[]> RowsList_buf = new List<object[]>();

            RowsList_已調劑完成 = (from temp in RowsList
                              where temp[(int)enum_udnoectc.調劑藥師].ObjectToString().StringIsEmpty() == false
                              select temp).ToList();
            RowsList_未調劑完成 = (from temp in RowsList
                              where temp[(int)enum_udnoectc.調劑藥師].ObjectToString().StringIsEmpty() == true
                              select temp).ToList();

            RowsList_buf.LockAdd(RowsList_未調劑完成);
            RowsList_buf.LockAdd(RowsList_已調劑完成);
            RowsList = RowsList_buf;
        }
        private void SqL_DataGridView_備藥通知_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            Color row_Backcolor = Color.White;
            Color row_Forecolor = Color.Black;

            
            object[] value = this.sqL_DataGridView_備藥通知.GetRowsList()[e.RowIndex];
            udnoectc udnoectc = value.SQLToClass<udnoectc, enum_udnoectc>();
            if (udnoectc.調劑藥師.StringIsEmpty() == false)
            {
                row_Backcolor = Color.Silver;
                row_Forecolor = Color.White;
            }
            if (this.sqL_DataGridView_備藥通知.dataGridView.Rows[e.RowIndex].Selected)
            {
                row_Backcolor = Color.Blue;
                row_Forecolor = Color.White;
            }
            Brush brush = new SolidBrush(row_Backcolor);
            int x = e.RowBounds.Left;
            int y = e.RowBounds.Top;
            int width = e.RowBounds.Width;
            int height = e.RowBounds.Height;
            e.Graphics.FillRectangle(brush, e.RowBounds);
            DrawingClass.Draw.DrawRoundShadow(e.Graphics, new RectangleF(x - 1, y - 1, width, height), Color.DarkGray, 5, 5);


            string 序號 = $"{e.RowIndex + 1}.";
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

            DrawingClass.Draw.文字左上繪製(序號, new PointF(10, y + 10), new Font("標楷體", 16), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(診別, new PointF(40, y + 10), new Font("標楷體", 16), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(加入時間, new PointF(150, y + 10), new Font("標楷體", 16, FontStyle.Italic), row_Forecolor, e.Graphics);

            DrawingClass.Draw.文字左上繪製(病人姓名, 300, new PointF(20, y + 50), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(病歷號, new PointF(180, y + 50), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(病床號, new PointF(280, y + 50), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(生日, new PointF(420, y + 50), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(性別, new PointF(640, y + 50), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(身高, new PointF(690, y + 50), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(體重, new PointF(840, y + 50), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);

            DrawingClass.Draw.文字左上繪製(診斷, new PointF(20, y + 100), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(科別, new PointF(600, y + 100), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            DrawingClass.Draw.文字左上繪製(開立醫師, new PointF(840, y + 100), new Font("標楷體", 16, FontStyle.Bold), row_Forecolor, e.Graphics);
            brush.Dispose();
        }
        private void ToolStripMenuItem_取得即時備藥通知_Click(object sender, EventArgs e)
        {
            this.Function_取得備藥通知(DateTime.Now.AddDays(-30), DateTime.Now, true); 
        }

        public class ICP_備藥通知 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_udnoectc.加入時間].StringToDateTime();
                DateTime datetime2 = y[(int)enum_udnoectc.加入時間].StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                return compare;
            }
        }
    }
}
