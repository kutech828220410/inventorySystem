using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Basic;
namespace 調劑台管理系統
{
    static class Program
    {
        private static System.Threading.Mutex mutex;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {

            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                try
                {
                    Application.Run(new Form1());
                }
                catch(Exception e)
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = $@"{desktopPath}\log.txt";
                    if (!File.Exists(filePath))
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, false))
                        {
                            string text = $"{e.Message} {DateTime.Now.ToDateTimeString()}";
                            writer.WriteLine(text);
                        }
                    }
                    else
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            string text = $"{e.Message} {DateTime.Now.ToDateTimeString()}";
                            writer.WriteLine(text);
                        }
                    }
                   
                }             
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
