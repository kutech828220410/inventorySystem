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
    public partial class Dialog_領藥號輸入 : MyDialog
    {
        public List<OrderClass> Value = new List<OrderClass>();
        public Dialog_領藥號輸入()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
           this.LoadFinishedEvent += Dialog_領藥號輸入_LoadFinishedEvent;
            this.rJ_Button_輸入.MouseDownEvent += RJ_Button_輸入_MouseDownEvent;
        }

        private void Dialog_領藥號輸入_LoadFinishedEvent(EventArgs e)
        {
            string url = $"{Main_Form.API_Server}/api/order/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
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
            //this.sqL_DataGridView_醫令資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.產出時間);

            this.sqL_DataGridView_醫令資料.Set_ColumnText("藥碼", enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料.Set_ColumnText("藥名", enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料.Set_ColumnText("使用時間", enum_醫囑資料.開方日期);

            this.sqL_DataGridView_醫令資料_已選取處方.Init(table);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_醫囑資料.交易量);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.開方日期);

            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnText("藥碼", enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnText("藥名", enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_醫令資料_已選取處方.Set_ColumnText("使用時間", enum_醫囑資料.開方日期);

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            this.rJ_Button_選取處方.MouseDownEvent += RJ_Button_選取處方_MouseDownEvent;
            this.rJ_Button_刪除.MouseDownEvent += RJ_Button_刪除_MouseDownEvent;
            this.Refresh();
        }

        private void RJ_Button_輸入_MouseDownEvent(MouseEventArgs mevent)
        {
            string BAG_NUM = rJ_TextBox_領藥號.Text;
            if (BAG_NUM.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("領藥號空白!");
                return;
            }
            List<OrderClass> orderClasses = new List<OrderClass>();
            LoadingForm.ShowLoadingForm();
            orderClasses = OrderClass.get_API_by_BAG_NUM(Main_Form.dBConfigClass.Order_bag_num_ApiURL, BAG_NUM, rJ_DatePicker_日期.Value);
            LoadingForm.CloseLoadingForm();
            if (orderClasses == null)
            {
                MyMessageBox.ShowDialog("輸入資訊錯誤");
                return;
            }
            if (orderClasses.Count == 0)
            {
                MyMessageBox.ShowDialog("查無資料");
                return;
            }
            List<object[]> list_order = orderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
            List<object[]> list_order_buf = new List<object[]>();
            List<string> Codes = (from temp in list_order
                                  select temp[(int)enum_醫囑資料.藥品碼].ObjectToString()).Distinct().ToList();
            List<medClass> medClasses = medClass.get_med_clouds_by_codes(Main_Form.API_Server, Codes);
            List<medClass> medClasses_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs = medClasses.CoverToDictionaryByCode();
            list_order.Sort(new ICP_醫令資料());


            for (int i = 0; i < list_order.Count; i++)
            {
                string 藥碼 = list_order[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                medClasses_buf = keyValuePairs.SortDictionaryByCode(藥碼);
                if (medClasses_buf.Count > 0)
                {
                    bool flag_add = false;
                    string 管制級別 = medClasses_buf[0].管制級別;
                    string 高價藥品 = medClasses_buf[0].高價藥品;
                    string 警訊藥品 = medClasses_buf[0].警訊藥品;
                    if (checkBox_管1_3.Checked) if (管制級別 == "1") flag_add = true;
                    if (checkBox_管1_3.Checked) if (管制級別 == "2") flag_add = true;
                    if (checkBox_管1_3.Checked) if (管制級別 == "3") flag_add = true;
                    if (checkBox_管4.Checked) if (管制級別 == "4") flag_add = true;
                    if (checkBox_高價藥.Checked) if (高價藥品.StringToBool()) flag_add = true;
                    if (checkBox_高警訊.Checked) if (警訊藥品.StringToBool()) flag_add = true;
                    if (checkBox_其餘品項.Checked) if (管制級別 == "N") flag_add = true;
                    if (flag_add == true) list_order_buf.Add(list_order[i]);
                }
            }
            if (list_order_buf.Count == 0)
            {
                MyMessageBox.ShowDialog("無可顯示醫令,請檢查勾選條件");
            }
            this.sqL_DataGridView_醫令資料.RefreshGrid(list_order_buf);

        }

   
        private void RJ_Button_選取處方_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_已選取處方 = sqL_DataGridView_醫令資料_已選取處方.GetAllRows();
            List<object[]> list_已選取處方_buf = new List<object[]>();
            List<object[]> list_醫令資料 = this.sqL_DataGridView_醫令資料.Get_All_Select_RowsValues();
            if (list_醫令資料.Count == 0)
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
                Value = list_已選處方.SQLToClass<OrderClass, enum_醫囑資料>();
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
