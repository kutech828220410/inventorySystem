
namespace E_UpdateVersion
{
    partial class Dialog_SetApiServer
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
            this.rJ_TextBox_API_URL = new MyUI.RJ_TextBox();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.SuspendLayout();
            // 
            // rJ_TextBox_API_URL
            // 
            this.rJ_TextBox_API_URL.AutoSize = true;
            this.rJ_TextBox_API_URL.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_API_URL.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_API_URL.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_API_URL.BorderRadius = 0;
            this.rJ_TextBox_API_URL.BorderSize = 2;
            this.rJ_TextBox_API_URL.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_API_URL.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_API_URL.GUID = "";
            this.rJ_TextBox_API_URL.Location = new System.Drawing.Point(12, 32);
            this.rJ_TextBox_API_URL.Multiline = false;
            this.rJ_TextBox_API_URL.Name = "rJ_TextBox_API_URL";
            this.rJ_TextBox_API_URL.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API_URL.PassWordChar = false;
            this.rJ_TextBox_API_URL.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API_URL.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API_URL.ShowTouchPannel = false;
            this.rJ_TextBox_API_URL.Size = new System.Drawing.Size(470, 36);
            this.rJ_TextBox_API_URL.TabIndex = 25;
            this.rJ_TextBox_API_URL.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API_URL.Texts = "";
            this.rJ_TextBox_API_URL.UnderlineStyle = false;
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認.BorderRadius = 5;
            this.rJ_Button_確認.BorderSize = 0;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Location = new System.Drawing.Point(498, 12);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.Size = new System.Drawing.Size(145, 73);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 26;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // Dialog_SetApiServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(654, 102);
            this.ControlBox = false;
            this.Controls.Add(this.rJ_Button_確認);
            this.Controls.Add(this.rJ_TextBox_API_URL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_SetApiServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API Server 設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyUI.RJ_TextBox rJ_TextBox_API_URL;
        private MyUI.RJ_Button rJ_Button_確認;
    }
}