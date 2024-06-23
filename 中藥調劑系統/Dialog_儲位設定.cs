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
using MyOffice;

namespace 中藥調劑系統
{
    public enum enum_儲位總表
    {
        [Description("GUID,VARCHAR,15,NONE")]
        GUID,
        [Description("IP,VARCHAR,15,NONE")]
        IP,
        [Description("名稱,VARCHAR,15,NONE")]
        名稱,
    }
    public enum enum_儲位列表
    {
        [Description("GUID,VARCHAR,15,NONE")]
        GUID,
        [Description("編號,VARCHAR,15,NONE")]
        編號,
        [Description("儲位名稱,VARCHAR,15,NONE")]
        儲位名稱,
        [Description("藥碼,VARCHAR,15,NONE")]
        藥碼,
        [Description("藥名,VARCHAR,15,NONE")]
        藥名,
        [Description("庫存,VARCHAR,15,NONE")]
        庫存,
    }
    public enum enum_效期及批號
    {
        [Description("效期,VARCHAR,15,NONE")]
        效期,
        [Description("批號,VARCHAR,15,NONE")]
        批號,
        [Description("庫存,VARCHAR,15,NONE")]
        庫存,
    }
    public partial class Dialog_儲位設定 : MyDialog
    {
        public Dialog_儲位設定()
        {
            InitializeComponent();
            this.Load += Dialog_儲位設定_Load;
            this.Shown += Dialog_儲位設定_Shown;
        }

        private void Dialog_儲位設定_Shown(object sender, EventArgs e)
        {
            comboBox_藥品資料_搜尋條件.SelectedIndex = 0;
            this.Refresh();
        }
        private void Dialog_儲位設定_Load(object sender, EventArgs e)
        {
            this.rowsLED_Pannel.Init(Main_Form._rowsLEDUI.List_UDP_Local);
            this.rowsLED_Pannel.AutoWrite = true;
            Table table_藥品資料 = medClass.init(Main_Form.API_Server);
            this.sqL_DataGridView_RowsLED_藥品資料.RowsHeight = 40;
            this.sqL_DataGridView_RowsLED_藥品資料.Init(table_藥品資料);
            this.sqL_DataGridView_RowsLED_藥品資料.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
            this.sqL_DataGridView_RowsLED_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_RowsLED_藥品資料.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_RowsLED_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_RowsLED_藥品資料.Set_ColumnText("藥碼", enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_RowsLED_藥品資料.Set_ColumnText("藥名", enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_RowsLED_藥品資料.Set_ColumnText("單位", enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_RowsLED_藥品資料.RowDoubleClickEvent += SqL_DataGridView_RowsLED_藥品資料_RowDoubleClickEvent;


            Table table_儲位總表 = new Table(new enum_儲位總表());
            this.sqL_DataGridView_RowsLED_層架列表.RowsHeight = 40;
            this.sqL_DataGridView_RowsLED_層架列表.Init(table_儲位總表);
            this.sqL_DataGridView_RowsLED_層架列表.Set_ColumnVisible(false, new enum_儲位總表().GetEnumNames());
            this.sqL_DataGridView_RowsLED_層架列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_儲位總表.IP);
            this.sqL_DataGridView_RowsLED_層架列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleCenter, enum_儲位總表.名稱);
            this.sqL_DataGridView_RowsLED_層架列表.RowEnterEvent += SqL_DataGridView_RowsLED_層架列表_RowEnterEvent;

            Table table_儲位列表 = new Table(new enum_儲位列表());
            this.sqL_DataGridView_RowsLED_儲位資料.RowsHeight = 40;
            this.sqL_DataGridView_RowsLED_儲位資料.Init(table_儲位列表);
            this.sqL_DataGridView_RowsLED_儲位資料.Set_ColumnVisible(false, new enum_儲位列表().GetEnumNames());
            this.sqL_DataGridView_RowsLED_儲位資料.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.編號);
            this.sqL_DataGridView_RowsLED_儲位資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.藥碼);
            this.sqL_DataGridView_RowsLED_儲位資料.Set_ColumnWidth(370, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.藥名);
            this.sqL_DataGridView_RowsLED_儲位資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_儲位列表.庫存);
            this.sqL_DataGridView_RowsLED_儲位資料.RowEnterEvent += SqL_DataGridView_RowsLED_儲位資料_RowEnterEvent;


            Table table_效期及批號 = new Table(new enum_效期及批號());
            this.sqL_DataGridView_儲位管理_RowsLED_效期及批號.RowsHeight = 40;
            this.sqL_DataGridView_儲位管理_RowsLED_效期及批號.Init(table_效期及批號);
            this.sqL_DataGridView_儲位管理_RowsLED_效期及批號.Set_ColumnVisible(false, new enum_效期及批號().GetEnumNames());
            this.sqL_DataGridView_儲位管理_RowsLED_效期及批號.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.效期);
            this.sqL_DataGridView_儲位管理_RowsLED_效期及批號.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.批號);
            this.sqL_DataGridView_儲位管理_RowsLED_效期及批號.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.庫存);

            this.rJ_Button_藥品資料_搜尋.MouseDownEvent += RJ_Button_藥品資料_搜尋_MouseDownEvent;
            this.rJ_Button_RowsLED_新增儲位.MouseDownEvent += RJ_Button_RowsLED_新增儲位_MouseDownEvent;
            this.rJ_Button_RowsLED_刪除儲位.MouseDownEvent += RJ_Button_RowsLED_刪除儲位_MouseDownEvent;
            this.rJ_Button_RowsLED_填入儲位.MouseDownEvent += RJ_Button_RowsLED_填入儲位_MouseDownEvent;
            this.rJ_Button_RowsLED_清除燈號.MouseDownEvent += RJ_Button_RowsLED_清除燈號_MouseDownEvent;
            this.rJ_Button_匯出.MouseDownEvent += RJ_Button_匯出_MouseDownEvent;
            this.rJ_Button_匯入.MouseDownEvent += RJ_Button_匯入_MouseDownEvent;
            this.rJ_Button_儲位名稱_儲存.MouseDownEvent += RJ_Button_儲位名稱_儲存_MouseDownEvent;

            this.plC_RJ_Button_RowLED_儲位設定_效期及批號_新增.MouseDownEvent += PlC_RJ_Button_RowLED_儲位設定_效期及批號_新增_MouseDownEvent;
            this.plC_RJ_Button_RowLED_儲位設定_效期及批號_刪除.MouseDownEvent += PlC_RJ_Button_RowLED_儲位設定_效期及批號_刪除_MouseDownEvent;
            this.plC_RJ_Button_RowLED_儲位設定_效期及批號_修改.MouseDownEvent += PlC_RJ_Button_RowLED_儲位設定_效期及批號_修改_MouseDownEvent;

            this.Function_層架列表_Refresh();
        }


        #region Function
        private void Function_層架列表_Refresh()
        {
            List<RowsLED> rowsLEDs = deviceApiClass.GetRowsLEDs(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType);
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < rowsLEDs.Count; i++)
            {
                object[] value = new object[new enum_儲位總表().GetLength()];
                value[(int)enum_儲位總表.GUID] = rowsLEDs[i].IP;
                value[(int)enum_儲位總表.IP] = rowsLEDs[i].IP;
                value[(int)enum_儲位總表.名稱] = rowsLEDs[i].Name;
                list_value.Add(value);
            }
            this.sqL_DataGridView_RowsLED_層架列表.RefreshGrid(list_value);
        }
        #endregion
        #region Event
        private void SqL_DataGridView_RowsLED_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {
            RJ_Button_RowsLED_填入儲位_MouseDownEvent(null);
        }
        private void SqL_DataGridView_RowsLED_儲位資料_RowEnterEvent(object[] RowValue)
        {
            if (this.rowsLED_Pannel.CurrentRowsLED != null) deviceApiClass.ReplaceRowsLED(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, this.rowsLED_Pannel.CurrentRowsLED);
            string GUID = RowValue[(int)enum_儲位列表.GUID].ObjectToString();
            RowsLED rowsLED = this.rowsLED_Pannel.CurrentRowsLED;
            RowsDevice rowsDevice = rowsLED.SortByGUID(GUID);
            if (rowsDevice == null) return;
            this.rowsLED_Pannel.RowsDeviceGUID = GUID;

            if (rJ_RatioButton_儲位資料_RowsLED_紅.Checked)
            {
                this.rowsLED_Pannel.SliderColor = Color.Red;
            }
            else if (rJ_RatioButton_儲位資料_RowsLED_藍.Checked)
            {
                this.rowsLED_Pannel.SliderColor = Color.Blue;
            }
            else if (rJ_RatioButton_儲位資料_RowsLED_綠.Checked)
            {
                this.rowsLED_Pannel.SliderColor = Color.Lime;
            }
            else if (rJ_RatioButton_儲位資料_RowsLED_白.Checked)
            {
                this.rowsLED_Pannel.SliderColor = Color.White;
            }

            sqL_DataGridView_儲位管理_RowsLED_效期及批號.ClearGrid();

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < rowsDevice.List_Validity_period.Count; i++)
            {
                object[] value = new object[new enum_效期及批號().GetLength()];
                value[(int)enum_效期及批號.效期] = rowsDevice.List_Validity_period[i];
                value[(int)enum_效期及批號.批號] = rowsDevice.List_Lot_number[i];
                value[(int)enum_效期及批號.庫存] = rowsDevice.List_Inventory[i];
                list_value.Add(value);
            }

            sqL_DataGridView_儲位管理_RowsLED_效期及批號.RefreshGrid(list_value);
        }
        private void SqL_DataGridView_RowsLED_層架列表_RowEnterEvent(object[] RowValue)
        {
            string IP = RowValue[(int)enum_儲位總表.IP].ObjectToString();
            RowsLED rowsLED = deviceApiClass.GetRowsLED_ByIP(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, IP);
            this.rowsLED_Pannel.CurrentRowsLED = rowsLED;
            rowsLED_Pannel.Maximum = rowsLED.Maximum;

            rJ_TextBox_儲位名稱.Text = rowsLED.Name;

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < rowsLED.RowsDevices.Count; i++)
            {
                object[] value = new object[new enum_儲位列表().GetLength()];
                value[(int)enum_儲位列表.GUID] = rowsLED.RowsDevices[i].GUID;
                value[(int)enum_儲位列表.編號] = rowsLED.RowsDevices[i].Index;
                value[(int)enum_儲位列表.藥碼] = rowsLED.RowsDevices[i].Code;
                value[(int)enum_儲位列表.藥名] = rowsLED.RowsDevices[i].Name;
                value[(int)enum_儲位列表.儲位名稱] = rowsLED.RowsDevices[i].StorageName;
                value[(int)enum_儲位列表.庫存] = rowsLED.RowsDevices[i].Inventory;
                list_value.Add(value);
            }

            this.sqL_DataGridView_RowsLED_儲位資料.RefreshGrid(list_value);
        }
        private void RJ_Button_RowsLED_新增儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_RowsLED_層架列表.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string IP = list_value[0][(int)enum_儲位總表.IP].ObjectToString();
            RowsLED rowsLED = deviceApiClass.GetRowsLED_ByIP(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, IP);
            rowsLED.Add(0, 8);
            deviceApiClass.ReplaceRowsLED(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, rowsLED);
            this.sqL_DataGridView_RowsLED_層架列表.On_RowEnter();
        }
        private void RJ_Button_RowsLED_刪除儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否刪除儲位?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<object[]> list_層架列表 = this.sqL_DataGridView_RowsLED_層架列表.Get_All_Select_RowsValues();
            List<object[]> list_儲位資料 = this.sqL_DataGridView_RowsLED_儲位資料.Get_All_Select_RowsValues();
            if (list_儲位資料.Count == 0 || list_層架列表.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string IP = list_層架列表[0][(int)enum_儲位總表.IP].ObjectToString();
            int index = list_儲位資料[0][(int)enum_儲位列表.編號].StringToInt32();
            RowsLED rowsLED = deviceApiClass.GetRowsLED_ByIP(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, IP);
            rowsLED.Delete(index);

            deviceApiClass.ReplaceRowsLED(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, rowsLED);
            this.sqL_DataGridView_RowsLED_層架列表.On_RowEnter();
        }
        private void RJ_Button_RowsLED_填入儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_藥品資料 = this.sqL_DataGridView_RowsLED_藥品資料.Get_All_Select_RowsValues();
            List<object[]> list_層架列表 = this.sqL_DataGridView_RowsLED_層架列表.Get_All_Select_RowsValues();
            List<object[]> list_儲位資料 = this.sqL_DataGridView_RowsLED_儲位資料.Get_All_Select_RowsValues();

            if (list_藥品資料.Count == 0 || list_儲位資料.Count == 0 || list_層架列表.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string IP = list_層架列表[0][(int)enum_儲位總表.IP].ObjectToString();
            string GUID = list_儲位資料[0][(int)enum_儲位列表.GUID].ObjectToString();
            string 藥碼 = list_藥品資料[0][(int)enum_雲端藥檔.藥品碼].ObjectToString();
            string 藥名 = list_藥品資料[0][(int)enum_雲端藥檔.藥品名稱].ObjectToString();
            string 包裝數量 = list_藥品資料[0][(int)enum_雲端藥檔.包裝數量].ObjectToString();
            RowsLED rowsLED = deviceApiClass.GetRowsLED_ByIP(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, IP);

            RowsDevice rowsDevice = rowsLED.SortByGUID(GUID);
            rowsDevice.Code = 藥碼;
            rowsDevice.Name = 藥名;
            rowsDevice.Package = 包裝數量;
            rowsLED.ReplaceRowsDevice(rowsDevice);

            deviceApiClass.ReplaceRowsLED(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, rowsLED);
            this.sqL_DataGridView_RowsLED_層架列表.On_RowEnter();
        }
        private void PlC_RJ_Button_RowLED_儲位設定_效期及批號_修改_MouseDownEvent(MouseEventArgs mevent)
        {

        }
        private void PlC_RJ_Button_RowLED_儲位設定_效期及批號_刪除_MouseDownEvent(MouseEventArgs mevent)
        {

        }
        private void PlC_RJ_Button_RowLED_儲位設定_效期及批號_新增_MouseDownEvent(MouseEventArgs mevent)
        {

        }
        private void RJ_Button_RowsLED_清除燈號_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.rowsLED_Pannel.CurrentRowsLED == null) return;
            Main_Form._rowsLEDUI.Set_Rows_LED_Clear_UDP(this.rowsLED_Pannel.CurrentRowsLED);          
        }
        private void RJ_Button_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                string text = textBox_處方搜尋_搜尋內容.Text;
                string cmb_text = "";
                this.Invoke(new Action(delegate { cmb_text = comboBox_藥品資料_搜尋條件.Text; }));
                LoadingForm.ShowLoadingForm();
                List<medClass> medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                List<medClass> medClasses_buf = new List<medClass>();
                if (cmb_text == "藥碼")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品碼.ToUpper().Contains(text)
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_RowsLED_藥品資料.RefreshGrid(list_value);

                }
                if (cmb_text == "藥名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品名稱.ToUpper().Contains(text)
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_RowsLED_藥品資料.RefreshGrid(list_value);
                }
                if (cmb_text == "中文名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.中文名稱.ToUpper().Contains(text)
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_RowsLED_藥品資料.RefreshGrid(list_value);
                }
            }
            catch
            {

            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
            
  
        }
        private void RJ_Button_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult dialogResult = DialogResult.None;
            this.Invoke(new Action(delegate
            {
                dialogResult = this.openFileDialog_LoadExcel.ShowDialog();
            }));
            if (dialogResult != DialogResult.OK) return;
            if (MyMessageBox.ShowDialog("確認匯入所有儲位?將會全部覆蓋!", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            List<SheetClass> sheetClasses = MyOffice.ExcelClass.NPOI_LoadToSheetClasses(this.openFileDialog_LoadExcel.FileName);
            for (int i = 0; i < sheetClasses.Count; i++)
            {
                string 儲位名稱 = sheetClasses[i].Name;
                RowsLED rowsLED = Main_Form.List_RowsLED_本地資料.SortByName(儲位名稱);
                if(rowsLED == null)
                {
                    MyMessageBox.ShowDialog($"找無此【{儲位名稱}】儲位名稱,請檢查表格頁籤是否與儲位名稱對應");
                    return;
                }
                rowsLED.RowsDevices.Clear();
                for (int k = 0; k < sheetClasses[i].Rows.Count; k++)
                {
                    if (sheetClasses[i].Rows[k].Cell.Count != 4) continue;
                    string Name = sheetClasses[i].Rows[k].Cell[0].Text;
                    string Code = sheetClasses[i].Rows[k].Cell[1].Text;
                    int RowsLEDStart = sheetClasses[i].Rows[k].Cell[2].Text.StringToInt32();
                    int RowsLEDEnd = sheetClasses[i].Rows[k].Cell[3].Text.StringToInt32();
                    RowsDevice rowsDevice = new RowsDevice(rowsLED.IP, rowsLED.Port, RowsLEDStart, RowsLEDEnd);
                    rowsDevice.Code = Code;
                    rowsDevice.Index = k;
                    rowsLED.RowsDevices.Add(rowsDevice);
                }
                Main_Form.List_RowsLED_本地資料.Add_NewRowsLED(rowsLED);
            }
            Main_Form._rowsLEDUI.SQL_ReplaceRowsLED(Main_Form.List_RowsLED_本地資料);
            MyMessageBox.ShowDialog($"匯入完成,將關閉開啟視窗更新資訊");
            this.Close();
        }
        private void RJ_Button_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult dialogResult = DialogResult.None;
            this.Invoke(new Action(delegate
            {
                dialogResult = this.saveFileDialog_SaveExcel.ShowDialog();
            }));
            Main_Form.Function_從SQL取得儲位到本地資料();
            if (dialogResult != DialogResult.OK) return;
            List<SheetClass> sheetClasses = new List<SheetClass>();
            List<object[]> list_儲位列表 = this.sqL_DataGridView_RowsLED_層架列表.GetAllRows();
            for (int i = 0; i < list_儲位列表.Count; i++)
            {
                string IP = list_儲位列表[i][(int)enum_儲位總表.IP].ObjectToString();
                RowsLED rowsLED = Main_Form.List_RowsLED_本地資料.SortByIP(IP);
                if (rowsLED == null) continue;
                SheetClass sheetClass = new SheetClass(rowsLED.Name);
                sheetClass.ColumnsWidth.Add(5000);
                sheetClass.ColumnsWidth.Add(5000);
                sheetClass.ColumnsWidth.Add(5000);
                sheetClass.ColumnsWidth.Add(5000);
                for (int k = 0; k < rowsLED.RowsDevices.Count; k++)
                {
                    int Num = k;
                    string Code = rowsLED.RowsDevices[k].Code;
                    int StartNum = rowsLED.RowsDevices[k].StartLED;
                    int EndNum = rowsLED.RowsDevices[k].EndLED;
                    sheetClass.AddNewCell(k, 0, $"{Num}", new Font("微軟正黑體", 14), 500);
                    sheetClass.AddNewCell(k, 1, $"{Code}", new Font("微軟正黑體", 14), 500);
                    sheetClass.AddNewCell(k, 2, $"{StartNum}", new Font("微軟正黑體", 14), 500);
                    sheetClass.AddNewCell(k, 3, $"{EndNum}", new Font("微軟正黑體", 14), 500);
                }
                sheetClasses.Add(sheetClass);
            }
            sheetClasses.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);
            MyMessageBox.ShowDialog("匯出完成!");
        }
        private void RJ_Button_儲位名稱_儲存_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_RowsLED_層架列表.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1000);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string IP = list_value[0][(int)enum_儲位總表.IP].ObjectToString();
            RowsLED rowsLED = deviceApiClass.GetRowsLED_ByIP(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, IP);
            rowsLED.Name = rJ_TextBox_儲位名稱.Text;

            deviceApiClass.ReplaceRowsLED(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType, rowsLED);
            list_value[0][(int)enum_儲位總表.名稱] = rowsLED.Name;
            this.sqL_DataGridView_RowsLED_層架列表.ReplaceExtra(list_value, true);
            MyMessageBox.ShowDialog("更新完成");


        }
        #endregion
    }
}
