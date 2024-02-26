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
    public partial class Dialog_備藥通知處方選擇 : MyDialog
    {
        public udnoectc udnoectc = null;
  
        public Dialog_備藥通知處方選擇()
        {
            InitializeComponent();
            this.TopMost = true;
            this.dateTimeIntervelPicker_備藥通知時間範圍.SureClick += DateTimeIntervelPicker_備藥通知時間範圍_SureClick;

            this.button_重新整理.Click += Button_重新整理_Click;
           
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.uc_備藥通知處方.Init();
        }

        
        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
           
                List<object[]> list_value = this.uc_備藥通知處方.GetSelectedRows();
                if(list_value.Count > 0)
                {
                    List<udnoectc> udnoectcs = list_value.SQLToClass<udnoectc, enum_udnoectc>();
                    if(udnoectcs.Count > 0)
                    {
                        this.DialogResult = DialogResult.Yes;

                        string url = $"{Main_Form.API_Server}/api/ChemotherapyRxScheduling/get_udnoectc_by_GUID";
                        returnData returnData = new returnData();
                        returnData.ServerName = "cheom";
                        returnData.ServerType = "癌症備藥機";
                        returnData.Value = udnoectcs[0].GUID;
                        string json_in = returnData.JsonSerializationt();
                        string json_out = Basic.Net.WEBApiPostJson(url, json_in);
                        returnData = json_out.JsonDeserializet<returnData>();
                        List<udnoectc> udnoectcs_temp = returnData.Data.ObjToClass<List<udnoectc>>();


                        udnoectc = udnoectcs_temp[0];
                        this.Close();
                    }
                
                }
                else
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選擇處方", 1500);
                    dialog_AlarmForm.ShowDialog();
                    //return;
                }
         
            }));
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.Close();
            }));
        }
        private void DateTimeIntervelPicker_備藥通知時間範圍_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            LoadingForm.ShowLoadingForm();
            this.uc_備藥通知處方.Function_取得備藥通知(dateTimeIntervelPicker_備藥通知時間範圍.StartTime, dateTimeIntervelPicker_備藥通知時間範圍.EndTime, true);
            LoadingForm.CloseLoadingForm();
        }
        private void Button_重新整理_Click(object sender, EventArgs e)
        {
            LoadingForm.ShowLoadingForm();
            this.uc_備藥通知處方.Function_取得備藥通知(dateTimeIntervelPicker_備藥通知時間範圍.StartTime, dateTimeIntervelPicker_備藥通知時間範圍.EndTime, true);
            LoadingForm.CloseLoadingForm();
        }
        public void Init()
        {
            this.uc_備藥通知處方.Init();
            this.uc_備藥通知處方.Function_取得備藥通知(dateTimeIntervelPicker_備藥通知時間範圍.StartTime, dateTimeIntervelPicker_備藥通知時間範圍.EndTime, true);         
        }
    }
}
