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
        private void Program_庫存查詢_Init()
        {
            string url = $"{API_Server}/api/MED_page/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"本地藥檔表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }

            this.sqL_DataGridView_庫存查詢.Init(table);
            this.sqL_DataGridView_庫存查詢.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
            this.sqL_DataGridView_庫存查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品碼);
            this.sqL_DataGridView_庫存查詢.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品名稱);
            this.sqL_DataGridView_庫存查詢.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.中文名稱);
            this.sqL_DataGridView_庫存查詢.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品學名);
            this.sqL_DataGridView_庫存查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.包裝單位);
            this.sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.庫存);
            this.sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.安全庫存);

            plC_UI_Init.Add_Method(Program_庫存查詢);
        }
        private void Program_庫存查詢()
        {

        }
    }
}
