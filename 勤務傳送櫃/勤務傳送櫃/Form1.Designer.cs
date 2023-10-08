namespace 勤務傳送櫃
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.saveFileDialog_SaveExcel = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog_LoadExcel = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.panel_Main = new System.Windows.Forms.Panel();
            this.panel232 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_登入者顏色 = new MyUI.RJ_TextBox();
            this.rJ_TextBox_登入者姓名 = new MyUI.RJ_TextBox();
            this.rJ_TextBox_登入者ID = new MyUI.RJ_TextBox();
            this.rJ_Lable2 = new MyUI.RJ_Lable();
            this.rJ_Lable66 = new MyUI.RJ_Lable();
            this.plC_RJ_Button_登入畫面_登出 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_ScreenButton_系統頁面 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton_人員資料 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton_事件紀錄 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton_登入畫面 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton_主畫面 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton1 = new MyUI.PLC_RJ_ScreenButton();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.plC_ScreenPage_Main = new MyUI.PLC_ScreenPage();
            this.主畫面 = new System.Windows.Forms.TabPage();
            this.plC_ScreenPage_主畫面_PannelBox = new MyUI.PLC_ScreenPage();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_PannelBox01 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_PannelBox02 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_PannelBox03 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_PannelBox04 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_主畫面_PannelBox = new System.Windows.Forms.Panel();
            this.plC_RJ_ScreenButton20 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton21 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton22 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton23 = new MyUI.PLC_RJ_ScreenButton();
            this.登入畫面 = new System.Windows.Forms.TabPage();
            this.rJ_GroupBox1 = new MyUI.RJ_GroupBox();
            this.plC_RJ_Button_登入畫面_登入 = new MyUI.PLC_RJ_Button();
            this.panel185 = new System.Windows.Forms.Panel();
            this.textBox_登入畫面_帳號 = new System.Windows.Forms.TextBox();
            this.panel186 = new System.Windows.Forms.Panel();
            this.panel183 = new System.Windows.Forms.Panel();
            this.textBox_登入畫面_密碼 = new System.Windows.Forms.TextBox();
            this.panel184 = new System.Windows.Forms.Panel();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.人員資料 = new System.Windows.Forms.TabPage();
            this.plC_ScreenPage_人員資料_權限設定 = new MyUI.PLC_ScreenPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel_權限設定 = new MyUI.RJ_Pannel();
            this.loginIndex_Pannel = new MySQL_Login.LoginIndex_Pannel();
            this.panel29 = new System.Windows.Forms.Panel();
            this.rJ_Lable64 = new MyUI.RJ_Lable();
            this.plC_RJ_ComboBox_權限管理_權限等級 = new MyUI.PLC_RJ_ComboBox();
            this.plC_Button_權限設定_設定至Server = new MyUI.PLC_RJ_Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.plC_ScreenPage_人員資料_開門權限 = new MyUI.PLC_ScreenPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_01 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_02 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_03 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_04 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_05 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_06 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_07 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel_開門權限_08 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_人員資料_開門權限 = new System.Windows.Forms.Panel();
            this.plC_RJ_ScreenButton15 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton14 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton13 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton12 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton11 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton10 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton9 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton8 = new MyUI.PLC_RJ_ScreenButton();
            this.panel_人員資料_權限設定 = new System.Windows.Forms.Panel();
            this.plC_RJ_ScreenButton7 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton4 = new MyUI.PLC_RJ_ScreenButton();
            this.rJ_GroupBox20 = new MyUI.RJ_GroupBox();
            this.plC_RJ_Button_人員資料_開門權限全關 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_人員資料_開門權限全開 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_人員資料_清除內容 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_人員資料_刪除 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_人員資料_登錄 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_人員資料_匯入 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_人員資料_匯出 = new MyUI.PLC_RJ_Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.rJ_TextBox_人員資料_一維條碼 = new MyUI.RJ_TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rJ_TextBox_人員資料_卡號 = new MyUI.RJ_TextBox();
            this.comboBox_人員資料_權限等級 = new MyUI.RJ_ComboBox();
            this.rJ_TextBox_人員資料_單位 = new MyUI.RJ_TextBox();
            this.rJ_TextBox_人員資料_密碼 = new MyUI.RJ_TextBox();
            this.rJ_TextBox_人員資料_姓名 = new MyUI.RJ_TextBox();
            this.rJ_TextBox_人員資料_ID = new MyUI.RJ_TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label144 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel149 = new System.Windows.Forms.Panel();
            this.panel150 = new System.Windows.Forms.Panel();
            this.button_人員資料_顏色選擇 = new System.Windows.Forms.Button();
            this.textBox_人員資料_顏色 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.rJ_RatioButton_人員資料_男 = new MyUI.RJ_RatioButton();
            this.rJ_RatioButton_人員資料_女 = new MyUI.RJ_RatioButton();
            this.rJ_TextBox_人員資料_識別圖案 = new MyUI.RJ_TextBox();
            this.sqL_DataGridView_人員資料 = new SQLUI.SQL_DataGridView();
            this.系統頁面 = new System.Windows.Forms.TabPage();
            this.plC_ScreenPage_系統頁面 = new MyUI.PLC_ScreenPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.plC_NumBox2 = new MyUI.PLC_NumBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.plC_NumBox1 = new MyUI.PLC_NumBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plC_NumBox_開門異常時間 = new MyUI.PLC_NumBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.rfiD_FX600_UI = new RFID_FX600lib.RFID_FX600_UI();
            this.lowerMachine_Pane = new LadderUI.LowerMachine_Panel();
            this.plC_UI_Init = new MyUI.PLC_UI_Init();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rfiD_UI = new H_Pannel_lib.RFID_UI();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.plC_RJ_Button_Box_Index_Table_刪除 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_Box_Index_Table_更新 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_Box_Index_Table_匯入 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_Box_Index_Table_匯出 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_GroupBox9 = new MyUI.PLC_RJ_GroupBox();
            this.sqL_DataGridView_Box_Index_Table = new SQLUI.SQL_DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.loginUI = new MySQL_Login.LoginUI();
            this.panel_系統頁面 = new System.Windows.Forms.Panel();
            this.plC_RJ_ScreenButton3 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton6 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton2 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton5 = new MyUI.PLC_RJ_ScreenButton();
            this.事件紀錄 = new System.Windows.Forms.TabPage();
            this.plC_RJ_Button_事件紀錄_搜尋 = new MyUI.PLC_RJ_Button();
            this.rJ_GroupBox5 = new MyUI.RJ_GroupBox();
            this.rJ_CheckBox_事件紀錄_操作時間 = new MyUI.PLC_RJ_ChechBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rJ_DatePicker_事件紀錄_操作結束時間 = new MyUI.RJ_DatePicker();
            this.rJ_DatePicker_事件紀錄_操作起始時間 = new MyUI.RJ_DatePicker();
            this.rJ_GroupBox7 = new MyUI.RJ_GroupBox();
            this.rJ_TextBox_事件紀錄_病房名稱 = new MyUI.RJ_TextBox();
            this.rJ_Lable26 = new MyUI.RJ_Lable();
            this.rJ_TextBox_事件紀錄_藥櫃編號 = new MyUI.RJ_TextBox();
            this.rJ_Lable24 = new MyUI.RJ_Lable();
            this.rJ_TextBox_事件紀錄_姓名 = new MyUI.RJ_TextBox();
            this.rJ_Lable22 = new MyUI.RJ_Lable();
            this.rJ_TextBox_事件紀錄_ID = new MyUI.RJ_TextBox();
            this.rJ_Lable20 = new MyUI.RJ_Lable();
            this.rJ_GroupBox6 = new MyUI.RJ_GroupBox();
            this.panel78 = new System.Windows.Forms.Panel();
            this.rJ_CheckBox_事件紀錄_關閉門片 = new MyUI.PLC_RJ_ChechBox();
            this.rJ_Lable14 = new MyUI.RJ_Lable();
            this.panel77 = new System.Windows.Forms.Panel();
            this.rJ_Lable12 = new MyUI.RJ_Lable();
            this.rJ_CheckBox_事件紀錄_登出 = new MyUI.PLC_RJ_ChechBox();
            this.panel79 = new System.Windows.Forms.Panel();
            this.rJ_Lable16 = new MyUI.RJ_Lable();
            this.rJ_CheckBox_事件紀錄_開啟門片 = new MyUI.PLC_RJ_ChechBox();
            this.panel76 = new System.Windows.Forms.Panel();
            this.rJ_CheckBox_事件紀錄_RFID登入 = new MyUI.PLC_RJ_ChechBox();
            this.rJ_Lable10 = new MyUI.RJ_Lable();
            this.panel80 = new System.Windows.Forms.Panel();
            this.rJ_Lable18 = new MyUI.RJ_Lable();
            this.rJ_CheckBox_事件紀錄_門片未關閉異常 = new MyUI.PLC_RJ_ChechBox();
            this.panel75 = new System.Windows.Forms.Panel();
            this.rJ_CheckBox_事件紀錄_密碼登入 = new MyUI.PLC_RJ_ChechBox();
            this.rJ_Lable9 = new MyUI.RJ_Lable();
            this.sqL_DataGridView_事件紀錄 = new SQLUI.SQL_DataGridView();
            this.暫存區 = new System.Windows.Forms.TabPage();
            this.plC_AlarmFlow1 = new MyUI.PLC_AlarmFlow();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pannel_Box1 = new 勤務傳送櫃.Pannel_Box();
            this.panel_Main.SuspendLayout();
            this.panel232.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.plC_ScreenPage_Main.SuspendLayout();
            this.主畫面.SuspendLayout();
            this.plC_ScreenPage_主畫面_PannelBox.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.panel_主畫面_PannelBox.SuspendLayout();
            this.登入畫面.SuspendLayout();
            this.rJ_GroupBox1.ContentsPanel.SuspendLayout();
            this.rJ_GroupBox1.SuspendLayout();
            this.panel185.SuspendLayout();
            this.panel183.SuspendLayout();
            this.人員資料.SuspendLayout();
            this.plC_ScreenPage_人員資料_權限設定.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel_權限設定.SuspendLayout();
            this.panel29.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.plC_ScreenPage_人員資料_開門權限.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.panel_人員資料_開門權限.SuspendLayout();
            this.panel_人員資料_權限設定.SuspendLayout();
            this.rJ_GroupBox20.ContentsPanel.SuspendLayout();
            this.rJ_GroupBox20.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel149.SuspendLayout();
            this.panel150.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.系統頁面.SuspendLayout();
            this.plC_ScreenPage_系統頁面.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.plC_RJ_GroupBox9.ContentsPanel.SuspendLayout();
            this.plC_RJ_GroupBox9.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel_系統頁面.SuspendLayout();
            this.事件紀錄.SuspendLayout();
            this.rJ_GroupBox5.ContentsPanel.SuspendLayout();
            this.rJ_GroupBox5.SuspendLayout();
            this.rJ_GroupBox7.ContentsPanel.SuspendLayout();
            this.rJ_GroupBox7.SuspendLayout();
            this.rJ_GroupBox6.ContentsPanel.SuspendLayout();
            this.rJ_GroupBox6.SuspendLayout();
            this.panel78.SuspendLayout();
            this.panel77.SuspendLayout();
            this.panel79.SuspendLayout();
            this.panel76.SuspendLayout();
            this.panel80.SuspendLayout();
            this.panel75.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog_SaveExcel
            // 
            this.saveFileDialog_SaveExcel.DefaultExt = "txt";
            this.saveFileDialog_SaveExcel.FileName = " ";
            this.saveFileDialog_SaveExcel.Filter = "txt File (*.txt)|*.txt;";
            // 
            // openFileDialog_LoadExcel
            // 
            this.openFileDialog_LoadExcel.DefaultExt = "txt";
            this.openFileDialog_LoadExcel.Filter = "txt File (*.txt)|*.txt;";
            // 
            // panel_Main
            // 
            this.panel_Main.BackColor = System.Drawing.Color.SkyBlue;
            this.panel_Main.Controls.Add(this.panel232);
            this.panel_Main.Controls.Add(this.plC_RJ_ScreenButton_系統頁面);
            this.panel_Main.Controls.Add(this.plC_RJ_ScreenButton_人員資料);
            this.panel_Main.Controls.Add(this.plC_RJ_ScreenButton_事件紀錄);
            this.panel_Main.Controls.Add(this.plC_RJ_ScreenButton_登入畫面);
            this.panel_Main.Controls.Add(this.plC_RJ_ScreenButton_主畫面);
            this.panel_Main.Controls.Add(this.plC_RJ_ScreenButton1);
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Main.Location = new System.Drawing.Point(0, 0);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(257, 1035);
            this.panel_Main.TabIndex = 3;
            // 
            // panel232
            // 
            this.panel232.Controls.Add(this.rJ_TextBox_登入者顏色);
            this.panel232.Controls.Add(this.rJ_TextBox_登入者姓名);
            this.panel232.Controls.Add(this.rJ_TextBox_登入者ID);
            this.panel232.Controls.Add(this.rJ_Lable2);
            this.panel232.Controls.Add(this.rJ_Lable66);
            this.panel232.Controls.Add(this.plC_RJ_Button_登入畫面_登出);
            this.panel232.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel232.Location = new System.Drawing.Point(0, 880);
            this.panel232.Name = "panel232";
            this.panel232.Size = new System.Drawing.Size(257, 73);
            this.panel232.TabIndex = 98;
            // 
            // rJ_TextBox_登入者顏色
            // 
            this.rJ_TextBox_登入者顏色.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_登入者顏色.BorderColor = System.Drawing.Color.RoyalBlue;
            this.rJ_TextBox_登入者顏色.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_登入者顏色.BorderRadius = 2;
            this.rJ_TextBox_登入者顏色.BorderSize = 1;
            this.rJ_TextBox_登入者顏色.Enabled = false;
            this.rJ_TextBox_登入者顏色.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_登入者顏色.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_登入者顏色.GUID = "";
            this.rJ_TextBox_登入者顏色.Location = new System.Drawing.Point(142, 2);
            this.rJ_TextBox_登入者顏色.Multiline = false;
            this.rJ_TextBox_登入者顏色.Name = "rJ_TextBox_登入者顏色";
            this.rJ_TextBox_登入者顏色.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_登入者顏色.PassWordChar = false;
            this.rJ_TextBox_登入者顏色.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_登入者顏色.PlaceholderText = "";
            this.rJ_TextBox_登入者顏色.ShowTouchPannel = false;
            this.rJ_TextBox_登入者顏色.Size = new System.Drawing.Size(34, 30);
            this.rJ_TextBox_登入者顏色.TabIndex = 114;
            this.rJ_TextBox_登入者顏色.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_登入者顏色.Texts = "";
            this.rJ_TextBox_登入者顏色.UnderlineStyle = false;
            // 
            // rJ_TextBox_登入者姓名
            // 
            this.rJ_TextBox_登入者姓名.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_登入者姓名.BorderColor = System.Drawing.Color.RoyalBlue;
            this.rJ_TextBox_登入者姓名.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_登入者姓名.BorderRadius = 2;
            this.rJ_TextBox_登入者姓名.BorderSize = 1;
            this.rJ_TextBox_登入者姓名.Enabled = false;
            this.rJ_TextBox_登入者姓名.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_登入者姓名.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_登入者姓名.GUID = "";
            this.rJ_TextBox_登入者姓名.Location = new System.Drawing.Point(68, 38);
            this.rJ_TextBox_登入者姓名.Multiline = false;
            this.rJ_TextBox_登入者姓名.Name = "rJ_TextBox_登入者姓名";
            this.rJ_TextBox_登入者姓名.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_登入者姓名.PassWordChar = false;
            this.rJ_TextBox_登入者姓名.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_登入者姓名.PlaceholderText = "";
            this.rJ_TextBox_登入者姓名.ShowTouchPannel = false;
            this.rJ_TextBox_登入者姓名.Size = new System.Drawing.Size(108, 30);
            this.rJ_TextBox_登入者姓名.TabIndex = 114;
            this.rJ_TextBox_登入者姓名.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_登入者姓名.Texts = "";
            this.rJ_TextBox_登入者姓名.UnderlineStyle = false;
            // 
            // rJ_TextBox_登入者ID
            // 
            this.rJ_TextBox_登入者ID.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_登入者ID.BorderColor = System.Drawing.Color.RoyalBlue;
            this.rJ_TextBox_登入者ID.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_登入者ID.BorderRadius = 2;
            this.rJ_TextBox_登入者ID.BorderSize = 1;
            this.rJ_TextBox_登入者ID.Enabled = false;
            this.rJ_TextBox_登入者ID.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_登入者ID.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_登入者ID.GUID = "";
            this.rJ_TextBox_登入者ID.Location = new System.Drawing.Point(68, 2);
            this.rJ_TextBox_登入者ID.Multiline = false;
            this.rJ_TextBox_登入者ID.Name = "rJ_TextBox_登入者ID";
            this.rJ_TextBox_登入者ID.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_登入者ID.PassWordChar = false;
            this.rJ_TextBox_登入者ID.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_登入者ID.PlaceholderText = "";
            this.rJ_TextBox_登入者ID.ShowTouchPannel = false;
            this.rJ_TextBox_登入者ID.Size = new System.Drawing.Size(68, 30);
            this.rJ_TextBox_登入者ID.TabIndex = 113;
            this.rJ_TextBox_登入者ID.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_登入者ID.Texts = "";
            this.rJ_TextBox_登入者ID.UnderlineStyle = false;
            // 
            // rJ_Lable2
            // 
            this.rJ_Lable2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_Lable2.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_Lable2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable2.BorderRadius = 12;
            this.rJ_Lable2.BorderSize = 0;
            this.rJ_Lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable2.ForeColor = System.Drawing.Color.White;
            this.rJ_Lable2.GUID = "";
            this.rJ_Lable2.Location = new System.Drawing.Point(4, 2);
            this.rJ_Lable2.Name = "rJ_Lable2";
            this.rJ_Lable2.Size = new System.Drawing.Size(58, 30);
            this.rJ_Lable2.TabIndex = 20;
            this.rJ_Lable2.Text = "ID";
            this.rJ_Lable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable2.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Lable66
            // 
            this.rJ_Lable66.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_Lable66.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_Lable66.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable66.BorderRadius = 12;
            this.rJ_Lable66.BorderSize = 0;
            this.rJ_Lable66.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable66.ForeColor = System.Drawing.Color.White;
            this.rJ_Lable66.GUID = "";
            this.rJ_Lable66.Location = new System.Drawing.Point(4, 38);
            this.rJ_Lable66.Name = "rJ_Lable66";
            this.rJ_Lable66.Size = new System.Drawing.Size(58, 30);
            this.rJ_Lable66.TabIndex = 22;
            this.rJ_Lable66.Text = "Name";
            this.rJ_Lable66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable66.TextColor = System.Drawing.Color.White;
            // 
            // plC_RJ_Button_登入畫面_登出
            // 
            this.plC_RJ_Button_登入畫面_登出.AutoResetState = false;
            this.plC_RJ_Button_登入畫面_登出.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_Button_登入畫面_登出.Bool = false;
            this.plC_RJ_Button_登入畫面_登出.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_登入畫面_登出.BorderRadius = 5;
            this.plC_RJ_Button_登入畫面_登出.BorderSize = 0;
            this.plC_RJ_Button_登入畫面_登出.but_press = false;
            this.plC_RJ_Button_登入畫面_登出.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_登入畫面_登出.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_登入畫面_登出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_登入畫面_登出.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_登入畫面_登出.GUID = "";
            this.plC_RJ_Button_登入畫面_登出.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_登入畫面_登出.Location = new System.Drawing.Point(182, 2);
            this.plC_RJ_Button_登入畫面_登出.Name = "plC_RJ_Button_登入畫面_登出";
            this.plC_RJ_Button_登入畫面_登出.OFF_文字內容 = "登出";
            this.plC_RJ_Button_登入畫面_登出.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_登入畫面_登出.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_登入畫面_登出.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_登入畫面_登出.ON_BorderSize = 5;
            this.plC_RJ_Button_登入畫面_登出.ON_文字內容 = "登出";
            this.plC_RJ_Button_登入畫面_登出.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F);
            this.plC_RJ_Button_登入畫面_登出.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_登入畫面_登出.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_登入畫面_登出.Size = new System.Drawing.Size(66, 66);
            this.plC_RJ_Button_登入畫面_登出.State = false;
            this.plC_RJ_Button_登入畫面_登出.TabIndex = 23;
            this.plC_RJ_Button_登入畫面_登出.Text = "登出";
            this.plC_RJ_Button_登入畫面_登出.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_登入畫面_登出.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_登入畫面_登出.字型鎖住 = false;
            this.plC_RJ_Button_登入畫面_登出.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_登入畫面_登出.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_登入畫面_登出.文字鎖住 = false;
            this.plC_RJ_Button_登入畫面_登出.致能讀取位置 = "S4000";
            this.plC_RJ_Button_登入畫面_登出.讀取位元反向 = false;
            this.plC_RJ_Button_登入畫面_登出.讀寫鎖住 = false;
            this.plC_RJ_Button_登入畫面_登出.音效 = true;
            this.plC_RJ_Button_登入畫面_登出.顯示 = false;
            this.plC_RJ_Button_登入畫面_登出.顯示狀態 = false;
            // 
            // plC_RJ_ScreenButton_系統頁面
            // 
            this.plC_RJ_ScreenButton_系統頁面.but_press = false;
            this.plC_RJ_ScreenButton_系統頁面.Dock = System.Windows.Forms.DockStyle.Top;
            this.plC_RJ_ScreenButton_系統頁面.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this.plC_RJ_ScreenButton_系統頁面.IconSize = 40;
            this.plC_RJ_ScreenButton_系統頁面.Location = new System.Drawing.Point(0, 260);
            this.plC_RJ_ScreenButton_系統頁面.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton_系統頁面.Name = "plC_RJ_ScreenButton_系統頁面";
            this.plC_RJ_ScreenButton_系統頁面.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton_系統頁面.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_系統頁面.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_系統頁面.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_系統頁面.OffText = "系統頁面";
            this.plC_RJ_ScreenButton_系統頁面.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton_系統頁面.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_系統頁面.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_系統頁面.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_系統頁面.OnText = "系統頁面";
            this.plC_RJ_ScreenButton_系統頁面.ShowIcon = true;
            this.plC_RJ_ScreenButton_系統頁面.Size = new System.Drawing.Size(257, 65);
            this.plC_RJ_ScreenButton_系統頁面.TabIndex = 97;
            this.plC_RJ_ScreenButton_系統頁面.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton_系統頁面.寫入位置註解 = "";
            this.plC_RJ_ScreenButton_系統頁面.寫入元件位置 = "";
            this.plC_RJ_ScreenButton_系統頁面.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton_系統頁面.控制位址 = "D6";
            this.plC_RJ_ScreenButton_系統頁面.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton_系統頁面.致能讀取位置 = "S4077";
            this.plC_RJ_ScreenButton_系統頁面.讀取位元反向 = false;
            this.plC_RJ_ScreenButton_系統頁面.讀取位置註解 = "";
            this.plC_RJ_ScreenButton_系統頁面.讀取元件位置 = "";
            this.plC_RJ_ScreenButton_系統頁面.音效 = true;
            this.plC_RJ_ScreenButton_系統頁面.頁面名稱 = "系統頁面";
            this.plC_RJ_ScreenButton_系統頁面.頁面編號 = 0;
            this.plC_RJ_ScreenButton_系統頁面.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton_系統頁面.顯示狀態 = false;
            this.plC_RJ_ScreenButton_系統頁面.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton_人員資料
            // 
            this.plC_RJ_ScreenButton_人員資料.but_press = false;
            this.plC_RJ_ScreenButton_人員資料.Dock = System.Windows.Forms.DockStyle.Top;
            this.plC_RJ_ScreenButton_人員資料.IconChar = FontAwesome.Sharp.IconChar.AddressCard;
            this.plC_RJ_ScreenButton_人員資料.IconSize = 40;
            this.plC_RJ_ScreenButton_人員資料.Location = new System.Drawing.Point(0, 195);
            this.plC_RJ_ScreenButton_人員資料.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton_人員資料.Name = "plC_RJ_ScreenButton_人員資料";
            this.plC_RJ_ScreenButton_人員資料.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton_人員資料.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_人員資料.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_人員資料.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_人員資料.OffText = "人員資料";
            this.plC_RJ_ScreenButton_人員資料.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton_人員資料.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_人員資料.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_人員資料.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_人員資料.OnText = "人員資料";
            this.plC_RJ_ScreenButton_人員資料.ShowIcon = true;
            this.plC_RJ_ScreenButton_人員資料.Size = new System.Drawing.Size(257, 65);
            this.plC_RJ_ScreenButton_人員資料.TabIndex = 95;
            this.plC_RJ_ScreenButton_人員資料.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton_人員資料.寫入位置註解 = "";
            this.plC_RJ_ScreenButton_人員資料.寫入元件位置 = "";
            this.plC_RJ_ScreenButton_人員資料.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton_人員資料.控制位址 = "D3";
            this.plC_RJ_ScreenButton_人員資料.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton_人員資料.致能讀取位置 = "S39001";
            this.plC_RJ_ScreenButton_人員資料.讀取位元反向 = false;
            this.plC_RJ_ScreenButton_人員資料.讀取位置註解 = "";
            this.plC_RJ_ScreenButton_人員資料.讀取元件位置 = "";
            this.plC_RJ_ScreenButton_人員資料.音效 = true;
            this.plC_RJ_ScreenButton_人員資料.頁面名稱 = "人員資料";
            this.plC_RJ_ScreenButton_人員資料.頁面編號 = 0;
            this.plC_RJ_ScreenButton_人員資料.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton_人員資料.顯示狀態 = false;
            this.plC_RJ_ScreenButton_人員資料.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton_事件紀錄
            // 
            this.plC_RJ_ScreenButton_事件紀錄.but_press = false;
            this.plC_RJ_ScreenButton_事件紀錄.Dock = System.Windows.Forms.DockStyle.Top;
            this.plC_RJ_ScreenButton_事件紀錄.IconChar = FontAwesome.Sharp.IconChar.CalendarAlt;
            this.plC_RJ_ScreenButton_事件紀錄.IconSize = 40;
            this.plC_RJ_ScreenButton_事件紀錄.Location = new System.Drawing.Point(0, 130);
            this.plC_RJ_ScreenButton_事件紀錄.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton_事件紀錄.Name = "plC_RJ_ScreenButton_事件紀錄";
            this.plC_RJ_ScreenButton_事件紀錄.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton_事件紀錄.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_事件紀錄.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_事件紀錄.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_事件紀錄.OffText = "事件紀錄";
            this.plC_RJ_ScreenButton_事件紀錄.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton_事件紀錄.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_事件紀錄.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_事件紀錄.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_事件紀錄.OnText = "事件紀錄";
            this.plC_RJ_ScreenButton_事件紀錄.ShowIcon = true;
            this.plC_RJ_ScreenButton_事件紀錄.Size = new System.Drawing.Size(257, 65);
            this.plC_RJ_ScreenButton_事件紀錄.TabIndex = 90;
            this.plC_RJ_ScreenButton_事件紀錄.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton_事件紀錄.寫入位置註解 = "";
            this.plC_RJ_ScreenButton_事件紀錄.寫入元件位置 = "";
            this.plC_RJ_ScreenButton_事件紀錄.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton_事件紀錄.控制位址 = "D5";
            this.plC_RJ_ScreenButton_事件紀錄.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton_事件紀錄.致能讀取位置 = "S39000";
            this.plC_RJ_ScreenButton_事件紀錄.讀取位元反向 = false;
            this.plC_RJ_ScreenButton_事件紀錄.讀取位置註解 = "";
            this.plC_RJ_ScreenButton_事件紀錄.讀取元件位置 = "";
            this.plC_RJ_ScreenButton_事件紀錄.音效 = true;
            this.plC_RJ_ScreenButton_事件紀錄.頁面名稱 = "事件紀錄";
            this.plC_RJ_ScreenButton_事件紀錄.頁面編號 = 0;
            this.plC_RJ_ScreenButton_事件紀錄.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton_事件紀錄.顯示狀態 = false;
            this.plC_RJ_ScreenButton_事件紀錄.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton_登入畫面
            // 
            this.plC_RJ_ScreenButton_登入畫面.but_press = false;
            this.plC_RJ_ScreenButton_登入畫面.Dock = System.Windows.Forms.DockStyle.Top;
            this.plC_RJ_ScreenButton_登入畫面.IconChar = FontAwesome.Sharp.IconChar.SignInAlt;
            this.plC_RJ_ScreenButton_登入畫面.IconSize = 40;
            this.plC_RJ_ScreenButton_登入畫面.Location = new System.Drawing.Point(0, 65);
            this.plC_RJ_ScreenButton_登入畫面.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton_登入畫面.Name = "plC_RJ_ScreenButton_登入畫面";
            this.plC_RJ_ScreenButton_登入畫面.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton_登入畫面.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_登入畫面.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_登入畫面.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_登入畫面.OffText = "登入畫面";
            this.plC_RJ_ScreenButton_登入畫面.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton_登入畫面.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_登入畫面.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_登入畫面.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_登入畫面.OnText = "登入畫面";
            this.plC_RJ_ScreenButton_登入畫面.ShowIcon = true;
            this.plC_RJ_ScreenButton_登入畫面.Size = new System.Drawing.Size(257, 65);
            this.plC_RJ_ScreenButton_登入畫面.TabIndex = 92;
            this.plC_RJ_ScreenButton_登入畫面.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton_登入畫面.寫入位置註解 = "";
            this.plC_RJ_ScreenButton_登入畫面.寫入元件位置 = "";
            this.plC_RJ_ScreenButton_登入畫面.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton_登入畫面.控制位址 = "D1";
            this.plC_RJ_ScreenButton_登入畫面.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton_登入畫面.致能讀取位置 = "";
            this.plC_RJ_ScreenButton_登入畫面.讀取位元反向 = false;
            this.plC_RJ_ScreenButton_登入畫面.讀取位置註解 = "";
            this.plC_RJ_ScreenButton_登入畫面.讀取元件位置 = "";
            this.plC_RJ_ScreenButton_登入畫面.音效 = true;
            this.plC_RJ_ScreenButton_登入畫面.頁面名稱 = "登入畫面";
            this.plC_RJ_ScreenButton_登入畫面.頁面編號 = 0;
            this.plC_RJ_ScreenButton_登入畫面.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton_登入畫面.顯示狀態 = false;
            this.plC_RJ_ScreenButton_登入畫面.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton_主畫面
            // 
            this.plC_RJ_ScreenButton_主畫面.but_press = false;
            this.plC_RJ_ScreenButton_主畫面.Dock = System.Windows.Forms.DockStyle.Top;
            this.plC_RJ_ScreenButton_主畫面.IconChar = FontAwesome.Sharp.IconChar.Tv;
            this.plC_RJ_ScreenButton_主畫面.IconSize = 40;
            this.plC_RJ_ScreenButton_主畫面.Location = new System.Drawing.Point(0, 0);
            this.plC_RJ_ScreenButton_主畫面.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton_主畫面.Name = "plC_RJ_ScreenButton_主畫面";
            this.plC_RJ_ScreenButton_主畫面.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton_主畫面.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_主畫面.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_主畫面.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_主畫面.OffText = "主畫面";
            this.plC_RJ_ScreenButton_主畫面.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton_主畫面.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton_主畫面.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_主畫面.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_主畫面.OnText = "主畫面";
            this.plC_RJ_ScreenButton_主畫面.ShowIcon = true;
            this.plC_RJ_ScreenButton_主畫面.Size = new System.Drawing.Size(257, 65);
            this.plC_RJ_ScreenButton_主畫面.TabIndex = 85;
            this.plC_RJ_ScreenButton_主畫面.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton_主畫面.寫入位置註解 = "";
            this.plC_RJ_ScreenButton_主畫面.寫入元件位置 = "";
            this.plC_RJ_ScreenButton_主畫面.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton_主畫面.控制位址 = "D0";
            this.plC_RJ_ScreenButton_主畫面.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton_主畫面.致能讀取位置 = "";
            this.plC_RJ_ScreenButton_主畫面.讀取位元反向 = false;
            this.plC_RJ_ScreenButton_主畫面.讀取位置註解 = "";
            this.plC_RJ_ScreenButton_主畫面.讀取元件位置 = "";
            this.plC_RJ_ScreenButton_主畫面.音效 = true;
            this.plC_RJ_ScreenButton_主畫面.頁面名稱 = "主畫面";
            this.plC_RJ_ScreenButton_主畫面.頁面編號 = 0;
            this.plC_RJ_ScreenButton_主畫面.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton_主畫面.顯示狀態 = false;
            this.plC_RJ_ScreenButton_主畫面.顯示讀取位置 = "";
            this.plC_RJ_ScreenButton_主畫面.Load += new System.EventHandler(this.plC_RJ_ScreenButton_主畫面_Load);
            // 
            // plC_RJ_ScreenButton1
            // 
            this.plC_RJ_ScreenButton1.but_press = false;
            this.plC_RJ_ScreenButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plC_RJ_ScreenButton1.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.plC_RJ_ScreenButton1.IconSize = 40;
            this.plC_RJ_ScreenButton1.Location = new System.Drawing.Point(0, 953);
            this.plC_RJ_ScreenButton1.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton1.Name = "plC_RJ_ScreenButton1";
            this.plC_RJ_ScreenButton1.OffBackColor = System.Drawing.Color.CornflowerBlue;
            this.plC_RJ_ScreenButton1.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton1.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton1.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton1.OffText = "退出程式";
            this.plC_RJ_ScreenButton1.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton1.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton1.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton1.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton1.OnText = "退出程式";
            this.plC_RJ_ScreenButton1.ShowIcon = true;
            this.plC_RJ_ScreenButton1.Size = new System.Drawing.Size(257, 82);
            this.plC_RJ_ScreenButton1.TabIndex = 84;
            this.plC_RJ_ScreenButton1.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton1.寫入位置註解 = "";
            this.plC_RJ_ScreenButton1.寫入元件位置 = "";
            this.plC_RJ_ScreenButton1.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton1.控制位址 = "D5";
            this.plC_RJ_ScreenButton1.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.退出程式;
            this.plC_RJ_ScreenButton1.致能讀取位置 = "";
            this.plC_RJ_ScreenButton1.讀取位元反向 = false;
            this.plC_RJ_ScreenButton1.讀取位置註解 = "";
            this.plC_RJ_ScreenButton1.讀取元件位置 = "";
            this.plC_RJ_ScreenButton1.音效 = true;
            this.plC_RJ_ScreenButton1.頁面名稱 = "";
            this.plC_RJ_ScreenButton1.頁面編號 = 0;
            this.plC_RJ_ScreenButton1.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.正常顯示;
            this.plC_RJ_ScreenButton1.顯示狀態 = false;
            this.plC_RJ_ScreenButton1.顯示讀取位置 = "";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // plC_ScreenPage_Main
            // 
            this.plC_ScreenPage_Main.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage_Main.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_Main.Controls.Add(this.主畫面);
            this.plC_ScreenPage_Main.Controls.Add(this.登入畫面);
            this.plC_ScreenPage_Main.Controls.Add(this.人員資料);
            this.plC_ScreenPage_Main.Controls.Add(this.系統頁面);
            this.plC_ScreenPage_Main.Controls.Add(this.事件紀錄);
            this.plC_ScreenPage_Main.Controls.Add(this.暫存區);
            this.plC_ScreenPage_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage_Main.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage_Main.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage_Main.Location = new System.Drawing.Point(257, 0);
            this.plC_ScreenPage_Main.Margin = new System.Windows.Forms.Padding(0);
            this.plC_ScreenPage_Main.Name = "plC_ScreenPage_Main";
            this.plC_ScreenPage_Main.SelectedIndex = 0;
            this.plC_ScreenPage_Main.Size = new System.Drawing.Size(1647, 1035);
            this.plC_ScreenPage_Main.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_Main.TabIndex = 1;
            this.plC_ScreenPage_Main.控制位址 = "D0";
            this.plC_ScreenPage_Main.狀態位址 = "D1";
            this.plC_ScreenPage_Main.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_Main.顯示頁面 = 0;
            // 
            // 主畫面
            // 
            this.主畫面.AutoScroll = true;
            this.主畫面.BackColor = System.Drawing.SystemColors.Window;
            this.主畫面.Controls.Add(this.plC_ScreenPage_主畫面_PannelBox);
            this.主畫面.Controls.Add(this.panel_主畫面_PannelBox);
            this.主畫面.Location = new System.Drawing.Point(4, 25);
            this.主畫面.Margin = new System.Windows.Forms.Padding(0);
            this.主畫面.Name = "主畫面";
            this.主畫面.Size = new System.Drawing.Size(1639, 1006);
            this.主畫面.TabIndex = 0;
            this.主畫面.Text = "主畫面";
            // 
            // plC_ScreenPage_主畫面_PannelBox
            // 
            this.plC_ScreenPage_主畫面_PannelBox.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage_主畫面_PannelBox.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_主畫面_PannelBox.Controls.Add(this.tabPage15);
            this.plC_ScreenPage_主畫面_PannelBox.Controls.Add(this.tabPage16);
            this.plC_ScreenPage_主畫面_PannelBox.Controls.Add(this.tabPage17);
            this.plC_ScreenPage_主畫面_PannelBox.Controls.Add(this.tabPage18);
            this.plC_ScreenPage_主畫面_PannelBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage_主畫面_PannelBox.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage_主畫面_PannelBox.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage_主畫面_PannelBox.Location = new System.Drawing.Point(0, 88);
            this.plC_ScreenPage_主畫面_PannelBox.Name = "plC_ScreenPage_主畫面_PannelBox";
            this.plC_ScreenPage_主畫面_PannelBox.SelectedIndex = 0;
            this.plC_ScreenPage_主畫面_PannelBox.Size = new System.Drawing.Size(1639, 918);
            this.plC_ScreenPage_主畫面_PannelBox.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_主畫面_PannelBox.TabIndex = 25;
            this.plC_ScreenPage_主畫面_PannelBox.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_主畫面_PannelBox.顯示頁面 = 0;
            // 
            // tabPage15
            // 
            this.tabPage15.BackColor = System.Drawing.Color.White;
            this.tabPage15.Controls.Add(this.flowLayoutPanel_PannelBox01);
            this.tabPage15.Location = new System.Drawing.Point(4, 25);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Size = new System.Drawing.Size(1631, 889);
            this.tabPage15.TabIndex = 0;
            this.tabPage15.Text = "01";
            // 
            // flowLayoutPanel_PannelBox01
            // 
            this.flowLayoutPanel_PannelBox01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_PannelBox01.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_PannelBox01.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_PannelBox01.Name = "flowLayoutPanel_PannelBox01";
            this.flowLayoutPanel_PannelBox01.Size = new System.Drawing.Size(1631, 889);
            this.flowLayoutPanel_PannelBox01.TabIndex = 0;
            // 
            // tabPage16
            // 
            this.tabPage16.BackColor = System.Drawing.Color.White;
            this.tabPage16.Controls.Add(this.flowLayoutPanel_PannelBox02);
            this.tabPage16.Location = new System.Drawing.Point(4, 25);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Size = new System.Drawing.Size(1631, 889);
            this.tabPage16.TabIndex = 1;
            this.tabPage16.Text = "02";
            // 
            // flowLayoutPanel_PannelBox02
            // 
            this.flowLayoutPanel_PannelBox02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_PannelBox02.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_PannelBox02.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_PannelBox02.Name = "flowLayoutPanel_PannelBox02";
            this.flowLayoutPanel_PannelBox02.Size = new System.Drawing.Size(1631, 889);
            this.flowLayoutPanel_PannelBox02.TabIndex = 1;
            // 
            // tabPage17
            // 
            this.tabPage17.BackColor = System.Drawing.Color.White;
            this.tabPage17.Controls.Add(this.flowLayoutPanel_PannelBox03);
            this.tabPage17.Location = new System.Drawing.Point(4, 25);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Size = new System.Drawing.Size(1631, 889);
            this.tabPage17.TabIndex = 2;
            this.tabPage17.Text = "03";
            // 
            // flowLayoutPanel_PannelBox03
            // 
            this.flowLayoutPanel_PannelBox03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_PannelBox03.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_PannelBox03.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_PannelBox03.Name = "flowLayoutPanel_PannelBox03";
            this.flowLayoutPanel_PannelBox03.Size = new System.Drawing.Size(1631, 889);
            this.flowLayoutPanel_PannelBox03.TabIndex = 1;
            // 
            // tabPage18
            // 
            this.tabPage18.BackColor = System.Drawing.Color.White;
            this.tabPage18.Controls.Add(this.flowLayoutPanel_PannelBox04);
            this.tabPage18.Location = new System.Drawing.Point(4, 25);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Size = new System.Drawing.Size(1631, 889);
            this.tabPage18.TabIndex = 3;
            this.tabPage18.Text = "04";
            // 
            // flowLayoutPanel_PannelBox04
            // 
            this.flowLayoutPanel_PannelBox04.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_PannelBox04.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_PannelBox04.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_PannelBox04.Name = "flowLayoutPanel_PannelBox04";
            this.flowLayoutPanel_PannelBox04.Size = new System.Drawing.Size(1631, 889);
            this.flowLayoutPanel_PannelBox04.TabIndex = 1;
            // 
            // panel_主畫面_PannelBox
            // 
            this.panel_主畫面_PannelBox.BackColor = System.Drawing.Color.White;
            this.panel_主畫面_PannelBox.Controls.Add(this.plC_RJ_ScreenButton20);
            this.panel_主畫面_PannelBox.Controls.Add(this.plC_RJ_ScreenButton21);
            this.panel_主畫面_PannelBox.Controls.Add(this.plC_RJ_ScreenButton22);
            this.panel_主畫面_PannelBox.Controls.Add(this.plC_RJ_ScreenButton23);
            this.panel_主畫面_PannelBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_主畫面_PannelBox.Location = new System.Drawing.Point(0, 0);
            this.panel_主畫面_PannelBox.Name = "panel_主畫面_PannelBox";
            this.panel_主畫面_PannelBox.Size = new System.Drawing.Size(1639, 88);
            this.panel_主畫面_PannelBox.TabIndex = 23;
            // 
            // plC_RJ_ScreenButton20
            // 
            this.plC_RJ_ScreenButton20.but_press = false;
            this.plC_RJ_ScreenButton20.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton20.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton20.IconSize = 40;
            this.plC_RJ_ScreenButton20.Location = new System.Drawing.Point(294, 0);
            this.plC_RJ_ScreenButton20.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton20.Name = "plC_RJ_ScreenButton20";
            this.plC_RJ_ScreenButton20.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton20.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton20.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton20.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton20.OffText = "04";
            this.plC_RJ_ScreenButton20.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton20.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton20.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton20.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton20.OnText = "04";
            this.plC_RJ_ScreenButton20.ShowIcon = false;
            this.plC_RJ_ScreenButton20.Size = new System.Drawing.Size(98, 88);
            this.plC_RJ_ScreenButton20.TabIndex = 89;
            this.plC_RJ_ScreenButton20.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton20.寫入位置註解 = "";
            this.plC_RJ_ScreenButton20.寫入元件位置 = "";
            this.plC_RJ_ScreenButton20.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton20.控制位址 = "D0";
            this.plC_RJ_ScreenButton20.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton20.致能讀取位置 = "";
            this.plC_RJ_ScreenButton20.讀取位元反向 = false;
            this.plC_RJ_ScreenButton20.讀取位置註解 = "";
            this.plC_RJ_ScreenButton20.讀取元件位置 = "";
            this.plC_RJ_ScreenButton20.音效 = true;
            this.plC_RJ_ScreenButton20.頁面名稱 = "04";
            this.plC_RJ_ScreenButton20.頁面編號 = 0;
            this.plC_RJ_ScreenButton20.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton20.顯示狀態 = false;
            this.plC_RJ_ScreenButton20.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton21
            // 
            this.plC_RJ_ScreenButton21.but_press = false;
            this.plC_RJ_ScreenButton21.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton21.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton21.IconSize = 40;
            this.plC_RJ_ScreenButton21.Location = new System.Drawing.Point(196, 0);
            this.plC_RJ_ScreenButton21.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton21.Name = "plC_RJ_ScreenButton21";
            this.plC_RJ_ScreenButton21.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton21.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton21.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton21.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton21.OffText = "03";
            this.plC_RJ_ScreenButton21.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton21.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton21.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton21.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton21.OnText = "03";
            this.plC_RJ_ScreenButton21.ShowIcon = false;
            this.plC_RJ_ScreenButton21.Size = new System.Drawing.Size(98, 88);
            this.plC_RJ_ScreenButton21.TabIndex = 88;
            this.plC_RJ_ScreenButton21.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton21.寫入位置註解 = "";
            this.plC_RJ_ScreenButton21.寫入元件位置 = "";
            this.plC_RJ_ScreenButton21.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton21.控制位址 = "D0";
            this.plC_RJ_ScreenButton21.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton21.致能讀取位置 = "";
            this.plC_RJ_ScreenButton21.讀取位元反向 = false;
            this.plC_RJ_ScreenButton21.讀取位置註解 = "";
            this.plC_RJ_ScreenButton21.讀取元件位置 = "";
            this.plC_RJ_ScreenButton21.音效 = true;
            this.plC_RJ_ScreenButton21.頁面名稱 = "03";
            this.plC_RJ_ScreenButton21.頁面編號 = 0;
            this.plC_RJ_ScreenButton21.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton21.顯示狀態 = false;
            this.plC_RJ_ScreenButton21.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton22
            // 
            this.plC_RJ_ScreenButton22.but_press = false;
            this.plC_RJ_ScreenButton22.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton22.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton22.IconSize = 40;
            this.plC_RJ_ScreenButton22.Location = new System.Drawing.Point(98, 0);
            this.plC_RJ_ScreenButton22.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton22.Name = "plC_RJ_ScreenButton22";
            this.plC_RJ_ScreenButton22.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton22.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton22.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton22.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton22.OffText = "02";
            this.plC_RJ_ScreenButton22.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton22.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton22.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton22.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton22.OnText = "02";
            this.plC_RJ_ScreenButton22.ShowIcon = false;
            this.plC_RJ_ScreenButton22.Size = new System.Drawing.Size(98, 88);
            this.plC_RJ_ScreenButton22.TabIndex = 87;
            this.plC_RJ_ScreenButton22.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton22.寫入位置註解 = "";
            this.plC_RJ_ScreenButton22.寫入元件位置 = "";
            this.plC_RJ_ScreenButton22.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton22.控制位址 = "D0";
            this.plC_RJ_ScreenButton22.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton22.致能讀取位置 = "";
            this.plC_RJ_ScreenButton22.讀取位元反向 = false;
            this.plC_RJ_ScreenButton22.讀取位置註解 = "";
            this.plC_RJ_ScreenButton22.讀取元件位置 = "";
            this.plC_RJ_ScreenButton22.音效 = true;
            this.plC_RJ_ScreenButton22.頁面名稱 = "02";
            this.plC_RJ_ScreenButton22.頁面編號 = 0;
            this.plC_RJ_ScreenButton22.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton22.顯示狀態 = false;
            this.plC_RJ_ScreenButton22.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton23
            // 
            this.plC_RJ_ScreenButton23.but_press = false;
            this.plC_RJ_ScreenButton23.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton23.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton23.IconSize = 40;
            this.plC_RJ_ScreenButton23.Location = new System.Drawing.Point(0, 0);
            this.plC_RJ_ScreenButton23.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton23.Name = "plC_RJ_ScreenButton23";
            this.plC_RJ_ScreenButton23.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton23.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton23.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton23.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton23.OffText = "01";
            this.plC_RJ_ScreenButton23.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton23.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton23.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton23.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton23.OnText = "01";
            this.plC_RJ_ScreenButton23.ShowIcon = false;
            this.plC_RJ_ScreenButton23.Size = new System.Drawing.Size(98, 88);
            this.plC_RJ_ScreenButton23.TabIndex = 86;
            this.plC_RJ_ScreenButton23.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton23.寫入位置註解 = "";
            this.plC_RJ_ScreenButton23.寫入元件位置 = "";
            this.plC_RJ_ScreenButton23.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton23.控制位址 = "D0";
            this.plC_RJ_ScreenButton23.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton23.致能讀取位置 = "";
            this.plC_RJ_ScreenButton23.讀取位元反向 = false;
            this.plC_RJ_ScreenButton23.讀取位置註解 = "";
            this.plC_RJ_ScreenButton23.讀取元件位置 = "";
            this.plC_RJ_ScreenButton23.音效 = true;
            this.plC_RJ_ScreenButton23.頁面名稱 = "01";
            this.plC_RJ_ScreenButton23.頁面編號 = 0;
            this.plC_RJ_ScreenButton23.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton23.顯示狀態 = false;
            this.plC_RJ_ScreenButton23.顯示讀取位置 = "";
            // 
            // 登入畫面
            // 
            this.登入畫面.AutoScroll = true;
            this.登入畫面.BackColor = System.Drawing.SystemColors.Window;
            this.登入畫面.Controls.Add(this.rJ_GroupBox1);
            this.登入畫面.Controls.Add(this.rJ_Lable1);
            this.登入畫面.Location = new System.Drawing.Point(4, 25);
            this.登入畫面.Name = "登入畫面";
            this.登入畫面.Size = new System.Drawing.Size(1639, 1006);
            this.登入畫面.TabIndex = 5;
            this.登入畫面.Text = "登入畫面";
            // 
            // rJ_GroupBox1
            // 
            // 
            // rJ_GroupBox1.ContentsPanel
            // 
            this.rJ_GroupBox1.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox1.ContentsPanel.BorderRadius = 5;
            this.rJ_GroupBox1.ContentsPanel.BorderSize = 2;
            this.rJ_GroupBox1.ContentsPanel.Controls.Add(this.plC_RJ_Button_登入畫面_登入);
            this.rJ_GroupBox1.ContentsPanel.Controls.Add(this.panel185);
            this.rJ_GroupBox1.ContentsPanel.Controls.Add(this.panel183);
            this.rJ_GroupBox1.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox1.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.ContentsPanel.IsSelected = false;
            this.rJ_GroupBox1.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.rJ_GroupBox1.ContentsPanel.Name = "ContentsPanel";
            this.rJ_GroupBox1.ContentsPanel.Size = new System.Drawing.Size(533, 249);
            this.rJ_GroupBox1.ContentsPanel.TabIndex = 2;
            this.rJ_GroupBox1.GUID = "";
            this.rJ_GroupBox1.Location = new System.Drawing.Point(553, 350);
            this.rJ_GroupBox1.Name = "rJ_GroupBox1";
            this.rJ_GroupBox1.PannelBackColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox1.PannelBorderRadius = 5;
            this.rJ_GroupBox1.PannelBorderSize = 2;
            this.rJ_GroupBox1.Size = new System.Drawing.Size(533, 286);
            this.rJ_GroupBox1.TabIndex = 110;
            this.rJ_GroupBox1.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_GroupBox1.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_GroupBox1.TitleBorderRadius = 5;
            this.rJ_GroupBox1.TitleBorderSize = 0;
            this.rJ_GroupBox1.TitleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_GroupBox1.TitleForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.TitleHeight = 37;
            this.rJ_GroupBox1.TitleTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_GroupBox1.TitleTexts = "    帳號登入";
            // 
            // plC_RJ_Button_登入畫面_登入
            // 
            this.plC_RJ_Button_登入畫面_登入.AutoResetState = false;
            this.plC_RJ_Button_登入畫面_登入.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_Button_登入畫面_登入.Bool = false;
            this.plC_RJ_Button_登入畫面_登入.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_登入畫面_登入.BorderRadius = 5;
            this.plC_RJ_Button_登入畫面_登入.BorderSize = 0;
            this.plC_RJ_Button_登入畫面_登入.but_press = false;
            this.plC_RJ_Button_登入畫面_登入.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_登入畫面_登入.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_登入畫面_登入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_登入畫面_登入.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_登入畫面_登入.GUID = "";
            this.plC_RJ_Button_登入畫面_登入.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_登入畫面_登入.Location = new System.Drawing.Point(317, 173);
            this.plC_RJ_Button_登入畫面_登入.Name = "plC_RJ_Button_登入畫面_登入";
            this.plC_RJ_Button_登入畫面_登入.OFF_文字內容 = "登入";
            this.plC_RJ_Button_登入畫面_登入.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_登入畫面_登入.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_登入畫面_登入.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_登入畫面_登入.ON_BorderSize = 5;
            this.plC_RJ_Button_登入畫面_登入.ON_文字內容 = "登入";
            this.plC_RJ_Button_登入畫面_登入.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.plC_RJ_Button_登入畫面_登入.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_登入畫面_登入.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_登入畫面_登入.Size = new System.Drawing.Size(133, 66);
            this.plC_RJ_Button_登入畫面_登入.State = false;
            this.plC_RJ_Button_登入畫面_登入.TabIndex = 31;
            this.plC_RJ_Button_登入畫面_登入.Text = "登入";
            this.plC_RJ_Button_登入畫面_登入.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_登入畫面_登入.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_登入畫面_登入.字型鎖住 = false;
            this.plC_RJ_Button_登入畫面_登入.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_登入畫面_登入.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_登入畫面_登入.文字鎖住 = false;
            this.plC_RJ_Button_登入畫面_登入.讀取位元反向 = false;
            this.plC_RJ_Button_登入畫面_登入.讀寫鎖住 = false;
            this.plC_RJ_Button_登入畫面_登入.音效 = true;
            this.plC_RJ_Button_登入畫面_登入.顯示 = false;
            this.plC_RJ_Button_登入畫面_登入.顯示狀態 = false;
            // 
            // panel185
            // 
            this.panel185.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel185.Controls.Add(this.textBox_登入畫面_帳號);
            this.panel185.Controls.Add(this.panel186);
            this.panel185.Location = new System.Drawing.Point(81, 27);
            this.panel185.Name = "panel185";
            this.panel185.Size = new System.Drawing.Size(369, 67);
            this.panel185.TabIndex = 2;
            // 
            // textBox_登入畫面_帳號
            // 
            this.textBox_登入畫面_帳號.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_登入畫面_帳號.Location = new System.Drawing.Point(82, 23);
            this.textBox_登入畫面_帳號.Name = "textBox_登入畫面_帳號";
            this.textBox_登入畫面_帳號.Size = new System.Drawing.Size(260, 27);
            this.textBox_登入畫面_帳號.TabIndex = 1;
            // 
            // panel186
            // 
            this.panel186.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel186.BackgroundImage")));
            this.panel186.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel186.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel186.Location = new System.Drawing.Point(14, 10);
            this.panel186.Name = "panel186";
            this.panel186.Size = new System.Drawing.Size(51, 47);
            this.panel186.TabIndex = 0;
            // 
            // panel183
            // 
            this.panel183.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel183.Controls.Add(this.textBox_登入畫面_密碼);
            this.panel183.Controls.Add(this.panel184);
            this.panel183.Location = new System.Drawing.Point(81, 100);
            this.panel183.Name = "panel183";
            this.panel183.Size = new System.Drawing.Size(369, 67);
            this.panel183.TabIndex = 3;
            // 
            // textBox_登入畫面_密碼
            // 
            this.textBox_登入畫面_密碼.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_登入畫面_密碼.Location = new System.Drawing.Point(82, 22);
            this.textBox_登入畫面_密碼.Name = "textBox_登入畫面_密碼";
            this.textBox_登入畫面_密碼.Size = new System.Drawing.Size(260, 27);
            this.textBox_登入畫面_密碼.TabIndex = 2;
            this.textBox_登入畫面_密碼.UseSystemPasswordChar = true;
            // 
            // panel184
            // 
            this.panel184.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel184.BackgroundImage")));
            this.panel184.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel184.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel184.Location = new System.Drawing.Point(14, 10);
            this.panel184.Name = "panel184";
            this.panel184.Size = new System.Drawing.Size(51, 47);
            this.panel184.TabIndex = 0;
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 12;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("標楷體", 30F);
            this.rJ_Lable1.ForeColor = System.Drawing.Color.White;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(349, 265);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.Size = new System.Drawing.Size(956, 77);
            this.rJ_Lable1.TabIndex = 28;
            this.rJ_Lable1.Text = "傳送櫃系統";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.White;
            // 
            // 人員資料
            // 
            this.人員資料.AutoScroll = true;
            this.人員資料.BackColor = System.Drawing.SystemColors.Window;
            this.人員資料.Controls.Add(this.plC_ScreenPage_人員資料_權限設定);
            this.人員資料.Controls.Add(this.panel_人員資料_權限設定);
            this.人員資料.Controls.Add(this.rJ_GroupBox20);
            this.人員資料.Controls.Add(this.sqL_DataGridView_人員資料);
            this.人員資料.Location = new System.Drawing.Point(4, 25);
            this.人員資料.Margin = new System.Windows.Forms.Padding(0);
            this.人員資料.Name = "人員資料";
            this.人員資料.Size = new System.Drawing.Size(1639, 1006);
            this.人員資料.TabIndex = 1;
            this.人員資料.Text = "人員資料";
            // 
            // plC_ScreenPage_人員資料_權限設定
            // 
            this.plC_ScreenPage_人員資料_權限設定.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage_人員資料_權限設定.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_人員資料_權限設定.Controls.Add(this.tabPage3);
            this.plC_ScreenPage_人員資料_權限設定.Controls.Add(this.tabPage5);
            this.plC_ScreenPage_人員資料_權限設定.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage_人員資料_權限設定.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage_人員資料_權限設定.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage_人員資料_權限設定.Location = new System.Drawing.Point(529, 573);
            this.plC_ScreenPage_人員資料_權限設定.Name = "plC_ScreenPage_人員資料_權限設定";
            this.plC_ScreenPage_人員資料_權限設定.SelectedIndex = 0;
            this.plC_ScreenPage_人員資料_權限設定.Size = new System.Drawing.Size(1110, 433);
            this.plC_ScreenPage_人員資料_權限設定.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_人員資料_權限設定.TabIndex = 116;
            this.plC_ScreenPage_人員資料_權限設定.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_人員資料_權限設定.顯示頁面 = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.panel_權限設定);
            this.tabPage3.Controls.Add(this.panel29);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1102, 404);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "一般權限";
            // 
            // panel_權限設定
            // 
            this.panel_權限設定.BackColor = System.Drawing.Color.White;
            this.panel_權限設定.BorderColor = System.Drawing.Color.SkyBlue;
            this.panel_權限設定.BorderRadius = 10;
            this.panel_權限設定.BorderSize = 2;
            this.panel_權限設定.Controls.Add(this.loginIndex_Pannel);
            this.panel_權限設定.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_權限設定.ForeColor = System.Drawing.Color.White;
            this.panel_權限設定.IsSelected = false;
            this.panel_權限設定.Location = new System.Drawing.Point(0, 66);
            this.panel_權限設定.Name = "panel_權限設定";
            this.panel_權限設定.Padding = new System.Windows.Forms.Padding(5);
            this.panel_權限設定.Size = new System.Drawing.Size(1102, 338);
            this.panel_權限設定.TabIndex = 125;
            // 
            // loginIndex_Pannel
            // 
            this.loginIndex_Pannel.CheckBox_OffBackColor = System.Drawing.Color.Gray;
            this.loginIndex_Pannel.CheckBox_OffToggleColor = System.Drawing.Color.Gainsboro;
            this.loginIndex_Pannel.CheckBox_OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.loginIndex_Pannel.CheckBox_OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.loginIndex_Pannel.CheckBox_SolidStyle = true;
            this.loginIndex_Pannel.CheckBoxWidth = 59;
            this.loginIndex_Pannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginIndex_Pannel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.loginIndex_Pannel.Index = -1;
            this.loginIndex_Pannel.Location = new System.Drawing.Point(5, 5);
            this.loginIndex_Pannel.LoginIndex = ((System.Collections.Generic.List<string>)(resources.GetObject("loginIndex_Pannel.LoginIndex")));
            this.loginIndex_Pannel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginIndex_Pannel.Name = "loginIndex_Pannel";
            this.loginIndex_Pannel.PanelHeight = 30;
            this.loginIndex_Pannel.PanelWidth = 220;
            this.loginIndex_Pannel.Show_Index = true;
            this.loginIndex_Pannel.Size = new System.Drawing.Size(1092, 328);
            this.loginIndex_Pannel.SpaceWidth = 10;
            this.loginIndex_Pannel.TabIndex = 1;
            this.loginIndex_Pannel.Title_BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.loginIndex_Pannel.Title_BorderColor = System.Drawing.Color.PaleVioletRed;
            this.loginIndex_Pannel.Title_BorderRadius = 5;
            this.loginIndex_Pannel.Title_BorderSize = 0;
            this.loginIndex_Pannel.Title_Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.loginIndex_Pannel.Title_TextColor = System.Drawing.Color.White;
            this.loginIndex_Pannel.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.loginIndex_Pannel.TitleWidth = 135;
            // 
            // panel29
            // 
            this.panel29.Controls.Add(this.rJ_Lable64);
            this.panel29.Controls.Add(this.plC_RJ_ComboBox_權限管理_權限等級);
            this.panel29.Controls.Add(this.plC_Button_權限設定_設定至Server);
            this.panel29.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel29.Location = new System.Drawing.Point(0, 0);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(1102, 66);
            this.panel29.TabIndex = 126;
            // 
            // rJ_Lable64
            // 
            this.rJ_Lable64.BackColor = System.Drawing.Color.LightSteelBlue;
            this.rJ_Lable64.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.rJ_Lable64.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable64.BorderRadius = 12;
            this.rJ_Lable64.BorderSize = 0;
            this.rJ_Lable64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable64.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable64.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable64.GUID = "";
            this.rJ_Lable64.Location = new System.Drawing.Point(12, 14);
            this.rJ_Lable64.Name = "rJ_Lable64";
            this.rJ_Lable64.Size = new System.Drawing.Size(107, 40);
            this.rJ_Lable64.TabIndex = 122;
            this.rJ_Lable64.Text = "權限等級";
            this.rJ_Lable64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable64.TextColor = System.Drawing.Color.Black;
            // 
            // plC_RJ_ComboBox_權限管理_權限等級
            // 
            this.plC_RJ_ComboBox_權限管理_權限等級.BackColor = System.Drawing.Color.WhiteSmoke;
            this.plC_RJ_ComboBox_權限管理_權限等級.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.plC_RJ_ComboBox_權限管理_權限等級.BorderSize = 1;
            this.plC_RJ_ComboBox_權限管理_權限等級.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.plC_RJ_ComboBox_權限管理_權限等級.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ComboBox_權限管理_權限等級.ForeColor = System.Drawing.Color.DimGray;
            this.plC_RJ_ComboBox_權限管理_權限等級.GUID = "";
            this.plC_RJ_ComboBox_權限管理_權限等級.IconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ComboBox_權限管理_權限等級.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.plC_RJ_ComboBox_權限管理_權限等級.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.plC_RJ_ComboBox_權限管理_權限等級.ListTextColor = System.Drawing.Color.DimGray;
            this.plC_RJ_ComboBox_權限管理_權限等級.Location = new System.Drawing.Point(127, 19);
            this.plC_RJ_ComboBox_權限管理_權限等級.MinimumSize = new System.Drawing.Size(100, 30);
            this.plC_RJ_ComboBox_權限管理_權限等級.Name = "plC_RJ_ComboBox_權限管理_權限等級";
            this.plC_RJ_ComboBox_權限管理_權限等級.Padding = new System.Windows.Forms.Padding(1);
            this.plC_RJ_ComboBox_權限管理_權限等級.Size = new System.Drawing.Size(100, 30);
            this.plC_RJ_ComboBox_權限管理_權限等級.TabIndex = 123;
            this.plC_RJ_ComboBox_權限管理_權限等級.Texts = "";
            this.plC_RJ_ComboBox_權限管理_權限等級.音效 = true;
            // 
            // plC_Button_權限設定_設定至Server
            // 
            this.plC_Button_權限設定_設定至Server.AutoResetState = false;
            this.plC_Button_權限設定_設定至Server.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.plC_Button_權限設定_設定至Server.Bool = false;
            this.plC_Button_權限設定_設定至Server.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_Button_權限設定_設定至Server.BorderRadius = 5;
            this.plC_Button_權限設定_設定至Server.BorderSize = 0;
            this.plC_Button_權限設定_設定至Server.but_press = false;
            this.plC_Button_權限設定_設定至Server.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_Button_權限設定_設定至Server.FlatAppearance.BorderSize = 0;
            this.plC_Button_權限設定_設定至Server.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_Button_權限設定_設定至Server.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_Button_權限設定_設定至Server.GUID = "";
            this.plC_Button_權限設定_設定至Server.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_權限設定_設定至Server.Location = new System.Drawing.Point(278, 4);
            this.plC_Button_權限設定_設定至Server.Name = "plC_Button_權限設定_設定至Server";
            this.plC_Button_權限設定_設定至Server.OFF_文字內容 = "上傳資料";
            this.plC_Button_權限設定_設定至Server.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_Button_權限設定_設定至Server.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_權限設定_設定至Server.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_權限設定_設定至Server.ON_BorderSize = 5;
            this.plC_Button_權限設定_設定至Server.ON_文字內容 = "上傳資料";
            this.plC_Button_權限設定_設定至Server.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F);
            this.plC_Button_權限設定_設定至Server.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_權限設定_設定至Server.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_權限設定_設定至Server.Size = new System.Drawing.Size(136, 58);
            this.plC_Button_權限設定_設定至Server.State = false;
            this.plC_Button_權限設定_設定至Server.TabIndex = 124;
            this.plC_Button_權限設定_設定至Server.Text = "上傳資料";
            this.plC_Button_權限設定_設定至Server.TextColor = System.Drawing.Color.White;
            this.plC_Button_權限設定_設定至Server.UseVisualStyleBackColor = false;
            this.plC_Button_權限設定_設定至Server.字型鎖住 = false;
            this.plC_Button_權限設定_設定至Server.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.交替型;
            this.plC_Button_權限設定_設定至Server.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_權限設定_設定至Server.文字鎖住 = false;
            this.plC_Button_權限設定_設定至Server.致能讀取位置 = "S39014";
            this.plC_Button_權限設定_設定至Server.讀取位元反向 = false;
            this.plC_Button_權限設定_設定至Server.讀寫鎖住 = false;
            this.plC_Button_權限設定_設定至Server.音效 = true;
            this.plC_Button_權限設定_設定至Server.顯示 = false;
            this.plC_Button_權限設定_設定至Server.顯示狀態 = false;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.White;
            this.tabPage5.Controls.Add(this.plC_ScreenPage_人員資料_開門權限);
            this.tabPage5.Controls.Add(this.panel_人員資料_開門權限);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1102, 404);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "開門權限";
            // 
            // plC_ScreenPage_人員資料_開門權限
            // 
            this.plC_ScreenPage_人員資料_開門權限.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage_人員資料_開門權限.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage7);
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage8);
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage9);
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage10);
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage11);
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage12);
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage13);
            this.plC_ScreenPage_人員資料_開門權限.Controls.Add(this.tabPage14);
            this.plC_ScreenPage_人員資料_開門權限.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage_人員資料_開門權限.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage_人員資料_開門權限.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage_人員資料_開門權限.Location = new System.Drawing.Point(0, 0);
            this.plC_ScreenPage_人員資料_開門權限.Name = "plC_ScreenPage_人員資料_開門權限";
            this.plC_ScreenPage_人員資料_開門權限.SelectedIndex = 0;
            this.plC_ScreenPage_人員資料_開門權限.Size = new System.Drawing.Size(1102, 339);
            this.plC_ScreenPage_人員資料_開門權限.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_人員資料_開門權限.TabIndex = 1;
            this.plC_ScreenPage_人員資料_開門權限.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_人員資料_開門權限.顯示頁面 = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.White;
            this.tabPage7.Controls.Add(this.flowLayoutPanel_開門權限_01);
            this.tabPage7.Location = new System.Drawing.Point(4, 25);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1094, 310);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "01";
            // 
            // flowLayoutPanel_開門權限_01
            // 
            this.flowLayoutPanel_開門權限_01.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_01.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_01.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_01.Name = "flowLayoutPanel_開門權限_01";
            this.flowLayoutPanel_開門權限_01.Size = new System.Drawing.Size(1094, 307);
            this.flowLayoutPanel_開門權限_01.TabIndex = 1;
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.White;
            this.tabPage8.Controls.Add(this.flowLayoutPanel_開門權限_02);
            this.tabPage8.Location = new System.Drawing.Point(4, 25);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(1094, 310);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "02";
            // 
            // flowLayoutPanel_開門權限_02
            // 
            this.flowLayoutPanel_開門權限_02.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_02.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_02.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_02.Name = "flowLayoutPanel_開門權限_02";
            this.flowLayoutPanel_開門權限_02.Size = new System.Drawing.Size(1094, 308);
            this.flowLayoutPanel_開門權限_02.TabIndex = 2;
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.Color.White;
            this.tabPage9.Controls.Add(this.flowLayoutPanel_開門權限_03);
            this.tabPage9.Location = new System.Drawing.Point(4, 25);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(1094, 310);
            this.tabPage9.TabIndex = 2;
            this.tabPage9.Text = "03";
            // 
            // flowLayoutPanel_開門權限_03
            // 
            this.flowLayoutPanel_開門權限_03.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_03.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_03.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_03.Name = "flowLayoutPanel_開門權限_03";
            this.flowLayoutPanel_開門權限_03.Size = new System.Drawing.Size(1094, 308);
            this.flowLayoutPanel_開門權限_03.TabIndex = 2;
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.Color.White;
            this.tabPage10.Controls.Add(this.flowLayoutPanel_開門權限_04);
            this.tabPage10.Location = new System.Drawing.Point(4, 25);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(1094, 310);
            this.tabPage10.TabIndex = 3;
            this.tabPage10.Text = "04";
            // 
            // flowLayoutPanel_開門權限_04
            // 
            this.flowLayoutPanel_開門權限_04.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_04.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_04.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_04.Name = "flowLayoutPanel_開門權限_04";
            this.flowLayoutPanel_開門權限_04.Size = new System.Drawing.Size(1094, 308);
            this.flowLayoutPanel_開門權限_04.TabIndex = 2;
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.White;
            this.tabPage11.Controls.Add(this.flowLayoutPanel_開門權限_05);
            this.tabPage11.Location = new System.Drawing.Point(4, 25);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(1094, 310);
            this.tabPage11.TabIndex = 4;
            this.tabPage11.Text = "05";
            // 
            // flowLayoutPanel_開門權限_05
            // 
            this.flowLayoutPanel_開門權限_05.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_05.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_05.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_05.Name = "flowLayoutPanel_開門權限_05";
            this.flowLayoutPanel_開門權限_05.Size = new System.Drawing.Size(1094, 308);
            this.flowLayoutPanel_開門權限_05.TabIndex = 2;
            // 
            // tabPage12
            // 
            this.tabPage12.BackColor = System.Drawing.Color.White;
            this.tabPage12.Controls.Add(this.flowLayoutPanel_開門權限_06);
            this.tabPage12.Location = new System.Drawing.Point(4, 25);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Size = new System.Drawing.Size(1094, 310);
            this.tabPage12.TabIndex = 5;
            this.tabPage12.Text = "06";
            // 
            // flowLayoutPanel_開門權限_06
            // 
            this.flowLayoutPanel_開門權限_06.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_06.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_06.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_06.Name = "flowLayoutPanel_開門權限_06";
            this.flowLayoutPanel_開門權限_06.Size = new System.Drawing.Size(1094, 308);
            this.flowLayoutPanel_開門權限_06.TabIndex = 2;
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.Color.White;
            this.tabPage13.Controls.Add(this.flowLayoutPanel_開門權限_07);
            this.tabPage13.Location = new System.Drawing.Point(4, 25);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Size = new System.Drawing.Size(1094, 310);
            this.tabPage13.TabIndex = 6;
            this.tabPage13.Text = "07";
            // 
            // flowLayoutPanel_開門權限_07
            // 
            this.flowLayoutPanel_開門權限_07.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_07.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_07.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_07.Name = "flowLayoutPanel_開門權限_07";
            this.flowLayoutPanel_開門權限_07.Size = new System.Drawing.Size(1094, 308);
            this.flowLayoutPanel_開門權限_07.TabIndex = 2;
            // 
            // tabPage14
            // 
            this.tabPage14.BackColor = System.Drawing.Color.White;
            this.tabPage14.Controls.Add(this.flowLayoutPanel_開門權限_08);
            this.tabPage14.Location = new System.Drawing.Point(4, 25);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Size = new System.Drawing.Size(1094, 310);
            this.tabPage14.TabIndex = 7;
            this.tabPage14.Text = "08";
            // 
            // flowLayoutPanel_開門權限_08
            // 
            this.flowLayoutPanel_開門權限_08.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_開門權限_08.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_開門權限_08.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_開門權限_08.Name = "flowLayoutPanel_開門權限_08";
            this.flowLayoutPanel_開門權限_08.Size = new System.Drawing.Size(1094, 308);
            this.flowLayoutPanel_開門權限_08.TabIndex = 2;
            // 
            // panel_人員資料_開門權限
            // 
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton15);
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton14);
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton13);
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton12);
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton11);
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton10);
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton9);
            this.panel_人員資料_開門權限.Controls.Add(this.plC_RJ_ScreenButton8);
            this.panel_人員資料_開門權限.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_人員資料_開門權限.Location = new System.Drawing.Point(0, 339);
            this.panel_人員資料_開門權限.Name = "panel_人員資料_開門權限";
            this.panel_人員資料_開門權限.Size = new System.Drawing.Size(1102, 65);
            this.panel_人員資料_開門權限.TabIndex = 0;
            // 
            // plC_RJ_ScreenButton15
            // 
            this.plC_RJ_ScreenButton15.but_press = false;
            this.plC_RJ_ScreenButton15.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton15.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton15.IconSize = 40;
            this.plC_RJ_ScreenButton15.Location = new System.Drawing.Point(686, 0);
            this.plC_RJ_ScreenButton15.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton15.Name = "plC_RJ_ScreenButton15";
            this.plC_RJ_ScreenButton15.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton15.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton15.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton15.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton15.OffText = "08";
            this.plC_RJ_ScreenButton15.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton15.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton15.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton15.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton15.OnText = "08";
            this.plC_RJ_ScreenButton15.ShowIcon = false;
            this.plC_RJ_ScreenButton15.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton15.TabIndex = 85;
            this.plC_RJ_ScreenButton15.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton15.寫入位置註解 = "";
            this.plC_RJ_ScreenButton15.寫入元件位置 = "";
            this.plC_RJ_ScreenButton15.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton15.控制位址 = "D0";
            this.plC_RJ_ScreenButton15.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton15.致能讀取位置 = "";
            this.plC_RJ_ScreenButton15.讀取位元反向 = false;
            this.plC_RJ_ScreenButton15.讀取位置註解 = "";
            this.plC_RJ_ScreenButton15.讀取元件位置 = "";
            this.plC_RJ_ScreenButton15.音效 = true;
            this.plC_RJ_ScreenButton15.頁面名稱 = "08";
            this.plC_RJ_ScreenButton15.頁面編號 = 0;
            this.plC_RJ_ScreenButton15.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton15.顯示狀態 = false;
            this.plC_RJ_ScreenButton15.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton14
            // 
            this.plC_RJ_ScreenButton14.but_press = false;
            this.plC_RJ_ScreenButton14.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton14.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton14.IconSize = 40;
            this.plC_RJ_ScreenButton14.Location = new System.Drawing.Point(588, 0);
            this.plC_RJ_ScreenButton14.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton14.Name = "plC_RJ_ScreenButton14";
            this.plC_RJ_ScreenButton14.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton14.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton14.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton14.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton14.OffText = "07";
            this.plC_RJ_ScreenButton14.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton14.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton14.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton14.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton14.OnText = "07";
            this.plC_RJ_ScreenButton14.ShowIcon = false;
            this.plC_RJ_ScreenButton14.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton14.TabIndex = 84;
            this.plC_RJ_ScreenButton14.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton14.寫入位置註解 = "";
            this.plC_RJ_ScreenButton14.寫入元件位置 = "";
            this.plC_RJ_ScreenButton14.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton14.控制位址 = "D0";
            this.plC_RJ_ScreenButton14.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton14.致能讀取位置 = "";
            this.plC_RJ_ScreenButton14.讀取位元反向 = false;
            this.plC_RJ_ScreenButton14.讀取位置註解 = "";
            this.plC_RJ_ScreenButton14.讀取元件位置 = "";
            this.plC_RJ_ScreenButton14.音效 = true;
            this.plC_RJ_ScreenButton14.頁面名稱 = "07";
            this.plC_RJ_ScreenButton14.頁面編號 = 0;
            this.plC_RJ_ScreenButton14.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton14.顯示狀態 = false;
            this.plC_RJ_ScreenButton14.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton13
            // 
            this.plC_RJ_ScreenButton13.but_press = false;
            this.plC_RJ_ScreenButton13.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton13.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton13.IconSize = 40;
            this.plC_RJ_ScreenButton13.Location = new System.Drawing.Point(490, 0);
            this.plC_RJ_ScreenButton13.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton13.Name = "plC_RJ_ScreenButton13";
            this.plC_RJ_ScreenButton13.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton13.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton13.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton13.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton13.OffText = "06";
            this.plC_RJ_ScreenButton13.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton13.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton13.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton13.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton13.OnText = "06";
            this.plC_RJ_ScreenButton13.ShowIcon = false;
            this.plC_RJ_ScreenButton13.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton13.TabIndex = 83;
            this.plC_RJ_ScreenButton13.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton13.寫入位置註解 = "";
            this.plC_RJ_ScreenButton13.寫入元件位置 = "";
            this.plC_RJ_ScreenButton13.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton13.控制位址 = "D0";
            this.plC_RJ_ScreenButton13.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton13.致能讀取位置 = "";
            this.plC_RJ_ScreenButton13.讀取位元反向 = false;
            this.plC_RJ_ScreenButton13.讀取位置註解 = "";
            this.plC_RJ_ScreenButton13.讀取元件位置 = "";
            this.plC_RJ_ScreenButton13.音效 = true;
            this.plC_RJ_ScreenButton13.頁面名稱 = "06";
            this.plC_RJ_ScreenButton13.頁面編號 = 0;
            this.plC_RJ_ScreenButton13.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton13.顯示狀態 = false;
            this.plC_RJ_ScreenButton13.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton12
            // 
            this.plC_RJ_ScreenButton12.but_press = false;
            this.plC_RJ_ScreenButton12.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton12.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton12.IconSize = 40;
            this.plC_RJ_ScreenButton12.Location = new System.Drawing.Point(392, 0);
            this.plC_RJ_ScreenButton12.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton12.Name = "plC_RJ_ScreenButton12";
            this.plC_RJ_ScreenButton12.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton12.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton12.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton12.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton12.OffText = "05";
            this.plC_RJ_ScreenButton12.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton12.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton12.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton12.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton12.OnText = "05";
            this.plC_RJ_ScreenButton12.ShowIcon = false;
            this.plC_RJ_ScreenButton12.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton12.TabIndex = 82;
            this.plC_RJ_ScreenButton12.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton12.寫入位置註解 = "";
            this.plC_RJ_ScreenButton12.寫入元件位置 = "";
            this.plC_RJ_ScreenButton12.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton12.控制位址 = "D0";
            this.plC_RJ_ScreenButton12.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton12.致能讀取位置 = "";
            this.plC_RJ_ScreenButton12.讀取位元反向 = false;
            this.plC_RJ_ScreenButton12.讀取位置註解 = "";
            this.plC_RJ_ScreenButton12.讀取元件位置 = "";
            this.plC_RJ_ScreenButton12.音效 = true;
            this.plC_RJ_ScreenButton12.頁面名稱 = "05";
            this.plC_RJ_ScreenButton12.頁面編號 = 0;
            this.plC_RJ_ScreenButton12.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton12.顯示狀態 = false;
            this.plC_RJ_ScreenButton12.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton11
            // 
            this.plC_RJ_ScreenButton11.but_press = false;
            this.plC_RJ_ScreenButton11.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton11.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton11.IconSize = 40;
            this.plC_RJ_ScreenButton11.Location = new System.Drawing.Point(294, 0);
            this.plC_RJ_ScreenButton11.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton11.Name = "plC_RJ_ScreenButton11";
            this.plC_RJ_ScreenButton11.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton11.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton11.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton11.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton11.OffText = "04";
            this.plC_RJ_ScreenButton11.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton11.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton11.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton11.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton11.OnText = "04";
            this.plC_RJ_ScreenButton11.ShowIcon = false;
            this.plC_RJ_ScreenButton11.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton11.TabIndex = 81;
            this.plC_RJ_ScreenButton11.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton11.寫入位置註解 = "";
            this.plC_RJ_ScreenButton11.寫入元件位置 = "";
            this.plC_RJ_ScreenButton11.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton11.控制位址 = "D0";
            this.plC_RJ_ScreenButton11.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton11.致能讀取位置 = "";
            this.plC_RJ_ScreenButton11.讀取位元反向 = false;
            this.plC_RJ_ScreenButton11.讀取位置註解 = "";
            this.plC_RJ_ScreenButton11.讀取元件位置 = "";
            this.plC_RJ_ScreenButton11.音效 = true;
            this.plC_RJ_ScreenButton11.頁面名稱 = "04";
            this.plC_RJ_ScreenButton11.頁面編號 = 0;
            this.plC_RJ_ScreenButton11.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton11.顯示狀態 = false;
            this.plC_RJ_ScreenButton11.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton10
            // 
            this.plC_RJ_ScreenButton10.but_press = false;
            this.plC_RJ_ScreenButton10.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton10.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton10.IconSize = 40;
            this.plC_RJ_ScreenButton10.Location = new System.Drawing.Point(196, 0);
            this.plC_RJ_ScreenButton10.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton10.Name = "plC_RJ_ScreenButton10";
            this.plC_RJ_ScreenButton10.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton10.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton10.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton10.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton10.OffText = "03";
            this.plC_RJ_ScreenButton10.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton10.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton10.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton10.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton10.OnText = "03";
            this.plC_RJ_ScreenButton10.ShowIcon = false;
            this.plC_RJ_ScreenButton10.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton10.TabIndex = 80;
            this.plC_RJ_ScreenButton10.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton10.寫入位置註解 = "";
            this.plC_RJ_ScreenButton10.寫入元件位置 = "";
            this.plC_RJ_ScreenButton10.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton10.控制位址 = "D0";
            this.plC_RJ_ScreenButton10.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton10.致能讀取位置 = "";
            this.plC_RJ_ScreenButton10.讀取位元反向 = false;
            this.plC_RJ_ScreenButton10.讀取位置註解 = "";
            this.plC_RJ_ScreenButton10.讀取元件位置 = "";
            this.plC_RJ_ScreenButton10.音效 = true;
            this.plC_RJ_ScreenButton10.頁面名稱 = "03";
            this.plC_RJ_ScreenButton10.頁面編號 = 0;
            this.plC_RJ_ScreenButton10.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton10.顯示狀態 = false;
            this.plC_RJ_ScreenButton10.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton9
            // 
            this.plC_RJ_ScreenButton9.but_press = false;
            this.plC_RJ_ScreenButton9.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton9.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton9.IconSize = 40;
            this.plC_RJ_ScreenButton9.Location = new System.Drawing.Point(98, 0);
            this.plC_RJ_ScreenButton9.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton9.Name = "plC_RJ_ScreenButton9";
            this.plC_RJ_ScreenButton9.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton9.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton9.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton9.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton9.OffText = "02";
            this.plC_RJ_ScreenButton9.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton9.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton9.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton9.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton9.OnText = "02";
            this.plC_RJ_ScreenButton9.ShowIcon = false;
            this.plC_RJ_ScreenButton9.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton9.TabIndex = 79;
            this.plC_RJ_ScreenButton9.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton9.寫入位置註解 = "";
            this.plC_RJ_ScreenButton9.寫入元件位置 = "";
            this.plC_RJ_ScreenButton9.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton9.控制位址 = "D0";
            this.plC_RJ_ScreenButton9.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton9.致能讀取位置 = "";
            this.plC_RJ_ScreenButton9.讀取位元反向 = false;
            this.plC_RJ_ScreenButton9.讀取位置註解 = "";
            this.plC_RJ_ScreenButton9.讀取元件位置 = "";
            this.plC_RJ_ScreenButton9.音效 = true;
            this.plC_RJ_ScreenButton9.頁面名稱 = "02";
            this.plC_RJ_ScreenButton9.頁面編號 = 0;
            this.plC_RJ_ScreenButton9.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton9.顯示狀態 = false;
            this.plC_RJ_ScreenButton9.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton8
            // 
            this.plC_RJ_ScreenButton8.but_press = false;
            this.plC_RJ_ScreenButton8.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton8.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton8.IconSize = 40;
            this.plC_RJ_ScreenButton8.Location = new System.Drawing.Point(0, 0);
            this.plC_RJ_ScreenButton8.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton8.Name = "plC_RJ_ScreenButton8";
            this.plC_RJ_ScreenButton8.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton8.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton8.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton8.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton8.OffText = "01";
            this.plC_RJ_ScreenButton8.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton8.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton8.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton8.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton8.OnText = "01";
            this.plC_RJ_ScreenButton8.ShowIcon = false;
            this.plC_RJ_ScreenButton8.Size = new System.Drawing.Size(98, 65);
            this.plC_RJ_ScreenButton8.TabIndex = 78;
            this.plC_RJ_ScreenButton8.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton8.寫入位置註解 = "";
            this.plC_RJ_ScreenButton8.寫入元件位置 = "";
            this.plC_RJ_ScreenButton8.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton8.控制位址 = "D0";
            this.plC_RJ_ScreenButton8.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton8.致能讀取位置 = "";
            this.plC_RJ_ScreenButton8.讀取位元反向 = false;
            this.plC_RJ_ScreenButton8.讀取位置註解 = "";
            this.plC_RJ_ScreenButton8.讀取元件位置 = "";
            this.plC_RJ_ScreenButton8.音效 = true;
            this.plC_RJ_ScreenButton8.頁面名稱 = "01";
            this.plC_RJ_ScreenButton8.頁面編號 = 0;
            this.plC_RJ_ScreenButton8.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton8.顯示狀態 = false;
            this.plC_RJ_ScreenButton8.顯示讀取位置 = "";
            // 
            // panel_人員資料_權限設定
            // 
            this.panel_人員資料_權限設定.Controls.Add(this.plC_RJ_ScreenButton7);
            this.panel_人員資料_權限設定.Controls.Add(this.plC_RJ_ScreenButton4);
            this.panel_人員資料_權限設定.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_人員資料_權限設定.Location = new System.Drawing.Point(529, 516);
            this.panel_人員資料_權限設定.Name = "panel_人員資料_權限設定";
            this.panel_人員資料_權限設定.Size = new System.Drawing.Size(1110, 57);
            this.panel_人員資料_權限設定.TabIndex = 115;
            // 
            // plC_RJ_ScreenButton7
            // 
            this.plC_RJ_ScreenButton7.but_press = false;
            this.plC_RJ_ScreenButton7.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton7.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton7.IconSize = 40;
            this.plC_RJ_ScreenButton7.Location = new System.Drawing.Point(166, 0);
            this.plC_RJ_ScreenButton7.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton7.Name = "plC_RJ_ScreenButton7";
            this.plC_RJ_ScreenButton7.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton7.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton7.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton7.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton7.OffText = "開門權限";
            this.plC_RJ_ScreenButton7.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton7.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton7.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton7.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton7.OnText = "開門權限";
            this.plC_RJ_ScreenButton7.ShowIcon = false;
            this.plC_RJ_ScreenButton7.Size = new System.Drawing.Size(166, 57);
            this.plC_RJ_ScreenButton7.TabIndex = 77;
            this.plC_RJ_ScreenButton7.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton7.寫入位置註解 = "";
            this.plC_RJ_ScreenButton7.寫入元件位置 = "";
            this.plC_RJ_ScreenButton7.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton7.控制位址 = "D0";
            this.plC_RJ_ScreenButton7.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton7.致能讀取位置 = "";
            this.plC_RJ_ScreenButton7.讀取位元反向 = false;
            this.plC_RJ_ScreenButton7.讀取位置註解 = "";
            this.plC_RJ_ScreenButton7.讀取元件位置 = "";
            this.plC_RJ_ScreenButton7.音效 = true;
            this.plC_RJ_ScreenButton7.頁面名稱 = "開門權限";
            this.plC_RJ_ScreenButton7.頁面編號 = 0;
            this.plC_RJ_ScreenButton7.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton7.顯示狀態 = false;
            this.plC_RJ_ScreenButton7.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton4
            // 
            this.plC_RJ_ScreenButton4.but_press = false;
            this.plC_RJ_ScreenButton4.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton4.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton4.IconSize = 40;
            this.plC_RJ_ScreenButton4.Location = new System.Drawing.Point(0, 0);
            this.plC_RJ_ScreenButton4.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton4.Name = "plC_RJ_ScreenButton4";
            this.plC_RJ_ScreenButton4.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton4.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton4.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton4.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton4.OffText = "一般權限";
            this.plC_RJ_ScreenButton4.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton4.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton4.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton4.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton4.OnText = "一般權限";
            this.plC_RJ_ScreenButton4.ShowIcon = false;
            this.plC_RJ_ScreenButton4.Size = new System.Drawing.Size(166, 57);
            this.plC_RJ_ScreenButton4.TabIndex = 76;
            this.plC_RJ_ScreenButton4.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton4.寫入位置註解 = "";
            this.plC_RJ_ScreenButton4.寫入元件位置 = "";
            this.plC_RJ_ScreenButton4.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton4.控制位址 = "D0";
            this.plC_RJ_ScreenButton4.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton4.致能讀取位置 = "";
            this.plC_RJ_ScreenButton4.讀取位元反向 = false;
            this.plC_RJ_ScreenButton4.讀取位置註解 = "";
            this.plC_RJ_ScreenButton4.讀取元件位置 = "";
            this.plC_RJ_ScreenButton4.音效 = true;
            this.plC_RJ_ScreenButton4.頁面名稱 = "一般權限";
            this.plC_RJ_ScreenButton4.頁面編號 = 0;
            this.plC_RJ_ScreenButton4.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton4.顯示狀態 = false;
            this.plC_RJ_ScreenButton4.顯示讀取位置 = "";
            // 
            // rJ_GroupBox20
            // 
            // 
            // rJ_GroupBox20.ContentsPanel
            // 
            this.rJ_GroupBox20.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.rJ_GroupBox20.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox20.ContentsPanel.BorderRadius = 2;
            this.rJ_GroupBox20.ContentsPanel.BorderSize = 2;
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.plC_RJ_Button_人員資料_開門權限全關);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.plC_RJ_Button_人員資料_開門權限全開);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.plC_RJ_Button_人員資料_清除內容);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.plC_RJ_Button_人員資料_刪除);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.plC_RJ_Button_人員資料_登錄);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.plC_RJ_Button_人員資料_匯入);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.plC_RJ_Button_人員資料_匯出);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.tableLayoutPanel5);
            this.rJ_GroupBox20.ContentsPanel.Controls.Add(this.rJ_TextBox_人員資料_識別圖案);
            this.rJ_GroupBox20.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox20.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox20.ContentsPanel.IsSelected = false;
            this.rJ_GroupBox20.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.rJ_GroupBox20.ContentsPanel.Name = "ContentsPanel";
            this.rJ_GroupBox20.ContentsPanel.Padding = new System.Windows.Forms.Padding(3);
            this.rJ_GroupBox20.ContentsPanel.Size = new System.Drawing.Size(529, 453);
            this.rJ_GroupBox20.ContentsPanel.TabIndex = 2;
            this.rJ_GroupBox20.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_GroupBox20.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_GroupBox20.GUID = "";
            this.rJ_GroupBox20.Location = new System.Drawing.Point(0, 516);
            this.rJ_GroupBox20.Name = "rJ_GroupBox20";
            this.rJ_GroupBox20.PannelBackColor = System.Drawing.Color.White;
            this.rJ_GroupBox20.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox20.PannelBorderRadius = 2;
            this.rJ_GroupBox20.PannelBorderSize = 2;
            this.rJ_GroupBox20.Size = new System.Drawing.Size(529, 490);
            this.rJ_GroupBox20.TabIndex = 114;
            this.rJ_GroupBox20.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_GroupBox20.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_GroupBox20.TitleBorderRadius = 5;
            this.rJ_GroupBox20.TitleBorderSize = 0;
            this.rJ_GroupBox20.TitleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_GroupBox20.TitleForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox20.TitleHeight = 37;
            this.rJ_GroupBox20.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_GroupBox20.TitleTexts = "資料內容";
            // 
            // plC_RJ_Button_人員資料_開門權限全關
            // 
            this.plC_RJ_Button_人員資料_開門權限全關.AutoResetState = false;
            this.plC_RJ_Button_人員資料_開門權限全關.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_人員資料_開門權限全關.Bool = false;
            this.plC_RJ_Button_人員資料_開門權限全關.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_人員資料_開門權限全關.BorderRadius = 5;
            this.plC_RJ_Button_人員資料_開門權限全關.BorderSize = 0;
            this.plC_RJ_Button_人員資料_開門權限全關.but_press = false;
            this.plC_RJ_Button_人員資料_開門權限全關.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_人員資料_開門權限全關.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_人員資料_開門權限全關.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_人員資料_開門權限全關.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_開門權限全關.GUID = "";
            this.plC_RJ_Button_人員資料_開門權限全關.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_人員資料_開門權限全關.Location = new System.Drawing.Point(157, 384);
            this.plC_RJ_Button_人員資料_開門權限全關.Name = "plC_RJ_Button_人員資料_開門權限全關";
            this.plC_RJ_Button_人員資料_開門權限全關.OFF_文字內容 = "開門權限全關";
            this.plC_RJ_Button_人員資料_開門權限全關.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_開門權限全關.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_開門權限全關.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_開門權限全關.ON_BorderSize = 5;
            this.plC_RJ_Button_人員資料_開門權限全關.ON_文字內容 = "開門權限全關";
            this.plC_RJ_Button_人員資料_開門權限全關.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_開門權限全關.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_人員資料_開門權限全關.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_開門權限全關.Size = new System.Drawing.Size(180, 56);
            this.plC_RJ_Button_人員資料_開門權限全關.State = false;
            this.plC_RJ_Button_人員資料_開門權限全關.TabIndex = 138;
            this.plC_RJ_Button_人員資料_開門權限全關.Text = "開門權限全關";
            this.plC_RJ_Button_人員資料_開門權限全關.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_開門權限全關.Texts = "開門權限全關";
            this.plC_RJ_Button_人員資料_開門權限全關.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_人員資料_開門權限全關.字型鎖住 = false;
            this.plC_RJ_Button_人員資料_開門權限全關.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_人員資料_開門權限全關.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_人員資料_開門權限全關.文字鎖住 = false;
            this.plC_RJ_Button_人員資料_開門權限全關.讀取位元反向 = false;
            this.plC_RJ_Button_人員資料_開門權限全關.讀寫鎖住 = false;
            this.plC_RJ_Button_人員資料_開門權限全關.音效 = true;
            this.plC_RJ_Button_人員資料_開門權限全關.顯示 = false;
            this.plC_RJ_Button_人員資料_開門權限全關.顯示狀態 = false;
            // 
            // plC_RJ_Button_人員資料_開門權限全開
            // 
            this.plC_RJ_Button_人員資料_開門權限全開.AutoResetState = false;
            this.plC_RJ_Button_人員資料_開門權限全開.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_人員資料_開門權限全開.Bool = false;
            this.plC_RJ_Button_人員資料_開門權限全開.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_人員資料_開門權限全開.BorderRadius = 5;
            this.plC_RJ_Button_人員資料_開門權限全開.BorderSize = 0;
            this.plC_RJ_Button_人員資料_開門權限全開.but_press = false;
            this.plC_RJ_Button_人員資料_開門權限全開.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_人員資料_開門權限全開.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_人員資料_開門權限全開.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_人員資料_開門權限全開.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_開門權限全開.GUID = "";
            this.plC_RJ_Button_人員資料_開門權限全開.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_人員資料_開門權限全開.Location = new System.Drawing.Point(343, 384);
            this.plC_RJ_Button_人員資料_開門權限全開.Name = "plC_RJ_Button_人員資料_開門權限全開";
            this.plC_RJ_Button_人員資料_開門權限全開.OFF_文字內容 = "開門權限全開";
            this.plC_RJ_Button_人員資料_開門權限全開.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_開門權限全開.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_開門權限全開.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_開門權限全開.ON_BorderSize = 5;
            this.plC_RJ_Button_人員資料_開門權限全開.ON_文字內容 = "開門權限全開";
            this.plC_RJ_Button_人員資料_開門權限全開.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_開門權限全開.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_人員資料_開門權限全開.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_開門權限全開.Size = new System.Drawing.Size(180, 56);
            this.plC_RJ_Button_人員資料_開門權限全開.State = false;
            this.plC_RJ_Button_人員資料_開門權限全開.TabIndex = 137;
            this.plC_RJ_Button_人員資料_開門權限全開.Text = "開門權限全開";
            this.plC_RJ_Button_人員資料_開門權限全開.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_開門權限全開.Texts = "開門權限全開";
            this.plC_RJ_Button_人員資料_開門權限全開.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_人員資料_開門權限全開.字型鎖住 = false;
            this.plC_RJ_Button_人員資料_開門權限全開.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_人員資料_開門權限全開.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_人員資料_開門權限全開.文字鎖住 = false;
            this.plC_RJ_Button_人員資料_開門權限全開.讀取位元反向 = false;
            this.plC_RJ_Button_人員資料_開門權限全開.讀寫鎖住 = false;
            this.plC_RJ_Button_人員資料_開門權限全開.音效 = true;
            this.plC_RJ_Button_人員資料_開門權限全開.顯示 = false;
            this.plC_RJ_Button_人員資料_開門權限全開.顯示狀態 = false;
            // 
            // plC_RJ_Button_人員資料_清除內容
            // 
            this.plC_RJ_Button_人員資料_清除內容.AutoResetState = false;
            this.plC_RJ_Button_人員資料_清除內容.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_人員資料_清除內容.Bool = false;
            this.plC_RJ_Button_人員資料_清除內容.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_人員資料_清除內容.BorderRadius = 5;
            this.plC_RJ_Button_人員資料_清除內容.BorderSize = 0;
            this.plC_RJ_Button_人員資料_清除內容.but_press = false;
            this.plC_RJ_Button_人員資料_清除內容.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_人員資料_清除內容.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_人員資料_清除內容.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_人員資料_清除內容.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_清除內容.GUID = "";
            this.plC_RJ_Button_人員資料_清除內容.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_人員資料_清除內容.Location = new System.Drawing.Point(343, 331);
            this.plC_RJ_Button_人員資料_清除內容.Name = "plC_RJ_Button_人員資料_清除內容";
            this.plC_RJ_Button_人員資料_清除內容.OFF_文字內容 = "清除內容";
            this.plC_RJ_Button_人員資料_清除內容.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_清除內容.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_清除內容.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_清除內容.ON_BorderSize = 5;
            this.plC_RJ_Button_人員資料_清除內容.ON_文字內容 = "清除內容";
            this.plC_RJ_Button_人員資料_清除內容.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_清除內容.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_人員資料_清除內容.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_清除內容.Size = new System.Drawing.Size(117, 46);
            this.plC_RJ_Button_人員資料_清除內容.State = false;
            this.plC_RJ_Button_人員資料_清除內容.TabIndex = 136;
            this.plC_RJ_Button_人員資料_清除內容.Text = "清除內容";
            this.plC_RJ_Button_人員資料_清除內容.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_清除內容.Texts = "清除內容";
            this.plC_RJ_Button_人員資料_清除內容.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_人員資料_清除內容.字型鎖住 = false;
            this.plC_RJ_Button_人員資料_清除內容.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_人員資料_清除內容.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_人員資料_清除內容.文字鎖住 = false;
            this.plC_RJ_Button_人員資料_清除內容.讀取位元反向 = false;
            this.plC_RJ_Button_人員資料_清除內容.讀寫鎖住 = false;
            this.plC_RJ_Button_人員資料_清除內容.音效 = true;
            this.plC_RJ_Button_人員資料_清除內容.顯示 = false;
            this.plC_RJ_Button_人員資料_清除內容.顯示狀態 = false;
            // 
            // plC_RJ_Button_人員資料_刪除
            // 
            this.plC_RJ_Button_人員資料_刪除.AutoResetState = false;
            this.plC_RJ_Button_人員資料_刪除.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_人員資料_刪除.Bool = false;
            this.plC_RJ_Button_人員資料_刪除.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_人員資料_刪除.BorderRadius = 5;
            this.plC_RJ_Button_人員資料_刪除.BorderSize = 0;
            this.plC_RJ_Button_人員資料_刪除.but_press = false;
            this.plC_RJ_Button_人員資料_刪除.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_人員資料_刪除.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_人員資料_刪除.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_人員資料_刪除.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_刪除.GUID = "";
            this.plC_RJ_Button_人員資料_刪除.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_人員資料_刪除.Location = new System.Drawing.Point(262, 331);
            this.plC_RJ_Button_人員資料_刪除.Name = "plC_RJ_Button_人員資料_刪除";
            this.plC_RJ_Button_人員資料_刪除.OFF_文字內容 = "刪除";
            this.plC_RJ_Button_人員資料_刪除.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_刪除.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_刪除.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_刪除.ON_BorderSize = 5;
            this.plC_RJ_Button_人員資料_刪除.ON_文字內容 = "刪除";
            this.plC_RJ_Button_人員資料_刪除.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_刪除.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_人員資料_刪除.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_刪除.Size = new System.Drawing.Size(75, 46);
            this.plC_RJ_Button_人員資料_刪除.State = false;
            this.plC_RJ_Button_人員資料_刪除.TabIndex = 133;
            this.plC_RJ_Button_人員資料_刪除.Text = "刪除";
            this.plC_RJ_Button_人員資料_刪除.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_刪除.Texts = "刪除";
            this.plC_RJ_Button_人員資料_刪除.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_人員資料_刪除.字型鎖住 = false;
            this.plC_RJ_Button_人員資料_刪除.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_人員資料_刪除.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_人員資料_刪除.文字鎖住 = false;
            this.plC_RJ_Button_人員資料_刪除.讀取位元反向 = false;
            this.plC_RJ_Button_人員資料_刪除.讀寫鎖住 = false;
            this.plC_RJ_Button_人員資料_刪除.音效 = true;
            this.plC_RJ_Button_人員資料_刪除.顯示 = false;
            this.plC_RJ_Button_人員資料_刪除.顯示狀態 = false;
            // 
            // plC_RJ_Button_人員資料_登錄
            // 
            this.plC_RJ_Button_人員資料_登錄.AutoResetState = false;
            this.plC_RJ_Button_人員資料_登錄.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_人員資料_登錄.Bool = false;
            this.plC_RJ_Button_人員資料_登錄.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_人員資料_登錄.BorderRadius = 5;
            this.plC_RJ_Button_人員資料_登錄.BorderSize = 0;
            this.plC_RJ_Button_人員資料_登錄.but_press = false;
            this.plC_RJ_Button_人員資料_登錄.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_人員資料_登錄.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_人員資料_登錄.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_人員資料_登錄.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_登錄.GUID = "";
            this.plC_RJ_Button_人員資料_登錄.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_人員資料_登錄.Location = new System.Drawing.Point(181, 331);
            this.plC_RJ_Button_人員資料_登錄.Name = "plC_RJ_Button_人員資料_登錄";
            this.plC_RJ_Button_人員資料_登錄.OFF_文字內容 = "登錄";
            this.plC_RJ_Button_人員資料_登錄.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_登錄.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_登錄.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_登錄.ON_BorderSize = 5;
            this.plC_RJ_Button_人員資料_登錄.ON_文字內容 = "登錄";
            this.plC_RJ_Button_人員資料_登錄.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_登錄.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_人員資料_登錄.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_登錄.Size = new System.Drawing.Size(75, 46);
            this.plC_RJ_Button_人員資料_登錄.State = false;
            this.plC_RJ_Button_人員資料_登錄.TabIndex = 132;
            this.plC_RJ_Button_人員資料_登錄.Text = "登錄";
            this.plC_RJ_Button_人員資料_登錄.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_登錄.Texts = "登錄";
            this.plC_RJ_Button_人員資料_登錄.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_人員資料_登錄.字型鎖住 = false;
            this.plC_RJ_Button_人員資料_登錄.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_人員資料_登錄.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_人員資料_登錄.文字鎖住 = false;
            this.plC_RJ_Button_人員資料_登錄.讀取位元反向 = false;
            this.plC_RJ_Button_人員資料_登錄.讀寫鎖住 = false;
            this.plC_RJ_Button_人員資料_登錄.音效 = true;
            this.plC_RJ_Button_人員資料_登錄.顯示 = false;
            this.plC_RJ_Button_人員資料_登錄.顯示狀態 = false;
            // 
            // plC_RJ_Button_人員資料_匯入
            // 
            this.plC_RJ_Button_人員資料_匯入.AutoResetState = false;
            this.plC_RJ_Button_人員資料_匯入.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_人員資料_匯入.Bool = false;
            this.plC_RJ_Button_人員資料_匯入.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_人員資料_匯入.BorderRadius = 5;
            this.plC_RJ_Button_人員資料_匯入.BorderSize = 0;
            this.plC_RJ_Button_人員資料_匯入.but_press = false;
            this.plC_RJ_Button_人員資料_匯入.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_人員資料_匯入.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_人員資料_匯入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_人員資料_匯入.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_匯入.GUID = "";
            this.plC_RJ_Button_人員資料_匯入.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_人員資料_匯入.Location = new System.Drawing.Point(100, 331);
            this.plC_RJ_Button_人員資料_匯入.Name = "plC_RJ_Button_人員資料_匯入";
            this.plC_RJ_Button_人員資料_匯入.OFF_文字內容 = "匯入";
            this.plC_RJ_Button_人員資料_匯入.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_匯入.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_匯入.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_匯入.ON_BorderSize = 5;
            this.plC_RJ_Button_人員資料_匯入.ON_文字內容 = "匯入";
            this.plC_RJ_Button_人員資料_匯入.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_匯入.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_人員資料_匯入.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_匯入.Size = new System.Drawing.Size(75, 46);
            this.plC_RJ_Button_人員資料_匯入.State = false;
            this.plC_RJ_Button_人員資料_匯入.TabIndex = 131;
            this.plC_RJ_Button_人員資料_匯入.Text = "匯入";
            this.plC_RJ_Button_人員資料_匯入.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_匯入.Texts = "匯入";
            this.plC_RJ_Button_人員資料_匯入.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_人員資料_匯入.字型鎖住 = false;
            this.plC_RJ_Button_人員資料_匯入.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_人員資料_匯入.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_人員資料_匯入.文字鎖住 = false;
            this.plC_RJ_Button_人員資料_匯入.讀取位元反向 = false;
            this.plC_RJ_Button_人員資料_匯入.讀寫鎖住 = false;
            this.plC_RJ_Button_人員資料_匯入.音效 = true;
            this.plC_RJ_Button_人員資料_匯入.顯示 = false;
            this.plC_RJ_Button_人員資料_匯入.顯示狀態 = false;
            // 
            // plC_RJ_Button_人員資料_匯出
            // 
            this.plC_RJ_Button_人員資料_匯出.AutoResetState = false;
            this.plC_RJ_Button_人員資料_匯出.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_人員資料_匯出.Bool = false;
            this.plC_RJ_Button_人員資料_匯出.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_人員資料_匯出.BorderRadius = 5;
            this.plC_RJ_Button_人員資料_匯出.BorderSize = 0;
            this.plC_RJ_Button_人員資料_匯出.but_press = false;
            this.plC_RJ_Button_人員資料_匯出.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_人員資料_匯出.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_人員資料_匯出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_人員資料_匯出.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_匯出.GUID = "";
            this.plC_RJ_Button_人員資料_匯出.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_人員資料_匯出.Location = new System.Drawing.Point(19, 331);
            this.plC_RJ_Button_人員資料_匯出.Name = "plC_RJ_Button_人員資料_匯出";
            this.plC_RJ_Button_人員資料_匯出.OFF_文字內容 = "匯出";
            this.plC_RJ_Button_人員資料_匯出.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_匯出.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_匯出.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_匯出.ON_BorderSize = 5;
            this.plC_RJ_Button_人員資料_匯出.ON_文字內容 = "匯出";
            this.plC_RJ_Button_人員資料_匯出.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_人員資料_匯出.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_人員資料_匯出.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_人員資料_匯出.Size = new System.Drawing.Size(75, 46);
            this.plC_RJ_Button_人員資料_匯出.State = false;
            this.plC_RJ_Button_人員資料_匯出.TabIndex = 130;
            this.plC_RJ_Button_人員資料_匯出.Text = "匯出";
            this.plC_RJ_Button_人員資料_匯出.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_人員資料_匯出.Texts = "匯出";
            this.plC_RJ_Button_人員資料_匯出.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_人員資料_匯出.字型鎖住 = false;
            this.plC_RJ_Button_人員資料_匯出.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_人員資料_匯出.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_人員資料_匯出.文字鎖住 = false;
            this.plC_RJ_Button_人員資料_匯出.讀取位元反向 = false;
            this.plC_RJ_Button_人員資料_匯出.讀寫鎖住 = false;
            this.plC_RJ_Button_人員資料_匯出.音效 = true;
            this.plC_RJ_Button_人員資料_匯出.顯示 = false;
            this.plC_RJ_Button_人員資料_匯出.顯示狀態 = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.00766F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.99234F));
            this.tableLayoutPanel5.Controls.Add(this.rJ_TextBox_人員資料_一維條碼, 1, 8);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 8);
            this.tableLayoutPanel5.Controls.Add(this.rJ_TextBox_人員資料_卡號, 1, 7);
            this.tableLayoutPanel5.Controls.Add(this.comboBox_人員資料_權限等級, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.rJ_TextBox_人員資料_單位, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.rJ_TextBox_人員資料_密碼, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.rJ_TextBox_人員資料_姓名, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.rJ_TextBox_人員資料_ID, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label49, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label144, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label108, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.label138, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.label143, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label102, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.label44, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.label17, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.panel149, 1, 6);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel2, 1, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 14;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(523, 325);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // rJ_TextBox_人員資料_一維條碼
            // 
            this.rJ_TextBox_人員資料_一維條碼.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_人員資料_一維條碼.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_TextBox_人員資料_一維條碼.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_人員資料_一維條碼.BorderRadius = 0;
            this.rJ_TextBox_人員資料_一維條碼.BorderSize = 2;
            this.rJ_TextBox_人員資料_一維條碼.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_人員資料_一維條碼.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_人員資料_一維條碼.GUID = "";
            this.rJ_TextBox_人員資料_一維條碼.Location = new System.Drawing.Point(98, 292);
            this.rJ_TextBox_人員資料_一維條碼.Multiline = false;
            this.rJ_TextBox_人員資料_一維條碼.Name = "rJ_TextBox_人員資料_一維條碼";
            this.rJ_TextBox_人員資料_一維條碼.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_人員資料_一維條碼.PassWordChar = false;
            this.rJ_TextBox_人員資料_一維條碼.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_人員資料_一維條碼.PlaceholderText = "";
            this.rJ_TextBox_人員資料_一維條碼.ShowTouchPannel = false;
            this.rJ_TextBox_人員資料_一維條碼.Size = new System.Drawing.Size(421, 30);
            this.rJ_TextBox_人員資料_一維條碼.TabIndex = 134;
            this.rJ_TextBox_人員資料_一維條碼.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_人員資料_一維條碼.Texts = "";
            this.rJ_TextBox_人員資料_一維條碼.UnderlineStyle = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightCyan;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 35);
            this.label1.TabIndex = 134;
            this.label1.Text = "一維條碼";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rJ_TextBox_人員資料_卡號
            // 
            this.rJ_TextBox_人員資料_卡號.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_人員資料_卡號.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_TextBox_人員資料_卡號.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_人員資料_卡號.BorderRadius = 0;
            this.rJ_TextBox_人員資料_卡號.BorderSize = 2;
            this.rJ_TextBox_人員資料_卡號.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_人員資料_卡號.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_人員資料_卡號.GUID = "";
            this.rJ_TextBox_人員資料_卡號.Location = new System.Drawing.Point(98, 256);
            this.rJ_TextBox_人員資料_卡號.Multiline = false;
            this.rJ_TextBox_人員資料_卡號.Name = "rJ_TextBox_人員資料_卡號";
            this.rJ_TextBox_人員資料_卡號.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_人員資料_卡號.PassWordChar = false;
            this.rJ_TextBox_人員資料_卡號.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_人員資料_卡號.PlaceholderText = "";
            this.rJ_TextBox_人員資料_卡號.ShowTouchPannel = false;
            this.rJ_TextBox_人員資料_卡號.Size = new System.Drawing.Size(421, 30);
            this.rJ_TextBox_人員資料_卡號.TabIndex = 113;
            this.rJ_TextBox_人員資料_卡號.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_人員資料_卡號.Texts = "";
            this.rJ_TextBox_人員資料_卡號.UnderlineStyle = false;
            // 
            // comboBox_人員資料_權限等級
            // 
            this.comboBox_人員資料_權限等級.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBox_人員資料_權限等級.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.comboBox_人員資料_權限等級.BorderSize = 1;
            this.comboBox_人員資料_權限等級.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBox_人員資料_權限等級.ForeColor = System.Drawing.Color.DimGray;
            this.comboBox_人員資料_權限等級.GUID = "";
            this.comboBox_人員資料_權限等級.IconColor = System.Drawing.Color.RoyalBlue;
            this.comboBox_人員資料_權限等級.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.comboBox_人員資料_權限等級.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.comboBox_人員資料_權限等級.ListTextColor = System.Drawing.Color.DimGray;
            this.comboBox_人員資料_權限等級.Location = new System.Drawing.Point(98, 184);
            this.comboBox_人員資料_權限等級.MinimumSize = new System.Drawing.Size(50, 30);
            this.comboBox_人員資料_權限等級.Name = "comboBox_人員資料_權限等級";
            this.comboBox_人員資料_權限等級.Padding = new System.Windows.Forms.Padding(1);
            this.comboBox_人員資料_權限等級.Size = new System.Drawing.Size(119, 30);
            this.comboBox_人員資料_權限等級.TabIndex = 111;
            this.comboBox_人員資料_權限等級.Texts = "";
            // 
            // rJ_TextBox_人員資料_單位
            // 
            this.rJ_TextBox_人員資料_單位.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_人員資料_單位.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_TextBox_人員資料_單位.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_人員資料_單位.BorderRadius = 0;
            this.rJ_TextBox_人員資料_單位.BorderSize = 2;
            this.rJ_TextBox_人員資料_單位.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_人員資料_單位.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_人員資料_單位.GUID = "";
            this.rJ_TextBox_人員資料_單位.Location = new System.Drawing.Point(98, 148);
            this.rJ_TextBox_人員資料_單位.Multiline = false;
            this.rJ_TextBox_人員資料_單位.Name = "rJ_TextBox_人員資料_單位";
            this.rJ_TextBox_人員資料_單位.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_人員資料_單位.PassWordChar = false;
            this.rJ_TextBox_人員資料_單位.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_人員資料_單位.PlaceholderText = "";
            this.rJ_TextBox_人員資料_單位.ShowTouchPannel = false;
            this.rJ_TextBox_人員資料_單位.Size = new System.Drawing.Size(421, 30);
            this.rJ_TextBox_人員資料_單位.TabIndex = 112;
            this.rJ_TextBox_人員資料_單位.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_人員資料_單位.Texts = "";
            this.rJ_TextBox_人員資料_單位.UnderlineStyle = false;
            // 
            // rJ_TextBox_人員資料_密碼
            // 
            this.rJ_TextBox_人員資料_密碼.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_人員資料_密碼.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_TextBox_人員資料_密碼.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_人員資料_密碼.BorderRadius = 0;
            this.rJ_TextBox_人員資料_密碼.BorderSize = 2;
            this.rJ_TextBox_人員資料_密碼.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_人員資料_密碼.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_人員資料_密碼.GUID = "";
            this.rJ_TextBox_人員資料_密碼.Location = new System.Drawing.Point(98, 112);
            this.rJ_TextBox_人員資料_密碼.Multiline = false;
            this.rJ_TextBox_人員資料_密碼.Name = "rJ_TextBox_人員資料_密碼";
            this.rJ_TextBox_人員資料_密碼.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_人員資料_密碼.PassWordChar = false;
            this.rJ_TextBox_人員資料_密碼.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_人員資料_密碼.PlaceholderText = "";
            this.rJ_TextBox_人員資料_密碼.ShowTouchPannel = false;
            this.rJ_TextBox_人員資料_密碼.Size = new System.Drawing.Size(421, 30);
            this.rJ_TextBox_人員資料_密碼.TabIndex = 111;
            this.rJ_TextBox_人員資料_密碼.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_人員資料_密碼.Texts = "";
            this.rJ_TextBox_人員資料_密碼.UnderlineStyle = false;
            // 
            // rJ_TextBox_人員資料_姓名
            // 
            this.rJ_TextBox_人員資料_姓名.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_人員資料_姓名.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_TextBox_人員資料_姓名.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_人員資料_姓名.BorderRadius = 0;
            this.rJ_TextBox_人員資料_姓名.BorderSize = 2;
            this.rJ_TextBox_人員資料_姓名.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_人員資料_姓名.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_人員資料_姓名.GUID = "";
            this.rJ_TextBox_人員資料_姓名.Location = new System.Drawing.Point(98, 40);
            this.rJ_TextBox_人員資料_姓名.Multiline = false;
            this.rJ_TextBox_人員資料_姓名.Name = "rJ_TextBox_人員資料_姓名";
            this.rJ_TextBox_人員資料_姓名.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_人員資料_姓名.PassWordChar = false;
            this.rJ_TextBox_人員資料_姓名.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_人員資料_姓名.PlaceholderText = "";
            this.rJ_TextBox_人員資料_姓名.ShowTouchPannel = false;
            this.rJ_TextBox_人員資料_姓名.Size = new System.Drawing.Size(421, 30);
            this.rJ_TextBox_人員資料_姓名.TabIndex = 17;
            this.rJ_TextBox_人員資料_姓名.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_人員資料_姓名.Texts = "";
            this.rJ_TextBox_人員資料_姓名.UnderlineStyle = false;
            // 
            // rJ_TextBox_人員資料_ID
            // 
            this.rJ_TextBox_人員資料_ID.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_人員資料_ID.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_TextBox_人員資料_ID.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_人員資料_ID.BorderRadius = 0;
            this.rJ_TextBox_人員資料_ID.BorderSize = 2;
            this.rJ_TextBox_人員資料_ID.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_人員資料_ID.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_人員資料_ID.GUID = "";
            this.rJ_TextBox_人員資料_ID.Location = new System.Drawing.Point(98, 4);
            this.rJ_TextBox_人員資料_ID.Multiline = false;
            this.rJ_TextBox_人員資料_ID.Name = "rJ_TextBox_人員資料_ID";
            this.rJ_TextBox_人員資料_ID.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_人員資料_ID.PassWordChar = false;
            this.rJ_TextBox_人員資料_ID.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_人員資料_ID.PlaceholderText = "";
            this.rJ_TextBox_人員資料_ID.ShowTouchPannel = false;
            this.rJ_TextBox_人員資料_ID.Size = new System.Drawing.Size(421, 30);
            this.rJ_TextBox_人員資料_ID.TabIndex = 2;
            this.rJ_TextBox_人員資料_ID.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_人員資料_ID.Texts = "";
            this.rJ_TextBox_人員資料_ID.UnderlineStyle = false;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.LightCyan;
            this.label49.Font = new System.Drawing.Font("新細明體", 12F);
            this.label49.ForeColor = System.Drawing.Color.Black;
            this.label49.Location = new System.Drawing.Point(4, 1);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(87, 35);
            this.label49.TabIndex = 8;
            this.label49.Text = "ID";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label144
            // 
            this.label144.BackColor = System.Drawing.Color.LightCyan;
            this.label144.Font = new System.Drawing.Font("新細明體", 12F);
            this.label144.ForeColor = System.Drawing.Color.Black;
            this.label144.Location = new System.Drawing.Point(4, 37);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(86, 35);
            this.label144.TabIndex = 0;
            this.label144.Text = "姓名";
            this.label144.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label108
            // 
            this.label108.BackColor = System.Drawing.Color.LightCyan;
            this.label108.Font = new System.Drawing.Font("新細明體", 12F);
            this.label108.ForeColor = System.Drawing.Color.Black;
            this.label108.Location = new System.Drawing.Point(4, 181);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(86, 35);
            this.label108.TabIndex = 12;
            this.label108.Text = "權限等級";
            this.label108.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label138
            // 
            this.label138.BackColor = System.Drawing.Color.LightCyan;
            this.label138.Font = new System.Drawing.Font("新細明體", 12F);
            this.label138.ForeColor = System.Drawing.Color.Black;
            this.label138.Location = new System.Drawing.Point(4, 145);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(86, 35);
            this.label138.TabIndex = 6;
            this.label138.Text = "單位";
            this.label138.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label143
            // 
            this.label143.BackColor = System.Drawing.Color.LightCyan;
            this.label143.Font = new System.Drawing.Font("新細明體", 12F);
            this.label143.ForeColor = System.Drawing.Color.Black;
            this.label143.Location = new System.Drawing.Point(4, 73);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(86, 35);
            this.label143.TabIndex = 2;
            this.label143.Text = "性別";
            this.label143.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label102
            // 
            this.label102.BackColor = System.Drawing.Color.LightCyan;
            this.label102.Font = new System.Drawing.Font("新細明體", 12F);
            this.label102.ForeColor = System.Drawing.Color.Black;
            this.label102.Location = new System.Drawing.Point(4, 109);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(86, 35);
            this.label102.TabIndex = 15;
            this.label102.Text = "密碼";
            this.label102.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.LightCyan;
            this.label44.Font = new System.Drawing.Font("新細明體", 12F);
            this.label44.ForeColor = System.Drawing.Color.Black;
            this.label44.Location = new System.Drawing.Point(4, 217);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(86, 35);
            this.label44.TabIndex = 14;
            this.label44.Text = "顏色";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.LightCyan;
            this.label17.Font = new System.Drawing.Font("新細明體", 12F);
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(4, 253);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 35);
            this.label17.TabIndex = 13;
            this.label17.Text = "卡號";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel149
            // 
            this.panel149.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel149.Controls.Add(this.panel150);
            this.panel149.Controls.Add(this.textBox_人員資料_顏色);
            this.panel149.Location = new System.Drawing.Point(98, 220);
            this.panel149.Name = "panel149";
            this.panel149.Size = new System.Drawing.Size(216, 29);
            this.panel149.TabIndex = 16;
            // 
            // panel150
            // 
            this.panel150.Controls.Add(this.button_人員資料_顏色選擇);
            this.panel150.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel150.Location = new System.Drawing.Point(179, 0);
            this.panel150.Name = "panel150";
            this.panel150.Size = new System.Drawing.Size(37, 29);
            this.panel150.TabIndex = 0;
            // 
            // button_人員資料_顏色選擇
            // 
            this.button_人員資料_顏色選擇.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_人員資料_顏色選擇.Location = new System.Drawing.Point(0, 0);
            this.button_人員資料_顏色選擇.Name = "button_人員資料_顏色選擇";
            this.button_人員資料_顏色選擇.Size = new System.Drawing.Size(37, 29);
            this.button_人員資料_顏色選擇.TabIndex = 5;
            this.button_人員資料_顏色選擇.Text = "...";
            this.button_人員資料_顏色選擇.UseVisualStyleBackColor = true;
            // 
            // textBox_人員資料_顏色
            // 
            this.textBox_人員資料_顏色.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_人員資料_顏色.Location = new System.Drawing.Point(2, 2);
            this.textBox_人員資料_顏色.Name = "textBox_人員資料_顏色";
            this.textBox_人員資料_顏色.ReadOnly = true;
            this.textBox_人員資料_顏色.Size = new System.Drawing.Size(177, 29);
            this.textBox_人員資料_顏色.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.rJ_RatioButton_人員資料_男);
            this.flowLayoutPanel2.Controls.Add(this.rJ_RatioButton_人員資料_女);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(98, 76);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(421, 29);
            this.flowLayoutPanel2.TabIndex = 114;
            // 
            // rJ_RatioButton_人員資料_男
            // 
            this.rJ_RatioButton_人員資料_男.AutoSize = true;
            this.rJ_RatioButton_人員資料_男.BackColor = System.Drawing.Color.White;
            this.rJ_RatioButton_人員資料_男.CheckColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_RatioButton_人員資料_男.Checked = true;
            this.rJ_RatioButton_人員資料_男.ForeColor = System.Drawing.Color.Black;
            this.rJ_RatioButton_人員資料_男.Location = new System.Drawing.Point(3, 3);
            this.rJ_RatioButton_人員資料_男.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.rJ_RatioButton_人員資料_男.MinimumSize = new System.Drawing.Size(0, 21);
            this.rJ_RatioButton_人員資料_男.Name = "rJ_RatioButton_人員資料_男";
            this.rJ_RatioButton_人員資料_男.Size = new System.Drawing.Size(55, 24);
            this.rJ_RatioButton_人員資料_男.TabIndex = 115;
            this.rJ_RatioButton_人員資料_男.TabStop = true;
            this.rJ_RatioButton_人員資料_男.Text = "男";
            this.rJ_RatioButton_人員資料_男.UncheckColor = System.Drawing.Color.Gray;
            this.rJ_RatioButton_人員資料_男.UseVisualStyleBackColor = false;
            // 
            // rJ_RatioButton_人員資料_女
            // 
            this.rJ_RatioButton_人員資料_女.AutoSize = true;
            this.rJ_RatioButton_人員資料_女.BackColor = System.Drawing.Color.White;
            this.rJ_RatioButton_人員資料_女.CheckColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_RatioButton_人員資料_女.ForeColor = System.Drawing.Color.Black;
            this.rJ_RatioButton_人員資料_女.Location = new System.Drawing.Point(69, 3);
            this.rJ_RatioButton_人員資料_女.MinimumSize = new System.Drawing.Size(0, 21);
            this.rJ_RatioButton_人員資料_女.Name = "rJ_RatioButton_人員資料_女";
            this.rJ_RatioButton_人員資料_女.Size = new System.Drawing.Size(55, 24);
            this.rJ_RatioButton_人員資料_女.TabIndex = 116;
            this.rJ_RatioButton_人員資料_女.Text = "女";
            this.rJ_RatioButton_人員資料_女.UncheckColor = System.Drawing.Color.Gray;
            this.rJ_RatioButton_人員資料_女.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_人員資料_識別圖案
            // 
            this.rJ_TextBox_人員資料_識別圖案.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_人員資料_識別圖案.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.rJ_TextBox_人員資料_識別圖案.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_人員資料_識別圖案.BorderRadius = 0;
            this.rJ_TextBox_人員資料_識別圖案.BorderSize = 2;
            this.rJ_TextBox_人員資料_識別圖案.Font = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_人員資料_識別圖案.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_人員資料_識別圖案.GUID = "";
            this.rJ_TextBox_人員資料_識別圖案.Location = new System.Drawing.Point(49, 334);
            this.rJ_TextBox_人員資料_識別圖案.Multiline = false;
            this.rJ_TextBox_人員資料_識別圖案.Name = "rJ_TextBox_人員資料_識別圖案";
            this.rJ_TextBox_人員資料_識別圖案.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_人員資料_識別圖案.PassWordChar = false;
            this.rJ_TextBox_人員資料_識別圖案.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_人員資料_識別圖案.PlaceholderText = "";
            this.rJ_TextBox_人員資料_識別圖案.ShowTouchPannel = false;
            this.rJ_TextBox_人員資料_識別圖案.Size = new System.Drawing.Size(421, 30);
            this.rJ_TextBox_人員資料_識別圖案.TabIndex = 135;
            this.rJ_TextBox_人員資料_識別圖案.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_人員資料_識別圖案.Texts = "";
            this.rJ_TextBox_人員資料_識別圖案.UnderlineStyle = false;
            this.rJ_TextBox_人員資料_識別圖案.Visible = false;
            // 
            // sqL_DataGridView_人員資料
            // 
            this.sqL_DataGridView_人員資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_人員資料.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_人員資料.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_人員資料.BorderRadius = 10;
            this.sqL_DataGridView_人員資料.BorderSize = 2;
            this.sqL_DataGridView_人員資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_人員資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_人員資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_人員資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_人員資料.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_人員資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_人員資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_人員資料.columnHeadersHeight = 23;
            this.sqL_DataGridView_人員資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns1"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns2"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns3"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns4"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns5"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns6"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns7"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns8"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns9"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns10"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns11"))));
            this.sqL_DataGridView_人員資料.DataBaseName = "Dispensing_000";
            this.sqL_DataGridView_人員資料.Dock = System.Windows.Forms.DockStyle.Top;
            this.sqL_DataGridView_人員資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_人員資料.ImageBox = false;
            this.sqL_DataGridView_人員資料.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_人員資料.Name = "sqL_DataGridView_人員資料";
            this.sqL_DataGridView_人員資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_人員資料.Password = "user82822040";
            this.sqL_DataGridView_人員資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_人員資料.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_人員資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_人員資料.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_人員資料.RowsHeight = 50;
            this.sqL_DataGridView_人員資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_人員資料.Server = "localhost";
            this.sqL_DataGridView_人員資料.Size = new System.Drawing.Size(1639, 516);
            this.sqL_DataGridView_人員資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_人員資料.TabIndex = 113;
            this.sqL_DataGridView_人員資料.TableName = "person_page";
            this.sqL_DataGridView_人員資料.UserName = "root";
            this.sqL_DataGridView_人員資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_人員資料.可選擇多列 = true;
            this.sqL_DataGridView_人員資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_人員資料.自動換行 = true;
            this.sqL_DataGridView_人員資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_人員資料.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_人員資料.顯示CheckBox = false;
            this.sqL_DataGridView_人員資料.顯示首列 = false;
            this.sqL_DataGridView_人員資料.顯示首行 = true;
            this.sqL_DataGridView_人員資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_人員資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // 系統頁面
            // 
            this.系統頁面.AutoScroll = true;
            this.系統頁面.BackColor = System.Drawing.SystemColors.Window;
            this.系統頁面.Controls.Add(this.plC_ScreenPage_系統頁面);
            this.系統頁面.Controls.Add(this.panel_系統頁面);
            this.系統頁面.Location = new System.Drawing.Point(4, 25);
            this.系統頁面.Name = "系統頁面";
            this.系統頁面.Size = new System.Drawing.Size(1639, 1006);
            this.系統頁面.TabIndex = 2;
            this.系統頁面.Text = "系統頁面";
            // 
            // plC_ScreenPage_系統頁面
            // 
            this.plC_ScreenPage_系統頁面.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage_系統頁面.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_系統頁面.Controls.Add(this.tabPage1);
            this.plC_ScreenPage_系統頁面.Controls.Add(this.tabPage4);
            this.plC_ScreenPage_系統頁面.Controls.Add(this.tabPage6);
            this.plC_ScreenPage_系統頁面.Controls.Add(this.tabPage2);
            this.plC_ScreenPage_系統頁面.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage_系統頁面.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage_系統頁面.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage_系統頁面.Location = new System.Drawing.Point(0, 52);
            this.plC_ScreenPage_系統頁面.Name = "plC_ScreenPage_系統頁面";
            this.plC_ScreenPage_系統頁面.SelectedIndex = 0;
            this.plC_ScreenPage_系統頁面.Size = new System.Drawing.Size(1639, 954);
            this.plC_ScreenPage_系統頁面.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_系統頁面.TabIndex = 119;
            this.plC_ScreenPage_系統頁面.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_系統頁面.顯示頁面 = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox18);
            this.tabPage1.Controls.Add(this.lowerMachine_Pane);
            this.tabPage1.Controls.Add(this.plC_UI_Init);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1631, 925);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PLC";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.plC_NumBox2);
            this.groupBox3.Location = new System.Drawing.Point(881, 333);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(136, 60);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "蜂鳴持續時間";
            // 
            // plC_NumBox2
            // 
            this.plC_NumBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_NumBox2.Location = new System.Drawing.Point(6, 21);
            this.plC_NumBox2.mBackColor = System.Drawing.SystemColors.Window;
            this.plC_NumBox2.mForeColor = System.Drawing.SystemColors.WindowText;
            this.plC_NumBox2.Name = "plC_NumBox2";
            this.plC_NumBox2.ReadOnly = false;
            this.plC_NumBox2.Size = new System.Drawing.Size(112, 33);
            this.plC_NumBox2.TabIndex = 0;
            this.plC_NumBox2.Value = 0;
            this.plC_NumBox2.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox2.密碼欄位 = false;
            this.plC_NumBox2.寫入元件位置 = "D3002";
            this.plC_NumBox2.小數點位置 = 3;
            this.plC_NumBox2.微調數值 = 0;
            this.plC_NumBox2.讀取元件位置 = "D3002";
            this.plC_NumBox2.音效 = true;
            this.plC_NumBox2.顯示微調按鈕 = false;
            this.plC_NumBox2.顯示螢幕小鍵盤 = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.plC_NumBox1);
            this.groupBox2.Location = new System.Drawing.Point(881, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 60);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "單層格數";
            // 
            // plC_NumBox1
            // 
            this.plC_NumBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_NumBox1.Location = new System.Drawing.Point(6, 21);
            this.plC_NumBox1.mBackColor = System.Drawing.SystemColors.Window;
            this.plC_NumBox1.mForeColor = System.Drawing.SystemColors.WindowText;
            this.plC_NumBox1.Name = "plC_NumBox1";
            this.plC_NumBox1.ReadOnly = false;
            this.plC_NumBox1.Size = new System.Drawing.Size(112, 33);
            this.plC_NumBox1.TabIndex = 0;
            this.plC_NumBox1.Value = 0;
            this.plC_NumBox1.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox1.密碼欄位 = false;
            this.plC_NumBox1.寫入元件位置 = "D3001";
            this.plC_NumBox1.小數點位置 = 0;
            this.plC_NumBox1.微調數值 = 0;
            this.plC_NumBox1.讀取元件位置 = "D3001";
            this.plC_NumBox1.音效 = true;
            this.plC_NumBox1.顯示微調按鈕 = false;
            this.plC_NumBox1.顯示螢幕小鍵盤 = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plC_NumBox_開門異常時間);
            this.groupBox1.Location = new System.Drawing.Point(881, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 60);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "開門異常時間設定";
            // 
            // plC_NumBox_開門異常時間
            // 
            this.plC_NumBox_開門異常時間.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_NumBox_開門異常時間.Location = new System.Drawing.Point(6, 21);
            this.plC_NumBox_開門異常時間.mBackColor = System.Drawing.SystemColors.Window;
            this.plC_NumBox_開門異常時間.mForeColor = System.Drawing.SystemColors.WindowText;
            this.plC_NumBox_開門異常時間.Name = "plC_NumBox_開門異常時間";
            this.plC_NumBox_開門異常時間.ReadOnly = false;
            this.plC_NumBox_開門異常時間.Size = new System.Drawing.Size(112, 33);
            this.plC_NumBox_開門異常時間.TabIndex = 0;
            this.plC_NumBox_開門異常時間.Value = 0;
            this.plC_NumBox_開門異常時間.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_開門異常時間.密碼欄位 = false;
            this.plC_NumBox_開門異常時間.寫入元件位置 = "D3000";
            this.plC_NumBox_開門異常時間.小數點位置 = 3;
            this.plC_NumBox_開門異常時間.微調數值 = 0;
            this.plC_NumBox_開門異常時間.讀取元件位置 = "D3000";
            this.plC_NumBox_開門異常時間.音效 = true;
            this.plC_NumBox_開門異常時間.顯示微調按鈕 = false;
            this.plC_NumBox_開門異常時間.顯示螢幕小鍵盤 = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.rfiD_FX600_UI);
            this.groupBox18.Location = new System.Drawing.Point(881, -5);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(398, 200);
            this.groupBox18.TabIndex = 30;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "RFID";
            // 
            // rfiD_FX600_UI
            // 
            this.rfiD_FX600_UI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rfiD_FX600_UI.Location = new System.Drawing.Point(6, 21);
            this.rfiD_FX600_UI.Name = "rfiD_FX600_UI";
            this.rfiD_FX600_UI.Size = new System.Drawing.Size(373, 158);
            this.rfiD_FX600_UI.TabIndex = 25;
            this.rfiD_FX600_UI.從站數量 = 3;
            this.rfiD_FX600_UI.掃描速度 = 1;
            this.rfiD_FX600_UI.是否自動通訊 = true;
            // 
            // lowerMachine_Pane
            // 
            this.lowerMachine_Pane.Location = new System.Drawing.Point(6, 6);
            this.lowerMachine_Pane.Name = "lowerMachine_Pane";
            this.lowerMachine_Pane.Size = new System.Drawing.Size(869, 565);
            this.lowerMachine_Pane.TabIndex = 25;
            this.lowerMachine_Pane.掃描速度 = 1;
            // 
            // plC_UI_Init
            // 
            this.plC_UI_Init.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plC_UI_Init.Location = new System.Drawing.Point(6, 577);
            this.plC_UI_Init.Name = "plC_UI_Init";
            this.plC_UI_Init.Size = new System.Drawing.Size(72, 25);
            this.plC_UI_Init.TabIndex = 26;
            this.plC_UI_Init.光道視覺元件初始化 = false;
            this.plC_UI_Init.全螢幕顯示 = false;
            this.plC_UI_Init.掃描速度 = 1;
            this.plC_UI_Init.起始畫面標題內容 = "鴻森整合機電有限公司";
            this.plC_UI_Init.起始畫面標題字體 = new System.Drawing.Font("標楷體", 20F, System.Drawing.FontStyle.Bold);
            this.plC_UI_Init.起始畫面背景 = ((System.Drawing.Image)(resources.GetObject("plC_UI_Init.起始畫面背景")));
            this.plC_UI_Init.起始畫面顯示 = false;
            this.plC_UI_Init.邁得威視元件初始化 = false;
            this.plC_UI_Init.開機延遲 = 0;
            this.plC_UI_Init.音效 = false;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.rfiD_UI);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1631, 925);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "RFID";
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
            this.rfiD_UI.Size = new System.Drawing.Size(1631, 925);
            this.rfiD_UI.SSID = "";
            this.rfiD_UI.Station = "0";
            this.rfiD_UI.Subnet = "0.0.0.0";
            this.rfiD_UI.TabIndex = 1;
            this.rfiD_UI.TableName = "RFID_Device_Jsonstring";
            this.rfiD_UI.UDP_LocalPorts = ((System.Collections.Generic.List<string>)(resources.GetObject("rfiD_UI.UDP_LocalPorts")));
            this.rfiD_UI.UDP_ServerPorts = ((System.Collections.Generic.List<string>)(resources.GetObject("rfiD_UI.UDP_ServerPorts")));
            this.rfiD_UI.UserName = "root";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.White;
            this.tabPage6.Controls.Add(this.plC_RJ_Button_Box_Index_Table_刪除);
            this.tabPage6.Controls.Add(this.plC_RJ_Button_Box_Index_Table_更新);
            this.tabPage6.Controls.Add(this.plC_RJ_Button_Box_Index_Table_匯入);
            this.tabPage6.Controls.Add(this.plC_RJ_Button_Box_Index_Table_匯出);
            this.tabPage6.Controls.Add(this.plC_RJ_GroupBox9);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1631, 925);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "BoxIndex";
            // 
            // plC_RJ_Button_Box_Index_Table_刪除
            // 
            this.plC_RJ_Button_Box_Index_Table_刪除.AutoResetState = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_Box_Index_Table_刪除.Bool = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_Box_Index_Table_刪除.BorderRadius = 5;
            this.plC_RJ_Button_Box_Index_Table_刪除.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_刪除.but_press = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_Box_Index_Table_刪除.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_刪除.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_Box_Index_Table_刪除.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_刪除.GUID = "";
            this.plC_RJ_Button_Box_Index_Table_刪除.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_Box_Index_Table_刪除.Location = new System.Drawing.Point(438, 547);
            this.plC_RJ_Button_Box_Index_Table_刪除.Name = "plC_RJ_Button_Box_Index_Table_刪除";
            this.plC_RJ_Button_Box_Index_Table_刪除.OFF_文字內容 = "刪除";
            this.plC_RJ_Button_Box_Index_Table_刪除.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_刪除.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_刪除.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_刪除.ON_BorderSize = 5;
            this.plC_RJ_Button_Box_Index_Table_刪除.ON_文字內容 = "刪除";
            this.plC_RJ_Button_Box_Index_Table_刪除.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_刪除.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_Box_Index_Table_刪除.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_刪除.Size = new System.Drawing.Size(139, 65);
            this.plC_RJ_Button_Box_Index_Table_刪除.State = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.TabIndex = 135;
            this.plC_RJ_Button_Box_Index_Table_刪除.Text = "刪除";
            this.plC_RJ_Button_Box_Index_Table_刪除.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_刪除.Texts = "刪除";
            this.plC_RJ_Button_Box_Index_Table_刪除.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.字型鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_Box_Index_Table_刪除.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_Box_Index_Table_刪除.文字鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.讀取位元反向 = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.讀寫鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.音效 = true;
            this.plC_RJ_Button_Box_Index_Table_刪除.顯示 = false;
            this.plC_RJ_Button_Box_Index_Table_刪除.顯示狀態 = false;
            // 
            // plC_RJ_Button_Box_Index_Table_更新
            // 
            this.plC_RJ_Button_Box_Index_Table_更新.AutoResetState = false;
            this.plC_RJ_Button_Box_Index_Table_更新.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_Box_Index_Table_更新.Bool = false;
            this.plC_RJ_Button_Box_Index_Table_更新.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_Box_Index_Table_更新.BorderRadius = 5;
            this.plC_RJ_Button_Box_Index_Table_更新.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_更新.but_press = false;
            this.plC_RJ_Button_Box_Index_Table_更新.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_Box_Index_Table_更新.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_更新.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_Box_Index_Table_更新.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_更新.GUID = "";
            this.plC_RJ_Button_Box_Index_Table_更新.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_Box_Index_Table_更新.Location = new System.Drawing.Point(293, 547);
            this.plC_RJ_Button_Box_Index_Table_更新.Name = "plC_RJ_Button_Box_Index_Table_更新";
            this.plC_RJ_Button_Box_Index_Table_更新.OFF_文字內容 = "更新";
            this.plC_RJ_Button_Box_Index_Table_更新.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_更新.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_更新.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_更新.ON_BorderSize = 5;
            this.plC_RJ_Button_Box_Index_Table_更新.ON_文字內容 = "更新";
            this.plC_RJ_Button_Box_Index_Table_更新.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_更新.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_Box_Index_Table_更新.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_更新.Size = new System.Drawing.Size(139, 65);
            this.plC_RJ_Button_Box_Index_Table_更新.State = false;
            this.plC_RJ_Button_Box_Index_Table_更新.TabIndex = 134;
            this.plC_RJ_Button_Box_Index_Table_更新.Text = "更新";
            this.plC_RJ_Button_Box_Index_Table_更新.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_更新.Texts = "更新";
            this.plC_RJ_Button_Box_Index_Table_更新.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_Box_Index_Table_更新.字型鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_更新.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_Box_Index_Table_更新.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_Box_Index_Table_更新.文字鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_更新.讀取位元反向 = false;
            this.plC_RJ_Button_Box_Index_Table_更新.讀寫鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_更新.音效 = true;
            this.plC_RJ_Button_Box_Index_Table_更新.顯示 = false;
            this.plC_RJ_Button_Box_Index_Table_更新.顯示狀態 = false;
            // 
            // plC_RJ_Button_Box_Index_Table_匯入
            // 
            this.plC_RJ_Button_Box_Index_Table_匯入.AutoResetState = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_Box_Index_Table_匯入.Bool = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_Box_Index_Table_匯入.BorderRadius = 5;
            this.plC_RJ_Button_Box_Index_Table_匯入.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_匯入.but_press = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_Box_Index_Table_匯入.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_匯入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_Box_Index_Table_匯入.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_匯入.GUID = "";
            this.plC_RJ_Button_Box_Index_Table_匯入.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_Box_Index_Table_匯入.Location = new System.Drawing.Point(148, 547);
            this.plC_RJ_Button_Box_Index_Table_匯入.Name = "plC_RJ_Button_Box_Index_Table_匯入";
            this.plC_RJ_Button_Box_Index_Table_匯入.OFF_文字內容 = "匯入";
            this.plC_RJ_Button_Box_Index_Table_匯入.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_匯入.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_匯入.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_匯入.ON_BorderSize = 5;
            this.plC_RJ_Button_Box_Index_Table_匯入.ON_文字內容 = "匯入";
            this.plC_RJ_Button_Box_Index_Table_匯入.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_匯入.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_Box_Index_Table_匯入.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_匯入.Size = new System.Drawing.Size(139, 65);
            this.plC_RJ_Button_Box_Index_Table_匯入.State = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.TabIndex = 133;
            this.plC_RJ_Button_Box_Index_Table_匯入.Text = "匯入";
            this.plC_RJ_Button_Box_Index_Table_匯入.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_匯入.Texts = "匯入";
            this.plC_RJ_Button_Box_Index_Table_匯入.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.字型鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_Box_Index_Table_匯入.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_Box_Index_Table_匯入.文字鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.讀取位元反向 = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.讀寫鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.音效 = true;
            this.plC_RJ_Button_Box_Index_Table_匯入.顯示 = false;
            this.plC_RJ_Button_Box_Index_Table_匯入.顯示狀態 = false;
            // 
            // plC_RJ_Button_Box_Index_Table_匯出
            // 
            this.plC_RJ_Button_Box_Index_Table_匯出.AutoResetState = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.plC_RJ_Button_Box_Index_Table_匯出.Bool = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_Box_Index_Table_匯出.BorderRadius = 5;
            this.plC_RJ_Button_Box_Index_Table_匯出.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_匯出.but_press = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_Box_Index_Table_匯出.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_Box_Index_Table_匯出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_Box_Index_Table_匯出.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_匯出.GUID = "";
            this.plC_RJ_Button_Box_Index_Table_匯出.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_Box_Index_Table_匯出.Location = new System.Drawing.Point(3, 547);
            this.plC_RJ_Button_Box_Index_Table_匯出.Name = "plC_RJ_Button_Box_Index_Table_匯出";
            this.plC_RJ_Button_Box_Index_Table_匯出.OFF_文字內容 = "匯出";
            this.plC_RJ_Button_Box_Index_Table_匯出.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_匯出.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_匯出.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_匯出.ON_BorderSize = 5;
            this.plC_RJ_Button_Box_Index_Table_匯出.ON_文字內容 = "匯出";
            this.plC_RJ_Button_Box_Index_Table_匯出.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_Box_Index_Table_匯出.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_Box_Index_Table_匯出.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_Box_Index_Table_匯出.Size = new System.Drawing.Size(139, 65);
            this.plC_RJ_Button_Box_Index_Table_匯出.State = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.TabIndex = 132;
            this.plC_RJ_Button_Box_Index_Table_匯出.Text = "匯出";
            this.plC_RJ_Button_Box_Index_Table_匯出.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_Box_Index_Table_匯出.Texts = "匯出";
            this.plC_RJ_Button_Box_Index_Table_匯出.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.字型鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_Box_Index_Table_匯出.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_Box_Index_Table_匯出.文字鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.讀取位元反向 = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.讀寫鎖住 = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.音效 = true;
            this.plC_RJ_Button_Box_Index_Table_匯出.顯示 = false;
            this.plC_RJ_Button_Box_Index_Table_匯出.顯示狀態 = false;
            // 
            // plC_RJ_GroupBox9
            // 
            // 
            // plC_RJ_GroupBox9.ContentsPanel
            // 
            this.plC_RJ_GroupBox9.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox9.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_GroupBox9.ContentsPanel.BorderRadius = 5;
            this.plC_RJ_GroupBox9.ContentsPanel.BorderSize = 2;
            this.plC_RJ_GroupBox9.ContentsPanel.Controls.Add(this.sqL_DataGridView_Box_Index_Table);
            this.plC_RJ_GroupBox9.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_RJ_GroupBox9.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox9.ContentsPanel.IsSelected = false;
            this.plC_RJ_GroupBox9.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.plC_RJ_GroupBox9.ContentsPanel.Name = "ContentsPanel";
            this.plC_RJ_GroupBox9.ContentsPanel.Size = new System.Drawing.Size(1631, 503);
            this.plC_RJ_GroupBox9.ContentsPanel.TabIndex = 2;
            this.plC_RJ_GroupBox9.Dock = System.Windows.Forms.DockStyle.Top;
            this.plC_RJ_GroupBox9.GUID = "";
            this.plC_RJ_GroupBox9.Location = new System.Drawing.Point(0, 0);
            this.plC_RJ_GroupBox9.Name = "plC_RJ_GroupBox9";
            this.plC_RJ_GroupBox9.PannelBackColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox9.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_GroupBox9.PannelBorderRadius = 5;
            this.plC_RJ_GroupBox9.PannelBorderSize = 2;
            this.plC_RJ_GroupBox9.Size = new System.Drawing.Size(1631, 540);
            this.plC_RJ_GroupBox9.TabIndex = 48;
            this.plC_RJ_GroupBox9.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.plC_RJ_GroupBox9.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_GroupBox9.TitleBorderRadius = 5;
            this.plC_RJ_GroupBox9.TitleBorderSize = 0;
            this.plC_RJ_GroupBox9.TitleFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_GroupBox9.TitleForeColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox9.TitleHeight = 37;
            this.plC_RJ_GroupBox9.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.plC_RJ_GroupBox9.TitleTexts = "Box_Index_Table";
            // 
            // sqL_DataGridView_Box_Index_Table
            // 
            this.sqL_DataGridView_Box_Index_Table.AutoSelectToDeep = true;
            this.sqL_DataGridView_Box_Index_Table.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_Box_Index_Table.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_Box_Index_Table.BorderRadius = 10;
            this.sqL_DataGridView_Box_Index_Table.BorderSize = 0;
            this.sqL_DataGridView_Box_Index_Table.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_Box_Index_Table.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_Box_Index_Table.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_Box_Index_Table.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_Box_Index_Table.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_Box_Index_Table.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_Box_Index_Table.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_Box_Index_Table.columnHeadersHeight = 26;
            this.sqL_DataGridView_Box_Index_Table.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns"))));
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns1"))));
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns2"))));
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns3"))));
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns4"))));
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns5"))));
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns6"))));
            this.sqL_DataGridView_Box_Index_Table.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_Box_Index_Table.Columns7"))));
            this.sqL_DataGridView_Box_Index_Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_Box_Index_Table.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_Box_Index_Table.ImageBox = false;
            this.sqL_DataGridView_Box_Index_Table.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_Box_Index_Table.Margin = new System.Windows.Forms.Padding(4);
            this.sqL_DataGridView_Box_Index_Table.Name = "sqL_DataGridView_Box_Index_Table";
            this.sqL_DataGridView_Box_Index_Table.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_Box_Index_Table.Password = "user82822040";
            this.sqL_DataGridView_Box_Index_Table.Port = ((uint)(3306u));
            this.sqL_DataGridView_Box_Index_Table.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_Box_Index_Table.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_Box_Index_Table.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_Box_Index_Table.RowsHeight = 30;
            this.sqL_DataGridView_Box_Index_Table.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_Box_Index_Table.Server = "127.0.0.0";
            this.sqL_DataGridView_Box_Index_Table.Size = new System.Drawing.Size(1631, 503);
            this.sqL_DataGridView_Box_Index_Table.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_Box_Index_Table.TabIndex = 27;
            this.sqL_DataGridView_Box_Index_Table.TableName = "box_index_table";
            this.sqL_DataGridView_Box_Index_Table.UserName = "root";
            this.sqL_DataGridView_Box_Index_Table.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_Box_Index_Table.可選擇多列 = true;
            this.sqL_DataGridView_Box_Index_Table.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_Box_Index_Table.自動換行 = true;
            this.sqL_DataGridView_Box_Index_Table.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_Box_Index_Table.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_Box_Index_Table.顯示CheckBox = false;
            this.sqL_DataGridView_Box_Index_Table.顯示首列 = true;
            this.sqL_DataGridView_Box_Index_Table.顯示首行 = true;
            this.sqL_DataGridView_Box_Index_Table.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_Box_Index_Table.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.loginUI);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1631, 925);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "權限設定";
            // 
            // loginUI
            // 
            this.loginUI.Level_num = ((uint)(20u));
            this.loginUI.Location = new System.Drawing.Point(3, 13);
            this.loginUI.Login_data_index_mySqlSslMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.loginUI.Login_data_index_Password = "66437068";
            this.loginUI.Login_data_index_Port = ((uint)(3306u));
            this.loginUI.Login_data_index_Server = "localhost";
            this.loginUI.Login_data_index_UserName = "user";
            this.loginUI.Login_data_mySqlSslMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.loginUI.Login_data_Password = "66437068";
            this.loginUI.Login_data_Port = ((uint)(3306u));
            this.loginUI.Login_data_Server = "localhost";
            this.loginUI.Login_data_UserName = "user";
            this.loginUI.Name = "loginUI";
            this.loginUI.Size = new System.Drawing.Size(861, 641);
            this.loginUI.TabIndex = 2;
            // 
            // panel_系統頁面
            // 
            this.panel_系統頁面.Controls.Add(this.plC_RJ_ScreenButton3);
            this.panel_系統頁面.Controls.Add(this.plC_RJ_ScreenButton6);
            this.panel_系統頁面.Controls.Add(this.plC_RJ_ScreenButton2);
            this.panel_系統頁面.Controls.Add(this.plC_RJ_ScreenButton5);
            this.panel_系統頁面.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_系統頁面.Location = new System.Drawing.Point(0, 0);
            this.panel_系統頁面.Name = "panel_系統頁面";
            this.panel_系統頁面.Padding = new System.Windows.Forms.Padding(2);
            this.panel_系統頁面.Size = new System.Drawing.Size(1639, 52);
            this.panel_系統頁面.TabIndex = 118;
            // 
            // plC_RJ_ScreenButton3
            // 
            this.plC_RJ_ScreenButton3.but_press = false;
            this.plC_RJ_ScreenButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton3.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton3.IconSize = 40;
            this.plC_RJ_ScreenButton3.Location = new System.Drawing.Point(500, 2);
            this.plC_RJ_ScreenButton3.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton3.Name = "plC_RJ_ScreenButton3";
            this.plC_RJ_ScreenButton3.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton3.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton3.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton3.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton3.OffText = "權限設定";
            this.plC_RJ_ScreenButton3.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton3.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton3.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton3.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton3.OnText = "權限設定";
            this.plC_RJ_ScreenButton3.ShowIcon = false;
            this.plC_RJ_ScreenButton3.Size = new System.Drawing.Size(166, 48);
            this.plC_RJ_ScreenButton3.TabIndex = 78;
            this.plC_RJ_ScreenButton3.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton3.寫入位置註解 = "";
            this.plC_RJ_ScreenButton3.寫入元件位置 = "";
            this.plC_RJ_ScreenButton3.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton3.控制位址 = "D0";
            this.plC_RJ_ScreenButton3.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton3.致能讀取位置 = "";
            this.plC_RJ_ScreenButton3.讀取位元反向 = false;
            this.plC_RJ_ScreenButton3.讀取位置註解 = "";
            this.plC_RJ_ScreenButton3.讀取元件位置 = "";
            this.plC_RJ_ScreenButton3.音效 = true;
            this.plC_RJ_ScreenButton3.頁面名稱 = "權限設定";
            this.plC_RJ_ScreenButton3.頁面編號 = 0;
            this.plC_RJ_ScreenButton3.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton3.顯示狀態 = false;
            this.plC_RJ_ScreenButton3.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton6
            // 
            this.plC_RJ_ScreenButton6.but_press = false;
            this.plC_RJ_ScreenButton6.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton6.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton6.IconSize = 40;
            this.plC_RJ_ScreenButton6.Location = new System.Drawing.Point(334, 2);
            this.plC_RJ_ScreenButton6.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton6.Name = "plC_RJ_ScreenButton6";
            this.plC_RJ_ScreenButton6.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton6.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton6.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton6.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton6.OffText = "BoxIndex";
            this.plC_RJ_ScreenButton6.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton6.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton6.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton6.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton6.OnText = "BoxIndex";
            this.plC_RJ_ScreenButton6.ShowIcon = false;
            this.plC_RJ_ScreenButton6.Size = new System.Drawing.Size(166, 48);
            this.plC_RJ_ScreenButton6.TabIndex = 77;
            this.plC_RJ_ScreenButton6.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton6.寫入位置註解 = "";
            this.plC_RJ_ScreenButton6.寫入元件位置 = "";
            this.plC_RJ_ScreenButton6.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton6.控制位址 = "D0";
            this.plC_RJ_ScreenButton6.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton6.致能讀取位置 = "";
            this.plC_RJ_ScreenButton6.讀取位元反向 = false;
            this.plC_RJ_ScreenButton6.讀取位置註解 = "";
            this.plC_RJ_ScreenButton6.讀取元件位置 = "";
            this.plC_RJ_ScreenButton6.音效 = true;
            this.plC_RJ_ScreenButton6.頁面名稱 = "BoxIndex";
            this.plC_RJ_ScreenButton6.頁面編號 = 0;
            this.plC_RJ_ScreenButton6.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton6.顯示狀態 = false;
            this.plC_RJ_ScreenButton6.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton2
            // 
            this.plC_RJ_ScreenButton2.but_press = false;
            this.plC_RJ_ScreenButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton2.IconSize = 40;
            this.plC_RJ_ScreenButton2.Location = new System.Drawing.Point(168, 2);
            this.plC_RJ_ScreenButton2.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton2.Name = "plC_RJ_ScreenButton2";
            this.plC_RJ_ScreenButton2.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton2.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton2.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton2.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton2.OffText = "RFID";
            this.plC_RJ_ScreenButton2.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton2.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton2.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton2.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton2.OnText = "RFID";
            this.plC_RJ_ScreenButton2.ShowIcon = false;
            this.plC_RJ_ScreenButton2.Size = new System.Drawing.Size(166, 48);
            this.plC_RJ_ScreenButton2.TabIndex = 76;
            this.plC_RJ_ScreenButton2.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton2.寫入位置註解 = "";
            this.plC_RJ_ScreenButton2.寫入元件位置 = "";
            this.plC_RJ_ScreenButton2.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton2.控制位址 = "D0";
            this.plC_RJ_ScreenButton2.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton2.致能讀取位置 = "";
            this.plC_RJ_ScreenButton2.讀取位元反向 = false;
            this.plC_RJ_ScreenButton2.讀取位置註解 = "";
            this.plC_RJ_ScreenButton2.讀取元件位置 = "";
            this.plC_RJ_ScreenButton2.音效 = true;
            this.plC_RJ_ScreenButton2.頁面名稱 = "RFID";
            this.plC_RJ_ScreenButton2.頁面編號 = 0;
            this.plC_RJ_ScreenButton2.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton2.顯示狀態 = false;
            this.plC_RJ_ScreenButton2.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton5
            // 
            this.plC_RJ_ScreenButton5.but_press = false;
            this.plC_RJ_ScreenButton5.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton5.IconChar = FontAwesome.Sharp.IconChar.None;
            this.plC_RJ_ScreenButton5.IconSize = 40;
            this.plC_RJ_ScreenButton5.Location = new System.Drawing.Point(2, 2);
            this.plC_RJ_ScreenButton5.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton5.Name = "plC_RJ_ScreenButton5";
            this.plC_RJ_ScreenButton5.OffBackColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton5.OffFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_ScreenButton5.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton5.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton5.OffText = "PLC";
            this.plC_RJ_ScreenButton5.OnBackColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton5.OnFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_ScreenButton5.OnForeColor = System.Drawing.Color.DarkCyan;
            this.plC_RJ_ScreenButton5.OnIconColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_ScreenButton5.OnText = "PLC";
            this.plC_RJ_ScreenButton5.ShowIcon = false;
            this.plC_RJ_ScreenButton5.Size = new System.Drawing.Size(166, 48);
            this.plC_RJ_ScreenButton5.TabIndex = 75;
            this.plC_RJ_ScreenButton5.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton5.寫入位置註解 = "";
            this.plC_RJ_ScreenButton5.寫入元件位置 = "";
            this.plC_RJ_ScreenButton5.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton5.控制位址 = "D0";
            this.plC_RJ_ScreenButton5.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton5.致能讀取位置 = "";
            this.plC_RJ_ScreenButton5.讀取位元反向 = false;
            this.plC_RJ_ScreenButton5.讀取位置註解 = "";
            this.plC_RJ_ScreenButton5.讀取元件位置 = "";
            this.plC_RJ_ScreenButton5.音效 = true;
            this.plC_RJ_ScreenButton5.頁面名稱 = "PLC";
            this.plC_RJ_ScreenButton5.頁面編號 = 0;
            this.plC_RJ_ScreenButton5.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton5.顯示狀態 = false;
            this.plC_RJ_ScreenButton5.顯示讀取位置 = "";
            // 
            // 事件紀錄
            // 
            this.事件紀錄.BackColor = System.Drawing.SystemColors.Window;
            this.事件紀錄.Controls.Add(this.plC_RJ_Button_事件紀錄_搜尋);
            this.事件紀錄.Controls.Add(this.rJ_GroupBox5);
            this.事件紀錄.Controls.Add(this.rJ_GroupBox7);
            this.事件紀錄.Controls.Add(this.rJ_GroupBox6);
            this.事件紀錄.Controls.Add(this.sqL_DataGridView_事件紀錄);
            this.事件紀錄.Location = new System.Drawing.Point(4, 25);
            this.事件紀錄.Name = "事件紀錄";
            this.事件紀錄.Size = new System.Drawing.Size(1639, 1006);
            this.事件紀錄.TabIndex = 7;
            this.事件紀錄.Text = "事件紀錄";
            // 
            // plC_RJ_Button_事件紀錄_搜尋
            // 
            this.plC_RJ_Button_事件紀錄_搜尋.AutoResetState = false;
            this.plC_RJ_Button_事件紀錄_搜尋.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_Button_事件紀錄_搜尋.Bool = false;
            this.plC_RJ_Button_事件紀錄_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_事件紀錄_搜尋.BorderRadius = 5;
            this.plC_RJ_Button_事件紀錄_搜尋.BorderSize = 0;
            this.plC_RJ_Button_事件紀錄_搜尋.but_press = false;
            this.plC_RJ_Button_事件紀錄_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_事件紀錄_搜尋.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_事件紀錄_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_事件紀錄_搜尋.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_事件紀錄_搜尋.GUID = "";
            this.plC_RJ_Button_事件紀錄_搜尋.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_事件紀錄_搜尋.Location = new System.Drawing.Point(1469, 476);
            this.plC_RJ_Button_事件紀錄_搜尋.Name = "plC_RJ_Button_事件紀錄_搜尋";
            this.plC_RJ_Button_事件紀錄_搜尋.OFF_文字內容 = "搜尋";
            this.plC_RJ_Button_事件紀錄_搜尋.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_事件紀錄_搜尋.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_事件紀錄_搜尋.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_事件紀錄_搜尋.ON_BorderSize = 5;
            this.plC_RJ_Button_事件紀錄_搜尋.ON_文字內容 = "搜尋";
            this.plC_RJ_Button_事件紀錄_搜尋.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14F);
            this.plC_RJ_Button_事件紀錄_搜尋.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_事件紀錄_搜尋.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_事件紀錄_搜尋.Size = new System.Drawing.Size(163, 69);
            this.plC_RJ_Button_事件紀錄_搜尋.State = false;
            this.plC_RJ_Button_事件紀錄_搜尋.TabIndex = 133;
            this.plC_RJ_Button_事件紀錄_搜尋.Text = "搜尋";
            this.plC_RJ_Button_事件紀錄_搜尋.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_事件紀錄_搜尋.Texts = "搜尋";
            this.plC_RJ_Button_事件紀錄_搜尋.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_事件紀錄_搜尋.字型鎖住 = false;
            this.plC_RJ_Button_事件紀錄_搜尋.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_事件紀錄_搜尋.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_事件紀錄_搜尋.文字鎖住 = false;
            this.plC_RJ_Button_事件紀錄_搜尋.讀取位元反向 = false;
            this.plC_RJ_Button_事件紀錄_搜尋.讀寫鎖住 = false;
            this.plC_RJ_Button_事件紀錄_搜尋.音效 = true;
            this.plC_RJ_Button_事件紀錄_搜尋.顯示 = false;
            this.plC_RJ_Button_事件紀錄_搜尋.顯示狀態 = false;
            // 
            // rJ_GroupBox5
            // 
            // 
            // rJ_GroupBox5.ContentsPanel
            // 
            this.rJ_GroupBox5.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.rJ_GroupBox5.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox5.ContentsPanel.BorderRadius = 5;
            this.rJ_GroupBox5.ContentsPanel.BorderSize = 2;
            this.rJ_GroupBox5.ContentsPanel.Controls.Add(this.rJ_CheckBox_事件紀錄_操作時間);
            this.rJ_GroupBox5.ContentsPanel.Controls.Add(this.label2);
            this.rJ_GroupBox5.ContentsPanel.Controls.Add(this.rJ_DatePicker_事件紀錄_操作結束時間);
            this.rJ_GroupBox5.ContentsPanel.Controls.Add(this.rJ_DatePicker_事件紀錄_操作起始時間);
            this.rJ_GroupBox5.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox5.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox5.ContentsPanel.IsSelected = false;
            this.rJ_GroupBox5.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.rJ_GroupBox5.ContentsPanel.Name = "ContentsPanel";
            this.rJ_GroupBox5.ContentsPanel.Size = new System.Drawing.Size(493, 68);
            this.rJ_GroupBox5.ContentsPanel.TabIndex = 2;
            this.rJ_GroupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_GroupBox5.GUID = "";
            this.rJ_GroupBox5.Location = new System.Drawing.Point(1146, 365);
            this.rJ_GroupBox5.Name = "rJ_GroupBox5";
            this.rJ_GroupBox5.PannelBackColor = System.Drawing.Color.White;
            this.rJ_GroupBox5.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox5.PannelBorderRadius = 5;
            this.rJ_GroupBox5.PannelBorderSize = 2;
            this.rJ_GroupBox5.Size = new System.Drawing.Size(493, 105);
            this.rJ_GroupBox5.TabIndex = 52;
            this.rJ_GroupBox5.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_GroupBox5.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_GroupBox5.TitleBorderRadius = 5;
            this.rJ_GroupBox5.TitleBorderSize = 0;
            this.rJ_GroupBox5.TitleFont = new System.Drawing.Font("新細明體", 12F);
            this.rJ_GroupBox5.TitleForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox5.TitleHeight = 37;
            this.rJ_GroupBox5.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_GroupBox5.TitleTexts = "時間條件";
            // 
            // rJ_CheckBox_事件紀錄_操作時間
            // 
            this.rJ_CheckBox_事件紀錄_操作時間.Bool = false;
            this.rJ_CheckBox_事件紀錄_操作時間.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_操作時間.ForeColor = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_操作時間.GUID = "";
            this.rJ_CheckBox_事件紀錄_操作時間.Location = new System.Drawing.Point(42, 19);
            this.rJ_CheckBox_事件紀錄_操作時間.MinimumSize = new System.Drawing.Size(45, 22);
            this.rJ_CheckBox_事件紀錄_操作時間.Name = "rJ_CheckBox_事件紀錄_操作時間";
            this.rJ_CheckBox_事件紀錄_操作時間.OffBackColor = System.Drawing.Color.Gray;
            this.rJ_CheckBox_事件紀錄_操作時間.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rJ_CheckBox_事件紀錄_操作時間.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_CheckBox_事件紀錄_操作時間.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_CheckBox_事件紀錄_操作時間.Size = new System.Drawing.Size(67, 31);
            this.rJ_CheckBox_事件紀錄_操作時間.SolidStyle = true;
            this.rJ_CheckBox_事件紀錄_操作時間.TabIndex = 141;
            this.rJ_CheckBox_事件紀錄_操作時間.UseVisualStyleBackColor = true;
            this.rJ_CheckBox_事件紀錄_操作時間.寫入元件位置 = "S5006";
            this.rJ_CheckBox_事件紀錄_操作時間.文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_操作時間.文字顏色 = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_操作時間.讀取元件位置 = "S5006";
            this.rJ_CheckBox_事件紀錄_操作時間.讀寫鎖住 = false;
            this.rJ_CheckBox_事件紀錄_操作時間.音效 = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("新細明體", 16F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(285, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "~";
            // 
            // rJ_DatePicker_事件紀錄_操作結束時間
            // 
            this.rJ_DatePicker_事件紀錄_操作結束時間.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_DatePicker_事件紀錄_操作結束時間.BorderSize = 0;
            this.rJ_DatePicker_事件紀錄_操作結束時間.Font = new System.Drawing.Font("新細明體", 15.75F);
            this.rJ_DatePicker_事件紀錄_操作結束時間.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.rJ_DatePicker_事件紀錄_操作結束時間.Location = new System.Drawing.Point(315, 17);
            this.rJ_DatePicker_事件紀錄_操作結束時間.MinimumSize = new System.Drawing.Size(150, 35);
            this.rJ_DatePicker_事件紀錄_操作結束時間.Name = "rJ_DatePicker_事件紀錄_操作結束時間";
            this.rJ_DatePicker_事件紀錄_操作結束時間.Size = new System.Drawing.Size(150, 35);
            this.rJ_DatePicker_事件紀錄_操作結束時間.SkinColor = System.Drawing.Color.SkyBlue;
            this.rJ_DatePicker_事件紀錄_操作結束時間.TabIndex = 1;
            this.rJ_DatePicker_事件紀錄_操作結束時間.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_DatePicker_事件紀錄_操作起始時間
            // 
            this.rJ_DatePicker_事件紀錄_操作起始時間.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_DatePicker_事件紀錄_操作起始時間.BorderSize = 0;
            this.rJ_DatePicker_事件紀錄_操作起始時間.Font = new System.Drawing.Font("新細明體", 15.75F);
            this.rJ_DatePicker_事件紀錄_操作起始時間.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.rJ_DatePicker_事件紀錄_操作起始時間.Location = new System.Drawing.Point(127, 17);
            this.rJ_DatePicker_事件紀錄_操作起始時間.MinimumSize = new System.Drawing.Size(150, 35);
            this.rJ_DatePicker_事件紀錄_操作起始時間.Name = "rJ_DatePicker_事件紀錄_操作起始時間";
            this.rJ_DatePicker_事件紀錄_操作起始時間.Size = new System.Drawing.Size(150, 35);
            this.rJ_DatePicker_事件紀錄_操作起始時間.SkinColor = System.Drawing.Color.SkyBlue;
            this.rJ_DatePicker_事件紀錄_操作起始時間.TabIndex = 0;
            this.rJ_DatePicker_事件紀錄_操作起始時間.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_GroupBox7
            // 
            // 
            // rJ_GroupBox7.ContentsPanel
            // 
            this.rJ_GroupBox7.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.rJ_GroupBox7.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox7.ContentsPanel.BorderRadius = 5;
            this.rJ_GroupBox7.ContentsPanel.BorderSize = 2;
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_TextBox_事件紀錄_病房名稱);
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_Lable26);
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_TextBox_事件紀錄_藥櫃編號);
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_Lable24);
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_TextBox_事件紀錄_姓名);
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_Lable22);
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_TextBox_事件紀錄_ID);
            this.rJ_GroupBox7.ContentsPanel.Controls.Add(this.rJ_Lable20);
            this.rJ_GroupBox7.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox7.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox7.ContentsPanel.IsSelected = false;
            this.rJ_GroupBox7.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.rJ_GroupBox7.ContentsPanel.Name = "ContentsPanel";
            this.rJ_GroupBox7.ContentsPanel.Size = new System.Drawing.Size(493, 146);
            this.rJ_GroupBox7.ContentsPanel.TabIndex = 2;
            this.rJ_GroupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_GroupBox7.GUID = "";
            this.rJ_GroupBox7.Location = new System.Drawing.Point(1146, 182);
            this.rJ_GroupBox7.Name = "rJ_GroupBox7";
            this.rJ_GroupBox7.PannelBackColor = System.Drawing.Color.White;
            this.rJ_GroupBox7.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox7.PannelBorderRadius = 5;
            this.rJ_GroupBox7.PannelBorderSize = 2;
            this.rJ_GroupBox7.Size = new System.Drawing.Size(493, 183);
            this.rJ_GroupBox7.TabIndex = 51;
            this.rJ_GroupBox7.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_GroupBox7.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_GroupBox7.TitleBorderRadius = 5;
            this.rJ_GroupBox7.TitleBorderSize = 0;
            this.rJ_GroupBox7.TitleFont = new System.Drawing.Font("新細明體", 12F);
            this.rJ_GroupBox7.TitleForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox7.TitleHeight = 37;
            this.rJ_GroupBox7.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_GroupBox7.TitleTexts = "一般條件";
            // 
            // rJ_TextBox_事件紀錄_病房名稱
            // 
            this.rJ_TextBox_事件紀錄_病房名稱.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_事件紀錄_病房名稱.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_TextBox_事件紀錄_病房名稱.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_事件紀錄_病房名稱.BorderRadius = 0;
            this.rJ_TextBox_事件紀錄_病房名稱.BorderSize = 2;
            this.rJ_TextBox_事件紀錄_病房名稱.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_事件紀錄_病房名稱.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_事件紀錄_病房名稱.GUID = "";
            this.rJ_TextBox_事件紀錄_病房名稱.Location = new System.Drawing.Point(364, 71);
            this.rJ_TextBox_事件紀錄_病房名稱.Multiline = false;
            this.rJ_TextBox_事件紀錄_病房名稱.Name = "rJ_TextBox_事件紀錄_病房名稱";
            this.rJ_TextBox_事件紀錄_病房名稱.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_事件紀錄_病房名稱.PassWordChar = false;
            this.rJ_TextBox_事件紀錄_病房名稱.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_事件紀錄_病房名稱.PlaceholderText = "";
            this.rJ_TextBox_事件紀錄_病房名稱.ShowTouchPannel = false;
            this.rJ_TextBox_事件紀錄_病房名稱.Size = new System.Drawing.Size(97, 36);
            this.rJ_TextBox_事件紀錄_病房名稱.TabIndex = 13;
            this.rJ_TextBox_事件紀錄_病房名稱.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_事件紀錄_病房名稱.Texts = "";
            this.rJ_TextBox_事件紀錄_病房名稱.UnderlineStyle = false;
            // 
            // rJ_Lable26
            // 
            this.rJ_Lable26.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable26.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable26.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable26.BorderRadius = 12;
            this.rJ_Lable26.BorderSize = 0;
            this.rJ_Lable26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable26.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable26.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable26.GUID = "";
            this.rJ_Lable26.Location = new System.Drawing.Point(244, 69);
            this.rJ_Lable26.Name = "rJ_Lable26";
            this.rJ_Lable26.Size = new System.Drawing.Size(109, 40);
            this.rJ_Lable26.TabIndex = 12;
            this.rJ_Lable26.Text = "病房名稱";
            this.rJ_Lable26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable26.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_事件紀錄_藥櫃編號
            // 
            this.rJ_TextBox_事件紀錄_藥櫃編號.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_事件紀錄_藥櫃編號.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_TextBox_事件紀錄_藥櫃編號.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_事件紀錄_藥櫃編號.BorderRadius = 0;
            this.rJ_TextBox_事件紀錄_藥櫃編號.BorderSize = 2;
            this.rJ_TextBox_事件紀錄_藥櫃編號.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_事件紀錄_藥櫃編號.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_事件紀錄_藥櫃編號.GUID = "";
            this.rJ_TextBox_事件紀錄_藥櫃編號.Location = new System.Drawing.Point(135, 71);
            this.rJ_TextBox_事件紀錄_藥櫃編號.Multiline = false;
            this.rJ_TextBox_事件紀錄_藥櫃編號.Name = "rJ_TextBox_事件紀錄_藥櫃編號";
            this.rJ_TextBox_事件紀錄_藥櫃編號.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_事件紀錄_藥櫃編號.PassWordChar = false;
            this.rJ_TextBox_事件紀錄_藥櫃編號.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_事件紀錄_藥櫃編號.PlaceholderText = "";
            this.rJ_TextBox_事件紀錄_藥櫃編號.ShowTouchPannel = false;
            this.rJ_TextBox_事件紀錄_藥櫃編號.Size = new System.Drawing.Size(97, 36);
            this.rJ_TextBox_事件紀錄_藥櫃編號.TabIndex = 11;
            this.rJ_TextBox_事件紀錄_藥櫃編號.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_事件紀錄_藥櫃編號.Texts = "";
            this.rJ_TextBox_事件紀錄_藥櫃編號.UnderlineStyle = false;
            // 
            // rJ_Lable24
            // 
            this.rJ_Lable24.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable24.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable24.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable24.BorderRadius = 12;
            this.rJ_Lable24.BorderSize = 0;
            this.rJ_Lable24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable24.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable24.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable24.GUID = "";
            this.rJ_Lable24.Location = new System.Drawing.Point(15, 69);
            this.rJ_Lable24.Name = "rJ_Lable24";
            this.rJ_Lable24.Size = new System.Drawing.Size(109, 40);
            this.rJ_Lable24.TabIndex = 10;
            this.rJ_Lable24.Text = "藥櫃編號";
            this.rJ_Lable24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable24.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_事件紀錄_姓名
            // 
            this.rJ_TextBox_事件紀錄_姓名.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_事件紀錄_姓名.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_TextBox_事件紀錄_姓名.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_事件紀錄_姓名.BorderRadius = 0;
            this.rJ_TextBox_事件紀錄_姓名.BorderSize = 2;
            this.rJ_TextBox_事件紀錄_姓名.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_事件紀錄_姓名.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_事件紀錄_姓名.GUID = "";
            this.rJ_TextBox_事件紀錄_姓名.Location = new System.Drawing.Point(364, 20);
            this.rJ_TextBox_事件紀錄_姓名.Multiline = false;
            this.rJ_TextBox_事件紀錄_姓名.Name = "rJ_TextBox_事件紀錄_姓名";
            this.rJ_TextBox_事件紀錄_姓名.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_事件紀錄_姓名.PassWordChar = false;
            this.rJ_TextBox_事件紀錄_姓名.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_事件紀錄_姓名.PlaceholderText = "";
            this.rJ_TextBox_事件紀錄_姓名.ShowTouchPannel = false;
            this.rJ_TextBox_事件紀錄_姓名.Size = new System.Drawing.Size(97, 36);
            this.rJ_TextBox_事件紀錄_姓名.TabIndex = 9;
            this.rJ_TextBox_事件紀錄_姓名.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_事件紀錄_姓名.Texts = "";
            this.rJ_TextBox_事件紀錄_姓名.UnderlineStyle = false;
            // 
            // rJ_Lable22
            // 
            this.rJ_Lable22.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable22.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable22.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable22.BorderRadius = 12;
            this.rJ_Lable22.BorderSize = 0;
            this.rJ_Lable22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable22.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable22.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable22.GUID = "";
            this.rJ_Lable22.Location = new System.Drawing.Point(244, 18);
            this.rJ_Lable22.Name = "rJ_Lable22";
            this.rJ_Lable22.Size = new System.Drawing.Size(109, 40);
            this.rJ_Lable22.TabIndex = 8;
            this.rJ_Lable22.Text = "姓名";
            this.rJ_Lable22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable22.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_事件紀錄_ID
            // 
            this.rJ_TextBox_事件紀錄_ID.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_事件紀錄_ID.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_TextBox_事件紀錄_ID.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_事件紀錄_ID.BorderRadius = 0;
            this.rJ_TextBox_事件紀錄_ID.BorderSize = 2;
            this.rJ_TextBox_事件紀錄_ID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_事件紀錄_ID.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_事件紀錄_ID.GUID = "";
            this.rJ_TextBox_事件紀錄_ID.Location = new System.Drawing.Point(135, 20);
            this.rJ_TextBox_事件紀錄_ID.Multiline = false;
            this.rJ_TextBox_事件紀錄_ID.Name = "rJ_TextBox_事件紀錄_ID";
            this.rJ_TextBox_事件紀錄_ID.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_事件紀錄_ID.PassWordChar = false;
            this.rJ_TextBox_事件紀錄_ID.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_事件紀錄_ID.PlaceholderText = "";
            this.rJ_TextBox_事件紀錄_ID.ShowTouchPannel = false;
            this.rJ_TextBox_事件紀錄_ID.Size = new System.Drawing.Size(97, 36);
            this.rJ_TextBox_事件紀錄_ID.TabIndex = 7;
            this.rJ_TextBox_事件紀錄_ID.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_事件紀錄_ID.Texts = "";
            this.rJ_TextBox_事件紀錄_ID.UnderlineStyle = false;
            // 
            // rJ_Lable20
            // 
            this.rJ_Lable20.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable20.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable20.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable20.BorderRadius = 12;
            this.rJ_Lable20.BorderSize = 0;
            this.rJ_Lable20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable20.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable20.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable20.GUID = "";
            this.rJ_Lable20.Location = new System.Drawing.Point(15, 18);
            this.rJ_Lable20.Name = "rJ_Lable20";
            this.rJ_Lable20.Size = new System.Drawing.Size(109, 40);
            this.rJ_Lable20.TabIndex = 6;
            this.rJ_Lable20.Text = "ID";
            this.rJ_Lable20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable20.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_GroupBox6
            // 
            // 
            // rJ_GroupBox6.ContentsPanel
            // 
            this.rJ_GroupBox6.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.rJ_GroupBox6.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox6.ContentsPanel.BorderRadius = 0;
            this.rJ_GroupBox6.ContentsPanel.BorderSize = 2;
            this.rJ_GroupBox6.ContentsPanel.Controls.Add(this.panel78);
            this.rJ_GroupBox6.ContentsPanel.Controls.Add(this.panel77);
            this.rJ_GroupBox6.ContentsPanel.Controls.Add(this.panel79);
            this.rJ_GroupBox6.ContentsPanel.Controls.Add(this.panel76);
            this.rJ_GroupBox6.ContentsPanel.Controls.Add(this.panel80);
            this.rJ_GroupBox6.ContentsPanel.Controls.Add(this.panel75);
            this.rJ_GroupBox6.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox6.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox6.ContentsPanel.IsSelected = false;
            this.rJ_GroupBox6.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.rJ_GroupBox6.ContentsPanel.Name = "ContentsPanel";
            this.rJ_GroupBox6.ContentsPanel.Size = new System.Drawing.Size(493, 145);
            this.rJ_GroupBox6.ContentsPanel.TabIndex = 2;
            this.rJ_GroupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_GroupBox6.GUID = "";
            this.rJ_GroupBox6.Location = new System.Drawing.Point(1146, 0);
            this.rJ_GroupBox6.Name = "rJ_GroupBox6";
            this.rJ_GroupBox6.PannelBackColor = System.Drawing.Color.White;
            this.rJ_GroupBox6.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox6.PannelBorderRadius = 0;
            this.rJ_GroupBox6.PannelBorderSize = 2;
            this.rJ_GroupBox6.Size = new System.Drawing.Size(493, 182);
            this.rJ_GroupBox6.TabIndex = 49;
            this.rJ_GroupBox6.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_GroupBox6.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_GroupBox6.TitleBorderRadius = 5;
            this.rJ_GroupBox6.TitleBorderSize = 0;
            this.rJ_GroupBox6.TitleFont = new System.Drawing.Font("新細明體", 12F);
            this.rJ_GroupBox6.TitleForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox6.TitleHeight = 37;
            this.rJ_GroupBox6.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_GroupBox6.TitleTexts = "事件類型";
            // 
            // panel78
            // 
            this.panel78.Controls.Add(this.rJ_CheckBox_事件紀錄_關閉門片);
            this.panel78.Controls.Add(this.rJ_Lable14);
            this.panel78.Location = new System.Drawing.Point(248, 96);
            this.panel78.Name = "panel78";
            this.panel78.Size = new System.Drawing.Size(200, 43);
            this.panel78.TabIndex = 11;
            // 
            // rJ_CheckBox_事件紀錄_關閉門片
            // 
            this.rJ_CheckBox_事件紀錄_關閉門片.Bool = false;
            this.rJ_CheckBox_事件紀錄_關閉門片.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_關閉門片.ForeColor = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_關閉門片.GUID = "";
            this.rJ_CheckBox_事件紀錄_關閉門片.Location = new System.Drawing.Point(0, 6);
            this.rJ_CheckBox_事件紀錄_關閉門片.MinimumSize = new System.Drawing.Size(45, 22);
            this.rJ_CheckBox_事件紀錄_關閉門片.Name = "rJ_CheckBox_事件紀錄_關閉門片";
            this.rJ_CheckBox_事件紀錄_關閉門片.OffBackColor = System.Drawing.Color.Gray;
            this.rJ_CheckBox_事件紀錄_關閉門片.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rJ_CheckBox_事件紀錄_關閉門片.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_CheckBox_事件紀錄_關閉門片.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_CheckBox_事件紀錄_關閉門片.Size = new System.Drawing.Size(67, 31);
            this.rJ_CheckBox_事件紀錄_關閉門片.SolidStyle = true;
            this.rJ_CheckBox_事件紀錄_關閉門片.TabIndex = 140;
            this.rJ_CheckBox_事件紀錄_關閉門片.UseVisualStyleBackColor = true;
            this.rJ_CheckBox_事件紀錄_關閉門片.寫入元件位置 = "S5005";
            this.rJ_CheckBox_事件紀錄_關閉門片.文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_關閉門片.文字顏色 = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_關閉門片.讀取元件位置 = "S5005";
            this.rJ_CheckBox_事件紀錄_關閉門片.讀寫鎖住 = false;
            this.rJ_CheckBox_事件紀錄_關閉門片.音效 = true;
            // 
            // rJ_Lable14
            // 
            this.rJ_Lable14.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable14.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable14.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable14.BorderRadius = 12;
            this.rJ_Lable14.BorderSize = 0;
            this.rJ_Lable14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable14.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable14.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable14.GUID = "";
            this.rJ_Lable14.Location = new System.Drawing.Point(72, 0);
            this.rJ_Lable14.Name = "rJ_Lable14";
            this.rJ_Lable14.Size = new System.Drawing.Size(125, 40);
            this.rJ_Lable14.TabIndex = 5;
            this.rJ_Lable14.Text = "關閉門片";
            this.rJ_Lable14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable14.TextColor = System.Drawing.Color.Black;
            // 
            // panel77
            // 
            this.panel77.Controls.Add(this.rJ_Lable12);
            this.panel77.Controls.Add(this.rJ_CheckBox_事件紀錄_登出);
            this.panel77.Location = new System.Drawing.Point(32, 96);
            this.panel77.Name = "panel77";
            this.panel77.Size = new System.Drawing.Size(200, 43);
            this.panel77.TabIndex = 8;
            // 
            // rJ_Lable12
            // 
            this.rJ_Lable12.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable12.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable12.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable12.BorderRadius = 12;
            this.rJ_Lable12.BorderSize = 0;
            this.rJ_Lable12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable12.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable12.GUID = "";
            this.rJ_Lable12.Location = new System.Drawing.Point(72, 0);
            this.rJ_Lable12.Name = "rJ_Lable12";
            this.rJ_Lable12.Size = new System.Drawing.Size(125, 40);
            this.rJ_Lable12.TabIndex = 5;
            this.rJ_Lable12.Text = "登出";
            this.rJ_Lable12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable12.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_CheckBox_事件紀錄_登出
            // 
            this.rJ_CheckBox_事件紀錄_登出.Bool = false;
            this.rJ_CheckBox_事件紀錄_登出.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_登出.ForeColor = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_登出.GUID = "";
            this.rJ_CheckBox_事件紀錄_登出.Location = new System.Drawing.Point(0, 8);
            this.rJ_CheckBox_事件紀錄_登出.MinimumSize = new System.Drawing.Size(45, 22);
            this.rJ_CheckBox_事件紀錄_登出.Name = "rJ_CheckBox_事件紀錄_登出";
            this.rJ_CheckBox_事件紀錄_登出.OffBackColor = System.Drawing.Color.Gray;
            this.rJ_CheckBox_事件紀錄_登出.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rJ_CheckBox_事件紀錄_登出.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_CheckBox_事件紀錄_登出.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_CheckBox_事件紀錄_登出.Size = new System.Drawing.Size(67, 31);
            this.rJ_CheckBox_事件紀錄_登出.SolidStyle = true;
            this.rJ_CheckBox_事件紀錄_登出.TabIndex = 136;
            this.rJ_CheckBox_事件紀錄_登出.UseVisualStyleBackColor = true;
            this.rJ_CheckBox_事件紀錄_登出.寫入元件位置 = "S5002";
            this.rJ_CheckBox_事件紀錄_登出.文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_登出.文字顏色 = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_登出.讀取元件位置 = "S5002";
            this.rJ_CheckBox_事件紀錄_登出.讀寫鎖住 = false;
            this.rJ_CheckBox_事件紀錄_登出.音效 = true;
            // 
            // panel79
            // 
            this.panel79.Controls.Add(this.rJ_Lable16);
            this.panel79.Controls.Add(this.rJ_CheckBox_事件紀錄_開啟門片);
            this.panel79.Location = new System.Drawing.Point(248, 51);
            this.panel79.Name = "panel79";
            this.panel79.Size = new System.Drawing.Size(200, 43);
            this.panel79.TabIndex = 10;
            // 
            // rJ_Lable16
            // 
            this.rJ_Lable16.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable16.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable16.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable16.BorderRadius = 12;
            this.rJ_Lable16.BorderSize = 0;
            this.rJ_Lable16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable16.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable16.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable16.GUID = "";
            this.rJ_Lable16.Location = new System.Drawing.Point(72, 0);
            this.rJ_Lable16.Name = "rJ_Lable16";
            this.rJ_Lable16.Size = new System.Drawing.Size(125, 40);
            this.rJ_Lable16.TabIndex = 5;
            this.rJ_Lable16.Text = "開啟門片";
            this.rJ_Lable16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable16.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_CheckBox_事件紀錄_開啟門片
            // 
            this.rJ_CheckBox_事件紀錄_開啟門片.Bool = false;
            this.rJ_CheckBox_事件紀錄_開啟門片.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_開啟門片.ForeColor = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_開啟門片.GUID = "";
            this.rJ_CheckBox_事件紀錄_開啟門片.Location = new System.Drawing.Point(0, 5);
            this.rJ_CheckBox_事件紀錄_開啟門片.MinimumSize = new System.Drawing.Size(45, 22);
            this.rJ_CheckBox_事件紀錄_開啟門片.Name = "rJ_CheckBox_事件紀錄_開啟門片";
            this.rJ_CheckBox_事件紀錄_開啟門片.OffBackColor = System.Drawing.Color.Gray;
            this.rJ_CheckBox_事件紀錄_開啟門片.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rJ_CheckBox_事件紀錄_開啟門片.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_CheckBox_事件紀錄_開啟門片.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_CheckBox_事件紀錄_開啟門片.Size = new System.Drawing.Size(67, 31);
            this.rJ_CheckBox_事件紀錄_開啟門片.SolidStyle = true;
            this.rJ_CheckBox_事件紀錄_開啟門片.TabIndex = 137;
            this.rJ_CheckBox_事件紀錄_開啟門片.UseVisualStyleBackColor = true;
            this.rJ_CheckBox_事件紀錄_開啟門片.寫入元件位置 = "S5004";
            this.rJ_CheckBox_事件紀錄_開啟門片.文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_開啟門片.文字顏色 = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_開啟門片.讀取元件位置 = "S5004";
            this.rJ_CheckBox_事件紀錄_開啟門片.讀寫鎖住 = false;
            this.rJ_CheckBox_事件紀錄_開啟門片.音效 = true;
            // 
            // panel76
            // 
            this.panel76.Controls.Add(this.rJ_CheckBox_事件紀錄_RFID登入);
            this.panel76.Controls.Add(this.rJ_Lable10);
            this.panel76.Location = new System.Drawing.Point(32, 51);
            this.panel76.Name = "panel76";
            this.panel76.Size = new System.Drawing.Size(200, 43);
            this.panel76.TabIndex = 7;
            // 
            // rJ_CheckBox_事件紀錄_RFID登入
            // 
            this.rJ_CheckBox_事件紀錄_RFID登入.Bool = false;
            this.rJ_CheckBox_事件紀錄_RFID登入.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_RFID登入.ForeColor = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_RFID登入.GUID = "";
            this.rJ_CheckBox_事件紀錄_RFID登入.Location = new System.Drawing.Point(0, 5);
            this.rJ_CheckBox_事件紀錄_RFID登入.MinimumSize = new System.Drawing.Size(45, 22);
            this.rJ_CheckBox_事件紀錄_RFID登入.Name = "rJ_CheckBox_事件紀錄_RFID登入";
            this.rJ_CheckBox_事件紀錄_RFID登入.OffBackColor = System.Drawing.Color.Gray;
            this.rJ_CheckBox_事件紀錄_RFID登入.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rJ_CheckBox_事件紀錄_RFID登入.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_CheckBox_事件紀錄_RFID登入.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_CheckBox_事件紀錄_RFID登入.Size = new System.Drawing.Size(67, 31);
            this.rJ_CheckBox_事件紀錄_RFID登入.SolidStyle = true;
            this.rJ_CheckBox_事件紀錄_RFID登入.TabIndex = 135;
            this.rJ_CheckBox_事件紀錄_RFID登入.UseVisualStyleBackColor = true;
            this.rJ_CheckBox_事件紀錄_RFID登入.寫入元件位置 = "S5001";
            this.rJ_CheckBox_事件紀錄_RFID登入.文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_RFID登入.文字顏色 = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_RFID登入.讀取元件位置 = "S5001";
            this.rJ_CheckBox_事件紀錄_RFID登入.讀寫鎖住 = false;
            this.rJ_CheckBox_事件紀錄_RFID登入.音效 = true;
            // 
            // rJ_Lable10
            // 
            this.rJ_Lable10.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable10.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable10.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable10.BorderRadius = 12;
            this.rJ_Lable10.BorderSize = 0;
            this.rJ_Lable10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable10.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable10.GUID = "";
            this.rJ_Lable10.Location = new System.Drawing.Point(72, 0);
            this.rJ_Lable10.Name = "rJ_Lable10";
            this.rJ_Lable10.Size = new System.Drawing.Size(125, 40);
            this.rJ_Lable10.TabIndex = 5;
            this.rJ_Lable10.Text = "RFID登入";
            this.rJ_Lable10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable10.TextColor = System.Drawing.Color.Black;
            // 
            // panel80
            // 
            this.panel80.Controls.Add(this.rJ_Lable18);
            this.panel80.Controls.Add(this.rJ_CheckBox_事件紀錄_門片未關閉異常);
            this.panel80.Location = new System.Drawing.Point(248, 3);
            this.panel80.Name = "panel80";
            this.panel80.Size = new System.Drawing.Size(200, 43);
            this.panel80.TabIndex = 9;
            // 
            // rJ_Lable18
            // 
            this.rJ_Lable18.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable18.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable18.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable18.BorderRadius = 12;
            this.rJ_Lable18.BorderSize = 0;
            this.rJ_Lable18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable18.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.rJ_Lable18.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable18.GUID = "";
            this.rJ_Lable18.Location = new System.Drawing.Point(72, 0);
            this.rJ_Lable18.Name = "rJ_Lable18";
            this.rJ_Lable18.Size = new System.Drawing.Size(125, 40);
            this.rJ_Lable18.TabIndex = 5;
            this.rJ_Lable18.Text = "門片未關閉異常";
            this.rJ_Lable18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable18.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_CheckBox_事件紀錄_門片未關閉異常
            // 
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.Bool = false;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.ForeColor = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.GUID = "";
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.Location = new System.Drawing.Point(0, 7);
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.MinimumSize = new System.Drawing.Size(45, 22);
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.Name = "rJ_CheckBox_事件紀錄_門片未關閉異常";
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.OffBackColor = System.Drawing.Color.Gray;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.Size = new System.Drawing.Size(67, 31);
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.SolidStyle = true;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.TabIndex = 139;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.UseVisualStyleBackColor = true;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.寫入元件位置 = "S5003";
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.文字顏色 = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.讀取元件位置 = "S5003";
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.讀寫鎖住 = false;
            this.rJ_CheckBox_事件紀錄_門片未關閉異常.音效 = true;
            // 
            // panel75
            // 
            this.panel75.Controls.Add(this.rJ_CheckBox_事件紀錄_密碼登入);
            this.panel75.Controls.Add(this.rJ_Lable9);
            this.panel75.Location = new System.Drawing.Point(32, 3);
            this.panel75.Name = "panel75";
            this.panel75.Size = new System.Drawing.Size(200, 43);
            this.panel75.TabIndex = 6;
            // 
            // rJ_CheckBox_事件紀錄_密碼登入
            // 
            this.rJ_CheckBox_事件紀錄_密碼登入.Bool = false;
            this.rJ_CheckBox_事件紀錄_密碼登入.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_密碼登入.ForeColor = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_密碼登入.GUID = "";
            this.rJ_CheckBox_事件紀錄_密碼登入.Location = new System.Drawing.Point(0, 5);
            this.rJ_CheckBox_事件紀錄_密碼登入.MinimumSize = new System.Drawing.Size(45, 22);
            this.rJ_CheckBox_事件紀錄_密碼登入.Name = "rJ_CheckBox_事件紀錄_密碼登入";
            this.rJ_CheckBox_事件紀錄_密碼登入.OffBackColor = System.Drawing.Color.Gray;
            this.rJ_CheckBox_事件紀錄_密碼登入.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.rJ_CheckBox_事件紀錄_密碼登入.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_CheckBox_事件紀錄_密碼登入.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_CheckBox_事件紀錄_密碼登入.Size = new System.Drawing.Size(67, 31);
            this.rJ_CheckBox_事件紀錄_密碼登入.SolidStyle = true;
            this.rJ_CheckBox_事件紀錄_密碼登入.TabIndex = 134;
            this.rJ_CheckBox_事件紀錄_密碼登入.UseVisualStyleBackColor = true;
            this.rJ_CheckBox_事件紀錄_密碼登入.寫入元件位置 = "S5000";
            this.rJ_CheckBox_事件紀錄_密碼登入.文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rJ_CheckBox_事件紀錄_密碼登入.文字顏色 = System.Drawing.Color.Black;
            this.rJ_CheckBox_事件紀錄_密碼登入.讀取元件位置 = "S5000";
            this.rJ_CheckBox_事件紀錄_密碼登入.讀寫鎖住 = false;
            this.rJ_CheckBox_事件紀錄_密碼登入.音效 = true;
            // 
            // rJ_Lable9
            // 
            this.rJ_Lable9.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable9.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable9.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable9.BorderRadius = 12;
            this.rJ_Lable9.BorderSize = 0;
            this.rJ_Lable9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable9.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable9.GUID = "";
            this.rJ_Lable9.Location = new System.Drawing.Point(72, 0);
            this.rJ_Lable9.Name = "rJ_Lable9";
            this.rJ_Lable9.Size = new System.Drawing.Size(125, 40);
            this.rJ_Lable9.TabIndex = 5;
            this.rJ_Lable9.Text = "密碼登入";
            this.rJ_Lable9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable9.TextColor = System.Drawing.Color.Black;
            // 
            // sqL_DataGridView_事件紀錄
            // 
            this.sqL_DataGridView_事件紀錄.AutoSelectToDeep = true;
            this.sqL_DataGridView_事件紀錄.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_事件紀錄.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_事件紀錄.BorderRadius = 0;
            this.sqL_DataGridView_事件紀錄.BorderSize = 2;
            this.sqL_DataGridView_事件紀錄.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_事件紀錄.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_事件紀錄.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_事件紀錄.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_事件紀錄.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_事件紀錄.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_事件紀錄.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_事件紀錄.columnHeadersHeight = 23;
            this.sqL_DataGridView_事件紀錄.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns"))));
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns1"))));
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns2"))));
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns3"))));
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns4"))));
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns5"))));
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns6"))));
            this.sqL_DataGridView_事件紀錄.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_事件紀錄.Columns7"))));
            this.sqL_DataGridView_事件紀錄.Dock = System.Windows.Forms.DockStyle.Left;
            this.sqL_DataGridView_事件紀錄.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_事件紀錄.ImageBox = false;
            this.sqL_DataGridView_事件紀錄.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_事件紀錄.Name = "sqL_DataGridView_事件紀錄";
            this.sqL_DataGridView_事件紀錄.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_事件紀錄.Password = "user82822040";
            this.sqL_DataGridView_事件紀錄.Port = ((uint)(3306u));
            this.sqL_DataGridView_事件紀錄.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_事件紀錄.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_事件紀錄.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_事件紀錄.RowsHeight = 40;
            this.sqL_DataGridView_事件紀錄.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_事件紀錄.Server = "localhost";
            this.sqL_DataGridView_事件紀錄.Size = new System.Drawing.Size(1146, 1006);
            this.sqL_DataGridView_事件紀錄.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_事件紀錄.TabIndex = 39;
            this.sqL_DataGridView_事件紀錄.TableName = "event_page";
            this.sqL_DataGridView_事件紀錄.UserName = "root";
            this.sqL_DataGridView_事件紀錄.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_事件紀錄.可選擇多列 = false;
            this.sqL_DataGridView_事件紀錄.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_事件紀錄.自動換行 = true;
            this.sqL_DataGridView_事件紀錄.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_事件紀錄.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_事件紀錄.顯示CheckBox = false;
            this.sqL_DataGridView_事件紀錄.顯示首列 = true;
            this.sqL_DataGridView_事件紀錄.顯示首行 = true;
            this.sqL_DataGridView_事件紀錄.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_事件紀錄.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // 暫存區
            // 
            this.暫存區.AutoScroll = true;
            this.暫存區.BackColor = System.Drawing.SystemColors.Window;
            this.暫存區.Location = new System.Drawing.Point(4, 25);
            this.暫存區.Name = "暫存區";
            this.暫存區.Size = new System.Drawing.Size(1639, 1006);
            this.暫存區.TabIndex = 3;
            this.暫存區.Text = "暫存區";
            // 
            // plC_AlarmFlow1
            // 
            this.plC_AlarmFlow1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plC_AlarmFlow1.Location = new System.Drawing.Point(0, 1035);
            this.plC_AlarmFlow1.Name = "plC_AlarmFlow1";
            this.plC_AlarmFlow1.Size = new System.Drawing.Size(1904, 26);
            this.plC_AlarmFlow1.TabIndex = 0;
            this.plC_AlarmFlow1.Visible = false;
            this.plC_AlarmFlow1.捲動速度 = 200;
            this.plC_AlarmFlow1.文字字體 = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_AlarmFlow1.文字顏色 = System.Drawing.Color.White;
            this.plC_AlarmFlow1.自動隱藏 = false;
            this.plC_AlarmFlow1.警報編輯 = ((System.Collections.Generic.List<string>)(resources.GetObject("plC_AlarmFlow1.警報編輯")));
            this.plC_AlarmFlow1.顯示警報編號 = false;
            // 
            // pannel_Box1
            // 
            this.pannel_Box1.Alarm_Time_Adress = "";
            this.pannel_Box1.enum_Door_State = 勤務傳送櫃.Pannel_Box.enum_door_state.None;
            this.pannel_Box1.Input_Adress = "";
            this.pannel_Box1.LED_Adress = "";
            this.pannel_Box1.Led_output_num = -1;
            this.pannel_Box1.LED_State_Adress = "";
            this.pannel_Box1.Location = new System.Drawing.Point(0, 0);
            this.pannel_Box1.Lock_input_num = -1;
            this.pannel_Box1.Lock_output_num = -1;
            this.pannel_Box1.MVisible = true;
            this.pannel_Box1.Name = "pannel_Box1";
            this.pannel_Box1.Output_Adress = "";
            this.pannel_Box1.Port = 0;
            this.pannel_Box1.RFID_num = -1;
            this.pannel_Box1.Sensor_Input_Adress = "";
            this.pannel_Box1.Sensor_input_num = -1;
            this.pannel_Box1.Size = new System.Drawing.Size(231, 177);
            this.pannel_Box1.SQL_Write = false;
            this.pannel_Box1.TabIndex = 0;
            this.pannel_Box1.WardFont = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1061);
            this.Controls.Add(this.plC_ScreenPage_Main);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.plC_AlarmFlow1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "勤務傳送櫃";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_Main.ResumeLayout(false);
            this.panel232.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.plC_ScreenPage_Main.ResumeLayout(false);
            this.主畫面.ResumeLayout(false);
            this.plC_ScreenPage_主畫面_PannelBox.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.tabPage17.ResumeLayout(false);
            this.tabPage18.ResumeLayout(false);
            this.panel_主畫面_PannelBox.ResumeLayout(false);
            this.登入畫面.ResumeLayout(false);
            this.rJ_GroupBox1.ContentsPanel.ResumeLayout(false);
            this.rJ_GroupBox1.ResumeLayout(false);
            this.panel185.ResumeLayout(false);
            this.panel185.PerformLayout();
            this.panel183.ResumeLayout(false);
            this.panel183.PerformLayout();
            this.人員資料.ResumeLayout(false);
            this.plC_ScreenPage_人員資料_權限設定.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel_權限設定.ResumeLayout(false);
            this.panel29.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.plC_ScreenPage_人員資料_開門權限.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.panel_人員資料_開門權限.ResumeLayout(false);
            this.panel_人員資料_權限設定.ResumeLayout(false);
            this.rJ_GroupBox20.ContentsPanel.ResumeLayout(false);
            this.rJ_GroupBox20.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel149.ResumeLayout(false);
            this.panel149.PerformLayout();
            this.panel150.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.系統頁面.ResumeLayout(false);
            this.plC_ScreenPage_系統頁面.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.plC_RJ_GroupBox9.ContentsPanel.ResumeLayout(false);
            this.plC_RJ_GroupBox9.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel_系統頁面.ResumeLayout(false);
            this.事件紀錄.ResumeLayout(false);
            this.rJ_GroupBox5.ContentsPanel.ResumeLayout(false);
            this.rJ_GroupBox5.ContentsPanel.PerformLayout();
            this.rJ_GroupBox5.ResumeLayout(false);
            this.rJ_GroupBox7.ContentsPanel.ResumeLayout(false);
            this.rJ_GroupBox7.ResumeLayout(false);
            this.rJ_GroupBox6.ContentsPanel.ResumeLayout(false);
            this.rJ_GroupBox6.ResumeLayout(false);
            this.panel78.ResumeLayout(false);
            this.panel77.ResumeLayout(false);
            this.panel79.ResumeLayout(false);
            this.panel76.ResumeLayout(false);
            this.panel80.ResumeLayout(false);
            this.panel75.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.PLC_AlarmFlow plC_AlarmFlow1;
        private MyUI.PLC_ScreenPage plC_ScreenPage_Main;
        private System.Windows.Forms.TabPage 主畫面;
        private System.Windows.Forms.TabPage 人員資料;
        private System.Windows.Forms.TabPage 系統頁面;
        private System.Windows.Forms.TabPage 暫存區;
        private MyUI.PLC_UI_Init plC_UI_Init;
        private LadderUI.LowerMachine_Panel lowerMachine_Pane;
        private System.Windows.Forms.Panel panel_主畫面_PannelBox;
        private System.Windows.Forms.TabPage 登入畫面;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_SaveExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadExcel;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.TabPage 事件紀錄;
        private SQLUI.SQL_DataGridView sqL_DataGridView_事件紀錄;
        private System.Windows.Forms.Panel panel_Main;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton1;
        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton_主畫面;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton_事件紀錄;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton_登入畫面;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton_系統頁面;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton_人員資料;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private Pannel_Box pannel_Box1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MyUI.RJ_GroupBox rJ_GroupBox6;
        private MyUI.RJ_Lable rJ_Lable9;
        private System.Windows.Forms.Panel panel76;
        private MyUI.RJ_Lable rJ_Lable10;
        private System.Windows.Forms.Panel panel75;
        private System.Windows.Forms.Panel panel78;
        private MyUI.RJ_Lable rJ_Lable14;
        private System.Windows.Forms.Panel panel77;
        private MyUI.RJ_Lable rJ_Lable12;
        private System.Windows.Forms.Panel panel79;
        private MyUI.RJ_Lable rJ_Lable16;
        private System.Windows.Forms.Panel panel80;
        private MyUI.RJ_Lable rJ_Lable18;
        private MyUI.RJ_GroupBox rJ_GroupBox5;
        private System.Windows.Forms.Label label2;
        private MyUI.RJ_DatePicker rJ_DatePicker_事件紀錄_操作結束時間;
        private MyUI.RJ_DatePicker rJ_DatePicker_事件紀錄_操作起始時間;
        private MyUI.RJ_GroupBox rJ_GroupBox7;
        private MyUI.RJ_TextBox rJ_TextBox_事件紀錄_病房名稱;
        private MyUI.RJ_Lable rJ_Lable26;
        private MyUI.RJ_TextBox rJ_TextBox_事件紀錄_藥櫃編號;
        private MyUI.RJ_Lable rJ_Lable24;
        private MyUI.RJ_TextBox rJ_TextBox_事件紀錄_姓名;
        private MyUI.RJ_Lable rJ_Lable22;
        private MyUI.RJ_TextBox rJ_TextBox_事件紀錄_ID;
        private MyUI.RJ_Lable rJ_Lable20;
        private System.Windows.Forms.GroupBox groupBox18;
        private RFID_FX600lib.RFID_FX600_UI rfiD_FX600_UI;
        private System.Windows.Forms.Panel panel_系統頁面;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton6;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton2;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton5;
        private MyUI.PLC_ScreenPage plC_ScreenPage_系統頁面;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage4;
        private H_Pannel_lib.RFID_UI rfiD_UI;
        private System.Windows.Forms.TabPage tabPage6;
        private MyUI.PLC_RJ_GroupBox plC_RJ_GroupBox9;
        private SQLUI.SQL_DataGridView sqL_DataGridView_Box_Index_Table;
        private MyUI.RJ_GroupBox rJ_GroupBox1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_登入畫面_登入;
        private System.Windows.Forms.Panel panel185;
        private System.Windows.Forms.TextBox textBox_登入畫面_帳號;
        private System.Windows.Forms.Panel panel186;
        private System.Windows.Forms.Panel panel183;
        private System.Windows.Forms.TextBox textBox_登入畫面_密碼;
        private System.Windows.Forms.Panel panel184;
        private System.Windows.Forms.TabPage tabPage2;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton3;
        private MySQL_Login.LoginUI loginUI;
        private MyUI.RJ_GroupBox rJ_GroupBox20;
        private MyUI.PLC_RJ_Button plC_RJ_Button_人員資料_清除內容;
        private MyUI.RJ_TextBox rJ_TextBox_人員資料_識別圖案;
        private MyUI.PLC_RJ_Button plC_RJ_Button_人員資料_刪除;
        private MyUI.PLC_RJ_Button plC_RJ_Button_人員資料_登錄;
        private MyUI.PLC_RJ_Button plC_RJ_Button_人員資料_匯入;
        private MyUI.PLC_RJ_Button plC_RJ_Button_人員資料_匯出;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private MyUI.RJ_TextBox rJ_TextBox_人員資料_一維條碼;
        private System.Windows.Forms.Label label1;
        private MyUI.RJ_TextBox rJ_TextBox_人員資料_卡號;
        private MyUI.RJ_ComboBox comboBox_人員資料_權限等級;
        private MyUI.RJ_TextBox rJ_TextBox_人員資料_單位;
        private MyUI.RJ_TextBox rJ_TextBox_人員資料_密碼;
        private MyUI.RJ_TextBox rJ_TextBox_人員資料_姓名;
        private MyUI.RJ_TextBox rJ_TextBox_人員資料_ID;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel149;
        private System.Windows.Forms.Panel panel150;
        private System.Windows.Forms.Button button_人員資料_顏色選擇;
        private System.Windows.Forms.TextBox textBox_人員資料_顏色;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MyUI.RJ_RatioButton rJ_RatioButton_人員資料_男;
        private MyUI.RJ_RatioButton rJ_RatioButton_人員資料_女;
        private SQLUI.SQL_DataGridView sqL_DataGridView_人員資料;
        private System.Windows.Forms.Panel panel232;
        private MyUI.RJ_TextBox rJ_TextBox_登入者顏色;
        private MyUI.RJ_TextBox rJ_TextBox_登入者姓名;
        private MyUI.RJ_TextBox rJ_TextBox_登入者ID;
        private MyUI.RJ_Lable rJ_Lable2;
        private MyUI.RJ_Lable rJ_Lable66;
        private MyUI.PLC_RJ_Button plC_RJ_Button_登入畫面_登出;
        private System.Windows.Forms.ColorDialog colorDialog;
        private MyUI.PLC_RJ_Button plC_RJ_Button_事件紀錄_搜尋;
        private MyUI.PLC_ScreenPage plC_ScreenPage_人員資料_權限設定;
        private System.Windows.Forms.TabPage tabPage3;
        private MyUI.RJ_Pannel panel_權限設定;
        private MySQL_Login.LoginIndex_Pannel loginIndex_Pannel;
        private MyUI.PLC_RJ_Button plC_Button_權限設定_設定至Server;
        private MyUI.PLC_RJ_ComboBox plC_RJ_ComboBox_權限管理_權限等級;
        private MyUI.RJ_Lable rJ_Lable64;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Panel panel_人員資料_權限設定;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton7;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton4;
        private System.Windows.Forms.Panel panel29;
        private MyUI.PLC_ScreenPage plC_ScreenPage_人員資料_開門權限;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_01;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.Panel panel_人員資料_開門權限;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton15;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton14;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton13;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton12;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton11;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton10;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton9;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton8;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_02;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_03;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_04;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_05;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_06;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_07;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_開門權限_08;
        private MyUI.PLC_RJ_Button plC_RJ_Button_Box_Index_Table_匯入;
        private MyUI.PLC_RJ_Button plC_RJ_Button_Box_Index_Table_匯出;
        private MyUI.PLC_RJ_Button plC_RJ_Button_Box_Index_Table_刪除;
        private MyUI.PLC_RJ_Button plC_RJ_Button_Box_Index_Table_更新;
        private MyUI.PLC_ScreenPage plC_ScreenPage_主畫面_PannelBox;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.TabPage tabPage17;
        private System.Windows.Forms.TabPage tabPage18;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton20;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton21;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton22;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton23;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_PannelBox01;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_PannelBox02;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_PannelBox03;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_PannelBox04;
        private MyUI.PLC_RJ_ChechBox rJ_CheckBox_事件紀錄_操作時間;
        private MyUI.PLC_RJ_ChechBox rJ_CheckBox_事件紀錄_關閉門片;
        private MyUI.PLC_RJ_ChechBox rJ_CheckBox_事件紀錄_登出;
        private MyUI.PLC_RJ_ChechBox rJ_CheckBox_事件紀錄_開啟門片;
        private MyUI.PLC_RJ_ChechBox rJ_CheckBox_事件紀錄_RFID登入;
        private MyUI.PLC_RJ_ChechBox rJ_CheckBox_事件紀錄_門片未關閉異常;
        private MyUI.PLC_RJ_ChechBox rJ_CheckBox_事件紀錄_密碼登入;
        private System.Windows.Forms.GroupBox groupBox1;
        private MyUI.PLC_NumBox plC_NumBox_開門異常時間;
        private System.Windows.Forms.GroupBox groupBox2;
        private MyUI.PLC_NumBox plC_NumBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private MyUI.PLC_NumBox plC_NumBox2;
        private MyUI.PLC_RJ_Button plC_RJ_Button_人員資料_開門權限全關;
        private MyUI.PLC_RJ_Button plC_RJ_Button_人員資料_開門權限全開;
    }
}

