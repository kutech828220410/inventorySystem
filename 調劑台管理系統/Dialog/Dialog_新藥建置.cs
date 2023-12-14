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
using MyUI;

namespace 調劑台管理系統
{
    public partial class Dialog_新藥建置 : Form
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

        public Dialog_新藥建置()
        {
            InitializeComponent();
            this.Load += Dialog_新藥建置_Load;
        }
        private void Dialog_新藥建置_Load(object sender, EventArgs e)
        {
            
        }
    }
}
