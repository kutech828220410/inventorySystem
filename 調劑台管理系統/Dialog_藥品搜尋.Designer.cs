
namespace 調劑台管理系統
{
    partial class Dialog_藥品搜尋
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_藥品搜尋 = new SQLUI.SQL_DataGridView();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.rJ_Button_返回 = new MyUI.RJ_Button();
            this.comboBox_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.rJ_Button_搜尋 = new MyUI.RJ_Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_Button_搜尋);
            this.panel1.Controls.Add(this.comboBox_搜尋條件);
            this.panel1.Controls.Add(this.rJ_Button_返回);
            this.panel1.Controls.Add(this.rJ_Button_確認);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 683);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1060, 100);
            this.panel1.TabIndex = 122;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1060, 12);
            this.panel2.TabIndex = 123;
            // 
            // sqL_DataGridView_藥品搜尋
            // 
            this.sqL_DataGridView_藥品搜尋.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥品搜尋.backColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥品搜尋.BorderColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥品搜尋.BorderRadius = 0;
            this.sqL_DataGridView_藥品搜尋.BorderSize = 2;
            this.sqL_DataGridView_藥品搜尋.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_藥品搜尋.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品搜尋.cellStylBackColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_藥品搜尋.cellStyleFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品搜尋.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品搜尋.columnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_藥品搜尋.columnHeaderBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_藥品搜尋.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品搜尋.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品搜尋.columnHeadersHeight = 15;
            this.sqL_DataGridView_藥品搜尋.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_藥品搜尋.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_藥品搜尋.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_藥品搜尋.Font = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_藥品搜尋.ImageBox = false;
            this.sqL_DataGridView_藥品搜尋.Location = new System.Drawing.Point(4, 40);
            this.sqL_DataGridView_藥品搜尋.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sqL_DataGridView_藥品搜尋.Name = "sqL_DataGridView_藥品搜尋";
            this.sqL_DataGridView_藥品搜尋.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥品搜尋.Password = "user82822040";
            this.sqL_DataGridView_藥品搜尋.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥品搜尋.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_藥品搜尋.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品搜尋.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_藥品搜尋.RowsHeight = 50;
            this.sqL_DataGridView_藥品搜尋.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥品搜尋.selectedBorderSize = 0;
            this.sqL_DataGridView_藥品搜尋.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品搜尋.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品搜尋.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品搜尋.Server = "localhost";
            this.sqL_DataGridView_藥品搜尋.Size = new System.Drawing.Size(1060, 643);
            this.sqL_DataGridView_藥品搜尋.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品搜尋.TabIndex = 124;
            this.sqL_DataGridView_藥品搜尋.UserName = "root";
            this.sqL_DataGridView_藥品搜尋.可拖曳欄位寬度 = true;
            this.sqL_DataGridView_藥品搜尋.可選擇多列 = true;
            this.sqL_DataGridView_藥品搜尋.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品搜尋.自動換行 = true;
            this.sqL_DataGridView_藥品搜尋.表單字體 = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_藥品搜尋.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_藥品搜尋.顯示CheckBox = false;
            this.sqL_DataGridView_藥品搜尋.顯示首列 = true;
            this.sqL_DataGridView_藥品搜尋.顯示首行 = true;
            this.sqL_DataGridView_藥品搜尋.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品搜尋.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認.BorderRadius = 10;
            this.rJ_Button_確認.BorderSize = 0;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(891, 0);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(169, 100);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 9;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_返回
            // 
            this.rJ_Button_返回.AutoResetState = false;
            this.rJ_Button_返回.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_返回.BackgroundColor = System.Drawing.Color.Gray;
            this.rJ_Button_返回.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_返回.BorderRadius = 10;
            this.rJ_Button_返回.BorderSize = 0;
            this.rJ_Button_返回.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_返回.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_返回.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_返回.FlatAppearance.BorderSize = 0;
            this.rJ_Button_返回.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_返回.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_返回.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_返回.GUID = "";
            this.rJ_Button_返回.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_返回.Location = new System.Drawing.Point(722, 0);
            this.rJ_Button_返回.Name = "rJ_Button_返回";
            this.rJ_Button_返回.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_返回.ProhibitionLineWidth = 4;
            this.rJ_Button_返回.ProhibitionSymbolSize = 30;
            this.rJ_Button_返回.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_返回.ShadowSize = 3;
            this.rJ_Button_返回.ShowLoadingForm = false;
            this.rJ_Button_返回.Size = new System.Drawing.Size(169, 100);
            this.rJ_Button_返回.State = false;
            this.rJ_Button_返回.TabIndex = 10;
            this.rJ_Button_返回.Text = "返回";
            this.rJ_Button_返回.TextColor = System.Drawing.Color.White;
            this.rJ_Button_返回.TextHeight = 0;
            this.rJ_Button_返回.UseVisualStyleBackColor = false;
            // 
            // comboBox_搜尋條件
            // 
            this.comboBox_搜尋條件.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_搜尋條件.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_搜尋條件.FormattingEnabled = true;
            this.comboBox_搜尋條件.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox_搜尋條件.Location = new System.Drawing.Point(24, 35);
            this.comboBox_搜尋條件.Name = "comboBox_搜尋條件";
            this.comboBox_搜尋條件.Size = new System.Drawing.Size(202, 35);
            this.comboBox_搜尋條件.TabIndex = 11;
            // 
            // rJ_Button_搜尋
            // 
            this.rJ_Button_搜尋.AutoResetState = false;
            this.rJ_Button_搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_搜尋.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_搜尋.BorderRadius = 10;
            this.rJ_Button_搜尋.BorderSize = 0;
            this.rJ_Button_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_搜尋.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_搜尋.GUID = "";
            this.rJ_Button_搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_搜尋.Location = new System.Drawing.Point(242, 19);
            this.rJ_Button_搜尋.Name = "rJ_Button_搜尋";
            this.rJ_Button_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_搜尋.ShadowSize = 3;
            this.rJ_Button_搜尋.ShowLoadingForm = false;
            this.rJ_Button_搜尋.Size = new System.Drawing.Size(119, 63);
            this.rJ_Button_搜尋.State = false;
            this.rJ_Button_搜尋.TabIndex = 12;
            this.rJ_Button_搜尋.Text = "搜尋";
            this.rJ_Button_搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_搜尋.TextHeight = 0;
            this.rJ_Button_搜尋.UseVisualStyleBackColor = false;
            // 
            // Dialog_藥品搜尋
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1068, 787);
            this.Controls.Add(this.sqL_DataGridView_藥品搜尋);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_藥品搜尋";
            this.Text = "藥品搜尋";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品搜尋;
        private MyUI.RJ_Button rJ_Button_返回;
        private MyUI.RJ_Button rJ_Button_確認;
        private System.Windows.Forms.ComboBox comboBox_搜尋條件;
        private MyUI.RJ_Button rJ_Button_搜尋;
    }
}