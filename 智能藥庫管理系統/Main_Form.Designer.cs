﻿
namespace 智能藥庫管理系統
{
    partial class Main_Form
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.plC_ScreenPage1 = new MyUI.PLC_ScreenPage();
            this.登入畫面 = new System.Windows.Forms.TabPage();
            this.plC_RJ_Button6 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button5 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button4 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button3 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button2 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button1 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_庫存查詢 = new MyUI.PLC_RJ_Button();
            this.功能頁面 = new System.Windows.Forms.TabPage();
            this.系統 = new System.Windows.Forms.TabPage();
            this.plC_UI_Init = new MyUI.PLC_UI_Init();
            this.lowerMachine_Panel = new LadderUI.LowerMachine_Panel();
            this.plC_ScreenPage1.SuspendLayout();
            this.登入畫面.SuspendLayout();
            this.系統.SuspendLayout();
            this.SuspendLayout();
            // 
            // plC_ScreenPage1
            // 
            this.plC_ScreenPage1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage1.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage1.Controls.Add(this.登入畫面);
            this.plC_ScreenPage1.Controls.Add(this.功能頁面);
            this.plC_ScreenPage1.Controls.Add(this.系統);
            this.plC_ScreenPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage1.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage1.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage1.Location = new System.Drawing.Point(0, 0);
            this.plC_ScreenPage1.Name = "plC_ScreenPage1";
            this.plC_ScreenPage1.SelectedIndex = 0;
            this.plC_ScreenPage1.Size = new System.Drawing.Size(1804, 1061);
            this.plC_ScreenPage1.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage1.TabIndex = 0;
            this.plC_ScreenPage1.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage1.顯示頁面 = 0;
            // 
            // 登入畫面
            // 
            this.登入畫面.BackColor = System.Drawing.Color.WhiteSmoke;
            this.登入畫面.Controls.Add(this.plC_RJ_Button6);
            this.登入畫面.Controls.Add(this.plC_RJ_Button5);
            this.登入畫面.Controls.Add(this.plC_RJ_Button4);
            this.登入畫面.Controls.Add(this.plC_RJ_Button3);
            this.登入畫面.Controls.Add(this.plC_RJ_Button2);
            this.登入畫面.Controls.Add(this.plC_RJ_Button1);
            this.登入畫面.Controls.Add(this.plC_RJ_Button_庫存查詢);
            this.登入畫面.Location = new System.Drawing.Point(4, 25);
            this.登入畫面.Name = "登入畫面";
            this.登入畫面.Size = new System.Drawing.Size(1796, 1032);
            this.登入畫面.TabIndex = 0;
            this.登入畫面.Text = "登入畫面";
            // 
            // plC_RJ_Button6
            // 
            this.plC_RJ_Button6.AutoResetState = false;
            this.plC_RJ_Button6.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button6.Bool = false;
            this.plC_RJ_Button6.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button6.BorderRadius = 10;
            this.plC_RJ_Button6.BorderSize = 0;
            this.plC_RJ_Button6.but_press = false;
            this.plC_RJ_Button6.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button6.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button6.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button6.GUID = "";
            this.plC_RJ_Button6.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button6.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button6.Location = new System.Drawing.Point(309, 163);
            this.plC_RJ_Button6.Name = "plC_RJ_Button6";
            this.plC_RJ_Button6.OFF_文字內容 = "儲位管理";
            this.plC_RJ_Button6.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button6.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button6.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button6.ON_BorderSize = 1;
            this.plC_RJ_Button6.ON_文字內容 = "儲位管理";
            this.plC_RJ_Button6.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button6.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button6.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button6.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button6.ProhibitionLineWidth = 4;
            this.plC_RJ_Button6.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button6.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button6.ShadowSize = 3;
            this.plC_RJ_Button6.ShowLoadingForm = false;
            this.plC_RJ_Button6.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_Button6.State = false;
            this.plC_RJ_Button6.TabIndex = 144;
            this.plC_RJ_Button6.Text = "儲位管理";
            this.plC_RJ_Button6.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button6.TextHeight = 35;
            this.plC_RJ_Button6.Texts = "儲位管理";
            this.plC_RJ_Button6.UseVisualStyleBackColor = false;
            this.plC_RJ_Button6.字型鎖住 = false;
            this.plC_RJ_Button6.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button6.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button6.文字鎖住 = false;
            this.plC_RJ_Button6.背景圖片 = global::智能藥庫管理系統.Properties.Resources.panel_storage_location_setup;
            this.plC_RJ_Button6.讀取位元反向 = false;
            this.plC_RJ_Button6.讀寫鎖住 = false;
            this.plC_RJ_Button6.音效 = true;
            this.plC_RJ_Button6.顯示 = false;
            this.plC_RJ_Button6.顯示狀態 = false;
            // 
            // plC_RJ_Button5
            // 
            this.plC_RJ_Button5.AutoResetState = false;
            this.plC_RJ_Button5.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button5.Bool = false;
            this.plC_RJ_Button5.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button5.BorderRadius = 10;
            this.plC_RJ_Button5.BorderSize = 0;
            this.plC_RJ_Button5.but_press = false;
            this.plC_RJ_Button5.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button5.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button5.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button5.GUID = "";
            this.plC_RJ_Button5.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button5.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button5.Location = new System.Drawing.Point(793, 17);
            this.plC_RJ_Button5.Name = "plC_RJ_Button5";
            this.plC_RJ_Button5.OFF_文字內容 = "人員資料";
            this.plC_RJ_Button5.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button5.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button5.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button5.ON_BorderSize = 1;
            this.plC_RJ_Button5.ON_文字內容 = "人員資料";
            this.plC_RJ_Button5.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button5.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button5.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button5.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button5.ProhibitionLineWidth = 4;
            this.plC_RJ_Button5.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button5.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button5.ShadowSize = 3;
            this.plC_RJ_Button5.ShowLoadingForm = false;
            this.plC_RJ_Button5.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_Button5.State = false;
            this.plC_RJ_Button5.TabIndex = 143;
            this.plC_RJ_Button5.Text = "人員資料";
            this.plC_RJ_Button5.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button5.TextHeight = 35;
            this.plC_RJ_Button5.Texts = "人員資料";
            this.plC_RJ_Button5.UseVisualStyleBackColor = false;
            this.plC_RJ_Button5.字型鎖住 = false;
            this.plC_RJ_Button5.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button5.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button5.文字鎖住 = false;
            this.plC_RJ_Button5.背景圖片 = global::智能藥庫管理系統.Properties.Resources.staff_big_icon;
            this.plC_RJ_Button5.讀取位元反向 = false;
            this.plC_RJ_Button5.讀寫鎖住 = false;
            this.plC_RJ_Button5.音效 = true;
            this.plC_RJ_Button5.顯示 = false;
            this.plC_RJ_Button5.顯示狀態 = false;
            // 
            // plC_RJ_Button4
            // 
            this.plC_RJ_Button4.AutoResetState = false;
            this.plC_RJ_Button4.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button4.Bool = false;
            this.plC_RJ_Button4.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button4.BorderRadius = 10;
            this.plC_RJ_Button4.BorderSize = 0;
            this.plC_RJ_Button4.but_press = false;
            this.plC_RJ_Button4.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button4.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button4.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button4.GUID = "";
            this.plC_RJ_Button4.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button4.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button4.Location = new System.Drawing.Point(648, 17);
            this.plC_RJ_Button4.Name = "plC_RJ_Button4";
            this.plC_RJ_Button4.OFF_文字內容 = "驗收管理";
            this.plC_RJ_Button4.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button4.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button4.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button4.ON_BorderSize = 1;
            this.plC_RJ_Button4.ON_文字內容 = "驗收管理";
            this.plC_RJ_Button4.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button4.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button4.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button4.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button4.ProhibitionLineWidth = 4;
            this.plC_RJ_Button4.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button4.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button4.ShadowSize = 3;
            this.plC_RJ_Button4.ShowLoadingForm = false;
            this.plC_RJ_Button4.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_Button4.State = false;
            this.plC_RJ_Button4.TabIndex = 142;
            this.plC_RJ_Button4.Text = "驗收管理";
            this.plC_RJ_Button4.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button4.TextHeight = 35;
            this.plC_RJ_Button4.Texts = "驗收管理";
            this.plC_RJ_Button4.UseVisualStyleBackColor = false;
            this.plC_RJ_Button4.字型鎖住 = false;
            this.plC_RJ_Button4.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button4.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button4.文字鎖住 = false;
            this.plC_RJ_Button4.背景圖片 = global::智能藥庫管理系統.Properties.Resources.order_receipt_management;
            this.plC_RJ_Button4.讀取位元反向 = false;
            this.plC_RJ_Button4.讀寫鎖住 = false;
            this.plC_RJ_Button4.音效 = true;
            this.plC_RJ_Button4.顯示 = false;
            this.plC_RJ_Button4.顯示狀態 = false;
            // 
            // plC_RJ_Button3
            // 
            this.plC_RJ_Button3.AutoResetState = false;
            this.plC_RJ_Button3.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button3.Bool = false;
            this.plC_RJ_Button3.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button3.BorderRadius = 10;
            this.plC_RJ_Button3.BorderSize = 0;
            this.plC_RJ_Button3.but_press = false;
            this.plC_RJ_Button3.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button3.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button3.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button3.GUID = "";
            this.plC_RJ_Button3.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button3.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button3.Location = new System.Drawing.Point(503, 17);
            this.plC_RJ_Button3.Name = "plC_RJ_Button3";
            this.plC_RJ_Button3.OFF_文字內容 = "盤點管理";
            this.plC_RJ_Button3.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button3.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button3.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button3.ON_BorderSize = 1;
            this.plC_RJ_Button3.ON_文字內容 = "盤點管理";
            this.plC_RJ_Button3.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button3.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button3.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button3.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button3.ProhibitionLineWidth = 4;
            this.plC_RJ_Button3.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button3.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button3.ShadowSize = 3;
            this.plC_RJ_Button3.ShowLoadingForm = false;
            this.plC_RJ_Button3.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_Button3.State = false;
            this.plC_RJ_Button3.TabIndex = 141;
            this.plC_RJ_Button3.Text = "盤點管理";
            this.plC_RJ_Button3.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button3.TextHeight = 35;
            this.plC_RJ_Button3.Texts = "盤點管理";
            this.plC_RJ_Button3.UseVisualStyleBackColor = false;
            this.plC_RJ_Button3.字型鎖住 = false;
            this.plC_RJ_Button3.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button3.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button3.文字鎖住 = false;
            this.plC_RJ_Button3.背景圖片 = global::智能藥庫管理系統.Properties.Resources.inventory_management;
            this.plC_RJ_Button3.讀取位元反向 = false;
            this.plC_RJ_Button3.讀寫鎖住 = false;
            this.plC_RJ_Button3.音效 = true;
            this.plC_RJ_Button3.顯示 = false;
            this.plC_RJ_Button3.顯示狀態 = false;
            // 
            // plC_RJ_Button2
            // 
            this.plC_RJ_Button2.AutoResetState = false;
            this.plC_RJ_Button2.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button2.Bool = false;
            this.plC_RJ_Button2.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button2.BorderRadius = 10;
            this.plC_RJ_Button2.BorderSize = 0;
            this.plC_RJ_Button2.but_press = false;
            this.plC_RJ_Button2.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button2.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button2.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button2.GUID = "";
            this.plC_RJ_Button2.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button2.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button2.Location = new System.Drawing.Point(309, 17);
            this.plC_RJ_Button2.Name = "plC_RJ_Button2";
            this.plC_RJ_Button2.OFF_文字內容 = "交易紀錄";
            this.plC_RJ_Button2.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button2.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button2.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button2.ON_BorderSize = 1;
            this.plC_RJ_Button2.ON_文字內容 = "交易紀錄";
            this.plC_RJ_Button2.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button2.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button2.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button2.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button2.ProhibitionLineWidth = 4;
            this.plC_RJ_Button2.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button2.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button2.ShadowSize = 3;
            this.plC_RJ_Button2.ShowLoadingForm = false;
            this.plC_RJ_Button2.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_Button2.State = false;
            this.plC_RJ_Button2.TabIndex = 140;
            this.plC_RJ_Button2.Text = "交易紀錄";
            this.plC_RJ_Button2.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button2.TextHeight = 35;
            this.plC_RJ_Button2.Texts = "交易紀錄";
            this.plC_RJ_Button2.UseVisualStyleBackColor = false;
            this.plC_RJ_Button2.字型鎖住 = false;
            this.plC_RJ_Button2.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button2.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button2.文字鎖住 = false;
            this.plC_RJ_Button2.背景圖片 = global::智能藥庫管理系統.Properties.Resources.transaction_records;
            this.plC_RJ_Button2.讀取位元反向 = false;
            this.plC_RJ_Button2.讀寫鎖住 = false;
            this.plC_RJ_Button2.音效 = true;
            this.plC_RJ_Button2.顯示 = false;
            this.plC_RJ_Button2.顯示狀態 = false;
            // 
            // plC_RJ_Button1
            // 
            this.plC_RJ_Button1.AutoResetState = false;
            this.plC_RJ_Button1.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button1.Bool = false;
            this.plC_RJ_Button1.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button1.BorderRadius = 10;
            this.plC_RJ_Button1.BorderSize = 0;
            this.plC_RJ_Button1.but_press = false;
            this.plC_RJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button1.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button1.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button1.GUID = "";
            this.plC_RJ_Button1.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button1.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button1.Location = new System.Drawing.Point(164, 17);
            this.plC_RJ_Button1.Name = "plC_RJ_Button1";
            this.plC_RJ_Button1.OFF_文字內容 = "庫存管理";
            this.plC_RJ_Button1.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button1.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button1.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button1.ON_BorderSize = 1;
            this.plC_RJ_Button1.ON_文字內容 = "庫存管理";
            this.plC_RJ_Button1.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button1.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button1.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button1.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button1.ProhibitionLineWidth = 4;
            this.plC_RJ_Button1.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button1.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button1.ShadowSize = 3;
            this.plC_RJ_Button1.ShowLoadingForm = false;
            this.plC_RJ_Button1.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_Button1.State = false;
            this.plC_RJ_Button1.TabIndex = 139;
            this.plC_RJ_Button1.Text = "庫存管理";
            this.plC_RJ_Button1.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button1.TextHeight = 35;
            this.plC_RJ_Button1.Texts = "庫存管理";
            this.plC_RJ_Button1.UseVisualStyleBackColor = false;
            this.plC_RJ_Button1.字型鎖住 = false;
            this.plC_RJ_Button1.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button1.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button1.文字鎖住 = false;
            this.plC_RJ_Button1.背景圖片 = global::智能藥庫管理系統.Properties.Resources.medication_inventory_management;
            this.plC_RJ_Button1.讀取位元反向 = false;
            this.plC_RJ_Button1.讀寫鎖住 = false;
            this.plC_RJ_Button1.音效 = true;
            this.plC_RJ_Button1.顯示 = false;
            this.plC_RJ_Button1.顯示狀態 = false;
            // 
            // plC_RJ_Button_庫存查詢
            // 
            this.plC_RJ_Button_庫存查詢.AutoResetState = false;
            this.plC_RJ_Button_庫存查詢.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_庫存查詢.Bool = false;
            this.plC_RJ_Button_庫存查詢.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_庫存查詢.BorderRadius = 10;
            this.plC_RJ_Button_庫存查詢.BorderSize = 0;
            this.plC_RJ_Button_庫存查詢.but_press = false;
            this.plC_RJ_Button_庫存查詢.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_庫存查詢.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_庫存查詢.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_庫存查詢.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_庫存查詢.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_庫存查詢.GUID = "";
            this.plC_RJ_Button_庫存查詢.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_庫存查詢.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_庫存查詢.Location = new System.Drawing.Point(19, 17);
            this.plC_RJ_Button_庫存查詢.Name = "plC_RJ_Button_庫存查詢";
            this.plC_RJ_Button_庫存查詢.OFF_文字內容 = "庫存查詢";
            this.plC_RJ_Button_庫存查詢.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_庫存查詢.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_庫存查詢.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_庫存查詢.ON_BorderSize = 1;
            this.plC_RJ_Button_庫存查詢.ON_文字內容 = "庫存查詢";
            this.plC_RJ_Button_庫存查詢.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_庫存查詢.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_庫存查詢.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button_庫存查詢.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_庫存查詢.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_庫存查詢.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_庫存查詢.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_庫存查詢.ShadowSize = 3;
            this.plC_RJ_Button_庫存查詢.ShowLoadingForm = false;
            this.plC_RJ_Button_庫存查詢.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_Button_庫存查詢.State = false;
            this.plC_RJ_Button_庫存查詢.TabIndex = 138;
            this.plC_RJ_Button_庫存查詢.Text = "庫存查詢";
            this.plC_RJ_Button_庫存查詢.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_庫存查詢.TextHeight = 35;
            this.plC_RJ_Button_庫存查詢.Texts = "庫存查詢";
            this.plC_RJ_Button_庫存查詢.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_庫存查詢.字型鎖住 = false;
            this.plC_RJ_Button_庫存查詢.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_庫存查詢.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_庫存查詢.文字鎖住 = false;
            this.plC_RJ_Button_庫存查詢.背景圖片 = global::智能藥庫管理系統.Properties.Resources.medication_inventory_inquiry;
            this.plC_RJ_Button_庫存查詢.讀取位元反向 = false;
            this.plC_RJ_Button_庫存查詢.讀寫鎖住 = false;
            this.plC_RJ_Button_庫存查詢.音效 = true;
            this.plC_RJ_Button_庫存查詢.顯示 = false;
            this.plC_RJ_Button_庫存查詢.顯示狀態 = false;
            // 
            // 功能頁面
            // 
            this.功能頁面.BackColor = System.Drawing.Color.White;
            this.功能頁面.Location = new System.Drawing.Point(4, 25);
            this.功能頁面.Name = "功能頁面";
            this.功能頁面.Size = new System.Drawing.Size(1896, 1032);
            this.功能頁面.TabIndex = 1;
            this.功能頁面.Text = "功能頁面";
            // 
            // 系統
            // 
            this.系統.BackColor = System.Drawing.Color.White;
            this.系統.Controls.Add(this.plC_UI_Init);
            this.系統.Controls.Add(this.lowerMachine_Panel);
            this.系統.Location = new System.Drawing.Point(4, 25);
            this.系統.Name = "系統";
            this.系統.Size = new System.Drawing.Size(1896, 1032);
            this.系統.TabIndex = 2;
            this.系統.Text = "系統";
            // 
            // plC_UI_Init
            // 
            this.plC_UI_Init.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plC_UI_Init.Location = new System.Drawing.Point(883, 3);
            this.plC_UI_Init.Name = "plC_UI_Init";
            this.plC_UI_Init.Size = new System.Drawing.Size(72, 25);
            this.plC_UI_Init.TabIndex = 1;
            this.plC_UI_Init.光道視覺元件初始化 = false;
            this.plC_UI_Init.全螢幕顯示 = false;
            this.plC_UI_Init.掃描速度 = 100;
            this.plC_UI_Init.起始畫面標題內容 = "鴻森整合機電有限公司";
            this.plC_UI_Init.起始畫面標題字體 = new System.Drawing.Font("標楷體", 20F, System.Drawing.FontStyle.Bold);
            this.plC_UI_Init.起始畫面背景 = ((System.Drawing.Image)(resources.GetObject("plC_UI_Init.起始畫面背景")));
            this.plC_UI_Init.起始畫面顯示 = false;
            this.plC_UI_Init.邁得威視元件初始化 = false;
            this.plC_UI_Init.開機延遲 = 0;
            this.plC_UI_Init.音效 = false;
            // 
            // lowerMachine_Panel
            // 
            this.lowerMachine_Panel.Location = new System.Drawing.Point(8, 3);
            this.lowerMachine_Panel.Name = "lowerMachine_Panel";
            this.lowerMachine_Panel.Size = new System.Drawing.Size(869, 565);
            this.lowerMachine_Panel.TabIndex = 0;
            this.lowerMachine_Panel.掃描速度 = 100;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1804, 1061);
            this.Controls.Add(this.plC_ScreenPage1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能藥庫管理系統";
            this.plC_ScreenPage1.ResumeLayout(false);
            this.登入畫面.ResumeLayout(false);
            this.系統.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.PLC_ScreenPage plC_ScreenPage1;
        private System.Windows.Forms.TabPage 登入畫面;
        private System.Windows.Forms.TabPage 功能頁面;
        private System.Windows.Forms.TabPage 系統;
        private MyUI.PLC_UI_Init plC_UI_Init;
        private LadderUI.LowerMachine_Panel lowerMachine_Panel;
        private MyUI.PLC_RJ_Button plC_RJ_Button1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_庫存查詢;
        private MyUI.PLC_RJ_Button plC_RJ_Button2;
        private MyUI.PLC_RJ_Button plC_RJ_Button3;
        private MyUI.PLC_RJ_Button plC_RJ_Button4;
        private MyUI.PLC_RJ_Button plC_RJ_Button5;
        private MyUI.PLC_RJ_Button plC_RJ_Button6;
    }
}

