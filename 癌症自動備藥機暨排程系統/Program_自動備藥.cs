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
namespace 癌症備藥機
{
    public partial class Main_Form : Form
    {
        MyThread myThread_開始備藥;
        MyThread myThread_自動備藥_處方更新;

        private void Program_自動備藥_Init()
        {
          
            plC_RJ_Button_開始備藥.MouseDownEvent += PlC_RJ_Button_開始備藥_MouseDownEvent;
            plC_RJ_Button_自動備藥_手動選擇備藥.MouseDownEvent += PlC_RJ_Button_自動備藥_手動選擇備藥_MouseDownEvent;
            plC_RJ_Button_自動備藥_重新整理.MouseDownEvent += PlC_RJ_Button_自動備藥_重新整理_MouseDownEvent;
            this.uc_備藥通知處方.Init();


            myThread_開始備藥 = new MyThread(this.FindForm());
            myThread_開始備藥.AutoRun(true);
            myThread_開始備藥.SetSleepTime(1);
            myThread_開始備藥.Add_Method(Program_自動備藥);
            myThread_開始備藥.Trigger();


            myThread_自動備藥_處方更新 = new MyThread(this.FindForm());
            myThread_自動備藥_處方更新.AutoRun(true);
            myThread_自動備藥_處方更新.SetSleepTime(500);
            myThread_自動備藥_處方更新.Add_Method(Program_自動備藥_處方更新);
            myThread_自動備藥_處方更新.Trigger();

            //plC_UI_Init.Add_Method(Program_自動備藥);
        }
     
        private void Program_自動備藥()
        {
            sub_Program_自動備藥_開始備藥();
            sub_Program_強制出盒();


        }
        private void Function_出料一次(string IP)
        {
            出料一次_IP = IP;
            int cnt = 0;
            while (true)
            {
                if (cnt == 0)
                {
                    if (PLC_Device_出料一次.Bool) continue;
                    PLC_Device_出料一次.Bool = true;
                    cnt++;
                }
                if (cnt == 1)
                {
                    if (PLC_Device_出料一次.Bool) continue;
                    cnt++;
                }
                if (cnt == 2)
                {
                    break;
                }
                System.Threading.Thread.Sleep(1);
            }
        }
        
        private void Program_自動備藥_處方更新()
        {
            if (this.plC_ScreenPage_main.PageText != "自動備藥") return;
            this.uc_備藥通知處方.RefreshGrid();
            this.Invoke(new Action(delegate 
            {
                label_自動備藥_處方上次更新時間.Text = $"處方上次更新間 : {DateTime.Now.ToDateTimeString()}";
            }));
        }


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
            if (cnt_Program_自動備藥_開始備藥 == 5) cnt_Program_自動備藥_開始備藥_常溫區_出料一次(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 6) cnt_Program_自動備藥_開始備藥_藥盒從常溫區傳送至冷藏區(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 7) cnt_Program_自動備藥_開始備藥_等待藥盒從常溫區傳送至冷藏區完成(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 8) cnt_Program_自動備藥_開始備藥_冷藏區_出料一次(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 9) cnt_Program_自動備藥_開始備藥_藥盒從冷藏區傳接至常溫區(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 10) cnt_Program_自動備藥_開始備藥_等待藥盒從冷藏區傳接至常溫區_已到傳接位置(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 11) cnt_Program_自動備藥_開始備藥_等待藥盒從冷藏區傳接至常溫區_已到結束待命位置(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 12) cnt_Program_自動備藥_開始備藥_藥盒從常溫區傳接至進出盒區(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 13) cnt_Program_自動備藥_開始備藥_等待藥盒從常溫區傳接至進出盒區完成(ref cnt_Program_自動備藥_開始備藥);
            if (cnt_Program_自動備藥_開始備藥 == 14) cnt_Program_自動備藥_開始備藥 = 65500;
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

                Function_登入畫面_登出();
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
            if(!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
            if (PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool) return;
            cnt++;
        }
        void cnt_Program_自動備藥_開始備藥_常溫區_出料一次(ref int cnt)
        {
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
                int temp = 數量;
                if (temp < 0) temp = temp * -1;
                for (int k = 0; k < temp; k++)
                {
                    Function_出料一次(IP);
                }
                if (數量 > 0)
                {
                    數量 = 數量 * -1;
                }
                list_自動備藥_開始備藥_常溫[i][(int)enum_儲位資訊.異動量] = 數量;
                Function_庫存異動至本地資料(list_自動備藥_開始備藥_常溫[i], true);
               
                string url = $"{Main_Form.API_Server}/api/transactions/add";
                returnData returnData = new returnData();
                returnData.ServerName = "cheom";
                returnData.ServerType = "癌症備藥機";
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.動作 = enum_交易記錄查詢動作.系統領藥.GetEnumName();
                transactionsClass.藥品碼 = storage.Code;
                transactionsClass.藥品名稱 = storage.Name;
                transactionsClass.操作人 = 登入者名稱;
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
            if (list_自動備藥_開始備藥_冷藏.Count == 0)
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
                int temp = 數量;
                if (temp < 0) temp = temp * -1;
                for (int k = 0; k < temp; k++)
                {
                    Function_出料一次(IP);
                }
                if (數量 > 0) 數量 = 數量 * -1;
                list_自動備藥_開始備藥_冷藏[i][(int)enum_儲位資訊.異動量] = 數量;
                Function_庫存異動至本地資料(list_自動備藥_開始備藥_冷藏[i], true);

                string url = $"{Main_Form.API_Server}/api/transactions/add";
                returnData returnData = new returnData();
                returnData.ServerName = "cheom";
                returnData.ServerType = "癌症備藥機";
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.動作 = enum_交易記錄查詢動作.系統領藥.GetEnumName();
                transactionsClass.藥品碼 = storage.Code;
                transactionsClass.藥品名稱 = storage.Name;
                transactionsClass.操作人 = 登入者名稱;
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
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
        #region PLC_強制出盒
        PLC_Device PLC_Device_強制出盒 = new PLC_Device("M30100");
        PLC_Device PLC_Device_強制出盒_OK = new PLC_Device("");
        MyTimer MyTimer_強制出盒_結束延遲 = new MyTimer();
        int cnt_Program_強制出盒 = 65534;
        void sub_Program_強制出盒()
        {
            if (myConfigClass.主機模式 == false) return;
            if (cnt_Program_強制出盒 == 65534)
            {
                this.MyTimer_強制出盒_結束延遲.StartTickTime(50);
                PLC_Device_強制出盒.SetComment("PLC_強制出盒");
                PLC_Device_強制出盒_OK.SetComment("PLC_強制出盒_OK");
                PLC_Device_強制出盒.Bool = false;
                cnt_Program_強制出盒 = 65535;
            }
            if (cnt_Program_強制出盒 == 65535) cnt_Program_強制出盒 = 1;
            if (cnt_Program_強制出盒 == 1) cnt_Program_強制出盒_檢查按下(ref cnt_Program_強制出盒);
            if (cnt_Program_強制出盒 == 2) cnt_Program_強制出盒_初始化(ref cnt_Program_強制出盒);
            if (cnt_Program_強制出盒 == 3) cnt_Program_強制出盒 = 65500;

            if (cnt_Program_強制出盒 == 100) cnt_Program_強制出盒_藥盒從冷藏區傳接至常溫區(ref cnt_Program_強制出盒);
            if (cnt_Program_強制出盒 == 101) cnt_Program_強制出盒_等待藥盒從冷藏區傳接至常溫區_已到傳接位置(ref cnt_Program_強制出盒);
            if (cnt_Program_強制出盒 == 102) cnt_Program_強制出盒 = 200;

            if (cnt_Program_強制出盒 == 200) cnt_Program_強制出盒_藥盒從常溫區傳接至進出盒區(ref cnt_Program_強制出盒);
            if (cnt_Program_強制出盒 == 201) cnt_Program_強制出盒_等待藥盒從常溫區傳接至進出盒區完成(ref cnt_Program_強制出盒);
            if (cnt_Program_強制出盒 == 202) cnt_Program_強制出盒 = 65500;
            if (cnt_Program_強制出盒 > 1) cnt_Program_強制出盒_檢查放開(ref cnt_Program_強制出盒);

            if (cnt_Program_強制出盒 == 65500)
            {
                this.MyTimer_強制出盒_結束延遲.TickStop();
                this.MyTimer_強制出盒_結束延遲.StartTickTime(50);
                PLC_Device_強制出盒.Bool = false;
                PLC_Device_強制出盒_OK.Bool = false;

                PLC_Device_出料一次.Bool = false;
                PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool = false;
                PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool = false;
                PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool = false;
                PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool = false;
                this.Invoke(new Action(delegate
                {
                    panel_備藥狀態.Visible = false;
                }));

                cnt_Program_強制出盒 = 65535;
            }
        }
        void cnt_Program_強制出盒_檢查按下(ref int cnt)
        {
            if (PLC_Device_強制出盒.Bool) cnt++;
        }
        void cnt_Program_強制出盒_檢查放開(ref int cnt)
        {
            if (!PLC_Device_強制出盒.Bool) cnt = 65500;
        }
        void cnt_Program_強制出盒_初始化(ref int cnt)
        {
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
            this.Invoke(new Action(delegate
            {
                panel_備藥狀態.Visible = true;
            }));
            if (PLC_IO_進出盒區_藥盒到位感應.Bool)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("出藥盒區域有盒子,請取出", 1500);
                dialog_AlarmForm.ShowDialog();
                cnt = 65500;
                return;
            }

            if (PLC_IO_常溫區_藥盒左感應.Bool
             || PLC_IO_常溫區_藥盒中感應.Bool
             || PLC_IO_常溫區_藥盒右感應.Bool)
            {
                this.Invoke(new Action(delegate
                {
                    rJ_Lable_備藥狀態.Text = "排出常溫區藥盒";
                }));
                cnt = 200;
                return;
            }
            if ( PLC_IO_冷藏區_藥盒左感應.Bool
             || PLC_IO_冷藏區_藥盒中感應.Bool
             || PLC_IO_冷藏區_藥盒右感應.Bool)
            {
                this.Invoke(new Action(delegate
                {
                    rJ_Lable_備藥狀態.Text = "排出冷藏區藥盒";
                }));
                cnt = 100;
                return;
            }


            cnt++;
        }
 
       
        void cnt_Program_強制出盒_藥盒從冷藏區傳接至常溫區(ref int cnt)
        {
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
     
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態.Text = "傳送回常溫區出貨";
            }));
            if (PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool) return;
            PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool = true;
            PLC_Device_將藥盒從冷藏區傳接至常溫區_已到傳接位置.Bool = false;
            PLC_Device_將藥盒從冷藏區傳接至常溫區_已到結束待命位置.Bool = false;
            cnt++;
        }
        void cnt_Program_強制出盒_等待藥盒從冷藏區傳接至常溫區_已到傳接位置(ref int cnt)
        {
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }

            if (PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool) return;
            if (PLC_Device_常溫區輸送門開啟.Bool) return;
            PLC_Device_常溫區輸送門開啟.Bool = true;
            cnt++;
        }
  
        void cnt_Program_強制出盒_藥盒從常溫區傳接至進出盒區(ref int cnt)
        {
            if (!Function_檢查軸控Alarm())
            {
                cnt = 65500;
                return;
            }
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態.Text = "傳送回出貨區";
            }));
            if (PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool) return;
            PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool = true;
            cnt++;
        }
        void cnt_Program_強制出盒_等待藥盒從常溫區傳接至進出盒區完成(ref int cnt)
        {
            if (PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool) return;
            cnt++;
        }
        #endregion

        #region Function

        #endregion
        #region Event    
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
            Dialog_備藥清單 dialog_備藥清單 = new Dialog_備藥清單(GUID, 登入者名稱);
            if (dialog_備藥清單.ShowDialog() != DialogResult.Yes) return;
        
            while(true)
            {
                string msg = "";
                for (int i = 0; i < dialog_備藥清單.stockClasses.Count; i++)
                {
                    string 藥碼 = dialog_備藥清單.stockClasses[i].Code;
                    string 藥名 = dialog_備藥清單.stockClasses[i].Name;
                    string 數量 = dialog_備藥清單.stockClasses[i].Qty;
                    msg += $"{i} ({藥碼}){藥名},數量:{數量}\n";
                }
                msg += $"【請將藥盒置放到入料口後按確認】";
                if (MyMessageBox.ShowDialog($"{msg}", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                if (!PLC_IO_進出盒區_藥盒到位感應.Bool)
                {
                    MyMessageBox.ShowDialog("藥盒未放入入料口");
                    System.Threading.Thread.Sleep(10);
                    continue;

                }
                else
                {
                    break;
                }
             
            }

            List<object[]> list_value_常溫 = new List<object[]>();
            List<object[]> list_value_冷藏 = new List<object[]>();
            List<StockClass> stockClasses = dialog_備藥清單.stockClasses;
            List<string> list_IP = new List<string>();
            Function_取得異動儲位資訊從本地資料(dialog_備藥清單.stockClasses, ref list_value_常溫, ref list_value_冷藏);
            list_自動備藥_開始備藥_常溫 = list_value_常溫;
            list_自動備藥_開始備藥_冷藏 = list_value_冷藏;
            if (PLC_Device_自動備藥_開始備藥.Bool == false)
            {
                for (int i = 0; i < stockClasses.Count; i++)
                {
                    list_IP.LockAdd(Function_儲位亮燈_取得層架亮燈IP(stockClasses[i].Code, Color.Blue));
                }
                Function_儲位亮燈_層架亮燈(list_IP);
                PLC_Device_自動備藥_開始備藥.Bool = true;
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("開始備藥", 1500, Color.Green);
                dialog_AlarmForm.ShowDialog();
                return;
            }
        }
        private void PlC_RJ_Button_自動備藥_重新整理_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            this.uc_備藥通知處方.RefreshGrid();
            LoadingForm.CloseLoadingForm();
        }
        private void PlC_RJ_Button_自動備藥_手動選擇備藥_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_手動選擇備藥品 dialog_手動選擇備藥 = new Dialog_手動選擇備藥品(this.sqL_DataGridView_出入庫作業);
            dialog_手動選擇備藥.ShowDialog();
            if (dialog_手動選擇備藥.DialogResult != DialogResult.Yes) return;
            List<StockClass> stockClasses = dialog_手動選擇備藥.Value;
            List<string> list_IP = new List<string>();
            List<object[]> list_value_常溫 = new List<object[]>();
            List<object[]> list_value_冷藏 = new List<object[]>();
            List<object[]> list_value = Function_取得異動儲位資訊從本地資料(stockClasses, ref list_value_常溫,ref list_value_冷藏);
            list_自動備藥_開始備藥_常溫 = list_value_常溫;
            list_自動備藥_開始備藥_冷藏 = list_value_冷藏;
            if (PLC_Device_自動備藥_開始備藥.Bool == false)
            {
                PLC_Device_自動備藥_開始備藥.Bool = true;
                for (int i = 0; i < stockClasses.Count; i++)
                {
                    list_IP.LockAdd(Function_儲位亮燈_取得層架亮燈IP(stockClasses[i].Code, Color.Blue));
                }
                Function_儲位亮燈_層架亮燈(list_IP);
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
