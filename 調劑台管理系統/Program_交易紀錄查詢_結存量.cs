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
    public partial class Main_Form : Form
    {
        private void Program_交易記錄查詢_結存量_Init()
        {
            Table table = new Table(new enum_consumption());
            this.sqL_DataGridView_交易紀錄_結存量.Init(table);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnVisible(false, new enum_consumption().GetEnumNames());
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_consumption.藥碼);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(1000, DataGridViewContentAlignment.MiddleLeft, enum_consumption.藥名);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_consumption.消耗量);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_consumption.庫存量);
            //this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, enum_consumption.結存量);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_consumption.藥碼);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_consumption.藥名);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_consumption.消耗量);
            this.sqL_DataGridView_交易紀錄_結存量.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_consumption.庫存量);

            this.rJ_Button_交易紀錄_結存量_搜尋.MouseDownEvent += RJ_Button_交易紀錄_結存量_搜尋_MouseDownEvent;
            this.plC_RJ_Button_交易紀錄_結存量_匯出資料.MouseDownEvent += PlC_RJ_Button_交易紀錄_結存量_匯出資料_MouseDownEvent;
            comboBox__交易紀錄_結存量_搜尋條件.SelectedIndexChanged += ComboBox__交易紀錄_結存量_搜尋條件_SelectedIndexChanged;
            comboBox__交易紀錄_結存量_搜尋條件.SelectedIndex = 0;

        }

        private void ComboBox__交易紀錄_結存量_搜尋條件_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmb_text = this.comboBox__交易紀錄_結存量_搜尋條件.Text;
            if(cmb_text == "全部顯示" || cmb_text == "管123" || cmb_text == "管4" )
            {
                comboBox_交易紀錄_結存量_搜尋內容.Text = "";
                comboBox_交易紀錄_結存量_搜尋內容.Enabled = false;
            }
            else
            {
                comboBox_交易紀錄_結存量_搜尋內容.Text = "";
                comboBox_交易紀錄_結存量_搜尋內容.Enabled = true;
            }
            if (cmb_text == "藥品群組")
            {
                comboBox_交易紀錄_結存量_搜尋內容.Items.Clear();
                comboBox_交易紀錄_結存量_搜尋內容.Items.AddRange(medGroupClass.get_medGroupList_header(Main_Form.API_Server,Main_Form.ServerName).ToArray());
                if (comboBox_交易紀錄_結存量_搜尋內容.Items.Count > 0)
                {
                    comboBox_交易紀錄_結存量_搜尋內容.SelectedIndex = 0;
                }
            }
        }
        private void RJ_Button_交易紀錄_結存量_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            try
            {
                LoadingForm.ShowLoadingForm();
                string cmb_text = this.comboBox__交易紀錄_結存量_搜尋條件.GetComboBoxText();
                string search_content = this.comboBox_交易紀錄_結存量_搜尋內容.GetComboBoxText();

                DateTime dateTime_startTime = rJ_DatePicker_交易紀錄_結存量_操作時間_開始時間.Value;
                DateTime dateTime_endTime = rJ_DatePicker_交易紀錄_結存量_操作時間_結束時間.Value;

                List<consumptionClass> consumptionClasses = new List<consumptionClass>();
                List<medClass> medClasses_cloud = medClass.get_med_cloud(Main_Form.API_Server);
                Dictionary<string, List<medClass>> keyValuePairs_medClasses_cloud = medClasses_cloud.CoverToDictionaryByCode();

                if (cmb_text == "藥品群組")
                {
                    if (string.IsNullOrEmpty(search_content))
                    {
                        MyMessageBox.ShowDialog("請選擇藥品群組");
                        return;
                    }
                    consumptionClasses = consumptionClass.serch_by_ST_END(
                       Main_Form.API_Server,
                       Main_Form.ServerName,
                       Main_Form.ServerType,
                       dateTime_startTime.GetStartDate(),
                       dateTime_endTime.GetEndDate(),
                       search_content);
                }
                else
                {
                    // 先取出所有消耗量資料
                    consumptionClasses = consumptionClass.serch_by_ST_END(
                        Main_Form.API_Server,
                        Main_Form.ServerName,
                        Main_Form.ServerType,
                        dateTime_startTime.GetStartDate(),
                        dateTime_endTime.GetEndDate());
                }

                if (cmb_text == "全部顯示")
                {
                    // 不篩選
                }
                else if (cmb_text == "管123")
                {
                    consumptionClasses = consumptionClasses
                        .Where(x =>
                        {
                            if (!keyValuePairs_medClasses_cloud.TryGetValue(x.藥碼, out var medList)) return false;
                            string 等級 = medList.FirstOrDefault()?.管制級別;
                            return 等級 == "1" || 等級 == "2" || 等級 == "3";
                        })
                        .ToList();
                }
                else if (cmb_text == "管4")
                {
                    consumptionClasses = consumptionClasses
                        .Where(x =>
                        {
                            if (!keyValuePairs_medClasses_cloud.TryGetValue(x.藥碼, out var medList)) return false;
                            string 等級 = medList.FirstOrDefault()?.管制級別;
                            return 等級 == "4";
                        })
                        .ToList();
                }
                else
                {
                    if (string.IsNullOrEmpty(search_content))
                    {
                        MyMessageBox.ShowDialog("請輸入搜尋內容");
                        return;
                    }

                    if (cmb_text == "藥碼")
                    {
                        consumptionClasses = consumptionClasses
                            .Where(x => x.藥碼.Contains(search_content))
                            .ToList();
                    }
                    else if (cmb_text == "藥名")
                    {
                        consumptionClasses = consumptionClasses
                            .Where(x =>
                            {
                                if (!keyValuePairs_medClasses_cloud.TryGetValue(x.藥碼, out var medList)) return false;
                                return medList.FirstOrDefault()?.藥品名稱.Contains(search_content) == true;
                            })
                            .ToList();
                    }
                    else if (cmb_text == "中文名")
                    {
                        consumptionClasses = consumptionClasses
                            .Where(x =>
                            {
                                if (!keyValuePairs_medClasses_cloud.TryGetValue(x.藥碼, out var medList)) return false;
                                return medList.FirstOrDefault()?.中文名稱.Contains(search_content) == true;
                            })
                            .ToList();
                    }
                }

                List<object[]> list_consumption = consumptionClasses.ClassToSQL<consumptionClass, enum_consumption>();
                this.sqL_DataGridView_交易紀錄_結存量.RefreshGrid(list_consumption);

                if (list_consumption.Count == 0)
                {
                    MyMessageBox.ShowDialog("查無資料!");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowDialog(ex.Message);
            }
            finally
            {
                LoadingForm.CloseLoadingForm();
            }
        }
        private void PlC_RJ_Button_交易紀錄_結存量_匯出資料_MouseDownEvent(MouseEventArgs mevent)
        {
            DateTime dateTime_start = rJ_DatePicker_交易紀錄_結存量_操作時間_開始時間.Value;
            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 00, 00, 00);
            DateTime dateTime_end = rJ_DatePicker_交易紀錄_結存量_操作時間_結束時間.Value;
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);

            returnData returnData = new returnData();
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_sys_serverSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page";
            returnData.Value = $"{dateTime_start.ToDateTimeString()},{dateTime_end.ToDateTimeString()}";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/consumption/serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            json = Basic.Net.WEBApiPostJson($"{dBConfigClass.Api_URL}/api/consumption/get_sheet_by_serch", json_in);
            returnData = json.JsonDeserializet<returnData>();
            string jsondata = returnData.Data.JsonSerializationt();
            List<SheetClass> sheetClass = jsondata.JsonDeserializet<List<SheetClass>>();
            if (sheetClass.Count == 0)
            {
                MyMessageBox.ShowDialog("無資料可匯出");
                return;
            }
            this.Invoke(new Action(delegate
            {
                if (this.saveFileDialog_SaveExcel.ShowDialog() == DialogResult.OK)
                {
                    sheetClass.NPOI_SaveFile(this.saveFileDialog_SaveExcel.FileName);
                }
            }));
            MyMessageBox.ShowDialog("完成");

        }
    }
}
