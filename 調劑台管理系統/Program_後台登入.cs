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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
namespace 調劑台管理系統
{
    public partial class Form1 : Form
    {
        readonly private string Admin_ID = "admin";
        readonly private string Admoin_Password = "66437068";
        private bool flag_後台登入_頁面更新 = false;
        private MyTimer myTimer_登出計時 = new MyTimer();
        private PLC_Device PLC_Device_已登入 = new PLC_Device("S4000");

        private PLC_Device pLC_Device_最高權限 = new PLC_Device("S4077");
        private PLC_Device PLC_Device_最高權限
        {
            get
            {
                return this.pLC_Device_最高權限;
            }
            set
            {
                if (value.Bool)
                {
                    this.pannel_Locker_Design.ShowControlPannel = true;
                }
                else
                {
                    this.pannel_Locker_Design.ShowControlPannel = false;
                }
                this.pLC_Device_最高權限 = value;
            }
        }

        
        private string 登入者名稱
        {
            get
            {
                return this.rJ_TextBox_登入者姓名.Texts;
            }
            set
            {
                this.rJ_TextBox_登入者姓名.Texts = value;
                this.rJ_Lable_後台登入_歡迎登入_姓名.Text = value;
            }
        }
        private string 登入者ID
        {
            get
            {
                return this.rJ_TextBox_登入者ID.Texts;
            }
            set
            {
                this.rJ_TextBox_登入者ID.Texts = value;
                this.rJ_Lable_後台登入_歡迎登入_ID.Text = value;
            }
        }
        private string 登入者顏色
        {
            get
            {
                return this.rJ_TextBox_登入者顏色.Text;
            }
            set
            {
                Color color = value.ToColor();
                this.rJ_TextBox_登入者顏色.ForeColor = color;
                this.rJ_TextBox_登入者顏色.BackColor = color;
                this.rJ_TextBox_登入者顏色.Text = color.ToColorString();
            }
        }
        private string 登入者藥師證字號 = "";
        private List<PermissionsClass> 登入者權限 = new List<PermissionsClass>();
        
        private void Program_後台登入_Init()
        {
            plC_RJ_Button_後台登入_登入.MouseDownEvent += PlC_RJ_Button_後台登入_登入_MouseDownEvent;
            plC_RJ_Button_後台登入_登出.MouseDownEvent += PlC_RJ_Button_後台登入_登出_MouseDownEvent;
            button_後台網址_開啟.Click += Button_後台網址_開啟_Click;
            textBox_後台登入_帳號.KeyPress += TextBox_後台登入_帳號_KeyPress;
            textBox_後台登入_密碼.KeyPress += TextBox_後台登入_密碼_KeyPress;

            this.Function_登出();

            this.plC_UI_Init.Add_Method(this.sub_Program_後台登入);
            

        }

  

        private void sub_Program_後台登入()
        {
            if (this.plC_ScreenPage_Main.PageText == "後台登入")
            {
                if (!this.flag_後台登入_頁面更新)
                {
                    this.Invoke(new Action(delegate
                    {
                        this.textBox_後台登入_帳號.Text = "";
                        this.textBox_後台登入_密碼.Text = "";
                    }));
                    this.flag_後台登入_頁面更新 = true;
                }
            }
            else
            {
                this.flag_後台登入_頁面更新 = false;
            }

            if (!PLC_Device_已登入.Bool || !plC_CheckBox_後台閒置要自動登出.Bool)
            {
                this.myTimer_登出計時.TickStop();
                this.myTimer_登出計時.StartTickTime(600000);
            }
            else
            {
                if (this.myTimer_登出計時.IsTimeOut())
                {
                    Function_登出();
                }
            }
            rJ_ProgressBar_閒置登出時間.Maximum = 600000;
            if ((int)this.myTimer_登出計時.GetTickTime() < rJ_ProgressBar_閒置登出時間.Maximum)
            {
                rJ_ProgressBar_閒置登出時間.Value = (int)this.myTimer_登出計時.GetTickTime();
            }

            this.sub_Program_後台登入_RFID登入();
            this.sub_Program_後台登入_一維碼登入();


        }
        #region PLC_後台登入_RFID登入
        PLC_Device PLC_Device_後台登入_RFID登入 = new PLC_Device("");
        int cnt_Program_後台登入_RFID登入 = 65534;
        void sub_Program_後台登入_RFID登入()
        {
            if (this.plC_ScreenPage_Main.PageText == "後台登入")
            {
                PLC_Device_後台登入_RFID登入.Bool = true;
            }
            else
            {
                PLC_Device_後台登入_RFID登入.Bool = false;
            }
            if (cnt_Program_後台登入_RFID登入 == 65534)
            {
                PLC_Device_後台登入_RFID登入.SetComment("PLC_後台登入_RFID登入");
                PLC_Device_後台登入_RFID登入.Bool = false;
                cnt_Program_後台登入_RFID登入 = 65535;
            }
            if (cnt_Program_後台登入_RFID登入 == 65535) cnt_Program_後台登入_RFID登入 = 1;
            if (cnt_Program_後台登入_RFID登入 == 1) cnt_Program_後台登入_RFID登入_檢查按下(ref cnt_Program_後台登入_RFID登入);
            if (cnt_Program_後台登入_RFID登入 == 2) cnt_Program_後台登入_RFID登入_初始化(ref cnt_Program_後台登入_RFID登入);
            if (cnt_Program_後台登入_RFID登入 == 3) cnt_Program_後台登入_RFID登入_檢查權限登入(ref cnt_Program_後台登入_RFID登入);
            if (cnt_Program_後台登入_RFID登入 == 4) cnt_Program_後台登入_RFID登入_外部設備資料(ref cnt_Program_後台登入_RFID登入);
            if (cnt_Program_後台登入_RFID登入 == 5) cnt_Program_後台登入_RFID登入_開始登入(ref cnt_Program_後台登入_RFID登入);
            if (cnt_Program_後台登入_RFID登入 == 6) cnt_Program_後台登入_RFID登入_等待登入完成(ref cnt_Program_後台登入_RFID登入);
            if (cnt_Program_後台登入_RFID登入 == 7) cnt_Program_後台登入_RFID登入 = 65500;
            if (cnt_Program_後台登入_RFID登入 > 1) cnt_Program_後台登入_RFID登入_檢查放開(ref cnt_Program_後台登入_RFID登入);

            if (cnt_Program_後台登入_RFID登入 == 65500)
            {
                PLC_Device_後台登入_RFID登入.Bool = false;
                cnt_Program_後台登入_RFID登入 = 65535;
            }
        }
        void cnt_Program_後台登入_RFID登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_後台登入_RFID登入.Bool) cnt++;
        }
        void cnt_Program_後台登入_RFID登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_後台登入_RFID登入.Bool) cnt = 65500;
        }
        void cnt_Program_後台登入_RFID登入_初始化(ref int cnt)
        {

            cnt++;
        }
        void cnt_Program_後台登入_RFID登入_檢查權限登入(ref int cnt)
        {
            if (!this.PLC_Device_已登入.Bool)
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
        void cnt_Program_後台登入_RFID登入_外部設備資料(ref int cnt)
        {
            string RFID = "0";
            List<RFID_FX600lib.RFID_FX600_UI.RFID_Device> list_RFID = this.rfiD_FX600_UI.Get_RFID();

            for (int i = 0; i < List_RFID_本地資料.Count; i++)
            {
                for (int k = 0; k < List_RFID_本地資料[i].DeviceClasses.Length; k++)
                {
                    if (List_RFID_本地資料[i].DeviceClasses[k].Enable)
                    {
                        string RFID_buf = this.rfiD_UI.GetRFID(List_RFID_本地資料[i].IP, k);                       
                        if (RFID_buf.StringToInt32() != 0 && !RFID_buf.StringIsEmpty())
                        {
                            RFID = RFID_buf;
                        }

                    }
                }
            }
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
                    this.textBox_後台登入_帳號.Text = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                    this.textBox_後台登入_密碼.Text = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                }));
                Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.RFID登入, list_人員資料[0][(int)enum_人員資料.姓名].ObjectToString(), "後台登入");
            }
            else
            {
                MyMessageBox.ShowDialog(string.Format("查無此卡帳號! {0}", RFID));
                cnt = 65500;
                return;
            }

            cnt++;
        }
        void cnt_Program_後台登入_RFID登入_開始登入(ref int cnt)
        {
            Function_登入();
            cnt++;
        }
        void cnt_Program_後台登入_RFID登入_等待登入完成(ref int cnt)
        {
            cnt++;
        }
        #endregion
        #region PLC_後台登入_一維碼登入
        PLC_Device PLC_Device_後台登入_一維碼登入 = new PLC_Device("");
        int cnt_Program_後台登入_一維碼登入 = 65534;
        void sub_Program_後台登入_一維碼登入()
        {
            if (this.plC_ScreenPage_Main.PageText == "後台登入")
            {
                PLC_Device_後台登入_一維碼登入.Bool = true;
            }
            else
            {
                PLC_Device_後台登入_一維碼登入.Bool = false;
            }
            if (cnt_Program_後台登入_一維碼登入 == 65534)
            {
                PLC_Device_後台登入_一維碼登入.SetComment("PLC_後台登入_一維碼登入");
                PLC_Device_後台登入_一維碼登入.Bool = false;
                cnt_Program_後台登入_一維碼登入 = 65535;
            }
            if (cnt_Program_後台登入_一維碼登入 == 65535) cnt_Program_後台登入_一維碼登入 = 1;
            if (cnt_Program_後台登入_一維碼登入 == 1) cnt_Program_後台登入_一維碼登入_檢查按下(ref cnt_Program_後台登入_一維碼登入);
            if (cnt_Program_後台登入_一維碼登入 == 2) cnt_Program_後台登入_一維碼登入_初始化(ref cnt_Program_後台登入_一維碼登入);
            if (cnt_Program_後台登入_一維碼登入 == 3) cnt_Program_後台登入_一維碼登入_檢查權限登入(ref cnt_Program_後台登入_一維碼登入);
            if (cnt_Program_後台登入_一維碼登入 == 4) cnt_Program_後台登入_一維碼登入_讀取一維碼(ref cnt_Program_後台登入_一維碼登入);
            if (cnt_Program_後台登入_一維碼登入 == 5) cnt_Program_後台登入_一維碼登入_開始登入(ref cnt_Program_後台登入_一維碼登入);
            if (cnt_Program_後台登入_一維碼登入 == 6) cnt_Program_後台登入_一維碼登入_等待登入完成(ref cnt_Program_後台登入_一維碼登入);
            if (cnt_Program_後台登入_一維碼登入 == 7) cnt_Program_後台登入_一維碼登入 = 65500;
            if (cnt_Program_後台登入_一維碼登入 > 1) cnt_Program_後台登入_一維碼登入_檢查放開(ref cnt_Program_後台登入_一維碼登入);

            if (cnt_Program_後台登入_一維碼登入 == 65500)
            {
                PLC_Device_後台登入_一維碼登入.Bool = false;
                cnt_Program_後台登入_一維碼登入 = 65535;
            }
        }
        void cnt_Program_後台登入_一維碼登入_檢查按下(ref int cnt)
        {
            if (PLC_Device_後台登入_一維碼登入.Bool) cnt++;
        }
        void cnt_Program_後台登入_一維碼登入_檢查放開(ref int cnt)
        {
            if (!PLC_Device_後台登入_一維碼登入.Bool) cnt = 65500;
        }
        void cnt_Program_後台登入_一維碼登入_初始化(ref int cnt)
        {

            cnt++;
        }
        void cnt_Program_後台登入_一維碼登入_檢查權限登入(ref int cnt)
        {
            if (!this.PLC_Device_已登入.Bool)
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
        void cnt_Program_後台登入_一維碼登入_讀取一維碼(ref int cnt)
        {
            string 一維碼 = "";
            if (MySerialPort_Scanner01.ReadByte() != null)
            {
                System.Threading.Thread.Sleep(50);
                string text = MySerialPort_Scanner01.ReadString();
                MySerialPort_Scanner01.ClearReadByte();
                if (text == null) return;
                text = text.Replace("\0", "");
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r\n", "");
                一維碼 = text;
            }
            if (MySerialPort_Scanner02.ReadByte() != null)
            {
                System.Threading.Thread.Sleep(100);
                string text = MySerialPort_Scanner02.ReadString();
                MySerialPort_Scanner02.ClearReadByte();
                if (text == null) return;
                text = text.Replace("\0", "");
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r\n", "");
                一維碼 = text;
            }
            if (一維碼.StringIsEmpty())
            {
                cnt = 65500;
                return;
            }
            List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetRows(enum_人員資料.一維條碼.GetEnumName(), 一維碼, false);
            if (list_人員資料.Count > 0)
            {
                this.Invoke(new Action(delegate
                {
                    this.textBox_後台登入_帳號.Text = list_人員資料[0][(int)enum_人員資料.ID].ObjectToString();
                    this.textBox_後台登入_密碼.Text = list_人員資料[0][(int)enum_人員資料.密碼].ObjectToString();
                }));
                Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.一維碼登入, list_人員資料[0][(int)enum_人員資料.姓名].ObjectToString(), "後台登入");
            }
            else
            {
                MyMessageBox.ShowDialog(string.Format("查無此一維碼帳號! {0}", 一維碼));
                cnt = 65500;
                return;
            }

            cnt++;
        }
        void cnt_Program_後台登入_一維碼登入_開始登入(ref int cnt)
        {
            Function_登入();
            cnt++;
        }
        void cnt_Program_後台登入_一維碼登入_等待登入完成(ref int cnt)
        {
            cnt++;
        }
        #endregion

        #region Function
        private bool Function_登入()
        {
            bool flag = true;
            string json_in = "";
            string json_result = "";
            returnData returnData = new returnData();
            sessionClass _sessionClass = new sessionClass();
            _sessionClass.ID = this.textBox_後台登入_帳號.Text.ToUpper();
            _sessionClass.Password = this.textBox_後台登入_密碼.Text.ToUpper();
            returnData.Data = _sessionClass;
            json_in = returnData.JsonSerializationt();
            json_result = Net.WEBApiPostJson(dBConfigClass.Login_URL, json_in);
            returnData = json_result.JsonDeserializet<returnData>();
            if(returnData == null)
            {
                MyMessageBox.ShowDialog("登入API呼叫異常,請檢查網路連結及設定!");
                return false;
            }
            _sessionClass = returnData.Data.ObjToClass<sessionClass>();
            if(returnData.Code == 200)
            {
                this.Invoke(new Action(delegate 
                {
                    this.登入者名稱 = _sessionClass.Name;
                    this.登入者ID = _sessionClass.ID;
                    this.登入者顏色 = _sessionClass.Color;
                    this.登入者藥師證字號 = _sessionClass.license;
                    this.textBox_後台登入_帳號.Text = "";
                    this.textBox_後台登入_密碼.Text = "";
                    this.登入者權限 = _sessionClass.Permissions;
                    if (this.登入者名稱 == "最高管理權限")
                    {
                        Function_登入權限資料_取得權限(this.登入者權限);
                        pLC_Device_最高權限.Bool = true;
                        this.Text = $"{this.FormText}         [登入者名稱 : {登入者名稱}] [登入者ID : {登入者ID}]";
                        this.pannel_Locker_Design.ShowControlPannel = true;

                        this.rJ_Pannel_後台登入_歡迎登入.Visible = true;
                        this.PLC_Device_已登入.Bool = true;
                    }
                    else
                    {
                        if (!PLC_Device_後台登入_RFID登入.Bool)
                        {
                            if (!myConfigClass.帳密登入_Enable)
                            {
                                MyMessageBox.ShowDialog("禁止帳密登入!");
                                flag = false;
                                return;
                            }
                        }
                        Function_登入權限資料_取得權限(this.登入者權限);
                        this.Text = $"{this.FormText}         [登入者名稱 : {登入者名稱}] [登入者ID : {登入者ID}]";
                        this.rJ_Pannel_後台登入_歡迎登入.Visible = true;
                        this.PLC_Device_已登入.Bool = true;
                    }
              
               
                }));
            }
            else
            {
                MyMessageBox.ShowDialog($"{returnData.Result}");
                return false;
            }
           
            return flag;
        }
        private void Function_登出()
        {
            Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.登出, this.登入者名稱, "後台登入");
            this.Invoke(new Action(delegate
            {
                this.登入者名稱 = "";
                this.登入者ID = "";
                this.登入者顏色 = "";
                this.登入者藥師證字號 = "";
                this.textBox_後台登入_帳號.Text = "";
                this.textBox_後台登入_密碼.Text = "";
                this.PLC_Device_已登入.Bool = false;
                this.Function_登入權限資料_清除權限();

                this.Text = $"{this.FormText}";
                this.pannel_Locker_Design.ShowControlPannel = false;
                this.rJ_Pannel_後台登入_歡迎登入.Visible = false;
            }));
            if (this.plC_ScreenPage_Main.PageText == "調劑作業") return;
            //if (this.plC_ScreenPage_Main.PageText == "管制抽屜") return;

            if (plC_RJ_ScreenButton_調劑作業.Visible) this.plC_ScreenPage_Main.SelecteTabText("調劑作業");
            else if (plC_RJ_ScreenButton_管制抽屜.Visible) this.plC_ScreenPage_Main.SelecteTabText("管制抽屜");
            else this.plC_ScreenPage_Main.SelecteTabText("後台登入");
         
            //this.PLC_Device_主頁面頁碼.Value = 0;
        }
        #endregion
        #region Event
        private void Button_後台網址_開啟_Click(object sender, EventArgs e)
        {
            string url = $"{dBConfigClass.Web_URL}";
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法開啟網頁: " + ex.Message);
            }
        }
        private void TextBox_後台登入_帳號_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox_後台登入_密碼.Focus();
            }
        }
        private void TextBox_後台登入_密碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!this.Function_登入())
                {
                    textBox_後台登入_帳號.Focus();
                }
                else
                {
                    Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.密碼登入, this.登入者名稱, "後台登入");
                }
            }
        }
        private void PlC_RJ_Button_後台登入_登入_MouseDownEvent(MouseEventArgs mevent)
        {
            Function_登入();
        }
        private void PlC_RJ_Button_後台登入_登出_MouseDownEvent(MouseEventArgs mevent)
        {
            Function_登出();
        }
        #endregion
    }
}
