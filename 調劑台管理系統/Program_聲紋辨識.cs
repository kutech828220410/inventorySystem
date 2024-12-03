using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Runtime.InteropServices;
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;
using AudioProcessingLibrary;
using System.Threading;
namespace 調劑台管理系統
{
    public class voice_analyze
    {
        public string name = "";
        public string conf = "";
    }
    public partial class Main_Form : Form
    {
        public voice_analyze VoiceSample = null;
        public static MicrophoneRecorder microphoneRecorder = new MicrophoneRecorder();
        private void Program_聲紋辨識_Init()
        {
            if (myConfigClass.聲紋辨識_IP.Check_IP_Adress())
            {
                string IP = myConfigClass.聲紋辨識_IP;
                microphoneRecorder.OnTcpMessageReceived += MicrophoneRecorder_OnTcpMessageReceived;


                // 啟動 TCP 字串監聽執行緒
                Thread listenerThread = new Thread(() => microphoneRecorder.StartTextTcpListener(3301))
                {
                    IsBackground = true
                };
                listenerThread.Start();

                // 啟動音訊串流到伺服器
                //microphoneRecorder.StartAudioStreaming(IP, 3300);
            }
            this.plC_UI_Init.Add_Method(Program_聲紋辨識);
        }

        private void Program_聲紋辨識()
        {

        }
        private void MicrophoneRecorder_OnTcpMessageReceived(string text)
        {
            if (text == "HEARTBEAT")
            {
                //Console.WriteLine($"收到心跳包: {text}");
                return;
            }
            Console.WriteLine($"原生資料 : {text}");
            returnData returnData = text.JsonDeserializet<returnData>();
            if (returnData == null) return;
            if (returnData.Method == "Voice_Print")
            {
                voice_analyze voice_Analyze = returnData.Data.ObjToClass<voice_analyze>();
                if (voice_Analyze != null)
                {
                    Console.WriteLine($"收到聲紋辨識資料: {text}");
                    VoiceSample = voice_Analyze;
                }
            }
            if (returnData.Method == "Pill_Name")
            {
                Console.WriteLine($"收到聲紋語音資料: {text}");
            }
        }
    }
}
