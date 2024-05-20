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
        public static string RFID_UID_01 = "";
        private void Program_RFID_Init()
        {
            Task.Run(new Action(delegate 
            {
                System.Threading.Thread.Sleep(2000);
                this.rfiD_FX600_UI.Init(2, "COM1");
                plC_UI_Init.Add_Method(Program_RFID);
            }));
           

       
        }
        private void Program_RFID()
        {
            RFID_UID_01 = this.rfiD_FX600_UI.Get_RFID_UID(1);
        }
    }
}
