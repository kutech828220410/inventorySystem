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
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using H_Pannel_lib;
using MyOffice;
using HIS_DB_Lib;
using SQLUI;

namespace 調劑台管理系統
{
    public partial class Main_Form : Form
    {
        public static SQL_DataGridView _sqL_DataGridView_LCD114_索引表;
        [EnumDescription("lcd114_index")]
        public enum enum_LCD114_索引表
        {
            [Description("GUID,VARCHAR,50,PRIKEY")]
            GUID,
            [Description("IP,VARCHAR,50,INDEX")]
            IP,
            [Description("index_IP,VARCHAR,50,INDEX")]
            index_IP,
        }
        public enum enum_LCD114_索引表_匯出
        {
            [Description("IP,VARCHAR,50,INDEX")]
            IP,
            [Description("index_IP,VARCHAR,50,INDEX")]
            index_IP,
        }
        public enum enum_LCD114_索引表_匯入
        {
            [Description("IP,VARCHAR,50,INDEX")]
            IP,
            [Description("index_IP,VARCHAR,50,INDEX")]
            index_IP,
        }
        private void Program_LCD114_索引表_Init()
        {
            Table table = new Table(new enum_LCD114_索引表());
            table.Server = dBConfigClass.DB_Basic.IP;
            table.Port = dBConfigClass.DB_Basic.Port.ToString();
            table.Username = dBConfigClass.DB_Basic.UserName;
            table.Password = dBConfigClass.DB_Basic.Password;
            table.DBName = dBConfigClass.DB_Basic.DataBaseName;

            _sqL_DataGridView_LCD114_索引表 = this.sqL_DataGridView_LCD114_索引表;
            this.sqL_DataGridView_LCD114_索引表.AutoSelectToDeep = false;
            this.sqL_DataGridView_LCD114_索引表.Init(table);

            if (this.sqL_DataGridView_LCD114_索引表.SQL_IsTableCreat() == false) this.sqL_DataGridView_LCD114_索引表.SQL_CreateTable();
            else this.sqL_DataGridView_LCD114_索引表.SQL_CheckAllColumnName(true);
            this.sqL_DataGridView_LCD114_索引表.MouseDown += SqL_DataGridView_LCD114_索引表_MouseDown;
            plC_RJ_Button_LCD114_索引表_匯出.MouseDownEvent += PlC_RJ_Button_LCD114_索引表_匯出_MouseDownEvent;
            plC_RJ_Button_LCD114_索引表_匯入.MouseDownEvent += PlC_RJ_Button_LCD114_索引表_匯入_MouseDownEvent;
            this.plC_UI_Init.Add_Method(this.Program_LCD114_索引表);

        }

        public static string Funcion_取得LCD114索引表_index_IP(string IP)
        {
            List<object[]> list_value = _sqL_DataGridView_LCD114_索引表.SQL_GetRows((int)enum_LCD114_索引表.IP, IP, false);
            if (list_value.Count == 0) return "";
            return list_value[0][(int)enum_LCD114_索引表.index_IP].ObjectToString();
        }

        private void Program_LCD114_索引表()
        {

        }
        private void SqL_DataGridView_LCD114_索引表_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_LCD114_索引表.SQL_GetAllRows(true);
        }
        private void PlC_RJ_Button_LCD114_索引表_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (this.openFileDialog_LoadExcel.ShowDialog() != DialogResult.OK) return;
                string filename = this.openFileDialog_LoadExcel.FileName;
                DataTable dataTable = MyOffice.ExcelClass.NPOI_LoadFile(filename);
                dataTable = dataTable.ReorderTable(new enum_LCD114_索引表_匯入());
                if (dataTable == null)
                {
                    MyMessageBox.ShowDialog("匯入失敗");
                    return;
                }
                List<object[]> list_value = dataTable.DataTableToRowList();
                List<object[]> list_value_add = new List<object[]>();

                List<object[]> list_LCD114_索引表 = this.sqL_DataGridView_LCD114_索引表.SQL_GetAllRows(false);
                List<object[]> list_LCD114_索引表_buf = new List<object[]>();

                this.sqL_DataGridView_LCD114_索引表.SQL_DeleteExtra(list_LCD114_索引表, false);

                for (int i = 0; i < list_value.Count; i++)
                {
                    string IP = list_value[i][(int)enum_LCD114_索引表_匯入.IP].ObjectToString();
                    string index_IP = list_value[i][(int)enum_LCD114_索引表_匯入.index_IP].ObjectToString();

                    if(IP.Check_IP_Adress() == true && index_IP.Check_IP_Adress() == true)
                    {
                        string GUID = Guid.NewGuid().ToString();
                        object[] value = new object[new enum_LCD114_索引表().GetLength()];
                        value[(int)enum_LCD114_索引表.GUID] = GUID;
                        value[(int)enum_LCD114_索引表.IP] = IP;
                        value[(int)enum_LCD114_索引表.index_IP] = index_IP;

                        list_value_add.Add(value);
                    }
                }

                this.sqL_DataGridView_LCD114_索引表.SQL_AddRows(list_value_add , true);
            }));
        }
        private void PlC_RJ_Button_LCD114_索引表_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() != DialogResult.OK) return;
                DataTable dataTable = sqL_DataGridView_LCD114_索引表.GetDataTable();
                dataTable = dataTable.ReorderTable(new enum_LCD114_索引表_匯出());
                string filename = this.saveFileDialog_SaveExcel.FileName;
                MyOffice.ExcelClass.NPOI_SaveFile(dataTable, filename);
                MyMessageBox.ShowDialog("匯出完成");

            }));
        }
    }
}
