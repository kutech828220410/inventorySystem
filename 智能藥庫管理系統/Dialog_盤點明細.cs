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
using SQLUI;
using HIS_DB_Lib;

namespace 智能藥庫系統
{
    public partial class Dialog_盤點明細 : MyDialog
    {
        [EnumDescription("inventory_content")]
        private enum enum_inventory_content
        {
            [Description("GUID,VARCHAR,50,PRIMARY")]
            GUID,
            [Description("藥碼,VARCHAR,50,None")]
            藥碼,
            [Description("藥名,VARCHAR,50,None")]
            藥名,
            [Description("單位,VARCHAR,50,None")]
            單位,
            [Description("盤點量,VARCHAR,50,None")]
            盤點量,
        }
        public inventoryClass.creat Creat = new inventoryClass.creat();
        private string _IC_SN = "";
        public Dialog_盤點明細(string IC_SN)
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));

            Reflection.MakeDoubleBuffered(this, true);
            this._IC_SN = IC_SN;
            this.CancelButton = this.plC_RJ_Button_返回;
            this.Load += Dialog_盤點明細_Load;
            this.LoadFinishedEvent += Dialog_盤點明細_LoadFinishedEvent;
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.rJ_Button_搜尋.MouseDownEvent += RJ_Button_搜尋_MouseDownEvent;
            this.plC_RJ_Button_刪除.MouseDownEvent += PlC_RJ_Button_刪除_MouseDownEvent;

            Table table = new Table(new enum_inventory_content());

            this.sqL_DataGridView_盤點明細.InitEx(table);
            this.sqL_DataGridView_盤點明細.RowsHeight = 50;
            this.sqL_DataGridView_盤點明細.Set_ColumnVisible(false, new enum_inventory_content().GetEnumNames());
            this.sqL_DataGridView_盤點明細.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_inventory_content.藥碼);
            this.sqL_DataGridView_盤點明細.Set_ColumnWidth(600, DataGridViewContentAlignment.MiddleLeft, enum_inventory_content.藥名);
            this.sqL_DataGridView_盤點明細.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_inventory_content.單位);
            this.sqL_DataGridView_盤點明細.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleLeft, enum_inventory_content.盤點量);

        }

  


        #region Fucntion
        private void Function_RereshUI(List<inventoryClass.content> contents)
        {
            List<object[]> list_value = new List<object[]>();
            form.Invoke(new Action(delegate
            {
                for (int i = 0; i < contents.Count; i++)
                {
                    object[] value = new object[new enum_inventory_content().GetLength()];
                    value[(int)enum_inventory_content.GUID] = contents[i].GUID;
                    value[(int)enum_inventory_content.藥碼] = contents[i].藥品碼;
                    value[(int)enum_inventory_content.藥名] = contents[i].藥品名稱;
                    value[(int)enum_inventory_content.單位] = contents[i].包裝單位;
                    value[(int)enum_inventory_content.盤點量] = contents[i].盤點量;
                    list_value.Add(value);
                }
            }));
            sqL_DataGridView_盤點明細.RefreshGrid(list_value);
        }


        #endregion
        #region Event
        private void Dialog_盤點明細_Load(object sender, EventArgs e)
        {

        }
        private void Dialog_盤點明細_LoadFinishedEvent(EventArgs e)
        {
            this.comboBox_搜尋條件.SelectedIndex = 0;
            LoadingForm.ShowLoadingForm();

            inventoryClass.creat creat = inventoryClass.creat_get_by_IC_SN(Main_Form.API_Server, _IC_SN);
            if (creat == null)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("找無盤點資料", 2000);
                dialog_AlarmForm.ShowDialog();
                this.Close();
                return;
            }
            this.Creat = creat;
            RJ_Button_搜尋_MouseDownEvent(null);
            //Function_RereshUI(creat.Contents);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                List<inventoryClass.content> contents = new List<inventoryClass.content>();
                List<inventoryClass.content> contents_buf = new List<inventoryClass.content>();
                string text = comboBox_搜尋條件.GetComboBoxText();
                string serch_value = comboBox_搜尋內容.GetComboBoxText();
                if (text == "全部顯示")
                {

                    contents = this.Creat.Contents;
                }
                if (text == "藥碼")
                {
                    if (serch_value.StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("搜尋內容空白");
                        return;
                    }
                    contents = (from temp in this.Creat.Contents
                                where temp.藥品碼.ToUpper().Contains(serch_value.ToUpper())
                                select temp).ToList();
                }
                if (text == "藥名")
                {
                    if (serch_value.StringIsEmpty())
                    {
                        MyMessageBox.ShowDialog("搜尋內容空白");
                        return;
                    }
                    contents = (from temp in this.Creat.Contents
                                where temp.藥品名稱.ToUpper().Contains(serch_value.ToUpper())
                                select temp).ToList();
                }
                if(this.checkBox_已盤.Checked)
                {
                    contents_buf.LockAdd(contents = (from temp in this.Creat.Contents
                                                     where temp.Sub_content.Count > 0
                                                     select temp).ToList());
                }
                if (this.checkBox_未盤.Checked)
                {
                    contents_buf.LockAdd(contents = (from temp in this.Creat.Contents
                                                     where temp.Sub_content.Count == 0
                                                     select temp).ToList());
                }
                Function_RereshUI(contents_buf);
                if (contents.Count == 0)
                {
                    MyMessageBox.ShowDialog("查無資料");
                    return;
                }
            }));
          
        }
        private void PlC_RJ_Button_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_盤點明細.Get_All_Checked_RowsValues();

            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料");
                return;
            }



            if (MyMessageBox.ShowDialog($"確認刪除,<{list_value.Count}>筆資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            LoadingForm.ShowLoadingForm();
            List<inventoryClass.content> contents = new List<inventoryClass.content>();
            for (int i = 0; i < list_value.Count; i++)
            {
                inventoryClass.content content = new inventoryClass.content();

                content.GUID = list_value[i][(int)enum_inventory_content.GUID].ObjectToString();
                contents.Add(content);
            }


            inventoryClass.contents_delete_by_GUID(Main_Form.API_Server, contents);
            inventoryClass.creat creat = inventoryClass.creat_get_by_IC_SN(Main_Form.API_Server, _IC_SN);
            this.Creat = creat;
            Function_RereshUI(creat.Contents);
            LoadingForm.CloseLoadingForm();
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
 
        #endregion
        public class ICP_content : IComparer<inventoryClass.content>
        {
            public int Compare(inventoryClass.content x, inventoryClass.content y)
            {
                // 檢查 x 和 y 的 Sub_content 是否為 null，並取得其 Count
                int xSubContentCount = x.Sub_content != null ? x.Sub_content.Count : 0;
                int ySubContentCount = y.Sub_content != null ? y.Sub_content.Count : 0;

                // 先以 Sub_content.Count 排序
                int result = ySubContentCount.CompareTo(xSubContentCount);
                if (result == 0)
                {
                    // 若 Sub_content.Count 相等，則以藥碼排序
                    result = x.藥品碼.CompareTo(y.藥品碼);
                }
                return result;

            }
        }
    }
}
