using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using FpMatchLib;
namespace 調劑台管理系統
{
    public partial class Dialog_指紋建置 : MyDialog
    {
        private MyThread myThread = new MyThread();
        public FpMatchClass Value;
        private string _name = "";
        private string _id = "";

        public Dialog_指紋建置(string name , string id)
        {
            InitializeComponent();
            this._name = name;
            this._id = id;

            List<StepEntity> list = new List<StepEntity>();
            list.Add(new StepEntity("1", "初始化", 1, "等待辨識機待命", eumStepState.Completed, null));
            list.Add(new StepEntity("2", "第一次壓指紋", 2, "請壓上指紋至辨識機", eumStepState.Completed, null));
            list.Add(new StepEntity("3", "第二次壓指紋", 3, "請壓上指紋至辨識機", eumStepState.Waiting, null));
            list.Add(new StepEntity("4", "第三次壓指紋", 4, "請壓上指紋至辨識機", eumStepState.Waiting, null));
            list.Add(new StepEntity("5", "完成", 5, "按下確認存檔", eumStepState.Waiting, null));
            this.stepViewer1.CurrentStep = 1;
            this.stepViewer1.ListDataSource = list;

            this.LoadFinishedEvent += Dialog_指紋建置_LoadFinishedEvent;

            this.plC_RJ_Button_確認完成.MouseDownEvent += PlC_RJ_Button_確認完成_MouseDownEvent;
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
        }

        #region Event
        private void Dialog_指紋建置_LoadFinishedEvent(EventArgs e)
        {
            this.rJ_Lable_姓名.Text = $"姓名 : {_name}";
            this.rJ_Lable_ID.Text = $"ID : {_id}";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(2000);
            if (Main_Form.fpMatchSoket.StateCode != stateCode.READY)
            {
                if (Main_Form.fpMatchSoket.Open() == false)
                {
                    this.Invoke(new Action(delegate
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("指紋模組未啟用", 2000);
                        dialog_AlarmForm.ShowDialog();
                        this.Close();
             
                    }));
                    return;
                }
            }

            while (true)
            {
                if (Main_Form.fpMatchSoket.IsOpen == true) break;
                if (myTimerBasic.IsTimeOut())
                {
                    this.Invoke(new Action(delegate
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("指紋模組未啟用", 2000);
                        dialog_AlarmForm.ShowDialog();
                        this.Close();
                
                    }));
                    return;
                }
                System.Threading.Thread.Sleep(10);
            }
            Task task = Task.Run(new Action(delegate 
            {
                Value = Main_Form.fpMatchSoket.Enroll();
                this.Invoke(new Action(delegate
                {
                    plC_RJ_Button_確認完成.Visible = true;
                }));
            }));
           
            myThread.AutoRun(true);
            myThread.SetSleepTime(50);
            myThread.Add_Method(sub_program);
            myThread.Trigger();
        }
        bool flag_init = false;
        private void sub_program()
        {
            if(flag_init == false)
            {
                System.Threading.Thread.Sleep(1000);
                flag_init = true;
            }
         
            if (Main_Form.fpMatchSoket.StateCode == FpMatchLib.stateCode.RT_NEED_FIRST_SWEEP)
            {
                this.stepViewer1.CurrentStep = 2;
            }
            if (Main_Form.fpMatchSoket.StateCode == FpMatchLib.stateCode.RT_NEED_SECOND_SWEEP)
            {
                this.stepViewer1.CurrentStep = 3;
            }
            if (Main_Form.fpMatchSoket.StateCode == FpMatchLib.stateCode.RT_NEED_THIRD_SWEEP)
            {
                this.stepViewer1.CurrentStep = 4;
            }
            if (Main_Form.fpMatchSoket.StateCode == FpMatchLib.stateCode.READY)
            {
                this.stepViewer1.CurrentStep = 5;
           
            }
            if (Main_Form.fpMatchSoket.StateCode == FpMatchLib.stateCode.RT_NEED_RELEASE_FINGER)
            {
                if (rJ_Lable_離開辨識機顯示.Visible == false)
                {
                    if(this.IsHandleCreated)
                    {
                        this.Invoke(new Action(delegate
                        {
                            rJ_Lable_離開辨識機顯示.Text = "------請將手指離開指紋機------";
                            rJ_Lable_離開辨識機顯示.Visible = true;
                        }));
                    }                   
                }              
            }
            else
            {
                if (rJ_Lable_離開辨識機顯示.Visible == true)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(delegate
                        {
                            rJ_Lable_離開辨識機顯示.Text = "------請將手指離開指紋機------";
                            rJ_Lable_離開辨識機顯示.Visible = false;
                        }));
                    }                 
                }
            }
                
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                Main_Form.fpMatchSoket.Abort();
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
        private void PlC_RJ_Button_確認完成_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        #endregion
    }
}
