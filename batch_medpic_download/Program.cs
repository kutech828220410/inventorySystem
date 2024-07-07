using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLUI;
using HIS_DB_Lib;
using H_Pannel_lib;
using Basic;
using MyUI;
using System.IO;
using System.Reflection;

namespace batch_medpic_download
{
    class Program
    {
        static private string API_Server = "http://127.0.0.1:4433";
        static public string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static void Main(string[] args)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                Logger.LogAddLine();

                List<medClass> medClasses = medClass.get_med_cloud(API_Server);
                Logger.Log($"取得藥品資料共<{medClasses.Count}>筆");
                List<Task> tasks = new List<Task>();
                List<med>
                List<object[]> list_medpic = new List<object[]>();
                foreach(medClass medClass in medClasses)
                {
                    if (medClass.圖片網址.StringIsEmpty() == false)
                    {
                        string base64 = Basic.Net.DownloadImageAsBase64(medClass.圖片網址);
                        Logger.Log($"({medClass.藥品碼}){medClass.藥品名稱}".StringLength(50) + $"取得圖片Base64成功");
                    }
                }

                Logger.LogAddLine();
            }
            catch(Exception ex)
            {
                Logger.Log($"Exception : {ex.Message}");
            }
            finally
            {

            }

        }
    }
}
