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
        private int index = 1;
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (index <= 0) return;
                
                index = value;
                this.Invoke(new Action(delegate
                {
                    this.rJ_Lable_編號.Text = index.ToString();
                }));
            }
        }
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
                    this.rJ_Lable_病房名稱.Text = wardName;
                }));
            }
        }
        public bool Permission
        {
            get
            {
                return this.rJ_CheckBox_開門權限.Checked;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    this.rJ_CheckBox_開門權限.Checked = value;
                }));
            }
        }


        public OpenDoorPermission_UI()
        {
            InitializeComponent();
        }

        private void OpenDoorPermission_UI_Load(object sender, EventArgs e)
        {

        }


    }

    static public class OpenDoorPermissionMethod
    {
        static private MyConvert myConvert = new MyConvert();
        static public string GetOpenDoorPermission(this List<OpenDoorPermission_UI> openDoorPermission_UIs)
        {
            string value = "";
            byte[] bytes = new byte[openDoorPermission_UIs.Count / 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (openDoorPermission_UIs[(i * 8) + k].Permission == false) continue;
                    bytes[i] |= (byte)(1 << k);
                }
            }
            value = $"{bytes.ByteToStringHex()}";
            return value;
        }
        static public void SetOpenDoorPermission(this List<OpenDoorPermission_UI> openDoorPermission_UIs , string value)
        {
            byte[] bytes = value.StringHexTobytes();
            int index = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    index = i * 8 + k;
                    if (index >= openDoorPermission_UIs.Count) continue;
                    openDoorPermission_UIs[index].Permission = bytes[i].ByteGetBit(k);
                }
            }
        }
    
    }
}
