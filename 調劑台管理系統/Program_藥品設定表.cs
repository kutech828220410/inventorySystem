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
    public enum enum_藥品設定表
    {
        GUID,
        藥品碼,
        效期管理,
        盲盤,
        複盤,
        結存報表,
        自定義,
    }
    public partial class Form1 : Form
    {
        private void Program_藥品設定表_Init()
        {
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_藥品設定表, dBConfigClass.DB_Medicine_Cloud);
            this.sqL_DataGridView_藥品設定表.Init();
            if (this.sqL_DataGridView_藥品設定表.SQL_IsTableCreat() == false) this.sqL_DataGridView_藥品設定表.SQL_CreateTable();
            else this.sqL_DataGridView_藥品設定表.SQL_CheckAllColumnName(true);


            this.plC_UI_Init.Add_Method(this.sub_Program_藥品設定表);
        }
        private void sub_Program_藥品設定表()
        {

        }

        #region Function
        
        private bool Function_藥品設定表_取得是否自訂義(string 藥品碼)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品設定表.SQL_GetRows((int)enum_藥品設定表.藥品碼, 藥品碼, false);
            if (list_value.Count == 0) return false;
            return list_value[0][(int)enum_藥品設定表.自定義].StringToBool();
        }
        private bool Function_藥品設定表_取得管制方式(enum_藥品設定表 _enum_藥品設定表, string 藥品碼)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品設定表.SQL_GetRows((int)enum_藥品設定表.藥品碼, 藥品碼, false);
            if (list_value.Count == 0) return false;
            if (list_value[0][(int)enum_藥品設定表.自定義].StringToBool() == false) return false;
            return list_value[0][(int)_enum_藥品設定表].StringToBool();
        }
        #endregion
    }


}
