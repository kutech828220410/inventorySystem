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
    public partial class Dialog_入出庫作業 : Form
    {
        public int 數量 = 0;
        public Storage storage;
        private StorageUI_EPD_266 _storageUI_EPD_266;
        private string _藥碼;
        private string _藥名;
        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }

        public Dialog_入出庫作業(string 藥碼 ,string 藥名, StorageUI_EPD_266 storageUI_EPD_266)
        {
            InitializeComponent();
            this.TopMost = true;
            Dialog_入出庫作業.form = this.ParentForm;
            this.Load += Dialog_入出庫作業_Load;
            this._藥碼 = 藥碼;
            this._藥名 = 藥名;
            this._storageUI_EPD_266 = storageUI_EPD_266;
            this.Shown += Dialog_入出庫作業_Shown;
        }

        private void Dialog_入出庫作業_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.Screen[] screen = System.Windows.Forms.Screen.AllScreens;

            Dialog_儲位選擇 dialog_儲位選擇 = new Dialog_儲位選擇(this._藥碼, this._藥名, this._storageUI_EPD_266);
            dialog_儲位選擇.Location = new Point((screen[0].Bounds.Width - dialog_儲位選擇.Width) / 2, (screen[0].Bounds.Height - dialog_儲位選擇.Height) / 2 + 150);
            dialog_儲位選擇.ShowDialog();
            Storage storage = dialog_儲位選擇.Value;
            rJ_Lable_儲位資訊.Text = $"({storage.IP}) {storage.StorageName}";
            this.stepViewer1.Next();

            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            dialog_NumPannel.X_Visible = false;
            dialog_NumPannel.ShowDialog();
            rJ_Lable_數量.Text = $"數量 : {dialog_NumPannel.Value}";
            this.stepViewer1.Next();

            this.數量 = dialog_NumPannel.Value;
            this.storage = storage;

            string url = $"{Main_Form.API_Server}/api/transactions/add";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            string json_in = returnData.JsonSerializationt();
            string json_out = Basic.Net.WEBApiPostJson(url, json_in);



        }

        private void Dialog_入出庫作業_Load(object sender, EventArgs e)
        {
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;

            List<StepEntity> list = new List<StepEntity>();
            list.Add(new StepEntity("1", "儲位選擇", 1, "請選擇入出庫位置", eumStepState.Completed, null));
            list.Add(new StepEntity("2", "輸入數量", 2, "填入入出庫數量", eumStepState.Waiting, null));
            list.Add(new StepEntity("3", "完成", 3, "確認交易", eumStepState.Waiting, null));
            this.stepViewer1.CurrentStep = 1;
            this.stepViewer1.ListDataSource = list;

            this.rJ_Lable_藥品資訊.Text = $"({this._藥碼 }) {this._藥名 }";
     
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
