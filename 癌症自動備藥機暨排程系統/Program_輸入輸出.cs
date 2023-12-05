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

namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        MySerialPort mySerial_IO = new MySerialPort();
        public void Program_輸入輸出_Init()
        {
            mySerial_IO.Init("COM1", 115200, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            H_Pannel_lib.Driver_IO_Board driver_IO_Board = new H_Pannel_lib.Driver_IO_Board();
            driver_IO_Board.Init(mySerial_IO, new byte[] { 0, 1, 2 });
            this.plC_Button_Y00.Click += PlC_Button_Y00_Click;
            this.plC_UI_Init.Add_Method(Program_輸入輸出);
        }

        private void PlC_Button_Y00_Click(object sender, EventArgs e)
        {
            
        }

        public void Program_輸入輸出()
        {

        }
    }
}
