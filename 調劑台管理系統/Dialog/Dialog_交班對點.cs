using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
using H_Pannel_lib;


namespace 調劑台管理系統
{
    public partial class Dialog_交班對點 : MyDialog
    {
        public enum enum_交班藥品
        {
            [Description("GUID,VARCHAR,200,NONE")]
            GUID,
            [Description("排列號,VARCHAR,200,NONE")]
            排列號,
            [Description("藥碼,VARCHAR,200,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,200,NONE")]
            藥名,
            [Description("單位,VARCHAR,200,NONE")]
            單位,
            [Description("庫存,VARCHAR,200,NONE")]
            庫存,
            [Description("盤點量,VARCHAR,200,NONE")]
            盤點量,
            [Description("覆盤量,VARCHAR,200,NONE")]
            覆盤量,
            [Description("差異值,VARCHAR,200,NONE")]
            差異值,
            [Description("收支原因,VARCHAR,200,NONE")]
            收支原因,
            [Description("確認時間,VARCHAR,200,NONE")]
            確認時間,
            [Description("備註,VARCHAR,200,NONE")]
            備註,
        }
        static public List<object[]> list_交班對點 = null;
        private bool flag_確認輸入 = false;
        private MyThread myThread_program;
        private personPageClass personPageClass_盤點人員 = null;
        private personPageClass personPageClass_覆盤人員 = null;
        private List<medGroupClass> medGroupClasses = null;
        public Dialog_交班對點()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));

            this.sqL_DataGridView_交班藥品.RowEnterEvent += SqL_DataGridView_交班藥品_RowEnterEvent;
            this.LoadFinishedEvent += Dialog_交班對點_LoadFinishedEvent;
            this.FormClosing += Dialog_交班對點_FormClosing;
            this.rJ_Button_藥品群組_選擇.MouseDownEvent += RJ_Button_藥品群組_選擇_MouseDownEvent;
            this.plC_RJ_Button_解鎖.MouseDownEvent += PlC_RJ_Button_解鎖_MouseDownEvent;
            this.plC_RJ_Button_盤點登入.MouseDownEvent += PlC_RJ_Button_盤點登入_MouseDownEvent;
            this.plC_RJ_Button_覆盤登入.MouseDownEvent += PlC_RJ_Button_覆盤登入_MouseDownEvent;
            this.rJ_Button_確認輸入.MouseDownEvent += RJ_Button_確認輸入_MouseDownEvent;
            this.rJ_Button_0.MouseDownEvent += RJ_Button_0_MouseDownEvent;
            this.rJ_Button_9.MouseDownEvent += RJ_Button_9_MouseDownEvent;
            this.rJ_Button_8.MouseDownEvent += RJ_Button_8_MouseDownEvent;
            this.rJ_Button_7.MouseDownEvent += RJ_Button_7_MouseDownEvent;
            this.rJ_Button_6.MouseDownEvent += RJ_Button_6_MouseDownEvent;
            this.rJ_Button_5.MouseDownEvent += RJ_Button_5_MouseDownEvent;
            this.rJ_Button_4.MouseDownEvent += RJ_Button_4_MouseDownEvent;
            this.rJ_Button_3.MouseDownEvent += RJ_Button_3_MouseDownEvent;
            this.rJ_Button_2.MouseDownEvent += RJ_Button_2_MouseDownEvent;
            this.rJ_Button_1.MouseDownEvent += RJ_Button_1_MouseDownEvent;
            this.rJ_Button_CE.MouseDownEvent += RJ_Button_CE_MouseDownEvent;

            this.plC_RJ_Button_確認送出.MouseDownEvent += PlC_RJ_Button_確認送出_MouseDownEvent;
            this.plC_RJ_Button_重新盤點.MouseDownEvent += PlC_RJ_Button_重新盤點_MouseDownEvent;
          

        }

       

        private void RJ_Button_CE_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = "";
            }));
        }
        private void RJ_Button_1_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}1".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_2_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}2".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_3_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}3".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_4_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}4".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_5_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}5".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_6_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}6".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_7_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}7".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_8_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}8".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_9_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}9".StringToInt32().ToString();
            }));
        }
        private void RJ_Button_0_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (rJ_TextBox_盤點量.StringToInt32() != 0)
                {
                    rJ_TextBox_盤點量.Text = $"{rJ_TextBox_盤點量.Text}0".StringToInt32().ToString(); ;
                }         
            }));
        }

        #region Function
        private void sub_program()
        {
            try
            {
                if (this.stepViewer.CurrentStep == 1)
                {
                    this.Invoke(new Action(delegate
                    {
                        plC_RJ_Button_盤點登入.Enabled = true;
                        plC_RJ_Button_覆盤登入.Enabled = false;
                        panel_藥品選擇.Enabled = false;
                        panel_交班內容.Enabled = false;
                    }));
                }
                if (this.stepViewer.CurrentStep == 2)
                {
                    this.Invoke(new Action(delegate
                    {
                        plC_RJ_Button_盤點登入.Enabled = false;
                        plC_RJ_Button_覆盤登入.Enabled = true;
                        panel_藥品選擇.Enabled = false;
                        panel_交班內容.Enabled = false;
                    }));


                }
                if (this.stepViewer.CurrentStep == 3)
                {
                    if (list_交班對點 != null)
                    {
                        int index = 0;
                        for (int i = 0; i < list_交班對點.Count; i++)
                        {
                            if (list_交班對點[i][(int)enum_交班藥品.盤點量].ObjectToString().StringIsEmpty())
                            {
                                index = i;
                                break;
                            }
                        }
                        this.sqL_DataGridView_交班藥品.SetSelectRow(index);
                        this.stepViewer.Next();
                        PLC_Device_交班對點.Bool = true;
                    }
                    this.Invoke(new Action(delegate
                    {
                        plC_RJ_Button_盤點登入.Enabled = false;
                        plC_RJ_Button_覆盤登入.Enabled = false;
                        panel_藥品選擇.Enabled = true;
                        panel_交班內容.Enabled = false;
                    }));
                }
                if (this.stepViewer.CurrentStep == 4)
                {
                    this.Invoke(new Action(delegate
                    {
                        plC_RJ_Button_盤點登入.Enabled = false;
                        plC_RJ_Button_覆盤登入.Enabled = false;
                        panel_藥品選擇.Enabled = false;
                        panel_交班內容.Enabled = true;
                    }));
                }
                if (this.stepViewer.CurrentStep == 5)
                {
                    this.Invoke(new Action(delegate
                    {
                        plC_RJ_Button_確認送出.Enabled = true;
                    }));
                }
                
                sub_Program_交班對點();
            }
            catch
            {

            }
   
        }
        #region PLC_交班對點
        int retry = 0;
        PLC_Device PLC_Device_交班對點 = new PLC_Device("");
        PLC_Device PLC_Device_交班對點_OK = new PLC_Device("");
        Task Task_交班對點;
        MyTimer MyTimer_交班對點_結束延遲 = new MyTimer();
        MyTimer MyTimer_交班對點_開始延遲 = new MyTimer();
        int cnt_Program_交班對點 = 65534;
        void sub_Program_交班對點()
        {
         
            if (cnt_Program_交班對點 == 65534)
            {
                this.MyTimer_交班對點_結束延遲.StartTickTime(10000);
                this.MyTimer_交班對點_開始延遲.StartTickTime(10000);
                PLC_Device_交班對點.SetComment("PLC_交班對點");
                PLC_Device_交班對點_OK.SetComment("PLC_交班對點_OK");
                PLC_Device_交班對點.Bool = false;
                cnt_Program_交班對點 = 65535;
            }
            if (this.stepViewer.CurrentStep != 4) return;
            if (cnt_Program_交班對點 == 65535) cnt_Program_交班對點 = 1;
            if (cnt_Program_交班對點 == 1) cnt_Program_交班對點_檢查按下(ref cnt_Program_交班對點);
            if (cnt_Program_交班對點 == 2) cnt_Program_交班對點_初始化(ref cnt_Program_交班對點);
            if (cnt_Program_交班對點 == 3) cnt_Program_交班對點_等待輸入盤點數量(ref cnt_Program_交班對點);
            if (cnt_Program_交班對點 == 4) cnt_Program_交班對點_選擇下一筆(ref cnt_Program_交班對點);
            if (cnt_Program_交班對點 == 5) cnt_Program_交班對點 = 65500;
            if (cnt_Program_交班對點 > 1) cnt_Program_交班對點_檢查放開(ref cnt_Program_交班對點);

            if (cnt_Program_交班對點 == 65500)
            {
                this.MyTimer_交班對點_結束延遲.TickStop();
                this.MyTimer_交班對點_結束延遲.StartTickTime(10000);
                PLC_Device_交班對點.Bool = false;
                PLC_Device_交班對點_OK.Bool = false;
                cnt_Program_交班對點 = 65535;
            }
        }
        void cnt_Program_交班對點_檢查按下(ref int cnt)
        {
            if (PLC_Device_交班對點.Bool) cnt++;
        }
        void cnt_Program_交班對點_檢查放開(ref int cnt)
        {
            if (!PLC_Device_交班對點.Bool) cnt = 65500;
        }
        void cnt_Program_交班對點_初始化(ref int cnt)
        {
            this.Invoke(new Action(delegate 
            {
                rJ_TextBox_盤點量.Text = "";
                rJ_Lable_狀態.Text = "請輸入【盤點】數量";
            }));
            cnt++;
        }
        void cnt_Program_交班對點_等待輸入盤點數量(ref int cnt)
        {
            if(flag_確認輸入)
            {
                flag_確認輸入 = false;
                int 盤點量 = rJ_TextBox_盤點量.Text.StringToInt32();
                int 庫存 = 0;
                if (盤點量.StringToInt32() < 0)
                {
                    MyMessageBox.ShowDialog("未輸入盤點值");
                    this.Invoke(new Action(delegate
                    {
                        rJ_TextBox_盤點量.Text = "";
                    }));
                    return;
                }
                List<object[]> list_交班對點 = this.sqL_DataGridView_交班藥品.Get_All_Select_RowsValues();
                if (list_交班對點.Count == 0)
                {
                    MyMessageBox.ShowDialog("交班表選擇異常");
                    cnt = 65534;
                    return;
                }
                list_交班對點[0][(int)enum_交班藥品.盤點量] = 盤點量.StringToInt32();
                庫存 = list_交班對點[0][(int)enum_交班藥品.庫存].StringToInt32();
                
                if (盤點量 != 庫存)
                {
                    if (retry == 0)
                    {
                        MyMessageBox.ShowDialog("盤點量與庫存不符");
                        this.Invoke(new Action(delegate
                        {
                            rJ_TextBox_盤點量.Text = "";
                            rJ_Lable_狀態.Text = "請輸入【覆盤】數量";
                        }));
                        retry++;
                        return;
                    }
                    if (retry == 1)
                    {
                        if (MyMessageBox.ShowDialog($"盤點量與庫存不符,是否繼續下一步驟", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                        Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                        if (dialog_收支原因選擇.ShowDialog() != DialogResult.Yes) return;
                        list_交班對點[0][(int)enum_交班藥品.收支原因] = dialog_收支原因選擇.Value;
                    }

                }
                int 差異值 = 盤點量 - 庫存;
                list_交班對點[0][(int)enum_交班藥品.差異值] = 差異值;
                list_交班對點[0][(int)enum_交班藥品.確認時間] = DateTime.Now.ToDateTimeString_6();

                this.Invoke(new Action(delegate
                {
                    retry = 0;
                    rJ_TextBox_盤點量.Text = "";
                    rJ_Lable_狀態.Text = "-----------";
                }));
                this.sqL_DataGridView_交班藥品.ReplaceExtra(list_交班對點, true);
                cnt++;
            }
        }
        void cnt_Program_交班對點_等待輸入覆盤數量(ref int cnt)
        {
            if (flag_確認輸入)
            {
                flag_確認輸入 = false;
                int 覆盤量 = rJ_TextBox_盤點量.Text.StringToInt32();
                int 庫存 = 0;
                if (覆盤量.StringToInt32() < 0)
                {
                    MyMessageBox.ShowDialog("未輸入覆盤值");
                    this.Invoke(new Action(delegate
                    {
                        rJ_TextBox_盤點量.Text = "";
                    }));
                    return;
                }
                List<object[]> list_交班對點 = this.sqL_DataGridView_交班藥品.Get_All_Select_RowsValues();
                if (list_交班對點.Count == 0)
                {
                    MyMessageBox.ShowDialog("交班表選擇異常");
                    cnt = 65534;
                    return;
                }
                list_交班對點[0][(int)enum_交班藥品.覆盤量] = 覆盤量.StringToInt32();
                庫存 = list_交班對點[0][(int)enum_交班藥品.庫存].StringToInt32();

                if (覆盤量 != 庫存)
                {
                    if (retry == 0)
                    {
                        MyMessageBox.ShowDialog("覆盤量與庫存不符");
                        this.Invoke(new Action(delegate
                        {
                            rJ_TextBox_盤點量.Text = "";
                            rJ_Lable_狀態.Text = "請輸入【覆盤】數量";
                        }));
                        retry++;
                        return;
                    }
                    if (retry == 1)
                    {
                        if (MyMessageBox.ShowDialog($"覆盤量與庫存不符,是否繼續下一步驟", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                    }

                }
                this.Invoke(new Action(delegate
                {
                    retry = 0;
                    rJ_TextBox_盤點量.Text = "";
                    rJ_Lable_狀態.Text = "請輸入【覆盤】數量";
                }));
                this.sqL_DataGridView_交班藥品.ReplaceExtra(list_交班對點, true);
                cnt++;
            }
        }
        void cnt_Program_交班對點_選擇下一筆(ref int cnt)
        { 
            int selectRow =  this.sqL_DataGridView_交班藥品.GetSelectRow();
            List<object[]> list_value = this.sqL_DataGridView_交班藥品.Get_All_Select_RowsValues();
            if(list_value.Count > 0)
            {
                string 藥碼 = list_value[0][(int)enum_交班藥品.藥碼].ObjectToString();
                Main_Form.Function_儲位亮燈(new Main_Form.LightOn(藥碼, Color.Black));
            }
            
            if (selectRow == this.sqL_DataGridView_交班藥品.GetAllRows().Count - 1)
            {
                this.stepViewer.Next();
                cnt++;
                return;
            }
            this.Invoke(new Action(delegate
            {
                this.sqL_DataGridView_交班藥品.SetSelectRow(selectRow + 1);
            }));

            PLC_Device_交班對點.Bool = true;
            cnt = 1;
            return;
        }
        #endregion
        private void Function_寫入交易紀錄(string 備註)
        {
            List<object[]> list_交班對點 = this.sqL_DataGridView_交班藥品.GetAllRows();
            if (備註.StringIsEmpty())
            {
                List<medRecheckLogClass> medRecheckLogClasses = new List<medRecheckLogClass>();
                for (int i = 0; i < list_交班對點.Count; i++)
                {
                    if (list_交班對點[i][(int)enum_交班藥品.差異值].ObjectToString().StringIsDouble() == false) continue;
                    if (list_交班對點[i][(int)enum_交班藥品.差異值].StringToInt32() != 0)
                    {
                        medRecheckLogClass medRecheckLogClass = new medRecheckLogClass();
                        medRecheckLogClass.發生類別 = "交班對點";
                        medRecheckLogClass.藥碼 = list_交班對點[i][(int)enum_交班藥品.藥碼].ObjectToString();
                        medRecheckLogClass.藥名 = list_交班對點[i][(int)enum_交班藥品.藥名].ObjectToString();
                        medRecheckLogClass.庫存值 = list_交班對點[i][(int)enum_交班藥品.庫存].ObjectToString();
                        medRecheckLogClass.盤點值 = list_交班對點[i][(int)enum_交班藥品.盤點量].ObjectToString();
                        medRecheckLogClass.盤點藥師1 = personPageClass_盤點人員.姓名;
                        medRecheckLogClass.盤點藥師ID1 = personPageClass_盤點人員.ID;
                        medRecheckLogClass.盤點藥師2 = personPageClass_覆盤人員.姓名;
                        medRecheckLogClass.盤點藥師ID2 = personPageClass_覆盤人員.ID;
                        medRecheckLogClass.異常原因 = list_交班對點[i][(int)enum_交班藥品.收支原因].ObjectToString();

                        medRecheckLogClasses.Add(medRecheckLogClass);
                    }

                }
                medRecheckLogClass.add(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, medRecheckLogClasses);
            }
         

            List<transactionsClass> transactionsClasses = new List<transactionsClass>();
            for (int i = 0; i < list_交班對點.Count; i++)
            {
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.動作 = enum_交易記錄查詢動作.交班對點.GetEnumName();
                transactionsClass.藥品碼 = list_交班對點[i][(int)enum_交班藥品.藥碼].ObjectToString();
                transactionsClass.藥品名稱 = list_交班對點[i][(int)enum_交班藥品.藥名].ObjectToString();
                transactionsClass.庫存量 = list_交班對點[i][(int)enum_交班藥品.庫存].ObjectToString();
                transactionsClass.盤點量 = list_交班對點[i][(int)enum_交班藥品.盤點量].ObjectToString();
                transactionsClass.操作人 = personPageClass_盤點人員.姓名;
                transactionsClass.覆核藥師 = personPageClass_覆盤人員.姓名;
                transactionsClass.開方時間 = list_交班對點[i][(int)enum_交班藥品.確認時間].ObjectToString();
                transactionsClass.操作時間 = list_交班對點[i][(int)enum_交班藥品.確認時間].ObjectToString();
                transactionsClass.備註 = 備註;
                if (transactionsClass.盤點量.ObjectToString().StringIsEmpty()) continue;

                transactionsClasses.Add(transactionsClass);
            }

            transactionsClass.add(Main_Form.API_Server, transactionsClasses, Main_Form.ServerName, Main_Form.ServerType);
        }
        #endregion
        #region Event
        private void SqL_DataGridView_交班藥品_RowEnterEvent(object[] RowValue)
        {
            if (RowValue != null)
            {
                string 藥碼 = RowValue[(int)enum_交班藥品.藥碼].ObjectToString();
                string 藥名 = RowValue[(int)enum_交班藥品.藥名].ObjectToString();
                string 庫存 = RowValue[(int)enum_交班藥品.庫存].ObjectToString();
                this.Invoke(new Action(delegate
                {

                    List<Image> images = medPicClass.get_images_by_code(Main_Form.API_Server, 藥碼);
                    if (images == null)
                    {
                        pictureBox_藥品資訊.Image = null;
                        return;
                    }
                    pictureBox_藥品資訊.Image = images[0];
                    this.rJ_Lable_藥品資訊.Text = $"({藥碼}){藥名}";
                    this.rJ_Lable_現有庫存.Text = $"{庫存}";
                }));

                if (CodeLast.StringIsEmpty() == false)
                {
                    Main_Form.Function_儲位亮燈(new Main_Form.LightOn(CodeLast, Color.Black));
                }
                Main_Form.Function_儲位亮燈(new Main_Form.LightOn(藥碼, Color.Blue));
                CodeLast = 藥碼;
            }


        }
        private void Dialog_交班對點_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Yes)
            {
                Function_寫入交易紀錄("盤點中斷");
                list_交班對點 = this.sqL_DataGridView_交班藥品.GetAllRows();
                for (int i = 0; i < list_交班對點.Count; i++)
                {
                    string 藥碼 = list_交班對點[i][(int)enum_交班藥品.藥碼].ObjectToString();
                    Main_Form.Function_儲位亮燈(new Main_Form.LightOn(藥碼, Color.Black));
                }
              
            }
            else
            {
                list_交班對點 = null;
            }
        }
        private void Dialog_交班對點_LoadFinishedEvent(EventArgs e)
        {
            List<StepEntity> list = new List<StepEntity>();
            list.Add(new StepEntity("1", "登入", 1, "請登入使用者(盤點)", eumStepState.Waiting, null));
            list.Add(new StepEntity("2", "登入", 2, "請登入使用者(覆盤)", eumStepState.Waiting, null));
            list.Add(new StepEntity("3", "藥品選擇", 3, "選擇交班藥品", eumStepState.Waiting, null));
            list.Add(new StepEntity("4", "清點藥品", 4, "清點交班藥品", eumStepState.Waiting, null));
            list.Add(new StepEntity("5", "確認送出", 5, "交班表紀錄送出", eumStepState.Waiting, null));
            this.stepViewer.CurrentStep = 1;
            this.stepViewer.ListDataSource = list;


            medGroupClasses = medGroupClass.get_all_group(Main_Form.API_Server);
            List<string> list_str = new List<string>();
            for (int i = 0; i < medGroupClasses.Count; i++)
            {
                list_str.Add(medGroupClasses[i].名稱);
            }
            this.comboBox_藥品群組.DataSource = list_str.ToArray();
            if (list_str.Count > 0)
            {
                this.comboBox_藥品群組.SelectedIndex = 0;
            }
            this.Invoke(new Action(delegate 
            {
                Table table_交班藥品 = new Table(new enum_交班藥品());
                this.sqL_DataGridView_交班藥品.Init(table_交班藥品);
                this.sqL_DataGridView_交班藥品.Set_ColumnVisible(false, new enum_交班藥品().GetEnumNames());
                //this.sqL_DataGridView_交班藥品.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.排列號);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.藥碼);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_交班藥品.藥名);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.單位);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.盤點量);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.差異值);
                this.sqL_DataGridView_交班藥品.ClearGrid();
            }));

            if (list_交班對點 != null)
            {
                if (MyMessageBox.ShowDialog("有交班表未完成,是否繼續盤點?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    this.sqL_DataGridView_交班藥品.RefreshGrid(list_交班對點);
                }
                else
                {
                    list_交班對點 = null;
                }
            }

            myThread_program = new MyThread();
            myThread_program.Add_Method(sub_program);
            myThread_program.AutoRun(true);
            myThread_program.SetSleepTime(100);
            myThread_program.Trigger();
        }
        string CodeLast = "";

        private void RJ_Button_藥品群組_選擇_MouseDownEvent(MouseEventArgs mevent)
        {

            string text = "";
            this.Invoke(new Action(delegate
            {
                text = this.comboBox_藥品群組.Text;
            }));

            List<medGroupClass> medGroupClasses_buf = (from temp in medGroupClasses
                                                       where temp.名稱 == text
                                                       select temp).ToList();
            List<DeviceBasic> devices_buf = new List<DeviceBasic>();
            if (medGroupClasses_buf.Count > 0)
            {
                List<object[]> list_value = new List<object[]>();
                List<object[]> list_value_buf = new List<object[]>();
                List<string> list_IP = new List<string>();
                for (int i = 0; i < medGroupClasses_buf[0].MedClasses.Count; i++)
                {

                    medClass medClass = medGroupClasses_buf[0].MedClasses[i];
                    object[] value = new object[new enum_交班藥品().GetLength()];
                    value[(int)enum_交班藥品.GUID] = medClass.GUID;
                    value[(int)enum_交班藥品.排列號] = Main_Form.Function_從SQL取得排列號(medClass.藥品碼).ToString();
                    value[(int)enum_交班藥品.藥碼] = medClass.藥品碼;
                    value[(int)enum_交班藥品.藥名] = medClass.藥品名稱;
                    value[(int)enum_交班藥品.單位] = medClass.包裝單位;
                    int 差異值 = medRecheckLogClass.get_unresolved_qty_by_code(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, medClass.藥品碼);
                    double 庫存 = Main_Form.Function_從SQL取得庫存(medClass.藥品碼);
                    value[(int)enum_交班藥品.庫存] = 差異值 + 庫存;
                    list_IP.LockAdd(Main_Form.Function_取得抽屜以藥品碼解鎖IP(medClass.藥品碼));
                    list_value.Add(value);
                }

                list_IP = (from temp in list_IP
                           select temp).Distinct().ToList();

                for (int i = 0; i < list_IP.Count; i++)
                {
                    List<string> IPs = new List<string>();
                    IPs.LockAdd(list_IP[i]);
                    Main_Form.Function_抽屜解鎖(IPs);
                    System.Threading.Thread.Sleep(100);
                }

                list_value.Sort(new ICP_交班藥品());
                this.sqL_DataGridView_交班藥品.RefreshGrid(list_value);
                this.Invoke(new Action(delegate
                {
                    this.sqL_DataGridView_交班藥品.SetSelectRow(0);
                }));
                list_交班對點 = this.sqL_DataGridView_交班藥品.GetAllRows();
                PLC_Device_交班對點.Bool = true;
                this.stepViewer.Next();
            }

        }

        private void PlC_RJ_Button_盤點登入_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_使用者登入 dialog_使用者登入 = new Dialog_使用者登入();
            if (dialog_使用者登入.ShowDialog() != DialogResult.Yes) return;
            personPageClass_盤點人員 = dialog_使用者登入.personPageClass;
            this.stepViewer.Next();
            this.Invoke(new Action(delegate
            {
                rJ_Lable_盤點人員.Text = $"盤點人員 : {personPageClass_盤點人員.姓名}";
                rJ_Lable_盤點人員.BackgroundColor = Color.YellowGreen;
            }));
        }
        private void PlC_RJ_Button_覆盤登入_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_使用者登入 dialog_使用者登入 = new Dialog_使用者登入(personPageClass_盤點人員.ID);
            if (dialog_使用者登入.ShowDialog() != DialogResult.Yes) return;
            personPageClass_覆盤人員 = dialog_使用者登入.personPageClass;
            this.stepViewer.Next();
            this.Invoke(new Action(delegate
            {
                rJ_Lable_覆盤人員.Text = $"覆盤人員 : {personPageClass_覆盤人員.姓名}";
                rJ_Lable_覆盤人員.BackgroundColor = Color.YellowGreen;
            }));
        }
        private void RJ_Button_確認輸入_MouseDownEvent(MouseEventArgs mevent)
        {
            flag_確認輸入 = true;
        }
        private void PlC_RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("確認送出交班表?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            Function_寫入交易紀錄("");
          
            this.DialogResult = DialogResult.Yes;
            this.Close();

        }
        private void PlC_RJ_Button_解鎖_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_交班對點 = this.sqL_DataGridView_交班藥品.Get_All_Select_RowsValues();
            if (list_交班對點.Count == 0)
            {
                MyMessageBox.ShowDialog("未選擇交班藥品");
                return;
            }
            string Code = list_交班對點[0][(int)enum_交班藥品.藥碼].ObjectToString();
            Main_Form.Function_抽屜以藥品碼解鎖(Code);
        }
        private void PlC_RJ_Button_重新盤點_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否重新開始盤點?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            this.stepViewer.CurrentStep = 1;
            this.sqL_DataGridView_交班藥品.ClearGrid();
            list_交班對點 = null;
        }
        #endregion


        public class ICP_交班藥品 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {

                int 排列號0 = x[(int)enum_交班藥品.排列號].StringToInt32();
                int 排列號1 = y[(int)enum_交班藥品.排列號].StringToInt32();
                return 排列號0.CompareTo(排列號1);
            }
        }
    }
}
