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

namespace 癌症自動備藥機暨排程系統
{
    public partial class Dialog_等待RFID感應 : Form
    {

        public string Value = "";
        private MyThread MyThread_program;
        private RFID_FX600lib.RFID_FX600_UI _rFID_FX600_UI;
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

        public Dialog_等待RFID感應(RFID_FX600lib.RFID_FX600_UI rFID_FX600_UI)
        {
            InitializeComponent();
            this._rFID_FX600_UI = rFID_FX600_UI;
            this.Load += Dialog_等待RFID感應_Load;
            this.FormClosed += Dialog_等待RFID感應_FormClosed;
            rJ_Button_退出.MouseDownEvent += RJ_Button_退出_MouseDownEvent;
           
        }


        private void sub_program()
        {
            List<RFID_FX600lib.RFID_FX600_UI.RFID_Device> list_RFID_Devices = this._rFID_FX600_UI.Get_RFID();
            if (list_RFID_Devices.Count > 0 && this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    Value = list_RFID_Devices[0].UID;
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
        private void Dialog_等待RFID感應_FormClosed(object sender, FormClosedEventArgs e)
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
        private void Dialog_等待RFID感應_Load(object sender, EventArgs e)
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
