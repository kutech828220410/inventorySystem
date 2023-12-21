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
            this.sqL_DataGridView_馬達輸出索引表.Init(table);
            this.sqL_DataGridView_馬達輸出索引表.Server = table.Server;
            this.sqL_DataGridView_馬達輸出索引表.DataBaseName = table.DBName;
            this.sqL_DataGridView_馬達輸出索引表.UserName = table.Username;
            this.sqL_DataGridView_馬達輸出索引表.Password = table.Password;
            this.sqL_DataGridView_馬達輸出索引表.Port = table.Port.StringToUInt32();
            this.sqL_DataGridView_馬達輸出索引表.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;

            this.plC_RJ_Button_馬達輸出索引表_匯出.MouseDownEvent += PlC_RJ_Button_馬達輸出索引表_匯出_MouseDownEvent;

            this.plC_UI_Init.Add_Method(Program_馬達輸出索引表);
        }

 

        private void Program_馬達輸出索引表()
        {

        }
        #region Event
        private void PlC_RJ_Button_馬達輸出索引表_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;
                List<object[]> list_value = this.sqL_DataGridView_馬達輸出索引表.SQL_GetAllRows(false);
                DataTable dataTable = list_value.ToDataTable(new enum_CMPM_StorageConfig());
                dataTable.ReorderTable(new enum_CMPM_StorageConfig_匯出());

                dataTable.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);

                MyMessageBox.ShowDialog("匯出完成!");

            }));
        }
        #endregion
    }
}
