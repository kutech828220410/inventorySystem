using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using System.Collections;
using HIS_DB_Lib;
namespace 智能藥庫系統
{
    public partial class Form1 : Form
    {
        private void sub_Program_驗收作業_資料庫_Init()
        {
            string url = $"{dBConfigClass.Api_URL}/api/inspection/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.藥庫.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            List<SQLUI.Table> tables = json.JsonDeserializet<List<SQLUI.Table>>();
            if (tables == null)
            {
                MyMessageBox.ShowDialog("驗收作業表單建立失敗!!");
                return;
            }
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].TableName == this.sqL_DataGridView_驗收單號.TableName)
                {
                    this.sqL_DataGridView_驗收單號.Init(tables[i]);
                }
                if (tables[i].TableName == this.sqL_DataGridView_驗收內容.TableName)
                {
                    this.sqL_DataGridView_驗收內容.Init(tables[i]);
                }
                if (tables[i].TableName == this.sqL_DataGridView_驗收明細.TableName)
                {
                    this.sqL_DataGridView_驗收明細.Init(tables[i]);
                }
            }

            this.sqL_DataGridView_驗收單號.MouseDown += SqL_DataGridView_驗收單號_MouseDown;
            this.sqL_DataGridView_驗收內容.MouseDown += SqL_DataGridView_驗收內容_MouseDown;
            this.sqL_DataGridView_驗收明細.MouseDown += SqL_DataGridView_驗收明細_MouseDown;

        }
        private void SqL_DataGridView_驗收明細_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_驗收明細.SQL_GetAllRows(true);
        }
        private void SqL_DataGridView_驗收內容_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_驗收內容.SQL_GetAllRows(true);
        }
        private void SqL_DataGridView_驗收單號_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_驗收單號.SQL_GetAllRows(true);

        }
    }
}
