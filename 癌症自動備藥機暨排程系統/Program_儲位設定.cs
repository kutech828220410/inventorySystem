using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;
using H_Pannel_lib;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        public enum enum_儲位列表
        {
            GUID,
            IP,
            藥碼,
            藥名,
            單位,
            包裝量,
            庫存,            
        }
        private void Program_儲位設定_Init()
        {
            sqL_DataGridView_儲位設定_藥品搜尋.Init(sqL_DataGridView_藥檔資料);
            sqL_DataGridView_儲位設定_藥品搜尋.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
            sqL_DataGridView_儲位設定_藥品搜尋.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品碼);
            sqL_DataGridView_儲位設定_藥品搜尋.Set_ColumnWidth(750, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
            sqL_DataGridView_儲位設定_藥品搜尋.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.包裝單位);

            this.plC_RJ_Button_儲位設定_藥品搜尋_藥碼搜尋.MouseDownEvent += PlC_RJ_Button_儲位設定_藥品搜尋_藥碼搜尋_MouseDownEvent;
            this.plC_RJ_Button_儲位設定_藥品搜尋_藥名搜尋.MouseDownEvent += PlC_RJ_Button_儲位設定_藥品搜尋_藥名搜尋_MouseDownEvent;


            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("IP", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("藥碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("藥名", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("單位", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("包裝量", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("庫存", Table.StringType.VARCHAR, 50, Table.IndexType.None);

            this.sqL_DataGridView_儲位列表.Init(table);
            sqL_DataGridView_儲位列表.Set_ColumnVisible(false, new enum_儲位列表().GetEnumNames());
            sqL_DataGridView_儲位列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_儲位列表.IP);
            sqL_DataGridView_儲位列表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位列表.藥碼);
            sqL_DataGridView_儲位列表.Set_ColumnWidth(450, DataGridViewContentAlignment.MiddleLeft, enum_儲位列表.藥名);
            sqL_DataGridView_儲位列表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位列表.單位);
            sqL_DataGridView_儲位列表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位列表.包裝量);
            sqL_DataGridView_儲位列表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位列表.庫存);

            plC_RJ_Button_儲位設定_儲位列表_重新整理.MouseDownEvent += PlC_RJ_Button_儲位設定_儲位列表_重新整理_MouseDownEvent;   

            plC_UI_Init.Add_Method(Program_儲位設定);
        }




        #region Function

        #endregion
        #region Event
        private void PlC_RJ_Button_儲位設定_藥品搜尋_藥名搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_儲位設定_藥品搜尋_藥名.Texts.StringIsEmpty() == true)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋欄位不得空白", 2000, 0, -200, Color.DarkRed);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = sqL_DataGridView_儲位設定_藥品搜尋.SQL_GetAllRows(false);
            list_value = list_value.GetRowsByLike((int)enum_雲端藥檔.藥品名稱, rJ_TextBox_儲位設定_藥品搜尋_藥名.Texts);
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 2000, 0, -200, Color.DarkRed);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            sqL_DataGridView_儲位設定_藥品搜尋.RefreshGrid(list_value);

        }
        private void PlC_RJ_Button_儲位設定_藥品搜尋_藥碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_儲位設定_藥品搜尋_藥碼.Texts.StringIsEmpty() == true)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋欄位不得空白", 2000, 0, -200, Color.DarkRed);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = sqL_DataGridView_儲位設定_藥品搜尋.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_雲端藥檔.藥品碼, rJ_TextBox_儲位設定_藥品搜尋_藥碼.Texts);
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 2000, 0, -200, Color.DarkRed);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            sqL_DataGridView_儲位設定_藥品搜尋.RefreshGrid(list_value);
        }

        private void PlC_RJ_Button_儲位設定_儲位列表_重新整理_MouseDownEvent(MouseEventArgs mevent)
        {
            List<Storage> storages = storageUI_EPD_266.SQL_GetAllStorage();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < storages.Count; i++)
            {
                string GUID = storages[i].GUID;
                string IP = storages[i].IP;
                string 藥碼 = storages[i].Code;
                string 藥名 = storages[i].Name;
                string 單位 = storages[i].Package;
                string 包裝量 = storages[i].Min_Package_Num;
                string 庫存 = storages[i].Inventory;
                object[] value = new object[new enum_儲位列表().GetLength()];
                value[(int)enum_儲位列表.GUID] = GUID;
                value[(int)enum_儲位列表.IP] = IP;
                value[(int)enum_儲位列表.藥碼] = 藥碼;
                value[(int)enum_儲位列表.藥名] = 藥名;
                value[(int)enum_儲位列表.單位] = 單位;
                value[(int)enum_儲位列表.包裝量] = 包裝量;
                value[(int)enum_儲位列表.庫存] = 庫存;
                list_value.Add(value);
            }
            sqL_DataGridView_儲位列表.RefreshGrid(list_value);
        }
        #endregion
        private void Program_儲位設定()
        {

        }
    }
}
