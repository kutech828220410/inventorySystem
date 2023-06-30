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
using HIS_DB_Lib;
namespace 調劑台管理系統
{
    public partial class Dialog_收支原因設定 : Form
    {

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
        public string Value = "";
        private MyThread MyThread_program;
        private List<RJ_Pannel> rJ_Pannels = new List<RJ_Pannel>();
        private string ServerName;
        private string ApiURL = "";



        public Dialog_收支原因設定(string ApiURL , string ServerName)
        {
            InitializeComponent();
            this.ApiURL = ApiURL;
            this.ServerName = ServerName;
        }

        private void Dialog_收支原因設定_Load(object sender, EventArgs e)
        {
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;


            rJ_Button_新增.MouseDownEvent += RJ_Button_新增_MouseDownEvent;
            rJ_Button_刷新.MouseDownEvent += RJ_Button_刷新_MouseDownEvent;
            RJ_Button_刷新_MouseDownEvent(null);

        }



       

        #region Event
        private void RJ_Button_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_收支原因.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("收支原因不得空白!");
            }
            List<IncomeReasonsClass> list_IncomeReasonsClass = new List<IncomeReasonsClass>();
            IncomeReasonsClass incomeReasonsClass = new IncomeReasonsClass();
            incomeReasonsClass.原因 = rJ_TextBox_收支原因.Text;
            list_IncomeReasonsClass.Add(incomeReasonsClass);
            returnData returnData = new returnData();
            returnData.Data = list_IncomeReasonsClass;
            returnData.ServerName = ServerName;
            returnData.ServerType = "調劑台";
            string json_in = returnData.JsonSerializationt();
            string json_result = Net.WEBApiPostJson($"{ApiURL}/add", json_in);

            RJ_Button_刷新_MouseDownEvent(null);
        }
        private void RJ_Button_刷新_MouseDownEvent(MouseEventArgs mevent)
        {
            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = "調劑台";
            string json_in = returnData.JsonSerializationt();
            string json_result = Net.WEBApiPostJson($"{ApiURL}/data", json_in);
            returnData = json_result.JsonDeserializet<returnData>();
            if (returnData.Code != 200)
            {
                MyMessageBox.ShowDialog(returnData.Result);
                return;
            }
            List<IncomeReasonsClass> list_IncomeReasonsClass = returnData.Data.ObjToListClass<IncomeReasonsClass>();
            this.Invoke(new Action(delegate
            {
                this.panel_收支原因.SuspendLayout();
                this.panel_收支原因.Controls.Clear();
                int y_temp = 0;
                for (int i = 0; i < list_IncomeReasonsClass.Count; i++)
                {
                    RJ_Pannel rJ_Pannel = new RJ_Pannel();
                    rJ_Pannel.GUID = list_IncomeReasonsClass[i].GUID;
                    rJ_Pannel.BackColor = System.Drawing.Color.White;
                    rJ_Pannel.BorderColor = System.Drawing.Color.SkyBlue;
                    rJ_Pannel.BorderRadius = 0;
                    rJ_Pannel.BorderSize = 10;
                    rJ_Pannel.Dock = System.Windows.Forms.DockStyle.None;
                    rJ_Pannel.ForeColor = System.Drawing.Color.White;
                    rJ_Pannel.IsSelected = false;
                    rJ_Pannel.Padding = new System.Windows.Forms.Padding(1);
                    rJ_Pannel.Size = new System.Drawing.Size(715, 60);
                    rJ_Pannel.Location = new Point(0, y_temp);
                    y_temp += rJ_Pannel.Size.Height;
                    rJ_Pannel.TabIndex = i;

                    RJ_Lable rJ_Lable = new RJ_Lable();
                    rJ_Lable.BackColor = System.Drawing.Color.DimGray;
                    rJ_Lable.BackgroundColor = System.Drawing.Color.DimGray;
                    rJ_Lable.BorderColor = System.Drawing.Color.PaleVioletRed;
                    rJ_Lable.BorderRadius = 3;
                    rJ_Lable.BorderSize = 0;
                    rJ_Lable.Dock = System.Windows.Forms.DockStyle.Left;
                    rJ_Lable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    rJ_Lable.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    rJ_Lable.ForeColor = System.Drawing.Color.White;
                    rJ_Lable.GUID = list_IncomeReasonsClass[i].GUID;
                    rJ_Lable.Location = new System.Drawing.Point(1, 1);
                    rJ_Lable.Size = new System.Drawing.Size(645, 58);
                    rJ_Lable.TabIndex = i;
                    rJ_Lable.Text = $" {i + 1}. {list_IncomeReasonsClass[i].原因}";
                    rJ_Lable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    rJ_Lable.TextColor = System.Drawing.Color.White;
                   

                    RJ_Button rJ_Button = new RJ_Button();

          
                    rJ_Button.AutoResetState = false;
                    rJ_Button.BackColor = System.Drawing.Color.DarkRed;
                    rJ_Button.BorderColor = System.Drawing.Color.PaleVioletRed;
                    rJ_Button.BorderRadius = 3;
                    rJ_Button.BorderSize = 0;
                    rJ_Button.buttonType = MyUI.RJ_Button.ButtonType.Push;
                    rJ_Button.Dock = System.Windows.Forms.DockStyle.Fill;
                    rJ_Button.FlatAppearance.BorderSize = 0;
                    rJ_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    rJ_Button.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    rJ_Button.ForeColor = System.Drawing.Color.White;
                    rJ_Button.GUID = list_IncomeReasonsClass[i].GUID;
                    //rJ_Button.Location = new System.Drawing.Point(646, 1);
                    //rJ_Button.Size = new System.Drawing.Size(68, 58);
                    rJ_Button.State = false;
                    rJ_Button.TabIndex = i;
                    rJ_Button.TextColor = System.Drawing.Color.White;
                    rJ_Button.Text = "刪除";
                    rJ_Button.UseVisualStyleBackColor = false;
                    rJ_Button.MouseDownEventEx += RJ_Button_MouseDownEventEx;

                    rJ_Pannel.Controls.Add(rJ_Button);
                    rJ_Pannel.Controls.Add(rJ_Lable);
                    this.panel_收支原因.Controls.Add(rJ_Pannel);
                }


                this.panel_收支原因.ResumeLayout(false);
            }));
        }

        private void RJ_Button_MouseDownEventEx(RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("確認刪除選取資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                      
            for (int i = 0; i < this.panel_收支原因.Controls.Count; i++)
            {
                if(this.panel_收支原因.Controls[i] is RJ_Pannel)
                {
                    if (rJ_Button.GUID == ((RJ_Pannel)this.panel_收支原因.Controls[i]).GUID)
                    {
                        List<IncomeReasonsClass> list_IncomeReasonsClass = new List<IncomeReasonsClass>();
                        IncomeReasonsClass incomeReasonsClass = new IncomeReasonsClass();
                        incomeReasonsClass.GUID = rJ_Button.GUID;
                        list_IncomeReasonsClass.Add(incomeReasonsClass);
                        returnData returnData = new returnData();
                        returnData.Data = list_IncomeReasonsClass;
                        returnData.ServerName = ServerName;
                        returnData.ServerType = "調劑台";
                        string json_in = returnData.JsonSerializationt();
                        string json_result = Net.WEBApiPostJson($"{ApiURL}/delete", json_in);
                        break;
                    }
                }         
            }
            RJ_Button_刷新_MouseDownEvent(null);
        }


        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Close();

            }));
        }

        #endregion
    }
}
