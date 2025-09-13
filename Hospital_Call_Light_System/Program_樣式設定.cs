using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Basic;
using SQLUI;
using hcls_DB_Lib;

namespace Hospital_Call_Light_System
{
    public partial class Form1 : System.Windows.Forms.Form
    {
      
        private void Program_樣式設定_Init()
        {
            SQL_DataGridView.SQL_Set_Properties(sqL_DataGridView_樣式設定, dBConfigClass.DB_Basic);
            Table table = new Table(new enum_sytle_setting());

            this.sqL_DataGridView_樣式設定.Init(table);
            if (this.sqL_DataGridView_樣式設定.SQL_IsTableCreat() == false) sqL_DataGridView_樣式設定.SQL_CreateTable();
            else sqL_DataGridView_樣式設定.SQL_CheckAllColumnName(true);

            this.button_樣式設定_標題_字體.Click += Button_樣式設定_標題_字體_Click;
            this.button_樣式設定_標題_字體顏色.Click += Button_樣式設定_標題_字體顏色_Click;
            this.button_樣式設定_標題_背景顏色.Click += Button_樣式設定_標題_背景顏色_Click;

            this.button_樣式設定_叫號_字體.Click += Button_樣式設定_叫號_字體_Click;
            this.button_樣式設定_叫號_字體顏色.Click += Button_樣式設定_叫號_字體顏色_Click;
            this.button_樣式設定_叫號_背景顏色.Click += Button_樣式設定_叫號_背景顏色_Click;

            this.button_樣式設定_標題_英文字體.Click += Button_樣式設定_標題_英文字體_Click;
            this.button_樣式設定_叫號_備註字體.Click += Button_樣式設定_叫號_備註字體_Click;


            comboBox_樣式設定_代碼.SelectedIndex = 0;
            comboBox_樣式設定_代碼.SelectedIndexChanged += ComboBox_樣式設定_代碼_SelectedIndexChanged;

            this.plC_RJ_Button_樣式設定_存檔.MouseDownEvent += PlC_RJ_Button_樣式設定_存檔_MouseDownEvent;
            this.plC_RJ_Button_樣式設定_讀取.MouseDownEvent += PlC_RJ_Button_樣式設定_讀取_MouseDownEvent;

            comboBox_公告設定_列高度.SelectedIndex = 0;
            comboBox_公告設定_跑馬速度.SelectedIndex = 0;

            Function_樣式設定表單檢查();

            plC_UI_Init.Add_Method(Program_樣式設定);
        }

        private void Program_樣式設定()
        {

        }
        #region Function
        private void Function_樣式設定表單檢查()
        {
            List<object[]> list_樣式設定 = this.sqL_DataGridView_樣式設定.SQL_GetAllRows(false);
            List<object[]> list_樣式設定_buf = new List<object[]>();
            List<object[]> list_樣式設定_add = new List<object[]>();
            List<object[]> list_樣式設定_delete = new List<object[]>();
            List<object[]> list_樣式設定_replace = new List<object[]>();

            for (int i = 0; i < list_樣式設定.Count; i++)
            {
                int 代碼 = list_樣式設定[i][(int)enum_sytle_setting.代碼].StringToInt32();
                if (代碼 <= 0 || 代碼 > 9)
                {
                    list_樣式設定_delete.Add(list_樣式設定[i]);
                }
            }
            for (int i = 1; i < 10; i++)
            {
                list_樣式設定_buf = list_樣式設定.GetRows((int)enum_sytle_setting.代碼, i.ToString());
                if (list_樣式設定_buf.Count == 0)
                {
                    object[] value = new object[new enum_sytle_setting().GetLength()];

                    value[(int)enum_sytle_setting.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_sytle_setting.代碼] = i.ToString();
                    value[(int)enum_sytle_setting.名稱] = "";
                    value[(int)enum_sytle_setting.台號] = "";
                    value[(int)enum_sytle_setting.標題名稱] = "";
                    value[(int)enum_sytle_setting.標題字體] = new Font("標楷體", 150, FontStyle.Bold).ToFontString();
                    value[(int)enum_sytle_setting.標題文字寬度] = 800;
                    value[(int)enum_sytle_setting.標題字體顏色] = Color.Black.ToColorString();
                    value[(int)enum_sytle_setting.標題背景顏色] = Color.RoyalBlue.ToColorString();
                    value[(int)enum_sytle_setting.標題高度] = 300;

                    value[(int)enum_sytle_setting.叫號號碼] = "0000";
                    value[(int)enum_sytle_setting.叫號字體] = new Font("標楷體", 200, FontStyle.Bold).ToFontString();
                    value[(int)enum_sytle_setting.叫號文字寬度] = 800;
                    value[(int)enum_sytle_setting.叫號字體顏色] = Color.Red.ToColorString();
                    value[(int)enum_sytle_setting.叫號背景顏色] = Color.White.ToColorString();

                    list_樣式設定_add.Add(value);
                }
                else
                {
                    object[] value = list_樣式設定_buf[0];
                    if (value[(int)enum_sytle_setting.英文標題字體].ObjectToString().StringIsEmpty()) value[(int)enum_sytle_setting.英文標題字體] = new Font("標楷體", 80, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_sytle_setting.叫號字體].ObjectToString().StringIsEmpty()) value[(int)enum_sytle_setting.叫號字體] = new Font("標楷體", 200, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_sytle_setting.叫號備註字體].ObjectToString().StringIsEmpty()) value[(int)enum_sytle_setting.叫號備註字體] = new Font("標楷體", 30, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_sytle_setting.標題字體].ObjectToString().StringIsEmpty()) value[(int)enum_sytle_setting.標題字體] = new Font("標楷體", 150, FontStyle.Bold).ToFontString();
                    if (value[(int)enum_sytle_setting.標題高度].StringToInt32() <= 0) value[(int)enum_sytle_setting.標題高度] = 300;
                    if (value[(int)enum_sytle_setting.標題文字寬度].StringToInt32() <= 0) value[(int)enum_sytle_setting.標題文字寬度] = 800;
                    if (value[(int)enum_sytle_setting.叫號文字寬度].StringToInt32() <= 0) value[(int)enum_sytle_setting.叫號文字寬度] = 800;
                    if (value[(int)enum_sytle_setting.叫號備註高度].StringToInt32() <= 0) value[(int)enum_sytle_setting.叫號備註高度] = 100;
                    if (value[(int)enum_sytle_setting.英文標題高度].StringToInt32() <= 0) value[(int)enum_sytle_setting.英文標題高度] = 200;
                    list_樣式設定_replace.Add(value);



                }
            }

            this.sqL_DataGridView_樣式設定.SQL_DeleteExtra(list_樣式設定_delete, false);
            this.sqL_DataGridView_樣式設定.SQL_AddRows(list_樣式設定_add, false);
            this.sqL_DataGridView_樣式設定.SQL_ReplaceExtra(list_樣式設定_replace, false);

        }
        private void Function_樣式設定讀取(string 代碼)
        {
            List<object[]> list_value = sqL_DataGridView_樣式設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_sytle_setting.代碼, 代碼);
            if (list_value.Count > 0)
            {
                this.Invoke(new Action(delegate
                {

                    rJ_TextBox_樣式設定_標題_寬度.Text = list_value[0][(int)enum_sytle_setting.標題文字寬度].ObjectToString();
                    rJ_TextBox_樣式設定_標題_字體.Text = list_value[0][(int)enum_sytle_setting.標題字體].ObjectToString();
                    rJ_TextBox_樣式設定_標題_高度.Text = list_value[0][(int)enum_sytle_setting.標題高度].ObjectToString();
                    rJ_TextBox_樣式設定_標題_字體顏色.Text = list_value[0][(int)enum_sytle_setting.標題字體顏色].ObjectToString();
                    rJ_TextBox_樣式設定_標題_字體顏色.BackColor = rJ_TextBox_樣式設定_標題_字體顏色.Text.ToColor();
                    rJ_TextBox_樣式設定_標題_背景顏色.Text = list_value[0][(int)enum_sytle_setting.標題背景顏色].ObjectToString();
                    rJ_TextBox_樣式設定_標題_背景顏色.BackColor = rJ_TextBox_樣式設定_標題_背景顏色.Text.ToColor();

                    rJ_TextBox_樣式設定_叫號_寬度.Text = list_value[0][(int)enum_sytle_setting.叫號文字寬度].ObjectToString();
                    rJ_TextBox_樣式設定_叫號_字體.Text = list_value[0][(int)enum_sytle_setting.叫號字體].ObjectToString();
                    rJ_TextBox_樣式設定_叫號_字體顏色.Text = list_value[0][(int)enum_sytle_setting.叫號字體顏色].ObjectToString();
                    rJ_TextBox_樣式設定_叫號_字體顏色.BackColor = rJ_TextBox_樣式設定_叫號_字體顏色.Text.ToColor();
                    rJ_TextBox_樣式設定_叫號_背景顏色.Text = list_value[0][(int)enum_sytle_setting.叫號背景顏色].ObjectToString();
                    rJ_TextBox_樣式設定_叫號_背景顏色.BackColor = rJ_TextBox_樣式設定_叫號_背景顏色.Text.ToColor();

                    rJ_TextBox_樣式設定_叫號_邊界距離.Text = list_value[0][(int)enum_sytle_setting.叫號邊界距離].ObjectToString().StringIsInt32() ? list_value[0][(int)enum_sytle_setting.叫號邊界距離].ObjectToString() : "0";
                    rJ_TextBox_樣式設定_叫號_邊框圓角.Text = list_value[0][(int)enum_sytle_setting.叫號邊框圓角].ObjectToString().StringIsInt32() ? list_value[0][(int)enum_sytle_setting.叫號邊框圓角].ObjectToString() : "0";

                    rJ_TextBox_樣式設定_標題_英文字體.Text = list_value[0][(int)enum_sytle_setting.英文標題字體].ObjectToString();
                    rJ_TextBox_樣式設定_標題_英文高度.Text = list_value[0][(int)enum_sytle_setting.英文標題高度].ObjectToString();

                    rJ_TextBox_樣式設定_叫號_備註字體.Text = list_value[0][(int)enum_sytle_setting.叫號備註字體].ObjectToString();
                    rJ_TextBox_樣式設定_叫號_備註高度.Text = list_value[0][(int)enum_sytle_setting.叫號備註高度].ObjectToString();

                }));
            }
        }
        #endregion
        #region Event

        private void PlC_RJ_Button_樣式設定_讀取_MouseDownEvent(MouseEventArgs mevent)
        {
            string 代碼 = "";
            this.Invoke(new Action(delegate
            {
                代碼 = comboBox_樣式設定_代碼.Text;
            }));
            this.Function_樣式設定讀取(代碼);
        }
        private void PlC_RJ_Button_樣式設定_存檔_MouseDownEvent(MouseEventArgs mevent)
        {
            string str_error = "";
            string 代碼 = "";
            this.Invoke(new Action(delegate
            {
                代碼 = comboBox_樣式設定_代碼.Text;
                if (comboBox_樣式設定_代碼.Text.StringIsEmpty())
                {
                    str_error += $"代碼空白!\n";
                }
            }));



            if (rJ_TextBox_樣式設定_標題_寬度.Text.StringToInt32() < 200)
            {
                str_error += $"'標題寬度'不得小於200!\n";
            }
            if (rJ_TextBox_樣式設定_標題_高度.Text.StringToInt32() < 200)
            {
                str_error += $"'標題高度'不得小於200!\n";
            }
            if (rJ_TextBox_樣式設定_標題_字體.Text.StringIsEmpty())
            {
                str_error += $"'標題字體'空白!\n";
            }
            if (rJ_TextBox_樣式設定_標題_字體顏色.Text.StringIsEmpty())
            {
                str_error += $"'標題字體顏色'空白!\n";
            }
            if (rJ_TextBox_樣式設定_標題_背景顏色.Text.StringIsEmpty())
            {
                str_error += $"'標題背景顏色'空白!\n";
            }

            if (rJ_TextBox_樣式設定_叫號_寬度.Text.StringToInt32() < 200)
            {
                str_error += $"'叫號寬度'不得小於200!\n";
            }

            if (rJ_TextBox_樣式設定_叫號_字體.Text.StringIsEmpty())
            {
                str_error += $"'叫號字體'空白!\n";
            }
            if (rJ_TextBox_樣式設定_叫號_字體顏色.Text.StringIsEmpty())
            {
                str_error += $"'叫號字體顏色'空白!\n";
            }
            if (rJ_TextBox_樣式設定_叫號_背景顏色.Text.StringIsEmpty())
            {
                str_error += $"'叫號背景顏色'空白!\n";
            }

            if (rJ_TextBox_樣式設定_標題_英文高度.Text.StringToInt32() <= 0)
            {
                str_error += $"'英文標題高度'不得小於0!\n";
            }
            if (rJ_TextBox_樣式設定_標題_英文字體.Text.StringIsEmpty())
            {
                str_error += $"'英文字體'空白!\n";
            }

            if (rJ_TextBox_樣式設定_叫號_備註高度.Text.StringToInt32() <= 0)
            {
                str_error += $"'備註高度'不得小於0!\n";
            }
            if (rJ_TextBox_樣式設定_叫號_備註字體.Text.StringIsEmpty())
            {
                str_error += $"'備註字體'空白!\n";
            }
            if (rJ_TextBox_樣式設定_叫號_邊界距離.Text.StringToInt32() < 0)
            {
                str_error += $"'叫號邊界距離為'非法文字'\n";
            }
            if (rJ_TextBox_樣式設定_叫號_邊框圓角.Text.StringToInt32() < 0)
            {
                str_error += $"'叫號邊框圓角為'非法文字'\n";
            }
            if (str_error.StringIsEmpty() == false)
            {
                MyMessageBox.ShowDialog(str_error);
                return;
            }
            List<object[]> list_value = sqL_DataGridView_樣式設定.SQL_GetAllRows(false);
            List<object[]> list_value_buf = new List<object[]>();

            string 標題字體 = rJ_TextBox_樣式設定_標題_字體.Text;
            string 標題文字寬度 = rJ_TextBox_樣式設定_標題_寬度.Text;
            string 標題字體顏色 = rJ_TextBox_樣式設定_標題_字體顏色.Text;
            string 標題背景顏色 = rJ_TextBox_樣式設定_標題_背景顏色.Text;
            string 標題高度 = rJ_TextBox_樣式設定_標題_高度.Text;

            string 叫號邊界距離 = rJ_TextBox_樣式設定_叫號_邊界距離.Text;
            string 叫號邊框圓角 = rJ_TextBox_樣式設定_叫號_邊框圓角.Text;
            string 叫號字體 = rJ_TextBox_樣式設定_叫號_字體.Text;
            string 叫號文字寬度 = rJ_TextBox_樣式設定_叫號_寬度.Text;
            string 叫號字體顏色 = rJ_TextBox_樣式設定_叫號_字體顏色.Text;
            string 叫號背景顏色 = rJ_TextBox_樣式設定_叫號_背景顏色.Text;
            string 英文標題高度 = rJ_TextBox_樣式設定_標題_英文高度.Text;
            string 英文標題字體 = rJ_TextBox_樣式設定_標題_英文字體.Text;
            string 備註高度 = rJ_TextBox_樣式設定_叫號_備註高度.Text;
            string 備註字體 = rJ_TextBox_樣式設定_叫號_備註字體.Text;

            

            list_value_buf = list_value.GetRows((int)enum_sytle_setting.代碼, 代碼);
       
            styleSettingClass styleSetting = new styleSettingClass();
            if (list_value_buf.Count == 0)
            {
                styleSetting.GUID = Guid.NewGuid().ToString();
            }
            else
            {
                styleSetting.GUID = list_value_buf[0][(int)enum_sytle_setting.GUID].ObjectToString();
            }
            styleSetting.Code = 代碼;
            styleSetting.Title = new TitleSetting()
            {
                TitleFont = 標題字體,
                TitleTextWidth = 標題文字寬度,
                TitleFontColor = 標題字體顏色,
                TitleBackColor = 標題背景顏色,
                TitleHeight = 標題高度,
                EngTitleHeight = 英文標題高度,
                EngTitleFont = 英文標題字體
            };
            styleSetting.Call = new CallSetting()
            {
                CallFont = 叫號字體,
                CallTextWidth = 叫號文字寬度,
                CallFontColor = 叫號字體顏色,
                CallBackColor = 叫號背景顏色,
                CallMargin = 叫號邊界距離,
                CallBorderRadius = 叫號邊框圓角,
                CallNoteFont = 備註字體,
                CallNoteHeight = 備註高度
            };
            if (list_value_buf.Count == 0)
            {
                styleSetting.Call.CallNumber = "0000";
                object[] value = styleSetting.StyleSettingToObject();
                sqL_DataGridView_樣式設定.SQL_AddRow(value, false);
            }
            else
            {
                object[] value = styleSetting.StyleSettingToObject();
                sqL_DataGridView_樣式設定.SQL_ReplaceExtra(value, false);
            }
              
            MyMessageBox.ShowDialog("存檔成功!");
        }

        private void ComboBox_樣式設定_代碼_SelectedIndexChanged(object sender, EventArgs e)
        {
            Function_樣式設定讀取(comboBox_樣式設定_代碼.Text);
        }
        private void Button_樣式設定_標題_背景顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_樣式設定_標題_背景顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_樣式設定_標題_背景顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_樣式設定_標題_字體顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_樣式設定_標題_字體顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_樣式設定_標題_字體顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_樣式設定_標題_字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_樣式設定_標題_字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_樣式設定_標題_字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_樣式設定_標題_字體.Text = this.fontDialog.Font.ToFontString();
            }
        }
        private void Button_樣式設定_叫號_背景顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_樣式設定_叫號_背景顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_樣式設定_叫號_背景顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_樣式設定_叫號_字體顏色_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_樣式設定_叫號_字體顏色.BackColor = this.colorDialog.Color;
                rJ_TextBox_樣式設定_叫號_字體顏色.Text = this.colorDialog.Color.ToColorString();
            }
        }
        private void Button_樣式設定_叫號_字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_樣式設定_叫號_字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_樣式設定_叫號_字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {

                rJ_TextBox_樣式設定_叫號_字體.Text = this.fontDialog.Font.ToFontString();
            }
        }
        private void Button_樣式設定_標題_英文字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_樣式設定_標題_英文字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_樣式設定_標題_英文字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {

                rJ_TextBox_樣式設定_標題_英文字體.Text = this.fontDialog.Font.ToFontString();
            }
        }
        private void Button_樣式設定_叫號_備註字體_Click(object sender, EventArgs e)
        {
            if (rJ_TextBox_樣式設定_叫號_備註字體.Text.StringIsEmpty() == false) this.fontDialog.Font = rJ_TextBox_樣式設定_叫號_備註字體.Text.ToFont();
            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
                rJ_TextBox_樣式設定_叫號_備註字體.Text = this.fontDialog.Font.ToFontString();
            }
        }
        #endregion
        public class Icp_叫號樣式設定 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 台號_0 = x[(int)enum_sytle_setting.台號].ObjectToString();
                string 台號_1 = y[(int)enum_sytle_setting.台號].ObjectToString();
                return 台號_0.CompareTo(台號_1);
            }
        }
    }
}
