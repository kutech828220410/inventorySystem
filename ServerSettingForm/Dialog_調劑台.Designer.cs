
namespace ServerSettingForm
{
    partial class Dialog_調劑台
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
            this.label1 = new System.Windows.Forms.Label();
            this.rJ_TextBox_API_Server = new MyUI.RJ_TextBox();
            this.button_測試 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rJ_TextBox_local_Server = new MyUI.RJ_TextBox();
            this.rJ_TextBox_local_Port = new MyUI.RJ_TextBox();
            this.rJ_TextBox_local_DBName = new MyUI.RJ_TextBox();
            this.rJ_TextBox_local_UserName = new MyUI.RJ_TextBox();
            this.rJ_TextBox_local_Password = new MyUI.RJ_TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rJ_TextBox_VM_Password = new MyUI.RJ_TextBox();
            this.rJ_TextBox_VM_UserName = new MyUI.RJ_TextBox();
            this.rJ_TextBox_VM_DBName = new MyUI.RJ_TextBox();
            this.rJ_TextBox_VM_Port = new MyUI.RJ_TextBox();
            this.rJ_TextBox_VM_Server = new MyUI.RJ_TextBox();
            this.comboBox_名稱 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_上傳 = new System.Windows.Forms.Button();
            this.button_新增 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rJ_TextBox_API01 = new MyUI.RJ_TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rJ_TextBox_API02 = new MyUI.RJ_TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rJ_TextBox_Website = new MyUI.RJ_TextBox();
            this.button_刪除 = new System.Windows.Forms.Button();
            this.button_讀取 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 25);
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
            this.rJ_TextBox_API_Server.Location = new System.Drawing.Point(135, 18);
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
            this.button_測試.Location = new System.Drawing.Point(434, 18);
            this.button_測試.Name = "button_測試";
            this.button_測試.Size = new System.Drawing.Size(72, 41);
            this.button_測試.TabIndex = 3;
            this.button_測試.Text = "測試";
            this.button_測試.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rJ_TextBox_local_Password);
            this.groupBox1.Controls.Add(this.rJ_TextBox_local_UserName);
            this.groupBox1.Controls.Add(this.rJ_TextBox_local_DBName);
            this.groupBox1.Controls.Add(this.rJ_TextBox_local_Port);
            this.groupBox1.Controls.Add(this.rJ_TextBox_local_Server);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(36, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 333);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "本地端";
            // 
            // rJ_TextBox_local_Server
            // 
            this.rJ_TextBox_local_Server.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_local_Server.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_local_Server.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_local_Server.BorderRadius = 0;
            this.rJ_TextBox_local_Server.BorderSize = 2;
            this.rJ_TextBox_local_Server.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_local_Server.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_local_Server.Location = new System.Drawing.Point(41, 85);
            this.rJ_TextBox_local_Server.Multiline = false;
            this.rJ_TextBox_local_Server.Name = "rJ_TextBox_local_Server";
            this.rJ_TextBox_local_Server.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_local_Server.PassWordChar = false;
            this.rJ_TextBox_local_Server.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_local_Server.PlaceholderText = "Server";
            this.rJ_TextBox_local_Server.ShowTouchPannel = false;
            this.rJ_TextBox_local_Server.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_local_Server.TabIndex = 4;
            this.rJ_TextBox_local_Server.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_local_Server.Texts = "";
            this.rJ_TextBox_local_Server.UnderlineStyle = false;
            // 
            // rJ_TextBox_local_Port
            // 
            this.rJ_TextBox_local_Port.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_local_Port.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_local_Port.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_local_Port.BorderRadius = 0;
            this.rJ_TextBox_local_Port.BorderSize = 2;
            this.rJ_TextBox_local_Port.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_local_Port.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_local_Port.Location = new System.Drawing.Point(41, 131);
            this.rJ_TextBox_local_Port.Multiline = false;
            this.rJ_TextBox_local_Port.Name = "rJ_TextBox_local_Port";
            this.rJ_TextBox_local_Port.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_local_Port.PassWordChar = false;
            this.rJ_TextBox_local_Port.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_local_Port.PlaceholderText = "Port";
            this.rJ_TextBox_local_Port.ShowTouchPannel = false;
            this.rJ_TextBox_local_Port.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_local_Port.TabIndex = 6;
            this.rJ_TextBox_local_Port.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_local_Port.Texts = "";
            this.rJ_TextBox_local_Port.UnderlineStyle = false;
            // 
            // rJ_TextBox_local_DBName
            // 
            this.rJ_TextBox_local_DBName.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_local_DBName.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_local_DBName.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_local_DBName.BorderRadius = 0;
            this.rJ_TextBox_local_DBName.BorderSize = 2;
            this.rJ_TextBox_local_DBName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_local_DBName.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_local_DBName.Location = new System.Drawing.Point(41, 177);
            this.rJ_TextBox_local_DBName.Multiline = false;
            this.rJ_TextBox_local_DBName.Name = "rJ_TextBox_local_DBName";
            this.rJ_TextBox_local_DBName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_local_DBName.PassWordChar = false;
            this.rJ_TextBox_local_DBName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_local_DBName.PlaceholderText = "DBName";
            this.rJ_TextBox_local_DBName.ShowTouchPannel = false;
            this.rJ_TextBox_local_DBName.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_local_DBName.TabIndex = 8;
            this.rJ_TextBox_local_DBName.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_local_DBName.Texts = "";
            this.rJ_TextBox_local_DBName.UnderlineStyle = false;
            // 
            // rJ_TextBox_local_UserName
            // 
            this.rJ_TextBox_local_UserName.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_local_UserName.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_local_UserName.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_local_UserName.BorderRadius = 0;
            this.rJ_TextBox_local_UserName.BorderSize = 2;
            this.rJ_TextBox_local_UserName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_local_UserName.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_local_UserName.Location = new System.Drawing.Point(41, 223);
            this.rJ_TextBox_local_UserName.Multiline = false;
            this.rJ_TextBox_local_UserName.Name = "rJ_TextBox_local_UserName";
            this.rJ_TextBox_local_UserName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_local_UserName.PassWordChar = false;
            this.rJ_TextBox_local_UserName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_local_UserName.PlaceholderText = "UserName";
            this.rJ_TextBox_local_UserName.ShowTouchPannel = false;
            this.rJ_TextBox_local_UserName.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_local_UserName.TabIndex = 10;
            this.rJ_TextBox_local_UserName.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_local_UserName.Texts = "";
            this.rJ_TextBox_local_UserName.UnderlineStyle = false;
            // 
            // rJ_TextBox_local_Password
            // 
            this.rJ_TextBox_local_Password.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_local_Password.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_local_Password.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_local_Password.BorderRadius = 0;
            this.rJ_TextBox_local_Password.BorderSize = 2;
            this.rJ_TextBox_local_Password.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_local_Password.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_local_Password.Location = new System.Drawing.Point(41, 269);
            this.rJ_TextBox_local_Password.Multiline = false;
            this.rJ_TextBox_local_Password.Name = "rJ_TextBox_local_Password";
            this.rJ_TextBox_local_Password.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_local_Password.PassWordChar = false;
            this.rJ_TextBox_local_Password.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_local_Password.PlaceholderText = "Password";
            this.rJ_TextBox_local_Password.ShowTouchPannel = false;
            this.rJ_TextBox_local_Password.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_local_Password.TabIndex = 12;
            this.rJ_TextBox_local_Password.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_local_Password.Texts = "";
            this.rJ_TextBox_local_Password.UnderlineStyle = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(41, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 40);
            this.label2.TabIndex = 13;
            this.label2.Text = "SQL Server";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.rJ_TextBox_VM_Password);
            this.groupBox2.Controls.Add(this.rJ_TextBox_VM_UserName);
            this.groupBox2.Controls.Add(this.rJ_TextBox_VM_DBName);
            this.groupBox2.Controls.Add(this.rJ_TextBox_VM_Port);
            this.groupBox2.Controls.Add(this.rJ_TextBox_VM_Server);
            this.groupBox2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(317, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 333);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VM端";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(41, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(209, 40);
            this.label5.TabIndex = 13;
            this.label5.Text = "SQL Server";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rJ_TextBox_VM_Password
            // 
            this.rJ_TextBox_VM_Password.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_VM_Password.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_VM_Password.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_VM_Password.BorderRadius = 0;
            this.rJ_TextBox_VM_Password.BorderSize = 2;
            this.rJ_TextBox_VM_Password.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_VM_Password.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_VM_Password.Location = new System.Drawing.Point(41, 269);
            this.rJ_TextBox_VM_Password.Multiline = false;
            this.rJ_TextBox_VM_Password.Name = "rJ_TextBox_VM_Password";
            this.rJ_TextBox_VM_Password.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_VM_Password.PassWordChar = false;
            this.rJ_TextBox_VM_Password.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_VM_Password.PlaceholderText = "Password";
            this.rJ_TextBox_VM_Password.ShowTouchPannel = false;
            this.rJ_TextBox_VM_Password.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_VM_Password.TabIndex = 12;
            this.rJ_TextBox_VM_Password.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_VM_Password.Texts = "";
            this.rJ_TextBox_VM_Password.UnderlineStyle = false;
            // 
            // rJ_TextBox_VM_UserName
            // 
            this.rJ_TextBox_VM_UserName.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_VM_UserName.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_VM_UserName.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_VM_UserName.BorderRadius = 0;
            this.rJ_TextBox_VM_UserName.BorderSize = 2;
            this.rJ_TextBox_VM_UserName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_VM_UserName.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_VM_UserName.Location = new System.Drawing.Point(41, 223);
            this.rJ_TextBox_VM_UserName.Multiline = false;
            this.rJ_TextBox_VM_UserName.Name = "rJ_TextBox_VM_UserName";
            this.rJ_TextBox_VM_UserName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_VM_UserName.PassWordChar = false;
            this.rJ_TextBox_VM_UserName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_VM_UserName.PlaceholderText = "UserName";
            this.rJ_TextBox_VM_UserName.ShowTouchPannel = false;
            this.rJ_TextBox_VM_UserName.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_VM_UserName.TabIndex = 10;
            this.rJ_TextBox_VM_UserName.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_VM_UserName.Texts = "";
            this.rJ_TextBox_VM_UserName.UnderlineStyle = false;
            // 
            // rJ_TextBox_VM_DBName
            // 
            this.rJ_TextBox_VM_DBName.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_VM_DBName.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_VM_DBName.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_VM_DBName.BorderRadius = 0;
            this.rJ_TextBox_VM_DBName.BorderSize = 2;
            this.rJ_TextBox_VM_DBName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_VM_DBName.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_VM_DBName.Location = new System.Drawing.Point(41, 177);
            this.rJ_TextBox_VM_DBName.Multiline = false;
            this.rJ_TextBox_VM_DBName.Name = "rJ_TextBox_VM_DBName";
            this.rJ_TextBox_VM_DBName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_VM_DBName.PassWordChar = false;
            this.rJ_TextBox_VM_DBName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_VM_DBName.PlaceholderText = "DBName";
            this.rJ_TextBox_VM_DBName.ShowTouchPannel = false;
            this.rJ_TextBox_VM_DBName.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_VM_DBName.TabIndex = 8;
            this.rJ_TextBox_VM_DBName.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_VM_DBName.Texts = "";
            this.rJ_TextBox_VM_DBName.UnderlineStyle = false;
            // 
            // rJ_TextBox_VM_Port
            // 
            this.rJ_TextBox_VM_Port.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_VM_Port.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_VM_Port.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_VM_Port.BorderRadius = 0;
            this.rJ_TextBox_VM_Port.BorderSize = 2;
            this.rJ_TextBox_VM_Port.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_VM_Port.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_VM_Port.Location = new System.Drawing.Point(41, 131);
            this.rJ_TextBox_VM_Port.Multiline = false;
            this.rJ_TextBox_VM_Port.Name = "rJ_TextBox_VM_Port";
            this.rJ_TextBox_VM_Port.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_VM_Port.PassWordChar = false;
            this.rJ_TextBox_VM_Port.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_VM_Port.PlaceholderText = "Port";
            this.rJ_TextBox_VM_Port.ShowTouchPannel = false;
            this.rJ_TextBox_VM_Port.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_VM_Port.TabIndex = 6;
            this.rJ_TextBox_VM_Port.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_VM_Port.Texts = "";
            this.rJ_TextBox_VM_Port.UnderlineStyle = false;
            // 
            // rJ_TextBox_VM_Server
            // 
            this.rJ_TextBox_VM_Server.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_VM_Server.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_VM_Server.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_VM_Server.BorderRadius = 0;
            this.rJ_TextBox_VM_Server.BorderSize = 2;
            this.rJ_TextBox_VM_Server.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_VM_Server.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_VM_Server.Location = new System.Drawing.Point(41, 85);
            this.rJ_TextBox_VM_Server.Multiline = false;
            this.rJ_TextBox_VM_Server.Name = "rJ_TextBox_VM_Server";
            this.rJ_TextBox_VM_Server.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_VM_Server.PassWordChar = false;
            this.rJ_TextBox_VM_Server.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_VM_Server.PlaceholderText = "Server";
            this.rJ_TextBox_VM_Server.ShowTouchPannel = false;
            this.rJ_TextBox_VM_Server.Size = new System.Drawing.Size(209, 40);
            this.rJ_TextBox_VM_Server.TabIndex = 4;
            this.rJ_TextBox_VM_Server.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_VM_Server.Texts = "";
            this.rJ_TextBox_VM_Server.UnderlineStyle = false;
            // 
            // comboBox_名稱
            // 
            this.comboBox_名稱.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_名稱.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_名稱.FormattingEnabled = true;
            this.comboBox_名稱.Location = new System.Drawing.Point(582, 22);
            this.comboBox_名稱.Name = "comboBox_名稱";
            this.comboBox_名稱.Size = new System.Drawing.Size(168, 32);
            this.comboBox_名稱.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(512, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 27);
            this.label4.TabIndex = 7;
            this.label4.Text = "名稱 :";
            // 
            // button_上傳
            // 
            this.button_上傳.Location = new System.Drawing.Point(944, 66);
            this.button_上傳.Name = "button_上傳";
            this.button_上傳.Size = new System.Drawing.Size(75, 44);
            this.button_上傳.TabIndex = 8;
            this.button_上傳.Text = "上傳";
            this.button_上傳.UseVisualStyleBackColor = true;
            // 
            // button_新增
            // 
            this.button_新增.Location = new System.Drawing.Point(944, 16);
            this.button_新增.Name = "button_新增";
            this.button_新增.Size = new System.Drawing.Size(75, 44);
            this.button_新增.TabIndex = 9;
            this.button_新增.Text = "新增";
            this.button_新增.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(625, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 40);
            this.label3.TabIndex = 17;
            this.label3.Text = "API 01";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rJ_TextBox_API01
            // 
            this.rJ_TextBox_API01.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_API01.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_API01.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_API01.BorderRadius = 0;
            this.rJ_TextBox_API01.BorderSize = 2;
            this.rJ_TextBox_API01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_API01.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_API01.Location = new System.Drawing.Point(625, 149);
            this.rJ_TextBox_API01.Multiline = false;
            this.rJ_TextBox_API01.Name = "rJ_TextBox_API01";
            this.rJ_TextBox_API01.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API01.PassWordChar = false;
            this.rJ_TextBox_API01.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API01.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API01.ShowTouchPannel = false;
            this.rJ_TextBox_API01.Size = new System.Drawing.Size(303, 40);
            this.rJ_TextBox_API01.TabIndex = 16;
            this.rJ_TextBox_API01.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API01.Texts = "";
            this.rJ_TextBox_API01.UnderlineStyle = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(625, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(303, 40);
            this.label6.TabIndex = 19;
            this.label6.Text = "API 02";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rJ_TextBox_API02
            // 
            this.rJ_TextBox_API02.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_API02.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_API02.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_API02.BorderRadius = 0;
            this.rJ_TextBox_API02.BorderSize = 2;
            this.rJ_TextBox_API02.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_API02.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_API02.Location = new System.Drawing.Point(625, 239);
            this.rJ_TextBox_API02.Multiline = false;
            this.rJ_TextBox_API02.Name = "rJ_TextBox_API02";
            this.rJ_TextBox_API02.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API02.PassWordChar = false;
            this.rJ_TextBox_API02.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API02.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API02.ShowTouchPannel = false;
            this.rJ_TextBox_API02.Size = new System.Drawing.Size(303, 40);
            this.rJ_TextBox_API02.TabIndex = 18;
            this.rJ_TextBox_API02.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API02.Texts = "";
            this.rJ_TextBox_API02.UnderlineStyle = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(625, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(303, 40);
            this.label7.TabIndex = 21;
            this.label7.Text = "Website";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rJ_TextBox_Website
            // 
            this.rJ_TextBox_Website.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_Website.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_Website.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_Website.BorderRadius = 0;
            this.rJ_TextBox_Website.BorderSize = 2;
            this.rJ_TextBox_Website.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_Website.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox_Website.Location = new System.Drawing.Point(625, 329);
            this.rJ_TextBox_Website.Multiline = false;
            this.rJ_TextBox_Website.Name = "rJ_TextBox_Website";
            this.rJ_TextBox_Website.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_Website.PassWordChar = false;
            this.rJ_TextBox_Website.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_Website.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_Website.ShowTouchPannel = false;
            this.rJ_TextBox_Website.Size = new System.Drawing.Size(303, 40);
            this.rJ_TextBox_Website.TabIndex = 20;
            this.rJ_TextBox_Website.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_Website.Texts = "";
            this.rJ_TextBox_Website.UnderlineStyle = false;
            // 
            // button_刪除
            // 
            this.button_刪除.Location = new System.Drawing.Point(944, 117);
            this.button_刪除.Name = "button_刪除";
            this.button_刪除.Size = new System.Drawing.Size(75, 44);
            this.button_刪除.TabIndex = 22;
            this.button_刪除.Text = "刪除";
            this.button_刪除.UseVisualStyleBackColor = true;
            // 
            // button_讀取
            // 
            this.button_讀取.Location = new System.Drawing.Point(944, 167);
            this.button_讀取.Name = "button_讀取";
            this.button_讀取.Size = new System.Drawing.Size(75, 44);
            this.button_讀取.TabIndex = 23;
            this.button_讀取.Text = "讀取";
            this.button_讀取.UseVisualStyleBackColor = true;
            // 
            // Dialog_調劑台
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 429);
            this.Controls.Add(this.button_讀取);
            this.Controls.Add(this.button_刪除);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rJ_TextBox_Website);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rJ_TextBox_API02);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rJ_TextBox_API01);
            this.Controls.Add(this.button_新增);
            this.Controls.Add(this.button_上傳);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox_名稱);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_測試);
            this.Controls.Add(this.rJ_TextBox_API_Server);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_調劑台";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Dialog_調劑台_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MyUI.RJ_TextBox rJ_TextBox_API_Server;
        private System.Windows.Forms.Button button_測試;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private MyUI.RJ_TextBox rJ_TextBox_local_Password;
        private MyUI.RJ_TextBox rJ_TextBox_local_UserName;
        private MyUI.RJ_TextBox rJ_TextBox_local_DBName;
        private MyUI.RJ_TextBox rJ_TextBox_local_Port;
        private MyUI.RJ_TextBox rJ_TextBox_local_Server;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private MyUI.RJ_TextBox rJ_TextBox_VM_Password;
        private MyUI.RJ_TextBox rJ_TextBox_VM_UserName;
        private MyUI.RJ_TextBox rJ_TextBox_VM_DBName;
        private MyUI.RJ_TextBox rJ_TextBox_VM_Port;
        private MyUI.RJ_TextBox rJ_TextBox_VM_Server;
        private System.Windows.Forms.ComboBox comboBox_名稱;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_上傳;
        private System.Windows.Forms.Button button_新增;
        private System.Windows.Forms.Label label3;
        private MyUI.RJ_TextBox rJ_TextBox_API01;
        private System.Windows.Forms.Label label6;
        private MyUI.RJ_TextBox rJ_TextBox_API02;
        private System.Windows.Forms.Label label7;
        private MyUI.RJ_TextBox rJ_TextBox_Website;
        private System.Windows.Forms.Button button_刪除;
        private System.Windows.Forms.Button button_讀取;
    }
}