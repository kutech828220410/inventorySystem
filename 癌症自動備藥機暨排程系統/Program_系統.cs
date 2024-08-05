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

namespace 癌症備藥機
{
    public partial class Main_Form : Form
    {
        bool flag_rfiD_FX600_UI_Init = false;
        MyTimer MyTimer_rfiD_FX600_UI_Init = new MyTimer();
        public void Program_系統_Init()
        {
            MyTimer_rfiD_FX600_UI_Init.StartTickTime(5000);

            this.plC_UI_Init.Add_Method(Program_系統);
        }
        public void Program_系統()
        {
            if (MyTimer_rfiD_FX600_UI_Init.IsTimeOut() && flag_rfiD_FX600_UI_Init == false)
            {
                if (myConfigClass.RFID_COMPort.StringIsEmpty() == false)
                {
                    this.Invoke(new Action(delegate 
                    {
                        this.rfiD_FX600_UI.是否自動通訊 = true;
                        this.rfiD_FX600_UI.Init(RFID_FX600lib.RFID_FX600_UI.Baudrate._9600, 2, myConfigClass.RFID_COMPort);
                    }));
                   

                }
                flag_rfiD_FX600_UI_Init = true;
            }
        }
    }
}
