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
                    returnData returnData = sessionClass.LoginByUID(Main_Form.API_Server, Main_Form.RFID_UID_01);
                    if (returnData == null) return;
                    if (returnData.Data == null) return;
                    if (returnData.Code != 200) return;
                    Value = returnData.Data.ObjToClass<sessionClass>();
                    if (returnData.Code == 200)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"[{Value.Name}] 登入成功", 1500, Color.Green);
                        dialog_AlarmForm.ShowDialog();
                        this.Invoke(new Action(delegate
                        {
                            DialogResult = DialogResult.Yes;
                            this.Close();
                        }));
                    }
            
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
            returnData returnData = sessionClass.LoginByID(Main_Form.API_Server, UserID, Password);
            if (returnData == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"API連結失敗", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if(returnData.Code != 200)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"{returnData.Result}", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if(returnData.Data == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"API連結失敗", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            Value = returnData.Data.ObjToClass<sessionClass>();

            if (returnData.Code == 200)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"[{Value.Name}] 登入成功", 1500, Color.Green);
                dialog_AlarmForm.ShowDialog();
            }
            this.Invoke(new Action(delegate 
            {
                DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
    }
}
