using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace 勤務傳送櫃
{
    public partial class OpenDoorPermission_UI : UserControl
    {
   
        private string wardName = "病房名稱";
        public string WardName
        {
            get
            {
                return wardName;
            }
            set
            {
                this.wardName = value;
                this.Invoke(new Action(delegate
                {
                    this.label_wardname.Text = wardName;
                }));
            }
        }
        private bool permission = false;
        public bool Permission
        {
            get
            {
                return this.permission;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    this.permission = value;
                    if(permission)
                    {
                        this.label_wardname.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        this.label_wardname.BackColor = Color.GhostWhite;
                    }
                }));
            }
        }


        public OpenDoorPermission_UI()
        {
            InitializeComponent();
            this.Click += OpenDoorPermission_UI_Click;
            this.label_wardname.Click += OpenDoorPermission_UI_Click;
        }

        private void OpenDoorPermission_UI_Click(object sender, EventArgs e)
        {
            Permission = !Permission;
        }

        private void OpenDoorPermission_UI_Load(object sender, EventArgs e)
        {

        }


    }

    static public class OpenDoorPermissionMethod
    {
        static private MyConvert myConvert = new MyConvert();
        static public bool GetOpenDoorPermission(string value, List<string> wardNames)
        {
            List<string> list_value = value.JsonDeserializet<List<string>>();
            if (list_value == null) return false;
            for (int i = 0; i < list_value.Count; i++)
            {
                for(int k = 0; k < wardNames.Count; k++)
                {
                    if (list_value[i] == wardNames[k]) return true;
                }
              
            }
            return false;
        }
        static public bool GetOpenDoorPermission(string value, string wardName)
        {
            List<string> list_value = value.JsonDeserializet<List<string>>();
            if (list_value == null) return false;
            for (int i = 0; i < list_value.Count; i++)
            {
                if (list_value[i] == wardName) return true;
            }
            return false;
        }
        static public string GetOpenDoorPermission(this List<OpenDoorPermission_UI> openDoorPermission_UIs)
        {
            List<string> list_value = new List<string>();
            for (int i = 0; i < openDoorPermission_UIs.Count; i++)
            {
                if(openDoorPermission_UIs[i].Permission)
                {
                    if (openDoorPermission_UIs[i].WardName.StringIsEmpty() == false)
                    {
                        list_value.Add(openDoorPermission_UIs[i].WardName);
                    }
                }
            }

            return list_value.JsonSerializationt();
        }
        static public void SetOpenDoorPermission(this List<OpenDoorPermission_UI> openDoorPermission_UIs, string value)
        {
            List<string> list_value = value.JsonDeserializet<List<string>>();
     
            if (list_value == null)
            {
                for (int k = 0; k < openDoorPermission_UIs.Count; k++)
                {
                    openDoorPermission_UIs[k].Permission = false;
                }
                return;
            }
            SetOpenDoorPermission(openDoorPermission_UIs, list_value);
        }
        static public void SetOpenDoorPermission(this List<OpenDoorPermission_UI> openDoorPermission_UIs , List<string> list_value)
        {
            for (int k = 0; k < openDoorPermission_UIs.Count; k++)
            {
                openDoorPermission_UIs[k].Permission = false;
            }
            List<OpenDoorPermission_UI> openDoorPermission_UIs_buf = new List<OpenDoorPermission_UI>();
            for (int i = 0; i < list_value.Count; i++)
            {
                openDoorPermission_UIs_buf = (from temp in openDoorPermission_UIs
                                              where temp.WardName == list_value[i]
                                              select temp).ToList();
                for (int k = 0; k< openDoorPermission_UIs_buf.Count; k++)
                {
                    openDoorPermission_UIs_buf[k].Permission = true;
                }
            }
        }
    
    }
}
