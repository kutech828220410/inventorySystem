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
using MyPrinterlib;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        public enum ContextMenuStrip_交班作業_交班表_設定
        {
            交班藥品選擇,
            //班別時間,
        }
      
        private PrinterClass printerClass_交班表 = new PrinterClass();
        private void Program_交班作業_交班表_Init()
        {
            SQLUI.Table table = new SQLUI.Table(new enum_medShiftList());
            this.sqL_DataGridView_交班作業_交班表_交班明細.RowsHeight = 40;
            this.sqL_DataGridView_交班作業_交班表_交班明細.Init(table);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnVisible(false, new enum_medShiftList().GetEnumNames());
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_medShiftList.藥碼);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(600, DataGridViewContentAlignment.MiddleLeft, enum_medShiftList.藥名);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_medShiftList.現有庫存);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_medShiftList.處方支出);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_medShiftList.處方數量);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_medShiftList.實際庫存);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(220, DataGridViewContentAlignment.MiddleCenter, enum_medShiftList.起始時間);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(220, DataGridViewContentAlignment.MiddleCenter, enum_medShiftList.結束時間);
            this.sqL_DataGridView_交班作業_交班表_交班明細.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_medShiftList.管制級別);

            this.plC_RJ_Button_交班作業_交班表_生成明細.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_生成明細_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_處方檢視.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_處方檢視_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_設定.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_設定_MouseDownEvent;

            this.button_交班作業_交班表_預覽列印.Click += Button_交班作業_交班表_預覽列印_Click;

            this.plC_RJ_Button_交班作業_交班表_班別_白班.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_白班_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_班別_小夜班.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_小夜班_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_班別_大夜班.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_大夜班_MouseDownEvent;

            printerClass_交班表.Init();
            printerClass_交班表.PrintPageEvent += PrinterClass_交班表_PrintPageEvent;

            this.plC_UI_Init.Add_Method(this.sub_Program_交班作業_交班表);
        }

     

        bool flag_交班作業_交班表_頁面更新 = false;
        private void sub_Program_交班作業_交班表()
        {
            if (this.plC_ScreenPage_Main.PageText == "交班作業" && this.plC_ScreenPage_交班作業.PageText == "交班表")
            {
                if (!flag_交班作業_交班表_頁面更新)
                {
                    if(!plC_RJ_Button_交班作業_交班表_班別_白班.Bool && !plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool && !plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool)
                    {
                        plC_RJ_Button_交班作業_交班表_班別_白班.Bool = true;
                        plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool = false;
                        plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool = false;
                    }
                    flag_交班作業_交班表_頁面更新 = true;
                }
            }
            else
            {
                flag_交班作業_交班表_頁面更新 = false;
            }
        }
        #region Function
        private List<string> Function_交班對點_交班表_取得需交班藥品()
        {
            List<string> list_codes = new List<string>();
            List<object[]> list_藥檔資料_buf = new List<object[]>();
            List<object[]> list_交班藥品 = new List<object[]>();



            string url = $"{Main_Form.API_Server}/api/medShiftConfig/get_all";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Main_Form.ServerName}";
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            List<medShiftConfigClass> medShiftConfigClasses = returnData.Data.ObjToListClass<medShiftConfigClass>();
            for (int i = 0; i < medShiftConfigClasses.Count; i++)
            {
                if (medShiftConfigClasses[i].是否交班 != true.ToString()) continue;
                list_codes.Add(medShiftConfigClasses[i].藥品碼);
            }
            return list_codes;
        }


        #endregion
        #region Event
        private void PrinterClass_交班表_PrintPageEvent(object sender, Graphics g, int width, int height, int page_num)
        {
            Rectangle rectangle = new Rectangle(0, 0, width, height);
            SheetClass sheetClass = printerClass_交班表.GetSheetClass(page_num);
            using (Bitmap bitmap = sheetClass.GetBitmap(width, height, 0.7, H_Alignment.Center, V_Alignment.Top, 0, 50))
            {
                rectangle.Height = bitmap.Height;
                g.DrawImage(bitmap, rectangle);
            }
        }
        private void PlC_RJ_Button_交班作業_交班表_班別_大夜班_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_交班作業_交班表_班別_白班.Bool = false;
            plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool = false;
            plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool = true;
        }

        private void PlC_RJ_Button_交班作業_交班表_班別_小夜班_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_交班作業_交班表_班別_白班.Bool = false;
            plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool = true;
            plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool = false;
        }

        private void PlC_RJ_Button_交班作業_交班表_班別_白班_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_交班作業_交班表_班別_白班.Bool = true;
            plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool = false;
            plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool = false;
        }
        private void PlC_RJ_Button_交班作業_交班表_班別_設定_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_交班作業_交班表_設定().GetEnumNames());
                if (dialog_ContextMenuStrip.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_交班作業_交班表_設定.交班藥品選擇.GetEnumName())
                    {
                        Dialog_交班藥品選擇 dialog_交班藥品選擇 = new Dialog_交班藥品選擇(dBConfigClass.DB_Basic);
                        dialog_交班藥品選擇.ShowDialog();
                    }
                    //if (dialog_ContextMenuStrip.Value == ContextMenuStrip_交班作業_交班表_設定.班別時間.GetEnumName())
                    //{

                    //}
                }
            }));
        
        }
        private void PlC_RJ_Button_交班作業_交班表_班別_生成明細_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                if (!plC_RJ_Button_交班作業_交班表_班別_白班.Bool && !plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool && !plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool)
                {
                    MyMessageBox.ShowDialog("未選擇班別!");
                    return;
                }
                DateTime dateTime_st = new DateTime();
                DateTime dateTime_end = new DateTime();
                DateTime dateTime_temp = rJ_DatePicker_交班作業_交班表_日期.Value;
                if (plC_RJ_Button_交班作業_交班表_班別_白班.Bool)
                {
                    dateTime_st = $"{dateTime_temp.ToDateString()} 08:00:00".StringToDateTime();
                    dateTime_end = $"{dateTime_temp.ToDateString()} 16:00:00".StringToDateTime();
                }
                if (plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool)
                {
                    dateTime_st = $"{dateTime_temp.ToDateString()} 16:00:00".StringToDateTime();
                    dateTime_end = $"{dateTime_temp.ToDateString()} 23:59:59".StringToDateTime();
                }
                if (plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool)
                {
                    dateTime_st = $"{dateTime_temp.ToDateString()} 00:00:00".StringToDateTime();
                    dateTime_end = $"{dateTime_temp.ToDateString()} 08:00:00".StringToDateTime();
                }
                List<string> list_Codes = Function_交班對點_交班表_取得需交班藥品();
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                if (list_Codes.Count == 0)
                {
                    MyMessageBox.ShowDialog("未設定管制結存藥品,請至藥品資料設定!");
                    return;
                }
                List<object[]> list_交易紀錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRowsByBetween((int)enum_交易記錄查詢資料.操作時間, dateTime_st, dateTime_end, false);
                List<object[]> list_交易紀錄_buf = new List<object[]>();
         
                //if (list_交易紀錄.Count == 0)
                //{
                //    MyMessageBox.ShowDialog("找無任何處方資料!");
                //    return;
                //}
                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < list_Codes.Count; i++)
                {
                    List<object[]> list_交易紀錄_buf_buf = new List<object[]>();
                    string 藥碼 = list_Codes[i];
                    list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥碼);
                    if (list_藥品資料_buf.Count > 0)
                    {
                        object[] value = new object[new enum_medShiftList().GetLength()];
                        string 藥名 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                        string 管制級別 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.管制級別].ObjectToString();
                        list_交易紀錄_buf = list_交易紀錄.GetRows((int)enum_交易記錄查詢資料.藥品碼, 藥碼);
                        list_交易紀錄_buf_buf.LockAdd(list_交易紀錄_buf.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.手輸領藥.GetEnumName()));
                        list_交易紀錄_buf_buf.LockAdd(list_交易紀錄_buf.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.手輸退藥.GetEnumName()));
                        list_交易紀錄_buf_buf.LockAdd(list_交易紀錄_buf.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.掃碼領藥.GetEnumName()));
                        list_交易紀錄_buf_buf.LockAdd(list_交易紀錄_buf.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.掃碼退藥.GetEnumName()));
                        list_交易紀錄_buf_buf.LockAdd(list_交易紀錄_buf.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統領藥.GetEnumName()));
                        list_交易紀錄_buf_buf.LockAdd(list_交易紀錄_buf.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.批次領藥.GetEnumName()));
                        list_交易紀錄_buf_buf.LockAdd(list_交易紀錄_buf.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.批次過帳.GetEnumName()));
                     
                        int 處方支出 = 0;
                        int 處方數量 = list_交易紀錄_buf_buf.Count;
                        int 現有庫存 = Function_從SQL取得庫存(藥碼);
                        if (list_交易紀錄_buf_buf.Count > 0)
                        {

                            if (現有庫存 == -999) 現有庫存 = 0;
                            for (int k = 0; k < list_交易紀錄_buf_buf.Count; k++)
                            {
                                int 交易量 = list_交易紀錄_buf_buf[k][(int)enum_交易記錄查詢資料.交易量].StringToInt32();
                                處方支出 += 交易量;
                            }
                        }
                        value[(int)enum_medShiftList.藥碼] = 藥碼;
                        value[(int)enum_medShiftList.藥名] = 藥名;
                        value[(int)enum_medShiftList.處方支出] = 處方支出;
                        value[(int)enum_medShiftList.處方數量] = 處方數量;
                        value[(int)enum_medShiftList.現有庫存] = 現有庫存;
                        value[(int)enum_medShiftList.起始時間] = dateTime_st.ToDateTimeString();
                        value[(int)enum_medShiftList.結束時間] = dateTime_end.ToDateTimeString();
                        value[(int)enum_medShiftList.管制級別] = 管制級別;

                        list_value.Add(value);
                    }
                }
                List<object[]> list_value_buf = new List<object[]>();
                list_value.Sort(new Icp_交班作業_交班表_交班明細());
                this.sqL_DataGridView_交班作業_交班表_交班明細.RefreshGrid(list_value);
                this.plC_RJ_GroupBox_交班作業_交班表_交班明細.TitleTexts = $"交班明細 [{dateTime_st.ToDateTimeString()}]-[{dateTime_end.ToDateTimeString()}]";
            }));
            
        }
        private void PlC_RJ_Button_交班作業_交班表_班別_處方檢視_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                List<object[]> list_交班明細 = this.sqL_DataGridView_交班作業_交班表_交班明細.Get_All_Select_RowsValues();
                if (list_交班明細.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選擇資料!");
                    return;
                }
                string 藥碼 = list_交班明細[0][(int)enum_medShiftList.藥碼].ObjectToString();
                string 藥名 = list_交班明細[0][(int)enum_medShiftList.藥名].ObjectToString();
                string 起始時間 = list_交班明細[0][(int)enum_medShiftList.起始時間].ObjectToString();
                string 結束時間 = list_交班明細[0][(int)enum_medShiftList.結束時間].ObjectToString();
                List<object[]> list_交易紀錄_buf_buf = new List<object[]>();
                List<object[]> list_交易紀錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRowsByBetween((int)enum_交易記錄查詢資料.操作時間, 起始時間, 結束時間, false);
                List<object[]> list_交易紀錄_buf = list_交易紀錄.GetRows((int)enum_交易記錄查詢資料.藥品碼, 藥碼);
 

                Dialog_交易紀錄明細 dialog_交易紀錄明細 = new Dialog_交易紀錄明細(list_交易紀錄_buf, 藥碼 , 藥名);
                dialog_交易紀錄明細.ShowDialog();
            }));
        }
        private void Button_交班作業_交班表_預覽列印_Click(object sender, EventArgs e)
        {
            List<object[]> list_交班明細 = this.sqL_DataGridView_交班作業_交班表_交班明細.GetAllRows();
            if (list_交班明細.Count == 0)
            {
                MyMessageBox.ShowDialog("未產生交班明細資料!");
                return;
            }
            List<medShiftListClass> medShiftListClasses = list_交班明細.SQLToClass<medShiftListClass, enum_medShiftList>();
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.Data = medShiftListClasses;
            returnData.Value = this.plC_RJ_GroupBox_交班作業_交班表_交班明細.TitleTexts;

            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/medShiftConfig/GET_SheetClass", json_in);
            returnData = json.JsonDeserializet<returnData>();
            json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/medShiftConfig/GET_SheetClass", json_in);
            returnData = json.JsonDeserializet<returnData>();

            string jsondata = returnData.Data.JsonSerializationt();
            List<SheetClass> sheetClass = jsondata.JsonDeserializet<List<SheetClass>>();
            if (sheetClass.Count == 0)
            {
                MyMessageBox.ShowDialog("無資料可列印");
                return;
            }
            if (printerClass_交班表.ShowPreviewDialog(sheetClass, MyPrinterlib.PrinterClass.PageSize.A4) == DialogResult.OK)
            {

            }
        }
        #endregion

        public class Icp_交班作業_交班表_交班明細 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 管制級別_0 = x[(int)enum_medShiftList.管制級別].ObjectToString();
                string 管制級別_1 = y[(int)enum_medShiftList.管制級別].ObjectToString();
                int temp0 = 管制級別_0.CompareTo(管制級別_1);
                if(temp0 == 0)
                {
                    string 藥品碼_0 = x[(int)enum_medShiftList.藥碼].ObjectToString();
                    string 藥品碼_1 = y[(int)enum_medShiftList.藥碼].ObjectToString();
                    return 藥品碼_0.CompareTo(藥品碼_1);
                }
                else
                {
                    return temp0;
                }
         
            }
        }
    }
}
