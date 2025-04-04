﻿using System;
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
using H_Pannel_lib;
using SQLUI;
using DrawingClass;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        public enum enum_ContextMenuStrip_Main_領藥內容
        {
            修改數量,
        }
        public enum enum_ContextMenuStrip_Main_醫令檢索
        {
            [Description("M2500")]
            病歷號,
            [Description("M2501")]
            領藥號,
        }

        static public SQL_DataGridView _sqL_DataGridView_領藥台_01_領藥內容;
        static public SQL_DataGridView _sqL_DataGridView_領藥台_02_領藥內容;
        static public SQL_DataGridView _sqL_DataGridView_領藥台_03_領藥內容;
        static public SQL_DataGridView _sqL_DataGridView_領藥台_04_領藥內容;
        static public Panel _panel_領藥台_01_藥品資訊;
        static public Panel _panel_領藥台_02_藥品資訊;
        static public string[] LoginUsers
        {
            get
            {
                string[] loginUsers = new string[4];

                loginUsers[0] = 領藥台_01_登入者姓名;
                loginUsers[1] = 領藥台_02_登入者姓名;
                loginUsers[2] = 領藥台_03_登入者姓名;
                loginUsers[3] = 領藥台_04_登入者姓名;

                return loginUsers;
            }
        }
        private string QR_Code_醫令模式切換 = "%%001";
        private MyTimer myTimer_領藥台_01_Logout = new MyTimer(5000);
        private MyTimer myTimer_領藥台_02_Logout = new MyTimer(5000);
        private MyTimer myTimer_領藥台_03_Logout = new MyTimer(5000);
        private MyTimer myTimer_領藥台_04_Logout = new MyTimer(5000);
        private Basic.MyThread MyThread_領藥台_01;
        private Basic.MyThread MyThread_領藥台_02;
        private Basic.MyThread MyThread_領藥台_03;
        private Basic.MyThread MyThread_領藥台_04;
        private Basic.MyThread MyThread_領藥_RFID;
        private Basic.MyThread MyThread_領藥_RFID_入出庫資料檢查;
        private Basic.MyThread MyThread_DHT;
        private Basic.MyThread MyThread_調劑作業;

        private MyTimerBasic MyTimerBasic_dht_timeout = new MyTimerBasic();

        private Voice voice = new Voice();
        private bool flag_Program_領藥台_01_換頁 = false;
        private bool flag_Program_領藥台_02_換頁 = false;
        private bool flag_Program_領藥台_03_換頁 = false;
        private bool flag_Program_領藥台_04_換頁 = false;
        private bool flag_Program_領藥_RFID_換頁 = false;


        private void Program_調劑作業_領藥台_01_Init()
        {
            Table table = new Table(new enum_取藥堆疊母資料());
            this.sqL_DataGridView_領藥台_01_領藥內容.Init(table);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnVisible(false, new enum_取藥堆疊母資料().GetEnumNames());
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(40, DataGridViewContentAlignment.MiddleCenter, enum_取藥堆疊母資料.序號);

            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.庫存量);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.單位);

            if ((myConfigClass.Scanner01_COMPort.StringIsEmpty() == true) && myConfigClass.Scanner02_COMPort.StringIsEmpty()
                && (myConfigClass.Scanner03_COMPort.StringIsEmpty() && myConfigClass.Scanner04_COMPort.StringIsEmpty()))
            {
                this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.盤點量);
                this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.動作);
                this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.狀態);
            }


            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnText("No", enum_取藥堆疊母資料.序號);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnText("藥碼", enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnText("藥名", enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnText("數量", enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnText("結存", enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnText("庫存", enum_取藥堆疊母資料.庫存量);

            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 24, FontStyle.Bold), enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 20, FontStyle.Bold), enum_取藥堆疊母資料.藥品名稱);

            this.sqL_DataGridView_領藥台_01_領藥內容.DataGridRowsChangeRefEvent += SqL_DataGridView_領藥台_01_領藥內容_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_領藥台_01_領藥內容.DataGridRefreshEvent += SqL_DataGridView_領藥台_01_領藥內容_DataGridRefreshEvent;
            this.sqL_DataGridView_領藥台_01_領藥內容.RowEnterEvent += SqL_DataGridView_領藥台_01_領藥內容_RowEnterEvent;
            this.sqL_DataGridView_領藥台_01_領藥內容.DataGridClearGridEvent += SqL_DataGridView_領藥台_01_領藥內容_DataGridClearGridEvent;

            this.textBox_領藥台_01_密碼.PassWordChar = true;
            this.textBox_領藥台_01_帳號.KeyPress += TextBox_領藥台_01_帳號_KeyPress;
            this.textBox_領藥台_01_密碼.KeyPress += TextBox_領藥台_01_密碼_KeyPress;

            this.plC_RJ_Button_領藥台_01_登入.MouseDownEvent += PlC_RJ_Button_領藥台_01_登入_MouseDownEvent;
            this.plC_RJ_Button_領藥台_01_取消作業.MouseDownEvent += PlC_RJ_Button_領藥台_01_取消作業_MouseDownEvent;
            this.plC_Button_領藥台_01_領.ValueChangeEvent += PlC_Button_領藥台_01_領_ValueChangeEvent;
            this.plC_Button_領藥台_01_退.ValueChangeEvent += PlC_Button_領藥台_01_退_ValueChangeEvent;
            this.plC_RJ_Button_全部滅燈.MouseDownEvent += PlC_RJ_Button_全部滅燈_MouseDownEvent;


            this.MyThread_領藥台_01 = new Basic.MyThread(this.FindForm());
            this.MyThread_領藥台_01.Add_Method(this.sub_Program_領藥台_01);
            this.MyThread_領藥台_01.AutoRun(true);
            this.MyThread_領藥台_01.AutoStop(false);
            this.MyThread_領藥台_01.SetSleepTime(20);
            this.MyThread_領藥台_01.Trigger();
        }
        private void Program_調劑作業_領藥台_02_Init()
        {
            Table table = new Table(new enum_取藥堆疊母資料());
            this.sqL_DataGridView_領藥台_02_領藥內容.Init(table);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnVisible(false, new enum_取藥堆疊母資料().GetEnumNames());
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(40, DataGridViewContentAlignment.MiddleCenter, enum_取藥堆疊母資料.序號);
            //this.sqL_DataGridView_領藥台_02_手輸醫令.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.動作);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.庫存量);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.單位);

            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnText("No", enum_取藥堆疊母資料.序號);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnText("藥碼", enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnText("藥名", enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnText("數量", enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnText("結存", enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnText("庫存", enum_取藥堆疊母資料.庫存量);

            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 24, FontStyle.Bold), enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 20, FontStyle.Bold), enum_取藥堆疊母資料.藥品名稱);
            //this.sqL_DataGridView_領藥台_02_手輸醫令.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_取藥堆疊母資料.狀態);


            this.sqL_DataGridView_領藥台_02_領藥內容.DataGridRowsChangeRefEvent += SqL_DataGridView_領藥台_02_領藥內容_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_領藥台_02_領藥內容.DataGridRefreshEvent += SqL_DataGridView_領藥台_02_領藥內容_DataGridRefreshEvent;
            this.sqL_DataGridView_領藥台_02_領藥內容.RowEnterEvent += SqL_DataGridView_領藥台_02_領藥內容_RowEnterEvent;
            this.sqL_DataGridView_領藥台_02_領藥內容.DataGridClearGridEvent += SqL_DataGridView_領藥台_02_領藥內容_DataGridClearGridEvent;

            this.textBox_領藥台_02_密碼.PassWordChar = true;
            this.textBox_領藥台_02_帳號.KeyPress += TextBox_領藥台_02_帳號_KeyPress;
            this.textBox_領藥台_02_密碼.KeyPress += TextBox_領藥台_02_密碼_KeyPress;

            this.plC_RJ_Button_領藥台_02_登入.MouseDownEvent += PlC_RJ_Button_領藥台_02_登入_MouseDownEvent;
            this.plC_RJ_Button_領藥台_02_取消作業.MouseDownEvent += PlC_RJ_Button_領藥台_02_取消作業_MouseDownEvent;
            this.plC_Button_領藥台_02_領.ValueChangeEvent += PlC_Button_領藥台_02_領_ValueChangeEvent;
            this.plC_Button_領藥台_02_退.ValueChangeEvent += PlC_Button_領藥台_02_退_ValueChangeEvent;

            this.MyThread_領藥台_02 = new Basic.MyThread(this.FindForm());
            this.MyThread_領藥台_02.Add_Method(this.sub_Program_領藥台_02);
            this.MyThread_領藥台_02.AutoRun(true);
            this.MyThread_領藥台_02.AutoStop(false);
            this.MyThread_領藥台_02.SetSleepTime(20);
            this.MyThread_領藥台_02.Trigger();
        }
        private void Program_調劑作業_領藥台_03_Init()
        {
            Table table = new Table(new enum_取藥堆疊母資料());
            this.sqL_DataGridView_領藥台_03_領藥內容.Init(table);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnVisible(false, new enum_取藥堆疊母資料().GetEnumNames());
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(40, DataGridViewContentAlignment.MiddleCenter, enum_取藥堆疊母資料.序號);
            //this.sqL_DataGridView_領藥台_03_手輸醫令.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.動作);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.庫存量);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.單位);

            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnText("No", enum_取藥堆疊母資料.序號);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnText("藥碼", enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnText("藥名", enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnText("數量", enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnText("結存", enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnText("庫存", enum_取藥堆疊母資料.庫存量);

            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 24, FontStyle.Bold), enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 20, FontStyle.Bold), enum_取藥堆疊母資料.藥品名稱);
            //this.sqL_DataGridView_領藥台_03_手輸醫令.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_取藥堆疊母資料.狀態);


            this.sqL_DataGridView_領藥台_03_領藥內容.DataGridRowsChangeRefEvent += SqL_DataGridView_領藥台_03_領藥內容_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_領藥台_03_領藥內容.DataGridRefreshEvent += SqL_DataGridView_領藥台_03_領藥內容_DataGridRefreshEvent;

            this.textBox_領藥台_03_密碼.PassWordChar = true;
            this.textBox_領藥台_03_帳號.KeyPress += TextBox_領藥台_03_帳號_KeyPress;
            this.textBox_領藥台_03_密碼.KeyPress += TextBox_領藥台_03_密碼_KeyPress;

            this.plC_RJ_Button_領藥台_03_登入.MouseDownEvent += PlC_RJ_Button_領藥台_03_登入_MouseDownEvent;
            this.plC_RJ_Button_領藥台_03_取消作業.MouseDownEvent += PlC_RJ_Button_領藥台_03_取消作業_MouseDownEvent;
            this.plC_Button_領藥台_03_領.ValueChangeEvent += PlC_Button_領藥台_03_領_ValueChangeEvent;
            this.plC_Button_領藥台_03_退.ValueChangeEvent += PlC_Button_領藥台_03_退_ValueChangeEvent;

            this.MyThread_領藥台_03 = new Basic.MyThread(this.FindForm());
            this.MyThread_領藥台_03.Add_Method(this.sub_Program_領藥台_03);
            this.MyThread_領藥台_03.AutoRun(true);
            this.MyThread_領藥台_03.AutoStop(false);
            this.MyThread_領藥台_03.SetSleepTime(20);
            this.MyThread_領藥台_03.Trigger();
        }
        private void Program_調劑作業_領藥台_04_Init()
        {
            Table table = new Table(new enum_取藥堆疊母資料());
            this.sqL_DataGridView_領藥台_04_領藥內容.Init(table);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnVisible(false, new enum_取藥堆疊母資料().GetEnumNames());
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(40, DataGridViewContentAlignment.MiddleCenter, enum_取藥堆疊母資料.序號);
            //this.sqL_DataGridView_領藥台_04_手輸醫令.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.動作);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.庫存量);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(75, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.單位);

            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnText("No", enum_取藥堆疊母資料.序號);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnText("藥碼", enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnText("藥名", enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnText("數量", enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnText("結存", enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnText("庫存", enum_取藥堆疊母資料.庫存量);

            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 24, FontStyle.Bold), enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnFont(new Font("微軟正黑體", 20, FontStyle.Bold), enum_取藥堆疊母資料.藥品名稱);
            //this.sqL_DataGridView_領藥台_04_手輸醫令.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_取藥堆疊母資料.狀態);


            this.sqL_DataGridView_領藥台_04_領藥內容.DataGridRowsChangeRefEvent += SqL_DataGridView_領藥台_04_領藥內容_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_領藥台_04_領藥內容.DataGridRefreshEvent += SqL_DataGridView_領藥台_04_領藥內容_DataGridRefreshEvent;

            this.textBox_領藥台_04_密碼.PassWordChar = true;
            this.textBox_領藥台_04_帳號.KeyPress += TextBox_領藥台_04_帳號_KeyPress;
            this.textBox_領藥台_04_密碼.KeyPress += TextBox_領藥台_04_密碼_KeyPress;

            this.plC_RJ_Button_領藥台_04_登入.MouseDownEvent += PlC_RJ_Button_領藥台_04_登入_MouseDownEvent;
            this.plC_RJ_Button_領藥台_04_取消作業.MouseDownEvent += PlC_RJ_Button_領藥台_04_取消作業_MouseDownEvent;
            this.plC_Button_領藥台_04_領.ValueChangeEvent += PlC_Button_領藥台_04_領_ValueChangeEvent;
            this.plC_Button_領藥台_04_退.ValueChangeEvent += PlC_Button_領藥台_04_退_ValueChangeEvent;

            this.MyThread_領藥台_04 = new Basic.MyThread(this.FindForm());
            this.MyThread_領藥台_04.Add_Method(this.sub_Program_領藥台_04);
            this.MyThread_領藥台_04.AutoRun(true);
            this.MyThread_領藥台_04.AutoStop(false);
            this.MyThread_領藥台_04.SetSleepTime(20);
            this.MyThread_領藥台_04.Trigger();


            sqL_DataGridView_領藥台_01_領藥內容.MouseDown += SqL_DataGridView_領藥台_領藥內容_MouseDown;
            sqL_DataGridView_領藥台_02_領藥內容.MouseDown += SqL_DataGridView_領藥台_領藥內容_MouseDown;
            sqL_DataGridView_領藥台_03_領藥內容.MouseDown += SqL_DataGridView_領藥台_領藥內容_MouseDown;
            sqL_DataGridView_領藥台_04_領藥內容.MouseDown += SqL_DataGridView_領藥台_領藥內容_MouseDown;
        }
        private void Fuction_領藥台_時間重置()
        {
            MyTimer_領藥台_01_閒置登出時間.TickStop();
            MyTimer_領藥台_01_閒置登出時間.StartTickTime();
            MyTimer_領藥台_01_入賬完成時間.TickStop();
            MyTimer_領藥台_01_入賬完成時間.StartTickTime();

            MyTimer_領藥台_02_閒置登出時間.TickStop();
            MyTimer_領藥台_02_閒置登出時間.StartTickTime();
            MyTimer_領藥台_02_入賬完成時間.TickStop();
            MyTimer_領藥台_02_入賬完成時間.StartTickTime();

            MyTimer_領藥台_03_閒置登出時間.TickStop();
            MyTimer_領藥台_03_閒置登出時間.StartTickTime();
            MyTimer_領藥台_03_入賬完成時間.TickStop();
            MyTimer_領藥台_03_入賬完成時間.StartTickTime();

            MyTimer_領藥台_04_閒置登出時間.TickStop();
            MyTimer_領藥台_04_閒置登出時間.StartTickTime();
            MyTimer_領藥台_04_入賬完成時間.TickStop();
            MyTimer_領藥台_04_入賬完成時間.StartTickTime();
        }
        private void SqL_DataGridView_領藥內容_RowPostPaintingEventEx(SQL_DataGridView sQL_DataGridView, DataGridViewRowPostPaintEventArgs e)
        {
            object[] value = sQL_DataGridView.GetRowValues(e.RowIndex);
            if (value != null)
            {

                Color row_Backcolor = Color.White;
                Color row_Forecolor = Color.Black;

                string 狀態 = value[(int)enum_取藥堆疊母資料.狀態].ObjectToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    row_Backcolor = Color.Yellow;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    row_Backcolor = Color.Lime;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    row_Backcolor = Color.Red;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                {
                    row_Backcolor = Color.Pink;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName())
                {
                    row_Backcolor = Color.Pink;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.已領用過.GetEnumName())
                {
                    row_Backcolor = Color.White;
                }




                using (Brush brush = new SolidBrush(row_Backcolor))
                {
                    int x = e.RowBounds.Left;
                    int y = e.RowBounds.Top;
                    int width = e.RowBounds.Width;
                    int height = e.RowBounds.Height;
                    int image_width = 250;
                    e.Graphics.FillRectangle(brush, e.RowBounds);
                    DrawingClass.Draw.DrawRoundShadow(e.Graphics, new RectangleF(x - 1, y - 1, width, height), Color.DarkGray, 5, 5);
                    Size size = new Size();
                    PointF pointF = new PointF();
                    float temp_x = 0;
                    Font font;
                    string Code = value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    int col_width = 0;
                    List<Image> images = Main_Form.Function_取得藥品圖片(Code);
                    if (images.Count > 0)
                    {
                        if (images[0] != null) e.Graphics.DrawImage(images[0], x + 2, y + 2, image_width - 2, height - 2);
                    }


                    //font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.藥品碼.GetEnumName());
                    //string 藥碼 = $"({Code})";
                    //DrawingClass.Draw.文字左上繪製(藥碼, new PointF(10, y + 10), font, Color.Black, e.Graphics);
                    //size = 藥碼.MeasureText(font);

                    col_width = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.藥品名稱.GetEnumName());
                    font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.藥品名稱.GetEnumName());
                    string 藥名 = $"{value[(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString()}";
                    size = 藥名.MeasureText(font);
                    pointF = new PointF(10 + image_width, y);
                    DrawingClass.Draw.DrawString(e.Graphics, 藥名, font, new Rectangle((int)pointF.X, (int)pointF.Y, col_width, height), row_Forecolor, DataGridViewContentAlignment.MiddleLeft);
                    temp_x = pointF.X + col_width;

                    //string 單位 = $"[{value[(int)enum_取藥堆疊母資料.單位].ObjectToString()}]";
                    //DrawingClass.Draw.文字左上繪製(單位, new PointF(10 + 650, y + 10), new Font("標楷體", 14), row_Forecolor, e.Graphics);

                    pointF = new PointF(temp_x + 10, y);
                    col_width = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.總異動量.GetEnumName());
                    font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.總異動量.GetEnumName());
                    string 總異動量 = $"{value[(int)enum_取藥堆疊母資料.總異動量].ObjectToString()}";
                    size = 總異動量.MeasureText(font);
                    DrawingClass.Draw.DrawString(e.Graphics, 總異動量, font, new Rectangle((int)pointF.X, (int)pointF.Y, col_width, height), row_Forecolor, DataGridViewContentAlignment.MiddleLeft);
                    temp_x = pointF.X + col_width;

                    pointF = new PointF(temp_x + 10, y);
                    col_width = sQL_DataGridView.Get_ColumnWidth(enum_取藥堆疊母資料.結存量.GetEnumName());
                    font = sQL_DataGridView.Get_ColumnFont(enum_取藥堆疊母資料.結存量.GetEnumName());
                    string 結存量 = $"{value[(int)enum_取藥堆疊母資料.結存量].ObjectToString()}";
                    size = 結存量.MeasureText(font);
                    DrawingClass.Draw.DrawString(e.Graphics, $"({結存量})", font, new Rectangle((int)pointF.X, (int)pointF.Y, col_width, height), row_Forecolor, DataGridViewContentAlignment.MiddleLeft);
                    temp_x = pointF.X + col_width;


                    font = new Font("標楷體", 14, FontStyle.Bold);
                    size = 狀態.MeasureText(font);
                    pointF = new PointF(e.RowBounds.Right - size.Width, e.RowBounds.Bottom - size.Height - 10);
                    DrawingClass.Draw.文字左上繪製($"{狀態}", pointF, font, row_Forecolor, e.Graphics);
                    temp_x = pointF.X + col_width;


                    if (sQL_DataGridView.dataGridView.Rows[e.RowIndex].Selected)
                    {
                        DrawingClass.Draw.方框繪製(new Point(x, y), new Size(width, height), Color.Blue, 2, false, e.Graphics, 1, 1);
                    }
                    else
                    {
                        DrawingClass.Draw.方框繪製(new Point(x, y), new Size(width, height), Color.Black, 1, false, e.Graphics, 1, 1);
                    }
                }
            }
        }
        private void SqL_DataGridView_領藥台_領藥內容_MouseDown(object sender, MouseEventArgs e)
        {
            return;
            if (e.Button == MouseButtons.Right)
            {
                Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new enum_ContextMenuStrip_Main_領藥內容().GetEnumNames());
                dialog_ContextMenuStrip.TitleText = "功能列表";
                if (dialog_ContextMenuStrip.ShowDialog() != DialogResult.Yes) return;
                if (dialog_ContextMenuStrip.Value == enum_ContextMenuStrip_Main_領藥內容.修改數量.GetEnumName())
                {
                    List<object[]> list_value = new List<object[]>();
                    SQL_DataGridView sQL_DataGridView = (SQL_DataGridView)sender;
                    if (sQL_DataGridView.Name == "sqL_DataGridView_領藥台_01_領藥內容") list_value = sqL_DataGridView_領藥台_01_領藥內容.Get_All_Select_RowsValues();
                    if (sQL_DataGridView.Name == "sqL_DataGridView_領藥台_02_領藥內容") list_value = sqL_DataGridView_領藥台_02_領藥內容.Get_All_Select_RowsValues();
                    if (sQL_DataGridView.Name == "sqL_DataGridView_領藥台_03_領藥內容") list_value = sqL_DataGridView_領藥台_03_領藥內容.Get_All_Select_RowsValues();
                    if (sQL_DataGridView.Name == "sqL_DataGridView_領藥台_04_領藥內容") list_value = sqL_DataGridView_領藥台_04_領藥內容.Get_All_Select_RowsValues();
                    if (list_value.Count == 0)
                    {
                        MyMessageBox.ShowDialog("未選取資料");
                        return;
                    }
                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
                    double 數量 = dialog_NumPannel.Value;



                }

            }
        }

        private void Program_調劑作業_Init()
        {
            _sqL_DataGridView_領藥台_01_領藥內容 = this.sqL_DataGridView_領藥台_01_領藥內容;
            _sqL_DataGridView_領藥台_02_領藥內容 = this.sqL_DataGridView_領藥台_02_領藥內容;
            _sqL_DataGridView_領藥台_03_領藥內容 = this.sqL_DataGridView_領藥台_03_領藥內容;
            _sqL_DataGridView_領藥台_04_領藥內容 = this.sqL_DataGridView_領藥台_04_領藥內容;

            Program_調劑作業_領藥台_01_Init();
            Program_調劑作業_領藥台_02_Init();
            Program_調劑作業_領藥台_03_Init();
            Program_調劑作業_領藥台_04_Init();


            _panel_領藥台_01_藥品資訊 = panel_領藥台_01_藥品資訊;
            _panel_領藥台_02_藥品資訊 = panel_領藥台_02_藥品資訊;

            Dialog_使用者登入.myTimerBasic_覆核完成.StartTickTime(1);
            this.plC_RJ_Button_指紋登入.MouseDownEvent += PlC_RJ_Button_指紋登入_MouseDownEvent;
            this.plC_RJ_Button_手輸醫令.MouseDownEvent += PlC_RJ_Button_手輸醫令_MouseDownEvent;
            this.plC_RJ_Button_條碼輸入.MouseDownEvent += PlC_RJ_Button_條碼輸入_MouseDownEvent;
            this.plC_RJ_Button_醫令檢索.MouseDownEvent += PlC_RJ_Button_醫令檢索_MouseDownEvent;
            this.plC_RJ_Button_藥品調入.MouseDownEvent += PlC_RJ_Button_藥品調入_MouseDownEvent;
            this.plC_RJ_Button_交班對點.MouseDownEvent += PlC_RJ_Button_交班對點_MouseDownEvent;
            this.plC_RJ_Button_藥品搜索.MouseDownEvent += PlC_RJ_Button_藥品搜索_MouseDownEvent;
            this.plC_RJ_Button_申領.MouseDownEvent += PlC_RJ_Button_申領_MouseDownEvent;
            this.toolStripMenuItem_調劑畫面_顯示設定.Click += ToolStripMenuItem_調劑畫面_顯示設定_Click;

            //this.MyThread_領藥_RFID = new Basic.MyThread(this.FindForm());
            //this.MyThread_領藥_RFID.Add_Method(this.sub_Program_領藥_RFID);
            //this.MyThread_領藥_RFID.AutoRun(true);
            //this.MyThread_領藥_RFID.AutoStop(false);
            //this.MyThread_領藥_RFID.SetSleepTime(100);
            //this.MyThread_領藥_RFID.Trigger();
            //this.MyThread_領藥_RFID_入出庫資料檢查 = new Basic.MyThread(this.FindForm());
            //this.MyThread_領藥_RFID_入出庫資料檢查.Add_Method(this.sub_Program_領藥_入出庫資料檢查);
            //this.MyThread_領藥_RFID_入出庫資料檢查.AutoRun(true);
            //this.MyThread_領藥_RFID_入出庫資料檢查.AutoStop(false);
            //this.MyThread_領藥_RFID_入出庫資料檢查.SetSleepTime(100);
            //this.MyThread_領藥_RFID_入出庫資料檢查.Trigger();

            this.MyThread_DHT = new Basic.MyThread(this.FindForm());
            this.MyThread_DHT.Add_Method(this.sub_Program_DHT);
            this.MyThread_DHT.AutoRun(true);
            this.MyThread_DHT.AutoStop(false);
            this.MyThread_DHT.SetSleepTime(500);
            this.MyThread_DHT.Trigger();

            this.MyThread_調劑作業 = new Basic.MyThread(this.FindForm());
            this.MyThread_調劑作業.Add_Method(this.sub_Program_調劑作業);
            this.MyThread_調劑作業.AutoRun(true);
            this.MyThread_調劑作業.AutoStop(false);
            this.MyThread_調劑作業.SetSleepTime(10);
            this.MyThread_調劑作業.Trigger();
            


            PLC_Device_領藥台_01_已登入.ValueChangeEvent += PLC_Device_領藥台_01_已登入_ValueChangeEvent;
            PLC_Device_領藥台_02_已登入.ValueChangeEvent += PLC_Device_領藥台_02_已登入_ValueChangeEvent;
            PLC_Device_領藥台_03_已登入.ValueChangeEvent += PLC_Device_領藥台_03_已登入_ValueChangeEvent;
            PLC_Device_領藥台_04_已登入.ValueChangeEvent += PLC_Device_領藥台_04_已登入_ValueChangeEvent;

            this.plC_UI_Init.Add_Method(Program_調劑作業);
        }

        bool flag_調劑作業_頁面更新 = false;
        private void Program_調劑作業()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業")
            {
                if (!this.flag_調劑作業_頁面更新)
                {
                    if (PLC_Device_已登入.Bool) Function_登出();
                    this.flag_調劑作業_頁面更新 = true;
                }
            }
            else
            {

                this.flag_調劑作業_頁面更新 = false;
            }
        }
        private void sub_Program_調劑作業()
        {
           

        }
        private void sub_Program_領藥台_01()
        {
            if (Dialog_藥品搜索.IsShown) return;
            if (PLC_Device_領藥台_01_閒置登出時間.Value != 0)
            {
                if (PLC_Device_領藥台_01_已登入.Bool)
                {
                    if (MyTimer_領藥台_01_閒置登出時間.IsTimeOut())
                    {
                        this.PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    }
                }
                else
                {
                    MyTimer_領藥台_01_閒置登出時間.TickStop();
                    MyTimer_領藥台_01_閒置登出時間.StartTickTime();
                }
            }
            if (PLC_Device_領藥台_01_入賬完成時間.Value != 0)
            {
                if (PLC_Device_領藥台_01_已登入.Bool)
                {
                    if (MyTimer_領藥台_01_入賬完成時間.IsTimeOut())
                    {
                        PlC_RJ_Button_領藥台_01_取消作業_MouseDownEvent(null);
                        MyTimer_領藥台_01_入賬完成時間.TickStop();
                        MyTimer_領藥台_01_入賬完成時間.StartTickTime();
                    }
                }
                else
                {
                    MyTimer_領藥台_01_入賬完成時間.TickStop();
                    MyTimer_領藥台_01_入賬完成時間.StartTickTime();
                }
            }
            if (!this.flag_Program_領藥台_01_換頁)
            {

                this.Invoke(new Action(delegate
                {
                    if (plC_CheckBox_QRcode_Mode.Checked)
                    {
                        plC_RJ_Button_條碼輸入.Visible = false;
                    }
                    else
                    {
                        plC_RJ_Button_條碼輸入.Visible = true;
                    }
                }));
                //this.PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                this.flag_Program_領藥台_01_換頁 = true;
            }

            this.sub_Program_領藥台_01_狀態顯示();
            this.sub_Program_領藥台_01_檢查登入();
            this.sub_Program_領藥台_01_檢查輸入資料();
            this.sub_Program_領藥台_01_刷新領藥內容();
        }
        private void sub_Program_領藥台_02()
        {
            if (Dialog_藥品搜索.IsShown) return;
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" || true)
            {
                this.Invoke(new Action(delegate
                {
                    if (PLC_Device_領藥台_02_閒置登出時間.Value != 0)
                    {
                        if (PLC_Device_領藥台_02_已登入.Bool)
                        {

                            if ((PLC_Device_領藥台_02_閒置登出時間.Value - (int)MyTimer_領藥台_02_閒置登出時間.GetTickTime()) <= 20000)
                            {
                                myTimer_領藥台_02_Logout.StartTickTime(5000);
                                if (myTimer_領藥台_02_Logout.IsTimeOut())
                                {
                                    myTimer_領藥台_02_Logout.TickStop();
                                    if (plC_CheckBox_登出時間到要警示.Checked)
                                    {
                                        Task.Run(new Action(delegate
                                        {
                                            using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\logout.wav"))
                                            {
                                                sp.Stop();
                                                sp.Play();
                                                sp.PlaySync();
                                            }

                                        }));
                                    }
                                }
                            }
                            if (MyTimer_領藥台_02_閒置登出時間.IsTimeOut())
                            {
                                this.PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                            }
                        }
                        else
                        {
                            MyTimer_領藥台_02_閒置登出時間.TickStop();
                            MyTimer_領藥台_02_閒置登出時間.StartTickTime();
                        }
                    }
                    if (PLC_Device_領藥台_02_入賬完成時間.Value != 0)
                    {
                        if (PLC_Device_領藥台_02_已登入.Bool)
                        {

                            if (MyTimer_領藥台_02_入賬完成時間.IsTimeOut())
                            {
                                PlC_RJ_Button_領藥台_02_取消作業_MouseDownEvent(null);
                                MyTimer_領藥台_02_入賬完成時間.TickStop();
                                MyTimer_領藥台_02_入賬完成時間.StartTickTime();
                            }
                        }
                        else
                        {
                            MyTimer_領藥台_02_入賬完成時間.TickStop();
                            MyTimer_領藥台_02_入賬完成時間.StartTickTime();
                        }
                    }
                }));
                if (!this.flag_Program_領藥台_02_換頁)
                {
                    this.Invoke(new Action(delegate
                    {
                        if (plC_CheckBox_QRcode_Mode.Checked)
                        {
                            //plC_RJ_Button_領藥台_02_手輸醫令.Visible = false;
                        }
                        else
                        {
                            //plC_RJ_Button_領藥台_02_手輸醫令.Visible = true;
                        }
                    }));


                    //this.PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    this.flag_Program_領藥台_02_換頁 = true;
                }

            }
            else
            {
                if (this.flag_Program_領藥台_02_換頁)
                {
                    //this.PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    this.flag_Program_領藥台_02_換頁 = false;
                }
            }

            this.sub_Program_領藥台_02_狀態顯示();
            this.sub_Program_領藥台_02_檢查登入();
            this.sub_Program_領藥台_02_檢查輸入資料();
            this.sub_Program_領藥台_02_刷新領藥內容();
        }
        private void sub_Program_領藥台_03()
        {
            if (Dialog_藥品搜索.IsShown) return;
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" || true)
            {
                this.Invoke(new Action(delegate
                {
                    if (PLC_Device_領藥台_03_閒置登出時間.Value != 0)
                    {
                        if (PLC_Device_領藥台_03_已登入.Bool)
                        {

                            if ((PLC_Device_領藥台_03_閒置登出時間.Value - (int)MyTimer_領藥台_03_閒置登出時間.GetTickTime()) <= 20000)
                            {
                                myTimer_領藥台_03_Logout.StartTickTime(5000);
                                if (myTimer_領藥台_03_Logout.IsTimeOut())
                                {
                                    myTimer_領藥台_03_Logout.TickStop();
                                    if (plC_CheckBox_登出時間到要警示.Checked)
                                    {
                                        Task.Run(new Action(delegate
                                        {
                                            using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\logout.wav"))
                                            {
                                                sp.Stop();
                                                sp.Play();
                                                sp.PlaySync();
                                            }

                                        }));
                                    }
                                }
                            }
                            if (MyTimer_領藥台_03_閒置登出時間.IsTimeOut())
                            {
                                this.PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                            }
                        }
                        else
                        {
                            MyTimer_領藥台_03_閒置登出時間.TickStop();
                            MyTimer_領藥台_03_閒置登出時間.StartTickTime();
                        }
                    }
                    if (PLC_Device_領藥台_03_入賬完成時間.Value != 0)
                    {
                        if (PLC_Device_領藥台_03_已登入.Bool)
                        {

                            if (MyTimer_領藥台_03_入賬完成時間.IsTimeOut())
                            {
                                PlC_RJ_Button_領藥台_03_取消作業_MouseDownEvent(null);
                                MyTimer_領藥台_03_入賬完成時間.TickStop();
                                MyTimer_領藥台_03_入賬完成時間.StartTickTime();
                            }
                        }
                        else
                        {
                            MyTimer_領藥台_03_入賬完成時間.TickStop();
                            MyTimer_領藥台_03_入賬完成時間.StartTickTime();
                        }
                    }
                }));
                if (!this.flag_Program_領藥台_03_換頁)
                {
                    this.Invoke(new Action(delegate
                    {
                        if (plC_CheckBox_QRcode_Mode.Checked)
                        {
                            //plC_RJ_Button_領藥台_03_手輸醫令.Visible = false;
                        }
                        else
                        {
                            //plC_RJ_Button_領藥台_03_手輸醫令.Visible = true;
                        }
                    }));
                    //this.PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    this.flag_Program_領藥台_03_換頁 = true;
                }

            }
            else
            {
                if (this.flag_Program_領藥台_03_換頁)
                {
                    //this.PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    this.flag_Program_領藥台_03_換頁 = false;
                }
            }

            this.sub_Program_領藥台_03_狀態顯示();
            this.sub_Program_領藥台_03_檢查登入();
            this.sub_Program_領藥台_03_檢查輸入資料();
            this.sub_Program_領藥台_03_刷新領藥內容();
        }
        private void sub_Program_領藥台_04()
        {
            if (Dialog_藥品搜索.IsShown) return;
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" || true)
            {
                this.Invoke(new Action(delegate
                {
                    if (PLC_Device_領藥台_04_閒置登出時間.Value != 0)
                    {
                        if (PLC_Device_領藥台_04_已登入.Bool)
                        {

                            if ((PLC_Device_領藥台_04_閒置登出時間.Value - (int)MyTimer_領藥台_04_閒置登出時間.GetTickTime()) <= 20000)
                            {
                                myTimer_領藥台_04_Logout.StartTickTime(5000);
                                if (myTimer_領藥台_04_Logout.IsTimeOut())
                                {
                                    myTimer_領藥台_04_Logout.TickStop();
                                    if (plC_CheckBox_登出時間到要警示.Checked)
                                    {
                                        Task.Run(new Action(delegate
                                        {
                                            using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\logout.wav"))
                                            {
                                                sp.Stop();
                                                sp.Play();
                                                sp.PlaySync();
                                            }

                                        }));
                                    }
                                }
                            }
                            if (MyTimer_領藥台_04_閒置登出時間.IsTimeOut())
                            {
                                this.PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                            }
                        }
                        else
                        {
                            MyTimer_領藥台_04_閒置登出時間.TickStop();
                            MyTimer_領藥台_04_閒置登出時間.StartTickTime();
                        }
                    }
                    if (PLC_Device_領藥台_04_入賬完成時間.Value != 0)
                    {
                        if (PLC_Device_領藥台_04_已登入.Bool)
                        {

                            if (MyTimer_領藥台_04_入賬完成時間.IsTimeOut())
                            {
                                PlC_RJ_Button_領藥台_04_取消作業_MouseDownEvent(null);
                                MyTimer_領藥台_04_入賬完成時間.TickStop();
                                MyTimer_領藥台_04_入賬完成時間.StartTickTime();
                            }
                        }
                        else
                        {
                            MyTimer_領藥台_04_入賬完成時間.TickStop();
                            MyTimer_領藥台_04_入賬完成時間.StartTickTime();
                        }
                    }
                }));
                if (!this.flag_Program_領藥台_04_換頁)
                {
                    this.Invoke(new Action(delegate
                    {
                        if (plC_CheckBox_QRcode_Mode.Checked)
                        {
                            //plC_RJ_Button_領藥台_04_手輸醫令.Visible = false;
                        }
                        else
                        {
                            //plC_RJ_Button_領藥台_04_手輸醫令.Visible = true;
                        }
                    }));

                    //this.PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    this.flag_Program_領藥台_04_換頁 = true;
                }

            }
            else
            {
                if (this.flag_Program_領藥台_04_換頁)
                {
                    //this.PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    this.flag_Program_領藥台_04_換頁 = false;
                }
            }

            this.sub_Program_領藥台_04_狀態顯示();
            this.sub_Program_領藥台_04_檢查登入();
            this.sub_Program_領藥台_04_檢查輸入資料();
            this.sub_Program_領藥台_04_刷新領藥內容();
        }

        private void sub_Program_領藥_RFID()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業")
            {

                if (flag_Program_領藥_RFID_換頁)
                {
                    flag_Program_領藥_RFID_換頁 = false;
                }
            }
            else
            {
                flag_Program_領藥_RFID_換頁 = true;
            }
            this.sub_Program_領藥_RFID_檢查刷卡();
        }

        private void sub_Program_DHT()
        {
            StorageUI_EPD_266.UDP_READ uDP_READ_266 = this.storageUI_EPD_266.Get_UDP_READ("192.168.0.50");
            bool flag_ping = Basic.Net.Ping("192.168.48.21", 2, 100);
            if (uDP_READ_266 != null)
            {
                this.Invoke(new Action(delegate 
                {
                    if (this.panel_DHT.Visible == false)
                    {
                        this.panel_DHT.Visible = true;
                    }
                  

                    label_溫度.Text = $"{uDP_READ_266.dht_t.ToString("0.00")}°C";
                    label_濕度.Text = $"{uDP_READ_266.dht_h.ToString("0.00")} %";
                }));
                MyTimerBasic_dht_timeout.TickStop();
                MyTimerBasic_dht_timeout.StartTickTime(3000);
            }
            if (this.panel_DHT.Visible == true)
            {
                this.Invoke(new Action(delegate
                {
                    this.panel_DHT.BackColor = flag_ping ? Color.White : Color.Yellow;
                }));
            }

        }

        #region 領藥台_01
        MyTimer MyTimer_領藥台_01_閒置登出時間 = new MyTimer("D100");
        PLC_Device PLC_Device_領藥台_01_閒置登出時間 = new PLC_Device("D100");
        MyTimer MyTimer_領藥台_01_入賬完成時間 = new MyTimer("D101");
        PLC_Device PLC_Device_領藥台_01_入賬完成時間 = new PLC_Device("D101");
        int 領藥台_01_RFID站號 = 1;
        List<object> 領藥台_01_儲位 = new List<object>();
        List<object[]> 領藥台_01_領藥儲位資訊 = new List<object[]>();
        List<string[]> 領藥台_01_掃描BUFFER = new List<string[]>();
        PLC_Device PLC_Device_領藥台_01_登出 = new PLC_Device();
        FpMatchLib.FpMatchClass FpMatchClass_領藥台_01_指紋資訊;
        PLC_Device PLC_Device_領藥台_01_已登入 = new PLC_Device("S100");

        PLC_Device PLC_Device_領藥台_01_單醫令模式 = new PLC_Device("S110");

        PLC_Device PLC_Device_領藥台_01_狀態顯示_等待登入 = new PLC_Device("M4000");
        PLC_Device PLC_Device_領藥台_01_狀態顯示_登入者姓名 = new PLC_Device("M4001");

        public static string 領藥台_01_登入者姓名 = "";
        public static string 領藥台_01_藥師證字號 = "";
        public static string 領藥台_01_ID = "";
        public static string 領藥台_01_卡號 = "";
        public static string _領藥台_01_顏色 = "";
        public string 領藥台_01_顏色
        {
            get
            {
                if (plC_CheckBox_掃碼顏色固定.Checked)
                {
                    this.Invoke(new Action(delegate
                    {
                        Color color = this.panel_工程模式_領藥台_01_顏色.BackColor;
                        if (color == Color.Black)
                        {
                            this.panel_工程模式_領藥台_01_顏色.BackColor = Color.Blue;
                            SaveConfig工程模式();
                        }
                        _領藥台_01_顏色 = color.ToColorString();
                    }));


                    return _領藥台_01_顏色;
                }
                else
                {
                    return _領藥台_01_顏色;
                }
            }
            set
            {
                _領藥台_01_顏色 = value;
            }
        }
        public static string 領藥台_01_一維碼 = "";


        private string 領藥台_01_醫令條碼 = "";

        #region PLC_領藥台_01_狀態顯示


        PLC_Device PLC_Device_領藥台_01_狀態顯示 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_01_狀態顯示_OK = new PLC_Device("");
        MyTimer MyTimer_領藥台_01_狀態顯示_結束延遲 = new MyTimer();
        int cnt_Program_領藥台_01_狀態顯示 = 65534;
        void sub_Program_領藥台_01_狀態顯示()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業")
            {
                PLC_Device_領藥台_01_狀態顯示.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_01_狀態顯示.Bool = false;
            }
            if (cnt_Program_領藥台_01_狀態顯示 == 65534)
            {
                this.MyTimer_領藥台_01_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_01_狀態顯示.SetComment("PLC_領藥台_01_狀態顯示");
                PLC_Device_領藥台_01_狀態顯示_OK.SetComment("PLC_領藥台_01_狀態顯示_OK");
                PLC_Device_領藥台_01_狀態顯示.Bool = false;
                cnt_Program_領藥台_01_狀態顯示 = 65535;
            }
            if (cnt_Program_領藥台_01_狀態顯示 == 65535) cnt_Program_領藥台_01_狀態顯示 = 1;
            if (cnt_Program_領藥台_01_狀態顯示 == 1) cnt_Program_領藥台_01_狀態顯示_檢查按下(ref cnt_Program_領藥台_01_狀態顯示);
            if (cnt_Program_領藥台_01_狀態顯示 == 2) cnt_Program_領藥台_01_狀態顯示_初始化(ref cnt_Program_領藥台_01_狀態顯示);
            if (cnt_Program_領藥台_01_狀態顯示 == 3) cnt_Program_領藥台_01_狀態顯示 = 65500;
            if (cnt_Program_領藥台_01_狀態顯示 > 1) cnt_Program_領藥台_01_狀態顯示_檢查放開(ref cnt_Program_領藥台_01_狀態顯示);

            if (cnt_Program_領藥台_01_狀態顯示 == 65500)
            {
                this.MyTimer_領藥台_01_狀態顯示_結束延遲.TickStop();
                this.MyTimer_領藥台_01_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_01_狀態顯示.Bool = false;
                PLC_Device_領藥台_01_狀態顯示_OK.Bool = false;
                cnt_Program_領藥台_01_狀態顯示 = 65535;
            }
        }



        void cnt_Program_領藥台_01_狀態顯示_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_01_狀態顯示.Bool) cnt++;
        }
        void cnt_Program_領藥台_01_狀態顯示_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_01_狀態顯示.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_01_狀態顯示_初始化(ref int cnt)
        {
            if (!this.PLC_Device_領藥台_01_已登入.Bool)
            {
                PLC_Device_領藥台_01_狀態顯示_等待登入.Bool = true;
                PLC_Device_領藥台_01_狀態顯示_登入者姓名.Bool = false;
            }
            else
            {
                PLC_Device_領藥台_01_狀態顯示_等待登入.Bool = false;
                PLC_Device_領藥台_01_狀態顯示_登入者姓名.Bool = true;
            }
            cnt++;
        }

        private void PLC_Device_領藥台_01_已登入_ValueChangeEvent(object Value)
        {
            if ((bool)Value == true)
            {
                this.Invoke(new Action(delegate
                {
                    this.panel_Main.Collapse();
                }));
            }
        }
        #endregion

        #region PLC_領藥台_01_檢查登入

        PLC_Device PLC_Device_領藥台_01_檢查登入 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_01_檢查登入_OK = new PLC_Device("");


        int cnt_Program_領藥台_01_檢查登入 = 65534;
        void sub_Program_領藥台_01_檢查登入()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業") PLC_Device_領藥台_01_檢查登入.Bool = true;
            else PLC_Device_領藥台_01_檢查登入.Bool = false;
            if (cnt_Program_領藥台_01_檢查登入 == 65534)
            {
                PLC_Device_領藥台_01_檢查登入.SetComment("PLC_領藥台_01_檢查登入");
                PLC_Device_領藥台_01_檢查登入_OK.SetComment("PLC_Device_領藥台_01_檢查登入_OK");
                PLC_Device_領藥台_01_檢查登入.Bool = false;
                PLC_Device_領藥台_01_已登入.Bool = false;
                cnt_Program_領藥台_01_檢查登入 = 65535;
            }
            if (cnt_Program_領藥台_01_檢查登入 == 65535) cnt_Program_領藥台_01_檢查登入 = 1;
            if (cnt_Program_領藥台_01_檢查登入 == 1) cnt_Program_領藥台_01_檢查登入_檢查按下(ref cnt_Program_領藥台_01_檢查登入);
            if (cnt_Program_領藥台_01_檢查登入 == 2) cnt_Program_領藥台_01_檢查登入_初始化(ref cnt_Program_領藥台_01_檢查登入);
            if (cnt_Program_領藥台_01_檢查登入 == 3) cnt_Program_領藥台_01_檢查登入_外部設備資料或帳號密碼登入(ref cnt_Program_領藥台_01_檢查登入);
            if (cnt_Program_領藥台_01_檢查登入 == 4) cnt_Program_領藥台_01_檢查登入 = 65500;
            if (cnt_Program_領藥台_01_檢查登入 > 1) cnt_Program_領藥台_01_檢查登入_檢查放開(ref cnt_Program_領藥台_01_檢查登入);

            if (cnt_Program_領藥台_01_檢查登入 == 65500)
            {
                PLC_Device_領藥台_01_檢查登入.Bool = false;
                cnt_Program_領藥台_01_檢查登入 = 65535;
            }
        }
        void cnt_Program_領藥台_01_檢查登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_01_檢查登入.Bool) cnt++;
        }
        void cnt_Program_領藥台_01_檢查登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_01_檢查登入.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_01_檢查登入_初始化(ref int cnt)
        {
            PLC_Device_領藥台_01_檢查登入_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_01_檢查登入_外部設備資料或帳號密碼登入(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (Dialog_使用者登入.IsShown)
            {
                if (MySerialPort_Scanner01.ReadByte() != null)
                {
                    System.Threading.Thread.Sleep(50);
                    string text = MySerialPort_Scanner01.ReadString();
                    MySerialPort_Scanner01.ClearReadByte();
                    if (text == null) return;
                    text = text.Replace("\0", "");
                    if (text.Length <= 2 || text.Length > 30) return;
                    if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                    領藥台_01_一維碼 = text;
                }
                return;
            }
            string UID = this.rfiD_FX600_UI.Get_RFID_UID(領藥台_01_RFID站號);
            if (!UID.StringIsEmpty() && UID.StringToInt32() != 0 && Dialog_使用者登入.myTimerBasic_覆核完成.IsTimeOut())
            {
                Console.WriteLine($"成功讀取RFID  {UID}");
                領藥台_01_卡號 = UID;
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.卡號.GetEnumName(), 領藥台_01_卡號, false);
                if (list_人員資料.Count == 0) return;
                Console.WriteLine($"取得人員資料完成!");
                if (!PLC_Device_領藥台_01_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_01_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_01_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                    }));
                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_01_登入者姓名, "01.號使用者");

                }
                else
                {
                    if (領藥台_01_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_01_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_01_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_01_登入者姓名, "01.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (MySerialPort_Scanner01.ReadByte() != null && !PLC_Device_領藥台_01_已登入.Bool)
            {
                System.Threading.Thread.Sleep(50);
                string text = MySerialPort_Scanner01.ReadString();
                MySerialPort_Scanner01.ClearReadByte();
                if (text == null) return;
                text = text.Replace("\0", "");
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r", "");
                text = text.Replace("\n", "");

                領藥台_01_一維碼 = text;
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), 領藥台_01_一維碼, false);
                if (list_人員資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\查無此一維碼.wav");
                    return;
                }
                if (!PLC_Device_領藥台_01_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_01_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_01_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_01_登入者姓名, "01.號使用者");
                }
                else
                {
                    if (領藥台_01_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_01_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_01_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_01_登入者姓名, "01.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (VoiceSample != null && !PLC_Device_領藥台_01_已登入.Bool)
            {

                領藥台_01_登入者姓名 = VoiceSample.name;
                VoiceSample = null;
                if (領藥台_01_登入者姓名.StringIsEmpty()) return;
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.姓名.GetEnumName(), 領藥台_01_登入者姓名, false);
                if (list_人員資料.Count == 0)
                {
                    Voice voice = new Voice();
                    voice.SpeakOnTask($"{領藥台_01_登入者姓名},聲紋辨識登入成功");
                    return;
                }
                if (!PLC_Device_領藥台_01_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_01_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_01_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.密碼登入, 領藥台_01_登入者姓名, "01.號使用者");
                }
                else
                {
                    if (領藥台_01_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_01_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_01_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_01_登入者姓名, "01.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (FpMatchClass_領藥台_01_指紋資訊 != null)
            {
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
                object[] value = null;
                for (int i = 0; i < list_人員資料.Count; i++)
                {
                    string feature = list_人員資料[i][(int)enum_人員資料.指紋辨識].ObjectToString();
                    if (fpMatchSoket.Match(FpMatchClass_領藥台_01_指紋資訊.feature, feature))
                    {
                        value = list_人員資料[i];
                        break;
                    }
                }
                FpMatchClass_領藥台_01_指紋資訊 = null;
                if (value == null)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找無符合指紋資訊", 2000);
                    dialog_AlarmForm.ShowDialog();
                    cnt = 65500;
                    return;
                }
                if (!PLC_Device_領藥台_01_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_01_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_01_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_01_登入者姓名, "01.號使用者");
                }
                else
                {
                    if (領藥台_01_ID != value[(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_01_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_01_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_01_登入者姓名, "01.號使用者");
                    }
                }
            }
            cnt = 65500;
            return;


        }

        #endregion
        #region PLC_領藥台_01_檢查輸入資料

        PLC_Device PLC_Device_領藥台_01_檢查輸入資料 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_01_檢查輸入資料_OK = new PLC_Device("");

        MyTimer MyTimer_領藥台_01_檢查輸入資料_NG訊息延遲 = new MyTimer();
        int cnt_Program_領藥台_01_檢查輸入資料 = 65534;
        void sub_Program_領藥台_01_檢查輸入資料()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" && PLC_Device_領藥台_01_已登入.Bool)
            {
                PLC_Device_領藥台_01_檢查輸入資料.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_01_檢查輸入資料.Bool = false;
            }
            if (Dialog_使用者登入.IsShown)
            {
                PLC_Device_領藥台_01_檢查輸入資料.Bool = false;
            }
            if (cnt_Program_領藥台_01_檢查輸入資料 == 65534)
            {
                PLC_Device_領藥台_01_檢查輸入資料.SetComment("PLC_領藥台_01_檢查輸入資料");
                PLC_Device_領藥台_01_檢查輸入資料_OK.SetComment("PLC_Device_領藥台_01_檢查輸入資料_OK");
                PLC_Device_領藥台_01_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_01_檢查輸入資料 = 65535;
            }
            if (cnt_Program_領藥台_01_檢查輸入資料 == 65535) cnt_Program_領藥台_01_檢查輸入資料 = 1;
            if (cnt_Program_領藥台_01_檢查輸入資料 == 1) cnt_Program_領藥台_01_檢查輸入資料_檢查按下(ref cnt_Program_領藥台_01_檢查輸入資料);
            if (cnt_Program_領藥台_01_檢查輸入資料 == 2) cnt_Program_領藥台_01_檢查輸入資料_初始化(ref cnt_Program_領藥台_01_檢查輸入資料);
            if (cnt_Program_領藥台_01_檢查輸入資料 == 3) cnt_Program_領藥台_01_檢查輸入資料_設定開始掃描(ref cnt_Program_領藥台_01_檢查輸入資料);
            if (cnt_Program_領藥台_01_檢查輸入資料 == 4) cnt_Program_領藥台_01_檢查輸入資料_等待掃描結束(ref cnt_Program_領藥台_01_檢查輸入資料);
            if (cnt_Program_領藥台_01_檢查輸入資料 == 5) cnt_Program_領藥台_01_檢查輸入資料_檢查醫令資料及寫入(ref cnt_Program_領藥台_01_檢查輸入資料);
            if (cnt_Program_領藥台_01_檢查輸入資料 == 6) cnt_Program_領藥台_01_檢查輸入資料 = 65500;


            if (cnt_Program_領藥台_01_檢查輸入資料 > 1) cnt_Program_領藥台_01_檢查輸入資料_檢查放開(ref cnt_Program_領藥台_01_檢查輸入資料);

            if (cnt_Program_領藥台_01_檢查輸入資料 == 65500)
            {
                PLC_Device_領藥台_01_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_01_檢查輸入資料 = 65535;
            }
        }
        void cnt_Program_領藥台_01_檢查輸入資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_01_檢查輸入資料.Bool) cnt++;
        }
        void cnt_Program_領藥台_01_檢查輸入資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_01_檢查輸入資料.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_01_檢查輸入資料_初始化(ref int cnt)
        {
            //PLC_Device_Scanner01_讀取藥單資料.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_01_檢查輸入資料_設定開始掃描(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner01_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner01_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner01_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
            else
            {
                if (!PLC_Device_Scanner01_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner01_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner01_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_01_檢查輸入資料_等待掃描結束(ref int cnt)
        {
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner01_讀取藥單資料.Bool || PLC_Device_Scanner01_讀取藥單資料_OK.Bool)
                {
                    if (PLC_Device_Scanner01_讀取藥單資料_OK.Bool)
                    {
                        cnt++;
                        return;
                    }
                    else
                    {
                        this.voice.SpeakOnTask("掃碼失敗");
                        cnt = 65500;
                        return;
                    }
                }
            }
            else
            {
                if (!PLC_Device_Scanner01_讀取藥單資料.Bool || PLC_Device_Scanner01_讀取藥單資料_OK.Bool)
                {
                    if (PLC_Device_領藥台_01_已登入.Bool)
                    {
                        if (領藥台_01_醫令條碼.Length < 15)
                        {
                            string text = 領藥台_01_醫令條碼.Replace("\n", "");
                            text = text.Replace("\r", "");
                            if (text.StringIsEmpty())
                            {
                                cnt = 65500;
                                return;
                            }
                            Console.WriteLine($"{text}");
                            if (text == QR_Code_醫令模式切換)
                            {
                                PLC_Device_領藥台_01_單醫令模式.Bool = !PLC_Device_領藥台_01_單醫令模式.Bool;

                                string text_temp = PLC_Device_領藥台_01_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                                Console.WriteLine($"切換模式至{text_temp}");
                                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"切換模式至{text_temp}", 1000, 0, 0);
                                this.Invoke(new Action(delegate
                                {

                                    rJ_Lable_領藥台_01_Title.Text = $"    01. [{領藥台_01_登入者姓名}] {text_temp}";
                                    this.rJ_Lable_領藥台_01_Title.BorderColor = this.panel_工程模式_領藥台_01_顏色.BackColor;
                                    this.rJ_Lable_領藥台_01_Title.BackgroundColor = Color.GreenYellow;
                                    this.rJ_Lable_領藥台_01_Title.TextColor = Color.Black;
                                }));
                                dialog_AlarmForm.ShowDialog();
                                cnt = 65500;
                                return;
                            }
                            if (text.StringIsEmpty())
                            {
                                cnt = 65500;
                                return;
                            }
                            List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), text, false);
                            if (list_人員資料.Count > 0)
                            {
                                if (領藥台_01_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                                {
                                    this.PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(null);

                                    this.Invoke(new Action(delegate
                                    {
                                        textBox_領藥台_01_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                                        textBox_領藥台_01_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                                        this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(null);
                                    }));
                                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_01_登入者姓名, "01.號使用者");

                                }
                                cnt = 65500;
                                return;
                            }
                        }

                    }


                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_01_檢查輸入資料_檢查醫令資料及寫入(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (plC_Button_領藥台_01_領.Bool)
                {
                    this.Function_領藥台_01_QRCode領藥(this.Scanner01_讀取藥單資料_Array);
                }
                else if (plC_Button_領藥台_01_退.Bool)
                {
                    this.Function_領藥台_01_QRCode退藥(this.Scanner01_讀取藥單資料_Array);
                }
                cnt++;
            }
            else
            {
                if (plC_Button_領藥台_01_領.Bool)
                {
                    this.Function_領藥台_01_醫令領藥(領藥台_01_醫令條碼);
                }
                else if (plC_Button_領藥台_01_退.Bool)
                {
                    this.Function_領藥台_01_醫令退藥(領藥台_01_醫令條碼);
                }
                cnt++;
            }

        }



        #endregion
        #region PLC_領藥台_01_刷新領藥內容
        PLC_Device PLC_Device_領藥台_01_刷新領藥內容 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_01_刷新領藥內容_OK = new PLC_Device("");
        MyTimer MyTimer__領藥台_01_刷新領藥內容_刷新間隔 = new MyTimer();
        int cnt_Program_領藥台_01_刷新領藥內容 = 65534;
        void sub_Program_領藥台_01_刷新領藥內容()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業")
            {
                PLC_Device_領藥台_01_刷新領藥內容.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_01_刷新領藥內容.Bool = false;
            }
            PLC_Device_領藥台_01_刷新領藥內容.Bool = (this.plC_ScreenPage_Main.PageText == "調劑作業");
            if (cnt_Program_領藥台_01_刷新領藥內容 == 65534)
            {
                PLC_Device_領藥台_01_刷新領藥內容.SetComment("PLC_領藥台_01_刷新領藥內容");
                PLC_Device_領藥台_01_刷新領藥內容_OK.SetComment("PLC_領藥台_01_刷新領藥內容_OK");
                PLC_Device_領藥台_01_刷新領藥內容.Bool = false;
                cnt_Program_領藥台_01_刷新領藥內容 = 65535;
            }
            if (cnt_Program_領藥台_01_刷新領藥內容 == 65535) cnt_Program_領藥台_01_刷新領藥內容 = 1;
            if (cnt_Program_領藥台_01_刷新領藥內容 == 1) cnt_Program_領藥台_01_刷新領藥內容_檢查按下(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 2) cnt_Program_領藥台_01_刷新領藥內容_初始化(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 3) cnt_Program_領藥台_01_刷新領藥內容_取得資料(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 4) cnt_Program_領藥台_01_刷新領藥內容_檢查雙人覆核(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 5) cnt_Program_領藥台_01_刷新領藥內容_檢查盲盤作業(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 6) cnt_Program_領藥台_01_刷新領藥內容_檢查複盤作業(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 7) cnt_Program_領藥台_01_刷新領藥內容_檢查作業完成(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 8) cnt_Program_領藥台_01_刷新領藥內容_檢查是否需輸入效期(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 9) cnt_Program_領藥台_01_刷新領藥內容_檢查是否需選擇效期(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 10) cnt_Program_領藥台_01_刷新領藥內容_檢查自動登出(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 11) cnt_Program_領藥台_01_刷新領藥內容_等待刷新間隔(ref cnt_Program_領藥台_01_刷新領藥內容);
            if (cnt_Program_領藥台_01_刷新領藥內容 == 12) cnt_Program_領藥台_01_刷新領藥內容 = 65500;
            if (cnt_Program_領藥台_01_刷新領藥內容 > 1) cnt_Program_領藥台_01_刷新領藥內容_檢查放開(ref cnt_Program_領藥台_01_刷新領藥內容);

            if (cnt_Program_領藥台_01_刷新領藥內容 == 65500)
            {
                PLC_Device_領藥台_01_刷新領藥內容.Bool = false;
                PLC_Device_領藥台_01_刷新領藥內容_OK.Bool = false;
                cnt_Program_領藥台_01_刷新領藥內容 = 65535;
            }
        }
        void cnt_Program_領藥台_01_刷新領藥內容_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_01_刷新領藥內容.Bool) cnt++;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_01_刷新領藥內容.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_取得資料(ref int cnt)
        {

            List<object[]> list_value = new List<object[]>();
            List<object[]> list_取藥堆疊資料 = new List<object[]>();
            List<object[]> list_取藥堆疊資料_buf = new List<object[]>();
            List<object[]> list_取藥堆疊資料_add = new List<object[]>();
            if (myConfigClass.系統取藥模式) list_取藥堆疊資料 = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            else list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
            List<object[]> list_取藥堆疊資料_replace = new List<object[]>();
            string GUID = "";
            string 序號 = "";
            string 動作 = "";
            string 藥袋序號 = "";
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 病歷號 = "";
            string 操作時間 = "";
            string 開方時間 = "";
            string 庫存量 = "";
            string 總異動量 = "";
            string 結存量 = "";
            string 單位 = "";
            string 狀態 = "";
            string 床號 = "";
            string 盤點量 = "";
            list_取藥堆疊資料.Sort(new Icp_取藥堆疊母資料_index排序());

            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示))
                {
                    this.voice.SpeakOnTask("庫存不足");
                    this.Function_取藥堆疊資料_設定作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示, false);
                    list_取藥堆疊資料_replace.Add(list_取藥堆疊資料[i]);
                }
                GUID = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                序號 = (i + 1).ToString();
                動作 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.動作].ObjectToString();
                藥袋序號 = $"{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥袋序號].ObjectToString()}:{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.頻次].ObjectToString()}";
                藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                病歷號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
                操作時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString();
                開方時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
                庫存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.庫存量].ObjectToString();
                總異動量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString();
                結存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                單位 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.單位].ObjectToString();
                狀態 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString();
                床號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.床號].ObjectToString();
                盤點量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.盤點量].ObjectToString();
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.盲盤))
                {
                    庫存量 = "無";
                    結存量 = "無";
                }
                object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                value[(int)enum_取藥堆疊母資料.GUID] = GUID;
                value[(int)enum_取藥堆疊母資料.序號] = 序號;
                value[(int)enum_取藥堆疊母資料.動作] = 動作;
                value[(int)enum_取藥堆疊母資料.藥袋序號] = 藥袋序號;
                value[(int)enum_取藥堆疊母資料.藥品碼] = 藥品碼;
                value[(int)enum_取藥堆疊母資料.藥品名稱] = 藥品名稱;
                value[(int)enum_取藥堆疊母資料.病歷號] = 病歷號;
                value[(int)enum_取藥堆疊母資料.操作時間] = 操作時間;
                value[(int)enum_取藥堆疊母資料.開方時間] = 開方時間;
                value[(int)enum_取藥堆疊母資料.庫存量] = 庫存量;
                value[(int)enum_取藥堆疊母資料.總異動量] = 總異動量;
                value[(int)enum_取藥堆疊母資料.結存量] = 結存量;
                value[(int)enum_取藥堆疊母資料.單位] = 單位;
                value[(int)enum_取藥堆疊母資料.狀態] = 狀態;
                value[(int)enum_取藥堆疊母資料.床號] = 床號;
                value[(int)enum_取藥堆疊母資料.盤點量] = 盤點量;

                list_value.Add(value);


            }

            if (plC_CheckBox_調劑畫面合併相同藥品.Bool)
            {
                List<object[]> list_value_new = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();
                string[] 藥品碼_array = (from value in list_value
                                      select value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString()).Distinct().ToList().ToArray();
                for (int i = 0; i < 藥品碼_array.Length; i++)
                {
                    list_value_buf = (from value in list_value
                                      where value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼_array[i]
                                      select value).ToList();
                    list_value_buf.Sort(new ICP_取藥堆疊母資料());
                    if (list_value_buf.Count == 0) continue;
                    object[] value_領藥內容 = new object[new enum_取藥堆疊母資料().GetLength()];
                    value_領藥內容[(int)enum_取藥堆疊母資料.GUID] = list_value_buf[0][(int)enum_取藥堆疊母資料.GUID];
                    value_領藥內容[(int)enum_取藥堆疊母資料.序號] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.動作] = 動作;
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥袋序號] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥袋序號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品碼] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品碼];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品名稱] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品名稱];
                    value_領藥內容[(int)enum_取藥堆疊母資料.病歷號] = list_value_buf[0][(int)enum_取藥堆疊母資料.病歷號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.操作時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.操作時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.開方時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.開方時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.庫存量] = list_value_buf[list_value_buf.Count - 1][(int)enum_取藥堆疊母資料.庫存量].ObjectToString();
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.結存量] = list_value_buf[list_value_buf.Count - 1][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                    value_領藥內容[(int)enum_取藥堆疊母資料.單位] = list_value_buf[0][(int)enum_取藥堆疊母資料.單位];
                    value_領藥內容[(int)enum_取藥堆疊母資料.床號] = list_value_buf[0][(int)enum_取藥堆疊母資料.床號];
                    double 總異動量_temp = 0;
                    bool flag_入賬完成 = true;
                    bool flag_無儲位 = false;
                    bool flag_庫存不足 = false;
                    bool flag_已領用過 = false;
                    for (int k = 0; k < list_value_buf.Count; k++)
                    {
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                        {
                            flag_入賬完成 = false;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                        {
                            flag_無儲位 = true;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                        {
                            flag_庫存不足 = true;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.已領用過.GetEnumName())
                        {
                            flag_已領用過 = true;
                        }
                        總異動量_temp += list_value_buf[k][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();
                    }
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = 總異動量_temp;
                    if (flag_入賬完成)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName();
                    }
                    else if (flag_無儲位)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    }
                    else if (flag_庫存不足)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName();
                    }
                    else if (flag_已領用過)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.已領用過.GetEnumName();
                    }
                    else
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                    }
                    list_value_new.Add(value_領藥內容);

                }
                for (int i = 0; i < list_value_new.Count; i++)
                {
                    list_value_new[i][(int)enum_取藥堆疊母資料.序號] = (i + 1).ToString();
                    藥品碼 = list_value_new[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    double 庫存量_temp = Function_從SQL取得庫存(藥品碼);
                    double 結存量_temp = 庫存量_temp + list_value_new[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();
                    //list_value_new[i][(int)enum_取藥堆疊母資料.庫存量] = 庫存量_temp.ToString();
                    //list_value_new[i][(int)enum_取藥堆疊母資料.結存量] = 結存量_temp.ToString();
                }
                list_value = list_value_new;
            }
            try
            {
                for (int i = 0; i < list_取藥堆疊資料.Count; i++)
                {
                    list_取藥堆疊資料_buf = this.sqL_DataGridView_領藥台_01_領藥內容.GetRows("GUID", list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString(), false);
                    if (list_取藥堆疊資料_buf.Count == 0)
                    {
                        list_取藥堆疊資料_add.Add(list_取藥堆疊資料[i]);
                    }
                }
                if (list_取藥堆疊資料_add.Count > 0)
                {
                    string 藥碼 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    string 藥名 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                    string 領藥號 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.領藥號].ObjectToString();
                    病歷號 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
                    開方時間 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
                    Function_調劑作業_醫令資訊更新(藥碼, 藥名, 領藥號, 病歷號, 開方時間, 1);
                }
            }
            catch
            {

            }

            this.sqL_DataGridView_領藥台_01_領藥內容.RefreshGrid(list_value);
            Application.DoEvents();
            if (list_取藥堆疊資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_檢查雙人覆核(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                Application.DoEvents();
                Dialog_使用者登入 dialog_使用者登入 = new Dialog_使用者登入(領藥台_01_ID, 藥名);
                this.Invoke(new Action(delegate
                {

                    //dialog_使用者登入.Location = new Point(this.rJ_Lable_領藥台_01_Title.PointToScreen(Point.Empty).X + this.rJ_Lable_領藥台_01_Title.Width + 20, 1);
                }));

                if (dialog_使用者登入.ShowDialog() != DialogResult.Yes)
                {
                    list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料[i]);
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                    Fuction_領藥台_01_時間重置();
                    continue;
                }
                Fuction_領藥台_01_時間重置();
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師姓名] = dialog_使用者登入.UserName;
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師ID] = dialog_使用者登入.UserID;
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
        void cnt_Program_領藥台_01_刷新領藥內容_檢查盲盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                int retry = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 總異動量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                double 結存量 = 0;
                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入盲盤數量.wav"); ;
                while (true)
                {
                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(盲盤)請輸入取藥後盤點數量\n交易量 : {總異動量}", $"藥碼:{藥碼} \n藥名:{藥名} ");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;

                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_01_時間重置();
                        break;
                    }
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    double 庫存量 = Function_從SQL取得庫存(藥碼);
                    double 差異值 = medRecheckLogClass.get_unresolved_qty_by_code(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, 藥碼);
                    結存量 = 庫存量 + 總異動量 + 差異值;
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量] = 結存量;
                    if (結存量 == dialog_NumPannel.Value)
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\盲盤數量錯誤.wav");
                    if (retry == 0)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("請再次覆盤", 2000);
                        dialog_錯誤提示.ShowDialog();
                    }
                    if (retry == 1)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"異常紀錄,盤點值 : {dialog_NumPannel.Value}", 2000);
                        dialog_錯誤提示.ShowDialog();
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = "盤點異常";
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    try_error++;
                    retry++;
                }
                Fuction_領藥台_01_時間重置();
            }
            if (list_取藥堆疊母資料_replace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
                this.sqL_DataGridView_取藥堆疊母資料.ReplaceExtra(list_取藥堆疊母資料, true);
                cnt = 1;
                return;
            }
            cnt++;

        }
        void cnt_Program_領藥台_01_刷新領藥內容_檢查複盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                string 結存量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                double 總異動量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();

                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入複盤數量.wav");
                while (true)
                {
                    if (try_error == 1)
                    {
                        Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                        if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                        {
                            Fuction_領藥台_01_時間重置();
                            try_error = 0;
                        }
                        else
                        {
                            Fuction_領藥台_01_時間重置();
                            try_error++;
                        }
                        continue;
                    }
                    if (try_error == 2)
                    {
                        Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                        dialog_收支原因選擇.Title = $"複盤數量錯誤({結存量}) 選擇原因";
                        dialog_收支原因選擇.ShowDialog();
                        Fuction_領藥台_01_時間重置();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n複盤錯誤原因:{dialog_收支原因選擇.Value}";
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(明盤)請輸入取藥後盤點數量\n交易量 : {總異動量}", $"藥碼:{藥碼} \n藥名:{藥名}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;
                    this.Invoke(new Action(delegate
                    {
                        //dialog_NumPannel.Location = new Point(this.rJ_Lable_領藥台_01_Title.PointToScreen(Point.Empty).X + this.rJ_Lable_領藥台_01_Title.Width + 20, 1);
                    }));
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_01_時間重置();
                        break;
                    }
                    Fuction_領藥台_01_時間重置();
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    if (結存量 == dialog_NumPannel.Value.ToString())
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\複盤數量錯誤.wav");
                    try_error++;

                }


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
        void cnt_Program_領藥台_01_刷新領藥內容_檢查作業完成(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = new List<object[]>();

            if (myConfigClass.系統取藥模式)
            {
                list_取藥堆疊母資料 = _sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
                list_取藥堆疊子資料 = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetAllRows(false);
            }
            else
            {
                list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
                list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(this.領藥台_01名稱);
            }

            List<object[]> list_取藥堆疊子資料_buf = new List<object[]>();
            List<object[]> list_取藥堆疊子資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.作業完成.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string Master_GUID = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                //if (Function_取藥堆疊資料_取得作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤))
                //{
                //    voice.SpeakOnTask("請輸入盤點數量");
                //    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"藥碼:{藥碼} 藥名:{藥名}  請輸入盤點數量");
                //    dialog_NumPannel.ShowDialog();
                //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                //    list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                //}

                list_取藥堆疊子資料_buf = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);
                for (int k = 0; k < list_取藥堆疊子資料_buf.Count; k++)
                {
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.調劑結束] = true.ToString();
                    list_取藥堆疊子資料_replace.Add(list_取藥堆疊子資料_buf[k]);
                }
            }
            if (list_取藥堆疊母資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
            if (list_取藥堆疊子資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊子資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_檢查是否需輸入效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.輸入新效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                Dialog_輸入效期 dialog = new Dialog_輸入效期();
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    cnt = 65500;
                    return;
                }
                string 效期 = dialog.效期.StringToDateTime().ToDateString(TypeConvert.Enum_Year_Type.Anno_Domini, "/");
                dialog.Dispose();
                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = 效期;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.新增效期.GetEnumName();

                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(enum_取藥堆疊母資料.GUID.GetEnumName(), GIUD, list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_檢查是否需選擇效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                string 藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 交易量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                List<string> list_效期 = new List<string>();
                List<string> list_效期_buf = new List<string>();
                List<string> list_批號 = new List<string>();
                List<string> list_數量 = new List<string>();

                List<object> list_device = Function_從SQL取得儲位到本地資料(藥品碼);
                if (list_device.Count == 0)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    continue;
                }
                for (int k = 0; k < list_device.Count; k++)
                {
                    Device device = list_device[k] as Device;
                    if (device != null)
                    {
                        for (int m = 0; m < device.List_Validity_period.Count; m++)
                        {
                            list_效期_buf = (from value in list_效期
                                           where value == device.List_Validity_period[m]
                                           select value).ToList();
                            if (list_效期_buf.Count == 0)
                            {
                                list_效期.Add(device.List_Validity_period[m]);
                                list_批號.Add(device.List_Lot_number[m]);
                                list_數量.Add(device.List_Inventory[m]);
                            }
                        }
                    }
                }
                Dialog_選擇效期 dialog = new Dialog_選擇效期(藥品碼, 藥品名稱, 交易量, list_效期, list_批號, list_數量);
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    voice.SpeakOnTask("請選擇效期");
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    cnt = 65500;
                    return;
                }

                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = dialog.Value;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();
                dialog.Dispose();
                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_檢查自動登出(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_01名稱);
            List<object[]> list_取藥堆疊資料_buf = new List<object[]>();
            list_取藥堆疊資料_buf = (from value in list_取藥堆疊資料
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.無儲位.GetEnumName()
                               select value
                                ).ToList();

            if (list_取藥堆疊資料.Count == 0 && plC_CheckBox_多醫令模式.Bool == false)
            {
                MyTimer_領藥台_01_閒置登出時間.TickStop();
                MyTimer_領藥台_01_閒置登出時間.StartTickTime();

                MyTimer_領藥台_01_入賬完成時間.TickStop();
                MyTimer_領藥台_01_入賬完成時間.StartTickTime();
            }
            else
            {
                if (list_取藥堆疊資料_buf.Count > 0)
                {
                    MyTimer_領藥台_01_閒置登出時間.TickStop();
                    MyTimer_領藥台_01_閒置登出時間.StartTickTime();

                    MyTimer_領藥台_01_入賬完成時間.TickStop();
                    MyTimer_領藥台_01_入賬完成時間.StartTickTime();
                }
                else
                {
                    MyTimer_領藥台_01_閒置登出時間.StartTickTime();
                    MyTimer_領藥台_01_入賬完成時間.StartTickTime();
                    if (PLC_Device_領藥台_01_閒置登出時間.Value != 0)
                    {
                        if ((PLC_Device_領藥台_01_閒置登出時間.Value - (int)MyTimer_領藥台_01_閒置登出時間.GetTickTime()) <= 20000)
                        {
                            myTimer_領藥台_01_Logout.StartTickTime(5000);
                            if (myTimer_領藥台_01_Logout.IsTimeOut())
                            {
                                myTimer_領藥台_01_Logout.TickStop();
                                Task.Run(new Action(delegate
                                {
                                    using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\logout.wav"))
                                    {
                                        sp.Stop();
                                        sp.Play();
                                        sp.PlaySync();
                                    }

                                }));
                            }
                        }
                    }

                }
            }
            this.MyTimer__領藥台_01_刷新領藥內容_刷新間隔.TickStop();
            this.MyTimer__領藥台_01_刷新領藥內容_刷新間隔.StartTickTime(100);
            cnt++;
        }
        void cnt_Program_領藥台_01_刷新領藥內容_等待刷新間隔(ref int cnt)
        {
            if (this.MyTimer__領藥台_01_刷新領藥內容_刷新間隔.IsTimeOut())
            {
                cnt++;
            }
        }
        #endregion

        #region Function
        private void Function_領藥台_01_醫令領藥(string BarCode)
        {
            personPageClass personPageClass = new personPageClass();
            personPageClass.ID = 領藥台_01_ID;
            personPageClass.姓名 = 領藥台_01_登入者姓名;
            personPageClass.藥師證字號 = 領藥台_01_藥師證字號;
            personPageClass.顏色 = 領藥台_01_顏色;
            Function_醫令領藥(BarCode, personPageClass, 領藥台_01名稱, PLC_Device_領藥台_01_單醫令模式.Bool);
      
       
        }
        private void Function_領藥台_01_醫令退藥(string BarCode)
        {
            personPageClass personPageClass = new personPageClass();
            personPageClass.ID = 領藥台_01_ID;
            personPageClass.姓名 = 領藥台_01_登入者姓名;
            personPageClass.藥師證字號 = 領藥台_01_藥師證字號;
            personPageClass.顏色 = 領藥台_01_顏色;
            Function_醫令退藥(BarCode, personPageClass, 領藥台_01名稱, PLC_Device_領藥台_01_單醫令模式.Bool);
            return;
            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();

            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            string 藥品碼 = "";
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);
            List<object[]> list_醫令資料 = new List<object[]>();

            if (plC_Button_手輸數量.Bool)
            {
                double 手輸數量 = 0;
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入退藥數量");
                DialogResult dialogResult = dialog_NumPannel.ShowDialog();
                if (dialogResult != DialogResult.Yes) return;
                手輸數量 = dialog_NumPannel.Value * 1;
                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, 手輸數量);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單已過期.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單已過期", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                object[] value = list_醫令資料[0];
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_01名稱);
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");

                string GUID = value[(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_01名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;
                藥品碼 = value[(int)enum_醫囑資料.藥品碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) return;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = "";
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = value[(int)enum_醫囑資料.病歷號].ObjectToString();
                string 診別 = value[(int)enum_醫囑資料.藥局代碼].ObjectToString();
                string 領藥號 = value[(int)enum_醫囑資料.領藥號].ObjectToString();
                string 病房號 = value[(int)enum_醫囑資料.病房].ObjectToString();

                string 床號 = "";
                string 病人姓名 = value[(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 開方時間 = value[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_01_ID;
                string 操作人 = 領藥台_01_登入者姓名;
                string 藥師證字號 = 領藥台_01_藥師證字號;
                string 顏色 = 領藥台_01_顏色;
                double 總異動量 = value[(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.領藥號 = 領藥號;
                takeMedicineStackClass.病房號 = 病房號;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }
            else
            {


                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, PLC_Device_領藥台_01_單醫令模式.Bool);

                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
                if (pLC_Device.Bool == false) list_醫令資料 = list_醫令資料.GetRows((int)enum_醫囑資料.狀態, enum_醫囑資料_狀態.已過帳.GetEnumName());
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無已過帳資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無已過帳資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
           
                Console.Write($"取得醫令資料 , 耗時{myTimer.ToString()}\n");


                Dialog_醫令退藥 dialog_醫令退藥 = new Dialog_醫令退藥(list_醫令資料);
                if (dialog_醫令退藥.ShowDialog() != DialogResult.Yes) return;
                Fuction_領藥台_01_時間重置();
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool)
                {
                    Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
                    this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_01名稱);
                }

                List<OrderClass> orderClasses = dialog_醫令退藥.orderClasses;
                for (int i = 0; i < orderClasses.Count; i++)
                {
                    OrderClass orderClass = orderClasses[i];
                    string GUID = Guid.NewGuid().ToString();
                    string 調劑台名稱 = this.領藥台_01名稱;
                    enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;            
                    string ID = 領藥台_01_ID;
                    string 操作人 = 領藥台_01_登入者姓名;
                    string 藥師證字號 = 領藥台_01_藥師證字號;
                    string 顏色 = 領藥台_01_顏色;
                    double 總異動量 = 0;
                    string 效期 = "";
                    string 批號 = "";
                    string 備註 = orderClasses[i].備註;
                    StockClass stockClass = convert_note(備註);
                    if(stockClass != null)
                    {
                        效期 = stockClass.Validity_period;
                        批號 = stockClass.Lot_number;
                        總異動量 = stockClass.Qty.StringToDouble();
                    }

                    takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                    takeMedicineStackClass.GUID = GUID;
                    takeMedicineStackClass.Order_GUID = orderClass.GUID;
                    takeMedicineStackClass.序號 = orderClass.批序;
                    takeMedicineStackClass.動作 = 動作.GetEnumName();
                    takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                    takeMedicineStackClass.藥品碼 = orderClass.藥品碼;
                    takeMedicineStackClass.領藥號 = orderClass.領藥號;
                    takeMedicineStackClass.病房號 = orderClass.病房;
                    takeMedicineStackClass.診別 = orderClass.藥局代碼;
                    takeMedicineStackClass.顏色 = 顏色;
                 
                    takeMedicineStackClass.藥品名稱 = orderClass.藥品名稱;
                    takeMedicineStackClass.單位 = orderClass.劑量單位;
                    takeMedicineStackClass.藥袋序號 = orderClass.PRI_KEY;
                    takeMedicineStackClass.病歷號 = orderClass.病歷號;
                    takeMedicineStackClass.病人姓名 = orderClass.病人姓名;
                    takeMedicineStackClass.床號 = orderClass.床號;
                    takeMedicineStackClass.開方時間 = orderClass.開方日期;
                    takeMedicineStackClass.操作人 = 操作人;
                    takeMedicineStackClass.藥師證字號 = 藥師證字號;
                    takeMedicineStackClass.ID = ID;
                    takeMedicineStackClass.效期 = 效期;
                    takeMedicineStackClass.批號 = 批號;
            
                    takeMedicineStackClass.總異動量 = 總異動量.ToString();
                    takeMedicineStackClass.收支原因 = "";

                    takeMedicineStackClasses.Add(takeMedicineStackClass);
                }


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);
                Console.Write($"新增退藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }

        }

        private void Function_領藥台_01_QRCode領藥(string[] Scanner01_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_01名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥袋序號].ObjectToString();
            string 病人姓名 = "";
            string ID = 領藥台_01_ID;
            string 操作人 = 領藥台_01_登入者姓名;
            string 顏色 = 領藥台_01_顏色;
            string 床號 = "";
            double 總異動量 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";

            string 藥品碼 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();
            string 頻次 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.頻次].ObjectToString();


            string[] _serchnames = new string[] { enum_交易記錄查詢資料.藥品碼.GetEnumName(), enum_交易記錄查詢資料.病歷號.GetEnumName(), enum_交易記錄查詢資料.開方時間.GetEnumName(), enum_交易記錄查詢資料.頻次.GetEnumName(), enum_交易記錄查詢資料.藥袋序號.GetEnumName() };
            string[] _serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 頻次, 藥袋序號 };

            bool flag_重複領藥 = false;
            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(_serchnames, _serchvalues, false);
            list_交易記錄 = (from value in list_交易記錄
                         where value[(int)enum_交易記錄查詢資料.交易量].ObjectToString().StringToDouble() < 0
                         select value).ToList();

            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (list_交易記錄.Count > 0 && !pLC_Device.Bool)
            {
                this.voice.SpeakOnTask("此藥單已領取過");
                if (MyMessageBox.ShowDialog("此藥單已領取過,是否重複領藥?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    flag_重複領藥 = true;
                }
                else return;
            }

            string[] serchnames = new string[] { enum_取藥堆疊母資料.藥品碼.GetEnumName(), enum_取藥堆疊母資料.病歷號.GetEnumName(), enum_取藥堆疊母資料.開方時間.GetEnumName(), enum_取藥堆疊母資料.藥袋序號.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 藥袋序號 };
            if (sqL_DataGridView_領藥台_01_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }


            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_01名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            if (flag_重複領藥) 總異動量 = 0;
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.頻次 = 頻次;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.ID = ID;

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
        private void Function_領藥台_01_QRCode退藥(string[] Scanner01_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_01名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = this.領藥台_01名稱;
            string 病人姓名 = "";
            string ID = 領藥台_01_ID;
            string 操作人 = 領藥台_01_登入者姓名;
            string 顏色 = 領藥台_01_顏色;
            double 總異動量 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";
            string 床號 = "";
            string 藥品碼 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner01_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();


            string[] serchnames = new string[] { enum_取藥堆疊母資料.藥品碼.GetEnumName(), enum_取藥堆疊母資料.病歷號.GetEnumName(), enum_取藥堆疊母資料.開方時間.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間 };

            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(serchnames, serchvalues, false);
            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_退藥不檢查是否掃碼領藥過.Checked)
            {
                if (list_交易記錄.Count == 0)
                {
                    this.voice.SpeakOnTask("查無領藥紀錄");
                    return;
                }
            }

            if (sqL_DataGridView_領藥台_01_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            總異動量 = dialog_NumPannel.Value;
            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_01名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            takeMedicineStackClass.ID = ID;

            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
     
        private void Fuction_領藥台_01_時間重置()
        {
            MyTimer_領藥台_01_閒置登出時間.TickStop();
            MyTimer_領藥台_01_閒置登出時間.StartTickTime();
            MyTimer_領藥台_01_入賬完成時間.TickStop();
            MyTimer_領藥台_01_入賬完成時間.StartTickTime();
        }
        #endregion

        #region Event

        private void SqL_DataGridView_領藥台_01_領藥內容_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].Cells[(int)enum_取藥堆疊母資料.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.已領用過.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.sqL_DataGridView_領藥台_01_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void SqL_DataGridView_領藥台_01_領藥內容_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            try
            {
                RowsList = Function_領藥內容_重新排序(RowsList);
            }
            catch (Exception e)
            {
                Console.WriteLine($"SqL_DataGridView_領藥台_01_領藥內容_DataGridRowsChangeRefEvent1 : {e.Message}");
            }
        }
        private void SqL_DataGridView_領藥台_01_領藥內容_DataGridClearGridEvent()
        {

            try
            {
                Function_調劑作業_醫令資訊更新(1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"SqL_DataGridView_領藥台_01_領藥內容_DataGridClearGridEvent : {e.Message}");
            }
        }
        private void SqL_DataGridView_領藥台_01_領藥內容_RowEnterEvent(object[] RowValue)
        {
            string 藥碼 = RowValue[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
            string 藥名 = RowValue[(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
            string 領藥號 = RowValue[(int)enum_取藥堆疊母資料.領藥號].ObjectToString();
            string 病歷號 = RowValue[(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
            string 開方時間 = RowValue[(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
            Function_調劑作業_醫令資訊更新(藥碼, 藥名, 領藥號, 病歷號, 開方時間, 1);

        }
        private void PlC_RJ_Button_領藥台_01_取消作業_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_01_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_01_藥品圖片.Image = null;
            //}));
            Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.取消作業, 領藥台_01_登入者姓名, "01.號使用者");
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_01名稱);
            Function_調劑作業_醫令資訊更新(1);
            this.sqL_DataGridView_領藥台_01_領藥內容.ClearGrid();

        }
        private void PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(MouseEventArgs mevent)
        {
            if (Function_檢查是否完成交班() == false)
            {
                MyMessageBox.ShowDialog("請先完成交班");
                return;
            }
            if (plC_RJ_Button_領藥台_01_登入.Texts == "登出")
            {
                PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(null);
                return;
            }
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
            if (this.PLC_Device_領藥台_01_已登入.Bool) return;
            if (textBox_領藥台_01_帳號.Texts.StringIsEmpty()) return;

            if (textBox_領藥台_01_帳號.Texts.ToUpper() == Admin_ID.ToUpper())
            {
                if (textBox_領藥台_01_密碼.Texts.ToUpper() == Admoin_Password.ToUpper())
                {
                    this.PLC_Device_領藥台_01_已登入.Bool = true;
                    領藥台_01_登入者姓名 = "最高管理權限";
                    領藥台_01_ID = "admin";
                    this.PLC_Device_最高權限.Bool = true;
                    return;
                }
            }

            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_人員資料.ID, textBox_領藥台_01_帳號.Texts);
            if (list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無此帳號", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("查無此帳號!");
                return;
            }
            string password = list_value[0][(int)enum_人員資料.密碼].ObjectToString();
            if (textBox_領藥台_01_密碼.Texts != password)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("密碼錯誤", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("密碼錯誤!");
                return;
            }
            領藥台_01_登入者姓名 = list_value[0][(int)enum_人員資料.姓名].ObjectToString();
            領藥台_01_ID = list_value[0][(int)enum_人員資料.ID].ObjectToString();
            領藥台_01_顏色 = list_value[0][(int)enum_人員資料.顏色].ObjectToString();
            領藥台_01_藥師證字號 = list_value[0][(int)enum_人員資料.藥師證字號].ObjectToString();
            this.PLC_Device_領藥台_01_已登入.Bool = true;
            if (mevent != null) Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.密碼登入, 領藥台_01_登入者姓名, "領藥台_01");
            string 狀態顯示 = "";
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_01_狀態顯示.GetAlignmentString(PLC_MultiStateDisplay.TextValue.Alignment.Left);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_01_狀態顯示.GetFontColorString(Color.Black, true);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_01_狀態顯示.GetFontString(new Font("微軟正黑體", 24F, FontStyle.Bold), true);
            //狀態顯示 += string.Format($"登入者姓名 : {領藥台_01_登入者姓名}");
            //this.plC_MultiStateDisplay_領藥台_01_狀態顯示.SetTextValue(PLC_Device_領藥台_01_狀態顯示_登入者姓名.GetAdress(), 狀態顯示);
            this.plC_Button_領藥台_01_領.Bool = true;
            this.plC_Button_領藥台_01_退.Bool = false;

            Console.WriteLine($"登入成功! ID : {領藥台_01_ID}, 名稱 : {領藥台_01_登入者姓名}");
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_01_帳號.Texts = "";
                textBox_領藥台_01_密碼.Texts = "";
                plC_RJ_Button_領藥台_01_登入.Texts = "登出";
                string text_temp = PLC_Device_領藥台_01_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                rJ_Lable_領藥台_01_Title.Text = $"    01. [{領藥台_01_登入者姓名}] {text_temp}";
                this.rJ_Lable_領藥台_01_Title.BorderColor = this.panel_工程模式_領藥台_01_顏色.BackColor;
                this.rJ_Lable_領藥台_01_Title.BackgroundColor = Color.GreenYellow;
                this.rJ_Lable_領藥台_01_Title.TextColor = Color.Black;
            }));
            commonSapceClasses = Function_取得共用區所有儲位();
            //MySerialPort_Scanner01.ClearReadByte();
            Voice.MediaPlayAsync($@"{currentDirectory}\登入成功.wav");

            PLC_Device_Scanner01_讀取藥單資料.Bool = false;
            PLC_Device_Scanner01_讀取藥單資料_OK.Bool = false;
            領藥台_01_醫令條碼 = "";
        }
        private void PlC_RJ_Button_領藥台_01_登出_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_01_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_01_藥品圖片.Image = null;
            //}));
            Function_調劑作業_醫令資訊更新(1);
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_01名稱);
            this.sqL_DataGridView_領藥台_01_領藥內容.ClearGrid();

            Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.登出, 領藥台_01_登入者姓名, "01.號使用者");
            領藥台_01_登入者姓名 = "None";
            this.PLC_Device_領藥台_01_已登入.Bool = false;
            this.PLC_Device_最高權限.Bool = false;
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_01_帳號.Texts = "";
                textBox_領藥台_01_密碼.Texts = "";
                plC_RJ_Button_領藥台_01_登入.Texts = "登入";
                rJ_Lable_領藥台_01_Title.Text = $"    01. [未登入]";
                this.rJ_Lable_領藥台_01_Title.BorderColor = Color.DimGray;
                this.rJ_Lable_領藥台_01_Title.BackgroundColor = Color.DimGray;
                this.rJ_Lable_領藥台_01_Title.TextColor = Color.White;

            }));
        }
        private void PlC_Button_領藥台_01_退_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_01_領.Bool = !Value;
            this.plC_Button_領藥台_01_退.Bool = Value;
        }
        private void PlC_Button_領藥台_01_領_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_01_領.Bool = Value;
            this.plC_Button_領藥台_01_退.Bool = !Value;
        }
        private void TextBox_領藥台_01_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox_領藥台_01_密碼.Focus();
            }
        }
        private void TextBox_領藥台_01_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.PlC_RJ_Button_領藥台_01_登入_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
            }
        }

        #endregion

        #endregion
        #region 領藥台_02
        MyTimer MyTimer_領藥台_02_閒置登出時間 = new MyTimer("D100");
        PLC_Device PLC_Device_領藥台_02_閒置登出時間 = new PLC_Device("D100");
        MyTimer MyTimer_領藥台_02_入賬完成時間 = new MyTimer("D101");
        PLC_Device PLC_Device_領藥台_02_入賬完成時間 = new PLC_Device("D101");
        int 領藥台_02_RFID站號 = 2;
        List<object> 領藥台_02_儲位 = new List<object>();
        List<object[]> 領藥台_02_領藥儲位資訊 = new List<object[]>();
        List<string[]> 領藥台_02_掃描BUFFER = new List<string[]>();
        PLC_Device PLC_Device_領藥台_02_登出 = new PLC_Device();
        FpMatchLib.FpMatchClass FpMatchClass_領藥台_02_指紋資訊;
        PLC_Device PLC_Device_領藥台_02_已登入 = new PLC_Device("S200");
        PLC_Device PLC_Device_領藥台_02_單醫令模式 = new PLC_Device("S210");
        PLC_Device PLC_Device_領藥台_02_狀態顯示_等待登入 = new PLC_Device("M5000");
        PLC_Device PLC_Device_領藥台_02_狀態顯示_登入者姓名 = new PLC_Device("M5001");

        public static string 領藥台_02_登入者姓名 = "";
        public static string 領藥台_02_ID = "";
        public static string 領藥台_02_卡號 = "";
        public static string _領藥台_02_顏色 = "";
        public string 領藥台_02_顏色
        {
            get
            {
                if (plC_CheckBox_掃碼顏色固定.Checked)
                {
                    this.Invoke(new Action(delegate
                    {
                        Color color = this.panel_工程模式_領藥台_02_顏色.BackColor;
                        if (color == Color.Black)
                        {
                            this.panel_工程模式_領藥台_02_顏色.BackColor = Color.Blue;
                            SaveConfig工程模式();
                        }
                        _領藥台_02_顏色 = color.ToColorString();
                    }));


                    return _領藥台_02_顏色;
                }
                else
                {
                    return _領藥台_02_顏色;
                }
            }
            set
            {
                _領藥台_02_顏色 = value;
            }
        }
        public static string 領藥台_02_一維碼 = "";
        public static string 領藥台_02_藥師證字號 = "";

        private string 領藥台_02_醫令條碼 = "";

        #region PLC_領藥台_02_狀態顯示


        PLC_Device PLC_Device_領藥台_02_狀態顯示 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_02_狀態顯示_OK = new PLC_Device("");
        MyTimer MyTimer_領藥台_02_狀態顯示_結束延遲 = new MyTimer();
        int cnt_Program_領藥台_02_狀態顯示 = 65534;
        void sub_Program_領藥台_02_狀態顯示()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" || true)
            {
                PLC_Device_領藥台_02_狀態顯示.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_02_狀態顯示.Bool = false;
            }
            if (cnt_Program_領藥台_02_狀態顯示 == 65534)
            {
                this.MyTimer_領藥台_02_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_02_狀態顯示.SetComment("PLC_領藥台_02_狀態顯示");
                PLC_Device_領藥台_02_狀態顯示_OK.SetComment("PLC_領藥台_02_狀態顯示_OK");
                PLC_Device_領藥台_02_狀態顯示.Bool = false;
                cnt_Program_領藥台_02_狀態顯示 = 65535;
            }
            if (cnt_Program_領藥台_02_狀態顯示 == 65535) cnt_Program_領藥台_02_狀態顯示 = 1;
            if (cnt_Program_領藥台_02_狀態顯示 == 1) cnt_Program_領藥台_02_狀態顯示_檢查按下(ref cnt_Program_領藥台_02_狀態顯示);
            if (cnt_Program_領藥台_02_狀態顯示 == 2) cnt_Program_領藥台_02_狀態顯示_初始化(ref cnt_Program_領藥台_02_狀態顯示);
            if (cnt_Program_領藥台_02_狀態顯示 == 3) cnt_Program_領藥台_02_狀態顯示 = 65500;
            if (cnt_Program_領藥台_02_狀態顯示 > 1) cnt_Program_領藥台_02_狀態顯示_檢查放開(ref cnt_Program_領藥台_02_狀態顯示);

            if (cnt_Program_領藥台_02_狀態顯示 == 65500)
            {
                this.MyTimer_領藥台_02_狀態顯示_結束延遲.TickStop();
                this.MyTimer_領藥台_02_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_02_狀態顯示.Bool = false;
                PLC_Device_領藥台_02_狀態顯示_OK.Bool = false;
                cnt_Program_領藥台_02_狀態顯示 = 65535;
            }
        }
        void cnt_Program_領藥台_02_狀態顯示_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_02_狀態顯示.Bool) cnt++;
        }
        void cnt_Program_領藥台_02_狀態顯示_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_02_狀態顯示.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_02_狀態顯示_初始化(ref int cnt)
        {
            if (!this.PLC_Device_領藥台_02_已登入.Bool)
            {
                PLC_Device_領藥台_02_狀態顯示_等待登入.Bool = true;
                PLC_Device_領藥台_02_狀態顯示_登入者姓名.Bool = false;
            }
            else
            {
                PLC_Device_領藥台_02_狀態顯示_等待登入.Bool = false;
                PLC_Device_領藥台_02_狀態顯示_登入者姓名.Bool = true;
            }
            cnt++;
        }


        #endregion

        #region PLC_領藥台_02_檢查登入

        PLC_Device PLC_Device_領藥台_02_檢查登入 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_02_檢查登入_OK = new PLC_Device("");


        int cnt_Program_領藥台_02_檢查登入 = 65534;
        void sub_Program_領藥台_02_檢查登入()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業") PLC_Device_領藥台_02_檢查登入.Bool = true;
            else PLC_Device_領藥台_02_檢查登入.Bool = false;
            if (cnt_Program_領藥台_02_檢查登入 == 65534)
            {
                PLC_Device_領藥台_02_檢查登入.SetComment("PLC_領藥台_02_檢查登入");
                PLC_Device_領藥台_02_檢查登入_OK.SetComment("PLC_Device_領藥台_02_檢查登入_OK");
                PLC_Device_領藥台_02_已登入.Bool = false;
                PLC_Device_領藥台_02_檢查登入.Bool = false;
                cnt_Program_領藥台_02_檢查登入 = 65535;
            }
            if (cnt_Program_領藥台_02_檢查登入 == 65535) cnt_Program_領藥台_02_檢查登入 = 1;
            if (cnt_Program_領藥台_02_檢查登入 == 1) cnt_Program_領藥台_02_檢查登入_檢查按下(ref cnt_Program_領藥台_02_檢查登入);
            if (cnt_Program_領藥台_02_檢查登入 == 2) cnt_Program_領藥台_02_檢查登入_初始化(ref cnt_Program_領藥台_02_檢查登入);
            if (cnt_Program_領藥台_02_檢查登入 == 3) cnt_Program_領藥台_02_檢查登入_外部設備資料或帳號密碼登入(ref cnt_Program_領藥台_02_檢查登入);
            if (cnt_Program_領藥台_02_檢查登入 == 4) cnt_Program_領藥台_02_檢查登入 = 65500;
            if (cnt_Program_領藥台_02_檢查登入 > 1) cnt_Program_領藥台_02_檢查登入_檢查放開(ref cnt_Program_領藥台_02_檢查登入);

            if (cnt_Program_領藥台_02_檢查登入 == 65500)
            {
                PLC_Device_領藥台_02_檢查登入.Bool = false;
                cnt_Program_領藥台_02_檢查登入 = 65535;
            }
        }
        void cnt_Program_領藥台_02_檢查登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_02_檢查登入.Bool) cnt++;
        }
        void cnt_Program_領藥台_02_檢查登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_02_檢查登入.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_02_檢查登入_初始化(ref int cnt)
        {
            PLC_Device_領藥台_02_檢查登入_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_02_檢查登入_外部設備資料或帳號密碼登入(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (Dialog_使用者登入.IsShown)
            {
                if (MySerialPort_Scanner02.ReadByte() != null)
                {
                    System.Threading.Thread.Sleep(50);
                    string text = MySerialPort_Scanner02.ReadString();
                    MySerialPort_Scanner02.ClearReadByte();
                    if (text == null) return;
                    text = text.Replace("\0", "");
                    if (text.Length <= 2 || text.Length > 30) return;
                    if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                    領藥台_02_一維碼 = text;
                }
                return;
            }
            string UID = this.rfiD_FX600_UI.Get_RFID_UID(領藥台_02_RFID站號);
            if (!UID.StringIsEmpty() && UID.StringToInt32() != 0)
            {
                Console.WriteLine($"成功讀取RFID  {UID}");
                領藥台_02_卡號 = UID;
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.卡號.GetEnumName(), 領藥台_02_卡號, false);
                if (list_人員資料.Count == 0) return;
                Console.WriteLine($"取得人員資料完成!");
                if (!PLC_Device_領藥台_02_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_02_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_02_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(null);
                    }));
                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_02_登入者姓名, "02.號使用者");

                }
                else
                {
                    if (領藥台_02_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_02_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_02_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_02_登入者姓名, "02.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (MySerialPort_Scanner02.ReadByte() != null && !PLC_Device_領藥台_02_已登入.Bool)
            {
                System.Threading.Thread.Sleep(50);
                string text = MySerialPort_Scanner02.ReadString();
                MySerialPort_Scanner02.ClearReadByte();
                if (text == null) return;
                text = text.Replace("\0", "");
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r", "");
                text = text.Replace("\n", "");
                領藥台_02_一維碼 = text;

                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), 領藥台_02_一維碼, false);
                if (list_人員資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\查無此一維碼.wav");
                    return;
                }
                if (!PLC_Device_領藥台_02_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_02_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_02_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_02_登入者姓名, "02.號使用者");
                }
                else
                {
                    if (領藥台_02_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_02_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_02_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_02_登入者姓名, "02.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (FpMatchClass_領藥台_02_指紋資訊 != null)
            {
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
                object[] value = null;
                for (int i = 0; i < list_人員資料.Count; i++)
                {
                    string feature = list_人員資料[i][(int)enum_人員資料.指紋辨識].ObjectToString();
                    if (fpMatchSoket.Match(FpMatchClass_領藥台_02_指紋資訊.feature, feature))
                    {
                        value = list_人員資料[i];
                        break;
                    }
                }
                FpMatchClass_領藥台_02_指紋資訊 = null;
                if (value == null)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找無符合指紋資訊", 2000);
                    dialog_AlarmForm.ShowDialog();
                    cnt = 65500;
                    return;
                }
                if (!PLC_Device_領藥台_02_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_02_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_02_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_02_登入者姓名, "01.號使用者");
                }
                else
                {
                    if (領藥台_02_ID != value[(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_02_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_02_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_02_登入者姓名, "01.號使用者");
                    }
                }
            }
            cnt = 65500;
            return;


        }

        #endregion
        #region PLC_領藥台_02_檢查輸入資料

        PLC_Device PLC_Device_領藥台_02_檢查輸入資料 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_02_檢查輸入資料_OK = new PLC_Device("");

        MyTimer MyTimer_領藥台_02_檢查輸入資料_NG訊息延遲 = new MyTimer();
        int cnt_Program_領藥台_02_檢查輸入資料 = 65534;
        void sub_Program_領藥台_02_檢查輸入資料()
        {
            if ((this.plC_ScreenPage_Main.PageText == "調劑作業" || true) && PLC_Device_領藥台_02_已登入.Bool)
            {
                PLC_Device_領藥台_02_檢查輸入資料.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_02_檢查輸入資料.Bool = false;
            }
            if (Dialog_使用者登入.IsShown)
            {
                PLC_Device_領藥台_02_檢查輸入資料.Bool = false;
            }
            if (cnt_Program_領藥台_02_檢查輸入資料 == 65534)
            {
                PLC_Device_領藥台_02_檢查輸入資料.SetComment("PLC_領藥台_02_檢查輸入資料");
                PLC_Device_領藥台_02_檢查輸入資料_OK.SetComment("PLC_Device_領藥台_02_檢查輸入資料_OK");
                PLC_Device_領藥台_02_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_02_檢查輸入資料 = 65535;
            }
            if (cnt_Program_領藥台_02_檢查輸入資料 == 65535) cnt_Program_領藥台_02_檢查輸入資料 = 1;
            if (cnt_Program_領藥台_02_檢查輸入資料 == 1) cnt_Program_領藥台_02_檢查輸入資料_檢查按下(ref cnt_Program_領藥台_02_檢查輸入資料);
            if (cnt_Program_領藥台_02_檢查輸入資料 == 2) cnt_Program_領藥台_02_檢查輸入資料_初始化(ref cnt_Program_領藥台_02_檢查輸入資料);
            if (cnt_Program_領藥台_02_檢查輸入資料 == 3) cnt_Program_領藥台_02_檢查輸入資料_設定開始掃描(ref cnt_Program_領藥台_02_檢查輸入資料);
            if (cnt_Program_領藥台_02_檢查輸入資料 == 4) cnt_Program_領藥台_02_檢查輸入資料_等待掃描結束(ref cnt_Program_領藥台_02_檢查輸入資料);
            if (cnt_Program_領藥台_02_檢查輸入資料 == 5) cnt_Program_領藥台_02_檢查輸入資料_檢查醫令資料及寫入(ref cnt_Program_領藥台_02_檢查輸入資料);
            if (cnt_Program_領藥台_02_檢查輸入資料 == 6) cnt_Program_領藥台_02_檢查輸入資料 = 65500;


            if (cnt_Program_領藥台_02_檢查輸入資料 > 1) cnt_Program_領藥台_02_檢查輸入資料_檢查放開(ref cnt_Program_領藥台_02_檢查輸入資料);

            if (cnt_Program_領藥台_02_檢查輸入資料 == 65500)
            {
                PLC_Device_領藥台_02_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_02_檢查輸入資料 = 65535;
            }
        }
        void cnt_Program_領藥台_02_檢查輸入資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_02_檢查輸入資料.Bool) cnt++;
        }
        void cnt_Program_領藥台_02_檢查輸入資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_02_檢查輸入資料.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_02_檢查輸入資料_初始化(ref int cnt)
        {
            //PLC_Device_Scanner02_讀取藥單資料.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_02_檢查輸入資料_設定開始掃描(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner02_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner02_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner02_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
            else
            {
                if (!PLC_Device_Scanner02_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner02_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner02_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_02_檢查輸入資料_等待掃描結束(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner02_讀取藥單資料.Bool || PLC_Device_Scanner02_讀取藥單資料_OK.Bool)
                {
                    if (PLC_Device_Scanner02_讀取藥單資料_OK.Bool)
                    {
                        cnt++;
                        return;
                    }
                    else
                    {
                        this.voice.SpeakOnTask("掃碼失敗");
                        cnt = 65500;
                        return;
                    }
                }
            }
            else
            {
                if (!PLC_Device_Scanner02_讀取藥單資料.Bool || PLC_Device_Scanner02_讀取藥單資料_OK.Bool)
                {

                    if (領藥台_02_醫令條碼.Length < 15)
                    {
                        string text = 領藥台_02_醫令條碼.Replace("\n", "");
                        text = text.Replace("\r", "");
                        Console.WriteLine($"{text}");
                        if (text.StringIsEmpty())
                        {
                            cnt = 65500;
                            return;
                        }
                        if (text == QR_Code_醫令模式切換)
                        {
                            PLC_Device_領藥台_02_單醫令模式.Bool = !PLC_Device_領藥台_02_單醫令模式.Bool;

                            string text_temp = PLC_Device_領藥台_02_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                            Console.WriteLine($"切換模式至{text_temp}");
                            Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"切換模式至{text_temp}", 1000, 0, 0);
                            this.Invoke(new Action(delegate
                            {

                                this.rJ_Lable_領藥台_02_Title.Text = $"    02. [{領藥台_02_登入者姓名}] {text_temp}";
                                this.rJ_Lable_領藥台_02_Title.BorderColor = this.panel_工程模式_領藥台_02_顏色.BackColor;
                                this.rJ_Lable_領藥台_02_Title.BackgroundColor = Color.GreenYellow;
                                this.rJ_Lable_領藥台_02_Title.TextColor = Color.Black;
                            }));
                            dialog_AlarmForm.ShowDialog();
                            cnt = 65500;
                            return;
                        }

                        List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), text, false);
                        if (list_人員資料.Count > 0)
                        {
                            if (領藥台_02_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                            {
                                this.PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(null);

                                this.Invoke(new Action(delegate
                                {
                                    textBox_領藥台_02_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                                    textBox_領藥台_02_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                                    this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(null);
                                }));
                                Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_02_登入者姓名, "02.號使用者");

                            }
                            cnt = 65500;
                            return;
                        }
                    }

                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_02_檢查輸入資料_檢查醫令資料及寫入(ref int cnt)
        {
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (plC_Button_領藥台_02_領.Bool)
                {
                    this.Function_領藥台_02_QRCode領藥(this.Scanner02_讀取藥單資料_Array);
                }
                else if (plC_Button_領藥台_02_退.Bool)
                {
                    this.Function_領藥台_02_QRCode退藥(this.Scanner02_讀取藥單資料_Array);
                }
                cnt++;
            }
            else
            {
                if (plC_Button_領藥台_02_領.Bool)
                {
                    this.Function_領藥台_02_醫令領藥(領藥台_02_醫令條碼);
                }
                else if (plC_Button_領藥台_02_退.Bool)
                {
                    this.Function_領藥台_02_醫令退藥(領藥台_02_醫令條碼);
                }
                cnt++;
            }

        }



        #endregion
        #region PLC_領藥台_02_刷新領藥內容
        PLC_Device PLC_Device_領藥台_02_刷新領藥內容 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_02_刷新領藥內容_OK = new PLC_Device("");
        MyTimer MyTimer__領藥台_02_刷新領藥內容_刷新間隔 = new MyTimer();
        int cnt_Program_領藥台_02_刷新領藥內容 = 65534;
        void sub_Program_領藥台_02_刷新領藥內容()
        {
            if ((this.plC_ScreenPage_Main.PageText == "調劑作業" || true))
            {
                PLC_Device_領藥台_02_刷新領藥內容.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_02_刷新領藥內容.Bool = false;
            }
            PLC_Device_領藥台_02_刷新領藥內容.Bool = (this.plC_ScreenPage_Main.PageText == "調劑作業");
            if (cnt_Program_領藥台_02_刷新領藥內容 == 65534)
            {
                PLC_Device_領藥台_02_刷新領藥內容.SetComment("PLC_領藥台_02_刷新領藥內容");
                PLC_Device_領藥台_02_刷新領藥內容_OK.SetComment("PLC_領藥台_02_刷新領藥內容_OK");
                PLC_Device_領藥台_02_刷新領藥內容.Bool = false;
                cnt_Program_領藥台_02_刷新領藥內容 = 65535;
            }
            if (cnt_Program_領藥台_02_刷新領藥內容 == 65535) cnt_Program_領藥台_02_刷新領藥內容 = 1;
            if (cnt_Program_領藥台_02_刷新領藥內容 == 1) cnt_Program_領藥台_02_刷新領藥內容_檢查按下(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 2) cnt_Program_領藥台_02_刷新領藥內容_初始化(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 3) cnt_Program_領藥台_02_刷新領藥內容_取得資料(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 4) cnt_Program_領藥台_02_刷新領藥內容_檢查雙人覆核(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 5) cnt_Program_領藥台_02_刷新領藥內容_檢查盲盤作業(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 6) cnt_Program_領藥台_02_刷新領藥內容_檢查複盤作業(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 7) cnt_Program_領藥台_02_刷新領藥內容_檢查作業完成(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 8) cnt_Program_領藥台_02_刷新領藥內容_檢查是否需輸入效期(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 9) cnt_Program_領藥台_02_刷新領藥內容_檢查是否需選擇效期(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 10) cnt_Program_領藥台_02_刷新領藥內容_檢查自動登出(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 11) cnt_Program_領藥台_02_刷新領藥內容_等待刷新間隔(ref cnt_Program_領藥台_02_刷新領藥內容);
            if (cnt_Program_領藥台_02_刷新領藥內容 == 12) cnt_Program_領藥台_02_刷新領藥內容 = 65500;
            if (cnt_Program_領藥台_02_刷新領藥內容 > 1) cnt_Program_領藥台_02_刷新領藥內容_檢查放開(ref cnt_Program_領藥台_02_刷新領藥內容);

            if (cnt_Program_領藥台_02_刷新領藥內容 == 65500)
            {
                PLC_Device_領藥台_02_刷新領藥內容.Bool = false;
                PLC_Device_領藥台_02_刷新領藥內容_OK.Bool = false;
                cnt_Program_領藥台_02_刷新領藥內容 = 65535;
            }
        }
        void cnt_Program_領藥台_02_刷新領藥內容_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_02_刷新領藥內容.Bool) cnt++;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_02_刷新領藥內容.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_取得資料(ref int cnt)
        {
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_取藥堆疊資料 = new List<object[]>();
            List<object[]> list_取藥堆疊資料_buf = new List<object[]>();
            List<object[]> list_取藥堆疊資料_add = new List<object[]>();
            if (myConfigClass.系統取藥模式) list_取藥堆疊資料 = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            else list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            List<object[]> list_取藥堆疊資料_replace = new List<object[]>();
            string GUID = "";
            string 序號 = "";
            string 動作 = "";
            string 藥袋序號 = "";
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 病歷號 = "";
            string 操作時間 = "";
            string 開方時間 = "";
            string 庫存量 = "";
            string 總異動量 = "";
            string 結存量 = "";
            string 單位 = "";
            string 狀態 = "";
            string 床號 = "";
            list_取藥堆疊資料.Sort(new Icp_取藥堆疊母資料_index排序());

            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示))
                {
                    this.voice.SpeakOnTask("庫存不足");
                    this.Function_取藥堆疊資料_設定作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示, false);
                    list_取藥堆疊資料_replace.Add(list_取藥堆疊資料[i]);
                }
                GUID = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                序號 = (i + 1).ToString();
                動作 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.動作].ObjectToString();
                藥袋序號 = $"{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥袋序號].ObjectToString()}:{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.頻次].ObjectToString()}";
                藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                病歷號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
                操作時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString();
                開方時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
                庫存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.庫存量].ObjectToString();
                總異動量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString();
                結存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                單位 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.單位].ObjectToString();
                狀態 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString();
                床號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.床號].ObjectToString();
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.盲盤))
                {
                    庫存量 = "無";
                    結存量 = "無";
                }
                object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                value[(int)enum_取藥堆疊母資料.GUID] = GUID;
                value[(int)enum_取藥堆疊母資料.序號] = 序號;
                value[(int)enum_取藥堆疊母資料.動作] = 動作;
                value[(int)enum_取藥堆疊母資料.藥袋序號] = 藥袋序號;
                value[(int)enum_取藥堆疊母資料.藥品碼] = 藥品碼;
                value[(int)enum_取藥堆疊母資料.藥品名稱] = 藥品名稱;
                value[(int)enum_取藥堆疊母資料.病歷號] = 病歷號;
                value[(int)enum_取藥堆疊母資料.操作時間] = 操作時間;
                value[(int)enum_取藥堆疊母資料.開方時間] = 開方時間;
                value[(int)enum_取藥堆疊母資料.庫存量] = 庫存量;
                value[(int)enum_取藥堆疊母資料.總異動量] = 總異動量;
                value[(int)enum_取藥堆疊母資料.結存量] = 結存量;
                value[(int)enum_取藥堆疊母資料.單位] = 單位;
                value[(int)enum_取藥堆疊母資料.狀態] = 狀態;
                value[(int)enum_取藥堆疊母資料.床號] = 床號;

                list_value.Add(value);


            }
            if (plC_CheckBox_調劑畫面合併相同藥品.Bool)
            {
                List<object[]> list_value_new = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();
                string[] 藥品碼_array = (from value in list_value
                                      select value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString()).Distinct().ToList().ToArray();
                for (int i = 0; i < 藥品碼_array.Length; i++)
                {
                    list_value_buf = (from value in list_value
                                      where value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼_array[i]
                                      select value).ToList();
                    if (list_value_buf.Count == 0) continue;
                    object[] value_領藥內容 = new object[new enum_取藥堆疊母資料().GetLength()];
                    value_領藥內容[(int)enum_取藥堆疊母資料.GUID] = list_value_buf[0][(int)enum_取藥堆疊母資料.GUID];
                    value_領藥內容[(int)enum_取藥堆疊母資料.序號] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.動作] = 動作;
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥袋序號] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥袋序號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品碼] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品碼];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品名稱] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品名稱];
                    value_領藥內容[(int)enum_取藥堆疊母資料.病歷號] = list_value_buf[0][(int)enum_取藥堆疊母資料.病歷號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.操作時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.操作時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.開方時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.開方時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.庫存量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.結存量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.單位] = list_value_buf[0][(int)enum_取藥堆疊母資料.單位];
                    value_領藥內容[(int)enum_取藥堆疊母資料.床號] = list_value_buf[0][(int)enum_取藥堆疊母資料.床號];
                    double 總異動量_temp = 0;
                    bool flag_入賬完成 = true;
                    bool flag_無儲位 = false;
                    bool flag_庫存不足 = false;
                    for (int k = 0; k < list_value_buf.Count; k++)
                    {
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                        {
                            flag_入賬完成 = false;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                        {
                            flag_無儲位 = true;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                        {
                            flag_庫存不足 = true;
                        }
                        總異動量_temp += list_value_buf[k][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();
                    }
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = 總異動量_temp;
                    if (flag_入賬完成)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName();
                    }
                    else if (flag_無儲位)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    }
                    else if (flag_庫存不足)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName();
                    }
                    else
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                    }
                    list_value_new.Add(value_領藥內容);

                }
                for (int i = 0; i < list_value_new.Count; i++)
                {
                    list_value_new[i][(int)enum_取藥堆疊母資料.序號] = (i + 1).ToString();
                    藥品碼 = list_value_new[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    double 庫存量_temp = Function_從SQL取得庫存(藥品碼);
                    double 結存量_temp = 庫存量_temp + list_value_new[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();

                    list_value_new[i][(int)enum_取藥堆疊母資料.庫存量] = 庫存量_temp.ToString();
                    list_value_new[i][(int)enum_取藥堆疊母資料.結存量] = 結存量_temp.ToString();

                }
                list_value = list_value_new;
            }
            try
            {
                for (int i = 0; i < list_取藥堆疊資料.Count; i++)
                {
                    list_取藥堆疊資料_buf = this.sqL_DataGridView_領藥台_02_領藥內容.GetRows("GUID", list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString(), false);
                    if (list_取藥堆疊資料_buf.Count == 0)
                    {
                        list_取藥堆疊資料_add.Add(list_取藥堆疊資料[i]);
                    }
                }
                if (list_取藥堆疊資料_add.Count > 0)
                {
                    string 藥碼 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    string 藥名 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                    string 領藥號 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.領藥號].ObjectToString();
                    病歷號 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
                    開方時間 = list_取藥堆疊資料_add[0][(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
                    Function_調劑作業_醫令資訊更新(藥碼, 藥名, 領藥號, 病歷號, 開方時間, 2);
                }
            }
            catch
            {

            }

            this.sqL_DataGridView_領藥台_02_領藥內容.RefreshGrid(list_value);
            Application.DoEvents();
            if (list_取藥堆疊資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_檢查雙人覆核(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                Application.DoEvents();
                Dialog_使用者登入 dialog_使用者登入 = new Dialog_使用者登入(領藥台_02_ID, 藥名);
                this.Invoke(new Action(delegate
                {
                    //dialog_使用者登入.Location = new Point(this.rJ_GroupBox_領藥台_02.PointToScreen(Point.Empty).X + this.rJ_GroupBox_領藥台_02.Width + 20, 1);
                }));

                if (dialog_使用者登入.ShowDialog() != DialogResult.Yes)
                {
                    list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料[i]);
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                    Fuction_領藥台_02_時間重置();
                    continue;
                }
                Fuction_領藥台_02_時間重置();
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師姓名] = dialog_使用者登入.UserName;
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師ID] = dialog_使用者登入.UserID;
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
        void cnt_Program_領藥台_02_刷新領藥內容_檢查盲盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                int retry = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 總異動量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                double 結存量 = 0;
                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入盲盤數量.wav"); ;
                while (true)
                {

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(盲盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}\n交易量:{總異動量}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;
                    this.Invoke(new Action(delegate
                    {
                        //dialog_NumPannel.Location = new Point(this.rJ_GroupBox_領藥台_02.PointToScreen(Point.Empty).X + this.rJ_GroupBox_領藥台_02.Width + 20, 1);
                    }));
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_02_時間重置();
                        break;
                    }
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    double 庫存量 = Function_從SQL取得庫存(藥碼);
                    double 差異值 = medRecheckLogClass.get_unresolved_qty_by_code(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, 藥碼);
                    結存量 = 庫存量 + 總異動量 + 差異值;

                    if (結存量 == dialog_NumPannel.Value)
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\盲盤數量錯誤.wav");
                    if (retry == 0)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("請再次覆盤", 2000);
                        dialog_錯誤提示.ShowDialog();
                    }
                    if (retry == 1)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"異常紀錄,盤點值 : {dialog_NumPannel.Value}", 2000);
                        dialog_錯誤提示.ShowDialog();
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = "盤點異常";
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    try_error++;
                    retry++;
                }
            }
            if (list_取藥堆疊母資料_replace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
                cnt = 1;
                return;
            }
            cnt++;

        }
        void cnt_Program_領藥台_02_刷新領藥內容_檢查複盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                string 結存量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                double 總異動量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入複盤數量.wav");
                while (true)
                {
                    if (try_error == 1)
                    {
                        Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                        if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                        {
                            Fuction_領藥台_02_時間重置();
                            try_error = 0;
                        }
                        else
                        {
                            Fuction_領藥台_02_時間重置();
                            try_error++;
                        }
                        continue;
                    }
                    if (try_error == 2)
                    {
                        Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                        dialog_收支原因選擇.Title = $"複盤數量錯誤({結存量}) 選擇原因";
                        dialog_收支原因選擇.ShowDialog();
                        Fuction_領藥台_02_時間重置();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n複盤錯誤原因:{dialog_收支原因選擇.Value}";
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(明盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}\n交易量:{總異動量}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;
                    this.Invoke(new Action(delegate
                    {
                        //dialog_NumPannel.Location = new Point(this.rJ_GroupBox_領藥台_02.PointToScreen(Point.Empty).X + this.rJ_GroupBox_領藥台_02.Width + 20, 1);
                    }));
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_02_時間重置();
                        break;
                    }
                    Fuction_領藥台_02_時間重置();
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    if (結存量 == dialog_NumPannel.Value.ToString())
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\複盤數量錯誤.wav");
                    try_error++;

                }


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
        void cnt_Program_領藥台_02_刷新領藥內容_檢查作業完成(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            List<object[]> list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(this.領藥台_02名稱);
            List<object[]> list_取藥堆疊子資料_buf = new List<object[]>();
            List<object[]> list_取藥堆疊子資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.作業完成.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string Master_GUID = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                //if (Function_取藥堆疊資料_取得作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤))
                //{
                //    voice.SpeakOnTask("請輸入盤點數量");
                //    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"藥碼:{藥碼} 藥名:{藥名}  請輸入盤點數量");
                //    dialog_NumPannel.ShowDialog();
                //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                //    list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                //}

                list_取藥堆疊子資料_buf = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);
                for (int k = 0; k < list_取藥堆疊子資料_buf.Count; k++)
                {
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.調劑結束] = true.ToString();
                    list_取藥堆疊子資料_replace.Add(list_取藥堆疊子資料_buf[k]);
                }
            }
            if (list_取藥堆疊母資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
            if (list_取藥堆疊子資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊子資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_檢查是否需輸入效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.輸入新效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                Dialog_輸入效期 dialog = new Dialog_輸入效期();
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    cnt = 65500;
                    return;
                }
                string 效期 = dialog.效期.StringToDateTime().ToDateString(TypeConvert.Enum_Year_Type.Anno_Domini, "/");
                dialog.Dispose();
                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = 效期;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.新增效期.GetEnumName();

                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(enum_取藥堆疊母資料.GUID.GetEnumName(), GIUD, list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_檢查是否需選擇效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                string 藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 交易量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                List<string> list_效期 = new List<string>();
                List<string> list_效期_buf = new List<string>();
                List<string> list_批號 = new List<string>();
                List<string> list_數量 = new List<string>();

                List<object> list_device = Function_從SQL取得儲位到本地資料(藥品碼);
                if (list_device.Count == 0)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    continue;
                }
                for (int k = 0; k < list_device.Count; k++)
                {
                    Device device = list_device[k] as Device;
                    if (device != null)
                    {
                        for (int m = 0; m < device.List_Validity_period.Count; m++)
                        {
                            list_效期_buf = (from value in list_效期
                                           where value == device.List_Validity_period[m]
                                           select value).ToList();
                            if (list_效期_buf.Count == 0)
                            {
                                list_效期.Add(device.List_Validity_period[m]);
                                list_批號.Add(device.List_Lot_number[m]);
                                list_數量.Add(device.List_Inventory[m]);
                            }
                        }
                    }
                }
                Dialog_選擇效期 dialog = new Dialog_選擇效期(藥品碼, 藥品名稱, 交易量, list_效期, list_批號, list_數量);
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    voice.SpeakOnTask("請選擇效期");
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    cnt = 65500;
                    return;
                }

                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = dialog.Value;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();
                dialog.Dispose();
                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_檢查自動登出(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_02名稱);
            List<object[]> list_取藥堆疊資料_buf = new List<object[]>();
            list_取藥堆疊資料_buf = (from value in list_取藥堆疊資料
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.無儲位.GetEnumName()
                               select value
                                ).ToList();

            if (list_取藥堆疊資料.Count == 0 && plC_CheckBox_多醫令模式.Bool == false)
            {
                MyTimer_領藥台_02_閒置登出時間.TickStop();
                MyTimer_領藥台_02_閒置登出時間.StartTickTime();

                MyTimer_領藥台_02_入賬完成時間.TickStop();
                MyTimer_領藥台_02_入賬完成時間.StartTickTime();
            }
            else
            {
                if (list_取藥堆疊資料_buf.Count > 0)
                {
                    MyTimer_領藥台_02_閒置登出時間.TickStop();
                    MyTimer_領藥台_02_閒置登出時間.StartTickTime();

                    MyTimer_領藥台_02_入賬完成時間.TickStop();
                    MyTimer_領藥台_02_入賬完成時間.StartTickTime();
                }
                else
                {
                    MyTimer_領藥台_02_閒置登出時間.StartTickTime();
                    MyTimer_領藥台_02_入賬完成時間.StartTickTime();
                    if (PLC_Device_領藥台_02_閒置登出時間.Value != 0)
                    {
                        if ((PLC_Device_領藥台_02_閒置登出時間.Value - (int)MyTimer_領藥台_02_閒置登出時間.GetTickTime()) <= 20000)
                        {
                            myTimer_領藥台_02_Logout.StartTickTime(5000);
                            if (myTimer_領藥台_02_Logout.IsTimeOut())
                            {
                                myTimer_領藥台_02_Logout.TickStop();
                                Task.Run(new Action(delegate
                                {
                                    using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\logout.wav"))
                                    {
                                        sp.Stop();
                                        sp.Play();
                                        sp.PlaySync();
                                    }

                                }));
                            }
                        }
                    }

                }
            }
            this.MyTimer__領藥台_02_刷新領藥內容_刷新間隔.TickStop();
            this.MyTimer__領藥台_02_刷新領藥內容_刷新間隔.StartTickTime(100);
            cnt++;
        }
        void cnt_Program_領藥台_02_刷新領藥內容_等待刷新間隔(ref int cnt)
        {
            if (this.MyTimer__領藥台_02_刷新領藥內容_刷新間隔.IsTimeOut())
            {
                cnt++;
            }
        }
        #endregion

        #region Function
        private void Function_領藥台_02_醫令領藥(string BarCode)
        {
            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            bool flag_OK = true;

            MyTimer myTimer_total = new MyTimer();
            myTimer_total.StartTickTime(50000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;

            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(14).Year, DateTime.Now.AddDays(14).Month, DateTime.Now.AddDays(14).Day, 23, 59, 59);
            List<object[]> list_堆疊資料 = Function_取藥堆疊資料_取得母資料();
            List<object[]> list_堆疊資料_buf = new List<object[]>();
            Task Task_刪除資料 = Task.Run(() =>
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                if (!plC_CheckBox_多醫令模式.Bool)
                {
                    this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_02名稱);
                }
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            });

            Task Task_取得醫令 = Task.Run(() =>
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                List<object[]> list_醫令資料 = new List<object[]>();
                PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);
                if (plC_Button_手輸數量.Bool)
                {
                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入領藥數量");
                    DialogResult dialogResult = dialog_NumPannel.ShowDialog();
                    if (dialogResult != DialogResult.Yes) return;
                    list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, dialog_NumPannel.Value * -1);
                }
                else
                {
                    list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, PLC_Device_領藥台_02_單醫令模式.Bool);
                }
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                list_醫令資料 = (from temp in list_醫令資料
                             where Basic.TypeConvert.IsInDate(temp[(int)enum_醫囑資料.開方日期].StringToDateTime(), dateTime_start, dateTime_end)
                             || Basic.TypeConvert.IsInDate(temp[(int)enum_醫囑資料.展藥時間].StringToDateTime(), dateTime_start, dateTime_end)
                             select temp).ToList();
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單已過期.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單已過期", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                List<object[]> list_醫令資料_remove = new List<object[]>();


                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }

                for (int i = 0; i < list_醫令資料_remove.Count; i++)
                {
                    list_醫令資料.RemoveByGUID(list_醫令資料_remove[i]);
                }
                Console.Write($"取得醫令資料 , 耗時{myTimer.ToString()}\n");
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\找不到儲位.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找不到儲位", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                bool flag_雙人覆核 = false;

                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");

                List<string> Codes = (from temp in list_醫令資料
                                      select temp[(int)enum_醫囑資料.藥品碼].ObjectToString()).Distinct().ToList();
                List<medClass> medClasses = medClass.get_med_clouds_by_codes(API_Server, Codes);
                List<medClass> medClasses_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medcloud = medClasses.CoverToDictionaryByCode();

                for (int i = 0; i < list_醫令資料.Count; i++)
                {
                    string 藥品碼 = list_醫令資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                    string 藥品名稱 = list_醫令資料[i][(int)enum_醫囑資料.藥品名稱].ObjectToString();
                    string 單位 = list_醫令資料[i][(int)enum_醫囑資料.劑量單位].ObjectToString();

                    list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                    if (list_藥品資料_buf.Count != 0)
                    {
                        藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                        單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                    }

                    string GUID = Guid.NewGuid().ToString();
                    string 調劑台名稱 = this.領藥台_02名稱;
                    enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
                    string Order_GUID = list_醫令資料[i][(int)enum_醫囑資料.GUID].ObjectToString();

                    string 診別 = list_醫令資料[i][(int)enum_醫囑資料.藥局代碼].ObjectToString();
                    string 領藥號 = list_醫令資料[i][(int)enum_醫囑資料.領藥號].ObjectToString();
                    string 病房號 = list_醫令資料[i][(int)enum_醫囑資料.病房].ObjectToString();
                    string 藥袋序號 = list_醫令資料[i][(int)enum_醫囑資料.PRI_KEY].ObjectToString();
                    string 病歷號 = list_醫令資料[i][(int)enum_醫囑資料.病歷號].ObjectToString();
                    string 病人姓名 = list_醫令資料[i][(int)enum_醫囑資料.病人姓名].ObjectToString();
                    string 床號 = "";
                    string 開方時間 = list_醫令資料[i][(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                    if (開方時間.StringIsEmpty()) 開方時間 = list_醫令資料[i][(int)enum_醫囑資料.開方日期].ObjectToString();
                    string ID = 領藥台_02_ID;
                    string 操作人 = 領藥台_02_登入者姓名;
                    string 藥師證字號 = 領藥台_02_藥師證字號;
                    string 顏色 = 領藥台_02_顏色;
                    double 總異動量 = list_醫令資料[i][(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                    string 效期 = "";
                    string 收支原因 = "";



                    list_堆疊資料_buf = (from temp in list_堆疊資料
                                     where temp[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼
                                     where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != "刷新面板"
                                     where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != 調劑台名稱
                                     where temp[(int)enum_取藥堆疊母資料.操作人].ObjectToString() != 操作人
                                     select temp).ToList();



                    takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                    takeMedicineStackClass.GUID = GUID;
                    takeMedicineStackClass.Order_GUID = Order_GUID;
                    takeMedicineStackClass.動作 = 動作.GetEnumName();
                    takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                    takeMedicineStackClass.藥品碼 = 藥品碼;
                    takeMedicineStackClass.領藥號 = 領藥號;
                    takeMedicineStackClass.病房號 = 病房號;
                    takeMedicineStackClass.診別 = 診別;
                    takeMedicineStackClass.顏色 = 顏色;
                    if (list_堆疊資料_buf.Count > 0 && plC_CheckBox_同藥碼同時取藥亮紫色.Checked)
                    {
                        takeMedicineStackClass.顏色 = Color.Purple.ToColorString();
                    }
                    takeMedicineStackClass.藥品名稱 = 藥品名稱;
                    takeMedicineStackClass.藥袋序號 = 藥袋序號;
                    takeMedicineStackClass.病歷號 = 病歷號;
                    takeMedicineStackClass.病人姓名 = 病人姓名;
                    takeMedicineStackClass.床號 = 床號;
                    takeMedicineStackClass.開方時間 = 開方時間;
                    takeMedicineStackClass.操作人 = 操作人;
                    takeMedicineStackClass.ID = ID;
                    takeMedicineStackClass.藥師證字號 = 藥師證字號;
                    takeMedicineStackClass.總異動量 = 總異動量.ToString();
                    takeMedicineStackClass.效期 = 效期;
                    takeMedicineStackClass.收支原因 = 收支原因;
                    takeMedicineStackClass.ID = ID;

                    bool flag_檢查過帳 = false;
                    medClasses_buf = keyValuePairs_medcloud.SortDictionaryByCode(藥品碼);
                    if (medClasses_buf.Count > 0)
                    {
                        if (medClasses_buf[0].高價藥品.ToUpper() == true.ToString().ToUpper())
                        {
                            flag_檢查過帳 = true;
                        }
                        if (medClasses_buf[0].管制級別.StringIsEmpty() == false && medClasses_buf[0].管制級別 != "N")
                        {
                            flag_檢查過帳 = true;
                        }
                    }

                    if (pLC_Device.Bool == false || flag_檢查過帳 == true)
                    {
                        if (list_醫令資料[i][(int)enum_醫囑資料.狀態].ObjectToString() == enum_醫囑資料_狀態.已過帳.GetEnumName())
                        {
                            takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.已領用過.GetEnumName();
                        }

                    }
                    if (flag_雙人覆核)
                    {
                        this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                        continue;
                    }

                    takeMedicineStackClasses.Add(takeMedicineStackClass);

                }
            });
            List<Task> taskList = new List<Task>();
            taskList.Add(Task_刪除資料);
            taskList.Add(Task_取得醫令);
            Task.WhenAll(taskList).Wait();

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);

            Console.Write($"掃碼完成 , 總耗時{myTimer_total.ToString()}\n");
            if (flag_OK) Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
        private void Function_領藥台_02_醫令退藥(string BarCode)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            string 藥品碼 = "";
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);
            List<object[]> list_醫令資料 = new List<object[]>();

            if (plC_Button_手輸數量.Bool)
            {
                double 手輸數量 = 0;
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入退藥數量");
                DialogResult dialogResult = dialog_NumPannel.ShowDialog();
                if (dialogResult != DialogResult.Yes) return;
                手輸數量 = dialog_NumPannel.Value * 1;
                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, 手輸數量);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單已過期.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單已過期", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                object[] value = list_醫令資料[0];
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_02名稱);
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");

                string GUID = value[(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_02名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;
                藥品碼 = value[(int)enum_醫囑資料.藥品碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) return;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = "";
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = value[(int)enum_醫囑資料.病歷號].ObjectToString();
                string 診別 = value[(int)enum_醫囑資料.藥局代碼].ObjectToString();
                string 領藥號 = value[(int)enum_醫囑資料.領藥號].ObjectToString();
                string 病房號 = value[(int)enum_醫囑資料.病房].ObjectToString();
                string 床號 = "";
                string 病人姓名 = value[(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 開方時間 = value[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_02_ID;
                string 操作人 = 領藥台_02_登入者姓名;
                string 藥師證字號 = 領藥台_02_藥師證字號;
                string 顏色 = 領藥台_02_顏色;
                double 總異動量 = value[(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.領藥號 = 領藥號;
                takeMedicineStackClass.病房號 = 病房號;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }
            else
            {
                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, PLC_Device_領藥台_02_單醫令模式.Bool);

                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                List<object[]> list_醫令資料_remove = new List<object[]>();
                PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
                if (pLC_Device.Bool == false) list_醫令資料 = list_醫令資料.GetRows((int)enum_醫囑資料.狀態, enum_醫囑資料_狀態.已過帳.GetEnumName());
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無已過帳資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無已過帳資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                //for (int i = 0; i < list_醫令資料.Count; i++)
                //{
                //    藥品碼 = list_醫令資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                //    if (Function_從本地資料取得儲位(藥品碼).Count == 0)
                //    {
                //        list_醫令資料_remove.Add(list_醫令資料[i]);
                //    }
                //}
                for (int i = 0; i < list_醫令資料_remove.Count; i++)
                {
                    list_醫令資料.RemoveByGUID(list_醫令資料_remove[i]);
                }
                Console.Write($"取得醫令資料 , 耗時{myTimer.ToString()}\n");
                //if (list_醫令資料.Count == 0)
                //{
                //    Voice.MediaPlayAsync($@"{currentDirectory}\找不到儲位.wav");
                //    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找不到儲位", 1500);
                //    dialog_AlarmForm.ShowDialog();
                //    return;
                //}

                Dialog_醫令退藥 dialog_醫令退藥 = new Dialog_醫令退藥(list_醫令資料);
                if (dialog_醫令退藥.ShowDialog() != DialogResult.Yes) return;
                object[] value = dialog_醫令退藥.Value;
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_02名稱);
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");

                string GUID = value[(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_02名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;
                藥品碼 = value[(int)enum_醫囑資料.藥品碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) return;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = "";
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = value[(int)enum_醫囑資料.病歷號].ObjectToString();
                string 診別 = value[(int)enum_醫囑資料.藥局代碼].ObjectToString();
                string 床號 = "";
                string 病人姓名 = value[(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 開方時間 = value[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_02_ID;
                string 操作人 = 領藥台_02_登入者姓名;
                string 藥師證字號 = 領藥台_02_藥師證字號;
                string 顏色 = 領藥台_02_顏色;
                double 總異動量 = value[(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }

        }

        private void Function_領藥台_02_QRCode領藥(string[] Scanner02_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_02名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥袋序號].ObjectToString();
            string 病人姓名 = "";
            string ID = 領藥台_02_ID;
            string 操作人 = 領藥台_02_登入者姓名;
            string 顏色 = 領藥台_02_顏色;
            string 床號 = "";
            double 總異動量 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";

            string 藥品碼 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();
            string 頻次 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.頻次].ObjectToString();


            string[] _serchnames = new string[] { enum_交易記錄查詢資料.藥品碼.GetEnumName(), enum_交易記錄查詢資料.病歷號.GetEnumName(), enum_交易記錄查詢資料.開方時間.GetEnumName(), enum_交易記錄查詢資料.頻次.GetEnumName(), enum_交易記錄查詢資料.藥袋序號.GetEnumName() };
            string[] _serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 頻次, 藥袋序號 };

            bool flag_重複領藥 = false;
            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(_serchnames, _serchvalues, false);
            list_交易記錄 = (from value in list_交易記錄
                         where value[(int)enum_交易記錄查詢資料.交易量].ObjectToString().StringToDouble() < 0
                         select value).ToList();

            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (list_交易記錄.Count > 0 && !pLC_Device.Bool)
            {
                this.voice.SpeakOnTask("此藥單已領取過");
                if (MyMessageBox.ShowDialog("此藥單已領取過,是否重複領藥?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    flag_重複領藥 = true;
                }
                else return;
            }
            string[] serchnames = new string[] { enum_取藥堆疊母資料.藥品碼.GetEnumName(), enum_取藥堆疊母資料.病歷號.GetEnumName(), enum_取藥堆疊母資料.開方時間.GetEnumName(), enum_取藥堆疊母資料.藥袋序號.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 藥袋序號 };
            if (sqL_DataGridView_領藥台_02_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }


            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_02名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            if (flag_重複領藥) 總異動量 = 0;
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.頻次 = 頻次;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            //takeMedicineStackClass.藥師證字號 = 藥師證字號;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.ID = ID;

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
        private void Function_領藥台_02_QRCode退藥(string[] Scanner02_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_02名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = this.領藥台_02名稱;
            string 病人姓名 = "";
            string ID = 領藥台_02_ID;
            string 操作人 = 領藥台_02_登入者姓名;
            string 顏色 = 領藥台_02_顏色;
            double 總異動量 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";
            string 床號 = "";
            string 藥品碼 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner02_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();


            string[] serchnames = new string[] { enum_交易記錄查詢資料.藥品碼.GetEnumName(), enum_交易記錄查詢資料.病歷號.GetEnumName(), enum_交易記錄查詢資料.開方時間.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間 };

            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(serchnames, serchvalues, false);
            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_退藥不檢查是否掃碼領藥過.Checked)
            {
                if (list_交易記錄.Count == 0)
                {
                    this.voice.SpeakOnTask("查無領藥紀錄");
                    return;
                }
            }

            if (sqL_DataGridView_領藥台_02_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            總異動量 = dialog_NumPannel.Value;
            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_02名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.ID = ID;

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }

        private void Fuction_領藥台_02_時間重置()
        {
            MyTimer_領藥台_02_閒置登出時間.TickStop();
            MyTimer_領藥台_02_閒置登出時間.StartTickTime();
            MyTimer_領藥台_02_入賬完成時間.TickStop();
            MyTimer_領藥台_02_入賬完成時間.StartTickTime();
        }
        #endregion

        #region Event
        private void SqL_DataGridView_領藥台_02_領藥內容_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList = Function_領藥內容_重新排序(RowsList);
        }
        private void PlC_RJ_Button_領藥台_02_取消作業_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_02_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_02_藥品圖片.Image = null;
            //}));
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_02名稱);
            this.sqL_DataGridView_領藥台_02_領藥內容.ClearGrid();
        }
        private void SqL_DataGridView_領藥台_02_領藥內容_DataGridClearGridEvent()
        {
            Function_調劑作業_醫令資訊更新(2);
        }
        private void PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(MouseEventArgs mevent)
        {
            if (Function_檢查是否完成交班() == false)
            {
                MyMessageBox.ShowDialog("請先完成交班");
                return;
            }
            if (plC_RJ_Button_領藥台_02_登入.Texts == "登出")
            {
                PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(null);
                return;
            }
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
            if (this.PLC_Device_領藥台_02_已登入.Bool) return;
            if (textBox_領藥台_02_帳號.Texts.StringIsEmpty()) return;

            if (textBox_領藥台_02_帳號.Texts.ToUpper() == Admin_ID.ToUpper())
            {
                if (textBox_領藥台_02_密碼.Texts.ToUpper() == Admoin_Password.ToUpper())
                {
                    this.PLC_Device_領藥台_02_已登入.Bool = true;
                    領藥台_02_登入者姓名 = "最高管理權限";
                    領藥台_02_ID = "admin";
                    this.PLC_Device_最高權限.Bool = true;
                    return;
                }
            }

            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_人員資料.ID, textBox_領藥台_02_帳號.Texts);
            if (list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無此帳號", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("查無此帳號!");
                return;
            }
            string password = list_value[0][(int)enum_人員資料.密碼].ObjectToString();
            if (textBox_領藥台_02_密碼.Texts != password)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("密碼錯誤", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("密碼錯誤!");
                return;
            }
            領藥台_02_登入者姓名 = list_value[0][(int)enum_人員資料.姓名].ObjectToString();
            領藥台_02_ID = list_value[0][(int)enum_人員資料.ID].ObjectToString();
            領藥台_02_顏色 = list_value[0][(int)enum_人員資料.顏色].ObjectToString();
            領藥台_02_藥師證字號 = list_value[0][(int)enum_人員資料.藥師證字號].ObjectToString();
            this.PLC_Device_領藥台_02_已登入.Bool = true;
            if (mevent != null) Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.密碼登入, 領藥台_02_登入者姓名, "領藥台_02");
            string 狀態顯示 = "";
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_02_狀態顯示.GetAlignmentString(PLC_MultiStateDisplay.TextValue.Alignment.Left);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_02_狀態顯示.GetFontColorString(Color.Black, true);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_02_狀態顯示.GetFontString(new Font("微軟正黑體", 24F, FontStyle.Bold), true);
            //狀態顯示 += string.Format($"登入者姓名 : {領藥台_02_登入者姓名}");
            //this.plC_MultiStateDisplay_領藥台_02_狀態顯示.SetTextValue(PLC_Device_領藥台_02_狀態顯示_登入者姓名.GetAdress(), 狀態顯示);
            this.plC_Button_領藥台_02_領.Bool = true;
            this.plC_Button_領藥台_02_退.Bool = false;

            Console.WriteLine($"登入成功! ID : {領藥台_02_ID}, 名稱 : {領藥台_02_登入者姓名}");
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_02_帳號.Texts = "";
                textBox_領藥台_02_密碼.Texts = "";
                plC_RJ_Button_領藥台_02_登入.Texts = "登出";
                string text_temp = PLC_Device_領藥台_02_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                this.rJ_Lable_領藥台_02_Title.Text = $"    02. [{領藥台_02_登入者姓名}] {text_temp}";
                this.rJ_Lable_領藥台_02_Title.BorderColor = this.panel_工程模式_領藥台_02_顏色.BackColor;
                this.rJ_Lable_領藥台_02_Title.BackgroundColor = Color.GreenYellow;
                this.rJ_Lable_領藥台_02_Title.TextColor = Color.Black;
            }));
            commonSapceClasses = Function_取得共用區所有儲位();
            Voice.MediaPlayAsync($@"{currentDirectory}\登入成功.wav");
            PLC_Device_Scanner02_讀取藥單資料.Bool = false;
            PLC_Device_Scanner02_讀取藥單資料_OK.Bool = false;
            領藥台_02_醫令條碼 = "";

        }
        private void PlC_RJ_Button_領藥台_02_登出_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_02_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_02_藥品圖片.Image = null;
            //}));
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_02名稱);
            this.sqL_DataGridView_領藥台_02_領藥內容.ClearGrid();

            Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.登出, 領藥台_02_登入者姓名, "02.號使用者");
            領藥台_02_登入者姓名 = "None";
            this.PLC_Device_領藥台_02_已登入.Bool = false;
            this.PLC_Device_最高權限.Bool = false;
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_02_帳號.Texts = "";
                textBox_領藥台_02_密碼.Texts = "";
                plC_RJ_Button_領藥台_02_登入.Texts = "登入";
                this.rJ_Lable_領藥台_02_Title.Text = $"    02. [未登入]";
                this.rJ_Lable_領藥台_02_Title.BorderColor = Color.DimGray;
                this.rJ_Lable_領藥台_02_Title.BackgroundColor = Color.DimGray;
                this.rJ_Lable_領藥台_02_Title.TextColor = Color.White;

            }));
        }
        private void PlC_Button_領藥台_02_退_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_02_領.Bool = !Value;
            this.plC_Button_領藥台_02_退.Bool = Value;
        }
        private void PlC_Button_領藥台_02_領_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_02_領.Bool = Value;
            this.plC_Button_領藥台_02_退.Bool = !Value;
        }

        private void SqL_DataGridView_領藥台_02_領藥內容_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].Cells[(int)enum_取藥堆疊母資料.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].Cells[enum_取藥堆疊母資料.結存量.GetEnumName()].Value = "無";
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.已領用過.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.sqL_DataGridView_領藥台_02_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void TextBox_領藥台_02_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox_領藥台_02_密碼.Focus();
            }
        }
        private void TextBox_領藥台_02_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.PlC_RJ_Button_領藥台_02_登入_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
            }
        }
        private void PLC_Device_領藥台_02_已登入_ValueChangeEvent(object Value)
        {
            if ((bool)Value == true)
            {
                this.Invoke(new Action(delegate
                {
                    this.panel_Main.Collapse();
                }));
            }
        }
        private void SqL_DataGridView_領藥台_02_領藥內容_RowEnterEvent(object[] RowValue)
        {
            string 藥碼 = RowValue[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
            string 藥名 = RowValue[(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
            string 領藥號 = RowValue[(int)enum_取藥堆疊母資料.領藥號].ObjectToString();
            string 病歷號 = RowValue[(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
            string 開方時間 = RowValue[(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
            Function_調劑作業_醫令資訊更新(藥碼, 藥名, 領藥號, 病歷號, 開方時間, 2);
        }
        #endregion

        #endregion
        #region 領藥台_03
        MyTimer MyTimer_領藥台_03_閒置登出時間 = new MyTimer("D100");
        PLC_Device PLC_Device_領藥台_03_閒置登出時間 = new PLC_Device("D100");
        MyTimer MyTimer_領藥台_03_入賬完成時間 = new MyTimer("D101");
        PLC_Device PLC_Device_領藥台_03_入賬完成時間 = new PLC_Device("D101");
        int 領藥台_03_RFID站號 = 3;
        List<object> 領藥台_03_儲位 = new List<object>();
        List<object[]> 領藥台_03_領藥儲位資訊 = new List<object[]>();
        List<string[]> 領藥台_03_掃描BUFFER = new List<string[]>();
        PLC_Device PLC_Device_領藥台_03_登出 = new PLC_Device();
        FpMatchLib.FpMatchClass FpMatchClass_領藥台_03_指紋資訊;
        PLC_Device PLC_Device_領藥台_03_已登入 = new PLC_Device("S250");
        PLC_Device PLC_Device_領藥台_03_單醫令模式 = new PLC_Device("S260");
        PLC_Device PLC_Device_領藥台_03_狀態顯示_等待登入 = new PLC_Device("M5000");
        PLC_Device PLC_Device_領藥台_03_狀態顯示_登入者姓名 = new PLC_Device("M5001");

        public static string 領藥台_03_登入者姓名 = "";
        public static string 領藥台_03_ID = "";
        public static string 領藥台_03_卡號 = "";
        public static string _領藥台_03_顏色 = "";
        public string 領藥台_03_顏色
        {
            get
            {
                if (plC_CheckBox_掃碼顏色固定.Checked)
                {
                    this.Invoke(new Action(delegate
                    {
                        Color color = this.panel_工程模式_領藥台_03_顏色.BackColor;
                        if (color == Color.Black)
                        {
                            this.panel_工程模式_領藥台_03_顏色.BackColor = Color.Blue;
                            SaveConfig工程模式();
                        }
                        _領藥台_03_顏色 = color.ToColorString();
                    }));


                    return _領藥台_03_顏色;
                }
                else
                {
                    return _領藥台_03_顏色;
                }
            }
            set
            {
                _領藥台_03_顏色 = value;
            }
        }
        public static string 領藥台_03_一維碼 = "";
        public static string 領藥台_03_藥師證字號 = "";

        private string 領藥台_03_醫令條碼 = "";

        #region PLC_領藥台_03_狀態顯示


        PLC_Device PLC_Device_領藥台_03_狀態顯示 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_03_狀態顯示_OK = new PLC_Device("");
        MyTimer MyTimer_領藥台_03_狀態顯示_結束延遲 = new MyTimer();
        int cnt_Program_領藥台_03_狀態顯示 = 65534;
        void sub_Program_領藥台_03_狀態顯示()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" || true)
            {
                PLC_Device_領藥台_03_狀態顯示.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_03_狀態顯示.Bool = false;
            }
            if (cnt_Program_領藥台_03_狀態顯示 == 65534)
            {
                this.MyTimer_領藥台_03_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_03_狀態顯示.SetComment("PLC_領藥台_03_狀態顯示");
                PLC_Device_領藥台_03_狀態顯示_OK.SetComment("PLC_領藥台_03_狀態顯示_OK");
                PLC_Device_領藥台_03_狀態顯示.Bool = false;
                cnt_Program_領藥台_03_狀態顯示 = 65535;
            }
            if (cnt_Program_領藥台_03_狀態顯示 == 65535) cnt_Program_領藥台_03_狀態顯示 = 1;
            if (cnt_Program_領藥台_03_狀態顯示 == 1) cnt_Program_領藥台_03_狀態顯示_檢查按下(ref cnt_Program_領藥台_03_狀態顯示);
            if (cnt_Program_領藥台_03_狀態顯示 == 2) cnt_Program_領藥台_03_狀態顯示_初始化(ref cnt_Program_領藥台_03_狀態顯示);
            if (cnt_Program_領藥台_03_狀態顯示 == 3) cnt_Program_領藥台_03_狀態顯示 = 65500;
            if (cnt_Program_領藥台_03_狀態顯示 > 1) cnt_Program_領藥台_03_狀態顯示_檢查放開(ref cnt_Program_領藥台_03_狀態顯示);

            if (cnt_Program_領藥台_03_狀態顯示 == 65500)
            {
                this.MyTimer_領藥台_03_狀態顯示_結束延遲.TickStop();
                this.MyTimer_領藥台_03_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_03_狀態顯示.Bool = false;
                PLC_Device_領藥台_03_狀態顯示_OK.Bool = false;
                cnt_Program_領藥台_03_狀態顯示 = 65535;
            }
        }
        void cnt_Program_領藥台_03_狀態顯示_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_03_狀態顯示.Bool) cnt++;
        }
        void cnt_Program_領藥台_03_狀態顯示_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_03_狀態顯示.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_03_狀態顯示_初始化(ref int cnt)
        {
            if (!this.PLC_Device_領藥台_03_已登入.Bool)
            {
                PLC_Device_領藥台_03_狀態顯示_等待登入.Bool = true;
                PLC_Device_領藥台_03_狀態顯示_登入者姓名.Bool = false;
            }
            else
            {
                PLC_Device_領藥台_03_狀態顯示_等待登入.Bool = false;
                PLC_Device_領藥台_03_狀態顯示_登入者姓名.Bool = true;
            }
            cnt++;
        }


        #endregion

        #region PLC_領藥台_03_檢查登入

        PLC_Device PLC_Device_領藥台_03_檢查登入 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_03_檢查登入_OK = new PLC_Device("");


        int cnt_Program_領藥台_03_檢查登入 = 65534;
        void sub_Program_領藥台_03_檢查登入()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業") PLC_Device_領藥台_03_檢查登入.Bool = true;
            else PLC_Device_領藥台_03_檢查登入.Bool = false;
            if (cnt_Program_領藥台_03_檢查登入 == 65534)
            {
                PLC_Device_領藥台_03_檢查登入.SetComment("PLC_領藥台_03_檢查登入");
                PLC_Device_領藥台_03_檢查登入_OK.SetComment("PLC_Device_領藥台_03_檢查登入_OK");
                PLC_Device_領藥台_03_檢查登入.Bool = false;
                PLC_Device_領藥台_03_已登入.Bool = false;
                cnt_Program_領藥台_03_檢查登入 = 65535;
            }
            if (cnt_Program_領藥台_03_檢查登入 == 65535) cnt_Program_領藥台_03_檢查登入 = 1;
            if (cnt_Program_領藥台_03_檢查登入 == 1) cnt_Program_領藥台_03_檢查登入_檢查按下(ref cnt_Program_領藥台_03_檢查登入);
            if (cnt_Program_領藥台_03_檢查登入 == 2) cnt_Program_領藥台_03_檢查登入_初始化(ref cnt_Program_領藥台_03_檢查登入);
            if (cnt_Program_領藥台_03_檢查登入 == 3) cnt_Program_領藥台_03_檢查登入_外部設備資料或帳號密碼登入(ref cnt_Program_領藥台_03_檢查登入);
            if (cnt_Program_領藥台_03_檢查登入 == 4) cnt_Program_領藥台_03_檢查登入 = 65500;
            if (cnt_Program_領藥台_03_檢查登入 > 1) cnt_Program_領藥台_03_檢查登入_檢查放開(ref cnt_Program_領藥台_03_檢查登入);

            if (cnt_Program_領藥台_03_檢查登入 == 65500)
            {
                PLC_Device_領藥台_03_檢查登入.Bool = false;
                cnt_Program_領藥台_03_檢查登入 = 65535;
            }
        }
        void cnt_Program_領藥台_03_檢查登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_03_檢查登入.Bool) cnt++;
        }
        void cnt_Program_領藥台_03_檢查登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_03_檢查登入.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_03_檢查登入_初始化(ref int cnt)
        {
            PLC_Device_領藥台_03_檢查登入_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_03_檢查登入_外部設備資料或帳號密碼登入(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (Dialog_使用者登入.IsShown)
            {
                if (MySerialPort_Scanner03.ReadByte() != null)
                {
                    System.Threading.Thread.Sleep(50);
                    string text = MySerialPort_Scanner03.ReadString();
                    MySerialPort_Scanner03.ClearReadByte();
                    if (text == null) return;
                    text = text.Replace("\0", "");
                    if (text.Length <= 2 || text.Length > 30) return;
                    if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                    領藥台_03_一維碼 = text;
                }
                return;
            }
            string UID = this.rfiD_FX600_UI.Get_RFID_UID(領藥台_03_RFID站號);
            if (!UID.StringIsEmpty() && UID.StringToInt32() != 0)
            {
                Console.WriteLine($"成功讀取RFID  {UID}");
                領藥台_03_卡號 = UID;
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.卡號.GetEnumName(), 領藥台_03_卡號, false);
                if (list_人員資料.Count == 0) return;
                Console.WriteLine($"取得人員資料完成!");
                if (!PLC_Device_領藥台_03_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_03_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_03_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(null);
                    }));
                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_03_登入者姓名, "03.號使用者");

                }
                else
                {
                    if (領藥台_03_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_03_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_03_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_03_登入者姓名, "03.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (MySerialPort_Scanner03.ReadByte() != null && !PLC_Device_領藥台_03_已登入.Bool)
            {
                System.Threading.Thread.Sleep(50);
                string text = MySerialPort_Scanner03.ReadString();
                MySerialPort_Scanner03.ClearReadByte();
                if (text == null) return;
                text = text.Replace("\0", "");
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r", "");
                text = text.Replace("\n", "");
                領藥台_03_一維碼 = text;

                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), 領藥台_03_一維碼, false);
                if (list_人員資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\查無此一維碼.wav");
                    return;
                }
                if (!PLC_Device_領藥台_03_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_03_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_03_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_03_登入者姓名, "03.號使用者");
                }
                else
                {
                    if (領藥台_03_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_03_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_03_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_03_登入者姓名, "03.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (FpMatchClass_領藥台_03_指紋資訊 != null)
            {
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
                object[] value = null;
                for (int i = 0; i < list_人員資料.Count; i++)
                {
                    string feature = list_人員資料[i][(int)enum_人員資料.指紋辨識].ObjectToString();
                    if (fpMatchSoket.Match(FpMatchClass_領藥台_03_指紋資訊.feature, feature))
                    {
                        value = list_人員資料[i];
                        break;
                    }
                }
                FpMatchClass_領藥台_03_指紋資訊 = null;
                if (value == null)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找無符合指紋資訊", 2000);
                    dialog_AlarmForm.ShowDialog();
                    cnt = 65500;
                    return;
                }
                if (!PLC_Device_領藥台_03_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_03_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_03_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_03_登入者姓名, "01.號使用者");
                }
                else
                {
                    if (領藥台_03_ID != value[(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_03_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_03_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_03_登入者姓名, "01.號使用者");
                    }
                }
            }
            cnt = 65500;
            return;


        }

        #endregion
        #region PLC_領藥台_03_檢查輸入資料

        PLC_Device PLC_Device_領藥台_03_檢查輸入資料 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_03_檢查輸入資料_OK = new PLC_Device("");

        MyTimer MyTimer_領藥台_03_檢查輸入資料_NG訊息延遲 = new MyTimer();
        int cnt_Program_領藥台_03_檢查輸入資料 = 65534;
        void sub_Program_領藥台_03_檢查輸入資料()
        {
            if ((this.plC_ScreenPage_Main.PageText == "調劑作業" || true) && PLC_Device_領藥台_03_已登入.Bool)
            {
                PLC_Device_領藥台_03_檢查輸入資料.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_03_檢查輸入資料.Bool = false;
            }
            if (Dialog_使用者登入.IsShown)
            {
                PLC_Device_領藥台_03_檢查輸入資料.Bool = false;
            }
            if (cnt_Program_領藥台_03_檢查輸入資料 == 65534)
            {
                PLC_Device_領藥台_03_檢查輸入資料.SetComment("PLC_領藥台_03_檢查輸入資料");
                PLC_Device_領藥台_03_檢查輸入資料_OK.SetComment("PLC_Device_領藥台_03_檢查輸入資料_OK");
                PLC_Device_領藥台_03_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_03_檢查輸入資料 = 65535;
            }
            if (cnt_Program_領藥台_03_檢查輸入資料 == 65535) cnt_Program_領藥台_03_檢查輸入資料 = 1;
            if (cnt_Program_領藥台_03_檢查輸入資料 == 1) cnt_Program_領藥台_03_檢查輸入資料_檢查按下(ref cnt_Program_領藥台_03_檢查輸入資料);
            if (cnt_Program_領藥台_03_檢查輸入資料 == 2) cnt_Program_領藥台_03_檢查輸入資料_初始化(ref cnt_Program_領藥台_03_檢查輸入資料);
            if (cnt_Program_領藥台_03_檢查輸入資料 == 3) cnt_Program_領藥台_03_檢查輸入資料_設定開始掃描(ref cnt_Program_領藥台_03_檢查輸入資料);
            if (cnt_Program_領藥台_03_檢查輸入資料 == 4) cnt_Program_領藥台_03_檢查輸入資料_等待掃描結束(ref cnt_Program_領藥台_03_檢查輸入資料);
            if (cnt_Program_領藥台_03_檢查輸入資料 == 5) cnt_Program_領藥台_03_檢查輸入資料_檢查醫令資料及寫入(ref cnt_Program_領藥台_03_檢查輸入資料);
            if (cnt_Program_領藥台_03_檢查輸入資料 == 6) cnt_Program_領藥台_03_檢查輸入資料 = 65500;


            if (cnt_Program_領藥台_03_檢查輸入資料 > 1) cnt_Program_領藥台_03_檢查輸入資料_檢查放開(ref cnt_Program_領藥台_03_檢查輸入資料);

            if (cnt_Program_領藥台_03_檢查輸入資料 == 65500)
            {
                PLC_Device_領藥台_03_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_03_檢查輸入資料 = 65535;
            }
        }
        void cnt_Program_領藥台_03_檢查輸入資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_03_檢查輸入資料.Bool) cnt++;
        }
        void cnt_Program_領藥台_03_檢查輸入資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_03_檢查輸入資料.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_03_檢查輸入資料_初始化(ref int cnt)
        {
            //PLC_Device_Scanner03_讀取藥單資料.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_03_檢查輸入資料_設定開始掃描(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner03_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner03_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner03_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
            else
            {
                if (!PLC_Device_Scanner03_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner03_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner03_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_03_檢查輸入資料_等待掃描結束(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner03_讀取藥單資料.Bool || PLC_Device_Scanner03_讀取藥單資料_OK.Bool)
                {
                    if (PLC_Device_Scanner03_讀取藥單資料_OK.Bool)
                    {
                        cnt++;
                        return;
                    }
                    else
                    {
                        this.voice.SpeakOnTask("掃碼失敗");
                        cnt = 65500;
                        return;
                    }
                }
            }
            else
            {
                if (!PLC_Device_Scanner03_讀取藥單資料.Bool || PLC_Device_Scanner03_讀取藥單資料_OK.Bool)
                {
                    if (領藥台_03_醫令條碼.Length < 15)
                    {
                        string text = 領藥台_03_醫令條碼.Replace("\n", "");
                        text = text.Replace("\r", "");
                        if (text.StringIsEmpty())
                        {
                            cnt = 65500;
                            return;
                        }
                        Console.WriteLine($"{text}");

                        if (text == QR_Code_醫令模式切換)
                        {
                            PLC_Device_領藥台_03_單醫令模式.Bool = !PLC_Device_領藥台_03_單醫令模式.Bool;

                            string text_temp = PLC_Device_領藥台_03_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                            Console.WriteLine($"切換模式至{text_temp}");
                            Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"切換模式至{text_temp}", 1000, 0, 0);
                            this.Invoke(new Action(delegate
                            {

                                this.rJ_Lable_領藥台_03_Title.Text = $"    03. [{領藥台_03_登入者姓名}] {text_temp}";
                                this.rJ_Lable_領藥台_03_Title.BorderColor = this.panel_工程模式_領藥台_03_顏色.BackColor;
                                this.rJ_Lable_領藥台_03_Title.BackgroundColor = Color.GreenYellow;
                                this.rJ_Lable_領藥台_03_Title.TextColor = Color.Black;
                            }));
                            dialog_AlarmForm.ShowDialog();
                            cnt = 65500;
                            return;
                        }
                        List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), text, false);
                        if (list_人員資料.Count > 0)
                        {
                            if (領藥台_03_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                            {
                                this.PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(null);

                                this.Invoke(new Action(delegate
                                {
                                    textBox_領藥台_03_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                                    textBox_領藥台_03_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                                    this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(null);
                                }));
                                Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_03_登入者姓名, "03.號使用者");

                            }
                            cnt = 65500;
                            return;
                        }
                    }
                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_03_檢查輸入資料_檢查醫令資料及寫入(ref int cnt)
        {
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (plC_Button_領藥台_03_領.Bool)
                {
                    this.Function_領藥台_03_QRCode領藥(this.Scanner03_讀取藥單資料_Array);
                }
                else if (plC_Button_領藥台_03_退.Bool)
                {
                    this.Function_領藥台_03_QRCode退藥(this.Scanner03_讀取藥單資料_Array);
                }
                cnt++;
            }
            else
            {
                if (plC_Button_領藥台_03_領.Bool)
                {
                    this.Function_領藥台_03_醫令領藥(領藥台_03_醫令條碼);
                }
                else if (plC_Button_領藥台_03_退.Bool)
                {
                    this.Function_領藥台_03_醫令退藥(領藥台_03_醫令條碼);
                }
                cnt++;
            }

        }



        #endregion
        #region PLC_領藥台_03_刷新領藥內容
        PLC_Device PLC_Device_領藥台_03_刷新領藥內容 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_03_刷新領藥內容_OK = new PLC_Device("");
        MyTimer MyTimer__領藥台_03_刷新領藥內容_刷新間隔 = new MyTimer();
        int cnt_Program_領藥台_03_刷新領藥內容 = 65534;
        void sub_Program_領藥台_03_刷新領藥內容()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" || true)
            {
                PLC_Device_領藥台_03_刷新領藥內容.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_03_刷新領藥內容.Bool = false;
            }
            PLC_Device_領藥台_03_刷新領藥內容.Bool = (this.plC_ScreenPage_Main.PageText == "調劑作業");
            if (cnt_Program_領藥台_03_刷新領藥內容 == 65534)
            {
                PLC_Device_領藥台_03_刷新領藥內容.SetComment("PLC_領藥台_03_刷新領藥內容");
                PLC_Device_領藥台_03_刷新領藥內容_OK.SetComment("PLC_領藥台_03_刷新領藥內容_OK");
                PLC_Device_領藥台_03_刷新領藥內容.Bool = false;
                cnt_Program_領藥台_03_刷新領藥內容 = 65535;
            }
            if (cnt_Program_領藥台_03_刷新領藥內容 == 65535) cnt_Program_領藥台_03_刷新領藥內容 = 1;
            if (cnt_Program_領藥台_03_刷新領藥內容 == 1) cnt_Program_領藥台_03_刷新領藥內容_檢查按下(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 2) cnt_Program_領藥台_03_刷新領藥內容_初始化(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 3) cnt_Program_領藥台_03_刷新領藥內容_取得資料(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 4) cnt_Program_領藥台_03_刷新領藥內容_檢查雙人覆核(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 5) cnt_Program_領藥台_03_刷新領藥內容_檢查盲盤作業(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 6) cnt_Program_領藥台_03_刷新領藥內容_檢查複盤作業(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 7) cnt_Program_領藥台_03_刷新領藥內容_檢查作業完成(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 8) cnt_Program_領藥台_03_刷新領藥內容_檢查是否需輸入效期(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 9) cnt_Program_領藥台_03_刷新領藥內容_檢查是否需選擇效期(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 10) cnt_Program_領藥台_03_刷新領藥內容_檢查自動登出(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 11) cnt_Program_領藥台_03_刷新領藥內容_等待刷新間隔(ref cnt_Program_領藥台_03_刷新領藥內容);
            if (cnt_Program_領藥台_03_刷新領藥內容 == 12) cnt_Program_領藥台_03_刷新領藥內容 = 65500;
            if (cnt_Program_領藥台_03_刷新領藥內容 > 1) cnt_Program_領藥台_03_刷新領藥內容_檢查放開(ref cnt_Program_領藥台_03_刷新領藥內容);

            if (cnt_Program_領藥台_03_刷新領藥內容 == 65500)
            {
                PLC_Device_領藥台_03_刷新領藥內容.Bool = false;
                PLC_Device_領藥台_03_刷新領藥內容_OK.Bool = false;
                cnt_Program_領藥台_03_刷新領藥內容 = 65535;
            }
        }
        void cnt_Program_領藥台_03_刷新領藥內容_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_03_刷新領藥內容.Bool) cnt++;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_03_刷新領藥內容.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_取得資料(ref int cnt)
        {
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            List<object[]> list_取藥堆疊資料_replace = new List<object[]>();
            string GUID = "";
            string 序號 = "";
            string 動作 = "";
            string 藥袋序號 = "";
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 病歷號 = "";
            string 操作時間 = "";
            string 開方時間 = "";
            string 庫存量 = "";
            string 總異動量 = "";
            string 結存量 = "";
            string 單位 = "";
            string 狀態 = "";
            string 床號 = "";
            list_取藥堆疊資料.Sort(new Icp_取藥堆疊母資料_index排序());

            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示))
                {
                    this.voice.SpeakOnTask("庫存不足");
                    this.Function_取藥堆疊資料_設定作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示, false);
                    list_取藥堆疊資料_replace.Add(list_取藥堆疊資料[i]);
                }
                GUID = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                序號 = (i + 1).ToString();
                動作 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.動作].ObjectToString();
                藥袋序號 = $"{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥袋序號].ObjectToString()}:{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.頻次].ObjectToString()}";
                藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                病歷號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
                操作時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString();
                開方時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
                庫存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.庫存量].ObjectToString();
                總異動量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString();
                結存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                單位 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.單位].ObjectToString();
                狀態 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString();
                床號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.床號].ObjectToString();
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.盲盤))
                {
                    庫存量 = "無";
                    結存量 = "無";
                }
                object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                value[(int)enum_取藥堆疊母資料.GUID] = GUID;
                value[(int)enum_取藥堆疊母資料.序號] = 序號;
                value[(int)enum_取藥堆疊母資料.動作] = 動作;
                value[(int)enum_取藥堆疊母資料.藥袋序號] = 藥袋序號;
                value[(int)enum_取藥堆疊母資料.藥品碼] = 藥品碼;
                value[(int)enum_取藥堆疊母資料.藥品名稱] = 藥品名稱;
                value[(int)enum_取藥堆疊母資料.病歷號] = 病歷號;
                value[(int)enum_取藥堆疊母資料.操作時間] = 操作時間;
                value[(int)enum_取藥堆疊母資料.開方時間] = 開方時間;
                value[(int)enum_取藥堆疊母資料.庫存量] = 庫存量;
                value[(int)enum_取藥堆疊母資料.總異動量] = 總異動量;
                value[(int)enum_取藥堆疊母資料.結存量] = 結存量;
                value[(int)enum_取藥堆疊母資料.單位] = 單位;
                value[(int)enum_取藥堆疊母資料.狀態] = 狀態;
                value[(int)enum_取藥堆疊母資料.床號] = 床號;

                list_value.Add(value);


            }

            if (plC_CheckBox_調劑畫面合併相同藥品.Bool)
            {
                List<object[]> list_value_new = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();
                string[] 藥品碼_array = (from value in list_value
                                      select value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString()).Distinct().ToList().ToArray();
                for (int i = 0; i < 藥品碼_array.Length; i++)
                {
                    list_value_buf = (from value in list_value
                                      where value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼_array[i]
                                      select value).ToList();
                    if (list_value_buf.Count == 0) continue;
                    object[] value_領藥內容 = new object[new enum_取藥堆疊母資料().GetLength()];
                    value_領藥內容[(int)enum_取藥堆疊母資料.GUID] = list_value_buf[0][(int)enum_取藥堆疊母資料.GUID];
                    value_領藥內容[(int)enum_取藥堆疊母資料.序號] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.動作] = 動作;
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥袋序號] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥袋序號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品碼] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品碼];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品名稱] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品名稱];
                    value_領藥內容[(int)enum_取藥堆疊母資料.病歷號] = list_value_buf[0][(int)enum_取藥堆疊母資料.病歷號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.操作時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.操作時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.開方時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.開方時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.庫存量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.結存量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.單位] = list_value_buf[0][(int)enum_取藥堆疊母資料.單位];
                    value_領藥內容[(int)enum_取藥堆疊母資料.床號] = list_value_buf[0][(int)enum_取藥堆疊母資料.床號];
                    double 總異動量_temp = 0;
                    bool flag_入賬完成 = true;
                    bool flag_無儲位 = false;
                    bool flag_庫存不足 = false;
                    for (int k = 0; k < list_value_buf.Count; k++)
                    {
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                        {
                            flag_入賬完成 = false;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                        {
                            flag_無儲位 = true;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                        {
                            flag_庫存不足 = true;
                        }
                        總異動量_temp += list_value_buf[k][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();
                    }
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = 總異動量_temp;
                    if (flag_入賬完成)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName();
                    }
                    else if (flag_無儲位)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    }
                    else if (flag_庫存不足)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName();
                    }
                    else
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                    }
                    list_value_new.Add(value_領藥內容);

                }
                for (int i = 0; i < list_value_new.Count; i++)
                {
                    list_value_new[i][(int)enum_取藥堆疊母資料.序號] = (i + 1).ToString();
                    藥品碼 = list_value_new[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    double 庫存量_temp = Function_從SQL取得庫存(藥品碼);
                    double 結存量_temp = 庫存量_temp + list_value_new[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();

                    list_value_new[i][(int)enum_取藥堆疊母資料.庫存量] = 庫存量_temp.ToString();
                    list_value_new[i][(int)enum_取藥堆疊母資料.結存量] = 結存量_temp.ToString();

                }
                list_value = list_value_new;
            }
            this.sqL_DataGridView_領藥台_03_領藥內容.RefreshGrid(list_value);
            Application.DoEvents();
            if (list_取藥堆疊資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_檢查雙人覆核(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                Application.DoEvents();
                Dialog_使用者登入 dialog_使用者登入 = new Dialog_使用者登入(領藥台_03_ID, 藥名);
                this.Invoke(new Action(delegate
                {
                    dialog_使用者登入.Location = new Point(this.rJ_Pannel_領藥台_03.PointToScreen(Point.Empty).X + this.rJ_Pannel_領藥台_03.Width + 20, 1);
                }));

                if (dialog_使用者登入.ShowDialog() != DialogResult.Yes)
                {
                    list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料[i]);
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                    Fuction_領藥台_03_時間重置();
                    continue;
                }
                Fuction_領藥台_03_時間重置();
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師姓名] = dialog_使用者登入.UserName;
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師ID] = dialog_使用者登入.UserID;
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
        void cnt_Program_領藥台_03_刷新領藥內容_檢查盲盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                int retry = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 總異動量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                double 結存量 = 0;
                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入盲盤數量.wav"); ;
                while (true)
                {
                    //if (try_error == 1)
                    //{
                    //    Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                    //    if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                    //    {
                    //        Fuction_領藥台_03_時間重置();
                    //        try_error = 0;
                    //    }
                    //    else
                    //    {
                    //        Fuction_領藥台_03_時間重置();
                    //        try_error++;
                    //    }
                    //    continue;
                    //}
                    //if (try_error == 2)
                    //{
                    //    Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                    //    dialog_收支原因選擇.Title = $"盲盤數量錯誤({結存量}) 選擇原因";
                    //    dialog_收支原因選擇.ShowDialog();
                    //    Fuction_領藥台_03_時間重置();
                    //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n盲盤錯誤原因:{dialog_收支原因選擇.Value}";
                    //    Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                    //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                    //    list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                    //    break;
                    //}

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(盲盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;
                    this.Invoke(new Action(delegate
                    {
                        dialog_NumPannel.Location = new Point(this.rJ_Pannel_領藥台_03.PointToScreen(Point.Empty).X + this.rJ_Pannel_領藥台_03.Width + 20, 1);
                    }));
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_03_時間重置();
                        break;
                    }
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    double 庫存量 = Function_從SQL取得庫存(藥碼);
                    double 差異值 = medRecheckLogClass.get_unresolved_qty_by_code(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, 藥碼);
                    結存量 = 庫存量 + 總異動量 + 差異值;
                    if (結存量 == dialog_NumPannel.Value)
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\盲盤數量錯誤.wav");
                    if (retry == 0)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("請再次覆盤", 2000);
                        dialog_錯誤提示.ShowDialog();
                    }
                    if (retry == 1)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"異常紀錄,盤點值 : {dialog_NumPannel.Value}", 2000);
                        dialog_錯誤提示.ShowDialog();
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = "盤點異常";
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    try_error++;
                    retry++;
                }
            }
            if (list_取藥堆疊母資料_replace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
                cnt = 1;
                return;
            }
            cnt++;

        }
        void cnt_Program_領藥台_03_刷新領藥內容_檢查複盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                string 結存量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入複盤數量.wav");
                while (true)
                {
                    if (try_error == 1)
                    {
                        Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                        if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                        {
                            Fuction_領藥台_03_時間重置();
                            try_error = 0;
                        }
                        else
                        {
                            Fuction_領藥台_03_時間重置();
                            try_error++;
                        }
                        continue;
                    }
                    if (try_error == 2)
                    {
                        Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                        dialog_收支原因選擇.Title = $"複盤數量錯誤({結存量}) 選擇原因";
                        dialog_收支原因選擇.ShowDialog();
                        Fuction_領藥台_03_時間重置();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n複盤錯誤原因:{dialog_收支原因選擇.Value}";
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(明盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;
                    this.Invoke(new Action(delegate
                    {
                        dialog_NumPannel.Location = new Point(this.rJ_Pannel_領藥台_03.PointToScreen(Point.Empty).X + this.rJ_Pannel_領藥台_03.Width + 20, 1);
                    }));
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_03_時間重置();
                        break;
                    }
                    Fuction_領藥台_03_時間重置();
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    if (結存量 == dialog_NumPannel.Value.ToString())
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\複盤數量錯誤.wav");
                    try_error++;

                }


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
        void cnt_Program_領藥台_03_刷新領藥內容_檢查作業完成(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            List<object[]> list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(this.領藥台_03名稱);
            List<object[]> list_取藥堆疊子資料_buf = new List<object[]>();
            List<object[]> list_取藥堆疊子資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.作業完成.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string Master_GUID = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                //if (Function_取藥堆疊資料_取得作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤))
                //{
                //    voice.SpeakOnTask("請輸入盤點數量");
                //    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"藥碼:{藥碼} 藥名:{藥名}  請輸入盤點數量");
                //    dialog_NumPannel.ShowDialog();
                //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                //    list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                //}

                list_取藥堆疊子資料_buf = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);
                for (int k = 0; k < list_取藥堆疊子資料_buf.Count; k++)
                {
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.調劑結束] = true.ToString();
                    list_取藥堆疊子資料_replace.Add(list_取藥堆疊子資料_buf[k]);
                }
            }
            if (list_取藥堆疊母資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
            if (list_取藥堆疊子資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊子資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_檢查是否需輸入效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.輸入新效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                Dialog_輸入效期 dialog = new Dialog_輸入效期();
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    cnt = 65500;
                    return;
                }
                string 效期 = dialog.效期.StringToDateTime().ToDateString(TypeConvert.Enum_Year_Type.Anno_Domini, "/");
                dialog.Dispose();
                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = 效期;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.新增效期.GetEnumName();

                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(enum_取藥堆疊母資料.GUID.GetEnumName(), GIUD, list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_檢查是否需選擇效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                string 藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 交易量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                List<string> list_效期 = new List<string>();
                List<string> list_效期_buf = new List<string>();
                List<string> list_批號 = new List<string>();
                List<string> list_數量 = new List<string>();

                List<object> list_device = Function_從SQL取得儲位到本地資料(藥品碼);
                if (list_device.Count == 0)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    continue;
                }
                for (int k = 0; k < list_device.Count; k++)
                {
                    Device device = list_device[k] as Device;
                    if (device != null)
                    {
                        for (int m = 0; m < device.List_Validity_period.Count; m++)
                        {
                            list_效期_buf = (from value in list_效期
                                           where value == device.List_Validity_period[m]
                                           select value).ToList();
                            if (list_效期_buf.Count == 0)
                            {
                                list_效期.Add(device.List_Validity_period[m]);
                                list_批號.Add(device.List_Lot_number[m]);
                                list_數量.Add(device.List_Inventory[m]);
                            }
                        }
                    }
                }
                Dialog_選擇效期 dialog = new Dialog_選擇效期(藥品碼, 藥品名稱, 交易量, list_效期, list_批號, list_數量);
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    voice.SpeakOnTask("請選擇效期");
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    cnt = 65500;
                    return;
                }

                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = dialog.Value;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();
                dialog.Dispose();
                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_檢查自動登出(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_03名稱);
            List<object[]> list_取藥堆疊資料_buf = new List<object[]>();
            list_取藥堆疊資料_buf = (from value in list_取藥堆疊資料
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.無儲位.GetEnumName()
                               select value
                                ).ToList();

            if (list_取藥堆疊資料.Count == 0 && plC_CheckBox_多醫令模式.Bool == false)
            {
                MyTimer_領藥台_03_閒置登出時間.TickStop();
                MyTimer_領藥台_03_閒置登出時間.StartTickTime();

                MyTimer_領藥台_03_入賬完成時間.TickStop();
                MyTimer_領藥台_03_入賬完成時間.StartTickTime();
            }
            else
            {
                if (list_取藥堆疊資料_buf.Count > 0)
                {
                    MyTimer_領藥台_03_閒置登出時間.TickStop();
                    MyTimer_領藥台_03_閒置登出時間.StartTickTime();

                    MyTimer_領藥台_03_入賬完成時間.TickStop();
                    MyTimer_領藥台_03_入賬完成時間.StartTickTime();
                }
                else
                {
                    MyTimer_領藥台_03_閒置登出時間.StartTickTime();
                    MyTimer_領藥台_03_入賬完成時間.StartTickTime();
                    if (PLC_Device_領藥台_03_閒置登出時間.Value != 0)
                    {
                        if ((PLC_Device_領藥台_03_閒置登出時間.Value - (int)MyTimer_領藥台_03_閒置登出時間.GetTickTime()) <= 20000)
                        {
                            myTimer_領藥台_03_Logout.StartTickTime(5000);
                            if (myTimer_領藥台_03_Logout.IsTimeOut())
                            {
                                myTimer_領藥台_03_Logout.TickStop();
                                Task.Run(new Action(delegate
                                {
                                    using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\logout.wav"))
                                    {
                                        sp.Stop();
                                        sp.Play();
                                        sp.PlaySync();
                                    }

                                }));
                            }
                        }
                    }

                }
            }
            this.MyTimer__領藥台_03_刷新領藥內容_刷新間隔.TickStop();
            this.MyTimer__領藥台_03_刷新領藥內容_刷新間隔.StartTickTime(100);
            cnt++;
        }
        void cnt_Program_領藥台_03_刷新領藥內容_等待刷新間隔(ref int cnt)
        {
            if (this.MyTimer__領藥台_03_刷新領藥內容_刷新間隔.IsTimeOut())
            {
                cnt++;
            }
        }
        #endregion

        #region Function

        private void Function_領藥台_03_醫令領藥(string BarCode)
        {
            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            bool flag_OK = true;

            MyTimer myTimer_total = new MyTimer();
            myTimer_total.StartTickTime(50000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;

            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(14).Year, DateTime.Now.AddDays(14).Month, DateTime.Now.AddDays(14).Day, 23, 59, 59);
            List<object[]> list_堆疊資料 = Function_取藥堆疊資料_取得母資料();
            List<object[]> list_堆疊資料_buf = new List<object[]>();
            Task Task_刪除資料 = Task.Run(() =>
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                if (!plC_CheckBox_多醫令模式.Bool)
                {
                    this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_03名稱);
                }
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            });

            Task Task_取得醫令 = Task.Run(() =>
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                List<object[]> list_醫令資料 = new List<object[]>();
                PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);
                if (plC_Button_手輸數量.Bool)
                {
                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入領藥數量");
                    DialogResult dialogResult = dialog_NumPannel.ShowDialog();
                    if (dialogResult != DialogResult.Yes) return;
                    list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, dialog_NumPannel.Value * -1);
                }
                else
                {
                    list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, PLC_Device_領藥台_03_單醫令模式.Bool);
                }
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                list_醫令資料 = (from temp in list_醫令資料
                             where Basic.TypeConvert.IsInDate(temp[(int)enum_醫囑資料.開方日期].StringToDateTime(), dateTime_start, dateTime_end)
                             || Basic.TypeConvert.IsInDate(temp[(int)enum_醫囑資料.展藥時間].StringToDateTime(), dateTime_start, dateTime_end)
                             select temp).ToList();
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單已過期.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單已過期", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                List<object[]> list_醫令資料_remove = new List<object[]>();


                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }

                for (int i = 0; i < list_醫令資料_remove.Count; i++)
                {
                    list_醫令資料.RemoveByGUID(list_醫令資料_remove[i]);
                }
                Console.Write($"取得醫令資料 , 耗時{myTimer.ToString()}\n");
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\找不到儲位.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找不到儲位", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                bool flag_雙人覆核 = false;

                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");

                List<string> Codes = (from temp in list_醫令資料
                                      select temp[(int)enum_醫囑資料.藥品碼].ObjectToString()).Distinct().ToList();
                List<medClass> medClasses = medClass.get_med_clouds_by_codes(API_Server, Codes);
                List<medClass> medClasses_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medcloud = medClasses.CoverToDictionaryByCode();

                for (int i = 0; i < list_醫令資料.Count; i++)
                {
                    string 藥品碼 = list_醫令資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                    string 藥品名稱 = list_醫令資料[i][(int)enum_醫囑資料.藥品名稱].ObjectToString();
                    string 單位 = list_醫令資料[i][(int)enum_醫囑資料.劑量單位].ObjectToString();

                    list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                    if (list_藥品資料_buf.Count != 0)
                    {
                        藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                        單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                    }

                    string GUID = Guid.NewGuid().ToString();
                    string 調劑台名稱 = this.領藥台_03名稱;
                    enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
                    string Order_GUID = list_醫令資料[i][(int)enum_醫囑資料.GUID].ObjectToString();

                    string 診別 = list_醫令資料[i][(int)enum_醫囑資料.藥局代碼].ObjectToString();
                    string 領藥號 = list_醫令資料[i][(int)enum_醫囑資料.領藥號].ObjectToString();
                    string 病房號 = list_醫令資料[i][(int)enum_醫囑資料.病房].ObjectToString();
                    string 藥袋序號 = list_醫令資料[i][(int)enum_醫囑資料.PRI_KEY].ObjectToString();
                    string 病歷號 = list_醫令資料[i][(int)enum_醫囑資料.病歷號].ObjectToString();
                    string 病人姓名 = list_醫令資料[i][(int)enum_醫囑資料.病人姓名].ObjectToString();
                    string 床號 = "";
                    string 開方時間 = list_醫令資料[i][(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                    if (開方時間.StringIsEmpty()) 開方時間 = list_醫令資料[i][(int)enum_醫囑資料.開方日期].ObjectToString();
                    string ID = 領藥台_03_ID;
                    string 操作人 = 領藥台_03_登入者姓名;
                    string 藥師證字號 = 領藥台_03_藥師證字號;
                    string 顏色 = 領藥台_03_顏色;
                    double 總異動量 = list_醫令資料[i][(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                    string 效期 = "";
                    string 收支原因 = "";



                    list_堆疊資料_buf = (from temp in list_堆疊資料
                                     where temp[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼
                                     where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != "刷新面板"
                                     where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != 調劑台名稱
                                     where temp[(int)enum_取藥堆疊母資料.操作人].ObjectToString() != 操作人
                                     select temp).ToList();



                    takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                    takeMedicineStackClass.GUID = GUID;
                    takeMedicineStackClass.Order_GUID = Order_GUID;
                    takeMedicineStackClass.動作 = 動作.GetEnumName();
                    takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                    takeMedicineStackClass.藥品碼 = 藥品碼;
                    takeMedicineStackClass.領藥號 = 領藥號;
                    takeMedicineStackClass.病房號 = 病房號;
                    takeMedicineStackClass.診別 = 診別;
                    takeMedicineStackClass.顏色 = 顏色;
                    if (list_堆疊資料_buf.Count > 0 && plC_CheckBox_同藥碼同時取藥亮紫色.Checked)
                    {
                        takeMedicineStackClass.顏色 = Color.Purple.ToColorString();
                    }
                    takeMedicineStackClass.藥品名稱 = 藥品名稱;
                    takeMedicineStackClass.藥袋序號 = 藥袋序號;
                    takeMedicineStackClass.病歷號 = 病歷號;
                    takeMedicineStackClass.病人姓名 = 病人姓名;
                    takeMedicineStackClass.床號 = 床號;
                    takeMedicineStackClass.開方時間 = 開方時間;
                    takeMedicineStackClass.操作人 = 操作人;
                    takeMedicineStackClass.藥師證字號 = 藥師證字號;
                    takeMedicineStackClass.總異動量 = 總異動量.ToString();
                    takeMedicineStackClass.效期 = 效期;
                    takeMedicineStackClass.收支原因 = 收支原因;
                    takeMedicineStackClass.ID = ID;

                    bool flag_檢查過帳 = false;
                    medClasses_buf = keyValuePairs_medcloud.SortDictionaryByCode(藥品碼);
                    if (medClasses_buf.Count > 0)
                    {
                        if (medClasses_buf[0].高價藥品.ToUpper() == true.ToString().ToUpper())
                        {
                            flag_檢查過帳 = true;
                        }
                        if (medClasses_buf[0].管制級別.StringIsEmpty() == false && medClasses_buf[0].管制級別 != "N")
                        {
                            flag_檢查過帳 = true;
                        }
                    }

                    if (pLC_Device.Bool == false || flag_檢查過帳 == true)
                    {
                        if (list_醫令資料[i][(int)enum_醫囑資料.狀態].ObjectToString() == enum_醫囑資料_狀態.已過帳.GetEnumName())
                        {
                            takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.已領用過.GetEnumName();
                        }

                    }
                    if (flag_雙人覆核)
                    {
                        this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                        continue;
                    }

                    takeMedicineStackClasses.Add(takeMedicineStackClass);

                }
            });
            List<Task> taskList = new List<Task>();
            taskList.Add(Task_刪除資料);
            taskList.Add(Task_取得醫令);
            Task.WhenAll(taskList).Wait();

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);

            Console.Write($"掃碼完成 , 總耗時{myTimer_total.ToString()}\n");
            if (flag_OK) Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
        private void Function_領藥台_03_醫令退藥(string BarCode)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            string 藥品碼 = "";
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);
            List<object[]> list_醫令資料 = new List<object[]>();

            if (plC_Button_手輸數量.Bool)
            {
                double 手輸數量 = 0;
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入退藥數量");
                DialogResult dialogResult = dialog_NumPannel.ShowDialog();
                if (dialogResult != DialogResult.Yes) return;
                手輸數量 = dialog_NumPannel.Value * 1;
                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, 手輸數量);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單已過期.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單已過期", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                object[] value = list_醫令資料[0];
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_03名稱);
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");

                string GUID = value[(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_03名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;
                藥品碼 = value[(int)enum_醫囑資料.藥品碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) return;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = "";
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = value[(int)enum_醫囑資料.病歷號].ObjectToString();
                string 診別 = value[(int)enum_醫囑資料.藥局代碼].ObjectToString();
                string 領藥號 = value[(int)enum_醫囑資料.領藥號].ObjectToString();
                string 病房號 = value[(int)enum_醫囑資料.病房].ObjectToString();

                string 床號 = "";
                string 病人姓名 = value[(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 開方時間 = value[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_03_ID;
                string 操作人 = 領藥台_03_登入者姓名;
                string 藥師證字號 = 領藥台_03_藥師證字號;
                string 顏色 = 領藥台_03_顏色;
                double 總異動量 = value[(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.領藥號 = 領藥號;
                takeMedicineStackClass.病房號 = 病房號;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }
            else
            {
                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, PLC_Device_領藥台_03_單醫令模式.Bool);

                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                List<object[]> list_醫令資料_remove = new List<object[]>();
                PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
                if (pLC_Device.Bool == false) list_醫令資料 = list_醫令資料.GetRows((int)enum_醫囑資料.狀態, enum_醫囑資料_狀態.已過帳.GetEnumName());
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無已過帳資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無已過帳資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                for (int i = 0; i < list_醫令資料.Count; i++)
                {
                    藥品碼 = list_醫令資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                    if (Function_從本地資料取得儲位(藥品碼).Count == 0)
                    {
                        list_醫令資料_remove.Add(list_醫令資料[i]);
                    }
                }
                for (int i = 0; i < list_醫令資料_remove.Count; i++)
                {
                    list_醫令資料.RemoveByGUID(list_醫令資料_remove[i]);
                }
                Console.Write($"取得醫令資料 , 耗時{myTimer.ToString()}\n");
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\找不到儲位.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找不到儲位", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                Dialog_醫令退藥 dialog_醫令退藥 = new Dialog_醫令退藥(list_醫令資料);
                if (dialog_醫令退藥.ShowDialog() != DialogResult.Yes) return;
                object[] value = dialog_醫令退藥.Value;
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_03名稱);
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");

                string GUID = value[(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_03名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;
                藥品碼 = value[(int)enum_醫囑資料.藥品碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) return;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = "";
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = value[(int)enum_醫囑資料.病歷號].ObjectToString();
                string 診別 = value[(int)enum_醫囑資料.藥局代碼].ObjectToString();
                string 床號 = "";
                string 病人姓名 = value[(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 開方時間 = value[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_03_ID;
                string 操作人 = 領藥台_03_登入者姓名;
                string 藥師證字號 = 領藥台_03_藥師證字號;
                string 顏色 = 領藥台_03_顏色;
                double 總異動量 = value[(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }

        }

        private void Function_領藥台_03_QRCode領藥(string[] Scanner03_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_03名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥袋序號].ObjectToString();
            string 病人姓名 = "";
            string ID = 領藥台_03_ID;
            string 操作人 = 領藥台_03_登入者姓名;
            string 顏色 = 領藥台_03_顏色;
            string 床號 = "";
            double 總異動量 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";

            string 藥品碼 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();
            string 頻次 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.頻次].ObjectToString();


            string[] _serchnames = new string[] { enum_交易記錄查詢資料.藥品碼.GetEnumName(), enum_交易記錄查詢資料.病歷號.GetEnumName(), enum_交易記錄查詢資料.開方時間.GetEnumName(), enum_交易記錄查詢資料.頻次.GetEnumName(), enum_交易記錄查詢資料.藥袋序號.GetEnumName() };
            string[] _serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 頻次, 藥袋序號 };

            bool flag_重複領藥 = false;
            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(_serchnames, _serchvalues, false);
            list_交易記錄 = (from value in list_交易記錄
                         where value[(int)enum_交易記錄查詢資料.交易量].ObjectToString().StringToDouble() < 0
                         select value).ToList();

            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (list_交易記錄.Count > 0 && !pLC_Device.Bool)
            {
                this.voice.SpeakOnTask("此藥單已領取過");
                if (MyMessageBox.ShowDialog("此藥單已領取過,是否重複領藥?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    flag_重複領藥 = true;
                }
                else return;
            }
            string[] serchnames = new string[] { enum_取藥堆疊母資料.藥品碼.GetEnumName(), enum_取藥堆疊母資料.病歷號.GetEnumName(), enum_取藥堆疊母資料.開方時間.GetEnumName(), enum_取藥堆疊母資料.藥袋序號.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 藥袋序號 };
            if (sqL_DataGridView_領藥台_03_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }


            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_03名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            if (flag_重複領藥) 總異動量 = 0;
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.頻次 = 頻次;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            //takeMedicineStackClass.藥師證字號 = 藥師證字號;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.ID = ID;

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
        private void Function_領藥台_03_QRCode退藥(string[] Scanner03_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_03名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = this.領藥台_03名稱;
            string 病人姓名 = "";
            string ID = 領藥台_03_ID;
            string 操作人 = 領藥台_03_登入者姓名;
            string 顏色 = 領藥台_03_顏色;
            double 總異動量 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";
            string 床號 = "";
            string 藥品碼 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner03_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();


            string[] serchnames = new string[] { enum_交易記錄查詢資料.藥品碼.GetEnumName(), enum_交易記錄查詢資料.病歷號.GetEnumName(), enum_交易記錄查詢資料.開方時間.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間 };

            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(serchnames, serchvalues, false);
            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_退藥不檢查是否掃碼領藥過.Checked)
            {
                if (list_交易記錄.Count == 0)
                {
                    this.voice.SpeakOnTask("查無領藥紀錄");
                    return;
                }
            }

            if (sqL_DataGridView_領藥台_03_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            總異動量 = dialog_NumPannel.Value;
            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_03名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.ID = ID;

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }

        private void Fuction_領藥台_03_時間重置()
        {
            MyTimer_領藥台_03_閒置登出時間.TickStop();
            MyTimer_領藥台_03_閒置登出時間.StartTickTime();
            MyTimer_領藥台_03_入賬完成時間.TickStop();
            MyTimer_領藥台_03_入賬完成時間.StartTickTime();
        }
        #endregion

        #region Event
        private void SqL_DataGridView_領藥台_03_領藥內容_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList = Function_領藥內容_重新排序(RowsList);
        }
        private void PlC_RJ_Button_領藥台_03_取消作業_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_03_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_03_藥品圖片.Image = null;
            //}));
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_03名稱);
            this.sqL_DataGridView_領藥台_03_領藥內容.ClearGrid();
        }
        private void PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(MouseEventArgs mevent)
        {
            if (plC_RJ_Button_領藥台_03_登入.Texts == "登出")
            {
                PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(null);
                return;
            }
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
            if (this.PLC_Device_領藥台_03_已登入.Bool) return;
            if (textBox_領藥台_03_帳號.Texts.StringIsEmpty()) return;

            if (textBox_領藥台_03_帳號.Texts.ToUpper() == Admin_ID.ToUpper())
            {
                if (textBox_領藥台_03_密碼.Texts.ToUpper() == Admoin_Password.ToUpper())
                {
                    this.PLC_Device_領藥台_03_已登入.Bool = true;
                    領藥台_03_登入者姓名 = "最高管理權限";
                    領藥台_03_ID = "admin";
                    this.PLC_Device_最高權限.Bool = true;
                    return;
                }
            }

            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_人員資料.ID, textBox_領藥台_03_帳號.Texts);
            if (list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無此帳號", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("查無此帳號!");
                return;
            }
            string password = list_value[0][(int)enum_人員資料.密碼].ObjectToString();
            if (textBox_領藥台_03_密碼.Texts != password)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("密碼錯誤", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("密碼錯誤!");
                return;
            }
            領藥台_03_登入者姓名 = list_value[0][(int)enum_人員資料.姓名].ObjectToString();
            領藥台_03_ID = list_value[0][(int)enum_人員資料.ID].ObjectToString();
            領藥台_03_顏色 = list_value[0][(int)enum_人員資料.顏色].ObjectToString();
            領藥台_03_藥師證字號 = list_value[0][(int)enum_人員資料.藥師證字號].ObjectToString();
            this.PLC_Device_領藥台_03_已登入.Bool = true;
            if (mevent != null) Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.密碼登入, 領藥台_03_登入者姓名, "領藥台_03");
            string 狀態顯示 = "";
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_03_狀態顯示.GetAlignmentString(PLC_MultiStateDisplay.TextValue.Alignment.Left);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_03_狀態顯示.GetFontColorString(Color.Black, true);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_03_狀態顯示.GetFontString(new Font("微軟正黑體", 24F, FontStyle.Bold), true);
            //狀態顯示 += string.Format($"登入者姓名 : {領藥台_03_登入者姓名}");
            //this.plC_MultiStateDisplay_領藥台_03_狀態顯示.SetTextValue(PLC_Device_領藥台_03_狀態顯示_登入者姓名.GetAdress(), 狀態顯示);
            this.plC_Button_領藥台_03_領.Bool = true;
            this.plC_Button_領藥台_03_退.Bool = false;
            Console.WriteLine($"登入成功! ID : {領藥台_03_ID}, 名稱 : {領藥台_03_登入者姓名}");
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_03_帳號.Texts = "";
                textBox_領藥台_03_密碼.Texts = "";
                plC_RJ_Button_領藥台_03_登入.Texts = "登出";
                string text_temp = PLC_Device_領藥台_03_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                this.rJ_Lable_領藥台_03_Title.Text = $"    03. [{領藥台_03_登入者姓名}] {text_temp}";
                this.rJ_Lable_領藥台_03_Title.BorderColor = this.panel_工程模式_領藥台_03_顏色.BackColor;
                this.rJ_Lable_領藥台_03_Title.BackgroundColor = Color.GreenYellow;
                this.rJ_Lable_領藥台_03_Title.TextColor = Color.Black;
            }));
            commonSapceClasses = Function_取得共用區所有儲位();
            Voice.MediaPlayAsync($@"{currentDirectory}\登入成功.wav");
            PLC_Device_Scanner03_讀取藥單資料.Bool = false;
            PLC_Device_Scanner03_讀取藥單資料_OK.Bool = false;
            領藥台_03_醫令條碼 = "";
        }
        private void PlC_RJ_Button_領藥台_03_登出_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_03_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_03_藥品圖片.Image = null;
            //}));
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_03名稱);
            this.sqL_DataGridView_領藥台_03_領藥內容.ClearGrid();

            Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.登出, 領藥台_03_登入者姓名, "03.號使用者");
            領藥台_03_登入者姓名 = "None";
            this.PLC_Device_領藥台_03_已登入.Bool = false;
            this.PLC_Device_最高權限.Bool = false;
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_03_帳號.Texts = "";
                textBox_領藥台_03_密碼.Texts = "";
                plC_RJ_Button_領藥台_03_登入.Texts = "登入";
                this.rJ_Lable_領藥台_03_Title.Text = $"    03. [未登入]";
                this.rJ_Lable_領藥台_03_Title.BorderColor = Color.DimGray;
                this.rJ_Lable_領藥台_03_Title.BackgroundColor = Color.DimGray;
                this.rJ_Lable_領藥台_03_Title.TextColor = Color.White;

            }));
        }
        private void PlC_Button_領藥台_03_退_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_03_領.Bool = !Value;
            this.plC_Button_領藥台_03_退.Bool = Value;
        }
        private void PlC_Button_領藥台_03_領_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_03_領.Bool = Value;
            this.plC_Button_領藥台_03_退.Bool = !Value;
        }
        private void SqL_DataGridView_領藥台_03_領藥內容_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].Cells[(int)enum_取藥堆疊母資料.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].Cells[enum_取藥堆疊母資料.結存量.GetEnumName()].Value = "無";
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.已領用過.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.sqL_DataGridView_領藥台_03_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void TextBox_領藥台_03_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox_領藥台_03_密碼.Focus();
            }
        }
        private void TextBox_領藥台_03_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.PlC_RJ_Button_領藥台_03_登入_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
            }
        }
        private void PLC_Device_領藥台_03_已登入_ValueChangeEvent(object Value)
        {
            if ((bool)Value == true)
            {
                this.Invoke(new Action(delegate
                {
                    this.panel_Main.Collapse();
                }));
            }
        }
        #endregion

        #endregion
        #region 領藥台_04
        MyTimer MyTimer_領藥台_04_閒置登出時間 = new MyTimer("D100");
        PLC_Device PLC_Device_領藥台_04_閒置登出時間 = new PLC_Device("D100");
        MyTimer MyTimer_領藥台_04_入賬完成時間 = new MyTimer("D101");
        PLC_Device PLC_Device_領藥台_04_入賬完成時間 = new PLC_Device("D101");
        int 領藥台_04_RFID站號 = 4;
        List<object> 領藥台_04_儲位 = new List<object>();
        List<object[]> 領藥台_04_領藥儲位資訊 = new List<object[]>();
        List<string[]> 領藥台_04_掃描BUFFER = new List<string[]>();
        PLC_Device PLC_Device_領藥台_04_登出 = new PLC_Device();
        FpMatchLib.FpMatchClass FpMatchClass_領藥台_04_指紋資訊;
        PLC_Device PLC_Device_領藥台_04_已登入 = new PLC_Device("S400");
        PLC_Device PLC_Device_領藥台_04_單醫令模式 = new PLC_Device("S410");
        PLC_Device PLC_Device_領藥台_04_狀態顯示_等待登入 = new PLC_Device("M5000");
        PLC_Device PLC_Device_領藥台_04_狀態顯示_登入者姓名 = new PLC_Device("M5001");

        public static string 領藥台_04_登入者姓名 = "";
        public static string 領藥台_04_ID = "";
        public static string 領藥台_04_卡號 = "";
        public static string _領藥台_04_顏色 = "";
        public string 領藥台_04_顏色
        {
            get
            {
                if (plC_CheckBox_掃碼顏色固定.Checked)
                {
                    this.Invoke(new Action(delegate
                    {
                        Color color = this.panel_工程模式_領藥台_04_顏色.BackColor;
                        if (color == Color.Black)
                        {
                            this.panel_工程模式_領藥台_04_顏色.BackColor = Color.Blue;
                            SaveConfig工程模式();
                        }
                        _領藥台_04_顏色 = color.ToColorString();
                    }));


                    return _領藥台_04_顏色;
                }
                else
                {
                    return _領藥台_04_顏色;
                }
            }
            set
            {
                _領藥台_04_顏色 = value;
            }
        }
        public static string 領藥台_04_一維碼 = "";
        public static string 領藥台_04_藥師證字號 = "";

        private string 領藥台_04_醫令條碼 = "";

        #region PLC_領藥台_04_狀態顯示


        PLC_Device PLC_Device_領藥台_04_狀態顯示 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_04_狀態顯示_OK = new PLC_Device("");
        MyTimer MyTimer_領藥台_04_狀態顯示_結束延遲 = new MyTimer();
        int cnt_Program_領藥台_04_狀態顯示 = 65534;
        void sub_Program_領藥台_04_狀態顯示()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業" || true)
            {
                PLC_Device_領藥台_04_狀態顯示.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_04_狀態顯示.Bool = false;
            }
            if (cnt_Program_領藥台_04_狀態顯示 == 65534)
            {
                this.MyTimer_領藥台_04_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_04_狀態顯示.SetComment("PLC_領藥台_04_狀態顯示");
                PLC_Device_領藥台_04_狀態顯示_OK.SetComment("PLC_領藥台_04_狀態顯示_OK");
                PLC_Device_領藥台_04_狀態顯示.Bool = false;
                cnt_Program_領藥台_04_狀態顯示 = 65535;
            }
            if (cnt_Program_領藥台_04_狀態顯示 == 65535) cnt_Program_領藥台_04_狀態顯示 = 1;
            if (cnt_Program_領藥台_04_狀態顯示 == 1) cnt_Program_領藥台_04_狀態顯示_檢查按下(ref cnt_Program_領藥台_04_狀態顯示);
            if (cnt_Program_領藥台_04_狀態顯示 == 2) cnt_Program_領藥台_04_狀態顯示_初始化(ref cnt_Program_領藥台_04_狀態顯示);
            if (cnt_Program_領藥台_04_狀態顯示 == 3) cnt_Program_領藥台_04_狀態顯示 = 65500;
            if (cnt_Program_領藥台_04_狀態顯示 > 1) cnt_Program_領藥台_04_狀態顯示_檢查放開(ref cnt_Program_領藥台_04_狀態顯示);

            if (cnt_Program_領藥台_04_狀態顯示 == 65500)
            {
                this.MyTimer_領藥台_04_狀態顯示_結束延遲.TickStop();
                this.MyTimer_領藥台_04_狀態顯示_結束延遲.StartTickTime(10000);
                PLC_Device_領藥台_04_狀態顯示.Bool = false;
                PLC_Device_領藥台_04_狀態顯示_OK.Bool = false;
                cnt_Program_領藥台_04_狀態顯示 = 65535;
            }
        }
        void cnt_Program_領藥台_04_狀態顯示_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_04_狀態顯示.Bool) cnt++;
        }
        void cnt_Program_領藥台_04_狀態顯示_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_04_狀態顯示.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_04_狀態顯示_初始化(ref int cnt)
        {
            if (!this.PLC_Device_領藥台_04_已登入.Bool)
            {
                PLC_Device_領藥台_04_狀態顯示_等待登入.Bool = true;
                PLC_Device_領藥台_04_狀態顯示_登入者姓名.Bool = false;
            }
            else
            {
                PLC_Device_領藥台_04_狀態顯示_等待登入.Bool = false;
                PLC_Device_領藥台_04_狀態顯示_登入者姓名.Bool = true;
            }
            cnt++;
        }


        #endregion

        #region PLC_領藥台_04_檢查登入

        PLC_Device PLC_Device_領藥台_04_檢查登入 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_04_檢查登入_OK = new PLC_Device("");


        int cnt_Program_領藥台_04_檢查登入 = 65534;
        void sub_Program_領藥台_04_檢查登入()
        {
            if (this.plC_ScreenPage_Main.PageText == "調劑作業") PLC_Device_領藥台_04_檢查登入.Bool = true;
            else PLC_Device_領藥台_04_檢查登入.Bool = false;
            if (cnt_Program_領藥台_04_檢查登入 == 65534)
            {
                PLC_Device_領藥台_04_檢查登入.SetComment("PLC_領藥台_04_檢查登入");
                PLC_Device_領藥台_04_檢查登入_OK.SetComment("PLC_Device_領藥台_04_檢查登入_OK");
                PLC_Device_領藥台_04_檢查登入.Bool = false;
                PLC_Device_領藥台_04_已登入.Bool = false;
                cnt_Program_領藥台_04_檢查登入 = 65535;
            }
            if (cnt_Program_領藥台_04_檢查登入 == 65535) cnt_Program_領藥台_04_檢查登入 = 1;
            if (cnt_Program_領藥台_04_檢查登入 == 1) cnt_Program_領藥台_04_檢查登入_檢查按下(ref cnt_Program_領藥台_04_檢查登入);
            if (cnt_Program_領藥台_04_檢查登入 == 2) cnt_Program_領藥台_04_檢查登入_初始化(ref cnt_Program_領藥台_04_檢查登入);
            if (cnt_Program_領藥台_04_檢查登入 == 3) cnt_Program_領藥台_04_檢查登入_外部設備資料或帳號密碼登入(ref cnt_Program_領藥台_04_檢查登入);
            if (cnt_Program_領藥台_04_檢查登入 == 4) cnt_Program_領藥台_04_檢查登入 = 65500;
            if (cnt_Program_領藥台_04_檢查登入 > 1) cnt_Program_領藥台_04_檢查登入_檢查放開(ref cnt_Program_領藥台_04_檢查登入);

            if (cnt_Program_領藥台_04_檢查登入 == 65500)
            {
                PLC_Device_領藥台_04_檢查登入.Bool = false;
                cnt_Program_領藥台_04_檢查登入 = 65535;
            }
        }
        void cnt_Program_領藥台_04_檢查登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_04_檢查登入.Bool) cnt++;
        }
        void cnt_Program_領藥台_04_檢查登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_04_檢查登入.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_04_檢查登入_初始化(ref int cnt)
        {
            PLC_Device_領藥台_04_檢查登入_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_04_檢查登入_外部設備資料或帳號密碼登入(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (Dialog_使用者登入.IsShown)
            {
                if (MySerialPort_Scanner04.ReadByte() != null)
                {
                    System.Threading.Thread.Sleep(50);
                    string text = MySerialPort_Scanner04.ReadString();
                    MySerialPort_Scanner04.ClearReadByte();
                    if (text == null) return;
                    text = text.Replace("\0", "");
                    if (text.Length <= 2 || text.Length > 30) return;
                    if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                    領藥台_04_一維碼 = text;
                }
                return;
            }
            string UID = this.rfiD_FX600_UI.Get_RFID_UID(領藥台_04_RFID站號);
            if (!UID.StringIsEmpty() && UID.StringToInt32() != 0)
            {
                Console.WriteLine($"成功讀取RFID  {UID}");
                領藥台_04_卡號 = UID;
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.卡號.GetEnumName(), 領藥台_04_卡號, false);
                if (list_人員資料.Count == 0) return;
                Console.WriteLine($"取得人員資料完成!");
                if (!PLC_Device_領藥台_04_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_04_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_04_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(null);
                    }));
                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_04_登入者姓名, "04.號使用者");

                }
                else
                {
                    if (領藥台_04_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_04_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_04_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, 領藥台_04_登入者姓名, "04.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (MySerialPort_Scanner04.ReadByte() != null && !PLC_Device_領藥台_04_已登入.Bool)
            {
                System.Threading.Thread.Sleep(50);
                string text = MySerialPort_Scanner04.ReadString();
                MySerialPort_Scanner04.ClearReadByte();
                if (text == null) return;
                text = text.Replace("\0", "");
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r", "");
                text = text.Replace("\n", "");
                領藥台_04_一維碼 = text;

                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), 領藥台_04_一維碼, false);
                if (list_人員資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\查無此一維碼.wav");
                    return;
                }
                if (!PLC_Device_領藥台_04_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_04_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_04_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_04_登入者姓名, "04.號使用者");
                }
                else
                {
                    if (領藥台_04_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_04_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_04_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_04_登入者姓名, "04.號使用者");
                    }
                }
                cnt++;
                return;
            }
            else if (FpMatchClass_領藥台_04_指紋資訊 != null)
            {
                List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
                object[] value = null;
                for (int i = 0; i < list_人員資料.Count; i++)
                {
                    string feature = list_人員資料[i][(int)enum_人員資料.指紋辨識].ObjectToString();
                    if (fpMatchSoket.Match(FpMatchClass_領藥台_04_指紋資訊.feature, feature))
                    {
                        value = list_人員資料[i];
                        break;
                    }
                }
                FpMatchClass_領藥台_04_指紋資訊 = null;
                if (value == null)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找無符合指紋資訊", 2000);
                    dialog_AlarmForm.ShowDialog();
                    cnt = 65500;
                    return;
                }
                if (!PLC_Device_領藥台_04_已登入.Bool)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox_領藥台_04_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                        textBox_領藥台_04_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                        this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(null);
                    }));

                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_04_登入者姓名, "01.號使用者");
                }
                else
                {
                    if (領藥台_04_ID != value[(int)enum_人員資料.ID].ObjectToString())
                    {
                        this.PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(null);

                        this.Invoke(new Action(delegate
                        {
                            textBox_領藥台_04_帳號.Texts = value[(int)enum_人員資料.ID].ObjectToString();
                            textBox_領藥台_04_密碼.Texts = value[(int)enum_人員資料.密碼].ObjectToString();
                            this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(null);
                        }));
                        Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.指紋登入, 領藥台_04_登入者姓名, "01.號使用者");
                    }
                }
            }
            cnt = 65500;
            return;


        }

        #endregion
        #region PLC_領藥台_04_檢查輸入資料

        PLC_Device PLC_Device_領藥台_04_檢查輸入資料 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_04_檢查輸入資料_OK = new PLC_Device("");

        MyTimer MyTimer_領藥台_04_檢查輸入資料_NG訊息延遲 = new MyTimer();
        int cnt_Program_領藥台_04_檢查輸入資料 = 65534;
        void sub_Program_領藥台_04_檢查輸入資料()
        {
            if ((this.plC_ScreenPage_Main.PageText == "調劑作業" || true) && PLC_Device_領藥台_04_已登入.Bool)
            {
                PLC_Device_領藥台_04_檢查輸入資料.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_04_檢查輸入資料.Bool = false;
            }
            if (Dialog_使用者登入.IsShown)
            {
                PLC_Device_領藥台_04_檢查輸入資料.Bool = false;
            }
            if (cnt_Program_領藥台_04_檢查輸入資料 == 65534)
            {
                PLC_Device_領藥台_04_檢查輸入資料.SetComment("PLC_領藥台_04_檢查輸入資料");
                PLC_Device_領藥台_04_檢查輸入資料_OK.SetComment("PLC_Device_領藥台_04_檢查輸入資料_OK");
                PLC_Device_領藥台_04_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_04_檢查輸入資料 = 65535;
            }
            if (cnt_Program_領藥台_04_檢查輸入資料 == 65535) cnt_Program_領藥台_04_檢查輸入資料 = 1;
            if (cnt_Program_領藥台_04_檢查輸入資料 == 1) cnt_Program_領藥台_04_檢查輸入資料_檢查按下(ref cnt_Program_領藥台_04_檢查輸入資料);
            if (cnt_Program_領藥台_04_檢查輸入資料 == 2) cnt_Program_領藥台_04_檢查輸入資料_初始化(ref cnt_Program_領藥台_04_檢查輸入資料);
            if (cnt_Program_領藥台_04_檢查輸入資料 == 3) cnt_Program_領藥台_04_檢查輸入資料_設定開始掃描(ref cnt_Program_領藥台_04_檢查輸入資料);
            if (cnt_Program_領藥台_04_檢查輸入資料 == 4) cnt_Program_領藥台_04_檢查輸入資料_等待掃描結束(ref cnt_Program_領藥台_04_檢查輸入資料);
            if (cnt_Program_領藥台_04_檢查輸入資料 == 5) cnt_Program_領藥台_04_檢查輸入資料_檢查醫令資料及寫入(ref cnt_Program_領藥台_04_檢查輸入資料);
            if (cnt_Program_領藥台_04_檢查輸入資料 == 6) cnt_Program_領藥台_04_檢查輸入資料 = 65500;


            if (cnt_Program_領藥台_04_檢查輸入資料 > 1) cnt_Program_領藥台_04_檢查輸入資料_檢查放開(ref cnt_Program_領藥台_04_檢查輸入資料);

            if (cnt_Program_領藥台_04_檢查輸入資料 == 65500)
            {
                PLC_Device_領藥台_04_檢查輸入資料.Bool = false;
                cnt_Program_領藥台_04_檢查輸入資料 = 65535;
            }
        }
        void cnt_Program_領藥台_04_檢查輸入資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_04_檢查輸入資料.Bool) cnt++;
        }
        void cnt_Program_領藥台_04_檢查輸入資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_04_檢查輸入資料.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_04_檢查輸入資料_初始化(ref int cnt)
        {
            //PLC_Device_Scanner04_讀取藥單資料.Bool = false;
            cnt++;
        }
        void cnt_Program_領藥台_04_檢查輸入資料_設定開始掃描(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner04_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner04_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner04_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
            else
            {
                if (!PLC_Device_Scanner04_讀取藥單資料.Bool)
                {
                    PLC_Device_Scanner04_讀取藥單資料_OK.Bool = false;
                    PLC_Device_Scanner04_讀取藥單資料.Bool = true;
                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_04_檢查輸入資料_等待掃描結束(ref int cnt)
        {
            if (Dialog_手輸醫令.IsShown)
            {
                cnt = 65500;
                return;
            }
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (!PLC_Device_Scanner04_讀取藥單資料.Bool || PLC_Device_Scanner04_讀取藥單資料_OK.Bool)
                {
                    if (PLC_Device_Scanner04_讀取藥單資料_OK.Bool)
                    {
                        cnt++;
                        return;
                    }
                    else
                    {
                        this.voice.SpeakOnTask("掃碼失敗");
                        cnt = 65500;
                        return;
                    }
                }
            }
            else
            {
                if (!PLC_Device_Scanner04_讀取藥單資料.Bool || PLC_Device_Scanner04_讀取藥單資料_OK.Bool)
                {
                    if (領藥台_04_醫令條碼.Length < 15)
                    {
                        string text = 領藥台_04_醫令條碼.Replace("\n", "");
                        text = text.Replace("\r", "");
                        if (text.StringIsEmpty())
                        {
                            cnt = 65500;
                            return;
                        }
                        Console.WriteLine($"{text}");

                        if (text == QR_Code_醫令模式切換)
                        {
                            PLC_Device_領藥台_04_單醫令模式.Bool = !PLC_Device_領藥台_04_單醫令模式.Bool;

                            string text_temp = PLC_Device_領藥台_04_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                            Console.WriteLine($"切換模式至{text_temp}");
                            Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"切換模式至{text_temp}", 1000, 0, 0);
                            this.Invoke(new Action(delegate
                            {

                                this.rJ_Lable_領藥台_04_Title.Text = $"    04. [{領藥台_04_登入者姓名}] {text_temp}";
                                this.rJ_Lable_領藥台_04_Title.BorderColor = this.panel_工程模式_領藥台_04_顏色.BackColor;
                                this.rJ_Lable_領藥台_04_Title.BackgroundColor = Color.GreenYellow;
                                this.rJ_Lable_領藥台_04_Title.TextColor = Color.Black;
                            }));
                            dialog_AlarmForm.ShowDialog();
                            cnt = 65500;
                            return;
                        }
                        List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), text, false);
                        if (list_人員資料.Count > 0)
                        {
                            if (領藥台_04_ID != list_人員資料[0][(int)enum_人員資料.ID].ObjectToString())
                            {
                                this.PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(null);

                                this.Invoke(new Action(delegate
                                {
                                    textBox_領藥台_04_帳號.Texts = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                                    textBox_領藥台_04_密碼.Texts = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                                    this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(null);
                                }));
                                Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, 領藥台_04_登入者姓名, "04.號使用者");

                            }
                            cnt = 65500;
                            return;
                        }
                    }
                    cnt++;
                    return;
                }
            }
        }
        void cnt_Program_領藥台_04_檢查輸入資料_檢查醫令資料及寫入(ref int cnt)
        {
            if (plC_CheckBox_QRcode_Mode.Bool)
            {
                if (plC_Button_領藥台_04_領.Bool)
                {
                    this.Function_領藥台_04_QRCode領藥(this.Scanner04_讀取藥單資料_Array);
                }
                else if (plC_Button_領藥台_04_退.Bool)
                {
                    this.Function_領藥台_04_QRCode退藥(this.Scanner04_讀取藥單資料_Array);
                }
                cnt++;
            }
            else
            {
                if (plC_Button_領藥台_04_領.Bool)
                {
                    this.Function_領藥台_04_醫令領藥(領藥台_04_醫令條碼);
                }
                else if (plC_Button_領藥台_04_退.Bool)
                {
                    this.Function_領藥台_04_醫令退藥(領藥台_04_醫令條碼);
                }
                cnt++;
            }

        }



        #endregion
        #region PLC_領藥台_04_刷新領藥內容
        PLC_Device PLC_Device_領藥台_04_刷新領藥內容 = new PLC_Device("");
        PLC_Device PLC_Device_領藥台_04_刷新領藥內容_OK = new PLC_Device("");
        MyTimer MyTimer__領藥台_04_刷新領藥內容_刷新間隔 = new MyTimer();
        int cnt_Program_領藥台_04_刷新領藥內容 = 65534;
        void sub_Program_領藥台_04_刷新領藥內容()
        {
            if ((this.plC_ScreenPage_Main.PageText == "調劑作業" || true))
            {
                PLC_Device_領藥台_04_刷新領藥內容.Bool = true;
            }
            else
            {
                PLC_Device_領藥台_04_刷新領藥內容.Bool = false;
            }
            PLC_Device_領藥台_04_刷新領藥內容.Bool = (this.plC_ScreenPage_Main.PageText == "調劑作業");
            if (cnt_Program_領藥台_04_刷新領藥內容 == 65534)
            {
                PLC_Device_領藥台_04_刷新領藥內容.SetComment("PLC_領藥台_04_刷新領藥內容");
                PLC_Device_領藥台_04_刷新領藥內容_OK.SetComment("PLC_領藥台_04_刷新領藥內容_OK");
                PLC_Device_領藥台_04_刷新領藥內容.Bool = false;
                cnt_Program_領藥台_04_刷新領藥內容 = 65535;
            }
            if (cnt_Program_領藥台_04_刷新領藥內容 == 65535) cnt_Program_領藥台_04_刷新領藥內容 = 1;
            if (cnt_Program_領藥台_04_刷新領藥內容 == 1) cnt_Program_領藥台_04_刷新領藥內容_檢查按下(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 2) cnt_Program_領藥台_04_刷新領藥內容_初始化(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 3) cnt_Program_領藥台_04_刷新領藥內容_取得資料(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 4) cnt_Program_領藥台_04_刷新領藥內容_檢查雙人覆核(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 5) cnt_Program_領藥台_04_刷新領藥內容_檢查盲盤作業(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 6) cnt_Program_領藥台_04_刷新領藥內容_檢查複盤作業(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 7) cnt_Program_領藥台_04_刷新領藥內容_檢查作業完成(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 8) cnt_Program_領藥台_04_刷新領藥內容_檢查是否需輸入效期(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 9) cnt_Program_領藥台_04_刷新領藥內容_檢查是否需選擇效期(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 10) cnt_Program_領藥台_04_刷新領藥內容_檢查自動登出(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 11) cnt_Program_領藥台_04_刷新領藥內容_等待刷新間隔(ref cnt_Program_領藥台_04_刷新領藥內容);
            if (cnt_Program_領藥台_04_刷新領藥內容 == 12) cnt_Program_領藥台_04_刷新領藥內容 = 65500;
            if (cnt_Program_領藥台_04_刷新領藥內容 > 1) cnt_Program_領藥台_04_刷新領藥內容_檢查放開(ref cnt_Program_領藥台_04_刷新領藥內容);

            if (cnt_Program_領藥台_04_刷新領藥內容 == 65500)
            {
                PLC_Device_領藥台_04_刷新領藥內容.Bool = false;
                PLC_Device_領藥台_04_刷新領藥內容_OK.Bool = false;
                cnt_Program_領藥台_04_刷新領藥內容 = 65535;
            }
        }
        void cnt_Program_領藥台_04_刷新領藥內容_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥台_04_刷新領藥內容.Bool) cnt++;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥台_04_刷新領藥內容.Bool) cnt = 65500;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_取得資料(ref int cnt)
        {
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            List<object[]> list_取藥堆疊資料_replace = new List<object[]>();
            string GUID = "";
            string 序號 = "";
            string 動作 = "";
            string 藥袋序號 = "";
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 病歷號 = "";
            string 操作時間 = "";
            string 開方時間 = "";
            string 庫存量 = "";
            string 總異動量 = "";
            string 結存量 = "";
            string 單位 = "";
            string 狀態 = "";
            string 床號 = "";
            list_取藥堆疊資料.Sort(new Icp_取藥堆疊母資料_index排序());

            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示))
                {
                    this.voice.SpeakOnTask("庫存不足");
                    this.Function_取藥堆疊資料_設定作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示, false);
                    list_取藥堆疊資料_replace.Add(list_取藥堆疊資料[i]);
                }
                GUID = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                序號 = (i + 1).ToString();
                動作 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.動作].ObjectToString();
                藥袋序號 = $"{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥袋序號].ObjectToString()}:{list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.頻次].ObjectToString()}";
                藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                病歷號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
                操作時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString();
                開方時間 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
                庫存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.庫存量].ObjectToString();
                總異動量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString();
                結存量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                單位 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.單位].ObjectToString();
                狀態 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString();
                床號 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.床號].ObjectToString();
                if (this.Function_取藥堆疊資料_取得作業模式(list_取藥堆疊資料[i], enum_取藥堆疊母資料_作業模式.盲盤))
                {
                    庫存量 = "無";
                    結存量 = "無";
                }
                object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                value[(int)enum_取藥堆疊母資料.GUID] = GUID;
                value[(int)enum_取藥堆疊母資料.序號] = 序號;
                value[(int)enum_取藥堆疊母資料.動作] = 動作;
                value[(int)enum_取藥堆疊母資料.藥袋序號] = 藥袋序號;
                value[(int)enum_取藥堆疊母資料.藥品碼] = 藥品碼;
                value[(int)enum_取藥堆疊母資料.藥品名稱] = 藥品名稱;
                value[(int)enum_取藥堆疊母資料.病歷號] = 病歷號;
                value[(int)enum_取藥堆疊母資料.操作時間] = 操作時間;
                value[(int)enum_取藥堆疊母資料.開方時間] = 開方時間;
                value[(int)enum_取藥堆疊母資料.庫存量] = 庫存量;
                value[(int)enum_取藥堆疊母資料.總異動量] = 總異動量;
                value[(int)enum_取藥堆疊母資料.結存量] = 結存量;
                value[(int)enum_取藥堆疊母資料.單位] = 單位;
                value[(int)enum_取藥堆疊母資料.狀態] = 狀態;
                value[(int)enum_取藥堆疊母資料.床號] = 床號;

                list_value.Add(value);


            }

            if (plC_CheckBox_調劑畫面合併相同藥品.Bool)
            {
                List<object[]> list_value_new = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();
                string[] 藥品碼_array = (from value in list_value
                                      select value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString()).Distinct().ToList().ToArray();
                for (int i = 0; i < 藥品碼_array.Length; i++)
                {
                    list_value_buf = (from value in list_value
                                      where value[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼_array[i]
                                      select value).ToList();
                    if (list_value_buf.Count == 0) continue;
                    object[] value_領藥內容 = new object[new enum_取藥堆疊母資料().GetLength()];
                    value_領藥內容[(int)enum_取藥堆疊母資料.GUID] = list_value_buf[0][(int)enum_取藥堆疊母資料.GUID];
                    value_領藥內容[(int)enum_取藥堆疊母資料.序號] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.動作] = 動作;
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥袋序號] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥袋序號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品碼] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品碼];
                    value_領藥內容[(int)enum_取藥堆疊母資料.藥品名稱] = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品名稱];
                    value_領藥內容[(int)enum_取藥堆疊母資料.病歷號] = list_value_buf[0][(int)enum_取藥堆疊母資料.病歷號];
                    value_領藥內容[(int)enum_取藥堆疊母資料.操作時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.操作時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.開方時間] = list_value_buf[0][(int)enum_取藥堆疊母資料.開方時間];
                    value_領藥內容[(int)enum_取藥堆疊母資料.庫存量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.結存量] = "";
                    value_領藥內容[(int)enum_取藥堆疊母資料.單位] = list_value_buf[0][(int)enum_取藥堆疊母資料.單位];
                    value_領藥內容[(int)enum_取藥堆疊母資料.床號] = list_value_buf[0][(int)enum_取藥堆疊母資料.床號];
                    double 總異動量_temp = 0;
                    bool flag_入賬完成 = true;
                    bool flag_無儲位 = false;
                    bool flag_庫存不足 = false;
                    for (int k = 0; k < list_value_buf.Count; k++)
                    {
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                        {
                            flag_入賬完成 = false;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                        {
                            flag_無儲位 = true;
                        }
                        if (list_value_buf[k][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                        {
                            flag_庫存不足 = true;
                        }
                        總異動量_temp += list_value_buf[k][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();
                    }
                    value_領藥內容[(int)enum_取藥堆疊母資料.總異動量] = 總異動量_temp;
                    if (flag_入賬完成)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName();
                    }
                    else if (flag_無儲位)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    }
                    else if (flag_庫存不足)
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName();
                    }
                    else
                    {
                        value_領藥內容[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                    }
                    list_value_new.Add(value_領藥內容);

                }
                for (int i = 0; i < list_value_new.Count; i++)
                {
                    list_value_new[i][(int)enum_取藥堆疊母資料.序號] = (i + 1).ToString();
                    藥品碼 = list_value_new[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    double 庫存量_temp = Function_從SQL取得庫存(藥品碼);
                    double 結存量_temp = 庫存量_temp + list_value_new[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToDouble();

                    list_value_new[i][(int)enum_取藥堆疊母資料.庫存量] = 庫存量_temp.ToString();
                    list_value_new[i][(int)enum_取藥堆疊母資料.結存量] = 結存量_temp.ToString();

                }
                list_value = list_value_new;
            }
            this.sqL_DataGridView_領藥台_04_領藥內容.RefreshGrid(list_value);
            Application.DoEvents();
            if (list_取藥堆疊資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_檢查雙人覆核(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                Application.DoEvents();
                Dialog_使用者登入 dialog_使用者登入 = new Dialog_使用者登入(領藥台_04_ID, 藥名);
                this.Invoke(new Action(delegate
                {
                    dialog_使用者登入.Location = new Point(this.rJ_Pannel_領藥台_04.PointToScreen(Point.Empty).X + this.rJ_Pannel_領藥台_04.Width + 20, 1);
                }));

                if (dialog_使用者登入.ShowDialog() != DialogResult.Yes)
                {
                    list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料[i]);
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                    Fuction_領藥台_04_時間重置();
                    continue;
                }
                Fuction_領藥台_04_時間重置();
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師姓名] = dialog_使用者登入.UserName;
                list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.覆核藥師ID] = dialog_使用者登入.UserID;
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
        void cnt_Program_領藥台_04_刷新領藥內容_檢查盲盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                int retry = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 總異動量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                double 結存量 = 0;
                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入盲盤數量.wav"); ;
                while (true)
                {
                    //if (try_error == 1)
                    //{
                    //    Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                    //    if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                    //    {
                    //        Fuction_領藥台_04_時間重置();
                    //        try_error = 0;
                    //    }
                    //    else
                    //    {
                    //        Fuction_領藥台_04_時間重置();
                    //        try_error++;
                    //    }
                    //    continue;
                    //}
                    //if (try_error == 2)
                    //{
                    //    Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                    //    dialog_收支原因選擇.Title = $"盲盤數量錯誤({結存量}) 選擇原因";
                    //    dialog_收支原因選擇.ShowDialog();
                    //    Fuction_領藥台_04_時間重置();
                    //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n盲盤錯誤原因:{dialog_收支原因選擇.Value}";
                    //    Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                    //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                    //    list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                    //    break;
                    //}

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(盲盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;
                    this.Invoke(new Action(delegate
                    {
                        dialog_NumPannel.Location = new Point(this.rJ_Pannel_領藥台_04.PointToScreen(Point.Empty).X + this.rJ_Pannel_領藥台_04.Width + 20, 1);
                    }));
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_04_時間重置();
                        break;
                    }
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    double 庫存量 = Function_從SQL取得庫存(藥碼);
                    double 差異值 = medRecheckLogClass.get_unresolved_qty_by_code(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, 藥碼);
                    結存量 = 庫存量 + 總異動量 + 差異值;
                    if (結存量 == dialog_NumPannel.Value)
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\盲盤數量錯誤.wav");
                    if (retry == 0)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("請再次覆盤", 2000);
                        dialog_錯誤提示.ShowDialog();
                    }
                    if (retry == 1)
                    {
                        Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"異常紀錄,盤點值 : {dialog_NumPannel.Value}", 2000);
                        dialog_錯誤提示.ShowDialog();
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = "盤點異常";
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    try_error++;
                    retry++;
                }
            }
            if (list_取藥堆疊母資料_replace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
                cnt = 1;
                return;
            }
            cnt++;

        }
        void cnt_Program_領藥台_04_刷新領藥內容_檢查複盤作業(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();

            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                int try_error = 0;
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                string 結存量 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
                Voice.MediaPlayAsync($@"{currentDirectory}\請輸入複盤數量.wav");
                while (true)
                {
                    if (try_error == 1)
                    {
                        Dialog_盤點數量錯誤 dialog_盤點數量錯誤 = new Dialog_盤點數量錯誤();
                        if (dialog_盤點數量錯誤.ShowDialog() == DialogResult.Yes)
                        {
                            Fuction_領藥台_04_時間重置();
                            try_error = 0;
                        }
                        else
                        {
                            Fuction_領藥台_04_時間重置();
                            try_error++;
                        }
                        continue;
                    }
                    if (try_error == 2)
                    {
                        Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                        dialog_收支原因選擇.Title = $"複盤數量錯誤({結存量}) 選擇原因";
                        dialog_收支原因選擇.ShowDialog();
                        Fuction_領藥台_04_時間重置();
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因] = $"{list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString()} \n複盤錯誤原因:{dialog_收支原因選擇.Value}";
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }

                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"(明盤)請輸入取藥後盤點數量", $"藥碼:{藥碼} \n藥名:{藥名}");
                    dialog_NumPannel.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                    dialog_NumPannel.X_Visible = true;
                    this.Invoke(new Action(delegate
                    {
                        dialog_NumPannel.Location = new Point(this.rJ_Pannel_領藥台_04.PointToScreen(Point.Empty).X + this.rJ_Pannel_領藥台_04.Width + 20, 1);
                    }));
                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                    {
                        //list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                        this.Function_取藥堆疊資料_刪除母資料(list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString());
                        Fuction_領藥台_04_時間重置();
                        break;
                    }
                    Fuction_領藥台_04_時間重置();
                    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                    if (結存量 == dialog_NumPannel.Value.ToString())
                    {
                        Function_取藥堆疊資料_設定作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤, false);
                        list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                        list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                        break;
                    }
                    Voice.MediaPlayAsync($@"{currentDirectory}\複盤數量錯誤.wav");
                    try_error++;

                }


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
        void cnt_Program_領藥台_04_刷新領藥內容_檢查作業完成(ref int cnt)
        {
            List<object[]> list_取藥堆疊母資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            List<object[]> list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(this.領藥台_04名稱);
            List<object[]> list_取藥堆疊子資料_buf = new List<object[]>();
            List<object[]> list_取藥堆疊子資料_replace = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_replace = new List<object[]>();
            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.作業完成.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
            {
                string Master_GUID = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                string 藥碼 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥名 = list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                //if (Function_取藥堆疊資料_取得作業模式(list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤))
                //{
                //    voice.SpeakOnTask("請輸入盤點數量");
                //    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"藥碼:{藥碼} 藥名:{藥名}  請輸入盤點數量");
                //    dialog_NumPannel.ShowDialog();
                //    list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.盤點量] = dialog_NumPannel.Value.ToString();
                //    list_取藥堆疊母資料_replace.Add(list_取藥堆疊母資料[i]);
                //}

                list_取藥堆疊子資料_buf = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);
                for (int k = 0; k < list_取藥堆疊子資料_buf.Count; k++)
                {
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                    list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.調劑結束] = true.ToString();
                    list_取藥堆疊子資料_replace.Add(list_取藥堆疊子資料_buf[k]);
                }
            }
            if (list_取藥堆疊母資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_replace, false);
            if (list_取藥堆疊子資料_replace.Count > 0) this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊子資料_replace, false);
            cnt++;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_檢查是否需輸入效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.輸入新效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                Dialog_輸入效期 dialog = new Dialog_輸入效期();
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    cnt = 65500;
                    return;
                }
                string 效期 = dialog.效期.StringToDateTime().ToDateString(TypeConvert.Enum_Year_Type.Anno_Domini, "/");
                dialog.Dispose();
                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = 效期;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.新增效期.GetEnumName();

                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(enum_取藥堆疊母資料.GUID.GetEnumName(), GIUD, list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_檢查是否需選擇效期(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            list_取藥堆疊資料 = list_取藥堆疊資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName());

            string GIUD = "";
            for (int i = 0; i < list_取藥堆疊資料.Count; i++)
            {
                string 藥品碼 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 藥品名稱 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                double 交易量 = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.總異動量].StringToDouble();
                List<string> list_效期 = new List<string>();
                List<string> list_效期_buf = new List<string>();
                List<string> list_批號 = new List<string>();
                List<string> list_數量 = new List<string>();

                List<object> list_device = Function_從SQL取得儲位到本地資料(藥品碼);
                if (list_device.Count == 0)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    continue;
                }
                for (int k = 0; k < list_device.Count; k++)
                {
                    Device device = list_device[k] as Device;
                    if (device != null)
                    {
                        for (int m = 0; m < device.List_Validity_period.Count; m++)
                        {
                            list_效期_buf = (from value in list_效期
                                           where value == device.List_Validity_period[m]
                                           select value).ToList();
                            if (list_效期_buf.Count == 0)
                            {
                                list_效期.Add(device.List_Validity_period[m]);
                                list_批號.Add(device.List_Lot_number[m]);
                                list_數量.Add(device.List_Inventory[m]);
                            }
                        }
                    }
                }
                Dialog_選擇效期 dialog = new Dialog_選擇效期(藥品碼, 藥品名稱, 交易量, list_效期, list_批號, list_數量);
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    voice.SpeakOnTask("請選擇效期");
                    dialogResult = dialog.ShowDialog();

                }));
                if (dialogResult != DialogResult.Yes)
                {
                    list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.取消作業.GetEnumName();
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
                    cnt = 65500;
                    return;
                }

                GIUD = list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.效期] = dialog.Value;
                list_取藥堆疊資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();
                dialog.Dispose();
                this.sqL_DataGridView_取藥堆疊母資料.SQL_Replace(list_取藥堆疊資料[i], false);
            }
            cnt++;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_檢查自動登出(ref int cnt)
        {
            List<object[]> list_取藥堆疊資料 = Function_取藥堆疊資料_取得指定調劑台名稱母資料(this.領藥台_04名稱);
            List<object[]> list_取藥堆疊資料_buf = new List<object[]>();
            list_取藥堆疊資料_buf = (from value in list_取藥堆疊資料
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()
                               where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.無儲位.GetEnumName()
                               select value
                                ).ToList();

            if (list_取藥堆疊資料.Count == 0 && plC_CheckBox_多醫令模式.Bool == false)
            {
                MyTimer_領藥台_04_閒置登出時間.TickStop();
                MyTimer_領藥台_04_閒置登出時間.StartTickTime();

                MyTimer_領藥台_04_入賬完成時間.TickStop();
                MyTimer_領藥台_04_入賬完成時間.StartTickTime();
            }
            else
            {
                if (list_取藥堆疊資料_buf.Count > 0)
                {
                    MyTimer_領藥台_04_閒置登出時間.TickStop();
                    MyTimer_領藥台_04_閒置登出時間.StartTickTime();

                    MyTimer_領藥台_04_入賬完成時間.TickStop();
                    MyTimer_領藥台_04_入賬完成時間.StartTickTime();
                }
                else
                {
                    MyTimer_領藥台_04_閒置登出時間.StartTickTime();
                    MyTimer_領藥台_04_入賬完成時間.StartTickTime();
                    if (PLC_Device_領藥台_04_閒置登出時間.Value != 0)
                    {
                        if ((PLC_Device_領藥台_04_閒置登出時間.Value - (int)MyTimer_領藥台_04_閒置登出時間.GetTickTime()) <= 20000)
                        {
                            myTimer_領藥台_04_Logout.StartTickTime(5000);
                            if (myTimer_領藥台_04_Logout.IsTimeOut())
                            {
                                myTimer_領藥台_04_Logout.TickStop();
                                Task.Run(new Action(delegate
                                {
                                    using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\logout.wav"))
                                    {
                                        sp.Stop();
                                        sp.Play();
                                        sp.PlaySync();
                                    }

                                }));
                            }
                        }
                    }

                }
            }
            this.MyTimer__領藥台_04_刷新領藥內容_刷新間隔.TickStop();
            this.MyTimer__領藥台_04_刷新領藥內容_刷新間隔.StartTickTime(100);
            cnt++;
        }
        void cnt_Program_領藥台_04_刷新領藥內容_等待刷新間隔(ref int cnt)
        {
            if (this.MyTimer__領藥台_04_刷新領藥內容_刷新間隔.IsTimeOut())
            {
                cnt++;
            }
        }
        #endregion

        #region Function
        private void Function_領藥台_04_醫令領藥(string BarCode)
        {
            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            bool flag_OK = true;

            MyTimer myTimer_total = new MyTimer();
            myTimer_total.StartTickTime(50000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;

            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(14).Year, DateTime.Now.AddDays(14).Month, DateTime.Now.AddDays(14).Day, 23, 59, 59);
            List<object[]> list_堆疊資料 = Function_取藥堆疊資料_取得母資料();
            List<object[]> list_堆疊資料_buf = new List<object[]>();
            Task Task_刪除資料 = Task.Run(() =>
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                if (!plC_CheckBox_多醫令模式.Bool)
                {
                    this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_04名稱);
                }
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            });

            Task Task_取得醫令 = Task.Run(() =>
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                List<object[]> list_醫令資料 = new List<object[]>();
                PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);
                if (plC_Button_手輸數量.Bool)
                {
                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入領藥數量");
                    DialogResult dialogResult = dialog_NumPannel.ShowDialog();
                    if (dialogResult != DialogResult.Yes) return;
                    list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, dialog_NumPannel.Value * -1);
                }
                else
                {
                    list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, PLC_Device_領藥台_04_單醫令模式.Bool);
                }
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                list_醫令資料 = (from temp in list_醫令資料
                             where Basic.TypeConvert.IsInDate(temp[(int)enum_醫囑資料.開方日期].StringToDateTime(), dateTime_start, dateTime_end)
                             || Basic.TypeConvert.IsInDate(temp[(int)enum_醫囑資料.展藥時間].StringToDateTime(), dateTime_start, dateTime_end)
                             select temp).ToList();
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單已過期.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單已過期", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                List<object[]> list_醫令資料_remove = new List<object[]>();


                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }

                for (int i = 0; i < list_醫令資料_remove.Count; i++)
                {
                    list_醫令資料.RemoveByGUID(list_醫令資料_remove[i]);
                }
                Console.Write($"取得醫令資料 , 耗時{myTimer.ToString()}\n");
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\找不到儲位.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找不到儲位", 1500);
                    dialog_AlarmForm.ShowDialog();
                    flag_OK = false;
                    return;
                }
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                bool flag_雙人覆核 = false;

                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");

                List<string> Codes = (from temp in list_醫令資料
                                      select temp[(int)enum_醫囑資料.藥品碼].ObjectToString()).Distinct().ToList();
                List<medClass> medClasses = medClass.get_med_clouds_by_codes(API_Server, Codes);
                List<medClass> medClasses_buf = new List<medClass>();
                Dictionary<string, List<medClass>> keyValuePairs_medcloud = medClasses.CoverToDictionaryByCode();

                for (int i = 0; i < list_醫令資料.Count; i++)
                {
                    string 藥品碼 = list_醫令資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                    string 藥品名稱 = list_醫令資料[i][(int)enum_醫囑資料.藥品名稱].ObjectToString();
                    string 單位 = list_醫令資料[i][(int)enum_醫囑資料.劑量單位].ObjectToString();

                    list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                    if (list_藥品資料_buf.Count != 0)
                    {
                        藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                        單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                    }

                    string GUID = Guid.NewGuid().ToString();
                    string 調劑台名稱 = this.領藥台_04名稱;
                    enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
                    string Order_GUID = list_醫令資料[i][(int)enum_醫囑資料.GUID].ObjectToString();

                    string 診別 = list_醫令資料[i][(int)enum_醫囑資料.藥局代碼].ObjectToString();
                    string 領藥號 = list_醫令資料[i][(int)enum_醫囑資料.領藥號].ObjectToString();
                    string 病房號 = list_醫令資料[i][(int)enum_醫囑資料.病房].ObjectToString();
                    string 藥袋序號 = list_醫令資料[i][(int)enum_醫囑資料.PRI_KEY].ObjectToString();
                    string 病歷號 = list_醫令資料[i][(int)enum_醫囑資料.病歷號].ObjectToString();
                    string 病人姓名 = list_醫令資料[i][(int)enum_醫囑資料.病人姓名].ObjectToString();
                    string 床號 = "";
                    string 開方時間 = list_醫令資料[i][(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                    if (開方時間.StringIsEmpty()) 開方時間 = list_醫令資料[i][(int)enum_醫囑資料.開方日期].ObjectToString();
                    string ID = 領藥台_04_ID;
                    string 操作人 = 領藥台_04_登入者姓名;
                    string 藥師證字號 = 領藥台_04_藥師證字號;
                    string 顏色 = 領藥台_04_顏色;
                    double 總異動量 = list_醫令資料[i][(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                    string 效期 = "";
                    string 收支原因 = "";



                    list_堆疊資料_buf = (from temp in list_堆疊資料
                                     where temp[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼
                                     where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != "刷新面板"
                                     where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != 調劑台名稱
                                     where temp[(int)enum_取藥堆疊母資料.操作人].ObjectToString() != 操作人
                                     select temp).ToList();



                    takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                    takeMedicineStackClass.GUID = GUID;
                    takeMedicineStackClass.Order_GUID = Order_GUID;
                    takeMedicineStackClass.動作 = 動作.GetEnumName();
                    takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                    takeMedicineStackClass.藥品碼 = 藥品碼;
                    takeMedicineStackClass.領藥號 = 領藥號;
                    takeMedicineStackClass.病房號 = 病房號;
                    takeMedicineStackClass.診別 = 診別;
                    takeMedicineStackClass.顏色 = 顏色;
                    if (list_堆疊資料_buf.Count > 0 && plC_CheckBox_同藥碼同時取藥亮紫色.Checked)
                    {
                        takeMedicineStackClass.顏色 = Color.Purple.ToColorString();
                    }
                    takeMedicineStackClass.藥品名稱 = 藥品名稱;
                    takeMedicineStackClass.藥袋序號 = 藥袋序號;
                    takeMedicineStackClass.病歷號 = 病歷號;
                    takeMedicineStackClass.病人姓名 = 病人姓名;
                    takeMedicineStackClass.床號 = 床號;
                    takeMedicineStackClass.開方時間 = 開方時間;
                    takeMedicineStackClass.操作人 = 操作人;
                    takeMedicineStackClass.藥師證字號 = 藥師證字號;
                    takeMedicineStackClass.總異動量 = 總異動量.ToString();
                    takeMedicineStackClass.效期 = 效期;
                    takeMedicineStackClass.收支原因 = 收支原因;
                    takeMedicineStackClass.ID = ID;

                    bool flag_檢查過帳 = false;
                    medClasses_buf = keyValuePairs_medcloud.SortDictionaryByCode(藥品碼);
                    if (medClasses_buf.Count > 0)
                    {
                        if (medClasses_buf[0].高價藥品.ToUpper() == true.ToString().ToUpper())
                        {
                            flag_檢查過帳 = true;
                        }
                        if (medClasses_buf[0].管制級別.StringIsEmpty() == false && medClasses_buf[0].管制級別 != "N")
                        {
                            flag_檢查過帳 = true;
                        }
                    }

                    if (pLC_Device.Bool == false || flag_檢查過帳 == true)
                    {
                        if (list_醫令資料[i][(int)enum_醫囑資料.狀態].ObjectToString() == enum_醫囑資料_狀態.已過帳.GetEnumName())
                        {
                            takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.已領用過.GetEnumName();
                        }

                    }
                    if (flag_雙人覆核)
                    {
                        this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                        continue;
                    }

                    takeMedicineStackClasses.Add(takeMedicineStackClass);

                }
            });
            List<Task> taskList = new List<Task>();
            taskList.Add(Task_刪除資料);
            taskList.Add(Task_取得醫令);
            Task.WhenAll(taskList).Wait();

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);

            Console.Write($"掃碼完成 , 總耗時{myTimer_total.ToString()}\n");
            if (flag_OK) Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
        private void Function_領藥台_04_醫令退藥(string BarCode)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            string 藥品碼 = "";
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);
            List<object[]> list_醫令資料 = new List<object[]>();

            if (plC_Button_手輸數量.Bool)
            {
                double 手輸數量 = 0;
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入退藥數量");
                DialogResult dialogResult = dialog_NumPannel.ShowDialog();
                if (dialogResult != DialogResult.Yes) return;
                手輸數量 = dialog_NumPannel.Value * 1;
                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, 手輸數量);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單已過期.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單已過期", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                object[] value = list_醫令資料[0];
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_04名稱);
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");

                string GUID = value[(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_04名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;
                藥品碼 = value[(int)enum_醫囑資料.藥品碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) return;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = "";
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = value[(int)enum_醫囑資料.病歷號].ObjectToString();
                string 診別 = value[(int)enum_醫囑資料.藥局代碼].ObjectToString();
                string 領藥號 = value[(int)enum_醫囑資料.領藥號].ObjectToString();

                string 床號 = "";
                string 病人姓名 = value[(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 開方時間 = value[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_04_ID;
                string 操作人 = 領藥台_04_登入者姓名;
                string 藥師證字號 = 領藥台_04_藥師證字號;
                string 顏色 = 領藥台_04_顏色;
                double 總異動量 = value[(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.領藥號 = 領藥號;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }
            else
            {
                list_醫令資料 = this.Function_醫令資料_API呼叫(BarCode, PLC_Device_領藥台_04_單醫令模式.Bool);

                list_醫令資料 = list_醫令資料.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTime_start, dateTime_end);
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                List<object[]> list_醫令資料_remove = new List<object[]>();
                PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
                if (pLC_Device.Bool == false) list_醫令資料 = list_醫令資料.GetRows((int)enum_醫囑資料.狀態, enum_醫囑資料_狀態.已過帳.GetEnumName());
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\藥單無已過帳資料.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("藥單無已過帳資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                for (int i = 0; i < list_醫令資料.Count; i++)
                {
                    藥品碼 = list_醫令資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                    if (Function_從本地資料取得儲位(藥品碼).Count == 0)
                    {
                        list_醫令資料_remove.Add(list_醫令資料[i]);
                    }
                }
                for (int i = 0; i < list_醫令資料_remove.Count; i++)
                {
                    list_醫令資料.RemoveByGUID(list_醫令資料_remove[i]);
                }
                Console.Write($"取得醫令資料 , 耗時{myTimer.ToString()}\n");
                if (list_醫令資料.Count == 0)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\找不到儲位.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找不到儲位", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                Dialog_醫令退藥 dialog_醫令退藥 = new Dialog_醫令退藥(list_醫令資料);
                if (dialog_醫令退藥.ShowDialog() != DialogResult.Yes) return;
                object[] value = dialog_醫令退藥.Value;
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
                if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_04名稱);
                Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");

                string GUID = value[(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_04名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼退藥;
                藥品碼 = value[(int)enum_醫囑資料.藥品碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) return;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = "";
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = value[(int)enum_醫囑資料.病歷號].ObjectToString();
                string 診別 = value[(int)enum_醫囑資料.藥局代碼].ObjectToString();
                string 床號 = "";
                string 病人姓名 = value[(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 開方時間 = value[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_04_ID;
                string 操作人 = 領藥台_04_登入者姓名;
                string 藥師證字號 = 領藥台_04_藥師證字號;
                string 顏色 = 領藥台_04_顏色;
                double 總異動量 = value[(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;


                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
                Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
            }

        }

        private void Function_領藥台_04_QRCode領藥(string[] Scanner04_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_04名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥袋序號].ObjectToString();
            string 病人姓名 = "";
            string ID = 領藥台_04_ID;
            string 操作人 = 領藥台_04_登入者姓名;
            string 顏色 = 領藥台_04_顏色;
            string 床號 = "";
            double 總異動量 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";

            string 藥品碼 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();
            string 頻次 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.頻次].ObjectToString();


            string[] _serchnames = new string[] { enum_交易記錄查詢資料.藥品碼.GetEnumName(), enum_交易記錄查詢資料.病歷號.GetEnumName(), enum_交易記錄查詢資料.開方時間.GetEnumName(), enum_交易記錄查詢資料.頻次.GetEnumName(), enum_交易記錄查詢資料.藥袋序號.GetEnumName() };
            string[] _serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 頻次, 藥袋序號 };

            bool flag_重複領藥 = false;
            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(_serchnames, _serchvalues, false);
            list_交易記錄 = (from value in list_交易記錄
                         where value[(int)enum_交易記錄查詢資料.交易量].ObjectToString().StringToDouble() < 0
                         select value).ToList();

            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (list_交易記錄.Count > 0 && !pLC_Device.Bool)
            {
                this.voice.SpeakOnTask("此藥單已領取過");
                if (MyMessageBox.ShowDialog("此藥單已領取過,是否重複領藥?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    flag_重複領藥 = true;
                }
                else return;
            }
            string[] serchnames = new string[] { enum_取藥堆疊母資料.藥品碼.GetEnumName(), enum_取藥堆疊母資料.病歷號.GetEnumName(), enum_取藥堆疊母資料.開方時間.GetEnumName(), enum_取藥堆疊母資料.藥袋序號.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間, 藥袋序號 };
            if (sqL_DataGridView_領藥台_04_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }


            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_04名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            if (flag_重複領藥) 總異動量 = 0;
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.頻次 = 頻次;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            //takeMedicineStackClass.藥師證字號 = 藥師證字號;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.ID = ID;

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }
        private void Function_領藥台_04_QRCode退藥(string[] Scanner04_讀取藥單資料_Array)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            int daynum = plC_ComboBox_醫令檢查範圍.GetValue();
            if (daynum == 7) daynum = 7;
            if (daynum == 8) daynum = 14;
            if (daynum == 9) daynum = 21;
            if (daynum == 10) daynum = 28;
            daynum *= -1;
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_退藥不檢查是否掃碼領藥過.讀取元件位置);
            DateTime dateTime_start = new DateTime(DateTime.Now.AddDays(daynum).Year, DateTime.Now.AddDays(daynum).Month, DateTime.Now.AddDays(daynum).Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(DateTime.Now.AddDays(0).Year, DateTime.Now.AddDays(0).Month, DateTime.Now.AddDays(0).Day, 23, 59, 59);


            string GUID = Guid.NewGuid().ToString();
            string 調劑台名稱 = this.領藥台_04名稱;
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
            string 藥袋序號 = this.領藥台_04名稱;
            string 病人姓名 = "";
            string ID = 領藥台_04_ID;
            string 操作人 = 領藥台_04_登入者姓名;
            string 顏色 = 領藥台_04_顏色;
            double 總異動量 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.使用數量].ObjectToString().StringToDouble();
            string 效期 = "";
            string 床號 = "";
            string 藥品碼 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品碼].ObjectToString();
            string 藥品名稱 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.藥品名稱].ObjectToString();
            string 病歷號 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.病歷號].ObjectToString();
            string 開方時間 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.開方時間].ObjectToString();
            string 包裝單位 = Scanner04_讀取藥單資料_Array[(int)enum_Scanner_陣列內容.包裝單位].ObjectToString();


            string[] serchnames = new string[] { enum_交易記錄查詢資料.藥品碼.GetEnumName(), enum_交易記錄查詢資料.病歷號.GetEnumName(), enum_交易記錄查詢資料.開方時間.GetEnumName() };
            string[] serchvalues = new string[] { 藥品碼, 病歷號, 開方時間 };

            List<object[]> list_交易記錄 = this.sqL_DataGridView_交易記錄查詢.SQL_GetRows(serchnames, serchvalues, false);
            // list_交易記錄 = list_交易記錄.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTime_start, dateTime_end);
            Console.Write($"取得交易記錄資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_退藥不檢查是否掃碼領藥過.Checked)
            {
                if (list_交易記錄.Count == 0)
                {
                    this.voice.SpeakOnTask("查無領藥紀錄");
                    return;
                }
            }

            if (sqL_DataGridView_領藥台_04_領藥內容.GetRows(serchnames, serchvalues, false).Count > 0)
            {
                this.voice.SpeakOnTask("此藥單正在領取中");
                return;
            }
            if (Function_從本地資料取得儲位(藥品碼).Count == 0)
            {
                this.voice.SpeakOnTask("未搜尋到儲位");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            總異動量 = dialog_NumPannel.Value;
            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            if (!plC_CheckBox_多醫令模式.Bool) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_04名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
            takeMedicineStackClass.GUID = GUID;
            takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
            takeMedicineStackClass.動作 = 動作.GetEnumName();
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.藥品碼 = 藥品碼;
            takeMedicineStackClass.藥品名稱 = 藥品名稱;
            takeMedicineStackClass.藥袋序號 = 藥袋序號;
            takeMedicineStackClass.單位 = 包裝單位;
            takeMedicineStackClass.病歷號 = 病歷號;
            takeMedicineStackClass.床號 = 床號;
            takeMedicineStackClass.病人姓名 = 病人姓名;
            takeMedicineStackClass.開方時間 = 開方時間;
            takeMedicineStackClass.操作人 = 操作人;
            takeMedicineStackClass.顏色 = 顏色;
            takeMedicineStackClass.總異動量 = 總異動量.ToString();
            takeMedicineStackClass.效期 = 效期;
            takeMedicineStackClass.ID = ID;

            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
            Console.Write($"新增取藥資料 , 耗時{myTimer.ToString()}\n");
            Voice.MediaPlayAsync($@"{currentDirectory}\sucess_01.wav");
        }

        private void Fuction_領藥台_04_時間重置()
        {
            MyTimer_領藥台_04_閒置登出時間.TickStop();
            MyTimer_領藥台_04_閒置登出時間.StartTickTime();
            MyTimer_領藥台_04_入賬完成時間.TickStop();
            MyTimer_領藥台_04_入賬完成時間.StartTickTime();
        }
        #endregion

        #region Event
        private void SqL_DataGridView_領藥台_04_領藥內容_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList = Function_領藥內容_重新排序(RowsList);
        }
        private void PlC_RJ_Button_領藥台_04_取消作業_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_04_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_04_藥品圖片.Image = null;
            //}));
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_04名稱);
            this.sqL_DataGridView_領藥台_04_領藥內容.ClearGrid();
        }
        private void PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(MouseEventArgs mevent)
        {
            if (plC_RJ_Button_領藥台_04_登入.Texts == "登出")
            {
                PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(null);
                return;
            }
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
            if (this.PLC_Device_領藥台_04_已登入.Bool) return;
            if (textBox_領藥台_04_帳號.Texts.StringIsEmpty()) return;

            if (textBox_領藥台_04_帳號.Texts.ToUpper() == Admin_ID.ToUpper())
            {
                if (textBox_領藥台_04_密碼.Texts.ToUpper() == Admoin_Password.ToUpper())
                {
                    this.PLC_Device_領藥台_04_已登入.Bool = true;
                    領藥台_04_登入者姓名 = "最高管理權限";
                    領藥台_04_ID = "admin";
                    this.PLC_Device_最高權限.Bool = true;
                    return;
                }
            }

            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_人員資料.ID, textBox_領藥台_04_帳號.Texts);
            if (list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無此帳號", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("查無此帳號!");
                return;
            }
            string password = list_value[0][(int)enum_人員資料.密碼].ObjectToString();
            if (textBox_領藥台_04_密碼.Texts != password)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("密碼錯誤", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("密碼錯誤!");
                return;
            }
            領藥台_04_登入者姓名 = list_value[0][(int)enum_人員資料.姓名].ObjectToString();
            領藥台_04_ID = list_value[0][(int)enum_人員資料.ID].ObjectToString();
            領藥台_04_顏色 = list_value[0][(int)enum_人員資料.顏色].ObjectToString();
            領藥台_04_藥師證字號 = list_value[0][(int)enum_人員資料.藥師證字號].ObjectToString();
            this.PLC_Device_領藥台_04_已登入.Bool = true;
            if (mevent != null) Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.密碼登入, 領藥台_04_登入者姓名, "領藥台_04");
            string 狀態顯示 = "";
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_04_狀態顯示.GetAlignmentString(PLC_MultiStateDisplay.TextValue.Alignment.Left);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_04_狀態顯示.GetFontColorString(Color.Black, true);
            //狀態顯示 += this.plC_MultiStateDisplay_領藥台_04_狀態顯示.GetFontString(new Font("微軟正黑體", 24F, FontStyle.Bold), true);
            //狀態顯示 += string.Format($"登入者姓名 : {領藥台_04_登入者姓名}");
            //this.plC_MultiStateDisplay_領藥台_04_狀態顯示.SetTextValue(PLC_Device_領藥台_04_狀態顯示_登入者姓名.GetAdress(), 狀態顯示);
            this.plC_Button_領藥台_04_領.Bool = true;
            this.plC_Button_領藥台_04_退.Bool = false;
            Console.WriteLine($"登入成功! ID : {領藥台_04_ID}, 名稱 : {領藥台_04_登入者姓名}");
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_04_帳號.Texts = "";
                textBox_領藥台_04_密碼.Texts = "";
                plC_RJ_Button_領藥台_04_登入.Texts = "登出";
                string text_temp = PLC_Device_領藥台_04_單醫令模式.Bool ? "【單醫令】" : "【多醫令】";
                this.rJ_Lable_領藥台_04_Title.Text = $"    04. [{領藥台_04_登入者姓名}] {text_temp}";
                this.rJ_Lable_領藥台_04_Title.BorderColor = this.panel_工程模式_領藥台_04_顏色.BackColor;
                this.rJ_Lable_領藥台_04_Title.BackgroundColor = Color.GreenYellow;
                this.rJ_Lable_領藥台_04_Title.TextColor = Color.Black;
            }));
            commonSapceClasses = Function_取得共用區所有儲位();
            Voice.MediaPlayAsync($@"{currentDirectory}\登入成功.wav");
            PLC_Device_Scanner04_讀取藥單資料.Bool = false;
            PLC_Device_Scanner04_讀取藥單資料_OK.Bool = false;
            領藥台_04_醫令條碼 = "";
        }
        private void PlC_RJ_Button_領藥台_04_登出_MouseDownEvent(MouseEventArgs mevent)
        {
            if (!this.PLC_Device_領藥台_04_已登入.Bool) return;
            //this.Invoke(new Action(delegate
            //{
            //    this.pictureBox_領藥台_04_藥品圖片.Image = null;
            //}));
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_04名稱);
            this.sqL_DataGridView_領藥台_04_領藥內容.ClearGrid();

            Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.登出, 領藥台_04_登入者姓名, "04.號使用者");
            領藥台_04_登入者姓名 = "None";
            this.PLC_Device_領藥台_04_已登入.Bool = false;
            this.PLC_Device_最高權限.Bool = false;
            this.Invoke(new Action(delegate
            {
                textBox_領藥台_04_帳號.Texts = "";
                textBox_領藥台_04_密碼.Texts = "";
                plC_RJ_Button_領藥台_04_登入.Texts = "登入";
                this.rJ_Lable_領藥台_04_Title.Text = $"    04. [未登入]";
                this.rJ_Lable_領藥台_04_Title.BorderColor = Color.DimGray;
                this.rJ_Lable_領藥台_04_Title.BackgroundColor = Color.DimGray;
                this.rJ_Lable_領藥台_04_Title.TextColor = Color.White;

            }));
        }
        private void PlC_Button_領藥台_04_退_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_04_領.Bool = !Value;
            this.plC_Button_領藥台_04_退.Bool = Value;
        }
        private void PlC_Button_領藥台_04_領_ValueChangeEvent(bool Value)
        {
            this.plC_Button_領藥台_04_領.Bool = Value;
            this.plC_Button_領藥台_04_退.Bool = !Value;
        }
        private void SqL_DataGridView_領藥台_04_領藥內容_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].Cells[(int)enum_取藥堆疊母資料.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].Cells[enum_取藥堆疊母資料.結存量.GetEnumName()].Value = "無";
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.已領用過.GetEnumName())
                {
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.sqL_DataGridView_領藥台_04_領藥內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void TextBox_領藥台_04_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox_領藥台_04_密碼.Focus();
            }
        }
        private void TextBox_領藥台_04_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.PlC_RJ_Button_領藥台_04_登入_MouseDownEvent(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
            }
        }
        private void PLC_Device_領藥台_04_已登入_ValueChangeEvent(object Value)
        {
            if ((bool)Value == true)
            {
                this.Invoke(new Action(delegate
                {
                    this.panel_Main.Collapse();
                }));
            }
        }
        #endregion

        #endregion

        #region PLC_領藥_RFID_檢查刷卡
        PLC_Device PLC_Device_領藥_RFID_檢查刷卡 = new PLC_Device("");
        PLC_Device PLC_Device_領藥_RFID_檢查刷卡_OK = new PLC_Device("");
        PLC_Device PLC_Device_領藥_RFID_檢查刷卡_TEST = new PLC_Device("");
        string 領藥_RFID_檢查刷卡_登入者ID;
        string 領藥_RFID_檢查刷卡_登入者姓名;
        Class_領藥_RFID_檢查刷卡 class_領藥_RFID_檢查刷卡 = new Class_領藥_RFID_檢查刷卡();
        private class Class_領藥_RFID_檢查刷卡
        {
            public string IP;
            public int Num;
            public string Name;
            public string RFID;
            public List<Device> devices = new List<Device>();
        }
        int cnt_Program_領藥_RFID_檢查刷卡 = 65534;
        void sub_Program_領藥_RFID_檢查刷卡()
        {
            if (this.plC_ScreenPage_Main.PageText != "管制抽屜" && this.plC_ScreenPage_Main.PageText != "調劑作業") PLC_Device_領藥_RFID_檢查刷卡.Bool = false;
            else PLC_Device_領藥_RFID_檢查刷卡.Bool = true;
            if (cnt_Program_領藥_RFID_檢查刷卡 == 65534)
            {
                PLC_Device_領藥_RFID_檢查刷卡.SetComment("PLC_領藥_RFID_檢查刷卡");
                PLC_Device_領藥_RFID_檢查刷卡_OK.SetComment("PLC_領藥_RFID_檢查刷卡_OK");
                PLC_Device_領藥_RFID_檢查刷卡.Bool = false;
                cnt_Program_領藥_RFID_檢查刷卡 = 65535;
            }
            if (cnt_Program_領藥_RFID_檢查刷卡 == 65535) cnt_Program_領藥_RFID_檢查刷卡 = 1;
            if (cnt_Program_領藥_RFID_檢查刷卡 == 1) cnt_Program_領藥_RFID_檢查刷卡_檢查按下(ref cnt_Program_領藥_RFID_檢查刷卡);
            if (cnt_Program_領藥_RFID_檢查刷卡 == 2) cnt_Program_領藥_RFID_檢查刷卡_初始化(ref cnt_Program_領藥_RFID_檢查刷卡);
            if (cnt_Program_領藥_RFID_檢查刷卡 == 3) cnt_Program_領藥_RFID_檢查刷卡_取得刷卡ID(ref cnt_Program_領藥_RFID_檢查刷卡);
            if (cnt_Program_領藥_RFID_檢查刷卡 == 4) cnt_Program_領藥_RFID_檢查刷卡_取得登入者資訊(ref cnt_Program_領藥_RFID_檢查刷卡);
            if (cnt_Program_領藥_RFID_檢查刷卡 == 5) cnt_Program_領藥_RFID_檢查刷卡_顯示RFID領退藥頁面(ref cnt_Program_領藥_RFID_檢查刷卡);
            if (cnt_Program_領藥_RFID_檢查刷卡 == 6) cnt_Program_領藥_RFID_檢查刷卡 = 65500;
            if (cnt_Program_領藥_RFID_檢查刷卡 > 1) cnt_Program_領藥_RFID_檢查刷卡_檢查放開(ref cnt_Program_領藥_RFID_檢查刷卡);

            if (cnt_Program_領藥_RFID_檢查刷卡 == 65500)
            {
                PLC_Device_領藥_RFID_檢查刷卡.Bool = false;
                PLC_Device_領藥_RFID_檢查刷卡_OK.Bool = false;
                cnt_Program_領藥_RFID_檢查刷卡 = 65535;
            }
        }
        void cnt_Program_領藥_RFID_檢查刷卡_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥_RFID_檢查刷卡.Bool) cnt++;
        }
        void cnt_Program_領藥_RFID_檢查刷卡_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥_RFID_檢查刷卡.Bool) cnt = 65500;
        }
        void cnt_Program_領藥_RFID_檢查刷卡_初始化(ref int cnt)
        {

            cnt++;
        }
        void cnt_Program_領藥_RFID_檢查刷卡_取得刷卡ID(ref int cnt)
        {
            for (int i = 0; i < List_RFID_本地資料.Count; i++)
            {
                for (int k = 0; k < List_RFID_本地資料[i].DeviceClasses.Length; k++)
                {
                    if (List_RFID_本地資料[i].DeviceClasses[k].Enable && !List_RFID_本地資料[i].DeviceClasses[k].IsLocker)
                    {
                        string RFID = this.rfiD_UI.GetRFID(List_RFID_本地資料[i].IP, k);
                        if (RFID.StringToInt32() != 0 && !RFID.StringIsEmpty() || PLC_Device_領藥_RFID_檢查刷卡_TEST.Bool)
                        {
                            PLC_Device_領藥_RFID_檢查刷卡_TEST.Bool = false;
                            this.class_領藥_RFID_檢查刷卡.devices.Clear();
                            for (int d = 0; d < List_RFID_本地資料[i].DeviceClasses[k].RFIDDevices.Count; d++)
                            {
                                this.class_領藥_RFID_檢查刷卡.devices.Add(List_RFID_本地資料[i].DeviceClasses[k].RFIDDevices[d]);
                            }
                            this.class_領藥_RFID_檢查刷卡.IP = List_RFID_本地資料[i].IP;
                            this.class_領藥_RFID_檢查刷卡.Num = k;
                            this.class_領藥_RFID_檢查刷卡.RFID = RFID;
                            this.class_領藥_RFID_檢查刷卡.Name = List_RFID_本地資料[i].DeviceClasses[k].Name;


                            cnt++;
                            return;
                        }

                    }
                }
            }
            cnt = 65500;
        }
        void cnt_Program_領藥_RFID_檢查刷卡_取得登入者資訊(ref int cnt)
        {
            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_人員資料.卡號, class_領藥_RFID_檢查刷卡.RFID);
            if (list_value.Count == 0)
            {
                cnt = 65500;
                return;
            }
            領藥_RFID_檢查刷卡_登入者ID = list_value[0][(int)enum_人員資料.ID].ObjectToString();
            領藥_RFID_檢查刷卡_登入者姓名 = list_value[0][(int)enum_人員資料.姓名].ObjectToString();
            cnt++;
        }
        void cnt_Program_領藥_RFID_檢查刷卡_顯示RFID領退藥頁面(ref int cnt)
        {
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(領藥台_02名稱);
            for (int i = 0; i < this.class_領藥_RFID_檢查刷卡.devices.Count; i++)
            {
                this.class_領藥_RFID_檢查刷卡.devices[i] = this.rfiD_UI.SQL_GetDevice((RFIDDevice)this.class_領藥_RFID_檢查刷卡.devices[i]);
            }

            this.Invoke(new Action(delegate
            {
                Dialog_RFID領退藥頁面 dialog_RFID領退藥頁面 = new Dialog_RFID領退藥頁面(領藥_RFID_檢查刷卡_登入者ID, 領藥_RFID_檢查刷卡_登入者姓名, this.class_領藥_RFID_檢查刷卡.Name, this.class_領藥_RFID_檢查刷卡.devices, 領藥台_02名稱, List_領藥_入出庫資料檢查);
                if (dialog_RFID領退藥頁面.ShowDialog() == DialogResult.Yes)
                {
                    this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(領藥台_02名稱);
                }
            }));

            cnt++;
        }

        #endregion
        #region PLC_領藥_入出庫資料檢查
        PLC_Device PLC_Device_領藥_入出庫資料檢查 = new PLC_Device("");
        PLC_Device PLC_Device_領藥_入出庫資料檢查_OK = new PLC_Device("");
        private List<object[]> List_領藥_入出庫資料檢查 = new List<object[]>();

        int cnt_Program_領藥_入出庫資料檢查 = 65534;
        void sub_Program_領藥_入出庫資料檢查()
        {
            if (this.plC_ScreenPage_Main.PageText != "調劑作業") PLC_Device_領藥_入出庫資料檢查.Bool = false;
            else PLC_Device_領藥_入出庫資料檢查.Bool = true;

            if (cnt_Program_領藥_入出庫資料檢查 == 65534)
            {
                PLC_Device_領藥_入出庫資料檢查.SetComment("PLC_領藥_入出庫資料檢查");
                PLC_Device_領藥_入出庫資料檢查_OK.SetComment("PLC_領藥_入出庫資料檢查_OK");
                PLC_Device_領藥_入出庫資料檢查.Bool = false;
                cnt_Program_領藥_入出庫資料檢查 = 65535;
            }
            if (cnt_Program_領藥_入出庫資料檢查 == 65535) cnt_Program_領藥_入出庫資料檢查 = 1;
            if (cnt_Program_領藥_入出庫資料檢查 == 1) cnt_Program_領藥_入出庫資料檢查_檢查按下(ref cnt_Program_領藥_入出庫資料檢查);
            if (cnt_Program_領藥_入出庫資料檢查 == 2) cnt_Program_領藥_入出庫資料檢查_初始化(ref cnt_Program_領藥_入出庫資料檢查);
            if (cnt_Program_領藥_入出庫資料檢查 == 3) cnt_Program_領藥_入出庫資料檢查 = 65500;
            if (cnt_Program_領藥_入出庫資料檢查 > 1) cnt_Program_領藥_入出庫資料檢查_檢查放開(ref cnt_Program_領藥_入出庫資料檢查);

            if (cnt_Program_領藥_入出庫資料檢查 == 65500)
            {
                PLC_Device_領藥_入出庫資料檢查.Bool = false;
                PLC_Device_領藥_入出庫資料檢查_OK.Bool = false;
                cnt_Program_領藥_入出庫資料檢查 = 65535;
            }
        }
        void cnt_Program_領藥_入出庫資料檢查_檢查按下(ref int cnt)
        {
            if (PLC_Device_領藥_入出庫資料檢查.Bool) cnt++;
        }
        void cnt_Program_領藥_入出庫資料檢查_檢查放開(ref int cnt)
        {
            if (!PLC_Device_領藥_入出庫資料檢查.Bool) cnt = 65500;
        }
        void cnt_Program_領藥_入出庫資料檢查_初始化(ref int cnt)
        {
            string GUID = "";
            string 調劑台名稱 = "";
            enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.None;
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 藥袋序號 = "";
            string 單位 = "";
            string 病歷號 = "";
            string 病人姓名 = "";
            string 開方時間 = "";
            string ID = "";
            string 操作人 = "";
            string 顏色 = "";
            double 總異動量 = 0;
            string 效期 = "";
            string 床號 = "";
            List<object[]> list_藥品設定表 = this.sqL_DataGridView_藥品設定表.SQL_GetAllRows(false);
            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            for (int i = 0; i < List_領藥_入出庫資料檢查.Count; i++)
            {
                GUID = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.GUID].ObjectToString();
                調劑台名稱 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.調劑台名稱].ObjectToString();
                藥品碼 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.藥品碼].ObjectToString();
                藥品名稱 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.藥品名稱].ObjectToString();
                藥袋序號 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.藥袋序號].ObjectToString();
                單位 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.單位].ObjectToString();
                病歷號 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.病歷號].ObjectToString();
                病人姓名 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.病人姓名].ObjectToString();
                開方時間 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.開方時間].ObjectToString();
                ID = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.ID].ObjectToString();
                操作人 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.操作人].ObjectToString();
                顏色 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.顏色].ObjectToString();
                總異動量 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.總異動量].ObjectToString().StringToDouble();
                效期 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.效期].ObjectToString();
                string str_動作 = List_領藥_入出庫資料檢查[i][(int)Dialog_RFID領退藥頁面.enum_入出庫資料檢查.動作].ObjectToString();
                if (str_動作 == enum_交易記錄查詢動作.手輸領藥.GetEnumName())
                {
                    動作 = enum_交易記錄查詢動作.手輸領藥;
                }
                if (str_動作 == enum_交易記錄查詢動作.入庫作業.GetEnumName())
                {
                    動作 = enum_交易記錄查詢動作.入庫作業;
                }

                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.單位 = 單位;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.ID = ID;

                this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                List_領藥_入出庫資料檢查.RemoveAt(i);
                this.voice.SpeakOnTask("成功");
                break;
            }
            cnt++;
        }




        #endregion

        private void PlC_RJ_Button_指紋登入_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_AlarmForm dialog_AlarmForm;
            if (fpMatchSoket.IsOpen == false && flag_指紋辨識_Init == false)
            {
                dialog_AlarmForm = new Dialog_AlarmForm("指紋模組未初始化", 2000);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            Dialog_指紋登入 dialog_指紋登入 = new Dialog_指紋登入();
            if (dialog_指紋登入.ShowDialog() != DialogResult.Yes) return;
            if (dialog_指紋登入.台號 == 1) FpMatchClass_領藥台_01_指紋資訊 = dialog_指紋登入.Value;
            if (dialog_指紋登入.台號 == 2) FpMatchClass_領藥台_02_指紋資訊 = dialog_指紋登入.Value;
            if (dialog_指紋登入.台號 == 3) FpMatchClass_領藥台_03_指紋資訊 = dialog_指紋登入.Value;
            if (dialog_指紋登入.台號 == 4) FpMatchClass_領藥台_04_指紋資訊 = dialog_指紋登入.Value;

        }
        private void PlC_RJ_Button_醫令檢索_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            List<OrderClass> orderClasses = new List<OrderClass>();
            Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new enum_ContextMenuStrip_Main_醫令檢索());

            dialog_ContextMenuStrip.TitleFont = new Font("微軟正黑體", 24, FontStyle.Bold);
            dialog_ContextMenuStrip.ControlsFont = new Font("微軟正黑體", 22);
            dialog_ContextMenuStrip.ControlsWidth = 400;
            dialog_ContextMenuStrip.ControlsHeight = 80;

            dialog_ContextMenuStrip.SetEnable(enum_ContextMenuStrip_Main_醫令檢索.病歷號, !dBConfigClass.Order_mrn_ApiURL.StringIsEmpty());
            dialog_ContextMenuStrip.SetEnable(enum_ContextMenuStrip_Main_醫令檢索.領藥號, !dBConfigClass.Order_bag_num_ApiURL.StringIsEmpty());
            dialog_ContextMenuStrip.TitleText = "醫令檢索";
            if (dialog_ContextMenuStrip.ShowDialog() != DialogResult.Yes) return;
            if (dialog_ContextMenuStrip.Value == enum_ContextMenuStrip_Main_醫令檢索.病歷號.GetEnumName())
            {
                Dialog_病歷號輸入 dialog_病歷號輸入;

                this.Invoke(new Action(delegate
                {
                    dialog_病歷號輸入 = new Dialog_病歷號輸入();
                    if (dialog_病歷號輸入.ShowDialog() != DialogResult.Yes) return;
                    orderClasses = dialog_病歷號輸入.Value;
                }));

            }
            if (dialog_ContextMenuStrip.Value == enum_ContextMenuStrip_Main_醫令檢索.領藥號.GetEnumName())
            {
                Dialog_領藥號輸入 dialog_領藥號輸入;
                this.Invoke(new Action(delegate
                {
                    dialog_領藥號輸入 = new Dialog_領藥號輸入();
                    if (dialog_領藥號輸入.ShowDialog() != DialogResult.Yes) return;
                    orderClasses = dialog_領藥號輸入.Value;
                }));

            }



            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_醫令資料 = orderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
            bool flag_雙人覆核 = false;
            List<object[]> list_藥品設定表 = this.sqL_DataGridView_藥品設定表.SQL_GetAllRows(false);
            List<object[]> list_藥品管制方式設定 = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
            List<object[]> list_藥品資料_buf = new List<object[]>();
            Console.Write($"取得藥品資料 , 耗時{myTimer.ToString()}\n");
            this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.領藥台_01名稱);
            Console.Write($"刪除調劑台資料資料 , 耗時{myTimer.ToString()}\n");
            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            PLC_Device pLC_Device = new PLC_Device(plC_CheckBox_領藥不檢查是否掃碼領藥過.讀取元件位置);

            List<string> Codes = (from temp in list_醫令資料
                                  select temp[(int)enum_醫囑資料.藥品碼].ObjectToString()).Distinct().ToList();
            List<medClass> medClasses = medClass.get_med_clouds_by_codes(API_Server, Codes);
            List<medClass> medClasses_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_medcloud = medClasses.CoverToDictionaryByCode();

            for (int i = 0; i < list_醫令資料.Count; i++)
            {


                string GUID = list_醫令資料[i][(int)enum_醫囑資料.GUID].ObjectToString();
                string 調劑台名稱 = this.領藥台_01名稱;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
                string 藥品碼 = list_醫令資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                string 診別 = list_醫令資料[i][(int)enum_醫囑資料.藥局代碼].ObjectToString();

                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                if (list_藥品資料_buf.Count == 0) continue;
                string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                string 藥袋序號 = list_醫令資料[i][(int)enum_醫囑資料.PRI_KEY].ObjectToString();
                string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                string 病歷號 = list_醫令資料[i][(int)enum_醫囑資料.病歷號].ObjectToString();
                string 病人姓名 = list_醫令資料[i][(int)enum_醫囑資料.病人姓名].ObjectToString();
                string 床號 = "";
                string 開方時間 = list_醫令資料[i][(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string ID = 領藥台_01_ID;
                string 操作人 = 領藥台_01_登入者姓名;
                string 藥師證字號 = 領藥台_01_藥師證字號;
                string 顏色 = 領藥台_01_顏色;
                double 總異動量 = list_醫令資料[i][(int)enum_醫囑資料.交易量].ObjectToString().StringToDouble();
                string 效期 = "";
                string 收支原因 = "";


                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.診別 = 診別;
                takeMedicineStackClass.顏色 = 顏色;
                takeMedicineStackClass.藥品名稱 = 藥品名稱;
                takeMedicineStackClass.藥袋序號 = 藥袋序號;
                takeMedicineStackClass.病歷號 = 病歷號;
                takeMedicineStackClass.病人姓名 = 病人姓名;
                takeMedicineStackClass.床號 = 床號;
                takeMedicineStackClass.開方時間 = 開方時間;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.總異動量 = 總異動量.ToString();
                takeMedicineStackClass.效期 = 效期;
                takeMedicineStackClass.收支原因 = 收支原因;
                takeMedicineStackClass.ID = ID;

                bool flag_檢查過帳 = false;
                medClasses_buf = keyValuePairs_medcloud.SortDictionaryByCode(藥品碼);
                if (medClasses_buf.Count > 0)
                {
                    if (medClasses_buf[0].高價藥品.ToUpper() == true.ToString().ToUpper())
                    {
                        flag_檢查過帳 = true;
                    }
                    if (medClasses_buf[0].管制級別.StringIsEmpty() == false && medClasses_buf[0].管制級別 != "N")
                    {
                        flag_檢查過帳 = true;
                    }
                }

                if (pLC_Device.Bool == false || flag_檢查過帳 == true)
                {
                    if (list_醫令資料[i][(int)enum_醫囑資料.狀態].ObjectToString() == enum_醫囑資料_狀態.已過帳.GetEnumName())
                    {
                        takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.已領用過.GetEnumName();
                    }
                }
                if (flag_雙人覆核)
                {
                    this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                    continue;
                }

                takeMedicineStackClasses.Add(takeMedicineStackClass);

            }
            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);
        }
        private void PlC_RJ_Button_條碼輸入_MouseDownEvent(MouseEventArgs mevent)
        {
            if(Function_檢查是否完成交班() == false)
            {
                MyMessageBox.ShowDialog("請先完成交班");
                return;
            }
            Dialog_條碼輸入 dialog_條碼輸入 = new Dialog_條碼輸入();
            if (dialog_條碼輸入.ShowDialog() != DialogResult.Yes) return;
            string Barcode = dialog_條碼輸入.Value;
            if (plC_Button_領藥台_01_領.Bool)
            {
                this.Function_領藥台_01_醫令領藥(Barcode);
            }
            else if (plC_Button_領藥台_01_退.Bool)
            {
                this.Function_領藥台_01_醫令退藥(Barcode);
            }
        }
        private void PlC_RJ_Button_手輸醫令_MouseDownEvent(MouseEventArgs mevent)
        {
            if (Function_檢查是否完成交班() == false)
            {
                MyMessageBox.ShowDialog("請先完成交班");
                return;
            }
            this.Invoke(new Action(delegate
            {
                Dialog_手輸醫令.enum_狀態 enum_狀態 = Dialog_手輸醫令.enum_狀態.領藥;
                if (this.plC_Button_領藥台_01_領.Bool)
                {
                    enum_狀態 = Dialog_手輸醫令.enum_狀態.領藥;
                }
                if (this.plC_Button_領藥台_01_退.Bool)
                {
                    enum_狀態 = Dialog_手輸醫令.enum_狀態.退藥;
                }
                Dialog_手輸醫令 dialog_手輸醫令 = new Dialog_手輸醫令((Main_Form)this.FindForm(), this.sqL_DataGridView_藥品資料_藥檔資料, enum_狀態);
                dialog_手輸醫令.ShowDialog();
                List<object[]> list_value = dialog_手輸醫令.Value;
                if (list_value.Count == 0) return;
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_藥品資料_buf = new List<object[]>();
                bool flag_雙人覆核 = false;
                List<object[]> list_藥品設定表 = this.sqL_DataGridView_藥品設定表.SQL_GetAllRows(false);
                List<object[]> list_藥品管制方式設定 = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
                List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
                for (int i = 0; i < list_value.Count; i++)
                {
                    string GUID = Guid.NewGuid().ToString();
                    string 調劑台名稱 = this.領藥台_01名稱;

                    string 藥品碼 = list_value[i][(int)enum_選擇藥品.藥品碼].ObjectToString();
                    string 床號 = dialog_手輸醫令.transactionsClass.病房號;
                    list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                    if (list_藥品資料_buf.Count == 0) continue;
                    string 藥品名稱 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                    string 藥袋序號 = "";
                    string 單位 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                    string 病歷號 = dialog_手輸醫令.transactionsClass.病歷號;
                    string 領藥號 = dialog_手輸醫令.transactionsClass.領藥號;
                    string 病房號 = dialog_手輸醫令.transactionsClass.病房號;
                    string 病人姓名 = dialog_手輸醫令.transactionsClass.病人姓名;
                    string 開方時間 = DateTime.Now.ToDateTimeString_6();
                    string ID = 領藥台_01_ID;
                    string 操作人 = 領藥台_01_登入者姓名;
                    string 藥師證字號 = 領藥台_01_藥師證字號;
                    string 顏色 = 領藥台_01_顏色;
                    string 收支原因 = "";
                    double 總異動量 = list_value[i][(int)enum_選擇藥品.交易量].ObjectToString().StringToDouble();
                    enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.掃碼領藥;
                    if (總異動量 <= 0)
                    {
                        動作 = enum_交易記錄查詢動作.手輸領藥;
                    }
                    else
                    {
                        動作 = enum_交易記錄查詢動作.手輸退藥;
                    }


                    string 效期 = "";
                    takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                    takeMedicineStackClass.GUID = GUID;
                    takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                    takeMedicineStackClass.動作 = 動作.GetEnumName();
                    takeMedicineStackClass.顏色 = 顏色;
                    takeMedicineStackClass.藥品碼 = 藥品碼;
                    takeMedicineStackClass.藥品名稱 = 藥品名稱;
                    takeMedicineStackClass.藥袋序號 = 藥袋序號;
                    takeMedicineStackClass.單位 = 單位;
                    takeMedicineStackClass.病歷號 = 病歷號;
                    takeMedicineStackClass.床號 = 床號;
                    takeMedicineStackClass.領藥號 = 領藥號;
                    takeMedicineStackClass.病房號 = 病房號;
                    takeMedicineStackClass.病人姓名 = 病人姓名;
                    takeMedicineStackClass.開方時間 = 開方時間;
                    takeMedicineStackClass.操作人 = 操作人;
                    takeMedicineStackClass.藥師證字號 = 藥師證字號;
                    takeMedicineStackClass.顏色 = 顏色;
                    takeMedicineStackClass.總異動量 = 總異動量.ToString();
                    takeMedicineStackClass.效期 = 效期;
                    takeMedicineStackClass.ID = ID;

                    if (flag_雙人覆核)
                    {
                        this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClass);
                        continue;
                    }
                    takeMedicineStackClasses.Add(takeMedicineStackClass);

                }
                List<Task> taskList = new List<Task>();
                taskList.Clear();

                taskList.Add(Task.Run(new Action(delegate
                {
                    this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);
                })));
                Task.WhenAll(taskList).Wait();

            }));
        }
        private void PlC_RJ_Button_藥品調入_MouseDownEvent(MouseEventArgs mevent)
        {
            if (Function_檢查是否完成交班() == false)
            {
                MyMessageBox.ShowDialog("請先完成交班");
                return;
            }
            Dialog_藥品調入 dialog_藥品調入 = new Dialog_藥品調入();
            dialog_藥品調入.ShowDialog();
        }
        private void PlC_RJ_Button_全部滅燈_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否全部滅燈?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            LoadingForm.ShowLoadingForm();
            try
            {
                List<Drawer> drawers_epd583 = new List<Drawer>();
                List<Storage> storages_epd266 = new List<Storage>();
                List<RowsLED> rowsLEDs = new List<RowsLED>();

                List<CommonSapceClass> commonSapceClasses = Function_取得共用區所有儲位();
                for (int i = 0; i < commonSapceClasses.Count; i++)
                {
                    drawers_epd583.LockAdd(commonSapceClasses[i].List_EPD583);
                    storages_epd266.LockAdd(commonSapceClasses[i].List_EPD266);
                    rowsLEDs.LockAdd(commonSapceClasses[i].List_RowsLED);
                }
                drawers_epd583.LockAdd(List_EPD583_本地資料);
                storages_epd266.LockAdd(List_EPD266_本地資料);
                rowsLEDs.LockAdd(List_RowsLED_本地資料);

                List<Task> tasks = new List<Task>();

                for (int i = 0; i < drawers_epd583.Count; i++)
                {
                    Drawer drawer = drawers_epd583[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        drawerUI_EPD_583.Set_LED_Clear_UDP(drawer);
                    })));
                }


                for (int i = 0; i < storages_epd266.Count; i++)
                {
                    Storage storage = storages_epd266[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        storageUI_EPD_266.Set_Stroage_LED_UDP(storage, Color.Black);
                    })));
                }

                for (int i = 0; i < rowsLEDs.Count; i++)
                {
                    RowsLED rowsLED = rowsLEDs[i];
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        rowsLEDUI.Set_Rows_LED_Clear_UDP(rowsLED);
                    })));
                }

                List<object[]> list_value = sqL_DataGridView_LCD114_索引表.SQL_GetAllRows(false);
                for (int i = 0; i < list_value.Count; i++)
                {
                    string IP = list_value[i][(int)enum_LCD114_索引表.index_IP].ObjectToString();
                    try
                    {
                        tasks.Add(Task.Run(new Action(delegate
                        {
                            storageUI_LCD_114.ClearCanvas(IP, 29008);
                        })));

                    }
                    catch
                    {

                    }


                }
                Task.WhenAll(tasks).Wait();
            }
            catch
            {
            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }


        }
        private void PlC_RJ_Button_交班對點_MouseDownEvent(MouseEventArgs mevent)
        {
            StorageAlarm = false;
            try
            {
                Dialog_交班對點 dialog_交班對點 = new Dialog_交班對點();
                dialog_交班對點.flag_單人交班 = plC_CheckBox_單人交班.Checked;
                dialog_交班對點.ShowDialog();
            }
            catch
            {

            }
            finally
            {
                StorageAlarm = true;
            }

        }
        private void PlC_RJ_Button_藥品搜索_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_藥品搜索 dialog_藥品搜索 = new Dialog_藥品搜索();
            dialog_藥品搜索.ShowDialog();
        }
        private void PlC_RJ_Button_申領_MouseDownEvent(MouseEventArgs mevent)
        {
            if (Function_檢查是否完成交班() == false)
            {
                MyMessageBox.ShowDialog("請先完成交班");
                return;
            }
            Dialog_申領 dialog_申領 = new Dialog_申領();
            dialog_申領.ShowDialog();
        }

        private void ToolStripMenuItem_調劑畫面_顯示設定_Click(object sender, EventArgs e)
        {
            Control control = contextMenuStrip_調劑畫面.SourceControl;

            if (control.Name == "sqL_DataGridView_領藥台_01_領藥內容")
            {
                Dialog_調劑畫面顯示調整 dialog_調劑畫面顯示調整 = new Dialog_調劑畫面顯示調整(0);
                if (dialog_調劑畫面顯示調整.ShowDialog() != DialogResult.Yes) return;
                SaveConfig工程模式();
            }
            if (control.Name == "sqL_DataGridView_領藥台_02_領藥內容")
            {
                Dialog_調劑畫面顯示調整 dialog_調劑畫面顯示調整 = new Dialog_調劑畫面顯示調整(1);
                if (dialog_調劑畫面顯示調整.ShowDialog() != DialogResult.Yes) return;
                SaveConfig工程模式();
            }
            if (control.Name == "sqL_DataGridView_領藥台_03_領藥內容")
            {
                Dialog_調劑畫面顯示調整 dialog_調劑畫面顯示調整 = new Dialog_調劑畫面顯示調整(2);
                if (dialog_調劑畫面顯示調整.ShowDialog() != DialogResult.Yes) return;
                SaveConfig工程模式();
            }
            if (control.Name == "sqL_DataGridView_領藥台_04_領藥內容")
            {
                Dialog_調劑畫面顯示調整 dialog_調劑畫面顯示調整 = new Dialog_調劑畫面顯示調整(3);
                if (dialog_調劑畫面顯示調整.ShowDialog() != DialogResult.Yes) return;
                SaveConfig工程模式();
            }
        }

        private List<object[]> Function_領藥內容_重新排序(List<object[]> list_value)
        {
            List<object[]> list_value_buf = new List<object[]>();
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.新增資料.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.盲盤完成.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.複盤完成.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待作業.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.作業完成.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.等待入賬.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.新增效期.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.輸入新效期.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.已領用過.GetEnumName()));
            list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.DC處方.GetEnumName()));
            if (!plC_CheckBox_領藥無儲位不顯示.Checked) list_value_buf.LockAdd(list_value.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.無儲位.GetEnumName()));
            return list_value_buf;
        }
 
    }
}
