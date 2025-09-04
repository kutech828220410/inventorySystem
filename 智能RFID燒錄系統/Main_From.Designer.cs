namespace 智能RFID燒錄系統
{
    partial class Main_From
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_From));
            this.comboBox_Comport = new System.Windows.Forms.ComboBox();
            this.rJ_Button_Connect = new MyUI.RJ_Button();
            this.rJ_Lable4 = new MyUI.RJ_Lable();
            this.rJ_Pannel2 = new MyUI.RJ_Pannel();
            this.rJ_Lable_標籤數量 = new MyUI.RJ_Lable();
            this.rJ_Lable_device_info = new MyUI.RJ_Lable();
            this.rJ_Pannel3 = new MyUI.RJ_Pannel();
            this.rJ_Button_藥品資料_搜尋 = new MyUI.RJ_Button();
            this.rJ_TextBox_藥品資料_搜尋內容 = new MyUI.RJ_TextBox();
            this.comboBox_藥品資料_搜尋方式 = new System.Windows.Forms.ComboBox();
            this.sqL_DataGridView_藥品資料 = new SQLUI.SQL_DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rJ_Button_Write = new MyUI.RJ_Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_數量 = new MyUI.RJ_TextBox();
            this.rJ_Lable6 = new MyUI.RJ_Lable();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_批號 = new MyUI.RJ_TextBox();
            this.rJ_Lable5 = new MyUI.RJ_Lable();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_DatePicker_效期 = new MyUI.RJ_DatePicker();
            this.rJ_Lable3 = new MyUI.RJ_Lable();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_藥名 = new MyUI.RJ_TextBox();
            this.rJ_Lable2 = new MyUI.RJ_Lable();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_藥碼 = new MyUI.RJ_TextBox();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.sqL_DataGridView_TagList = new SQLUI.SQL_DataGridView();
            this.comboBox_燒錄位置 = new System.Windows.Forms.ComboBox();
            this.rJ_Lable7 = new MyUI.RJ_Lable();
            this.rJ_Pannel2.SuspendLayout();
            this.rJ_Pannel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.rJ_Pannel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Comport
            // 
            this.comboBox_Comport.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_Comport.FormattingEnabled = true;
            this.comboBox_Comport.Location = new System.Drawing.Point(70, 34);
            this.comboBox_Comport.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_Comport.Name = "comboBox_Comport";
            this.comboBox_Comport.Size = new System.Drawing.Size(103, 32);
            this.comboBox_Comport.TabIndex = 0;
            // 
            // rJ_Button_Connect
            // 
            this.rJ_Button_Connect.AutoResetState = false;
            this.rJ_Button_Connect.BackColor = System.Drawing.Color.White;
            this.rJ_Button_Connect.BackgroundColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Connect.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_Connect.BorderRadius = 22;
            this.rJ_Button_Connect.BorderSize = 0;
            this.rJ_Button_Connect.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_Connect.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_Connect.FlatAppearance.BorderSize = 0;
            this.rJ_Button_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rJ_Button_Connect.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_Connect.GUID = "";
            this.rJ_Button_Connect.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_Connect.Location = new System.Drawing.Point(184, 25);
            this.rJ_Button_Connect.Name = "rJ_Button_Connect";
            this.rJ_Button_Connect.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_Connect.ProhibitionLineWidth = 4;
            this.rJ_Button_Connect.ProhibitionSymbolSize = 30;
            this.rJ_Button_Connect.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Connect.ShadowSize = 0;
            this.rJ_Button_Connect.ShowLoadingForm = false;
            this.rJ_Button_Connect.Size = new System.Drawing.Size(97, 48);
            this.rJ_Button_Connect.State = false;
            this.rJ_Button_Connect.TabIndex = 160;
            this.rJ_Button_Connect.Text = "Connect";
            this.rJ_Button_Connect.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Connect.TextHeight = 0;
            this.rJ_Button_Connect.UseVisualStyleBackColor = false;
            // 
            // rJ_Lable4
            // 
            this.rJ_Lable4.BackColor = System.Drawing.Color.White;
            this.rJ_Lable4.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable4.BorderRadius = 5;
            this.rJ_Lable4.BorderSize = 0;
            this.rJ_Lable4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable4.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable4.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable4.GUID = "";
            this.rJ_Lable4.Location = new System.Drawing.Point(13, 13);
            this.rJ_Lable4.Name = "rJ_Lable4";
            this.rJ_Lable4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable4.ShadowSize = 0;
            this.rJ_Lable4.Size = new System.Drawing.Size(49, 71);
            this.rJ_Lable4.TabIndex = 161;
            this.rJ_Lable4.Text = "Port";
            this.rJ_Lable4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable4.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Pannel2
            // 
            this.rJ_Pannel2.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel2.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel2.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel2.BorderRadius = 10;
            this.rJ_Pannel2.BorderSize = 2;
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable7);
            this.rJ_Pannel2.Controls.Add(this.comboBox_燒錄位置);
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable_標籤數量);
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable_device_info);
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable4);
            this.rJ_Pannel2.Controls.Add(this.comboBox_Comport);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_Connect);
            this.rJ_Pannel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Pannel2.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel2.IsSelected = false;
            this.rJ_Pannel2.Location = new System.Drawing.Point(14, 54);
            this.rJ_Pannel2.Name = "rJ_Pannel2";
            this.rJ_Pannel2.Padding = new System.Windows.Forms.Padding(10);
            this.rJ_Pannel2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel2.ShadowSize = 3;
            this.rJ_Pannel2.Size = new System.Drawing.Size(1572, 97);
            this.rJ_Pannel2.TabIndex = 162;
            // 
            // rJ_Lable_標籤數量
            // 
            this.rJ_Lable_標籤數量.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_標籤數量.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_標籤數量.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_標籤數量.BorderRadius = 5;
            this.rJ_Lable_標籤數量.BorderSize = 0;
            this.rJ_Lable_標籤數量.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Lable_標籤數量.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_標籤數量.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_標籤數量.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_標籤數量.GUID = "";
            this.rJ_Lable_標籤數量.Location = new System.Drawing.Point(1405, 10);
            this.rJ_Lable_標籤數量.Name = "rJ_Lable_標籤數量";
            this.rJ_Lable_標籤數量.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_標籤數量.ShadowSize = 0;
            this.rJ_Lable_標籤數量.Size = new System.Drawing.Size(157, 77);
            this.rJ_Lable_標籤數量.TabIndex = 163;
            this.rJ_Lable_標籤數量.Text = "標籤數量 : --";
            this.rJ_Lable_標籤數量.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_標籤數量.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_device_info
            // 
            this.rJ_Lable_device_info.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_device_info.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_device_info.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_device_info.BorderRadius = 5;
            this.rJ_Lable_device_info.BorderSize = 0;
            this.rJ_Lable_device_info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_device_info.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_device_info.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_device_info.GUID = "";
            this.rJ_Lable_device_info.Location = new System.Drawing.Point(309, 13);
            this.rJ_Lable_device_info.Name = "rJ_Lable_device_info";
            this.rJ_Lable_device_info.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_device_info.ShadowSize = 0;
            this.rJ_Lable_device_info.Size = new System.Drawing.Size(722, 71);
            this.rJ_Lable_device_info.TabIndex = 162;
            this.rJ_Lable_device_info.Text = "Device info : -------------";
            this.rJ_Lable_device_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_device_info.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Pannel3
            // 
            this.rJ_Pannel3.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel3.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel3.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel3.BorderRadius = 10;
            this.rJ_Pannel3.BorderSize = 2;
            this.rJ_Pannel3.Controls.Add(this.rJ_Button_藥品資料_搜尋);
            this.rJ_Pannel3.Controls.Add(this.rJ_TextBox_藥品資料_搜尋內容);
            this.rJ_Pannel3.Controls.Add(this.comboBox_藥品資料_搜尋方式);
            this.rJ_Pannel3.Controls.Add(this.sqL_DataGridView_藥品資料);
            this.rJ_Pannel3.Controls.Add(this.panel1);
            this.rJ_Pannel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Pannel3.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel3.IsSelected = false;
            this.rJ_Pannel3.Location = new System.Drawing.Point(1004, 151);
            this.rJ_Pannel3.Name = "rJ_Pannel3";
            this.rJ_Pannel3.Padding = new System.Windows.Forms.Padding(10, 10, 11, 11);
            this.rJ_Pannel3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel3.ShadowSize = 3;
            this.rJ_Pannel3.Size = new System.Drawing.Size(582, 648);
            this.rJ_Pannel3.TabIndex = 165;
            // 
            // rJ_Button_藥品資料_搜尋
            // 
            this.rJ_Button_藥品資料_搜尋.AutoResetState = false;
            this.rJ_Button_藥品資料_搜尋.BackColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_搜尋.BackgroundColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品資料_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品資料_搜尋.BorderRadius = 22;
            this.rJ_Button_藥品資料_搜尋.BorderSize = 0;
            this.rJ_Button_藥品資料_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品資料_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品資料_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品資料_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品資料_搜尋.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rJ_Button_藥品資料_搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_搜尋.GUID = "";
            this.rJ_Button_藥品資料_搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品資料_搜尋.Location = new System.Drawing.Point(366, 322);
            this.rJ_Button_藥品資料_搜尋.Name = "rJ_Button_藥品資料_搜尋";
            this.rJ_Button_藥品資料_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品資料_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品資料_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品資料_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品資料_搜尋.ShadowSize = 0;
            this.rJ_Button_藥品資料_搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥品資料_搜尋.Size = new System.Drawing.Size(97, 48);
            this.rJ_Button_藥品資料_搜尋.State = false;
            this.rJ_Button_藥品資料_搜尋.TabIndex = 175;
            this.rJ_Button_藥品資料_搜尋.Text = "搜尋";
            this.rJ_Button_藥品資料_搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_搜尋.TextHeight = 0;
            this.rJ_Button_藥品資料_搜尋.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_藥品資料_搜尋內容
            // 
            this.rJ_TextBox_藥品資料_搜尋內容.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥品資料_搜尋內容.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥品資料_搜尋內容.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥品資料_搜尋內容.BorderRadius = 0;
            this.rJ_TextBox_藥品資料_搜尋內容.BorderSize = 0;
            this.rJ_TextBox_藥品資料_搜尋內容.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥品資料_搜尋內容.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥品資料_搜尋內容.GUID = "";
            this.rJ_TextBox_藥品資料_搜尋內容.Location = new System.Drawing.Point(136, 330);
            this.rJ_TextBox_藥品資料_搜尋內容.Margin = new System.Windows.Forms.Padding(2);
            this.rJ_TextBox_藥品資料_搜尋內容.Multiline = false;
            this.rJ_TextBox_藥品資料_搜尋內容.Name = "rJ_TextBox_藥品資料_搜尋內容";
            this.rJ_TextBox_藥品資料_搜尋內容.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.rJ_TextBox_藥品資料_搜尋內容.PassWordChar = false;
            this.rJ_TextBox_藥品資料_搜尋內容.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品資料_搜尋內容.PlaceholderText = "";
            this.rJ_TextBox_藥品資料_搜尋內容.ShowTouchPannel = false;
            this.rJ_TextBox_藥品資料_搜尋內容.Size = new System.Drawing.Size(225, 32);
            this.rJ_TextBox_藥品資料_搜尋內容.TabIndex = 173;
            this.rJ_TextBox_藥品資料_搜尋內容.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥品資料_搜尋內容.Texts = "";
            this.rJ_TextBox_藥品資料_搜尋內容.UnderlineStyle = false;
            // 
            // comboBox_藥品資料_搜尋方式
            // 
            this.comboBox_藥品資料_搜尋方式.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_藥品資料_搜尋方式.FormattingEnabled = true;
            this.comboBox_藥品資料_搜尋方式.Items.AddRange(new object[] {
            "藥碼",
            "藥名"});
            this.comboBox_藥品資料_搜尋方式.Location = new System.Drawing.Point(10, 333);
            this.comboBox_藥品資料_搜尋方式.Name = "comboBox_藥品資料_搜尋方式";
            this.comboBox_藥品資料_搜尋方式.Size = new System.Drawing.Size(121, 28);
            this.comboBox_藥品資料_搜尋方式.TabIndex = 172;
            // 
            // sqL_DataGridView_藥品資料
            // 
            this.sqL_DataGridView_藥品資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥品資料.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_藥品資料.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_藥品資料.BorderRadius = 0;
            this.sqL_DataGridView_藥品資料.BorderSize = 0;
            this.sqL_DataGridView_藥品資料.CellBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_藥品資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品資料.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_藥品資料.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥品資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥品資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.columnHeadersHeight = 40;
            this.sqL_DataGridView_藥品資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_藥品資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sqL_DataGridView_藥品資料.DataKeyEnable = false;
            this.sqL_DataGridView_藥品資料.Dock = System.Windows.Forms.DockStyle.Top;
            this.sqL_DataGridView_藥品資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品資料.ImageBox = false;
            this.sqL_DataGridView_藥品資料.Location = new System.Drawing.Point(10, 10);
            this.sqL_DataGridView_藥品資料.Margin = new System.Windows.Forms.Padding(4);
            this.sqL_DataGridView_藥品資料.Name = "sqL_DataGridView_藥品資料";
            this.sqL_DataGridView_藥品資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥品資料.Password = "user82822040";
            this.sqL_DataGridView_藥品資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥品資料.rowBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_藥品資料.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_藥品資料.rowHeaderBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_藥品資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_藥品資料.RowsHeight = 40;
            this.sqL_DataGridView_藥品資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥品資料.selectedBorderSize = 2;
            this.sqL_DataGridView_藥品資料.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品資料.Server = "127.0.0.0";
            this.sqL_DataGridView_藥品資料.Size = new System.Drawing.Size(561, 305);
            this.sqL_DataGridView_藥品資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品資料.TabIndex = 171;
            this.sqL_DataGridView_藥品資料.UserName = "root";
            this.sqL_DataGridView_藥品資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_藥品資料.可選擇多列 = false;
            this.sqL_DataGridView_藥品資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品資料.自動換行 = true;
            this.sqL_DataGridView_藥品資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品資料.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_藥品資料.顯示CheckBox = false;
            this.sqL_DataGridView_藥品資料.顯示首列 = true;
            this.sqL_DataGridView_藥品資料.顯示首行 = true;
            this.sqL_DataGridView_藥品資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_Button_Write);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 383);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 254);
            this.panel1.TabIndex = 170;
            // 
            // rJ_Button_Write
            // 
            this.rJ_Button_Write.AutoResetState = false;
            this.rJ_Button_Write.BackColor = System.Drawing.Color.White;
            this.rJ_Button_Write.BackgroundColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Write.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_Write.BorderRadius = 22;
            this.rJ_Button_Write.BorderSize = 0;
            this.rJ_Button_Write.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_Write.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_Write.FlatAppearance.BorderSize = 0;
            this.rJ_Button_Write.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_Write.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rJ_Button_Write.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_Write.GUID = "";
            this.rJ_Button_Write.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_Write.Location = new System.Drawing.Point(448, 188);
            this.rJ_Button_Write.Name = "rJ_Button_Write";
            this.rJ_Button_Write.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_Write.ProhibitionLineWidth = 4;
            this.rJ_Button_Write.ProhibitionSymbolSize = 30;
            this.rJ_Button_Write.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Write.ShadowSize = 0;
            this.rJ_Button_Write.ShowLoadingForm = false;
            this.rJ_Button_Write.Size = new System.Drawing.Size(97, 48);
            this.rJ_Button_Write.State = false;
            this.rJ_Button_Write.TabIndex = 174;
            this.rJ_Button_Write.Text = "Write";
            this.rJ_Button_Write.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Write.TextHeight = 0;
            this.rJ_Button_Write.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.rJ_TextBox_數量);
            this.panel5.Controls.Add(this.rJ_Lable6);
            this.panel5.Location = new System.Drawing.Point(2, 185);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(325, 53);
            this.panel5.TabIndex = 173;
            // 
            // rJ_TextBox_數量
            // 
            this.rJ_TextBox_數量.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_數量.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_數量.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_數量.BorderRadius = 0;
            this.rJ_TextBox_數量.BorderSize = 0;
            this.rJ_TextBox_數量.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_數量.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_數量.GUID = "";
            this.rJ_TextBox_數量.Location = new System.Drawing.Point(79, 11);
            this.rJ_TextBox_數量.Margin = new System.Windows.Forms.Padding(2);
            this.rJ_TextBox_數量.Multiline = false;
            this.rJ_TextBox_數量.Name = "rJ_TextBox_數量";
            this.rJ_TextBox_數量.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.rJ_TextBox_數量.PassWordChar = false;
            this.rJ_TextBox_數量.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_數量.PlaceholderText = "";
            this.rJ_TextBox_數量.ShowTouchPannel = false;
            this.rJ_TextBox_數量.Size = new System.Drawing.Size(183, 32);
            this.rJ_TextBox_數量.TabIndex = 163;
            this.rJ_TextBox_數量.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_數量.Texts = "";
            this.rJ_TextBox_數量.UnderlineStyle = false;
            // 
            // rJ_Lable6
            // 
            this.rJ_Lable6.BackColor = System.Drawing.Color.White;
            this.rJ_Lable6.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable6.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable6.BorderRadius = 5;
            this.rJ_Lable6.BorderSize = 0;
            this.rJ_Lable6.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable6.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable6.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable6.GUID = "";
            this.rJ_Lable6.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable6.Name = "rJ_Lable6";
            this.rJ_Lable6.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable6.ShadowSize = 0;
            this.rJ_Lable6.Size = new System.Drawing.Size(74, 51);
            this.rJ_Lable6.TabIndex = 162;
            this.rJ_Lable6.Text = "數量";
            this.rJ_Lable6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable6.TextColor = System.Drawing.Color.Black;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rJ_TextBox_批號);
            this.panel4.Controls.Add(this.rJ_Lable5);
            this.panel4.Location = new System.Drawing.Point(355, 128);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(204, 53);
            this.panel4.TabIndex = 172;
            // 
            // rJ_TextBox_批號
            // 
            this.rJ_TextBox_批號.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_批號.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_批號.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_批號.BorderRadius = 0;
            this.rJ_TextBox_批號.BorderSize = 0;
            this.rJ_TextBox_批號.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_批號.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_批號.GUID = "";
            this.rJ_TextBox_批號.Location = new System.Drawing.Point(57, 9);
            this.rJ_TextBox_批號.Margin = new System.Windows.Forms.Padding(2);
            this.rJ_TextBox_批號.Multiline = false;
            this.rJ_TextBox_批號.Name = "rJ_TextBox_批號";
            this.rJ_TextBox_批號.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.rJ_TextBox_批號.PassWordChar = false;
            this.rJ_TextBox_批號.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_批號.PlaceholderText = "";
            this.rJ_TextBox_批號.ShowTouchPannel = false;
            this.rJ_TextBox_批號.Size = new System.Drawing.Size(132, 32);
            this.rJ_TextBox_批號.TabIndex = 163;
            this.rJ_TextBox_批號.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_批號.Texts = "";
            this.rJ_TextBox_批號.UnderlineStyle = false;
            // 
            // rJ_Lable5
            // 
            this.rJ_Lable5.BackColor = System.Drawing.Color.White;
            this.rJ_Lable5.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable5.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable5.BorderRadius = 5;
            this.rJ_Lable5.BorderSize = 0;
            this.rJ_Lable5.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable5.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable5.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable5.GUID = "";
            this.rJ_Lable5.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable5.Name = "rJ_Lable5";
            this.rJ_Lable5.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable5.ShadowSize = 0;
            this.rJ_Lable5.Size = new System.Drawing.Size(74, 51);
            this.rJ_Lable5.TabIndex = 162;
            this.rJ_Lable5.Text = "批號";
            this.rJ_Lable5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable5.TextColor = System.Drawing.Color.Black;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rJ_DatePicker_效期);
            this.panel3.Controls.Add(this.rJ_Lable3);
            this.panel3.Location = new System.Drawing.Point(2, 128);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(349, 53);
            this.panel3.TabIndex = 171;
            // 
            // rJ_DatePicker_效期
            // 
            this.rJ_DatePicker_效期.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_DatePicker_效期.BorderSize = 0;
            this.rJ_DatePicker_效期.CalendarFont = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_效期.Font = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_效期.Location = new System.Drawing.Point(79, 9);
            this.rJ_DatePicker_效期.MinimumSize = new System.Drawing.Size(250, 35);
            this.rJ_DatePicker_效期.Name = "rJ_DatePicker_效期";
            this.rJ_DatePicker_效期.PickerFont = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_效期.PickerFore = System.Drawing.SystemColors.ControlText;
            this.rJ_DatePicker_效期.Size = new System.Drawing.Size(250, 35);
            this.rJ_DatePicker_效期.SkinColor = System.Drawing.Color.DimGray;
            this.rJ_DatePicker_效期.TabIndex = 163;
            this.rJ_DatePicker_效期.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Lable3
            // 
            this.rJ_Lable3.BackColor = System.Drawing.Color.White;
            this.rJ_Lable3.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable3.BorderRadius = 5;
            this.rJ_Lable3.BorderSize = 0;
            this.rJ_Lable3.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable3.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable3.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable3.GUID = "";
            this.rJ_Lable3.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable3.Name = "rJ_Lable3";
            this.rJ_Lable3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable3.ShadowSize = 0;
            this.rJ_Lable3.Size = new System.Drawing.Size(74, 51);
            this.rJ_Lable3.TabIndex = 162;
            this.rJ_Lable3.Text = "效期";
            this.rJ_Lable3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable3.TextColor = System.Drawing.Color.Black;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rJ_TextBox_藥名);
            this.panel2.Controls.Add(this.rJ_Lable2);
            this.panel2.Location = new System.Drawing.Point(2, 71);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(557, 53);
            this.panel2.TabIndex = 170;
            // 
            // rJ_TextBox_藥名
            // 
            this.rJ_TextBox_藥名.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥名.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥名.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥名.BorderRadius = 0;
            this.rJ_TextBox_藥名.BorderSize = 0;
            this.rJ_TextBox_藥名.Enabled = false;
            this.rJ_TextBox_藥名.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥名.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥名.GUID = "";
            this.rJ_TextBox_藥名.Location = new System.Drawing.Point(79, 11);
            this.rJ_TextBox_藥名.Margin = new System.Windows.Forms.Padding(2);
            this.rJ_TextBox_藥名.Multiline = false;
            this.rJ_TextBox_藥名.Name = "rJ_TextBox_藥名";
            this.rJ_TextBox_藥名.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.rJ_TextBox_藥名.PassWordChar = false;
            this.rJ_TextBox_藥名.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥名.PlaceholderText = "";
            this.rJ_TextBox_藥名.ShowTouchPannel = false;
            this.rJ_TextBox_藥名.Size = new System.Drawing.Size(463, 32);
            this.rJ_TextBox_藥名.TabIndex = 163;
            this.rJ_TextBox_藥名.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥名.Texts = "";
            this.rJ_TextBox_藥名.UnderlineStyle = false;
            // 
            // rJ_Lable2
            // 
            this.rJ_Lable2.BackColor = System.Drawing.Color.White;
            this.rJ_Lable2.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable2.BorderRadius = 5;
            this.rJ_Lable2.BorderSize = 0;
            this.rJ_Lable2.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable2.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable2.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable2.GUID = "";
            this.rJ_Lable2.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable2.Name = "rJ_Lable2";
            this.rJ_Lable2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable2.ShadowSize = 0;
            this.rJ_Lable2.Size = new System.Drawing.Size(74, 51);
            this.rJ_Lable2.TabIndex = 162;
            this.rJ_Lable2.Text = "藥名";
            this.rJ_Lable2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable2.TextColor = System.Drawing.Color.Black;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.rJ_TextBox_藥碼);
            this.panel6.Controls.Add(this.rJ_Lable1);
            this.panel6.Location = new System.Drawing.Point(2, 14);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(325, 53);
            this.panel6.TabIndex = 169;
            // 
            // rJ_TextBox_藥碼
            // 
            this.rJ_TextBox_藥碼.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥碼.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥碼.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥碼.BorderRadius = 0;
            this.rJ_TextBox_藥碼.BorderSize = 0;
            this.rJ_TextBox_藥碼.Enabled = false;
            this.rJ_TextBox_藥碼.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥碼.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥碼.GUID = "";
            this.rJ_TextBox_藥碼.Location = new System.Drawing.Point(79, 11);
            this.rJ_TextBox_藥碼.Margin = new System.Windows.Forms.Padding(2);
            this.rJ_TextBox_藥碼.Multiline = false;
            this.rJ_TextBox_藥碼.Name = "rJ_TextBox_藥碼";
            this.rJ_TextBox_藥碼.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.rJ_TextBox_藥碼.PassWordChar = false;
            this.rJ_TextBox_藥碼.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥碼.PlaceholderText = "";
            this.rJ_TextBox_藥碼.ShowTouchPannel = false;
            this.rJ_TextBox_藥碼.Size = new System.Drawing.Size(183, 32);
            this.rJ_TextBox_藥碼.TabIndex = 163;
            this.rJ_TextBox_藥碼.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥碼.Texts = "";
            this.rJ_TextBox_藥碼.UnderlineStyle = false;
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 5;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 0;
            this.rJ_Lable1.Size = new System.Drawing.Size(74, 51);
            this.rJ_Lable1.TabIndex = 162;
            this.rJ_Lable1.Text = "藥碼";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable1.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.BorderRadius = 10;
            this.rJ_Pannel1.BorderSize = 2;
            this.rJ_Pannel1.Controls.Add(this.sqL_DataGridView_TagList);
            this.rJ_Pannel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(14, 151);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.Padding = new System.Windows.Forms.Padding(10, 10, 11, 11);
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 3;
            this.rJ_Pannel1.Size = new System.Drawing.Size(990, 648);
            this.rJ_Pannel1.TabIndex = 166;
            // 
            // sqL_DataGridView_TagList
            // 
            this.sqL_DataGridView_TagList.AutoSelectToDeep = false;
            this.sqL_DataGridView_TagList.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_TagList.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_TagList.BorderRadius = 0;
            this.sqL_DataGridView_TagList.BorderSize = 0;
            this.sqL_DataGridView_TagList.CellBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_TagList.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_TagList.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_TagList.cellStyleFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_TagList.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_TagList.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_TagList.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_TagList.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_TagList.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_TagList.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_TagList.columnHeadersHeight = 40;
            this.sqL_DataGridView_TagList.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_TagList.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_TagList.DataKeyEnable = false;
            this.sqL_DataGridView_TagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_TagList.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_TagList.ImageBox = false;
            this.sqL_DataGridView_TagList.Location = new System.Drawing.Point(10, 10);
            this.sqL_DataGridView_TagList.Margin = new System.Windows.Forms.Padding(4);
            this.sqL_DataGridView_TagList.Name = "sqL_DataGridView_TagList";
            this.sqL_DataGridView_TagList.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_TagList.Password = "user82822040";
            this.sqL_DataGridView_TagList.Port = ((uint)(3306u));
            this.sqL_DataGridView_TagList.rowBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_TagList.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_TagList.rowHeaderBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_TagList.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_TagList.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_TagList.RowsHeight = 40;
            this.sqL_DataGridView_TagList.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_TagList.selectedBorderSize = 2;
            this.sqL_DataGridView_TagList.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_TagList.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_TagList.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_TagList.Server = "127.0.0.0";
            this.sqL_DataGridView_TagList.Size = new System.Drawing.Size(969, 627);
            this.sqL_DataGridView_TagList.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_TagList.TabIndex = 151;
            this.sqL_DataGridView_TagList.UserName = "root";
            this.sqL_DataGridView_TagList.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_TagList.可選擇多列 = false;
            this.sqL_DataGridView_TagList.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_TagList.自動換行 = true;
            this.sqL_DataGridView_TagList.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_TagList.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_TagList.顯示CheckBox = false;
            this.sqL_DataGridView_TagList.顯示首列 = true;
            this.sqL_DataGridView_TagList.顯示首行 = true;
            this.sqL_DataGridView_TagList.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_TagList.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // comboBox_燒錄位置
            // 
            this.comboBox_燒錄位置.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_燒錄位置.FormattingEnabled = true;
            this.comboBox_燒錄位置.Location = new System.Drawing.Point(1152, 33);
            this.comboBox_燒錄位置.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_燒錄位置.Name = "comboBox_燒錄位置";
            this.comboBox_燒錄位置.Size = new System.Drawing.Size(166, 32);
            this.comboBox_燒錄位置.TabIndex = 164;
            // 
            // rJ_Lable7
            // 
            this.rJ_Lable7.BackColor = System.Drawing.Color.White;
            this.rJ_Lable7.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable7.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable7.BorderRadius = 5;
            this.rJ_Lable7.BorderSize = 0;
            this.rJ_Lable7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable7.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable7.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable7.GUID = "";
            this.rJ_Lable7.Location = new System.Drawing.Point(1078, 13);
            this.rJ_Lable7.Name = "rJ_Lable7";
            this.rJ_Lable7.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable7.ShadowSize = 0;
            this.rJ_Lable7.Size = new System.Drawing.Size(69, 71);
            this.rJ_Lable7.TabIndex = 165;
            this.rJ_Lable7.Text = "冰箱:";
            this.rJ_Lable7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable7.TextColor = System.Drawing.Color.Black;
            // 
            // Main_From
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 40;
            this.ClientSize = new System.Drawing.Size(1600, 813);
            this.CloseBoxSize = new System.Drawing.Size(40, 40);
            this.ControlBox = true;
            this.Controls.Add(this.rJ_Pannel1);
            this.Controls.Add(this.rJ_Pannel3);
            this.Controls.Add(this.rJ_Pannel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(40, 40);
            this.MinimizeBox = true;
            this.MiniSize = new System.Drawing.Size(40, 40);
            this.Name = "Main_From";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能RFID燒錄系統";
            this.TopMost = false;
            this.rJ_Pannel2.ResumeLayout(false);
            this.rJ_Pannel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.rJ_Pannel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Comport;
        private MyUI.RJ_Button rJ_Button_Connect;
        private MyUI.RJ_Lable rJ_Lable4;
        private MyUI.RJ_Pannel rJ_Pannel2;
        private MyUI.RJ_Lable rJ_Lable_device_info;
        private MyUI.RJ_Lable rJ_Lable_標籤數量;
        private MyUI.RJ_Pannel rJ_Pannel3;
        private MyUI.RJ_Pannel rJ_Pannel1;
        private SQLUI.SQL_DataGridView sqL_DataGridView_TagList;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品資料;
        private System.Windows.Forms.Panel panel1;
        private MyUI.RJ_Button rJ_Button_Write;
        private System.Windows.Forms.Panel panel5;
        private MyUI.RJ_TextBox rJ_TextBox_數量;
        private MyUI.RJ_Lable rJ_Lable6;
        private System.Windows.Forms.Panel panel4;
        private MyUI.RJ_TextBox rJ_TextBox_批號;
        private MyUI.RJ_Lable rJ_Lable5;
        private System.Windows.Forms.Panel panel3;
        private MyUI.RJ_DatePicker rJ_DatePicker_效期;
        private MyUI.RJ_Lable rJ_Lable3;
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_TextBox rJ_TextBox_藥名;
        private MyUI.RJ_Lable rJ_Lable2;
        private System.Windows.Forms.Panel panel6;
        private MyUI.RJ_TextBox rJ_TextBox_藥碼;
        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.RJ_Button rJ_Button_藥品資料_搜尋;
        private MyUI.RJ_TextBox rJ_TextBox_藥品資料_搜尋內容;
        private System.Windows.Forms.ComboBox comboBox_藥品資料_搜尋方式;
        private MyUI.RJ_Lable rJ_Lable7;
        private System.Windows.Forms.ComboBox comboBox_燒錄位置;
    }
}

