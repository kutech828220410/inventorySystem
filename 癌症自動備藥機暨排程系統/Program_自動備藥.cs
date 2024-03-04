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
using H_Pannel_lib;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        MyThread myThread_開始備藥;
        public PLC_Device PLC_Device_自動備藥_已登入 = new PLC_Device();
        static public sessionClass SessionClass_自動備藥;
        public sessionClass sessionClass_自動備藥
        {
            get
            {
                return SessionClass_自動備藥;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    plC_RJ_Button_自動備藥_手動選擇備藥.Enabled = (value != null);
                }));
             
                SessionClass_自動備藥 = value;
            }
        }
        private void Program_自動備藥_Init()
        {
            plC_RJ_Button_自動備藥_登入.MouseClickEvent += PlC_RJ_Button_自動備藥_登入_MouseClickEvent;
            plC_RJ_Button_開始備藥.MouseDownEvent += PlC_RJ_Button_開始備藥_MouseDownEvent;
            plC_RJ_Button_自動備藥_手動選擇備藥.MouseDownEvent += PlC_RJ_Button_自動備藥_手動選擇備藥_MouseDownEvent;
            textBox_自動備藥_帳號.KeyPress += TextBox_自動備藥_帳號_KeyPress;
            textBox_自動備藥_密碼.KeyPress += TextBox_自動備藥_密碼_KeyPress;
            this.uc_備藥通知處方.Init();


            myThread_開始備藥 = new MyThread(this.FindForm());
            myThread_開始備藥.AutoRun(true);
            myThread_開始備藥.SetSleepTime(1);
            myThread_開始備藥.Add_Method(Program_自動備藥);
            myThread_開始備藥.Trigger();

            //plC_UI_Init.Add_Method(Program_自動備藥);
        }

     

        private void Program_自動備藥()
        {
            sub_Program_自動備藥_開始備藥();
            sub_Program_自動備藥_RFID登入();
        }
        private void Function_出料一次(string IP)
        {
            出料一次_IP = IP;
            int cnt = 0;
            while (true)
            {
                if (cnt == 0)
                {
                    if (PLC_Device_出料一次.Bool) return;
                    PLC_Device_出料一次.Bool = true;
                    cnt++;
                }
                if (cnt == 0)
                {
                    if (PLC_Device_出料一次.Bool) return;
                    cnt++;
                }
                System.Threading.Thread.Sleep(1);
            }
        }
        #region PLC_自動備藥_RFID登入
        PLC_Device PLC_Device_自動備藥_RFID登入 = new PLC_Device("");
        int cnt_Program_自動備藥_RFID登入 = 65534;
        void sub_Program_自動備藥_RFID登入()
        {
            if (this.plC_ScreenPage_main.PageText == "自動備藥")
            {
                PLC_Device_自動備藥_RFID登入.Bool = true;
            }
            else
            {
                PLC_Device_自動備藥_RFID登入.Bool = false;
            }
            if (cnt_Program_自動備藥_RFID登入 == 65534)
            {
                PLC_Device_自動備藥_RFID登入.SetComment("PLC_自動備藥_RFID登入");
                PLC_Device_自動備藥_RFID登入.Bool = false;
                cnt_Program_自動備藥_RFID登入 = 65535;
            }
            if (cnt_Program_自動備藥_RFID登入 == 65535) cnt_Program_自動備藥_RFID登入 = 1;
            if (cnt_Program_自動備藥_RFID登入 == 1) cnt_Program_自動備藥_RFID登入_檢查按下(ref cnt_Program_自動備藥_RFID登入);
            if (cnt_Program_自動備藥_RFID登入 == 2) cnt_Program_自動備藥_RFID登入_初始化(ref cnt_Program_自動備藥_RFID登入);
            if (cnt_Program_自動備藥_RFID登入 == 3) cnt_Program_自動備藥_RFID登入_檢查權限登入(ref cnt_Program_自動備藥_RFID登入);
            if (cnt_Program_自動備藥_RFID登入 == 4) cnt_Program_自動備藥_RFID登入_外部設備資料(ref cnt_Program_自動備藥_RFID登入);
            if (cnt_Program_自動備藥_RFID登入 == 5) cnt_Program_自動備藥_RFID登入_開始登入(ref cnt_Program_自動備藥_RFID登入);
            if (cnt_Program_自動備藥_RFID登入 == 6) cnt_Program_自動備藥_RFID登入_等待登入完成(ref cnt_Program_自動備藥_RFID登入);
            if (cnt_Program_自動備藥_RFID登入 == 7) cnt_Program_自動備藥_RFID登入 = 65500;
            if (cnt_Program_自動備藥_RFID登入 > 1) cnt_Program_自動備藥_RFID登入_檢查放開(ref cnt_Program_自動備藥_RFID登入);

            if (cnt_Program_自動備藥_RFID登入 == 65500)
            {
                PLC_Device_自動備藥_RFID登入.Bool = false;
                cnt_Program_自動備藥_RFID登入 = 65535;
            }
        }
        void cnt_Program_自動備藥_RFID登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_自動備藥_RFID登入.Bool) cnt++;
        }
        void cnt_Program_自動備藥_RFID登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_自動備藥_RFID登入.Bool) cnt = 65500;
        }
        void cnt_Program_自動備藥_RFID登入_初始化(ref int cnt)
        {

            cnt++;
        }
        void cnt_Program_自動備藥_RFID登入_檢查權限登入(ref int cnt)
        {
            if (!this.PLC_Device_自動備藥_已登入.Bool)
            {
                cnt++;
                return;
            }
            else
            {
                cnt = 65500;
                return;
            }

        }
        void cnt_Program_自動備藥_RFID登入_外部設備資料(ref int cnt)
        {
            string RFID = "0";
            List<RFID_FX600lib.RFID_FX600_UI.RFID_Device> list_RFID = this.rfiD_FX600_UI.Get_RFID();

   
            if (list_RFID.Count != 0)
            {
                if (list_RFID[0].UID.StringToInt32() != 0)
                {
                    RFID = list_RFID[0].UID;
                }
            }
            if (RFID.StringToInt32() == 0 || RFID.StringIsEmpty())
            {
                cnt = 65500;
                return;
            }

            List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.卡號.GetEnumName(), RFID, false);
            if (list_人員資料.Count > 0)
            {
                this.Invoke(new Action(delegate
                {
                    this.textBox_自動備藥_帳號.Text = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                    this.textBox_自動備藥_密碼.Text = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                }));
                Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, list_人員資料[0][(int)enum_人員資料.姓名].ObjectToString(), "自動備藥");
            }
            else
            {
                MyMessageBox.ShowDialog(string.Format("查無此卡帳號! {0}", RFID));
                cnt = 65500;
                return;
            }

            cnt++;
        }
        void cnt_Program_自動備藥_RFID登入_開始登入(ref int cnt)
        {
            Function_自動備藥_登入();
            cnt++;
        }
        void cnt_Program_自動備藥_RFID登入_等待登入完成(ref int cnt)
        {
            cnt++;
        }
        #endregion
        #region PLC_自動備藥_開始備藥
        List<object[]> list_自動備藥_開始備藥_常溫 = new List<object[]>();
        List<object[]> list_自動備藥_開始備藥_冷藏 = new List<object[]>();
        int cnt_自動備藥_開始備藥_常溫_已完成 = 0;
        int cnt_自動備藥_開始備藥_冷藏_已完成 = 0;
        PLC_Device PLC_Device_自動備藥_開始備藥 = new PLC_Device("M30000");
        PLC_Device PLC_Device_自動備藥_開始備藥_OK = new PLC_Device("");
        MyTimer MyTimer_自動備藥_開始備藥_結束延遲 = new MyTimer();
        int cnt_Program_自動備藥_開始備藥 = 65534;
        void sub_Program_自動備藥_開始備藥()
        {
            if (myConfigClass.主機模式 == false) return;
            if (cnt_Program_自動備藥_開始備藥 == 65534)
            {
                this.MyTimer_自動備藥_開始備藥_結束延遲.StartTickTime(50);
                PLC_Device_自動備藥_開始備藥.SetComment("PLC_自動備藥_開始備藥");
                PLC_Device_自動備藥_開始備藥_OK.SetComment("PLC_自動備藥_開始備藥_OK");
                PLC_Device_自動備藥_開始備藥.Bool = false;
                cnt_Program_自動備藥_開始備藥 = 65535;
            }
            if (cnt_Program_自動備藥_開始備藥 == 65535) cnt_Program_自動備藥_開始備藥 = 1;
            if (cnt_Program_自動備藥_開始備藥 == 1) cnt_Program_自動備藥_開始備藥_檢查按下(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 2) cnt_Program_自動備藥_開始備藥_初始化(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 3) cnt_Program_自動備藥_開始備藥_藥盒從進出盒區傳送至常溫區(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 4) cnt_Program_自動備藥_開始備藥_等待藥盒從進出盒區傳送至常溫區完成(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 6) cnt_Program_自動備藥_開始備藥_常溫區_出料一次(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 7) cnt_Program_自動備藥_開始備藥_藥盒從常溫區傳送至冷藏區(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 8) cnt_Program_自動備藥_開始備藥_等待藥盒從常溫區傳送至冷藏區完成(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 9) cnt_Program_自動備藥_開始備藥_冷藏區_出料一次(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 10) cnt_Program_自動備藥_開始備藥_藥盒從冷藏區傳接至常溫區(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 11) cnt_Program_自動備藥_開始備藥_等待藥盒從冷藏區傳接至常溫區_已到傳接位置(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 12) cnt_Program_自動備藥_開始備藥_等待藥盒從冷藏區傳接至常溫區_已到結束待命位置(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 13) cnt_Program_自動備藥_開始備藥_藥盒從常溫區傳接至進出盒區(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 14) cnt_Program_自動備藥_開始備藥_等待藥盒從常溫區傳接至進出盒區完成(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 15) cnt_Program_自動備藥_開始備藥 = 65500;
            if (cnt_Program_自動備藥_開始備藥 > 1) cnt_Program_自動備藥_開始備藥_檢查放開(ref cnt_Program_自動備藥_開始備藥);

            if (cnt_Program_自動備藥_開始備藥 == 65500)
            {
                this.MyTimer_自動備藥_開始備藥_結束延遲.TickStop();
                this.MyTimer_自動備藥_開始備藥_結束延遲.StartTickTime(50);
                PLC_Device_自動備藥_開始備藥.Bool = false;
                PLC_Device_自動備藥_開始備藥_OK.Bool = false;

                PLC_Device_出料一次.Bool = false;
                PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool = false;
                PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool = false;
                PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool = false;
                PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool = false;
                this.Invoke(new Action(delegate
                {
                    panel_備藥狀態.Visible = false;
                }));

                Function_自動備藥_登出();
                cnt_Program_自動備藥_開始備藥 = 65535;
            }
        }
        void cnt_Program_自動備藥_開始備藥_檢查按下(ref int cnt)
        {
            if (PLC_Device_自動備藥_開始備藥.Bool) cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_檢查放開(ref int cnt)
        {
            if (!PLC_Device_自動備藥_開始備藥.Bool) cnt = 65500;
        }
        void cnt_Program_自動備藥_開始備藥_初始化(ref int cnt)
        {
            this.Invoke(new Action(delegate
            {
                panel_備藥狀態.Visible = true;
            }));
            cnt_自動備藥_開始備藥_常溫_已完成 = 0;
            cnt_自動備藥_開始備藥_冷藏_已完成 = 0;
            if (list_自動備藥_開始備藥_常溫.Count == 0 && list_自動備藥_開始備藥_冷藏.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("無藥品須備藥", 1500);
                dialog_AlarmForm.ShowDialog();
                cnt = 65500;
                return;
            }
            if (!PLC_IO_進出盒區_藥盒到位感應.Bool)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未置入藥盒", 1500);
                dialog_AlarmForm.ShowDialog();
                cnt = 65500;
                return;
            }
            if (PLC_IO_常溫區_藥盒左感應.Bool
            || PLC_IO_常溫區_藥盒中感應.Bool
            || PLC_IO_常溫區_藥盒右感應.Bool
            || PLC_IO_冷藏區_藥盒左感應.Bool
            || PLC_IO_冷藏區_藥盒中感應.Bool
            || PLC_IO_冷藏區_藥盒右感應.Bool)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("機器內有藥盒需排除", 1500);
                dialog_AlarmForm.ShowDialog();
                cnt = 65500;
                return;
            }
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態_常溫.Text = $"【常溫】({cnt_自動備藥_開始備藥_常溫_已完成}/{list_自動備藥_開始備藥_常溫.Count })";
                rJ_Lable_備藥狀態_冷藏.Text = $"【冷藏】({cnt_自動備藥_開始備藥_冷藏_已完成}/{list_自動備藥_開始備藥_冷藏.Count })";
                rJ_Lable_備藥狀態.Text = "---------------------";
            }));
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_藥盒從進出盒區傳送至常溫區(ref int cnt)
        {
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態_常溫.Text = $"【常溫】({cnt_自動備藥_開始備藥_常溫_已完成}/{list_自動備藥_開始備藥_常溫.Count })";
                rJ_Lable_備藥狀態_冷藏.Text = $"【冷藏】({cnt_自動備藥_開始備藥_冷藏_已完成}/{list_自動備藥_開始備藥_冷藏.Count })";
                rJ_Lable_備藥狀態.Text = "傳送入常溫區取藥";
            }));
            if (PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool) return;
            PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool = true;
            if (list_自動備藥_開始備藥_冷藏.Count != 0) PLC_Device_冷藏區輸送門開啟.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_等待藥盒從進出盒區傳送至常溫區完成(ref int cnt)
        {
            if (PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool) return;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_常溫區_出料一次(ref int cnt)
        {
            for (int i = 0; i < list_自動備藥_開始備藥_常溫.Count; i++)
            {
                this.Invoke(new Action(delegate
                {
                    rJ_Lable_備藥狀態_常溫.Text = $"【常溫】({cnt_自動備藥_開始備藥_常溫_已完成}/{list_自動備藥_開始備藥_常溫.Count })";
                    rJ_Lable_備藥狀態_冷藏.Text = $"【冷藏】({cnt_自動備藥_開始備藥_冷藏_已完成}/{list_自動備藥_開始備藥_冷藏.Count })";
                    rJ_Lable_備藥狀態.Text = "常溫區開始出料";
                }));
                string IP = list_自動備藥_開始備藥_常溫[i][(int)enum_儲位資訊.IP].ObjectToString();
                string 效期 = list_自動備藥_開始備藥_常溫[i][(int)enum_儲位資訊.效期].ObjectToString();
                string 批號 = list_自動備藥_開始備藥_常溫[i][(int)enum_儲位資訊.批號].ObjectToString();
                int 數量 = list_自動備藥_開始備藥_常溫[i][(int)enum_儲位資訊.異動量].StringToInt32();
                Storage storage = (Storage)list_自動備藥_開始備藥_常溫[i][(int)enum_儲位資訊.Value];
                int 原有庫存 = Main_Form.Function_從SQL取得庫存(storage.Code);
                if (數量 < 0) 數量 = 數量 * -1;
                for (int k = 0; k < 數量; k++)
                {
                    Function_出料一次(IP);
                }
                Function_庫存異動至本地資料(list_自動備藥_開始備藥_常溫[i], true);
               
                string url = $"{Main_Form.API_Server}/api/transactions/add";
                returnData returnData = new returnData();
                returnData.ServerName = "cheom";
                returnData.ServerType = "癌症備藥機";
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.動作 = enum_交易記錄查詢動作.入庫作業.GetEnumName();
                transactionsClass.藥品碼 = storage.Code;
                transactionsClass.藥品名稱 = storage.Name;
                transactionsClass.操作人 = this.登入者名稱;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.庫存量 = 原有庫存.ToString(); ;
                transactionsClass.交易量 = 數量.ToString();
                transactionsClass.結存量 = (原有庫存 + 數量).ToString();
                transactionsClass.備註 += $"[效期]:{效期},[批號]:{批號}";
                returnData.Data = transactionsClass;
                string json_in = returnData.JsonSerializationt();
                string json_out = Basic.Net.WEBApiPostJson(url, json_in);
                cnt_自動備藥_開始備藥_常溫_已完成++;
            }
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_藥盒從常溫區傳送至冷藏區(ref int cnt)
        {
            if(list_自動備藥_開始備藥_冷藏.Count == 0)
            {
                cnt++;
                return;
            }
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態_常溫.Text = $"【常溫】({cnt_自動備藥_開始備藥_常溫_已完成}/{list_自動備藥_開始備藥_常溫.Count })";
                rJ_Lable_備藥狀態_冷藏.Text = $"【冷藏】({cnt_自動備藥_開始備藥_冷藏_已完成}/{list_自動備藥_開始備藥_冷藏.Count })";
                rJ_Lable_備藥狀態.Text = "傳送入冷藏區取藥";
            }));
            if (PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool) return;
            PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_等待藥盒從常溫區傳送至冷藏區完成(ref int cnt)
        {
            if (list_自動備藥_開始備藥_冷藏.Count == 0)
            {
                cnt++;
                return;
            }
            if (PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool) return;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_冷藏區_出料一次(ref int cnt)
        {
            if (list_自動備藥_開始備藥_冷藏.Count == 0)
            {
                cnt++;
                return;
            }
            for (int i = 0; i < list_自動備藥_開始備藥_冷藏.Count; i++)
            {
                this.Invoke(new Action(delegate
                {
                    rJ_Lable_備藥狀態_常溫.Text = $"【常溫】({cnt_自動備藥_開始備藥_常溫_已完成}/{list_自動備藥_開始備藥_常溫.Count })";
                    rJ_Lable_備藥狀態_冷藏.Text = $"【冷藏】({cnt_自動備藥_開始備藥_冷藏_已完成}/{list_自動備藥_開始備藥_冷藏.Count })";
                    rJ_Lable_備藥狀態.Text = "冷藏區開始出料";
                }));
                string IP = list_自動備藥_開始備藥_冷藏[i][(int)enum_儲位資訊.IP].ObjectToString();
                string 效期 = list_自動備藥_開始備藥_冷藏[i][(int)enum_儲位資訊.效期].ObjectToString();
                string 批號 = list_自動備藥_開始備藥_冷藏[i][(int)enum_儲位資訊.批號].ObjectToString();
                int 數量 = list_自動備藥_開始備藥_冷藏[i][(int)enum_儲位資訊.異動量].StringToInt32();
                Storage storage = (Storage)list_自動備藥_開始備藥_冷藏[i][(int)enum_儲位資訊.Value];
                int 原有庫存 = Main_Form.Function_從SQL取得庫存(storage.Code);
                if (數量 < 0) 數量 = 數量 * -1;
                for (int k = 0; k < 數量; k++)
                {
                    Function_出料一次(IP);
                }
                Function_庫存異動至本地資料(list_自動備藥_開始備藥_冷藏[i], true);

                string url = $"{Main_Form.API_Server}/api/transactions/add";
                returnData returnData = new returnData();
                returnData.ServerName = "cheom";
                returnData.ServerType = "癌症備藥機";
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.動作 = enum_交易記錄查詢動作.入庫作業.GetEnumName();
                transactionsClass.藥品碼 = storage.Code;
                transactionsClass.藥品名稱 = storage.Name;
                transactionsClass.操作人 = this.登入者名稱;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.庫存量 = 原有庫存.ToString(); ;
                transactionsClass.交易量 = 數量.ToString();
                transactionsClass.結存量 = (原有庫存 + 數量).ToString();
                transactionsClass.備註 += $"[效期]:{效期},[批號]:{批號}";
                returnData.Data = transactionsClass;
                string json_in = returnData.JsonSerializationt();
                string json_out = Basic.Net.WEBApiPostJson(url, json_in);
                cnt_自動備藥_開始備藥_冷藏_已完成++;
            }
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_藥盒從冷藏區傳接至常溫區(ref int cnt)
        {
            if (list_自動備藥_開始備藥_冷藏.Count == 0)
            {
                cnt++;
                return;
            }
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態_常溫.Text = $"【常溫】({cnt_自動備藥_開始備藥_常溫_已完成}/{list_自動備藥_開始備藥_常溫.Count })";
                rJ_Lable_備藥狀態_冷藏.Text = $"【冷藏】({cnt_自動備藥_開始備藥_冷藏_已完成}/{list_自動備藥_開始備藥_冷藏.Count })";
                rJ_Lable_備藥狀態.Text = "傳送回常溫區出貨";
            }));
            if (PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool) return;
            PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool = true;
            PLC_Device_將藥盒從冷藏區傳接至常溫區_已到傳接位置.Bool = false;
            PLC_Device_將藥盒從冷藏區傳接至常溫區_已到結束待命位置.Bool = false;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_等待藥盒從冷藏區傳接至常溫區_已到傳接位置(ref int cnt)
        {
            if (list_自動備藥_開始備藥_冷藏.Count == 0)
            {
                cnt++;
                return;
            }
            if (!PLC_Device_將藥盒從冷藏區傳接至常溫區_已到傳接位置.Bool) return;
            if (PLC_Device_常溫區輸送門開啟.Bool) return;
            PLC_Device_常溫區輸送門開啟.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_等待藥盒從冷藏區傳接至常溫區_已到結束待命位置(ref int cnt)
        {
            if (list_自動備藥_開始備藥_冷藏.Count == 0)
            {
                cnt++;
                return;
            }
            if (!PLC_Device_將藥盒從冷藏區傳接至常溫區_已到結束待命位置.Bool) return;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_藥盒從常溫區傳接至進出盒區(ref int cnt)
        {
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態_常溫.Text = $"【常溫】({cnt_自動備藥_開始備藥_常溫_已完成}/{list_自動備藥_開始備藥_常溫.Count })";
                rJ_Lable_備藥狀態_冷藏.Text = $"【冷藏】({cnt_自動備藥_開始備藥_冷藏_已完成}/{list_自動備藥_開始備藥_冷藏.Count })";
                rJ_Lable_備藥狀態.Text = "傳送回出貨區";
            }));
            if (PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool) return;
            PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_等待藥盒從常溫區傳接至進出盒區完成(ref int cnt)
        {
            if (PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool) return;
            cnt++;
        }
        #endregion

        #region Function
        private void Function_自動備藥_登入()
        {
            string user = textBox_自動備藥_帳號.Text;
            string pwd = textBox_自動備藥_密碼.Text;
            this.Invoke(new Action(delegate
            {
                textBox_自動備藥_帳號.Text = "";
                textBox_自動備藥_密碼.Text = "";

            }));
 
            if (user.StringIsEmpty() == true)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("帳號空白", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            bool flag = true;
            string json_in = "";
            string json_result = "";
            returnData returnData = new returnData();
            sessionClass _sessionClass = new sessionClass();
            _sessionClass.ID = user.ToUpper();
            _sessionClass.Password = pwd.ToUpper();
            returnData.Data = _sessionClass;
            json_in = returnData.JsonSerializationt();
            json_result = Net.WEBApiPostJson(dBConfigClass.Login_URL, json_in);
            returnData = json_result.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("登入API呼叫異常,請檢查網路連結及設定", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            sessionClass_自動備藥 = returnData.Data.ObjToClass<sessionClass>();
            if (returnData.Code == 200)
            {
                this.Invoke(new Action(delegate
                {
                    this.plC_RJ_Button_自動備藥_登入.SetBackgroundColor(Color.DarkRed);         
                    this.plC_RJ_Button_自動備藥_登入.Texts = "登出";
                    this.rJ_Lable_自動備藥_登入狀態.TextColor = Color.White;
                    this.rJ_Lable_自動備藥_登入狀態.Text = $"[{sessionClass_自動備藥.Name}] 已登入";
                    this.rJ_Lable_自動備藥_登入狀態.BackgroundColor = Color.Green;
                    this.textBox_自動備藥_帳號.Enabled = false;
                    this.textBox_自動備藥_密碼.Enabled = false;
                    this.plC_RJ_Button_自動備藥_登入.Refresh();
                    Application.DoEvents();
                }));
                PLC_Device_自動備藥_已登入.Bool = true;
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"登入成功 {sessionClass_自動備藥.Name}", 1500, Color.Green);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            else
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"{returnData.Result}", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            
            }
        }
        private void Function_自動備藥_登出()
        {
            sessionClass_自動備藥 = null;
            this.Invoke(new Action(delegate
            {
                this.plC_RJ_Button_自動備藥_登入.SetBackgroundColor(Color.Black);
                this.plC_RJ_Button_自動備藥_登入.Texts = "登入";
                this.rJ_Lable_自動備藥_登入狀態.TextColor = Color.Black;
                this.rJ_Lable_自動備藥_登入狀態.Text = $"請登入系統...";
                this.rJ_Lable_自動備藥_登入狀態.BackgroundColor = Color.White;
                this.textBox_自動備藥_帳號.Enabled = true;
                this.textBox_自動備藥_密碼.Enabled = true;
                this.plC_RJ_Button_自動備藥_登入.Refresh();
                Application.DoEvents();
            }));
            PLC_Device_自動備藥_已登入.Bool = false;
        }
        #endregion
        #region Event
        private void TextBox_自動備藥_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ((char)Keys.Enter))
            {
                Function_自動備藥_登入();
            }
        }
        private void TextBox_自動備藥_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                if (textBox_自動備藥_帳號.Text.StringIsEmpty() == false)
                {
                    textBox_自動備藥_密碼.Focus();
                }
            }
           
        }
        private void PlC_RJ_Button_自動備藥_登入_MouseClickEvent(MouseEventArgs mevent)
        {
            if (plC_RJ_Button_自動備藥_登入.Text == "登入")
            {
                this.Function_自動備藥_登入();
            }
            else
            {
                this.Function_自動備藥_登出();
            }
        }
        private void PlC_RJ_Button_開始備藥_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.uc_備藥通知處方.GetSelectedRows();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string GUID = list_value[0][(int)enum_udnoectc.GUID].ObjectToString();
            this.Invoke(new Action(delegate 
            {
                Dialog_備藥清單 dialog_備藥清單 = new Dialog_備藥清單(GUID , 登入者名稱);
                dialog_備藥清單.ShowDialog();
            }));           
        }
        private void PlC_RJ_Button_自動備藥_手動選擇備藥_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_手動選擇備藥品 dialog_手動選擇備藥 = new Dialog_手動選擇備藥品(this.sqL_DataGridView_出入庫作業);
            dialog_手動選擇備藥.ShowDialog();
            if (dialog_手動選擇備藥.DialogResult != DialogResult.Yes) return;
            List<object[]> list_value_常溫 = new List<object[]>();
            List<object[]> list_value_冷藏 = new List<object[]>();
            List<object[]> list_value = Function_取得異動儲位資訊從本地資料(dialog_手動選擇備藥.Value,ref list_value_常溫,ref list_value_冷藏);
            list_自動備藥_開始備藥_常溫 = list_value_常溫;
            list_自動備藥_開始備藥_冷藏 = list_value_冷藏;
            if (PLC_Device_自動備藥_開始備藥.Bool == false)
            {
                PLC_Device_自動備藥_開始備藥.Bool = true;
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("開始備藥", 1500, Color.Green);
                dialog_AlarmForm.ShowDialog();
                return;
            }

        }
        #endregion
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
