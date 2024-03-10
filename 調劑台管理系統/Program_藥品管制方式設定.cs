using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using HIS_DB_Lib;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
   

        private void Program_藥品管制方式設定_Init()
        {
            this.sqL_DataGridView_藥品管制方式設定.Init();
            if (this.sqL_DataGridView_藥品管制方式設定.SQL_IsTableCreat() == false) this.sqL_DataGridView_藥品管制方式設定.SQL_CreateTable();
            else this.sqL_DataGridView_藥品管制方式設定.SQL_CheckAllColumnName(true);

            Function_藥品管制方式設定_檢查表單();

            this.plC_UI_Init.Add_Method(this.sub_Program_藥品管制方式設定);
        }

        private void sub_Program_藥品管制方式設定()
        {
         
        }
        #region Function
        private void Function_藥品管制方式設定_檢查表單()
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
            List<object[]> list_value_buf = new List<object[]>();
            List<object[]> list_value_delete = new List<object[]>();
            List<object[]> list_value_add = new List<object[]>();
            list_value_delete = (from value in list_value
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "N"
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "1"
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "2"
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "3"
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "4"
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "警訊"
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "高價"
                                 where value[(int)enum_藥品管制方式設定.代號].ObjectToString() != "生物製劑"
                                 select value).ToList();
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "N");
            if(list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "N";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "1");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "1";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "2");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "2";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "3");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "3";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "4");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "4";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "警訊");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "警訊";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "高價");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "高價";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            list_value_buf = list_value.GetRows((int)enum_藥品管制方式設定.代號, "生物製劑");
            if (list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = "生物製劑";
                value[(int)enum_藥品管制方式設定.效期管理] = false.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = false.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = false.ToString();
                list_value_add.Add(value);
            }
            this.sqL_DataGridView_藥品管制方式設定.SQL_DeleteExtra(list_value_delete, false);
            this.sqL_DataGridView_藥品管制方式設定.SQL_AddRows(list_value_add, true);
        }
        #endregion
        #region Event
        private bool Function_藥品管制方式設定_取得管制方式(List<object[]> list_藥品管制方式設定, enum_藥品管制方式設定 _enum_藥品管制方式設定, string 代號)
        {
            List<object[]> list_value = list_藥品管制方式設定.GetRows((int)enum_藥品管制方式設定.代號, 代號);
            if (list_value.Count == 0) return false;
            string 管制方式 = list_value[0][(int)_enum_藥品管制方式設定].ObjectToString();
            return (管制方式 == true.ToString());
        }
        #endregion
    }
}
