using System;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class IncomeReasonsController : Controller
    {
        static private string API_Server = ConfigurationManager.AppSettings["API_Server"];

        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("init")]
        [HttpGet]
        public string GET_init(returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
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
            catch(Exception e)
            {
                return e.Message;
            }
        
        }
        [Route("data")]
        [HttpGet]
        public string GET_data()
        {
            returnData returndata = new returnData();
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returndata.ServerName, returndata.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returndata.Code = -200;
                    returndata.Result = $"找無Server資料!";
                    return returndata.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "IncomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl.GetAllRows(null);
                List<IncomeReasonsClass> list_IncomeReasonsClass = IncomeReasonsClass.SQLToListClass(list_value);
                list_IncomeReasonsClass.Sort(new ICP_by_CT_TIME());
                returndata.Code = 200;
                returndata.Data = list_IncomeReasonsClass;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = "取得收支原因成功!";
            }
            catch (Exception e)
            {
                returndata.Code = -200;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = e.Message;
            }
            return returndata.JsonSerializationt(true);
        }
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returndata)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returndata.ServerName, returndata.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returndata.Code = -200;
                    returndata.Result = $"找無Server資料!";
                    return returndata.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "IncomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = IncomeReasonsClass.ObjToListSQL(returndata.Data);
                for (int i = 0; i < list_value.Count; i++)
                {
                    list_value[i][(int)enum_收支原因.GUID] = Guid.NewGuid().ToString();
                    list_value[i][(int)enum_收支原因.新增時間] = DateTime.Now.ToDateTimeString_6();
                }
                sQLControl.AddRows(null, list_value);
                returndata.Code = 200;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = "新增收支原因成功!";
            }
            catch (Exception e)
            {
                returndata.Code = -200;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = e.Message;
            }
            finally
            {
                
            }

            return returndata.JsonSerializationt(true);
        }

        [Route("update")]
        [HttpPost]
        public string POST_update([FromBody] returnData returndata)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returndata.ServerName, returndata.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returndata.Code = -200;
                    returndata.Result = $"找無Server資料!";
                    return returndata.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "IncomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = IncomeReasonsClass.ObjToListSQL(returndata.Data);
                sQLControl.UpdateByDefulteExtra(null, list_value);
                returndata.Code = 200;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = "修改收支原因成功!";
            }
            catch (Exception e)
            {
                returndata.Code = -200;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = e.Message;
            }
            finally
            {

            }

            return returndata.JsonSerializationt(true);
        }
        [Route("delete")]
        [HttpPost]
        public string POST_delete([FromBody] returnData returndata)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returndata.ServerName, returndata.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returndata.Code = -200;
                    returndata.Result = $"找無Server資料!";
                    return returndata.JsonSerializationt();
                }
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                CheckCreatTable(serverSettingClasses[0]);
                SQLControl sQLControl = new SQLControl(Server, DB, "IncomeReasons", UserName, Password, Port, SSLMode);
                List<object[]> list_value = IncomeReasonsClass.ObjToListSQL(returndata.Data);
                sQLControl.DeleteExtra(null, list_value);
                returndata.Code = 200;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = "刪除收支原因成功!";
            }
            catch (Exception e)
            {
                returndata.Code = -200;
                returndata.TimeTaken = myTimer.ToString();
                returndata.Result = e.Message;
            }
            finally
            {

            }

            return returndata.JsonSerializationt(true);
        }


        public string CheckCreatTable(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            SQLControl sQLControl = new SQLControl(Server, DB, "IncomeReasons", UserName, Password, Port, SSLMode);
            Table table = new Table("IncomeReasons");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("類別", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("原因", Table.StringType.TEXT, 200, Table.IndexType.None);
            table.AddColumnList("新增時間", Table.DateType.DATETIME, Table.IndexType.None);
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
        private class ICP_by_CT_TIME : IComparer<IncomeReasonsClass>
        {
            public int Compare(IncomeReasonsClass x, IncomeReasonsClass y)
            {
                string Code0 = y.新增時間;
                string Code1 = x.新增時間;
                return Code1.CompareTo(Code0);
            }
        }
    }
}
