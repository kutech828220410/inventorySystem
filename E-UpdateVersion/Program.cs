﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace E_UpdateVersion
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string ProcessName = Process.GetCurrentProcess().ProcessName;
            Process[] process = Process.GetProcesses();
            int num = 0;
            for (int i = 0; i < process.Length; i++)
            {
                if (process[i].ProcessName == ProcessName) num++;
            }
            if (num <= 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("程式已經在執行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

           
        }
    }
}
