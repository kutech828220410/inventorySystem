﻿using System;
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
using SQLUI;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        [EnumDescription("")]
        public enum enum_儲位效期表 : int
        {
            [Description("儲位名稱,VARCHAR,300,NONE")]
            儲位名稱,
            [Description("藥品碼,VARCHAR,300,NONE")]
            藥品碼,
            [Description("藥品名稱,VARCHAR,300,NONE")]
            藥品名稱,
            [Description("單位,VARCHAR,300,NONE")]
            單位,
            [Description("效期,VARCHAR,300,NONE")]
            效期,
            [Description("庫存,VARCHAR,300,NONE")]
            庫存,
            [Description("儲位型式,VARCHAR,300,NONE")]
            儲位型式,
            [Description("IP,VARCHAR,300,NONE")]
            IP,
        }
        public enum enum_儲位效期表_儲位型式 : int
        {
            EPD583,
            小抽屜,
            面板層架,
            LED層架,
        }

        private void Program_藥品資料_儲位效期表_Init()
        {
            Table table = new Table(new enum_儲位效期表());
            this.sqL_DataGridView_藥品資料_儲位效期表.Init(table);
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnVisible(false, new enum_儲位效期表().GetEnumNames());
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位效期表.儲位名稱);
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_儲位效期表.藥品碼);
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_儲位效期表.藥品名稱);
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_儲位效期表.單位);
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleRight, enum_儲位效期表.效期);
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_儲位效期表.庫存);
            this.sqL_DataGridView_藥品資料_儲位效期表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_儲位效期表.儲位型式);
            this.sqL_DataGridView_藥品資料_儲位效期表.DataGridRowsChangeRefEvent += SqL_DataGridView_藥品資料_儲位效期表_DataGridRowsChangeRefEvent;

            this.plC_RJ_Button_藥品資料_儲位效期表_匯出資料.MouseDownEvent += PlC_RJ_Button_藥品資料_儲位效期表_匯出資料_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_儲位效期表_儲位名稱搜尋.MouseDownEvent += PlC_RJ_Button_藥品資料_儲位效期表_儲位名稱搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_儲位效期表_藥品碼搜尋.MouseDownEvent += PlC_RJ_Button_藥品資料_儲位效期表_藥品碼搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_儲位效期表_藥品名稱搜尋.MouseDownEvent += PlC_RJ_Button_藥品資料_儲位效期表_藥品名稱搜尋_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_儲位效期表_顯示全部.MouseDownEvent += PlC_RJ_Button_藥品資料_儲位效期表_顯示全部_MouseDownEvent;

            this.plC_UI_Init.Add_Method(this.sub_Program_藥品資料_儲位效期表);
        }



        bool flag_藥品資料_儲位效期表_頁面更新 = false;
        private void sub_Program_藥品資料_儲位效期表()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥品資料" && this.plC_ScreenPage_藥品資料.PageText == "儲位效期表")
            {
                if (!this.flag_藥品資料_儲位效期表_頁面更新)
                {
                    this.flag_藥品資料_儲位效期表_頁面更新 = true;
                }
            }
            else
            {
                this.flag_藥品資料_儲位效期表_頁面更新 = false;
            }


        }
        #region Function
        private List<object[]> Function_藥品資料_儲位效期表_取得資料()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            List<Device> devices = this.Function_從SQL取得所有儲位();
            Console.Write($"取得所有儲位,耗時{myTimer.ToString()}ms\n");
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < devices.Count; i++)
            {
                for (int k = 0; k < devices[i].List_Validity_period.Count; k++)
                {
                    object[] value = new object[new enum_儲位效期表().GetLength()];
                    if (devices[i].GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString().StringIsEmpty()) continue;
                    value[(int)enum_儲位效期表.IP] = devices[i].GetValue(Device.ValueName.IP, Device.ValueType.Value).ObjectToString();
                    value[(int)enum_儲位效期表.儲位名稱] = devices[i].GetValue(Device.ValueName.儲位名稱, Device.ValueType.Value).ObjectToString();
                    value[(int)enum_儲位效期表.藥品碼] = devices[i].GetValue(Device.ValueName.藥品碼, Device.ValueType.Value).ObjectToString();
                    value[(int)enum_儲位效期表.藥品名稱] = devices[i].GetValue(Device.ValueName.藥品名稱, Device.ValueType.Value).ObjectToString();
                    value[(int)enum_儲位效期表.單位] = devices[i].GetValue(Device.ValueName.包裝單位, Device.ValueType.Value).ObjectToString();
                    value[(int)enum_儲位效期表.效期] = devices[i].List_Validity_period[k];
                    value[(int)enum_儲位效期表.庫存] = devices[i].GetValue(Device.ValueName.庫存, Device.ValueType.Value).ObjectToString();
                    value[(int)enum_儲位效期表.儲位型式] = devices[i].DeviceType.GetEnumName();
                    list_value.Add(value);
                }

            }
            Console.Write($"計算所有儲位庫存,耗時{myTimer.ToString()}ms\n");
            return list_value;
        }
        #endregion
        #region Event
        private void SqL_DataGridView_藥品資料_儲位效期表_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList = RowsList.OrderBy(r => r[(int)enum_儲位效期表.藥品碼].ObjectToString()).ToList();
        }
        private void PlC_RJ_Button_藥品資料_儲位效期表_藥品名稱搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.Function_藥品資料_儲位效期表_取得資料();

            list_value = list_value.GetRowsByLike((int)enum_儲位效期表.藥品名稱, rJ_TextBox_藥品資料_儲位效期表_藥品名稱搜尋.Texts);
            this.sqL_DataGridView_藥品資料_儲位效期表.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥品資料_儲位效期表_藥品碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.Function_藥品資料_儲位效期表_取得資料();

            list_value = list_value.GetRowsByLike((int)enum_儲位效期表.藥品碼, rJ_TextBox_藥品資料_儲位效期表_藥品碼搜尋.Texts);
            this.sqL_DataGridView_藥品資料_儲位效期表.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥品資料_儲位效期表_儲位名稱搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.Function_藥品資料_儲位效期表_取得資料();

            list_value = list_value.GetRowsByLike((int)enum_儲位效期表.儲位名稱, rJ_TextBox_藥品資料_儲位效期表_儲位名稱搜尋.Texts);
            this.sqL_DataGridView_藥品資料_儲位效期表.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥品資料_儲位效期表_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.Function_藥品資料_儲位效期表_取得資料();

            this.sqL_DataGridView_藥品資料_儲位效期表.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥品資料_儲位效期表_匯出資料_MouseDownEvent(MouseEventArgs mevent)
        {
            saveFileDialog_SaveExcel.OverwritePrompt = false;
            this.Invoke(new Action(delegate
            {
                if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
                {

                    DataTable datatable = new DataTable();
                    datatable = sqL_DataGridView_藥品資料_儲位效期表.GetDataTable(false);
                    CSVHelper.SaveFile(datatable, saveFileDialog_SaveExcel.FileName);
                    MyMessageBox.ShowDialog("匯出完成!");
                }
            }));
        }

        #endregion

    }
}
