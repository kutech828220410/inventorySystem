
namespace 調劑台管理系統
{
    partial class Dialog_盤點數量錯誤
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
            this.rJ_Lable_Title = new MyUI.RJ_Lable();
            this.rJ_Button_是 = new MyUI.RJ_Button();
            this.rJ_Button_否 = new MyUI.RJ_Button();
            this.SuspendLayout();
            // 
            // rJ_Lable_Title
            // 
            this.rJ_Lable_Title.BackColor = System.Drawing.Color.Navy;
            this.rJ_Lable_Title.BackgroundColor = System.Drawing.Color.Navy;
            this.rJ_Lable_Title.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_Title.BorderRadius = 12;
            this.rJ_Lable_Title.BorderSize = 0;
            this.rJ_Lable_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_Title.Font = new System.Drawing.Font("微軟正黑體", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_Title.ForeColor = System.Drawing.Color.White;
            this.rJ_Lable_Title.GUID = "";
            this.rJ_Lable_Title.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable_Title.Name = "rJ_Lable_Title";
            this.rJ_Lable_Title.Size = new System.Drawing.Size(830, 112);
            this.rJ_Lable_Title.TabIndex = 3;
            this.rJ_Lable_Title.Text = "盤點數量錯誤,重新盤點?";
            this.rJ_Lable_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_Title.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Button_是
            // 
            this.rJ_Button_是.AutoResetState = false;
            this.rJ_Button_是.BackColor = System.Drawing.Color.YellowGreen;
            this.rJ_Button_是.BackgroundColor = System.Drawing.Color.YellowGreen;
            this.rJ_Button_是.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_是.BorderRadius = 116;
            this.rJ_Button_是.BorderSize = 0;
            this.rJ_Button_是.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_是.FlatAppearance.BorderSize = 0;
            this.rJ_Button_是.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_是.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_是.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_是.GUID = "";
            this.rJ_Button_是.Location = new System.Drawing.Point(492, 127);
            this.rJ_Button_是.Name = "rJ_Button_是";
            this.rJ_Button_是.Size = new System.Drawing.Size(210, 116);
            this.rJ_Button_是.State = false;
            this.rJ_Button_是.TabIndex = 4;
            this.rJ_Button_是.Text = "是";
            this.rJ_Button_是.TextColor = System.Drawing.Color.White;
            this.rJ_Button_是.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_否
            // 
            this.rJ_Button_否.AutoResetState = false;
            this.rJ_Button_否.BackColor = System.Drawing.Color.Crimson;
            this.rJ_Button_否.BackgroundColor = System.Drawing.Color.Crimson;
            this.rJ_Button_否.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_否.BorderRadius = 116;
            this.rJ_Button_否.BorderSize = 0;
            this.rJ_Button_否.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_否.FlatAppearance.BorderSize = 0;
            this.rJ_Button_否.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_否.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_否.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_否.GUID = "";
            this.rJ_Button_否.Location = new System.Drawing.Point(118, 127);
            this.rJ_Button_否.Name = "rJ_Button_否";
            this.rJ_Button_否.Size = new System.Drawing.Size(210, 116);
            this.rJ_Button_否.State = false;
            this.rJ_Button_否.TabIndex = 5;
            this.rJ_Button_否.Text = "否";
            this.rJ_Button_否.TextColor = System.Drawing.Color.White;
            this.rJ_Button_否.UseVisualStyleBackColor = false;
            // 
            // Dialog_盤點數量錯誤
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(830, 259);
            this.ControlBox = false;
            this.Controls.Add(this.rJ_Button_否);
            this.Controls.Add(this.rJ_Button_是);
            this.Controls.Add(this.rJ_Lable_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Dialog_盤點數量錯誤";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_Lable rJ_Lable_Title;
        private MyUI.RJ_Button rJ_Button_是;
        private MyUI.RJ_Button rJ_Button_否;
    }
}