using System;
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

namespace Hospital_Call_Light_System
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        
      
        private void Program_設定_Init()
        {
            
            this.rJ_TextBox_第一台_加一號.KeyPress += RJ_TextBox_第一台_加一號_KeyPress;
            this.rJ_TextBox_第一台_減一號.KeyPress += RJ_TextBox_第一台_減一號_KeyPress;
            this.rJ_TextBox_第一台_加二號.KeyPress += RJ_TextBox_第一台_加二號_KeyPress;
            this.rJ_TextBox_第一台_減二號.KeyPress += RJ_TextBox_第一台_減二號_KeyPress;
            this.rJ_TextBox_第一台_加十號.KeyPress += RJ_TextBox_第一台_加十號_KeyPress;
            this.rJ_TextBox_第一台_減十號.KeyPress += RJ_TextBox_第一台_減十號_KeyPress;

            this.rJ_TextBox_第二台_加一號.KeyPress += RJ_TextBox_第二台_加一號_KeyPress;
            this.rJ_TextBox_第二台_減一號.KeyPress += RJ_TextBox_第二台_減一號_KeyPress;
            this.rJ_TextBox_第二台_加二號.KeyPress += RJ_TextBox_第二台_加二號_KeyPress;
            this.rJ_TextBox_第二台_減二號.KeyPress += RJ_TextBox_第二台_減二號_KeyPress;
            this.rJ_TextBox_第二台_加十號.KeyPress += RJ_TextBox_第二台_加十號_KeyPress;
            this.rJ_TextBox_第二台_減十號.KeyPress += RJ_TextBox_第二台_減十號_KeyPress;

     

            this.plC_RJ_Button_叫號內容設定_刪除.MouseDownEvent += PlC_RJ_Button_叫號內容設定_刪除_MouseDownEvent;
            this.plC_RJ_Button_叫號內容設定_登錄.MouseDownEvent += PlC_RJ_Button_叫號內容設定_登錄_MouseDownEvent;

            this.plC_RJ_Button_按鈕設定_存檔.MouseDownEvent += PlC_RJ_Button_按鈕設定_存檔_MouseDownEvent;

   
        }

      

        #region Function



        #endregion
        #region Event
 
        private void RJ_TextBox_第一台_加一號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第一台_減一號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第一台_減二號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第一台_加二號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void RJ_TextBox_第一台_減十號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第一台_加十號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第二台_加一號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第二台_減一號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第二台_減二號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第二台_加二號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void RJ_TextBox_第二台_減十號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RJ_TextBox_第二台_加十號_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }



        private void PlC_RJ_Button_按鈕設定_存檔_MouseDownEvent(MouseEventArgs mevent)
        {
            myConfigClass.第一台加一號 = this.rJ_TextBox_第一台_加一號.Text;
            myConfigClass.第一台減一號 = this.rJ_TextBox_第一台_減一號.Text;
            myConfigClass.第一台加二號 = this.rJ_TextBox_第一台_加二號.Text;
            myConfigClass.第一台減二號 = this.rJ_TextBox_第一台_減二號.Text;
            myConfigClass.第一台加十號 = this.rJ_TextBox_第一台_加十號.Text;
            myConfigClass.第一台減十號 = this.rJ_TextBox_第一台_減十號.Text;


            myConfigClass.第二台加一號 = this.rJ_TextBox_第二台_加一號.Text;
            myConfigClass.第二台減一號 = this.rJ_TextBox_第二台_減一號.Text;
            myConfigClass.第二台加二號 = this.rJ_TextBox_第二台_加二號.Text;
            myConfigClass.第二台減二號 = this.rJ_TextBox_第二台_減二號.Text;
            myConfigClass.第二台加十號 = this.rJ_TextBox_第二台_加十號.Text;
            myConfigClass.第二台減十號 = this.rJ_TextBox_第二台_減十號.Text;
            string jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass, true);
            List<string> list_jsonstring = new List<string>();
            list_jsonstring.Add(jsonstr);
            if (!MyFileStream.SaveFile($"{MyConfigFileName}", list_jsonstring))
            {
                MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                return;
            }
            MyMessageBox.ShowDialog("完成!");
        }
        #endregion
    }
}
