
namespace ServerSettingForm
{
    partial class Dialog_中藥調劑系統
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
            HIS_DB_Lib.ServerSettingClass serverSettingClass3 = new HIS_DB_Lib.ServerSettingClass();
            HIS_DB_Lib.ServerSettingClass serverSettingClass4 = new HIS_DB_Lib.ServerSettingClass();
            HIS_DB_Lib.ServerSettingClass serverSettingClass5 = new HIS_DB_Lib.ServerSettingClass();
            HIS_DB_Lib.ServerSettingClass serverSettingClass6 = new HIS_DB_Lib.ServerSettingClass();
            this.button_讀取 = new System.Windows.Forms.Button();
            this.button_刪除 = new System.Windows.Forms.Button();
            this.button_新增 = new System.Windows.Forms.Button();
            this.button_上傳 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_名稱 = new System.Windows.Forms.ComboBox();
            this.button_測試 = new System.Windows.Forms.Button();
            this.rJ_TextBox_API_Server = new MyUI.RJ_TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_SQLContent1 = new ServerSettingForm.Panel_SQLContent();
            this.panel_API_URL5 = new ServerSettingForm.Panel_API_URL();
            this.panel_API_URL4 = new ServerSettingForm.Panel_API_URL();
            this.panel_API_URL2 = new ServerSettingForm.Panel_API_URL();
            this.panel_API_URL1 = new ServerSettingForm.Panel_API_URL();
            this.panel_SQLContent8 = new ServerSettingForm.Panel_SQLContent();
            this.SuspendLayout();
            // 
            // button_讀取
            // 
            this.button_讀取.Location = new System.Drawing.Point(1691, 7);
            this.button_讀取.Name = "button_讀取";
            this.button_讀取.Size = new System.Drawing.Size(89, 44);
            this.button_讀取.TabIndex = 41;
            this.button_讀取.Text = "讀取";
            this.button_讀取.UseVisualStyleBackColor = true;
            // 
            // button_刪除
            // 
            this.button_刪除.Location = new System.Drawing.Point(1596, 7);
            this.button_刪除.Name = "button_刪除";
            this.button_刪除.Size = new System.Drawing.Size(89, 44);
            this.button_刪除.TabIndex = 40;
            this.button_刪除.Text = "刪除";
            this.button_刪除.UseVisualStyleBackColor = true;
            // 
            // button_新增
            // 
            this.button_新增.Location = new System.Drawing.Point(1406, 7);
            this.button_新增.Name = "button_新增";
            this.button_新增.Size = new System.Drawing.Size(89, 44);
            this.button_新增.TabIndex = 39;
            this.button_新增.Text = "新增";
            this.button_新增.UseVisualStyleBackColor = true;
            // 
            // button_上傳
            // 
            this.button_上傳.Location = new System.Drawing.Point(1501, 7);
            this.button_上傳.Name = "button_上傳";
            this.button_上傳.Size = new System.Drawing.Size(89, 44);
            this.button_上傳.TabIndex = 38;
            this.button_上傳.Text = "上傳";
            this.button_上傳.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(513, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 27);
            this.label4.TabIndex = 37;
            this.label4.Text = "名稱 :";
            // 
            // comboBox_名稱
            // 
            this.comboBox_名稱.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_名稱.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_名稱.FormattingEnabled = true;
            this.comboBox_名稱.Location = new System.Drawing.Point(583, 16);
            this.comboBox_名稱.Name = "comboBox_名稱";
            this.comboBox_名稱.Size = new System.Drawing.Size(168, 32);
            this.comboBox_名稱.TabIndex = 36;
            // 
            // button_測試
            // 
            this.button_測試.Location = new System.Drawing.Point(435, 12);
            this.button_測試.Name = "button_測試";
            this.button_測試.Size = new System.Drawing.Size(72, 41);
            this.button_測試.TabIndex = 35;
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
            this.rJ_TextBox_API_Server.Location = new System.Drawing.Point(136, 12);
            this.rJ_TextBox_API_Server.Multiline = false;
            this.rJ_TextBox_API_Server.Name = "rJ_TextBox_API_Server";
            this.rJ_TextBox_API_Server.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API_Server.PassWordChar = false;
            this.rJ_TextBox_API_Server.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API_Server.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API_Server.ShowTouchPannel = false;
            this.rJ_TextBox_API_Server.Size = new System.Drawing.Size(293, 40);
            this.rJ_TextBox_API_Server.TabIndex = 34;
            this.rJ_TextBox_API_Server.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API_Server.Texts = "";
            this.rJ_TextBox_API_Server.UnderlineStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 27);
            this.label1.TabIndex = 33;
            this.label1.Text = "API Server :";
            // 
            // panel_SQLContent1
            // 
            this.panel_SQLContent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SQLContent1.Content = "一般資料";
            this.panel_SQLContent1.Location = new System.Drawing.Point(18, 67);
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
            serverSettingClass1.類別 = "中藥調劑系統";
            this.panel_SQLContent1.ServerSetting = serverSettingClass1;
            this.panel_SQLContent1.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中藥調劑系統;
            this.panel_SQLContent1.Size = new System.Drawing.Size(175, 309);
            this.panel_SQLContent1.TabIndex = 74;
            // 
            // panel_API_URL5
            // 
            this.panel_API_URL5.Content = "Med_API";
            this.panel_API_URL5.Location = new System.Drawing.Point(18, 537);
            this.panel_API_URL5.Name = "panel_API_URL5";
            serverSettingClass2.DBName = null;
            serverSettingClass2.GUID = null;
            serverSettingClass2.Password = null;
            serverSettingClass2.Port = null;
            serverSettingClass2.Server = "";
            serverSettingClass2.TableName = null;
            serverSettingClass2.User = null;
            serverSettingClass2.Value = null;
            serverSettingClass2.內容 = "Med_API";
            serverSettingClass2.單位 = null;
            serverSettingClass2.程式類別 = null;
            serverSettingClass2.設備名稱 = "";
            serverSettingClass2.類別 = "中藥調劑系統";
            this.panel_API_URL5.ServerSetting = serverSettingClass2;
            this.panel_API_URL5.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中藥調劑系統;
            this.panel_API_URL5.Size = new System.Drawing.Size(864, 36);
            this.panel_API_URL5.TabIndex = 73;
            // 
            // panel_API_URL4
            // 
            this.panel_API_URL4.Content = "Order_API";
            this.panel_API_URL4.Location = new System.Drawing.Point(18, 495);
            this.panel_API_URL4.Name = "panel_API_URL4";
            serverSettingClass3.DBName = null;
            serverSettingClass3.GUID = null;
            serverSettingClass3.Password = null;
            serverSettingClass3.Port = null;
            serverSettingClass3.Server = "";
            serverSettingClass3.TableName = null;
            serverSettingClass3.User = null;
            serverSettingClass3.Value = null;
            serverSettingClass3.內容 = "Order_API";
            serverSettingClass3.單位 = null;
            serverSettingClass3.程式類別 = null;
            serverSettingClass3.設備名稱 = "";
            serverSettingClass3.類別 = "中藥調劑系統";
            this.panel_API_URL4.ServerSetting = serverSettingClass3;
            this.panel_API_URL4.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中藥調劑系統;
            this.panel_API_URL4.Size = new System.Drawing.Size(864, 36);
            this.panel_API_URL4.TabIndex = 72;
            // 
            // panel_API_URL2
            // 
            this.panel_API_URL2.Content = "API02";
            this.panel_API_URL2.Location = new System.Drawing.Point(18, 453);
            this.panel_API_URL2.Name = "panel_API_URL2";
            serverSettingClass4.DBName = null;
            serverSettingClass4.GUID = null;
            serverSettingClass4.Password = null;
            serverSettingClass4.Port = null;
            serverSettingClass4.Server = "";
            serverSettingClass4.TableName = null;
            serverSettingClass4.User = null;
            serverSettingClass4.Value = null;
            serverSettingClass4.內容 = "API02";
            serverSettingClass4.單位 = null;
            serverSettingClass4.程式類別 = null;
            serverSettingClass4.設備名稱 = "";
            serverSettingClass4.類別 = "中藥調劑系統";
            this.panel_API_URL2.ServerSetting = serverSettingClass4;
            this.panel_API_URL2.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中藥調劑系統;
            this.panel_API_URL2.Size = new System.Drawing.Size(864, 36);
            this.panel_API_URL2.TabIndex = 71;
            // 
            // panel_API_URL1
            // 
            this.panel_API_URL1.Content = "API01";
            this.panel_API_URL1.Location = new System.Drawing.Point(18, 411);
            this.panel_API_URL1.Name = "panel_API_URL1";
            serverSettingClass5.DBName = null;
            serverSettingClass5.GUID = null;
            serverSettingClass5.Password = null;
            serverSettingClass5.Port = null;
            serverSettingClass5.Server = "";
            serverSettingClass5.TableName = null;
            serverSettingClass5.User = null;
            serverSettingClass5.Value = null;
            serverSettingClass5.內容 = "API01";
            serverSettingClass5.單位 = null;
            serverSettingClass5.程式類別 = null;
            serverSettingClass5.設備名稱 = "";
            serverSettingClass5.類別 = "中藥調劑系統";
            this.panel_API_URL1.ServerSetting = serverSettingClass5;
            this.panel_API_URL1.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中藥調劑系統;
            this.panel_API_URL1.Size = new System.Drawing.Size(864, 36);
            this.panel_API_URL1.TabIndex = 70;
            // 
            // panel_SQLContent8
            // 
            this.panel_SQLContent8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SQLContent8.Content = "VM端";
            this.panel_SQLContent8.Location = new System.Drawing.Point(199, 67);
            this.panel_SQLContent8.Name = "panel_SQLContent8";
            serverSettingClass6.DBName = "";
            serverSettingClass6.GUID = null;
            serverSettingClass6.Password = "";
            serverSettingClass6.Port = "";
            serverSettingClass6.Server = "";
            serverSettingClass6.TableName = null;
            serverSettingClass6.User = "";
            serverSettingClass6.Value = null;
            serverSettingClass6.內容 = "VM端";
            serverSettingClass6.單位 = null;
            serverSettingClass6.程式類別 = null;
            serverSettingClass6.設備名稱 = "";
            serverSettingClass6.類別 = "中藥調劑系統";
            this.panel_SQLContent8.ServerSetting = serverSettingClass6;
            this.panel_SQLContent8.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中藥調劑系統;
            this.panel_SQLContent8.Size = new System.Drawing.Size(175, 309);
            this.panel_SQLContent8.TabIndex = 66;
            // 
            // Dialog_中藥調劑系統
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1801, 970);
            this.Controls.Add(this.panel_SQLContent1);
            this.Controls.Add(this.panel_API_URL5);
            this.Controls.Add(this.panel_API_URL4);
            this.Controls.Add(this.panel_API_URL2);
            this.Controls.Add(this.panel_API_URL1);
            this.Controls.Add(this.panel_SQLContent8);
            this.Controls.Add(this.button_讀取);
            this.Controls.Add(this.button_刪除);
            this.Controls.Add(this.button_新增);
            this.Controls.Add(this.button_上傳);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox_名稱);
            this.Controls.Add(this.button_測試);
            this.Controls.Add(this.rJ_TextBox_API_Server);
            this.Controls.Add(this.label1);
            this.Name = "Dialog_中藥調劑系統";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中藥調劑系統";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_讀取;
        private System.Windows.Forms.Button button_刪除;
        private System.Windows.Forms.Button button_新增;
        private System.Windows.Forms.Button button_上傳;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_名稱;
        private System.Windows.Forms.Button button_測試;
        private MyUI.RJ_TextBox rJ_TextBox_API_Server;
        private System.Windows.Forms.Label label1;
        private Panel_SQLContent panel_SQLContent8;
        private Panel_API_URL panel_API_URL5;
        private Panel_API_URL panel_API_URL4;
        private Panel_API_URL panel_API_URL2;
        private Panel_API_URL panel_API_URL1;
        private Panel_SQLContent panel_SQLContent1;
    }
}