using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
namespace 癌症備藥機
{
    public partial class Main_Form : Form
    {
        static MySerialPort MySerialPort_Scanner01 = new MySerialPort();
        static MySerialPort MySerialPort_Scanner02 = new MySerialPort();

        private void Program_Scanner_RS232_Init()
        {
            MySerialPort_Scanner01.ConsoleWrite = true;
            MySerialPort_Scanner02.ConsoleWrite = true;
            if (!myConfigClass.Scanner01_COMPort.StringIsEmpty())
            {
                MySerialPort_Scanner01.Init(myConfigClass.Scanner01_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
                if (!MySerialPort_Scanner01.IsConnected)
                {
                    MyMessageBox.ShowDialog("掃碼器[01]初始化失敗!");
                }
            }
            if (!myConfigClass.Scanner02_COMPort.StringIsEmpty())
            {
                MySerialPort_Scanner02.Init(myConfigClass.Scanner02_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
                if (!MySerialPort_Scanner02.IsConnected)
                {
                    MyMessageBox.ShowDialog("掃碼器[02]初始化失敗!");
                }
            }

            this.plC_UI_Init.Add_Method(Program_Scanner_RS232);
        }
        private void Program_Scanner_RS232()
        {

        }

    }
}
