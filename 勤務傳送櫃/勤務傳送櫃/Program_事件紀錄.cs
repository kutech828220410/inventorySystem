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
namespace 勤務傳送櫃
{
    public partial class Form1 : Form
    {
      
        public enum enum_事件類型 : int
        {
           密碼登入,
           RFID登入,
           登出,
           門片未關閉異常,
           開啟門片,
           關閉門片,
        }
        public enum enum_事件紀錄 : int
        {
            GUID,
            事件類型,
            ID,
            姓名,
            藥櫃編號,
            病房名稱,
            操作時間,
            備註,
        }
        #region PLC_Program_事件紀錄_頁面更新
        bool flag_事件紀錄_頁面更新_init = false;
        void sub_PLC_Program_事件紀錄_頁面更新()
        {
            if (this.plC_ScreenPage_Main.PageText == "事件紀錄")
            {
                if (flag_事件紀錄_頁面更新_init)
                {
                    this.Invoke(new Action(delegate
                    {
                        this.sqL_DataGridView_事件紀錄.ClearGrid();
                    }));
                    flag_事件紀錄_頁面更新_init = false;
                }
            }
            else
            {
                flag_事件紀錄_頁面更新_init = true;
            }
        }
        #endregion
        private void Program_事件紀錄_Init()
        {
            this.sqL_DataGridView_事件紀錄.Init();
            if (!this.sqL_DataGridView_事件紀錄.SQL_IsTableCreat())
            {
                this.sqL_DataGridView_事件紀錄.SQL_CreateTable();
            }
            this.plC_RJ_Button_事件紀錄_搜尋.MouseDownEvent += PlC_RJ_Button_事件紀錄_搜尋_MouseDownEvent;

            this.plC_UI_Init.Add_Method(this.Program_事件紀錄);

        }

  

        private void Program_事件紀錄() 
        {

        }
        #region Function
        private void 新增事件紀錄(enum_事件類型 _enum_事件類型, string ID, string 姓名, string 備註)
        {
            this.新增事件紀錄(_enum_事件類型, ID, 姓名, "", "", 備註);
        }
        private void 新增事件紀錄(enum_事件類型 _enum_事件類型, string ID, string 姓名, string 藥櫃編號, string 病房名稱, string 備註)
        {
            object[] values = new object[new enum_事件紀錄().GetLength()];
            values[(int)enum_事件紀錄.GUID] = Guid.NewGuid().ToString();
            values[(int)enum_事件紀錄.事件類型] = _enum_事件類型.GetEnumName();
            values[(int)enum_事件紀錄.ID] = ID;
            values[(int)enum_事件紀錄.姓名] = 姓名;
            values[(int)enum_事件紀錄.藥櫃編號] = 藥櫃編號;
            values[(int)enum_事件紀錄.病房名稱] = 病房名稱;
            values[(int)enum_事件紀錄.操作時間] = DateTime.Now;
            values[(int)enum_事件紀錄.備註] = 備註;
            this.sqL_DataGridView_事件紀錄.SQL_AddRow(values, false);
        }
        #endregion

        #region Event
        private void PlC_RJ_Button_事件紀錄_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = new List<object[]>();
            List<object[]> list_value_buf = new List<object[]>();
            List<List<object[]>> list_list_value = new List<List<object[]>>();
            if (rJ_CheckBox_事件紀錄_操作時間.Checked)
            {
                list_value = this.sqL_DataGridView_事件紀錄.SQL_GetRowsByBetween((int)enum_事件紀錄.操作時間, rJ_DatePicker_事件紀錄_操作起始時間, rJ_DatePicker_事件紀錄_操作結束時間, false);
            }
            else
            {
                list_value = this.sqL_DataGridView_事件紀錄.SQL_GetAllRows(false);
            }
            if (!rJ_TextBox_事件紀錄_ID.Text.StringIsEmpty()) list_value = list_value.GetRows((int)enum_事件紀錄.ID, rJ_TextBox_事件紀錄_ID.Text);
            if (!rJ_TextBox_事件紀錄_姓名.Text.StringIsEmpty()) list_value = list_value.GetRows((int)enum_事件紀錄.姓名, rJ_TextBox_事件紀錄_姓名.Text);
            if (!rJ_TextBox_事件紀錄_藥櫃編號.Text.StringIsEmpty()) list_value = list_value.GetRows((int)enum_事件紀錄.藥櫃編號, rJ_TextBox_事件紀錄_藥櫃編號.Text);
            if (!rJ_TextBox_事件紀錄_病房名稱.Text.StringIsEmpty()) list_value = list_value.GetRows((int)enum_事件紀錄.病房名稱, rJ_TextBox_事件紀錄_病房名稱.Text);

            if (rJ_CheckBox_事件紀錄_密碼登入.Checked) list_list_value.Add(list_value.GetRows((int)enum_事件紀錄.事件類型, enum_事件類型.密碼登入.GetEnumName()));
            if (rJ_CheckBox_事件紀錄_RFID登入.Checked) list_list_value.Add(list_value.GetRows((int)enum_事件紀錄.事件類型, enum_事件類型.RFID登入.GetEnumName()));
            if (rJ_CheckBox_事件紀錄_登出.Checked) list_list_value.Add(list_value.GetRows((int)enum_事件紀錄.事件類型, enum_事件類型.登出.GetEnumName()));
            if (rJ_CheckBox_事件紀錄_門片未關閉異常.Checked) list_list_value.Add(list_value.GetRows((int)enum_事件紀錄.事件類型, enum_事件類型.門片未關閉異常.GetEnumName()));
            if (rJ_CheckBox_事件紀錄_開啟門片.Checked) list_list_value.Add(list_value.GetRows((int)enum_事件紀錄.事件類型, enum_事件類型.開啟門片.GetEnumName()));
            if (rJ_CheckBox_事件紀錄_關閉門片.Checked) list_list_value.Add(list_value.GetRows((int)enum_事件紀錄.事件類型, enum_事件類型.關閉門片.GetEnumName()));

            for(int i = 0; i < list_list_value.Count; i++)
            {
                foreach(object[] value in list_list_value[i])
                {
                    list_value_buf.Add(value);
                }
            }
            if (list_list_value.Count == 0)
            {
                list_value_buf = list_value;
            }
            list_value_buf.Sort(new EventComparerby());
            this.sqL_DataGridView_事件紀錄.RefreshGrid(list_value_buf);
        }
        #endregion


        public class EventComparerby : IComparer<object[]>
        {
            //實作Compare方法
            //依Speed由小排到大。
            public int Compare(object[] x, object[] y)
            {
                DateTime datetime1 = x[(int)enum_事件紀錄.操作時間].ToDateTimeString_6().StringToDateTime();
                DateTime datetime2 = y[(int)enum_事件紀錄.操作時間].ToDateTimeString_6().StringToDateTime();
                int compare = DateTime.Compare(datetime1, datetime2);
                return compare;
            }
        }
    }
}
