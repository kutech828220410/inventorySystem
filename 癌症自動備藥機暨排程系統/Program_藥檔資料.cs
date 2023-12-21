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
        public void Program_藥檔資料_Init()
        {
            string url = $"{API_Server}/api/MED_page/init";
            returnData returnData = new returnData();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.TableName = "medicine_page_cloud";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"雲端藥檔表單建立失敗!! Api_URL:{API_Server}");
                return;
            }
            this.sqL_DataGridView_藥檔資料.Init(table);
            this.sqL_DataGridView_藥檔資料.Set_ColumnVisible(true, new enum_雲端藥檔().GetEnumNames());
            this.sqL_DataGridView_藥檔資料.MouseDown += SqL_DataGridView_藥檔資料_MouseDown;

            this.plC_UI_Init.Add_Method(Program_藥檔資料);
        }
        public void Program_藥檔資料()
        {

        }

        #region Event
        private void SqL_DataGridView_藥檔資料_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_藥檔資料.SQL_GetAllRows(true);
        }
        #endregion

    }
}
