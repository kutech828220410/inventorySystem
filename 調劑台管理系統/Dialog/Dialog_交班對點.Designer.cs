
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
            this.panel9 = new System.Windows.Forms.Panel();
            this.stepViewer = new MyUI.StepViewer();
            this.plC_RJ_Button_確認送出 = new MyUI.PLC_RJ_Button();
            this.panel_藥品選擇 = new System.Windows.Forms.Panel();
            this.rJ_Button_藥品群組_選擇 = new MyUI.RJ_Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_藥品群組 = new System.Windows.Forms.ComboBox();
            this.plC_RJ_Button_覆盤登入 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_盤點登入 = new MyUI.PLC_RJ_Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.sqL_DataGridView_交班藥品 = new SQLUI.SQL_DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_Lable_覆盤人員 = new MyUI.RJ_Lable();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rJ_Lable_盤點人員 = new MyUI.RJ_Lable();
            this.panel_交班內容 = new System.Windows.Forms.Panel();
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.rJ_Lable_狀態 = new MyUI.RJ_Lable();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.rJ_Button_確認輸入 = new MyUI.RJ_Button();
            this.rJ_TextBox_盤點量 = new MyUI.RJ_TextBox();
            this.rJ_Pannel2 = new MyUI.RJ_Pannel();
            this.rJ_Button_1 = new MyUI.RJ_Button();
            this.rJ_Button_CE = new MyUI.RJ_Button();
            this.rJ_Button_6 = new MyUI.RJ_Button();
            this.rJ_Button_7 = new MyUI.RJ_Button();
            this.rJ_Button_2 = new MyUI.RJ_Button();
            this.rJ_Button_5 = new MyUI.RJ_Button();
            this.rJ_Button_8 = new MyUI.RJ_Button();
            this.rJ_Button_0 = new MyUI.RJ_Button();
            this.rJ_Button_4 = new MyUI.RJ_Button();
            this.rJ_Button_9 = new MyUI.RJ_Button();
            this.rJ_Button_3 = new MyUI.RJ_Button();
            this.rJ_Lable3 = new MyUI.RJ_Lable();
            this.rJ_Lable_現有庫存 = new MyUI.RJ_Lable();
            this.pictureBox_藥品資訊 = new System.Windows.Forms.PictureBox();
            this.rJ_Lable_藥品資訊 = new MyUI.RJ_Lable();
            this.panel9.SuspendLayout();
            this.panel_藥品選擇.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_交班內容.SuspendLayout();
            this.rJ_Pannel1.SuspendLayout();
            this.rJ_Pannel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品資訊)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1792, 16);
            this.panel1.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.stepViewer);
            this.panel9.Controls.Add(this.plC_RJ_Button_確認送出);
            this.panel9.Controls.Add(this.panel_藥品選擇);
            this.panel9.Controls.Add(this.plC_RJ_Button_覆盤登入);
            this.panel9.Controls.Add(this.plC_RJ_Button_盤點登入);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(4, 44);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1792, 110);
            this.panel9.TabIndex = 11;
            // 
            // stepViewer
            // 
            this.stepViewer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.stepViewer.CurrentStep = 0;
            this.stepViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepViewer.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stepViewer.LineWidth = 80;
            this.stepViewer.ListDataSource = null;
            this.stepViewer.Location = new System.Drawing.Point(847, 0);
            this.stepViewer.Margin = new System.Windows.Forms.Padding(4);
            this.stepViewer.Name = "stepViewer";
            this.stepViewer.Size = new System.Drawing.Size(837, 110);
            this.stepViewer.TabIndex = 143;
            // 
            // plC_RJ_Button_確認送出
            // 
            this.plC_RJ_Button_確認送出.AutoResetState = true;
            this.plC_RJ_Button_確認送出.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_確認送出.Bool = false;
            this.plC_RJ_Button_確認送出.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.BorderRadius = 15;
            this.plC_RJ_Button_確認送出.BorderSize = 1;
            this.plC_RJ_Button_確認送出.but_press = false;
            this.plC_RJ_Button_確認送出.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_確認送出.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_確認送出.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_確認送出.Enabled = false;
            this.plC_RJ_Button_確認送出.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_確認送出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_確認送出.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認送出.GUID = "";
            this.plC_RJ_Button_確認送出.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_確認送出.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_確認送出.Location = new System.Drawing.Point(1684, 0);
            this.plC_RJ_Button_確認送出.Name = "plC_RJ_Button_確認送出";
            this.plC_RJ_Button_確認送出.OFF_文字內容 = "確認送出";
            this.plC_RJ_Button_確認送出.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認送出.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認送出.ON_BorderSize = 1;
            this.plC_RJ_Button_確認送出.ON_文字內容 = "確認送出";
            this.plC_RJ_Button_確認送出.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認送出.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.ON_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認送出.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_確認送出.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_確認送出.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_確認送出.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_確認送出.ShadowSize = 3;
            this.plC_RJ_Button_確認送出.ShowLoadingForm = false;
            this.plC_RJ_Button_確認送出.Size = new System.Drawing.Size(108, 110);
            this.plC_RJ_Button_確認送出.State = false;
            this.plC_RJ_Button_確認送出.TabIndex = 141;
            this.plC_RJ_Button_確認送出.Text = "確認送出";
            this.plC_RJ_Button_確認送出.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認送出.TextHeight = 35;
            this.plC_RJ_Button_確認送出.Texts = "確認送出";
            this.plC_RJ_Button_確認送出.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_確認送出.字型鎖住 = false;
            this.plC_RJ_Button_確認送出.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_確認送出.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_確認送出.文字鎖住 = false;
            this.plC_RJ_Button_確認送出.背景圖片 = global::調劑台管理系統.Properties.Resources.adjusted_checkmark_removebg_preview;
            this.plC_RJ_Button_確認送出.讀取位元反向 = false;
            this.plC_RJ_Button_確認送出.讀寫鎖住 = false;
            this.plC_RJ_Button_確認送出.音效 = false;
            this.plC_RJ_Button_確認送出.顯示 = false;
            this.plC_RJ_Button_確認送出.顯示狀態 = false;
            // 
            // panel_藥品選擇
            // 
            this.panel_藥品選擇.Controls.Add(this.rJ_Button_藥品群組_選擇);
            this.panel_藥品選擇.Controls.Add(this.label1);
            this.panel_藥品選擇.Controls.Add(this.comboBox_藥品群組);
            this.panel_藥品選擇.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_藥品選擇.Location = new System.Drawing.Point(216, 0);
            this.panel_藥品選擇.Name = "panel_藥品選擇";
            this.panel_藥品選擇.Size = new System.Drawing.Size(631, 110);
            this.panel_藥品選擇.TabIndex = 139;
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
            this.rJ_Button_藥品群組_選擇.Location = new System.Drawing.Point(507, 23);
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
            this.comboBox_藥品群組.Size = new System.Drawing.Size(328, 44);
            this.comboBox_藥品群組.TabIndex = 8;
            // 
            // plC_RJ_Button_覆盤登入
            // 
            this.plC_RJ_Button_覆盤登入.AutoResetState = true;
            this.plC_RJ_Button_覆盤登入.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_覆盤登入.Bool = false;
            this.plC_RJ_Button_覆盤登入.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_覆盤登入.BorderRadius = 15;
            this.plC_RJ_Button_覆盤登入.BorderSize = 1;
            this.plC_RJ_Button_覆盤登入.but_press = false;
            this.plC_RJ_Button_覆盤登入.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_覆盤登入.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_覆盤登入.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_Button_覆盤登入.Enabled = false;
            this.plC_RJ_Button_覆盤登入.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_覆盤登入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_覆盤登入.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_覆盤登入.GUID = "";
            this.plC_RJ_Button_覆盤登入.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_覆盤登入.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_覆盤登入.Location = new System.Drawing.Point(108, 0);
            this.plC_RJ_Button_覆盤登入.Name = "plC_RJ_Button_覆盤登入";
            this.plC_RJ_Button_覆盤登入.OFF_文字內容 = "覆盤登入";
            this.plC_RJ_Button_覆盤登入.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_覆盤登入.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_覆盤登入.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_覆盤登入.ON_BorderSize = 1;
            this.plC_RJ_Button_覆盤登入.ON_文字內容 = "覆盤登入";
            this.plC_RJ_Button_覆盤登入.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_覆盤登入.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_覆盤登入.ON_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_覆盤登入.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_覆盤登入.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_覆盤登入.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_覆盤登入.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_覆盤登入.ShadowSize = 3;
            this.plC_RJ_Button_覆盤登入.ShowLoadingForm = false;
            this.plC_RJ_Button_覆盤登入.Size = new System.Drawing.Size(108, 110);
            this.plC_RJ_Button_覆盤登入.State = false;
            this.plC_RJ_Button_覆盤登入.TabIndex = 136;
            this.plC_RJ_Button_覆盤登入.Text = "覆盤登入";
            this.plC_RJ_Button_覆盤登入.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_覆盤登入.TextHeight = 35;
            this.plC_RJ_Button_覆盤登入.Texts = "覆盤登入";
            this.plC_RJ_Button_覆盤登入.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_覆盤登入.字型鎖住 = false;
            this.plC_RJ_Button_覆盤登入.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_覆盤登入.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_覆盤登入.文字鎖住 = false;
            this.plC_RJ_Button_覆盤登入.背景圖片 = global::調劑台管理系統.Properties.Resources.personnel_information_and_permission_login_removebg_preview;
            this.plC_RJ_Button_覆盤登入.讀取位元反向 = false;
            this.plC_RJ_Button_覆盤登入.讀寫鎖住 = false;
            this.plC_RJ_Button_覆盤登入.音效 = false;
            this.plC_RJ_Button_覆盤登入.顯示 = false;
            this.plC_RJ_Button_覆盤登入.顯示狀態 = false;
            // 
            // plC_RJ_Button_盤點登入
            // 
            this.plC_RJ_Button_盤點登入.AutoResetState = true;
            this.plC_RJ_Button_盤點登入.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_盤點登入.Bool = false;
            this.plC_RJ_Button_盤點登入.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_盤點登入.BorderRadius = 15;
            this.plC_RJ_Button_盤點登入.BorderSize = 1;
            this.plC_RJ_Button_盤點登入.but_press = false;
            this.plC_RJ_Button_盤點登入.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_盤點登入.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_盤點登入.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_Button_盤點登入.Enabled = false;
            this.plC_RJ_Button_盤點登入.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_盤點登入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_盤點登入.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_盤點登入.GUID = "";
            this.plC_RJ_Button_盤點登入.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_盤點登入.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_盤點登入.Location = new System.Drawing.Point(0, 0);
            this.plC_RJ_Button_盤點登入.Name = "plC_RJ_Button_盤點登入";
            this.plC_RJ_Button_盤點登入.OFF_文字內容 = "盤點登入";
            this.plC_RJ_Button_盤點登入.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_盤點登入.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_盤點登入.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_盤點登入.ON_BorderSize = 1;
            this.plC_RJ_Button_盤點登入.ON_文字內容 = "盤點登入";
            this.plC_RJ_Button_盤點登入.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_盤點登入.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_盤點登入.ON_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_盤點登入.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_盤點登入.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_盤點登入.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_盤點登入.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_盤點登入.ShadowSize = 3;
            this.plC_RJ_Button_盤點登入.ShowLoadingForm = false;
            this.plC_RJ_Button_盤點登入.Size = new System.Drawing.Size(108, 110);
            this.plC_RJ_Button_盤點登入.State = false;
            this.plC_RJ_Button_盤點登入.TabIndex = 135;
            this.plC_RJ_Button_盤點登入.Text = "盤點登入";
            this.plC_RJ_Button_盤點登入.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_盤點登入.TextHeight = 35;
            this.plC_RJ_Button_盤點登入.Texts = "盤點登入";
            this.plC_RJ_Button_盤點登入.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_盤點登入.字型鎖住 = false;
            this.plC_RJ_Button_盤點登入.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_盤點登入.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_盤點登入.文字鎖住 = false;
            this.plC_RJ_Button_盤點登入.背景圖片 = global::調劑台管理系統.Properties.Resources.personnel_information_and_permission_login_removebg_preview;
            this.plC_RJ_Button_盤點登入.讀取位元反向 = false;
            this.plC_RJ_Button_盤點登入.讀寫鎖住 = false;
            this.plC_RJ_Button_盤點登入.音效 = false;
            this.plC_RJ_Button_盤點登入.顯示 = false;
            this.plC_RJ_Button_盤點登入.顯示狀態 = false;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.sqL_DataGridView_交班藥品);
            this.panel8.Controls.Add(this.panel4);
            this.panel8.Controls.Add(this.panel2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(784, 762);
            this.panel8.TabIndex = 13;
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
            this.sqL_DataGridView_交班藥品.columnHeadersHeight = 18;
            this.sqL_DataGridView_交班藥品.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_交班藥品.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_交班藥品.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_交班藥品.Enabled = false;
            this.sqL_DataGridView_交班藥品.Font = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_交班藥品.ImageBox = false;
            this.sqL_DataGridView_交班藥品.Location = new System.Drawing.Point(0, 75);
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
            this.sqL_DataGridView_交班藥品.Size = new System.Drawing.Size(784, 687);
            this.sqL_DataGridView_交班藥品.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_交班藥品.TabIndex = 126;
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
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 65);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(784, 10);
            this.panel4.TabIndex = 125;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rJ_Lable_覆盤人員);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.rJ_Lable_盤點人員);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 65);
            this.panel2.TabIndex = 123;
            // 
            // rJ_Lable_覆盤人員
            // 
            this.rJ_Lable_覆盤人員.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_覆盤人員.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.rJ_Lable_覆盤人員.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable_覆盤人員.BorderRadius = 10;
            this.rJ_Lable_覆盤人員.BorderSize = 1;
            this.rJ_Lable_覆盤人員.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Lable_覆盤人員.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_覆盤人員.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_覆盤人員.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_覆盤人員.GUID = "";
            this.rJ_Lable_覆盤人員.Location = new System.Drawing.Point(415, 0);
            this.rJ_Lable_覆盤人員.Name = "rJ_Lable_覆盤人員";
            this.rJ_Lable_覆盤人員.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_覆盤人員.ShadowSize = 0;
            this.rJ_Lable_覆盤人員.Size = new System.Drawing.Size(369, 65);
            this.rJ_Lable_覆盤人員.TabIndex = 124;
            this.rJ_Lable_覆盤人員.Text = "覆盤人員 : -------";
            this.rJ_Lable_覆盤人員.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_覆盤人員.TextColor = System.Drawing.Color.Black;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(369, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(14, 65);
            this.panel3.TabIndex = 123;
            // 
            // rJ_Lable_盤點人員
            // 
            this.rJ_Lable_盤點人員.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_盤點人員.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.rJ_Lable_盤點人員.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable_盤點人員.BorderRadius = 10;
            this.rJ_Lable_盤點人員.BorderSize = 1;
            this.rJ_Lable_盤點人員.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_Lable_盤點人員.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_盤點人員.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_盤點人員.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_盤點人員.GUID = "";
            this.rJ_Lable_盤點人員.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable_盤點人員.Name = "rJ_Lable_盤點人員";
            this.rJ_Lable_盤點人員.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_盤點人員.ShadowSize = 0;
            this.rJ_Lable_盤點人員.Size = new System.Drawing.Size(369, 65);
            this.rJ_Lable_盤點人員.TabIndex = 122;
            this.rJ_Lable_盤點人員.Text = "盤點人員 : -------";
            this.rJ_Lable_盤點人員.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_盤點人員.TextColor = System.Drawing.Color.Black;
            // 
            // panel_交班內容
            // 
            this.panel_交班內容.Controls.Add(this.rJ_Pannel1);
            this.panel_交班內容.Controls.Add(this.panel8);
            this.panel_交班內容.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_交班內容.Location = new System.Drawing.Point(4, 154);
            this.panel_交班內容.Name = "panel_交班內容";
            this.panel_交班內容.Size = new System.Drawing.Size(1792, 762);
            this.panel_交班內容.TabIndex = 12;
            // 
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.BorderRadius = 10;
            this.rJ_Pannel1.BorderSize = 2;
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable_狀態);
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable1);
            this.rJ_Pannel1.Controls.Add(this.rJ_Button_確認輸入);
            this.rJ_Pannel1.Controls.Add(this.rJ_TextBox_盤點量);
            this.rJ_Pannel1.Controls.Add(this.rJ_Pannel2);
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable3);
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable_現有庫存);
            this.rJ_Pannel1.Controls.Add(this.pictureBox_藥品資訊);
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable_藥品資訊);
            this.rJ_Pannel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(784, 0);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.Padding = new System.Windows.Forms.Padding(10);
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel1.ShadowSize = 3;
            this.rJ_Pannel1.Size = new System.Drawing.Size(1008, 762);
            this.rJ_Pannel1.TabIndex = 14;
            // 
            // rJ_Lable_狀態
            // 
            this.rJ_Lable_狀態.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_狀態.BackgroundColor = System.Drawing.Color.DarkRed;
            this.rJ_Lable_狀態.BorderColor = System.Drawing.Color.DarkRed;
            this.rJ_Lable_狀態.BorderRadius = 5;
            this.rJ_Lable_狀態.BorderSize = 0;
            this.rJ_Lable_狀態.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable_狀態.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_狀態.Font = new System.Drawing.Font("微軟正黑體", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_狀態.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_狀態.GUID = "";
            this.rJ_Lable_狀態.Location = new System.Drawing.Point(10, 95);
            this.rJ_Lable_狀態.Name = "rJ_Lable_狀態";
            this.rJ_Lable_狀態.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_狀態.ShadowSize = 0;
            this.rJ_Lable_狀態.Size = new System.Drawing.Size(988, 73);
            this.rJ_Lable_狀態.TabIndex = 46;
            this.rJ_Lable_狀態.Text = "------";
            this.rJ_Lable_狀態.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_狀態.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 1;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(620, 264);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 3;
            this.rJ_Lable1.Size = new System.Drawing.Size(209, 72);
            this.rJ_Lable1.TabIndex = 45;
            this.rJ_Lable1.Text = "現有庫存";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.Black;
            this.rJ_Lable1.Visible = false;
            // 
            // rJ_Button_確認輸入
            // 
            this.rJ_Button_確認輸入.AutoResetState = false;
            this.rJ_Button_確認輸入.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_確認輸入.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_確認輸入.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認輸入.BorderRadius = 20;
            this.rJ_Button_確認輸入.BorderSize = 0;
            this.rJ_Button_確認輸入.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認輸入.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認輸入.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認輸入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認輸入.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認輸入.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認輸入.GUID = "";
            this.rJ_Button_確認輸入.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認輸入.Location = new System.Drawing.Point(44, 640);
            this.rJ_Button_確認輸入.Name = "rJ_Button_確認輸入";
            this.rJ_Button_確認輸入.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認輸入.ProhibitionLineWidth = 4;
            this.rJ_Button_確認輸入.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認輸入.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認輸入.ShadowSize = 3;
            this.rJ_Button_確認輸入.ShowLoadingForm = false;
            this.rJ_Button_確認輸入.Size = new System.Drawing.Size(549, 106);
            this.rJ_Button_確認輸入.State = false;
            this.rJ_Button_確認輸入.TabIndex = 43;
            this.rJ_Button_確認輸入.Text = "確認輸入";
            this.rJ_Button_確認輸入.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認輸入.TextHeight = 0;
            this.rJ_Button_確認輸入.UseVisualStyleBackColor = false;
            // 
            // rJ_TextBox_盤點量
            // 
            this.rJ_TextBox_盤點量.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_盤點量.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_盤點量.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_盤點量.BorderRadius = 0;
            this.rJ_TextBox_盤點量.BorderSize = 2;
            this.rJ_TextBox_盤點量.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_盤點量.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_盤點量.GUID = "";
            this.rJ_TextBox_盤點量.Location = new System.Drawing.Point(842, 189);
            this.rJ_TextBox_盤點量.Multiline = false;
            this.rJ_TextBox_盤點量.Name = "rJ_TextBox_盤點量";
            this.rJ_TextBox_盤點量.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_盤點量.PassWordChar = false;
            this.rJ_TextBox_盤點量.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_盤點量.PlaceholderText = "";
            this.rJ_TextBox_盤點量.ShowTouchPannel = false;
            this.rJ_TextBox_盤點量.Size = new System.Drawing.Size(134, 56);
            this.rJ_TextBox_盤點量.TabIndex = 42;
            this.rJ_TextBox_盤點量.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_盤點量.Texts = "";
            this.rJ_TextBox_盤點量.UnderlineStyle = false;
            // 
            // rJ_Pannel2
            // 
            this.rJ_Pannel2.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel2.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Pannel2.BorderColor = System.Drawing.Color.Gray;
            this.rJ_Pannel2.BorderRadius = 50;
            this.rJ_Pannel2.BorderSize = 2;
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_1);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_CE);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_6);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_7);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_2);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_5);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_8);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_0);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_4);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_9);
            this.rJ_Pannel2.Controls.Add(this.rJ_Button_3);
            this.rJ_Pannel2.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel2.IsSelected = false;
            this.rJ_Pannel2.Location = new System.Drawing.Point(627, 351);
            this.rJ_Pannel2.Name = "rJ_Pannel2";
            this.rJ_Pannel2.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.rJ_Pannel2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Pannel2.ShadowSize = 3;
            this.rJ_Pannel2.Size = new System.Drawing.Size(357, 377);
            this.rJ_Pannel2.TabIndex = 41;
            // 
            // rJ_Button_1
            // 
            this.rJ_Button_1.AutoResetState = false;
            this.rJ_Button_1.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_1.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_1.BorderRadius = 10;
            this.rJ_Button_1.BorderSize = 0;
            this.rJ_Button_1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_1.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_1.FlatAppearance.BorderSize = 0;
            this.rJ_Button_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_1.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_1.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_1.GUID = "";
            this.rJ_Button_1.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_1.Location = new System.Drawing.Point(50, 19);
            this.rJ_Button_1.Name = "rJ_Button_1";
            this.rJ_Button_1.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_1.ProhibitionLineWidth = 4;
            this.rJ_Button_1.ProhibitionSymbolSize = 30;
            this.rJ_Button_1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_1.ShadowSize = 0;
            this.rJ_Button_1.ShowLoadingForm = false;
            this.rJ_Button_1.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_1.State = false;
            this.rJ_Button_1.TabIndex = 19;
            this.rJ_Button_1.Text = "1";
            this.rJ_Button_1.TextColor = System.Drawing.Color.White;
            this.rJ_Button_1.TextHeight = 0;
            this.rJ_Button_1.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_CE
            // 
            this.rJ_Button_CE.AutoResetState = false;
            this.rJ_Button_CE.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_CE.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_CE.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_CE.BorderRadius = 10;
            this.rJ_Button_CE.BorderSize = 0;
            this.rJ_Button_CE.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_CE.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_CE.FlatAppearance.BorderSize = 0;
            this.rJ_Button_CE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_CE.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_CE.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_CE.GUID = "";
            this.rJ_Button_CE.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_CE.Location = new System.Drawing.Point(222, 277);
            this.rJ_Button_CE.Name = "rJ_Button_CE";
            this.rJ_Button_CE.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_CE.ProhibitionLineWidth = 4;
            this.rJ_Button_CE.ProhibitionSymbolSize = 30;
            this.rJ_Button_CE.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_CE.ShadowSize = 0;
            this.rJ_Button_CE.ShowLoadingForm = false;
            this.rJ_Button_CE.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_CE.State = false;
            this.rJ_Button_CE.TabIndex = 29;
            this.rJ_Button_CE.Text = "CE";
            this.rJ_Button_CE.TextColor = System.Drawing.Color.White;
            this.rJ_Button_CE.TextHeight = 0;
            this.rJ_Button_CE.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_6
            // 
            this.rJ_Button_6.AutoResetState = false;
            this.rJ_Button_6.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_6.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_6.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_6.BorderRadius = 10;
            this.rJ_Button_6.BorderSize = 0;
            this.rJ_Button_6.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_6.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_6.FlatAppearance.BorderSize = 0;
            this.rJ_Button_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_6.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_6.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_6.GUID = "";
            this.rJ_Button_6.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_6.Location = new System.Drawing.Point(222, 105);
            this.rJ_Button_6.Name = "rJ_Button_6";
            this.rJ_Button_6.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_6.ProhibitionLineWidth = 4;
            this.rJ_Button_6.ProhibitionSymbolSize = 30;
            this.rJ_Button_6.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_6.ShadowSize = 0;
            this.rJ_Button_6.ShowLoadingForm = false;
            this.rJ_Button_6.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_6.State = false;
            this.rJ_Button_6.TabIndex = 24;
            this.rJ_Button_6.Text = "6";
            this.rJ_Button_6.TextColor = System.Drawing.Color.White;
            this.rJ_Button_6.TextHeight = 0;
            this.rJ_Button_6.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_7
            // 
            this.rJ_Button_7.AutoResetState = false;
            this.rJ_Button_7.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_7.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_7.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_7.BorderRadius = 10;
            this.rJ_Button_7.BorderSize = 0;
            this.rJ_Button_7.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_7.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_7.FlatAppearance.BorderSize = 0;
            this.rJ_Button_7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_7.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_7.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_7.GUID = "";
            this.rJ_Button_7.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_7.Location = new System.Drawing.Point(50, 191);
            this.rJ_Button_7.Name = "rJ_Button_7";
            this.rJ_Button_7.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_7.ProhibitionLineWidth = 4;
            this.rJ_Button_7.ProhibitionSymbolSize = 30;
            this.rJ_Button_7.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_7.ShadowSize = 0;
            this.rJ_Button_7.ShowLoadingForm = false;
            this.rJ_Button_7.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_7.State = false;
            this.rJ_Button_7.TabIndex = 25;
            this.rJ_Button_7.Text = "7";
            this.rJ_Button_7.TextColor = System.Drawing.Color.White;
            this.rJ_Button_7.TextHeight = 0;
            this.rJ_Button_7.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_2
            // 
            this.rJ_Button_2.AutoResetState = false;
            this.rJ_Button_2.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_2.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_2.BorderRadius = 10;
            this.rJ_Button_2.BorderSize = 0;
            this.rJ_Button_2.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_2.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_2.FlatAppearance.BorderSize = 0;
            this.rJ_Button_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_2.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_2.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_2.GUID = "";
            this.rJ_Button_2.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_2.Location = new System.Drawing.Point(136, 19);
            this.rJ_Button_2.Name = "rJ_Button_2";
            this.rJ_Button_2.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_2.ProhibitionLineWidth = 4;
            this.rJ_Button_2.ProhibitionSymbolSize = 30;
            this.rJ_Button_2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_2.ShadowSize = 0;
            this.rJ_Button_2.ShowLoadingForm = false;
            this.rJ_Button_2.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_2.State = false;
            this.rJ_Button_2.TabIndex = 20;
            this.rJ_Button_2.Text = "2";
            this.rJ_Button_2.TextColor = System.Drawing.Color.White;
            this.rJ_Button_2.TextHeight = 0;
            this.rJ_Button_2.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_5
            // 
            this.rJ_Button_5.AutoResetState = false;
            this.rJ_Button_5.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_5.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_5.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_5.BorderRadius = 10;
            this.rJ_Button_5.BorderSize = 0;
            this.rJ_Button_5.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_5.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_5.FlatAppearance.BorderSize = 0;
            this.rJ_Button_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_5.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_5.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_5.GUID = "";
            this.rJ_Button_5.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_5.Location = new System.Drawing.Point(136, 105);
            this.rJ_Button_5.Name = "rJ_Button_5";
            this.rJ_Button_5.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_5.ProhibitionLineWidth = 4;
            this.rJ_Button_5.ProhibitionSymbolSize = 30;
            this.rJ_Button_5.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_5.ShadowSize = 0;
            this.rJ_Button_5.ShowLoadingForm = false;
            this.rJ_Button_5.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_5.State = false;
            this.rJ_Button_5.TabIndex = 23;
            this.rJ_Button_5.Text = "5";
            this.rJ_Button_5.TextColor = System.Drawing.Color.White;
            this.rJ_Button_5.TextHeight = 0;
            this.rJ_Button_5.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_8
            // 
            this.rJ_Button_8.AutoResetState = false;
            this.rJ_Button_8.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_8.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_8.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_8.BorderRadius = 10;
            this.rJ_Button_8.BorderSize = 0;
            this.rJ_Button_8.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_8.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_8.FlatAppearance.BorderSize = 0;
            this.rJ_Button_8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_8.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_8.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_8.GUID = "";
            this.rJ_Button_8.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_8.Location = new System.Drawing.Point(136, 191);
            this.rJ_Button_8.Name = "rJ_Button_8";
            this.rJ_Button_8.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_8.ProhibitionLineWidth = 4;
            this.rJ_Button_8.ProhibitionSymbolSize = 30;
            this.rJ_Button_8.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_8.ShadowSize = 0;
            this.rJ_Button_8.ShowLoadingForm = false;
            this.rJ_Button_8.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_8.State = false;
            this.rJ_Button_8.TabIndex = 26;
            this.rJ_Button_8.Text = "8";
            this.rJ_Button_8.TextColor = System.Drawing.Color.White;
            this.rJ_Button_8.TextHeight = 0;
            this.rJ_Button_8.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_0
            // 
            this.rJ_Button_0.AutoResetState = false;
            this.rJ_Button_0.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_0.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_0.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_0.BorderRadius = 10;
            this.rJ_Button_0.BorderSize = 0;
            this.rJ_Button_0.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_0.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_0.FlatAppearance.BorderSize = 0;
            this.rJ_Button_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_0.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_0.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_0.GUID = "";
            this.rJ_Button_0.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_0.Location = new System.Drawing.Point(136, 277);
            this.rJ_Button_0.Name = "rJ_Button_0";
            this.rJ_Button_0.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_0.ProhibitionLineWidth = 4;
            this.rJ_Button_0.ProhibitionSymbolSize = 30;
            this.rJ_Button_0.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_0.ShadowSize = 0;
            this.rJ_Button_0.ShowLoadingForm = false;
            this.rJ_Button_0.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_0.State = false;
            this.rJ_Button_0.TabIndex = 28;
            this.rJ_Button_0.Text = "0";
            this.rJ_Button_0.TextColor = System.Drawing.Color.White;
            this.rJ_Button_0.TextHeight = 0;
            this.rJ_Button_0.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_4
            // 
            this.rJ_Button_4.AutoResetState = false;
            this.rJ_Button_4.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_4.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_4.BorderRadius = 10;
            this.rJ_Button_4.BorderSize = 0;
            this.rJ_Button_4.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_4.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_4.FlatAppearance.BorderSize = 0;
            this.rJ_Button_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_4.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_4.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_4.GUID = "";
            this.rJ_Button_4.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_4.Location = new System.Drawing.Point(50, 105);
            this.rJ_Button_4.Name = "rJ_Button_4";
            this.rJ_Button_4.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_4.ProhibitionLineWidth = 4;
            this.rJ_Button_4.ProhibitionSymbolSize = 30;
            this.rJ_Button_4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_4.ShadowSize = 0;
            this.rJ_Button_4.ShowLoadingForm = false;
            this.rJ_Button_4.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_4.State = false;
            this.rJ_Button_4.TabIndex = 22;
            this.rJ_Button_4.Text = "4";
            this.rJ_Button_4.TextColor = System.Drawing.Color.White;
            this.rJ_Button_4.TextHeight = 0;
            this.rJ_Button_4.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_9
            // 
            this.rJ_Button_9.AutoResetState = false;
            this.rJ_Button_9.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_9.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_9.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_9.BorderRadius = 10;
            this.rJ_Button_9.BorderSize = 0;
            this.rJ_Button_9.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_9.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_9.FlatAppearance.BorderSize = 0;
            this.rJ_Button_9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_9.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_9.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_9.GUID = "";
            this.rJ_Button_9.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_9.Location = new System.Drawing.Point(222, 191);
            this.rJ_Button_9.Name = "rJ_Button_9";
            this.rJ_Button_9.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_9.ProhibitionLineWidth = 4;
            this.rJ_Button_9.ProhibitionSymbolSize = 30;
            this.rJ_Button_9.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_9.ShadowSize = 0;
            this.rJ_Button_9.ShowLoadingForm = false;
            this.rJ_Button_9.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_9.State = false;
            this.rJ_Button_9.TabIndex = 27;
            this.rJ_Button_9.Text = "9";
            this.rJ_Button_9.TextColor = System.Drawing.Color.White;
            this.rJ_Button_9.TextHeight = 0;
            this.rJ_Button_9.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_3
            // 
            this.rJ_Button_3.AutoResetState = false;
            this.rJ_Button_3.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_3.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_3.BorderRadius = 10;
            this.rJ_Button_3.BorderSize = 0;
            this.rJ_Button_3.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_3.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_3.FlatAppearance.BorderSize = 0;
            this.rJ_Button_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_3.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_3.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_3.GUID = "";
            this.rJ_Button_3.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_3.Location = new System.Drawing.Point(222, 19);
            this.rJ_Button_3.Name = "rJ_Button_3";
            this.rJ_Button_3.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_3.ProhibitionLineWidth = 4;
            this.rJ_Button_3.ProhibitionSymbolSize = 30;
            this.rJ_Button_3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_3.ShadowSize = 0;
            this.rJ_Button_3.ShowLoadingForm = false;
            this.rJ_Button_3.Size = new System.Drawing.Size(80, 80);
            this.rJ_Button_3.State = false;
            this.rJ_Button_3.TabIndex = 21;
            this.rJ_Button_3.Text = "3";
            this.rJ_Button_3.TextColor = System.Drawing.Color.White;
            this.rJ_Button_3.TextHeight = 0;
            this.rJ_Button_3.UseVisualStyleBackColor = false;
            // 
            // rJ_Lable3
            // 
            this.rJ_Lable3.BackColor = System.Drawing.Color.White;
            this.rJ_Lable3.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.rJ_Lable3.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable3.BorderRadius = 10;
            this.rJ_Lable3.BorderSize = 1;
            this.rJ_Lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable3.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable3.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable3.GUID = "";
            this.rJ_Lable3.Location = new System.Drawing.Point(620, 181);
            this.rJ_Lable3.Name = "rJ_Lable3";
            this.rJ_Lable3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable3.ShadowSize = 3;
            this.rJ_Lable3.Size = new System.Drawing.Size(209, 72);
            this.rJ_Lable3.TabIndex = 40;
            this.rJ_Lable3.Text = "盤點量";
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
            this.rJ_Lable_現有庫存.Location = new System.Drawing.Point(842, 272);
            this.rJ_Lable_現有庫存.Name = "rJ_Lable_現有庫存";
            this.rJ_Lable_現有庫存.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_現有庫存.ShadowSize = 0;
            this.rJ_Lable_現有庫存.Size = new System.Drawing.Size(134, 56);
            this.rJ_Lable_現有庫存.TabIndex = 39;
            this.rJ_Lable_現有庫存.Text = "-----";
            this.rJ_Lable_現有庫存.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_現有庫存.TextColor = System.Drawing.Color.Black;
            this.rJ_Lable_現有庫存.Visible = false;
            // 
            // pictureBox_藥品資訊
            // 
            this.pictureBox_藥品資訊.BackColor = System.Drawing.Color.Snow;
            this.pictureBox_藥品資訊.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_藥品資訊.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_藥品資訊.Location = new System.Drawing.Point(41, 181);
            this.pictureBox_藥品資訊.Name = "pictureBox_藥品資訊";
            this.pictureBox_藥品資訊.Size = new System.Drawing.Size(552, 453);
            this.pictureBox_藥品資訊.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_藥品資訊.TabIndex = 38;
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
            this.rJ_Lable_藥品資訊.Location = new System.Drawing.Point(10, 10);
            this.rJ_Lable_藥品資訊.Name = "rJ_Lable_藥品資訊";
            this.rJ_Lable_藥品資訊.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_藥品資訊.ShadowSize = 0;
            this.rJ_Lable_藥品資訊.Size = new System.Drawing.Size(988, 85);
            this.rJ_Lable_藥品資訊.TabIndex = 37;
            this.rJ_Lable_藥品資訊.Text = "(------) -------------------------------------------";
            this.rJ_Lable_藥品資訊.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_藥品資訊.TextColor = System.Drawing.Color.Black;
            // 
            // Dialog_交班對點
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1800, 920);
            this.ControlBox = true;
            this.Controls.Add(this.panel_交班內容);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Dialog_交班對點";
            this.Text = "交班對點";
            this.panel9.ResumeLayout(false);
            this.panel_藥品選擇.ResumeLayout(false);
            this.panel_藥品選擇.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel_交班內容.ResumeLayout(false);
            this.rJ_Pannel1.ResumeLayout(false);
            this.rJ_Pannel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_藥品資訊)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_覆盤登入;
        private MyUI.PLC_RJ_Button plC_RJ_Button_盤點登入;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel_藥品選擇;
        private MyUI.RJ_Button rJ_Button_藥品群組_選擇;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_藥品群組;
        private System.Windows.Forms.Panel panel8;
        private SQLUI.SQL_DataGridView sqL_DataGridView_交班藥品;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_Lable rJ_Lable_覆盤人員;
        private System.Windows.Forms.Panel panel3;
        private MyUI.RJ_Lable rJ_Lable_盤點人員;
        private System.Windows.Forms.Panel panel_交班內容;
        private MyUI.RJ_Pannel rJ_Pannel1;
        private MyUI.RJ_Lable rJ_Lable_狀態;
        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.RJ_Button rJ_Button_確認輸入;
        private MyUI.RJ_TextBox rJ_TextBox_盤點量;
        private MyUI.RJ_Pannel rJ_Pannel2;
        private MyUI.RJ_Button rJ_Button_1;
        private MyUI.RJ_Button rJ_Button_CE;
        private MyUI.RJ_Button rJ_Button_6;
        private MyUI.RJ_Button rJ_Button_7;
        private MyUI.RJ_Button rJ_Button_2;
        private MyUI.RJ_Button rJ_Button_5;
        private MyUI.RJ_Button rJ_Button_8;
        private MyUI.RJ_Button rJ_Button_0;
        private MyUI.RJ_Button rJ_Button_4;
        private MyUI.RJ_Button rJ_Button_9;
        private MyUI.RJ_Button rJ_Button_3;
        private MyUI.RJ_Lable rJ_Lable3;
        private MyUI.RJ_Lable rJ_Lable_現有庫存;
        private System.Windows.Forms.PictureBox pictureBox_藥品資訊;
        private MyUI.RJ_Lable rJ_Lable_藥品資訊;
        private MyUI.PLC_RJ_Button plC_RJ_Button_確認送出;
        private MyUI.StepViewer stepViewer;
    }
}