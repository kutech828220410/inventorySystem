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
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Program_自動備藥_Init()
        {


     
            plC_RJ_Button_開始備藥.MouseDownEvent += PlC_RJ_Button_開始備藥_MouseDownEvent;

            this.uc_備藥通知處方.Init();
            plC_UI_Init.Add_Method(Program_自動備藥);
        }

     

       

        private void Program_自動備藥()
        {
            sub_Program_自動備藥_測試處方();
        }

        #region PLC_自動備藥_測試處方
        PLC_Device PLC_Device_自動備藥_測試處方 = new PLC_Device("M30000");
        PLC_Device PLC_Device_自動備藥_測試處方_OK = new PLC_Device("");
        Task Task_自動備藥_測試處方;
        MyTimer MyTimer_自動備藥_測試處方_結束延遲 = new MyTimer();
        int cnt_Program_自動備藥_測試處方 = 65534;
        void sub_Program_自動備藥_測試處方()
        {
            if (myConfigClass.主機模式 == false) return;
            if (cnt_Program_自動備藥_測試處方 == 65534)
            {
                this.MyTimer_自動備藥_測試處方_結束延遲.StartTickTime(50);
                PLC_Device_自動備藥_測試處方.SetComment("PLC_自動備藥_測試處方");
                PLC_Device_自動備藥_測試處方_OK.SetComment("PLC_自動備藥_測試處方_OK");
                PLC_Device_自動備藥_測試處方.Bool = false;
                cnt_Program_自動備藥_測試處方 = 65535;
            }
            if (cnt_Program_自動備藥_測試處方 == 65535) cnt_Program_自動備藥_測試處方 = 1;
            if (cnt_Program_自動備藥_測試處方 == 1) cnt_Program_自動備藥_測試處方_檢查按下(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 2) cnt_Program_自動備藥_測試處方_初始化(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 3) cnt_Program_自動備藥_測試處方_藥盒從進出盒區傳送至常溫區(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 4) cnt_Program_自動備藥_測試處方_等待藥盒從進出盒區傳送至常溫區完成(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 5) cnt_Program_自動備藥_測試處方_出料一次01(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 6) cnt_Program_自動備藥_測試處方_等待出料一次01完成(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 7) cnt_Program_自動備藥_測試處方_出料一次02(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 8) cnt_Program_自動備藥_測試處方_等待出料一次02完成(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 9) cnt_Program_自動備藥_測試處方_藥盒從常溫區傳送至冷藏區(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 10) cnt_Program_自動備藥_測試處方_等待藥盒從常溫區傳送至冷藏區完成(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 11) cnt_Program_自動備藥_測試處方_出料一次03(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 12) cnt_Program_自動備藥_測試處方_等待出料一次03完成(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 13) cnt_Program_自動備藥_測試處方_出料一次04(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 14) cnt_Program_自動備藥_測試處方_等待出料一次04完成(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 15) cnt_Program_自動備藥_測試處方_藥盒從冷藏區傳接至常溫區(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 16) cnt_Program_自動備藥_測試處方_等待藥盒從冷藏區傳接至常溫區_已到傳接位置(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 17) cnt_Program_自動備藥_測試處方_等待藥盒從冷藏區傳接至常溫區_已到結束待命位置(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 18) cnt_Program_自動備藥_測試處方_藥盒從常溫區傳接至進出盒區(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 19) cnt_Program_自動備藥_測試處方_等待藥盒從常溫區傳接至進出盒區完成(ref cnt_Program_自動備藥_測試處方);
            if (cnt_Program_自動備藥_測試處方 == 20) cnt_Program_自動備藥_測試處方 = 65500;
            if (cnt_Program_自動備藥_測試處方 > 1) cnt_Program_自動備藥_測試處方_檢查放開(ref cnt_Program_自動備藥_測試處方);

            if (cnt_Program_自動備藥_測試處方 == 65500)
            {
                this.MyTimer_自動備藥_測試處方_結束延遲.TickStop();
                this.MyTimer_自動備藥_測試處方_結束延遲.StartTickTime(50);
                PLC_Device_自動備藥_測試處方.Bool = false;
                PLC_Device_自動備藥_測試處方_OK.Bool = false;

                PLC_Device_出料一次.Bool = false;
                PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool = false;
                PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool = false;
                PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool = false;
                PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool = false;


                this.Invoke(new Action(delegate 
                {
                    rJ_Lable_備藥狀態.Visible = false;
                }));
                cnt_Program_自動備藥_測試處方 = 65535;
            }
        }
        void cnt_Program_自動備藥_測試處方_檢查按下(ref int cnt)
        {
            if (PLC_Device_自動備藥_測試處方.Bool) cnt++;
        }
        void cnt_Program_自動備藥_測試處方_檢查放開(ref int cnt)
        {
            if (!PLC_Device_自動備藥_測試處方.Bool) cnt = 65500;
        }
        void cnt_Program_自動備藥_測試處方_初始化(ref int cnt)
        {
            if(!PLC_IO_進出盒區_藥盒到位感應.Bool)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未置入藥盒", 2000);
                dialog_AlarmForm.ShowDialog();
                cnt = 65500;
                return;
            }
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態.Text = $"備藥中...(0 / 4)";
                rJ_Lable_備藥狀態.Visible = true;
            }));
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_藥盒從進出盒區傳送至常溫區(ref int cnt)
        {
            if (PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool) return;
            PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool = true;
            PLC_Device_冷藏區輸送門開啟.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待藥盒從進出盒區傳送至常溫區完成(ref int cnt)
        {
            if (PLC_Device_將藥盒從進出盒區傳接至常溫區.Bool) return;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_出料一次01(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            出料一次_IP = "192.168.60.35";
            PLC_Device_出料一次.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待出料一次01完成(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態.Text = $"備藥中...(1 / 4)";
                rJ_Lable_備藥狀態.Visible = true;
            }));
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_出料一次02(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            出料一次_IP = "192.168.60.78";
            PLC_Device_出料一次.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待出料一次02完成(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態.Text = $"備藥中...(2 / 4)";
                rJ_Lable_備藥狀態.Visible = true;
            }));
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_藥盒從常溫區傳送至冷藏區(ref int cnt)
        {
            if (PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool) return;
            PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待藥盒從常溫區傳送至冷藏區完成(ref int cnt)
        {
            if (PLC_Device_將藥盒從常溫區傳接至冷藏區.Bool) return;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_出料一次03(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            出料一次_IP = "192.168.61.11";
            PLC_Device_出料一次.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待出料一次03完成(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態.Text = $"備藥中...(3 / 4)";
                rJ_Lable_備藥狀態.Visible = true;
            }));
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_出料一次04(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            出料一次_IP = "192.168.61.27";
            PLC_Device_出料一次.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待出料一次04完成(ref int cnt)
        {
            if (PLC_Device_出料一次.Bool) return;
            this.Invoke(new Action(delegate
            {
                rJ_Lable_備藥狀態.Text = $"備藥中...(4 / 4)";
                rJ_Lable_備藥狀態.Visible = true;
            }));
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_藥盒從冷藏區傳接至常溫區(ref int cnt)
        {
            if (PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool) return;
            PLC_Device_將藥盒從冷藏區傳接至常溫區.Bool = true;
            PLC_Device_將藥盒從冷藏區傳接至常溫區_已到傳接位置.Bool = false;
            PLC_Device_將藥盒從冷藏區傳接至常溫區_已到結束待命位置.Bool = false;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待藥盒從冷藏區傳接至常溫區_已到傳接位置(ref int cnt)
        {
            if (!PLC_Device_將藥盒從冷藏區傳接至常溫區_已到傳接位置.Bool) return;
            if (PLC_Device_常溫區輸送門開啟.Bool) return;
            PLC_Device_常溫區輸送門開啟.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待藥盒從冷藏區傳接至常溫區_已到結束待命位置(ref int cnt)
        {
            if (!PLC_Device_將藥盒從冷藏區傳接至常溫區_已到結束待命位置.Bool) return;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_藥盒從常溫區傳接至進出盒區(ref int cnt)
        {
            if (PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool) return;
            PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool = true;
            cnt++;
        }
        void cnt_Program_自動備藥_測試處方_等待藥盒從常溫區傳接至進出盒區完成(ref int cnt)
        {
            if (PLC_Device_將藥盒從常溫區傳接至進出盒區.Bool) return;
            cnt++;
        }
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
            this.Invoke(new Action(delegate 
            {
                Dialog_備藥清單 dialog_備藥清單 = new Dialog_備藥清單(GUID , 登入者名稱);
                dialog_備藥清單.ShowDialog();
            }));           
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
