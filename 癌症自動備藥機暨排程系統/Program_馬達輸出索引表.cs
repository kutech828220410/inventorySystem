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
using DeltaMotor485;
namespace 癌症自動備藥機暨排程系統
{
    public enum enum_CMPM_StorageConfig_匯出
    {
        IP,
        鎖控輸出索引,
        鎖控輸入索引,
        出料馬達輸出索引,
        出料馬達輸入索引,
        出料馬達輸入延遲時間,
        出料位置X,
        出料位置Y,
        藥盒方位,
    }
    public enum enum_CMPM_StorageConfig_匯入
    {
        IP,
        鎖控輸出索引,
        鎖控輸入索引,
        出料馬達輸出索引,
        出料馬達輸入索引,
        出料馬達輸入延遲時間,
        出料位置X,
        出料位置Y,
        藥盒方位,
    }
    public partial class Main_Form : Form
    {
        private void Program_馬達輸出索引表_Init()
        {
            string url = $"{API_Server}/api/CPMP_StorageConfig/init";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);

            Table table = json.JsonDeserializet<Table>();
            this.sqL_DataGridView_馬達輸出索引表.Server = table.Server;
            this.sqL_DataGridView_馬達輸出索引表.DataBaseName = table.DBName;
            this.sqL_DataGridView_馬達輸出索引表.UserName = table.Username;
            this.sqL_DataGridView_馬達輸出索引表.Password = table.Password;
            this.sqL_DataGridView_馬達輸出索引表.Port = table.Port.StringToUInt32();
            this.sqL_DataGridView_馬達輸出索引表.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_馬達輸出索引表.MouseDown += SqL_DataGridView_馬達輸出索引表_MouseDown;
            this.sqL_DataGridView_馬達輸出索引表.Init(table);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnVisible(false, new enum_CMPM_StorageConfig().GetEnumNames());
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.IP);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.鎖控輸出索引);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.鎖控輸入索引);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.出料馬達輸出索引);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.出料馬達輸入索引);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.出料馬達輸入延遲時間);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.出料位置X);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.出料位置Y);
            this.sqL_DataGridView_馬達輸出索引表.Set_ColumnWidth(80, enum_CMPM_StorageConfig.藥盒方位);


            this.plC_RJ_Button_馬達輸出索引表_匯出.MouseDownEvent += PlC_RJ_Button_馬達輸出索引表_匯出_MouseDownEvent;
            this.plC_RJ_Button_馬達輸出索引表_匯入.MouseDownEvent += PlC_RJ_Button_馬達輸出索引表_匯入_MouseDownEvent;

            this.plC_UI_Init.Add_Method(Program_馬達輸出索引表);
        }

        private void Program_馬達輸出索引表()
        {

        }

        #region Event
        private void SqL_DataGridView_馬達輸出索引表_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_馬達輸出索引表.SQL_GetAllRows(true);
        }
        private void PlC_RJ_Button_馬達輸出索引表_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;
                List<object[]> list_value = this.sqL_DataGridView_馬達輸出索引表.SQL_GetAllRows(false);
                DataTable dataTable = list_value.ToDataTable(new enum_CMPM_StorageConfig());
                dataTable = dataTable.ReorderTable(new enum_CMPM_StorageConfig_匯出());

                dataTable.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);

                MyMessageBox.ShowDialog("匯出完成!");

            }));
        }
        private void PlC_RJ_Button_馬達輸出索引表_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.openFileDialog_LoadExcel.ShowDialog() != DialogResult.OK) return;
                DataTable dataTable = MyOffice.ExcelClass.NPOI_LoadFile(this.openFileDialog_LoadExcel.FileName);
                List<object[]> list_匯入資料 = dataTable.DataTableToRowList();
                List<object[]> list_匯入資料_buf = new List<object[]>();
                List<object[]> list_馬達輸出索引表 = this.sqL_DataGridView_馬達輸出索引表.SQL_GetAllRows(false);
                List<object[]> list_馬達輸出索引表_buf = new List<object[]>();
                List<object[]> list_馬達輸出索引表_add = new List<object[]>();
                List<object[]> list_馬達輸出索引表_replace = new List<object[]>();
                for (int i = 0; i < list_匯入資料.Count; i++)
                {
                    string IP = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.IP].ObjectToString();
                    list_馬達輸出索引表_buf = list_馬達輸出索引表.GetRows((int)enum_CMPM_StorageConfig.IP, IP);
                    if(list_馬達輸出索引表_buf.Count == 0)
                    {
                        object[] value = new object[new enum_CMPM_StorageConfig().GetLength()];
                        value[(int)enum_CMPM_StorageConfig.GUID] = Guid.NewGuid().ToString();
                        value[(int)enum_CMPM_StorageConfig.IP] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.IP].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料位置X] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料位置X].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料位置Y] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料位置Y].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料馬達輸入延遲時間] = "100";
                        value[(int)enum_CMPM_StorageConfig.出料馬達輸入狀態] = "False";
                        value[(int)enum_CMPM_StorageConfig.出料馬達輸入索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料馬達輸入索引].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料馬達輸出索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料馬達輸出索引].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料馬達輸出觸發] = "False";
                        value[(int)enum_CMPM_StorageConfig.藥盒方位] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.藥盒方位].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.鎖控輸入狀態] = "False";
                        value[(int)enum_CMPM_StorageConfig.鎖控輸入索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.鎖控輸入索引].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.鎖控輸出索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.鎖控輸出索引].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.鎖控輸出觸發] = "False";
                        list_馬達輸出索引表_add.Add(value);
                    }
                    else
                    {
                        object[] value = list_馬達輸出索引表_buf[0];
                        value[(int)enum_CMPM_StorageConfig.IP] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.IP].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料位置X] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料位置X].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料位置Y] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料位置Y].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料馬達輸入索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料馬達輸入索引].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.出料馬達輸出索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.出料馬達輸出索引].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.藥盒方位] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.藥盒方位].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.鎖控輸入索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.鎖控輸入索引].ObjectToString();
                        value[(int)enum_CMPM_StorageConfig.鎖控輸出索引] = list_匯入資料[i][(int)enum_CMPM_StorageConfig_匯入.鎖控輸出索引].ObjectToString();
                        list_馬達輸出索引表_replace.Add(value);
                    }

                }
                this.sqL_DataGridView_馬達輸出索引表.SQL_AddRows(list_馬達輸出索引表_add, false);
                this.sqL_DataGridView_馬達輸出索引表.SQL_ReplaceExtra(list_馬達輸出索引表_replace, false);
                this.sqL_DataGridView_馬達輸出索引表.SQL_GetAllRows(true);
                MyMessageBox.ShowDialog("匯入完成!");

            }));
        }
        #endregion
    }
}
