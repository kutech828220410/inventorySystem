
namespace 智能藥庫系統
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
            this.plC_ScreenPage_main = new MyUI.PLC_ScreenPage();
            this.主畫面 = new System.Windows.Forms.TabPage();
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_MainForm = new System.Windows.Forms.Panel();
            this.sidePanel1 = new MyUI.SidePanel();
            this.plC_RJ_Button7 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button3 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button5 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_驗收管理 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_庫存查詢 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_ScreenButtonEx1 = new MyUI.PLC_RJ_ScreenButtonEx();
            this.plC_RJ_Button1 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button2 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_儲位管理 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_ScreenButtonEx3 = new MyUI.PLC_RJ_ScreenButtonEx();
            this.功能頁面 = new System.Windows.Forms.TabPage();
            this.系統 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.plC_RJ_ScreenButtonEx_主畫面 = new MyUI.PLC_RJ_ScreenButtonEx();
            this.lowerMachine_Panel = new LadderUI.LowerMachine_Panel();
            this.plC_UI_Init = new MyUI.PLC_UI_Init();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.storageUI_EPD_266 = new H_Pannel_lib.StorageUI_EPD_266();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rfiD_UI = new H_Pannel_lib.RFID_UI();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.sqL_DataGridView_藥品區域 = new SQLUI.SQL_DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rJ_TextBox_藥品區域_IP = new MyUI.RJ_TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rJ_TextBox_藥品區域_Port = new MyUI.RJ_TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.rJ_TextBox_藥品區域_PIN_Num = new MyUI.RJ_TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rJ_TextBox_藥品區域_名稱 = new MyUI.RJ_TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rJ_Button_藥品區域_新增 = new MyUI.RJ_Button();
            this.rJ_Button_藥品區域_刪除 = new MyUI.RJ_Button();
            this.rJ_Button_藥品區域_更新 = new MyUI.RJ_Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sqL_DataGridView_堆疊母資料 = new SQLUI.SQL_DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sqL_DataGridView_堆疊子資料 = new SQLUI.SQL_DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sqL_DataGridView_交易記錄查詢 = new SQLUI.SQL_DataGridView();
            this.plC_ScreenPage_main.SuspendLayout();
            this.主畫面.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.sidePanel1.ContentsPanel.SuspendLayout();
            this.sidePanel1.SuspendLayout();
            this.系統.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // plC_ScreenPage_main
            // 
            this.plC_ScreenPage_main.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage_main.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_main.Controls.Add(this.主畫面);
            this.plC_ScreenPage_main.Controls.Add(this.功能頁面);
            this.plC_ScreenPage_main.Controls.Add(this.系統);
            this.plC_ScreenPage_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage_main.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage_main.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage_main.Location = new System.Drawing.Point(0, 0);
            this.plC_ScreenPage_main.Name = "plC_ScreenPage_main";
            this.plC_ScreenPage_main.SelectedIndex = 0;
            this.plC_ScreenPage_main.Size = new System.Drawing.Size(1904, 1061);
            this.plC_ScreenPage_main.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_main.TabIndex = 0;
            this.plC_ScreenPage_main.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_main.顯示頁面 = 0;
            // 
            // 主畫面
            // 
            this.主畫面.BackColor = System.Drawing.Color.WhiteSmoke;
            this.主畫面.Controls.Add(this.panel_main);
            this.主畫面.Location = new System.Drawing.Point(4, 25);
            this.主畫面.Name = "主畫面";
            this.主畫面.Size = new System.Drawing.Size(1896, 1032);
            this.主畫面.TabIndex = 0;
            this.主畫面.Text = "主畫面";
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.panel_MainForm);
            this.panel_main.Controls.Add(this.sidePanel1);
            this.panel_main.Controls.Add(this.plC_RJ_ScreenButtonEx3);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1896, 1032);
            this.panel_main.TabIndex = 164;
            // 
            // panel_MainForm
            // 
            this.panel_MainForm.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_MainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MainForm.Location = new System.Drawing.Point(276, 0);
            this.panel_MainForm.Name = "panel_MainForm";
            this.panel_MainForm.Padding = new System.Windows.Forms.Padding(5, 0, 5, 25);
            this.panel_MainForm.Size = new System.Drawing.Size(1620, 1032);
            this.panel_MainForm.TabIndex = 171;
            // 
            // sidePanel1
            // 
            this.sidePanel1.AnimationStep = 30;
            this.sidePanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sidePanel1.CollapsedWidth = 40;
            // 
            // sidePanel1.ContentsPanel
            // 
            this.sidePanel1.ContentsPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sidePanel1.ContentsPanel.BackgroundColor = System.Drawing.Color.Transparent;
            this.sidePanel1.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.sidePanel1.ContentsPanel.BorderRadius = 5;
            this.sidePanel1.ContentsPanel.BorderSize = 0;
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button7);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button3);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button5);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button_驗收管理);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button_庫存查詢);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_ScreenButtonEx1);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button1);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button2);
            this.sidePanel1.ContentsPanel.Controls.Add(this.plC_RJ_Button_儲位管理);
            this.sidePanel1.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.sidePanel1.ContentsPanel.IsSelected = false;
            this.sidePanel1.ContentsPanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel1.ContentsPanel.Name = "ContentsPanel";
            this.sidePanel1.ContentsPanel.ShadowColor = System.Drawing.Color.Black;
            this.sidePanel1.ContentsPanel.ShadowSize = 0;
            this.sidePanel1.ContentsPanel.Size = new System.Drawing.Size(276, 1032);
            this.sidePanel1.ContentsPanel.TabIndex = 2;
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel1.ExpandedWidth = 268;
            this.sidePanel1.Location = new System.Drawing.Point(0, 0);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(276, 1032);
            this.sidePanel1.TabIndex = 170;
            // 
            // plC_RJ_Button7
            // 
            this.plC_RJ_Button7.AutoResetState = false;
            this.plC_RJ_Button7.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button7.Bool = false;
            this.plC_RJ_Button7.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button7.BorderRadius = 10;
            this.plC_RJ_Button7.BorderSize = 1;
            this.plC_RJ_Button7.but_press = false;
            this.plC_RJ_Button7.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button7.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button7.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button7.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button7.GUID = "";
            this.plC_RJ_Button7.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button7.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button7.Location = new System.Drawing.Point(121, 369);
            this.plC_RJ_Button7.Name = "plC_RJ_Button7";
            this.plC_RJ_Button7.OFF_文字內容 = "撥補";
            this.plC_RJ_Button7.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button7.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button7.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button7.ON_BorderSize = 1;
            this.plC_RJ_Button7.ON_文字內容 = "撥補";
            this.plC_RJ_Button7.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button7.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button7.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button7.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button7.ProhibitionLineWidth = 4;
            this.plC_RJ_Button7.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button7.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button7.ShadowSize = 3;
            this.plC_RJ_Button7.ShowLoadingForm = false;
            this.plC_RJ_Button7.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button7.State = false;
            this.plC_RJ_Button7.TabIndex = 170;
            this.plC_RJ_Button7.Text = "撥補";
            this.plC_RJ_Button7.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button7.TextHeight = 35;
            this.plC_RJ_Button7.Texts = "撥補";
            this.plC_RJ_Button7.UseVisualStyleBackColor = false;
            this.plC_RJ_Button7.字型鎖住 = false;
            this.plC_RJ_Button7.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button7.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button7.文字鎖住 = false;
            this.plC_RJ_Button7.背景圖片 = global::智能藥庫系統.Properties.Resources.transaction_records;
            this.plC_RJ_Button7.讀取位元反向 = false;
            this.plC_RJ_Button7.讀寫鎖住 = false;
            this.plC_RJ_Button7.音效 = true;
            this.plC_RJ_Button7.顯示 = false;
            this.plC_RJ_Button7.顯示狀態 = false;
            // 
            // plC_RJ_Button3
            // 
            this.plC_RJ_Button3.AutoResetState = false;
            this.plC_RJ_Button3.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button3.Bool = false;
            this.plC_RJ_Button3.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button3.BorderRadius = 10;
            this.plC_RJ_Button3.BorderSize = 1;
            this.plC_RJ_Button3.but_press = false;
            this.plC_RJ_Button3.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button3.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button3.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button3.GUID = "";
            this.plC_RJ_Button3.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button3.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button3.Location = new System.Drawing.Point(121, 247);
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
            this.plC_RJ_Button3.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button3.State = false;
            this.plC_RJ_Button3.TabIndex = 169;
            this.plC_RJ_Button3.Text = "盤點管理";
            this.plC_RJ_Button3.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button3.TextHeight = 35;
            this.plC_RJ_Button3.Texts = "盤點管理";
            this.plC_RJ_Button3.UseVisualStyleBackColor = false;
            this.plC_RJ_Button3.字型鎖住 = false;
            this.plC_RJ_Button3.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button3.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button3.文字鎖住 = false;
            this.plC_RJ_Button3.背景圖片 = global::智能藥庫系統.Properties.Resources.inventory_management;
            this.plC_RJ_Button3.讀取位元反向 = false;
            this.plC_RJ_Button3.讀寫鎖住 = false;
            this.plC_RJ_Button3.音效 = true;
            this.plC_RJ_Button3.顯示 = false;
            this.plC_RJ_Button3.顯示狀態 = false;
            // 
            // plC_RJ_Button5
            // 
            this.plC_RJ_Button5.AutoResetState = false;
            this.plC_RJ_Button5.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button5.Bool = false;
            this.plC_RJ_Button5.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button5.BorderRadius = 10;
            this.plC_RJ_Button5.BorderSize = 1;
            this.plC_RJ_Button5.but_press = false;
            this.plC_RJ_Button5.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button5.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button5.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button5.GUID = "";
            this.plC_RJ_Button5.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button5.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button5.Location = new System.Drawing.Point(3, 369);
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
            this.plC_RJ_Button5.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button5.State = false;
            this.plC_RJ_Button5.TabIndex = 172;
            this.plC_RJ_Button5.Text = "人員資料";
            this.plC_RJ_Button5.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button5.TextHeight = 35;
            this.plC_RJ_Button5.Texts = "人員資料";
            this.plC_RJ_Button5.UseVisualStyleBackColor = false;
            this.plC_RJ_Button5.字型鎖住 = false;
            this.plC_RJ_Button5.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button5.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button5.文字鎖住 = false;
            this.plC_RJ_Button5.背景圖片 = global::智能藥庫系統.Properties.Resources.staff_big_icon;
            this.plC_RJ_Button5.致能讀取位置 = "M8001";
            this.plC_RJ_Button5.讀取位元反向 = false;
            this.plC_RJ_Button5.讀寫鎖住 = false;
            this.plC_RJ_Button5.音效 = true;
            this.plC_RJ_Button5.顯示 = false;
            this.plC_RJ_Button5.顯示狀態 = false;
            // 
            // plC_RJ_Button_驗收管理
            // 
            this.plC_RJ_Button_驗收管理.AutoResetState = false;
            this.plC_RJ_Button_驗收管理.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_驗收管理.Bool = false;
            this.plC_RJ_Button_驗收管理.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_驗收管理.BorderRadius = 10;
            this.plC_RJ_Button_驗收管理.BorderSize = 1;
            this.plC_RJ_Button_驗收管理.but_press = false;
            this.plC_RJ_Button_驗收管理.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_驗收管理.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_驗收管理.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_驗收管理.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_驗收管理.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_驗收管理.GUID = "";
            this.plC_RJ_Button_驗收管理.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_驗收管理.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_驗收管理.Location = new System.Drawing.Point(3, 247);
            this.plC_RJ_Button_驗收管理.Name = "plC_RJ_Button_驗收管理";
            this.plC_RJ_Button_驗收管理.OFF_文字內容 = "驗收管理";
            this.plC_RJ_Button_驗收管理.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_驗收管理.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_驗收管理.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_驗收管理.ON_BorderSize = 1;
            this.plC_RJ_Button_驗收管理.ON_文字內容 = "驗收管理";
            this.plC_RJ_Button_驗收管理.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_驗收管理.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_驗收管理.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button_驗收管理.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_驗收管理.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_驗收管理.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_驗收管理.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_驗收管理.ShadowSize = 3;
            this.plC_RJ_Button_驗收管理.ShowLoadingForm = false;
            this.plC_RJ_Button_驗收管理.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button_驗收管理.State = false;
            this.plC_RJ_Button_驗收管理.TabIndex = 171;
            this.plC_RJ_Button_驗收管理.Text = "驗收管理";
            this.plC_RJ_Button_驗收管理.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_驗收管理.TextHeight = 35;
            this.plC_RJ_Button_驗收管理.Texts = "驗收管理";
            this.plC_RJ_Button_驗收管理.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_驗收管理.字型鎖住 = false;
            this.plC_RJ_Button_驗收管理.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_驗收管理.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_驗收管理.文字鎖住 = false;
            this.plC_RJ_Button_驗收管理.背景圖片 = global::智能藥庫系統.Properties.Resources.order_receipt_management;
            this.plC_RJ_Button_驗收管理.讀取位元反向 = false;
            this.plC_RJ_Button_驗收管理.讀寫鎖住 = false;
            this.plC_RJ_Button_驗收管理.音效 = true;
            this.plC_RJ_Button_驗收管理.顯示 = false;
            this.plC_RJ_Button_驗收管理.顯示狀態 = false;
            // 
            // plC_RJ_Button_庫存查詢
            // 
            this.plC_RJ_Button_庫存查詢.AutoResetState = false;
            this.plC_RJ_Button_庫存查詢.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_庫存查詢.Bool = false;
            this.plC_RJ_Button_庫存查詢.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_庫存查詢.BorderRadius = 10;
            this.plC_RJ_Button_庫存查詢.BorderSize = 1;
            this.plC_RJ_Button_庫存查詢.but_press = false;
            this.plC_RJ_Button_庫存查詢.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_庫存查詢.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_庫存查詢.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_庫存查詢.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_庫存查詢.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_庫存查詢.GUID = "";
            this.plC_RJ_Button_庫存查詢.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_庫存查詢.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_庫存查詢.Location = new System.Drawing.Point(3, 3);
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
            this.plC_RJ_Button_庫存查詢.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button_庫存查詢.State = false;
            this.plC_RJ_Button_庫存查詢.TabIndex = 166;
            this.plC_RJ_Button_庫存查詢.Text = "庫存查詢";
            this.plC_RJ_Button_庫存查詢.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_庫存查詢.TextHeight = 35;
            this.plC_RJ_Button_庫存查詢.Texts = "庫存查詢";
            this.plC_RJ_Button_庫存查詢.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_庫存查詢.字型鎖住 = false;
            this.plC_RJ_Button_庫存查詢.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_庫存查詢.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_庫存查詢.文字鎖住 = false;
            this.plC_RJ_Button_庫存查詢.背景圖片 = global::智能藥庫系統.Properties.Resources.medication_inventory_inquiry;
            this.plC_RJ_Button_庫存查詢.讀取位元反向 = false;
            this.plC_RJ_Button_庫存查詢.讀寫鎖住 = false;
            this.plC_RJ_Button_庫存查詢.音效 = true;
            this.plC_RJ_Button_庫存查詢.顯示 = false;
            this.plC_RJ_Button_庫存查詢.顯示狀態 = false;
            // 
            // plC_RJ_ScreenButtonEx1
            // 
            this.plC_RJ_ScreenButtonEx1.AutoResetState = false;
            this.plC_RJ_ScreenButtonEx1.BackColor = System.Drawing.Color.Transparent;
            this.plC_RJ_ScreenButtonEx1.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.BackgroundImage = global::智能藥庫系統.Properties.Resources.system_gear_removebg_preview;
            this.plC_RJ_ScreenButtonEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.plC_RJ_ScreenButtonEx1.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx1.BorderRadius = 10;
            this.plC_RJ_ScreenButtonEx1.BorderSize = 1;
            this.plC_RJ_ScreenButtonEx1.but_press = false;
            this.plC_RJ_ScreenButtonEx1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.plC_RJ_ScreenButtonEx1.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_ScreenButtonEx1.FlatAppearance.BorderSize = 0;
            this.plC_RJ_ScreenButtonEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_ScreenButtonEx1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx1.ForeColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx1.GUID = "";
            this.plC_RJ_ScreenButtonEx1.Image = global::智能藥庫系統.Properties.Resources.login_page_removebg_preview;
            this.plC_RJ_ScreenButtonEx1.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_ScreenButtonEx1.Location = new System.Drawing.Point(3, 491);
            this.plC_RJ_ScreenButtonEx1.Name = "plC_RJ_ScreenButtonEx1";
            this.plC_RJ_ScreenButtonEx1.OffFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx1.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.OffIconColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.OffText = "iConText";
            this.plC_RJ_ScreenButtonEx1.OnFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx1.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.OnIconColor = System.Drawing.Color.LemonChiffon;
            this.plC_RJ_ScreenButtonEx1.OnText = "iConText";
            this.plC_RJ_ScreenButtonEx1.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_ScreenButtonEx1.ProhibitionLineWidth = 4;
            this.plC_RJ_ScreenButtonEx1.ProhibitionSymbolSize = 25;
            this.plC_RJ_ScreenButtonEx1.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_ScreenButtonEx1.ShadowSize = 3;
            this.plC_RJ_ScreenButtonEx1.ShowLoadingForm = false;
            this.plC_RJ_ScreenButtonEx1.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_ScreenButtonEx1.State = false;
            this.plC_RJ_ScreenButtonEx1.TabIndex = 174;
            this.plC_RJ_ScreenButtonEx1.Text = "系統";
            this.plC_RJ_ScreenButtonEx1.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx1.TextHeight = 35;
            this.plC_RJ_ScreenButtonEx1.UseVisualStyleBackColor = false;
            this.plC_RJ_ScreenButtonEx1.字元長度 = MyUI.PLC_RJ_ScreenButtonEx.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButtonEx1.寫入位置註解 = "";
            this.plC_RJ_ScreenButtonEx1.寫入元件位置 = "";
            this.plC_RJ_ScreenButtonEx1.按鈕型態 = MyUI.PLC_RJ_ScreenButtonEx.StatusEnum.保持型;
            this.plC_RJ_ScreenButtonEx1.控制位址 = "D0";
            this.plC_RJ_ScreenButtonEx1.換頁選擇方式 = MyUI.PLC_RJ_ScreenButtonEx.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButtonEx1.致能讀取位置 = "";
            this.plC_RJ_ScreenButtonEx1.讀取位元反向 = false;
            this.plC_RJ_ScreenButtonEx1.讀取位置註解 = "";
            this.plC_RJ_ScreenButtonEx1.讀取元件位置 = "";
            this.plC_RJ_ScreenButtonEx1.音效 = false;
            this.plC_RJ_ScreenButtonEx1.頁面名稱 = "系統";
            this.plC_RJ_ScreenButtonEx1.頁面編號 = 0;
            this.plC_RJ_ScreenButtonEx1.顯示方式 = MyUI.PLC_RJ_ScreenButtonEx.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButtonEx1.顯示狀態 = false;
            this.plC_RJ_ScreenButtonEx1.顯示讀取位置 = "";
            // 
            // plC_RJ_Button1
            // 
            this.plC_RJ_Button1.AutoResetState = false;
            this.plC_RJ_Button1.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button1.Bool = false;
            this.plC_RJ_Button1.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button1.BorderRadius = 10;
            this.plC_RJ_Button1.BorderSize = 1;
            this.plC_RJ_Button1.but_press = false;
            this.plC_RJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button1.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button1.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button1.GUID = "";
            this.plC_RJ_Button1.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button1.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button1.Location = new System.Drawing.Point(121, 3);
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
            this.plC_RJ_Button1.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button1.State = false;
            this.plC_RJ_Button1.TabIndex = 167;
            this.plC_RJ_Button1.Text = "庫存管理";
            this.plC_RJ_Button1.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button1.TextHeight = 35;
            this.plC_RJ_Button1.Texts = "庫存管理";
            this.plC_RJ_Button1.UseVisualStyleBackColor = false;
            this.plC_RJ_Button1.字型鎖住 = false;
            this.plC_RJ_Button1.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button1.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button1.文字鎖住 = false;
            this.plC_RJ_Button1.背景圖片 = global::智能藥庫系統.Properties.Resources.medication_inventory_management;
            this.plC_RJ_Button1.讀取位元反向 = false;
            this.plC_RJ_Button1.讀寫鎖住 = false;
            this.plC_RJ_Button1.音效 = true;
            this.plC_RJ_Button1.顯示 = false;
            this.plC_RJ_Button1.顯示狀態 = false;
            // 
            // plC_RJ_Button2
            // 
            this.plC_RJ_Button2.AutoResetState = false;
            this.plC_RJ_Button2.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button2.Bool = false;
            this.plC_RJ_Button2.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button2.BorderRadius = 10;
            this.plC_RJ_Button2.BorderSize = 1;
            this.plC_RJ_Button2.but_press = false;
            this.plC_RJ_Button2.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button2.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button2.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button2.GUID = "";
            this.plC_RJ_Button2.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button2.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button2.Location = new System.Drawing.Point(3, 125);
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
            this.plC_RJ_Button2.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button2.State = false;
            this.plC_RJ_Button2.TabIndex = 168;
            this.plC_RJ_Button2.Text = "交易紀錄";
            this.plC_RJ_Button2.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button2.TextHeight = 35;
            this.plC_RJ_Button2.Texts = "交易紀錄";
            this.plC_RJ_Button2.UseVisualStyleBackColor = false;
            this.plC_RJ_Button2.字型鎖住 = false;
            this.plC_RJ_Button2.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button2.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button2.文字鎖住 = false;
            this.plC_RJ_Button2.背景圖片 = global::智能藥庫系統.Properties.Resources.transaction_records;
            this.plC_RJ_Button2.讀取位元反向 = false;
            this.plC_RJ_Button2.讀寫鎖住 = false;
            this.plC_RJ_Button2.音效 = true;
            this.plC_RJ_Button2.顯示 = false;
            this.plC_RJ_Button2.顯示狀態 = false;
            // 
            // plC_RJ_Button_儲位管理
            // 
            this.plC_RJ_Button_儲位管理.AutoResetState = false;
            this.plC_RJ_Button_儲位管理.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_儲位管理.Bool = false;
            this.plC_RJ_Button_儲位管理.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_儲位管理.BorderRadius = 10;
            this.plC_RJ_Button_儲位管理.BorderSize = 1;
            this.plC_RJ_Button_儲位管理.but_press = false;
            this.plC_RJ_Button_儲位管理.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_儲位管理.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_儲位管理.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_儲位管理.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_儲位管理.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_儲位管理.GUID = "";
            this.plC_RJ_Button_儲位管理.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_儲位管理.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_儲位管理.Location = new System.Drawing.Point(121, 125);
            this.plC_RJ_Button_儲位管理.Name = "plC_RJ_Button_儲位管理";
            this.plC_RJ_Button_儲位管理.OFF_文字內容 = "儲位管理";
            this.plC_RJ_Button_儲位管理.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_儲位管理.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_儲位管理.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_儲位管理.ON_BorderSize = 1;
            this.plC_RJ_Button_儲位管理.ON_文字內容 = "儲位管理";
            this.plC_RJ_Button_儲位管理.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_儲位管理.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_儲位管理.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button_儲位管理.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_儲位管理.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_儲位管理.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_儲位管理.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_儲位管理.ShadowSize = 3;
            this.plC_RJ_Button_儲位管理.ShowLoadingForm = false;
            this.plC_RJ_Button_儲位管理.Size = new System.Drawing.Size(112, 116);
            this.plC_RJ_Button_儲位管理.State = false;
            this.plC_RJ_Button_儲位管理.TabIndex = 173;
            this.plC_RJ_Button_儲位管理.Text = "儲位管理";
            this.plC_RJ_Button_儲位管理.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_儲位管理.TextHeight = 35;
            this.plC_RJ_Button_儲位管理.Texts = "儲位管理";
            this.plC_RJ_Button_儲位管理.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_儲位管理.字型鎖住 = false;
            this.plC_RJ_Button_儲位管理.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_儲位管理.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_儲位管理.文字鎖住 = false;
            this.plC_RJ_Button_儲位管理.背景圖片 = global::智能藥庫系統.Properties.Resources.panel_storage_location_setup;
            this.plC_RJ_Button_儲位管理.讀取位元反向 = false;
            this.plC_RJ_Button_儲位管理.讀寫鎖住 = false;
            this.plC_RJ_Button_儲位管理.音效 = true;
            this.plC_RJ_Button_儲位管理.顯示 = false;
            this.plC_RJ_Button_儲位管理.顯示狀態 = false;
            // 
            // plC_RJ_ScreenButtonEx3
            // 
            this.plC_RJ_ScreenButtonEx3.AutoResetState = false;
            this.plC_RJ_ScreenButtonEx3.BackColor = System.Drawing.Color.Transparent;
            this.plC_RJ_ScreenButtonEx3.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx3.BackgroundImage = global::智能藥庫系統.Properties.Resources.system_gear_removebg_preview;
            this.plC_RJ_ScreenButtonEx3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.plC_RJ_ScreenButtonEx3.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx3.BorderRadius = 10;
            this.plC_RJ_ScreenButtonEx3.BorderSize = 1;
            this.plC_RJ_ScreenButtonEx3.but_press = false;
            this.plC_RJ_ScreenButtonEx3.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.plC_RJ_ScreenButtonEx3.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_ScreenButtonEx3.FlatAppearance.BorderSize = 0;
            this.plC_RJ_ScreenButtonEx3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_ScreenButtonEx3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx3.ForeColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx3.GUID = "";
            this.plC_RJ_ScreenButtonEx3.Image = global::智能藥庫系統.Properties.Resources.system_gear_removebg_preview;
            this.plC_RJ_ScreenButtonEx3.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_ScreenButtonEx3.Location = new System.Drawing.Point(1502, 3);
            this.plC_RJ_ScreenButtonEx3.Name = "plC_RJ_ScreenButtonEx3";
            this.plC_RJ_ScreenButtonEx3.OffFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx3.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx3.OffIconColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx3.OffText = "iConText";
            this.plC_RJ_ScreenButtonEx3.OnFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx3.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx3.OnIconColor = System.Drawing.Color.LemonChiffon;
            this.plC_RJ_ScreenButtonEx3.OnText = "iConText";
            this.plC_RJ_ScreenButtonEx3.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_ScreenButtonEx3.ProhibitionLineWidth = 4;
            this.plC_RJ_ScreenButtonEx3.ProhibitionSymbolSize = 25;
            this.plC_RJ_ScreenButtonEx3.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_ScreenButtonEx3.ShadowSize = 3;
            this.plC_RJ_ScreenButtonEx3.ShowLoadingForm = false;
            this.plC_RJ_ScreenButtonEx3.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_ScreenButtonEx3.State = false;
            this.plC_RJ_ScreenButtonEx3.TabIndex = 163;
            this.plC_RJ_ScreenButtonEx3.Text = "系統";
            this.plC_RJ_ScreenButtonEx3.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx3.TextHeight = 35;
            this.plC_RJ_ScreenButtonEx3.UseVisualStyleBackColor = false;
            this.plC_RJ_ScreenButtonEx3.字元長度 = MyUI.PLC_RJ_ScreenButtonEx.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButtonEx3.寫入位置註解 = "";
            this.plC_RJ_ScreenButtonEx3.寫入元件位置 = "";
            this.plC_RJ_ScreenButtonEx3.按鈕型態 = MyUI.PLC_RJ_ScreenButtonEx.StatusEnum.保持型;
            this.plC_RJ_ScreenButtonEx3.控制位址 = "D0";
            this.plC_RJ_ScreenButtonEx3.換頁選擇方式 = MyUI.PLC_RJ_ScreenButtonEx.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButtonEx3.致能讀取位置 = "";
            this.plC_RJ_ScreenButtonEx3.讀取位元反向 = false;
            this.plC_RJ_ScreenButtonEx3.讀取位置註解 = "";
            this.plC_RJ_ScreenButtonEx3.讀取元件位置 = "";
            this.plC_RJ_ScreenButtonEx3.音效 = true;
            this.plC_RJ_ScreenButtonEx3.頁面名稱 = "系統";
            this.plC_RJ_ScreenButtonEx3.頁面編號 = 0;
            this.plC_RJ_ScreenButtonEx3.顯示方式 = MyUI.PLC_RJ_ScreenButtonEx.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButtonEx3.顯示狀態 = false;
            this.plC_RJ_ScreenButtonEx3.顯示讀取位置 = "";
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
            this.系統.Controls.Add(this.tabControl1);
            this.系統.Location = new System.Drawing.Point(4, 25);
            this.系統.Name = "系統";
            this.系統.Size = new System.Drawing.Size(1896, 1032);
            this.系統.TabIndex = 2;
            this.系統.Text = "系統";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1896, 1032);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.plC_RJ_ScreenButtonEx_主畫面);
            this.tabPage1.Controls.Add(this.lowerMachine_Panel);
            this.tabPage1.Controls.Add(this.plC_UI_Init);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1888, 1006);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "系統";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // plC_RJ_ScreenButtonEx_主畫面
            // 
            this.plC_RJ_ScreenButtonEx_主畫面.AutoResetState = false;
            this.plC_RJ_ScreenButtonEx_主畫面.BackColor = System.Drawing.Color.Transparent;
            this.plC_RJ_ScreenButtonEx_主畫面.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx_主畫面.BackgroundImage = global::智能藥庫系統.Properties.Resources.login_page_removebg_preview;
            this.plC_RJ_ScreenButtonEx_主畫面.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.plC_RJ_ScreenButtonEx_主畫面.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx_主畫面.BorderRadius = 10;
            this.plC_RJ_ScreenButtonEx_主畫面.BorderSize = 1;
            this.plC_RJ_ScreenButtonEx_主畫面.but_press = false;
            this.plC_RJ_ScreenButtonEx_主畫面.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.plC_RJ_ScreenButtonEx_主畫面.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_ScreenButtonEx_主畫面.FlatAppearance.BorderSize = 0;
            this.plC_RJ_ScreenButtonEx_主畫面.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_ScreenButtonEx_主畫面.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx_主畫面.ForeColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx_主畫面.GUID = "";
            this.plC_RJ_ScreenButtonEx_主畫面.Image = global::智能藥庫系統.Properties.Resources.login_page_removebg_preview;
            this.plC_RJ_ScreenButtonEx_主畫面.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_ScreenButtonEx_主畫面.Location = new System.Drawing.Point(1643, 6);
            this.plC_RJ_ScreenButtonEx_主畫面.Name = "plC_RJ_ScreenButtonEx_主畫面";
            this.plC_RJ_ScreenButtonEx_主畫面.OffFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx_主畫面.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx_主畫面.OffIconColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx_主畫面.OffText = "iConText";
            this.plC_RJ_ScreenButtonEx_主畫面.OnFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButtonEx_主畫面.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx_主畫面.OnIconColor = System.Drawing.Color.LemonChiffon;
            this.plC_RJ_ScreenButtonEx_主畫面.OnText = "iConText";
            this.plC_RJ_ScreenButtonEx_主畫面.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_ScreenButtonEx_主畫面.ProhibitionLineWidth = 4;
            this.plC_RJ_ScreenButtonEx_主畫面.ProhibitionSymbolSize = 25;
            this.plC_RJ_ScreenButtonEx_主畫面.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_ScreenButtonEx_主畫面.ShadowSize = 3;
            this.plC_RJ_ScreenButtonEx_主畫面.ShowLoadingForm = false;
            this.plC_RJ_ScreenButtonEx_主畫面.Size = new System.Drawing.Size(139, 140);
            this.plC_RJ_ScreenButtonEx_主畫面.State = false;
            this.plC_RJ_ScreenButtonEx_主畫面.TabIndex = 164;
            this.plC_RJ_ScreenButtonEx_主畫面.Text = "主畫面";
            this.plC_RJ_ScreenButtonEx_主畫面.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx_主畫面.TextHeight = 35;
            this.plC_RJ_ScreenButtonEx_主畫面.UseVisualStyleBackColor = false;
            this.plC_RJ_ScreenButtonEx_主畫面.字元長度 = MyUI.PLC_RJ_ScreenButtonEx.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButtonEx_主畫面.寫入位置註解 = "";
            this.plC_RJ_ScreenButtonEx_主畫面.寫入元件位置 = "";
            this.plC_RJ_ScreenButtonEx_主畫面.按鈕型態 = MyUI.PLC_RJ_ScreenButtonEx.StatusEnum.保持型;
            this.plC_RJ_ScreenButtonEx_主畫面.控制位址 = "D0";
            this.plC_RJ_ScreenButtonEx_主畫面.換頁選擇方式 = MyUI.PLC_RJ_ScreenButtonEx.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButtonEx_主畫面.致能讀取位置 = "";
            this.plC_RJ_ScreenButtonEx_主畫面.讀取位元反向 = false;
            this.plC_RJ_ScreenButtonEx_主畫面.讀取位置註解 = "";
            this.plC_RJ_ScreenButtonEx_主畫面.讀取元件位置 = "";
            this.plC_RJ_ScreenButtonEx_主畫面.音效 = true;
            this.plC_RJ_ScreenButtonEx_主畫面.頁面名稱 = "主畫面";
            this.plC_RJ_ScreenButtonEx_主畫面.頁面編號 = 0;
            this.plC_RJ_ScreenButtonEx_主畫面.顯示方式 = MyUI.PLC_RJ_ScreenButtonEx.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButtonEx_主畫面.顯示狀態 = false;
            this.plC_RJ_ScreenButtonEx_主畫面.顯示讀取位置 = "";
            // 
            // lowerMachine_Panel
            // 
            this.lowerMachine_Panel.Location = new System.Drawing.Point(6, 6);
            this.lowerMachine_Panel.Name = "lowerMachine_Panel";
            this.lowerMachine_Panel.Size = new System.Drawing.Size(869, 565);
            this.lowerMachine_Panel.TabIndex = 0;
            this.lowerMachine_Panel.掃描速度 = 100;
            // 
            // plC_UI_Init
            // 
            this.plC_UI_Init.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plC_UI_Init.Location = new System.Drawing.Point(881, 6);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.storageUI_EPD_266);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1888, 1006);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "EPD266";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // storageUI_EPD_266
            // 
            this.storageUI_EPD_266._Password = "";
            this.storageUI_EPD_266.DataBaseName = "TEST";
            this.storageUI_EPD_266.DNS = "0.0.0.0";
            this.storageUI_EPD_266.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storageUI_EPD_266.Gateway = "0.0.0.0";
            this.storageUI_EPD_266.IP = "localhost";
            this.storageUI_EPD_266.IP_Adress = "0.0.0.0";
            this.storageUI_EPD_266.Local_Port = "0";
            this.storageUI_EPD_266.Location = new System.Drawing.Point(3, 3);
            this.storageUI_EPD_266.Name = "storageUI_EPD_266";
            this.storageUI_EPD_266.Password = "user82822040";
            this.storageUI_EPD_266.Port = ((uint)(3306u));
            this.storageUI_EPD_266.Server_IP_Adress = "0.0.0.0";
            this.storageUI_EPD_266.Server_Port = "0";
            this.storageUI_EPD_266.Size = new System.Drawing.Size(1882, 1000);
            this.storageUI_EPD_266.SSID = "";
            this.storageUI_EPD_266.Station = "0";
            this.storageUI_EPD_266.Subnet = "0.0.0.0";
            this.storageUI_EPD_266.TabIndex = 2;
            this.storageUI_EPD_266.TableName = "EPD266_Jsonstring";
            this.storageUI_EPD_266.UDP_LocalPorts = ((System.Collections.Generic.List<string>)(resources.GetObject("storageUI_EPD_266.UDP_LocalPorts")));
            this.storageUI_EPD_266.UDP_SendTime = "0";
            this.storageUI_EPD_266.UDP_ServerPorts = ((System.Collections.Generic.List<string>)(resources.GetObject("storageUI_EPD_266.UDP_ServerPorts")));
            this.storageUI_EPD_266.UserName = "root";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rfiD_UI);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1888, 1006);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "RFID";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rfiD_UI
            // 
            this.rfiD_UI._Password = "";
            this.rfiD_UI.BackColor = System.Drawing.SystemColors.Window;
            this.rfiD_UI.DataBaseName = "TEST";
            this.rfiD_UI.DNS = "0.0.0.0";
            this.rfiD_UI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rfiD_UI.Gateway = "0.0.0.0";
            this.rfiD_UI.IP = "localhost";
            this.rfiD_UI.IP_Adress = "0.0.0.0";
            this.rfiD_UI.Local_Port = "0";
            this.rfiD_UI.Location = new System.Drawing.Point(0, 0);
            this.rfiD_UI.Name = "rfiD_UI";
            this.rfiD_UI.Password = "user82822040";
            this.rfiD_UI.Port = ((uint)(3306u));
            this.rfiD_UI.RFID_Enable = "0";
            this.rfiD_UI.Server_IP_Adress = "0.0.0.0";
            this.rfiD_UI.Server_Port = "0";
            this.rfiD_UI.Size = new System.Drawing.Size(1888, 1006);
            this.rfiD_UI.SSID = "";
            this.rfiD_UI.Station = "0";
            this.rfiD_UI.Subnet = "0.0.0.0";
            this.rfiD_UI.TabIndex = 1;
            this.rfiD_UI.TableName = "RFID_Device_Jsonstring";
            this.rfiD_UI.UDP_LocalPorts = ((System.Collections.Generic.List<string>)(resources.GetObject("rfiD_UI.UDP_LocalPorts")));
            this.rfiD_UI.UDP_ServerPorts = ((System.Collections.Generic.List<string>)(resources.GetObject("rfiD_UI.UDP_ServerPorts")));
            this.rfiD_UI.UserName = "root";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel5);
            this.tabPage4.Controls.Add(this.panel4);
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Controls.Add(this.panel3);
            this.tabPage4.Controls.Add(this.sqL_DataGridView_藥品區域);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1888, 1006);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "藥品區域";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // sqL_DataGridView_藥品區域
            // 
            this.sqL_DataGridView_藥品區域.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥品區域.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品區域.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品區域.BorderRadius = 0;
            this.sqL_DataGridView_藥品區域.BorderSize = 0;
            this.sqL_DataGridView_藥品區域.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_藥品區域.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_藥品區域.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品區域.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品區域.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品區域.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_藥品區域.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥品區域.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品區域.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品區域.columnHeadersHeight = 18;
            this.sqL_DataGridView_藥品區域.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_藥品區域.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_藥品區域.Dock = System.Windows.Forms.DockStyle.Left;
            this.sqL_DataGridView_藥品區域.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品區域.ImageBox = false;
            this.sqL_DataGridView_藥品區域.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_藥品區域.Name = "sqL_DataGridView_藥品區域";
            this.sqL_DataGridView_藥品區域.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥品區域.Password = "user82822040";
            this.sqL_DataGridView_藥品區域.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥品區域.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品區域.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品區域.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品區域.RowsHeight = 20;
            this.sqL_DataGridView_藥品區域.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥品區域.selectedBorderSize = 0;
            this.sqL_DataGridView_藥品區域.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品區域.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品區域.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品區域.Server = "127.0.0.0";
            this.sqL_DataGridView_藥品區域.Size = new System.Drawing.Size(1016, 1006);
            this.sqL_DataGridView_藥品區域.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品區域.TabIndex = 8;
            this.sqL_DataGridView_藥品區域.UserName = "root";
            this.sqL_DataGridView_藥品區域.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_藥品區域.可選擇多列 = false;
            this.sqL_DataGridView_藥品區域.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_藥品區域.自動換行 = true;
            this.sqL_DataGridView_藥品區域.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品區域.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_藥品區域.顯示CheckBox = false;
            this.sqL_DataGridView_藥品區域.顯示首列 = true;
            this.sqL_DataGridView_藥品區域.顯示首行 = true;
            this.sqL_DataGridView_藥品區域.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品區域.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.rJ_TextBox_藥品區域_IP);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(1016, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(872, 88);
            this.panel3.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(36, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP :";
            // 
            // rJ_TextBox_藥品區域_IP
            // 
            this.rJ_TextBox_藥品區域_IP.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥品區域_IP.BorderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_IP.BorderFocusColor = System.Drawing.Color.SteelBlue;
            this.rJ_TextBox_藥品區域_IP.BorderRadius = 5;
            this.rJ_TextBox_藥品區域_IP.BorderSize = 1;
            this.rJ_TextBox_藥品區域_IP.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥品區域_IP.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥品區域_IP.GUID = "";
            this.rJ_TextBox_藥品區域_IP.Location = new System.Drawing.Point(31, 25);
            this.rJ_TextBox_藥品區域_IP.Multiline = false;
            this.rJ_TextBox_藥品區域_IP.Name = "rJ_TextBox_藥品區域_IP";
            this.rJ_TextBox_藥品區域_IP.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥品區域_IP.PassWordChar = false;
            this.rJ_TextBox_藥品區域_IP.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_IP.PlaceholderText = "";
            this.rJ_TextBox_藥品區域_IP.ShowTouchPannel = false;
            this.rJ_TextBox_藥品區域_IP.Size = new System.Drawing.Size(373, 56);
            this.rJ_TextBox_藥品區域_IP.TabIndex = 0;
            this.rJ_TextBox_藥品區域_IP.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥品區域_IP.Texts = "";
            this.rJ_TextBox_藥品區域_IP.UnderlineStyle = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rJ_TextBox_藥品區域_Port);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1016, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 88);
            this.panel1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(36, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port :";
            // 
            // rJ_TextBox_藥品區域_Port
            // 
            this.rJ_TextBox_藥品區域_Port.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥品區域_Port.BorderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_Port.BorderFocusColor = System.Drawing.Color.SteelBlue;
            this.rJ_TextBox_藥品區域_Port.BorderRadius = 5;
            this.rJ_TextBox_藥品區域_Port.BorderSize = 1;
            this.rJ_TextBox_藥品區域_Port.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥品區域_Port.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥品區域_Port.GUID = "";
            this.rJ_TextBox_藥品區域_Port.Location = new System.Drawing.Point(31, 25);
            this.rJ_TextBox_藥品區域_Port.Multiline = false;
            this.rJ_TextBox_藥品區域_Port.Name = "rJ_TextBox_藥品區域_Port";
            this.rJ_TextBox_藥品區域_Port.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥品區域_Port.PassWordChar = false;
            this.rJ_TextBox_藥品區域_Port.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_Port.PlaceholderText = "";
            this.rJ_TextBox_藥品區域_Port.ShowTouchPannel = false;
            this.rJ_TextBox_藥品區域_Port.Size = new System.Drawing.Size(373, 56);
            this.rJ_TextBox_藥品區域_Port.TabIndex = 0;
            this.rJ_TextBox_藥品區域_Port.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥品區域_Port.Texts = "";
            this.rJ_TextBox_藥品區域_Port.UnderlineStyle = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.rJ_TextBox_藥品區域_PIN_Num);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1016, 176);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(872, 88);
            this.panel2.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(36, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "PIN Num :";
            // 
            // rJ_TextBox_藥品區域_PIN_Num
            // 
            this.rJ_TextBox_藥品區域_PIN_Num.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥品區域_PIN_Num.BorderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_PIN_Num.BorderFocusColor = System.Drawing.Color.SteelBlue;
            this.rJ_TextBox_藥品區域_PIN_Num.BorderRadius = 5;
            this.rJ_TextBox_藥品區域_PIN_Num.BorderSize = 1;
            this.rJ_TextBox_藥品區域_PIN_Num.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥品區域_PIN_Num.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥品區域_PIN_Num.GUID = "";
            this.rJ_TextBox_藥品區域_PIN_Num.Location = new System.Drawing.Point(31, 25);
            this.rJ_TextBox_藥品區域_PIN_Num.Multiline = false;
            this.rJ_TextBox_藥品區域_PIN_Num.Name = "rJ_TextBox_藥品區域_PIN_Num";
            this.rJ_TextBox_藥品區域_PIN_Num.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥品區域_PIN_Num.PassWordChar = false;
            this.rJ_TextBox_藥品區域_PIN_Num.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_PIN_Num.PlaceholderText = "";
            this.rJ_TextBox_藥品區域_PIN_Num.ShowTouchPannel = false;
            this.rJ_TextBox_藥品區域_PIN_Num.Size = new System.Drawing.Size(373, 56);
            this.rJ_TextBox_藥品區域_PIN_Num.TabIndex = 0;
            this.rJ_TextBox_藥品區域_PIN_Num.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥品區域_PIN_Num.Texts = "";
            this.rJ_TextBox_藥品區域_PIN_Num.UnderlineStyle = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.rJ_TextBox_藥品區域_名稱);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(1016, 264);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(872, 88);
            this.panel4.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(36, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "名稱 :";
            // 
            // rJ_TextBox_藥品區域_名稱
            // 
            this.rJ_TextBox_藥品區域_名稱.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥品區域_名稱.BorderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_名稱.BorderFocusColor = System.Drawing.Color.SteelBlue;
            this.rJ_TextBox_藥品區域_名稱.BorderRadius = 5;
            this.rJ_TextBox_藥品區域_名稱.BorderSize = 1;
            this.rJ_TextBox_藥品區域_名稱.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥品區域_名稱.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥品區域_名稱.GUID = "";
            this.rJ_TextBox_藥品區域_名稱.Location = new System.Drawing.Point(31, 25);
            this.rJ_TextBox_藥品區域_名稱.Multiline = false;
            this.rJ_TextBox_藥品區域_名稱.Name = "rJ_TextBox_藥品區域_名稱";
            this.rJ_TextBox_藥品區域_名稱.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥品區域_名稱.PassWordChar = false;
            this.rJ_TextBox_藥品區域_名稱.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品區域_名稱.PlaceholderText = "";
            this.rJ_TextBox_藥品區域_名稱.ShowTouchPannel = false;
            this.rJ_TextBox_藥品區域_名稱.Size = new System.Drawing.Size(373, 56);
            this.rJ_TextBox_藥品區域_名稱.TabIndex = 0;
            this.rJ_TextBox_藥品區域_名稱.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥品區域_名稱.Texts = "";
            this.rJ_TextBox_藥品區域_名稱.UnderlineStyle = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rJ_Button_藥品區域_更新);
            this.panel5.Controls.Add(this.rJ_Button_藥品區域_刪除);
            this.panel5.Controls.Add(this.rJ_Button_藥品區域_新增);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(1016, 352);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(872, 88);
            this.panel5.TabIndex = 18;
            // 
            // rJ_Button_藥品區域_新增
            // 
            this.rJ_Button_藥品區域_新增.AutoResetState = false;
            this.rJ_Button_藥品區域_新增.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品區域_新增.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品區域_新增.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品區域_新增.BorderRadius = 20;
            this.rJ_Button_藥品區域_新增.BorderSize = 0;
            this.rJ_Button_藥品區域_新增.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品區域_新增.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品區域_新增.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Button_藥品區域_新增.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品區域_新增.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品區域_新增.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥品區域_新增.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品區域_新增.GUID = "";
            this.rJ_Button_藥品區域_新增.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品區域_新增.Location = new System.Drawing.Point(0, 0);
            this.rJ_Button_藥品區域_新增.Name = "rJ_Button_藥品區域_新增";
            this.rJ_Button_藥品區域_新增.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品區域_新增.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品區域_新增.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品區域_新增.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品區域_新增.ShadowSize = 3;
            this.rJ_Button_藥品區域_新增.ShowLoadingForm = false;
            this.rJ_Button_藥品區域_新增.Size = new System.Drawing.Size(135, 88);
            this.rJ_Button_藥品區域_新增.State = false;
            this.rJ_Button_藥品區域_新增.TabIndex = 34;
            this.rJ_Button_藥品區域_新增.Text = "新增";
            this.rJ_Button_藥品區域_新增.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品區域_新增.TextHeight = 0;
            this.rJ_Button_藥品區域_新增.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_藥品區域_刪除
            // 
            this.rJ_Button_藥品區域_刪除.AutoResetState = false;
            this.rJ_Button_藥品區域_刪除.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品區域_刪除.BackgroundColor = System.Drawing.Color.Red;
            this.rJ_Button_藥品區域_刪除.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品區域_刪除.BorderRadius = 20;
            this.rJ_Button_藥品區域_刪除.BorderSize = 0;
            this.rJ_Button_藥品區域_刪除.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品區域_刪除.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品區域_刪除.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Button_藥品區域_刪除.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品區域_刪除.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品區域_刪除.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥品區域_刪除.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品區域_刪除.GUID = "";
            this.rJ_Button_藥品區域_刪除.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品區域_刪除.Location = new System.Drawing.Point(135, 0);
            this.rJ_Button_藥品區域_刪除.Name = "rJ_Button_藥品區域_刪除";
            this.rJ_Button_藥品區域_刪除.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品區域_刪除.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品區域_刪除.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品區域_刪除.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品區域_刪除.ShadowSize = 3;
            this.rJ_Button_藥品區域_刪除.ShowLoadingForm = false;
            this.rJ_Button_藥品區域_刪除.Size = new System.Drawing.Size(135, 88);
            this.rJ_Button_藥品區域_刪除.State = false;
            this.rJ_Button_藥品區域_刪除.TabIndex = 35;
            this.rJ_Button_藥品區域_刪除.Text = "刪除";
            this.rJ_Button_藥品區域_刪除.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品區域_刪除.TextHeight = 0;
            this.rJ_Button_藥品區域_刪除.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_藥品區域_更新
            // 
            this.rJ_Button_藥品區域_更新.AutoResetState = false;
            this.rJ_Button_藥品區域_更新.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品區域_更新.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品區域_更新.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品區域_更新.BorderRadius = 20;
            this.rJ_Button_藥品區域_更新.BorderSize = 0;
            this.rJ_Button_藥品區域_更新.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品區域_更新.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品區域_更新.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_藥品區域_更新.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品區域_更新.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品區域_更新.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥品區域_更新.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品區域_更新.GUID = "";
            this.rJ_Button_藥品區域_更新.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品區域_更新.Location = new System.Drawing.Point(737, 0);
            this.rJ_Button_藥品區域_更新.Name = "rJ_Button_藥品區域_更新";
            this.rJ_Button_藥品區域_更新.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品區域_更新.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品區域_更新.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品區域_更新.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品區域_更新.ShadowSize = 3;
            this.rJ_Button_藥品區域_更新.ShowLoadingForm = false;
            this.rJ_Button_藥品區域_更新.Size = new System.Drawing.Size(135, 88);
            this.rJ_Button_藥品區域_更新.State = false;
            this.rJ_Button_藥品區域_更新.TabIndex = 36;
            this.rJ_Button_藥品區域_更新.Text = "更新";
            this.rJ_Button_藥品區域_更新.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品區域_更新.TextHeight = 0;
            this.rJ_Button_藥品區域_更新.UseVisualStyleBackColor = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox3);
            this.tabPage5.Controls.Add(this.groupBox2);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1888, 1006);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "取藥堆疊資料";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sqL_DataGridView_堆疊母資料);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(863, 485);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "堆疊母資料";
            // 
            // sqL_DataGridView_堆疊母資料
            // 
            this.sqL_DataGridView_堆疊母資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_堆疊母資料.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊母資料.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊母資料.BorderRadius = 0;
            this.sqL_DataGridView_堆疊母資料.BorderSize = 0;
            this.sqL_DataGridView_堆疊母資料.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_堆疊母資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_堆疊母資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊母資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_堆疊母資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_堆疊母資料.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_堆疊母資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_堆疊母資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_堆疊母資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_堆疊母資料.columnHeadersHeight = 18;
            this.sqL_DataGridView_堆疊母資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_堆疊母資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_堆疊母資料.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_堆疊母資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_堆疊母資料.ImageBox = false;
            this.sqL_DataGridView_堆疊母資料.Location = new System.Drawing.Point(3, 18);
            this.sqL_DataGridView_堆疊母資料.Name = "sqL_DataGridView_堆疊母資料";
            this.sqL_DataGridView_堆疊母資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_堆疊母資料.Password = "user82822040";
            this.sqL_DataGridView_堆疊母資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_堆疊母資料.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊母資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_堆疊母資料.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_堆疊母資料.RowsHeight = 20;
            this.sqL_DataGridView_堆疊母資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_堆疊母資料.selectedBorderSize = 0;
            this.sqL_DataGridView_堆疊母資料.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_堆疊母資料.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_堆疊母資料.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_堆疊母資料.Server = "127.0.0.0";
            this.sqL_DataGridView_堆疊母資料.Size = new System.Drawing.Size(857, 464);
            this.sqL_DataGridView_堆疊母資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_堆疊母資料.TabIndex = 9;
            this.sqL_DataGridView_堆疊母資料.UserName = "root";
            this.sqL_DataGridView_堆疊母資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_堆疊母資料.可選擇多列 = false;
            this.sqL_DataGridView_堆疊母資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_堆疊母資料.自動換行 = true;
            this.sqL_DataGridView_堆疊母資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_堆疊母資料.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_堆疊母資料.顯示CheckBox = false;
            this.sqL_DataGridView_堆疊母資料.顯示首列 = true;
            this.sqL_DataGridView_堆疊母資料.顯示首行 = true;
            this.sqL_DataGridView_堆疊母資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_堆疊母資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sqL_DataGridView_堆疊子資料);
            this.groupBox2.Location = new System.Drawing.Point(7, 494);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(863, 485);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "堆疊子資料";
            // 
            // sqL_DataGridView_堆疊子資料
            // 
            this.sqL_DataGridView_堆疊子資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_堆疊子資料.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊子資料.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊子資料.BorderRadius = 0;
            this.sqL_DataGridView_堆疊子資料.BorderSize = 0;
            this.sqL_DataGridView_堆疊子資料.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_堆疊子資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_堆疊子資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊子資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_堆疊子資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_堆疊子資料.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_堆疊子資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_堆疊子資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_堆疊子資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_堆疊子資料.columnHeadersHeight = 18;
            this.sqL_DataGridView_堆疊子資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_堆疊子資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_堆疊子資料.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_堆疊子資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_堆疊子資料.ImageBox = false;
            this.sqL_DataGridView_堆疊子資料.Location = new System.Drawing.Point(3, 18);
            this.sqL_DataGridView_堆疊子資料.Name = "sqL_DataGridView_堆疊子資料";
            this.sqL_DataGridView_堆疊子資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_堆疊子資料.Password = "user82822040";
            this.sqL_DataGridView_堆疊子資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_堆疊子資料.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_堆疊子資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_堆疊子資料.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_堆疊子資料.RowsHeight = 20;
            this.sqL_DataGridView_堆疊子資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_堆疊子資料.selectedBorderSize = 0;
            this.sqL_DataGridView_堆疊子資料.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_堆疊子資料.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_堆疊子資料.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_堆疊子資料.Server = "127.0.0.0";
            this.sqL_DataGridView_堆疊子資料.Size = new System.Drawing.Size(857, 464);
            this.sqL_DataGridView_堆疊子資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_堆疊子資料.TabIndex = 9;
            this.sqL_DataGridView_堆疊子資料.UserName = "root";
            this.sqL_DataGridView_堆疊子資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_堆疊子資料.可選擇多列 = false;
            this.sqL_DataGridView_堆疊子資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_堆疊子資料.自動換行 = true;
            this.sqL_DataGridView_堆疊子資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_堆疊子資料.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_堆疊子資料.顯示CheckBox = false;
            this.sqL_DataGridView_堆疊子資料.顯示首列 = true;
            this.sqL_DataGridView_堆疊子資料.顯示首行 = true;
            this.sqL_DataGridView_堆疊子資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_堆疊子資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sqL_DataGridView_交易記錄查詢);
            this.groupBox3.Location = new System.Drawing.Point(876, 494);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(863, 485);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "交易記錄查詢";
            // 
            // sqL_DataGridView_交易記錄查詢
            // 
            this.sqL_DataGridView_交易記錄查詢.AutoSelectToDeep = false;
            this.sqL_DataGridView_交易記錄查詢.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_交易記錄查詢.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_交易記錄查詢.BorderRadius = 0;
            this.sqL_DataGridView_交易記錄查詢.BorderSize = 0;
            this.sqL_DataGridView_交易記錄查詢.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_交易記錄查詢.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_交易記錄查詢.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_交易記錄查詢.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_交易記錄查詢.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_交易記錄查詢.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_交易記錄查詢.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_交易記錄查詢.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_交易記錄查詢.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_交易記錄查詢.columnHeadersHeight = 18;
            this.sqL_DataGridView_交易記錄查詢.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_交易記錄查詢.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_交易記錄查詢.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_交易記錄查詢.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_交易記錄查詢.ImageBox = false;
            this.sqL_DataGridView_交易記錄查詢.Location = new System.Drawing.Point(3, 18);
            this.sqL_DataGridView_交易記錄查詢.Name = "sqL_DataGridView_交易記錄查詢";
            this.sqL_DataGridView_交易記錄查詢.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_交易記錄查詢.Password = "user82822040";
            this.sqL_DataGridView_交易記錄查詢.Port = ((uint)(3306u));
            this.sqL_DataGridView_交易記錄查詢.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_交易記錄查詢.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_交易記錄查詢.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_交易記錄查詢.RowsHeight = 20;
            this.sqL_DataGridView_交易記錄查詢.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_交易記錄查詢.selectedBorderSize = 0;
            this.sqL_DataGridView_交易記錄查詢.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_交易記錄查詢.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_交易記錄查詢.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_交易記錄查詢.Server = "127.0.0.0";
            this.sqL_DataGridView_交易記錄查詢.Size = new System.Drawing.Size(857, 464);
            this.sqL_DataGridView_交易記錄查詢.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_交易記錄查詢.TabIndex = 9;
            this.sqL_DataGridView_交易記錄查詢.UserName = "root";
            this.sqL_DataGridView_交易記錄查詢.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_交易記錄查詢.可選擇多列 = false;
            this.sqL_DataGridView_交易記錄查詢.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_交易記錄查詢.自動換行 = true;
            this.sqL_DataGridView_交易記錄查詢.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_交易記錄查詢.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_交易記錄查詢.顯示CheckBox = false;
            this.sqL_DataGridView_交易記錄查詢.顯示首列 = true;
            this.sqL_DataGridView_交易記錄查詢.顯示首行 = true;
            this.sqL_DataGridView_交易記錄查詢.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_交易記錄查詢.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1904, 1061);
            this.Controls.Add(this.plC_ScreenPage_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能藥庫系統";
            this.plC_ScreenPage_main.ResumeLayout(false);
            this.主畫面.ResumeLayout(false);
            this.panel_main.ResumeLayout(false);
            this.sidePanel1.ContentsPanel.ResumeLayout(false);
            this.sidePanel1.ResumeLayout(false);
            this.系統.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.PLC_ScreenPage plC_ScreenPage_main;
        private System.Windows.Forms.TabPage 主畫面;
        private System.Windows.Forms.TabPage 功能頁面;
        private System.Windows.Forms.TabPage 系統;
        private MyUI.PLC_UI_Init plC_UI_Init;
        private LadderUI.LowerMachine_Panel lowerMachine_Panel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private H_Pannel_lib.StorageUI_EPD_266 storageUI_EPD_266;
        private MyUI.PLC_RJ_ScreenButtonEx plC_RJ_ScreenButtonEx3;
        private System.Windows.Forms.Panel panel_main;
        private MyUI.PLC_RJ_ScreenButtonEx plC_RJ_ScreenButtonEx_主畫面;
        private System.Windows.Forms.TabPage tabPage3;
        private H_Pannel_lib.RFID_UI rfiD_UI;
        private MyUI.PLC_RJ_ScreenButtonEx plC_RJ_ScreenButtonEx1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_庫存查詢;
        private MyUI.PLC_RJ_Button plC_RJ_Button_驗收管理;
        private MyUI.PLC_RJ_Button plC_RJ_Button5;
        private MyUI.PLC_RJ_Button plC_RJ_Button1;
        private MyUI.PLC_RJ_Button plC_RJ_Button3;
        private MyUI.PLC_RJ_Button plC_RJ_Button7;
        private MyUI.PLC_RJ_Button plC_RJ_Button_儲位管理;
        private MyUI.PLC_RJ_Button plC_RJ_Button2;
        private MyUI.SidePanel sidePanel1;
        private System.Windows.Forms.Panel panel_MainForm;
        private System.Windows.Forms.TabPage tabPage4;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品區域;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private MyUI.RJ_TextBox rJ_TextBox_藥品區域_名稱;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private MyUI.RJ_TextBox rJ_TextBox_藥品區域_PIN_Num;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private MyUI.RJ_TextBox rJ_TextBox_藥品區域_Port;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private MyUI.RJ_TextBox rJ_TextBox_藥品區域_IP;
        private System.Windows.Forms.Panel panel5;
        private MyUI.RJ_Button rJ_Button_藥品區域_刪除;
        private MyUI.RJ_Button rJ_Button_藥品區域_新增;
        private MyUI.RJ_Button rJ_Button_藥品區域_更新;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox1;
        private SQLUI.SQL_DataGridView sqL_DataGridView_堆疊母資料;
        private System.Windows.Forms.GroupBox groupBox2;
        private SQLUI.SQL_DataGridView sqL_DataGridView_堆疊子資料;
        private System.Windows.Forms.GroupBox groupBox3;
        private SQLUI.SQL_DataGridView sqL_DataGridView_交易記錄查詢;
    }
}

