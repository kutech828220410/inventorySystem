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
using H_Pannel_lib;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using SQLUI;

namespace 調劑台管理系統
{
  
    public partial class Main_Form : Form
    {
        private void Program_藥品設定表_Init()
        {
            Table table = medConfigClass.init(API_Server);
            if (table == null)
            {
                MyMessageBox.ShowDialog($"[藥品設定表]建立失敗");
                return;
            }


            this.sqL_DataGridView_藥品設定表.Init(table);
            this.sqL_DataGridView_藥品設定表.Set_ColumnVisible(true, new enum_medConfig().GetEnumNames());

            this.plC_UI_Init.Add_Method(this.sub_Program_藥品設定表);
        }
        private void sub_Program_藥品設定表()
        {

        }

        #region Function

        static private bool Function_藥品設定表_取得是否自訂義(List<object[]> list_value ,string 藥品碼)
        {
       
            List<object[]> list_value_buf = list_value.GetRows((int)enum_medConfig.藥碼, 藥品碼);
            if (list_value_buf.Count == 0) return false;
            return list_value_buf[0][(int)enum_medConfig.自定義].StringToBool();
        }
        static private bool Function_藥品設定表_取得管制方式(List<object[]> list_value, enum_medConfig _enum_medConfig, string 藥品碼)
        {
       
            List<object[]> list_value_buf = list_value.GetRows((int)enum_medConfig.藥碼, 藥品碼);
            if (list_value_buf.Count == 0) return false;
            if (list_value_buf[0][(int)enum_medConfig.自定義].StringToBool() == false && _enum_medConfig != enum_medConfig.使用RFID) return false;
            return list_value_buf[0][(int)_enum_medConfig].StringToBool();
        }
        #endregion
    }


}
