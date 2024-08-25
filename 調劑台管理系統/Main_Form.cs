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
[assembly: AssemblyVersion("1.2.2.6")]
[assembly: AssemblyFileVersion("1.2.2.6")]
namespace 調劑台管理系統
{

    public partial class Main_Form : Form
    {

        public bool ControlMode = false;
        private bool flag_Init = false;
        public static string ServerName = "";
        public static string ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
        public static string API_Server = "";
        public static string Order_URL = "";
        public static string OrderByCodeApi_URL = "";
        public static string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static RFID_FX600lib.RFID_FX600_UI _RFID_FX600_UI = null;
        public string 領藥台_01名稱
        {
            get
            {
                if (this.PLC_Device_主機扣賬模式.Bool == true) return $"{this.textBox_工程模式_領藥台_名稱.Text}_01";
                else return $"{this.textBox_工程模式_領藥台_名稱.Text}_S01";

            }
        }
        public string 領藥台_02名稱
        {
            get
            {
                if (this.PLC_Device_主機扣賬模式.Bool == true) return $"{this.textBox_工程模式_領藥台_名稱.Text}_02";
                else return $"{this.textBox_工程模式_領藥台_名稱.Text}_S02";
            }
        }
        public string 領藥台_03名稱
        {
            get
            {
                if (this.PLC_Device_主機扣賬模式.Bool == true) return $"{this.textBox_工程模式_領藥台_名稱.Text}_03";
                else return $"{this.textBox_工程模式_領藥台_名稱.Text}_S03";
            }
        }
        public string 領藥台_04名稱
        {
            get
            {
                if (this.PLC_Device_主機扣賬模式.Bool == true) return $"{this.textBox_工程模式_領藥台_名稱.Text}_04";
                else return $"{this.textBox_工程模式_領藥台_名稱.Text}_S04";
            }
        }

        private PrinterClass printerClass = new PrinterClass();
        private string FormText = "";
        private LadderConnection.LowerMachine PLC;
        MyTimer MyTimer_TickTime = new MyTimer();
        private Stopwatch stopwatch = new Stopwatch();
        List<Pannel_Locker> List_Locker = new List<Pannel_Locker>();
        Basic.MyConvert myConvert = new Basic.MyConvert();


        static public PLC_Device PLC_Device_主機輸出模式 = new PLC_Device("S1001");
        static public PLC_Device PLC_Device_申領_不需輸入申領量 = new PLC_Device("S5025");
        PLC_Device PLC_Device_主機扣賬模式 = new PLC_Device("S1002");
        PLC_Device PLC_Device_掃碼槍COM通訊 = new PLC_Device("S1003");
        PLC_Device PLC_Device_抽屜不鎖上 = new PLC_Device("S1004");
        PLC_Device PLC_Device_藥物辨識圖片顯示 = new PLC_Device("S1005");
        PLC_Device PLC_Device_S800 = new PLC_Device("S800");
        PLC_Device PLC_Device_刷藥袋有相同藥品需警示 = new PLC_Device("S5026");
        #region DBConfigClass
        private static string DBConfigFileName = $@"{currentDirectory}\DBConfig.txt";
        static public DBConfigClass dBConfigClass = new DBConfigClass();
        public class DBConfigClass
        {

            public string Name { get => name; set => name = value; }
            public string Api_Server { get => api_Server; set => api_Server = value; }

            private SQL_DataGridView.ConnentionClass dB_Basic = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_person_page = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_order_list = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_tradding = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_Medicine_Cloud = new SQL_DataGridView.ConnentionClass();

            private string web_URL = "";
            private string api_URL = "";
            private string login_URL = "";
            private string name = "";
            private string api_Server = "";

            private string orderApiURL = "";
            private string order_upload_ApiURL = "";
            private string orderByCodeApiURL = "";
            private string medApiURL = "";
            private string med_Update_ApiURL = "";



            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_Basic { get => dB_Basic; set => dB_Basic = value; }
            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_person_page { get => dB_person_page; set => dB_person_page = value; }
            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_order_list { get => dB_order_list; set => dB_order_list = value; }
            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_Medicine_Cloud { get => dB_Medicine_Cloud; set => dB_Medicine_Cloud = value; }
            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_tradding { get => dB_tradding; set => dB_tradding = value; }

            [JsonIgnore]
            public string OrderApiURL { get => orderApiURL; set => orderApiURL = value; }
            [JsonIgnore]
            public string MedApiURL { get => medApiURL; set => medApiURL = value; }
            [JsonIgnore]
            public string Api_URL { get => api_URL; set => api_URL = value; }
            [JsonIgnore]
            public string Web_URL { get => web_URL; set => web_URL = value; }
            [JsonIgnore]
            public string Login_URL { get => login_URL; set => login_URL = value; }
            [JsonIgnore]
            public string Med_Update_ApiURL { get => med_Update_ApiURL; set => med_Update_ApiURL = value; }
            [JsonIgnore]
            public string OrderByCodeApiURL { get => orderByCodeApiURL; set => orderByCodeApiURL = value; }
            [JsonIgnore]
            public string Order_upload_ApiURL { get => order_upload_ApiURL; set => order_upload_ApiURL = value; }
        }
        private void LoadDBConfig()
        {

            this.LoadcommandLineArgs();
            string jsonstr = MyFileStream.LoadFileAllText($"{DBConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(new DBConfigClass(), true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }
                MyMessageBox.ShowDialog($"未建立參數文件!請至子目錄設定{DBConfigFileName}");
                Application.Exit();
            }
            else
            {
                dBConfigClass = Basic.Net.JsonDeserializet<DBConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(dBConfigClass, true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }

            }
        }
        #endregion
        #region MyConfigClass
        private static string MyConfigFileName = $@"{currentDirectory}\MyConfig.txt";
        static public MyConfigClass myConfigClass = new MyConfigClass();
        public class MyConfigClass
        {

            private bool _ControlMode = false;
            private bool _主機扣帳模式 = false;
            private bool _主機輸出模式 = false;
            private bool _系統取藥模式 = false;
            private bool _RFID使用 = true;
            private bool _掃碼槍COM通訊 = true;
            private bool _藥物辨識圖片顯示 = true;
            private bool ePD583_Enable = true;
            private bool ePD266_Enable = true;
            private bool ePD1020_Enable = true;
            private bool rowsLED_Enable = true;
            private bool rFID_Enable = true;
            private bool pannel35_Enable = true;

            private int ePD583_Port = 29005;
            private int ePD266_Port = 29000;
            private int ePD1020_Port = 29012;
            private int rowsLED_Port = 29001;
            private int pannel35_Port = 29020;

            private bool _帳密登入_Enable = true;
            private bool _外部輸出 = false;
            private bool _全螢幕顯示 = true;
            private bool _鍵盤掃碼模式 = false;

            private string rFID_COMPort = "COM1";
            private string scanner01_COMPort = "COM2";
            private string scanner02_COMPort = "COM3";
            private string scanner03_COMPort = "";
            private string scanner04_COMPort = "";
            private string _藥物辨識網址 = "";


            public bool 主機扣帳模式 { get => _主機扣帳模式; set => _主機扣帳模式 = value; }
            public bool 主機輸出模式 { get => _主機輸出模式; set => _主機輸出模式 = value; }
            public bool RFID使用 { get => _RFID使用; set => _RFID使用 = value; }
            public bool 掃碼槍COM通訊 { get => _掃碼槍COM通訊; set => _掃碼槍COM通訊 = value; }
            public string RFID_COMPort { get => rFID_COMPort; set => rFID_COMPort = value; }
            public string Scanner01_COMPort { get => scanner01_COMPort; set => scanner01_COMPort = value; }
            public string Scanner02_COMPort { get => scanner02_COMPort; set => scanner02_COMPort = value; }
            public string Scanner03_COMPort { get => scanner03_COMPort; set => scanner03_COMPort = value; }
            public string Scanner04_COMPort { get => scanner04_COMPort; set => scanner04_COMPort = value; }
            public bool 藥物辨識圖片顯示 { get => _藥物辨識圖片顯示; set => _藥物辨識圖片顯示 = value; }
            public bool EPD583_Enable { get => ePD583_Enable; set => ePD583_Enable = value; }
            public bool EPD266_Enable { get => ePD266_Enable; set => ePD266_Enable = value; }
            public bool RowsLED_Enable { get => rowsLED_Enable; set => rowsLED_Enable = value; }
            public bool RFID_Enable { get => rFID_Enable; set => rFID_Enable = value; }
            public bool Pannel35_Enable { get => pannel35_Enable; set => pannel35_Enable = value; }
            public bool 帳密登入_Enable { get => _帳密登入_Enable; set => _帳密登入_Enable = value; }
            public string 藥物辨識網址 { get => _藥物辨識網址; set => _藥物辨識網址 = value; }
            public bool 外部輸出 { get => _外部輸出; set => _外部輸出 = value; }
            public bool 系統取藥模式 { get => _系統取藥模式; set => _系統取藥模式 = value; }
            public bool EPD1020_Enable { get => ePD1020_Enable; set => ePD1020_Enable = value; }
            public bool 全螢幕顯示 { get => _全螢幕顯示; set => _全螢幕顯示 = value; }
            public bool 鍵盤掃碼模式 { get => _鍵盤掃碼模式; set => _鍵盤掃碼模式 = value; }
            public int EPD583_Port { get => ePD583_Port; set => ePD583_Port = value; }
            public int EPD266_Port { get => ePD266_Port; set => ePD266_Port = value; }
            public int EPD1020_Port { get => ePD1020_Port; set => ePD1020_Port = value; }
            public int RowsLED_Port { get => rowsLED_Port; set => rowsLED_Port = value; }
            public int Pannel35_Port { get => pannel35_Port; set => pannel35_Port = value; }
            public bool ControlMode { get => _ControlMode; set => _ControlMode = value; }
        }
        private void LoadMyConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($"{MyConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {
                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(new MyConfigClass(), true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }
                MyMessageBox.ShowDialog($"未建立參數文件!請至子目錄設定{MyConfigFileName}");
                Application.Exit();
            }
            else
            {
                myConfigClass = Basic.Net.JsonDeserializet<MyConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass, true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }

            }

        }
        #endregion
        #region MyParameter
        private static string MyParameterFileName = $@"{currentDirectory}\parameter.txt";
        static public MyParameter myParameter = new MyParameter();
        public class MyParameter
        {

            private List<string> _交班開鎖抽屜 = new List<string>();

            public List<string> 交班開鎖抽屜 { get => _交班開鎖抽屜; set => _交班開鎖抽屜 = value; }
        }
        static public void SaveMyParameter()
        {
            string jsonstr = myParameter.JsonSerializationt();
            if (!MyFileStream.SaveFile($"{MyParameterFileName}", jsonstr))
            {
                MyMessageBox.ShowDialog($"建立{MyParameterFileName}檔案失敗!");

            }
        }
        static public void LoadMyParameter()
        {
            string jsonstr = MyFileStream.LoadFileAllText($"{MyParameterFileName}");
            myParameter = jsonstr.JsonDeserializet<MyParameter>();
            if (myParameter == null)
            {
                myParameter = new MyParameter();
                SaveMyParameter();
            }

        }
        #endregion

        public Main_Form()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // 使用雙重緩衝
            BufferedGraphicsContext currentContext;
            BufferedGraphics myBuffer;

            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            // 在緩衝區域進行繪製
            Graphics g = myBuffer.Graphics;
            g.Clear(this.BackColor); // 清除背景
            base.OnPaint(new PaintEventArgs(g, this.ClientRectangle));

            // 將緩衝區域的內容繪製到表單
            myBuffer.Render(e.Graphics);
            myBuffer.Dispose(); // 釋放緩衝區資源
        }
        private void Main_Form_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
            {
                MyDialog.form = this.FindForm();
                LoadingForm.form = this.FindForm();
                MyMessageBox.form = this.FindForm();
                Dialog_交易紀錄明細.form = this.FindForm();
                Dialog_NumPannel.form = this.FindForm();
                Dialog_輸入批號.form = this.FindForm();
                Dialog_輸入效期.form = this.FindForm();
                Dialog_輸入藥品碼.form = this.FindForm();
                Dialog_條碼輸入.form = this.FindForm();
                Dialog_醫令退藥.form = this.FindForm();
                Dialog_設定產出時間.form = this.FindForm();
                Dialog_RFID領退藥頁面.form = this.FindForm();
                Dialog_輸入輸出設定.form = this.FindForm();
                Dialog_新增容器設定.form = this.FindForm();
                Dialog_選擇效期.form = this.FindForm();
                Dialog_收支原因設定.form = this.FindForm();
                Dialog_收支原因選擇.form = this.FindForm();
                Dialog_DateTime.form = this.FindForm();
                Dialog_條碼管理.form = this.FindForm();
                Dialog_使用者登入.form = this.FindForm();
                Dialog_盤點數量錯誤.form = this.FindForm();
                Dialog_病歷號輸入.form = this.FindForm();
                Dialog_藥檔維護.form = this.FindForm();
                Dialog_錯誤提示.form = this.FindForm();
                Dialog_共用區設置.form = this.FindForm();
                Dialog_AlarmForm.form = this.FindForm();
                Dialog_藥品調出.form = this.FindForm();
                Dialog_交班對點.form = this.FindForm();
                Dialog_藥品群組.form = this.FindForm();
                Dialog_異常通知.form = this.FindForm();
                Dialog_申領.form = this.FindForm();
                Dialog_批次入庫.form = this.FindForm();


                LoadDBConfig();
                LoadMyConfig();

                if (myConfigClass.ControlMode) ControlMode = true;

                ApiServerSetting();

                this.stopwatch.Start();

                this.FormText = this.Text;
                this.plC_UI_Init.音效 = false;
                this.plC_UI_Init.全螢幕顯示 = myConfigClass.全螢幕顯示;
           
                this.plC_UI_Init.UI_Finished_Event += PlC_UI_Init_UI_Finished_Event;
                this.plC_UI_Init.Run(this.FindForm(), this.lowerMachine_Panel);


                Basic.Keyboard.Hook.KeyDown += Hook_KeyDown;
                Basic.Keyboard.Hook.MouseDown += Hook_MouseDown;
                Basic.Keyboard.Hook.MouseUp += Hook_MouseUp;
                this.button_調劑台切換.Click += Button_調劑台切換_Click;
                Basic.MyMessageBox.音效 = false;
                string ProcessName = "WINWORD";//換成想要結束的進程名字
                System.Diagnostics.Process[] MyProcess = System.Diagnostics.Process.GetProcessesByName(ProcessName);
                for (int i = 0; i < MyProcess.Length; i++)
                {
                    MyProcess[i].Kill();
                }
                printerClass.Init();
                printerClass.PrintPageEvent += PrinterClass_PrintPageEvent;


                this.plC_ScreenPage_Main.TabChangeEvent += PlC_ScreenPage_Main_TabChangeEvent;
                this.plC_ScreenPage_調劑樣式.Resize += PlC_ScreenPage_調劑樣式_Resize;
                this.ToolStripMenuItem_顯示主控台.Click += ToolStripMenuItem_顯示主控台_Click;
                this.ToolStripMenuItem_隱藏主控台.Click += ToolStripMenuItem_隱藏主控台_Click;
            }
        }

        #region Event
        private void PlC_ScreenPage_Main_TabChangeEvent(string PageText)
        {
            if (this.plC_ScreenPage_Main.PageText == "後台登入")
            {
                panel_主畫面登入.Location = new Point((後台登入.Width - panel_主畫面登入.Width) / 2, (後台登入.Height - panel_主畫面登入.Height) / 2);
            }
        }
        private void PlC_UI_Init_UI_Finished_Event()
        {
            if (myConfigClass.全螢幕顯示 == false)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.PLC = this.lowerMachine_Panel.GetlowerMachine();

            PLC_Device_主機輸出模式.Bool = myConfigClass.主機輸出模式;
            PLC_Device_主機扣賬模式.Bool = myConfigClass.主機扣帳模式;
            PLC_Device_掃碼槍COM通訊.Bool = myConfigClass.掃碼槍COM通訊;
            PLC_Device_藥物辨識圖片顯示.Bool = myConfigClass.藥物辨識圖片顯示;
            PLC_Device_S800.Bool = false;


            if (myConfigClass.Scanner01_COMPort.StringIsEmpty())
            {
                rJ_Pannel_領藥台_01.Visible = false;
            }
            if (myConfigClass.Scanner02_COMPort.StringIsEmpty())
            {
                rJ_Pannel_領藥台_02.Visible = false;
            }
            if (myConfigClass.Scanner03_COMPort.StringIsEmpty())
            {
                rJ_Pannel_領藥台_03.Visible = false;
            }
            if (myConfigClass.Scanner04_COMPort.StringIsEmpty())
            {
                rJ_Pannel_領藥台_04.Visible = false;
            }
            if (myConfigClass.Scanner03_COMPort.StringIsEmpty() && myConfigClass.Scanner04_COMPort.StringIsEmpty())
            {
                panel_領藥台_03_04.Visible = false;
            }
            if (dBConfigClass.Web_URL.StringIsEmpty()) plC_RJ_GroupBox_後台網址_QRCODE.Visible = false;
            else
            {
                pictureBox_後台網址_QRCODE.Image = H_Pannel_lib.Communication.CreateQRCode(dBConfigClass.Web_URL, pictureBox_後台網址_QRCODE.Width, pictureBox_後台網址_QRCODE.Height);
            }
            plC_CheckBox_調劑畫面合併相同藥品.Bool = false;
            if (this.ControlMode)
            {
                this.plC_RJ_GroupBox_調劑台切換.Visible = true;
                PLC_Device_主機輸出模式.Bool = false;
                PLC_Device_主機扣賬模式.Bool = false;
                PLC_Device_掃碼槍COM通訊.Bool = false;

                PLC_Device_藥物辨識圖片顯示.Bool = false;
                myConfigClass.RFID使用 = false;
                this.plC_RJ_ScreenButton_調劑作業.顯示讀取位置 = "M8001";
                this.plC_RJ_ScreenButton_管制抽屜.顯示讀取位置 = "M8001";
                this.plC_RJ_ScreenButton_交班作業.顯示讀取位置 = "M8000";
                this.plC_RJ_ScreenButton_收支作業.顯示讀取位置 = "M8001";
                this.plC_RJ_ScreenButton_交易紀錄查詢.顯示讀取位置 = "M8000";
                this.plC_RJ_ScreenButton_醫令資料.顯示讀取位置 = "M8000";
                this.plC_RJ_ScreenButton_藥品資料.顯示讀取位置 = "M8000";
                this.plC_RJ_ScreenButton_人員資料.顯示讀取位置 = "M8000";
                this.plC_RJ_ScreenButton_儲位管理.顯示讀取位置 = "M8000";
                this.plC_RJ_ScreenButton_盤點作業.顯示讀取位置 = "M8001";
                this.plC_RJ_ScreenButton_工程模式.顯示讀取位置 = "M8001";
                this.plC_ScreenPage_Main.SelecteTabText("後台登入");
            }

            PLC_UI_Init.Set_PLC_ScreenPage(panel_Main, this.plC_ScreenPage_Main);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_交班作業, this.plC_ScreenPage_交班作業);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_藥品資料, this.plC_ScreenPage_藥品資料);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_系統, this.plC_ScreenPage_系統);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_系統_Pannel設定, this.plC_ScreenPage_系統_Pannel設定);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_儲位管理, this.plC_ScreenPage_儲位管理);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_人員資料, this.plC_ScreenPage_人員資料);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_盤點作業, this.plC_ScreenPage_盤點作業);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_交易紀錄查詢, this.plC_ScreenPage_交易紀錄查詢);

            this.plC_RJ_ScreenButton_EPD583.Visible = myConfigClass.EPD583_Enable;
            this.plC_RJ_ScreenButton_EPD266.Visible = myConfigClass.EPD266_Enable;
            this.plC_RJ_ScreenButton_EPD1020.Visible = myConfigClass.EPD1020_Enable;
            this.plC_RJ_ScreenButton_RowsLED.Visible = myConfigClass.RowsLED_Enable;
            this.plC_RJ_ScreenButton_RFID.Visible = myConfigClass.RFID_Enable;
            this.plC_RJ_ScreenButton_Pannel35.Visible = myConfigClass.Pannel35_Enable;


            this.DBConfigInit();


            this.Program_Scanner_RS232_Init();
            this.Program_系統_Init();

            this.Program_醫令資料_Init();
            this.Program_藥品資料_藥檔資料_Init();

            this.Program_儲位管理_EPD583_Init();
            this.Program_儲位管理_EPD266_Init();
            this.Program_儲位管理_EPD1020_Init();
            this.Program_儲位管理_RowsLED_Init();
            this.Program_儲位管理_RFID_Init();
            this.Program_儲位管理_Pannel35_Init();
            this.Program_共用區_Init();

            this.Program_取藥堆疊資料_Init();
            if (!this.ControlMode) this.Program_調劑作業_Init();


            this.Program_藥品資料_儲位總庫存表_Init();
            this.Program_藥品資料_儲位效期表_Init();
            this.Program_藥品資料_管藥設定_Init();

            this.Program_雲端藥檔_Init();
            this.Program_人員資料_Init();
            this.Program_工程模式_Init();
            this.Program_交易記錄查詢_Init();
            this.Program_效期管理_Init();

            this.Program_後台登入_Init();
            this.Program_批次領藥_Init();


            this.Program_管制抽屜_Init();
            this.Program_設備資料_Init();
            this.Program_交班作業_對點作業_Init();
            this.Program_交班作業_管制結存_Init();
            this.Program_交班作業_交班表_Init();
            this.Program_藥品管制方式設定_Init();
            this.Program_藥品設定表_Init();
      

            this.sub_Program_盤點作業_定盤_Init();
            this.sub_Program_盤點作業_新增盤點_Init();
            this.sub_Program_盤點作業_單號查詢_Init();
            this.sub_Program_盤點作業_資料庫_Init();
            this.Program_異常通知_Init();

            if (!this.ControlMode) this.Program_輸出入檢查_Init();
            this.Program_收支作業_Init();
            this.Program_指紋辨識_Init();


            this.Program_異常通知_盤點錯誤_Init();

            this.LoadConfig工程模式();

            if (!this.ControlMode) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(領藥台_01名稱);
            if (!this.ControlMode) this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(領藥台_02名稱);

            this.drawerUI_EPD_1020.Set_UDP_WriteTime(10);
            this.drawerUI_EPD_583.Set_UDP_WriteTime(10);

            _storageUI_EPD_266 = this.storageUI_EPD_266;
            _storageUI_WT32 = this.storageUI_WT32;
            _drawerUI_EPD_583 = this.drawerUI_EPD_583;

            Task task = Task.Run(new Action(delegate
            {
                if (!this.ControlMode) Function_從SQL取得儲位到本地資料();
            }));
            if (!this.ControlMode) this.plC_ScreenPage_Main.SelecteTabText("調劑作業");


            RFID_Iint();


            flag_Init = true;
        }
        private void PrinterClass_PrintPageEvent(object sender, Graphics g, int width, int height, int page_num)
        {
            Rectangle rectangle = new Rectangle(0, 0, width, height);
            using (Bitmap bitmap = printerClass.GetSheetClass(page_num).GetBitmap(width, height, 0.63, H_Alignment.Center, V_Alignment.Top, 0, 50))
            {
                rectangle.Height = bitmap.Height;
                g.DrawImage(bitmap, rectangle);
            }
        }
        private void Hook_MouseDown(int nCode, int mouse_x, int mouse_y)
        {
            if (this.CanFocus)
            {
                this.myTimer_登出計時.TickStop();
                this.myTimer_登出計時.StartTickTime(1200000);
            }
        }
        private void Hook_KeyDown(int nCode, IntPtr wParam, Keys Keys)
        {
            if (this.CanFocus)
            {
                this.myTimer_登出計時.TickStop();
                this.myTimer_登出計時.StartTickTime(1200000);
            }
        }
        private void Hook_MouseUp(int nCode, int mouse_x, int mouse_y)
        {

        }
        private void plC_RJ_Button_系統更新_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Update();
        }
        private void Button_調劑台切換_Click(object sender, EventArgs e)
        {
            string DPS_Name = this.comboBox_調劑台名稱.Text;
            if (DPS_Name.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("未選擇調劑台!");
                return;
            }
            this.ApiServerSetting(DPS_Name, "一般資料");
            this.DBConfigInit();
            MyMessageBox.ShowDialog("切換完成!");
        }
        private void Main_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void PlC_ScreenPage_調劑樣式_Resize(object sender, EventArgs e)
        {     
            Control control = sender as Control;
            int basic_width = 1660;
            int offset_width = (control.Width -basic_width) / 2;

            if (NumOfConnectedScanner > 1)
            {
                if (flag_Init) this.sqL_DataGridView_領藥台_01_領藥內容.Set_ColumnWidth(355 + offset_width, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
                if (flag_Init) this.sqL_DataGridView_領藥台_02_領藥內容.Set_ColumnWidth(355 + offset_width, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
                if (flag_Init) this.sqL_DataGridView_領藥台_03_領藥內容.Set_ColumnWidth(355 + offset_width, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
                if (flag_Init) this.sqL_DataGridView_領藥台_04_領藥內容.Set_ColumnWidth(355 + offset_width, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            }
            if (NumOfConnectedScanner == 4)
            {
           
                int width = control.Width / 2 - 10;
                int height = control.Height / 2 - 10;
                rJ_Pannel_領藥台_01.Width = width;
                rJ_Pannel_領藥台_02.Width = width;
                rJ_Pannel_領藥台_03.Width = width;
                rJ_Pannel_領藥台_04.Width = width;
                panel_領藥台_01_02.Height = height;
                panel_領藥台_03_04.Height = height;
                panel_領藥台_01_藥品資訊.Visible = false;
                panel_領藥台_02_藥品資訊.Visible = false;
            }
            else if (NumOfConnectedScanner == 3)
            {
                int width = control.Width / 2 - 10;
                int height = control.Height / 2 - 10;
                rJ_Pannel_領藥台_01.Width = width;
                rJ_Pannel_領藥台_02.Width = width;
                rJ_Pannel_領藥台_03.Width = width;
                rJ_Pannel_領藥台_04.Width = width;
                panel_領藥台_01_02.Height = height;
                panel_領藥台_03_04.Height = height;
                panel_領藥台_01_藥品資訊.Visible = false;
                panel_領藥台_02_藥品資訊.Visible = false;
            }
            else if (NumOfConnectedScanner == 2)
            {
                int width = control.Width / 2 - 10;
                int height = control.Height / 1 - 10;
                rJ_Pannel_領藥台_01.Width = width;
                rJ_Pannel_領藥台_02.Width = width;
                rJ_Pannel_領藥台_03.Width = width;
                rJ_Pannel_領藥台_04.Width = width;
                panel_領藥台_01_02.Height = height;
                panel_領藥台_03_04.Height = height;

                pictureBox_領藥台_01_藥品圖片01.Width = panel_領藥台_01_藥品圖片.Width / 2;
                pictureBox_領藥台_01_藥品圖片02.Width = panel_領藥台_01_藥品圖片.Width / 2;

                pictureBox_領藥台_02_藥品圖片01.Width = panel_領藥台_02_藥品圖片.Width / 2;
                pictureBox_領藥台_02_藥品圖片02.Width = panel_領藥台_02_藥品圖片.Width / 2;
            }
            else if (NumOfConnectedScanner == 1)
            {
                int width = control.Width / 1 - 10;
                int height = control.Height / 1 - 10;
                rJ_Pannel_領藥台_01.Width = width;
                rJ_Pannel_領藥台_02.Width = width;
                rJ_Pannel_領藥台_03.Width = width;
                rJ_Pannel_領藥台_04.Width = width;
                panel_領藥台_01_02.Height = height;
                panel_領藥台_03_04.Height = height;

                pictureBox_領藥台_01_藥品圖片01.Width = panel_領藥台_01_藥品圖片.Width / 2;
                pictureBox_領藥台_01_藥品圖片02.Width = panel_領藥台_01_藥品圖片.Width / 2;

                pictureBox_領藥台_02_藥品圖片01.Width = panel_領藥台_02_藥品圖片.Width / 2;
                pictureBox_領藥台_02_藥品圖片02.Width = panel_領藥台_02_藥品圖片.Width / 2;
            }
        }
        private void ToolStripMenuItem_顯示主控台_Click(object sender, EventArgs e)
        {
            Basic.Screen.ShowConsole();
        }
        private void ToolStripMenuItem_隱藏主控台_Click(object sender, EventArgs e)
        {
            Basic.Screen.CloseConsole();
        }
        #endregion
        #region Function
        private void RFID_Iint()
        {
            Task.Run(new Action(delegate
            {
                MyTimer MyTimer_rfiD_FX600_UI_Init = new MyTimer();
                bool flag_rfiD_FX600_UI_Init = false;
                MyTimer_rfiD_FX600_UI_Init.TickStop();
                MyTimer_rfiD_FX600_UI_Init.StartTickTime(5000);
                while (true)
                {
                    _RFID_FX600_UI = this.rfiD_FX600_UI;
                    if (myConfigClass.RFID使用 == false) break;

                    if (MyTimer_rfiD_FX600_UI_Init.IsTimeOut() && !flag_rfiD_FX600_UI_Init)
                    {
                        if (myConfigClass.RFID使用)
                        {
                            int num = 1;
                            if (myConfigClass.Scanner01_COMPort.StringIsEmpty() == false)
                            {
                                num++;
                            }
                            if (myConfigClass.Scanner02_COMPort.StringIsEmpty() == false)
                            {
                                num++;
                            }
                            if (myConfigClass.Scanner03_COMPort.StringIsEmpty() == false)
                            {
                                num++;
                            }
                            if (myConfigClass.Scanner04_COMPort.StringIsEmpty() == false)
                            {
                                num++;
                            }
                        
                            this.rfiD_FX600_UI.Init(RFID_FX600lib.RFID_FX600_UI.Baudrate._9600, num, myConfigClass.RFID_COMPort);
                           
                        }
                        flag_rfiD_FX600_UI_Init = true;
                        break;

                    }
                    System.Threading.Thread.Sleep(100);
                }
            }));
        }
        private void LoadcommandLineArgs()
        {
            string jsonstr = MyFileStream.LoadFileAllText($"{DBConfigFileName}");
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Length >= 3)
            {
                dBConfigClass.Api_Server = commandLineArgs[1];
                dBConfigClass.Name = commandLineArgs[2];
                if (commandLineArgs.Length == 4)
                {
                    this.ControlMode = (commandLineArgs[3] == true.ToString());
           
                }
                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(dBConfigClass, true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }
                return;
            }
        }
        private bool flag_DBConfigInit = false;
        private void DBConfigInit()
        {
            this.pannel_Locker_Design.Init(dBConfigClass.DB_Basic);
            Dialog_RFID領退藥頁面.connentionClass = dBConfigClass.DB_Basic;
            SQLUI.SQL_DataGridView.SQL_Set_Properties(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, this.FindForm());
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_批次領藥資料, dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_藥品資料_藥檔資料, dBConfigClass.DB_Basic);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_雲端藥檔, dBConfigClass.DB_Medicine_Cloud);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_人員資料, dBConfigClass.DB_person_page);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_醫令資料, dBConfigClass.DB_order_list);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_交易記錄查詢, dBConfigClass.DB_tradding);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_管制抽屜權限資料, dBConfigClass.DB_Basic);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_設備資料, dBConfigClass.DB_Medicine_Cloud);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_藥品管制方式設定, dBConfigClass.DB_Medicine_Cloud);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_藥品設定表, dBConfigClass.DB_Medicine_Cloud);
            SQLUI.SQL_DataGridView.ConnentionClass dB_local = new SQL_DataGridView.ConnentionClass();
            dB_local.IP = dBConfigClass.DB_Basic.IP;
            dB_local.DataBaseName = dBConfigClass.DB_Basic.DataBaseName;
            dB_local.Port = dBConfigClass.DB_Basic.Port;
            dB_local.UserName = "user";
            dB_local.Password = "66437068";
            dB_local.MySqlSslMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_取藥堆疊母資料, dB_local);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_取藥堆疊子資料, dB_local);

            this.sqL_DataGridView_藥品資料_藥檔資料.SQL_Reset();
            this.sqL_DataGridView_人員資料.SQL_Reset();
            this.sqL_DataGridView_醫令資料.SQL_Reset();
            this.sqL_DataGridView_管制抽屜權限資料.SQL_Reset();
            this.sqL_DataGridView_設備資料.SQL_Reset();
            this.sqL_DataGridView_藥品管制方式設定.SQL_Reset();
            this.sqL_DataGridView_藥品設定表.SQL_Reset();
            this.sqL_DataGridView_雲端藥檔.SQL_Reset();
            this.sqL_DataGridView_交易記錄查詢.SQL_Reset();

            this.drawerUI_EPD_583.Init(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, 30005, myConfigClass.EPD583_Port);
            this.drawerUI_EPD_1020.Init(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, 30012, myConfigClass.EPD1020_Port);
            this.storageUI_EPD_266.Init(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, 30000, myConfigClass.EPD266_Port);
            this.rowsLEDUI.Init(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, 30001, myConfigClass.RowsLED_Port);
            this.rfiD_UI.Init(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode);
            this.storageUI_WT32.Init(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, 30020, myConfigClass.Pannel35_Port);
          
            if (flag_DBConfigInit == true)
            {
                this.sqL_DataGridView_儲位管理_EPD266_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
                this.sqL_DataGridView_儲位管理_EPD1020_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
                this.sqL_DataGridView_儲位管理_EPD583_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
                this.sqL_DataGridView_儲位管理_Pannel35_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
                this.sqL_DataGridView_儲位管理_RFID_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
                this.sqL_DataGridView_儲位管理_RowsLED_藥品資料_藥檔資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
            }

            flag_DBConfigInit = true;
            this.DeviceBasicClass_儲位庫存.Init(dBConfigClass.DB_Basic, "devicebasic_jsonstring");
        }
        private void ApiServerSetting()
        {
            
            if (ControlMode)
            {
                this.ApiServerSetting(dBConfigClass.Name, "一般資料");
            }
            else
            {
                this.ApiServerSetting(dBConfigClass.Name, "一般資料(LAN)");
            }

        }
        private void ApiServerSetting(string Name, string basicName)
        {
            this.textBox_工程模式_領藥台_名稱.Text = Name;
            this.Text = $"智慧調劑台管理系統 Ver{this.ProductVersion} [{this.textBox_工程模式_領藥台_名稱.Text}] {(this.ControlMode ? "****控制中心模式****" : "")}";
            this.label_版本.Text = $"Ver {this.ProductVersion} [{this.textBox_工程模式_領藥台_名稱.Text}] {(this.ControlMode ? "****控制中心模式****" : "")}";
            string json_result = Basic.Net.WEBApiGet($"{dBConfigClass.Api_Server}/api/ServerSetting");
            if (json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("API Server 連結失敗!");
                return;
            }
            Console.WriteLine(json_result);
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<HIS_DB_Lib.ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            HIS_DB_Lib.ServerSettingClass serverSettingClass;
            ServerName = Name;
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, basicName);
            List<string> DPS_Names = (from temp in serverSettingClasses
                                      where temp.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                      select temp.設備名稱).Distinct().ToList();
            if (!flag_Init) comboBox_調劑台名稱.DataSource = DPS_Names;

            if (serverSettingClass != null)
            {
                dBConfigClass.DB_Basic.IP = serverSettingClass.Server;
                dBConfigClass.DB_Basic.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_Basic.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_Basic.UserName = serverSettingClass.User;
                dBConfigClass.DB_Basic.Password = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.人員資料);
            if (serverSettingClass != null)
            {
                dBConfigClass.DB_person_page.IP = serverSettingClass.Server;
                dBConfigClass.DB_person_page.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_person_page.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_person_page.UserName = serverSettingClass.User;
                dBConfigClass.DB_person_page.Password = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.藥檔資料);
            if (serverSettingClass != null)
            {
                dBConfigClass.DB_Medicine_Cloud.IP = serverSettingClass.Server;
                dBConfigClass.DB_Medicine_Cloud.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_Medicine_Cloud.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_Medicine_Cloud.UserName = serverSettingClass.User;
                dBConfigClass.DB_Medicine_Cloud.Password = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, "醫囑資料");
            if (serverSettingClass != null)
            {
                dBConfigClass.DB_order_list.IP = serverSettingClass.Server;
                dBConfigClass.DB_order_list.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_order_list.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_order_list.UserName = serverSettingClass.User;
                dBConfigClass.DB_order_list.Password = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.交易紀錄資料);
            if (serverSettingClass != null)
            {
                dBConfigClass.DB_tradding.IP = serverSettingClass.Server;
                dBConfigClass.DB_tradding.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_tradding.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_tradding.UserName = serverSettingClass.User;
                dBConfigClass.DB_tradding.Password = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.API02);
            if (serverSettingClass != null)
            {
                dBConfigClass.Api_URL = serverSettingClass.Server;
                API_Server = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.Order_API);
            if (serverSettingClass != null) dBConfigClass.OrderApiURL = serverSettingClass.Server;
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, "Order_By_Code_API");
            if (serverSettingClass != null) dBConfigClass.OrderByCodeApiURL = serverSettingClass.Server;
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, "Order_upload_API");
            if (serverSettingClass != null) dBConfigClass.Order_upload_ApiURL = serverSettingClass.Server;


            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.Med_API);
            if (serverSettingClass != null) dBConfigClass.MedApiURL = serverSettingClass.Server;
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.Website);
            if (serverSettingClass != null) dBConfigClass.Web_URL = serverSettingClass.Server;
            serverSettingClass = serverSettingClasses.MyFind("Main", enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API_Login);
            if (serverSettingClass != null) dBConfigClass.Login_URL = serverSettingClass.Server;


            OrderByCodeApi_URL = dBConfigClass.OrderByCodeApiURL;
            Order_URL = dBConfigClass.OrderApiURL;
            if (OrderByCodeApi_URL.StringIsEmpty())
            {
                OrderByCodeApi_URL = Order_URL;
            }
        }
        new private void Update()
        {
            if (this.ftp_DounloadUI.DownloadFile())
            {
                if (this.ftp_DounloadUI.SaveFile())
                {
                    this.ftp_DounloadUI.RunFile(this.FindForm());
                }
                else
                {
                    Basic.MyMessageBox.ShowDialog("安裝檔存檔失敗!");
                }
            }
            else
            {
                Basic.MyMessageBox.ShowDialog("下載失敗!");
            }
        }
        #endregion
        #region PLC_Method
        PLC_Device PLC_Device_Method = new PLC_Device("");
        PLC_Device PLC_Device_Method_OK = new PLC_Device("");
        Task Task_Method;
        MyTimer MyTimer_Method_結束延遲 = new MyTimer();
        MyTimer MyTimer_Method_開始延遲 = new MyTimer();
        int cnt_Program_Method = 65534;
        void sub_Program_Method()
        {
            if (cnt_Program_Method == 65534)
            {
                this.MyTimer_Method_結束延遲.StartTickTime(10000);
                this.MyTimer_Method_開始延遲.StartTickTime(10000);
                PLC_Device_Method.SetComment("PLC_Method");
                PLC_Device_Method_OK.SetComment("PLC_Method_OK");
                PLC_Device_Method.Bool = false;
                cnt_Program_Method = 65535;
            }
            if (cnt_Program_Method == 65535) cnt_Program_Method = 1;
            if (cnt_Program_Method == 1) cnt_Program_Method_檢查按下(ref cnt_Program_Method);
            if (cnt_Program_Method == 2) cnt_Program_Method_初始化(ref cnt_Program_Method);
            if (cnt_Program_Method == 3) cnt_Program_Method = 65500;
            if (cnt_Program_Method > 1) cnt_Program_Method_檢查放開(ref cnt_Program_Method);

            if (cnt_Program_Method == 65500)
            {
                this.MyTimer_Method_結束延遲.TickStop();
                this.MyTimer_Method_結束延遲.StartTickTime(10000);
                PLC_Device_Method.Bool = false;
                PLC_Device_Method_OK.Bool = false;
                cnt_Program_Method = 65535;
            }
        }
        void cnt_Program_Method_檢查按下(ref int cnt)
        {
            if (PLC_Device_Method.Bool) cnt++;
        }
        void cnt_Program_Method_檢查放開(ref int cnt)
        {
            if (!PLC_Device_Method.Bool) cnt = 65500;
        }
        void cnt_Program_Method_初始化(ref int cnt)
        {
            if (this.MyTimer_Method_結束延遲.IsTimeOut())
            {
                if (Task_Method == null)
                {
                    Task_Method = new Task(new Action(delegate { }));
                }
                if (Task_Method.Status == TaskStatus.RanToCompletion)
                {
                    Task_Method = new Task(new Action(delegate { }));
                }
                if (Task_Method.Status == TaskStatus.Created)
                {
                    Task_Method.Start();
                }
                cnt++;
            }
        }













        #endregion

 
    }


}
