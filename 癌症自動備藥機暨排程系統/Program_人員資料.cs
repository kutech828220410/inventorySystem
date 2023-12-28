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

namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Program_人員資料_Init()
        {
            string url = $"{API_Server}/api/person_page/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"人員資料表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }
            this.sqL_DataGridView_人員資料.Init(table);
            this.sqL_DataGridView_人員資料.Set_ColumnVisible(false, new enum_人員資料().GetEnumNames());
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.ID);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.姓名);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_人員資料.性別);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_人員資料.權限等級);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.卡號);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.一維條碼);


            this.plC_RJ_Button_人員資料_資料搜尋_姓名.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_姓名_MouseDownEvent;
            this.plC_RJ_Button_人員資料_資料搜尋_ID.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_ID_MouseDownEvent;
            this.plC_RJ_Button_人員資料_資料搜尋_卡號.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_卡號_MouseDownEvent;
            this.plC_RJ_Button_人員資料_資料搜尋_一維條碼.MouseDownEvent += PlC_RJ_Button_人員資料_資料搜尋_一維條碼_MouseDownEvent;
            plC_UI_Init.Add_Method(Program_人員資料);
        }

   

        private void Program_人員資料()
        {

           
        }

        #region Function

        #endregion
        #region Event
        private void PlC_RJ_Button_人員資料_資料搜尋_一維條碼_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = this.plC_RJ_Button_人員資料_資料搜尋_一維條碼.Texts;
            if(text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("搜尋條件空白", 1500);
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_value_buf = list_value.GetRows((int)enum_人員資料.一維條碼, text);
            this.sqL_DataGridView_人員資料.RefreshGrid(list_value_buf);
            

        }
        private void PlC_RJ_Button_人員資料_資料搜尋_卡號_MouseDownEvent(MouseEventArgs mevent)
        {

        }
        private void PlC_RJ_Button_人員資料_資料搜尋_ID_MouseDownEvent(MouseEventArgs mevent)
        {

        }
        private void PlC_RJ_Button_人員資料_資料搜尋_姓名_MouseDownEvent(MouseEventArgs mevent)
        {

        }
        #endregion
    }
}
