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
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Program_出入庫作業_Init()
        {
            this.plC_RJ_Button_出入庫作業_入庫.MouseDownEvent += PlC_RJ_Button_出入庫作業_入庫_MouseDownEvent;
            this.plC_RJ_Button_出入庫作業_出庫.MouseDownEvent += PlC_RJ_Button_出入庫作業_出庫_MouseDownEvent;
            plC_UI_Init.Add_Method(Program_出入庫作業);
        }

        private void PlC_RJ_Button_出入庫作業_出庫_MouseDownEvent(MouseEventArgs mevent)
        {
            this.plC_RJ_Button_出入庫作業_入庫.Bool = false;
            this.plC_RJ_Button_出入庫作業_出庫.Bool = true;
            this.plC_RJ_Button_出入庫作業_入庫.BackgroundColor = Color.Silver;
            this.plC_RJ_Button_出入庫作業_出庫.BackgroundColor = Color.Black;
        }
        private void PlC_RJ_Button_出入庫作業_入庫_MouseDownEvent(MouseEventArgs mevent)
        {
            this.plC_RJ_Button_出入庫作業_入庫.Bool = true;
            this.plC_RJ_Button_出入庫作業_出庫.Bool = false;
            this.plC_RJ_Button_出入庫作業_入庫.BackgroundColor = Color.Black;
            this.plC_RJ_Button_出入庫作業_出庫.BackgroundColor = Color.Silver;

        }

        private void Program_出入庫作業()
        {

        }
    }
}
