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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using HIS_DB_Lib;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        enum enum_批次領藥資料
        {
            藥品碼,
            藥品名稱,
            交易量,
            病歷號,
            病房號,
            病人姓名,
            日期,
        }
   
        #region PLC_批次領藥_頁面更新
        bool flag_批次領藥_頁面更新 = false;
        void sub_Program_批次領藥_頁面更新()
        {
            if (this.plC_ScreenPage_Main.PageText == "批次領藥")
            {
                if (!this.flag_批次領藥_頁面更新)
                {
                    //this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_名稱.Text);
                    //this.Function_取藥堆疊資料_刪除指定調劑台名稱母資料(this.textBox_工程模式_領藥台_02_名稱.Text);
                    this.sqL_DataGridView_批次領藥_領藥總量清單.ClearGrid();
            

                    this.flag_批次領藥_頁面更新 = true;
                }
            }
            else
            {
                this.flag_批次領藥_頁面更新 = false;
            }

        }

        #endregion
        private void Program_批次領藥_Init()
        {
            this.sqL_DataGridView_批次領藥資料.Init();
            this.sqL_DataGridView_批次領藥_未領取領藥清單.Init();
            this.sqL_DataGridView_批次領藥_未領取領藥清單.DataGridRefreshEvent += SqL_DataGridView_批次領藥_領藥清單_DataGridRefreshEvent;
            this.sqL_DataGridView_批次領藥_已領取領藥清單.Init();

            this.sqL_DataGridView_批次領藥_領藥總量清單.Init();
            this.sqL_DataGridView_批次領藥_領藥總量清單.DataGridRefreshEvent += SqL_DataGridView_批次領藥_領藥總量清單_DataGridRefreshEvent;

            this.plC_UI_Init.Add_Method(this.sub_Program_批次領藥);
        }

        private void sub_Program_批次領藥()
        {
            sub_Program_批次領藥_頁面更新();
     
        }


        #region Event
        private void SqL_DataGridView_批次領藥_領藥總量清單_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows[i].Cells[(int)enum_領藥內容.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_批次領藥_領藥總量清單.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void SqL_DataGridView_批次領藥_領藥清單_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows[i].Cells[(int)enum_領藥內容.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_批次領藥_未領取領藥清單.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void plC_RJ_Button_批次領藥_重領已領取藥品_MouseDownEvent(MouseEventArgs mevent)
        {
        
        }
        #endregion
    }
}
