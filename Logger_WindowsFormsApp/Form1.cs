using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLUI;
using LoggerClassLibrary;
using Basic;

namespace Logger_WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.init.Click += Init_Click;
            this.get_all.Click += Get_all_Click;
        }

        private void Get_all_Click(object sender, EventArgs e)
        {
            List<loggerClass> loggerClasses = loggerClass.get_all("http://www.kutech.tw:4436", "", "");
            if (loggerClasses == null)
            {
                Console.WriteLine("未接到回傳資訊11111111");
                return;
            }
            Console.WriteLine($"{loggerClasses.JsonSerializationt()}");
        }

        private void Init_Click(object sender, EventArgs e)
        {
            SQLUI.Table table = loggerClass.Init("http://www.kutech.tw:4436", "", "");
            if (table == null)
            {
                Console.WriteLine($"未接收到回傳資訊");
                return;
            }
            Console.WriteLine($"{table.JsonSerializationt()}");
        }
    }
}
