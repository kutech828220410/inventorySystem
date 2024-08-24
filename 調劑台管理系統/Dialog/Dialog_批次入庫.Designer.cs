
namespace 調劑台管理系統
{
    partial class Dialog_批次入庫
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
            this.plC_RJ_Button_範例下載 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_匯入 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_確認送出 = new MyUI.PLC_RJ_Button();
            this.rJ_Button_搜尋 = new MyUI.PLC_RJ_Button();
            this.comboBox_搜尋內容 = new System.Windows.Forms.ComboBox();
            this.comboBox_搜尋條件 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rJ_RatioButton_入庫時間 = new MyUI.RJ_RatioButton();
            this.rJ_RatioButton_建表時間 = new MyUI.RJ_RatioButton();
            this.dateTimeIntervelPicker_建表日期 = new MyUI.DateTimeIntervelPicker();
            this.sqL_DataGridView_批次入庫 = new SQLUI.SQL_DataGridView();
            this.openFileDialog_LoadExcel = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_SaveExcel = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plC_RJ_Button_範例下載);
            this.panel1.Controls.Add(this.plC_RJ_Button_匯入);
            this.panel1.Controls.Add(this.plC_RJ_Button_確認送出);
            this.panel1.Controls.Add(this.rJ_Button_搜尋);
            this.panel1.Controls.Add(this.comboBox_搜尋內容);
            this.panel1.Controls.Add(this.comboBox_搜尋條件);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1642, 159);
            this.panel1.TabIndex = 0;
            // 
            // plC_RJ_Button_範例下載
            // 
            this.plC_RJ_Button_範例下載.AutoResetState = true;
            this.plC_RJ_Button_範例下載.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_範例下載.Bool = false;
            this.plC_RJ_Button_範例下載.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_範例下載.BorderRadius = 15;
            this.plC_RJ_Button_範例下載.BorderSize = 1;
            this.plC_RJ_Button_範例下載.but_press = false;
            this.plC_RJ_Button_範例下載.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_範例下載.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_範例下載.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_範例下載.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_範例下載.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_範例下載.GUID = "";
            this.plC_RJ_Button_範例下載.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_範例下載.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_範例下載.Location = new System.Drawing.Point(1321, 47);
            this.plC_RJ_Button_範例下載.Name = "plC_RJ_Button_範例下載";
            this.plC_RJ_Button_範例下載.OFF_文字內容 = "範例下載";
            this.plC_RJ_Button_範例下載.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_範例下載.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_範例下載.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_範例下載.ON_BorderSize = 1;
            this.plC_RJ_Button_範例下載.ON_文字內容 = "範例下載";
            this.plC_RJ_Button_範例下載.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_範例下載.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_範例下載.ON_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_範例下載.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_範例下載.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_範例下載.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_範例下載.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_範例下載.ShadowSize = 3;
            this.plC_RJ_Button_範例下載.ShowLoadingForm = false;
            this.plC_RJ_Button_範例下載.Size = new System.Drawing.Size(102, 102);
            this.plC_RJ_Button_範例下載.State = false;
            this.plC_RJ_Button_範例下載.TabIndex = 158;
            this.plC_RJ_Button_範例下載.Text = "範例下載";
            this.plC_RJ_Button_範例下載.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_範例下載.TextHeight = 35;
            this.plC_RJ_Button_範例下載.Texts = "範例下載";
            this.plC_RJ_Button_範例下載.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_範例下載.字型鎖住 = false;
            this.plC_RJ_Button_範例下載.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_範例下載.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_範例下載.文字鎖住 = false;
            this.plC_RJ_Button_範例下載.背景圖片 = global::調劑台管理系統.Properties.Resources.exsample_excell_file_download_removebg_preview;
            this.plC_RJ_Button_範例下載.讀取位元反向 = false;
            this.plC_RJ_Button_範例下載.讀寫鎖住 = false;
            this.plC_RJ_Button_範例下載.音效 = false;
            this.plC_RJ_Button_範例下載.顯示 = false;
            this.plC_RJ_Button_範例下載.顯示狀態 = false;
            // 
            // plC_RJ_Button_匯入
            // 
            this.plC_RJ_Button_匯入.AutoResetState = true;
            this.plC_RJ_Button_匯入.BackgroundColor = System.Drawing.Color.White;
            this.plC_RJ_Button_匯入.Bool = false;
            this.plC_RJ_Button_匯入.BorderColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯入.BorderRadius = 15;
            this.plC_RJ_Button_匯入.BorderSize = 1;
            this.plC_RJ_Button_匯入.but_press = false;
            this.plC_RJ_Button_匯入.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_匯入.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_匯入.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_匯入.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_匯入.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_匯入.GUID = "";
            this.plC_RJ_Button_匯入.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_匯入.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_匯入.Location = new System.Drawing.Point(1429, 47);
            this.plC_RJ_Button_匯入.Name = "plC_RJ_Button_匯入";
            this.plC_RJ_Button_匯入.OFF_文字內容 = "匯入";
            this.plC_RJ_Button_匯入.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_匯入.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯入.OFF_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_匯入.ON_BorderSize = 1;
            this.plC_RJ_Button_匯入.ON_文字內容 = "匯入";
            this.plC_RJ_Button_匯入.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_匯入.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯入.ON_背景顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_匯入.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_Button_匯入.ProhibitionLineWidth = 4;
            this.plC_RJ_Button_匯入.ProhibitionSymbolSize = 30;
            this.plC_RJ_Button_匯入.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_匯入.ShadowSize = 3;
            this.plC_RJ_Button_匯入.ShowLoadingForm = false;
            this.plC_RJ_Button_匯入.Size = new System.Drawing.Size(102, 102);
            this.plC_RJ_Button_匯入.State = false;
            this.plC_RJ_Button_匯入.TabIndex = 157;
            this.plC_RJ_Button_匯入.Text = "匯入";
            this.plC_RJ_Button_匯入.TextColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_匯入.TextHeight = 35;
            this.plC_RJ_Button_匯入.Texts = "匯入";
            this.plC_RJ_Button_匯入.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_匯入.字型鎖住 = false;
            this.plC_RJ_Button_匯入.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_匯入.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_匯入.文字鎖住 = false;
            this.plC_RJ_Button_匯入.背景圖片 = global::調劑台管理系統.Properties.Resources.excell_file_upload_to_server_removebg_preview;
            this.plC_RJ_Button_匯入.讀取位元反向 = false;
            this.plC_RJ_Button_匯入.讀寫鎖住 = false;
            this.plC_RJ_Button_匯入.音效 = false;
            this.plC_RJ_Button_匯入.顯示 = false;
            this.plC_RJ_Button_匯入.顯示狀態 = false;
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
            this.plC_RJ_Button_確認送出.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_確認送出.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_確認送出.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認送出.GUID = "";
            this.plC_RJ_Button_確認送出.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_確認送出.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.plC_RJ_Button_確認送出.Location = new System.Drawing.Point(1537, 47);
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
            this.plC_RJ_Button_確認送出.Size = new System.Drawing.Size(102, 102);
            this.plC_RJ_Button_確認送出.State = false;
            this.plC_RJ_Button_確認送出.TabIndex = 156;
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
            // rJ_Button_搜尋
            // 
            this.rJ_Button_搜尋.AutoResetState = true;
            this.rJ_Button_搜尋.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_搜尋.Bool = false;
            this.rJ_Button_搜尋.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_搜尋.BorderRadius = 15;
            this.rJ_Button_搜尋.BorderSize = 1;
            this.rJ_Button_搜尋.but_press = false;
            this.rJ_Button_搜尋.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.rJ_Button_搜尋.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_搜尋.FlatAppearance.BorderSize = 0;
            this.rJ_Button_搜尋.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_搜尋.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_搜尋.GUID = "";
            this.rJ_Button_搜尋.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.rJ_Button_搜尋.Image_padding = new System.Windows.Forms.Padding(18, 7, 23, 5);
            this.rJ_Button_搜尋.Location = new System.Drawing.Point(882, 47);
            this.rJ_Button_搜尋.Name = "rJ_Button_搜尋";
            this.rJ_Button_搜尋.OFF_文字內容 = "搜尋";
            this.rJ_Button_搜尋.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_搜尋.OFF_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_搜尋.OFF_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_搜尋.ON_BorderSize = 1;
            this.rJ_Button_搜尋.ON_文字內容 = "搜尋";
            this.rJ_Button_搜尋.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_搜尋.ON_文字顏色 = System.Drawing.Color.Black;
            this.rJ_Button_搜尋.ON_背景顏色 = System.Drawing.Color.White;
            this.rJ_Button_搜尋.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_搜尋.ProhibitionLineWidth = 4;
            this.rJ_Button_搜尋.ProhibitionSymbolSize = 30;
            this.rJ_Button_搜尋.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_搜尋.ShadowSize = 3;
            this.rJ_Button_搜尋.ShowLoadingForm = false;
            this.rJ_Button_搜尋.Size = new System.Drawing.Size(102, 102);
            this.rJ_Button_搜尋.State = false;
            this.rJ_Button_搜尋.TabIndex = 155;
            this.rJ_Button_搜尋.Text = "搜尋";
            this.rJ_Button_搜尋.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_搜尋.TextHeight = 35;
            this.rJ_Button_搜尋.Texts = "搜尋";
            this.rJ_Button_搜尋.UseVisualStyleBackColor = false;
            this.rJ_Button_搜尋.字型鎖住 = false;
            this.rJ_Button_搜尋.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.rJ_Button_搜尋.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.rJ_Button_搜尋.文字鎖住 = false;
            this.rJ_Button_搜尋.背景圖片 = global::調劑台管理系統.Properties.Resources.icon_for_searching_medicine_removebg_preview;
            this.rJ_Button_搜尋.讀取位元反向 = false;
            this.rJ_Button_搜尋.讀寫鎖住 = false;
            this.rJ_Button_搜尋.音效 = false;
            this.rJ_Button_搜尋.顯示 = false;
            this.rJ_Button_搜尋.顯示狀態 = false;
            // 
            // comboBox_搜尋內容
            // 
            this.comboBox_搜尋內容.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_搜尋內容.FormattingEnabled = true;
            this.comboBox_搜尋內容.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox_搜尋內容.Location = new System.Drawing.Point(565, 79);
            this.comboBox_搜尋內容.Name = "comboBox_搜尋內容";
            this.comboBox_搜尋內容.Size = new System.Drawing.Size(296, 44);
            this.comboBox_搜尋內容.TabIndex = 154;
            // 
            // comboBox_搜尋條件
            // 
            this.comboBox_搜尋條件.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_搜尋條件.FormattingEnabled = true;
            this.comboBox_搜尋條件.Items.AddRange(new object[] {
            "全部顯示",
            "藥碼",
            "藥名",
            "中文名"});
            this.comboBox_搜尋條件.Location = new System.Drawing.Point(363, 79);
            this.comboBox_搜尋條件.Name = "comboBox_搜尋條件";
            this.comboBox_搜尋條件.Size = new System.Drawing.Size(179, 44);
            this.comboBox_搜尋條件.TabIndex = 153;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rJ_RatioButton_入庫時間);
            this.panel2.Controls.Add(this.rJ_RatioButton_建表時間);
            this.panel2.Controls.Add(this.dateTimeIntervelPicker_建表日期);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 159);
            this.panel2.TabIndex = 15;
            // 
            // rJ_RatioButton_入庫時間
            // 
            this.rJ_RatioButton_入庫時間.AutoSize = true;
            this.rJ_RatioButton_入庫時間.CheckColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_RatioButton_入庫時間.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_RatioButton_入庫時間.Location = new System.Drawing.Point(153, 15);
            this.rJ_RatioButton_入庫時間.MinimumSize = new System.Drawing.Size(0, 21);
            this.rJ_RatioButton_入庫時間.Name = "rJ_RatioButton_入庫時間";
            this.rJ_RatioButton_入庫時間.Size = new System.Drawing.Size(126, 30);
            this.rJ_RatioButton_入庫時間.TabIndex = 17;
            this.rJ_RatioButton_入庫時間.Text = "入庫時間";
            this.rJ_RatioButton_入庫時間.UncheckColor = System.Drawing.Color.Gray;
            this.rJ_RatioButton_入庫時間.UseVisualStyleBackColor = true;
            // 
            // rJ_RatioButton_建表時間
            // 
            this.rJ_RatioButton_建表時間.AutoSize = true;
            this.rJ_RatioButton_建表時間.CheckColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_RatioButton_建表時間.Checked = true;
            this.rJ_RatioButton_建表時間.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_RatioButton_建表時間.Location = new System.Drawing.Point(21, 15);
            this.rJ_RatioButton_建表時間.MinimumSize = new System.Drawing.Size(0, 21);
            this.rJ_RatioButton_建表時間.Name = "rJ_RatioButton_建表時間";
            this.rJ_RatioButton_建表時間.Size = new System.Drawing.Size(126, 30);
            this.rJ_RatioButton_建表時間.TabIndex = 16;
            this.rJ_RatioButton_建表時間.TabStop = true;
            this.rJ_RatioButton_建表時間.Text = "建表時間";
            this.rJ_RatioButton_建表時間.UncheckColor = System.Drawing.Color.Gray;
            this.rJ_RatioButton_建表時間.UseVisualStyleBackColor = true;
            // 
            // dateTimeIntervelPicker_建表日期
            // 
            this.dateTimeIntervelPicker_建表日期.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeIntervelPicker_建表日期.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dateTimeIntervelPicker_建表日期.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeIntervelPicker_建表日期.DateFont = new System.Drawing.Font("微軟正黑體", 14F);
            this.dateTimeIntervelPicker_建表日期.DateSize = new System.Drawing.Size(217, 39);
            this.dateTimeIntervelPicker_建表日期.EndTime = new System.DateTime(2024, 3, 28, 23, 59, 59, 0);
            this.dateTimeIntervelPicker_建表日期.Location = new System.Drawing.Point(9, 60);
            this.dateTimeIntervelPicker_建表日期.Name = "dateTimeIntervelPicker_建表日期";
            this.dateTimeIntervelPicker_建表日期.Padding = new System.Windows.Forms.Padding(2);
            this.dateTimeIntervelPicker_建表日期.Size = new System.Drawing.Size(316, 83);
            this.dateTimeIntervelPicker_建表日期.StartTime = new System.DateTime(2024, 3, 28, 0, 0, 0, 0);
            this.dateTimeIntervelPicker_建表日期.TabIndex = 14;
            this.dateTimeIntervelPicker_建表日期.TitleFont = new System.Drawing.Font("新細明體", 9F);
            this.dateTimeIntervelPicker_建表日期.TiTleSize = new System.Drawing.Size(33, 39);
            // 
            // sqL_DataGridView_批次入庫
            // 
            this.sqL_DataGridView_批次入庫.AutoSelectToDeep = false;
            this.sqL_DataGridView_批次入庫.backColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_批次入庫.BorderColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_批次入庫.BorderRadius = 0;
            this.sqL_DataGridView_批次入庫.BorderSize = 2;
            this.sqL_DataGridView_批次入庫.CellBorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_批次入庫.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_批次入庫.cellStylBackColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_批次入庫.cellStyleFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_批次入庫.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_批次入庫.checkedRowBackColor = System.Drawing.Color.YellowGreen;
            this.sqL_DataGridView_批次入庫.columnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_批次入庫.columnHeaderBorderColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_批次入庫.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_批次入庫.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_批次入庫.columnHeadersHeight = 18;
            this.sqL_DataGridView_批次入庫.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_批次入庫.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.sqL_DataGridView_批次入庫.DataKeyEnable = false;
            this.sqL_DataGridView_批次入庫.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_批次入庫.Font = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_批次入庫.ImageBox = false;
            this.sqL_DataGridView_批次入庫.Location = new System.Drawing.Point(4, 223);
            this.sqL_DataGridView_批次入庫.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sqL_DataGridView_批次入庫.Name = "sqL_DataGridView_批次入庫";
            this.sqL_DataGridView_批次入庫.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_批次入庫.Password = "user82822040";
            this.sqL_DataGridView_批次入庫.Port = ((uint)(3306u));
            this.sqL_DataGridView_批次入庫.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_批次入庫.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_批次入庫.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_批次入庫.RowsHeight = 50;
            this.sqL_DataGridView_批次入庫.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_批次入庫.selectedBorderSize = 0;
            this.sqL_DataGridView_批次入庫.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_批次入庫.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_批次入庫.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_批次入庫.Server = "localhost";
            this.sqL_DataGridView_批次入庫.Size = new System.Drawing.Size(1642, 671);
            this.sqL_DataGridView_批次入庫.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_批次入庫.TabIndex = 127;
            this.sqL_DataGridView_批次入庫.UserName = "root";
            this.sqL_DataGridView_批次入庫.可拖曳欄位寬度 = true;
            this.sqL_DataGridView_批次入庫.可選擇多列 = true;
            this.sqL_DataGridView_批次入庫.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_批次入庫.自動換行 = true;
            this.sqL_DataGridView_批次入庫.表單字體 = new System.Drawing.Font("新細明體", 12F);
            this.sqL_DataGridView_批次入庫.邊框樣式 = System.Windows.Forms.BorderStyle.None;
            this.sqL_DataGridView_批次入庫.顯示CheckBox = true;
            this.sqL_DataGridView_批次入庫.顯示首列 = true;
            this.sqL_DataGridView_批次入庫.顯示首行 = true;
            this.sqL_DataGridView_批次入庫.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_批次入庫.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // openFileDialog_LoadExcel
            // 
            this.openFileDialog_LoadExcel.DefaultExt = "txt";
            this.openFileDialog_LoadExcel.Filter = "Excel File (*.xlsx)|*.xlsx|txt File (*.txt)|*.txt;";
            // 
            // saveFileDialog_SaveExcel
            // 
            this.saveFileDialog_SaveExcel.DefaultExt = "txt";
            this.saveFileDialog_SaveExcel.Filter = "Excel File (*.xlsx)|*.xlsx|txt File (*.txt)|*.txt;";
            // 
            // Dialog_批次入庫
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 60;
            this.ClientSize = new System.Drawing.Size(1650, 898);
            this.CloseBoxSize = new System.Drawing.Size(50, 50);
            this.ControlBox = true;
            this.Controls.Add(this.sqL_DataGridView_批次入庫);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(50, 50);
            this.MiniSize = new System.Drawing.Size(50, 50);
            this.Name = "Dialog_批次入庫";
            this.Text = "批次入庫";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyUI.DateTimeIntervelPicker dateTimeIntervelPicker_建表日期;
        private SQLUI.SQL_DataGridView sqL_DataGridView_批次入庫;
        private System.Windows.Forms.Panel panel2;
        private MyUI.RJ_RatioButton rJ_RatioButton_入庫時間;
        private MyUI.RJ_RatioButton rJ_RatioButton_建表時間;
        private System.Windows.Forms.ComboBox comboBox_搜尋內容;
        private System.Windows.Forms.ComboBox comboBox_搜尋條件;
        private MyUI.PLC_RJ_Button rJ_Button_搜尋;
        private MyUI.PLC_RJ_Button plC_RJ_Button_確認送出;
        private MyUI.PLC_RJ_Button plC_RJ_Button_匯入;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadExcel;
        private MyUI.PLC_RJ_Button plC_RJ_Button_範例下載;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_SaveExcel;
    }
}