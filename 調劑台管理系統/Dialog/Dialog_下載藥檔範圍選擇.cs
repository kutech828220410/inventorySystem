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

namespace 調劑台管理系統
{
    public partial class Dialog_中西藥選擇 : MyDialog
    {

        public bool flag_中藥
        {
            get
            {
                return this.checkBox_中藥.Checked;
            }
        }
        public bool flag_西藥
        {
            get
            {
                return this.checkBox_西藥.Checked;
            }
        }

        public Dialog_中西藥選擇()
        {
            InitializeComponent();
            this.rJ_Button_確認選擇.MouseDownEvent += RJ_Button_確認選擇_MouseDownEvent;
        }

        private void RJ_Button_確認選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (flag_中藥 == false && flag_西藥 == false)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請至少選取一種藥品範圍", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }));
        }
    }
}
