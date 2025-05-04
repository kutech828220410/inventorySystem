using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUI;
using Basic;
using SQLUI;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using HIS_DB_Lib;
using MyOffice;
using H_Pannel_lib;
namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        static public SQL_DataGridView _sqL_DataGridView_交易記錄查詢;
        private void Program_交易記錄查詢_Init()
        {
            _sqL_DataGridView_交易記錄查詢 = this.sqL_DataGridView_交易記錄查詢;
            Table table = new Table(new enum_consumption());       
            table = transactionsClass.Init(Main_Form.API_Server, Main_Form.ServerName, Main_Form.ServerType);
          
            this.sqL_DataGridView_交易記錄查詢.Init(table);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnVisible(false, new enum_交易記錄查詢資料().GetEnumNames());
        
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.動作);
            //this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.診別);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品碼); 
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.藥品名稱);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.領藥號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.庫存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.交易量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.結存量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleRight, enum_交易記錄查詢資料.盤點量);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.覆核藥師);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作人);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病人姓名);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病房號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(50, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.床號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.病歷號);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.操作時間);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交易記錄查詢資料.開方時間);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.收支原因);
            this.sqL_DataGridView_交易記錄查詢.Set_ColumnWidth(250, DataGridViewContentAlignment.MiddleLeft, enum_交易記錄查詢資料.備註);

            this.sqL_DataGridView_交易記錄查詢.DataGridRefreshEvent += SqL_DataGridView_交易記錄查詢_DataGridRefreshEvent;
            this.sqL_DataGridView_交易記錄查詢.DataGridRowsChangeRefEvent += SqL_DataGridView_交易記錄查詢_DataGridRowsChangeRefEvent;
            this.plC_RJ_Button_交易記錄查詢_顯示全部.MouseDownEvent += PlC_RJ_Button_交易記錄查詢_顯示全部_MouseDownEvent;
            this.plC_RJ_Button_交易記錄查詢_選取資料刪除.MouseDownEvent += PlC_RJ_Button_交易記錄查詢_選取資料刪除_MouseDownEvent;
            this.plC_RJ_Button_交易紀錄查詢_匯出資料.MouseDownEvent += PlC_RJ_Button_交易紀錄查詢_匯出資料_MouseDownEvent;



            this.plC_UI_Init.Add_Method(this.sub_Program_交易記錄查詢);
        }
        private void sub_Program_交易記錄查詢()
        {

        }

        #region Funnction
        static public void Funnction_交易記錄查詢_取得指定藥碼批號期效期(string 藥碼, ref List<string> list_效期, ref List<string> list_批號)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            if (藥碼.StringIsEmpty()) return;
            List<object[]> list_value = _sqL_DataGridView_交易記錄查詢.SQL_GetRows((int)enum_交易記錄查詢資料.藥品碼, 藥碼, false);
            Console.WriteLine($"搜尋藥碼: {藥碼} , {myTimerBasic.ToString()}");
            Funnction_交易記錄查詢_取得指定藥碼批號期效期(list_value, ref list_效期, ref list_批號);
        }
        static public void Funnction_交易記錄查詢_取得指定藥碼批號期效期(List<transactionsClass> transactionsClasses, ref List<string> list_效期, ref List<string> list_批號)
        {
            List<object[]> list_value = transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>();
            Funnction_交易記錄查詢_取得指定藥碼批號期效期(list_value, ref list_效期, ref list_批號);
        }
        static public List<StockClass> Funnction_交易記錄查詢_取得指定藥碼批號期效期(string 藥碼)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            if (藥碼.StringIsEmpty()) return new List<StockClass>();
            List<object[]> list_value = _sqL_DataGridView_交易記錄查詢.SQL_GetRows((int)enum_交易記錄查詢資料.藥品碼, 藥碼, false);
            Console.WriteLine($"搜尋藥碼: {藥碼} , {myTimerBasic.ToString()}");
            return Funnction_交易記錄查詢_取得指定藥碼批號期效期(list_value);
        }
        static public List<StockClass> Funnction_交易記錄查詢_取得指定藥碼批號期效期(List<transactionsClass> transactionsClasses)
        {
            return Funnction_交易記錄查詢_取得指定藥碼批號期效期(transactionsClasses.ClassToSQL<transactionsClass, enum_交易記錄查詢資料>());
        }

        static public List<StockClass> Funnction_交易記錄查詢_取得指定藥碼批號期效期(List<object[]> list_value)
        {
            List<string> list_效期 = new List<string>();
            List<string> list_批號 = new List<string>();

            MyTimerBasic myTimerBasic = new MyTimerBasic();
            List<string> list_效期_buf = new List<string>();
            List<string> list_操作時間 = new List<string>();
            string 備註 = "";
            string 操作時間 = "";
            for (int i = 0; i < list_value.Count; i++)
            {
                備註 = list_value[i][(int)enum_交易記錄查詢資料.備註].ObjectToString();
                備註 = 備註.Replace("[NEW處方]", "");
                備註 = 備註.Replace("[DC處方]", "");
                備註 = 備註.Replace("[NEW]", "");
                備註 = 備註.Replace("[DC]", "");

                string[] temp_ary = 備註.Split('\n');
                for (int k = 0; k < temp_ary.Length; k++)
                {
                    string 效期 = temp_ary[k].GetTextValue("效期");
                    string 批號 = temp_ary[k].GetTextValue("批號");
                    操作時間 = list_value[i][(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString();
                    if (操作時間.StringIsEmpty())
                    {
                        操作時間 = list_value[i][(int)enum_交易記錄查詢資料.操作時間].ObjectToString();
                    }
                    if (效期.StringIsEmpty() == true) continue;
                    list_效期_buf = (from temp in list_效期
                                   where temp == 效期
                                   select temp).ToList();
                    if (list_效期_buf.Count > 0) continue;
                    list_效期.Add(效期);
                    list_批號.Add(批號);
                    list_操作時間.Add(操作時間);
                }
            }
            // 組合效期、批號和操作時間
            List<Tuple<string, string, DateTime>> combinedList = new List<Tuple<string, string, DateTime>>();
            for (int i = 0; i < list_效期.Count; i++)
            {
                combinedList.Add(new Tuple<string, string, DateTime>(list_效期[i], list_批號[i], DateTime.Parse(list_操作時間[i])));
            }

            // 根據操作時間排序
            combinedList.Sort((x, y) => DateTime.Compare(y.Item3, x.Item3));

            // 更新list_效期、list_批號和list_操作時間
            list_效期.Clear();
            list_批號.Clear();
            list_操作時間.Clear();
            foreach (var item in combinedList)
            {
                list_效期.Add(item.Item1);
                list_批號.Add(item.Item2);
                list_操作時間.Add(item.Item3.ToString());
            }

            List<StockClass> stockClasses = new List<StockClass>();
            for (int i = 0; i < list_效期.Count; i++)
            {
                StockClass stockClass = new StockClass();
                stockClass.Validity_period = list_效期[i];
                stockClass.Lot_number = list_批號[i];
                stockClasses.Add(stockClass);
            }
            return stockClasses;
        }
        static public void Funnction_交易記錄查詢_取得指定藥碼批號期效期(List<object[]> list_value, ref List<string> list_效期, ref List<string> list_批號)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            List<string> list_效期_buf = new List<string>();
            List<string> list_操作時間 = new List<string>();
            string 備註 = "";
            string 操作時間 = "";
            for (int i = 0; i < list_value.Count; i++)
            {
                備註 = list_value[i][(int)enum_交易記錄查詢資料.備註].ObjectToString();
                備註 = 備註.Replace("[NEW處方]", "");
                備註 = 備註.Replace("[DC處方]", "");
                string[] temp_ary = 備註.Split('\n');
                for (int k = 0; k < temp_ary.Length; k++)
                {
                    string 效期 = temp_ary[k].GetTextValue("效期");
                    string 批號 = temp_ary[k].GetTextValue("批號");
                    操作時間 = list_value[i][(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString();
                    if (操作時間.StringIsEmpty())
                    {
                        操作時間 = list_value[i][(int)enum_交易記錄查詢資料.操作時間].ObjectToString();
                    }
                    if (效期.StringIsEmpty() == true) continue;
                    list_效期_buf = (from temp in list_效期
                                   where temp == 效期
                                   select temp).ToList();
                    if (list_效期_buf.Count > 0) continue;
                    list_效期.Add(效期);
                    list_批號.Add(批號);
                    list_操作時間.Add(操作時間);
                }
            }
            // 組合效期、批號和操作時間
            List<Tuple<string, string, DateTime>> combinedList = new List<Tuple<string, string, DateTime>>();
            for (int i = 0; i < list_效期.Count; i++)
            {
                combinedList.Add(new Tuple<string, string, DateTime>(list_效期[i], list_批號[i], DateTime.Parse(list_操作時間[i])));
            }

            // 根據操作時間排序
            combinedList.Sort((x, y) => DateTime.Compare(y.Item3, x.Item3));

            // 更新list_效期、list_批號和list_操作時間
            list_效期.Clear();
            list_批號.Clear();
            list_操作時間.Clear();
            foreach (var item in combinedList)
            {
                list_效期.Add(item.Item1);
                list_批號.Add(item.Item2);
                list_操作時間.Add(item.Item3.ToString());
            }
        }

        static public void Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作 enum_交易記錄查詢動作, string 操作人, string 備註)
        {
            if (操作人.StringIsEmpty()) return;
            string GUID = Guid.NewGuid().ToString();
            string 動作 = enum_交易記錄查詢動作.GetEnumName();
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 藥袋序號 = "";
            string 庫存量 = "";
            string 交易量 = "";
            string 結存量 = "";
            string 病人姓名 = "";
            string 病歷號 = "";
            string 操作時間 = DateTime.Now.ToDateTimeString_6();
            string 開方時間 = DateTime.Now.ToDateTimeString_6();
            object[] value = new object[new enum_交易記錄查詢資料().GetLength()];
            value[(int)enum_交易記錄查詢資料.GUID] = GUID;
            value[(int)enum_交易記錄查詢資料.動作] = 動作;
            value[(int)enum_交易記錄查詢資料.藥品碼] = 藥品碼;
            value[(int)enum_交易記錄查詢資料.藥品名稱] = 藥品名稱;
            value[(int)enum_交易記錄查詢資料.藥袋序號] = 藥袋序號;
            value[(int)enum_交易記錄查詢資料.庫存量] = 庫存量;
            value[(int)enum_交易記錄查詢資料.交易量] = 交易量;
            value[(int)enum_交易記錄查詢資料.結存量] = 結存量;
            value[(int)enum_交易記錄查詢資料.操作人] = 操作人;
            value[(int)enum_交易記錄查詢資料.病人姓名] = 病人姓名;
            value[(int)enum_交易記錄查詢資料.病歷號] = 病歷號;
            value[(int)enum_交易記錄查詢資料.操作時間] = 操作時間;
            value[(int)enum_交易記錄查詢資料.開方時間] = 開方時間;
            value[(int)enum_交易記錄查詢資料.備註] = 備註;

            _sqL_DataGridView_交易記錄查詢.SQL_AddRow(value, false);
        }


        #endregion
        #region Event
        private void PlC_RJ_Button_交易紀錄查詢_匯出資料_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
                {
                    DataTable datatable = new DataTable();
                    datatable = sqL_DataGridView_交易記錄查詢.GetDataTable();
                    datatable = datatable.ReorderTable(new enum_交易記錄查詢資料_匯出());
                    string Extension = System.IO.Path.GetExtension(this.saveFileDialog_SaveExcel.FileName);
                    if (Extension == ".txt")
                    {
                        CSVHelper.SaveFile(datatable, this.saveFileDialog_SaveExcel.FileName);
                        MyMessageBox.ShowDialog("匯出完成!");
                    }
                    else if (Extension == ".xlsx")
                    {
                        MyOffice.ExcelClass.NPOI_SaveFile(datatable, this.saveFileDialog_SaveExcel.FileName, (int)enum_交易記錄查詢資料_匯出.庫存量, (int)enum_交易記錄查詢資料_匯出.盤點量, (int)enum_交易記錄查詢資料_匯出.交易量, (int)enum_交易記錄查詢資料_匯出.結存量);
                        MyMessageBox.ShowDialog("匯出完成!");
                    }
                }
            }));
         
        }
        private void PlC_RJ_Button_交易記錄查詢_選取資料刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_交易記錄查詢.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }

            this.sqL_DataGridView_交易記錄查詢.SQL_DeleteExtra(list_value, false);
            this.sqL_DataGridView_交易記錄查詢.DeleteExtra(list_value, true);

        }
        private void SqL_DataGridView_交易記錄查詢_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_交易記錄查詢());
        }
        private void SqL_DataGridView_交易記錄查詢_DataGridRefreshEvent()
        {
            string 動作 = "";
            for (int i = 0; i < this.sqL_DataGridView_交易記錄查詢.dataGridView.Rows.Count; i++)
            {
                動作 = this.sqL_DataGridView_交易記錄查詢.dataGridView.Rows[i].Cells[(int)enum_交易記錄查詢資料.動作].Value.ToString();
                if (動作 == enum_交易記錄查詢動作.人臉識別登入.GetEnumName() 
                    || 動作 == enum_交易記錄查詢動作.一維碼登入.GetEnumName()
                    || 動作 == enum_交易記錄查詢動作.密碼登入.GetEnumName()
                    || 動作 == enum_交易記錄查詢動作.登出.GetEnumName()
                    || 動作 == enum_交易記錄查詢動作.效期庫存異動.GetEnumName()
                    || 動作 == enum_交易記錄查詢動作.操作工程模式.GetEnumName()
                    )
                {
                    this.sqL_DataGridView_交易記錄查詢.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    this.sqL_DataGridView_交易記錄查詢.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void PlC_RJ_Button_交易記錄查詢_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_交易記錄查詢.SQL_GetRowsByBetween((int)enum_交易記錄查詢資料.操作時間, dateTimePicker_交易記錄查詢_操作時間_起始, dateTimePicker_交易記錄查詢_操作時間_結束, false);
            if(this.plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_開方時間.Checked)
            {
                list_value = list_value.GetRowsInDate((int)enum_交易記錄查詢資料.開方時間, dateTimePicker_交易記錄查詢_開方時間_起始, dateTimePicker_交易記錄查詢_開方時間_結束);
            }
            List<List<object[]>> list_list_value_buf = new List<List<object[]>>();
            List<object[]> list_value_buf = new List<object[]>();

            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_管制抽屜.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.管制抽屜開啟.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.管制抽屜關閉.GetEnumName()));
            }

            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_自動過帳.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.自動過帳.GetEnumName()));
            }
            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_調劑作業.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.掃碼領藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.手輸領藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.批次領藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.重複領藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統領藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.盤點校正.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.掃碼退藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.手輸退藥.GetEnumName()));

            }
            if(plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_交班盤點.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.交班對點.GetEnumName()));
            }
            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_收支作業.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統入庫.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統加藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統退藥.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統調入.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統調出.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統撥入.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.系統撥出.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.入庫作業.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.出庫作業.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.撥入作業.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.撥出作業.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.調出作業.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.調入作業.GetEnumName()));
            }
            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_效期庫存異動.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.效期庫存異動.GetEnumName()));

            }
            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_後臺操作.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.操作工程模式.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.交班對點.GetEnumName()));
            }
            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_登入及登出.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.人臉識別登入.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.指紋登入.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.RFID登入.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.一維碼登入.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.密碼登入.GetEnumName()));
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.登出.GetEnumName()));
            }
            if (plC_RJ_ChechBox_交易紀錄查詢_搜尋條件_盤存盈虧.Bool)
            {
                list_list_value_buf.Add(list_value.GetRows((int)enum_交易記錄查詢資料.動作, enum_交易記錄查詢動作.盤存盈虧.GetEnumName()));
      
            }
            for (int i = 0; i < list_list_value_buf.Count; i++)
            {
                foreach (object[] value in list_list_value_buf[i])
                {
                    list_value_buf.Add(value);
                }
            }
            if (textBox_交易記錄查詢_藥品碼.Text.StringIsEmpty() == false) list_value_buf = list_value_buf.GetRowsByLike((int)enum_交易記錄查詢資料.藥品碼, textBox_交易記錄查詢_藥品碼.Text);
            if (textBox_交易記錄查詢_藥品名稱.Text.StringIsEmpty() == false) list_value_buf = list_value_buf.GetRowsByLike((int)enum_交易記錄查詢資料.藥品名稱, textBox_交易記錄查詢_藥品名稱.Text);
            if (textBox_交易記錄查詢_藥袋序號.Text.StringIsEmpty() == false) list_value_buf = list_value_buf.GetRows((int)enum_交易記錄查詢資料.藥袋序號, textBox_交易記錄查詢_藥品碼.Text);
            if (textBox_交易記錄查詢_操作人.Text.StringIsEmpty() == false) list_value_buf = list_value_buf.GetRows((int)enum_交易記錄查詢資料.操作人, textBox_交易記錄查詢_操作人.Text);
            if (textBox_交易記錄查詢_病人姓名.Text.StringIsEmpty() == false) list_value_buf = list_value_buf.GetRows((int)enum_交易記錄查詢資料.病人姓名, textBox_交易記錄查詢_病人姓名.Text);
            if (textBox_交易記錄查詢_病歷號.Text.StringIsEmpty() == false) list_value_buf = list_value_buf.GetRows((int)enum_交易記錄查詢資料.病歷號, textBox_交易記錄查詢_病歷號.Text);
    
           
            this.sqL_DataGridView_交易記錄查詢.RefreshGrid(list_value_buf);
            
            if (list_value_buf.Count == 0)
            {
                MyMessageBox.ShowDialog("查無資料!");
            }
        }

      
        #endregion

        public class ICP_交易記錄查詢 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                DateTime datetime2 = y[(int)enum_交易記錄查詢資料.操作時間].ToDateTimeString_6().StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                if (compare != 0) return compare;
                double 結存量1 = x[(int)enum_交易記錄查詢資料.結存量].StringToDouble();
                double 結存量2 = y[(int)enum_交易記錄查詢資料.結存量].StringToDouble();
                if (結存量1 > 結存量2)
                {
                    return -1;
                }
                else if (結存量1 < 結存量2)
                {
                    return 1;
                }
                else if (結存量1 == 結存量2) return 0;
                return 0;
            }
        }

    }
}
