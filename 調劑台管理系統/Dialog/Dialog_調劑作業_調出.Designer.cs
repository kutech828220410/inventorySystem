
namespace 調劑台管理系統
{
    partial class Dialog_調劑作業_調出
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
            this.comboBox_目的調劑台名稱 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rJ_Lable_來源調劑台名稱 = new MyUI.RJ_Lable();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_Button_取消 = new MyUI.RJ_Button();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_庫儲藥品 = new SQLUI.SQL_DataGridView();
            this.comboBox_庫儲藥品_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.rJ_TextBox1 = new MyUI.RJ_TextBox();
            this.rJ_Button_庫儲藥品_搜尋 = new MyUI.RJ_Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.rJ_Pannel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_目的調劑台名稱
            // 
            this.comboBox_目的調劑台名稱.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_目的調劑台名稱.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_目的調劑台名稱.FormattingEnabled = true;
            this.comboBox_目的調劑台名稱.Location = new System.Drawing.Point(371, 36);
            this.comboBox_目的調劑台名稱.Name = "comboBox_目的調劑台名稱";
            this.comboBox_目的調劑台名稱.Size = new System.Drawing.Size(240, 40);
            this.comboBox_目的調劑台名稱.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::調劑台管理系統.Properties.Resources.arrow_right;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(255, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 75);
            this.panel1.TabIndex = 1;
            // 
            // rJ_Lable_來源調劑台名稱
            // 
            this.rJ_Lable_來源調劑台名稱.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_來源調劑台名稱.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable_來源調劑台名稱.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable_來源調劑台名稱.BorderRadius = 10;
            this.rJ_Lable_來源調劑台名稱.BorderSize = 1;
            this.rJ_Lable_來源調劑台名稱.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_來源調劑台名稱.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold);
            this.rJ_Lable_來源調劑台名稱.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_來源調劑台名稱.GUID = "";
            this.rJ_Lable_來源調劑台名稱.Location = new System.Drawing.Point(20, 37);
            this.rJ_Lable_來源調劑台名稱.Name = "rJ_Lable_來源調劑台名稱";
            this.rJ_Lable_來源調劑台名稱.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_來源調劑台名稱.ShadowSize = 0;
            this.rJ_Lable_來源調劑台名稱.Size = new System.Drawing.Size(229, 40);
            this.rJ_Lable_來源調劑台名稱.TabIndex = 2;
            this.rJ_Lable_來源調劑台名稱.Text = "------";
            this.rJ_Lable_來源調劑台名稱.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_來源調劑台名稱.TextColor = System.Drawing.Color.Black;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "來源";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(360, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "目的";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rJ_Button_取消);
            this.panel2.Controls.Add(this.rJ_Button_確認);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(4, 896);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1192, 100);
            this.panel2.TabIndex = 5;
            // 
            // rJ_Button_取消
            // 
            this.rJ_Button_取消.AutoResetState = false;
            this.rJ_Button_取消.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_取消.BackgroundColor = System.Drawing.Color.Gray;
            this.rJ_Button_取消.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_取消.BorderRadius = 10;
            this.rJ_Button_取消.BorderSize = 0;
            this.rJ_Button_取消.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_取消.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_取消.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_取消.FlatAppearance.BorderSize = 0;
            this.rJ_Button_取消.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_取消.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_取消.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_取消.GUID = "";
            this.rJ_Button_取消.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_取消.Location = new System.Drawing.Point(848, 0);
            this.rJ_Button_取消.Name = "rJ_Button_取消";
            this.rJ_Button_取消.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_取消.ProhibitionLineWidth = 4;
            this.rJ_Button_取消.ProhibitionSymbolSize = 30;
            this.rJ_Button_取消.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_取消.ShadowSize = 3;
            this.rJ_Button_取消.ShowLoadingForm = false;
            this.rJ_Button_取消.Size = new System.Drawing.Size(172, 100);
            this.rJ_Button_取消.State = false;
            this.rJ_Button_取消.TabIndex = 3;
            this.rJ_Button_取消.Text = "取消";
            this.rJ_Button_取消.TextColor = System.Drawing.Color.White;
            this.rJ_Button_取消.TextHeight = 0;
            this.rJ_Button_取消.UseVisualStyleBackColor = false;
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
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(1020, 0);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(172, 100);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 2;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.comboBox_目的調劑台名稱);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.rJ_Lable_來源調劑台名稱);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1192, 107);
            this.panel3.TabIndex = 6;
            // 
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.BorderRadius = 5;
            this.rJ_Pannel1.BorderSize = 2;
            this.rJ_Pannel1.Controls.Add(this.sqL_DataGridView_庫儲藥品);
            this.rJ_Pannel1.Controls.Add(this.panel4);
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable1);
            this.rJ_Pannel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(4, 135);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 3;
            this.rJ_Pannel1.Size = new System.Drawing.Size(659, 761);
            this.rJ_Pannel1.TabIndex = 8;
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(10, 5);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 0;
            this.rJ_Lable1.Size = new System.Drawing.Size(639, 78);
            this.rJ_Lable1.TabIndex = 0;
            this.rJ_Lable1.Text = "庫 儲 藥 品";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.White;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rJ_Button_庫儲藥品_搜尋);
            this.panel4.Controls.Add(this.rJ_TextBox1);
            this.panel4.Controls.Add(this.comboBox_庫儲藥品_搜尋條件);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(10, 652);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(639, 99);
            this.panel4.TabIndex = 117;
            // 
            // sqL_DataGridView_庫儲藥品
            // 
            this.sqL_DataGridView_庫儲藥品.AutoSelectToDeep = false;
            this.sqL_DataGridView_庫儲藥品.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_庫儲藥品.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_庫儲藥品.BorderRadius = 0;
            this.sqL_DataGridView_庫儲藥品.BorderSize = 2;
            this.sqL_DataGridView_庫儲藥品.CellBorderColor = System.Drawing.Color.White;
            this.sqL_DataGridView_庫儲藥品.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_庫儲藥品.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_庫儲藥品.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_庫儲藥品.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_庫儲藥品.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_庫儲藥品.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_庫儲藥品.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_庫儲藥品.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_庫儲藥品.columnHeadersHeight = 15;
            this.sqL_DataGridView_庫儲藥品.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_庫儲藥品.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sqL_DataGridView_庫儲藥品.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_庫儲藥品.Font = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_庫儲藥品.ImageBox = false;
            this.sqL_DataGridView_庫儲藥品.Location = new System.Drawing.Point(10, 83);
            this.sqL_DataGridView_庫儲藥品.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sqL_DataGridView_庫儲藥品.Name = "sqL_DataGridView_庫儲藥品";
            this.sqL_DataGridView_庫儲藥品.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_庫儲藥品.Password = "user82822040";
            this.sqL_DataGridView_庫儲藥品.Port = ((uint)(3306u));
            this.sqL_DataGridView_庫儲藥品.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_庫儲藥品.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_庫儲藥品.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_庫儲藥品.RowsHeight = 50;
            this.sqL_DataGridView_庫儲藥品.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_庫儲藥品.selectedBorderSize = 0;
            this.sqL_DataGridView_庫儲藥品.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_庫儲藥品.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_庫儲藥品.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_庫儲藥品.Server = "localhost";
            this.sqL_DataGridView_庫儲藥品.Size = new System.Drawing.Size(639, 569);
            this.sqL_DataGridView_庫儲藥品.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_庫儲藥品.TabIndex = 118;
            this.sqL_DataGridView_庫儲藥品.UserName = "root";
            this.sqL_DataGridView_庫儲藥品.可拖曳欄位寬度 = true;
            this.sqL_DataGridView_庫儲藥品.可選擇多列 = true;
            this.sqL_DataGridView_庫儲藥品.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_庫儲藥品.自動換行 = true;
            this.sqL_DataGridView_庫儲藥品.表單字體 = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_庫儲藥品.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_庫儲藥品.顯示CheckBox = false;
            this.sqL_DataGridView_庫儲藥品.顯示首列 = true;
            this.sqL_DataGridView_庫儲藥品.顯示首行 = true;
            this.sqL_DataGridView_庫儲藥品.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_庫儲藥品.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // comboBox_庫儲藥品_搜尋條件
            // 
            this.comboBox_庫儲藥品_搜尋條件.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_庫儲藥品_搜尋條件.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_庫儲藥品_搜尋條件.FormattingEnabled = true;
            this.comboBox_庫儲藥品_搜尋條件.Items.AddRange(new object[] {
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox_庫儲藥品_搜尋條件.Location = new System.Drawing.Point(16, 30);
            this.comboBox_庫儲藥品_搜尋條件.Name = "comboBox_庫儲藥品_搜尋條件";
            this.comboBox_庫儲藥品_搜尋條件.Size = new System.Drawing.Size(130, 40);
            this.comboBox_庫儲藥品_搜尋條件.TabIndex = 0;
            // 
            // rJ_TextBox1
            // 
            this.rJ_TextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox1.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox1.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox1.BorderRadius = 0;
            this.rJ_TextBox1.BorderSize = 2;
            this.rJ_TextBox1.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox1.GUID = "";
            this.rJ_TextBox1.Location = new System.Drawing.Point(152, 29);
            this.rJ_TextBox1.Multiline = false;
            this.rJ_TextBox1.Name = "rJ_TextBox1";
            this.rJ_TextBox1.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox1.PassWordChar = false;
            this.rJ_TextBox1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox1.PlaceholderText = "";
            this.rJ_TextBox1.ShowTouchPannel = false;
            this.rJ_TextBox1.Size = new System.Drawing.Size(250, 43);
            this.rJ_TextBox1.TabIndex = 1;
            this.rJ_TextBox1.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox1.Texts = "";
            this.rJ_TextBox1.UnderlineStyle = false;
            // 
            // rJ_Button_庫儲藥品_搜尋
            // 
            this.rJ_Button_庫儲藥品_搜尋.AutoResetState = false;
            this.rJ_Button_庫儲藥品_搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_庫儲藥品_搜尋.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_庫儲藥品_搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_庫儲藥品_搜尋.BorderRadius = 10;
            this.rJ_Button_庫儲藥品_搜尋.BorderSize = 0;
            this.rJ_Button_庫儲藥品_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_庫儲藥品_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_庫儲藥品_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_庫儲藥品_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_庫儲藥品_搜尋.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_庫儲藥品_搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_庫儲藥品_搜尋.GUID = "";
            this.rJ_Button_庫儲藥品_搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_庫儲藥品_搜尋.Location = new System.Drawing.Point(408, 15);
            this.rJ_Button_庫儲藥品_搜尋.Name = "rJ_Button_庫儲藥品_搜尋";
            this.rJ_Button_庫儲藥品_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_庫儲藥品_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_庫儲藥品_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_庫儲藥品_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_庫儲藥品_搜尋.ShadowSize = 3;
            this.rJ_Button_庫儲藥品_搜尋.ShowLoadingForm = false;
            this.rJ_Button_庫儲藥品_搜尋.Size = new System.Drawing.Size(103, 70);
            this.rJ_Button_庫儲藥品_搜尋.State = false;
            this.rJ_Button_庫儲藥品_搜尋.TabIndex = 3;
            this.rJ_Button_庫儲藥品_搜尋.Text = "搜尋";
            this.rJ_Button_庫儲藥品_搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_庫儲藥品_搜尋.TextHeight = 0;
            this.rJ_Button_庫儲藥品_搜尋.UseVisualStyleBackColor = false;
            // 
            // Dialog_調劑作業_調出
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 1000);
            this.Controls.Add(this.rJ_Pannel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "Dialog_調劑作業_調出";
            this.Text = "調出";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.rJ_Pannel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_目的調劑台名稱;
        private System.Windows.Forms.Panel panel1;
        private MyUI.RJ_Lable rJ_Lable_來源調劑台名稱;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_Button rJ_Button_取消;
        private MyUI.RJ_Button rJ_Button_確認;
        private System.Windows.Forms.Panel panel3;
        private MyUI.RJ_Pannel rJ_Pannel1;
        private MyUI.RJ_Lable rJ_Lable1;
        private SQLUI.SQL_DataGridView sqL_DataGridView_庫儲藥品;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox comboBox_庫儲藥品_搜尋條件;
        private MyUI.RJ_Button rJ_Button_庫儲藥品_搜尋;
        private MyUI.RJ_TextBox rJ_TextBox1;
    }
}