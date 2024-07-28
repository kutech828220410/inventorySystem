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
using H_Pannel_lib;
using SQLUI;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Dialog_效期批號修改 : MyDialog
    {
        private object _device_UI;
        private Device _device;
        public Device Value
        {
            get
            {
                return _device;
            }
        }
        public Dialog_效期批號修改(Device device , object device_UI)
        {
            InitializeComponent();
            _device = device;
            _device_UI = device_UI;
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_修正批號.MouseDownEvent += PlC_RJ_Button_修正批號_MouseDownEvent;
            this.plC_RJ_Button_修正庫存.MouseDownEvent += PlC_RJ_Button_修正庫存_MouseDownEvent;
            this.Load += Dialog_效期批號修改_Load;
        }

        private void PlC_RJ_Button_修正庫存_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_效期批號 = this.sqL_DataGridView_效期批號.Get_All_Select_RowsValues();
            if(list_效期批號.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 2000);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            StockClass stockClass = list_效期批號[0][0].ObjectToString().JsonDeserializet<StockClass>();
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("修正庫存");
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            _device.效期庫存覆蓋(stockClass.Validity_period, dialog_NumPannel.Value.ToString());


            int 原有庫存 = Main_Form.Function_從SQL取得庫存(_device.Code);
            int 數量 = dialog_NumPannel.Value - stockClass.Qty.StringToInt32();
            string 效期 = stockClass.Validity_period;
            string 批號 = stockClass.Lot_number;


            string url = $"{Main_Form.API_Server}/api/transactions/add";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            transactionsClass transactionsClass = new transactionsClass();
            transactionsClass.動作 = enum_交易記錄查詢動作.修正庫存.GetEnumName();
            transactionsClass.藥品碼 = _device.Code;
            transactionsClass.藥品名稱 = _device.Name;
            transactionsClass.操作人 = Main_Form.登入者名稱;
            transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
            transactionsClass.開方時間 = DateTime.Now.ToDateTimeString_6();
            transactionsClass.庫存量 = 原有庫存.ToString(); ;
            transactionsClass.交易量 = 數量.ToString();
            transactionsClass.結存量 = (原有庫存 + 數量).ToString();
            transactionsClass.備註 += $"[效期]:{效期},[批號]:{批號}";
            returnData.Data = transactionsClass;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);

            if (_device_UI is StorageUI_EPD_266)
            {
                StorageUI_EPD_266 storageUI_EPD_266 = _device_UI as StorageUI_EPD_266;
                storageUI_EPD_266.SQL_ReplaceStorage((Storage)_device);
            }
            if (_device_UI is RowsLEDUI)
            {
                RowsLEDUI rowsLEDUI = _device_UI as RowsLEDUI;
                RowsLED rowsLED = rowsLEDUI.SQL_GetRowsLED(_device.IP);
                if(rowsLED != null)
                {
                    RowsDevice rowsDevice = rowsLED.SortByGUID(_device.GUID);
                    if (rowsDevice != null)
                    {
                        rowsDevice = rowsDevice.Paste(_device);
                        rowsLED.ReplaceRowsDevice(rowsDevice);
                        rowsLEDUI.SQL_ReplaceRowsLED(rowsLED);

                    }
                }
            }
            List<StockClass> stockClasses = _device.stockClasses;

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < stockClasses.Count; i++)
            {
                object[] value = new object[] { stockClasses[i].JsonSerializationt() };
                list_value.Add(value);
            }
            this.sqL_DataGridView_效期批號.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_修正批號_MouseDownEvent(MouseEventArgs mevent)
        {
           
        }
        private void Dialog_效期批號修改_Load(object sender, EventArgs e)
        {
            rJ_Lable_藥品資訊.Text = $"({_device.Code}){_device.Name}";
            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.None);

            this.sqL_DataGridView_效期批號.RowsHeight = 70;
            this.sqL_DataGridView_效期批號.Init(table);
            this.sqL_DataGridView_效期批號.Set_ColumnWidth(sqL_DataGridView_效期批號.Width - 20, DataGridViewContentAlignment.MiddleLeft, "GUID");
            this.sqL_DataGridView_效期批號.RowPostPaintingEvent += SqL_DataGridView_效期批號_RowPostPaintingEvent;


            List<StockClass> stockClasses = _device.stockClasses;

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < stockClasses.Count; i++)
            {
                object[] value = new object[] { stockClasses[i].JsonSerializationt() };
                list_value.Add(value);
            }
            this.sqL_DataGridView_效期批號.RefreshGrid(list_value);
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
                string 庫存 = $"{stockClass.Qty}";
                string str = $"{序號} 效期 : {效期} 批號 {((批號.StringIsEmpty()) ? "無" : $"{批號}")}   庫存:{庫存}";
                DrawingClass.Draw.文字左上繪製(str, new PointF(10, y + 10), new Font("標楷體", 16), Color.Black, e.Graphics);

            }
        }

        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return;
            }));
        }
        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
                return;
            }));
        }
    }
}
