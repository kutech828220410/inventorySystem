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
using HIS_DB_Lib;


namespace Hospital_Call_Light_System
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        enum enum_叫號內容設定
        {
            GUID,
            名稱,
            英文名,
            叫號備註,
            樣式代碼,
            號碼
        }
        private void Program_叫號內容_Init()
        {
            SQL_DataGridView.SQL_Set_Properties(sqL_DataGridView_叫號內容設定, dBConfigClass.DB_Basic);
            Table table = new Table("num_setting");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table.AddColumnList("英文名", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table.AddColumnList("叫號備註", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table.AddColumnList("樣式代碼", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("號碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            this.sqL_DataGridView_叫號內容設定.Init(table);
            if (sqL_DataGridView_叫號內容設定.SQL_IsTableCreat() == false)
            {
                sqL_DataGridView_叫號內容設定.SQL_CreateTable();
            }
            else
            {
                sqL_DataGridView_叫號內容設定.SQL_CheckAllColumnName(true);
            }
            this.sqL_DataGridView_叫號內容設定.Set_ColumnVisible(false, new enum_叫號內容設定().GetEnumNames());

            this.sqL_DataGridView_叫號內容設定.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_叫號內容設定.名稱);
            this.sqL_DataGridView_叫號內容設定.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_叫號內容設定.英文名);
            this.sqL_DataGridView_叫號內容設定.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_叫號內容設定.叫號備註);
            this.sqL_DataGridView_叫號內容設定.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_叫號內容設定.樣式代碼);
            this.sqL_DataGridView_叫號內容設定.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleLeft, enum_叫號內容設定.號碼);
            this.sqL_DataGridView_叫號內容設定.RowDoubleClickEvent += SqL_DataGridView_叫號內容設定_RowDoubleClickEvent;

            plC_UI_Init.Add_Method(Program_叫號內容);
        }
        private void Program_叫號內容()
        {

        }
        #region Function
        private List<string> Function_取得叫號名稱()
        {
            List<string> list_str = new List<string>();
            List<object[]> list_value = this.sqL_DataGridView_叫號內容設定.SQL_GetAllRows(false);
            for (int i = 0; i < list_value.Count; i++)
            {
                list_str.Add(list_value[i][(int)enum_叫號內容設定.名稱].ObjectToString());
            }
            return list_str;
        }
        #endregion
        #region Event
        private void SqL_DataGridView_叫號內容設定_RowDoubleClickEvent(object[] RowValue)
        {
            rJ_TextBox_叫號內容設定_名稱.Text = RowValue[(int)enum_叫號內容設定.名稱].ObjectToString();
            rJ_TextBox_叫號內容設定_英文名.Text = RowValue[(int)enum_叫號內容設定.英文名].ObjectToString();
            rJ_TextBox_叫號內容設定_叫號備註.Text = RowValue[(int)enum_叫號內容設定.叫號備註].ObjectToString();
            comboBox_叫號內容設定_代碼.Text = RowValue[(int)enum_叫號內容設定.樣式代碼].ObjectToString();
        }
        private void PlC_RJ_Button_叫號內容設定_登錄_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_叫號內容設定_名稱.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("名稱空白!");
                return;
            }
            string text = "";
            this.Invoke(new Action(delegate
            {
                text = comboBox_叫號內容設定_代碼.Text;
            }));
            if (text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("未選擇代碼!");
                return;
            }
            List<object[]> list_value = this.sqL_DataGridView_叫號內容設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_叫號內容設定.名稱, rJ_TextBox_叫號內容設定_名稱.Text);
            if (list_value.Count == 0)
            {
                object[] value = new object[new enum_叫號內容設定().GetLength()];
                value[(int)enum_叫號內容設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_叫號內容設定.名稱] = rJ_TextBox_叫號內容設定_名稱.Text;
                value[(int)enum_叫號內容設定.英文名] = rJ_TextBox_叫號內容設定_英文名.Text;
                value[(int)enum_叫號內容設定.叫號備註] = rJ_TextBox_叫號內容設定_叫號備註.Text;
                value[(int)enum_叫號內容設定.樣式代碼] = text;
                value[(int)enum_叫號內容設定.號碼] = "0000";
                this.sqL_DataGridView_叫號內容設定.SQL_AddRow(value, true);
            }
            else
            {
                object[] value = list_value[0];
                value[(int)enum_叫號內容設定.名稱] = rJ_TextBox_叫號內容設定_名稱.Text;
                value[(int)enum_叫號內容設定.英文名] = rJ_TextBox_叫號內容設定_英文名.Text;
                value[(int)enum_叫號內容設定.叫號備註] = rJ_TextBox_叫號內容設定_叫號備註.Text;
                value[(int)enum_叫號內容設定.樣式代碼] = text;
                this.sqL_DataGridView_叫號內容設定.SQL_ReplaceExtra(value, true);
            }

        }
        private void PlC_RJ_Button_叫號內容設定_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_叫號內容設定.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            this.sqL_DataGridView_叫號內容設定.SQL_DeleteExtra(list_value, true);
        }
        #endregion
    }
}
