using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLUI;
using HIS_DB_Lib;
using Basic;

namespace ServerSettingForm
{
    public partial class Panel_API_URL : UserControl
    {
        [Browsable(false)]
        public ServerSettingClass ServerSetting
        {
            get
            {
                ServerSettingClass serverSettingClass = new ServerSettingClass();
                serverSettingClass.Server = rJ_TextBox_API_URL.Texts;
    
                serverSettingClass.設備名稱 = this.DeviceName;
                serverSettingClass.類別 = this.ServerSetting_Type.GetEnumName();
                serverSettingClass.內容 = this.Content;
                return serverSettingClass;
            }
            set
            {
                if (this.IsHandleCreated == false) return;
                rJ_TextBox_API_URL.Texts = value.Server;
            }
        }
        private string deviceName = "";
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public string DeviceName
        {
            get
            {
                return this.deviceName;
            }
            set
            {
                this.deviceName = value;
            }
        }
        private enum_ServerSetting_Type serverSetting_Type;
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public enum_ServerSetting_Type ServerSetting_Type { get => serverSetting_Type; set => serverSetting_Type = value; }
        private string content;
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public string Content
        {
            get => content;
            set
            {
                content = value;
                this.label_標題.Text = value;
            }
        }
        private string description = "-----";
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public string Description
        {
            get => description;
            set
            {
                description = value;
                this.label_中文標題.Text = value;
            }
        }


        public Panel_API_URL()
        {
            InitializeComponent();
            this.button_測試.Click += Button_測試_Click;
        }
        public void init(string DeviceName, enum_ServerSetting_Type enum_ServerSetting_Type, string Content)
        {
            this.DeviceName = DeviceName;
            this.ServerSetting_Type = enum_ServerSetting_Type;
            this.Content = Content;
        }
        new public void Load(List<ServerSettingClass> serverSettingClasses)
        {
            ServerSettingClass serverSettingClass = serverSettingClasses.MyFind(DeviceName, ServerSetting_Type, this.Content);
            if (serverSettingClass != null) this.ServerSetting = serverSettingClass;
        }
        private void Button_測試_Click(object sender, EventArgs e)
        {
            string json_result = Basic.Net.WEBApiGet($"{rJ_TextBox_API_URL.Text}/api/test");
            if (json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog($"測試失敗!");
                return;
            }
            MyMessageBox.ShowDialog($"{json_result}");
        }
        static public void LoadAll(Form form, List<ServerSettingClass> serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            ServerSettingClass serverSettingClass = new ServerSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_API_URL)
                {
                    Panel_API_URL panel_API_URL = ((Panel_API_URL)controlList[i]);
                    serverSettingClass = serverSettingClasses.MyFind(panel_API_URL.DeviceName, panel_API_URL.serverSetting_Type, panel_API_URL.content);
                    if (serverSettingClass != null) panel_API_URL.ServerSetting = serverSettingClass;
                    else panel_API_URL.ServerSetting = new ServerSettingClass();
                }
            }
        }
        static public void SaveAll(Form form, ref List<ServerSettingClass> serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            ServerSettingClass serverSettingClass = new ServerSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_API_URL)
                {
                    Panel_API_URL panel_API_URL = ((Panel_API_URL)controlList[i]);
                    serverSettingClass = serverSettingClasses.MyFind(panel_API_URL.DeviceName, panel_API_URL.serverSetting_Type, panel_API_URL.content);
                    if (serverSettingClass != null)
                    {
                        serverSettingClasses.SetValue(panel_API_URL.ServerSetting);
                    }
                    else
                    {
                        serverSettingClasses.Add(panel_API_URL.ServerSetting);
                    }
                }
            }
        }

        static public void SetValue(Form form, string deviceName, enum_ServerSetting_Type serverSetting_Type)
        {
            List<Control> controlList = GetAllControls(form);
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_API_URL)
                {
                    ((Panel_API_URL)controlList[i]).deviceName = deviceName;
                    ((Panel_API_URL)controlList[i]).serverSetting_Type = serverSetting_Type;
                }
            }
        }
        static private List<Control> GetAllControls(Control container)
        {
            List<Control> controlList = new List<Control>();

            foreach (Control c in container.Controls)
            {
                controlList.Add(c);
                controlList.AddRange(GetAllControls(c));
            }

            return controlList;
        }
    }
}
