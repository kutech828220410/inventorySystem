using System;
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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using H_Pannel_lib;
namespace 勤務傳送櫃
{
    public partial class Form1 : Form
    {
        private void Program_櫃體狀態_Init()
        {
            this.plC_UI_Init.Add_Method(Program_櫃體狀態);
        }

        private void Program_櫃體狀態()
        {
            this.Program_櫃體狀態_更新病房資料();
            this.Program_櫃體狀態_讀取RFID();
        }

        #region PLC_櫃體狀態_更新病房資料
        PLC_Device PLC_Device_櫃體狀態_更新病房資料 = new PLC_Device("");
        MyTimer MyTimer_櫃體狀態_更新病房資料_更新間隔 = new MyTimer();
        int cnt_Program_櫃體狀態_更新病房資料 = 65534;
        void Program_櫃體狀態_更新病房資料()
        {
            if (this.plC_ScreenPage_Main.PageText == "櫃體狀態") PLC_Device_櫃體狀態_更新病房資料.Bool = true;
            if (cnt_Program_櫃體狀態_更新病房資料 == 65534)
            {
                PLC_Device_櫃體狀態_更新病房資料.SetComment("PLC_櫃體狀態_更新病房資料");
                PLC_Device_櫃體狀態_更新病房資料.Bool = false;
                cnt_Program_櫃體狀態_更新病房資料 = 65535;
            }
            if (cnt_Program_櫃體狀態_更新病房資料 == 65535) cnt_Program_櫃體狀態_更新病房資料 = 1;
            if (cnt_Program_櫃體狀態_更新病房資料 == 1) cnt_Program_櫃體狀態_更新病房資料_檢查按下(ref cnt_Program_櫃體狀態_更新病房資料);
            if (cnt_Program_櫃體狀態_更新病房資料 == 2) cnt_Program_櫃體狀態_更新病房資料_初始化(ref cnt_Program_櫃體狀態_更新病房資料);
            if (cnt_Program_櫃體狀態_更新病房資料 == 3) cnt_Program_櫃體狀態_更新病房資料_檢查時間到達(ref cnt_Program_櫃體狀態_更新病房資料);
            if (cnt_Program_櫃體狀態_更新病房資料 == 4) cnt_Program_櫃體狀態_更新病房資料_檢查需要寫入SQL(ref cnt_Program_櫃體狀態_更新病房資料);
            if (cnt_Program_櫃體狀態_更新病房資料 == 5) cnt_Program_櫃體狀態_更新病房資料_更新資料(ref cnt_Program_櫃體狀態_更新病房資料);
            if (cnt_Program_櫃體狀態_更新病房資料 == 6) cnt_Program_櫃體狀態_更新病房資料 = 65500;
            if (cnt_Program_櫃體狀態_更新病房資料 > 1) cnt_Program_櫃體狀態_更新病房資料_檢查放開(ref cnt_Program_櫃體狀態_更新病房資料);

            if (cnt_Program_櫃體狀態_更新病房資料 == 65500)
            {
                PLC_Device_櫃體狀態_更新病房資料.Bool = false;
                cnt_Program_櫃體狀態_更新病房資料 = 65535;
            }
        }
        void cnt_Program_櫃體狀態_更新病房資料_檢查按下(ref int cnt)
        {
            if (PLC_Device_櫃體狀態_更新病房資料.Bool) cnt++;
        }
        void cnt_Program_櫃體狀態_更新病房資料_檢查放開(ref int cnt)
        {
            if (!PLC_Device_櫃體狀態_更新病房資料.Bool) cnt = 65500;
        }
        void cnt_Program_櫃體狀態_更新病房資料_初始化(ref int cnt)
        {
            this.MyTimer_櫃體狀態_更新病房資料_更新間隔.StartTickTime(1000);
            cnt++;
        }
        void cnt_Program_櫃體狀態_更新病房資料_檢查時間到達(ref int cnt)
        {
            if(this.MyTimer_櫃體狀態_更新病房資料_更新間隔.IsTimeOut())
            {
                cnt++;
            } 
        }
        void cnt_Program_櫃體狀態_更新病房資料_檢查需要寫入SQL(ref int cnt)
        {
            for (int i = 0; i < this.List_Pannel_Box.Count; i++)
            {
                if(this.List_Pannel_Box[i].SQL_Write)
                {
                    int index = this.List_Pannel_Box[i].Number.StringToInt32() - 1;
              
                    this.List_Pannel_Box[i].SQL_Write = false;
                }
             
            }
            cnt++;
        }
        void cnt_Program_櫃體狀態_更新病房資料_更新資料(ref int cnt)
        {
            List<RFIDClass> rFIDClasses = this.rfiD_UI.SQL_GetAllRFIDClass();
            RFIDClass rFIDClass;
            List<object[]> list_Box_Index_Table = this.sqL_DataGridView_Box_Index_Table.SQL_GetAllRows(false);
            List<object[]> list_Box_Index_Table_buf = new List<object[]>();
            string IP = "";
            string EPD_IP = "";
            int Number = 0;
            int RFID_num = 0;
            int Lock_input_num = 0;
            int Lock_output_num = 0;
            int Led_output_num = 0;
            int Sensor_input_num = 0;

            for (int i = 0; i < this.List_Pannel_Box.Count; i++)
            {
                list_Box_Index_Table_buf = list_Box_Index_Table.GetRows((int)enum_Box_Index_Table.Number, (i).ToString());
                if (list_Box_Index_Table_buf.Count == 0) continue;
                IP = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.IP].ObjectToString();
                Number = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.Number].ObjectToString().StringToInt32();
                RFID_num = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.RFID_num].ObjectToString().StringToInt32();
                Lock_input_num = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.Lock_input_num].ObjectToString().StringToInt32();
                Lock_output_num = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.Lock_output_num].ObjectToString().StringToInt32();
                Led_output_num = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.Led_output_num].ObjectToString().StringToInt32();
                Sensor_input_num = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.Sensor_input_num].ObjectToString().StringToInt32();
                EPD_IP = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.EPD_IP].ObjectToString();
                if (RFID_num < 0 || RFID_num >= 5) continue;
                rFIDClass = rFIDClasses.SortByIP(IP);
                if (rFIDClass == null) continue;
                this.Invoke(new Action(delegate
                {
                    this.List_Pannel_Box[i].Number = i.ToString();
                    this.List_Pannel_Box[i].IP = rFIDClass.DeviceClasses[RFID_num].IP;
                    this.List_Pannel_Box[i].EPD_IP = EPD_IP;
                    this.List_Pannel_Box[i].Port = rFIDClass.DeviceClasses[RFID_num].Port;
                    this.List_Pannel_Box[i].WardName = rFIDClass.DeviceClasses[RFID_num].Name;
                    this.List_Pannel_Box[i].WardFont = rFIDClass.DeviceClasses[RFID_num].Name_font;
                    this.List_Pannel_Box[i].RFID_num = RFID_num;
                    this.List_Pannel_Box[i].Lock_input_num = Lock_input_num;
                    this.List_Pannel_Box[i].Lock_output_num = Lock_output_num;
                    this.List_Pannel_Box[i].Led_output_num = Led_output_num;
                    this.List_Pannel_Box[i].Sensor_input_num = Sensor_input_num;
                    this.List_Pannel_Box[i].MVisible = true;
                }));


            }
        
            cnt++;
        }





















        #endregion
        #region PLC_櫃體狀態_讀取RFID
        PLC_Device PLC_Device_櫃體狀態_讀取RFID = new PLC_Device("");
        int cnt_Program_櫃體狀態_讀取RFID = 65534;
        void Program_櫃體狀態_讀取RFID()
        {
            if (this.plC_ScreenPage_Main.PageText != "人員權限") PLC_Device_櫃體狀態_讀取RFID.Bool = true;
            else PLC_Device_櫃體狀態_讀取RFID.Bool = false;
            if (cnt_Program_櫃體狀態_讀取RFID == 65534)
            {
                PLC_Device_櫃體狀態_讀取RFID.SetComment("PLC_櫃體狀態_讀取RFID");
                PLC_Device_櫃體狀態_讀取RFID.Bool = false;
                cnt_Program_櫃體狀態_讀取RFID = 65535;
            }
            if (cnt_Program_櫃體狀態_讀取RFID == 65535) cnt_Program_櫃體狀態_讀取RFID = 1;
            if (cnt_Program_櫃體狀態_讀取RFID == 1) cnt_Program_櫃體狀態_讀取RFID_檢查按下(ref cnt_Program_櫃體狀態_讀取RFID);
            if (cnt_Program_櫃體狀態_讀取RFID == 2) cnt_Program_櫃體狀態_讀取RFID_初始化(ref cnt_Program_櫃體狀態_讀取RFID);
            if (cnt_Program_櫃體狀態_讀取RFID == 3) cnt_Program_櫃體狀態_讀取RFID = 65500;
            if (cnt_Program_櫃體狀態_讀取RFID > 1) cnt_Program_櫃體狀態_讀取RFID_檢查放開(ref cnt_Program_櫃體狀態_讀取RFID);

            if (cnt_Program_櫃體狀態_讀取RFID == 65500)
            {
                PLC_Device_櫃體狀態_讀取RFID.Bool = false;
                cnt_Program_櫃體狀態_讀取RFID = 65535;
            }
        }
        void cnt_Program_櫃體狀態_讀取RFID_檢查按下(ref int cnt)
        {
            if (PLC_Device_櫃體狀態_讀取RFID.Bool) cnt++;
        }
        void cnt_Program_櫃體狀態_讀取RFID_檢查放開(ref int cnt)
        {
            if (!PLC_Device_櫃體狀態_讀取RFID.Bool) cnt = 65500;
        }
        void cnt_Program_櫃體狀態_讀取RFID_初始化(ref int cnt)
        {
            string IP = "";
            int RFID_Num = -1;
            string 卡號 = "";
            List<object[]> list_人員資料 = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_人員資料_buf = new List<object[]>();
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Box_Index_Table.SQL_GetAllRows(false);
            List<object[]> list_locker_table_value_buf = new List<object[]>();
            List<RFID_UI.RFID_UID_Class> list_RFID_UID_Class = this.rfiD_UI.GetRFID();

            for (int i = 0; i < list_RFID_UID_Class.Count; i++)
            {
                IP = list_RFID_UID_Class[i].IP;
                RFID_Num = list_RFID_UID_Class[i].Num;
                卡號 = list_RFID_UID_Class[i].UID;
                list_人員資料_buf = list_人員資料.GetRows((int)enum_人員資料.卡號, 卡號);

                if (list_人員資料_buf.Count == 0) continue;
                Pannel_Box pannel_Box = this.List_Pannel_Box.SortByRFID(IP, RFID_Num);
                if (pannel_Box == null) continue;
                string 姓名 = list_人員資料_buf[0][(int)enum_人員資料.姓名].ObjectToString();
                string 藥櫃編號 = pannel_Box.Number.ToString();
                string 病房名稱 = pannel_Box.WardName;
                string ID = list_人員資料_buf[0][(int)enum_人員資料.ID].ObjectToString();
                string value_hex = list_人員資料_buf[0][(int)enum_人員資料.開門權限].ObjectToString();
                //long.TryParse(list_人員資料_buf[0][(int)enum_人員資料.開門權限].ObjectToString(), out 權限);
                bool flag_開門權限 = this.Function_人員資料_取得開門權限(value_hex, pannel_Box.Number.StringToInt32());
                if (flag_開門權限)
                {
                    if (!pannel_Box.IsOpen())
                    {
                        this.新增事件紀錄(enum_事件類型.開啟門片, ID, 姓名, 藥櫃編號, 病房名稱, "");
                        pannel_Box.Open();
                    }
                }
            }
            cnt++;
        }
























        #endregion
    }
}
