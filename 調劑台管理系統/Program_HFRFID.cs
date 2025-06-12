using Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RfidReaderLib;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        static public RfidReader rfidReader_1 = new RfidReader();
        static public RfidReader rfidReader_2 = new RfidReader();
        static public List<string> rfidReader_TagUID = new List<string>();
        static bool RfidReaderEnable
        {
            get
            {
                return (myConfigClass.HFRFID_1_COMPort.StringIsEmpty() == false || myConfigClass.HFRFID_2_COMPort.StringIsEmpty() == false);
            }
        }
        private MyThread myThread_HFRFID = new MyThread();
        private void Program_HFRFID_Init()
        {
            if (myConfigClass.HFRFID_1_COMPort.StringIsEmpty() == false)
            {
                rfidReader_1.ConfigurePort(myConfigClass.HFRFID_1_COMPort, 115200);
                rfidReader_1.Open();
                if (rfidReader_1.IsOpen == false)
                {
                    MessageBox.Show("HFRFID(1)讀取器無法開啟，請檢查連接");
                }
            }
            if (myConfigClass.HFRFID_2_COMPort.StringIsEmpty() == false)
            {
                rfidReader_2.ConfigurePort(myConfigClass.HFRFID_2_COMPort, 115200);
                rfidReader_2.Open();
                if (rfidReader_2.IsOpen == false)
                {
                    MessageBox.Show("HFRFID(2)讀取器無法開啟，請檢查連接");
                }
            }
            if (RfidReaderEnable)
            {
                plC_RJ_Button_收支作業_RFID入庫.Visible = true;
                plC_RJ_Button_收支作業_RFID出庫.Visible = true;
                plC_RJ_Button_收支作業_RFID盤點.Visible = true;
                myThread_HFRFID.AutoRun(true);
                myThread_HFRFID.Add_Method(Program_HFRFID);
                myThread_HFRFID.SetSleepTime(100);
                myThread_HFRFID.Trigger();
            }
         
        }
        private void Program_HFRFID()
        {
            List<string> rfidReader_temp = new List<string>();
            if (myConfigClass.HFRFID_1_COMPort.StringIsEmpty() == false)
            {
                List<string> temp = rfidReader_1.ReadMultipleUIDs();
                rfidReader_temp.LockAdd(temp);
            }
            if (myConfigClass.HFRFID_2_COMPort.StringIsEmpty() == false)
            {
                List<string> temp = rfidReader_2.ReadMultipleUIDs();
                rfidReader_temp.LockAdd(temp);
            }
            rfidReader_TagUID = rfidReader_temp;
        }
    }
}
