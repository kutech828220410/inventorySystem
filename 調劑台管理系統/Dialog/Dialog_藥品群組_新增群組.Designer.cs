
namespace 調劑台管理系統
{
    partial class Dialog_藥品群組_新增群組
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
            this.rJ_TextBox_名稱 = new MyUI.RJ_TextBox();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.SuspendLayout();
            // 
            // rJ_TextBox_名稱
            // 
            this.rJ_TextBox_名稱.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_名稱.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_名稱.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_名稱.BorderRadius = 0;
            this.rJ_TextBox_名稱.BorderSize = 2;
            this.rJ_TextBox_名稱.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_名稱.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_名稱.GUID = "";
            this.rJ_TextBox_名稱.Location = new System.Drawing.Point(31, 59);
            this.rJ_TextBox_名稱.Multiline = false;
            this.rJ_TextBox_名稱.Name = "rJ_TextBox_名稱";
            this.rJ_TextBox_名稱.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_名稱.PassWordChar = false;
            this.rJ_TextBox_名稱.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_名稱.PlaceholderText = "請輸入群組名稱";
            this.rJ_TextBox_名稱.ShowTouchPannel = false;
            this.rJ_TextBox_名稱.Size = new System.Drawing.Size(566, 57);
            this.rJ_TextBox_名稱.TabIndex = 51;
            this.rJ_TextBox_名稱.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_名稱.Texts = "";
            this.rJ_TextBox_名稱.UnderlineStyle = false;
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認.BorderRadius = 20;
            this.rJ_Button_確認.BorderSize = 0;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(608, 44);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(119, 86);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 52;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // Dialog_藥品群組_新增群組
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(754, 149);
            this.ControlBox = true;
            this.Controls.Add(this.rJ_Button_確認);
            this.Controls.Add(this.rJ_TextBox_名稱);
            this.MaximizeBox = false;
            this.Name = "Dialog_藥品群組_新增群組";
            this.Text = "新增群組";
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_TextBox rJ_TextBox_名稱;
        private MyUI.RJ_Button rJ_Button_確認;
    }
}