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
using FpMatchLib;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        static public FpMatchSoket fpMatchSoket = new FpMatchSoket();
        static public bool flag_指紋辨識_Init = false;
        private void Program_指紋辨識_Init()
        {
            FpMatchSoket.ConsoleWrite = true;
            Task.Run(new Action(delegate 
            {
                flag_指紋辨識_Init = fpMatchSoket.Open();
                if (flag_指紋辨識_Init)
                {
                    this.Invoke(new Action(delegate
                    {
                        plC_RJ_Button_調劑作業_指紋登入.Visible = true;
                        plC_Button_人員資料_指紋註冊.Visible = true;
                    }));
               
                }
            }));

            this.plC_UI_Init.Add_Method(Program_指紋辨識);
        }
        private void Program_指紋辨識()
        {

        }
        #region Function
        static public bool Function_指紋辨識初始化(bool show_error_message , bool openSoket)
        {
            if (flag_指紋辨識_Init == false) return false;
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(2000);
            if (Main_Form.fpMatchSoket.StateCode != stateCode.READY && Main_Form.fpMatchSoket.StateCode != stateCode.NONE || Main_Form.fpMatchSoket.IsOpen == false || openSoket == true)
            {
                if (Main_Form.fpMatchSoket.Open() == false)
                {
                    if(show_error_message)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("指紋模組未啟用", 2000);
                        dialog_AlarmForm.ShowDialog();
                    }
            
                    return false;
                }
            }

            while (true)
            {
                if (Main_Form.fpMatchSoket.IsOpen == true) break;
                if (myTimerBasic.IsTimeOut())
                {
                    if (show_error_message)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("指紋模組未啟用", 2000);
                        dialog_AlarmForm.ShowDialog();
                    }
                    return false;
                }
                System.Threading.Thread.Sleep(10);
            }
            return true;
        }
        static public bool Function_指紋辨識初始化()
        {
            return Function_指紋辨識初始化(true , true);
        }
        #endregion
    }
}
