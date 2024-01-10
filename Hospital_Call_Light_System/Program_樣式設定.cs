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
        public enum enum_樣式設定
        {
            GUID,
            代碼,
            名稱,
            台號,
            寬度,
            標題名稱,
            標題字體,
            標題文字寬度,
            標題字體顏色,
            標題背景顏色,
            標題高度,
            英文標題高度,
            英文標題字體,
            叫號號碼,
            叫號字體,
            叫號文字寬度,
            叫號字體顏色,
            叫號背景顏色,
            叫號備註高度,
            叫號備註字體,

        }
        private void Program_樣式設定_Init()
        {
            SQL_DataGridView.SQL_Set_Properties(sqL_DataGridView_樣式設定, dBConfigClass.DB_Basic);
            Table table = new Table("sytle_setting");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("代碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table.AddColumnList("台號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("寬度", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("標題名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table.AddColumnList("標題字體", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("標題文字寬度", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("標題字體顏色", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("標題背景顏色", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("標題高度", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("英文標題高度", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("英文標題字體", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("叫號號碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("叫號字體", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("叫號文字寬度", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("叫號字體顏色", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("叫號背景顏色", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("叫號備註高度", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("叫號備註字體", Table.StringType.VARCHAR, 50, Table.IndexType.None);


            this.sqL_DataGridView_樣式設定.Init(table);
            if (this.sqL_DataGridView_樣式設定.SQL_IsTableCreat() == false) sqL_DataGridView_樣式設定.SQL_CreateTable();
            else sqL_DataGridView_樣式設定.SQL_CheckAllColumnName(true);

            comboBox_公告設定_列高度.SelectedIndex = 0;
            comboBox_公告設定_跑馬速度.SelectedIndex = 0;

            Function_樣式設定表單檢查();

            plC_UI_Init.Add_Method(Program_樣式設定);
        }

        private void Program_樣式設定()
        {

        }
        #region Function
        private void Function_樣式設定表單檢查()
        {
            List<object[]> list_樣式設定 = this.sqL_DataGridView_樣式設定.SQL_GetAllRows(false);
            List<object[]> list_樣式設定_buf = new List<object[]>();
            List<object[]> list_樣式設定_add = new List<object[]>();
            List<object[]> list_樣式設定_delete = new List<object[]>();
            List<object[]> list_樣式設定_replace = new List<object[]>();

            for (int i = 0; i < list_樣式設定.Count; i++)
            {
                int 代碼 = list_樣式設定[i][(int)enum_樣式設定.代碼].StringToInt32();
                if (代碼 <= 0 || 代碼 > 9)
                {
                    list_樣式設定_delete.Add(list_樣式設定[i]);
                }
            }
            for (int i = 1; i < 10; i++)
            {
                list_樣式設定_buf = list_樣式設定.GetRows((int)enum_樣式設定.代碼, i.ToString());
                if (list_樣式設定_buf.Count == 0)
                {
                    object[] value = new object[new enum_樣式設定().GetLength()];

                    value[(int)enum_樣式設定.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_樣式設定.代碼] = i.ToString();
                    value[(int)enum_樣式設定.名稱] = "";
                    value[(int)enum_樣式設定.台號] = "";
                    value[(int)enum_樣式設定.標題名稱] = "";
                    value[(int)enum_樣式設定.標題字體] = new Font("標楷體", 150, FontStyle.Bold).ToFontString();
                    value[(int)enum_樣式設定.標題文字寬度] = 800;
                    value[(int)enum_樣式設定.標題字體顏色] = Color.Black.ToColorString();
                    value[(int)enum_樣式設定.標題背景顏色] = Color.RoyalBlue.ToColorString();
                    value[(int)enum_樣式設定.標題高度] = 300;

                    value[(int)enum_樣式設定.叫號號碼] = "0000";
                    value[(int)enum_樣式設定.叫號字體] = new Font("標楷體", 200, FontStyle.Bold).ToFontString();
                    value[(int)enum_樣式設定.叫號文字寬度] = 800;
                    value[(int)enum_樣式設定.叫號字體顏色] = Color.Red.ToColorString();
                    value[(int)enum_樣式設定.叫號背景顏色] = Color.White.ToColorString();

                    list_樣式設定_add.Add(value);
                }
                else
                {
                    object[] value = list_樣式設定_buf[0];
                    if (value[(int)enum_樣式設定.英文標題字體].ObjectToString().StringIsEmpty()) value[(int)enum_樣式設定.英文標題字體] = new Font("標楷體", 80, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_樣式設定.叫號字體].ObjectToString().StringIsEmpty()) value[(int)enum_樣式設定.叫號字體] = new Font("標楷體", 200, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_樣式設定.叫號備註字體].ObjectToString().StringIsEmpty()) value[(int)enum_樣式設定.叫號備註字體] = new Font("標楷體", 30, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_樣式設定.標題字體].ObjectToString().StringIsEmpty()) value[(int)enum_樣式設定.標題字體] = new Font("標楷體", 150, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_樣式設定.標題高度].StringToInt32() <= 0) value[(int)enum_樣式設定.標題高度] = 300;
                    if (value[(int)enum_樣式設定.標題文字寬度].StringToInt32() <= 0) value[(int)enum_樣式設定.標題文字寬度] = 800;
                    if (value[(int)enum_樣式設定.叫號文字寬度].StringToInt32() <= 0) value[(int)enum_樣式設定.叫號文字寬度] = 800;
                    if (value[(int)enum_樣式設定.叫號備註高度].StringToInt32() <= 0) value[(int)enum_樣式設定.叫號備註高度] = 100;
                    if (value[(int)enum_樣式設定.英文標題高度].StringToInt32() <= 0) value[(int)enum_樣式設定.英文標題高度] = 200;
                    list_樣式設定_replace.Add(value);



                }
            }

            this.sqL_DataGridView_樣式設定.SQL_DeleteExtra(list_樣式設定_delete, false);
            this.sqL_DataGridView_樣式設定.SQL_AddRows(list_樣式設定_add, false);
            this.sqL_DataGridView_樣式設定.SQL_ReplaceExtra(list_樣式設定_replace, false);

        }
        #endregion
        public class Icp_叫號樣式設定 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 台號_0 = x[(int)enum_樣式設定.台號].ObjectToString();
                string 台號_1 = y[(int)enum_樣式設定.台號].ObjectToString();
                return 台號_0.CompareTo(台號_1);
            }
        }
    }
}
