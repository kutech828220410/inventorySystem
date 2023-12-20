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
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;

namespace 調劑台管理系統
{
    public partial class Dialog_共用區設置 : Form
    {


        public string api_server = "";
        public Table table_共用區 = null;
        public List<string> serverNames = new List<string>();
        public List<CheckBox> checkBoxes = new List<CheckBox>();
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

        public Dialog_共用區設置(Table table_共用區)
        {
            InitializeComponent();
            this.TopMost = true;
            this.Load += Dialog_共用區設置_Load;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.plC_RJ_Button_取消.MouseDownEvent += PlC_RJ_Button_取消_MouseDownEvent;
            api_server = 調劑台管理系統.Form1.API_Server;
            this.table_共用區 = table_共用區;
        }
        private void Dialog_共用區設置_Load(object sender, EventArgs e)
        {
            this.flowLayoutPanel.SuspendLayout();

            SQLUI.SQLControl sQLControl = new SQLControl(table_共用區.Server, table_共用區.DBName, table_共用區.TableName, table_共用區.Username, table_共用區.Password, table_共用區.Port.StringToUInt32(), MySql.Data.MySqlClient.MySqlSslMode.None);
            List<object[]> list_value = sQLControl.GetAllRows(null);
            List<object[]> list_value_buf = new List<object[]>();
            string json_result = Basic.Net.WEBApiGet($"{api_server}/api/ServerSetting");
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<HIS_DB_Lib.ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();

            serverNames = (from temp in serverSettingClasses
                           where temp.類別 == "調劑台"
                           select temp.設備名稱).Distinct().ToList();

            for (int i = 0; i < serverNames.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = false;
                checkBox.Size = new Size(200, 30);
                checkBox.Font = new Font("微軟正黑體", 16);
                checkBox.Text = $"{serverNames[i]}";
                list_value_buf = list_value.GetRows((int)enum_commonSpaceSetup.共用區名稱, serverNames[i]);
                if (list_value_buf.Count > 0)
                {
                    if(list_value_buf[0][(int)enum_commonSpaceSetup.是否共用].ObjectToString().ToUpper() == true.ToString().ToUpper())
                    {
                        checkBox.Checked = true;
                    }
                }
                checkBoxes.Add(checkBox);
                this.flowLayoutPanel.Controls.Add(checkBox);
            }
            this.flowLayoutPanel.ResumeLayout(false);

            
        }
        private void PlC_RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }

        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                SQLUI.SQLControl sQLControl = new SQLControl(table_共用區.Server, table_共用區.DBName, table_共用區.TableName, table_共用區.Username, table_共用區.Password, table_共用區.Port.StringToUInt32(), MySql.Data.MySqlClient.MySqlSslMode.None);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                for (int i = 0; i < checkBoxes.Count; i++)
                {
                    list_value_buf = list_value.GetRows((int)enum_commonSpaceSetup.共用區名稱, checkBoxes[i].Text);
                    if (list_value_buf.Count == 0)
                    {
                        object[] value = new object[new enum_commonSpaceSetup().GetLength()];
                        value[(int)enum_commonSpaceSetup.GUID] = Guid.NewGuid().ToString();
                        value[(int)enum_commonSpaceSetup.共用區名稱] = checkBoxes[i].Text;
                        value[(int)enum_commonSpaceSetup.共用區類型] = "調劑台";
                        value[(int)enum_commonSpaceSetup.是否共用] = checkBoxes[i].Checked.ToString();
                        value[(int)enum_commonSpaceSetup.設置時間] = DateTime.Now.ToDateTimeString();
                        list_value_add.Add(value);
                    }
                    else
                    {
                        object[] value = list_value_buf[0];
                        value[(int)enum_commonSpaceSetup.共用區名稱] = checkBoxes[i].Text;
                        value[(int)enum_commonSpaceSetup.共用區類型] = "調劑台";
                        value[(int)enum_commonSpaceSetup.是否共用] = checkBoxes[i].Checked.ToString();
                        value[(int)enum_commonSpaceSetup.設置時間] = DateTime.Now.ToDateTimeString();
                        list_value_replace.Add(value);
                    }
                }
                sQLControl.AddRows(null, list_value_add);
                sQLControl.UpdateByDefulteExtra(null, list_value_replace);

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }

      
    }
}
