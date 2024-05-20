
namespace ServerSettingForm
{
    partial class Dialog_設定
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
            this.button_上傳 = new System.Windows.Forms.Button();
            this.button_測試 = new System.Windows.Forms.Button();
            this.rJ_TextBox_API_Server = new MyUI.RJ_TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_CheckBox6 = new ServerSettingForm.Panel_CheckBox();
            this.panel_CheckBox5 = new ServerSettingForm.Panel_CheckBox();
            this.panel_CheckBox4 = new ServerSettingForm.Panel_CheckBox();
            this.panel_CheckBox3 = new ServerSettingForm.Panel_CheckBox();
            this.panel_CheckBox2 = new ServerSettingForm.Panel_CheckBox();
            this.panel_CheckBox1 = new ServerSettingForm.Panel_CheckBox();
            this.SuspendLayout();
            // 
            // button_讀取
            // 
            this.button_讀取.Location = new System.Drawing.Point(785, 12);
            this.button_讀取.Name = "button_讀取";
            this.button_讀取.Size = new System.Drawing.Size(89, 44);
            this.button_讀取.TabIndex = 61;
            this.button_讀取.Text = "讀取";
            this.button_讀取.UseVisualStyleBackColor = true;
            // 
            // button_上傳
            // 
            this.button_上傳.Location = new System.Drawing.Point(690, 12);
            this.button_上傳.Name = "button_上傳";
            this.button_上傳.Size = new System.Drawing.Size(89, 44);
            this.button_上傳.TabIndex = 60;
            this.button_上傳.Text = "上傳";
            this.button_上傳.UseVisualStyleBackColor = true;
            // 
            // button_測試
            // 
            this.button_測試.Location = new System.Drawing.Point(442, 10);
            this.button_測試.Name = "button_測試";
            this.button_測試.Size = new System.Drawing.Size(72, 41);
            this.button_測試.TabIndex = 100;
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
            this.rJ_TextBox_API_Server.Location = new System.Drawing.Point(143, 10);
            this.rJ_TextBox_API_Server.Multiline = false;
            this.rJ_TextBox_API_Server.Name = "rJ_TextBox_API_Server";
            this.rJ_TextBox_API_Server.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API_Server.PassWordChar = false;
            this.rJ_TextBox_API_Server.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API_Server.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API_Server.ShowTouchPannel = false;
            this.rJ_TextBox_API_Server.Size = new System.Drawing.Size(293, 40);
            this.rJ_TextBox_API_Server.TabIndex = 99;
            this.rJ_TextBox_API_Server.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API_Server.Texts = "";
            this.rJ_TextBox_API_Server.UnderlineStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 27);
            this.label1.TabIndex = 98;
            this.label1.Text = "API Server :";
            // 
            // panel_CheckBox6
            // 
            this.panel_CheckBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CheckBox6.Content = "智慧藥局整合平台不顯示";
            this.panel_CheckBox6.Location = new System.Drawing.Point(25, 380);
            this.panel_CheckBox6.Name = "panel_CheckBox6";
            serverSettingClass1.DBName = null;
            serverSettingClass1.GUID = null;
            serverSettingClass1.Password = null;
            serverSettingClass1.Port = null;
            serverSettingClass1.Server = null;
            serverSettingClass1.TableName = null;
            serverSettingClass1.User = null;
            serverSettingClass1.Value = "False";
            serverSettingClass1.內容 = "智慧藥局整合平台不顯示";
            serverSettingClass1.單位 = null;
            serverSettingClass1.程式類別 = null;
            serverSettingClass1.設備名稱 = "";
            serverSettingClass1.類別 = "中藥調劑系統";
            this.panel_CheckBox6.ServerSetting = serverSettingClass1;
            this.panel_CheckBox6.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中藥調劑系統;
            this.panel_CheckBox6.Size = new System.Drawing.Size(246, 54);
            this.panel_CheckBox6.TabIndex = 97;
            // 
            // panel_CheckBox5
            // 
            this.panel_CheckBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CheckBox5.Content = "智慧藥局整合平台不顯示";
            this.panel_CheckBox5.Location = new System.Drawing.Point(25, 320);
            this.panel_CheckBox5.Name = "panel_CheckBox5";
            serverSettingClass2.DBName = null;
            serverSettingClass2.GUID = null;
            serverSettingClass2.Password = null;
            serverSettingClass2.Port = null;
            serverSettingClass2.Server = null;
            serverSettingClass2.TableName = null;
            serverSettingClass2.User = null;
            serverSettingClass2.Value = "False";
            serverSettingClass2.內容 = "智慧藥局整合平台不顯示";
            serverSettingClass2.單位 = null;
            serverSettingClass2.程式類別 = null;
            serverSettingClass2.設備名稱 = "";
            serverSettingClass2.類別 = "中心叫號系統";
            this.panel_CheckBox5.ServerSetting = serverSettingClass2;
            this.panel_CheckBox5.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.中心叫號系統;
            this.panel_CheckBox5.Size = new System.Drawing.Size(246, 54);
            this.panel_CheckBox5.TabIndex = 96;
            // 
            // panel_CheckBox4
            // 
            this.panel_CheckBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CheckBox4.Content = "智慧藥局整合平台不顯示";
            this.panel_CheckBox4.Location = new System.Drawing.Point(25, 260);
            this.panel_CheckBox4.Name = "panel_CheckBox4";
            serverSettingClass3.DBName = null;
            serverSettingClass3.GUID = null;
            serverSettingClass3.Password = null;
            serverSettingClass3.Port = null;
            serverSettingClass3.Server = null;
            serverSettingClass3.TableName = null;
            serverSettingClass3.User = null;
            serverSettingClass3.Value = "False";
            serverSettingClass3.內容 = "智慧藥局整合平台不顯示";
            serverSettingClass3.單位 = null;
            serverSettingClass3.程式類別 = null;
            serverSettingClass3.設備名稱 = "";
            serverSettingClass3.類別 = "癌症備藥機";
            this.panel_CheckBox4.ServerSetting = serverSettingClass3;
            this.panel_CheckBox4.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.癌症備藥機;
            this.panel_CheckBox4.Size = new System.Drawing.Size(246, 54);
            this.panel_CheckBox4.TabIndex = 95;
            // 
            // panel_CheckBox3
            // 
            this.panel_CheckBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CheckBox3.Content = "智慧藥局整合平台不顯示";
            this.panel_CheckBox3.Location = new System.Drawing.Point(25, 200);
            this.panel_CheckBox3.Name = "panel_CheckBox3";
            serverSettingClass4.DBName = null;
            serverSettingClass4.GUID = null;
            serverSettingClass4.Password = null;
            serverSettingClass4.Port = null;
            serverSettingClass4.Server = null;
            serverSettingClass4.TableName = null;
            serverSettingClass4.User = null;
            serverSettingClass4.Value = "False";
            serverSettingClass4.內容 = "智慧藥局整合平台不顯示";
            serverSettingClass4.單位 = null;
            serverSettingClass4.程式類別 = null;
            serverSettingClass4.設備名稱 = "";
            serverSettingClass4.類別 = "傳送櫃";
            this.panel_CheckBox3.ServerSetting = serverSettingClass4;
            this.panel_CheckBox3.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.傳送櫃;
            this.panel_CheckBox3.Size = new System.Drawing.Size(246, 54);
            this.panel_CheckBox3.TabIndex = 94;
            // 
            // panel_CheckBox2
            // 
            this.panel_CheckBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CheckBox2.Content = "智慧藥局整合平台不顯示";
            this.panel_CheckBox2.Location = new System.Drawing.Point(25, 140);
            this.panel_CheckBox2.Name = "panel_CheckBox2";
            serverSettingClass5.DBName = null;
            serverSettingClass5.GUID = null;
            serverSettingClass5.Password = null;
            serverSettingClass5.Port = null;
            serverSettingClass5.Server = null;
            serverSettingClass5.TableName = null;
            serverSettingClass5.User = null;
            serverSettingClass5.Value = "False";
            serverSettingClass5.內容 = "智慧藥局整合平台不顯示";
            serverSettingClass5.單位 = null;
            serverSettingClass5.程式類別 = null;
            serverSettingClass5.設備名稱 = "";
            serverSettingClass5.類別 = "藥庫";
            this.panel_CheckBox2.ServerSetting = serverSettingClass5;
            this.panel_CheckBox2.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.藥庫;
            this.panel_CheckBox2.Size = new System.Drawing.Size(246, 54);
            this.panel_CheckBox2.TabIndex = 93;
            // 
            // panel_CheckBox1
            // 
            this.panel_CheckBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CheckBox1.Content = "智慧藥局整合平台不顯示";
            this.panel_CheckBox1.Location = new System.Drawing.Point(25, 80);
            this.panel_CheckBox1.Name = "panel_CheckBox1";
            serverSettingClass6.DBName = null;
            serverSettingClass6.GUID = null;
            serverSettingClass6.Password = null;
            serverSettingClass6.Port = null;
            serverSettingClass6.Server = null;
            serverSettingClass6.TableName = null;
            serverSettingClass6.User = null;
            serverSettingClass6.Value = "False";
            serverSettingClass6.內容 = "智慧藥局整合平台不顯示";
            serverSettingClass6.單位 = null;
            serverSettingClass6.程式類別 = null;
            serverSettingClass6.設備名稱 = "";
            serverSettingClass6.類別 = "調劑台";
            this.panel_CheckBox1.ServerSetting = serverSettingClass6;
            this.panel_CheckBox1.ServerSetting_Type = HIS_DB_Lib.enum_ServerSetting_Type.調劑台;
            this.panel_CheckBox1.Size = new System.Drawing.Size(246, 54);
            this.panel_CheckBox1.TabIndex = 92;
            // 
            // Dialog_設定
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 615);
            this.Controls.Add(this.button_測試);
            this.Controls.Add(this.rJ_TextBox_API_Server);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel_CheckBox6);
            this.Controls.Add(this.panel_CheckBox5);
            this.Controls.Add(this.panel_CheckBox4);
            this.Controls.Add(this.panel_CheckBox3);
            this.Controls.Add(this.panel_CheckBox2);
            this.Controls.Add(this.panel_CheckBox1);
            this.Controls.Add(this.button_讀取);
            this.Controls.Add(this.button_上傳);
            this.Name = "Dialog_設定";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_讀取;
        private System.Windows.Forms.Button button_上傳;
        private Panel_CheckBox panel_CheckBox1;
        private Panel_CheckBox panel_CheckBox2;
        private Panel_CheckBox panel_CheckBox3;
        private Panel_CheckBox panel_CheckBox4;
        private Panel_CheckBox panel_CheckBox5;
        private Panel_CheckBox panel_CheckBox6;
        private System.Windows.Forms.Button button_測試;
        private MyUI.RJ_TextBox rJ_TextBox_API_Server;
        private System.Windows.Forms.Label label1;
    }
}