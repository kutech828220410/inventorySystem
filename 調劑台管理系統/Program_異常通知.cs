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
    public partial class Main_Form : Form
    {
        MyTimerBasic MyTimerBasic_異常通知 = new MyTimerBasic(3000);
        private void Program_異常通知_Init()
        {
            plC_RJ_Button_異常通知.MouseDownEvent += PlC_RJ_Button_異常通知_MouseDownEvent;
            plC_UI_Init.Add_Method(Program_異常通知);
        }



        private void Program_異常通知()
        {
            if (MyTimerBasic_異常通知.IsTimeOut())
            {
                List<notifyExceptionClass> notifyExceptionClasses = Function_取得異常通知訊息();
                if (Function_取得異常通知訊息().Count > 0)
                {
                    if (plC_RJ_Button_異常通知.Visible == false)
                    {
                        this.Invoke(new Action(delegate
                        {
                            plC_RJ_Button_異常通知.Visible = true;
                        }));
                    }
                }
                else
                {
                    if (plC_RJ_Button_異常通知.Visible == true)
                    {
                        this.Invoke(new Action(delegate
                        {
                            plC_RJ_Button_異常通知.Visible = false;
                        }));
                    }
                }

                MyTimerBasic_異常通知.TickStop();
                MyTimerBasic_異常通知.StartTickTime();
            }
        }
        private void PlC_RJ_Button_異常通知_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_異常通知 dialog_異常通知 = new Dialog_異常通知(Function_取得異常通知訊息());
            dialog_異常通知.ShowDialog();
        }
        public static List<notifyExceptionClass> Function_取得異常通知訊息()
        {
            List<notifyExceptionClass> notifyExceptionClasses = new List<notifyExceptionClass>();
            List<medRecheckLogClass> medRecheckLogClasses = medRecheckLogClass.get_ng_state_data(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType);
            if (medRecheckLogClasses != null)
            {
                for (int i = 0; i < medRecheckLogClasses.Count; i++)
                {
                    notifyExceptionClass notifyExceptionClass = new notifyExceptionClass();
                    notifyExceptionClass.GUID = medRecheckLogClasses[i].GUID;
                    notifyExceptionClass.發生時間 = medRecheckLogClasses[i].發生時間;
                    notifyExceptionClass.類別 = medRecheckLogClasses[i].發生類別;
                    if (medRecheckLogClasses[i].發生類別 == enum_medRecheckLog_ICDT_TYPE.交班對點.GetEnumName() || medRecheckLogClasses[i].發生類別 == enum_medRecheckLog_ICDT_TYPE.盤點異常.GetEnumName())
                    {
                        notifyExceptionClass.內容 = $"({medRecheckLogClasses[i].藥碼}){medRecheckLogClasses[i].藥名} ,盤點量:{medRecheckLogClasses[i].盤點值}";
                    }
                    if (medRecheckLogClasses[i].發生類別 == enum_medRecheckLog_ICDT_TYPE.RFID入庫異常.GetEnumName() || medRecheckLogClasses[i].發生類別 == enum_medRecheckLog_ICDT_TYPE.RFID出庫異常.GetEnumName())
                    {
                        notifyExceptionClass.內容 = $"({medRecheckLogClasses[i].藥碼}){medRecheckLogClasses[i].藥名} ,{medRecheckLogClasses[i].事件描述}";
                    }
                    if (medRecheckLogClasses[i].發生類別 == enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName())
                    {
                        notifyExceptionClass.內容 = $"({medRecheckLogClasses[i].藥碼}){medRecheckLogClasses[i].藥名} ,{medRecheckLogClasses[i].事件描述}";
                    }
                    notifyExceptionClasses.Add(notifyExceptionClass);
                }
            }
            return notifyExceptionClasses;
        }

    }
}
