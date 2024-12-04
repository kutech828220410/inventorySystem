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
        private List<medClass> MedClasses = new List<medClass>();
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
            if (Main_Form.VoiceMed != null)
            {
                string voice_name = Main_Form.VoiceMed.name;
                Main_Form.VoiceMed = null;
                if (voice_name.StringIsEmpty()) return;
                List<medClass> medClasses = medClass.get_med_clouds_by_name(Main_Form.API_Server, voice_name);
                Console.WriteLine($"{DateTime.Now.ToDateTimeString()} - 搜尋到<{medClasses.Count}>種藥品");
                if (medClasses.Count > 0)
                {
                    for (int i = 0; i < MedClasses.Count; i++)
                    {
                        Main_Form.Function_儲位亮燈(new Main_Form.LightOn(MedClasses[i].藥品碼, Color.Black));
                    }
                    if (藥碼.StringIsEmpty() == false)
                    {

                    }
                    MedClasses = medClasses;
                    for (int i = 0; i < MedClasses.Count; i++)
                    {
                        Main_Form.LightOn lightOn = new Main_Form.LightOn(MedClasses[i].藥品碼, Color.Blue);
                        lightOn.flag_Refresh_Light = true;
                        Main_Form.Function_儲位亮燈(lightOn);
                    }

                    this.Invoke(new Action(delegate
                    {
                        rJ_Lable_藥品搜尋_藥名.Text = $"{medClasses[0].藥品名稱}";
                        rJ_Lable_藥品搜尋_狀態.BackgroundColor = Color.Green;
                        rJ_Lable_藥品搜尋_狀態.Text = "聲聞辨識成功";

                    }));
                    Task.Run(new Action(delegate
                    {
                        System.Threading.Thread.Sleep(1500);
                        this.Invoke(new Action(delegate
                        {
                            rJ_Lable_藥品搜尋_狀態.BackgroundColor = Color.Red;
                            rJ_Lable_藥品搜尋_狀態.Text = "【刷取辨識藥品條碼】";
                        }));
                    }));
                }
                return;
            }
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
                    for (int k = 0; k < MedClasses.Count; k++)
                    {
                        Main_Form.Function_儲位亮燈(new Main_Form.LightOn(MedClasses[k].藥品碼, Color.Black));
                    }
                    MedClasses = medClasses;
                    for (int k = 0; k < MedClasses.Count; k++)
                    {
                        Main_Form.LightOn lightOn = new Main_Form.LightOn(MedClasses[k].藥品碼, Color.Blue);
                        lightOn.flag_Refresh_Light = true;
                        Main_Form.Function_儲位亮燈(lightOn);
                    }
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
                            rJ_Lable_藥品搜尋_狀態.Text = "【刷取辨識藥品條碼】";
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
            for (int k = 0; k < MedClasses.Count; k++)
            {
                Main_Form.Function_儲位亮燈(new Main_Form.LightOn(MedClasses[k].藥品碼, Color.Black));
            }
        }

        #endregion
    }
}
