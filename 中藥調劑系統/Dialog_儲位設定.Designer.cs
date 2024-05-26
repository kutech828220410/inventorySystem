
namespace 中藥調劑系統
{
    partial class Dialog_儲位設定
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rowsLED_Pannel1 = new H_Pannel_lib.RowsLED_Pannel();
            this.rJ_Pannel3 = new MyUI.RJ_Pannel();
            this.rJ_Pannel5 = new MyUI.RJ_Pannel();
            this.sqL_DataGridView_藥品資料 = new SQLUI.SQL_DataGridView();
            this.rJ_Pannel4 = new MyUI.RJ_Pannel();
            this.comboBox_藥品資料_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.rJ_Button_藥品資料_搜尋 = new MyUI.RJ_Button();
            this.textBox_處方搜尋_搜尋內容 = new MyUI.RJ_TextBox();
            this.rJ_Pannel2 = new MyUI.RJ_Pannel();
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.sqL_DataGridView_層架列表 = new SQLUI.SQL_DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.rJ_Pannel3.SuspendLayout();
            this.rJ_Pannel5.SuspendLayout();
            this.rJ_Pannel4.SuspendLayout();
            this.rJ_Pannel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1272, 10);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1272, 858);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rowsLED_Pannel1);
            this.tabPage1.Controls.Add(this.rJ_Pannel3);
            this.tabPage1.Controls.Add(this.rJ_Pannel2);
            this.tabPage1.Controls.Add(this.rJ_Pannel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1264, 832);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "層架燈";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rowsLED_Pannel1
            // 
            this.rowsLED_Pannel1.AutoWrite = true;
            this.rowsLED_Pannel1.BackColor = System.Drawing.SystemColors.Window;
            this.rowsLED_Pannel1.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.rowsLED_Pannel1.BarFont = new System.Drawing.Font("微軟正黑體", 9F);
            this.rowsLED_Pannel1.BarForeColor = System.Drawing.Color.DarkGray;
            this.rowsLED_Pannel1.BarSize = 50;
            this.rowsLED_Pannel1.BottomSliderColor = System.Drawing.Color.Red;
            this.rowsLED_Pannel1.CurrentRowsDevice = null;
            this.rowsLED_Pannel1.CurrentRowsLED = null;
            this.rowsLED_Pannel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rowsLED_Pannel1.Location = new System.Drawing.Point(321, 361);
            this.rowsLED_Pannel1.Maximum = 100;
            this.rowsLED_Pannel1.MaxValue = 0;
            this.rowsLED_Pannel1.Minimum = 0;
            this.rowsLED_Pannel1.MinValue = 0;
            this.rowsLED_Pannel1.Name = "rowsLED_Pannel1";
            this.rowsLED_Pannel1.RowsDeviceGUID = "";
            this.rowsLED_Pannel1.Size = new System.Drawing.Size(943, 113);
            this.rowsLED_Pannel1.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rowsLED_Pannel1.SliderSize = 10;
            this.rowsLED_Pannel1.TabIndex = 4;
            this.rowsLED_Pannel1.TopSliderColor = System.Drawing.Color.Silver;
            // 
            // rJ_Pannel3
            // 
            this.rJ_Pannel3.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel3.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel3.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel3.BorderRadius = 2;
            this.rJ_Pannel3.BorderSize = 1;
            this.rJ_Pannel3.Controls.Add(this.rJ_Pannel5);
            this.rJ_Pannel3.Controls.Add(this.rJ_Pannel4);
            this.rJ_Pannel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Pannel3.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel3.IsSelected = false;
            this.rJ_Pannel3.Location = new System.Drawing.Point(321, 50);
            this.rJ_Pannel3.Name = "rJ_Pannel3";
            this.rJ_Pannel3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel3.ShadowSize = 0;
            this.rJ_Pannel3.Size = new System.Drawing.Size(943, 311);
            this.rJ_Pannel3.TabIndex = 3;
            // 
            // rJ_Pannel5
            // 
            this.rJ_Pannel5.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel5.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel5.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel5.BorderRadius = 2;
            this.rJ_Pannel5.BorderSize = 1;
            this.rJ_Pannel5.Controls.Add(this.sqL_DataGridView_藥品資料);
            this.rJ_Pannel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Pannel5.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel5.IsSelected = false;
            this.rJ_Pannel5.Location = new System.Drawing.Point(0, 0);
            this.rJ_Pannel5.Name = "rJ_Pannel5";
            this.rJ_Pannel5.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel5.ShadowSize = 0;
            this.rJ_Pannel5.Size = new System.Drawing.Size(943, 241);
            this.rJ_Pannel5.TabIndex = 5;
            // 
            // sqL_DataGridView_藥品資料
            // 
            this.sqL_DataGridView_藥品資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥品資料.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.BorderRadius = 0;
            this.sqL_DataGridView_藥品資料.BorderSize = 2;
            this.sqL_DataGridView_藥品資料.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_藥品資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_藥品資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品資料.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_藥品資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥品資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品資料.columnHeadersHeight = 18;
            this.sqL_DataGridView_藥品資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_藥品資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_藥品資料.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_藥品資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品資料.ImageBox = false;
            this.sqL_DataGridView_藥品資料.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_藥品資料.Name = "sqL_DataGridView_藥品資料";
            this.sqL_DataGridView_藥品資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥品資料.Password = "user82822040";
            this.sqL_DataGridView_藥品資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥品資料.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品資料.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品資料.RowsHeight = 20;
            this.sqL_DataGridView_藥品資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥品資料.selectedBorderSize = 0;
            this.sqL_DataGridView_藥品資料.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品資料.Server = "127.0.0.0";
            this.sqL_DataGridView_藥品資料.Size = new System.Drawing.Size(943, 241);
            this.sqL_DataGridView_藥品資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品資料.TabIndex = 3;
            this.sqL_DataGridView_藥品資料.UserName = "root";
            this.sqL_DataGridView_藥品資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_藥品資料.可選擇多列 = false;
            this.sqL_DataGridView_藥品資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_藥品資料.自動換行 = true;
            this.sqL_DataGridView_藥品資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品資料.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_藥品資料.顯示CheckBox = false;
            this.sqL_DataGridView_藥品資料.顯示首列 = true;
            this.sqL_DataGridView_藥品資料.顯示首行 = true;
            this.sqL_DataGridView_藥品資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // rJ_Pannel4
            // 
            this.rJ_Pannel4.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel4.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel4.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel4.BorderRadius = 2;
            this.rJ_Pannel4.BorderSize = 1;
            this.rJ_Pannel4.Controls.Add(this.comboBox_藥品資料_搜尋條件);
            this.rJ_Pannel4.Controls.Add(this.rJ_Button_藥品資料_搜尋);
            this.rJ_Pannel4.Controls.Add(this.textBox_處方搜尋_搜尋內容);
            this.rJ_Pannel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rJ_Pannel4.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel4.IsSelected = false;
            this.rJ_Pannel4.Location = new System.Drawing.Point(0, 241);
            this.rJ_Pannel4.Name = "rJ_Pannel4";
            this.rJ_Pannel4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel4.ShadowSize = 0;
            this.rJ_Pannel4.Size = new System.Drawing.Size(943, 70);
            this.rJ_Pannel4.TabIndex = 4;
            // 
            // comboBox_藥品資料_搜尋條件
            // 
            this.comboBox_藥品資料_搜尋條件.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_藥品資料_搜尋條件.FormattingEnabled = true;
            this.comboBox_藥品資料_搜尋條件.Items.AddRange(new object[] {
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox_藥品資料_搜尋條件.Location = new System.Drawing.Point(433, 17);
            this.comboBox_藥品資料_搜尋條件.Name = "comboBox_藥品資料_搜尋條件";
            this.comboBox_藥品資料_搜尋條件.Size = new System.Drawing.Size(121, 39);
            this.comboBox_藥品資料_搜尋條件.TabIndex = 32;
            // 
            // rJ_Button_藥品資料_搜尋
            // 
            this.rJ_Button_藥品資料_搜尋.AutoResetState = false;
            this.rJ_Button_藥品資料_搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品資料_搜尋.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品資料_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品資料_搜尋.BorderRadius = 20;
            this.rJ_Button_藥品資料_搜尋.BorderSize = 0;
            this.rJ_Button_藥品資料_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品資料_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品資料_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品資料_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品資料_搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥品資料_搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_搜尋.GUID = "";
            this.rJ_Button_藥品資料_搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品資料_搜尋.Location = new System.Drawing.Point(806, 5);
            this.rJ_Button_藥品資料_搜尋.Name = "rJ_Button_藥品資料_搜尋";
            this.rJ_Button_藥品資料_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品資料_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品資料_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品資料_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品資料_搜尋.ShadowSize = 3;
            this.rJ_Button_藥品資料_搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥品資料_搜尋.Size = new System.Drawing.Size(135, 63);
            this.rJ_Button_藥品資料_搜尋.State = false;
            this.rJ_Button_藥品資料_搜尋.TabIndex = 31;
            this.rJ_Button_藥品資料_搜尋.Text = "搜尋";
            this.rJ_Button_藥品資料_搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_搜尋.TextHeight = 0;
            this.rJ_Button_藥品資料_搜尋.UseVisualStyleBackColor = false;
            // 
            // textBox_處方搜尋_搜尋內容
            // 
            this.textBox_處方搜尋_搜尋內容.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_處方搜尋_搜尋內容.BorderColor = System.Drawing.Color.Black;
            this.textBox_處方搜尋_搜尋內容.BorderFocusColor = System.Drawing.Color.HotPink;
            this.textBox_處方搜尋_搜尋內容.BorderRadius = 0;
            this.textBox_處方搜尋_搜尋內容.BorderSize = 2;
            this.textBox_處方搜尋_搜尋內容.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_處方搜尋_搜尋內容.ForeColor = System.Drawing.Color.DimGray;
            this.textBox_處方搜尋_搜尋內容.GUID = "";
            this.textBox_處方搜尋_搜尋內容.Location = new System.Drawing.Point(569, 18);
            this.textBox_處方搜尋_搜尋內容.Multiline = false;
            this.textBox_處方搜尋_搜尋內容.Name = "textBox_處方搜尋_搜尋內容";
            this.textBox_處方搜尋_搜尋內容.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.textBox_處方搜尋_搜尋內容.PassWordChar = false;
            this.textBox_處方搜尋_搜尋內容.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.textBox_處方搜尋_搜尋內容.PlaceholderText = "";
            this.textBox_處方搜尋_搜尋內容.ShowTouchPannel = false;
            this.textBox_處方搜尋_搜尋內容.Size = new System.Drawing.Size(227, 37);
            this.textBox_處方搜尋_搜尋內容.TabIndex = 30;
            this.textBox_處方搜尋_搜尋內容.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox_處方搜尋_搜尋內容.Texts = "";
            this.textBox_處方搜尋_搜尋內容.UnderlineStyle = false;
            // 
            // rJ_Pannel2
            // 
            this.rJ_Pannel2.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel2.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel2.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel2.BorderRadius = 2;
            this.rJ_Pannel2.BorderSize = 1;
            this.rJ_Pannel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Pannel2.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel2.IsSelected = false;
            this.rJ_Pannel2.Location = new System.Drawing.Point(321, 0);
            this.rJ_Pannel2.Name = "rJ_Pannel2";
            this.rJ_Pannel2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel2.ShadowSize = 0;
            this.rJ_Pannel2.Size = new System.Drawing.Size(943, 50);
            this.rJ_Pannel2.TabIndex = 2;
            // 
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.BorderRadius = 2;
            this.rJ_Pannel1.BorderSize = 1;
            this.rJ_Pannel1.Controls.Add(this.sqL_DataGridView_層架列表);
            this.rJ_Pannel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(0, 0);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 0;
            this.rJ_Pannel1.Size = new System.Drawing.Size(321, 832);
            this.rJ_Pannel1.TabIndex = 1;
            // 
            // sqL_DataGridView_層架列表
            // 
            this.sqL_DataGridView_層架列表.AutoSelectToDeep = false;
            this.sqL_DataGridView_層架列表.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_層架列表.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_層架列表.BorderRadius = 0;
            this.sqL_DataGridView_層架列表.BorderSize = 2;
            this.sqL_DataGridView_層架列表.CellBorderColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_層架列表.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_層架列表.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_層架列表.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_層架列表.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_層架列表.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_層架列表.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_層架列表.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_層架列表.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_層架列表.columnHeadersHeight = 18;
            this.sqL_DataGridView_層架列表.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_層架列表.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_層架列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_層架列表.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_層架列表.ImageBox = false;
            this.sqL_DataGridView_層架列表.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_層架列表.Name = "sqL_DataGridView_層架列表";
            this.sqL_DataGridView_層架列表.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_層架列表.Password = "user82822040";
            this.sqL_DataGridView_層架列表.Port = ((uint)(3306u));
            this.sqL_DataGridView_層架列表.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_層架列表.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_層架列表.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_層架列表.RowsHeight = 20;
            this.sqL_DataGridView_層架列表.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_層架列表.selectedBorderSize = 0;
            this.sqL_DataGridView_層架列表.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_層架列表.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_層架列表.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_層架列表.Server = "127.0.0.0";
            this.sqL_DataGridView_層架列表.Size = new System.Drawing.Size(321, 832);
            this.sqL_DataGridView_層架列表.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_層架列表.TabIndex = 2;
            this.sqL_DataGridView_層架列表.UserName = "root";
            this.sqL_DataGridView_層架列表.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_層架列表.可選擇多列 = false;
            this.sqL_DataGridView_層架列表.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_層架列表.自動換行 = true;
            this.sqL_DataGridView_層架列表.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_層架列表.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_層架列表.顯示CheckBox = false;
            this.sqL_DataGridView_層架列表.顯示首列 = false;
            this.sqL_DataGridView_層架列表.顯示首行 = true;
            this.sqL_DataGridView_層架列表.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_層架列表.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1264, 832);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "電子紙";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Dialog_儲位設定
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 900);
            this.ControlBox = true;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_儲位設定";
            this.Text = "儲位設定";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.rJ_Pannel3.ResumeLayout(false);
            this.rJ_Pannel5.ResumeLayout(false);
            this.rJ_Pannel4.ResumeLayout(false);
            this.rJ_Pannel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MyUI.RJ_Pannel rJ_Pannel1;
        private SQLUI.SQL_DataGridView sqL_DataGridView_層架列表;
        private MyUI.RJ_Pannel rJ_Pannel3;
        private MyUI.RJ_Pannel rJ_Pannel5;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品資料;
        private MyUI.RJ_Pannel rJ_Pannel4;
        private MyUI.RJ_Pannel rJ_Pannel2;
        private H_Pannel_lib.RowsLED_Pannel rowsLED_Pannel1;
        private MyUI.RJ_Button rJ_Button_藥品資料_搜尋;
        private MyUI.RJ_TextBox textBox_處方搜尋_搜尋內容;
        private System.Windows.Forms.ComboBox comboBox_藥品資料_搜尋條件;
    }
}