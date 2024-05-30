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
using SQLUI;
using ExcelScaleLib;

namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        private Port.enum_unit_type enum_Unit_Type = Port.enum_unit_type.g;
        private MyThread myThread_RS232_EXCELL_SCALE_Init;
        private ExcelScaleLib.Port ExcelScaleLib_Port = new Port();
        private void Program_RS232_EXCELL_SCALE_Init()
        {
            //ExcelScaleLib.Communication.ConsoleWrite = true;
            Task.Run(new Action(delegate
            {
                while(true)
                {
                    if (myConfigClass.SCALE_COMPort.StringIsEmpty()) break;
                    if (ExcelScaleLib_Port.Init(myConfigClass.SCALE_COMPort, 9600))
                    {
                        myThread_RS232_EXCELL_SCALE_Init = new MyThread();
                        myThread_RS232_EXCELL_SCALE_Init.Add_Method(Program_RS232_EXCELL_SCALE);
                        myThread_RS232_EXCELL_SCALE_Init.SetSleepTime(10);
                        myThread_RS232_EXCELL_SCALE_Init.AutoRun(true);
                        myThread_RS232_EXCELL_SCALE_Init.Trigger();
                        Console.WriteLine($"{DateTime.Now.ToDateTimeString()} ExcelScaleLib_Port [{myConfigClass.SCALE_COMPort}] open sucess!");
                        break;
                    }
                    Console.WriteLine($"{DateTime.Now.ToDateTimeString()} ExcelScaleLib_Port [{myConfigClass.SCALE_COMPort}] open failed!");
                    System.Threading.Thread.Sleep(2000);
                }
          

            }));
  
        }
        private void Program_RS232_EXCELL_SCALE()
        {
            double? weight = ExcelScaleLib_Port.get_weight(enum_Unit_Type);
            if (weight == null) return;
            this.Invoke(new Action(delegate 
            {
                if(enum_Unit_Type == Port.enum_unit_type.g)
                {
                    rJ_Lable_應調單位.Text = "克";
                    rJ_Lable_實調單位.Text = "克";
                }
                else if (enum_Unit_Type == Port.enum_unit_type.dwt)
                {
                    rJ_Lable_應調單位.Text = "錢";
                    rJ_Lable_實調單位.Text = "錢";
                }
                double temp = (double)weight;
                rJ_Lable_實調.Text = temp.ToString("0.00");
            }));
        }
    }
}
