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
namespace 智能藥庫系統
{
    public partial class Dialog_儲位管理 : MyDialog
    {
        public enum ContextMenuStrip_儲架電子紙列表
        {
            //[Description("M8000")]
            匯出,
            //[Description("M8000")]
            匯入,
            複製格式,
            貼上格式,
            清除儲位內容,

        }
        public enum enum_儲架儲位總表
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("IP,VARCHAR,15,NONE")]
            IP,
            [Description("名稱,VARCHAR,15,NONE")]
            名稱,
            [Description("區域,VARCHAR,15,NONE")]
            區域
                ,
        }

        private List<Storage> storages = new List<Storage>();
        private List<Drawer> drawers = new List<Drawer>();
        private Storage storage_copy = null;
        public enum enum_儲架電子紙列表
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("IP,VARCHAR,15,NONE")]
            IP,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("區域,VARCHAR,15,NONE")]
            區域,
        }
        public enum enum_儲架電子紙列表_匯出
        {     
            [Description("IP,VARCHAR,15,NONE")]
            IP,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("區域,VARCHAR,15,NONE")]
            區域,
        }
        public enum enum_儲架電子紙列表_匯入
        {
            [Description("IP,VARCHAR,15,NONE")]
            IP,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("區域,VARCHAR,15,NONE")]
            區域,
        }
        public static bool IsShown = false;
        static public Dialog_儲位管理 myDialog;
        static public Dialog_儲位管理 GetForm()
        {
            if (myDialog != null)
            {
                return myDialog;
            }
            else
            {
                myDialog = new Dialog_儲位管理();
                return myDialog;
            }
        }
        public Dialog_儲位管理()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
                
            }));
            this.Load += Dialog_儲位管理_Load;
            this.LoadFinishedEvent += Dialog_儲位管理_LoadFinishedEvent;
            this.ShowDialogEvent += Dialog_儲位管理_ShowDialogEvent;
            this.FormClosing += Dialog_儲位管理_FormClosing;
            this.rJ_Button_儲架電子紙_藥品資料_搜尋.MouseDownEvent += RJ_Button_儲架電子紙_藥品資料_搜尋_MouseDownEvent;
            this.rJ_Button_儲架電子紙_面板亮燈.MouseDownEvent += RJ_Button_儲架電子紙_面板亮燈_MouseDownEvent;



        }
        private void Dialog_儲位管理_Load(object sender, EventArgs e)
        {
            #region 儲架電子紙
            this.epD_290_Pannel.Init(Main_Form._storageUI_EPD_266.List_UDP_Local);

            this.comboBox_儲架電子紙_藥品資料_搜尋條件.SelectedIndex = 0;

            Table table_藥品資料 = medClass.init(Main_Form.API_Server);
            this.sqL_DataGridView_儲架電子紙_藥品資料.RowsHeight = 40;
            this.sqL_DataGridView_儲架電子紙_藥品資料.Init(table_藥品資料);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnText("藥碼", enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnText("藥名", enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_儲架電子紙_藥品資料.Set_ColumnText("單位", enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_儲架電子紙_藥品資料.RowDoubleClickEvent += SqL_DataGridView_儲架電子紙_藥品資料_RowDoubleClickEvent;

            Table table_儲架電子紙列表 = new Table(new enum_儲架電子紙列表());
            table_儲架電子紙列表[enum_儲架電子紙列表.區域.GetEnumName()].TypeName = Table.GetTypeName(Table.OtherType.ENUM, Main_Form.Function_取得藥品區域名稱().ToArray());

            this.sqL_DataGridView_儲架電子紙列表.Init(table_儲架電子紙列表);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnVisible(false, new enum_儲架電子紙列表().GetEnumNames());
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.IP);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.藥碼);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(600, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.藥名);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_儲架電子紙列表.區域);
            this.sqL_DataGridView_儲架電子紙列表.MouseDown += SqL_DataGridView_儲架電子紙列表_MouseDown;
            this.sqL_DataGridView_儲架電子紙列表.RowEnterEvent += SqL_DataGridView_儲架電子紙列表_RowEnterEvent;
            this.sqL_DataGridView_儲架電子紙列表.DataGridRowsChangeRefEvent += SqL_DataGridView_儲架電子紙列表_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_儲架電子紙列表.ComboBoxSelectedIndexChangedEvent += SqL_DataGridView_儲架電子紙列表_ComboBoxSelectedIndexChangedEvent;

            this.rJ_Button_儲架電子紙_藥品資料_填入儲位.MouseDownEvent += RJ_Button_儲架電子紙_藥品資料_填入儲位_MouseDownEvent;

            this.rJ_Button_儲架電子紙_面板亮燈.MouseDownEvent += RJ_Button_儲架電子紙_面板亮燈_MouseDownEvent;
            this.rJ_Button_儲架電子紙_清除燈號.MouseDownEvent += RJ_Button_儲架電子紙_清除燈號_MouseDownEvent;
            this.rJ_Button_儲架電子紙_面板刷新.MouseDownEvent += RJ_Button_儲架電子紙_面板刷新_MouseDownEvent;

            this.plC_RJ_Button_儲架電子紙_儲位內容_藥品名稱字體更動.MouseDownEvent += PlC_RJ_Button_儲架電子紙_儲位內容_藥品名稱字體更動_MouseDownEvent;
            this.plC_RJ_Button_儲架電子紙_儲位內容_藥品學名字體更動.MouseDownEvent += PlC_RJ_Button_儲架電子紙_儲位內容_藥品學名字體更動_MouseDownEvent;
            this.plC_RJ_Button_儲架電子紙_儲位內容_中文名稱字體更動.MouseDownEvent += PlC_RJ_Button_儲架電子紙_儲位內容_中文名稱字體更動_MouseDownEvent;
            this.plC_RJ_Button_儲架電子紙_儲位內容_藥品碼字體更動.MouseDownEvent += PlC_RJ_Button_儲架電子紙_儲位內容_藥品碼字體更動_MouseDownEvent;
            this.plC_RJ_Button_儲架電子紙_儲位內容_包裝單位字體更動.MouseDownEvent += PlC_RJ_Button_儲架電子紙_儲位內容_包裝單位字體更動_MouseDownEvent;
            this.plC_RJ_Button_儲架電子紙_儲位內容_效期字體更動.MouseDownEvent += PlC_RJ_Button_儲架電子紙_儲位內容_效期字體更動_MouseDownEvent;

            this.plC_CheckBox_儲架電子紙_儲位內容_藥品名稱顯示.CheckStateChanged += PlC_CheckBox_儲架電子紙_儲位內容_藥品名稱顯示_CheckStateChanged;
            this.plC_CheckBox_儲架電子紙_儲位內容_藥品學名顯示.CheckStateChanged += PlC_CheckBox_儲架電子紙_儲位內容_藥品學名顯示_CheckStateChanged;
            this.plC_CheckBox_儲架電子紙_儲位內容_中文名稱顯示.CheckStateChanged += PlC_CheckBox_儲架電子紙_儲位內容_中文名稱顯示_CheckStateChanged;
            this.plC_CheckBox_儲架電子紙_儲位內容_藥品碼顯示.CheckStateChanged += PlC_CheckBox_儲架電子紙_儲位內容_藥品碼顯示_CheckStateChanged;
            this.plC_CheckBox_儲架電子紙_儲位內容_包裝單位顯示.CheckStateChanged += PlC_CheckBox_儲架電子紙_儲位內容_包裝單位顯示_CheckStateChanged;
            this.plC_CheckBox_儲架電子紙_儲位內容_Barcode顯示.CheckStateChanged += PlC_CheckBox_儲架電子紙_儲位內容_Barcode顯示_CheckStateChanged;
            this.plC_CheckBox_儲架電子紙_儲位內容_效期顯示.CheckStateChanged += PlC_CheckBox_儲架電子紙_儲位內容_效期顯示_CheckStateChanged;

            this.rJ_Button_儲架電子紙列表_搜尋.MouseDownEvent += RJ_Button_儲架電子紙列表_搜尋_MouseDownEvent;
            this.comboBox_儲架電子紙列表_搜尋條件.SelectedIndex = 0;
            this.comboBox_儲架電子紙列表_搜尋條件.SelectedIndexChanged += ComboBox_儲架電子紙列表_搜尋條件_SelectedIndexChanged;
            Refresh_儲架電子紙列表_UI();
            #endregion
            #region 7"大電子紙
            this.sqL_DataGridView_EPD583_藥品資料.RowsHeight = 40;
            this.sqL_DataGridView_EPD583_藥品資料.Init(table_藥品資料);
            this.sqL_DataGridView_EPD583_藥品資料.Set_ColumnVisible(false, new enum_雲端藥檔().GetEnumNames());
            this.sqL_DataGridView_EPD583_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_EPD583_藥品資料.Set_ColumnWidth(650, DataGridViewContentAlignment.MiddleLeft, enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_EPD583_藥品資料.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_EPD583_藥品資料.Set_ColumnText("藥碼", enum_雲端藥檔.藥品碼);
            this.sqL_DataGridView_EPD583_藥品資料.Set_ColumnText("藥名", enum_雲端藥檔.藥品名稱);
            this.sqL_DataGridView_EPD583_藥品資料.Set_ColumnText("單位", enum_雲端藥檔.包裝單位);
            this.sqL_DataGridView_EPD583_藥品資料.RowDoubleClickEvent += SqL_DataGridView_EPD583_藥品資料_RowDoubleClickEvent;

            this.epD_583_Pannel.Init(Main_Form._drawerUI_EPD_583.GetLoacalUDP_Class());
            Table table_儲架儲位總表 = new Table(new enum_儲架儲位總表());
            table_儲架儲位總表[enum_儲架儲位總表.區域.GetEnumName()].TypeName = Table.GetTypeName(Table.OtherType.ENUM, Main_Form.Function_取得藥品區域名稱().ToArray());
            this.sqL_DataGridView_EPD583_儲位列表.RowsHeight = 40;
            this.sqL_DataGridView_EPD583_儲位列表.Init(table_儲架儲位總表);
            this.sqL_DataGridView_EPD583_儲位列表.Set_ColumnVisible(false, new enum_儲架儲位總表().GetEnumNames());
            this.sqL_DataGridView_EPD583_儲位列表.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_儲架儲位總表.IP);
            //this.sqL_DataGridView_EPD583_儲位列表.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_儲架儲位總表.名稱);
            this.sqL_DataGridView_EPD583_儲位列表.Set_ColumnWidth(140, DataGridViewContentAlignment.MiddleLeft, enum_儲架儲位總表.區域);
            this.sqL_DataGridView_EPD583_儲位列表.RowEnterEvent += SqL_DataGridView_EPD583_儲位列表_RowEnterEvent;
            this.sqL_DataGridView_EPD583_儲位列表.ComboBoxSelectedIndexChangedEvent += SqL_DataGridView_EPD583_儲位列表_ComboBoxSelectedIndexChangedEvent;

            this.rJ_Button_EPD583_藥品資料_搜尋.MouseDownEvent += RJ_Button_EPD583_藥品資料_搜尋_MouseDownEvent;
            this.rJ_Button_EPD583_藥品資料_填入儲位.MouseDownEvent += RJ_Button_EPD583_藥品資料_填入儲位_MouseDownEvent;
            this.comboBox_EPD583_藥品資料_搜尋條件.SelectedIndex = 0;

            this.button_EPD583_藥碼字體.Click += Button_EPD583_藥碼字體_Click;
            this.button_EPD583_藥名字體.Click += Button_EPD583_藥名字體_Click;
            this.panel_EPD583_藥碼顏色.Click += Panel_EPD583_藥碼顏色_Click;
            this.panel_EPD583_藥名顏色.Click += Panel_EPD583_藥名顏色_Click;
            this.panel_EPD583_背景顏色.Click += Panel_EPD583_背景顏色_Click;
            this.epD_583_Pannel.DrawerChangeEvent += EpD_583_Pannel_DrawerChangeEvent;
            this.epD_583_Pannel.MouseDownEvent += EpD_583_Pannel_MouseDownEvent;
            Refresh_7吋大電子紙_UI();
            #endregion
        }
        #region Function
        private void Refresh_儲架電子紙列表_UI()
        {
            this.storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < storages.Count; i++)
            {
                object[] value = new object[new enum_儲架電子紙列表().GetLength()];
                value[(int)enum_儲架電子紙列表.GUID] = storages[i].GUID;
                value[(int)enum_儲架電子紙列表.IP] = storages[i].IP;
                value[(int)enum_儲架電子紙列表.藥碼] = storages[i].Code;
                value[(int)enum_儲架電子紙列表.藥名] = storages[i].Name;
                value[(int)enum_儲架電子紙列表.區域] = storages[i].Area;


                list_value.Add(value);
            }
            list_value.Sort(new ICP_儲架電子紙());
            this.sqL_DataGridView_儲架電子紙列表.RefreshGrid(list_value);
        }
        private void Refresh_7吋大電子紙_UI()
        {
            this.drawers = Main_Form._drawerUI_EPD_583.SQL_GetAllDrawers();

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < drawers.Count; i++)
            {
                object[] value = new object[new enum_儲架儲位總表().GetLength()];
                value[(int)enum_儲架儲位總表.IP] = drawers[i].IP;
                value[(int)enum_儲架儲位總表.名稱] = drawers[i].Name;
                value[(int)enum_儲架儲位總表.區域] = drawers[i].Area;


                list_value.Add(value);
            }
            list_value.Sort(new ICP_儲架儲位總表());
            this.sqL_DataGridView_EPD583_儲位列表.RefreshGrid(list_value);
        }
        #endregion
        #region Event
        private void Dialog_儲位管理_ShowDialogEvent()
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
        private void Dialog_儲位管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            myDialog = null;
        }
        private void Dialog_儲位管理_LoadFinishedEvent(EventArgs e)
        {
            this.Refresh();
        }

        #region 儲架電子紙
        private void SqL_DataGridView_儲架電子紙列表_ComboBoxSelectedIndexChangedEvent(object sender, string colName, object[] RowValue)
        {
            string IP = RowValue[(int)enum_儲架電子紙列表.IP].ObjectToString();
            string 區域 = RowValue[(int)enum_儲架電子紙列表.區域].ObjectToString();

            Storage storage = Main_Form._storageUI_EPD_266.SQL_GetStorage(IP);
            if(storage == null)
            {
                MyMessageBox.ShowDialog("查無儲位資料");
                return;
            }
            storage.Area = 區域;
            Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            Refresh_儲架電子紙列表_UI();
        }
        private void SqL_DataGridView_儲架電子紙列表_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_儲架電子紙列表());
                if (dialog_ContextMenuStrip.ShowDialog() == DialogResult.Yes)
                {
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_儲架電子紙列表.匯出.GetEnumName())
                    {
                        List<object[]> list_value = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
                        //if (list_value.Count == 0)
                        //{
                        //    MyMessageBox.ShowDialog("未選取資料");
                        //    return;
                        //}
                        DataTable dataTable = list_value.ToDataTable(new enum_儲架電子紙列表());
                        dataTable = dataTable.ReorderTable(new enum_儲架電子紙列表_匯出().GetEnumNames());

                        if (saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;
                        dataTable.NPOI_SaveFile(saveFileDialog_SaveExcel.FileName);
                        MyMessageBox.ShowDialog("匯出成功");
                    }
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_儲架電子紙列表.匯入.GetEnumName())
                    {
                        if (openFileDialog_LoadExcel.ShowDialog() != DialogResult.OK) return;

                        DataTable dataTable = MyOffice.ExcelClass.NPOI_LoadFile(openFileDialog_LoadExcel.FileName);
                        if (dataTable == null)
                        {
                            MyMessageBox.ShowDialog("匯入失敗");
                            return;
                        }
                        dataTable = dataTable.ReorderTable(new enum_儲架電子紙列表());
                        if (dataTable == null)
                        {
                            MyMessageBox.ShowDialog("匯入失敗");
                            return;
                        }
                        List<object[]> list_value = dataTable.DataTableToRowList();
                        List<Storage> storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();
                        List<Storage> storages_replace = new List<Storage>();
                        List<medClass> medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                        List<medClass> medClasses_buf = new List<medClass>();
                        Dictionary<string, List<medClass>> keyValuePairs_medcloud = medClasses.CoverToDictionaryByCode();

                        for (int i = 0; i < list_value.Count; i++)
                        {
                            string 藥碼 = list_value[i][(int)enum_儲架電子紙列表.藥碼].ObjectToString();
                            string 區域 = list_value[i][(int)enum_儲架電子紙列表.區域].ObjectToString();
                            string IP = list_value[i][(int)enum_儲架電子紙列表.IP].ObjectToString();
                            Storage storage = storages.SortByIP(IP);
                            if (storage == null) continue;
                            medClasses_buf = keyValuePairs_medcloud.SortDictionaryByCode(藥碼);
                            storage.Area = 區域;
                            if(medClasses_buf.Count > 0)
                            {
                                storage.Code = 藥碼;
                                storage.Name = medClasses_buf[0].藥品名稱;
                                storage.ChineseName = medClasses_buf[0].中文名稱;
                                storage.Package = medClasses_buf[0].包裝單位;
                                storage.IsWarning = medClasses_buf[0].警訊藥品.StringToBool();

                            }
                            storages_replace.Add(storage);
                        }

                        Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storages_replace);
                        Refresh_儲架電子紙列表_UI();
                        MyMessageBox.ShowDialog("匯入完成");
                    }
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_儲架電子紙列表.複製格式.GetEnumName())
                    {
                        List<object[]> list_儲架電子紙列表 = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
                        if (list_儲架電子紙列表.Count == 0)
                        {
                            MyMessageBox.ShowDialog("未選取儲架電子紙");
                            return;
                        }
                        string IP = list_儲架電子紙列表[0][(int)enum_儲架電子紙列表.IP].ObjectToString();
                        Storage storage = Main_Form._storageUI_EPD_266.SQL_GetStorage(IP);
                        storage_copy = storage;
                        MyMessageBox.ShowDialog("複製完成");
                    }
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_儲架電子紙列表.貼上格式.GetEnumName())
                    {
                        if (storage_copy == null)
                        {
                            MyMessageBox.ShowDialog("操作失敗,請先複製儲位");
                            return;
                        }
                        List<object[]> list_儲架電子紙列表 = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
                        if (list_儲架電子紙列表.Count == 0)
                        {
                            MyMessageBox.ShowDialog("未選取儲架電子紙");
                            return;
                        }

                        if (MyMessageBox.ShowDialog($"是否將複製至所選儲位共<{list_儲架電子紙列表.Count}>筆?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

                        List<Storage> storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();
                        List<Storage> storages_replace = new List<Storage>();


                        for (int i = 0; i < list_儲架電子紙列表.Count; i++)
                        {
                            string IP = list_儲架電子紙列表[i][(int)enum_儲架電子紙列表.IP].ObjectToString();
                            Storage storage = storages.SortByIP(IP);
                            if (storage != null)
                            {
                                storage.PasteFormat(storage_copy);
                                storages_replace.Add(storage);
                            }
                        }

                        Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storages_replace);
                        Refresh_儲架電子紙列表_UI();

                    }
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_儲架電子紙列表.清除儲位內容.GetEnumName())
                    {

                        List<object[]> list_儲架電子紙列表 = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
                        if (list_儲架電子紙列表.Count == 0)
                        {
                            MyMessageBox.ShowDialog("未選取儲架電子紙");
                            return;
                        }

                        if (MyMessageBox.ShowDialog($"是否清除所選儲位共<{list_儲架電子紙列表.Count}>筆?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

                        List<Storage> storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();
                        List<Storage> storages_replace = new List<Storage>();


                        for (int i = 0; i < list_儲架電子紙列表.Count; i++)
                        {
                            string IP = list_儲架電子紙列表[i][(int)enum_儲架電子紙列表.IP].ObjectToString();
                            Storage storage = storages.SortByIP(IP);
                            if (storage != null)
                            {
                                storage.ClearStorage();
                                storages_replace.Add(storage);
                            }
                        }

                        Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storages_replace);
                        Refresh_儲架電子紙列表_UI();

                        MyMessageBox.ShowDialog("清除完成");
                    }
                }
            }
        }
        private void SqL_DataGridView_儲架電子紙_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {
            RJ_Button_儲架電子紙_藥品資料_填入儲位_MouseDownEvent(null);
        }
        private void SqL_DataGridView_儲架電子紙列表_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            if (plC_CheckBox_儲架電子紙列表_顯示空儲位.Checked == false)
            {
                RowsList = (from temp in RowsList
                            where temp[(int)enum_儲架電子紙列表.藥碼].ObjectToString().StringIsEmpty() == false
                            select temp).ToList();
            }

   

        }
        private void SqL_DataGridView_儲架電子紙列表_RowEnterEvent(object[] RowValue)
        {
            string IP = RowValue[(int)enum_儲架電子紙列表.IP].ObjectToString();
            Console.WriteLine($"[Debug] 取得儲位 IP: {IP}");

            Storage storage = Main_Form._storageUI_EPD_266.SQL_GetStorage(IP);
            if (storage == null)
            {
                Console.WriteLine("[Warning] 找不到對應的 Storage 資料！");
                return;
            }

            Console.WriteLine($"[Debug] 取得儲位資料: Code={storage.Code}, Name={storage.Name}");

            medClass _medClass = medClass.get_med_clouds_by_code(Main_Form.API_Server, storage.Code);

            rJ_TextBox_儲架電子紙_儲位內容_藥品名稱.Text = storage.Name;
            rJ_TextBox_儲架電子紙_儲位內容_藥品學名.Text = storage.Scientific_Name;
            rJ_TextBox_儲架電子紙_儲位內容_中文名稱.Text = storage.ChineseName;
            rJ_TextBox_儲架電子紙_儲位內容_藥品碼.Text = storage.Code;
            rJ_TextBox_儲架電子紙_儲位內容_包裝單位.Text = storage.Package;
            rJ_TextBox_儲架電子紙_儲位內容_總庫存.Text = storage.Inventory;
            Console.WriteLine($"[Debug] 更新儲位內容顯示：藥品碼={storage.Code}, 總庫存={storage.Inventory}");

            plC_CheckBox_儲架電子紙_儲位內容_藥品名稱顯示.Checked = storage.Name_Visable;
            plC_CheckBox_儲架電子紙_儲位內容_藥品學名顯示.Checked = storage.Scientific_Name_Visable;
            plC_CheckBox_儲架電子紙_儲位內容_中文名稱顯示.Checked = storage.ChineseName_Visable;
            plC_CheckBox_儲架電子紙_儲位內容_藥品碼顯示.Checked = storage.Code_Visable;
            plC_CheckBox_儲架電子紙_儲位內容_包裝單位顯示.Checked = storage.Package_Visable;
            plC_CheckBox_儲架電子紙_儲位內容_庫存顯示.Checked = storage.Inventory_Visable;
            plC_CheckBox_儲架電子紙_儲位內容_Barcode顯示.Checked = storage.BarCode_Visable;
            plC_CheckBox_儲架電子紙_儲位內容_效期顯示.Checked = storage.Validity_period_Visable;
            Console.WriteLine("[Debug] 顯示選項已同步到 Checkbox");

            if (_medClass != null)
            {
                Console.WriteLine($"[Debug] _medClass.警訊藥品 : {_medClass.警訊藥品}");

                if (_medClass.警訊藥品 == "Y" || _medClass.警訊藥品.ToUpper() == "TRUE")
                {
                    storage.IsWarning = true;
                    Console.WriteLine($"[Debug] 設定警訊藥品標記: {storage.IsWarning}");
                }

            }

            epD_290_Pannel.DrawToPictureBox(storage);
            Console.WriteLine("[Debug] 已繪製至電子紙面板");



        }
        private void ComboBox_儲架電子紙列表_搜尋條件_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_儲架電子紙列表_搜尋條件.Text == "區域")
            {
                this.comboBox_儲架電子紙列表_搜尋內容.DataSource = Main_Form.Function_取得藥品區域名稱();
                this.comboBox_儲架電子紙列表_搜尋內容.SelectedIndex = 0;
            }
            else
            {
                this.comboBox_儲架電子紙列表_搜尋內容.DataSource = null;
            }
        }
        private void RJ_Button_儲架電子紙列表_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = "";
            string serch_value = "";
            this.Invoke(new Action(delegate 
            {
                text = comboBox_儲架電子紙列表_搜尋條件.Text;
                serch_value = comboBox_儲架電子紙列表_搜尋內容.Text;
            }));
            if (text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("搜尋內容空白");
                return;
            }
            LoadingForm.ShowLoadingForm();
            this.storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();

            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < storages.Count; i++)
            {
                object[] value = new object[new enum_儲架電子紙列表().GetLength()];
                value[(int)enum_儲架電子紙列表.GUID] = storages[i].GUID;
                value[(int)enum_儲架電子紙列表.IP] = storages[i].IP;
                value[(int)enum_儲架電子紙列表.藥碼] = storages[i].Code;
                value[(int)enum_儲架電子紙列表.藥名] = storages[i].Name;
                value[(int)enum_儲架電子紙列表.區域] = storages[i].Area;


                list_value.Add(value);
            }
            list_value.Sort(new ICP_儲架電子紙());
            if (text == "全部顯示")
            {
                list_value = list_value;
            }
            if (text == "藥碼")
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_儲架電子紙列表.藥碼].ObjectToString().ToUpper().Contains(serch_value.ToUpper())
                              select temp).ToList();
            }
            if (text == "藥名")
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_儲架電子紙列表.藥名].ObjectToString().ToUpper().Contains(serch_value.ToUpper())
                              select temp).ToList();
            }
            if (text == "IP")
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_儲架電子紙列表.IP].ObjectToString().ToUpper().Contains(serch_value.ToUpper())
                              select temp).ToList();
            }
            if (text == "區域")
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_儲架電子紙列表.區域].ObjectToString().ToUpper().Contains(serch_value.ToUpper())
                              select temp).ToList();
            }
            this.sqL_DataGridView_儲架電子紙列表.RefreshGrid(list_value);
         
            LoadingForm.CloseLoadingForm();

            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("查無資料");
                return;
            }
        }
        private void PlC_CheckBox_儲架電子紙_儲位內容_效期顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.效期, Device.ValueType.Visable, this.plC_CheckBox_儲架電子紙_儲位內容_效期顯示.Checked);
                this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_儲架電子紙_儲位內容_Barcode顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.BarCode, Device.ValueType.Visable, this.plC_CheckBox_儲架電子紙_儲位內容_Barcode顯示.Checked);
                this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_儲架電子紙_儲位內容_包裝單位顯示_CheckStateChanged(object sender, EventArgs e)
        {

            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.包裝單位, Device.ValueType.Visable, this.plC_CheckBox_儲架電子紙_儲位內容_包裝單位顯示.Checked);
                this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_儲架電子紙_儲位內容_藥品碼顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.藥品碼, Device.ValueType.Visable, this.plC_CheckBox_儲架電子紙_儲位內容_藥品碼顯示.Checked);
                this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_儲架電子紙_儲位內容_中文名稱顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Visable, this.plC_CheckBox_儲架電子紙_儲位內容_中文名稱顯示.Checked);
                this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_儲架電子紙_儲位內容_藥品學名顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.藥品學名, Device.ValueType.Visable, this.plC_CheckBox_儲架電子紙_儲位內容_藥品學名顯示.Checked);
                this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
        private void PlC_CheckBox_儲架電子紙_儲位內容_藥品名稱顯示_CheckStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                storage.SetValue(Device.ValueName.藥品名稱, Device.ValueType.Visable, this.plC_CheckBox_儲架電子紙_儲位內容_藥品名稱顯示.Checked);
                this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            }));
        }
  
        private void PlC_RJ_Button_儲架電子紙_儲位內容_效期字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.效期, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.效期, Device.ValueType.Font, fontDialog.Font);
                    this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                    Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_儲架電子紙_儲位內容_包裝單位字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.包裝單位, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.包裝單位, Device.ValueType.Font, fontDialog.Font);
                    this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                    Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_儲架電子紙_儲位內容_藥品碼字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品碼, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品碼, Device.ValueType.Font, fontDialog.Font);
                    this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                    Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_儲架電子紙_儲位內容_中文名稱字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品中文名稱, Device.ValueType.Font, fontDialog.Font);
                    this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                    Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_儲架電子紙_儲位內容_藥品學名字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品學名, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品學名, Device.ValueType.Font, fontDialog.Font);
                    this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                    Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }
        private void PlC_RJ_Button_儲架電子紙_儲位內容_藥品名稱字體更動_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Storage storage = this.epD_290_Pannel.CurrentStorage;
                if (storage == null) return;
                this.fontDialog.Font = storage.GetValue(Device.ValueName.藥品名稱, Device.ValueType.Font) as Font;
                if (this.fontDialog.ShowDialog() == DialogResult.OK)
                {
                    storage.SetValue(Device.ValueName.藥品名稱, Device.ValueType.Font, fontDialog.Font);
                    this.epD_290_Pannel.DrawToPictureBox(this.epD_290_Pannel.CurrentStorage);
                    Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
                }
            }));
        }

        private void RJ_Button_儲架電子紙_面板刷新_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_儲架電子紙列表 = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
            if (list_儲架電子紙列表.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲架電子紙");
                return;
            }
            List<Storage> storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();
            List<Storage> storages_buf = new List<Storage>();

            for (int i = 0; i < list_儲架電子紙列表.Count; i++)
            {
                string IP = list_儲架電子紙列表[i][(int)enum_儲架電子紙列表.IP].ObjectToString();
                Storage storage = storages.SortByIP(IP);
                medClass _medClass = medClass.get_med_clouds_by_code(Main_Form.API_Server, storage.Code);
                if (_medClass.警訊藥品 == "Y" || _medClass.警訊藥品.ToUpper() == "TRUE")
                {
                    storage.IsWarning = true;
                    Console.WriteLine($"[Debug] 設定警訊藥品標記: {storage.IsWarning}");
                }
                storage.Inventory_Visable = false;
                if (storage != null)
                {
                    storages_buf.Add(storage);
                }
            }
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < storages_buf.Count; i++)
            {
                Storage storage = storages_buf[i];
                tasks.Add(Task.Run(new Action(delegate
                {
                    Main_Form._storageUI_EPD_266.DrawToEpd_UDP(storage);
                })));
            }
            Task.WhenAll(tasks).Wait();

            Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("面板更新完成", 1000, Color.YellowGreen);
            dialog_AlarmForm.ShowDialog();
        }
    
        private void RJ_Button_儲架電子紙_面板亮燈_MouseDownEvent(MouseEventArgs mevent)
        {
            Color color = Color.Red;
            if (rJ_RatioButton_儲架電子紙_紅.Checked)
            {
                color = Color.Red;
            }
            if (rJ_RatioButton_儲架電子紙_綠.Checked)
            {
                color = Color.Green;
            }
            if (rJ_RatioButton_儲架電子紙_藍.Checked)
            {
                color = Color.Blue;
            }
            if (rJ_RatioButton_儲架電子紙_白.Checked)
            {
                color = Color.White;
            }
            List<object[]> list_儲架電子紙列表 = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
            if (list_儲架電子紙列表.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲架電子紙");
                return;
            }
            List<Storage> storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();
            List<Storage> storages_buf = new List<Storage>();

            for (int i = 0; i < list_儲架電子紙列表.Count; i++)
            {
                string IP = list_儲架電子紙列表[i][(int)enum_儲架電子紙列表.IP].ObjectToString();
                Storage storage = storages.SortByIP(IP);
                if (storage != null)
                {
                    storages_buf.Add(storage);
                }
            }
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < storages_buf.Count; i++)
            {
                Storage storage = storages_buf[i];
                tasks.Add(Task.Run(new Action(delegate
                {
                    Main_Form._storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);

                })));

            }
            Task.WhenAll(tasks).Wait();

            //Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("亮燈完成", 1000, Color.YellowGreen);
            //dialog_AlarmForm.ShowDialog();

        }
        private void RJ_Button_儲架電子紙_清除燈號_MouseDownEvent(MouseEventArgs mevent)
        {
            Color color = Color.Black;
        
            List<object[]> list_儲架電子紙列表 = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
            if (list_儲架電子紙列表.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲架電子紙");
                return;
            }
            List<Storage> storages = Main_Form._storageUI_EPD_266.SQL_GetAllStorage();
            List<Storage> storages_buf = new List<Storage>();

            for (int i = 0; i < list_儲架電子紙列表.Count; i++)
            {
                string IP = list_儲架電子紙列表[i][(int)enum_儲架電子紙列表.IP].ObjectToString();
                Storage storage = storages.SortByIP(IP);
                if (storage != null)
                {
                    storages_buf.Add(storage);
                }
            }
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < storages_buf.Count; i++)
            {
                Storage storage = storages_buf[i];
                tasks.Add(Task.Run(new Action(delegate
                {
                    Main_Form._storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);

                })));

            }
            Task.WhenAll(tasks).Wait();

  
        }
        private void RJ_Button_儲架電子紙_藥品資料_填入儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_藥品資料 = this.sqL_DataGridView_儲架電子紙_藥品資料.Get_All_Select_RowsValues();
            List<object[]> list_儲架電子紙列表 = this.sqL_DataGridView_儲架電子紙列表.Get_All_Select_RowsValues();
            if (list_藥品資料.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取藥品資料");
                return;
            }
            if (list_儲架電子紙列表.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲架電子紙");
                return;
            }
            medClass medClass = list_藥品資料[0].SQLToClass<medClass, enum_雲端藥檔>();
            string IP = list_儲架電子紙列表[0][(int)enum_儲架電子紙列表.IP].ObjectToString();
            Storage storage = Main_Form._storageUI_EPD_266.SQL_GetStorage(IP);
            storage = storage.SetMedClass(medClass);
            Main_Form._storageUI_EPD_266.SQL_ReplaceStorage(storage);
            Refresh_儲架電子紙列表_UI();
        }
        private void RJ_Button_儲架電子紙_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                string text = textBox_儲架電子紙_藥品資料_搜尋內容.Text;
                string cmb_text = "";
                this.Invoke(new Action(delegate { cmb_text = comboBox_儲架電子紙_藥品資料_搜尋條件.Text; }));
                LoadingForm.ShowLoadingForm();
                List<medClass> medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                List<medClass> medClasses_buf = new List<medClass>();
                if (cmb_text == "藥碼")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品碼.ToUpper().Contains(text.ToUpper())
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_儲架電子紙_藥品資料.RefreshGrid(list_value);

                }
                if (cmb_text == "藥名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品名稱.ToUpper().Contains(text.ToUpper())
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_儲架電子紙_藥品資料.RefreshGrid(list_value);
                }
                if (cmb_text == "中文名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.中文名稱.ToUpper().Contains(text.ToUpper())
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_儲架電子紙_藥品資料.RefreshGrid(list_value);
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
        #endregion

        #region EPD583
        private void SqL_DataGridView_EPD583_儲位列表_ComboBoxSelectedIndexChangedEvent(object sender, string colName, object[] RowValue)
        {
            string IP = RowValue[(int)enum_儲架儲位總表.IP].ObjectToString();
            string 區域 = RowValue[(int)enum_儲架儲位總表.區域].ObjectToString();

            Drawer drawer = Main_Form._drawerUI_EPD_583.SQL_GetDrawer(IP);
            if (drawer == null)
            {
                MyMessageBox.ShowDialog("查無儲位資料");
                return;
            }
            drawer.Area = 區域;
            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);
            Refresh_7吋大電子紙_UI();
        }
        private void EpD_583_Pannel_MouseDownEvent(List<Box> Boxes)
        {
            if (Boxes.Count == 0) return;
            Box box = Boxes[0];
            label_EPD583_藥碼.Text = box.Code;
            label_EPD583_料號.Text = box.SKDIACODE;
            label_EPD583_藥名.Text = box.Name;
            label_EPD583_中文名.Text = box.ChineseName;

            if (label_EPD583_藥碼.Text.StringIsEmpty()) label_EPD583_藥碼.Text = "無";
            if (label_EPD583_料號.Text.StringIsEmpty()) label_EPD583_料號.Text = "無";
            if (label_EPD583_藥名.Text.StringIsEmpty()) label_EPD583_藥名.Text = "無";
            if (label_EPD583_中文名.Text.StringIsEmpty()) label_EPD583_中文名.Text = "無";

            this.panel_EPD583_藥碼顏色.BackColor = box.Code_ForeColor;
            this.panel_EPD583_藥名顏色.BackColor = box.Name_ForeColor;
            this.panel_EPD583_背景顏色.BackColor = box.BackColor;
        }
        private void EpD_583_Pannel_DrawerChangeEvent(Drawer drawer)
        {
            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);
        }
        private void SqL_DataGridView_EPD583_儲位列表_RowEnterEvent(object[] RowValue)
        {
            string IP = RowValue[(int)enum_儲架儲位總表.IP].ObjectToString();

            Drawer drawer = Main_Form._drawerUI_EPD_583.SQL_GetDrawer(IP);
            if (drawer != null)
            {
                this.epD_583_Pannel.DrawToPictureBox(drawer);
            }
        }
        private void SqL_DataGridView_EPD583_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {
           
        }
        private void RJ_Button_EPD583_藥品資料_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                string text = textBox_EPD583_藥品資料_搜尋內容.Text;
                string cmb_text = "";
                this.Invoke(new Action(delegate { cmb_text = comboBox_EPD583_藥品資料_搜尋條件.Text; }));
                LoadingForm.ShowLoadingForm();
                List<medClass> medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                List<medClass> medClasses_buf = new List<medClass>();
                if (cmb_text == "藥碼")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品碼.ToUpper().Contains(text.ToUpper())
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_EPD583_藥品資料.RefreshGrid(list_value);

                }
                if (cmb_text == "藥名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.藥品名稱.ToUpper().Contains(text.ToUpper())
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_EPD583_藥品資料.RefreshGrid(list_value);
                }
                if (cmb_text == "中文名")
                {
                    medClasses_buf = (from temp in medClasses
                                      where temp.中文名稱.ToUpper().Contains(text.ToUpper())
                                      select temp).ToList();
                    if (medClasses_buf.Count == 0)
                    {
                        Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                        dialog_AlarmForm.ShowDialog();
                        return;
                    }
                    List<object[]> list_value = medClasses_buf.ClassToSQL<medClass, enum_雲端藥檔>();
                    this.sqL_DataGridView_EPD583_藥品資料.RefreshGrid(list_value);
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
        private void RJ_Button_EPD583_藥品資料_填入儲位_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_藥品資料 = this.sqL_DataGridView_EPD583_藥品資料.Get_All_Select_RowsValues();
            if (list_藥品資料.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取藥品資料");
                return;
            }
            List<Box> boxes = this.epD_583_Pannel.GetSelectBoxes();
            if (boxes.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲位");
                return;
            }
            Drawer drawer = epD_583_Pannel.CurrentDrawer;
            medClass medClass = list_藥品資料[0].SQLToClass<medClass, enum_雲端藥檔>();
            boxes[0].SetMedClass(medClass);
            drawer.ReplaceBox(boxes[0]);
            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(drawer);

            this.epD_583_Pannel.DrawToPictureBox(drawer);
        }
        private void Button_EPD583_藥名字體_Click(object sender, EventArgs e)
        {
            List<Box> boxes = this.epD_583_Pannel.GetSelectBoxes();
            if (boxes.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲位");
            }
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;

           
            Box box = boxes[0];
            box.Name_font = this.fontDialog.Font;

            this.epD_583_Pannel.CurrentDrawer.ReplaceBox(box);
            this.epD_583_Pannel.DrawToPictureBox();

            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(this.epD_583_Pannel.CurrentDrawer);

        }
        private void Button_EPD583_藥碼字體_Click(object sender, EventArgs e)
        {
            List<Box> boxes = this.epD_583_Pannel.GetSelectBoxes();
            if (boxes.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲位");
            }
            if (this.fontDialog.ShowDialog() != DialogResult.OK) return;

          
            Box box = boxes[0];
            box.Code_font = this.fontDialog.Font;

            this.epD_583_Pannel.CurrentDrawer.ReplaceBox(box);
            this.epD_583_Pannel.DrawToPictureBox();
            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(this.epD_583_Pannel.CurrentDrawer);
        }

        private void Panel_EPD583_藥碼顏色_Click(object sender, EventArgs e)
        {
            List<Box> boxes = this.epD_583_Pannel.GetSelectBoxes();
            if (boxes.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲位");
            }

            Box box = boxes[0];
            Dialog_EPD730_顏色選擇 dialog_EPD730_顏色選擇 = new Dialog_EPD730_顏色選擇(box.Code_ForeColor);
            if (dialog_EPD730_顏色選擇.ShowDialog() != DialogResult.Yes) return;
            box.Code_ForeColor = dialog_EPD730_顏色選擇.Value;

            this.epD_583_Pannel.CurrentDrawer.ReplaceBox(box);
            this.epD_583_Pannel.DrawToPictureBox();
            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(this.epD_583_Pannel.CurrentDrawer);
        }
        private void Panel_EPD583_藥名顏色_Click(object sender, EventArgs e)
        {
            List<Box> boxes = this.epD_583_Pannel.GetSelectBoxes();
            if (boxes.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲位");
            }

            Box box = boxes[0];
            Dialog_EPD730_顏色選擇 dialog_EPD730_顏色選擇 = new Dialog_EPD730_顏色選擇(box.Name_ForeColor);
            if (dialog_EPD730_顏色選擇.ShowDialog() != DialogResult.Yes) return;
            box.Name_ForeColor = dialog_EPD730_顏色選擇.Value;

            this.epD_583_Pannel.CurrentDrawer.ReplaceBox(box);
            this.epD_583_Pannel.DrawToPictureBox();
            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(this.epD_583_Pannel.CurrentDrawer);
        }
        private void Panel_EPD583_背景顏色_Click(object sender, EventArgs e)
        {
            List<Box> boxes = this.epD_583_Pannel.GetSelectBoxes();
            if (boxes.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取儲位");
            }

            Box box = boxes[0];
            Dialog_EPD730_顏色選擇 dialog_EPD730_顏色選擇 = new Dialog_EPD730_顏色選擇(box.BackColor);
            if (dialog_EPD730_顏色選擇.ShowDialog() != DialogResult.Yes) return;
            box.BackColor = dialog_EPD730_顏色選擇.Value;

            this.epD_583_Pannel.CurrentDrawer.ReplaceBox(box);
            this.epD_583_Pannel.DrawToPictureBox();
            Main_Form._drawerUI_EPD_583.SQL_ReplaceDrawer(this.epD_583_Pannel.CurrentDrawer);
        }
        #endregion

        #endregion

        private class ICP_儲架電子紙: IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string IP_0 = x[(int)enum_儲架電子紙列表.IP].ObjectToString();
                string IP_1 = y[(int)enum_儲架電子紙列表.IP].ObjectToString();
                string[] IP_0_Array = IP_0.Split('.');
                string[] IP_1_Array = IP_1.Split('.');
                IP_0 = "";
                IP_1 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];

                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];

                    IP_0 += IP_0_Array[i];
                    IP_1 += IP_1_Array[i];
                }
                int cmp = IP_0_Array[2].CompareTo(IP_1_Array[2]);
                if (cmp > 0)
                {
                    return 1;
                }
                else if (cmp < 0)
                {
                    return -1;
                }
                else if (cmp == 0)
                {
                    cmp = IP_0_Array[3].CompareTo(IP_1_Array[3]);
                    if (cmp > 0)
                    {
                        return 1;
                    }
                    else if (cmp < 0)
                    {
                        return -1;
                    }
                    else if (cmp == 0)
                    {
                        return 0;
                    }
                }

                return 0;

            }
        }
        private class ICP_儲架儲位總表 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string IP_0 = x[(int)enum_儲架儲位總表.IP].ObjectToString();
                string IP_1 = y[(int)enum_儲架儲位總表.IP].ObjectToString();
                string[] IP_0_Array = IP_0.Split('.');
                string[] IP_1_Array = IP_1.Split('.');
                IP_0 = "";
                IP_1 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];

                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];

                    IP_0 += IP_0_Array[i];
                    IP_1 += IP_1_Array[i];
                }
                int cmp = IP_0_Array[2].CompareTo(IP_1_Array[2]);
                if (cmp > 0)
                {
                    return 1;
                }
                else if (cmp < 0)
                {
                    return -1;
                }
                else if (cmp == 0)
                {
                    cmp = IP_0_Array[3].CompareTo(IP_1_Array[3]);
                    if (cmp > 0)
                    {
                        return 1;
                    }
                    else if (cmp < 0)
                    {
                        return -1;
                    }
                    else if (cmp == 0)
                    {
                        return 0;
                    }
                }

                return 0;

            }
        }
    }
}
