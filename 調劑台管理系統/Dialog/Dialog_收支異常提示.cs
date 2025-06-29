using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using SQLUI;
using FpMatchLib;
using HIS_DB_Lib;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_收支異常提示 : MyDialog
    {
        public event RJ_Button.MouseDownEventHandler MouseDownEvent_LokcOpen;
        public event RJ_Button.MouseDownEventHandler MouseDownEvent_Ignore;
        public bool IgnoreVisible = false;
        public string tts_content = "";
        private MyThread myThread = new MyThread();
        public Dialog_收支異常提示(string tts_content)
        {
            InitializeComponent();
            this.tts_content = tts_content;
            this.Load += Dialog_收支異常提示_Load;
            this.FormClosing += Dialog_收支異常提示_FormClosing;
        }

        private void Dialog_收支異常提示_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThread != null) myThread.Abort();
            myThread = null;
        }
        private void Dialog_收支異常提示_Load(object sender, EventArgs e)
        {
            rJ_Button_開鎖.MouseDownEvent += RJ_Button_開鎖_MouseDownEvent;
            rJ_Button_跳過.MouseDownEvent += RJ_Button_跳過_MouseDownEvent;
            rJ_Button_跳過.Visible = IgnoreVisible;
            myThread.Add_Method(sub_program);
            myThread.AutoRun(true);
            myThread.SetSleepTime(500);
            myThread.Trigger();
        }
        public void sub_program()
        {
            if(tts_content.StringIsEmpty())
            {
                Voice.MediaPlay($@"{Main_Form.currentDirectory}\alarm.wav");
            }
            else
            {
                Voice.PlayGoogleTTS(tts_content);
            }
        
        }
        private void RJ_Button_跳過_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MouseDownEvent_Ignore != null) MouseDownEvent_Ignore(mevent);
            this.DialogResult = DialogResult.Abort;
        }
        private void RJ_Button_開鎖_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MouseDownEvent_LokcOpen != null) MouseDownEvent_LokcOpen(mevent);
        }

        /// <summary>
        /// 關閉所有已開啟的 Dialog_收支異常提示 視窗
        /// </summary>
        public static void CloseAllDialog()
        {
            foreach (Form form in Application.OpenForms.OfType<Form>().ToList())
            {
                if (form is Dialog_收支異常提示)
                {
                    form.Invoke(new Action(() =>
                    {
                        try
                        {
                            form.Close();
                        }
                        catch
                        {
                            // 可以視情況加上LOG
                        }
                    }));
                }
            }
        }
    }
}
