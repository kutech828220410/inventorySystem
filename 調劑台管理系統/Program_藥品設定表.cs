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
  
    public partial class Form1 : Form
    {
        private void Program_藥品設定表_Init()
        {
  
            string url = $"{dBConfigClass.Api_URL}/api/medConfig/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"藥品設定表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }

            this.sqL_DataGridView_藥品設定表.Init(table);
            this.sqL_DataGridView_藥品設定表.Set_ColumnVisible(true, new enum_藥品資料_藥檔資料().GetEnumNames());

            this.plC_UI_Init.Add_Method(this.sub_Program_藥品設定表);
        }
        private void sub_Program_藥品設定表()
        {

        }

        #region Function
        
        private bool Function_藥品設定表_取得是否自訂義(List<object[]> list_value ,string 藥品碼)
        {
       
            List<object[]> list_value_buf = list_value.GetRows((int)enum_藥品設定表.藥品碼, 藥品碼);
            if (list_value_buf.Count == 0) return false;
            return list_value_buf[0][(int)enum_藥品設定表.自定義].StringToBool();
        }
        private bool Function_藥品設定表_取得管制方式(List<object[]> list_value, enum_藥品設定表 _enum_藥品設定表, string 藥品碼)
        {
       
            List<object[]> list_value_buf = list_value.GetRows((int)enum_藥品設定表.藥品碼, 藥品碼);
            if (list_value_buf.Count == 0) return false;
            if (list_value_buf[0][(int)enum_藥品設定表.自定義].StringToBool() == false) return false;
            return list_value_buf[0][(int)_enum_藥品設定表].StringToBool();
        }
        #endregion
    }


}
