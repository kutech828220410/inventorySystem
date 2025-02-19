
namespace ServerSettingForm
{
    partial class Panel_sys_updateVersion
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
            this.checkBox_致能 = new System.Windows.Forms.CheckBox();
            this.button_上傳 = new System.Windows.Forms.Button();
            this.button_瀏覽 = new System.Windows.Forms.Button();
            this.textBox_filepath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_version = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_更新時間 = new System.Windows.Forms.Label();
            this.label_標題 = new System.Windows.Forms.Label();
            this.button_下載檔案 = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.button_刪除 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_致能
            // 
            this.checkBox_致能.AutoSize = true;
            this.checkBox_致能.Location = new System.Drawing.Point(451, 65);
            this.checkBox_致能.Name = "checkBox_致能";
            this.checkBox_致能.Size = new System.Drawing.Size(48, 16);
            this.checkBox_致能.TabIndex = 11;
            this.checkBox_致能.Text = "致能";
            this.checkBox_致能.UseVisualStyleBackColor = true;
            // 
            // button_上傳
            // 
            this.button_上傳.Location = new System.Drawing.Point(281, 104);
            this.button_上傳.Name = "button_上傳";
            this.button_上傳.Size = new System.Drawing.Size(72, 41);
            this.button_上傳.TabIndex = 10;
            this.button_上傳.Text = "上傳";
            this.button_上傳.UseVisualStyleBackColor = true;
            // 
            // button_瀏覽
            // 
            this.button_瀏覽.Location = new System.Drawing.Point(203, 104);
            this.button_瀏覽.Name = "button_瀏覽";
            this.button_瀏覽.Size = new System.Drawing.Size(72, 41);
            this.button_瀏覽.TabIndex = 9;
            this.button_瀏覽.Text = "瀏覽";
            this.button_瀏覽.UseVisualStyleBackColor = true;
            // 
            // textBox_filepath
            // 
            this.textBox_filepath.Location = new System.Drawing.Point(95, 62);
            this.textBox_filepath.Name = "textBox_filepath";
            this.textBox_filepath.Size = new System.Drawing.Size(350, 22);
            this.textBox_filepath.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 43);
            this.label2.TabIndex = 7;
            this.label2.Text = "路徑";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(13, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 43);
            this.label1.TabIndex = 13;
            this.label1.Text = "版本";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_version
            // 
            this.textBox_version.Location = new System.Drawing.Point(95, 113);
            this.textBox_version.Name = "textBox_version";
            this.textBox_version.Size = new System.Drawing.Size(102, 22);
            this.textBox_version.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_更新時間);
            this.panel1.Controls.Add(this.label_標題);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 37);
            this.panel1.TabIndex = 15;
            // 
            // label_更新時間
            // 
            this.label_更新時間.BackColor = System.Drawing.SystemColors.Window;
            this.label_更新時間.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_更新時間.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_更新時間.Location = new System.Drawing.Point(350, 0);
            this.label_更新時間.Name = "label_更新時間";
            this.label_更新時間.Size = new System.Drawing.Size(176, 37);
            this.label_更新時間.TabIndex = 14;
            this.label_更新時間.Text = "0000/00/00 00:00:00";
            this.label_更新時間.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_標題
            // 
            this.label_標題.BackColor = System.Drawing.Color.Black;
            this.label_標題.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_標題.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_標題.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_標題.Location = new System.Drawing.Point(0, 0);
            this.label_標題.Name = "label_標題";
            this.label_標題.Size = new System.Drawing.Size(350, 37);
            this.label_標題.TabIndex = 13;
            this.label_標題.Text = "標題";
            this.label_標題.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_下載檔案
            // 
            this.button_下載檔案.Location = new System.Drawing.Point(358, 104);
            this.button_下載檔案.Name = "button_下載檔案";
            this.button_下載檔案.Size = new System.Drawing.Size(72, 41);
            this.button_下載檔案.TabIndex = 16;
            this.button_下載檔案.Text = "下載檔案";
            this.button_下載檔案.UseVisualStyleBackColor = true;
            // 
            // button_刪除
            // 
            this.button_刪除.Location = new System.Drawing.Point(436, 104);
            this.button_刪除.Name = "button_刪除";
            this.button_刪除.Size = new System.Drawing.Size(72, 41);
            this.button_刪除.TabIndex = 17;
            this.button_刪除.Text = "刪除";
            this.button_刪除.UseVisualStyleBackColor = true;
            // 
            // Panel_sys_updateVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button_刪除);
            this.Controls.Add(this.button_下載檔案);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_version);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_致能);
            this.Controls.Add(this.button_上傳);
            this.Controls.Add(this.button_瀏覽);
            this.Controls.Add(this.textBox_filepath);
            this.Controls.Add(this.label2);
            this.Name = "Panel_sys_updateVersion";
            this.Size = new System.Drawing.Size(526, 160);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_致能;
        private System.Windows.Forms.Button button_上傳;
        private System.Windows.Forms.Button button_瀏覽;
        private System.Windows.Forms.TextBox textBox_filepath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_version;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_更新時間;
        private System.Windows.Forms.Label label_標題;
        private System.Windows.Forms.Button button_下載檔案;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button button_刪除;
    }
}
