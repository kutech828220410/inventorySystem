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

    public partial class Main_Form : Form
    {
        private void sub_Program_盤點作業_資料庫_Init()
        {
            string url = $"{dBConfigClass.Api_URL}/api/inventory/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}" , json_in);
            List<SQLUI.Table> tables = json.JsonDeserializet<List<SQLUI.Table>>();
            if (tables == null)
            {
                MyMessageBox.ShowDialog($"盤點作業表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }

            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].TableName == this.sqL_DataGridView_盤點單號.TableName)
                {
                    this.sqL_DataGridView_盤點單號.Init(tables[i]);
                }
                if (tables[i].TableName == this.sqL_DataGridView_盤點內容.TableName)
                {
                    this.sqL_DataGridView_盤點內容.Init(tables[i]);
                }
                if (tables[i].TableName == this.sqL_DataGridView_盤點明細.TableName)
                {
                    this.sqL_DataGridView_盤點明細.Init(tables[i]);
                }
            }
            //this.sqL_DataGridView_盤點單號.Init();
           
            //this.sqL_DataGridView_盤點內容.Init();
           
            //this.sqL_DataGridView_盤點明細.Init();

            this.sqL_DataGridView_盤點單號.MouseDown += SqL_DataGridView_盤點單號_MouseDown;
            this.sqL_DataGridView_盤點內容.MouseDown += SqL_DataGridView_盤點內容_MouseDown;
            this.sqL_DataGridView_盤點明細.MouseDown += SqL_DataGridView_盤點明細_MouseDown;

        }

        private void SqL_DataGridView_盤點內容_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_盤點內容.SQL_GetAllRows(true);
        }

        private void SqL_DataGridView_盤點單號_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_盤點單號.SQL_GetAllRows(true);
        }
        private void SqL_DataGridView_盤點明細_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_盤點明細.SQL_GetAllRows(true);
        }
    }
}
