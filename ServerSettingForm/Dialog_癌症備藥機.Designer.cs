
namespace ServerSettingForm
{
    partial class Dialog_癌症備藥機
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
            HIS_DB_Lib.ServerSettingClass serverSettingClass1 = new HIS_DB_Lib.ServerSettingClass();
            HIS_DB_Lib.ServerSettingClass serverSettingClass2 = new HIS_DB_Lib.ServerSettingClass();
            this.button_讀取 = new System.Windows.Forms.Button();
            this.button_刪除 = new System.Windows.Forms.Button();
            this.button_上傳 = new System.Windows.Forms.Button();
            this.button_測試 = new System.Windows.Forms.Button();
            this.rJ_TextBox_API_Server = new MyUI.RJ_TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_名稱 = new System.Windows.Forms.ComboBox();
            this.button_新增 = new System.Windows.Forms.Button();
            this.panel_SQLContent2 = new ServerSettingForm.Panel_SQLContent();
            this.panel_SQLContent1 = new ServerSettingForm.Panel_SQLContent();
            this.SuspendLayout();
            // 
            // button_讀取
            // 
            this.button_讀取.Location = new System.Drawing.Point(956, 15);
            this.button_讀取.Name = "button_讀取";
            this.button_讀取.Size = new System.Drawing.Size(89, 44);
            this.button_讀取.TabIndex = 59;
            this.button_讀取.Text = "讀取";
            this.button_讀取.UseVisualStyleBackColor = true;
            // 
            // button_刪除
            // 
            this.button_刪除.Location = new System.Drawing.Point(1605, 12);
            this.button_刪除.Name = "button_刪除";
            this.button_刪除.Size = new System.Drawing.Size(89, 44);
            this.button_刪除.TabIndex = 58;
            this.button_刪除.Text = "刪除";
            this.button_刪除.UseVisualStyleBackColor = true;
            // 
            // button_上傳
            // 
            this.button_上傳.Location = new System.Drawing.Point(861, 15);
            this.button_上傳.Name = "button_上傳";
            this.button_上傳.Size = new System.Drawing.Size(89, 44);
            this.button_上傳.TabIndex = 57;
            this.button_上傳.Text = "上傳";
            this.button_上傳.UseVisualStyleBackColor = true;
            // 
            // button_測試
            // 
            this.button_測試.Location = new System.Drawing.Point(444, 17);
            this.button_測試.Name = "button_測試";
            this.button_測試.Size = new System.Drawing.Size(72, 41);
            this.button_測試.TabIndex = 55;
            this.button_測試.Text = "測試";
            this.button_測試.UseVisualStyleBackColor = true;
            // 
            // rJ_TextBox_API_Server
            // 
            this.rJ_TextBox_API_Server.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_API_Server.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_API_Server.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_API_Server.BorderRadius = 0;
            this.rJ_TextBox_API_Server.BorderSize = 2;
            this.rJ_TextBox_API_Server.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_API_Server.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_API_Server.GUID = "";
            this.rJ_TextBox_API_Server.Location = new System.Drawing.Point(145, 17);
            this.rJ_TextBox_API_Server.Multiline = false;
            this.rJ_TextBox_API_Server.Name = "rJ_TextBox_API_Server";
            this.rJ_TextBox_API_Server.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API_Server.PassWordChar = false;
            this.rJ_TextBox_API_Server.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API_Server.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API_Server.ShowTouchPannel = false;
            this.rJ_TextBox_API_Server.Size = new System.Drawing.Size(293, 40);
            this.rJ_TextBox_API_Server.TabIndex = 54;
            this.rJ_TextBox_API_Server.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API_Server.Texts = "";
            this.rJ_TextBox_API_Server.UnderlineStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 27);
            this.label1.TabIndex = 53;
            this.label1.Text = "API Server :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(522, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 27);
            this.label4.TabIndex = 61;
            this.label4.Text = "名稱 :";
            // 
            // comboBox_名稱
            // 
            this.comboBox_名稱.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_名稱.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_名稱.FormattingEnabled = true;
            this.comboBox_名稱.Location = new System.Drawing.Point(592, 21);
            this.comboBox_名稱.Name = "comboBox_名稱";
            this.comboBox_名稱.Size = new System.Drawing.Size(168, 32);
            this.comboBox_名稱.TabIndex = 60;
            // 
            // button_新增
            // 
            this.button_新增.Location = new System.Drawing.Point(766, 14);
            this.button_新增.Name = "button_新增";
            this.button_新增.Size = new System.Drawing.Size(89, 44);
            this.button_新增.TabIndex = 63;
            this.button_新增.Text = "新增";
            this.button_新增.UseVisualStyleBackColor = true;
            // 
            // panel_SQLContent2
            // 
            this.panel_SQLContent2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SQLContent2.Content = "一般資料";
            this.panel_SQLContent2.Location = new System.Drawing.Point(208, 79);
            this.panel_SQLContent2.Name = "panel_SQLContent2";
            serverSettingClass1.DBName = "";
            serverSettingClass1.GUID = null;
            serverSettingClass1.Password = "";
            serverSettingClass1.Port = "";
            serverSettingClass1.Server = "";
            serverSettingClass1.TableName = null;
            serverSettingClass1.User = "";
            serverSettingClass1.Value = null;
            serverSettingClass1.內容 = "一般資料";
            serverSettingClass1.單位 = null;
            serverSettingClass1.程式類別 = null;
            serverSettingClass1.設備名稱 = "";
            serverSettingClass1.類別 = "癌症備藥機";
            this.panel_SQLContent2.ServerSetting = serverSettingClass1;
            this.panel_SQLContent2.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.癌症備藥機;
            this.panel_SQLContent2.Size = new System.Drawing.Size(175, 309);
            this.panel_SQLContent2.TabIndex = 64;
            // 
            // panel_SQLContent1
            // 
            this.panel_SQLContent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SQLContent1.Content = "排程醫令資料";
            this.panel_SQLContent1.Location = new System.Drawing.Point(27, 79);
            this.panel_SQLContent1.Name = "panel_SQLContent1";
            serverSettingClass2.DBName = "";
            serverSettingClass2.GUID = null;
            serverSettingClass2.Password = "";
            serverSettingClass2.Port = "";
            serverSettingClass2.Server = "";
            serverSettingClass2.TableName = null;
            serverSettingClass2.User = "";
            serverSettingClass2.Value = null;
            serverSettingClass2.內容 = "排程醫令資料";
            serverSettingClass2.單位 = null;
            serverSettingClass2.程式類別 = null;
            serverSettingClass2.設備名稱 = "";
            serverSettingClass2.類別 = "癌症備藥機";
            this.panel_SQLContent1.ServerSetting = serverSettingClass2;
            this.panel_SQLContent1.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.癌症備藥機;
            this.panel_SQLContent1.Size = new System.Drawing.Size(175, 309);
            this.panel_SQLContent1.TabIndex = 62;
            // 
            // Dialog_癌症備藥機
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 970);
            this.Controls.Add(this.panel_SQLContent2);
            this.Controls.Add(this.button_新增);
            this.Controls.Add(this.panel_SQLContent1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox_名稱);
            this.Controls.Add(this.button_讀取);
            this.Controls.Add(this.button_刪除);
            this.Controls.Add(this.button_上傳);
            this.Controls.Add(this.button_測試);
            this.Controls.Add(this.rJ_TextBox_API_Server);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Dialog_癌症備藥機";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_讀取;
        private System.Windows.Forms.Button button_刪除;
        private System.Windows.Forms.Button button_上傳;
        private System.Windows.Forms.Button button_測試;
        private MyUI.RJ_TextBox rJ_TextBox_API_Server;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_名稱;
        private Panel_SQLContent panel_SQLContent1;
        private System.Windows.Forms.Button button_新增;
        private Panel_SQLContent panel_SQLContent2;
    }
}