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
    public partial class Dialog_更新資訊 : Form
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
        public Dialog_更新資訊()
        {
            InitializeComponent();
        }

        private void Dialog_更新資訊_Load(object sender, EventArgs e)
        {
            this.FormClosing += Dialog_更新資訊_FormClosing;
            button_上傳.Click += Button_上傳_Click;
            button_測試.Click += Button_測試_Click;
            button_刪除.Click += Button_刪除_Click;
            button_讀取.Click += Button_讀取_Click;

            this.LoadMyConfig();
          
            if (myConfigClass != null)
            {
                rJ_TextBox_API_Server.Texts = myConfigClass.Api_server;
                Panel_sys_updateVersion.ApiURL = $"{myConfigClass.Api_server}";
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
            List<sys_serverSettingClass> sys_serverSettingClasses = returnData.Data.ObjToListClass<sys_serverSettingClass>();
            sys_serverSettingClasses = sys_serverSettingClassMethod.MyFind(sys_serverSettingClasses, enum_sys_serverSetting_Type.更新資訊);
            Panel_SQLContent.SetValue(this.FindForm(), Name, enum_sys_serverSetting_Type.更新資訊);
            Panel_SQLContent.SaveAll(this.FindForm(), ref sys_serverSettingClasses);
            Panel_API_URL.SetValue(this.FindForm(), Name, enum_sys_serverSetting_Type.更新資訊);
            Panel_API_URL.SaveAll(this.FindForm(), ref sys_serverSettingClasses);


   
            returnData.Data = sys_serverSettingClasses;
            string json_in = returnData.JsonSerializationt(true);
            Console.WriteLine(json_in);
            json_result = Basic.Net.WEBApiPostJson($"{myConfigClass.Api_server}/api/serversetting/add", json_in);
            Console.WriteLine(json_result);
        }
        #endregion
        #region Event
        private void Dialog_更新資訊_FormClosing(object sender, FormClosingEventArgs e)
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
            List<sys_serverSettingClass> sys_serverSettingClasses = returnData.Data.ObjToListClass<sys_serverSettingClass>();
            sys_serverSettingClasses = sys_serverSettingClassMethod.MyFind(sys_serverSettingClasses, enum_sys_serverSetting_Type.更新資訊);
            Panel_SQLContent.SetValue(this.FindForm(), DataName, enum_sys_serverSetting_Type.更新資訊);
            Panel_SQLContent.LoadAll(this.FindForm(), sys_serverSettingClasses);

            Panel_API_URL.SetValue(this.FindForm(), DataName, enum_sys_serverSetting_Type.更新資訊);
            Panel_API_URL.LoadAll(this.FindForm(), sys_serverSettingClasses);


            Panel_sys_updateVersion.LoadAll(this.FindForm());
        }
        private void Button_刪除_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowDialog("確認刪除?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            string json_result = Basic.Net.WEBApiGet($"{myConfigClass.Api_server}/api/serversetting");
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<sys_serverSettingClass> sys_serverSettingClasses = returnData.Data.ObjToListClass<sys_serverSettingClass>();
            sys_serverSettingClasses = (from value in sys_serverSettingClasses
                                    where value.類別 == enum_sys_serverSetting_Type.更新資訊.GetEnumName()
                                    where value.設備名稱 == DataName
                                    select value).ToList();
            returnData.Data = sys_serverSettingClasses;
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

        #endregion
    }
}
