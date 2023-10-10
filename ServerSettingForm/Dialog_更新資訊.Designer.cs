
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
            HIS_DB_Lib.ServerSettingClass serverSettingClass1 = new HIS_DB_Lib.ServerSettingClass();
            HIS_DB_Lib.updateVersionClass updateVersionClass1 = new HIS_DB_Lib.updateVersionClass();
            HIS_DB_Lib.updateVersionClass updateVersionClass2 = new HIS_DB_Lib.updateVersionClass();
            HIS_DB_Lib.updateVersionClass updateVersionClass3 = new HIS_DB_Lib.updateVersionClass();
            HIS_DB_Lib.updateVersionClass updateVersionClass4 = new HIS_DB_Lib.updateVersionClass();
            HIS_DB_Lib.updateVersionClass updateVersionClass5 = new HIS_DB_Lib.updateVersionClass();
            HIS_DB_Lib.updateVersionClass updateVersionClass6 = new HIS_DB_Lib.updateVersionClass();
            HIS_DB_Lib.updateVersionClass updateVersionClass7 = new HIS_DB_Lib.updateVersionClass();
            this.label1 = new System.Windows.Forms.Label();
            this.rJ_TextBox_API_Server = new MyUI.RJ_TextBox();
            this.button_測試 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_上傳 = new System.Windows.Forms.Button();
            this.button_刪除 = new System.Windows.Forms.Button();
            this.button_讀取 = new System.Windows.Forms.Button();
            this.label_名稱 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel_SQLContent1 = new ServerSettingForm.Panel_SQLContent();
            this.panel_UpdateVersion1 = new ServerSettingForm.Panel_UpdateVersion();
            this.panel_UpdateVersion2 = new ServerSettingForm.Panel_UpdateVersion();
            this.panel_UpdateVersion3 = new ServerSettingForm.Panel_UpdateVersion();
            this.panel_UpdateVersion4 = new ServerSettingForm.Panel_UpdateVersion();
            this.panel_UpdateVersion5 = new ServerSettingForm.Panel_UpdateVersion();
            this.panel_UpdateVersion6 = new ServerSettingForm.Panel_UpdateVersion();
            this.panel_UpdateVersion7 = new ServerSettingForm.Panel_UpdateVersion();
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
            // panel_SQLContent1
            // 
            this.panel_SQLContent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SQLContent1.Content = "一般資料";
            this.panel_SQLContent1.Location = new System.Drawing.Point(17, 74);
            this.panel_SQLContent1.Name = "panel_SQLContent1";
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
            serverSettingClass1.類別 = "更新資訊";
            this.panel_SQLContent1.ServerSetting = serverSettingClass1;
            this.panel_SQLContent1.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.更新資訊;
            this.panel_SQLContent1.Size = new System.Drawing.Size(175, 309);
            this.panel_SQLContent1.TabIndex = 67;
            // 
            // panel_UpdateVersion1
            // 
            this.panel_UpdateVersion1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_UpdateVersion1.Location = new System.Drawing.Point(210, 243);
            this.panel_UpdateVersion1.Name = "panel_UpdateVersion1";
            this.panel_UpdateVersion1.Program_name = "調劑台";
            this.panel_UpdateVersion1.Size = new System.Drawing.Size(1072, 106);
            this.panel_UpdateVersion1.TabIndex = 68;
            updateVersionClass1.enable = "False";
            updateVersionClass1.filepath = "";
            updateVersionClass1.GUID = null;
            updateVersionClass1.program_name = "調劑台";
            updateVersionClass1.update_time = "0000/00/00 00:00:00";
            updateVersionClass1.version = "";
            this.panel_UpdateVersion1.UpdateVersionClass = updateVersionClass1;
            // 
            // panel_UpdateVersion2
            // 
            this.panel_UpdateVersion2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_UpdateVersion2.Location = new System.Drawing.Point(210, 355);
            this.panel_UpdateVersion2.Name = "panel_UpdateVersion2";
            this.panel_UpdateVersion2.Program_name = "藥庫";
            this.panel_UpdateVersion2.Size = new System.Drawing.Size(1072, 106);
            this.panel_UpdateVersion2.TabIndex = 69;
            updateVersionClass2.enable = "False";
            updateVersionClass2.filepath = "";
            updateVersionClass2.GUID = null;
            updateVersionClass2.program_name = "藥庫";
            updateVersionClass2.update_time = "0000/00/00 00:00:00";
            updateVersionClass2.version = "";
            this.panel_UpdateVersion2.UpdateVersionClass = updateVersionClass2;
            // 
            // panel_UpdateVersion3
            // 
            this.panel_UpdateVersion3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_UpdateVersion3.Location = new System.Drawing.Point(210, 467);
            this.panel_UpdateVersion3.Name = "panel_UpdateVersion3";
            this.panel_UpdateVersion3.Program_name = "中心叫號系統";
            this.panel_UpdateVersion3.Size = new System.Drawing.Size(1072, 106);
            this.panel_UpdateVersion3.TabIndex = 70;
            updateVersionClass3.enable = "False";
            updateVersionClass3.filepath = "";
            updateVersionClass3.GUID = null;
            updateVersionClass3.program_name = "中心叫號系統";
            updateVersionClass3.update_time = "0000/00/00 00:00:00";
            updateVersionClass3.version = "";
            this.panel_UpdateVersion3.UpdateVersionClass = updateVersionClass3;
            // 
            // panel_UpdateVersion4
            // 
            this.panel_UpdateVersion4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_UpdateVersion4.Location = new System.Drawing.Point(210, 74);
            this.panel_UpdateVersion4.Name = "panel_UpdateVersion4";
            this.panel_UpdateVersion4.Program_name = "update";
            this.panel_UpdateVersion4.Size = new System.Drawing.Size(1072, 106);
            this.panel_UpdateVersion4.TabIndex = 71;
            updateVersionClass4.enable = "False";
            updateVersionClass4.filepath = "";
            updateVersionClass4.GUID = null;
            updateVersionClass4.program_name = "update";
            updateVersionClass4.update_time = "0000/00/00 00:00:00";
            updateVersionClass4.version = "";
            this.panel_UpdateVersion4.UpdateVersionClass = updateVersionClass4;
            // 
            // panel_UpdateVersion5
            // 
            this.panel_UpdateVersion5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_UpdateVersion5.Location = new System.Drawing.Point(12, 745);
            this.panel_UpdateVersion5.Name = "panel_UpdateVersion5";
            this.panel_UpdateVersion5.Program_name = "temp0";
            this.panel_UpdateVersion5.Size = new System.Drawing.Size(1072, 106);
            this.panel_UpdateVersion5.TabIndex = 72;
            updateVersionClass5.enable = "False";
            updateVersionClass5.filepath = "";
            updateVersionClass5.GUID = null;
            updateVersionClass5.program_name = "temp0";
            updateVersionClass5.update_time = "0000/00/00 00:00:00";
            updateVersionClass5.version = "";
            this.panel_UpdateVersion5.UpdateVersionClass = updateVersionClass5;
            // 
            // panel_UpdateVersion6
            // 
            this.panel_UpdateVersion6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_UpdateVersion6.Location = new System.Drawing.Point(12, 857);
            this.panel_UpdateVersion6.Name = "panel_UpdateVersion6";
            this.panel_UpdateVersion6.Program_name = "temp1";
            this.panel_UpdateVersion6.Size = new System.Drawing.Size(1072, 106);
            this.panel_UpdateVersion6.TabIndex = 73;
            updateVersionClass6.enable = "False";
            updateVersionClass6.filepath = "";
            updateVersionClass6.GUID = null;
            updateVersionClass6.program_name = "temp1";
            updateVersionClass6.update_time = "0000/00/00 00:00:00";
            updateVersionClass6.version = "";
            this.panel_UpdateVersion6.UpdateVersionClass = updateVersionClass6;
            // 
            // panel_UpdateVersion7
            // 
            this.panel_UpdateVersion7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_UpdateVersion7.Location = new System.Drawing.Point(210, 579);
            this.panel_UpdateVersion7.Name = "panel_UpdateVersion7";
            this.panel_UpdateVersion7.Program_name = "傳送櫃";
            this.panel_UpdateVersion7.Size = new System.Drawing.Size(1072, 106);
            this.panel_UpdateVersion7.TabIndex = 74;
            updateVersionClass7.enable = "False";
            updateVersionClass7.filepath = "";
            updateVersionClass7.GUID = null;
            updateVersionClass7.program_name = "傳送櫃";
            updateVersionClass7.update_time = "0000/00/00 00:00:00";
            updateVersionClass7.version = "";
            this.panel_UpdateVersion7.UpdateVersionClass = updateVersionClass7;
            // 
            // Dialog_更新資訊
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1801, 970);
            this.Controls.Add(this.panel_UpdateVersion7);
            this.Controls.Add(this.panel_UpdateVersion6);
            this.Controls.Add(this.panel_UpdateVersion5);
            this.Controls.Add(this.panel_UpdateVersion4);
            this.Controls.Add(this.panel_UpdateVersion3);
            this.Controls.Add(this.panel_UpdateVersion2);
            this.Controls.Add(this.panel_UpdateVersion1);
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
        private Panel_UpdateVersion panel_UpdateVersion1;
        private Panel_UpdateVersion panel_UpdateVersion2;
        private Panel_UpdateVersion panel_UpdateVersion3;
        private Panel_UpdateVersion panel_UpdateVersion4;
        private Panel_UpdateVersion panel_UpdateVersion5;
        private Panel_UpdateVersion panel_UpdateVersion6;
        private Panel_UpdateVersion panel_UpdateVersion7;
    }
}