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
using H_Pannel_lib;
using HIS_DB_Lib;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        public enum enum_收支作業_單品入庫_儲位搜尋 : int
        {
            GUID,
            IP,
            藥品碼,
            藥品名稱,
            中文名稱,
            儲位名稱,
            儲位型式,
            庫存,        
            Value,
        }
        #region Function


        #endregion
        private void Program_收支作業_Init()
        {
            string url = $"{dBConfigClass.Api_URL}/api/IncomeReasons/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            SQLUI.Table tables = json.JsonDeserializet<SQLUI.Table>();

            this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.Init();
            this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.RowDoubleClickEvent += SqL_DataGridView_收支作業_單品入庫_儲位搜尋_RowDoubleClickEvent;

            this.sqL_DataGridView_收支作業_入庫狀態.Init(this.sqL_DataGridView_取藥堆疊母資料);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnVisible(false, new enum_取藥堆疊母資料().GetEnumNames());
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnVisible(true, enum_取藥堆疊母資料.藥品碼, enum_取藥堆疊母資料.藥品名稱, enum_取藥堆疊母資料.總異動量, enum_取藥堆疊母資料.結存量, enum_取藥堆疊母資料.效期, enum_取藥堆疊母資料.狀態);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.庫存量);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.效期);
            this.sqL_DataGridView_收支作業_入庫狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.狀態);
            this.sqL_DataGridView_收支作業_入庫狀態.DataGridRefreshEvent += SqL_DataGridView_收支作業_入庫狀態_DataGridRefreshEvent;
            this.sqL_DataGridView_收支作業_入庫狀態.DataGridRowsChangeEvent += SqL_DataGridView_收支作業_入庫狀態_DataGridRowsChangeEvent;

            this.rJ_TextBox_收支作業_單品入庫_藥品碼.KeyPress += RJ_TextBox_收支作業_單品入庫_藥品碼_KeyPress;
            this.rJ_TextBox_收支作業_單品入庫_藥品名稱.KeyPress += RJ_TextBox_收支作業_單品入庫_藥品名稱_KeyPress;
            this.rJ_TextBox_收支作業_單品入庫_中文名稱.KeyPress += RJ_TextBox_收支作業_單品入庫_中文名稱_KeyPress;
            this.rJ_TextBox_收支作業_單品入庫_藥品條碼.KeyPress += RJ_TextBox_收支作業_單品入庫_藥品條碼_KeyPress;

            this.plC_RJ_Button_收支作業_單品入庫_顯示所有儲位.MouseDownEvent += PlC_RJ_Button_收支作業_單品入庫_顯示所有儲位_MouseDownEvent;
            this.plC_RJ_Button_收支作業_單品入庫_藥品碼搜尋.MouseDownEvent += PlC_RJ_Button_收支作業_單品入庫_藥品碼搜尋_MouseDownEvent;
            this.plC_RJ_Button_收支作業_單品入庫_藥品名稱搜尋.MouseDownEvent += PlC_RJ_Button_收支作業_單品入庫_藥品名稱搜尋_MouseDownEvent;
            this.plC_RJ_Button_收支作業_單品入庫_中文名稱搜尋.MouseDownEvent += PlC_RJ_Button_收支作業_單品入庫_中文名稱搜尋_MouseDownEvent;
            this.plC_RJ_Button_收支作業_單品入庫_藥品條碼輸入.MouseDownEvent += PlC_RJ_Button_收支作業_單品入庫_藥品條碼輸入_MouseDownEvent;

            this.plC_RJ_Button_收支作業_選擇儲位.MouseDownEvent += PlC_RJ_Button_收支作業_選擇儲位_MouseDownEvent;
            this.plC_RJ_Button_收支作業_入庫狀態_清除所有資料.MouseDownEvent += PlC_RJ_Button_收支作業_入庫狀態_清除所有資料_MouseDownEvent;
            this.plC_RJ_Button_收支作業_入庫狀態_清除選取資料.MouseDownEvent += PlC_RJ_Button_收支作業_入庫狀態_清除選取資料_MouseDownEvent;
            this.plC_RJ_Button_收支作業_入庫狀態_選取資料強制入賬.MouseDownEvent += PlC_RJ_Button_收支作業_入庫狀態_選取資料強制入賬_MouseDownEvent;


            this.plC_RJ_Button_收支作業_入庫.MouseDownEvent += PlC_RJ_Button_收支作業_入庫_MouseDownEvent;
            this.plC_RJ_Button_收支作業_出庫.MouseDownEvent += PlC_RJ_Button_收支作業_出庫_MouseDownEvent;
            this.plC_RJ_Button_收支作業_調入.MouseDownEvent += PlC_RJ_Button_收支作業_調入_MouseDownEvent;
            this.plC_RJ_Button_收支作業_調出.MouseDownEvent += PlC_RJ_Button_收支作業_調出_MouseDownEvent;

            this.plC_RJ_Button_收支作業_設定.MouseDownEvent += PlC_RJ_Button_收支作業_設定_MouseDownEvent;
            

            this.plC_UI_Init.Add_Method(this.sub_Program_收支作業);
        }

     

        private bool flag_Program_收支作業_換頁 = false;
        private bool flag_Program_收支作業_換頁離開 = false;
        private void sub_Program_收支作業()
        {
            if (this.plC_ScreenPage_Main.PageText == "收支作業")
            {
                string bacode01 = Function_ReadBacodeScanner01();
                //string bacode02 = Function_ReadBacodeScanner02();
                if (bacode01.StringIsEmpty() == false)
                {
                    this.Invoke(new Action(delegate 
                    {
                        this.rJ_TextBox_收支作業_單品入庫_藥品條碼.Texts = bacode01;
                        PlC_RJ_Button_收支作業_單品入庫_藥品條碼輸入_MouseDownEvent(null);
                    }));
                 
                }
                //else if (bacode02.StringIsEmpty() == false)
                //{
                //    this.Invoke(new Action(delegate
                //    {
                //        this.rJ_TextBox_收支作業_單品入庫_藥品條碼.Texts = bacode02;
                //        PlC_RJ_Button_收支作業_單品入庫_藥品條碼輸入_MouseDownEvent(null);
                //    }));
                //}

                if (this.plC_RJ_Button_收支作業_入庫.Bool)
                {
                    this.plC_RJ_Button_收支作業_入庫.BorderSize = 5;
                }
                if (flag_Program_收支作業_換頁)
                {
                    Function_從SQL取得儲位到本地資料();
                    Function_從SQL取得儲位到雲端資料();
                    this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
                    flag_Program_收支作業_換頁 = false;
                }
            }
            else
            {
                if(flag_Program_收支作業_換頁 == false)
                {
                    this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
                }
                flag_Program_收支作業_換頁 = true;
            }
            this.sub_Program_收支作業_單品入庫_狀態更新();
        }

        
        #region PLC_收支作業_單品入庫_狀態更新
        PLC_Device PLC_Device_收支作業_單品入庫_狀態更新 = new PLC_Device("");
        PLC_Device PLC_Device_收支作業_單品入庫_狀態更新_OK = new PLC_Device("");
        MyTimer MyTimer_收支作業_單品入庫_狀態更新_刷新時間 = new MyTimer();
        int cnt_Program_收支作業_單品入庫_狀態更新 = 65534;
        void sub_Program_收支作業_單品入庫_狀態更新()
        {
            if (this.plC_ScreenPage_Main.PageText == "收支作業")
            {
                PLC_Device_收支作業_單品入庫_狀態更新.Bool = true;
            }
            else
            {
                PLC_Device_收支作業_單品入庫_狀態更新.Bool = false;
            }
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 65534)
            {
                PLC_Device_收支作業_單品入庫_狀態更新.SetComment("PLC_收支作業_單品入庫_狀態更新");
                PLC_Device_收支作業_單品入庫_狀態更新_OK.SetComment("PLC_收支作業_單品入庫_狀態更新_OK");
                PLC_Device_收支作業_單品入庫_狀態更新.Bool = false;
                cnt_Program_收支作業_單品入庫_狀態更新 = 65535;
            }
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 65535) cnt_Program_收支作業_單品入庫_狀態更新 = 1;
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 1) cnt_Program_收支作業_單品入庫_狀態更新_檢查按下(ref cnt_Program_收支作業_單品入庫_狀態更新);
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 2) cnt_Program_收支作業_單品入庫_狀態更新_初始化(ref cnt_Program_收支作業_單品入庫_狀態更新);
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 3) cnt_Program_收支作業_單品入庫_狀態更新_檢查雙人覆核(ref cnt_Program_收支作業_單品入庫_狀態更新);
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 4) cnt_Program_收支作業_單品入庫_狀態更新_檢查盲盤作業(ref cnt_Program_收支作業_單品入庫_狀態更新);
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 5) cnt_Program_收支作業_單品入庫_狀態更新_檢查複盤作業(ref cnt_Program_收支作業_單品入庫_狀態更新);
            if (cnt_Program_收支作業_單品入庫_狀態更新 == 6) cnt_Program_收支作業_單品入庫_狀態更新 = 65500;
            if (cnt_Program_收支作業_單品入庫_狀態更新 > 1) cnt_Program_收支作業_單品入庫_狀態更新_檢查放開(ref cnt_Program_收支作業_單品入庫_狀態更新);

            if (cnt_Program_收支作業_單品入庫_狀態更新 == 65500)
            {
                PLC_Device_收支作業_單品入庫_狀態更新.Bool = false;
                PLC_Device_收支作業_單品入庫_狀態更新_OK.Bool = false;
                cnt_Program_收支作業_單品入庫_狀態更新 = 65535;
            }
        }
        void cnt_Program_收支作業_單品入庫_狀態更新_檢查按下(ref int cnt)
        {
            if (PLC_Device_收支作業_單品入庫_狀態更新.Bool) cnt++;
        }
        void cnt_Program_收支作業_單品入庫_狀態更新_檢查放開(ref int cnt)
        {
            if (!PLC_Device_收支作業_單品入庫_狀態更新.Bool) cnt = 65500;
        }
        void cnt_Program_收支作業_單品入庫_狀態更新_初始化(ref int cnt)
        {
            MyTimer_收支作業_單品入庫_狀態更新_刷新時間.StartTickTime(200);
            if (MyTimer_收支作業_單品入庫_狀態更新_刷新時間.IsTimeOut())
            {
                List<object[]> list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
                List<object[]> list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得子資料();
                List<object[]> list_取藥堆疊子資料_buf = new List<object[]>();
                List<object[]> list_取藥堆疊子資料_replace = new List<object[]>();
                list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, this.textBox_工程模式_領藥台_名稱.Text);
                string GUID = "";
                for(int i = 0; i < list_取藥堆疊母資料.Count; i++)
                {
                    GUID = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                    if (list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.作業完成.GetEnumName())
                    {
                        list_取藥堆疊子資料_buf = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, GUID);
                        foreach(object[] value in list_取藥堆疊子資料_buf)
                        {
                            value[(int)enum_取藥堆疊子資料.調劑結束] = true.ToString();
                            list_取藥堆疊子資料_replace.Add(value);
                        }
                    }
                }
                this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊子資料_replace, false);
                this.sqL_DataGridView_收支作業_入庫狀態.RefreshGrid(list_取藥堆疊母資料);
            }
 

            cnt++;
        }
        void cnt_Program_收支作業_單品入庫_狀態更新_檢查雙人覆核(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                Application.DoEvents();
                Dialog_使用者登入 dialog_使用者登入 = new Dialog_使用者登入(登入者ID, 藥名, this.sqL_DataGridView_人員資料, this.rfiD_FX600_UI);
    

                if (dialog_使用者登入.ShowDialog() != DialogResult.Yes)
                {
                    list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料[i]);
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                    continue;
                }
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.雙人覆核, false);
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n覆核:{dialog_使用者登入.UserName}";
                list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
            }
            if (list_取藥堆疊母資料_replace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
                cnt = 1;
            }
            if (list_取藥堆疊母資料_delete.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                cnt = 1;
            }
            if (cnt == 1) return;
            cnt++;

        }
        void cnt_Program_收支作業_單品入庫_狀態更新_檢查盲盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                int retry = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                int 總異動量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToInt32();
                int 結存量 = 0;

                voice.SpeakOnTask("請輸入盲盤數量");
                while (true)
                {
                    //if (try_error == 1)
                    //{
                    //    Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                    //    if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                    //    {
                    //        try_error = 0;
                    //    }
                    //    else
                    //    {
                    //        try_error++;
                    //    }
                    //    continue;
                    //}
                    //if (try_error == 2)
                    //{
                    //    Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇($"{dBConfigClass.Api_URL}/api/IncomeReasons", dBConfigClass.Name);
                    //    dialog_收支原因選擇.Title = $"盲盤數量錯誤({結存量}) 選擇原因";
                    //    dialog_收支原因選擇.ShowDialog();
                    //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n盲盤錯誤原因:{dialog_收支原因選擇.Value}";
                    //    break;
                    //}

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(盲盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;

                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        break;
                    }
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    int 庫存量 = Function_從SQL取得庫存(藥碼);
                    結存量 = 庫存量 + 總異動量;
                    if (結存量.ToString() == dialog_NumPannel.Value.ToString()) break;
                    voice.SpeakOnTask("盲盤數量錯誤");
                    if (retry == 0)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("請再次覆盤", 2000);
                        dialog_錯誤提示.ShowDialog();
                    }
                    if (retry == 1)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"異常紀錄,異常理論值 : {dialog_NumPannel.Value}", 2000);
                        dialog_錯誤提示.ShowDialog();
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = "覆盤錯誤";
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    try_error++;
                    retry++;

                }

                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
            }
            if (list_取藥堆疊母資料_replace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
                cnt = 1;
                return;
            }
            cnt++;

        }
        void cnt_Program_收支作業_單品入庫_狀態更新_檢查複盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                string 結存量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                voice.SpeakOnTask("請輸入複盤數量");
                while (true)
                {
                    if (try_error == 1)
                    {
                        Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                        if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                        {
                            try_error = 0;
                        }
                        else
                        {
                            try_error++;
                        }
                        continue;
                    }
                    if (try_error == 2)
                    {
                        Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇($"{dBConfigClass.Api_URL}/api/IncomeReasons", dBConfigClass.Name);
                        dialog_收支原因選擇.Title = $"複盤數量錯誤({結存量}) 選擇原因";
                        dialog_收支原因選擇.ShowDialog();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n複盤錯誤原因:{dialog_收支原因選擇.Value}";
                        break;
                    }

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(明盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);

                    dialog_NumPannel.X_Visible = true;

                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        break;
                    }
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    if (結存量 == dialog_NumPannel.Value.ToString()) break;
                    voice.SpeakOnTask("複盤數量錯誤");
                    try_error++;

                }

                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
            }
            if (list_取藥堆疊母資料_replace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
                cnt = 1;
                return;
            }
            cnt++;

        }


        #endregion
        #region Function
        private void Function_收支作業_單品入庫_儲位搜尋_Refresh()
        {

        }
        #endregion
        #region Event
        private void PlC_RJ_Button_收支作業_設定_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_收支原因設定 dialog_收支原因設定 = new Dialog_收支原因設定($"{dBConfigClass.Api_URL}/api/IncomeReasons", dBConfigClass.Name);
            dialog_收支原因設定.ShowDialog();
        }
        private void RJ_TextBox_收支作業_單品入庫_中文名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_收支作業_單品入庫_中文名稱搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_收支作業_單品入庫_藥品名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_收支作業_單品入庫_藥品名稱搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_收支作業_單品入庫_藥品碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_收支作業_單品入庫_藥品碼搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_收支作業_單品入庫_藥品條碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_收支作業_單品入庫_藥品條碼輸入_MouseDownEvent(null);
            }
        }
        private void SqL_DataGridView_收支作業_單品入庫_儲位搜尋_RowDoubleClickEvent(object[] RowValue)
        {
            PlC_RJ_Button_收支作業_選擇儲位_MouseDownEvent(null);
        }
        private void PlC_RJ_Button_收支作業_單品入庫_藥品條碼輸入_MouseDownEvent(MouseEventArgs mevent)
        {
            string BarCode = this.rJ_TextBox_收支作業_單品入庫_藥品條碼.Texts;
            List<medClass> medClasses = this.Function_搜尋Barcode(BarCode);
            if(medClasses.Count == 0)
            {
                MyMessageBox.ShowDialog("找無此國際條碼!");
                return;
            }
            List<object> list_儲位資訊 = this.Function_從SQL取得儲位到本地資料(medClasses[0].藥品碼);
            if (list_儲位資訊.Count == 0)
            {
                MyMessageBox.ShowDialog($"藥碼 : {medClasses[0].藥品碼}\n藥名 : {medClasses[0].藥品名稱}\n找無此儲位資訊!");
                return;
            }
            List<Device> devices = new List<Device>();
            for(int i = 0; i < list_儲位資訊.Count; i++)
            {
                devices.Add((Device)list_儲位資訊[i]);
            }
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Code.StringIsEmpty()) continue;
                object[] value = new object[new enum_收支作業_單品入庫_儲位搜尋().GetLength()];
                value[(int)enum_收支作業_單品入庫_儲位搜尋.GUID] = devices[i].GUID;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.IP] = devices[i].IP;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品碼] = devices[i].Code;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品名稱] = devices[i].Name;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.中文名稱] = devices[i].ChineseName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位名稱] = devices[i].StorageName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位型式] = devices[i].DeviceType.GetEnumName();
                value[(int)enum_收支作業_單品入庫_儲位搜尋.庫存] = devices[i].Inventory;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.Value] = devices[i];
                list_value.Add(value);
            }
            this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.RefreshGrid(list_value);
         
            if(list_value.Count == 1)
            {
                this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.SetSelectRow(list_value[0]);
                PlC_RJ_Button_收支作業_選擇儲位_MouseDownEvent(null);
            }
        }
        private void PlC_RJ_Button_收支作業_單品入庫_顯示所有儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            List<Device> devices = this.Function_從SQL取得所有儲位();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Code.StringIsEmpty()) continue;
                object[] value = new object[new enum_收支作業_單品入庫_儲位搜尋().GetLength()];
                value[(int)enum_收支作業_單品入庫_儲位搜尋.GUID] = devices[i].GUID;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.IP] = devices[i].IP;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品碼] = devices[i].Code;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品名稱] = devices[i].Name;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.中文名稱] = devices[i].ChineseName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位名稱] = devices[i].StorageName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位型式] = devices[i].DeviceType.GetEnumName();
                value[(int)enum_收支作業_單品入庫_儲位搜尋.庫存] = devices[i].Inventory;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.Value] = devices[i];
                list_value.Add(value);
            }
 
            this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.RefreshGrid(list_value);

        }
        private void PlC_RJ_Button_收支作業_單品入庫_中文名稱搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_收支作業_單品入庫_中文名稱.Texts.StringIsEmpty()) return;
            List<Device> devices = this.Function_從SQL取得所有儲位();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Code.StringIsEmpty()) continue;
                object[] value = new object[new enum_收支作業_單品入庫_儲位搜尋().GetLength()];
                value[(int)enum_收支作業_單品入庫_儲位搜尋.GUID] = devices[i].GUID;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.IP] = devices[i].IP;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品碼] = devices[i].Code;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品名稱] = devices[i].Name;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.中文名稱] = devices[i].ChineseName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位名稱] = devices[i].StorageName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位型式] = devices[i].DeviceType.GetEnumName();
                value[(int)enum_收支作業_單品入庫_儲位搜尋.庫存] = devices[i].Inventory;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.Value] = devices[i];
                list_value.Add(value);
            }       
            list_value = list_value.GetRowsByLike((int)enum_收支作業_單品入庫_儲位搜尋.中文名稱, rJ_TextBox_收支作業_單品入庫_中文名稱.Texts);
    
            this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_收支作業_單品入庫_藥品名稱搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_收支作業_單品入庫_藥品名稱.Texts.StringIsEmpty()) return;
            List<Device> devices = this.Function_從SQL取得所有儲位();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Code.StringIsEmpty()) continue;
                object[] value = new object[new enum_收支作業_單品入庫_儲位搜尋().GetLength()];
                value[(int)enum_收支作業_單品入庫_儲位搜尋.GUID] = devices[i].GUID;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.IP] = devices[i].IP;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品碼] = devices[i].Code;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品名稱] = devices[i].Name;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.中文名稱] = devices[i].ChineseName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位名稱] = devices[i].StorageName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位型式] = devices[i].DeviceType.GetEnumName();
                value[(int)enum_收支作業_單品入庫_儲位搜尋.庫存] = devices[i].Inventory;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.Value] = devices[i];
                list_value.Add(value);
            }
     
            list_value = list_value.GetRowsByLike((int)enum_收支作業_單品入庫_儲位搜尋.藥品名稱, rJ_TextBox_收支作業_單品入庫_藥品名稱.Texts);
        
            this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_收支作業_單品入庫_藥品碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_收支作業_單品入庫_藥品碼.Texts.StringIsEmpty()) return;
            List<Device> devices = this.Function_從SQL取得所有儲位();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Code.StringIsEmpty()) continue;
                object[] value = new object[new enum_收支作業_單品入庫_儲位搜尋().GetLength()];
                value[(int)enum_收支作業_單品入庫_儲位搜尋.GUID] = devices[i].GUID;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.IP] = devices[i].IP;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品碼] = devices[i].Code;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.藥品名稱] = devices[i].Name;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.中文名稱] = devices[i].ChineseName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位名稱] = devices[i].StorageName;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.儲位型式] = devices[i].DeviceType.GetEnumName();
                value[(int)enum_收支作業_單品入庫_儲位搜尋.庫存] = devices[i].Inventory;
                value[(int)enum_收支作業_單品入庫_儲位搜尋.Value] = devices[i];
                list_value.Add(value);
            }
      
            list_value = list_value.GetRowsByLike((int)enum_收支作業_單品入庫_儲位搜尋.藥品碼, rJ_TextBox_收支作業_單品入庫_藥品碼.Texts);
  
            this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.RefreshGrid(list_value);
        }
        private void SqL_DataGridView_收支作業_儲位資料_RowEnterEvent(object[] RowValue)
        {
          
        }
        private void PlC_RJ_Button_收支作業_選擇儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            object[] value = sqL_DataGridView_收支作業_單品入庫_儲位搜尋.GetRowValues();
            if (value == null)
            {
                MyMessageBox.ShowDialog("未選擇儲位!");
                return;
            }
            if (!plC_RJ_Button_收支作業_入庫.Bool &&
                !plC_RJ_Button_收支作業_出庫.Bool &&
                !plC_RJ_Button_收支作業_調入.Bool &&
                !plC_RJ_Button_收支作業_調出.Bool )
            {
                MyMessageBox.ShowDialog("請選擇作業方式!");
                return;
            }
            object device_object = value[(int)enum_收支作業_單品入庫_儲位搜尋.Value];
            if (!(device_object is Device)) return;
            Device device = device_object as Device;

            string 輸入效期 = "";
            string 輸入批號 = "";
            string 輸入數量 = "";
            string 收支原因 = "";
            if(plC_RJ_Button_收支作業_入庫.Bool || plC_RJ_Button_收支作業_調入.Bool)
            {
                Dialog_DateTime dialog_DateTime = new Dialog_DateTime(DateTime.Now);
                if (dialog_DateTime.ShowDialog() == DialogResult.Yes)
                {
                    輸入效期 = dialog_DateTime.Value.ToDateString();
                }
                else
                {
                    return;
                }

                if (device.取得庫存(輸入效期) == -1)
                {
                    Dialog_輸入批號 dialog_輸入批號 = new Dialog_輸入批號();
                    if (dialog_輸入批號.ShowDialog() == DialogResult.Yes)
                    {
                        輸入批號 = dialog_輸入批號.Value;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    輸入批號 = device.取得批號(輸入效期);
                }
            }
            if (plC_RJ_Button_收支作業_調入.Bool || plC_RJ_Button_收支作業_調出.Bool)
            {
                Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇($"{dBConfigClass.Api_URL}/api/IncomeReasons", dBConfigClass.Name);
                dialog_收支原因選擇.ShowDialog();
                收支原因 = dialog_收支原因選擇.Value;
            }
                

            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
            {
                輸入數量 = dialog_NumPannel.Value.ToString();
            }
            else
            {
                return;
            }
            if (plC_RJ_Button_收支作業_出庫.Bool || plC_RJ_Button_收支作業_調出.Bool)
            {
                輸入數量 = (dialog_NumPannel.Value * -1).ToString();
            }
            Color color = 登入者顏色.ToColor();
            if (color == Color.Black) 登入者顏色 = Color.White.ToColorString();
            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.textBox_工程模式_領藥台_名稱.Text;
            enum_交易記錄查詢動作 動作 = new enum_交易記錄查詢動作();
            if (plC_RJ_Button_收支作業_入庫.Bool) 動作 = enum_交易記錄查詢動作.入庫作業;
            if (plC_RJ_Button_收支作業_出庫.Bool) 動作 = enum_交易記錄查詢動作.出庫作業;
            if (plC_RJ_Button_收支作業_調入.Bool) 動作 = enum_交易記錄查詢動作.調入作業;
            if (plC_RJ_Button_收支作業_調出.Bool) 動作 = enum_交易記錄查詢動作.調出作業;
            string 藥品碼 = device.Code;
            string 藥品名稱 = device.Name;
            string 藥袋序號 = "";
            string 單位 = device.Package;
            string 病歷號 = "";
            string 病人姓名 = "";
            string 床號 = "";
            string 開方時間 = DateTime.Now.ToDateTimeString_6();
            string IP = device.IP;
            string 操作人 = 登入者名稱;
            string 顏色 = 登入者顏色;
            string 藥師證字號 = 登入者藥師證字號;
            int 總異動量 = 輸入數量.StringToInt32();
            string 效期 = 輸入效期;
            string 批號 = 輸入批號;
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作;         
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.收支原因 = 收支原因;
            takeMedicineStackClass.操作人 = 操作人;
            takeMedicineStackClass.藥師證字號 = 藥師證字號;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.批號 = 批號;
            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            //this.sqL_DataGridView_收支作業_單品入庫_儲位搜尋.ClearGrid();
           
          
        }

        private void SqL_DataGridView_收支作業_入庫狀態_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
            RowsList.Sort(new Icp_取藥堆疊母資料_index排序());
        }
        private void SqL_DataGridView_收支作業_入庫狀態_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows[i].Cells[(int)enum_取藥堆疊母資料.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_收支作業_入庫狀態.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
   
        private void PlC_RJ_Button_收支作業_入庫狀態_清除選取資料_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_收支作業_入庫狀態.Get_All_Select_RowsValues();
            for(int i = 0; i< list_value.Count; i++)
            {
                Function_取藥堆疊資料_刪除母資料(list_value[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
            }

        }
        private void PlC_RJ_Button_收支作業_入庫狀態_清除所有資料_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
        }
        private void PlC_RJ_Button_收支作業_入庫狀態_選取資料強制入賬_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_收支作業_入庫狀態.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            for (int i = 0; i < list_value.Count; i++)
            {
                string 藥品碼 = list_value[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 調劑台名稱 = this.textBox_工程模式_領藥台_名稱.Text;

                Function_取藥堆疊子資料_設定配藥完成ByCode(調劑台名稱, 藥品碼);
            }
        }
        private void PlC_RJ_Button_收支作業_調入_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_收支作業_入庫.Bool = false;
            plC_RJ_Button_收支作業_出庫.Bool = false;
            plC_RJ_Button_收支作業_調入.Bool = true;
            plC_RJ_Button_收支作業_調出.Bool = false;
        }
        private void PlC_RJ_Button_收支作業_調出_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_收支作業_入庫.Bool = false;
            plC_RJ_Button_收支作業_出庫.Bool = false;
            plC_RJ_Button_收支作業_調入.Bool = false;
            plC_RJ_Button_收支作業_調出.Bool = true;
        }
        private void PlC_RJ_Button_收支作業_入庫_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_收支作業_入庫.Bool = true;
            plC_RJ_Button_收支作業_出庫.Bool = false;
            plC_RJ_Button_收支作業_調入.Bool = false;
            plC_RJ_Button_收支作業_調出.Bool = false;
        }
        private void PlC_RJ_Button_收支作業_出庫_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_收支作業_入庫.Bool = false;
            plC_RJ_Button_收支作業_出庫.Bool = true;
            plC_RJ_Button_收支作業_調入.Bool = false;
            plC_RJ_Button_收支作業_調出.Bool = false;
        }
        #endregion
    }
}
