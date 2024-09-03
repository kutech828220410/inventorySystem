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
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;

namespace 中藥調劑系統
{
    public partial class Dialog_其它處方 : MyDialog
    {
        public enum enum_套餐選擇
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("名稱,VARCHAR,15,NONE")]
            名稱,
        }
        public enum enum_套餐藥品內容
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("數量,VARCHAR,15,NONE")]
            數量,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
        }
        public Dialog_其它處方()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
   
            this.LoadFinishedEvent += Dialog_其它處方_LoadFinishedEvent;
        }

        private void Dialog_其它處方_LoadFinishedEvent(EventArgs e)
        {
            Table table_套餐選擇 = new Table(new enum_套餐選擇());
            this.sqL_DataGridView_套餐選擇.Init(table_套餐選擇);
            this.sqL_DataGridView_套餐選擇.Set_ColumnVisible(false, new enum_套餐選擇().GetEnumNames());
            this.sqL_DataGridView_套餐選擇.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, "名稱");


            Table table_套餐藥品內容 = new Table(new enum_套餐藥品內容());
            this.sqL_DataGridView_套餐藥品內容.Init(table_套餐藥品內容);
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnVisible(false, new enum_套餐藥品內容().GetEnumNames());
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, "藥碼");
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, "藥名");
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, "數量");
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, "單位");
        }
    }
}
