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
    public partial class Dialog_中藥調劑系統 : Form
    {
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
        public Dialog_中藥調劑系統()
        {
            InitializeComponent();
            this.Load += Dialog_中藥調劑系統_Load;
            this.FormClosing += Dialog_中藥調劑系統_FormClosing;
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
            serverSettingClasses = serverSettingClasses.MyFind(Name, enum_ServerSetting_Type.中藥調劑系統);
            Panel_SQLContent.SetValue(this.FindForm(), Name, enum_ServerSetting_Type.中藥調劑系統);
            Panel_SQLContent.SaveAll(this.FindForm(), ref serverSettingClasses);
            Panel_API_URL.SetValue(this.FindForm(), Name, enum_ServerSetting_Type.中藥調劑系統);
            Panel_API_URL.SaveAll(this.FindForm(), ref serverSettingClasses);
            Panel_CheckBox.SetValue(this.FindForm(), enum_ServerSetting_Type.中藥調劑系統);
            Panel_CheckBox.SaveAll(this.FindForm(), ref serverSettingClasses);

            serverSettingClasses.Set_department_type(comboBox_單位.Text);

            returnData.Data = serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/add", json_in);
            Console.WriteLine(json_result);
        }
        #endregion
        #region Event
        private void Dialog_中藥調劑系統_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void Dialog_中藥調劑系統_Load(object sender, EventArgs e)
        {
            this.button_測試.Click += Button_測試_Click;
            this.button_新增.Click += Button_新增_Click;
            this.button_上傳.Click += Button_上傳_Click;
            this.button_刪除.Click += Button_刪除_Click;
            this.button_讀取.Click += Button_讀取_Click;

            this.LoadMyConfig();
            if (myConfigClass != null)
            {
                rJ_TextBox_API_Server.Texts = myConfigClass.Api_server;
            }
            Button_測試_Click(null, null);
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
            serverSettingClasses = serverSettingClasses.MyFind(enum_ServerSetting_Type.中藥調劑系統);
            Panel_SQLContent.SetValue(this.FindForm(), comboBox_名稱.Text, enum_ServerSetting_Type.中藥調劑系統);
            Panel_SQLContent.LoadAll(this.FindForm(), serverSettingClasses);

            Panel_API_URL.SetValue(this.FindForm(), comboBox_名稱.Text, enum_ServerSetting_Type.中藥調劑系統);
            Panel_API_URL.LoadAll(this.FindForm(), serverSettingClasses);

            Panel_CheckBox.SetValue(this.FindForm(), enum_ServerSetting_Type.中藥調劑系統);
            Panel_CheckBox.LoadAll(this.FindForm(), serverSettingClasses);

            this.comboBox_單位.DataSource = serverSettingClasses.Get_department_types();
            ServerSettingClass serverSettingClass = serverSettingClasses.myFind(comboBox_名稱.Text, enum_ServerSetting_Type.中藥調劑系統.GetEnumName(), "一般資料");
            this.comboBox_單位.Text = serverSettingClass.單位;
        }
        private void Button_刪除_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowDialog("確認刪除?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.中藥調劑系統.GetEnumName()
                                    where value.設備名稱 == comboBox_名稱.Text
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
            Function_Add(comboBox_名稱.Text);
            MyMessageBox.ShowDialog("完成!");
        }
        private void Button_新增_Click(object sender, EventArgs e)
        {
            Dialog_新增 dialog_新增 = new Dialog_新增("請輸入中藥調劑系統名稱...");
            dialog_新增.ShowDialog();
            if (dialog_新增.DialogResult != DialogResult.Yes) return;
            Function_Add(dialog_新增.Value);

            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();

            serverSettingClasses = (from value in serverSettingClasses
                                    where value.類別 == enum_ServerSetting_Type.中藥調劑系統.GetEnumName()
                                    select value).ToList();
            comboBox_名稱.Items.Clear();

            for (int i = 0; i < serverSettingClasses.Count; i++)
            {
                if (!comboBox_名稱.Items.Contains(serverSettingClasses[i].設備名稱))
                {
                    comboBox_名稱.Items.Add(serverSettingClasses[i].設備名稱);
                }
            }
            if (comboBox_名稱.Items.Count > 0) comboBox_名稱.SelectedIndex = 0;
        }
        private void Button_測試_Click(object sender, EventArgs e)
        {
            myConfigClass.Api_server = rJ_TextBox_API_Server.Text;
            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            if (json_result.StringIsEmpty())
            {
                if (sender != null) MyMessageBox.ShowDialog("測試失敗!");
                return;
            }
            Console.WriteLine($"{json_result}");
            SaveConfig();
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            serverSettingClasses = ServerSettingClassMethod.MyFind(serverSettingClasses, enum_ServerSetting_Type.中藥調劑系統);
            comboBox_名稱.Items.Clear();

            for (int i = 0; i < serverSettingClasses.Count; i++)
            {
                if (!comboBox_名稱.Items.Contains(serverSettingClasses[i].設備名稱))
                {
                    if (!serverSettingClasses[i].設備名稱.StringIsEmpty()) comboBox_名稱.Items.Add(serverSettingClasses[i].設備名稱);
                }
            }
            if (comboBox_名稱.Items.Count > 0) comboBox_名稱.SelectedIndex = 0;

            if (returnData.Code == 200)
            {
                if (sender != null) MyMessageBox.ShowDialog("測試成功!");
            }
        }
        #endregion
    }
}
