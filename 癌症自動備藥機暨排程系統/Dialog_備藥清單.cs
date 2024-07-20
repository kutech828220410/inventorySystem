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
namespace 癌症自動備藥機暨排程系統
{
    public partial class Dialog_備藥清單 : MyDialog
    {
        public delegate void SureClickEventHandler();
        public event SureClickEventHandler SureClickEvent;

        private string _login_name = "";
   

        private string GUID = "";
        private udnoectc udnoectc = null;
        public Dialog_備藥清單(string guid , string login_name)
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
            LoadingForm.ShowLoadingForm();
            this.GUID = guid;
            udnoectc udnoectc = udnoectc.get_udnoectc_by_GUID(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, GUID);
            this.Load += Dialog_備藥清單_Load;
            if (udnoectc == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("異常:查無資料", 2000);
                form.Invoke(new Action(delegate
                {
                    this.Close();
                    this.DialogResult = DialogResult.No;
                }));
            }
            this._login_name = login_name;
            this.udnoectc = udnoectc;
            this.LoadFinishedEvent += Dialog_備藥清單_LoadFinishedEvent;


        }

        private void Dialog_備藥清單_LoadFinishedEvent(EventArgs e)
        {
            this.uc_備藥通知內容.Init(udnoectc, _login_name, false);
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.plC_RJ_Button_變異紀錄.MouseDownEvent += PlC_RJ_Button_變異紀錄_MouseDownEvent;
            LoadingForm.CloseLoadingForm();
        }

        private void Dialog_備藥清單_Load(object sender, EventArgs e)
        {         
         
        }
     
        #region Event
        private void PlC_RJ_Button_變異紀錄_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Dialog_變異紀錄 dialog_變異紀錄 = new Dialog_變異紀錄(this.GUID);
                dialog_變異紀錄.ShowDialog();
            }));
        }
        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            string url = $"{Main_Form.API_Server}/api/ChemotherapyRxScheduling/update_udnoectc_orders_comp";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = this._login_name;

            List<object[]> list_value = this.uc_備藥通知內容.GetSelectedRows();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取備藥藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<udnoectc_orders> list_udnoectc_orders = new List<udnoectc_orders>();
            List<udnoectc_orders> list_udnoectc_orders_replace = new List<udnoectc_orders>();
            for (int i = 0; i < list_value.Count; i++)
            {
                list_udnoectc_orders = list_value[i][0].ObjectToString().JsonDeserializet<List<udnoectc_orders>>();
                list_udnoectc_orders_replace.LockAdd(list_udnoectc_orders);
            }
            returnData.Data = list_udnoectc_orders_replace;

            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            this.Close();
            if (SureClickEvent != null) SureClickEvent();
        }
   
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Close();
                this.DialogResult = DialogResult.No;
            }));
        }
        #endregion
    }
}
