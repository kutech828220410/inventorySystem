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
namespace 智能藥庫系統
{
    public partial class Dialog_盤點單覆盤建議設定 : MyDialog
    {
        private MyThread myThread;

        public Dialog_盤點單覆盤建議設定()
        {
            InitializeComponent();
            this.LoadFinishedEvent += Dialog_盤點單覆盤建議設定_LoadFinishedEvent;
            this.FormClosing += Dialog_盤點單覆盤建議設定_FormClosing;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
        }

        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
  
        }

        private void Dialog_盤點單覆盤建議設定_FormClosing(object sender, FormClosingEventArgs e)
        {
 
        }
        private void Dialog_盤點單覆盤建議設定_LoadFinishedEvent(EventArgs e)
        {
           
        }
    }
}
