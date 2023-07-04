using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Basic;
using HIS_DB_Lib;
namespace ServerSettingForm
{
    public partial class Dialog_藥庫 : Form
    {
        private string DataName
        {
            get
            {
                return label_名稱.Text;
            }
        }
        #region MyConfigClass
        private const string MyConfigFileName = "WebConfig.txt";
        public MyConfigClass myConfigClass = new MyConfigClass();
        public class MyConfigClass
        {
            private string api_server = "";

            public string Api_server { get => api_server; set => api_server = value; }
        }
        private void LoadMyConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($".//{MyConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {
                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(new MyConfigClass(), true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }
            }
            else
            {
                myConfigClass = Basic.Net.JsonDeserializet<MyConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass, true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }

            }


        }
        private void SaveConfig()
        {
            string jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass, true);
            List<string> list_jsonstring = new List<string>();
            list_jsonstring.Add(jsonstr);
            if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
            {
                MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                return;
            }
        }
        #endregion

        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }
        public Dialog_藥庫()
        {
            InitializeComponent();
        }

        private void Dialog_藥庫_Load(object sender, EventArgs e)
        {
            this.FormClosing += Dialog_藥庫_FormClosing;
            button_上傳.Click += Button_上傳_Click;
            button_測試.Click += Button_測試_Click;
            button_刪除.Click += Button_刪除_Click;
            button_讀取.Click += Button_讀取_Click;

            button_一般資料_測試.Click += Button_一般資料_測試_Click;
            button_人員資料_測試.Click += Button_人員資料_測試_Click;
            button_藥檔資料_測試.Click += Button_藥檔資料_測試_Click;
            button_批次過帳資料_測試.Click += Button_批次過帳資料_測試_Click;
            button_API_本地端_測試.Click += Button_本地端_測試_Click;
            button_API_VM端_測試.Click += Button_VM端_測試_Click;

            button_API01_測試.Click += Button_API01_測試_Click;
            button_API02_測試.Click += Button_API02_測試_Click;
    
            button_Website_開啟.Click += Button_Website_開啟_Click;

            this.LoadMyConfig();
          
            if (myConfigClass != null)
            {
                rJ_TextBox_API_Server.Texts = myConfigClass.Api_server;
            }
            Button_測試_Click(null, null);
        }

        #region Function
        private void Function_Add(string Name)
        {
            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            if (json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("讀取失敗!");
                return;
            }
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
            serverSettingClasses = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫);
            ServerSettingClass serverSettingClass;
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.一般資料);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_一般資料_Server.Text;
                serverSettingClass.Port = rJ_TextBox_一般資料_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_一般資料_DBName.Text;
                serverSettingClass.User = rJ_TextBox_一般資料_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_一般資料_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.一般資料,
                rJ_TextBox_一般資料_Server.Text, rJ_TextBox_一般資料_Port.Text, rJ_TextBox_一般資料_DBName.Text, "", rJ_TextBox_一般資料_UserName.Text, rJ_TextBox_一般資料_Password.Text));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.人員資料);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_人員資料_Server.Text;
                serverSettingClass.Port = rJ_TextBox_人員資料_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_人員資料_DBName.Text;
                serverSettingClass.User = rJ_TextBox_人員資料_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_人員資料_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.人員資料,
                rJ_TextBox_人員資料_Server.Text, rJ_TextBox_人員資料_Port.Text, rJ_TextBox_人員資料_DBName.Text, "", rJ_TextBox_人員資料_UserName.Text, rJ_TextBox_人員資料_Password.Text));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.藥檔資料);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_藥檔資料_Server.Text;
                serverSettingClass.Port = rJ_TextBox_藥檔資料_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_藥檔資料_DBName.Text;
                serverSettingClass.User = rJ_TextBox_藥檔資料_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_藥檔資料_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.藥檔資料,
                rJ_TextBox_藥檔資料_Server.Text, rJ_TextBox_藥檔資料_Port.Text, rJ_TextBox_藥檔資料_DBName.Text, "", rJ_TextBox_藥檔資料_UserName.Text, rJ_TextBox_藥檔資料_Password.Text));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.批次過帳資料);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_批次過帳資料_Server.Text;
                serverSettingClass.Port = rJ_TextBox_批次過帳資料_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_批次過帳資料_DBName.Text;
                serverSettingClass.User = rJ_TextBox_批次過帳資料_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_批次過帳資料_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.批次過帳資料,
                rJ_TextBox_批次過帳資料_Server.Text, rJ_TextBox_批次過帳資料_Port.Text, rJ_TextBox_批次過帳資料_DBName.Text, "", rJ_TextBox_批次過帳資料_UserName.Text, rJ_TextBox_批次過帳資料_Password.Text));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_VM端);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API_VM端_Server.Text;
                serverSettingClass.Port = rJ_TextBox_API_VM端_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_API_VM端_DBName.Text;
                serverSettingClass.User = rJ_TextBox_API_VM端_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_API_VM端_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.API_VM端,
                rJ_TextBox_API_VM端_Server.Text, rJ_TextBox_API_VM端_Port.Text, rJ_TextBox_API_VM端_DBName.Text, "", rJ_TextBox_API_VM端_UserName.Text, rJ_TextBox_API_VM端_Password.Text));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_本地端);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API_本地端_Server.Text;
                serverSettingClass.Port = rJ_TextBox_API_本地端_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_API_本地端_DBName.Text;
                serverSettingClass.User = rJ_TextBox_API_本地端_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_API_本地端_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.API_本地端,
                rJ_TextBox_API_本地端_Server.Text, rJ_TextBox_API_本地端_Port.Text, rJ_TextBox_API_本地端_DBName.Text, "", rJ_TextBox_API_本地端_UserName.Text, rJ_TextBox_API_本地端_Password.Text));
            }

      

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_儲位資料);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API_儲位資料_Server.Text;
                serverSettingClass.Port = rJ_TextBox_API_儲位資料_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_API_儲位資料_DBName.Text;
                serverSettingClass.User = rJ_TextBox_API_儲位資料_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_API_儲位資料_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.API_儲位資料,
                rJ_TextBox_API_儲位資料_Server.Text, rJ_TextBox_API_儲位資料_Port.Text, rJ_TextBox_API_儲位資料_DBName.Text, "", rJ_TextBox_API_儲位資料_UserName.Text, rJ_TextBox_API_儲位資料_Password.Text));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_交易紀錄資料);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API_交易紀錄資料_Server.Text;
                serverSettingClass.Port = rJ_TextBox_API_交易紀錄資料_Port.Text;
                serverSettingClass.DBName = rJ_TextBox_API_交易紀錄資料_DBName.Text;
                serverSettingClass.User = rJ_TextBox_API_交易紀錄資料_UserName.Text;
                serverSettingClass.Password = rJ_TextBox_API_交易紀錄資料_Password.Text;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_藥庫.API_交易紀錄資料,
                rJ_TextBox_API_交易紀錄資料_Server.Text, rJ_TextBox_API_交易紀錄資料_Port.Text, rJ_TextBox_API_交易紀錄資料_DBName.Text, "", rJ_TextBox_API_交易紀錄資料_UserName.Text, rJ_TextBox_API_交易紀錄資料_Password.Text));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API01);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API01.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.API, enum_ServerSetting_藥庫.API01,
                rJ_TextBox_API01.Text, "", "", "", "", ""));
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API02);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API02.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.API, enum_ServerSetting_藥庫.API02,
                rJ_TextBox_API02.Text, "", "", "", "", ""));
            }
           
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.Website);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_Website.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.WEB, enum_ServerSetting_藥庫.Website,
               rJ_TextBox_Website.Text, "", "", "", "", ""));
            }

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_inspection_excel);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_inspection_excel.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.API, enum_ServerSetting_藥庫.API_inspection_excel,
               rJ_TextBox_inspection_excel.Text, "", "", "", "", ""));
            }
            
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.功能);
            if (serverSettingClass != null)
            {
                List<string> list_value = new List<string>();
                if (checkBox_驗收.Checked) list_value.Add("驗收");
                if (checkBox_盤點.Checked) list_value.Add("盤點");
                if (checkBox_揀貨.Checked) list_value.Add("揀貨");
                if (checkBox_條碼管理.Checked) list_value.Add("條碼管理");
                if (checkBox_儲位管理.Checked) list_value.Add("儲位管理");
                serverSettingClass.Value = list_value.JsonSerializationt();
            }
            else
            {
                ServerSettingClass serverSettingClass_temp = new ServerSettingClass(Name, enum_ServerSetting_Type.藥庫, enum_ServerSetting_ProgramType.WEB, enum_ServerSetting_藥庫.功能,
                rJ_TextBox_Website.Text, "", "", "", "", "");
                List<string> list_value = new List<string>();
                if (checkBox_驗收.Checked) list_value.Add("驗收");
                if (checkBox_盤點.Checked) list_value.Add("盤點");
                if (checkBox_揀貨.Checked) list_value.Add("揀貨");
                if (checkBox_條碼管理.Checked) list_value.Add("條碼管理");
                if (checkBox_儲位管理.Checked) list_value.Add("儲位管理");
                serverSettingClass_temp.Value = list_value.JsonSerializationt();
                serverSettingClasses.Add(serverSettingClass_temp);
            }


            returnData.Data = serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/add", json_in);
            Console.WriteLine(json_result);
        }
        #endregion
        #region Event
        private void Dialog_藥庫_FormClosing(object sender, FormClosingEventArgs e)
        {
      
        }
        private void Button_測試_Click(object sender, EventArgs e)
        {
            myConfigClass.Api_server = rJ_TextBox_API_Server.Text;
            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            if(json_result.StringIsEmpty())
            {
                if (sender != null) MyMessageBox.ShowDialog("測試失敗!");
                return;
            }
            Console.WriteLine($"{json_result}");
            SaveConfig();
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            serverSettingClasses = ServerSettingClassMethod.MyFind(serverSettingClasses, enum_ServerSetting_Type.藥庫);

            if (returnData.Code == 200)
            {
                if (sender != null) MyMessageBox.ShowDialog("測試成功!");
            }
        }
        private void Button_讀取_Click(object sender, EventArgs e)
        {
            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            if (json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("讀取失敗!");
                return;
            }
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
            serverSettingClasses = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫);

            ServerSettingClass serverSettingClass;
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.一般資料);
            if (serverSettingClass != null)
            {
                rJ_TextBox_一般資料_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_一般資料_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_一般資料_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_一般資料_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_一般資料_Password.Texts = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.人員資料);
            if (serverSettingClass != null)
            {
                rJ_TextBox_人員資料_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_人員資料_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_人員資料_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_人員資料_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_人員資料_Password.Texts = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.藥檔資料);
            if (serverSettingClass != null)
            {
                rJ_TextBox_藥檔資料_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_藥檔資料_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_藥檔資料_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_藥檔資料_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_藥檔資料_Password.Texts = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.批次過帳資料);
            if (serverSettingClass != null)
            {
                rJ_TextBox_批次過帳資料_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_批次過帳資料_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_批次過帳資料_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_批次過帳資料_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_批次過帳資料_Password.Texts = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_本地端);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API_本地端_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_API_本地端_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_API_本地端_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_API_本地端_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_API_本地端_Password.Texts = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_VM端);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API_VM端_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_API_VM端_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_API_VM端_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_API_VM端_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_API_VM端_Password.Texts = serverSettingClass.Password;
            }
          
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_儲位資料);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API_儲位資料_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_API_儲位資料_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_API_儲位資料_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_API_儲位資料_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_API_儲位資料_Password.Texts = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_交易紀錄資料);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API_交易紀錄資料_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_API_交易紀錄資料_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_API_交易紀錄資料_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_API_交易紀錄資料_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_API_交易紀錄資料_Password.Texts = serverSettingClass.Password;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API01);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API01.Texts = serverSettingClass.Server;
            }

            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API02);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API02.Texts = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.API_inspection_excel);
            if (serverSettingClass != null)
            {
                rJ_TextBox_inspection_excel.Texts = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.Website);
            if (serverSettingClass != null)
            {
                rJ_TextBox_Website.Texts  = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(DataName, enum_ServerSetting_Type.藥庫, enum_ServerSetting_藥庫.功能);
            if (serverSettingClass != null)
            {
                List<string> list_value = serverSettingClass.Value.JsonDeserializet<List<string>>();
                if (list_value == null) return;
                checkBox_驗收.Checked = false;
                checkBox_盤點.Checked = false;
                checkBox_揀貨.Checked = false;
                checkBox_條碼管理.Checked = false;
                checkBox_儲位管理.Checked = false;
                for (int i = 0; i < list_value.Count; i++)
                {
                    if (list_value[i] == "驗收") checkBox_驗收.Checked = true;
                    if (list_value[i] == "盤點") checkBox_盤點.Checked = true;
                    if (list_value[i] == "揀貨") checkBox_揀貨.Checked = true;
                    if (list_value[i] == "條碼管理") checkBox_條碼管理.Checked = true;
                    if (list_value[i] == "儲位管理") checkBox_儲位管理.Checked = true;
                }
            }

        }
        private void Button_刪除_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowDialog("確認刪除?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.藥庫.GetEnumName()
                                    where value.設備名稱 == DataName
                                    select value).ToList();
            returnData.Data = serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/delete", json_in);
            Console.WriteLine(json_result);
            MyMessageBox.ShowDialog("完成!");

        }
        private void Button_上傳_Click(object sender, EventArgs e)
        {
            Function_Add(DataName);
            MyMessageBox.ShowDialog("完成!");
        }
        private void Button_人員資料_測試_Click(object sender, EventArgs e)
        {
            string server = rJ_TextBox_人員資料_Server.Text;
            string port = rJ_TextBox_人員資料_Port.Text;
            string dbname = rJ_TextBox_人員資料_DBName.Text;
            string username = rJ_TextBox_人員資料_UserName.Text;
            string password = rJ_TextBox_人員資料_Password.Text;
            SQLUI.SQLControl sQLControl = new SQLUI.SQLControl(server, dbname, username, password, (uint)port.StringToInt32());

            if (sQLControl.TestConnection())
            {
                MyMessageBox.ShowDialog("人員資料連線測試成功!");
            }
            else
            {
                MyMessageBox.ShowDialog("人員資料連線測試失敗!");
            }
        }
        private void Button_一般資料_測試_Click(object sender, EventArgs e)
        {
            string server = rJ_TextBox_一般資料_Server.Text;
            string port = rJ_TextBox_一般資料_Port.Text;
            string dbname = rJ_TextBox_一般資料_DBName.Text;
            string username = rJ_TextBox_一般資料_UserName.Text;
            string password = rJ_TextBox_一般資料_Password.Text;
            SQLUI.SQLControl sQLControl = new SQLUI.SQLControl(server, dbname, username, password, (uint)port.StringToInt32());

            if (sQLControl.TestConnection())
            {
                MyMessageBox.ShowDialog("一般資料連線測試成功!");
            }
            else
            {
                MyMessageBox.ShowDialog("一般資料連線測試失敗!");
            }
        }
        private void Button_批次過帳資料_測試_Click(object sender, EventArgs e)
        {
            string server = rJ_TextBox_批次過帳資料_Server.Text;
            string port = rJ_TextBox_批次過帳資料_Port.Text;
            string dbname = rJ_TextBox_批次過帳資料_DBName.Text;
            string username = rJ_TextBox_批次過帳資料_UserName.Text;
            string password = rJ_TextBox_批次過帳資料_Password.Text;
            SQLUI.SQLControl sQLControl = new SQLUI.SQLControl(server, dbname, username, password, (uint)port.StringToInt32());

            if (sQLControl.TestConnection())
            {
                MyMessageBox.ShowDialog("批次過帳資料連線測試成功!");
            }
            else
            {
                MyMessageBox.ShowDialog("批次過帳資料連線測試失敗!");
            }
        }
        private void Button_藥檔資料_測試_Click(object sender, EventArgs e)
        {
            string server = rJ_TextBox_藥檔資料_Server.Text;
            string port = rJ_TextBox_藥檔資料_Port.Text;
            string dbname = rJ_TextBox_藥檔資料_DBName.Text;
            string username = rJ_TextBox_藥檔資料_UserName.Text;
            string password = rJ_TextBox_藥檔資料_Password.Text;
            SQLUI.SQLControl sQLControl = new SQLUI.SQLControl(server, dbname, username, password, (uint)port.StringToInt32());

            if (sQLControl.TestConnection())
            {
                MyMessageBox.ShowDialog("藥檔資料連線測試成功!");
            }
            else
            {
                MyMessageBox.ShowDialog("藥檔資料連線測試失敗!");
            }
        }
        private void Button_VM端_測試_Click(object sender, EventArgs e)
        {
            string server = rJ_TextBox_API_VM端_Server.Text;
            string port = rJ_TextBox_API_VM端_Port.Text;
            string dbname = rJ_TextBox_API_VM端_DBName.Text;
            string username = rJ_TextBox_API_VM端_UserName.Text;
            string password = rJ_TextBox_API_VM端_Password.Text;
            SQLUI.SQLControl sQLControl = new SQLUI.SQLControl(server, dbname, username, password, (uint)port.StringToInt32());

            if (sQLControl.TestConnection())
            {
                MyMessageBox.ShowDialog("VM端連線測試成功!");
            }
            else
            {
                MyMessageBox.ShowDialog("VM端連線測試失敗!");
            }
        }
        private void Button_本地端_測試_Click(object sender, EventArgs e)
        {
            string server = rJ_TextBox_API_本地端_Server.Text;
            string port = rJ_TextBox_API_本地端_Port.Text;
            string dbname = rJ_TextBox_API_本地端_DBName.Text;
            string username = rJ_TextBox_API_本地端_UserName.Text;
            string password = rJ_TextBox_API_本地端_Password.Text;
            SQLUI.SQLControl sQLControl = new SQLUI.SQLControl(server, dbname, username, password, (uint)port.StringToInt32());

            if (sQLControl.TestConnection())
            {
                MyMessageBox.ShowDialog("本地端連線測試成功!");
            }
            else
            {
                MyMessageBox.ShowDialog("本地端連線測試失敗!");
            }
        }
        private void Button_API01_測試_Click(object sender, EventArgs e)
        {
            string json_result = Basic.Net.WEBApiGet($"{rJ_TextBox_API01.Text}/api/test");
            if (json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog($"測試失敗!");
                return;
            }
            MyMessageBox.ShowDialog($"{json_result}");
        }
        private void Button_API02_測試_Click(object sender, EventArgs e)
        {
            string json_result = Basic.Net.WEBApiGet($"{rJ_TextBox_API02.Text}/api/test");
            if(json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog($"測試失敗!");
                return;
            }
            MyMessageBox.ShowDialog($"{json_result}");
        }
      
        private void Button_Website_開啟_Click(object sender, EventArgs e)
        {
            string url = $"{rJ_TextBox_Website.Text}";
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法開啟網頁: " + ex.Message);
            }
        }
        #endregion
    }
}
