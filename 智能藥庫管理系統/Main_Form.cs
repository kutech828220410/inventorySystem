using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
namespace 智能藥庫管理系統
{
    public partial class Main_Form : Form
    {
        public static string API_Server = "http://127.0.0.1:4433";

        public Main_Form()
        {
       
            InitializeComponent();
            this.Load += Main_Form_Load;
            this.plC_RJ_Button_庫存查詢.MouseDownEvent += PlC_RJ_Button_庫存查詢_MouseDownEvent;
        }


        private void Main_Form_Load(object sender, EventArgs e)
        {
            MyMessageBox.音效 = false;

            LoadingForm.form = this.FindForm();
            MyDialog.form = this.FindForm();

            this.WindowState = FormWindowState.Maximized;
            this.plC_UI_Init.Run(this.FindForm(), lowerMachine_Panel);
        }
        private void PlC_RJ_Button_庫存查詢_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_庫存查詢 dialog_庫存查詢 = new Dialog_庫存查詢();
            dialog_庫存查詢.ShowDialog();
        }
    }
}
