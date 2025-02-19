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

namespace 智能藥庫系統
{
    public partial class Dialog_驗收管理 : MyDialog
    {
        inspectionClass.creat current_creat = null;
        static public Dialog_驗收管理 myDialog;
        static public Dialog_驗收管理 GetForm()
        {
            if (myDialog != null)
            {
                return myDialog;
            }
            else
            {
                myDialog = new Dialog_驗收管理();
                return myDialog;
            }
        }

        public Dialog_驗收管理()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));
            this.ShowDialogEvent += Dialog_驗收管理_ShowDialogEvent;
            this.FormClosing += Dialog_驗收管理_FormClosing;
            this.LoadFinishedEvent += Dialog_驗收管理_LoadFinishedEvent;

            this.rJ_Button_驗收明細_新增.MouseDownEvent += RJ_Button_驗收明細_新增_MouseDownEvent;
            this.rJ_Button_驗收明細_刪除.MouseDownEvent += RJ_Button_驗收明細_刪除_MouseDownEvent;
            this.rJ_Button_驗收明細_修改.MouseDownEvent += RJ_Button_驗收明細_修改_MouseDownEvent;
            this.rJ_Button_驗收明細_輸入數量.MouseDownEvent += RJ_Button_驗收明細_輸入數量_MouseDownEvent;
            this.rJ_Button_驗收明細_確認入庫.MouseDownEvent += RJ_Button_驗收明細_確認入庫_MouseDownEvent;

            this.rJ_Button_驗收單_讀取.MouseDownEvent += RJ_Button_驗收單_讀取_MouseDownEvent;

            this.dateTimeIntervelPicker_報表日期.SureClick += DateTimeIntervelPicker_報表日期_SureClick;
            this.comboBox_驗收單號.SelectedIndexChanged += ComboBox_驗收單號_SelectedIndexChanged;
        }

  




        #region Event
        private void Dialog_驗收管理_LoadFinishedEvent(EventArgs e)
        {
            dateTimeIntervelPicker_報表日期.SetDateTime(DateTime.Now.GetStartDate(), DateTime.Now.GetEndDate());
            dateTimeIntervelPicker_報表日期.OnSureClick();

            List<Table> tables = inspectionClass.Init(Main_Form.API_Server);
            sqL_DataGridView_驗收品項.RowsHeight = 50;
            sqL_DataGridView_驗收品項.Init(tables.GetTable(new enum_驗收內容()));
            sqL_DataGridView_驗收品項.Set_ColumnVisible(false, new enum_驗收內容().GetEnumNames());
            sqL_DataGridView_驗收品項.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_驗收內容.料號);
            sqL_DataGridView_驗收品項.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_驗收內容.藥品碼);
            sqL_DataGridView_驗收品項.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_驗收內容.藥品名稱);
            sqL_DataGridView_驗收品項.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_驗收內容.應收數量);
            sqL_DataGridView_驗收品項.RowEnterEvent += SqL_DataGridView_驗收品項_RowEnterEvent;

            sqL_DataGridView_驗收品項.Set_ColumnText("藥碼", enum_驗收內容.藥品碼);
            sqL_DataGridView_驗收品項.Set_ColumnText("藥名", enum_驗收內容.藥品名稱);


            sqL_DataGridView_驗收明細.RowsHeight = 60;
            sqL_DataGridView_驗收明細.Init(tables.GetTable(new enum_驗收明細()));
            sqL_DataGridView_驗收明細.Set_ColumnVisible(false, new enum_驗收明細().GetEnumNames());
            sqL_DataGridView_驗收明細.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_驗收明細.實收數量);
            sqL_DataGridView_驗收明細.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_驗收明細.操作人);
            sqL_DataGridView_驗收明細.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_驗收明細.操作時間);
            sqL_DataGridView_驗收明細.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_驗收明細.效期);
            sqL_DataGridView_驗收明細.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_驗收明細.批號);
            sqL_DataGridView_驗收明細.RowEnterEvent += SqL_DataGridView_驗收明細_RowEnterEvent;

        }

  

        private void Dialog_驗收管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            myDialog = null;
        }
        private void Dialog_驗收管理_ShowDialogEvent()
        {
            if (myDialog != null)
            {
                form.Invoke(new Action(delegate
                {
                    myDialog.WindowState = FormWindowState.Normal;
                    myDialog.BringToFront();
                    this.DialogResult = DialogResult.Cancel;
                }));
            }
        }
        private void SqL_DataGridView_驗收明細_RowEnterEvent(object[] RowValue)
        {
           
        }
        private void SqL_DataGridView_驗收品項_RowEnterEvent(object[] RowValue)
        {
            string GUID = RowValue[(int)enum_驗收內容.GUID].ObjectToString();
            string 藥碼 = RowValue[(int)enum_驗收內容.藥品碼].ObjectToString();
            string 藥名 = RowValue[(int)enum_驗收內容.藥品名稱].ObjectToString();
            string 料號 = RowValue[(int)enum_驗收內容.料號].ObjectToString();
            string 驗收單號 = RowValue[(int)enum_驗收內容.驗收單號].ObjectToString();

            rJ_Lable_藥碼.Text = $"藥碼 : {藥碼}";
            rJ_Lable_藥名.Text = $"藥名 : {藥名}";
            rJ_Lable_料號.Text = $"料號 : {料號}";
            rJ_Lable_驗收單號.Text = $"驗收單號 : {驗收單號}";
            if (current_creat != null)
            {
                List<inspectionClass.sub_content> sub_Contents = current_creat.Get_SubContent_By_MasterGUID(GUID);
                List<object[]> list_sub_Contents = sub_Contents.ClassToSQL<inspectionClass.sub_content, enum_驗收明細>();
                sqL_DataGridView_驗收明細.RefreshGrid(list_sub_Contents);
            }

        }
        private void DateTimeIntervelPicker_報表日期_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClass.get_server(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, "API_inspection_refresh");
            if (sys_serverSettingClass != null)
            {
                if (sys_serverSettingClass.Server.StringIsEmpty() == false)
                {
                    string json = Basic.Net.WEBApiPostJson(sys_serverSettingClass.Server, new returnData().JsonSerializationt());
                }
            }
            List<inspectionClass.creat> creats = inspectionClass.creat_get_by_CT_TIME_ST_END(Main_Form.API_Server, dateTimeIntervelPicker_報表日期.StartTime, dateTimeIntervelPicker_報表日期.EndTime);

            List<string> vs = (from temp in creats
                               select temp.驗收單號).ToList();
            comboBox_驗收單號.DataSource = vs;
        }
        private void ComboBox_驗收單號_SelectedIndexChanged(object sender, EventArgs e)
        {
         

        }
        private void RJ_Button_驗收單_讀取_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                LoadingForm.ShowLoadingForm();

                sys_serverSettingClass sys_serverSettingClass = sys_serverSettingClass.get_server(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, "API_inspection_refresh");
                if (sys_serverSettingClass != null)
                {
                    if (sys_serverSettingClass.Server.StringIsEmpty() == false)
                    {
                        string json = Basic.Net.WEBApiPostJson(sys_serverSettingClass.Server, new returnData().JsonSerializationt());
                    }
                }
                string IC_SN = "";
                this.Invoke(new Action(delegate
                {
                    IC_SN = this.comboBox_驗收單號.Text;
                }));
                current_creat = inspectionClass.creat_get_by_IC_SN(Main_Form.API_Server, IC_SN);
                if (current_creat == null)
                {
                    MyMessageBox.ShowDialog("查無資料");
                    return;
                }
                List<object[]> list_contents = current_creat.Contents.ClassToSQL<inspectionClass.content, enum_驗收內容>();
                sqL_DataGridView_驗收品項.RefreshGrid(list_contents);
                sqL_DataGridView_驗收明細.ClearGrid();
            }
            catch
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
       
        }
        private void RJ_Button_驗收明細_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收品項 = sqL_DataGridView_驗收品項.Get_All_Select_RowsValues();
            if (list_驗收品項.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取驗收品項");
                return;
            }
            string Master_GUID = list_驗收品項[0][(int)enum_驗收內容.GUID].ObjectToString();
            string 藥品碼 = list_驗收品項[0][(int)enum_驗收內容.藥品碼].ObjectToString();
            string 藥品名稱 = list_驗收品項[0][(int)enum_驗收內容.藥品名稱].ObjectToString();
            string 料號 = list_驗收品項[0][(int)enum_驗收內容.料號].ObjectToString();
            inspectionClass.sub_content sub_Content = new inspectionClass.sub_content();

            Dialog_效期批號數量輸入 dialog_效期批號數量輸入 = new Dialog_效期批號數量輸入();
            if (dialog_效期批號數量輸入.ShowDialog() != DialogResult.Yes) return;

            sub_Content.Master_GUID = Master_GUID;
            sub_Content.藥品碼 = 藥品碼;
            sub_Content.藥品名稱 = 藥品名稱;
            sub_Content.料號 = 料號;
            sub_Content.效期 = dialog_效期批號數量輸入.效期;
            sub_Content.批號 = dialog_效期批號數量輸入.批號;
            sub_Content.實收數量 = dialog_效期批號數量輸入.數量.ToString();
            sub_Content.操作人 = Main_Form.登入者名稱;

            inspectionClass.content content = inspectionClass.sub_content_add(Main_Form.API_Server, sub_Content);
            List<object[]> list_驗收明細 = content.Sub_content.ClassToSQL<inspectionClass.sub_content, enum_驗收明細>();
            sqL_DataGridView_驗收明細.ClearGrid();
            sqL_DataGridView_驗收明細.AddRows(list_驗收明細, true);
        }
        private void RJ_Button_驗收明細_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收明細 = sqL_DataGridView_驗收明細.Get_All_Select_RowsValues();
            if (list_驗收明細.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取驗收品項");
                return;
            }

            if (MyMessageBox.ShowDialog("是否將選取資料刪除", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            inspectionClass.sub_content sub_Content = new inspectionClass.sub_content();
            sub_Content = list_驗收明細[0].SQLToClass<inspectionClass.sub_content , enum_驗收明細>();
            inspectionClass.content content = inspectionClass.sub_contents_delete_by_GUID(Main_Form.API_Server, sub_Content);
            list_驗收明細 = content.Sub_content.ClassToSQL<inspectionClass.sub_content, enum_驗收明細>();
            sqL_DataGridView_驗收明細.ClearGrid();
            sqL_DataGridView_驗收明細.AddRows(list_驗收明細, true);
        }
        private void RJ_Button_驗收明細_修改_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收明細 = sqL_DataGridView_驗收明細.Get_All_Select_RowsValues();
            if (list_驗收明細.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取驗收資料");
                return;
            }
            DateTime dateTime;
            string 效期 = list_驗收明細[0][(int)enum_驗收明細.效期].ObjectToString();
            string 批號 = list_驗收明細[0][(int)enum_驗收明細.批號].ObjectToString();
            int 實收數量 = list_驗收明細[0][(int)enum_驗收明細.實收數量].StringToInt32();
            dateTime = 效期.StringToDateTime();
            if (效期.Check_Date_String() == false) dateTime = DateTime.MinValue;
            if (實收數量 < 0) 實收數量 = 0;
            Dialog_效期批號數量輸入 dialog_效期批號數量輸入 = new Dialog_效期批號數量輸入(dateTime, 批號, 實收數量);
            if (dialog_效期批號數量輸入.ShowDialog() != DialogResult.Yes) return;

            list_驗收明細[0][(int)enum_驗收明細.效期] = dialog_效期批號數量輸入.效期;
            list_驗收明細[0][(int)enum_驗收明細.批號] = dialog_效期批號數量輸入.批號;
            list_驗收明細[0][(int)enum_驗收明細.實收數量] = dialog_效期批號數量輸入.數量;

            inspectionClass.sub_content sub_Content = list_驗收明細[0].SQLToClass<inspectionClass.sub_content, enum_驗收明細>();
            if (sub_Content != null)
            {
                inspectionClass.sub_content_update(Main_Form.API_Server, sub_Content);
            }
            sqL_DataGridView_驗收明細.ReplaceExtra(list_驗收明細[0], true);
        }
        private void RJ_Button_驗收明細_確認入庫_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收內容 = sqL_DataGridView_驗收品項.Get_All_Select_RowsValues();
            if (list_驗收內容.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取驗收資料");
                return;
            }
            inspectionClass.content content = list_驗收內容[0].SQLToClass<inspectionClass.content, enum_驗收內容>();
            string json = content.JsonSerializationt();

        }
        private void RJ_Button_驗收明細_輸入數量_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_驗收明細 = sqL_DataGridView_驗收明細.Get_All_Select_RowsValues();
            if (list_驗收明細.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取驗收資料");
                return;
            }
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            list_驗收明細[0][(int)enum_驗收明細.實收數量] = dialog_NumPannel.Value;
            inspectionClass.sub_content sub_Content = list_驗收明細[0].SQLToClass<inspectionClass.sub_content, enum_驗收明細>();
            if (sub_Content != null)
            {
                inspectionClass.sub_content_update(Main_Form.API_Server, sub_Content);
            }
            sqL_DataGridView_驗收明細.ReplaceExtra(list_驗收明細[0], true);
        }
        #endregion
    }
}
