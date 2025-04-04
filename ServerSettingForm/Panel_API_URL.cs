﻿using System;
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
        public sys_serverSettingClass ServerSetting
        {
            get
            {
                sys_serverSettingClass sys_serverSettingClass = new sys_serverSettingClass();
                sys_serverSettingClass.Server = rJ_TextBox_API_URL.Texts;
    
                sys_serverSettingClass.設備名稱 = this.DeviceName;
                sys_serverSettingClass.類別 = this.ServerSetting_Type.GetEnumName();
                sys_serverSettingClass.內容 = this.Content;
                return sys_serverSettingClass;
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
        private enum_sys_serverSetting_Type serverSetting_Type;
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public enum_sys_serverSetting_Type ServerSetting_Type { get => serverSetting_Type; set => serverSetting_Type = value; }
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
        public void init(string DeviceName, enum_sys_serverSetting_Type enum_sys_serverSetting_Type, string Content)
        {
            this.DeviceName = DeviceName;
            this.ServerSetting_Type = enum_sys_serverSetting_Type;
            this.Content = Content;
        }
        new public void Load(List<sys_serverSettingClass> sys_serverSettingClasses)
        {
            sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClasses.MyFind(DeviceName, ServerSetting_Type, this.Content);
            if (sys_serverSettingClass != null) this.ServerSetting = sys_serverSettingClass;
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
        static public void LoadAll(Form form, List<sys_serverSettingClass> sys_serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            sys_serverSettingClass sys_serverSettingClass = new sys_serverSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_API_URL)
                {
                    Panel_API_URL panel_API_URL = ((Panel_API_URL)controlList[i]);
                    sys_serverSettingClass = sys_serverSettingClasses.MyFind(panel_API_URL.DeviceName, panel_API_URL.serverSetting_Type, panel_API_URL.content);
                    if (sys_serverSettingClass != null) panel_API_URL.ServerSetting = sys_serverSettingClass;
                    else panel_API_URL.ServerSetting = new sys_serverSettingClass();
                }
            }
        }
        static public void SaveAll(Form form, ref List<sys_serverSettingClass> sys_serverSettingClasses)
        {
            List<Control> controlList = GetAllControls(form);
            sys_serverSettingClass sys_serverSettingClass = new sys_serverSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_API_URL)
                {
                    Panel_API_URL panel_API_URL = ((Panel_API_URL)controlList[i]);
                    sys_serverSettingClass = sys_serverSettingClasses.MyFind(panel_API_URL.DeviceName, panel_API_URL.serverSetting_Type, panel_API_URL.content);
                    if (sys_serverSettingClass != null)
                    {
                        sys_serverSettingClasses.SetValue(panel_API_URL.ServerSetting);
                    }
                    else
                    {
                        sys_serverSettingClasses.Add(panel_API_URL.ServerSetting);
                    }
                }
            }
        }

        static public void SetValue(Form form, string deviceName, enum_sys_serverSetting_Type serverSetting_Type)
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
