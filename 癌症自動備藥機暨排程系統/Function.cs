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

namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Function_取得本地儲位()
        {
            this.List_本地儲位 = storageUI_EPD_266.SQL_GetAllStorage();
        }
    }
}
