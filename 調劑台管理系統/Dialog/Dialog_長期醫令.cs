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
    public partial class Dialog_長期醫令 : MyDialog
    {
        private List<medClass> medClasses_cloud = new List<medClass>();
        Dictionary<string, List<medClass>> keyValuePairs_med_cloud;
        private MyThread myThread_program = null;
        public personPageClass personPageClass = new personPageClass();
        public string deviceName = "";
        public enum enum_用藥資訊_總量
        {
            [Description("GUID,VARCHAR,100,PRIMARY")]
            GUID,
            [Description("藥碼,VARCHAR,100,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,100,NONE")]
            藥名,
            [Description("處方數量,VARCHAR,100,NONE")]
            處方數量,
            [Description("已調量,VARCHAR,100,NONE")]
            已調量,
            [Description("總量,VARCHAR,100,NONE")]
            總量,
        }
        public Dialog_長期醫令(personPageClass personPageClass , string deviceName)
        {
            this.personPageClass = personPageClass;
            this.deviceName = deviceName;
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
            }));
            this.LoadFinishedEvent += Dialog_長期醫令_LoadFinishedEvent;
            this.FormClosing += Dialog_長期醫令_FormClosing;
            this.rJ_Button_批次調劑.MouseDownEvent += RJ_Button_批次調劑_MouseDownEvent;

        }

        private void Dialog_長期醫令_FormClosing(object sender, FormClosingEventArgs e)
        {
            myThread_program.Abort();
            myThread_program = null;
        }
        private void Dialog_長期醫令_LoadFinishedEvent(EventArgs e)
        {
            this.sqL_DataGridView_護理站列表.RowsHeight = 40;
            this.sqL_DataGridView_護理站列表.Init(new Table(new enum_nursingStation()));
            if (Main_Form.PLC_Device_批次領藥_藥品總量調劑.Bool == false)
            {
                this.sqL_DataGridView_護理站列表.Set_ColumnVisible(false, new enum_nursingStation().GetEnumNames());
                this.sqL_DataGridView_護理站列表.Set_ColumnWidth(100, enum_nursingStation.代碼);
                this.sqL_DataGridView_護理站列表.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleCenter, enum_nursingStation.名稱);
                this.sqL_DataGridView_護理站列表.RowEnterEvent += SqL_DataGridView_護理站列表_RowEnterEvent;
                this.sqL_DataGridView_護理站列表.RowClickEvent += SqL_DataGridView_護理站列表_RowClickEvent;
                List<nursingStationClass> nursingStationClasses = nursingStationClass.get_all(Main_Form.API_Server);
                List<object[]> list_nursingStation = nursingStationClasses.ClassToSQL<nursingStationClass, enum_nursingStation>();
                this.sqL_DataGridView_護理站列表.RefreshGrid(list_nursingStation);
            }
            else
            {
                checkBox_管1.Checked = true;
                checkBox_管2.Checked = true;
                checkBox_管3.Checked = true;
                checkBox_管4.Checked = true;
                this.sqL_DataGridView_護理站列表.Visible = false;
            }


            this.sqL_DataGridView_用藥資訊.RowsHeight = 40;
            this.sqL_DataGridView_用藥資訊.Init(new Table(new enum_用藥資訊_總量()));
            this.sqL_DataGridView_用藥資訊.Set_ColumnVisible(false, new enum_用藥資訊_總量().GetEnumNames());
            this.sqL_DataGridView_用藥資訊.Set_ColumnWidth(150, enum_用藥資訊_總量.藥碼);
            this.sqL_DataGridView_用藥資訊.Set_ColumnWidth(600, DataGridViewContentAlignment.MiddleLeft, enum_用藥資訊_總量.藥名);
            this.sqL_DataGridView_用藥資訊.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_用藥資訊_總量.處方數量);
            this.sqL_DataGridView_用藥資訊.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_用藥資訊_總量.已調量);
            this.sqL_DataGridView_用藥資訊.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_用藥資訊_總量.總量);
            this.sqL_DataGridView_用藥資訊.RefreshGrid();
            Refresh();

            medClasses_cloud = medClass.get_med_cloud($"{Main_Form.API_Server}");
            keyValuePairs_med_cloud = medClasses_cloud.CoverToDictionaryByCode();

            myThread_program = new MyThread();
            myThread_program.Add_Method(sub_program);
            myThread_program.AutoRun(true);
            myThread_program.SetSleepTime(1000);
            myThread_program.Trigger();


        }
        private void sub_program()
        {

            try
            {
                List<object[]> list_value = new List<object[]>();
                List<OrderClass> orderClasses = new List<OrderClass>();
                //LoadingForm.ShowLoadingForm();
                if (Main_Form.PLC_Device_批次領藥_藥品總量調劑.Bool == false)
                {
                    List<object[]> list_護理站列表 = sqL_DataGridView_護理站列表.Get_All_Select_RowsValues();
                    if (list_護理站列表.Count == 0) return;
                    object[] RowValue = list_護理站列表[0];
                    string 護理站代碼 = RowValue[(int)enum_nursingStation.代碼].ObjectToString();

                    orderClasses = OrderClass.get_by_nursingstation_day(Main_Form.API_Server, 護理站代碼, rJ_DatePicker_日期.Value);
                }
                else
                {
                    orderClasses = OrderClass.get_batch_order_by_day(Main_Form.API_Server, rJ_DatePicker_日期.Value);
    
                }
                Dictionary<string, List<OrderClass>> keyValuePairs_order = orderClasses.CoverToDictionaryBy_Code();
                foreach (string key in keyValuePairs_order.Keys)
                {
                    List<OrderClass> orderClasses_temp = keyValuePairs_order[key];
                    object[] value = new object[new enum_用藥資訊_總量().GetLength()];
                    if (orderClasses_temp.Count == 0) continue;
                    if (checkBox_顯示有儲位藥品.Checked)
                    {
                        if (Main_Form.Function_從本地資料取得儲位(orderClasses_temp[0].藥品碼).Count == 0)
                        {
                            continue;
                        }
                    }
                    if (orderClasses_temp[0].藥品碼.StringIsEmpty()) continue;
                    List<medClass> medClasses_buf = keyValuePairs_med_cloud.SortDictionaryByCode(orderClasses_temp[0].藥品碼);
                    if (medClasses_buf.Count == 0) continue;
                    else
                    {
                        string 管制級別 = medClasses_buf[0].管制級別;
                        if (checkBox_管1.Checked == false && 管制級別 == "1") continue;
                        if (checkBox_管2.Checked == false && 管制級別 == "2") continue;
                        if (checkBox_管3.Checked == false && 管制級別 == "3") continue;
                        if (checkBox_管4.Checked == false && 管制級別 == "4") continue;
                        if (checkBox_N.Checked == false && (管制級別 == "N" || 管制級別.StringIsEmpty())) continue;
                    }

                    value[(int)enum_用藥資訊_總量.GUID] = orderClasses_temp[0].藥品碼;
                    value[(int)enum_用藥資訊_總量.藥碼] = orderClasses_temp[0].藥品碼;
                    value[(int)enum_用藥資訊_總量.藥名] = orderClasses_temp[0].藥品名稱;
                    double 總量 = 0;
                    double 實調量 = 0;
                    for (int i = 0; i < orderClasses_temp.Count; i++)
                    {
                        總量 += orderClasses_temp[i].交易量.StringToDouble();
                        if (orderClasses_temp[i].實際調劑量.StringIsInt32()) 實調量 += orderClasses_temp[i].實際調劑量.StringToDouble();
                    }
                    總量 *= -1;
                    實調量 *= -1;
                    value[(int)enum_用藥資訊_總量.處方數量] = orderClasses_temp.Count;
                    value[(int)enum_用藥資訊_總量.總量] = 總量;
                    value[(int)enum_用藥資訊_總量.已調量] = 實調量;
                    list_value.Add(value);
                }
                sqL_DataGridView_用藥資訊.RefreshGrid(list_value);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowDialog($"Exception : {ex.Message}");
            }
            finally
            {
                //LoadingForm.CloseLoadingForm();
            }
        }
        private void SqL_DataGridView_護理站列表_RowClickEvent(object[] RowValue)
        {

          

        }
        private void SqL_DataGridView_護理站列表_RowEnterEvent(object[] RowValue)
        {
        
        }

        private void RJ_Button_批次調劑_MouseDownEvent(MouseEventArgs mevent)
        {
            List<OrderClass> orderClasses = new List<OrderClass>();
            List<OrderClass> orderClasse_buf = new List<OrderClass>();
            string 護理站代碼 = "";
            string 護理站名稱 = "";

            if (Main_Form.PLC_Device_批次領藥_藥品總量調劑.Bool == false)
            {
                List<object[]> list_護理站列表 = sqL_DataGridView_護理站列表.Get_All_Select_RowsValues();
                if (list_護理站列表.Count == 0)
                {
                    MyMessageBox.ShowDialog("未選取護理站");
                    return;
                }
                護理站代碼 = list_護理站列表[0][(int)enum_nursingStation.代碼].ObjectToString();
                護理站名稱 = list_護理站列表[0][(int)enum_nursingStation.名稱].ObjectToString();
                orderClasses = OrderClass.get_by_nursingstation_day(Main_Form.API_Server, 護理站代碼, rJ_DatePicker_日期.Value);
            }
            else
            {
                護理站代碼 = "批次領藥";
                orderClasses = OrderClass.get_batch_order_by_day(Main_Form.API_Server, rJ_DatePicker_日期.Value);
         
            }
       
            Dictionary<string, List<OrderClass>> keyValuePairs_order = orderClasses.CoverToDictionaryBy_Code();
            foreach (string key in keyValuePairs_order.Keys)
            {
                List<OrderClass> orderClasses_temp = keyValuePairs_order[key];
                object[] value = new object[new enum_用藥資訊_總量().GetLength()];
                if (orderClasses_temp.Count == 0) continue;
                if (orderClasses_temp[0].藥品碼.StringIsEmpty()) continue;
                if (Main_Form.Function_從本地資料取得儲位(orderClasses_temp[0].藥品碼).Count == 0)
                {
                    continue;
                }

                for (int i = 0; i < orderClasses_temp.Count; i++)
                {
                    if (orderClasses_temp[i].實際調劑量.StringToDouble() * -1 < orderClasses_temp[i].交易量.StringToDouble() * -1)
                    {
                        orderClasse_buf.Add(orderClasses_temp[i]);
                    }
                }
            }


            if (MyMessageBox.ShowDialog($"「({護理站代碼}){護理站名稱}」是否執行批次調劑,共<{orderClasse_buf.Count}>筆處方?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            List<object[]> list_堆疊資料 = Main_Form.Function_取藥堆疊資料_取得母資料();
            List<object[]> list_堆疊資料_buf = new List<object[]>();
            List<string> Codes = (from temp in orderClasse_buf
                                  select temp.藥品碼).Distinct().ToList();
            List<medClass> medClasses = medClass.get_med_clouds_by_codes(Main_Form.API_Server, Codes);
            List<medClass> medClasses_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_medcloud = medClasses.CoverToDictionaryByCode();
            string ID = personPageClass.ID;
            string 操作人 = personPageClass.姓名;
            string 藥師證字號 = personPageClass.藥師證字號;
            string 顏色 = personPageClass.顏色;

            for (int i = 0; i < orderClasse_buf.Count; i++)
            {
                OrderClass orderClass = orderClasse_buf[i];
                string GUID = Guid.NewGuid().ToString();
                string 調劑台名稱 = Main_Form.ServerName;
                enum_交易記錄查詢動作 動作 = enum_交易記錄查詢動作.系統領藥;

                medClasses_buf = keyValuePairs_medcloud.SortDictionaryByCode(orderClass.藥品碼);
                bool flag_檢查過帳 = false;
                if (medClasses_buf.Count > 0)
                {
                    orderClass.藥品名稱 = medClasses_buf[0].藥品名稱;
                    orderClass.劑量單位 = medClasses_buf[0].包裝單位;
                    if (medClasses_buf[0].高價藥品.ToUpper() == true.ToString().ToUpper())
                    {
                        flag_檢查過帳 = true;
                    }
                    if (medClasses_buf[0].管制級別.StringIsEmpty() == false && medClasses_buf[0].管制級別 != "N")
                    {
                        flag_檢查過帳 = true;
                    }
                }

                list_堆疊資料_buf = (from temp in list_堆疊資料
                                 where temp[(int)enum_取藥堆疊母資料.藥品碼].ObjectToString() == orderClass.藥品碼
                                 where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != "刷新面板"
                                 where temp[(int)enum_取藥堆疊母資料.調劑台名稱].ObjectToString() != 調劑台名稱
                                 where temp[(int)enum_取藥堆疊母資料.操作人].ObjectToString() != 操作人
                                 select temp).ToList();



                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();


                if (flag_檢查過帳 == true)
                {
                    if (orderClass.狀態 == enum_醫囑資料_狀態.已過帳.GetEnumName())
                    {
                        if (orderClass.實際調劑量.StringIsDouble() == false)
                        {
                            takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.已領用過.GetEnumName();
                        }
                        else
                        {
                            if (orderClass.交易量 == orderClass.實際調劑量)
                            {
                                takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.已領用過.GetEnumName();
                            }
                            else
                            {
                                orderClass.交易量 = (orderClass.交易量.StringToDouble() - orderClass.實際調劑量.StringToDouble()).ToString();
                            }
                        }

                    }

                }

                takeMedicineStackClass.GUID = GUID;
                takeMedicineStackClass.Order_GUID = orderClass.GUID;
                takeMedicineStackClass.序號 = orderClass.批序;
                takeMedicineStackClass.動作 = 動作.GetEnumName();
                takeMedicineStackClass.調劑台名稱 = deviceName;
                takeMedicineStackClass.藥品碼 = orderClass.藥品碼;
                takeMedicineStackClass.領藥號 = orderClass.領藥號;
                takeMedicineStackClass.病房號 = orderClass.病房;
                takeMedicineStackClass.診別 = orderClass.藥局代碼;
                takeMedicineStackClass.顏色 = 顏色;

                takeMedicineStackClass.藥品名稱 = orderClass.藥品名稱;
                takeMedicineStackClass.單位 = orderClass.劑量單位;
                takeMedicineStackClass.藥袋序號 = orderClass.PRI_KEY;
                takeMedicineStackClass.病歷號 = orderClass.病歷號;
                takeMedicineStackClass.病人姓名 = orderClass.病人姓名;
                takeMedicineStackClass.床號 = orderClass.床號;
                takeMedicineStackClass.開方時間 = orderClass.開方日期;
                takeMedicineStackClass.操作人 = 操作人;
                takeMedicineStackClass.ID = ID;
                takeMedicineStackClass.藥師證字號 = 藥師證字號;
                takeMedicineStackClass.總異動量 = orderClass.交易量;
                takeMedicineStackClass.收支原因 = "批領作業";
                takeMedicineStackClass.操作時間 = DateTime.Now.ToDateTimeString_6();


                if (orderClass.批序.StringIsEmpty() == false)
                {
                    if (orderClass.批序.Contains("DC"))
                    {
                        takeMedicineStackClass.備註 = "[DC處方]";
                        takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.DC處方.GetEnumName();
                    }
                    else if (orderClass.批序.Contains("NEW"))
                    {
                        takeMedicineStackClass.備註 = "[NEW處方]";
                    }
                }



                takeMedicineStackClasses.Add(takeMedicineStackClass);

            }

            List<object[]> list_takemed = takeMedicineStackClasses.ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>();
            Main_Form._sqL_DataGridView_取藥堆疊母資料.SQL_AddRows(list_takemed, false);
        }
    }
}
