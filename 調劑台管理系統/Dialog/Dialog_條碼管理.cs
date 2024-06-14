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
namespace 調劑台管理系統
{
    public partial class Dialog_條碼管理 : MyDialog
    {
        public static bool IsShown = false;
        private MyThread MyThread_program;
        public medClass Value = null;
        private string 藥品碼 = "";
        public Dialog_條碼管理(string 藥品碼)
        {
            InitializeComponent();
            this.Load += Dialog_條碼管理_Load;
            this.FormClosed += Dialog_條碼管理_FormClosed;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_輸入.MouseDownEvent += RJ_Button_輸入_MouseDownEvent;
            this.rJ_TextBox_BarCode.KeyPress += RJ_TextBox_BarCode_KeyPress;
            this.藥品碼 = 藥品碼;
        }

        private void Function_Refresh()
        {
            this.Invoke(new Action(delegate
            {
                this.panel_Content.SuspendLayout();
                this.panel_Content.Controls.Clear();
                int y_temp = 0;
                for (int i = 0; i < Value.Barcode.Count; i++)
                {
                    RJ_Pannel rJ_Pannel = new RJ_Pannel();
                    rJ_Pannel.GUID = Value.Barcode[i];
                    rJ_Pannel.BackColor = System.Drawing.Color.White;
                    rJ_Pannel.BorderColor = System.Drawing.Color.SkyBlue;
                    rJ_Pannel.BorderRadius = 0;
                    rJ_Pannel.BorderSize = 10;
                    rJ_Pannel.Dock = System.Windows.Forms.DockStyle.None;
                    rJ_Pannel.ForeColor = System.Drawing.Color.White;
                    rJ_Pannel.IsSelected = false;
                    rJ_Pannel.Padding = new System.Windows.Forms.Padding(1);
                    rJ_Pannel.Size = new System.Drawing.Size(633, 60);
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
                    rJ_Lable.GUID = Value.Barcode[i];
                    rJ_Lable.Location = new System.Drawing.Point(1, 1);
                    rJ_Lable.Size = new System.Drawing.Size(520, 58);
                    rJ_Lable.TabIndex = i;
                    rJ_Lable.Text = $" {i + 1}. {Value.Barcode[i]}";
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
                    rJ_Button.GUID = Value.Barcode[i];
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
                    this.panel_Content.Controls.Add(rJ_Pannel);
                }


                this.panel_Content.ResumeLayout(false);
            }));
        }

        #region Event
        private void Dialog_條碼管理_FormClosed(object sender, FormClosedEventArgs e)
        {
           if(MyThread_program != null)
            {
                MyThread_program.Abort();
                MyThread_program = null;
            }
            IsShown = false;
        }
        private void Dialog_條碼管理_Load(object sender, EventArgs e)
        {
            IsShown = true;
            returnData returnData = new returnData($"{Main_Form.API_Server}/api/MED_page/get_by_code");
            returnData.ServerName = Main_Form.ServerName;
            returnData.ServerType = Main_Form.ServerType;
            returnData.TableName = "medicine_page_cloud";
            returnData.Value = 藥品碼;
            string json = returnData.ApiPostJson();
            List<medClass> medClasses = returnData.ResultData.Data.ObjToListClass<medClass>();
            if (medClasses.Count == 0)
            {
                MyMessageBox.ShowDialog($"找無此藥品碼! [{藥品碼}]");
                RJ_Button_確認_MouseDownEvent(null);
                return;
            }
            Value = medClasses[0];
            this.rJ_Lable_藥碼.Text = $" 藥碼 : {Value.藥品碼}";
            this.rJ_Lable_藥名.Text = $" 藥名 : {Value.藥品名稱}";
            Function_Refresh();

            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();
        }
        private void RJ_Button_MouseDownEventEx(RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否刪除國際條碼?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            Value.Delete_BarCode(rJ_Button.GUID);
            returnData returnData = new returnData($"{Main_Form.API_Server}/api/MED_page/upadte_by_guid");
            returnData.ServerName = Main_Form.ServerName;
            returnData.ServerType = Main_Form.ServerType;
            returnData.TableName = "medicine_page_cloud";
            returnData.Data = Value;
            string json = returnData.ApiPostJson();
            Function_Refresh();
        }
        private void RJ_TextBox_BarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) RJ_Button_輸入_MouseDownEvent(null);
        }
        private void RJ_Button_輸入_MouseDownEvent(MouseEventArgs mevent)
        {
            returnData returnData = new returnData();
            string json = "";
            string BarCode = rJ_TextBox_BarCode.Texts;
            if(BarCode.StringIsEmpty())
            {
                MyMessageBox.ShowDialog($"國際條碼 空白!");
                return;
            }
      
            List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, BarCode);
            if (medClasses.Count > 0)
            {
                MyMessageBox.ShowDialog("此國際條碼已被使用,請刪除使用藥品國際條碼再建立!");
                return;
            }
            Value.Add_BarCode(BarCode);
            returnData = new returnData($"{Main_Form.API_Server}/api/MED_page/upadte_by_guid");
            returnData.ServerName = Main_Form.ServerName;
            returnData.ServerType = Main_Form.ServerType;
            returnData.TableName = "medicine_page_cloud";
            returnData.Data = Value;
            json = returnData.ApiPostJson();
            if(returnData.ResultData.Code != 200)
            {
                MyMessageBox.ShowDialog(returnData.ResultData.Result);
                return;
            }
            RJ_Button_確認_MouseDownEvent(null);
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.Close();
            }));
        }
        private void sub_program()
        {
            string text01 = Main_Form.Function_ReadBacodeScanner01();
            string text02 = Main_Form.Function_ReadBacodeScanner02();
            if (text01 != null)
            {
                this.Invoke(new Action(delegate 
                {
                    rJ_TextBox_BarCode.Texts = text01;
                    RJ_Button_輸入_MouseDownEvent(null);
                }));
            }
            else if (text02 != null)
            {
                this.Invoke(new Action(delegate
                {
                    rJ_TextBox_BarCode.Texts = text02;
                    RJ_Button_輸入_MouseDownEvent(null);
                }));
            }
        }
        #endregion
    }
}
