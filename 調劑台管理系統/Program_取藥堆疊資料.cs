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
using HIS_DB_Lib;
using SQLUI;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        public class MyColor
        {
            public int R = 0;
            public int G = 0;
            public int B = 0;
            public MyColor(int r, int g, int b)
            {
                this.R = r;
                this.G = g;
                this.B = b;
            }


            public bool IsEqual(int r, int g, int b)
            {
                return (R == r && G == g && B == b);
            }
        }

        List<object[]> list_取藥堆疊母資料 = new List<object[]>();
        List<object[]> list_取藥堆疊子資料 = new List<object[]>();
        private MyThread MyThread_取藥堆疊資料_檢查資料;
        private MyThread MyThread_取藥堆疊資料_儲位亮燈;
        #region Function
        public class Icp_取藥堆疊母資料_index排序 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string index_0 = x[(int)enum_取藥堆疊母資料.序號].ObjectToString();
                string index_1 = y[(int)enum_取藥堆疊母資料.序號].ObjectToString();
                DateTime date0 = index_0.StringToDateTime();
                DateTime date1 = index_1.StringToDateTime();
                return DateTime.Compare(date0, date1);

            }
        }
        public class Icp_取藥堆疊母資料_操作時間排序 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string index_0 = x[(int)enum_取藥堆疊母資料.操作時間].ToDateTimeString_6();
                string index_1 = y[(int)enum_取藥堆疊母資料.操作時間].ToDateTimeString_6();
                DateTime date0 = index_0.StringToDateTime();
                DateTime date1 = index_1.StringToDateTime();
                return DateTime.Compare(date0, date1);

            }
        }
        public class Icp_取藥堆疊子資料_index排序 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string index_0 = x[(int)enum_取藥堆疊子資料.序號].ObjectToString();
                string index_1 = y[(int)enum_取藥堆疊子資料.序號].ObjectToString();
                UInt64 temp0 = 0;
                UInt64 temp1 = 0;
                UInt64.TryParse(index_0, out temp0);
                UInt64.TryParse(index_1, out temp1);
                if (temp0 > temp1) return 1;
                else if (temp0 < temp1) return -1;
                else return 0;
            }
        }
        public class Icp_取藥堆疊子資料_致能排序 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 致能_A = x[(int)enum_取藥堆疊子資料.致能].ObjectToString();
                string 致能_B = y[(int)enum_取藥堆疊子資料.致能].ObjectToString();
                if (致能_A == true.ToString()) 致能_A = "1";
                else 致能_A = "0";
                if (致能_B == true.ToString()) 致能_B = "1";
                else 致能_B = "0";
                return 致能_B.CompareTo(致能_A);
            }
        }

        private void Function_取藥堆疊資料_設定作業模式(object[] value, enum_取藥堆疊母資料_作業模式 enum_value)
        {
            Function_取藥堆疊資料_設定作業模式(value, enum_value, true);
        }
        private void Function_取藥堆疊資料_設定作業模式(object[] value, enum_取藥堆疊母資料_作業模式 enum_value, bool state)
        {
            UInt32 temp = value[(int)enum_取藥堆疊母資料.作業模式].StringToUInt32();
            temp.SetBit((int)enum_value, state);
            value[(int)enum_取藥堆疊母資料.作業模式] = temp.ToString();
        }
        private bool Function_取藥堆疊資料_取得作業模式(object[] value, enum_取藥堆疊母資料_作業模式 enum_value)
        {
            UInt32 temp = value[(int)enum_取藥堆疊母資料.作業模式].StringToUInt32();
            return temp.GetBit((int)enum_value);
        }
        private void Function_取藥堆疊資料_取得儲位資訊內容(object[] value, ref string Device_GUID, ref string TYPE, ref string IP, ref string Num, ref string 效期, ref string 批號, ref string 庫存, ref string 異動量)
        {
            if (value[(int)enum_儲位資訊.Value] is Device)
            {
                Device device = value[(int)enum_儲位資訊.Value] as Device;
                Num = (-1).ToString();
                value[(int)enum_儲位資訊.IP] = device.IP;
                value[(int)enum_儲位資訊.TYPE] = device.DeviceType.GetEnumName();
                Device_GUID = device.GUID;
                if (device.DeviceType == DeviceType.RFID_Device)
                {
                    Num = device.MasterIndex.ToString();
                }

            }
            IP = value[(int)enum_儲位資訊.IP].ObjectToString();
            TYPE = value[(int)enum_儲位資訊.TYPE].ObjectToString();
            效期 = value[(int)enum_儲位資訊.效期].ObjectToString();
            批號 = value[(int)enum_儲位資訊.批號].ObjectToString();
            庫存 = value[(int)enum_儲位資訊.庫存].ObjectToString();
            異動量 = value[(int)enum_儲位資訊.異動量].ObjectToString();

        }

        private List<object[]> Function_取藥堆疊資料_取得母資料()
        {
            List<object[]> list_value = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            if(list_value.Count > 0)
            {

            }
            return list_value;
        }
        private List<object[]> Function_取藥堆疊資料_取得子資料()
        {
            return this.sqL_DataGridView_取藥堆疊子資料.SQL_GetAllRows(false);
        }

        private void Function_取藥堆疊資料_新增母資料(takeMedicineStackClass takeMedicineStackClass)
        {
            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            takeMedicineStackClasses.Add(takeMedicineStackClass);
            this.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);

        }
        private void Function_取藥堆疊資料_新增母資料(List<takeMedicineStackClass> takeMedicineStackClasses)
        {
            List<takeMedicineStackClass> takeMedicineStackClasses_buf = new List<takeMedicineStackClass>();
            List<Task> taskList = new List<Task>();
            MyTimer myTimer = new MyTimer(500000);
            MyTimer myTimer_total = new MyTimer(500000);
            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_藥品設定表 = this.sqL_DataGridView_藥品設定表.SQL_GetAllRows(false);
            List<object[]> list_藥品管制方式設定 = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
            List<object[]> list_堆疊母資料 = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            List<object[]> list_堆疊母資料_add = new List<object[]>();
            bool flag_複盤 = false;
            bool flag_盲盤 = false;
            bool flag_效期管理 = false;
            bool flag_雙人覆核 = false;
            List<string> list_藥品碼 = (from temp in takeMedicineStackClasses
                                     select temp.藥品碼).Distinct().ToList();
            string 顏色 = "";
            for (int i = 0; i < takeMedicineStackClasses.Count; i++)
            {
                List<object[]> list_堆疊母資料_buf = new List<object[]>();
                string 藥品碼 = takeMedicineStackClasses[i].藥品碼;
                string 病歷號 = takeMedicineStackClasses[i].病歷號;
                string 開方時間 = takeMedicineStackClasses[i].開方時間;
                string 藥袋序號 = takeMedicineStackClasses[i].藥袋序號;
                顏色 = takeMedicineStackClasses[i].顏色;


                list_堆疊母資料 = list_堆疊母資料_buf.GetRows((int)enum_取藥堆疊母資料.藥品碼, 藥品碼);
                list_堆疊母資料_buf = list_堆疊母資料_buf.GetRows((int)enum_取藥堆疊母資料.病歷號, takeMedicineStackClasses[i].病歷號);
                list_堆疊母資料_buf = list_堆疊母資料_buf.GetRows((int)enum_取藥堆疊母資料.開方時間, takeMedicineStackClasses[i].開方時間);
                list_堆疊母資料_buf = list_堆疊母資料_buf.GetRows((int)enum_取藥堆疊母資料.藥袋序號, takeMedicineStackClasses[i].藥袋序號);
                if (list_堆疊母資料_buf.Count != 0) continue;
                if (takeMedicineStackClasses[i].GUID.StringIsEmpty())
                {
                    takeMedicineStackClasses[i].GUID = Guid.NewGuid().ToString();

                }
                takeMedicineStackClasses[i].序號 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClasses[i].操作時間 = DateTime.Now.ToDateTimeString_6();
                if (takeMedicineStackClasses[i].狀態 != enum_取藥堆疊母資料_狀態.刪除資料 && takeMedicineStackClasses[i].狀態 != enum_取藥堆疊母資料_狀態.已領用過) takeMedicineStackClasses[i].狀態 = enum_取藥堆疊母資料_狀態.等待刷新;

                if (takeMedicineStackClasses[i].動作 != enum_交易記錄查詢動作.入庫作業) takeMedicineStackClasses[i].IP = "";
                if (takeMedicineStackClasses[i].效期.Check_Date_String())
                {
                    if (takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.入庫作業 || takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.調入作業 || takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.掃碼退藥 || takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.手輸退藥)
                    {
                        takeMedicineStackClasses[i].狀態 = enum_取藥堆疊母資料_狀態.新增效期;
                    }
                }
                takeMedicineStackClasses[i].庫存量 = "0";
                takeMedicineStackClasses[i].結存量 = "0";
                takeMedicineStackClasses[i].作業模式 = "0";
                object[] value = takeMedicineStackClasses[i].ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>();
                value[(int)enum_取藥堆疊母資料.動作] = takeMedicineStackClasses[i].動作.GetEnumName();
                value[(int)enum_取藥堆疊母資料.狀態] = takeMedicineStackClasses[i].狀態.GetEnumName();
                List<object[]> list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, 藥品碼);

                if (list_藥品資料_buf.Count > 0)
                {
                    if (Function_藥品設定表_取得是否自訂義(list_藥品設定表, 藥品碼))
                    {
                        flag_複盤 = Function_藥品設定表_取得管制方式(list_藥品設定表, enum_藥品設定表.複盤, 藥品碼);
                        flag_盲盤 = Function_藥品設定表_取得管制方式(list_藥品設定表, enum_藥品設定表.盲盤, 藥品碼);
                        flag_效期管理 = Function_藥品設定表_取得管制方式(list_藥品設定表, enum_藥品設定表.效期管理, 藥品碼);
                        flag_雙人覆核 = Function_藥品設定表_取得管制方式(list_藥品設定表, enum_藥品設定表.雙人覆核, 藥品碼);
                    }
                    else
                    {
                        string 管制級別 = list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.管制級別].ObjectToString();
                        string 警訊藥品 = (list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString() == true.ToString()) ? "警訊" : "";
                        string 高價藥品 = (list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.高價藥品].ObjectToString() == true.ToString()) ? "高價" : "";
                        string 生物製劑 = (list_藥品資料_buf[0][(int)enum_藥品資料_藥檔資料.生物製劑].ObjectToString() == true.ToString()) ? "生物製劑" : "";
                        flag_複盤 = (Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.複盤, 管制級別) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.複盤, 警訊藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.複盤, 高價藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.複盤, 生物製劑));
                        flag_盲盤 = (Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.盲盤, 管制級別) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.盲盤, 警訊藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.盲盤, 高價藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.盲盤, 生物製劑));
                        flag_效期管理 = (Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.效期管理, 管制級別) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.效期管理, 警訊藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.效期管理, 高價藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.效期管理, 生物製劑));
                        flag_雙人覆核 = (Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.雙人覆核, 管制級別) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.雙人覆核, 警訊藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.雙人覆核, 高價藥品) || Function_藥品管制方式設定_取得管制方式(list_藥品管制方式設定, enum_medGeneralConfig.雙人覆核, 生物製劑));
                    }
                    if (flag_複盤) Function_取藥堆疊資料_設定作業模式(value, enum_取藥堆疊母資料_作業模式.複盤);
                    if (flag_盲盤) Function_取藥堆疊資料_設定作業模式(value, enum_取藥堆疊母資料_作業模式.盲盤);
                    if (flag_效期管理) Function_取藥堆疊資料_設定作業模式(value, enum_取藥堆疊母資料_作業模式.效期管控);
                    if (flag_雙人覆核)
                    {
                        Function_取藥堆疊資料_設定作業模式(value, enum_取藥堆疊母資料_作業模式.雙人覆核);
                        //value[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName();
                    }
                    if (flag_複盤 || flag_盲盤 || flag_雙人覆核) Function_取藥堆疊資料_設定作業模式(value, enum_取藥堆疊母資料_作業模式.獨立作業);

                }
                list_堆疊母資料_add.Add(value);

                Console.WriteLine($"----------------------------------------------");
                Console.WriteLine($"{i + 1}.新增取藥堆疊 : ");
                Console.WriteLine($" (動作){takeMedicineStackClasses[i].動作.GetEnumName()} ");
                Console.WriteLine($" (藥品碼){藥品碼} ");
                Console.WriteLine($" (病歷號){病歷號} ");
                Console.WriteLine($" (開方時間){開方時間} ");
                Console.WriteLine($" (狀態){takeMedicineStackClasses[i].狀態.GetEnumName()} ");
                Console.WriteLine($" (總異動量){takeMedicineStackClasses[i].總異動量} ");
                Console.WriteLine($" (顏色){takeMedicineStackClasses[i].顏色} ");
                Console.WriteLine($" (複盤){flag_複盤} ");
                Console.WriteLine($" (盲盤){flag_盲盤} ");
                Console.WriteLine($" (效期管理){flag_效期管理} ");
                Console.WriteLine($" (雙人覆核){flag_雙人覆核} ");
                Console.WriteLine($" (效期管理){flag_效期管理} ");
                Console.WriteLine($" (耗時){myTimer.ToString()} ");
                Console.WriteLine($"----------------------------------------------");

                takeMedicineStackClass takeMedicineStackClass = takeMedicineStackClasses[i];

            }

            for (int i = 0; i < list_藥品碼.Count; i++)
            {
                string 藥品碼 = list_藥品碼[i];
                if (藥品碼.StringIsEmpty()) continue;
                takeMedicineStackClasses_buf = (from temp in takeMedicineStackClasses
                                                where temp.藥品碼 == 藥品碼
                                                select temp).ToList();
                if(takeMedicineStackClasses_buf.Count > 0)
                {
                    顏色 = takeMedicineStackClasses_buf[0].顏色;
                }
                this.Function_儲位亮燈(藥品碼, 顏色.ToColor());
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();

            if (list_堆疊母資料_add.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_AddRows(list_堆疊母資料_add, false);

            List<object[]> list_value_delete = new List<object[]>();
            for (int i = 0; i < takeMedicineStackClasses.Count; i++)
            {
                string 藥品碼 = takeMedicineStackClasses[i].藥品碼;
                List<object[]> list_value = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
                List<object[]> list_value_buf = new List<object[]>();

                list_value_buf = list_value.GetRows((int)enum_取藥堆疊母資料.藥品碼, 藥品碼);
                list_value_buf = list_value_buf.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, "刷新面板");
                if (list_value_buf.Count > 0)
                {
                    list_value_delete.Add(list_value_buf[0]);
                }
            }
            if (list_value_delete.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_value_delete, false);
            Console.WriteLine($" 新增取藥資料 (耗時){myTimer_total.ToString()} ");

        }
        private object[] Function_取藥堆疊資料_新增子資料(string Master_GUID, string Device_GUID, string 調劑台名稱, string 藥品碼, string IP, string Num, string _enum_取藥堆疊_TYPE, string 效期, string 批號, string 異動量)
        {
            string GUID = Guid.NewGuid().ToString();
            string 序號 = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetTimeNow_6();
            string 致能 = false.ToString();
            string 流程作業完成 = false.ToString();
            string 配藥完成 = false.ToString();
            string 調劑結束 = false.ToString();
            string 已入賬 = false.ToString();

            object[] value = new object[new enum_取藥堆疊子資料().GetLength()];
            value[(int)enum_取藥堆疊子資料.GUID] = Guid.NewGuid().ToString();
            value[(int)enum_取藥堆疊子資料.Master_GUID] = Master_GUID;
            value[(int)enum_取藥堆疊子資料.Device_GUID] = Device_GUID;
            value[(int)enum_取藥堆疊子資料.序號] = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetTimeNow_6();
            value[(int)enum_取藥堆疊子資料.調劑台名稱] = 調劑台名稱;
            value[(int)enum_取藥堆疊子資料.藥品碼] = 藥品碼;
            value[(int)enum_取藥堆疊子資料.IP] = IP;
            value[(int)enum_取藥堆疊子資料.Num] = Num;
            value[(int)enum_取藥堆疊子資料.TYPE] = _enum_取藥堆疊_TYPE;
            value[(int)enum_取藥堆疊子資料.效期] = 效期;
            value[(int)enum_取藥堆疊子資料.批號] = 批號;
            value[(int)enum_取藥堆疊子資料.異動量] = 異動量.ToString();
            value[(int)enum_取藥堆疊子資料.致能] = false.ToString();
            value[(int)enum_取藥堆疊子資料.流程作業完成] = false.ToString();
            value[(int)enum_取藥堆疊子資料.配藥完成] = false.ToString();
            value[(int)enum_取藥堆疊子資料.調劑結束] = false.ToString();
            value[(int)enum_取藥堆疊子資料.已入賬] = false.ToString();

            this.sqL_DataGridView_取藥堆疊子資料.SQL_AddRow(value, false);
            return value;
        }
        private void Function_取藥堆疊資料_刪除母資料(string GUID)
        {
            List<object[]> list_value = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            List<object[]> list_value_buf = list_value.GetRows((int)enum_取藥堆疊母資料.GUID, GUID);
            if (list_value_buf.Count == 0) return;
            this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_value_buf, false);

            string 藥品碼 = list_value_buf[0][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
            string 操作人 = list_value_buf[0][(int)enum_取藥堆疊母資料.操作人].ObjectToString();
            list_value_buf = list_value.GetRows((int)enum_取藥堆疊母資料.藥品碼, 藥品碼);
            list_value_buf = list_value_buf.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, "刷新面板");
            if (list_value_buf.Count == 0)
            {
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = Guid.NewGuid().ToString();
                takeMedicineStackClass.藥品碼 = 藥品碼;
                takeMedicineStackClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.調劑台名稱 = "刷新面板";
                takeMedicineStackClass.操作人 = $"{操作人}";
                this.sqL_DataGridView_取藥堆疊母資料.SQL_AddRow(takeMedicineStackClass.ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>(), false);
            }
            else
            {
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.GUID = list_value_buf[0][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                takeMedicineStackClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.調劑台名稱 = "刷新面板";
                takeMedicineStackClass.操作人 = $"{操作人}";
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(takeMedicineStackClass.ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>(), false);
            }

            //this.Function_儲位亮燈(藥品碼, Color.Black);
        }
        private void Function_取藥堆疊資料_刪除子資料(string GUID)
        {
            Function_取藥堆疊資料_刪除子資料(GUID, true);
        }
        private void Function_取藥堆疊資料_刪除子資料(string GUID, bool color_off)
        {
            List<object[]> list_value = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetRows(enum_取藥堆疊子資料.GUID.GetEnumName(), GUID, false);
            if (list_value.Count > 0)
            {
                string 藥品碼 = list_value[0][(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
                this.sqL_DataGridView_取藥堆疊子資料.SQL_Delete(enum_取藥堆疊子資料.GUID.GetEnumName(), GUID, false);
                if (藥品碼.StringIsEmpty()) return;

              

                this.Function_儲位亮燈(藥品碼, Color.Black);

            }
        }
        private void Function_取藥堆疊資料_語音提示(object[] 取藥堆疊子資料)
        {
            string device_Type = 取藥堆疊子資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString();
            string IP = 取藥堆疊子資料[(int)enum_取藥堆疊子資料.IP].ObjectToString();
            string 藥品碼 = 取藥堆疊子資料[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
            string device_GUID = 取藥堆疊子資料[(int)enum_取藥堆疊子資料.Device_GUID].ObjectToString();

            if (device_Type == DeviceType.EPD266.GetEnumName() || device_Type == DeviceType.EPD266_lock.GetEnumName() || device_Type == DeviceType.EPD290.GetEnumName() || device_Type == DeviceType.EPD290_lock.GetEnumName())
            {
                Storage storage = List_EPD266_雲端資料.SortByIP(IP);
                if (storage != null && storage.Speaker.StringIsEmpty() == false)
                {
                    Task.Run(() =>
                    {
                        this.voice.SpeakOnTask(storage.Speaker);
                    });
                }
            }
            else if (device_Type == DeviceType.Pannel35.GetEnumName() || device_Type == DeviceType.Pannel35_lock.GetEnumName())
            {
                Storage storage = List_Pannel35_雲端資料.SortByIP(IP);
                if (storage != null && storage.Speaker.StringIsEmpty() == false)
                {
                    Task.Run(() =>
                    {
                        this.voice.SpeakOnTask(storage.Speaker);
                    });

                }
            }
            else if (device_Type == DeviceType.EPD583.GetEnumName() || device_Type == DeviceType.EPD583_lock.GetEnumName())
            {
                Drawer drawer = List_EPD583_雲端資料.SortByIP(IP);
                if (drawer != null && drawer.Speaker.StringIsEmpty() == false)
                {
                    Task.Run(() =>
                    {
                        this.voice.SpeakOnTask(drawer.Speaker);
                    });
                }
            }
            else if (device_Type == DeviceType.EPD1020.GetEnumName() || device_Type == DeviceType.EPD1020_lock.GetEnumName())
            {
                Drawer drawer = this.List_EPD1020_雲端資料.SortByIP(IP);
                if (drawer != null && drawer.Speaker.StringIsEmpty() == false)
                {
                    Task.Run(() =>
                    {
                        this.voice.SpeakOnTask(drawer.Speaker);
                    });
                }
            }
            else if (device_Type == DeviceType.RowsLED.GetEnumName())
            {
                RowsLED rowsLED = this.List_RowsLED_雲端資料.SortByIP(IP);
                RowsDevice rowsDevice = rowsLED.SortByGUID(device_GUID);
                if (rowsLED != null && rowsLED.Speaker.StringIsEmpty() == false)
                {
                    Task.Run(() =>
                    {
                        this.voice.SpeakOnTask(rowsLED.Speaker);
                    });
                }
            }
        }

        private void Function_取藥堆疊資料_刪除指定調劑台名稱母資料(string 調劑台名稱)
        {
            while (true)
            {
                List<object[]> list_value = sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
                List<object[]> list_value_buf = list_value.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, 調劑台名稱);
                List<object[]> list_value_add = new List<object[]>();
                List<object[]> list_value_replace = new List<object[]>();
                if (list_value_buf.Count == 0) break;
                sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_value_buf, false);
                for (int i = 0; i < list_value_buf.Count; i++)
                {
                
                    string 藥品碼 = list_value_buf[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                 
                    if (藥品碼.StringIsEmpty()) continue;
                    this.Function_儲位亮燈(藥品碼, Color.Black);
                    List<object[]> list_value_buf_temp = list_value_buf.GetRows((int)enum_取藥堆疊母資料.藥品碼, 藥品碼);
                    list_value_buf_temp = list_value_buf_temp.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, "刷新面板");

                    if (list_value_buf_temp.Count == 0)
                    {
                        takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                        takeMedicineStackClass.GUID = Guid.NewGuid().ToString();
                        takeMedicineStackClass.藥品碼 = 藥品碼;
                        takeMedicineStackClass.動作 = enum_交易記錄查詢動作.None;
                        takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.None;
                        takeMedicineStackClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                        takeMedicineStackClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                        takeMedicineStackClass.調劑台名稱 = "刷新面板";
                        object[] value = takeMedicineStackClass.ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>();
                        value[(int)enum_取藥堆疊母資料.動作] = enum_交易記錄查詢動作.None.GetEnumName();
                        value[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.None.GetEnumName();
                        list_value_add.Add(value);
                        Console.WriteLine($"{takeMedicineStackClass.JsonSerializationt(true)}");
                    }
                    else
                    {
                        takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                        takeMedicineStackClass.GUID = list_value_buf[0][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                        takeMedicineStackClass.動作 = enum_交易記錄查詢動作.None;
                        takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.None;
                        takeMedicineStackClass.藥品碼 = 藥品碼;
                        takeMedicineStackClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                        takeMedicineStackClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                        takeMedicineStackClass.調劑台名稱 = "刷新面板";
                        object[] value = takeMedicineStackClass.ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>();
                        value[(int)enum_取藥堆疊母資料.動作] = enum_交易記錄查詢動作.None.GetEnumName();
                        value[(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.None.GetEnumName();
                        list_value_add.Add(value);
                        Console.WriteLine($"{takeMedicineStackClass.JsonSerializationt(true)}");
                    }

                }
                if (list_value_add.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_AddRows(list_value_add, false);
                if (list_value_replace.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_value_replace, false);

            }
            System.Threading.Thread.Sleep(100);
        }
        private List<object[]> Function_取藥堆疊資料_取得指定調劑台名稱母資料(string 調劑台名稱)
        {
            List<object[]> list_values = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            list_values = list_values.Where(a => a[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() == 調劑台名稱).ToList();
            return list_values;
        }
        private List<object[]> Function_取藥堆疊資料_取得指定調劑台名稱母資料(string 調劑台名稱, string 藥品碼)
        {
            List<object[]> list_values = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(調劑台名稱);
            list_values = list_values.Where(a => a[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == 藥品碼).ToList();
            return list_values;
        }

        private List<object[]> Function_取藥堆疊資料_取得指定調劑台名稱子資料(string 調劑台名稱)
        {
            List<object[]> list_values = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetAllRows(false);
            list_values = list_values.Where(a => a[(int)enum_取藥堆疊子資料.調劑台名稱].ObjectToString() == 調劑台名稱).ToList();
            return list_values;
        }
        private List<object[]> Function_取藥堆疊資料_取得指定調劑台名稱子資料(string 調劑台名稱, string 藥品碼)
        {
            List<object[]> list_values = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱);
            list_values = list_values.Where(a => a[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString() == 藥品碼).ToList();
            return list_values;
        }
        private List<object[]> Function_取藥堆疊資料_取得指定調劑台名稱子資料(string 調劑台名稱, string 藥品碼, string IP)
        {
            List<object[]> list_values = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱);
            list_values = list_values.Where(a => a[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString() == 藥品碼).ToList();
            list_values = list_values.Where(a => a[(int)enum_取藥堆疊子資料.IP].ObjectToString() == IP).ToList();
            return list_values;
        }

        private Color Function_取藥堆疊母資料_取得指定Master_GUID顏色(string GUID)
        {
            string[] serch_cols = new string[] { enum_取藥堆疊母資料.GUID.GetEnumName() };
            string[] serch_values = new string[] { GUID };
            List<object[]> list_values = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetRows(serch_cols, serch_values, false);
            if (list_values.Count > 0)
            {
                return list_values[0][(int)enum_取藥堆疊母資料.顏色].ObjectToString().ToColor();
            }
            return Color.Black;
        }
        private string Function_取藥堆疊母資料_取得指定Master_GUID結存量(string GUID)
        {
            string[] serch_cols = new string[] { enum_取藥堆疊母資料.GUID.GetEnumName() };
            string[] serch_values = new string[] { GUID };
            List<object[]> list_values = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetRows(serch_cols, serch_values, false);
            if (list_values.Count > 0)
            {
                return list_values[0][(int)enum_取藥堆疊母資料.結存量].ObjectToString();
            }
            return "";
        }
        private string Function_取藥堆疊母資料_取得指定Master_GUID調劑台名稱(string GUID)
        {
            string[] serch_cols = new string[] { enum_取藥堆疊母資料.GUID.GetEnumName() };
            string[] serch_values = new string[] { GUID };
            List<object[]> list_values = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetRows(serch_cols, serch_values, false);
            if (list_values.Count > 0)
            {
                return list_values[0][(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString();
            }
            return "";
        }
        private void Function_取藥堆疊子資料_設定流程作業完成ByCode(string 調劑台名稱, string 藥品碼)
        {
            string Master_GUID = "";
            List<object[]> list_堆疊母資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(調劑台名稱, 藥品碼);
            List<object[]> list_堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱, 藥品碼);
            List<object[]> list_堆疊子資料_buf;
            List<object[]> list_serch_values = new List<object[]>();
            for (int i = 0; i < list_堆疊母資料.Count; i++)
            {
                Master_GUID = list_堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_堆疊子資料_buf = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);

                for (int k = 0; k < list_堆疊子資料_buf.Count; k++)
                {
                    list_堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    list_serch_values.Add(list_堆疊子資料_buf[k]);
                }
            }
            this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_serch_values, false);

        }
        private void Function_取藥堆疊子資料_設定流程作業完成ByCode(string 調劑台名稱, string 藥品碼, string IP)
        {
            List<object[]> list_堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱, 藥品碼, IP);
            List<object[]> list_serch_values = new List<object[]>();
            for (int k = 0; k < list_堆疊子資料.Count; k++)
            {
                list_堆疊子資料[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                list_堆疊子資料[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                list_serch_values.Add(list_堆疊子資料[k]);
            }
            this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_serch_values, false);
        }
        private List<object[]> Function_取藥堆疊子資料_設定流程作業完成ByIP(string 調劑台名稱, string IP)
        {
            return Function_取藥堆疊子資料_設定流程作業完成ByIP(調劑台名稱, IP, "-1");
        }
        private List<object[]> Function_取藥堆疊子資料_設定流程作業完成ByIP(string 調劑台名稱, string IP, string Num)
        {
            List<object[]> list_堆疊子資料 = new List<object[]>();
            List<object[]> serch_values = new List<object[]>();
            if (調劑台名稱 != "None")
            {
                list_堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.IP, IP);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Num, Num);

                for (int i = 0; i < list_堆疊子資料.Count; i++)
                {
                    list_堆疊子資料[i][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_堆疊子資料[i][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    serch_values.Add(list_堆疊子資料[i]);

                }
                this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(serch_values, false);
            }
            else
            {
                if (Num.StringIsEmpty()) Num = "-1";
                list_堆疊子資料 = this.Function_取藥堆疊資料_取得子資料();
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.IP, IP);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Num, Num);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.致能, true.ToString());
                for (int i = 0; i < list_堆疊子資料.Count; i++)
                {
                    list_堆疊子資料[i][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    serch_values.Add(list_堆疊子資料[i]);
                }
                this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(serch_values, false);
            }
            return list_堆疊子資料;
        }
        private void Function_取藥堆疊子資料_設定配藥完成ByCode(string 調劑台名稱, string 藥品碼)
        {
            string Master_GUID = "";
            List<object[]> list_堆疊母資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(調劑台名稱, 藥品碼);
            List<object[]> list_堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱, 藥品碼);
            List<object[]> list_堆疊子資料_buf;
            List<object[]> list_serch_values = new List<object[]>();
            for (int i = 0; i < list_堆疊母資料.Count; i++)
            {
                Master_GUID = list_堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                list_堆疊子資料_buf = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);
                for (int k = 0; k < list_堆疊子資料_buf.Count; k++)
                {
                    list_堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    list_堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                    list_serch_values.Add(list_堆疊子資料_buf[k]);
                }
            }
            this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_serch_values, false);
        }
        private void Function_取藥堆疊子資料_設定配藥完成ByCode(string 調劑台名稱, string 藥品碼, string IP)
        {
            List<object[]> list_堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱, 藥品碼, IP);
            List<object[]> list_serch_values = new List<object[]>();
            for (int k = 0; k < list_堆疊子資料.Count; k++)
            {
                list_堆疊子資料[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                list_堆疊子資料[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                list_堆疊子資料[k][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                list_serch_values.Add(list_堆疊子資料[k]);
            }
            this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_serch_values, false);
        }
        private void Function_取藥堆疊子資料_設定配藥完成ByIP(string 調劑台名稱, string IP)
        {
            Function_取藥堆疊子資料_設定配藥完成ByIP(調劑台名稱, IP, "-1");
        }
        private void Function_取藥堆疊子資料_設定配藥完成ByIP(string 調劑台名稱, string IP, string Num)
        {
            List<object[]> list_堆疊子資料 = new List<object[]>();
            List<object[]> serch_values = new List<object[]>();
            if (調劑台名稱 != "None")
            {
                list_堆疊子資料 = this.Function_取藥堆疊資料_取得指定調劑台名稱子資料(調劑台名稱);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.IP, IP);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Num, Num);
                for (int i = 0; i < list_堆疊子資料.Count; i++)
                {
                    list_堆疊子資料[i][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                    list_堆疊子資料[i][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                    list_堆疊子資料[i][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                    serch_values.Add(list_堆疊子資料[i]);

                }
                this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(serch_values, false);
            }
            else
            {
                if (Num.StringIsEmpty()) Num = "-1";
                list_堆疊子資料 = this.Function_取藥堆疊資料_取得子資料();
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.IP, IP);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Num, Num);
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.致能, true.ToString());
                list_堆疊子資料 = list_堆疊子資料.GetRows((int)enum_取藥堆疊子資料.流程作業完成, true.ToString());
                for (int i = 0; i < list_堆疊子資料.Count; i++)
                {
                    list_堆疊子資料[i][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                    serch_values.Add(list_堆疊子資料[i]);
                }
                this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(serch_values, false);
            }
        }
        private void Function_取藥堆疊子資料_設定調劑結束(string 調劑台名稱, string 藥品碼)
        {
            string GUID = "";
            List<object[]> list_values = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(調劑台名稱, 藥品碼);
            for (int i = 0; i < list_values.Count; i++)
            {
                GUID = list_values[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                this.Function_取藥堆疊子資料_設定調劑結束(GUID);
            }
        }
        private void Function_取藥堆疊子資料_設定調劑結束(string 調劑台名稱)
        {
            string GUID = "";
            List<object[]> list_values = this.Function_取藥堆疊資料_取得指定調劑台名稱母資料(調劑台名稱);
            for (int i = 0; i < list_values.Count; i++)
            {
                GUID = list_values[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                this.Function_取藥堆疊子資料_設定調劑結束(GUID);
            }

        }
        private object[] Function_取藥堆疊子資料_設定已入賬(object[] 堆疊子資料)
        {
            string IP = 堆疊子資料[(int)enum_取藥堆疊子資料.IP].ObjectToString();
            string 藥品碼 = 堆疊子資料[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
            string str_TYPE = 堆疊子資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString();
            string 效期 = 堆疊子資料[(int)enum_取藥堆疊子資料.效期].ObjectToString();
            int 異動量 = 堆疊子資料[(int)enum_取藥堆疊子資料.異動量].StringToInt32();
            int 儲位庫存 = 0;
            string 批號 = 堆疊子資料[(int)enum_取藥堆疊子資料.批號].ObjectToString();
            if (str_TYPE == DeviceType.EPD583.GetEnumName() || str_TYPE == DeviceType.EPD583_lock.GetEnumName())
            {
                List<Box> boxes = List_EPD583_入賬資料.SortByCode(藥品碼);
                for (int i = 0; i < boxes.Count; i++)
                {
                    if (boxes[i].IP != IP) continue;
                    boxes[i] = this.drawerUI_EPD_583.SQL_GetBox(boxes[i]);
                    儲位庫存 = boxes[i].取得庫存(效期);
                    if (儲位庫存 + 異動量 < 0)
                    {
                        List<string> list_效期 = new List<string>();
                        List<string> list_批號 = new List<string>();
                        List<string> list_異動量 = new List<string>();
                        boxes[i].庫存異動(異動量, out list_效期, out list_批號);

                        Drawer drawer = List_EPD583_入賬資料.ReplaceBox(boxes[i]);
                        List_EPD583_入賬資料.Add_NewDrawer(boxes[i]);
                        this.drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);
                        break;
                    }
                    else if ((儲位庫存) >= 0)
                    {
                        boxes[i].效期庫存異動(效期, 異動量);
                        批號 = boxes[i].取得批號(效期);
                        Drawer drawer = List_EPD583_入賬資料.ReplaceBox(boxes[i]);
                        List_EPD583_入賬資料.Add_NewDrawer(boxes[i]);
                        this.drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);
                        break;
                    }
                    else if ((儲位庫存) == -1)
                    {
                        boxes[i].新增效期(效期, 批號, 異動量.ToString());
                        Drawer drawer = List_EPD583_入賬資料.ReplaceBox(boxes[i]);
                        List_EPD583_入賬資料.Add_NewDrawer(boxes[i]);
                        this.drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);
                        break;
                    }
                }
            }
            if (str_TYPE == DeviceType.EPD1020.GetEnumName() || str_TYPE == DeviceType.EPD1020_lock.GetEnumName())
            {
                List<Box> boxes = this.List_EPD1020_入賬資料.SortByCode(藥品碼);
                for (int i = 0; i < boxes.Count; i++)
                {
                    if (boxes[i].IP != IP) continue;
                    boxes[i] = this.drawerUI_EPD_1020.SQL_GetBox(boxes[i]);
                    儲位庫存 = boxes[i].取得庫存(效期);
                    if (儲位庫存 + 異動量 < 0)
                    {
                        List<string> list_效期 = new List<string>();
                        List<string> list_批號 = new List<string>();
                        List<string> list_異動量 = new List<string>();
                        boxes[i].庫存異動(異動量, out list_效期, out list_批號);

                        Drawer drawer = this.List_EPD1020_入賬資料.ReplaceBox(boxes[i]);
                        this.List_EPD1020_入賬資料.Add_NewDrawer(boxes[i]);
                        this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                        break;
                    }
                    else if ((儲位庫存) >= 0)
                    {
                        boxes[i].效期庫存異動(效期, 異動量);
                        Drawer drawer = this.List_EPD1020_入賬資料.ReplaceByGUID(boxes[i]);
                        this.List_EPD1020_入賬資料.Add_NewDrawer(boxes[i]);
                        this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                        break;
                    }
                    else if ((儲位庫存) == -1)
                    {
                        boxes[i].新增效期(效期, 批號, 異動量.ToString());
                        Drawer drawer = this.List_EPD1020_入賬資料.ReplaceBox(boxes[i]);
                        this.List_EPD1020_入賬資料.Add_NewDrawer(boxes[i]);
                        this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                        break;
                    }
                }
            }
            else if (str_TYPE == DeviceType.EPD266.GetEnumName() || str_TYPE == DeviceType.EPD266_lock.GetEnumName() || str_TYPE == DeviceType.EPD290.GetEnumName() || str_TYPE == DeviceType.EPD290_lock.GetEnumName())
            {
                Storage storage = List_EPD266_入賬資料.SortByIP(IP);
                storage = this.storageUI_EPD_266.SQL_GetStorage(storage);
                儲位庫存 = storage.取得庫存(效期);
                if (儲位庫存 + 異動量 < 0)
                {
                    List<string> list_效期 = new List<string>();
                    List<string> list_批號 = new List<string>();
                    List<string> list_異動量 = new List<string>();
                    storage.庫存異動(異動量, out list_效期, out list_批號);

                    List_EPD266_入賬資料.Add_NewStorage(storage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
                else if ((儲位庫存) >= 0)
                {
                    storage.效期庫存異動(效期, 異動量);
                    List_EPD266_入賬資料.Add_NewStorage(storage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);

                }
                else if ((儲位庫存) == -1)
                {
                    storage.新增效期(效期, 批號, 異動量.ToString());
                    List_EPD266_入賬資料.Add_NewStorage(storage);
                    this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }
            else if (str_TYPE == DeviceType.Pannel35_lock.GetEnumName() || str_TYPE == DeviceType.Pannel35.GetEnumName())
            {
                Storage storage = List_Pannel35_入賬資料.SortByIP(IP);
                storage = this.storageUI_WT32.SQL_GetStorage(storage);
                儲位庫存 = storage.取得庫存(效期);
                if (儲位庫存 + 異動量 < 0)
                {
                    List<string> list_效期 = new List<string>();
                    List<string> list_批號 = new List<string>();
                    List<string> list_異動量 = new List<string>();
                    storage.庫存異動(異動量, out list_效期, out list_批號);

                    List_Pannel35_入賬資料.Add_NewStorage(storage);
                    this.storageUI_WT32.SQL_ReplaceStorage(storage);
                }
                else if ((儲位庫存) >= 0)
                {
                    storage.效期庫存異動(效期, 異動量);
                    List_Pannel35_入賬資料.Add_NewStorage(storage);
                    this.storageUI_WT32.SQL_ReplaceStorage(storage);
                    if (!plC_CheckBox_測試模式.Checked)
                    {
                        Task.Run(() =>
                        {
                            this.storageUI_WT32.Set_DrawPannelJEPG(storage);

                        });
                    }

                }
                else if ((儲位庫存) == -1)
                {
                    storage.新增效期(效期, 批號, 異動量.ToString());
                    List_Pannel35_入賬資料.Add_NewStorage(storage);
                    this.storageUI_WT32.SQL_ReplaceStorage(storage);
                }
            }
            else if (str_TYPE == DeviceType.RowsLED.GetEnumName())
            {
                List<RowsDevice> rowsDevices = this.List_RowsLED_入賬資料.SortByCode(藥品碼);
                for (int i = 0; i < rowsDevices.Count; i++)
                {
                    if (rowsDevices[i].IP != IP) continue;
                    rowsDevices[i] = this.rowsLEDUI.SQL_GetRowsDevice(rowsDevices[i]);
                    儲位庫存 = rowsDevices[i].取得庫存(效期);
                    if (儲位庫存 + 異動量 < 0)
                    {
                        List<string> list_效期 = new List<string>();
                        List<string> list_批號 = new List<string>();
                        List<string> list_異動量 = new List<string>();
                        rowsDevices[i].庫存異動(異動量, out list_效期, out list_批號);

                        this.List_RowsLED_入賬資料.Add_NewRowsLED(rowsDevices[i]);
                        RowsLED rowsLED = this.List_RowsLED_入賬資料.SortByIP(rowsDevices[i].IP);
                        this.rowsLEDUI.SQL_ReplaceRowsLED(rowsLED);
                        break;
                    }
                    else if ((儲位庫存) >= 0)
                    {
                        rowsDevices[i].效期庫存異動(效期, 異動量);
                        this.List_RowsLED_入賬資料.Add_NewRowsLED(rowsDevices[i]);
                        RowsLED rowsLED = this.List_RowsLED_入賬資料.SortByIP(rowsDevices[i].IP);
                        this.rowsLEDUI.SQL_ReplaceRowsLED(rowsLED);
                        break;
                    }
                    else if ((儲位庫存) == -1)
                    {
                        rowsDevices[i].新增效期(效期, 批號, 異動量.ToString());
                        this.List_RowsLED_入賬資料.Add_NewRowsLED(rowsDevices[i]);
                        RowsLED rowsLED = this.List_RowsLED_入賬資料.SortByIP(rowsDevices[i].IP);
                        this.rowsLEDUI.SQL_ReplaceRowsLED(rowsLED);
                    }
                }
            }
            else if (str_TYPE == DeviceType.RFID_Device.GetEnumName())
            {
                List<RFIDDevice> rFIDDevices = this.List_RFID_入賬資料.SortByCode(藥品碼);
                for (int i = 0; i < rFIDDevices.Count; i++)
                {
                    if (rFIDDevices[i].IP != IP) continue;
                    rFIDDevices[i] = this.rfiD_UI.SQL_GetDevice(rFIDDevices[i]);
                    儲位庫存 = rFIDDevices[i].取得庫存(效期);
                    if ((儲位庫存) >= 0)
                    {
                        rFIDDevices[i].效期庫存異動(效期, 異動量);
                        this.List_RFID_入賬資料.Add_NewRFIDClass(rFIDDevices[i]);
                        RFIDClass rFIDClass = this.List_RFID_入賬資料.SortByIP(rFIDDevices[i].IP);
                        this.rfiD_UI.SQL_ReplaceRFIDClass(rFIDClass);
                        break;
                    }
                    else if ((儲位庫存) == -1)
                    {
                        rFIDDevices[i].新增效期(效期, 批號, 異動量.ToString());
                        this.List_RFID_入賬資料.Add_NewRFIDClass(rFIDDevices[i]);
                        RFIDClass rFIDClass = this.List_RFID_入賬資料.SortByIP(rFIDDevices[i].IP);
                        this.rfiD_UI.SQL_ReplaceRFIDClass(rFIDClass);
                    }
                }
            }
            堆疊子資料[(int)enum_取藥堆疊子資料.已入賬] = true.ToString();
            堆疊子資料[(int)enum_取藥堆疊子資料.致能] = true.ToString();
            堆疊子資料[(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
            堆疊子資料[(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
            return 堆疊子資料;
        }
        private List<object[]> Function_取藥堆疊子資料_取得可致能(ref List<object[]> list_value)
        {
            string IP;
            string Num = "";
            string 調劑台名稱 = "";
            string 藥品碼 = "";
            string 致能 = "";
            string 流程作業完成 = "";
            string 配藥完成 = "";
            bool flag_可致能資料 = true;
            List<object[]> list_取藥堆疊子資料 = list_value;
            List<object[]> list_取藥堆疊子資料_buf = new List<object[]>();
            list_取藥堆疊子資料.Sort(new Icp_取藥堆疊子資料_致能排序());
            for (int i = 0; i < list_取藥堆疊子資料.Count; i++)
            {
                flag_可致能資料 = true;
                IP = list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.IP].ObjectToString();
                Num = list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.Num].ObjectToString();
                致能 = list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.致能].ObjectToString();
                流程作業完成 = list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.流程作業完成].ObjectToString();
                配藥完成 = list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.配藥完成].ObjectToString();
                藥品碼 = list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
                調劑台名稱 = list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.調劑台名稱].ObjectToString();

                if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD266.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD266_lock.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD290.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD290_lock.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.Pannel35.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.Pannel35_lock.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD583.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD583_lock.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD1020.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD1020_lock.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.RowsLED.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                else if (list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.RFID_Device.GetEnumName())
                {
                    if (致能 == true.ToString() && 流程作業完成 == true.ToString() && 配藥完成 == true.ToString())
                    {
                        flag_可致能資料 = false;
                    }
                }
                if (flag_可致能資料)
                {
                    List<object[]> list_temp = (from value in list_取藥堆疊子資料_buf
                                                where IP == value[(int)enum_取藥堆疊子資料.IP].ObjectToString()
                                                where Num == value[(int)enum_取藥堆疊子資料.Num].ObjectToString()
                                                where 調劑台名稱 != value[(int)enum_取藥堆疊子資料.調劑台名稱].ObjectToString()
                                                where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                                select value).ToList();
                    if (list_temp.Count > 0) flag_可致能資料 = false;
                }
                if (flag_可致能資料)
                {
                    list_取藥堆疊子資料_buf.Add(list_取藥堆疊子資料[i]);
                }
            }
            return list_取藥堆疊子資料_buf;
        }
        private List<object[]> Function_取藥堆疊母資料_取得可入賬資料()
        {
            List<object[]> list_取藥堆疊母資料 = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            list_取藥堆疊母資料 = (from value in list_取藥堆疊母資料
                            where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待入賬.GetEnumName()
                            select value).ToList();
            return list_取藥堆疊母資料;
        }

        private void Function_取藥堆疊資料_檢查資料儲位正常(object[] list_母資料)
        {
            string Master_GUID = list_母資料[(int)enum_取藥堆疊母資料.GUID].ObjectToString();
            int 總異動量 = list_母資料[(int)enum_取藥堆疊母資料.總異動量].StringToInt32();
            if (list_母資料[(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringIsInt32() == false)
            {
                總異動量 = -99999;
            }
            List<object[]> list_取藥堆疊子資料 = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetAllRows(false);
            list_取藥堆疊子資料 = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);
            int 累積異動量 = 0;
            for (int i = 0; i < list_取藥堆疊子資料.Count; i++)
            {
                累積異動量 += list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.異動量].StringToInt32();
            }
            if (總異動量 != 累積異動量)
            {
                this.sqL_DataGridView_取藥堆疊子資料.SQL_DeleteExtra(list_取藥堆疊子資料, false);
            }
        }
        #endregion

        private void Program_取藥堆疊資料_Init()
        {

            string url = $"{dBConfigClass.Api_URL}/api/OutTakeMed/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            List<SQLUI.Table> tables = json.JsonDeserializet<List<SQLUI.Table>>();



            if (tables == null)
            {
                MyMessageBox.ShowDialog($"取藥堆疊表單建立失敗!! Api_URL:{url}");
                return;
            }

            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].TableName == this.sqL_DataGridView_取藥堆疊母資料.TableName)
                {
                    this.sqL_DataGridView_取藥堆疊母資料.Init(tables[i]);
                }
                if (tables[i].TableName == this.sqL_DataGridView_取藥堆疊子資料.TableName)
                {
                    this.sqL_DataGridView_取藥堆疊子資料.Init(tables[i]);
                }
            }

            this.MyThread_取藥堆疊資料_檢查資料 = new MyThread();
            this.MyThread_取藥堆疊資料_檢查資料.AutoRun(true);
            this.MyThread_取藥堆疊資料_檢查資料.AutoStop(true);
            this.MyThread_取藥堆疊資料_檢查資料.Add_Method(this.sub_Program_取藥堆疊資料_檢查資料);
            this.MyThread_取藥堆疊資料_檢查資料.Add_Method(this.sub_Program_取藥堆疊資料_狀態更新);
            this.MyThread_取藥堆疊資料_檢查資料.Add_Method(this.sub_Program_取藥堆疊資料_流程作業檢查);
            this.MyThread_取藥堆疊資料_檢查資料.Add_Method(this.sub_Program_取藥堆疊資料_入賬檢查);
            this.MyThread_取藥堆疊資料_檢查資料.Trigger();


            this.MyThread_取藥堆疊資料_儲位亮燈 = new MyThread();
            this.MyThread_取藥堆疊資料_儲位亮燈.AutoRun(true);
            this.MyThread_取藥堆疊資料_儲位亮燈.AutoStop(true);
            this.MyThread_取藥堆疊資料_儲位亮燈.Add_Method(this.sub_Program_取藥堆疊資料_儲位亮燈);
            this.MyThread_取藥堆疊資料_儲位亮燈.Trigger();
        }
        public class LightOn
        {
            public string IP { get; set; }
            public string 藥品碼 { get; set; }
            public Color 顏色 { get; set; }
        }
        private List<LightOn> lightOns = new List<LightOn>();
        private int cnt_儲位亮燈 = 0;
        private int 儲位亮燈數量_temp = 0;
        private MyTimerBasic MyTimerBasic_儲位亮燈 = new MyTimerBasic();
        private void sub_Program_取藥堆疊資料_儲位亮燈()
        {
            if(lightOns.Count > 0)
            {
                lock (lightOns)
                {
                    try
                    {
                        if (cnt_儲位亮燈 == 0)
                        {
                            儲位亮燈數量_temp = lightOns.Count;
                            MyTimerBasic_儲位亮燈.TickStop();
                            MyTimerBasic_儲位亮燈.StartTickTime(20);
                            cnt_儲位亮燈++;
                        }
                        if (cnt_儲位亮燈 == 1)
                        {
                            if(MyTimerBasic_儲位亮燈.IsTimeOut())
                            {
                                if(儲位亮燈數量_temp == lightOns.Count)
                                {
                                    cnt_儲位亮燈++;
                                }
                                else
                                {
                                    cnt_儲位亮燈 = 0;
                                }
                            }
                        }
                        if (cnt_儲位亮燈 == 2)
                        {
                            List<LightOn> lightOns_buf = new List<LightOn>();
                            for (int i = 0; i < lightOns.Count; i++)
                            {
                                LightOn lightOn = new LightOn();
                                lightOn.藥品碼 = lightOns[i].藥品碼;
                                lightOn.顏色 = lightOns[i].顏色;
                                lightOns_buf.Add(lightOn);
                            }
                            lightOns.Clear();

                            List<Task> taskList_抽屜層架 = new List<Task>();

                            List<string> list_抽屜亮燈_IP = new List<string>();
                            List<string> list_層架亮燈_IP = new List<string>();
                            List<string> list_EPD1020亮燈_IP = new List<string>();

                            for (int i = 0; i < lightOns_buf.Count; i++)
                            {
                                string 藥品碼 = lightOns_buf[i].藥品碼;
                                Color 顏色 = lightOns_buf[i].顏色;
                                list_抽屜亮燈_IP.LockAdd(Function_儲位亮燈_取得抽屜亮燈IP(藥品碼, 顏色));
                                list_層架亮燈_IP.LockAdd(Function_儲位亮燈_取得層架亮燈IP(藥品碼, 顏色));
                                list_EPD1020亮燈_IP.LockAdd(Function_儲位亮燈_取得EPD1020亮燈IP(藥品碼, 顏色));
                            }

                            taskList_抽屜層架.Add(Task.Run(() =>
                            {
                                Function_儲位亮燈_抽屜亮燈(list_抽屜亮燈_IP);
                            }));
                            taskList_抽屜層架.Add(Task.Run(() =>
                            {
                                Function_儲位亮燈_層架亮燈(list_層架亮燈_IP);
                            }));
                            taskList_抽屜層架.Add(Task.Run(() =>
                            {
                                Function_儲位亮燈_EPD1020亮燈(list_EPD1020亮燈_IP);
                            }));
                            Task.WhenAll(taskList_抽屜層架).Wait();
                            List<Task> taskList = new List<Task>();
                            for (int i = 0; i < lightOns_buf.Count; i++)
                            {
                                string 藥品碼 = lightOns_buf[i].藥品碼;
                                Color 顏色 = lightOns_buf[i].顏色;
                                taskList.Add(Task.Run(() =>
                                {
                                    Function_儲位亮燈_Ex(藥品碼, 顏色);

                                }));
                            }
                            Task.WhenAll(taskList).Wait();
                            cnt_儲位亮燈++;
                        }
                        if (cnt_儲位亮燈 == 3)
                        {
                            cnt_儲位亮燈 = 0;
                        }
       
                       
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }

                   
                }
            }
   
        }
        public List<string> Function_儲位亮燈_取得抽屜亮燈IP(string 藥品碼, Color color)
        {
            if (藥品碼.StringIsEmpty()) return new List<string>();
            List<object> list_Device = this.Function_從本地資料取得儲位(藥品碼);
            List<object> list_commonSpace_device = this.Function_從共用區取得儲位(藥品碼);
            for (int i = 0; i < list_commonSpace_device.Count; i++)
            {
                list_Device.Add(list_commonSpace_device[i]);
            }
            bool flag_led_refresh = true;
            List<string> list_IP = new List<string>();
            for (int i = 0; i < list_Device.Count; i++)
            {
                Device device = list_Device[i] as Device;
                string IP = device.IP;

                if (device == null) continue;

                if (device.DeviceType == DeviceType.EPD583 || device.DeviceType == DeviceType.EPD583_lock)
                {
                    Box box = list_Device[i] as Box;
                    if (box != null)
                    {
                        bool flag_common_device = false;
                        Drawer drawer = List_EPD583_本地資料.SortByIP(IP);
                        if (drawer == null)
                        {
                            drawer = CommonSapceClass.GetEPD583(IP, ref this.commonSapceClasses);
                            flag_common_device = true;
                        }
                        if (drawer == null) continue;
                        List<Box> boxes = drawer.SortByCode(藥品碼);
                      
                        if (drawer.IsAllLight)
                        {
                            byte[] LED_Bytes = new byte[drawer.LED_Bytes.Length];
                            for (int k = 0; k < drawer.LED_Bytes.Length; k++)
                            {
                                LED_Bytes[k] = drawer.LED_Bytes[k];
                            }
                            bool flag_commonlight = false;
                            List<MyColor> myColors = new List<MyColor>();
                            if (color != Color.Black)
                            {
                                for (int k = 0; k < 400; k++)
                                {
                                    int R = LED_Bytes[k * 3 + 0];
                                    int G = LED_Bytes[k * 3 + 1];
                                    int B = LED_Bytes[k * 3 + 2];
                                    if (R == 0 && G == 0 && B == 0) continue;

                                    if (R != color.R || G != color.G || B != color.B)
                                    {
                                        Console.WriteLine($"藥品碼 : {藥品碼} , {color.R},{color.G},{color.B} [{R},{G},{B}]");
                                        flag_commonlight = true;
                                        break;
                                    }
                                }
                            }

                            drawer.LED_Bytes = DrawerUI_EPD_583.Set_LEDBytes(drawer, boxes, color);
                            if (!flag_commonlight)
                            {
                                if (color != Color.Black)
                                {
                                    drawer.LED_Bytes = DrawerUI_EPD_583.Set_Pannel_LEDBytes(drawer, color);
                                }
                                else
                                {
                                    bool flag_led_black_enable = true;
                                    for (int k = 0; k < 400; k++)
                                    {
                                        int R = drawer.LED_Bytes[k * 3 + 0];
                                        int G = drawer.LED_Bytes[k * 3 + 1];
                                        int B = drawer.LED_Bytes[k * 3 + 2];
                                        if (R != 0 || G != 0 || B != 0)
                                        {
                                            flag_led_black_enable = false;
                                            break;
                                        }
                                    }
                                    if (flag_led_black_enable|| flag_common_device) drawer.LED_Bytes = DrawerUI_EPD_583.Set_Pannel_LEDBytes(drawer, color);

                                }
                            }
                            else drawer.LED_Bytes = DrawerUI_EPD_583.Set_Pannel_LEDBytes(drawer, Color.Purple);


                            flag_led_refresh = true;
                            for (int k = 0; k < drawer.LED_Bytes.Length; k++)
                            {
                                if (LED_Bytes[k] != drawer.LED_Bytes[k])
                                {
                                    flag_led_refresh = true;
                                }
                            }
                        }
                        else
                        {
                            drawer.LED_Bytes = DrawerUI_EPD_583.Set_LEDBytes(drawer, color);
                            flag_led_refresh = true;
                        }
                    }
                    list_IP.Add(IP);
                }

            }
            list_IP = (from temp in list_IP
                       select temp).Distinct().ToList();
            return list_IP;
        }
        public void Function_儲位亮燈_抽屜亮燈(List<string> list_IP)
        {
            try
            {
                list_IP = (from temp in list_IP
                           select temp).Distinct().ToList();
                Task allTask;
                List<Task> taskList = new List<Task>();
                for (int i = 0; i < list_IP.Count; i++)
                {
                    string IP = list_IP[i];
                    taskList.Add(Task.Run(() =>
                    {
                        Drawer drawer = List_EPD583_本地資料.SortByIP(IP);
                        if (drawer == null)
                        {
                            drawer = CommonSapceClass.GetEPD583(IP, ref this.commonSapceClasses);
                        }
                        if (drawer == null) return;
                        if (!plC_CheckBox_測試模式.Checked)
                        {
                            this.drawerUI_EPD_583.Set_LED_UDP(drawer);
                        }

                    }));
                   
                }
                allTask = Task.WhenAll(taskList);
                allTask.Wait();
            }
            catch
            {

            }
            finally
            {

            }
          
        }

        public List<string> Function_儲位亮燈_取得層架亮燈IP(string 藥品碼, Color color)
        {
            if (藥品碼.StringIsEmpty()) return new List<string>();
            List<object> list_Device = this.Function_從本地資料取得儲位(藥品碼);
            List<object> list_commonSpace_device = this.Function_從共用區取得儲位(藥品碼);
            for (int i = 0; i < list_commonSpace_device.Count; i++)
            {
                list_Device.Add(list_commonSpace_device[i]);
            }
            bool flag_led_refresh = true;
            List<string> list_IP = new List<string>();
            for (int i = 0; i < list_Device.Count; i++)
            {
                Device device = list_Device[i] as Device;
                string IP = device.IP;

                if (device == null) continue;
                if (device.DeviceType == DeviceType.RowsLED)
                {
                    RowsDevice rowsDevice = list_Device[i] as RowsDevice;
                    if (rowsDevice != null)
                    {
                        RowsLED rowsLED = List_RowsLED_本地資料.SortByIP(rowsDevice.IP);
                        if (rowsLED == null)
                        {
                            rowsLED = CommonSapceClass.GetRowsLED(IP, ref this.commonSapceClasses);
                        }
                        rowsLED.LED_Bytes = RowsLEDUI.Get_Rows_LEDBytes(ref rowsLED.LED_Bytes, rowsDevice, color);
                    }

                    list_IP.Add(IP);
                }



            }
            list_IP = (from temp in list_IP
                       select temp).Distinct().ToList();
            return list_IP;
        }
        public void Function_儲位亮燈_層架亮燈(List<string> list_IP)
        {
            try
            {
                list_IP = (from temp in list_IP
                           select temp).Distinct().ToList();
                Task allTask;
                List<Task> taskList = new List<Task>();
                for (int i = 0; i < list_IP.Count; i++)
                {
                    string IP = list_IP[i];
                    taskList.Add(Task.Run(() =>
                    {
                        RowsLED rowsLED = List_RowsLED_本地資料.SortByIP(IP);
                        if (rowsLED == null)
                        {
                            rowsLED = CommonSapceClass.GetRowsLED(IP, ref this.commonSapceClasses);
                        }
                        taskList.Add(Task.Run(() =>
                        {
                            if (!plC_CheckBox_測試模式.Checked)
                            {
                                this.rowsLEDUI.Set_Rows_LED_UDP(rowsLED);
                            }

                        }));


                    }));
                  
                }
                allTask = Task.WhenAll(taskList);
                allTask.Wait();
            }
            catch
            {

            }
            finally
            {

            }
        
        }

        public List<string> Function_儲位亮燈_取得EPD1020亮燈IP(string 藥品碼, Color color)
        {
            if (藥品碼.StringIsEmpty()) return new List<string>();
            List<object> list_Device = this.Function_從本地資料取得儲位(藥品碼);
 
            bool flag_led_refresh = true;
            List<string> list_IP = new List<string>();
            for (int i = 0; i < list_Device.Count; i++)
            {
                Device device = list_Device[i] as Device;
                string IP = device.IP;

                if (device == null) continue;

                if (device.DeviceType == DeviceType.EPD1020 || device.DeviceType == DeviceType.EPD1020_lock)
                {
                    Box box = list_Device[i] as Box;
                    if (box != null)
                    {
                        Drawer drawer = List_EPD1020_雲端資料.SortByIP(IP);
                 
                        if (drawer == null) continue;
                        List<Box> boxes = drawer.SortByCode(藥品碼);
                        drawer.LED_Bytes = DrawerUI_EPD_1020.Set_Pannel_LEDBytes(drawer, color);
                        drawer.LED_Bytes = DrawerUI_EPD_1020.Set_LEDBytes(drawer, color);
                    }
                    list_IP.Add(IP);
                }

            }
            list_IP = (from temp in list_IP
                       select temp).Distinct().ToList();
            return list_IP;
        }
        public void Function_儲位亮燈_EPD1020亮燈(List<string> list_IP)
        {
            try
            {
                list_IP = (from temp in list_IP
                           select temp).Distinct().ToList();
                Task allTask;
                List<Task> taskList = new List<Task>();
                for (int i = 0; i < list_IP.Count; i++)
                {
                    string IP = list_IP[i];
                    taskList.Add(Task.Run(() =>
                    {
                        Drawer drawer = List_EPD1020_雲端資料.SortByIP(IP);
        
                        if (drawer == null) return;
                        if (!plC_CheckBox_測試模式.Checked)
                        {
                            this.drawerUI_EPD_1020.Set_LED_UDP(drawer);
                        }

                    }));

                }
                allTask = Task.WhenAll(taskList);
                allTask.Wait();
            }
            catch
            {

            }
            finally
            {

            }

        }


        public void Function_儲位亮燈_Ex(string 藥品碼, Color color)
        {
            if (藥品碼.StringIsEmpty()) return;
            List<object> list_Device = this.Function_從雲端資料取得儲位(藥品碼);
            List<object> list_commonSpace_device = this.Function_從共用區取得儲位(藥品碼);
            for(int i = 0; i< list_commonSpace_device.Count; i++)
            {
                list_Device.Add(list_commonSpace_device[i]);
            }


            if (color == Color.Black)
            {
                Console.WriteLine($"●●儲位滅燈●●,藥品碼:{藥品碼},color{color.ToColorString()}-------------");
            }
            else
            {
                Console.WriteLine($"◇◇儲位亮燈◇◇,藥品碼:{藥品碼},color{color.ToColorString()}");
            }
            Task allTask;
            List<Task> taskList = new List<Task>();
            List<string> list_IP = new List<string>();
            List<string> list_IP_buf = new List<string>();
            bool flag_led_refresh = false;
      
            list_IP.Clear();
            list_IP_buf.Clear();
            for (int i = 0; i < list_Device.Count; i++)
            {
                Device device = list_Device[i] as Device;
                string IP = device.IP;

                if (device != null)
                {
                    
                 
                }
            }
            taskList.Clear();
            for (int i = 0; i < list_Device.Count; i++)
            {
                Device device = list_Device[i] as Device;
                string IP = device.IP;
                list_IP_buf = (from value in list_IP
                               where value == IP
                               select value).ToList();
                if (list_IP_buf.Count > 0) continue;

                if (device != null)
                {
                    if (device.DeviceType == DeviceType.EPD266 || device.DeviceType == DeviceType.EPD266_lock || device.DeviceType == DeviceType.EPD290 || device.DeviceType == DeviceType.EPD290_lock)
                    {
                        Storage storage = list_Device[i] as Storage;
                        if (storage != null)
                        {
                            taskList.Add(Task.Run(() =>
                            {
                                if (!plC_CheckBox_測試模式.Checked)
                                {
                                    this.storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);
                                }

                            }));
          
                            list_IP.Add(IP);
                        }
                    }
                    else if (device.DeviceType == DeviceType.EPD583 || device.DeviceType == DeviceType.EPD583_lock)
                    {
    
                    }
                    else if (device.DeviceType == DeviceType.EPD1020 || device.DeviceType == DeviceType.EPD1020_lock)
                    {
                        //Box box = list_Device[i] as Box;
                        //if (box != null)
                        //{
                        //    taskList.Add(Task.Run(() =>
                        //    {
                        //        Drawer drawer = List_EPD1020_雲端資料.SortByIP(IP);
                        //        List<Box> boxes = drawer.SortByCode(藥品碼);
                        //        if (!plC_CheckBox_測試模式.Checked)
                        //        {
                        //            this.drawerUI_EPD_1020.Set_Pannel_LED_UDP(drawer, color);
                        //        }

                        //    }));
  
                        //    list_IP.Add(IP);
                        //}
                    }
                    else if (device.DeviceType == DeviceType.Pannel35)
                    {
                        Storage storage = list_Device[i] as Storage;
                        if (storage != null)
                        {
                            taskList.Add(Task.Run(() =>
                            {
                                if (!plC_CheckBox_測試模式.Checked)
                                {
                                    this.storageUI_WT32.Set_Stroage_LED_UDP(storage, color);
                                }

                            }));
  
                            list_IP.Add(IP);
                        }
                    }
                    else if (device.DeviceType == DeviceType.RowsLED)
                    {
                      
                    }
                }
            }
            allTask = Task.WhenAll(taskList);
            allTask.Wait();
        }
        #region PLC_取藥堆疊資料_檢查資料
        PLC_Device PLC_Device_取藥堆疊資料_檢查資料 = new PLC_Device("");
        PLC_Device PLC_Device_取藥堆疊資料_檢查資料_更新儲位資料 = new PLC_Device("");
        MyTimer MyTimer_取藥堆疊資料_刷新時間 = new MyTimer("K1");
        MyTimer MyTimer_取藥堆疊資料_自動過帳時間 = new MyTimer();
        MyTimer MyTimer_取藥堆疊資料_資料更新時間 = new MyTimer();
        int cnt_Program_取藥堆疊資料_檢查資料 = 65534;
        void sub_Program_取藥堆疊資料_檢查資料()
        {
            //this.MyThread_取藥堆疊資料_檢查資料.GetCycleTime(100, this.label_取要推疊_資料更新時間);
            PLC_Device_取藥堆疊資料_檢查資料.Bool = PLC_Device_主機扣賬模式.Bool;
            if (cnt_Program_取藥堆疊資料_檢查資料 == 65534)
            {
                List<object[]> list_堆疊母資料 = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
                List<object[]> list_堆疊子資料 = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetAllRows(false);
                this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_堆疊母資料, false);
                this.sqL_DataGridView_取藥堆疊子資料.SQL_DeleteExtra(list_堆疊子資料, false);
                PLC_Device_取藥堆疊資料_檢查資料_更新儲位資料.Bool = true;
                PLC_Device_取藥堆疊資料_檢查資料_更新儲位資料.SetComment("PLC_Device_取藥堆疊資料_檢查資料_更新儲位資料");
                PLC_Device_取藥堆疊資料_檢查資料.SetComment("PLC_取藥堆疊資料_檢查資料");
                PLC_Device_取藥堆疊資料_檢查資料.Bool = false;
                cnt_Program_取藥堆疊資料_檢查資料 = 65535;
            }
            if (cnt_Program_取藥堆疊資料_檢查資料 == 65535) cnt_Program_取藥堆疊資料_檢查資料 = 1;
            if (cnt_Program_取藥堆疊資料_檢查資料 == 1) cnt_Program_取藥堆疊資料_檢查資料_檢查按下(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 2) cnt_Program_取藥堆疊資料_檢查資料_初始化(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 3) cnt_Program_取藥堆疊資料_檢查資料_檢查儲位刷新(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 4) cnt_Program_取藥堆疊資料_檢查資料_檢查儲位亮燈(ref cnt_Program_取藥堆疊資料_檢查資料);          
            if (cnt_Program_取藥堆疊資料_檢查資料 == 5) cnt_Program_取藥堆疊資料_檢查資料_檢查系統領藥(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 6) cnt_Program_取藥堆疊資料_檢查資料_刷新新增效期(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 7) cnt_Program_取藥堆疊資料_檢查資料_堆疊資料整理(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 8) cnt_Program_取藥堆疊資料_檢查資料_從SQL讀取儲位資料(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 9) cnt_Program_取藥堆疊資料_檢查資料_刷新無庫存(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 10) cnt_Program_取藥堆疊資料_檢查資料_刷新資料(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 11) cnt_Program_取藥堆疊資料_檢查資料_設定致能(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 12) cnt_Program_取藥堆疊資料_檢查資料_等待刷新時間到達(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 13) cnt_Program_取藥堆疊資料_檢查資料 = 65500;
            if (cnt_Program_取藥堆疊資料_檢查資料 > 1) cnt_Program_取藥堆疊資料_檢查資料_檢查放開(ref cnt_Program_取藥堆疊資料_檢查資料);
            if (cnt_Program_取藥堆疊資料_檢查資料 == 65500)
            {
                PLC_Device_取藥堆疊資料_檢查資料.Bool = false;
                cnt_Program_取藥堆疊資料_檢查資料 = 65535;
            }
        }
        void cnt_Program_取藥堆疊資料_檢查資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_取藥堆疊資料_檢查資料.Bool) cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_取藥堆疊資料_檢查資料.Bool) cnt = 65500;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_初始化(ref int cnt)
        {
            MyTimer_取藥堆疊資料_資料更新時間.TickStop();
            MyTimer_取藥堆疊資料_資料更新時間.StartTickTime(9999999);

            MyTimer_取藥堆疊資料_自動過帳時間.StartTickTime(10000);
            if (MyTimer_取藥堆疊資料_自動過帳時間.IsTimeOut())
            {
                if (plC_CheckBox_自動過帳.Checked)
                {
                    //PlC_RJ_Button_醫囑資料_自動過帳_MouseDownEvent(null);
                }
                MyTimer_取藥堆疊資料_自動過帳時間.TickStop();
            }

            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_檢查儲位刷新(ref int cnt)
        {
            //-----------------------------------------------------------------------------------------------------------------------------------------
            //檢查系統領藥是否資料是否到達時間
            this.list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
            this.list_取藥堆疊母資料 = this.list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, "刷新面板");
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();
            int 刷新時間 = 2;
            for (int i = 0; i < this.list_取藥堆疊母資料.Count; i++)
            {
                DateTime dt_start = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString().StringToDateTime();
                DateTime dt_end = DateTime.Now;
                TimeSpan ts = dt_end - dt_start;
                if (ts.TotalSeconds >= 刷新時間)
                {
                    list_取藥堆疊母資料_delete.Add(this.list_取藥堆疊母資料[i]);
                    string 藥品碼 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    if (藥品碼.StringIsEmpty() == false)
                    {
                        Task.Run(() =>
                        {
                            Function_從SQL取得儲位到本地資料(藥品碼);
                            this.Function_儲位刷新(藥品碼);
                        });
                    }

                }
            }
            if (list_取藥堆疊母資料_delete.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);

            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_檢查儲位亮燈(ref int cnt)
        {
            //-----------------------------------------------------------------------------------------------------------------------------------------
            //檢查系統領藥是否資料是否到達時間
            List<object[]> list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
            list_取藥堆疊母資料 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, "儲位亮燈");
            List<object[]> list_取藥堆疊母資料_buf_未亮燈 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.備註, "");
            List<object[]> list_取藥堆疊母資料_buf_已亮燈 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.備註, "已亮燈");
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_retplace = new List<object[]>();
            int 刷新時間 = 10;
            if (list_取藥堆疊母資料_buf_未亮燈.Count > 0 && list_取藥堆疊母資料_buf_已亮燈.Count > 0)
            {
                for (int i = 0; i < list_取藥堆疊母資料_buf_已亮燈.Count; i++)
                {
                    list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料_buf_已亮燈[i]);
                    string 藥品碼 = list_取藥堆疊母資料_buf_已亮燈[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    string 顏色 = list_取藥堆疊母資料_buf_已亮燈[i][(int)enum_取藥堆疊母資料.顏色].ObjectToString();
                    if (藥品碼.StringIsEmpty() == false)
                    {
                        Task.Run(() =>
                        {
                            this.Function_儲位亮燈(藥品碼, Color.Black);
                        });
                    }
                }
                if (list_取藥堆疊母資料_delete.Count > 0)
                {
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                    return;
                }
            }

            for (int i = 0; i < list_取藥堆疊母資料_buf_已亮燈.Count; i++)
            {
                string 藥品碼 = list_取藥堆疊母資料_buf_已亮燈[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 顏色 = list_取藥堆疊母資料_buf_已亮燈[i][(int)enum_取藥堆疊母資料.顏色].ObjectToString();
                DateTime dt_start = list_取藥堆疊母資料_buf_已亮燈[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString().StringToDateTime();
                DateTime dt_end = DateTime.Now;
                TimeSpan ts = dt_end - dt_start;
                if (ts.TotalSeconds >= 刷新時間)
                {
                    list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料_buf_已亮燈[i]);
                    if (藥品碼.StringIsEmpty() == false)
                    {
                        Task.Run(() =>
                        {
                            this.Function_儲位亮燈(藥品碼, Color.Black);
                        });
                    }

                }
                if (list_取藥堆疊母資料_delete.Count > 0)
                {
                    this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
                    return;
                }
            }
            for (int i = 0; i < list_取藥堆疊母資料_buf_未亮燈.Count; i++)
            {
                list_取藥堆疊母資料_buf_未亮燈[i][(int)enum_取藥堆疊母資料.備註] = "已亮燈";
                list_取藥堆疊母資料_retplace.Add(list_取藥堆疊母資料_buf_未亮燈[i]);
                string 藥品碼 = list_取藥堆疊母資料_buf_未亮燈[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                string 顏色 = list_取藥堆疊母資料_buf_未亮燈[i][(int)enum_取藥堆疊母資料.顏色].ObjectToString();
                if (藥品碼.StringIsEmpty() == false)
                {
                    Task.Run(() =>
                    {
                        this.Function_儲位亮燈(藥品碼, 顏色.ToColor());
                    });
                }
            }
            if (list_取藥堆疊母資料_retplace.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_retplace, false);
                return;
            }

            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_檢查系統領藥(ref int cnt)
        {
            //-----------------------------------------------------------------------------------------------------------------------------------------
            //檢查領藥是否資料是否到達時間
            if (plC_NumBox_處方存在時間.Value < 20000) plC_NumBox_處方存在時間.Value = 20000;
            this.list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
            this.list_取藥堆疊母資料 = this.list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName());
            List<object[]> list_取藥堆疊母資料_delete = new List<object[]>();
            int 處方存在時間 = plC_NumBox_處方存在時間.Value / 1000;
            for (int i = 0; i < this.list_取藥堆疊母資料.Count; i++)
            {
                DateTime dt_start = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString().StringToDateTime();
                DateTime dt_end = DateTime.Now;
                TimeSpan ts = dt_end - dt_start;
                if (ts.TotalSeconds >= 處方存在時間)
                {
                    list_取藥堆疊母資料_delete.Add(this.list_取藥堆疊母資料[i]);
              
                }
            }
            if (list_取藥堆疊母資料_delete.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
            //-----------------------------------------------------------------------------------------------------------------------------------------
            //檢查系統領藥是否有新資料
            this.list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
            list_取藥堆疊母資料_delete.Clear();
            List<object[]> list_取藥堆疊母資料_buf = new List<object[]>();
            this.list_取藥堆疊母資料 = this.list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.動作, enum_交易記錄查詢動作.系統領藥.GetEnumName());
            list_取藥堆疊母資料_buf = this.list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName());
            List<DateTime> List_操作時間 = new List<DateTime>();
            List<DateTime> List_操作時間_buf = new List<DateTime>();
            for (int i = 0; i < list_取藥堆疊母資料_buf.Count; i++)
            {
                DateTime date = list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.操作時間].ObjectToString().StringToDateTime();

                List_操作時間_buf = (from value in List_操作時間
                                 where value.ToDateTimeString() == date.ToDateTimeString()
                                 select value).ToList();
                if (List_操作時間_buf.Count == 0) List_操作時間.Add(date);
            }
            List_操作時間.Sort();


            if (List_操作時間.Count > 1)
            {
                for (int i = 0; i < List_操作時間.Count - 1; i++)
                {
                    string date0 = List_操作時間[i].ToDateTimeString();
                    for (int k = 0; k < list_取藥堆疊母資料.Count; k++)
                    {
                        string date1 = list_取藥堆疊母資料[k][(int)enum_取藥堆疊母資料.操作時間].ObjectToString().StringToDateTime().ToDateTimeString();
                        if (date0 == date1)
                        {
                            string 藥品碼 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
             
                            list_取藥堆疊母資料_delete.Add(list_取藥堆疊母資料[k]);
                        }
                    }
                }
            }
            if (list_取藥堆疊母資料_delete.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_delete, false);
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_堆疊資料整理(ref int cnt)
        {
            string GUID = "";
            this.list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
            this.list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得子資料();
            List<object[]> list_取藥堆疊子資料_DeleteValue = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_資料更新 = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_取消作業 = new List<object[]>();

            //檢查更新雲端資料
            list_取藥堆疊母資料_資料更新 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.調劑台名稱, "更新資料");
            for (int i = 0; i < list_取藥堆疊母資料_資料更新.Count; i++)
            {
                GUID = list_取藥堆疊母資料_資料更新[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                this.list_取藥堆疊母資料.Remove(list_取藥堆疊母資料_資料更新[i]);
                this.Function_取藥堆疊資料_刪除母資料(GUID);
                PLC_Device_取藥堆疊資料_檢查資料_更新儲位資料.Bool = true;
            }
            if (PLC_Device_取藥堆疊資料_檢查資料_更新儲位資料.Bool)
            {
                if (dBConfigClass.Med_Update_ApiURL.StringIsEmpty() == false)
                {
                    string Jsonstring = Basic.Net.WEBApiGet(dBConfigClass.Med_Update_ApiURL);
                    Console.WriteLine(Jsonstring);
                }
                this.Function_從SQL取得儲位到雲端資料();
                PLC_Device_取藥堆疊資料_檢查資料_更新儲位資料.Bool = false;
            }

            //檢查取消作業-刪除母資料
            list_取藥堆疊母資料_取消作業 = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.取消作業.GetEnumName());
            for (int i = 0; i < list_取藥堆疊母資料_取消作業.Count; i++)
            {
                GUID = list_取藥堆疊母資料_取消作業[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                this.list_取藥堆疊母資料.Remove(list_取藥堆疊母資料_取消作業[i]);
                this.Function_取藥堆疊資料_刪除母資料(GUID);
            }

            //檢查無效資料-刪除子資料
            for (int i = 0; i < this.list_取藥堆疊子資料.Count; i++)
            {
                GUID = this.list_取藥堆疊子資料[i][(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();
                if (list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.GUID, GUID).Count == 0)
                {
                    list_取藥堆疊子資料_DeleteValue.Add(this.list_取藥堆疊子資料[i]);
                }

            }
            for (int i = 0; i < list_取藥堆疊子資料_DeleteValue.Count; i++)
            {
                this.Function_取藥堆疊資料_刪除子資料(list_取藥堆疊子資料_DeleteValue[i][(int)enum_取藥堆疊子資料.GUID].ObjectToString(), true);
            }
            this.list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得子資料();
            this.list_取藥堆疊子資料.Sort(new Icp_取藥堆疊子資料_index排序());
            this.list_取藥堆疊母資料.Sort(new Icp_取藥堆疊母資料_index排序());
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_刷新新增效期(ref int cnt)
        {
            List<object[]> _list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
            if (_list_取藥堆疊母資料.Count > 0)
            {
                List<object[]> list_取藥堆疊母資料_ReplaceValue = new List<object[]>();
                List<object[]> list_取藥堆疊母資料_buf = new List<object[]>();
                List<string> TYPE = new List<string>();
                List<object> values = new List<object>();
                string 藥品碼 = "";
                string 異動量 = "";
                string 效期 = "";
                string 批號 = "";
                string IP = "";
                list_取藥堆疊母資料_buf = _list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.新增效期.GetEnumName());
                for (int i = 0; i < list_取藥堆疊母資料_buf.Count; i++)
                {
                    藥品碼 = list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    效期 = list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.效期].ObjectToString();
                    批號 = list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.批號].ObjectToString();
                    IP = list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.IP].ObjectToString();
                    Function_從SQL取得儲位到雲端資料(藥品碼);
                    this.Function_從雲端資料取得儲位(藥品碼, ref TYPE, ref values);
                    for (int k = 0; k < values.Count; k++)
                    {
                        if (TYPE[k] == DeviceType.EPD266_lock.GetEnumName() || TYPE[k] == DeviceType.EPD266.GetEnumName() || TYPE[k] == DeviceType.EPD290_lock.GetEnumName() || TYPE[k] == DeviceType.EPD290.GetEnumName())
                        {

                            Storage storage = (Storage)values[k];
                            if (storage.取得庫存(效期) == -1)
                            {
                                if (!IP.StringIsEmpty())
                                {
                                    if (storage.IP != IP) continue;
                                }
                                storage.新增效期(效期, 批號, "00");
                                List_EPD266_雲端資料.Add_NewStorage(storage);
                                //this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                                break;
                            }

                        }
                        else if (TYPE[k] == DeviceType.Pannel35.GetEnumName() || TYPE[k] == DeviceType.Pannel35_lock.GetEnumName())
                        {

                            Storage storage = (Storage)values[k];
                            if (storage.取得庫存(效期) == -1)
                            {
                                if (!IP.StringIsEmpty())
                                {
                                    if (storage.IP != IP) continue;
                                }
                                storage.新增效期(效期, 批號, "00");
                                List_Pannel35_雲端資料.Add_NewStorage(storage);
                                //this.storageUI_WT32.SQL_ReplaceStorage(storage);
                                break;
                            }

                        }
                        else if (TYPE[k] == DeviceType.EPD583_lock.GetEnumName() || TYPE[k] == DeviceType.EPD583.GetEnumName())
                        {
                            Box box = (Box)values[k];
                            if (!IP.StringIsEmpty())
                            {
                                if (box.IP != IP) continue;
                            }
                            if (box.取得庫存(效期) == -1)
                            {
                                box.新增效期(效期, 批號, "00");
                                Drawer drawer = List_EPD583_雲端資料.SortByIP(box.IP);
                                drawer.ReplaceBox(box);
                                List_EPD583_雲端資料.Add_NewDrawer(drawer);
                                //this.drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);
                                break;
                            }
                        }
                        else if (TYPE[k] == DeviceType.EPD1020_lock.GetEnumName() || TYPE[k] == DeviceType.EPD1020.GetEnumName())
                        {
                            Box box = (Box)values[k];
                            if (!IP.StringIsEmpty())
                            {
                                if (box.IP != IP) continue;
                            }
                            if (box.取得庫存(效期) == -1)
                            {
                                box.新增效期(效期, 批號, "00");
                                Drawer drawer = List_EPD1020_雲端資料.SortByIP(box.IP);
                                drawer.ReplaceByGUID(box);
                                List_EPD1020_雲端資料.Add_NewDrawer(drawer);
                                //this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                                break;
                            }
                        }
                        else if (TYPE[k] == DeviceType.RowsLED.GetEnumName())
                        {
                            RowsDevice rowsDevice = values[k] as RowsDevice;
                            if (!IP.StringIsEmpty())
                            {
                                if (rowsDevice.IP != IP) continue;
                            }
                            if (rowsDevice.取得庫存(效期) == -1)
                            {
                                rowsDevice.新增效期(效期, 批號, "00");
                                RowsLED rowsLED = List_RowsLED_雲端資料.SortByIP(rowsDevice.IP);
                                rowsLED.ReplaceRowsDevice(rowsDevice);
                                List_RowsLED_雲端資料.Add_NewRowsLED(rowsDevice);
                                //this.rowsLEDUI.SQL_ReplaceRowsLED(rowsLED);
                                break;
                            }
                        }
                        else if (TYPE[k] == DeviceType.RFID_Device.GetEnumName())
                        {
                            RFIDDevice rFIDDevice = values[k] as RFIDDevice;
                            if (!IP.StringIsEmpty())
                            {
                                if (rFIDDevice.IP != IP) continue;
                            }
                            if (rFIDDevice.取得庫存(效期) == -1)
                            {
                                rFIDDevice.新增效期(效期, 批號, "00");
                                RFIDClass rFIDClass = List_RFID_雲端資料.SortByIP(rFIDDevice.IP);
                                rFIDClass.ReplaceRFIDDevice(rFIDDevice);
                                List_RFID_雲端資料.Add_NewRFIDClass(rFIDDevice);
                                //this.rfiD_UI.SQL_ReplaceRFIDClass(rFIDClass);
                                break;
                            }
                        }
                    }

                    list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();
                    list_取藥堆疊母資料_ReplaceValue.Add(list_取藥堆疊母資料_buf[i]);
                }
                if (list_取藥堆疊母資料_ReplaceValue.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_ReplaceValue, false);
            }
            cnt++;

        }
        void cnt_Program_取藥堆疊資料_檢查資料_從SQL讀取儲位資料(ref int cnt)
        {
            if (this.list_取藥堆疊母資料.Count > 0)
            {
                var Code_LINQ = (from value in list_取藥堆疊母資料
                                 select value[(int)enum_取藥堆疊母資料.藥品碼]).ToList().Distinct();
                List<object> list_code = Code_LINQ.ToList();
                for (int i = 0; i < list_code.Count; i++)
                {
                    this.Function_從SQL取得儲位到雲端資料(list_code[i].ObjectToString());
                }

                List<object[]> list_取藥堆疊母資料_buf = new List<object[]>();
                List<object[]> list_藥品設定表 = this.sqL_DataGridView_藥品設定表.SQL_GetAllRows(false);
                List<object[]> list_藥品管制方式設定 = this.sqL_DataGridView_藥品管制方式設定.SQL_GetAllRows(false);
                bool flag_獨立作業 = false;
                bool flag_雙人覆核 = false;
                string 藥品碼 = "";
                int 總異動量 = 0;
                int 庫存量 = 0;
                int 結存量 = 0;

                for (int i = 0; i < list_取藥堆疊母資料.Count; i++)
                {
                    藥品碼 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    總異動量 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToInt32();
                    庫存量 = this.Function_從雲端資料取得庫存(藥品碼);
                    結存量 = (庫存量 + 總異動量);
                    flag_獨立作業 = this.Function_取藥堆疊資料_取得作業模式(this.list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.獨立作業);
                    flag_雙人覆核 = this.Function_取藥堆疊資料_取得作業模式(this.list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.雙人覆核);
                    if (庫存量 == -999)
                    {
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.無儲位.GetEnumName()) continue;
                        this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                        this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(this.list_取藥堆疊母資料[i], false);
                        return;
                    }
                    if (結存量 < 0)
                    {
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()) continue;
                        this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName();
                        this.Function_取藥堆疊資料_設定作業模式(this.list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示);
                        this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(this.list_取藥堆疊母資料[i], false);
                        return;
                    }
                    if (flag_獨立作業)
                    {
                        if (flag_雙人覆核)
                        {
                            if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName())
                            {
                                this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName();
                                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(this.list_取藥堆疊母資料[i], false);
                                return;
                            }
                        }
                        bool flag_單循環取藥 = false;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待入賬.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.盲盤完成.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.複盤完成.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.作業完成.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName()) flag_單循環取藥 = true;
                        if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.輸入新效期.GetEnumName()) flag_單循環取藥 = true;
                        if (flag_單循環取藥)
                        {
                            list_取藥堆疊母資料_buf.Add(list_取藥堆疊母資料[i]);
                            this.list_取藥堆疊母資料 = list_取藥堆疊母資料_buf;
                            cnt++;
                            return;
                        }
                    }
                }
            }
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_刷新無庫存(ref int cnt)
        {
            if (this.list_取藥堆疊母資料.Count > 0)
            {
                if (!plC_Button_無庫存自動補足.Bool)
                {
                    cnt++;
                    return;
                }
                List<object[]> list_取藥堆疊母資料_ReplaceValue = new List<object[]>();
                List<object[]> list_取藥堆疊母資料_buf = new List<object[]>();
                List<string> TYPE = new List<string>();
                List<object> values = new List<object>();
                string 藥品碼 = "";
                string 異動量 = "";
                string 效期 = "";
                string 批號 = "";
                string IP = "";
                list_取藥堆疊母資料_buf = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.狀態, enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName());
                for (int i = 0; i < list_取藥堆疊母資料_buf.Count; i++)
                {
                    藥品碼 = list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    效期 = "2200/01/01";
                    批號 = "自動補足";
                    IP = list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.IP].ObjectToString();
                    Function_從SQL取得儲位到雲端資料(藥品碼);

                    this.Function_從雲端資料取得儲位(藥品碼, ref TYPE, ref values);
                    for (int k = 0; k < values.Count; k++)
                    {
                        if (TYPE[k] == DeviceType.EPD266_lock.GetEnumName() || TYPE[k] == DeviceType.EPD266.GetEnumName() || TYPE[k] == DeviceType.EPD290_lock.GetEnumName() || TYPE[k] == DeviceType.EPD290.GetEnumName())
                        {

                            Storage storage = (Storage)values[k];
                            if (!IP.StringIsEmpty())
                            {
                                if (storage.IP != IP) continue;
                            }
                            storage.新增效期(效期, 批號, "100000");
                            List_EPD266_雲端資料.Add_NewStorage(storage);
                            this.storageUI_EPD_266.SQL_ReplaceStorage(storage);
                            break;

                        }
                        else if (TYPE[k] == DeviceType.Pannel35.GetEnumName() || TYPE[k] == DeviceType.Pannel35_lock.GetEnumName())
                        {

                            Storage storage = (Storage)values[k];
                            if (!IP.StringIsEmpty())
                            {
                                if (storage.IP != IP) continue;
                            }
                            storage.新增效期(效期, 批號, "100000");
                            List_Pannel35_雲端資料.Add_NewStorage(storage);
                            this.storageUI_WT32.SQL_ReplaceStorage(storage);
                            break;

                        }
                        else if (TYPE[k] == DeviceType.EPD583_lock.GetEnumName() || TYPE[k] == DeviceType.EPD583.GetEnumName())
                        {

                            Box box = (Box)values[k];
                            if (!IP.StringIsEmpty())
                            {
                                if (box.IP != IP) continue;
                            }
                            box.新增效期(效期, 批號, "100000");
                            Drawer drawer = List_EPD583_雲端資料.SortByIP(box.IP);
                            drawer.ReplaceBox(box);
                            List_EPD583_雲端資料.Add_NewDrawer(drawer);
                            this.drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);
                            break;

                        }
                        else if (TYPE[k] == DeviceType.EPD1020_lock.GetEnumName() || TYPE[k] == DeviceType.EPD1020.GetEnumName())
                        {

                            Box box = (Box)values[k];
                            if (!IP.StringIsEmpty())
                            {
                                if (box.IP != IP) continue;
                            }
                            box.新增效期(效期, 批號, "100000");
                            Drawer drawer = List_EPD1020_雲端資料.SortByIP(box.IP);
                            drawer.ReplaceByGUID(box);
                            List_EPD1020_雲端資料.Add_NewDrawer(drawer);
                            this.drawerUI_EPD_1020.SQL_ReplaceDrawer(drawer);
                            break;

                        }
                        else if (TYPE[k] == DeviceType.RowsLED.GetEnumName())
                        {
                            RowsDevice rowsDevice = values[k] as RowsDevice;
                            if (!IP.StringIsEmpty())
                            {
                                if (rowsDevice.IP != IP) continue;
                            }
                            rowsDevice.新增效期(效期, 批號, "100000");
                            RowsLED rowsLED = List_RowsLED_雲端資料.SortByIP(rowsDevice.IP);
                            rowsLED.ReplaceRowsDevice(rowsDevice);
                            List_RowsLED_雲端資料.Add_NewRowsLED(rowsDevice);
                            this.rowsLEDUI.SQL_ReplaceRowsLED(rowsLED);
                            break;
                        }
                        else if (TYPE[k] == DeviceType.RFID_Device.GetEnumName())
                        {
                            RFIDDevice rFIDDevice = values[k] as RFIDDevice;
                            if (!IP.StringIsEmpty())
                            {
                                if (rFIDDevice.IP != IP) continue;
                            }
                            rFIDDevice.新增效期(效期, 批號, "100000");
                            RFIDClass rFIDClass = List_RFID_雲端資料.SortByIP(rFIDDevice.IP);
                            rFIDClass.ReplaceRFIDDevice(rFIDDevice);
                            List_RFID_雲端資料.Add_NewRFIDClass(rFIDDevice);
                            this.rfiD_UI.SQL_ReplaceRFIDClass(rFIDClass);
                            break;
                        }
                    }

                    list_取藥堆疊母資料_buf[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();
                    list_取藥堆疊母資料_ReplaceValue.Add(list_取藥堆疊母資料_buf[i]);
                }
                if (list_取藥堆疊母資料_ReplaceValue.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_ReplaceValue, false);
            }
            cnt++;

        }
        void cnt_Program_取藥堆疊資料_檢查資料_刷新資料(ref int cnt)
        {
            if (this.list_取藥堆疊母資料.Count > 0)
            {
                string 藥品碼 = "";
                string 調劑台名稱 = "";
                string GUID = "";
                string 效期 = "";
                string 批號 = "";
                string IP = "";
                int 總異動量 = 0;
                int 庫存量 = 0;
                int 結存量 = 0;
                bool flag_取藥堆疊母資料_Update = false;
                List<object[]> list_取藥堆疊母資料_ReplaceValue = new List<object[]>();
                List<object[]> list_取藥堆疊母資料_DeleteValue = new List<object[]>();
                List<object[]> list_取藥堆疊子資料_buf = new List<object[]>();
                List<object[]> list_取藥堆疊子資料_DeleteValue = new List<object[]>();
                List<object[]> list_取藥堆疊子資料_ReplaceValue = new List<object[]>();


                this.list_取藥堆疊母資料 = (from value in this.list_取藥堆疊母資料
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.None.GetEnumName()
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName()
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.等待入賬.GetEnumName()
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.新增效期.GetEnumName()
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName()
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.雙人覆核.GetEnumName()
                                     where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.已領用過.GetEnumName()
                                     select value).ToList();

                for (int i = 0; i < this.list_取藥堆疊母資料.Count; i++)
                {
                    flag_取藥堆疊母資料_Update = false;
                    GUID = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                    調劑台名稱 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString();
                    藥品碼 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                    總異動量 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToInt32();
                    庫存量 = this.Function_從雲端資料取得庫存(藥品碼);
                    結存量 = (庫存量 + 總異動量);
                    效期 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.效期].ObjectToString();
                    批號 = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.批號].ObjectToString();
                    IP = this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.IP].ObjectToString();
                    if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.庫存量].ObjectToString() != 庫存量.ToString())
                    {
                        this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.庫存量] = 庫存量.ToString();
                        flag_取藥堆疊母資料_Update = true;
                    }
                    if (this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量].ObjectToString() != 結存量.ToString())
                    {
                        this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.結存量] = 結存量.ToString();
                        flag_取藥堆疊母資料_Update = true;
                    }


                    //找無儲位
                    if (庫存量 == -999)
                    {
                        this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.無儲位.GetEnumName();
                        flag_取藥堆疊母資料_Update = true;
                        //list_取藥堆疊母資料_DeleteValue.Add(this.list_取藥堆疊母資料[i]);
                    }
                    //無庫存
                    else if (結存量 < 0)
                    {
                        this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName();
                        // this.Function_取藥堆疊資料_設定作業模式(this.list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.庫存不足語音提示);
                        flag_取藥堆疊母資料_Update = true;
                    }
                    //更新取藥子堆疊資料
                    else if (總異動量 == 0 || 庫存量 >= 0)
                    {
                        if (this.Function_取藥堆疊資料_取得作業模式(this.list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.效期管控) && 效期.StringIsEmpty())
                        {
                            this.list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.選擇效期.GetEnumName();
                            flag_取藥堆疊母資料_Update = true;
                        }
                        else
                        {
                            List<object[]> 儲位資訊 = new List<object[]>();
                            string 儲位資訊_TYPE = "";
                            string 儲位資訊_IP = "";
                            string 儲位資訊_Num = "";
                            string 儲位資訊_效期 = "";
                            string 儲位資訊_批號 = "";

                            string 儲位資訊_庫存 = "";
                            string 儲位資訊_異動量 = "";
                            string 儲位資訊_GUID = "";
                            list_取藥堆疊子資料_buf = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, GUID);


                            if (效期.StringIsEmpty())
                            {
                                儲位資訊 = this.Function_取得異動儲位資訊從雲端資料(藥品碼, 總異動量);
                            }
                            else
                            {
                                if (IP.StringIsEmpty())
                                {
                                    儲位資訊 = this.Function_取得異動儲位資訊從雲端資料(藥品碼, 總異動量, 效期);
                                }
                                else
                                {
                                    儲位資訊 = this.Function_取得異動儲位資訊從雲端資料(藥品碼, 總異動量, 效期, IP);
                                }
                            }


                            if (儲位資訊.Count == 0 && 結存量 > 0)
                            {
                                List<string> list_效期 = new List<string>();
                                List<string> list_批號 = new List<string>();
                                if(效期.StringIsEmpty())
                                {
                                    Funnction_交易記錄查詢_取得指定藥碼批號期效期(藥品碼, ref list_效期, ref list_批號);
                                    if (list_效期.Count > 0 && list_批號.Count > 0)
                                    {
                                        效期 = list_效期[0];
                                        批號 = list_批號[0];
                                    }
                                }
                          
                                儲位資訊 = Function_新增效期至雲端資料(藥品碼, 總異動量, 效期, 批號);
                            }

                            List<object[]> list_sortValue = new List<object[]>();
                            //檢查子資料新增及修改
                            for (int m = 0; m < list_取藥堆疊子資料_buf.Count; m++)
                            {
                                bool flag_Delete = true;
                                for (int k = 0; k < 儲位資訊.Count; k++)
                                {
                                    this.Function_取藥堆疊資料_取得儲位資訊內容(儲位資訊[k], ref 儲位資訊_GUID, ref 儲位資訊_TYPE, ref 儲位資訊_IP, ref 儲位資訊_Num, ref 儲位資訊_效期, ref 儲位資訊_批號, ref 儲位資訊_庫存, ref 儲位資訊_異動量);
                                    if (list_取藥堆疊子資料_buf[m][(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == 儲位資訊_TYPE)
                                        if (list_取藥堆疊子資料_buf[m][(int)enum_取藥堆疊子資料.IP].ObjectToString() == 儲位資訊_IP)
                                            if (list_取藥堆疊子資料_buf[m][(int)enum_取藥堆疊子資料.Num].ObjectToString() == 儲位資訊_Num)
                                                if (list_取藥堆疊子資料_buf[m][(int)enum_取藥堆疊子資料.效期].ObjectToString() == 儲位資訊_效期)
                                                {
                                                    flag_Delete = false;
                                                    break;
                                                }
                                }
                                if (flag_Delete)
                                {
                                    this.Function_取藥堆疊資料_刪除子資料(list_取藥堆疊子資料_buf[m][(int)enum_取藥堆疊子資料.GUID].ObjectToString(), false);
                                }
                            }
                            for (int k = 0; k < 儲位資訊.Count; k++)
                            {

                                this.Function_取藥堆疊資料_取得儲位資訊內容(儲位資訊[k], ref 儲位資訊_GUID, ref 儲位資訊_TYPE, ref 儲位資訊_IP, ref 儲位資訊_Num, ref 儲位資訊_效期, ref 儲位資訊_批號, ref 儲位資訊_庫存, ref 儲位資訊_異動量);

                                list_sortValue = (from value in list_取藥堆疊子資料_buf
                                                  where value[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == 儲位資訊_TYPE
                                                  where value[(int)enum_取藥堆疊子資料.IP].ObjectToString() == 儲位資訊_IP
                                                  where value[(int)enum_取藥堆疊子資料.Num].ObjectToString() == 儲位資訊_Num
                                                  where value[(int)enum_取藥堆疊子資料.效期].ObjectToString() == 儲位資訊_效期
                                                  select value).ToList();
                                if (list_sortValue.Count != 1)
                                {
                                    for (int m = 0; m < list_sortValue.Count; m++)
                                    {
                                        this.Function_取藥堆疊資料_刪除子資料(list_取藥堆疊子資料_buf[m][(int)enum_取藥堆疊子資料.GUID].ObjectToString(), false);
                                    }
                                    object[] value = this.Function_取藥堆疊資料_新增子資料(GUID, 儲位資訊_GUID, 調劑台名稱, 藥品碼, 儲位資訊_IP, 儲位資訊_Num, 儲位資訊_TYPE, 儲位資訊_效期, 儲位資訊_批號, 儲位資訊_異動量);

                                    list_取藥堆疊子資料_buf.Add(value);
                                    this.Function_庫存異動至雲端資料(儲位資訊[k]);
                                }
                                else
                                {
                                    if (list_sortValue[0][(int)enum_取藥堆疊子資料.異動量].ObjectToString() != 儲位資訊_異動量)
                                    {
                                        list_sortValue[0][(int)enum_取藥堆疊子資料.異動量] = 儲位資訊_異動量;
                                        list_取藥堆疊子資料_ReplaceValue.Add(list_sortValue[0]);
                                    }
                                    if (list_sortValue[0][(int)enum_取藥堆疊子資料.已入賬].ObjectToString() == false.ToString())
                                    {
                                        this.Function_庫存異動至雲端資料(儲位資訊[k]);
                                    }
                                }
                            }
                        }



                    }



                    if (flag_取藥堆疊母資料_Update)
                    {
                        list_取藥堆疊母資料_ReplaceValue.Add(this.list_取藥堆疊母資料[i]);
                    }
                    else
                    {
                        this.Function_取藥堆疊資料_檢查資料儲位正常(this.list_取藥堆疊母資料[i]);
                    }
                }

                if (list_取藥堆疊母資料_DeleteValue.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_DeleteExtra(list_取藥堆疊母資料_DeleteValue, false);
                if (list_取藥堆疊母資料_ReplaceValue.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_ReplaceValue, false);
                if (list_取藥堆疊子資料_ReplaceValue.Count > 0) this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊子資料_ReplaceValue, false);

            }
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_設定致能(ref int cnt)
        {
            List<object[]> list_取藥堆疊子資料 = this.Function_取藥堆疊子資料_取得可致能(ref this.list_取藥堆疊子資料);
            List<object[]> list_取藥堆疊母資料_buf;
            List<object[]> list_取藥堆疊資料_ReplaceValue = new List<object[]>();
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            List<object[]> list_locker_table_value_buf = new List<object[]>();
            List<object[]> list_locker_table_value_ReplaceValue = new List<object[]>();
            List<string> list_lock_IP = new List<string>();
            string IP = "";
            string Slave_GUID = "";
            string Master_GUID = "";
            string Device_GUID = "";
            string Num = "";
            string 藥品碼 = "";
            Color color = Color.Black;

            List<string> list_已亮燈藥碼 = new List<string>();
            List<string> list_已亮燈藥碼_buf = new List<string>();
            list_取藥堆疊子資料 = list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.致能, false.ToString());

            foreach (object[] 取藥堆疊資料 in list_取藥堆疊子資料)
            {
                Function_取藥堆疊資料_語音提示(取藥堆疊資料);
                IP = 取藥堆疊資料[(int)enum_取藥堆疊子資料.IP].ObjectToString();
                Master_GUID = 取藥堆疊資料[(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();
                Slave_GUID = 取藥堆疊資料[(int)enum_取藥堆疊子資料.GUID].ObjectToString();
                Device_GUID = 取藥堆疊資料[(int)enum_取藥堆疊子資料.Device_GUID].ObjectToString();
                藥品碼 = 取藥堆疊資料[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();

                list_取藥堆疊母資料_buf = list_取藥堆疊母資料.GetRows((int)enum_取藥堆疊母資料.GUID, Master_GUID);
                if (list_取藥堆疊母資料_buf.Count > 0) color = list_取藥堆疊母資料_buf[0][(int)enum_取藥堆疊母資料.顏色].ObjectToString().ToColor();

                取藥堆疊資料[(int)enum_取藥堆疊子資料.致能] = true.ToString();
                list_取藥堆疊資料_ReplaceValue.Add(取藥堆疊資料);

                list_已亮燈藥碼_buf = (from temp in list_已亮燈藥碼
                                  where temp == 藥品碼
                                  select temp).ToList();
                if (list_已亮燈藥碼_buf.Count != 0) continue;

                if (plC_Button_同藥碼全亮.Bool)
                {

                    this.Function_儲位亮燈(藥品碼, color, ref list_lock_IP);
                    list_已亮燈藥碼.Add(藥品碼);
                    for (int k = 0; k < list_lock_IP.Count; k++)
                    {
                        list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.IP, list_lock_IP[k]);
                        if (list_locker_table_value_buf.Count > 0)
                        {
                            list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Master_GUID] = Master_GUID;
                            list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Device_GUID] = Device_GUID;
                            list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Slave_GUID] = Slave_GUID;
                            list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.輸出狀態] = true.ToString();
                            list_locker_table_value_ReplaceValue.Add(list_locker_table_value_buf[0]);
                        }
                    }

                }
                if (!plC_Button_同藥碼全亮.Bool)
                {
                    if (取藥堆疊資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD583_lock.GetEnumName())
                    {
                        if (藥品碼.StringIsEmpty()) return;
                        Drawer drawer = List_EPD583_雲端資料.SortByIP(IP);
                        List<Box> boxes = drawer.SortByCode(藥品碼);
                        if (drawer.IsAllLight)
                        {
                            drawer.LED_Bytes = DrawerUI_EPD_583.Set_LEDBytes(drawer, boxes, color);
                            drawer.LED_Bytes = DrawerUI_EPD_583.Set_Pannel_LEDBytes(drawer, color);
                        }
                        else
                        {
                            drawer.LED_Bytes = DrawerUI_EPD_583.Set_Pannel_LEDBytes(drawer, color);
                        }
                        if (!plC_CheckBox_測試模式.Checked)
                        {
                            this.drawerUI_EPD_583.Set_LED_UDP(drawer);
                            list_已亮燈藥碼.Add(藥品碼);
                        }

                        list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.IP, IP);
                    }
                    if (取藥堆疊資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD1020_lock.GetEnumName() || 取藥堆疊資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD1020.GetEnumName())
                    {
                        Drawer drawer = List_EPD1020_雲端資料.SortByIP(IP);
                        List<Box> boxes = drawer.SortByCode(藥品碼);
                        if (!plC_CheckBox_測試模式.Checked)
                        {
                            this.drawerUI_EPD_1020.Set_Pannel_LED_UDP(drawer, color);
                            list_已亮燈藥碼.Add(藥品碼);
                        }

                        list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.IP, IP);
                    }
                    if (取藥堆疊資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD266_lock.GetEnumName() || 取藥堆疊資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD290_lock.GetEnumName())
                    {
                        Storage storage = List_EPD266_雲端資料.SortByIP(IP);
                        if (!plC_CheckBox_測試模式.Checked)
                        {
                            this.storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);
                            list_已亮燈藥碼.Add(藥品碼);
                        }

                        list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.IP, IP);
                    }
                }



                if (取藥堆疊資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.Pannel35_lock.GetEnumName())
                {
                    Storage storage = List_Pannel35_雲端資料.SortByIP(IP);
                    if (!plC_CheckBox_測試模式.Checked)
                    {
                        this.storageUI_WT32.Set_Stroage_LED_UDP(storage, color);
                        list_已亮燈藥碼.Add(藥品碼);

                    }

                    list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.IP, IP);
                }

                if (取藥堆疊資料[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.RFID_Device.GetEnumName())
                {
                    RFIDClass rFIDClass = List_RFID_雲端資料.SortByIP(IP);
                    RFIDDevice rFIDDevice = rFIDClass.SortByGUID(Device_GUID);
                    Num = rFIDDevice.MasterIndex.ToString();
                    list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.IP, IP);
                    list_locker_table_value_buf = list_locker_table_value_buf.GetRows((int)enum_Locker_Index_Table.Num, Num.ToString());
                }

                if (list_locker_table_value_buf.Count == 0) continue;
                list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Master_GUID] = Master_GUID;
                list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Device_GUID] = Device_GUID;
                list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Slave_GUID] = Slave_GUID;
                list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.輸出狀態] = true.ToString();
                list_locker_table_value_ReplaceValue.Add(list_locker_table_value_buf[0]);
                Console.WriteLine($"開啟抽屜致能,藥品碼:{藥品碼} {DateTime.Now.ToDateTimeString()}");
            }

            if (list_locker_table_value_ReplaceValue.Count > 0) this.sqL_DataGridView_Locker_Index_Table.SQL_ReplaceExtra(list_locker_table_value_ReplaceValue, false);
            if (list_取藥堆疊資料_ReplaceValue.Count > 0) this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊資料_ReplaceValue, false);

            this.MyTimer_取藥堆疊資料_刷新時間.TickStop();
            this.MyTimer_取藥堆疊資料_刷新時間.StartTickTime();
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_檢查資料_等待刷新時間到達(ref int cnt)
        {
            if (this.MyTimer_取藥堆疊資料_刷新時間.IsTimeOut())
            {
                cnt++;
            }
        }
        #endregion
        #region PLC_取藥堆疊資料_狀態更新
        PLC_Device PLC_Device_取藥堆疊資料_狀態更新 = new PLC_Device("");
        PLC_Device PLC_Device_取藥堆疊資料_狀態更新_OK = new PLC_Device("");
        int cnt_Program_取藥堆疊資料_狀態更新 = 65534;
        void sub_Program_取藥堆疊資料_狀態更新()
        {
            PLC_Device_取藥堆疊資料_狀態更新.Bool = PLC_Device_主機扣賬模式.Bool;
            if (cnt_Program_取藥堆疊資料_狀態更新 == 65534)
            {
                PLC_Device_取藥堆疊資料_狀態更新.SetComment("PLC_取藥堆疊資料_狀態更新");
                PLC_Device_取藥堆疊資料_狀態更新_OK.SetComment("PLC_取藥堆疊資料_狀態更新_OK");
                PLC_Device_取藥堆疊資料_狀態更新.Bool = false;
                cnt_Program_取藥堆疊資料_狀態更新 = 65535;
            }
            if (cnt_Program_取藥堆疊資料_狀態更新 == 65535) cnt_Program_取藥堆疊資料_狀態更新 = 1;
            if (cnt_Program_取藥堆疊資料_狀態更新 == 1) cnt_Program_取藥堆疊資料_狀態更新_檢查按下(ref cnt_Program_取藥堆疊資料_狀態更新);
            if (cnt_Program_取藥堆疊資料_狀態更新 == 2) cnt_Program_取藥堆疊資料_狀態更新_初始化(ref cnt_Program_取藥堆疊資料_狀態更新);
            if (cnt_Program_取藥堆疊資料_狀態更新 == 3) cnt_Program_取藥堆疊資料_狀態更新 = 65500;
            if (cnt_Program_取藥堆疊資料_狀態更新 > 1) cnt_Program_取藥堆疊資料_狀態更新_檢查放開(ref cnt_Program_取藥堆疊資料_狀態更新);

            if (cnt_Program_取藥堆疊資料_狀態更新 == 65500)
            {
                PLC_Device_取藥堆疊資料_狀態更新.Bool = false;
                PLC_Device_取藥堆疊資料_狀態更新_OK.Bool = false;
                cnt_Program_取藥堆疊資料_狀態更新 = 65535;
            }
        }
        void cnt_Program_取藥堆疊資料_狀態更新_檢查按下(ref int cnt)
        {
            if (PLC_Device_取藥堆疊資料_狀態更新.Bool) cnt++;
        }
        void cnt_Program_取藥堆疊資料_狀態更新_檢查放開(ref int cnt)
        {
            if (!PLC_Device_取藥堆疊資料_狀態更新.Bool) cnt = 65500;
        }
        void cnt_Program_取藥堆疊資料_狀態更新_初始化(ref int cnt)
        {
            string 狀態 = "";
            string 狀態_buf = "";
            string GUID = "";
            bool 致能 = true;
            bool 流程作業完成 = true;
            bool 配藥完成 = true;
            bool 調劑結束 = true;
            bool 已入賬 = true;
            List<object[]> _list_取藥堆疊母資料 = this.Function_取藥堆疊資料_取得母資料();
            List<object[]> _list_取藥堆疊子資料 = this.Function_取藥堆疊資料_取得子資料();
            List<object[]> _list_取藥堆疊母資料_ReplaceValue = new List<object[]>();
            List<object[]> _list_取藥堆疊子資料_ReplaceValue = new List<object[]>();
            List<object[]> _list_取藥堆疊子資料_buf = new List<object[]>();


            _list_取藥堆疊母資料 = (from value in _list_取藥堆疊母資料
                             where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName()
                             where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.等待入賬.GetEnumName()
                             where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.新增效期.GetEnumName()
                             where value[(int)enum_取藥堆疊母資料.狀態].ObjectToString() != enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName()
                             select value).ToList();

            _list_取藥堆疊母資料.Sort(new Icp_取藥堆疊母資料_index排序());
            for (int i = 0; i < _list_取藥堆疊母資料.Count; i++)
            {
                致能 = true;
                流程作業完成 = true;
                配藥完成 = true;
                調劑結束 = true;
                已入賬 = true;
                狀態_buf = 狀態 = _list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態].ObjectToString();
                GUID = _list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                _list_取藥堆疊子資料_buf = _list_取藥堆疊子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, GUID);

                for (int k = 0; k < _list_取藥堆疊子資料_buf.Count; k++)
                {
                    if (_list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能].ObjectToString() == false.ToString())
                    {
                        致能 = false;
                        break;
                    }
                }
                for (int k = 0; k < _list_取藥堆疊子資料_buf.Count; k++)
                {
                    if (_list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成].ObjectToString() == false.ToString())
                    {
                        流程作業完成 = false;
                        break;
                    }
                }
                for (int k = 0; k < _list_取藥堆疊子資料_buf.Count; k++)
                {
                    if (_list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString())
                    {
                        配藥完成 = false;
                        break;
                    }
                }
                for (int k = 0; k < _list_取藥堆疊子資料_buf.Count; k++)
                {
                    if (_list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.調劑結束].ObjectToString() == false.ToString())
                    {
                        調劑結束 = false;
                        break;
                    }
                }
                for (int k = 0; k < _list_取藥堆疊子資料_buf.Count; k++)
                {
                    if (_list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.已入賬].ObjectToString() == false.ToString())
                    {
                        已入賬 = false;
                        break;
                    }
                }
                if (_list_取藥堆疊子資料_buf.Count > 0)
                {
                    if (已入賬) 狀態_buf = enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName();
                    else if (調劑結束) 狀態_buf = enum_取藥堆疊母資料_狀態.等待入賬.GetEnumName();
                    else if (配藥完成) 狀態_buf = enum_取藥堆疊母資料_狀態.作業完成.GetEnumName();
                    else if (致能)
                    {
                        if (Function_取藥堆疊資料_取得作業模式(_list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.複盤)) 狀態_buf = enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName();
                        else if (Function_取藥堆疊資料_取得作業模式(_list_取藥堆疊母資料[i], enum_取藥堆疊母資料_作業模式.盲盤)) 狀態_buf = enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName();
                        else 狀態_buf = enum_取藥堆疊母資料_狀態.等待作業.GetEnumName();
                    }
                    if (_list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.動作].ObjectToString().Contains("系統"))

                    {
                        if (狀態_buf == enum_取藥堆疊母資料_狀態.作業完成.GetEnumName())
                        {
                            狀態_buf = new enum_取藥堆疊母資料_狀態().GetEnumName();
                            for (int k = 0; k < _list_取藥堆疊子資料_buf.Count; k++)
                            {
                                _list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.致能] = true.ToString();
                                _list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                                _list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                                _list_取藥堆疊子資料_buf[k][(int)enum_取藥堆疊子資料.調劑結束] = true.ToString();
                                _list_取藥堆疊子資料_ReplaceValue.Add(_list_取藥堆疊子資料_buf[k]);
                            }
                        }
                    }
                }

                if (狀態_buf != 狀態)
                {
                    狀態 = 狀態_buf;

                    _list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.狀態] = 狀態;
                    _list_取藥堆疊母資料[i][(int)enum_取藥堆疊母資料.操作時間] = DateTime.Now.ToDateTimeString_6();
                    _list_取藥堆疊母資料_ReplaceValue.Add(_list_取藥堆疊母資料[i]);
                }



            }
            if (_list_取藥堆疊母資料_ReplaceValue.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(_list_取藥堆疊母資料_ReplaceValue, false);
            }
            if (_list_取藥堆疊子資料_ReplaceValue.Count > 0)
            {
                this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(_list_取藥堆疊子資料_ReplaceValue, false);
            }
            cnt++;
        }


        #endregion
        #region PLC_取藥堆疊資料_流程作業檢查
        List<object[]> list_流程作業檢查_取藥母堆疊資料 = new List<object[]>();
        List<object[]> list_流程作業檢查_取藥子堆疊資料 = new List<object[]>();
        PLC_Device PLC_Device_取藥堆疊資料_流程作業檢查 = new PLC_Device("");
        PLC_Device PLC_Device_取藥堆疊資料_流程作業檢查_不檢測 = new PLC_Device("S5246");
        public int 取藥堆疊資料_流程作業檢查_感測設定值 = 80;
        MyTimer MyTimer_取藥堆疊資料_流程作業檢查 = new MyTimer("K1");
        MyTimer MyTimer_取藥堆疊資料_流程作業檢查時間 = new MyTimer();
        int cnt_Program_取藥堆疊資料_流程作業檢查 = 65534;
        void sub_Program_取藥堆疊資料_流程作業檢查()
        {
            PLC_Device_取藥堆疊資料_流程作業檢查.Bool = PLC_Device_主機扣賬模式.Bool;
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 65534)
            {
                PLC_Device_取藥堆疊資料_流程作業檢查.SetComment("PLC_取藥堆疊資料_流程作業檢查");
                PLC_Device_取藥堆疊資料_流程作業檢查.Bool = false;
                cnt_Program_取藥堆疊資料_流程作業檢查 = 65535;
            }
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 65535) cnt_Program_取藥堆疊資料_流程作業檢查 = 1;
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 1) cnt_Program_取藥堆疊資料_流程作業檢查_檢查按下(ref cnt_Program_取藥堆疊資料_流程作業檢查);
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 2) cnt_Program_取藥堆疊資料_流程作業檢查_初始化(ref cnt_Program_取藥堆疊資料_流程作業檢查);
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 3) cnt_Program_取藥堆疊資料_流程作業檢查_檢查盲盤複盤(ref cnt_Program_取藥堆疊資料_流程作業檢查);
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 4) cnt_Program_取藥堆疊資料_流程作業檢查_檢查同藥碼全亮(ref cnt_Program_取藥堆疊資料_流程作業檢查);
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 5) cnt_Program_取藥堆疊資料_流程作業檢查_檢查層架及手勢感測(ref cnt_Program_取藥堆疊資料_流程作業檢查);
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 6) cnt_Program_取藥堆疊資料_流程作業檢查 = 65500;
            if (cnt_Program_取藥堆疊資料_流程作業檢查 > 1) cnt_Program_取藥堆疊資料_流程作業檢查_檢查放開(ref cnt_Program_取藥堆疊資料_流程作業檢查);
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 65500)
            {
                this.MyTimer_取藥堆疊資料_流程作業檢查.TickStop();
                this.MyTimer_取藥堆疊資料_流程作業檢查.StartTickTime();
                cnt_Program_取藥堆疊資料_流程作業檢查 = 65501;
            }
            if (cnt_Program_取藥堆疊資料_流程作業檢查 == 65501)
            {
                if (this.MyTimer_取藥堆疊資料_流程作業檢查.IsTimeOut())
                {
                    PLC_Device_取藥堆疊資料_流程作業檢查.Bool = false;
                    cnt_Program_取藥堆疊資料_流程作業檢查 = 65535;
                }
            }
        }
        void cnt_Program_取藥堆疊資料_流程作業檢查_檢查按下(ref int cnt)
        {
            if (PLC_Device_取藥堆疊資料_流程作業檢查.Bool) cnt++;
        }
        void cnt_Program_取藥堆疊資料_流程作業檢查_檢查放開(ref int cnt)
        {
            if (!PLC_Device_取藥堆疊資料_流程作業檢查.Bool) cnt = 65500;
        }
        void cnt_Program_取藥堆疊資料_流程作業檢查_初始化(ref int cnt)
        {




            cnt++;
        }
        void cnt_Program_取藥堆疊資料_流程作業檢查_檢查盲盤複盤(ref int cnt)
        {
            string IP = "";
            string 藥品碼 = "";
            string 調劑台名稱 = "";
            string GUID = "";
            string Master_GUID = "";
            string Device_GUID = "";
            bool flag_TOFON = false;
            Color color = Color.Black;
            list_流程作業檢查_取藥母堆疊資料 = this.sqL_DataGridView_取藥堆疊母資料.SQL_GetAllRows(false);
            list_流程作業檢查_取藥子堆疊資料 = this.sqL_DataGridView_取藥堆疊子資料.SQL_GetAllRows(false);

            List<object[]> list_取藥母堆疊資料 = list_流程作業檢查_取藥母堆疊資料;
            List<object[]> list_取藥母堆疊資料_buf = new List<object[]>();
            List<object[]> list_取藥子堆疊資料 = list_流程作業檢查_取藥子堆疊資料;
            List<object[]> list_取藥子堆疊資料_buf = new List<object[]>();

            list_取藥子堆疊資料_buf = (from value in list_取藥子堆疊資料
                                where value[(int)enum_取藥堆疊子資料.致能].ObjectToString() == true.ToString()
                                where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                select value).ToList();

            for (int i = 0; i < list_取藥子堆疊資料_buf.Count; i++)
            {
                GUID = list_取藥子堆疊資料_buf[i][(int)enum_取藥堆疊子資料.GUID].ObjectToString();
                Master_GUID = list_取藥子堆疊資料_buf[i][(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();

                List<object[]> _list_取藥母堆疊資料_buf = list_取藥母堆疊資料.GetRows((int)enum_取藥堆疊母資料.GUID, Master_GUID);

                if (_list_取藥母堆疊資料_buf.Count > 0)
                {
                    bool flag_remove = false;
                    if (_list_取藥母堆疊資料_buf[0][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待複盤.GetEnumName()) flag_remove = true;
                    if (_list_取藥母堆疊資料_buf[0][(int)enum_取藥堆疊母資料.狀態].ObjectToString() == enum_取藥堆疊母資料_狀態.等待盲盤.GetEnumName()) flag_remove = true;
                    if (flag_remove)
                    {
                        list_取藥母堆疊資料.RemoveByGUID(_list_取藥母堆疊資料_buf[0]);
                        list_取藥子堆疊資料.RemoveByGUID(GUID);
                    }

                }
            }
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_流程作業檢查_檢查同藥碼全亮(ref int cnt)
        {
            string IP = "";
            string 藥品碼 = "";
            string 調劑台名稱 = "";
            string GUID = "";
            string Master_GUID = "";
            string Device_GUID = "";
            bool flag_TOFON = false;
            Color color = Color.Black;
            List<object[]> list_取藥母堆疊資料 = list_流程作業檢查_取藥母堆疊資料;
            List<object[]> list_取藥母堆疊資料_buf = new List<object[]>();
            List<object[]> list_取藥子堆疊資料 = list_流程作業檢查_取藥子堆疊資料;
            List<object[]> list_取藥子堆疊資料_buf = new List<object[]>();

            if (plC_Button_同藥碼全亮.Bool)
            {
                list_取藥子堆疊資料_buf = (from value in list_取藥子堆疊資料
                                    where value[(int)enum_取藥堆疊子資料.致能].ObjectToString() == true.ToString()
                                    where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                    select value).ToList();

                List<object[]> list_取藥子堆疊資料_Replace = new List<object[]>();
                for (int i = 0; i < list_取藥子堆疊資料_buf.Count; i++)
                {
                    Master_GUID = list_取藥子堆疊資料[i][(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();
                    IP = list_取藥子堆疊資料[i][(int)enum_取藥堆疊子資料.IP].ObjectToString();

                    Storage storage = List_EPD266_雲端資料.SortByIP(IP);
                    if (storage != null && (storage.DeviceType == DeviceType.EPD266 || storage.DeviceType == DeviceType.EPD290))
                    {
                        if (!storage.TOFON)
                        {
                            list_取藥子堆疊資料_buf[i][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                            list_取藥子堆疊資料_buf[i][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                            list_取藥子堆疊資料_Replace.Add(list_取藥子堆疊資料_buf[i]);
                        }
                        else
                        {
                            flag_TOFON = true;
                        }
                    }
                    else
                    {
                        list_取藥子堆疊資料_buf[i][(int)enum_取藥堆疊子資料.流程作業完成] = true.ToString();
                        list_取藥子堆疊資料_buf[i][(int)enum_取藥堆疊子資料.配藥完成] = true.ToString();
                        list_取藥子堆疊資料_Replace.Add(list_取藥子堆疊資料_buf[i]);
                    }


                }
                this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥子堆疊資料_Replace, false);
                this.MyTimer_取藥堆疊資料_流程作業檢查.TickStop();
                this.MyTimer_取藥堆疊資料_流程作業檢查.StartTickTime();

                if (!flag_TOFON)
                {
                    cnt = 65500;
                    return;
                }
                else
                {
                    cnt++;
                    return;
                }
            }
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_流程作業檢查_檢查層架及手勢感測(ref int cnt)
        {
            List<Task> taskList = new List<Task>();
            string IP = "";
            string 藥品碼 = "";
            string 調劑台名稱 = "";
            string GUID = "";
            string Master_GUID = "";
            string Device_GUID = "";
            bool flag_TOFON = false;
            Color color = Color.Black;
            List<object[]> list_取藥母堆疊資料 = list_流程作業檢查_取藥母堆疊資料;
            List<object[]> list_取藥母堆疊資料_buf = new List<object[]>();
            List<object[]> list_取藥子堆疊資料 = list_流程作業檢查_取藥子堆疊資料;
            List<object[]> list_取藥子堆疊資料_buf = new List<object[]>();

            list_取藥子堆疊資料_buf = (from value in list_取藥子堆疊資料
                                where value[(int)enum_取藥堆疊子資料.致能].ObjectToString() == true.ToString()
                                where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                select value).ToList();




            List<string[]> list_需更新資料;
            List<object[]> list_取藥子堆疊資料_2_66層架_作業未完成 = new List<object[]>();
            List<object[]> list_取藥子堆疊資料_2_66層架_作業已完成 = new List<object[]>();
            List<object[]> list_取藥子堆疊資料_LED層架_作業未完成 = new List<object[]>();
            List<object[]> list_取藥子堆疊資料_LED層架_作業已完成 = new List<object[]>();

            list_取藥子堆疊資料_2_66層架_作業未完成 = (from value in list_取藥子堆疊資料
                                         where value[(int)enum_取藥堆疊子資料.致能].ObjectToString() == true.ToString()
                                         where value[(int)enum_取藥堆疊子資料.流程作業完成].ObjectToString() == false.ToString()
                                         where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                         where value[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD266.GetEnumName() || value[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD290.GetEnumName()
                                         select value).ToList();
            list_取藥子堆疊資料_2_66層架_作業已完成 = (from value in list_取藥子堆疊資料
                                         where value[(int)enum_取藥堆疊子資料.致能].ObjectToString() == true.ToString()
                                         where value[(int)enum_取藥堆疊子資料.流程作業完成].ObjectToString() == true.ToString()
                                         where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                         where value[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD266.GetEnumName() || value[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.EPD290.GetEnumName()
                                         select value).ToList();
            list_取藥子堆疊資料_LED層架_作業未完成 = (from value in list_取藥子堆疊資料
                                        where value[(int)enum_取藥堆疊子資料.致能].ObjectToString() == true.ToString()
                                        where value[(int)enum_取藥堆疊子資料.流程作業完成].ObjectToString() == false.ToString()
                                        where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                        where value[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.RowsLED.GetEnumName()
                                        select value).ToList();
            list_取藥子堆疊資料_LED層架_作業已完成 = (from value in list_取藥子堆疊資料
                                        where value[(int)enum_取藥堆疊子資料.致能].ObjectToString() == true.ToString()
                                        where value[(int)enum_取藥堆疊子資料.流程作業完成].ObjectToString() == true.ToString()
                                        where value[(int)enum_取藥堆疊子資料.配藥完成].ObjectToString() == false.ToString()
                                        where value[(int)enum_取藥堆疊子資料.TYPE].ObjectToString() == DeviceType.RowsLED.GetEnumName()
                                        select value).ToList();


            #region 2_66層架_作業未完成
            taskList = new List<Task>();
            list_需更新資料 = new List<string[]>();
            foreach (object[] value in list_取藥子堆疊資料_2_66層架_作業未完成)
            {
                IP = value[(int)enum_取藥堆疊子資料.IP].ObjectToString();
                藥品碼 = value[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
                調劑台名稱 = value[(int)enum_取藥堆疊子資料.調劑台名稱].ObjectToString();
                Master_GUID = value[(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();

                list_取藥母堆疊資料_buf = list_取藥母堆疊資料.GetRows((int)enum_取藥堆疊母資料.GUID, Master_GUID);

                if (list_取藥母堆疊資料_buf.Count > 0)
                {
                    color = list_取藥母堆疊資料_buf[0][(int)enum_取藥堆疊母資料.顏色].ObjectToString().ToColor();
                    int match = (from values in list_需更新資料
                                 where values[0] == 調劑台名稱
                                 where values[1] == IP
                                 select values).ToList().Count;
                    if (match == 0)
                    {
                        list_需更新資料.Add(new string[] { 調劑台名稱, IP });
                        Storage storage = List_EPD266_雲端資料.SortByIP(IP);
                        if (storage != null)
                        {
                            if (!plC_CheckBox_測試模式.Checked)
                            {
                                taskList.Add(Task.Run(() =>
                                {
                                    this.storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);
                                }));
                            }

                        }
                    }
                }
            }
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();

            for (int i = 0; i < list_需更新資料.Count; i++)
            {
                this.Function_取藥堆疊子資料_設定流程作業完成ByIP(list_需更新資料[i][0], list_需更新資料[i][1]);
            }
            #endregion
            #region 2_66層架_作業已完成
            taskList = new List<Task>();
            list_需更新資料 = new List<string[]>();
            List<string[]> list_手勢檢查資料 = new List<string[]>();
            foreach (object[] value in list_取藥子堆疊資料_2_66層架_作業已完成)
            {
                IP = value[(int)enum_取藥堆疊子資料.IP].ObjectToString();
                藥品碼 = value[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
                調劑台名稱 = value[(int)enum_取藥堆疊子資料.調劑台名稱].ObjectToString();
                Master_GUID = value[(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();
                list_取藥母堆疊資料_buf = list_取藥母堆疊資料.GetRows((int)enum_取藥堆疊母資料.GUID, Master_GUID);
                if (list_取藥母堆疊資料_buf.Count > 0)
                {
                    color = list_取藥母堆疊資料_buf[0][(int)enum_取藥堆疊母資料.顏色].ObjectToString().ToColor();
                    int match = (from values in list_手勢檢查資料
                                 where values[0] == 調劑台名稱
                                 where values[1] == IP
                                 select values).ToList().Count;
                    if (match == 0)
                    {
                        Storage storage = List_EPD266_雲端資料.SortByIP(IP);
                        if (storage != null)
                        {
                            if (!plC_CheckBox_測試模式.Checked)
                            {
                                taskList.Add(Task.Run(() =>
                                {
                                    int Dis_value = this.storageUI_EPD_266.Get_LaserDistance(storage);
                                    Console.WriteLine($"IP: {storage.IP} ,雷射數值 :{Dis_value}");
                                    if (Dis_value <= this.取藥堆疊資料_流程作業檢查_感測設定值 || this.PLC_Device_取藥堆疊資料_流程作業檢查_不檢測.Bool || !storage.TOFON)
                                    {
                                        //if (!this.PLC_Device_取藥堆疊資料_流程作業檢查_不檢測.Bool || !storage.TOFON) this.storageUI_EPD_266.Set_Stroage_LED_UDP(storage, Color.Black);
                                        list_需更新資料.Add(new string[] { 調劑台名稱, IP });
                                    }

                                }));
                            }

                            list_手勢檢查資料.Add(new string[] { 調劑台名稱, IP });
                        }
                    }
                }

            }




            allTask = Task.WhenAll(taskList);
            allTask.Wait();
            for (int i = 0; i < list_需更新資料.Count; i++)
            {
                this.Function_取藥堆疊子資料_設定配藥完成ByIP(list_需更新資料[i][0], list_需更新資料[i][1]);
            }
            #endregion

            #region LED層架_作業未完成
            taskList = new List<Task>();
            list_需更新資料 = new List<string[]>();
            foreach (object[] value in list_取藥子堆疊資料_LED層架_作業未完成)
            {
                IP = value[(int)enum_取藥堆疊子資料.IP].ObjectToString();
                藥品碼 = value[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
                調劑台名稱 = value[(int)enum_取藥堆疊子資料.調劑台名稱].ObjectToString();
                Master_GUID = value[(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();
                Device_GUID = value[(int)enum_取藥堆疊子資料.Device_GUID].ObjectToString();

                list_取藥母堆疊資料_buf = list_取藥母堆疊資料.GetRows((int)enum_取藥堆疊母資料.GUID, Master_GUID);

                if (list_取藥母堆疊資料_buf.Count > 0)
                {
                    color = list_取藥母堆疊資料_buf[0][(int)enum_取藥堆疊母資料.顏色].ObjectToString().ToColor();
                    int match = (from values in list_需更新資料
                                 where values[0] == 調劑台名稱
                                 where values[1] == IP
                                 select values).ToList().Count;
                    if (match == 0)
                    {
                        list_需更新資料.Add(new string[] { 調劑台名稱, 藥品碼, IP });

                        if (plC_Button_同藥碼全亮.Bool)
                        {

                            List<RowsDevice> rowsDevices = this.List_RowsLED_雲端資料.SortByCode(藥品碼);
                            for (int i = 0; i < rowsDevices.Count; i++)
                            {
                                RowsLED rowsLED = List_RowsLED_雲端資料.SortByIP(rowsDevices[i].IP);
                                RowsDevice rowsDevice = rowsDevices[i];
                                rowsLED.LED_Bytes = RowsLEDUI.Get_Rows_LEDBytes(ref rowsLED.LED_Bytes, rowsDevice, color);
                                if (!plC_CheckBox_測試模式.Checked)
                                {
                                    this.rowsLEDUI.Set_Rows_LED_UDP(rowsLED);
                                }

                            }
                        }
                        else
                        {
                            RowsLED rowsLED = List_RowsLED_雲端資料.SortByIP(IP);
                            RowsDevice rowsDevice = rowsLED.SortByGUID(Device_GUID);

                            if (rowsDevice != null)
                            {
                                if (!plC_CheckBox_測試模式.Checked)
                                {
                                    rowsLED.LED_Bytes = RowsLEDUI.Get_Rows_LEDBytes(ref rowsLED.LED_Bytes, rowsDevice, color);
                                    this.rowsLEDUI.Set_Rows_LED_UDP(rowsLED);
                                }
                            }
                        }

                    }
                }
            }
            allTask = Task.WhenAll(taskList);
            allTask.Wait();

            for (int i = 0; i < list_需更新資料.Count; i++)
            {
                this.Function_取藥堆疊子資料_設定流程作業完成ByCode(list_需更新資料[i][0], list_需更新資料[i][1], list_需更新資料[i][2]);
            }
            #endregion
            #region LED層架_作業已完成
            taskList = new List<Task>();
            list_需更新資料 = new List<string[]>();
            foreach (object[] value in list_取藥子堆疊資料_LED層架_作業已完成)
            {
                IP = value[(int)enum_取藥堆疊子資料.IP].ObjectToString();
                藥品碼 = value[(int)enum_取藥堆疊子資料.藥品碼].ObjectToString();
                調劑台名稱 = value[(int)enum_取藥堆疊子資料.調劑台名稱].ObjectToString();
                Master_GUID = value[(int)enum_取藥堆疊子資料.Master_GUID].ObjectToString();
                list_取藥母堆疊資料_buf = list_取藥母堆疊資料.GetRows((int)enum_取藥堆疊母資料.GUID, Master_GUID);
                if (list_取藥母堆疊資料_buf.Count > 0)
                {
                    color = list_取藥母堆疊資料_buf[0][(int)enum_取藥堆疊母資料.顏色].ObjectToString().ToColor();
                    int match = (from values in list_需更新資料
                                 where values[0] == 調劑台名稱
                                 where values[1] == IP
                                 select values).ToList().Count;
                    if (match == 0)
                    {
                        list_需更新資料.Add(new string[] { 調劑台名稱, 藥品碼, IP });
                    }
                }

            }
            allTask = Task.WhenAll(taskList);
            allTask.Wait();
            for (int i = 0; i < list_需更新資料.Count; i++)
            {
                this.Function_取藥堆疊子資料_設定配藥完成ByCode(list_需更新資料[i][0], list_需更新資料[i][1], list_需更新資料[i][2]);
            }
            #endregion
            cnt++;
        }

        #endregion      
        #region PLC_取藥堆疊資料_入賬檢查
        PLC_Device PLC_Device_取藥堆疊資料_入賬檢查 = new PLC_Device("");
        MyTimer MyTimer_取藥堆疊資料_入賬檢查刷新延遲 = new MyTimer();
        int cnt_Program_取藥堆疊資料_入賬檢查 = 65534;
        void sub_Program_取藥堆疊資料_入賬檢查()
        {
            PLC_Device_取藥堆疊資料_入賬檢查.Bool = PLC_Device_主機扣賬模式.Bool;
            if (cnt_Program_取藥堆疊資料_入賬檢查 == 65534)
            {
                PLC_Device_取藥堆疊資料_入賬檢查.SetComment("PLC_取藥堆疊資料_入賬檢查");
                PLC_Device_取藥堆疊資料_入賬檢查.Bool = false;
                cnt_Program_取藥堆疊資料_入賬檢查 = 65535;
            }
            if (cnt_Program_取藥堆疊資料_入賬檢查 == 65535) cnt_Program_取藥堆疊資料_入賬檢查 = 1;
            if (cnt_Program_取藥堆疊資料_入賬檢查 == 1) cnt_Program_取藥堆疊資料_入賬檢查_檢查按下(ref cnt_Program_取藥堆疊資料_入賬檢查);
            if (cnt_Program_取藥堆疊資料_入賬檢查 == 2) cnt_Program_取藥堆疊資料_入賬檢查_初始化(ref cnt_Program_取藥堆疊資料_入賬檢查);
            if (cnt_Program_取藥堆疊資料_入賬檢查 == 3) cnt_Program_取藥堆疊資料_入賬檢查_等待延遲(ref cnt_Program_取藥堆疊資料_入賬檢查);
            if (cnt_Program_取藥堆疊資料_入賬檢查 == 4) cnt_Program_取藥堆疊資料_入賬檢查 = 65500;
            if (cnt_Program_取藥堆疊資料_入賬檢查 > 1) cnt_Program_取藥堆疊資料_入賬檢查_檢查放開(ref cnt_Program_取藥堆疊資料_入賬檢查);
            if (cnt_Program_取藥堆疊資料_入賬檢查 == 65500)
            {

                PLC_Device_取藥堆疊資料_入賬檢查.Bool = false;
                cnt_Program_取藥堆疊資料_入賬檢查 = 65535;
            }
        }
        void cnt_Program_取藥堆疊資料_入賬檢查_檢查按下(ref int cnt)
        {
            if (PLC_Device_取藥堆疊資料_入賬檢查.Bool) cnt++;
        }
        void cnt_Program_取藥堆疊資料_入賬檢查_檢查放開(ref int cnt)
        {
            if (!PLC_Device_取藥堆疊資料_入賬檢查.Bool) cnt = 65500;
        }
        void cnt_Program_取藥堆疊資料_入賬檢查_初始化(ref int cnt)
        {
            List<object[]> list_可入賬母資料 = this.Function_取藥堆疊母資料_取得可入賬資料();
            List<object[]> list_子資料 = this.Function_取藥堆疊資料_取得子資料();
            List<object[]> list_子資料_buf;
            List<object[]> list_取藥堆疊子資料_ReplaceValue = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_ReplaceValue = new List<object[]>();
            List<object[]> list_取藥堆疊母資料_Add = new List<object[]>();
            List<object[]> list_交易紀錄新增資料_AddValue = new List<object[]>();
            List<object[]> list_醫囑資料_ReplaceValue = new List<object[]>();

            bool flag_修正盤點量 = false;
            string GUID = "";
            string Master_GUID = "";
            int 庫存量 = 0;
            int 結存量 = 0;
            int 總異動量 = 0;
            string 盤點量 = "";
            string 動作 = "";
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 藥袋序號 = "";
            string 類別 = "";
            string 交易量 = "";
            string 操作人 = "";
            string 病人姓名 = "";
            string 床號 = "";
            string 頻次 = "";
            string 病歷號 = "";
            string 操作時間 = "";
            string 開方時間 = "";
            string 備註 = "";
            string 收支原因 = "";
            string 診別 = "";
            string 藥師證字號 = "";
            string 效期 = "";
            string 批號 = "";
            string 顏色 = "";
            string 領藥號 = "";
            string 病房號 = "";
            string 醫令_GUID = "";
            string 交易紀錄_GUID = "";

            List<string> List_效期 = new List<string>();
            List<string> List_批號 = new List<string>();
            List<string> list_儲位刷新_藥品碼 = new List<string>();
            List<string> list_儲位刷新_藥品碼_buf = new List<string>();
            list_可入賬母資料.Sort(new Icp_取藥堆疊母資料_index排序());
            List<string> list_Codes = (from temp in list_可入賬母資料
                                       select temp[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString()).Distinct().ToList();
            for (int i = 0; i < list_Codes.Count; i++)
            {
                this.Function_從SQL取得儲位到入賬資料(list_Codes[i]);
            }
            for (int i = 0; i < list_可入賬母資料.Count; i++)
            {

                Master_GUID = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.GUID].ObjectToString();
                動作 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.動作].ObjectToString();
                診別 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.診別].ObjectToString();
                藥品碼 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.藥品碼].ObjectToString();
                //this.Function_從SQL取得儲位到入賬資料(藥品碼);
                藥品名稱 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.藥品名稱].ObjectToString();
                藥袋序號 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.藥袋序號].ObjectToString();
                類別 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.類別].ObjectToString();
                操作人 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.操作人].ObjectToString();
                藥師證字號 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.藥師證字號].ObjectToString();
                總異動量 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString().StringToInt32();
                交易量 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.總異動量].ObjectToString();
                盤點量 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.盤點量].ObjectToString();
                顏色 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.顏色].ObjectToString();
                領藥號 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.領藥號].ObjectToString();
                病房號 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.病房號].ObjectToString();
                病人姓名 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.病人姓名].ObjectToString();
                床號 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.床號].ObjectToString();
                頻次 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.頻次].ObjectToString();
                病歷號 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.病歷號].ObjectToString();
                操作時間 = DateTime.Now.ToDateTimeString_6();
                開方時間 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.開方時間].ObjectToString();
                備註 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.備註].ObjectToString();
                收支原因 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.收支原因].ObjectToString();
                效期 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.效期].ObjectToString();
                批號 = list_可入賬母資料[i][(int)enum_取藥堆疊母資料.批號].ObjectToString();
                庫存量 = this.Function_從入賬資料取得庫存(藥品碼);
                結存量 = (庫存量 + 總異動量);
                List_效期.Clear();
                List_批號.Clear();
                list_子資料_buf = list_子資料.GetRows((int)enum_取藥堆疊子資料.Master_GUID, Master_GUID);

                for (int k = 0; k < list_子資料_buf.Count; k++)
                {

                    list_子資料_buf[k] = this.Function_取藥堆疊子資料_設定已入賬(list_子資料_buf[k]);
                    List_效期.Add(list_子資料_buf[k][(int)enum_取藥堆疊子資料.效期].ObjectToString());
                    List_批號.Add(list_子資料_buf[k][(int)enum_取藥堆疊子資料.批號].ObjectToString());
                    list_取藥堆疊子資料_ReplaceValue.Add(list_子資料_buf[k]);
                    //Function_取藥堆疊資料_語音提示(list_子資料_buf[k]);
                }
                list_可入賬母資料[i][(int)enum_取藥堆疊母資料.庫存量] = 庫存量.ToString();
                list_可入賬母資料[i][(int)enum_取藥堆疊母資料.結存量] = 結存量.ToString();
                list_可入賬母資料[i][(int)enum_取藥堆疊母資料.狀態] = enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName();
                list_取藥堆疊母資料_ReplaceValue.Add(list_可入賬母資料[i]);



                //新增交易紀錄資料
                for (int k = 0; k < List_效期.Count; k++)
                {
                    備註 += $"[效期]:{List_效期[k]},[批號]:{List_批號[k]}";
                    if (k != List_效期.Count - 1) 備註 += "\n";
                }

                object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                value_trading[(int)enum_交易記錄查詢資料.GUID] = Guid.NewGuid().ToString();
                交易紀錄_GUID = value_trading[(int)enum_交易記錄查詢資料.GUID].ObjectToString();
                value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                value_trading[(int)enum_交易記錄查詢資料.診別] = 診別;
                value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                value_trading[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
                value_trading[(int)enum_交易記錄查詢資料.藥師證字號] = 藥師證字號;
                value_trading[(int)enum_交易記錄查詢資料.領藥號] = 領藥號;
                value_trading[(int)enum_交易記錄查詢資料.病房號] = 病房號;
                value_trading[(int)enum_交易記錄查詢資料.類別] = 類別;
                value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                if (盤點量.StringIsEmpty()) 盤點量 = "無";
                else flag_修正盤點量 = true;
                value_trading[(int)enum_交易記錄查詢資料.盤點量] = 盤點量;
                value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                value_trading[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
                value_trading[(int)enum_交易記錄查詢資料.床號] = 床號;
                value_trading[(int)enum_交易記錄查詢資料.頻次] = 頻次;
                value_trading[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
                value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                if (開方時間.StringIsEmpty()) 開方時間 = DateTime.Now.ToDateTimeString_6();
                value_trading[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
                value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;
                收支原因 = $"{收支原因}";
                value_trading[(int)enum_交易記錄查詢資料.收支原因] = 收支原因;

                if ((動作.Contains("系統")))
                {
                    if (總異動量 == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (顏色 == Color.Black.ToColorString())
                        {
                            list_儲位刷新_藥品碼_buf = (from temp in list_儲位刷新_藥品碼
                                                 where temp == 藥品碼
                                                 select temp).ToList();
                            if (list_儲位刷新_藥品碼_buf.Count == 0)
                            {
                                list_儲位刷新_藥品碼.Add(藥品碼);
                            }
                        }

                    }
                }
                list_交易紀錄新增資料_AddValue.Add(value_trading);
                Console.WriteLine($"寫入交易紀錄,藥碼 : {藥品碼} ,交易量 : {交易量}");
                if (flag_修正盤點量 && plC_CheckBox_盤點量要修正至結存量.Checked)
                {
                    int 結存量_temp = 結存量.StringToInt32();
                    int 盤點量_temp = 盤點量.StringToInt32();
                    交易量 = (盤點量_temp - 結存量_temp).ToString();
                    庫存量 = this.Function_從入賬資料取得庫存(藥品碼);
                    List<object[]> list_儲位資料 = Function_取得異動儲位資訊從入賬資料(藥品碼, 效期, 批號, 交易量.StringToInt32());
                    for (int k = 0; k < list_儲位資料.Count; k++)
                    {
                        Function_庫存異動至入賬資料(list_儲位資料[k], true);
                    }
                    transactionsClass transactionsClass = value_trading.SQLToClass<transactionsClass, enum_交易記錄查詢資料>();
                    transactionsClass.GUID = Guid.NewGuid().ToString();
                    transactionsClass.動作 = enum_交易記錄查詢動作.盤點調整.GetEnumName();
                    transactionsClass.庫存量 = 庫存量.ToString();
                    transactionsClass.交易量 = 交易量;
                    transactionsClass.盤點量 = "無";
                    transactionsClass.結存量 = 盤點量;
                    transactionsClass.備註 = "";
                    transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                    transactionsClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                    transactionsClass.收支原因 = "";
                    list_交易紀錄新增資料_AddValue.Add(transactionsClass.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>());

                }
                if (收支原因.Contains("覆盤錯誤"))
                {
                    string url = $"{API_Server}/api/MedRecheckLog/add";
                    returnData returnData = new returnData();
                    returnData.ServerName = ServerName;
                    returnData.ServerType = ServerType;
                    MedRecheckLogClass medRecheckLogClass = new MedRecheckLogClass();
                    medRecheckLogClass.藥碼 = 藥品碼;
                    medRecheckLogClass.藥名 = 藥品名稱;
                    medRecheckLogClass.系統理論值 = 庫存量.ToString();
                    medRecheckLogClass.覆盤理論值 = 盤點量;
                    medRecheckLogClass.操作人 = 操作人;
                    medRecheckLogClass.交易紀錄_GUID = 交易紀錄_GUID;
                    for (int m = 0; m < List_效期.Count; m++)
                    {
                        medRecheckLogClass.效期 += List_效期[m];
                        if (m != List_效期.Count - 1) medRecheckLogClass.效期 += ",";
                    }
                    for (int m = 0; m < List_批號.Count; m++)
                    {
                        medRecheckLogClass.批號 += List_批號[m];
                        if (m != List_批號.Count - 1) medRecheckLogClass.批號 += ",";
                    }
                    returnData.Data = medRecheckLogClass;
                    string json_in = returnData.JsonSerializationt(true);
                    string json_result = Net.WEBApiPostJson(url, json_in);
                }
            }
            for (int i = 0; i < list_取藥堆疊母資料_ReplaceValue.Count; i++)
            {
                string Order_GUID = list_取藥堆疊母資料_ReplaceValue[i][(int)enum_取藥堆疊母資料.藥袋序號].ObjectToString();
                操作人 = list_取藥堆疊母資料_ReplaceValue[i][(int)enum_取藥堆疊母資料.操作人].ObjectToString();
                List<object[]> list_value = this.sqL_DataGridView_醫令資料.SQL_GetRows((int)enum_醫囑資料.PRI_KEY, Order_GUID, false);
                if (list_value.Count == 0) continue;
                for (int m = 0; m < list_value.Count; m++)
                {
                    if (list_value[m][(int)enum_醫囑資料.狀態].ObjectToString() == enum_醫囑資料_狀態.已過帳.GetEnumName()) continue;
                    list_value[m][(int)enum_醫囑資料.狀態] = enum_醫囑資料_狀態.已過帳.GetEnumName();
                    list_value[m][(int)enum_醫囑資料.過帳時間] = DateTime.Now.ToDateTimeString_6();
                    list_value[m][(int)enum_醫囑資料.備註] = $"調劑人[{操作人}]";
                    list_醫囑資料_ReplaceValue.Add(list_value[m]);
                }
                //List<OrderClass> orderClasses = list_value.SQLToClass<OrderClass, enum_醫囑資料>();
                //Console.WriteLine($"{orderClasses.JsonSerializationt()}");
            }
            for (int i = 0; i < list_儲位刷新_藥品碼.Count; i++)
            {
                Function_儲位刷新(list_儲位刷新_藥品碼[i]);
            }

            if (list_交易紀錄新增資料_AddValue.Count > 0) this.sqL_DataGridView_交易記錄查詢.SQL_AddRows(list_交易紀錄新增資料_AddValue, false);
            if (list_取藥堆疊子資料_ReplaceValue.Count > 0) this.sqL_DataGridView_取藥堆疊子資料.SQL_ReplaceExtra(list_取藥堆疊子資料_ReplaceValue, false);
            if (list_取藥堆疊母資料_ReplaceValue.Count > 0) this.sqL_DataGridView_取藥堆疊母資料.SQL_ReplaceExtra(list_取藥堆疊母資料_ReplaceValue, false);
            if (list_醫囑資料_ReplaceValue.Count > 0) this.sqL_DataGridView_醫令資料.SQL_ReplaceExtra(list_醫囑資料_ReplaceValue, false);
            cnt++;
        }
        void cnt_Program_取藥堆疊資料_入賬檢查_等待延遲(ref int cnt)
        {
            cnt++;
        }




        #endregion

    }
}
