using System;
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
    public partial class Form1 : Form
    {
        PLC_Device PLC_Device_異常通知_覆盤錯誤 = new PLC_Device("S800");
        private void Program_異常通知_覆盤錯誤_Init()
        {
            string url = $"{dBConfigClass.Api_URL}/api/MedRecheckLog/init";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            Table table = json.JsonDeserializet<Table>();
            this.sqL_DataGridView_異常通知_覆盤錯誤.DataBaseName = table.DBName;
            this.sqL_DataGridView_異常通知_覆盤錯誤.Server = table.Server;
            this.sqL_DataGridView_異常通知_覆盤錯誤.UserName = table.Username;
            this.sqL_DataGridView_異常通知_覆盤錯誤.Password = table.Password;
            this.sqL_DataGridView_異常通知_覆盤錯誤.Port = table.Port.StringToUInt32();
            this.sqL_DataGridView_異常通知_覆盤錯誤.Init(table);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnVisible(false, new enum_MedRecheckLog().GetEnumNames());

            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.藥碼);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.藥名);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.效期);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.批號);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.操作人);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.校正庫存值);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.系統理論值);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.覆盤理論值);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.排除時間);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.發生時間);
            this.sqL_DataGridView_異常通知_覆盤錯誤.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_MedRecheckLog.狀態);
            plC_UI_Init.Add_Method(Program_異常通知_覆盤錯誤);
        }
        MyTimerBasic MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄 = new MyTimerBasic(5000);

        private void Program_異常通知_覆盤錯誤()
        {
            if (MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄.IsTimeOut())
            {
                List<MedRecheckLogClass> medRecheckLogClasses = Function_異常通知_覆盤錯誤_取得未排除();
                PLC_Device_異常通知_覆盤錯誤.Bool = (medRecheckLogClasses.Count != 0);

                MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄.TickStop();
                MyTimerBasic_異常通知_覆盤錯誤_取得異常紀錄.StartTickTime();
            }
        }

        #region Function

        private List<MedRecheckLogClass> Function_異常通知_覆盤錯誤_取得未排除()
        {
            List<MedRecheckLogClass> medRecheckLogClasses = new List<MedRecheckLogClass>();
            string url = $"{API_Server}/api/MedRecheckLog/get_ng_state_data";
            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;

            string json_in = returnData.JsonSerializationt(true);
            string json_result = Net.WEBApiPostJson(url, json_in);

            returnData = json_result.JsonDeserializet<returnData>();
            medRecheckLogClasses = returnData.Data.ObjToListClass<MedRecheckLogClass>();
            return medRecheckLogClasses;
        }

        #endregion
    }
}
