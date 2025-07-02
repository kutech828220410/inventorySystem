
namespace 調劑台管理系統
{
    partial class Dialog_收支作業_RFID清點作業
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.rJ_Lable2 = new MyUI.RJ_Lable();
            this.rJ_Lable3 = new MyUI.RJ_Lable();
            this.sqL_DataGridView_理論庫存 = new SQLUI.SQL_DataGridView();
            this.sqL_DataGridView_實際庫存 = new SQLUI.SQL_DataGridView();
            this.sqL_DataGridView_異常事件 = new SQLUI.SQL_DataGridView();
            this.rJ_Lable_藥名 = new MyUI.RJ_Lable();
            this.rJ_Lable4 = new MyUI.RJ_Lable();
            this.rJ_Button_取消 = new MyUI.RJ_Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_Button_取消);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.rJ_Button_確認);
            this.panel1.Controls.Add(this.rJ_Lable_藥名);
            this.panel1.Controls.Add(this.rJ_Lable4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 44);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1400, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1400, 322);
            this.panel2.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.sqL_DataGridView_實際庫存);
            this.panel5.Controls.Add(this.rJ_Lable2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(699, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(5);
            this.panel5.Size = new System.Drawing.Size(701, 322);
            this.panel5.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.sqL_DataGridView_理論庫存);
            this.panel4.Controls.Add(this.rJ_Lable1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(699, 322);
            this.panel4.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.sqL_DataGridView_異常事件);
            this.panel3.Controls.Add(this.rJ_Lable3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 466);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(1400, 455);
            this.panel3.TabIndex = 2;
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 5;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(5, 5);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 0;
            this.rJ_Lable1.Size = new System.Drawing.Size(689, 65);
            this.rJ_Lable1.TabIndex = 160;
            this.rJ_Lable1.Text = "理論庫存";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable1.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable2
            // 
            this.rJ_Lable2.BackColor = System.Drawing.Color.White;
            this.rJ_Lable2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Lable2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable2.BorderRadius = 5;
            this.rJ_Lable2.BorderSize = 0;
            this.rJ_Lable2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable2.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable2.GUID = "";
            this.rJ_Lable2.Location = new System.Drawing.Point(5, 5);
            this.rJ_Lable2.Name = "rJ_Lable2";
            this.rJ_Lable2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable2.ShadowSize = 0;
            this.rJ_Lable2.Size = new System.Drawing.Size(691, 65);
            this.rJ_Lable2.TabIndex = 161;
            this.rJ_Lable2.Text = "實際庫存";
            this.rJ_Lable2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable2.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable3
            // 
            this.rJ_Lable3.BackColor = System.Drawing.Color.White;
            this.rJ_Lable3.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Lable3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable3.BorderRadius = 5;
            this.rJ_Lable3.BorderSize = 0;
            this.rJ_Lable3.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable3.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable3.GUID = "";
            this.rJ_Lable3.Location = new System.Drawing.Point(5, 5);
            this.rJ_Lable3.Name = "rJ_Lable3";
            this.rJ_Lable3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable3.ShadowSize = 0;
            this.rJ_Lable3.Size = new System.Drawing.Size(1390, 65);
            this.rJ_Lable3.TabIndex = 161;
            this.rJ_Lable3.Text = "異常事件";
            this.rJ_Lable3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable3.TextColor = System.Drawing.Color.Black;
            // 
            // sqL_DataGridView_理論庫存
            // 
            this.sqL_DataGridView_理論庫存.AutoSelectToDeep = false;
            this.sqL_DataGridView_理論庫存.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_理論庫存.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_理論庫存.BorderRadius = 0;
            this.sqL_DataGridView_理論庫存.BorderSize = 0;
            this.sqL_DataGridView_理論庫存.CellBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_理論庫存.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_理論庫存.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_理論庫存.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_理論庫存.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_理論庫存.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_理論庫存.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_理論庫存.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_理論庫存.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_理論庫存.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_理論庫存.columnHeadersHeight = 40;
            this.sqL_DataGridView_理論庫存.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_理論庫存.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_理論庫存.DataKeyEnable = false;
            this.sqL_DataGridView_理論庫存.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_理論庫存.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_理論庫存.ImageBox = false;
            this.sqL_DataGridView_理論庫存.Location = new System.Drawing.Point(5, 70);
            this.sqL_DataGridView_理論庫存.Margin = new System.Windows.Forms.Padding(4);
            this.sqL_DataGridView_理論庫存.Name = "sqL_DataGridView_理論庫存";
            this.sqL_DataGridView_理論庫存.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_理論庫存.Password = "user82822040";
            this.sqL_DataGridView_理論庫存.Port = ((uint)(3306u));
            this.sqL_DataGridView_理論庫存.rowBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_理論庫存.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_理論庫存.rowHeaderBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_理論庫存.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_理論庫存.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_理論庫存.RowsHeight = 40;
            this.sqL_DataGridView_理論庫存.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_理論庫存.selectedBorderSize = 2;
            this.sqL_DataGridView_理論庫存.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_理論庫存.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_理論庫存.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_理論庫存.Server = "127.0.0.0";
            this.sqL_DataGridView_理論庫存.Size = new System.Drawing.Size(689, 247);
            this.sqL_DataGridView_理論庫存.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_理論庫存.TabIndex = 162;
            this.sqL_DataGridView_理論庫存.UserName = "root";
            this.sqL_DataGridView_理論庫存.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_理論庫存.可選擇多列 = false;
            this.sqL_DataGridView_理論庫存.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_理論庫存.自動換行 = true;
            this.sqL_DataGridView_理論庫存.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_理論庫存.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_理論庫存.顯示CheckBox = false;
            this.sqL_DataGridView_理論庫存.顯示首列 = true;
            this.sqL_DataGridView_理論庫存.顯示首行 = true;
            this.sqL_DataGridView_理論庫存.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_理論庫存.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // sqL_DataGridView_實際庫存
            // 
            this.sqL_DataGridView_實際庫存.AutoSelectToDeep = false;
            this.sqL_DataGridView_實際庫存.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_實際庫存.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_實際庫存.BorderRadius = 0;
            this.sqL_DataGridView_實際庫存.BorderSize = 0;
            this.sqL_DataGridView_實際庫存.CellBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_實際庫存.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_實際庫存.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_實際庫存.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_實際庫存.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_實際庫存.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_實際庫存.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_實際庫存.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_實際庫存.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_實際庫存.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_實際庫存.columnHeadersHeight = 40;
            this.sqL_DataGridView_實際庫存.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_實際庫存.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_實際庫存.DataKeyEnable = false;
            this.sqL_DataGridView_實際庫存.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_實際庫存.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_實際庫存.ImageBox = false;
            this.sqL_DataGridView_實際庫存.Location = new System.Drawing.Point(5, 70);
            this.sqL_DataGridView_實際庫存.Margin = new System.Windows.Forms.Padding(4);
            this.sqL_DataGridView_實際庫存.Name = "sqL_DataGridView_實際庫存";
            this.sqL_DataGridView_實際庫存.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_實際庫存.Password = "user82822040";
            this.sqL_DataGridView_實際庫存.Port = ((uint)(3306u));
            this.sqL_DataGridView_實際庫存.rowBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_實際庫存.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_實際庫存.rowHeaderBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_實際庫存.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_實際庫存.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_實際庫存.RowsHeight = 40;
            this.sqL_DataGridView_實際庫存.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_實際庫存.selectedBorderSize = 2;
            this.sqL_DataGridView_實際庫存.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_實際庫存.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_實際庫存.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_實際庫存.Server = "127.0.0.0";
            this.sqL_DataGridView_實際庫存.Size = new System.Drawing.Size(691, 247);
            this.sqL_DataGridView_實際庫存.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_實際庫存.TabIndex = 163;
            this.sqL_DataGridView_實際庫存.UserName = "root";
            this.sqL_DataGridView_實際庫存.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_實際庫存.可選擇多列 = false;
            this.sqL_DataGridView_實際庫存.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_實際庫存.自動換行 = true;
            this.sqL_DataGridView_實際庫存.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_實際庫存.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_實際庫存.顯示CheckBox = false;
            this.sqL_DataGridView_實際庫存.顯示首列 = true;
            this.sqL_DataGridView_實際庫存.顯示首行 = true;
            this.sqL_DataGridView_實際庫存.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_實際庫存.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // sqL_DataGridView_異常事件
            // 
            this.sqL_DataGridView_異常事件.AutoSelectToDeep = false;
            this.sqL_DataGridView_異常事件.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_異常事件.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_異常事件.BorderRadius = 0;
            this.sqL_DataGridView_異常事件.BorderSize = 0;
            this.sqL_DataGridView_異常事件.CellBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_異常事件.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_異常事件.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_異常事件.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_異常事件.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_異常事件.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_異常事件.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_異常事件.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_異常事件.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_異常事件.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_異常事件.columnHeadersHeight = 40;
            this.sqL_DataGridView_異常事件.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_異常事件.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_異常事件.DataKeyEnable = false;
            this.sqL_DataGridView_異常事件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_異常事件.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_異常事件.ImageBox = false;
            this.sqL_DataGridView_異常事件.Location = new System.Drawing.Point(5, 70);
            this.sqL_DataGridView_異常事件.Margin = new System.Windows.Forms.Padding(4);
            this.sqL_DataGridView_異常事件.Name = "sqL_DataGridView_異常事件";
            this.sqL_DataGridView_異常事件.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_異常事件.Password = "user82822040";
            this.sqL_DataGridView_異常事件.Port = ((uint)(3306u));
            this.sqL_DataGridView_異常事件.rowBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_異常事件.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_異常事件.rowHeaderBorderStyleOption = SQLUI.SQL_DataGridView.RowBorderStyleOption.All;
            this.sqL_DataGridView_異常事件.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_異常事件.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_異常事件.RowsHeight = 40;
            this.sqL_DataGridView_異常事件.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_異常事件.selectedBorderSize = 2;
            this.sqL_DataGridView_異常事件.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_異常事件.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_異常事件.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_異常事件.Server = "127.0.0.0";
            this.sqL_DataGridView_異常事件.Size = new System.Drawing.Size(1390, 380);
            this.sqL_DataGridView_異常事件.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_異常事件.TabIndex = 164;
            this.sqL_DataGridView_異常事件.UserName = "root";
            this.sqL_DataGridView_異常事件.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_異常事件.可選擇多列 = false;
            this.sqL_DataGridView_異常事件.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_異常事件.自動換行 = true;
            this.sqL_DataGridView_異常事件.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_異常事件.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_異常事件.顯示CheckBox = false;
            this.sqL_DataGridView_異常事件.顯示首列 = true;
            this.sqL_DataGridView_異常事件.顯示首行 = true;
            this.sqL_DataGridView_異常事件.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_異常事件.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // rJ_Lable_藥名
            // 
            this.rJ_Lable_藥名.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_藥名.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Lable_藥名.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_藥名.BorderRadius = 5;
            this.rJ_Lable_藥名.BorderSize = 0;
            this.rJ_Lable_藥名.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_藥名.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_藥名.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_藥名.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_藥名.GUID = "";
            this.rJ_Lable_藥名.Location = new System.Drawing.Point(118, 5);
            this.rJ_Lable_藥名.Name = "rJ_Lable_藥名";
            this.rJ_Lable_藥名.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_藥名.ShadowSize = 0;
            this.rJ_Lable_藥名.Size = new System.Drawing.Size(951, 90);
            this.rJ_Lable_藥名.TabIndex = 162;
            this.rJ_Lable_藥名.Text = "--------------------------";
            this.rJ_Lable_藥名.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_藥名.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Lable4
            // 
            this.rJ_Lable4.BackColor = System.Drawing.Color.White;
            this.rJ_Lable4.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Lable4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable4.BorderRadius = 5;
            this.rJ_Lable4.BorderSize = 0;
            this.rJ_Lable4.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable4.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable4.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable4.GUID = "";
            this.rJ_Lable4.Location = new System.Drawing.Point(5, 5);
            this.rJ_Lable4.Name = "rJ_Lable4";
            this.rJ_Lable4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable4.ShadowSize = 0;
            this.rJ_Lable4.Size = new System.Drawing.Size(113, 90);
            this.rJ_Lable4.TabIndex = 161;
            this.rJ_Lable4.Text = "藥名:";
            this.rJ_Lable4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable4.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_Button_取消
            // 
            this.rJ_Button_取消.AutoResetState = false;
            this.rJ_Button_取消.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_取消.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_取消.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Button_取消.BorderRadius = 22;
            this.rJ_Button_取消.BorderSize = 1;
            this.rJ_Button_取消.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_取消.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_取消.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_取消.Enabled = false;
            this.rJ_Button_取消.FlatAppearance.BorderSize = 0;
            this.rJ_Button_取消.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_取消.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.rJ_Button_取消.ForeColor = System.Drawing.Color.Black;
            this.rJ_Button_取消.GUID = "";
            this.rJ_Button_取消.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_取消.Location = new System.Drawing.Point(1130, 5);
            this.rJ_Button_取消.Name = "rJ_Button_取消";
            this.rJ_Button_取消.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_取消.ProhibitionLineWidth = 4;
            this.rJ_Button_取消.ProhibitionSymbolSize = 30;
            this.rJ_Button_取消.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_取消.ShadowSize = 0;
            this.rJ_Button_取消.ShowLoadingForm = false;
            this.rJ_Button_取消.Size = new System.Drawing.Size(126, 90);
            this.rJ_Button_取消.State = false;
            this.rJ_Button_取消.TabIndex = 165;
            this.rJ_Button_取消.Text = "取消";
            this.rJ_Button_取消.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_取消.TextHeight = 0;
            this.rJ_Button_取消.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(1256, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(13, 90);
            this.panel6.TabIndex = 164;
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.Color.White;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認.BorderRadius = 22;
            this.rJ_Button_確認.BorderSize = 0;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(1269, 5);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 0;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(126, 90);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 163;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // Dialog_收支作業_RFID清點作業
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 40;
            this.ClientSize = new System.Drawing.Size(1408, 925);
            this.CloseBoxSize = new System.Drawing.Size(40, 40);
            this.ControlBox = true;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(40, 40);
            this.MiniSize = new System.Drawing.Size(40, 40);
            this.Name = "Dialog_收支作業_RFID清點作業";
            this.Text = "RFID清點作業";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private MyUI.RJ_Lable rJ_Lable2;
        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.RJ_Lable rJ_Lable3;
        private SQLUI.SQL_DataGridView sqL_DataGridView_實際庫存;
        private SQLUI.SQL_DataGridView sqL_DataGridView_理論庫存;
        private SQLUI.SQL_DataGridView sqL_DataGridView_異常事件;
        private MyUI.RJ_Lable rJ_Lable_藥名;
        private MyUI.RJ_Lable rJ_Lable4;
        private MyUI.RJ_Button rJ_Button_取消;
        private System.Windows.Forms.Panel panel6;
        private MyUI.RJ_Button rJ_Button_確認;
    }
}