using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;
using HIS_DB_Lib;


namespace Hospital_Call_Light_System
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public enum enum_圖片列表
        {
            GUID,
            代碼,
            bytes,
            停留秒數,
        }

        private void Program_圖片上傳_Init()
        {
            Table table = new Table("image_list");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("序號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("bytes", Table.StringType.LONGTEXT, 65535, Table.IndexType.None);
            table.AddColumnList("停留秒數", Table.StringType.VARCHAR, 20, Table.IndexType.None);

            SQL_DataGridView.SQL_Set_Properties(sqL_DataGridView_圖片列表, dBConfigClass.DB_Basic);
            this.sqL_DataGridView_圖片列表.Init(table);
            if (this.sqL_DataGridView_圖片列表.SQL_IsTableCreat() == false) sqL_DataGridView_圖片列表.SQL_CreateTable();
            else sqL_DataGridView_圖片列表.SQL_CheckAllColumnName(true);

            Function_圖片上傳_圖片列表檢查();

            comboBox_圖片上傳_序號.Items.Clear();
            List<object[]> list_value = this.sqL_DataGridView_圖片列表.SQL_GetAllRows(false);
            list_value.Sort(new Icp_圖片列表());
            List<string> list_str = (from temp in list_value
                                     select temp[(int)enum_圖片列表.代碼].ObjectToString()).ToList();
            comboBox_圖片上傳_序號.DataSource = list_str.ToArray();

            this.button_圖片上傳_從本機讀取圖片.Click += Button_圖片上傳_從本機讀取圖片_Click;
            this.button_圖片上傳_圖片上傳至資料庫.Click += Button_圖片上傳_圖片上傳至資料庫_Click;
            this.button_上傳圖片_從資料庫讀取圖片.Click += Button_上傳圖片_從資料庫讀取圖片_Click;
            plC_UI_Init.Add_Method(Program_圖片上傳);
        }
  
        private void Program_圖片上傳()
        {
      

        }


        #region Function

        private void Function_圖片上傳_圖片列表檢查()
        {
            List<object[]> list_圖片列表 = this.sqL_DataGridView_圖片列表.SQL_GetAllRows(false);
            List<object[]> list_圖片列表_buf = new List<object[]>();
            List<object[]> list_圖片列表_add = new List<object[]>();
            List<object[]> list_圖片列表_delete = new List<object[]>();
            List<object[]> list_圖片列表_replace = new List<object[]>();

            for (int i = 0; i < list_圖片列表.Count; i++)
            {
                int 代碼 = list_圖片列表[i][(int)enum_圖片列表.代碼].StringToInt32();
                if (代碼 <= 0 || 代碼 > 9)
                {
                    list_圖片列表_delete.Add(list_圖片列表[i]);
                }
            }
            for (int i = 1; i < 10; i++)
            {
                list_圖片列表_buf = list_圖片列表.GetRows((int)enum_圖片列表.代碼, i.ToString());
                if (list_圖片列表_buf.Count == 0)
                {
                    object[] value = new object[new enum_圖片列表().GetLength()];

                    value[(int)enum_圖片列表.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_圖片列表.代碼] = i.ToString();


                    list_圖片列表_add.Add(value);
                }
                else
                {
                    object[] value = list_圖片列表_buf[0];

                    list_圖片列表_replace.Add(value);



                }
            }

            this.sqL_DataGridView_圖片列表.SQL_DeleteExtra(list_圖片列表_delete, false);
            this.sqL_DataGridView_圖片列表.SQL_AddRows(list_圖片列表_add, false);
            this.sqL_DataGridView_圖片列表.SQL_ReplaceExtra(list_圖片列表_replace, false);
        }
        public Bitmap Function_圖片上傳_取得指定代碼圖片(string 代碼 , ref int 停留秒數)
        {
            try
            {
                List<object[]> list_value = sqL_DataGridView_圖片列表.SQL_GetRows((int)enum_圖片列表.代碼, 代碼, false);
                if (list_value.Count == 0) return null;
                byte[] bytes = list_value[0][(int)enum_圖片列表.bytes].ObjectToString().StringHexTobytes();
                停留秒數 = list_value[0][(int)enum_圖片列表.停留秒數].StringToInt32();
                if (停留秒數 <= 0) 停留秒數 = 1;
                Bitmap image = bytes.BytesToBitmap();
                return image;
            }
            catch
            {
                return null;
            }
         
        }
        #endregion
        #region Event
        private void Button_上傳圖片_從資料庫讀取圖片_Click(object sender, EventArgs e)
        {
            try
            {
                int 停留秒數 = 0;
                string 代碼 = comboBox_圖片上傳_序號.Text;
                Image image = Function_圖片上傳_取得指定代碼圖片(代碼 , ref 停留秒數);
                plC_NumBox_圖片上傳_停留秒數.Value = 停留秒數;
                if (image == null)
                {
                    MyMessageBox.ShowDialog("載入圖片失敗!");
                    return;
                }
                this.pictureBox_圖片上傳.Image = image;
            }
            catch
            {
                MyMessageBox.ShowDialog("載入圖片失敗!");
            }
          
        }
        private void Button_圖片上傳_圖片上傳至資料庫_Click(object sender, EventArgs e)
        {
            if (this.pictureBox_圖片上傳.Image == null)
            {
                MyMessageBox.ShowDialog("未載入任何圖片!");
                return;
            }
            if (MyMessageBox.ShowDialog("是否上傳至資料庫,並覆蓋原本圖檔?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            Bitmap bitmap = new Bitmap(this.pictureBox_圖片上傳.Image);
            byte[] bytes = bitmap.BitmapToBytes();
            string 代碼 = comboBox_圖片上傳_序號.Text;
            List<object[]> list_value = sqL_DataGridView_圖片列表.SQL_GetRows((int)enum_圖片列表.代碼, 代碼, false);
            if (list_value.Count > 0)
            {
                list_value[0][(int)enum_圖片列表.bytes] = bytes.ByteToStringHex();
                list_value[0][(int)enum_圖片列表.停留秒數] = plC_NumBox_圖片上傳_停留秒數.Value;
                sqL_DataGridView_圖片列表.SQL_ReplaceExtra(list_value, false);
            }
            bitmap.Dispose();
            MyMessageBox.ShowDialog("完成!");
        }
        private void Button_圖片上傳_從本機讀取圖片_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog_LoadImage.ShowDialog() == DialogResult.OK)
            {
                string filename = this.openFileDialog_LoadImage.FileName;
                Image image = Bitmap.FromFile(filename);
                this.pictureBox_圖片上傳.Image = image;
            }
        }
 
        #endregion
        public class Icp_圖片列表 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 代碼_0 = x[(int)enum_圖片列表.代碼].ObjectToString();
                string 代碼_1 = y[(int)enum_圖片列表.代碼].ObjectToString();
                return 代碼_0.CompareTo(代碼_1);
            }
        }
    }
}
