
namespace ServerSettingForm
{
    partial class Panel_API_URL
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_標題 = new System.Windows.Forms.Label();
            this.button_測試 = new System.Windows.Forms.Button();
            this.rJ_TextBox_API_URL = new MyUI.RJ_TextBox();
            this.SuspendLayout();
            // 
            // label_標題
            // 
            this.label_標題.BackColor = System.Drawing.Color.Black;
            this.label_標題.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_標題.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_標題.ForeColor = System.Drawing.Color.White;
            this.label_標題.Location = new System.Drawing.Point(0, 0);
            this.label_標題.Name = "label_標題";
            this.label_標題.Size = new System.Drawing.Size(214, 36);
            this.label_標題.TabIndex = 18;
            this.label_標題.Text = "標題";
            this.label_標題.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_測試
            // 
            this.button_測試.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_測試.Location = new System.Drawing.Point(824, 0);
            this.button_測試.Name = "button_測試";
            this.button_測試.Size = new System.Drawing.Size(40, 36);
            this.button_測試.TabIndex = 23;
            this.button_測試.Text = "測試";
            this.button_測試.UseVisualStyleBackColor = true;
            // 
            // rJ_TextBox_API_URL
            // 
            this.rJ_TextBox_API_URL.AutoSize = true;
            this.rJ_TextBox_API_URL.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_API_URL.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_API_URL.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_API_URL.BorderRadius = 0;
            this.rJ_TextBox_API_URL.BorderSize = 2;
            this.rJ_TextBox_API_URL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_TextBox_API_URL.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_API_URL.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_API_URL.GUID = "";
            this.rJ_TextBox_API_URL.Location = new System.Drawing.Point(214, 0);
            this.rJ_TextBox_API_URL.Multiline = false;
            this.rJ_TextBox_API_URL.Name = "rJ_TextBox_API_URL";
            this.rJ_TextBox_API_URL.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_API_URL.PassWordChar = false;
            this.rJ_TextBox_API_URL.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_API_URL.PlaceholderText = "http://XXX.XXX.XXX.XXX:4433";
            this.rJ_TextBox_API_URL.ShowTouchPannel = false;
            this.rJ_TextBox_API_URL.Size = new System.Drawing.Size(610, 36);
            this.rJ_TextBox_API_URL.TabIndex = 24;
            this.rJ_TextBox_API_URL.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox_API_URL.Texts = "";
            this.rJ_TextBox_API_URL.UnderlineStyle = false;
            // 
            // Panel_API_URL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rJ_TextBox_API_URL);
            this.Controls.Add(this.button_測試);
            this.Controls.Add(this.label_標題);
            this.Name = "Panel_API_URL";
            this.Size = new System.Drawing.Size(864, 36);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_標題;
        private System.Windows.Forms.Button button_測試;
        private MyUI.RJ_TextBox rJ_TextBox_API_URL;
    }
}
