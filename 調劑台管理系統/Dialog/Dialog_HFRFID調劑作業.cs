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
        private string 調劑台名稱 = $"{Main_Form.ServerName}";
        private List<DrugHFTagClass> errorTags = new List<DrugHFTagClass>();
        private takeMedicineStackClass _takeMedicineStackClass = null;
        public IncomeOutcomeMode _Import_Export = IncomeOutcomeMode.支出;
        public List<StockClass> stockClasses = new List<StockClass>();
        public List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();

        private bool hasRetriedConfirmation = false;
        private MyThread myThread_HFRFID = new MyThread();
        private double qty = 0;
        private int retry = 0;
        public Dialog_HFRFID調劑作業(takeMedicineStackClass takeMedicineStackClass)
        {
            form.Invoke(new Action(delegate
            {
                InitializeComponent();
            }));
            if (takeMedicineStackClass.動作.Contains("退")) _Import_Export = IncomeOutcomeMode.收入;
            _takeMedicineStackClass = takeMedicineStackClass;
            label_藥碼.Text = takeMedicineStackClass.藥品碼;
            label_藥名.Text = takeMedicineStackClass.藥品名稱;
            label_交易量.Text = takeMedicineStackClass.總異動量;
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


            Logger.Log("dialog_main_HRFID", $"[Locker Check] 藥碼 = {label_藥碼.Text}，開始查詢解鎖 IP");

            List<object[]> list_locker_table_value = Main_Form._sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            Logger.Log("dialog_main_HRFID", $"[Locker Check] 取得 locker_index_table 筆數 = {list_locker_table_value.Count}");

            List<string> ips = Main_Form.Function_取得抽屜以藥品碼解鎖IP(label_藥碼.Text);
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
            List<DrugHFTagClass> drugHFTagClasses = new List<DrugHFTagClass>();
            if (_Import_Export == IncomeOutcomeMode.收入)
                drugHFTagClasses = DrugHFTagClass.get_latest_stockin_eligible_tags(Main_Form.API_Server);
            if (_Import_Export == IncomeOutcomeMode.支出)
                drugHFTagClasses = DrugHFTagClass.get_latest_stockout_eligible_tags(Main_Form.API_Server);


            if (drugHFTagClasses.Count == 0) return;

            List<DrugHFTag_IncomeOutcomeListClass> drugHFTag_IncomeOutcomeListClasses = drugHFTagClasses.ToIncomeOutcomeList(_Import_Export);
            List<string> drugCodes = drugHFTag_IncomeOutcomeListClasses.Select(x => x.藥碼).Distinct().ToList();
         
            string selectedDrugCode = _takeMedicineStackClass.藥品碼;
            List<string> uids = Main_Form.rfidReader_TagUID;

            List<DrugHFTagClass> tagDisplayList = new List<DrugHFTagClass>();
            errorTags.Clear();
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
                    else if (!isUIDMatch && !isSameCode)
                    {
                        // ⚠ 異常資料：不處理狀態，但加入顯示
                        errorTags.Add(tag);
                    }
                }
            }

            // 計算收支數量（只包含已標記的入庫註記或出庫註記）
            qty = tagDisplayList
                .Where(t =>
                    (_Import_Export == IncomeOutcomeMode.收入 && t.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName()) ||
                    (_Import_Export == IncomeOutcomeMode.支出 && t.狀態 == enum_DrugHFTagStatus.出庫註記.GetEnumName()))
                .Sum(t => t.數量.StringToDouble());

       

            this.sqL_DataGridView_TagList.RefreshGrid(tagDisplayList.ToObjectList());
        }

        private void Dialog_HFRFID調劑作業_LoadFinishedEvent(EventArgs e)
        {
            this.sqL_DataGridView_TagList.RowsHeight = 50;
            this.sqL_DataGridView_TagList.Init(DrugHFTagClass.init(Main_Form.API_Server));
            this.sqL_DataGridView_TagList.Set_ColumnVisible(false, new enum_DrugHFTag().GetEnumNames());
            this.sqL_DataGridView_TagList.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.TagSN);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.藥碼);
            //this.sqL_DataGridView_TagList.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_DrugHFTag.藥名);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.效期);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.批號);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.數量);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.更新時間);
            this.sqL_DataGridView_TagList.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_DrugHFTag.狀態);
            //this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.TagSN);
            //this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.效期);
            //this.sqL_DataGridView_TagList.Set_ColumnFont(new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Pixel), enum_DrugHFTag.更新時間);
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
                bool 驗證失敗 = false;
                bool 數量異常 = false;

                Logger.Log("dialog_main_HRFID", $"[RJ_Button_確認_MouseDownEvent] 使用者: {Main_Form._登入者名稱}");

                List<object[]> list_drugHFTagClasses = this.sqL_DataGridView_TagList.GetAllRows();
                Logger.Log("dialog_main_HRFID", $"取得標籤筆數: {list_drugHFTagClasses.Count}");
                if (list_drugHFTagClasses.Count == 0)
                {
                    Logger.Log("dialog_main_HRFID", "未讀取到任何 RFID 標籤");
                    MyMessageBox.ShowDialog("未讀取到RFID標籤");
                    return;
                }
                if (_Import_Export == IncomeOutcomeMode.支出 && label_交易量.Text.StringToDouble() != qty)
                {
                    Logger.Log("dialog_main_HRFID", $"標籤數量 {label_交易量.Text} ≠ 輸入數量 {qty}");

                    數量異常 = true;
                }
                if (errorTags.Count > 0)
                {
                    Logger.Log("dialog_main_HRFID", $"偵測到異常標籤 {errorTags.Count} 筆");
                    驗證失敗 = true;
                }
                //if (_Import_Export == IncomeOutcomeMode.支出 && label_交易量.Text.StringToDouble() != qty)
                //{
                //    double temp = label_交易量.Text.StringToDouble() * -1;
                //    if (temp != qty)
                //    {
                //        if(retry == 0)
                //        {
                //            Logger.Log("dialog_main_HRFID", $"標籤數量 ({qty}) 不等於輸入支出數量 ({temp})");
                //            MyMessageBox.ShowDialog("RFID標籤數量與支出數量不符,請再次確認數量");
                //            retry++;
                //            return;
                //        }
                //        else if (retry > 0)
                //        {
                //            Dialog_收支原因選擇 dialog_收支原因選擇 = new Dialog_收支原因選擇();
                //            if(dialog_收支原因選擇.ShowDialog() == DialogResult.Yes)
                //            {
                //                string reason = dialog_收支原因選擇.Value;
                //                Logger.Log("dialog_main_HRFID", $"使用者輸入原因: {reason}");
                //                _takeMedicineStackClass.收支原因 = reason;

                //            }
                //            else
                //            {
                //                Logger.Log("dialog_main_HRFID", "使用者取消輸入原因");
                //                return;
                //            }

                //        }

                //    }
                //}

                if (驗證失敗)
                {
                    if (!hasRetriedConfirmation)
                    {
                        hasRetriedConfirmation = true;
                        Logger.Log("dialog_main_HRFID", $"第一次驗證失敗，提示使用者重新掃描");
                        Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\alarm.wav");
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("偵測到數量不符或異常標籤,請重新掃描標籤後再按一次確認", 2000);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }

                    Logger.Log("dialog_main_HRFID", $"第二次驗證仍失敗，自動記錄異常");

                    List<medRecheckLogClass> errorLogs = new List<medRecheckLogClass>();

                    foreach (var tag in errorTags)
                    {
                        var log = new medRecheckLogClass
                        {
                            GUID = Guid.NewGuid().ToString(),
                            發生類別 = (_Import_Export == IncomeOutcomeMode.收入) ? enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName() : enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName(),
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
                            事件描述 = "標籤未註記為入/出庫",
                            通知註記 = "未通知",
                            通知時間 = DateTime.MinValue.ToDateTimeString(),
                            參數1 = tag.TagSN,
                            參數2 = ""
                        };
                        errorLogs.Add(log);
                    }

                    if (數量異常)
                    {
                        double 實際標籤數量 = qty;
                        double temp = label_交易量.Text.StringToDouble();
                        var qtyLog = new medRecheckLogClass
                        {
                            GUID = Guid.NewGuid().ToString(),
                            發生類別 = (_Import_Export == IncomeOutcomeMode.收入) ? enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName() : enum_medRecheckLog_ICDT_TYPE.RFID調劑異常.GetEnumName(),
                            藥碼 = label_藥碼.Text,
                            藥名 = label_藥名.Text,
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

                List<DrugHFTagClass> drugHFTagClasses = list_drugHFTagClasses.ToDrugHFTagClassList();

                if (_Import_Export == IncomeOutcomeMode.收入)
                {
                    drugHFTagClasses = drugHFTagClasses
                        .Where(drugHFTagClass => drugHFTagClass.狀態 == enum_DrugHFTagStatus.入庫註記.GetEnumName())
                        .ToList();
                }

                stockClasses = drugHFTagClasses.GetStockClasses();
                Logger.Log("dialog_main_HRFID", $"取得有效標籤轉為 StockClass，共 {stockClasses.Count} 筆");

                StringBuilder sb = new StringBuilder();
                if (_Import_Export == IncomeOutcomeMode.收入) sb.AppendLine($"收入藥品品項：{stockClasses.Count}");
                if (_Import_Export == IncomeOutcomeMode.支出) sb.AppendLine($"支出藥品品項：{stockClasses.Count}");
                sb.AppendLine(new string('-', 30));

                for (int i = 0; i < stockClasses.Count; i++)
                {
                    sb.AppendLine($"[{i + 1}]");
                    sb.AppendLine($"藥碼：{stockClasses[i].Code}");
                    sb.AppendLine($"藥名：{stockClasses[i].Name}");
                    sb.AppendLine($"數量：{stockClasses[i].Qty}");
                    sb.AppendLine($"效期：{stockClasses[i].Validity_period}");
                    sb.AppendLine($"批號：{stockClasses[i].Lot_number}");
                    sb.AppendLine();

                }

                if (MyMessageBox.ShowDialog(sb.ToString(), MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes)
                {
                    Logger.Log("dialog_main_HRFID", "使用者取消確認提交操作");
                    return;
                }

             

                for (int i = 0; i < stockClasses.Count; i++)
                {
                    takeMedicineStackClass takeMedicineStackClass = _takeMedicineStackClass.DeepClone();
                    double _qty = stockClasses[i].Qty.StringToDouble();
                    if(_Import_Export == IncomeOutcomeMode.支出) _qty *= -1;
                    takeMedicineStackClass.GUID = Guid.NewGuid().ToString();
                    takeMedicineStackClass.藥品碼 = stockClasses[i].Code;
                    takeMedicineStackClass.藥品名稱 = stockClasses[i].Name;
                    takeMedicineStackClass.總異動量 = _qty.ToString();
                    takeMedicineStackClass.效期 = stockClasses[i].Validity_period;
                    takeMedicineStackClass.批號 = stockClasses[i].Lot_number;
                    takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.等待刷新.GetEnumName();
                    takeMedicineStackClasses.Add(takeMedicineStackClass);
                }


                Logger.Log("dialog_main_HRFID", $"呼叫 API 寫入 DrugHFTag 標籤資料，共 {drugHFTagClasses.Count} 筆");
                LoadingForm.ShowLoadingForm();
                DrugHFTagClass.add(Main_Form.API_Server, drugHFTagClasses);
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
    }
}
