using Basic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SQLUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using H_Pannel_lib;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
namespace HIS_WebApi
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        public static string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        [Route("IsAlive")]
        [HttpGet()]
        public string Get_IsAlive()
        {
            string ProcessName = "調劑台管理系統";
            Process[] process = Process.GetProcesses();
            int num = 0;
            for (int i = 0; i < process.Length; i++)
            {
                if (process[i].ProcessName == ProcessName) num++;
            }
            if (num >= 1) return "OK";
            else return "NG";
        }
        [Route("time")]
        [HttpGet()]
        public string Get_time()
        {
            return DateTime.Now.ToDateTimeString();
        }
        [Route("goolge_voice")]
        [HttpGet()]
        public string Get_goolge_voice(string text,string lan)
        {
            if(lan.StringIsEmpty())
            {
                lan = "zh-tw";
            }
            string base64 = Basic.Voice.GoogleSpeakerBase64(text, lan);
            if(ContainerChecker.IsRunningInDocker() == true)
            {
                base64 = AudioProcessingLibrary.Voice.PlayBase64Mp3WithFFmpegAndReturnMp3Base64_Docker(base64, 1.8F);
            }
            return base64;
           

        
        }
        [Route("currentDirectory")]
        [HttpGet()]
        public string Get_currentDirectory(string text, string lan)
        {
         
            return currentDirectory;
        }
    }
}
