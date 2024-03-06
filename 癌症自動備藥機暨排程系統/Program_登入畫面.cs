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
using DeltaMotor485;
using DrawingClass;
using H_Pannel_lib;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        static public string 登入者名稱
        {
            get
            {
                if (SessionClass_登入畫面 != null) return SessionClass_登入畫面.Name;
                return "";
            }
            
        }
        static public sessionClass SessionClass_登入畫面;
        public sessionClass sessionClass_登入畫面
        {
            get
            {
                return SessionClass_登入畫面;
            }
            set
            {

                SessionClass_登入畫面 = value;
                this.Invoke(new Action(delegate
                {
                    if (SessionClass_登入畫面 != null)
                    {
                        rJ_Lable_使用者ID.Text = SessionClass_登入畫面.ID;
                        rJ_Lable_使用者姓名.Text = SessionClass_登入畫面.Name;
                    }
                    else
                    {
                        rJ_Lable_使用者ID.Text = "------------";
                        rJ_Lable_使用者姓名.Text = "------------";
                    }
                }));
            }
        }
        public PLC_Device PLC_Device_登入畫面_已登入 = new PLC_Device("S4000");
        public PLC_Device PLC_Device_登入畫面_未登入 = new PLC_Device("S4001");
        private void Program_登入畫面_Init()
        {
            plC_RJ_Button_登入畫面_登入.MouseClickEvent += PlC_RJ_Button_登入畫面_登入_MouseClickEvent;
            plC_RJ_Button_登出.MouseDownEvent += PlC_RJ_Button_登出_MouseDownEvent;
            textBox_登入畫面_帳號.KeyPress += TextBox_登入畫面_帳號_KeyPress;
            textBox_登入畫面_密碼.KeyPress += TextBox_登入畫面_密碼_KeyPress;
            panel_登入畫面.Location = new Point((this.Width - panel_登入畫面.Width) / 2, (this.Height - panel_登入畫面.Height) / 2);
            panel_登入畫面.Visible = true;
            this.plC_UI_Init.Add_Method(Program_登入畫面);
        }

    

        private void Program_登入畫面()
        {
            PLC_Device_登入畫面_未登入.Bool = (!PLC_Device_登入畫面_已登入.Bool);
            sub_Program_登入畫面_RFID登入();
        }
        #region PLC_登入畫面_RFID登入
        PLC_Device PLC_Device_登入畫面_RFID登入 = new PLC_Device("");
        int cnt_Program_登入畫面_RFID登入 = 65534;
        void sub_Program_登入畫面_RFID登入()
        {
            if (this.plC_ScreenPage_main.PageText == "登入畫面")
            {
                PLC_Device_登入畫面_RFID登入.Bool = true;
            }
            else
            {
                PLC_Device_登入畫面_RFID登入.Bool = false;
            }
            if (cnt_Program_登入畫面_RFID登入 == 65534)
            {
                PLC_Device_登入畫面_RFID登入.SetComment("PLC_登入畫面_RFID登入");
                PLC_Device_登入畫面_RFID登入.Bool = false;
                cnt_Program_登入畫面_RFID登入 = 65535;
            }
            if (cnt_Program_登入畫面_RFID登入 == 65535) cnt_Program_登入畫面_RFID登入 = 1;
            if (cnt_Program_登入畫面_RFID登入 == 1) cnt_Program_登入畫面_RFID登入_檢查按下(ref cnt_Program_登入畫面_RFID登入);
            if (cnt_Program_登入畫面_RFID登入 == 2) cnt_Program_登入畫面_RFID登入_初始化(ref cnt_Program_登入畫面_RFID登入);
            if (cnt_Program_登入畫面_RFID登入 == 3) cnt_Program_登入畫面_RFID登入_檢查權限登入(ref cnt_Program_登入畫面_RFID登入);
            if (cnt_Program_登入畫面_RFID登入 == 4) cnt_Program_登入畫面_RFID登入_外部設備資料(ref cnt_Program_登入畫面_RFID登入);
            if (cnt_Program_登入畫面_RFID登入 == 5) cnt_Program_登入畫面_RFID登入_開始登入(ref cnt_Program_登入畫面_RFID登入);
            if (cnt_Program_登入畫面_RFID登入 == 6) cnt_Program_登入畫面_RFID登入_等待登入完成(ref cnt_Program_登入畫面_RFID登入);
            if (cnt_Program_登入畫面_RFID登入 == 7) cnt_Program_登入畫面_RFID登入 = 65500;
            if (cnt_Program_登入畫面_RFID登入 > 1) cnt_Program_登入畫面_RFID登入_檢查放開(ref cnt_Program_登入畫面_RFID登入);

            if (cnt_Program_登入畫面_RFID登入 == 65500)
            {
                PLC_Device_登入畫面_RFID登入.Bool = false;
                cnt_Program_登入畫面_RFID登入 = 65535;
            }
        }
        void cnt_Program_登入畫面_RFID登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_登入畫面_RFID登入.Bool) cnt++;
        }
        void cnt_Program_登入畫面_RFID登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_登入畫面_RFID登入.Bool) cnt = 65500;
        }
        void cnt_Program_登入畫面_RFID登入_初始化(ref int cnt)
        {

            cnt++;
        }
        void cnt_Program_登入畫面_RFID登入_檢查權限登入(ref int cnt)
        {
            if (!this.PLC_Device_登入畫面_已登入.Bool)
            {
                cnt++;
                return;
            }
            else
            {
                cnt = 65500;
                return;
            }

        }
        void cnt_Program_登入畫面_RFID登入_外部設備資料(ref int cnt)
        {
            string RFID = "0";
            List<RFID_FX600lib.RFID_FX600_UI.RFID_Device> list_RFID = this.rfiD_FX600_UI.Get_RFID();


            if (list_RFID.Count != 0)
            {
                if (list_RFID[0].UID.StringToInt32() != 0)
                {
                    RFID = list_RFID[0].UID;
                }
            }
            if (RFID.StringToInt32() == 0 || RFID.StringIsEmpty())
            {
                cnt = 65500;
                return;
            }

            List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.卡號.GetEnumName(), RFID, false);
            if (list_人員資料.Count > 0)
            {
                this.Invoke(new Action(delegate
                {
                    this.textBox_登入畫面_帳號.Text = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                    this.textBox_登入畫面_密碼.Text = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                }));
                Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, list_人員資料[0][(int)enum_人員資料.姓名].ObjectToString(), "登入畫面");
            }
            else
            {
                MyMessageBox.ShowDialog(string.Format("查無此卡帳號! {0}", RFID));
                cnt = 65500;
                return;
            }

            cnt++;
        }
        void cnt_Program_登入畫面_RFID登入_開始登入(ref int cnt)
        {
            Function_登入畫面_登入();
            cnt++;
        }
        void cnt_Program_登入畫面_RFID登入_等待登入完成(ref int cnt)
        {
            cnt++;
        }
        #endregion
        #region Function
        private void Function_登入畫面_登入()
        {
            string user = textBox_登入畫面_帳號.Text;
            string pwd = textBox_登入畫面_密碼.Text;
            this.Invoke(new Action(delegate
            {
                textBox_登入畫面_帳號.Text = "";
                textBox_登入畫面_密碼.Text = "";

            }));

            if (user.StringIsEmpty() == true)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("帳號空白", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            bool flag = true;
            string json_in = "";
            string json_result = "";
            returnData returnData = new returnData();
            sessionClass _sessionClass = new sessionClass();
            _sessionClass.ID = user.ToUpper();
            _sessionClass.Password = pwd.ToUpper();
            returnData.Data = _sessionClass;
            json_in = returnData.JsonSerializationt();
            json_result = Net.WEBApiPostJson(dBConfigClass.Login_URL, json_in);
            returnData = json_result.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("登入API呼叫異常,請檢查網路連結及設定", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            sessionClass_登入畫面 = returnData.Data.ObjToClass<sessionClass>();
            if (returnData.Code == 200)
            {
                this.Invoke(new Action(delegate
                {
                    //this.plC_RJ_Button_登入畫面_登入.SetBackgroundColor(Color.DarkRed);
                    //this.plC_RJ_Button_登入畫面_登入.Texts = "登出";
                    this.rJ_Lable_登入畫面_登入狀態.TextColor = Color.White;
                    //this.rJ_Lable_登入畫面_登入狀態.Text = $"[{sessionClass_登入畫面.Name}] 已登入";
                    //this.rJ_Lable_登入畫面_登入狀態.BackgroundColor = Color.Green;
                    this.textBox_登入畫面_帳號.Enabled = false;
                    this.textBox_登入畫面_密碼.Enabled = false;
                    this.plC_RJ_Button_登入畫面_登入.Refresh();
                    Application.DoEvents();
                }));
                PLC_Device_登入畫面_已登入.Bool = true;
                Function_登入畫面_權限登入();
                //Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"登入成功 {sessionClass_登入畫面.Name}", 1500, Color.Green);
                //dialog_AlarmForm.ShowDialog();
                this.plC_ScreenPage_main.SelecteTabText("自動備藥");
                return;
            }
            else
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"{returnData.Result}", 1500);
                dialog_AlarmForm.ShowDialog();
                return;

            }
        }
        private void Function_登入畫面_登出()
        {
            sessionClass_登入畫面 = null;
            this.Invoke(new Action(delegate
            {
                this.plC_RJ_Button_登入畫面_登入.SetBackgroundColor(Color.Black);
                this.plC_RJ_Button_登入畫面_登入.Texts = "登入";
                this.rJ_Lable_登入畫面_登入狀態.TextColor = Color.Black;
                this.rJ_Lable_登入畫面_登入狀態.Text = $"請登入系統...";
                this.rJ_Lable_登入畫面_登入狀態.BackgroundColor = Color.White;
                this.textBox_登入畫面_帳號.Enabled = true;
                this.textBox_登入畫面_密碼.Enabled = true;
                this.plC_RJ_Button_登入畫面_登入.Refresh();
                Function_登入畫面_權限登出();
                Application.DoEvents();
            }));
            this.plC_ScreenPage_main.SelecteTabText("登入畫面");
     
            PLC_Device_登入畫面_已登入.Bool = false;
        }
        private void Function_登入畫面_權限登入()
        {
            this.Invoke(new Action(delegate 
            {
                plC_RJ_ScreenButtonEx_系統.Visible = true;
                plC_RJ_ScreenButtonEx_工程模式.Visible = true;
                plC_RJ_ScreenButtonEx_交易紀錄.Visible = true;
                plC_RJ_ScreenButtonEx_儲位設定.Visible = true;
                plC_RJ_ScreenButtonEx_人員資料.Visible = true;
                plC_RJ_ScreenButtonEx_出入庫作業.Visible = true;
                plC_RJ_ScreenButtonEx_調配排程.Visible = true;
                plC_RJ_ScreenButtonEx_自動備藥.Visible = true;

            }));
        }
        private void Function_登入畫面_權限登出()
        {
            this.Invoke(new Action(delegate
            {
                plC_RJ_ScreenButtonEx_自動備藥.Visible = false;
                plC_RJ_ScreenButtonEx_調配排程.Visible = false;
                plC_RJ_ScreenButtonEx_出入庫作業.Visible = false;
                plC_RJ_ScreenButtonEx_人員資料.Visible = false;
                plC_RJ_ScreenButtonEx_儲位設定.Visible = false;
                plC_RJ_ScreenButtonEx_交易紀錄.Visible = false;
                plC_RJ_ScreenButtonEx_工程模式.Visible = false;
                plC_RJ_ScreenButtonEx_系統.Visible = false;
            }));
        }
        #endregion
        #region Event
        private void PlC_RJ_Button_登出_MouseDownEvent(MouseEventArgs mevent)
        {
            Function_登入畫面_登出();
        }
        private void TextBox_登入畫面_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                Function_登入畫面_登入();
            }
        }
        private void TextBox_登入畫面_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                if (textBox_登入畫面_帳號.Text.StringIsEmpty() == false)
                {
                    textBox_登入畫面_密碼.Focus();
                }
            }

        }
        private void PlC_RJ_Button_登入畫面_登入_MouseClickEvent(MouseEventArgs mevent)
        {
            if (plC_RJ_Button_登入畫面_登入.Text == "登入")
            {
                this.Function_登入畫面_登入();
            }
            else
            {
                this.Function_登入畫面_登出();
            }
        }
        #endregion
    }
}
