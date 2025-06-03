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

        private string _deviceName = $"{Main_Form.ServerName}";
        private personPageClass _personPageClass = new personPageClass();
        private List<DrugHFTagClass> DrugHFTags = new List<DrugHFTagClass>();
        public IncomeOutcomeMode _Import_Export = IncomeOutcomeMode.支出;
        public List<StockClass> stockClasses = new List<StockClass>();
        public List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
        public List<takeMedicineStackClass> takeMedicineStackClasses_temp = new List<takeMedicineStackClass>();
        private List<DrugHFTag_IncomeOutcomeListClass> drugHFTag_IncomeOutcomeListClasses;
        private bool hasRetriedConfirmation = false;
        private MyThread myThread_HFRFID = new MyThread();
        private double qty = 0;
        private int retry = 0;
        public Dialog_HFRFID調劑作業(string deviceName, IncomeOutcomeMode incomeOutcomeMode )
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

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            this.plC_RJ_Button_解鎖.MouseDownEvent += PlC_RJ_Button_解鎖_MouseDownEvent;
            Main_Form.LockClosingEvent += Main_Form_LockClosingEvent;

            myThread_HFRFID.AutoRun(true);
            myThread_HFRFID.Add_Method(Program_HFRFID);
            myThread_HFRFID.SetSleepTime(100);
            myThread_HFRFID.Trigger();
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
                    //Function_處理RFID確認流程();
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

                Main_Form.Function_外門片解鎖();
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
            List<object[]> list_value = new List<object[]>();
            List<DrugHFTagClass> drugHFTagClasses = new List<DrugHFTagClass>();
            if (_Import_Export == IncomeOutcomeMode.收入)
                drugHFTagClasses = DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server);
            if (_Import_Export == IncomeOutcomeMode.支出)
                drugHFTagClasses = DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);

            List<object[]> list_取藥堆疊母資料 = Main_Form.Function_取藥堆疊資料_取得指定調劑台名稱母資料(_deviceName);
            takeMedicineStackClasses_temp = list_取藥堆疊母資料.ToTakeMedicineStackClassList();
            if (drugHFTagClasses.Count == 0 || takeMedicineStackClasses.Count == 0) return;

            List<(string 藥品碼, string 藥品名稱, double 交易量總和)> codeNameAmountList = takeMedicineStackClasses
                  .Where(t => t.狀態 == "RFID使用")
                  .GroupBy(t => t.藥品碼)
                  .Select(g => (
                      藥品碼: g.Key,
                      藥品名稱: g.First().藥品名稱,
                      交易量總和: g.Sum(x => Math.Abs(x.總異動量.StringToDouble()))
                  ))
                  .ToList();

            foreach (var item in codeNameAmountList)
            {
                Console.WriteLine($"藥品碼: {item.藥品碼}, 藥品名稱: {item.藥品名稱}, 交易量總和: {item.交易量總和}");
     
                drugHFTag_IncomeOutcomeListClasses = drugHFTagClasses.ToIncomeOutcomeList(_Import_Export);
                List<string> drugCodes = drugHFTag_IncomeOutcomeListClasses.Select(x => x.藥碼).Distinct().ToList();

                string selectedDrugCode = item.藥品碼;
                List<string> uids = Main_Form.rfidReader_TagUID;

                List<DrugHFTagClass> tagDisplayList = new List<DrugHFTagClass>();
                foreach (var tag in drugHFTagClasses)
                {
                    bool isUIDMatch = uids.Contains(tag.TagSN);
                    bool isSameCode = tag.藥碼 == selectedDrugCode;

                    if (_Import_Export == IncomeOutcomeMode.收入)
                    {
                        if (isUIDMatch && isSameCode)
                        {
                            tag.狀態 = enum_DrugHFTagStatus.入庫註記.GetEnumName();
                            tagDisplayList.Add(tag);
                        }
                    }
                    else if (_Import_Export == IncomeOutcomeMode.支出)
                    {
                        if (!isUIDMatch && isSameCode)
                        {
                            tag.狀態 = enum_DrugHFTagStatus.出庫註記.GetEnumName();
                            tagDisplayList.Add(tag);
                        }
                 
                    }
                }

                // 計算收支數量（只包含已標記的入庫註記或出庫註記）
                qty = tagDisplayList
                    .Where(t =>
                        (_Import_Export == IncomeOutcomeMode.收入 && t.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()) ||
                        (_Import_Export == IncomeOutcomeMode.支出 && t.狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName()))
                    .Sum(t => t.數量.StringToDouble());

                DrugHFTags = tagDisplayList;

                object[] value = new object[new enum_TagList().GetLength()];
                value[(int)enum_TagList.GUID] = item.藥品碼;
                value[(int)enum_TagList.藥碼] = item.藥品碼;
                value[(int)enum_TagList.藥名] = item.藥品名稱;
                value[(int)enum_TagList.應出] = item.交易量總和;
                value[(int)enum_TagList.實出] = qty;
                list_value.Add(value);
            }

            this.sqL_DataGridView_TagList.RefreshGrid(list_value);
        }

        private void Dialog_HFRFID調劑作業_LoadFinishedEvent(EventArgs e)
        {
            this.sqL_DataGridView_TagList.RowsHeight = 65;
            this.sqL_DataGridView_TagList.Init(new Table(new enum_TagList()));
            this.sqL_DataGridView_TagList.Set_ColumnVisible(false, new enum_TagList().GetEnumNames());
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_TagList.藥碼);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(700, DataGridViewContentAlignment.MiddleLeft, enum_TagList.藥名);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_TagList.應出);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_TagList.實出);
            this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 32), enum_TagList.應出);
            this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 32), enum_TagList.實出);
            if (_Import_Export == IncomeOutcomeMode.收入)
            {
                this.sqL_DataGridView_TagList.Set_ColumnText("應入", enum_TagList.應出);
                this.sqL_DataGridView_TagList.Set_ColumnText("實入", enum_TagList.實出);
            }

            this.sqL_DataGridView_TagList.DataGridRefreshEvent += SqL_DataGridView_TagList_DataGridRefreshEvent;
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
                List<object[]> list_TagList = this.sqL_DataGridView_TagList.GetAllRows();
                List<medRecheckLogClass> errorLogs = new List<medRecheckLogClass>();

                bool 數量異常 = false;
                for (int i = 0; i < list_TagList.Count; i++)
                {
                    string 藥碼 = list_TagList[i][(int)enum_TagList.藥碼].ObjectToString();
                    string 藥名 = list_TagList[i][(int)enum_TagList.藥名].ObjectToString();
                    double 應出 = list_TagList[i][(int)enum_TagList.應出].StringToDouble();
                    double 實出 = list_TagList[i][(int)enum_TagList.實出].StringToDouble();
                    if (應出 != 實出)
                    {
                        數量異常 = true;
                        Logger.Log("dialog_main_HRFID", $"[RJ_Button_確認_MouseDownEvent] 藥品碼: {藥碼}, 藥名: {藥名}, 應出: {應出}, 實出: {實出}");

                        double 實際標籤數量 = 實出;
                        double temp = 應出;
                        var qtyLog = new medRecheckLogClass
                        {
                            GUID = Guid.NewGuid().ToString(),
                            發生類別 = (_Import_Export == IncomeOutcomeMode.收入) ? enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName() : enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName(),
                            藥碼 = 藥碼,
                            藥名 = 藥名,
                            效期 = "",
                            批號 = "",
                            庫存值 = "0",
                            盤點值 = 實際標籤數量.ToString("0.###"),
                            差異值 = (實際標籤數量 - temp).ToString("0.###"),
                            發生時間 = DateTime.Now.ToDateTimeString_6(),
                            排除時間 = DateTime.MinValue.ToDateTimeString(),
                            狀態 = enum_medRecheckLog_State.未排除.GetEnumName(),
                            事件描述 = "取藥數量與標籤數量不符",
                            通知註記 = "未通知",
                            通知時間 = DateTime.MinValue.ToDateTimeString(),
                            參數1 = "",
                            參數2 = ""
                        };
                        errorLogs.Add(qtyLog);
                    }
                }
                if (數量異常)
                {
                    if (!hasRetriedConfirmation)
                    {
                        hasRetriedConfirmation = true;
                        Logger.Log("dialog_main_HRFID", $"[RJ_Button_確認_MouseDownEvent] 偵測到數量異常，請確認標籤數量是否正確");
                        Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\alarm.wav");
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("偵測到數量不符,請確認標籤數量是否正確", 2000);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }



                    if (errorLogs.Count > 0)
                    {
                        Logger.Log("dialog_main_HRFID", $"自動寫入異常標籤與數量記錄 {errorLogs.Count} 筆");
                        LoadingForm.ShowLoadingForm();
                        medRecheckLogClass.add(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, errorLogs);
                        LoadingForm.CloseLoadingForm();
                        MyMessageBox.ShowDialog($"⚠ 已自動新增異常記錄 {errorLogs.Count} 筆，請查閱紀錄！");
                    }

                }

                hasRetriedConfirmation = false;
                StringBuilder sb = new StringBuilder();
                List<object[]> list_drugHFTagClasses = DrugHFTags.ClassToSQL<DrugHFTagClass, enum_DrugHFTag>();
                List<DrugHFTagClass> drugHFTagClasses_replace = new List<DrugHFTagClass>();

                takeMedicineStackClasses.Clear();

                for (int i = 0; i < list_TagList.Count; i++)
                {
                    string 藥碼 = list_TagList[i][(int)enum_TagList.藥碼].ObjectToString();
                    string 藥名 = list_TagList[i][(int)enum_TagList.藥名].ObjectToString();
                    double 應出 = list_TagList[i][(int)enum_TagList.應出].StringToDouble();
                    double 實出 = list_TagList[i][(int)enum_TagList.實出].StringToDouble();
                    List<DrugHFTagClass> drugHFTagClasses = (from temp in DrugHFTags
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

                    List<object[]> list_取藥堆疊母資料 = Main_Form.Function_取藥堆疊資料_取得指定調劑台名稱母資料(_deviceName);
                    List<takeMedicineStackClass> takeMedicineStackClasses_org = list_取藥堆疊母資料.ToTakeMedicineStackClassList();

                    foreach (var stock in stockClasses)
                    {
                        double stockQty = stock.Qty.StringToDouble();
                        if (_Import_Export == IncomeOutcomeMode.支出) stockQty *= -1;

                        string 動作 = (_Import_Export == IncomeOutcomeMode.收入) ?
                            "退" :
                            "領";

                        double 剩餘量 = stockQty;

                        var matchList = takeMedicineStackClasses_org
                            .Where(x => x.動作.Contains(動作) && x.藥品碼 == stock.Code)
                            .ToList();

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
                            clone.狀態 = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();

                            takeMedicineStackClasses.Add(clone);
                            剩餘量 -= 可用量;
                        }

                        // 若還有殘量未分配，補新增一筆
                        if (Math.Abs(剩餘量) > 0.0001)
                        {
                            takeMedicineStackClass newItem = new takeMedicineStackClass
                            {
                                GUID = Guid.NewGuid().ToString(),
                                動作 = 動作,
                                調劑台名稱 = _deviceName,
                                藥品碼 = stock.Code,
                                藥品名稱 = stock.Name,
                                總異動量 = 剩餘量.ToString("0.###"),
                                效期 = stock.Validity_period,
                                批號 = stock.Lot_number,
                                狀態 = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName(),
                                操作人 = takeMedicineStackClasses_org[0].操作人,
                                ID = takeMedicineStackClasses_org[0].ID,
                                顏色 = takeMedicineStackClasses_org[0].顏色
                            };
                            takeMedicineStackClasses.Add(newItem);
                        }
                    }


                }
                if (MyMessageBox.ShowDialog(sb.ToString(), MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes)
                {
                    Logger.Log("dialog_main_HRFID", "使用者取消確認提交操作");
                    return;
                }

                Logger.Log("dialog_main_HRFID", $"呼叫 API 寫入 DrugHFTag 標籤資料，共 {drugHFTagClasses_replace.Count} 筆");
                LoadingForm.ShowLoadingForm();
                DrugHFTagClass.add(Main_Form.API_Server, drugHFTagClasses_replace);
                LoadingForm.CloseLoadingForm();
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
