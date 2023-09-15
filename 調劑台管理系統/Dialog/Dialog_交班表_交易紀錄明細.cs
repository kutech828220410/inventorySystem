using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;

namespace 調劑台管理系統
{
    public partial class Dialog_交易紀錄明細 : Form
    {
        private List<object[]> list_value = new List<object[]>();
        private string _藥碼 = "";
        private string _藥名 = "";

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
        public Dialog_交易紀錄明細()
        {
            InitializeComponent();
            this.Load += Dialog_交易紀錄明細_Load;
        }
        public Dialog_交易紀錄明細(List<object[]> list_value, string 藥碼, string 藥名)
        {
            InitializeComponent();
            this.Load += Dialog_交易紀錄明細_Load;
            this.list_value = list_value;
            _藥碼 = 藥碼;
            _藥名 = 藥名;
        }
        #region Function

        #endregion
        #region Event
        private void Dialog_交易紀錄明細_Load(object sender, EventArgs e)
        {
            string url = $"{Form1.API_Server}/api/transactions/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Form1.ServerName}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"交易紀錄表單建立失敗!! Api_URL:{Form1.API_Server}");
                return;
            }
            this.sqL_DataGridView_交易記錄查詢.Init(table);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnVisible(false, new enum_交易記錄查詢資料().GetEnumNames());

            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.動作);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.診別);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.領藥號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.庫存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.交易量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.結存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作人);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病人姓名);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病歷號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(140, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作時間);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(140, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.開方時間);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.收支原因);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(320, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.備註);
            if (list_value.Count > 0) this.sqL_DataGridView_交易記錄查詢.RefreshGrid(list_value);
            this.rJ_Lable_Title.Text = $"[{_藥碼}] 藥名:{_藥名}";
        }
        #endregion
    }
}
