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
    public partial class Dialog_調劑作業_調出 : MyDialog
    {


        public Dialog_調劑作業_調出()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();

                Table table_藥品資料 = medClass.init(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, medClass.StoreType.調劑台);
                sqL_DataGridView_庫儲藥品.Init(table_藥品資料);
                sqL_DataGridView_庫儲藥品.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
                sqL_DataGridView_庫儲藥品.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_藥品資料_藥檔資料.藥品碼);
                sqL_DataGridView_庫儲藥品.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_藥品資料_藥檔資料.藥品名稱);
                sqL_DataGridView_庫儲藥品.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_藥品資料_藥檔資料.庫存);
                sqL_DataGridView_庫儲藥品.RowDoubleClickEvent += SqL_DataGridView_庫儲藥品_RowDoubleClickEvent;

                Table table_已選藥品 = new Table(new enum_drugDispatch());
                sqL_DataGridView_已選藥品.Init(table_已選藥品);
                sqL_DataGridView_已選藥品.Set_ColumnVisible(false, new enum_drugDispatch().GetEnumNames());
                sqL_DataGridView_已選藥品.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.藥碼);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_drugDispatch.藥名);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.調出庫存);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.調出量);
                sqL_DataGridView_已選藥品.Set_ColumnWidth(90, DataGridViewContentAlignment.MiddleCenter, enum_drugDispatch.調入庫存);

                this.Load += Dialog_調劑作業_調出_Load;
                this.LoadFinishedEvent += Dialog_調劑作業_調出_LoadFinishedEvent;

                this.rJ_Button_庫儲藥品_搜尋.MouseDownEvent += RJ_Button_庫儲藥品_搜尋_MouseDownEvent;
                this.rJ_Button_選擇藥品.MouseDownEvent += RJ_Button_選擇藥品_MouseDownEvent;
                this.rJ_Button_已選藥品_刪除.MouseDownEvent += RJ_Button_已選藥品_刪除_MouseDownEvent;
                this.rJ_Button_已選藥品_送出.MouseDownEvent += RJ_Button_已選藥品_送出_MouseDownEvent;

                this.rJ_Button_返回.MouseDownEvent += RJ_Button_返回_MouseDownEvent;
            }));
                    
        }

 



        #region Event
        private void SqL_DataGridView_庫儲藥品_RowDoubleClickEvent(object[] RowValue)
        {
            RJ_Button_選擇藥品_MouseDownEvent(null);
        }
        private void Dialog_調劑作業_調出_LoadFinishedEvent(EventArgs e)
        {
            comboBox_目的調劑台名稱.SelectedIndex = 0;
            comboBox_庫儲藥品_搜尋條件.SelectedIndex = 0;
            comboBox_目的調劑台名稱.Refresh();
        }
        private void Dialog_調劑作業_調出_Load(object sender, EventArgs e)
        {
            rJ_Lable_來源調劑台名稱.Text = Main_Form.ServerName;
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.get_serversetting_by_type(Main_Form.API_Server, "調劑台");
            List<string> serverNames = (from temp in serverSettingClasses
                                        select temp.設備名稱).Distinct().ToList();
            serverNames.Remove(Main_Form.ServerName);

            comboBox_目的調劑台名稱.DataSource = serverNames;
        }
        private void RJ_Button_已選藥品_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_已選藥品 = sqL_DataGridView_已選藥品.Get_All_Select_RowsValues();
            if (list_已選藥品.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選擇藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_已選藥品[0][(int)enum_drugDispatch.藥碼].ObjectToString();
            string 藥名 = list_已選藥品[0][(int)enum_drugDispatch.藥名].ObjectToString();
            if (MyMessageBox.ShowDialog($"({藥碼}){藥名} 確刪除選擇藥品?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            sqL_DataGridView_已選藥品.DeleteExtra(list_已選藥品, true);
        }
        private void RJ_Button_選擇藥品_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_NumPannel dialog_NumPannel;
            string cmb_text = "";
            this.Invoke(new Action(delegate 
            {
                cmb_text = comboBox_目的調劑台名稱.Text;
            }));
            List<object[]> list_value = this.sqL_DataGridView_庫儲藥品.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選擇藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_value[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            string 調出庫存量 = list_value[0][(int)enum_藥品資料_藥檔資料.庫存].ObjectToString();
            string 調入庫存量 = "";
            string 調入庫別 = cmb_text;
            string 調出庫別 = Main_Form.ServerName;
            int 調出數量 = 0;
            List<medClass> medClasses = medClass.get_dps_medClass_by_code(Main_Form.API_Server, cmb_text, 藥碼);
        
            if (medClasses.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("調入儲位無此藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (medClasses[0].DeviceBasics.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("調出庫無此藥品儲位", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            調入庫存量 = medClasses[0].庫存;
            
            while(true)
            {
                dialog_NumPannel = new Dialog_NumPannel($"({medClasses[0].藥品碼}) {medClasses[0].藥品名稱}", $"可調出庫存 : {調出庫存量} , 調入庫存量 : {medClasses[0].庫存}");
                dialog_NumPannel.ShowDialog();
                if (dialog_NumPannel.Value > 調出庫存量.StringToInt32())
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("調出數量不足",1500);
                    dialog_AlarmForm.ShowDialog();
                    continue;
                }
                調出數量 = dialog_NumPannel.Value;
                break;
            }
            List<object[]> list_已選藥品 = sqL_DataGridView_已選藥品.GetAllRows();
            List<object[]> list_已選藥品_buf = list_已選藥品.GetRows((int)enum_drugDispatch.藥碼, medClasses[0].藥品碼);
            if(list_已選藥品_buf.Count == 0)
            {
                object[] value = new object[new enum_drugDispatch().GetLength()];
                value[(int)enum_drugDispatch.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_drugDispatch.藥碼] = medClasses[0].藥品碼;
                value[(int)enum_drugDispatch.藥名] = medClasses[0].藥品名稱;
                value[(int)enum_drugDispatch.單位] = medClasses[0].包裝單位;
                value[(int)enum_drugDispatch.調出庫存] = 調出庫存量;
                value[(int)enum_drugDispatch.調出量] = 調出數量;
                value[(int)enum_drugDispatch.調入庫存] = 調入庫存量;
                value[(int)enum_drugDispatch.調入庫別] = 調入庫別;
                value[(int)enum_drugDispatch.調出庫別] = 調出庫別;
                sqL_DataGridView_已選藥品.AddRow(value, true);
            }
            else
            {
                object[] value = list_已選藥品_buf[0];
                value[(int)enum_drugDispatch.調出量] = 調出數量;
                sqL_DataGridView_已選藥品.ReplaceExtra(value, true);
            }



   
        }
        private void RJ_Button_庫儲藥品_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<medClass> medClasses = new List<medClass>();
            string text = "";
            this.Invoke(new Action(delegate 
            {
                text = this.comboBox_庫儲藥品_搜尋條件.Text;
            }));

            if (text == "全部藥品")
            {
                medClasses = medClass.get_dps_medClass(Main_Form.API_Server, Main_Form.ServerName);
            }
            if (text == "藥碼")
            {
                medClasses = medClass.get_dps_medClass(Main_Form.API_Server, Main_Form.ServerName);

                medClasses = (from temp in medClasses
                              where temp.藥品碼.ToUpper().Contains(rJ_TextBox_庫儲藥品_搜尋.Texts.ToUpper())
                              select temp).ToList();
            }
            if (text == "藥名")
            {
                medClasses = medClass.get_dps_medClass(Main_Form.API_Server, Main_Form.ServerName);

                medClasses = (from temp in medClasses
                              where temp.藥品名稱.ToUpper().Contains(rJ_TextBox_庫儲藥品_搜尋.Texts.ToUpper())
                              select temp).ToList();
            }
            if (text == "中文名")
            {
                medClasses = medClass.get_dps_medClass(Main_Form.API_Server, Main_Form.ServerName);

                medClasses = (from temp in medClasses
                              where temp.中文名稱.ToUpper().Contains(rJ_TextBox_庫儲藥品_搜尋.Texts.ToUpper())
                              select temp).ToList();
            }
            if (medClasses.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            medClasses = (from temp in medClasses
                          where temp.庫存.StringToInt32() > 0
                          select temp).ToList();


            List<object[]> list_庫儲藥品 = medClasses.ClassToSQL<medClass, enum_藥品資料_藥檔資料>();
            sqL_DataGridView_庫儲藥品.RefreshGrid(list_庫儲藥品);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_已選藥品_送出_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_AlarmForm dialog_AlarmForm;
            List<object[]> list_value = sqL_DataGridView_已選藥品.GetAllRows();
            if (list_value.Count == 0)
            {
                dialog_AlarmForm = new Dialog_AlarmForm("未選擇藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            LoadingForm.ShowLoadingForm();

            List<drugDispatchClass> drugDispatchClasses = new List<drugDispatchClass>();

            for (int i = 0; i < list_value.Count; i++)
            {
                drugDispatchClass drugDispatchClass = new drugDispatchClass();
                drugDispatchClass.調出人員 = Main_Form.LoginUsers[0];
                drugDispatchClass.調入庫別 = list_value[i][(int)enum_drugDispatch.調入庫別].ObjectToString();
                drugDispatchClass.調出庫別 = list_value[i][(int)enum_drugDispatch.調出庫別].ObjectToString();
                drugDispatchClass.調出量 = list_value[i][(int)enum_drugDispatch.調出量].ObjectToString();
                drugDispatchClass.藥碼 = list_value[i][(int)enum_drugDispatch.藥碼].ObjectToString();
                drugDispatchClass.藥名 = list_value[i][(int)enum_drugDispatch.藥名].ObjectToString();
                drugDispatchClass.單位 = list_value[i][(int)enum_drugDispatch.單位].ObjectToString();
                drugDispatchClasses.Add(drugDispatchClass);
            }
            drugDispatchClass.add(Main_Form.API_Server, drugDispatchClasses);
            sqL_DataGridView_已選藥品.ClearGrid();
            LoadingForm.CloseLoadingForm();
            Voice.MediaPlayAsync($@"{Main_Form.currentDirectory}\送出成功.wav");
            dialog_AlarmForm = new Dialog_AlarmForm("送出成功", 1500, Color.Green);
            dialog_AlarmForm.ShowDialog();
            return;
        }
        private void RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }
      
        #endregion
    }
}
