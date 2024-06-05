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
    public partial class Dialog_藥品調入 : MyDialog
    {
        private MyThread MyThread_program;
        public Dialog_藥品調入()
        {
            InitializeComponent();
            this.LoadFinishedEvent += Dialog_藥品調入_LoadFinishedEvent;
            this.rJ_Button_返回.MouseDownEvent += RJ_Button_返回_MouseDownEvent;
            this.FormClosing += Dialog_藥品調入_FormClosing;
        }



        public void Function_SerchByBarCode(string barCode)
        {
            
           List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, barCode);
            if (medClasses.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.get_serversetting_by_type(Main_Form.API_Server, "調劑台");
            List<string> serverNames = (from temp in serverSettingClasses
                                        select temp.設備名稱).Distinct().ToList();
            serverNames.Remove(Main_Form.ServerName);
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < serverNames.Count; i++)
            {
                string serverName = serverNames[i];
                string Code = medClasses[0].藥品碼;
                tasks.Add(Task.Run(new Action(delegate
                {
                    medClass.get_dps_medClass_by_code(Main_Form.API_Server, serverName, Code);
                })));
            }
            Task.WhenAll(tasks).Wait();
        }
        private void sub_program()
        {
            string[] text = Main_Form.Function_ReadBacodeScanner();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != null)
                {
                    LoadingForm.ShowLoadingForm();
                    Function_SerchByBarCode(text[i]);
                    LoadingForm.CloseLoadingForm();
                    break;
                }
            }
   


        }

        private void Dialog_藥品調入_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyThread_program.Stop();
            MyThread_program.Abort();
            MyThread_program = null;
        }
        private void Dialog_藥品調入_LoadFinishedEvent(EventArgs e)
        {

            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();

        }

        private void RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.Close();
            }));
        }


    }
}
