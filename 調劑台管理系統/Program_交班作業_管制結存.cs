using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        [EnumDescription("")]
        public enum enum_交班對點_管制結存_紀錄顯示
        {
            [Description("日期時間,VARCHAR,50,None")]
            日期時間,
            [Description("床號,VARCHAR,50,None")]
            床號,
            [Description("病人姓名,VARCHAR,50,None")]
            病人姓名,
            [Description("病歷號,VARCHAR,50,None")]
            病歷號,
            [Description("調劑人,VARCHAR,50,None")]
            調劑人,
            [Description("交易量,VARCHAR,50,None")]
            交易量,
            [Description("結存量,VARCHAR,50,None")]
            結存量,
            [Description("盤點量,VARCHAR,50,None")]
            盤點量,
        }
        private int _交班對點_管制結存_現在頁面 = 0;
        public  int 交班對點_管制結存_現在頁面
        {
            set
            {
                if (value >= List_rJ_Buttons_交班對點_管制結存.Count) return;
                if (value < 0) return;
                _交班對點_管制結存_現在頁面 = value;
            }
            get
            {
                return _交班對點_管制結存_現在頁面;
            }
        }
        private int 交班對點_管制結存_選擇索引 = 0;
        List<RJ_Button> rJ_Buttons_交班對點_管制結存 = new List<RJ_Button>();
        List<List<RJ_Button>> List_rJ_Buttons_交班對點_管制結存 = new List<List<RJ_Button>>();
        private void Program_交班作業_管制結存_Init()
        {
            this.plC_RJ_Button_交班對點_管制結存_重置.MouseDownEvent += RJ_Button_交班對點_管制結存_重置_MouseDownEvent;
            this.plC_RJ_Button_交班對點_管制結存_搜尋.MouseDownEvent += PlC_RJ_Button_交班對點_管制結存_搜尋_MouseDownEvent;
            this.button_交班作業_管制結存_上一頁.Click += Button_交班作業_管制結存_上一頁_Click;
            this.button_交班作業_管制結存_下一頁.Click += Button_交班作業_管制結存_下一頁_Click;
            this.button_交班對點_管制結存_預覽列印.Click += Button_交班對點_管制結存_預覽列印_Click;
            this.button_交班對點_管制結存_匯出資料.Click += Button_交班對點_管制結存_匯出資料_Click;


            SQLUI.Table table = new SQLUI.Table(new enum_交班對點_管制結存_紀錄顯示());
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Init(table);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnVisible(false, new enum_交班對點_管制結存_紀錄顯示().GetEnumNames());
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_交班對點_管制結存_紀錄顯示.日期時間);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_交班對點_管制結存_紀錄顯示.床號);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleRight, enum_交班對點_管制結存_紀錄顯示.病人姓名);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleRight, enum_交班對點_管制結存_紀錄顯示.病歷號);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_交班對點_管制結存_紀錄顯示.調劑人);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_交班對點_管制結存_紀錄顯示.交易量);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_交班對點_管制結存_紀錄顯示.結存量);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_交班對點_管制結存_紀錄顯示.盤點量);
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.DataGridRefreshEvent += SqL_DataGridView_交班對點_管制結存_紀錄顯示_DataGridRefreshEvent;
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.DataGridRowsChangeRefEvent += SqL_DataGridView_交班對點_管制結存_紀錄顯示_DataGridRowsChangeRefEvent;
            this.plC_UI_Init.Add_Method(this.sub_Program_交班作業_管制結存);
        }


        bool flag_交班作業_管制結存_頁面更新 = false;
        private void sub_Program_交班作業_管制結存()
        {
            if (this.plC_ScreenPage_Main.PageText == "交班作業" && this.plC_ScreenPage_交班作業.PageText == "管制結存")
            {
                if (!flag_交班作業_管制結存_頁面更新)
                {
                    RJ_Button_交班對點_管制結存_重置_MouseDownEvent(null);
                    flag_交班作業_管制結存_頁面更新 = true;
                }
            }
            else
            {
                flag_交班作業_管制結存_頁面更新 = false;
            }
        }
        #region Function
        private List<RJ_Button> Function_交班對點_管制結存_取得Button()
        {
            List<RJ_Button> rJ_Buttons = new List<RJ_Button>();
            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_藥品設定表 = this.sqL_DataGridView_藥品設定表.SQL_GetAllRows(false);
            List<object[]> list_藥品設定表_buf = new List<object[]>();
            List<object[]> list_藥品管制方式設定 = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
            List<object[]> list_藥品管制方式設定_buf = new List<object[]>();
            for (int i = 0; i < list_藥品資料.Count; i++)
            {
                string 藥品碼 = list_藥品資料[i][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                string 藥品名稱 = list_藥品資料[i][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 管制級別 = list_藥品資料[i][(int)enum_藥品資料_藥檔資料.管制級別].ObjectToString();
                string 高價藥品 = list_藥品資料[i][(int)enum_藥品資料_藥檔資料.高價藥品].ObjectToString();
                string 生物製劑 = list_藥品資料[i][(int)enum_藥品資料_藥檔資料.生物製劑].ObjectToString();
                bool flag_自訂義 = false;
                bool flag_要新增 = false;
                list_藥品設定表_buf = list_藥品設定表.GetRows((int)enum_藥品設定表.藥碼, 藥品碼);
                if (list_藥品設定表_buf.Count > 0)
                {
                    if(list_藥品設定表_buf[0][(int)enum_藥品設定表.自定義].StringToBool())
                    {
                        if(list_藥品設定表_buf[0][(int)enum_藥品設定表.結存報表].StringToBool())
                        {
                            flag_要新增 = true;
                        }
                        flag_自訂義 = true;
                    }
                }
                if(!flag_自訂義)
                {
                    list_藥品管制方式設定_buf = list_藥品管制方式設定.GetRows((int)enum_medGeneralConfig.代號, 管制級別);
                    if(list_藥品管制方式設定_buf.Count > 0)
                    {
                        if(list_藥品管制方式設定_buf[0][(int)enum_medGeneralConfig.結存報表].StringToBool())
                        {
                            flag_要新增 = true;
                        }
                    }
                    list_藥品管制方式設定_buf = list_藥品管制方式設定.GetRows((int)enum_medGeneralConfig.代號, 高價藥品);
                    if (list_藥品管制方式設定_buf.Count > 0)
                    {
                        if (list_藥品管制方式設定_buf[0][(int)enum_medGeneralConfig.結存報表].StringToBool())
                        {
                            flag_要新增 = true;
                        }
                    }
                }
                if(flag_要新增)
                {
                    RJ_Button rJ_Button = new RJ_Button();
                    rJ_Button.Dock = DockStyle.Top;
                    rJ_Button.Height = 50;
                    rJ_Button.BackgroundColor = Color.Gray;
                    rJ_Button.TextAlign = ContentAlignment.MiddleLeft;
                    rJ_Button.Text = $" [{藥品碼}] {藥品名稱}";
                    rJ_Button.Name = $"{藥品碼}";
                    rJ_Button.BorderColor = Color.LightGreen;
                    rJ_Button.Font = new Font("微軟正黑體", 14);
                    rJ_Button.BorderRadius = 5;
                    rJ_Button.TabIndex = rJ_Buttons.Count;
                    rJ_Button.MouseDownEventEx += RJ_Button_MouseDownEventEx;
                    rJ_Buttons.Add(rJ_Button);

                }
            }
            return rJ_Buttons;
        }

        private void RJ_Button_MouseDownEventEx(RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            for (int i = 0; i < rJ_Buttons_交班對點_管制結存.Count; i++)
            {
                rJ_Buttons_交班對點_管制結存[i].state = false;
                rJ_Buttons_交班對點_管制結存[i].BorderSize = 0;
                if(rJ_Button.TabIndex == i)
                {
                    交班對點_管制結存_選擇索引 = i;
                }
            }
            rJ_Button.state = true;
            rJ_Button.BorderSize = 5;
            this.Invoke(new Action(delegate
            {
                rJ_Button.BackColor = Color.YellowGreen;
                rJ_TextBox_交班作業_管制結存_藥碼.Text = rJ_Button.Name;
            }));
            PlC_RJ_Button_交班對點_管制結存_搜尋_MouseDownEvent(null);
        }

        private void Functionn_交班對點_管制結存_顯示(int num)
        {
            this.Invoke(new Action(delegate
            {
                if (num >= List_rJ_Buttons_交班對點_管制結存.Count) return;
                panel_交班作業_管制結存_藥品表.SuspendLayout();
                panel_交班作業_管制結存_藥品表.Controls.Clear();
                for (int i = 0; i < List_rJ_Buttons_交班對點_管制結存[num].Count; i++)
                {
                    panel_交班作業_管制結存_藥品表.Controls.Add(List_rJ_Buttons_交班對點_管制結存[num][i]);
                }
                panel_交班作業_管制結存_藥品表.ResumeLayout();
            }));
        }
        #endregion
        #region Event
        private void SqL_DataGridView_交班對點_管制結存_紀錄顯示_DataGridRefreshEvent()
        {

        }
        private void SqL_DataGridView_交班對點_管制結存_紀錄顯示_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            int count = RowsList.Count;
            this.Invoke(new Action(delegate
            {
                rJ_Lable_交班對點_管制結存_查無資料.Visible = (count == 0);
            }));
        }
        private void Button_交班作業_管制結存_上一頁_Click(object sender, EventArgs e)
        {
            交班對點_管制結存_現在頁面--;
            Functionn_交班對點_管制結存_顯示(交班對點_管制結存_現在頁面);
        }
        private void Button_交班作業_管制結存_下一頁_Click(object sender, EventArgs e)
        {
            交班對點_管制結存_現在頁面++;
            Functionn_交班對點_管制結存_顯示(交班對點_管制結存_現在頁面);
        }
        private void RJ_Button_交班對點_管制結存_重置_MouseDownEvent(MouseEventArgs mevent)
        {
            rJ_Buttons_交班對點_管制結存.Clear();
            List_rJ_Buttons_交班對點_管制結存.Clear();
            List<RJ_Button> rJ_Buttons = Function_交班對點_管制結存_取得Button();
            rJ_Buttons_交班對點_管制結存 = rJ_Buttons;
            List<RJ_Button> rJ_Buttons_buf = new List<RJ_Button>();
            int panel_height = panel_交班作業_管制結存_藥品表.Height;
            int height_temp = 0;
            for (int i = 0; i < rJ_Buttons.Count; i++)
            {
                int Height = rJ_Buttons[i].Height;
                height_temp += Height;
                if(height_temp > panel_height)
                {
                    List_rJ_Buttons_交班對點_管制結存.Add(rJ_Buttons_buf);
                    rJ_Buttons_buf = new List<RJ_Button>();
                    height_temp = 0;
                }       
                rJ_Buttons_buf.Add(rJ_Buttons[i]);         
            }
            if(rJ_Buttons_buf.Count > 0) List_rJ_Buttons_交班對點_管制結存.Add(rJ_Buttons_buf);
            交班對點_管制結存_現在頁面 = 0;
            交班對點_管制結存_選擇索引 = 0;
            Functionn_交班對點_管制結存_顯示(交班對點_管制結存_現在頁面);


        }
        private void PlC_RJ_Button_交班對點_管制結存_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_交班作業_管制結存_藥碼.Text.StringIsEmpty()) return;
            DateTime dateTime_start = dateTimePicker_交班作業_管制結存_起始時間.Value;
            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 00, 00, 00);
            DateTime dateTime_end = dateTimePicker_交班作業_管制結存_結束時間.Value;
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);

            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            returnData.Value = $"{rJ_TextBox_交班作業_管制結存_藥碼.Text},{dateTime_start.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt();
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/transactions/serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            string jsondata = returnData.Data.JsonSerializationt();
            List<transactionsClass> transactionsClasses = jsondata.JsonDeserializet<List<transactionsClass>>();
            if(transactionsClasses.Count ==0)
            {

            }
            int 總消耗量 = 0;
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < transactionsClasses.Count; i++)
            {
                object[] value = new object[new enum_交班對點_管制結存_紀錄顯示().GetLength()];
                value[(int)enum_交班對點_管制結存_紀錄顯示.日期時間] = transactionsClasses[i].操作時間;
                value[(int)enum_交班對點_管制結存_紀錄顯示.床號] = transactionsClasses[i].床號;
                value[(int)enum_交班對點_管制結存_紀錄顯示.病人姓名] = transactionsClasses[i].病歷號;
                value[(int)enum_交班對點_管制結存_紀錄顯示.病歷號] = transactionsClasses[i].病歷號;
                value[(int)enum_交班對點_管制結存_紀錄顯示.調劑人] = transactionsClasses[i].操作人;
                value[(int)enum_交班對點_管制結存_紀錄顯示.交易量] = transactionsClasses[i].交易量;
                value[(int)enum_交班對點_管制結存_紀錄顯示.結存量] = transactionsClasses[i].結存量;
                value[(int)enum_交班對點_管制結存_紀錄顯示.盤點量] = transactionsClasses[i].盤點量;

                if(transactionsClasses[i].交易量.StringIsInt32())
                {
                    總消耗量 += transactionsClasses[i].交易量.StringToInt32();
                }
                list_value.Add(value);
            }
            this.Invoke(new Action(delegate 
            {
                rJ_TextBox_交班作業_管制結存_總消耗量.Text = (總消耗量 * -1).ToString();
            }));
 
            this.sqL_DataGridView_交班對點_管制結存_紀錄顯示.RefreshGrid(list_value);
        }
        private void Button_交班對點_管制結存_預覽列印_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_交班作業_管制結存_藥碼.Text.StringIsEmpty()) return;
            DateTime dateTime_start = dateTimePicker_交班作業_管制結存_起始時間.Value;
            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 00, 00, 00);
            DateTime dateTime_end = dateTimePicker_交班作業_管制結存_結束時間.Value;
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);

            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            returnData.Value = $"{rJ_TextBox_交班作業_管制結存_藥碼.Text},{dateTime_start.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/transactions/serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/transactions/get_sheet_by_serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            string jsondata = returnData.Data.JsonSerializationt();
            List<SheetClass> sheetClass = jsondata.JsonDeserializet<List<SheetClass>>();
            if(sheetClass.Count == 0)
            {
                MyMessageBox.ShowDialog("無資料可列印");
                return;
            }
            if (printerClass.ShowPreviewDialog(sheetClass, MyPrinterlib.PrinterClass.PageSize.A4_reverse) == DialogResult.OK)
            {

            }
        }
        private void Button_交班對點_管制結存_匯出資料_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_交班作業_管制結存_藥碼.Text.StringIsEmpty()) return;
            DateTime dateTime_start = dateTimePicker_交班作業_管制結存_起始時間.Value;
            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 00, 00, 00);
            DateTime dateTime_end = dateTimePicker_交班作業_管制結存_結束時間.Value;
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);

            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            returnData.Value = $"{rJ_TextBox_交班作業_管制結存_藥碼.Text},{dateTime_start.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/transactions/serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/transactions/get_sheet_by_serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            string jsondata = returnData.Data.JsonSerializationt();
            List<SheetClass> sheetClass = jsondata.JsonDeserializet<List<SheetClass>>();
            if (sheetClass.Count == 0)
            {
                MyMessageBox.ShowDialog("無資料可匯出");
                return;
            }
            if(this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
            {
                sheetClass.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);
            }
            MyMessageBox.ShowDialog("完成");
        }
        #endregion
    }
}
