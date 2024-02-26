
namespace 癌症自動備藥機暨排程系統
{
    partial class Dialog_備藥通知處方選擇
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
            this.button_重新整理 = new System.Windows.Forms.Button();
            this.dateTimeIntervelPicker_備藥通知時間範圍 = new MyUI.DateTimeIntervelPicker();
            this.plC_RJ_Button_返回 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_確認 = new MyUI.PLC_RJ_Button();
            this.uc_備藥通知處方 = new 癌症自動備藥機暨排程系統.uc_備藥通知處方();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_重新整理);
            this.panel1.Controls.Add(this.dateTimeIntervelPicker_備藥通知時間範圍);
            this.panel1.Controls.Add(this.plC_RJ_Button_返回);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 99);
            this.panel1.TabIndex = 2;
            // 
            // button_重新整理
            // 
            this.button_重新整理.BackgroundImage = global::癌症自動備藥機暨排程系統.Properties.Resources._568370;
            this.button_重新整理.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_重新整理.Location = new System.Drawing.Point(346, 14);
            this.button_重新整理.Name = "button_重新整理";
            this.button_重新整理.Size = new System.Drawing.Size(75, 72);
            this.button_重新整理.TabIndex = 12;
            this.button_重新整理.UseVisualStyleBackColor = true;
            // 
            // dateTimeIntervelPicker_備藥通知時間範圍
            // 
            this.dateTimeIntervelPicker_備藥通知時間範圍.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeIntervelPicker_備藥通知時間範圍.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeIntervelPicker_備藥通知時間範圍.Location = new System.Drawing.Point(12, 7);
            this.dateTimeIntervelPicker_備藥通知時間範圍.Name = "dateTimeIntervelPicker_備藥通知時間範圍";
            this.dateTimeIntervelPicker_備藥通知時間範圍.Size = new System.Drawing.Size(328, 79);
            this.dateTimeIntervelPicker_備藥通知時間範圍.TabIndex = 11;
            // 
            // plC_RJ_Button_返回
            // 
            this.plC_RJ_Button_返回.AutoResetState = false;
            this.plC_RJ_Button_返回.BackgroundColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.Bool = false;
            this.plC_RJ_Button_返回.BorderColor = System.Drawing.Color.Thistle;
            this.plC_RJ_Button_返回.BorderRadius = 20;
            this.plC_RJ_Button_返回.BorderSize = 0;
            this.plC_RJ_Button_返回.but_press = false;
            this.plC_RJ_Button_返回.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_返回.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_返回.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_返回.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_返回.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_返回.GUID = "";
            this.plC_RJ_Button_返回.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_返回.Location = new System.Drawing.Point(875, 0);
            this.plC_RJ_Button_返回.Name = "plC_RJ_Button_返回";
            this.plC_RJ_Button_返回.OFF_文字內容 = "返回";
            this.plC_RJ_Button_返回.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_返回.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.OFF_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.ON_BorderSize = 5;
            this.plC_RJ_Button_返回.ON_文字內容 = "返回";
            this.plC_RJ_Button_返回.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_返回.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_返回.ON_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_返回.ShadowSize = 3;
            this.plC_RJ_Button_返回.ShowLoadingForm = false;
            this.plC_RJ_Button_返回.Size = new System.Drawing.Size(161, 99);
            this.plC_RJ_Button_返回.State = false;
            this.plC_RJ_Button_返回.TabIndex = 10;
            this.plC_RJ_Button_返回.Text = "返回";
            this.plC_RJ_Button_返回.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.Texts = "返回";
            this.plC_RJ_Button_返回.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_返回.字型鎖住 = false;
            this.plC_RJ_Button_返回.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_返回.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_返回.文字鎖住 = false;
            this.plC_RJ_Button_返回.讀取位元反向 = false;
            this.plC_RJ_Button_返回.讀寫鎖住 = false;
            this.plC_RJ_Button_返回.音效 = false;
            this.plC_RJ_Button_返回.顯示 = false;
            this.plC_RJ_Button_返回.顯示狀態 = false;
            // 
            // plC_RJ_Button_確認
            // 
            this.plC_RJ_Button_確認.AutoResetState = false;
            this.plC_RJ_Button_確認.BackgroundColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_確認.Bool = false;
            this.plC_RJ_Button_確認.BorderColor = System.Drawing.Color.Thistle;
            this.plC_RJ_Button_確認.BorderRadius = 20;
            this.plC_RJ_Button_確認.BorderSize = 0;
            this.plC_RJ_Button_確認.but_press = false;
            this.plC_RJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plC_RJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_確認.GUID = "";
            this.plC_RJ_Button_確認.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_確認.Location = new System.Drawing.Point(4, 1255);
            this.plC_RJ_Button_確認.Name = "plC_RJ_Button_確認";
            this.plC_RJ_Button_確認.OFF_文字內容 = "確    認";
            this.plC_RJ_Button_確認.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_確認.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認.OFF_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_確認.ON_BorderSize = 5;
            this.plC_RJ_Button_確認.ON_文字內容 = "確    認";
            this.plC_RJ_Button_確認.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認.ON_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_確認.ShadowSize = 3;
            this.plC_RJ_Button_確認.ShowLoadingForm = false;
            this.plC_RJ_Button_確認.Size = new System.Drawing.Size(1036, 110);
            this.plC_RJ_Button_確認.State = false;
            this.plC_RJ_Button_確認.TabIndex = 13;
            this.plC_RJ_Button_確認.Text = "確    認";
            this.plC_RJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_確認.Texts = "確    認";
            this.plC_RJ_Button_確認.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_確認.字型鎖住 = false;
            this.plC_RJ_Button_確認.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_確認.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_確認.文字鎖住 = false;
            this.plC_RJ_Button_確認.讀取位元反向 = false;
            this.plC_RJ_Button_確認.讀寫鎖住 = false;
            this.plC_RJ_Button_確認.音效 = false;
            this.plC_RJ_Button_確認.顯示 = false;
            this.plC_RJ_Button_確認.顯示狀態 = false;
            // 
            // uc_備藥通知處方
            // 
            this.uc_備藥通知處方.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_備藥通知處方.Location = new System.Drawing.Point(4, 127);
            this.uc_備藥通知處方.Name = "uc_備藥通知處方";
            this.uc_備藥通知處方.Size = new System.Drawing.Size(1036, 1128);
            this.uc_備藥通知處方.TabIndex = 14;
            // 
            // Dialog_備藥通知處方選擇
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1044, 1369);
            this.Controls.Add(this.uc_備藥通知處方);
            this.Controls.Add(this.plC_RJ_Button_確認);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Dialog_備藥通知處方選擇";
            this.Special_Time = 100;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_返回;
        private MyUI.PLC_RJ_Button plC_RJ_Button_確認;
        private uc_備藥通知處方 uc_備藥通知處方;
        private MyUI.DateTimeIntervelPicker dateTimeIntervelPicker_備藥通知時間範圍;
        private System.Windows.Forms.Button button_重新整理;
    }
}