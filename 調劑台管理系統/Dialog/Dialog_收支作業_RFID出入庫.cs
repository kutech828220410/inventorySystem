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
using static 調劑台管理系統.Dialog_收支作業_RFID出入庫;

namespace 調劑台管理系統
{
    public partial class Dialog_收支作業_RFID出入庫 : MyDialog
    {
        public enum enum_import_export
        {
            入庫,
            出庫
        }
        public enum_import_export _Import_Export = enum_import_export.入庫;
        public List<StockClass> stockClasses = new List<StockClass>();
        private MyThread myThread = new MyThread();
        public Dialog_收支作業_RFID出入庫(enum_import_export enum_Import_Export)
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
            _Import_Export = enum_Import_Export;
            this.Text = _Import_Export == enum_import_export.入庫 ? "RFID入庫" : "RFID出庫";
            this.LoadFinishedEvent += Dialog_收支作業_RFID出入庫_LoadFinishedEvent;
            this.FormClosing += Dialog_收支作業_RFID出入庫_FormClosing;

            dateTimeIntervelPicker_更新時間.SetDateTime(DateTime.Now.AddYears(-1).GetStartDate(), DateTime.Now.AddDays(0).GetEndDate());
        }

        private void Dialog_收支作業_RFID出入庫_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(myThread != null)
            {
                myThread.Abort();
                myThread = null;
            }
         
        }

        private void Dialog_收支作業_RFID出入庫_LoadFinishedEvent(EventArgs e)
        {
 
        
            Table table = DrugHFTagClass.init(Main_Form.API_Server);
            this.sqL_DataGridView_TagList.RowsHeight = 50;
            this.sqL_DataGridView_TagList.Init(table);
            this.sqL_DataGridView_TagList.Set_ColumnVisible(false, new enum_DrugHFTag().GetEnumNames());
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.TagSN);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.藥碼);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_DrugHFTag.藥名);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.效期);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.批號);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.數量);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.更新時間);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.狀態);

            this.sqL_DataGridView_TagList.DataGridRefreshEvent += SqL_DataGridView_TagList_DataGridRefreshEvent;

            this.plC_RJ_Button_確認送出.MouseDownEvent += PlC_RJ_Button_確認送出_MouseDownEvent;
            this.plC_RJ_Button_解鎖.MouseDownEvent += PlC_RJ_Button_解鎖_MouseDownEvent;

            myThread.AutoRun(true);
            myThread.Add_Method(Program_HFRFID);
            myThread.SetSleepTime(100);
            myThread.Trigger();

            this.Refresh();
            Main_Form.Function_外門片解鎖();
        }
        private void SqL_DataGridView_TagList_DataGridRefreshEvent()
        {
            for(int i = 0; i < this.sqL_DataGridView_TagList.Rows.Count; i++)
            {
                string 狀態 = "";
                狀態 = this.sqL_DataGridView_TagList.Rows[i].Cells[(int)enum_DrugHFTag.狀態].Value.ToString();
                if ((狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName() && _Import_Export == enum_import_export.入庫) || (狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName() && _Import_Export == enum_import_export.出庫))
                {
                    this.sqL_DataGridView_TagList.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    this.sqL_DataGridView_TagList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void Program_HFRFID()
        {
            List<DrugHFTagClass> drugHFTagClasses = new List<DrugHFTagClass>();
            if (_Import_Export == enum_import_export.入庫) drugHFTagClasses = DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server);
            if (_Import_Export == enum_import_export.出庫) drugHFTagClasses = DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);
            if (drugHFTagClasses == null) return;

            drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.更新時間.StringToDateTime() >= dateTimeIntervelPicker_更新時間.StartTime && drugHFTagClass.更新時間.StringToDateTime() <= dateTimeIntervelPicker_更新時間.EndTime).ToList();

            List<object[]> list_drugHFTagClasses = new List<object[]>();

            List<string> uids = Main_Form.rfidReader_TagUID;

            for (int i = 0; i < drugHFTagClasses.Count; i++)
            {
                if (uids.Contains(drugHFTagClasses[i].TagSN))
                {
                    if (_Import_Export == enum_import_export.入庫)
                    {
                        drugHFTagClasses[i].狀態 = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                        list_drugHFTagClasses.Add(drugHFTagClasses[i].ClassToSQL<DrugHFTagClass, enum_DrugHFTag>());
                    }
                }
                else
                {
                    if (_Import_Export == enum_import_export.出庫)
                    {
                        drugHFTagClasses[i].狀態 = enum_DrugHFTagStatus.出庫註記.GetEnumName();
                        list_drugHFTagClasses.Add(drugHFTagClasses[i].ClassToSQL<DrugHFTagClass, enum_DrugHFTag>());
                    }                  
                }
            }

            this.sqL_DataGridView_TagList.RefreshGrid(list_drugHFTagClasses);
        }
        private void PlC_RJ_Button_解鎖_MouseDownEvent(MouseEventArgs mevent)
        {
            Main_Form.Function_外門片解鎖();
        }
        private void PlC_RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_drugHFTagClasses = this.sqL_DataGridView_TagList.GetAllRows();
            if (list_drugHFTagClasses.Count == 0)
            {
                MyMessageBox.ShowDialog("未讀取到RFID標籤");
                return;
            }
            List<DrugHFTagClass> drugHFTagClasses = list_drugHFTagClasses.SQLToClass<DrugHFTagClass, enum_DrugHFTag>();
            if (_Import_Export == enum_import_export.入庫) drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()).ToList();
            if (_Import_Export == enum_import_export.出庫) drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName()).ToList();

            stockClasses = drugHFTagClasses.GetStockClasses();

            StringBuilder sb = new StringBuilder();
            if (_Import_Export == enum_import_export.入庫) sb.AppendLine($"入庫藥品品項：{stockClasses.Count}");
            if (_Import_Export == enum_import_export.出庫)
            {
                sb.AppendLine($"出庫藥品品項：{stockClasses.Count}");
               
            }
            sb.AppendLine(new string('-', 30)); // 分隔線

            for (int i = 0; i < stockClasses.Count; i++)
            {
                if (_Import_Export == enum_import_export.出庫)
                {
                    StockClass stockClass = Main_Form.Function_取得庫存值從雲端資料(stockClasses[i].Code, stockClasses[i].Validity_period);
                    if (stockClass == null)
                    {
                        MyMessageBox.ShowDialog($"藥品：{stockClasses[i].Code} 無法取得庫存資訊");
                        return;
                    }
                    if(stockClass.Qty.StringToDouble() < stockClasses[i].Qty.StringToDouble())
                    {
                        MyMessageBox.ShowDialog($"藥品 : {stockClasses[i].Code} (效期 : {stockClasses[i].Validity_period}) 庫存不足，無法出庫");
                        return;
                    }
                }
                  
                sb.AppendLine($"[{i + 1}]");
                sb.AppendLine($"藥碼：{stockClasses[i].Code}");
                sb.AppendLine($"藥名：{stockClasses[i].Name}");
                sb.AppendLine($"數量：{stockClasses[i].Qty}");
                sb.AppendLine($"效期：{stockClasses[i].Validity_period}");
                sb.AppendLine($"批號：{stockClasses[i].Lot_number}");
                sb.AppendLine(); // 空一行
            }

            if (MyMessageBox.ShowDialog(sb.ToString(), MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            LoadingForm.ShowLoadingForm();
            DrugHFTagClass.add(Main_Form.API_Server, drugHFTagClasses);
            LoadingForm.CloseLoadingForm();

            this.DialogResult = DialogResult.Yes;
      
            this.Close();

        }
    }
}
