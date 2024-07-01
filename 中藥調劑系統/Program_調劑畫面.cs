﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;
namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        private object[] order_value = null;
        private object[] order_current_row = null;
        private List<OrderTClass> OrderTClass_現在調劑處方 = null;
        private bool flag_新處方提示 = false;
        private MyThread myThread_更新處方;
        private int orderDate = 0;
        public enum enum_處方內容
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("應調,VARCHAR,15,NONE")]
            應調,
            [Description("實調,VARCHAR,15,NONE")]
            實調,
            [Description("天,VARCHAR,15,NONE")]
            天,
            [Description("頻次,VARCHAR,15,NONE")]
            頻次,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
            [Description("服用方法,VARCHAR,15,NONE")]
            服用方法,
        }
        public enum enum_病患資訊
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("PRI_KEY,VARCHAR,15,NONE")]
            PRI_KEY,
            [Description("領藥號,VARCHAR,15,NONE")]
            領藥號,    
            [Description("姓名,VARCHAR,15,NONE")]
            姓名,
            [Description("年齡,VARCHAR,15,NONE")]
            年齡,
            [Description("性別,VARCHAR,15,NONE")]
            性別,
            [Description("病歷號,VARCHAR,15,NONE")]
            病歷號,
            [Description("處方類型,VARCHAR,15,NONE")]
            處方類型,
            [Description("處方日期,VARCHAR,15,NONE")]
            處方日期,
            [Description("調劑完成,VARCHAR,15,NONE")]
            調劑完成,
        }
        public static sessionClass sessionClass = new sessionClass();
        private void Program_調劑畫面_Init()
        {
            Table table_處方內容 = new Table(new enum_處方內容());
            this.sqL_DataGridView_處方內容.Init(table_處方內容);
            this.sqL_DataGridView_處方內容.Set_ColumnVisible(false, new enum_處方內容().GetEnumNames());
            //this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, "藥碼");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, "藥名");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "應調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "實調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, "天");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, "頻次");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, "單位");
            this.sqL_DataGridView_處方內容.RowEnterEvent += SqL_DataGridView_處方內容_RowEnterEvent;
            this.sqL_DataGridView_處方內容.DataGridRefreshEvent += SqL_DataGridView_處方內容_DataGridRefreshEvent;
            this.sqL_DataGridView_處方內容.DataGridClearGridEvent += SqL_DataGridView_處方內容_DataGridClearGridEvent;

            Table table_病患資訊 = new Table(new enum_病患資訊());
            this.sqL_DataGridView_病患資訊.Init(table_病患資訊);
            this.sqL_DataGridView_病患資訊.Set_ColumnVisible(false, new enum_病患資訊().GetEnumNames());
            this.sqL_DataGridView_病患資訊.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, "領藥號");
            this.sqL_DataGridView_病患資訊.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, "姓名");
            this.sqL_DataGridView_病患資訊.DataGridClearGridEvent += SqL_DataGridView_病患資訊_DataGridClearGridEvent;
            this.sqL_DataGridView_病患資訊.RowEnterEvent += SqL_DataGridView_病患資訊_RowEnterEvent;

            this.plC_RJ_Button_登入.MouseDownEvent += PlC_RJ_Button_登入_MouseDownEvent;
            Function_重置處方();

            myThread_更新處方 = new MyThread();
            myThread_更新處方.Add_Method(sub_Progran_更新處方);
            myThread_更新處方.AutoRun(true);
            myThread_更新處方.AutoStop(true);
            myThread_更新處方.SetSleepTime(500);
            myThread_更新處方.Trigger();

            this.plC_RJ_Button_完成調劑.MouseDownEvent += PlC_RJ_Button_完成調劑_MouseDownEvent;
            this.plC_RJ_Button_取消調劑.MouseDownEvent += PlC_RJ_Button_取消調劑_MouseDownEvent;
            this.plC_RJ_Button_移至未調劑.MouseDownEvent += PlC_RJ_Button_移至未調劑_MouseDownEvent;
            this.ToolStripMenuItem_處方內容_調劑完成.Click += ToolStripMenuItem_處方內容_調劑完成_Click;
            this.ToolStripMenuItem_處方內容_設為未調劑.Click += ToolStripMenuItem_處方內容_設為未調劑_Click;

            this.rJ_Button_調劑畫面_全亮.MouseDownEvent += RJ_Button_調劑畫面_全亮_MouseDownEvent;
            this.rJ_Button_調劑畫面_單亮.MouseDownEvent += RJ_Button_調劑畫面_單亮_MouseDownEvent;
            this.rJ_Button_調劑畫面_單滅.MouseDownEvent += RJ_Button_調劑畫面_單滅_MouseDownEvent;
            this.rJ_Button_調劑畫面_全滅.MouseDownEvent += RJ_Button_調劑畫面_全滅_MouseDownEvent;

            plC_UI_Init.Add_Method(Program_調劑畫面);
        }   

        private void sub_Progran_更新處方()
        {
            orderDate = plC_NumBox_更新前幾天醫令.Value;
            DateTime dateTime = DateTime.Now.AddDays(orderDate);
            if (PLC_Device_已登入.Bool == false) return;
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_value_temp = new List<object[]>();
            List<object[]> list_value_add = new List<object[]>();
            List<object[]> list_value_delete = new List<object[]>();
            List<object[]> list_value_replace = new List<object[]>();
            List<object[]> list_value_buf = new List<object[]>();
            List<object[]> list_病患資訊 = this.sqL_DataGridView_病患資訊.GetAllRows();
            List<object[]> list_病患資訊_buf = new List<object[]>();

            List<OrderTClass> orderTClasses = OrderTClass.get_by_rx_time_st_end(Main_Form.API_Server, dateTime.GetStartDate(), dateTime.GetEndDate());
            List<OrderTClass> orderTClasses_buf = new List<OrderTClass>();
            if (orderTClasses == null) return;

            orderTClasses.sort(OrderTClassMethod.SortType.領藥號);
            List<string> list_PRI_KEY = (from temp in orderTClasses
                                         select temp.PRI_KEY).Distinct().ToList();
            var keyValuePairs = orderTClasses.CoverToDictionaryBy_PRI_KEY();

            for (int i = 0; i < list_PRI_KEY.Count; i++)
            {
                string PRI_KEY = list_PRI_KEY[i];
                string 調劑完成 = "Y";
                string 處方類型 = "";
                orderTClasses_buf = keyValuePairs.SortDictionaryBy_PRI_KEY(list_PRI_KEY[i]);
                list_value_buf = list_value.GetRows((int)enum_病患資訊.PRI_KEY, PRI_KEY);
                List<string> packages = orderTClasses_buf.GetPackages();
                for (int k = 0; k < orderTClasses_buf.Count; k++)
                {
                    if (orderTClasses_buf[k].實際調劑量.StringIsDouble() == false)
                    {
                        調劑完成 = "N";
                    }
                }
                if (packages.Count > 1)
                {
                    處方類型 = "多劑型";
                }
                else if (packages.Count == 0)
                {
                    處方類型 = "無劑型";
                }
                else if (packages.Count == 1)
                {
                    if(packages[0] == "錢")
                    {
                        處方類型 = "煎煮";
                    }
                    else if (packages[0] == "克")
                    {
                        處方類型 = "科中";
                    }
                }

                if (orderTClasses_buf.Count > 0)
                {

                    if (list_value_buf.Count == 0)
                    {
                        object[] value = new object[new enum_病患資訊().GetLength()];
                        value[(int)enum_病患資訊.GUID] = orderTClasses_buf[0].PRI_KEY;
                        value[(int)enum_病患資訊.PRI_KEY] = orderTClasses_buf[0].PRI_KEY;
                        value[(int)enum_病患資訊.領藥號] = orderTClasses_buf[0].領藥號;
                        value[(int)enum_病患資訊.姓名] = orderTClasses_buf[0].病人姓名;
                        value[(int)enum_病患資訊.年齡] = orderTClasses_buf[0].年齡;
                        value[(int)enum_病患資訊.性別] = orderTClasses_buf[0].性別;
                        value[(int)enum_病患資訊.病歷號] = orderTClasses_buf[0].病歷號;
                        value[(int)enum_病患資訊.處方日期] = orderTClasses_buf[0].開方日期.StringToDateTime().ToDateString();
                        value[(int)enum_病患資訊.調劑完成] = 調劑完成;
                        value[(int)enum_病患資訊.處方類型] = 處方類型;

                        list_value.Add(value);
                   }



                }
      
            }

            Dictionary<object, List<object[]>> keyValues = list_value.ConvertToDictionary((int)enum_病患資訊.病歷號);

            foreach (string key in keyValues.Keys)
            {

                list_value_buf = keyValues.SortDictionary(key);
                if (list_value_buf.Count != 0)
                {
                    List<string> list_處方類型 = (from temp in list_value_buf
                                              select temp[(int)enum_病患資訊.處方類型].ObjectToString()).Distinct().ToList();

                    for (int i = 0; i < list_處方類型.Count; i++)
                    {
                        List<object[]> list_value_temp_buf = (from temp in list_value_buf
                                                          where temp[(int)enum_病患資訊.處方類型].ObjectToString() == list_處方類型[i]
                                                          select temp).ToList();

                        if(list_value_temp_buf.Count > 0)
                        {
                            list_value_temp_buf.Sort(new ICP_病患資訊_降序());
                            if (list_value_temp_buf[0][(int)enum_病患資訊.調劑完成].ObjectToString() != "Y")
                            {
                                list_value_temp.Add(list_value_temp_buf[0]);
                            }
                        }
                       
                    }

                
         
                
                }
            }


            list_value = list_value_temp;

            list_value.Sort(new ICP_病患資訊_升序());
            for (int i = 0; i < list_value.Count; i++)
            {
                string PRI_KEY = list_value[i][(int)enum_病患資訊.PRI_KEY].ObjectToString();
                list_病患資訊_buf = list_病患資訊.GetRows((int)enum_病患資訊.PRI_KEY, PRI_KEY);
                if (list_病患資訊_buf.Count == 0)
                {
                  
                    list_value_add.Add(list_value[i]);

                }
                else
                {


                    string 病歷號 = list_病患資訊_buf[0][(int)enum_病患資訊.病歷號].ObjectToString();
                    string 處方類型 = list_病患資訊_buf[0][(int)enum_病患資訊.處方類型].ObjectToString();
                    list_病患資訊_buf = list_病患資訊.GetRows((int)enum_病患資訊.病歷號, 病歷號);

                    list_病患資訊_buf = (from temp in list_病患資訊
                                     where temp[(int)enum_病患資訊.病歷號].ObjectToString() == 病歷號
                                     where temp[(int)enum_病患資訊.處方類型].ObjectToString() == 處方類型
                                     select temp).ToList();
                    if (list_病患資訊_buf.Count > 1)
                    {

                        list_病患資訊_buf.Sort(new ICP_病患資訊_升序());
                        list_value_delete.Add(list_病患資訊_buf[0]);
                    }
                }

            }
            if (order_value != null)
            {
                string PRI_KEY = order_value[(int)enum_病患資訊.PRI_KEY].ObjectToString();
                string 姓名 = order_value[(int)enum_病患資訊.姓名].ObjectToString();
                string 病歷號 = order_value[(int)enum_病患資訊.病歷號].ObjectToString();
                list_病患資訊_buf = list_病患資訊.GetRows((int)enum_病患資訊.PRI_KEY, PRI_KEY);
                if (list_病患資訊_buf.Count == 0)
                {
                    list_value_add.Add(order_value);
                }
                list_value_temp.Clear();
                for (int i = 0; i < list_value_delete.Count; i++)
                {
                    if(PRI_KEY != list_value_delete[i][(int)enum_病患資訊.PRI_KEY].ObjectToString())
                    {
                        list_value_temp.Add(list_value_delete[i]);
                    }
                    else
                    {
                        if(flag_新處方提示 == false)
                        {
                            string 領藥號 = list_value_delete[i][(int)enum_病患資訊.領藥號].ObjectToString();
                            Task.Run(new Action(delegate
                            {
                                MyMessageBox.ShowDialog($"{姓名} 有新處方 【{領藥號}】");
                            }));
                    
                            flag_新處方提示 = true;
                        }
                    }
                }
                list_value_delete = list_value_temp;

            }
            if (list_value_add.Count > 0) this.sqL_DataGridView_病患資訊.AddRows(list_value_add, true);
            if (list_value_delete.Count > 0) this.sqL_DataGridView_病患資訊.DeleteExtra(list_value_delete, true);


        }

        private void Program_調劑畫面()
        {
            if (plC_NumBox_檢核上限_克.Value < 1) plC_NumBox_檢核上限_克.Value = 1;
            if (plC_NumBox_檢核下限_克.Value < 1) plC_NumBox_檢核下限_克.Value = 1;
            if (plC_NumBox_檢核下限_錢.Value < 1) plC_NumBox_檢核下限_錢.Value = 1;
            if (plC_NumBox_檢核下限_錢.Value < 1) plC_NumBox_檢核下限_錢.Value = 1;
            if (MySerialPort_Scanner01.IsConnected && plC_ScreenPage_main.PageText == "調劑畫面" && PLC_Device_已登入.Bool)
            {
     
                try
                {
                    string text = MySerialPort_Scanner01.ReadString();
                    if (text.StringIsEmpty()) return;
                    System.Threading.Thread.Sleep(200);
                    text = MySerialPort_Scanner01.ReadString();
                    MySerialPort_Scanner01.ClearReadByte();
                    text = text.Replace("\0", "");
                    text = text.Replace("\n", "");
                    text = text.Replace("\r", "");
                    List<OrderClass> orderClasses = Funtion_醫令資料_API呼叫(text);
                    if (orderClasses != null)
                    {
                        if (orderClasses.Count > 0)
                        {
                            string PRI_KEY = orderClasses[0].PRI_KEY;
                            this.sqL_DataGridView_病患資訊.SetSelectRow(enum_病患資訊.PRI_KEY.GetEnumName(), PRI_KEY);
                        }
                    }
                    else
                    {
                        List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, text);
                        if (medClasses.Count > 0)
                        {
                            string 藥碼 = medClasses[0].藥品碼;

                            List<object[]> list_處方內容 = this.sqL_DataGridView_處方內容.GetAllRows();
                            List<object[]> list_處方內容_selected = this.sqL_DataGridView_處方內容.Get_All_Select_RowsValues();
                            List<object[]> list_處方內容_buf = (from temp in list_處方內容
                                                            where ReplaceHyphenWithStar(temp[(int)enum_處方內容.藥碼].ObjectToString()) == ReplaceHyphenWithStar(藥碼)
                                                            select temp).ToList();
                
                            if (list_處方內容_buf.Count == 0)
                            {
                                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("處方無此條碼藥品", 1500);
                                dialog_AlarmForm.ShowDialog();
                                return;
                            }
                            if(list_處方內容_selected.Count == 0)
                            {
                                string 頻次 = list_處方內容_buf[0][(int)enum_處方內容.頻次].ObjectToString();

                                string 現在調劑頻次 = OrderTClass_現在調劑處方.GetCurrentFreq();
                                if (現在調劑頻次 != null)
                                {
                                    if (頻次 != 現在調劑頻次)
                                    {
                                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"[{現在調劑頻次}]未完成", 1500);
                                        dialog_AlarmForm.ShowDialog();
                                        return;
                                    }
                                }

                                if (list_處方內容_buf[0][(int)enum_處方內容.實調].ObjectToString().StringIsDouble())
                                {
                                    Voice.MediaPlayAsync($@"{currentDirectory}\此藥品已調劑.wav");
                                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("此藥品已調劑", 1500);
                                    dialog_AlarmForm.ShowDialog();
                                    return;
                                }
                                string 單位 = list_處方內容_buf[0][(int)enum_處方內容.單位].ObjectToString();
                                if (單位 == "錢") enum_Unit_Type = Port.enum_unit_type.dwt;
                                else if (單位 == "克") enum_Unit_Type = Port.enum_unit_type.g;
                                else if (單位 == "BTL")
                                {
                                    Voice.MediaPlayAsync($@"{currentDirectory}\此為罐裝調劑.wav");
                                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("此為罐裝調劑", 1500);
                                    dialog_AlarmForm.ShowDialog();
                                }
                                else
                                {
                                    this.sqL_DataGridView_處方內容.SetSelectRow(list_處方內容_buf[0]);
                                    ToolStripMenuItem_處方內容_調劑完成_Click(null, null);
                                    return;
                                }
                                if (flag_EXCELL_SCALE_IS_READY == true) EXCELL_set_sub_current_weight();
                                this.sqL_DataGridView_處方內容.SetSelectRow(list_處方內容_buf[0]);
                            }
                            else if(list_處方內容_selected[0][(int)enum_處方內容.GUID].ObjectToString() == list_處方內容_buf[0][(int)enum_處方內容.GUID].ObjectToString())
                            {
                                string 頻次 = list_處方內容_buf[0][(int)enum_處方內容.頻次].ObjectToString();
                                string GUID = list_處方內容_buf[0][(int)enum_處方內容.GUID].ObjectToString();

                                string 現在調劑頻次 = OrderTClass_現在調劑處方.GetCurrentFreq();
                                if (現在調劑頻次 != null)
                                {
                                    if (頻次 != 現在調劑頻次)
                                    {
                                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"[{現在調劑頻次}]未完成", 1500);
                                        dialog_AlarmForm.ShowDialog();
                                        return;
                                    }
                                }

                                double 應調 = list_處方內容_selected[0][(int)enum_處方內容.應調].StringToDouble();
                                string 單位 = list_處方內容_selected[0][(int)enum_處方內容.單位].ObjectToString();
                                double 實調 = rJ_Lable_實調.Text.StringToDouble();
                                double 檢核上限 = 0;
                                double 檢核下限 = 0;

                                if (單位 == "錢")
                                {
                                    檢核上限 = (double)((double)plC_NumBox_檢核上限_錢.Value / 10D);
                                    檢核下限 = (double)((double)plC_NumBox_檢核下限_錢.Value / 10D);
                                }
                                else
                                {
                                    檢核上限 = (double)((double)plC_NumBox_檢核上限_克.Value / 10D);
                                    檢核下限 = (double)((double)plC_NumBox_檢核下限_克.Value / 10D);

                                }
                           
                                double 應調_L = 應調 + 檢核上限;
                                double 應調_H = 應調 - 檢核下限;
                                if (實調 <= 應調_L || 實調 >= 應調_H)
                                {
                                    if (MyMessageBox.ShowDialog("秤重範圍異常,是否完成調劑?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                                    {
                                        Funtion_調劑完成(GUID, (實調 * -1).ToString() ,"");
                                    }
                                }
                                else
                                {
                                    Funtion_調劑完成(list_處方內容_selected[0][(int)enum_處方內容.GUID].ObjectToString(), 應調.ToString(),"");
                                }


                            }
                            else
                            {
                                if (list_處方內容_buf[0][(int)enum_處方內容.實調].ObjectToString().StringIsDouble())
                                {
                                    Voice.MediaPlayAsync($@"{currentDirectory}\此藥品已調劑.wav");
                                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("此藥品已調劑", 1500);
                                    dialog_AlarmForm.ShowDialog();
                                    return;
                                }
                                string 單位 = list_處方內容_buf[0][(int)enum_處方內容.單位].ObjectToString();
                                string 頻次 = list_處方內容_buf[0][(int)enum_處方內容.頻次].ObjectToString();
                                string 現在調劑頻次 = OrderTClass_現在調劑處方.GetCurrentFreq();
                                if (現在調劑頻次 != null)
                                {
                                    if (頻次 != 現在調劑頻次)
                                    {
                                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"[{現在調劑頻次}]頻次未完成", 1500);
                                        dialog_AlarmForm.ShowDialog();
                                        return;
                                    }
                                }


                                if (單位 == "錢") enum_Unit_Type = Port.enum_unit_type.dwt;
                                else if (單位 == "克") enum_Unit_Type = Port.enum_unit_type.g;
                                else if (單位 == "BTL")
                                {
                                    Voice.MediaPlayAsync($@"{currentDirectory}\此為罐裝調劑.wav");
                                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("此為罐裝調劑", 1500);
                                    dialog_AlarmForm.ShowDialog();
                                }
                                else
                                {
                                    this.sqL_DataGridView_處方內容.SetSelectRow(list_處方內容_buf[0]);
                                    ToolStripMenuItem_處方內容_調劑完成_Click(null, null);
                                    return;
                                }
                                if (flag_EXCELL_SCALE_IS_READY == true) EXCELL_set_sub_current_weight();
                                this.sqL_DataGridView_處方內容.SetSelectRow(list_處方內容_buf[0]);
                            }
                         
                        }
                    }
                }
                catch
                {

                }
               


            }

        }
        #region Function
        public void Function_重置處方()
        {
            this.Invoke(new Action(delegate
            {
                sqL_DataGridView_病患資訊.ClearGrid();
                sqL_DataGridView_處方內容.ClearGrid();
                order_value = null;
                OrderTClass_現在調劑處方 = null;
                order_current_row = null;
                flag_新處方提示 = false;
            }));
        }
        public void Function_更新處方內容(string PRI_KEY)
        {
            List<OrderTClass> orderTClasses = OrderTClass.get_by_pri_key(Main_Form.API_Server, PRI_KEY);
            if (orderTClasses.Count == 0) return;

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < orderTClasses.Count; i++)
            {
                object[] value = Funtion_orderTClassesToObject(orderTClasses[i]);

                list_value.Add(value);
            }
        
            order_value = new object[new enum_病患資訊().GetLength()];
            order_value[(int)enum_病患資訊.PRI_KEY] = orderTClasses[0].PRI_KEY;
            order_value[(int)enum_病患資訊.領藥號] = orderTClasses[0].領藥號;
            order_value[(int)enum_病患資訊.姓名] = orderTClasses[0].病人姓名;
            order_value[(int)enum_病患資訊.年齡] = orderTClasses[0].年齡;
            order_value[(int)enum_病患資訊.性別] = orderTClasses[0].性別;
            order_value[(int)enum_病患資訊.病歷號] = orderTClasses[0].病歷號;
            order_value[(int)enum_病患資訊.處方日期] = orderTClasses[0].開方日期.StringToDateTime().ToDateString();

            OrderTClass_現在調劑處方 = orderTClasses;
            order_current_row = null;
            this.sqL_DataGridView_處方內容.ClearGrid();
            this.sqL_DataGridView_處方內容.RefreshGrid(list_value);
        }
        public void Function_更新處方UI(string GUID)
        {
            OrderTClass orderTClass = OrderTClass.get_by_guid(Main_Form.API_Server, GUID);
            if (orderTClass != null)
            {
                double 總重 = 0;
                double 應調 = 0;
                string 單位 = "";
                List<object[]> list_value = this.sqL_DataGridView_處方內容.GetAllRows();
                for (int i = 0; i < list_value.Count; i++)
                {

                    if (list_value[i][(int)enum_處方內容.應調].ObjectToString().StringIsDouble())
                    {
                        應調 = list_value[i][(int)enum_處方內容.應調].ObjectToString().StringToDouble();
                        單位 = list_value[i][(int)enum_處方內容.單位].ObjectToString();
                        if (單位 == "錢") 應調 *= 3.75;

                        總重 += 應調;
                    }
                }
                Function_更新處方UI(orderTClass, 總重);
            }
        }
        public void Function_更新處方UI(OrderTClass orderTClass , double 總重)
        {
            string 包數 = "";
            if (orderTClass.頻次.ToUpper() == "Q1H") 包數 = "24";
            if (orderTClass.頻次.ToUpper() == "Q2H") 包數 = "12";
            if (orderTClass.頻次.ToUpper() == "Q3H") 包數 = "8";
            if (orderTClass.頻次.ToUpper() == "Q4H") 包數 = "6";
            if (orderTClass.頻次.ToUpper() == "Q6H") 包數 = "4";
            if (orderTClass.頻次.ToUpper() == "Q8H") 包數 = "3";
            if (orderTClass.頻次.ToUpper() == "Q12H") 包數 = "2";
            if (orderTClass.頻次.ToUpper() == "QDAM") 包數 = "1";
            if (orderTClass.頻次.ToUpper() == "QDPM") 包數 = "1";
            if (orderTClass.頻次.ToUpper() == "QDHS") 包數 = "1";
            if (orderTClass.頻次.ToUpper() == "QN") 包數 = "1";
            if (orderTClass.頻次.ToUpper() == "BID") 包數 = "2";
            if (orderTClass.頻次.ToUpper() == "QN") 包數 = "1";
            if (orderTClass.頻次.ToUpper() == "QDHS") 包數 = "1";
            if (orderTClass.頻次.ToUpper() == "QDPM") 包數 = "3";
            if (orderTClass.頻次.ToUpper() == "BID&HS") 包數 = "3";
            if (orderTClass.頻次.ToUpper() == "QID") 包數 = "4";
            if (orderTClass.頻次.ToUpper() == "HS") 包數 = "1";
            if (orderTClass.頻次.ToUpper() == "TID&HS") 包數 = "4";
            if (orderTClass.頻次.ToUpper() == "TID") 包數 = "3";
            if (orderTClass.頻次.ToUpper() == "TIDAC") 包數 = "3";
            if (orderTClass.頻次.ToUpper() == "ASORDER") 包數 = "1";
            string 天數 = orderTClass.天數;
            string 總包數 = (包數.StringToInt32() * 天數.StringToInt32()).ToString();

            this.Invoke(new Action(delegate
            {

                orderTClass.藥品名稱 = RemoveParenthesesContent(orderTClass.藥品名稱);
        
                rJ_Lable_處方藥品.Text = $"{orderTClass.藥品名稱}";
                rJ_Lable_領藥號.Text = $"{orderTClass.領藥號}";
                rJ_Lable_處方資訊_姓名_性別_病歷號.Text = $"{orderTClass.病人姓名}({orderTClass.性別}) {orderTClass.病歷號}";
                rJ_Lable_處方資訊_處方日期.Text = $"{orderTClass.開方日期.StringToDateTime().ToDateString()}";
                rJ_Lable_處方資訊_年齡.Text = $"{orderTClass.年齡.StringToInt32()}歲";
                rJ_Lable_處方資訊_單筆包數.Text = $"{包數}包/天";
                rJ_Lable_處方資訊_單筆處方天數.Text = $"{天數}天";
                rJ_Lable_處方資訊_單包重.Text = $"{(總重 / 總包數.StringToInt32()).ToString("0.00")} 克/包";
                rJ_Lable_處方資訊_單筆總重.Text = $"總重:{總重.ToString("0.00")}克";
                rJ_Lable_總包數.Text = $"{總包數}包";
                rJ_Lable_應調.Text = $"{orderTClass.交易量.StringToDouble() * -1}";
                rJ_Lable_醫師代號.Text = $"醫師代號 : {orderTClass.醫師ID}";
                rJ_Lable_處方時間.Text = $"處方時間 : {orderTClass.開方日期.StringToDateTime().ToTimeString()}";
                rJ_Lable_科別.Text = $"科別 : {orderTClass.科別}";
                rJ_Lable_應調單位.Text = $"{orderTClass.劑量單位}";
                rJ_Lable_實調單位.Text = $"{orderTClass.劑量單位}";
            }));
        }
        private void Function_登入(sessionClass _sessionClass)
        {
            Function_從SQL取得儲位到本地資料();
            sessionClass = _sessionClass;
            this.Invoke(new Action(delegate 
            {
                this.plC_RJ_Button_登入.Texts = "登出";
                PLC_Device_已登入.Bool = true;
                rJ_Lable_調劑人員.Text = $"調劑人員 : {sessionClass.Name}";
                rJ_Lable_調劑人員.TextColor = Color.Green;
                Function_重置處方();
            }));
        }
        private void Function_登出()
        {
            sessionClass = null;
            this.Invoke(new Action(delegate
            {    
                PLC_Device_已登入.Bool = false;
                this.plC_RJ_Button_登入.Texts = "登入";
                rJ_Lable_調劑人員.Text = $"【請登入系統】";
                rJ_Lable_調劑人員.TextColor = Color.OrangeRed;
                Function_重置處方();
            }));
        }
        private object[] Funtion_orderTClassesToObject(OrderTClass orderTClass)
        {
            object[] value = new object[new enum_處方內容().GetLength()];
            if (orderTClass.交易量.StringIsDouble())
            {
                orderTClass.交易量 = (orderTClass.交易量.StringToDouble() * -1).ToString("0.00");
            }
            if (orderTClass.實際調劑量.StringIsEmpty()) orderTClass.實際調劑量 = "-";

            orderTClass.藥品名稱 = RemoveParenthesesContent(orderTClass.藥品名稱);
            orderTClass.藥品名稱 = $"({orderTClass.藥品碼}){orderTClass.藥品名稱}";
            value[(int)enum_處方內容.GUID] = orderTClass.GUID;
            value[(int)enum_處方內容.藥碼] = orderTClass.藥品碼;
            value[(int)enum_處方內容.藥名] = orderTClass.藥品名稱;
            value[(int)enum_處方內容.應調] = orderTClass.交易量.StringToDouble();
            value[(int)enum_處方內容.實調] = orderTClass.實際調劑量;
            if (orderTClass.實際調劑量.StringIsDouble())
            {
                value[(int)enum_處方內容.實調] = orderTClass.實際調劑量.StringToDouble();
            }
     
            value[(int)enum_處方內容.頻次] = orderTClass.頻次;
            value[(int)enum_處方內容.天] = orderTClass.天數;
            value[(int)enum_處方內容.單位] = orderTClass.劑量單位;
            value[(int)enum_處方內容.服用方法] = orderTClass.頻次;
            return value;
        }
        private OrderTClass Funtion_調劑完成(string GUID , string 實調 , string 備註)
        {
            Dialog_AlarmForm dialog_AlarmForm;
            OrderTClass orderTClass = OrderTClass.get_by_guid(Main_Form.API_Server, GUID);
            if (orderTClass == null) return null;
            string 藥碼 = orderTClass.藥品碼;
            if (實調.StringIsEmpty())
            {
                實調 = orderTClass.交易量;
            }
            Task.Run(new Action(delegate
            {
                for (int i = 0; i < 4; i++)
                {
                    Function_儲位亮燈(藥碼, Color.Red);
                    System.Threading.Thread.Sleep(300);
                    Function_儲位亮燈(藥碼, Color.Black);
                    System.Threading.Thread.Sleep(300);
                }
            }));
            orderTClass.實際調劑量 = (實調.StringToDouble() * -1).ToString("0.00");
            orderTClass.藥師姓名 = sessionClass.Name;
            orderTClass.藥師ID = sessionClass.ID;
            orderTClass.過帳時間 = DateTime.Now.ToDateTimeString_6();

            OrderTClass.updete_by_guid(Main_Form.API_Server, orderTClass);

            transactionsClass transactionsClass = new transactionsClass();
            transactionsClass.藥品碼 = orderTClass.藥品碼;
            transactionsClass.藥品名稱 = orderTClass.藥品名稱;
            transactionsClass.交易量 = 實調.StringToDouble().ToString("0.00");
            transactionsClass.操作人 = orderTClass.藥師姓名;
            transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
            transactionsClass.盤點量 = "0";
            transactionsClass.結存量 = "0";
            transactionsClass.領藥號 = orderTClass.領藥號;
            transactionsClass.頻次 = orderTClass.頻次;
            transactionsClass.動作 = enum_交易記錄查詢動作.掃碼領藥.GetEnumName();
            transactionsClass.開方時間 = orderTClass.開方日期;
            transactionsClass.病人姓名 = orderTClass.病人姓名;
            transactionsClass.病歷號 = orderTClass.病歷號;
            transactionsClass.藥袋序號 = orderTClass.PRI_KEY;
            transactionsClass.備註 = orderTClass.備註;
            transactionsClass.add(Main_Form.API_Server, transactionsClass, Main_Form.ServerName, Main_Form.ServerType);
            Function_更新處方內容(orderTClass.PRI_KEY);

            if(OrderTClass_現在調劑處方.GetFreqIsDone(orderTClass.頻次))
            {
                dialog_AlarmForm = new Dialog_AlarmForm($"[{orderTClass.頻次}]調劑完成", 1000 , Color.Green);
                dialog_AlarmForm.ShowDialog();
            }

            object[] value = Funtion_orderTClassesToObject(orderTClass);
            this.sqL_DataGridView_處方內容.ClearSelection();
            this.sqL_DataGridView_處方內容.ReplaceExtra(value, true);

            List<object[]> list_處方內容 = this.sqL_DataGridView_處方內容.GetAllRows();
            for (int i = 0; i < list_處方內容.Count; i++)
            {
                if (list_處方內容[i][(int)enum_處方內容.實調].ObjectToString().StringIsDouble() == false) return orderTClass;
            }
            this.sqL_DataGridView_處方內容.ClearGrid();


            dialog_AlarmForm = new Dialog_AlarmForm("調劑完成", 1500, Color.Green);
            dialog_AlarmForm.ShowDialog();

            return orderTClass;
        }
        private OrderTClass Funtion_設為未調劑(string GUID)
        {
            OrderTClass orderTClass = OrderTClass.get_by_guid(Main_Form.API_Server, GUID);
            if (orderTClass == null) return null;
            Task.Run(new Action(delegate
            {
                Function_儲位亮燈(orderTClass.藥品碼, Color.Black);
            }));

            orderTClass.實際調劑量 = "";
            orderTClass.藥師姓名 = sessionClass.Name;
            orderTClass.藥師ID = sessionClass.ID;
            orderTClass.過帳時間 = DateTime.Now.ToDateTimeString_6();

            OrderTClass.updete_by_guid(Main_Form.API_Server, orderTClass);

        
            object[] value = Funtion_orderTClassesToObject(orderTClass);
            this.sqL_DataGridView_處方內容.ClearSelection();
            this.sqL_DataGridView_處方內容.ReplaceExtra(value, true);

            return orderTClass;
        }

        #endregion
        #region Event
        private void SqL_DataGridView_處方內容_DataGridClearGridEvent()
        {
            this.Invoke(new Action(delegate
            {
                rJ_Lable_處方藥品.Text = $"-----------";
                rJ_Lable_處方資訊_單筆包數.Text = $"-包";
                rJ_Lable_處方資訊_單筆處方天數.Text = $"--天";
                rJ_Lable_處方資訊_單包重.Text = $"--.-- 克/包";
                rJ_Lable_處方資訊_單筆總重.Text = $"總重:---克";
                rJ_Lable_總包數.Text = $"--包";
                rJ_Lable_應調.Text = $"-.--";
                rJ_Lable_醫師代號.Text = $"醫師代號 : ------------";
                rJ_Lable_處方時間.Text = $"處方時間 : --:--:--";
                rJ_Lable_科別.Text = $"科別 : -----";
            }));

        }
        private void SqL_DataGridView_病患資訊_DataGridClearGridEvent()
        {
            this.Invoke(new Action(delegate
            {
                rJ_Lable_領藥號.Text = $"----";
                rJ_Lable_處方資訊_姓名_性別_病歷號.Text = $"-------(-) -----------";
                rJ_Lable_處方資訊_處方日期.Text = $"----/--/--";
                rJ_Lable_處方資訊_年齡.Text = $"--歲";
                
            }));

        }
        private void SqL_DataGridView_病患資訊_RowEnterEvent(object[] RowValue)
        {
            string PRI_KEY = RowValue[(int)enum_病患資訊.PRI_KEY].ObjectToString();
            string 領藥號 = RowValue[(int)enum_病患資訊.領藥號].ObjectToString();
            string 姓名 = RowValue[(int)enum_病患資訊.姓名].ObjectToString();
            string 性別 = RowValue[(int)enum_病患資訊.性別].ObjectToString();
            string 病歷號 = RowValue[(int)enum_病患資訊.病歷號].ObjectToString();
            string 年齡 = RowValue[(int)enum_病患資訊.年齡].ObjectToString();
            string 處方日期 = RowValue[(int)enum_病患資訊.處方日期].ObjectToString();
            if (OrderTClass_現在調劑處方 != null)
            {
                if (PRI_KEY != order_value[(int)enum_病患資訊.PRI_KEY].ObjectToString())
                {
                    if (OrderTClass_現在調劑處方.GetIsDone() == false)
                    {
                        if (MyMessageBox.ShowDialog($"({ order_value[(int)enum_病患資訊.領藥號].ObjectToString()}){ order_value[(int)enum_病患資訊.姓名].ObjectToString()} 未調劑完成,是否跳至其他處方?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes)
                        {
                        
                            this.sqL_DataGridView_病患資訊.SetSelectRow("PRI_KEY", order_value[(int)enum_病患資訊.PRI_KEY].ObjectToString());
                            return;
                        }
                        else
                        {
                            RJ_Button_調劑畫面_全滅_MouseDownEvent(null);
                        }
                    }
                }
              
            }
            order_value = RowValue;


            this.Invoke(new Action(delegate
            {
                rJ_Lable_領藥號.Text = $"{領藥號}";
                rJ_Lable_處方資訊_姓名_性別_病歷號.Text = $"{姓名}({性別}) {病歷號}";
                rJ_Lable_處方資訊_處方日期.Text = $"{處方日期}";
                rJ_Lable_處方資訊_年齡.Text = $"{年齡}歲";
            }));
            List<OrderTClass> orderTClasses = OrderTClass.get_header_by_PATCODE(Main_Form.API_Server, 病歷號, DateTime.Now.AddDays(orderDate));

            Console.WriteLine($"{orderTClasses.JsonSerializationt(true)}");

            if(orderTClasses.Count > 1)
            {
                string temp = "";
                for(int i = 0; i < orderTClasses.Count; i++)
                {
                    if(orderTClasses[i].領藥號 != 領藥號)
                    {
                        temp += $"{orderTClasses[i].領藥號}";
                        temp += ",";
                    }
                }
                temp = temp.Substring(0, temp.Length - 1);
                Task.Run(new Action(delegate
                {
                    MyMessageBox.ShowDialog($"提示有前置處方領藥號【{temp}】請確認內容");
                }));
             
            }    
            Function_更新處方內容(PRI_KEY);
            RJ_Button_調劑畫面_全亮_MouseDownEvent(null);

        }
        private void SqL_DataGridView_處方內容_DataGridRefreshEvent()
        {
            string 實調 = "";
            for (int i = 0; i < this.sqL_DataGridView_處方內容.dataGridView.Rows.Count; i++)
            {
                實調 = this.sqL_DataGridView_處方內容.dataGridView.Rows[i].Cells[enum_處方內容.實調.GetEnumName()].Value.ToString();
                if (實調.StringIsDouble())
                {
                    this.sqL_DataGridView_處方內容.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                    this.sqL_DataGridView_處方內容.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }

            }
        }
        private void SqL_DataGridView_處方內容_RowEnterEvent(object[] RowValue)
        {
            string GUID = RowValue[(int)enum_處方內容.GUID].ObjectToString();
            string 藥名 = RowValue[(int)enum_處方內容.藥名].ObjectToString();
            string 藥碼 = RowValue[(int)enum_處方內容.藥碼].ObjectToString();
            string 藥碼_current_row = "";
            if (order_current_row != null)
            {
                藥碼_current_row = order_current_row[(int)enum_處方內容.藥碼].ObjectToString();
                if (GUID != order_current_row[(int)enum_處方內容.GUID].ObjectToString())
                {
                    if (order_current_row[(int)enum_處方內容.實調].ObjectToString().StringIsDouble() == false)
                    {
                        if (MyMessageBox.ShowDialog($"{ order_current_row[(int)enum_處方內容.藥名].ObjectToString()} 未調劑完成,是否跳至其他藥品調劑?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes)
                        {
                            RJ_Button_調劑畫面_全滅_MouseDownEvent(null);
                            this.sqL_DataGridView_處方內容.SetSelectRow(order_current_row);
                            return;
                        }
                    }
                }             
            }


            Task.Run(new Action(delegate
            {
                //Function_儲位亮燈(藥碼_current_row, Color.Black);
                Function_儲位亮燈(藥碼, Color.Lime);
                Voice voice = new Voice();
                voice.SpeakOnTask(RemoveParenthesesContent(藥名));
            }));

            Function_更新處方UI(GUID);
            order_current_row = RowValue;
        }
        private void PlC_RJ_Button_登入_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.plC_RJ_Button_登入.Texts == "登入")
            {
                Dialog_系統登入 dialog_系統登入 = new Dialog_系統登入();
                if (dialog_系統登入.ShowDialog() != DialogResult.Yes) return;
                this.Function_登入(dialog_系統登入.Value);            
                return;
            }
            if (this.plC_RJ_Button_登入.Texts == "登出")
            {
                if (MyMessageBox.ShowDialog("是否登出?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                this.Function_登出();
                return;
            }
        }
        private void PlC_RJ_Button_完成調劑_MouseDownEvent(MouseEventArgs mevent)
        {
            if (order_value == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取病患資訊", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (MyMessageBox.ShowDialog("所有未調劑藥品,系統強制扣帳並記錄", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            string PRI_KEY = order_value[(int)enum_病患資訊.PRI_KEY].ObjectToString();
            List<OrderTClass> orderTClasses = OrderTClass.get_by_pri_key(Main_Form.API_Server, PRI_KEY);
            List<transactionsClass> transactionsClasses = new List<transactionsClass>();
            for (int i = 0; i < orderTClasses.Count; i++)
            {
                OrderTClass orderTClass = orderTClasses[i];
                if (orderTClass.實際調劑量.StringIsDouble() == true) continue;                
                orderTClass.實際調劑量 = (orderTClass.交易量.StringToDouble() * -1).ToString("0.00");

                orderTClass.藥師姓名 = sessionClass.Name;
                orderTClass.藥師ID = sessionClass.ID;
                orderTClass.過帳時間 = DateTime.Now.ToDateTimeString_6();
                orderTClass.備註 = "強制扣帳";
                OrderTClass.updete_by_guid(Main_Form.API_Server, orderTClass);

                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.藥品碼 = orderTClass.藥品碼;
                transactionsClass.藥品名稱 = orderTClass.藥品名稱;
                transactionsClass.交易量 = orderTClass.交易量;
                transactionsClass.操作人 = orderTClass.藥師姓名;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.盤點量 = "0";
                transactionsClass.結存量 = "0";
                transactionsClass.領藥號 = orderTClass.領藥號;
                transactionsClass.頻次 = orderTClass.頻次;
                transactionsClass.動作 = enum_交易記錄查詢動作.掃碼領藥.GetEnumName();
                transactionsClass.開方時間 = orderTClass.開方日期;
                transactionsClass.病人姓名 = orderTClass.病人姓名;
                transactionsClass.病歷號 = orderTClass.病歷號;
                transactionsClass.藥袋序號 = PRI_KEY;
                transactionsClasses.Add(transactionsClass);

            }
            transactionsClass.add(Main_Form.API_Server, transactionsClasses, Main_Form.ServerName, Main_Form.ServerType);
            //this.sqL_DataGridView_處方內容.ClearSelection();
            //this.sqL_DataGridView_處方內容.ReplaceExtra(list_value_buf, true);

            Function_重置處方();

        }
        private void PlC_RJ_Button_取消調劑_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否取消調劑?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            RJ_Button_調劑畫面_全滅_MouseDownEvent(null);
            Function_重置處方();
        }
        private void PlC_RJ_Button_移至未調劑_MouseDownEvent(MouseEventArgs mevent)
        {
            if (order_value == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取病患資訊", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (MyMessageBox.ShowDialog("剩餘調劑項目不扣帳", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            string PRI_KEY = order_value[(int)enum_病患資訊.PRI_KEY].ObjectToString();
            List<OrderTClass> orderTClasses = OrderTClass.get_by_pri_key(Main_Form.API_Server, PRI_KEY);
            List<transactionsClass> transactionsClasses = new List<transactionsClass>();
            for (int i = 0; i < orderTClasses.Count; i++)
            {
                OrderTClass orderTClass = orderTClasses[i];
                if (orderTClass.實際調劑量.StringIsDouble() == true) continue;
                orderTClass.實際調劑量 = "0";

                orderTClass.藥師姓名 = sessionClass.Name;
                orderTClass.藥師ID = sessionClass.ID;
                orderTClass.備註 = "移至未調劑";
                orderTClass.過帳時間 = DateTime.Now.ToDateTimeString_6();
                OrderTClass.updete_by_guid(Main_Form.API_Server, orderTClass);

                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.藥品碼 = orderTClass.藥品碼;
                transactionsClass.藥品名稱 = orderTClass.藥品名稱;
                transactionsClass.交易量 = orderTClass.交易量;
                transactionsClass.操作人 = orderTClass.藥師姓名;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.盤點量 = "0";
                transactionsClass.結存量 = "0";
                transactionsClass.領藥號 = orderTClass.領藥號;
                transactionsClass.頻次 = orderTClass.頻次;
                transactionsClass.動作 = enum_交易記錄查詢動作.掃碼領藥.GetEnumName();
                transactionsClass.開方時間 = orderTClass.開方日期;
                transactionsClass.病人姓名 = orderTClass.病人姓名;
                transactionsClass.病歷號 = orderTClass.病歷號;
                transactionsClass.藥袋序號 = PRI_KEY;
                transactionsClasses.Add(transactionsClass);

            }
            transactionsClass.add(Main_Form.API_Server, transactionsClasses, Main_Form.ServerName, Main_Form.ServerType);
            RJ_Button_調劑畫面_全滅_MouseDownEvent(null);
            Function_重置處方();
        }
        private void ToolStripMenuItem_處方內容_調劑完成_Click(object sender, EventArgs e)
        {
            Dialog_AlarmForm dialog_AlarmForm;
            List<object[]> list_value = this.sqL_DataGridView_處方內容.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string GUID = list_value[0][(int)enum_處方內容.GUID].ObjectToString();
            OrderTClass orderTClass = Funtion_調劑完成(GUID, "" , "強制扣帳");

        }
        private void ToolStripMenuItem_處方內容_設為未調劑_Click(object sender, EventArgs e)
        {
            Dialog_AlarmForm dialog_AlarmForm;
            List<object[]> list_value = this.sqL_DataGridView_處方內容.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string GUID = list_value[0][(int)enum_處方內容.GUID].ObjectToString();
            OrderTClass orderTClass = Funtion_設為未調劑(GUID);

        }
        private void RJ_Button_調劑畫面_全滅_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_處方內容.GetAllRows();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1000);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<string> Codes = new List<string>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string 藥碼 = list_value[i][(int)enum_處方內容.藥碼].ObjectToString();
                Codes.Add(藥碼);

            }
            Function_儲位亮燈(Codes, Color.Black);
        }
        private void RJ_Button_調劑畫面_單滅_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_處方內容.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1000);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_value[0][(int)enum_處方內容.藥碼].ObjectToString();
            Task.Run(new Action(delegate 
            {
                for (int i = 0; i < 4; i++)
                {
                    Function_儲位亮燈(藥碼, Color.Red);
                    System.Threading.Thread.Sleep(300);
                    Function_儲位亮燈(藥碼, Color.Black);
                    System.Threading.Thread.Sleep(300);
                }           
            }));

        }
        private void RJ_Button_調劑畫面_單亮_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_處方內容.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1000);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_value[0][(int)enum_處方內容.藥碼].ObjectToString();
            Function_儲位亮燈(藥碼, Color.Red);
        }
        private void RJ_Button_調劑畫面_全亮_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_處方內容.GetAllRows();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1000);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            List<string> Codes = new List<string>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string 藥碼 = list_value[i][(int)enum_處方內容.藥碼].ObjectToString();
                Codes.Add(藥碼);

            }
            Function_儲位亮燈(Codes, Color.Red);
        }
        #endregion
        public static string RemoveParenthesesContent(string input)
        {
            // 使用正則表達式替換括號及其內部的內容
            return System.Text.RegularExpressions.Regex.Replace(input, @"\s*\([^)]*\)", "");
        }


        public class ICP_病患資訊_降序 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                int temp0 = x[(int)enum_病患資訊.領藥號].StringToInt32();
                int temp1 = y[(int)enum_病患資訊.領藥號].StringToInt32();

                return temp1.CompareTo(temp0);

            }
        }
        public class ICP_病患資訊_升序 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                int temp0 = x[(int)enum_病患資訊.領藥號].StringToInt32();
                int temp1 = y[(int)enum_病患資訊.領藥號].StringToInt32();

                return temp0.CompareTo(temp1);

            }
        }
    }
}
