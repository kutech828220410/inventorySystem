
namespace 中藥調劑系統
{
    partial class Dialog_儲位區域選擇
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
            this.comboBox_區域 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rJ_Button_確認 = new MyUI.RJ_Button();
            this.SuspendLayout();
            // 
            // comboBox_區域
            // 
            this.comboBox_區域.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_區域.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_區域.FormattingEnabled = true;
            this.comboBox_區域.Items.AddRange(new object[] {
            "藥碼",
            "藥名"});
            this.comboBox_區域.Location = new System.Drawing.Point(27, 72);
            this.comboBox_區域.Name = "comboBox_區域";
            this.comboBox_區域.Size = new System.Drawing.Size(388, 45);
            this.comboBox_區域.TabIndex = 126;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 17);
            this.panel1.TabIndex = 127;
            // 
            // rJ_Button_確認
            // 
            this.rJ_Button_確認.AutoResetState = false;
            this.rJ_Button_確認.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_確認.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_確認.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認.BorderRadius = 10;
            this.rJ_Button_確認.BorderSize = 0;
            this.rJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_確認.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認.GUID = "";
            this.rJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_確認.Location = new System.Drawing.Point(455, 45);
            this.rJ_Button_確認.Name = "rJ_Button_確認";
            this.rJ_Button_確認.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_確認.ProhibitionLineWidth = 4;
            this.rJ_Button_確認.ProhibitionSymbolSize = 30;
            this.rJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認.ShadowSize = 3;
            this.rJ_Button_確認.ShowLoadingForm = false;
            this.rJ_Button_確認.Size = new System.Drawing.Size(169, 100);
            this.rJ_Button_確認.State = false;
            this.rJ_Button_確認.TabIndex = 128;
            this.rJ_Button_確認.Text = "確認";
            this.rJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認.TextHeight = 0;
            this.rJ_Button_確認.UseVisualStyleBackColor = false;
            // 
            // Dialog_儲位區域選擇
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(628, 149);
            this.ControlBox = true;
            this.Controls.Add(this.rJ_Button_確認);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBox_區域);
            this.MaximizeBox = false;
            this.Name = "Dialog_儲位區域選擇";
            this.Text = "儲位區域選擇";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_區域;
        private System.Windows.Forms.Panel panel1;
        private MyUI.RJ_Button rJ_Button_確認;
    }
}