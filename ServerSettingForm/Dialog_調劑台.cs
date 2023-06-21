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
    public partial class Dialog_調劑台 : Form
    {
        #region MyConfigClass
        private const string MyConfigFileName = "DPSConfig.txt";
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
        public Dialog_調劑台()
        {
            InitializeComponent();
        }

        private void Dialog_調劑台_Load(object sender, EventArgs e)
        {
            this.FormClosing += Dialog_調劑台_FormClosing;
            button_上傳.Click += Button_上傳_Click;
            button_新增.Click += Button_新增_Click;
            button_測試.Click += Button_測試_Click;
            button_刪除.Click += Button_刪除_Click;
            button_讀取.Click += Button_讀取_Click;

            button_一般資料_測試.Click += Button_一般資料_測試_Click;
            button_人員資料_測試.Click += Button_人員資料_測試_Click;

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
        #region Event
        private void Dialog_調劑台_FormClosing(object sender, FormClosingEventArgs e)
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
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);

            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                    select value).ToList();
            comboBox_名稱.Items.Clear();

            for (int i = 0; i < serverSettingClasses.Count; i++)
            {
               if(! comboBox_名稱.Items.Contains(serverSettingClasses[i].名稱))
                {
                    comboBox_名稱.Items.Add(serverSettingClasses[i].名稱);
                }
            }
            if (comboBox_名稱.Items.Count > 0) comboBox_名稱.SelectedIndex = 0;

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
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);
            List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                    where value.名稱 == comboBox_名稱.Text
                                    select value).ToList();

            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.SQLServer.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "一般資料"
                                        select value).ToList();
            if(serverSettingClasses_buf.Count > 0)
            {
                rJ_TextBox_一般資料_Server.Texts = serverSettingClasses_buf[0].Server;
                rJ_TextBox_一般資料_Port.Texts = serverSettingClasses_buf[0].Port;
                rJ_TextBox_一般資料_DBName.Texts = serverSettingClasses_buf[0].DBName;
                rJ_TextBox_一般資料_UserName.Texts = serverSettingClasses_buf[0].User;
                rJ_TextBox_一般資料_Password.Texts = serverSettingClasses_buf[0].Password;
            }
            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.SQLServer.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "人員資料"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                rJ_TextBox_人員資料_Server.Texts = serverSettingClasses_buf[0].Server;
                rJ_TextBox_人員資料_Port.Texts = serverSettingClasses_buf[0].Port;
                rJ_TextBox_人員資料_DBName.Texts = serverSettingClasses_buf[0].DBName;
                rJ_TextBox_人員資料_UserName.Texts = serverSettingClasses_buf[0].User;
                rJ_TextBox_人員資料_Password.Texts = serverSettingClasses_buf[0].Password;
            }

            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.API.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "01"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                rJ_TextBox_API01.Texts = serverSettingClasses_buf[0].Server;
            }
            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.API.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "02"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                rJ_TextBox_API02.Texts = serverSettingClasses_buf[0].Server;
            }
            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.WEB.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "網域"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                rJ_TextBox_Website.Texts = serverSettingClasses_buf[0].Server;
            }
        }
        private void Button_刪除_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowDialog("確認刪除?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);
            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                    where value.名稱 == comboBox_名稱.Text
                                    select value).ToList();
            returnData.Data = serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/delete", json_in);
            Console.WriteLine(json_result);
            MyMessageBox.ShowDialog("完成!");

        }
        private void Button_新增_Click(object sender, EventArgs e)
        {
            Dialog_新增 dialog_新增 = new Dialog_新增();
            dialog_新增.ShowDialog();
            if (dialog_新增.DialogResult != DialogResult.Yes) return;
            List<ServerSettingClass> list_value = new List<ServerSettingClass>();
            list_value.Add(new ServerSettingClass(dialog_新增.Value, enum_ServerSetting_Type.調劑台, enum_ServerSetting_ProgramType.SQLServer, "一般資料",
            rJ_TextBox_一般資料_Server.Text, rJ_TextBox_一般資料_Port.Text, rJ_TextBox_一般資料_DBName.Text, "", rJ_TextBox_一般資料_UserName.Text, rJ_TextBox_一般資料_Password.Text));

            list_value.Add(new ServerSettingClass(dialog_新增.Value, enum_ServerSetting_Type.調劑台, enum_ServerSetting_ProgramType.SQLServer, "人員資料",
            rJ_TextBox_人員資料_Server.Text, rJ_TextBox_人員資料_Port.Text, rJ_TextBox_人員資料_DBName.Text, "", rJ_TextBox_人員資料_UserName.Text, rJ_TextBox_人員資料_Password.Text));

            list_value.Add(new ServerSettingClass(dialog_新增.Value, enum_ServerSetting_Type.調劑台, enum_ServerSetting_ProgramType.API, "01",
            rJ_TextBox_API01.Text, "", "", "", "", ""));

            list_value.Add(new ServerSettingClass(dialog_新增.Value, enum_ServerSetting_Type.調劑台, enum_ServerSetting_ProgramType.API, "02",
            rJ_TextBox_API01.Text, "", "", "", "", ""));

            list_value.Add(new ServerSettingClass(dialog_新增.Value, enum_ServerSetting_Type.調劑台, enum_ServerSetting_ProgramType.WEB, "網域",
            rJ_TextBox_Website.Text, "", "", "", "", ""));

            returnData returnData = new returnData();
            returnData.Data = list_value;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            string json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/add", json_in);
            Console.WriteLine(json_result);

            returnData returnData_result = new returnData();
            returnData_result = json_result.JsonDeserializet<returnData>();

            json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);

            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                    select value).ToList();
            comboBox_名稱.Items.Clear();

            for (int i = 0; i < serverSettingClasses.Count; i++)
            {
                if (!comboBox_名稱.Items.Contains(serverSettingClasses[i].名稱))
                {
                    comboBox_名稱.Items.Add(serverSettingClasses[i].名稱);
                }
            }
            if (comboBox_名稱.Items.Count > 0) comboBox_名稱.SelectedIndex = 0;
        }
        private void Button_上傳_Click(object sender, EventArgs e)
        {
            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            if (json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("讀取失敗!");
                return;
            }
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);
            List<ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                    where value.名稱 == comboBox_名稱.Text
                                    select value).ToList();

            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "一般資料"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                serverSettingClasses_buf[0].Server = rJ_TextBox_一般資料_Server.Text;
                serverSettingClasses_buf[0].Port = rJ_TextBox_一般資料_Port.Text;
                serverSettingClasses_buf[0].DBName = rJ_TextBox_一般資料_DBName.Text;
                serverSettingClasses_buf[0].User = rJ_TextBox_一般資料_UserName.Text;
                serverSettingClasses_buf[0].Password = rJ_TextBox_一般資料_Password.Text;
            }
            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "人員資料"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                serverSettingClasses_buf[0].Server = rJ_TextBox_人員資料_Server.Text;
                serverSettingClasses_buf[0].Port = rJ_TextBox_人員資料_Port.Text;
                serverSettingClasses_buf[0].DBName = rJ_TextBox_人員資料_DBName.Text;
                serverSettingClasses_buf[0].User = rJ_TextBox_人員資料_UserName.Text;
                serverSettingClasses_buf[0].Password = rJ_TextBox_人員資料_Password.Text;
            }

            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.API.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "01"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                serverSettingClasses_buf[0].Server = rJ_TextBox_API01.Texts;
            }
            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.API.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "02"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                serverSettingClasses_buf[0].Server = rJ_TextBox_API02.Texts;
            }
            serverSettingClasses_buf = (from value in serverSettingClasses
                                        where value.類別 == enum_ServerSetting_Type.調劑台.GetEnumName()
                                        where value.程式類別 == enum_ServerSetting_ProgramType.WEB.GetEnumName()
                                        where value.名稱 == comboBox_名稱.Text
                                        where value.內容 == "網域"
                                        select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                serverSettingClasses_buf[0].Server = rJ_TextBox_Website.Texts;
            }
            returnData.Data = serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/add", json_in);
            Console.WriteLine(json_result);
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
