using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;
using MyUI;
using SQLUI;
using DrawingClass;
using H_Pannel_lib;
namespace 癌症備藥機
{
    public partial class Dialog_備藥清單 : MyDialog
    {
        public delegate void SureClickEventHandler(List<udnoectc_orders> udnoectc_Orders);
        public event SureClickEventHandler SureClickEvent;

        private string _login_name = "";
        private string GUID = "";
        private udnoectc udnoectc = null;

        public List<udnoectc_orders> Value = null;
        public List<StockClass> stockClasses = new List<StockClass>();
        public Dialog_備藥清單(string guid , string login_name)
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
            LoadingForm.ShowLoadingForm();
            this.GUID = guid;
            udnoectc udnoectc = udnoectc.get_udnoectc_by_GUID(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, GUID);
            this.Load += Dialog_備藥清單_Load;
            if (udnoectc == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("異常:查無資料", 2000);
                form.Invoke(new Action(delegate
                {
                    this.Close();
                    this.DialogResult = DialogResult.No;
                }));
            }
            this._login_name = login_name;
            this.udnoectc = udnoectc;
            this.LoadFinishedEvent += Dialog_備藥清單_LoadFinishedEvent;

            Logger.Log($"[備藥清單] Form 開啟...");
        }

        private void Dialog_備藥清單_LoadFinishedEvent(EventArgs e)
        {
            this.uc_備藥通知內容.Enabled = !udnoectc.醫囑確認藥師.StringIsEmpty();
            this.plC_RJ_Button_醫令確認.Enabled = !this.uc_備藥通知內容.Enabled;
            this.label_狀態.Visible = this.plC_RJ_Button_醫令確認.Enabled;
            this.uc_備藥通知內容.Init(udnoectc, _login_name, false);
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.plC_RJ_Button_變異紀錄.MouseDownEvent += PlC_RJ_Button_變異紀錄_MouseDownEvent;
            this.plC_RJ_Button_醫令確認.MouseDownEvent += PlC_RJ_Button_醫令確認_MouseDownEvent;
            LoadingForm.CloseLoadingForm();
        }

    

        private void Dialog_備藥清單_Load(object sender, EventArgs e)
        {         
         
        }
     
        #region Event
        private void PlC_RJ_Button_變異紀錄_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Logger.Log($"[備藥清單] 變異紀錄開啟");
                Dialog_變異紀錄 dialog_變異紀錄 = new Dialog_變異紀錄(this.GUID);
                dialog_變異紀錄.ShowDialog();
            }));
        }
        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                stockClasses.Clear();
                List<object[]> list_value = this.uc_備藥通知內容.GetSelectedRows();
                if (list_value.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取備藥藥品", 1500);
                    dialog_AlarmForm.ShowDialog();
                    Logger.Log($"[備藥清單] 未選取備藥藥品...");
                    return;
                }
                List<udnoectc_orders> list_udnoectc_orders = new List<udnoectc_orders>();
                List<udnoectc_orders> list_udnoectc_orders_replace = new List<udnoectc_orders>();
                List<StockClass> stockClasses_buf = new List<StockClass>();
                LoadingForm.ShowLoadingForm();
                for (int i = 0; i < list_value.Count; i++)
                {
                    list_udnoectc_orders = list_value[i][0].ObjectToString().JsonDeserializet<List<udnoectc_orders>>();
                    list_udnoectc_orders_replace.LockAdd(list_udnoectc_orders);
                    for (int k = 0; k < list_udnoectc_orders.Count; k++)
                    {
                        string 藥碼 = list_udnoectc_orders[k].藥碼;
                        string 藥名 = list_udnoectc_orders[k].藥名;
                        int 數量 = list_udnoectc_orders[k].數量.StringToInt32();
                        stockClasses_buf = (from temp in stockClasses
                                            where temp.Code == 藥碼
                                            select temp).ToList();
                        if (stockClasses_buf.Count == 0)
                        {
                            StockClass stockClass = new StockClass();
                            stockClass.Code = 藥碼;
                            stockClass.Name = 藥名;
                            stockClass.Qty = 數量.ToString();
                            stockClasses.Add(stockClass);
                        }
                        else
                        {
                            StockClass stockClass = stockClasses_buf[0];
                            stockClass.Code = 藥碼;
                            stockClass.Name = 藥名;
                            stockClass.Qty = (stockClass.Qty.StringToInt32() + 數量).ToString();
                        }
                    }
                }
                string error_msg = "";
                for (int i = 0; i < stockClasses.Count; i++)
                {
                    string 藥碼 = stockClasses[i].Code;
                    string 藥名 = stockClasses[i].Name;
                    int 數量 = stockClasses[i].Qty.StringToInt32();
                    int 庫存 = Main_Form.Function_從SQL取得庫存(藥碼);
                    Logger.Log($"[備藥清單] 領用,({藥碼}){藥名} ,數量:{數量},庫存{庫存}");
                    if (數量 > 庫存)
                    {
                        error_msg += $"({藥碼}){藥名},領用:{數量},庫存{庫存}\n";
                    }
                }

                if (error_msg.StringIsEmpty() == false)
                {
                    MyMessageBox.ShowDialog($"{error_msg}");
                    Logger.Log($"[備藥清單] error_msg : {error_msg}");
                    return;
                }

                Logger.Log($"[備藥清單] 選取備藥藥品<{list_value.Count}>筆");
                Dialog_藥盒掃描 dialog_藥盒掃描 = new Dialog_藥盒掃描();
                if (dialog_藥盒掃描.ShowDialog() != DialogResult.Yes)
                {
                    Logger.Log($"[備藥清單] 掃描藥盒取消...");
                    return;
                }
                Logger.Log($"[備藥清單] 掃描藥盒<{dialog_藥盒掃描.Value}>");


                List<object[]> list_藥盒索引 = Main_Form._sqL_DataGridView_藥盒索引.SQL_GetRows((int)enum_drugBoxIndex.barcode, dialog_藥盒掃描.Value, false);
                if(list_藥盒索引.Count > 0)
                {
                    object[] value = list_藥盒索引[0];
                    value[(int)enum_drugBoxIndex.barcode] = dialog_藥盒掃描.Value;
                    value[(int)enum_drugBoxIndex.master_GUID] = udnoectc.GUID;
                    Main_Form._sqL_DataGridView_藥盒索引.SQL_ReplaceExtra(value, false);
                }
                else
                {
                    object[] value = new object[new enum_drugBoxIndex().GetLength()];
                    value[(int)enum_drugBoxIndex.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_drugBoxIndex.barcode] = dialog_藥盒掃描.Value;
                    value[(int)enum_drugBoxIndex.master_GUID] = udnoectc.GUID;
                    Main_Form._sqL_DataGridView_藥盒索引.SQL_AddRow(value, false);
                }

                List<udnoectc_orders> list_udnoectc_orders_return = udnoectc.update_udnoectc_orders_comp(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, list_udnoectc_orders_replace, this._login_name);
                Logger.Log($"[備藥清單] 成功確認備藥<{list_udnoectc_orders_return.Count}>筆");
     
                if (SureClickEvent != null) SureClickEvent(list_udnoectc_orders_return);
                this.DialogResult = DialogResult.Yes;
                Value = list_udnoectc_orders_return;
                this.Close();
            }
            catch(Exception ex)
            {
                Logger.Log($"[備藥清單] Exception : {ex.Message}");


            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
          
     

        }
        private void PlC_RJ_Button_醫令確認_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否確認醫令?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
        

            LoadingForm.ShowLoadingForm();
            udnoectc.update_udnoectc_confirm_ph(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, this._login_name, udnoectc);
            udnoectc = udnoectc.get_udnoectc_by_GUID(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, udnoectc.GUID);
            this.uc_備藥通知內容.Init(udnoectc, _login_name, false);
            this.Invoke(new Action(delegate 
            {
                this.uc_備藥通知內容.Enabled = !udnoectc.醫囑確認藥師.StringIsEmpty();
                this.plC_RJ_Button_醫令確認.Enabled = !this.uc_備藥通知內容.Enabled;
                this.label_狀態.Visible = this.plC_RJ_Button_醫令確認.Enabled;
            }));
            Logger.Log($"[備藥清單] 醫囑確認,病人{udnoectc.病人姓名}({udnoectc.病歷號}) 藥師:{_login_name}");
            LoadingForm.CloseLoadingForm();
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            Logger.Log($"[備藥清單] Form 返回...");
            this.Invoke(new Action(delegate
            {
                this.Close();
                this.DialogResult = DialogResult.No;
            }));
        }
        #endregion
    }
}
