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

namespace 智能藥庫管理系統
{
    public partial class Main_Form : Form
    {

        private void Program_系統_Init()
        {
            storageUI_EPD_266.Init();
            plC_UI_Init.Add_Method(Program_系統);
        }
        private void Program_系統()
        {

        }
    }
}
