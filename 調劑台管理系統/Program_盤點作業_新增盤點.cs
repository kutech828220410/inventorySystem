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
namespace 調劑台管理系統
{

    public enum enum_盤點作業_盤點藥品清單
    {
        GUID,
        藥品碼,
        料號,
        藥品名稱,
        藥品條碼1,
        藥品條碼2,
        理論值,

    }

    public partial class Form1 : Form
    {
        private void sub_Program_盤點作業_新增盤點_Init()
        {
            this.sqL_DataGridView_盤點作業_盤點藥品清單.Init();
            this.sqL_DataGridView_盤點作業_盤點藥品清單.DataGridRowsChangeRefEvent += SqL_DataGridView_盤點作業_盤點藥品清單_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_盤點作業_盤點藥品清單.RowEndEditEvent += SqL_DataGridView_盤點作業_盤點藥品清單_RowEndEditEvent1;
            this.sqL_DataGridView_盤點作業_盤點藥品清單.CellValidatingEvent += SqL_DataGridView_盤點作業_盤點藥品清單_CellValidatingEvent1;
            this.sqL_DataGridView_盤點作業_盤點藥品清單.Paint += SqL_DataGridView_盤點作業_盤點藥品清單_Paint;

            this.sqL_DataGridView_盤點作業_藥品資料.Init(this.sqL_DataGridView_藥品資料_藥檔資料);
            this.sqL_DataGridView_盤點作業_藥品資料.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
            this.sqL_DataGridView_盤點作業_藥品資料.Set_ColumnVisible(true, enum_藥品資料_藥檔資料.藥品碼, enum_藥品資料_藥檔資料.藥品名稱, enum_藥品資料_藥檔資料.料號, enum_藥品資料_藥檔資料.中文名稱, enum_藥品資料_藥檔資料.包裝單位, enum_藥品資料_藥檔資料.庫存);
            this.sqL_DataGridView_盤點作業_藥品資料.RowDoubleClickEvent += SqL_DataGridView_盤點作業_藥品資料_RowDoubleClickEvent;
            this.sqL_DataGridView_盤點作業_藥品資料.DataGridRowsChangeRefEvent += SqL_DataGridView_盤點作業_藥品資料_DataGridRowsChangeRefEvent;

            this.plC_RJ_Button_盤點作業_新增盤點_自動生成.MouseDownEvent += PlC_RJ_Button_盤點作業_新增盤點_自動生成_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_藥品資料_搜尋.MouseDownEvent += PlC_RJ_Button_盤點作業_藥品資料_搜尋_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_盤點藥品清單_刪除.MouseDownEvent += PlC_RJ_Button_盤點作業_盤點藥品清單_刪除_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_盤點藥品清單_送出.MouseDownEvent += PlC_RJ_Button_盤點作業_盤點藥品清單_送出_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_新增盤點_建立測試單.MouseDownEvent += PlC_RJ_Button_盤點作業_新增盤點_建立測試單_MouseDownEvent;

            this.plC_UI_Init.Add_Method(sub_Program_盤點作業_新增盤點);
        }

  

        private bool flag_Program_盤點作業_新增盤點_Init = false;
        private void sub_Program_盤點作業_新增盤點()
        {
            if (this.plC_ScreenPage_Main.PageText == "盤點作業" && this.plC_ScreenPage_盤點作業.PageText == "新增盤點")
            {
                if (!flag_Program_盤點作業_新增盤點_Init)
                {
                    PlC_RJ_Button_盤點作業_新增盤點_自動生成_MouseDownEvent(null);
                    flag_Program_盤點作業_新增盤點_Init = true;
                }
            }
            else
            {
                flag_Program_盤點作業_新增盤點_Init = false;
            }
        }

        #region Function

        #endregion
        #region Event
        private void SqL_DataGridView_盤點作業_盤點藥品清單_Paint(object sender, PaintEventArgs e)
        {
            this.sqL_DataGridView_盤點作業_盤點藥品清單.RefreshGrid();
        }
        private void SqL_DataGridView_盤點作業_盤點藥品清單_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_盤點作業_盤點藥品清單());
        }
        private void SqL_DataGridView_盤點作業_盤點藥品清單_RowEndEditEvent1(object[] RowValue, int rowIndex, int colIndex, string value)
        {
            this.sqL_DataGridView_盤點作業_盤點藥品清單.ReplaceExtra(RowValue, true);
        }

        private void SqL_DataGridView_盤點作業_盤點藥品清單_CellValidatingEvent1(object[] RowValue, int rowIndex, int colIndex, string value, DataGridViewCellValidatingEventArgs e)
        {
            string 異動量 = value;
            if (異動量.StringToInt32() < 0)
            {
                MyMessageBox.ShowDialog("請輸入正確數字(大於'0')!");
                e.Cancel = true;
            }
        }
        private void SqL_DataGridView_盤點作業_藥品資料_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList = this.sqL_DataGridView_藥品資料_藥檔資料.RowsChangeFunction(RowsList);
        }
        private void SqL_DataGridView_盤點作業_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {
            
            string 藥品碼 = RowValue[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            string 料號 = RowValue[(int)enum_藥品資料_藥檔資料.料號].ObjectToString();
            string 藥品名稱 = RowValue[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
            string 藥品條碼1 = RowValue[(int)enum_藥品資料_藥檔資料.藥品條碼1].ObjectToString();
            string 藥品條碼2 = RowValue[(int)enum_藥品資料_藥檔資料.藥品條碼2].ObjectToString();
            string 理論值 = RowValue[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString();
            List<object[]> list_盤點藥品清單 = this.sqL_DataGridView_盤點作業_盤點藥品清單.GetAllRows();
            list_盤點藥品清單 = list_盤點藥品清單.GetRows((int)enum_盤點作業_盤點藥品清單.藥品碼, 藥品碼);
            if (list_盤點藥品清單.Count != 0) return;
            object[] value = new object[new enum_盤點作業_盤點藥品清單().GetLength()];

            value[(int)enum_盤點作業_盤點藥品清單.GUID] = Guid.NewGuid().ToString();
            value[(int)enum_盤點作業_盤點藥品清單.藥品碼] = 藥品碼;
            value[(int)enum_盤點作業_盤點藥品清單.料號] = 料號;
            value[(int)enum_盤點作業_盤點藥品清單.藥品名稱] = 藥品名稱;
            value[(int)enum_盤點作業_盤點藥品清單.理論值] = 理論值;
            value[(int)enum_盤點作業_盤點藥品清單.藥品條碼1] = 藥品條碼1;
            value[(int)enum_盤點作業_盤點藥品清單.藥品條碼2] = 藥品條碼2;

            this.sqL_DataGridView_盤點作業_盤點藥品清單.AddRow(value, true);
        }
        private void PlC_RJ_Button_盤點作業_新增盤點_自動生成_MouseDownEvent(MouseEventArgs mevent)
        {
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/new_IC_SN", json_in);
            Console.WriteLine(json);
            this.Invoke(new Action(delegate
            {
                returnData = json.JsonDeserializet<returnData>();
                if (returnData == null) return;
                if (returnData.Code != 200) return;
                this.rJ_TextBox_盤點作業_新增盤點_盤點單號.Texts = $"{returnData.Value}";
            }));
        }
        private void PlC_RJ_Button_盤點作業_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_盤點作業_藥品資料.SQL_GetAllRows(false);

            if (this.rJ_TextBox_盤點作業_藥品資料_藥碼搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.藥品碼, this.rJ_TextBox_盤點作業_藥品資料_藥碼搜尋.Texts);
            }
            if (this.rJ_TextBox_盤點作業_藥品資料_藥名搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.藥品名稱, this.rJ_TextBox_盤點作業_藥品資料_藥名搜尋.Texts);
            }
            if (this.rJ_TextBox_盤點作業_藥品資料_料號搜尋.Texts.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsStartWithByLike((int)enum_藥品資料_藥檔資料.料號, this.rJ_TextBox_盤點作業_藥品資料_料號搜尋.Texts);
            }

            this.sqL_DataGridView_盤點作業_藥品資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_盤點作業_盤點藥品清單_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_盤點作業_藥品資料.Get_All_Checked_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            this.sqL_DataGridView_盤點作業_藥品資料.DeleteExtra(list_value, true);
        }
        private void PlC_RJ_Button_盤點作業_盤點藥品清單_送出_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_盤點藥品清單 = this.sqL_DataGridView_盤點作業_盤點藥品清單.GetAllRows();
            list_盤點藥品清單 = (from value in list_盤點藥品清單
                           where value[(int)enum_盤點作業_盤點藥品清單.理論值].StringToInt32() > 0
                           select value).ToList();
            if (list_盤點藥品清單.Count == 0)
            {
                MyMessageBox.ShowDialog("未建立藥品盤點資料!");
                return;
            }
            if (rJ_TextBox_盤點作業_新增盤點_盤點單號.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("盤點單號空白!");
                return;
            }
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            inventoryClass.creat creat = new inventoryClass.creat();
            creat.建表人 = 登入者名稱;
            creat.盤點單號 = rJ_TextBox_盤點作業_新增盤點_盤點單號.Text;
            creat.盤點名稱 =  rJ_TextBox_盤點作業_新增盤點_盤點名稱.Text;
            for (int i = 0; i < list_盤點藥品清單.Count; i++)
            {
                inventoryClass.content content = new inventoryClass.content();
                content.盤點單號 = creat.盤點單號;
                content.藥品碼 = list_盤點藥品清單[i][(int)enum_盤點作業_盤點藥品清單.藥品碼].ObjectToString();
                content.料號 = list_盤點藥品清單[i][(int)enum_盤點作業_盤點藥品清單.料號].ObjectToString();
                content.理論值 = list_盤點藥品清單[i][(int)enum_盤點作業_盤點藥品清單.理論值].ObjectToString();
                content.藥品條碼1 = list_盤點藥品清單[i][(int)enum_盤點作業_盤點藥品清單.藥品條碼1].ObjectToString();
                content.藥品條碼2 = list_盤點藥品清單[i][(int)enum_盤點作業_盤點藥品清單.藥品條碼2].ObjectToString();
                creat.Contents.Add(content);
            }
            returnData.Data = creat;
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/creat_add", json_in);
            Console.WriteLine(json);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog($"{returnData.Result}");
            this.sqL_DataGridView_盤點作業_盤點藥品清單.ClearGrid();
        }
        private void PlC_RJ_Button_盤點作業_新增盤點_建立測試單_MouseDownEvent(MouseEventArgs mevent)
        {
            this.PlC_RJ_Button_盤點作業_新增盤點_自動生成_MouseDownEvent(null);
            string 盤點單號 = this.rJ_TextBox_盤點作業_新增盤點_盤點單號.Texts;
            string 盤點名稱 = rJ_TextBox_盤點作業_新增盤點_盤點名稱.Text;
            returnData returnData = new returnData();
            inventoryClass.creat creat = new inventoryClass.creat();
            creat.建表人 = 登入者名稱;
            creat.盤點單號 = 盤點單號;
            creat.盤點名稱 = 盤點名稱;

            returnData.Data = creat;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/creat_auto_add", json_in);
            Console.WriteLine(json);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog($"{returnData.Result}");
            this.sqL_DataGridView_盤點作業_盤點藥品清單.ClearGrid();
        }
        #endregion

        private class ICP_盤點作業_盤點藥品清單 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 藥品碼0 = x[(int)enum_盤點作業_盤點藥品清單.藥品碼].ObjectToString();
                string 藥品碼1 = y[(int)enum_盤點作業_盤點藥品清單.藥品碼].ObjectToString();
                int temp = 藥品碼0.CompareTo(藥品碼1);
                return temp;
            }
        }
    }
}
