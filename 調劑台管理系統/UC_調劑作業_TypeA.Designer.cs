namespace 調劑台管理系統
{
    partial class UC_調劑作業_TypeA
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel114 = new System.Windows.Forms.Panel();
            this.panel121 = new System.Windows.Forms.Panel();
            this.rJ_Lable_Title = new MyUI.RJ_Lable();
            this.panel119 = new System.Windows.Forms.Panel();
            this.textBox_帳號 = new MyUI.RJ_TextBox();
            this.textBox_密碼 = new MyUI.RJ_TextBox();
            this.plC_RJ_Button_登入 = new MyUI.PLC_RJ_Button();
            this.plC_Button_領 = new MyUI.PLC_RJ_Button();
            this.plC_Button_退 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_取消作業 = new MyUI.PLC_RJ_Button();
            this.panel_藥品資訊 = new System.Windows.Forms.Panel();
            this.panel_藥品圖片 = new System.Windows.Forms.Panel();
            this.pictureBox_藥品圖片01 = new System.Windows.Forms.PictureBox();
            this.pictureBox_藥品圖片02 = new System.Windows.Forms.PictureBox();
            this.rJ_Lable_MedGPT_Title = new MyUI.RJ_Lable();
            this.panel_領藥台01_處方資訊 = new System.Windows.Forms.Panel();
            this.panel_病患資訊 = new System.Windows.Forms.Panel();
            this.rJ_Lable_領藥號 = new MyUI.RJ_Lable();
            this.rJ_Lable2 = new MyUI.RJ_Lable();
            this.rJ_Lable_病歷號 = new MyUI.RJ_Lable();
            this.rJ_Lable4 = new MyUI.RJ_Lable();
            this.rJ_Lable_年齡 = new MyUI.RJ_Lable();
            this.rJ_Lable6 = new MyUI.RJ_Lable();
            this.rJ_Lable_姓名 = new MyUI.RJ_Lable();
            this.rJ_Lable8 = new MyUI.RJ_Lable();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_診斷資訊 = new System.Windows.Forms.Panel();
            this.rJ_Lable_診斷 = new MyUI.RJ_Lable();
            this.sqL_DataGridView_領藥內容 = new SQLUI.SQL_DataGridView();
            this.contextMenuStrip_調劑畫面 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_調劑畫面_顯示設定 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel114.SuspendLayout();
            this.panel121.SuspendLayout();
            this.panel119.SuspendLayout();
            this.panel_藥品資訊.SuspendLayout();
            this.panel_藥品圖片.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品圖片01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品圖片02)).BeginInit();
            this.panel_領藥台01_處方資訊.SuspendLayout();
            this.panel_病患資訊.SuspendLayout();
            this.panel_診斷資訊.SuspendLayout();
            this.contextMenuStrip_調劑畫面.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel114
            // 
            this.panel114.Controls.Add(this.panel121);
            this.panel114.Controls.Add(this.panel119);
            this.panel114.Controls.Add(this.plC_RJ_Button_登入);
            this.panel114.Controls.Add(this.plC_Button_領);
            this.panel114.Controls.Add(this.plC_Button_退);
            this.panel114.Controls.Add(this.plC_RJ_Button_取消作業);
            this.panel114.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel114.Location = new System.Drawing.Point(0, 0);
            this.panel114.Name = "panel114";
            this.panel114.Padding = new System.Windows.Forms.Padding(3);
            this.panel114.Size = new System.Drawing.Size(880, 76);
            this.panel114.TabIndex = 1;
            // 
            // panel121
            // 
            this.panel121.Controls.Add(this.rJ_Lable_Title);
            this.panel121.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel121.Location = new System.Drawing.Point(3, 3);
            this.panel121.Name = "panel121";
            this.panel121.Padding = new System.Windows.Forms.Padding(2);
            this.panel121.Size = new System.Drawing.Size(440, 70);
            this.panel121.TabIndex = 142;
            // 
            // rJ_Lable_Title
            // 
            this.rJ_Lable_Title.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_Title.BackgroundColor = System.Drawing.Color.DarkGray;
            this.rJ_Lable_Title.BorderColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_Title.BorderRadius = 20;
            this.rJ_Lable_Title.BorderSize = 2;
            this.rJ_Lable_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Lable_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_Title.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_Title.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_Title.GUID = "";
            this.rJ_Lable_Title.Location = new System.Drawing.Point(2, 2);
            this.rJ_Lable_Title.Name = "rJ_Lable_Title";
            this.rJ_Lable_Title.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_Title.ShadowSize = 3;
            this.rJ_Lable_Title.Size = new System.Drawing.Size(436, 66);
            this.rJ_Lable_Title.TabIndex = 1;
            this.rJ_Lable_Title.Text = "-------";
            this.rJ_Lable_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_Title.TextColor = System.Drawing.Color.White;
            // 
            // panel119
            // 
            this.panel119.Controls.Add(this.textBox_帳號);
            this.panel119.Controls.Add(this.textBox_密碼);
            this.panel119.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel119.Location = new System.Drawing.Point(443, 3);
            this.panel119.Name = "panel119";
            this.panel119.Size = new System.Drawing.Size(134, 70);
            this.panel119.TabIndex = 141;
            // 
            // textBox_帳號
            // 
            this.textBox_帳號.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_帳號.BorderColor = System.Drawing.Color.RoyalBlue;
            this.textBox_帳號.BorderFocusColor = System.Drawing.Color.HotPink;
            this.textBox_帳號.BorderRadius = 10;
            this.textBox_帳號.BorderSize = 1;
            this.textBox_帳號.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_帳號.ForeColor = System.Drawing.Color.DimGray;
            this.textBox_帳號.GUID = "";
            this.textBox_帳號.Location = new System.Drawing.Point(21, 1);
            this.textBox_帳號.Multiline = false;
            this.textBox_帳號.Name = "textBox_帳號";
            this.textBox_帳號.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.textBox_帳號.PassWordChar = false;
            this.textBox_帳號.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.textBox_帳號.PlaceholderText = "UserName";
            this.textBox_帳號.ShowTouchPannel = false;
            this.textBox_帳號.Size = new System.Drawing.Size(108, 34);
            this.textBox_帳號.TabIndex = 4;
            this.textBox_帳號.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox_帳號.Texts = "";
            this.textBox_帳號.UnderlineStyle = false;
            // 
            // textBox_密碼
            // 
            this.textBox_密碼.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_密碼.BorderColor = System.Drawing.Color.RoyalBlue;
            this.textBox_密碼.BorderFocusColor = System.Drawing.Color.HotPink;
            this.textBox_密碼.BorderRadius = 10;
            this.textBox_密碼.BorderSize = 1;
            this.textBox_密碼.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_密碼.ForeColor = System.Drawing.Color.DimGray;
            this.textBox_密碼.GUID = "";
            this.textBox_密碼.Location = new System.Drawing.Point(21, 37);
            this.textBox_密碼.Multiline = false;
            this.textBox_密碼.Name = "textBox_密碼";
            this.textBox_密碼.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.textBox_密碼.PassWordChar = false;
            this.textBox_密碼.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.textBox_密碼.PlaceholderText = "Password";
            this.textBox_密碼.ShowTouchPannel = false;
            this.textBox_密碼.Size = new System.Drawing.Size(108, 34);
            this.textBox_密碼.TabIndex = 4;
            this.textBox_密碼.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox_密碼.Texts = "";
            this.textBox_密碼.UnderlineStyle = false;
            // 
            // plC_RJ_Button_登入
            // 
            this.plC_RJ_Button_登入.AutoResetState = true;
            this.plC_RJ_Button_登入.BackgroundColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_登入.Bool = false;
            this.plC_RJ_Button_登入.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_登入.BorderRadius = 15;
            this.plC_RJ_Button_登入.BorderSize = 0;
            this.plC_RJ_Button_登入.but_press = false;
            this.plC_RJ_Button_登入.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_登入.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_登入.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_登入.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_登入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_登入.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_登入.GUID = "";
            this.plC_RJ_Button_登入.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_登入.Image_padding = new System.Windows.Forms.Padding(5, 5, 10, 5);
            this.plC_RJ_Button_登入.Location = new System.Drawing.Point(577, 3);
            this.plC_RJ_Button_登入.Name = "plC_RJ_Button_登入";
            this.plC_RJ_Button_登入.OFF_文字內容 = "登入";
            this.plC_RJ_Button_登入.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_登入.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_登入.OFF_背景顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_登入.ON_BorderSize = 1;
            this.plC_RJ_Button_登入.ON_文字內容 = "登入";
            this.plC_RJ_Button_登入.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_登入.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_登入.ON_背景顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_登入.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_登入.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_登入.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_登入.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_登入.ShadowSize = 3;
            this.plC_RJ_Button_登入.ShowLoadingForm = false;
            this.plC_RJ_Button_登入.Size = new System.Drawing.Size(84, 70);
            this.plC_RJ_Button_登入.State = false;
            this.plC_RJ_Button_登入.TabIndex = 140;
            this.plC_RJ_Button_登入.Text = "登入";
            this.plC_RJ_Button_登入.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_登入.TextHeight = 0;
            this.plC_RJ_Button_登入.Texts = "登入";
            this.plC_RJ_Button_登入.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_登入.字型鎖住 = false;
            this.plC_RJ_Button_登入.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_登入.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_登入.文字鎖住 = false;
            this.plC_RJ_Button_登入.背景圖片 = null;
            this.plC_RJ_Button_登入.讀取位元反向 = false;
            this.plC_RJ_Button_登入.讀寫鎖住 = false;
            this.plC_RJ_Button_登入.音效 = true;
            this.plC_RJ_Button_登入.顯示 = false;
            this.plC_RJ_Button_登入.顯示狀態 = false;
            // 
            // plC_Button_領
            // 
            this.plC_Button_領.AutoResetState = false;
            this.plC_Button_領.BackgroundColor = System.Drawing.Color.Silver;
            this.plC_Button_領.Bool = false;
            this.plC_Button_領.BorderColor = System.Drawing.Color.Yellow;
            this.plC_Button_領.BorderRadius = 20;
            this.plC_Button_領.BorderSize = 0;
            this.plC_Button_領.but_press = false;
            this.plC_Button_領.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_Button_領.DisenableColor = System.Drawing.Color.Gray;
            this.plC_Button_領.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_Button_領.FlatAppearance.BorderSize = 0;
            this.plC_Button_領.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_Button_領.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold);
            this.plC_Button_領.GUID = "";
            this.plC_Button_領.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_領.Image_padding = new System.Windows.Forms.Padding(0);
            this.plC_Button_領.Location = new System.Drawing.Point(661, 3);
            this.plC_Button_領.Name = "plC_Button_領";
            this.plC_Button_領.OFF_文字內容 = "領";
            this.plC_Button_領.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold);
            this.plC_Button_領.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_領.OFF_背景顏色 = System.Drawing.Color.Silver;
            this.plC_Button_領.ON_BorderSize = 0;
            this.plC_Button_領.ON_文字內容 = "領";
            this.plC_Button_領.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_Button_領.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_領.ON_背景顏色 = System.Drawing.Color.Green;
            this.plC_Button_領.ProhibitionBorderLineWidth = 1;
            this.plC_Button_領.ProhibitionLineWidth = 4;
            this.plC_Button_領.ProhibitionSymbolSize = 30;
            this.plC_Button_領.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_Button_領.ShadowSize = 3;
            this.plC_Button_領.ShowLoadingForm = false;
            this.plC_Button_領.Size = new System.Drawing.Size(74, 70);
            this.plC_Button_領.State = false;
            this.plC_Button_領.TabIndex = 138;
            this.plC_Button_領.Text = "領";
            this.plC_Button_領.TextColor = System.Drawing.Color.White;
            this.plC_Button_領.TextHeight = 0;
            this.plC_Button_領.UseVisualStyleBackColor = false;
            this.plC_Button_領.字型鎖住 = false;
            this.plC_Button_領.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.交替型;
            this.plC_Button_領.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_領.文字鎖住 = false;
            this.plC_Button_領.背景圖片 = null;
            this.plC_Button_領.讀取位元反向 = false;
            this.plC_Button_領.讀寫鎖住 = false;
            this.plC_Button_領.音效 = true;
            this.plC_Button_領.顯示 = false;
            this.plC_Button_領.顯示狀態 = false;
            // 
            // plC_Button_退
            // 
            this.plC_Button_退.AutoResetState = false;
            this.plC_Button_退.BackgroundColor = System.Drawing.Color.Silver;
            this.plC_Button_退.Bool = false;
            this.plC_Button_退.BorderColor = System.Drawing.Color.Yellow;
            this.plC_Button_退.BorderRadius = 20;
            this.plC_Button_退.BorderSize = 0;
            this.plC_Button_退.but_press = false;
            this.plC_Button_退.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_Button_退.DisenableColor = System.Drawing.Color.Gray;
            this.plC_Button_退.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_Button_退.FlatAppearance.BorderSize = 0;
            this.plC_Button_退.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_Button_退.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold);
            this.plC_Button_退.GUID = "";
            this.plC_Button_退.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_退.Image_padding = new System.Windows.Forms.Padding(0);
            this.plC_Button_退.Location = new System.Drawing.Point(735, 3);
            this.plC_Button_退.Name = "plC_Button_退";
            this.plC_Button_退.OFF_文字內容 = "退";
            this.plC_Button_退.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold);
            this.plC_Button_退.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_退.OFF_背景顏色 = System.Drawing.Color.Silver;
            this.plC_Button_退.ON_BorderSize = 0;
            this.plC_Button_退.ON_文字內容 = "退";
            this.plC_Button_退.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_Button_退.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_退.ON_背景顏色 = System.Drawing.Color.Red;
            this.plC_Button_退.ProhibitionBorderLineWidth = 1;
            this.plC_Button_退.ProhibitionLineWidth = 4;
            this.plC_Button_退.ProhibitionSymbolSize = 30;
            this.plC_Button_退.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_Button_退.ShadowSize = 3;
            this.plC_Button_退.ShowLoadingForm = false;
            this.plC_Button_退.Size = new System.Drawing.Size(74, 70);
            this.plC_Button_退.State = false;
            this.plC_Button_退.TabIndex = 139;
            this.plC_Button_退.Text = "退";
            this.plC_Button_退.TextColor = System.Drawing.Color.White;
            this.plC_Button_退.TextHeight = 0;
            this.plC_Button_退.UseVisualStyleBackColor = false;
            this.plC_Button_退.字型鎖住 = false;
            this.plC_Button_退.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.交替型;
            this.plC_Button_退.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_退.文字鎖住 = false;
            this.plC_Button_退.背景圖片 = null;
            this.plC_Button_退.讀取位元反向 = false;
            this.plC_Button_退.讀寫鎖住 = false;
            this.plC_Button_退.音效 = true;
            this.plC_Button_退.顯示 = false;
            this.plC_Button_退.顯示狀態 = false;
            // 
            // plC_RJ_Button_取消作業
            // 
            this.plC_RJ_Button_取消作業.AutoResetState = true;
            this.plC_RJ_Button_取消作業.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_取消作業.Bool = false;
            this.plC_RJ_Button_取消作業.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_取消作業.BorderRadius = 15;
            this.plC_RJ_Button_取消作業.BorderSize = 1;
            this.plC_RJ_Button_取消作業.but_press = false;
            this.plC_RJ_Button_取消作業.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_取消作業.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_取消作業.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_取消作業.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_取消作業.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_取消作業.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_取消作業.GUID = "";
            this.plC_RJ_Button_取消作業.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_取消作業.Image_padding = new System.Windows.Forms.Padding(5, 5, 10, 5);
            this.plC_RJ_Button_取消作業.Location = new System.Drawing.Point(809, 3);
            this.plC_RJ_Button_取消作業.Name = "plC_RJ_Button_取消作業";
            this.plC_RJ_Button_取消作業.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_取消作業.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_取消作業.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_取消作業.ON_BorderSize = 1;
            this.plC_RJ_Button_取消作業.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_取消作業.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_取消作業.ON_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_取消作業.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_取消作業.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_取消作業.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_取消作業.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_取消作業.ShadowSize = 3;
            this.plC_RJ_Button_取消作業.ShowLoadingForm = false;
            this.plC_RJ_Button_取消作業.Size = new System.Drawing.Size(68, 70);
            this.plC_RJ_Button_取消作業.State = false;
            this.plC_RJ_Button_取消作業.TabIndex = 137;
            this.plC_RJ_Button_取消作業.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_取消作業.TextHeight = 0;
            this.plC_RJ_Button_取消作業.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_取消作業.字型鎖住 = false;
            this.plC_RJ_Button_取消作業.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_取消作業.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_取消作業.文字鎖住 = false;
            this.plC_RJ_Button_取消作業.背景圖片 = global::調劑台管理系統.Properties.Resources.essential_set_close_512;
            this.plC_RJ_Button_取消作業.讀取位元反向 = false;
            this.plC_RJ_Button_取消作業.讀寫鎖住 = false;
            this.plC_RJ_Button_取消作業.音效 = true;
            this.plC_RJ_Button_取消作業.顯示 = false;
            this.plC_RJ_Button_取消作業.顯示狀態 = false;
            // 
            // panel_藥品資訊
            // 
            this.panel_藥品資訊.Controls.Add(this.panel_藥品圖片);
            this.panel_藥品資訊.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_藥品資訊.ForeColor = System.Drawing.Color.Black;
            this.panel_藥品資訊.Location = new System.Drawing.Point(0, 433);
            this.panel_藥品資訊.Name = "panel_藥品資訊";
            this.panel_藥品資訊.Padding = new System.Windows.Forms.Padding(3);
            this.panel_藥品資訊.Size = new System.Drawing.Size(880, 210);
            this.panel_藥品資訊.TabIndex = 134;
            // 
            // panel_藥品圖片
            // 
            this.panel_藥品圖片.Controls.Add(this.pictureBox_藥品圖片01);
            this.panel_藥品圖片.Controls.Add(this.pictureBox_藥品圖片02);
            this.panel_藥品圖片.Controls.Add(this.rJ_Lable_MedGPT_Title);
            this.panel_藥品圖片.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_藥品圖片.Location = new System.Drawing.Point(3, 3);
            this.panel_藥品圖片.Name = "panel_藥品圖片";
            this.panel_藥品圖片.Padding = new System.Windows.Forms.Padding(2);
            this.panel_藥品圖片.Size = new System.Drawing.Size(874, 204);
            this.panel_藥品圖片.TabIndex = 4;
            // 
            // pictureBox_藥品圖片01
            // 
            this.pictureBox_藥品圖片01.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_藥品圖片01.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_藥品圖片01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_藥品圖片01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_藥品圖片01.Location = new System.Drawing.Point(2, 42);
            this.pictureBox_藥品圖片01.Name = "pictureBox_藥品圖片01";
            this.pictureBox_藥品圖片01.Size = new System.Drawing.Size(438, 160);
            this.pictureBox_藥品圖片01.TabIndex = 8;
            this.pictureBox_藥品圖片01.TabStop = false;
            // 
            // pictureBox_藥品圖片02
            // 
            this.pictureBox_藥品圖片02.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_藥品圖片02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_藥品圖片02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_藥品圖片02.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_藥品圖片02.Location = new System.Drawing.Point(440, 42);
            this.pictureBox_藥品圖片02.Name = "pictureBox_藥品圖片02";
            this.pictureBox_藥品圖片02.Size = new System.Drawing.Size(432, 160);
            this.pictureBox_藥品圖片02.TabIndex = 7;
            this.pictureBox_藥品圖片02.TabStop = false;
            // 
            // rJ_Lable_MedGPT_Title
            // 
            this.rJ_Lable_MedGPT_Title.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_MedGPT_Title.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.rJ_Lable_MedGPT_Title.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_MedGPT_Title.BorderRadius = 10;
            this.rJ_Lable_MedGPT_Title.BorderSize = 0;
            this.rJ_Lable_MedGPT_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable_MedGPT_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_MedGPT_Title.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_MedGPT_Title.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_MedGPT_Title.GUID = "";
            this.rJ_Lable_MedGPT_Title.Location = new System.Drawing.Point(2, 2);
            this.rJ_Lable_MedGPT_Title.Name = "rJ_Lable_MedGPT_Title";
            this.rJ_Lable_MedGPT_Title.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_MedGPT_Title.ShadowSize = 0;
            this.rJ_Lable_MedGPT_Title.Size = new System.Drawing.Size(870, 40);
            this.rJ_Lable_MedGPT_Title.TabIndex = 0;
            this.rJ_Lable_MedGPT_Title.Text = "MedGPT 處方核對";
            this.rJ_Lable_MedGPT_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_MedGPT_Title.TextColor = System.Drawing.Color.Black;
            this.rJ_Lable_MedGPT_Title.Visible = false;
            // 
            // panel_領藥台01_處方資訊
            // 
            this.panel_領藥台01_處方資訊.Controls.Add(this.panel_病患資訊);
            this.panel_領藥台01_處方資訊.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_領藥台01_處方資訊.Location = new System.Drawing.Point(0, 378);
            this.panel_領藥台01_處方資訊.Name = "panel_領藥台01_處方資訊";
            this.panel_領藥台01_處方資訊.Size = new System.Drawing.Size(880, 55);
            this.panel_領藥台01_處方資訊.TabIndex = 135;
            // 
            // panel_病患資訊
            // 
            this.panel_病患資訊.Controls.Add(this.rJ_Lable_領藥號);
            this.panel_病患資訊.Controls.Add(this.rJ_Lable2);
            this.panel_病患資訊.Controls.Add(this.rJ_Lable_病歷號);
            this.panel_病患資訊.Controls.Add(this.rJ_Lable4);
            this.panel_病患資訊.Controls.Add(this.rJ_Lable_年齡);
            this.panel_病患資訊.Controls.Add(this.rJ_Lable6);
            this.panel_病患資訊.Controls.Add(this.rJ_Lable_姓名);
            this.panel_病患資訊.Controls.Add(this.rJ_Lable8);
            this.panel_病患資訊.Controls.Add(this.panel3);
            this.panel_病患資訊.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_病患資訊.Location = new System.Drawing.Point(0, 0);
            this.panel_病患資訊.Name = "panel_病患資訊";
            this.panel_病患資訊.Size = new System.Drawing.Size(880, 50);
            this.panel_病患資訊.TabIndex = 0;
            // 
            // rJ_Lable_領藥號
            // 
            this.rJ_Lable_領藥號.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_領藥號.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_領藥號.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_領藥號.BorderRadius = 10;
            this.rJ_Lable_領藥號.BorderSize = 0;
            this.rJ_Lable_領藥號.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_領藥號.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_領藥號.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_領藥號.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_領藥號.GUID = "";
            this.rJ_Lable_領藥號.Location = new System.Drawing.Point(714, 0);
            this.rJ_Lable_領藥號.Name = "rJ_Lable_領藥號";
            this.rJ_Lable_領藥號.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_領藥號.ShadowSize = 0;
            this.rJ_Lable_領藥號.Size = new System.Drawing.Size(101, 50);
            this.rJ_Lable_領藥號.TabIndex = 21;
            this.rJ_Lable_領藥號.Text = "-----";
            this.rJ_Lable_領藥號.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_領藥號.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable2
            // 
            this.rJ_Lable2.BackColor = System.Drawing.Color.White;
            this.rJ_Lable2.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable2.BorderRadius = 10;
            this.rJ_Lable2.BorderSize = 0;
            this.rJ_Lable2.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable2.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable2.GUID = "";
            this.rJ_Lable2.Location = new System.Drawing.Point(622, 0);
            this.rJ_Lable2.Name = "rJ_Lable2";
            this.rJ_Lable2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable2.ShadowSize = 0;
            this.rJ_Lable2.Size = new System.Drawing.Size(92, 50);
            this.rJ_Lable2.TabIndex = 20;
            this.rJ_Lable2.Text = "領藥號:";
            this.rJ_Lable2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable2.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_病歷號
            // 
            this.rJ_Lable_病歷號.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_病歷號.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_病歷號.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_病歷號.BorderRadius = 10;
            this.rJ_Lable_病歷號.BorderSize = 0;
            this.rJ_Lable_病歷號.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_病歷號.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_病歷號.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_病歷號.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_病歷號.GUID = "";
            this.rJ_Lable_病歷號.Location = new System.Drawing.Point(464, 0);
            this.rJ_Lable_病歷號.Name = "rJ_Lable_病歷號";
            this.rJ_Lable_病歷號.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_病歷號.ShadowSize = 0;
            this.rJ_Lable_病歷號.Size = new System.Drawing.Size(158, 50);
            this.rJ_Lable_病歷號.TabIndex = 19;
            this.rJ_Lable_病歷號.Text = "---------";
            this.rJ_Lable_病歷號.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_病歷號.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable4
            // 
            this.rJ_Lable4.BackColor = System.Drawing.Color.White;
            this.rJ_Lable4.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable4.BorderRadius = 10;
            this.rJ_Lable4.BorderSize = 0;
            this.rJ_Lable4.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable4.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable4.GUID = "";
            this.rJ_Lable4.Location = new System.Drawing.Point(372, 0);
            this.rJ_Lable4.Name = "rJ_Lable4";
            this.rJ_Lable4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable4.ShadowSize = 0;
            this.rJ_Lable4.Size = new System.Drawing.Size(92, 50);
            this.rJ_Lable4.TabIndex = 18;
            this.rJ_Lable4.Text = "病歷號:";
            this.rJ_Lable4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable4.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_年齡
            // 
            this.rJ_Lable_年齡.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_年齡.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_年齡.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_年齡.BorderRadius = 10;
            this.rJ_Lable_年齡.BorderSize = 0;
            this.rJ_Lable_年齡.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_年齡.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_年齡.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_年齡.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_年齡.GUID = "";
            this.rJ_Lable_年齡.Location = new System.Drawing.Point(313, 0);
            this.rJ_Lable_年齡.Name = "rJ_Lable_年齡";
            this.rJ_Lable_年齡.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_年齡.ShadowSize = 0;
            this.rJ_Lable_年齡.Size = new System.Drawing.Size(59, 50);
            this.rJ_Lable_年齡.TabIndex = 17;
            this.rJ_Lable_年齡.Text = "----";
            this.rJ_Lable_年齡.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_年齡.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable6
            // 
            this.rJ_Lable6.BackColor = System.Drawing.Color.White;
            this.rJ_Lable6.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable6.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable6.BorderRadius = 10;
            this.rJ_Lable6.BorderSize = 0;
            this.rJ_Lable6.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable6.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable6.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable6.GUID = "";
            this.rJ_Lable6.Location = new System.Drawing.Point(247, 0);
            this.rJ_Lable6.Name = "rJ_Lable6";
            this.rJ_Lable6.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable6.ShadowSize = 0;
            this.rJ_Lable6.Size = new System.Drawing.Size(66, 50);
            this.rJ_Lable6.TabIndex = 16;
            this.rJ_Lable6.Text = "年齡:";
            this.rJ_Lable6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable6.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_姓名
            // 
            this.rJ_Lable_姓名.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_姓名.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_姓名.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_姓名.BorderRadius = 10;
            this.rJ_Lable_姓名.BorderSize = 0;
            this.rJ_Lable_姓名.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_姓名.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_姓名.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_姓名.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_姓名.GUID = "";
            this.rJ_Lable_姓名.Location = new System.Drawing.Point(76, 0);
            this.rJ_Lable_姓名.Name = "rJ_Lable_姓名";
            this.rJ_Lable_姓名.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_姓名.ShadowSize = 0;
            this.rJ_Lable_姓名.Size = new System.Drawing.Size(171, 50);
            this.rJ_Lable_姓名.TabIndex = 15;
            this.rJ_Lable_姓名.Text = "---------";
            this.rJ_Lable_姓名.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_姓名.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable8
            // 
            this.rJ_Lable8.BackColor = System.Drawing.Color.White;
            this.rJ_Lable8.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable8.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable8.BorderRadius = 10;
            this.rJ_Lable8.BorderSize = 0;
            this.rJ_Lable8.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable8.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable8.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable8.GUID = "";
            this.rJ_Lable8.Location = new System.Drawing.Point(10, 0);
            this.rJ_Lable8.Name = "rJ_Lable8";
            this.rJ_Lable8.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable8.ShadowSize = 0;
            this.rJ_Lable8.Size = new System.Drawing.Size(66, 50);
            this.rJ_Lable8.TabIndex = 14;
            this.rJ_Lable8.Text = "姓名:";
            this.rJ_Lable8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable8.TextColor = System.Drawing.Color.Black;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 50);
            this.panel3.TabIndex = 0;
            // 
            // panel_診斷資訊
            // 
            this.panel_診斷資訊.Controls.Add(this.rJ_Lable_診斷);
            this.panel_診斷資訊.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_診斷資訊.Location = new System.Drawing.Point(0, 76);
            this.panel_診斷資訊.Name = "panel_診斷資訊";
            this.panel_診斷資訊.Size = new System.Drawing.Size(880, 100);
            this.panel_診斷資訊.TabIndex = 136;
            // 
            // rJ_Lable_診斷
            // 
            this.rJ_Lable_診斷.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_診斷.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_診斷.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_診斷.BorderRadius = 0;
            this.rJ_Lable_診斷.BorderSize = 2;
            this.rJ_Lable_診斷.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Lable_診斷.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_診斷.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_診斷.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_診斷.GUID = "";
            this.rJ_Lable_診斷.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable_診斷.Name = "rJ_Lable_診斷";
            this.rJ_Lable_診斷.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_診斷.ShadowSize = 0;
            this.rJ_Lable_診斷.Size = new System.Drawing.Size(880, 100);
            this.rJ_Lable_診斷.TabIndex = 10;
            this.rJ_Lable_診斷.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_診斷.TextColor = System.Drawing.Color.Black;
            // 
            // sqL_DataGridView_領藥內容
            // 
            this.sqL_DataGridView_領藥內容.AutoSelectToDeep = false;
            this.sqL_DataGridView_領藥內容.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_領藥內容.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_領藥內容.BorderRadius = 0;
            this.sqL_DataGridView_領藥內容.BorderSize = 0;
            this.sqL_DataGridView_領藥內容.CellBorderColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_領藥內容.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_領藥內容.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_領藥內容.cellStyleFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_領藥內容.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_領藥內容.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_領藥內容.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_領藥內容.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_領藥內容.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_領藥內容.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_領藥內容.columnHeadersHeight = 40;
            this.sqL_DataGridView_領藥內容.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_領藥內容.ContextMenuStrip = this.contextMenuStrip_調劑畫面;
            this.sqL_DataGridView_領藥內容.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_領藥內容.DataKeyEnable = false;
            this.sqL_DataGridView_領藥內容.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_領藥內容.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_領藥內容.ImageBox = false;
            this.sqL_DataGridView_領藥內容.Location = new System.Drawing.Point(0, 176);
            this.sqL_DataGridView_領藥內容.Name = "sqL_DataGridView_領藥內容";
            this.sqL_DataGridView_領藥內容.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_領藥內容.Password = "user82822040";
            this.sqL_DataGridView_領藥內容.Port = ((uint)(3306u));
            this.sqL_DataGridView_領藥內容.rowBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_領藥內容.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_領藥內容.rowHeaderBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_領藥內容.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_領藥內容.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_領藥內容.RowsHeight = 40;
            this.sqL_DataGridView_領藥內容.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_領藥內容.selectedBorderSize = 2;
            this.sqL_DataGridView_領藥內容.selectedRowBackColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_領藥內容.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_領藥內容.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_領藥內容.Server = "127.0.0.0";
            this.sqL_DataGridView_領藥內容.Size = new System.Drawing.Size(880, 202);
            this.sqL_DataGridView_領藥內容.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_領藥內容.TabIndex = 137;
            this.sqL_DataGridView_領藥內容.UserName = "root";
            this.sqL_DataGridView_領藥內容.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_領藥內容.可選擇多列 = false;
            this.sqL_DataGridView_領藥內容.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_領藥內容.自動換行 = true;
            this.sqL_DataGridView_領藥內容.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_領藥內容.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_領藥內容.顯示CheckBox = false;
            this.sqL_DataGridView_領藥內容.顯示首列 = false;
            this.sqL_DataGridView_領藥內容.顯示首行 = true;
            this.sqL_DataGridView_領藥內容.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_領藥內容.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // contextMenuStrip_調劑畫面
            // 
            this.contextMenuStrip_調劑畫面.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.contextMenuStrip_調劑畫面.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_調劑畫面_顯示設定});
            this.contextMenuStrip_調劑畫面.Name = "contextMenuStrip_Main";
            this.contextMenuStrip_調劑畫面.Size = new System.Drawing.Size(143, 28);
            // 
            // toolStripMenuItem_調劑畫面_顯示設定
            // 
            this.toolStripMenuItem_調劑畫面_顯示設定.Name = "toolStripMenuItem_調劑畫面_顯示設定";
            this.toolStripMenuItem_調劑畫面_顯示設定.Size = new System.Drawing.Size(142, 24);
            this.toolStripMenuItem_調劑畫面_顯示設定.Text = "顯示設定";
            // 
            // UC_調劑作業_TypeA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.sqL_DataGridView_領藥內容);
            this.Controls.Add(this.panel_診斷資訊);
            this.Controls.Add(this.panel_領藥台01_處方資訊);
            this.Controls.Add(this.panel_藥品資訊);
            this.Controls.Add(this.panel114);
            this.Name = "UC_調劑作業_TypeA";
            this.Size = new System.Drawing.Size(880, 643);
            this.panel114.ResumeLayout(false);
            this.panel121.ResumeLayout(false);
            this.panel119.ResumeLayout(false);
            this.panel_藥品資訊.ResumeLayout(false);
            this.panel_藥品圖片.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品圖片01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品圖片02)).EndInit();
            this.panel_領藥台01_處方資訊.ResumeLayout(false);
            this.panel_病患資訊.ResumeLayout(false);
            this.panel_診斷資訊.ResumeLayout(false);
            this.contextMenuStrip_調劑畫面.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel114;
        private System.Windows.Forms.Panel panel121;
        private System.Windows.Forms.Panel panel119;
        private System.Windows.Forms.Panel panel_領藥台01_處方資訊;
        private System.Windows.Forms.Panel panel_病患資訊;
        private System.Windows.Forms.Panel panel_診斷資訊;
        private MyUI.RJ_Lable rJ_Lable2;
        private MyUI.RJ_Lable rJ_Lable4;
        private MyUI.RJ_Lable rJ_Lable6;
        private MyUI.RJ_Lable rJ_Lable8;
        private System.Windows.Forms.Panel panel3;
        public MyUI.RJ_TextBox textBox_帳號;
        public MyUI.RJ_TextBox textBox_密碼;
        public MyUI.PLC_RJ_Button plC_RJ_Button_登入;
        public MyUI.PLC_RJ_Button plC_Button_領;
        public MyUI.PLC_RJ_Button plC_Button_退;
        public MyUI.PLC_RJ_Button plC_RJ_Button_取消作業;
        public MyUI.RJ_Lable rJ_Lable_領藥號;
        public MyUI.RJ_Lable rJ_Lable_病歷號;
        public MyUI.RJ_Lable rJ_Lable_年齡;
        public MyUI.RJ_Lable rJ_Lable_姓名;
        public MyUI.RJ_Lable rJ_Lable_診斷;
        public SQLUI.SQL_DataGridView sqL_DataGridView_領藥內容;
        public System.Windows.Forms.Panel panel_藥品資訊;
        public System.Windows.Forms.Panel panel_藥品圖片;
        public MyUI.RJ_Lable rJ_Lable_Title;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_調劑畫面;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_調劑畫面_顯示設定;
        public System.Windows.Forms.PictureBox pictureBox_藥品圖片01;
        public System.Windows.Forms.PictureBox pictureBox_藥品圖片02;
        private MyUI.RJ_Lable rJ_Lable_MedGPT_Title;
    }
}
