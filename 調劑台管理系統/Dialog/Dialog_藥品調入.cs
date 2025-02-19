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
using MyUI;
using SQLUI;
using DrawingClass;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_藥品調入 : MyDialog
    {
        private List<medClass> medClasses_local = new List<medClass>();
        private string 藥碼 = "";
        private string 藥名 = "";
        private string 單位 = "";
        private string Selected_ServerName = "";


        List<RJ_Pannel> rJ_Pannels = new List<RJ_Pannel>();
        private MyThread MyThread_program;
        public Dialog_藥品調入()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
                this.LoadFinishedEvent += Dialog_藥品調入_LoadFinishedEvent;
                this.rJ_Button_搜尋.MouseDownEvent += RJ_Button_搜尋_MouseDownEvent;
                this.rJ_Button_返回.MouseDownEvent += RJ_Button_返回_MouseDownEvent;
                this.rJ_Button_確認送出.MouseDownEvent += RJ_Button_確認送出_MouseDownEvent;
                this.FormClosing += Dialog_藥品調入_FormClosing;
                this.Load += Dialog_藥品調入_Load;

                Table table_已選藥品 = new Table(new enum_drugDispatch());
                sqL_DataGridView_已選藥品.Init(table_已選藥品);
                sqL_DataGridView_已選藥品.Set_ColumnVisible(false, new enum_drugDispatch().GetEnumNames());
                sqL_DataGridView_已選藥品.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.藥碼);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_drugDispatch.藥名);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.單位);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.出庫庫別);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.出庫庫存);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.出庫量);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.入庫庫存);
                sqL_DataGridView_已選藥品.Set_ColumnText("調出庫別", enum_drugDispatch.出庫庫別.GetEnumName());
                sqL_DataGridView_已選藥品.Set_ColumnText("調出庫存", enum_drugDispatch.出庫庫存.GetEnumName());
                sqL_DataGridView_已選藥品.Set_ColumnText("調出量", enum_drugDispatch.出庫量.GetEnumName());
                sqL_DataGridView_已選藥品.Set_ColumnText("本台庫存", enum_drugDispatch.入庫庫存.GetEnumName());
            }));
        }

        public void Function_SerchByBarCode(string barCode)
        {
            
           List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, barCode);
            if (medClasses.Count == 0)
            {
                Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\fail_01.wav");
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }

            Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\sucess_01.wav");

            List<sys_serverSettingClass> sys_serverSettingClasses = sys_serverSettingClass.get_serversetting_by_type(Main_Form.API_Server, "調劑台");
            List<string> serverNames = (from temp in sys_serverSettingClasses
                                        select temp.設備名稱.Trim()).Distinct().OrderBy(name => name).ToList();
            serverNames.Remove(Main_Form.ServerName);
            List<Task> tasks = new List<Task>();
            List<medClass> medClasses_Ary = new List<medClass>();
            tasks.Add(Task.Run(new Action(delegate
            {
                medClasses_local = medClass.get_dps_medClass_by_code(Main_Form.API_Server, Main_Form.ServerName, medClasses[0].藥品碼);
            })));

            tasks.Add(Task.Run(new Action(delegate
            {
                string Code = medClasses[0].藥品碼;

                medClasses_Ary = medClass.get_datas_dps_medClass_by_code(Main_Form.API_Server, serverNames, Code);
            })));
            Task.WhenAll(tasks).Wait();
            if (medClasses_local[0].DeviceBasics.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("本台無此藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }

            if (medClasses_Ary == null) return;
            bool flag_IsMedOn = false;
            for(int i = 0; i < medClasses_Ary.Count; i++)
            {           
                藥碼 = medClasses_Ary[0].藥品碼;
                藥名 = medClasses_Ary[0].藥品名稱;
                flag_IsMedOn = true;
            }
            if (flag_IsMedOn == false)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無藥品儲位", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            this.Invoke(new Action(delegate
            {
                rJ_Lable_State.Text = $"請選擇調出台";
            }));
 
            RefreshUI(serverNames ,medClasses_Ary);
 
        }
        public double Function_取得選取台號庫存(RJ_Lable rJ_Lable)
        {
            object sender = rJ_Lable.Parent;
            if(sender is RJ_Pannel)
            {
                RJ_Pannel rJ_Pannel = (RJ_Pannel)sender;
                for (int i = 0; i < rJ_Pannel.Controls.Count; i++)
                {
                    if (rJ_Pannel.Controls[i] is RJ_Lable)
                    {
                        RJ_Lable temp = (RJ_Lable)rJ_Pannel.Controls[i];
                        if (temp.GUID == "庫存")
                        {
                            return temp.Text.StringToDouble();
                        }
                    }
                }
            }
            return -1;
        }
        public void ClearUI()
        {
            this.Invoke(new Action(delegate 
            {
                this.flowLayoutPanel_調劑台選擇.Controls.Clear();
                this.flowLayoutPanel_調劑台選擇.Refresh();
                藥碼 = "";
                藥名 = "";
                單位 = "";
                Selected_ServerName = "";
                this.rJ_Lable_State.Text = "等待藥品帶入";
                this.rJ_Lable_藥碼.Text = "(-------)";
                this.rJ_Lable_藥名.Text = "------------------------------";
            }));
        }
        public void RefreshUI(List<string> serverNames ,List<medClass> medClasses_Ary)
        {

            this.Invoke(new Action(delegate 
            {
                this.flowLayoutPanel_調劑台選擇.Dock = System.Windows.Forms.DockStyle.Fill;
                this.flowLayoutPanel_調劑台選擇.Location = new System.Drawing.Point(0, 0);
                this.flowLayoutPanel_調劑台選擇.Size = new System.Drawing.Size(1002, 283);

                this.flowLayoutPanel_調劑台選擇.Controls.Clear();
                this.flowLayoutPanel_調劑台選擇.Refresh();

                for (int i = 0; i < medClasses_Ary.Count; i++)
                {
                    if (medClasses_Ary == null) continue;
                    if (medClasses_Ary.Count == 0) continue;

                    rJ_Lable_藥碼.Text = $"({medClasses_Ary[i].藥品碼})";
                    rJ_Lable_藥名.Text = $"{medClasses_Ary[i].藥品名稱}";
                    this.單位 = $"{medClasses_Ary[i].包裝單位}";

                    RJ_Lable rJ_Lable_庫存 = new RJ_Lable();
                    RJ_Lable rJ_Lable_台號 = new RJ_Lable();

                    Color color = new Color();
                    color = Color.DimGray;
                    RJ_Pannel rJ_Pannel = new RJ_Pannel();

                    rJ_Pannel.BackColor = System.Drawing.Color.White;
                    rJ_Pannel.BackgroundColor = System.Drawing.Color.Transparent;
                    rJ_Pannel.BorderColor = color;
                    rJ_Pannel.BorderRadius = 10;
                    rJ_Pannel.BorderSize = 3;
                    rJ_Pannel.Controls.Add(rJ_Lable_庫存);
                    rJ_Pannel.Controls.Add(rJ_Lable_台號);
                    rJ_Pannel.ForeColor = System.Drawing.Color.White;
                    rJ_Pannel.IsSelected = false;
                    rJ_Pannel.Location = new System.Drawing.Point(3, 3);
                    rJ_Pannel.Padding = new System.Windows.Forms.Padding(5, 5, 10, 10);
                    rJ_Pannel.ShadowColor = color;
                    rJ_Pannel.ShadowSize = 3;
                    rJ_Pannel.Size = new System.Drawing.Size(192, 135);
                    rJ_Pannel.GUID = Guid.NewGuid().ToString();
   
                    
                    rJ_Lable_庫存.BackColor = System.Drawing.Color.White;
                    rJ_Lable_庫存.BackgroundColor = System.Drawing.Color.White;
                    rJ_Lable_庫存.BorderColor = System.Drawing.Color.PaleVioletRed;
                    rJ_Lable_庫存.BorderRadius = 10;
                    rJ_Lable_庫存.BorderSize = 0;
                    rJ_Lable_庫存.Dock = System.Windows.Forms.DockStyle.Fill;
                    rJ_Lable_庫存.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    rJ_Lable_庫存.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    rJ_Lable_庫存.ForeColor = System.Drawing.Color.Transparent;
                    rJ_Lable_庫存.GUID = "庫存";
                    rJ_Lable_庫存.Location = new System.Drawing.Point(5, 57);
                    rJ_Lable_庫存.ShadowColor = System.Drawing.Color.DimGray;
                    rJ_Lable_庫存.ShadowSize = 0;
                    rJ_Lable_庫存.Size = new System.Drawing.Size(177, 68);
                    rJ_Lable_庫存.Text = $"{medClasses_Ary[i].庫存}";
                    rJ_Lable_庫存.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    rJ_Lable_庫存.TextColor = System.Drawing.Color.Black;

                    rJ_Lable_台號.BackColor = Color.Transparent;
                    rJ_Lable_台號.BackgroundColor = color;
                    rJ_Lable_台號.BorderColor = System.Drawing.Color.PaleVioletRed;
                    rJ_Lable_台號.BorderRadius = 10;
                    rJ_Lable_台號.BorderSize = 0;
                    rJ_Lable_台號.Dock = System.Windows.Forms.DockStyle.Top;
                    rJ_Lable_台號.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    rJ_Lable_台號.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    rJ_Lable_台號.ForeColor = System.Drawing.Color.Transparent;
                    rJ_Lable_台號.GUID = "台號";
                    rJ_Lable_台號.Location = new System.Drawing.Point(5, 5);
                    rJ_Lable_台號.ShadowColor = System.Drawing.Color.DimGray;
                    rJ_Lable_台號.ShadowSize = 0;
                    rJ_Lable_台號.Size = new System.Drawing.Size(177, 52);
                    rJ_Lable_台號.Text = $"{medClasses_Ary[i].ServerName}";
                    rJ_Lable_台號.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    rJ_Lable_台號.TextColor = System.Drawing.Color.White;

                    rJ_Pannel.Click += RJ_Pannel_Click;
                    rJ_Lable_台號.Click += RJ_Pannel_Click;
                    rJ_Lable_庫存.Click += RJ_Pannel_Click;
                    this.flowLayoutPanel_調劑台選擇.Controls.Add(rJ_Pannel);

                    rJ_Pannels.Add(rJ_Pannel);
                }
            }));

        }

        private void sub_program()
        {
            string[] text = Main_Form.Function_ReadBacodeScanner();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != null)
                {
                    LoadingForm.ShowLoadingForm();
                    Function_SerchByBarCode(text[i]);
                    LoadingForm.CloseLoadingForm();
                    break;
                }
            }
        }
        #region Event
        private void RJ_Pannel_Click(object sender, EventArgs e)
        {
            if (sender is RJ_Lable)
            {
                sender = ((RJ_Lable)sender).Parent;
            }
            if (sender is RJ_Pannel)
            {
                RJ_Pannel rJ_Pannel = (RJ_Pannel)sender;
                for (int i = 0; i < rJ_Pannels.Count; i++)
                {
                    Color color = new Color();
                    rJ_Pannels[i].IsSelected = (rJ_Pannels[i].GUID == rJ_Pannel.GUID);
                    color = (rJ_Pannels[i].IsSelected) ? Color.Blue : Color.DimGray;
                    rJ_Pannels[i].BorderColor = color;
                    rJ_Pannels[i].ShadowColor = color;

                    for (int k = 0; k < rJ_Pannels[i].Controls.Count; k++)
                    {
                        if (rJ_Pannels[i].Controls[k] is RJ_Lable)
                        {
                            RJ_Lable rJ_Lable = ((RJ_Lable)rJ_Pannels[i].Controls[k]);
                            if (rJ_Lable.GUID == "台號")
                            {
                                rJ_Lable.BackgroundColor = color;
                                if (rJ_Pannels[i].IsSelected)
                                {

                                    double 庫存 = Function_取得選取台號庫存(rJ_Lable);
                                    double 調出量 = 0;
                                    if (庫存 <= 0)
                                    {
                                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("無庫存可調入", 1000);
                                        dialog_AlarmForm.ShowDialog();

                                        rJ_Pannels[i].IsSelected = false;
                                        color = (rJ_Pannels[i].IsSelected) ? Color.Blue : Color.DimGray;
                                        rJ_Pannels[i].BorderColor = color;
                                        rJ_Pannels[i].ShadowColor = color;
                                        rJ_Lable.BackgroundColor = color;
                                        rJ_Lable_State.Text = $"請選擇調出台";
                                        continue;
                                    }

                                    this.Selected_ServerName = rJ_Lable.Text;
                                    rJ_Lable_State.Text = $"已選擇【{this.Selected_ServerName}】>>【{Main_Form.ServerName}】請輸入數量";
                                    Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                                    dialog_NumPannel.Set_Location_Offset(700, 0);
                                    if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
                                    if(dialog_NumPannel.Value > 庫存)
                                    {
                                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("調出庫存不足", 1000);
                                        dialog_AlarmForm.ShowDialog();
                                        return;
                                    }
                                    調出量 = dialog_NumPannel.Value;
                                    drugDispatchClass drugDispatchClass = new drugDispatchClass();
                                    drugDispatchClass.GUID = Guid.NewGuid().ToString();
                                    drugDispatchClass.動作類別 = enum_交易記錄查詢動作.調入作業.GetEnumName();
                                    drugDispatchClass.出庫庫別 = this.Selected_ServerName;
                                    drugDispatchClass.出庫庫存 = 庫存.ToString();
                                    drugDispatchClass.出庫量 = 調出量.ToString();
                                    drugDispatchClass.入庫庫存 = medClasses_local[0].庫存;                                 
                                    drugDispatchClass.入庫庫別 = Main_Form.ServerName;
                                    drugDispatchClass.入庫人員 = Main_Form.LoginUsers[0];
                                    drugDispatchClass.藥名 = this.藥名;
                                    drugDispatchClass.藥碼 = this.藥碼;
                                    drugDispatchClass.單位 = this.單位;
                                    object[] value = drugDispatchClass.ClassToSQL<drugDispatchClass, enum_drugDispatch>();
                                    sqL_DataGridView_已選藥品.AddRow(value , true);
                                    ClearUI();
                                }
                            }
                        }
                    }
     
             
                    rJ_Pannels[i].Refresh();
                }
            }

        }
        private void Dialog_藥品調入_Load(object sender, EventArgs e)
        {
            this.flowLayoutPanel_調劑台選擇.Controls.Clear();
            this.flowLayoutPanel_調劑台選擇.Refresh();
        }
        private void Dialog_藥品調入_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyThread_program.Stop();
            MyThread_program.Abort();
            MyThread_program = null;
        }
        private void Dialog_藥品調入_LoadFinishedEvent(EventArgs e)
        {

            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();
       
        }
        private void RJ_Button_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_藥品搜尋 dialog_藥品搜尋 = new Dialog_藥品搜尋();
            dialog_藥品搜尋.IsDeviceSerch = true;
            if (dialog_藥品搜尋.ShowDialog() != DialogResult.Yes) return;
            medClass medClass = dialog_藥品搜尋.Value;
            Function_SerchByBarCode(medClass.藥品碼);
        }
        private void RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_已選藥品.GetAllRows();
            Dialog_AlarmForm dialog_AlarmForm;
            if (list_value.Count == 0)
            {
                dialog_AlarmForm = new Dialog_AlarmForm("無資料可送出", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            LoadingForm.ShowLoadingForm();

            List<drugDispatchClass> drugDispatchClasses = list_value.SQLToClass<drugDispatchClass, enum_drugDispatch>();

            drugDispatchClass.datas_posting(Main_Form.API_Server, drugDispatchClasses);


            LoadingForm.CloseLoadingForm();
            dialog_AlarmForm = new Dialog_AlarmForm("調入完成", 1500, Color.Green);
            dialog_AlarmForm.ShowDialog();
            this.Close();
        }
        private void RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.Close();
            }));
        }
        #endregion

    }
}
