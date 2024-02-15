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
    public partial class Dialog_備藥通知處方選擇 : Form
    {
        public udnoectc udnoectc = null;
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
        public Dialog_備藥通知處方選擇()
        {
            InitializeComponent();
            this.button_重新整理.Click += Button_重新整理_Click;
           
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.uc_備藥通知處方.Init();
        }

 

        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                List<object[]> list_value = this.uc_備藥通知處方.GetSelectedRows();
                if(list_value.Count > 0)
                {
                    List<udnoectc> udnoectcs = list_value.SQLToClass<udnoectc, enum_udnoectc>();
                    if(udnoectcs.Count > 0)
                    {
                        udnoectc = udnoectcs[0];
                    }
                }
                this.Close();
            }));
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.Close();
            }));
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
