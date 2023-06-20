
namespace ServerSettingForm
{
    partial class Dialog_新增
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
            this.rJ_TextBox_Value = new MyUI.RJ_TextBox();
            this.button_確認 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rJ_TextBox_Value
            // 
            this.rJ_TextBox_Value.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_Value.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_Value.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_Value.BorderRadius = 0;
            this.rJ_TextBox_Value.BorderSize = 2;
            this.rJ_TextBox_Value.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_Value.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_Value.Location = new System.Drawing.Point(25, 21);
            this.rJ_TextBox_Value.Multiline = false;
            this.rJ_TextBox_Value.Name = "rJ_TextBox_Value";
            this.rJ_TextBox_Value.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_Value.PassWordChar = false;
            this.rJ_TextBox_Value.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_Value.PlaceholderText = "請輸入名稱";
            this.rJ_TextBox_Value.ShowTouchPannel = false;
            this.rJ_TextBox_Value.Size = new System.Drawing.Size(407, 40);
            this.rJ_TextBox_Value.TabIndex = 4;
            this.rJ_TextBox_Value.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_Value.Texts = "";
            this.rJ_TextBox_Value.UnderlineStyle = false;
            // 
            // button_確認
            // 
            this.button_確認.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_確認.Location = new System.Drawing.Point(448, 19);
            this.button_確認.Name = "button_確認";
            this.button_確認.Size = new System.Drawing.Size(103, 45);
            this.button_確認.TabIndex = 5;
            this.button_確認.Text = "確認";
            this.button_確認.UseVisualStyleBackColor = true;
            // 
            // Dialog_新增
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(563, 84);
            this.Controls.Add(this.button_確認);
            this.Controls.Add(this.rJ_TextBox_Value);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_新增";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Dialog_新增_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_確認;
        private MyUI.RJ_TextBox rJ_TextBox_Value;
    }
}