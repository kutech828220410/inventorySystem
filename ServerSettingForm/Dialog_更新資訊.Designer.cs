
namespace ServerSettingForm
{
    partial class Dialog_更新資訊
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
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass1 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass2 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass3 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass4 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass5 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass6 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass7 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass8 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_updateVersionClass sys_updateVersionClass9 = new HIS_DB_Lib.sys_updateVersionClass();
            HIS_DB_Lib.sys_serverSettingClass sys_serverSettingClass1 = new HIS_DB_Lib.sys_serverSettingClass();
            this.label1 = new System.Windows.Forms.Label();
            this.rJ_TextBox_API_Server = new MyUI.RJ_TextBox();
            this.button_測試 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_上傳 = new System.Windows.Forms.Button();
            this.button_刪除 = new System.Windows.Forms.Button();
            this.button_讀取 = new System.Windows.Forms.Button();
            this.label_名稱 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel_sys_updateVersion9 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion8 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion7 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion6 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion5 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion4 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion3 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion2 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_sys_updateVersion1 = new ServerSettingForm.Panel_sys_updateVersion();
            this.panel_SQLContent1 = new ServerSettingForm.Panel_SQLContent();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "API Server :";
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
            this.rJ_TextBox_API_Server.Location = new System.Drawing.Point(135, 13);
            this.rJ_TextBox_API_Server.Multiline = false;
            this.rJ_TextBox_API_Server.Name = "rJ_TextBox_API_Server";
            this.rJ_TextBox_API_Server.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API_Server.PassWordChar = false;
            this.rJ_TextBox_API_Server.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API_Server.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API_Server.ShowTouchPannel = false;
            this.rJ_TextBox_API_Server.Size = new System.Drawing.Size(293, 40);
            this.rJ_TextBox_API_Server.TabIndex = 2;
            this.rJ_TextBox_API_Server.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API_Server.Texts = "";
            this.rJ_TextBox_API_Server.UnderlineStyle = false;
            // 
            // button_測試
            // 
            this.button_測試.Location = new System.Drawing.Point(434, 13);
            this.button_測試.Name = "button_測試";
            this.button_測試.Size = new System.Drawing.Size(72, 41);
            this.button_測試.TabIndex = 3;
            this.button_測試.Text = "測試";
            this.button_測試.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(512, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 27);
            this.label4.TabIndex = 7;
            this.label4.Text = "名稱 :";
            // 
            // button_上傳
            // 
            this.button_上傳.Location = new System.Drawing.Point(1500, 8);
            this.button_上傳.Name = "button_上傳";
            this.button_上傳.Size = new System.Drawing.Size(89, 44);
            this.button_上傳.TabIndex = 8;
            this.button_上傳.Text = "上傳";
            this.button_上傳.UseVisualStyleBackColor = true;
            // 
            // button_刪除
            // 
            this.button_刪除.Location = new System.Drawing.Point(1595, 8);
            this.button_刪除.Name = "button_刪除";
            this.button_刪除.Size = new System.Drawing.Size(89, 44);
            this.button_刪除.TabIndex = 22;
            this.button_刪除.Text = "刪除";
            this.button_刪除.UseVisualStyleBackColor = true;
            // 
            // button_讀取
            // 
            this.button_讀取.Location = new System.Drawing.Point(1690, 8);
            this.button_讀取.Name = "button_讀取";
            this.button_讀取.Size = new System.Drawing.Size(89, 44);
            this.button_讀取.TabIndex = 23;
            this.button_讀取.Text = "讀取";
            this.button_讀取.UseVisualStyleBackColor = true;
            // 
            // label_名稱
            // 
            this.label_名稱.AutoSize = true;
            this.label_名稱.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_名稱.Location = new System.Drawing.Point(582, 20);
            this.label_名稱.Name = "label_名稱";
            this.label_名稱.Size = new System.Drawing.Size(83, 27);
            this.label_名稱.TabIndex = 52;
            this.label_名稱.Text = "update";
            // 
            // panel_sys_updateVersion9
            // 
            this.panel_sys_updateVersion9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion9.Location = new System.Drawing.Point(549, 463);
            this.panel_sys_updateVersion9.Name = "panel_sys_updateVersion9";
            this.panel_sys_updateVersion9.Program_name = "癌症備藥機";
            this.panel_sys_updateVersion9.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion9.TabIndex = 76;
            sys_updateVersionClass1.enable = "False";
            sys_updateVersionClass1.filepath = "";
            sys_updateVersionClass1.GUID = null;
            sys_updateVersionClass1.program_name = "癌症備藥機";
            sys_updateVersionClass1.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass1.version = "";
            this.panel_sys_updateVersion9.sys_updateVersionClass = sys_updateVersionClass1;
            // 
            // panel_sys_updateVersion8
            // 
            this.panel_sys_updateVersion8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion8.Location = new System.Drawing.Point(549, 297);
            this.panel_sys_updateVersion8.Name = "panel_sys_updateVersion8";
            this.panel_sys_updateVersion8.Program_name = "中藥調劑系統";
            this.panel_sys_updateVersion8.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion8.TabIndex = 75;
            sys_updateVersionClass2.enable = "False";
            sys_updateVersionClass2.filepath = "";
            sys_updateVersionClass2.GUID = null;
            sys_updateVersionClass2.program_name = "中藥調劑系統";
            sys_updateVersionClass2.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass2.version = "";
            this.panel_sys_updateVersion8.sys_updateVersionClass = sys_updateVersionClass2;
            // 
            // panel_sys_updateVersion7
            // 
            this.panel_sys_updateVersion7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion7.Location = new System.Drawing.Point(17, 795);
            this.panel_sys_updateVersion7.Name = "panel_sys_updateVersion7";
            this.panel_sys_updateVersion7.Program_name = "傳送櫃";
            this.panel_sys_updateVersion7.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion7.TabIndex = 74;
            sys_updateVersionClass3.enable = "False";
            sys_updateVersionClass3.filepath = "";
            sys_updateVersionClass3.GUID = null;
            sys_updateVersionClass3.program_name = "傳送櫃";
            sys_updateVersionClass3.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass3.version = "";
            this.panel_sys_updateVersion7.sys_updateVersionClass = sys_updateVersionClass3;
            // 
            // panel_sys_updateVersion6
            // 
            this.panel_sys_updateVersion6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion6.Location = new System.Drawing.Point(1263, 795);
            this.panel_sys_updateVersion6.Name = "panel_sys_updateVersion6";
            this.panel_sys_updateVersion6.Program_name = "temp1";
            this.panel_sys_updateVersion6.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion6.TabIndex = 73;
            sys_updateVersionClass4.enable = "False";
            sys_updateVersionClass4.filepath = "";
            sys_updateVersionClass4.GUID = null;
            sys_updateVersionClass4.program_name = "temp1";
            sys_updateVersionClass4.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass4.version = "";
            this.panel_sys_updateVersion6.sys_updateVersionClass = sys_updateVersionClass4;
            // 
            // panel_sys_updateVersion5
            // 
            this.panel_sys_updateVersion5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion5.Location = new System.Drawing.Point(1263, 629);
            this.panel_sys_updateVersion5.Name = "panel_sys_updateVersion5";
            this.panel_sys_updateVersion5.Program_name = "temp0";
            this.panel_sys_updateVersion5.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion5.TabIndex = 72;
            sys_updateVersionClass5.enable = "False";
            sys_updateVersionClass5.filepath = "";
            sys_updateVersionClass5.GUID = null;
            sys_updateVersionClass5.program_name = "temp0";
            sys_updateVersionClass5.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass5.version = "";
            this.panel_sys_updateVersion5.sys_updateVersionClass = sys_updateVersionClass5;
            // 
            // panel_sys_updateVersion4
            // 
            this.panel_sys_updateVersion4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion4.Location = new System.Drawing.Point(17, 60);
            this.panel_sys_updateVersion4.Name = "panel_sys_updateVersion4";
            this.panel_sys_updateVersion4.Program_name = "update";
            this.panel_sys_updateVersion4.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion4.TabIndex = 71;
            sys_updateVersionClass6.enable = "False";
            sys_updateVersionClass6.filepath = "";
            sys_updateVersionClass6.GUID = null;
            sys_updateVersionClass6.program_name = "update";
            sys_updateVersionClass6.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass6.version = "";
            this.panel_sys_updateVersion4.sys_updateVersionClass = sys_updateVersionClass6;
            // 
            // panel_sys_updateVersion3
            // 
            this.panel_sys_updateVersion3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion3.Location = new System.Drawing.Point(17, 629);
            this.panel_sys_updateVersion3.Name = "panel_sys_updateVersion3";
            this.panel_sys_updateVersion3.Program_name = "中心叫號系統";
            this.panel_sys_updateVersion3.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion3.TabIndex = 70;
            sys_updateVersionClass7.enable = "False";
            sys_updateVersionClass7.filepath = "";
            sys_updateVersionClass7.GUID = null;
            sys_updateVersionClass7.program_name = "中心叫號系統";
            sys_updateVersionClass7.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass7.version = "";
            this.panel_sys_updateVersion3.sys_updateVersionClass = sys_updateVersionClass7;
            // 
            // panel_sys_updateVersion2
            // 
            this.panel_sys_updateVersion2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion2.Location = new System.Drawing.Point(17, 463);
            this.panel_sys_updateVersion2.Name = "panel_sys_updateVersion2";
            this.panel_sys_updateVersion2.Program_name = "藥庫";
            this.panel_sys_updateVersion2.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion2.TabIndex = 69;
            sys_updateVersionClass8.enable = "False";
            sys_updateVersionClass8.filepath = "";
            sys_updateVersionClass8.GUID = null;
            sys_updateVersionClass8.program_name = "藥庫";
            sys_updateVersionClass8.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass8.version = "";
            this.panel_sys_updateVersion2.sys_updateVersionClass = sys_updateVersionClass8;
            // 
            // panel_sys_updateVersion1
            // 
            this.panel_sys_updateVersion1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_sys_updateVersion1.Location = new System.Drawing.Point(17, 297);
            this.panel_sys_updateVersion1.Name = "panel_sys_updateVersion1";
            this.panel_sys_updateVersion1.Program_name = "調劑台";
            this.panel_sys_updateVersion1.Size = new System.Drawing.Size(526, 160);
            this.panel_sys_updateVersion1.TabIndex = 68;
            sys_updateVersionClass9.enable = "False";
            sys_updateVersionClass9.filepath = "";
            sys_updateVersionClass9.GUID = null;
            sys_updateVersionClass9.program_name = "調劑台";
            sys_updateVersionClass9.update_time = "0000/00/00 00:00:00";
            sys_updateVersionClass9.version = "";
            this.panel_sys_updateVersion1.sys_updateVersionClass = sys_updateVersionClass9;
            // 
            // panel_SQLContent1
            // 
            this.panel_SQLContent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SQLContent1.Content = "一般資料";
            this.panel_SQLContent1.Location = new System.Drawing.Point(1604, 71);
            this.panel_SQLContent1.Name = "panel_SQLContent1";
            sys_serverSettingClass1.DBName = "";
            sys_serverSettingClass1.GUID = null;
            sys_serverSettingClass1.Password = "";
            sys_serverSettingClass1.Port = "";
            sys_serverSettingClass1.Server = "";
            sys_serverSettingClass1.TableName = null;
            sys_serverSettingClass1.User = "";
            sys_serverSettingClass1.Value = null;
            sys_serverSettingClass1.內容 = "一般資料";
            sys_serverSettingClass1.單位 = null;
            sys_serverSettingClass1.程式類別 = null;
            sys_serverSettingClass1.設備名稱 = "";
            sys_serverSettingClass1.類別 = "更新資訊";
            this.panel_SQLContent1.ServerSetting = sys_serverSettingClass1;
            this.panel_SQLContent1.ServerSetting_Type = HIS_DB_Lib.enum_sys_serverSetting_Type.更新資訊;
            this.panel_SQLContent1.Size = new System.Drawing.Size(175, 309);
            this.panel_SQLContent1.TabIndex = 67;
            // 
            // Dialog_更新資訊
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1801, 970);
            this.Controls.Add(this.panel_sys_updateVersion9);
            this.Controls.Add(this.panel_sys_updateVersion8);
            this.Controls.Add(this.panel_sys_updateVersion7);
            this.Controls.Add(this.panel_sys_updateVersion6);
            this.Controls.Add(this.panel_sys_updateVersion5);
            this.Controls.Add(this.panel_sys_updateVersion4);
            this.Controls.Add(this.panel_sys_updateVersion3);
            this.Controls.Add(this.panel_sys_updateVersion2);
            this.Controls.Add(this.panel_sys_updateVersion1);
            this.Controls.Add(this.panel_SQLContent1);
            this.Controls.Add(this.label_名稱);
            this.Controls.Add(this.button_讀取);
            this.Controls.Add(this.button_刪除);
            this.Controls.Add(this.button_上傳);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_測試);
            this.Controls.Add(this.rJ_TextBox_API_Server);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Dialog_更新資訊";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Dialog_更新資訊_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MyUI.RJ_TextBox rJ_TextBox_API_Server;
        private System.Windows.Forms.Button button_測試;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_上傳;
        private System.Windows.Forms.Button button_刪除;
        private System.Windows.Forms.Button button_讀取;
        private System.Windows.Forms.Label label_名稱;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel_SQLContent panel_SQLContent1;
        private Panel_sys_updateVersion panel_sys_updateVersion1;
        private Panel_sys_updateVersion panel_sys_updateVersion2;
        private Panel_sys_updateVersion panel_sys_updateVersion3;
        private Panel_sys_updateVersion panel_sys_updateVersion4;
        private Panel_sys_updateVersion panel_sys_updateVersion5;
        private Panel_sys_updateVersion panel_sys_updateVersion6;
        private Panel_sys_updateVersion panel_sys_updateVersion7;
        private Panel_sys_updateVersion panel_sys_updateVersion8;
        private Panel_sys_updateVersion panel_sys_updateVersion9;
    }
}