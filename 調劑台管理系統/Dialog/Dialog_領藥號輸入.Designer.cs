﻿
namespace 調劑台管理系統
{
    partial class Dialog_領藥號輸入
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
            this.checkBox_高價藥 = new System.Windows.Forms.CheckBox();
            this.checkBox_其餘品項 = new System.Windows.Forms.CheckBox();
            this.checkBox_高警訊 = new System.Windows.Forms.CheckBox();
            this.checkBox_管4 = new System.Windows.Forms.CheckBox();
            this.checkBox_管1_3 = new System.Windows.Forms.CheckBox();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.rJ_Button_取消 = new MyUI.RJ_Button();
            this.rJ_Button_輸入 = new MyUI.RJ_Button();
            this.rJ_TextBox_領藥號 = new MyUI.RJ_TextBox();
            this.rJ_Lable26 = new MyUI.RJ_Lable();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_Button_選取處方 = new MyUI.RJ_Button();
            this.sqL_DataGridView_醫令資料 = new SQLUI.SQL_DataGridView();
            this.plC_RJ_GroupBox1 = new MyUI.PLC_RJ_GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Button_刪除 = new MyUI.RJ_Button();
            this.sqL_DataGridView_醫令資料_已選取處方 = new SQLUI.SQL_DataGridView();
            this.rJ_DatePicker_日期 = new MyUI.RJ_DatePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.plC_RJ_GroupBox1.ContentsPanel.SuspendLayout();
            this.plC_RJ_GroupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_DatePicker_日期);
            this.panel1.Controls.Add(this.checkBox_高價藥);
            this.panel1.Controls.Add(this.checkBox_其餘品項);
            this.panel1.Controls.Add(this.checkBox_高警訊);
            this.panel1.Controls.Add(this.checkBox_管4);
            this.panel1.Controls.Add(this.checkBox_管1_3);
            this.panel1.Controls.Add(this.rJ_Button_確認);
            this.panel1.Controls.Add(this.rJ_Button_取消);
            this.panel1.Controls.Add(this.rJ_Button_輸入);
            this.panel1.Controls.Add(this.rJ_TextBox_領藥號);
            this.panel1.Controls.Add(this.rJ_Lable26);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1676, 81);
            this.panel1.TabIndex = 1;
            // 
            // checkBox_高價藥
            // 
            this.checkBox_高價藥.AutoSize = true;
            this.checkBox_高價藥.Checked = true;
            this.checkBox_高價藥.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_高價藥.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_高價藥.Location = new System.Drawing.Point(1168, 25);
            this.checkBox_高價藥.Name = "checkBox_高價藥";
            this.checkBox_高價藥.Size = new System.Drawing.Size(86, 28);
            this.checkBox_高價藥.TabIndex = 62;
            this.checkBox_高價藥.Text = "高價藥";
            this.checkBox_高價藥.UseVisualStyleBackColor = true;
            // 
            // checkBox_其餘品項
            // 
            this.checkBox_其餘品項.AutoSize = true;
            this.checkBox_其餘品項.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_其餘品項.Location = new System.Drawing.Point(1260, 25);
            this.checkBox_其餘品項.Name = "checkBox_其餘品項";
            this.checkBox_其餘品項.Size = new System.Drawing.Size(105, 28);
            this.checkBox_其餘品項.TabIndex = 61;
            this.checkBox_其餘品項.Text = "其餘品項";
            this.checkBox_其餘品項.UseVisualStyleBackColor = true;
            // 
            // checkBox_高警訊
            // 
            this.checkBox_高警訊.AutoSize = true;
            this.checkBox_高警訊.Checked = true;
            this.checkBox_高警訊.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_高警訊.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_高警訊.Location = new System.Drawing.Point(1076, 25);
            this.checkBox_高警訊.Name = "checkBox_高警訊";
            this.checkBox_高警訊.Size = new System.Drawing.Size(86, 28);
            this.checkBox_高警訊.TabIndex = 60;
            this.checkBox_高警訊.Text = "高警訊";
            this.checkBox_高警訊.UseVisualStyleBackColor = true;
            // 
            // checkBox_管4
            // 
            this.checkBox_管4.AutoSize = true;
            this.checkBox_管4.Checked = true;
            this.checkBox_管4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_管4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_管4.Location = new System.Drawing.Point(1011, 25);
            this.checkBox_管4.Name = "checkBox_管4";
            this.checkBox_管4.Size = new System.Drawing.Size(59, 28);
            this.checkBox_管4.TabIndex = 59;
            this.checkBox_管4.Text = "管4";
            this.checkBox_管4.UseVisualStyleBackColor = true;
            // 
            // checkBox_管1_3
            // 
            this.checkBox_管1_3.AutoSize = true;
            this.checkBox_管1_3.Checked = true;
            this.checkBox_管1_3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_管1_3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_管1_3.Location = new System.Drawing.Point(921, 25);
            this.checkBox_管1_3.Name = "checkBox_管1_3";
            this.checkBox_管1_3.Size = new System.Drawing.Size(84, 28);
            this.checkBox_管1_3.TabIndex = 58;
            this.checkBox_管1_3.Text = "管1~3";
            this.checkBox_管1_3.UseVisualStyleBackColor = true;
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.Color.White;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認.BorderRadius = 20;
            this.rJ_Button_確認.BorderSize = 0;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(1436, 0);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(120, 81);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 57;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_取消
            // 
            this.rJ_Button_取消.AutoResetState = false;
            this.rJ_Button_取消.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_取消.BackgroundColor = System.Drawing.Color.Gray;
            this.rJ_Button_取消.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_取消.BorderRadius = 20;
            this.rJ_Button_取消.BorderSize = 0;
            this.rJ_Button_取消.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_取消.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_取消.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_取消.FlatAppearance.BorderSize = 0;
            this.rJ_Button_取消.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_取消.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_取消.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_取消.GUID = "";
            this.rJ_Button_取消.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_取消.Location = new System.Drawing.Point(1556, 0);
            this.rJ_Button_取消.Name = "rJ_Button_取消";
            this.rJ_Button_取消.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_取消.ProhibitionLineWidth = 4;
            this.rJ_Button_取消.ProhibitionSymbolSize = 30;
            this.rJ_Button_取消.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_取消.ShadowSize = 3;
            this.rJ_Button_取消.ShowLoadingForm = false;
            this.rJ_Button_取消.Size = new System.Drawing.Size(120, 81);
            this.rJ_Button_取消.State = false;
            this.rJ_Button_取消.TabIndex = 56;
            this.rJ_Button_取消.Text = "取消";
            this.rJ_Button_取消.TextColor = System.Drawing.Color.White;
            this.rJ_Button_取消.TextHeight = 0;
            this.rJ_Button_取消.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_輸入
            // 
            this.rJ_Button_輸入.AutoResetState = false;
            this.rJ_Button_輸入.BackColor = System.Drawing.Color.White;
            this.rJ_Button_輸入.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_輸入.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_輸入.BorderRadius = 20;
            this.rJ_Button_輸入.BorderSize = 0;
            this.rJ_Button_輸入.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_輸入.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_輸入.FlatAppearance.BorderSize = 0;
            this.rJ_Button_輸入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_輸入.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button_輸入.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_輸入.GUID = "";
            this.rJ_Button_輸入.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_輸入.Location = new System.Drawing.Point(784, 7);
            this.rJ_Button_輸入.Name = "rJ_Button_輸入";
            this.rJ_Button_輸入.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_輸入.ProhibitionLineWidth = 4;
            this.rJ_Button_輸入.ProhibitionSymbolSize = 30;
            this.rJ_Button_輸入.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_輸入.ShadowSize = 3;
            this.rJ_Button_輸入.ShowLoadingForm = false;
            this.rJ_Button_輸入.Size = new System.Drawing.Size(120, 65);
            this.rJ_Button_輸入.State = false;
            this.rJ_Button_輸入.TabIndex = 55;
            this.rJ_Button_輸入.Text = "輸入";
            this.rJ_Button_輸入.TextColor = System.Drawing.Color.White;
            this.rJ_Button_輸入.TextHeight = 0;
            this.rJ_Button_輸入.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_領藥號
            // 
            this.rJ_TextBox_領藥號.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_領藥號.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_領藥號.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_領藥號.BorderRadius = 0;
            this.rJ_TextBox_領藥號.BorderSize = 2;
            this.rJ_TextBox_領藥號.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.rJ_TextBox_領藥號.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_領藥號.GUID = "";
            this.rJ_TextBox_領藥號.Location = new System.Drawing.Point(510, 15);
            this.rJ_TextBox_領藥號.Multiline = false;
            this.rJ_TextBox_領藥號.Name = "rJ_TextBox_領藥號";
            this.rJ_TextBox_領藥號.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_領藥號.PassWordChar = false;
            this.rJ_TextBox_領藥號.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_領藥號.PlaceholderText = "";
            this.rJ_TextBox_領藥號.ShowTouchPannel = false;
            this.rJ_TextBox_領藥號.Size = new System.Drawing.Size(270, 50);
            this.rJ_TextBox_領藥號.TabIndex = 54;
            this.rJ_TextBox_領藥號.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_領藥號.Texts = "";
            this.rJ_TextBox_領藥號.UnderlineStyle = false;
            // 
            // rJ_Lable26
            // 
            this.rJ_Lable26.BackColor = System.Drawing.Color.White;
            this.rJ_Lable26.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.rJ_Lable26.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable26.BorderRadius = 8;
            this.rJ_Lable26.BorderSize = 0;
            this.rJ_Lable26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable26.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable26.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable26.GUID = "";
            this.rJ_Lable26.Location = new System.Drawing.Point(353, 9);
            this.rJ_Lable26.Name = "rJ_Lable26";
            this.rJ_Lable26.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable26.ShadowSize = 0;
            this.rJ_Lable26.Size = new System.Drawing.Size(147, 63);
            this.rJ_Lable26.TabIndex = 53;
            this.rJ_Lable26.Text = "領藥號";
            this.rJ_Lable26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable26.TextColor = System.Drawing.Color.Black;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rJ_Button_選取處方);
            this.panel2.Controls.Add(this.sqL_DataGridView_醫令資料);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(4, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(950, 871);
            this.panel2.TabIndex = 9;
            // 
            // rJ_Button_選取處方
            // 
            this.rJ_Button_選取處方.AutoResetState = false;
            this.rJ_Button_選取處方.BackColor = System.Drawing.Color.White;
            this.rJ_Button_選取處方.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_選取處方.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_選取處方.BorderRadius = 20;
            this.rJ_Button_選取處方.BorderSize = 0;
            this.rJ_Button_選取處方.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_選取處方.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_選取處方.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Button_選取處方.FlatAppearance.BorderSize = 0;
            this.rJ_Button_選取處方.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_選取處方.Font = new System.Drawing.Font("微軟正黑體", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_選取處方.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_選取處方.GUID = "";
            this.rJ_Button_選取處方.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_選取處方.Location = new System.Drawing.Point(0, 747);
            this.rJ_Button_選取處方.Name = "rJ_Button_選取處方";
            this.rJ_Button_選取處方.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_選取處方.ProhibitionLineWidth = 4;
            this.rJ_Button_選取處方.ProhibitionSymbolSize = 30;
            this.rJ_Button_選取處方.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_選取處方.ShadowSize = 3;
            this.rJ_Button_選取處方.ShowLoadingForm = false;
            this.rJ_Button_選取處方.Size = new System.Drawing.Size(950, 124);
            this.rJ_Button_選取處方.State = false;
            this.rJ_Button_選取處方.TabIndex = 56;
            this.rJ_Button_選取處方.Text = "選取處方";
            this.rJ_Button_選取處方.TextColor = System.Drawing.Color.White;
            this.rJ_Button_選取處方.TextHeight = 0;
            this.rJ_Button_選取處方.UseVisualStyleBackColor = false;
            // 
            // sqL_DataGridView_醫令資料
            // 
            this.sqL_DataGridView_醫令資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_醫令資料.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_醫令資料.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_醫令資料.BorderRadius = 0;
            this.sqL_DataGridView_醫令資料.BorderSize = 2;
            this.sqL_DataGridView_醫令資料.CellBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_醫令資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_醫令資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_醫令資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_醫令資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_醫令資料.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_醫令資料.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_醫令資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_醫令資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_醫令資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_醫令資料.columnHeadersHeight = 18;
            this.sqL_DataGridView_醫令資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_醫令資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sqL_DataGridView_醫令資料.DataKeyEnable = false;
            this.sqL_DataGridView_醫令資料.Dock = System.Windows.Forms.DockStyle.Top;
            this.sqL_DataGridView_醫令資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_醫令資料.ImageBox = false;
            this.sqL_DataGridView_醫令資料.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_醫令資料.Name = "sqL_DataGridView_醫令資料";
            this.sqL_DataGridView_醫令資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_醫令資料.Password = "user82822040";
            this.sqL_DataGridView_醫令資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_醫令資料.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_醫令資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_醫令資料.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_醫令資料.RowsHeight = 50;
            this.sqL_DataGridView_醫令資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_醫令資料.selectedBorderSize = 0;
            this.sqL_DataGridView_醫令資料.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_醫令資料.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_醫令資料.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_醫令資料.Server = "127.0.0.0";
            this.sqL_DataGridView_醫令資料.Size = new System.Drawing.Size(950, 747);
            this.sqL_DataGridView_醫令資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_醫令資料.TabIndex = 8;
            this.sqL_DataGridView_醫令資料.TableName = "order_list";
            this.sqL_DataGridView_醫令資料.UserName = "root";
            this.sqL_DataGridView_醫令資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_醫令資料.可選擇多列 = true;
            this.sqL_DataGridView_醫令資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_醫令資料.自動換行 = true;
            this.sqL_DataGridView_醫令資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_醫令資料.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_醫令資料.顯示CheckBox = false;
            this.sqL_DataGridView_醫令資料.顯示首列 = true;
            this.sqL_DataGridView_醫令資料.顯示首行 = true;
            this.sqL_DataGridView_醫令資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_醫令資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // plC_RJ_GroupBox1
            // 
            // 
            // plC_RJ_GroupBox1.ContentsPanel
            // 
            this.plC_RJ_GroupBox1.ContentsPanel.BackColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox1.ContentsPanel.BackgroundColor = System.Drawing.Color.Transparent;
            this.plC_RJ_GroupBox1.ContentsPanel.BorderColor = System.Drawing.Color.Gray;
            this.plC_RJ_GroupBox1.ContentsPanel.BorderRadius = 2;
            this.plC_RJ_GroupBox1.ContentsPanel.BorderSize = 2;
            this.plC_RJ_GroupBox1.ContentsPanel.Controls.Add(this.panel3);
            this.plC_RJ_GroupBox1.ContentsPanel.Controls.Add(this.sqL_DataGridView_醫令資料_已選取處方);
            this.plC_RJ_GroupBox1.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_RJ_GroupBox1.ContentsPanel.ForeColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox1.ContentsPanel.IsSelected = false;
            this.plC_RJ_GroupBox1.ContentsPanel.Location = new System.Drawing.Point(0, 70);
            this.plC_RJ_GroupBox1.ContentsPanel.Name = "ContentsPanel";
            this.plC_RJ_GroupBox1.ContentsPanel.Padding = new System.Windows.Forms.Padding(10);
            this.plC_RJ_GroupBox1.ContentsPanel.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_GroupBox1.ContentsPanel.ShadowSize = 0;
            this.plC_RJ_GroupBox1.ContentsPanel.Size = new System.Drawing.Size(726, 801);
            this.plC_RJ_GroupBox1.ContentsPanel.TabIndex = 2;
            this.plC_RJ_GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_RJ_GroupBox1.GUID = "";
            this.plC_RJ_GroupBox1.Location = new System.Drawing.Point(954, 109);
            this.plC_RJ_GroupBox1.Name = "plC_RJ_GroupBox1";
            this.plC_RJ_GroupBox1.PannelBackColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox1.PannelBorderColor = System.Drawing.Color.Gray;
            this.plC_RJ_GroupBox1.PannelBorderRadius = 2;
            this.plC_RJ_GroupBox1.PannelBorderSize = 2;
            this.plC_RJ_GroupBox1.Size = new System.Drawing.Size(726, 871);
            this.plC_RJ_GroupBox1.TabIndex = 11;
            this.plC_RJ_GroupBox1.TitleBackColor = System.Drawing.Color.Gray;
            this.plC_RJ_GroupBox1.TitleBorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_GroupBox1.TitleBorderRadius = 5;
            this.plC_RJ_GroupBox1.TitleBorderSize = 0;
            this.plC_RJ_GroupBox1.TitleFont = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_GroupBox1.TitleForeColor = System.Drawing.Color.White;
            this.plC_RJ_GroupBox1.TitleHeight = 70;
            this.plC_RJ_GroupBox1.TitleTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.plC_RJ_GroupBox1.TitleTexts = "已選取處方";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rJ_Button_刪除);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(10, 704);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(706, 87);
            this.panel3.TabIndex = 59;
            // 
            // rJ_Button_刪除
            // 
            this.rJ_Button_刪除.AutoResetState = false;
            this.rJ_Button_刪除.BackColor = System.Drawing.Color.White;
            this.rJ_Button_刪除.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_刪除.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_刪除.BorderRadius = 20;
            this.rJ_Button_刪除.BorderSize = 0;
            this.rJ_Button_刪除.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_刪除.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_刪除.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_刪除.FlatAppearance.BorderSize = 0;
            this.rJ_Button_刪除.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_刪除.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_刪除.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_刪除.GUID = "";
            this.rJ_Button_刪除.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_刪除.Location = new System.Drawing.Point(574, 5);
            this.rJ_Button_刪除.Name = "rJ_Button_刪除";
            this.rJ_Button_刪除.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_刪除.ProhibitionLineWidth = 4;
            this.rJ_Button_刪除.ProhibitionSymbolSize = 30;
            this.rJ_Button_刪除.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_刪除.ShadowSize = 3;
            this.rJ_Button_刪除.ShowLoadingForm = false;
            this.rJ_Button_刪除.Size = new System.Drawing.Size(127, 77);
            this.rJ_Button_刪除.State = false;
            this.rJ_Button_刪除.TabIndex = 58;
            this.rJ_Button_刪除.Text = "刪除";
            this.rJ_Button_刪除.TextColor = System.Drawing.Color.White;
            this.rJ_Button_刪除.TextHeight = 0;
            this.rJ_Button_刪除.UseVisualStyleBackColor = false;
            // 
            // sqL_DataGridView_醫令資料_已選取處方
            // 
            this.sqL_DataGridView_醫令資料_已選取處方.AutoSelectToDeep = false;
            this.sqL_DataGridView_醫令資料_已選取處方.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_醫令資料_已選取處方.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_醫令資料_已選取處方.BorderRadius = 0;
            this.sqL_DataGridView_醫令資料_已選取處方.BorderSize = 2;
            this.sqL_DataGridView_醫令資料_已選取處方.CellBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_醫令資料_已選取處方.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_醫令資料_已選取處方.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_醫令資料_已選取處方.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_醫令資料_已選取處方.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_醫令資料_已選取處方.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_醫令資料_已選取處方.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_醫令資料_已選取處方.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_醫令資料_已選取處方.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_醫令資料_已選取處方.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_醫令資料_已選取處方.columnHeadersHeight = 18;
            this.sqL_DataGridView_醫令資料_已選取處方.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_醫令資料_已選取處方.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sqL_DataGridView_醫令資料_已選取處方.DataKeyEnable = false;
            this.sqL_DataGridView_醫令資料_已選取處方.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_醫令資料_已選取處方.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_醫令資料_已選取處方.ImageBox = false;
            this.sqL_DataGridView_醫令資料_已選取處方.Location = new System.Drawing.Point(10, 10);
            this.sqL_DataGridView_醫令資料_已選取處方.Name = "sqL_DataGridView_醫令資料_已選取處方";
            this.sqL_DataGridView_醫令資料_已選取處方.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_醫令資料_已選取處方.Password = "user82822040";
            this.sqL_DataGridView_醫令資料_已選取處方.Port = ((uint)(3306u));
            this.sqL_DataGridView_醫令資料_已選取處方.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_醫令資料_已選取處方.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_醫令資料_已選取處方.RowsColor = System.Drawing.Color.White;
            this.sqL_DataGridView_醫令資料_已選取處方.RowsHeight = 50;
            this.sqL_DataGridView_醫令資料_已選取處方.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_醫令資料_已選取處方.selectedBorderSize = 0;
            this.sqL_DataGridView_醫令資料_已選取處方.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_醫令資料_已選取處方.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_醫令資料_已選取處方.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_醫令資料_已選取處方.Server = "127.0.0.0";
            this.sqL_DataGridView_醫令資料_已選取處方.Size = new System.Drawing.Size(706, 781);
            this.sqL_DataGridView_醫令資料_已選取處方.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_醫令資料_已選取處方.TabIndex = 9;
            this.sqL_DataGridView_醫令資料_已選取處方.TableName = "order_list";
            this.sqL_DataGridView_醫令資料_已選取處方.UserName = "root";
            this.sqL_DataGridView_醫令資料_已選取處方.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_醫令資料_已選取處方.可選擇多列 = true;
            this.sqL_DataGridView_醫令資料_已選取處方.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_醫令資料_已選取處方.自動換行 = true;
            this.sqL_DataGridView_醫令資料_已選取處方.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_醫令資料_已選取處方.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_醫令資料_已選取處方.顯示CheckBox = false;
            this.sqL_DataGridView_醫令資料_已選取處方.顯示首列 = true;
            this.sqL_DataGridView_醫令資料_已選取處方.顯示首行 = true;
            this.sqL_DataGridView_醫令資料_已選取處方.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_醫令資料_已選取處方.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // rJ_DatePicker_日期
            // 
            this.rJ_DatePicker_日期.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_DatePicker_日期.BorderSize = 0;
            this.rJ_DatePicker_日期.CalendarFont = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_日期.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_日期.Location = new System.Drawing.Point(22, 19);
            this.rJ_DatePicker_日期.MinimumSize = new System.Drawing.Size(250, 35);
            this.rJ_DatePicker_日期.Name = "rJ_DatePicker_日期";
            this.rJ_DatePicker_日期.PickerFont = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker_日期.PickerFore = System.Drawing.SystemColors.ControlText;
            this.rJ_DatePicker_日期.Size = new System.Drawing.Size(308, 46);
            this.rJ_DatePicker_日期.SkinColor = System.Drawing.Color.DimGray;
            this.rJ_DatePicker_日期.TabIndex = 63;
            this.rJ_DatePicker_日期.TextColor = System.Drawing.Color.White;
            // 
            // Dialog_領藥號輸入
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1684, 984);
            this.Controls.Add(this.plC_RJ_GroupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_領藥號輸入";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.plC_RJ_GroupBox1.ContentsPanel.ResumeLayout(false);
            this.plC_RJ_GroupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_高價藥;
        private System.Windows.Forms.CheckBox checkBox_其餘品項;
        private System.Windows.Forms.CheckBox checkBox_高警訊;
        private System.Windows.Forms.CheckBox checkBox_管4;
        private System.Windows.Forms.CheckBox checkBox_管1_3;
        private MyUI.RJ_Button rJ_Button_確認;
        private MyUI.RJ_Button rJ_Button_取消;
        private MyUI.RJ_Button rJ_Button_輸入;
        private MyUI.RJ_TextBox rJ_TextBox_領藥號;
        private MyUI.RJ_Lable rJ_Lable26;
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_Button rJ_Button_選取處方;
        private SQLUI.SQL_DataGridView sqL_DataGridView_醫令資料;
        private MyUI.RJ_DatePicker rJ_DatePicker_日期;
        private MyUI.PLC_RJ_GroupBox plC_RJ_GroupBox1;
        private System.Windows.Forms.Panel panel3;
        private MyUI.RJ_Button rJ_Button_刪除;
        private SQLUI.SQL_DataGridView sqL_DataGridView_醫令資料_已選取處方;
    }
}