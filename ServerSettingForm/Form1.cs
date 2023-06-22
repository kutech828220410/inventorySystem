using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace ServerSettingForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyMessageBox.音效 = false;
            MyMessageBox.form = this.FindForm();
            Dialog_調劑台.form = this.FindForm();

            this.button_網頁.Click += Button_網頁_Click;
            this.button_調劑台.Click += Button_調劑台_Click;
            this.button_藥庫.Click += Button_藥庫_Click;
        }

        private void Button_網頁_Click(object sender, EventArgs e)
        {
            Dialog_網頁 dialog_網頁 = new Dialog_網頁();
            dialog_網頁.ShowDialog();
        }

        private void Button_藥庫_Click(object sender, EventArgs e)
        {
            Dialog_藥庫 dialog_藥庫 = new Dialog_藥庫();
            dialog_藥庫.ShowDialog();
        }
        private void Button_調劑台_Click(object sender, EventArgs e)
        {
            Dialog_調劑台 dialog_調劑台 = new Dialog_調劑台();
            dialog_調劑台.ShowDialog();
        }
    }
}
