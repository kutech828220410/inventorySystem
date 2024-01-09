using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_Call_Light_System
{
    public partial class Dialog_螢幕選擇 : Form
    {
        public static System.Windows.Forms.Form form;
        new public DialogResult ShowDialog()
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
        public int Value = 0;
        public Dialog_螢幕選擇()
        {
            InitializeComponent();
        }

        private void Dialog_Load(object sender, EventArgs e)
        {
            this.button_1.Click += Button_1_Click;
            this.button_2.Click += Button_2_Click;
            this.button_3.Click += Button_3_Click;
            this.button_4.Click += Button_4_Click;
            this.button_取消.Click += Button_取消_Click;

        }
        private void Button_取消_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            Value = 0;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void Button_2_Click(object sender, EventArgs e)
        {
            Value = 1;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void Button_3_Click(object sender, EventArgs e)
        {
            Value = 2;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void Button_4_Click(object sender, EventArgs e)
        {
            Value = 3;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
