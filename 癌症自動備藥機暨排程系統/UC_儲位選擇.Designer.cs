﻿
namespace 癌症自動備藥機暨排程系統
{
    partial class UC_儲位選擇
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
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.rJ_Lable_藥品資訊 = new MyUI.RJ_Lable();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rJ_Button_確認選擇 = new MyUI.RJ_Button();
            this.sqL_DataGridView_儲位選擇 = new SQLUI.SQL_DataGridView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.DarkGray;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 3;
            this.rJ_Lable1.Size = new System.Drawing.Size(744, 108);
            this.rJ_Lable1.TabIndex = 1;
            this.rJ_Lable1.Text = "儲 位 選 擇";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Lable_藥品資訊
            // 
            this.rJ_Lable_藥品資訊.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_藥品資訊.BackgroundColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_藥品資訊.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_藥品資訊.BorderRadius = 10;
            this.rJ_Lable_藥品資訊.BorderSize = 0;
            this.rJ_Lable_藥品資訊.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable_藥品資訊.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_藥品資訊.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable_藥品資訊.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_藥品資訊.GUID = "";
            this.rJ_Lable_藥品資訊.Location = new System.Drawing.Point(0, 108);
            this.rJ_Lable_藥品資訊.Name = "rJ_Lable_藥品資訊";
            this.rJ_Lable_藥品資訊.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_藥品資訊.ShadowSize = 0;
            this.rJ_Lable_藥品資訊.Size = new System.Drawing.Size(744, 78);
            this.rJ_Lable_藥品資訊.TabIndex = 8;
            this.rJ_Lable_藥品資訊.Text = "(XXXXXX) XXXXXXXXXXXXXXXXXXX";
            this.rJ_Lable_藥品資訊.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rJ_Lable_藥品資訊.TextColor = System.Drawing.Color.Black;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rJ_Button_確認選擇);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 521);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 116);
            this.panel1.TabIndex = 9;
            // 
            // rJ_Button_確認選擇
            // 
            this.rJ_Button_確認選擇.AutoResetState = false;
            this.rJ_Button_確認選擇.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_確認選擇.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button_確認選擇.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_確認選擇.BorderRadius = 10;
            this.rJ_Button_確認選擇.BorderSize = 0;
            this.rJ_Button_確認選擇.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_確認選擇.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Button_確認選擇.FlatAppearance.BorderSize = 0;
            this.rJ_Button_確認選擇.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_確認選擇.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_確認選擇.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_確認選擇.GUID = "";
            this.rJ_Button_確認選擇.Location = new System.Drawing.Point(0, 0);
            this.rJ_Button_確認選擇.Name = "rJ_Button_確認選擇";
            this.rJ_Button_確認選擇.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_確認選擇.ShadowSize = 3;
            this.rJ_Button_確認選擇.ShowLoadingForm = false;
            this.rJ_Button_確認選擇.Size = new System.Drawing.Size(744, 116);
            this.rJ_Button_確認選擇.State = false;
            this.rJ_Button_確認選擇.TabIndex = 0;
            this.rJ_Button_確認選擇.Text = "確 認 選 擇";
            this.rJ_Button_確認選擇.TextColor = System.Drawing.Color.White;
            this.rJ_Button_確認選擇.UseVisualStyleBackColor = false;
            // 
            // sqL_DataGridView_儲位選擇
            // 
            this.sqL_DataGridView_儲位選擇.AutoSelectToDeep = true;
            this.sqL_DataGridView_儲位選擇.backColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_儲位選擇.BorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_儲位選擇.BorderRadius = 0;
            this.sqL_DataGridView_儲位選擇.BorderSize = 2;
            this.sqL_DataGridView_儲位選擇.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_儲位選擇.cellStylBackColor = System.Drawing.Color.PowderBlue;
            this.sqL_DataGridView_儲位選擇.cellStyleFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_儲位選擇.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_儲位選擇.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_儲位選擇.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_儲位選擇.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_儲位選擇.columnHeadersHeight = 40;
            this.sqL_DataGridView_儲位選擇.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_儲位選擇.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqL_DataGridView_儲位選擇.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_儲位選擇.ImageBox = false;
            this.sqL_DataGridView_儲位選擇.Location = new System.Drawing.Point(0, 186);
            this.sqL_DataGridView_儲位選擇.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.sqL_DataGridView_儲位選擇.Name = "sqL_DataGridView_儲位選擇";
            this.sqL_DataGridView_儲位選擇.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_儲位選擇.Password = "user82822040";
            this.sqL_DataGridView_儲位選擇.Port = ((uint)(3306u));
            this.sqL_DataGridView_儲位選擇.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_儲位選擇.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_儲位選擇.RowsColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_儲位選擇.RowsHeight = 80;
            this.sqL_DataGridView_儲位選擇.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_儲位選擇.Server = "127.0.0.0";
            this.sqL_DataGridView_儲位選擇.Size = new System.Drawing.Size(744, 335);
            this.sqL_DataGridView_儲位選擇.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_儲位選擇.TabIndex = 18;
            this.sqL_DataGridView_儲位選擇.UserName = "root";
            this.sqL_DataGridView_儲位選擇.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_儲位選擇.可選擇多列 = false;
            this.sqL_DataGridView_儲位選擇.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_儲位選擇.自動換行 = true;
            this.sqL_DataGridView_儲位選擇.表單字體 = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_儲位選擇.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_儲位選擇.顯示CheckBox = false;
            this.sqL_DataGridView_儲位選擇.顯示首列 = false;
            this.sqL_DataGridView_儲位選擇.顯示首行 = false;
            this.sqL_DataGridView_儲位選擇.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_儲位選擇.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // UC_儲位選擇
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.sqL_DataGridView_儲位選擇);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rJ_Lable_藥品資訊);
            this.Controls.Add(this.rJ_Lable1);
            this.Name = "UC_儲位選擇";
            this.Size = new System.Drawing.Size(744, 637);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_Lable rJ_Lable1;
        private MyUI.RJ_Lable rJ_Lable_藥品資訊;
        private System.Windows.Forms.Panel panel1;
        private MyUI.RJ_Button rJ_Button_確認選擇;
        private SQLUI.SQL_DataGridView sqL_DataGridView_儲位選擇;
    }
}
