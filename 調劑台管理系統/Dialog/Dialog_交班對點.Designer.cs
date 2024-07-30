
namespace 調劑台管理系統
{
    partial class Dialog_交班對點
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
            this.stepViewer = new MyUI.StepViewer();
            this.comboBox_藥品群組 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Button_藥品群組_選擇 = new MyUI.RJ_Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_交班藥品 = new SQLUI.SQL_DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.rJ_Button13 = new MyUI.RJ_Button();
            this.rJ_Button12 = new MyUI.RJ_Button();
            this.rJ_TextBox_盤點庫存 = new MyUI.RJ_TextBox();
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.rJ_Button1 = new MyUI.RJ_Button();
            this.rJ_Button11 = new MyUI.RJ_Button();
            this.rJ_Button6 = new MyUI.RJ_Button();
            this.rJ_Button7 = new MyUI.RJ_Button();
            this.rJ_Button2 = new MyUI.RJ_Button();
            this.rJ_Button5 = new MyUI.RJ_Button();
            this.rJ_Button8 = new MyUI.RJ_Button();
            this.rJ_Button10 = new MyUI.RJ_Button();
            this.rJ_Button4 = new MyUI.RJ_Button();
            this.rJ_Button9 = new MyUI.RJ_Button();
            this.rJ_Button3 = new MyUI.RJ_Button();
            this.rJ_Lable3 = new MyUI.RJ_Lable();
            this.rJ_Lable_現有庫存 = new MyUI.RJ_Lable();
            this.pictureBox_藥品資訊 = new System.Windows.Forms.PictureBox();
            this.rJ_Lable_藥品資訊 = new MyUI.RJ_Lable();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.rJ_Pannel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品資訊)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1692, 16);
            this.panel1.TabIndex = 0;
            // 
            // stepViewer
            // 
            this.stepViewer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.stepViewer.CurrentStep = 0;
            this.stepViewer.Dock = System.Windows.Forms.DockStyle.Top;
            this.stepViewer.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stepViewer.LineWidth = 120;
            this.stepViewer.ListDataSource = null;
            this.stepViewer.Location = new System.Drawing.Point(4, 44);
            this.stepViewer.Margin = new System.Windows.Forms.Padding(4);
            this.stepViewer.Name = "stepViewer";
            this.stepViewer.Size = new System.Drawing.Size(1692, 108);
            this.stepViewer.TabIndex = 7;
            // 
            // comboBox_藥品群組
            // 
            this.comboBox_藥品群組.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_藥品群組.FormattingEnabled = true;
            this.comboBox_藥品群組.Location = new System.Drawing.Point(186, 28);
            this.comboBox_藥品群組.Name = "comboBox_藥品群組";
            this.comboBox_藥品群組.Size = new System.Drawing.Size(378, 44);
            this.comboBox_藥品群組.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1692, 100);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rJ_Button_藥品群組_選擇);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.comboBox_藥品群組);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1692, 100);
            this.panel3.TabIndex = 10;
            // 
            // rJ_Button_藥品群組_選擇
            // 
            this.rJ_Button_藥品群組_選擇.AutoResetState = false;
            this.rJ_Button_藥品群組_選擇.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品群組_選擇.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_選擇.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品群組_選擇.BorderRadius = 20;
            this.rJ_Button_藥品群組_選擇.BorderSize = 0;
            this.rJ_Button_藥品群組_選擇.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品群組_選擇.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品群組_選擇.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品群組_選擇.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品群組_選擇.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥品群組_選擇.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_選擇.GUID = "";
            this.rJ_Button_藥品群組_選擇.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥品群組_選擇.Location = new System.Drawing.Point(580, 20);
            this.rJ_Button_藥品群組_選擇.Name = "rJ_Button_藥品群組_選擇";
            this.rJ_Button_藥品群組_選擇.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品群組_選擇.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品群組_選擇.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品群組_選擇.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品群組_選擇.ShadowSize = 3;
            this.rJ_Button_藥品群組_選擇.ShowLoadingForm = false;
            this.rJ_Button_藥品群組_選擇.Size = new System.Drawing.Size(119, 63);
            this.rJ_Button_藥品群組_選擇.State = false;
            this.rJ_Button_藥品群組_選擇.TabIndex = 10;
            this.rJ_Button_藥品群組_選擇.Text = "選擇";
            this.rJ_Button_藥品群組_選擇.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_選擇.TextHeight = 0;
            this.rJ_Button_藥品群組_選擇.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(29, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "藥品群組:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.sqL_DataGridView_交班藥品);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(4, 252);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(674, 664);
            this.panel4.TabIndex = 11;
            // 
            // sqL_DataGridView_交班藥品
            // 
            this.sqL_DataGridView_交班藥品.AutoSelectToDeep = false;
            this.sqL_DataGridView_交班藥品.backColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_交班藥品.BorderColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_交班藥品.BorderRadius = 0;
            this.sqL_DataGridView_交班藥品.BorderSize = 2;
            this.sqL_DataGridView_交班藥品.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_交班藥品.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_交班藥品.cellStylBackColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_交班藥品.cellStyleFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_交班藥品.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_交班藥品.columnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_交班藥品.columnHeaderBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_交班藥品.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_交班藥品.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_交班藥品.columnHeadersHeight = 15;
            this.sqL_DataGridView_交班藥品.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_交班藥品.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_交班藥品.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_交班藥品.Font = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_交班藥品.ImageBox = false;
            this.sqL_DataGridView_交班藥品.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_交班藥品.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sqL_DataGridView_交班藥品.Name = "sqL_DataGridView_交班藥品";
            this.sqL_DataGridView_交班藥品.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_交班藥品.Password = "user82822040";
            this.sqL_DataGridView_交班藥品.Port = ((uint)(3306u));
            this.sqL_DataGridView_交班藥品.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_交班藥品.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_交班藥品.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_交班藥品.RowsHeight = 50;
            this.sqL_DataGridView_交班藥品.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_交班藥品.selectedBorderSize = 0;
            this.sqL_DataGridView_交班藥品.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_交班藥品.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_交班藥品.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_交班藥品.Server = "localhost";
            this.sqL_DataGridView_交班藥品.Size = new System.Drawing.Size(674, 664);
            this.sqL_DataGridView_交班藥品.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_交班藥品.TabIndex = 121;
            this.sqL_DataGridView_交班藥品.UserName = "root";
            this.sqL_DataGridView_交班藥品.可拖曳欄位寬度 = true;
            this.sqL_DataGridView_交班藥品.可選擇多列 = true;
            this.sqL_DataGridView_交班藥品.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_交班藥品.自動換行 = true;
            this.sqL_DataGridView_交班藥品.表單字體 = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_交班藥品.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_交班藥品.顯示CheckBox = false;
            this.sqL_DataGridView_交班藥品.顯示首列 = true;
            this.sqL_DataGridView_交班藥品.顯示首行 = true;
            this.sqL_DataGridView_交班藥品.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_交班藥品.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rJ_Lable1);
            this.panel5.Controls.Add(this.rJ_Button13);
            this.panel5.Controls.Add(this.rJ_Button12);
            this.panel5.Controls.Add(this.rJ_TextBox_盤點庫存);
            this.panel5.Controls.Add(this.rJ_Pannel1);
            this.panel5.Controls.Add(this.rJ_Lable3);
            this.panel5.Controls.Add(this.rJ_Lable_現有庫存);
            this.panel5.Controls.Add(this.pictureBox_藥品資訊);
            this.panel5.Controls.Add(this.rJ_Lable_藥品資訊);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(678, 252);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1018, 664);
            this.panel5.TabIndex = 12;
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 1;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 30F);
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(583, 117);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 0;
            this.rJ_Lable1.Size = new System.Drawing.Size(209, 72);
            this.rJ_Lable1.TabIndex = 33;
            this.rJ_Lable1.Text = "現有庫存";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Button13
            // 
            this.rJ_Button13.AutoResetState = false;
            this.rJ_Button13.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button13.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button13.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button13.BorderRadius = 20;
            this.rJ_Button13.BorderSize = 0;
            this.rJ_Button13.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button13.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button13.FlatAppearance.BorderSize = 0;
            this.rJ_Button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button13.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button13.ForeColor = System.Drawing.Color.White;
            this.rJ_Button13.GUID = "";
            this.rJ_Button13.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button13.Location = new System.Drawing.Point(207, 548);
            this.rJ_Button13.Name = "rJ_Button13";
            this.rJ_Button13.ProhibitionBorderLineWidth = 1;
            this.rJ_Button13.ProhibitionLineWidth = 4;
            this.rJ_Button13.ProhibitionSymbolSize = 30;
            this.rJ_Button13.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button13.ShadowSize = 3;
            this.rJ_Button13.ShowLoadingForm = false;
            this.rJ_Button13.Size = new System.Drawing.Size(168, 90);
            this.rJ_Button13.State = false;
            this.rJ_Button13.TabIndex = 32;
            this.rJ_Button13.Text = "下一筆";
            this.rJ_Button13.TextColor = System.Drawing.Color.White;
            this.rJ_Button13.TextHeight = 0;
            this.rJ_Button13.UseVisualStyleBackColor = false;
            // 
            // rJ_Button12
            // 
            this.rJ_Button12.AutoResetState = false;
            this.rJ_Button12.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button12.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button12.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button12.BorderRadius = 20;
            this.rJ_Button12.BorderSize = 0;
            this.rJ_Button12.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button12.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button12.FlatAppearance.BorderSize = 0;
            this.rJ_Button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button12.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button12.ForeColor = System.Drawing.Color.White;
            this.rJ_Button12.GUID = "";
            this.rJ_Button12.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button12.Location = new System.Drawing.Point(381, 548);
            this.rJ_Button12.Name = "rJ_Button12";
            this.rJ_Button12.ProhibitionBorderLineWidth = 1;
            this.rJ_Button12.ProhibitionLineWidth = 4;
            this.rJ_Button12.ProhibitionSymbolSize = 30;
            this.rJ_Button12.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button12.ShadowSize = 3;
            this.rJ_Button12.ShowLoadingForm = false;
            this.rJ_Button12.Size = new System.Drawing.Size(168, 90);
            this.rJ_Button12.State = false;
            this.rJ_Button12.TabIndex = 31;
            this.rJ_Button12.Text = "確認輸入";
            this.rJ_Button12.TextColor = System.Drawing.Color.White;
            this.rJ_Button12.TextHeight = 0;
            this.rJ_Button12.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_盤點庫存
            // 
            this.rJ_TextBox_盤點庫存.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_盤點庫存.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_盤點庫存.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_盤點庫存.BorderRadius = 0;
            this.rJ_TextBox_盤點庫存.BorderSize = 2;
            this.rJ_TextBox_盤點庫存.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_盤點庫存.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_盤點庫存.GUID = "";
            this.rJ_TextBox_盤點庫存.Location = new System.Drawing.Point(805, 202);
            this.rJ_TextBox_盤點庫存.Multiline = false;
            this.rJ_TextBox_盤點庫存.Name = "rJ_TextBox_盤點庫存";
            this.rJ_TextBox_盤點庫存.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_盤點庫存.PassWordChar = false;
            this.rJ_TextBox_盤點庫存.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_盤點庫存.PlaceholderText = "";
            this.rJ_TextBox_盤點庫存.ShowTouchPannel = false;
            this.rJ_TextBox_盤點庫存.Size = new System.Drawing.Size(134, 56);
            this.rJ_TextBox_盤點庫存.TabIndex = 30;
            this.rJ_TextBox_盤點庫存.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_盤點庫存.Texts = "";
            this.rJ_TextBox_盤點庫存.UnderlineStyle = false;
            // 
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.BorderRadius = 50;
            this.rJ_Pannel1.BorderSize = 1;
            this.rJ_Pannel1.Controls.Add(this.rJ_Button1);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button11);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button6);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button7);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button2);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button5);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button8);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button10);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button4);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button9);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button3);
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(578, 281);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 0;
            this.rJ_Pannel1.Size = new System.Drawing.Size(423, 375);
            this.rJ_Pannel1.TabIndex = 20;
            // 
            // rJ_Button1
            // 
            this.rJ_Button1.AutoResetState = false;
            this.rJ_Button1.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button1.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button1.BorderRadius = 10;
            this.rJ_Button1.BorderSize = 0;
            this.rJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button1.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button1.FlatAppearance.BorderSize = 0;
            this.rJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button1.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button1.ForeColor = System.Drawing.Color.White;
            this.rJ_Button1.GUID = "";
            this.rJ_Button1.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button1.Location = new System.Drawing.Point(84, 19);
            this.rJ_Button1.Name = "rJ_Button1";
            this.rJ_Button1.ProhibitionBorderLineWidth = 1;
            this.rJ_Button1.ProhibitionLineWidth = 4;
            this.rJ_Button1.ProhibitionSymbolSize = 30;
            this.rJ_Button1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button1.ShadowSize = 0;
            this.rJ_Button1.ShowLoadingForm = false;
            this.rJ_Button1.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button1.State = false;
            this.rJ_Button1.TabIndex = 19;
            this.rJ_Button1.Text = "1";
            this.rJ_Button1.TextColor = System.Drawing.Color.White;
            this.rJ_Button1.TextHeight = 0;
            this.rJ_Button1.UseVisualStyleBackColor = false;
            // 
            // rJ_Button11
            // 
            this.rJ_Button11.AutoResetState = false;
            this.rJ_Button11.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button11.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button11.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button11.BorderRadius = 10;
            this.rJ_Button11.BorderSize = 0;
            this.rJ_Button11.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button11.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button11.FlatAppearance.BorderSize = 0;
            this.rJ_Button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button11.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button11.ForeColor = System.Drawing.Color.White;
            this.rJ_Button11.GUID = "";
            this.rJ_Button11.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button11.Location = new System.Drawing.Point(256, 277);
            this.rJ_Button11.Name = "rJ_Button11";
            this.rJ_Button11.ProhibitionBorderLineWidth = 1;
            this.rJ_Button11.ProhibitionLineWidth = 4;
            this.rJ_Button11.ProhibitionSymbolSize = 30;
            this.rJ_Button11.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button11.ShadowSize = 0;
            this.rJ_Button11.ShowLoadingForm = false;
            this.rJ_Button11.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button11.State = false;
            this.rJ_Button11.TabIndex = 29;
            this.rJ_Button11.Text = "CE";
            this.rJ_Button11.TextColor = System.Drawing.Color.White;
            this.rJ_Button11.TextHeight = 0;
            this.rJ_Button11.UseVisualStyleBackColor = false;
            // 
            // rJ_Button6
            // 
            this.rJ_Button6.AutoResetState = false;
            this.rJ_Button6.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button6.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button6.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button6.BorderRadius = 10;
            this.rJ_Button6.BorderSize = 0;
            this.rJ_Button6.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button6.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button6.FlatAppearance.BorderSize = 0;
            this.rJ_Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button6.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button6.ForeColor = System.Drawing.Color.White;
            this.rJ_Button6.GUID = "";
            this.rJ_Button6.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button6.Location = new System.Drawing.Point(256, 105);
            this.rJ_Button6.Name = "rJ_Button6";
            this.rJ_Button6.ProhibitionBorderLineWidth = 1;
            this.rJ_Button6.ProhibitionLineWidth = 4;
            this.rJ_Button6.ProhibitionSymbolSize = 30;
            this.rJ_Button6.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button6.ShadowSize = 0;
            this.rJ_Button6.ShowLoadingForm = false;
            this.rJ_Button6.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button6.State = false;
            this.rJ_Button6.TabIndex = 24;
            this.rJ_Button6.Text = "6";
            this.rJ_Button6.TextColor = System.Drawing.Color.White;
            this.rJ_Button6.TextHeight = 0;
            this.rJ_Button6.UseVisualStyleBackColor = false;
            // 
            // rJ_Button7
            // 
            this.rJ_Button7.AutoResetState = false;
            this.rJ_Button7.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button7.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button7.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button7.BorderRadius = 10;
            this.rJ_Button7.BorderSize = 0;
            this.rJ_Button7.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button7.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button7.FlatAppearance.BorderSize = 0;
            this.rJ_Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button7.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button7.ForeColor = System.Drawing.Color.White;
            this.rJ_Button7.GUID = "";
            this.rJ_Button7.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button7.Location = new System.Drawing.Point(84, 191);
            this.rJ_Button7.Name = "rJ_Button7";
            this.rJ_Button7.ProhibitionBorderLineWidth = 1;
            this.rJ_Button7.ProhibitionLineWidth = 4;
            this.rJ_Button7.ProhibitionSymbolSize = 30;
            this.rJ_Button7.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button7.ShadowSize = 0;
            this.rJ_Button7.ShowLoadingForm = false;
            this.rJ_Button7.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button7.State = false;
            this.rJ_Button7.TabIndex = 25;
            this.rJ_Button7.Text = "7";
            this.rJ_Button7.TextColor = System.Drawing.Color.White;
            this.rJ_Button7.TextHeight = 0;
            this.rJ_Button7.UseVisualStyleBackColor = false;
            // 
            // rJ_Button2
            // 
            this.rJ_Button2.AutoResetState = false;
            this.rJ_Button2.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button2.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button2.BorderRadius = 10;
            this.rJ_Button2.BorderSize = 0;
            this.rJ_Button2.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button2.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button2.FlatAppearance.BorderSize = 0;
            this.rJ_Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button2.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button2.ForeColor = System.Drawing.Color.White;
            this.rJ_Button2.GUID = "";
            this.rJ_Button2.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button2.Location = new System.Drawing.Point(170, 19);
            this.rJ_Button2.Name = "rJ_Button2";
            this.rJ_Button2.ProhibitionBorderLineWidth = 1;
            this.rJ_Button2.ProhibitionLineWidth = 4;
            this.rJ_Button2.ProhibitionSymbolSize = 30;
            this.rJ_Button2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button2.ShadowSize = 0;
            this.rJ_Button2.ShowLoadingForm = false;
            this.rJ_Button2.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button2.State = false;
            this.rJ_Button2.TabIndex = 20;
            this.rJ_Button2.Text = "2";
            this.rJ_Button2.TextColor = System.Drawing.Color.White;
            this.rJ_Button2.TextHeight = 0;
            this.rJ_Button2.UseVisualStyleBackColor = false;
            // 
            // rJ_Button5
            // 
            this.rJ_Button5.AutoResetState = false;
            this.rJ_Button5.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button5.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button5.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button5.BorderRadius = 10;
            this.rJ_Button5.BorderSize = 0;
            this.rJ_Button5.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button5.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button5.FlatAppearance.BorderSize = 0;
            this.rJ_Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button5.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button5.ForeColor = System.Drawing.Color.White;
            this.rJ_Button5.GUID = "";
            this.rJ_Button5.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button5.Location = new System.Drawing.Point(170, 105);
            this.rJ_Button5.Name = "rJ_Button5";
            this.rJ_Button5.ProhibitionBorderLineWidth = 1;
            this.rJ_Button5.ProhibitionLineWidth = 4;
            this.rJ_Button5.ProhibitionSymbolSize = 30;
            this.rJ_Button5.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button5.ShadowSize = 0;
            this.rJ_Button5.ShowLoadingForm = false;
            this.rJ_Button5.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button5.State = false;
            this.rJ_Button5.TabIndex = 23;
            this.rJ_Button5.Text = "5";
            this.rJ_Button5.TextColor = System.Drawing.Color.White;
            this.rJ_Button5.TextHeight = 0;
            this.rJ_Button5.UseVisualStyleBackColor = false;
            // 
            // rJ_Button8
            // 
            this.rJ_Button8.AutoResetState = false;
            this.rJ_Button8.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button8.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button8.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button8.BorderRadius = 10;
            this.rJ_Button8.BorderSize = 0;
            this.rJ_Button8.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button8.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button8.FlatAppearance.BorderSize = 0;
            this.rJ_Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button8.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button8.ForeColor = System.Drawing.Color.White;
            this.rJ_Button8.GUID = "";
            this.rJ_Button8.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button8.Location = new System.Drawing.Point(170, 191);
            this.rJ_Button8.Name = "rJ_Button8";
            this.rJ_Button8.ProhibitionBorderLineWidth = 1;
            this.rJ_Button8.ProhibitionLineWidth = 4;
            this.rJ_Button8.ProhibitionSymbolSize = 30;
            this.rJ_Button8.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button8.ShadowSize = 0;
            this.rJ_Button8.ShowLoadingForm = false;
            this.rJ_Button8.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button8.State = false;
            this.rJ_Button8.TabIndex = 26;
            this.rJ_Button8.Text = "8";
            this.rJ_Button8.TextColor = System.Drawing.Color.White;
            this.rJ_Button8.TextHeight = 0;
            this.rJ_Button8.UseVisualStyleBackColor = false;
            // 
            // rJ_Button10
            // 
            this.rJ_Button10.AutoResetState = false;
            this.rJ_Button10.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button10.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button10.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button10.BorderRadius = 10;
            this.rJ_Button10.BorderSize = 0;
            this.rJ_Button10.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button10.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button10.FlatAppearance.BorderSize = 0;
            this.rJ_Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button10.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button10.ForeColor = System.Drawing.Color.White;
            this.rJ_Button10.GUID = "";
            this.rJ_Button10.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button10.Location = new System.Drawing.Point(170, 277);
            this.rJ_Button10.Name = "rJ_Button10";
            this.rJ_Button10.ProhibitionBorderLineWidth = 1;
            this.rJ_Button10.ProhibitionLineWidth = 4;
            this.rJ_Button10.ProhibitionSymbolSize = 30;
            this.rJ_Button10.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button10.ShadowSize = 0;
            this.rJ_Button10.ShowLoadingForm = false;
            this.rJ_Button10.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button10.State = false;
            this.rJ_Button10.TabIndex = 28;
            this.rJ_Button10.Text = "0";
            this.rJ_Button10.TextColor = System.Drawing.Color.White;
            this.rJ_Button10.TextHeight = 0;
            this.rJ_Button10.UseVisualStyleBackColor = false;
            // 
            // rJ_Button4
            // 
            this.rJ_Button4.AutoResetState = false;
            this.rJ_Button4.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button4.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button4.BorderRadius = 10;
            this.rJ_Button4.BorderSize = 0;
            this.rJ_Button4.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button4.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button4.FlatAppearance.BorderSize = 0;
            this.rJ_Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button4.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button4.ForeColor = System.Drawing.Color.White;
            this.rJ_Button4.GUID = "";
            this.rJ_Button4.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button4.Location = new System.Drawing.Point(84, 105);
            this.rJ_Button4.Name = "rJ_Button4";
            this.rJ_Button4.ProhibitionBorderLineWidth = 1;
            this.rJ_Button4.ProhibitionLineWidth = 4;
            this.rJ_Button4.ProhibitionSymbolSize = 30;
            this.rJ_Button4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button4.ShadowSize = 0;
            this.rJ_Button4.ShowLoadingForm = false;
            this.rJ_Button4.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button4.State = false;
            this.rJ_Button4.TabIndex = 22;
            this.rJ_Button4.Text = "4";
            this.rJ_Button4.TextColor = System.Drawing.Color.White;
            this.rJ_Button4.TextHeight = 0;
            this.rJ_Button4.UseVisualStyleBackColor = false;
            // 
            // rJ_Button9
            // 
            this.rJ_Button9.AutoResetState = false;
            this.rJ_Button9.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button9.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button9.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button9.BorderRadius = 10;
            this.rJ_Button9.BorderSize = 0;
            this.rJ_Button9.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button9.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button9.FlatAppearance.BorderSize = 0;
            this.rJ_Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button9.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button9.ForeColor = System.Drawing.Color.White;
            this.rJ_Button9.GUID = "";
            this.rJ_Button9.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button9.Location = new System.Drawing.Point(256, 191);
            this.rJ_Button9.Name = "rJ_Button9";
            this.rJ_Button9.ProhibitionBorderLineWidth = 1;
            this.rJ_Button9.ProhibitionLineWidth = 4;
            this.rJ_Button9.ProhibitionSymbolSize = 30;
            this.rJ_Button9.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button9.ShadowSize = 0;
            this.rJ_Button9.ShowLoadingForm = false;
            this.rJ_Button9.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button9.State = false;
            this.rJ_Button9.TabIndex = 27;
            this.rJ_Button9.Text = "9";
            this.rJ_Button9.TextColor = System.Drawing.Color.White;
            this.rJ_Button9.TextHeight = 0;
            this.rJ_Button9.UseVisualStyleBackColor = false;
            // 
            // rJ_Button3
            // 
            this.rJ_Button3.AutoResetState = false;
            this.rJ_Button3.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button3.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button3.BorderRadius = 10;
            this.rJ_Button3.BorderSize = 0;
            this.rJ_Button3.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button3.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button3.FlatAppearance.BorderSize = 0;
            this.rJ_Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button3.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button3.ForeColor = System.Drawing.Color.White;
            this.rJ_Button3.GUID = "";
            this.rJ_Button3.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button3.Location = new System.Drawing.Point(256, 19);
            this.rJ_Button3.Name = "rJ_Button3";
            this.rJ_Button3.ProhibitionBorderLineWidth = 1;
            this.rJ_Button3.ProhibitionLineWidth = 4;
            this.rJ_Button3.ProhibitionSymbolSize = 30;
            this.rJ_Button3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button3.ShadowSize = 0;
            this.rJ_Button3.ShowLoadingForm = false;
            this.rJ_Button3.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button3.State = false;
            this.rJ_Button3.TabIndex = 21;
            this.rJ_Button3.Text = "3";
            this.rJ_Button3.TextColor = System.Drawing.Color.White;
            this.rJ_Button3.TextHeight = 0;
            this.rJ_Button3.UseVisualStyleBackColor = false;
            // 
            // rJ_Lable3
            // 
            this.rJ_Lable3.BackColor = System.Drawing.Color.White;
            this.rJ_Lable3.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.rJ_Lable3.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable3.BorderRadius = 10;
            this.rJ_Lable3.BorderSize = 1;
            this.rJ_Lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable3.Font = new System.Drawing.Font("微軟正黑體", 30F);
            this.rJ_Lable3.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable3.GUID = "";
            this.rJ_Lable3.Location = new System.Drawing.Point(583, 194);
            this.rJ_Lable3.Name = "rJ_Lable3";
            this.rJ_Lable3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable3.ShadowSize = 0;
            this.rJ_Lable3.Size = new System.Drawing.Size(209, 72);
            this.rJ_Lable3.TabIndex = 6;
            this.rJ_Lable3.Text = "盤點庫存";
            this.rJ_Lable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable3.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable_現有庫存
            // 
            this.rJ_Lable_現有庫存.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_現有庫存.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable_現有庫存.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_現有庫存.BorderRadius = 10;
            this.rJ_Lable_現有庫存.BorderSize = 0;
            this.rJ_Lable_現有庫存.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_現有庫存.Font = new System.Drawing.Font("微軟正黑體", 30F);
            this.rJ_Lable_現有庫存.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_現有庫存.GUID = "";
            this.rJ_Lable_現有庫存.Location = new System.Drawing.Point(805, 117);
            this.rJ_Lable_現有庫存.Name = "rJ_Lable_現有庫存";
            this.rJ_Lable_現有庫存.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_現有庫存.ShadowSize = 0;
            this.rJ_Lable_現有庫存.Size = new System.Drawing.Size(127, 72);
            this.rJ_Lable_現有庫存.TabIndex = 5;
            this.rJ_Lable_現有庫存.Text = "-----";
            this.rJ_Lable_現有庫存.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_現有庫存.TextColor = System.Drawing.Color.Black;
            // 
            // pictureBox_藥品資訊
            // 
            this.pictureBox_藥品資訊.BackColor = System.Drawing.Color.Snow;
            this.pictureBox_藥品資訊.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_藥品資訊.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_藥品資訊.Location = new System.Drawing.Point(69, 129);
            this.pictureBox_藥品資訊.Name = "pictureBox_藥品資訊";
            this.pictureBox_藥品資訊.Size = new System.Drawing.Size(480, 392);
            this.pictureBox_藥品資訊.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_藥品資訊.TabIndex = 4;
            this.pictureBox_藥品資訊.TabStop = false;
            // 
            // rJ_Lable_藥品資訊
            // 
            this.rJ_Lable_藥品資訊.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_藥品資訊.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable_藥品資訊.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_藥品資訊.BorderRadius = 10;
            this.rJ_Lable_藥品資訊.BorderSize = 0;
            this.rJ_Lable_藥品資訊.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable_藥品資訊.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_藥品資訊.Font = new System.Drawing.Font("微軟正黑體", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_藥品資訊.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_藥品資訊.GUID = "";
            this.rJ_Lable_藥品資訊.Location = new System.Drawing.Point(35, 14);
            this.rJ_Lable_藥品資訊.Name = "rJ_Lable_藥品資訊";
            this.rJ_Lable_藥品資訊.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_藥品資訊.ShadowSize = 0;
            this.rJ_Lable_藥品資訊.Size = new System.Drawing.Size(983, 96);
            this.rJ_Lable_藥品資訊.TabIndex = 3;
            this.rJ_Lable_藥品資訊.Text = "(------) -------------------------------------------";
            this.rJ_Lable_藥品資訊.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_藥品資訊.TextColor = System.Drawing.Color.Black;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 14);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(35, 650);
            this.panel7.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1018, 14);
            this.panel6.TabIndex = 1;
            // 
            // Dialog_交班對點
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1700, 920);
            this.ControlBox = true;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.stepViewer);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_交班對點";
            this.Text = "交班對點";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.rJ_Pannel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品資訊)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyUI.StepViewer stepViewer;
        private System.Windows.Forms.ComboBox comboBox_藥品群組;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private MyUI.RJ_Button rJ_Button_藥品群組_選擇;
        private SQLUI.SQL_DataGridView sqL_DataGridView_交班藥品;
        private System.Windows.Forms.PictureBox pictureBox_藥品資訊;
        private MyUI.RJ_Lable rJ_Lable_藥品資訊;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private MyUI.RJ_Lable rJ_Lable_現有庫存;
        private MyUI.RJ_Lable rJ_Lable3;
        private MyUI.RJ_Pannel rJ_Pannel1;
        private MyUI.RJ_Button rJ_Button1;
        private MyUI.RJ_Button rJ_Button11;
        private MyUI.RJ_Button rJ_Button6;
        private MyUI.RJ_Button rJ_Button7;
        private MyUI.RJ_Button rJ_Button2;
        private MyUI.RJ_Button rJ_Button5;
        private MyUI.RJ_Button rJ_Button8;
        private MyUI.RJ_Button rJ_Button10;
        private MyUI.RJ_Button rJ_Button4;
        private MyUI.RJ_Button rJ_Button9;
        private MyUI.RJ_Button rJ_Button3;
        private MyUI.RJ_TextBox rJ_TextBox_盤點庫存;
        private MyUI.RJ_Button rJ_Button13;
        private MyUI.RJ_Button rJ_Button12;
        private MyUI.RJ_Lable rJ_Lable1;
    }
}