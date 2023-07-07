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
    public partial class Panel_UpdateVersion : UserControl
    {
        static public string ApiURL = "";
        [Browsable(false)]
        public updateVersionClass UpdateVersionClass
        {
            get
            {
                updateVersionClass updateVersionClass = new updateVersionClass();
                updateVersionClass.program_name = label_標題.Text;
                updateVersionClass.filepath = textBox_filepath.Text;
                updateVersionClass.version = textBox_version.Text;
                updateVersionClass.update_time = label_更新時間.Text;              
                updateVersionClass.enable = checkBox_致能.Checked.ToString();
                return updateVersionClass;
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

        public Panel_UpdateVersion()
        {
            InitializeComponent();
            this.button_瀏覽.Click += Button_瀏覽_Click;
            this.button_上傳.Click += Button_上傳_Click;
            this.button_下載檔案.Click += Button_下載檔案_Click;
        }

   

        private void Button_上傳_Click(object sender, EventArgs e)
        {
            if (ApiURL.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("update version api url is empty!");
                return;
            }
            returnData returnData = new returnData();
            List<updateVersionClass> updateVersionClasses = new List<updateVersionClass>();
            updateVersionClasses.Add(UpdateVersionClass);
            returnData.Data = updateVersionClasses;
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
            this.saveFileDialog.Filter = $"預設檔案|{extension}";
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
            List<updateVersionClass> updateVersionClasses = returnData.Data.ObjToListClass<updateVersionClass>();
            List<updateVersionClass> updateVersionClasses_buf = new List<updateVersionClass>();
            List<Control> controlList = GetAllControls(form);
            ServerSettingClass serverSettingClass = new ServerSettingClass();
            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is Panel_UpdateVersion)
                {
                    Panel_UpdateVersion panel_UpdateVersion = ((Panel_UpdateVersion)controlList[i]);
                    updateVersionClasses_buf = (from temp in updateVersionClasses
                                                where temp.program_name == panel_UpdateVersion.program_name
                                                select temp).ToList();
                    if(updateVersionClasses_buf.Count > 0)
                    {
                        panel_UpdateVersion.UpdateVersionClass = updateVersionClasses_buf[0];
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
