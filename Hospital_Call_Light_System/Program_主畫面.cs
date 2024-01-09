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
        int 叫號台01_號碼 = 0;
        int 叫號台02_號碼 = 0;

        private void Program_主畫面()
        {
            this.button_第一台號碼輸入.Click += Button_第一台號碼輸入_Click;
            this.button_第二台號碼輸入.Click += Button_第二台號碼輸入_Click;
            this.plC_RJ_Button_刷新螢幕.MouseDownEvent += PlC_RJ_Button_刷新螢幕_MouseDownEvent;
            this.plC_RJ_Button_檢查按鈕.MouseDownEvent += PlC_RJ_Button_檢查按鈕_MouseDownEvent;
            this.plC_RJ_Button_刷新音效.MouseDownEvent += PlC_RJ_Button_刷新音效_MouseDownEvent;


            this.button_主畫面_存檔.Click += Button_主畫面_存檔_Click;
            comboBox_一號台名稱.Click += ComboBox_一號台名稱_Click; ;
            comboBox_二號台名稱.Click += ComboBox_二號台名稱_Click;



            ComboBox_一號台名稱_Click(null, null);
            ComboBox_二號台名稱_Click(null, null);
            this.comboBox_一號台名稱.Text = myConfigClass.一號台名稱;
            this.comboBox_二號台名稱.Text = myConfigClass.二號台名稱;
            checkBox_1號台顯示.Checked = myConfigClass.第一台顯示;
            checkBox_2號台顯示.Checked = myConfigClass.第二台顯示;

            Dialog_小叫號台.EnrterNum01Event += Dialog_小叫號台_EnrterNum01Event;
            Dialog_小叫號台.EnrterNum02Event += Dialog_小叫號台_EnrterNum02Event;
            this.plC_UI_Init.Add_Method(sub_Program_主畫面);
        }

   

        private void sub_Program_主畫面()
        {
            sub_Program_刷新螢幕();
            sub_Program_刷新音效();
        }

        private Bitmap Function_主畫面_取得文字Bitmap(string 標題名稱, Font 標題字體, Size 標題大小, int 標題文字寬度, Color 標題字體顏色, Color 標題背景顏色)
        {
            try
            {
                Bitmap bitmap = new Bitmap(標題大小.Width, 標題大小.Height);
                using (Graphics g_bmp = Graphics.FromImage(bitmap))
                {

                    DrawingClass.Draw.方框繪製(new PointF(0, 0), bitmap.Size, Color.Transparent, 2, true, g_bmp, 1, 1);
                    DrawingClass.Draw.方框繪製(new PointF(0, 0), new Size(bitmap.Width - 2, bitmap.Height), 標題背景顏色, 1, true, g_bmp, 1, 1);
                    Size size_font = TextRenderer.MeasureText(標題名稱, 標題字體);
                    int x = (標題大小.Width - 標題文字寬度) / 2;
                    int y = (bitmap.Height - size_font.Height) / 2;


                    DrawingClass.Draw.文字左上繪製(標題名稱, 標題文字寬度, new PointF(x, y), 標題字體, 標題字體顏色, 標題背景顏色, g_bmp);
                }
                return bitmap;
            }
            catch
            {
                return null;
            }
       
        }
        private Bitmap Function_主畫面_取得英文文字Bitmap(string 標題名稱, Font 標題字體, Size 標題大小, int 標題文字寬度, Color 標題字體顏色, Color 標題背景顏色)
        {
            try
            {
                Bitmap bitmap = new Bitmap(標題大小.Width, 標題大小.Height);
                using (Graphics g_bmp = Graphics.FromImage(bitmap))
                {

                    DrawingClass.Draw.方框繪製(new PointF(0, 0), bitmap.Size, Color.Transparent, 2, true, g_bmp, 1, 1);
                    DrawingClass.Draw.方框繪製(new PointF(0, 0), new Size(bitmap.Width - 2, bitmap.Height), 標題背景顏色, 1, true, g_bmp, 1, 1);
                    Size size_font = TextRenderer.MeasureText(標題名稱, 標題字體);
                    int x = (標題大小.Width - 標題文字寬度) / 2;
                    int y = (bitmap.Height - size_font.Height) / 2;


                    DrawingClass.Draw.文字左上繪製(標題名稱, 標題文字寬度, new PointF(x, y), 標題字體, 標題字體顏色, 標題背景顏色, g_bmp);
                }
                return bitmap;
            }
            catch
            {
                return null;
            }

        }
        #region Event
        private void ComboBox_二號台名稱_Click(object sender, EventArgs e)
        {
            comboBox_二號台名稱.Items.Clear();
            List<string> list_str = Function_取得叫號名稱();
            for (int i = 0; i < list_str.Count; i++)
            {
                comboBox_二號台名稱.Items.Add(list_str[i]);
            }
        }
        private void ComboBox_一號台名稱_Click(object sender, EventArgs e)
        {
            comboBox_一號台名稱.Items.Clear();
            List<string> list_str = Function_取得叫號名稱();
            for(int i = 0; i < list_str.Count; i++)
            {
                comboBox_一號台名稱.Items.Add(list_str[i]);
            }
           
        }

        private void Button_主畫面_存檔_Click(object sender, EventArgs e)
        {
            myConfigClass.一號台名稱 = this.comboBox_一號台名稱.Text;
            myConfigClass.二號台名稱 = this.comboBox_二號台名稱.Text;
            myConfigClass.第一台顯示 = checkBox_1號台顯示.Checked;
            myConfigClass.第二台顯示 = checkBox_2號台顯示.Checked;
            myConfigClass.全局音效 = checkBox_全局音效.Checked;
            myConfigClass.本地音效 = checkBox_本地音效.Checked;

            string jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass, true);
            List<string> list_jsonstring = new List<string>();
            list_jsonstring.Add(jsonstr);
            if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
            {
                MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                return;
            }
            MyMessageBox.ShowDialog("完成!");

        }
        private void PlC_RJ_Button_刷新音效_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.myConfigClass.全局音效)
            {
                System.Media.SoundPlayer sp = null;
                List<object[]> list_value = this.sqL_DataGridView_參數.SQL_GetAllRows(false);
                list_value = list_value.GetRows((int)enum_參數.Name, "音效");
                if (list_value.Count > 0)
                {
                    object[] value = list_value[0];
                  

                    if (value[(int)enum_參數.Value].ObjectToString().ToUpper() == true.ToString().ToUpper())
                    {
                        try
                        {

                            sp = new System.Media.SoundPlayer(".//RING.wav");
                            sp.Stop();

                            sp.Play();

                        }
                        finally
                        {
                            if (sp != null) sp.Dispose();
                        }
                    }

                    value[(int)enum_參數.Name] = "音效";
                    value[(int)enum_參數.Value] = "false";
                    this.sqL_DataGridView_參數.SQL_ReplaceExtra(value, false);
                }
            }
        }
        private void PlC_RJ_Button_刷新螢幕_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                using (Graphics g = this.panel_叫號.CreateGraphics())
                {

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //使繪圖質量最高，即消除鋸齒
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                    string 一號台名稱 = "";
                    string 二號台名稱 = "";

                    this.Invoke(new Action(delegate 
                    {
                        一號台名稱 = comboBox_一號台名稱.Text;
                        二號台名稱 = comboBox_二號台名稱.Text;
                    }));
                    int width = this.panel_叫號.Width;
                    int height = this.panel_叫號.Height;
                    Bitmap bitmap_標題_0 = null;
                    Bitmap bitmap_英文標題_0 = null;
                    Bitmap bitmap_叫號_0 = null;
                    Bitmap bitmap_叫號備註_0 = null;
                    Bitmap bitmap_標題_1 = null;
                    Bitmap bitmap_英文標題_1 = null;
                    Bitmap bitmap_叫號_1 = null;
                    Bitmap bitmap_叫號備註_1 = null;
                    List<object[]> list_叫號台設定 = this.sqL_DataGridView_叫號台設定.SQL_GetAllRows(false);
                    List<object[]> list_叫號台設定_buf = new List<object[]>();
                    List<object[]> list_樣式設定 = this.sqL_DataGridView_樣式設定.SQL_GetAllRows(false);
                    List<object[]> list_樣式設定_buf = new List<object[]>();

                    bool flag_RING = false;
                    for (int i = 0; i < list_樣式設定.Count; i++)
                    {
                        if (i == 0)
                        {
                            list_叫號台設定_buf = list_叫號台設定.GetRows((int)enum_叫號台設定.名稱, 一號台名稱);
                            if (list_叫號台設定_buf.Count == 0) continue;
                            list_樣式設定_buf = list_樣式設定.GetRows((int)enum_樣式設定.代碼, list_叫號台設定_buf[0][(int)enum_叫號台設定.樣式代碼].ObjectToString());
                            if (list_樣式設定_buf.Count == 0) continue;
                        }
                        if (i == 1)
                        {
                            list_叫號台設定_buf = list_叫號台設定.GetRows((int)enum_叫號台設定.名稱, 二號台名稱);
                            if (list_叫號台設定_buf.Count == 0) continue;
                            list_樣式設定_buf = list_樣式設定.GetRows((int)enum_樣式設定.代碼, list_叫號台設定_buf[0][(int)enum_叫號台設定.樣式代碼].ObjectToString());
                            if (list_樣式設定_buf.Count == 0) continue;
                        }
                        if (list_叫號台設定_buf.Count == 0) continue;
                        int 寬度 = (width - 0) / 2;
                        int temp = 0;
                        if (checkBox_1號台顯示.Checked) temp++;
                        if (checkBox_2號台顯示.Checked) temp++;

                        if (temp == 1) 寬度 = (width - 0) / 1;

                        string 標題名稱 = list_叫號台設定_buf[0][(int)enum_叫號台設定.名稱].ObjectToString();
                        Font 標題字體 = list_樣式設定_buf[0][(int)enum_樣式設定.標題字體].ObjectToString().ToFont();
                        int 標題文字寬度 = list_樣式設定_buf[0][(int)enum_樣式設定.標題文字寬度].StringToInt32();
                        Color 標題字體顏色 = list_樣式設定_buf[0][(int)enum_樣式設定.標題字體顏色].ObjectToString().ToColor();
                        Color 標題背景顏色 = list_樣式設定_buf[0][(int)enum_樣式設定.標題背景顏色].ObjectToString().ToColor();
                        int 標題高度 = list_樣式設定_buf[0][(int)enum_樣式設定.標題高度].StringToInt32();

                        string 英文標題名稱 = list_叫號台設定_buf[0][(int)enum_叫號台設定.英文名].ObjectToString();
                        Font 英文標題字體 = list_樣式設定_buf[0][(int)enum_樣式設定.英文標題字體].ObjectToString().ToFont();
                        int 英文標題高度 = list_樣式設定_buf[0][(int)enum_樣式設定.英文標題高度].StringToInt32();

                        string 叫號名稱 = list_樣式設定_buf[0][(int)enum_樣式設定.叫號號碼].ObjectToString();
                        Font 叫號字體 = list_樣式設定_buf[0][(int)enum_樣式設定.叫號字體].ObjectToString().ToFont();
                        int 叫號文字寬度 = list_樣式設定_buf[0][(int)enum_樣式設定.叫號文字寬度].StringToInt32();
                        Color 叫號字體顏色 = list_樣式設定_buf[0][(int)enum_樣式設定.叫號字體顏色].ObjectToString().ToColor();
                        Color 叫號背景顏色 = list_樣式設定_buf[0][(int)enum_樣式設定.叫號背景顏色].ObjectToString().ToColor();
                   

                        string 叫號備註 = list_叫號台設定_buf[0][(int)enum_叫號台設定.叫號備註].ObjectToString();

                        Font 叫號備註字體 = list_樣式設定_buf[0][(int)enum_樣式設定.叫號備註字體].ObjectToString().ToFont();
                        int 叫號備註高度 = list_樣式設定_buf[0][(int)enum_樣式設定.叫號備註高度].StringToInt32();

                        int 叫號高度 = height - (標題高度 + 英文標題高度 + 叫號備註高度);
                        if (i == 0 && checkBox_1號台顯示.Checked)
                        {
                            if (叫號台01_號碼 != list_叫號台設定_buf[0][(int)enum_叫號台設定.號碼].StringToInt32())
                            {
                                flag_RING = true;
                            }
                            叫號台01_號碼 = list_叫號台設定_buf[0][(int)enum_叫號台設定.號碼].StringToInt32();
                            bitmap_標題_0 = Function_主畫面_取得文字Bitmap(標題名稱, 標題字體, new Size(寬度, 標題高度), 標題文字寬度, 標題字體顏色, 標題背景顏色);
                            bitmap_英文標題_0 = Function_主畫面_取得英文文字Bitmap(英文標題名稱, 英文標題字體, new Size(寬度, 英文標題高度), 標題文字寬度, 標題字體顏色, 標題背景顏色);
                            bitmap_叫號_0 = Function_主畫面_取得文字Bitmap(叫號台01_號碼.ToString("0000"), 叫號字體, new Size(寬度, 叫號高度), 叫號文字寬度, 叫號字體顏色, 叫號背景顏色);
                            bitmap_叫號備註_0 = Function_主畫面_取得文字Bitmap(叫號備註, 叫號備註字體, new Size(寬度, 叫號備註高度), 叫號文字寬度, 叫號字體顏色, 叫號背景顏色);
                        }
                        if (i == 1 && checkBox_2號台顯示.Checked)
                        {
                            if (叫號台02_號碼 != list_叫號台設定_buf[0][(int)enum_叫號台設定.號碼].StringToInt32())
                            {
                                flag_RING = true;
                            }

                            叫號台02_號碼 = list_叫號台設定_buf[0][(int)enum_叫號台設定.號碼].StringToInt32();
                            bitmap_標題_1 = Function_主畫面_取得文字Bitmap(標題名稱, 標題字體, new Size(寬度, 標題高度), 標題文字寬度, 標題字體顏色, 標題背景顏色);
                            bitmap_英文標題_1 = Function_主畫面_取得英文文字Bitmap(英文標題名稱, 英文標題字體, new Size(寬度, 英文標題高度), 標題文字寬度, 標題字體顏色, 標題背景顏色);
                            bitmap_叫號_1 = Function_主畫面_取得文字Bitmap(叫號台02_號碼.ToString("0000"), 叫號字體, new Size(寬度, 叫號高度), 叫號文字寬度, 叫號字體顏色, 叫號背景顏色);
                            bitmap_叫號備註_1 = Function_主畫面_取得文字Bitmap(叫號備註, 叫號備註字體, new Size(寬度, 叫號備註高度), 叫號文字寬度, 叫號字體顏色, 叫號背景顏色);
                        }



                    }
                    int tota_width = 0;
                    if (bitmap_標題_0 != null) tota_width += bitmap_標題_0.Width;
                    if (bitmap_標題_1 != null) tota_width += bitmap_標題_1.Width;
                    int posx = (width - tota_width) / 2;
                    if (bitmap_標題_0 != null)
                    {
                        g.DrawImage(bitmap_標題_0, new PointF(posx, 0));
                        g.DrawImage(bitmap_英文標題_0, new PointF(posx, bitmap_標題_0.Height));
                        g.DrawImage(bitmap_叫號_0, new PointF(posx, bitmap_標題_0.Height + bitmap_英文標題_0.Height));
                        g.DrawImage(bitmap_叫號備註_0, new PointF(posx, bitmap_標題_0.Height + bitmap_英文標題_0.Height + bitmap_叫號_0.Height));
                    }
                    if (bitmap_標題_1 != null)
                    {
                        g.DrawImage(bitmap_標題_1, new PointF(posx + bitmap_標題_1.Width, 0));
                        g.DrawImage(bitmap_英文標題_1, new PointF(posx + bitmap_標題_1.Width, bitmap_標題_1.Height));
                        g.DrawImage(bitmap_叫號_1, new PointF(posx + bitmap_標題_1.Width, bitmap_標題_1.Height + bitmap_英文標題_1.Height));
                        g.DrawImage(bitmap_叫號備註_1, new PointF(posx + bitmap_標題_1.Width, bitmap_標題_1.Height + bitmap_英文標題_1.Height + bitmap_叫號_1.Height));
                    }
                    if (flag_RING)
                    {
                        List<object[]> list_value = this.sqL_DataGridView_參數.SQL_GetAllRows(false);
                        list_value = list_value.GetRows((int)enum_參數.Name, "音效");
                        if (list_value.Count == 0)
                        {
                            object[] value = new object[new enum_參數().GetLength()];
                            value[(int)enum_參數.GUID] = Guid.NewGuid().ToString();
                            value[(int)enum_參數.Name] = "音效";
                            value[(int)enum_參數.Value] = "true";
                            this.sqL_DataGridView_參數.SQL_AddRow(value, false);
                        }
                        else
                        {
                            object[] value = list_value[0];
                            value[(int)enum_參數.Name] = "音效";
                            value[(int)enum_參數.Value] = "true";
                            this.sqL_DataGridView_參數.SQL_ReplaceExtra(value, false);
                        }
                       
                        System.Media.SoundPlayer sp = null;
                        Dialog_小叫號台.Refresh(叫號台01_號碼, 叫號台02_號碼);
                        if(this.myConfigClass.本地音效)
                        {
                            try
                            {

                                sp = new System.Media.SoundPlayer(".//RING.wav");
                                sp.Stop();

                                sp.Play();

                            }
                            finally
                            {
                                if (sp != null) sp.Dispose();
                            }
                        }
                 
                    }
          

                }
            }
            catch
            {

            }
          
        }
        private void PlC_RJ_Button_檢查按鈕_MouseDownEvent(MouseEventArgs mevent)
        {

        }
        private void Button_第一台號碼輸入_Click(object sender, EventArgs e)
        {
            string 機台名稱 = this.comboBox_一號台名稱.Text;
            List<object[]> list_value = this.sqL_DataGridView_叫號台設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_叫號台設定.名稱, 機台名稱);
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("找無資料!");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            list_value[0][(int)enum_叫號台設定.號碼] = dialog_NumPannel.Value.ToString("0000");
            this.sqL_DataGridView_叫號台設定.SQL_ReplaceExtra(list_value, false);
        }
        private void Button_第二台號碼輸入_Click(object sender, EventArgs e)
        {
            string 機台名稱 = this.comboBox_二號台名稱.Text;
            List<object[]> list_value = this.sqL_DataGridView_叫號台設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_叫號台設定.名稱, 機台名稱);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("找無資料!");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            list_value[0][(int)enum_叫號台設定.號碼] = dialog_NumPannel.Value.ToString("0000");
            this.sqL_DataGridView_叫號台設定.SQL_ReplaceExtra(list_value, false);
        }
        private void PlC_RJ_Button_全螢幕顯示_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_螢幕選擇 dialog_螢幕選擇 = new Dialog_螢幕選擇();

            this.Invoke(new Action(delegate
            {
                if (dialog_螢幕選擇.ShowDialog() == DialogResult.Yes)
                {

                    try
                    {
                        Dialog_小叫號台.ShowForm();
                        Dialog_小叫號台.Refresh(叫號台01_號碼, 叫號台02_號碼);


                        Basic.Screen.FullScreen(this.FindForm(), dialog_螢幕選擇.Value, true);
                        Basic.Screen.FullScreen(this.FindForm(), dialog_螢幕選擇.Value, false);
                        List<Task> taskList = new List<Task>();
                        taskList.Add(Task.Run(() =>
                        {

                            System.Threading.Thread.Sleep(500);
                            this.Invoke(new Action(delegate
                            {
                                Basic.Screen.FullScreen(this.FindForm(), dialog_螢幕選擇.Value, true);
                            }));
                        }));


                        panel_Main.Visible = false;
                        Application.DoEvents();

                        this.全螢幕 = true;
                    }
                    catch
                    {
                        Basic.Screen.FullScreen(this.FindForm(), 0, false);
                        MyMessageBox.ShowDialog("找無此螢幕!");
                    }
                }
            }));


        }
        private void Dialog_小叫號台_EnrterNum02Event()
        {
            string 機台名稱 = this.comboBox_二號台名稱.Text;
            List<object[]> list_value = this.sqL_DataGridView_叫號台設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_叫號台設定.名稱, 機台名稱);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("找無資料!");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            list_value[0][(int)enum_叫號台設定.號碼] = dialog_NumPannel.Value.ToString("0000");
            this.sqL_DataGridView_叫號台設定.SQL_ReplaceExtra(list_value, false);
        }
        private void Dialog_小叫號台_EnrterNum01Event()
        {
            string 機台名稱 = this.comboBox_一號台名稱.Text;
            List<object[]> list_value = this.sqL_DataGridView_叫號台設定.SQL_GetAllRows(false);
            list_value = list_value.GetRows((int)enum_叫號台設定.名稱, 機台名稱);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("找無資料!");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            list_value[0][(int)enum_叫號台設定.號碼] = dialog_NumPannel.Value.ToString("0000");
            this.sqL_DataGridView_叫號台設定.SQL_ReplaceExtra(list_value, false);
        }
        #endregion

        #region PLC_刷新螢幕
        PLC_Device PLC_Device_刷新螢幕 = new PLC_Device("");
        PLC_Device PLC_Device_刷新螢幕_OK = new PLC_Device("");
        Task Task_刷新螢幕;
        MyTimer MyTimer_刷新螢幕_結束延遲 = new MyTimer();
        int cnt_Program_刷新螢幕 = 65534;
        void sub_Program_刷新螢幕()
        {
            if (plC_ScreenPage_Main.PageText == "主畫面") PLC_Device_刷新螢幕.Bool = true;
            if (cnt_Program_刷新螢幕 == 65534)
            {
                this.MyTimer_刷新螢幕_結束延遲.StartTickTime(500);
                PLC_Device_刷新螢幕.SetComment("PLC_刷新螢幕");
                PLC_Device_刷新螢幕_OK.SetComment("PLC_刷新螢幕_OK");
                PLC_Device_刷新螢幕.Bool = false;
                cnt_Program_刷新螢幕 = 65535;
            }
            if (cnt_Program_刷新螢幕 == 65535) cnt_Program_刷新螢幕 = 1;
            if (cnt_Program_刷新螢幕 == 1) cnt_Program_刷新螢幕_檢查按下(ref cnt_Program_刷新螢幕);
            if (cnt_Program_刷新螢幕 == 2) cnt_Program_刷新螢幕_初始化(ref cnt_Program_刷新螢幕);
            if (cnt_Program_刷新螢幕 == 3) cnt_Program_刷新螢幕 = 65500;
            if (cnt_Program_刷新螢幕 > 1) cnt_Program_刷新螢幕_檢查放開(ref cnt_Program_刷新螢幕);

            if (cnt_Program_刷新螢幕 == 65500)
            {
                this.MyTimer_刷新螢幕_結束延遲.TickStop();
                this.MyTimer_刷新螢幕_結束延遲.StartTickTime(500);
                PLC_Device_刷新螢幕.Bool = false;
                PLC_Device_刷新螢幕_OK.Bool = false;
                cnt_Program_刷新螢幕 = 65535;
            }
        }
        void cnt_Program_刷新螢幕_檢查按下(ref int cnt)
        {
            if (PLC_Device_刷新螢幕.Bool) cnt++;
        }
        void cnt_Program_刷新螢幕_檢查放開(ref int cnt)
        {
            if (!PLC_Device_刷新螢幕.Bool) cnt = 65500;
        }
        void cnt_Program_刷新螢幕_初始化(ref int cnt)
        {
            if (this.MyTimer_刷新螢幕_結束延遲.IsTimeOut())
            {
                if (Task_刷新螢幕 == null)
                {
                    Task_刷新螢幕 = new Task(new Action(delegate { PlC_RJ_Button_刷新螢幕_MouseDownEvent(null); }));
                }
                if (Task_刷新螢幕.Status == TaskStatus.RanToCompletion)
                {
                    Task_刷新螢幕 = new Task(new Action(delegate { PlC_RJ_Button_刷新螢幕_MouseDownEvent(null); }));
                }
                if (Task_刷新螢幕.Status == TaskStatus.Created)
                {
                    Task_刷新螢幕.Start();
                }
                cnt++;
            }
        }







        #endregion
        #region PLC_刷新音效
        PLC_Device PLC_Device_刷新音效 = new PLC_Device("");
        PLC_Device PLC_Device_刷新音效_OK = new PLC_Device("");
        Task Task_刷新音效;
        MyTimer MyTimer_刷新音效_結束延遲 = new MyTimer();
        int cnt_Program_刷新音效 = 65534;
        void sub_Program_刷新音效()
        {
            if (plC_ScreenPage_Main.PageText == "主畫面") PLC_Device_刷新音效.Bool = true;
            if (cnt_Program_刷新音效 == 65534)
            {
                this.MyTimer_刷新音效_結束延遲.StartTickTime(500);
                PLC_Device_刷新音效.SetComment("PLC_刷新音效");
                PLC_Device_刷新音效_OK.SetComment("PLC_刷新音效_OK");
                PLC_Device_刷新音效.Bool = false;
                cnt_Program_刷新音效 = 65535;
            }
            if (cnt_Program_刷新音效 == 65535) cnt_Program_刷新音效 = 1;
            if (cnt_Program_刷新音效 == 1) cnt_Program_刷新音效_檢查按下(ref cnt_Program_刷新音效);
            if (cnt_Program_刷新音效 == 2) cnt_Program_刷新音效_初始化(ref cnt_Program_刷新音效);
            if (cnt_Program_刷新音效 == 3) cnt_Program_刷新音效 = 65500;
            if (cnt_Program_刷新音效 > 1) cnt_Program_刷新音效_檢查放開(ref cnt_Program_刷新音效);

            if (cnt_Program_刷新音效 == 65500)
            {
                this.MyTimer_刷新音效_結束延遲.TickStop();
                this.MyTimer_刷新音效_結束延遲.StartTickTime(500);
                PLC_Device_刷新音效.Bool = false;
                PLC_Device_刷新音效_OK.Bool = false;
                cnt_Program_刷新音效 = 65535;
            }
        }
        void cnt_Program_刷新音效_檢查按下(ref int cnt)
        {
            if (PLC_Device_刷新音效.Bool) cnt++;
        }
        void cnt_Program_刷新音效_檢查放開(ref int cnt)
        {
            if (!PLC_Device_刷新音效.Bool) cnt = 65500;
        }
        void cnt_Program_刷新音效_初始化(ref int cnt)
        {
            if (this.MyTimer_刷新音效_結束延遲.IsTimeOut())
            {
                if (Task_刷新音效 == null)
                {
                    Task_刷新音效 = new Task(new Action(delegate { PlC_RJ_Button_刷新音效_MouseDownEvent(null); }));
                }
                if (Task_刷新音效.Status == TaskStatus.RanToCompletion)
                {
                    Task_刷新音效 = new Task(new Action(delegate { PlC_RJ_Button_刷新音效_MouseDownEvent(null); }));
                }
                if (Task_刷新音效.Status == TaskStatus.Created)
                {
                    Task_刷新音效.Start();
                }
                cnt++;
            }
        }







        #endregion
    }
}
