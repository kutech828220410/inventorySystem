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
using MyUI;
using Basic;
using H_Pannel_lib;
using HIS_DB_Lib;
using SQLUI;
namespace 智能藥庫系統
{
    public partial class Form1 : Form
    {
        private DeviceBasicClass DeviceBasicClass_藥庫 = new DeviceBasicClass();

        private enum enum_medDrugstore_效期及庫存
        {
            效期,
            批號,
            庫存,
        }
     
        public enum enum_medDrugstore_匯出
        {
            藥品碼,
            中文名稱,
            藥品名稱,
            藥品學名,
            總庫存,
            包裝單位,
            包裝數量,
            基準量,
            安全庫存,
        }
        public enum enum_medDrugstore_匯入
        {
            藥品碼,
            基準量,
            安全庫存,
            包裝數量,
        }
        public enum enum_medDrugstore_匯入_安全基準量
        {
            藥品碼,
            基準量,
            安全庫存,

        }
  
        public enum ContextMenuStrip_藥庫_藥品資料
        {
            匯出,          
            匯入,
            匯出選取資料,
            設定基準量,
            設定安全庫存,
        }
        private void sub_Program_藥庫_藥品資料_Init()
        {
            this.DeviceBasicClass_藥庫.Init(dBConfigClass.DB_Basic, "firstclass_device_jsonstring");

            string url = $"{dBConfigClass.Api_URL}/api/MED_page/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.藥庫.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.TableName = "medicine_page_firstclass";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            if (table == null)
            {
                MyMessageBox.ShowDialog($"藥庫-藥品資料表單建立失敗!! Api_URL:{dBConfigClass.Api_URL}");
                return;
            }
            this.sqL_DataGridView_藥庫_藥品資料.Init(table);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnVisible(false, new enum_medDrugstore().GetEnumNames());
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.藥品碼);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.料號);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_medDrugstore.藥品名稱);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_medDrugstore.藥品學名);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_medDrugstore.中文名稱);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.包裝單位);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.包裝數量);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.基準量);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.安全庫存);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.藥局庫存);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.藥庫庫存);
            this.sqL_DataGridView_藥庫_藥品資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_medDrugstore.總庫存);

            this.sqL_DataGridView_藥庫_藥品資料.DataGridRowsChangeEvent += SqL_DataGridView_藥庫_藥品資料_DataGridRowsChangeEvent;
            this.sqL_DataGridView_藥庫_藥品資料.RowEnterEvent += SqL_DataGridView_藥庫_藥品資料_RowEnterEvent;
            this.sqL_DataGridView_藥庫_藥品資料_效期及庫存.Init();


            this.plC_RJ_Button_藥庫_藥品資料_匯出.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_匯出_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_匯入.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_匯入_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_匯出選取資料.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_匯出選取資料_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_設定基準量.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_設定基準量_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_設定安全庫存.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_設定安全庫存_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_新增效期.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_新增效期_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_修正庫存.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_修正庫存_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_修正批號.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_修正批號_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_測試清除所有效期資料.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_測試清除所有效期資料_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_顯示有庫存藥品.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_顯示有庫存藥品_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_設定包裝數量.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_設定包裝數量_MouseDownEvent;
            this.plC_RJ_Button_藥庫_藥品資料_匯入安全基準量.MouseDownEvent += PlC_RJ_Button_藥庫_藥品資料_匯入安全基準量_MouseDownEvent;


            this.plC_UI_Init.Add_Method(sub_Program_藥庫_藥品資料);
        }

     

        private bool flag_Program_藥庫_藥品資料_Init = false;
        private void sub_Program_藥庫_藥品資料()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥庫" && this.plC_ScreenPage_藥庫.PageText == "藥品資料")
            {
                if(!flag_Program_藥庫_藥品資料_Init)
                {
                    this.Function_藥庫_藥品資料_檢查表格();
                    this.Function_藥庫_藥品資料_檢查DeviceBasic();
                    flag_Program_藥庫_藥品資料_Init = true;
                }            
            }
            else
            {
                flag_Program_藥庫_藥品資料_Init = false;
            }
        }
        #region Function
        private void Function_藥庫_藥品資料_檢查表格()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(10000);
            List<object[]> list_雲端藥檔 = this.sqL_DataGridView_藥品資料_資料維護_雲端藥檔.SQL_GetAllRows(false);
            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
            Console.Write($"取得 藥庫-藥品資料,耗時 : {myTimer.ToString()} ms\n");
            List<object[]> list_Add = new List<object[]>();
            List<object> list_Delete_SerchValue = new List<object>();
            List<string[]> list_Replace_SerchValue = new List<string[]>();
            List<object[]> list_Replace_Value = new List<object[]>();

            Parallel.ForEach(list_雲端藥檔, value =>
            {
                List<object[]> list_藥品資料_buf = new List<object[]>();
                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_medDrugstore.藥品碼, value[(int)enum_雲端藥檔.藥品碼].ObjectToString());
                object[] src_value = LINQ.CopyRow(value, new enum_雲端藥檔(), new enum_medDrugstore());
                if (list_藥品資料_buf.Count > 0)
                {
                    object[] dst_value = LINQ.CopyRow(list_藥品資料_buf[0], new enum_medDrugstore(), new enum_medDrugstore());
                    src_value[(int)enum_medDrugstore.GUID] = dst_value[(int)enum_medDrugstore.GUID];
                    src_value[(int)enum_medDrugstore.藥局庫存] = dst_value[(int)enum_medDrugstore.藥局庫存];
                    src_value[(int)enum_medDrugstore.藥庫庫存] = dst_value[(int)enum_medDrugstore.藥庫庫存];
                    src_value[(int)enum_medDrugstore.總庫存] = dst_value[(int)enum_medDrugstore.總庫存];
                    src_value[(int)enum_medDrugstore.基準量] = dst_value[(int)enum_medDrugstore.基準量];
                    src_value[(int)enum_medDrugstore.安全庫存] = dst_value[(int)enum_medDrugstore.安全庫存];
                    bool flag_IsEqual = src_value.IsEqual(dst_value, (int)enum_medDrugstore.包裝數量, (int)enum_medDrugstore.藥庫庫存, (int)enum_medDrugstore.藥局庫存, (int)enum_medDrugstore.藥庫庫存, (int)enum_medDrugstore.總庫存, (int)enum_medDrugstore.基準量, (int)enum_medDrugstore.安全庫存);
                    if (src_value[(int)enum_medDrugstore.藥庫庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_medDrugstore.藥庫庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_medDrugstore.藥局庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_medDrugstore.藥局庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_medDrugstore.總庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_medDrugstore.總庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_medDrugstore.基準量].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_medDrugstore.基準量] = "0";
                        flag_IsEqual = false;
                    }
                    if (src_value[(int)enum_medDrugstore.安全庫存].ObjectToString().StringIsEmpty())
                    {
                        src_value[(int)enum_medDrugstore.安全庫存] = "0";
                        flag_IsEqual = false;
                    }
                    if (!flag_IsEqual)
                    {
                        list_Replace_SerchValue.LockAdd(new string[] { src_value[(int)enum_medDrugstore.GUID].ObjectToString() });
                        list_Replace_Value.LockAdd(src_value);
                    }

                }
                else
                {
                    src_value[(int)enum_medDrugstore.總庫存] = "0";
                    src_value[(int)enum_medDrugstore.藥庫庫存] = "0";
                    src_value[(int)enum_medDrugstore.藥局庫存] = "0";
                    src_value[(int)enum_medDrugstore.安全庫存] = "0";
                    src_value[(int)enum_medDrugstore.基準量] = "0";
                    list_Add.LockAdd(src_value);
                }
            });
            Console.Write($"計算 藥庫-藥品資料,耗時 : {myTimer.ToString()} ms\n");
            Parallel.ForEach(list_藥品資料, value =>
            {
                List<object[]> list_雲端藥檔_buf = list_雲端藥檔.GetRows((int)enum_雲端藥檔.藥品碼, value[(int)enum_medDrugstore.藥品碼].ObjectToString());
                if (list_雲端藥檔_buf.Count == 0)
                {
                    list_Delete_SerchValue.LockAdd(value[(int)enum_medDrugstore.GUID]);
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
        private void Function_藥庫_藥品資料_匯入()
        {
            if (this.openFileDialog_LoadExcel.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = new DataTable();
                CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                DataTable datatable_buf = dataTable.ReorderTable(new enum_medDrugstore_匯入());
                if (datatable_buf == null)
                {
                    MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                    return;
                }
                List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();

                List<object[]> list_SQL_Value = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
                List<object[]> list_Add = new List<object[]>();
                List<object[]> list_Delete_ColumnName = new List<object[]>();
                List<object[]> list_Delete_SerchValue = new List<object[]>();
                List<string> list_Replace_SerchValue = new List<string>();
                List<object[]> list_Replace_Value = new List<object[]>();
                List<object[]> list_SQL_Value_buf = new List<object[]>();

                for (int i = 0; i < list_LoadValue.Count; i++)
                {
                    object[] value_load = list_LoadValue[i];
                    list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_medDrugstore.藥品碼, value_load[(int)enum_medDrugstore_匯入.藥品碼].ObjectToString());
                    if (list_SQL_Value_buf.Count > 0)
                    {
                        object[] value_SQL = list_SQL_Value_buf[0];
                        value_load = value_load.CopyRow(new enum_medDrugstore_匯入(), new enum_medDrugstore());
                        value_SQL.CopyRow(ref value_load ,new enum_medDrugstore(), new enum_medDrugstore(), (int)enum_medDrugstore.藥庫庫存, (int)enum_medDrugstore.總庫存, (int)enum_medDrugstore.安全庫存, (int)enum_medDrugstore.基準量, (int)enum_medDrugstore.藥品條碼1, (int)enum_medDrugstore.藥品條碼2);
                        bool flag_Equal = value_load.IsEqual(value_SQL);
                        if(!flag_Equal)
                        {
                            list_Replace_SerchValue.Add(value_load[(int)enum_medDrugstore.GUID].ObjectToString());
                            list_Replace_Value.Add(value_load);
                        }
                    }
                }
                this.sqL_DataGridView_藥庫_藥品資料.SQL_ReplaceExtra(enum_medDrugstore.GUID.GetEnumName(), list_Replace_SerchValue, list_Replace_Value, true);
            }
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
                string 藥品碼 = value[(int)enum_medDrugstore.藥品碼].ObjectToString();
                string 藥品名稱 = value[(int)enum_medDrugstore.藥品名稱].ObjectToString();
                string 藥品學名 = value[(int)enum_medDrugstore.藥品學名].ObjectToString();
                string 中文名稱 = value[(int)enum_medDrugstore.中文名稱].ObjectToString();
                string 包裝單位 = value[(int)enum_medDrugstore.包裝單位].ObjectToString();
                string 藥品條碼1 = value[(int)enum_medDrugstore.藥品條碼1].ObjectToString();
                string 藥品條碼2 = value[(int)enum_medDrugstore.藥品條碼2].ObjectToString();
                string 料號 = value[(int)enum_medDrugstore.料號].ObjectToString();
                List<DeviceBasic> devices_buf = (from Value in this.List_藥庫_DeviceBasic
                                                 where Value.Code == 藥品碼
                                                 select Value).ToList();
                if (devices_buf.Count == 0)
                {
                    DeviceBasic device = new DeviceBasic();
                    device.Code = 藥品碼;
                    device.SKDIACODE = 料號;
                    device.Name = 藥品名稱;
                    device.ChineseName = 中文名稱;
                    device.Package = 包裝單位;
                    device.BarCode1 = 藥品條碼1;
                    device.BarCode2 = 藥品條碼2;
                    devices_Add.LockAdd(device);
                }
                else
                {

                }
            });


            DeviceBasicClass_藥庫.SQL_AddDeviceBasic(devices_Add);
            Console.Write($"儲位總量新增時間 ,耗時 :{myTimer.GetTickTime().ToString("0.000")}\n");
        }
        #endregion
        #region Event
        private void sqL_DataGridView_藥庫_藥品資料_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_藥庫_藥品資料().GetEnumNames());
                if (dialog_ContextMenuStrip.ShowDialog() == DialogResult.Yes)
                {
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥庫_藥品資料.匯出選取資料.GetEnumName())
                    {
                        PlC_RJ_Button_藥庫_藥品資料_匯出選取資料_MouseDownEvent(null);
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥庫_藥品資料.匯出.GetEnumName())
                    {
                        PlC_RJ_Button_藥庫_藥品資料_匯出_MouseDownEvent(null);
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥庫_藥品資料.匯入.GetEnumName())
                    {
                        PlC_RJ_Button_藥庫_藥品資料_匯入_MouseDownEvent(null);
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥庫_藥品資料.設定基準量.GetEnumName())
                    {
                        PlC_RJ_Button_藥庫_藥品資料_設定基準量_MouseDownEvent(null);
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥庫_藥品資料.設定安全庫存.GetEnumName())
                    {
                        PlC_RJ_Button_藥庫_藥品資料_設定安全庫存_MouseDownEvent(null);
                    }
               
                }
            }
     
        }
        private void SqL_DataGridView_藥庫_藥品資料_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
            this.List_藥庫_DeviceBasic = DeviceBasicClass_藥庫.SQL_GetAllDeviceBasic();
            this.List_藥局_DeviceBasic = DeviceBasicClass_藥局.SQL_GetAllDeviceBasic();
            this.List_Pannel35_本地資料 = this.storageUI_WT32.SQL_GetAllStorage();
            Parallel.ForEach(RowsList, value =>
            {
                string 藥品碼 = value[(int)enum_medDrugstore.藥品碼].ObjectToString();
  
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

                value[(int)enum_medDrugstore.藥庫庫存] = 藥庫庫存;
                value[(int)enum_medDrugstore.藥局庫存] = 藥局庫存;
                value[(int)enum_medDrugstore.總庫存] = 總庫存;
            });

            Finction_藥品資料_藥品群組_序號轉名稱(RowsList, (int)enum_medDrugstore.藥品群組);
            RowsList.Sort(new ICP_藥庫_藥品資料());
        }
        private void SqL_DataGridView_藥庫_藥品資料_RowEnterEvent(object[] RowValue)
        {
            this.sqL_DataGridView_藥庫_藥品資料_效期及庫存.ClearGrid();
            string 藥品碼 = RowValue[(int)enum_medDrugstore.藥品碼].ObjectToString();
            List<DeviceBasic> deviceBasic_buf = this.List_藥庫_DeviceBasic.SortByCode(藥品碼);
            if (deviceBasic_buf.Count == 0) return;

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < deviceBasic_buf[0].List_Validity_period.Count; i++)
            {
                object[] value = new object[new enum_medDrugstore_效期及庫存().GetLength()];
                value[(int)enum_medDrugstore_效期及庫存.效期] = deviceBasic_buf[0].List_Validity_period[i];
                value[(int)enum_medDrugstore_效期及庫存.批號] = deviceBasic_buf[0].List_Lot_number[i];
                value[(int)enum_medDrugstore_效期及庫存.庫存] = deviceBasic_buf[0].List_Inventory[i];
                list_value.Add(value);
            }

            this.sqL_DataGridView_藥庫_藥品資料_效期及庫存.RefreshGrid(list_value);
        }
        private void plC_RJ_ComboBox_藥庫_藥品資料_藥品群組_Enter(object sender, EventArgs e)
        {
            plC_RJ_ComboBox_藥庫_藥品資料_藥品群組.SetDataSource(Function_藥品資料_藥品群組_取得選單());
        }

        private void plC_RJ_Button_藥庫_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
            if (!this.rJ_TextBox_藥庫_藥品資料_藥品碼.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_medDrugstore.藥品碼, this.rJ_TextBox_藥庫_藥品資料_藥品碼.Text);
            }
            if (!this.rJ_TextBox_藥庫_藥品資料_中文名稱.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_medDrugstore.中文名稱, this.rJ_TextBox_藥庫_藥品資料_中文名稱.Text);
            }
            if (!this.rJ_TextBox_藥庫_藥品資料_中文名稱.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_medDrugstore.中文名稱, this.rJ_TextBox_藥庫_藥品資料_中文名稱.Text);
            }
            if (!this.rJ_TextBox_藥庫_藥品資料_藥品名稱.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_medDrugstore.藥品名稱, this.rJ_TextBox_藥庫_藥品資料_藥品名稱.Text);
            }
            if (!this.rJ_TextBox_藥庫_藥品資料_藥品學名.Text.StringIsEmpty())
            {
                list_value = list_value.GetRowsByLike((int)enum_medDrugstore.藥品學名, this.rJ_TextBox_藥庫_藥品資料_藥品學名.Text);
            }
            if (plC_RJ_ChechBox_藥庫_藥品資料_藥品群組.Checked)
            {
                string[] strArray = myConvert.分解分隔號字串(plC_RJ_ComboBox_藥庫_藥品資料_藥品群組.Texts, ".");
                int 群組序號 = strArray[0].StringToInt32();
                if (群組序號 >= 1 && 群組序號 <= 20)
                {
                    list_value = list_value.GetRows((int)enum_medDrugstore.藥品群組, 群組序號.ToString("00"));
                }
            }
            this.sqL_DataGridView_藥庫_藥品資料.RefreshGrid(list_value);
        }

        private void PlC_RJ_Button_藥庫_藥品資料_設定安全庫存_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                {
                    int num = dialog_NumPannel.Value;
                    List<object[]> list_value = this.sqL_DataGridView_藥庫_藥品資料.Get_All_Select_RowsValues();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();

                    for (int i = 0; i < list_value.Count; i++)
                    {
                        list_value[i][(int)enum_medDrugstore.安全庫存] = num.ToString();

                        list_Replace_SerchValue.Add(list_value[i][(int)enum_medDrugstore.GUID].ObjectToString());
                        list_Replace_Value.Add(list_value[i]);
                    }
                    this.sqL_DataGridView_藥庫_藥品資料.SQL_ReplaceExtra(list_Replace_Value, false);
                    this.sqL_DataGridView_藥庫_藥品資料.ReplaceExtra(list_Replace_Value, true);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_設定包裝數量_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                {
                    int num = dialog_NumPannel.Value;
                    List<object[]> list_value = this.sqL_DataGridView_藥庫_藥品資料.Get_All_Select_RowsValues();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();
                    if (num <= 0) return;
                    for (int i = 0; i < list_value.Count; i++)
                    {
                        list_value[i][(int)enum_medDrugstore.包裝數量] = num.ToString();

                        list_Replace_SerchValue.Add(list_value[i][(int)enum_medDrugstore.GUID].ObjectToString());
                        list_Replace_Value.Add(list_value[i]);
                    }
                    this.sqL_DataGridView_藥庫_藥品資料.SQL_ReplaceExtra(list_Replace_Value, false);
                    this.sqL_DataGridView_藥庫_藥品資料.ReplaceExtra(list_Replace_Value, true);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_設定基準量_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                {
                    int num = dialog_NumPannel.Value;
                    List<object[]> list_value = this.sqL_DataGridView_藥庫_藥品資料.Get_All_Select_RowsValues();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();

                    for (int i = 0; i < list_value.Count; i++)
                    {
                        list_value[i][(int)enum_medDrugstore.基準量] = num.ToString();

                        list_Replace_SerchValue.Add(list_value[i][(int)enum_medDrugstore.GUID].ObjectToString());
                        list_Replace_Value.Add(list_value[i]);
                    }
                    this.sqL_DataGridView_藥庫_藥品資料.SQL_ReplaceExtra(list_Replace_Value, false);
                    this.sqL_DataGridView_藥庫_藥品資料.ReplaceExtra(list_Replace_Value, true);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_匯出選取資料_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dataTable = this.sqL_DataGridView_藥庫_藥品資料.GetSelectRowsDataTable();
                    dataTable = dataTable.ReorderTable(new enum_medDrugstore_匯出());
                    string Extension = System.IO.Path.GetExtension(this.saveFileDialog_SaveExcel.FileName);
                    if (Extension == ".txt")
                    {
                        CSVHelper.SaveFile(dataTable, this.saveFileDialog_SaveExcel.FileName);
                    }
                    else if (Extension == ".xls")
                    {
                        MyOffice.ExcelClass.NPOI_SaveFile(dataTable, this.saveFileDialog_SaveExcel.FileName);
                    }
                    this.Cursor = Cursors.Default;
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.openFileDialog_LoadExcel.ShowDialog() == DialogResult.OK)
                {
                    DataTable dataTable = new DataTable();
                    CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                    DataTable datatable_buf = dataTable.ReorderTable(new enum_medDrugstore_匯入());
                    if (datatable_buf == null)
                    {
                        MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                        return;
                    }
                    List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();

                    List<object[]> list_SQL_Value = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
                    List<object[]> list_Add = new List<object[]>();
                    List<object[]> list_Delete_ColumnName = new List<object[]>();
                    List<object[]> list_Delete_SerchValue = new List<object[]>();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();
                    List<object[]> list_SQL_Value_buf = new List<object[]>();

                    for (int i = 0; i < list_LoadValue.Count; i++)
                    {
                        object[] value_load = list_LoadValue[i];
                        list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_medDrugstore.藥品碼, value_load[(int)enum_medDrugstore_匯入.藥品碼].ObjectToString());
                        if (list_SQL_Value_buf.Count > 0)
                        {
                            object[] value_SQL = list_SQL_Value_buf[0];
                            value_load = value_load.CopyRow(new enum_medDrugstore_匯入(), new enum_medDrugstore());
                            value_SQL.CopyRow(ref value_load, new enum_medDrugstore(), new enum_medDrugstore(), (int)enum_medDrugstore.總庫存, (int)enum_medDrugstore.藥庫庫存, (int)enum_medDrugstore.藥局庫存, (int)enum_medDrugstore.安全庫存, (int)enum_medDrugstore.基準量, (int)enum_medDrugstore.藥品條碼1, (int)enum_medDrugstore.藥品條碼2);
                            bool flag_Equal = value_load.IsEqual(value_SQL);
                            if (!flag_Equal)
                            {
                                list_Replace_SerchValue.Add(value_load[(int)enum_medDrugstore.GUID].ObjectToString());
                                list_Replace_Value.Add(value_load);
                            }
                        }
                    }
                    this.sqL_DataGridView_藥庫_藥品資料.SQL_ReplaceExtra(enum_medDrugstore.GUID.GetEnumName(), list_Replace_SerchValue, list_Replace_Value, true);
                }
            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dataTable = this.sqL_DataGridView_藥庫_藥品資料.GetDataTable();
                    dataTable = dataTable.ReorderTable(new enum_medDrugstore_匯出());
                    string Extension = System.IO.Path.GetExtension(this.saveFileDialog_SaveExcel.FileName);
                    if (Extension == ".txt")
                    {
                        CSVHelper.SaveFile(dataTable, this.saveFileDialog_SaveExcel.FileName);
                    }
                    else if (Extension == ".xls")
                    {
                        MyOffice.ExcelClass.NPOI_SaveFile(dataTable, this.saveFileDialog_SaveExcel.FileName);
                    }
                    this.Cursor = Cursors.Default;
                    MyMessageBox.ShowDialog("匯出完成");
                }
            }));
 
        }
        private void PlC_RJ_Button_藥庫_藥品資料_新增效期_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥庫_藥品資料.Get_All_Select_RowsValues();

                if (list_藥品資料.Count == 0)
                {
                    this.Invoke(new Action(delegate
                    {
                        MyMessageBox.ShowDialog("未選擇資料!");
                    }));

                    return;
                }
                string 藥品碼 = list_藥品資料[0][(int)enum_medDrugstore.藥品碼].ObjectToString();
                藥品碼 = Function_藥品碼檢查(藥品碼);
                string 藥品名稱 = list_藥品資料[0][(int)enum_medDrugstore.藥品名稱].ObjectToString();
                List<DeviceBasic> deviceBasic_buf = this.List_藥庫_DeviceBasic.SortByCode(藥品碼);
                if (deviceBasic_buf.Count == 0)
                {
                    this.Invoke(new Action(delegate
                    {
                        MyMessageBox.ShowDialog("找無此藥品內容!");
                    }));
                    return;
                }
                string 效期 = "";
                string 批號 = "";
                string 數量 = "";
                Dialog_DateTime dialog_DateTime = new Dialog_DateTime();
                if (dialog_DateTime.ShowDialog() == DialogResult.Yes)
                {
                    效期 = dialog_DateTime.Value.ToDateString();
                }
                else
                {
                    return;
                }
                Dialog_輸入批號 Dialog_輸入批號 = new Dialog_輸入批號();
                if (Dialog_輸入批號.ShowDialog() == DialogResult.Yes)
                {
                    批號 = Dialog_輸入批號.Value;
                }
                else
                {
                    return;
                }
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                {
                    數量 = dialog_NumPannel.Value.ToString();
                }
                else
                {
                    return;
                }

                double 原有庫存 = deviceBasic_buf[0].取得庫存();


                string 庫存量 = deviceBasic_buf[0].取得庫存().ToString();
                deviceBasic_buf[0].效期庫存覆蓋(效期, 批號, 數量);
                double 修正庫存 = deviceBasic_buf[0].取得庫存();
                this.DeviceBasicClass_藥庫.SQL_ReplaceDeviceBasic(deviceBasic_buf[0]);

                string GUID = Guid.NewGuid().ToString();
                string 動作 = enum_交易記錄查詢動作.新增效期.GetEnumName();

                string 交易量 = (修正庫存 - 原有庫存).ToString();
                string 結存量 = deviceBasic_buf[0].取得庫存().ToString();
                string 操作人 = this.登入者名稱;
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 備註 = $"效期[{效期}],批號[{批號}]";

                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.庫別] = enum_庫別.藥庫.GetEnumName();
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;

                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);


                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < deviceBasic_buf[0].List_Validity_period.Count; i++)
                {
                    object[] value = new object[new enum_medDrugstore_效期及庫存().GetLength()];
                    value[(int)enum_medDrugstore_效期及庫存.效期] = deviceBasic_buf[0].List_Validity_period[i];
                    value[(int)enum_medDrugstore_效期及庫存.批號] = deviceBasic_buf[0].List_Lot_number[i];
                    value[(int)enum_medDrugstore_效期及庫存.庫存] = deviceBasic_buf[0].List_Inventory[i];
                    list_value.Add(value);
                }
                this.sqL_DataGridView_藥庫_藥品資料_效期及庫存.RefreshGrid(list_value);

            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_修正批號_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥庫_藥品資料.Get_All_Select_RowsValues();

                if (list_藥品資料.Count == 0)
                {
                    this.Invoke(new Action(delegate
                    {
                        MyMessageBox.ShowDialog("未選擇資料!");
                    }));

                    return;
                }
                string 藥品碼 = list_藥品資料[0][(int)enum_medDrugstore.藥品碼].ObjectToString();
                藥品碼 = Function_藥品碼檢查(藥品碼);
                string 藥品名稱 = list_藥品資料[0][(int)enum_medDrugstore.藥品名稱].ObjectToString();
                List<DeviceBasic> deviceBasic_buf = this.List_藥庫_DeviceBasic.SortByCode(藥品碼);
                if (deviceBasic_buf.Count == 0)
                {
                    this.Invoke(new Action(delegate
                    {
                        MyMessageBox.ShowDialog("找無此藥品內容!");
                    }));
                    return;
                }


                object[] value = sqL_DataGridView_藥庫_藥品資料_效期及庫存.GetRowValues();
                if (value == null)
                {
                    MyMessageBox.ShowDialog("未選擇效期!");
                    return;
                }
                string 效期 = value[(int)enum_medDrugstore_效期及庫存.效期].ObjectToString();
                string 舊批號 = value[(int)enum_medDrugstore_效期及庫存.批號].ObjectToString();
                string 新批號 = "";

                Dialog_輸入批號 Dialog_輸入批號 = new Dialog_輸入批號();
                if (Dialog_輸入批號.ShowDialog() == DialogResult.Yes)
                {
                    新批號 = Dialog_輸入批號.Value;
                }
                else
                {
                    return;
                }


                deviceBasic_buf[0].修正批號(效期, 新批號);
                this.DeviceBasicClass_藥庫.SQL_ReplaceDeviceBasic(deviceBasic_buf[0]);


                string GUID = Guid.NewGuid().ToString();
                string 動作 = enum_交易記錄查詢動作.修正批號.GetEnumName();
                string 交易量 = (0).ToString();
                string 結存量 = 0.ToString();
                string 操作人 = this.登入者名稱;
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 備註 = $"效期[{效期}]新批號[{新批號}]";

                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.庫別] = enum_庫別.藥庫.GetEnumName();
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 0.ToString();
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;

                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);

                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < deviceBasic_buf[0].List_Validity_period.Count; i++)
                {
                    object[] value_0 = new object[new enum_medDrugstore_效期及庫存().GetLength()];
                    value_0[(int)enum_medDrugstore_效期及庫存.效期] = deviceBasic_buf[0].List_Validity_period[i];
                    value_0[(int)enum_medDrugstore_效期及庫存.批號] = deviceBasic_buf[0].List_Lot_number[i];
                    value_0[(int)enum_medDrugstore_效期及庫存.庫存] = deviceBasic_buf[0].List_Inventory[i];
                    list_value.Add(value_0);
                }
                this.sqL_DataGridView_藥庫_藥品資料_效期及庫存.RefreshGrid(list_value);
            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_修正庫存_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                List<object[]> list_藥品資料 = this.sqL_DataGridView_藥庫_藥品資料.Get_All_Select_RowsValues();

                if (list_藥品資料.Count == 0)
                {
                    this.Invoke(new Action(delegate
                    {
                        MyMessageBox.ShowDialog("未選擇資料!");
                    }));

                    return;
                }
                string 藥品碼 = list_藥品資料[0][(int)enum_medDrugstore.藥品碼].ObjectToString();
                藥品碼 = Function_藥品碼檢查(藥品碼);
                string 藥品名稱 = list_藥品資料[0][(int)enum_medDrugstore.藥品名稱].ObjectToString();
                List<DeviceBasic> deviceBasic_buf = this.List_藥庫_DeviceBasic.SortByCode(藥品碼);
                if (deviceBasic_buf.Count == 0)
                {
                    this.Invoke(new Action(delegate
                    {
                        MyMessageBox.ShowDialog("找無此藥品內容!");
                    }));
                    return;
                }


                object[] value = sqL_DataGridView_藥庫_藥品資料_效期及庫存.GetRowValues();
                if (value == null)
                {
                    MyMessageBox.ShowDialog("未選擇效期!");
                    return;
                }

                string 效期 = value[(int)enum_medDrugstore_效期及庫存.效期].ObjectToString();
                string 批號 = value[(int)enum_medDrugstore_效期及庫存.批號].ObjectToString();
                string 數量 = "";
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                if (dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                {
                    數量 = dialog_NumPannel.Value.ToString();
                }
                else
                {
                    return;
                }


                double 原有庫存 = deviceBasic_buf[0].取得庫存();
                藥品碼 = Function_藥品碼檢查(藥品碼);
                string 庫存量 = deviceBasic_buf[0].Inventory;
                deviceBasic_buf[0].效期庫存覆蓋(效期, 數量);
                double 修正庫存 = deviceBasic_buf[0].取得庫存();
                this.DeviceBasicClass_藥庫.SQL_ReplaceDeviceBasic(deviceBasic_buf[0]);

                string GUID = Guid.NewGuid().ToString();
                string 動作 = enum_交易記錄查詢動作.修正庫存.GetEnumName();
                string 交易量 = (修正庫存 - 原有庫存).ToString();
                string 結存量 = deviceBasic_buf[0].Inventory;
                string 操作人 = this.登入者名稱;
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 備註 = $"效期[{效期}],批號[{批號}]";
                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.庫別] = enum_庫別.藥庫.GetEnumName();
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;

                this.sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);
                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < deviceBasic_buf[0].List_Validity_period.Count; i++)
                {
                    object[] value_0 = new object[new enum_medDrugstore_效期及庫存().GetLength()];
                    value_0[(int)enum_medDrugstore_效期及庫存.效期] = deviceBasic_buf[0].List_Validity_period[i];
                    value_0[(int)enum_medDrugstore_效期及庫存.批號] = deviceBasic_buf[0].List_Lot_number[i];
                    value_0[(int)enum_medDrugstore_效期及庫存.庫存] = deviceBasic_buf[0].List_Inventory[i];
                    list_value.Add(value_0);
                }
                this.sqL_DataGridView_藥庫_藥品資料_效期及庫存.RefreshGrid(list_value);
            }));
        }
        private void PlC_RJ_Button_藥庫_藥品資料_顯示有庫存藥品_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
            this.sqL_DataGridView_藥庫_藥品資料.RowsChangeFunction(list_value);
            list_value = (from value in list_value
                          where value[(int)enum_medDrugstore.總庫存].StringToInt32() > 0
                          select value).ToList();
            this.sqL_DataGridView_藥庫_藥品資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥庫_藥品資料_測試清除所有效期資料_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("確認測試清除所有效期資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<DeviceBasic> deviceBasics = this.DeviceBasicClass_藥庫.SQL_GetAllDeviceBasic();
            Dialog_Prcessbar dialog_Prcessbar = new Dialog_Prcessbar(deviceBasics.Count);
            dialog_Prcessbar.State = "清除儲位庫存資料";
            for (int i = 0; i < deviceBasics.Count; i++)
            {
                dialog_Prcessbar.Value = i;
                deviceBasics[i].清除所有庫存資料();
            }

            dialog_Prcessbar.State = "上傳儲位資料...";
            this.DeviceBasicClass_藥庫.SQL_ReplaceDeviceBasic(deviceBasics);
            dialog_Prcessbar.Close();
        }
        private void PlC_RJ_Button_藥庫_藥品資料_匯入安全基準量_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.openFileDialog_LoadExcel.ShowDialog() == DialogResult.OK)
                {
                    DataTable dataTable = new DataTable();
                    CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                    DataTable datatable_buf = dataTable.ReorderTable(new enum_medDrugstore_匯入_安全基準量());
                    if (datatable_buf == null)
                    {
                        MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                        return;
                    }
                    List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();

                    List<object[]> list_SQL_Value = this.sqL_DataGridView_藥庫_藥品資料.SQL_GetAllRows(false);
                    List<object[]> list_Add = new List<object[]>();
                    List<object[]> list_Delete_ColumnName = new List<object[]>();
                    List<object[]> list_Delete_SerchValue = new List<object[]>();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();
                    List<object[]> list_SQL_Value_buf = new List<object[]>();

                    for (int i = 0; i < list_LoadValue.Count; i++)
                    {
                        object[] value_load = list_LoadValue[i];
                        list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_medDrugstore.藥品碼, value_load[(int)enum_medDrugstore_匯入_安全基準量.藥品碼].ObjectToString());
                        if (list_SQL_Value_buf.Count > 0)
                        {
                            object[] value_SQL = list_SQL_Value_buf[0];
                            value_load = value_load.CopyRow(new enum_medDrugstore_匯入_安全基準量(), new enum_medDrugstore());
                            value_SQL.CopyRow(ref value_load, new enum_medDrugstore(), new enum_medDrugstore(), (int)enum_medDrugstore.總庫存, (int)enum_medDrugstore.藥庫庫存, (int)enum_medDrugstore.藥局庫存, (int)enum_medDrugstore.安全庫存, (int)enum_medDrugstore.基準量, (int)enum_medDrugstore.藥品條碼1, (int)enum_medDrugstore.藥品條碼2);
                            bool flag_Equal = value_load.IsEqual(value_SQL);
                            if (!flag_Equal)
                            {
                                list_Replace_SerchValue.Add(value_load[(int)enum_medDrugstore.GUID].ObjectToString());
                                list_Replace_Value.Add(value_load);
                            }
                        }
                    }
                    this.sqL_DataGridView_藥庫_藥品資料.SQL_ReplaceExtra(enum_medDrugstore.GUID.GetEnumName(), list_Replace_SerchValue, list_Replace_Value, true);
                }
            }));
        }
        #endregion

        private class ICP_藥庫_藥品資料 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string Code0 = x[(int)enum_medDrugstore.藥品碼].ObjectToString();
                string Code1 = y[(int)enum_medDrugstore.藥品碼].ObjectToString();
                return Code0.CompareTo(Code1);
            }
        }
    }
}
