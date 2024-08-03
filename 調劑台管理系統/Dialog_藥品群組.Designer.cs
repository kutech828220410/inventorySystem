
namespace 調劑台管理系統
{
    partial class Dialog_藥品群組
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
            this.panel_藥品選擇 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_藥品群組 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Button_藥品群組_刪除 = new MyUI.PLC_RJ_Button();
            this.rJ_Button_藥品群組_新增 = new MyUI.PLC_RJ_Button();
            this.rJ_Button_藥品群組_選擇 = new MyUI.PLC_RJ_Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rJ_Button_藥品搜尋 = new MyUI.PLC_RJ_Button();
            this.comboBox_藥品資料_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_藥品資料 = new SQLUI.SQL_DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel_藥品選擇.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_藥品選擇
            // 
            this.panel_藥品選擇.Controls.Add(this.rJ_Button_藥品群組_刪除);
            this.panel_藥品選擇.Controls.Add(this.rJ_Button_藥品群組_新增);
            this.panel_藥品選擇.Controls.Add(this.rJ_Button_藥品群組_選擇);
            this.panel_藥品選擇.Controls.Add(this.label1);
            this.panel_藥品選擇.Controls.Add(this.comboBox_藥品群組);
            this.panel_藥品選擇.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_藥品選擇.Location = new System.Drawing.Point(0, 0);
            this.panel_藥品選擇.Name = "panel_藥品選擇";
            this.panel_藥品選擇.Size = new System.Drawing.Size(862, 110);
            this.panel_藥品選擇.TabIndex = 141;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(16, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "藥品群組:";
            // 
            // comboBox_藥品群組
            // 
            this.comboBox_藥品群組.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_藥品群組.FormattingEnabled = true;
            this.comboBox_藥品群組.Location = new System.Drawing.Point(173, 31);
            this.comboBox_藥品群組.Name = "comboBox_藥品群組";
            this.comboBox_藥品群組.Size = new System.Drawing.Size(296, 44);
            this.comboBox_藥品群組.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1520, 14);
            this.panel1.TabIndex = 142;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel_藥品選擇);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1520, 110);
            this.panel2.TabIndex = 143;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(4, 855);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1520, 100);
            this.panel3.TabIndex = 144;
            // 
            // rJ_Button_藥品群組_刪除
            // 
            this.rJ_Button_藥品群組_刪除.AutoResetState = true;
            this.rJ_Button_藥品群組_刪除.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_刪除.Bool = false;
            this.rJ_Button_藥品群組_刪除.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_刪除.BorderRadius = 15;
            this.rJ_Button_藥品群組_刪除.BorderSize = 1;
            this.rJ_Button_藥品群組_刪除.but_press = false;
            this.rJ_Button_藥品群組_刪除.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.rJ_Button_藥品群組_刪除.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品群組_刪除.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品群組_刪除.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品群組_刪除.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_刪除.GUID = "";
            this.rJ_Button_藥品群組_刪除.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.rJ_Button_藥品群組_刪除.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.rJ_Button_藥品群組_刪除.Location = new System.Drawing.Point(693, 5);
            this.rJ_Button_藥品群組_刪除.Name = "rJ_Button_藥品群組_刪除";
            this.rJ_Button_藥品群組_刪除.OFF_文字內容 = "刪除";
            this.rJ_Button_藥品群組_刪除.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_刪除.OFF_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_刪除.OFF_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_刪除.ON_BorderSize = 1;
            this.rJ_Button_藥品群組_刪除.ON_文字內容 = "刪除";
            this.rJ_Button_藥品群組_刪除.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_刪除.ON_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_刪除.ON_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_刪除.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品群組_刪除.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品群組_刪除.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品群組_刪除.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品群組_刪除.ShadowSize = 3;
            this.rJ_Button_藥品群組_刪除.ShowLoadingForm = false;
            this.rJ_Button_藥品群組_刪除.Size = new System.Drawing.Size(102, 102);
            this.rJ_Button_藥品群組_刪除.State = false;
            this.rJ_Button_藥品群組_刪除.TabIndex = 149;
            this.rJ_Button_藥品群組_刪除.Text = "刪除";
            this.rJ_Button_藥品群組_刪除.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_刪除.TextHeight = 35;
            this.rJ_Button_藥品群組_刪除.Texts = "刪除";
            this.rJ_Button_藥品群組_刪除.UseVisualStyleBackColor = false;
            this.rJ_Button_藥品群組_刪除.字型鎖住 = false;
            this.rJ_Button_藥品群組_刪除.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.rJ_Button_藥品群組_刪除.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.rJ_Button_藥品群組_刪除.文字鎖住 = false;
            this.rJ_Button_藥品群組_刪除.背景圖片 = global::調劑台管理系統.Properties.Resources.delete_data_removebg_preview;
            this.rJ_Button_藥品群組_刪除.讀取位元反向 = false;
            this.rJ_Button_藥品群組_刪除.讀寫鎖住 = false;
            this.rJ_Button_藥品群組_刪除.音效 = false;
            this.rJ_Button_藥品群組_刪除.顯示 = false;
            this.rJ_Button_藥品群組_刪除.顯示狀態 = false;
            // 
            // rJ_Button_藥品群組_新增
            // 
            this.rJ_Button_藥品群組_新增.AutoResetState = true;
            this.rJ_Button_藥品群組_新增.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_新增.Bool = false;
            this.rJ_Button_藥品群組_新增.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_新增.BorderRadius = 15;
            this.rJ_Button_藥品群組_新增.BorderSize = 1;
            this.rJ_Button_藥品群組_新增.but_press = false;
            this.rJ_Button_藥品群組_新增.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.rJ_Button_藥品群組_新增.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品群組_新增.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品群組_新增.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品群組_新增.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_新增.GUID = "";
            this.rJ_Button_藥品群組_新增.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.rJ_Button_藥品群組_新增.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.rJ_Button_藥品群組_新增.Location = new System.Drawing.Point(589, 5);
            this.rJ_Button_藥品群組_新增.Name = "rJ_Button_藥品群組_新增";
            this.rJ_Button_藥品群組_新增.OFF_文字內容 = "新增";
            this.rJ_Button_藥品群組_新增.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_新增.OFF_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_新增.OFF_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_新增.ON_BorderSize = 1;
            this.rJ_Button_藥品群組_新增.ON_文字內容 = "新增";
            this.rJ_Button_藥品群組_新增.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_新增.ON_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_新增.ON_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_新增.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品群組_新增.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品群組_新增.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品群組_新增.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品群組_新增.ShadowSize = 3;
            this.rJ_Button_藥品群組_新增.ShowLoadingForm = false;
            this.rJ_Button_藥品群組_新增.Size = new System.Drawing.Size(102, 102);
            this.rJ_Button_藥品群組_新增.State = false;
            this.rJ_Button_藥品群組_新增.TabIndex = 148;
            this.rJ_Button_藥品群組_新增.Text = "新增";
            this.rJ_Button_藥品群組_新增.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_新增.TextHeight = 35;
            this.rJ_Button_藥品群組_新增.Texts = "新增";
            this.rJ_Button_藥品群組_新增.UseVisualStyleBackColor = false;
            this.rJ_Button_藥品群組_新增.字型鎖住 = false;
            this.rJ_Button_藥品群組_新增.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.rJ_Button_藥品群組_新增.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.rJ_Button_藥品群組_新增.文字鎖住 = false;
            this.rJ_Button_藥品群組_新增.背景圖片 = global::調劑台管理系統.Properties.Resources.plus_removebg_preview;
            this.rJ_Button_藥品群組_新增.讀取位元反向 = false;
            this.rJ_Button_藥品群組_新增.讀寫鎖住 = false;
            this.rJ_Button_藥品群組_新增.音效 = false;
            this.rJ_Button_藥品群組_新增.顯示 = false;
            this.rJ_Button_藥品群組_新增.顯示狀態 = false;
            // 
            // rJ_Button_藥品群組_選擇
            // 
            this.rJ_Button_藥品群組_選擇.AutoResetState = true;
            this.rJ_Button_藥品群組_選擇.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_選擇.Bool = false;
            this.rJ_Button_藥品群組_選擇.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_選擇.BorderRadius = 15;
            this.rJ_Button_藥品群組_選擇.BorderSize = 1;
            this.rJ_Button_藥品群組_選擇.but_press = false;
            this.rJ_Button_藥品群組_選擇.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.rJ_Button_藥品群組_選擇.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品群組_選擇.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品群組_選擇.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品群組_選擇.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_選擇.GUID = "";
            this.rJ_Button_藥品群組_選擇.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.rJ_Button_藥品群組_選擇.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.rJ_Button_藥品群組_選擇.Location = new System.Drawing.Point(485, 5);
            this.rJ_Button_藥品群組_選擇.Name = "rJ_Button_藥品群組_選擇";
            this.rJ_Button_藥品群組_選擇.OFF_文字內容 = "選擇";
            this.rJ_Button_藥品群組_選擇.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_選擇.OFF_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_選擇.OFF_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_選擇.ON_BorderSize = 1;
            this.rJ_Button_藥品群組_選擇.ON_文字內容 = "選擇";
            this.rJ_Button_藥品群組_選擇.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品群組_選擇.ON_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_選擇.ON_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品群組_選擇.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品群組_選擇.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品群組_選擇.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品群組_選擇.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品群組_選擇.ShadowSize = 3;
            this.rJ_Button_藥品群組_選擇.ShowLoadingForm = false;
            this.rJ_Button_藥品群組_選擇.Size = new System.Drawing.Size(102, 102);
            this.rJ_Button_藥品群組_選擇.State = false;
            this.rJ_Button_藥品群組_選擇.TabIndex = 147;
            this.rJ_Button_藥品群組_選擇.Text = "選擇";
            this.rJ_Button_藥品群組_選擇.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品群組_選擇.TextHeight = 35;
            this.rJ_Button_藥品群組_選擇.Texts = "選擇";
            this.rJ_Button_藥品群組_選擇.UseVisualStyleBackColor = false;
            this.rJ_Button_藥品群組_選擇.字型鎖住 = false;
            this.rJ_Button_藥品群組_選擇.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.rJ_Button_藥品群組_選擇.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.rJ_Button_藥品群組_選擇.文字鎖住 = false;
            this.rJ_Button_藥品群組_選擇.背景圖片 = global::調劑台管理系統.Properties.Resources.adjusted_checkmark_removebg_preview;
            this.rJ_Button_藥品群組_選擇.讀取位元反向 = false;
            this.rJ_Button_藥品群組_選擇.讀寫鎖住 = false;
            this.rJ_Button_藥品群組_選擇.音效 = false;
            this.rJ_Button_藥品群組_選擇.顯示 = false;
            this.rJ_Button_藥品群組_選擇.顯示狀態 = false;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.comboBox1);
            this.panel4.Controls.Add(this.rJ_Button_藥品搜尋);
            this.panel4.Controls.Add(this.comboBox_藥品資料_搜尋條件);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(862, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(658, 110);
            this.panel4.TabIndex = 142;
            // 
            // rJ_Button_藥品搜尋
            // 
            this.rJ_Button_藥品搜尋.AutoResetState = true;
            this.rJ_Button_藥品搜尋.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_藥品搜尋.Bool = false;
            this.rJ_Button_藥品搜尋.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.BorderRadius = 15;
            this.rJ_Button_藥品搜尋.BorderSize = 1;
            this.rJ_Button_藥品搜尋.but_press = false;
            this.rJ_Button_藥品搜尋.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.rJ_Button_藥品搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_藥品搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_藥品搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_藥品搜尋.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品搜尋.GUID = "";
            this.rJ_Button_藥品搜尋.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.rJ_Button_藥品搜尋.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.rJ_Button_藥品搜尋.Location = new System.Drawing.Point(538, 4);
            this.rJ_Button_藥品搜尋.Name = "rJ_Button_藥品搜尋";
            this.rJ_Button_藥品搜尋.OFF_文字內容 = "搜尋";
            this.rJ_Button_藥品搜尋.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品搜尋.OFF_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.OFF_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品搜尋.ON_BorderSize = 1;
            this.rJ_Button_藥品搜尋.ON_文字內容 = "搜尋";
            this.rJ_Button_藥品搜尋.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_藥品搜尋.ON_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.ON_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_藥品搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_藥品搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_藥品搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_藥品搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_藥品搜尋.ShadowSize = 3;
            this.rJ_Button_藥品搜尋.ShowLoadingForm = false;
            this.rJ_Button_藥品搜尋.Size = new System.Drawing.Size(102, 102);
            this.rJ_Button_藥品搜尋.State = false;
            this.rJ_Button_藥品搜尋.TabIndex = 151;
            this.rJ_Button_藥品搜尋.Text = "搜尋";
            this.rJ_Button_藥品搜尋.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_藥品搜尋.TextHeight = 35;
            this.rJ_Button_藥品搜尋.Texts = "搜尋";
            this.rJ_Button_藥品搜尋.UseVisualStyleBackColor = false;
            this.rJ_Button_藥品搜尋.字型鎖住 = false;
            this.rJ_Button_藥品搜尋.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.rJ_Button_藥品搜尋.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.rJ_Button_藥品搜尋.文字鎖住 = false;
            this.rJ_Button_藥品搜尋.背景圖片 = global::調劑台管理系統.Properties.Resources.icon_for_searching_medicine_removebg_preview;
            this.rJ_Button_藥品搜尋.讀取位元反向 = false;
            this.rJ_Button_藥品搜尋.讀寫鎖住 = false;
            this.rJ_Button_藥品搜尋.音效 = false;
            this.rJ_Button_藥品搜尋.顯示 = false;
            this.rJ_Button_藥品搜尋.顯示狀態 = false;
            // 
            // comboBox_藥品資料_搜尋條件
            // 
            this.comboBox_藥品資料_搜尋條件.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_藥品資料_搜尋條件.FormattingEnabled = true;
            this.comboBox_藥品資料_搜尋條件.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox_藥品資料_搜尋條件.Location = new System.Drawing.Point(19, 31);
            this.comboBox_藥品資料_搜尋條件.Name = "comboBox_藥品資料_搜尋條件";
            this.comboBox_藥品資料_搜尋條件.Size = new System.Drawing.Size(179, 44);
            this.comboBox_藥品資料_搜尋條件.TabIndex = 149;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 152);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1520, 17);
            this.panel5.TabIndex = 145;
            // 
            // sqL_DataGridView_藥品資料
            // 
            this.sqL_DataGridView_藥品資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_藥品資料.backColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_藥品資料.BorderColor = System.Drawing.Color.Transparent;
            this.sqL_DataGridView_藥品資料.BorderRadius = 0;
            this.sqL_DataGridView_藥品資料.BorderSize = 0;
            this.sqL_DataGridView_藥品資料.CellBorderColor = System.Drawing.Color.WhiteSmoke;
            this.sqL_DataGridView_藥品資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_藥品資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_藥品資料.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_藥品資料.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_藥品資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_藥品資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.columnHeadersHeight = 40;
            this.sqL_DataGridView_藥品資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.sqL_DataGridView_藥品資料.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_藥品資料.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_藥品資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品資料.ImageBox = false;
            this.sqL_DataGridView_藥品資料.Location = new System.Drawing.Point(4, 169);
            this.sqL_DataGridView_藥品資料.Name = "sqL_DataGridView_藥品資料";
            this.sqL_DataGridView_藥品資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_藥品資料.Password = "user82822040";
            this.sqL_DataGridView_藥品資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_藥品資料.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_藥品資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.RowsColor = System.Drawing.SystemColors.Window;
            this.sqL_DataGridView_藥品資料.RowsHeight = 40;
            this.sqL_DataGridView_藥品資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_藥品資料.selectedBorderSize = 2;
            this.sqL_DataGridView_藥品資料.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_藥品資料.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_藥品資料.Server = "127.0.0.0";
            this.sqL_DataGridView_藥品資料.Size = new System.Drawing.Size(1520, 686);
            this.sqL_DataGridView_藥品資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_藥品資料.TabIndex = 146;
            this.sqL_DataGridView_藥品資料.UserName = "root";
            this.sqL_DataGridView_藥品資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_藥品資料.可選擇多列 = false;
            this.sqL_DataGridView_藥品資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_藥品資料.自動換行 = true;
            this.sqL_DataGridView_藥品資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_藥品資料.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_藥品資料.顯示CheckBox = false;
            this.sqL_DataGridView_藥品資料.顯示首列 = true;
            this.sqL_DataGridView_藥品資料.顯示首行 = true;
            this.sqL_DataGridView_藥品資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_藥品資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox1.Location = new System.Drawing.Point(221, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(296, 44);
            this.comboBox1.TabIndex = 152;
            // 
            // Dialog_藥品群組
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1528, 959);
            this.ControlBox = true;
            this.Controls.Add(this.sqL_DataGridView_藥品資料);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_藥品群組";
            this.Text = "藥品群組";
            this.panel_藥品選擇.ResumeLayout(false);
            this.panel_藥品選擇.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_藥品選擇;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_藥品群組;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private MyUI.PLC_RJ_Button rJ_Button_藥品群組_選擇;
        private MyUI.PLC_RJ_Button rJ_Button_藥品群組_新增;
        private MyUI.PLC_RJ_Button rJ_Button_藥品群組_刪除;
        private System.Windows.Forms.Panel panel4;
        private MyUI.PLC_RJ_Button rJ_Button_藥品搜尋;
        private System.Windows.Forms.ComboBox comboBox_藥品資料_搜尋條件;
        private System.Windows.Forms.Panel panel5;
        private SQLUI.SQL_DataGridView sqL_DataGridView_藥品資料;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}