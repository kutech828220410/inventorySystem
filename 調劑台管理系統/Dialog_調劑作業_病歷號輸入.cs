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
using SQLUI;

namespace 調劑台管理系統
{
    public partial class Dialog_調劑作業_病歷號輸入 : Form
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

        public Dialog_調劑作業_病歷號輸入()
        {
            InitializeComponent();
            this.Load += Dialog_調劑作業_病歷號輸入_Load;
        }

        private void Dialog_調劑作業_病歷號輸入_Load(object sender, EventArgs e)
        {
            string url = $"{Form1.API_Server}/api/order/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Form1.ServerName}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"醫囑資料表單建立失敗!! Api_URL:{Form1.API_Server}");
                return;
            }

            this.sqL_DataGridView_醫囑資料.Init(table);
            this.sqL_DataGridView_醫囑資料.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥局代碼);
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥袋類型);
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.病人姓名);
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_醫囑資料.交易量);
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.開方日期);
            this.sqL_DataGridView_醫囑資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.產出時間);

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            this.Invoke(new Action(delegate
            {
            }));
        }

    

        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
    }
}
