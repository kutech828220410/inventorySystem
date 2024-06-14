using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SQLUI;
using MyUI;
using Basic;
using H_Pannel_lib;
using HIS_DB_Lib;
namespace 智能藥庫系統
{

    public partial class Form1 : Form
    {
        public enum enum_驗收作業_驗收藥品清單
        {
            GUID,
            藥品碼,
            料號,
            藥品名稱,
            藥品條碼1,
            藥品條碼2,
            應收數量,

        }
        private void sub_Program_驗收作業_新增驗收_Init()
        {
            this.sqL_DataGridView_驗收作業_驗收藥品清單.Init();
            this.sqL_DataGridView_驗收作業_驗收藥品清單.DataGridRowsChangeRefEvent += SqL_DataGridView_驗收作業_驗收藥品清單_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_驗收作業_驗收藥品清單.RowEndEditEvent += SqL_DataGridView_驗收作業_驗收藥品清單_RowEndEditEvent1;
            this.sqL_DataGridView_驗收作業_驗收藥品清單.CellValidatingEvent += SqL_DataGridView_驗收作業_驗收藥品清單_CellValidatingEvent1;
            this.sqL_DataGridView_驗收作業_驗收藥品清單.Paint += SqL_DataGridView_驗收作業_驗收藥品清單_Paint;

            this.sqL_DataGridView_驗收作業_藥品資料.Init(this.sqL_DataGridView_藥庫_藥品資料);
            this.sqL_DataGridView_驗收作業_藥品資料.Set_ColumnVisible(false, new enum_medDrugstore().GetEnumNames());
            this.sqL_DataGridView_驗收作業_藥品資料.Set_ColumnVisible(true, enum_medDrugstore.藥品碼, enum_medDrugstore.藥品名稱, enum_medDrugstore.料號, enum_medDrugstore.中文名稱, enum_medDrugstore.包裝單位, enum_medDrugstore.藥庫庫存);
            this.sqL_DataGridView_驗收作業_藥品資料.RowDoubleClickEvent += SqL_DataGridView_驗收作業_藥品資料_RowDoubleClickEvent;
            this.sqL_DataGridView_驗收作業_藥品資料.DataGridRowsChangeRefEvent += SqL_DataGridView_驗收作業_藥品資料_DataGridRowsChangeRefEvent;

            this.plC_RJ_Button_驗收作業_新增驗收_自動生成.MouseDownEvent += PlC_RJ_Button_驗收作業_新增驗收_自動生成_MouseDownEvent;
            this.plC_RJ_Button_驗收作業_藥品資料_搜尋.MouseDownEvent += PlC_RJ_Button_驗收作業_藥品資料_搜尋_MouseDownEvent;
            this.plC_RJ_Button_驗收作業_驗收藥品清單_刪除.MouseDownEvent += PlC_RJ_Button_驗收作業_驗收藥品清單_刪除_MouseDownEvent;
            this.plC_RJ_Button_驗收作業_驗收藥品清單_送出.MouseDownEvent += PlC_RJ_Button_驗收作業_驗收藥品清單_送出_MouseDownEvent;
            this.plC_RJ_Button_驗收作業_新增驗收_建立測試單.MouseDownEvent += PlC_RJ_Button_驗收作業_新增驗收_建立測試單_MouseDownEvent;

            this.plC_UI_Init.Add_Method(sub_Program_驗收作業_新增驗收);
        }
        private bool flag_Program_驗收作業_新增驗收_Init = false;
        private void sub_Program_驗收作業_新增驗收()
        {
            if (this.plC_ScreenPage_Main.PageText == "驗收作業" && this.plC_ScreenPage_驗收作業.PageText == "新增驗收")
            {
                if (!flag_Program_驗收作業_新增驗收_Init)
                {
                    PlC_RJ_Button_驗收作業_新增驗收_自動生成_MouseDownEvent(null);
                    flag_Program_驗收作業_新增驗收_Init = true;
                }
            }
            else
            {
                flag_Program_驗收作業_新增驗收_Init = false;
            }
        }

        #region Function

        #endregion
        #region Event
        private void SqL_DataGridView_驗收作業_驗收藥品清單_Paint(object sender, PaintEventArgs e)
        {
            this.sqL_DataGridView_驗收作業_驗收藥品清單.RefreshGrid();
        }
        private void SqL_DataGridView_驗收作業_驗收藥品清單_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_驗收作業_驗收藥品清單());
        }
        private void SqL_DataGridView_驗收作業_驗收藥品清單_RowEndEditEvent1(object[] RowValue, int rowIndex, int colIndex, string value)
        {
            this.sqL_DataGridView_驗收作業_驗收藥品清單.ReplaceExtra(RowValue, true);
        }

        private void SqL_DataGridView_驗收作業_驗收藥品清單_CellValidatingEvent1(object[] RowValue, int rowIndex, int colIndex, string value, DataGridViewCellValidatingEventArgs e)
        {
            string 異動量 = value;
            if (異動量.StringToInt32() < 0)
            {
                MyMessageBox.ShowDialog("請輸入正確數字(大於'0')!");
                e.Cancel = true;
            }
        }
        private void SqL_DataGridView_驗收作業_藥品資料_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList = this.sqL_DataGridView_藥庫_藥品資料.RowsChangeFunction(RowsList);
        }
        private void SqL_DataGridView_驗收作業_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {

            string 藥品碼 = RowValue[(int)enum_medDrugstore.藥品碼].ObjectToString();
            string 料號 = RowValue[(int)enum_medDrugstore.料號].ObjectToString();
            string 藥品名稱 = RowValue[(int)enum_medDrugstore.藥品名稱].ObjectToString();
            string 藥品條碼1 = RowValue[(int)enum_medDrugstore.藥品條碼1].ObjectToString();
            string 藥品條碼2 = RowValue[(int)enum_medDrugstore.藥品條碼2].ObjectToString();
            string 理論值 = RowValue[(int)enum_medDrugstore.藥庫庫存].ObjectToString();
            List<object[]> list_驗收藥品清單 = this.sqL_DataGridView_驗收作業_驗收藥品清單.GetAllRows();
            list_驗收藥品清單 = list_驗收藥品清單.GetRows((int)enum_驗收作業_驗收藥品清單.藥品碼, 藥品碼);
            if (list_驗收藥品清單.Count != 0) return;
            object[] value = new object[new enum_驗收作業_驗收藥品清單().GetLength()];

            value[(int)enum_驗收作業_驗收藥品清單.GUID] = Guid.NewGuid().ToString();
            value[(int)enum_驗收作業_驗收藥品清單.藥品碼] = 藥品碼;
            value[(int)enum_驗收作業_驗收藥品清單.料號] = 料號;
            value[(int)enum_驗收作業_驗收藥品清單.藥品名稱] = 藥品名稱;
            value[(int)enum_驗收作業_驗收藥品清單.應收數量] = 理論值;
            value[(int)enum_驗收作業_驗收藥品清單.藥品條碼1] = 藥品條碼1;
            value[(int)enum_驗收作業_驗收藥品清單.藥品條碼2] = 藥品條碼2;

            this.sqL_DataGridView_驗收作業_驗收藥品清單.AddRow(value, true);
        }
        private void PlC_RJ_Button_驗收作業_新增驗收_自動生成_MouseDownEvent(MouseEventArgs mevent)
        {
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.藥庫.GetEnumName();
            returnData.TableName = "medicine_page_firstclass";
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inspection/new_IC_SN", returnData.JsonSerializationt());
            Console.WriteLine(json);
            this.Invoke(new Action(delegate
            {
                returnData = json.JsonDeserializet<returnData>();
                returnData.ServerName = dBConfigClass.Name;
                returnData.ServerType = enum_ServerSetting_Type.藥庫.GetEnumName();
                if (returnData == null) return;
                if (returnData.Code != 200) return;
                this.rJ_TextBox_驗收作業_新增驗收_驗收單號.Texts = $"{returnData.Value}";
            }));
        }
        private void PlC_RJ_Button_驗收作業_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收作業_藥品資料.SQL_GetAllRows(false);

            if (this.rJ_TextBox_驗收作業_藥品資料_藥碼搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_medDrugstore.藥品碼, this.rJ_TextBox_驗收作業_藥品資料_藥碼搜尋.Texts);
            }
            if (this.rJ_TextBox_驗收作業_藥品資料_藥名搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_medDrugstore.藥品名稱, this.rJ_TextBox_驗收作業_藥品資料_藥名搜尋.Texts);
            }
            if (this.rJ_TextBox_驗收作業_藥品資料_料號搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_medDrugstore.料號, this.rJ_TextBox_驗收作業_藥品資料_料號搜尋.Texts);
            }

            this.sqL_DataGridView_驗收作業_藥品資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_驗收作業_驗收藥品清單_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收作業_驗收藥品清單.Get_All_Checked_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            this.sqL_DataGridView_驗收作業_驗收藥品清單.DeleteExtra(list_value, true);
        }
        private void PlC_RJ_Button_驗收作業_驗收藥品清單_送出_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收藥品清單 = this.sqL_DataGridView_驗收作業_驗收藥品清單.GetAllRows();
            list_驗收藥品清單 = (from value in list_驗收藥品清單
                           where value[(int)enum_驗收作業_驗收藥品清單.應收數量].StringToInt32() > 0
                           select value).ToList();
            if (list_驗收藥品清單.Count == 0)
            {
                MyMessageBox.ShowDialog("未建立藥品驗收資料!");
                return;
            }
            if (rJ_TextBox_驗收作業_新增驗收_驗收單號.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("驗收單號空白!");
                return;
            }
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.藥庫.GetEnumName();
            returnData.TableName = "medicine_page_firstclass";

            inspectionClass.creat creat = new inspectionClass.creat();
            creat.建表人 = 登入者名稱;
            creat.驗收單號 = rJ_TextBox_驗收作業_新增驗收_驗收單號.Text;
            creat.驗收名稱 = rJ_TextBox_驗收作業_新增驗收_驗收名稱.Text;
            for (int i = 0; i < list_驗收藥品清單.Count; i++)
            {
                inspectionClass.content content = new inspectionClass.content();
                content.驗收單號 = creat.驗收單號;
                content.藥品碼 = list_驗收藥品清單[i][(int)enum_驗收作業_驗收藥品清單.藥品碼].ObjectToString();
                content.料號 = list_驗收藥品清單[i][(int)enum_驗收作業_驗收藥品清單.料號].ObjectToString();
                content.應收數量 = list_驗收藥品清單[i][(int)enum_驗收作業_驗收藥品清單.應收數量].ObjectToString();
                content.藥品條碼1 = list_驗收藥品清單[i][(int)enum_驗收作業_驗收藥品清單.藥品條碼1].ObjectToString();
                content.藥品條碼2 = list_驗收藥品清單[i][(int)enum_驗收作業_驗收藥品清單.藥品條碼2].ObjectToString();
                creat.Contents.Add(content);
            }
            returnData.Data = creat;
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inspection/creat_add", json_in);
            Console.WriteLine(json);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog($"{returnData.Result}");
            this.sqL_DataGridView_驗收作業_驗收藥品清單.ClearGrid();
        }
        private void PlC_RJ_Button_驗收作業_新增驗收_建立測試單_MouseDownEvent(MouseEventArgs mevent)
        {
            this.PlC_RJ_Button_驗收作業_新增驗收_自動生成_MouseDownEvent(null);
            string 驗收單號 = this.rJ_TextBox_驗收作業_新增驗收_驗收單號.Texts;
            string 驗收名稱 = rJ_TextBox_驗收作業_新增驗收_驗收名稱.Text;
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.藥庫.GetEnumName();
            returnData.TableName = "medicine_page_firstclass";
            inspectionClass.creat creat = new inspectionClass.creat();
            creat.建表人 = 登入者名稱;
            creat.驗收單號 = 驗收單號;
            creat.驗收名稱 = 驗收名稱;

            returnData.Data = creat;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.藥庫.GetEnumName();
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inspection/creat_auto_add", json_in);
            Console.WriteLine(json);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog($"{returnData.Result}");
            this.sqL_DataGridView_驗收作業_驗收藥品清單.ClearGrid();
        }
        #endregion

        private class ICP_驗收作業_驗收藥品清單 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 藥品碼0 = x[(int)enum_驗收作業_驗收藥品清單.藥品碼].ObjectToString();
                string 藥品碼1 = y[(int)enum_驗收作業_驗收藥品清單.藥品碼].ObjectToString();
                int temp = 藥品碼0.CompareTo(藥品碼1);
                return temp;
            }
        }
    }
}
