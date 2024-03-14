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
using MyUI;
using Basic;
using HIS_DB_Lib;
using H_Pannel_lib;
namespace 調劑台管理系統
{
    public partial class Dialog_抽屜選擇 : MyDialog
    {
     
        public List<string> Value
        {
            get
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < checkBoxes.Count; i++)
                {
                    if (checkBoxes[i].Checked) temp.Add(checkBoxes[i].Name);
                }
                return temp;
            }
        }
        

        List<Drawer> List_EPD583 = new List<Drawer>();
        List<Storage> List_EPD266 = new List<Storage>();
        List<Storage> List_Pannel35 = new List<Storage>();
        List<CheckBox> checkBoxes_大抽屜 = new List<CheckBox>();
        List<CheckBox> checkBoxes_小抽屜 = new List<CheckBox>();
        List<CheckBox> checkBoxes
        {
            get
            {
                List<CheckBox> temp = new List<CheckBox>();
                for (int i = 0; i < checkBoxes_大抽屜.Count; i++)
                {
                    temp.Add(checkBoxes_大抽屜[i]);
                }
                for (int i = 0; i < checkBoxes_小抽屜.Count; i++)
                {
                    temp.Add(checkBoxes_小抽屜[i]);
                }
                return temp;
            }
        }
        public Dialog_抽屜選擇()
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_返回.MouseDownEvent += RJ_Button_返回_MouseDownEvent;
            this.button_大抽屜_全部勾選.Click += Button_大抽屜_全部勾選_Click;
            this.button_大抽屜_全部取消.Click += Button_大抽屜_全部取消_Click;
            this.button_小抽屜_全部勾選.Click += Button_小抽屜_全部勾選_Click;
            this.button_小抽屜_全部取消.Click += Button_小抽屜_全部取消_Click;
            this.LoadFinishedEvent += Dialog_抽屜選擇_LoadFinishedEvent;
            this.FormClosing += Dialog_抽屜選擇_FormClosing;

            List_EPD583 = Main_Form.List_EPD583_本地資料;
            List_EPD266 = (from temp in Main_Form.List_EPD266_本地資料
                           where temp.DeviceType == DeviceType.EPD266_lock || temp.DeviceType == DeviceType.EPD290_lock
                           select temp).ToList();
            List_Pannel35 = (from temp in Main_Form.List_Pannel35_本地資料
                           where temp.DeviceType == DeviceType.Pannel35_lock
                           select temp).ToList();


            List_EPD583.Sort(new ICP_Drawer());
            List_EPD266.Sort(new ICP_Storage());
            List_Pannel35.Sort(new ICP_Storage());
            Function_Refresh();

            Main_Form.LoadMyParameter();
            
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                List<string> list_抽屜IP = (from temp in Main_Form.myParameter.交班開鎖抽屜
                                          where temp == checkBoxes[i].Name
                                          select temp).ToList();
                if(list_抽屜IP.Count > 0)
                {
                    checkBoxes[i].Checked = true;
                }
            }
        }


        private void Function_Refresh()
        {
            this.flowLayoutPanel_大抽屜.SuspendLayout();
            this.flowLayoutPanel_大抽屜.Controls.Clear();
            for (int i = 0; i < List_EPD583.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = false;
                checkBox.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                checkBox.ForeColor = System.Drawing.Color.Black;
                checkBox.Name = $"{List_EPD583[i].IP}";
                checkBox.Size = new System.Drawing.Size(180, 35);
                checkBox.TabIndex = 0;
                string[] ip_ary = List_EPD583[i].IP.Split('.');
                if (ip_ary.Length != 4) continue;
                checkBox.Text = $"[{ip_ary[2]}.{ip_ary[3]}]{List_EPD583[i].Name}";
                checkBox.UseVisualStyleBackColor = true;
                checkBoxes_大抽屜.Add(checkBox);
                this.flowLayoutPanel_大抽屜.Controls.Add(checkBox);
            }
            if (this.flowLayoutPanel_大抽屜.Controls.Count > 0) rJ_Pannel_大抽屜.Visible = true;
            this.flowLayoutPanel_大抽屜.ResumeLayout(false);

            this.flowLayoutPanel_小抽屜.SuspendLayout();
            this.flowLayoutPanel_小抽屜.Controls.Clear();
            for (int i = 0; i < List_EPD266.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = false;
                checkBox.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                checkBox.ForeColor = System.Drawing.Color.Black;
                checkBox.Name = $"{List_EPD266[i].IP}";
                checkBox.Size = new System.Drawing.Size(180, 35);
                checkBox.TabIndex = 0;
                string[] ip_ary = List_EPD266[i].IP.Split('.');
                if (ip_ary.Length != 4) continue;
                checkBox.Text = $"[{ip_ary[2]}.{ip_ary[3]}]{List_EPD266[i].Name}";
                checkBox.UseVisualStyleBackColor = true;
                checkBoxes_小抽屜.Add(checkBox);
                this.flowLayoutPanel_小抽屜.Controls.Add(checkBox);
            }
            for (int i = 0; i < List_Pannel35.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = false;
                checkBox.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                checkBox.ForeColor = System.Drawing.Color.Black;
                checkBox.Name = $"{List_Pannel35[i].IP}";
                checkBox.Size = new System.Drawing.Size(180, 35);
                checkBox.TabIndex = 0;
                string[] ip_ary = List_Pannel35[i].IP.Split('.');
                if (ip_ary.Length != 4) continue;
                checkBox.Text = $"[{ip_ary[2]}.{ip_ary[3]}]{List_Pannel35[i].Name}";
                checkBox.UseVisualStyleBackColor = true;
                checkBoxes_小抽屜.Add(checkBox);
                this.flowLayoutPanel_小抽屜.Controls.Add(checkBox);
            }
            if (this.flowLayoutPanel_小抽屜.Controls.Count > 0) rJ_Pannel_小抽屜.Visible = true;
            this.flowLayoutPanel_小抽屜.ResumeLayout(false);

            for (int i = 0; i < checkBoxes.Count; i++)
            {
                checkBoxes[i].CheckStateChanged += Dialog_抽屜選擇_CheckStateChanged;
            }
        }




        #region Event
        private void Dialog_抽屜選擇_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                checkBox.BackColor = Color.Green;
                checkBox.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                checkBox.BackColor = Color.Transparent;
                checkBox.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void Button_大抽屜_全部取消_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkBoxes_大抽屜.Count; i++)
            {
                checkBoxes_大抽屜[i].Checked = false;
            }
        }
        private void Button_大抽屜_全部勾選_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkBoxes_大抽屜.Count; i++)
            {
                checkBoxes_大抽屜[i].Checked = true;
            }
        }
        private void Button_小抽屜_全部取消_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkBoxes_小抽屜.Count; i++)
            {
                checkBoxes_小抽屜[i].Checked = false;
            }
        }
        private void Button_小抽屜_全部勾選_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkBoxes_小抽屜.Count; i++)
            {
                checkBoxes_小抽屜[i].Checked = true;
            }
        }
        private void RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Main_Form.myParameter.交班開鎖抽屜 = Value;
                Main_Form.SaveMyParameter();
               
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Main_Form.myParameter.交班開鎖抽屜 = Value;
                Main_Form.SaveMyParameter();
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        private void Dialog_抽屜選擇_LoadFinishedEvent(EventArgs e)
        {
           
        }
        private void Dialog_抽屜選擇_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        #endregion

        private class ICP_Drawer : IComparer<Drawer>
        {
            public int Compare(Drawer x, Drawer y)
            {
                string IP_0 = x.IP;
                string IP_1 = y.IP;
                string[] IP_0_Array = IP_0.Split('.');
                string[] IP_1_Array = IP_1.Split('.');
                IP_0 = "";
                IP_1 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];

                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];

                    IP_0 += IP_0_Array[i];
                    IP_1 += IP_1_Array[i];
                }
                int cmp = IP_0_Array[2].CompareTo(IP_1_Array[2]);
                if (cmp > 0)
                {
                    return 1;
                }
                else if (cmp < 0)
                {
                    return -1;
                }
                else if (cmp == 0)
                {
                    cmp = IP_0_Array[3].CompareTo(IP_1_Array[3]);
                    if (cmp > 0)
                    {
                        return 1;
                    }
                    else if (cmp < 0)
                    {
                        return -1;
                    }
                    else if (cmp == 0)
                    {
                        return 0;
                    }
                }

                return 0;

            }
        }

        private class ICP_Storage : IComparer<Storage>
        {
            public int Compare(Storage x, Storage y)
            {
                string IP_0 = x.IP;
                string IP_1 = y.IP;
                string[] IP_0_Array = IP_0.Split('.');
                string[] IP_1_Array = IP_1.Split('.');
                IP_0 = "";
                IP_1 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];

                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];

                    IP_0 += IP_0_Array[i];
                    IP_1 += IP_1_Array[i];
                }
                int cmp = IP_0_Array[2].CompareTo(IP_1_Array[2]);
                if (cmp > 0)
                {
                    return 1;
                }
                else if (cmp < 0)
                {
                    return -1;
                }
                else if (cmp == 0)
                {
                    cmp = IP_0_Array[3].CompareTo(IP_1_Array[3]);
                    if (cmp > 0)
                    {
                        return 1;
                    }
                    else if (cmp < 0)
                    {
                        return -1;
                    }
                    else if (cmp == 0)
                    {
                        return 0;
                    }
                }

                return 0;

            }
        }
    }
}
