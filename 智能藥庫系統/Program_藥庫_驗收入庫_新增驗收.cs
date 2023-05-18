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
using SQLUI;
using MyUI;
using Basic;
using H_Pannel_lib;
using HIS_DB_Lib;
namespace 智能藥庫系統
{

    public partial class Form1 : Form
    {
        private void sub_Program_藥庫_驗收入庫_新增驗收_Init()
        {
            this.sqL_DataGridView_驗收入庫_新增驗收.Init(this.sqL_DataGridView_藥局_藥品資料);
            this.sqL_DataGridView_驗收入庫_新增驗收.Set_ColumnVisible(false, new enum_藥局_藥品資料().GetEnumNames());
            this.sqL_DataGridView_驗收入庫_新增驗收.Set_ColumnVisible(true, enum_藥局_藥品資料.藥品碼, enum_藥局_藥品資料.藥品名稱, enum_藥局_藥品資料.料號, enum_藥局_藥品資料.中文名稱, enum_藥局_藥品資料.包裝單位);

            this.plC_RJ_Button_驗收入庫_新增驗收_自動生成.MouseDownEvent += PlC_RJ_Button_驗收入庫_新增驗收_自動生成_MouseDownEvent;

            this.plC_UI_Init.Add_Method(sub_Program_藥庫_驗收入庫_新增驗收);
        }

      

        private bool flag_藥庫_驗收入庫_新增驗收 = false;
        private void sub_Program_藥庫_驗收入庫_新增驗收()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥庫" && this.plC_ScreenPage_藥庫.PageText == "驗收入庫" && this.plC_ScreenPage_藥庫_驗收入庫.PageText == "新增驗收")
            {
                if (!this.flag_藥庫_驗收入庫_新增驗收)
                {

                    this.flag_藥庫_驗收入庫_新增驗收 = true;
                }

            }
            else
            {
                this.flag_藥庫_驗收入庫_新增驗收 = false;
            }
        }

        #region Function

        #endregion
        #region Event
        private void PlC_RJ_Button_驗收入庫_新增驗收_自動生成_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.rJ_TextBox_驗收入庫_新增驗收_驗收單號.Text = $"{DateTime.Now.ToDateTinyString()}";
            }));
        }
        #endregion
    }
}
