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
using MyUI;
using HIS_DB_Lib;
using SQLUI;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_醫師疑義處方紀錄表 : MyDialog
    {
        private nearmissClass _nearmissClass = new nearmissClass();

        public nearmissClass Value
        {
            get
            {
                return _nearmissClass;
            }
        }
        private string op_name = "";
        public Dialog_醫師疑義處方紀錄表(nearmissClass nearmissClass , string OP_Name)
        {        
            InitializeComponent();
            rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this._nearmissClass = nearmissClass;
            this.op_name = OP_Name;
            this.LoadFinishedEvent += Dialog_醫師疑義處方紀錄表_LoadFinishedEvent;
        }

        private void Dialog_醫師疑義處方紀錄表_LoadFinishedEvent(EventArgs e)
        {
            List<OrderClass> orderClasses = OrderClass.get_by_barcode(Main_Form.API_Server, _nearmissClass.藥袋條碼);
            if (orderClasses == null)
            {
                MyMessageBox.ShowDialog("錯誤:找無醫令資料");
                this.Close();
                return;
            }
            rJ_Lable_調劑藥師.Text = op_name;
            rJ_Lable_發生時間.Text = DateTime.Now.ToDateTimeString();
            rJ_Lable_錯誤類別.Text = _nearmissClass.錯誤類別;
            rJ_Lable_藥袋類型.Text = _nearmissClass.藥袋類型;
            rJ_Lable_病歷號.Text = _nearmissClass.病歷號;
            rJ_Lable_病人姓名.Text = orderClasses[0].病人姓名;
            rJ_Lable_科別.Text = _nearmissClass.科別;
            rJ_Lable_醫師姓名.Text = _nearmissClass.醫生姓名;
            rJ_Lable_開方時間.Text = _nearmissClass.開方時間;
            rJ_TextBox_簡述事件.Text = _nearmissClass.簡述事件;


        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            _nearmissClass.簡述事件 = rJ_TextBox_簡述事件.Text;
            _nearmissClass.調劑人員 = op_name;
            _nearmissClass.調劑時間 = DateTime.Now.ToDateTimeString_6();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
