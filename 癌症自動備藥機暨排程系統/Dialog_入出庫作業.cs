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
namespace 癌症自動備藥機暨排程系統
{
    public partial class Dialog_入出庫作業 : MyDialog
    {
        public enum enum_type
        {
            入庫,
            出庫,
        }
        public int 數量 = 0;
        public string 效期 = "";
        public string 批號 = "";
        public Storage storage;
        private StorageUI_EPD_266 _storageUI_EPD_266;
        private string _藥碼;
        private string _藥名;
        private string _操作人;
        private enum_type _enum_type;
        

        public Dialog_入出庫作業(enum_type _enum_type ,string 操作人, string 藥碼 ,string 藥名, StorageUI_EPD_266 storageUI_EPD_266)
        {
            InitializeComponent();

            this.Load += Dialog_入出庫作業_Load;

            if(_enum_type == enum_type.出庫)
            {
                rJ_Lable_狀態.Text = "出庫";
                rJ_Lable_狀態.BackgroundColor = Color.Red;

                List<StepEntity> list = new List<StepEntity>();
                list.Add(new StepEntity("1", "儲位選擇", 1, "請選擇出庫位置", eumStepState.Completed, null));
                list.Add(new StepEntity("2", "輸入數量", 2, "填入出庫數量", eumStepState.Waiting, null));
                list.Add(new StepEntity("3", "完成", 3, "確認交易", eumStepState.Waiting, null));

     
                this.stepViewer1.CurrentStep = 1;
                this.stepViewer1.ListDataSource = list;
            }
            else if (_enum_type == enum_type.入庫)
            {
                List<StepEntity> list = new List<StepEntity>();
                list.Add(new StepEntity("1", "儲位選擇", 1, "請選擇入庫位置", eumStepState.Completed, null));
                list.Add(new StepEntity("2", "藥品資訊輸入", 2, "請輸入效期批號", eumStepState.Completed, null));
                list.Add(new StepEntity("3", "輸入數量", 3, "填入入庫數量", eumStepState.Waiting, null));
                list.Add(new StepEntity("4", "完成", 4, "確認交易", eumStepState.Waiting, null));
                this.stepViewer1.CurrentStep = 1;
                this.stepViewer1.ListDataSource = list;
            }
            this._enum_type = _enum_type;
            this._藥碼 = 藥碼;
            this._藥名 = 藥名;
            this._操作人 = 操作人;
            this._storageUI_EPD_266 = storageUI_EPD_266;
            this.Shown += Dialog_入出庫作業_Shown;
            this.plC_RJ_Button_確認完成.MouseDownEvent += PlC_RJ_Button_確認完成_MouseDownEvent;
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.rJ_Lable_藥品資訊.Text = $"({this._藥碼 }) {this._藥名 }";

         
        }

       

        private void Dialog_入出庫作業_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.Screen[] screen = System.Windows.Forms.Screen.AllScreens;
            if (_enum_type == enum_type.出庫)
            {
                Dialog_儲位選擇 dialog_儲位選擇 = new Dialog_儲位選擇(this._藥碼, this._藥名, this._storageUI_EPD_266);
                dialog_儲位選擇.StartPosition = FormStartPosition.Manual;
                dialog_儲位選擇.Location = new Point((screen[0].Bounds.Width - dialog_儲位選擇.Width) / 2, this.Location.Y + 200);
                dialog_儲位選擇.ShowDialog();
                Storage storage = dialog_儲位選擇.Value;
                rJ_Lable_儲位資訊.Text = $"({storage.IP}) {storage.StorageName}";
                this.stepViewer1.Next();
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel($"請輸入小於[{storage.Inventory}]庫存值");
                dialog_NumPannel.StartPosition = FormStartPosition.Manual;
                dialog_NumPannel.Location = new Point((screen[0].Bounds.Width - dialog_NumPannel.Width) / 2, this.Location.Y + 200);
                dialog_NumPannel.X_Visible = false;
                while (true)
                {
                 
                    dialog_NumPannel.ShowDialog();
                    if(dialog_NumPannel.Value > storage.Inventory.StringToInt32())
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("輸入數值大於庫存", 1500);
                        dialog_AlarmForm.ShowDialog();
                    }
                    else
                    {
                        break;
                    }
                }
               
                rJ_Lable_數量.Text = $"數量 : {dialog_NumPannel.Value}";
                this.stepViewer1.Next();

                this.數量 = dialog_NumPannel.Value;
                this.storage = storage;

                this.plC_RJ_Button_確認完成.Visible = true;
            }
            if (_enum_type == enum_type.入庫)
            {
                Dialog_儲位選擇 dialog_儲位選擇 = new Dialog_儲位選擇(this._藥碼, this._藥名, this._storageUI_EPD_266);
                dialog_儲位選擇.StartPosition = FormStartPosition.Manual;
                dialog_儲位選擇.Location = new Point((screen[0].Bounds.Width - dialog_儲位選擇.Width) / 2, this.Location.Y + 200);
                dialog_儲位選擇.ShowDialog();
                Storage storage = dialog_儲位選擇.Value;
                rJ_Lable_儲位資訊.Text = $"({storage.IP}) {storage.StorageName}";
                this.stepViewer1.Next();


                Dialog_效期批號選擇 dialog_效期批號選擇 = new Dialog_效期批號選擇(_藥碼);
                dialog_效期批號選擇.StartPosition = FormStartPosition.Manual;
                dialog_效期批號選擇.Location = new Point((screen[0].Bounds.Width - dialog_效期批號選擇.Width) / 2, this.Location.Y + 200);
                dialog_效期批號選擇.ShowDialog();
                this.效期 = dialog_效期批號選擇.效期;
                this.批號 = dialog_效期批號選擇.批號;
                rJ_Lable_效期及批號.Text = $"效期 : {效期} 批號 : {(批號.StringIsEmpty() ? "無" : 批號)}";
                this.stepViewer1.Next();

                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                dialog_NumPannel.StartPosition = FormStartPosition.Manual;
                dialog_NumPannel.Location = new Point((screen[0].Bounds.Width - dialog_NumPannel.Width) / 2, this.Location.Y + 200);
                dialog_NumPannel.X_Visible = false;
                dialog_NumPannel.ShowDialog();
                rJ_Lable_數量.Text = $"數量 : {dialog_NumPannel.Value}";
                this.stepViewer1.Next();

                this.數量 = dialog_NumPannel.Value;
                this.storage = storage;

                this.plC_RJ_Button_確認完成.Visible = true;
            }
        }

        private void Dialog_入出庫作業_Load(object sender, EventArgs e)
        {
         
     
        }
        private void PlC_RJ_Button_確認完成_MouseDownEvent(MouseEventArgs mevent)
        {
            if (_enum_type == enum_type.入庫)
            {
                int 原有庫存 = Main_Form.Function_從SQL取得庫存(storage.Code);
                string url = $"{Main_Form.API_Server}/api/transactions/add";
                returnData returnData = new returnData();
                returnData.ServerName = "cheom";
                returnData.ServerType = "癌症備藥機";
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.動作 = enum_交易記錄查詢動作.入庫作業.GetEnumName();
                transactionsClass.藥品碼 = storage.Code;
                transactionsClass.藥品名稱 = storage.Name;
                transactionsClass.操作人 = this._操作人;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.庫存量 = 原有庫存.ToString(); ;
                transactionsClass.交易量 = 數量.ToString();
                transactionsClass.結存量 = (原有庫存 + 數量).ToString();
                transactionsClass.備註 += $"[效期]:{this.效期},[批號]:{this.批號}";
                returnData.Data = transactionsClass;
                string json_in = returnData.JsonSerializationt();
                string json_out = Basic.Net.WEBApiPostJson(url, json_in);

                storage = _storageUI_EPD_266.SQL_GetStorage(storage);
                storage.效期庫存異動(效期, 批號, 數量.ToString());
                _storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }
            if (_enum_type == enum_type.出庫)
            {
                int 原有庫存 = Main_Form.Function_從SQL取得庫存(storage.Code);
                storage = _storageUI_EPD_266.SQL_GetStorage(storage);   
                List<string> List_效期 = new List<string>();
                List<string> List_批號 = new List<string>();
                List<string> List_異動量 = new List<string>();
                storage.庫存異動((數量 * -1), out List_效期,out List_批號,out List_異動量);
                string url = $"{Main_Form.API_Server}/api/transactions/add";
                returnData returnData = new returnData();
                returnData.ServerName = "cheom";
                returnData.ServerType = "癌症備藥機";
                transactionsClass transactionsClass = new transactionsClass();
                transactionsClass.動作 = enum_交易記錄查詢動作.出庫作業.GetEnumName();
                transactionsClass.藥品碼 = storage.Code;
                transactionsClass.藥品名稱 = storage.Name;
                transactionsClass.操作人 = this._操作人;
                transactionsClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                transactionsClass.庫存量 = 原有庫存.ToString();
                transactionsClass.交易量 = (數量 * -1).ToString();
                transactionsClass.結存量 = (原有庫存 - 數量).ToString();
                string 備註 = "";
                for (int k = 0; k < List_效期.Count; k++)
                {
                    備註 += $"[效期]:{List_效期[k]},[批號]:{List_批號[k]}";
                    if (k != List_效期.Count - 1) 備註 += "\n";
                }
                transactionsClass.備註 = 備註;
                returnData.Data = transactionsClass;
                string json_in = returnData.JsonSerializationt();
                string json_out = Basic.Net.WEBApiPostJson(url, json_in);

              
                _storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }

            List<Storage> storages = _storageUI_EPD_266.SQL_GetAllStorage();

            List<Storage> storages_buf = (from temp in storages
                                          where temp.Code == storage.Code
                                          select temp).ToList();

            for (int i = 0; i < storages_buf.Count; i++)
            {
                _storageUI_EPD_266.Set_Stroage_LED_UDP(storages_buf[i], Color.Black);
            }
            this.Invoke(new Action(delegate
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("作業完成", 1500, Color.Green);
                dialog_AlarmForm.ShowDialog();
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }));
        }

        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Close();
                this.DialogResult = DialogResult.No;
            }));
        }
    }
}
