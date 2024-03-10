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
    public partial class Dialog_交班藥品選擇 : Form
    {
        private SQL_DataGridView.ConnentionClass _dB_Basic;
        public enum enum_已選藥品
        {
            GUID,
            藥碼,
            藥名,
            單位,
        }
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

        public Dialog_交班藥品選擇(SQL_DataGridView.ConnentionClass dB_Basic)
        {
            InitializeComponent();
            this.Load += Dialog_交班藥品選擇_Load;
            this.rJ_Button_藥品資料_管制級別_搜尋.MouseDownEvent += RJ_Button_藥品資料_管制級別_搜尋_MouseDownEvent;
            this.rJ_Button_藥品資料_藥品碼_搜尋.MouseDownEvent += RJ_Button_藥品資料_藥品碼_搜尋_MouseDownEvent;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.MouseDownEvent += RJ_Button_藥品資料_藥品名稱_搜尋_MouseDownEvent;
            this.rJ_Button_藥品資料_中文名稱_搜尋.MouseDownEvent += RJ_Button_藥品資料_中文名稱_搜尋_MouseDownEvent;

            this.rJ_Button_藥品資料_選擇藥品.MouseDownEvent += RJ_Button_藥品資料_選擇藥品_MouseDownEvent;
            this.rJ_Button_藥品資料_刪除選擇藥品.MouseDownEvent += RJ_Button_藥品資料_刪除選擇藥品_MouseDownEvent;
            _dB_Basic = dB_Basic;
        }

     

        #region Function
        private List<object[]> Function_取得所有交班藥品()
        {
            List<object[]> list_藥檔資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_藥檔資料_buf = new List<object[]>();
            List<object[]> list_交班藥品 = new List<object[]>();

        

            string url = $"{Main_Form.API_Server}/api/medShiftConfig/get_all";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Main_Form.ServerName}";
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json_out.JsonDeserializet<returnData>();
            List<medShiftConfigClass> medShiftConfigClasses = returnData.Data.ObjToListClass<medShiftConfigClass>();

            for (int i = 0; i < medShiftConfigClasses.Count; i++)
            {
                if (medShiftConfigClasses[i].是否交班 != true.ToString()) continue;
                object[] value = new object[new enum_已選藥品().GetLength()];
                value[(int)enum_已選藥品.GUID] = medShiftConfigClasses[i].GUID;
                value[(int)enum_已選藥品.藥碼] = medShiftConfigClasses[i].藥品碼;

                list_藥檔資料_buf = list_藥檔資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, medShiftConfigClasses[i].藥品碼);
                if (list_藥檔資料_buf.Count > 0)
                {
                    value[(int)enum_已選藥品.藥名] = list_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
                    value[(int)enum_已選藥品.單位] = list_藥檔資料_buf[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
                    list_交班藥品.Add(value);
                }
            }

            return list_交班藥品;
        }
        #endregion
        #region Event
        private void Dialog_交班藥品選擇_Load(object sender, EventArgs e)
        {
 
            string url = $"{Main_Form.API_Server}/api/MED_page/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Main_Form.ServerName}";
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"本地藥檔表單建立失敗!! Api_URL:{Main_Form.API_Server}");
                return;
            }

            comboBox_藥品資料_管制級別.SelectedIndex = 0;
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_藥品資料_藥檔資料, _dB_Basic);

            this.sqL_DataGridView_藥品資料_藥檔資料.Init(table);
            this.sqL_DataGridView_藥品資料_藥檔資料.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
            this.sqL_DataGridView_藥品資料_藥檔資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品碼);
            this.sqL_DataGridView_藥品資料_藥檔資料.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品名稱);
            this.sqL_DataGridView_藥品資料_藥檔資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.包裝單位);

            this.sqL_DataGridView_已選藥品.Init();
            this.sqL_DataGridView_已選藥品.RefreshGrid(Function_取得所有交班藥品());


        }
        private void RJ_Button_藥品資料_刪除選擇藥品_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_已選藥品.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"未選擇藥品");
                return;
            }

            if (MyMessageBox.ShowDialog("是否刪除選擇藥品?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            List<medShiftConfigClass> medShiftConfigClasses = new List<medShiftConfigClass>();
            for (int i = 0; i < list_value.Count; i++)
            {
                medShiftConfigClass medShiftConfigClass = new medShiftConfigClass();
                medShiftConfigClass.藥品碼 = list_value[i][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                medShiftConfigClass.是否交班 = false.ToString();
                medShiftConfigClasses.Add(medShiftConfigClass);
            }
            string url = $"{Main_Form.API_Server}/api/medShiftConfig/update_by_code";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Main_Form.ServerName}";
            returnData.Data = medShiftConfigClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json_out.JsonDeserializet<returnData>();



            this.sqL_DataGridView_已選藥品.RefreshGrid(Function_取得所有交班藥品());
            MyMessageBox.ShowDialog($"{returnData.Result}");
        }
        private void RJ_Button_藥品資料_中文名稱_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.中文名稱, rJ_TextBox_藥品資料_中文名稱.Texts, true);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"未搜尋到資料!");
                return;
            }
            this.sqL_DataGridView_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void RJ_Button_藥品資料_藥品名稱_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品名稱, rJ_TextBox_藥品資料_藥品名稱.Texts, true);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"未搜尋到資料!");
                return;
            }
            this.sqL_DataGridView_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void RJ_Button_藥品資料_藥品碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品碼, rJ_TextBox_藥品資料_藥品碼.Texts, true);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"未搜尋到資料!");
                return;
            }
            this.sqL_DataGridView_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void RJ_Button_藥品資料_管制級別_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);

            this.Invoke(new Action(delegate 
            {
                list_value = list_value.GetRows((int)enum_藥品資料_藥檔資料.管制級別, comboBox_藥品資料_管制級別.Text);
                if (list_value.Count == 0)
                {
                    MyMessageBox.ShowDialog($"未搜尋到資料!");
                    return;
                }
                this.sqL_DataGridView_藥品資料_藥檔資料.RefreshGrid(list_value);
            }));
         
        }
        private void RJ_Button_藥品資料_選擇藥品_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"未選擇藥品");
                return;
            }
            List<medShiftConfigClass> medShiftConfigClasses = new List<medShiftConfigClass>(); 
            for (int i = 0; i < list_value.Count; i++)
            {
                medShiftConfigClass medShiftConfigClass = new medShiftConfigClass();
                medShiftConfigClass.藥品碼 = list_value[i][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                medShiftConfigClass.是否交班 = true.ToString();
                medShiftConfigClasses.Add(medShiftConfigClass);
            }
            string url = $"{Main_Form.API_Server}/api/medShiftConfig/update_by_code";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{Main_Form.ServerName}";
            returnData.Data = medShiftConfigClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json_out.JsonDeserializet<returnData>();


   
            this.sqL_DataGridView_已選藥品.RefreshGrid(Function_取得所有交班藥品());
            MyMessageBox.ShowDialog($"{returnData.Result}");
      

        }
        #endregion

    }
}
