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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using MySQL_Login;
using H_Pannel_lib;
namespace 勤務傳送櫃
{
    public partial class Form1 : Form
    {
        public enum enum_人員資料
        {
            GUID,
            ID,
            姓名,
            性別,
            密碼,
            單位,
            權限等級,
            顏色,
            卡號,
            一維卡號,
            識別圖案,
            開門權限,
        }
        public enum enum_人員資料_匯出
        {
            ID,
            姓名,
            性別,
            密碼,
            單位,
            卡號,
            一維卡號,
        }
        public enum enum_人員資料_匯入
        {
            ID,
            姓名,
            性別,
            密碼,
            單位,
            卡號,
            一維卡號,
        }
        public enum ContextMenuStrip_人員資料
        {
            [Description("M8000")]
            匯出,
            [Description("M8000")]
            匯入,
            [Description("M8000")]
            匯出選取資料,
            [Description("M8000")]
            登錄資料,
            [Description("M8000")]
            刪除選取資料,
        }

        private List<PLC_Device> List_PLC_Device_權限管理 = new List<PLC_Device>();
        private List<LoginDataWebAPI.Class_login_data> List_class_Login_Data = new List<LoginDataWebAPI.Class_login_data>();
        private List<LoginDataWebAPI.Class_login_data_index> List_class_Login_Data_index = new List<LoginDataWebAPI.Class_login_data_index>();
        private List<OpenDoorPermission_UI> openDoorPermission_UIs = new List<OpenDoorPermission_UI>();
        private void Program_人員資料_Init()
        {

            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_人員資料, dBConfigClass.DB_person_page);

            this.loginUI.Set_login_data_DB(dBConfigClass.DB_person_page);
            this.loginUI.Set_login_data_index_DB(dBConfigClass.DB_person_page);
            this.loginUI.Init();

            this.sqL_DataGridView_人員資料.Init();
            if (!this.sqL_DataGridView_人員資料.SQL_IsTableCreat()) this.sqL_DataGridView_人員資料.SQL_CreateTable();
            this.sqL_DataGridView_人員資料.DataGridRefreshEvent += SqL_DataGridView_人員資料_DataGridRefreshEvent;
            this.sqL_DataGridView_人員資料.RowEnterEvent += SqL_DataGridView_人員資料_RowEnterEvent;
            this.sqL_DataGridView_人員資料.RowDoubleClickEvent += SqL_DataGridView_人員資料_RowDoubleClickEvent;
            this.sqL_DataGridView_人員資料.MouseDown += SqL_DataGridView_人員資料_MouseDown;
            this.sqL_DataGridView_人員資料.SQL_GetAllRows(true);

            this.plC_Button_權限設定_設定至Server.MouseDownEvent += PlC_Button_權限設定_設定至Server_MouseDownEvent;
            this.plC_RJ_ComboBox_權限管理_權限等級.OnSelectedIndexChanged += PlC_RJ_ComboBox_權限管理_權限等級_OnSelectedIndexChanged;
            this.plC_RJ_ComboBox_權限管理_權限等級.Items.Clear();
            int level_num = (int)this.loginUI.Level_num;
            for (int i = 1; i <= level_num; i++)
            {
                this.plC_RJ_ComboBox_權限管理_權限等級.Items.Add(i.ToString("00"));
            }
            for (int i = 0; i < 256; i++) this.List_PLC_Device_權限管理.Add(new PLC_Device($"S{39000 + i}"));


            this.plC_RJ_Button_人員資料_匯出.MouseDownEvent += PlC_RJ_Button_人員資料_匯出_MouseDownEvent;
            this.plC_RJ_Button_人員資料_匯入.MouseDownEvent += PlC_RJ_Button_人員資料_匯入_MouseDownEvent;
            this.plC_RJ_Button_人員資料_登錄.MouseDownEvent += PlC_RJ_Button_人員資料_登錄_MouseDownEvent;
            this.plC_RJ_Button_人員資料_刪除.MouseDownEvent += PlC_RJ_Button_人員資料_刪除_MouseDownEvent;
            this.plC_RJ_Button_人員資料_清除內容.MouseDownEvent += PlC_RJ_Button_人員資料_清除內容_MouseDownEvent;
            this.plC_RJ_Button_人員資料_開門權限全開.MouseDownEvent += PlC_RJ_Button_人員資料_開門權限全開_MouseDownEvent;
            this.plC_RJ_Button_人員資料_開門權限全關.MouseDownEvent += PlC_RJ_Button_人員資料_開門權限全關_MouseDownEvent;

            this.Function_人員資料_開門權限_初始化();

            this.plC_UI_Init.Add_Method(this.Program_人員資料);
        }

 

        bool flag_人員資料_頁面更新 = false;
        private void Program_人員資料()
        {
            if (this.plC_ScreenPage_Main.PageText == "人員資料")
            {
                if (!this.flag_人員資料_頁面更新)
                {
                    this.List_class_Login_Data = this.loginUI.Get_login_data();
                    this.List_class_Login_Data_index = this.loginUI.Get_login_data_index();
                    this.plC_RJ_ComboBox_權限管理_權限等級.SetValue(0);
                    this.loginIndex_Pannel.Set_Login_Data_Index(this.List_class_Login_Data_index);
                    this.loginIndex_Pannel.Set_Login_Data(this.List_class_Login_Data[0]);

                    this.sqL_DataGridView_人員資料.SQL_GetAllRows(true);

                    string IP = "";
                    int RFID_Num = -1;
                    List<object[]> list_Box_Index_Table = this.sqL_DataGridView_Box_Index_Table.SQL_GetAllRows(false);
                    List<object[]> list_Box_Index_Table_buf = new List<object[]>();
                    for (int i = 0; i < openDoorPermission_UIs.Count; i++)
                    {
                        list_Box_Index_Table_buf = list_Box_Index_Table.GetRows((int)enum_Box_Index_Table.Number, i.ToString());
                        if (list_Box_Index_Table_buf.Count == 0) continue;
                        IP = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.IP].ObjectToString();
                        RFID_Num = list_Box_Index_Table_buf[0][(int)enum_Box_Index_Table.RFID_num].ObjectToString().StringToInt32();
                        if (RFID_Num < 0 || RFID_Num >= 5) continue;
                        Pannel_Box pannel_Box = this.List_Pannel_Box.SortByRFID(IP, RFID_Num);
                        if (pannel_Box == null) continue;
                        this.Invoke(new Action(delegate
                        {
                            openDoorPermission_UIs[i].WardName = pannel_Box.WardName;
                            openDoorPermission_UIs[i].Visible = (list_Box_Index_Table_buf.Count > 0);
                        }));
               
                    }


                    this.flag_人員資料_頁面更新 = true;
                }
            }
            else
            {
                this.flag_人員資料_頁面更新 = false;
            }
            this.Program_人員資料_讀取RFID();
        }
        #region PLC_人員資料_讀取RFID
        PLC_Device PLC_Device_人員資料_讀取RFID = new PLC_Device("");
        int cnt_Program_人員資料_讀取RFID = 65534;
        void Program_人員資料_讀取RFID()
        {
            if (this.plC_ScreenPage_Main.PageText == "人員資料")
            {
                PLC_Device_人員資料_讀取RFID.Bool = true;
            }
            else
            {
                PLC_Device_人員資料_讀取RFID.Bool = false;
            }
            if (cnt_Program_人員資料_讀取RFID == 65534)
            {
                PLC_Device_人員資料_讀取RFID.Bool = false;
                cnt_Program_人員資料_讀取RFID = 65535;
            }
            if (cnt_Program_人員資料_讀取RFID == 65535) cnt_Program_人員資料_讀取RFID = 1;
            if (cnt_Program_人員資料_讀取RFID == 1) cnt_Program_人員資料_讀取RFID_檢查按下(ref cnt_Program_人員資料_讀取RFID);
            if (cnt_Program_人員資料_讀取RFID == 2) cnt_Program_人員資料_讀取RFID_初始化(ref cnt_Program_人員資料_讀取RFID);
            if (cnt_Program_人員資料_讀取RFID == 3) cnt_Program_人員資料_讀取RFID = 65500;
            if (cnt_Program_人員資料_讀取RFID > 1) cnt_Program_人員資料_讀取RFID_檢查放開(ref cnt_Program_人員資料_讀取RFID);

            if (cnt_Program_人員資料_讀取RFID == 65500)
            {
                PLC_Device_人員資料_讀取RFID.Bool = false;
                cnt_Program_人員資料_讀取RFID = 65535;
            }
        }
        void cnt_Program_人員資料_讀取RFID_檢查按下(ref int cnt)
        {
            if ((PLC_Device_人員資料_讀取RFID.Bool == true)) cnt++;
        }
        void cnt_Program_人員資料_讀取RFID_檢查放開(ref int cnt)
        {
            if (!(PLC_Device_人員資料_讀取RFID.Bool == false)) cnt = 65500;
        }
        void cnt_Program_人員資料_讀取RFID_初始化(ref int cnt)
        {
            List<RFID_FX600lib.RFID_FX600_UI.RFID_Device> list_RFID_Devices = this.rfiD_FX600_UI.Get_RFID();
            if (list_RFID_Devices.Count > 0)
            {
                this.Invoke(new Action(delegate
                {
                    this.rJ_TextBox_人員資料_卡號.Text = list_RFID_Devices[0].UID;
                }));
                cnt++;
                return;

            }
            //List<RFID_UI.RFID_UID_Class> list_RFID_UID_Class = this.rfiD_UI.GetRFID();
            //if(list_RFID_UID_Class.Count > 0)
            //{
            //    this.Invoke(new Action(delegate
            //    {
            //        this.rJ_TextBox_人員資料_卡號.Text = list_RFID_UID_Class[0].UID;
            //    }));
            //    cnt++;
            //    return;
            //}
            cnt++;
        }


















        #endregion

        #region Function
        private string Function_人員資料_檢查內容(object[] value)
        {
            string str_error = "";
            List<string> list_error = new List<string>();
            if (value[(int)enum_人員資料.姓名].ObjectToString().StringIsEmpty())
            {
                list_error.Add("'姓名'欄位不得空白!");
            }
            if (value[(int)enum_人員資料.ID].ObjectToString().StringIsEmpty())
            {
                list_error.Add("'ID'欄位不得空白!");
            }
            for (int i = 0; i < list_error.Count; i++)
            {
                str_error += $"{(i + 1).ToString("00")}. {list_error[i]}";
                if (i != list_error.Count - 1) str_error += "\n";
            }
            return str_error;
        }
        private void Function_人員資料_清除內容()
        {
            this.Invoke(new Action(delegate
            {
                this.rJ_TextBox_人員資料_ID.Text = "";
                this.rJ_TextBox_人員資料_姓名.Text = "";
                this.rJ_TextBox_人員資料_密碼.Text = "";
                this.rJ_TextBox_人員資料_單位.Text = "";
                this.rJ_TextBox_人員資料_卡號.Text = "";
                this.textBox_人員資料_顏色.Text = colorDialog.Color.ToColorString();
                this.comboBox_人員資料_權限等級.Text = "";
                this.rJ_TextBox_人員資料_一維條碼.Text = "";
                this.rJ_TextBox_人員資料_識別圖案.Text = "";
            }));

        }
        private void Function_人員資料_登錄資料()
        {
            string 性別 = rJ_RatioButton_人員資料_男.Checked ? "男" : "女";
            List<object[]> list_value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
            List<object[]> list_value_buf = new List<object[]>();
            list_value_buf = list_value.GetRows((int)enum_人員資料.ID, rJ_TextBox_人員資料_ID.Text);
            object[] value = new object[new enum_人員資料().GetLength()];
            if (list_value_buf.Count == 0)
            {
                value[(int)enum_人員資料.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_人員資料.ID] = this.rJ_TextBox_人員資料_ID.Text;
                value[(int)enum_人員資料.姓名] = this.rJ_TextBox_人員資料_姓名.Text;
                value[(int)enum_人員資料.性別] = 性別;
                value[(int)enum_人員資料.密碼] = this.rJ_TextBox_人員資料_密碼.Text;
                value[(int)enum_人員資料.單位] = this.rJ_TextBox_人員資料_單位.Text;
                value[(int)enum_人員資料.卡號] = this.rJ_TextBox_人員資料_卡號.Text;
                value[(int)enum_人員資料.權限等級] = this.comboBox_人員資料_權限等級.Text;
                value[(int)enum_人員資料.顏色] = this.textBox_人員資料_顏色.Text;
                value[(int)enum_人員資料.一維卡號] = this.rJ_TextBox_人員資料_一維條碼.Text;
                value[(int)enum_人員資料.識別圖案] = this.rJ_TextBox_人員資料_識別圖案.Text;
                value[(int)enum_人員資料.開門權限] = this.openDoorPermission_UIs.GetOpenDoorPermission();
                string str_error = this.Function_人員資料_檢查內容(value);
                if (!str_error.StringIsEmpty())
                {
                    MyMessageBox.ShowDialog(str_error);
                    return;
                }
                this.sqL_DataGridView_人員資料.SQL_AddRow(value, true);
            }
            else
            {
                if (MyMessageBox.ShowDialog("此ID已註冊,是否覆寫?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    value = list_value_buf[0];
                    value[(int)enum_人員資料.ID] = this.rJ_TextBox_人員資料_ID.Text;
                    value[(int)enum_人員資料.姓名] = this.rJ_TextBox_人員資料_姓名.Text;
                    value[(int)enum_人員資料.性別] = 性別;
                    value[(int)enum_人員資料.密碼] = this.rJ_TextBox_人員資料_密碼.Text;
                    value[(int)enum_人員資料.單位] = this.rJ_TextBox_人員資料_單位.Text;
                    value[(int)enum_人員資料.卡號] = this.rJ_TextBox_人員資料_卡號.Text;
                    value[(int)enum_人員資料.權限等級] = this.comboBox_人員資料_權限等級.Text;
                    value[(int)enum_人員資料.顏色] = this.textBox_人員資料_顏色.Text;
                    value[(int)enum_人員資料.一維卡號] = this.rJ_TextBox_人員資料_一維條碼.Text;
                    value[(int)enum_人員資料.識別圖案] = this.rJ_TextBox_人員資料_識別圖案.Text;
                    value[(int)enum_人員資料.開門權限] = this.openDoorPermission_UIs.GetOpenDoorPermission();
                    string str_error = this.Function_人員資料_檢查內容(value);
                    if (!str_error.StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog(str_error);
                        return;
                    }
                    this.sqL_DataGridView_人員資料.SQL_ReplaceExtra(value, true);
                }
            }

            Function_人員資料_清除內容();
        }
        private void Function_人員資料_匯出()
        {
            saveFileDialog_SaveExcel.OverwritePrompt = false;
            if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
            {
                DataTable datatable = new DataTable();
                datatable = sqL_DataGridView_人員資料.GetDataTable();
                datatable = datatable.ReorderTable(new enum_人員資料_匯出());
                CSVHelper.SaveFile(datatable, saveFileDialog_SaveExcel.FileName);
                MyMessageBox.ShowDialog("匯出完成!");
            }
        }
        private void Function_人員資料_匯入()
        {
            if (openFileDialog_LoadExcel.ShowDialog(this) == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dataTable = new DataTable();
                CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                DataTable datatable_buf = dataTable.ReorderTable(new enum_人員資料_匯入());
                if (datatable_buf == null)
                {
                    MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                    return;
                }
                List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();
                List<object[]> list_SQL_Value = this.sqL_DataGridView_人員資料.SQL_GetAllRows(false);
                List<object[]> list_Add = new List<object[]>();
                List<object[]> list_Delete_ColumnName = new List<object[]>();
                List<object[]> list_Delete_SerchValue = new List<object[]>();
                List<string> list_Replace_SerchValue = new List<string>();
                List<object[]> list_Replace_Value = new List<object[]>();
                List<object[]> list_SQL_Value_buf = new List<object[]>();

                for (int i = 0; i < list_LoadValue.Count; i++)
                {
                    object[] value_load = list_LoadValue[i];
                    value_load = value_load.CopyRow(new enum_人員資料_匯入(), new enum_人員資料());
                    if (!Function_人員資料_檢查內容(value_load).StringIsEmpty()) continue;

                    string 性別 = value_load[(int)enum_人員資料.性別].ObjectToString();
                    string 權限等級 = value_load[(int)enum_人員資料.權限等級].ObjectToString();
                    if (!(性別 == "男" || 性別 == "女")) 性別 = "男";
                    if (權限等級.StringToInt32() <= 0 || 權限等級.StringToInt32() > 20) 權限等級 = "01";
                    value_load[(int)enum_人員資料.性別] = 性別;
                    value_load[(int)enum_人員資料.權限等級] = 權限等級;

                    list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_人員資料.ID, value_load[(int)enum_人員資料.ID].ObjectToString());
                    if (list_SQL_Value_buf.Count > 0)
                    {
                        object[] value_SQL = list_SQL_Value_buf[0];
                        value_load[(int)enum_人員資料.GUID] = value_SQL[(int)enum_人員資料.GUID];
                        bool flag_Equal = value_load.IsEqual(value_SQL);
                        if (!flag_Equal)
                        {
                            list_Replace_SerchValue.Add(value_load[(int)enum_人員資料.GUID].ObjectToString());
                            list_Replace_Value.Add(value_load);
                        }
                    }
                    else
                    {
                        value_load[(int)enum_人員資料.GUID] = Guid.NewGuid().ToString();
                        list_Add.Add(value_load);
                    }
                }
                this.sqL_DataGridView_人員資料.SQL_AddRows(list_Add, false);
                this.sqL_DataGridView_人員資料.SQL_ReplaceExtra(enum_人員資料.GUID.GetEnumName(), list_Replace_SerchValue, list_Replace_Value, false);
                this.sqL_DataGridView_人員資料.SQL_GetAllRows(true);
                this.Cursor = Cursors.Default;
                MyMessageBox.ShowDialog("匯入完成!");
            }
            this.Cursor = Cursors.Default;

        }

        private void Function_登入權限資料_取得權限(int level)
        {
            LoginDataWebAPI.Class_login_data class_Login_Data = this.loginUI.Get_login_data(level);
            if (class_Login_Data != null)
            {
                for (int i = 0; i < class_Login_Data.data.Count; i++)
                {
                    this.List_PLC_Device_權限管理[i].Bool = class_Login_Data.data[i];
                }
            }
        }
        private void Function_登入權限資料_最高權限()
        {
            for (int i = 0; i < 256; i++)
            {
                this.List_PLC_Device_權限管理[i].Bool = true;
            }
            PLC_Device_最高權限.Bool = true;
        }
        private void Function_登入權限資料_清除權限()
        {
            for (int i = 0; i < 256; i++)
            {
                this.List_PLC_Device_權限管理[i].Bool = false;
            }
            PLC_Device_最高權限.Bool = false;
        }


        private void Function_人員資料_開門權限_初始化()
        {
            this.SuspendLayout();
  
            List<FlowLayoutPanel> flowLayoutPanels = new List<FlowLayoutPanel>();
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_01);
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_02);
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_03);
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_04);
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_05);
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_06);
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_07);
            flowLayoutPanels.Add(flowLayoutPanel_開門權限_08);

            for(int i = 0; i < 160; i++)
            {
                OpenDoorPermission_UI openDoorPermission_UI = new OpenDoorPermission_UI();
                flowLayoutPanels[i / 20].Controls.Add(openDoorPermission_UI);
                openDoorPermission_UI.Index = (i);
                openDoorPermission_UI.Size = new Size(260, 55);
                openDoorPermission_UI.TabIndex = i + 5;
                openDoorPermission_UI.Visible = false;
                this.openDoorPermission_UIs.Add(openDoorPermission_UI);
            }
            this.ResumeLayout(false);
        }

        private bool Function_人員資料_取得開門權限(string value, int num)
        {
            if (num < 0) return false;
            byte[] bytes = value.StringHexTobytes();
            int temp0 = num / 8;
            int temp1 = num % 8;
            if (temp0 >= bytes.Length) return false;
            return myConvert.ByteGetBit(bytes[temp0], temp1);
        }
        #endregion
        #region Event
        private void SqL_DataGridView_人員資料_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_人員資料());
                if (dialog_ContextMenuStrip.ShowDialog() == DialogResult.Yes)
                {
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_人員資料.匯入.GetEnumName())
                    {
                        Function_人員資料_匯入();
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_人員資料.匯出.GetEnumName())
                    {
                        Function_人員資料_匯出();
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_人員資料.匯出選取資料.GetEnumName())
                    {
                        saveFileDialog_SaveExcel.OverwritePrompt = false;
                        if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
                        {
                            DataTable datatable = new DataTable();
                            datatable = sqL_DataGridView_人員資料.GetSelectRowsDataTable();
                            datatable = datatable.ReorderTable(new enum_人員資料_匯出());
                            CSVHelper.SaveFile(datatable, saveFileDialog_SaveExcel.FileName);
                            MyMessageBox.ShowDialog("匯出完成!");
                        }
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_人員資料.刪除選取資料.GetEnumName())
                    {
                        DialogResult Result = MyMessageBox.ShowDialog("是否刪除選取欄位資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel);
                        if (Result == System.Windows.Forms.DialogResult.Yes)
                        {
                            List<object[]> list_value = this.sqL_DataGridView_人員資料.Get_All_Select_RowsValues();
                            this.sqL_DataGridView_人員資料.SQL_DeleteExtra(list_value, true);
                        }
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_人員資料.登錄資料.GetEnumName())
                    {
                        Function_人員資料_登錄資料();
                    }
                }
            }
        }
        private void button_人員資料_顏色選擇_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_人員資料_顏色.Text = this.colorDialog.Color.ToColorString();
                textBox_人員資料_顏色.BackColor = textBox_人員資料_顏色.Text.ToColor();
            }
        }
        private void SqL_DataGridView_人員資料_DataGridRefreshEvent()
        {
            for (int i = 0; i < this.sqL_DataGridView_人員資料.dataGridView.Rows.Count; i++)
            {
                Color color = this.sqL_DataGridView_人員資料.dataGridView.Rows[i].Cells[(int)enum_人員資料.顏色].Value.ObjectToString().ToColor();
                this.sqL_DataGridView_人員資料.dataGridView.Rows[i].Cells[(int)enum_人員資料.顏色].Style.BackColor = color;
                this.sqL_DataGridView_人員資料.dataGridView.Rows[i].Cells[(int)enum_人員資料.顏色].Style.ForeColor = color;
            }
        }
        private void SqL_DataGridView_人員資料_RowEnterEvent(object[] RowValue)
        {
            //rJ_TextBox_人員資料_ID.Text = RowValue[(int)enum_人員資料.ID].ObjectToString();
            //rJ_TextBox_人員資料_姓名.Text = RowValue[(int)enum_人員資料.姓名].ObjectToString();
            //rJ_TextBox_人員資料_密碼.Text = RowValue[(int)enum_人員資料.密碼].ObjectToString();
            //rJ_TextBox_人員資料_單位.Text = RowValue[(int)enum_人員資料.單位].ObjectToString();
            //comboBox_人員資料_權限等級.Text = RowValue[(int)enum_人員資料.權限等級].ObjectToString();
            //textBox_人員資料_顏色.Text = RowValue[(int)enum_人員資料.顏色].ObjectToString();
            //textBox_人員資料_顏色.BackColor = textBox_人員資料_顏色.Text.ToColor();
            //rJ_TextBox_人員資料_卡號.Text = RowValue[(int)enum_人員資料.卡號].ObjectToString();
            //rJ_TextBox_人員資料_一維條碼.Text = RowValue[(int)enum_人員資料.一維卡號].ObjectToString();
            //rJ_TextBox_人員資料_識別圖案.Text = RowValue[(int)enum_人員資料.識別圖案].ObjectToString();


            //string 性別 = RowValue[(int)enum_人員資料.性別].ObjectToString();
            //if (性別 == "男") rJ_RatioButton_人員資料_男.Checked = true;
            //else rJ_RatioButton_人員資料_女.Checked = true;
        }
        private void SqL_DataGridView_人員資料_RowDoubleClickEvent(object[] RowValue)
        {
            rJ_TextBox_人員資料_ID.Text = RowValue[(int)enum_人員資料.ID].ObjectToString();
            rJ_TextBox_人員資料_姓名.Text = RowValue[(int)enum_人員資料.姓名].ObjectToString();
            rJ_TextBox_人員資料_密碼.Text = RowValue[(int)enum_人員資料.密碼].ObjectToString();
            rJ_TextBox_人員資料_單位.Text = RowValue[(int)enum_人員資料.單位].ObjectToString();
            comboBox_人員資料_權限等級.Text = RowValue[(int)enum_人員資料.權限等級].ObjectToString();
            textBox_人員資料_顏色.Text = RowValue[(int)enum_人員資料.顏色].ObjectToString();
            textBox_人員資料_顏色.BackColor = textBox_人員資料_顏色.Text.ToColor();
            rJ_TextBox_人員資料_卡號.Text = RowValue[(int)enum_人員資料.卡號].ObjectToString();
            rJ_TextBox_人員資料_一維條碼.Text = RowValue[(int)enum_人員資料.一維卡號].ObjectToString();
            rJ_TextBox_人員資料_識別圖案.Text = RowValue[(int)enum_人員資料.識別圖案].ObjectToString();
            this.openDoorPermission_UIs.SetOpenDoorPermission(RowValue[(int)enum_人員資料.開門權限].ObjectToString());

            string 性別 = RowValue[(int)enum_人員資料.性別].ObjectToString();
            if (性別 == "男") rJ_RatioButton_人員資料_男.Checked = true;
            else rJ_RatioButton_人員資料_女.Checked = true;
        }

        private void PlC_RJ_ComboBox_權限管理_權限等級_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int level = plC_RJ_ComboBox_權限管理_權限等級.Text.StringToInt32();
            if (level > 0)
            {
                List<LoginDataWebAPI.Class_login_data> List_class_Login_Data_buf = new List<LoginDataWebAPI.Class_login_data>();
                List_class_Login_Data_buf = (from value in List_class_Login_Data
                                             where value.level.StringToInt32() == level
                                             select value).ToList();
                if (List_class_Login_Data_buf.Count > 0)
                {
                    this.loginIndex_Pannel.Set_Login_Data(List_class_Login_Data_buf[0]);
                }
            }
        }
        private void PlC_Button_權限設定_設定至Server_MouseDownEvent(MouseEventArgs mevent)
        {
            for (int i = 0; i < List_class_Login_Data.Count; i++)
            {
                this.loginUI.Set_login_data(List_class_Login_Data[i]);
            }
            this.Invoke(new Action(delegate
            {
                MyMessageBox.ShowDialog("權限更動完成!");
            }));
     
        }
        private void PlC_RJ_Button_人員資料_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                DialogResult Result = MyMessageBox.ShowDialog("是否刪除選取欄位資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel);
                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    List<object[]> list_value = this.sqL_DataGridView_人員資料.Get_All_Select_RowsValues();
                    this.sqL_DataGridView_人員資料.SQL_DeleteExtra(list_value, true);
                }
            }));
        }
        private void PlC_RJ_Button_人員資料_登錄_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Function_人員資料_登錄資料();
            }));
        }
        private void PlC_RJ_Button_人員資料_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Function_人員資料_匯入();
            }));
        }
        private void PlC_RJ_Button_人員資料_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Function_人員資料_匯出();
            }));
        }
        private void PlC_RJ_Button_人員資料_清除內容_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Function_人員資料_清除內容();
        }
        private void PlC_RJ_Button_人員資料_開門權限全關_MouseDownEvent(MouseEventArgs mevent)
        {
            string value = "";
            byte[] bytes = new byte[openDoorPermission_UIs.Count / 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    bytes[i] |= (byte)(0 << k);
                }
            }
            value = $"{bytes.ByteToStringHex()}";

            this.openDoorPermission_UIs.SetOpenDoorPermission(value);
        }
        private void PlC_RJ_Button_人員資料_開門權限全開_MouseDownEvent(MouseEventArgs mevent)
        {
            string value = "";
            byte[] bytes = new byte[openDoorPermission_UIs.Count / 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    bytes[i] |= (byte)(1 << k);
                }
            }
            value = $"{bytes.ByteToStringHex()}";

            this.openDoorPermission_UIs.SetOpenDoorPermission(value);
        }
        #endregion
    }
}
