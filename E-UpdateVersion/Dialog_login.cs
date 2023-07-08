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
namespace E_UpdateVersion
{
    public partial class Dialog_login : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                Button_登入_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
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

        static public string user = "admin";
        static public string password = "66437068";

        public Dialog_login()
        {
            InitializeComponent();
        }

        private void Dialog_login_Load(object sender, EventArgs e)
        {
            this.button_登入.Click += Button_登入_Click;
        }

        private void Button_登入_Click(object sender, EventArgs e)
        {
           if(textBox_user.Text.StringIsEmpty() || textBox_password.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("帳號密碼不得空白!");
                return;
            }
           if((textBox_user.Text.ToUpper() == user.ToUpper() == false) || (textBox_password.Text.ToUpper() == password.ToUpper() == false))
            {
                MyMessageBox.ShowDialog("帳號密碼錯誤!");
                return;
            }
            MyMessageBox.ShowDialog("登入成功!");
            DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
