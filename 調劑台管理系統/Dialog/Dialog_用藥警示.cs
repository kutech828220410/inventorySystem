﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using MyUI;
using HIS_DB_Lib;
using SQLUI;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_用藥警示 : MyDialog
    {
        public Dialog_用藥警示(string text)
        {
            InitializeComponent();
            this.textBox1.Text = text;
        }
    }
}
