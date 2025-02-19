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
        public sys_serverSettingClass ServerSetting
        {
            get
            {
                sys_serverSettingClass sys_serverSettingClass = new sys_serverSettingClass();
                sys_serverSettingClass.設備名稱 = "";
                sys_serverSettingClass.程式類別 = "peremeter";
                sys_serverSettingClass.類別 = this.ServerSetting_Type.GetEnumName();
                sys_serverSettingClass.內容 = this.Content;
                sys_serverSettingClass.Value = this.checkBox.Checked.ToString();
                return sys_serverSettingClass;
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
        private enum_sys_serverSetting_Type serverSetting_Type;
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public enum_sys_serverSetting_Type ServerSetting_Type
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

        public void init(string DeviceName, enum_sys_serverSetting_Type enum_sys_serverSetting_Type, string Content)
        {
            //this.DeviceName = DeviceName;
            this.ServerSetting_Type = enum_sys_serverSetting_Type;
            this.Content = Content;
        }
        new public void Load(List<sys_serverSettingClass> sys_serverSettingClasses)
        {
            sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses.MyFind(DeviceName, ServerSetting_Type, this.Content);
            if (sys_serverSettingClass != null) this.ServerSetting = sys_serverSettingClass;
        }


        static public void LoadAll(Form form, List<sys_serverSettingClass> sys_serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            sys_serverSettingClass sys_serverSettingClass = new sys_serverSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_CheckBox)
                {
                    Panel_CheckBox Panel_CheckBox = ((Panel_CheckBox)controlList[i]);
                    sys_serverSettingClass = sys_serverSettingClasses.myFind("", Panel_CheckBox.serverSetting_Type.GetEnumName(), Panel_CheckBox.content);
                    if (sys_serverSettingClass != null) Panel_CheckBox.ServerSetting = sys_serverSettingClass;
                    else Panel_CheckBox.ServerSetting = new sys_serverSettingClass();
                }
            }
        }
        static public void SaveAll(Form form, ref List<sys_serverSettingClass> sys_serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            sys_serverSettingClass sys_serverSettingClass = new sys_serverSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_CheckBox)
                {
                    Panel_CheckBox Panel_CheckBox = ((Panel_CheckBox)controlList[i]);
                    sys_serverSettingClass = sys_serverSettingClasses.myFind("", Panel_CheckBox.serverSetting_Type.GetEnumName(), Panel_CheckBox.content);
                    if (sys_serverSettingClass != null)
                    {
                        sys_serverSettingClasses.SetValue(Panel_CheckBox.ServerSetting);
                    }
                    else
                    {
                        sys_serverSettingClasses.Add(Panel_CheckBox.ServerSetting);
                    }
                }
            }
        }

        static public void SetValue(Form form, enum_sys_serverSetting_Type serverSetting_Type)
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
