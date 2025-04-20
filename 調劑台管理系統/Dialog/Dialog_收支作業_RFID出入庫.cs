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
    public partial class Dialog_收支作業_RFID出入庫 : MyDialog
    {
        public List<StockClass> stockClasses = new List<StockClass>();
        private MyThread myThread = new MyThread();
        public Dialog_收支作業_RFID出入庫()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
          
            this.LoadFinishedEvent += Dialog_收支作業_RFID出入庫_LoadFinishedEvent;
            this.FormClosing += Dialog_收支作業_RFID出入庫_FormClosing;
        }

        private void Dialog_收支作業_RFID出入庫_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void Dialog_收支作業_RFID出入庫_LoadFinishedEvent(EventArgs e)
        {
            if (Main_Form.rfidReader.IsOpen == false)
            {
                MyMessageBox.ShowDialog("RFID讀取器無法開啟，請檢查連接");
                return;
            }

            dateTimeIntervelPicker_更新時間.StartTime = DateTime.Now.AddDays(0).GetStartDate();
            dateTimeIntervelPicker_更新時間.EndTime = DateTime.Now.AddDays(0).GetEndDate();

         

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

            myThread.AutoRun(true);
            myThread.Add_Method(Program_HFRFID);
            myThread.SetSleepTime(100);
            myThread.Trigger();
        }

       

        private void SqL_DataGridView_TagList_DataGridRefreshEvent()
        {
            for(int i = 0; i < this.sqL_DataGridView_TagList.Rows.Count; i++)
            {
                if (this.sqL_DataGridView_TagList.Rows[i].Cells[(int)enum_DrugHFTag.狀態].Value.ToString() == enum_DrugHFTagStatus.入庫註記.GetEnumName())
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
            if (Main_Form.rfidReader.IsOpen == true)
            {
                List<DrugHFTagClass> drugHFTagClasses = DrugHFTagClass.get_latest_reset_tag(Main_Form.API_Server);
                if (drugHFTagClasses == null) return;

                drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.更新時間.StringToDateTime() >= dateTimeIntervelPicker_更新時間.StartTime && drugHFTagClass.更新時間.StringToDateTime() <= dateTimeIntervelPicker_更新時間.EndTime).ToList();

                List<object[]> list_drugHFTagClasses = drugHFTagClasses.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>();

                List<string> uids = Main_Form.rfidReader_TagUID;

                for (int i = 0; i < list_drugHFTagClasses.Count; i++)
                {
                    if (uids.Contains(list_drugHFTagClasses[i][(int)enum_DrugHFTag.TagSN].ObjectToString()))
                    {
                        list_drugHFTagClasses[i][(int)enum_DrugHFTag.狀態] = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                    }
                }

                this.sqL_DataGridView_TagList.RefreshGrid(list_drugHFTagClasses);
            }
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
            drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()).ToList();

            stockClasses = drugHFTagClasses.GetStockClasses();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"入庫藥品品項：{stockClasses.Count}");
            sb.AppendLine(new string('-', 30)); // 分隔線

            for (int i = 0; i < stockClasses.Count; i++)
            {
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
            myThread.Abort();
            myThread = null;
            this.Close();

        }
    }
}
