using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;
using MyOffice;
namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += Button1_Click;
            this.button2.Click += Button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<string> serverNames = new List<string>();
            List<string> serverTypes = new List<string>();
            serverNames.Add("口服2");
            serverNames.Add("口服2");
            serverTypes.Add("調劑台");
            serverTypes.Add("調劑台");
            List<transactionsClass> transactionsClasses = transactionsClass.get_datas_by_op_time_st_end("http://127.0.0.1:4433", "2024-03-20".StringToDateTime(), "2024-04-01".StringToDateTime(), serverNames, serverTypes);

            byte[] excels = transactionsClass.download_datas_excel("http://127.0.0.1:4433",transactionsClasses);
            //byte[] excels = transactionsClass.download_cdmis_datas_excel("http://127.0.0.1:4433", "VALI2", "2024-01-01".StringToDateTime(), "2024-04-01".StringToDateTime(), serverNames, serverTypes);
            if (saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;
            excels.SaveFileStream(saveFileDialog_SaveExcel.FileName);
        }
    }
}
