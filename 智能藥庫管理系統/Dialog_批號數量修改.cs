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

namespace 智能藥庫管理系統
{
    public partial class Dialog_批號數量修改 : MyDialog
    {
        public Dialog_批號數量修改()
        {
            InitializeComponent();

            this.rJ_Button_確認選擇.MouseDownEvent += RJ_Button_確認選擇_MouseDownEvent;
        }

        private void RJ_Button_確認選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {  
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }));
        }
    }
}
