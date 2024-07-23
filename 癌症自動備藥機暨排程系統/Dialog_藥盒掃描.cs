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

namespace 癌症自動備藥機暨排程系統
{
    public partial class Dialog_藥盒掃描 : MyDialog
    {
        private MyThread myThread_program;
        public string Value = "";
        public Dialog_藥盒掃描()
        {
            InitializeComponent();

            this.LoadFinishedEvent += Dialog_藥盒掃描_LoadFinishedEvent;
            this.plC_RJ_Button_取消.MouseDownEvent += PlC_RJ_Button_取消_MouseDownEvent;

        }

    

        private void sub_program()
        {
            string text = "";
            string text_01 = Main_Form.Function_ReadBacodeScanner01();
            string text_02 = Main_Form.Function_ReadBacodeScanner01();
            if (text_01.StringIsEmpty() == false) text = text_01;
            if (text_02.StringIsEmpty() == false) text = text_02;
            if (text.StringIsEmpty() == false)
            {
                this.Invoke(new Action(delegate 
                {
                    Value = text;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }));

                return;
            }
        }
        private void Dialog_藥盒掃描_LoadFinishedEvent(EventArgs e)
        {
            myThread_program = new MyThread();
            myThread_program.AutoRun(true);
            myThread_program.AutoStop(true);
            myThread_program.Add_Method(sub_program);
            myThread_program.Trigger();
        }
        private void PlC_RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Close();
        }
    }
}
