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
using DeltaMotor485;
using DrawingClass;
using H_Pannel_lib;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        public PLC_Device PLC_Device_自動備藥_已登入 = new PLC_Device("S4000");
        public PLC_Device PLC_Device_自動備藥_未登入 = new PLC_Device("S4001");
        private void Program_登入畫面_Init()
        {
            this.plC_UI_Init.Add_Method(Program_登入畫面);
        }
        private void Program_登入畫面()
        {
            PLC_Device_自動備藥_未登入.Bool = (!PLC_Device_自動備藥_已登入.Bool);
        }
    }
}
