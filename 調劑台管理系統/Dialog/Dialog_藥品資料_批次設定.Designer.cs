namespace 調劑台管理系統
{
    partial class Dialog_藥品資料_批次設定
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_藥品選擇 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_設定種類 = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox_只顯示調劑台品項 = new System.Windows.Forms.CheckBox();
            this.comboBox_搜尋內容 = new System.Windows.Forms.ComboBox();
            this.rJ_Button_藥品搜尋 = new MyUI.PLC_RJ_Button();
            this.comboBox_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.sqL_DataGridView_藥品資料 = new SQLUI.SQL_DataGridView();
            this.rJ_Button_確認 = new MyUI.PLC_RJ_Button();
            this.panel2.SuspendLayout();
            this.panel_藥品選擇.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel_藥品選擇);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1520, 110);
            this.panel2.TabIndex = 144;
            // 
            // panel_藥品選擇
            // 
            this.panel_藥品選擇.Controls.Add(this.label1);
            this.panel_藥品選擇.Controls.Add(this.comboBox_設定種類);
            this.panel_藥品選擇.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_藥品選擇.Location = new System.Drawing.Point(0, 0);
            this.panel_藥品選擇.Name = "panel_藥品選擇";
            this.panel_藥品選擇.Size = new System.Drawing.Size(1520, 110);
            this.panel_藥品選擇.TabIndex = 141;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 40);
            this.label1.TabIndex = 9;
            this.label1.Text = "設定種類:";
            // 
            // comboBox_設定種類
            // 
            this.comboBox_設定種類.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_設定種類.FormattingEnabled = true;
            this.comboBox_設定種類.Items.AddRange(new object[] {
            "調劑註記",
            "形狀相似",
            "發音相似"});
            this.comboBox_設定種類.Location = new System.Drawing.Point(167, 30);
            this.comboBox_設定種類.Name = "comboBox_設定種類";
            this.comboBox_設定種類.Size = new System.Drawing.Size(302, 48);
            this.comboBox_設定種類.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 154);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1520, 17);
            this.panel5.TabIndex = 147;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rJ_Button_確認);
            this.panel4.Controls.Add(this.checkBox_只顯示調劑台品項);
            this.panel4.Controls.Add(this.comboBox_搜尋內容);
            this.panel4.Controls.Add(this.rJ_Button_藥品搜尋);
            this.panel4.Controls.Add(this.comboBox_搜尋條件);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(4, 845);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1520, 110);
            this.panel4.TabIndex = 149;
            // 
            // checkBox_只顯示調劑台品項
            // 
            this.checkBox_只顯示調劑台品項.AutoSize = true;
            this.checkBox_只顯示調劑台品項.Checked = true;
            this.checkBox_只顯示調劑台品項.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_只顯示調劑台品項.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_只顯示調劑台品項.Location = new System.Drawing.Point(14, 72);
            this.checkBox_只顯示調劑台品項.Name = "checkBox_只顯示調劑台品項";
            this.checkBox_只顯示調劑台品項.Size = new System.Drawing.Size(181, 28);
            this.checkBox_只顯示調劑台品項.TabIndex = 153;
            this.checkBox_只顯示調劑台品項.Text = "只顯示調劑台品項";
            this.checkBox_只顯示調劑台品項.UseVisualStyleBackColor = true;
            // 
            // comboBox_搜尋內容
            // 
            this.comboBox_搜尋內容.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_搜尋內容.FormattingEnabled = true;
            this.comboBox_搜尋內容.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox_搜尋內容.Location = new System.Drawing.Point(199, 18);
            this.comboBox_搜尋內容.Name = "comboBox_搜尋內容";
            this.comboBox_搜尋內容.Size = new System.Drawing.Size(319, 44);
            this.comboBox_搜尋內容.TabIndex = 152;
            // 
            // rJ_Button_藥品搜尋
            // 
            this.rJ_Button_藥品搜尋.AutoResetState = true;
            this.rJ_Button_藥品搜尋.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_藥品搜尋.Bool = false;
            this.rJ_Button_藥品搜尋.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.BorderRadius = 15;
            this.rJ_Button_藥品搜尋.BorderSize = 1;
            this.rJ_Button_藥品搜尋.but_press = false;
            this.rJ_Button_藥品搜尋.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.rJ_Button_藥品搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品搜尋.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品搜尋.GUID = "";
            this.rJ_Button_藥品搜尋.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.rJ_Button_藥品搜尋.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.rJ_Button_藥品搜尋.Location = new System.Drawing.Point(523, 4);
            this.rJ_Button_藥品搜尋.Name = "rJ_Button_藥品搜尋";
            this.rJ_Button_藥品搜尋.OFF_文字內容 = "搜尋";
            this.rJ_Button_藥品搜尋.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品搜尋.OFF_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.OFF_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品搜尋.ON_BorderSize = 1;
            this.rJ_Button_藥品搜尋.ON_文字內容 = "搜尋";
            this.rJ_Button_藥品搜尋.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品搜尋.ON_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.ON_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品搜尋.ShadowSize = 3;
            this.rJ_Button_藥品搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥品搜尋.Size = new System.Drawing.Size(102, 102);
            this.rJ_Button_藥品搜尋.State = false;
            this.rJ_Button_藥品搜尋.TabIndex = 151;
            this.rJ_Button_藥品搜尋.Text = "搜尋";
            this.rJ_Button_藥品搜尋.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.TextHeight = 35;
            this.rJ_Button_藥品搜尋.Texts = "搜尋";
            this.rJ_Button_藥品搜尋.UseVisualStyleBackColor = false;
            this.rJ_Button_藥品搜尋.字型鎖住 = false;
            this.rJ_Button_藥品搜尋.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.rJ_Button_藥品搜尋.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.rJ_Button_藥品搜尋.文字鎖住 = false;
            this.rJ_Button_藥品搜尋.背景圖片 = global::調劑台管理系統.Properties.Resources.icon_for_searching_medicine_removebg_preview;
            this.rJ_Button_藥品搜尋.讀取位元反向 = false;
            this.rJ_Button_藥品搜尋.讀寫鎖住 = false;
            this.rJ_Button_藥品搜尋.音效 = false;
            this.rJ_Button_藥品搜尋.顯示 = false;
            this.rJ_Button_藥品搜尋.顯示狀態 = false;
            // 
            // comboBox_搜尋條件
            // 
            this.comboBox_搜尋條件.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_搜尋條件.FormattingEnabled = true;
            this.comboBox_搜尋條件.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名",
            "中文名",
            "管制級別",
            "已選藥品"});
            this.comboBox_搜尋條件.Location = new System.Drawing.Point(14, 18);
            this.comboBox_搜尋條件.Name = "comboBox_搜尋條件";
            this.comboBox_搜尋條件.Size = new System.Drawing.Size(179, 44);
            this.comboBox_搜尋條件.TabIndex = 149;
            // 
            // sqL_DataGridView_藥品資料
            // 
            this.sqL_DataGridView_藥品資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥品資料.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_藥品資料.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_藥品資料.BorderRadius = 0;
            this.sqL_DataGridView_藥品資料.BorderSize = 0;
            this.sqL_DataGridView_藥品資料.CellBorderColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_藥品資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品資料.checkedRowBackColor = System.Drawing.Color.DodgerBlue;
            this.sqL_DataGridView_藥品資料.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥品資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥品資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.columnHeadersHeight = 40;
            this.sqL_DataGridView_藥品資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_藥品資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_藥品資料.DataKeyEnable = true;
            this.sqL_DataGridView_藥品資料.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_藥品資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品資料.ImageBox = false;
            this.sqL_DataGridView_藥品資料.Location = new System.Drawing.Point(4, 171);
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
            this.sqL_DataGridView_藥品資料.selectedRowForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品資料.Server = "127.0.0.0";
            this.sqL_DataGridView_藥品資料.Size = new System.Drawing.Size(1520, 674);
            this.sqL_DataGridView_藥品資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品資料.TabIndex = 150;
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
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = true;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_確認.Bool = false;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_確認.BorderRadius = 15;
            this.rJ_Button_確認.BorderSize = 1;
            this.rJ_Button_確認.but_press = false;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.rJ_Button_確認.Location = new System.Drawing.Point(1416, 0);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.OFF_文字內容 = "確認";
            this.rJ_Button_確認.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_確認.OFF_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_確認.OFF_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_確認.ON_BorderSize = 1;
            this.rJ_Button_確認.ON_文字內容 = "確認";
            this.rJ_Button_確認.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_確認.ON_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_確認.ON_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(102, 108);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 154;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_確認.TextHeight = 35;
            this.rJ_Button_確認.Texts = "確認";
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            this.rJ_Button_確認.字型鎖住 = false;
            this.rJ_Button_確認.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.rJ_Button_確認.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.rJ_Button_確認.文字鎖住 = false;
            this.rJ_Button_確認.背景圖片 = global::調劑台管理系統.Properties.Resources.adjusted_checkmark_removebg_preview;
            this.rJ_Button_確認.讀取位元反向 = false;
            this.rJ_Button_確認.讀寫鎖住 = false;
            this.rJ_Button_確認.音效 = false;
            this.rJ_Button_確認.顯示 = false;
            this.rJ_Button_確認.顯示狀態 = false;
            // 
            // Dialog_藥品資料_批次設定
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 40;
            this.ClientSize = new System.Drawing.Size(1528, 959);
            this.CloseBoxSize = new System.Drawing.Size(40, 40);
            this.ControlBox = true;
            this.Controls.Add(this.sqL_DataGridView_藥品資料);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(40, 40);
            this.MiniSize = new System.Drawing.Size(40, 40);
            this.Name = "Dialog_藥品資料_批次設定";
            this.Text = "批次設定";
            this.panel2.ResumeLayout(false);
            this.panel_藥品選擇.ResumeLayout(false);
            this.panel_藥品選擇.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_藥品選擇;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_設定種類;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox_只顯示調劑台品項;
        private System.Windows.Forms.ComboBox comboBox_搜尋內容;
        private MyUI.PLC_RJ_Button rJ_Button_藥品搜尋;
        private System.Windows.Forms.ComboBox comboBox_搜尋條件;
        private MyUI.PLC_RJ_Button rJ_Button_確認;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品資料;
    }
}