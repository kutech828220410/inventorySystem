
namespace ServerSettingForm
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
            this.button_調劑台 = new System.Windows.Forms.Button();
            this.button_藥庫 = new System.Windows.Forms.Button();
            this.button_網頁 = new System.Windows.Forms.Button();
            this.button_更新資訊 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_調劑台
            // 
            this.button_調劑台.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_調劑台.Location = new System.Drawing.Point(84, 119);
            this.button_調劑台.Name = "button_調劑台";
            this.button_調劑台.Size = new System.Drawing.Size(284, 86);
            this.button_調劑台.TabIndex = 0;
            this.button_調劑台.Text = "調劑台";
            this.button_調劑台.UseVisualStyleBackColor = true;
            // 
            // button_藥庫
            // 
            this.button_藥庫.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_藥庫.Location = new System.Drawing.Point(84, 211);
            this.button_藥庫.Name = "button_藥庫";
            this.button_藥庫.Size = new System.Drawing.Size(284, 86);
            this.button_藥庫.TabIndex = 1;
            this.button_藥庫.Text = "藥庫";
            this.button_藥庫.UseVisualStyleBackColor = true;
            // 
            // button_網頁
            // 
            this.button_網頁.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_網頁.Location = new System.Drawing.Point(84, 27);
            this.button_網頁.Name = "button_網頁";
            this.button_網頁.Size = new System.Drawing.Size(284, 86);
            this.button_網頁.TabIndex = 2;
            this.button_網頁.Text = "網頁";
            this.button_網頁.UseVisualStyleBackColor = true;
            // 
            // button_更新資訊
            // 
            this.button_更新資訊.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_更新資訊.Location = new System.Drawing.Point(84, 332);
            this.button_更新資訊.Name = "button_更新資訊";
            this.button_更新資訊.Size = new System.Drawing.Size(284, 86);
            this.button_更新資訊.TabIndex = 3;
            this.button_更新資訊.Text = "更新資訊";
            this.button_更新資訊.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 430);
            this.Controls.Add(this.button_更新資訊);
            this.Controls.Add(this.button_網頁);
            this.Controls.Add(this.button_藥庫);
            this.Controls.Add(this.button_調劑台);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server 設定";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_調劑台;
        private System.Windows.Forms.Button button_藥庫;
        private System.Windows.Forms.Button button_網頁;
        private System.Windows.Forms.Button button_更新資訊;
    }
}

