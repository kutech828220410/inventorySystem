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
   
        Image image = null;
        public Form1()
        {
            InitializeComponent();
            button_LoadImage.Click += Button_LoadImage_Click;
            button_call_api.Click += Button_call_api_Click;
        }

        private void Button_call_api_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(image);
            string Base64 = image.ImageToBase64();
            List<textVisionClass> textVisionClasses = new List<textVisionClass>();
            textVisionClass textVisionClass = new textVisionClass();
            textVisionClass.圖片 = $"data:image/jpeg;base64,{Base64}";
            textVisionClass.操作者ID = "";
            textVisionClass.操作者姓名 = "";
            textVisionClasses.Add(textVisionClass);

            returnData returnData = new returnData();
            returnData.Data = textVisionClasses;

            string json_in = returnData.JsonSerializationt(true);


            string url = "https://www.kutech.tw:4443/api/PCMPO/analyze";

            string json_out = Basic.Net.WEBApiPostJson(url, json_in);

            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            List<textVisionClass> textVisionClasses_out = returnData_out.Data.ObjToClass<List<textVisionClass>>();
            string[] strs_temp = new textVisionClass.enum_point_type().GetEnumNames();
            for (int i = 0; i < strs_temp.Length; i++)
            {
                List<Point> points = textVisionClasses_out[0].GetPoints(strs_temp[i]);
     
                Point p1 = points[0];
                Point p2 = points[1];
                Point p3 = points[2];
                Point p4 = points[3];
                g.DrawLine(new Pen(new SolidBrush(Color.Red)), p1, p2);
                g.DrawLine(new Pen(new SolidBrush(Color.Red)), p2, p3);
                g.DrawLine(new Pen(new SolidBrush(Color.Red)), p3, p4);
                g.DrawLine(new Pen(new SolidBrush(Color.Red)), p4, p1);
            }
       



            pictureBox1.Image = image;

            g.Dispose();

        }
        private void Button_LoadImage_Click(object sender, EventArgs e)
        {
              //"batch_num_coord": "1909,967;2224,967;2224,1020;1909,1020",
              //"cht_name_coord": "35,1023;753,1023;753,1067;35,1067",
              //"expirydate_coord": "2410,967;2637,967;2637,1023;2410,1023",
              //"po_num_coord": "1518,683;2290,683;2290,737;1518,737",
              //"qty_coord": "1301,960;1392,960;1392,1020;1301,1020",
              //"name_coord": "32,967;621,967;621,1011;32,1011",

            if (this.openFileDialog.ShowDialog() != DialogResult.OK) return;

            image = Bitmap.FromFile(this.openFileDialog.FileName);

         

            pictureBox1.Image = image;


        }

    }
}
