namespace 調劑台管理系統
{
    partial class Dialog_手輸醫令
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.rJ_Lable_病房號 = new MyUI.RJ_Lable();
            this.rJ_Lable_領藥號 = new MyUI.RJ_Lable();
            this.rJ_Lable_病人姓名 = new MyUI.RJ_Lable();
            this.rJ_Lable_病歷號 = new MyUI.RJ_Lable();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rJ_Lable_領退藥狀態 = new MyUI.RJ_Lable();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.rJ_Button_退出 = new MyUI.RJ_Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rJ_GroupBox2 = new MyUI.RJ_GroupBox();
            this.sqL_DataGridView_選擇藥品 = new SQLUI.SQL_DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Button1 = new MyUI.RJ_Button();
            this.rJ_GroupBox1 = new MyUI.RJ_GroupBox();
            this.sqL_DataGridView_藥品資料 = new SQLUI.SQL_DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_Button_處方資訊填寫 = new MyUI.RJ_Button();
            this.rJ_Button_藥品資料_選擇藥品 = new MyUI.RJ_Button();
            this.rJ_TextBox_藥品資料_藥品名稱 = new MyUI.RJ_TextBox();
            this.rJ_TextBox_藥品資料_藥品碼 = new MyUI.RJ_TextBox();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.rJ_Button_藥品資料_藥品名稱_搜尋 = new MyUI.RJ_Button();
            this.rJ_Lable26 = new MyUI.RJ_Lable();
            this.rJ_Button_藥品資料_藥品碼_搜尋 = new MyUI.RJ_Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.rJ_GroupBox2.ContentsPanel.SuspendLayout();
            this.rJ_GroupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.rJ_GroupBox1.ContentsPanel.SuspendLayout();
            this.rJ_GroupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.rJ_Lable_領退藥狀態);
            this.panel1.Controls.Add(this.rJ_Button_確認);
            this.panel1.Controls.Add(this.rJ_Button_退出);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 864);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1417, 86);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rJ_Lable_病房號);
            this.panel5.Controls.Add(this.rJ_Lable_領藥號);
            this.panel5.Controls.Add(this.rJ_Lable_病人姓名);
            this.panel5.Controls.Add(this.rJ_Lable_病歷號);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(185, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(902, 42);
            this.panel5.TabIndex = 32;
            // 
            // rJ_Lable_病房號
            // 
            this.rJ_Lable_病房號.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_病房號.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable_病房號.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_病房號.BorderRadius = 8;
            this.rJ_Lable_病房號.BorderSize = 0;
            this.rJ_Lable_病房號.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_病房號.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_病房號.Font = new System.Drawing.Font("新細明體", 12F);
            this.rJ_Lable_病房號.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable_病房號.GUID = "";
            this.rJ_Lable_病房號.Location = new System.Drawing.Point(667, 0);
            this.rJ_Lable_病房號.Name = "rJ_Lable_病房號";
            this.rJ_Lable_病房號.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_病房號.ShadowSize = 0;
            this.rJ_Lable_病房號.Size = new System.Drawing.Size(217, 42);
            this.rJ_Lable_病房號.TabIndex = 50;
            this.rJ_Lable_病房號.Text = "病房號:無";
            this.rJ_Lable_病房號.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_病房號.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_領藥號
            // 
            this.rJ_Lable_領藥號.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_領藥號.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable_領藥號.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_領藥號.BorderRadius = 8;
            this.rJ_Lable_領藥號.BorderSize = 0;
            this.rJ_Lable_領藥號.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_領藥號.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_領藥號.Font = new System.Drawing.Font("新細明體", 12F);
            this.rJ_Lable_領藥號.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable_領藥號.GUID = "";
            this.rJ_Lable_領藥號.Location = new System.Drawing.Point(450, 0);
            this.rJ_Lable_領藥號.Name = "rJ_Lable_領藥號";
            this.rJ_Lable_領藥號.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_領藥號.ShadowSize = 0;
            this.rJ_Lable_領藥號.Size = new System.Drawing.Size(217, 42);
            this.rJ_Lable_領藥號.TabIndex = 49;
            this.rJ_Lable_領藥號.Text = "領藥號:無";
            this.rJ_Lable_領藥號.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_領藥號.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_病人姓名
            // 
            this.rJ_Lable_病人姓名.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_病人姓名.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable_病人姓名.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_病人姓名.BorderRadius = 8;
            this.rJ_Lable_病人姓名.BorderSize = 0;
            this.rJ_Lable_病人姓名.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_病人姓名.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_病人姓名.Font = new System.Drawing.Font("新細明體", 12F);
            this.rJ_Lable_病人姓名.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable_病人姓名.GUID = "";
            this.rJ_Lable_病人姓名.Location = new System.Drawing.Point(233, 0);
            this.rJ_Lable_病人姓名.Name = "rJ_Lable_病人姓名";
            this.rJ_Lable_病人姓名.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_病人姓名.ShadowSize = 0;
            this.rJ_Lable_病人姓名.Size = new System.Drawing.Size(217, 42);
            this.rJ_Lable_病人姓名.TabIndex = 48;
            this.rJ_Lable_病人姓名.Text = "病人姓名:無";
            this.rJ_Lable_病人姓名.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_病人姓名.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_病歷號
            // 
            this.rJ_Lable_病歷號.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_病歷號.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable_病歷號.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_病歷號.BorderRadius = 8;
            this.rJ_Lable_病歷號.BorderSize = 0;
            this.rJ_Lable_病歷號.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_病歷號.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_病歷號.Font = new System.Drawing.Font("新細明體", 12F);
            this.rJ_Lable_病歷號.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable_病歷號.GUID = "";
            this.rJ_Lable_病歷號.Location = new System.Drawing.Point(16, 0);
            this.rJ_Lable_病歷號.Name = "rJ_Lable_病歷號";
            this.rJ_Lable_病歷號.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_病歷號.ShadowSize = 0;
            this.rJ_Lable_病歷號.Size = new System.Drawing.Size(217, 42);
            this.rJ_Lable_病歷號.TabIndex = 47;
            this.rJ_Lable_病歷號.Text = "病歷號:無";
            this.rJ_Lable_病歷號.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_病歷號.TextColor = System.Drawing.Color.Black;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(16, 42);
            this.panel6.TabIndex = 0;
            // 
            // rJ_Lable_領退藥狀態
            // 
            this.rJ_Lable_領退藥狀態.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_領退藥狀態.BackgroundColor = System.Drawing.Color.Red;
            this.rJ_Lable_領退藥狀態.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_領退藥狀態.BorderRadius = 12;
            this.rJ_Lable_領退藥狀態.BorderSize = 0;
            this.rJ_Lable_領退藥狀態.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_領退藥狀態.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_領退藥狀態.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_領退藥狀態.ForeColor = System.Drawing.Color.White;
            this.rJ_Lable_領退藥狀態.GUID = "";
            this.rJ_Lable_領退藥狀態.Location = new System.Drawing.Point(25, 0);
            this.rJ_Lable_領退藥狀態.Name = "rJ_Lable_領退藥狀態";
            this.rJ_Lable_領退藥狀態.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_領退藥狀態.ShadowSize = 0;
            this.rJ_Lable_領退藥狀態.Size = new System.Drawing.Size(160, 86);
            this.rJ_Lable_領退藥狀態.TabIndex = 30;
            this.rJ_Lable_領退藥狀態.Text = "領藥";
            this.rJ_Lable_領退藥狀態.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_領退藥狀態.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.Color.White;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認.BorderRadius = 10;
            this.rJ_Button_確認.BorderSize = 0;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(1087, 0);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(165, 86);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 29;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_退出
            // 
            this.rJ_Button_退出.AutoResetState = false;
            this.rJ_Button_退出.BackColor = System.Drawing.Color.White;
            this.rJ_Button_退出.BackgroundColor = System.Drawing.Color.Gray;
            this.rJ_Button_退出.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_退出.BorderRadius = 10;
            this.rJ_Button_退出.BorderSize = 0;
            this.rJ_Button_退出.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_退出.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_退出.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_退出.FlatAppearance.BorderSize = 0;
            this.rJ_Button_退出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_退出.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_退出.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_退出.GUID = "";
            this.rJ_Button_退出.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_退出.Location = new System.Drawing.Point(1252, 0);
            this.rJ_Button_退出.Name = "rJ_Button_退出";
            this.rJ_Button_退出.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_退出.ProhibitionLineWidth = 4;
            this.rJ_Button_退出.ProhibitionSymbolSize = 30;
            this.rJ_Button_退出.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_退出.ShadowSize = 3;
            this.rJ_Button_退出.ShowLoadingForm = false;
            this.rJ_Button_退出.Size = new System.Drawing.Size(165, 86);
            this.rJ_Button_退出.State = false;
            this.rJ_Button_退出.TabIndex = 19;
            this.rJ_Button_退出.Text = "退出";
            this.rJ_Button_退出.TextColor = System.Drawing.Color.White;
            this.rJ_Button_退出.TextHeight = 0;
            this.rJ_Button_退出.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(25, 86);
            this.panel4.TabIndex = 31;
            // 
            // rJ_GroupBox2
            // 
            // 
            // rJ_GroupBox2.ContentsPanel
            // 
            this.rJ_GroupBox2.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.rJ_GroupBox2.ContentsPanel.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_GroupBox2.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox2.ContentsPanel.BorderRadius = 5;
            this.rJ_GroupBox2.ContentsPanel.BorderSize = 2;
            this.rJ_GroupBox2.ContentsPanel.Controls.Add(this.sqL_DataGridView_選擇藥品);
            this.rJ_GroupBox2.ContentsPanel.Controls.Add(this.panel3);
            this.rJ_GroupBox2.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox2.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox2.ContentsPanel.IsSelected = false;
            this.rJ_GroupBox2.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.rJ_GroupBox2.ContentsPanel.Name = "ContentsPanel";
            this.rJ_GroupBox2.ContentsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.rJ_GroupBox2.ContentsPanel.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_GroupBox2.ContentsPanel.ShadowSize = 0;
            this.rJ_GroupBox2.ContentsPanel.Size = new System.Drawing.Size(765, 799);
            this.rJ_GroupBox2.ContentsPanel.TabIndex = 2;
            this.rJ_GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox2.GUID = "";
            this.rJ_GroupBox2.Location = new System.Drawing.Point(656, 28);
            this.rJ_GroupBox2.Name = "rJ_GroupBox2";
            this.rJ_GroupBox2.PannelBackColor = System.Drawing.Color.White;
            this.rJ_GroupBox2.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox2.PannelBorderRadius = 5;
            this.rJ_GroupBox2.PannelBorderSize = 2;
            this.rJ_GroupBox2.Size = new System.Drawing.Size(765, 836);
            this.rJ_GroupBox2.TabIndex = 2;
            this.rJ_GroupBox2.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_GroupBox2.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_GroupBox2.TitleBorderRadius = 5;
            this.rJ_GroupBox2.TitleBorderSize = 0;
            this.rJ_GroupBox2.TitleFont = new System.Drawing.Font("新細明體", 12F);
            this.rJ_GroupBox2.TitleForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox2.TitleHeight = 37;
            this.rJ_GroupBox2.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_GroupBox2.TitleTexts = "選擇藥品";
            // 
            // sqL_DataGridView_選擇藥品
            // 
            this.sqL_DataGridView_選擇藥品.AutoSelectToDeep = false;
            this.sqL_DataGridView_選擇藥品.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_選擇藥品.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_選擇藥品.BorderRadius = 0;
            this.sqL_DataGridView_選擇藥品.BorderSize = 2;
            this.sqL_DataGridView_選擇藥品.CellBorderColor = System.Drawing.Color.White;
            this.sqL_DataGridView_選擇藥品.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_選擇藥品.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_選擇藥品.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_選擇藥品.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_選擇藥品.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_選擇藥品.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_選擇藥品.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_選擇藥品.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_選擇藥品.columnHeadersHeight = 18;
            this.sqL_DataGridView_選擇藥品.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_選擇藥品.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sqL_DataGridView_選擇藥品.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_選擇藥品.Font = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_選擇藥品.ImageBox = false;
            this.sqL_DataGridView_選擇藥品.Location = new System.Drawing.Point(5, 5);
            this.sqL_DataGridView_選擇藥品.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sqL_DataGridView_選擇藥品.Name = "sqL_DataGridView_選擇藥品";
            this.sqL_DataGridView_選擇藥品.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_選擇藥品.Password = "user82822040";
            this.sqL_DataGridView_選擇藥品.Port = ((uint)(3306u));
            this.sqL_DataGridView_選擇藥品.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_選擇藥品.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_選擇藥品.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_選擇藥品.RowsHeight = 50;
            this.sqL_DataGridView_選擇藥品.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_選擇藥品.selectedBorderSize = 0;
            this.sqL_DataGridView_選擇藥品.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_選擇藥品.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_選擇藥品.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_選擇藥品.Server = "localhost";
            this.sqL_DataGridView_選擇藥品.Size = new System.Drawing.Size(755, 674);
            this.sqL_DataGridView_選擇藥品.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_選擇藥品.TabIndex = 116;
            this.sqL_DataGridView_選擇藥品.UserName = "root";
            this.sqL_DataGridView_選擇藥品.可拖曳欄位寬度 = true;
            this.sqL_DataGridView_選擇藥品.可選擇多列 = true;
            this.sqL_DataGridView_選擇藥品.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_選擇藥品.自動換行 = true;
            this.sqL_DataGridView_選擇藥品.表單字體 = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_選擇藥品.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_選擇藥品.顯示CheckBox = false;
            this.sqL_DataGridView_選擇藥品.顯示首列 = true;
            this.sqL_DataGridView_選擇藥品.顯示首行 = true;
            this.sqL_DataGridView_選擇藥品.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_選擇藥品.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rJ_Button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(5, 679);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(755, 115);
            this.panel3.TabIndex = 0;
            // 
            // rJ_Button1
            // 
            this.rJ_Button1.AutoResetState = false;
            this.rJ_Button1.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button1.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button1.BorderRadius = 20;
            this.rJ_Button1.BorderSize = 0;
            this.rJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button1.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button1.FlatAppearance.BorderSize = 0;
            this.rJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button1.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button1.ForeColor = System.Drawing.Color.White;
            this.rJ_Button1.GUID = "";
            this.rJ_Button1.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button1.Location = new System.Drawing.Point(530, 12);
            this.rJ_Button1.Name = "rJ_Button1";
            this.rJ_Button1.ProhibitionBorderLineWidth = 1;
            this.rJ_Button1.ProhibitionLineWidth = 4;
            this.rJ_Button1.ProhibitionSymbolSize = 30;
            this.rJ_Button1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button1.ShadowSize = 3;
            this.rJ_Button1.ShowLoadingForm = false;
            this.rJ_Button1.Size = new System.Drawing.Size(213, 88);
            this.rJ_Button1.State = false;
            this.rJ_Button1.TabIndex = 53;
            this.rJ_Button1.Text = "刪除選取資料";
            this.rJ_Button1.TextColor = System.Drawing.Color.White;
            this.rJ_Button1.TextHeight = 0;
            this.rJ_Button1.UseVisualStyleBackColor = false;
            // 
            // rJ_GroupBox1
            // 
            // 
            // rJ_GroupBox1.ContentsPanel
            // 
            this.rJ_GroupBox1.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.ContentsPanel.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_GroupBox1.ContentsPanel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox1.ContentsPanel.BorderRadius = 5;
            this.rJ_GroupBox1.ContentsPanel.BorderSize = 2;
            this.rJ_GroupBox1.ContentsPanel.Controls.Add(this.sqL_DataGridView_藥品資料);
            this.rJ_GroupBox1.ContentsPanel.Controls.Add(this.panel2);
            this.rJ_GroupBox1.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_GroupBox1.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.ContentsPanel.IsSelected = false;
            this.rJ_GroupBox1.ContentsPanel.Location = new System.Drawing.Point(0, 37);
            this.rJ_GroupBox1.ContentsPanel.Name = "ContentsPanel";
            this.rJ_GroupBox1.ContentsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.rJ_GroupBox1.ContentsPanel.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_GroupBox1.ContentsPanel.ShadowSize = 0;
            this.rJ_GroupBox1.ContentsPanel.Size = new System.Drawing.Size(652, 799);
            this.rJ_GroupBox1.ContentsPanel.TabIndex = 2;
            this.rJ_GroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_GroupBox1.GUID = "";
            this.rJ_GroupBox1.Location = new System.Drawing.Point(4, 28);
            this.rJ_GroupBox1.Name = "rJ_GroupBox1";
            this.rJ_GroupBox1.PannelBackColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.PannelBorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_GroupBox1.PannelBorderRadius = 5;
            this.rJ_GroupBox1.PannelBorderSize = 2;
            this.rJ_GroupBox1.Size = new System.Drawing.Size(652, 836);
            this.rJ_GroupBox1.TabIndex = 1;
            this.rJ_GroupBox1.TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_GroupBox1.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_GroupBox1.TitleBorderRadius = 5;
            this.rJ_GroupBox1.TitleBorderSize = 0;
            this.rJ_GroupBox1.TitleFont = new System.Drawing.Font("新細明體", 12F);
            this.rJ_GroupBox1.TitleForeColor = System.Drawing.Color.White;
            this.rJ_GroupBox1.TitleHeight = 37;
            this.rJ_GroupBox1.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_GroupBox1.TitleTexts = "藥品資料";
            // 
            // sqL_DataGridView_藥品資料
            // 
            this.sqL_DataGridView_藥品資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥品資料.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.BorderRadius = 0;
            this.sqL_DataGridView_藥品資料.BorderSize = 2;
            this.sqL_DataGridView_藥品資料.CellBorderColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品資料.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_藥品資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥品資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥品資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.columnHeadersHeight = 18;
            this.sqL_DataGridView_藥品資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_藥品資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sqL_DataGridView_藥品資料.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_藥品資料.Font = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_藥品資料.ImageBox = false;
            this.sqL_DataGridView_藥品資料.Location = new System.Drawing.Point(5, 5);
            this.sqL_DataGridView_藥品資料.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sqL_DataGridView_藥品資料.Name = "sqL_DataGridView_藥品資料";
            this.sqL_DataGridView_藥品資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥品資料.Password = "user82822040";
            this.sqL_DataGridView_藥品資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥品資料.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_藥品資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_藥品資料.RowsHeight = 50;
            this.sqL_DataGridView_藥品資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥品資料.selectedBorderSize = 0;
            this.sqL_DataGridView_藥品資料.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品資料.Server = "localhost";
            this.sqL_DataGridView_藥品資料.Size = new System.Drawing.Size(642, 601);
            this.sqL_DataGridView_藥品資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品資料.TabIndex = 115;
            this.sqL_DataGridView_藥品資料.UserName = "root";
            this.sqL_DataGridView_藥品資料.可拖曳欄位寬度 = true;
            this.sqL_DataGridView_藥品資料.可選擇多列 = true;
            this.sqL_DataGridView_藥品資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品資料.自動換行 = true;
            this.sqL_DataGridView_藥品資料.表單字體 = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_藥品資料.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_藥品資料.顯示CheckBox = false;
            this.sqL_DataGridView_藥品資料.顯示首列 = true;
            this.sqL_DataGridView_藥品資料.顯示首行 = true;
            this.sqL_DataGridView_藥品資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rJ_Button_處方資訊填寫);
            this.panel2.Controls.Add(this.rJ_Button_藥品資料_選擇藥品);
            this.panel2.Controls.Add(this.rJ_TextBox_藥品資料_藥品名稱);
            this.panel2.Controls.Add(this.rJ_TextBox_藥品資料_藥品碼);
            this.panel2.Controls.Add(this.rJ_Lable1);
            this.panel2.Controls.Add(this.rJ_Button_藥品資料_藥品名稱_搜尋);
            this.panel2.Controls.Add(this.rJ_Lable26);
            this.panel2.Controls.Add(this.rJ_Button_藥品資料_藥品碼_搜尋);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(5, 606);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(642, 188);
            this.panel2.TabIndex = 116;
            // 
            // rJ_Button_處方資訊填寫
            // 
            this.rJ_Button_處方資訊填寫.AutoResetState = false;
            this.rJ_Button_處方資訊填寫.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_處方資訊填寫.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_處方資訊填寫.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_處方資訊填寫.BorderRadius = 20;
            this.rJ_Button_處方資訊填寫.BorderSize = 0;
            this.rJ_Button_處方資訊填寫.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_處方資訊填寫.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_處方資訊填寫.FlatAppearance.BorderSize = 0;
            this.rJ_Button_處方資訊填寫.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_處方資訊填寫.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button_處方資訊填寫.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_處方資訊填寫.GUID = "";
            this.rJ_Button_處方資訊填寫.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_處方資訊填寫.Location = new System.Drawing.Point(460, 99);
            this.rJ_Button_處方資訊填寫.Name = "rJ_Button_處方資訊填寫";
            this.rJ_Button_處方資訊填寫.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_處方資訊填寫.ProhibitionLineWidth = 4;
            this.rJ_Button_處方資訊填寫.ProhibitionSymbolSize = 30;
            this.rJ_Button_處方資訊填寫.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_處方資訊填寫.ShadowSize = 3;
            this.rJ_Button_處方資訊填寫.ShowLoadingForm = false;
            this.rJ_Button_處方資訊填寫.Size = new System.Drawing.Size(167, 88);
            this.rJ_Button_處方資訊填寫.State = false;
            this.rJ_Button_處方資訊填寫.TabIndex = 53;
            this.rJ_Button_處方資訊填寫.Text = "處方資訊填寫";
            this.rJ_Button_處方資訊填寫.TextColor = System.Drawing.Color.White;
            this.rJ_Button_處方資訊填寫.TextHeight = 0;
            this.rJ_Button_處方資訊填寫.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_藥品資料_選擇藥品
            // 
            this.rJ_Button_藥品資料_選擇藥品.AutoResetState = false;
            this.rJ_Button_藥品資料_選擇藥品.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品資料_選擇藥品.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_藥品資料_選擇藥品.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品資料_選擇藥品.BorderRadius = 20;
            this.rJ_Button_藥品資料_選擇藥品.BorderSize = 0;
            this.rJ_Button_藥品資料_選擇藥品.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品資料_選擇藥品.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品資料_選擇藥品.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品資料_選擇藥品.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品資料_選擇藥品.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button_藥品資料_選擇藥品.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_選擇藥品.GUID = "";
            this.rJ_Button_藥品資料_選擇藥品.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品資料_選擇藥品.Location = new System.Drawing.Point(460, 7);
            this.rJ_Button_藥品資料_選擇藥品.Name = "rJ_Button_藥品資料_選擇藥品";
            this.rJ_Button_藥品資料_選擇藥品.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品資料_選擇藥品.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品資料_選擇藥品.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品資料_選擇藥品.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品資料_選擇藥品.ShadowSize = 3;
            this.rJ_Button_藥品資料_選擇藥品.ShowLoadingForm = false;
            this.rJ_Button_藥品資料_選擇藥品.Size = new System.Drawing.Size(167, 88);
            this.rJ_Button_藥品資料_選擇藥品.State = false;
            this.rJ_Button_藥品資料_選擇藥品.TabIndex = 52;
            this.rJ_Button_藥品資料_選擇藥品.Text = "選擇藥品";
            this.rJ_Button_藥品資料_選擇藥品.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_選擇藥品.TextHeight = 0;
            this.rJ_Button_藥品資料_選擇藥品.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_藥品資料_藥品名稱
            // 
            this.rJ_TextBox_藥品資料_藥品名稱.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥品資料_藥品名稱.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_TextBox_藥品資料_藥品名稱.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥品資料_藥品名稱.BorderRadius = 0;
            this.rJ_TextBox_藥品資料_藥品名稱.BorderSize = 2;
            this.rJ_TextBox_藥品資料_藥品名稱.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.rJ_TextBox_藥品資料_藥品名稱.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥品資料_藥品名稱.GUID = "";
            this.rJ_TextBox_藥品資料_藥品名稱.Location = new System.Drawing.Point(135, 64);
            this.rJ_TextBox_藥品資料_藥品名稱.Multiline = false;
            this.rJ_TextBox_藥品資料_藥品名稱.Name = "rJ_TextBox_藥品資料_藥品名稱";
            this.rJ_TextBox_藥品資料_藥品名稱.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥品資料_藥品名稱.PassWordChar = false;
            this.rJ_TextBox_藥品資料_藥品名稱.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品資料_藥品名稱.PlaceholderText = "";
            this.rJ_TextBox_藥品資料_藥品名稱.ShowTouchPannel = false;
            this.rJ_TextBox_藥品資料_藥品名稱.Size = new System.Drawing.Size(149, 36);
            this.rJ_TextBox_藥品資料_藥品名稱.TabIndex = 51;
            this.rJ_TextBox_藥品資料_藥品名稱.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥品資料_藥品名稱.Texts = "";
            this.rJ_TextBox_藥品資料_藥品名稱.UnderlineStyle = false;
            // 
            // rJ_TextBox_藥品資料_藥品碼
            // 
            this.rJ_TextBox_藥品資料_藥品碼.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥品資料_藥品碼.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_TextBox_藥品資料_藥品碼.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥品資料_藥品碼.BorderRadius = 0;
            this.rJ_TextBox_藥品資料_藥品碼.BorderSize = 2;
            this.rJ_TextBox_藥品資料_藥品碼.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.rJ_TextBox_藥品資料_藥品碼.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥品資料_藥品碼.GUID = "";
            this.rJ_TextBox_藥品資料_藥品碼.Location = new System.Drawing.Point(135, 12);
            this.rJ_TextBox_藥品資料_藥品碼.Multiline = false;
            this.rJ_TextBox_藥品資料_藥品碼.Name = "rJ_TextBox_藥品資料_藥品碼";
            this.rJ_TextBox_藥品資料_藥品碼.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥品資料_藥品碼.PassWordChar = false;
            this.rJ_TextBox_藥品資料_藥品碼.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥品資料_藥品碼.PlaceholderText = "";
            this.rJ_TextBox_藥品資料_藥品碼.ShowTouchPannel = false;
            this.rJ_TextBox_藥品資料_藥品碼.Size = new System.Drawing.Size(149, 36);
            this.rJ_TextBox_藥品資料_藥品碼.TabIndex = 50;
            this.rJ_TextBox_藥品資料_藥品碼.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥品資料_藥品碼.Texts = "";
            this.rJ_TextBox_藥品資料_藥品碼.UnderlineStyle = false;
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 8;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("新細明體", 12F);
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(7, 59);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 0;
            this.rJ_Lable1.Size = new System.Drawing.Size(122, 46);
            this.rJ_Lable1.TabIndex = 48;
            this.rJ_Lable1.Text = "藥品名稱";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Button_藥品資料_藥品名稱_搜尋
            // 
            this.rJ_Button_藥品資料_藥品名稱_搜尋.AutoResetState = false;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.BorderRadius = 20;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.BorderSize = 0;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button_藥品資料_藥品名稱_搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.GUID = "";
            this.rJ_Button_藥品資料_藥品名稱_搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品資料_藥品名稱_搜尋.Location = new System.Drawing.Point(290, 59);
            this.rJ_Button_藥品資料_藥品名稱_搜尋.Name = "rJ_Button_藥品資料_藥品名稱_搜尋";
            this.rJ_Button_藥品資料_藥品名稱_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.ShadowSize = 3;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.Size = new System.Drawing.Size(106, 46);
            this.rJ_Button_藥品資料_藥品名稱_搜尋.State = false;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.TabIndex = 47;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.Text = "搜尋";
            this.rJ_Button_藥品資料_藥品名稱_搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.TextHeight = 0;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.UseVisualStyleBackColor = false;
            // 
            // rJ_Lable26
            // 
            this.rJ_Lable26.BackColor = System.Drawing.Color.White;
            this.rJ_Lable26.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable26.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable26.BorderRadius = 8;
            this.rJ_Lable26.BorderSize = 0;
            this.rJ_Lable26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable26.Font = new System.Drawing.Font("新細明體", 12F);
            this.rJ_Lable26.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable26.GUID = "";
            this.rJ_Lable26.Location = new System.Drawing.Point(7, 7);
            this.rJ_Lable26.Name = "rJ_Lable26";
            this.rJ_Lable26.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable26.ShadowSize = 0;
            this.rJ_Lable26.Size = new System.Drawing.Size(122, 46);
            this.rJ_Lable26.TabIndex = 45;
            this.rJ_Lable26.Text = "藥品碼";
            this.rJ_Lable26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable26.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Button_藥品資料_藥品碼_搜尋
            // 
            this.rJ_Button_藥品資料_藥品碼_搜尋.AutoResetState = false;
            this.rJ_Button_藥品資料_藥品碼_搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品資料_藥品碼_搜尋.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_藥品資料_藥品碼_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品資料_藥品碼_搜尋.BorderRadius = 20;
            this.rJ_Button_藥品資料_藥品碼_搜尋.BorderSize = 0;
            this.rJ_Button_藥品資料_藥品碼_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品資料_藥品碼_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品資料_藥品碼_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品資料_藥品碼_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品資料_藥品碼_搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button_藥品資料_藥品碼_搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_藥品碼_搜尋.GUID = "";
            this.rJ_Button_藥品資料_藥品碼_搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品資料_藥品碼_搜尋.Location = new System.Drawing.Point(290, 7);
            this.rJ_Button_藥品資料_藥品碼_搜尋.Name = "rJ_Button_藥品資料_藥品碼_搜尋";
            this.rJ_Button_藥品資料_藥品碼_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品資料_藥品碼_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品資料_藥品碼_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品資料_藥品碼_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品資料_藥品碼_搜尋.ShadowSize = 3;
            this.rJ_Button_藥品資料_藥品碼_搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥品資料_藥品碼_搜尋.Size = new System.Drawing.Size(106, 46);
            this.rJ_Button_藥品資料_藥品碼_搜尋.State = false;
            this.rJ_Button_藥品資料_藥品碼_搜尋.TabIndex = 30;
            this.rJ_Button_藥品資料_藥品碼_搜尋.Text = "搜尋";
            this.rJ_Button_藥品資料_藥品碼_搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品資料_藥品碼_搜尋.TextHeight = 0;
            this.rJ_Button_藥品資料_藥品碼_搜尋.UseVisualStyleBackColor = false;
            // 
            // Dialog_手輸醫令
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1425, 954);
            this.Controls.Add(this.rJ_GroupBox2);
            this.Controls.Add(this.rJ_GroupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_手輸醫令";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Dialog_手動作業_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.rJ_GroupBox2.ContentsPanel.ResumeLayout(false);
            this.rJ_GroupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.rJ_GroupBox1.ContentsPanel.ResumeLayout(false);
            this.rJ_GroupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyUI.RJ_Button rJ_Button_退出;
        private MyUI.RJ_Button rJ_Button_確認;
        private MyUI.RJ_GroupBox rJ_GroupBox1;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品資料;
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_Button rJ_Button_藥品資料_藥品碼_搜尋;
        private MyUI.RJ_Lable rJ_Lable26;
        private MyUI.RJ_TextBox rJ_TextBox_藥品資料_藥品名稱;
        private MyUI.RJ_TextBox rJ_TextBox_藥品資料_藥品碼;
        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.RJ_Button rJ_Button_藥品資料_藥品名稱_搜尋;
        private MyUI.RJ_Button rJ_Button_藥品資料_選擇藥品;
        private MyUI.RJ_GroupBox rJ_GroupBox2;
        private SQLUI.SQL_DataGridView sqL_DataGridView_選擇藥品;
        private System.Windows.Forms.Panel panel3;
        private MyUI.RJ_Button rJ_Button1;
        private MyUI.RJ_Lable rJ_Lable_領退藥狀態;
        private System.Windows.Forms.Panel panel4;
        private MyUI.RJ_Button rJ_Button_處方資訊填寫;
        private System.Windows.Forms.Panel panel5;
        private MyUI.RJ_Lable rJ_Lable_病房號;
        private MyUI.RJ_Lable rJ_Lable_領藥號;
        private MyUI.RJ_Lable rJ_Lable_病人姓名;
        private MyUI.RJ_Lable rJ_Lable_病歷號;
        private System.Windows.Forms.Panel panel6;
    }
}