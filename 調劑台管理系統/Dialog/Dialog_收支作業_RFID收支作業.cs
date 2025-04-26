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
using static 調劑台管理系統.Dialog_收支作業_RFID收支作業;

namespace 調劑台管理系統
{
    public partial class Dialog_收支作業_RFID收支作業 : MyDialog
    {
        private string 調劑台名稱 = $"{Main_Form.ServerName}";

        DrugHFTag_IncomeOutcomeListClass _drugHFTag_IncomeOutcomeList = null;
        DrugHFTag_IncomeOutcomeListClass drugHFTag_IncomeOutcomeList
        {
            get
            {
                return _drugHFTag_IncomeOutcomeList;
            }
            set
            {
                if(value == null)
                {
                    form.Invoke(new Action(delegate
                    {
                        rJ_Lable_藥名.Text = "--------------------------";
                        rJ_Lable_數量.Text = "----";
                        rJ_Lable_異常.Text = "----";
                    }));
                    return;
                }
                form.Invoke(new Action(delegate
                {
                    rJ_Lable_藥名.Text = value.藥名;
                    rJ_Lable_數量.Text = "----";
                    rJ_Lable_異常.Text = "----";

                }));
                _drugHFTag_IncomeOutcomeList = value;
            }
        }
        public IncomeOutcomeMode _Import_Export = IncomeOutcomeMode.收入;
        public List<StockClass> stockClasses = new List<StockClass>();
        private MyThread myThread_HFRFID = new MyThread();
        private MyThread myThread_UI = new MyThread();
        public Dialog_收支作業_RFID收支作業(IncomeOutcomeMode IncomeOutcomeMode)
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
            _Import_Export = IncomeOutcomeMode;
            this.Text = _Import_Export == IncomeOutcomeMode.收入 ? "RFID收入" : "RFID出庫";
            this.LoadFinishedEvent += Dialog_收支作業_RFID出收入_LoadFinishedEvent;
            this.FormClosing += Dialog_收支作業_RFID出收入_FormClosing;

            dateTimeIntervelPicker_報表時間.SetDateTime(DateTime.Now.AddDays(0).GetStartDate(), DateTime.Now.AddDays(0).GetEndDate());
        }

        private void Dialog_收支作業_RFID出收入_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(myThread_HFRFID != null)
            {
                myThread_HFRFID.Abort();
                myThread_HFRFID = null;
            }
            if (myThread_UI != null)
            {
                myThread_UI.Abort();
                myThread_UI = null;
            }
        }

        private void Dialog_收支作業_RFID出收入_LoadFinishedEvent(EventArgs e)
        {
 
        
            this.sqL_DataGridView_TagList.RowsHeight = 50;
            this.sqL_DataGridView_TagList.Init(DrugHFTagClass.init(Main_Form.API_Server));
            this.sqL_DataGridView_TagList.Set_ColumnVisible(false, new enum_DrugHFTag().GetEnumNames());
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.TagSN);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.藥碼);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_DrugHFTag.藥名);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.效期);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.批號);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.數量);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.更新時間);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.狀態);
            this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.TagSN);
            this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.效期);
            this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.更新時間);
            this.sqL_DataGridView_TagList.DataGridRefreshEvent += SqL_DataGridView_TagList_DataGridRefreshEvent;

            this.sqL_DataGridView_收支清單.RowsHeight = 60;
            this.sqL_DataGridView_收支清單.Init(new Table(new enum_DrugHFTag_IncomeOutcomeList()));
            this.sqL_DataGridView_收支清單.Set_ColumnVisible(false, new enum_DrugHFTag_IncomeOutcomeList().GetEnumNames());
            this.sqL_DataGridView_收支清單.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag_IncomeOutcomeList.藥碼);
            this.sqL_DataGridView_收支清單.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_DrugHFTag_IncomeOutcomeList.藥名);
            this.sqL_DataGridView_收支清單.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag_IncomeOutcomeList.收支數量);
            this.sqL_DataGridView_收支清單.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag_IncomeOutcomeList.報表數量);

            Table table = takeMedicineStackClass.init(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType).GetTable(new enum_取藥堆疊母資料());
            this.sqL_DataGridView_取藥狀態.InitEx(table);
            this.sqL_DataGridView_取藥狀態.Set_ColumnVisible(false, new enum_取藥堆疊母資料().GetEnumNames());
            this.sqL_DataGridView_取藥狀態.Set_ColumnVisible(true, enum_取藥堆疊母資料.藥品碼, enum_取藥堆疊母資料.藥品名稱, enum_取藥堆疊母資料.總異動量, enum_取藥堆疊母資料.結存量, enum_取藥堆疊母資料.狀態);
            this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.庫存量);
            this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.狀態);
            this.sqL_DataGridView_取藥狀態.Set_ColumnText("藥碼", enum_取藥堆疊母資料.藥品碼);
            this.sqL_DataGridView_取藥狀態.Set_ColumnText("藥名", enum_取藥堆疊母資料.藥品名稱);
            this.sqL_DataGridView_取藥狀態.Set_ColumnText("庫存", enum_取藥堆疊母資料.庫存量);
            this.sqL_DataGridView_取藥狀態.Set_ColumnText("異動", enum_取藥堆疊母資料.總異動量);
            this.sqL_DataGridView_取藥狀態.Set_ColumnText("結存", enum_取藥堆疊母資料.結存量);
            this.sqL_DataGridView_取藥狀態.DataGridRefreshEvent += SqL_DataGridView_取藥狀態_DataGridRefreshEvent;


            this.rJ_Button_選擇.MouseDownEvent += RJ_Button_選擇_MouseDownEvent;
            this.plC_RJ_Button_解鎖.MouseDownEvent += PlC_RJ_Button_解鎖_MouseDownEvent;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;

            myThread_HFRFID.AutoRun(true);
            myThread_HFRFID.Add_Method(Program_HFRFID);
            myThread_HFRFID.SetSleepTime(100);
            myThread_HFRFID.Trigger();

            myThread_UI.AutoRun(true);
            myThread_UI.Add_Method(Program_UI);
            myThread_UI.SetSleepTime(100);
            myThread_UI.Trigger();

            this.Refresh();
            Main_Form.Function_外門片解鎖();
        }

    

        private void SqL_DataGridView_取藥狀態_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_取藥狀態.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_取藥狀態.dataGridView.Rows[i].Cells[(int)enum_取藥堆疊母資料.狀態].Value.ToString();
                if (狀態 == enum_取藥堆疊母資料_狀態.等待作業.GetEnumName())
                {
                    this.sqL_DataGridView_取藥狀態.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    this.sqL_DataGridView_取藥狀態.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.入賬完成.GetEnumName())
                {
                    this.sqL_DataGridView_取藥狀態.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_取藥狀態.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (狀態 == enum_取藥堆疊母資料_狀態.庫存不足.GetEnumName())
                {
                    this.sqL_DataGridView_取藥狀態.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    this.sqL_DataGridView_取藥狀態.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void SqL_DataGridView_TagList_DataGridRefreshEvent()
        {
            for(int i = 0; i < this.sqL_DataGridView_TagList.Rows.Count; i++)
            {
                string 狀態 = "";
                狀態 = this.sqL_DataGridView_TagList.Rows[i].Cells[(int)enum_DrugHFTag.狀態].Value.ToString();
                if ((狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName() && _Import_Export == IncomeOutcomeMode.收入) || (狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName() && _Import_Export == IncomeOutcomeMode.支出))
                {
                    this.sqL_DataGridView_TagList.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    this.sqL_DataGridView_TagList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void Program_UI()
        {
            sqL_DataGridView_取藥狀態.SQL_GetRows((int)enum_取藥堆疊母資料.調劑台名稱, 調劑台名稱, true);
        }
        private void Program_HFRFID()
        {
            List<DrugHFTagClass> drugHFTagClasses = new List<DrugHFTagClass>();
            List<DrugHFTagClass> drugHFTagClasses_others = new List<DrugHFTagClass>();

            // 取得可以入庫 or 出庫的標籤
            if (_Import_Export == IncomeOutcomeMode.收入)
                drugHFTagClasses = DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server);
            if (_Import_Export == IncomeOutcomeMode.支出)
                drugHFTagClasses = DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);

            if (drugHFTagClasses == null || drugHFTagClasses.Count == 0) return;

            // 產生收支清單
            List<DrugHFTag_IncomeOutcomeListClass> drugHFTag_IncomeOutcomeListClasses = drugHFTagClasses.ToIncomeOutcomeList(_Import_Export);

            // 抓取要統計的藥碼清單
            List<string> drugCodes = drugHFTag_IncomeOutcomeListClasses.Select(x => x.藥碼).Distinct().ToList();

            // 使用新的批次API取得統計資訊
            var (code, result, summaries) = DrugHFTagClass.GetStockinStatusSummariesByCodes(
                Main_Form.API_Server,
                dateTimeIntervelPicker_報表時間.StartTime,
                dateTimeIntervelPicker_報表時間.EndTime,
                drugCodes
            );

            // 更新每個收支清單內的報表數量、收支數量
            if (code == 200 && summaries != null)
            {
                foreach (var item in drugHFTag_IncomeOutcomeListClasses)
                {
                    var summary = summaries.FirstOrDefault(s => s.藥碼 == item.藥碼);
                    if (summary != null)
                    {
                        item.報表數量 = (summary.已重置數量 + summary.入庫註記數量).ToString("0.###");

                        if (_Import_Export == IncomeOutcomeMode.收入)
                        {
                            item.收支數量 = (summary.入庫註記數量).ToString("0.###");
                        }
                        else if (_Import_Export == IncomeOutcomeMode.支出)
                        {
                            item.收支數量 = (summary.出庫註記數量).ToString("0.###");
                        }
                    }
                }
            }

            // 更新收支清單顯示
            this.sqL_DataGridView_收支清單.RefreshGrid(drugHFTag_IncomeOutcomeListClasses.ToObjectList());

            if (drugHFTag_IncomeOutcomeList == null) return;

            // 分類資料：選定藥碼 vs 其他藥碼
            drugHFTagClasses_others = drugHFTagClasses
                .Where(drugHFTagClass => drugHFTagClass.藥碼 != drugHFTag_IncomeOutcomeList.藥碼)
                .ToList();
            drugHFTagClasses = drugHFTagClasses
                .Where(drugHFTagClass => drugHFTagClass.藥碼 == drugHFTag_IncomeOutcomeList.藥碼)
                .ToList();

            List<object[]> list_drugHFTagClasses = new List<object[]>();
            List<string> uids = Main_Form.rfidReader_TagUID;

            // 更新標籤狀態（依UID是否包含）
            for (int i = 0; i < drugHFTagClasses.Count; i++)
            {
                if (uids.Contains(drugHFTagClasses[i].TagSN))
                {
                    if (_Import_Export == IncomeOutcomeMode.收入)
                    {
                        drugHFTagClasses[i].狀態 = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                    }
                }
                else
                {
                    if (_Import_Export == IncomeOutcomeMode.支出)
                    {
                        drugHFTagClasses[i].狀態 = enum_DrugHFTagStatus.出庫註記.GetEnumName();
                    }
                }
            }

            // 🔥重點：計算目前標記為「入庫註記」的總數量
            double stockinQty = drugHFTagClasses
                .Where(t => t.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName())
                .Sum(t => t.數量.StringToDouble());

            // 更新 rJ_Lable_數量
            form.Invoke(new Action(delegate
            {
                rJ_Lable_數量.Text = stockinQty.ToString("0.###");
            }));

            // 更新Tag清單畫面
            list_drugHFTagClasses.LockAdd(drugHFTagClasses.ToObjectList());
            this.sqL_DataGridView_TagList.RefreshGrid(list_drugHFTagClasses);
        }

        private void PlC_RJ_Button_解鎖_MouseDownEvent(MouseEventArgs mevent)
        {
            Main_Form.Function_外門片解鎖();
        }
        private void RJ_Button_選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_收支清單 = this.sqL_DataGridView_收支清單.Get_All_Select_RowsValues();
            if(list_收支清單.Count == 0)
            {
                MyMessageBox.ShowDialog("請選擇要作業的藥品");
                return;
            }
            string 藥碼 = list_收支清單[0][(int)enum_DrugHFTag_IncomeOutcomeList.藥碼].ObjectToString();
            Main_Form.Function_儲位亮燈(new Main_Form.LightOn(藥碼, Color.Blue));
            List<string> list_ip = Main_Form.Function_取得抽屜以藥品碼解鎖IP(藥碼);
            Main_Form.Function_抽屜解鎖(list_ip);
            drugHFTag_IncomeOutcomeList = list_收支清單[0].ToIncomeOutcomeClass();

        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_drugHFTagClasses = this.sqL_DataGridView_TagList.GetAllRows();
            if (list_drugHFTagClasses.Count == 0)
            {
                MyMessageBox.ShowDialog("未讀取到RFID標籤");
                return;
            }
            List<DrugHFTagClass> drugHFTagClasses = list_drugHFTagClasses.ToDrugHFTagClassList();
            if (_Import_Export == IncomeOutcomeMode.收入) drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()).ToList();
            stockClasses = drugHFTagClasses.GetStockClasses();
            StringBuilder sb = new StringBuilder();
            if (_Import_Export == IncomeOutcomeMode.收入) sb.AppendLine($"收入藥品品項：{stockClasses.Count}");
            if (_Import_Export == IncomeOutcomeMode.支出) sb.AppendLine($"支出藥品品項：{stockClasses.Count}");
            sb.AppendLine(new string('-', 30)); // 分隔線

            for (int i = 0; i < stockClasses.Count; i++)
            {
                if (_Import_Export == IncomeOutcomeMode.支出)
                {
                    StockClass stockClass = Main_Form.Function_取得庫存值從雲端資料(stockClasses[i].Code, stockClasses[i].Validity_period);
                    if (stockClass == null)
                    {
                        MyMessageBox.ShowDialog($"藥品：{stockClasses[i].Code} 無法取得庫存資訊");
                        return;
                    }
                    if (stockClass.Qty.StringToDouble() < stockClasses[i].Qty.StringToDouble())
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

            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            for (int i = 0; i < stockClasses.Count; i++)
            {
                StockClass stockClass = stockClasses[i];
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.調劑台名稱 = 調劑台名稱;
                takeMedicineStackClass.動作 = enum_交易記錄查詢動作.入庫作業.GetEnumName();
                takeMedicineStackClass.藥品碼 = stockClass.Code;
                takeMedicineStackClass.藥品名稱 = stockClass.Name;
                takeMedicineStackClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.收支原因 = "";
                takeMedicineStackClass.操作人 = Main_Form._登入者名稱;
                takeMedicineStackClass.ID = Main_Form._登入者ID;
                takeMedicineStackClass.顏色 = Color.Blue.ToColorString();
                takeMedicineStackClass.總異動量 = stockClass.Qty;
                takeMedicineStackClass.效期 = stockClass.Validity_period;
                takeMedicineStackClass.批號 = stockClass.Lot_number;
                takeMedicineStackClasses.Add(takeMedicineStackClass);
            }
            Main_Form.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);

            LoadingForm.ShowLoadingForm();
            DrugHFTagClass.add(Main_Form.API_Server, drugHFTagClasses);
            LoadingForm.CloseLoadingForm();

            drugHFTag_IncomeOutcomeList = null;
            this.sqL_DataGridView_TagList.ClearGrid();

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
            if (_Import_Export == IncomeOutcomeMode.收入) drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()).ToList();
            if (_Import_Export == IncomeOutcomeMode.支出) drugHFTagClasses = drugHFTagClasses.Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName()).ToList();

            stockClasses = drugHFTagClasses.GetStockClasses();

            StringBuilder sb = new StringBuilder();
            if (_Import_Export == IncomeOutcomeMode.收入) sb.AppendLine($"收入藥品品項：{stockClasses.Count}");
            if (_Import_Export == IncomeOutcomeMode.支出)
            {
                sb.AppendLine($"出庫藥品品項：{stockClasses.Count}");
               
            }
            sb.AppendLine(new string('-', 30)); // 分隔線

            for (int i = 0; i < stockClasses.Count; i++)
            {
                if (_Import_Export == IncomeOutcomeMode.支出)
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
