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
        static public RfidReader rfidReader = new RfidReader();
        static public List<string> rfidReader_TagUID = new List<string>();
        private MyThread myThread_HFRFID = new MyThread();
        private void Program_HFRFID_Init()
        {
            if (myConfigClass.HFRFID_COMPort.StringIsEmpty() == false)
            {
                rfidReader.ConfigurePort(myConfigClass.HFRFID_COMPort, 115200);
                rfidReader.Open();
                if(rfidReader.IsOpen == false)
                {
                    plC_RJ_Button_收支作業_RFID入庫.Visible = false;
                    plC_RJ_Button_收支作業_RFID出庫.Visible = false;

                    MessageBox.Show("RFID讀取器無法開啟，請檢查連接");
                }
                else
                {
                    plC_RJ_Button_收支作業_RFID入庫.Visible = true;
                    plC_RJ_Button_收支作業_RFID出庫.Visible = true;
                    myThread_HFRFID.AutoRun(true);
                    myThread_HFRFID.Add_Method(Program_HFRFID);
                    myThread_HFRFID.SetSleepTime(100);
                    myThread_HFRFID.Trigger();
                }
             
            }
        }
        private void Program_HFRFID()
        {
            rfidReader_TagUID = rfidReader.ReadMultipleUIDs();
        }
    }
}
