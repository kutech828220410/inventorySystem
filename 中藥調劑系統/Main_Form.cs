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
using System.Text.Json.Serialization;
using System.Reflection;
using HIS_DB_Lib;
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        public static string API_Server = "http://127.0.0.1:4433";
        public PLC_Device PLC_Device_已登入 = new PLC_Device("S4000");
        public static string Order_URL = "";
        public static string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public Main_Form()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
 

        }

        #region DBConfigClass
        private static string DBConfigFileName = $@"{currentDirectory}\DBConfig.txt";
        static public DBConfigClass dBConfigClass = new DBConfigClass();
        public class DBConfigClass
        {
            private SQL_DataGridView.ConnentionClass dB_Basic = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_VM = new SQL_DataGridView.ConnentionClass();
            private string name = "";
            private string api_Server = "";
            private string orderApiURL = "";

            public string Name { get => name; set => name = value; }
            public string Api_Server { get => api_Server; set => api_Server = value; }

            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_Basic { get => dB_Basic; set => dB_Basic = value; }
            [JsonIgnore]
            public SQL_DataGridView.ConnentionClass DB_VM { get => dB_VM; set => dB_VM = value; }
            [JsonIgnore]
            public string OrderApiURL { get => orderApiURL; set => orderApiURL = value; }

        }
        private void LoadDBConfig()
        {

            //this.LoadcommandLineArgs();
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

            private bool _主機模式 = false;
            private bool _主機輸出模式 = false;
            
            private string rFID_COMPort = "COM1";
            private string scanner01_COMPort = "COM2";
            private string scanner02_COMPort = "COM3";
            private string scanner03_COMPort = "";
            private string scanner04_COMPort = "";

            public bool 主機模式 { get => _主機模式; set => _主機模式 = value; }
            public bool 主機輸出模式 { get => _主機輸出模式; set => _主機輸出模式 = value; }
            public string RFID_COMPort { get => rFID_COMPort; set => rFID_COMPort = value; }
            public string Scanner01_COMPort { get => scanner01_COMPort; set => scanner01_COMPort = value; }
            public string Scanner02_COMPort { get => scanner02_COMPort; set => scanner02_COMPort = value; }
            public string Scanner03_COMPort { get => scanner03_COMPort; set => scanner03_COMPort = value; }
            public string Scanner04_COMPort { get => scanner04_COMPort; set => scanner04_COMPort = value; }
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            Dialog_AlarmForm.form = this.FindForm();
            MyDialog.form = this.FindForm();
            MyMessageBox.form = this.FindForm();
            MyMessageBox.音效 = false;
            this.plC_UI_Init.音效 = false;

            LoadDBConfig();
            LoadMyConfig();

            ApiServerSetting(dBConfigClass.Name);


            plC_UI_Init.Run(this.FindForm(), this.lowerMachine_Panel);
            plC_UI_Init.UI_Finished_Event += PlC_UI_Init_UI_Finished_Event;
        }
        private void PlC_UI_Init_UI_Finished_Event()
        {
            PLC_UI_Init.Set_PLC_ScreenPage(panel_main, this.plC_ScreenPage_main);
            PLC_Device_已登入.Bool = false;


       

            Program_調劑畫面_Init();
            Program_RS232_EXCELL_SCALE_Init();
            Program_RFID_Init();
        }

        private void ApiServerSetting(string Name)
        {
            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}/api/serversetting");
            ServerSettingClass serverSettingClass_一般資料 = serverSettingClasses.myFind(Name, "中藥調劑系統", "一般資料");
            ServerSettingClass serverSettingClass_VM端 = serverSettingClasses.myFind(Name, "中藥調劑系統", "VM端");
            ServerSettingClass serverSettingClass_Order_API = serverSettingClasses.myFind(Name, "中藥調劑系統", "Order_API");


            if (serverSettingClass_一般資料 != null)
            {
                dBConfigClass.DB_Basic.IP = serverSettingClass_一般資料.Server;
                dBConfigClass.DB_Basic.Port = (uint)(serverSettingClass_一般資料.Port.StringToInt32());
                dBConfigClass.DB_Basic.DataBaseName = serverSettingClass_一般資料.DBName;
                dBConfigClass.DB_Basic.UserName = serverSettingClass_一般資料.User;
                dBConfigClass.DB_Basic.Password = serverSettingClass_一般資料.Password;
            }
            if (serverSettingClass_VM端 != null)
            {
                dBConfigClass.DB_Basic.IP = serverSettingClass_VM端.Server;
                dBConfigClass.DB_Basic.Port = (uint)(serverSettingClass_VM端.Port.StringToInt32());
                dBConfigClass.DB_Basic.DataBaseName = serverSettingClass_VM端.DBName;
                dBConfigClass.DB_Basic.UserName = serverSettingClass_VM端.User;
                dBConfigClass.DB_Basic.Password = serverSettingClass_VM端.Password;
            }

            if (serverSettingClass_Order_API != null) dBConfigClass.OrderApiURL = serverSettingClass_Order_API.Server;
        }
    }
}
