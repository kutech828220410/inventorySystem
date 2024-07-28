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
using DeltaMotor485;
using DrawingClass;
namespace 癌症備藥機
{
    public partial class Main_Form : Form
    {
        private void Program_調配排程_Init()
        {
            this.plC_RJ_Button_調配排程_處方選擇.MouseDownEvent += PlC_RJ_Button_調配排程_處方選擇_MouseDownEvent;
            this.button_調配排程_焦點.Click += Button_調配排程_焦點_Click;
            this.textBox_調配排程_條碼輸入.KeyPress += TextBox_調配排程_條碼輸入_KeyPress;


            this.plC_UI_Init.Add_Method(Program_調配排程);
        }

     

        private void Program_調配排程()
        {

        }
        #region Funtion

        #endregion
        #region Event
        private void PlC_RJ_Button_調配排程_處方選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_備藥通知處方選擇 dialog_備藥通知處方選擇 = new Dialog_備藥通知處方選擇();
            if (dialog_備藥通知處方選擇.ShowDialog() != DialogResult.Yes) return;

            uc_備藥通知內容.Init(dialog_備藥通知處方選擇.udnoectc, 登入者名稱 ,true);
        }
        private void TextBox_調配排程_條碼輸入_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool flag_enter = false;
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    flag_enter = true;
                    if (this.textBox_調配排程_條碼輸入.Text.StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("未輸入條碼資訊");
                        return;
                    }
                    string barcode = this.textBox_調配排程_條碼輸入.Text;
                    barcode = barcode.Replace("\n", "");
                    barcode = barcode.Replace("\r", "");
                    List<object[]> list_藥盒索引 = Main_Form._sqL_DataGridView_藥盒索引.SQL_GetRows((int)enum_drugBoxIndex.barcode, barcode, false);
                    if (list_藥盒索引.Count == 0)
                    {
                        MyMessageBox.ShowDialog("找無藥盒索引");
                        return;
                    }
                    string GUID = list_藥盒索引[0][(int)enum_drugBoxIndex.master_GUID].ObjectToString();
                    udnoectc udnoectc = udnoectc.get_udnoectc_by_GUID(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, GUID);
                    if (udnoectc == null)
                    {
                        MyMessageBox.ShowDialog("找無醫令處方");
                        return;
                    }
                    uc_備藥通知內容.Init(udnoectc, 登入者名稱, true);

                }
            }
            catch
            {

            }
            finally
            {
              if(flag_enter)  this.textBox_調配排程_條碼輸入.Text = "";
            }
         
        }
        private void Button_調配排程_焦點_Click(object sender, EventArgs e)
        {
            textBox_調配排程_條碼輸入.Focus();
        }
        #endregion
    }
}
