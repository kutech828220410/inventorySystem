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
            this.plC_RJ_Button_盤點作業_新增盤點_盤點單號_取得API.MouseDownEvent += PlC_RJ_Button_盤點作業_新增盤點_盤點單號_取得API_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示.MouseDownEvent += PlC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示_MouseDownEvent;

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
        private void Function_盤點作業_取得盤點單號()
        {
          
            
        }
        #endregion
        #region Event
        private void PlC_RJ_Button_盤點作業_新增盤點_盤點單號_取得API_MouseDownEvent(MouseEventArgs mevent)
        {
            

        }
        private void PlC_RJ_Button_盤點作業_新增盤點_盤點單號_全部顯示_MouseDownEvent(MouseEventArgs mevent)
        {
            string json = Basic.Net.WEBApiGet(dBConfigClass.Inventory_get_creat_ApiURL);
            returnData returnData = json.JsonDeserializet<returnData>();
            List<inventoryClass.creat_OUT> inventory_Creat_OUTs = new List<inventoryClass.creat_OUT>();
            for (int i = 0; i < returnData.Data.Count; i++)
            {
                inventoryClass.creat_OUT inventory_creat_OUT = inventoryClass.creat_OUT.ObjToData(returnData.Data[i]);
                inventory_Creat_OUTs.Add(inventory_creat_OUT);
            }
        }
        #endregion


    }
}
