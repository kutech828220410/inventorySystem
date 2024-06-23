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
using H_Pannel_lib;
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
namespace 智能藥庫管理系統
{
    public partial class Main_Form : Form
    {
        public static string API_Server = "http://127.0.0.1:4433";
        public static StorageUI_EPD_266 _storageUI_EPD_266 = null;

        public Main_Form()
        {
       
            InitializeComponent();
            this.Load += Main_Form_Load;
            plC_UI_Init.UI_Finished_Event += PlC_UI_Init_UI_Finished_Event;


            this.plC_RJ_Button_庫存查詢.MouseDownEvent += PlC_RJ_Button_庫存查詢_MouseDownEvent;
            this.plC_RJ_Button_儲位管理.MouseDownEvent += PlC_RJ_Button_儲位管理_MouseDownEvent;
        }

     

        private void Main_Form_Load(object sender, EventArgs e)
        {
            MyMessageBox.音效 = false;
            MyMessageBox.form = this.FindForm();
            LoadingForm.form = this.FindForm();
            MyDialog.form = this.FindForm();
            Dialog_儲位管理.form = this.FindForm();

            this.WindowState = FormWindowState.Maximized;
            this.plC_UI_Init.Run(this.FindForm(), lowerMachine_Panel);
        }
        private void PlC_UI_Init_UI_Finished_Event()
        {
            _storageUI_EPD_266 = this.storageUI_EPD_266;
        }

        private void PlC_RJ_Button_儲位管理_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_儲位管理 dialog_儲位管理 = new Dialog_儲位管理();
            dialog_儲位管理.ShowDialog();
        }
        private void PlC_RJ_Button_庫存查詢_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_庫存查詢 dialog_庫存查詢 = new Dialog_庫存查詢();
            dialog_庫存查詢.ShowDialog();
        }
    }
}
