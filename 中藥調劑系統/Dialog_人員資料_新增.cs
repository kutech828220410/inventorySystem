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
    public partial class Dialog_人員資料_新增 : MyDialog
    {
        private personPageClass _personPageClass = new personPageClass();
        public personPageClass PersonPageClass
        {
            set
            {
                _personPageClass = value;

            }
        }
        public Dialog_人員資料_新增()
        {
            InitializeComponent();


            this.Load += Dialog_人員資料_新增_Load;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            rJ_Lable_OrangeRed.Click += RJ_Lable_Click;
            rJ_Lable_Orange.Click += RJ_Lable_Click;
            rJ_Lable_Yellow.Click += RJ_Lable_Click;
            rJ_Lable_Lime.Click += RJ_Lable_Click;
            rJ_Lable_Blue.Click += RJ_Lable_Click;
            rJ_Lable_Purple.Click += RJ_Lable_Click;
            rJ_Lable_Aqua.Click += RJ_Lable_Click;

            rJ_TextBox_帳號.Leave += RJ_TextBox_帳號_Leave;
        }
        public Dialog_人員資料_新增(personPageClass personPageClass)
        {
            InitializeComponent();

            this.PersonPageClass = personPageClass;

            this.Load += Dialog_人員資料_新增_Load;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            rJ_Lable_OrangeRed.Click += RJ_Lable_Click;
            rJ_Lable_Orange.Click += RJ_Lable_Click;
            rJ_Lable_Yellow.Click += RJ_Lable_Click;
            rJ_Lable_Lime.Click += RJ_Lable_Click;
            rJ_Lable_Blue.Click += RJ_Lable_Click;
            rJ_Lable_Purple.Click += RJ_Lable_Click;
            rJ_Lable_Aqua.Click += RJ_Lable_Click;

            rJ_TextBox_帳號.Leave += RJ_TextBox_帳號_Leave;

        }

        private void RJ_TextBox_帳號_Leave(object sender, EventArgs e)
        {
            personPageClass personPageClass = personPageClass.serch_by_id(Main_Form.API_Server, this.rJ_TextBox_帳號.Texts);
            if(personPageClass != null)
            {
                Task.Run(new Action(delegate 
                {
                    rJ_TextBox_帳號.SetPlcaeHolder("帳號已註冊過", Color.Red);
                    System.Threading.Thread.Sleep(1500);
                    rJ_TextBox_帳號.SetPlcaeHolder("帳號", Color.DarkGray);

                }));                     
            }
            else
            {
                rJ_TextBox_帳號.PlaceholderColor = Color.DarkGray;
                rJ_TextBox_帳號.PlaceholderText = "帳號";
            }
        }

        private void RJ_Lable_Click(object sender, EventArgs e)
        {
            RJ_Lable rJ_Lable = (RJ_Lable)sender;
            rJ_Lable_OrangeRed.BackgroundColor = (rJ_Lable == rJ_Lable_OrangeRed) ? Color.White: rJ_Lable_OrangeRed.TextColor;
            rJ_Lable_Orange.BackgroundColor = (rJ_Lable == rJ_Lable_Orange) ? Color.White : rJ_Lable_Orange.TextColor;
            rJ_Lable_Yellow.BackgroundColor = (rJ_Lable == rJ_Lable_Yellow) ? Color.White : rJ_Lable_Yellow.TextColor;
            rJ_Lable_Lime.BackgroundColor = (rJ_Lable == rJ_Lable_Lime) ? Color.White : rJ_Lable_Lime.TextColor;
            rJ_Lable_Blue.BackgroundColor = (rJ_Lable == rJ_Lable_Blue) ? Color.White : rJ_Lable_Blue.TextColor;
            rJ_Lable_Purple.BackgroundColor = (rJ_Lable == rJ_Lable_Purple) ? Color.White : rJ_Lable_Purple.TextColor;
            rJ_Lable_Aqua.BackgroundColor = (rJ_Lable == rJ_Lable_Aqua) ? Color.White : rJ_Lable_Aqua.TextColor;
        }

        private void Dialog_人員資料_新增_Load(object sender, EventArgs e)
        {
            this.comboBox_權限等級.SelectedIndex = 0;

            rJ_TextBox_帳號.Text = _personPageClass.ID;
            rJ_TextBox_密碼.PassWordChar = true;
            rJ_TextBox_單位.Text = _personPageClass.單位;
            rJ_TextBox_藥師証號.Text = _personPageClass.藥師證字號;
            comboBox_權限等級.Text = _personPageClass.權限等級;
            if (_personPageClass.顏色 != null)
            {
                if (_personPageClass.顏色.ToColor() == Color.Red) RJ_Lable_Click(rJ_Lable_OrangeRed, null);
                if (_personPageClass.顏色.ToColor() == Color.Orange) RJ_Lable_Click(rJ_Lable_Orange, null);
                if (_personPageClass.顏色.ToColor() == Color.Yellow) RJ_Lable_Click(rJ_Lable_Yellow, null);
                if (_personPageClass.顏色.ToColor() == Color.Lime) RJ_Lable_Click(rJ_Lable_Lime, null);
                if (_personPageClass.顏色.ToColor() == Color.Blue) RJ_Lable_Click(rJ_Lable_Blue, null);
                if (_personPageClass.顏色.ToColor() == Color.Purple) RJ_Lable_Click(rJ_Lable_Purple, null);
                if (_personPageClass.顏色.ToColor() == Color.Aqua) RJ_Lable_Click(rJ_Lable_Aqua, null);
            }
  

        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                if(this.rJ_TextBox_帳號.Texts.StringIsEmpty())
                {
                    Task.Run(new Action(delegate
                    {
                        rJ_TextBox_帳號.SetPlcaeHolder("帳號空白", Color.Red);
                        System.Threading.Thread.Sleep(1500);
                        rJ_TextBox_帳號.SetPlcaeHolder("帳號", Color.DarkGray);

                    }));
                    return;
                }
                DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
    }
}
