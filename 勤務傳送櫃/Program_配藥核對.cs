﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using MyUI;
using Basic;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using SQLUI;
using H_Pannel_lib;
using HIS_DB_Lib;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace 勤務傳送櫃
{
    public partial class Form1 : Form
    {
        MyThread myThread_配藥核對;
        private void Program_配藥核對_Init()
        {
            this.plC_UI_Init.Add_Method(this.Program_配藥核對);
            myThread_配藥核對 = new MyThread();
            myThread_配藥核對.AutoRun(true);
            myThread_配藥核對.Add_Method(sub_Program_配藥核對_刷入藥袋);
            myThread_配藥核對.SetSleepTime(10);
            myThread_配藥核對.Trigger();
        }
        bool flag_配藥核對_頁面更新_init = false;
        private void Program_配藥核對()
        {
            if (this.plC_ScreenPage_Main.PageText == "配藥核對")
            {
                if (flag_配藥核對_頁面更新_init)
                {
                    this.Invoke(new Action(delegate
                    {

                    }));
                    MySerialPort_Scanner01.ClearReadByte();
                    flag_配藥核對_頁面更新_init = false;
                }
            }
            else
            {
                flag_配藥核對_頁面更新_init = true;
            }

            //sub_Program_配藥核對_刷入藥袋();
        }

        #region PLC_配藥核對_刷入藥袋
        PLC_Device PLC_Device_配藥核對_刷入藥袋 = new PLC_Device("");
        PLC_Device PLC_Device_配藥核對_刷入藥袋_OK = new PLC_Device("");
        Task Task_配藥核對_刷入藥袋;
        MyTimer MyTimer_配藥核對_刷入藥袋_結束延遲 = new MyTimer();
        int cnt_Program_配藥核對_刷入藥袋 = 65534;
        void sub_Program_配藥核對_刷入藥袋()
        {
            if (cnt_Program_配藥核對_刷入藥袋 == 65534)
            {
                this.MyTimer_配藥核對_刷入藥袋_結束延遲.StartTickTime(100);
                PLC_Device_配藥核對_刷入藥袋.SetComment("PLC_配藥核對_刷入藥袋");
                PLC_Device_配藥核對_刷入藥袋_OK.SetComment("PLC_配藥核對_刷入藥袋_OK");
                PLC_Device_配藥核對_刷入藥袋.Bool = false;
                cnt_Program_配藥核對_刷入藥袋 = 65535;
            }
            if (this.plC_ScreenPage_Main.PageText == "配藥核對") PLC_Device_配藥核對_刷入藥袋.Bool = true;
            if (cnt_Program_配藥核對_刷入藥袋 == 65535) cnt_Program_配藥核對_刷入藥袋 = 1;
            if (cnt_Program_配藥核對_刷入藥袋 == 1) cnt_Program_配藥核對_刷入藥袋_檢查按下(ref cnt_Program_配藥核對_刷入藥袋);
            if (cnt_Program_配藥核對_刷入藥袋 == 2) cnt_Program_配藥核對_刷入藥袋_初始化(ref cnt_Program_配藥核對_刷入藥袋);
            if (cnt_Program_配藥核對_刷入藥袋 == 3) cnt_Program_配藥核對_刷入藥袋 = 65500;
            if (cnt_Program_配藥核對_刷入藥袋 > 1) cnt_Program_配藥核對_刷入藥袋_檢查放開(ref cnt_Program_配藥核對_刷入藥袋);

            if (cnt_Program_配藥核對_刷入藥袋 == 65500)
            {
                this.MyTimer_配藥核對_刷入藥袋_結束延遲.TickStop();
                this.MyTimer_配藥核對_刷入藥袋_結束延遲.StartTickTime(100);
                PLC_Device_配藥核對_刷入藥袋.Bool = false;
                PLC_Device_配藥核對_刷入藥袋_OK.Bool = false;
                cnt_Program_配藥核對_刷入藥袋 = 65535;
            }
        }
        void cnt_Program_配藥核對_刷入藥袋_檢查按下(ref int cnt)
        {
            if (PLC_Device_配藥核對_刷入藥袋.Bool) cnt++;
        }
        void cnt_Program_配藥核對_刷入藥袋_檢查放開(ref int cnt)
        {
            if (!PLC_Device_配藥核對_刷入藥袋.Bool) cnt = 65500;
        }
        void cnt_Program_配藥核對_刷入藥袋_初始化(ref int cnt)
        {
            Function_配藥核對_刷入藥袋();
            //if (this.MyTimer_配藥核對_刷入藥袋_結束延遲.IsTimeOut())
            //{
            //    if (Task_配藥核對_刷入藥袋 == null)
            //    {
            //        Task_配藥核對_刷入藥袋 = new Task(new Action(delegate { Function_配藥核對_刷入藥袋(); }));
            //    }
            //    if (Task_配藥核對_刷入藥袋.Status == TaskStatus.RanToCompletion)
            //    {
            //        Task_配藥核對_刷入藥袋 = new Task(new Action(delegate { Function_配藥核對_刷入藥袋(); }));
            //    }
            //    if (Task_配藥核對_刷入藥袋.Status == TaskStatus.Created)
            //    {
            //        Task_配藥核對_刷入藥袋.Start();
            //    }
            //    cnt++;
            //}
        }

        #endregion

        #region Function
        MyTimerBasic MyTimerBasic_配藥核對_刷藥單結束計時 = new MyTimerBasic();
        private void Function_配藥核對_刷入藥袋()
        {
            try
            {
                if (MyTimerBasic_配藥核對_刷藥單結束計時.IsTimeOut())
                {
                    if (rJ_Lable_配藥核對_狀態.Text != "等待刷藥單...")
                    {
                        this.Invoke(new Action(delegate
                        {
                            rJ_Lable_配藥核對_狀態.BackgroundColor = Color.MidnightBlue;
                            rJ_Lable_配藥核對_狀態.Text = "等待刷藥單...";

                            rJ_Lable_配藥核對_藥名.Text = "";
                            rJ_Lable_配藥核對_總量.Text = "";
                            rJ_Lable_配藥核對_頻次.Text = "";
                            rJ_Lable_配藥核對_病人姓名.Text = "";
                            rJ_Lable_配藥核對_病歷號.Text = "";
                            rJ_Lable_配藥核對_開方時間.Text = "";
                            rJ_Lable_配藥核對_病房.Text = "";
                            //Application.DoEvents();
                        }));
                    }
                }

                string text = null;
                int scn_load = 0;
                if (text == null)
                {
                    if (MySerialPort_Scanner01.IsConnected)
                    {
                        text = MySerialPort_Scanner01.ReadString();
                        scn_load = 1;
                    }
                }
                if (text == null)
                {
                    if (MySerialPort_Scanner02.IsConnected)
                    {
                        text = MySerialPort_Scanner02.ReadString();
                        scn_load = 2;
                    }
                }
                if (text == null)
                {
                    return;
                }
                System.Threading.Thread.Sleep(200);
                MyTimerBasic_配藥核對_刷藥單結束計時.TickStop();
                MyTimerBasic_配藥核對_刷藥單結束計時.StartTickTime(100000);
                if (scn_load == 1) text = MySerialPort_Scanner01.ReadString();
                if (scn_load == 2) text = MySerialPort_Scanner02.ReadString();
                MySerialPort_Scanner01.ClearReadByte();
                MySerialPort_Scanner02.ClearReadByte();
                text = text.Replace("\0", "");
                text = text.Replace("\n", "");
                if (text.StringIsEmpty()) return;
                if (text.Length <= 2 || text.Length > 500)
                {
                    MySerialPort_Scanner01.ClearReadByte();
                    MySerialPort_Scanner02.ClearReadByte();

                    return;
                }
                text = text.Replace("\r\n", "");
                Console.WriteLine($"接收掃碼內容:{text}");
                List<OrderClass> orderClasses = this.Function_醫令資料_API呼叫(dBConfigClass.OrderApiURL, text);
                if (orderClasses.Count == 0)
                {
                    this.Invoke(new Action(delegate
                    {
                        rJ_Lable_配藥核對_狀態.BackgroundColor = Color.HotPink;
                        rJ_Lable_配藥核對_狀態.Text = "找無藥單資料!";
                        //Application.DoEvents();
                        MyTimerBasic_配藥核對_刷藥單結束計時.TickStop();
                        MyTimerBasic_配藥核對_刷藥單結束計時.StartTickTime(3000);
                        using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\ttsmaker-請重刷.wav"))
                        {
                            sp.Stop();
                            sp.Play();
                            sp.PlaySync();
                        }
                    }));
                    return;
                }


                this.Invoke(new Action(delegate
                {
                    rJ_Lable_配藥核對_藥名.Text = $"  {orderClasses[0].藥品名稱}";
                    rJ_Lable_配藥核對_總量.Text = orderClasses[0].交易量;
                    rJ_Lable_配藥核對_頻次.Text = orderClasses[0].頻次;
                    rJ_Lable_配藥核對_病人姓名.Text = orderClasses[0].病人姓名;
                    rJ_Lable_配藥核對_病歷號.Text = orderClasses[0].病歷號;
                    rJ_Lable_配藥核對_開方時間.Text = orderClasses[0].開方日期;
                    rJ_Lable_配藥核對_病房.Text = $"{orderClasses[0].病房}-{orderClasses[0].床號}";
                    //Application.DoEvents();
                }));
                if (orderClasses[0].狀態 == "已調劑")
                {
                    this.Invoke(new Action(delegate
                    {
                        rJ_Lable_配藥核對_狀態.BackgroundColor = Color.HotPink;
                        rJ_Lable_配藥核對_狀態.Text = "此藥單已刷入過!";
                        //Application.DoEvents();
                        MyTimerBasic_配藥核對_刷藥單結束計時.TickStop();
                        MyTimerBasic_配藥核對_刷藥單結束計時.StartTickTime(3000);
                        using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\fail_01.wav"))
                        {
                            sp.Stop();
                            sp.Play();
                            sp.PlaySync();
                        }
                    }));
                    return;
                }
                else
                {
                    this.Invoke(new Action(delegate
                    {
                        rJ_Lable_配藥核對_狀態.BackgroundColor = Color.DarkGreen;
                        rJ_Lable_配藥核對_狀態.Text = "刷取成功!";
                        //Application.DoEvents();
                    }));

                }

                if (Pannel_Box.PharLightOn(orderClasses[0].病房) == false)
                {
                    this.Invoke(new Action(delegate
                    {
                        rJ_Lable_配藥核對_狀態.BackgroundColor = Color.HotPink;
                        rJ_Lable_配藥核對_狀態.Text = "未在櫃體內,找到病房名稱!";
                        //Application.DoEvents();
                        MyTimerBasic_配藥核對_刷藥單結束計時.TickStop();
                        MyTimerBasic_配藥核對_刷藥單結束計時.StartTickTime(3000);
                        using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\fail_01.wav"))
                        {
                            sp.Stop();
                            sp.Play();
                            sp.PlaySync();
                        }
                    }));
                    return;
                }
                this.Invoke(new Action(delegate
                {
                    //Application.DoEvents();
                    MyTimerBasic_配藥核對_刷藥單結束計時.TickStop();
                    MyTimerBasic_配藥核對_刷藥單結束計時.StartTickTime(5000);
                    using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\sucess_01.wav"))
                    {
                        sp.Stop();
                        sp.Play();
                        sp.PlaySync();
                    }

                }));

                object[] value = new object[new enum_交易記錄查詢資料().GetLength()];
                value[(int)enum_交易記錄查詢資料.GUID] = orderClasses[0].GUID;
                value[(int)enum_交易記錄查詢資料.動作] = enum_交易記錄查詢動作.藥袋刷入.GetEnumName();
                value[(int)enum_交易記錄查詢資料.藥品碼] = orderClasses[0].藥品碼;
                value[(int)enum_交易記錄查詢資料.領藥號] = orderClasses[0].領藥號;
                value[(int)enum_交易記錄查詢資料.藥品名稱] = orderClasses[0].藥品名稱;
                value[(int)enum_交易記錄查詢資料.頻次] = orderClasses[0].頻次;
                value[(int)enum_交易記錄查詢資料.病房號] = orderClasses[0].病房;
                value[(int)enum_交易記錄查詢資料.交易量] = orderClasses[0].交易量;
                value[(int)enum_交易記錄查詢資料.病人姓名] = orderClasses[0].病人姓名;
                value[(int)enum_交易記錄查詢資料.病歷號] = orderClasses[0].病歷號;
                value[(int)enum_交易記錄查詢資料.開方時間] = orderClasses[0].開方日期;
                value[(int)enum_交易記錄查詢資料.領用人] = "未領用";
                value[(int)enum_交易記錄查詢資料.領用時間] = "1999-01-01 00:00:00";
                value[(int)enum_交易記錄查詢資料.操作時間] = DateTime.Now.ToDateTimeString_6();
                value[(int)enum_交易記錄查詢資料.操作人] = this.登入者名稱;

                object[] value_醫令資料 = orderClasses[0].ClassToSQL<OrderClass, enum_醫囑資料>();
                value_醫令資料[(int)enum_醫囑資料.狀態] = "已調劑";
                value_醫令資料[(int)enum_醫囑資料.結方日期] = DateTime.MinValue.ToDateTimeString();
                value_醫令資料[(int)enum_醫囑資料.展藥時間] = DateTime.MinValue.ToDateTimeString();
                value_醫令資料[(int)enum_醫囑資料.過帳時間] = DateTime.Now.ToDateTimeString_6();
                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value, false);
                this.sqL_DataGridView_醫令資料.SQL_ReplaceExtra(value_醫令資料, false);

                Funtion_藥袋刷入API(orderClasses[0], this.登入者名稱, this.登入者ID);
            }
            catch(Exception e)
            {
                MyMessageBox.ShowDialog($"錯誤({e.Message})");
            }
           

        }
        #endregion
        #region Event

        #endregion
    }
}
