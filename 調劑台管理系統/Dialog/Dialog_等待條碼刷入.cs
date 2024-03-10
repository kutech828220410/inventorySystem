﻿using System;
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

namespace 調劑台管理系統
{
    public partial class Dialog_等待條碼刷入 : MyDialog
    {
        public string Value = "";
        private MyThread MyThread_program;


        public Dialog_等待條碼刷入()
        {
            InitializeComponent();
            this.Load += Dialog_等待條碼刷入_Load;
            this.FormClosed += Dialog_等待條碼刷入_FormClosed;
            rJ_Button_退出.MouseDownEvent += RJ_Button_退出_MouseDownEvent;
        }
        private void sub_program()
        {
            if (Main_Form.人員資料_BarCode.StringIsEmpty() == false && this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    Value = Main_Form.人員資料_BarCode;
                    this.label_state.Text = $"成功刷入!{Value}";
                    this.label_state.BackColor = Color.GreenYellow;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }));

            }
        }
        #region Event
        private void Dialog_等待條碼刷入_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MyThread_program != null)
            {
                MyThread_program.Abort();
                MyThread_program = null;
            }
        }
        private void RJ_Button_退出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Close();

            }));
        }
        private void Dialog_等待條碼刷入_Load(object sender, EventArgs e)
        {
            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();
        }
        #endregion
    }
}
