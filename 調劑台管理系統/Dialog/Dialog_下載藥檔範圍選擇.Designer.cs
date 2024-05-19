
namespace 調劑台管理系統
{
    partial class Dialog_中西藥選擇
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
            this.checkBox_中藥 = new System.Windows.Forms.CheckBox();
            this.checkBox_西藥 = new System.Windows.Forms.CheckBox();
            this.rJ_Button_確認選擇 = new MyUI.RJ_Button();
            this.SuspendLayout();
            // 
            // checkBox_中藥
            // 
            this.checkBox_中藥.AutoSize = true;
            this.checkBox_中藥.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_中藥.Location = new System.Drawing.Point(71, 58);
            this.checkBox_中藥.Name = "checkBox_中藥";
            this.checkBox_中藥.Size = new System.Drawing.Size(81, 35);
            this.checkBox_中藥.TabIndex = 0;
            this.checkBox_中藥.Text = "中藥";
            this.checkBox_中藥.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_中藥.UseVisualStyleBackColor = true;
            // 
            // checkBox_西藥
            // 
            this.checkBox_西藥.AutoSize = true;
            this.checkBox_西藥.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_西藥.Location = new System.Drawing.Point(166, 58);
            this.checkBox_西藥.Name = "checkBox_西藥";
            this.checkBox_西藥.Size = new System.Drawing.Size(81, 35);
            this.checkBox_西藥.TabIndex = 1;
            this.checkBox_西藥.Text = "西藥";
            this.checkBox_西藥.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_西藥.UseVisualStyleBackColor = true;
            // 
            // rJ_Button_確認選擇
            // 
            this.rJ_Button_確認選擇.AutoResetState = false;
            this.rJ_Button_確認選擇.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_確認選擇.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_確認選擇.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認選擇.BorderRadius = 10;
            this.rJ_Button_確認選擇.BorderSize = 0;
            this.rJ_Button_確認選擇.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認選擇.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認選擇.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認選擇.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認選擇.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認選擇.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認選擇.GUID = "";
            this.rJ_Button_確認選擇.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認選擇.Location = new System.Drawing.Point(288, 42);
            this.rJ_Button_確認選擇.Name = "rJ_Button_確認選擇";
            this.rJ_Button_確認選擇.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認選擇.ProhibitionLineWidth = 4;
            this.rJ_Button_確認選擇.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認選擇.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認選擇.ShadowSize = 3;
            this.rJ_Button_確認選擇.ShowLoadingForm = false;
            this.rJ_Button_確認選擇.Size = new System.Drawing.Size(117, 65);
            this.rJ_Button_確認選擇.State = false;
            this.rJ_Button_確認選擇.TabIndex = 2;
            this.rJ_Button_確認選擇.Text = "確 認";
            this.rJ_Button_確認選擇.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認選擇.TextHeight = 0;
            this.rJ_Button_確認選擇.UseVisualStyleBackColor = false;
            // 
            // Dialog_中西藥選擇
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(412, 114);
            this.ControlBox = true;
            this.Controls.Add(this.rJ_Button_確認選擇);
            this.Controls.Add(this.checkBox_西藥);
            this.Controls.Add(this.checkBox_中藥);
            this.MaximizeBox = false;
            this.Name = "Dialog_中西藥選擇";
            this.Text = "選擇下載藥檔範圍";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_中藥;
        private System.Windows.Forms.CheckBox checkBox_西藥;
        private MyUI.RJ_Button rJ_Button_確認選擇;
    }
}