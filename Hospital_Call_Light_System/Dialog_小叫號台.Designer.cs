﻿
namespace Hospital_Call_Light_System
{
    partial class Dialog_小叫號台
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
            this.button_第一台號碼輸入 = new System.Windows.Forms.Button();
            this.button_第二台號碼輸入 = new System.Windows.Forms.Button();
            this.label_第一台號碼 = new System.Windows.Forms.Label();
            this.label_第二台號碼 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_第一台號碼輸入
            // 
            this.button_第一台號碼輸入.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_第一台號碼輸入.Location = new System.Drawing.Point(42, 22);
            this.button_第一台號碼輸入.Name = "button_第一台號碼輸入";
            this.button_第一台號碼輸入.Size = new System.Drawing.Size(187, 51);
            this.button_第一台號碼輸入.TabIndex = 18;
            this.button_第一台號碼輸入.Text = "第一台號碼輸入";
            this.button_第一台號碼輸入.UseVisualStyleBackColor = true;
            // 
            // button_第二台號碼輸入
            // 
            this.button_第二台號碼輸入.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_第二台號碼輸入.Location = new System.Drawing.Point(251, 22);
            this.button_第二台號碼輸入.Name = "button_第二台號碼輸入";
            this.button_第二台號碼輸入.Size = new System.Drawing.Size(187, 51);
            this.button_第二台號碼輸入.TabIndex = 19;
            this.button_第二台號碼輸入.Text = "第二台號碼輸入";
            this.button_第二台號碼輸入.UseVisualStyleBackColor = true;
            // 
            // label_第一台號碼
            // 
            this.label_第一台號碼.AutoSize = true;
            this.label_第一台號碼.BackColor = System.Drawing.Color.LightGray;
            this.label_第一台號碼.Font = new System.Drawing.Font("微軟正黑體", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_第一台號碼.ForeColor = System.Drawing.Color.Red;
            this.label_第一台號碼.Location = new System.Drawing.Point(44, 83);
            this.label_第一台號碼.Name = "label_第一台號碼";
            this.label_第一台號碼.Size = new System.Drawing.Size(183, 81);
            this.label_第一台號碼.TabIndex = 20;
            this.label_第一台號碼.Text = "0000";
            // 
            // label_第二台號碼
            // 
            this.label_第二台號碼.AutoSize = true;
            this.label_第二台號碼.BackColor = System.Drawing.Color.LightGray;
            this.label_第二台號碼.Font = new System.Drawing.Font("微軟正黑體", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_第二台號碼.ForeColor = System.Drawing.Color.Red;
            this.label_第二台號碼.Location = new System.Drawing.Point(253, 83);
            this.label_第二台號碼.Name = "label_第二台號碼";
            this.label_第二台號碼.Size = new System.Drawing.Size(183, 81);
            this.label_第二台號碼.TabIndex = 21;
            this.label_第二台號碼.Text = "0000";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(0, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(492, 36);
            this.label1.TabIndex = 22;
            this.label1.Text = "Copyright ©2023 鴻森整合機電有限公司";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Dialog_小叫號台
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(492, 224);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_第二台號碼);
            this.Controls.Add(this.label_第一台號碼);
            this.Controls.Add(this.button_第二台號碼輸入);
            this.Controls.Add(this.button_第一台號碼輸入);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_小叫號台";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Dialog_小叫號台_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_第一台號碼輸入;
        private System.Windows.Forms.Button button_第二台號碼輸入;
        private System.Windows.Forms.Label label_第一台號碼;
        private System.Windows.Forms.Label label_第二台號碼;
        private System.Windows.Forms.Label label1;
    }
}