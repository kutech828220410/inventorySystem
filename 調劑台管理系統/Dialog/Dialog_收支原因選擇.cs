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
    public partial class Dialog_收支原因選擇 : MyDialog
    {
        public string Value = "";
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
        private MyThread MyThread_program;
        private List<RJ_Pannel> rJ_Pannels = new List<RJ_Pannel>();

        private string title = "";
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }
        public Dialog_收支原因選擇()
        {
            InitializeComponent();

        }
        private void Dialog_收支原因選擇_Load(object sender, EventArgs e)
        {
            RJ_Button_刷新_MouseDownEvent(null);
            if (Title.StringIsEmpty() == false)
            {
                this.Text = this.title;
            }
            this.rJ_Button_確認輸入左側原因.MouseDownEvent += RJ_Button_確認輸入左側原因_MouseDownEvent;
        }

        #region Event
        private void RJ_Button_確認輸入左側原因_MouseDownEvent(MouseEventArgs mevent)
        {
            Value = rJ_TextBox_原因.Texts;
            if(Value.Length > 100)
            {
                MyMessageBox.ShowDialog("字數限制100字元,請刪減原因字數!");
                return;
            }
            this.Invoke(new Action(delegate
            {
                DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        private void RJ_Button_刷新_MouseDownEvent(MouseEventArgs mevent)
        {

            List<incomeReasonsClass> list_incomeReasonsClass = incomeReasonsClass.get_all(Main_Form.API_Server);
            this.Invoke(new Action(delegate
            {
                this.panel_收支原因.SuspendLayout();
                this.panel_收支原因.Controls.Clear();
                RJ_Button rJ_Button;
                RJ_Pannel rJ_Pannel;
                int y_temp = 0;
                for (int i = 0; i < list_incomeReasonsClass.Count; i++)
                {
                    rJ_Pannel = new RJ_Pannel();
                    rJ_Pannel.GUID = list_incomeReasonsClass[i].GUID;
                    rJ_Pannel.BackColor = System.Drawing.Color.White;
                    rJ_Pannel.BorderColor = System.Drawing.Color.White;
                    rJ_Pannel.BorderRadius = 0;
                    rJ_Pannel.BorderSize = 10;
                    rJ_Pannel.Dock = System.Windows.Forms.DockStyle.None;
                    rJ_Pannel.ForeColor = System.Drawing.Color.White;
                    rJ_Pannel.IsSelected = false;
                    rJ_Pannel.Padding = new System.Windows.Forms.Padding(3);
                    rJ_Pannel.Size = new System.Drawing.Size(715, 60);
                    rJ_Pannel.Location = new Point(0, y_temp);
                    y_temp += rJ_Pannel.Size.Height;
                    rJ_Pannel.TabIndex = i;

  
                   

                    rJ_Button = new RJ_Button();
                    rJ_Button.TextAlign = ContentAlignment.MiddleLeft;
                    rJ_Button.AutoResetState = false;
                    rJ_Button.BackColor = System.Drawing.Color.White;
                    rJ_Button.BackgroundColor = Color.DimGray;
                    rJ_Button.BorderColor = System.Drawing.Color.PaleVioletRed;
                    rJ_Button.BorderRadius = 20;
                    rJ_Button.BorderSize = 0;
                    rJ_Button.buttonType = MyUI.RJ_Button.ButtonType.Push;
                    rJ_Button.Dock = System.Windows.Forms.DockStyle.Fill;
                    rJ_Button.FlatAppearance.BorderSize = 0;
                    rJ_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    rJ_Button.Font = new System.Drawing.Font("微軟正黑體", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    rJ_Button.ForeColor = System.Drawing.Color.White;
                    rJ_Button.GUID = list_incomeReasonsClass[i].GUID;
                    rJ_Button.Name = $"{list_incomeReasonsClass[i].原因}";
                    rJ_Button.State = false;
                    rJ_Button.TabIndex = i;
                    rJ_Button.TextColor = System.Drawing.Color.White;
                    rJ_Button.Text = $" ({i + 1}). {list_incomeReasonsClass[i].原因}";
                    rJ_Button.UseVisualStyleBackColor = false;
                    rJ_Button.MouseDownEventEx += RJ_Button_MouseDownEventEx;

                    rJ_Pannel.Controls.Add(rJ_Button);
                    this.panel_收支原因.Controls.Add(rJ_Pannel);
                }


                rJ_Pannel = new RJ_Pannel();
                rJ_Pannel.GUID = "";
                rJ_Pannel.BackColor = System.Drawing.Color.White;
                rJ_Pannel.BorderColor = System.Drawing.Color.White;
                rJ_Pannel.BorderRadius = 0;
                rJ_Pannel.BorderSize = 10;
                rJ_Pannel.Dock = System.Windows.Forms.DockStyle.None;
                rJ_Pannel.ForeColor = System.Drawing.Color.White;
                rJ_Pannel.IsSelected = false;
                rJ_Pannel.Padding = new System.Windows.Forms.Padding(3);
                rJ_Pannel.Size = new System.Drawing.Size(715, 60);
                rJ_Pannel.Location = new Point(0, y_temp);
                y_temp += rJ_Pannel.Size.Height;
                rJ_Pannel.TabIndex = 99;


                rJ_Button = new RJ_Button();
                rJ_Button.AutoResetState = false;
                rJ_Button.BackColor = System.Drawing.Color.White;
                rJ_Button.BackgroundColor = Color.DimGray;
                rJ_Button.BorderColor = System.Drawing.Color.PaleVioletRed;
                rJ_Button.BorderRadius = 20;
                rJ_Button.BorderSize = 0;
                rJ_Button.buttonType = MyUI.RJ_Button.ButtonType.Push;
                rJ_Button.Dock = System.Windows.Forms.DockStyle.Fill;
                rJ_Button.FlatAppearance.BorderSize = 0;
                rJ_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                rJ_Button.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                rJ_Button.ForeColor = System.Drawing.Color.White;
                rJ_Button.GUID = "";
                rJ_Button.Name = "其他";
                //rJ_Button.Location = new System.Drawing.Point(646, 1);
                //rJ_Button.Size = new System.Drawing.Size(68, 58);
                rJ_Button.State = false;
                rJ_Button.TabIndex = 99;
                rJ_Button.TextAlign = ContentAlignment.MiddleLeft;
                rJ_Button.TextColor = System.Drawing.Color.White;
                rJ_Button.Text = $" ({list_incomeReasonsClass.Count + 1}). 其他";
                rJ_Button.UseVisualStyleBackColor = false;
                rJ_Button.MouseDownEventEx += RJ_Button_MouseDownEventEx;

                rJ_Pannel.Controls.Add(rJ_Button);
                this.panel_收支原因.Controls.Add(rJ_Pannel);

                this.panel_收支原因.ResumeLayout(false);
            }));
        }
        private void RJ_Button_MouseDownEventEx(RJ_Button rJ_Button, MouseEventArgs mevent)
        {

            Value = rJ_Button.Name;
            this.Invoke(new Action(delegate
            {
                DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        #endregion
    }
}
