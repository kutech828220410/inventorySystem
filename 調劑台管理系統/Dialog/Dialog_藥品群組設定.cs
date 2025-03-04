using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_藥品群組設定 : MyDialog
    {
        private List<CheckBox> checkBoxes = new List<CheckBox>();
        private CheckBox CheckBox_全選;
        public Dialog_藥品群組設定()
        {
            InitializeComponent();


            this.Load += Dialog_藥品群組設定_Load;
            this.LoadFinishedEvent += Dialog_藥品群組設定_LoadFinishedEvent;
            //this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            //this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            rJ_Button_存檔.MouseDownEvent += RJ_Button_存檔_MouseDownEvent;
            comboBox_藥品群組.DataSource = medGroupClass.get_medGroupList(Main_Form.API_Server);
            comboBox_藥品群組.DisplayMember = "藥品群組名稱";
            if(comboBox_藥品群組.Items.Count == 0)
            {
                MyMessageBox.ShowDialog("藥品群組不存在");
                return;
            }
            comboBox_藥品群組.SelectedIndexChanged += ComboBox_藥品群組_SelectedIndexChanged;
        }

        public void update_checkBox()
        {
            LoadingForm.ShowLoadingForm();
            string text = comboBox_藥品群組.Text;
            medGroupClass medGroupClass = new medGroupClass();
            medGroupClass = medGroupClass.get_medGroup(Main_Form.API_Server, text);
            if (medGroupClass == null)
            {
                MyMessageBox.ShowDialog("藥品群組不存在");
                return;
            }
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                checkBoxes[i].Checked = medGroupClass.顯示資訊.Contains(checkBoxes[i].Text);
            }
            LoadingForm.CloseLoadingForm();
        }
        private void ComboBox_藥品群組_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_checkBox();
        }
        private void RJ_Button_存檔_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = comboBox_藥品群組.GetComboBoxText();
            medGroupClass medGroupClass = new medGroupClass();
            medGroupClass = medGroupClass.get_medGroup(Main_Form.API_Server, text);
            if (medGroupClass == null)
            {
                MyMessageBox.ShowDialog("藥品群組不存在");
                return;
            }
            List<string> serverNames = new List<string>();
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i].Checked)
                {
                    serverNames.Add(checkBoxes[i].Text);
                }
            }
            medGroupClass.update_visible_info(Main_Form.API_Server, medGroupClass.GUID, serverNames);
            MyMessageBox.ShowDialog("儲存成功");
        }
        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Close();
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
        
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void Dialog_藥品群組設定_Load(object sender, EventArgs e)
        {
            this.flowLayoutPanel.SuspendLayout();

            List<HIS_DB_Lib.sys_serverSettingClass> sys_serverSettingClasses = sys_serverSettingClass.get_serversetting_by_type(Main_Form.API_Server,"調劑台");

            List<string> serverNames = (from temp in sys_serverSettingClasses
                           where temp.類別 == "調劑台"
                           select temp.設備名稱).Distinct().ToList();

            CheckBox checkBox;
            checkBox = new CheckBox();
            checkBox.AutoSize = false;
            checkBox.Size = new Size(200, 30);
            checkBox.Font = new Font("微軟正黑體", 16);
            checkBox.Text = $"全選";
            checkBox.CheckedChanged += CheckBox_CheckedChanged;
            CheckBox_全選 = checkBox;
            this.flowLayoutPanel.Controls.Add(checkBox);

            for (int i = 0; i < serverNames.Count; i++)
            {
                checkBox = new CheckBox();
                checkBox.AutoSize = false;
                checkBox.Size = new Size(200, 30);
                checkBox.Font = new Font("微軟正黑體", 16);
                checkBox.Text = $"{serverNames[i]}";
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                checkBoxes.Add(checkBox);
                this.flowLayoutPanel.Controls.Add(checkBox);
            }
            this.flowLayoutPanel.ResumeLayout(false);
  
         
        }

        private void Dialog_藥品群組設定_LoadFinishedEvent(EventArgs e)
        {
            comboBox_藥品群組.SelectedIndex = 0;

            this.Refresh();
            update_checkBox();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox checkBox = (CheckBox)sender;
                if (checkBox.Text == "全選")
                {
                    for (int i = 0; i < checkBoxes.Count; i++)
                    {
                        checkBoxes[i].CheckedChanged -= CheckBox_CheckedChanged;
                        checkBoxes[i].Checked = checkBox.Checked;
                        checkBoxes[i].CheckedChanged += CheckBox_CheckedChanged;
                    }
                }
                else
                {
                    bool allChecked = true;
                    for (int i = 0; i < checkBoxes.Count; i++)
                    {
                        if (checkBoxes[i].Checked == false)
                        {
                            allChecked = false;
                            break;
                        }
                    }
                    CheckBox_全選.CheckedChanged -= CheckBox_CheckedChanged;
                    CheckBox_全選.Checked = allChecked;
                    CheckBox_全選.CheckedChanged += CheckBox_CheckedChanged;
                }
            }
        }
        
    }
}
