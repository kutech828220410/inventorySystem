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
    public partial class Dialog_新增 : Form
    {
        public string Value
        {
            get
            {
                return rJ_TextBox_Value.Text;
            }
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

        public Dialog_新增()
        {
            InitializeComponent();
        }

        private void Dialog_新增_Load(object sender, EventArgs e)
        {
            button_確認.Click += Button_確認_Click;
        }

        private void Button_確認_Click(object sender, EventArgs e)
        {
            if(Value.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("名稱不得空白!");
                return;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();

        }
    }
}
