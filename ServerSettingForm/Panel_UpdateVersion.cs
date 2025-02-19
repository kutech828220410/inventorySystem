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
    public partial class Panel_sys_updateVersion : UserControl
    {
        static public string ApiURL = "";
        [Browsable(false)]
        public sys_updateVersionClass sys_updateVersionClass
        {
            get
            {
                sys_updateVersionClass sys_updateVersionClass = new sys_updateVersionClass();
                sys_updateVersionClass.program_name = label_標題.Text;
                sys_updateVersionClass.filepath = textBox_filepath.Text;
                sys_updateVersionClass.version = textBox_version.Text;
                sys_updateVersionClass.update_time = label_更新時間.Text;              
                sys_updateVersionClass.enable = checkBox_致能.Checked.ToString();
                return sys_updateVersionClass;
            }
            set
            {
                textBox_filepath.Text = value.filepath;
                textBox_version.Text = value.version;
                label_更新時間.Text = value.update_time;
                checkBox_致能.Checked = (value.enable == true.ToString());

            }
        }

        private string program_name = "";
        [ReadOnly(false), Browsable(true), Category("config"), Description(""), DefaultValue("")]
        public string Program_name 
        {
            get => program_name; 
            set
            {
                this.label_標題.Text = value;
                program_name = value;
            }
        }

        public Panel_sys_updateVersion()
        {
            InitializeComponent();
            this.button_瀏覽.Click += Button_瀏覽_Click;
            this.button_上傳.Click += Button_上傳_Click;
            this.button_下載檔案.Click += Button_下載檔案_Click;
            this.button_刪除.Click += Button_刪除_Click;
        }


        private void Button_刪除_Click(object sender, EventArgs e)
        {
            if (ApiURL.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("update version api url is empty!");
                return;
            }
            returnData returnData = new returnData();

            returnData.Value = sys_updateVersionClass.program_name;
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{ApiURL}/api/update/delete", json_in);
            MyMessageBox.ShowDialog(json);
        }
        private void Button_上傳_Click(object sender, EventArgs e)
        {
            if (ApiURL.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("update version api url is empty!");
                return;
            }
            returnData returnData = new returnData();
            List<sys_updateVersionClass> sys_updateVersionClasses = new List<sys_updateVersionClass>();
            sys_updateVersionClasses.Add(sys_updateVersionClass);
            returnData.Data = sys_updateVersionClasses;
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{ApiURL}/api/update/add", json_in);
            MyMessageBox.ShowDialog(json);
        }
        private void Button_瀏覽_Click(object sender, EventArgs e)
        {
            if(this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_filepath.Text = this.openFileDialog.FileName;
            }
        }
        private void Button_下載檔案_Click(object sender, EventArgs e)
        {
            string url = $"{ApiURL}/api/update/download/{program_name}";
            string extension = Basic.Net.WEBApiGet($"{ApiURL}/api/update/extension/{program_name}");
            this.saveFileDialog.Filter = $"預設檔案|.zip";
            if (extension.StringIsEmpty())
            {
                MyMessageBox.ShowDialog($"URL:{url} , 取得失敗!");
            }

            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filename = this.saveFileDialog.FileName;
                string saveFilePath = System.IO.Path.GetFileNameWithoutExtension(filename);
                saveFilePath = $"{saveFilePath}{extension}";
                if(Basic.Net.DownloadFile($"{ApiURL}/api/update/download/{program_name}", filename)) MyMessageBox.ShowDialog("下載完成!");
                else MyMessageBox.ShowDialog("下載失敗!");
            }
        }

        static public void LoadAll(Form form)
        {
            string json_result = Basic.Net.WEBApiGet($"{ApiURL}/api/update");
            if(json_result.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("update version 讀取失敗!");
                return;
            }
            returnData returnData = json_result.JsonDeserializet<returnData>();
            if(returnData.Code != 200)
            {
                MyMessageBox.ShowDialog(returnData.Result);
                return;
            }
            List<sys_updateVersionClass> sys_updateVersionClasses = returnData.Data.ObjToListClass<sys_updateVersionClass>();
            List<sys_updateVersionClass> sys_updateVersionClasses_buf = new List<sys_updateVersionClass>();
            List<Control> controlList = GetAllControls(form);
            sys_serverSettingClass sys_serverSettingClass = new sys_serverSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_sys_updateVersion)
                {
                    Panel_sys_updateVersion panel_sys_updateVersion = ((Panel_sys_updateVersion)controlList[i]);
                    sys_updateVersionClasses_buf = (from temp in sys_updateVersionClasses
                                                where temp.program_name == panel_sys_updateVersion.program_name
                                                select temp).ToList();
                    if(sys_updateVersionClasses_buf.Count > 0)
                    {
                        string version = Basic.Net.WEBApiGet($"{ApiURL}/api/update/version/{sys_updateVersionClasses_buf[0].program_name}");
                        sys_updateVersionClasses_buf[0].version = version;
                        panel_sys_updateVersion.sys_updateVersionClass = sys_updateVersionClasses_buf[0];
                    }
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
