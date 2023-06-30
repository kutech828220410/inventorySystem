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
    public partial class Dialog_網頁 : Form
    {
        private string WEB_Name = "Main";
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
        public Dialog_網頁()
        {
            InitializeComponent();
        }

        private void Dialog_網頁_Load(object sender, EventArgs e)
        {
            this.FormClosing += Dialog_網頁_FormClosing;
            button_上傳.Click += Button_上傳_Click;
            button_測試.Click += Button_測試_Click;
            button_讀取.Click += Button_讀取_Click;
            button_重置.Click += Button_重置_Click;

            button_人員資料_測試.Click += Button_人員資料_測試_Click;

            button_API01_測試.Click += Button_API01_測試_Click;
            button_API02_測試.Click += Button_API02_測試_Click;
            button_API_Session.Click += Button_API_Session_Click;
            button_Website_開啟.Click += Button_Website_開啟_Click;

            this.LoadMyConfig();
          
            if (myConfigClass != null)
            {
                rJ_TextBox_API_Server.Texts = myConfigClass.Api_server;
            }
            Button_測試_Click(null, null);
            Button_讀取_Click(null, null);
        }

      



        #region Function
        private void Function_Add(string Name)
        {

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{myConfigClass.Api_server}/api/serversetting");
            List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
            serverSettingClasses = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.網頁);
            ServerSettingClass serverSettingClass;
   

            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
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
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_ProgramType.SQLServer, enum_ServerSetting_網頁.人員資料,
                rJ_TextBox_人員資料_Server.Text, rJ_TextBox_人員資料_Port.Text, rJ_TextBox_人員資料_DBName.Text, "", rJ_TextBox_人員資料_UserName.Text, rJ_TextBox_人員資料_Password.Text));
            }


            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API01);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API01.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_ProgramType.API, enum_ServerSetting_網頁.API01,
                rJ_TextBox_API01.Text, "", "", "", "", ""));
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API02);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API02.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_ProgramType.API, enum_ServerSetting_網頁.API02,
                rJ_TextBox_API02.Text, "", "", "", "", ""));
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API_Session);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API_Session.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_ProgramType.API, enum_ServerSetting_網頁.API_Session,
                rJ_TextBox_API_Session.Text, "", "", "", "", ""));
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API_Login);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_API_Login.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_ProgramType.API, enum_ServerSetting_網頁.API_Login,
                rJ_TextBox_API_Login.Text, "", "", "", "", ""));
            }
            serverSettingClass = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.Website);
            if (serverSettingClass != null)
            {
                serverSettingClass.Server = rJ_TextBox_Website.Texts;
            }
            else
            {
                serverSettingClasses.Add(new ServerSettingClass(Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_ProgramType.WEB, enum_ServerSetting_網頁.Website,
               rJ_TextBox_Website.Text, "", "", "", "", ""));
            }

            returnData returnData = new returnData();
            returnData.Data = serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            string json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/add", json_in);
            Console.WriteLine(json_result);
        }
        #endregion
        #region Event
        private void Dialog_網頁_FormClosing(object sender, FormClosingEventArgs e)
        {
      
        }
        private void Button_重置_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowDialog("確認重置Web表單?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            Function_Add(WEB_Name);
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
         

            if (returnData.Code == 200)
            {
                if (sender != null) MyMessageBox.ShowDialog("測試成功!");
            }
        }
        private void Button_讀取_Click(object sender, EventArgs e)
        {
    
            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{myConfigClass.Api_server}/api/serversetting");
            List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
            serverSettingClasses = serverSettingClasses.MyFind(WEB_Name, enum_ServerSetting_Type.網頁);

            ServerSettingClass serverSettingClass;
          
            serverSettingClass = serverSettingClasses.MyFind(WEB_Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.人員資料);
            if (serverSettingClass != null)
            {
                rJ_TextBox_人員資料_Server.Texts = serverSettingClass.Server;
                rJ_TextBox_人員資料_Port.Texts = serverSettingClass.Port;
                rJ_TextBox_人員資料_DBName.Texts = serverSettingClass.DBName;
                rJ_TextBox_人員資料_UserName.Texts = serverSettingClass.User;
                rJ_TextBox_人員資料_Password.Texts = serverSettingClass.Password;
            }
          
           
         
            serverSettingClass = serverSettingClasses.MyFind(WEB_Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API01);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API01.Texts = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(WEB_Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API02);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API02.Texts = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(WEB_Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API_Session);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API_Session.Texts = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(WEB_Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.API_Login);
            if (serverSettingClass != null)
            {
                rJ_TextBox_API_Login.Texts = serverSettingClass.Server;
            }
            serverSettingClass = serverSettingClasses.MyFind(WEB_Name, enum_ServerSetting_Type.網頁, enum_ServerSetting_網頁.Website);
            if (serverSettingClass != null)
            {
                rJ_TextBox_Website.Texts  = serverSettingClass.Server;
            }

        }
        private void Button_刪除_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowDialog("確認刪除?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{myConfigClass.Api_server}/api/serversetting");
            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.網頁.GetEnumName()
                                    where value.設備名稱 == WEB_Name
                                    select value).ToList();
            returnData returnData = new returnData();
            returnData.Data = serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            string json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/delete", json_in);
            Console.WriteLine(json_result);
            MyMessageBox.ShowDialog("完成!");

        }
        private void Button_上傳_Click(object sender, EventArgs e)
        {
            Function_Add(WEB_Name);
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
        private void Button_API_Session_Click(object sender, EventArgs e)
        {
           
        }
        #endregion
    }
}
