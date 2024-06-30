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
using MyOffice;
using HIS_DB_Lib;
using H_Pannel_lib;

namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        private medClass medClass_藥品搜尋 = new medClass();
        private bool flag_藥品搜尋 = false;
        private void Program_藥品搜尋_Init()
        {
            this.rJ_Button_藥品搜尋_搜尋.MouseDownEvent += RJ_Button_藥品搜尋_搜尋_MouseDownEvent;
            plC_UI_Init.Add_Method(Program_藥品搜尋);
        }
        private bool flag_藥品搜尋頁面離開 = false;
        private void Program_藥品搜尋()
        {
            if (plC_ScreenPage_main.PageText == "藥品搜尋")
            {
                flag_藥品搜尋頁面離開 = false;
            }
            else
            {
                if (flag_藥品搜尋頁面離開 == false)
                {
                    if (medClass_藥品搜尋 != null)
                    {
                        Function_儲位亮燈(medClass_藥品搜尋.藥品碼, Color.Black);
                    }
                    flag_藥品搜尋頁面離開 = true;
                }
            }
            if (MySerialPort_Scanner01.IsConnected && plC_ScreenPage_main.PageText == "藥品搜尋")
            {
                string text = MySerialPort_Scanner01.ReadString();
                if (text.StringIsEmpty() == false)
                {
                    System.Threading.Thread.Sleep(200);
                    text = MySerialPort_Scanner01.ReadString();
                    MySerialPort_Scanner01.ClearReadByte();
                    text = text.Replace("\0", "");
                    text = text.Replace("\n", "");
                    text = text.Replace("\r", "");
                    List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, text);
                    if (medClasses.Count > 0)
                    {
                        
                        if (flag_藥品搜尋 == false)
                        {
                            if (medClass_藥品搜尋 != null)
                            {
                                Function_儲位亮燈(medClass_藥品搜尋.藥品碼, Color.Black);
                            }
                            medClass_藥品搜尋 = medClasses[0];
                            flag_藥品搜尋 = true;
                        }
                    }
                    else
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無條碼資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                    }
                }
            
                
            }

            if (flag_藥品搜尋)
            {

                this.Invoke(new Action(delegate
                {
                    this.rJ_Lable_藥品搜尋_藥名.Text = $"({medClass_藥品搜尋.藥品碼}){ RemoveParenthesesContent(medClass_藥品搜尋.藥品名稱)}";
                    this.rJ_Lable_藥品搜尋_狀態.BackgroundColor = Color.Green;
                    this.rJ_Lable_藥品搜尋_狀態.Text = "藥品帶入成功";

                }));
                Function_儲位亮燈(medClass_藥品搜尋.藥品碼, Color.Purple);
                System.Threading.Thread.Sleep(2000);

                this.Invoke(new Action(delegate
                {
                    this.rJ_Lable_藥品搜尋_狀態.BackgroundColor = Color.Red;

                    this.rJ_Lable_藥品搜尋_狀態.Text = "請【刷取條碼】或【搜尋藥品】";
                }));
                flag_藥品搜尋 = false;

            }
        }
        private void RJ_Button_藥品搜尋_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_藥品搜尋 dialog_藥品搜尋 = new Dialog_藥品搜尋();
            if (dialog_藥品搜尋.ShowDialog() != DialogResult.Yes) return;

            if (medClass_藥品搜尋 != null)
            {
                Function_儲位亮燈(medClass_藥品搜尋.藥品碼, Color.Black);
            }
            if (flag_藥品搜尋 == false)
            {
                medClass_藥品搜尋 = dialog_藥品搜尋.Value;
                flag_藥品搜尋 = true;
            }
         
            

        }
    }
}
