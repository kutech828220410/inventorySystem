
namespace 智能藥庫管理系統
{
    partial class Dialog_庫存查詢
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
            this.sqL_DataGridView_庫存查詢 = new SQLUI.SQL_DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rJ_Button_顯示全部 = new MyUI.RJ_Button();
            this.rJ_Button_藥碼搜尋 = new MyUI.RJ_Button();
            this.rJ_TextBox_藥碼搜尋 = new MyUI.RJ_TextBox();
            this.rJ_Button_藥名搜尋 = new MyUI.RJ_Button();
            this.rJ_TextBox_藥名搜尋 = new MyUI.RJ_TextBox();
            this.rJ_Button_中文名搜尋 = new MyUI.RJ_Button();
            this.rJ_TextBox_中文名搜尋 = new MyUI.RJ_TextBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.rJ_RatioButton_搜尋方式_模糊 = new MyUI.RJ_RatioButton();
            this.rJ_RatioButton_搜尋方式_前綴 = new MyUI.RJ_RatioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_藥局_效期及批號 = new SQLUI.SQL_DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_藥庫_效期及批號 = new SQLUI.SQL_DataGridView();
            this.plC_RJ_Button_匯出 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button3 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button2 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button1 = new MyUI.PLC_RJ_Button();
            this.panel2.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sqL_DataGridView_庫存查詢);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 28);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(1272, 495);
            this.panel2.TabIndex = 21;
            // 
            // sqL_DataGridView_庫存查詢
            // 
            this.sqL_DataGridView_庫存查詢.AutoSelectToDeep = false;
            this.sqL_DataGridView_庫存查詢.backColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_庫存查詢.BorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_庫存查詢.BorderRadius = 0;
            this.sqL_DataGridView_庫存查詢.BorderSize = 0;
            this.sqL_DataGridView_庫存查詢.CellBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_庫存查詢.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_庫存查詢.cellStylBackColor = System.Drawing.Color.PowderBlue;
            this.sqL_DataGridView_庫存查詢.cellStyleFont = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_庫存查詢.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_庫存查詢.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_庫存查詢.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_庫存查詢.columnHeaderFont = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_庫存查詢.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_庫存查詢.columnHeadersHeight = 70;
            this.sqL_DataGridView_庫存查詢.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_庫存查詢.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_庫存查詢.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_庫存查詢.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_庫存查詢.ImageBox = false;
            this.sqL_DataGridView_庫存查詢.Location = new System.Drawing.Point(3, 42);
            this.sqL_DataGridView_庫存查詢.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sqL_DataGridView_庫存查詢.Name = "sqL_DataGridView_庫存查詢";
            this.sqL_DataGridView_庫存查詢.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_庫存查詢.Password = "user82822040";
            this.sqL_DataGridView_庫存查詢.Port = ((uint)(3306u));
            this.sqL_DataGridView_庫存查詢.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_庫存查詢.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_庫存查詢.RowsColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_庫存查詢.RowsHeight = 40;
            this.sqL_DataGridView_庫存查詢.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_庫存查詢.selectedBorderSize = 0;
            this.sqL_DataGridView_庫存查詢.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_庫存查詢.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_庫存查詢.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_庫存查詢.Server = "127.0.0.0";
            this.sqL_DataGridView_庫存查詢.Size = new System.Drawing.Size(1266, 450);
            this.sqL_DataGridView_庫存查詢.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_庫存查詢.TabIndex = 24;
            this.sqL_DataGridView_庫存查詢.UserName = "root";
            this.sqL_DataGridView_庫存查詢.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_庫存查詢.可選擇多列 = false;
            this.sqL_DataGridView_庫存查詢.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_庫存查詢.自動換行 = true;
            this.sqL_DataGridView_庫存查詢.表單字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_庫存查詢.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_庫存查詢.顯示CheckBox = false;
            this.sqL_DataGridView_庫存查詢.顯示首列 = true;
            this.sqL_DataGridView_庫存查詢.顯示首行 = true;
            this.sqL_DataGridView_庫存查詢.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_庫存查詢.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1266, 39);
            this.panel1.TabIndex = 0;
            // 
            // rJ_Button_顯示全部
            // 
            this.rJ_Button_顯示全部.AutoResetState = false;
            this.rJ_Button_顯示全部.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_顯示全部.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_顯示全部.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_顯示全部.BorderRadius = 10;
            this.rJ_Button_顯示全部.BorderSize = 0;
            this.rJ_Button_顯示全部.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_顯示全部.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_顯示全部.FlatAppearance.BorderSize = 0;
            this.rJ_Button_顯示全部.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_顯示全部.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_顯示全部.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_顯示全部.GUID = "";
            this.rJ_Button_顯示全部.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_顯示全部.Location = new System.Drawing.Point(1138, 886);
            this.rJ_Button_顯示全部.Name = "rJ_Button_顯示全部";
            this.rJ_Button_顯示全部.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_顯示全部.ProhibitionLineWidth = 4;
            this.rJ_Button_顯示全部.ProhibitionSymbolSize = 30;
            this.rJ_Button_顯示全部.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_顯示全部.ShadowSize = 3;
            this.rJ_Button_顯示全部.ShowLoadingForm = false;
            this.rJ_Button_顯示全部.Size = new System.Drawing.Size(135, 69);
            this.rJ_Button_顯示全部.State = false;
            this.rJ_Button_顯示全部.TabIndex = 25;
            this.rJ_Button_顯示全部.Text = "顯示全部";
            this.rJ_Button_顯示全部.TextColor = System.Drawing.Color.White;
            this.rJ_Button_顯示全部.TextHeight = 0;
            this.rJ_Button_顯示全部.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_藥碼搜尋
            // 
            this.rJ_Button_藥碼搜尋.AutoResetState = false;
            this.rJ_Button_藥碼搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥碼搜尋.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_藥碼搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥碼搜尋.BorderRadius = 10;
            this.rJ_Button_藥碼搜尋.BorderSize = 0;
            this.rJ_Button_藥碼搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥碼搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥碼搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥碼搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥碼搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥碼搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥碼搜尋.GUID = "";
            this.rJ_Button_藥碼搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥碼搜尋.Location = new System.Drawing.Point(205, 898);
            this.rJ_Button_藥碼搜尋.Name = "rJ_Button_藥碼搜尋";
            this.rJ_Button_藥碼搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥碼搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥碼搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥碼搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥碼搜尋.ShadowSize = 3;
            this.rJ_Button_藥碼搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥碼搜尋.Size = new System.Drawing.Size(78, 50);
            this.rJ_Button_藥碼搜尋.State = false;
            this.rJ_Button_藥碼搜尋.TabIndex = 27;
            this.rJ_Button_藥碼搜尋.Text = "搜尋";
            this.rJ_Button_藥碼搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥碼搜尋.TextHeight = 0;
            this.rJ_Button_藥碼搜尋.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_藥碼搜尋
            // 
            this.rJ_TextBox_藥碼搜尋.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥碼搜尋.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥碼搜尋.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥碼搜尋.BorderRadius = 0;
            this.rJ_TextBox_藥碼搜尋.BorderSize = 2;
            this.rJ_TextBox_藥碼搜尋.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥碼搜尋.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥碼搜尋.GUID = "";
            this.rJ_TextBox_藥碼搜尋.Location = new System.Drawing.Point(18, 902);
            this.rJ_TextBox_藥碼搜尋.Multiline = false;
            this.rJ_TextBox_藥碼搜尋.Name = "rJ_TextBox_藥碼搜尋";
            this.rJ_TextBox_藥碼搜尋.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥碼搜尋.PassWordChar = false;
            this.rJ_TextBox_藥碼搜尋.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥碼搜尋.PlaceholderText = "請輸入藥碼...";
            this.rJ_TextBox_藥碼搜尋.ShowTouchPannel = false;
            this.rJ_TextBox_藥碼搜尋.Size = new System.Drawing.Size(181, 42);
            this.rJ_TextBox_藥碼搜尋.TabIndex = 26;
            this.rJ_TextBox_藥碼搜尋.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥碼搜尋.Texts = "";
            this.rJ_TextBox_藥碼搜尋.UnderlineStyle = false;
            // 
            // rJ_Button_藥名搜尋
            // 
            this.rJ_Button_藥名搜尋.AutoResetState = false;
            this.rJ_Button_藥名搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥名搜尋.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_藥名搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥名搜尋.BorderRadius = 10;
            this.rJ_Button_藥名搜尋.BorderSize = 0;
            this.rJ_Button_藥名搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥名搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥名搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥名搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥名搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥名搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥名搜尋.GUID = "";
            this.rJ_Button_藥名搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_藥名搜尋.Location = new System.Drawing.Point(509, 898);
            this.rJ_Button_藥名搜尋.Name = "rJ_Button_藥名搜尋";
            this.rJ_Button_藥名搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥名搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥名搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥名搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥名搜尋.ShadowSize = 3;
            this.rJ_Button_藥名搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥名搜尋.Size = new System.Drawing.Size(78, 50);
            this.rJ_Button_藥名搜尋.State = false;
            this.rJ_Button_藥名搜尋.TabIndex = 29;
            this.rJ_Button_藥名搜尋.Text = "搜尋";
            this.rJ_Button_藥名搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥名搜尋.TextHeight = 0;
            this.rJ_Button_藥名搜尋.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_藥名搜尋
            // 
            this.rJ_TextBox_藥名搜尋.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_藥名搜尋.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_藥名搜尋.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_藥名搜尋.BorderRadius = 0;
            this.rJ_TextBox_藥名搜尋.BorderSize = 2;
            this.rJ_TextBox_藥名搜尋.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_藥名搜尋.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_藥名搜尋.GUID = "";
            this.rJ_TextBox_藥名搜尋.Location = new System.Drawing.Point(289, 902);
            this.rJ_TextBox_藥名搜尋.Multiline = false;
            this.rJ_TextBox_藥名搜尋.Name = "rJ_TextBox_藥名搜尋";
            this.rJ_TextBox_藥名搜尋.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥名搜尋.PassWordChar = false;
            this.rJ_TextBox_藥名搜尋.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥名搜尋.PlaceholderText = "請輸入藥名...";
            this.rJ_TextBox_藥名搜尋.ShowTouchPannel = false;
            this.rJ_TextBox_藥名搜尋.Size = new System.Drawing.Size(214, 42);
            this.rJ_TextBox_藥名搜尋.TabIndex = 28;
            this.rJ_TextBox_藥名搜尋.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥名搜尋.Texts = "";
            this.rJ_TextBox_藥名搜尋.UnderlineStyle = false;
            // 
            // rJ_Button_中文名搜尋
            // 
            this.rJ_Button_中文名搜尋.AutoResetState = false;
            this.rJ_Button_中文名搜尋.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_中文名搜尋.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_中文名搜尋.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_中文名搜尋.BorderRadius = 10;
            this.rJ_Button_中文名搜尋.BorderSize = 0;
            this.rJ_Button_中文名搜尋.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_中文名搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_中文名搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_中文名搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_中文名搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_中文名搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_中文名搜尋.GUID = "";
            this.rJ_Button_中文名搜尋.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_中文名搜尋.Location = new System.Drawing.Point(813, 898);
            this.rJ_Button_中文名搜尋.Name = "rJ_Button_中文名搜尋";
            this.rJ_Button_中文名搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_中文名搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_中文名搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_中文名搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_中文名搜尋.ShadowSize = 3;
            this.rJ_Button_中文名搜尋.ShowLoadingForm = false;
            this.rJ_Button_中文名搜尋.Size = new System.Drawing.Size(78, 50);
            this.rJ_Button_中文名搜尋.State = false;
            this.rJ_Button_中文名搜尋.TabIndex = 31;
            this.rJ_Button_中文名搜尋.Text = "搜尋";
            this.rJ_Button_中文名搜尋.TextColor = System.Drawing.Color.White;
            this.rJ_Button_中文名搜尋.TextHeight = 0;
            this.rJ_Button_中文名搜尋.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_中文名搜尋
            // 
            this.rJ_TextBox_中文名搜尋.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_中文名搜尋.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_中文名搜尋.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_中文名搜尋.BorderRadius = 0;
            this.rJ_TextBox_中文名搜尋.BorderSize = 2;
            this.rJ_TextBox_中文名搜尋.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_中文名搜尋.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_中文名搜尋.GUID = "";
            this.rJ_TextBox_中文名搜尋.Location = new System.Drawing.Point(593, 902);
            this.rJ_TextBox_中文名搜尋.Multiline = false;
            this.rJ_TextBox_中文名搜尋.Name = "rJ_TextBox_中文名搜尋";
            this.rJ_TextBox_中文名搜尋.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_中文名搜尋.PassWordChar = false;
            this.rJ_TextBox_中文名搜尋.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_中文名搜尋.PlaceholderText = "請輸入中文名...";
            this.rJ_TextBox_中文名搜尋.ShowTouchPannel = false;
            this.rJ_TextBox_中文名搜尋.Size = new System.Drawing.Size(214, 42);
            this.rJ_TextBox_中文名搜尋.TabIndex = 30;
            this.rJ_TextBox_中文名搜尋.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_中文名搜尋.Texts = "";
            this.rJ_TextBox_中文名搜尋.UnderlineStyle = false;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.rJ_RatioButton_搜尋方式_模糊);
            this.groupBox16.Controls.Add(this.rJ_RatioButton_搜尋方式_前綴);
            this.groupBox16.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox16.ForeColor = System.Drawing.Color.Black;
            this.groupBox16.Location = new System.Drawing.Point(897, 885);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(204, 65);
            this.groupBox16.TabIndex = 141;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "搜尋方式";
            // 
            // rJ_RatioButton_搜尋方式_模糊
            // 
            this.rJ_RatioButton_搜尋方式_模糊.AutoSize = true;
            this.rJ_RatioButton_搜尋方式_模糊.BackColor = System.Drawing.Color.White;
            this.rJ_RatioButton_搜尋方式_模糊.CheckColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_RatioButton_搜尋方式_模糊.Checked = true;
            this.rJ_RatioButton_搜尋方式_模糊.ForeColor = System.Drawing.Color.Black;
            this.rJ_RatioButton_搜尋方式_模糊.Location = new System.Drawing.Point(99, 24);
            this.rJ_RatioButton_搜尋方式_模糊.MinimumSize = new System.Drawing.Size(0, 21);
            this.rJ_RatioButton_搜尋方式_模糊.Name = "rJ_RatioButton_搜尋方式_模糊";
            this.rJ_RatioButton_搜尋方式_模糊.Size = new System.Drawing.Size(78, 28);
            this.rJ_RatioButton_搜尋方式_模糊.TabIndex = 1;
            this.rJ_RatioButton_搜尋方式_模糊.TabStop = true;
            this.rJ_RatioButton_搜尋方式_模糊.Text = "模糊";
            this.rJ_RatioButton_搜尋方式_模糊.UncheckColor = System.Drawing.Color.Gray;
            this.rJ_RatioButton_搜尋方式_模糊.UseVisualStyleBackColor = false;
            // 
            // rJ_RatioButton_搜尋方式_前綴
            // 
            this.rJ_RatioButton_搜尋方式_前綴.AutoSize = true;
            this.rJ_RatioButton_搜尋方式_前綴.BackColor = System.Drawing.Color.White;
            this.rJ_RatioButton_搜尋方式_前綴.CheckColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_RatioButton_搜尋方式_前綴.ForeColor = System.Drawing.Color.Black;
            this.rJ_RatioButton_搜尋方式_前綴.Location = new System.Drawing.Point(24, 24);
            this.rJ_RatioButton_搜尋方式_前綴.MinimumSize = new System.Drawing.Size(0, 21);
            this.rJ_RatioButton_搜尋方式_前綴.Name = "rJ_RatioButton_搜尋方式_前綴";
            this.rJ_RatioButton_搜尋方式_前綴.Size = new System.Drawing.Size(78, 28);
            this.rJ_RatioButton_搜尋方式_前綴.TabIndex = 0;
            this.rJ_RatioButton_搜尋方式_前綴.Text = "前綴";
            this.rJ_RatioButton_搜尋方式_前綴.UncheckColor = System.Drawing.Color.Gray;
            this.rJ_RatioButton_搜尋方式_前綴.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.sqL_DataGridView_藥局_效期及批號);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 523);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1272, 240);
            this.panel3.TabIndex = 143;
            // 
            // sqL_DataGridView_藥局_效期及批號
            // 
            this.sqL_DataGridView_藥局_效期及批號.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥局_效期及批號.backColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_藥局_效期及批號.BorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥局_效期及批號.BorderRadius = 0;
            this.sqL_DataGridView_藥局_效期及批號.BorderSize = 1;
            this.sqL_DataGridView_藥局_效期及批號.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sqL_DataGridView_藥局_效期及批號.CellBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥局_效期及批號.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥局_效期及批號.cellStylBackColor = System.Drawing.Color.PowderBlue;
            this.sqL_DataGridView_藥局_效期及批號.cellStyleFont = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥局_效期及批號.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥局_效期及批號.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥局_效期及批號.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥局_效期及批號.columnHeaderFont = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥局_效期及批號.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥局_效期及批號.columnHeadersHeight = 60;
            this.sqL_DataGridView_藥局_效期及批號.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_藥局_效期及批號.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_藥局_效期及批號.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥局_效期及批號.ImageBox = false;
            this.sqL_DataGridView_藥局_效期及批號.Location = new System.Drawing.Point(622, 1);
            this.sqL_DataGridView_藥局_效期及批號.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sqL_DataGridView_藥局_效期及批號.Name = "sqL_DataGridView_藥局_效期及批號";
            this.sqL_DataGridView_藥局_效期及批號.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥局_效期及批號.Password = "user82822040";
            this.sqL_DataGridView_藥局_效期及批號.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥局_效期及批號.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_藥局_效期及批號.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥局_效期及批號.RowsColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_藥局_效期及批號.RowsHeight = 30;
            this.sqL_DataGridView_藥局_效期及批號.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥局_效期及批號.selectedBorderSize = 0;
            this.sqL_DataGridView_藥局_效期及批號.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥局_效期及批號.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥局_效期及批號.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥局_效期及批號.Server = "127.0.0.0";
            this.sqL_DataGridView_藥局_效期及批號.Size = new System.Drawing.Size(602, 238);
            this.sqL_DataGridView_藥局_效期及批號.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥局_效期及批號.TabIndex = 23;
            this.sqL_DataGridView_藥局_效期及批號.UserName = "root";
            this.sqL_DataGridView_藥局_效期及批號.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_藥局_效期及批號.可選擇多列 = false;
            this.sqL_DataGridView_藥局_效期及批號.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥局_效期及批號.自動換行 = true;
            this.sqL_DataGridView_藥局_效期及批號.表單字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥局_效期及批號.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_藥局_效期及批號.顯示CheckBox = false;
            this.sqL_DataGridView_藥局_效期及批號.顯示首列 = true;
            this.sqL_DataGridView_藥局_效期及批號.顯示首行 = true;
            this.sqL_DataGridView_藥局_效期及批號.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥局_效期及批號.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.plC_RJ_Button3);
            this.panel4.Controls.Add(this.plC_RJ_Button2);
            this.panel4.Controls.Add(this.sqL_DataGridView_藥庫_效期及批號);
            this.panel4.Controls.Add(this.plC_RJ_Button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(615, 238);
            this.panel4.TabIndex = 23;
            // 
            // sqL_DataGridView_藥庫_效期及批號
            // 
            this.sqL_DataGridView_藥庫_效期及批號.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥庫_效期及批號.backColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_藥庫_效期及批號.BorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥庫_效期及批號.BorderRadius = 0;
            this.sqL_DataGridView_藥庫_效期及批號.BorderSize = 1;
            this.sqL_DataGridView_藥庫_效期及批號.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sqL_DataGridView_藥庫_效期及批號.CellBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥庫_效期及批號.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥庫_效期及批號.cellStylBackColor = System.Drawing.Color.PowderBlue;
            this.sqL_DataGridView_藥庫_效期及批號.cellStyleFont = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥庫_效期及批號.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥庫_效期及批號.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥庫_效期及批號.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥庫_效期及批號.columnHeaderFont = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_藥庫_效期及批號.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥庫_效期及批號.columnHeadersHeight = 60;
            this.sqL_DataGridView_藥庫_效期及批號.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_藥庫_效期及批號.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_藥庫_效期及批號.Dock = System.Windows.Forms.DockStyle.Left;
            this.sqL_DataGridView_藥庫_效期及批號.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥庫_效期及批號.ImageBox = false;
            this.sqL_DataGridView_藥庫_效期及批號.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_藥庫_效期及批號.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sqL_DataGridView_藥庫_效期及批號.Name = "sqL_DataGridView_藥庫_效期及批號";
            this.sqL_DataGridView_藥庫_效期及批號.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥庫_效期及批號.Password = "user82822040";
            this.sqL_DataGridView_藥庫_效期及批號.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥庫_效期及批號.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_藥庫_效期及批號.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥庫_效期及批號.RowsColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_藥庫_效期及批號.RowsHeight = 30;
            this.sqL_DataGridView_藥庫_效期及批號.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥庫_效期及批號.selectedBorderSize = 0;
            this.sqL_DataGridView_藥庫_效期及批號.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥庫_效期及批號.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥庫_效期及批號.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥庫_效期及批號.Server = "127.0.0.0";
            this.sqL_DataGridView_藥庫_效期及批號.Size = new System.Drawing.Size(540, 238);
            this.sqL_DataGridView_藥庫_效期及批號.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥庫_效期及批號.TabIndex = 23;
            this.sqL_DataGridView_藥庫_效期及批號.UserName = "root";
            this.sqL_DataGridView_藥庫_效期及批號.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_藥庫_效期及批號.可選擇多列 = false;
            this.sqL_DataGridView_藥庫_效期及批號.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥庫_效期及批號.自動換行 = true;
            this.sqL_DataGridView_藥庫_效期及批號.表單字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥庫_效期及批號.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_藥庫_效期及批號.顯示CheckBox = false;
            this.sqL_DataGridView_藥庫_效期及批號.顯示首列 = true;
            this.sqL_DataGridView_藥庫_效期及批號.顯示首行 = true;
            this.sqL_DataGridView_藥庫_效期及批號.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥庫_效期及批號.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // plC_RJ_Button_匯出
            // 
            this.plC_RJ_Button_匯出.AutoResetState = false;
            this.plC_RJ_Button_匯出.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_匯出.Bool = false;
            this.plC_RJ_Button_匯出.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯出.BorderRadius = 10;
            this.plC_RJ_Button_匯出.BorderSize = 1;
            this.plC_RJ_Button_匯出.but_press = false;
            this.plC_RJ_Button_匯出.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_匯出.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_匯出.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_匯出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_匯出.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_匯出.GUID = "";
            this.plC_RJ_Button_匯出.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_匯出.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_匯出.Location = new System.Drawing.Point(18, 769);
            this.plC_RJ_Button_匯出.Name = "plC_RJ_Button_匯出";
            this.plC_RJ_Button_匯出.OFF_文字內容 = "匯出";
            this.plC_RJ_Button_匯出.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_匯出.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯出.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_匯出.ON_BorderSize = 1;
            this.plC_RJ_Button_匯出.ON_文字內容 = "匯出";
            this.plC_RJ_Button_匯出.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_匯出.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯出.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button_匯出.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_匯出.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_匯出.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_匯出.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_匯出.ShadowSize = 3;
            this.plC_RJ_Button_匯出.ShowLoadingForm = false;
            this.plC_RJ_Button_匯出.Size = new System.Drawing.Size(112, 110);
            this.plC_RJ_Button_匯出.State = false;
            this.plC_RJ_Button_匯出.TabIndex = 145;
            this.plC_RJ_Button_匯出.Text = "匯出";
            this.plC_RJ_Button_匯出.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯出.TextHeight = 35;
            this.plC_RJ_Button_匯出.Texts = "匯出";
            this.plC_RJ_Button_匯出.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_匯出.字型鎖住 = false;
            this.plC_RJ_Button_匯出.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_匯出.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_匯出.文字鎖住 = false;
            this.plC_RJ_Button_匯出.背景圖片 = global::智能藥庫管理系統.Properties.Resources.Files_and_Folders_file_export_arrow_512;
            this.plC_RJ_Button_匯出.讀取位元反向 = false;
            this.plC_RJ_Button_匯出.讀寫鎖住 = false;
            this.plC_RJ_Button_匯出.音效 = true;
            this.plC_RJ_Button_匯出.顯示 = false;
            this.plC_RJ_Button_匯出.顯示狀態 = false;
            // 
            // plC_RJ_Button3
            // 
            this.plC_RJ_Button3.AutoResetState = false;
            this.plC_RJ_Button3.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button3.Bool = false;
            this.plC_RJ_Button3.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button3.BorderRadius = 10;
            this.plC_RJ_Button3.BorderSize = 1;
            this.plC_RJ_Button3.but_press = false;
            this.plC_RJ_Button3.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button3.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button3.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button3.GUID = "";
            this.plC_RJ_Button3.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button3.Image_padding = new System.Windows.Forms.Padding(15, 7, 20, 5);
            this.plC_RJ_Button3.Location = new System.Drawing.Point(547, 149);
            this.plC_RJ_Button3.Name = "plC_RJ_Button3";
            this.plC_RJ_Button3.OFF_文字內容 = "修改";
            this.plC_RJ_Button3.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button3.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button3.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button3.ON_BorderSize = 1;
            this.plC_RJ_Button3.ON_文字內容 = "修改";
            this.plC_RJ_Button3.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button3.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button3.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button3.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button3.ProhibitionLineWidth = 4;
            this.plC_RJ_Button3.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button3.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button3.ShadowSize = 3;
            this.plC_RJ_Button3.ShowLoadingForm = false;
            this.plC_RJ_Button3.Size = new System.Drawing.Size(61, 66);
            this.plC_RJ_Button3.State = false;
            this.plC_RJ_Button3.TabIndex = 148;
            this.plC_RJ_Button3.Text = "修改";
            this.plC_RJ_Button3.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button3.TextHeight = 30;
            this.plC_RJ_Button3.Texts = "修改";
            this.plC_RJ_Button3.UseVisualStyleBackColor = false;
            this.plC_RJ_Button3.字型鎖住 = false;
            this.plC_RJ_Button3.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button3.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button3.文字鎖住 = false;
            this.plC_RJ_Button3.背景圖片 = global::智能藥庫管理系統.Properties.Resources.EDIT;
            this.plC_RJ_Button3.讀取位元反向 = false;
            this.plC_RJ_Button3.讀寫鎖住 = false;
            this.plC_RJ_Button3.音效 = true;
            this.plC_RJ_Button3.顯示 = false;
            this.plC_RJ_Button3.顯示狀態 = false;
            // 
            // plC_RJ_Button2
            // 
            this.plC_RJ_Button2.AutoResetState = false;
            this.plC_RJ_Button2.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button2.Bool = false;
            this.plC_RJ_Button2.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button2.BorderRadius = 10;
            this.plC_RJ_Button2.BorderSize = 1;
            this.plC_RJ_Button2.but_press = false;
            this.plC_RJ_Button2.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button2.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button2.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button2.GUID = "";
            this.plC_RJ_Button2.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button2.Image_padding = new System.Windows.Forms.Padding(15, 7, 20, 5);
            this.plC_RJ_Button2.Location = new System.Drawing.Point(547, 77);
            this.plC_RJ_Button2.Name = "plC_RJ_Button2";
            this.plC_RJ_Button2.OFF_文字內容 = "刪除";
            this.plC_RJ_Button2.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button2.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button2.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button2.ON_BorderSize = 1;
            this.plC_RJ_Button2.ON_文字內容 = "刪除";
            this.plC_RJ_Button2.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button2.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button2.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button2.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button2.ProhibitionLineWidth = 4;
            this.plC_RJ_Button2.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button2.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button2.ShadowSize = 3;
            this.plC_RJ_Button2.ShowLoadingForm = false;
            this.plC_RJ_Button2.Size = new System.Drawing.Size(61, 66);
            this.plC_RJ_Button2.State = false;
            this.plC_RJ_Button2.TabIndex = 147;
            this.plC_RJ_Button2.Text = "刪除";
            this.plC_RJ_Button2.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button2.TextHeight = 30;
            this.plC_RJ_Button2.Texts = "刪除";
            this.plC_RJ_Button2.UseVisualStyleBackColor = false;
            this.plC_RJ_Button2.字型鎖住 = false;
            this.plC_RJ_Button2.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button2.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button2.文字鎖住 = false;
            this.plC_RJ_Button2.背景圖片 = global::智能藥庫管理系統.Properties.Resources.trash_512;
            this.plC_RJ_Button2.讀取位元反向 = false;
            this.plC_RJ_Button2.讀寫鎖住 = false;
            this.plC_RJ_Button2.音效 = true;
            this.plC_RJ_Button2.顯示 = false;
            this.plC_RJ_Button2.顯示狀態 = false;
            // 
            // plC_RJ_Button1
            // 
            this.plC_RJ_Button1.AutoResetState = false;
            this.plC_RJ_Button1.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button1.Bool = false;
            this.plC_RJ_Button1.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button1.BorderRadius = 10;
            this.plC_RJ_Button1.BorderSize = 1;
            this.plC_RJ_Button1.but_press = false;
            this.plC_RJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button1.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button1.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button1.GUID = "";
            this.plC_RJ_Button1.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button1.Image_padding = new System.Windows.Forms.Padding(15, 7, 20, 5);
            this.plC_RJ_Button1.Location = new System.Drawing.Point(547, 5);
            this.plC_RJ_Button1.Name = "plC_RJ_Button1";
            this.plC_RJ_Button1.OFF_文字內容 = "新增";
            this.plC_RJ_Button1.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button1.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button1.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button1.ON_BorderSize = 1;
            this.plC_RJ_Button1.ON_文字內容 = "新增";
            this.plC_RJ_Button1.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button1.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button1.ON_背景顏色 = System.Drawing.Color.Linen;
            this.plC_RJ_Button1.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button1.ProhibitionLineWidth = 4;
            this.plC_RJ_Button1.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button1.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button1.ShadowSize = 3;
            this.plC_RJ_Button1.ShowLoadingForm = false;
            this.plC_RJ_Button1.Size = new System.Drawing.Size(61, 66);
            this.plC_RJ_Button1.State = false;
            this.plC_RJ_Button1.TabIndex = 146;
            this.plC_RJ_Button1.Text = "新增";
            this.plC_RJ_Button1.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button1.TextHeight = 30;
            this.plC_RJ_Button1.Texts = "新增";
            this.plC_RJ_Button1.UseVisualStyleBackColor = false;
            this.plC_RJ_Button1.字型鎖住 = false;
            this.plC_RJ_Button1.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button1.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button1.文字鎖住 = false;
            this.plC_RJ_Button1.背景圖片 = global::智能藥庫管理系統.Properties.Resources.add_new_plus_512;
            this.plC_RJ_Button1.讀取位元反向 = false;
            this.plC_RJ_Button1.讀寫鎖住 = false;
            this.plC_RJ_Button1.音效 = true;
            this.plC_RJ_Button1.顯示 = false;
            this.plC_RJ_Button1.顯示狀態 = false;
            // 
            // Dialog_庫存查詢
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 960);
            this.ControlBox = true;
            this.Controls.Add(this.plC_RJ_Button_匯出);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox16);
            this.Controls.Add(this.rJ_Button_中文名搜尋);
            this.Controls.Add(this.rJ_TextBox_中文名搜尋);
            this.Controls.Add(this.rJ_Button_藥名搜尋);
            this.Controls.Add(this.rJ_TextBox_藥名搜尋);
            this.Controls.Add(this.rJ_Button_藥碼搜尋);
            this.Controls.Add(this.rJ_TextBox_藥碼搜尋);
            this.Controls.Add(this.rJ_Button_顯示全部);
            this.Controls.Add(this.panel2);
            this.Name = "Dialog_庫存查詢";
            this.Special_Time = 50;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "庫存查詢";
            this.panel2.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_Button rJ_Button_顯示全部;
        private MyUI.RJ_Button rJ_Button_藥碼搜尋;
        private MyUI.RJ_TextBox rJ_TextBox_藥碼搜尋;
        private MyUI.RJ_Button rJ_Button_藥名搜尋;
        private MyUI.RJ_TextBox rJ_TextBox_藥名搜尋;
        private MyUI.RJ_Button rJ_Button_中文名搜尋;
        private MyUI.RJ_TextBox rJ_TextBox_中文名搜尋;
        private System.Windows.Forms.GroupBox groupBox16;
        private MyUI.RJ_RatioButton rJ_RatioButton_搜尋方式_模糊;
        private MyUI.RJ_RatioButton rJ_RatioButton_搜尋方式_前綴;
        private System.Windows.Forms.Panel panel3;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥局_效期及批號;
        private SQLUI.SQL_DataGridView sqL_DataGridView_庫存查詢;
        private System.Windows.Forms.Panel panel1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_匯出;
        private MyUI.PLC_RJ_Button plC_RJ_Button1;
        private System.Windows.Forms.Panel panel4;
        private MyUI.PLC_RJ_Button plC_RJ_Button3;
        private MyUI.PLC_RJ_Button plC_RJ_Button2;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥庫_效期及批號;
    }
}