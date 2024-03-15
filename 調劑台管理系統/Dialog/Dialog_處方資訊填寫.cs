using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLUI;
using MyUI;
using Basic;
using HIS_DB_Lib;

namespace 調劑台管理系統
{
    public partial class Dialog_處方資訊填寫 : MyDialog
    {
        private transactionsClass _transactionsClass;
        public transactionsClass transactionsClass
        {
            get
            {
                _transactionsClass.病歷號 = 病歷號;
                _transactionsClass.病人姓名 = 病人姓名;
                _transactionsClass.領藥號 = 領藥號;
                _transactionsClass.病房號 = 病房號;

                return _transactionsClass;
            }
            set
            {
                _transactionsClass = value;

                病歷號 = _transactionsClass.病歷號;
                病人姓名 = _transactionsClass.病人姓名;
                領藥號 = _transactionsClass.領藥號;
                病房號 = _transactionsClass.病房號;
            }
        }
        public string 病歷號
        {
            get
            {
                return this.rJ_TextBox_病歷號.Texts;
            }
            set
            {
                this.Invoke(new Action(delegate 
                {
                    this.rJ_TextBox_病歷號.Texts = value;
                }));
            }
        }
        public string 病人姓名
        {
            get
            {
                return this.rJ_TextBox_病人姓名.Texts;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    this.rJ_TextBox_病人姓名.Texts = value;
                }));
            }
        }
        public string 領藥號
        {
            get
            {
                return this.rJ_TextBox_領藥號.Texts;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    this.rJ_TextBox_領藥號.Texts = value;
                }));
            }
        }
        public string 病房號
        {
            get
            {
                return this.rJ_TextBox_病房號.Texts;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    this.rJ_TextBox_病房號.Texts = value;
                }));
            }
        }
        public Dialog_處方資訊填寫(transactionsClass _transactionsClass)
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);
            this._transactionsClass = _transactionsClass;
            this.LoadFinishedEvent += Dialog_處方資訊_LoadFinishedEvent;
            this.Load += Dialog_處方資訊填寫_Load;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
        }

        private void Dialog_處方資訊填寫_Load(object sender, EventArgs e)
        {
            this.transactionsClass = this._transactionsClass;
        }
        private void Dialog_處方資訊_LoadFinishedEvent(EventArgs e)
        {
       
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }

        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }

  
    }
}
