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
    public partial class Panel_CheckBox : UserControl
    {
        [Browsable(false)]
        public ServerSettingClass ServerSetting
        {
            get
            {
                ServerSettingClass serverSettingClass = new ServerSettingClass();
                serverSettingClass.設備名稱 = "";
                serverSettingClass.類別 = this.ServerSetting_Type.GetEnumName();
                serverSettingClass.內容 = this.Content;
                serverSettingClass.Value = this.checkBox.Checked.ToString();
                return serverSettingClass;
            }
            set
            {
                if (this.IsHandleCreated == false) return;
                checkBox.Checked = (value.Value == true.ToString());
                label_ServerType.Text = value.類別;
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
        public enum_ServerSetting_Type ServerSetting_Type
        { 
            get => serverSetting_Type;
            set
            {
                serverSetting_Type = value;
                label_ServerType.Text = value.GetEnumName();
            }
        }
        private string content;
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public string Content
        {
            get => content;
            set
            {
                content = value;
                this.checkBox.Text = value;
            }
        }

        public Panel_CheckBox()
        {
            InitializeComponent();
        }

        public void init(string DeviceName, enum_ServerSetting_Type enum_ServerSetting_Type, string Content)
        {
            //this.DeviceName = DeviceName;
            this.ServerSetting_Type = enum_ServerSetting_Type;
            this.Content = Content;
        }
        new public void Load(List<ServerSettingClass> serverSettingClasses)
        {
            ServerSettingClass serverSettingClass = serverSettingClasses.MyFind(DeviceName, ServerSetting_Type, this.Content);
            if (serverSettingClass != null) this.ServerSetting = serverSettingClass;
        }


        static public void LoadAll(Form form, List<ServerSettingClass> serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            ServerSettingClass serverSettingClass = new ServerSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_CheckBox)
                {
                    Panel_CheckBox Panel_CheckBox = ((Panel_CheckBox)controlList[i]);
                    serverSettingClass = serverSettingClasses.myFind("", Panel_CheckBox.serverSetting_Type.GetEnumName(), Panel_CheckBox.content);
                    if (serverSettingClass != null) Panel_CheckBox.ServerSetting = serverSettingClass;
                    else Panel_CheckBox.ServerSetting = new ServerSettingClass();
                }
            }
        }
        static public void SaveAll(Form form, ref List<ServerSettingClass> serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            ServerSettingClass serverSettingClass = new ServerSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_CheckBox)
                {
                    Panel_CheckBox Panel_CheckBox = ((Panel_CheckBox)controlList[i]);
                    serverSettingClass = serverSettingClasses.myFind("", Panel_CheckBox.serverSetting_Type.GetEnumName(), Panel_CheckBox.content);
                    if (serverSettingClass != null)
                    {
                        serverSettingClasses.SetValue(Panel_CheckBox.ServerSetting);
                    }
                    else
                    {
                        serverSettingClasses.Add(Panel_CheckBox.ServerSetting);
                    }
                }
            }
        }

        static public void SetValue(Form form, enum_ServerSetting_Type serverSetting_Type)
        {
            List<Control> controlList = GetAllControls(form);
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_CheckBox)
                {
                    ((Panel_CheckBox)controlList[i]).deviceName = "";
                    ((Panel_CheckBox)controlList[i]).serverSetting_Type = serverSetting_Type;
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
