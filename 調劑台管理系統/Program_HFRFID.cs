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
        static public HF_ReaderLib.HF_readerlib rfidReader_1 = new HF_ReaderLib.HF_readerlib();
        static public HF_ReaderLib.HF_readerlib rfidReader_2 = new HF_ReaderLib.HF_readerlib();
        static public List<string> rfidReader_TagUID = new List<string>();
        static public bool RfidReaderEnable
        {
            get
            {
                return (myConfigClass.HFRFID_1_COMPort.StringIsEmpty() == false || myConfigClass.HFRFID_2_COMPort.StringIsEmpty() == false);
            }
        }
        static public bool HFRFID_debug = false;

        private MyThread myThread_HFRFID = new MyThread();
        private void Program_HFRFID_Init()
        {
            if (myConfigClass.HFRFID_1_COMPort.StringIsEmpty() == false)
            {
                rfidReader_1.OpenPort(myConfigClass.HFRFID_1_COMPort, 115200);
                //if (rfidReader_1.IsOpen == false)
                //{
                //    MessageBox.Show("HFRFID(1)讀取器無法開啟，請檢查連接");
                //}
            }
            if (myConfigClass.HFRFID_2_COMPort.StringIsEmpty() == false)
            {
                rfidReader_2.OpenPort(myConfigClass.HFRFID_2_COMPort, 115200);
                //if (rfidReader_2.IsOpen == false)
                //{
                //    MessageBox.Show("HFRFID(2)讀取器無法開啟，請檢查連接");
                //}
            }
            if (RfidReaderEnable)
            {
                plC_RJ_Button_收支作業_RFID入庫.Visible = true;
                plC_RJ_Button_收支作業_RFID出庫.Visible = true;
                plC_RJ_Button_收支作業_RFID清點.Visible = true;
                //myThread_HFRFID.AutoRun(true);
                //myThread_HFRFID.Add_Method(Program_HFRFID);
                //myThread_HFRFID.SetSleepTime(1);
                //myThread_HFRFID.Trigger();
            }
         
        }

        private void Program_HFRFID()
        {
            //DateTime totalStart = DateTime.Now;
            //if (HFRFID_debug) Console.WriteLine($"[RFID] 開始取得時間: {totalStart:HH:mm:ss.fff}");

            //List<string> rfidReader_temp = new List<string>();
            //List<Task> tasks = new List<Task>();

            //tasks.Add(Task.Run(new Action(delegate
            //{
            //    DateTime start = DateTime.Now;
            //    if (HFRFID_debug) Console.WriteLine($"[RFID_1] 開始讀取: {start:HH:mm:ss.fff}");

            //    if (myConfigClass.HFRFID_1_COMPort.StringIsEmpty() == false)
            //    {
            //        List<string> temp = rfidReader_1.ReadMultipleUIDs();
            //        rfidReader_temp.LockAdd(temp);
            //    }

            //    DateTime end = DateTime.Now;
            //    if (HFRFID_debug) Console.WriteLine($"[RFID_1] 結束讀取: {end:HH:mm:ss.fff}，耗時: {(end - start).TotalMilliseconds} ms");
            //})));

            //tasks.Add(Task.Run(new Action(delegate
            //{
            //    DateTime start = DateTime.Now;
            //    if (HFRFID_debug) Console.WriteLine($"[RFID_2] 開始讀取: {start:HH:mm:ss.fff}");

            //    if (myConfigClass.HFRFID_2_COMPort.StringIsEmpty() == false)
            //    {
            //        List<string> temp = rfidReader_2.ReadMultipleUIDs();
            //        rfidReader_temp.LockAdd(temp);
            //    }

            //    DateTime end = DateTime.Now;
            //    if (HFRFID_debug) Console.WriteLine($"[RFID_2] 結束讀取: {end:HH:mm:ss.fff}，耗時: {(end - start).TotalMilliseconds} ms");
            //})));

            //Task.WhenAll(tasks.ToArray()).Wait();

            //DateTime totalEnd = DateTime.Now;
            //if (HFRFID_debug) Console.WriteLine($"[RFID] 全部完成: {totalEnd:HH:mm:ss.fff}，總耗時: {(totalEnd - totalStart).TotalMilliseconds} ms");

            //rfidReader_TagUID = rfidReader_temp;
        }

        private static readonly object ReadAllUIDsOnceOnly_lock = new object();


        public static List<string> ReadAllUIDsOnceOnly(bool HFRFID_debug = true, params int[] reader_index)
        {
            lock (ReadAllUIDsOnceOnly_lock)
            {
                DateTime totalStart = DateTime.Now;
                if (HFRFID_debug) Console.WriteLine($"[RFID] 開始取得時間: {totalStart:HH:mm:ss.fff}");

                List<HF_ReaderLib.Entity.TagInfoEntity> rfidReader_temp = new List<HF_ReaderLib.Entity.TagInfoEntity>();
                List<Task> tasks = new List<Task>();
                List<HF_ReaderLib.Entity.TagInfoEntity> reader1_tagInfoList = new List<HF_ReaderLib.Entity.TagInfoEntity>();
                List<HF_ReaderLib.Entity.TagInfoEntity> reader1_tagInfoList_temp = new List<HF_ReaderLib.Entity.TagInfoEntity>();
                List<HF_ReaderLib.Entity.TagInfoEntity> reader2_tagInfoList = new List<HF_ReaderLib.Entity.TagInfoEntity>();
                List<HF_ReaderLib.Entity.TagInfoEntity> reader2_tagInfoList_temp = new List<HF_ReaderLib.Entity.TagInfoEntity>();

          

                if (!myConfigClass.HFRFID_1_COMPort.StringIsEmpty())
                {
                    if (reader_index == null || reader_index.Length == 0)
                    {
                        for (int i = 0; i <= 10; i++)
                        {
                            rfidReader_1.Inventory(i, out reader1_tagInfoList_temp);
                            reader1_tagInfoList.LockAdd(reader1_tagInfoList_temp);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < reader_index.Length; i++)
                        {
                            rfidReader_1.Inventory(reader_index[i], out reader1_tagInfoList_temp);
                            reader1_tagInfoList.LockAdd(reader1_tagInfoList_temp);
                        }
                    }
                }
     

                DateTime totalEnd = DateTime.Now;

          
                if (HFRFID_debug) Console.WriteLine($"[RFID] 全部完成: {totalEnd:HH:mm:ss.fff}，總耗時: {(totalEnd - totalStart).TotalMilliseconds} ms");
                rfidReader_temp.LockAdd(reader1_tagInfoList);
                rfidReader_temp.LockAdd(reader2_tagInfoList);
                return (from temp in rfidReader_temp
                        select temp.TagUID).Distinct().ToList();
            }
        }
    }
}
