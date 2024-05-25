
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
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.sqL_DataGridView_層架列表 = new SQLUI.SQL_DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabPage1.Controls.Add(this.rJ_Pannel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1264, 832);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "層架燈";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.rJ_Pannel1.Size = new System.Drawing.Size(250, 832);
            this.rJ_Pannel1.TabIndex = 1;
            // 
            // sqL_DataGridView_層架列表
            // 
            this.sqL_DataGridView_層架列表.AutoSelectToDeep = false;
            this.sqL_DataGridView_層架列表.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_層架列表.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_層架列表.BorderRadius = 0;
            this.sqL_DataGridView_層架列表.BorderSize = 2;
            this.sqL_DataGridView_層架列表.CellBorderColor = System.Drawing.Color.White;
            this.sqL_DataGridView_層架列表.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_層架列表.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_層架列表.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_層架列表.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_層架列表.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_層架列表.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_層架列表.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_層架列表.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_層架列表.columnHeadersHeight = 4;
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
            this.sqL_DataGridView_層架列表.RowsColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_層架列表.RowsHeight = 10;
            this.sqL_DataGridView_層架列表.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_層架列表.selectedBorderSize = 0;
            this.sqL_DataGridView_層架列表.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_層架列表.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_層架列表.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_層架列表.Server = "127.0.0.0";
            this.sqL_DataGridView_層架列表.Size = new System.Drawing.Size(250, 832);
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
    }
}