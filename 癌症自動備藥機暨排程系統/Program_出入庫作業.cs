using System;
using System.IO;
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
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;
using DrawingClass;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Program_出入庫作業_Init()
        {

            string url = $"{API_Server}/api/MED_page/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"本地藥檔表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }

            this.sqL_DataGridView_出入庫作業.Init(table);
            this.sqL_DataGridView_出入庫作業.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
            this.sqL_DataGridView_出入庫作業.RowPostPaintingEvent += SqL_DataGridView_出入庫作業_RowPostPaintingEvent;

            this.plC_RJ_Button_出入庫作業_藥碼搜尋.MouseDownEvent += PlC_RJ_Button_出入庫作業_藥碼搜尋_MouseDownEvent;
            this.plC_RJ_Button_出入庫作業_藥名搜尋.MouseDownEvent += PlC_RJ_Button_出入庫作業_藥名搜尋_MouseDownEvent;
            this.plC_RJ_Button_出入庫作業_入庫.MouseDownEvent += PlC_RJ_Button_出入庫作業_入庫_MouseDownEvent;
            this.plC_RJ_Button_出入庫作業_出庫.MouseDownEvent += PlC_RJ_Button_出入庫作業_出庫_MouseDownEvent;
            this.plC_RJ_Button_出入庫作業_確認選擇.MouseDownEvent += PlC_RJ_Button_出入庫作業_確認選擇_MouseDownEvent;

            plC_UI_Init.Add_Method(Program_出入庫作業);
        }

      

        private void Program_出入庫作業()
        {

        }


        #region Event
        private void SqL_DataGridView_出入庫作業_RowPostPaintingEvent(DataGridViewRowPostPaintEventArgs e)
        {
            object[] value = this.sqL_DataGridView_出入庫作業.GetRowValues(e.RowIndex);
            if (value != null)
            {
                Color row_Backcolor = Color.White;
                Color row_Forecolor = Color.Black;
                using (Brush brush = new SolidBrush(row_Backcolor))
                {
                    int x = e.RowBounds.Left;
                    int y = e.RowBounds.Top;
                    int width = e.RowBounds.Width;
                    int height = e.RowBounds.Height;
                    e.Graphics.FillRectangle(brush, e.RowBounds);
                    DrawingClass.Draw.DrawRoundShadow(e.Graphics, new RectangleF(x - 1, y - 1, width, height), Color.DarkGray, 5, 5);

                    Size size = new Size();
                    PointF pointF = new PointF();

                    string 藥碼 = $"({value[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString()})";
                    DrawingClass.Draw.文字左上繪製(藥碼, new PointF(10, y + 10), new Font("標楷體", 16 , FontStyle.Bold), Color.Black, e.Graphics);
                    size = 藥碼.MeasureText(new Font("標楷體", 16, FontStyle.Bold));

                    string 藥名 = $"{value[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString()}";
                    DrawingClass.Draw.文字左上繪製(藥名, new PointF(10 + size.Width, y + 10), new Font("標楷體", 16), Color.Black, e.Graphics);
                  
                    string 單位 = $"[{value[(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString()}]";
                    DrawingClass.Draw.文字左上繪製(單位, new PointF(10 + 650, y + 10), new Font("標楷體", 16), Color.Black, e.Graphics);

                    string 庫存 = $"庫存:{value[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString()}";
                    size = 庫存.MeasureText(new Font("標楷體", 16, FontStyle.Bold));
                    DrawingClass.Draw.文字左上繪製(庫存, new PointF(e.RowBounds.Width - size.Width - 10, y + 10), new Font("標楷體", 16), Color.Black, e.Graphics);


                }
            }
        }
        private void PlC_RJ_Button_出入庫作業_出庫_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_出入庫作業_入庫.Bool = false;
            plC_RJ_Button_出入庫作業_出庫.Bool = true;
        }

        private void PlC_RJ_Button_出入庫作業_入庫_MouseDownEvent(MouseEventArgs mevent)
        {
            plC_RJ_Button_出入庫作業_入庫.Bool = true;
            plC_RJ_Button_出入庫作業_出庫.Bool = false;
        }

        private void PlC_RJ_Button_出入庫作業_藥名搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<medClass> medClasses = Function_取得有儲位藥檔資料();
            List<object[]> list_value = medClasses.ClassToSQL<medClass , enum_藥品資料_藥檔資料>();
            string 藥名 = rJ_TextBox_出入庫作業_藥名搜尋.Texts;
            if(藥名.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品名稱, 藥名);
            }

            this.sqL_DataGridView_出入庫作業.RefreshGrid(list_value);

        }
        private void PlC_RJ_Button_出入庫作業_藥碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<medClass> medClasses = Function_取得有儲位藥檔資料();
            List<object[]> list_value = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
            string 藥品碼 = rJ_TextBox_出入庫作業_藥碼搜尋.Texts;
            if (藥品碼.StringIsEmpty() == false)
            {
                list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);
            }

            this.sqL_DataGridView_出入庫作業.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_出入庫作業_確認選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            if (plC_RJ_Button_出入庫作業_入庫.Bool == false && plC_RJ_Button_出入庫作業_出庫.Bool == false)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請選取[出庫][入庫]", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<object[]> list_value = sqL_DataGridView_出入庫作業.Get_All_Select_RowsValues();

            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_value[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            string 藥品名稱 = list_value[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
            Dialog_入出庫作業.enum_type enum_Type = new Dialog_入出庫作業.enum_type();
            if (plC_RJ_Button_出入庫作業_入庫.Bool) enum_Type = Dialog_入出庫作業.enum_type.入庫;
            if (plC_RJ_Button_出入庫作業_出庫.Bool) enum_Type = Dialog_入出庫作業.enum_type.出庫;
            Dialog_入出庫作業 dialog_入出庫作業 = new Dialog_入出庫作業(enum_Type ,this.登入者名稱, 藥碼, 藥品名稱, storageUI_EPD_266);
            dialog_入出庫作業.ShowDialog();

            List<medClass> medClasses = Function_取得有儲位藥檔資料();
            List<object[]> list_medClasses = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
            List<object[]> list_medClasses_buf = new List<object[]>();
            list_medClasses_buf = list_medClasses.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥碼);
            if(list_medClasses_buf.Count > 0)
            {
                this.sqL_DataGridView_出入庫作業.ReplaceExtra(list_medClasses_buf[0], true);
            }
        }
        #endregion
    }
}
