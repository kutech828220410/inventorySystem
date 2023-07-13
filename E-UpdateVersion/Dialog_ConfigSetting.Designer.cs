
namespace E_UpdateVersion
{
    partial class Dialog_ConfigSetting
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
            this.label_API_URL = new System.Windows.Forms.Label();
            this.label_DeviceName = new System.Windows.Forms.Label();
            this.button_上傳 = new System.Windows.Forms.Button();
            this.button_讀取 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dB_CheckBox3 = new E_UpdateVersion.DB_CheckBox();
            this.dB_TextBox3 = new E_UpdateVersion.DB_TextBox();
            this.dB_CheckBox2 = new E_UpdateVersion.DB_CheckBox();
            this.dB_TextBox2 = new E_UpdateVersion.DB_TextBox();
            this.dB_CheckBox1 = new E_UpdateVersion.DB_CheckBox();
            this.dB_TextBox1 = new E_UpdateVersion.DB_TextBox();
            this.comboBox_預設程式 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_預設程式);
            this.panel1.Controls.Add(this.label_API_URL);
            this.panel1.Controls.Add(this.label_DeviceName);
            this.panel1.Controls.Add(this.button_上傳);
            this.panel1.Controls.Add(this.button_讀取);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panel1.Size = new System.Drawing.Size(1123, 58);
            this.panel1.TabIndex = 0;
            // 
            // label_API_URL
            // 
            this.label_API_URL.AutoSize = true;
            this.label_API_URL.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_API_URL.Location = new System.Drawing.Point(13, 31);
            this.label_API_URL.Name = "label_API_URL";
            this.label_API_URL.Size = new System.Drawing.Size(67, 19);
            this.label_API_URL.TabIndex = 5;
            this.label_API_URL.Text = "API URL";
            // 
            // label_DeviceName
            // 
            this.label_DeviceName.AutoSize = true;
            this.label_DeviceName.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_DeviceName.Location = new System.Drawing.Point(12, 6);
            this.label_DeviceName.Name = "label_DeviceName";
            this.label_DeviceName.Size = new System.Drawing.Size(102, 19);
            this.label_DeviceName.TabIndex = 4;
            this.label_DeviceName.Text = "DeviceName";
            // 
            // button_上傳
            // 
            this.button_上傳.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_上傳.Location = new System.Drawing.Point(919, 0);
            this.button_上傳.Name = "button_上傳";
            this.button_上傳.Size = new System.Drawing.Size(97, 58);
            this.button_上傳.TabIndex = 3;
            this.button_上傳.Text = "上傳";
            this.button_上傳.UseVisualStyleBackColor = true;
            // 
            // button_讀取
            // 
            this.button_讀取.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_讀取.Location = new System.Drawing.Point(1016, 0);
            this.button_讀取.Name = "button_讀取";
            this.button_讀取.Size = new System.Drawing.Size(97, 58);
            this.button_讀取.TabIndex = 2;
            this.button_讀取.Text = "讀取";
            this.button_讀取.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dB_CheckBox1);
            this.groupBox1.Controls.Add(this.dB_TextBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1123, 197);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "調劑台管理系統";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dB_CheckBox2);
            this.groupBox2.Controls.Add(this.dB_TextBox2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1123, 222);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "智能藥庫系統";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dB_CheckBox3);
            this.groupBox3.Controls.Add(this.dB_TextBox3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 477);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1123, 222);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "中心叫號系統";
            // 
            // dB_CheckBox3
            // 
            this.dB_CheckBox3.AutoSize = true;
            this.dB_CheckBox3.ConfigName = "程式致能";
            this.dB_CheckBox3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dB_CheckBox3.Location = new System.Drawing.Point(25, 34);
            this.dB_CheckBox3.Name = "dB_CheckBox3";
            this.dB_CheckBox3.Size = new System.Drawing.Size(93, 25);
            this.dB_CheckBox3.TabIndex = 10;
            this.dB_CheckBox3.Text = "程式致能";
            this.dB_CheckBox3.TitleWidth = 100;
            this.dB_CheckBox3.Type = "中心叫號系統";
            this.dB_CheckBox3.UseVisualStyleBackColor = true;
            this.dB_CheckBox3.Value = "False";
            // 
            // dB_TextBox3
            // 
            this.dB_TextBox3.BackColor = System.Drawing.Color.White;
            this.dB_TextBox3.ConfigName = "系統名稱";
            this.dB_TextBox3.Location = new System.Drawing.Point(25, 68);
            this.dB_TextBox3.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.dB_TextBox3.Name = "dB_TextBox3";
            this.dB_TextBox3.Size = new System.Drawing.Size(314, 30);
            this.dB_TextBox3.TabIndex = 9;
            this.dB_TextBox3.TitleWidth = 150;
            this.dB_TextBox3.Type = "中心叫號系統";
            // 
            // dB_CheckBox2
            // 
            this.dB_CheckBox2.AutoSize = true;
            this.dB_CheckBox2.ConfigName = "程式致能";
            this.dB_CheckBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dB_CheckBox2.Location = new System.Drawing.Point(25, 34);
            this.dB_CheckBox2.Name = "dB_CheckBox2";
            this.dB_CheckBox2.Size = new System.Drawing.Size(93, 25);
            this.dB_CheckBox2.TabIndex = 10;
            this.dB_CheckBox2.Text = "程式致能";
            this.dB_CheckBox2.TitleWidth = 100;
            this.dB_CheckBox2.Type = "智能藥庫系統";
            this.dB_CheckBox2.UseVisualStyleBackColor = true;
            this.dB_CheckBox2.Value = "False";
            // 
            // dB_TextBox2
            // 
            this.dB_TextBox2.BackColor = System.Drawing.Color.White;
            this.dB_TextBox2.ConfigName = "系統名稱";
            this.dB_TextBox2.Location = new System.Drawing.Point(25, 68);
            this.dB_TextBox2.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.dB_TextBox2.Name = "dB_TextBox2";
            this.dB_TextBox2.Size = new System.Drawing.Size(314, 30);
            this.dB_TextBox2.TabIndex = 9;
            this.dB_TextBox2.TitleWidth = 150;
            this.dB_TextBox2.Type = "智能藥庫系統";
            // 
            // dB_CheckBox1
            // 
            this.dB_CheckBox1.AutoSize = true;
            this.dB_CheckBox1.ConfigName = "程式致能";
            this.dB_CheckBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dB_CheckBox1.Location = new System.Drawing.Point(24, 32);
            this.dB_CheckBox1.Name = "dB_CheckBox1";
            this.dB_CheckBox1.Size = new System.Drawing.Size(93, 25);
            this.dB_CheckBox1.TabIndex = 8;
            this.dB_CheckBox1.Text = "程式致能";
            this.dB_CheckBox1.TitleWidth = 100;
            this.dB_CheckBox1.Type = "調劑台管理系統";
            this.dB_CheckBox1.UseVisualStyleBackColor = true;
            this.dB_CheckBox1.Value = "False";
            // 
            // dB_TextBox1
            // 
            this.dB_TextBox1.BackColor = System.Drawing.Color.White;
            this.dB_TextBox1.ConfigName = "系統名稱";
            this.dB_TextBox1.Location = new System.Drawing.Point(24, 66);
            this.dB_TextBox1.Margin = new System.Windows.Forms.Padding(6);
            this.dB_TextBox1.Name = "dB_TextBox1";
            this.dB_TextBox1.Size = new System.Drawing.Size(318, 30);
            this.dB_TextBox1.TabIndex = 7;
            this.dB_TextBox1.TitleWidth = 150;
            this.dB_TextBox1.Type = "調劑台管理系統";
            // 
            // comboBox_預設程式
            // 
            this.comboBox_預設程式.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_預設程式.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_預設程式.FormattingEnabled = true;
            this.comboBox_預設程式.Items.AddRange(new object[] {
            "調劑台管理系統",
            "智能藥庫系統",
            "中心叫號系統"});
            this.comboBox_預設程式.Location = new System.Drawing.Point(732, 11);
            this.comboBox_預設程式.Name = "comboBox_預設程式";
            this.comboBox_預設程式.Size = new System.Drawing.Size(166, 32);
            this.comboBox_預設程式.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(657, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "預設程式";
            // 
            // Dialog_ConfigSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1123, 730);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_ConfigSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "參數設定";
            this.Load += new System.EventHandler(this.Dialog_ConfigSetting_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_上傳;
        private System.Windows.Forms.Button button_讀取;
        private System.Windows.Forms.Label label_API_URL;
        private System.Windows.Forms.Label label_DeviceName;
        private DB_CheckBox dB_CheckBox2;
        private DB_TextBox dB_TextBox2;
        private DB_CheckBox dB_CheckBox1;
        private DB_TextBox dB_TextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private DB_CheckBox dB_CheckBox3;
        private DB_TextBox dB_TextBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_預設程式;
    }
}