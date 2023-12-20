using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using HIS_DB_Lib;
using SQLUI;

namespace 調劑台管理系統
{
    public class CommonSapceClass
    {
        public ServerSettingClass serverSettingClass = new ServerSettingClass();
        public List<Storage> List_EPD266 = new List<Storage>();
        public List<Drawer> List_EPD583 = new List<Drawer>();
        public List<RowsLED> List_RowsLED = new List<RowsLED>();

        public CommonSapceClass(ServerSettingClass serverSettingClass)
        {
            this.serverSettingClass = serverSettingClass;
        }

        public void Load()
        {
            string IP = serverSettingClass.Server;
            string DataBaseName = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = serverSettingClass.Port.StringToUInt32();
            SQLControl sQLControl_EPD583_serialize = new SQLControl(IP, DataBaseName, "epd583_jsonstring", UserName, Password, Port, MySql.Data.MySqlClient.MySqlSslMode.None);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DataBaseName, "epd266_jsonstring", UserName, Password, Port, MySql.Data.MySqlClient.MySqlSslMode.None);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DataBaseName, "rowsled_jsonstring", UserName, Password, Port, MySql.Data.MySqlClient.MySqlSslMode.None);
            List<object[]> list_EPD583 = sQLControl_EPD583_serialize.GetAllRows(null);
            List<object[]> list_EPD266 = sQLControl_EPD266_serialize.GetAllRows(null);
            List<object[]> list_RowsLED = sQLControl_RowsLED_serialize.GetAllRows(null);
            List_EPD583 = H_Pannel_lib.DrawerMethod.SQL_GetAllDrawers(list_EPD583);
            List_EPD266 = H_Pannel_lib.StorageMethod.SQL_GetAllStorage(list_EPD266);
            List_RowsLED = H_Pannel_lib.RowsLEDMethod.SQL_GetAllRowsLED(list_RowsLED);
        }
    }

    public partial class Form1 : Form
    {
        List<CommonSapceClass> commonSapceClasses = new List<CommonSapceClass>();
        public Table table_共用區 = null;
        private void Program_共用區_Init()
        {
            Table table = new Table("");
            table.Server = dBConfigClass.DB_Basic.IP;
            table.Port = dBConfigClass.DB_Basic.Port.ToString();
            table.Username = dBConfigClass.DB_Basic.UserName;
            table.Password = dBConfigClass.DB_Basic.Password;
            table.DBName = "dps01";
            table.TableName = "common_space_setup";
            table.AddColumnList("GUID", Table.StringType.VARCHAR, Table.IndexType.PRIMARY);
            table.AddColumnList("共用區名稱", Table.StringType.VARCHAR, 200 ,Table.IndexType.None);
            table.AddColumnList("共用區類型", Table.StringType.VARCHAR, Table.IndexType.None);
            table.AddColumnList("是否共用", Table.StringType.VARCHAR, Table.IndexType.None);
            table.AddColumnList("設置時間", Table.DateType.DATETIME, Table.IndexType.None);
            table_共用區 = table;
            this.sqL_DataGridView_共用區設定.Init(table);
            if(this.sqL_DataGridView_共用區設定.SQL_IsTableCreat())
            {
                this.sqL_DataGridView_共用區設定.SQL_CheckAllColumnName(true);
            }
            else
            {
                this.sqL_DataGridView_共用區設定.SQL_CreateTable();
            }


            this.plC_RJ_Button_共用區亮燈範圍設置.MouseDownEvent += PlC_RJ_Button_共用區亮燈範圍設置_MouseDownEvent;
            this.plC_UI_Init.Add_Method(this.Program_共用區);

        }
        private void Program_共用區()
        {

        }
        #region Function

        public List<object> Function_從共用區取得儲位(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            for (int m = 0; m < commonSapceClasses.Count; m++)
            {
                List<Box> boxes = commonSapceClasses[m].List_EPD583.SortByCode(藥品碼);
                List<Storage> storages = commonSapceClasses[m].List_EPD266.SortByCode(藥品碼);
                List<RowsDevice> rowsDevices = commonSapceClasses[m].List_RowsLED.SortByCode(藥品碼);
                for (int i = 0; i < boxes.Count; i++)
                {
                    list_value.Add(boxes[i]);
                }

                for (int i = 0; i < storages.Count; i++)
                {
                    list_value.Add(storages[i]);
                }

                for (int i = 0; i < rowsDevices.Count; i++)
                {
                    list_value.Add(rowsDevices[i]);
                }
            }
           
           
          
            return list_value;
        }


        private List<CommonSapceClass> Function_取得共用區所有儲位()
        {
            List<CommonSapceClass> commonSapceClasses = new List<CommonSapceClass>();
            List<HIS_DB_Lib.ServerSettingClass> serverSettingClasses = Function_取得共用區連線資訊();

            for (int i = 0; i < serverSettingClasses.Count; i++)
            {
                CommonSapceClass commonSapceClass = new CommonSapceClass(serverSettingClasses[i]);
                commonSapceClass.Load();
                commonSapceClasses.Add(commonSapceClass);
            }


            return commonSapceClasses;
        }
        private List<HIS_DB_Lib.ServerSettingClass> Function_取得共用區連線資訊()
        {
            List<object[]> list_value = this.sqL_DataGridView_共用區設定.SQL_GetAllRows(false);
            list_value = (from temp in list_value
                          where temp[(int)enum_commonSpaceSetup.是否共用].ObjectToString().ToUpper() == true.ToString().ToUpper()
                          select temp).ToList();
            string json_result = Basic.Net.WEBApiGet($"{dBConfigClass.Api_Server}/api/ServerSetting");
            if (json_result.StringIsEmpty())
            {
                Console.WriteLine($"API 連結失敗 : {dBConfigClass.Api_Server}/api/ServerSetting");
                return new List<ServerSettingClass>();
            }
            Console.WriteLine(json_result);
            returnData returnData = json_result.JsonDeserializet<returnData>();
            List<HIS_DB_Lib.ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
            List<HIS_DB_Lib.ServerSettingClass> serverSettingClasses_buf = new List<ServerSettingClass>();
            List<HIS_DB_Lib.ServerSettingClass> serverSettingClasses_return = new List<ServerSettingClass>();
            for (int i = 0; i < list_value.Count; i++ )
            {
                string 名稱 = list_value[i][(int)enum_commonSpaceSetup.共用區名稱].ObjectToString();
                serverSettingClasses_buf = (from temp in serverSettingClasses
                                            where temp.設備名稱 == 名稱
                                            where temp.類別 == "調劑台"
                                            where temp.內容 == "儲位資料"
                                            select temp).ToList();
                if (serverSettingClasses_buf.Count > 0)
                {
                    serverSettingClasses_return.Add(serverSettingClasses_buf[0]);
                }
            }
            return serverSettingClasses_return; 

        }
        #endregion
        #region Event

        #endregion
        private void PlC_RJ_Button_共用區亮燈範圍設置_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_共用區設置 dialog_共用區設置 = new Dialog_共用區設置(table_共用區);
            if (dialog_共用區設置.ShowDialog() != DialogResult.Yes) return;
            this.commonSapceClasses = Function_取得共用區所有儲位();
            MyMessageBox.ShowDialog("已完成共用區設定!");
        }


    }
}
