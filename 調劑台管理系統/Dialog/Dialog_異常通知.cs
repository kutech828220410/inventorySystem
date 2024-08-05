using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_異常通知 : MyDialog
    {
        private List<notifyExceptionClass> notifyExceptionClasses;
        public Dialog_異常通知(List<notifyExceptionClass> notifyExceptionClasses)
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
         
            this.notifyExceptionClasses = notifyExceptionClasses;
            this.LoadFinishedEvent += Dialog_異常通知_LoadFinishedEvent;
        }

        private void Dialog_異常通知_LoadFinishedEvent(EventArgs e)
        {
            Table table = new Table(new enum_notifyException());
            this.sqL_DataGridView_異常通知.RowsHeight = 60;
            this.sqL_DataGridView_異常通知.Init(table);
            this.sqL_DataGridView_異常通知.Set_ColumnVisible(false, new enum_notifyException().GetEnumNames());
            this.sqL_DataGridView_異常通知.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_notifyException.類別);
            this.sqL_DataGridView_異常通知.Set_ColumnWidth(1100, DataGridViewContentAlignment.MiddleLeft, enum_notifyException.內容);
            this.sqL_DataGridView_異常通知.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_notifyException.發生時間);

            List<object[]> list_value = this.notifyExceptionClasses.ClassToSQL<notifyExceptionClass , enum_notifyException>();
            this.sqL_DataGridView_異常通知.RefreshGrid(list_value);
;
        }
    }
}
