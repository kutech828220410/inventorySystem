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
using H_Pannel_lib;
using HIS_DB_Lib;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        Basic.MyThread MyThread_輸出入檢查;
        Basic.MyThread 輸出入檢查_蜂鳴器輸出;
        private void Program_輸出入檢查_Init()
        {
            for (int i = 0; i < 鎖控列表01.Controls.Count; i++)
            {
                if (鎖控列表01.Controls[i] is Pannel_Locker)
                {
                    this.List_Locker.Add((Pannel_Locker)鎖控列表01.Controls[i]);
                }
            }
            for (int i = 0; i < 鎖控列表02.Controls.Count; i++)
            {
                if (鎖控列表02.Controls[i] is Pannel_Locker)
                {
                    this.List_Locker.Add((Pannel_Locker)鎖控列表02.Controls[i]);
                }
            }

            this.MyThread_輸出入檢查 = new Basic.MyThread(this.FindForm());

            foreach (Pannel_Locker loker in this.List_Locker)
            {
                loker.Init();
                loker.ShowAdress = false;
                loker.OuputReverse = myConfigClass.外部輸出;
                this.MyThread_取藥堆疊資料_檢查資料.Add_Method(loker.sub_Program);
                loker.LockClosingEvent += Loker_LockClosingEvent;
                loker.MouseDownEvent += Loker_MouseDownEvent;
                loker.LockOpeningEvent += Loker_LockOpeningEvent;
                loker.LockAlarmEvent += Loker_LockAlarmEvent;
            }
            this.MyThread_取藥堆疊資料_檢查資料.Add_Method(this.sub_Program_輸出入檢查);

            //this.MyThread_輸出入檢查.Add_Method(this.sub_Program_輸出入檢查);
            //this.MyThread_輸出入檢查.SetSleepTime(1);
            //this.MyThread_輸出入檢查.AutoRun(true);
            //this.MyThread_輸出入檢查.AutoStop(false);
            //this.MyThread_輸出入檢查.Trigger();

            this.輸出入檢查_蜂鳴器輸出 = new MyThread();
            this.輸出入檢查_蜂鳴器輸出.Add_Method(this.sub_Program_輸出入檢查_蜂鳴器輸出);
            this.輸出入檢查_蜂鳴器輸出.SetSleepTime(50);
            this.輸出入檢查_蜂鳴器輸出.AutoRun(true);
            this.輸出入檢查_蜂鳴器輸出.AutoStop(false);
            this.輸出入檢查_蜂鳴器輸出.Trigger();
        }

      

        private void Loker_MouseDownEvent(PLC_Device pLC_Device_Input, PLC_Device pLC_Device_Output)
        {
            string OutputAdress = pLC_Device_Output.GetAdress();
            if (OutputAdress.StringIsEmpty()) return;
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            list_locker_table_value = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出位置, OutputAdress);
            if (list_locker_table_value.Count == 0) return;
            list_locker_table_value[0][(int)enum_Locker_Index_Table.輸出狀態] = true.ToString();

            this.sqL_DataGridView_Locker_Index_Table.SQL_Replace(list_locker_table_value[0], false);

        }
        private void Loker_LockClosingEvent(object sender, PLC_Device PLC_Device_Input, PLC_Device PLC_Device_Output, string Master_GUID)
        {
            //Master_GUID 為取藥堆疊母資料
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            list_locker_table_value = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出位置, PLC_Device_Output.GetAdress());
            if (list_locker_table_value.Count == 0) return;
            string IP = list_locker_table_value[0][(int)enum_Locker_Index_Table.IP].ObjectToString();
            string Num = list_locker_table_value[0][(int)enum_Locker_Index_Table.Num].ObjectToString();
            string 調劑台名稱 = "";
            Console.WriteLine($"{IP},{Num},<抽屜關閉> {DateTime.Now.ToDateTimeString()}");
            if (IP.Check_IP_Adress() && PLC_Device_主機輸出模式.Bool)
            {
                object value_device = this.Fucnction_從雲端資料取得儲位(IP);
                if (value_device == null) return;
                if (value_device is Storage)
                {
                    Storage storage = value_device as Storage;
                    if (storage.DeviceType == DeviceType.EPD266 || storage.DeviceType == DeviceType.EPD266_lock
                        || storage.DeviceType == DeviceType.EPD290 || storage.DeviceType == DeviceType.EPD290_lock)
                    {
                        this.storageUI_EPD_266.Set_Stroage_LED_UDP(storage, Color.Black);
                        storage.ActionDone = true;
                        if (plC_Button_同藥碼全亮.Bool) return;
                 
                        //List_EPD266_雲端資料.Add_NewStorage(storage);
                        this.Function_取藥堆疊子資料_設定配藥完成ByIP("None", IP, Num);
                    }
                    else if (storage.DeviceType == DeviceType.Pannel35 || storage.DeviceType == DeviceType.Pannel35_lock)
                    {
                        this.storageUI_WT32.Set_Stroage_LED_UDP(storage, Color.Black);
                        storage.ActionDone = true;
                        if (plC_Button_同藥碼全亮.Bool) return;
              
                        //List_Pannel35_雲端資料.Add_NewStorage(storage);
                        this.Function_取藥堆疊子資料_設定配藥完成ByIP("None", IP, Num);
                    }
           
                }
                else if (value_device is Drawer)
                {
                    Drawer drawer = value_device as Drawer;
                    List<Box> boxes = drawer.GetAllBoxes();
                    if (boxes.Count > 0)
                    {
                        if(boxes[0].DeviceType == DeviceType.EPD583 || boxes[0].DeviceType == DeviceType.EPD583_lock)
                        {
                            if(this.plC_CheckBox_關閉抽屜不滅燈.Checked == false)
                            {
                                drawer.LED_Bytes = DrawerUI_EPD_583.Get_Empty_LEDBytes();
                                drawer.ActionDone = true;
                                this.drawerUI_EPD_583.Set_LED_Clear_UDP(drawer);
                            }
                         
                            if (plC_Button_同藥碼全亮.Bool) return;
                         
                            //List_EPD583_雲端資料.Add_NewDrawer(drawer);
                            this.Function_取藥堆疊子資料_設定配藥完成ByIP("None", IP, Num);
                        }
                        if (boxes[0].DeviceType == DeviceType.EPD1020 || boxes[0].DeviceType == DeviceType.EPD1020_lock)
                        {
                            drawer.ActionDone = true;
                            this.drawerUI_EPD_1020.Set_LED_Clear_UDP(drawer);
                            if (plC_Button_同藥碼全亮.Bool) return;
                          
                            //this.List_EPD1020_雲端資料.Add_NewDrawer(drawer);
                            this.Function_取藥堆疊子資料_設定配藥完成ByIP("None", IP, Num);
                        }
                             
                    }
    
                  
                }
                else if (value_device is RFIDClass)
                {
                    RFIDClass rFIDClass = value_device as RFIDClass;
                    this.Function_取藥堆疊子資料_設定配藥完成ByIP("None", IP, Num);
                    this.List_RFID_雲端資料.Add_NewRFIDClass(rFIDClass);
                }

            }
        
        }
        private void Loker_LockOpeningEvent(object sender, PLC_Device PLC_Device_Input, PLC_Device PLC_Device_Output, string GUID)
        {
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            list_locker_table_value = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出位置, PLC_Device_Output.GetAdress());
            if (list_locker_table_value.Count == 0) return;
            string IP = list_locker_table_value[0][(int)enum_Locker_Index_Table.IP].ObjectToString();
            string Num = list_locker_table_value[0][(int)enum_Locker_Index_Table.Num].ObjectToString();
            Console.WriteLine($"{IP},{Num},<抽屜開啟> {DateTime.Now.ToDateTimeString()}");
            //if (plC_ScreenPage_Main.PageText == "管制抽屜")
            //{
            //    Pannel_Locker pannel_Locker = (Pannel_Locker)sender;
            //    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.管制抽屜開啟, this.登入者名稱, $"[{pannel_Locker.StorageName}]");
            //}
        }
        private bool Loker_LockAlarmEvent(object sender, PLC_Device PLC_Device_Input, PLC_Device PLC_Device_Output, string GUID)
        {
            if (this.plC_ScreenPage_Main.PageText != "調劑作業" && this.plC_ScreenPage_Main.PageText != "收支作業") return true;
            if (!plC_CheckBox_要檢查抽屜開啟異常.Checked) return true;
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            List<object[]> list_locker_table_value_replace = new List<object[]>();
            list_locker_table_value = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出位置, PLC_Device_Output.GetAdress());
            if (list_locker_table_value.Count == 0) return true;
            string IP = list_locker_table_value[0][(int)enum_Locker_Index_Table.IP].ObjectToString();
            string Num = list_locker_table_value[0][(int)enum_Locker_Index_Table.Num].ObjectToString();
            Console.WriteLine($"{IP},{Num},<抽屜開啟異常!!!> {DateTime.Now.ToDateTimeString()}");
            List<object[]> list_value = sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);

            list_value = list_value.GetRows((int)enum_取藥堆疊母資料.GUID, GUID);
            if (list_value.Count == 0) return true;
            if (list_value[0][(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.等待作業.GetEnumName()) return false;

            if (MyMessageBox.ShowDialog("抽屜開啟異常,是否強制入帳?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return true;
            Loker_LockClosingEvent(sender, PLC_Device_Input, PLC_Device_Output, GUID);
            return true;
            //MyMessageBox.ShowDialog("抽屜開啟異常,按下確認重新開啟一次!");    
            //list_locker_table_value[0][(int)enum_Locker_Index_Table.輸出狀態] = true.ToString();
            //list_locker_table_value_replace.Add(list_locker_table_value[0]);
            //if (list_locker_table_value_replace.Count != 0) this.sqL_DataGridView_Locker_Index_Table.SQL_ReplaceExtra(list_locker_table_value_replace, false);

        }
        #region Function
        private void Function_輸出入檢查_搜尋輸出(object[] value)
        {
            string IP = value[(int)enum_Locker_Index_Table.IP].ObjectToString();
            string 輸出位置 = value[(int)enum_Locker_Index_Table.輸出位置].ObjectToString();
            string 輸入位置 = value[(int)enum_Locker_Index_Table.輸入位置].ObjectToString();
            string 輸出狀態 = value[(int)enum_Locker_Index_Table.輸出狀態].ObjectToString();
            string 同步輸出 = value[(int)enum_Locker_Index_Table.同步輸出].ObjectToString();
            string Master_GUID = value[(int)enum_Locker_Index_Table.Master_GUID].ObjectToString();
            int Num = value[(int)enum_Locker_Index_Table.Num].ObjectToString().StringToInt32();
            this.Function_輸出入檢查_搜尋輸出(IP, Num, 輸入位置, 輸出位置, Master_GUID);//實體輸出
        }
        private void Function_輸出入檢查_搜尋輸出(string IP, int Num, string InputAdress, string OutputAdress, string Master_GUID)
        {
            //Master_GUID 為取藥堆疊母資料
            foreach (Pannel_Locker loker in this.List_Locker)
            {
                if (loker.Get_OutputAdress() == OutputAdress)
                {
                    if (loker.Input || true)
                    {
                        List<Task> taskList = new List<Task>();

                        taskList.Add(Task.Run(() =>
                        {
                            Drawer drawer = List_EPD583_雲端資料.SortByIP(IP);
                            if (drawer != null)
                            {
                                this.drawerUI_EPD_583.Set_LockOpen(drawer);
                            }
                        }));
                        taskList.Add(Task.Run(() =>
                        {
                            Drawer drawer = this.List_EPD1020_雲端資料.SortByIP(IP);
                            if (drawer != null)
                            {
                                this.drawerUI_EPD_1020.Set_LockOpen(drawer);
                            }
                        }));
                        taskList.Add(Task.Run(() =>
                        {
                            Storage storage = List_EPD266_雲端資料.SortByIP(IP);
                            if (storage != null)
                            {
                                this.storageUI_EPD_266.Set_LockOpen(storage);
                            }
                        }));
                        taskList.Add(Task.Run(() =>
                        {
                            Storage pannel35 = List_Pannel35_雲端資料.SortByIP(IP);
                            if (pannel35 != null)
                            {
                                this.storageUI_WT32.Set_LockOpen(pannel35);
                            }
                        }));
                        taskList.Add(Task.Run(() =>
                        {
                            RFIDClass rFIDClass = this.List_RFID_雲端資料.SortByIP(IP);
                            if (rFIDClass != null)
                            {
                                if (Num == -1) return;
                                this.rfiD_UI.Set_LockOpen(rFIDClass, Num);
                            }
                        }));
                        Task allTask = Task.WhenAll(taskList);
                        loker.Master_GUID = Master_GUID;
                        loker.Open();
                    }
                }
            }
        }
        private bool Function_輸出入檢查_檢查抽屜忙碌()
        {
            foreach (Pannel_Locker loker in this.List_Locker)
            {
                if (loker.IsBusy) return true;
            }
            return false;
        }
        #endregion
        private void sub_Program_輸出入檢查()
        {
            this.sub_Program_輸出入檢查_輸出刷新();
            //this.sub_Program_輸出入檢查_輸入刷新();
            if (PLC_Device_抽屜不鎖上.Bool)
            {
                for (int i = 0; i < List_Locker.Count; i++) List_Locker[i].Unlock = true;
            }
            else
            {
                for (int i = 0; i < List_Locker.Count; i++) List_Locker[i].Unlock = false;
            }
        }
        #region PLC_輸出入檢查_輸出刷新
        List<object[]> list_locker_table_value = new List<object[]>();
        bool flag_輸出入檢查_輸出刷新_全部輸出完成 = false;
        PLC_Device PLC_Device_輸出入檢查_輸出刷新 = new PLC_Device("");
        MyTimer MyTimer_輸出入檢查_輸出刷新 = new MyTimer("K50");
        int cnt_Program_輸出入檢查_輸出刷新 = 65534;
        bool flag_Program_輸出入檢查_輸出刷新_Init = false;
        void sub_Program_輸出入檢查_輸出刷新()
        {
            if (cnt_Program_輸出入檢查_輸出刷新 == 65534)
            {
                PLC_Device_輸出入檢查_輸出刷新.SetComment("PLC_輸出入檢查_輸出刷新");
                PLC_Device_輸出入檢查_輸出刷新.Bool = false;
                cnt_Program_輸出入檢查_輸出刷新 = 65535;
            }
            if (PLC_Device_主機輸出模式.Bool)
            {
                PLC_Device_輸出入檢查_輸出刷新.Bool = true;
                if (cnt_Program_輸出入檢查_輸出刷新 == 65535) cnt_Program_輸出入檢查_輸出刷新 = 1;
                if (cnt_Program_輸出入檢查_輸出刷新 == 1) cnt_Program_輸出入檢查_輸出刷新_檢查按下(ref cnt_Program_輸出入檢查_輸出刷新);
                if (cnt_Program_輸出入檢查_輸出刷新 == 2) cnt_Program_輸出入檢查_輸出刷新_初始化(ref cnt_Program_輸出入檢查_輸出刷新);
                if (cnt_Program_輸出入檢查_輸出刷新 == 3) cnt_Program_輸出入檢查_輸出刷新 = 100;

                if (cnt_Program_輸出入檢查_輸出刷新 == 100) cnt_Program_輸出入檢查_輸出刷新_100_檢查全部輸出完成(ref cnt_Program_輸出入檢查_輸出刷新);
                if (cnt_Program_輸出入檢查_輸出刷新 == 101) cnt_Program_輸出入檢查_輸出刷新_100_檢查輸入(ref cnt_Program_輸出入檢查_輸出刷新);
                if (cnt_Program_輸出入檢查_輸出刷新 == 102) cnt_Program_輸出入檢查_輸出刷新_100_檢查輸出時段(ref cnt_Program_輸出入檢查_輸出刷新);
                if (cnt_Program_輸出入檢查_輸出刷新 == 103) cnt_Program_輸出入檢查_輸出刷新_100_開始輸出(ref cnt_Program_輸出入檢查_輸出刷新);
                if (cnt_Program_輸出入檢查_輸出刷新 == 104) cnt_Program_輸出入檢查_輸出刷新 = 200;

                if (cnt_Program_輸出入檢查_輸出刷新 == 200) cnt_Program_輸出入檢查_輸出刷新_200_等待刷新延遲(ref cnt_Program_輸出入檢查_輸出刷新);
                if (cnt_Program_輸出入檢查_輸出刷新 == 201) cnt_Program_輸出入檢查_輸出刷新 = 65500;
                if (cnt_Program_輸出入檢查_輸出刷新 > 1) cnt_Program_輸出入檢查_輸出刷新_檢查放開(ref cnt_Program_輸出入檢查_輸出刷新);
            }
            else
            {
                cnt_Program_輸出入檢查_輸出刷新 = 65500;
            }

            if (cnt_Program_輸出入檢查_輸出刷新 == 65500)
            {
                PLC_Device_輸出入檢查_輸出刷新.Bool = false;
                cnt_Program_輸出入檢查_輸出刷新 = 65535;
            }
        }
        void cnt_Program_輸出入檢查_輸出刷新_檢查按下(ref int cnt)
        {
            if (PLC_Device_輸出入檢查_輸出刷新.Bool) cnt++;
        }
        void cnt_Program_輸出入檢查_輸出刷新_檢查放開(ref int cnt)
        {
            if (!PLC_Device_輸出入檢查_輸出刷新.Bool) cnt = 65500;
        }
        void cnt_Program_輸出入檢查_輸出刷新_初始化(ref int cnt)
        {
            this.flag_輸出入檢查_輸出刷新_全部輸出完成 = false;
            cnt++;
        }
        void cnt_Program_輸出入檢查_輸出刷新_100_檢查全部輸出完成(ref int cnt)
        {
            if (this.flag_輸出入檢查_輸出刷新_全部輸出完成)
            {
                this.MyTimer_輸出入檢查_輸出刷新.StartTickTime();
                cnt = 200;
            }
            else
            {
                list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
                cnt++;
            }
        }
        void cnt_Program_輸出入檢查_輸出刷新_100_檢查輸入(ref int cnt)
        {
            List<object[]> list_locker_table_value_buf = new List<object[]>();
            List<object[]> list_locker_table_value_replace = new List<object[]>();
            List<Pannel_Locker> lockers_buf = new List<Pannel_Locker>();
            list_locker_table_value_buf = list_locker_table_value;
            for (int i = 0; i < list_locker_table_value_buf.Count; i++)
            {
                string IP = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.IP].ObjectToString();
                string Input = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸入位置].ObjectToString();
                string OutPut = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸出位置].ObjectToString();
                bool Input_state = (list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸入狀態].ObjectToString().ToUpper() == "TRUE");
                bool flag_state = false;
                int Num = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.Num].ObjectToString().StringToInt32();
                bool AlarmEnable = false;
                if (Input.StringIsEmpty()) continue;
                Drawer drawer = List_EPD583_雲端資料.SortByIP(IP);
                if (drawer != null)
                {
                    flag_state = this.drawerUI_EPD_583.GetInput(drawer.IP);
                    this.PLC.properties.device_system.Set_Device(Input, flag_state);
                    //Console.WriteLine($"Input:{Input},{flag_state}");
                    AlarmEnable = drawer.AlarmEnable;
                }
                Drawer drawer_1020 = this.List_EPD1020_雲端資料.SortByIP(IP);
                if (drawer_1020 != null)
                {
                    flag_state = this.drawerUI_EPD_1020.GetInput(drawer_1020.IP);
                    this.PLC.properties.device_system.Set_Device(Input, flag_state);
               
                    AlarmEnable = drawer_1020.AlarmEnable;
                }
                Storage storage = List_EPD266_雲端資料.SortByIP(IP);
                if (storage != null)
                {
                    if(storage.DeviceType == DeviceType.EPD266_lock || storage.DeviceType == DeviceType.EPD290_lock)
                    {
                        flag_state = this.storageUI_EPD_266.GetInput(storage.IP);
                        this.PLC.properties.device_system.Set_Device(Input, flag_state);
           
                        AlarmEnable = storage.AlarmEnable;
                        if (storage.DeviceType != DeviceType.EPD266_lock || storage.DeviceType == DeviceType.EPD290_lock) AlarmEnable = false;
                    }
               
                }
                Storage pannel35 = List_Pannel35_雲端資料.SortByIP(IP);
                if (pannel35 != null)
                {
                    if(pannel35.DeviceType == DeviceType.Pannel35_lock)
                    {
                        flag_state = this.storageUI_WT32.GetInput(pannel35.IP);
                        this.PLC.properties.device_system.Set_Device(Input, flag_state);
    
                        AlarmEnable = pannel35.AlarmEnable;
                        if (pannel35.DeviceType != DeviceType.Pannel35_lock) AlarmEnable = false;
                    }
                   
                }
                RFIDClass rFIDClass = this.List_RFID_雲端資料.SortByIP(IP);
                if (rFIDClass != null)
                {
                    if (Num >= 0)
                    {
                        flag_state = this.rfiD_UI.GetInput(IP, Num);
                        this.PLC.properties.device_system.Set_Device(Input, flag_state);
                        if (flag_state != Input_state)
                        {
                            list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸入狀態] = flag_state.ToString();
                            list_locker_table_value_replace.Add(list_locker_table_value_buf[i]);
                        }
                        
                    }
                    AlarmEnable = true;
                }
                if (flag_state != Input_state)
                {
                    list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸入狀態] = flag_state.ToString();
                    list_locker_table_value_replace.Add(list_locker_table_value_buf[i]);
                }
                if (!flag_Program_輸出入檢查_輸出刷新_Init || true)
                {
                    lockers_buf = (from value in List_Locker
                                   where value.Get_InputAdress() == Input
                                   select value).ToList();
                    for (int k = 0; k < lockers_buf.Count; k++)
                    {
                        lockers_buf[k].AlarmEnable = AlarmEnable;
                    }
                }
            }
            //if (list_locker_table_value_replace.Count > 0) this.sqL_DataGridView_Locker_Index_Table.SQL_ReplaceExtra(list_locker_table_value_replace, false);
            flag_Program_輸出入檢查_輸出刷新_Init = true;
            cnt++;
        }
        void cnt_Program_輸出入檢查_輸出刷新_100_檢查輸出時段(ref int cnt)
        {
            for (int i = 0; i < List_RFID_雲端資料.Count; i++)
            {
                for (int k = 0; k < List_RFID_雲端資料[i].DeviceClasses.Length; k++)
                {
                    List<object[]> list_locker_table_value_buf = new List<object[]>();
                    list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.IP, List_RFID_雲端資料[i].IP);
                    list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.Num, k.ToString());
                    if (List_RFID_雲端資料[i].DeviceClasses[k].UnlockTimeEnable)
                    {
                        if (Basic.TypeConvert.IsInDate(new DateTime(1900, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, 0), List_RFID_雲端資料[i].DeviceClasses[k].Unlock_start_dateTime, List_RFID_雲端資料[i].DeviceClasses[k].Unlock_end_dateTime))
                        {
                            if (list_locker_table_value_buf.Count > 0)
                            {
                                string Input = list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.輸入位置].ObjectToString();
                                foreach (Pannel_Locker loker in this.List_Locker)
                                {
                                    if (loker.Get_InputAdress() == Input)
                                    {
                                        loker.AlarmEnable = false;
                                    }
                                }
                            }
                            bool input = this.rfiD_UI.GetInput(List_RFID_雲端資料[i].IP, k);
                            if (input)
                            {
                                this.rfiD_UI.Set_LockOpen(List_RFID_雲端資料[i], k);
                            }
                        }

                    }
                    else
                    {
                        if (list_locker_table_value_buf.Count > 0)
                        {
                            string Input = list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.輸入位置].ObjectToString();
                            foreach (Pannel_Locker loker in this.List_Locker)
                            {
                                if (loker.Get_InputAdress() == Input)
                                {
                                    loker.AlarmEnable = true;
                                }
                            }
                        }
                    }
                }
            }
            cnt++;
        }
        void cnt_Program_輸出入檢查_輸出刷新_100_開始輸出(ref int cnt)
        {
            List<object[]> list_locker_table_value_buf = new List<object[]>();
            List<object[]> list_locker_table_value_同步輸出_buf = new List<object[]>();
            list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出狀態, true.ToString());
            List<object[]> list_locker_table_value_ReplaceValue = new List<object[]>();
            this.flag_輸出入檢查_輸出刷新_全部輸出完成 = true;

            if (this.flag_輸出入檢查_輸出刷新_全部輸出完成)
            {
                list_locker_table_value_buf.Sort(new ICP_Locker_Index_Table());
                for (int i = 0; i < list_locker_table_value_buf.Count; i++)
                {
                    string IP = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.IP].ObjectToString();
                    string 輸出位置 = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸出位置].ObjectToString();
                    string 輸入位置 = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸入位置].ObjectToString();
                    string 輸出狀態 = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸出狀態].ObjectToString();
                    string 同步輸出 = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.同步輸出].ObjectToString();
                    string Master_GUID = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.Master_GUID].ObjectToString();
                    int Num = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.Num].ObjectToString().StringToInt32();
                    string Slave_GUID = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.Slave_GUID].ObjectToString();
                    string Device_GUID = list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.Device_GUID].ObjectToString();
                    string 調劑台名稱 = this.Function_取藥堆疊母資料_取得指定Master_GUID調劑台名稱(Master_GUID);

                    if (輸出狀態 == true.ToString())
                    {
                        this.flag_輸出入檢查_輸出刷新_全部輸出完成 = false;
                        list_locker_table_value_同步輸出_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出位置, 同步輸出);
                        if (輸出位置 != "") this.Function_輸出入檢查_搜尋輸出(IP, Num, 輸入位置, 輸出位置, Master_GUID);//實體輸出
                        if (list_locker_table_value_同步輸出_buf.Count > 0) this.Function_輸出入檢查_搜尋輸出(list_locker_table_value_同步輸出_buf[0]);//實體輸出

                        list_locker_table_value_buf[i][(int)enum_Locker_Index_Table.輸出狀態] = false.ToString();
                        this.Function_取藥堆疊子資料_設定流程作業完成ByIP("None", IP, Num.ToString());
                        list_locker_table_value_ReplaceValue.Add(list_locker_table_value_buf[i]);


                        if (Num != -1)
                        {
                            RFIDClass rFIDClass = List_RFID_雲端資料.SortByIP(IP);
                            if (rFIDClass != null)
                            {
                                RFIDClass.DeviceClass deviceClass = rFIDClass.DeviceClasses[Num];
                                if (deviceClass.UnlockTimeEnable)
                                {
                                    if (Basic.TypeConvert.IsInDate(new DateTime(1900, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, 0), deviceClass.Unlock_start_dateTime, deviceClass.Unlock_end_dateTime))
                                    {
                                        this.Function_取藥堆疊子資料_設定配藥完成ByIP("None", IP, Num.ToString());
                                    }
                                }
                            }
                        }

                        break;
                    }
                }
            }
            if (list_locker_table_value_ReplaceValue.Count > 0) this.sqL_DataGridView_Locker_Index_Table.SQL_ReplaceExtra(list_locker_table_value_ReplaceValue, false);
            this.MyTimer_輸出入檢查_輸出刷新.TickStop();
            this.MyTimer_輸出入檢查_輸出刷新.StartTickTime();
            cnt++;
        }

        void cnt_Program_輸出入檢查_輸出刷新_200_等待刷新延遲(ref int cnt)
        {
            if (this.MyTimer_輸出入檢查_輸出刷新.IsTimeOut())
            {
                cnt++;
            }
        }

        #endregion
        #region PLC_輸出入檢查_輸入刷新
        bool flag_輸出入檢查_輸入刷新_全部輸出完成 = false;
        PLC_Device PLC_Device_輸出入檢查_輸入刷新 = new PLC_Device("");
        MyTimer MyTimer_輸出入檢查_輸入刷新 = new MyTimer("K100");
        int cnt_Program_輸出入檢查_輸入刷新 = 65534;
        bool flag_Program_輸出入檢查_輸入刷新_Init = false;
        void sub_Program_輸出入檢查_輸入刷新()
        {
            if (cnt_Program_輸出入檢查_輸入刷新 == 65534)
            {
                PLC_Device_輸出入檢查_輸入刷新.SetComment("PLC_輸出入檢查_輸入刷新");
                PLC_Device_輸出入檢查_輸入刷新.Bool = false;
                cnt_Program_輸出入檢查_輸入刷新 = 65535;
            }
            if (!PLC_Device_主機輸出模式.Bool)
            {
                PLC_Device_輸出入檢查_輸入刷新.Bool = true;
                if (cnt_Program_輸出入檢查_輸入刷新 == 65535) cnt_Program_輸出入檢查_輸入刷新 = 1;
                if (cnt_Program_輸出入檢查_輸入刷新 == 1) cnt_Program_輸出入檢查_輸入刷新_檢查按下(ref cnt_Program_輸出入檢查_輸入刷新);
                if (cnt_Program_輸出入檢查_輸入刷新 == 2) cnt_Program_輸出入檢查_輸入刷新_初始化(ref cnt_Program_輸出入檢查_輸入刷新);
                if (cnt_Program_輸出入檢查_輸入刷新 == 3) cnt_Program_輸出入檢查_輸入刷新 = 100;

                if (cnt_Program_輸出入檢查_輸入刷新 == 100) cnt_Program_輸出入檢查_輸入刷新_100_檢查輸入(ref cnt_Program_輸出入檢查_輸入刷新);
                if (cnt_Program_輸出入檢查_輸入刷新 == 101) cnt_Program_輸出入檢查_輸入刷新 = 200;

                if (cnt_Program_輸出入檢查_輸入刷新 == 200) cnt_Program_輸出入檢查_輸入刷新_200_等待刷新延遲(ref cnt_Program_輸出入檢查_輸入刷新);
                if (cnt_Program_輸出入檢查_輸入刷新 == 201) cnt_Program_輸出入檢查_輸入刷新 = 65500;
                if (cnt_Program_輸出入檢查_輸入刷新 > 1) cnt_Program_輸出入檢查_輸入刷新_檢查放開(ref cnt_Program_輸出入檢查_輸入刷新);
            }
            else
            {
                cnt_Program_輸出入檢查_輸入刷新 = 65500;
            }

            if (cnt_Program_輸出入檢查_輸入刷新 == 65500)
            {
                PLC_Device_輸出入檢查_輸入刷新.Bool = false;
                cnt_Program_輸出入檢查_輸入刷新 = 65535;
            }
        }
        void cnt_Program_輸出入檢查_輸入刷新_檢查按下(ref int cnt)
        {
            if (PLC_Device_輸出入檢查_輸入刷新.Bool) cnt++;
        }
        void cnt_Program_輸出入檢查_輸入刷新_檢查放開(ref int cnt)
        {
            if (!PLC_Device_輸出入檢查_輸入刷新.Bool) cnt = 65500;
        }
        void cnt_Program_輸出入檢查_輸入刷新_初始化(ref int cnt)
        {
            this.flag_輸出入檢查_輸入刷新_全部輸出完成 = false;
            cnt++;
        }
        void cnt_Program_輸出入檢查_輸入刷新_100_檢查輸入(ref int cnt)
        {
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            List<object[]> list_locker_table_value_replace = new List<object[]>();
            List<Pannel_Locker> lockers_buf = new List<Pannel_Locker>();
            for (int i = 0; i < list_locker_table_value.Count; i++)
            {
                string Input = list_locker_table_value[i][(int)enum_Locker_Index_Table.輸入位置].ObjectToString();
                bool Input_state = (list_locker_table_value[i][(int)enum_Locker_Index_Table.輸入狀態].ObjectToString().ToUpper() == "TRUE");
                if (Input.StringIsEmpty()) continue;
                this.PLC.properties.device_system.Set_Device(Input, Input_state);

            }
            cnt++;
        }

        void cnt_Program_輸出入檢查_輸入刷新_200_等待刷新延遲(ref int cnt)
        {
            if (this.MyTimer_輸出入檢查_輸入刷新.IsTimeOut())
            {
                cnt++;
            }
        }

        #endregion
        #region PLC_輸出入檢查_蜂鳴器輸出
        PLC_Device PLC_Device_輸出入檢查_蜂鳴器輸出 = new PLC_Device("S5205");
        PLC_Device PLC_Device_輸出入檢查_蜂鳴器輸出_OK = new PLC_Device("S5206");
        PLC_Device PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴持續時間 = new PLC_Device("D110");
        PLC_Device PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴開始時間 = new PLC_Device("D115");
        PLC_Device PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴不使用 = new PLC_Device("S4060");
        List<object[]> list_輸出入檢查_蜂鳴器輸出_特殊輸出表 = new List<object[]>();
        MyTimer MyTimer_輸出入檢查_蜂鳴器輸出_蜂鳴時間 = new MyTimer();
        MyTimer MyTimer_輸出入檢查_蜂鳴器輸出_語音時間 = new MyTimer();
        string PLC_輸出入檢查_蜂鳴器輸出_IP = "";
        string PLC_輸出入檢查_蜂鳴器輸出_PINNum = "";
        object PLC_輸出入檢查_蜂鳴器輸出_輸出裝置;
        bool flag_輸出入檢查_蜂鳴器輸出 = false;
        int cnt_Program_輸出入檢查_蜂鳴器輸出 = 65534;
        void sub_Program_輸出入檢查_蜂鳴器輸出()
        {
            if (!PLC_Device_主機輸出模式.Bool)
            {
                PLC_Device_輸出入檢查_蜂鳴器輸出.Bool = false;
                return;
            }
            else
            {
                PLC_Device_輸出入檢查_蜂鳴器輸出.Bool = true;
            }

            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 65534)
            {
                list_輸出入檢查_蜂鳴器輸出_特殊輸出表 = this.sqL_DataGridView_特殊輸出表.SQL_GetAllRows(false);
                PLC_Device_輸出入檢查_蜂鳴器輸出.SetComment("PLC_輸出入檢查_蜂鳴器輸出");
                PLC_Device_輸出入檢查_蜂鳴器輸出_OK.SetComment("PLC_輸出入檢查_蜂鳴器輸出_OK");
                PLC_Device_輸出入檢查_蜂鳴器輸出.Bool = false;
                cnt_Program_輸出入檢查_蜂鳴器輸出 = 65535;
            }
            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 65535) cnt_Program_輸出入檢查_蜂鳴器輸出 = 1;
            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 1) cnt_Program_輸出入檢查_蜂鳴器輸出_檢查按下(ref cnt_Program_輸出入檢查_蜂鳴器輸出);
            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 2) cnt_Program_輸出入檢查_蜂鳴器輸出_初始化(ref cnt_Program_輸出入檢查_蜂鳴器輸出);
            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 3) cnt_Program_輸出入檢查_蜂鳴器輸出_尋找輸出位置(ref cnt_Program_輸出入檢查_蜂鳴器輸出);
            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 4) cnt_Program_輸出入檢查_蜂鳴器輸出_檢查抽屜異常(ref cnt_Program_輸出入檢查_蜂鳴器輸出);
            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 5) cnt_Program_輸出入檢查_蜂鳴器輸出 = 65500;
            if (cnt_Program_輸出入檢查_蜂鳴器輸出 > 1) cnt_Program_輸出入檢查_蜂鳴器輸出_檢查放開(ref cnt_Program_輸出入檢查_蜂鳴器輸出);

            if (cnt_Program_輸出入檢查_蜂鳴器輸出 == 65500)
            {
                PLC_Device_輸出入檢查_蜂鳴器輸出.Bool = false;
                PLC_Device_輸出入檢查_蜂鳴器輸出_OK.Bool = false;
                cnt_Program_輸出入檢查_蜂鳴器輸出 = 65535;
            }
        }
        void cnt_Program_輸出入檢查_蜂鳴器輸出_檢查按下(ref int cnt)
        {
            if (PLC_Device_輸出入檢查_蜂鳴器輸出.Bool) cnt++;
        }
        void cnt_Program_輸出入檢查_蜂鳴器輸出_檢查放開(ref int cnt)
        {
            if (!PLC_Device_輸出入檢查_蜂鳴器輸出.Bool) cnt = 65500;
        }
        void cnt_Program_輸出入檢查_蜂鳴器輸出_初始化(ref int cnt)
        {
        
            Pannel_Locker.AlarmTimeOut = PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴開始時間.Value;

      

            cnt++;
        }
        void cnt_Program_輸出入檢查_蜂鳴器輸出_尋找輸出位置(ref int cnt)
        {
       
            cnt++;
        }
        void cnt_Program_輸出入檢查_蜂鳴器輸出_檢查抽屜異常(ref int cnt)
        {
            bool flag_Alarm = true;

            for (int i = 0; i < List_Locker.Count; i++)
            {
                if (List_Locker[i].AlarmEnable)
                {
                    if (List_Locker[i].Alarm)
                    {
                        flag_Alarm = false;
                        break;
                    }
                }
            }

            if (PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴不使用.Bool)
            {
                MyTimer_輸出入檢查_蜂鳴器輸出_語音時間.TickStop();
                MyTimer_輸出入檢查_蜂鳴器輸出_語音時間.StartTickTime(1000);
            }
            if (!flag_Alarm && !PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴不使用.Bool)
            {
                if (MyTimer_輸出入檢查_蜂鳴器輸出_語音時間.IsTimeOut() == false || PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴持續時間.Value == 0)
                {
                    using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{currentDirectory}\alarm.wav"))
                    {
                        sp.Stop();
                        sp.Play();
                        sp.PlaySync();
                    }

                }
            }
            else
            {
                MyTimer_輸出入檢查_蜂鳴器輸出_語音時間.TickStop();
                MyTimer_輸出入檢查_蜂鳴器輸出_語音時間.StartTickTime(PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴持續時間.Value);
            }


            List<object[]> list_value = this.list_輸出入檢查_蜂鳴器輸出_特殊輸出表;
            if (list_value.Count == 0)
            {
                cnt = 65500;
                return;
            }
            this.PLC_輸出入檢查_蜂鳴器輸出_IP = list_value[0][(int)enum_特殊輸出表.IP].ObjectToString();
            this.PLC_輸出入檢查_蜂鳴器輸出_PINNum = list_value[0][(int)enum_特殊輸出表.Num].ObjectToString();

            PLC_輸出入檢查_蜂鳴器輸出_輸出裝置 = List_RFID_本地資料.SortByIP(PLC_輸出入檢查_蜂鳴器輸出_IP);
            if (PLC_輸出入檢查_蜂鳴器輸出_輸出裝置 == null)
            {
                cnt = 65500;
                return;
            }

            if (PLC_輸出入檢查_蜂鳴器輸出_輸出裝置 == null)
            {
                cnt = 65500;
                return;
            }
            string IP = this.PLC_輸出入檢查_蜂鳴器輸出_IP;
            int PINNum = this.PLC_輸出入檢查_蜂鳴器輸出_PINNum.StringToInt32();
            if (PLC_輸出入檢查_蜂鳴器輸出_輸出裝置 is RFIDClass)
            {
                RFIDClass rFIDClass = PLC_輸出入檢查_蜂鳴器輸出_輸出裝置 as RFIDClass;
                int num = this.PLC_輸出入檢查_蜂鳴器輸出_PINNum.StringToInt32() - 1;
                if (num < 0)
                {
                    return;
                }
                this.flag_輸出入檢查_蜂鳴器輸出 = this.rfiD_UI.GetOutput(rFIDClass.IP, num);
                if (PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴不使用.Bool)
                {
                    MyTimer_輸出入檢查_蜂鳴器輸出_蜂鳴時間.TickStop();
                    MyTimer_輸出入檢查_蜂鳴器輸出_蜂鳴時間.StartTickTime(PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴持續時間.Value);
                    if (flag_輸出入檢查_蜂鳴器輸出) this.rfiD_UI.Set_OutputPIN(rFIDClass.IP, rFIDClass.Port, PINNum, false);
                    cnt++;
                    return;
                }
                if (!flag_Alarm)
                {
                    if (!MyTimer_輸出入檢查_蜂鳴器輸出_蜂鳴時間.IsTimeOut() || (PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴持續時間.Value == 0))
                    {
                        if (!flag_輸出入檢查_蜂鳴器輸出) this.rfiD_UI.Set_OutputPIN(rFIDClass.IP, rFIDClass.Port, PINNum, true);
                    }
                    else
                    {
                        this.rfiD_UI.Set_OutputPIN(rFIDClass.IP, rFIDClass.Port, PINNum, false);
                    }
                }
                else
                {
                    MyTimer_輸出入檢查_蜂鳴器輸出_蜂鳴時間.TickStop();
                    MyTimer_輸出入檢查_蜂鳴器輸出_蜂鳴時間.StartTickTime(PLC_Device_輸出入檢查_蜂鳴器輸出_蜂鳴持續時間.Value);
                    if (flag_輸出入檢查_蜂鳴器輸出) this.rfiD_UI.Set_OutputPIN(rFIDClass.IP, rFIDClass.Port, PINNum, false);
                }

            }


            cnt++;
        }



        #endregion


    }
}
