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
using SQLUI;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using HIS_DB_Lib;
namespace 智能藥庫系統
{
    public partial class Main_Form : Form
    {
        public static SQL_DataGridView _sqL_DataGridView_交易記錄查詢;


        private void Program_交易紀錄查詢_Init()
        {
            Table table = transactionsClass.Init(API_Server, ServerName, ServerType);

            sqL_DataGridView_交易記錄查詢.InitEx(table);
            _sqL_DataGridView_交易記錄查詢 = this.sqL_DataGridView_交易記錄查詢;


            plC_UI_Init.Add_Method(Program_交易紀錄查詢);
        }
        private void Program_交易紀錄查詢()
        {

        }
    }
}
