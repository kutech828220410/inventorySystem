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
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;

namespace Hospital_Call_Light_System
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public enum enum_參數
        {
            GUID,
            Name,
            Value,
        }



        private void Program_系統_Init()
        {


            Table table = new Table("parameter");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("Name", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("Value", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            SQL_DataGridView.SQL_Set_Properties(sqL_DataGridView_參數, dBConfigClass.DB_Basic);
            this.sqL_DataGridView_參數.Init(table);
            if (this.sqL_DataGridView_參數.SQL_IsTableCreat() == false) sqL_DataGridView_參數.SQL_CreateTable();
            else sqL_DataGridView_參數.SQL_CheckAllColumnName(true);

       

        }



    }
}
