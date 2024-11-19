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
using System.Net;
using System.Net.Http;

namespace 調劑台管理系統
{

    public partial class Main_Form : Form
    {
        public enum 盤點作業_定盤
        {
            藥碼,
            藥名,
            單位,
            庫存量,
            盤點量,
            庫存差異量,
            異動量,
            異動後結存量,
            效期,
        }
        public enum 盤點作業_定盤_匯入
        {
            藥碼,
            藥名,
            單位,
            庫存量,
            盤點量,
            庫存差異量,
            異動後結存量,
        }
        private void sub_Program_盤點作業_定盤_Init()
        {
            this.sqL_DataGridView_盤點作業_定盤.Init();
            this.plC_RJ_Button_盤點作業_定盤_上傳Excel.MouseDownEvent += PlC_RJ_Button_盤點作業_定盤_上傳Excel_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_定盤_計算定盤結果.MouseDownEvent += PlC_RJ_Button_盤點作業_定盤_計算定盤結果_MouseDownEvent; 
            this.plC_RJ_Button_盤點作業_定盤_確認更動庫存值.MouseDownEvent += PlC_RJ_Button_盤點作業_定盤_確認更動庫存值_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_定盤_重置作業.MouseDownEvent += PlC_RJ_Button_盤點作業_定盤_重置作業_MouseDownEvent;
           
            PlC_RJ_Button_盤點作業_定盤_重置作業_MouseDownEvent(null);
            this.plC_UI_Init.Add_Method(sub_Program_盤點作業_定盤);
        }

    

        private bool flag_Program_盤點作業_定盤_Init = false;
        private void sub_Program_盤點作業_定盤()
        {
            if (this.plC_ScreenPage_Main.PageText == "盤點作業" && this.plC_ScreenPage_盤點作業.PageText == "定盤")
            {
                if (!flag_Program_盤點作業_定盤_Init)
                {
                    PlC_RJ_Button_盤點作業_定盤_重置作業_MouseDownEvent(null);
                    flag_Program_盤點作業_定盤_Init = true;
                }
            }
            else
            {
                flag_Program_盤點作業_定盤_Init = false;
            }
        }
        #region Function

        #endregion
        #region Event
        private void PlC_RJ_Button_盤點作業_定盤_重置作業_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.plC_RJ_Button_盤點作業_定盤_上傳Excel.Enabled = true;
                this.plC_RJ_Button_盤點作業_定盤_計算定盤結果.Enabled = false;
                this.plC_RJ_Button_盤點作業_定盤_確認更動庫存值.Enabled = false;
                this.sqL_DataGridView_盤點作業_定盤.ClearGrid();
            }));
         
        }
        private void PlC_RJ_Button_盤點作業_定盤_上傳Excel_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                DialogResult dialogResult = DialogResult.None;
                this.Invoke(new Action(delegate
                {
                    dialogResult = this.openFileDialog_LoadExcel.ShowDialog();
                }));
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                DataTable dataTable = MyOffice.ExcelClass.LoadFile(this.openFileDialog_LoadExcel.FileName);
                if (dataTable == null)
                {
                    MyMessageBox.ShowDialog("讀取失敗!");
                    return;
                }
                dataTable = dataTable.ReorderTable(new 盤點作業_定盤_匯入());
                if (dataTable == null)
                {
                    MyMessageBox.ShowDialog("讀取失敗!");
                    return;
                }
           
                List<object[]> list_value = dataTable.DataTableToRowList();
                Function_從SQL取得儲位到雲端資料();
                Dialog_Prcessbar dialog_Prcessbar = new Dialog_Prcessbar(list_value.Count);
                dialog_Prcessbar.State = "讀取Excel...";
                list_value = list_value.CopyRows(new 盤點作業_定盤_匯入(), new 盤點作業_定盤());
                for (int i = 0; i < list_value.Count; i++)
                {
                    dialog_Prcessbar.Value = i;
                    string 藥碼 = list_value[i][(int)盤點作業_定盤.藥碼].ObjectToString();
                    double 庫存量 = Function_從雲端資料取得庫存(藥碼);
                    double 盤點量 = list_value[i][(int)盤點作業_定盤.盤點量].ObjectToString().StringIsDouble() ? list_value[i][(int)盤點作業_定盤.盤點量].ObjectToString().StringToDouble() : 0;
                    double 庫存差異量 = list_value[i][(int)盤點作業_定盤.庫存差異量].ObjectToString().StringIsDouble() ? list_value[i][(int)盤點作業_定盤.庫存差異量].ObjectToString().StringToDouble() : 0;

                    if (list_value[i][(int)盤點作業_定盤.庫存差異量].ObjectToString().StringIsInt32() == false)
                    {
                        list_value[i][(int)盤點作業_定盤.庫存差異量] = "0";
                    }
                    if (庫存量 < 0) 庫存量 = 0;
                    double 異動量 = (盤點量 + 庫存差異量) - 庫存量;
                    list_value[i][(int)盤點作業_定盤.異動量] = 異動量;
                    list_value[i][(int)盤點作業_定盤.庫存量] = 庫存量;
                    list_value[i][(int)盤點作業_定盤.異動後結存量] = 盤點量 + 庫存差異量;
                }
                this.sqL_DataGridView_盤點作業_定盤.RefreshGrid(list_value);
                this.Invoke(new Action(delegate
                {
                    this.plC_RJ_Button_盤點作業_定盤_計算定盤結果.Enabled = true;
                }));
                dialog_Prcessbar.Close();
                dialog_Prcessbar.Dispose();
            }));
           

        }
        private void PlC_RJ_Button_盤點作業_定盤_計算定盤結果_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                List<object[]> list_value = this.sqL_DataGridView_盤點作業_定盤.GetAllRows();

                Dialog_Prcessbar dialog_Prcessbar = new Dialog_Prcessbar(list_value.Count);
                Function_從SQL取得儲位到雲端資料();
                dialog_Prcessbar.State = "計算中....";
                for (int i = 0; i < list_value.Count; i++)
                {
                    dialog_Prcessbar.Value = i;
                    List<string> list_效期 = new List<string>();
                    List<string> list_批號 = new List<string>();
                    List<string> list_異動量 = new List<string>();
                    string 備註 = "";
                    string 藥碼 = list_value[i][(int)盤點作業_定盤.藥碼].ObjectToString();
                    double 庫存量 = Function_從雲端資料取得庫存(藥碼);
                    double 盤點量 = list_value[i][(int)盤點作業_定盤.盤點量].ObjectToString().StringIsDouble() ? list_value[i][(int)盤點作業_定盤.盤點量].ObjectToString().StringToDouble() : 0;
                    double 異動後結存量 = list_value[i][(int)盤點作業_定盤.異動後結存量].ObjectToString().StringIsDouble() ? list_value[i][(int)盤點作業_定盤.異動後結存量].ObjectToString().StringToDouble() : 0;
                    double 異動量 = 異動後結存量 - 庫存量;
                   
                    if (異動量 == 0)
                    {
                        continue;
                    }
                    if (庫存量 == 0 || 異動量 > 0)
                    {
                        List<string> 儲位_TYPE = new List<string>();
                        List<object> 儲位 = new List<object>();
                        this.Function_從雲端資料取得儲位(this.Function_藥品碼檢查(藥碼), ref 儲位_TYPE, ref 儲位);
                        Funnction_交易記錄查詢_取得指定藥碼批號期效期(藥碼, ref list_效期, ref list_批號);
                        if (list_效期.Count == 0)
                        {
                            list_效期.Add("2050/01/01");
                            list_批號.Add("自動補足");
                        }
                        備註 += $"[效期]:{list_效期[0]},[批號]:{list_批號[0]}";
                 
                        if (儲位.Count == 0)
                        {
                            list_value[i][(int)盤點作業_定盤.效期] = "無儲位";
                            continue;
                        }
                        object device = Function_庫存異動至雲端資料(儲位[0], 儲位_TYPE[0], list_效期[0], list_批號[0], 異動量.ToString(), false);
                    }
                    else
                    {
                        List<object[]> list_儲位資訊 = Function_取得異動儲位資訊從雲端資料(藥碼, 異動量);
                        if (list_儲位資訊.Count == 0)
                        {
                            list_value[i][(int)盤點作業_定盤.效期] = "無儲位";
                            continue;
                        }
                        for (int k = 0; k < list_儲位資訊.Count; k++)
                        {
                            string 效期 = list_儲位資訊[k][(int)enum_儲位資訊.效期].ObjectToString();
                            string 批號 = list_儲位資訊[k][(int)enum_儲位資訊.批號].ObjectToString();
                            備註 += $"[效期]:{效期},[批號]:{批號}";
                            if (k != list_儲位資訊.Count - 1) 備註 += "\n";
                        }

                    }
                    list_value[i][(int)盤點作業_定盤.效期] = 備註;

                }
                this.sqL_DataGridView_盤點作業_定盤.RefreshGrid(list_value);
                this.Invoke(new Action(delegate
                {
                    this.plC_RJ_Button_盤點作業_定盤_確認更動庫存值.Enabled = true;
                }));
                dialog_Prcessbar.Close();
                dialog_Prcessbar.Dispose();
            }));
          
        }
        private void PlC_RJ_Button_盤點作業_定盤_確認更動庫存值_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (MyMessageBox.ShowDialog("確認將盤點結果寫回庫存?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

                List<object[]> list_value = this.sqL_DataGridView_盤點作業_定盤.GetAllRows();
                List<object[]> list_交易紀錄_Add = new List<object[]>();
                Dialog_Prcessbar dialog_Prcessbar = new Dialog_Prcessbar(list_value.Count);
                Function_從SQL取得儲位到雲端資料();
                dialog_Prcessbar.State = "計算中....";
                for (int i = 0; i < list_value.Count; i++)
                {
                    dialog_Prcessbar.Value = i;
                    List<string> list_效期 = new List<string>();
                    List<string> list_批號 = new List<string>();
                    List<string> list_異動量 = new List<string>();
                    string 備註 = "";
                    string 藥碼 = list_value[i][(int)盤點作業_定盤.藥碼].ObjectToString();
                    string 藥名 = list_value[i][(int)盤點作業_定盤.藥名].ObjectToString();
                    double 庫存量 = list_value[i][(int)盤點作業_定盤.庫存量].ObjectToString().StringIsDouble() ? list_value[i][(int)盤點作業_定盤.庫存量].ObjectToString().StringToDouble() : 0;
                    double 盤點量 = list_value[i][(int)盤點作業_定盤.盤點量].ObjectToString().StringIsDouble() ? list_value[i][(int)盤點作業_定盤.盤點量].ObjectToString().StringToDouble() : 0;
                    double 異動後結存量 = list_value[i][(int)盤點作業_定盤.異動後結存量].ObjectToString().StringIsDouble() ? list_value[i][(int)盤點作業_定盤.異動後結存量].ObjectToString().StringToDouble() : 0;
                    double 異動量 = 異動後結存量 - 庫存量;

                    if (異動量 == 0)
                    {
                        continue;
                    }
                    if (庫存量 == 0 || 異動量 > 0)
                    {
                        List<string> 儲位_TYPE = new List<string>();
                        List<object> 儲位 = new List<object>();
                        this.Function_從雲端資料取得儲位(this.Function_藥品碼檢查(藥碼), ref 儲位_TYPE, ref 儲位);
                        Funnction_交易記錄查詢_取得指定藥碼批號期效期(藥碼, ref list_效期, ref list_批號);
                        if (list_效期.Count == 0)
                        {
                            list_效期.Add("2050/01/01");
                            list_批號.Add("自動補足");
                        }
                        備註 += $"[效期]:{list_效期[0]},[批號]:{list_批號[0]}";

                        if (儲位.Count == 0)
                        {
                            list_value[i][(int)盤點作業_定盤.效期] = "無儲位";
                            continue;
                        }
                        object device = Function_庫存異動至雲端資料(儲位[0], 儲位_TYPE[0], list_效期[0], list_批號[0], 異動量.ToString(), false);
                    }
                    else
                    {
                        List<object[]> list_儲位資訊 = Function_取得異動儲位資訊從雲端資料(藥碼, 異動量);
                        if (list_儲位資訊.Count == 0)
                        {
                            list_value[i][(int)盤點作業_定盤.效期] = "無儲位";
                            continue;
                        }
                        for (int k = 0; k < list_儲位資訊.Count; k++)
                        {
                            string 效期 = list_儲位資訊[k][(int)enum_儲位資訊.效期].ObjectToString();
                            string 批號 = list_儲位資訊[k][(int)enum_儲位資訊.批號].ObjectToString();
                            備註 += $"[效期]:{效期},[批號]:{批號}";
                            if (k != list_儲位資訊.Count - 1) 備註 += "\n";
                            Function_庫存異動至雲端資料(list_儲位資訊[k], false);
                        }

                    }
                    list_value[i][(int)盤點作業_定盤.效期] = 備註;

                    transactionsClass transactionsClass = new transactionsClass();
                    transactionsClass.GUID = Guid.NewGuid().ToString();
                    transactionsClass.動作 = enum_交易記錄查詢動作.盤存盈虧.GetEnumName();
                    transactionsClass.藥品碼 = 藥碼;
                    transactionsClass.藥品名稱 = 藥名;
                    transactionsClass.庫存量 = 庫存量.ToString();
                    transactionsClass.交易量 = 異動量.ToString();
                    transactionsClass.結存量 = 盤點量.ToString();
                    transactionsClass.操作人 = 登入者名稱;
                    transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                    transactionsClass.備註 = 備註;
                    object[] trading_value = transactionsClass.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();

                    list_交易紀錄_Add.Add(trading_value);

                }
                this.sqL_DataGridView_盤點作業_定盤.RefreshGrid(list_value);
                if (list_交易紀錄_Add.Count > 0) sqL_DataGridView_交易記錄查詢.SQL_AddRows(list_交易紀錄_Add, false);
                Function_雲端資料上傳至SQL();
                dialog_Prcessbar.Close();
                dialog_Prcessbar.Dispose();
                this.PlC_RJ_Button_盤點作業_定盤_重置作業_MouseDownEvent(null);
                MyMessageBox.ShowDialog("定盤庫存調整完成!");
            }));
        }
    
      
        #endregion



    }
}
