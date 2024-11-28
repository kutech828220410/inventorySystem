
namespace TestForm
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
            this.saveFileDialog_SaveExcel = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_call_api = new System.Windows.Forms.Button();
            this.button_LoadImage = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileDialog_SaveExcel
            // 
            this.saveFileDialog_SaveExcel.DefaultExt = "txt";
            this.saveFileDialog_SaveExcel.Filter = "Excel File (*.xlsx)|*.xlsx|txt File (*.txt)|*.txt;";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 69);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 828);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button_call_api
            // 
            this.button_call_api.Location = new System.Drawing.Point(131, 12);
            this.button_call_api.Name = "button_call_api";
            this.button_call_api.Size = new System.Drawing.Size(113, 40);
            this.button_call_api.TabIndex = 2;
            this.button_call_api.Text = "CallAPI";
            this.button_call_api.UseVisualStyleBackColor = true;
            // 
            // button_LoadImage
            // 
            this.button_LoadImage.Location = new System.Drawing.Point(12, 12);
            this.button_LoadImage.Name = "button_LoadImage";
            this.button_LoadImage.Size = new System.Drawing.Size(113, 40);
            this.button_LoadImage.TabIndex = 0;
            this.button_LoadImage.Text = "LoadImage";
            this.button_LoadImage.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView.Location = new System.Drawing.Point(725, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1247, 973);
            this.dataGridView.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1972, 973);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_call_api);
            this.Controls.Add(this.button_LoadImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog_SaveExcel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_call_api;
        private System.Windows.Forms.Button button_LoadImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}

