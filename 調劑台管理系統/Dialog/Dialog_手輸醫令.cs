using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLUI;
using MyUI;
using Basic;
using HIS_DB_Lib;

namespace 調劑台管理系統
{
    public enum enum_選擇藥品
    {
        GUID,
        藥品碼,
        藥品名稱,
        交易量,
    }
    public partial class Dialog_手輸醫令 : MyDialog
    {
        private transactionsClass _transactionsClass = new transactionsClass();
        public transactionsClass transactionsClass
        {
            set
            {
                _transactionsClass = value;
                this.Invoke(new Action(delegate 
                {
                    rJ_Lable_病歷號.Text = $"病歷號:{_transactionsClass.病歷號}";
                    rJ_Lable_病人姓名.Text = $"病人姓名:{_transactionsClass.病人姓名}";
                    rJ_Lable_領藥號.Text = $"領藥號:{_transactionsClass.領藥號}";
                    rJ_Lable_病房號.Text = $"病房號:{_transactionsClass.病房號}";
                }));
            }
            get
            {
                return _transactionsClass;
            }
        }
        public static bool IsShown = false;
        public enum enum_狀態
        {
            領藥,
            退藥,
        }
        private MyThread MyThread_program;
        public List<object[]> Value = new List<object[]>();
        private SQL_DataGridView sQL_DataGridView_藥品資料_buf;
        private Main_Form Main_Form_buf;
        private enum_狀態 _enum_狀態;
        public Dialog_手輸醫令(Main_Form Main_Form, SQL_DataGridView sQL_DataGridView_藥品資料 , enum_狀態 enum_狀態)
        {
            InitializeComponent();

            this.FormClosing += Dialog_手動作業_FormClosing;
            this.sQL_DataGridView_藥品資料_buf = sQL_DataGridView_藥品資料;
            this.Main_Form_buf = Main_Form;
            this._enum_狀態 = enum_狀態;
        }

     

        private void Dialog_手動作業_Load(object sender, EventArgs e)
        {
            Table table = new Table("");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, Table.IndexType.None);
            table.AddColumnList("藥品碼", Table.StringType.VARCHAR, Table.IndexType.None);
            table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, Table.IndexType.None);
            table.AddColumnList("交易量", Table.StringType.VARCHAR, Table.IndexType.None);
            this.sqL_DataGridView_選擇藥品.Init(table);
            this.sqL_DataGridView_選擇藥品.Set_ColumnVisible(false, new enum_選擇藥品().GetEnumNames());
            this.sqL_DataGridView_選擇藥品.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_選擇藥品.藥品碼);
            this.sqL_DataGridView_選擇藥品.Set_ColumnWidth(520, DataGridViewContentAlignment.MiddleLeft, enum_選擇藥品.藥品名稱);
            this.sqL_DataGridView_選擇藥品.Set_ColumnWidth(80, enum_選擇藥品.交易量);
 
            this.sqL_DataGridView_選擇藥品.RowEndEditEvent += SqL_DataGridView_選擇藥品_RowEndEditEvent;
          

            this.sqL_DataGridView_藥品資料.Init(this.sQL_DataGridView_藥品資料_buf);
            this.sqL_DataGridView_藥品資料.Set_ColumnVisible(false, new enum_藥品資料_藥檔資料().GetEnumNames());
            this.sqL_DataGridView_藥品資料.Set_ColumnWidth(350, enum_藥品資料_藥檔資料.藥品名稱);
            this.sqL_DataGridView_藥品資料.Set_ColumnWidth(100, enum_藥品資料_藥檔資料.庫存);
            this.sqL_DataGridView_藥品資料.Set_ColumnVisible(true, enum_藥品資料_藥檔資料.藥品碼, enum_藥品資料_藥檔資料.藥品名稱, enum_藥品資料_藥檔資料.庫存);
            this.sqL_DataGridView_藥品資料.DataGridRowsChangeRefEvent += SqL_DataGridView_藥品資料_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_藥品資料.RowDoubleClickEvent += SqL_DataGridView_藥品資料_RowDoubleClickEvent;

            this.rJ_TextBox_藥品資料_藥品碼.KeyPress += RJ_TextBox_藥品資料_藥品碼_KeyPress;
            this.rJ_TextBox_藥品資料_藥品名稱.KeyPress += RJ_TextBox_藥品資料_藥品名稱_KeyPress;

            this.rJ_Button_藥品資料_藥品碼_搜尋.MouseDownEvent += RJ_Button_藥品資料_藥品碼_搜尋_MouseDownEvent;
            this.rJ_Button_藥品資料_藥品名稱_搜尋.MouseDownEvent += RJ_Button_藥品資料_藥品名稱_搜尋_MouseDownEvent;
            this.rJ_Button_藥品資料_選擇藥品.MouseDownEvent += RJ_Button_藥品資料_選擇藥品_MouseDownEvent;
            this.rJ_Button_處方資訊填寫.MouseDownEvent += RJ_Button_處方資訊填寫_MouseDownEvent;

            this.rJ_Button1.MouseDownEvent += RJ_Button_選擇藥品_刪除選取資料_MouseDownEvent;

            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
            this.rJ_Button_退出.MouseDownEvent += RJ_Button_退出_MouseDownEvent;
            this.LoadFinishedEvent += Dialog_手輸醫令_LoadFinishedEvent;
            this.Invoke(new Action(delegate
            {
                this.rJ_Lable_領退藥狀態.Text = this._enum_狀態.GetEnumName();
                if(this._enum_狀態 == enum_狀態.領藥)
                {
                    this.rJ_Lable_領退藥狀態.BackColor = Color.Green;
                }
                else if (this._enum_狀態 == enum_狀態.退藥)
                {
                    this.rJ_Lable_領退藥狀態.BackColor = Color.Red;
                }
            }));
            this.sqL_DataGridView_藥品資料.SQL_GetAllRows(true);
            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();
            IsShown = true;

        }

        private void Dialog_手輸醫令_LoadFinishedEvent(EventArgs e)
        {
            Dialog_處方資訊填寫 dialog_處方資訊填寫 = new Dialog_處方資訊填寫(transactionsClass);
            if (dialog_處方資訊填寫.ShowDialog() != DialogResult.Yes) return;
            this.transactionsClass = dialog_處方資訊填寫.transactionsClass;
        }

        private void sub_program()
        {
            string text = "";
            text = Main_Form.Function_ReadBacodeScanner01();
            if (text.StringIsEmpty() == false)
            {
                List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, text);
                if (medClasses.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("條碼查無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                else
                {
                    this.Function_選擇藥品(medClasses[0].藥品碼, medClasses[0].藥品名稱);
                }
            }
            text = Main_Form.Function_ReadBacodeScanner02();
            if (text.StringIsEmpty() == false)
            {
                List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, text);
                if (medClasses.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("條碼查無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                else
                {
                    this.Function_選擇藥品(medClasses[0].藥品碼, medClasses[0].藥品名稱);
                }
            }
            text = Main_Form.Function_ReadBacodeScanner03();
            if (text.StringIsEmpty() == false)
            {
                List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, text);
                if (medClasses.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("條碼查無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                else
                {
                    this.Function_選擇藥品(medClasses[0].藥品碼, medClasses[0].藥品名稱);
                }
            }
            text = Main_Form.Function_ReadBacodeScanner04();
            if (text.StringIsEmpty() == false)
            {
                List<medClass> medClasses = medClass.serch_by_BarCode(Main_Form.API_Server, text);
                if (medClasses.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("條碼查無資料", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                else
                {
                    this.Function_選擇藥品(medClasses[0].藥品碼, medClasses[0].藥品名稱);
                }
            }
        }
        #region Function
        private bool Function_選擇藥品(string 藥碼 ,string 藥名)
        {
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入數量");
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes)
            {
                return false;
            }
            int 交易量 = dialog_NumPannel.Value;
            if (交易量 == 0)
            {
                if (MyMessageBox.ShowDialog("交易量為（0） ,確定選擇此藥品?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes)
                {
                    return false;
                }
            }
            if (_enum_狀態 == enum_狀態.領藥) 交易量 *= -1;
            object[] value = new object[new enum_選擇藥品().GetLength()];
            value[(int)enum_選擇藥品.GUID] = Guid.NewGuid().ToString();
            value[(int)enum_選擇藥品.藥品碼] = 藥碼;
            value[(int)enum_選擇藥品.藥品名稱] = 藥名;
            value[(int)enum_選擇藥品.交易量] = 交易量;
            this.sqL_DataGridView_選擇藥品.AddRow(value, true);
            return true;
        }
        #endregion
        #region Event
        private bool SqL_DataGridView_選擇藥品_RowEndEditEvent(object[] RowValue, int rowIndex, int colIndex, string value)
        {
            this.sqL_DataGridView_選擇藥品.ReplaceExtra(RowValue , true);
            return false;
        }
        private void SqL_DataGridView_藥品資料_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            this.Main_Form_buf.Function_從SQL取得儲位到本地資料();
            Main_Form.commonSapceClasses = Main_Form.Function_取得共用區所有儲位();

            List<object[]> RowsList_buf = new List<object[]>();
            Parallel.ForEach(RowsList, value =>
            {
                string 藥品碼 = value[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                int 本台庫存 = this.Main_Form_buf.Function_從本地資料取得庫存(藥品碼);
                int 共用區庫存 = Main_Form.Function_從共用區取得庫存(藥品碼);
       
            
                if (本台庫存 != -999 || 共用區庫存 != -999)
                {
                    if (共用區庫存 == -999)
                    {
                        共用區庫存 = 0;
                    }
                    if (本台庫存 == -999)
                    {
                        本台庫存 = 0;
                    }
                    value[(int)enum_藥品資料_藥檔資料.庫存] = 本台庫存 + 共用區庫存;
                    RowsList_buf.LockAdd(value);
                }
     
            });
            RowsList_buf.Sort(new ICP_藥品資料());
            RowsList = RowsList_buf;
        }
        private void SqL_DataGridView_藥品資料_RowDoubleClickEvent(object[] RowValue)
        {
            this.RJ_Button_藥品資料_選擇藥品_MouseDownEvent(null);
        }
        private void RJ_Button_處方資訊填寫_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_處方資訊填寫 dialog_處方資訊填寫 = new Dialog_處方資訊填寫(transactionsClass);
            if (dialog_處方資訊填寫.ShowDialog() != DialogResult.Yes) return;
            this.transactionsClass = dialog_處方資訊填寫.transactionsClass;

        }
        private void RJ_Button_選擇藥品_刪除選取資料_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_選擇藥品.Get_All_Select_RowsValues();
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            if (MyMessageBox.ShowDialog($"確定刪除{list_value.Count}筆資料?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes)
            {
                return;
            }
            this.sqL_DataGridView_選擇藥品.DeleteExtra(list_value, true);
        }
        private void RJ_Button_藥品資料_選擇藥品_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料.Get_All_Select_RowsValues();
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料!");
                return;
            }
            string 藥品碼 = list_value[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            string 藥品名稱 = list_value[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
            this.Function_選擇藥品(藥品碼, 藥品名稱);
        }
        private void RJ_TextBox_藥品資料_藥品名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                this.RJ_Button_藥品資料_藥品名稱_搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_TextBox_藥品資料_藥品碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.RJ_Button_藥品資料_藥品碼_搜尋_MouseDownEvent(null);
            }
        }
        private void RJ_Button_藥品資料_藥品名稱_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.rJ_TextBox_藥品資料_藥品名稱.Texts.StringIsEmpty())
            {
                this.sqL_DataGridView_藥品資料.SQL_GetAllRows(true);
                return;
            }
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_value = this.sqL_DataGridView_藥品資料.SQL_GetAllRows(false);
            Console.Write($"從SQL取得藥品資料,耗時{myTimer.ToString()}ms\n");
            list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品名稱, this.rJ_TextBox_藥品資料_藥品名稱.Texts , true);
            Console.Write($"搜尋藥品資料,耗時{myTimer.ToString()}ms\n");
            this.sqL_DataGridView_藥品資料.RefreshGrid(list_value);
            Console.Write($"更新藥品資料,耗時{myTimer.ToString()}ms\n");
        }
        private void RJ_Button_藥品資料_藥品碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.rJ_TextBox_藥品資料_藥品碼.Texts.StringIsEmpty())
            {
                this.sqL_DataGridView_藥品資料.SQL_GetAllRows(true);
                return;
            }
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_value = this.sqL_DataGridView_藥品資料.SQL_GetAllRows(false);
            Console.Write($"從SQL取得藥品資料,耗時{myTimer.ToString()}ms\n");
            list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品碼, this.rJ_TextBox_藥品資料_藥品碼.Texts);
            Console.Write($"搜尋藥品資料,耗時{myTimer.ToString()}ms\n");
            this.sqL_DataGridView_藥品資料.RefreshGrid(list_value);
            Console.Write($"更新藥品資料,耗時{myTimer.ToString()}ms\n");
        }
        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                this.Value = this.sqL_DataGridView_選擇藥品.GetAllRows();
                this.Close();
            }));
        }
        private void RJ_Button_退出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
        private void Dialog_手動作業_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MyThread_program != null)
            {
                MyThread_program.Abort();
                MyThread_program = null;
            }
            IsShown = false;
        }
        #endregion

        public class ICP_藥品資料 : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                int 庫存_0 = x[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString().StringToInt32();
                int 庫存_1 = y[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString().StringToInt32();
                return 庫存_1.CompareTo(庫存_0);

            }
        }

    }
}
