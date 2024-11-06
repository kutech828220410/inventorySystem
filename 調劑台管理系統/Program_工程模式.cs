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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using HIS_DB_Lib;
using SQLUI;
using DrawingClass;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {

        private void Program_工程模式_Init()
        {
            this.plC_Button_工程模式_全部開鎖.btnClick += PlC_Button_工程模式_全部開鎖_btnClick;
            this.button_工程模式_調劑台名稱儲存.Click += Button_工程模式_調劑台名稱儲存_Click;
            this.plC_UI_Init.Add_Method(this.sub_Program_工程模式);

            panel_工程模式_領藥台_03_顏色.Click += Panel_工程模式_領藥台_03_顏色_Click;
            panel_工程模式_領藥台_04_顏色.Click += Panel_工程模式_領藥台_04_顏色_Click;
        }



        bool flag_工程模式_頁面更新 = false;
        private void sub_Program_工程模式()
        {
            if (plC_NumBox_亮燈亮度.Value > 100) plC_NumBox_亮燈亮度.Value = 100;
            if (plC_NumBox_亮燈亮度.Value <= 0) plC_NumBox_亮燈亮度.Value = 80;
            DrawerUI_EPD_583.Lightness = plC_NumBox_亮燈亮度.Value / 100D;
            RowsLEDUI.Lightness = plC_NumBox_亮燈亮度.Value / 100D;
            if (this.plC_ScreenPage_Main.PageText == "工程模式")
            {
                if (!this.flag_工程模式_頁面更新)
                {
                    this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(true);
                    this.Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.操作工程模式, this.登入者名稱, "");
                    this.Function_工程模式_鎖控按鈕更新();
                    this.flag_工程模式_頁面更新 = true;
                }
            }
            else
            {
                this.flag_工程模式_頁面更新 = false;
            }
            this.sub_Program_檢查Pannel35_主畫面();
        }

        #region PLC_檢查Pannel35_主畫面
        PLC_Device PLC_Device_檢查Pannel35_主畫面 = new PLC_Device("");
        PLC_Device PLC_Device_檢查Pannel35_主畫面_OK = new PLC_Device("");
        int cnt_Program_檢查Pannel35_主畫面 = 65534;
        void sub_Program_檢查Pannel35_主畫面()
        {
            if (PLC_Device_主機扣賬模式.Bool) PLC_Device_檢查Pannel35_主畫面.Bool = true;
            if (cnt_Program_檢查Pannel35_主畫面 == 65534)
            {
                PLC_Device_檢查Pannel35_主畫面.SetComment("PLC_檢查Pannel35_主畫面");
                PLC_Device_檢查Pannel35_主畫面_OK.SetComment("PLC_檢查Pannel35_主畫面_OK");
                PLC_Device_檢查Pannel35_主畫面.Bool = false;
                cnt_Program_檢查Pannel35_主畫面 = 65535;
            }
            if (cnt_Program_檢查Pannel35_主畫面 == 65535) cnt_Program_檢查Pannel35_主畫面 = 1;
            if (cnt_Program_檢查Pannel35_主畫面 == 1) cnt_Program_檢查Pannel35_主畫面_檢查按下(ref cnt_Program_檢查Pannel35_主畫面);
            if (cnt_Program_檢查Pannel35_主畫面 == 2) cnt_Program_檢查Pannel35_主畫面_初始化(ref cnt_Program_檢查Pannel35_主畫面);
            if (cnt_Program_檢查Pannel35_主畫面 == 3) cnt_Program_檢查Pannel35_主畫面 = 65500;
            if (cnt_Program_檢查Pannel35_主畫面 > 1) cnt_Program_檢查Pannel35_主畫面_檢查放開(ref cnt_Program_檢查Pannel35_主畫面);

            if (cnt_Program_檢查Pannel35_主畫面 == 65500)
            {
                PLC_Device_檢查Pannel35_主畫面.Bool = false;
                PLC_Device_檢查Pannel35_主畫面_OK.Bool = false;
                cnt_Program_檢查Pannel35_主畫面 = 65535;
            }
        }
        void cnt_Program_檢查Pannel35_主畫面_檢查按下(ref int cnt)
        {
            if (PLC_Device_檢查Pannel35_主畫面.Bool) cnt++;
        }
        void cnt_Program_檢查Pannel35_主畫面_檢查放開(ref int cnt)
        {
            if (!PLC_Device_檢查Pannel35_主畫面.Bool) cnt = 65500;
        }
        void cnt_Program_檢查Pannel35_主畫面_初始化(ref int cnt)
        {
            List<StorageUI_WT32.UDP_READ> uDP_READs = this.storageUI_WT32.GerAllUDP_READ();
            List<string> list_refresh_ip = new List<string>();
            for (int i = 0; i < uDP_READs.Count; i++)
            {
                if (uDP_READs[i].Screen_Page == (int)StorageUI_WT32.enum_Page.主頁面 && uDP_READs[i].ScreenPage_Init == true)
                {
                    list_refresh_ip.Add(uDP_READs[i].IP);
                }
            }
            List<Task> taskList = new List<Task>();
            List<Storage> storages = List_Pannel35_本地資料;
            try
            {
                for (int i = 0; i < list_refresh_ip.Count; i++)
                {
                    List<Storage> storages_buf = (from value in storages
                                                  where value.IP == list_refresh_ip[i]
                                                  select value).ToList();
                    if (storages_buf.Count > 0)
                    {
                        taskList.Add(Task.Run(() =>
                        {
                            this.storageUI_WT32.Set_DrawPannelJEPG(storages_buf[0]);
                        }));
                    }

                }

                Task allTask = Task.WhenAll(taskList);
                allTask.Wait();
            }
            catch
            {

            }

            cnt++;
        }


        #endregion
        #region Function
        private void Function_工程模式_鎖控按鈕更新()
        {
            this.Function_從SQL取得儲位到本地資料();
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            List<object[]> list_locker_table_value_buf = new List<object[]>();
            string OutputAdress = "";
            string IP = "";
            int Num = -1;
            for (int i = 0; i < this.List_Locker.Count; i++)
            {
                OutputAdress = List_Locker[i].Get_OutputAdress();
                list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出位置, OutputAdress);
                if (list_locker_table_value_buf.Count == 0)
                {
                    List_Locker[i].Visible = false;
                    continue;
                }
                IP = list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.IP].ObjectToString();
                Num = list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Num].ObjectToString().StringToInt32();
                object device = Fucnction_從本地資料取得儲位(IP);
                if (device == null)
                {
                    List_Locker[i].Visible = false;
                    continue;
                }
                if(device is Drawer)
                {
                    Drawer drawer = device as Drawer;
                    List_Locker[i].IP = IP;
                    List_Locker[i].Visible = true;
                    if (drawer.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = drawer.Name;
                }
                if(device is Storage)
                {
                    Storage storage = device as Storage;
                    List_Locker[i].IP = IP;
                    List_Locker[i].Visible = true;
                    if (storage.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = storage.Name;
                }
                if(device is RowsLED)
                {
                    RowsLED rowsLED = device as RowsLED;
                    List_Locker[i].IP = IP;
                    List_Locker[i].Visible = true;
                    if (rowsLED.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = rowsLED.Name;
                }
                if(device is RFIDClass)
                {
                    if (Num == -1) return;
                    RFIDClass rFIDClass = device as RFIDClass;
                    RFIDClass.DeviceClass deviceClass = rFIDClass.DeviceClasses[Num];
                    List_Locker[i].IP = IP;
                    List_Locker[i].Num = Num;
                    List_Locker[i].Visible = true;
                    if (deviceClass.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = deviceClass.Name;

                }
                else
                {
                    
                }
            }

        }

        #endregion
        #region Event

        private void Button_工程模式_調劑台名稱儲存_Click(object sender, EventArgs e)
        {
            this.SaveConfig工程模式();
            PLC.properties.Device.Set_Device("S8", true);
            MyMessageBox.ShowDialog("儲存成功!");
        }
        private void panel_工程模式_領藥台_01_顏色_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_工程模式_領藥台_01_顏色.BackColor = colorDialog.Color;
            }
        }
        private void panel_工程模式_領藥台_02_顏色_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_工程模式_領藥台_02_顏色.BackColor = colorDialog.Color;
            }
        }
        private void Panel_工程模式_領藥台_03_顏色_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_工程模式_領藥台_03_顏色.BackColor = colorDialog.Color;
            }
        }
        private void Panel_工程模式_領藥台_04_顏色_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_工程模式_領藥台_04_顏色.BackColor = colorDialog.Color;
            }
        }
       
        private void PlC_Button_工程模式_全部開鎖_btnClick(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowDialog("是否全部抽屜解鎖?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            for (int i = 0; i < this.List_Locker.Count; i++)
            {
                if (this.List_Locker[i].Visible) this.List_Locker[i].Open();
            }
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            for (int i = 0; i < list_locker_table_value.Count; i++)
            {
                list_locker_table_value[i][(int)enum_Locker_Index_Table.輸出狀態] = true.ToString();
            }
            this.sqL_DataGridView_Locker_Index_Table.SQL_ReplaceExtra(list_locker_table_value, false);
        }
        #endregion
        #region StreamIO
        [Serializable]
        private class SaveConfig工程模式Class
        {
            public string 領藥台_01_名稱 = "";
            public string 領藥台_02_名稱 = "";
            public string 領藥台_03_名稱 = "";
            public string 領藥台_04_名稱 = "";
            public string 門未關閉警報語音內容 = "";

            public Color 領藥台_01_Color = Color.Black;
            public Color 領藥台_02_Color = Color.Black;
            public Color 領藥台_03_Color = Color.Black;
            public Color 領藥台_04_Color = Color.Black;

            public int 領藥台_01_RowsHeight = 80;
            public int 領藥台_02_RowsHeight = 80;
            public int 領藥台_03_RowsHeight = 80;
            public int 領藥台_04_RowsHeight = 80;

            public string 領藥台_01_columns_jsonStr;
            public string 領藥台_02_columns_jsonStr;
            public string 領藥台_03_columns_jsonStr;
            public string 領藥台_04_columns_jsonStr;

            public int 領藥台_01_藥品資訊_Height = 275;
            public int 領藥台_02_藥品資訊_Height = 275;

            public bool flah_圖文辨識樣式01 = false;
            public bool flah_圖文辨識樣式02 = false;
            public bool flah_圖文辨識樣式03 = false;
            public bool flah_圖文辨識樣式04 = false;
        }
        public void SaveConfig工程模式()
        {
            
            string StreamName = $@"{currentDirectory}\adminConfig.pro";
            SaveConfig工程模式Class saveConfig = new SaveConfig工程模式Class();

            saveConfig.領藥台_01_Color = this.panel_工程模式_領藥台_01_顏色.BackColor;
            saveConfig.領藥台_02_Color = this.panel_工程模式_領藥台_02_顏色.BackColor;
            saveConfig.領藥台_03_Color = this.panel_工程模式_領藥台_03_顏色.BackColor;
            saveConfig.領藥台_04_Color = this.panel_工程模式_領藥台_04_顏色.BackColor;

            saveConfig.領藥台_01_columns_jsonStr = _sqL_DataGridView_領藥台_01_領藥內容.GetColumnsJsonStr();
            saveConfig.領藥台_02_columns_jsonStr = _sqL_DataGridView_領藥台_02_領藥內容.GetColumnsJsonStr();
            saveConfig.領藥台_03_columns_jsonStr = _sqL_DataGridView_領藥台_03_領藥內容.GetColumnsJsonStr();
            saveConfig.領藥台_04_columns_jsonStr = _sqL_DataGridView_領藥台_04_領藥內容.GetColumnsJsonStr();

            saveConfig.領藥台_01_藥品資訊_Height = _panel_領藥台_01_藥品資訊.Height;
            saveConfig.領藥台_02_藥品資訊_Height = _panel_領藥台_02_藥品資訊.Height;

            saveConfig.flah_圖文辨識樣式01 = _sqL_DataGridView_領藥台_01_領藥內容.CustomEnable;
            saveConfig.flah_圖文辨識樣式02 = _sqL_DataGridView_領藥台_02_領藥內容.CustomEnable;
            saveConfig.flah_圖文辨識樣式03 = _sqL_DataGridView_領藥台_03_領藥內容.CustomEnable;
            saveConfig.flah_圖文辨識樣式04 = _sqL_DataGridView_領藥台_04_領藥內容.CustomEnable;

            saveConfig.領藥台_01_RowsHeight = _sqL_DataGridView_領藥台_01_領藥內容.RowsHeight;
            saveConfig.領藥台_02_RowsHeight = _sqL_DataGridView_領藥台_02_領藥內容.RowsHeight;
            saveConfig.領藥台_03_RowsHeight = _sqL_DataGridView_領藥台_03_領藥內容.RowsHeight;
            saveConfig.領藥台_04_RowsHeight = _sqL_DataGridView_領藥台_04_領藥內容.RowsHeight;

            Basic.FileIO.SaveProperties(saveConfig, StreamName);
        }
        public void LoadConfig工程模式()
        {
            string StreamName = $@"{currentDirectory}\adminConfig.pro";
            object temp = new object();
            Basic.FileIO.LoadProperties(ref temp, StreamName);
            if(temp is SaveConfig工程模式Class)
            {
                SaveConfig工程模式Class saveConfig = (SaveConfig工程模式Class)temp;
               this.Invoke(new Action(delegate 
                {

                    this.panel_工程模式_領藥台_01_顏色.BackColor = saveConfig.領藥台_01_Color;
                    this.panel_工程模式_領藥台_02_顏色.BackColor = saveConfig.領藥台_02_Color;
                    this.panel_工程模式_領藥台_03_顏色.BackColor = saveConfig.領藥台_03_Color;
                    this.panel_工程模式_領藥台_04_顏色.BackColor = saveConfig.領藥台_04_Color;
                    if(ControlMode == false)
                    {
                        _sqL_DataGridView_領藥台_01_領藥內容.CustomEnable = saveConfig.flah_圖文辨識樣式01;
                        _sqL_DataGridView_領藥台_02_領藥內容.CustomEnable = saveConfig.flah_圖文辨識樣式02;
                        _sqL_DataGridView_領藥台_03_領藥內容.CustomEnable = saveConfig.flah_圖文辨識樣式03;
                        _sqL_DataGridView_領藥台_04_領藥內容.CustomEnable = saveConfig.flah_圖文辨識樣式04;
                        if (saveConfig.領藥台_01_columns_jsonStr.StringIsEmpty() == false) _sqL_DataGridView_領藥台_01_領藥內容.SetColumnsJsonStr(saveConfig.領藥台_01_columns_jsonStr);
                        if (saveConfig.領藥台_02_columns_jsonStr.StringIsEmpty() == false) _sqL_DataGridView_領藥台_02_領藥內容.SetColumnsJsonStr(saveConfig.領藥台_02_columns_jsonStr);
                        if (saveConfig.領藥台_03_columns_jsonStr.StringIsEmpty() == false) _sqL_DataGridView_領藥台_03_領藥內容.SetColumnsJsonStr(saveConfig.領藥台_03_columns_jsonStr);
                        if (saveConfig.領藥台_04_columns_jsonStr.StringIsEmpty() == false) _sqL_DataGridView_領藥台_04_領藥內容.SetColumnsJsonStr(saveConfig.領藥台_04_columns_jsonStr);

                        if (_sqL_DataGridView_領藥台_01_領藥內容.CustomEnable) { _sqL_DataGridView_領藥台_01_領藥內容.顯示首列 = false; _sqL_DataGridView_領藥台_01_領藥內容.顯示首行 = false; }
                        if (_sqL_DataGridView_領藥台_02_領藥內容.CustomEnable) { _sqL_DataGridView_領藥台_02_領藥內容.顯示首列 = false; _sqL_DataGridView_領藥台_02_領藥內容.顯示首行 = false; }
                        if (_sqL_DataGridView_領藥台_03_領藥內容.CustomEnable) { _sqL_DataGridView_領藥台_03_領藥內容.顯示首列 = false; _sqL_DataGridView_領藥台_03_領藥內容.顯示首行 = false; }
                        if (_sqL_DataGridView_領藥台_04_領藥內容.CustomEnable) { _sqL_DataGridView_領藥台_04_領藥內容.顯示首列 = false; _sqL_DataGridView_領藥台_04_領藥內容.顯示首行 = false; }

                        if (saveConfig.領藥台_01_RowsHeight == 0) saveConfig.領藥台_01_RowsHeight = 80;
                        if (saveConfig.領藥台_02_RowsHeight == 0) saveConfig.領藥台_02_RowsHeight = 80;
                        if (saveConfig.領藥台_03_RowsHeight == 0) saveConfig.領藥台_03_RowsHeight = 80;
                        if (saveConfig.領藥台_04_RowsHeight == 0) saveConfig.領藥台_04_RowsHeight = 80;

                        _sqL_DataGridView_領藥台_01_領藥內容.RowsHeight = saveConfig.領藥台_01_RowsHeight;
                        _sqL_DataGridView_領藥台_02_領藥內容.RowsHeight = saveConfig.領藥台_02_RowsHeight;
                        _sqL_DataGridView_領藥台_03_領藥內容.RowsHeight = saveConfig.領藥台_03_RowsHeight;
                        _sqL_DataGridView_領藥台_04_領藥內容.RowsHeight = saveConfig.領藥台_04_RowsHeight;


                        _sqL_DataGridView_領藥台_01_領藥內容.Init();
                        _sqL_DataGridView_領藥台_02_領藥內容.Init();
                        _sqL_DataGridView_領藥台_03_領藥內容.Init();
                        _sqL_DataGridView_領藥台_04_領藥內容.Init();




                        if (_sqL_DataGridView_領藥台_01_領藥內容.CustomEnable)
                        {
                            this.sqL_DataGridView_領藥台_01_領藥內容.RowPostPaintingEventEx += SqL_DataGridView_領藥內容_RowPostPaintingEventEx;
                            panel_領藥台_01_藥品資訊.Visible = false;
                        }
                        if (_sqL_DataGridView_領藥台_02_領藥內容.CustomEnable)
                        {
                            this.sqL_DataGridView_領藥台_02_領藥內容.RowPostPaintingEventEx += SqL_DataGridView_領藥內容_RowPostPaintingEventEx;
                            panel_領藥台_02_藥品資訊.Visible = false;
                        }
                        if (_sqL_DataGridView_領藥台_03_領藥內容.CustomEnable) this.sqL_DataGridView_領藥台_03_領藥內容.RowPostPaintingEventEx += SqL_DataGridView_領藥內容_RowPostPaintingEventEx;
                        if (_sqL_DataGridView_領藥台_04_領藥內容.CustomEnable) this.sqL_DataGridView_領藥台_04_領藥內容.RowPostPaintingEventEx += SqL_DataGridView_領藥內容_RowPostPaintingEventEx;

                        if (saveConfig.領藥台_01_藥品資訊_Height == 0) saveConfig.領藥台_01_藥品資訊_Height = 275;
                        if (saveConfig.領藥台_02_藥品資訊_Height == 0) saveConfig.領藥台_02_藥品資訊_Height = 275;
                        _panel_領藥台_01_藥品資訊.Height = saveConfig.領藥台_01_藥品資訊_Height;
                        _panel_領藥台_02_藥品資訊.Height = saveConfig.領藥台_02_藥品資訊_Height;
                    }
                

                }));
            }

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

   
        #endregion
    }
}
