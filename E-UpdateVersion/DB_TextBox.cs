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
    public partial class DB_TextBox : UserControl
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
                this.label_標題.Text = value;
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
                this.label_標題.Width = value;
            }
        }
        [ReadOnly(false), Browsable(false), Category(""), Description(""), DefaultValue("")]
        public string Value
        {
            get
            {
                return this.textBox.Text;
            }
            set
            {
                if (this.IsHandleCreated == false) return;
                this.textBox.Text = value;
            }
        }
        public DB_TextBox()
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
                if (controlList[i] is DB_TextBox)
                {
                    DB_TextBox dB_TextBox = ((DB_TextBox)controlList[i]);
                    sub_computerConfigClass sub_ComputerConfigClass = computerConfigClass.FindParameter(dB_TextBox.type, dB_TextBox.configName);
                    if (sub_ComputerConfigClass != null)
                    {
                        dB_TextBox.Value = sub_ComputerConfigClass.value;
                    }
                }
            }
        }
        static public void SaveAll(Form form, ref computerConfigClass _computerConfigClass)
        {
            List<Control> controlList = GetAllControls(form);
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is DB_TextBox)
                {
                    DB_TextBox dB_TextBox = ((DB_TextBox)controlList[i]);
                    _computerConfigClass.SetValue(dB_TextBox.type, dB_TextBox.configName, dB_TextBox.Value);
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
