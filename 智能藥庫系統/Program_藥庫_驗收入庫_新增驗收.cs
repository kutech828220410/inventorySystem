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
        public enum enum_驗收入庫_驗收藥品清單
        {
            GUID,
            藥品碼,
            料號,
            藥品名稱,
            藥品條碼1,
            藥品條碼2,
            驗收量,

        }
        private void sub_Program_藥庫_驗收入庫_新增驗收_Init()
        {
            this.sqL_DataGridView_驗收入庫_新增驗收.Init(this.sqL_DataGridView_藥局_藥品資料);
            this.sqL_DataGridView_驗收入庫_新增驗收.Set_ColumnVisible(false, new enum_藥局_藥品資料().GetEnumNames());
            this.sqL_DataGridView_驗收入庫_新增驗收.Set_ColumnVisible(true, enum_藥局_藥品資料.藥品碼, enum_藥局_藥品資料.藥品名稱, enum_藥局_藥品資料.料號, enum_藥局_藥品資料.中文名稱, enum_藥局_藥品資料.包裝單位);
            this.sqL_DataGridView_驗收入庫_新增驗收.RowDoubleClickEvent += SqL_DataGridView_驗收入庫_新增驗收_RowDoubleClickEvent;

            this.sqL_DataGridView_驗收入庫_驗收藥品清單.Init();
            this.sqL_DataGridView_驗收入庫_驗收藥品清單.DataGridRowsChangeRefEvent += SqL_DataGridView_驗收入庫_驗收藥品清單_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_驗收入庫_驗收藥品清單.CellValidatingEvent += SqL_DataGridView_驗收入庫_驗收藥品清單_CellValidatingEvent;
            this.sqL_DataGridView_驗收入庫_驗收藥品清單.RowEndEditEvent += SqL_DataGridView_驗收入庫_驗收藥品清單_RowEndEditEvent;


            this.plC_RJ_Button_驗收入庫_新增驗收_自動生成.MouseDownEvent += PlC_RJ_Button_驗收入庫_新增驗收_自動生成_MouseDownEvent;
            this.plC_RJ_Button_驗收入庫_新增驗收_搜尋.MouseDownEvent += PlC_RJ_Button_驗收入庫_新增驗收_搜尋_MouseDownEvent;
            this.plC_RJ_Button_驗收入庫_新增驗收_刪除選取資料.MouseDownEvent += PlC_RJ_Button_驗收入庫_新增驗收_刪除選取資料_MouseDownEvent;
            this.plC_RJ_Button_驗收入庫_新增驗收_送出.MouseDownEvent += PlC_RJ_Button_驗收入庫_新增驗收_送出_MouseDownEvent;
            this.plC_RJ_Button_驗收入庫_新增驗收_建立測試單.MouseDownEvent += PlC_RJ_Button_驗收入庫_新增驗收_建立測試單_MouseDownEvent;
            this.plC_UI_Init.Add_Method(sub_Program_藥庫_驗收入庫_新增驗收);
        }

  

        private bool flag_藥庫_驗收入庫_新增驗收 = false;
        private void sub_Program_藥庫_驗收入庫_新增驗收()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥庫" && this.plC_ScreenPage_藥庫.PageText == "驗收入庫" && this.plC_ScreenPage_藥庫_驗收入庫.PageText == "新增驗收")
            {
                if (!this.flag_藥庫_驗收入庫_新增驗收)
                {
                    PlC_RJ_Button_驗收入庫_新增驗收_自動生成_MouseDownEvent(null);

                    this.flag_藥庫_驗收入庫_新增驗收 = true;
                }

            }
            else
            {
                this.flag_藥庫_驗收入庫_新增驗收 = false;
            }
        }

        #region Function

        #endregion
        #region Event
        private void SqL_DataGridView_驗收入庫_新增驗收_RowDoubleClickEvent(object[] RowValue)
        {
            string 藥品碼 = RowValue[(int)enum_藥局_藥品資料.藥品碼].ObjectToString();
            string 料號 = RowValue[(int)enum_藥局_藥品資料.料號].ObjectToString();
            string 藥品名稱 = RowValue[(int)enum_藥局_藥品資料.藥品名稱].ObjectToString();
            string 藥品條碼1 = RowValue[(int)enum_藥局_藥品資料.藥品條碼1].ObjectToString();
            string 藥品條碼2 = RowValue[(int)enum_藥局_藥品資料.藥品條碼2].ObjectToString();
            string 驗收量 = "0";
            List<object[]> list_驗收藥品清單 = this.sqL_DataGridView_驗收入庫_驗收藥品清單.GetAllRows();
            list_驗收藥品清單 = list_驗收藥品清單.GetRows((int)enum_驗收入庫_驗收藥品清單.藥品碼, 藥品碼);
            if (list_驗收藥品清單.Count != 0) return;
            object[] value = new object[new enum_驗收入庫_驗收藥品清單().GetLength()];

            value[(int)enum_驗收入庫_驗收藥品清單.GUID] = Guid.NewGuid().ToString();
            value[(int)enum_驗收入庫_驗收藥品清單.藥品碼] = 藥品碼;
            value[(int)enum_驗收入庫_驗收藥品清單.料號] = 料號;
            value[(int)enum_驗收入庫_驗收藥品清單.藥品名稱] = 藥品名稱;
            value[(int)enum_驗收入庫_驗收藥品清單.驗收量] = 驗收量;
            value[(int)enum_驗收入庫_驗收藥品清單.藥品條碼1] = 藥品條碼1;
            value[(int)enum_驗收入庫_驗收藥品清單.藥品條碼2] = 藥品條碼2;

            this.sqL_DataGridView_驗收入庫_驗收藥品清單.AddRow(value, true);


        }
        private void SqL_DataGridView_驗收入庫_驗收藥品清單_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_驗收入庫_驗收藥品清單());
        }
        private void SqL_DataGridView_驗收入庫_驗收藥品清單_RowEndEditEvent(object[] RowValue, int rowIndex, int colIndex, string value)
        {
            this.sqL_DataGridView_驗收入庫_驗收藥品清單.ReplaceExtra(RowValue, true);
        }

        private void SqL_DataGridView_驗收入庫_驗收藥品清單_CellValidatingEvent(object[] RowValue, int rowIndex, int colIndex, string value, DataGridViewCellValidatingEventArgs e)
        {
            string 異動量 = value;
            if (異動量.StringToInt32() < 0)
            {
                MyMessageBox.ShowDialog("請輸入正確數字(大於'0')!");
                e.Cancel = true;
            }
        }
        private void PlC_RJ_Button_驗收入庫_新增驗收_自動生成_MouseDownEvent(MouseEventArgs mevent)
        {
            string json = Basic.Net.WEBApiGet($"{dBConfigClass.Inspection_ApiURL}/new_ACPT_SN");
            Console.WriteLine(json);
            this.Invoke(new Action(delegate
            {      
                returnData returnData = json.JsonDeserializet<returnData>();
                if (returnData == null) return;
                if (returnData.Code != 200) return;
                this.rJ_TextBox_驗收入庫_新增驗收_驗收單號.Texts = $"{returnData.Value}";
            }));
        }
        private void PlC_RJ_Button_驗收入庫_新增驗收_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥局_藥品資料.SQL_GetAllRows(false);
            this.sqL_DataGridView_藥局_藥品資料.RowsChangeFunction(list_value);

            if (this.rJ_TextBox_驗收入庫_新增驗收_藥碼搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥局_藥品資料.藥品碼, this.rJ_TextBox_驗收入庫_新增驗收_藥碼搜尋.Texts);
            }
            if (this.rJ_TextBox_驗收入庫_新增驗收_藥名搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥局_藥品資料.藥品名稱, this.rJ_TextBox_驗收入庫_新增驗收_藥名搜尋.Texts);
            }
            if (this.rJ_TextBox_驗收入庫_新增驗收_料號搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥局_藥品資料.料號, this.rJ_TextBox_驗收入庫_新增驗收_料號搜尋.Texts);
            }
            
            this.sqL_DataGridView_驗收入庫_新增驗收.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_驗收入庫_新增驗收_刪除選取資料_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_驗收入庫_驗收藥品清單.Get_All_Checked_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            this.sqL_DataGridView_驗收入庫_驗收藥品清單.DeleteExtra(list_value, true);
        }
        private void PlC_RJ_Button_驗收入庫_新增驗收_送出_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收藥品清單 = this.sqL_DataGridView_驗收入庫_驗收藥品清單.GetAllRows();
            list_驗收藥品清單 = (from value in list_驗收藥品清單
                           where value[(int)enum_驗收入庫_驗收藥品清單.驗收量].StringToInt32() > 0
                           select value).ToList();
            if (list_驗收藥品清單.Count == 0)
            {
                MyMessageBox.ShowDialog("未建立藥品驗收資料!");
                return;
            }
            if(rJ_TextBox_驗收入庫_新增驗收_驗收單號.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("驗收單號空白!");
                return;
            }
            returnData returnData = new returnData();
            inspectionClass.creat creat = new inspectionClass.creat();
            creat.建表人 = 登入者名稱;
            creat.請購單號 = rJ_TextBox_驗收入庫_新增驗收_驗收單號.Text;
            creat.驗收單號 = rJ_TextBox_驗收入庫_新增驗收_驗收單號.Text;
            for (int i = 0; i < list_驗收藥品清單.Count; i++)
            {
                inspectionClass.content content = new inspectionClass.content();
                content.請購單號 = creat.請購單號;
                content.驗收單號 = creat.驗收單號;
                content.藥品碼 = list_驗收藥品清單[i][(int)enum_驗收入庫_驗收藥品清單.藥品碼].ObjectToString();
                content.料號 = list_驗收藥品清單[i][(int)enum_驗收入庫_驗收藥品清單.料號].ObjectToString();
                content.應收數量 = list_驗收藥品清單[i][(int)enum_驗收入庫_驗收藥品清單.驗收量].ObjectToString();
                content.藥品條碼1 = list_驗收藥品清單[i][(int)enum_驗收入庫_驗收藥品清單.藥品條碼1].ObjectToString();
                content.藥品條碼2 = list_驗收藥品清單[i][(int)enum_驗收入庫_驗收藥品清單.藥品條碼2].ObjectToString();
                creat.Contents.Add(content);
            }
            returnData.Data = creat;
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Inspection_ApiURL}/creat_add", returnData.JsonSerializationt());
            Console.WriteLine(json);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog($"{returnData.Result}");
            this.sqL_DataGridView_驗收入庫_驗收藥品清單.ClearGrid();
        }
        private void PlC_RJ_Button_驗收入庫_新增驗收_建立測試單_MouseDownEvent(MouseEventArgs mevent)
        {
            this.PlC_RJ_Button_驗收入庫_新增驗收_自動生成_MouseDownEvent(null);
            List<object[]> list_value = this.sqL_DataGridView_藥局_藥品資料.SQL_GetAllRows(false);
            string 驗收單號 = this.rJ_TextBox_驗收入庫_新增驗收_驗收單號.Texts;
            returnData returnData = new returnData();
            inspectionClass.creat creat = new inspectionClass.creat();
            creat.建表人 = 登入者名稱;
            creat.驗收單號 = 驗收單號;
            creat.請購單號 = 驗收單號;
            for (int i = 0; i < list_value.Count; i++)
            {
                inspectionClass.content content = new inspectionClass.content();
                content.請購單號 = creat.請購單號;
                content.驗收單號 = creat.驗收單號;
                content.藥品碼 = list_value[i][(int)enum_藥局_藥品資料.藥品碼].ObjectToString();
                content.料號 = list_value[i][(int)enum_藥局_藥品資料.料號].ObjectToString();
                content.應收數量 = (i + 1).ToString();
                content.藥品條碼1 = list_value[i][(int)enum_藥局_藥品資料.藥品條碼1].ObjectToString();
                content.藥品條碼2 = list_value[i][(int)enum_藥局_藥品資料.藥品條碼2].ObjectToString();
                creat.Contents.Add(content);
            }
            returnData.Data = creat;
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Inspection_ApiURL}/creat_add", returnData.JsonSerializationt());
            Console.WriteLine(json);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog($"{returnData.Result}");
            this.sqL_DataGridView_驗收入庫_驗收藥品清單.ClearGrid();
        }
        #endregion

        private class ICP_驗收入庫_驗收藥品清單 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 藥品碼0 = x[(int)enum_驗收入庫_驗收藥品清單.藥品碼].ObjectToString();
                string 藥品碼1 = y[(int)enum_驗收入庫_驗收藥品清單.藥品碼].ObjectToString();
                int temp = 藥品碼0.CompareTo(藥品碼1);
                return temp;
            }
        }
    }
}
