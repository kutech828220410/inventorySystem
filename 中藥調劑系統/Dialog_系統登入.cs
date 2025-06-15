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


namespace 中藥調劑系統
{
    public partial class Dialog_系統登入 : MyDialog
    {
        private MyThread myThread_program;
        public sessionClass Value = null;
        public string UserID
        {
            get
            {
                return this.rJ_TextBox_帳號.Texts;
            }
        }
        public string Password
        {
            get
            {
                return this.rJ_TextBox_密碼.Texts;
            }
        }


        public Dialog_系統登入()
        {
            InitializeComponent();

            rJ_TextBox_密碼.PassWordChar = true;

            this.Load += Dialog_系統登入_Load;
            this.FormClosing += Dialog_系統登入_FormClosing;
            this.rJ_TextBox_帳號.KeyPress += RJ_TextBox_帳號_KeyPress;
            this.rJ_TextBox_密碼.KeyPress += RJ_TextBox_密碼_KeyPress;

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            
        }

        private void sub_program()
        {
            if (Main_Form.RFID_UID_01.StringIsEmpty() == false)
            {
                if (Main_Form.RFID_UID_01.StringToInt32() != 0)
                {
                    sessionClass _sessionClass = sessionClass.LoginByUID(Main_Form.API_Server, Main_Form.RFID_UID_01);
                    Value = _sessionClass;
                    Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\登入成功.wav");
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"[{Value.Name}] 登入成功", 1500, 0, -(this.Height - 50), Color.Green);
                    dialog_AlarmForm.ShowDialog();
                    this.Invoke(new Action(delegate
                    {
                        DialogResult = DialogResult.Yes;
                        this.Close();
                    }));
                }
            }
        }

        private void Dialog_系統登入_Load(object sender, EventArgs e)
        {
            myThread_program = new MyThread();
            myThread_program.Add_Method(sub_program);
            myThread_program.AutoRun(true);
            myThread_program.AutoStop(true);
            myThread_program.SetSleepTime(100);
            myThread_program.Trigger();
        }
        private void Dialog_系統登入_FormClosing(object sender, FormClosingEventArgs e)
        {
            myThread_program.Abort();
            myThread_program = null;
        }
        private void RJ_TextBox_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.rJ_TextBox_密碼.Focus();
            }
        }
        private void RJ_TextBox_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RJ_Button_確認_MouseDownEvent(null);
            }
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_AlarmForm dialog_AlarmForm;
            sessionClass _sessionClass = sessionClass.LoginByID(Main_Form.API_Server, UserID, Password);
            if (_sessionClass == null)
            {
                dialog_AlarmForm = new Dialog_AlarmForm($"API連結失敗", 1500, 0, -(this.Height - 50));
                dialog_AlarmForm.ShowDialog();
                return;
            }
         
            Value = _sessionClass.ObjToClass<sessionClass>();

            Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\登入成功.wav");
            dialog_AlarmForm = new Dialog_AlarmForm($"[{Value.Name}] 登入成功", 1500, 0, -(this.Height - 50), Color.Green);
            dialog_AlarmForm.ShowDialog();

            this.Invoke(new Action(delegate 
            {
                DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
    }
}
