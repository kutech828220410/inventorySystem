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
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using H_Pannel_lib;
using MyOffice;
using HIS_DB_Lib;
using SQLUI;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        [EnumDescription("lcd114_index")]
        public enum enum_LCD114_索引表
        {
            [Description("GUID,VARCHAR,50,PRIKEY")]
            GUID,
            [Description("IP,VARCHAR,50,INDEX")]
            IP,
            [Description("index_IP,VARCHAR,50,INDEX")]
            index_IP,
        }
        private void Program_LCD114_索引表_Init()
        {
            Table table = new Table(new enum_LCD114_索引表());
            table.Server = dBConfigClass.DB_Basic.IP;
            table.Port = dBConfigClass.DB_Basic.Port.ToString();
            table.Username = dBConfigClass.DB_Basic.UserName;
            table.Password = dBConfigClass.DB_Basic.Password;
            table.DBName = dBConfigClass.DB_Basic.DataBaseName;

            this.sqL_DataGridView_LCD114_索引表.Init(table);

            if (this.sqL_DataGridView_LCD114_索引表.SQL_IsTableCreat() == false) this.sqL_DataGridView_LCD114_索引表.SQL_CreateTable();
            else this.sqL_DataGridView_LCD114_索引表.SQL_CheckAllColumnName(true);

            this.plC_UI_Init.Add_Method(this.Program_LCD114_索引表);

        }

        private void Program_LCD114_索引表()
        {

        }
    }
}
