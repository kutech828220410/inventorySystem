using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;

namespace 調劑台管理系統
{
    public partial class Dialog_交班對點 : MyDialog
    {
        public Dialog_交班對點()
        {
            InitializeComponent();
            this.LoadFinishedEvent += Dialog_交班對點_LoadFinishedEvent;
        }

        private void Dialog_交班對點_LoadFinishedEvent(EventArgs e)
        {
            List<StepEntity> list = new List<StepEntity>();
            list.Add(new StepEntity("1", "登入", 1, "請登入使用者(1號)", eumStepState.Completed, null));
            list.Add(new StepEntity("2", "登入", 2, "請登入使用者(1號)", eumStepState.Completed, null));
            list.Add(new StepEntity("3", "藥品選擇", 3, "選擇交班藥品", eumStepState.Waiting, null));
            list.Add(new StepEntity("4", "交班完成", 4, "清點交班藥品", eumStepState.Waiting, null));
            this.stepViewer.CurrentStep = 1;
            this.stepViewer.ListDataSource = list;

        }
    }
}
