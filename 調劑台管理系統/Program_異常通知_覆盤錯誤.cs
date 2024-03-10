using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using HIS_DB_Lib;
using SQLUI;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        PLC_Device PLC_Device_異常通知_覆盤錯誤 = new PLC_Device("S800");
        private void Program_異常通知_覆盤錯誤_Init()
        {
            string url = $"{dBConfigClass.Api_URL}/api/MedRecheckLog/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            this.sqL_DataGridView_異常通知_覆盤錯誤.DataBaseName = table.DBName;
            this.sqL_DataGridView_異常通知_覆盤錯誤.Server = table.Server;
            this.sqL_DataGridView_異常通知_覆盤錯誤.UserName = table.Username;
            this.sqL_DataGridView_異常通知_覆盤錯誤.Password = table.Password;
            this.sqL_DataGridView_異常通知_覆盤錯誤.Port = table.Port.StringToUInt32();
            this.sqL_DataGridView_異常通知_覆盤錯誤.Init(table);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnVisible(false, new enum_MedRecheckLog().GetEnumNames());

            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.藥碼);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.藥名);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.效期);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.批號);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.操作人);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.校正庫存值);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.系統理論值);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.覆盤理論值);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.排除時間);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.發生時間);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.狀態);

            this.plC_RJ_Button_異常通知_覆盤錯誤_未排除顯示.MouseDownEvent += PlC_RJ_Button_異常通知_覆盤錯誤_未排除顯示_MouseDownEvent;
            this.plC_RJ_Button_異常通知_覆盤錯誤_選取藥品異常排除.MouseDownEvent += PlC_RJ_Button_異常通知_覆盤錯誤_選取藥品異常排除_MouseDownEvent;
            this.plC_RJ_Button_異常通知_覆盤錯誤_發生時段搜尋.MouseDownEvent += PlC_RJ_Button_異常通知_覆盤錯誤_發生時段搜尋_MouseDownEvent;
            this.plC_RJ_Button_異常通知_覆盤錯誤_排除時段搜尋.MouseDownEvent += PlC_RJ_Button_異常通知_覆盤錯誤_排除時段搜尋_MouseDownEvent;

            plC_UI_Init.Add_Method(Program_異常通知_覆盤錯誤);
        }     


        MyTimerBasic MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄 = new MyTimerBasic(5000);
        private void PlC_RJ_Button_異常通知_覆盤錯誤_選取藥品異常排除_MouseDownEvent(MouseEventArgs mevent)
        {
            string msg = "";
            bool flag_修正庫存 = false;
            int 效正庫存值 = 0;
            List<object[]> list_value = this.sqL_DataGridView_異常通知_覆盤錯誤.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            string 藥碼 = list_value[0][(int)enum_MedRecheckLog.藥碼].ObjectToString();
            string 藥名 = list_value[0][(int)enum_MedRecheckLog.藥名].ObjectToString();

            msg = $"藥碼:{藥碼}\n" +
                $"藥名:{藥名}\n" +
                $"是否排除覆盤異常?";
            if (MyMessageBox.ShowDialog($"{msg}", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            msg = $"藥碼:{藥碼}\n" +
              $"藥名:{藥名}\n" +
              $"是否修正[庫存值]?";
            if (MyMessageBox.ShowDialog($"{msg}", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
            {
                flag_修正庫存 = true;
            }

            if(flag_修正庫存)
            {
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入[效正庫存值]");
                if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
                效正庫存值 = dialog_NumPannel.Value;
                Function_異常通知_覆盤錯誤_庫存異動(藥碼, 藥名,效正庫存值);
            }
            Function_異常通知_覆盤錯誤_設定已排除(藥碼, 效正庫存值);
            MyMessageBox.ShowDialog("完成!");
        }
        private void Program_異常通知_覆盤錯誤()
        {
            if (MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄.IsTimeOut())
            {
                List<MedRecheckLogClass> medRecheckLogClasses = Function_異常通知_覆盤錯誤_取得未排除();
                PLC_Device_異常通知_覆盤錯誤.Bool = (medRecheckLogClasses.Count != 0);

                MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄.TickStop();
                MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄.StartTickTime();
            }
        }

        private void PlC_RJ_Button_異常通知_覆盤錯誤_排除時段搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            DateTime dateTime_st = rJ_DatePicker_異常通知_覆盤錯誤_排除時段_起始.Value;
            DateTime dateTime_end = rJ_DatePicker_異常通知_覆盤錯誤_排除時段_結束.Value;
            List<MedRecheckLogClass> MedRecheckLogClasses = Function_異常通知_覆盤錯誤_排除時段搜尋(dateTime_st, dateTime_end);
            List<object[]> list_med_recheck = MedRecheckLogClasses.ClassToSQL<MedRecheckLogClass, enum_MedRecheckLog>();
            this.sqL_DataGridView_異常通知_覆盤錯誤.RefreshGrid(list_med_recheck);
        }
        private void PlC_RJ_Button_異常通知_覆盤錯誤_發生時段搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            DateTime dateTime_st = rJ_DatePicker_異常通知_覆盤錯誤_發生時段_起始.Value;
            DateTime dateTime_end = rJ_DatePicker_異常通知_覆盤錯誤_發生時段_結束.Value;
            List<MedRecheckLogClass> MedRecheckLogClasses = Function_異常通知_覆盤錯誤_發生時段搜尋(dateTime_st , dateTime_end);
            List<object[]> list_med_recheck = MedRecheckLogClasses.ClassToSQL<MedRecheckLogClass, enum_MedRecheckLog>();
            this.sqL_DataGridView_異常通知_覆盤錯誤.RefreshGrid(list_med_recheck);
        }
        #region Function
        private void PlC_RJ_Button_異常通知_覆盤錯誤_未排除顯示_MouseDownEvent(MouseEventArgs mevent)
        {
            List<MedRecheckLogClass> MedRecheckLogClasses = Function_異常通知_覆盤錯誤_取得未排除();
            List<object[]> list_med_recheck = MedRecheckLogClasses.ClassToSQL<MedRecheckLogClass, enum_MedRecheckLog>();
            this.sqL_DataGridView_異常通知_覆盤錯誤.RefreshGrid(list_med_recheck);
        }
        private List<MedRecheckLogClass> Function_異常通知_覆盤錯誤_取得未排除()
        {
            List<MedRecheckLogClass> medRecheckLogClasses = new List<MedRecheckLogClass>();
            string url = $"{API_Server}/api/MedRecheckLog/get_ng_state_data";
            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;

            string json_in = returnData.JsonSerializationt(true);
            string json_result = Net.WEBApiPostJson(url, json_in , false);

            returnData = json_result.JsonDeserializet<returnData>();
            medRecheckLogClasses = returnData.Data.ObjToListClass<MedRecheckLogClass>();
            return medRecheckLogClasses;
        }
        private List<MedRecheckLogClass> Function_異常通知_覆盤錯誤_發生時段搜尋(DateTime dateTime_st, DateTime dateTime_end)
        {
            dateTime_st = new DateTime(dateTime_st.Year, dateTime_st.Month, dateTime_st.Day, 00, 00, 00);
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);
            List<MedRecheckLogClass> medRecheckLogClasses = new List<MedRecheckLogClass>();
            string url = $"{API_Server}/api/MedRecheckLog/get_by_occurrence_time_st_end";
            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;
            returnData.Value = $"{dateTime_st.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt(true);
            string json_result = Net.WEBApiPostJson(url, json_in);

            returnData = json_result.JsonDeserializet<returnData>();
            medRecheckLogClasses = returnData.Data.ObjToListClass<MedRecheckLogClass>();
            return medRecheckLogClasses;
        }
        private List<MedRecheckLogClass> Function_異常通知_覆盤錯誤_排除時段搜尋(DateTime dateTime_st, DateTime dateTime_end)
        {
            dateTime_st = new DateTime(dateTime_st.Year, dateTime_st.Month, dateTime_st.Day, 00, 00, 00);
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);
            List<MedRecheckLogClass> medRecheckLogClasses = new List<MedRecheckLogClass>();
            string url = $"{API_Server}/api/MedRecheckLog/get_by_troubleshooting_time_st_end";
            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;
            returnData.Value = $"{dateTime_st.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt(true);
            string json_result = Net.WEBApiPostJson(url, json_in);

            returnData = json_result.JsonDeserializet<returnData>();
            medRecheckLogClasses = returnData.Data.ObjToListClass<MedRecheckLogClass>();
            return medRecheckLogClasses;
        }
        private void Function_異常通知_覆盤錯誤_設定已排除(string 藥碼,int 校正庫存值)
        {
            List<MedRecheckLogClass> medRecheckLogClasses = new List<MedRecheckLogClass>();
            string url = $"{API_Server}/api/MedRecheckLog/replace_QTY_by_code";
            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;
            returnData.Value = $"{藥碼},{校正庫存值}";
            string json_in = returnData.JsonSerializationt(true);
            string json_result = Net.WEBApiPostJson(url, json_in);

            returnData = json_result.JsonDeserializet<returnData>();
            medRecheckLogClasses = returnData.Data.ObjToListClass<MedRecheckLogClass>();
        }
        private void Function_異常通知_覆盤錯誤_庫存異動(string 藥碼, string 藥名 ,int 校正庫存值)
        {
            List<string> list_效期 = new List<string>();
            List<string> list_批號 = new List<string>();
            List<string> list_異動量 = new List<string>();
            List<object[]> list_交易紀錄_Add = new List<object[]>();

            string 備註 = "";
            Function_從SQL取得儲位到雲端資料(藥碼);
            int 庫存量 = Function_從雲端資料取得庫存(藥碼);
            int 盤點量 = 校正庫存值;
            int 異動後結存量 = 校正庫存值;
            int 異動量 = 異動後結存量 - 庫存量;

            if (庫存量 == 0 || 異動量 > 0)
            {
                List<string> 儲位_TYPE = new List<string>();
                List<object> 儲位 = new List<object>();
                this.Function_從雲端資料取得儲位(this.Function_藥品碼檢查(藥碼), ref 儲位_TYPE, ref 儲位);
                Funnction_交易記錄查詢_取得指定藥碼批號期效期(藥碼, ref list_效期, ref list_批號);
                if (list_效期.Count == 0)
                {
                    list_效期.Add("2050/01/01");
                    list_批號.Add("自動補足");
                }
                備註 += $"[效期]:{list_效期[0]},[批號]:{list_批號[0]}";

                if (儲位.Count == 0)
                {
                    return;
                }
                object device = Function_庫存異動至雲端資料(儲位[0], 儲位_TYPE[0], list_效期[0], list_批號[0], 異動量.ToString(), true);
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.GUID = Guid.NewGuid().ToString();
                transactionsClass.動作 = enum_交易記錄查詢動作.盤存盈虧.GetEnumName();
                transactionsClass.藥品碼 = 藥碼;
                transactionsClass.藥品名稱 = 藥名;
                transactionsClass.庫存量 = 庫存量.ToString();
                transactionsClass.交易量 = 異動量.ToString();
                transactionsClass.結存量 = 盤點量.ToString();
                transactionsClass.操作人 = 登入者名稱;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.備註 = 備註;
                object[] trading_value = transactionsClass.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
                list_交易紀錄_Add.Add(trading_value);

            }
            else
            {
                List<object[]> list_儲位資訊 = Function_取得異動儲位資訊從雲端資料(藥碼, 異動量);
                if (list_儲位資訊.Count == 0)
                {
                    return;
                }
                for (int k = 0; k < list_儲位資訊.Count; k++)
                {
                    string 效期 = list_儲位資訊[k][(int)enum_儲位資訊.效期].ObjectToString();
                    string 批號 = list_儲位資訊[k][(int)enum_儲位資訊.批號].ObjectToString();
                    備註 += $"[效期]:{效期},[批號]:{批號}";
                    if (k != list_儲位資訊.Count - 1) 備註 += "\n";
                    Function_庫存異動至雲端資料(list_儲位資訊[k], true);
                }

                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.GUID = Guid.NewGuid().ToString();
                transactionsClass.動作 = enum_交易記錄查詢動作.盤存盈虧.GetEnumName();
                transactionsClass.藥品碼 = 藥碼;
                transactionsClass.藥品名稱 = 藥名;
                transactionsClass.庫存量 = 庫存量.ToString();
                transactionsClass.交易量 = 異動量.ToString();
                transactionsClass.結存量 = 盤點量.ToString();
                transactionsClass.操作人 = 登入者名稱;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.備註 = 備註;
                object[] trading_value = transactionsClass.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();

                list_交易紀錄_Add.Add(trading_value);
            }
            if (list_交易紀錄_Add.Count > 0) sqL_DataGridView_交易記錄查詢.SQL_AddRows(list_交易紀錄_Add, false);
        }
        #endregion
    }
}
