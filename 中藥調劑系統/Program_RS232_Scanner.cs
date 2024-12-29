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
        static private MySerialPort MySerialPort_Scanner01 = new MySerialPort();
        private void Program_RS232_Scanner_Init()
        {
            MySerialPort_Scanner01.ConsoleWrite = true;
            Task.Run(new Action(delegate
            {
                while (true)
                {
                    if (myConfigClass.Scanner01_COMPort.StringIsEmpty()) break;
                    MySerialPort_Scanner01.Init(myConfigClass.Scanner01_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
                    if (MySerialPort_Scanner01.SerialPortOpen())
                    {
                      
                        Console.WriteLine($"{DateTime.Now.ToDateTimeString()} Scanner01_COMPort [{myConfigClass.Scanner01_COMPort}] open sucess!");
                        break;
                    }
                    Console.WriteLine($"{DateTime.Now.ToDateTimeString()} Scanner01_COMPort [{myConfigClass.Scanner01_COMPort}] open failed!");
                    System.Threading.Thread.Sleep(2000);
                }


            }));
            plC_UI_Init.Add_Method(Program_RS232_Scanner);
        }
        private void Program_RS232_Scanner()
        {

        }
    }
}
