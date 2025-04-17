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
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.sqL_DataGridView_TagList = new SQLUI.SQL_DataGridView();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_藥碼 = new MyUI.RJ_TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_藥名 = new MyUI.RJ_TextBox();
            this.rJ_Lable2 = new MyUI.RJ_Lable();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Lable3 = new MyUI.RJ_Lable();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_批號 = new MyUI.RJ_TextBox();
            this.rJ_Lable5 = new MyUI.RJ_Lable();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_數量 = new MyUI.RJ_TextBox();
            this.rJ_Lable6 = new MyUI.RJ_Lable();
            this.rJ_DatePicker_效期 = new MyUI.RJ_DatePicker();
            this.rJ_Button_Write = new MyUI.RJ_Button();
            this.rJ_Pannel2.SuspendLayout();
            this.rJ_Pannel3.SuspendLayout();
            this.rJ_Pannel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Comport
            // 
            this.comboBox_Comport.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_Comport.FormattingEnabled = true;
            this.comboBox_Comport.Location = new System.Drawing.Point(105, 51);
            this.comboBox_Comport.Name = "comboBox_Comport";
            this.comboBox_Comport.Size = new System.Drawing.Size(152, 44);
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
            this.rJ_Button_Connect.Location = new System.Drawing.Point(276, 37);
            this.rJ_Button_Connect.Margin = new System.Windows.Forms.Padding(4);
            this.rJ_Button_Connect.Name = "rJ_Button_Connect";
            this.rJ_Button_Connect.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_Connect.ProhibitionLineWidth = 4;
            this.rJ_Button_Connect.ProhibitionSymbolSize = 30;
            this.rJ_Button_Connect.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Connect.ShadowSize = 0;
            this.rJ_Button_Connect.ShowLoadingForm = false;
            this.rJ_Button_Connect.Size = new System.Drawing.Size(145, 72);
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
            this.rJ_Lable4.Location = new System.Drawing.Point(20, 19);
            this.rJ_Lable4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable4.Name = "rJ_Lable4";
            this.rJ_Lable4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable4.ShadowSize = 0;
            this.rJ_Lable4.Size = new System.Drawing.Size(74, 106);
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
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable_標籤數量);
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable_device_info);
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable4);
            this.rJ_Pannel2.Controls.Add(this.comboBox_Comport);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_Connect);
            this.rJ_Pannel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Pannel2.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel2.IsSelected = false;
            this.rJ_Pannel2.Location = new System.Drawing.Point(14, 54);
            this.rJ_Pannel2.Margin = new System.Windows.Forms.Padding(4);
            this.rJ_Pannel2.Name = "rJ_Pannel2";
            this.rJ_Pannel2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel2.ShadowSize = 3;
            this.rJ_Pannel2.Size = new System.Drawing.Size(2206, 146);
            this.rJ_Pannel2.TabIndex = 162;
            // 
            // rJ_Lable_標籤數量
            // 
            this.rJ_Lable_標籤數量.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_標籤數量.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_標籤數量.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_標籤數量.BorderRadius = 5;
            this.rJ_Lable_標籤數量.BorderSize = 0;
            this.rJ_Lable_標籤數量.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_標籤數量.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_標籤數量.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_標籤數量.GUID = "";
            this.rJ_Lable_標籤數量.Location = new System.Drawing.Point(1887, 19);
            this.rJ_Lable_標籤數量.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable_標籤數量.Name = "rJ_Lable_標籤數量";
            this.rJ_Lable_標籤數量.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_標籤數量.ShadowSize = 0;
            this.rJ_Lable_標籤數量.Size = new System.Drawing.Size(235, 106);
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
            this.rJ_Lable_device_info.Location = new System.Drawing.Point(463, 19);
            this.rJ_Lable_device_info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable_device_info.Name = "rJ_Lable_device_info";
            this.rJ_Lable_device_info.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_device_info.ShadowSize = 0;
            this.rJ_Lable_device_info.Size = new System.Drawing.Size(1416, 106);
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
            this.rJ_Pannel3.Controls.Add(this.rJ_Button_Write);
            this.rJ_Pannel3.Controls.Add(this.panel5);
            this.rJ_Pannel3.Controls.Add(this.panel4);
            this.rJ_Pannel3.Controls.Add(this.panel3);
            this.rJ_Pannel3.Controls.Add(this.panel2);
            this.rJ_Pannel3.Controls.Add(this.panel1);
            this.rJ_Pannel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Pannel3.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel3.IsSelected = false;
            this.rJ_Pannel3.Location = new System.Drawing.Point(1598, 200);
            this.rJ_Pannel3.Margin = new System.Windows.Forms.Padding(4);
            this.rJ_Pannel3.Name = "rJ_Pannel3";
            this.rJ_Pannel3.Padding = new System.Windows.Forms.Padding(15, 15, 17, 17);
            this.rJ_Pannel3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel3.ShadowSize = 3;
            this.rJ_Pannel3.Size = new System.Drawing.Size(622, 1230);
            this.rJ_Pannel3.TabIndex = 165;
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
            this.rJ_Pannel1.Location = new System.Drawing.Point(14, 200);
            this.rJ_Pannel1.Margin = new System.Windows.Forms.Padding(4);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.Padding = new System.Windows.Forms.Padding(15, 15, 17, 17);
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 3;
            this.rJ_Pannel1.Size = new System.Drawing.Size(1584, 1230);
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
            this.sqL_DataGridView_TagList.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_TagList.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_TagList.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_TagList.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_TagList.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_TagList.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_TagList.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_TagList.columnHeadersHeight = 40;
            this.sqL_DataGridView_TagList.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_TagList.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sqL_DataGridView_TagList.DataKeyEnable = false;
            this.sqL_DataGridView_TagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_TagList.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_TagList.ImageBox = false;
            this.sqL_DataGridView_TagList.Location = new System.Drawing.Point(15, 15);
            this.sqL_DataGridView_TagList.Margin = new System.Windows.Forms.Padding(6);
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
            this.sqL_DataGridView_TagList.Size = new System.Drawing.Size(1552, 1198);
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
            this.rJ_Lable1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 0;
            this.rJ_Lable1.Size = new System.Drawing.Size(111, 77);
            this.rJ_Lable1.TabIndex = 162;
            this.rJ_Lable1.Text = "藥碼";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable1.TextColor = System.Drawing.Color.Black;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rJ_TextBox_藥碼);
            this.panel1.Controls.Add(this.rJ_Lable1);
            this.panel1.Location = new System.Drawing.Point(52, 435);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 79);
            this.panel1.TabIndex = 163;
            // 
            // rJ_TextBox_藥碼
            // 
            this.rJ_TextBox_藥碼.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥碼.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥碼.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥碼.BorderRadius = 0;
            this.rJ_TextBox_藥碼.BorderSize = 0;
            this.rJ_TextBox_藥碼.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥碼.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥碼.GUID = "";
            this.rJ_TextBox_藥碼.Location = new System.Drawing.Point(118, 16);
            this.rJ_TextBox_藥碼.Multiline = false;
            this.rJ_TextBox_藥碼.Name = "rJ_TextBox_藥碼";
            this.rJ_TextBox_藥碼.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥碼.PassWordChar = false;
            this.rJ_TextBox_藥碼.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥碼.PlaceholderText = "";
            this.rJ_TextBox_藥碼.ShowTouchPannel = false;
            this.rJ_TextBox_藥碼.Size = new System.Drawing.Size(274, 46);
            this.rJ_TextBox_藥碼.TabIndex = 163;
            this.rJ_TextBox_藥碼.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥碼.Texts = "";
            this.rJ_TextBox_藥碼.UnderlineStyle = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rJ_TextBox_藥名);
            this.panel2.Controls.Add(this.rJ_Lable2);
            this.panel2.Location = new System.Drawing.Point(52, 516);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(486, 79);
            this.panel2.TabIndex = 164;
            // 
            // rJ_TextBox_藥名
            // 
            this.rJ_TextBox_藥名.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥名.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥名.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥名.BorderRadius = 0;
            this.rJ_TextBox_藥名.BorderSize = 0;
            this.rJ_TextBox_藥名.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥名.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥名.GUID = "";
            this.rJ_TextBox_藥名.Location = new System.Drawing.Point(118, 16);
            this.rJ_TextBox_藥名.Multiline = false;
            this.rJ_TextBox_藥名.Name = "rJ_TextBox_藥名";
            this.rJ_TextBox_藥名.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥名.PassWordChar = false;
            this.rJ_TextBox_藥名.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥名.PlaceholderText = "";
            this.rJ_TextBox_藥名.ShowTouchPannel = false;
            this.rJ_TextBox_藥名.Size = new System.Drawing.Size(274, 46);
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
            this.rJ_Lable2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable2.Name = "rJ_Lable2";
            this.rJ_Lable2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable2.ShadowSize = 0;
            this.rJ_Lable2.Size = new System.Drawing.Size(111, 77);
            this.rJ_Lable2.TabIndex = 162;
            this.rJ_Lable2.Text = "藥名";
            this.rJ_Lable2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable2.TextColor = System.Drawing.Color.Black;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rJ_DatePicker_效期);
            this.panel3.Controls.Add(this.rJ_Lable3);
            this.panel3.Location = new System.Drawing.Point(52, 597);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(522, 79);
            this.panel3.TabIndex = 165;
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
            this.rJ_Lable3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable3.Name = "rJ_Lable3";
            this.rJ_Lable3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable3.ShadowSize = 0;
            this.rJ_Lable3.Size = new System.Drawing.Size(111, 77);
            this.rJ_Lable3.TabIndex = 162;
            this.rJ_Lable3.Text = "效期";
            this.rJ_Lable3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable3.TextColor = System.Drawing.Color.Black;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rJ_TextBox_批號);
            this.panel4.Controls.Add(this.rJ_Lable5);
            this.panel4.Location = new System.Drawing.Point(52, 678);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(486, 79);
            this.panel4.TabIndex = 166;
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
            this.rJ_TextBox_批號.Location = new System.Drawing.Point(118, 16);
            this.rJ_TextBox_批號.Multiline = false;
            this.rJ_TextBox_批號.Name = "rJ_TextBox_批號";
            this.rJ_TextBox_批號.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_批號.PassWordChar = false;
            this.rJ_TextBox_批號.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_批號.PlaceholderText = "";
            this.rJ_TextBox_批號.ShowTouchPannel = false;
            this.rJ_TextBox_批號.Size = new System.Drawing.Size(274, 46);
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
            this.rJ_Lable5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable5.Name = "rJ_Lable5";
            this.rJ_Lable5.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable5.ShadowSize = 0;
            this.rJ_Lable5.Size = new System.Drawing.Size(111, 77);
            this.rJ_Lable5.TabIndex = 162;
            this.rJ_Lable5.Text = "批號";
            this.rJ_Lable5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable5.TextColor = System.Drawing.Color.Black;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.rJ_TextBox_數量);
            this.panel5.Controls.Add(this.rJ_Lable6);
            this.panel5.Location = new System.Drawing.Point(52, 763);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(486, 79);
            this.panel5.TabIndex = 167;
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
            this.rJ_TextBox_數量.Location = new System.Drawing.Point(118, 16);
            this.rJ_TextBox_數量.Multiline = false;
            this.rJ_TextBox_數量.Name = "rJ_TextBox_數量";
            this.rJ_TextBox_數量.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_數量.PassWordChar = false;
            this.rJ_TextBox_數量.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_數量.PlaceholderText = "";
            this.rJ_TextBox_數量.ShowTouchPannel = false;
            this.rJ_TextBox_數量.Size = new System.Drawing.Size(274, 46);
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
            this.rJ_Lable6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rJ_Lable6.Name = "rJ_Lable6";
            this.rJ_Lable6.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable6.ShadowSize = 0;
            this.rJ_Lable6.Size = new System.Drawing.Size(111, 77);
            this.rJ_Lable6.TabIndex = 162;
            this.rJ_Lable6.Text = "數量";
            this.rJ_Lable6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable6.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_DatePicker_效期
            // 
            this.rJ_DatePicker_效期.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_DatePicker_效期.BorderSize = 0;
            this.rJ_DatePicker_效期.CalendarFont = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_效期.Font = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_效期.Location = new System.Drawing.Point(118, 13);
            this.rJ_DatePicker_效期.Margin = new System.Windows.Forms.Padding(4);
            this.rJ_DatePicker_效期.MinimumSize = new System.Drawing.Size(373, 35);
            this.rJ_DatePicker_效期.Name = "rJ_DatePicker_效期";
            this.rJ_DatePicker_效期.PickerFont = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_效期.PickerFore = System.Drawing.SystemColors.ControlText;
            this.rJ_DatePicker_效期.Size = new System.Drawing.Size(373, 46);
            this.rJ_DatePicker_效期.SkinColor = System.Drawing.Color.DimGray;
            this.rJ_DatePicker_效期.TabIndex = 163;
            this.rJ_DatePicker_效期.TextColor = System.Drawing.Color.White;
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
            this.rJ_Button_Write.Location = new System.Drawing.Point(393, 849);
            this.rJ_Button_Write.Margin = new System.Windows.Forms.Padding(4);
            this.rJ_Button_Write.Name = "rJ_Button_Write";
            this.rJ_Button_Write.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_Write.ProhibitionLineWidth = 4;
            this.rJ_Button_Write.ProhibitionSymbolSize = 30;
            this.rJ_Button_Write.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Write.ShadowSize = 0;
            this.rJ_Button_Write.ShowLoadingForm = false;
            this.rJ_Button_Write.Size = new System.Drawing.Size(145, 72);
            this.rJ_Button_Write.State = false;
            this.rJ_Button_Write.TabIndex = 168;
            this.rJ_Button_Write.Text = "Write";
            this.rJ_Button_Write.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Write.TextHeight = 0;
            this.rJ_Button_Write.UseVisualStyleBackColor = false;
            // 
            // Main_From
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 40;
            this.ClientSize = new System.Drawing.Size(2234, 1444);
            this.CloseBoxSize = new System.Drawing.Size(40, 40);
            this.ControlBox = true;
            this.Controls.Add(this.rJ_Pannel1);
            this.Controls.Add(this.rJ_Pannel3);
            this.Controls.Add(this.rJ_Pannel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.rJ_Pannel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel5;
        private MyUI.RJ_TextBox rJ_TextBox_數量;
        private MyUI.RJ_Lable rJ_Lable6;
        private System.Windows.Forms.Panel panel4;
        private MyUI.RJ_TextBox rJ_TextBox_批號;
        private MyUI.RJ_Lable rJ_Lable5;
        private System.Windows.Forms.Panel panel3;
        private MyUI.RJ_Lable rJ_Lable3;
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_TextBox rJ_TextBox_藥名;
        private MyUI.RJ_Lable rJ_Lable2;
        private System.Windows.Forms.Panel panel1;
        private MyUI.RJ_TextBox rJ_TextBox_藥碼;
        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.RJ_DatePicker rJ_DatePicker_效期;
        private MyUI.RJ_Button rJ_Button_Write;
    }
}

