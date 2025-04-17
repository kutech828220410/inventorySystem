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
using HIS_DB_Lib;
using RfidReaderLib;
using System.Text.Json.Serialization;
namespace 智能RFID燒錄系統
{
    public partial class Main_From : MyDialog
    {
        private static bool IsComConnected = false;
        public static string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string API_Server = "http://127.0.0.1:4433";
        private RfidReader _rfidReader = new RfidReader();
        private MyThread myThread_program = new MyThread();
        #region DBConfigClass
        private static string DBConfigFileName = $@"{currentDirectory}\DBConfig.txt";
        static public DBConfigClass dBConfigClass = new DBConfigClass();
        public class DBConfigClass
        {

            private string name = "";
            private string api_Server = "";

            public string Name { get => name; set => name = value; }
            public string Api_Server { get => api_Server; set => api_Server = value; }
        }
        private void LoadDBConfig()
        {

            //this.LoadcommandLineArgs();
            string jsonstr = MyFileStream.LoadFileAllText($"{DBConfigFileName}");

            Console.WriteLine($"[LoadDBConfig] path : {DBConfigFileName} ");
            Console.WriteLine($"[LoadDBConfig] jsonstr : {jsonstr} ");

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

        public Main_From()
        {
            InitializeComponent();
            this.Load += Main_From_Load;
        }

        private void Main_From_Load(object sender, EventArgs e)
        {
            LoadDBConfig();

            MyMessageBox.form = this.FindForm();

            API_Server = dBConfigClass.Api_Server;
            this.comboBox_Comport.DataSource = MySerialPort.GetPortNames();
            rJ_Button_Connect.MouseDownEvent += RJ_Button_Connect_MouseDownEvent;
            rJ_Button_Write.MouseDownEvent += RJ_Button_Write_MouseDownEvent;

            Table table = DrugHFTagClass.init(API_Server);
            this.sqL_DataGridView_TagList.RowsHeight = 50;
            this.sqL_DataGridView_TagList.Init(table);
            this.sqL_DataGridView_TagList.Set_ColumnVisible(false, new enum_DrugHFTag().GetEnumNames());
            this.sqL_DataGridView_TagList.Set_ColumnWidth(180, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.TagSN);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.藥碼);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_DrugHFTag.藥名);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.效期);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.批號);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.數量);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.更新時間);

            myThread_program.AutoRun(true);
            myThread_program.SetSleepTime(10);
            myThread_program.Add_Method(sub_program);
            myThread_program.Trigger();

    
        }

      
        private void sub_program()
        {
            if (IsComConnected)
            {
                List<string> uids = _rfidReader.ReadMultipleUIDs();

                this.Invoke(new Action(delegate
                {
                    rJ_Lable_標籤數量.Text = $"標籤數量 : {uids.Count.ToString()}";
                }));

                List<DrugHFTagClass> drugHFTagClasses = DrugHFTagClass.get_latest_tag_ByTagSN(API_Server, uids);
                List<DrugHFTagClass> drugHFTagClasses_grid = new List<DrugHFTagClass>();
                for (int i = 0; i < uids.Count; i++)
                {
                    string uid = uids[i];
                    DrugHFTagClass drugHFTagClass = drugHFTagClasses.SerchByTagSN(uid);
                    if (drugHFTagClass == null)
                    {
                        drugHFTagClass = new DrugHFTagClass();
                        drugHFTagClass.GUID = Guid.NewGuid().ToString();
                        drugHFTagClass.TagSN = uid;
                        drugHFTagClass.藥碼 = "";
                        drugHFTagClass.藥名 = "";
                        drugHFTagClass.效期 = "";
                        drugHFTagClass.批號 = "";
                        drugHFTagClass.數量 = "";
                        drugHFTagClass.更新時間 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    else
                    {
                        drugHFTagClass.更新時間 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    drugHFTagClasses_grid.Add(drugHFTagClass);
                }
                List<object[]> list_value = drugHFTagClasses_grid.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>();
                //this.sqL_DataGridView_TagList.AddRows(list_value , true);
                this.sqL_DataGridView_TagList.RefreshGrid(list_value);

            }
        }
        private void RJ_Button_Write_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_TagList.GetAllRows();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("請先讀取標籤!");
                return;
            }
            List<DrugHFTagClass> drugHFTagClasses = list_value.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();
            for (int i = 0; i < drugHFTagClasses.Count; i++)
            {
                drugHFTagClasses[i].藥碼 = rJ_TextBox_藥碼.Text;
                drugHFTagClasses[i].藥名 = rJ_TextBox_藥名.Text;
                drugHFTagClasses[i].效期 = rJ_DatePicker_效期.Value.ToDateString();
                drugHFTagClasses[i].批號 = rJ_TextBox_批號.Text;
                drugHFTagClasses[i].數量 = rJ_TextBox_數量.Text;
            }
            (int code, string result, var data) = DrugHFTagClass.add_full(API_Server, drugHFTagClasses);
            if (code != 200)
            {
                MyMessageBox.ShowDialog($"寫入失敗!{result}");
                return;
            }
            MyMessageBox.ShowDialog($"寫入成功!{result}");
        }
        private void RJ_Button_Connect_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                _rfidReader.ConfigurePort(this.comboBox_Comport.Text, 115200);
                _rfidReader.Open();
                if (_rfidReader.IsOpen)
                {
                    string str = _rfidReader.ReadHardwareInfo();
                    if (str.StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("Failed to read RFID reader information.");
                        return;
                    }
                    rJ_Lable_device_info.Text = $"Device info :{str}";
                    this.rJ_Button_Connect.Text = "Connected";
                    this.rJ_Button_Connect.Enabled = false;
                    this.comboBox_Comport.Enabled = false;
                    IsComConnected = true;
                }
                else
                {
                    MyMessageBox.ShowDialog("Failed to connect to RFID reader.");
                }
            }));

        }

    }
}
