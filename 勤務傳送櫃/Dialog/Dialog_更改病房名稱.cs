using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 勤務傳送櫃
{
    public partial class Dialog_更改病房名稱 : Form
    {
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
        private static Dialog_更改病房名稱 dialog_更改病房名稱;
        private static readonly object synRoot = new object();
        static private bool formIsCreate = false;
        public enum_Type Enum_Type = enum_Type.None;
        public string 原始名稱
        {
            get
            {
                return this.rJ_TextBox_原始名稱.Texts;
            }
            private set
            {
                this.rJ_TextBox_原始名稱.Texts = value;
            }
        }
        public string 修改名稱
        {
            get
            {
                return this.rJ_TextBox_更動名稱.Texts;
            }
            private set
            {
                this.rJ_TextBox_更動名稱.Texts = value;
            }
        }
        public enum enum_Type
        {
            OK,
            Cancel,
            None,
        }
        static public bool FormIsCreate
        {
            get
            {
                return formIsCreate;
            }
            private set
            {
                formIsCreate = value;
            }
        }

        public static Dialog_更改病房名稱 GetForm(string name)
        {
            lock (synRoot)
            {
                if (dialog_更改病房名稱 == null)
                {
                    dialog_更改病房名稱 = new Dialog_更改病房名稱();
                }
                formIsCreate = true;
            }
            dialog_更改病房名稱.原始名稱 = name;
            dialog_更改病房名稱.修改名稱 = "";
            dialog_更改病房名稱.Enum_Type = enum_Type.None;
            return dialog_更改病房名稱;
        }
        public Dialog_更改病房名稱()
        {
            InitializeComponent();
        }

        private void rJ_Button_確認_Click(object sender, EventArgs e)
        {
            Enum_Type = enum_Type.OK;
            this.Close();
        }

        private void rJ_Button_取消_Click(object sender, EventArgs e)
        {
            Enum_Type = enum_Type.Cancel;
            this.Close();
        }

        private void Dialog_更改病房名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                Enum_Type = enum_Type.OK;
                this.Close();
            }
        }

        private void rJ_TextBox_更動名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Enum_Type = enum_Type.OK;
                this.Close();
            }
        }

        private void Dialog_更改病房名稱_FormClosed(object sender, FormClosedEventArgs e)
        {
  
        }
    }
}
