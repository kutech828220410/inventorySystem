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

namespace 調劑台管理系統
{
    public partial class Dialog_交班對點 : MyDialog
    {
        public enum enum_交班藥品
        {
            [Description("GUID,VARCHAR,200,NONE")]
            GUID,
            [Description("藥碼,VARCHAR,200,NONE")]
            藥碼,
            [Description("藥名,VARCHAR,200,NONE")]
            藥名,
            [Description("單位,VARCHAR,200,NONE")]
            單位,
            [Description("庫存,VARCHAR,200,NONE")]
            庫存,
            [Description("盤點庫存,VARCHAR,200,NONE")]
            盤點庫存,
            [Description("覆盤庫存,VARCHAR,200,NONE")]
            覆盤庫存,
            [Description("備註,VARCHAR,200,NONE")]
            備註,
        }
        private List<medGroupClass> medGroupClasses = null;
        public Dialog_交班對點()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));

            this.sqL_DataGridView_交班藥品.RowEnterEvent += SqL_DataGridView_交班藥品_RowEnterEvent;
            this.LoadFinishedEvent += Dialog_交班對點_LoadFinishedEvent;
            this.rJ_Button_藥品群組_選擇.MouseDownEvent += RJ_Button_藥品群組_選擇_MouseDownEvent;
        }

        #region Event
        private void Dialog_交班對點_LoadFinishedEvent(EventArgs e)
        {
            List<StepEntity> list = new List<StepEntity>();
            list.Add(new StepEntity("1", "登入", 1, "請登入使用者(盤點)", eumStepState.Waiting, null));
            list.Add(new StepEntity("2", "登入", 2, "請登入使用者(覆盤)", eumStepState.Waiting, null));
            list.Add(new StepEntity("3", "藥品選擇", 3, "選擇交班藥品", eumStepState.Waiting, null));
            list.Add(new StepEntity("4", "交班完成", 4, "清點交班藥品", eumStepState.Waiting, null));
            this.stepViewer.CurrentStep = 1;
            this.stepViewer.ListDataSource = list;


            medGroupClasses = medGroupClass.get_all_group(Main_Form.API_Server);
            List<string> list_str = new List<string>();
            for (int i = 0; i < medGroupClasses.Count; i++)
            {
                list_str.Add(medGroupClasses[i].名稱);
            }
            this.comboBox_藥品群組.DataSource = list_str.ToArray();
            if (list_str.Count > 0)
            {
                this.comboBox_藥品群組.SelectedIndex = 0;
            }
            this.Invoke(new Action(delegate 
            {
                Table table_交班藥品 = new Table(new enum_交班藥品());
                this.sqL_DataGridView_交班藥品.Init(table_交班藥品);
                this.sqL_DataGridView_交班藥品.Set_ColumnVisible(false, new enum_交班藥品().GetEnumNames());
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.藥碼);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, enum_交班藥品.藥名);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.單位);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.盤點庫存);
                this.sqL_DataGridView_交班藥品.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, enum_交班藥品.覆盤庫存);
                this.sqL_DataGridView_交班藥品.ClearGrid();
            }));
            
        }
        private void RJ_Button_藥品群組_選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            string text = "";
            this.Invoke(new Action(delegate 
            {
                text = this.comboBox_藥品群組.Text;
            }));
            
            List<medGroupClass> medGroupClasses_buf = (from temp in medGroupClasses
                                                       where temp.名稱 == text
                                                       select temp).ToList();
            if(medGroupClasses_buf.Count > 0)
            {
                List<object[]> list_value = new List<object[]>();
                for (int i = 0; i < medGroupClasses_buf[0].MedClasses.Count; i++)
                {
                    medClass medClass = medGroupClasses_buf[0].MedClasses[i];
                    object[] value = new object[new enum_交班藥品().GetLength()];
                    value[(int)enum_交班藥品.GUID] = medClass.GUID;
                    value[(int)enum_交班藥品.藥碼] = medClass.藥品碼;
                    value[(int)enum_交班藥品.藥名] = medClass.藥品名稱;
                    value[(int)enum_交班藥品.單位] = medClass.包裝單位;
                    value[(int)enum_交班藥品.庫存] = "20";
                    list_value.Add(value);
                }
                this.sqL_DataGridView_交班藥品.RefreshGrid(list_value);
            }

        }
        private void SqL_DataGridView_交班藥品_RowEnterEvent(object[] RowValue)
        {
            string 藥碼 = RowValue[(int)enum_交班藥品.藥碼].ObjectToString();
            string 藥名 = RowValue[(int)enum_交班藥品.藥名].ObjectToString();
            string 庫存 = RowValue[(int)enum_交班藥品.庫存].ObjectToString();
            List<Image> images = medPicClass.get_images_by_code(Main_Form.API_Server, 藥碼);
            if(images == null)
            {
                pictureBox_藥品資訊.Image = null;
                return;
            }
            pictureBox_藥品資訊.Image = images[0];
            this.rJ_Lable_藥品資訊.Text = $"({藥碼}){藥名}";
            this.rJ_Lable_現有庫存.Text = $"{庫存}";
        }
        #endregion
    }
}
