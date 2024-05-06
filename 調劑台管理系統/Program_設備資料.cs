using System;
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

namespace 調劑台管理系統
{
    [EnumDescription("devicelist")]
    public enum enum_設備資料
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("名稱,VARCHAR,100,NONE")]
        名稱,
        [Description("顏色,VARCHAR,20,NONE")]
        顏色,
        [Description("備註,VARCHAR,200,NONE")]
        備註,
    }
    public partial class Main_Form : Form
    {

        private void Program_設備資料_Init()
        {
            SQLUI.Table table = new SQLUI.Table(new enum_設備資料());
            this.sqL_DataGridView_設備資料.Init(table);
            if (!sqL_DataGridView_設備資料.SQL_IsTableCreat())
            {
                sqL_DataGridView_設備資料.SQL_CreateTable();
            }
        }
    }
}
