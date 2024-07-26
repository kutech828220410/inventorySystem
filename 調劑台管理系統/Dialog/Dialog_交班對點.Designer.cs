
namespace 調劑台管理系統
{
    partial class Dialog_交班對點
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.stepViewer = new MyUI.StepViewer();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1592, 16);
            this.panel1.TabIndex = 0;
            // 
            // stepViewer
            // 
            this.stepViewer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.stepViewer.CurrentStep = 0;
            this.stepViewer.Dock = System.Windows.Forms.DockStyle.Top;
            this.stepViewer.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stepViewer.LineWidth = 60;
            this.stepViewer.ListDataSource = null;
            this.stepViewer.Location = new System.Drawing.Point(4, 44);
            this.stepViewer.Margin = new System.Windows.Forms.Padding(4);
            this.stepViewer.Name = "stepViewer";
            this.stepViewer.Size = new System.Drawing.Size(1592, 108);
            this.stepViewer.TabIndex = 7;
            // 
            // Dialog_交班對點
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.ControlBox = true;
            this.Controls.Add(this.stepViewer);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_交班對點";
            this.Text = "交班對點";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyUI.StepViewer stepViewer;
    }
}