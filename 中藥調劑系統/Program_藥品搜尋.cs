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
using MyOffice;
using HIS_DB_Lib;
using H_Pannel_lib;

namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        private void Program_藥品搜尋_Init()
        {
            this.rJ_Button_藥品搜尋_搜尋.MouseDownEvent += RJ_Button_藥品搜尋_搜尋_MouseDownEvent;
            plC_UI_Init.Add_Method(Program_藥品搜尋);
        }

        private void Program_藥品搜尋()
        {

        }
        private void RJ_Button_藥品搜尋_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_藥品搜尋 dialog_藥品搜尋 = new Dialog_藥品搜尋();
            if (dialog_藥品搜尋.ShowDialog() != DialogResult.Yes) return;
        }
    }
}
