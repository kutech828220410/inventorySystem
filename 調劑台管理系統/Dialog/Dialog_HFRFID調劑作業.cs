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
using FpMatchLib;
using HIS_DB_Lib;
using H_Pannel_lib;

namespace 調劑台管理系統
{
    public partial class Dialog_HFRFID調劑作業 : MyDialog
    {
        public enum enum_TagList
        {
            [Description("GUID,VARCHAR,50,NONE")]
            GUID,
            [Description("藥碼,VARCHAR,50,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,50,NONE")]
            藥名,
            [Description("應出,VARCHAR,50,NONE")]
            應出,
            [Description("實出,VARCHAR,50,NONE")]
            實出,
        }

        private string _deviceName = "";
        private personPageClass _personPageClass = new personPageClass();
        private List<DrugHFTagClass> DrugHFTags = new List<DrugHFTagClass>();
        private List<DrugHFTagClass> DrugHFTags_error = new List<DrugHFTagClass>();
        public IncomeOutcomeMode _Import_Export = IncomeOutcomeMode.支出;
        public List<StockClass> stockClasses = new List<StockClass>();
        public List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
        public List<takeMedicineStackClass> takeMedicineStackClasses_temp = new List<takeMedicineStackClass>();
        private List<DrugHFTag_IncomeOutcomeListClass> drugHFTag_IncomeOutcomeListClasses;
        private bool hasRetriedConfirmation = false;
        private MyThread myThread_HFRFID = new MyThread();
        private double qty = 0;
        private int retry = 0;
        private const double EPS = 0.0001;

        // 樓層→Reader Index 對應
        private static readonly (string areaKey, int r1, int r2)[] _areaReaders = new[]
        {
            ("第一層", 1, 2),
            ("第二層", 3, 4),
            ("第三層", 5, 6),
            ("第四層", 7, 8),
            ("第五層", 9, 10),
        };

        public Dialog_HFRFID調劑作業(string deviceName, IncomeOutcomeMode incomeOutcomeMode)
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
            }));
            _deviceName = deviceName;
            _Import_Export = incomeOutcomeMode;

            this.Text = _Import_Export == IncomeOutcomeMode.收入 ? "高頻RFID調劑作業(退)" : "高頻RFID調劑作業(領)";
            this.LoadFinishedEvent += Dialog_HFRFID調劑作業_LoadFinishedEvent;
            this.FormClosing += Dialog_HFRFID調劑作業_FormClosing;
            this.FormClosed += Dialog_HFRFID調劑作業_FormClosed;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            this.plC_RJ_Button_解鎖.MouseDownEvent += PlC_RJ_Button_解鎖_MouseDownEvent;
            Main_Form.LockClosingEvent += Main_Form_LockClosingEvent;

        }

        private void Dialog_HFRFID調劑作業_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.Yes)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("調劑完成", 2000, 0, 0, Color.LightGreen, Color.Black);
                dialog_AlarmForm.ShowDialog();
            }
            else
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("調劑取消", 2000, 0, 0, Color.Red, Color.White);
                dialog_AlarmForm.ShowDialog();
            }
        }

        private void Main_Form_LockClosingEvent(object sender, PLC_Device PLC_Device_Input, PLC_Device PLC_Device_Output, string GUID)
        {

            List<object[]> list_locker_table_value = Main_Form._sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            Logger.Log("dialog_main_HRFID", $"[Locker Check] 取得 locker_index_table 筆數 = {list_locker_table_value.Count}");

            List<string> ips = new List<string>();
            for (int i = 0; i < takeMedicineStackClasses_temp.Count; i++)
            {
                ips.LockAdd(Main_Form.Function_取得抽屜以藥品碼解鎖IP(takeMedicineStackClasses_temp[i].藥品碼));
            }


            Logger.Log("dialog_main_HRFID", $"[Locker Check] 解鎖對應 IP 清單 = {string.Join(", ", ips)}");

            if (ips.Count == 0)
            {
                Logger.Log("dialog_main_HRFID", $"[Locker Check] 無可解鎖的 IP，結束流程");
                return;
            }

            List<lockerIndexClass> lockerIndexClasses = list_locker_table_value.SQLToClass<lockerIndexClass, enum_lockerIndex>();
            Logger.Log("dialog_main_HRFID", $"[Locker Check] lockerIndexClass 轉換完成，共 {lockerIndexClasses.Count} 筆");

            List<lockerIndexClass> lockerIndexClasse_buf = new List<lockerIndexClass>();
            for (int i = 0; i < ips.Count; i++)
            {
                string ip = ips[i];
                string outputAddress = PLC_Device_Output.GetAdress().ToUpper();

                lockerIndexClasse_buf = (from temp in lockerIndexClasses
                                         where temp.同步輸出.ToUpper() == outputAddress
                                         select temp).ToList();

                Logger.Log("dialog_main_HRFID", $"[Locker Check] 比對 IP = {ip}，同步輸出 = {outputAddress}，匹配筆數 = {lockerIndexClasse_buf.Count}");

                if (lockerIndexClasse_buf.Count > 0)
                {
                    Logger.Log("dialog_main_HRFID", $"[Locker Check] 符合條件，執行 Function_處理RFID確認流程()");
                    Task.Run(new Action(delegate
                    {
                        RJ_Button_確認_MouseDownEvent(null);
                    }));

                    return;
                }
            }

            Logger.Log("dialog_main_HRFID", $"[Locker Check] 無符合同步輸出條件的 locker 資料，結束流程");
        }

        private void PlC_RJ_Button_解鎖_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                Logger.Log($"dialog_main_HRFID", $"[PlC_RJ_Button_解鎖_MouseDownEvent] 使用者 : {Main_Form._登入者名稱}");

                List<string> ips = new List<string>();
                for (int i = 0; i < takeMedicineStackClasses_temp.Count; i++)
                {
                    ips.LockAdd(Main_Form.Function_取得抽屜以藥品碼解鎖IP(takeMedicineStackClasses_temp[i].藥品碼));
                }

                Main_Form.Function_外門片解鎖(ips);
            }
            catch (Exception ex)
            {
                Logger.Log("dialog_main_HRFID", $"[錯誤] {ex.Message} | {ex.StackTrace}");
            }
            finally
            {
                Logger.Log("dialog_main_HRFID", $"[PlC_RJ_Button_解鎖_MouseDownEvent] 結束執行流程");

            }
        }

        private void Program_HFRFID()
        {
            bool debug = false;

            // 取得藥品堆疊資訊並更新 DataGrid
            List<object[]> list_value = GetTakeMedicineStackAndUpdateGrid(debug);

        }
        private List<object[]> GetTakeMedicineStackAndUpdateGrid(bool debug = false)
        {
            List<object[]> list_value = new List<object[]>();
            List<DrugHFTagClass> drugHFTagClasses = new List<DrugHFTagClass>();

            if (_Import_Export == IncomeOutcomeMode.收入)
            {
                if (debug) Console.WriteLine("取得最新可入庫標籤...");
                drugHFTagClasses = DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server);
            }
            if (_Import_Export == IncomeOutcomeMode.支出)
            {
                if (debug) Console.WriteLine("取得最新可出庫標籤...");
                drugHFTagClasses = DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);
            }

            var list_取藥堆疊母資料 = Main_Form.Function_取藥堆疊資料_取得指定調劑台名稱母資料(_deviceName);
            takeMedicineStackClasses_temp = list_取藥堆疊母資料.ToTakeMedicineStackClassList();

            if (drugHFTagClasses.Count == 0)
            {
                if (debug) Console.WriteLine("無標籤資料，流程中止");
                return null;
            }

            if (takeMedicineStackClasses_temp.Count == 0)
            {
                if (debug) Console.WriteLine($"{_deviceName}, 無堆疊資料，流程中止");
                return null;
            }

            var codeNameAmountList = takeMedicineStackClasses_temp
                .Where(t => t.狀態 == "RFID使用")
                .GroupBy(t => t.藥品碼)
                .Select(g => (
                    藥品碼: g.Key,
                    藥品名稱: g.First().藥品名稱,
                    交易量總和: g.Sum(x => Math.Abs(x.總異動量.StringToDouble()))
                ))
                .ToList();

            if (debug) Console.WriteLine($"需處理藥品共 {codeNameAmountList.Count} 項");

            foreach (var item in codeNameAmountList)
            {
                object[] value = new object[new enum_TagList().GetLength()];
                value[(int)enum_TagList.GUID] = item.藥品碼;
                value[(int)enum_TagList.藥碼] = item.藥品碼;
                value[(int)enum_TagList.藥名] = item.藥品名稱;
                value[(int)enum_TagList.應出] = item.交易量總和;
                value[(int)enum_TagList.實出] = 0;  // 初始實出為 0，待後續更新
                list_value.Add(value);
            }

            this.sqL_DataGridView_TagList.RefreshGrid(list_value);

            return list_value;
        }
        private List<string> UpdateGridWithUIDs(List<object[]> list_value, bool debug = false)
        {
            List<string> uids = Main_Form.ReadAllUIDsOnceOnly();

            List<DrugHFTagClass> drugHFTagClasses = (_Import_Export == IncomeOutcomeMode.收入)
                ? DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server)
                : DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);

            foreach (var value in list_value)
            {
                string drugCode = value[(int)enum_TagList.藥碼]?.ToString();

                var matchedTags = drugHFTagClasses
                    .Where(tag =>
                        tag.藥碼 == drugCode &&
                        (
                            (_Import_Export == IncomeOutcomeMode.收入 && uids.Contains(tag.TagSN)) ||
                            (_Import_Export == IncomeOutcomeMode.支出 && !uids.Contains(tag.TagSN))
                        )
                    ).ToList();

                double qty = matchedTags.Sum(t => t.數量.StringToDouble());

                value[(int)enum_TagList.實出] = qty;

                if (debug)
                {
                    Console.WriteLine($"藥品碼: {drugCode}, 實出/實入 數量: {qty}, 符合標籤數: {matchedTags.Count}");
                }
            }

            this.sqL_DataGridView_TagList.RefreshGrid(list_value);
            return uids;
        }
        /// <summary>
        /// 依本次要處理的藥碼，從雲端儲位資訊彙整出對應的 Reader Index 清單
        /// </summary>
        private void CollectReaderIndexForDrugCodes(IEnumerable<string> drugCodes, HashSet<int> readerIndexSet)
        {
            foreach (var code in drugCodes)
            {
                var list_obj_device = Main_Form.Function_從雲端資料取得儲位(code);
                for (int i = 0; i < list_obj_device.Count; i++)
                {
                    if (list_obj_device[i] is Device device)
                    {
                        foreach (var m in _areaReaders)
                        {
                            if (device.Area.Contains(m.areaKey))
                            {
                                readerIndexSet.Add(m.r1);
                                readerIndexSet.Add(m.r2);
                            }
                        }
                    }
                }
            }
        }


        private void Dialog_HFRFID調劑作業_LoadFinishedEvent(EventArgs e)
        {
            this.sqL_DataGridView_TagList.RowsHeight = 80;
            this.sqL_DataGridView_TagList.Init(new Table(new enum_TagList()));
            this.sqL_DataGridView_TagList.Set_ColumnVisible(false, new enum_TagList().GetEnumNames());
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_TagList.藥碼);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(800, DataGridViewContentAlignment.MiddleLeft, enum_TagList.藥名);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_TagList.應出);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_TagList.實出);
            this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 32), enum_TagList.應出);
            if (_Import_Export == IncomeOutcomeMode.收入)
            {
                this.sqL_DataGridView_TagList.Set_ColumnText("收入", enum_TagList.應出);
                this.sqL_DataGridView_TagList.Set_ColumnText("實入", enum_TagList.實出);
            }
            if (_Import_Export == IncomeOutcomeMode.支出)
            {
                this.sqL_DataGridView_TagList.Set_ColumnText("支出", enum_TagList.應出);
                this.sqL_DataGridView_TagList.Set_ColumnText("實出", enum_TagList.實出);
            }
            this.sqL_DataGridView_TagList.DataGridRefreshEvent += SqL_DataGridView_TagList_DataGridRefreshEvent;
            PlC_RJ_Button_解鎖_MouseDownEvent(null);

            myThread_HFRFID.AutoRun(true);
            myThread_HFRFID.Add_Method(Program_HFRFID);
            myThread_HFRFID.SetSleepTime(100);
            myThread_HFRFID.Trigger();

            Main_Form.Function_外門片解鎖();
            "冰箱門,注意關閉".PlayGooleVoice(Main_Form.API_Server);

        }
        private void Dialog_HFRFID調劑作業_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThread_HFRFID != null)
            {
                myThread_HFRFID.Abort();
                myThread_HFRFID = null;
            }
            Main_Form.LockClosingEvent -= Main_Form_LockClosingEvent;

        }
        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                Dialog_收支異常提示.CloseAllDialog();

                // 取得目前要作業的藥碼清單（以 TagList 為準）
                List<object[]> list_TagList = this.sqL_DataGridView_TagList.GetAllRows();
                if (list_TagList == null || list_TagList.Count == 0)
                {
                    MyMessageBox.ShowDialog("目前無待確認藥品。");
                    return;
                }
                List<string> drugCodes = (from temp in list_TagList
                                          select temp[(int)enum_TagList.藥碼].ObjectToString())
                                         .Distinct().ToList();

                // 取調劑台現有堆疊母資料（用於異常紀錄中的操作人欄位）
                List<object[]> list_取藥堆疊母資料 = Main_Form.Function_取藥堆疊資料_取得指定調劑台名稱母資料(_deviceName);
                List<takeMedicineStackClass> takeMedicineStackClasses_org = list_取藥堆疊母資料.ToTakeMedicineStackClassList();

                // =========================================================
                // Phase-1：僅掃「相關儲位層」（快速數量驗證）
                // =========================================================
                List<string> uids_phase1 = new List<string>();
                List<DrugHFTagClass> DBTags_phase1 = new List<DrugHFTagClass>();
                var readerIndexSet = new HashSet<int>();

                CollectReaderIndexForDrugCodes(drugCodes, readerIndexSet);

                LoadingForm.ShowLoadingForm("讀取同層 UID 中...");
                try
                {
                    // 只掃關聯層；若抓不到任何層，就退回掃全部（避免極端狀況）
                    uids_phase1 = (readerIndexSet.Count > 0)
                        ? Main_Form.ReadAllUIDsOnceOnly(true, readerIndexSet.ToArray())
                        : Main_Form.ReadAllUIDsOnceOnly();

                    // 取可用標籤（依收入/支出模式）
                    DBTags_phase1 = (_Import_Export == IncomeOutcomeMode.收入)
                        ? DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server)
                        : DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);
                }
                finally
                {
                    LoadingForm.CloseLoadingForm();
                }

                // 依 Phase-1 的 UID 做數量快速驗證（僅針對 TagList 裡的藥碼）
                List<DrugHFTagClass> tagDisplayList_phase1 = new List<DrugHFTagClass>();
                foreach (var tag in DBTags_phase1)
                {
                    bool isUIDMatch = uids_phase1.Contains(tag.TagSN);
                    bool isSameCode = drugCodes.Contains(tag.藥碼);

                    if (_Import_Export == IncomeOutcomeMode.收入)
                    {
                        if (isUIDMatch && isSameCode)
                        {
                            tag.狀態 = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                            tagDisplayList_phase1.Add(tag);
                        }
                    }
                    else // 支出
                    {
                        if (!isUIDMatch && isSameCode)
                        {
                            tag.狀態 = enum_DrugHFTagStatus.出庫註記.GetEnumName();
                            tagDisplayList_phase1.Add(tag);
                        }
                        // Phase-1 不做品項檢查，忽略其他情況
                    }
                }

                // 做數量比對（只更新「實出」），若有不符則先處理數量異常，不做全櫃掃描
                List<medRecheckLogClass> errorLogs_phase1 = new List<medRecheckLogClass>();
                bool 數量異常_phase1 = false;

                for (int i = 0; i < list_TagList.Count; i++)
                {
                    string 藥碼 = list_TagList[i][(int)enum_TagList.藥碼].ObjectToString();
                    string 藥名 = list_TagList[i][(int)enum_TagList.藥名].ObjectToString();
                    double 應出 = list_TagList[i][(int)enum_TagList.應出].StringToDouble();

                    var matchedTags = tagDisplayList_phase1.Where(t => t.藥碼 == 藥碼);
                    double 實出 = matchedTags.Sum(t => t.數量.StringToDouble());

                    list_TagList[i][(int)enum_TagList.實出] = 實出;

                    if (Math.Abs(應出 - 實出) > EPS)
                    {
                        數量異常_phase1 = true;
                        Logger.Log("dialog_main_HRFID", $"[Phase-1] 數量不符：藥碼={藥碼}, 應出={應出}, 實出={實出}");

                        errorLogs_phase1.Add(new medRecheckLogClass
                        {
                            GUID = Guid.NewGuid().ToString(),
                            發生類別 = enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName(),
                            藥碼 = 藥碼,
                            藥名 = 藥名,
                            效期 = "",
                            批號 = "",
                            庫存值 = "0",
                            盤點值 = 實出.ToString("0.###"),
                            差異值 = (實出 - 應出).ToString("0.###"),
                            發生時間 = DateTime.Now.ToDateTimeString_6(),
                            排除時間 = DateTime.MinValue.ToDateTimeString(),
                            狀態 = enum_medRecheckLog_State.未排除.GetEnumName(),
                            事件描述 = "數量錯誤",
                            通知註記 = "未通知",
                            通知時間 = DateTime.MinValue.ToDateTimeString(),
                            參數1 = "",
                            參數2 = "",
                            盤點藥師1 = (takeMedicineStackClasses_org.Count > 0) ? takeMedicineStackClasses_org[0].操作人 : ""
                        });
                    }
                }
                this.sqL_DataGridView_TagList.RefreshGrid(list_TagList);

                if (數量異常_phase1)
                {
                    // 只提示「數量錯誤」，不做品項檢查（加速回饋）
                    string tts_content = "數量錯誤,請再次確認";
                    Logger.Log("dialog_main_HRFID", $"[Phase-1] {tts_content}");
                    Dialog_收支異常提示 dialog_收支異常提示 = new Dialog_收支異常提示(
                        $"{(takeMedicineStackClasses_org.Count > 0 ? takeMedicineStackClasses_org[0].操作人 : "")},{tts_content}",
                        errorLogs_phase1
                    );
                    dialog_收支異常提示.IgnoreVisible = hasRetriedConfirmation;
                    dialog_收支異常提示.MouseDownEvent_LokcOpen += PlC_RJ_Button_解鎖_MouseDownEvent;
                    dialog_收支異常提示.ShowDialog();
                    hasRetriedConfirmation = true;

                    if (dialog_收支異常提示.DialogResult == DialogResult.Abort)
                    {
                        Logger.Log("dialog_main_HRFID", "[Phase-1] 第二次仍數量錯誤，寫入異常記錄");
                        LoadingForm.ShowLoadingForm("寫入異常記錄...");
                        try
                        {
                            medRecheckLogClass.add(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, errorLogs_phase1);
                        }
                        finally
                        {
                            LoadingForm.CloseLoadingForm();
                        }
                    }
                    return; // Phase-1 不通過就結束
                }
                "數量驗證完成".PlayGooleVoiceAsync(Main_Form.API_Server);
                // =========================================================
                // Phase-2：全櫃掃描（檢查是否拿錯品項）
                // =========================================================
                List<string> uids_phase2 = new List<string>();
                List<DrugHFTagClass> DBTags_phase2 = new List<DrugHFTagClass>();

                LoadingForm.ShowLoadingForm("全櫃掃描中...");
                try
                {
                    uids_phase2 = Main_Form.ReadAllUIDsOnceOnly(); // 掃全部層
                    DBTags_phase2 = (_Import_Export == IncomeOutcomeMode.收入)
                        ? DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server)
                        : DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);
                }
                finally
                {
                    LoadingForm.CloseLoadingForm();
                }

                List<DrugHFTagClass> tagDisplayList = new List<DrugHFTagClass>();
                List<DrugHFTagClass> errorTags = new List<DrugHFTagClass>();

                foreach (var tag in DBTags_phase2)
                {
                    bool isUIDMatch = uids_phase2.Contains(tag.TagSN);
                    bool isSameCode = drugCodes.Contains(tag.藥碼);

                    if (_Import_Export == IncomeOutcomeMode.收入)
                    {
                        if (isUIDMatch && isSameCode)
                        {
                            tag.狀態 = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                            tagDisplayList.Add(tag);
                        }
                        else if (isUIDMatch && !isSameCode)
                        {
                            // 收入：掃到UID但不是本次藥碼 → 放錯/拿錯
                            errorTags.Add(tag);
                        }
                    }
                    else // 支出
                    {
                        if (!isUIDMatch && isSameCode)
                        {
                            tag.狀態 = enum_DrugHFTagStatus.出庫註記.GetEnumName();
                            tagDisplayList.Add(tag);
                        }
                        else if (!isUIDMatch && !isSameCode)
                        {
                            // 支出：該標籤仍在櫃中、且不在本次藥碼 → 櫃內遺留/可能拿錯
                            errorTags.Add(tag);
                        }
                    }
                }

                // 製作品項錯誤的異常紀錄
                List<medRecheckLogClass> errorLogs_phase2 = new List<medRecheckLogClass>();
                bool 品項錯誤 = (errorTags.Count > 0);

                foreach (var tag in errorTags)
                {
                    errorLogs_phase2.Add(new medRecheckLogClass
                    {
                        GUID = Guid.NewGuid().ToString(),
                        發生類別 = (_Import_Export == IncomeOutcomeMode.收入) ? enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName() : enum_medRecheckLog_ICDT_TYPE.RFID退藥異常.GetEnumName(),
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
                        參數2 = "",
                        盤點藥師1 = (takeMedicineStackClasses_org.Count > 0) ? takeMedicineStackClasses_org[0].操作人 : ""
                    });
                }

                if (品項錯誤)
                {
                    string tts_content = "品項錯誤,請再次確認";
                    Logger.Log("dialog_main_HRFID", $"[Phase-2] {tts_content}");

                    Dialog_收支異常提示 dialog_收支異常提示 = new Dialog_收支異常提示(
                        $"{(takeMedicineStackClasses_org.Count > 0 ? takeMedicineStackClasses_org[0].操作人 : "")},{tts_content}",
                        errorLogs_phase2
                    );
                    dialog_收支異常提示.IgnoreVisible = hasRetriedConfirmation;
                    dialog_收支異常提示.MouseDownEvent_LokcOpen += PlC_RJ_Button_解鎖_MouseDownEvent;
                    dialog_收支異常提示.ShowDialog();
                    hasRetriedConfirmation = true;

                    if (dialog_收支異常提示.DialogResult == DialogResult.Abort)
                    {
                        Logger.Log("dialog_main_HRFID", "[Phase-2] 第二次仍有品項錯誤，寫入異常記錄");
                        LoadingForm.ShowLoadingForm("寫入異常記錄...");
                        try
                        {
                            medRecheckLogClass.add(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, errorLogs_phase2);
                        }
                        finally
                        {
                            LoadingForm.CloseLoadingForm();
                        }
                    }
                    return; // 有品項錯誤則結束流程
                }

                // =========================================================
                // 全部通過 → 後續流程維持原本：寫入 DrugHFTag / 產生 takeMedicineStack
                // =========================================================
                hasRetriedConfirmation = false;

                StringBuilder sb = new StringBuilder();
                List<object[]> list_drugHFTagClasses = tagDisplayList.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>();
                List<DrugHFTagClass> drugHFTagClasses_replace = new List<DrugHFTagClass>();

                takeMedicineStackClasses.Clear();

                for (int i = 0; i < list_TagList.Count; i++)
                {
                    string 藥碼 = list_TagList[i][(int)enum_TagList.藥碼].ObjectToString();
                    string 藥名 = list_TagList[i][(int)enum_TagList.藥名].ObjectToString();
                    double 應出 = list_TagList[i][(int)enum_TagList.應出].StringToDouble();
                    double 實出 = list_TagList[i][(int)enum_TagList.實出].StringToDouble();

                    List<DrugHFTagClass> drugHFTagClasses = (from temp in tagDisplayList
                                                             where temp.藥碼 == 藥碼
                                                             select temp).ToList();
                    drugHFTagClasses_replace.LockAdd(drugHFTagClasses);

                    stockClasses = drugHFTagClasses.GetStockClasses();
                    Logger.Log("dialog_main_HRFID", $"取得有效標籤轉為 StockClass，共 {stockClasses.Count} 筆");

                    if (_Import_Export == IncomeOutcomeMode.收入) sb.AppendLine($"收入藥品品項：{stockClasses.Count}");
                    if (_Import_Export == IncomeOutcomeMode.支出) sb.AppendLine($"支出藥品品項：{stockClasses.Count}");
                    var groupedStocks = stockClasses
                        .GroupBy(x => x.Code)
                        .Select(g => new
                        {
                            藥碼 = g.Key,
                            藥名 = g.First().Name,
                            明細 = g.ToList()
                        }).ToList();

                    foreach (var group in groupedStocks)
                    {
                        sb.AppendLine($"藥碼：{group.藥碼}");
                        sb.AppendLine($"藥名：{group.藥名}");
                        sb.AppendLine($"明細：共 {group.明細.Count} 筆");
                        for (int idx = 0; idx < group.明細.Count; idx++)
                        {
                            var item = group.明細[idx];
                            sb.AppendLine($"  [{idx + 1}] 數量：{item.Qty}，效期：{item.Validity_period}，批號：{item.Lot_number}");
                        }
                        sb.AppendLine(new string('-', 30));
                    }

                    foreach (var stock in stockClasses)
                    {
                        double stockQty = stock.Qty.StringToDouble();
                        if (_Import_Export == IncomeOutcomeMode.支出) stockQty *= -1;

                        string 動作 = (_Import_Export == IncomeOutcomeMode.收入) ? "退" : "領";
                        double 剩餘量 = stockQty;

                        var matchList = takeMedicineStackClasses_org
                            .Where(x => x.動作.Contains(動作) && x.藥品碼 == stock.Code)
                            .ToList();
                        if (matchList.Count == 0) continue;

                        foreach (var org in matchList)
                        {
                            if (Math.Abs(剩餘量) < 0.0001) break;

                            double orgQty = org.總異動量.StringToDouble();
                            if (Math.Abs(orgQty) < 0.0001) continue;

                            double 可用量 = Math.Min(Math.Abs(orgQty), Math.Abs(剩餘量));
                            if (剩餘量 < 0) 可用量 *= -1;

                            var clone = org.DeepClone();
                            clone = Main_Form.Function_取藥堆疊資料_設定作業模式(clone, enum_取藥堆疊母資料_作業模式.RFID使用, false);
                            clone.GUID = Guid.NewGuid().ToString();
                            clone.總異動量 = 可用量.ToString("0.###");
                            clone.效期 = stock.Validity_period;
                            clone.批號 = stock.Lot_number;
                            clone.顏色 = Color.Black.ToColorString();
                            clone.狀態 = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();

                            takeMedicineStackClasses.Add(clone);
                            剩餘量 -= 可用量;
                        }
                    }
                }

                Logger.Log("dialog_main_HRFID", $"呼叫 API 寫入 DrugHFTag 標籤資料，共 {drugHFTagClasses_replace.Count} 筆");
                LoadingForm.ShowLoadingForm();
                try
                {
                    DrugHFTagClass.add(Main_Form.API_Server, drugHFTagClasses_replace);
                }
                finally
                {
                    LoadingForm.CloseLoadingForm();
                }

                Logger.Log("dialog_main_HRFID", $"完成 API 寫入與畫面清除");
                this.sqL_DataGridView_TagList.ClearGrid();
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Log("dialog_main_HRFID", $"[例外] {ex.Message}\n{ex.StackTrace}");
                MyMessageBox.ShowDialog("發生例外錯誤，請查看 log 或聯絡工程人員！");
            }
            finally
            {
                Logger.Log("dialog_main_HRFID", $"[RJ_Button_確認_MouseDownEvent] 結束執行流程");
            }
        }

        private void SqL_DataGridView_TagList_DataGridRefreshEvent()
        {
            for (int i = 0; i < this.sqL_DataGridView_TagList.Rows.Count; i++)
            {
                string 應出 = this.sqL_DataGridView_TagList.Rows[i].Cells[(int)enum_TagList.應出].Value.ToString();
                string 實出 = this.sqL_DataGridView_TagList.Rows[i].Cells[(int)enum_TagList.實出].Value.ToString();
                if (應出 == 實出)
                {
                    this.sqL_DataGridView_TagList.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    this.sqL_DataGridView_TagList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}