using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;
using MyOffice;
using NPOI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;
namespace HIS_WebApi
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class person_pageController : ControllerBase
    {

        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
  
        [Route("init")]
        [HttpPost]
        public string POST_init(returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        [HttpPost]
        public string Get(returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_personPage = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_personPage.GetAllRows(null);
                List<personPageClass> personPageClasses = list_value.SQLToClass<personPageClass, enum_人員資料>();
                returnData.Data = personPageClasses;
                returnData.Code = 200;
                returnData.Result = "取得人員資料成功!";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }


        }
        [Route("serch_by_id")]
        [HttpPost]
        public string POST_serch_by_id([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string ID = returnData.Value;
                if(ID.StringIsEmpty())
                {

                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_personPage = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_personPage.GetRowsByDefult(null, (int)enum_人員資料.ID, ID);
                if(list_value.Count > 0)
                {
                    returnData.Data = list_value[0].SQLToClass<personPageClass, enum_人員資料>();
                }
                returnData.Code = 200;
                returnData.Result = $"搜尋人員資料成功";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_personPage = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_personPage.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_src = returnData.Data.ObjToListSQL<personPageClass, enum_人員資料>();
                List<object[]> list_add = new List<object[]>();
                List<object[]> list_replace = new List<object[]>();

                for(int i = 0; i < list_src.Count; i++)
                {
                    string ID = list_src[i][(int)enum_人員資料.ID].ObjectToString();
                    list_value_buf = list_value.GetRows((int)enum_人員資料.ID, ID);
                    if(list_value_buf.Count > 0)
                    {
                        list_replace.Add(list_src[i]);
                    }
                    else
                    {
                        list_src[i][(int)enum_人員資料.GUID] = Guid.NewGuid().ToString();
                        list_add.Add(list_src[i]);
                    }
                }
                if (list_replace.Count > 0) sQLControl_personPage.UpdateByDefulteExtra(null, list_replace);
                if (list_add.Count > 0) sQLControl_personPage.AddRows(null, list_add);

                returnData.Code = 200;
                returnData.Result = $"更動人員資料成功,新增<{list_add.Count}>筆,修正<{list_replace.Count}>筆!";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }


        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);
            Table table = new Table("person_page");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("ID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table.AddColumnList("姓名", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("性別", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("密碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("單位", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("藥師證字號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("權限等級", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("顏色", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("卡號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("一維條碼", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("識別圖案", Table.StringType.TEXT, 50, Table.IndexType.None);
            table.AddColumnList("指紋辨識", Table.StringType.VARCHAR, 500, Table.IndexType.None);
            table.AddColumnList("指紋ID", Table.StringType.VARCHAR, 20, Table.IndexType.None);

            if (!sQLControl.IsTableCreat())
            {
                sQLControl.CreatTable(table);
            }
            else
            {
                sQLControl.CheckAllColumnName(table, true);
            }
            return table.JsonSerializationt(true);
        }

    }
}
