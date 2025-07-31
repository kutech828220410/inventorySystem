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
    public partial class Dialog_收支作業_RFID清點作業 : MyDialog
    {
        public enum enum_庫存訊息 : int
        {
            [Description("GUID,VARCHAR,50,PRIMARY")]
            GUID,
            [Description("效期,VARCHAR,300,NONE")]
            效期,
            [Description("批號,VARCHAR,300,NONE")]
            批號,
            [Description("庫存,VARCHAR,300,NONE")]
            庫存,
          
        }

        private string drugCode = "";
        private string drugName = "";
        private Dictionary<string, List<medRecheckLogClass>> keyValuePairs_medRecheckLogClass;
        private Dictionary<string, List<DrugHFTagClass>> keyValuePairs_drugHFTagClasses;
        public Dialog_收支作業_RFID清點作業(string _drugCode , string _drugName )
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
           
            this.LoadFinishedEvent += Dialog_收支作業_RFID清點作業_LoadFinishedEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            drugCode = _drugCode;
            drugName = _drugName;
        }

        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_medRecheckLog = sqL_DataGridView_異常事件.GetAllRows();
            if (list_medRecheckLog.Count > 0)
            {
                if (MyMessageBox.ShowDialog($"確認排除<{list_medRecheckLog.Count}>筆異常事件?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    medRecheckLogClass.set_unresolved_data_by_code(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, this.drugCode, Main_Form._登入者名稱);
                }
            }
            if (MyMessageBox.ShowDialog($"確認異動庫存?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
            {
                List<object> devices_obj = Main_Form.Function_從雲端資料取得儲位(this.drugCode);
                for (int i = 0; i < devices_obj.Count; i++)
                {
                    if (devices_obj[i] is Device)
                    {
                        Device device = (Device)(devices_obj[i]);
                        device.清除所有庫存資料();
                    }
                }
                if (devices_obj.Count > 0)
                {
                    List<object[]> list_value = sqL_DataGridView_實際庫存.GetAllRows();

                    for (int i = 0; i < list_value.Count; i++)
                    {
                        string 效期 = list_value[i][(int)enum_庫存訊息.效期].ObjectToString();
                        string 批號 = list_value[i][(int)enum_庫存訊息.批號].ObjectToString();
                        string 庫存 = list_value[i][(int)enum_庫存訊息.庫存].ObjectToString();
                        if (devices_obj[0] is Device)
                        {
                            Device device = (Device)(devices_obj[0]);
                            device.新增效期(效期, 批號, 庫存);
                        }
                    }
                    if (devices_obj[0] is Storage)
                    {
                        Storage storage = (Storage)(devices_obj[0]);
                        double 原有庫存 = storage.取得庫存();
                        string 藥品碼 = storage.Code;
                        藥品碼 = Main_Form.Function_藥品碼檢查(藥品碼);
                        string 庫存量 = Main_Form.Function_從SQL取得庫存(藥品碼).ToString();
                        double 修正庫存 = storage.取得庫存();
                        Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
                        Main_Form.List_EPD266_本地資料.Add_NewStorage(storage);


                        string GUID = Guid.NewGuid().ToString();
                        string 動作 = enum_交易記錄查詢動作.效期庫存異動.GetEnumName();
                        string 藥品名稱 = storage.Name;
                        string 藥袋序號 = "";
                        string 交易量 = (修正庫存 - 庫存量.StringToDouble()).ToString();
                        string 結存量 = Main_Form.Function_從SQL取得庫存(藥品碼).ToString();
                        string 操作人 = Main_Form._登入者名稱;
                        string 病人姓名 = "";
                        string 病歷號 = "";
                        string 操作時間 = DateTime.Now.ToDateTimeString_6();
                        string 開方時間 = DateTime.Now.ToDateTimeString_6();
                        string 備註 = "";
                        for (int i = 0; i < list_value.Count; i++)
                        {
                            string 效期 = list_value[i][(int)enum_庫存訊息.效期].ObjectToString();
                            string 批號 = list_value[i][(int)enum_庫存訊息.批號].ObjectToString();
                            string 庫存 = list_value[i][(int)enum_庫存訊息.庫存].ObjectToString();
                            備註 = $"[效期]:{效期},[批號]:{批號},[庫存]:{庫存}";
                            if (i != list_value.Count - 1) 備註 += "\n";
                        }
                        object[] value_trading = new object[new enum_交易記錄查詢資料().GetLength()];
                        value_trading[(int)enum_交易記錄查詢資料.GUID] = GUID;
                        value_trading[(int)enum_交易記錄查詢資料.動作] = 動作;
                        value_trading[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
                        value_trading[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
                        value_trading[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
                        value_trading[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
                        value_trading[(int)enum_交易記錄查詢資料.交易量] = 交易量;
                        value_trading[(int)enum_交易記錄查詢資料.結存量] = 結存量;
                        value_trading[(int)enum_交易記錄查詢資料.操作人] = 操作人;
                        value_trading[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
                        value_trading[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
                        value_trading[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
                        value_trading[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
                        value_trading[(int)enum_交易記錄查詢資料.備註] = 備註;
                        value_trading[(int)enum_交易記錄查詢資料.收支原因] = "庫存異動";
                        value_trading[(int)enum_交易記錄查詢資料.藥師證字號] = Main_Form._登入者藥師證字號;
                        Main_Form._sqL_DataGridView_交易記錄查詢.SQL_AddRow(value_trading, false);


                    }
                }
            }
            
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Close();
        }

        private void Dialog_收支作業_RFID清點作業_LoadFinishedEvent(EventArgs e)
        {
            sqL_DataGridView_理論庫存.RowsHeight = 40;
            sqL_DataGridView_理論庫存.Init(new Table(new enum_庫存訊息()));
            sqL_DataGridView_理論庫存.Set_ColumnVisible(false, new enum_庫存訊息().GetEnumNames());
            sqL_DataGridView_理論庫存.Set_ColumnWidth(200, enum_庫存訊息.效期);
            sqL_DataGridView_理論庫存.Set_ColumnWidth(200, enum_庫存訊息.批號);
            sqL_DataGridView_理論庫存.Set_ColumnWidth(120, enum_庫存訊息.庫存);


            sqL_DataGridView_實際庫存.RowsHeight = 40;
            sqL_DataGridView_實際庫存.Init(new Table(new enum_庫存訊息()));
            sqL_DataGridView_實際庫存.Set_ColumnVisible(false, new enum_庫存訊息().GetEnumNames());
            sqL_DataGridView_實際庫存.Set_ColumnWidth(200, enum_庫存訊息.效期);
            sqL_DataGridView_實際庫存.Set_ColumnWidth(200, enum_庫存訊息.批號);
            sqL_DataGridView_實際庫存.Set_ColumnWidth(120, enum_庫存訊息.庫存);


            rJ_Lable_藥名.Text = $"({this.drugCode}){this.drugName}";

            LoadingForm.ShowLoadingForm();
            List<DrugHFTagClass> drugHFTagClasses = DrugHFTagClass.get_latest_tags(Main_Form.API_Server);
            List<medRecheckLogClass> medRecheckLogClasses = medRecheckLogClass.get_all_unresolved_data(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType);
            keyValuePairs_medRecheckLogClass = medRecheckLogClasses.CoverToDictionaryBy_Code();
            keyValuePairs_drugHFTagClasses = drugHFTagClasses.CoverToDictionaryBy_Code();

            List<DrugHFTagClass> drugHFTagClasses_buf = keyValuePairs_drugHFTagClasses.SortDictionaryBy_Code(this.drugCode);

            drugHFTagClasses_buf = (from temp in drugHFTagClasses_buf
                                    where Main_Form.stocks_uids.Contains(temp.TagSN)
                                    select temp).ToList();

            DrugHFTagClass.set_tag_stockin(Main_Form.API_Server, drugHFTagClasses_buf);


            List<StockClass> stockClasses = new List<StockClass>();
            stockClasses = drugHFTagClasses_buf.GetStockClasses();
            for (int i = 0; i < stockClasses.Count; i++)
            {
                object[] value = new object[new enum_庫存訊息().GetLength()];
                value[(int)enum_庫存訊息.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_庫存訊息.效期] = stockClasses[i].Validity_period;
                value[(int)enum_庫存訊息.批號] = stockClasses[i].Lot_number;
                value[(int)enum_庫存訊息.庫存] = stockClasses[i].Qty;
                sqL_DataGridView_實際庫存.AddRow(value, true);

            }
            stockClasses = Main_Form.Function_取得庫存值從雲端資料(this.drugCode);
            for (int i = 0; i < stockClasses.Count; i++)
            {
                object[] value = new object[new enum_庫存訊息().GetLength()];
                value[(int)enum_庫存訊息.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_庫存訊息.效期] = stockClasses[i].Validity_period;
                value[(int)enum_庫存訊息.批號] = stockClasses[i].Lot_number;
                value[(int)enum_庫存訊息.庫存] = stockClasses[i].Qty;
                sqL_DataGridView_理論庫存.AddRow(value, true);

            }

            Table table = medRecheckLogClass.init(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType);
            sqL_DataGridView_異常事件.InitEx(table);
            sqL_DataGridView_異常事件.Set_ColumnVisible(false, new enum_medRecheckLog().GetEnumNames());
            sqL_DataGridView_異常事件.Set_ColumnWidth(400, enum_medRecheckLog.事件描述);
            sqL_DataGridView_異常事件.Set_ColumnWidth(250, enum_medRecheckLog.發生時間);
            sqL_DataGridView_異常事件.Set_ColumnWidth(150, enum_medRecheckLog.發生類別);
            sqL_DataGridView_異常事件.Set_ColumnWidth(150, enum_medRecheckLog.盤點藥師1);
            sqL_DataGridView_異常事件.Set_ColumnWidth(150, enum_medRecheckLog.差異值);
            sqL_DataGridView_異常事件.Set_ColumnWidth(150, enum_medRecheckLog.參數1);
            sqL_DataGridView_異常事件.Set_ColumnText("操作人", enum_medRecheckLog.盤點藥師1);

            List<medRecheckLogClass> medRecheckLogClasses_buf = keyValuePairs_medRecheckLogClass.SortDictionaryBy_Code(this.drugCode);
            List<object[]> list_medRecheckLog = medRecheckLogClasses_buf.ClassToSQL<medRecheckLogClass, enum_medRecheckLog>();
            sqL_DataGridView_異常事件.RefreshGrid(list_medRecheckLog);
            LoadingForm.CloseLoadingForm();
        }
    }
}
