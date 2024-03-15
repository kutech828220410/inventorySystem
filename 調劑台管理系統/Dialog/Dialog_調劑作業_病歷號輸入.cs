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
    public partial class Dialog_調劑作業_病歷號輸入 : MyDialog
    {
        public List<OrderClass> Value = new List<OrderClass>();
   
        private SQLUI.SQL_DataGridView sQL_DataGridView_藥檔資料 = null;
        public Dialog_調劑作業_病歷號輸入(SQL_DataGridView _sQL_DataGridView_藥檔資料)
        {
            InitializeComponent();
            this.Load += Dialog_調劑作業_病歷號輸入_Load;
            this.rJ_Button_輸入.MouseDownEvent += RJ_Button_輸入_MouseDownEvent;
            this.sQL_DataGridView_藥檔資料 = _sQL_DataGridView_藥檔資料;
        }

        private void RJ_Button_輸入_MouseDownEvent(MouseEventArgs mevent)
        {
            string MRN = rJ_TextBox_病歷號.Text;
            if (MRN.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("病歷號空白!");
                return;
            }
            string order_url = Main_Form.Order_URL.ToLower().Replace("?barcode=", "");
            if (order_url.Substring(order_url.Length - 1, 1) == "/")
            {
                order_url = order_url.Substring(0, order_url.Length - 1);
            }

            string url = $"{order_url}?MRN={MRN}";
            string json = Net.WEBApiGet(url);
            returnData returnData = json.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                MyMessageBox.ShowDialog("未搜尋到醫令!");
                return;
            }
            if(returnData.Code != 200)
            {
                MyMessageBox.ShowDialog($"{returnData.Result}");
                return;
            }
            List<OrderClass> orderClasses = returnData.Data.ObjToListClass<OrderClass>();
    
      
            List<object[]> list_order = orderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
            List<object[]> list_order_buf = new List<object[]>();
            List<object[]> list_藥檔資料 = this.sQL_DataGridView_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_藥檔資料_buf = new List<object[]>();
            list_order.Sort(new ICP_醫令資料());

         
            for (int i = 0; i < list_order.Count; i++)
            {
                string 藥碼 = list_order[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                list_藥檔資料_buf = list_藥檔資料.GetRows((int)enum_雲端藥檔.藥品碼, 藥碼);
                if (list_藥檔資料_buf.Count > 0)
                {
                    bool flag_add = false;
                    string 管制級別 = list_藥檔資料_buf[0][(int)enum_雲端藥檔.管制級別].ObjectToString();
                    string 高價藥品 = list_藥檔資料_buf[0][(int)enum_雲端藥檔.高價藥品].ObjectToString();
                    string 警訊藥品 = list_藥檔資料_buf[0][(int)enum_雲端藥檔.警訊藥品].ObjectToString();
                    if (管制級別 != "N") flag_add = true;
                    if (高價藥品.StringToBool()) flag_add = true;
                    if (警訊藥品.StringToBool()) flag_add = true;
                    if (flag_add == true) list_order_buf.Add(list_order[i]);
                }
            }

            this.sqL_DataGridView_醫令資料.RefreshGrid(list_order_buf);

        }
        private void Dialog_調劑作業_病歷號輸入_Load(object sender, EventArgs e)
        {
            string url = $"{Main_Form.API_Server}/api/order/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Main_Form.ServerName}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"醫令資料表單建立失敗!! Api_URL:{Main_Form.API_Server}");
                return;
            }

            this.sqL_DataGridView_醫令資料.Init(table);
            this.sqL_DataGridView_醫令資料.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥局代碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.病人姓名);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_醫囑資料.交易量);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.開方日期);
            this.sqL_DataGridView_醫令資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.產出時間);

            this.sqL_DataGridView_醫令資料_已選取處方.Init(table);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_醫囑資料.交易量);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.開方日期);
            
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            this.rJ_Button_選取處方.MouseDownEvent += RJ_Button_選取處方_MouseDownEvent;
            this.rJ_Button_刪除.MouseDownEvent += RJ_Button_刪除_MouseDownEvent;
            this.Invoke(new Action(delegate
            {
            }));
        }

   

        private void RJ_Button_選取處方_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_已選取處方 = sqL_DataGridView_醫令資料_已選取處方.GetAllRows();
            List<object[]> list_已選取處方_buf = new List<object[]>();
            List<object[]> list_醫令資料 = this.sqL_DataGridView_醫令資料.Get_All_Select_RowsValues();
            if(list_醫令資料.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取處方資料!");
                return;
            }
            string GUID = list_醫令資料[0][(int)enum_醫囑資料.GUID].ObjectToString();
            list_已選取處方_buf = list_已選取處方.GetRows((int)enum_醫囑資料.GUID, GUID);
            if (list_已選取處方_buf.Count > 0)
            {
                MyMessageBox.ShowDialog("此處方已加入!");
                return;
            }
            sqL_DataGridView_醫令資料_已選取處方.AddRow(list_醫令資料[0], true);
        }
        private void RJ_Button_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_已選取處方 = sqL_DataGridView_醫令資料_已選取處方.Get_All_Select_RowsValues();
            if (list_已選取處方.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取處方資料!");
                return;
            }
            if (MyMessageBox.ShowDialog("確認刪除選擇處方?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            sqL_DataGridView_醫令資料_已選取處方.DeleteExtra(list_已選取處方, true);

        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                List<object[]> list_已選處方 = sqL_DataGridView_醫令資料_已選取處方.GetAllRows();
                if (list_已選處方.Count == 0)
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
                    return;
                }
                Value = list_已選處方.SQLToClass<OrderClass , enum_醫囑資料>();
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

        public class ICP_醫令資料 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_醫囑資料.開方日期].ToString().StringToDateTime();
                DateTime datetime2 = y[(int)enum_醫囑資料.開方日期].ToString().StringToDateTime();
                int compare = DateTime.Compare(datetime2, datetime1);
                return compare;
 

            }
        }
    }
}
