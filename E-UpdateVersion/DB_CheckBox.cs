using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;

namespace E_UpdateVersion
{
    public partial class DB_CheckBox : CheckBox
    {
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
        private string type = "";
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        private string configName = "";
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public string ConfigName
        {
            get
            {
                return this.configName;
            }
            set
            {
                this.configName = value;
                this.Text = value;
            }
        }
        private int titleWidth = 100;
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public int TitleWidth
        {
            get
            {
                return this.titleWidth;
            }
            set
            {
                this.titleWidth = value;
                this.Width = value;
            }
        }
        [ReadOnly(false), Browsable(false), Category(""), Description(""), DefaultValue("")]
        public string Value
        {
            get
            {
                return this.Checked.ToString();
            }
            set
            {
                if (this.IsHandleCreated == false) return;
                if(value.ToUpper() == true.ToString().ToUpper())
                {
                    this.Checked = true;
                }
                else
                {
                    this.Checked = false;
                }
            }
        }

        public DB_CheckBox()
        {
            InitializeComponent();
        }
        public void init(string DeviceName, string ConfigName, string Value)
        {
            this.DeviceName = DeviceName;
            this.ConfigName = ConfigName;
            this.Value = Value;
        }
        static public void LoadAll(Form form, computerConfigClass computerConfigClass)
        {
            List<Control> controlList = GetAllControls(form);
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is DB_CheckBox)
                {
                    DB_CheckBox dB_CheckBox = ((DB_CheckBox)controlList[i]);
                    sub_computerConfigClass sub_ComputerConfigClass = computerConfigClass.FindParameter(dB_CheckBox.type, dB_CheckBox.configName);
                    if (sub_ComputerConfigClass != null)
                    {
                        dB_CheckBox.Value = sub_ComputerConfigClass.value;
                    }
                }
            }
        }
        static public void SaveAll(Form form, ref computerConfigClass _computerConfigClass)
        {
            List<Control> controlList = GetAllControls(form);
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is DB_CheckBox)
                {
                    DB_CheckBox dB_CheckBox = ((DB_CheckBox)controlList[i]);
                    _computerConfigClass.SetValue(dB_CheckBox.type, dB_CheckBox.configName, dB_CheckBox.Value);
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
