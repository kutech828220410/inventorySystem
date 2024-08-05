using System;
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
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using H_Pannel_lib;
using MyOffice;
using HIS_DB_Lib;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        static public List<Drawer> List_EPD1020_本地資料 = new List<Drawer>();
        static public List<Drawer> List_EPD1020_雲端資料 = new List<Drawer>();
        static public List<Drawer> List_EPD1020_入賬資料 = new List<Drawer>();
        static public DrawerUI _drawerUI_EPD_1020;
        [EnumDescription("")]
        private enum enum_儲位管理_EPD1020_效期及庫存
        {
            [Description("效期,VARCHAR,300,NONE")]
            效期,
            [Description("批號,VARCHAR,300,NONE")]
            批號,
            [Description("庫存,VARCHAR,300,NONE")]
            庫存,
        }
        [EnumDescription("")]
        private enum enum_儲位管理_EPD1020_抽屜列表
        {
            [Description("IP,VARCHAR,300,NONE")]
            IP,
            [Description("名稱,VARCHAR,300,NONE")]
            名稱,
        }
        [EnumDescription("")]
        private enum enum_儲位管理_EPD1020_儲位資料
        {
            [Description("GUID,VARCHAR,300,NONE")]
            GUID,
            [Description("藥品碼,VARCHAR,300,NONE")]
            藥品碼,
            [Description("藥品名稱,VARCHAR,300,NONE")]
            藥品名稱,
            [Description("庫存,VARCHAR,300,NONE")]
            庫存,
        }
        private bool flag_Program_儲位管理_EPD1020_Init = false;
        private void Program_儲位管理_EPD1020_Init()
        {

            this.drawerUI_EPD_1020.DrawerChangeEvent += DrawerUI_EPD_1020_DrawerChangeEvent;

            SQLUI.Table table = new SQLUI.Table(new enum_儲位管理_EPD1020_效期及庫存());
            this.sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.Init(table);
            this.sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.Set_ColumnVisible(false, new enum_儲位管理_EPD1020_效期及庫存().GetEnumNames());
            this.sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_效期及庫存.效期);
            this.sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_效期及庫存.批號);
            this.sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_效期及庫存.庫存);

            table = new SQLUI.Table(new enum_儲位管理_EPD1020_儲位資料());
            this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Init(table);
            this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Set_ColumnVisible(false, new enum_儲位管理_EPD1020_儲位資料().GetEnumNames());
            this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_儲位資料.藥品碼);
            this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_儲位資料.藥品名稱);
            this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_儲位資料.庫存);

            table = new SQLUI.Table(new enum_儲位管理_EPD1020_抽屜列表());
            this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Init(table);
            this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Set_ColumnVisible(false, new enum_儲位管理_EPD1020_抽屜列表().GetEnumNames());
            this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_抽屜列表.IP);
            this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_儲位管理_EPD1020_抽屜列表.名稱);


            this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.RowEnterEvent += SqL_DataGridView_儲位管理_EPD1020_儲位資料_RowEnterEvent;

            this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.RowEnterEvent += SqL_DataGridView_儲位管理_EPD1020_抽屜列表_RowEnterEvent;

            this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
            this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
            this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.Set_ColumnVisible(true, enum_藥品資料_藥檔資料.藥品碼, enum_藥品資料_藥檔資料.藥品名稱);
            this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.RowDoubleClickEvent += SqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料_RowDoubleClickEvent;

            this.rJ_TextBox_儲位管理_EPD1020_抽屜列表_儲位名稱.KeyPress += RJ_TextBox_儲位管理_EPD1020_抽屜列表_儲位名稱_KeyPress;
            this.rJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品碼.KeyPress += RJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品碼_KeyPress;
            this.rJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品名稱.KeyPress += RJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品名稱_KeyPress;
            this.rJ_TextBox_儲位管理_EPD1020_儲位內容_儲位名稱.KeyPress += RJ_TextBox_儲位管理_EPD1020_儲位內容_儲位名稱_KeyPress;
            this.rJ_TextBox_儲位管理_EPD1020_儲位內容_儲位搜尋_藥品碼.KeyPress += RJ_TextBox_儲位管理_EPD1020_儲位內容_儲位搜尋_藥品碼_KeyPress;
            this.rJ_TextBox_儲位管理_EPD1020_抽屜列表_語音.KeyPress += RJ_TextBox_儲位管理_EPD1020_抽屜列表_語音_KeyPress;

            this.plC_RJ_Button_儲位管理_EPD1020_面板亮燈.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_面板亮燈_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_寫入.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_寫入_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_清除燈號.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_清除燈號_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_上傳至面板.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_上傳至面板_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品碼_搜尋.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品碼_搜尋_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品名稱_搜尋.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品名稱_搜尋_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_藥品搜尋_填入資料.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_填入資料_MouseDownEvent;

            this.plC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_新增效期.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_新增效期_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_修正庫存.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_修正庫存_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_修正批號.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_修正批號_MouseDownEvent;
  

            this.plC_RJ_Button_儲位管理_EPD1020_儲位內容_儲位搜尋_藥品碼_搜尋.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_儲位內容_藥品碼_搜尋_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_開鎖.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_開鎖_MouseDownEvent;
           

            this.plC_RJ_Button_儲位管理_EPD1020_儲位資料_新增儲位.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_儲位資料_新增儲位_MouseDownEvent;
            this.plC_RJ_Button_儲位管理_EPD1020_儲位資料_刪除儲位.MouseDownEvent += PlC_RJ_Button_儲位管理_EPD1020_儲位資料_刪除儲位_MouseDownEvent;
            this.plC_CheckBox_儲位管理_EPD1020_警報.CheckStateChanged += PlC_CheckBox_儲位管理_EPD1020_警報_CheckStateChanged;

            this.epD_1020_Pannel.Init(this.drawerUI_EPD_1020.List_UDP_Local);
            this.plC_UI_Init.Add_Method(this.Program_儲位管理_EPD1020);
        }

  

        private void Program_儲位管理_EPD1020()
        {
            if (this.plC_ScreenPage_Main.PageText == "儲位管理" && this.plC_ScreenPage_儲位管理.PageText == "EPD1020")
            {
                if (flag_Program_儲位管理_EPD1020_Init == false)
                {
                    PLC_Device_儲位管理_EPD1020_資料更新.Bool = true;
                    sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();
                    sqL_DataGridView_儲位管理_EPD1020_儲位資料.On_RowEnter();

                    flag_Program_儲位管理_EPD1020_Init = true;
                }
            }
            else
            {
                flag_Program_儲位管理_EPD1020_Init = false;
            }

            sub_Program_儲位管理_EPD1020_資料更新();
        }

        #region PLC_儲位管理_EPD1020_資料更新
        PLC_Device PLC_Device_儲位管理_EPD1020_資料更新 = new PLC_Device("S9005");
        int cnt_Program_儲位管理_EPD1020_資料更新 = 65534;
        void sub_Program_儲位管理_EPD1020_資料更新()
        {
            if (cnt_Program_儲位管理_EPD1020_資料更新 == 65534)
            {
                PLC_Device_儲位管理_EPD1020_資料更新.SetComment("PLC_儲位管理_EPD1020_資料更新");
                PLC_Device_儲位管理_EPD1020_資料更新.Bool = false;
                cnt_Program_儲位管理_EPD1020_資料更新 = 65535;
            }
            if (cnt_Program_儲位管理_EPD1020_資料更新 == 65535) cnt_Program_儲位管理_EPD1020_資料更新 = 1;
            if (cnt_Program_儲位管理_EPD1020_資料更新 == 1) cnt_Program_儲位管理_EPD1020_資料更新_檢查按下(ref cnt_Program_儲位管理_EPD1020_資料更新);
            if (cnt_Program_儲位管理_EPD1020_資料更新 == 2) cnt_Program_儲位管理_EPD1020_資料更新_初始化(ref cnt_Program_儲位管理_EPD1020_資料更新);
            if (cnt_Program_儲位管理_EPD1020_資料更新 == 3) cnt_Program_儲位管理_EPD1020_資料更新_更新面板資料(ref cnt_Program_儲位管理_EPD1020_資料更新);
            if (cnt_Program_儲位管理_EPD1020_資料更新 == 4) cnt_Program_儲位管理_EPD1020_資料更新_更新藥檔(ref cnt_Program_儲位管理_EPD1020_資料更新);
            if (cnt_Program_儲位管理_EPD1020_資料更新 == 5) cnt_Program_儲位管理_EPD1020_資料更新 = 65500;
            if (cnt_Program_儲位管理_EPD1020_資料更新 > 1) cnt_Program_儲位管理_EPD1020_資料更新_檢查放開(ref cnt_Program_儲位管理_EPD1020_資料更新);

            if (cnt_Program_儲位管理_EPD1020_資料更新 == 65500)
            {
                PLC_Device_儲位管理_EPD1020_資料更新.Bool = false;
                cnt_Program_儲位管理_EPD1020_資料更新 = 65535;
            }
        }
        void cnt_Program_儲位管理_EPD1020_資料更新_檢查按下(ref int cnt)
        {
            if (PLC_Device_儲位管理_EPD1020_資料更新.Bool) cnt++;
        }
        void cnt_Program_儲位管理_EPD1020_資料更新_檢查放開(ref int cnt)
        {
            if (!PLC_Device_儲位管理_EPD1020_資料更新.Bool) cnt = 65500;
        }
        void cnt_Program_儲位管理_EPD1020_資料更新_初始化(ref int cnt)
        {
            MyTimer_TickTime.TickStop();
            MyTimer_TickTime.StartTickTime(50000);
            List_EPD1020_本地資料 = this.drawerUI_EPD_1020.SQL_GetAllDrawers();
            Console.Write($"儲位管理EPD1020:從SQL取得資料 ,耗時 :{MyTimer_TickTime.GetTickTime().ToString("0.000")}\n");
            cnt++;
        }
        void cnt_Program_儲位管理_EPD1020_資料更新_更新面板資料(ref int cnt)
        {
            MyTimer_TickTime.TickStop();
            MyTimer_TickTime.StartTickTime(50000);
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < List_EPD1020_本地資料.Count; i++)
            {
                object[] value = new object[new enum_儲位管理_EPD1020_抽屜列表().GetLength()];
                value[(int)enum_儲位管理_EPD1020_抽屜列表.IP] = List_EPD1020_本地資料[i].IP;
                value[(int)enum_儲位管理_EPD1020_抽屜列表.名稱] = List_EPD1020_本地資料[i].Name;
                list_value.Add(value);
            }
            list_value.Sort(new ICP_儲位管理_EPD1020_抽屜列表());
            this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.RefreshGrid(list_value);
            Console.Write($"儲位管理EPD1020:更新抽屜列表完成 ,耗時 :{MyTimer_TickTime.GetTickTime().ToString("0.000")}\n");
            cnt++;
        }
        void cnt_Program_儲位管理_EPD1020_資料更新_更新藥檔(ref int cnt)
        {
            MyTimer_TickTime.TickStop();
            MyTimer_TickTime.StartTickTime(50000);

            List<object[]> list_藥品資料_藥檔資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_藥品資料_藥檔資料_buf = new List<object[]>();
            List<Drawer> list_replaceValue = new List<Drawer>();
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 藥品學名 = "";
            string BarCode = "";
            string 包裝單位 = "";
            string 警訊藥品 = "";


            string 藥品碼_buf = "";
            string 藥品名稱_buf = "";
            string 藥品學名_buf = "";
            string BarCode_buf = "";
            string 包裝單位_buf = "";
            string 警訊藥品_buf = "";
            for (int i = 0; i < List_EPD1020_本地資料.Count; i++)
            {
                string IP = List_EPD1020_本地資料[i].IP;
                List<Box> boxes = List_EPD1020_本地資料[i].GetAllBoxes();
                bool Is_Replace = false;
                for (int k = 0; k < boxes.Count; k++)
                {
                    藥品碼 = boxes[k].GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                    if (藥品碼.StringIsEmpty()) continue;
                    list_藥品資料_藥檔資料_buf = list_藥品資料_藥檔資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
                    if (list_藥品資料_藥檔資料_buf.Count == 0)
                    {
                        boxes[k].Clear();
                        Is_Replace = true;
                    }
                    else
                    {
                        藥品碼_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                        藥品名稱_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                        藥品學名_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品學名].ObjectToString();
                        BarCode_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品條碼].ObjectToString();
                        包裝單位_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                        警訊藥品_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString().ToUpper();

                        藥品碼 = boxes[k].GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                        藥品名稱 = boxes[k].GetValue(Device.ValueName.藥品名稱, Device.ValueType.Value).ObjectToString();
                        藥品學名 = boxes[k].GetValue(Device.ValueName.藥品學名, Device.ValueType.Value).ObjectToString();
                        BarCode = boxes[k].GetValue(Device.ValueName.BarCode, Device.ValueType.Value).ObjectToString();
                        包裝單位 = boxes[k].GetValue(Device.ValueName.包裝單位, Device.ValueType.Value).ObjectToString();
                        警訊藥品 = boxes[k].IsWarning ? "TRUE" : "FALSE";


                        if (藥品碼 != 藥品碼_buf) Is_Replace = true;
                        if (藥品名稱 != 藥品名稱_buf) Is_Replace = true;
                        if (藥品學名 != 藥品學名_buf) Is_Replace = true;
                        if (BarCode != BarCode_buf) Is_Replace = true;
                        if (包裝單位 != 包裝單位_buf) Is_Replace = true;
                        if (警訊藥品 != 警訊藥品_buf) Is_Replace = true;

                        boxes[k].SetValue(Device.ValueName.藥品碼, Device.ValueType.Value, 藥品碼_buf);
                        boxes[k].SetValue(Device.ValueName.藥品名稱, Device.ValueType.Value, 藥品名稱_buf);
                        boxes[k].SetValue(Device.ValueName.藥品學名, Device.ValueType.Value, 藥品學名_buf);
                        boxes[k].SetValue(Device.ValueName.BarCode, Device.ValueType.Value, BarCode_buf);
                        boxes[k].SetValue(Device.ValueName.包裝單位, Device.ValueType.Value, 包裝單位_buf);
                        boxes[k].IsWarning = (警訊藥品_buf == "TRUE");

                    }
                }
                if (Is_Replace)
                {
                    list_replaceValue.Add(List_EPD1020_本地資料[i]);
                }
            }

            this.drawerUI_EPD_1020.SQL_ReplaceDrawer(list_replaceValue);
            Console.Write($"儲位管理EPD1020:更新藥檔完成 ,耗時 :{MyTimer_TickTime.GetTickTime().ToString("0.000")}\n");
            cnt++;
        }
        #endregion
        #region Event
        private void DrawerUI_EPD_1020_DrawerChangeEvent(List<Drawer> drawers)
        {
            //for (int i = 0; i < drawers.Count; i++)
            //{
            //    List_EPD1020_本地資料.Add_NewDrawer(drawers[i]);
            //}

        }
        private void SqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料_RowDoubleClickEvent(object[] RowValue)
        {

        }
        private void SqL_DataGridView_儲位管理_EPD1020_抽屜列表_RowEnterEvent(object[] RowValue)
        {
            string IP = RowValue[(int)enum_儲位管理_EPD1020_抽屜列表.IP].ObjectToString();
            string 儲位名稱 = RowValue[(int)enum_儲位管理_EPD1020_抽屜列表.名稱].ObjectToString();

            rJ_TextBox_儲位管理_EPD1020_抽屜列表_IP.Texts = IP;
            rJ_TextBox_儲位管理_EPD1020_抽屜列表_儲位名稱.Texts = 儲位名稱;
            Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
            rJ_TextBox_儲位管理_EPD1020_抽屜列表_語音.Texts = drawer.Speaker;
            if (drawer != null)
            {
                this.epD_1020_Pannel.CurrentDrawer = drawer;
                this.epD_1020_Pannel.DrawToPictureBox(this.epD_1020_Pannel.CurrentDrawer);
                List<Box> boxes = drawer.GetAllBoxes();
                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < boxes.Count; i++)
                {
                    object[] value = new object[new enum_儲位管理_EPD1020_儲位資料().GetLength()];
                    value[(int)enum_儲位管理_EPD1020_儲位資料.GUID] = boxes[i].GUID;
                    value[(int)enum_儲位管理_EPD1020_儲位資料.藥品碼] = boxes[i].Code;
                    value[(int)enum_儲位管理_EPD1020_儲位資料.藥品名稱] = boxes[i].Name;
                    value[(int)enum_儲位管理_EPD1020_儲位資料.庫存] = boxes[i].Inventory;
                    list_value.Add(value);
                }
                this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.RefreshGrid(list_value);
            }

        }
        private void SqL_DataGridView_儲位管理_EPD1020_儲位資料_RowEnterEvent(object[] RowValue)
        {
            string GUID = RowValue[(int)enum_儲位管理_EPD1020_儲位資料.GUID].ObjectToString();
            if (epD_1020_Pannel.CurrentDrawer == null) return;
            Drawer drawer = epD_1020_Pannel.CurrentDrawer;
            Box box = drawer.GetByGUID(GUID);
            if (box == null) return;
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_儲位管理_EPD1020_儲位內容_藥品名稱.Text = box.GetValue(Device.ValueName.藥品名稱, Device.ValueType.Value).ObjectToString();
                rJ_TextBox_儲位管理_EPD1020_儲位內容_藥品學名.Text = box.GetValue(Device.ValueName.藥品學名, Device.ValueType.Value).ObjectToString();
                rJ_TextBox_儲位管理_EPD1020_儲位內容_中文名稱.Text = box.GetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Value).ObjectToString();
                rJ_TextBox_儲位管理_EPD1020_儲位內容_藥品碼.Text = box.GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                rJ_TextBox_儲位管理_EPD1020_儲位內容_藥品條碼.Text = box.GetValue(Device.ValueName.BarCode, Device.ValueType.Value).ObjectToString();
                rJ_TextBox_儲位管理_EPD1020_儲位內容_包裝單位.Text = box.GetValue(Device.ValueName.包裝單位, Device.ValueType.Value).ObjectToString();
                rJ_TextBox_儲位管理_EPD1020_儲位內容_儲位名稱.Text = box.GetValue(Device.ValueName.儲位名稱, Device.ValueType.Value).ObjectToString();
                rJ_TextBox_儲位管理_EPD1020_儲位內容_總庫存.Text = box.GetValue(Device.ValueName.庫存, Device.ValueType.Value).ObjectToString();
            }));

            sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.ClearGrid();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < box.List_Validity_period.Count; i++)
            {
                object[] value = new object[new enum_儲位管理_EPD1020_效期及庫存().GetLength()];
                value[(int)enum_儲位管理_EPD1020_效期及庫存.效期] = box.List_Validity_period[i];
                value[(int)enum_儲位管理_EPD1020_效期及庫存.批號] = box.List_Lot_number[i];
                value[(int)enum_儲位管理_EPD1020_效期及庫存.庫存] = box.List_Inventory[i];
                list_value.Add(value);
            }

            sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.RefreshGrid(list_value);
        }
    
   
        private void PlC_CheckBox_儲位管理_EPD1020_隔板亮燈_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                string IP = rJ_TextBox_儲位管理_EPD1020_抽屜列表_IP.Texts;
                Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
                if (drawer != null)
                {
                    this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                    List_EPD1020_本地資料.Add_NewDrawer(drawer);
                    this.epD_1020_Pannel.CurrentDrawer = drawer;
                    this.Function_設定雲端資料更新();
                }
            }));
        }
  
        private void RJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品名稱_搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品碼_搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_儲位管理_EPD1020_抽屜列表_儲位名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string IP = rJ_TextBox_儲位管理_EPD1020_抽屜列表_IP.Texts;
                Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
                if (drawer != null)
                {
                    drawer.Name = rJ_TextBox_儲位管理_EPD1020_抽屜列表_儲位名稱.Text;
                    this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                    sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Replace(new object[] { drawer.IP, drawer.Name }, true);
                }
            }
        }
        private void RJ_TextBox_儲位管理_EPD1020_儲位內容_儲位名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Drawer drawer = this.epD_1020_Pannel.CurrentDrawer;
                if (drawer == null) return;
                List<object[]> list_儲位資料 = this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Get_All_Select_RowsValues();
                if (list_儲位資料.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選取儲位!");
                    return;
                }
                string box_GUID = list_儲位資料[0][(int)enum_儲位管理_EPD1020_儲位資料.GUID].ObjectToString();
                Box box = drawer.GetByGUID(box_GUID);
                if (box == null) return;
                box.SetValue(Device.ValueName.儲位名稱, Device.ValueType.Value, rJ_TextBox_儲位管理_EPD1020_儲位內容_儲位名稱.Text);
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(epD_1020_Pannel.CurrentDrawer);
                List_EPD1020_本地資料.Add_NewDrawer(epD_1020_Pannel.CurrentDrawer);
            }
        }
        private void RJ_TextBox_儲位管理_EPD1020_儲位內容_儲位搜尋_藥品碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_儲位管理_EPD1020_儲位內容_藥品碼_搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_儲位管理_EPD1020_抽屜列表_語音_KeyPress(object sender, KeyPressEventArgs e)
        {
            string IP = rJ_TextBox_儲位管理_EPD1020_抽屜列表_IP.Texts;
            Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
            if (drawer != null)
            {
                drawer.Speaker = rJ_TextBox_儲位管理_EPD1020_抽屜列表_語音.Text;
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                List_EPD1020_本地資料.Add_NewDrawer(drawer);
                sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Replace(new object[] { drawer.IP, drawer.Name }, true);
                sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();
                this.Function_設定雲端資料更新();
            }
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_面板亮燈_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            Color color = Color.Black;
            if (this.radioButton_儲位管理_EPD1020_面板亮燈_白.Checked)
            {
                color = Color.White;
            }
            else if (this.radioButton_儲位管理_EPD1020_面板亮燈_紅.Checked)
            {
                color = Color.Red;
            }
            else if (this.radioButton_儲位管理_EPD1020_面板亮燈_藍.Checked)
            {
                color = Color.Blue;
            }
            else if (this.radioButton_儲位管理_EPD1020_面板亮燈_綠.Checked)
            {
                color = Color.Green;
            }
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string IP = list_value[i][(int)enum_儲位管理_EPD1020_抽屜列表.IP].ObjectToString();
                Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
                taskList.Add(Task.Run(() =>
                {

                    if (drawer != null)
                    {
                        if (!this.drawerUI_EPD_1020.Set_Pannel_LED_UDP(drawer, color))
                        {
                            MyMessageBox.ShowDialog($"{drawer.IP}:{drawer.Port} : EPD 抽屜面板亮燈失敗!");
                        }
                        Console.WriteLine($"{drawer.IP}:{drawer.Port} : EPD 抽屜亮燈成功!");
                    }
                }));
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();


        }
        private void PlC_RJ_Button_儲位管理_EPD1020_清除燈號_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string IP = list_value[i][(int)enum_儲位管理_EPD1020_抽屜列表.IP].ObjectToString();
                Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
                taskList.Add(Task.Run(() =>
                {

                    if (drawer != null)
                    {
                        if (!this.drawerUI_EPD_1020.Set_LED_Clear_UDP(drawer))
                        {
                            MyMessageBox.ShowDialog($"{drawer.IP}:{drawer.Port} : EPD 抽屜滅燈失敗!");
                        }
                        Console.WriteLine($"{drawer.IP}:{drawer.Port} : EPD 抽屜成功!");
                    }
                }));
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_上傳至面板_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string IP = list_value[i][(int)enum_儲位管理_EPD1020_抽屜列表.IP].ObjectToString();
                Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
                taskList.Add(Task.Run(() =>
                {
                    if (drawer != null)
                    {
                        if (!this.drawerUI_EPD_1020.DrawToEpd_UDP(drawer))
                        {
                            MyMessageBox.ShowDialog($"{drawer.IP}:{drawer.Port} : EPD 抽屜上傳失敗!");
                        }
                        Console.WriteLine($"{drawer.IP}:{drawer.Port} : EPD 抽屜上傳成功!");
                    }
                }));
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();


        }
        private void PlC_RJ_Button_儲位管理_EPD1020_寫入_MouseDownEvent(MouseEventArgs mevent)
        {
            string IP = rJ_TextBox_儲位管理_EPD1020_抽屜列表_IP.Texts;
            Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
            if (drawer != null)
            {
                drawer.Name = rJ_TextBox_儲位管理_EPD1020_抽屜列表_儲位名稱.Text;
                drawer.Speaker = rJ_TextBox_儲位管理_EPD1020_抽屜列表_語音.Text;
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                List_EPD1020_本地資料.Add_NewDrawer(drawer);
                sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Replace(new object[] { drawer.IP, drawer.Name }, true);
                sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();
                this.Function_設定雲端資料更新();
            }
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品碼.Text.StringIsEmpty()) return;
            List<object[]> list_value = this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.SQL_GetAllRows(false);
            list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品碼, rJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品碼.Text);
            this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_藥品名稱_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品名稱.Text.Length < 3)
            {
                MyMessageBox.ShowDialog("藥品名稱搜尋字元不得小於3個!");
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.SQL_GetAllRows(false);
            if (rJ_RatioButton_儲位管理_EPD1020_藥品搜尋_前綴.Checked)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.藥品名稱, rJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品名稱.Text);
            }
            else if (rJ_RatioButton_儲位管理_EPD1020_藥品搜尋_模糊.Checked)
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品名稱, rJ_TextBox_儲位管理_EPD1020_藥品搜尋_藥品名稱.Text , true);
            }


            this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_藥品搜尋_填入資料_MouseDownEvent(MouseEventArgs mevent)
        {
            object[] value = this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.GetRowValues();
            if (value == null) return;
            Drawer drawer = this.epD_1020_Pannel.CurrentDrawer;
            if (drawer == null) return;
            List<object[]> list_儲位資料 = this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Get_All_Select_RowsValues();
            if (list_儲位資料.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲位!");
                return;
            }
            string GUID = list_儲位資料[0][(int)enum_儲位管理_EPD1020_儲位資料.GUID].ObjectToString();
            Box box = drawer.GetBox(GUID);
            if (box == null) return;

            box.Clear();
            box.SetValue(Device.ValueName.藥品碼, Device.ValueType.Value, value[(int)enum_藥品資料_藥檔資料.藥品碼]);
            box.SetValue(Device.ValueName.藥品名稱, Device.ValueType.Value, value[(int)enum_藥品資料_藥檔資料.藥品名稱]);
            box.SetValue(Device.ValueName.藥品學名, Device.ValueType.Value, value[(int)enum_藥品資料_藥檔資料.藥品學名]);
            box.SetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Value, value[(int)enum_藥品資料_藥檔資料.中文名稱]);
            box.SetValue(Device.ValueName.包裝單位, Device.ValueType.Value, value[(int)enum_藥品資料_藥檔資料.包裝單位]);
            box.SetValue(Device.ValueName.BarCode, Device.ValueType.Value, value[(int)enum_藥品資料_藥檔資料.藥品條碼]);

            if (value[(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString().ToUpper() == true.ToString().ToUpper())
            {
                box.BackColor = Color.Red;
                box.ForeColor = Color.White;
            }
            else
            {
                box.BackColor = Color.White;
                box.ForeColor = Color.Black;
            }
            drawer.ReplaceByGUID(box);

            this.epD_1020_Pannel.DrawToPictureBox(drawer);
            this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
            List_EPD1020_本地資料.Add_NewDrawer(drawer);
            this.Function_設定雲端資料更新();
            this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();
        }

        private void PlC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_新增效期_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Drawer drawer = this.epD_1020_Pannel.CurrentDrawer;
                if (drawer == null) return;
                List<object[]> list_儲位資料 = this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Get_All_Select_RowsValues();
                if (list_儲位資料.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選取儲位!");
                    return;
                }
                string box_GUID = list_儲位資料[0][(int)enum_儲位管理_EPD1020_儲位資料.GUID].ObjectToString();
                Box box = drawer.GetBox(box_GUID);
                if (box == null) return;

                string 效期 = "";
                string 批號 = "";
                string 數量 = "";
                Dialog_DateTime dialog_DateTime = new Dialog_DateTime();
                if (dialog_DateTime.ShowDialog() == DialogResult.Yes)
                {
                    效期 = dialog_DateTime.Value.ToDateString();
                }
                else
                {
                    return;
                }
                Dialog_輸入批號 dialog_輸入批號 = new Dialog_輸入批號();
                if (dialog_輸入批號.ShowDialog() == DialogResult.Yes)
                {
                    批號 = dialog_輸入批號.Value;
                }
                else
                {
                    return;
                }
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                {
                    數量 = dialog_NumPannel.Value.ToString();
                }
                else
                {
                    return;
                }

                int 原有庫存 = box.取得庫存();
                string 藥品碼 = box.Code;
                藥品碼 = Function_藥品碼檢查(藥品碼);
                string 庫存量 = Function_從SQL取得庫存(藥品碼).ToString();
                box.效期庫存覆蓋(效期, 批號, 數量);
                int 修正庫存 = box.取得庫存();
                epD_1020_Pannel.CurrentDrawer.ReplaceByGUID(box);
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(epD_1020_Pannel.CurrentDrawer);
                List_EPD1020_本地資料.Add_NewDrawer(epD_1020_Pannel.CurrentDrawer);

                string GUID = Guid.NewGuid().ToString();
                string 動作 = enum_交易記錄查詢動作.效期庫存異動.GetEnumName();
                string 藥品名稱 = box.Name;
                string 藥袋序號 = "";
                string 交易量 = (修正庫存 - 原有庫存).ToString();
                string 結存量 = Function_從SQL取得庫存(藥品碼).ToString();
                string 操作人 = this.登入者名稱;
                string 病人姓名 = "";
                string 病歷號 = "";
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 備註 = $"[效期]:{效期},[批號]:{批號}";
                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
                value_trading[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                value_trading[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;

                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);
                this.Function_設定雲端資料更新();
                this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();
                this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.On_RowEnter();
            }));

        }
        private void PlC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_修正庫存_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Drawer drawer = this.epD_1020_Pannel.CurrentDrawer;
                if (drawer == null) return;
                List<object[]> list_儲位資料 = this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Get_All_Select_RowsValues();
                if (list_儲位資料.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選取儲位!");
                    return;
                }
                string box_GUID = list_儲位資料[0][(int)enum_儲位管理_EPD1020_儲位資料.GUID].ObjectToString();
                Box box = drawer.GetBox(box_GUID);
                if (box == null) return;

                object[] value = sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.GetRowValues();
                if (value == null)
                {
                    MyMessageBox.ShowDialog("未選擇效期!");
                    return;
                }
                string 效期 = value[(int)enum_儲位管理_EPD1020_效期及庫存.效期].ObjectToString();
                string 批號 = value[(int)enum_儲位管理_EPD1020_效期及庫存.批號].ObjectToString();
                string 數量 = "";
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                {
                    數量 = dialog_NumPannel.Value.ToString();
                }
                else
                {
                    return;
                }

                int 原有庫存 = box.取得庫存();
                string 藥品碼 = box.Code;
                藥品碼 = Function_藥品碼檢查(藥品碼);
                string 庫存量 = Function_從SQL取得庫存(藥品碼).ToString();
                box.效期庫存覆蓋(效期, 批號, 數量);
                int 修正庫存 = box.取得庫存();
                epD_1020_Pannel.CurrentDrawer.ReplaceByGUID(box);
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(epD_1020_Pannel.CurrentDrawer);

                string GUID = Guid.NewGuid().ToString();
                string 動作 = enum_交易記錄查詢動作.效期庫存異動.GetEnumName();
                string 藥品名稱 = box.Name;
                string 藥袋序號 = "";
                string 交易量 = (修正庫存 - 原有庫存).ToString();
                string 結存量 = Function_從SQL取得庫存(藥品碼).ToString();
                string 操作人 = this.登入者名稱;
                string 病人姓名 = "";
                string 病歷號 = "";
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 備註 = $"[效期]:{效期},[批號]:{批號}";
                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
                value_trading[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                value_trading[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;

                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);
                this.Function_設定雲端資料更新();
                this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();
                this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.On_RowEnter();
            }));
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_儲位內容_效期管理_修正批號_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Drawer drawer = this.epD_1020_Pannel.CurrentDrawer;
                if (drawer == null) return;
                List<object[]> list_儲位資料 = this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Get_All_Select_RowsValues();
                if (list_儲位資料.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選取儲位!");
                    return;
                }
                string box_GUID = list_儲位資料[0][(int)enum_儲位管理_EPD1020_儲位資料.GUID].ObjectToString();
                Box box = drawer.GetBox(box_GUID);
                if (box == null) return;

                object[] value = sqL_DataGridView_儲位管理_EPD1020_儲位內容_效期及庫存.GetRowValues();
                if (value == null)
                {
                    MyMessageBox.ShowDialog("未選擇效期!");
                    return;
                }

                string 效期 = value[(int)enum_儲位管理_EPD1020_效期及庫存.效期].ObjectToString();
                string 舊批號 = value[(int)enum_儲位管理_EPD1020_效期及庫存.批號].ObjectToString();
                string 新批號 = "";

                Dialog_輸入批號 dialog_輸入批號 = new Dialog_輸入批號();
                if (dialog_輸入批號.ShowDialog() == DialogResult.Yes)
                {
                    新批號 = dialog_輸入批號.Value;
                }
                else
                {
                    return;
                }

                box.修正批號(效期, 新批號);
                epD_1020_Pannel.CurrentDrawer.ReplaceByGUID(box);
                List_EPD1020_本地資料.Add_NewDrawer(epD_1020_Pannel.CurrentDrawer);
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(epD_1020_Pannel.CurrentDrawer);

                string GUID = Guid.NewGuid().ToString();
                string 動作 = enum_交易記錄查詢動作.效期庫存異動.GetEnumName();
                string 藥品名稱 = box.Name;
                string 藥袋序號 = "";
                string 交易量 = (0).ToString();
                string 結存量 = 0.ToString();
                string 操作人 = this.登入者名稱;
                string 病人姓名 = "";
                string 病歷號 = "";
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 備註 = $"[效期]:{效期},[批號]:{新批號}";
                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = "";
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 0.ToString();
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
                value_trading[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                value_trading[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;

                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);
                this.Function_設定雲端資料更新();
                this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();
                this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.On_RowEnter();

            }));
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_儲位內容_藥品碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            string Code = this.rJ_TextBox_儲位管理_EPD1020_儲位內容_儲位搜尋_藥品碼.Text;
            if (Code.StringIsEmpty()) return;
          
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_開鎖_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.Get_All_Select_RowsValues();
            string IP = "";
            string ID_Name = this.登入者名稱;

            foreach (object[] value in list_value)
            {
                IP = value[(int)enum_儲位管理_EPD1020_抽屜列表.IP].ObjectToString();
                // this.Function_取藥堆疊資料_新增母資料(Guid.NewGuid().ToString(), this.textBox_工程模式_領藥台_01_名稱.Text, enum_取藥堆疊_TYPE.EPD_5_83鎖控, "", IP, 登入者名稱, Color.Lime.ToColorString(), 0);
                List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetRows(enum_Locker_Index_Table.IP.GetEnumName(), IP, false);
                if (list_locker_table_value.Count > 0)
                {
                    list_locker_table_value[0][(int)enum_Locker_Index_Table.輸出狀態] = true.ToString();
                    this.sqL_DataGridView_Locker_Index_Table.SQL_Replace(list_locker_table_value[0], false);
                }
                string GUID = Guid.NewGuid().ToString();
                string 動作 = enum_交易記錄查詢動作.操作工程模式.GetEnumName();
                string 藥品名稱 = "";
                string 藥袋序號 = "";
                string 藥品碼 = "";
                string 庫存量 = 0.ToString();
                string 交易量 = 0.ToString();
                string 結存量 = 0.ToString();
                string 病人姓名 = "";
                string 操作人 = ID_Name;
                string 病歷號 = "";
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 備註 = $"儲位名稱[{value[(int)enum_儲位管理_EPD1020_抽屜列表.名稱].ObjectToString()}]";
                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
                value_trading[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                value_trading[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;
                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);
            }
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_儲位資料_刪除儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.epD_1020_Pannel.CurrentDrawer != null)
            {
                List<object[]> list_select_value = this.sqL_DataGridView_儲位管理_EPD1020_儲位資料.Get_All_Select_RowsValues();
                if(list_select_value.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選取儲位!");
                    return;
                }
                if (MyMessageBox.ShowDialog("確認刪除選取儲位?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                string GUID = list_select_value[0][(int)enum_儲位管理_EPD1020_儲位資料.GUID].ObjectToString();
                Drawer drawer = this.epD_1020_Pannel.CurrentDrawer;
                drawer.RemoveByGUID(GUID);
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();

            }
        }
        private void PlC_RJ_Button_儲位管理_EPD1020_儲位資料_新增儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.epD_1020_Pannel.CurrentDrawer != null)
            {
                Drawer drawer = this.epD_1020_Pannel.CurrentDrawer;
                Box box = new Box();
                box.GUID = Guid.NewGuid().ToString();
                box.DeviceType = DeviceType.EPD1020_lock;
                drawer.Add_NewBox(box);
                this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                this.sqL_DataGridView_儲位管理_EPD1020_抽屜列表.On_RowEnter();

            }
        }
        private void PlC_CheckBox_儲位管理_EPD1020_警報_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                string IP = rJ_TextBox_儲位管理_EPD1020_抽屜列表_IP.Texts;
                Drawer drawer = this.drawerUI_EPD_1020.SQL_GetDrawer(IP);
                if (drawer != null)
                {
                    drawer.AlarmEnable = plC_CheckBox_儲位管理_EPD1020_警報.Checked;
                    this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                    List_EPD1020_本地資料.Add_NewDrawer(drawer);
                    this.epD_1020_Pannel.CurrentDrawer = drawer;
                    this.Function_設定雲端資料更新();
                    flag_Program_輸出入檢查_輸出刷新_Init = false;
                }
            }));
        }
        #endregion

        private class ICP_儲位管理_EPD1020_抽屜列表 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string IP_0 = x[(int)enum_儲位管理_EPD1020_抽屜列表.IP].ObjectToString();
                string IP_1 = y[(int)enum_儲位管理_EPD1020_抽屜列表.IP].ObjectToString();
                string[] IP_0_Array = IP_0.Split('.');
                string[] IP_1_Array = IP_1.Split('.');
                IP_0 = "";
                IP_1 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];

                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];

                    IP_0 += IP_0_Array[i];
                    IP_1 += IP_1_Array[i];
                }
                int cmp = IP_0_Array[2].CompareTo(IP_1_Array[2]);
                if (cmp > 0)
                {
                    return 1;
                }
                else if (cmp < 0)
                {
                    return -1;
                }
                else if (cmp == 0)
                {
                    cmp = IP_0_Array[3].CompareTo(IP_1_Array[3]);
                    if (cmp > 0)
                    {
                        return 1;
                    }
                    else if (cmp < 0)
                    {
                        return -1;
                    }
                    else if (cmp == 0)
                    {
                        return 0;
                    }
                }

                return 0;

            }
        }
    }
}
