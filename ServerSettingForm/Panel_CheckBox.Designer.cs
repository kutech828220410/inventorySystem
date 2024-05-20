
namespace ServerSettingForm
{
    partial class Panel_CheckBox
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
            this.label_ServerType = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label_ServerType
            // 
            this.label_ServerType.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_ServerType.Location = new System.Drawing.Point(0, 0);
            this.label_ServerType.Name = "label_ServerType";
            this.label_ServerType.Size = new System.Drawing.Size(358, 18);
            this.label_ServerType.TabIndex = 0;
            this.label_ServerType.Text = "label1";
            this.label_ServerType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox
            // 
            this.checkBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox.Location = new System.Drawing.Point(0, 18);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(358, 30);
            this.checkBox.TabIndex = 1;
            this.checkBox.Text = "內容";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // Panel_CheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.label_ServerType);
            this.Name = "Panel_CheckBox";
            this.Size = new System.Drawing.Size(358, 48);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_ServerType;
        private System.Windows.Forms.CheckBox checkBox;
    }
}
