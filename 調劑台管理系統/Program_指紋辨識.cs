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
        public bool flag_指紋辨識_Init = false;
        private void Program_指紋辨識_Init()
        {
            FpMatchSoket.ConsoleWrite = true;
            Task.Run(new Action(delegate 
            {
                fpMatchSoket.Open();
                flag_指紋辨識_Init = true;
            }));

            this.plC_UI_Init.Add_Method(Program_指紋辨識);
        }
        private void Program_指紋辨識()
        {

        }
        #region Function

        #endregion
    }
}
