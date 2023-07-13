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
    public partial class Dialog_ConfigSetting : Form
    {
        public computerConfigClass computerConfigClass = new computerConfigClass();
        private string aPIServer = "";
        private string deviceName = "";
        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }
        public Dialog_ConfigSetting(string APIServer,string DeviceName)
        {
            InitializeComponent();
            this.button_上傳.Click += Button_上傳_Click;
            this.button_讀取.Click += Button_讀取_Click;
            this.aPIServer = APIServer;
            this.deviceName = DeviceName;
        }
        private void Dialog_ConfigSetting_Load(object sender, EventArgs e)
        {
            this.label_DeviceName.Text = $"DeviceName : {deviceName}";
            this.label_API_URL.Text = $"API Server : {aPIServer}";
            computerConfigClass = computerConfigClass.DownloadConfig(aPIServer, deviceName);
            DB_TextBox.LoadAll(this.FindForm(), computerConfigClass);
            DB_CheckBox.LoadAll(this.FindForm(), computerConfigClass);
            this.comboBox_預設程式.Text = Form1.myConfigClass.Default_program;
        }
        private void Button_讀取_Click(object sender, EventArgs e)
        {
            computerConfigClass = computerConfigClass.DownloadConfig(aPIServer, deviceName);
            DB_TextBox.LoadAll(this.FindForm(), computerConfigClass);
            DB_CheckBox.LoadAll(this.FindForm(), computerConfigClass);
            if (computerConfigClass == null)
            {
                MyMessageBox.ShowDialog("讀取失敗!");
                return;
            }
            MyMessageBox.ShowDialog("讀取成功!");
        }
        private void Button_上傳_Click(object sender, EventArgs e)
        {
            DB_TextBox.SaveAll(this.FindForm(), ref computerConfigClass);
            DB_CheckBox.SaveAll(this.FindForm(), ref computerConfigClass);
            Form1.myConfigClass.Default_program = this.comboBox_預設程式.Text;
            Form1.SaveConfig();
            if (computerConfigClass.UploadConfig(aPIServer, computerConfigClass) == false)
            {
                MyMessageBox.ShowDialog("上傳失敗!");
                return;
            }
            MyMessageBox.ShowDialog("上傳成功!");
        }


    }
}
