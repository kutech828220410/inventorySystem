using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_藥品群組_新增群組 : MyDialog
    {
        public string Value = "";
        public Dialog_藥品群組_新增群組()
        {
            InitializeComponent();
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
        }

        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            if(this.rJ_TextBox_名稱.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("未輸入名稱");
                return;
            }
            medGroupClass medGroupClass = medGroupClass.get_group_by_name(Main_Form.API_Server, this.rJ_TextBox_名稱.Text);
            if (medGroupClass != null && medGroupClass.GUID.StringIsEmpty() == false)
            {
                MyMessageBox.ShowDialog("此名稱已存在");
                return;
            }
            
            medGroupClass = new medGroupClass();
            medGroupClass.名稱 = this.rJ_TextBox_名稱.Text;
            medGroupClass.add_group(Main_Form.API_Server, medGroupClass);

            Value = this.rJ_TextBox_名稱.Text;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
ㄏ