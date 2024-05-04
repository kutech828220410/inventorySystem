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
namespace 中藥調劑系統
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
 

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MyMessageBox.音效 = false;
            this.plC_UI_Init.音效 = false;

            plC_UI_Init.Run(this.FindForm(), this.lowerMachine_Panel);
            plC_UI_Init.UI_Finished_Event += PlC_UI_Init_UI_Finished_Event;
        }

        private void PlC_UI_Init_UI_Finished_Event()
        {
            PLC_UI_Init.Set_PLC_ScreenPage(panel_main, this.plC_ScreenPage_main);

            Table table_處方內容 = new Table("");
            table_處方內容.AddColumnList("藥名", Table.StringType.VARCHAR, Table.IndexType.None);
            table_處方內容.AddColumnList("應調", Table.StringType.VARCHAR, Table.IndexType.None);
            table_處方內容.AddColumnList("實調", Table.StringType.VARCHAR, Table.IndexType.None);
            table_處方內容.AddColumnList("天", Table.StringType.VARCHAR, Table.IndexType.None);
            table_處方內容.AddColumnList("庫存", Table.StringType.VARCHAR, Table.IndexType.None);
            table_處方內容.AddColumnList("服用方法", Table.StringType.VARCHAR, Table.IndexType.None);
            this.sqL_DataGridView_處方內容.Init(table_處方內容);
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, "藥名");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "應調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "實調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, "天");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "庫存");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, "服法");

            List<object[]> list_處方內容 = new List<object[]>();
            list_處方內容.Add(new object[] { "補陽還五湯", "35.00", "-", 14, 0, "BIPC" });
            list_處方內容.Add(new object[] { "六味地黃丸", "28.00", "-", 14, 0, "BIPC" });
            list_處方內容.Add(new object[] { "蒼耳散", "28.00", "-", 14, 0, "BIPC" });
            list_處方內容.Add(new object[] { "淡竹葉", "8.40", "-", 14, 0, "BIPC" });
            list_處方內容.Add(new object[] { "黨蔘", "4.20", "-", 14, 0, "BIPC" });
            list_處方內容.Add(new object[] { "牡蠣", "8.40", "-", 14, 0, "BIPC" });
            list_處方內容.Add(new object[] { "紫雲膏", "1.00", "-", 14, 0, "BIPC" });
            this.sqL_DataGridView_處方內容.RefreshGrid(list_處方內容);



            Table table_病患資訊= new Table("");
            table_病患資訊.AddColumnList("領藥號", Table.StringType.VARCHAR, Table.IndexType.None);
            table_病患資訊.AddColumnList("姓名", Table.StringType.VARCHAR, Table.IndexType.None);

            this.sqL_DataGridView_病患資訊.Init(table_病患資訊);
            this.sqL_DataGridView_病患資訊.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, "領藥號");
            this.sqL_DataGridView_病患資訊.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, "姓名");

            List<object[]> list_病患資訊 = new List<object[]>();
            list_病患資訊.Add(new object[] { "C0216", "劉秀婂" });
            list_病患資訊.Add(new object[] { "576", "劉秀婂" });
            list_病患資訊.Add(new object[] { "574", "劉秀婂" });
            list_病患資訊.Add(new object[] { "503", "吳昭芳" });
            list_病患資訊.Add(new object[] { "498", "王成枝" });
            list_病患資訊.Add(new object[] { "457", "林韻茹" });
            this.sqL_DataGridView_病患資訊.RefreshGrid(list_病患資訊);
        }
    }
}
