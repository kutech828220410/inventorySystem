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
using DeltaMotor485;
using DrawingClass;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Program_調配排程_Init()
        {
            this.plC_RJ_Button_調配排程_處方選擇.MouseDownEvent += PlC_RJ_Button_調配排程_處方選擇_MouseDownEvent;

            this.plC_UI_Init.Add_Method(Program_調配排程);
        }

  

        private void Program_調配排程()
        {

        }
        #region Funtion

        #endregion
        #region Event
        private void PlC_RJ_Button_調配排程_處方選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_備藥通知處方選擇 dialog_備藥通知處方選擇 = new Dialog_備藥通知處方選擇();
            if (dialog_備藥通知處方選擇.ShowDialog() != DialogResult.Yes) return;

            uc_備藥通知內容.Init(dialog_備藥通知處方選擇.udnoectc, 登入者名稱 ,true);
        }
        #endregion
    }
}
