using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace 智能藥庫系統
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main_Form());
            }
            catch(Exception ex)
            {
                Logger.Log($"Exception : {ex.Message}");
            }
          
        }
    }
}
