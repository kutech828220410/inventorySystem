using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;
using MyOffice;
using dBASE.NET;
using System.Data.SQLite;
using System.Data.OleDb;
using System.Data.Odbc;
using System.IO;
namespace TestForm
{
    public partial class Form1 : Form
    {
        private DragDropListBox dragDropListBox;

        Image image = null;
        public Form1()
        {
            InitializeComponent();
            // 初始化 DragDropListBox
            dragDropListBox = new DragDropListBox();
            dragDropListBox.Size = new Size(300, 200);
            dragDropListBox.Location = new Point(20, 20);
            dragDropListBox.Font = new Font("標楷體", 18);
            dragDropListBox.ItemHeight = 40;
            dragDropListBox.Dock = DockStyle.Fill;
            panel1.Controls.Add(dragDropListBox);

            // 建立並繫結 IndexedDictionary
            IndexedDictionary<string> drugDict = new IndexedDictionary<string>();
            drugDict.Add("藥品代碼 A123-普拿疼", "普拿疼");
            drugDict.Add("藥品代碼 B456-阿斯匹靈", "阿斯匹靈");
            drugDict.Add("藥品代碼 C789-克痰靈", "克痰靈");
            drugDict.Add("藥品代碼 D321-維他命C", "維他命C");
            drugDict.Add("藥品代碼 E654-胃散", "胃散");
            drugDict.Add("藥品代碼 F987-抗生素", "抗生素");
            drugDict.Add("藥品代碼 G111-咳嗽糖漿", "咳嗽糖漿");
            drugDict.Add("藥品代碼 H222-降血壓藥", "降血壓藥");
            drugDict.Add("藥品代碼 I333-消炎藥", "消炎藥");
            drugDict.Add("藥品代碼 J444-鎮靜劑", "鎮靜劑");

            // 綁定至 DragDropListBox
            dragDropListBox.Bind(drugDict);
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            $"盤點量與庫存不符,請確認是否正確".PlayGooleVoiceAsync("http://220.135.128.247:4433");

            List<consumptionClass> consumptionClasses =  consumptionClass.serch_by_ST_END("http://220.135.128.247:4433","A6","調劑台",DateTime.Now.AddMonths(-1),DateTime.Now.GetEndDate(), "TEST");
        }

        private OleDbConnection conn;

        private void Button_call_api_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Driver={Microsoft Paradox Driver (*.db )};Fil=Paradox 4.x;DefaultDir=C:\0.醫院資料\衛福部立玉里醫院\DB;";

            try
            {
                using (OdbcConnection ParadoxConn = new OdbcConnection(ConnectionString))
                {
                    ParadoxConn.Open();

                    // 查詢資料
                    OdbcDataAdapter da = new OdbcDataAdapter("SELECT * FROM BED35", ParadoxConn);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    // 將資料顯示在 DataGridView 上
                    dataGridView.DataSource = table;
                    // 列出欄位名稱
                    Console.WriteLine("欄位名稱:");

                    // 格式化列標題
                    foreach (DataColumn column in table.Columns)
                    {
                        Console.Write($"{column.ColumnName,-20}"); // 固定欄位寬度 20
                    }
                    Console.WriteLine();

                    // 分隔線
                    Console.WriteLine(new string('-', table.Columns.Count * 20));

                    // 顯示資料內容，轉換為 Big5 編碼
                    Console.WriteLine("\n資料內容 (Big5 編碼):");
                    foreach (DataRow row in table.Rows)
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            // 原始文字
                            string originalValue = row[i]?.ToString();

                            // 將文字從 ANSI (Big5) 轉換為 UTF-8
                            string convertedValue = ConvertEncoding(originalValue);

                            // 將轉換後的文字寫回 DataRow
                            row[i] = convertedValue;

                            // 格式化輸出到 Console（可選）
                            Console.Write($"{convertedValue,-20}"); // 固定欄位寬度 20
                        }
                        Console.WriteLine(); // 換行顯示下一列
                    }
                }
                dataGridView.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }
        }

        static string ConvertEncoding(string strSource)
        {
            byte[] arrSource = Encoding.Unicode.GetBytes(strSource);
            byte[] arrDest = new byte[arrSource.Length / 2];
            for (int i = 0; i < arrSource.Length; i = i + 2)
            {
                arrDest[i / 2] = arrSource[i];
            }
            return Encoding.Default.GetString(arrDest);
        }
  
        private void Button_LoadImage_Click(object sender, EventArgs e)
        {
              //"batch_num_coord": "1909,967;2224,967;2224,1020;1909,1020",
              //"cht_name_coord": "35,1023;753,1023;753,1067;35,1067",
              //"expirydate_coord": "2410,967;2637,967;2637,1023;2410,1023",
              //"po_num_coord": "1518,683;2290,683;2290,737;1518,737",
              //"qty_coord": "1301,960;1392,960;1392,1020;1301,1020",
              //"name_coord": "32,967;621,967;621,1011;32,1011",

            if (this.openFileDialog.ShowDialog() != DialogResult.OK) return;

            image = Bitmap.FromFile(this.openFileDialog.FileName);

            string base64 = image.ImageToBase64();
            base64 = $"data:image/jpeg;base64,{base64}";
            pictureBox1.Image = image;


        }
        private void Button_loadBase64_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() != DialogResult.OK) return;

            string base64 = File.ReadAllText(this.openFileDialog.FileName);
            base64 = base64.Replace("data:image/jpeg;base64,", "");                                
            byte[] imageBytes = Convert.FromBase64String(base64);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                image = Image.FromStream(ms);
            }
            pictureBox1.Image = image;
        }
    }
}
