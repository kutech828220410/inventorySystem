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
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;
namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        public enum enum_處方搜尋
        {
            [Description("PRI_KEY,VARCHAR,15,NONE")]
            PRI_KEY,
            [Description("領藥號,VARCHAR,15,NONE")]
            領藥號,
            [Description("姓名,VARCHAR,15,NONE")]
            姓名,
            [Description("病歷號,VARCHAR,15,NONE")]
            病歷號,
            [Description("性別,VARCHAR,15,NONE")]
            性別,
            [Description("年齡,VARCHAR,15,NONE")]
            年齡,
            [Description("天數,VARCHAR,15,NONE")]
            天數,
            [Description("筆數,VARCHAR,15,NONE")]
            筆數,
            [Description("已調劑,VARCHAR,15,NONE")]
            已調劑,
            [Description("處方時間,VARCHAR,15,NONE")]
            處方時間,
    

        }
        private void Program_處方搜尋_Init()
        {
            
            Table table = new Table(new enum_處方搜尋());

            this.sqL_DataGridView_處方搜尋.Init(table);
            this.sqL_DataGridView_處方搜尋.Set_ColumnVisible(false, new enum_OrderT().GetEnumNames());
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_處方搜尋.領藥號);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_處方搜尋.病歷號);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_處方搜尋.姓名);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_處方搜尋.性別);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_處方搜尋.年齡);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_處方搜尋.天數);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_處方搜尋.筆數);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_處方搜尋.已調劑);
            this.sqL_DataGridView_處方搜尋.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleCenter, enum_處方搜尋.處方時間);
            this.sqL_DataGridView_處方搜尋.DataGridRefreshEvent += SqL_DataGridView_處方搜尋_DataGridRefreshEvent;
            this.sqL_DataGridView_處方搜尋.RowDoubleClickEvent += SqL_DataGridView_處方搜尋_RowDoubleClickEvent;
            this.sqL_DataGridView_處方搜尋.DataGridRowsChangeRefEvent += SqL_DataGridView_處方搜尋_DataGridRowsChangeRefEvent;

            this.dateTimeIntervelPicker_處方搜尋_開方時間.SureClick += DateTimeIntervelPicker_處方搜尋_開方時間_SureClick;
            this.comboBox_處方搜尋_搜尋條件.SelectedIndex = 0;
            this.rJ_Button_處方搜尋_搜尋.MouseDownEvent += RJ_Button_處方搜尋_搜尋_MouseDownEvent;
            plC_UI_Init.Add_Method(Program_處方搜尋);
        }

      

        private void Program_處方搜尋()
        {

        }
        #region Function
        private void Finction_處方搜尋_更新UI(List<OrderTClass> orderTClasses)
        {
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_value_已調劑 = new List<object[]>();
            List<object[]> list_value_未調劑 = new List<object[]>();
            List<object[]> list_value_buf = new List<object[]>();
            List<OrderTClass> orderTClasses_buf = new List<OrderTClass>();
            orderTClasses.sort(OrderTClassMethod.SortType.領藥號);
            List<string> list_PRI_KEY = (from temp in orderTClasses
                                         select temp.PRI_KEY).ToList();
            var keyValuePairs = orderTClasses.CoverToDictionaryBy_PRI_KEY();

            for (int i = 0; i < list_PRI_KEY.Count; i++)
            {
                orderTClasses_buf = keyValuePairs.SortDictionaryBy_PRI_KEY(list_PRI_KEY[i]);
                for (int k = 0; k < orderTClasses_buf.Count; k++)
                {
                    bool flag_已調劑 = true;
                    if (orderTClasses_buf[k].實際調劑量.StringIsDouble() == false)
                    {
                        flag_已調劑 = false;
                    }
                    list_value_buf = list_value.GetRows((int)enum_病患資訊.PRI_KEY, list_PRI_KEY[i]);
                    if (list_value_buf.Count == 0)
                    {

                        object[] value = new object[new enum_處方搜尋().GetLength()];
                        value[(int)enum_處方搜尋.PRI_KEY] = orderTClasses_buf[k].PRI_KEY;
                        value[(int)enum_處方搜尋.領藥號] = orderTClasses_buf[k].領藥號;
                        value[(int)enum_處方搜尋.姓名] = orderTClasses_buf[k].病人姓名;
                        value[(int)enum_處方搜尋.年齡] = orderTClasses_buf[k].年齡.StringToInt32().ToString();
                        value[(int)enum_處方搜尋.性別] = orderTClasses_buf[k].性別;
                        value[(int)enum_處方搜尋.病歷號] = orderTClasses_buf[k].病歷號;
                        value[(int)enum_處方搜尋.天數] = orderTClasses_buf[k].天數;
                        value[(int)enum_處方搜尋.筆數] = orderTClasses_buf.Count;
                        value[(int)enum_處方搜尋.處方時間] = orderTClasses_buf[k].開方日期;
                        value[(int)enum_處方搜尋.已調劑] = flag_已調劑 ? "Y" : "N";
                        list_value.Add(value);
                    }
                }
            }
            this.sqL_DataGridView_處方搜尋.RefreshGrid(list_value);
        }
        private bool Finction_處方搜尋_處方分類(object[] value)
        {
            string 已調劑 = value[(int)enum_處方搜尋.已調劑].ObjectToString();
            if (checkBox_處方搜尋_已調劑.Checked)
            {
                if (已調劑 == "Y") return true;
            }
            if (checkBox_處方搜尋_未調劑.Checked)
            {
                if (已調劑 == "N") return true;
            }
            return false;
        }
        #endregion
        #region Event
        private void SqL_DataGridView_處方搜尋_DataGridRefreshEvent()
        {
            string 已調劑 = "";
            for (int i = 0; i < this.sqL_DataGridView_處方搜尋.dataGridView.Rows.Count; i++)
            {
                已調劑 = this.sqL_DataGridView_處方搜尋.dataGridView.Rows[i].Cells[enum_處方搜尋.已調劑.GetEnumName()].Value.ToString();
                if (已調劑 == "Y")
                {
                    this.sqL_DataGridView_處方搜尋.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                    this.sqL_DataGridView_處方搜尋.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void SqL_DataGridView_處方搜尋_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList = (from temp in RowsList
                        where Finction_處方搜尋_處方分類(temp)
                        select temp).ToList();
        }
        private void DateTimeIntervelPicker_處方搜尋_開方時間_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {     
            List<OrderTClass> orderTClasses = OrderTClass.get_by_rx_time_st_end(Main_Form.API_Server, start, end);
            Finction_處方搜尋_更新UI(orderTClasses);
        }
        private void SqL_DataGridView_處方搜尋_RowDoubleClickEvent(object[] RowValue)
        {
            string PRI_KEY = RowValue[(int)enum_處方搜尋.PRI_KEY].ObjectToString();
            Dialog_處方查詢 dialog_處方查詢 = new Dialog_處方查詢(PRI_KEY);
            dialog_處方查詢.ShowDialog();
        }
        private void RJ_Button_處方搜尋_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<OrderTClass> orderTClasses = new List<OrderTClass>();
            string text = textBox_處方搜尋_搜尋內容.Text;
            string cmb_text = "";
            this.Invoke(new Action(delegate 
            {
                cmb_text = this.comboBox_處方搜尋_搜尋條件.Text;
            }));

            if (text.StringIsEmpty() == true)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請輸入搜尋資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (cmb_text == enum_OrderT.領藥號.GetEnumName())
            {
                orderTClasses = OrderTClass.get_by_MED_BAG_NUM(Main_Form.API_Server, text);
            }
            if (cmb_text == enum_OrderT.病歷號.GetEnumName())
            {
                orderTClasses = OrderTClass.get_by_PATCODE(Main_Form.API_Server, text);
            }
            if (cmb_text == enum_OrderT.病人姓名.GetEnumName())
            {
                orderTClasses = OrderTClass.get_by_PATNAME(Main_Form.API_Server, text);
            }
            if(orderTClasses.Count == 0 )
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            Finction_處方搜尋_更新UI(orderTClasses);
            LoadingForm.CloseLoadingForm();
        }
        #endregion
    }
}
