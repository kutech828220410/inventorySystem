﻿using System;
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
    public partial class Dialog_收支作業_RFID收支作業 : MyDialog
    {
        private string 調劑台名稱 = $"{Main_Form.ServerName}";
        private List<DrugHFTagClass> DBTags = new List<DrugHFTagClass>();
        private List<DrugHFTagClass> errorTags = new List<DrugHFTagClass>();
        private bool hasRetriedConfirmation = false;
        private bool showAlert = false;
        private DrugHFTag_IncomeOutcomeListClass _drugHFTag_IncomeOutcomeList = null;
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
                        rJ_Lable_應收入.Text = "----";
                        rJ_Button_選擇.Enabled = true;
                        rJ_Button_取消.Enabled = false;
                        rJ_Lable_請關門過帳.Visible = false;
                    }));
                    if (drugHFTag_IncomeOutcomeList != null) Main_Form.Function_儲位亮燈(new Main_Form.LightOn(drugHFTag_IncomeOutcomeList.藥碼, Color.Black));

                    _drugHFTag_IncomeOutcomeList = value;
                    return;
                }
                form.Invoke(new Action(delegate
                {
                    rJ_Lable_藥名.Text = value.藥名;
                    rJ_Lable_應收入.Text = value.收支數量;
                    rJ_Lable_請關門過帳.Visible = true;
                }));
                _drugHFTag_IncomeOutcomeList = value;
            }
        }
        public IncomeOutcomeMode _Import_Export = IncomeOutcomeMode.收入;
        public List<StockClass> stockClasses = new List<StockClass>();
        private double qty = 0;

        private List<DrugHFTagClass> _cachedTagList = null;
        private DateTime _cachedTagMaxUpdateTime = DateTime.MinValue;
        private DateTime _cachedReportStartTime;
        private DateTime _cachedReportEndTime;

        private List<DrugHFTagStatusSummaryByCode> _cachedSummaries = null;
        private List<string> _cachedDrugCodes = null;

        private MyThread myThread_HFRFID = new MyThread();
        private MyThread myThread_UI = new MyThread();

        public Dialog_收支作業_RFID收支作業(IncomeOutcomeMode IncomeOutcomeMode)
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
            _Import_Export = IncomeOutcomeMode;
            this.Text = _Import_Export == IncomeOutcomeMode.收入 ? "RFID收入" : "RFID支出";
            this.LoadFinishedEvent += Dialog_收支作業_RFID出收入_LoadFinishedEvent;
            this.FormClosing += Dialog_收支作業_RFID出收入_FormClosing;
            this.FormClosed += Dialog_收支作業_RFID收支作業_FormClosed;
            dateTimeIntervelPicker_報表時間.SetDateTime(DateTime.Now.AddDays(0).GetStartDate(), DateTime.Now.AddDays(0).GetEndDate());
        }

        private void Dialog_收支作業_RFID收支作業_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(showAlert)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm((_Import_Export == IncomeOutcomeMode.收入 ? "入庫完成" : "出庫完成"), 2000, 0, 0, Color.LightGreen, Color.Black);
                dialog_AlarmForm.ShowDialog();
            }
         
        }

        private void Dialog_收支作業_RFID出收入_LoadFinishedEvent(EventArgs e)
        {

            Main_Form.HFRFID_debug = true;
            //this.sqL_DataGridView_TagList.RowsHeight = 50;
            //this.sqL_DataGridView_TagList.Init(DrugHFTagClass.init(Main_Form.API_Server));
            //this.sqL_DataGridView_TagList.Set_ColumnVisible(false, new enum_DrugHFTag().GetEnumNames());
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.TagSN);
            ////this.sqL_DataGridView_TagList.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.藥碼);
            ////this.sqL_DataGridView_TagList.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_DrugHFTag.藥名);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.效期);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.批號);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.數量);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.更新時間);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.狀態);
            //this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.TagSN);
            //this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.效期);
            //this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.更新時間);
            //this.sqL_DataGridView_TagList.DataGridRefreshEvent += SqL_DataGridView_TagList_DataGridRefreshEvent;

            this.sqL_DataGridView_收支清單.RowsHeight = 60;
            this.sqL_DataGridView_收支清單.Init(new Table(new enum_DrugHFTag_IncomeOutcomeList()));
            this.sqL_DataGridView_收支清單.Set_ColumnVisible(false, new enum_DrugHFTag_IncomeOutcomeList().GetEnumNames());
            this.sqL_DataGridView_收支清單.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag_IncomeOutcomeList.藥碼);
            this.sqL_DataGridView_收支清單.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_DrugHFTag_IncomeOutcomeList.藥名);
 

            Table table = takeMedicineStackClass.init(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType).GetTable(new enum_取藥堆疊母資料());
            //this.sqL_DataGridView_取藥狀態.InitEx(table);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnVisible(false, new enum_取藥堆疊母資料().GetEnumNames());
            ////this.sqL_DataGridView_取藥狀態.Set_ColumnVisible(true, enum_取藥堆疊母資料.藥品碼, enum_取藥堆疊母資料.藥品名稱, enum_取藥堆疊母資料.總異動量, enum_取藥堆疊母資料.結存量, enum_取藥堆疊母資料.狀態);
            ////this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品碼);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(300, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.藥品名稱);
            ////this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.庫存量);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.總異動量);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, enum_取藥堆疊母資料.結存量);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_取藥堆疊母資料.狀態);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnText("藥碼", enum_取藥堆疊母資料.藥品碼);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnText("藥名", enum_取藥堆疊母資料.藥品名稱);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnText("庫存", enum_取藥堆疊母資料.庫存量);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnText("異動", enum_取藥堆疊母資料.總異動量);
            //this.sqL_DataGridView_取藥狀態.Set_ColumnText("結存", enum_取藥堆疊母資料.結存量);
            //this.sqL_DataGridView_取藥狀態.DataGridRefreshEvent += SqL_DataGridView_取藥狀態_DataGridRefreshEvent;

            if(_Import_Export == IncomeOutcomeMode.收入)
            {
                this.sqL_DataGridView_收支清單.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag_IncomeOutcomeList.收支數量);
                this.sqL_DataGridView_收支清單.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag_IncomeOutcomeList.報表數量);
                this.sqL_DataGridView_收支清單.Set_ColumnText("未收入", enum_DrugHFTag_IncomeOutcomeList.報表數量);
                this.sqL_DataGridView_收支清單.Set_ColumnText("已收入", enum_DrugHFTag_IncomeOutcomeList.收支數量);

                rJ_Lable_應收入_title.Text = "收入";
            }
            if (_Import_Export == IncomeOutcomeMode.支出)
            {
                this.sqL_DataGridView_收支清單.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag_IncomeOutcomeList.報表數量);
                this.sqL_DataGridView_收支清單.Set_ColumnText("實際值", enum_DrugHFTag_IncomeOutcomeList.報表數量);
                dateTimeIntervelPicker_報表時間.Visible = false;
                rJ_Lable_應收入_title.Text = "支出";
            }
            this.rJ_Button_選擇.MouseDownEvent += RJ_Button_選擇_MouseDownEvent;
            this.plC_RJ_Button_解鎖.MouseDownEvent += PlC_RJ_Button_解鎖_MouseDownEvent;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            myThread_HFRFID.AutoRun(true);
            myThread_HFRFID.Add_Method(Program_HFRFID);
            myThread_HFRFID.SetSleepTime(100);
            myThread_HFRFID.Trigger();

            Main_Form.LockClosingEvent += Main_Form_LockClosingEvent;
            this.Refresh();
            Main_Form.Function_外門片解鎖();
        }
        private void Dialog_收支作業_RFID出收入_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThread_HFRFID != null)
            {
                myThread_HFRFID.Abort();
                myThread_HFRFID = null;
            }
            if (myThread_UI != null)
            {
                myThread_UI.Abort();
                myThread_UI = null;
            }
            Main_Form.LockClosingEvent -= Main_Form_LockClosingEvent;
            Main_Form.HFRFID_debug = false;
        }

        private void Main_Form_LockClosingEvent(object sender, PLC_Device PLC_Device_Input, PLC_Device PLC_Device_Output, string GUID)
        {
            if (drugHFTag_IncomeOutcomeList == null)
            {
                Logger.Log("dialog_HRFID", $"[Locker Check] 略過：drugHFTag_IncomeOutcomeList 為 null");
                return;
            }

            Logger.Log("dialog_HRFID", $"[Locker Check] 藥碼 = {drugHFTag_IncomeOutcomeList.藥碼}，開始查詢解鎖 IP");

            List<object[]> list_locker_table_value = Main_Form._sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            Logger.Log("dialog_HRFID", $"[Locker Check] 取得 locker_index_table 筆數 = {list_locker_table_value.Count}");

            List<string> ips = Main_Form.Function_取得抽屜以藥品碼解鎖IP(drugHFTag_IncomeOutcomeList.藥碼);
            Logger.Log("dialog_HRFID", $"[Locker Check] 解鎖對應 IP 清單 = {string.Join(", ", ips)}");

            if (ips.Count == 0)
            {
                Logger.Log("dialog_HRFID", $"[Locker Check] 無可解鎖的 IP，結束流程");
                return;
            }

            List<lockerIndexClass> lockerIndexClasses = list_locker_table_value.SQLToClass<lockerIndexClass, enum_lockerIndex>();
            Logger.Log("dialog_HRFID", $"[Locker Check] lockerIndexClass 轉換完成，共 {lockerIndexClasses.Count} 筆");

            List<lockerIndexClass> lockerIndexClasse_buf = new List<lockerIndexClass>();
            for (int i = 0; i < ips.Count; i++)
            {
                string ip = ips[i];
                string outputAddress = PLC_Device_Output.GetAdress().ToUpper();

                lockerIndexClasse_buf = (from temp in lockerIndexClasses
                                         where temp.同步輸出.ToUpper() == outputAddress
                                         select temp).ToList();

                Logger.Log("dialog_HRFID", $"[Locker Check] 比對 IP = {ip}，同步輸出 = {outputAddress}，匹配筆數 = {lockerIndexClasse_buf.Count}");

                if (lockerIndexClasse_buf.Count > 0)
                {
                    Logger.Log("dialog_HRFID", $"[Locker Check] 符合條件，執行 Function_處理RFID確認流程()");
                    Task.Run(new Action(delegate
                    {
                        Function_處理RFID確認流程();
                    }));
                   
                    return;
                }
            }

            Logger.Log("dialog_HRFID", $"[Locker Check] 無符合同步輸出條件的 locker 資料，結束流程");


        }

     

        private void Program_HFRFID()
        {
            DateTime startTotal = DateTime.Now;
            Console.WriteLine($"[{startTotal:HH:mm:ss.fff}]  開始執行 Program_HFRFID");

            DateTime reportStart = dateTimeIntervelPicker_報表時間.StartTime;
            DateTime reportEnd = dateTimeIntervelPicker_報表時間.EndTime;

            // ---------- 取得標籤資料 ----------
            bool needFetchTags = false;
            if (_cachedTagList == null || _cachedReportStartTime != reportStart || _cachedReportEndTime != reportEnd)
            {
                needFetchTags = true;
            }
            else
            {
                DateTime maxUpdate = _cachedTagList.Max(x => x.更新時間.StringToDateTime());
                if (maxUpdate > _cachedTagMaxUpdateTime)
                    needFetchTags = true;
            }

            if (needFetchTags)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [重新讀取 RFID 標籤資料]");
                if (_Import_Export == IncomeOutcomeMode.收入)
                    _cachedTagList = DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server);
                else
                    _cachedTagList = DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);

                _cachedTagMaxUpdateTime = _cachedTagList.Max(x => x.更新時間.StringToDateTime());
                _cachedReportStartTime = reportStart;
                _cachedReportEndTime = reportEnd;
            }
            if (_Import_Export == IncomeOutcomeMode.收入)
            {
                DBTags = _cachedTagList
               .Where(d => d.更新時間.StringToDateTime() >= reportStart &&
                           d.更新時間.StringToDateTime() <= reportEnd)
               .ToList();
            }
            else
            {
                DBTags = _cachedTagList;
            }
               

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  篩選後標籤筆數: {DBTags.Count}");
            if (DBTags.Count == 0)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  無符合時間範圍的標籤，結束流程");
                return;
            }

            // ---------- 轉為報表清單 ----------
            List<DrugHFTag_IncomeOutcomeListClass> drugHFTag_IncomeOutcomeListClasses = DBTags.ToIncomeOutcomeList(_Import_Export);
            List<string> drugCodes = drugHFTag_IncomeOutcomeListClasses.Select(x => x.藥碼).Distinct().ToList();

            // ---------- 取得統計資料 ----------
            bool sameDrugCodes = _cachedDrugCodes != null &&
                                 _cachedDrugCodes.Count == drugCodes.Count &&
                                 !_cachedDrugCodes.Except(drugCodes).Any();

            bool needFetchSummary = !sameDrugCodes ||
                                    _cachedReportStartTime != reportStart ||
                                    _cachedReportEndTime != reportEnd ||
                                    _cachedSummaries == null;

            if (needFetchSummary)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  [重新取得統計資料]");
                var (_, _, summaries) = DrugHFTagClass.GetStockinStatusSummariesByCodes(
                    Main_Form.API_Server, reportStart, reportEnd, drugCodes);
                _cachedSummaries = summaries;
                _cachedDrugCodes = new List<string>(drugCodes);
            }

            if (_cachedSummaries != null)
            {
                foreach (var item in drugHFTag_IncomeOutcomeListClasses)
                {
                    var summary = _cachedSummaries.FirstOrDefault(s => s.藥碼 == item.藥碼);
                    if (summary != null)
                    {
                        item.報表數量 = (_Import_Export == IncomeOutcomeMode.收入 ? summary.可入庫數量 : summary.可出庫數量).ToString("0.###");
                        item.收支數量 = (_Import_Export == IncomeOutcomeMode.收入 ? summary.已入庫數量 : summary.已出庫數量).ToString("0.###");
                    }
                }
            }

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  收支清單筆數: {drugHFTag_IncomeOutcomeListClasses.Count}");
            this.sqL_DataGridView_收支清單.RefreshGrid(drugHFTag_IncomeOutcomeListClasses.ToObjectList());
         
        }
        private List<DrugHFTagClass> readUID()
        {
            DateTime startTotal = DateTime.Now;
            List<string> uids = Main_Form.ReadAllUIDsOnceOnly();

            if (drugHFTag_IncomeOutcomeList == null)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  無選擇的藥品，結束流程");
                return null;
            }

            string selectedDrugCode = drugHFTag_IncomeOutcomeList.藥碼;

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  UID 數量: {uids.Count}，選取藥碼: {selectedDrugCode}");

            // ---------- 比對標籤 ----------
            List<DrugHFTagClass> tagDisplayList = new List<DrugHFTagClass>();
            errorTags.Clear();

            foreach (var tag in DBTags)
            {
                bool isUIDMatch = uids.Contains(tag.TagSN);
                bool isSameCode = tag.藥碼 == selectedDrugCode;

                if (_Import_Export == IncomeOutcomeMode.收入 && isUIDMatch && isSameCode)
                {
                    tag.狀態 = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                    tagDisplayList.Add(tag);
                }
                else if (_Import_Export == IncomeOutcomeMode.支出)
                {
                    if (!isUIDMatch && isSameCode)
                    {
                        tag.狀態 = enum_DrugHFTagStatus.出庫註記.GetEnumName();
                        tagDisplayList.Add(tag);
                    }
                    else if (!isUIDMatch && !isSameCode)
                    {
                        errorTags.Add(tag);
                    }
                }
            }

            // ---------- 計算總量 ----------
            double qty = tagDisplayList
                .Where(t =>
                    (_Import_Export == IncomeOutcomeMode.收入 && t.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()) ||
                    (_Import_Export == IncomeOutcomeMode.支出 && t.狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName()))
                .Sum(t => t.數量.StringToDouble());

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  計算完成，標記數量: {qty}，異常標籤數: {errorTags.Count}");

            // ---------- 顯示 ----------
            form.Invoke(new Action(() =>
            {
                rJ_Lable_應收入.Text = this.qty.ToString();
            }));

            //this.sqL_DataGridView_TagList.RefreshGrid(tagDisplayList.ToObjectList());

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  標籤清單刷新完成，顯示數量: {tagDisplayList.Count}");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}]  程式結束，總耗時: {(DateTime.Now - startTotal).TotalMilliseconds} ms");
            return tagDisplayList;
        }

        private void PlC_RJ_Button_解鎖_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                Logger.Log($"dialog_HRFID", $"[PlC_RJ_Button_解鎖_MouseDownEvent] 使用者 : {Main_Form._登入者名稱}");

                Main_Form.Function_外門片解鎖();
            }
            catch (Exception ex)
            {
                Logger.Log("dialog_HRFID", $"[錯誤] {ex.Message} | {ex.StackTrace}");
            }
            finally
            {
                Logger.Log("dialog_HRFID", $"[PlC_RJ_Button_解鎖_MouseDownEvent] 結束執行流程");

            }

        }
        private void RJ_Button_選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                Logger.Log("dialog_HRFID", $"[RJ_Button_選擇_MouseDownEvent] 使用者: {Main_Form._登入者名稱}");

                List<object[]> list_收支清單 = this.sqL_DataGridView_收支清單.Get_All_Select_RowsValues();
                Logger.Log("dialog_HRFID", $"取得收支清單筆數: {list_收支清單.Count}");

                if (list_收支清單.Count == 0)
                {
                    Logger.Log("dialog_HRFID", "使用者未選擇任何藥品，顯示提示視窗");
                    MyMessageBox.ShowDialog("請選擇要作業的藥品");
                    return;
                }

                double 報表數量 = list_收支清單[0][(int)enum_DrugHFTag_IncomeOutcomeList.報表數量].ObjectToString().StringToDouble();

                Logger.Log("dialog_HRFID", $"目前為【{_Import_Export}】模式，顯示數量輸入面板");
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"請輸入{_Import_Export}數量");

                if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
                {
                    Logger.Log("dialog_HRFID", $"使用者取消輸入{_Import_Export}數量");
                    return;
                }

                qty = dialog_NumPannel.Value;
                Logger.Log("dialog_HRFID", $"使用者輸入{_Import_Export}數量: {qty}");

                if (qty <= 0)
                {
                    Logger.Log("dialog_HRFID", $"{_Import_Export}數量為 0 或負數，阻止繼續執行");
                    MyMessageBox.ShowDialog("請輸入正確的數量");
                    return;
                }

                if (qty > 報表數量)
                {
                    Logger.Log("dialog_HRFID", $"輸入{_Import_Export}數量 {qty} 大於報表數量 {報表數量}，阻止執行");
                    MyMessageBox.ShowDialog($"{_Import_Export}數量大於報表數量");
                    return;
                }



                string 藥碼 = list_收支清單[0][(int)enum_DrugHFTag_IncomeOutcomeList.藥碼].ObjectToString();
                list_收支清單[0][(int)enum_DrugHFTag_IncomeOutcomeList.收支數量] = qty;
                Logger.Log("dialog_HRFID", $"取得藥碼: {藥碼}，執行亮燈與解鎖程序");

                Main_Form.Function_儲位亮燈(new Main_Form.LightOn(藥碼, Color.Blue));

                List<string> list_ip = Main_Form.Function_取得抽屜以藥品碼解鎖IP(藥碼);
                Logger.Log("dialog_HRFID", $"取得 IP 清單，共 {list_ip.Count} 筆: {string.Join(", ", list_ip)}");

                Main_Form.Function_抽屜解鎖(list_ip);
                Logger.Log("dialog_HRFID", "完成抽屜解鎖");

          
                drugHFTag_IncomeOutcomeList = list_收支清單[0].ToIncomeOutcomeClass();
                Logger.Log("dialog_HRFID", $"完成 drugHFTag_IncomeOutcomeList 建立，藥品名稱: {drugHFTag_IncomeOutcomeList.藥名}");

                if (_Import_Export == IncomeOutcomeMode.收入)
                {
                    Task.Run(new Action(delegate 
                    {
                        this.Invoke(new Action(delegate
                        {
                            Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\請開門入庫.wav");
                                            
                        }));
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請開門入庫", 1500, 0, 0, Color.LightGreen, Color.Black);
                        dialog_AlarmForm.ShowDialog();
         
                    }));
                   
                }
                else if (_Import_Export == IncomeOutcomeMode.支出)
                {
                    Task.Run(new Action(delegate
                    {
                        this.Invoke(new Action(delegate
                        {
                            Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\請開門出庫.wav");
                        }));
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請開門出庫", 1500, 0, 0, Color.LightGreen, Color.Black);
                        dialog_AlarmForm.ShowDialog();

                    }));
               
                }
                form.Invoke(new Action(delegate 
                {
                    rJ_Button_選擇.Enabled = false;
                    rJ_Button_取消.Enabled = true;
                }));
                hasRetriedConfirmation = false;


            }
            catch (Exception ex)
            {
                Logger.Log("dialog_HRFID", $"[錯誤] {ex.Message} | {ex.StackTrace}");
                MyMessageBox.ShowDialog("發生例外錯誤，請檢查 Log 或聯繫工程人員");
            }
            finally
            {
                Logger.Log("dialog_HRFID", $"[RJ_Button_選擇_MouseDownEvent] 結束執行流程");
            }



        }
        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                Logger.Log($"dialog_HRFID", $"[RJ_Button_取消_MouseDownEvent] 使用者 : {Main_Form._登入者名稱}");
                if (drugHFTag_IncomeOutcomeList != null)
                {
                    Main_Form.Function_儲位亮燈(new Main_Form.LightOn(drugHFTag_IncomeOutcomeList.藥碼, Color.Black));
                }
        
                drugHFTag_IncomeOutcomeList = null;
                //this.sqL_DataGridView_TagList.ClearGrid();
            }
            catch (Exception ex)
            {
                Logger.Log("dialog_HRFID", $"[錯誤] {ex.Message} | {ex.StackTrace}");

            }
            finally
            {
                Logger.Log("dialog_HRFID", $"[RJ_Button_取消_MouseDownEvent] 結束執行流程");

            }

        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                Function_處理RFID確認流程();
            }
            catch (Exception ex)
            {
                Logger.Log("dialog_HRFID", $"[例外] {ex.Message}\n{ex.StackTrace}");
                MyMessageBox.ShowDialog("發生例外錯誤，請查看 log 或聯絡工程人員！");
            }
            finally
            {
                Logger.Log("dialog_HRFID", $"[RJ_Button_確認_MouseDownEvent] 結束執行流程");
            }


        }
        private void Function_處理RFID確認流程()
        {
            LoadingForm.ShowLoadingForm();
            List<DrugHFTagClass> list_drugHFTagClasses = readUID();
            LoadingForm.CloseLoadingForm();
            Dialog_收支異常提示.CloseAllDialog();

            Logger.Log("dialog_HRFID", $"[RJ_Button_確認_MouseDownEvent] 使用者: {Main_Form._登入者名稱}");

            Logger.Log("dialog_HRFID", $"取得標籤筆數: {list_drugHFTagClasses.Count}");

            if (list_drugHFTagClasses.Count == 0 && errorTags.Count == 0)
            {
                Logger.Log("dialog_HRFID", "未讀取到任何 RFID 標籤");
                MyMessageBox.ShowDialog("未讀取到RFID標籤");
                return;
            }
    
            bool 驗證失敗 = false;
            bool 數量異常 = false;

            double 實際標籤數量 = list_drugHFTagClasses
                .Where(t =>
                    (_Import_Export == IncomeOutcomeMode.收入 && t.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()) ||
                    (_Import_Export == IncomeOutcomeMode.支出 && t.狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName()))
                .Sum(t => t.數量.StringToDouble());
        
            if (實際標籤數量 != qty)
            {
                Logger.Log("dialog_HRFID", $"標籤數量 {實際標籤數量} ≠ 輸入數量 {qty}");
                驗證失敗 = true;
                數量異常 = true;
            }

            if (errorTags.Count > 0)
            {
                Logger.Log("dialog_HRFID", $"偵測到異常標籤 {errorTags.Count} 筆");
                驗證失敗 = true;
            }
            bool 品項錯誤 = errorTags.Count > 0;
            品項錯誤 = false;
            if (驗證失敗)
            {
                string tts_content = "";
                if (實際標籤數量 != qty && 品項錯誤)
                {
                    tts_content = "數量及品項錯誤,請再次確認";
                }
                else if(實際標籤數量 != qty)
                {
                    tts_content = "數量錯誤,請再次確認";
                }
                else if (品項錯誤)
                {
                    tts_content = "品項錯誤,請再次確認";
                }
                Dialog_收支異常提示 dialog_收支異常提示 = new Dialog_收支異常提示($"{Main_Form._登入者名稱},{tts_content}");
                dialog_收支異常提示.IgnoreVisible = hasRetriedConfirmation;
                dialog_收支異常提示.MouseDownEvent_LokcOpen += PlC_RJ_Button_解鎖_MouseDownEvent;
                dialog_收支異常提示.ShowDialog();
                hasRetriedConfirmation = true;
                if (dialog_收支異常提示.DialogResult != DialogResult.Abort) return;

                Logger.Log("dialog_HRFID", $"第二次驗證仍失敗，自動記錄異常");

                List<medRecheckLogClass> errorLogs = new List<medRecheckLogClass>();

                foreach (var tag in errorTags)
                {
                    var log = new medRecheckLogClass
                    {
                        GUID = Guid.NewGuid().ToString(),
                        發生類別 = (_Import_Export == IncomeOutcomeMode.收入) ? enum_medRecheckLog_ICDT_TYPE.RFID入庫異常.GetEnumName() : enum_medRecheckLog_ICDT_TYPE.RFID出庫異常.GetEnumName(),
                        藥碼 = tag.藥碼,
                        藥名 = tag.藥名,
                        效期 = tag.效期,
                        批號 = tag.批號,
                        庫存值 = "0",
                        盤點值 = tag.數量.ToString(),
                        差異值 = tag.數量.ToString(),
                        發生時間 = DateTime.Now.ToDateTimeString_6(),
                        排除時間 = DateTime.MinValue.ToDateTimeString(),
                        狀態 = enum_medRecheckLog_State.未排除.GetEnumName(),
                        事件描述 = "品項錯誤",
                        通知註記 = "未通知",
                        通知時間 = DateTime.MinValue.ToDateTimeString(),
                        參數1 = tag.TagSN,
                        參數2 = ""
                    };
                    errorLogs.Add(log);
                }

                if (數量異常 && drugHFTag_IncomeOutcomeList != null)
                {
                    var qtyLog = new medRecheckLogClass
                    {
                        GUID = Guid.NewGuid().ToString(),
                        發生類別 = (_Import_Export == IncomeOutcomeMode.收入) ? enum_medRecheckLog_ICDT_TYPE.RFID入庫異常.GetEnumName() : enum_medRecheckLog_ICDT_TYPE.RFID出庫異常.GetEnumName(),
                        藥碼 = drugHFTag_IncomeOutcomeList.藥碼,
                        藥名 = drugHFTag_IncomeOutcomeList.藥名,
                        效期 = "",
                        批號 = "",
                        庫存值 = qty.ToString(),
                        盤點值 = 實際標籤數量.ToString("0.###"),
                        差異值 = (實際標籤數量 - qty).ToString("0.###"),
                        發生時間 = DateTime.Now.ToDateTimeString_6(),
                        排除時間 = DateTime.MinValue.ToDateTimeString(),
                        狀態 = enum_medRecheckLog_State.未排除.GetEnumName(),
                        事件描述 = "數量錯誤",
                        通知註記 = "未通知",
                        通知時間 = DateTime.MinValue.ToDateTimeString(),
                        參數1 = "",
                        參數2 = ""
                    };
                    errorLogs.Add(qtyLog);
                }

                if (errorLogs.Count > 0)
                {
                    Logger.Log("dialog_HRFID", $"自動寫入異常標籤與數量記錄 {errorLogs.Count} 筆");
                    LoadingForm.ShowLoadingForm();
                    medRecheckLogClass.add(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, errorLogs);
                    //(int code, var result, var val) = DrugHFTagClass.set_tag_broken_full(Main_Form.API_Server, errorTags);
                    LoadingForm.CloseLoadingForm();
                    //MyMessageBox.ShowDialog($"⚠ 已自動新增異常記錄 {errorLogs.Count} 筆，請查閱紀錄！");
                }

            }

            hasRetriedConfirmation = false;

            List<DrugHFTagClass> drugHFTagClasses = list_drugHFTagClasses;

            if (_Import_Export == IncomeOutcomeMode.收入)
            {
                drugHFTagClasses = drugHFTagClasses
                    .Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName())
                    .ToList();
            }

            stockClasses = drugHFTagClasses.GetStockClasses();
            Logger.Log("dialog_HRFID", $"取得有效標籤轉為 StockClass，共 {stockClasses.Count} 筆");

            StringBuilder sb = new StringBuilder();
            if (_Import_Export == IncomeOutcomeMode.收入) sb.AppendLine($"收入藥品品項：{stockClasses.Count}");
            if (_Import_Export == IncomeOutcomeMode.支出) sb.AppendLine($"支出藥品品項：{stockClasses.Count}");
            sb.AppendLine(new string('-', 30));

            for (int i = 0; i < stockClasses.Count; i++)
            {
                if (_Import_Export == IncomeOutcomeMode.支出)
                {
                    StockClass stockClass = Main_Form.Function_取得庫存值從雲端資料(stockClasses[i].Code, stockClasses[i].Validity_period);
                    if (stockClass == null)
                    {
                        Logger.Log("dialog_HRFID", $"雲端查無藥品庫存資料：{stockClasses[i].Code}");
                        MyMessageBox.ShowDialog($"藥品：{stockClasses[i].Code} 無法取得庫存資訊");
                        return;
                    }
                    if (stockClass.Qty.StringToDouble() < stockClasses[i].Qty.StringToDouble())
                    {
                        Logger.Log("dialog_HRFID", $"庫存不足：{stockClasses[i].Code} 效期 {stockClasses[i].Validity_period}，庫存 {stockClass.Qty} < 需求 {stockClasses[i].Qty}");
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
                sb.AppendLine();
            }

            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            for (int i = 0; i < stockClasses.Count; i++)
            {
                StockClass stockClass = stockClasses[i];
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass
                {
                    調劑台名稱 = 調劑台名稱,
                    動作 = ((_Import_Export == IncomeOutcomeMode.收入) ? enum_交易記錄查詢動作.入庫作業.GetEnumName() : enum_交易記錄查詢動作.出庫作業.GetEnumName()),
                    藥品碼 = stockClass.Code,
                    藥品名稱 = stockClass.Name,
                    開方時間 = DateTime.Now.ToDateTimeString_6(),
                    收支原因 = "",
                    操作人 = Main_Form._登入者名稱,
                    ID = Main_Form._登入者ID,
                    顏色 = Color.Black.ToColorString(),
                    總異動量 = (_Import_Export == IncomeOutcomeMode.支出) ? (stockClass.Qty.StringToDouble() * -1).ToString() : stockClass.Qty,
                    效期 = stockClass.Validity_period,
                    批號 = stockClass.Lot_number
                };
                takeMedicineStackClasses.Add(takeMedicineStackClass);
            }

            Logger.Log("dialog_HRFID", $"準備新增母資料筆數: {takeMedicineStackClasses.Count}");
            Main_Form.Function_取藥堆疊資料_新增母資料(takeMedicineStackClasses);

            Logger.Log("dialog_HRFID", $"呼叫 API 寫入 DrugHFTag 標籤資料，共 {drugHFTagClasses.Count} 筆");
            LoadingForm.ShowLoadingForm();
            DrugHFTagClass.add(Main_Form.API_Server, drugHFTagClasses);
            LoadingForm.CloseLoadingForm();
            Logger.Log("dialog_HRFID", $"完成 API 寫入與畫面清除");
            drugHFTag_IncomeOutcomeList = null;
            //this.sqL_DataGridView_TagList.ClearGrid();
            showAlert = true;
            this.Close();
        }
    }
}
