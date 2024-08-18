
namespace 智能藥庫系統
{
    partial class Dialog_盤點明細
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.plC_RJ_Button_返回 = new MyUI.PLC_RJ_Button();
            this.panel_controls = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_盤點明細 = new SQLUI.SQL_DataGridView();
            this.rJ_Button_搜尋 = new MyUI.RJ_Button();
            this.comboBox_搜尋內容 = new System.Windows.Forms.ComboBox();
            this.comboBox_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox_已盤 = new System.Windows.Forms.CheckBox();
            this.checkBox_未盤 = new System.Windows.Forms.CheckBox();
            this.plC_RJ_Button_刪除 = new MyUI.PLC_RJ_Button();
            this.panel1.SuspendLayout();
            this.panel_controls.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1192, 42);
            this.panel2.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plC_RJ_Button_刪除);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.plC_RJ_Button_返回);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 868);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1192, 88);
            this.panel1.TabIndex = 10;
            // 
            // plC_RJ_Button_返回
            // 
            this.plC_RJ_Button_返回.AutoResetState = false;
            this.plC_RJ_Button_返回.BackgroundColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.Bool = false;
            this.plC_RJ_Button_返回.BorderColor = System.Drawing.Color.Thistle;
            this.plC_RJ_Button_返回.BorderRadius = 20;
            this.plC_RJ_Button_返回.BorderSize = 0;
            this.plC_RJ_Button_返回.but_press = false;
            this.plC_RJ_Button_返回.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_返回.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.plC_RJ_Button_返回.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_返回.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_返回.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_返回.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_返回.GUID = "";
            this.plC_RJ_Button_返回.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_返回.Image_padding = new System.Windows.Forms.Padding(0);
            this.plC_RJ_Button_返回.Location = new System.Drawing.Point(1031, 0);
            this.plC_RJ_Button_返回.Name = "plC_RJ_Button_返回";
            this.plC_RJ_Button_返回.OFF_文字內容 = "返回";
            this.plC_RJ_Button_返回.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_返回.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.OFF_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.ON_BorderSize = 5;
            this.plC_RJ_Button_返回.ON_文字內容 = "返回";
            this.plC_RJ_Button_返回.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_返回.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.ON_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_返回.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_返回.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_返回.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_返回.ShadowSize = 3;
            this.plC_RJ_Button_返回.ShowLoadingForm = false;
            this.plC_RJ_Button_返回.Size = new System.Drawing.Size(161, 88);
            this.plC_RJ_Button_返回.State = false;
            this.plC_RJ_Button_返回.TabIndex = 10;
            this.plC_RJ_Button_返回.Text = "返回";
            this.plC_RJ_Button_返回.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.TextHeight = 0;
            this.plC_RJ_Button_返回.Texts = "返回";
            this.plC_RJ_Button_返回.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_返回.字型鎖住 = false;
            this.plC_RJ_Button_返回.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_返回.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_返回.文字鎖住 = false;
            this.plC_RJ_Button_返回.背景圖片 = null;
            this.plC_RJ_Button_返回.讀取位元反向 = false;
            this.plC_RJ_Button_返回.讀寫鎖住 = false;
            this.plC_RJ_Button_返回.音效 = false;
            this.plC_RJ_Button_返回.顯示 = false;
            this.plC_RJ_Button_返回.顯示狀態 = false;
            // 
            // panel_controls
            // 
            this.panel_controls.Controls.Add(this.sqL_DataGridView_盤點明細);
            this.panel_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_controls.Location = new System.Drawing.Point(4, 70);
            this.panel_controls.Name = "panel_controls";
            this.panel_controls.Size = new System.Drawing.Size(1192, 798);
            this.panel_controls.TabIndex = 11;
            // 
            // sqL_DataGridView_盤點明細
            // 
            this.sqL_DataGridView_盤點明細.AutoSelectToDeep = false;
            this.sqL_DataGridView_盤點明細.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_盤點明細.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_盤點明細.BorderRadius = 0;
            this.sqL_DataGridView_盤點明細.BorderSize = 0;
            this.sqL_DataGridView_盤點明細.CellBorderColor = System.Drawing.Color.White;
            this.sqL_DataGridView_盤點明細.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_盤點明細.cellStylBackColor = System.Drawing.Color.White;
            this.sqL_DataGridView_盤點明細.cellStyleFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_盤點明細.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_盤點明細.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_盤點明細.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_盤點明細.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_盤點明細.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_盤點明細.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_盤點明細.columnHeadersHeight = 18;
            this.sqL_DataGridView_盤點明細.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_盤點明細.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_盤點明細.DataKeyEnable = false;
            this.sqL_DataGridView_盤點明細.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_盤點明細.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_盤點明細.ImageBox = false;
            this.sqL_DataGridView_盤點明細.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_盤點明細.Name = "sqL_DataGridView_盤點明細";
            this.sqL_DataGridView_盤點明細.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_盤點明細.Password = "user82822040";
            this.sqL_DataGridView_盤點明細.Port = ((uint)(3306u));
            this.sqL_DataGridView_盤點明細.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_盤點明細.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_盤點明細.RowsColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_盤點明細.RowsHeight = 10;
            this.sqL_DataGridView_盤點明細.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_盤點明細.selectedBorderSize = 0;
            this.sqL_DataGridView_盤點明細.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_盤點明細.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_盤點明細.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_盤點明細.Server = "127.0.0.0";
            this.sqL_DataGridView_盤點明細.Size = new System.Drawing.Size(1192, 798);
            this.sqL_DataGridView_盤點明細.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_盤點明細.TabIndex = 12;
            this.sqL_DataGridView_盤點明細.UserName = "root";
            this.sqL_DataGridView_盤點明細.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_盤點明細.可選擇多列 = false;
            this.sqL_DataGridView_盤點明細.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_盤點明細.自動換行 = true;
            this.sqL_DataGridView_盤點明細.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_盤點明細.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_盤點明細.顯示CheckBox = true;
            this.sqL_DataGridView_盤點明細.顯示首列 = true;
            this.sqL_DataGridView_盤點明細.顯示首行 = true;
            this.sqL_DataGridView_盤點明細.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_盤點明細.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // rJ_Button_搜尋
            // 
            this.rJ_Button_搜尋.AutoResetState = false;
            this.rJ_Button_搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_搜尋.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_搜尋.BorderRadius = 20;
            this.rJ_Button_搜尋.BorderSize = 0;
            this.rJ_Button_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_搜尋.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_搜尋.GUID = "";
            this.rJ_Button_搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_搜尋.Location = new System.Drawing.Point(648, 18);
            this.rJ_Button_搜尋.Name = "rJ_Button_搜尋";
            this.rJ_Button_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_搜尋.ShadowSize = 3;
            this.rJ_Button_搜尋.ShowLoadingForm = false;
            this.rJ_Button_搜尋.Size = new System.Drawing.Size(110, 57);
            this.rJ_Button_搜尋.State = false;
            this.rJ_Button_搜尋.TabIndex = 127;
            this.rJ_Button_搜尋.Text = "搜尋";
            this.rJ_Button_搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_搜尋.TextHeight = 0;
            this.rJ_Button_搜尋.UseVisualStyleBackColor = false;
            // 
            // comboBox_搜尋內容
            // 
            this.comboBox_搜尋內容.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.comboBox_搜尋內容.FormattingEnabled = true;
            this.comboBox_搜尋內容.Location = new System.Drawing.Point(349, 25);
            this.comboBox_搜尋內容.Name = "comboBox_搜尋內容";
            this.comboBox_搜尋內容.Size = new System.Drawing.Size(291, 39);
            this.comboBox_搜尋內容.TabIndex = 126;
            // 
            // comboBox_搜尋條件
            // 
            this.comboBox_搜尋條件.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_搜尋條件.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.comboBox_搜尋條件.FormattingEnabled = true;
            this.comboBox_搜尋條件.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名"});
            this.comboBox_搜尋條件.Location = new System.Drawing.Point(179, 25);
            this.comboBox_搜尋條件.Name = "comboBox_搜尋條件";
            this.comboBox_搜尋條件.Size = new System.Drawing.Size(164, 39);
            this.comboBox_搜尋條件.TabIndex = 125;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBox_未盤);
            this.panel3.Controls.Add(this.checkBox_已盤);
            this.panel3.Controls.Add(this.comboBox_搜尋條件);
            this.panel3.Controls.Add(this.rJ_Button_搜尋);
            this.panel3.Controls.Add(this.comboBox_搜尋內容);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(768, 88);
            this.panel3.TabIndex = 128;
            // 
            // checkBox_已盤
            // 
            this.checkBox_已盤.AutoSize = true;
            this.checkBox_已盤.Checked = true;
            this.checkBox_已盤.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_已盤.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_已盤.Location = new System.Drawing.Point(22, 36);
            this.checkBox_已盤.Name = "checkBox_已盤";
            this.checkBox_已盤.Size = new System.Drawing.Size(66, 23);
            this.checkBox_已盤.TabIndex = 129;
            this.checkBox_已盤.Text = "已盤";
            this.checkBox_已盤.UseVisualStyleBackColor = true;
            // 
            // checkBox_未盤
            // 
            this.checkBox_未盤.AutoSize = true;
            this.checkBox_未盤.Checked = true;
            this.checkBox_未盤.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_未盤.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_未盤.Location = new System.Drawing.Point(94, 36);
            this.checkBox_未盤.Name = "checkBox_未盤";
            this.checkBox_未盤.Size = new System.Drawing.Size(66, 23);
            this.checkBox_未盤.TabIndex = 130;
            this.checkBox_未盤.Text = "未盤";
            this.checkBox_未盤.UseVisualStyleBackColor = true;
            // 
            // plC_RJ_Button_刪除
            // 
            this.plC_RJ_Button_刪除.AutoResetState = false;
            this.plC_RJ_Button_刪除.BackgroundColor = System.Drawing.Color.Red;
            this.plC_RJ_Button_刪除.Bool = false;
            this.plC_RJ_Button_刪除.BorderColor = System.Drawing.Color.Thistle;
            this.plC_RJ_Button_刪除.BorderRadius = 20;
            this.plC_RJ_Button_刪除.BorderSize = 0;
            this.plC_RJ_Button_刪除.but_press = false;
            this.plC_RJ_Button_刪除.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_刪除.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.plC_RJ_Button_刪除.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_刪除.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_刪除.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_刪除.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_刪除.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_刪除.GUID = "";
            this.plC_RJ_Button_刪除.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_刪除.Image_padding = new System.Windows.Forms.Padding(0);
            this.plC_RJ_Button_刪除.Location = new System.Drawing.Point(870, 0);
            this.plC_RJ_Button_刪除.Name = "plC_RJ_Button_刪除";
            this.plC_RJ_Button_刪除.OFF_文字內容 = "刪除";
            this.plC_RJ_Button_刪除.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_刪除.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_刪除.OFF_背景顏色 = System.Drawing.Color.Red;
            this.plC_RJ_Button_刪除.ON_BorderSize = 5;
            this.plC_RJ_Button_刪除.ON_文字內容 = "刪除";
            this.plC_RJ_Button_刪除.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_刪除.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_刪除.ON_背景顏色 = System.Drawing.Color.Red;
            this.plC_RJ_Button_刪除.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_刪除.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_刪除.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_刪除.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_刪除.ShadowSize = 3;
            this.plC_RJ_Button_刪除.ShowLoadingForm = false;
            this.plC_RJ_Button_刪除.Size = new System.Drawing.Size(161, 88);
            this.plC_RJ_Button_刪除.State = false;
            this.plC_RJ_Button_刪除.TabIndex = 129;
            this.plC_RJ_Button_刪除.Text = "刪除";
            this.plC_RJ_Button_刪除.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_刪除.TextHeight = 0;
            this.plC_RJ_Button_刪除.Texts = "刪除";
            this.plC_RJ_Button_刪除.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_刪除.字型鎖住 = false;
            this.plC_RJ_Button_刪除.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_刪除.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_刪除.文字鎖住 = false;
            this.plC_RJ_Button_刪除.背景圖片 = null;
            this.plC_RJ_Button_刪除.讀取位元反向 = false;
            this.plC_RJ_Button_刪除.讀寫鎖住 = false;
            this.plC_RJ_Button_刪除.音效 = false;
            this.plC_RJ_Button_刪除.顯示 = false;
            this.plC_RJ_Button_刪除.顯示狀態 = false;
            // 
            // Dialog_盤點明細
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 960);
            this.ControlBox = true;
            this.Controls.Add(this.panel_controls);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Dialog_盤點明細";
            this.Text = "盤點明細";
            this.panel1.ResumeLayout(false);
            this.panel_controls.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_返回;
        private System.Windows.Forms.Panel panel_controls;
        private SQLUI.SQL_DataGridView sqL_DataGridView_盤點明細;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBox_搜尋條件;
        private MyUI.RJ_Button rJ_Button_搜尋;
        private System.Windows.Forms.ComboBox comboBox_搜尋內容;
        private System.Windows.Forms.CheckBox checkBox_未盤;
        private System.Windows.Forms.CheckBox checkBox_已盤;
        private MyUI.PLC_RJ_Button plC_RJ_Button_刪除;
    }
}