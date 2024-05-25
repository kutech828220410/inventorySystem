using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;
using MyUI;
using SQLUI;
using DrawingClass;
using H_Pannel_lib;

namespace 中藥調劑系統
{
    public enum enum_儲位列表
    {
        [Description("IP,VARCHAR,15,NONE")]
        IP,
        [Description("名稱,VARCHAR,15,NONE")]
        名稱,
    }
    public partial class Dialog_儲位設定 : MyDialog
    {
        public Dialog_儲位設定()
        {
            InitializeComponent();
            this.Load += Dialog_儲位設定_Load;
        }

        private void Dialog_儲位設定_Load(object sender, EventArgs e)
        {
            Table table_儲位列表 = new Table(new enum_儲位列表());

            this.sqL_DataGridView_層架列表.Init(table_儲位列表);
            this.sqL_DataGridView_層架列表.Set_ColumnVisible(false, new enum_儲位列表().GetEnumNames());
            this.sqL_DataGridView_層架列表.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.IP);
            this.sqL_DataGridView_層架列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.名稱);
        }
    }
}
