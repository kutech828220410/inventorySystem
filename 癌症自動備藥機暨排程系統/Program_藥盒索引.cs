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
    [EnumDescription("drugBoxIndex")]
    public enum enum_drugBoxIndex
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("barcode,VARCHAR,50,NONE")]
        barcode,
        [Description("master_GUID,VARCHAR,50,NONE")]
        master_GUID,

    }
 
    public partial class Main_Form : Form
    {
        static public SQL_DataGridView _sqL_DataGridView_藥盒索引 = null;
        private void Program_藥盒索引_Init()
        {
            Table table = new Table(new enum_drugBoxIndex());
            table.Server = dBConfigClass.DB_Basic.IP;
            table.Port = dBConfigClass.DB_Basic.Port.ToString();
            table.DBName = dBConfigClass.DB_Basic.DataBaseName;
            table.Username = dBConfigClass.DB_Basic.UserName;
            table.Password = dBConfigClass.DB_Basic.Password;

            sqL_DataGridView_藥盒索引.InitEx(table);
            if (sqL_DataGridView_藥盒索引.SQL_IsTableCreat() == false)
            {
                sqL_DataGridView_藥盒索引.SQL_CreateTable();
            }
            else
            {
                sqL_DataGridView_藥盒索引.SQL_CheckAllColumnName(true);
            }
            _sqL_DataGridView_藥盒索引 = sqL_DataGridView_藥盒索引;

            sqL_DataGridView_藥盒索引.MouseDown += SqL_DataGridView_藥盒索引_MouseDown;
        }

        private void SqL_DataGridView_藥盒索引_MouseDown(object sender, MouseEventArgs e)
        {
            sqL_DataGridView_藥盒索引.SQL_GetAllRows(true);
        }
    }
}
