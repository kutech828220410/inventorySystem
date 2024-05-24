using System;
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
        private MyThread myThread_更新處方;

        public enum enum_處方內容
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("應調,VARCHAR,15,NONE")]
            應調,
            [Description("實調,VARCHAR,15,NONE")]
            實調,
            [Description("天,VARCHAR,15,NONE")]
            天,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
            [Description("服用方法,VARCHAR,15,NONE")]
            服用方法,
        }
        public enum enum_病患資訊
        {
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
            [Description("處方日期,VARCHAR,15,NONE")]
            處方日期,
        }
        public static sessionClass sessionClass = new sessionClass();
        private void Program_調劑畫面_Init()
        {
            Table table_處方內容 = new Table(new enum_處方內容());
            this.sqL_DataGridView_處方內容.Init(table_處方內容);
            this.sqL_DataGridView_處方內容.Set_ColumnVisible(false, new enum_處方內容().GetEnumNames());
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, "藥名");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "應調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "實調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, "天");
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
            this.ToolStripMenuItem_處方內容_調劑完成.Click += ToolStripMenuItem_處方內容_調劑完成_Click;

     
            plC_UI_Init.Add_Method(Program_調劑畫面);
        }

    

        private void sub_Progran_更新處方()
        {
            if (PLC_Device_已登入.Bool == false) return;
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_value_buf = new List<object[]>();
            List<OrderTClass> orderTClasses = OrderTClass.get_by_rx_time_st_end(Main_Form.API_Server, "2024-05-09 00:00:00".StringToDateTime(), "2024-05-09 23:59:59".StringToDateTime());
            List<OrderTClass> orderTClasses_buf = new List<OrderTClass>();
            orderTClasses.sort(OrderTClassMethod.SortType.領藥號);
            List<string> list_PRI_KEY = (from temp in orderTClasses
                                         select temp.PRI_KEY).ToList();
            var keyValuePairs = orderTClasses.CoverToDictionaryBy_PRI_KEY();

            for (int i = 0; i < list_PRI_KEY.Count; i++)
            {
                orderTClasses_buf = keyValuePairs.SortDictionaryBy_PRI_KEY(list_PRI_KEY[i]);
                for (int k = 0; k < orderTClasses_buf.Count; k++)
                {
                    list_value_buf = list_value.GetRows((int)enum_病患資訊.PRI_KEY, list_PRI_KEY[i]);
                    if(list_value_buf.Count == 0)
                    {
                        if (orderTClasses_buf[k].實際調劑量.StringIsDouble())
                        {
                            object[] value = new object[new enum_病患資訊().GetLength()];
                            value[(int)enum_病患資訊.PRI_KEY] = orderTClasses_buf[k].PRI_KEY;
                            value[(int)enum_病患資訊.領藥號] = orderTClasses_buf[k].領藥號;
                            value[(int)enum_病患資訊.姓名] = orderTClasses_buf[k].病人姓名;
                            value[(int)enum_病患資訊.年齡] = orderTClasses_buf[k].年齡;
                            value[(int)enum_病患資訊.性別] = orderTClasses_buf[k].性別;
                            value[(int)enum_病患資訊.病歷號] = orderTClasses_buf[k].病歷號;
                            value[(int)enum_病患資訊.處方日期] = orderTClasses_buf[k].開方日期.StringToDateTime().ToDateString();

                            list_value.Add(value);
                            break;
                        }
                    }   
                }
            }
            this.sqL_DataGridView_病患資訊.RefreshGrid(list_value);
        }

        private void Program_調劑畫面()
        {
            if(MySerialPort_Scanner01.IsConnected)
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
                if (orderClasses.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("", 1500);
                }
                if (orderClasses.Count > 0)
                {
                    string PRI_KEY = orderClasses[0].PRI_KEY;
                    this.sqL_DataGridView_病患資訊.SetSelectRow(enum_病患資訊.PRI_KEY.GetEnumName(), PRI_KEY);
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
            }));
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
            string 天數 = orderTClass.天數;
            string 總包數 = (包數.StringToInt32() * 天數.StringToInt32()).ToString();

            this.Invoke(new Action(delegate
            {
                rJ_Lable_處方藥品.Text = $"{orderTClass.藥品名稱}";
                rJ_Lable_領藥號.Text = $"{orderTClass.領藥號}";
                rJ_Lable_處方資訊_姓名_性別_病歷號.Text = $"{orderTClass.病人姓名}({orderTClass.性別}) {orderTClass.病歷號}";
                rJ_Lable_處方資訊_處方日期.Text = $"{orderTClass.開方日期.StringToDateTime().ToDateString()}";
                rJ_Lable_處方資訊_年齡.Text = $"{orderTClass.年齡.StringToInt32()}歲";
                rJ_Lable_處方資訊_單筆包數.Text = $"{包數}包";
                rJ_Lable_處方資訊_單筆處方天數.Text = $"{天數}天";
                rJ_Lable_處方資訊_單包重.Text = $"{(總重 / 總包數.StringToInt32()).ToString("0.00")} 克/包";
                rJ_Lable_處方資訊_單筆總重.Text = $"總重:{總重.ToString("0.00")}克";
                rJ_Lable_總包數.Text = $"{總包數}包";
                rJ_Lable_應調.Text = $"{orderTClass.交易量.StringToDouble() * -1}";
                rJ_Lable_醫師代號.Text = $"醫師代號 : {orderTClass.醫師ID}";
                rJ_Lable_處方時間.Text = $"處方時間 : {orderTClass.開方日期.StringToDateTime().ToTimeString()}";

                rJ_Lable_應調單位.Text = $"{orderTClass.劑量單位}";
                rJ_Lable_實調單位.Text = $"{orderTClass.劑量單位}";
            }));
        }
        private void Function_登入(sessionClass _sessionClass)
        {
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
            value[(int)enum_處方內容.GUID] = orderTClass.GUID;
            value[(int)enum_處方內容.藥名] = orderTClass.藥品名稱;
            value[(int)enum_處方內容.應調] = orderTClass.交易量;
            value[(int)enum_處方內容.實調] = orderTClass.實際調劑量;
            value[(int)enum_處方內容.天] = orderTClass.天數;
            value[(int)enum_處方內容.單位] = orderTClass.劑量單位;
            value[(int)enum_處方內容.服用方法] = orderTClass.頻次;
            return value;
        }
        #endregion
        #region Event
        private void SqL_DataGridView_處方內容_DataGridClearGridEvent()
        {
            this.Invoke(new Action(delegate
            {
                rJ_Lable_處方藥品.Text = $"-------------------";
                rJ_Lable_處方資訊_單筆包數.Text = $"-包";
                rJ_Lable_處方資訊_單筆處方天數.Text = $"--天";
                rJ_Lable_處方資訊_單包重.Text = $"--.-- 克/包";
                rJ_Lable_處方資訊_單筆總重.Text = $"總重:---克";
                rJ_Lable_總包數.Text = $"--包";
                rJ_Lable_應調.Text = $"-.--";
                rJ_Lable_醫師代號.Text = $"醫師代號 : ------------";
                rJ_Lable_處方時間.Text = $"處方時間 : --:--:--";
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

            this.Invoke(new Action(delegate
            {
                rJ_Lable_領藥號.Text = $"{領藥號}";
                rJ_Lable_處方資訊_姓名_性別_病歷號.Text = $"{姓名}({性別}) {病歷號}";
                rJ_Lable_處方資訊_處方日期.Text = $"{處方日期}";
                rJ_Lable_處方資訊_年齡.Text = $"{年齡}歲";
            }));
        

            List<OrderTClass> orderTClasses = OrderTClass.get_by_pri_key(Main_Form.API_Server, PRI_KEY);
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < orderTClasses.Count; i++)
            {
                object[] value = Funtion_orderTClassesToObject(orderTClasses[i]);
              
                list_value.Add(value);
            }
            this.sqL_DataGridView_處方內容.ClearGrid();
            this.sqL_DataGridView_處方內容.RefreshGrid(list_value);
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
            List<object[]> list_value = this.sqL_DataGridView_病患資訊.Get_All_Select_RowsValues();
            List<object[]> list_value_buf = new List<object[]>();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取病患資訊", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (MyMessageBox.ShowDialog("確定將所有處方設為調劑完成?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            string PRI_KEY = list_value[0][(int)enum_病患資訊.PRI_KEY].ObjectToString();
            List<OrderTClass> orderTClasses = OrderTClass.get_by_pri_key(Main_Form.API_Server, PRI_KEY);
            for (int i = 0; i < orderTClasses.Count; i++)
            {
                OrderTClass orderTClass = orderTClasses[i];
                orderTClass.實際調劑量 = (orderTClass.交易量.StringToDouble() * -1).ToString("0.00");
                orderTClass.藥師姓名 = sessionClass.Name;
                orderTClass.藥師ID = sessionClass.ID;
                orderTClass.過帳時間 = DateTime.Now.ToDateTimeString_6();
                object[] value = Funtion_orderTClassesToObject(orderTClass);
                OrderTClass.updete_by_guid(Main_Form.API_Server, orderTClass);
                list_value_buf.Add(value);
            }
         
            this.sqL_DataGridView_處方內容.ClearSelection();
            this.sqL_DataGridView_處方內容.ReplaceExtra(list_value_buf, true);
        }
   
        private void ToolStripMenuItem_處方內容_調劑完成_Click(object sender, EventArgs e)
        {
            List<object[]> list_value = this.sqL_DataGridView_處方內容.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
            }
            string GUID = list_value[0][(int)enum_處方內容.GUID].ObjectToString();
            OrderTClass orderTClass = OrderTClass.get_by_guid(Main_Form.API_Server, GUID);
            orderTClass.實際調劑量 = (orderTClass.交易量.StringToDouble() * -1).ToString("0.00");
            orderTClass.藥師姓名 = sessionClass.Name;
            orderTClass.藥師ID = sessionClass.ID;
            orderTClass.過帳時間 = DateTime.Now.ToDateTimeString_6();

            OrderTClass.updete_by_guid(Main_Form.API_Server, orderTClass);

            object[] value = Funtion_orderTClassesToObject(orderTClass);
            this.sqL_DataGridView_處方內容.ClearSelection();
            this.sqL_DataGridView_處方內容.ReplaceExtra(value, true);

        }
        #endregion
    }
}
