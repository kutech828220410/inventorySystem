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
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        DeltaMotor485.Port DeltaMotor485_port = new DeltaMotor485.Port();
        MySerialPort mySerialPort_delta = new MySerialPort();
        private void Program_軸控_Init()
        {
            DeltaMotor485.Communication.ConsoleWrite = true;
            mySerialPort_delta.Init("COM2", 38400, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            DeltaMotor485_port.Init(mySerialPort_delta, new byte[] { 1 });
            DeltaMotor485_port[1].flag_Init = true;
            plC_RJ_Button_Y100.MouseDownEvent += PlC_RJ_Button_Y100_MouseDownEvent;

            this.plC_RJ_Button_冷藏區X軸_PJOG.MouseDownEvent += PlC_RJ_Button_冷藏區X軸_PJOG_MouseDownEvent;
            this.plC_RJ_Button_冷藏區X軸_NJOG.MouseDownEvent += PlC_RJ_Button_冷藏區X軸_NJOG_MouseDownEvent;
            this.plC_RJ_Button_冷藏區X軸_Stop.MouseDownEvent += PlC_RJ_Button_冷藏區X軸_Stop_MouseDownEvent;
            this.plC_UI_Init.Add_Method(Program_軸控);
        }

        private void PlC_RJ_Button_冷藏區X軸_Stop_MouseDownEvent(MouseEventArgs mevent)
        {
       
        }
        private void PlC_RJ_Button_冷藏區X軸_NJOG_MouseDownEvent(MouseEventArgs mevent)
        {
        
        }
        private void PlC_RJ_Button_冷藏區X軸_PJOG_MouseDownEvent(MouseEventArgs mevent)
        {
          
        }

        private void PlC_RJ_Button_Y100_MouseDownEvent(MouseEventArgs mevent)
        {
            bool flag_output = PLC.Device.Get_DeviceFast_Ex(plC_RJ_Button_Y100.寫入元件位置);
            DeltaMotor485_port[1].Servo_on_off(!flag_output);
        }

  
        private void Program_軸控()
        {
            PLC.Device.Set_Device(plC_RJ_Button_Y100.讀取元件位置, DeltaMotor485_port[1].SON);



            plC_RJ_Button_Y100.Bool = DeltaMotor485_port[1].SON;
            plC_Button_X101.Bool = DeltaMotor485_port[1].SRDY;
            plC_Button_X102.Bool = DeltaMotor485_port[1].TOPS;
            plC_NumBox_冷藏區X軸_現在位置.Value = DeltaMotor485_port[1].CommandPosition;
        }
    }
}
