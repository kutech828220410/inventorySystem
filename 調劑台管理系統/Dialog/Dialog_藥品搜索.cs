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

namespace 調劑台管理系統
{
    public partial class Dialog_藥品搜索 : MyDialog
    {
        public static bool IsShown = false;
        private string 藥碼 = "";
        private MyThread myThread = null;
        public Dialog_藥品搜索()
        {
            InitializeComponent();
            this.LoadFinishedEvent += Dialog_藥品搜索_LoadFinishedEvent;
            this.FormClosing += Dialog_藥品搜索_FormClosing;
        }

        #region Function
        private void sub_program()
        {
            string[] brcode_scanner_lines = Main_Form.Function_ReadBacodeScanner();
            for (int i = 0; i < brcode_scanner_lines.Length; i++)
            {
                if (brcode_scanner_lines[i].StringIsEmpty() == false)
                {
                    List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, brcode_scanner_lines[i]);
                    if (medClasses.Count == 0)
                    {
                        this.Invoke(new Action(delegate
                        {
                            rJ_Lable_藥品搜尋_藥名.Text = $"---------";
                        }));
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        continue;
                    }
                    if (藥碼.StringIsEmpty() == false)
                    {
                        Main_Form.Function_儲位亮燈(藥碼, Color.Black);
                    }
                    藥碼 = medClasses[0].藥品碼;
                    Main_Form.Function_儲位亮燈(medClasses[0].藥品碼, Color.Blue);
                    this.Invoke(new Action(delegate 
                    {
                        rJ_Lable_藥品搜尋_藥名.Text = $"({藥碼}){medClasses[0].藥品名稱}";
                        rJ_Lable_藥品搜尋_狀態.BackgroundColor = Color.Green;
                        rJ_Lable_藥品搜尋_狀態.Text = "搜尋成功";
                   
                    }));
                    Task.Run(new Action(delegate
                    {
                        System.Threading.Thread.Sleep(1500);
                        this.Invoke(new Action(delegate
                        {
                            rJ_Lable_藥品搜尋_狀態.BackgroundColor = Color.Red;
                            rJ_Lable_藥品搜尋_狀態.Text = "【刷取藥品條碼】";
                        }));
                    }));
                }
            }
        }
        #endregion
        #region Event
        private void Dialog_藥品搜索_LoadFinishedEvent(EventArgs e)
        {
            IsShown = true;
            myThread = new MyThread();
            myThread.AutoRun(true);
            myThread.AutoStop(false);
            myThread.Add_Method(sub_program);
            myThread.SetSleepTime(50);
            myThread.Trigger();

        }
        private void Dialog_藥品搜索_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsShown = false;
            myThread.Abort();
            myThread = null;
            if (藥碼.StringIsEmpty() == false)
            {
                Main_Form.Function_儲位亮燈(藥碼, Color.Black);
            }
        }

        #endregion
    }
}
