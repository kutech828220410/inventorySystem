﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using SQLUI;
namespace 調劑台管理系統
{
   
   
    public partial class Main_Form : Form
    {

        private void Program_醫令資料_Init()
        {


            string url = $"{dBConfigClass.Api_URL}/api/order/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"醫令資料表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }

            this.sqL_DataGridView_醫令資料.Init(table);
            this.sqL_DataGridView_醫令資料.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥局代碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥袋類型);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥袋條碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.領藥號);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.批序);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.單次劑量);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.劑量單位);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.途徑);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.頻次);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.費用別);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.病房);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.床號);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.病人姓名);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.病歷號);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_醫囑資料.交易量);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.開方日期);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.過帳時間);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.展藥時間);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.備註);

            this.sqL_DataGridView_醫令資料.DataGridRowsChangeRefEvent += SqL_DataGridView_醫令資料_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_醫令資料.DataGridRefreshEvent += SqL_DataGridView_醫令資料_DataGridRefreshEvent;
            this.sqL_DataGridView_醫令資料.DataGridRowsChangeEvent += SqL_DataGridView_醫令資料_DataGridRowsChangeEvent;


            this.plC_RJ_Button_醫令資料_自動過帳.MouseDownEvent += PlC_RJ_Button_醫令資料_自動過帳_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_設定產出時間.MouseDownEvent += PlC_RJ_Button_醫令資料_設定產出時間_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_設為未過帳.MouseDownEvent += PlC_RJ_Button_醫令資料_設為未過帳_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_定期API測試.MouseDownEvent += PlC_RJ_Button_醫令資料_定期API測試_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_選取資料刪除.MouseDownEvent += PlC_RJ_Button_醫令資料_選取資料刪除_MouseDownEvent;

            this.plC_RJ_Button_醫令資料_搜尋條件_開方日期_搜尋.MouseDownEvent += PlC_RJ_Button_醫令資料_搜尋條件_開方日期_搜尋_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_搜尋條件_藥品碼_搜尋.MouseDownEvent += PlC_RJ_Button_醫令資料_搜尋條件_藥品碼_搜尋_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_搜尋條件_藥品名稱_搜尋.MouseDownEvent += PlC_RJ_Button_醫令資料_搜尋條件_藥品名稱_搜尋_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_搜尋條件_病歷號_搜尋.MouseDownEvent += PlC_RJ_Button_醫令資料_搜尋條件_病歷號_搜尋_MouseDownEvent;
            this.plC_RJ_Button_醫令資料_搜尋條件_藥袋條碼_搜尋.MouseDownEvent += PlC_RJ_Button_醫令資料_搜尋條件_藥袋條碼_搜尋_MouseDownEvent;

            this.plC_UI_Init.Add_Method(Program_醫令資料);
        }

 

        private void Program_醫令資料()
        {
            this.sub_Program_醫令資料_檢查刷條碼();
            this.sub_Program_醫令資料_定期API測試();
        }
        #region PLC_醫令資料_檢查刷條碼
        PLC_Device PLC_Device_醫令資料_檢查刷條碼 = new PLC_Device("");
        PLC_Device PLC_Device_醫令資料_檢查刷條碼_OK = new PLC_Device("");
        MyTimer MyTimer_醫令資料_檢查刷條碼_結束延遲 = new MyTimer();
        MyTimer MyTimer_醫令資料_檢查刷條碼_刷藥單延遲 = new MyTimer();
        bool flag_醫令資料_檢查刷條碼_01 = false;
        bool flag_醫令資料_檢查刷條碼_02 = false;
        int cnt_Program_醫令資料_檢查刷條碼 = 65534;
        void sub_Program_醫令資料_檢查刷條碼()
        {
            if (this.plC_ScreenPage_Main.PageText == "醫令資料") PLC_Device_醫令資料_檢查刷條碼.Bool = true;
            else PLC_Device_醫令資料_檢查刷條碼.Bool = false;
            if (cnt_Program_醫令資料_檢查刷條碼 == 65534)
            {
                this.MyTimer_醫令資料_檢查刷條碼_結束延遲.StartTickTime(10000);
                PLC_Device_醫令資料_檢查刷條碼.SetComment("PLC_醫令資料_檢查刷條碼");
                PLC_Device_醫令資料_檢查刷條碼_OK.SetComment("PLC_醫令資料_檢查刷條碼_OK");
                PLC_Device_醫令資料_檢查刷條碼.Bool = false;
                cnt_Program_醫令資料_檢查刷條碼 = 65535;
            }
            if (cnt_Program_醫令資料_檢查刷條碼 == 65535) cnt_Program_醫令資料_檢查刷條碼 = 1;
            if (cnt_Program_醫令資料_檢查刷條碼 == 1) cnt_Program_醫令資料_檢查刷條碼_檢查按下(ref cnt_Program_醫令資料_檢查刷條碼);
            if (cnt_Program_醫令資料_檢查刷條碼 == 2) cnt_Program_醫令資料_檢查刷條碼_初始化(ref cnt_Program_醫令資料_檢查刷條碼);
            if (cnt_Program_醫令資料_檢查刷條碼 == 3) cnt_Program_醫令資料_檢查刷條碼_等待延遲(ref cnt_Program_醫令資料_檢查刷條碼);
            if (cnt_Program_醫令資料_檢查刷條碼 == 4) cnt_Program_醫令資料_檢查刷條碼_搜尋醫令(ref cnt_Program_醫令資料_檢查刷條碼);
            if (cnt_Program_醫令資料_檢查刷條碼 == 5) cnt_Program_醫令資料_檢查刷條碼 = 65500;
            if (cnt_Program_醫令資料_檢查刷條碼 > 1) cnt_Program_醫令資料_檢查刷條碼_檢查放開(ref cnt_Program_醫令資料_檢查刷條碼);

            if (cnt_Program_醫令資料_檢查刷條碼 == 65500)
            {
                this.MyTimer_醫令資料_檢查刷條碼_結束延遲.TickStop();
                this.MyTimer_醫令資料_檢查刷條碼_結束延遲.StartTickTime(10000);
                PLC_Device_醫令資料_檢查刷條碼.Bool = false;
                PLC_Device_醫令資料_檢查刷條碼_OK.Bool = false;
                cnt_Program_醫令資料_檢查刷條碼 = 65535;
            }
        }
        void cnt_Program_醫令資料_檢查刷條碼_檢查按下(ref int cnt)
        {
            if (PLC_Device_醫令資料_檢查刷條碼.Bool) cnt++;
        }
        void cnt_Program_醫令資料_檢查刷條碼_檢查放開(ref int cnt)
        {
            if (!PLC_Device_醫令資料_檢查刷條碼.Bool) cnt = 65500;
        }
        void cnt_Program_醫令資料_檢查刷條碼_初始化(ref int cnt)
        {
            string 一維碼 = "";
            flag_醫令資料_檢查刷條碼_01 = false;
            flag_醫令資料_檢查刷條碼_02 = false;

            if (MySerialPort_Scanner01.ReadByte() != null)
            {
                flag_醫令資料_檢查刷條碼_01 = true;
                MyTimer_醫令資料_檢查刷條碼_刷藥單延遲.TickStop();
                MyTimer_醫令資料_檢查刷條碼_刷藥單延遲.StartTickTime(200);
                cnt++;
                return;
            }
            if (MySerialPort_Scanner02.ReadByte() != null)
            {
                flag_醫令資料_檢查刷條碼_02 = true;
                MyTimer_醫令資料_檢查刷條碼_刷藥單延遲.TickStop();
                MyTimer_醫令資料_檢查刷條碼_刷藥單延遲.StartTickTime(200);
                cnt++;
                return;
            }


        }
        void cnt_Program_醫令資料_檢查刷條碼_等待延遲(ref int cnt)
        {
            if (MyTimer_醫令資料_檢查刷條碼_刷藥單延遲.IsTimeOut())
            {
                cnt++;
            }

        }
        void cnt_Program_醫令資料_檢查刷條碼_搜尋醫令(ref int cnt)
        {
            string 一維碼 = "";


            if (flag_醫令資料_檢查刷條碼_01)
            {
                string text = MySerialPort_Scanner01.ReadString();
                if (text == null) return;
                text = text.Replace("\0", "");
                if (text.StringIsEmpty()) return;

                if (text.Length <= 2 || text.Length > 200) return;
                //if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                MySerialPort_Scanner01.ClearReadByte();
                text = text.Replace("\r\n", "");
                一維碼 = text;
            }
            if (flag_醫令資料_檢查刷條碼_02)
            {

                string text = MySerialPort_Scanner02.ReadString();
                text = text.Replace("\0", "");
                if (text == null) return;
                if (text.StringIsEmpty()) return;
     
                if (text.Length <= 2 || text.Length > 200) return;
                //if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                MySerialPort_Scanner02.ClearReadByte();
                text = text.Replace("\r\n", "");
                一維碼 = text;
            }
            if (一維碼.StringIsEmpty()) return;
            this.Invoke(new Action(delegate 
            {
                this.rJ_TextBox_醫令資料_搜尋條件_藥袋條碼.Texts = 一維碼;
                this.PlC_RJ_Button_醫令資料_搜尋條件_藥袋條碼_搜尋_MouseDownEvent(null);
            }));
   
            cnt++;
        }





        #endregion
        #region PLC_醫令資料_定期API測試
        PLC_Device PLC_Device_醫令資料_定期API測試 = new PLC_Device("");
        PLC_Device PLC_Device_醫令資料_定期API測試_OK = new PLC_Device("");
        Task Task_醫令資料_定期API測試;
        MyTimer MyTimer_醫令資料_定期API測試_結束延遲 = new MyTimer();
        int cnt_Program_醫令資料_定期API測試 = 65534;
        void sub_Program_醫令資料_定期API測試()
        {
            PLC_Device_醫令資料_定期API測試.Bool = true;
            if (cnt_Program_醫令資料_定期API測試 == 65534)
            {
                this.MyTimer_醫令資料_定期API測試_結束延遲.StartTickTime(3000);
                PLC_Device_醫令資料_定期API測試.SetComment("PLC_醫令資料_定期API測試");
                PLC_Device_醫令資料_定期API測試_OK.SetComment("PLC_醫令資料_定期API測試_OK");
                PLC_Device_醫令資料_定期API測試.Bool = false;
                cnt_Program_醫令資料_定期API測試 = 65535;
            }
            if (cnt_Program_醫令資料_定期API測試 == 65535) cnt_Program_醫令資料_定期API測試 = 1;
            if (cnt_Program_醫令資料_定期API測試 == 1) cnt_Program_醫令資料_定期API測試_檢查按下(ref cnt_Program_醫令資料_定期API測試);
            if (cnt_Program_醫令資料_定期API測試 == 2) cnt_Program_醫令資料_定期API測試_初始化(ref cnt_Program_醫令資料_定期API測試);
            if (cnt_Program_醫令資料_定期API測試 == 3) cnt_Program_醫令資料_定期API測試 = 65500;
            if (cnt_Program_醫令資料_定期API測試 > 1) cnt_Program_醫令資料_定期API測試_檢查放開(ref cnt_Program_醫令資料_定期API測試);

            if (cnt_Program_醫令資料_定期API測試 == 65500)
            {
                this.MyTimer_醫令資料_定期API測試_結束延遲.TickStop();
                this.MyTimer_醫令資料_定期API測試_結束延遲.StartTickTime(180000);
                PLC_Device_醫令資料_定期API測試.Bool = false;
                PLC_Device_醫令資料_定期API測試_OK.Bool = false;
                cnt_Program_醫令資料_定期API測試 = 65535;
            }
        }
        void cnt_Program_醫令資料_定期API測試_檢查按下(ref int cnt)
        {
            if (PLC_Device_醫令資料_定期API測試.Bool) cnt++;
        }
        void cnt_Program_醫令資料_定期API測試_檢查放開(ref int cnt)
        {
            if (!PLC_Device_醫令資料_定期API測試.Bool) cnt = 65500;
        }
        void cnt_Program_醫令資料_定期API測試_初始化(ref int cnt)
        {
            if (this.MyTimer_醫令資料_定期API測試_結束延遲.IsTimeOut())
            {
                if (Task_醫令資料_定期API測試 == null)
                {
                    Task_醫令資料_定期API測試 = new Task(new Action(delegate { PlC_RJ_Button_醫令資料_定期API測試_MouseDownEvent(null); }));
                }
                if (Task_醫令資料_定期API測試.Status == TaskStatus.RanToCompletion)
                {
                    Task_醫令資料_定期API測試 = new Task(new Action(delegate { PlC_RJ_Button_醫令資料_定期API測試_MouseDownEvent(null); }));
                }
                if (Task_醫令資料_定期API測試.Status == TaskStatus.Created)
                {
                    Task_醫令資料_定期API測試.Start();
                }
                cnt++;
            }
        }







        #endregion

        #region Function
        private List<object[]> Function_醫令資料_API呼叫(string barcode , int value)
        {
            List<OrderClass> orderClasses = this.Function_醫令資料_API呼叫(dBConfigClass.OrderApiURL, barcode, value);
            List<object[]> list_value = new List<object[]>();
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            for (int i = 0; i < orderClasses.Count; i++)
            {
                string pri_key = orderClasses[i].PRI_KEY;
                List<object[]> list_value_buf = this.sqL_DataGridView_醫令資料.SQL_GetRows((int)enum_醫囑資料.PRI_KEY, pri_key, false);
                list_value.LockAdd(list_value_buf);
            }
            Console.Write($"醫令資料搜尋共<{list_value.Count}>筆,耗時{myTimer.ToString()}ms\n");
            return list_value;
        }
        private List<object[]> Function_醫令資料_API呼叫(string barcode , bool 單醫令模式)
        {   
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string url = "";
            if (單醫令模式) url = dBConfigClass.OrderByCodeApiURL;
            else url = dBConfigClass.OrderApiURL;
            List<OrderClass> orderClasses = this.Function_醫令資料_API呼叫(url, barcode);
            List<object[]> list_value = orderClasses.ClassToSQL<OrderClass ,enum_醫囑資料>();
            //for (int i = 0; i < orderClasses.Count; i++)
            //{
            //    string pri_key = orderClasses[i].PRI_KEY;
            //    List<object[]> list_value_buf = this.sqL_DataGridView_醫令資料.SQL_GetRows((int)enum_醫囑資料.PRI_KEY, pri_key, false);
            //    list_value.LockAdd(list_value_buf);
            //}
            Console.Write($"醫令資料搜尋共<{list_value.Count}>筆,耗時{myTimer.ToString()}ms\n");
            return list_value;
        }
        private List<OrderClass> Function_醫令資料_API呼叫(string url, string barcode, int num)
        {
            barcode = barcode.Replace("\r\n", "");
            barcode = Uri.EscapeDataString(barcode);
            List<OrderClass> orderClasses = new List<OrderClass>();
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string apitext = $"{url}/?Barcode={barcode}&num={num}";

            Console.Write($"Call api : {apitext}\n");
            string jsonString = Basic.Net.WEBApiGet(apitext);
            Console.Write($"{jsonString}\n");
            Console.Write($"耗時 {myTimer.ToString()}ms\n");
            if (jsonString.StringIsEmpty())
            {
                this.voice.SpeakOnTask("網路異常");
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"呼叫串接資料失敗!請檢查網路連線...", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog($"呼叫串接資料失敗!請檢查網路連線...");
                return orderClasses;
            }
            returnData returnData = jsonString.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                this.voice.SpeakOnTask("藥單條碼錯誤");
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"藥單條碼錯誤:{jsonString}", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog(jsonString);
                return new List<OrderClass>();
            }
            orderClasses = returnData.Data.ObjToListClass<OrderClass>();
            if (orderClasses == null)
            {
                Console.WriteLine($"串接資料傳回格式錯誤!");
                this.voice.SpeakOnTask("回傳資料錯誤");
                orderClasses = new List<OrderClass>();

            }

            return orderClasses;
        }
        private List<OrderClass> Function_醫令資料_API呼叫(string url, string barcode)
        {
            barcode = barcode.Replace("\r\n", "");
            barcode = Uri.EscapeDataString(barcode);
            List<OrderClass> orderClasses = new List<OrderClass>();
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string apitext = $"{url}{barcode}";
         
            Console.Write($"Call api : {apitext}\n");
            string jsonString = Basic.Net.WEBApiGet(apitext);
            Console.Write($"{jsonString}\n");
            Console.Write($"耗時 {myTimer.ToString()}ms\n");
            if (jsonString.StringIsEmpty())
            {
                this.voice.SpeakOnTask("網路異常");
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"呼叫串接資料失敗!請檢查網路連線", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog($"呼叫串接資料失敗!請檢查網路連線...");
                return orderClasses;
            }
            returnData returnData = jsonString.JsonDeserializet<returnData>();
            if(returnData == null)
            {
                this.voice.SpeakOnTask("藥單條碼錯誤");
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"藥單條碼錯誤:{jsonString}", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog(jsonString);
                return new List<OrderClass>();
            }
            if(returnData.Code != 200)
            {
                //MyMessageBox.ShowDialog($"{returnData.Result}");
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示($"{returnData.Result}", 2000);
                dialog_錯誤提示.ShowDialog();
                return new List<OrderClass>();
                
            }
            orderClasses = returnData.Data.ObjToListClass<OrderClass>();
            if (orderClasses == null)
            {
                Console.WriteLine($"串接資料傳回格式錯誤!");
                this.voice.SpeakOnTask("資料錯誤");
                orderClasses = new List<OrderClass>();

            }

            return orderClasses;
        }
        #endregion
        #region Event
        private void SqL_DataGridView_醫令資料_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_醫令資料.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_醫令資料.dataGridView.Rows[i].Cells[(int)enum_醫囑資料.狀態].Value.ToString();
                if (狀態 == enum_醫囑資料_狀態.已過帳.GetEnumName())
                {
                    this.sqL_DataGridView_醫令資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_醫令資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                if (狀態 == enum_醫囑資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_醫令資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_醫令資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                if (狀態 == enum_醫囑資料_狀態.無儲位.GetEnumName())
                {
                    this.sqL_DataGridView_醫令資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_醫令資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void SqL_DataGridView_醫令資料_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            for (int i = 0; i < RowsList.Count; i++)
            { 
                if(RowsList[i][(int)enum_醫囑資料.狀態].ObjectToString() == enum_醫囑資料_狀態.未過帳.GetEnumName())
                {

                }
            }
            RowsList.Sort(new ICP_醫令資料());
        }
        private void SqL_DataGridView_醫令資料_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
        
        }
        private void PlC_RJ_Button_醫令資料_設定產出時間_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("請選取資料!");
                return;
            }

            Dialog_設定產出時間 dialog_設定產出時間 = new Dialog_設定產出時間();
            if (dialog_設定產出時間.ShowDialog() != DialogResult.Yes) return;
            if (dialog_設定產出時間.Value.CompareTo(DateTime.Now) >= 0)
            {
                MyMessageBox.ShowDialog("設定日期時間不得大於現在!");
                return;
            }
            string str_datetime = dialog_設定產出時間.Value.ToDateTimeString_6();
            for (int i = 0; i < list_value.Count; i++)
            {
                list_value[i][(int)enum_醫囑資料.產出時間] = str_datetime;
                list_value[i][(int)enum_醫囑資料.狀態] = enum_醫囑資料_狀態.未過帳.GetEnumName();
            }
            this.sqL_DataGridView_醫令資料.SQL_ReplaceExtra(list_value, false);
            this.sqL_DataGridView_醫令資料.ReplaceExtra(list_value, true);
        }
        private void PlC_RJ_Button_醫令資料_自動過帳_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_交易紀錄新增資料_AddValue = new List<object[]>();
            DateTime dateTime_start = DateTime.Now.AddHours(-1).AddMinutes(-10);
            DateTime dateTime_end = DateTime.Now.AddHours(-1).AddMinutes(0);
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.SQL_GetRowsByBetween((int)enum_醫囑資料.產出時間, dateTime_start, dateTime_end, false);
            List<object[]> list_replace = new List<object[]>();
            Console.Write($"取得醫令資料共{list_value.Count}筆資料,{dateTime_start.ToString()} 至 {dateTime_end.ToString()},耗時{myTimer.ToString()}ms\n");
            if (mevent != null) this.Function_從SQL取得儲位到雲端資料();
            Console.Write($"SQL取得儲位到雲端資料,耗時{myTimer.ToString()}ms\n");
            Dialog_Prcessbar dialog_Prcessbar = null;
            if (mevent != null) dialog_Prcessbar = new Dialog_Prcessbar(list_value.Count);
            if (mevent != null) dialog_Prcessbar.State = "醫令自動入賬..";

            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 藥袋序號 = "";
            string 病人姓名 = "";
            string 病歷號 = "";
            string 開方時間 = "";
            string 備註 = "";
            int 交易量 = 0;
            int 庫存量 = 0;
            int 結存量 = 0;
            List<string> List_效期 = new List<string>();
            List<string> List_批號 = new List<string>();


            for (int i = 0; i < list_value.Count; i++)
            {
                if (mevent != null) dialog_Prcessbar.Value = i;
                if (list_value[i][(int)enum_醫囑資料.狀態].ObjectToString() != enum_醫囑資料_狀態.未過帳.GetEnumName()) continue;
                藥品碼 = list_value[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                藥品名稱 = list_value[i][(int)enum_醫囑資料.藥品名稱].ObjectToString();
                病人姓名 = list_value[i][(int)enum_醫囑資料.病人姓名].ObjectToString();
                藥袋序號 = list_value[i][(int)enum_醫囑資料.藥袋條碼].ObjectToString();
                病歷號 = list_value[i][(int)enum_醫囑資料.病歷號].ObjectToString();
                開方時間 = list_value[i][(int)enum_醫囑資料.開方日期].ToDateTimeString();
                交易量 = list_value[i][(int)enum_醫囑資料.交易量].ObjectToString().StringToInt32();
                庫存量 = this.Function_從雲端資料取得庫存(藥品碼);
                結存量 = 交易量 + 庫存量;
                備註 = "";
                if (庫存量 == -999)
                {
                    list_value[i][(int)enum_醫囑資料.狀態] = enum_醫囑資料_狀態.無儲位.GetEnumName();
                    list_replace.Add(list_value[i]);
                    continue;
                }
                if (交易量 + 庫存量 < 0)
                {
                    list_value[i][(int)enum_醫囑資料.狀態] = enum_醫囑資料_狀態.庫存不足.GetEnumName();
                    list_replace.Add(list_value[i]);
                    continue;
                }
                List<object[]> list_儲位資訊 = this.Function_取得異動儲位資訊從雲端資料(藥品碼, 交易量);
                List_效期.Clear();
                for (int k = 0; k < list_儲位資訊.Count; k++)
                {
                    this.Function_庫存異動至雲端資料(list_儲位資訊[k], false);
                    List_效期.Add(list_儲位資訊[k][(int)enum_儲位資訊.效期].ObjectToString());
                }
                list_value[i][(int)enum_醫囑資料.狀態] = enum_醫囑資料_狀態.已過帳.GetEnumName();
                list_replace.Add(list_value[i]);

                //新增交易紀錄資料
                for (int k = 0; k < List_效期.Count; k++)
                {
                    備註 += $"效期[{List_效期[k]}]";
                    if (k != List_效期.Count - 1) 備註 += ",";
                }

                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = Guid.NewGuid().ToString();
                value_trading[(int)enum_交易記錄查詢資料.動作] = enum_交易記錄查詢動作.自動過帳.GetEnumName();
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = "Auto";
                value_trading[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
                value_trading[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = DateTime.Now.ToDateTimeString_6();
                value_trading[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
                if (開方時間.StringIsEmpty())
                {
                    開方時間 = DateTime.Now.ToDateTimeString_6();
                }
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;
                list_交易紀錄新增資料_AddValue.Add(value_trading);


            }
            this.Function_雲端資料上傳至SQL();
            Console.Write($"過帳至儲位,耗時{myTimer.ToString()}ms\n");
            if (mevent != null) dialog_Prcessbar.State = "醫令上傳..";
            if (list_replace.Count > 0) this.sqL_DataGridView_醫令資料.SQL_ReplaceExtra(list_replace, false);
            if (list_交易紀錄新增資料_AddValue.Count > 0) this.sqL_DataGridView_交易記錄查詢.SQL_AddRows(list_交易紀錄新增資料_AddValue, false);
            if (mevent != null) this.sqL_DataGridView_醫令資料.RefreshGrid(list_replace);
            Console.Write($"上傳醫令,耗時{myTimer.ToString()}ms\n");
            if (mevent != null) dialog_Prcessbar.Close();
        }
        private void PlC_RJ_Button_醫令資料_設為未過帳_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("請選取資料!");
                return;
            }
            for (int i = 0; i < list_value.Count; i++)
            {
                list_value[i][(int)enum_醫囑資料.狀態] = enum_醫囑資料_狀態.未過帳.GetEnumName();
            }
            this.sqL_DataGridView_醫令資料.SQL_ReplaceExtra(list_value, false);
            this.sqL_DataGridView_醫令資料.ReplaceExtra(list_value, true);
        }
        private void PlC_RJ_Button_醫令資料_搜尋條件_藥袋條碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<OrderClass> orderClasses = this.Function_醫令資料_API呼叫(dBConfigClass.OrderApiURL, this.rJ_TextBox_醫令資料_搜尋條件_藥袋條碼.Texts);
            Console.Write($"醫令API回傳共<{orderClasses.Count}>筆,耗時{myTimer.ToString()}ms\n");
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < orderClasses.Count; i++)
            {
                string pri_key = orderClasses[i].PRI_KEY;
                List<object[]> list_value_buf = this.sqL_DataGridView_醫令資料.SQL_GetRows((int)enum_醫囑資料.PRI_KEY, pri_key, false);
                list_value.LockAdd(list_value_buf);
            }
            Console.Write($"醫令資料搜尋共<{list_value.Count}>筆,耗時{myTimer.ToString()}ms\n");
            this.sqL_DataGridView_醫令資料.RefreshGrid(list_value);

        }
        private void PlC_RJ_Button_醫令資料_搜尋條件_開方日期_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic(50000);
            DateTime dateTime_st = new DateTime(dateTimePicke_醫令資料_開方日期_起始.Value.Year, dateTimePicke_醫令資料_開方日期_起始.Value.Month, dateTimePicke_醫令資料_開方日期_起始.Value.Day, 00, 00, 00);
            DateTime dateTime_end = new DateTime(dateTimePicke_醫令資料_開方日期_結束.Value.Year, dateTimePicke_醫令資料_開方日期_結束.Value.Month, dateTimePicke_醫令資料_開方日期_結束.Value.Day, 23, 59, 59);
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.SQL_GetRowsByBetween((int)enum_醫囑資料.開方日期, dateTime_st, dateTime_end, false);
            Console.WriteLine($"搜尋資料耗時{myTimerBasic.ToString()}");
            this.sqL_DataGridView_醫令資料.RefreshGrid(list_value);
            if(list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無資料", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("查無資料");
            }
        }
        private void PlC_RJ_Button_醫令資料_搜尋條件_病歷號_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic(50000);
            if (rJ_TextBox_醫令資料_搜尋條件_病歷號.Text.StringIsEmpty())
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("未輸入搜尋條件", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("未輸入搜尋條件");
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.SQL_GetRows((int)enum_醫囑資料.病歷號, rJ_TextBox_醫令資料_搜尋條件_病歷號.Text , false);
            Console.WriteLine($"搜尋資料耗時{myTimerBasic.ToString()}");
            this.sqL_DataGridView_醫令資料.RefreshGrid(list_value);
            if (list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無資料", 2000);
                dialog_錯誤提示.ShowDialog();

                //MyMessageBox.ShowDialog("查無資料");
            }
        }
        private void PlC_RJ_Button_醫令資料_搜尋條件_藥品名稱_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic(50000);
            if (rJ_TextBox_醫令資料_搜尋條件_藥品名稱.Text.StringIsEmpty())
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("未輸入搜尋條件", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("未輸入搜尋條件");
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.SQL_GetRows((int)enum_醫囑資料.藥品名稱, rJ_TextBox_醫令資料_搜尋條件_藥品名稱.Text, false);
            Console.WriteLine($"搜尋資料耗時{myTimerBasic.ToString()}");
            this.sqL_DataGridView_醫令資料.RefreshGrid(list_value);
            if (list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無資料", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("查無資料");
            }
        }
        private void PlC_RJ_Button_醫令資料_搜尋條件_藥品碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.SQL_GetRows((int)enum_醫囑資料.藥品碼 ,rJ_TextBox_醫令資料_搜尋條件_藥品碼.Text, false);
            if (rJ_TextBox_醫令資料_搜尋條件_藥品碼.Text.StringIsEmpty())
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("未輸入搜尋條件", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("未輸入搜尋條件");
                return;
            }
            this.sqL_DataGridView_醫令資料.RefreshGrid(list_value);
            if (list_value.Count == 0)
            {
                Dialog_錯誤提示 dialog_錯誤提示 = new Dialog_錯誤提示("查無資料", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog("查無資料");
            }

        }
        private void PlC_RJ_Button_醫令資料_定期API測試_MouseDownEvent(MouseEventArgs mevent)
        {
            string apitext = $"{dBConfigClass.OrderApiURL}";

            string jsonString = Basic.Net.WEBApiGet(apitext);
        }
        private void PlC_RJ_Button_醫令資料_選取資料刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_醫令資料.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            if (MyMessageBox.ShowDialog($"確認刪除選取{list_value.Count}筆資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            this.sqL_DataGridView_醫令資料.SQL_DeleteExtra(list_value, false);
            this.sqL_DataGridView_醫令資料.DeleteExtra(list_value, true);
        }
        #endregion

        private class ICP_醫令資料 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {

                string date01 = x[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                string date02 = y[(int)enum_醫囑資料.開方日期].ToDateTimeString_6();
                return date01.CompareTo(date02);

            }
        }
    }
}
