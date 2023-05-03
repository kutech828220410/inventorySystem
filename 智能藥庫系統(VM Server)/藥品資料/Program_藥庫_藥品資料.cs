﻿using System;
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
namespace 智能藥庫系統_VM_Server_
{
    public partial class Form1 : Form
    {
        public enum enum_藥庫_藥品資料
        {
            GUID,
            藥品碼,
            中文名稱,
            藥品名稱,
            藥品學名,
            藥品群組,
            健保碼,
            包裝單位,
            包裝數量,
            最小包裝單位,
            最小包裝數量,
            藥局庫存,
            藥庫庫存,
            總庫存,
            基準量,
            安全庫存,
            藥品條碼1,
            藥品條碼2,
            狀態,
        }
        private void sub_Program_藥庫_藥品資料_Init()
        {
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_藥庫_藥品資料, dBConfigClass.DB_DS01);
            this.sqL_DataGridView_藥庫_藥品資料.Init();
            this.sqL_DataGridView_藥庫_藥品資料.DataGridRowsChangeEvent += SqL_DataGridView_藥庫_藥品資料_DataGridRowsChangeEvent;

            this.plC_RJ_Button_藥庫_藥品資料_顯示全部.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_顯示全部_MouseDownEvent;
            this.plC_UI_Init.Add_Method(sub_Program_藥庫_藥品資料);
        }

    

        private bool flag_藥庫_藥品資料 = false;
        private void sub_Program_藥庫_藥品資料()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥品資料")
            {
                if (!this.flag_藥庫_藥品資料)
                {
                    this.flag_藥庫_藥品資料 = true;
                }

            }
            else
            {
                this.flag_藥庫_藥品資料 = false;
            }
        }

        #region Function
        private void Function_藥庫_藥品資料_檢查表格()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(10000);
            List<object[]> list_本地藥檔 = this.sqL_DataGridView_本地_藥品資料.SQL_GetAllRows(false);
            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
            Console.Write($"取得 藥庫-藥品資料,耗時 : {myTimer.ToString()} ms\n");
            List<object[]> list_Add = new List<object[]>();
            List<object> list_Delete_SerchValue = new List<object>();
            List<string[]> list_Replace_SerchValue = new List<string[]>();
            List<object[]> list_Replace_Value = new List<object[]>();

            Parallel.ForEach(list_本地藥檔, value =>
            {
                List<object[]> list_藥品資料_buf = new List<object[]>();
                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥庫_藥品資料.藥品碼, value[(int)enum_本地_藥品資料.藥品碼].ObjectToString());
                object[] src_value = LINQ.CopyRow(value, new enum_本地_藥品資料(), new enum_藥庫_藥品資料());
                if (list_藥品資料_buf.Count > 0)
                {
                    object[] dst_value = LINQ.CopyRow(list_藥品資料_buf[0], new enum_藥庫_藥品資料(), new enum_藥庫_藥品資料());
                    src_value[(int)enum_藥庫_藥品資料.GUID] = dst_value[(int)enum_藥庫_藥品資料.GUID];
                    src_value[(int)enum_藥庫_藥品資料.藥局庫存] = dst_value[(int)enum_藥庫_藥品資料.藥局庫存];
                    src_value[(int)enum_藥庫_藥品資料.藥庫庫存] = dst_value[(int)enum_藥庫_藥品資料.藥庫庫存];
                    src_value[(int)enum_藥庫_藥品資料.總庫存] = dst_value[(int)enum_藥庫_藥品資料.總庫存];
                    src_value[(int)enum_藥庫_藥品資料.基準量] = dst_value[(int)enum_藥庫_藥品資料.基準量];
                    src_value[(int)enum_藥庫_藥品資料.安全庫存] = dst_value[(int)enum_藥庫_藥品資料.安全庫存];
                    bool flag_IsEqual = src_value.IsEqual(dst_value, (int)enum_藥庫_藥品資料.藥局庫存, (int)enum_藥庫_藥品資料.包裝數量, (int)enum_藥庫_藥品資料.藥庫庫存, (int)enum_藥庫_藥品資料.總庫存, (int)enum_藥庫_藥品資料.基準量, (int)enum_藥庫_藥品資料.安全庫存);
                    if (src_value[(int)enum_藥庫_藥品資料.藥庫庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_藥庫_藥品資料.藥庫庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_藥庫_藥品資料.藥局庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_藥庫_藥品資料.藥局庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_藥庫_藥品資料.總庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_藥庫_藥品資料.總庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_藥庫_藥品資料.基準量].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_藥庫_藥品資料.基準量] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_藥庫_藥品資料.安全庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_藥庫_藥品資料.安全庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (!flag_IsEqual)
                    {
                        list_Replace_SerchValue.LockAdd(new string[] { src_value[(int)enum_藥庫_藥品資料.GUID].ObjectToString() });
                        list_Replace_Value.LockAdd(src_value);
                    }

                }
                else
                {
                    src_value[(int)enum_藥庫_藥品資料.總庫存] = "0";
                    src_value[(int)enum_藥庫_藥品資料.藥庫庫存] = "0";
                    src_value[(int)enum_藥庫_藥品資料.藥局庫存] = "0";
                    src_value[(int)enum_藥庫_藥品資料.安全庫存] = "0";
                    src_value[(int)enum_藥庫_藥品資料.基準量] = "0";
                    list_Add.LockAdd(src_value);
                }
            });
            Console.Write($"計算 藥庫-藥品資料,耗時 : {myTimer.ToString()} ms\n");
            Parallel.ForEach(list_藥品資料, value =>
            {
                List<object[]> list_本地藥檔_buf = list_本地藥檔.GetRows((int)enum_本地_藥品資料.藥品碼, value[(int)enum_藥庫_藥品資料.藥品碼].ObjectToString());
                if (list_本地藥檔_buf.Count == 0)
                {
                    list_Delete_SerchValue.LockAdd(value[(int)enum_藥庫_藥品資料.GUID]);
                }
            });
            Console.Write($"刪除 藥庫-藥品資料 多餘資料{list_Delete_SerchValue.Count}筆,耗時 : {myTimer.ToString()} ms\n");
            this.sqL_DataGridView_藥庫_藥品資料.SQL_DeleteExtra(list_Delete_SerchValue, false);
            this.sqL_DataGridView_藥庫_藥品資料.SQL_ReplaceExtra(list_Replace_Value, false);
            this.sqL_DataGridView_藥庫_藥品資料.SQL_AddRows(list_Add, false);
            Console.Write($"上傳 藥庫-藥品資料 耗時 : {myTimer.ToString()} ms\n");

            this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(true);
            Console.Write($"更新 藥庫-藥品資料Grid 耗時 : {myTimer.ToString()} ms\n");
        }
        private void Function_藥庫_藥品資料_檢查DeviceBasic()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.TickStop();
            myTimer.StartTickTime(100000);
            this.List_藥庫_DeviceBasic = DeviceBasicClass_藥庫.SQL_GetAllDeviceBasic();
            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);

            List<DeviceBasic> devices_Add = new List<DeviceBasic>();
            List<DeviceBasic> devices_Replace = new List<DeviceBasic>();

            Parallel.ForEach(list_藥品資料, value =>
            {
                string 藥品碼 = value[(int)enum_藥庫_藥品資料.藥品碼].ObjectToString();
                List<DeviceBasic> devices_buf = (from Value in this.List_藥庫_DeviceBasic
                                                 where Value.Code == 藥品碼
                                                 select Value).ToList();
                if (devices_buf.Count == 0)
                {
                    DeviceBasic device = new DeviceBasic();
                    device.Code = 藥品碼;
                    devices_Add.LockAdd(device);
                }
            });


            DeviceBasicClass_藥庫.SQL_AddDeviceBasic(devices_Add);
            Console.Write($"儲位總量新增時間 ,耗時 :{myTimer.GetTickTime().ToString("0.000")}\n");
        }
        #endregion
        #region Event
        private void PlC_RJ_Button_藥庫_藥品資料_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(true);
        }
        private void SqL_DataGridView_藥庫_藥品資料_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
            this.List_藥庫_DeviceBasic = DeviceBasicClass_藥庫.SQL_GetAllDeviceBasic();
            this.List_藥局_DeviceBasic = DeviceBasicClass_藥局.SQL_GetAllDeviceBasic();
            this.List_Pannel35_本地資料 = this.storageUI_WT32.SQL_GetAllStorage();
            Parallel.ForEach(RowsList, value =>
            {
                string 藥品碼 = value[(int)enum_藥庫_藥品資料.藥品碼].ObjectToString();

                int 總庫存 = 0;
                int 藥庫庫存 = 0;
                int 藥局庫存 = 0;
                List<DeviceBasic> deviceBasic_藥庫_buf = this.List_藥庫_DeviceBasic.SortByCode(藥品碼);
                List<DeviceBasic> deviceBasic_藥局_buf = this.List_藥局_DeviceBasic.SortByCode(藥品碼);
                List<Storage> storages_buf = this.List_Pannel35_本地資料.SortByCode(藥品碼);

                for (int i = 0; i < deviceBasic_藥庫_buf.Count; i++)
                {

                    總庫存 += deviceBasic_藥庫_buf[i].Inventory.StringToInt32();
                    藥庫庫存 += deviceBasic_藥庫_buf[i].Inventory.StringToInt32();
                }
                for (int i = 0; i < deviceBasic_藥局_buf.Count; i++)
                {
                    總庫存 += deviceBasic_藥局_buf[i].Inventory.StringToInt32();
                    藥局庫存 += deviceBasic_藥局_buf[i].Inventory.StringToInt32();
                }
                for (int i = 0; i < storages_buf.Count; i++)
                {
                    總庫存 += storages_buf[i].Inventory.StringToInt32();
                    藥庫庫存 += storages_buf[i].Inventory.StringToInt32();
                }
                //庫存 = this.Function_從本地資料取得庫存(藥品碼);

                value[(int)enum_藥庫_藥品資料.藥庫庫存] = 藥庫庫存;
                value[(int)enum_藥庫_藥品資料.藥局庫存] = 藥局庫存;
                value[(int)enum_藥庫_藥品資料.總庫存] = 總庫存;
            });

            RowsList.Sort(new ICP_藥庫_藥品資料());
        }
        #endregion
        private class ICP_藥庫_藥品資料 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string Code0 = x[(int)enum_藥庫_藥品資料.藥品碼].ObjectToString();
                string Code1 = y[(int)enum_藥庫_藥品資料.藥品碼].ObjectToString();
                return Code0.CompareTo(Code1);
            }
        }
    }
}
