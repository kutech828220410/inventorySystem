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
    public partial class Form1 : Form
    {
        public enum ContextMenuStrip_交班作業_交班表_設定
        {
            班別時間,
            交班藥品選擇,
        }
        public enum enum_交班作業_交班表_交班明細
        {
            藥碼,
            藥名,
            現有庫存,
            處方支出,
            處方數量,
            起始時間,
            結束時間,
        }
        private void Program_交班作業_交班表_Init()
        {

            this.sqL_DataGridView_交班作業_交班表_交班明細.Init();

            this.plC_RJ_Button_交班作業_交班表_班別_生成明細.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_生成明細_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_班別_處方檢視.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_處方檢視_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_班別_設定.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_設定_MouseDownEvent;

            this.plC_RJ_Button_交班作業_交班表_班別_白班.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_白班_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_班別_小夜班.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_小夜班_MouseDownEvent;
            this.plC_RJ_Button_交班作業_交班表_班別_大夜班.MouseDownEvent += PlC_RJ_Button_交班作業_交班表_班別_大夜班_MouseDownEvent;

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
                list_藥品設定表_buf = list_藥品設定表.GetRows((int)enum_藥品設定表.藥品碼, 藥品碼);
                if (list_藥品設定表_buf.Count > 0)
                {
                    if (list_藥品設定表_buf[0][(int)enum_藥品設定表.自定義].StringToBool())
                    {
                        if (list_藥品設定表_buf[0][(int)enum_藥品設定表.結存報表].StringToBool())
                        {
                            flag_要新增 = true;
                        }
                        flag_自訂義 = true;
                    }
                }
                if (!flag_自訂義)
                {
                    list_藥品管制方式設定_buf = list_藥品管制方式設定.GetRows((int)enum_藥品管制方式設定.代號, 管制級別);
                    if (list_藥品管制方式設定_buf.Count > 0)
                    {
                        if (list_藥品管制方式設定_buf[0][(int)enum_藥品管制方式設定.結存報表].StringToBool())
                        {
                            flag_要新增 = true;
                        }
                    }
                    list_藥品管制方式設定_buf = list_藥品管制方式設定.GetRows((int)enum_藥品管制方式設定.代號, 高價藥品);
                    if (list_藥品管制方式設定_buf.Count > 0)
                    {
                        if (list_藥品管制方式設定_buf[0][(int)enum_藥品管制方式設定.結存報表].StringToBool())
                        {
                            flag_要新增 = true;
                        }
                    }
                }
                if (flag_要新增)
                {

                    list_codes.Add(藥品碼);
                }
            }
            return list_codes;
        }
        #endregion
        #region Event
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
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_交班作業_交班表_設定.班別時間.GetEnumName())
                    {

                    }
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
                    dateTime_end = $"{dateTime_temp.ToDateString()} 17:59:59".StringToDateTime();
                }
                if (plC_RJ_Button_交班作業_交班表_班別_小夜班.Bool)
                {
                    dateTime_st = $"{dateTime_temp.ToDateString()} 18:00:00".StringToDateTime();
                    dateTime_end = $"{dateTime_temp.ToDateString()} 23:59:59".StringToDateTime();
                }
                if (plC_RJ_Button_交班作業_交班表_班別_大夜班.Bool)
                {
                    dateTime_st = $"{dateTime_temp.ToDateString()} 00:00:00".StringToDateTime();
                    dateTime_end = $"{dateTime_temp.ToDateString()} 07:59:59".StringToDateTime();
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
                    string 藥碼 = list_Codes[i];
                    list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥碼);
                    if (list_藥品資料_buf.Count > 0)
                    {
                        object[] value = new object[new enum_交班作業_交班表_交班明細().GetLength()];
                        string 藥名 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                        list_交易紀錄_buf = list_交易紀錄.GetRows((int)enum_交易記錄查詢資料.藥品碼, 藥碼);
                        int 處方支出 = 0;
                        int 處方數量 = list_交易紀錄_buf.Count;
                        int 現有庫存 = Function_從SQL取得庫存(藥碼);
                        if (list_交易紀錄_buf.Count > 0)
                        {

                            if (現有庫存 == -999) 現有庫存 = 0;
                            for (int k = 0; k < list_交易紀錄_buf.Count; k++)
                            {
                                int 交易量 = list_交易紀錄_buf[k][(int)enum_交易記錄查詢資料.交易量].StringToInt32();
                                處方支出 += 交易量;
                            }
                        }
                        value[(int)enum_交班作業_交班表_交班明細.藥碼] = 藥碼;
                        value[(int)enum_交班作業_交班表_交班明細.藥名] = 藥名;
                        value[(int)enum_交班作業_交班表_交班明細.處方支出] = 處方支出;
                        value[(int)enum_交班作業_交班表_交班明細.處方數量] = 處方數量;
                        value[(int)enum_交班作業_交班表_交班明細.現有庫存] = 現有庫存;
                        value[(int)enum_交班作業_交班表_交班明細.起始時間] = dateTime_st.ToDateTimeString();
                        value[(int)enum_交班作業_交班表_交班明細.結束時間] = dateTime_end.ToDateTimeString();
                        list_value.Add(value);
                    }
                }
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
                string 藥碼 = list_交班明細[0][(int)enum_交班作業_交班表_交班明細.藥碼].ObjectToString();
                string 藥名 = list_交班明細[0][(int)enum_交班作業_交班表_交班明細.藥名].ObjectToString();
                string 起始時間 = list_交班明細[0][(int)enum_交班作業_交班表_交班明細.起始時間].ObjectToString();
                string 結束時間 = list_交班明細[0][(int)enum_交班作業_交班表_交班明細.結束時間].ObjectToString();

                List<object[]> list_交易紀錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRowsByBetween((int)enum_交易記錄查詢資料.操作時間, 起始時間, 結束時間, false);
                List<object[]> list_交易紀錄_buf = list_交易紀錄.GetRows((int)enum_交易記錄查詢資料.藥品碼, 藥碼);

                Dialog_交易紀錄明細 dialog_交易紀錄明細 = new Dialog_交易紀錄明細(list_交易紀錄_buf, 藥碼 , 藥名);
                dialog_交易紀錄明細.ShowDialog();
            }));
        }
        #endregion
    }
}
