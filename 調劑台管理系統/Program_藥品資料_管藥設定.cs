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
    public partial class Form1 : Form
    {
        private void Program_藥品資料_管藥設定_Init()
        {

            this.comboBox_藥品資料_管藥設定_類型.SelectedIndexChanged += ComboBox_藥品資料_管藥設定_類型_SelectedIndexChanged;

            this.plC_RJ_ChechBox_藥品資料_管藥設定_效期管理.CheckedChanged += PlC_RJ_ChechBox_藥品資料_管藥設定_效期管理_CheckedChanged;
            this.plC_RJ_ChechBox_藥品資料_管藥設定_複盤.CheckedChanged += PlC_RJ_ChechBox_藥品資料_管藥設定_複盤_CheckedChanged;
            this.plC_RJ_ChechBox_藥品資料_管藥設定_盲盤.CheckedChanged += PlC_RJ_ChechBox_藥品資料_管藥設定_盲盤_CheckedChanged;
            this.plC_RJ_ChechBox_藥品資料_管藥設定_結存報表.CheckedChanged += PlC_RJ_ChechBox_藥品資料_管藥設定_結存報表_CheckedChanged;

            this.plC_RJ_Button_藥品資料_管藥設定_管制取藥設定_上傳.MouseDownEvent += PlC_RJ_Button_藥品資料_管藥設定_管制取藥設定_上傳_MouseDownEvent;
            this.plC_UI_Init.Add_Method(this.sub_Program_藥品資料_管藥設定);
        }

       

        bool flag_藥品資料_管藥設定_頁面更新 = false;
        private void sub_Program_藥品資料_管藥設定()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥品資料" && this.plC_ScreenPage_藥品資料.PageText == "管藥設定")
            {
                if (!this.flag_藥品資料_管藥設定_頁面更新)
                {
                    this.Invoke(new Action(delegate
                    {
                        this.comboBox_藥品資料_管藥設定_類型.SelectedIndex = 0;
                    }));
                    this.flag_藥品資料_管藥設定_頁面更新 = true;
                }
            }
            else
            {
                this.flag_藥品資料_管藥設定_頁面更新 = false;
            }
        }
        #region Function
        private string Function_藥品資料_管藥設定_取得代號(string 類型)
        {
            string 代號 = "";
            if (類型 == "管1")
            {
                代號 = "1";
            }
            if (類型 == "管2")
            {
                代號 = "2";
            }
            if (類型 == "管3")
            {
                代號 = "3";
            }
            if (類型 == "管4")
            {
                代號 = "4";
            }
            if (類型 == "N")
            {
                代號 = "N";
            }
            if (類型 == "警訊")
            {
                代號 = "警訊";
            }
            if (類型 == "高價")
            {
                代號 = "高價";
            }
            if (類型 == "生物製劑")
            {
                代號 = "生物製劑";
            }
            return 代號;
        }
        #endregion
        #region Event
        private void ComboBox_藥品資料_管藥設定_類型_SelectedIndexChanged(object sender, EventArgs e)
        {
            string 類型 = this.comboBox_藥品資料_管藥設定_類型.Text;
            string 代號 = this.Function_藥品資料_管藥設定_取得代號(類型);

            List<object[]> list_value = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_藥品管制方式設定.代號, 代號);
            if (list_value.Count > 0)
            {
                this.plC_RJ_ChechBox_藥品資料_管藥設定_效期管理.Checked = (list_value[0][(int)enum_藥品管制方式設定.效期管理].ObjectToString() == true.ToString());
                this.plC_RJ_ChechBox_藥品資料_管藥設定_複盤.Checked = (list_value[0][(int)enum_藥品管制方式設定.複盤].ObjectToString() == true.ToString());
                this.plC_RJ_ChechBox_藥品資料_管藥設定_盲盤.Checked = (list_value[0][(int)enum_藥品管制方式設定.盲盤].ObjectToString() == true.ToString());
                this.plC_RJ_ChechBox_藥品資料_管藥設定_結存報表.Checked = (list_value[0][(int)enum_藥品管制方式設定.結存報表].ObjectToString() == true.ToString());
                this.plC_RJ_ChechBox_藥品資料_管藥設定_雙人覆核.Checked = (list_value[0][(int)enum_藥品管制方式設定.雙人覆核].ObjectToString() == true.ToString()); 
            }
        }
        private void PlC_RJ_ChechBox_藥品資料_管藥設定_複盤_CheckedChanged(object sender, EventArgs e)
        {
            if (plC_RJ_ChechBox_藥品資料_管藥設定_複盤.Checked)
            {
                if (this.plC_RJ_ChechBox_藥品資料_管藥設定_盲盤.Checked) this.plC_RJ_ChechBox_藥品資料_管藥設定_盲盤.Checked = false;
            }
        }
        private void PlC_RJ_ChechBox_藥品資料_管藥設定_盲盤_CheckedChanged(object sender, EventArgs e)
        {
            if (plC_RJ_ChechBox_藥品資料_管藥設定_盲盤.Checked)
            {
                if (this.plC_RJ_ChechBox_藥品資料_管藥設定_複盤.Checked) this.plC_RJ_ChechBox_藥品資料_管藥設定_複盤.Checked = false;
            }
        }
        private void PlC_RJ_ChechBox_藥品資料_管藥設定_效期管理_CheckedChanged(object sender, EventArgs e)
        {
  
        }
        private void PlC_RJ_ChechBox_藥品資料_管藥設定_結存報表_CheckedChanged(object sender, EventArgs e)
        {
      
        }
        private void PlC_RJ_Button_藥品資料_管藥設定_管制取藥設定_上傳_MouseDownEvent(MouseEventArgs mevent)
        {
            string 類型 = "";
            this.Invoke(new Action(delegate 
            {
                類型 = this.comboBox_藥品資料_管藥設定_類型.Text;
            }));
           
            string 代號 = this.Function_藥品資料_管藥設定_取得代號(類型);
            List<object[]> list_value = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_藥品管制方式設定.代號, 代號);
            if (list_value.Count > 0)
            {
                object[] value = list_value[0];
                value[(int)enum_藥品管制方式設定.效期管理] = this.plC_RJ_ChechBox_藥品資料_管藥設定_效期管理.Checked.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = this.plC_RJ_ChechBox_藥品資料_管藥設定_複盤.Checked.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = this.plC_RJ_ChechBox_藥品資料_管藥設定_盲盤.Checked.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = this.plC_RJ_ChechBox_藥品資料_管藥設定_結存報表.Checked.ToString();
                value[(int)enum_藥品管制方式設定.雙人覆核] = this.plC_RJ_ChechBox_藥品資料_管藥設定_雙人覆核.Checked.ToString();
                this.sqL_DataGridView_藥品管制方式設定.SQL_ReplaceExtra(value, false);
            }
            else
            {
                object[] value = new object[new enum_藥品管制方式設定().GetLength()];
                value[(int)enum_藥品管制方式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_藥品管制方式設定.代號] = 代號;
                value[(int)enum_藥品管制方式設定.效期管理] = this.plC_RJ_ChechBox_藥品資料_管藥設定_效期管理.Checked.ToString();
                value[(int)enum_藥品管制方式設定.複盤] = this.plC_RJ_ChechBox_藥品資料_管藥設定_複盤.Checked.ToString();
                value[(int)enum_藥品管制方式設定.盲盤] = this.plC_RJ_ChechBox_藥品資料_管藥設定_盲盤.Checked.ToString();
                value[(int)enum_藥品管制方式設定.結存報表] = this.plC_RJ_ChechBox_藥品資料_管藥設定_結存報表.Checked.ToString();
                value[(int)enum_藥品管制方式設定.雙人覆核] = this.plC_RJ_ChechBox_藥品資料_管藥設定_雙人覆核.Checked.ToString();
                this.sqL_DataGridView_藥品管制方式設定.SQL_AddRow(value, false);
            }
            MyMessageBox.ShowDialog("上傳完成!");
        }

        #endregion
    }
}
