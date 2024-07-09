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


namespace batch_medpic_download_to_base64
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
                List<medPicClass> medPicClasses = new List<medPicClass>();
                List<object[]> list_medpic = new List<object[]>();
                string log = "";
                foreach (medClass medClass in medClasses)
                {
                    if (medClass.圖片網址.StringIsEmpty() == false)
                    {
                        tasks.Add(Task.Run(new Action(delegate
                        {
                            List<medPicClass> medPicClasses_buf = new List<medPicClass>();
                            string base64 = Basic.Net.DownloadImageAsBase64(medClass.圖片網址);
                            if(base64.StringIsEmpty())
                            {

                            }
                            medPicClass medPicClass = new medPicClass();
                            medPicClass.藥碼 = medClass.藥品碼;
                            medPicClass.藥名 = medClass.藥品名稱;
                            medPicClass.副檔名 = GetFileExtension(medClass.圖片網址);
                            medPicClass.pic_base64 = base64;
                            medPicClasses.LockAdd(medPicClass);
                            string losg_temp = $"({medClass.藥品碼}){medClass.藥品名稱}".StringLength(50) + $"取得圖片Base64成功\n";
                            log += losg_temp;

                            Console.Write(losg_temp);
                        })));
              
                    }
                }
                Logger.Log(log);
        
                Task.WhenAll(tasks).Wait();
                for(int i = 0; i < medPicClasses.Count; i++)
                {
                    medPicClass.add(API_Server, medPicClasses[i]);
                }
                Logger.Log($"新增<{medPicClasses.Count}>筆藥品圖片至資料庫");

                Logger.LogAddLine();
            }
            catch (Exception ex)
            {
                Logger.Log($"Exception : {ex.Message}");
            }
            finally
            {

            }

        }
        public static string GetFileExtension(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL不能為空", nameof(url));
            }

            int lastDotIndex = url.LastIndexOf('.');
            if (lastDotIndex == -1 || lastDotIndex == url.Length - 1)
            {
                throw new ArgumentException("URL不包含有效的副檔名", nameof(url));
            }

            return url.Substring(lastDotIndex).Replace(".","");
        }
    }


}
