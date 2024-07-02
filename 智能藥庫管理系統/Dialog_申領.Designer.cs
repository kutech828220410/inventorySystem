
namespace 智能藥庫系統
{
    partial class Dialog_申領
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
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.rJ_Button_搜尋 = new MyUI.RJ_Button();
            this.comboBox_搜尋內容 = new System.Windows.Forms.ComboBox();
            this.comboBox_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.dateTimeIntervelPicker_報表日期 = new MyUI.DateTimeIntervelPicker();
            this.sqL_DataGridView_申領品項 = new SQLUI.SQL_DataGridView();
            this.rJ_Pannel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.BorderRadius = 10;
            this.rJ_Pannel1.BorderSize = 2;
            this.rJ_Pannel1.Controls.Add(this.rJ_Button_搜尋);
            this.rJ_Pannel1.Controls.Add(this.comboBox_搜尋內容);
            this.rJ_Pannel1.Controls.Add(this.comboBox_搜尋條件);
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable1);
            this.rJ_Pannel1.Controls.Add(this.dateTimeIntervelPicker_報表日期);
            this.rJ_Pannel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(4, 28);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 3;
            this.rJ_Pannel1.Size = new System.Drawing.Size(1668, 155);
            this.rJ_Pannel1.TabIndex = 3;
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
            this.rJ_Button_搜尋.Location = new System.Drawing.Point(822, 57);
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
            this.comboBox_搜尋內容.Items.AddRange(new object[] {
            "藥碼",
            "藥名",
            "IP",
            "區域"});
            this.comboBox_搜尋內容.Location = new System.Drawing.Point(523, 64);
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
            "藥名",
            "申領單位"});
            this.comboBox_搜尋條件.Location = new System.Drawing.Point(378, 64);
            this.comboBox_搜尋條件.Name = "comboBox_搜尋條件";
            this.comboBox_搜尋條件.Size = new System.Drawing.Size(130, 39);
            this.comboBox_搜尋條件.TabIndex = 125;
            this.comboBox_搜尋條件.SelectedIndexChanged += new System.EventHandler(this.comboBox_搜尋條件_SelectedIndexChanged);
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.LightGray;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(24, 12);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 0;
            this.rJ_Lable1.Size = new System.Drawing.Size(319, 40);
            this.rJ_Lable1.TabIndex = 1;
            this.rJ_Lable1.Text = "報表日期";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.Black;
            // 
            // dateTimeIntervelPicker_報表日期
            // 
            this.dateTimeIntervelPicker_報表日期.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeIntervelPicker_報表日期.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeIntervelPicker_報表日期.DateFont = new System.Drawing.Font("微軟正黑體", 14F);
            this.dateTimeIntervelPicker_報表日期.DateSize = new System.Drawing.Size(217, 39);
            this.dateTimeIntervelPicker_報表日期.EndTime = new System.DateTime(2024, 6, 26, 23, 59, 59, 0);
            this.dateTimeIntervelPicker_報表日期.Location = new System.Drawing.Point(28, 55);
            this.dateTimeIntervelPicker_報表日期.Name = "dateTimeIntervelPicker_報表日期";
            this.dateTimeIntervelPicker_報表日期.Padding = new System.Windows.Forms.Padding(2);
            this.dateTimeIntervelPicker_報表日期.Size = new System.Drawing.Size(315, 85);
            this.dateTimeIntervelPicker_報表日期.StartTime = new System.DateTime(2024, 6, 26, 0, 0, 0, 0);
            this.dateTimeIntervelPicker_報表日期.TabIndex = 0;
            this.dateTimeIntervelPicker_報表日期.TitleFont = new System.Drawing.Font("新細明體", 9F);
            this.dateTimeIntervelPicker_報表日期.TiTleSize = new System.Drawing.Size(33, 39);
            // 
            // sqL_DataGridView_申領品項
            // 
            this.sqL_DataGridView_申領品項.AutoSelectToDeep = false;
            this.sqL_DataGridView_申領品項.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_申領品項.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_申領品項.BorderRadius = 0;
            this.sqL_DataGridView_申領品項.BorderSize = 0;
            this.sqL_DataGridView_申領品項.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_申領品項.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_申領品項.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_申領品項.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_申領品項.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_申領品項.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_申領品項.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_申領品項.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_申領品項.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_申領品項.columnHeadersHeight = 18;
            this.sqL_DataGridView_申領品項.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_申領品項.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_申領品項.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_申領品項.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_申領品項.ImageBox = false;
            this.sqL_DataGridView_申領品項.Location = new System.Drawing.Point(4, 183);
            this.sqL_DataGridView_申領品項.Name = "sqL_DataGridView_申領品項";
            this.sqL_DataGridView_申領品項.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_申領品項.Password = "user82822040";
            this.sqL_DataGridView_申領品項.Port = ((uint)(3306u));
            this.sqL_DataGridView_申領品項.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_申領品項.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_申領品項.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_申領品項.RowsHeight = 40;
            this.sqL_DataGridView_申領品項.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_申領品項.selectedBorderSize = 0;
            this.sqL_DataGridView_申領品項.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_申領品項.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_申領品項.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_申領品項.Server = "127.0.0.0";
            this.sqL_DataGridView_申領品項.Size = new System.Drawing.Size(1668, 870);
            this.sqL_DataGridView_申領品項.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_申領品項.TabIndex = 11;
            this.sqL_DataGridView_申領品項.UserName = "root";
            this.sqL_DataGridView_申領品項.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_申領品項.可選擇多列 = true;
            this.sqL_DataGridView_申領品項.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_申領品項.自動換行 = true;
            this.sqL_DataGridView_申領品項.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_申領品項.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_申領品項.顯示CheckBox = false;
            this.sqL_DataGridView_申領品項.顯示首列 = true;
            this.sqL_DataGridView_申領品項.顯示首行 = true;
            this.sqL_DataGridView_申領品項.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_申領品項.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // Dialog_申領
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1676, 1057);
            this.ControlBox = true;
            this.Controls.Add(this.sqL_DataGridView_申領品項);
            this.Controls.Add(this.rJ_Pannel1);
            this.MinimizeBox = true;
            this.Name = "Dialog_申領";
            this.ShowInTaskbar = true;
            this.Text = "申領";
            this.rJ_Pannel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_Pannel rJ_Pannel1;
        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.DateTimeIntervelPicker dateTimeIntervelPicker_報表日期;
        private MyUI.RJ_Button rJ_Button_搜尋;
        private System.Windows.Forms.ComboBox comboBox_搜尋內容;
        private System.Windows.Forms.ComboBox comboBox_搜尋條件;
        private SQLUI.SQL_DataGridView sqL_DataGridView_申領品項;
    }
}