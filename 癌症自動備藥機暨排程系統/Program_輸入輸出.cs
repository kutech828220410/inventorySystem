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
        List<List<PLC_Button>> pLC_Buttons_board_input = new List<List<PLC_Button>>();
        List<List<PLC_Button>> pLC_Buttons_board_output = new List<List<PLC_Button>>();
        MySerialPort mySerial_IO = new MySerialPort();
        public void Program_輸入輸出_Init()
        {
            if(myConfigClass.主機模式)
            {
                mySerial_IO.BufferSize = 2048;
                mySerial_IO.Init("COM1", 115200, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.Two);
                H_Pannel_lib.Communication.UART_ConsoletWrite = false;
                H_Pannel_lib.Driver_IO_Board driver_IO_Board = new H_Pannel_lib.Driver_IO_Board();
                H_Pannel_lib.Communication.UART_TimeOut = 120;
                H_Pannel_lib.Communication.UART_Delay = 10;
                this.BoardUIInit();
                driver_IO_Board.SleepTime = 0;
                driver_IO_Board.ProgramEvent += Driver_IO_Board_ProgramEvent;
                driver_IO_Board.Init(mySerial_IO, new byte[] { 0, 1, 2 });
                this.plC_UI_Init.Add_Method(Program_輸入輸出);
            }
           
 
        }
        public void Program_輸入輸出()
        {

        }

        private void BoardUIInit()
        {
            List<PLC_Button> pLC_Buttons_board0_input = new List<PLC_Button>();
            pLC_Buttons_board0_input.Add(plC_Button_X200);
            pLC_Buttons_board0_input.Add(plC_Button_X201);
            pLC_Buttons_board0_input.Add(plC_Button_X202);
            pLC_Buttons_board0_input.Add(plC_Button_X203);
            pLC_Buttons_board0_input.Add(plC_Button_X204);
            pLC_Buttons_board0_input.Add(plC_Button_X205);
            pLC_Buttons_board0_input.Add(plC_Button_X206);
            pLC_Buttons_board0_input.Add(plC_Button_X207);
            pLC_Buttons_board0_input.Add(plC_Button_X210);
            pLC_Buttons_board0_input.Add(plC_Button_X211);
            pLC_Buttons_board0_input.Add(plC_Button_X212);
            pLC_Buttons_board0_input.Add(plC_Button_X213);
            pLC_Buttons_board0_input.Add(plC_Button_X214);
            pLC_Buttons_board0_input.Add(plC_Button_X215);
            pLC_Buttons_board0_input.Add(plC_Button_X216);
            pLC_Buttons_board0_input.Add(plC_Button_X217);
            pLC_Buttons_board_input.Add(pLC_Buttons_board0_input);



            List<PLC_Button> pLC_Buttons_board0_output = new List<PLC_Button>();
            pLC_Buttons_board0_output.Add(plC_Button_Y200);
            pLC_Buttons_board0_output.Add(plC_Button_Y201);
            pLC_Buttons_board0_output.Add(plC_Button_Y202);
            pLC_Buttons_board0_output.Add(plC_Button_Y203);
            pLC_Buttons_board0_output.Add(plC_Button_Y204);
            pLC_Buttons_board0_output.Add(plC_Button_Y205);
            pLC_Buttons_board0_output.Add(plC_Button_Y206);
            pLC_Buttons_board0_output.Add(plC_Button_Y207);
            pLC_Buttons_board0_output.Add(plC_Button_Y210);
            pLC_Buttons_board0_output.Add(plC_Button_Y211);
            pLC_Buttons_board0_output.Add(plC_Button_Y212);
            pLC_Buttons_board0_output.Add(plC_Button_Y213);
            pLC_Buttons_board0_output.Add(plC_Button_Y214);
            pLC_Buttons_board0_output.Add(plC_Button_Y215);
            pLC_Buttons_board0_output.Add(plC_Button_Y216);
            pLC_Buttons_board0_output.Add(plC_Button_Y217);
            pLC_Buttons_board_output.Add(pLC_Buttons_board0_output);


            List<PLC_Button> pLC_Buttons_board1_input = new List<PLC_Button>();
            pLC_Buttons_board1_input.Add(plC_Button_X220);
            pLC_Buttons_board1_input.Add(plC_Button_X221);
            pLC_Buttons_board1_input.Add(plC_Button_X222);
            pLC_Buttons_board1_input.Add(plC_Button_X223);
            pLC_Buttons_board1_input.Add(plC_Button_X224);
            pLC_Buttons_board1_input.Add(plC_Button_X225);
            pLC_Buttons_board1_input.Add(plC_Button_X226);
            pLC_Buttons_board1_input.Add(plC_Button_X227);
            pLC_Buttons_board1_input.Add(plC_Button_X230);
            pLC_Buttons_board1_input.Add(plC_Button_X231);
            pLC_Buttons_board1_input.Add(plC_Button_X232);
            pLC_Buttons_board1_input.Add(plC_Button_X233);
            pLC_Buttons_board1_input.Add(plC_Button_X234);
            pLC_Buttons_board1_input.Add(plC_Button_X235);
            pLC_Buttons_board1_input.Add(plC_Button_X236);
            pLC_Buttons_board1_input.Add(plC_Button_X237);
            pLC_Buttons_board_input.Add(pLC_Buttons_board1_input);



            List<PLC_Button> pLC_Buttons_board1_output = new List<PLC_Button>();
            pLC_Buttons_board1_output.Add(plC_Button_Y220);
            pLC_Buttons_board1_output.Add(plC_Button_Y221);
            pLC_Buttons_board1_output.Add(plC_Button_Y222);
            pLC_Buttons_board1_output.Add(plC_Button_Y223);
            pLC_Buttons_board1_output.Add(plC_Button_Y224);
            pLC_Buttons_board1_output.Add(plC_Button_Y225);
            pLC_Buttons_board1_output.Add(plC_Button_Y226);
            pLC_Buttons_board1_output.Add(plC_Button_Y227);
            pLC_Buttons_board1_output.Add(plC_Button_Y230);
            pLC_Buttons_board1_output.Add(plC_Button_Y231);
            pLC_Buttons_board1_output.Add(plC_Button_Y232);
            pLC_Buttons_board1_output.Add(plC_Button_Y233);
            pLC_Buttons_board1_output.Add(plC_Button_Y234);
            pLC_Buttons_board1_output.Add(plC_Button_Y235);
            pLC_Buttons_board1_output.Add(plC_Button_Y236);
            pLC_Buttons_board1_output.Add(plC_Button_Y237);
            pLC_Buttons_board_output.Add(pLC_Buttons_board1_output);


            List<PLC_Button> pLC_Buttons_board2_input = new List<PLC_Button>();
            pLC_Buttons_board2_input.Add(plC_Button_X240);
            pLC_Buttons_board2_input.Add(plC_Button_X241);
            pLC_Buttons_board2_input.Add(plC_Button_X242);
            pLC_Buttons_board2_input.Add(plC_Button_X243);
            pLC_Buttons_board2_input.Add(plC_Button_X244);
            pLC_Buttons_board2_input.Add(plC_Button_X245);
            pLC_Buttons_board2_input.Add(plC_Button_X246);
            pLC_Buttons_board2_input.Add(plC_Button_X247);
            pLC_Buttons_board2_input.Add(plC_Button_X250);
            pLC_Buttons_board2_input.Add(plC_Button_X251);
            pLC_Buttons_board2_input.Add(plC_Button_X252);
            pLC_Buttons_board2_input.Add(plC_Button_X253);
            pLC_Buttons_board2_input.Add(plC_Button_X254);
            pLC_Buttons_board2_input.Add(plC_Button_X255);
            pLC_Buttons_board2_input.Add(plC_Button_X256);
            pLC_Buttons_board2_input.Add(plC_Button_X257);
            pLC_Buttons_board_input.Add(pLC_Buttons_board2_input);



            List<PLC_Button> pLC_Buttons_board2_output = new List<PLC_Button>();
            pLC_Buttons_board2_output.Add(plC_Button_Y240);
            pLC_Buttons_board2_output.Add(plC_Button_Y241);
            pLC_Buttons_board2_output.Add(plC_Button_Y242);
            pLC_Buttons_board2_output.Add(plC_Button_Y243);
            pLC_Buttons_board2_output.Add(plC_Button_Y244);
            pLC_Buttons_board2_output.Add(plC_Button_Y245);
            pLC_Buttons_board2_output.Add(plC_Button_Y246);
            pLC_Buttons_board2_output.Add(plC_Button_Y247);
            pLC_Buttons_board2_output.Add(plC_Button_Y250);
            pLC_Buttons_board2_output.Add(plC_Button_Y251);
            pLC_Buttons_board2_output.Add(plC_Button_Y252);
            pLC_Buttons_board2_output.Add(plC_Button_Y253);
            pLC_Buttons_board2_output.Add(plC_Button_Y254);
            pLC_Buttons_board2_output.Add(plC_Button_Y255);
            pLC_Buttons_board2_output.Add(plC_Button_Y256);
            pLC_Buttons_board2_output.Add(plC_Button_Y257);
            pLC_Buttons_board_output.Add(pLC_Buttons_board2_output);
        }
        private void Driver_IO_Board_ProgramEvent(H_Pannel_lib.Driver_IO_Board driver_IO_Board)
        {
            for(int i = 0; i < 10; i ++)
            {
                for (byte k = 0; k < driver_IO_Board.stationClasses.Count; k++)
                {
                    bool flag_input = driver_IO_Board[k].Input[i];
                    PLC.device_system.Set_Device(pLC_Buttons_board_input[k][i].讀取元件位置, flag_input);

                }
              
            }
            for (int i = 0; i < 10; i++)
            {
                for (byte k = 0; k < driver_IO_Board.stationClasses.Count; k++)
                {
                    bool flag_output = driver_IO_Board[k].Output[i];
                    PLC.Device.Set_Device(pLC_Buttons_board_output[k][i].讀取元件位置, flag_output);
                }
             
            }
            for (int i = 0; i < 10; i++)
            {
                for (byte k = 0; k < driver_IO_Board.stationClasses.Count; k++)
                {
                    bool flag_output = PLC.Device.Get_DeviceFast_Ex(pLC_Buttons_board_output[k][i].寫入元件位置);
                    if (driver_IO_Board[k].Output[i] != flag_output)
                    {
                        driver_IO_Board[k].Output[i] = flag_output;
                    }
                }
             
            }

        }
    }
}
