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
            

            this.button_第一台_標題_字體.Click += Button_第一台_標題_字體_Click;
            this.button_第一台_標題_字體顏色.Click += Button_第一台_標題_字體顏色_Click;
            this.button_第一台_標題_背景顏色.Click += Button_第一台_標題_背景顏色_Click;

            this.button_第一台_叫號_字體.Click += Button_第一台_叫號_字體_Click;
            this.button_第一台_叫號_字體顏色.Click += Button_第一台_叫號_字體顏色_Click;
            this.button_第一台_叫號_背景顏色.Click += Button_第一台_叫號_背景顏色_Click;

            this.button_第一台_標題_英文字體.Click += Button_第一台_標題_英文字體_Click;
            this.button_第一台_叫號_備註字體.Click += Button_第一台_叫號_備註字體_Click;

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

            this.plC_RJ_Button_第一台_存檔.MouseDownEvent += PlC_RJ_Button_第一台_存檔_MouseDownEvent;
            this.plC_RJ_Button_設定_讀取.MouseDownEvent += PlC_RJ_Button_設定_讀取_MouseDownEvent;

            this.plC_RJ_Button_叫號內容設定_刪除.MouseDownEvent += PlC_RJ_Button_叫號內容設定_刪除_MouseDownEvent;
            this.plC_RJ_Button_叫號內容設定_登錄.MouseDownEvent += PlC_RJ_Button_叫號內容設定_登錄_MouseDownEvent;

            this.plC_RJ_Button_按鈕設定_存檔.MouseDownEvent += PlC_RJ_Button_按鈕設定_存檔_MouseDownEvent;

            comboBox_代碼.SelectedIndex = 0;
            comboBox_代碼.SelectedIndexChanged += ComboBox_代碼_SelectedIndexChanged;

        }


        #region Function
        private void Function_設定讀取(string 代碼)
        {
            Function_第一台設定讀取(代碼);
        }
    
        private void Function_第一台設定讀取(string 代碼)
        {
            List<object[]> list_value = sqL_DataGridView_樣式設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_樣式設定.代碼, 代碼);
            if (list_value.Count > 0)
            {
                this.Invoke(new Action(delegate
                {
   
                    rJ_TextBox_第一台_標題_寬度.Text = list_value[0][(int)enum_樣式設定.標題文字寬度].ObjectToString();
                    rJ_TextBox_第一台_標題_字體.Text = list_value[0][(int)enum_樣式設定.標題字體].ObjectToString();
                    rJ_TextBox_第一台_標題_高度.Text = list_value[0][(int)enum_樣式設定.標題高度].ObjectToString();
                    rJ_TextBox_第一台_標題_字體顏色.Text = list_value[0][(int)enum_樣式設定.標題字體顏色].ObjectToString();
                    rJ_TextBox_第一台_標題_字體顏色.BackColor = rJ_TextBox_第一台_標題_字體顏色.Text.ToColor();
                    rJ_TextBox_第一台_標題_背景顏色.Text = list_value[0][(int)enum_樣式設定.標題背景顏色].ObjectToString();
                    rJ_TextBox_第一台_標題_背景顏色.BackColor = rJ_TextBox_第一台_標題_背景顏色.Text.ToColor();

                    rJ_TextBox_第一台_叫號_寬度.Text = list_value[0][(int)enum_樣式設定.叫號文字寬度].ObjectToString();
                    rJ_TextBox_第一台_叫號_字體.Text = list_value[0][(int)enum_樣式設定.叫號字體].ObjectToString();
                    rJ_TextBox_第一台_叫號_字體顏色.Text = list_value[0][(int)enum_樣式設定.叫號字體顏色].ObjectToString();
                    rJ_TextBox_第一台_叫號_字體顏色.BackColor = rJ_TextBox_第一台_叫號_字體顏色.Text.ToColor();
                    rJ_TextBox_第一台_叫號_背景顏色.Text = list_value[0][(int)enum_樣式設定.叫號背景顏色].ObjectToString();
                    rJ_TextBox_第一台_叫號_背景顏色.BackColor = rJ_TextBox_第一台_叫號_背景顏色.Text.ToColor();

                    rJ_TextBox_第一台_標題_英文字體.Text = list_value[0][(int)enum_樣式設定.英文標題字體].ObjectToString();
                    rJ_TextBox_第一台_標題_英文高度.Text = list_value[0][(int)enum_樣式設定.英文標題高度].ObjectToString();

                    rJ_TextBox_第一台_叫號_備註字體.Text = list_value[0][(int)enum_樣式設定.叫號備註字體].ObjectToString();
                    rJ_TextBox_第一台_叫號_備註高度.Text = list_value[0][(int)enum_樣式設定.叫號備註高度].ObjectToString();

                }));
            }
        }
     
        #endregion
        #region Event
     

        private void ComboBox_代碼_SelectedIndexChanged(object sender, EventArgs e)
        {
            Function_第一台設定讀取(comboBox_代碼.Text);
        }
        private void PlC_RJ_Button_設定_讀取_MouseDownEvent(MouseEventArgs mevent)
        {

            string 代碼 = "";
            this.Invoke(new Action(delegate
            {
                代碼 = comboBox_代碼.Text;
            }));
            this.Function_第一台設定讀取(代碼);
        }

        private void Button_第一台_標題_背景顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_第一台_標題_背景顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_第一台_標題_背景顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_第一台_標題_字體顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_第一台_標題_字體顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_第一台_標題_字體顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_第一台_標題_字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_第一台_標題_字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_第一台_標題_字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
         
                rJ_TextBox_第一台_標題_字體.Text = this.fontDialog.Font.ToFontString();
            }
        }
        private void Button_第一台_叫號_背景顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_第一台_叫號_背景顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_第一台_叫號_背景顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_第一台_叫號_字體顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_第一台_叫號_字體顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_第一台_叫號_字體顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_第一台_叫號_字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_第一台_叫號_字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_第一台_叫號_字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
     
                rJ_TextBox_第一台_叫號_字體.Text = this.fontDialog.Font.ToFontString();
            }
        }
        private void Button_第一台_標題_英文字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_第一台_標題_英文字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_第一台_標題_英文字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
              
                rJ_TextBox_第一台_標題_英文字體.Text = this.fontDialog.Font.ToFontString();
            }
        }

        private void Button_第一台_叫號_備註字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_第一台_叫號_備註字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_第一台_叫號_備註字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {

                rJ_TextBox_第一台_叫號_備註字體.Text = this.fontDialog.Font.ToFontString();
            }
        }
    
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
        private void PlC_RJ_Button_第一台_存檔_MouseDownEvent(MouseEventArgs mevent)
        {
            string str_error = "";
            string 代碼 = "";
            this.Invoke(new Action(delegate
            {
                代碼 = comboBox_代碼.Text;
                if (comboBox_代碼.Text.StringIsEmpty())
                {
                    str_error += $"代碼空白!\n";
                }
            }));

          

            if (rJ_TextBox_第一台_標題_寬度.Text.StringToInt32() < 200)
            {
                str_error += $"'標題寬度'不得小於200!\n";
            }
            if (rJ_TextBox_第一台_標題_高度.Text.StringToInt32() < 200)
            {
                str_error += $"'標題高度'不得小於200!\n";
            }
            if (rJ_TextBox_第一台_標題_字體.Text.StringIsEmpty())
            {
                str_error += $"'標題字體'空白!\n";
            }
            if (rJ_TextBox_第一台_標題_字體顏色.Text.StringIsEmpty())
            {
                str_error += $"'標題字體顏色'空白!\n";
            }
            if (rJ_TextBox_第一台_標題_背景顏色.Text.StringIsEmpty())
            {
                str_error += $"'標題背景顏色'空白!\n";
            }

            if (rJ_TextBox_第一台_叫號_寬度.Text.StringToInt32() < 200)
            {
                str_error += $"'叫號寬度'不得小於200!\n";
            }

            if (rJ_TextBox_第一台_叫號_字體.Text.StringIsEmpty())
            {
                str_error += $"'叫號字體'空白!\n";
            }
            if (rJ_TextBox_第一台_叫號_字體顏色.Text.StringIsEmpty())
            {
                str_error += $"'叫號字體顏色'空白!\n";
            }
            if (rJ_TextBox_第一台_叫號_背景顏色.Text.StringIsEmpty())
            {
                str_error += $"'叫號背景顏色'空白!\n";
            }

            if (rJ_TextBox_第一台_標題_英文高度.Text.StringToInt32() <= 0)
            {
                str_error += $"'英文標題高度'不得小於0!\n";
            }
            if (rJ_TextBox_第一台_標題_英文字體.Text.StringIsEmpty())
            {
                str_error += $"'英文字體'空白!\n";
            }

            if (rJ_TextBox_第一台_叫號_備註高度.Text.StringToInt32() <= 0)
            {
                str_error += $"'備註高度'不得小於0!\n";
            }
            if (rJ_TextBox_第一台_叫號_備註字體.Text.StringIsEmpty())
            {
                str_error += $"'備註字體'空白!\n";
            }
            if (str_error.StringIsEmpty() == false)
            {
                MyMessageBox.ShowDialog(str_error);
                return;
            }
            List<object[]> list_value = sqL_DataGridView_樣式設定.SQL_GetAllRows(false);
            List<object[]> list_value_buf = new List<object[]>();

            string 標題字體 = rJ_TextBox_第一台_標題_字體.Text;
            string 標題文字寬度 = rJ_TextBox_第一台_標題_寬度.Text;
            string 標題字體顏色 = rJ_TextBox_第一台_標題_字體顏色.Text;
            string 標題背景顏色 = rJ_TextBox_第一台_標題_背景顏色.Text;
            string 標題高度 = rJ_TextBox_第一台_標題_高度.Text;

            string 叫號字體 = rJ_TextBox_第一台_叫號_字體.Text;
            string 叫號文字寬度 = rJ_TextBox_第一台_叫號_寬度.Text;
            string 叫號字體顏色 = rJ_TextBox_第一台_叫號_字體顏色.Text;
            string 叫號背景顏色 = rJ_TextBox_第一台_叫號_背景顏色.Text;
            string 英文標題高度 = rJ_TextBox_第一台_標題_英文高度.Text;
            string 英文標題字體 = rJ_TextBox_第一台_標題_英文字體.Text;
            string 備註高度 = rJ_TextBox_第一台_叫號_備註高度.Text;
            string 備註字體 = rJ_TextBox_第一台_叫號_備註字體.Text;



            list_value_buf = list_value.GetRows((int)enum_樣式設定.代碼, 代碼);
            object[] value = new object[new enum_樣式設定().GetLength()];

            if (list_value_buf.Count == 0)
            {
                value[(int)enum_樣式設定.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_樣式設定.代碼] = 代碼;
                value[(int)enum_樣式設定.標題字體] = 標題字體;
                value[(int)enum_樣式設定.標題文字寬度] = 標題文字寬度;
                value[(int)enum_樣式設定.標題字體顏色] = 標題字體顏色;
                value[(int)enum_樣式設定.標題背景顏色] = 標題背景顏色;
                value[(int)enum_樣式設定.標題高度] = 標題高度;

                value[(int)enum_樣式設定.叫號號碼] = "0000";
                value[(int)enum_樣式設定.叫號字體] = 叫號字體;
                value[(int)enum_樣式設定.叫號文字寬度] = 叫號文字寬度;
                value[(int)enum_樣式設定.叫號字體顏色] = 叫號字體顏色;
                value[(int)enum_樣式設定.叫號背景顏色] = 叫號背景顏色;
                value[(int)enum_樣式設定.英文標題高度] = 英文標題高度;
                value[(int)enum_樣式設定.英文標題字體] = 英文標題字體;
                value[(int)enum_樣式設定.叫號備註高度] = 備註高度;
                value[(int)enum_樣式設定.叫號備註字體] = 備註字體;


                sqL_DataGridView_樣式設定.SQL_AddRow(value, false);
            }
            else
            {
                value = list_value_buf[0];
                value[(int)enum_樣式設定.代碼] = 代碼;
                value[(int)enum_樣式設定.標題字體] = 標題字體;
                value[(int)enum_樣式設定.標題文字寬度] = 標題文字寬度;
                value[(int)enum_樣式設定.標題字體顏色] = 標題字體顏色;
                value[(int)enum_樣式設定.標題背景顏色] = 標題背景顏色;
                value[(int)enum_樣式設定.標題高度] = 標題高度;

                value[(int)enum_樣式設定.叫號號碼] = "0000";
                value[(int)enum_樣式設定.叫號字體] = 叫號字體;
                value[(int)enum_樣式設定.叫號文字寬度] = 叫號文字寬度;
                value[(int)enum_樣式設定.叫號字體顏色] = 叫號字體顏色;
                value[(int)enum_樣式設定.叫號背景顏色] = 叫號背景顏色;

                value[(int)enum_樣式設定.英文標題高度] = 英文標題高度;
                value[(int)enum_樣式設定.英文標題字體] = 英文標題字體;
                value[(int)enum_樣式設定.叫號備註高度] = 備註高度;
                value[(int)enum_樣式設定.叫號備註字體] = 備註字體;
                sqL_DataGridView_樣式設定.SQL_ReplaceExtra(value, false);

            }
            MyMessageBox.ShowDialog("存檔成功!");
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
