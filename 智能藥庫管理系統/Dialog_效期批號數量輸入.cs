using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;
using MyUI;
using SQLUI;
using DrawingClass;
using H_Pannel_lib;

namespace 智能藥庫系統
{
    public partial class Dialog_效期批號數量輸入 : MyDialog
    {
        public string 效期 = "";
        public string 批號 = "";
        public double 數量 = 0;
        public Dialog_效期批號數量輸入()
        {
            InitializeComponent();

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_TextBox_數量.Click += RJ_TextBox_數量_Click;
            this.rJ_TextBox_效期.Leave += RJ_TextBox_效期_Leave;
        }
        public Dialog_效期批號數量輸入(DateTime 效期, string 批號, int 數量)
        {
            InitializeComponent();

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_TextBox_數量.Click += RJ_TextBox_數量_Click;
            this.rJ_TextBox_效期.Leave += RJ_TextBox_效期_Leave;

            this.效期 = 效期.ToDateString("");
            if (效期 == DateTime.MinValue) this.效期 = "";
            this.批號 = 批號;
            this.數量 = 數量;

            this.Load += Dialog_效期批號數量輸入_Load;

        }

        private void Dialog_效期批號數量輸入_Load(object sender, EventArgs e)
        {
            this.rJ_TextBox_效期.Texts = this.效期;
            this.rJ_TextBox_批號.Texts = this.批號;
            this.rJ_TextBox_數量.Texts = this.數量.ToString();
        }
        private void RJ_TextBox_效期_Leave(object sender, EventArgs e)
        {
            string text = this.rJ_TextBox_效期.Text;
            if(text.StringIsEmpty())
            {
                rJ_TextBox_效期.PlaceholderColor = Color.DarkGray;
                rJ_TextBox_效期.PlaceholderText = "效期(YYYYMMDD)";
                return;
            }
            if(text.Length < 8)
            {
                Task.Run(new Action(delegate
                {
                    rJ_TextBox_效期.SetPlcaeHolder("非法格式", Color.Red);
                    System.Threading.Thread.Sleep(1500);
                    rJ_TextBox_效期.SetPlcaeHolder("效期(YYYYMMDD)", Color.DarkGray);
                }));
                return;
            }

            text = $"{text.Substring(0, 4)}/{text.Substring(4, 2)}/{text.Substring(6, 2)}";

            if (text.Check_Date_String() == false)
            {
                Task.Run(new Action(delegate
                {
                    rJ_TextBox_效期.SetPlcaeHolder("非法格式", Color.Red);
                    System.Threading.Thread.Sleep(1500);
                    rJ_TextBox_效期.SetPlcaeHolder("效期(YYYYMMDD)", Color.DarkGray);
                }));
            }
            else
            {
                rJ_TextBox_效期.PlaceholderColor = Color.DarkGray;
                rJ_TextBox_效期.PlaceholderText = "效期(YYYYMMDD)";
            }
      
        }
        private void RJ_TextBox_數量_Click(object sender, EventArgs e)
        {
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("輸入數量", 0);
            dialog_NumPannel.Set_Location_Offset(this.Width,0);
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            數量 = dialog_NumPannel.Value;
            this.rJ_TextBox_數量.Text = 數量.ToString();
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.rJ_TextBox_效期.Text;
            if (text.StringIsEmpty() || text.Length < 8)
            {
                MyMessageBox.ShowDialog("效期日期無效");
                return;
            }
            text = $"{text.Substring(0, 4)}/{text.Substring(4, 2)}/{text.Substring(6, 2)}";
            if (text.Check_Date_String() == false)
            {
                MyMessageBox.ShowDialog("效期日期無效");
                return;
            }
            if (rJ_TextBox_數量.Texts.StringIsInt32() == false)
            {
                MyMessageBox.ShowDialog("請輸入數量");
                return;
            }
            效期 = text;
            批號 = rJ_TextBox_批號.Texts;
            數量 = rJ_TextBox_數量.Texts.StringToInt32();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
