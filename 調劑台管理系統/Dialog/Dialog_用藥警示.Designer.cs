namespace 調劑台管理系統
{
    partial class Dialog_用藥警示
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.SuspendLayout();
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.LightGray;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.Black;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 2;
            this.rJ_Lable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(4, 64);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.OrangeRed;
            this.rJ_Lable1.ShadowSize = 3;
            this.rJ_Lable1.Size = new System.Drawing.Size(1074, 63);
            this.rJ_Lable1.TabIndex = 0;
            this.rJ_Lable1.Text = "過敏藥品";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.White;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.OrangeRed;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(4, 127);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1074, 384);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Keto lml(30mg)(Ketorolac)\r\n\r\nErythrocin(Erythromycin)oph.oint.\r\n\r\n(口服)Voren 50mg(" +
    "Diclofenac sod.)";
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.BorderRadius = 22;
            this.rJ_Button_確認.BorderSize = 1;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(4, 511);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 0;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(1074, 94);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 163;
            this.rJ_Button_確認.Text = "確      認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // Dialog_用藥警示
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.CaptionHeight = 60;
            this.ClientSize = new System.Drawing.Size(1082, 609);
            this.CloseBoxSize = new System.Drawing.Size(60, 60);
            this.ControlBox = true;
            this.Controls.Add(this.rJ_Button_確認);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rJ_Lable1);
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(60, 60);
            this.MiniSize = new System.Drawing.Size(60, 60);
            this.Name = "Dialog_用藥警示";
            this.Text = "用藥警示";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyUI.RJ_Lable rJ_Lable1;
        private System.Windows.Forms.TextBox textBox1;
        private MyUI.RJ_Button rJ_Button_確認;
    }
}