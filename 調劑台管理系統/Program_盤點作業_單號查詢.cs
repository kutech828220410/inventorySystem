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
        private enum enum_盤點作業_單號查詢_盤點藥品內容
        {
            GUID,
            Master_GUID,
            藥品碼,
            藥品名稱,
            中文名稱,
            單位,
            理論值,
            盤點量,

        }
        private enum enum_盤點作業_單號查詢_盤點藥品明細
        {
            GUID,
            Master_GUID,
            效期,
            批號,
            盤點量,
            盤點時間,
            操作人,
        }
        private void sub_Program_盤點作業_單號查詢_Init()
        {
            this.sqL_DataGridView_盤點作業_單號查詢_盤點藥品內容.Init();
            this.sqL_DataGridView_盤點作業_單號查詢_盤點藥品內容.RowEnterEvent += SqL_DataGridView_盤點作業_單號查詢_盤點藥品內容_RowEnterEvent;
            this.sqL_DataGridView_盤點作業_單號查詢_盤點藥品明細.Init();

            this.rJ_DatePicker_盤點作業_單號查詢_建表日期.ValueChanged += RJ_DatePicker_盤點作業_單號查詢_建表日期_ValueChanged;
            this.plC_RJ_Button_盤點作業_單號查詢_選擇.MouseDownEvent += PlC_RJ_Button_盤點作業_單號查詢_選擇_MouseDownEvent;
            this.plC_RJ_Button__盤點作業_單號查詢_刪除單號.MouseDownEvent += PlC_RJ_Button__盤點作業_單號查詢_刪除單號_MouseDownEvent;
            this.plC_RJ_Button__盤點作業_單號查詢_刪除盤點藥品內容.MouseDownEvent += PlC_RJ_Button__盤點作業_單號查詢_刪除盤點藥品內容_MouseDownEvent;
            this.plC_RJ_Button__盤點作業_單號查詢_新增藥品盤點明細.MouseDownEvent += PlC_RJ_Button__盤點作業_單號查詢_新增藥品盤點明細_MouseDownEvent;
            this.plC_RJ_Button__盤點作業_單號查詢_刪除藥品盤點明細.MouseDownEvent += PlC_RJ_Button__盤點作業_單號查詢_刪除藥品盤點明細_MouseDownEvent;
            this.plC_RJ_Button_盤點作業_單號查詢_下載.MouseClickEvent += PlC_RJ_Button_盤點作業_單號查詢_下載_MouseClickEvent;

            this.plC_UI_Init.Add_Method(sub_Program_盤點作業_單號查詢);
        }



        private bool flag_Program_盤點作業_單號查詢_Init = false;
        private void sub_Program_盤點作業_單號查詢()
        {
            if (this.plC_ScreenPage_Main.PageText == "盤點作業" && this.plC_ScreenPage_盤點作業.PageText == "單號查詢")
            {
                if (!flag_Program_盤點作業_單號查詢_Init)
                {
                    this.Function_盤點作業_單號查詢_取得單號(DateTime.Now.ToDateString());
                    flag_Program_盤點作業_單號查詢_Init = true;
                }
            }
            else
            {
                flag_Program_盤點作業_單號查詢_Init = false;
            }
        }

        #region Function
        private void Function_盤點作業_單號查詢_清除顯示UI()
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點作業_單號查詢_盤點單號.Text = "";
                rJ_TextBox_盤點作業_單號查詢_盤點名稱.Text = "";
                rJ_TextBox_盤點作業_單號查詢_建表人.Text = "";
                rJ_TextBox_盤點作業_單號查詢_建表時間.Text = "";
                rJ_TextBox_盤點作業_單號查詢_盤點開始時間.Text = "";
                rJ_TextBox_盤點作業_單號查詢_盤點結束時間.Text = "";
                rJ_TextBox_盤點作業_單號查詢_盤點狀態.Text = "";
            }));
            sqL_DataGridView_盤點作業_單號查詢_盤點藥品內容.ClearGrid();
            sqL_DataGridView_盤點作業_單號查詢_盤點藥品明細.ClearGrid();
        }
        private void Function_盤點作業_單號查詢_顯示UI(inventoryClass.creat creat)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點作業_單號查詢_盤點單號.Text = creat.盤點單號;
                rJ_TextBox_盤點作業_單號查詢_盤點名稱.Text = creat.盤點名稱;
                rJ_TextBox_盤點作業_單號查詢_建表人.Text = creat.建表人;
                rJ_TextBox_盤點作業_單號查詢_建表時間.Text = creat.建表時間;
                rJ_TextBox_盤點作業_單號查詢_盤點開始時間.Text = creat.盤點開始時間;
                rJ_TextBox_盤點作業_單號查詢_盤點結束時間.Text = creat.盤點結束時間;
                rJ_TextBox_盤點作業_單號查詢_盤點狀態.Text = creat.盤點狀態;
            }));
            List<object[]> list_盤點藥品內容 = new List<object[]>();
            for (int i = 0; i < creat.Contents.Count; i++)
            {
                object[] value = new object[new enum_盤點作業_單號查詢_盤點藥品內容().GetLength()];
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.GUID] = creat.Contents[i].GUID;
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.Master_GUID] = creat.Contents[i].Master_GUID;
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.藥品碼] = creat.Contents[i].藥品碼;
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.藥品名稱] = creat.Contents[i].藥品名稱;
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.中文名稱] = creat.Contents[i].中文名稱;
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.單位] = creat.Contents[i].包裝單位;
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.理論值] = creat.Contents[i].理論值;
                value[(int)enum_盤點作業_單號查詢_盤點藥品內容.盤點量] = creat.Contents[i].盤點量;
                list_盤點藥品內容.Add(value);
            }
            sqL_DataGridView_盤點作業_單號查詢_盤點藥品內容.RefreshGrid(list_盤點藥品內容);
        }
        private void Function_盤點作業_單號查詢_取得單號(string date)
        {
            List<string> str_ary = new List<string>();
            returnData returnData = new returnData();
            inventoryClass.creat creat = new inventoryClass.creat();
            creat.建表時間 = date;


            returnData.Data = creat;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/creat_get_by_CT_TIME", json_in);
            returnData = json.JsonDeserializet<returnData>();
            List<inventoryClass.creat> creats = returnData.Data.ObjToListClass<inventoryClass.creat>();
            for (int i = 0; i < creats.Count; i++)
            {
                str_ary.Add(creats[i].盤點單號);
            }
            this.Invoke(new Action(delegate
            {
                comboBoxr_盤點作業_單號查詢_盤點單號.Items.Clear();
                for (int i = 0; i < str_ary.Count; i++)
                {
                    comboBoxr_盤點作業_單號查詢_盤點單號.Items.Add(str_ary[i]);
                }
                if (comboBoxr_盤點作業_單號查詢_盤點單號.Items.Count > 0) comboBoxr_盤點作業_單號查詢_盤點單號.SelectedIndex = 0;
            }));

        }
        private void Function_盤點作業_單號查詢_取得盤點明細(string Content_GUID)
        {
            if (Content_GUID.StringIsEmpty()) return;
            returnData returnData = new returnData();
            inventoryClass.content content = new inventoryClass.content();
            content.GUID = Content_GUID;

            returnData.Data = content;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt(true);
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/sub_content_get_by_content_GUID", json_in);
            returnData = json.JsonDeserializet<returnData>();
            List<inventoryClass.sub_content> sub_contents = returnData.Data.ObjToListClass<inventoryClass.sub_content>();
            List<object[]> list_盤點藥品明細 = new List<object[]>();
            for (int i = 0; i < sub_contents.Count; i++)
            {
                inventoryClass.sub_content sub_Content = sub_contents[i];
                object[] value = new object[new enum_盤點作業_單號查詢_盤點藥品明細().GetLength()];
                value[(int)enum_盤點作業_單號查詢_盤點藥品明細.GUID] = sub_Content.GUID;
                value[(int)enum_盤點作業_單號查詢_盤點藥品明細.Master_GUID] = sub_Content.Master_GUID;
                value[(int)enum_盤點作業_單號查詢_盤點藥品明細.效期] = sub_Content.效期;
                string temp0 = sub_Content.效期.StringToDateTime().ToDateString();
                string temp1 = DateTime.MaxValue.ToDateString();
                if (sub_Content.效期.StringToDateTime().ToDateString() == DateTime.MaxValue.ToDateString())
                {
                    value[(int)enum_盤點作業_單號查詢_盤點藥品明細.效期] = "無";
                }
                value[(int)enum_盤點作業_單號查詢_盤點藥品明細.批號] = sub_Content.批號;
                if(sub_Content.批號.StringIsEmpty())
                {
                    value[(int)enum_盤點作業_單號查詢_盤點藥品明細.批號] = "無";
                }
                value[(int)enum_盤點作業_單號查詢_盤點藥品明細.盤點量] = sub_Content.盤點量;
                value[(int)enum_盤點作業_單號查詢_盤點藥品明細.操作人] = sub_Content.操作人;
                value[(int)enum_盤點作業_單號查詢_盤點藥品明細.盤點時間] = sub_Content.操作時間;

                list_盤點藥品明細.Add(value);
            }
            sqL_DataGridView_盤點作業_單號查詢_盤點藥品明細.RefreshGrid(list_盤點藥品明細);
        }
        private void Function_盤點作業_單號查詢_刪除盤點單號(string IC_SN)
        {
            List<string> str_ary = new List<string>();
            returnData returnData = new returnData();
            inventoryClass.creat creat = new inventoryClass.creat();
            creat.盤點單號 = IC_SN;

            returnData.Data = creat;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/creat_delete_by_IC_SN", json_in);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog(returnData.Result);
            Function_盤點作業_單號查詢_清除顯示UI();
        }
        private void Function_盤點作業_單號查詢_選擇盤點單號(string IC_SN)
        {
            returnData returnData = new returnData();
            inventoryClass.creat creat = new inventoryClass.creat();
            creat.盤點單號 = IC_SN;

            returnData.Data = creat;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/creat_get_by_IC_SN", json_in);
            returnData = json.JsonDeserializet<returnData>();
            List<inventoryClass.creat> creats = returnData.Data.ObjToListClass<inventoryClass.creat>();
            if (returnData.Code < 0 || creats.Count == 0)
            {
                MyMessageBox.ShowDialog(returnData.Result);
                return;
            }
            Function_盤點作業_單號查詢_顯示UI(creats[0]);
        }
        #endregion
        #region Event
        private void SqL_DataGridView_盤點作業_單號查詢_盤點藥品內容_RowEnterEvent(object[] RowValue)
        {
            string GUID = RowValue[(int)enum_盤點作業_單號查詢_盤點藥品內容.GUID].ObjectToString();
            Function_盤點作業_單號查詢_取得盤點明細(GUID);
        }
        private void PlC_RJ_Button_盤點作業_單號查詢_選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            string 盤點單號 = "";
            this.Invoke(new Action(delegate
            {
                盤點單號 = comboBoxr_盤點作業_單號查詢_盤點單號.Text;
            }));
            Function_盤點作業_單號查詢_選擇盤點單號(盤點單號);
        }
        private void RJ_DatePicker_盤點作業_單號查詢_建表日期_ValueChanged(object sender, EventArgs e)
        {
            Function_盤點作業_單號查詢_取得單號(this.rJ_DatePicker_盤點作業_單號查詢_建表日期.Value.ToDateString());
        }
        private void PlC_RJ_Button__盤點作業_單號查詢_刪除單號_MouseDownEvent(MouseEventArgs mevent)
        {
            string 盤點單號 = rJ_TextBox_盤點作業_單號查詢_盤點單號.Text;
            if (MyMessageBox.ShowDialog($"是否刪除[{盤點單號}]所有資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            Function_盤點作業_單號查詢_刪除盤點單號(盤點單號);
            Function_盤點作業_單號查詢_取得單號(DateTime.Now.ToDateString());
        }
        private void PlC_RJ_Button__盤點作業_單號查詢_刪除盤點藥品內容_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_盤點作業_單號查詢_盤點藥品內容.Get_All_Checked_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            if (MyMessageBox.ShowDialog($"是否刪除選取<{list_value.Count}>筆資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            returnData returnData = new returnData();
            List<inventoryClass.content> contents = new List<inventoryClass.content>();
            for (int i = 0; i < list_value.Count; i++)
            {
                inventoryClass.content content = new inventoryClass.content();
                content.GUID = list_value[i][(int)enum_盤點作業_單號查詢_盤點藥品內容.GUID].ObjectToString();
                contents.Add(content);
            }
            returnData.Data = contents;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt(true);
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/contents_delete_by_GUID", json_in);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog(returnData.Result);

            Function_盤點作業_單號查詢_選擇盤點單號(rJ_TextBox_盤點作業_單號查詢_盤點單號.Text);
            sqL_DataGridView_盤點作業_單號查詢_盤點藥品明細.ClearGrid();
        }
        private void PlC_RJ_Button__盤點作業_單號查詢_新增藥品盤點明細_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_盤點藥品內容 = sqL_DataGridView_盤點作業_單號查詢_盤點藥品內容.Get_All_Select_RowsValues();
            if (list_盤點藥品內容.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
            }
            string Master_GUID = list_盤點藥品內容[0][(int)enum_盤點作業_單號查詢_盤點藥品內容.GUID].ObjectToString();

            string 效期 = "";
            string 批號 = "";
            string 數量 = "";
            if(checkBox_盤點作業_單號查詢_盤點藥品明細_輸入效期批號.Checked)
            {
                Dialog_輸入效期 dialog_輸入效期 = new Dialog_輸入效期();
                if (dialog_輸入效期.ShowDialog() != DialogResult.Yes) return;
                效期 = dialog_輸入效期.Value;
            }
            if (checkBox_盤點作業_單號查詢_盤點藥品明細_輸入效期批號.Checked)
            {
                Dialog_輸入批號 dialog_輸入批號 = new Dialog_輸入批號();
                if (dialog_輸入批號.ShowDialog() != DialogResult.Yes) return;
                批號 = dialog_輸入批號.Value;
            }

            if (!checkBox_盤點作業_單號查詢_盤點藥品明細_輸入效期批號.Checked)
            {
                效期 = DateTime.MaxValue.ToDateString();
            }

            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("輸入盤點量");
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;

           

            數量 = dialog_NumPannel.Value.ToString();
            returnData returnData = new returnData();
            inventoryClass.sub_content sub_Content = new inventoryClass.sub_content();
            sub_Content.Master_GUID = Master_GUID;
            sub_Content.效期 = 效期;
            sub_Content.批號 = 批號;
            sub_Content.盤點量 = 數量;
            sub_Content.操作人 = 登入者名稱;
            returnData.Data = sub_Content;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt(true);
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/sub_content_add", json_in);
            returnData = json.JsonDeserializet<returnData>();
            if(returnData.Code < 0) MyMessageBox.ShowDialog(returnData.Result);
            Function_盤點作業_單號查詢_選擇盤點單號(rJ_TextBox_盤點作業_單號查詢_盤點單號.Text);
            Function_盤點作業_單號查詢_取得盤點明細(Master_GUID);

        }
        private void PlC_RJ_Button__盤點作業_單號查詢_刪除藥品盤點明細_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_盤點作業_單號查詢_盤點藥品明細.Get_All_Checked_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            if (MyMessageBox.ShowDialog($"是否刪除選取<{list_value.Count}>筆資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            returnData returnData = new returnData();
            List<inventoryClass.sub_content> sub_contents = new List<inventoryClass.sub_content>();
            string Master_GUID = "";
            for (int i = 0; i < list_value.Count; i++)
            {
                inventoryClass.sub_content sub_Content = new inventoryClass.sub_content();
                sub_Content.GUID = list_value[i][(int)enum_盤點作業_單號查詢_盤點藥品明細.GUID].ObjectToString();
                sub_contents.Add(sub_Content);
                Master_GUID = list_value[i][(int)enum_盤點作業_單號查詢_盤點藥品明細.Master_GUID].ObjectToString();
            }
            returnData.Data = sub_contents;
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt(true);
            string json = Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/inventory/sub_contents_delete_by_GUID", json_in);
            returnData = json.JsonDeserializet<returnData>();
            MyMessageBox.ShowDialog(returnData.Result);
            Function_盤點作業_單號查詢_選擇盤點單號(rJ_TextBox_盤點作業_單號查詢_盤點單號.Text);
            Function_盤點作業_單號查詢_取得盤點明細(Master_GUID);
        }
        private void PlC_RJ_Button_盤點作業_單號查詢_下載_MouseClickEvent(MouseEventArgs mevent)
        {
            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            inventoryClass.creat creat = new inventoryClass.creat();
            this.Invoke(new Action(delegate
            {
                creat.盤點單號 = comboBoxr_盤點作業_單號查詢_盤點單號.Text;
            }));
            returnData.Data = creat;
            string json_in = returnData.JsonSerializationt(true);
            try
            {
                byte[] excelData = Net.WEBApiPostJsonBytes($"{dBConfigClass.Api_URL}/api/inventory/download_excel_by_IC_SN", json_in);

                this.Invoke(new Action(delegate
                {
                    if (this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
                    {
                        excelData.SaveFileStream(saveFileDialog_SaveExcel.FileName);

                        MyMessageBox.ShowDialog("Excel 文件下载成功！");
                    }
                }));
            }
            catch
            {
                MyMessageBox.ShowDialog("Excel 文件下载失敗！");
            }
          
        }
        #endregion
      
        
    }
}
