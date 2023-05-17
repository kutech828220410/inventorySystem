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
    public enum enum_盤點作業_新增盤點_盤點單號
    {
        GUID,
        盤點單號,
        建表人,
        建表時間,
        盤點開始時間,
        盤點結束時間,
        盤點狀態
    }

    public partial class Form1 : Form
    {
        private void sub_Program_盤點作業_新增盤點_Init()
        {

            this.sqL_DataGridView_盤點作業_新增盤點_盤點單號.Init();
            this.plC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示.MouseDownEvent += PlC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_新增盤點_盤點單號_新建盤點表.MouseDownEvent += PlC_RJ_Button_盤點作業_新增盤點_盤點單號_新建盤點表_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_新增盤點_盤點單號_刪除資料.MouseDownEvent += PlC_RJ_Button_盤點作業_新增盤點_盤點單號_刪除資料_MouseDownEvent;
            this.plC_UI_Init.Add_Method(sub_Program_盤點作業_新增盤點);
        }

     

        private bool flag_Program_盤點作業_新增盤點_Init = false;
        private void sub_Program_盤點作業_新增盤點()
        {
            if (this.plC_ScreenPage_Main.PageText == "盤點作業" && this.plC_ScreenPage_盤點作業.PageText == "新增盤點")
            {
                if (!flag_Program_盤點作業_新增盤點_Init)
                {
                    flag_Program_盤點作業_新增盤點_Init = true;
                }
            }
            else
            {
                flag_Program_盤點作業_新增盤點_Init = false;
            }
        }

        #region Function

        #endregion
        #region Event
        private void PlC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = new List<object[]>();
            string json = Basic.Net.WEBApiGet(dBConfigClass.Inventory_get_creat_ApiURL);
            returnData returnData = json.JsonDeserializet<returnData>();
            List<inventoryClass.creat_OUT> inventory_Creat_OUTs = new List<inventoryClass.creat_OUT>();
            for (int i = 0; i < returnData.Data.Count; i++)
            {
                inventoryClass.creat_OUT inventory_creat_OUT = inventoryClass.creat_OUT.ObjToClass(returnData.Data[i]);
                inventory_Creat_OUTs.Add(inventory_creat_OUT);
            }
            for (int i = 0; i < inventory_Creat_OUTs.Count; i++)
            {
                object[] value = new object[new enum_盤點作業_新增盤點_盤點單號().GetLength()];
                value[(int)enum_盤點作業_新增盤點_盤點單號.GUID] = inventory_Creat_OUTs[i].GUID;
                value[(int)enum_盤點作業_新增盤點_盤點單號.盤點單號] = inventory_Creat_OUTs[i].盤點單號;
                value[(int)enum_盤點作業_新增盤點_盤點單號.建表人] = inventory_Creat_OUTs[i].建表人;
                value[(int)enum_盤點作業_新增盤點_盤點單號.建表時間] = inventory_Creat_OUTs[i].建表時間;
                value[(int)enum_盤點作業_新增盤點_盤點單號.盤點開始時間] = inventory_Creat_OUTs[i].盤點開始時間;
                value[(int)enum_盤點作業_新增盤點_盤點單號.盤點結束時間] = inventory_Creat_OUTs[i].盤點結束時間;
                value[(int)enum_盤點作業_新增盤點_盤點單號.盤點狀態] = inventory_Creat_OUTs[i].盤點狀態;
                list_value.Add(value);
            }
            this.sqL_DataGridView_盤點作業_新增盤點_盤點單號.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_盤點作業_新增盤點_盤點單號_新建盤點表_MouseDownEvent(MouseEventArgs mevent)
        {
            returnData InData = new returnData();
            inventoryClass.creat_OUT creat_OUT = new inventoryClass.creat_OUT();
            creat_OUT.建表人 = 登入者名稱;
            InData.Data.Add(creat_OUT);
            string json = Basic.Net.WEBApiPostJson(dBConfigClass.Inventory_post_creat_ApiURL, InData.JsonSerializationt());
            returnData returnData = json.JsonDeserializet<returnData>();
            if(returnData == null)
            {
                MyMessageBox.ShowDialog("回傳格式錯誤!");
                return;
            }
            MyMessageBox.ShowDialog(returnData.Result);
            PlC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示_MouseDownEvent(null);
        }
        private void PlC_RJ_Button_盤點作業_新增盤點_盤點單號_刪除資料_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_盤點作業_新增盤點_盤點單號.Get_All_Checked_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            if (MyMessageBox.ShowDialog($"是否刪除選取{list_value.Count}筆資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            returnData InData = new returnData();
            for (int i = 0; i < list_value.Count; i++)
            {
                inventoryClass.creat_OUT creat_OUT = new inventoryClass.creat_OUT();
                creat_OUT.GUID = list_value[i][(int)enum_盤點作業_新增盤點_盤點單號.GUID].ObjectToString();
                InData.Data.Add(creat_OUT);
            }
            string json = Basic.Net.WEBApiPostJson(dBConfigClass.Inventory_post_delete_ApiURL, InData.JsonSerializationt());
            returnData returnData = json.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                MyMessageBox.ShowDialog("回傳格式錯誤!");
                return;
            }
            MyMessageBox.ShowDialog(returnData.Result);
            PlC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示_MouseDownEvent(null);
        }
        #endregion


    }
}
