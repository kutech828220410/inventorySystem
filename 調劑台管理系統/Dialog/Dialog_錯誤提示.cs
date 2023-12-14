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

namespace 調劑台管理系統
{
    public partial class Dialog_錯誤提示 : Form
    {
        public string Value = "";
        private MyThread MyThread_program;
        private string _title = "";
        private int _time_ms = 1000;
        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }

        public Dialog_錯誤提示(string title, int time_ms)
        {
            InitializeComponent();
            _title = title;
            _time_ms = time_ms;


            this.Load += Dialog_錯誤提示_Load;
            this.FormClosing += Dialog_錯誤提示_FormClosing;
        }
        private void sub_program()
        {
            System.Threading.Thread.Sleep(_time_ms);
            this.Invoke(new Action(delegate
            {
                this.Close();
            }));
        
        }
        private void Dialog_錯誤提示_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MyThread_program != null)
            {
                MyThread_program.Abort();
                MyThread_program = null;
            }
        }

        private void Dialog_錯誤提示_Load(object sender, EventArgs e)
        {
            this.rJ_Lable1.Text = _title;

            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();
        }
    }
}
