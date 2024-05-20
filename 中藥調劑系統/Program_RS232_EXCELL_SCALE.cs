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
        private MyThread myThread_RS232_EXCELL_SCALE_Init;
        private ExcelScaleLib.Port ExcelScaleLib_Port = new Port();
        private void Program_RS232_EXCELL_SCALE_Init()
        {
            //ExcelScaleLib.Communication.ConsoleWrite = true;

            ExcelScaleLib_Port.Init("COM5", 9600);

            myThread_RS232_EXCELL_SCALE_Init = new MyThread();
            myThread_RS232_EXCELL_SCALE_Init.Add_Method(Program_RS232_EXCELL_SCALE);
            myThread_RS232_EXCELL_SCALE_Init.SetSleepTime(10);
            myThread_RS232_EXCELL_SCALE_Init.AutoRun(true);
            myThread_RS232_EXCELL_SCALE_Init.Trigger();
        }

        private void Program_RS232_EXCELL_SCALE()
        {
            double? weight = ExcelScaleLib_Port.get_weight(Port.enum_unit_type.g);
            if (weight == null) return;
            this.Invoke(new Action(delegate 
            {
                double temp = (double)weight;
                rJ_Lable_實調.Text = temp.ToString("0.00");
            }));
        }
    }
}
