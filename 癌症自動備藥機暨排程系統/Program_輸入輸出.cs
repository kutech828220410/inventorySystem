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
        List<List<PLC_Button>> pLC_Buttons_board_input = new List<List<PLC_Button>>();
        List<List<PLC_Button>> pLC_Buttons_board_output = new List<List<PLC_Button>>();
        MySerialPort mySerial_IO = new MySerialPort();
        public void Program_輸入輸出_Init()
        {
            mySerial_IO.Init("COM1", 115200, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            H_Pannel_lib.Communication.UART_ConsoletWrite = false;
            H_Pannel_lib.Driver_IO_Board driver_IO_Board = new H_Pannel_lib.Driver_IO_Board();

            this.BoardUIInit();


            driver_IO_Board.ProgramEvent += Driver_IO_Board_ProgramEvent;
            driver_IO_Board.Init(mySerial_IO, new byte[] { 0,1,2 });
            this.plC_UI_Init.Add_Method(Program_輸入輸出);
        }
        public void Program_輸入輸出()
        {

        }

        private void BoardUIInit()
        {
            List<PLC_Button> pLC_Buttons_board0_input = new List<PLC_Button>();
            pLC_Buttons_board0_input.Add(plC_Button_X00);
            pLC_Buttons_board0_input.Add(plC_Button_X01);
            pLC_Buttons_board0_input.Add(plC_Button_X02);
            pLC_Buttons_board0_input.Add(plC_Button_X03);
            pLC_Buttons_board0_input.Add(plC_Button_X04);
            pLC_Buttons_board0_input.Add(plC_Button_X05);
            pLC_Buttons_board0_input.Add(plC_Button_X06);
            pLC_Buttons_board0_input.Add(plC_Button_X07);
            pLC_Buttons_board0_input.Add(plC_Button_X10);
            pLC_Buttons_board0_input.Add(plC_Button_X11);
            pLC_Buttons_board0_input.Add(plC_Button_X12);
            pLC_Buttons_board0_input.Add(plC_Button_X13);
            pLC_Buttons_board0_input.Add(plC_Button_X14);
            pLC_Buttons_board0_input.Add(plC_Button_X15);
            pLC_Buttons_board0_input.Add(plC_Button_X16);
            pLC_Buttons_board0_input.Add(plC_Button_X17);
            pLC_Buttons_board_input.Add(pLC_Buttons_board0_input);



            List<PLC_Button> pLC_Buttons_board0_output = new List<PLC_Button>();
            pLC_Buttons_board0_output.Add(plC_Button_Y00);
            pLC_Buttons_board0_output.Add(plC_Button_Y01);
            pLC_Buttons_board0_output.Add(plC_Button_Y02);
            pLC_Buttons_board0_output.Add(plC_Button_Y03);
            pLC_Buttons_board0_output.Add(plC_Button_Y04);
            pLC_Buttons_board0_output.Add(plC_Button_Y05);
            pLC_Buttons_board0_output.Add(plC_Button_Y06);
            pLC_Buttons_board0_output.Add(plC_Button_Y07);
            pLC_Buttons_board0_output.Add(plC_Button_Y10);
            pLC_Buttons_board0_output.Add(plC_Button_Y11);
            pLC_Buttons_board0_output.Add(plC_Button_Y12);
            pLC_Buttons_board0_output.Add(plC_Button_Y13);
            pLC_Buttons_board0_output.Add(plC_Button_Y14);
            pLC_Buttons_board0_output.Add(plC_Button_Y15);
            pLC_Buttons_board0_output.Add(plC_Button_Y16);
            pLC_Buttons_board0_output.Add(plC_Button_Y17);
            pLC_Buttons_board_output.Add(pLC_Buttons_board0_output);


            List<PLC_Button> pLC_Buttons_board1_input = new List<PLC_Button>();
            pLC_Buttons_board1_input.Add(plC_Button_X20);
            pLC_Buttons_board1_input.Add(plC_Button_X21);
            pLC_Buttons_board1_input.Add(plC_Button_X22);
            pLC_Buttons_board1_input.Add(plC_Button_X23);
            pLC_Buttons_board1_input.Add(plC_Button_X24);
            pLC_Buttons_board1_input.Add(plC_Button_X25);
            pLC_Buttons_board1_input.Add(plC_Button_X26);
            pLC_Buttons_board1_input.Add(plC_Button_X27);
            pLC_Buttons_board1_input.Add(plC_Button_X30);
            pLC_Buttons_board1_input.Add(plC_Button_X31);
            pLC_Buttons_board1_input.Add(plC_Button_X32);
            pLC_Buttons_board1_input.Add(plC_Button_X33);
            pLC_Buttons_board1_input.Add(plC_Button_X34);
            pLC_Buttons_board1_input.Add(plC_Button_X35);
            pLC_Buttons_board1_input.Add(plC_Button_X36);
            pLC_Buttons_board1_input.Add(plC_Button_X37);
            pLC_Buttons_board_input.Add(pLC_Buttons_board1_input);



            List<PLC_Button> pLC_Buttons_board1_output = new List<PLC_Button>();
            pLC_Buttons_board1_output.Add(plC_Button_Y20);
            pLC_Buttons_board1_output.Add(plC_Button_Y21);
            pLC_Buttons_board1_output.Add(plC_Button_Y22);
            pLC_Buttons_board1_output.Add(plC_Button_Y23);
            pLC_Buttons_board1_output.Add(plC_Button_Y24);
            pLC_Buttons_board1_output.Add(plC_Button_Y25);
            pLC_Buttons_board1_output.Add(plC_Button_Y26);
            pLC_Buttons_board1_output.Add(plC_Button_Y27);
            pLC_Buttons_board1_output.Add(plC_Button_Y30);
            pLC_Buttons_board1_output.Add(plC_Button_Y31);
            pLC_Buttons_board1_output.Add(plC_Button_Y32);
            pLC_Buttons_board1_output.Add(plC_Button_Y33);
            pLC_Buttons_board1_output.Add(plC_Button_Y34);
            pLC_Buttons_board1_output.Add(plC_Button_Y35);
            pLC_Buttons_board1_output.Add(plC_Button_Y36);
            pLC_Buttons_board1_output.Add(plC_Button_Y37);
            pLC_Buttons_board_output.Add(pLC_Buttons_board1_output);


            List<PLC_Button> pLC_Buttons_board2_input = new List<PLC_Button>();
            pLC_Buttons_board2_input.Add(plC_Button_X40);
            pLC_Buttons_board2_input.Add(plC_Button_X41);
            pLC_Buttons_board2_input.Add(plC_Button_X42);
            pLC_Buttons_board2_input.Add(plC_Button_X43);
            pLC_Buttons_board2_input.Add(plC_Button_X44);
            pLC_Buttons_board2_input.Add(plC_Button_X45);
            pLC_Buttons_board2_input.Add(plC_Button_X46);
            pLC_Buttons_board2_input.Add(plC_Button_X47);
            pLC_Buttons_board2_input.Add(plC_Button_X50);
            pLC_Buttons_board2_input.Add(plC_Button_X51);
            pLC_Buttons_board2_input.Add(plC_Button_X52);
            pLC_Buttons_board2_input.Add(plC_Button_X53);
            pLC_Buttons_board2_input.Add(plC_Button_X54);
            pLC_Buttons_board2_input.Add(plC_Button_X55);
            pLC_Buttons_board2_input.Add(plC_Button_X56);
            pLC_Buttons_board2_input.Add(plC_Button_X57);
            pLC_Buttons_board_input.Add(pLC_Buttons_board2_input);



            List<PLC_Button> pLC_Buttons_board2_output = new List<PLC_Button>();
            pLC_Buttons_board2_output.Add(plC_Button_Y40);
            pLC_Buttons_board2_output.Add(plC_Button_Y41);
            pLC_Buttons_board2_output.Add(plC_Button_Y42);
            pLC_Buttons_board2_output.Add(plC_Button_Y43);
            pLC_Buttons_board2_output.Add(plC_Button_Y44);
            pLC_Buttons_board2_output.Add(plC_Button_Y45);
            pLC_Buttons_board2_output.Add(plC_Button_Y46);
            pLC_Buttons_board2_output.Add(plC_Button_Y47);
            pLC_Buttons_board2_output.Add(plC_Button_Y50);
            pLC_Buttons_board2_output.Add(plC_Button_Y51);
            pLC_Buttons_board2_output.Add(plC_Button_Y52);
            pLC_Buttons_board2_output.Add(plC_Button_Y53);
            pLC_Buttons_board2_output.Add(plC_Button_Y54);
            pLC_Buttons_board2_output.Add(plC_Button_Y55);
            pLC_Buttons_board2_output.Add(plC_Button_Y56);
            pLC_Buttons_board2_output.Add(plC_Button_Y57);
            pLC_Buttons_board_output.Add(pLC_Buttons_board2_output);
        }
        private void Driver_IO_Board_ProgramEvent(H_Pannel_lib.Driver_IO_Board driver_IO_Board)
        {
            for(int i = 0; i < 16; i ++)
            {
                for (byte k = 0; k < driver_IO_Board.stationClasses.Count; k++)
                {
                    bool flag_input = driver_IO_Board[k].Input[i];
                    pLC_Buttons_board_input[k][i].Bool = flag_input;
                }
              
            }
            for (int i = 0; i < 16; i++)
            {
                for (byte k = 0; k < driver_IO_Board.stationClasses.Count; k++)
                {
                    bool flag_output = driver_IO_Board[k].Output[i];
                    PLC.Device.Set_Device(pLC_Buttons_board_output[k][i].讀取元件位置, flag_output);
                }
             
            }
            for (int i = 0; i < 16; i++)
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
