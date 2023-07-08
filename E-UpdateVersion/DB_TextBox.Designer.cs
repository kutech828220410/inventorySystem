
namespace E_UpdateVersion
{
    partial class DB_TextBox
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_標題
            // 
            this.label_標題.BackColor = System.Drawing.Color.Black;
            this.label_標題.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_標題.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_標題.ForeColor = System.Drawing.Color.White;
            this.label_標題.Location = new System.Drawing.Point(0, 0);
            this.label_標題.Name = "label_標題";
            this.label_標題.Size = new System.Drawing.Size(153, 30);
            this.label_標題.TabIndex = 0;
            this.label_標題.Text = "標題";
            this.label_標題.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox.Location = new System.Drawing.Point(153, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(462, 29);
            this.textBox.TabIndex = 1;
            // 
            // DB_TextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label_標題);
            this.Name = "DB_TextBox";
            this.Size = new System.Drawing.Size(615, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_標題;
        private System.Windows.Forms.TextBox textBox;
    }
}
