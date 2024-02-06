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
    public partial class Dialog_變異紀錄 : Form
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
        private string GUID = "";
        private udnoectc udnoectc = null;
        public Dialog_變異紀錄(string guid)
        {
            InitializeComponent();

            this.GUID = guid;
            string url = $"{Main_Form.API_Server}/api/ChemotherapyRxScheduling/get_udnoectc_by_GUID";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = GUID;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            List<udnoectc> udnoectcs = returnData.Data.ObjToClass<List<udnoectc>>();


            Dialog_備藥清單.form = this.ParentForm;
            this.Load += Dialog_變異紀錄_Load;
            if (udnoectcs.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("異常:查無資料", 2000);
                this.Invoke(new Action(delegate
                {
                    this.Close();
                    this.DialogResult = DialogResult.No;
                }));
            }
            udnoectc = udnoectcs[0];
        }

        private void Dialog_變異紀錄_Load(object sender, EventArgs e)
        {
            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 200, Table.IndexType.None);

            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
        }
        #region Event
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
