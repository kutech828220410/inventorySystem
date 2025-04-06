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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using System.Runtime.InteropServices;
using NPOI.SS.Formula.Functions;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);


        private const int WM_CLIPBOARDUPDATE = 0x031D;
        private MyTimerBasic MyTimerBasic_Keyboard = new MyTimerBasic();
        static public string keyDataString = "";

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                // 當剪貼簿更新時處理
                if (myConfigClass.鍵盤掃碼模式)
                {
                    try
                    {
                        IDataObject data = Clipboard.GetDataObject();
                        if (data != null && data.GetDataPresent(DataFormats.Text))
                        {
                            if (data.GetData(DataFormats.Text).ToString() != keyDataString)
                            {
                                keyDataString = data.GetData(DataFormats.Text).ToString();

                                if (keyDataString.Length > 2)
                                {
                                    string temp = keyDataString;
                                    //temp = temp.Replace("\n", "");
                                    //temp = temp.Replace("\r", "");
                                    byte[] byteArray = Encoding.UTF8.GetBytes(temp);

                                    Console.WriteLine($"keyDataString : {temp}");
                                    MySerialPort_Scanner01.SetReadByte(byteArray);

                                    MyTimerBasic_Keyboard.TickStop();
                                    MyTimerBasic_Keyboard.StartTickTime(200);

                                }
                            }

                        }
                    }
                    catch(Exception ex)
                    {   
                        Logger.Log(ex.Message);
                    }
                   
                }
              
            }
            base.WndProc(ref m);
        }

        static MySerialPort MySerialPort_Scanner01 = new MySerialPort();
        static MySerialPort MySerialPort_Scanner02 = new MySerialPort();
        static MySerialPort MySerialPort_Scanner03 = new MySerialPort();
        static MySerialPort MySerialPort_Scanner04 = new MySerialPort();
        static int NumOfConnectedScanner
        {
            get
            {
                int index = 0;
                if (myConfigClass.Scanner01_COMPort.StringIsEmpty() == false) index++;
                if (myConfigClass.Scanner02_COMPort.StringIsEmpty() == false) index++;
                if (myConfigClass.Scanner03_COMPort.StringIsEmpty() == false) index++;
                if (myConfigClass.Scanner04_COMPort.StringIsEmpty() == false) index++;
                return index;
            }
        }
        private enum enum_Scanner_陣列內容
        {
            病人姓名 = 4,
            藥品碼 = 14,
            藥袋序號 = 15,
            使用數量 = 9,
            病歷號 = 10,
            開方日期 = 11,
            開方時間 = 13,
            藥品名稱 = 0,
            中文名稱 = 1,
            頻次 = 7,
            包裝單位 = 8,

        }
        void Program_Scanner_RS232_Init()
        {
            AddClipboardFormatListener(this.Handle);

            MySerialPort_Scanner01.ConsoleWrite = true;
            MySerialPort_Scanner02.ConsoleWrite = true;
            MySerialPort_Scanner03.ConsoleWrite = true;
            MySerialPort_Scanner04.ConsoleWrite = true;
            if (PLC_Device_掃碼槍COM通訊.Bool)
            {
           
                if(myConfigClass.鍵盤掃碼模式 == false)
                {
                    if (!myConfigClass.Scanner01_COMPort.StringIsEmpty())
                    {
                        MySerialPort_Scanner01.Init(myConfigClass.Scanner01_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
                        if (!MySerialPort_Scanner01.IsConnected)
                        {
                            MyMessageBox.ShowDialog("掃碼器[01]初始化失敗!");
                        }
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
                if (!myConfigClass.Scanner03_COMPort.StringIsEmpty())
                {
                    MySerialPort_Scanner03.Init(myConfigClass.Scanner03_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
                    if (!MySerialPort_Scanner03.IsConnected)
                    {
                        MyMessageBox.ShowDialog("掃碼器[03]初始化失敗!");
                    }
                }
                if (!myConfigClass.Scanner04_COMPort.StringIsEmpty())
                {
                    MySerialPort_Scanner04.Init(myConfigClass.Scanner04_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
                    if (!MySerialPort_Scanner04.IsConnected)
                    {
                        MyMessageBox.ShowDialog("掃碼器[04]初始化失敗!");
                    }
                }
            }

            plC_UI_Init.Add_Method(sub_Program_Scanner_RS232);
        }
        void sub_Program_Scanner_RS232()
        {
            if (MyTimerBasic_Keyboard.IsTimeOut())
            {
                keyDataString = "";
            }
            sub_Program_Scanner01_讀取藥單資料();
            sub_Program_Scanner02_讀取藥單資料();
            sub_Program_Scanner03_讀取藥單資料();
            sub_Program_Scanner04_讀取藥單資料();
        }
        #region PLC_Scanner01_讀取藥單資料

        PLC_Device PLC_Device_Scanner01_讀取藥單資料 = new PLC_Device("");
        PLC_Device PLC_Device_Scanner01_讀取藥單資料_OK = new PLC_Device("");
        string[] Scanner01_讀取藥單資料_Array;
        MyTimer MyTimer_Scanner01_讀取藥單資料 = new MyTimer();
        MyTimer MyTimer_Scanner01_讀取藥單資料_結束延遲 = new MyTimer();
        int cnt_Program_Scanner01_讀取藥單資料 = 65534;
        void sub_Program_Scanner01_讀取藥單資料()
        {
            if (cnt_Program_Scanner01_讀取藥單資料 == 65534)
            {
                PLC_Device_Scanner01_讀取藥單資料.SetComment("PLC_Scanner01_讀取藥單資料");
                PLC_Device_Scanner01_讀取藥單資料_OK.SetComment("PLC_Scanner01_讀取藥單資料_OK");
                PLC_Device_Scanner01_讀取藥單資料.Bool = false;
                cnt_Program_Scanner01_讀取藥單資料 = 65535;
            }
            if (cnt_Program_Scanner01_讀取藥單資料 == 65535) cnt_Program_Scanner01_讀取藥單資料 = 1;
            if (cnt_Program_Scanner01_讀取藥單資料 == 1) cnt_Program_Scanner01_讀取藥單資料_檢查按下(ref cnt_Program_Scanner01_讀取藥單資料);
            if (cnt_Program_Scanner01_讀取藥單資料 == 2) cnt_Program_Scanner01_讀取藥單資料_初始化(ref cnt_Program_Scanner01_讀取藥單資料);
            if (cnt_Program_Scanner01_讀取藥單資料 == 3) cnt_Program_Scanner01_讀取藥單資料_等待接收延遲(ref cnt_Program_Scanner01_讀取藥單資料);
            if (cnt_Program_Scanner01_讀取藥單資料 == 4) cnt_Program_Scanner01_讀取藥單資料_檢查接收結果(ref cnt_Program_Scanner01_讀取藥單資料);
            if (cnt_Program_Scanner01_讀取藥單資料 == 5) cnt_Program_Scanner01_讀取藥單資料_結束延遲(ref cnt_Program_Scanner01_讀取藥單資料);
            if (cnt_Program_Scanner01_讀取藥單資料 == 6) cnt_Program_Scanner01_讀取藥單資料 = 65500;
            if (cnt_Program_Scanner01_讀取藥單資料 > 1) cnt_Program_Scanner01_讀取藥單資料_檢查放開(ref cnt_Program_Scanner01_讀取藥單資料);

            if (cnt_Program_Scanner01_讀取藥單資料 == 65500)
            {
                PLC_Device_Scanner01_讀取藥單資料.Bool = false;
                cnt_Program_Scanner01_讀取藥單資料 = 65535;
            }
        }
        void cnt_Program_Scanner01_讀取藥單資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            if (PLC_Device_Scanner01_讀取藥單資料.Bool)
            {
                MySerialPort_Scanner01.ClearReadByte();
                cnt++;
            }
        }
        void cnt_Program_Scanner01_讀取藥單資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_Scanner01_讀取藥單資料.Bool) cnt = 65500;
        }
        void cnt_Program_Scanner01_讀取藥單資料_初始化(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            PLC_Device_Scanner01_讀取藥單資料_OK.Bool = false;
            if (MySerialPort_Scanner01.ReadByte() != null)
            {
                MyTimer_Scanner01_讀取藥單資料.TickStop();
                MyTimer_Scanner01_讀取藥單資料.StartTickTime(200);
                cnt++;
            }

        }
        void cnt_Program_Scanner01_讀取藥單資料_等待接收延遲(ref int cnt)
        {

            if (MyTimer_Scanner01_讀取藥單資料.IsTimeOut())
            {
                cnt++;
            }

        }
        void cnt_Program_Scanner01_讀取藥單資料_檢查接收結果(ref int cnt)
        {
            try
            {
                if (MySerialPort_Scanner01.ReadByte() != null )
                {

                    string text = "";

                    text = MySerialPort_Scanner01.ReadString();
                    text = text.Replace("\0", "");
                    Console.WriteLine($"接收資料長度 : {text.Length} ");
                    MySerialPort_Scanner01.ClearReadByte();

                    this.領藥台_01_醫令條碼 = text;
                    PLC_Device_Scanner01_讀取藥單資料_OK.Bool = true;
                    Console.WriteLine($"接收資料內容 : {text} ");
                    cnt++;
                    return;

                }
            }
            catch
            {

            }
            finally
            {
                MyTimer_Scanner01_讀取藥單資料_結束延遲.TickStop();
                MyTimer_Scanner01_讀取藥單資料_結束延遲.StartTickTime(1500);
            }
          
        }
        void cnt_Program_Scanner01_讀取藥單資料_結束延遲(ref int cnt)
        {
            if (MyTimer_Scanner01_讀取藥單資料_結束延遲.IsTimeOut())
            {
                cnt++;
            }

        }










        #endregion
        #region PLC_Scanner02_讀取藥單資料

        PLC_Device PLC_Device_Scanner02_讀取藥單資料 = new PLC_Device("");
        PLC_Device PLC_Device_Scanner02_讀取藥單資料_OK = new PLC_Device("");
        string[] Scanner02_讀取藥單資料_Array;
        MyTimer MyTimer_Scanner02_讀取藥單資料 = new MyTimer();
        MyTimer MyTimer_Scanner02_讀取藥單資料_結束延遲 = new MyTimer();
        int cnt_Program_Scanner02_讀取藥單資料 = 65534;
        void sub_Program_Scanner02_讀取藥單資料()
        {
            if (cnt_Program_Scanner02_讀取藥單資料 == 65534)
            {
                PLC_Device_Scanner02_讀取藥單資料.SetComment("PLC_Scanner02_讀取藥單資料");
                PLC_Device_Scanner02_讀取藥單資料_OK.SetComment("PLC_Scanner02_讀取藥單資料_OK");
                PLC_Device_Scanner02_讀取藥單資料.Bool = false;
                cnt_Program_Scanner02_讀取藥單資料 = 65535;
            }
            if (cnt_Program_Scanner02_讀取藥單資料 == 65535) cnt_Program_Scanner02_讀取藥單資料 = 1;
            if (cnt_Program_Scanner02_讀取藥單資料 == 1) cnt_Program_Scanner02_讀取藥單資料_檢查按下(ref cnt_Program_Scanner02_讀取藥單資料);
            if (cnt_Program_Scanner02_讀取藥單資料 == 2) cnt_Program_Scanner02_讀取藥單資料_初始化(ref cnt_Program_Scanner02_讀取藥單資料);
            if (cnt_Program_Scanner02_讀取藥單資料 == 3) cnt_Program_Scanner02_讀取藥單資料_等待接收延遲(ref cnt_Program_Scanner02_讀取藥單資料);
            if (cnt_Program_Scanner02_讀取藥單資料 == 4) cnt_Program_Scanner02_讀取藥單資料_檢查接收結果(ref cnt_Program_Scanner02_讀取藥單資料);
            if (cnt_Program_Scanner02_讀取藥單資料 == 5) cnt_Program_Scanner02_讀取藥單資料_結束延遲(ref cnt_Program_Scanner02_讀取藥單資料);
            if (cnt_Program_Scanner02_讀取藥單資料 == 6) cnt_Program_Scanner02_讀取藥單資料 = 65500;
            if (cnt_Program_Scanner02_讀取藥單資料 > 1) cnt_Program_Scanner02_讀取藥單資料_檢查放開(ref cnt_Program_Scanner02_讀取藥單資料);

            if (cnt_Program_Scanner02_讀取藥單資料 == 65500)
            {
                PLC_Device_Scanner02_讀取藥單資料.Bool = false;
                cnt_Program_Scanner02_讀取藥單資料 = 65535;
            }
        }
        void cnt_Program_Scanner02_讀取藥單資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            if (PLC_Device_Scanner02_讀取藥單資料.Bool)
            {
                MySerialPort_Scanner02.ClearReadByte();
                cnt++;
            }
        }
        void cnt_Program_Scanner02_讀取藥單資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_Scanner02_讀取藥單資料.Bool) cnt = 65500;
        }
        void cnt_Program_Scanner02_讀取藥單資料_初始化(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            PLC_Device_Scanner02_讀取藥單資料_OK.Bool = false;
            if (MySerialPort_Scanner02.ReadByte() != null)
            {
                MyTimer_Scanner02_讀取藥單資料.TickStop();
                MyTimer_Scanner02_讀取藥單資料.StartTickTime(200);
                cnt++;
            }

        }
        void cnt_Program_Scanner02_讀取藥單資料_等待接收延遲(ref int cnt)
        {

            if (MyTimer_Scanner02_讀取藥單資料.IsTimeOut())
            {
                cnt++;
            }

        }
        void cnt_Program_Scanner02_讀取藥單資料_檢查接收結果(ref int cnt)
        {
            try
            {
                if (MySerialPort_Scanner02.ReadByte() != null)
                {
                    string text = "";

                    text = MySerialPort_Scanner02.ReadString();
                    text = text.Replace("\0", "");
                    Console.WriteLine($"接收資料長度 : {text.Length} ");
                    MySerialPort_Scanner02.ClearReadByte();

                    this.領藥台_02_醫令條碼 = text;
                    PLC_Device_Scanner02_讀取藥單資料_OK.Bool = true;
                    Console.WriteLine($"接收資料內容 : {text} ");
                    cnt++;
                    return;

                }
            }
            catch
            {

            }
            finally
            {
                MyTimer_Scanner02_讀取藥單資料_結束延遲.TickStop();
                MyTimer_Scanner02_讀取藥單資料_結束延遲.StartTickTime(1500);
            }
            
        }
        void cnt_Program_Scanner02_讀取藥單資料_結束延遲(ref int cnt)
        {
            if (MyTimer_Scanner02_讀取藥單資料_結束延遲.IsTimeOut())
            {
                cnt++;
            }

        }










        #endregion
        #region PLC_Scanner03_讀取藥單資料

        PLC_Device PLC_Device_Scanner03_讀取藥單資料 = new PLC_Device("");
        PLC_Device PLC_Device_Scanner03_讀取藥單資料_OK = new PLC_Device("");
        string[] Scanner03_讀取藥單資料_Array;
        MyTimer MyTimer_Scanner03_讀取藥單資料 = new MyTimer();
        MyTimer MyTimer_Scanner03_讀取藥單資料_結束延遲 = new MyTimer();
        int cnt_Program_Scanner03_讀取藥單資料 = 65534;
        void sub_Program_Scanner03_讀取藥單資料()
        {
            if (cnt_Program_Scanner03_讀取藥單資料 == 65534)
            {
                PLC_Device_Scanner03_讀取藥單資料.SetComment("PLC_Scanner03_讀取藥單資料");
                PLC_Device_Scanner03_讀取藥單資料_OK.SetComment("PLC_Scanner03_讀取藥單資料_OK");
                PLC_Device_Scanner03_讀取藥單資料.Bool = false;
                cnt_Program_Scanner03_讀取藥單資料 = 65535;
            }
            if (cnt_Program_Scanner03_讀取藥單資料 == 65535) cnt_Program_Scanner03_讀取藥單資料 = 1;
            if (cnt_Program_Scanner03_讀取藥單資料 == 1) cnt_Program_Scanner03_讀取藥單資料_檢查按下(ref cnt_Program_Scanner03_讀取藥單資料);
            if (cnt_Program_Scanner03_讀取藥單資料 == 2) cnt_Program_Scanner03_讀取藥單資料_初始化(ref cnt_Program_Scanner03_讀取藥單資料);
            if (cnt_Program_Scanner03_讀取藥單資料 == 3) cnt_Program_Scanner03_讀取藥單資料_等待接收延遲(ref cnt_Program_Scanner03_讀取藥單資料);
            if (cnt_Program_Scanner03_讀取藥單資料 == 4) cnt_Program_Scanner03_讀取藥單資料_檢查接收結果(ref cnt_Program_Scanner03_讀取藥單資料);
            if (cnt_Program_Scanner03_讀取藥單資料 == 5) cnt_Program_Scanner03_讀取藥單資料_結束延遲(ref cnt_Program_Scanner03_讀取藥單資料);
            if (cnt_Program_Scanner03_讀取藥單資料 == 6) cnt_Program_Scanner03_讀取藥單資料 = 65500;
            if (cnt_Program_Scanner03_讀取藥單資料 > 1) cnt_Program_Scanner03_讀取藥單資料_檢查放開(ref cnt_Program_Scanner03_讀取藥單資料);

            if (cnt_Program_Scanner03_讀取藥單資料 == 65500)
            {
                PLC_Device_Scanner03_讀取藥單資料.Bool = false;
                cnt_Program_Scanner03_讀取藥單資料 = 65535;
            }
        }
        void cnt_Program_Scanner03_讀取藥單資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            if (PLC_Device_Scanner03_讀取藥單資料.Bool)
            {
                MySerialPort_Scanner03.ClearReadByte();
                cnt++;
            }
        }
        void cnt_Program_Scanner03_讀取藥單資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_Scanner03_讀取藥單資料.Bool) cnt = 65500;
        }
        void cnt_Program_Scanner03_讀取藥單資料_初始化(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            PLC_Device_Scanner03_讀取藥單資料_OK.Bool = false;
            if (MySerialPort_Scanner03.ReadByte() != null)
            {
                MyTimer_Scanner03_讀取藥單資料.TickStop();
                MyTimer_Scanner03_讀取藥單資料.StartTickTime(200);
                cnt++;
            }

        }
        void cnt_Program_Scanner03_讀取藥單資料_等待接收延遲(ref int cnt)
        {

            if (MyTimer_Scanner03_讀取藥單資料.IsTimeOut())
            {
                cnt++;
            }

        }
        void cnt_Program_Scanner03_讀取藥單資料_檢查接收結果(ref int cnt)
        {
            try
            {
                if (MySerialPort_Scanner03.ReadByte() != null)
                {

                    string text = "";
                    text = MySerialPort_Scanner03.ReadString();
                    text = text.Replace("\0", "");
                    Console.WriteLine($"接收資料長度 : {text.Length} ");
                    MySerialPort_Scanner03.ClearReadByte();

                    this.領藥台_03_醫令條碼 = text;
                    PLC_Device_Scanner03_讀取藥單資料_OK.Bool = true;
                    Console.WriteLine($"接收資料內容 : {text} ");
                    cnt++;
                    return;
                }
            }
            catch
            {

            }
            finally
            {
                MyTimer_Scanner03_讀取藥單資料_結束延遲.TickStop();
                MyTimer_Scanner03_讀取藥單資料_結束延遲.StartTickTime(1500);
            }

        }
        void cnt_Program_Scanner03_讀取藥單資料_結束延遲(ref int cnt)
        {
            if (MyTimer_Scanner03_讀取藥單資料_結束延遲.IsTimeOut())
            {
                cnt++;
            }

        }










        #endregion
        #region PLC_Scanner04_讀取藥單資料

        PLC_Device PLC_Device_Scanner04_讀取藥單資料 = new PLC_Device("");
        PLC_Device PLC_Device_Scanner04_讀取藥單資料_OK = new PLC_Device("");
        string[] Scanner04_讀取藥單資料_Array;
        MyTimer MyTimer_Scanner04_讀取藥單資料 = new MyTimer();
        MyTimer MyTimer_Scanner04_讀取藥單資料_結束延遲 = new MyTimer();
        int cnt_Program_Scanner04_讀取藥單資料 = 65534;
        void sub_Program_Scanner04_讀取藥單資料()
        {
            if (cnt_Program_Scanner04_讀取藥單資料 == 65534)
            {
                PLC_Device_Scanner04_讀取藥單資料.SetComment("PLC_Scanner04_讀取藥單資料");
                PLC_Device_Scanner04_讀取藥單資料_OK.SetComment("PLC_Scanner04_讀取藥單資料_OK");
                PLC_Device_Scanner04_讀取藥單資料.Bool = false;
                cnt_Program_Scanner04_讀取藥單資料 = 65535;
            }
            if (cnt_Program_Scanner04_讀取藥單資料 == 65535) cnt_Program_Scanner04_讀取藥單資料 = 1;
            if (cnt_Program_Scanner04_讀取藥單資料 == 1) cnt_Program_Scanner04_讀取藥單資料_檢查按下(ref cnt_Program_Scanner04_讀取藥單資料);
            if (cnt_Program_Scanner04_讀取藥單資料 == 2) cnt_Program_Scanner04_讀取藥單資料_初始化(ref cnt_Program_Scanner04_讀取藥單資料);
            if (cnt_Program_Scanner04_讀取藥單資料 == 3) cnt_Program_Scanner04_讀取藥單資料_等待接收延遲(ref cnt_Program_Scanner04_讀取藥單資料);
            if (cnt_Program_Scanner04_讀取藥單資料 == 4) cnt_Program_Scanner04_讀取藥單資料_檢查接收結果(ref cnt_Program_Scanner04_讀取藥單資料);
            if (cnt_Program_Scanner04_讀取藥單資料 == 5) cnt_Program_Scanner04_讀取藥單資料_結束延遲(ref cnt_Program_Scanner04_讀取藥單資料);
            if (cnt_Program_Scanner04_讀取藥單資料 == 6) cnt_Program_Scanner04_讀取藥單資料 = 65500;
            if (cnt_Program_Scanner04_讀取藥單資料 > 1) cnt_Program_Scanner04_讀取藥單資料_檢查放開(ref cnt_Program_Scanner04_讀取藥單資料);

            if (cnt_Program_Scanner04_讀取藥單資料 == 65500)
            {
                PLC_Device_Scanner04_讀取藥單資料.Bool = false;
                cnt_Program_Scanner04_讀取藥單資料 = 65535;
            }
        }
        void cnt_Program_Scanner04_讀取藥單資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            if (PLC_Device_Scanner04_讀取藥單資料.Bool)
            {
                MySerialPort_Scanner04.ClearReadByte();
                cnt++;
            }
        }
        void cnt_Program_Scanner04_讀取藥單資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_Scanner04_讀取藥單資料.Bool) cnt = 65500;
        }
        void cnt_Program_Scanner04_讀取藥單資料_初始化(ref int cnt)
        {
            if (PLC_Device_掃碼槍COM通訊.Bool == false)
            {
                cnt = 65500;
                return;
            }
            PLC_Device_Scanner04_讀取藥單資料_OK.Bool = false;
            if (MySerialPort_Scanner04.ReadByte() != null)
            {
                MyTimer_Scanner04_讀取藥單資料.TickStop();
                MyTimer_Scanner04_讀取藥單資料.StartTickTime(200);
                cnt++;
            }

        }
        void cnt_Program_Scanner04_讀取藥單資料_等待接收延遲(ref int cnt)
        {

            if (MyTimer_Scanner04_讀取藥單資料.IsTimeOut())
            {
                cnt++;
            }

        }
        void cnt_Program_Scanner04_讀取藥單資料_檢查接收結果(ref int cnt)
        {
            try
            {
                if (MySerialPort_Scanner04.ReadByte() != null)
                {

                    string text = "";
                    text = MySerialPort_Scanner04.ReadString();
                    text = text.Replace("\0", "");
                    Console.WriteLine($"接收資料長度 : {text.Length} ");
                    MySerialPort_Scanner04.ClearReadByte();

                    this.領藥台_04_醫令條碼 = text;
                    PLC_Device_Scanner04_讀取藥單資料_OK.Bool = true;
                    Console.WriteLine($"接收資料內容 : {text} ");
                    cnt++;
                    return;

                }
            }
            catch
            {

            }
            finally
            {
                MyTimer_Scanner04_讀取藥單資料_結束延遲.TickStop();
                MyTimer_Scanner04_讀取藥單資料_結束延遲.StartTickTime(1500);
            }

        }
        void cnt_Program_Scanner04_讀取藥單資料_結束延遲(ref int cnt)
        {
            if (MyTimer_Scanner04_讀取藥單資料_結束延遲.IsTimeOut())
            {
                cnt++;
            }

        }










        #endregion
    }
}
