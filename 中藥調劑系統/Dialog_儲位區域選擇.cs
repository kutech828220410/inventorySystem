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
using MyOffice;

namespace 中藥調劑系統
{
    public partial class Dialog_儲位區域選擇 : MyDialog
    {
        public string Value = "";
        private string[] _enum_text = null;
        public Dialog_儲位區域選擇(string[] enum_text)
        {
            InitializeComponent();
            this.Load += Dialog_儲位區域選擇_Load;
            this.FormClosing += Dialog_儲位區域選擇_FormClosing;
            this.LoadFinishedEvent += Dialog_儲位區域選擇_LoadFinishedEvent;
            this._enum_text = enum_text;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
        }

    
        private void Dialog_儲位區域選擇_LoadFinishedEvent(EventArgs e)
        {
            this.comboBox_區域.DataSource = _enum_text;
            this.comboBox_區域.SelectedIndex = 0;
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                Value = this.comboBox_區域.Text;
                this.Close();
            }));
          
        }
        private void Dialog_儲位區域選擇_FormClosing(object sender, FormClosingEventArgs e)
        {
    
        }
        private void Dialog_儲位區域選擇_Load(object sender, EventArgs e)
        {
            
        }
    }
}
