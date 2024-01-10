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
        public enum enum_公告設定
        {
            GUID,
            名稱,
            內容,
            字體,
            字體顏色,
            背景顏色,
            高度,
            移動速度,
        }

        private void Program_公告設定_Init()
        {
            SQL_DataGridView.SQL_Set_Properties(sqL_DataGridView_公告設定, dBConfigClass.DB_Basic);
            Table table = new Table("notice_settings");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("名稱", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("內容", Table.StringType.TEXT, 65535, Table.IndexType.None);
            table.AddColumnList("字體", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("字體顏色", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("背景顏色", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("高度", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("移動速度", Table.StringType.VARCHAR, 50, Table.IndexType.None);

            this.sqL_DataGridView_公告設定.Init(table);
            if (this.sqL_DataGridView_公告設定.SQL_IsTableCreat() == false) sqL_DataGridView_公告設定.SQL_CreateTable();
            else sqL_DataGridView_公告設定.SQL_CheckAllColumnName(true);

            this.sqL_DataGridView_公告設定.Set_ColumnVisible(false, new enum_公告設定().GetEnumNames());

            this.sqL_DataGridView_公告設定.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_公告設定.名稱);
            this.sqL_DataGridView_公告設定.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_公告設定.內容);
            this.sqL_DataGridView_公告設定.SQL_GetAllRows(true);
            this.sqL_DataGridView_公告設定.RowDoubleClickEvent += SqL_DataGridView_公告設定_RowDoubleClickEvent;

            this.plC_RJ_Button_公告設定_登錄.MouseDownEvent += PlC_RJ_Button_公告設定_登錄_MouseDownEvent;
            this.plC_RJ_Button_公告設定_刪除.MouseDownEvent += PlC_RJ_Button_公告設定_刪除_MouseDownEvent;
            this.plC_RJ_Button_公告設定_重新整理.MouseDownEvent += PlC_RJ_Button_公告設定_重新整理_MouseDownEvent;
            this.panel_公告設定_字體顏色.Click += Panel_公告設定_字體顏色_Click;
            this.panel_公告設定_背景顏色.Click += Panel_公告設定_背景顏色_Click;
            this.button_公告設定_字體選擇.Click += Button_公告設定_字體選擇_Click;
            plC_UI_Init.Add_Method(Program_公告設定);
        }

        private void Program_公告設定()
        {

        }
        #region Function

        #endregion
        #region Event
        private void PlC_RJ_Button_公告設定_登錄_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                string 名稱 = this.rJ_TextBox_公告設定_名稱.Texts;
                string 內容 = this.rJ_TextBox_公告設定_內容.Texts;
                string 高度 = this.comboBox_公告設定_列高度.Text;
                string 移動速度 = this.comboBox_公告設定_跑馬速度.Text;
                string 字體顏色 = this.panel_公告設定_字體顏色.BackColor.ToColorString();
                string 背景顏色 = this.panel_公告設定_背景顏色.BackColor.ToColorString();
                string 字體 = this.rJ_TextBox_公告設定_字體.Texts;

                List<object[]> list_value = this.sqL_DataGridView_公告設定.SQL_GetRows((int)enum_公告設定.名稱, 名稱, false);
                if (list_value.Count == 0)
                {
                    object[] value = new object[new enum_公告設定().GetLength()];
                    value[(int)enum_公告設定.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_公告設定.名稱] = 名稱;
                    value[(int)enum_公告設定.內容] = 內容;
                    value[(int)enum_公告設定.高度] = 高度;
                    value[(int)enum_公告設定.移動速度] = 移動速度;
                    value[(int)enum_公告設定.字體顏色] = 字體顏色;
                    value[(int)enum_公告設定.背景顏色] = 背景顏色;
                    value[(int)enum_公告設定.字體] = 字體;

                    this.sqL_DataGridView_公告設定.SQL_AddRow(value, false);
                    this.sqL_DataGridView_公告設定.AddRow(value, true);
                }
                else
                {
                    object[] value = list_value[0];
                    value[(int)enum_公告設定.名稱] = 名稱;
                    value[(int)enum_公告設定.內容] = 內容;
                    value[(int)enum_公告設定.高度] = 高度;
                    value[(int)enum_公告設定.移動速度] = 移動速度;
                    value[(int)enum_公告設定.字體顏色] = 字體顏色;
                    value[(int)enum_公告設定.背景顏色] = 背景顏色;
                    value[(int)enum_公告設定.字體] = 字體;

                    this.sqL_DataGridView_公告設定.SQL_ReplaceExtra(value, false);
                    this.sqL_DataGridView_公告設定.ReplaceExtra(value, true);
                }
            }));
         
        }
        private void PlC_RJ_Button_公告設定_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                string 名稱 = this.rJ_TextBox_公告設定_名稱.Texts;

                this.sqL_DataGridView_公告設定.SQL_Delete((int)enum_公告設定.名稱, 名稱, false);
                this.sqL_DataGridView_公告設定.Delete((int)enum_公告設定.名稱, 名稱, true);
            }));
           
        }
        #endregion
        #region Event
        private void SqL_DataGridView_公告設定_RowDoubleClickEvent(object[] RowValue)
        {
            this.rJ_TextBox_公告設定_名稱.Texts = RowValue[(int)enum_公告設定.名稱].ObjectToString();
            this.rJ_TextBox_公告設定_內容.Texts = RowValue[(int)enum_公告設定.內容].ObjectToString();
            this.comboBox_公告設定_列高度.Text = RowValue[(int)enum_公告設定.高度].ObjectToString();
            this.comboBox_公告設定_跑馬速度.Text = RowValue[(int)enum_公告設定.移動速度].ObjectToString();
            this.panel_公告設定_字體顏色.BackColor = RowValue[(int)enum_公告設定.字體顏色].ObjectToString().ToColor();
            this.panel_公告設定_背景顏色.BackColor = RowValue[(int)enum_公告設定.背景顏色].ObjectToString().ToColor();
            this.rJ_TextBox_公告設定_字體.Texts = RowValue[(int)enum_公告設定.字體].ObjectToString();

            if (this.rJ_TextBox_公告設定_字體.Texts.StringIsEmpty() == true)
            {
                this.rJ_TextBox_公告設定_字體.Texts = new Font("微軟正黑體", 16).ToFontString();
            }
        }
        private void Button_公告設定_字體選擇_Click(object sender, EventArgs e)
        {
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
                this.rJ_TextBox_公告設定_字體.Texts = this.fontDialog.Font.ToFontString();
            }
        }
        private void Panel_公告設定_背景顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_公告設定_背景顏色.BackColor = this.colorDialog.Color;
            }
        }
        private void Panel_公告設定_字體顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_公告設定_字體顏色.BackColor = this.colorDialog.Color;
            }
        }
        private void PlC_RJ_Button_公告設定_重新整理_MouseDownEvent(MouseEventArgs mevent)
        {
            this.sqL_DataGridView_公告設定.SQL_GetAllRows(true);
        }

        #endregion
    }
}
