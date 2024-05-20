
namespace E_UpdateVersion
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rJ_Button_離開 = new MyUI.RJ_Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.後台設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.label_info = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rJ_Button_智能藥庫系統 = new MyUI.RJ_Button();
            this.rJ_Button_智慧調劑台系統 = new MyUI.RJ_Button();
            this.rJ_Button_中藥調劑系統 = new MyUI.RJ_Button();
            this.rJ_Button_勤務傳送系統 = new MyUI.RJ_Button();
            this.rJ_Button_中心叫號系統 = new MyUI.RJ_Button();
            this.rJ_Button_癌症備藥機 = new MyUI.RJ_Button();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rJ_Button_離開
            // 
            this.rJ_Button_離開.AutoResetState = false;
            this.rJ_Button_離開.BackColor = System.Drawing.Color.White;
            this.rJ_Button_離開.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_離開.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_離開.BorderRadius = 15;
            this.rJ_Button_離開.BorderSize = 0;
            this.rJ_Button_離開.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_離開.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_離開.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Button_離開.FlatAppearance.BorderSize = 0;
            this.rJ_Button_離開.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_離開.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_離開.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_離開.GUID = "";
            this.rJ_Button_離開.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_離開.Location = new System.Drawing.Point(499, 0);
            this.rJ_Button_離開.Name = "rJ_Button_離開";
            this.rJ_Button_離開.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_離開.ProhibitionLineWidth = 4;
            this.rJ_Button_離開.ProhibitionSymbolSize = 30;
            this.rJ_Button_離開.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_離開.ShadowSize = 3;
            this.rJ_Button_離開.ShowLoadingForm = false;
            this.rJ_Button_離開.Size = new System.Drawing.Size(154, 63);
            this.rJ_Button_離開.State = false;
            this.rJ_Button_離開.TabIndex = 1;
            this.rJ_Button_離開.Text = "離開";
            this.rJ_Button_離開.TextColor = System.Drawing.Color.White;
            this.rJ_Button_離開.TextHeight = 0;
            this.rJ_Button_離開.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.ContextMenuStrip = this.contextMenuStrip;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(653, 88);
            this.label1.TabIndex = 4;
            this.label1.Text = "智慧藥局整合平台";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.後台設定ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(123, 26);
            // 
            // 後台設定ToolStripMenuItem
            // 
            this.後台設定ToolStripMenuItem.Name = "後台設定ToolStripMenuItem";
            this.後台設定ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.後台設定ToolStripMenuItem.Text = "後台設定";
            this.後台設定ToolStripMenuItem.Click += new System.EventHandler(this.後台設定ToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(0, 581);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(653, 36);
            this.label2.TabIndex = 23;
            this.label2.Text = "Copyright ©2023 鴻森智能科技有限公司";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_version
            // 
            this.label_version.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_version.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_version.Location = new System.Drawing.Point(0, 567);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(653, 14);
            this.label_version.TabIndex = 24;
            this.label_version.Text = "Ver 0.0.0.0";
            // 
            // label_info
            // 
            this.label_info.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_info.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_info.Location = new System.Drawing.Point(0, 88);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(653, 16);
            this.label_info.TabIndex = 26;
            this.label_info.Text = "XXXXXXXX";
            this.label_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_Button_離開);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 504);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 63);
            this.panel1.TabIndex = 27;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rJ_Button_智能藥庫系統);
            this.flowLayoutPanel1.Controls.Add(this.rJ_Button_智慧調劑台系統);
            this.flowLayoutPanel1.Controls.Add(this.rJ_Button_中藥調劑系統);
            this.flowLayoutPanel1.Controls.Add(this.rJ_Button_勤務傳送系統);
            this.flowLayoutPanel1.Controls.Add(this.rJ_Button_中心叫號系統);
            this.flowLayoutPanel1.Controls.Add(this.rJ_Button_癌症備藥機);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 104);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(653, 400);
            this.flowLayoutPanel1.TabIndex = 31;
            // 
            // rJ_Button_智能藥庫系統
            // 
            this.rJ_Button_智能藥庫系統.AutoResetState = false;
            this.rJ_Button_智能藥庫系統.BackColor = System.Drawing.Color.White;
            this.rJ_Button_智能藥庫系統.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_智能藥庫系統.BackgroundImage = global::E_UpdateVersion.Properties.Resources.智能藥庫系統;
            this.rJ_Button_智能藥庫系統.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_智能藥庫系統.BorderRadius = 12;
            this.rJ_Button_智能藥庫系統.BorderSize = 1;
            this.rJ_Button_智能藥庫系統.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_智能藥庫系統.DisenableColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Button_智能藥庫系統.Enabled = false;
            this.rJ_Button_智能藥庫系統.FlatAppearance.BorderSize = 0;
            this.rJ_Button_智能藥庫系統.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_智能藥庫系統.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_智能藥庫系統.ForeColor = System.Drawing.Color.Black;
            this.rJ_Button_智能藥庫系統.GUID = "";
            this.rJ_Button_智能藥庫系統.Image_padding = new System.Windows.Forms.Padding(16, 10, 22, 0);
            this.rJ_Button_智能藥庫系統.Location = new System.Drawing.Point(33, 3);
            this.rJ_Button_智能藥庫系統.Name = "rJ_Button_智能藥庫系統";
            this.rJ_Button_智能藥庫系統.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_智能藥庫系統.ProhibitionLineWidth = 10;
            this.rJ_Button_智能藥庫系統.ProhibitionSymbolSize = 60;
            this.rJ_Button_智能藥庫系統.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_智能藥庫系統.ShadowSize = 3;
            this.rJ_Button_智能藥庫系統.ShowLoadingForm = false;
            this.rJ_Button_智能藥庫系統.Size = new System.Drawing.Size(191, 189);
            this.rJ_Button_智能藥庫系統.State = false;
            this.rJ_Button_智能藥庫系統.TabIndex = 31;
            this.rJ_Button_智能藥庫系統.Text = "智能藥庫系統";
            this.rJ_Button_智能藥庫系統.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_智能藥庫系統.TextHeight = 40;
            this.rJ_Button_智能藥庫系統.UseVisualStyleBackColor = false;
            this.rJ_Button_智能藥庫系統.Visible = false;
            // 
            // rJ_Button_智慧調劑台系統
            // 
            this.rJ_Button_智慧調劑台系統.AutoResetState = false;
            this.rJ_Button_智慧調劑台系統.BackColor = System.Drawing.Color.White;
            this.rJ_Button_智慧調劑台系統.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_智慧調劑台系統.BackgroundImage = global::E_UpdateVersion.Properties.Resources.智慧調劑台系統;
            this.rJ_Button_智慧調劑台系統.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_智慧調劑台系統.BorderRadius = 12;
            this.rJ_Button_智慧調劑台系統.BorderSize = 1;
            this.rJ_Button_智慧調劑台系統.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_智慧調劑台系統.DisenableColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Button_智慧調劑台系統.Enabled = false;
            this.rJ_Button_智慧調劑台系統.FlatAppearance.BorderSize = 0;
            this.rJ_Button_智慧調劑台系統.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_智慧調劑台系統.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_智慧調劑台系統.ForeColor = System.Drawing.Color.Black;
            this.rJ_Button_智慧調劑台系統.GUID = "";
            this.rJ_Button_智慧調劑台系統.Image_padding = new System.Windows.Forms.Padding(16, 10, 22, 0);
            this.rJ_Button_智慧調劑台系統.Location = new System.Drawing.Point(230, 3);
            this.rJ_Button_智慧調劑台系統.Name = "rJ_Button_智慧調劑台系統";
            this.rJ_Button_智慧調劑台系統.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_智慧調劑台系統.ProhibitionLineWidth = 10;
            this.rJ_Button_智慧調劑台系統.ProhibitionSymbolSize = 60;
            this.rJ_Button_智慧調劑台系統.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_智慧調劑台系統.ShadowSize = 3;
            this.rJ_Button_智慧調劑台系統.ShowLoadingForm = false;
            this.rJ_Button_智慧調劑台系統.Size = new System.Drawing.Size(191, 189);
            this.rJ_Button_智慧調劑台系統.State = false;
            this.rJ_Button_智慧調劑台系統.TabIndex = 30;
            this.rJ_Button_智慧調劑台系統.Text = "智慧調劑台系統";
            this.rJ_Button_智慧調劑台系統.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_智慧調劑台系統.TextHeight = 40;
            this.rJ_Button_智慧調劑台系統.UseVisualStyleBackColor = false;
            this.rJ_Button_智慧調劑台系統.Visible = false;
            // 
            // rJ_Button_中藥調劑系統
            // 
            this.rJ_Button_中藥調劑系統.AutoResetState = false;
            this.rJ_Button_中藥調劑系統.BackColor = System.Drawing.Color.White;
            this.rJ_Button_中藥調劑系統.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_中藥調劑系統.BackgroundImage = global::E_UpdateVersion.Properties.Resources.中藥調劑系統ICON;
            this.rJ_Button_中藥調劑系統.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_中藥調劑系統.BorderRadius = 12;
            this.rJ_Button_中藥調劑系統.BorderSize = 1;
            this.rJ_Button_中藥調劑系統.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_中藥調劑系統.DisenableColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Button_中藥調劑系統.Enabled = false;
            this.rJ_Button_中藥調劑系統.FlatAppearance.BorderSize = 0;
            this.rJ_Button_中藥調劑系統.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_中藥調劑系統.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_中藥調劑系統.ForeColor = System.Drawing.Color.Black;
            this.rJ_Button_中藥調劑系統.GUID = "";
            this.rJ_Button_中藥調劑系統.Image_padding = new System.Windows.Forms.Padding(16, 10, 22, 0);
            this.rJ_Button_中藥調劑系統.Location = new System.Drawing.Point(427, 3);
            this.rJ_Button_中藥調劑系統.Name = "rJ_Button_中藥調劑系統";
            this.rJ_Button_中藥調劑系統.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_中藥調劑系統.ProhibitionLineWidth = 10;
            this.rJ_Button_中藥調劑系統.ProhibitionSymbolSize = 60;
            this.rJ_Button_中藥調劑系統.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_中藥調劑系統.ShadowSize = 3;
            this.rJ_Button_中藥調劑系統.ShowLoadingForm = false;
            this.rJ_Button_中藥調劑系統.Size = new System.Drawing.Size(191, 189);
            this.rJ_Button_中藥調劑系統.State = false;
            this.rJ_Button_中藥調劑系統.TabIndex = 38;
            this.rJ_Button_中藥調劑系統.Text = "中藥調劑系統";
            this.rJ_Button_中藥調劑系統.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_中藥調劑系統.TextHeight = 40;
            this.rJ_Button_中藥調劑系統.UseVisualStyleBackColor = false;
            this.rJ_Button_中藥調劑系統.Visible = false;
            // 
            // rJ_Button_勤務傳送系統
            // 
            this.rJ_Button_勤務傳送系統.AutoResetState = false;
            this.rJ_Button_勤務傳送系統.BackColor = System.Drawing.Color.White;
            this.rJ_Button_勤務傳送系統.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_勤務傳送系統.BackgroundImage = global::E_UpdateVersion.Properties.Resources.勤務傳送櫃;
            this.rJ_Button_勤務傳送系統.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_勤務傳送系統.BorderRadius = 12;
            this.rJ_Button_勤務傳送系統.BorderSize = 1;
            this.rJ_Button_勤務傳送系統.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_勤務傳送系統.DisenableColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Button_勤務傳送系統.Enabled = false;
            this.rJ_Button_勤務傳送系統.FlatAppearance.BorderSize = 0;
            this.rJ_Button_勤務傳送系統.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_勤務傳送系統.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_勤務傳送系統.ForeColor = System.Drawing.Color.Black;
            this.rJ_Button_勤務傳送系統.GUID = "";
            this.rJ_Button_勤務傳送系統.Image_padding = new System.Windows.Forms.Padding(16, 10, 22, 0);
            this.rJ_Button_勤務傳送系統.Location = new System.Drawing.Point(33, 198);
            this.rJ_Button_勤務傳送系統.Name = "rJ_Button_勤務傳送系統";
            this.rJ_Button_勤務傳送系統.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_勤務傳送系統.ProhibitionLineWidth = 10;
            this.rJ_Button_勤務傳送系統.ProhibitionSymbolSize = 60;
            this.rJ_Button_勤務傳送系統.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_勤務傳送系統.ShadowSize = 3;
            this.rJ_Button_勤務傳送系統.ShowLoadingForm = false;
            this.rJ_Button_勤務傳送系統.Size = new System.Drawing.Size(191, 189);
            this.rJ_Button_勤務傳送系統.State = false;
            this.rJ_Button_勤務傳送系統.TabIndex = 36;
            this.rJ_Button_勤務傳送系統.Text = "勤務傳送系統";
            this.rJ_Button_勤務傳送系統.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_勤務傳送系統.TextHeight = 40;
            this.rJ_Button_勤務傳送系統.UseVisualStyleBackColor = false;
            this.rJ_Button_勤務傳送系統.Visible = false;
            // 
            // rJ_Button_中心叫號系統
            // 
            this.rJ_Button_中心叫號系統.AutoResetState = false;
            this.rJ_Button_中心叫號系統.BackColor = System.Drawing.Color.White;
            this.rJ_Button_中心叫號系統.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_中心叫號系統.BackgroundImage = global::E_UpdateVersion.Properties.Resources.候藥系統;
            this.rJ_Button_中心叫號系統.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_中心叫號系統.BorderRadius = 12;
            this.rJ_Button_中心叫號系統.BorderSize = 1;
            this.rJ_Button_中心叫號系統.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_中心叫號系統.DisenableColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Button_中心叫號系統.Enabled = false;
            this.rJ_Button_中心叫號系統.FlatAppearance.BorderSize = 0;
            this.rJ_Button_中心叫號系統.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_中心叫號系統.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_中心叫號系統.ForeColor = System.Drawing.Color.Black;
            this.rJ_Button_中心叫號系統.GUID = "";
            this.rJ_Button_中心叫號系統.Image_padding = new System.Windows.Forms.Padding(16, 10, 22, 0);
            this.rJ_Button_中心叫號系統.Location = new System.Drawing.Point(230, 198);
            this.rJ_Button_中心叫號系統.Name = "rJ_Button_中心叫號系統";
            this.rJ_Button_中心叫號系統.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_中心叫號系統.ProhibitionLineWidth = 10;
            this.rJ_Button_中心叫號系統.ProhibitionSymbolSize = 60;
            this.rJ_Button_中心叫號系統.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_中心叫號系統.ShadowSize = 3;
            this.rJ_Button_中心叫號系統.ShowLoadingForm = false;
            this.rJ_Button_中心叫號系統.Size = new System.Drawing.Size(191, 189);
            this.rJ_Button_中心叫號系統.State = false;
            this.rJ_Button_中心叫號系統.TabIndex = 37;
            this.rJ_Button_中心叫號系統.Text = "中心叫號系統";
            this.rJ_Button_中心叫號系統.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_中心叫號系統.TextHeight = 40;
            this.rJ_Button_中心叫號系統.UseVisualStyleBackColor = false;
            this.rJ_Button_中心叫號系統.Visible = false;
            // 
            // rJ_Button_癌症備藥機
            // 
            this.rJ_Button_癌症備藥機.AutoResetState = false;
            this.rJ_Button_癌症備藥機.BackColor = System.Drawing.Color.White;
            this.rJ_Button_癌症備藥機.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Button_癌症備藥機.BackgroundImage = global::E_UpdateVersion.Properties.Resources.癌症備藥機;
            this.rJ_Button_癌症備藥機.BorderColor = System.Drawing.Color.Black;
            this.rJ_Button_癌症備藥機.BorderRadius = 12;
            this.rJ_Button_癌症備藥機.BorderSize = 1;
            this.rJ_Button_癌症備藥機.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_癌症備藥機.DisenableColor = System.Drawing.Color.WhiteSmoke;
            this.rJ_Button_癌症備藥機.Enabled = false;
            this.rJ_Button_癌症備藥機.FlatAppearance.BorderSize = 0;
            this.rJ_Button_癌症備藥機.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_癌症備藥機.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.rJ_Button_癌症備藥機.ForeColor = System.Drawing.Color.Black;
            this.rJ_Button_癌症備藥機.GUID = "";
            this.rJ_Button_癌症備藥機.Image_padding = new System.Windows.Forms.Padding(16, 10, 22, 0);
            this.rJ_Button_癌症備藥機.Location = new System.Drawing.Point(427, 198);
            this.rJ_Button_癌症備藥機.Name = "rJ_Button_癌症備藥機";
            this.rJ_Button_癌症備藥機.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_癌症備藥機.ProhibitionLineWidth = 10;
            this.rJ_Button_癌症備藥機.ProhibitionSymbolSize = 60;
            this.rJ_Button_癌症備藥機.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_癌症備藥機.ShadowSize = 3;
            this.rJ_Button_癌症備藥機.ShowLoadingForm = false;
            this.rJ_Button_癌症備藥機.Size = new System.Drawing.Size(191, 189);
            this.rJ_Button_癌症備藥機.State = false;
            this.rJ_Button_癌症備藥機.TabIndex = 35;
            this.rJ_Button_癌症備藥機.Text = "癌症備藥系統";
            this.rJ_Button_癌症備藥機.TextColor = System.Drawing.Color.Black;
            this.rJ_Button_癌症備藥機.TextHeight = 40;
            this.rJ_Button_癌症備藥機.UseVisualStyleBackColor = false;
            this.rJ_Button_癌症備藥機.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(653, 617);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_Button rJ_Button_離開;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 後台設定ToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyUI.RJ_Button rJ_Button_智能藥庫系統;
        private MyUI.RJ_Button rJ_Button_智慧調劑台系統;
        private MyUI.RJ_Button rJ_Button_癌症備藥機;
        private MyUI.RJ_Button rJ_Button_勤務傳送系統;
        private MyUI.RJ_Button rJ_Button_中心叫號系統;
        private MyUI.RJ_Button rJ_Button_中藥調劑系統;
    }
}

