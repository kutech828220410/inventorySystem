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
namespace 調劑台管理系統
{
    public partial class Dialog_使用者登入 : Form
    {
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
        private MyThread MyThread_program;
        public static bool IsShown = false;
        private bool _flag_已登入 = false;
        private SQL_DataGridView sQL_DataGridView_人員資料;
        private string 藥名;
        public string UserName = "";
        public string UserID = "";
        public Dialog_使用者登入(string _藥名,SQL_DataGridView _sQL_DataGridView_人員資料)
        {
            InitializeComponent();
            this.Load += Dialog_使用者登入_Load;
            this.FormClosed += Dialog_使用者登入_FormClosed;
            this.plC_RJ_Button_登入.MouseDownEventEx += PlC_RJ_Button_登入_MouseDownEventEx;
            this.plC_RJ_Button_確認.MouseDownEventEx += PlC_RJ_Button_確認_MouseDownEventEx;
            this.plC_RJ_Button_取消.MouseDownEventEx += PlC_RJ_Button_取消_MouseDownEventEx;
            plC_RJ_Button_登入.音效 = false;
            plC_RJ_Button_確認.音效 = false;
            plC_RJ_Button_取消.音效 = false;
            this.sQL_DataGridView_人員資料 = _sQL_DataGridView_人員資料;
            this.藥名 = _藥名;


        }
        private void sub_program()
        {
            if (Form1.領藥台_01_一維碼.StringIsEmpty() == false && this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    Console.WriteLine($"接收到領藥台01[一維碼] {Form1.領藥台_01_一維碼}");
                    Form1.領藥台_01_一維碼 = "";
                    List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
                    List<object[]> list_人員資料_buf = new List<object[]>();
                    list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.一維條碼, Form1.領藥台_01_一維碼);
                    if (list_人員資料_buf.Count == 0) return;
                    string id = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
                    string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
                    Function_登入(id, pwd);
                }));

            }
            if (Form1.領藥台_01_卡號.StringIsEmpty() == false && this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    Console.WriteLine($"接收到領藥台01[RFID] {Form1.領藥台_01_卡號}");
                    Form1.領藥台_01_卡號 = "";
                    List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
                    List<object[]> list_人員資料_buf = new List<object[]>();
                    list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.卡號, Form1.領藥台_01_卡號);
                    if (list_人員資料_buf.Count == 0) return;
                    string id = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
                    string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
                    Function_登入(id, pwd);
                }));

            }
        }


        private void Function_登入(string ID , string PWD)
        {
            this.Invoke(new Action(delegate
            {
    

                List<object[]> list_人員資料 = this.sQL_DataGridView_人員資料.SQL_GetAllRows(false);
                List<object[]> list_人員資料_buf = new List<object[]>();
                list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.ID, ID);
                if (list_人員資料_buf.Count == 0)
                {
                    MyMessageBox.ShowDialog("查無此帳號!");
                    return;
                }
                string pwd = list_人員資料_buf[0][(int)enum_人員資料.密碼].ObjectToString();
                if (PWD != pwd)
                {
                    MyMessageBox.ShowDialog("密碼錯誤!");
                    return;
                }
                _flag_已登入 = true;
                UserName = list_人員資料_buf[0][(int)enum_人員資料.姓名].ObjectToString();
                UserID = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();

                rJ_Lable_Title.Text = $"雙人覆核 [已登入] {UserName}";
            }));
        }
        #region Event
        private void PlC_RJ_Button_取消_MouseDownEventEx(MyUI.RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (MyMessageBox.ShowDialog("確認取消領用此藥品?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
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
                this.rJ_Lable_藥名.Text = $" 藥名 : { this.藥名}";
            }));

            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();

            IsShown = true;

        }
        private void Dialog_使用者登入_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsShown = false;
        }
        #endregion
    }
}
