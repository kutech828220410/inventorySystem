
namespace Logger_WindowsFormsApp
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
            this.init = new System.Windows.Forms.Button();
            this.get_all = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // init
            // 
            this.init.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.init.Location = new System.Drawing.Point(128, 108);
            this.init.Name = "init";
            this.init.Size = new System.Drawing.Size(149, 141);
            this.init.TabIndex = 0;
            this.init.Text = "Init";
            this.init.UseVisualStyleBackColor = true;
            // 
            // get_all
            // 
            this.get_all.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.get_all.Location = new System.Drawing.Point(303, 108);
            this.get_all.Name = "get_all";
            this.get_all.Size = new System.Drawing.Size(298, 141);
            this.get_all.TabIndex = 0;
            this.get_all.Text = "Get_ALL";
            this.get_all.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.get_all);
            this.Controls.Add(this.init);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button init;
        private System.Windows.Forms.Button get_all;
    }
}

