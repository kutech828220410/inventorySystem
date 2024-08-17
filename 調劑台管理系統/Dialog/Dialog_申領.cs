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

namespace 調劑台管理系統
{
    public partial class Dialog_申領 : MyDialog
    {
        private MyThread myThread_program;

        public Dialog_申領()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
        
            this.Load += Dialog_申領_Load;
            this.FormClosing += Dialog_申領_FormClosing;
            this.LoadFinishedEvent += Dialog_申領_LoadFinishedEvent;
            this.rJ_Button_搜尋.MouseDownEvent += RJ_Button_搜尋_MouseDownEvent;
            this.rJ_Button_刪除.MouseDownEvent += RJ_Button_刪除_MouseDownEvent;
            this.plC_RJ_Button_確認送出.MouseDownEvent += PlC_RJ_Button_確認送出_MouseDownEvent;
            myThread_program = new MyThread();
            myThread_program.Add_Method(sub_program);
            myThread_program.AutoRun(true);
            myThread_program.AutoStop(true);
            myThread_program.Trigger();
        }


        #region Function
        private string Fucintion_取得申領類別()
        {
            if(rJ_RatioButton_一般申領.Checked)
            {
                return "一般申領";
            }
            return "緊急申領";
        }
        private void Function_新增申領藥品(medClass medClass , int 申領量)
        {
            List<object[]> list_value = this.sqL_DataGridView_申領.GetRows((int)enum_materialRequisition.藥碼, medClass.藥品碼, false);
            if(list_value.Count == 0)
            {
                materialRequisitionClass materialRequisitionClass = new materialRequisitionClass();
                materialRequisitionClass.GUID = Guid.NewGuid().ToString();
                materialRequisitionClass.申領單號 = $"{DateTime.Now.ToDateString()}-{Main_Form.ServerName}";
                materialRequisitionClass.藥碼 = medClass.藥品碼;
                materialRequisitionClass.料號 = medClass.料號;
                materialRequisitionClass.藥名 = medClass.藥品名稱;
                materialRequisitionClass.包裝單位 = medClass.包裝單位;
                materialRequisitionClass.包裝量 = medClass.包裝數量;
                materialRequisitionClass.申領類別 = Fucintion_取得申領類別();
                materialRequisitionClass.申領量 = 申領量.ToString();
                materialRequisitionClass.申領人員 = Main_Form.領藥台_01_登入者姓名;
                materialRequisitionClass.申領人員ID = Main_Form.領藥台_01_ID;
                materialRequisitionClass.申領單位 = Main_Form.ServerName;
                materialRequisitionClass.申領庫庫存 = Main_Form.Function_從SQL取得庫存(medClass.藥品碼).ToString();
                object[] value = materialRequisitionClass.ClassToSQL<materialRequisitionClass,enum_materialRequisition>();
                this.sqL_DataGridView_申領.AddRow(value, true);
            }
            else
            {
                materialRequisitionClass materialRequisitionClass = list_value[0].SQLToClass<materialRequisitionClass, enum_materialRequisition>();
                materialRequisitionClass.申領單號 = $"{DateTime.Now.ToDateString()}-{Main_Form.ServerName}";
                materialRequisitionClass.藥碼 = medClass.藥品碼;
                materialRequisitionClass.料號 = medClass.料號;
                materialRequisitionClass.藥名 = medClass.藥品名稱;
                materialRequisitionClass.包裝單位 = medClass.包裝單位;
                materialRequisitionClass.包裝量 = medClass.包裝數量;
                materialRequisitionClass.申領類別 = Fucintion_取得申領類別();
                materialRequisitionClass.申領量 = 申領量.ToString();
                materialRequisitionClass.申領人員 = Main_Form.領藥台_01_登入者姓名;
                materialRequisitionClass.申領人員ID = Main_Form.領藥台_01_ID;
                materialRequisitionClass.申領單位 = Main_Form.ServerName;
                materialRequisitionClass.申領庫庫存 = Main_Form.Function_從SQL取得庫存(medClass.藥品碼).ToString();
                object[] value = materialRequisitionClass.ClassToSQL<materialRequisitionClass, enum_materialRequisition>();
                this.sqL_DataGridView_申領.ReplaceExtra(value, true);
            }
            Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\sucess_01.wav");
        }
        private void sub_program()
        {
            try
            {
                string[] brcode_scanner_lines = Main_Form.Function_ReadBacodeScanner();
                for (int i = 0; i < brcode_scanner_lines.Length; i++)
                {
                    if (brcode_scanner_lines[i].StringIsEmpty() == false)
                    {
                        List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, brcode_scanner_lines[i]);
                        if (medClasses.Count == 0)
                        {
                           
                            Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                            dialog_AlarmForm.ShowDialog();
                            continue;
                        }

                        medClass _medClass = medClasses[0];
                        int 申領量 = 0;
                        if (Main_Form.PLC_Device_申領_不需輸入申領量.Bool == false)
                        {
                            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入申領量", _medClass.藥品名稱);
                            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
                            申領量 = dialog_NumPannel.Value;
                        }

                        Function_新增申領藥品(_medClass, 申領量);
         
                    }
                }
            }
            catch
            {

            }
        }
        #endregion
        #region Event
        private void RJ_Button_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_藥品搜尋 dialog_藥品搜尋 = new Dialog_藥品搜尋();
            if (dialog_藥品搜尋.ShowDialog() != DialogResult.Yes) return;
            medClass medClass = dialog_藥品搜尋.Value;
            int 申領量 = 0;
            if(Main_Form.PLC_Device_申領_不需輸入申領量.Bool == false)
            {
                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入申領量", medClass.藥品名稱);
                if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
                申領量 = dialog_NumPannel.Value;
            }

            Function_新增申領藥品(medClass, 申領量);
        }
        private void RJ_Button_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_申領.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料");
                return;
            }
            if (MyMessageBox.ShowDialog($"是否刪除選取資料共<{list_value.Count}>筆", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            this.sqL_DataGridView_申領.DeleteExtra(list_value, true);

        }
        private void PlC_RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_申領.GetAllRows();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未建立申領資料");
                return;
            }
            if (MyMessageBox.ShowDialog($"是否送出資料共<{list_value.Count}>筆", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            LoadingForm.ShowLoadingForm();
            List<materialRequisitionClass> materialRequisitionClasses = list_value.SQLToClass<materialRequisitionClass, enum_materialRequisition>();
            materialRequisitionClass.add(Main_Form.API_Server, materialRequisitionClasses);

            LoadingForm.CloseLoadingForm();
            this.Close();
        }
        private void Dialog_申領_Load(object sender, EventArgs e)
        {

        }
        private void Dialog_申領_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.myThread_program.Abort();
            this.myThread_program = null;
        }
        private void Dialog_申領_LoadFinishedEvent(EventArgs e)
        {
            Table table = materialRequisitionClass.init(Main_Form.API_Server);
            sqL_DataGridView_申領.RowsHeight = 50;
            sqL_DataGridView_申領.InitEx(table);
            sqL_DataGridView_申領.Set_ColumnVisible(false, new enum_materialRequisition().GetEnumNames());
            sqL_DataGridView_申領.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_materialRequisition.藥碼);
            sqL_DataGridView_申領.Set_ColumnWidth(700, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.藥名);
            sqL_DataGridView_申領.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_materialRequisition.申領類別);
            sqL_DataGridView_申領.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_materialRequisition.申領庫庫存);
            sqL_DataGridView_申領.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleRight, enum_materialRequisition.申領量);

            sqL_DataGridView_申領.Set_ColumnText("庫存", enum_materialRequisition.申領庫庫存);

        }
        #endregion
    }
}
