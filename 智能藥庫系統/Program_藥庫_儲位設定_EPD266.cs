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
namespace 智能藥庫系統
{
    public partial class Form1 : Form
    {
        private List<Storage> List_EPD266_本地資料 = new List<Storage>();
        private List<Storage> List_EPD266_雲端資料 = new List<Storage>();
        private List<Storage> List_EPD266_入賬資料 = new List<Storage>();
        private Storage EPD266_Storage_Copy;
        private enum enum_藥庫_儲位管理_EPD266_效期及庫存
        {
            效期,
            批號,
            庫存,
        }
        private enum enum_藥庫_儲位管理_EPD266_儲位資料
        {
            IP,
            儲位名稱,
            藥品碼,
            藥品名稱,
            藥品學名,
            藥品中文名稱,
            包裝單位,
            藥品條碼,
            庫存,
            警訊藥品,         
            區域儲位,
        }

        private bool flag_Program_藥庫_儲位管理_EPD266_Init = false;
        private void sub_Program_藥庫_儲位管理_EPD266_Init()
        {
            this.storageUI_EPD_266.Init(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode);
            this.epD_266_Pannel.Init(this.storageUI_EPD_266.List_UDP_Local);

            this.sqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥庫_藥品資料);
            this.sqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料.Set_ColumnVisible(false, new enum_藥庫_藥品資料().GetEnumNames());
            this.sqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料.Set_ColumnVisible(true, enum_藥庫_藥品資料.藥品碼, enum_藥庫_藥品資料.藥品名稱, enum_藥庫_藥品資料.中文名稱, enum_藥庫_藥品資料.包裝單位);
            this.sqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料.RowDoubleClickEvent += SqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料_RowDoubleClickEvent;
           

            this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Init();
            this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.RowEnterEvent += SqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料_RowEnterEvent;


            this.rJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼.KeyPress += RJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼_KeyPress;
            this.rJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱.KeyPress += RJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱_KeyPress;
            this.rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位名稱.KeyPress += RJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位名稱_KeyPress;
            this.rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼.KeyPress += RJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼_KeyPress;

            this.plC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼_搜尋.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼_搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱_搜尋.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱_搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_填入資料.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_填入資料_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品名稱字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品名稱字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品學名字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品學名字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_中文名稱字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_中文名稱字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品條碼字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品條碼字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品碼字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品碼字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_包裝單位字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_包裝單位字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_儲位名稱字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_儲位名稱字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_總庫存字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_總庫存字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_效期字體更動.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_效期字體更動_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_區域儲位管理.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_區域儲位管理_MouseDownEvent;

            this.plC_RJ_Button_藥庫_儲位管理_EPD266_面板亮燈.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_面板亮燈_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_清除燈號.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_清除燈號_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_上傳至面板.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_上傳至面板_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_刪除儲位.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_刪除儲位_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼搜尋.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_貼上格式.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_貼上格式_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_複製格式.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_複製格式_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_儲位初始化.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位初始化_MouseDownEvent;
            this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品名稱顯示.CheckStateChanged += PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品名稱顯示_CheckStateChanged;
            this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品學名顯示.CheckStateChanged += PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品學名顯示_CheckStateChanged;
            this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_中文名稱顯示.CheckStateChanged += PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_中文名稱顯示_CheckStateChanged;
            this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_效期顯示.CheckStateChanged += PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_效期顯示_CheckStateChanged;
            this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_Barcode顯示.CheckStateChanged += PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_Barcode顯示_CheckStateChanged;
            this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_顯示空白儲位.CheckStateChanged += PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_顯示空白儲位_CheckStateChanged;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_匯出.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_匯出_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_匯入.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_匯入_MouseDownEvent;
            this.plC_RJ_Button_藥庫_儲位管理_EPD266_自動填入儲位名稱.MouseDownEvent += PlC_RJ_Button_藥庫_儲位管理_EPD266_自動填入儲位名稱_MouseDownEvent;

            this.plC_UI_Init.Add_Method(this.Program_藥庫_儲位管理_EPD266);
        }

  
        private void Program_藥庫_儲位管理_EPD266()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥庫" && this.plC_ScreenPage_藥庫.PageText == "儲位管理" && this.plC_ScreenPage_藥庫_儲位管理.PageText == "EPD266")
            {
                if (flag_Program_藥庫_儲位管理_EPD266_Init == false)
                {
                    this.List_藥庫_DeviceBasic = DeviceBasicClass_藥庫.SQL_GetAllDeviceBasic();
                    PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool = true;
                    flag_Program_藥庫_儲位管理_EPD266_Init = true;
                }
            }
            else
            {
                flag_Program_藥庫_儲位管理_EPD266_Init = false;
            }


            sub_Program_藥庫_儲位管理_EPD266_資料更新();
        }

        #region PLC_藥庫_儲位管理_EPD266_資料更新
        PLC_Device PLC_Device_藥庫_儲位管理_EPD266_資料更新 = new PLC_Device("S9025");
        int cnt_Program_藥庫_儲位管理_EPD266_資料更新 = 65534;
        void sub_Program_藥庫_儲位管理_EPD266_資料更新()
        {
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 65534)
            {
                PLC_Device_藥庫_儲位管理_EPD266_資料更新.SetComment("PLC_藥庫_儲位管理_EPD266_資料更新");
                PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool = false;
                cnt_Program_藥庫_儲位管理_EPD266_資料更新 = 65535;
            }
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 65535) cnt_Program_藥庫_儲位管理_EPD266_資料更新 = 1;
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 1) cnt_Program_藥庫_儲位管理_EPD266_資料更新_檢查按下(ref cnt_Program_藥庫_儲位管理_EPD266_資料更新);
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 2) cnt_Program_藥庫_儲位管理_EPD266_資料更新_初始化(ref cnt_Program_藥庫_儲位管理_EPD266_資料更新);
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 3) cnt_Program_藥庫_儲位管理_EPD266_資料更新_更新藥檔(ref cnt_Program_藥庫_儲位管理_EPD266_資料更新);
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 4) cnt_Program_藥庫_儲位管理_EPD266_資料更新_更新面板資料(ref cnt_Program_藥庫_儲位管理_EPD266_資料更新);
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 5) cnt_Program_藥庫_儲位管理_EPD266_資料更新 = 65500;
            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 > 1) cnt_Program_藥庫_儲位管理_EPD266_資料更新_檢查放開(ref cnt_Program_藥庫_儲位管理_EPD266_資料更新);

            if (cnt_Program_藥庫_儲位管理_EPD266_資料更新 == 65500)
            {
                PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool = false;
                cnt_Program_藥庫_儲位管理_EPD266_資料更新 = 65535;
            }
        }
        void cnt_Program_藥庫_儲位管理_EPD266_資料更新_檢查按下(ref int cnt)
        {
            if (PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool) cnt++;
        }
        void cnt_Program_藥庫_儲位管理_EPD266_資料更新_檢查放開(ref int cnt)
        {
            if (!PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool) cnt = 65500;
        }
        void cnt_Program_藥庫_儲位管理_EPD266_資料更新_初始化(ref int cnt)
        {
            MyTimer_TickTime.TickStop();
            MyTimer_TickTime.StartTickTime(50000);
            List_EPD266_本地資料 = this.storageUI_EPD_266.SQL_GetAllStorage();
            Console.Write($"儲位管理EPD266:從SQL取得資料 ,耗時 :{MyTimer_TickTime.GetTickTime().ToString("0.000")}\n");
            cnt++;
        }
        void cnt_Program_藥庫_儲位管理_EPD266_資料更新_更新藥檔(ref int cnt)
        {
            MyTimer_TickTime.TickStop();
            MyTimer_TickTime.StartTickTime(50000);

            List<object[]> list_藥品資料_藥檔資料 = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);

            List<Storage> list_replaceValue = new List<Storage>();


            Parallel.ForEach(this.List_EPD266_本地資料, value =>
            {
                if (this.List_EPD266_本地資料 == null) return;
                List<object[]> list_藥品資料_藥檔資料_buf = new List<object[]>();

                string 藥品碼 = "";
                string 藥品名稱 = "";
                string 藥品學名 = "";
                string BarCode = "";
                string 包裝單位 = "";
                string 警訊藥品 = "";
                string 料號 = "";
                string BarCode1 = "";
                string BarCode2 = "";
                string 中文名稱 = "";

                string 藥品碼_buf = "";
                string 藥品名稱_buf = "";
                string 藥品學名_buf = "";
                string BarCode_buf = "";
                string 包裝單位_buf = "";
                string 警訊藥品_buf = "";
                string 料號_buf = "";
                string BarCode1_buf = "";
                string BarCode2_buf = "";
                string 中文名稱_buf = "";


                string IP = value.IP;
                Storage storage = value;
                bool Is_Replace = false;
                藥品碼 = storage.GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                if (藥品碼.StringIsEmpty()) return;
                list_藥品資料_藥檔資料_buf = list_藥品資料_藥檔資料.GetRows((int)enum_藥庫_藥品資料.藥品碼, 藥品碼);
                if (list_藥品資料_藥檔資料_buf.Count == 0)
                {
                    storage.Clear();
                    Is_Replace = true;
                }
                else
                {
                    藥品碼_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.藥品碼].ObjectToString();
                    藥品名稱_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.藥品名稱].ObjectToString();
                    藥品學名_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.藥品學名].ObjectToString();
                    BarCode_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.藥品條碼1].ObjectToString();
                    包裝單位_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.包裝單位].ObjectToString();
                    警訊藥品_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.警訊藥品].ObjectToString().ToUpper();
                    料號_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.料號].ObjectToString();
                    BarCode1_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.藥品條碼1].ObjectToString();
                    BarCode2_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.藥品條碼2].ObjectToString();
                    中文名稱_buf = list_藥品資料_藥檔資料_buf[0][(int)enum_藥庫_藥品資料.中文名稱].ObjectToString();



                    if (警訊藥品_buf.StringIsEmpty()) 警訊藥品_buf = false.ToString().ToUpper();
                    藥品碼 = storage.GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                    藥品名稱 = storage.GetValue(Device.ValueName.藥品名稱, Device.ValueType.Value).ObjectToString();
                    藥品學名 = storage.GetValue(Device.ValueName.藥品學名, Device.ValueType.Value).ObjectToString();
                    BarCode = storage.GetValue(Device.ValueName.BarCode, Device.ValueType.Value).ObjectToString();
                    包裝單位 = storage.GetValue(Device.ValueName.包裝單位, Device.ValueType.Value).ObjectToString();
                    警訊藥品 = storage.IsWarning ? "TRUE" : "FALSE";
                    料號 = storage.SKDIACODE;
                    BarCode1 = storage.BarCode1;
                    BarCode2 = storage.BarCode2;
                    中文名稱 = storage.ChineseName;

                    if (藥品碼 != 藥品碼_buf) Is_Replace = true;
                    if (藥品名稱 != 藥品名稱_buf) Is_Replace = true;
                    if (藥品學名 != 藥品學名_buf) Is_Replace = true;
                    if (BarCode != BarCode_buf) Is_Replace = true;
                    if (包裝單位 != 包裝單位_buf) Is_Replace = true;
                    if (警訊藥品 != 警訊藥品_buf) Is_Replace = true;
                    if (料號 != 料號_buf) Is_Replace = true;
                    if (BarCode1 != BarCode1_buf) Is_Replace = true;
                    if (BarCode2 != BarCode2_buf) Is_Replace = true;
                    if (中文名稱 != 中文名稱_buf) Is_Replace = true;

                    storage.SetValue(Device.ValueName.藥品碼, Device.ValueType.Value, 藥品碼_buf);
                    storage.SetValue(Device.ValueName.藥品名稱, Device.ValueType.Value, 藥品名稱_buf);
                    storage.SetValue(Device.ValueName.藥品學名, Device.ValueType.Value, 藥品學名_buf);
                    storage.SetValue(Device.ValueName.BarCode, Device.ValueType.Value, BarCode_buf);
                    storage.SetValue(Device.ValueName.包裝單位, Device.ValueType.Value, 包裝單位_buf);
                    storage.SKDIACODE = 料號_buf;
                    storage.BarCode1 = BarCode1_buf;
                    storage.BarCode2 = BarCode2_buf;
                    storage.ChineseName = 中文名稱_buf;


                    storage.IsWarning = (警訊藥品_buf == "TRUE");


                    storage = Function_藥庫_儲位管理_EPD266_儲位資料_更新效期批號(storage);
                }
                if (Is_Replace)
                {
                    list_replaceValue.LockAdd(value);
                }
            });



            this.storageUI_EPD_266.SQL_ReplaceStorage(list_replaceValue);
            Console.Write($"儲位管理EPD266:更新藥檔完成 ,共{list_replaceValue.Count}筆 ,耗時 :{MyTimer_TickTime.GetTickTime().ToString("0.000")}\n");
            cnt++;
        }
        void cnt_Program_藥庫_儲位管理_EPD266_資料更新_更新面板資料(ref int cnt)
        {
            MyTimer_TickTime.TickStop();
            MyTimer_TickTime.StartTickTime(50000);
            bool flag_顯示空白儲位 = plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_顯示空白儲位.Checked;
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_區域儲位 = sqL_DataGridView_貨架區域儲位列表.SQL_GetAllRows(false);
            List<object[]> list_區域儲位_buf = new List<object[]>();
            for (int i = 0; i < this.List_EPD266_本地資料.Count; i++)
            {
                object[] value = new object[new enum_藥庫_儲位管理_EPD266_儲位資料().GetLength()];
                string 藥品碼 = List_EPD266_本地資料[i].GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                if (!flag_顯示空白儲位)
                {
                    if (藥品碼.StringIsEmpty()) continue;
                }
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP] = List_EPD266_本地資料[i].GetValue(Device.ValueName.IP, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.儲位名稱] = List_EPD266_本地資料[i].GetValue(Device.ValueName.儲位名稱, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品碼] = List_EPD266_本地資料[i].GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品名稱] = List_EPD266_本地資料[i].GetValue(Device.ValueName.藥品名稱, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品學名] = List_EPD266_本地資料[i].GetValue(Device.ValueName.藥品學名, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品中文名稱] = List_EPD266_本地資料[i].GetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.包裝單位] = List_EPD266_本地資料[i].GetValue(Device.ValueName.包裝單位, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品條碼] = List_EPD266_本地資料[i].GetValue(Device.ValueName.BarCode, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.庫存] = List_EPD266_本地資料[i].GetValue(Device.ValueName.庫存, Device.ValueType.Value).ObjectToString();
                value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.警訊藥品] = List_EPD266_本地資料[i].IsWarning.ToString();
                string Master_GUID = List_EPD266_本地資料[i].Master_GUID;

                list_區域儲位_buf = list_區域儲位.GetRows((int)enum_藥庫_儲位管理_區域儲位.GUID, Master_GUID);
                if (list_區域儲位_buf.Count > 0)
                {
                    value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.區域儲位] = list_區域儲位_buf[0][(int)enum_藥庫_儲位管理_區域儲位.名稱].ObjectToString();
                }

                list_value.Add(value);
            }
            list_value.Sort(new ICP_藥庫_儲位管理_EPD266_抽屜列表());
            this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.RefreshGrid(list_value);
            Console.Write($"儲位管理EPD266:更新儲位資料完成 ,耗時 :{MyTimer_TickTime.GetTickTime().ToString("0.000")}\n");
            cnt++;
        }

        #endregion


        #region Function
        private Storage Function_藥庫_儲位管理_EPD266_儲位資料_更新效期批號(Storage storage)
        {
            lock(storage)
            {
                List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
                deviceBasics = (from temp in List_藥庫_DeviceBasic
                                where temp.Code == storage.Code
                                select temp).ToList();
                if (deviceBasics.Count > 0)
                {
                    lock (deviceBasics[0])
                    {
                        storage.List_Validity_period.Clear();
                        storage.List_Lot_number.Clear();
                        storage.List_Inventory.Clear();

                        for (int i = 0; i < deviceBasics[0].List_Inventory.Count; i++)
                        {
                            string Validity_period = deviceBasics[0].List_Validity_period[i];
                            string Lot_number = deviceBasics[0].List_Lot_number[i];
                            string Inventory = deviceBasics[0].List_Inventory[i];
                            storage.新增效期(Validity_period, Lot_number, Inventory);
                        }
                    }             
                }
            }
          
            return storage;
        }
        #endregion
        #region Event
        private void SqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料_RowDoubleClickEvent(object[] RowValue)
        {
            PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_填入資料_MouseDownEvent(null);
        }
        private void SqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料_RowEnterEvent(object[] RowValue)
        {
            string IP = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
            string 儲位名稱 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.儲位名稱].ObjectToString();
            string 藥品碼 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品碼].ObjectToString();
            string 藥品名稱 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品名稱].ObjectToString();
            string 藥品學名 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品學名].ObjectToString();
            string 藥品中文名稱 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品中文名稱].ObjectToString();
            string 包裝單位 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.包裝單位].ObjectToString();
            string 藥品條碼 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品條碼].ObjectToString();
            string 庫存 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.庫存].ObjectToString();
            string 警訊藥品 = RowValue[(int)enum_藥庫_儲位管理_EPD266_儲位資料.警訊藥品].ObjectToString();

            this.Invoke(new Action(delegate
            {
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_藥品名稱.Texts = 藥品名稱;
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_藥品碼.Texts = 藥品碼;
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_藥品學名.Texts = 藥品學名;
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_中文名稱.Texts = 藥品中文名稱;
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_包裝單位.Texts = 包裝單位;
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_藥品條碼.Texts = 藥品條碼;
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位名稱.Texts = 儲位名稱;
                rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_總庫存.Texts = 庫存;
            }));
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5500000);
            Storage storage = this.storageUI_EPD_266.SQL_GetStorage(IP);
            Console.WriteLine($"{myTimer.ToString()}"); 
            storage.IsWarning = (警訊藥品 == "True");
            if (storage != null)
            {
                this.epD_266_Pannel.DrawToPictureBox(storage);
            }
            this.Invoke(new Action(delegate
            {
                this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品名稱顯示.Checked = (bool)storage.GetValue(Device.ValueName.藥品名稱, Device.ValueType.Visable);
                this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品學名顯示.Checked = (bool)storage.GetValue(Device.ValueName.藥品學名, Device.ValueType.Visable);
                this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_中文名稱顯示.Checked = (bool)storage.GetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Visable);
                this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_效期顯示.Checked = (bool)storage.GetValue(Device.ValueName.效期, Device.ValueType.Visable);
                this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_Barcode顯示.Checked = (bool)storage.GetValue(Device.ValueName.BarCode, Device.ValueType.Visable);
            }));


        }
        private void RJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱_搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼_搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.epD_266_Pannel.CurrentStorage == null) return;
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.epD_266_Pannel.CurrentStorage == null) return;
            if (e.KeyChar == (char)Keys.Enter)
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                storage.SetValue(Device.ValueName.儲位名稱, Device.ValueType.Value, this.rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位名稱.Text);
                this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                this.List_EPD266_本地資料.Add_NewStorage(storage);
            }
        }

        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱.Text.Length < 3)
            {
                MyMessageBox.ShowDialog("藥品名稱搜尋字元不得小於3個!");
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
            if (rJ_RatioButton_藥庫_儲位管理_EPD266_藥品搜尋_前綴.Checked)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥庫_藥品資料.藥品名稱, rJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱.Text);
            }
            else if (rJ_RatioButton_藥庫_儲位管理_EPD266_藥品搜尋_模糊.Checked)
            {
                list_value = list_value.GetRowsByLike((int)enum_藥庫_藥品資料.藥品名稱, rJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品名稱.Text);
            }

            this.sqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼.Text.StringIsEmpty()) return;
            List<object[]> list_value = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
            list_value = list_value.GetRowsByLike((int)enum_藥庫_藥品資料.藥品碼, rJ_TextBox_藥庫_儲位管理_EPD266_藥品搜尋_藥品碼.Text);
            this.sqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_藥品搜尋_填入資料_MouseDownEvent(MouseEventArgs mevent)
        {
            object[] value = this.sqL_DataGridView_藥庫_儲位管理_EPD266_藥品資料_藥檔資料.GetRowValues();
            if (value == null) return;
            Storage storage = this.epD_266_Pannel.CurrentStorage;
            if (storage == null) return;
            storage.Clear();
            storage.SetValue(Device.ValueName.藥品碼, Device.ValueType.Value, value[(int)enum_藥庫_藥品資料.藥品碼]);
            storage.SetValue(Device.ValueName.藥品名稱, Device.ValueType.Value, value[(int)enum_藥庫_藥品資料.藥品名稱]);
            storage.SetValue(Device.ValueName.藥品學名, Device.ValueType.Value, value[(int)enum_藥庫_藥品資料.藥品學名]);
            storage.SetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Value, value[(int)enum_藥庫_藥品資料.中文名稱]);
            storage.SetValue(Device.ValueName.包裝單位, Device.ValueType.Value, value[(int)enum_藥庫_藥品資料.包裝單位]);
            storage.SetValue(Device.ValueName.BarCode, Device.ValueType.Value, value[(int)enum_藥庫_藥品資料.藥品條碼1]);
            if (value[(int)enum_藥庫_藥品資料.警訊藥品].ObjectToString().ToUpper() == true.ToString().ToUpper())
            {
                storage.BackColor = Color.Red;
                storage.ForeColor = Color.White;
            }
            else
            {
                storage.BackColor = Color.White;
                storage.ForeColor = Color.Black;
            }

            value = new object[new enum_藥庫_儲位管理_EPD266_儲位資料().GetLength()];
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP] = storage.GetValue(Device.ValueName.IP, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.儲位名稱] = storage.GetValue(Device.ValueName.儲位名稱, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品碼] = storage.GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品名稱] = storage.GetValue(Device.ValueName.藥品名稱, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品學名] = storage.GetValue(Device.ValueName.藥品學名, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品中文名稱] = storage.GetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.包裝單位] = storage.GetValue(Device.ValueName.包裝單位, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.藥品條碼] = storage.GetValue(Device.ValueName.BarCode, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.庫存] = storage.GetValue(Device.ValueName.庫存, Device.ValueType.Value).ObjectToString();
            value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.警訊藥品] = storage.IsWarning.ToString();
            storage = Function_藥庫_儲位管理_EPD266_儲位資料_更新效期批號(storage);

            this.List_EPD266_本地資料.Add_NewStorage(storage);
            this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
            this.epD_266_Pannel.DrawToPictureBox(storage);
            this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Replace(enum_藥庫_儲位管理_EPD266_儲位資料.IP.GetEnumName(), value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString(), value, true);
            this.Function_設定雲端資料更新();

        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_總庫存字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.庫存, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {

                    storage.SetValue(Device.ValueName.庫存, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));

        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_儲位名稱字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.儲位名稱, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.儲位名稱, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_包裝單位字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.包裝單位, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.包裝單位, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品碼字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品碼, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品碼, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品條碼字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.BarCode, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.BarCode, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_中文名稱字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品學名字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品學名, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品學名, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_藥品名稱字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品名稱, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品名稱, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_效期字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.效期, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.效期, Device.ValueType.Font, fontDialog.Font);
                    this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }

        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_區域儲位管理_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_Replace_Value = new List<object[]>();
            List<object[]> list_value = this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Get_All_Select_RowsValues();
            List<object[]> list_區域儲位 = sqL_DataGridView_貨架區域儲位列表.SQL_GetAllRows(false);
            List<object[]> list_區域儲位_buf = new List<object[]>();
            if (list_value.Count == 0)
            {
                this.Invoke(new Action(delegate
                {
                    MyMessageBox.ShowDialog("未選擇儲位");
                }));
                return;
            }

            List<Storage> storages = this.storageUI_EPD_266.SQL_GetAllStorage();
            List<Storage> storages_buf = new List<Storage>();
            List<Storage> storages_replace = new List<Storage>();
            for (int i = 0; i < list_value.Count; i++)
            {
                storages_buf = (from temp in storages
                                where temp.IP == list_value[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString()
                                select temp).ToList();
                if (storages_buf.Count > 0)
                {
                    storages_replace.Add(storages_buf[0]);
                }
            }

            DialogResult dialogResult = DialogResult.None;
            string value = "";
            this.Invoke(new Action(delegate
            {
                Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(Function_藥庫_儲位管理_區域儲位_取得選單());
                dialog_ContextMenuStrip.TitleText = "區域儲位管理";
                dialog_ContextMenuStrip.ControlsTextAlign = ContentAlignment.MiddleLeft;
                dialog_ContextMenuStrip.ControlsHeight = 40;
                dialogResult = dialog_ContextMenuStrip.ShowDialog();
                value = dialog_ContextMenuStrip.Value;
            }));

            if (dialogResult == DialogResult.Yes)
            {
                string[] strArray = myConvert.分解分隔號字串(value, ".");
                if (strArray.Length == 2)
                {
                    int 序號 = strArray[0].StringToInt32();
                    if (序號 <= 0) return;
                    string Master_GUID = "";
                    list_區域儲位_buf = list_區域儲位.GetRows((int)enum_藥庫_儲位管理_區域儲位.序號, 序號.ToString());
                    if (list_區域儲位_buf.Count > 0)
                    {
                        Master_GUID = list_區域儲位_buf[0][(int)enum_藥庫_儲位管理_區域儲位.GUID].ObjectToString();
                        for (int i = 0; i < storages_replace.Count; i++)
                        {
                            storages_replace[i].Master_GUID = Master_GUID;
                            this.List_EPD266_本地資料.Add_NewStorage(storages_replace[i]);
                        }
                   
                        this.storageUI_EPD_266.SQL_ReplaceStorage(storages_replace);
                    }
                }
                this.Function_設定雲端資料更新();
                PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool = true;
            }
        }

        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_面板亮燈_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;
            Color color = Color.Black;

            if (this.radioButton_藥庫_儲位管理_EPD266_面板亮燈_白.Checked)
            {
                color = Color.White;
            }
            else if (this.radioButton_藥庫_儲位管理_EPD266_面板亮燈_紅.Checked)
            {
                color = Color.Red;
            }
            else if (this.radioButton_藥庫_儲位管理_EPD266_面板亮燈_藍.Checked)
            {
                color = Color.Blue;
            }
            else if (this.radioButton_藥庫_儲位管理_EPD266_面板亮燈_綠.Checked)
            {
                color = Color.Green;
            }
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string IP = list_value[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                Storage storage = this.storageUI_EPD_266.SQL_GetStorage(IP);

                taskList.Add(Task.Run(() =>
                {
                    if (storage != null)
                    {
                        if (!this.storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color))
                        {
                            MyMessageBox.ShowDialog($"{storage.IP}:{storage.Port} : EPD266 面板亮燈失敗!");
                        }
                        Console.WriteLine($"{storage.IP}:{storage.Port} : EPD266 面板亮燈成功!");
                    }
                }));
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();

        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_清除燈號_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;

            List<Task> taskList = new List<Task>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string IP = list_value[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                Storage storage = this.storageUI_EPD_266.SQL_GetStorage(IP);

                taskList.Add(Task.Run(() =>
                {
                    if (storage != null)
                    {

                        if (!this.storageUI_EPD_266.Set_Stroage_LED_UDP(storage, Color.Black))
                        {
                            MyMessageBox.ShowDialog($"{storage.IP}:{storage.Port} : EPD266 面板滅燈失敗!");
                        }
                        Console.WriteLine($"{storage.IP}:{storage.Port} : EPD266 面板滅燈成功!");
                    }
                }));
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_上傳至面板_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0) return;

            List<Task> taskList = new List<Task>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string IP = list_value[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                Storage storage = this.storageUI_EPD_266.SQL_GetStorage(IP);

                taskList.Add(Task.Run(() =>
                {
                    if (storage != null)
                    {
                        if (!this.storageUI_EPD_266.DrawToEpd_UDP(storage))
                        {
                            MyMessageBox.ShowDialog($"{storage.IP}:{storage.Port} : EPD266 面板上傳失敗!");
                        }
                        Console.WriteLine($"{storage.IP}:{storage.Port} : EPD266 面板上傳成功!");
                    }
                }));
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_刪除儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("使否刪除選取儲位?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
            {
                List<object[]> list_value = sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Get_All_Select_RowsValues();
                List<object[]> list_value_buf = new List<object[]>();
                if (list_value.Count == 0) return;
                for (int i = 0; i < list_value.Count; i++)
                {
                    string IP = list_value[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                    Storage storage = this.storageUI_EPD_266.SQL_GetStorage(IP);
                    storage.Clear();
                    this.List_EPD266_本地資料.Add_NewStorage(storage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);

                    list_value_buf = this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.GetRows((int)enum_藥庫_儲位管理_EPD266_儲位資料.IP, storage.IP, false);
                    if (list_value_buf.Count == 0) continue;
                    list_value_buf[0] = new object[new enum_藥庫_儲位管理_EPD266_儲位資料().GetLength()];
                    list_value_buf[0][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP] = storage.IP;
                    list_value_buf[0][(int)enum_藥庫_儲位管理_EPD266_儲位資料.警訊藥品] = false.ToString();

                    this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Replace((int)enum_藥庫_儲位管理_EPD266_儲位資料.IP, storage.IP, list_value_buf[0], true);

                }
                this.Function_設定雲端資料更新();
            }
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_複製格式_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                this.Invoke(new Action(delegate
                {
                    MyMessageBox.ShowDialog("未選擇儲位!");
                }));
                return;
            }
            string IP = list_value[0][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();

            Storage storage = this.storageUI_EPD_266.SQL_GetStorage(IP);
            if (storage == null)
            {
                this.Invoke(new Action(delegate
                {
                    MyMessageBox.ShowDialog($"未搜尋到 {IP} 儲位!");
                }));
                return;
            }
            EPD266_Storage_Copy = storage;
            MyMessageBox.ShowDialog("已複製到剪貼簿!");

        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_貼上格式_MouseDownEvent(MouseEventArgs mevent)
        {
            if (EPD266_Storage_Copy == null)
            {
                this.Invoke(new Action(delegate
                {
                    MyMessageBox.ShowDialog($"尚未複製儲位!");
                }));
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                this.Invoke(new Action(delegate
                {
                    MyMessageBox.ShowDialog("未選擇儲位!");
                }));
                return;
            }
            DialogResult dialogResult = DialogResult.None;
            this.Invoke(new Action(delegate
            {
                dialogResult = MyMessageBox.ShowDialog("是否覆蓋選取儲位?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel);
            }));
            if (dialogResult != DialogResult.Yes) return;

            List<Storage> storages_replace = new List<Storage>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string IP = list_value[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                Storage storage = List_EPD266_本地資料.SortByIP(IP);
                if (storage != null)
                {
                    storage.PasteFormat(EPD266_Storage_Copy);
                    List_EPD266_本地資料.Add_NewStorage(storage);
                    storages_replace.Add(storage);
                }
            }
            this.storageUI_EPD_266.SQL_ReplaceStorage(storages_replace);
            sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.On_RowEnter();
            this.Function_設定雲端資料更新();
            MyMessageBox.ShowDialog("貼上格式完成!");
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            string Code = this.rJ_TextBox_藥庫_儲位管理_EPD266_儲位內容_儲位搜尋_藥品碼.Text;
            if (Code.StringIsEmpty()) return;
            List<Storage> storages = this.List_EPD266_本地資料.SortByCode(Code);
            if (storages.Count == 0)
            {
                MyMessageBox.ShowDialog("查無無此藥品!!");
                return;
            }
            int select_index = -1;
            object[] value = sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.GetRowValues();
            if (value != null)
            {
                string IP = value[(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                for (int i = 0; i < storages.Count; i++)
                {
                    if (storages[i].IP == IP)
                    {
                        select_index = i;
                    }
                }
            }

            Storage storage;
            if (storages.Count == 0) return;
            if (storages[0] == null) return;
            if (select_index == -1)
            {
                storage = storages[0];
            }
            else if ((select_index + 1) == storages.Count)
            {
                storage = storages[0];
            }
            else
            {
                storage = storages[select_index + 1];
            }

            this.epD_266_Pannel.DrawToPictureBox(storage);

            List<object[]> list_values = sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.GetAllRows();
            for (int i = 0; i < list_values.Count; i++)
            {
                if (list_values[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString() == storage.IP)
                {
                    sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.SetSelectRow(i);
                    return;
                }
            }
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_儲位初始化_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (MyMessageBox.ShowDialog("確認將所有儲位效期庫存更動為測試版?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    List<Storage> storages = this.storageUI_EPD_266.SQL_GetAllStorage();
                    List<Device> devices = storages.GetAllDevice();
                    Dialog_Prcessbar dialog_Prcessbar = new Dialog_Prcessbar(devices.Count);
                    dialog_Prcessbar.State = "更動效期庫存..";
                    for (int i = 0; i < devices.Count; i++)
                    {
                        dialog_Prcessbar.Value = i;

                        devices[i].清除所有庫存資料();
                        if (!devices[i].Code.StringIsEmpty())
                        {
                            devices[i].效期庫存覆蓋("2050/01/01", "999999");
                        }

                    }
                    dialog_Prcessbar.State = "上傳資料..";
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storages);
                    dialog_Prcessbar.Close();
                }
            }));
        }
        private void PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品名稱顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.藥品名稱, Device.ValueType.Visable, this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品名稱顯示.Checked);
                this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品學名顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.藥品學名, Device.ValueType.Visable, this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_藥品學名顯示.Checked);
                this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));

        }
        private void PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_中文名稱顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Visable, this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_中文名稱顯示.Checked);
                this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_效期顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.效期, Device.ValueType.Visable, this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_效期顯示.Checked);
                this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_Barcode顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_266_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.BarCode, Device.ValueType.Visable, this.plC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_Barcode顯示.Checked);
                this.epD_266_Pannel.DrawToPictureBox(this.epD_266_Pannel.CurrentStorage);
                this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_藥庫_儲位管理_EPD266_儲位內容_顯示空白儲位_CheckStateChanged(object sender, EventArgs e)
        {
            PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool = true;
        }
     
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_自動填入儲位名稱_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("確認自動填入儲位名稱?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<object[]> list_儲位列表 = this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.GetAllRows();
            for (int i = 0; i < list_儲位列表.Count; i++)
            {
                string IP = list_儲位列表[i][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                Storage storage = this.List_EPD266_本地資料.SortByIP(IP);
                if (storage == null) continue;
                storage.StorageName = $"{i + 1}";
                this.List_EPD266_本地資料.Add_NewStorage(storage);
            }
            this.storageUI_EPD_266.SQL_ReplaceStorage(this.List_EPD266_本地資料);
            this.Function_設定雲端資料更新();
            PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool = true;
            while (true)
            {
                if (PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool == false) break;
                System.Threading.Thread.Sleep(10);
            }

        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult dialogResult = DialogResult.None;
            if (MyMessageBox.ShowDialog("確認匯入所有儲位?將會全部覆蓋!", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            this.Invoke(new Action(delegate
            {
                dialogResult = this.openFileDialog_LoadExcel.ShowDialog();
            }));
            if (dialogResult != DialogResult.OK) return;
            SheetClass sheetClass = MyOffice.ExcelClass.NPOI_LoadToSheetClass(this.openFileDialog_LoadExcel.FileName);
            for (int k = 0; k < sheetClass.Rows.Count; k++)
            {
                string SotrageName = sheetClass.Rows[k].Cell[0].Text;
                string Code = sheetClass.Rows[k].Cell[1].Text;

                Storage storage = this.List_EPD266_本地資料.SortByName(SotrageName);
                if (storage == null) continue;
                storage.Code = Code;
                this.List_EPD266_本地資料.Add_NewStorage(storage);
            }
            this.storageUI_EPD_266.SQL_ReplaceStorage(this.List_EPD266_本地資料);
            this.Function_設定雲端資料更新();
            PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool = true;
            while (true)
            {
                if (PLC_Device_藥庫_儲位管理_EPD266_資料更新.Bool == false) break;
                System.Threading.Thread.Sleep(10);
            }
        }
        private void PlC_RJ_Button_藥庫_儲位管理_EPD266_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult dialogResult = DialogResult.None;
            this.Invoke(new Action(delegate
            {
                dialogResult = this.saveFileDialog_SaveExcel.ShowDialog();
            }));
            if (dialogResult != DialogResult.OK) return;
            List<SheetClass> sheetClasses = new List<SheetClass>();
            SheetClass sheetClass = new SheetClass("EPD266");
            List<object[]> list_儲位列表 = this.sqL_DataGridView_藥庫_儲位管理_EPD266_儲位資料.GetAllRows();
            for (int d = 0; d < list_儲位列表.Count; d++)
            {
                string IP = list_儲位列表[d][(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                Storage storage = List_EPD266_本地資料.SortByIP(IP);
                if (storage == null) continue;

                sheetClass.ColumnsWidth.Add(5000);
                sheetClass.ColumnsWidth.Add(5000);
                sheetClass.AddNewCell(d, 0, $"{storage.StorageName}", new Font("微軟正黑體", 14), 500);
                sheetClass.AddNewCell(d, 1, $"{storage.Code}", new Font("微軟正黑體", 14), 500);
            }
            sheetClasses.Add(sheetClass);
            sheetClasses.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);
            MyMessageBox.ShowDialog("匯出完成!");

        }
        #endregion

        private class ICP_藥庫_儲位管理_EPD266_抽屜列表 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string IP_0 = x[(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
                string IP_1 = y[(int)enum_藥庫_儲位管理_EPD266_儲位資料.IP].ObjectToString();
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
