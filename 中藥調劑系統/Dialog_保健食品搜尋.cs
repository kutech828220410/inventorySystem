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
    public partial class Dialog_保健食品搜尋 : MyDialog
    {
        static public bool IsShown = false;
        private List<string> codes = new List<string>();
        private MyThread myThread = new MyThread();
        public enum enum_處方內容
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("藥碼,VARCHAR,15,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("調劑量,VARCHAR,15,NONE")]
            調劑量,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
            [Description("頻次,VARCHAR,15,NONE")]
            頻次,

        }

        public Dialog_保健食品搜尋()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); })); 

            this.LoadFinishedEvent += Dialog_保健食品搜尋_LoadFinishedEvent;
            this.FormClosing += Dialog_保健食品搜尋_FormClosing;
        }

        private void Dialog_保健食品搜尋_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (codes.Count > 0)
            {
                Main_Form.Function_儲位亮燈(codes, Color.Black, true);
            }
            myThread.Abort();
            myThread = null;
            IsShown = false;
        }

        private void Dialog_保健食品搜尋_LoadFinishedEvent(EventArgs e)
        {
            Table table_處方內容 = new Table(new enum_醫囑資料());
            this.sqL_DataGridView_處方內容.RowsHeight = 50;
            this.sqL_DataGridView_處方內容.Init(table_處方內容);
            this.sqL_DataGridView_處方內容.Set_ColumnVisible(false, new enum_醫囑資料().GetEnumNames());
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(170, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(650, DataGridViewContentAlignment.MiddleLeft, enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleRight, enum_醫囑資料.交易量);
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleRight, enum_醫囑資料.劑量單位);
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_醫囑資料.頻次);

            this.sqL_DataGridView_處方內容.Set_ColumnText("藥碼", enum_醫囑資料.藥品碼);
            this.sqL_DataGridView_處方內容.Set_ColumnText("藥名", enum_醫囑資料.藥品名稱);
            this.sqL_DataGridView_處方內容.Set_ColumnText("單位", enum_醫囑資料.劑量單位);
            this.sqL_DataGridView_處方內容.RefreshGrid();

            myThread = new MyThread();
            myThread.AutoRun(true);
            myThread.SetSleepTime(10);
            myThread.Add_Method(sub_program);
            myThread.Trigger();

            IsShown = true;
        }
        private void sub_program()
        {
            string text = Main_Form.Function_ReadBacodeScanner01();
            if (text.StringIsEmpty() == false)
            {
                List<OrderClass> orderClasses = Main_Form.Function_西藥醫令資料_API呼叫(Main_Form.dBConfigClass.OrderApiURL, text);
                if (orderClasses != null)
                {
                    if(codes.Count > 0)
                    {
                        Main_Form.Function_儲位亮燈(codes, Color.Black);
                    }
                    List<object[]> list_orders = orderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
                    this.sqL_DataGridView_處方內容.RefreshGrid(list_orders);

                    codes = (from temp in orderClasses
                             select temp.藥品碼).Distinct().ToList();
                    if (codes.Count > 0)
                    {
                        Main_Form.Function_儲位亮燈(codes, Color.Green , true);
                    }

                }
            }
            
           
        }
    }
}
