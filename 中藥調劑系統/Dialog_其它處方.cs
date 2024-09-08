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
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;

namespace 中藥調劑系統
{
    public partial class Dialog_其它處方 : MyDialog
    {
        public List<OrderTClass> Value = new List<OrderTClass>();
        private List<formularyClass> formularyClasses = new List<formularyClass>();
        private List<formularyClass> formularyClasses_seleted;
        public enum enum_套餐選擇
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("名稱,VARCHAR,15,NONE")]
            名稱,
        }
        public enum enum_套餐藥品內容
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("數量,VARCHAR,15,NONE")]
            數量,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
        }
        public Dialog_其它處方()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();
            }));
   
            this.LoadFinishedEvent += Dialog_其它處方_LoadFinishedEvent;
            this.plC_RJ_Button_確認送出.MouseDownEvent += PlC_RJ_Button_確認送出_MouseDownEvent;
        }

    

        private void Dialog_其它處方_LoadFinishedEvent(EventArgs e)
        {
            Table table_套餐選擇 = new Table(new enum_formulary());
            this.sqL_DataGridView_套餐選擇.Init(table_套餐選擇);
            this.sqL_DataGridView_套餐選擇.Set_ColumnVisible(false, new enum_formulary().GetEnumNames());
            this.sqL_DataGridView_套餐選擇.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, "名稱");
            this.sqL_DataGridView_套餐選擇.RowEnterEvent += SqL_DataGridView_套餐選擇_RowEnterEvent;

            Table table_套餐藥品內容 = new Table(new enum_formulary());
            this.sqL_DataGridView_套餐藥品內容.Init(table_套餐藥品內容);
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnVisible(false, new enum_formulary().GetEnumNames());
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, "藥碼");
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, "藥名");
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, "數量");
            this.sqL_DataGridView_套餐藥品內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, "單位");

            //formularyClass.add(Main_Form.API_Server, add_養肝明目飲());
            //formularyClass.add(Main_Form.API_Server, add_防疫正氣茶());
            //formularyClass.add(Main_Form.API_Server, add_桂圓紅棗茶());
            //formularyClass.add(Main_Form.API_Server, add_消脂纖體茶());
            //formularyClass.add(Main_Form.API_Server, add_養氣生脈飲());
            formularyClasses = formularyClass.get_all(Main_Form.API_Server);
            List<formularyClass> formularyClasses_套餐選擇 = formularyClasses
            .GroupBy(f => f.名稱)
            .Select(g => g.First())
            .ToList();
            List<object[]> list_套餐選擇 = formularyClasses_套餐選擇.ClassToSQL<formularyClass, enum_formulary>();
            sqL_DataGridView_套餐選擇.RefreshGrid(list_套餐選擇);
        }

        private void SqL_DataGridView_套餐選擇_RowEnterEvent(object[] RowValue)
        {
            string 名稱 = RowValue[(int)enum_formulary.名稱].ObjectToString();
            List<formularyClass> formularyClasses_buf = (from temp in formularyClasses
                                                         where temp.名稱 == 名稱
                                                         select temp).ToList();

            List<object[]> list_value = formularyClasses_buf.ClassToSQL<formularyClass, enum_formulary>();
            formularyClasses_seleted = list_value.SQLToClass<formularyClass, enum_formulary>();
            sqL_DataGridView_套餐藥品內容.RefreshGrid(list_value);
        }

        private List<formularyClass> add_養肝明目飲()
        {
            List<formularyClass> formularyClasses = new List<formularyClass>();

            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "1", 藥碼 = "C0069-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "2", 藥碼 = "C0106-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "3", 藥碼 = "C0107-1", 數量 = "9", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "4", 藥碼 = "C0008-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "5", 藥碼 = "C0108-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "6", 藥碼 = "C0071-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "7", 藥碼 = "C0109-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養肝明目飲", 批序 = "8", 藥碼 = "C0035-1", 數量 = "6", 備註 = "水量6000ml" });
            return formularyClasses;
        }
        private List<formularyClass> add_防疫正氣茶()
        {
            List<formularyClass> formularyClasses = new List<formularyClass>();

            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "1", 藥碼 = "C0029-1", 數量 = "9", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "2", 藥碼 = "C0033-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "3", 藥碼 = "C0035-1", 數量 = "9", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "4", 藥碼 = "C0055-1", 數量 = "9", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "5", 藥碼 = "C0041-1", 數量 = "9", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "6", 藥碼 = "C0038-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "7", 藥碼 = "C0087-1", 數量 = "10", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "防疫正氣茶", 批序 = "8", 藥碼 = "C0068-1", 數量 = "6", 備註 = "水量6000ml" });
            return formularyClasses;
        }
        private List<formularyClass> add_桂圓紅棗茶()
        {
            List<formularyClass> formularyClasses = new List<formularyClass>();

            formularyClasses.Add(new formularyClass { 名稱 = "桂圓紅棗茶", 批序 = "1", 藥碼 = "C0105-1", 數量 = "36", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "桂圓紅棗茶", 批序 = "2", 藥碼 = "C0087-1", 數量 = "84", 備註 = "水量6000ml" });
            return formularyClasses;
        }
        private List<formularyClass> add_消脂纖體茶()
        {
            List<formularyClass> formularyClasses = new List<formularyClass>();

            formularyClasses.Add(new formularyClass { 名稱 = "消脂纖體茶", 批序 = "1", 藥碼 = "C0029-1", 數量 = "12", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "消脂纖體茶", 批序 = "2", 藥碼 = "C0036-1", 數量 = "9", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "消脂纖體茶", 批序 = "3", 藥碼 = "C0054-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "消脂纖體茶", 批序 = "4", 藥碼 = "C0075-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "消脂纖體茶", 批序 = "5", 藥碼 = "C0111-1", 數量 = "9", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "消脂纖體茶", 批序 = "6", 藥碼 = "C0110-1", 數量 = "6", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "消脂纖體茶", 批序 = "7", 藥碼 = "C0068-1", 數量 = "4.5", 備註 = "水量6000ml" });
            return formularyClasses;
        }
        private List<formularyClass> add_養氣生脈飲()
        {
            List<formularyClass> formularyClasses = new List<formularyClass>();

            formularyClasses.Add(new formularyClass { 名稱 = "養氣生脈飲", 批序 = "1", 藥碼 = "C0112-1", 數量 = "12", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養氣生脈飲", 批序 = "2", 藥碼 = "C0108-1", 數量 = "18", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養氣生脈飲", 批序 = "3", 藥碼 = "C0005-1", 數量 = "0.46", 備註 = "水量6000ml" });
            formularyClasses.Add(new formularyClass { 名稱 = "養氣生脈飲", 批序 = "4", 藥碼 = "C0043-1", 數量 = "6", 備註 = "水量6000ml" });
            return formularyClasses;
        }

        private void PlC_RJ_Button_確認送出_MouseDownEvent(MouseEventArgs mevent)
        {
            if(formularyClasses_seleted.Count ==0)
            {
                MyMessageBox.ShowDialog("未選取處方集");
                return;
            }
            if (MyMessageBox.ShowDialog("是否開始調劑處方集?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;


            string 頻次 = "ASORDER";
            string 天數 = "1";
            string 領藥號 = $"M{DateTime.Now.Hour.ToString("00")}{DateTime.Now.Minute.ToString("00")}{DateTime.Now.Second.ToString("00")}";

            List<OrderTClass> orderTClasses = new List<OrderTClass>();
            orderTClasses.Add(new OrderTClass() {
                批序 = "0",
                PRI_KEY = $"M{DateTime.Now.ToDateTimeString_6()}",
                藥品碼 = "CM062101",
                藥品名稱 = "水劑處方代製",
                交易量 = "0",             
                開方日期 = DateTime.Now.ToDateTimeString_6(),
                結方日期 = DateTime.Now.ToDateTimeString_6(),
                展藥時間 = DateTime.Now.ToDateTimeString_6(),
                領藥號 = 領藥號,
                頻次 = 頻次,
                天數 = 天數,
                病人姓名 = 領藥號,
                病歷號 = 領藥號,
            }) ;

            for (int i = 0; i < formularyClasses_seleted.Count; i++)
            {
                OrderTClass orderTClass = new OrderTClass();
                orderTClasses.Add(new OrderTClass() { 
                    批序 = $"{i + 1}",
                    PRI_KEY = $"M{DateTime.Now.ToDateTimeString_6()}",
                    藥品碼 = formularyClasses_seleted[i].藥碼,
                    藥品名稱 = formularyClasses_seleted[i].藥名,
                    劑量單位 = formularyClasses_seleted[i].單位,
                    交易量 = formularyClasses_seleted[i].數量,
                    開方日期 = DateTime.Now.ToDateTimeString_6(),
                    結方日期 = DateTime.Now.ToDateTimeString_6(),
                    展藥時間 = DateTime.Now.ToDateTimeString_6(),
                    領藥號 = 領藥號,
                    頻次 = 頻次,
                    天數 = 天數,
                    病人姓名 = 領藥號,
                    病歷號 = 領藥號,
                });

            }
            Value = orderTClasses;
            LoadingForm.ShowLoadingForm();
            OrderTClass.add(Main_Form.API_Server, orderTClasses);
            LoadingForm.CloseLoadingForm();
            MyMessageBox.ShowDialog($"新增處方完成");
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

    }
}
