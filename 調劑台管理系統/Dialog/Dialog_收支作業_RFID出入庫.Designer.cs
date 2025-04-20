namespace 調劑台管理系統
{
    partial class Dialog_收支作業_RFID出入庫
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
            this.rJ_Pannel2 = new MyUI.RJ_Pannel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plC_RJ_Button_確認送出 = new MyUI.PLC_RJ_Button();
            this.sqL_DataGridView_TagList = new SQLUI.SQL_DataGridView();
            this.dateTimeIntervelPicker_更新時間 = new MyUI.DateTimeIntervelPicker();
            this.rJ_Lable4 = new MyUI.RJ_Lable();
            this.rJ_Pannel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rJ_Pannel2
            // 
            this.rJ_Pannel2.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel2.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel2.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel2.BorderRadius = 10;
            this.rJ_Pannel2.BorderSize = 2;
            this.rJ_Pannel2.Controls.Add(this.panel1);
            this.rJ_Pannel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Pannel2.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel2.IsSelected = false;
            this.rJ_Pannel2.Location = new System.Drawing.Point(4, 44);
            this.rJ_Pannel2.Name = "rJ_Pannel2";
            this.rJ_Pannel2.Padding = new System.Windows.Forms.Padding(5, 5, 8, 8);
            this.rJ_Pannel2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel2.ShadowSize = 3;
            this.rJ_Pannel2.Size = new System.Drawing.Size(1642, 127);
            this.rJ_Pannel2.TabIndex = 152;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_Lable4);
            this.panel1.Controls.Add(this.dateTimeIntervelPicker_更新時間);
            this.panel1.Controls.Add(this.plC_RJ_Button_確認送出);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1629, 114);
            this.panel1.TabIndex = 1;
            // 
            // plC_RJ_Button_確認送出
            // 
            this.plC_RJ_Button_確認送出.AutoResetState = true;
            this.plC_RJ_Button_確認送出.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_確認送出.Bool = false;
            this.plC_RJ_Button_確認送出.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.BorderRadius = 15;
            this.plC_RJ_Button_確認送出.BorderSize = 1;
            this.plC_RJ_Button_確認送出.but_press = false;
            this.plC_RJ_Button_確認送出.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_確認送出.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_確認送出.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_確認送出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_確認送出.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認送出.GUID = "";
            this.plC_RJ_Button_確認送出.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_確認送出.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_確認送出.Location = new System.Drawing.Point(1519, 7);
            this.plC_RJ_Button_確認送出.Name = "plC_RJ_Button_確認送出";
            this.plC_RJ_Button_確認送出.OFF_文字內容 = "確認送出";
            this.plC_RJ_Button_確認送出.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認送出.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認送出.ON_BorderSize = 1;
            this.plC_RJ_Button_確認送出.ON_文字內容 = "確認送出";
            this.plC_RJ_Button_確認送出.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認送出.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.ON_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認送出.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_確認送出.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_確認送出.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_確認送出.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_確認送出.ShadowSize = 3;
            this.plC_RJ_Button_確認送出.ShowLoadingForm = false;
            this.plC_RJ_Button_確認送出.Size = new System.Drawing.Size(102, 102);
            this.plC_RJ_Button_確認送出.State = false;
            this.plC_RJ_Button_確認送出.TabIndex = 156;
            this.plC_RJ_Button_確認送出.Text = "確認送出";
            this.plC_RJ_Button_確認送出.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.TextHeight = 35;
            this.plC_RJ_Button_確認送出.Texts = "確認送出";
            this.plC_RJ_Button_確認送出.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_確認送出.字型鎖住 = false;
            this.plC_RJ_Button_確認送出.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_確認送出.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_確認送出.文字鎖住 = false;
            this.plC_RJ_Button_確認送出.背景圖片 = global::調劑台管理系統.Properties.Resources.adjusted_checkmark_removebg_preview;
            this.plC_RJ_Button_確認送出.讀取位元反向 = false;
            this.plC_RJ_Button_確認送出.讀寫鎖住 = false;
            this.plC_RJ_Button_確認送出.音效 = false;
            this.plC_RJ_Button_確認送出.顯示 = false;
            this.plC_RJ_Button_確認送出.顯示狀態 = false;
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
            this.sqL_DataGridView_TagList.Location = new System.Drawing.Point(4, 171);
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
            this.sqL_DataGridView_TagList.Size = new System.Drawing.Size(1642, 723);
            this.sqL_DataGridView_TagList.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_TagList.TabIndex = 153;
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
            // dateTimeIntervelPicker_更新時間
            // 
            this.dateTimeIntervelPicker_更新時間.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeIntervelPicker_更新時間.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dateTimeIntervelPicker_更新時間.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeIntervelPicker_更新時間.DateFont = new System.Drawing.Font("微軟正黑體", 14F);
            this.dateTimeIntervelPicker_更新時間.DateSize = new System.Drawing.Size(217, 39);
            this.dateTimeIntervelPicker_更新時間.EndTime = new System.DateTime(2024, 3, 28, 23, 59, 59, 0);
            this.dateTimeIntervelPicker_更新時間.Location = new System.Drawing.Point(167, 16);
            this.dateTimeIntervelPicker_更新時間.Name = "dateTimeIntervelPicker_更新時間";
            this.dateTimeIntervelPicker_更新時間.Padding = new System.Windows.Forms.Padding(2);
            this.dateTimeIntervelPicker_更新時間.Size = new System.Drawing.Size(316, 83);
            this.dateTimeIntervelPicker_更新時間.StartTime = new System.DateTime(2024, 3, 28, 0, 0, 0, 0);
            this.dateTimeIntervelPicker_更新時間.TabIndex = 157;
            this.dateTimeIntervelPicker_更新時間.TitleFont = new System.Drawing.Font("新細明體", 9F);
            this.dateTimeIntervelPicker_更新時間.TiTleSize = new System.Drawing.Size(33, 39);
            // 
            // rJ_Lable4
            // 
            this.rJ_Lable4.BackColor = System.Drawing.Color.White;
            this.rJ_Lable4.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable4.BorderRadius = 5;
            this.rJ_Lable4.BorderSize = 0;
            this.rJ_Lable4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable4.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable4.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable4.GUID = "";
            this.rJ_Lable4.Location = new System.Drawing.Point(21, 22);
            this.rJ_Lable4.Name = "rJ_Lable4";
            this.rJ_Lable4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable4.ShadowSize = 0;
            this.rJ_Lable4.Size = new System.Drawing.Size(140, 71);
            this.rJ_Lable4.TabIndex = 158;
            this.rJ_Lable4.Text = "更新時間";
            this.rJ_Lable4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable4.TextColor = System.Drawing.Color.Black;
            // 
            // Dialog_收支作業_RFID出入庫
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 40;
            this.ClientSize = new System.Drawing.Size(1650, 898);
            this.CloseBoxSize = new System.Drawing.Size(40, 40);
            this.ControlBox = true;
            this.Controls.Add(this.sqL_DataGridView_TagList);
            this.Controls.Add(this.rJ_Pannel2);
            this.MaxSize = new System.Drawing.Size(40, 40);
            this.MiniSize = new System.Drawing.Size(40, 40);
            this.Name = "Dialog_收支作業_RFID出入庫";
            this.Text = "RFID出入庫";
            this.rJ_Pannel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_Pannel rJ_Pannel2;
        private System.Windows.Forms.Panel panel1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_確認送出;
        private SQLUI.SQL_DataGridView sqL_DataGridView_TagList;
        private MyUI.DateTimeIntervelPicker dateTimeIntervelPicker_更新時間;
        private MyUI.RJ_Lable rJ_Lable4;
    }
}