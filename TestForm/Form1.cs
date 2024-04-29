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

            List<inventoryResultClass> inventoryResultClasses = inventoryResultClass.get_inventoryResult_by_st_end_time("http://127.0.0.1:4433", "ds01", "藥庫", DateTime.Now, DateTime.Now);
            inventoryResult_content inventoryResult_Content=   inventoryResultClasses[0].Contents[0];
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            inventoryResultClass inventoryResultClass = new inventoryResultClass();
            inventoryResultClass.庫別 = "藥庫";
            inventoryResultClass.庫名 = "藥局";
            inventoryResultClass.盤點切帳時間 = DateTime.Now.ToDateTimeString_6();

            ValidityClass validityClass = new ValidityClass();
            validityClass.AddValidity("2024-04-29", "TEST");
            validityClass.AddValidity("2026-05-30", "QQ212355");
            string text = validityClass.ToString();
            Console.WriteLine($"[ValidityClass]\n{text}");
            validityClass.Value = text;
            inventoryResultClass.AddContent("21098", "Neuquinon Tab 10mg(Ubidecarenone", 5000, 4000, validityClass);
            string json = inventoryResultClass.JsonSerializationt(true);
            
            this.textBox1.Text = json;
            inventoryResultClass.add_inventoryResult("http://127.0.0.1:4433", "ds01", "藥庫", inventoryResultClass);
        }
    }
}
