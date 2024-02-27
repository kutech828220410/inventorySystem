
namespace 癌症自動備藥機暨排程系統
{
    partial class Dialog_手動選擇備藥品
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
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rJ_Button1 = new MyUI.RJ_Button();
            this.rJ_Button_取消 = new MyUI.RJ_Button();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_Button_確認選擇 = new MyUI.RJ_Button();
            this.rJ_Button_藥品選擇_顯示全部 = new MyUI.RJ_Button();
            this.rJ_Button_藥名搜尋 = new MyUI.RJ_Button();
            this.rJ_TextBox_藥名搜尋 = new MyUI.RJ_TextBox();
            this.rJ_Button_藥碼搜尋 = new MyUI.RJ_Button();
            this.rJ_TextBox_藥碼搜尋 = new MyUI.RJ_TextBox();
            this.sqL_DataGridView_藥品選擇 = new SQLUI.SQL_DataGridView();
            this.rJ_Lable2 = new MyUI.RJ_Lable();
            this.sqL_DataGridView_已選藥品 = new SQLUI.SQL_DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.DarkGray;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(4, 10);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 3;
            this.rJ_Lable1.Size = new System.Drawing.Size(942, 71);
            this.rJ_Lable1.TabIndex = 1;
            this.rJ_Lable1.Text = "選 擇 備 藥 藥 品";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.White;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_Button1);
            this.panel1.Controls.Add(this.rJ_Button_取消);
            this.panel1.Controls.Add(this.rJ_Button_確認);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 880);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 116);
            this.panel1.TabIndex = 9;
            // 
            // rJ_Button1
            // 
            this.rJ_Button1.AutoResetState = false;
            this.rJ_Button1.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button1.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.rJ_Button1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button1.BorderRadius = 10;
            this.rJ_Button1.BorderSize = 0;
            this.rJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Button1.FlatAppearance.BorderSize = 0;
            this.rJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button1.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button1.ForeColor = System.Drawing.Color.White;
            this.rJ_Button1.GUID = "";
            this.rJ_Button1.Location = new System.Drawing.Point(0, 0);
            this.rJ_Button1.Name = "rJ_Button1";
            this.rJ_Button1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button1.ShadowSize = 3;
            this.rJ_Button1.ShowLoadingForm = false;
            this.rJ_Button1.Size = new System.Drawing.Size(172, 116);
            this.rJ_Button1.State = false;
            this.rJ_Button1.TabIndex = 2;
            this.rJ_Button1.Text = "刪除";
            this.rJ_Button1.TextColor = System.Drawing.Color.White;
            this.rJ_Button1.UseVisualStyleBackColor = false;
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
            this.rJ_Button_取消.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_取消.FlatAppearance.BorderSize = 0;
            this.rJ_Button_取消.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_取消.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_取消.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_取消.GUID = "";
            this.rJ_Button_取消.Location = new System.Drawing.Point(598, 0);
            this.rJ_Button_取消.Name = "rJ_Button_取消";
            this.rJ_Button_取消.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_取消.ShadowSize = 3;
            this.rJ_Button_取消.ShowLoadingForm = false;
            this.rJ_Button_取消.Size = new System.Drawing.Size(172, 116);
            this.rJ_Button_取消.State = false;
            this.rJ_Button_取消.TabIndex = 1;
            this.rJ_Button_取消.Text = "取消";
            this.rJ_Button_取消.TextColor = System.Drawing.Color.White;
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
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Location = new System.Drawing.Point(770, 0);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(172, 116);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 0;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rJ_Button_確認選擇);
            this.panel2.Controls.Add(this.rJ_Button_藥品選擇_顯示全部);
            this.panel2.Controls.Add(this.rJ_Button_藥名搜尋);
            this.panel2.Controls.Add(this.rJ_TextBox_藥名搜尋);
            this.panel2.Controls.Add(this.rJ_Button_藥碼搜尋);
            this.panel2.Controls.Add(this.rJ_TextBox_藥碼搜尋);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(942, 59);
            this.panel2.TabIndex = 10;
            // 
            // rJ_Button_確認選擇
            // 
            this.rJ_Button_確認選擇.AutoResetState = false;
            this.rJ_Button_確認選擇.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_確認選擇.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.rJ_Button_確認選擇.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認選擇.BorderRadius = 10;
            this.rJ_Button_確認選擇.BorderSize = 0;
            this.rJ_Button_確認選擇.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認選擇.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認選擇.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認選擇.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認選擇.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認選擇.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認選擇.GUID = "";
            this.rJ_Button_確認選擇.Location = new System.Drawing.Point(807, 0);
            this.rJ_Button_確認選擇.Name = "rJ_Button_確認選擇";
            this.rJ_Button_確認選擇.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認選擇.ShadowSize = 3;
            this.rJ_Button_確認選擇.ShowLoadingForm = false;
            this.rJ_Button_確認選擇.Size = new System.Drawing.Size(135, 59);
            this.rJ_Button_確認選擇.State = false;
            this.rJ_Button_確認選擇.TabIndex = 25;
            this.rJ_Button_確認選擇.Text = "確認選擇";
            this.rJ_Button_確認選擇.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認選擇.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_藥品選擇_顯示全部
            // 
            this.rJ_Button_藥品選擇_顯示全部.AutoResetState = false;
            this.rJ_Button_藥品選擇_顯示全部.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_藥品選擇_顯示全部.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品選擇_顯示全部.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_藥品選擇_顯示全部.BorderRadius = 10;
            this.rJ_Button_藥品選擇_顯示全部.BorderSize = 0;
            this.rJ_Button_藥品選擇_顯示全部.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_藥品選擇_顯示全部.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品選擇_顯示全部.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品選擇_顯示全部.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥品選擇_顯示全部.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥品選擇_顯示全部.GUID = "";
            this.rJ_Button_藥品選擇_顯示全部.Location = new System.Drawing.Point(635, 3);
            this.rJ_Button_藥品選擇_顯示全部.Name = "rJ_Button_藥品選擇_顯示全部";
            this.rJ_Button_藥品選擇_顯示全部.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品選擇_顯示全部.ShadowSize = 3;
            this.rJ_Button_藥品選擇_顯示全部.ShowLoadingForm = false;
            this.rJ_Button_藥品選擇_顯示全部.Size = new System.Drawing.Size(135, 50);
            this.rJ_Button_藥品選擇_顯示全部.State = false;
            this.rJ_Button_藥品選擇_顯示全部.TabIndex = 24;
            this.rJ_Button_藥品選擇_顯示全部.Text = "顯示全部";
            this.rJ_Button_藥品選擇_顯示全部.TextColor = System.Drawing.Color.White;
            this.rJ_Button_藥品選擇_顯示全部.UseVisualStyleBackColor = false;
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
            this.rJ_Button_藥名搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥名搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥名搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥名搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥名搜尋.GUID = "";
            this.rJ_Button_藥名搜尋.Location = new System.Drawing.Point(541, 4);
            this.rJ_Button_藥名搜尋.Name = "rJ_Button_藥名搜尋";
            this.rJ_Button_藥名搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥名搜尋.ShadowSize = 3;
            this.rJ_Button_藥名搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥名搜尋.Size = new System.Drawing.Size(78, 50);
            this.rJ_Button_藥名搜尋.State = false;
            this.rJ_Button_藥名搜尋.TabIndex = 23;
            this.rJ_Button_藥名搜尋.Text = "搜尋";
            this.rJ_Button_藥名搜尋.TextColor = System.Drawing.Color.White;
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
            this.rJ_TextBox_藥名搜尋.Location = new System.Drawing.Point(301, 8);
            this.rJ_TextBox_藥名搜尋.Multiline = false;
            this.rJ_TextBox_藥名搜尋.Name = "rJ_TextBox_藥名搜尋";
            this.rJ_TextBox_藥名搜尋.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥名搜尋.PassWordChar = false;
            this.rJ_TextBox_藥名搜尋.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥名搜尋.PlaceholderText = "請輸入藥名...";
            this.rJ_TextBox_藥名搜尋.ShowTouchPannel = false;
            this.rJ_TextBox_藥名搜尋.Size = new System.Drawing.Size(234, 42);
            this.rJ_TextBox_藥名搜尋.TabIndex = 22;
            this.rJ_TextBox_藥名搜尋.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥名搜尋.Texts = "";
            this.rJ_TextBox_藥名搜尋.UnderlineStyle = false;
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
            this.rJ_Button_藥碼搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥碼搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥碼搜尋.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_藥碼搜尋.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_藥碼搜尋.GUID = "";
            this.rJ_Button_藥碼搜尋.Location = new System.Drawing.Point(217, 4);
            this.rJ_Button_藥碼搜尋.Name = "rJ_Button_藥碼搜尋";
            this.rJ_Button_藥碼搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥碼搜尋.ShadowSize = 3;
            this.rJ_Button_藥碼搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥碼搜尋.Size = new System.Drawing.Size(78, 50);
            this.rJ_Button_藥碼搜尋.State = false;
            this.rJ_Button_藥碼搜尋.TabIndex = 21;
            this.rJ_Button_藥碼搜尋.Text = "搜尋";
            this.rJ_Button_藥碼搜尋.TextColor = System.Drawing.Color.White;
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
            this.rJ_TextBox_藥碼搜尋.Location = new System.Drawing.Point(6, 8);
            this.rJ_TextBox_藥碼搜尋.Multiline = false;
            this.rJ_TextBox_藥碼搜尋.Name = "rJ_TextBox_藥碼搜尋";
            this.rJ_TextBox_藥碼搜尋.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_藥碼搜尋.PassWordChar = false;
            this.rJ_TextBox_藥碼搜尋.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_藥碼搜尋.PlaceholderText = "請輸入藥碼...";
            this.rJ_TextBox_藥碼搜尋.ShowTouchPannel = false;
            this.rJ_TextBox_藥碼搜尋.Size = new System.Drawing.Size(205, 42);
            this.rJ_TextBox_藥碼搜尋.TabIndex = 10;
            this.rJ_TextBox_藥碼搜尋.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_藥碼搜尋.Texts = "";
            this.rJ_TextBox_藥碼搜尋.UnderlineStyle = false;
            // 
            // sqL_DataGridView_藥品選擇
            // 
            this.sqL_DataGridView_藥品選擇.AutoSelectToDeep = true;
            this.sqL_DataGridView_藥品選擇.backColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_藥品選擇.BorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_藥品選擇.BorderRadius = 0;
            this.sqL_DataGridView_藥品選擇.BorderSize = 2;
            this.sqL_DataGridView_藥品選擇.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_藥品選擇.cellStylBackColor = System.Drawing.Color.PowderBlue;
            this.sqL_DataGridView_藥品選擇.cellStyleFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品選擇.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品選擇.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥品選擇.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品選擇.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品選擇.columnHeadersHeight = 40;
            this.sqL_DataGridView_藥品選擇.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_藥品選擇.Dock = System.Windows.Forms.DockStyle.Top;
            this.sqL_DataGridView_藥品選擇.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品選擇.ImageBox = false;
            this.sqL_DataGridView_藥品選擇.Location = new System.Drawing.Point(4, 140);
            this.sqL_DataGridView_藥品選擇.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.sqL_DataGridView_藥品選擇.Name = "sqL_DataGridView_藥品選擇";
            this.sqL_DataGridView_藥品選擇.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥品選擇.Password = "user82822040";
            this.sqL_DataGridView_藥品選擇.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥品選擇.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_藥品選擇.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品選擇.RowsColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_藥品選擇.RowsHeight = 80;
            this.sqL_DataGridView_藥品選擇.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥品選擇.Server = "127.0.0.0";
            this.sqL_DataGridView_藥品選擇.Size = new System.Drawing.Size(942, 441);
            this.sqL_DataGridView_藥品選擇.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品選擇.TabIndex = 17;
            this.sqL_DataGridView_藥品選擇.UserName = "root";
            this.sqL_DataGridView_藥品選擇.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_藥品選擇.可選擇多列 = false;
            this.sqL_DataGridView_藥品選擇.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_藥品選擇.自動換行 = true;
            this.sqL_DataGridView_藥品選擇.表單字體 = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品選擇.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_藥品選擇.顯示CheckBox = false;
            this.sqL_DataGridView_藥品選擇.顯示首列 = false;
            this.sqL_DataGridView_藥品選擇.顯示首行 = false;
            this.sqL_DataGridView_藥品選擇.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_藥品選擇.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // rJ_Lable2
            // 
            this.rJ_Lable2.BackColor = System.Drawing.Color.White;
            this.rJ_Lable2.BackgroundColor = System.Drawing.Color.DarkGray;
            this.rJ_Lable2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable2.BorderRadius = 10;
            this.rJ_Lable2.BorderSize = 0;
            this.rJ_Lable2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable2.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable2.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable2.GUID = "";
            this.rJ_Lable2.Location = new System.Drawing.Point(4, 581);
            this.rJ_Lable2.Name = "rJ_Lable2";
            this.rJ_Lable2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable2.ShadowSize = 3;
            this.rJ_Lable2.Size = new System.Drawing.Size(942, 71);
            this.rJ_Lable2.TabIndex = 18;
            this.rJ_Lable2.Text = "已 選 藥 品";
            this.rJ_Lable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable2.TextColor = System.Drawing.Color.White;
            // 
            // sqL_DataGridView_已選藥品
            // 
            this.sqL_DataGridView_已選藥品.AutoSelectToDeep = true;
            this.sqL_DataGridView_已選藥品.backColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_已選藥品.BorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_已選藥品.BorderRadius = 0;
            this.sqL_DataGridView_已選藥品.BorderSize = 2;
            this.sqL_DataGridView_已選藥品.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_已選藥品.cellStylBackColor = System.Drawing.Color.PowderBlue;
            this.sqL_DataGridView_已選藥品.cellStyleFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_已選藥品.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_已選藥品.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_已選藥品.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_已選藥品.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_已選藥品.columnHeadersHeight = 40;
            this.sqL_DataGridView_已選藥品.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_已選藥品.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_已選藥品.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_已選藥品.ImageBox = false;
            this.sqL_DataGridView_已選藥品.Location = new System.Drawing.Point(4, 652);
            this.sqL_DataGridView_已選藥品.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.sqL_DataGridView_已選藥品.Name = "sqL_DataGridView_已選藥品";
            this.sqL_DataGridView_已選藥品.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_已選藥品.Password = "user82822040";
            this.sqL_DataGridView_已選藥品.Port = ((uint)(3306u));
            this.sqL_DataGridView_已選藥品.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_已選藥品.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_已選藥品.RowsColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_已選藥品.RowsHeight = 80;
            this.sqL_DataGridView_已選藥品.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_已選藥品.Server = "127.0.0.0";
            this.sqL_DataGridView_已選藥品.Size = new System.Drawing.Size(942, 228);
            this.sqL_DataGridView_已選藥品.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_已選藥品.TabIndex = 19;
            this.sqL_DataGridView_已選藥品.UserName = "root";
            this.sqL_DataGridView_已選藥品.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_已選藥品.可選擇多列 = false;
            this.sqL_DataGridView_已選藥品.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_已選藥品.自動換行 = true;
            this.sqL_DataGridView_已選藥品.表單字體 = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_已選藥品.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_已選藥品.顯示CheckBox = false;
            this.sqL_DataGridView_已選藥品.顯示首列 = false;
            this.sqL_DataGridView_已選藥品.顯示首行 = false;
            this.sqL_DataGridView_已選藥品.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_已選藥品.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // Dialog_手動選擇備藥品
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 6;
            this.ClientSize = new System.Drawing.Size(950, 1000);
            this.Controls.Add(this.sqL_DataGridView_已選藥品);
            this.Controls.Add(this.rJ_Lable2);
            this.Controls.Add(this.sqL_DataGridView_藥品選擇);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rJ_Lable1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_手動選擇備藥品";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_Lable rJ_Lable1;
        private System.Windows.Forms.Panel panel1;
        private MyUI.RJ_Button rJ_Button_確認;
        private MyUI.RJ_Button rJ_Button1;
        private MyUI.RJ_Button rJ_Button_取消;
        private System.Windows.Forms.Panel panel2;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品選擇;
        private MyUI.RJ_Lable rJ_Lable2;
        private MyUI.RJ_Button rJ_Button_藥品選擇_顯示全部;
        private MyUI.RJ_Button rJ_Button_藥名搜尋;
        private MyUI.RJ_TextBox rJ_TextBox_藥名搜尋;
        private MyUI.RJ_Button rJ_Button_藥碼搜尋;
        private MyUI.RJ_TextBox rJ_TextBox_藥碼搜尋;
        private MyUI.RJ_Button rJ_Button_確認選擇;
        private SQLUI.SQL_DataGridView sqL_DataGridView_已選藥品;
    }
}