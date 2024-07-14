using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;
using MyOffice;
using HIS_DB_Lib;
using H_Pannel_lib;
namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        public List<CheckBox> checkBoxes_共用區 = new List<CheckBox>();


        private void Program_設定_Init()
        {
            this.rJ_Button_存檔.MouseDownEvent += RJ_Button_存檔_MouseDownEvent;
            this.panel_調劑刷藥單顏色.Click += Panel_調劑刷藥單顏色_Click;
            this.panel_調劑中顏色.Click += Panel_調劑中顏色_Click;
            this.panel_調劑完閃爍顏色.Click += Panel_調劑完閃爍顏色_Click;


            string[] common_spaces_ary = Function_取得藥品區域名稱().ToArray();
            comboBox_亮燈區域.DataSource = common_spaces_ary;
            this.flowLayoutPanel_亮燈區域_共用.SuspendLayout();
            for (int i = 0; i < common_spaces_ary.Length; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = false;
                checkBox.Size = new Size(200, 30);
                checkBox.Font = new Font("微軟正黑體", 16);
                checkBox.Text = $"{common_spaces_ary[i]}";

                checkBoxes_共用區.Add(checkBox);
                this.flowLayoutPanel_亮燈區域_共用.Controls.Add(checkBox);
            }

            this.flowLayoutPanel_亮燈區域_共用.ResumeLayout(false);


            LoadConfig工程模式();
      
        }

        private void Panel_調劑完閃爍顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() != DialogResult.OK) return;
            this.panel_調劑完閃爍顏色.BackColor = this.colorDialog.Color;
        }
        private void Panel_調劑中顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() != DialogResult.OK) return;
            this.panel_調劑中顏色.BackColor = this.colorDialog.Color;
        }
        private void Panel_調劑刷藥單顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() != DialogResult.OK) return;
            this.panel_調劑刷藥單顏色.BackColor = this.colorDialog.Color;
        }
        #region StreamIO
        public static SaveConfig工程模式Class SaveConfig = null;
        [Serializable]
        public class SaveConfig工程模式Class
        {

            public Color Color_調劑刷藥單顏色 = Color.Blue;
            public Color Color_調劑完閃爍顏色 = Color.Green;
            public Color Color_調劑中顏色 = Color.Green;
            public string 調劑種類 = "科中";
            public string 亮燈區域 = "";
            public List<string> 共用亮燈區域 = new List<string>();
        }
        public void SaveConfig工程模式()
        {

            string StreamName = $@"{currentDirectory}\adminConfig.pro";
            SaveConfig工程模式Class saveConfig = new SaveConfig工程模式Class();

            this.Invoke(new Action(delegate
            {
                saveConfig.Color_調劑刷藥單顏色 = this.panel_調劑刷藥單顏色.BackColor;
                saveConfig.Color_調劑完閃爍顏色 = this.panel_調劑完閃爍顏色.BackColor;
                saveConfig.Color_調劑中顏色 = this.panel_調劑中顏色.BackColor;

                if (rJ_RatioButton_調劑種類_科中.Checked) saveConfig.調劑種類 = "科中";
                if (rJ_RatioButton_調劑種類_飲片.Checked) saveConfig.調劑種類 = "飲片";

                saveConfig.亮燈區域 = comboBox_亮燈區域.Text;
                saveConfig.共用亮燈區域.Clear();
                foreach (CheckBox checkBox in checkBoxes_共用區)
                {
                    if(checkBox.Checked)
                    {
                        saveConfig.共用亮燈區域.Add(checkBox.Text);
                    }
                }
                SaveConfig = saveConfig;
            }));


            Basic.FileIO.SaveProperties(saveConfig, StreamName);
        }
        public void LoadConfig工程模式()
        {
            string StreamName = $@"{currentDirectory}\adminConfig.pro";
            object temp = new object();
            Basic.FileIO.LoadProperties(ref temp, StreamName);
            if (temp is SaveConfig工程模式Class)
            {
                this.Invoke(new Action(delegate
                {
                    SaveConfig工程模式Class saveConfig = (SaveConfig工程模式Class)temp;
                    this.panel_調劑刷藥單顏色.BackColor = saveConfig.Color_調劑刷藥單顏色;
                    this.panel_調劑完閃爍顏色.BackColor = saveConfig.Color_調劑完閃爍顏色;
                    this.panel_調劑中顏色.BackColor = saveConfig.Color_調劑中顏色;

                    if (saveConfig.調劑種類 == "科中")
                    {
                        rJ_RatioButton_調劑種類_科中.Checked = true;
                    }
                    else
                    {
                        rJ_RatioButton_調劑種類_飲片.Checked = true;
                    }
                  
                    if (saveConfig.亮燈區域.StringIsEmpty() == false)
                    {
                        comboBox_亮燈區域.Text = saveConfig.亮燈區域;
                    }
                    if (saveConfig.共用亮燈區域 == null) saveConfig.共用亮燈區域 = new List<string>();
                    foreach (CheckBox checkBox in checkBoxes_共用區)
                    {
                        if (checkBox.Text == comboBox_亮燈區域.Text)
                        {
                            checkBox.Enabled = false;
                            checkBox.Checked = false;
                            continue;
                        }
                        for (int i = 0; i < saveConfig.共用亮燈區域.Count; i++)
                        {
                            if (checkBox.Text == saveConfig.共用亮燈區域[i])
                            {
                                checkBox.Checked = true;
                            }
                        }
                    }

                    SaveConfig = saveConfig;
                }));
            }

        }
        #endregion
        private void RJ_Button_存檔_MouseDownEvent(MouseEventArgs mevent)
        {
            this.SaveConfig工程模式();
            PLC.properties.Device.Set_Device("S8", true);
            if (plC_NumBox_亮度.Value == 0) plC_NumBox_亮度.Value = 80;
            RowsLEDUI.Lightness = (double)plC_NumBox_亮度.Value / 100D;
            MyMessageBox.ShowDialog("儲存成功!");
        }
    }
}
