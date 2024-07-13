using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;
using H_Pannel_lib;
namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        private void Program_設定_Init()
        {
            this.rJ_Button_存檔.MouseDownEvent += RJ_Button_存檔_MouseDownEvent;
            this.panel_調劑刷藥單顏色.Click += Panel_調劑刷藥單顏色_Click;
            this.panel_調劑中顏色.Click += Panel_調劑中顏色_Click;
            this.panel_調劑完閃爍顏色.Click += Panel_調劑完閃爍顏色_Click;
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
        [Serializable]
        private class SaveConfig工程模式Class
        {

            public Color Color_調劑刷藥單顏色 = Color.Blue;
            public Color Color_調劑完閃爍顏色 = Color.Green;
            public Color Color_調劑中顏色 = Color.Green;
            public string 調劑種類 = "科中";
        }
        public void SaveConfig工程模式()
        {

            string StreamName = $@"{currentDirectory}\adminConfig.pro";
            SaveConfig工程模式Class saveConfig = new SaveConfig工程模式Class();

            saveConfig.Color_調劑刷藥單顏色 = this.panel_調劑刷藥單顏色.BackColor;
            saveConfig.Color_調劑完閃爍顏色 = this.panel_調劑完閃爍顏色.BackColor;
            saveConfig.Color_調劑中顏色 = this.panel_調劑中顏色.BackColor;

            if (rJ_RatioButton_調劑種類_科中.Checked) saveConfig.調劑種類 = "科中";
            if (rJ_RatioButton_調劑種類_飲片.Checked) saveConfig.調劑種類 = "飲片";

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

                    this.panel_調劑刷藥單顏色.BackColor = ((SaveConfig工程模式Class)temp).Color_調劑刷藥單顏色;
                    this.panel_調劑完閃爍顏色.BackColor = ((SaveConfig工程模式Class)temp).Color_調劑完閃爍顏色;
                    this.panel_調劑中顏色.BackColor = ((SaveConfig工程模式Class)temp).Color_調劑中顏色;

                    if (((SaveConfig工程模式Class)temp).調劑種類 == "科中")
                    {
                        rJ_RatioButton_調劑種類_科中.Checked = true;
                    }
                    else
                    {
                        rJ_RatioButton_調劑種類_飲片.Checked = true;

                    }
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
