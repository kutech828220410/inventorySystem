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
    public partial class Dialog_人員資料 : MyDialog
    {
        public Dialog_人員資料()
        {
            InitializeComponent();

            this.Load += Dialog_人員資料_Load;
            this.Shown += Dialog_人員資料_Shown;
        }

        #region Function

        #endregion
        #region Event
        private void Dialog_人員資料_Shown(object sender, EventArgs e)
        {
        
        }
        private void Dialog_人員資料_Load(object sender, EventArgs e)
        {
            Table table = personPageClass.Init(Main_Form.API_Server);

            this.sqL_DataGridView_人員資料.Init(table);
            this.sqL_DataGridView_人員資料.Set_ColumnVisible(false, new enum_人員資料().GetEnumNames());
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.ID);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.姓名);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_人員資料.性別);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.單位);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_人員資料.藥師證字號);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_人員資料.權限等級);
            this.sqL_DataGridView_人員資料.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_人員資料.顏色);
         

            this.sqL_DataGridView_人員資料.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_人員資料.ID);
            this.sqL_DataGridView_人員資料.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_人員資料.姓名);
            this.sqL_DataGridView_人員資料.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_人員資料.藥師證字號);
            this.sqL_DataGridView_人員資料.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_人員資料.單位);

            this.sqL_DataGridView_人員資料.DataGridRefreshEvent += SqL_DataGridView_人員資料_DataGridRefreshEvent;


            List<personPageClass> personPageClasses = personPageClass.get_all(Main_Form.API_Server);
            List<object[]> list_人員資料 = personPageClasses.ClassToSQL<personPageClass , enum_人員資料>();
            this.sqL_DataGridView_人員資料.RefreshGrid(list_人員資料);

            this.rJ_Button_新增.MouseDownEvent += RJ_Button_新增_MouseDownEvent;

        }

        private void RJ_Button_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_人員資料_新增 dialog_人員資料_新增 = new Dialog_人員資料_新增();
            dialog_人員資料_新增.ShowDialog();
        }
        private void SqL_DataGridView_人員資料_DataGridRefreshEvent()
        {
            for (int i = 0; i < this.sqL_DataGridView_人員資料.dataGridView.Rows.Count; i++)
            {

                Color color = this.sqL_DataGridView_人員資料.dataGridView.Rows[i].Cells[enum_人員資料.顏色.GetEnumName()].Value.ObjectToString().ToColor();
                this.sqL_DataGridView_人員資料.dataGridView.Rows[i].Cells[enum_人員資料.顏色.GetEnumName()].Style.BackColor = color;
                this.sqL_DataGridView_人員資料.dataGridView.Rows[i].Cells[enum_人員資料.顏色.GetEnumName()].Style.ForeColor = color;
            }
        }
        #endregion
    }
}
