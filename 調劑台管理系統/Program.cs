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
            Basic.Screen.CloseConsole();
            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                Application.Run(new Main_Form());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
