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
    public partial class Dialog_儲位管理 : MyDialog
    {
        private List<Storage> storages = new List<Storage>();
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

  

  

        private void RefreshUI()
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
            list_value.Sort(new ICP_儲架電子紙_藥品資料());
            this.sqL_DataGridView_儲架電子紙列表.RefreshGrid(list_value);
        }
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
        private void SqL_DataGridView_儲架電子紙_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {

        }
        private void Dialog_儲位管理_Load(object sender, EventArgs e)
        {
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
            this.sqL_DataGridView_儲架電子紙列表.Init(table_儲架電子紙列表);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnVisible(false, new enum_儲架電子紙列表().GetEnumNames());
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.IP);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.藥碼);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(600, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.藥名);
            this.sqL_DataGridView_儲架電子紙列表.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_儲架電子紙列表.區域);
            this.sqL_DataGridView_儲架電子紙列表.RowEnterEvent += SqL_DataGridView_儲架電子紙列表_RowEnterEvent;

            this.rJ_Button_儲架電子紙_藥品資料_填入儲位.MouseDownEvent += RJ_Button_儲架電子紙_藥品資料_填入儲位_MouseDownEvent;

            this.rJ_Button_儲架電子紙_面板亮燈.MouseDownEvent += RJ_Button_儲架電子紙_面板亮燈_MouseDownEvent;
            this.rJ_Button_儲架電子紙_清除燈號.MouseDownEvent += RJ_Button_儲架電子紙_清除燈號_MouseDownEvent;
            this.rJ_Button_儲架電子紙_複製格式.MouseDownEvent += RJ_Button_儲架電子紙_複製格式_MouseDownEvent;
            this.rJ_Button_儲架電子紙_清除儲位.MouseDownEvent += RJ_Button_儲架電子紙_清除儲位_MouseDownEvent;
            this.rJ_Button_儲架電子紙_面板刷新.MouseDownEvent += RJ_Button_儲架電子紙_面板刷新_MouseDownEvent;
            this.rJ_Button_儲架電子紙_貼上格式.MouseDownEvent += RJ_Button_儲架電子紙_貼上格式_MouseDownEvent;

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

            RefreshUI();
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

        private void SqL_DataGridView_儲架電子紙列表_RowEnterEvent(object[] RowValue)
        {
            string IP = RowValue[(int)enum_儲架電子紙列表.IP].ObjectToString();

            Storage storage = Main_Form._storageUI_EPD_266.SQL_GetStorage(IP);

            if (storage == null) return;
            rJ_TextBox_儲架電子紙_儲位內容_藥品名稱.Text = storage.Name;
            rJ_TextBox_儲架電子紙_儲位內容_藥品學名.Text = storage.Scientific_Name;
            rJ_TextBox_儲架電子紙_儲位內容_中文名稱.Text = storage.ChineseName;
            rJ_TextBox_儲架電子紙_儲位內容_藥品碼.Text = storage.Code;
            rJ_TextBox_儲架電子紙_儲位內容_包裝單位.Text = storage.Package;
            rJ_TextBox_儲架電子紙_儲位內容_總庫存.Text = storage.Inventory;

          
            epD_290_Pannel.DrawToPictureBox(storage);


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
        private void RJ_Button_儲架電子紙_清除儲位_MouseDownEvent(MouseEventArgs mevent)
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
            RefreshUI();

            MyMessageBox.ShowDialog("清除完成");
        }
        private void RJ_Button_儲架電子紙_複製格式_MouseDownEvent(MouseEventArgs mevent)
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
        private void RJ_Button_儲架電子紙_貼上格式_MouseDownEvent(MouseEventArgs mevent)
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
            RefreshUI();

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

            Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("亮燈完成", 1000, Color.YellowGreen);
            dialog_AlarmForm.ShowDialog();

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

            Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("滅燈完成", 1000, Color.YellowGreen);
            dialog_AlarmForm.ShowDialog();
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
            RefreshUI();
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

        private class ICP_儲架電子紙_藥品資料 : IComparer<object[]>
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
    }
}
