﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using HIS_DB_Lib;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        private void Program_雲端藥檔_Init()
        {
            string url = $"{dBConfigClass.Api_URL}/api/MED_page/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.TableName = "medicine_page_cloud";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"雲端藥檔表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }
            this.sqL_DataGridView_雲端藥檔.Init(table);
            this.sqL_DataGridView_雲端藥檔.Set_ColumnVisible(true, new enum_雲端藥檔().GetEnumNames());
            this.sqL_DataGridView_雲端藥檔.MouseDown += SqL_DataGridView_雲端藥檔_MouseDown;


            this.plC_RJ_Button_雲端藥檔_取得資料.MouseDownEvent += PlC_RJ_Button_雲端藥檔_取得資料_MouseDownEvent;
        }

        private void SqL_DataGridView_雲端藥檔_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_雲端藥檔.SQL_GetAllRows(true);
        }
        private void PlC_RJ_Button_雲端藥檔_取得資料_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<object[]> list_value = this.sqL_DataGridView_雲端藥檔.SQL_GetAllRows(false);
            Console.WriteLine($"取得雲端藥檔資料,耗時{myTimer.ToString()}ms");
            this.sqL_DataGridView_雲端藥檔.RefreshGrid(list_value);
            Console.WriteLine($"更新雲端藥檔至Datagridview,耗時{myTimer.ToString()}ms");
        }
    }
}
