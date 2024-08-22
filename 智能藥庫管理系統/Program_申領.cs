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

namespace 智能藥庫系統
{
    public partial class Main_Form : Form
    {
        private MyThread myThread_申領;

        private void Program_申領_Init()
        {
            myThread_申領 = new MyThread();
            myThread_申領.Add_Method(Program_申領);
            myThread_申領.SetSleepTime(200);
            myThread_申領.AutoRun(true);
            myThread_申領.AutoStop(true);
            myThread_申領.Trigger();

        }
        private int cnt_申領通知 = 0;
        private MyTimerBasic MyTimerBasic_申領通知_背景顏色 = new MyTimerBasic(1000);
        private MyTimerBasic MyTimerBasic_申領通知_開始語音提示 = new MyTimerBasic(3000);
        List<materialRequisitionClass> materialRequisitionClasses_等待撥補 = new List<materialRequisitionClass>();
        List<materialRequisitionClass> materialRequisitionClasses_已通知 = new List<materialRequisitionClass>();
        List<materialRequisitionClass> materialRequisitionClasses_已通知_buf = new List<materialRequisitionClass>();
        private void Program_申領()
        {
            if (cnt_申領通知 == 0)
            {
                List<materialRequisitionClass> materialRequisitionClasses = materialRequisitionClass.get_by_requestTime(API_Server, DateTime.Now.GetStartDate(), DateTime.Now.GetEndDate());
                materialRequisitionClasses = (from temp in materialRequisitionClasses
                                              where temp.狀態 == "等待過帳"
                                              select temp).ToList();
                if (materialRequisitionClasses.Count > 0)
                {
                    materialRequisitionClasses_等待撥補 = materialRequisitionClasses;
                }
                else
                {
                    materialRequisitionClasses_等待撥補 = new List<materialRequisitionClass>();
                }
            }
            if(MyTimerBasic_申領通知_開始語音提示.IsTimeOut())
            {
                bool flag_語音提示 = false;
                for (int i = 0; i < materialRequisitionClasses_等待撥補.Count; i++)
                {
                    materialRequisitionClasses_已通知_buf = (from temp in materialRequisitionClasses_已通知
                                                          where temp.GUID == materialRequisitionClasses_等待撥補[i].GUID
                                                          select temp).ToList();
                    if (materialRequisitionClasses_已通知_buf.Count == 0)
                    {
                        materialRequisitionClasses_已通知.Add(materialRequisitionClasses_等待撥補[i]);
                        flag_語音提示 = true;
                    }
                }
                if (flag_語音提示)
                {
                    Voice.MediaPlayAsync($@"{currentDirectory}\有新申領通知.wav");
                }
            }
       
            if (materialRequisitionClasses_等待撥補.Count > 0)
            {
                if (MyTimerBasic_申領通知_背景顏色.IsTimeOut())
                {
                    if (plC_RJ_Button_申領.ForeColor == Color.Black)
                    {
                        this.Invoke(new Action(delegate
                        {
                            plC_RJ_Button_申領.ForeColor = Color.White;
                            plC_RJ_Button_申領.BackColor = Color.Red;
                            plC_RJ_Button_申領.BackgroundColor = Color.Red;
                            plC_RJ_Button_申領.Invalidate();
                        }));
        
                    }
                    else
                    {
                        this.Invoke(new Action(delegate
                        {
                            plC_RJ_Button_申領.ForeColor = Color.Black;
                            plC_RJ_Button_申領.BackColor = Color.White;
                            plC_RJ_Button_申領.BackgroundColor = Color.White;
 
                            plC_RJ_Button_申領.Invalidate();
                        }));               
                    }

                    MyTimerBasic_申領通知_背景顏色.StartTickTime(1000);
                }
            }
            else
            {
                this.Invoke(new Action(delegate
                {
                    plC_RJ_Button_申領.ForeColor = Color.Black;
                    plC_RJ_Button_申領.BackColor = Color.White;
                    plC_RJ_Button_申領.BackgroundColor = Color.White;

                    plC_RJ_Button_申領.Invalidate();
                }));
            }
        }
    }
}
