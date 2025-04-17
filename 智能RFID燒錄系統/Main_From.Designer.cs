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
            this.rJ_Lable_device_info = new MyUI.RJ_Lable();
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.sqL_DataGridView_TagList = new SQLUI.SQL_DataGridView();
            this.rJ_Lable_標籤數量 = new MyUI.RJ_Lable();
            this.rJ_Pannel2.SuspendLayout();
            this.rJ_Pannel1.SuspendLayout();
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
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.BorderRadius = 10;
            this.rJ_Pannel1.BorderSize = 2;
            this.rJ_Pannel1.Controls.Add(this.sqL_DataGridView_TagList);
            this.rJ_Pannel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(14, 200);
            this.rJ_Pannel1.Margin = new System.Windows.Forms.Padding(4);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.Padding = new System.Windows.Forms.Padding(15, 15, 17, 17);
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 3;
            this.rJ_Pannel1.Size = new System.Drawing.Size(1576, 1230);
            this.rJ_Pannel1.TabIndex = 163;
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
            this.sqL_DataGridView_TagList.Size = new System.Drawing.Size(1544, 1198);
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
            this.rJ_Pannel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Comport;
        private MyUI.RJ_Button rJ_Button_Connect;
        private MyUI.RJ_Lable rJ_Lable4;
        private MyUI.RJ_Pannel rJ_Pannel2;
        private MyUI.RJ_Lable rJ_Lable_device_info;
        private MyUI.RJ_Pannel rJ_Pannel1;
        private SQLUI.SQL_DataGridView sqL_DataGridView_TagList;
        private MyUI.RJ_Lable rJ_Lable_標籤數量;
    }
}

