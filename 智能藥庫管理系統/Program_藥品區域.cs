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
using H_Pannel_lib;

namespace 智能藥庫系統
{
    public partial class Main_Form : Form
    {
        public static SQL_DataGridView _sqL_DataGridView_藥品區域 = null;
        private MyThread MyThread_藥品區域亮燈 = null;
        private void Program_藥品區域_Init()
        {
            Table table_drugStotreArea = drugStotreArea.init(Main_Form.API_Server);
            this.sqL_DataGridView_藥品區域.RowsHeight = 40;
            this.sqL_DataGridView_藥品區域.InitEx(table_drugStotreArea);
            this.sqL_DataGridView_藥品區域.Set_ColumnVisible(false, new enum_drugStotreArea().GetEnumNames());
            this.sqL_DataGridView_藥品區域.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_drugStotreArea.IP);
            this.sqL_DataGridView_藥品區域.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_drugStotreArea.Num);
            this.sqL_DataGridView_藥品區域.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_drugStotreArea.Port);
            this.sqL_DataGridView_藥品區域.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_drugStotreArea.名稱);
            this.sqL_DataGridView_藥品區域.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_drugStotreArea.序號);
            this.sqL_DataGridView_藥品區域.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_drugStotreArea.狀態);
            this.sqL_DataGridView_藥品區域.DataGridRowsChangeRefEvent += SqL_DataGridView_藥品區域_DataGridRowsChangeRefEvent;
            _sqL_DataGridView_藥品區域 = this.sqL_DataGridView_藥品區域;


            this.rJ_Button_藥品區域_新增.MouseDownEvent += RJ_Button_藥品區域_新增_MouseDownEvent;
            this.rJ_Button_藥品區域_刪除.MouseDownEvent += RJ_Button_藥品區域_刪除_MouseDownEvent;
            this.rJ_Button_藥品區域_更新.MouseDownEvent += RJ_Button_藥品區域_更新_MouseDownEvent;

            if (PLC_Device_主機模式.Bool)
            {
                MyThread_藥品區域亮燈 = new MyThread();
                MyThread_藥品區域亮燈.SetSleepTime(10);
                MyThread_藥品區域亮燈.Add_Method(Program_藥品區域);
                MyThread_藥品區域亮燈.AutoRun(true);
                MyThread_藥品區域亮燈.Trigger();
            }

            //plC_UI_Init.Add_Method(Program_藥品區域);
        }

        public static List<string> Function_取得藥品區域名稱()
        {
            List<object[]> list_value = _sqL_DataGridView_藥品區域.SQL_GetAllRows(false);
            List<string> strs = (from temp in list_value
                                 select temp[(int)enum_drugStotreArea.名稱].ObjectToString()).ToList();
            return strs;
        }

        private void SqL_DataGridView_藥品區域_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_藥品區域());
        }
        private void RJ_Button_藥品區域_更新_MouseDownEvent(MouseEventArgs mevent)
        {
            this.sqL_DataGridView_藥品區域.SQL_GetAllRows(true);
        }
        private void RJ_Button_藥品區域_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品區域.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料");
                return;
            }

            this.sqL_DataGridView_藥品區域.SQL_DeleteExtra(list_value, false);
            this.sqL_DataGridView_藥品區域.SQL_GetAllRows(true);
        }
        private void RJ_Button_藥品區域_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_藥品區域_IP.Texts.Check_IP_Adress() == false)
            {
                MyMessageBox.ShowDialog("IP 參數錯誤");
                return;
            }
            if (rJ_TextBox_藥品區域_Port.Texts.StringIsInt32() == false)
            {
                MyMessageBox.ShowDialog("Port 參數錯誤");
                return;
            }
            if (rJ_TextBox_藥品區域_PIN_Num.Texts.StringIsInt32() == false)
            {
                MyMessageBox.ShowDialog("PIN Num 參數錯誤");
                return;
            }
            if (rJ_TextBox_藥品區域_名稱.Texts.StringIsEmpty() == true)
            {
                MyMessageBox.ShowDialog("名稱空白");
                return;
            }
            drugStotreArea drugStotreArea = new drugStotreArea();
            List<object[]> list_value = this.sqL_DataGridView_藥品區域.SQL_GetAllRows(false);
            List<object[]> list_value_buf = new List<object[]>();
            list_value_buf = list_value.GetRows((int)enum_drugStotreArea.IP, rJ_TextBox_藥品區域_IP.Texts);
            if(list_value_buf.Count == 0)
            {
                object[] value = new object[new enum_drugStotreArea().GetLength()];
                value[(int)enum_drugStotreArea.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_drugStotreArea.IP] = rJ_TextBox_藥品區域_IP.Texts;
                value[(int)enum_drugStotreArea.Port] = rJ_TextBox_藥品區域_Port.Texts;
                value[(int)enum_drugStotreArea.Num] = rJ_TextBox_藥品區域_PIN_Num.Texts;
                value[(int)enum_drugStotreArea.名稱] = rJ_TextBox_藥品區域_名稱.Texts;
                value[(int)enum_drugStotreArea.序號] = list_value.Count + 1;
                this.sqL_DataGridView_藥品區域.SQL_AddRow(value, false);
                this.sqL_DataGridView_藥品區域.SQL_GetAllRows(true);
            }
            else
            {
                object[] value = list_value_buf[0];
                value[(int)enum_drugStotreArea.IP] = rJ_TextBox_藥品區域_IP.Texts;
                value[(int)enum_drugStotreArea.Port] = rJ_TextBox_藥品區域_Port.Texts;
                value[(int)enum_drugStotreArea.Num] = rJ_TextBox_藥品區域_PIN_Num.Texts;
                value[(int)enum_drugStotreArea.名稱] = rJ_TextBox_藥品區域_名稱.Texts;
                value[(int)enum_drugStotreArea.序號] = list_value.Count + 1;
                this.sqL_DataGridView_藥品區域.SQL_ReplaceExtra(value, false);
                this.sqL_DataGridView_藥品區域.SQL_GetAllRows(true);
            }
        }

        private void Program_藥品區域()
        {
            List<Storage> storages_EPD266 = List_EPD266_雲端資料;
            List<Storage> storages_EPD266_buf = new List<Storage>();

            Dictionary<string, List<Storage>> keyValuePairs_storage =  storages_EPD266.CoverToDictionaryByIP();

            List<StorageUI_EPD_266.UDP_READ> uDP_READs_EPD266 = this.storageUI_EPD_266.GerAllUDP_READ();
            List<StorageUI_EPD_266.UDP_READ> uDP_READs_EPD266_buf = new List<StorageUI_EPD_266.UDP_READ>();
            for (int i = 0; i < uDP_READs_EPD266.Count; i++)
            {
                storages_EPD266_buf = keyValuePairs_storage.SortDictionaryByIP(uDP_READs_EPD266[i].IP);
                if(storages_EPD266_buf.Count > 0)
                {
                    storages_EPD266_buf[0].IsLightOn = uDP_READs_EPD266[i].WS2812_State;
                }
              
            }
            List<object[]> list_藥品區域 = _sqL_DataGridView_藥品區域.SQL_GetAllRows(false);
            
            for (int i = 0; i < list_藥品區域.Count; i++)
            {
                string 名稱 = list_藥品區域[i][(int)enum_drugStotreArea.名稱].ObjectToString();
                string IP = list_藥品區域[i][(int)enum_drugStotreArea.IP].ObjectToString();
                string Port = list_藥品區域[i][(int)enum_drugStotreArea.Port].ObjectToString();
                string Num = list_藥品區域[i][(int)enum_drugStotreArea.Num].ObjectToString();
                storages_EPD266_buf = (from temp in storages_EPD266
                                       where temp.Area == 名稱 && temp.IsLightOn == true
                                       select temp).ToList();

                int port = Port.StringToInt32();
                int num = Num.StringToInt32();

                if (IP.Check_IP_Adress())
                {
                    if (storages_EPD266_buf.Count > 0)
                    {
                        if (!this.rfiD_UI.Get_IO_Output(IP, port, num)) this.rfiD_UI.Set_OutputPIN(IP, port, num, true);
                    }
                    else
                    {
                        if (this.rfiD_UI.Get_IO_Output(IP, port, num)) this.rfiD_UI.Set_OutputPIN(IP, port, num, false);
                    }
                }             
            }
        }

        public class ICP_藥品區域 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {  
                int 序號1 = x[(int)enum_drugStotreArea.序號].StringToInt32();
                int 序號2 = y[(int)enum_drugStotreArea.序號].StringToInt32();
                
                return 序號1.CompareTo(序號2);
            }
        }
    }
}
