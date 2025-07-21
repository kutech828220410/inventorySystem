using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using MyUI;
using HIS_DB_Lib;
using SQLUI;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_用藥警示 : MyDialog
    {
        public Dialog_用藥警示(string text)
        {
            InitializeComponent();
            this.textBox1.Multiline = true; // 啟用多行顯示
            this.textBox1.WordWrap = true;  // 啟用自動換行
            text = text.Replace("\n", Environment.NewLine);

            this.textBox1.Text = text;

            // 預設不要全選，讓游標在文字最末端，或隱藏游標
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.SelectionLength = 0;

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
        }

        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Close();
        }

        public static void CloseAllDialog()
        {
            foreach (Form form in Application.OpenForms.OfType<Form>().ToList())
            {
                if (form is Dialog_用藥警示)
                {
                    form.Invoke(new Action(() =>
                    {
                        try
                        {
                            form.Close();
                        }
                        catch
                        {
                            // 可以視情況加上LOG
                        }
                    }));
                }
            }
        }
    }
}
