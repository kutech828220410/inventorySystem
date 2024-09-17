using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using MyUI;
using SQLUI;
using HIS_DB_Lib;

using MyOffice;

namespace 智能藥庫系統
{
    public partial class Dialog_盤點單合併_選擇 : MyDialog
    {
        public enum enum_盤點單搜尋
        {
            [Description("GUID,VARCHAR,50,None")]
            GUID,
            [Description("單號,VARCHAR,50,None")]
            單號,
            [Description("名稱,VARCHAR,50,None")]
            名稱,
            [Description("建表時間,VARCHAR,50,None")]
            建表時間,
            [Description("盤點狀態,VARCHAR,50,None")]
            盤點狀態,
            [Description("Value,VARCHAR,50,None")]
            Value,
        }
        public enum enum_已選盤點單
        {
            [Description("單號,VARCHAR,50,None")]
            單號,
            [Description("名稱,VARCHAR,50,None")]
            名稱,
            [Description("建表時間,VARCHAR,50,None")]
            建表時間,
            [Description("盤點狀態,VARCHAR,50,None")]
            盤點狀態,
        }
        private inv_combinelistClass inv_CombinelistClass = null;
        public inv_combinelistClass Value
        {
            get
            {
                return inv_CombinelistClass;
            }
        }
        private List<inventoryClass.creat> _creats = new List<inventoryClass.creat>();
        private string _SN = "";
        public Dialog_盤點單合併_選擇(string SN)
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
            }));
            _SN = SN;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_盤點單搜尋.MouseDownEvent += PlC_RJ_Button_盤點單搜尋_MouseDownEvent;
            this.plC_RJ_Button_加入已選擇.MouseDownEvent += PlC_RJ_Button_加入已選擇_MouseDownEvent;
            this.plC_RJ_Button_已選盤點單_選取資料刪除.MouseDownEvent += PlC_RJ_Button_已選盤點單_選取資料刪除_MouseDownEvent;
            this.dateTimeIntervelPicker_建表日期.SureClick += DateTimeIntervelPicker_建表日期_SureClick;
            this.LoadFinishedEvent += Dialog_盤點單合併_選擇_LoadFinishedEvent;

        }

        #region Function
        public List<inventoryClass.creat> Fuction_取得盤點單(DateTime start, DateTime end)
        {
            List<inventoryClass.creat> creats = HIS_DB_Lib.inventoryClass.creat_get_by_CT_TIME_ST_END(Main_Form.API_Server, start, end);
            if (creats == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("盤點資訊回傳錯誤", 1500);
                dialog_AlarmForm.ShowDialog();
                return null;
            }
            creats = (from temp in creats
                      where inv_CombinelistClass.IsHaveRecord(temp) == false
                      select temp).ToList();

            return creats;
        }
        private void Function_RereshUI(List<inventoryClass.creat> creats)
        {
            sqL_DataGridView_盤點單搜尋.ClearGrid();
            _creats = creats;
            for (int i = 0; i < creats.Count; i++)
            {
                object[] value = new object[new enum_盤點單搜尋().GetLength()];
                value[(int)enum_盤點單搜尋.GUID] = creats[i].GUID;
                value[(int)enum_盤點單搜尋.單號] = creats[i].盤點單號;
                value[(int)enum_盤點單搜尋.名稱] = creats[i].盤點名稱;
                value[(int)enum_盤點單搜尋.建表時間] = creats[i].建表時間;
                value[(int)enum_盤點單搜尋.盤點狀態] = creats[i].盤點狀態;
                value[(int)enum_盤點單搜尋.Value] = creats[i].JsonSerializationt();
                sqL_DataGridView_盤點單搜尋.AddRow(value, false);
            }
            sqL_DataGridView_盤點單搜尋.RefreshGrid();
        }
        private void Function_已選盤點單_Reresh(List<inventoryClass.creat> creats)
        {
            sqL_DataGridView_已選盤點單.ClearGrid();
            _creats = creats;
            for (int i = 0; i < creats.Count; i++)
            {
                object[] value = new object[new enum_已選盤點單().GetLength()];
                value[(int)enum_已選盤點單.單號] = creats[i].盤點單號;
                value[(int)enum_已選盤點單.名稱] = creats[i].盤點名稱;
                value[(int)enum_已選盤點單.建表時間] = creats[i].建表時間;
                value[(int)enum_已選盤點單.盤點狀態] = creats[i].盤點狀態;
                sqL_DataGridView_已選盤點單.AddRow(value, false);
            }
            sqL_DataGridView_已選盤點單.RefreshGrid();
        }
        #endregion
        #region Event
        private void Dialog_盤點單合併_選擇_LoadFinishedEvent(EventArgs e)
        {
            LoadingForm.ShowLoadingForm();
            inv_CombinelistClass = inv_combinelistClass.get_full_inv_by_SN(Main_Form.API_Server, _SN);
            sqL_DataGridView_盤點單搜尋.RowsHeight = 40;
            sqL_DataGridView_盤點單搜尋.Init(new Table(new enum_盤點單搜尋()));
            sqL_DataGridView_盤點單搜尋.Set_ColumnVisible(false, new enum_盤點單搜尋().GetEnumNames());
            sqL_DataGridView_盤點單搜尋.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_盤點單搜尋.單號);
            sqL_DataGridView_盤點單搜尋.Set_ColumnWidth(700, DataGridViewContentAlignment.MiddleLeft, enum_盤點單搜尋.名稱);
            sqL_DataGridView_盤點單搜尋.Set_ColumnWidth(250, enum_盤點單搜尋.建表時間);
            sqL_DataGridView_盤點單搜尋.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_盤點單搜尋.盤點狀態);

            sqL_DataGridView_已選盤點單.RowsHeight = 40;
            sqL_DataGridView_已選盤點單.Init(new Table(new enum_已選盤點單()));
            sqL_DataGridView_已選盤點單.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_已選盤點單.單號);
            sqL_DataGridView_已選盤點單.Set_ColumnWidth(700, DataGridViewContentAlignment.MiddleLeft, enum_已選盤點單.名稱);
            sqL_DataGridView_已選盤點單.Set_ColumnWidth(250, enum_已選盤點單.建表時間);
            sqL_DataGridView_已選盤點單.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_已選盤點單.盤點狀態);


            Function_已選盤點單_Reresh(inv_CombinelistClass.Creats);

            this.dateTimeIntervelPicker_建表日期.SetDateTime(DateTime.Now.AddMonths(-1).GetStartDate(), DateTime.Now.AddMonths(0).GetEndDate());
            this.dateTimeIntervelPicker_建表日期.OnSureClick();

            LoadingForm.CloseLoadingForm();
        }
        private void PlC_RJ_Button_加入已選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_盤點單搜尋.Get_All_Checked_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料");
                return;
            }
            for (int i = 0; i < list_value.Count; i++)
            {
                string SN = list_value[i][(int)enum_盤點單搜尋.單號].ObjectToString();
                string json = list_value[i][(int)enum_盤點單搜尋.Value].ObjectToString();
                inventoryClass.creat creat = json.JsonDeserializet<inventoryClass.creat>();
                inv_CombinelistClass.AddRecord(creat);
         
            }
            this.sqL_DataGridView_盤點單搜尋.DeleteExtra(list_value, true);
            Function_已選盤點單_Reresh(inv_CombinelistClass.Creats);
        }
        private void PlC_RJ_Button_盤點單搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                LoadingForm.ShowLoadingForm();
                List<inventoryClass.creat> creats = Fuction_取得盤點單(dateTimeIntervelPicker_建表日期.StartTime, dateTimeIntervelPicker_建表日期.EndTime);
                this.rJ_Lable_狀態.Text = $"已搜尋到{creats.Count}筆資料";
                LoadingForm.CloseLoadingForm();
                if (creats == null) return;
                Function_RereshUI(creats);
            }));
    
        }
        private void DateTimeIntervelPicker_建表日期_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            LoadingForm.ShowLoadingForm();
            List<inventoryClass.creat> creats = Fuction_取得盤點單(start, end);
            this.rJ_Lable_狀態.Text = $"已搜尋到{creats.Count}筆資料";
            LoadingForm.CloseLoadingForm();
            if (creats == null) return;
            Function_RereshUI(creats);
        }
        private void PlC_RJ_Button_已選盤點單_選取資料刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_已選盤點單.Get_All_Checked_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料");
                return;
            }
            for(int i = 0; i < list_value.Count; i++)
            {
                string SN = list_value[i][(int)enum_已選盤點單.單號].ObjectToString();
                inv_CombinelistClass.DeleteRecord(SN);
            }
            Function_已選盤點單_Reresh(inv_CombinelistClass.Creats);

        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
          

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }


        #endregion
    }
}
