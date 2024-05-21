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
using H_Pannel_lib;
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        public static string ServerName = "";
        public static string ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
        public static string API_Server = "";
        public static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public LadderConnection.Properties PLC;
      
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
            private SQL_DataGridView.ConnentionClass dB_Storagelist = new SQL_DataGridView.ConnentionClass();

            private string web_URL = "";
            private string api_URL = "";
            private string login_URL = "";
            private string name = "";
            private string api_Server = "";

            private string orderApiURL = "";
            private string medApiURL = "";
            private string med_Update_ApiURL = "";

            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_Storagelist { get => dB_Storagelist; set => dB_Storagelist = value; }
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


            public string Med_Update_ApiURL { get => med_Update_ApiURL; set => med_Update_ApiURL = value; }
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
        public MyConfigClass myConfigClass = new MyConfigClass();
        public class MyConfigClass
        {

            private bool _掃碼槍COM通訊 = true;
            private bool _主機模式 = false;
            private string rFID_COMPort = "COM1";
            private string scanner01_COMPort = "COM2";
            private string scanner02_COMPort = "COM3";
  

            public string Scanner01_COMPort { get => scanner01_COMPort; set => scanner01_COMPort = value; }
            public string Scanner02_COMPort { get => scanner02_COMPort; set => scanner02_COMPort = value; }
            public bool 掃碼槍COM通訊 { get => _掃碼槍COM通訊; set => _掃碼槍COM通訊 = value; }
            public bool 主機模式 { get => _主機模式; set => _主機模式 = value; }
            public string RFID_COMPort { get => rFID_COMPort; set => rFID_COMPort = value; }
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
        #region Function
        private void LoadcommandLineArgs()
        {
            string jsonstr = MyFileStream.LoadFileAllText($"{DBConfigFileName}");
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Length >= 3)
            {
                dBConfigClass.Api_Server = commandLineArgs[1];
                dBConfigClass.Name = commandLineArgs[2];
               
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
        private void ApiServerSetting()
        {
            this.ApiServerSetting(dBConfigClass.Name, "一般資料");
        }
        private void ApiServerSetting(string Name, string basicName)
        {
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
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.癌症備藥機, basicName);
            List<string> DPS_Names = (from temp in serverSettingClasses
                                      where temp.類別 == enum_ServerSetting_Type.癌症備藥機.GetEnumName()
                                      select temp.設備名稱).Distinct().ToList();

            if (serverSettingClass != null)
            {
                dBConfigClass.DB_Basic.IP = serverSettingClass.Server;
                dBConfigClass.DB_Basic.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_Basic.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_Basic.UserName = serverSettingClass.User;
                dBConfigClass.DB_Basic.Password = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.癌症備藥機, "人員資料");
            if (serverSettingClass != null)
            {
                dBConfigClass.DB_person_page.IP = serverSettingClass.Server;
                dBConfigClass.DB_person_page.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_person_page.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_person_page.UserName = serverSettingClass.User;
                dBConfigClass.DB_person_page.Password = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.癌症備藥機, "儲位資料");
            if (serverSettingClass != null)
            {
                dBConfigClass.DB_Storagelist.IP = serverSettingClass.Server;
                dBConfigClass.DB_Storagelist.Port = (uint)(serverSettingClass.Port.StringToInt32());
                dBConfigClass.DB_Storagelist.DataBaseName = serverSettingClass.DBName;
                dBConfigClass.DB_Storagelist.UserName = serverSettingClass.User;
                dBConfigClass.DB_Storagelist.Password = serverSettingClass.Password;
            }
            API_Server = dBConfigClass.Api_Server;

            serverSettingClass = serverSettingClasses.MyFind("Main", enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API_Login);
            if (serverSettingClass != null) dBConfigClass.Login_URL = serverSettingClass.Server;
        }
        #endregion
        public Main_Form()
        {
            InitializeComponent();
            this.Load += Main_Form_Load;
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            LoadingForm.form = this.FindForm();
            Dialog_AlarmForm.form = this.FindForm();
            Dialog_NumPannel.form = this.FindForm();
            MyMessageBox.form = this.FindForm();
            MyMessageBox.音效 = false;
            this.plC_UI_Init.音效 = false;
            this.plC_UI_Init.全螢幕顯示 = true;

            this.plC_UI_Init.Run(this.FindForm(), this.lowerMachine_Panel);
            this.plC_UI_Init.UI_Finished_Event += PlC_UI_Init_UI_Finished_Event;
        }

        private void PlC_UI_Init_UI_Finished_Event()
        {
            this.WindowState = FormWindowState.Maximized;
            PLC = this.lowerMachine_Panel.GetlowerMachine().properties;
            LoadDBConfig();
            LoadMyConfig();
            ApiServerSetting();

            PLC_UI_Init.Set_PLC_ScreenPage(panel_main01, this.plC_ScreenPage_main);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_系統, this.plC_ScreenPage_系統);

            Program_登入畫面_Init();
            Program_系統_Init();
            Program_輸入輸出_Init();
            Program_軸控_Init();
            Program_藥檔資料_Init();
            Program_儲位設定_Init();
            Program_馬達輸出索引表_Init();
            Program_登入權限資料_Init();
            Program_人員資料_Init();
          
            Program_工程模式_Init();
            Program_自動備藥_Init();
            Program_調配排程_Init();
            Program_出入庫作業_Init();
            Program_交易紀錄_Init();
            Program_Scanner_RS232_Init();


            plC_ScreenPage_main.TabChangeEvent += PlC_ScreenPage_main_TabChangeEvent;
        }

 

        private void PlC_ScreenPage_main_TabChangeEvent(string PageText)
        {
            if (PageText == "出入庫作業")
            {
                List<Storage> storages = Function_取得本地儲位();
                PlC_RJ_Button_出入庫作業_入庫_MouseDownEvent(null);

            }
            if (PageText == "儲位設定")
            {
                PlC_RJ_Button_儲位設定_儲位列表_重新整理_MouseDownEvent(null);
                comboBox_儲位設定_儲位列表_亮燈顏色.SelectedIndex = 0;
            }
        }



        #region PLC_Method
        PLC_Device PLC_Device_Method = new PLC_Device("");
        PLC_Device PLC_Device_Method_OK = new PLC_Device("");
        Task Task_Method;
        MyTimer MyTimer_Method_結束延遲 = new MyTimer();
        int cnt_Program_Method = 65534;
        void sub_Program_Method()
        {
            if (cnt_Program_Method == 65534)
            {
                this.MyTimer_Method_結束延遲.StartTickTime(10000);
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
