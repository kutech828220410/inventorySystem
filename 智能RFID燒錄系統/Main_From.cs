using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using SQLUI;
using RfidReaderLib;
namespace 智能RFID燒錄系統
{
    public partial class Main_From : MyDialog
    {
        private RfidReader _rfidReader = new RfidReader();
        public Main_From()
        {
            InitializeComponent();
            this.Load += Main_From_Load;
        }

        private void Main_From_Load(object sender, EventArgs e)
        {
            this.comboBox_Comport.DataSource = MySerialPort.GetPortNames();
            rJ_Button_Connect.MouseDownEvent += RJ_Button_Connect_MouseDownEvent;
        }

        private void RJ_Button_Connect_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                _rfidReader.ConfigurePort(this.comboBox_Comport.Text , 115200);
                _rfidReader.Open();
                if (_rfidReader.IsOpen)
                {
                    string str = _rfidReader.ReadHardwareInfo();
                    if(str.StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("Failed to read RFID reader information.");
                        return;
                    }
                    rJ_Lable_device_info.Text = $"Device info :{str}";
                    this.rJ_Button_Connect.Text = "Connected";
                    this.rJ_Button_Connect.Enabled = false;
                    this.comboBox_Comport.Enabled = false;
                }
                else
                {
                    MyMessageBox.ShowDialog("Failed to connect to RFID reader.");
                }
            }));
        
        }
    }
}
