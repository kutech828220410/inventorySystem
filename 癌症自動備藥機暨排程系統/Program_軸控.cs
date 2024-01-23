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
        public enum enum_軸號
        {
            冷藏區_X軸 = 1,
            冷藏區_Z軸 = 2,
            常溫區_X軸 = 3,
            常溫區_Z軸 = 4,
            進出盒區_Y軸 = 5,
        }
        PLC_Device PLC_IO_冷藏區X軸_現在位置 = new PLC_Device("R5000");
        PLC_Device PLC_IO_冷藏區Z軸_解剎車 = new PLC_Device("Y13");





        PLC_Device PLC_IO_常溫區X軸_現在位置 = new PLC_Device("R5200");
        PLC_Device PLC_IO_常溫區Z軸_解剎車 = new PLC_Device("Y12");




        DeltaMotor485.Port DeltaMotor485_port_冷藏區_X軸 = new DeltaMotor485.Port();
        DeltaMotor485.Port DeltaMotor485_port_冷藏區_Z軸 = new DeltaMotor485.Port();

        DeltaMotor485.Port DeltaMotor485_port_常溫區_X軸 = new DeltaMotor485.Port();
        DeltaMotor485.Port DeltaMotor485_port_常溫區_Z軸 = new DeltaMotor485.Port();
        DeltaMotor485.Port DeltaMotor485_port_進出盒區_Y軸 = new DeltaMotor485.Port();

        MySerialPort mySerialPort_delta_冷藏區_X軸 = new MySerialPort();
        MySerialPort mySerialPort_delta_冷藏區_Z軸 = new MySerialPort();
        MySerialPort mySerialPort_delta_常溫區_X軸 = new MySerialPort();
        MySerialPort mySerialPort_delta_常溫區_Z軸 = new MySerialPort();
        MySerialPort mySerialPort_delta_進出盒區_Y軸 = new MySerialPort();
        private void Program_軸控_Init()
        {
  
            mySerialPort_delta_冷藏區_X軸.BufferSize = 2048;
            mySerialPort_delta_冷藏區_X軸.Init("COM2", 38400, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.Two);
            DeltaMotor485.Communication.UART_Delay = 5;
            DeltaMotor485.Communication.ConsoleWrite = false;
            DeltaMotor485_port_冷藏區_X軸.Init(mySerialPort_delta_冷藏區_X軸, new byte[] { 1 });
            DeltaMotor485_port_冷藏區_X軸.SleepTime = 10;


            mySerialPort_delta_冷藏區_Z軸.BufferSize = 2048;
            mySerialPort_delta_冷藏區_Z軸.Init("COM3", 38400, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.Two);
            DeltaMotor485.Communication.UART_Delay = 5;
            DeltaMotor485.Communication.ConsoleWrite = false;
            DeltaMotor485_port_冷藏區_Z軸.Init(mySerialPort_delta_冷藏區_Z軸, new byte[] { 2 });
            DeltaMotor485_port_冷藏區_Z軸.SleepTime = 10;


            mySerialPort_delta_常溫區_X軸.BufferSize = 2048;
            mySerialPort_delta_常溫區_X軸.Init("COM4", 38400, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.Two);
            DeltaMotor485.Communication.UART_Delay = 5;
            DeltaMotor485.Communication.ConsoleWrite = false;
            DeltaMotor485_port_常溫區_X軸.Init(mySerialPort_delta_常溫區_X軸, new byte[] { 3 });
            DeltaMotor485_port_常溫區_X軸.SleepTime = 10;


            mySerialPort_delta_常溫區_Z軸.BufferSize = 2048;
            mySerialPort_delta_常溫區_Z軸.Init("COM5", 38400, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.Two);
            DeltaMotor485.Communication.UART_Delay = 5;
            DeltaMotor485.Communication.ConsoleWrite = false;
            DeltaMotor485_port_常溫區_Z軸.Init(mySerialPort_delta_常溫區_Z軸, new byte[] { 4 });
            DeltaMotor485_port_常溫區_Z軸.SleepTime = 10;


            mySerialPort_delta_進出盒區_Y軸.BufferSize = 2048;
            mySerialPort_delta_進出盒區_Y軸.Init("COM6", 38400, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.Two);
            DeltaMotor485.Communication.UART_Delay = 5;
            DeltaMotor485.Communication.ConsoleWrite = false;
            DeltaMotor485_port_進出盒區_Y軸.Init(mySerialPort_delta_進出盒區_Y軸, new byte[] { 5 });
            DeltaMotor485_port_進出盒區_Y軸.SleepTime = 10;

            this.plC_RJ_Button_冷藏區X軸_ServoON.MouseDownEvent += PlC_RJ_Button_冷藏區X軸_ServoON_MouseDownEvent;
            this.plC_RJ_Button_冷藏區X軸_PJOG.MouseDownEvent += PlC_RJ_Button_冷藏區X軸_PJOG_MouseDownEvent;
            this.plC_RJ_Button_冷藏區X軸_NJOG.MouseDownEvent += PlC_RJ_Button_冷藏區X軸_NJOG_MouseDownEvent;
            this.plC_RJ_Button_冷藏區X軸_Stop.MouseDownEvent += PlC_RJ_Button_冷藏區X軸_Stop_MouseDownEvent;
            
            this.plC_RJ_Button_冷藏區Z軸_ServoON.MouseDownEvent += PlC_RJ_Button_冷藏區Z軸_ServoON_MouseDownEvent;
            this.plC_RJ_Button_冷藏區Z軸_PJOG.MouseDownEvent += PlC_RJ_Button_冷藏區Z軸_PJOG_MouseDownEvent;
            this.plC_RJ_Button_冷藏區Z軸_NJOG.MouseDownEvent += PlC_RJ_Button_冷藏區Z軸_NJOG_MouseDownEvent;
            this.plC_RJ_Button_冷藏區Z軸_Stop.MouseDownEvent += PlC_RJ_Button_冷藏區Z軸_Stop_MouseDownEvent;

            this.plC_RJ_Button_常溫區X軸_ServoON.MouseDownEvent += PlC_RJ_Button_常溫區X軸_ServoON_MouseDownEvent;
            this.plC_RJ_Button_常溫區X軸_PJOG.MouseDownEvent += PlC_RJ_Button_常溫區X軸_PJOG_MouseDownEvent;
            this.plC_RJ_Button_常溫區X軸_NJOG.MouseDownEvent += PlC_RJ_Button_常溫區X軸_NJOG_MouseDownEvent;
            this.plC_RJ_Button_常溫區X軸_Stop.MouseDownEvent += PlC_RJ_Button_常溫區X軸_Stop_MouseDownEvent;

            this.plC_RJ_Button_常溫區Z軸_ServoON.MouseDownEvent += PlC_RJ_Button_常溫區Z軸_ServoON_MouseDownEvent;
            this.plC_RJ_Button_常溫區Z軸_PJOG.MouseDownEvent += PlC_RJ_Button_常溫區Z軸_PJOG_MouseDownEvent;
            this.plC_RJ_Button_常溫區Z軸_NJOG.MouseDownEvent += PlC_RJ_Button_常溫區Z軸_NJOG_MouseDownEvent;
            this.plC_RJ_Button_常溫區Z軸_Stop.MouseDownEvent += PlC_RJ_Button_常溫區Z軸_Stop_MouseDownEvent;

            this.plC_RJ_Button_進出盒區Y軸_ServoON.MouseDownEvent += PlC_RJ_Button_進出盒區Y軸_ServoON_MouseDownEvent;
            this.plC_RJ_Button_進出盒區Y軸_PJOG.MouseDownEvent += PlC_RJ_Button_進出盒區Y軸_PJOG_MouseDownEvent;
            this.plC_RJ_Button_進出盒區Y軸_NJOG.MouseDownEvent += PlC_RJ_Button_進出盒區Y軸_NJOG_MouseDownEvent;
            this.plC_RJ_Button_進出盒區Y軸_Stop.MouseDownEvent += PlC_RJ_Button_進出盒區Y軸_Stop_MouseDownEvent;

            this.plC_UI_Init.Add_Method(Program_軸控);
        }

   
        private void PlC_RJ_Button_冷藏區X軸_Stop_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_冷藏區_X軸[1].Stop();
        }
        private void PlC_RJ_Button_冷藏區X軸_NJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_冷藏區_X軸[1].JOG(-plC_NumBox_冷藏區X軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_冷藏區X軸_PJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_冷藏區_X軸[1].JOG(+plC_NumBox_冷藏區X軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_冷藏區X軸_ServoON_MouseDownEvent(MouseEventArgs mevent)
        {
            bool flag_output = PLC.Device.Get_DeviceFast_Ex(plC_RJ_Button_冷藏區X軸_ServoON.寫入元件位置);
            DeltaMotor485_port_冷藏區_X軸[1].Servo_on_off(!flag_output);
        }



        private void PlC_RJ_Button_冷藏區Z軸_Stop_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_冷藏區_Z軸[2].Stop();
        }
        private void PlC_RJ_Button_冷藏區Z軸_NJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_冷藏區_Z軸[2].JOG(-plC_NumBox_冷藏區Z軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_冷藏區Z軸_PJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_冷藏區_Z軸[2].JOG(+plC_NumBox_冷藏區Z軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_冷藏區Z軸_ServoON_MouseDownEvent(MouseEventArgs mevent)
        {
            bool flag_output = PLC.Device.Get_DeviceFast_Ex(plC_RJ_Button_冷藏區Z軸_ServoON.寫入元件位置);
            DeltaMotor485_port_冷藏區_Z軸[2].Servo_on_off(!flag_output);
        }


        private void PlC_RJ_Button_常溫區X軸_Stop_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_常溫區_X軸[3].Stop();
        }
        private void PlC_RJ_Button_常溫區X軸_NJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_常溫區_X軸[3].JOG(-plC_NumBox_常溫區X軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_常溫區X軸_PJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_常溫區_X軸[3].JOG(+plC_NumBox_常溫區X軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_常溫區X軸_ServoON_MouseDownEvent(MouseEventArgs mevent)
        {
            bool flag_output = PLC.Device.Get_DeviceFast_Ex(plC_RJ_Button_常溫區X軸_ServoON.寫入元件位置);
            DeltaMotor485_port_常溫區_X軸[3].Servo_on_off(!flag_output);
        }


        private void PlC_RJ_Button_常溫區Z軸_Stop_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_常溫區_Z軸[4].Stop();
        }
        private void PlC_RJ_Button_常溫區Z軸_NJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_常溫區_Z軸[4].JOG(-plC_NumBox_常溫區Z軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_常溫區Z軸_PJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_常溫區_Z軸[4].JOG(+plC_NumBox_常溫區Z軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_常溫區Z軸_ServoON_MouseDownEvent(MouseEventArgs mevent)
        {
            bool flag_output = PLC.Device.Get_DeviceFast_Ex(plC_RJ_Button_常溫區Z軸_ServoON.寫入元件位置);
            DeltaMotor485_port_常溫區_Z軸[4].Servo_on_off(!flag_output);
        }


        private void PlC_RJ_Button_進出盒區Y軸_Stop_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_進出盒區_Y軸[5].Stop();
        }
        private void PlC_RJ_Button_進出盒區Y軸_NJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_進出盒區_Y軸[5].JOG(-plC_NumBox_進出盒區Y軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_進出盒區Y軸_PJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port_進出盒區_Y軸[5].JOG(+plC_NumBox_進出盒區Y軸_JOG速度.Value);
        }
        private void PlC_RJ_Button_進出盒區Y軸_ServoON_MouseDownEvent(MouseEventArgs mevent)
        {
            bool flag_output = PLC.Device.Get_DeviceFast_Ex(plC_RJ_Button_進出盒區Y軸_ServoON.寫入元件位置);
            DeltaMotor485_port_進出盒區_Y軸[5].Servo_on_off(!flag_output);
        }

        private void Program_軸控()
        {
            PLC.Device.Set_Device(plC_RJ_Button_冷藏區X軸_ServoON.讀取元件位置, DeltaMotor485_port_冷藏區_X軸[1].SON);
            DeltaMotor485_port_冷藏區_X軸[1].Read485_Enable = true;
            plC_RJ_Button_冷藏區X軸_ServoON.Bool = DeltaMotor485_port_冷藏區_X軸[1].SON;
            plC_Button_冷藏區X軸_Ready.Bool = DeltaMotor485_port_冷藏區_X軸[1].SRDY;
            plC_Button_冷藏區X軸_零速度檢出.Bool = DeltaMotor485_port_冷藏區_X軸[1].ZSPD;
            plC_Button_冷藏區X軸_原點.Bool = DeltaMotor485_port_冷藏區_X軸[1].DI.ORGP;
            plC_Button_冷藏區X軸_正極限.Bool = DeltaMotor485_port_冷藏區_X軸[1].DI.PL;
            plC_Button_冷藏區X軸_ALARM.Bool = DeltaMotor485_port_冷藏區_X軸[1].ALRM;      
            plC_NumBox_冷藏區X軸_現在位置.Value = DeltaMotor485_port_冷藏區_X軸[1].CommandPosition;

            PLC.Device.Set_Device(plC_RJ_Button_冷藏區Z軸_ServoON.讀取元件位置, DeltaMotor485_port_冷藏區_Z軸[2].SON);
            DeltaMotor485_port_冷藏區_Z軸[2].Read485_Enable = true;
            plC_RJ_Button_冷藏區Z軸_ServoON.Bool = DeltaMotor485_port_冷藏區_Z軸[2].SON;
            plC_Button_冷藏區Z軸_Ready.Bool = DeltaMotor485_port_冷藏區_Z軸[2].SRDY;
            plC_Button_冷藏區Z軸_零速度檢出.Bool = DeltaMotor485_port_冷藏區_Z軸[2].ZSPD;
            plC_Button_冷藏區Z軸_原點.Bool = DeltaMotor485_port_冷藏區_Z軸[2].DI.ORGP;
            plC_Button_冷藏區Z軸_正極限.Bool = DeltaMotor485_port_冷藏區_Z軸[2].DI.PL;
            plC_Button_冷藏區Z軸_ALARM.Bool = DeltaMotor485_port_冷藏區_Z軸[2].ALRM;
            plC_NumBox_冷藏區Z軸_現在位置.Value = DeltaMotor485_port_冷藏區_Z軸[2].CommandPosition;


            PLC.Device.Set_Device(plC_RJ_Button_常溫區X軸_ServoON.讀取元件位置, DeltaMotor485_port_常溫區_X軸[3].SON);
            DeltaMotor485_port_常溫區_X軸[3].Read485_Enable = true;
            plC_RJ_Button_常溫區X軸_ServoON.Bool = DeltaMotor485_port_常溫區_X軸[3].SON;
            plC_Button_常溫區X軸_Ready.Bool = DeltaMotor485_port_常溫區_X軸[3].SRDY;
            plC_Button_常溫區X軸_零速度檢出.Bool = DeltaMotor485_port_常溫區_X軸[3].ZSPD;
            plC_Button_常溫區X軸_原點.Bool = DeltaMotor485_port_常溫區_X軸[3].DI.ORGP;
            plC_Button_常溫區X軸_正極限.Bool = DeltaMotor485_port_常溫區_X軸[3].DI.PL;
            plC_Button_常溫區X軸_ALARM.Bool = DeltaMotor485_port_常溫區_X軸[3].ALRM;
            plC_NumBox_常溫區X軸_現在位置.Value = DeltaMotor485_port_常溫區_X軸[3].CommandPosition;

            PLC.Device.Set_Device(plC_RJ_Button_常溫區Z軸_ServoON.讀取元件位置, DeltaMotor485_port_常溫區_Z軸[4].SON);
            DeltaMotor485_port_常溫區_Z軸[4].Read485_Enable = true;
            plC_RJ_Button_常溫區Z軸_ServoON.Bool = DeltaMotor485_port_常溫區_Z軸[4].SON;
            plC_Button_常溫區Z軸_Ready.Bool = DeltaMotor485_port_常溫區_Z軸[4].SRDY;
            plC_Button_常溫區Z軸_零速度檢出.Bool = DeltaMotor485_port_常溫區_Z軸[4].ZSPD;
            plC_Button_常溫區Z軸_原點.Bool = DeltaMotor485_port_常溫區_Z軸[4].DI.ORGP;
            plC_Button_常溫區Z軸_正極限.Bool = DeltaMotor485_port_常溫區_Z軸[4].DI.PL;
            plC_Button_常溫區Z軸_ALARM.Bool = DeltaMotor485_port_常溫區_Z軸[4].ALRM;
            plC_NumBox_常溫區Z軸_現在位置.Value = DeltaMotor485_port_常溫區_Z軸[4].CommandPosition;

            PLC.Device.Set_Device(plC_RJ_Button_進出盒區Y軸_ServoON.讀取元件位置, DeltaMotor485_port_進出盒區_Y軸[5].SON);
            DeltaMotor485_port_進出盒區_Y軸[5].Read485_Enable = true;
            plC_RJ_Button_進出盒區Y軸_ServoON.Bool = DeltaMotor485_port_進出盒區_Y軸[5].SON;
            plC_Button_進出盒區Y軸_Ready.Bool = DeltaMotor485_port_進出盒區_Y軸[5].SRDY;
            plC_Button_進出盒區Y軸_零速度檢出.Bool = DeltaMotor485_port_進出盒區_Y軸[5].ZSPD;
            plC_Button_進出盒區Y軸_原點.Bool = DeltaMotor485_port_進出盒區_Y軸[5].DI.ORGP;
            plC_Button_進出盒區Y軸_正極限.Bool = DeltaMotor485_port_進出盒區_Y軸[5].DI.PL;
            plC_Button_進出盒區Y軸_ALARM.Bool = DeltaMotor485_port_進出盒區_Y軸[5].ALRM;
            plC_NumBox_進出盒區Y軸_現在位置.Value = DeltaMotor485_port_進出盒區_Y軸[5].CommandPosition;


            sub_Program_軸控初始化();

            sub_Program_冷藏區復歸();
            sub_Program_冷藏區X軸復歸();
            sub_Program_冷藏區Z軸復歸();

            sub_Program_冷藏區輸送帶前進();
            sub_Program_冷藏區輸送帶後退();

            sub_Program_冷藏區輸送門開啟();
            sub_Program_冷藏區輸送門關閉();

            sub_Program_冷藏區X軸_絕對位置移動();
            sub_Program_冷藏區Z軸_絕對位置移動();
            sub_Program_冷藏區_移動至待命位置();
            sub_Program_冷藏區_移動至與常溫區藥盒傳接位置();
            sub_Program_冷藏區_移動至零點位置();
      

            sub_Program_常溫區復歸();
            sub_Program_常溫區X軸復歸();
            sub_Program_常溫區Z軸復歸();

            sub_Program_常溫區輸送帶前進();
            sub_Program_常溫區輸送帶後退();

            sub_Program_常溫區輸送門開啟();
            sub_Program_常溫區輸送門關閉();

            sub_Program_常溫區X軸_絕對位置移動();
            sub_Program_常溫區Z軸_絕對位置移動();
            sub_Program_常溫區_移動至冷藏待命位置();
            sub_Program_常溫區_移動至與冷藏區藥盒傳接位置();
            sub_Program_常溫區_移動至零點位置();
   

            sub_Program_進出盒區Y軸復歸();
        }


        private void ServoON(enum_軸號 enum_軸號 , bool state)
        {
            DeltaMotor485.Driver_DO driver_DO = null;
            if (enum_軸號 == enum_軸號.冷藏區_X軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_X軸[1];
            }
            else if (enum_軸號 == enum_軸號.冷藏區_Z軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_Z軸[2];
            }
            else if (enum_軸號 == enum_軸號.常溫區_X軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_X軸[3];
            }
            else if (enum_軸號 == enum_軸號.常溫區_Z軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_Z軸[4];
            }
            else if (enum_軸號 == enum_軸號.進出盒區_Y軸)
            {
                driver_DO = DeltaMotor485_port_進出盒區_Y軸[5];
            }
            driver_DO.Servo_on_off(state);
        }
        private void ServoInit(enum_軸號 enum_軸號)
        {
            DeltaMotor485.Driver_DO driver_DO = null;
            if (enum_軸號 == enum_軸號.冷藏區_X軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_X軸[1];
            }
            else if (enum_軸號 == enum_軸號.冷藏區_Z軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_Z軸[2];
            }
            else if (enum_軸號 == enum_軸號.常溫區_X軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_X軸[3];
            }
            else if (enum_軸號 == enum_軸號.常溫區_Z軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_Z軸[4];
            }
            else if (enum_軸號 == enum_軸號.進出盒區_Y軸)
            {
                driver_DO = DeltaMotor485_port_進出盒區_Y軸[5];
            }
            driver_DO.flag_Init = true;
        }
        private void Servo_JOG(enum_軸號 enum_軸號 , int speed_rpm)
        {
            DeltaMotor485.Driver_DO driver_DO = null;
            if (enum_軸號 == enum_軸號.冷藏區_X軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_X軸[1];
            }
            else if (enum_軸號 == enum_軸號.冷藏區_Z軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_Z軸[2];
            }
            else if (enum_軸號 == enum_軸號.常溫區_X軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_X軸[3];
            }
            else if (enum_軸號 == enum_軸號.常溫區_Z軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_Z軸[4];
            }
            else if (enum_軸號 == enum_軸號.進出盒區_Y軸)
            {
                driver_DO = DeltaMotor485_port_進出盒區_Y軸[5];
            }
            driver_DO.JOG(speed_rpm);
        }
        private void Servo_Stop(enum_軸號 enum_軸號)
        {
            DeltaMotor485.Driver_DO driver_DO = null;
            if (enum_軸號 == enum_軸號.冷藏區_X軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_X軸[1];
            }
            else if (enum_軸號 == enum_軸號.冷藏區_Z軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_Z軸[2];
            }
            else if (enum_軸號 == enum_軸號.常溫區_X軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_X軸[3];
            }
            else if (enum_軸號 == enum_軸號.常溫區_Z軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_Z軸[4];
            }
            else if (enum_軸號 == enum_軸號.進出盒區_Y軸)
            {
                driver_DO = DeltaMotor485_port_進出盒區_Y軸[5];
            }
            driver_DO.Stop();
            
        }
        private bool Servo_State(enum_軸號 enum_軸號 , DeltaMotor485.enum_DO enum_DO)
        {
            DeltaMotor485.Driver_DO driver_DO = null;
            if (enum_軸號 == enum_軸號.冷藏區_X軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_X軸[1];
            }
            else if (enum_軸號 == enum_軸號.冷藏區_Z軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_Z軸[2];
            }
            else if (enum_軸號 == enum_軸號.常溫區_X軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_X軸[3];
            }
            else if (enum_軸號 == enum_軸號.常溫區_Z軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_Z軸[4];
            }
            else if (enum_軸號 == enum_軸號.進出盒區_Y軸)
            {
                driver_DO = DeltaMotor485_port_進出盒區_Y軸[5];
            }

            if (enum_DO == enum_DO.SON)
            {
                return driver_DO.SON;
            }
            else if (enum_DO == enum_DO.SRDY)
            {
                return driver_DO.SRDY;
            }
            else if (enum_DO == enum_DO.ZSPD)
            {
                return driver_DO.ZSPD;
            }
            else if (enum_DO == enum_DO.ALRM)
            {
                return driver_DO.ALRM;
            }
            else if (enum_DO == enum_DO.HOME)
            {
                return driver_DO.HOME;
            }

            return false;
        }
        private void Servo_DDRVA(enum_軸號 enum_軸號, int position, int speed, int acc )
        {
            DeltaMotor485.Driver_DO driver_DO = null;
            if (enum_軸號 == enum_軸號.冷藏區_X軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_X軸[1];
            }
            else if (enum_軸號 == enum_軸號.冷藏區_Z軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_Z軸[2];
            }
            else if (enum_軸號 == enum_軸號.常溫區_X軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_X軸[3];
            }
            else if (enum_軸號 == enum_軸號.常溫區_Z軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_Z軸[4];
            }
            else if (enum_軸號 == enum_軸號.進出盒區_Y軸)
            {
                driver_DO = DeltaMotor485_port_進出盒區_Y軸[5];
            }
            driver_DO.DDRVA(position, speed, acc);
        }
        private bool Servo_DDRVA(enum_軸號 enum_軸號)
        {
            DeltaMotor485.Driver_DO driver_DO = null;
            if (enum_軸號 == enum_軸號.冷藏區_X軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_X軸[1];
            }
            else if (enum_軸號 == enum_軸號.冷藏區_Z軸)
            {
                driver_DO = DeltaMotor485_port_冷藏區_Z軸[2];
            }
            else if (enum_軸號 == enum_軸號.常溫區_X軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_X軸[3];
            }
            else if (enum_軸號 == enum_軸號.常溫區_Z軸)
            {
                driver_DO = DeltaMotor485_port_常溫區_Z軸[4];
            }
            else if (enum_軸號 == enum_軸號.進出盒區_Y軸)
            {
                driver_DO = DeltaMotor485_port_進出盒區_Y軸[5];
            }
            return driver_DO.DDRVA_Done;
        }

        #region PLC_軸控初始化
        PLC_Device PLC_Device_軸控初始化 = new PLC_Device("");
        PLC_Device PLC_Device_軸控初始化_OK = new PLC_Device("");
        MyTimer MyTimer_軸控初始化_結束延遲 = new MyTimer();
        MyTimer MyTimer_軸控初始化_開始延遲 = new MyTimer();
        int cnt_Program_軸控初始化 = 65534;
        void sub_Program_軸控初始化()
        {
            if (cnt_Program_軸控初始化 == 65534)
            {
                this.MyTimer_軸控初始化_結束延遲.TickStop();
                this.MyTimer_軸控初始化_結束延遲.StartTickTime(3000);
                this.MyTimer_軸控初始化_開始延遲.TickStop();
                this.MyTimer_軸控初始化_開始延遲.StartTickTime(3000);
                PLC_Device_軸控初始化.SetComment("PLC_軸控初始化");
                PLC_Device_軸控初始化_OK.SetComment("PLC_軸控初始化_OK");
                cnt_Program_軸控初始化 = 65535;
            }
            if(MyTimer_軸控初始化_開始延遲.IsTimeOut())
            {
                MyTimer_軸控初始化_開始延遲.Stop = true;
                PLC_Device_軸控初始化.Bool = true;
            }
            if (cnt_Program_軸控初始化 == 65535) cnt_Program_軸控初始化 = 1;
            if (cnt_Program_軸控初始化 == 1) cnt_Program_軸控初始化_檢查按下(ref cnt_Program_軸控初始化);
            if (cnt_Program_軸控初始化 == 2) cnt_Program_軸控初始化_初始化(ref cnt_Program_軸控初始化);
            if (cnt_Program_軸控初始化 == 3) cnt_Program_軸控初始化 = 65500;
            if (cnt_Program_軸控初始化 > 1) cnt_Program_軸控初始化_檢查放開(ref cnt_Program_軸控初始化);

            if (cnt_Program_軸控初始化 == 65500)
            {
                this.MyTimer_軸控初始化_結束延遲.TickStop();
                this.MyTimer_軸控初始化_結束延遲.StartTickTime(3000);
                PLC_Device_軸控初始化.Bool = false;
                PLC_Device_軸控初始化_OK.Bool = true;
                cnt_Program_軸控初始化 = 65535;
            }
        }
        void cnt_Program_軸控初始化_檢查按下(ref int cnt)
        {
            if (PLC_Device_軸控初始化.Bool) cnt++;
        }
        void cnt_Program_軸控初始化_檢查放開(ref int cnt)
        {
            if (!PLC_Device_軸控初始化.Bool) cnt = 65500;
        }
        void cnt_Program_軸控初始化_初始化(ref int cnt)
        {

            ServoInit(enum_軸號.冷藏區_X軸);
            ServoON(enum_軸號.冷藏區_X軸, true);

            ServoInit(enum_軸號.冷藏區_Z軸);
            ServoON(enum_軸號.冷藏區_Z軸, true);
            PLC_IO_冷藏區Z軸_解剎車.Bool = true;


            ServoInit(enum_軸號.常溫區_X軸) ;
            ServoON(enum_軸號.常溫區_X軸, true);

            ServoInit(enum_軸號.常溫區_Z軸);
            ServoON(enum_軸號.常溫區_Z軸, true);
            PLC_IO_常溫區Z軸_解剎車.Bool = true;


            ServoInit(enum_軸號.進出盒區_Y軸);
            ServoON(enum_軸號.進出盒區_Y軸, true);
            cnt++;
        }







        #endregion

        #region PLC_冷藏區復歸
        PLC_Device PLC_Device_冷藏區復歸 = new PLC_Device("S5012");
        PLC_Device PLC_Device_冷藏區復歸_OK = new PLC_Device("");
        Task Task_冷藏區復歸;
        MyTimer MyTimer_冷藏區復歸_向前JOG時間 = new MyTimer();
        MyTimer MyTimer_冷藏區復歸_結束延遲 = new MyTimer();
        MyTimer MyTimer_冷藏區復歸_開始延遲 = new MyTimer();
        int cnt_Program_冷藏區復歸 = 65534;
        void sub_Program_冷藏區復歸()
        {
            if (cnt_Program_冷藏區復歸 == 65534)
            {
                this.MyTimer_冷藏區復歸_結束延遲.StartTickTime(10000);
                this.MyTimer_冷藏區復歸_開始延遲.StartTickTime(10000);
                PLC_Device_冷藏區復歸.SetComment("PLC_冷藏區復歸");
                PLC_Device_冷藏區復歸_OK.SetComment("PLC_冷藏區復歸_OK");
                PLC_Device_冷藏區復歸.Bool = false;
                cnt_Program_冷藏區復歸 = 65535;
            }
            if (cnt_Program_冷藏區復歸 == 65535) cnt_Program_冷藏區復歸 = 1;
            if (cnt_Program_冷藏區復歸 == 1) cnt_Program_冷藏區復歸_檢查按下(ref cnt_Program_冷藏區復歸);
            if (cnt_Program_冷藏區復歸 == 2) cnt_Program_冷藏區復歸_初始化(ref cnt_Program_冷藏區復歸);
            if (cnt_Program_冷藏區復歸 == 3) cnt_Program_冷藏區復歸_X軸復歸(ref cnt_Program_冷藏區復歸);
            if (cnt_Program_冷藏區復歸 == 4) cnt_Program_冷藏區復歸_等待X軸復歸完成(ref cnt_Program_冷藏區復歸);
            if (cnt_Program_冷藏區復歸 == 5) cnt_Program_冷藏區復歸_Z軸復歸(ref cnt_Program_冷藏區復歸);
            if (cnt_Program_冷藏區復歸 == 6) cnt_Program_冷藏區復歸_等待Z軸復歸完成(ref cnt_Program_冷藏區復歸);
            if (cnt_Program_冷藏區復歸 == 7) cnt_Program_冷藏區復歸 = 65500;
            if (cnt_Program_冷藏區復歸 > 1) cnt_Program_冷藏區復歸_檢查放開(ref cnt_Program_冷藏區復歸);

            if (cnt_Program_冷藏區復歸 == 65500)
            {
                this.MyTimer_冷藏區復歸_結束延遲.TickStop();
                this.MyTimer_冷藏區復歸_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區X軸復歸.Bool = false;
                PLC_Device_冷藏區Z軸復歸.Bool = false;
                PLC_Device_冷藏區復歸.Bool = false;
                cnt_Program_冷藏區復歸 = 65535;
            }
        }
        void cnt_Program_冷藏區復歸_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區復歸.Bool) cnt++;
        }
        void cnt_Program_冷藏區復歸_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區復歸.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區復歸_初始化(ref int cnt)
        {
            PLC_Device_冷藏區復歸_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_冷藏區復歸_X軸復歸(ref int cnt)
        {
            if (!PLC_Device_冷藏區X軸復歸.Bool)
            {
                PLC_Device_冷藏區X軸復歸.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區復歸_等待X軸復歸完成(ref int cnt)
        {
            if (!PLC_Device_冷藏區X軸復歸.Bool)
            {
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區復歸_Z軸復歸(ref int cnt)
        {
            if (!PLC_Device_冷藏區Z軸復歸.Bool)
            {
                PLC_Device_冷藏區Z軸復歸.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區復歸_等待Z軸復歸完成(ref int cnt)
        {
            if (!PLC_Device_冷藏區Z軸復歸.Bool)
            {
                cnt++;
                return;
            }
        }


        #endregion
        #region PLC_冷藏區X軸復歸
        PLC_Device PLC_Device_冷藏區X軸復歸 = new PLC_Device("S5010");
        PLC_Device PLC_Device_冷藏區X軸復歸_OK = new PLC_Device("");
        Task Task_冷藏區X軸復歸;
        MyTimer MyTimer_冷藏區X軸復歸_向前JOG時間 = new MyTimer();
        MyTimer MyTimer_冷藏區X軸復歸_結束延遲 = new MyTimer();
        MyTimer MyTimer_冷藏區X軸復歸_開始延遲 = new MyTimer();
        int cnt_Program_冷藏區X軸復歸 = 65534;
        void sub_Program_冷藏區X軸復歸()
        {
            if (cnt_Program_冷藏區X軸復歸 == 65534)
            {
                this.MyTimer_冷藏區X軸復歸_結束延遲.StartTickTime(10000);
                this.MyTimer_冷藏區X軸復歸_開始延遲.StartTickTime(10000);
                PLC_Device_冷藏區X軸復歸.SetComment("PLC_冷藏區X軸復歸");
                PLC_Device_冷藏區X軸復歸_OK.SetComment("PLC_冷藏區X軸復歸_OK");
                PLC_Device_冷藏區X軸復歸.Bool = false;
                cnt_Program_冷藏區X軸復歸 = 65535;
            }
            if (cnt_Program_冷藏區X軸復歸 == 65535) cnt_Program_冷藏區X軸復歸 = 1;
            if (cnt_Program_冷藏區X軸復歸 == 1) cnt_Program_冷藏區X軸復歸_檢查按下(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 2) cnt_Program_冷藏區X軸復歸_初始化(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 3) cnt_Program_冷藏區X軸復歸_輸送帶後退(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 4) cnt_Program_冷藏區X軸復歸_等待輸送帶後退(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 5) cnt_Program_冷藏區X軸復歸_向前JOG(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 6) cnt_Program_冷藏區X軸復歸_向前JOG時間到達(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 7) cnt_Program_冷藏區X軸復歸_離開正極限(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 8) cnt_Program_冷藏區X軸復歸_檢查馬達停止(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 9) cnt_Program_冷藏區X軸復歸_開始復歸(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 10) cnt_Program_冷藏區X軸復歸_檢查HOME_OFF(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 11) cnt_Program_冷藏區X軸復歸_檢查復歸完成(ref cnt_Program_冷藏區X軸復歸);
            if (cnt_Program_冷藏區X軸復歸 == 12) cnt_Program_冷藏區X軸復歸 = 65500;
            if (cnt_Program_冷藏區X軸復歸 > 1) cnt_Program_冷藏區X軸復歸_檢查放開(ref cnt_Program_冷藏區X軸復歸);

            if (cnt_Program_冷藏區X軸復歸 == 65500)
            {
                this.MyTimer_冷藏區X軸復歸_結束延遲.TickStop();
                this.MyTimer_冷藏區X軸復歸_結束延遲.StartTickTime(10000);
                Servo_Stop(enum_軸號.冷藏區_X軸);
                plC_RJ_Button_冷藏區X軸_復歸.Bool = false;
           
                cnt_Program_冷藏區X軸復歸 = 65535;
            }
        }
        void cnt_Program_冷藏區X軸復歸_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸復歸.Bool) cnt++;
        }
        void cnt_Program_冷藏區X軸復歸_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區X軸復歸.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區X軸復歸_初始化(ref int cnt)
        {
            PLC_Device_冷藏區X軸復歸_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_冷藏區X軸復歸_輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_冷藏區_輸送帶後退.Bool)
            {
                plC_RJ_Button_冷藏區_輸送帶後退.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區X軸復歸_等待輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_冷藏區_輸送帶後退.Bool)
            {
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區X軸復歸_向前JOG(ref int cnt)
        {
            if (plC_Button_冷藏區X軸_正極限.Bool)
            {
                cnt++;
                return;
            }
            Servo_JOG(enum_軸號.冷藏區_X軸, 100);
            MyTimer_冷藏區X軸復歸_向前JOG時間.TickStop();
            MyTimer_冷藏區X軸復歸_向前JOG時間.StartTickTime(2000);
            cnt++;
        }
        void cnt_Program_冷藏區X軸復歸_向前JOG時間到達(ref int cnt)
        {
            if (plC_Button_冷藏區X軸_正極限.Bool)
            {
                Servo_JOG(enum_軸號.冷藏區_X軸, -100);
                cnt++;
                return;
            }
            if(MyTimer_冷藏區X軸復歸_向前JOG時間.IsTimeOut())
            {       
                Servo_Stop(enum_軸號.冷藏區_X軸);
                cnt++;
            }
      
        }
        void cnt_Program_冷藏區X軸復歸_離開正極限(ref int cnt)
        {
            if (!plC_Button_冷藏區X軸_正極限.Bool)
            {
                Servo_Stop(enum_軸號.冷藏區_X軸);
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區X軸復歸_檢查馬達停止(ref int cnt)
        {
            if (Servo_State(enum_軸號.冷藏區_X軸, enum_DO.ZSPD) == true)
            {
                cnt++;
            }
        }
        void cnt_Program_冷藏區X軸復歸_開始復歸(ref int cnt)
        {
            DeltaMotor485_port_冷藏區_X軸[1].Home(enum_Direction.CCW, true, 250, 10, 50, 50, plC_NumBox_冷藏區X軸_復歸偏移.Value, 200, 50);
            cnt++;
        }
        void cnt_Program_冷藏區X軸復歸_檢查HOME_OFF(ref int cnt)
        {
            if (Servo_State(enum_軸號.冷藏區_X軸, enum_DO.HOME) == false)
            {
                cnt++;
            }
        }
        void cnt_Program_冷藏區X軸復歸_檢查復歸完成(ref int cnt)
        {
            if (Servo_State(enum_軸號.冷藏區_X軸, enum_DO.HOME) == true && Servo_State(enum_軸號.冷藏區_X軸, enum_DO.ZSPD) == true)
            {
                plC_RJ_Button_冷藏區X軸_已完成復歸.Bool = true;
                cnt++;
            }
        }


        #endregion
        #region PLC_冷藏區Z軸復歸
        PLC_Device PLC_Device_冷藏區Z軸復歸 = new PLC_Device("S5011");
        PLC_Device PLC_Device_冷藏區Z軸復歸_OK = new PLC_Device("");
        Task Task_冷藏區Z軸復歸;
        MyTimer MyTimer_冷藏區Z軸復歸_向前JOG時間 = new MyTimer();
        MyTimer MyTimer_冷藏區Z軸復歸_結束延遲 = new MyTimer();
        MyTimer MyTimer_冷藏區Z軸復歸_開始延遲 = new MyTimer();
        int cnt_Program_冷藏區Z軸復歸 = 65534;
        void sub_Program_冷藏區Z軸復歸()
        {
            if (cnt_Program_冷藏區Z軸復歸 == 65534)
            {
                this.MyTimer_冷藏區Z軸復歸_結束延遲.StartTickTime(10000);
                this.MyTimer_冷藏區Z軸復歸_開始延遲.StartTickTime(10000);
                PLC_Device_冷藏區Z軸復歸.SetComment("PLC_冷藏區Z軸復歸");
                PLC_Device_冷藏區Z軸復歸_OK.SetComment("PLC_冷藏區Z軸復歸_OK");
                PLC_Device_冷藏區Z軸復歸.Bool = false;
                cnt_Program_冷藏區Z軸復歸 = 65535;
            }
            if (cnt_Program_冷藏區Z軸復歸 == 65535) cnt_Program_冷藏區Z軸復歸 = 1;
            if (cnt_Program_冷藏區Z軸復歸 == 1) cnt_Program_冷藏區Z軸復歸_檢查按下(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 2) cnt_Program_冷藏區Z軸復歸_初始化(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 3) cnt_Program_冷藏區Z軸復歸_輸送帶後退(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 4) cnt_Program_冷藏區Z軸復歸_等待輸送帶後退(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 5) cnt_Program_冷藏區Z軸復歸_向前JOG(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 6) cnt_Program_冷藏區Z軸復歸_向前JOG時間到達(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 7) cnt_Program_冷藏區Z軸復歸_離開正極限(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 8) cnt_Program_冷藏區Z軸復歸_檢查馬達停止(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 9) cnt_Program_冷藏區Z軸復歸_開始復歸(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 10) cnt_Program_冷藏區Z軸復歸_檢查HOME_OFF(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 11) cnt_Program_冷藏區Z軸復歸_檢查復歸完成(ref cnt_Program_冷藏區Z軸復歸);
            if (cnt_Program_冷藏區Z軸復歸 == 12) cnt_Program_冷藏區Z軸復歸 = 65500;
            if (cnt_Program_冷藏區Z軸復歸 > 1) cnt_Program_冷藏區Z軸復歸_檢查放開(ref cnt_Program_冷藏區Z軸復歸);

            if (cnt_Program_冷藏區Z軸復歸 == 65500)
            {
                this.MyTimer_冷藏區Z軸復歸_結束延遲.TickStop();
                this.MyTimer_冷藏區Z軸復歸_結束延遲.StartTickTime(10000);
                Servo_Stop(enum_軸號.冷藏區_Z軸);
                plC_RJ_Button_冷藏區Z軸_復歸.Bool = false;
    
                cnt_Program_冷藏區Z軸復歸 = 65535;
            }
        }
        void cnt_Program_冷藏區Z軸復歸_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區Z軸復歸.Bool) cnt++;
        }
        void cnt_Program_冷藏區Z軸復歸_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區Z軸復歸.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區Z軸復歸_初始化(ref int cnt)
        {
            PLC_Device_冷藏區Z軸復歸_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_冷藏區Z軸復歸_輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_冷藏區_輸送帶後退.Bool)
            {
                plC_RJ_Button_冷藏區_輸送帶後退.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區Z軸復歸_等待輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_冷藏區_輸送帶後退.Bool)
            {
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區Z軸復歸_向前JOG(ref int cnt)
        {
            if (plC_Button_冷藏區Z軸_正極限.Bool)
            {
                cnt++;
                return;
            }
            Servo_JOG(enum_軸號.冷藏區_Z軸, 100);
            MyTimer_冷藏區Z軸復歸_向前JOG時間.TickStop();
            MyTimer_冷藏區Z軸復歸_向前JOG時間.StartTickTime(2000);
            cnt++;
        }
        void cnt_Program_冷藏區Z軸復歸_向前JOG時間到達(ref int cnt)
        {
            if (plC_Button_冷藏區Z軸_正極限.Bool)
            {
                Servo_JOG(enum_軸號.冷藏區_Z軸, -100);
                cnt++;
                return;
            }
            if (MyTimer_冷藏區Z軸復歸_向前JOG時間.IsTimeOut())
            {
                Servo_Stop(enum_軸號.冷藏區_Z軸);
                cnt++;
            }

        }
        void cnt_Program_冷藏區Z軸復歸_離開正極限(ref int cnt)
        {
            if (!plC_Button_冷藏區Z軸_正極限.Bool)
            {
                Servo_Stop(enum_軸號.冷藏區_Z軸);
                cnt++;
                return;
            }
        }
        void cnt_Program_冷藏區Z軸復歸_檢查馬達停止(ref int cnt)
        {
            if (Servo_State(enum_軸號.冷藏區_Z軸, enum_DO.ZSPD) == true)
            {
                cnt++;
            }
        }
        void cnt_Program_冷藏區Z軸復歸_開始復歸(ref int cnt)
        {
            DeltaMotor485_port_冷藏區_Z軸[2].Home(enum_Direction.CCW, true, 100, 10, 50, 50, plC_NumBox_冷藏區Z軸_復歸偏移.Value, 200, 50);
            cnt++;
        }
        void cnt_Program_冷藏區Z軸復歸_檢查HOME_OFF(ref int cnt)
        {
            if (Servo_State(enum_軸號.冷藏區_Z軸, enum_DO.HOME) == false)
            {
                cnt++;
            }
        }
        void cnt_Program_冷藏區Z軸復歸_檢查復歸完成(ref int cnt)
        {
            if (Servo_State(enum_軸號.冷藏區_Z軸, enum_DO.HOME) == true && Servo_State(enum_軸號.冷藏區_Z軸, enum_DO.ZSPD) == true)
            {
                plC_RJ_Button_冷藏區Z軸_已完成復歸.Bool = true;
                cnt++;
            }
        }


        #endregion

        #region PLC_冷藏區X軸_絕對位置移動
        PLC_Device PLC_Device_冷藏區X軸_絕對位置移動 = new PLC_Device("S5040");
        PLC_Device PLC_Device_冷藏區X軸_絕對位置移動_目標位置 = new PLC_Device("R5003");
        PLC_Device PLC_Device_冷藏區X軸_絕對位置移動_運傳速度 = new PLC_Device("R5004");
        PLC_Device PLC_Device_冷藏區X軸_絕對位置移動_加減速度 = new PLC_Device("k800");
        PLC_Device PLC_Device_冷藏區X軸_絕對位置移動_OK = new PLC_Device("");
        Task Task_冷藏區X軸_絕對位置移動;
        MyTimer MyTimer_冷藏區X軸_絕對位置移動_結束延遲 = new MyTimer();
        int cnt_Program_冷藏區X軸_絕對位置移動 = 65534;
        void sub_Program_冷藏區X軸_絕對位置移動()
        {
            if (cnt_Program_冷藏區X軸_絕對位置移動 == 65534)
            {
                this.MyTimer_冷藏區X軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區X軸_絕對位置移動.Bool = false;
                cnt_Program_冷藏區X軸_絕對位置移動 = 65535;
            }
            if (cnt_Program_冷藏區X軸_絕對位置移動 == 65535) cnt_Program_冷藏區X軸_絕對位置移動 = 1;
            if (cnt_Program_冷藏區X軸_絕對位置移動 == 1) cnt_Program_冷藏區X軸_絕對位置移動_檢查按下(ref cnt_Program_冷藏區X軸_絕對位置移動);
            if (cnt_Program_冷藏區X軸_絕對位置移動 == 2) cnt_Program_冷藏區X軸_絕對位置移動_初始化(ref cnt_Program_冷藏區X軸_絕對位置移動);
            if (cnt_Program_冷藏區X軸_絕對位置移動 == 3) cnt_Program_冷藏區X軸_絕對位置移動_開始移動(ref cnt_Program_冷藏區X軸_絕對位置移動);
            if (cnt_Program_冷藏區X軸_絕對位置移動 == 4) cnt_Program_冷藏區X軸_絕對位置移動_等待移動結束(ref cnt_Program_冷藏區X軸_絕對位置移動);
            if (cnt_Program_冷藏區X軸_絕對位置移動 == 5) cnt_Program_冷藏區X軸_絕對位置移動 = 65500;
            if (cnt_Program_冷藏區X軸_絕對位置移動 > 1) cnt_Program_冷藏區X軸_絕對位置移動_檢查放開(ref cnt_Program_冷藏區X軸_絕對位置移動);

            if (cnt_Program_冷藏區X軸_絕對位置移動 == 65500)
            {
                this.MyTimer_冷藏區X軸_絕對位置移動_結束延遲.TickStop();
                this.MyTimer_冷藏區X軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區X軸_絕對位置移動.Bool = false;
                if(!PLC_Device_冷藏區X軸_絕對位置移動_OK.Bool) Servo_Stop(enum_軸號.冷藏區_X軸);
                cnt_Program_冷藏區X軸_絕對位置移動 = 65535;
            }
        }
        void cnt_Program_冷藏區X軸_絕對位置移動_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) cnt++;
        }
        void cnt_Program_冷藏區X軸_絕對位置移動_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區X軸_絕對位置移動.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區X軸_絕對位置移動_初始化(ref int cnt)
        {
            PLC_Device_冷藏區X軸_絕對位置移動_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_冷藏區X軸_絕對位置移動_開始移動(ref int cnt)
        {
            int position = PLC_Device_冷藏區X軸_絕對位置移動_目標位置.Value;
            int speed = PLC_Device_冷藏區X軸_絕對位置移動_運傳速度.Value;
            int acc = PLC_Device_冷藏區X軸_絕對位置移動_加減速度.Value;
            Servo_DDRVA(enum_軸號.冷藏區_X軸, position, speed, acc);
            cnt++;
        }
        void cnt_Program_冷藏區X軸_絕對位置移動_等待移動結束(ref int cnt)
        {
            if (Servo_DDRVA(enum_軸號.冷藏區_X軸))
            {
                PLC_Device_冷藏區X軸_絕對位置移動_OK.Bool = true;
                cnt++;
            }
        }





        #endregion
        #region PLC_冷藏區Z軸_絕對位置移動
        PLC_Device PLC_Device_冷藏區Z軸_絕對位置移動 = new PLC_Device("S5140");
        PLC_Device PLC_Device_冷藏區Z軸_絕對位置移動_目標位置 = new PLC_Device("R5103");
        PLC_Device PLC_Device_冷藏區Z軸_絕對位置移動_運傳速度 = new PLC_Device("R5104");
        PLC_Device PLC_Device_冷藏區Z軸_絕對位置移動_加減速度 = new PLC_Device("k800");
        PLC_Device PLC_Device_冷藏區Z軸_絕對位置移動_OK = new PLC_Device("");
        Task Task_冷藏區Z軸_絕對位置移動;
        MyTimer MyTimer_冷藏區Z軸_絕對位置移動_結束延遲 = new MyTimer();
        int cnt_Program_冷藏區Z軸_絕對位置移動 = 65534;
        void sub_Program_冷藏區Z軸_絕對位置移動()
        {
            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 65534)
            {
                this.MyTimer_冷藏區Z軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區Z軸_絕對位置移動.Bool = false;
                cnt_Program_冷藏區Z軸_絕對位置移動 = 65535;
            }
            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 65535) cnt_Program_冷藏區Z軸_絕對位置移動 = 1;
            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 1) cnt_Program_冷藏區Z軸_絕對位置移動_檢查按下(ref cnt_Program_冷藏區Z軸_絕對位置移動);
            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 2) cnt_Program_冷藏區Z軸_絕對位置移動_初始化(ref cnt_Program_冷藏區Z軸_絕對位置移動);
            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 3) cnt_Program_冷藏區Z軸_絕對位置移動_開始移動(ref cnt_Program_冷藏區Z軸_絕對位置移動);
            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 4) cnt_Program_冷藏區Z軸_絕對位置移動_等待移動結束(ref cnt_Program_冷藏區Z軸_絕對位置移動);
            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 5) cnt_Program_冷藏區Z軸_絕對位置移動 = 65500;
            if (cnt_Program_冷藏區Z軸_絕對位置移動 > 1) cnt_Program_冷藏區Z軸_絕對位置移動_檢查放開(ref cnt_Program_冷藏區Z軸_絕對位置移動);

            if (cnt_Program_冷藏區Z軸_絕對位置移動 == 65500)
            {
                this.MyTimer_冷藏區Z軸_絕對位置移動_結束延遲.TickStop();
                this.MyTimer_冷藏區Z軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區Z軸_絕對位置移動.Bool = false;
                if (!PLC_Device_冷藏區Z軸_絕對位置移動_OK.Bool) Servo_Stop(enum_軸號.冷藏區_Z軸);
                cnt_Program_冷藏區Z軸_絕對位置移動 = 65535;
            }
        }
        void cnt_Program_冷藏區Z軸_絕對位置移動_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) cnt++;
        }
        void cnt_Program_冷藏區Z軸_絕對位置移動_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區Z軸_絕對位置移動.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區Z軸_絕對位置移動_初始化(ref int cnt)
        {
            PLC_Device_冷藏區Z軸_絕對位置移動_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_冷藏區Z軸_絕對位置移動_開始移動(ref int cnt)
        {
            int position = PLC_Device_冷藏區Z軸_絕對位置移動_目標位置.Value;
            int speed = PLC_Device_冷藏區Z軸_絕對位置移動_運傳速度.Value;
            int acc = PLC_Device_冷藏區Z軸_絕對位置移動_加減速度.Value;
            Servo_DDRVA(enum_軸號.冷藏區_Z軸, position, speed, acc);
            cnt++;
        }
        void cnt_Program_冷藏區Z軸_絕對位置移動_等待移動結束(ref int cnt)
        {
            if (Servo_DDRVA(enum_軸號.冷藏區_Z軸))
            {
                PLC_Device_冷藏區Z軸_絕對位置移動_OK.Bool = true;
                cnt++;
            }
        }





        #endregion
        #region PLC_冷藏區_移動至待命位置
        PLC_Device PLC_Device_冷藏區_移動至待命位置_目標位置X = new PLC_Device("R5020");
        PLC_Device PLC_Device_冷藏區_移動至待命位置_目標位置Z = new PLC_Device("R5021");

        PLC_Device PLC_Device_冷藏區_移動至待命位置 = new PLC_Device("S5000");
        PLC_Device PLC_Device_冷藏區_移動至待命位置_OK = new PLC_Device("");
        Task Task_冷藏區_移動至待命位置;
        MyTimer MyTimer_冷藏區_移動至待命位置_結束延遲 = new MyTimer();
        int cnt_Program_冷藏區_移動至待命位置 = 65534;
        void sub_Program_冷藏區_移動至待命位置()
        {
            if (cnt_Program_冷藏區_移動至待命位置 == 65534)
            {
                this.MyTimer_冷藏區_移動至待命位置_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區_移動至待命位置.SetComment("PLC_冷藏區_移動至待命位置");
                PLC_Device_冷藏區_移動至待命位置_OK.SetComment("PLC_冷藏區_移動至待命位置_OK");
                PLC_Device_冷藏區_移動至待命位置.Bool = false;
                cnt_Program_冷藏區_移動至待命位置 = 65535;
            }
            if (cnt_Program_冷藏區_移動至待命位置 == 65535) cnt_Program_冷藏區_移動至待命位置 = 1;
            if (cnt_Program_冷藏區_移動至待命位置 == 1) cnt_Program_冷藏區_移動至待命位置_檢查按下(ref cnt_Program_冷藏區_移動至待命位置);
            if (cnt_Program_冷藏區_移動至待命位置 == 2) cnt_Program_冷藏區_移動至待命位置_初始化(ref cnt_Program_冷藏區_移動至待命位置);
            if (cnt_Program_冷藏區_移動至待命位置 == 3) cnt_Program_冷藏區_移動至待命位置_移動X軸(ref cnt_Program_冷藏區_移動至待命位置);
            if (cnt_Program_冷藏區_移動至待命位置 == 4) cnt_Program_冷藏區_移動至待命位置_等待移動X軸完成(ref cnt_Program_冷藏區_移動至待命位置);
            if (cnt_Program_冷藏區_移動至待命位置 == 5) cnt_Program_冷藏區_移動至待命位置_移動Z軸(ref cnt_Program_冷藏區_移動至待命位置);
            if (cnt_Program_冷藏區_移動至待命位置 == 6) cnt_Program_冷藏區_移動至待命位置_等待移動Z軸完成(ref cnt_Program_冷藏區_移動至待命位置);
            if (cnt_Program_冷藏區_移動至待命位置 == 7) cnt_Program_冷藏區_移動至待命位置 = 65500;
            if (cnt_Program_冷藏區_移動至待命位置 > 1) cnt_Program_冷藏區_移動至待命位置_檢查放開(ref cnt_Program_冷藏區_移動至待命位置);

            if (cnt_Program_冷藏區_移動至待命位置 == 65500)
            {
                this.MyTimer_冷藏區_移動至待命位置_結束延遲.TickStop();
                this.MyTimer_冷藏區_移動至待命位置_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區_移動至待命位置.Bool = false;
                PLC_Device_冷藏區_移動至待命位置_OK.Bool = false;

                PLC_Device_冷藏區X軸_絕對位置移動.Bool = false;
                PLC_Device_冷藏區Z軸_絕對位置移動.Bool = false;
                cnt_Program_冷藏區_移動至待命位置 = 65535;
            }
        }
        void cnt_Program_冷藏區_移動至待命位置_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區_移動至待命位置.Bool) cnt++;
        }
        void cnt_Program_冷藏區_移動至待命位置_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區_移動至待命位置.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區_移動至待命位置_初始化(ref int cnt)
        {
            cnt++;
        }

        void cnt_Program_冷藏區_移動至待命位置_檢查各部件READY(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) return;
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) return;
            cnt++;

        }
        void cnt_Program_冷藏區_移動至待命位置_移動X軸(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) return;
            PLC_Device_冷藏區X軸_絕對位置移動_目標位置.Value = PLC_Device_冷藏區_移動至待命位置_目標位置X.Value;
            PLC_Device_冷藏區X軸_絕對位置移動.Bool = true;
            cnt++;

        }
        void cnt_Program_冷藏區_移動至待命位置_等待移動X軸完成(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) return;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至待命位置_移動Z軸(ref int cnt)
        {
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) return;
            PLC_Device_冷藏區Z軸_絕對位置移動_目標位置.Value = PLC_Device_冷藏區_移動至待命位置_目標位置Z.Value;
            PLC_Device_冷藏區Z軸_絕對位置移動.Bool = true;
            cnt++;

        }
        void cnt_Program_冷藏區_移動至待命位置_等待移動Z軸完成(ref int cnt)
        {
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) return;
            cnt++;
        }





        #endregion
        #region PLC_冷藏區_移動至與常溫區藥盒傳接位置
        PLC_Device PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置_目標位置X = new PLC_Device("R5022");
        PLC_Device PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置_目標位置Z = new PLC_Device("R5023");

        PLC_Device PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置 = new PLC_Device("S5001");
        PLC_Device PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置_OK = new PLC_Device("");
        Task Task_冷藏區_移動至與常溫區藥盒傳接位置;
        MyTimer MyTimer_冷藏區_移動至與常溫區藥盒傳接位置_結束延遲 = new MyTimer();
        int cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 = 65534;
        void sub_Program_冷藏區_移動至與常溫區藥盒傳接位置()
        {
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 65534)
            {
                this.MyTimer_冷藏區_移動至與常溫區藥盒傳接位置_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置.SetComment("PLC_冷藏區_移動至與常溫區藥盒傳接位置");
                PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置_OK.SetComment("PLC_冷藏區_移動至與常溫區藥盒傳接位置_OK");
                PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置.Bool = false;
                cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 = 65535;
            }
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 65535) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 = 1;
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 1) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_檢查按下(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 2) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_初始化(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 3) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_檢查待命位置READY(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 4) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_等待待命位置完成(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 5) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_開門Ready(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 6) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_開門完成(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 7) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_Z軸Ready(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 8) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_Z軸完成(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 9) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_X軸Ready(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 10) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_X軸完成(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 11) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 = 65500;
            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 > 1) cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_檢查放開(ref cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置);

            if (cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 == 65500)
            {
                this.MyTimer_冷藏區_移動至與常溫區藥盒傳接位置_結束延遲.TickStop();
                this.MyTimer_冷藏區_移動至與常溫區藥盒傳接位置_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置.Bool = false;
                PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置_OK.Bool = false;

                plC_RJ_Button_冷藏區_輸送帶後退.Bool = false;
                plC_RJ_Button_冷藏區_輸送門開啟.Bool = false;
                PLC_Device_冷藏區X軸_絕對位置移動.Bool = false;
                PLC_Device_冷藏區Z軸_絕對位置移動.Bool = false;
                PLC_Device_冷藏區_移動至待命位置.Bool = false;
                cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置 = 65535;
            }
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置.Bool) cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_初始化(ref int cnt)
        {
            cnt++;
        }

        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_檢查待命位置READY(ref int cnt)
        {
            if (PLC_Device_冷藏區_移動至待命位置.Bool) return;
            if (plC_RJ_Button_冷藏區_輸送帶後退.Bool) return;
            PLC_Device_冷藏區_移動至待命位置.Bool = true;
            plC_RJ_Button_冷藏區_輸送帶後退.Bool = true;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_等待待命位置完成(ref int cnt)
        {
            if (PLC_Device_冷藏區_移動至待命位置.Bool) return;
            if (plC_RJ_Button_冷藏區_輸送帶後退.Bool) return;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_開門Ready(ref int cnt)
        {
            if (plC_RJ_Button_冷藏區_輸送門開啟.Bool) return;
            plC_RJ_Button_冷藏區_輸送門開啟.Bool = true;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_開門完成(ref int cnt)
        {
            if (plC_RJ_Button_冷藏區_輸送門開啟.Bool) return;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_Z軸Ready(ref int cnt)
        {
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) return;
            PLC_Device_冷藏區Z軸_絕對位置移動_目標位置.Value = PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置_目標位置Z.Value;
            PLC_Device_冷藏區Z軸_絕對位置移動.Bool = true;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_Z軸完成(ref int cnt)
        {
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) return;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_X軸Ready(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) return;
            PLC_Device_冷藏區X軸_絕對位置移動_目標位置.Value = PLC_Device_冷藏區_移動至與常溫區藥盒傳接位置_目標位置X.Value;
            PLC_Device_冷藏區X軸_絕對位置移動.Bool = true;
            cnt++;
        }
        void cnt_Program_冷藏區_移動至與常溫區藥盒傳接位置_X軸完成(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) return;
            cnt++;
        }

        #endregion
        #region PLC_冷藏區_移動至零點位置
        PLC_Device PLC_Device_冷藏區_移動至零點位置_目標位置X = new PLC_Device("R5024");
        PLC_Device PLC_Device_冷藏區_移動至零點位置_目標位置Z = new PLC_Device("R5025");

        PLC_Device PLC_Device_冷藏區_移動至零點位置 = new PLC_Device("S5002");
        PLC_Device PLC_Device_冷藏區_移動至零點位置_OK = new PLC_Device("");
        Task Task_冷藏區_移動至零點位置;
        MyTimer MyTimer_冷藏區_移動至零點位置_結束延遲 = new MyTimer();
        int cnt_Program_冷藏區_移動至零點位置 = 65534;
        void sub_Program_冷藏區_移動至零點位置()
        {
            if (cnt_Program_冷藏區_移動至零點位置 == 65534)
            {
                this.MyTimer_冷藏區_移動至零點位置_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區_移動至零點位置.SetComment("PLC_冷藏區_移動至零點位置");
                PLC_Device_冷藏區_移動至零點位置_OK.SetComment("PLC_冷藏區_移動至零點位置_OK");
                PLC_Device_冷藏區_移動至零點位置.Bool = false;
                cnt_Program_冷藏區_移動至零點位置 = 65535;
            }
            if (cnt_Program_冷藏區_移動至零點位置 == 65535) cnt_Program_冷藏區_移動至零點位置 = 1;
            if (cnt_Program_冷藏區_移動至零點位置 == 1) cnt_Program_冷藏區_移動至零點位置_檢查按下(ref cnt_Program_冷藏區_移動至零點位置);
            if (cnt_Program_冷藏區_移動至零點位置 == 2) cnt_Program_冷藏區_移動至零點位置_初始化(ref cnt_Program_冷藏區_移動至零點位置);
            if (cnt_Program_冷藏區_移動至零點位置 == 3) cnt_Program_冷藏區_移動至零點位置_檢查各部件READY(ref cnt_Program_冷藏區_移動至零點位置);
            if (cnt_Program_冷藏區_移動至零點位置 == 4) cnt_Program_冷藏區_移動至零點位置_檢查X軸小於待命位置(ref cnt_Program_冷藏區_移動至零點位置);
            if (cnt_Program_冷藏區_移動至零點位置 == 5) cnt_Program_冷藏區_移動至零點位置_等待完成(ref cnt_Program_冷藏區_移動至零點位置);
            if (cnt_Program_冷藏區_移動至零點位置 == 6) cnt_Program_冷藏區_移動至零點位置 = 65500;
            if (cnt_Program_冷藏區_移動至零點位置 > 1) cnt_Program_冷藏區_移動至零點位置_檢查放開(ref cnt_Program_冷藏區_移動至零點位置);

            if (cnt_Program_冷藏區_移動至零點位置 == 65500)
            {
                this.MyTimer_冷藏區_移動至零點位置_結束延遲.TickStop();
                this.MyTimer_冷藏區_移動至零點位置_結束延遲.StartTickTime(10000);
                PLC_Device_冷藏區_移動至零點位置.Bool = false;
                PLC_Device_冷藏區_移動至零點位置_OK.Bool = false;

                PLC_Device_冷藏區X軸_絕對位置移動.Bool = false;
                PLC_Device_冷藏區Z軸_絕對位置移動.Bool = false;
                cnt_Program_冷藏區_移動至零點位置 = 65535;
            }
        }
        void cnt_Program_冷藏區_移動至零點位置_檢查按下(ref int cnt)
        {
            if (PLC_Device_冷藏區_移動至零點位置.Bool) cnt++;
        }
        void cnt_Program_冷藏區_移動至零點位置_檢查放開(ref int cnt)
        {
            if (!PLC_Device_冷藏區_移動至零點位置.Bool) cnt = 65500;
        }
        void cnt_Program_冷藏區_移動至零點位置_初始化(ref int cnt)
        {
            cnt++;
        }

        void cnt_Program_冷藏區_移動至零點位置_檢查各部件READY(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) return;
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) return;
            
            PLC_Device_冷藏區X軸_絕對位置移動_目標位置.Value = PLC_Device_冷藏區_移動至零點位置_目標位置X.Value;
            PLC_Device_冷藏區X軸_絕對位置移動.Bool = true;
            cnt++;

        }
        void cnt_Program_冷藏區_移動至零點位置_檢查X軸小於待命位置(ref int cnt)
        {
            if (PLC_IO_冷藏區X軸_現在位置.Value <= PLC_Device_冷藏區_移動至待命位置_目標位置X.Value)
            {
                PLC_Device_冷藏區Z軸_絕對位置移動_目標位置.Value = PLC_Device_冷藏區_移動至零點位置_目標位置Z.Value;
                PLC_Device_冷藏區Z軸_絕對位置移動.Bool = true;
                cnt++;
            }      
           
        }
        void cnt_Program_冷藏區_移動至零點位置_等待完成(ref int cnt)
        {
            if (PLC_Device_冷藏區X軸_絕對位置移動.Bool) return;
            if (PLC_Device_冷藏區Z軸_絕對位置移動.Bool) return;
            cnt++;

        }





        #endregion
 


        #region PLC_常溫區復歸
        PLC_Device PLC_Device_常溫區復歸 = new PLC_Device("S6012");
        PLC_Device PLC_Device_常溫區復歸_OK = new PLC_Device("");
        Task Task_常溫區復歸;
        MyTimer MyTimer_常溫區復歸_向前JOG時間 = new MyTimer();
        MyTimer MyTimer_常溫區復歸_結束延遲 = new MyTimer();
        MyTimer MyTimer_常溫區復歸_開始延遲 = new MyTimer();
        int cnt_Program_常溫區復歸 = 65534;
        void sub_Program_常溫區復歸()
        {
            if (cnt_Program_常溫區復歸 == 65534)
            {
                this.MyTimer_常溫區復歸_結束延遲.StartTickTime(10000);
                this.MyTimer_常溫區復歸_開始延遲.StartTickTime(10000);
                PLC_Device_常溫區復歸.SetComment("PLC_常溫區復歸");
                PLC_Device_常溫區復歸_OK.SetComment("PLC_常溫區復歸_OK");
                PLC_Device_常溫區復歸.Bool = false;
                cnt_Program_常溫區復歸 = 65535;
            }
            if (cnt_Program_常溫區復歸 == 65535) cnt_Program_常溫區復歸 = 1;
            if (cnt_Program_常溫區復歸 == 1) cnt_Program_常溫區復歸_檢查按下(ref cnt_Program_常溫區復歸);
            if (cnt_Program_常溫區復歸 == 2) cnt_Program_常溫區復歸_初始化(ref cnt_Program_常溫區復歸);
            if (cnt_Program_常溫區復歸 == 3) cnt_Program_常溫區復歸_X軸復歸(ref cnt_Program_常溫區復歸);
            if (cnt_Program_常溫區復歸 == 4) cnt_Program_常溫區復歸_等待X軸復歸完成(ref cnt_Program_常溫區復歸);
            if (cnt_Program_常溫區復歸 == 5) cnt_Program_常溫區復歸_Z軸復歸(ref cnt_Program_常溫區復歸);
            if (cnt_Program_常溫區復歸 == 6) cnt_Program_常溫區復歸_等待Z軸復歸完成(ref cnt_Program_常溫區復歸);
            if (cnt_Program_常溫區復歸 == 7) cnt_Program_常溫區復歸 = 65500;
            if (cnt_Program_常溫區復歸 > 1) cnt_Program_常溫區復歸_檢查放開(ref cnt_Program_常溫區復歸);

            if (cnt_Program_常溫區復歸 == 65500)
            {
                this.MyTimer_常溫區復歸_結束延遲.TickStop();
                this.MyTimer_常溫區復歸_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區X軸復歸.Bool = false;
                PLC_Device_常溫區Z軸復歸.Bool = false;
                PLC_Device_常溫區復歸.Bool = false;
                cnt_Program_常溫區復歸 = 65535;
            }
        }
        void cnt_Program_常溫區復歸_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區復歸.Bool) cnt++;
        }
        void cnt_Program_常溫區復歸_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區復歸.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區復歸_初始化(ref int cnt)
        {
            PLC_Device_常溫區復歸_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_常溫區復歸_X軸復歸(ref int cnt)
        {
            if (!PLC_Device_常溫區X軸復歸.Bool)
            {
                PLC_Device_常溫區X軸復歸.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區復歸_等待X軸復歸完成(ref int cnt)
        {
            if (!PLC_Device_常溫區X軸復歸.Bool)
            {
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區復歸_Z軸復歸(ref int cnt)
        {
            if (!PLC_Device_常溫區Z軸復歸.Bool)
            {
                PLC_Device_常溫區Z軸復歸.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區復歸_等待Z軸復歸完成(ref int cnt)
        {
            if (!PLC_Device_常溫區Z軸復歸.Bool)
            {
                cnt++;
                return;
            }
        }


        #endregion
        #region PLC_常溫區X軸復歸
        PLC_Device PLC_Device_常溫區X軸復歸 = new PLC_Device("S6010");
        PLC_Device PLC_Device_常溫區X軸復歸_OK = new PLC_Device("");
        Task Task_常溫區X軸復歸;
        MyTimer MyTimer_常溫區X軸復歸_向前JOG時間 = new MyTimer();
        MyTimer MyTimer_常溫區X軸復歸_結束延遲 = new MyTimer();
        MyTimer MyTimer_常溫區X軸復歸_開始延遲 = new MyTimer();
        int cnt_Program_常溫區X軸復歸 = 65534;
        void sub_Program_常溫區X軸復歸()
        {
            PLC_Device_常溫區X軸復歸.Bool = plC_RJ_Button_常溫區X軸_復歸.Bool;
            if (cnt_Program_常溫區X軸復歸 == 65534)
            {
                this.MyTimer_常溫區X軸復歸_結束延遲.StartTickTime(10000);
                this.MyTimer_常溫區X軸復歸_開始延遲.StartTickTime(10000);
                PLC_Device_常溫區X軸復歸.SetComment("PLC_常溫區X軸復歸");
                PLC_Device_常溫區X軸復歸_OK.SetComment("PLC_常溫區X軸復歸_OK");
                PLC_Device_常溫區X軸復歸.Bool = false;
                cnt_Program_常溫區X軸復歸 = 65535;
            }
            if (cnt_Program_常溫區X軸復歸 == 65535) cnt_Program_常溫區X軸復歸 = 1;
            if (cnt_Program_常溫區X軸復歸 == 1) cnt_Program_常溫區X軸復歸_檢查按下(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 2) cnt_Program_常溫區X軸復歸_初始化(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 3) cnt_Program_常溫區X軸復歸_輸送帶後退(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 4) cnt_Program_常溫區X軸復歸_等待輸送帶後退(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 5) cnt_Program_常溫區X軸復歸_向前JOG(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 6) cnt_Program_常溫區X軸復歸_向前JOG時間到達(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 7) cnt_Program_常溫區X軸復歸_離開正極限(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 8) cnt_Program_常溫區X軸復歸_檢查馬達停止(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 9) cnt_Program_常溫區X軸復歸_開始復歸(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 10) cnt_Program_常溫區X軸復歸_檢查HOME_OFF(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 11) cnt_Program_常溫區X軸復歸_檢查復歸完成(ref cnt_Program_常溫區X軸復歸);
            if (cnt_Program_常溫區X軸復歸 == 12) cnt_Program_常溫區X軸復歸 = 65500;
            if (cnt_Program_常溫區X軸復歸 > 1) cnt_Program_常溫區X軸復歸_檢查放開(ref cnt_Program_常溫區X軸復歸);

            if (cnt_Program_常溫區X軸復歸 == 65500)
            {
                this.MyTimer_常溫區X軸復歸_結束延遲.TickStop();
                this.MyTimer_常溫區X軸復歸_結束延遲.StartTickTime(10000);
                Servo_Stop(enum_軸號.常溫區_X軸);
                plC_RJ_Button_常溫區X軸_復歸.Bool = false;
                cnt_Program_常溫區X軸復歸 = 65535;
            }
        }
        void cnt_Program_常溫區X軸復歸_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區X軸復歸.Bool) cnt++;
        }
        void cnt_Program_常溫區X軸復歸_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區X軸復歸.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區X軸復歸_初始化(ref int cnt)
        {
            PLC_Device_常溫區X軸復歸_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_常溫區X軸復歸_輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_常溫區_輸送帶後退.Bool)
            {
                plC_RJ_Button_常溫區_輸送帶後退.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區X軸復歸_等待輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_常溫區_輸送帶後退.Bool)
            {
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區X軸復歸_向前JOG(ref int cnt)
        {
            if (plC_Button_常溫區X軸_正極限.Bool)
            {
                cnt++;
                return;
            }
            Servo_JOG(enum_軸號.常溫區_X軸, 100);
            MyTimer_常溫區X軸復歸_向前JOG時間.TickStop();
            MyTimer_常溫區X軸復歸_向前JOG時間.StartTickTime(2000);
            cnt++;
        }
        void cnt_Program_常溫區X軸復歸_向前JOG時間到達(ref int cnt)
        {
            if (plC_Button_常溫區X軸_正極限.Bool)
            {
                Servo_JOG(enum_軸號.常溫區_X軸, -100);
                cnt++;
                return;
            }
            if (MyTimer_常溫區X軸復歸_向前JOG時間.IsTimeOut())
            {
                Servo_Stop(enum_軸號.常溫區_X軸);
                cnt++;
            }

        }
        void cnt_Program_常溫區X軸復歸_離開正極限(ref int cnt)
        {
            if (!plC_Button_常溫區X軸_正極限.Bool)
            {
                Servo_Stop(enum_軸號.常溫區_X軸);
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區X軸復歸_檢查馬達停止(ref int cnt)
        {
            if (Servo_State(enum_軸號.常溫區_X軸, enum_DO.ZSPD) == true)
            {
                cnt++;
            }
        }
        void cnt_Program_常溫區X軸復歸_開始復歸(ref int cnt)
        {
            DeltaMotor485_port_常溫區_X軸[3].Home(enum_Direction.CCW, true, 250, 10, 50, 50, plC_NumBox_常溫區X軸_復歸偏移.Value, 200, 50);
            cnt++;
        }
        void cnt_Program_常溫區X軸復歸_檢查HOME_OFF(ref int cnt)
        {
            if (Servo_State(enum_軸號.常溫區_X軸, enum_DO.HOME) == false)
            {
                cnt++;
            }
        }
        void cnt_Program_常溫區X軸復歸_檢查復歸完成(ref int cnt)
        {
            if (Servo_State(enum_軸號.常溫區_X軸, enum_DO.HOME) == true && Servo_State(enum_軸號.常溫區_X軸, enum_DO.ZSPD) == true)
            {
                plC_RJ_Button_常溫區X軸_已完成復歸.Bool = true;
                cnt++;
            }
        }


        #endregion
        #region PLC_常溫區Z軸復歸
        PLC_Device PLC_Device_常溫區Z軸復歸 = new PLC_Device("S6011");
        PLC_Device PLC_Device_常溫區Z軸復歸_OK = new PLC_Device("");
        Task Task_常溫區Z軸復歸;
        MyTimer MyTimer_常溫區Z軸復歸_向前JOG時間 = new MyTimer();
        MyTimer MyTimer_常溫區Z軸復歸_結束延遲 = new MyTimer();
        MyTimer MyTimer_常溫區Z軸復歸_開始延遲 = new MyTimer();
        int cnt_Program_常溫區Z軸復歸 = 65534;
        void sub_Program_常溫區Z軸復歸()
        {
            PLC_Device_常溫區Z軸復歸.Bool = plC_RJ_Button_常溫區Z軸_復歸.Bool;
            if (cnt_Program_常溫區Z軸復歸 == 65534)
            {
                this.MyTimer_常溫區Z軸復歸_結束延遲.StartTickTime(10000);
                this.MyTimer_常溫區Z軸復歸_開始延遲.StartTickTime(10000);
                PLC_Device_常溫區Z軸復歸.SetComment("PLC_常溫區Z軸復歸");
                PLC_Device_常溫區Z軸復歸_OK.SetComment("PLC_常溫區Z軸復歸_OK");
                PLC_Device_常溫區Z軸復歸.Bool = false;
                cnt_Program_常溫區Z軸復歸 = 65535;
            }
            if (cnt_Program_常溫區Z軸復歸 == 65535) cnt_Program_常溫區Z軸復歸 = 1;
            if (cnt_Program_常溫區Z軸復歸 == 1) cnt_Program_常溫區Z軸復歸_檢查按下(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 2) cnt_Program_常溫區Z軸復歸_初始化(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 3) cnt_Program_常溫區Z軸復歸_輸送帶後退(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 4) cnt_Program_常溫區Z軸復歸_等待輸送帶後退(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 5) cnt_Program_常溫區Z軸復歸_向前JOG(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 6) cnt_Program_常溫區Z軸復歸_向前JOG時間到達(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 7) cnt_Program_常溫區Z軸復歸_離開正極限(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 8) cnt_Program_常溫區Z軸復歸_檢查馬達停止(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 9) cnt_Program_常溫區Z軸復歸_開始復歸(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 10) cnt_Program_常溫區Z軸復歸_檢查HOME_OFF(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 11) cnt_Program_常溫區Z軸復歸_檢查復歸完成(ref cnt_Program_常溫區Z軸復歸);
            if (cnt_Program_常溫區Z軸復歸 == 12) cnt_Program_常溫區Z軸復歸 = 65500;
            if (cnt_Program_常溫區Z軸復歸 > 1) cnt_Program_常溫區Z軸復歸_檢查放開(ref cnt_Program_常溫區Z軸復歸);

            if (cnt_Program_常溫區Z軸復歸 == 65500)
            {
                this.MyTimer_常溫區Z軸復歸_結束延遲.TickStop();
                this.MyTimer_常溫區Z軸復歸_結束延遲.StartTickTime(10000);
                Servo_Stop(enum_軸號.常溫區_Z軸);
                plC_RJ_Button_常溫區Z軸_復歸.Bool = false;
                cnt_Program_常溫區Z軸復歸 = 65535;
            }
        }
        void cnt_Program_常溫區Z軸復歸_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區Z軸復歸.Bool) cnt++;
        }
        void cnt_Program_常溫區Z軸復歸_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區Z軸復歸.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區Z軸復歸_初始化(ref int cnt)
        {
            PLC_Device_常溫區Z軸復歸_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_常溫區Z軸復歸_輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_常溫區_輸送帶後退.Bool)
            {
                plC_RJ_Button_常溫區_輸送帶後退.Bool = true;
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區Z軸復歸_等待輸送帶後退(ref int cnt)
        {
            if (!plC_RJ_Button_常溫區_輸送帶後退.Bool)
            {
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區Z軸復歸_向前JOG(ref int cnt)
        {
            if (plC_Button_常溫區Z軸_正極限.Bool)
            {
                cnt++;
                return;
            }
            Servo_JOG(enum_軸號.常溫區_Z軸, 100);
            MyTimer_常溫區Z軸復歸_向前JOG時間.TickStop();
            MyTimer_常溫區Z軸復歸_向前JOG時間.StartTickTime(2000);
            cnt++;
        }
        void cnt_Program_常溫區Z軸復歸_向前JOG時間到達(ref int cnt)
        {
            if (plC_Button_常溫區Z軸_正極限.Bool)
            {
                Servo_JOG(enum_軸號.常溫區_Z軸, -100);
                cnt++;
                return;
            }
            if (MyTimer_常溫區Z軸復歸_向前JOG時間.IsTimeOut())
            {
                Servo_Stop(enum_軸號.常溫區_Z軸);
                cnt++;
            }

        }
        void cnt_Program_常溫區Z軸復歸_離開正極限(ref int cnt)
        {
            if (!plC_Button_常溫區Z軸_正極限.Bool)
            {
                Servo_Stop(enum_軸號.常溫區_Z軸);
                cnt++;
                return;
            }
        }
        void cnt_Program_常溫區Z軸復歸_檢查馬達停止(ref int cnt)
        {
            if (Servo_State(enum_軸號.常溫區_Z軸, enum_DO.ZSPD) == true)
            {
                cnt++;
            }
        }
        void cnt_Program_常溫區Z軸復歸_開始復歸(ref int cnt)
        {
            DeltaMotor485_port_常溫區_Z軸[4].Home(enum_Direction.CCW, true, 100, 10, 50, 50, plC_NumBox_常溫區Z軸_復歸偏移.Value, 200, 50);
            cnt++;
        }
        void cnt_Program_常溫區Z軸復歸_檢查HOME_OFF(ref int cnt)
        {
            if (Servo_State(enum_軸號.常溫區_Z軸, enum_DO.HOME) == false)
            {
                cnt++;
            }
        }
        void cnt_Program_常溫區Z軸復歸_檢查復歸完成(ref int cnt)
        {
            if (Servo_State(enum_軸號.常溫區_Z軸, enum_DO.HOME) == true && Servo_State(enum_軸號.常溫區_Z軸, enum_DO.ZSPD) == true)
            {
                plC_RJ_Button_常溫區Z軸_已完成復歸.Bool = true;

                cnt++;
            }
        }


        #endregion
    
        #region PLC_常溫區X軸_絕對位置移動
        PLC_Device PLC_Device_常溫區X軸_絕對位置移動 = new PLC_Device("S5240");
        PLC_Device PLC_Device_常溫區X軸_絕對位置移動_目標位置 = new PLC_Device("R5203");
        PLC_Device PLC_Device_常溫區X軸_絕對位置移動_運傳速度 = new PLC_Device("R5204");
        PLC_Device PLC_Device_常溫區X軸_絕對位置移動_加減速度 = new PLC_Device("k800");
        PLC_Device PLC_Device_常溫區X軸_絕對位置移動_OK = new PLC_Device("");
        Task Task_常溫區X軸_絕對位置移動;
        MyTimer MyTimer_常溫區X軸_絕對位置移動_結束延遲 = new MyTimer();
        int cnt_Program_常溫區X軸_絕對位置移動 = 65534;
        void sub_Program_常溫區X軸_絕對位置移動()
        {
            if (cnt_Program_常溫區X軸_絕對位置移動 == 65534)
            {
                this.MyTimer_常溫區X軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區X軸_絕對位置移動.Bool = false;
                cnt_Program_常溫區X軸_絕對位置移動 = 65535;
            }
            if (cnt_Program_常溫區X軸_絕對位置移動 == 65535) cnt_Program_常溫區X軸_絕對位置移動 = 1;
            if (cnt_Program_常溫區X軸_絕對位置移動 == 1) cnt_Program_常溫區X軸_絕對位置移動_檢查按下(ref cnt_Program_常溫區X軸_絕對位置移動);
            if (cnt_Program_常溫區X軸_絕對位置移動 == 2) cnt_Program_常溫區X軸_絕對位置移動_初始化(ref cnt_Program_常溫區X軸_絕對位置移動);
            if (cnt_Program_常溫區X軸_絕對位置移動 == 3) cnt_Program_常溫區X軸_絕對位置移動_開始移動(ref cnt_Program_常溫區X軸_絕對位置移動);
            if (cnt_Program_常溫區X軸_絕對位置移動 == 4) cnt_Program_常溫區X軸_絕對位置移動_等待移動結束(ref cnt_Program_常溫區X軸_絕對位置移動);
            if (cnt_Program_常溫區X軸_絕對位置移動 == 5) cnt_Program_常溫區X軸_絕對位置移動 = 65500;
            if (cnt_Program_常溫區X軸_絕對位置移動 > 1) cnt_Program_常溫區X軸_絕對位置移動_檢查放開(ref cnt_Program_常溫區X軸_絕對位置移動);

            if (cnt_Program_常溫區X軸_絕對位置移動 == 65500)
            {
                this.MyTimer_常溫區X軸_絕對位置移動_結束延遲.TickStop();
                this.MyTimer_常溫區X軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區X軸_絕對位置移動.Bool = false;
                if(!PLC_Device_常溫區X軸_絕對位置移動_OK.Bool) Servo_Stop(enum_軸號.常溫區_X軸);
                cnt_Program_常溫區X軸_絕對位置移動 = 65535;
            }
        }
        void cnt_Program_常溫區X軸_絕對位置移動_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) cnt++;
        }
        void cnt_Program_常溫區X軸_絕對位置移動_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區X軸_絕對位置移動.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區X軸_絕對位置移動_初始化(ref int cnt)
        {
            PLC_Device_常溫區X軸_絕對位置移動_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_常溫區X軸_絕對位置移動_開始移動(ref int cnt)
        {
            int position = PLC_Device_常溫區X軸_絕對位置移動_目標位置.Value;
            int speed = PLC_Device_常溫區X軸_絕對位置移動_運傳速度.Value;
            int acc = PLC_Device_常溫區X軸_絕對位置移動_加減速度.Value;
            Servo_DDRVA(enum_軸號.常溫區_X軸, position, speed, acc);
            MyTimer_常溫區X軸_絕對位置移動_結束延遲.TickStop();
            MyTimer_常溫區X軸_絕對位置移動_結束延遲.StartTickTime(0);
            cnt++;
        }
        void cnt_Program_常溫區X軸_絕對位置移動_等待移動結束(ref int cnt)
        {
            if (Servo_DDRVA(enum_軸號.常溫區_X軸) && MyTimer_常溫區X軸_絕對位置移動_結束延遲.IsTimeOut())
            {
                PLC_Device_常溫區X軸_絕對位置移動_OK.Bool = true;
                cnt++;
            }
        }





        #endregion
        #region PLC_常溫區Z軸_絕對位置移動
        PLC_Device PLC_Device_常溫區Z軸_絕對位置移動 = new PLC_Device("S5340");
        PLC_Device PLC_Device_常溫區Z軸_絕對位置移動_目標位置 = new PLC_Device("R5303");
        PLC_Device PLC_Device_常溫區Z軸_絕對位置移動_運傳速度 = new PLC_Device("R5304");
        PLC_Device PLC_Device_常溫區Z軸_絕對位置移動_加減速度 = new PLC_Device("k800");
        PLC_Device PLC_Device_常溫區Z軸_絕對位置移動_OK = new PLC_Device("");
        Task Task_常溫區Z軸_絕對位置移動;
        MyTimer MyTimer_常溫區Z軸_絕對位置移動_結束延遲 = new MyTimer();
        int cnt_Program_常溫區Z軸_絕對位置移動 = 65534;
        void sub_Program_常溫區Z軸_絕對位置移動()
        {
            if (cnt_Program_常溫區Z軸_絕對位置移動 == 65534)
            {
                this.MyTimer_常溫區Z軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區Z軸_絕對位置移動.Bool = false;
                cnt_Program_常溫區Z軸_絕對位置移動 = 65535;
            }
            if (cnt_Program_常溫區Z軸_絕對位置移動 == 65535) cnt_Program_常溫區Z軸_絕對位置移動 = 1;
            if (cnt_Program_常溫區Z軸_絕對位置移動 == 1) cnt_Program_常溫區Z軸_絕對位置移動_檢查按下(ref cnt_Program_常溫區Z軸_絕對位置移動);
            if (cnt_Program_常溫區Z軸_絕對位置移動 == 2) cnt_Program_常溫區Z軸_絕對位置移動_初始化(ref cnt_Program_常溫區Z軸_絕對位置移動);
            if (cnt_Program_常溫區Z軸_絕對位置移動 == 3) cnt_Program_常溫區Z軸_絕對位置移動_開始移動(ref cnt_Program_常溫區Z軸_絕對位置移動);
            if (cnt_Program_常溫區Z軸_絕對位置移動 == 4) cnt_Program_常溫區Z軸_絕對位置移動_等待移動結束(ref cnt_Program_常溫區Z軸_絕對位置移動);
            if (cnt_Program_常溫區Z軸_絕對位置移動 == 5) cnt_Program_常溫區Z軸_絕對位置移動 = 65500;
            if (cnt_Program_常溫區Z軸_絕對位置移動 > 1) cnt_Program_常溫區Z軸_絕對位置移動_檢查放開(ref cnt_Program_常溫區Z軸_絕對位置移動);

            if (cnt_Program_常溫區Z軸_絕對位置移動 == 65500)
            {
                this.MyTimer_常溫區Z軸_絕對位置移動_結束延遲.TickStop();
                this.MyTimer_常溫區Z軸_絕對位置移動_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區Z軸_絕對位置移動.Bool = false;
                if (!PLC_Device_常溫區Z軸_絕對位置移動_OK.Bool) Servo_Stop(enum_軸號.常溫區_Z軸);
                cnt_Program_常溫區Z軸_絕對位置移動 = 65535;
            }
        }
        void cnt_Program_常溫區Z軸_絕對位置移動_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) cnt++;
        }
        void cnt_Program_常溫區Z軸_絕對位置移動_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區Z軸_絕對位置移動.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區Z軸_絕對位置移動_初始化(ref int cnt)
        {
            PLC_Device_常溫區Z軸_絕對位置移動_OK.Bool = false;
            cnt++;
        }
        void cnt_Program_常溫區Z軸_絕對位置移動_開始移動(ref int cnt)
        {
            int position = PLC_Device_常溫區Z軸_絕對位置移動_目標位置.Value;
            int speed = PLC_Device_常溫區Z軸_絕對位置移動_運傳速度.Value;
            int acc = PLC_Device_常溫區Z軸_絕對位置移動_加減速度.Value;
            Servo_DDRVA(enum_軸號.常溫區_Z軸, position, speed, acc);
            MyTimer_常溫區Z軸_絕對位置移動_結束延遲.TickStop();
            MyTimer_常溫區Z軸_絕對位置移動_結束延遲.StartTickTime(0);
            cnt++;
        }
        void cnt_Program_常溫區Z軸_絕對位置移動_等待移動結束(ref int cnt)
        {
            if (Servo_DDRVA(enum_軸號.常溫區_Z軸) && MyTimer_常溫區Z軸_絕對位置移動_結束延遲.IsTimeOut())
            {
                PLC_Device_常溫區Z軸_絕對位置移動_OK.Bool = true;
                cnt++;
            }
        }





        #endregion
        #region PLC_常溫區_移動至冷藏待命位置
        PLC_Device PLC_Device_常溫區_移動至冷藏待命位置_目標位置X = new PLC_Device("R6020");
        PLC_Device PLC_Device_常溫區_移動至冷藏待命位置_目標位置Z = new PLC_Device("R6021");

        PLC_Device PLC_Device_常溫區_移動至冷藏待命位置 = new PLC_Device("S6000");
        PLC_Device PLC_Device_常溫區_移動至冷藏待命位置_OK = new PLC_Device("");
        Task Task_常溫區_移動至冷藏待命位置;
        MyTimer MyTimer_常溫區_移動至冷藏待命位置_結束延遲 = new MyTimer();
        int cnt_Program_常溫區_移動至冷藏待命位置 = 65534;
        void sub_Program_常溫區_移動至冷藏待命位置()
        {
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 65534)
            {
                this.MyTimer_常溫區_移動至冷藏待命位置_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區_移動至冷藏待命位置.SetComment("PLC_常溫區_移動至冷藏待命位置");
                PLC_Device_常溫區_移動至冷藏待命位置_OK.SetComment("PLC_常溫區_移動至冷藏待命位置_OK");
                PLC_Device_常溫區_移動至冷藏待命位置.Bool = false;
                cnt_Program_常溫區_移動至冷藏待命位置 = 65535;
            }
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 65535) cnt_Program_常溫區_移動至冷藏待命位置 = 1;
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 1) cnt_Program_常溫區_移動至冷藏待命位置_檢查按下(ref cnt_Program_常溫區_移動至冷藏待命位置);
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 2) cnt_Program_常溫區_移動至冷藏待命位置_初始化(ref cnt_Program_常溫區_移動至冷藏待命位置);
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 3) cnt_Program_常溫區_移動至冷藏待命位置_移動X軸(ref cnt_Program_常溫區_移動至冷藏待命位置);
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 4) cnt_Program_常溫區_移動至冷藏待命位置_等待移動X軸完成(ref cnt_Program_常溫區_移動至冷藏待命位置);
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 5) cnt_Program_常溫區_移動至冷藏待命位置_移動Z軸(ref cnt_Program_常溫區_移動至冷藏待命位置);
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 6) cnt_Program_常溫區_移動至冷藏待命位置_等待移動Z軸完成(ref cnt_Program_常溫區_移動至冷藏待命位置);
            if (cnt_Program_常溫區_移動至冷藏待命位置 == 7) cnt_Program_常溫區_移動至冷藏待命位置 = 65500;
            if (cnt_Program_常溫區_移動至冷藏待命位置 > 1) cnt_Program_常溫區_移動至冷藏待命位置_檢查放開(ref cnt_Program_常溫區_移動至冷藏待命位置);

            if (cnt_Program_常溫區_移動至冷藏待命位置 == 65500)
            {
                this.MyTimer_常溫區_移動至冷藏待命位置_結束延遲.TickStop();
                this.MyTimer_常溫區_移動至冷藏待命位置_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區_移動至冷藏待命位置.Bool = false;
                PLC_Device_常溫區_移動至冷藏待命位置_OK.Bool = false;

                PLC_Device_常溫區X軸_絕對位置移動.Bool = false;
                PLC_Device_常溫區Z軸_絕對位置移動.Bool = false;
                cnt_Program_常溫區_移動至冷藏待命位置 = 65535;
            }
        }
        void cnt_Program_常溫區_移動至冷藏待命位置_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區_移動至冷藏待命位置.Bool) cnt++;
        }
        void cnt_Program_常溫區_移動至冷藏待命位置_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區_移動至冷藏待命位置.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區_移動至冷藏待命位置_初始化(ref int cnt)
        {
            cnt++;
        }

        void cnt_Program_常溫區_移動至冷藏待命位置_檢查各部件READY(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) return;
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) return;
            cnt++;

        }
        void cnt_Program_常溫區_移動至冷藏待命位置_移動X軸(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) return;
            PLC_Device_常溫區X軸_絕對位置移動_目標位置.Value = PLC_Device_常溫區_移動至冷藏待命位置_目標位置X.Value;
            PLC_Device_常溫區X軸_絕對位置移動.Bool = true;
            cnt++;

        }
        void cnt_Program_常溫區_移動至冷藏待命位置_等待移動X軸完成(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) return;
            cnt++;
        }
        void cnt_Program_常溫區_移動至冷藏待命位置_移動Z軸(ref int cnt)
        {
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) return;
            PLC_Device_常溫區Z軸_絕對位置移動_目標位置.Value = PLC_Device_常溫區_移動至冷藏待命位置_目標位置Z.Value;
            PLC_Device_常溫區Z軸_絕對位置移動.Bool = true;
            cnt++;

        }
        void cnt_Program_常溫區_移動至冷藏待命位置_等待移動Z軸完成(ref int cnt)
        {
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) return;
            cnt++;
        }





        #endregion
        #region PLC_常溫區_移動至與冷藏區藥盒傳接位置
        PLC_Device PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置_目標位置X = new PLC_Device("R6022");
        PLC_Device PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置_目標位置Z = new PLC_Device("R6023");

        PLC_Device PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置 = new PLC_Device("S6001");
        PLC_Device PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置_OK = new PLC_Device("");
        Task Task_常溫區_移動至與冷藏區藥盒傳接位置;
        MyTimer MyTimer_常溫區_移動至與冷藏區藥盒傳接位置_結束延遲 = new MyTimer();
        int cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 = 65534;
        void sub_Program_常溫區_移動至與冷藏區藥盒傳接位置()
        {
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 65534)
            {
                this.MyTimer_常溫區_移動至與冷藏區藥盒傳接位置_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置.SetComment("PLC_常溫區_移動至與冷藏區藥盒傳接位置");
                PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置_OK.SetComment("PLC_常溫區_移動至與冷藏區藥盒傳接位置_OK");
                PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置.Bool = false;
                cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 = 65535;
            }
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 65535) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 = 1;
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 1) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_檢查按下(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 2) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_初始化(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 3) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_檢查待命位置READY(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 4) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_等待待命位置完成(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 5) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_開門Ready(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 6) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_開門完成(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 7) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_Z軸Ready(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 8) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_Z軸完成(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 9) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_X軸Ready(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 10) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_X軸完成(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 11) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 = 65500;
            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 > 1) cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_檢查放開(ref cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置);

            if (cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 == 65500)
            {
                this.MyTimer_常溫區_移動至與冷藏區藥盒傳接位置_結束延遲.TickStop();
                this.MyTimer_常溫區_移動至與冷藏區藥盒傳接位置_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置.Bool = false;
                PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置_OK.Bool = false;

                plC_RJ_Button_常溫區_輸送帶後退.Bool = false;
                plC_RJ_Button_冷藏區_輸送門開啟.Bool = false;
                PLC_Device_常溫區X軸_絕對位置移動.Bool = false;
                PLC_Device_常溫區Z軸_絕對位置移動.Bool = false;
                PLC_Device_常溫區_移動至冷藏待命位置.Bool = false;
                cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置 = 65535;
            }
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置.Bool) cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_初始化(ref int cnt)
        {
            cnt++;
        }

        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_檢查待命位置READY(ref int cnt)
        {
            if (PLC_Device_常溫區_移動至冷藏待命位置.Bool) return;
            if (plC_RJ_Button_常溫區_輸送帶後退.Bool) return;
            PLC_Device_常溫區_移動至冷藏待命位置.Bool = true;
            plC_RJ_Button_常溫區_輸送帶後退.Bool = true;
            cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_等待待命位置完成(ref int cnt)
        {
            if (PLC_Device_常溫區_移動至冷藏待命位置.Bool) return;
            if (plC_RJ_Button_常溫區_輸送帶後退.Bool) return;
            cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_開門Ready(ref int cnt)
        {
            if (plC_RJ_Button_冷藏區_輸送門開啟.Bool) return;
            plC_RJ_Button_冷藏區_輸送門開啟.Bool = true;
            cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_開門完成(ref int cnt)
        {
            if (plC_RJ_Button_冷藏區_輸送門開啟.Bool) return;
            cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_Z軸Ready(ref int cnt)
        {
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) return;
            PLC_Device_常溫區Z軸_絕對位置移動_目標位置.Value = PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置_目標位置Z.Value;
            PLC_Device_常溫區Z軸_絕對位置移動.Bool = true;
            cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_Z軸完成(ref int cnt)
        {
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) return;
            cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_X軸Ready(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) return;
            PLC_Device_常溫區X軸_絕對位置移動_目標位置.Value = PLC_Device_常溫區_移動至與冷藏區藥盒傳接位置_目標位置X.Value;
            PLC_Device_常溫區X軸_絕對位置移動.Bool = true;
            cnt++;
        }
        void cnt_Program_常溫區_移動至與冷藏區藥盒傳接位置_X軸完成(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) return;
            cnt++;
        }

        #endregion
        #region PLC_常溫區_移動至零點位置
        PLC_Device PLC_Device_常溫區_移動至零點位置_目標位置X = new PLC_Device("R6024");
        PLC_Device PLC_Device_常溫區_移動至零點位置_目標位置Z = new PLC_Device("R6025");

        PLC_Device PLC_Device_常溫區_移動至零點位置 = new PLC_Device("S6002");
        PLC_Device PLC_Device_常溫區_移動至零點位置_OK = new PLC_Device("");
        Task Task_常溫區_移動至零點位置;
        MyTimer MyTimer_常溫區_移動至零點位置_結束延遲 = new MyTimer();
        int cnt_Program_常溫區_移動至零點位置 = 65534;
        void sub_Program_常溫區_移動至零點位置()
        {
            if (cnt_Program_常溫區_移動至零點位置 == 65534)
            {
                this.MyTimer_常溫區_移動至零點位置_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區_移動至零點位置.SetComment("PLC_常溫區_移動至零點位置");
                PLC_Device_常溫區_移動至零點位置_OK.SetComment("PLC_常溫區_移動至零點位置_OK");
                PLC_Device_常溫區_移動至零點位置.Bool = false;
                cnt_Program_常溫區_移動至零點位置 = 65535;
            }
            if (cnt_Program_常溫區_移動至零點位置 == 65535) cnt_Program_常溫區_移動至零點位置 = 1;
            if (cnt_Program_常溫區_移動至零點位置 == 1) cnt_Program_常溫區_移動至零點位置_檢查按下(ref cnt_Program_常溫區_移動至零點位置);
            if (cnt_Program_常溫區_移動至零點位置 == 2) cnt_Program_常溫區_移動至零點位置_初始化(ref cnt_Program_常溫區_移動至零點位置);
            if (cnt_Program_常溫區_移動至零點位置 == 3) cnt_Program_常溫區_移動至零點位置_檢查各部件READY(ref cnt_Program_常溫區_移動至零點位置);
            if (cnt_Program_常溫區_移動至零點位置 == 4) cnt_Program_常溫區_移動至零點位置_檢查X軸小於待命位置(ref cnt_Program_常溫區_移動至零點位置);
            if (cnt_Program_常溫區_移動至零點位置 == 5) cnt_Program_常溫區_移動至零點位置_等待完成(ref cnt_Program_常溫區_移動至零點位置);
            if (cnt_Program_常溫區_移動至零點位置 == 6) cnt_Program_常溫區_移動至零點位置 = 65500;
            if (cnt_Program_常溫區_移動至零點位置 > 1) cnt_Program_常溫區_移動至零點位置_檢查放開(ref cnt_Program_常溫區_移動至零點位置);

            if (cnt_Program_常溫區_移動至零點位置 == 65500)
            {
                this.MyTimer_常溫區_移動至零點位置_結束延遲.TickStop();
                this.MyTimer_常溫區_移動至零點位置_結束延遲.StartTickTime(10000);
                PLC_Device_常溫區_移動至零點位置.Bool = false;
                PLC_Device_常溫區_移動至零點位置_OK.Bool = false;

                PLC_Device_常溫區X軸_絕對位置移動.Bool = false;
                PLC_Device_常溫區Z軸_絕對位置移動.Bool = false;
                cnt_Program_常溫區_移動至零點位置 = 65535;
            }
        }
        void cnt_Program_常溫區_移動至零點位置_檢查按下(ref int cnt)
        {
            if (PLC_Device_常溫區_移動至零點位置.Bool) cnt++;
        }
        void cnt_Program_常溫區_移動至零點位置_檢查放開(ref int cnt)
        {
            if (!PLC_Device_常溫區_移動至零點位置.Bool) cnt = 65500;
        }
        void cnt_Program_常溫區_移動至零點位置_初始化(ref int cnt)
        {
            cnt++;
        }

        void cnt_Program_常溫區_移動至零點位置_檢查各部件READY(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) return;
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) return;

            PLC_Device_常溫區X軸_絕對位置移動_目標位置.Value = PLC_Device_常溫區_移動至零點位置_目標位置X.Value;
            PLC_Device_常溫區X軸_絕對位置移動.Bool = true;
            cnt++;

        }
        void cnt_Program_常溫區_移動至零點位置_檢查X軸小於待命位置(ref int cnt)
        {
            if (PLC_IO_常溫區X軸_現在位置.Value <= PLC_Device_常溫區_移動至冷藏待命位置_目標位置X.Value)
            {
                PLC_Device_常溫區Z軸_絕對位置移動_目標位置.Value = PLC_Device_常溫區_移動至零點位置_目標位置Z.Value;
                PLC_Device_常溫區Z軸_絕對位置移動.Bool = true;
                cnt++;
            }

        }
        void cnt_Program_常溫區_移動至零點位置_等待完成(ref int cnt)
        {
            if (PLC_Device_常溫區X軸_絕對位置移動.Bool) return;
            if (PLC_Device_常溫區Z軸_絕對位置移動.Bool) return;
            cnt++;

        }





        #endregion



        #region PLC_進出盒區Y軸復歸
        PLC_Device PLC_Device_進出盒區Y軸復歸 = new PLC_Device("S7010");
        PLC_Device PLC_Device_進出盒區Y軸復歸_OK = new PLC_Device("");
        Task Task_進出盒區Y軸復歸;
        MyTimer MyTimer_進出盒區Y軸復歸_向前JOG時間 = new MyTimer();
        MyTimer MyTimer_進出盒區Y軸復歸_結束延遲 = new MyTimer();
        MyTimer MyTimer_進出盒區Y軸復歸_開始延遲 = new MyTimer();
        int cnt_Program_進出盒區Y軸復歸 = 65534;
        void sub_Program_進出盒區Y軸復歸()
        {
            if (cnt_Program_進出盒區Y軸復歸 == 65534)
            {
                this.MyTimer_進出盒區Y軸復歸_結束延遲.StartTickTime(10000);
                this.MyTimer_進出盒區Y軸復歸_開始延遲.StartTickTime(10000);
                PLC_Device_進出盒區Y軸復歸.SetComment("PLC_進出盒區Y軸復歸");
                PLC_Device_進出盒區Y軸復歸_OK.SetComment("PLC_進出盒區Y軸復歸_OK");
                PLC_Device_進出盒區Y軸復歸.Bool = false;
                cnt_Program_進出盒區Y軸復歸 = 65535;
            }
            if (cnt_Program_進出盒區Y軸復歸 == 65535) cnt_Program_進出盒區Y軸復歸 = 1;
            if (cnt_Program_進出盒區Y軸復歸 == 1) cnt_Program_進出盒區Y軸復歸_檢查按下(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 2) cnt_Program_進出盒區Y軸復歸_初始化(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 3) cnt_Program_進出盒區Y軸復歸_向前JOG(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 4) cnt_Program_進出盒區Y軸復歸_向前JOG時間到達(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 5) cnt_Program_進出盒區Y軸復歸_離開正極限(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 6) cnt_Program_進出盒區Y軸復歸_檢查馬達停止(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 7) cnt_Program_進出盒區Y軸復歸_開始復歸(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 8) cnt_Program_進出盒區Y軸復歸_檢查HOME_OFF(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 9) cnt_Program_進出盒區Y軸復歸_檢查復歸完成(ref cnt_Program_進出盒區Y軸復歸);
            if (cnt_Program_進出盒區Y軸復歸 == 10) cnt_Program_進出盒區Y軸復歸 = 65500;
            if (cnt_Program_進出盒區Y軸復歸 > 1) cnt_Program_進出盒區Y軸復歸_檢查放開(ref cnt_Program_進出盒區Y軸復歸);

            if (cnt_Program_進出盒區Y軸復歸 == 65500)
            {
                this.MyTimer_進出盒區Y軸復歸_結束延遲.TickStop();
                this.MyTimer_進出盒區Y軸復歸_結束延遲.StartTickTime(10000);
                Servo_Stop(enum_軸號.進出盒區_Y軸);
                plC_RJ_Button_進出盒區Y軸_復歸.Bool = false;

                cnt_Program_進出盒區Y軸復歸 = 65535;
            }
        }
        void cnt_Program_進出盒區Y軸復歸_檢查按下(ref int cnt)
        {
            if (PLC_Device_進出盒區Y軸復歸.Bool) cnt++;
        }
        void cnt_Program_進出盒區Y軸復歸_檢查放開(ref int cnt)
        {
            if (!PLC_Device_進出盒區Y軸復歸.Bool) cnt = 65500;
        }
        void cnt_Program_進出盒區Y軸復歸_初始化(ref int cnt)
        {
            PLC_Device_進出盒區Y軸復歸_OK.Bool = false;
            cnt++;
        }
      
        void cnt_Program_進出盒區Y軸復歸_向前JOG(ref int cnt)
        {
            if (plC_Button_進出盒區Y軸_正極限.Bool)
            {
                cnt++;
                return;
            }
            Servo_JOG(enum_軸號.進出盒區_Y軸, 100);
            MyTimer_進出盒區Y軸復歸_向前JOG時間.TickStop();
            MyTimer_進出盒區Y軸復歸_向前JOG時間.StartTickTime(2000);
            cnt++;
        }
        void cnt_Program_進出盒區Y軸復歸_向前JOG時間到達(ref int cnt)
        {
            if (plC_Button_進出盒區Y軸_正極限.Bool)
            {
                Servo_JOG(enum_軸號.進出盒區_Y軸, -100);
                cnt++;
                return;
            }
            if (MyTimer_進出盒區Y軸復歸_向前JOG時間.IsTimeOut())
            {
                Servo_Stop(enum_軸號.進出盒區_Y軸);
                cnt++;
            }

        }
        void cnt_Program_進出盒區Y軸復歸_離開正極限(ref int cnt)
        {
            if (!plC_Button_進出盒區Y軸_正極限.Bool)
            {
                Servo_Stop(enum_軸號.進出盒區_Y軸);
                cnt++;
                return;
            }
        }
        void cnt_Program_進出盒區Y軸復歸_檢查馬達停止(ref int cnt)
        {
            if (Servo_State(enum_軸號.進出盒區_Y軸, enum_DO.ZSPD) == true)
            {
                cnt++;
            }
        }
        void cnt_Program_進出盒區Y軸復歸_開始復歸(ref int cnt)
        {
            DeltaMotor485_port_進出盒區_Y軸[5].Home(enum_Direction.CCW, true, 250, 10, 50, 50, plC_NumBox_進出盒區Y軸_復歸偏移.Value, 200, 50);
            cnt++;
        }
        void cnt_Program_進出盒區Y軸復歸_檢查HOME_OFF(ref int cnt)
        {
            if (Servo_State(enum_軸號.進出盒區_Y軸, enum_DO.HOME) == false)
            {
                cnt++;
            }
        }
        void cnt_Program_進出盒區Y軸復歸_檢查復歸完成(ref int cnt)
        {
            if (Servo_State(enum_軸號.進出盒區_Y軸, enum_DO.HOME) == true && Servo_State(enum_軸號.進出盒區_Y軸, enum_DO.ZSPD) == true)
            {
                plC_RJ_Button_進出盒區Y軸_已完成復歸.Bool = true;
                cnt++;
            }
        }


        #endregion
    }
}
