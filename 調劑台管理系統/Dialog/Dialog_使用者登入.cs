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
using SQLUI;
using HIS_DB_Lib;
using H_Pannel_lib;
using MyUI;
using FpMatchLib;
namespace 調劑台管理系統
{
    public partial class Dialog_使用者登入 : MyDialog
    {
        public object[] Value;
        static public MyTimerBasic myTimerBasic_覆核完成 = new MyTimerBasic();
        private MyThread MyThread_program;
        public static bool IsShown = false;
        private bool _flag_已登入 = false;
        private SQL_DataGridView sQL_DataGridView_人員資料;
        private RFID_FX600lib.RFID_FX600_UI rFID_FX600_UI;
        private string 藥名;
        private string 已登入ID;
        public string UserName = "";
        public string UserID = "";
        private Point location = new Point(0, 0);
        public new Point Location
        {
            get
            {
                return this.location;
            }
            set
            {
           
                this.location = value;
            }
        }
        public Dialog_使用者登入()
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);

            this.Load += Dialog_使用者登入_Load;
            this.FormClosed += Dialog_使用者登入_FormClosed;
            this.plC_RJ_Button_登入.MouseDownEventEx += PlC_RJ_Button_登入_MouseDownEventEx;
            this.plC_RJ_Button_確認.MouseDownEventEx += PlC_RJ_Button_確認_MouseDownEventEx;
            this.plC_RJ_Button_取消.MouseDownEventEx += PlC_RJ_Button_取消_MouseDownEventEx;
            plC_RJ_Button_登入.音效 = false;
            plC_RJ_Button_確認.音效 = false;
            plC_RJ_Button_取消.音效 = false;
            this.sQL_DataGridView_人員資料 = Main_Form._sqL_DataGridView_人員資料;
            this.rFID_FX600_UI = Main_Form._RFID_FX600_UI;
            this.LoadFinishedEvent += Dialog_使用者登入_LoadFinishedEvent;

        }
        public Dialog_使用者登入(string _已登入ID)
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);

            this.Load += Dialog_使用者登入_Load;
            this.FormClosed += Dialog_使用者登入_FormClosed;
            this.plC_RJ_Button_登入.MouseDownEventEx += PlC_RJ_Button_登入_MouseDownEventEx;
            this.plC_RJ_Button_確認.MouseDownEventEx += PlC_RJ_Button_確認_MouseDownEventEx;
            this.plC_RJ_Button_取消.MouseDownEventEx += PlC_RJ_Button_取消_MouseDownEventEx;
            plC_RJ_Button_登入.音效 = false;
            plC_RJ_Button_確認.音效 = false;
            plC_RJ_Button_取消.音效 = false;
            this.sQL_DataGridView_人員資料 = Main_Form._sqL_DataGridView_人員資料;
            this.已登入ID = _已登入ID;
            this.rFID_FX600_UI = Main_Form._RFID_FX600_UI;
            this.LoadFinishedEvent += Dialog_使用者登入_LoadFinishedEvent;

        }
        public Dialog_使用者登入(string _已登入ID , string _藥名)
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);
         
            this.Load += Dialog_使用者登入_Load;
            this.FormClosed += Dialog_使用者登入_FormClosed;
            this.plC_RJ_Button_登入.MouseDownEventEx += PlC_RJ_Button_登入_MouseDownEventEx;
            this.plC_RJ_Button_確認.MouseDownEventEx += PlC_RJ_Button_確認_MouseDownEventEx;
            this.plC_RJ_Button_取消.MouseDownEventEx += PlC_RJ_Button_取消_MouseDownEventEx;
            plC_RJ_Button_登入.音效 = false;
            plC_RJ_Button_確認.音效 = false;
            plC_RJ_Button_取消.音效 = false;
            this.sQL_DataGridView_人員資料 = Main_Form._sqL_DataGridView_人員資料;
            this.藥名 = _藥名;
            this.已登入ID = _已登入ID;
            this.rFID_FX600_UI = Main_Form._RFID_FX600_UI;
            this.LoadFinishedEvent += Dialog_使用者登入_LoadFinishedEvent;

        }

      

        private void sub_program()
        {
            string text = Main_Form.Function_ReadBacodeScanner01();
            if (text.StringIsEmpty() == false)
            {
                this.Invoke(new Action(delegate
                {
                    Console.WriteLine($"接收到領藥台01[一維碼] {text}");
      
                    List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
                    List<object[]> list_人員資料_buf = new List<object[]>();
                    list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.一維條碼, text);
                    if (list_人員資料_buf.Count == 0) return;
                    string id = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
                    string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
                    Function_登入(id, pwd);
                }));

            }
            text = Main_Form.Function_ReadBacodeScanner02();
            if (text.StringIsEmpty() == false)
            {
                this.Invoke(new Action(delegate
                {
                    Console.WriteLine($"接收到領藥台02[一維碼] {text}");

                    List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
                    List<object[]> list_人員資料_buf = new List<object[]>();
                    list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.一維條碼, text);
                    if (list_人員資料_buf.Count == 0) return;
                    string id = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
                    string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
                    Function_登入(id, pwd);
                }));

            }
            string UID_01 = this.rFID_FX600_UI.Get_RFID_UID(1);
            if (!UID_01.StringIsEmpty() && UID_01.StringToInt32() != 0  && this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {               
                    Console.WriteLine($"接收到領藥台01[RFID] {UID_01}");
                
                    List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
                    List<object[]> list_人員資料_buf = new List<object[]>();
                    list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.卡號, UID_01);
                    if (list_人員資料_buf.Count == 0) return;
                    string id = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
                    string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
                    Function_登入(id, pwd);
                }));
            }
            string UID_02 = this.rFID_FX600_UI.Get_RFID_UID(2);
            if (!UID_02.StringIsEmpty() && UID_02.StringToInt32() != 0 && this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    Console.WriteLine($"接收到領藥台02[RFID] {UID_02}");
                
                    List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
                    List<object[]> list_人員資料_buf = new List<object[]>();
                    list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.卡號, UID_02);
                    UID_02 = "";
                    if (list_人員資料_buf.Count == 0) return;
                    string id = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
                    string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
                    Function_登入(id, pwd);
                }));
            }
            if (Main_Form.flag_指紋辨識_Init)
            {
                FpMatchClass fpMatchClass = Main_Form.fpMatchSoket.GetFeatureOnce();
                if (fpMatchClass == null) return;
                if (fpMatchClass.featureLen == 768)
                {

                    List<object[]> list_人員資料 = Main_Form._sqL_DataGridView_人員資料.SQL_GetAllRows(false);
                    object[] value = null;
                    for (int i = 0; i < list_人員資料.Count; i++)
                    {
                        string feature = list_人員資料[i][(int)enum_人員資料.指紋辨識].ObjectToString();
                        if (Main_Form.fpMatchSoket.Match(fpMatchClass.feature, feature))
                        {
                            value = list_人員資料[i];

                        }
                    }
                    if (value != null)
                    {
                        string ID = value[(int)enum_人員資料.ID].ObjectToString();
                        string PWD = value[(int)enum_人員資料.密碼].ObjectToString();
                        if (Function_登入(ID, PWD) == true) return;

                    }
                    else
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找無符合指紋資訊", 2000);
                        dialog_AlarmForm.ShowDialog();

                    }
                }
            }
       

        }
        Task task;
        private void Dialog_使用者登入_LoadFinishedEvent(EventArgs e)
        {
            textBox_密碼.PassWordChar = true;
            Main_Form.Function_指紋辨識初始化(true,false);
            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();
         
        }
        private bool Function_登入(string ID , string PWD)
        {
            if (ID.ToUpper() == this.已登入ID.ToUpper())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("此ID已登入", 2000);
                dialog_AlarmForm.ShowDialog();
                return false;
            }
            List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_人員資料_buf = new List<object[]>();
            list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.ID, ID);
            if (list_人員資料_buf.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無此帳號", 2000);
                dialog_AlarmForm.ShowDialog();
                return false;
            }
            string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
            if (PWD != pwd)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("密碼錯誤", 2000);
                dialog_AlarmForm.ShowDialog();
                return false;
            }
            _flag_已登入 = true;
            UserName = list_人員資料_buf[0][(int)enum_人員資料.姓名].ObjectToString();
            UserID = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
            Value = list_人員資料_buf[0];
            this.Invoke(new Action(delegate
            {
                rJ_Lable_Title.Text = $"[已登入] {UserName}";
                int cnt = 0;
                while(true)
                {
                    string UID_01 = this.rFID_FX600_UI.Get_RFID_UID(1);
                    string UID_02 = this.rFID_FX600_UI.Get_RFID_UID(2);
                    if (cnt >= 3) break;
                    if ((UID_01.StringIsEmpty() == true || UID_01.StringToInt32() == 0) && (UID_02.StringIsEmpty() == true || UID_02.StringToInt32() == 0))
                    {
                        Console.WriteLine($"UID_01 {UID_01} ");
                        Console.WriteLine($"UID_02 {UID_02} ");
                        cnt++;
                    }
                    System.Threading.Thread.Sleep(100);
                }
                PlC_RJ_Button_確認_MouseDownEventEx(null, null);
            }));
            return true;
        }
        #region Event
        private void PlC_RJ_Button_取消_MouseDownEventEx(MyUI.RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (MyMessageBox.ShowDialog("確認取消?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    Main_Form.fpMatchSoket.Abort();
                    this.DialogResult = DialogResult.No;
                    this.Close();
                    return;
                }
            }));
          
        }
        private void PlC_RJ_Button_確認_MouseDownEventEx(MyUI.RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (_flag_已登入 == false)
                {
                    MyMessageBox.ShowDialog("未登入!無法完成!");
                    return;
                }
                myTimerBasic_覆核完成.TickStop();
                myTimerBasic_覆核完成.StartTickTime(5000);
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
           
        }
        private void PlC_RJ_Button_登入_MouseDownEventEx(MyUI.RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            if (textBox_帳號.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("帳號空白!");
                return;
            }
            Function_登入(textBox_帳號.Text, textBox_密碼.Text);
        }
        private void Dialog_使用者登入_Load(object sender, EventArgs e)
        {

            this.Invoke(new Action(delegate
            {
                if (藥名.StringIsEmpty() == true)
                {
                    this.rJ_Lable_Title.Text = "使用者登入";
                    this.rJ_Lable_藥名.Text = "";
                    this.rJ_Lable_藥名.Visible = false;
                }
                else
                {
                    this.rJ_Lable_藥名.Text = $" 藥名 : { this.藥名}";
                    this.rJ_Lable_藥名.Visible = true;
                }
                
            }));
            if (this.location.X != 0 && this.location.Y != 0)
            {
                this.StartPosition = FormStartPosition.WindowsDefaultLocation;
                base.Location = this.location;
            }
            IsShown = true;
            this.textBox_密碼.KeyPress += TextBox_密碼_KeyPress;
            Main_Form.領藥台_01_卡號 = "";

        }

        private void TextBox_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                PlC_RJ_Button_登入_MouseDownEventEx(null, null);
            }
        }
        private void Dialog_使用者登入_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MyThread_program != null)
            {
                MyThread_program.Abort();
                MyThread_program.Stop();
                MyThread_program = null;
            }
            IsShown = false;
        }
        #endregion
    }
}
