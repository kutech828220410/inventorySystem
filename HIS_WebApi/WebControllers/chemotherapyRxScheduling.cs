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
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChemotherapyRxScheduling : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;


        /// <summary>
        /// 初始化處方資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "cheom",
        ///     "ServerType" : "癌症備藥機",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init_ctclist")]
        [HttpPost]
        public string GET_init_ctclist(returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "排程醫令資料");
                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                return CheckCreatTable_ctclist(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// 新增處方至資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="ctclistClass">化療處方結構</param>
        /// <returns></returns>
        [Route("add_ctclist_ex")]
        [HttpPost]
        public string POST_add_ctclist(ctclistClass ctclistClass)
        {
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Data = ctclistClass;
            return POST_add_ctclist(returnData);
        }
        /// <summary>
        /// 新增處方至資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "cheom",
        ///     "ServerType" : "癌症備藥機",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add_ctclist")]
        [HttpPost]
        public string POST_add_ctclist(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add_ctclist";
            try
            {

                ctclistClass ctclistClass = returnData.Data.ObjToClass<ctclistClass>();
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "排程醫令資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                if (ctclistClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料結構錯誤";
                    return returnData.JsonSerializationt(true);
                }
                returnData.Data = "";
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_ctclist = new SQLControl(Server, DB, "ctclist", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_ctclist_udAry = new SQLControl(Server, DB, "ctclist_udAry", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_ctclist_changAry = new SQLControl(Server, DB, "ctclist_changeAry", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_ctclist_ctcAry = new SQLControl(Server, DB, "ctclist_ctcAry", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_ctclist_noteAry = new SQLControl(Server, DB, "ctclist_noteAry", UserName, Password, Port, SSLMode);

                object[] ctclist = ctclistClass.ClassToSQL<ctclistClass, enum_ctclist>();
                List<object[]> ctclist_udAry = ctclistClass.udAry.ClassToSQL<ctclistClass.udAryClass, ctclistClass.enum_udAry>();
                List<object[]> ctclist_changAry = ctclistClass.changAry.ClassToSQL<ctclistClass.changAryClass, ctclistClass.enum_changAry>();
                List<object[]> ctclist_ctcAry = ctclistClass.ctcAry.ClassToSQL<ctclistClass.ctcAryClass, ctclistClass.enum_ctcAry>();
                List<object[]> ctclist_noteAry = ctclistClass.noteAry.ClassToSQL<ctclistClass.noteAryClass, ctclistClass.enum_noteAry>();

                string Master_GUID = Guid.NewGuid().ToString();
                ctclist[(int)enum_ctclist.GUID] = Master_GUID;
                for (int i = 0; i < ctclist_udAry.Count; i++)
                {
                    ctclist_udAry[i][(int)ctclistClass.enum_udAry.GUID] = Guid.NewGuid().ToString();
                    ctclist_udAry[i][(int)ctclistClass.enum_udAry.Master_GUID] = Master_GUID;
                    ctclist_udAry[i][(int)ctclistClass.enum_udAry.開始時間] = $"{ctclistClass.udAry[i].開始日} {ctclistClass.udAry[i].開始時間}";
                    ctclist_udAry[i][(int)ctclistClass.enum_udAry.結束時間] = $"{ctclistClass.udAry[i].結束日} {ctclistClass.udAry[i].結束時間}";

                }
                for (int i = 0; i < ctclist_changAry.Count; i++)
                {
                    ctclist_changAry[i][(int)ctclistClass.enum_changAry.GUID] = Guid.NewGuid().ToString();
                    ctclist_changAry[i][(int)ctclistClass.enum_changAry.Master_GUID] = Master_GUID;
                    ctclist_changAry[i][(int)ctclistClass.enum_changAry.變異時間] = $"{ctclistClass.changAry[i].變異日期} {ctclistClass.changAry[i].變異時間}";
                }
                for (int i = 0; i < ctclist_ctcAry.Count; i++)
                {
                    ctclist_ctcAry[i][(int)ctclistClass.enum_ctcAry.GUID] = Guid.NewGuid().ToString();
                    ctclist_ctcAry[i][(int)ctclistClass.enum_ctcAry.Master_GUID] = Master_GUID;
                }
                for (int i = 0; i < ctclist_noteAry.Count; i++)
                {
                    ctclist_noteAry[i][(int)ctclistClass.enum_noteAry.GUID] = Guid.NewGuid().ToString();
                    ctclist_noteAry[i][(int)ctclistClass.enum_noteAry.Master_GUID] = Master_GUID;
                }

                sQLControl_ctclist.AddRow(null, ctclist);
                sQLControl_ctclist_udAry.AddRows(null, ctclist_udAry);
                sQLControl_ctclist_changAry.AddRows(null, ctclist_changAry);
                sQLControl_ctclist_ctcAry.AddRows(null, ctclist_ctcAry);
                sQLControl_ctclist_noteAry.AddRows(null, ctclist_noteAry);
                returnData.Result = $"ctclist 寫入成功!";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                return returnData.JsonSerializationt(true);
            }

        }

        /// <summary>
        /// 初始化配藥通知資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "cheom",
        ///     "ServerType" : "癌症備藥機",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init_udnoectc")]
        [HttpPost]
        public string GET_init_udnoectc(returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "排程醫令資料");
                if (serverSettingClasses.Count == 0)
                {
                    return $"找無Server資料!";
                }
                return CheckCreatTable_udnoectc(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// 新增配藥通知至資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="udnoectcClass">化療配藥通知結構</param>
        /// <returns></returns>
        [Route("add_udnoectc_ex")]
        [HttpPost]
        public string POST_add_udnoectc(udnoectc udnoectcClass)
        {
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Data = udnoectcClass;
            List<udnoectc.ordersClass> orsersAry = udnoectcClass.ordersAry;
            return POST_add_udnoectc(returnData);
        }
        /// <summary>
        /// 新增配藥通知至資料庫
        /// </summary>
        /// <remarks>
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ServerName" : "cheom",
        ///     "ServerType" : "癌症備藥機",
        ///     "Data": 
        ///     {
        ///  
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("add_udnoectc")]
        [HttpPost]
        public string POST_add_udnoectc(returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            myTimerBasic.StartTickTime(50000);
            returnData.Method = "add_udnoectc";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "排程醫令資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt(true);
                }
                udnoectc udnoectcClass = returnData.Data.ObjToClass<udnoectc>();
                if (udnoectcClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入資料結構錯誤";
                    return returnData.JsonSerializationt(true);
                }
                returnData.Data = "";
                string Server = serverSettingClasses[0].Server;
                string DB = serverSettingClasses[0].DBName;
                string UserName = serverSettingClasses[0].User;
                string Password = serverSettingClasses[0].Password;
                uint Port = (uint)serverSettingClasses[0].Port.StringToInt32();

                SQLControl sQLControl_udnoectc = new SQLControl(Server, DB, "udnoectc", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_udnoectc_orders = new SQLControl(Server, DB, "udnoectc_orders", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_udnoectc_ctcvars = new SQLControl(Server, DB, "udnoectc_ctcvars", UserName, Password, Port, SSLMode);

                object[] _udnoectc = udnoectcClass.ClassToSQL<udnoectc, enum_udnoectc>();
                List<object[]> list_udnoectc = new List<object[]>();
                List<object[]> list_udnoectc_Add = new List<object[]>();
                List<object[]> list_udnoectc_Replace = new List<object[]>();
                List<object[]> udnoectc_order = udnoectcClass.ordersAry.ClassToSQL<udnoectc.ordersClass, udnoectc.enum_orders>();
                List<object[]> list_udnoectc_order_Add = new List<object[]>();
                List<object[]> list_udnoectc_order_Replace = new List<object[]>();
                List<object[]> udnoectc_ctcvars = udnoectcClass.ctcvarsAry.ClassToSQL<udnoectc.ctcvarsClass, udnoectc.enum_ctcvars>();
                List<object[]> list_udnoectc_ctcvars_Add = new List<object[]>();
                List<object[]> list_udnoectc_ctcvars_Replace = new List<object[]>();

                string 病歷號 = _udnoectc[(int)enum_udnoectc.病歷號].ObjectToString();
                string 診別 = _udnoectc[(int)enum_udnoectc.診別].ObjectToString();
                string 就醫序號 = _udnoectc[(int)enum_udnoectc.就醫序號].ObjectToString();
                string 醫囑序號 = _udnoectc[(int)enum_udnoectc.醫囑序號].ObjectToString();
              
                string[] udnoectc_serch_colname = new string[] { "病歷號", "診別", "就醫序號", "醫囑序號" };
                string[] udnoectc_serch_value = new string[] { $"{病歷號}", $"{診別}", $"{就醫序號}", $"{醫囑序號}" };
                string Master_GUID = "";
                list_udnoectc = sQLControl_udnoectc.GetRowsByDefult(null, udnoectc_serch_colname, udnoectc_serch_value);
                if(list_udnoectc.Count == 0)
                {
                    Master_GUID = Guid.NewGuid().ToString();
                    object[] value = _udnoectc;
                    value[(int)enum_udnoectc.GUID] = Master_GUID;
                  
                    list_udnoectc_Add.Add(value);
                }
                else
                {
                    Master_GUID = list_udnoectc[0][(int)enum_udnoectc.GUID].ObjectToString();
                    object[] value = _udnoectc;
                    value[(int)enum_udnoectc.GUID] = Master_GUID;
                    sQLControl_udnoectc_orders.DeleteByDefult(null, (int)udnoectc.enum_orders.Master_GUID, Master_GUID);
                    sQLControl_udnoectc_ctcvars.DeleteByDefult(null, (int)udnoectc.enum_ctcvars.Master_GUID, Master_GUID);

                    list_udnoectc_Replace.Add(value);
                }
                for (int i = 0; i < udnoectc_order.Count; i++)
                {
                    udnoectc_order[i][(int)udnoectc.enum_orders.GUID] = Guid.NewGuid().ToString();
                    udnoectc_order[i][(int)udnoectc.enum_orders.Master_GUID] = Master_GUID;
                }
                for (int i = 0; i < udnoectc_ctcvars.Count; i++)
                {
                    udnoectc_ctcvars[i][(int)udnoectc.enum_ctcvars.GUID] = Guid.NewGuid().ToString();
                    udnoectc_ctcvars[i][(int)udnoectc.enum_ctcvars.Master_GUID] = Master_GUID;
                }

                if (list_udnoectc_Add.Count > 0) sQLControl_udnoectc.AddRows(null, list_udnoectc_Add);
                if (list_udnoectc_Replace.Count > 0) sQLControl_udnoectc.UpdateByDefulteExtra(null, list_udnoectc_Replace);
                sQLControl_udnoectc_orders.AddRows(null, udnoectc_order);
                sQLControl_udnoectc_ctcvars.AddRows(null, udnoectc_ctcvars);

                returnData.Result = $"udnoectc 新增<{list_udnoectc_Add.Count}>筆,修改<{list_udnoectc_Replace.Count}>筆 ";
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Code = 200;
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Data = null;
                returnData.Result = $"{e.Message}";
                return returnData.JsonSerializationt(true);
            }

        }

        private string CheckCreatTable_ctclist(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            SQLControl sQLControl_ctclist = new SQLControl(Server, DB, "ctclist", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_ctclist_udAry = new SQLControl(Server, DB, "ctclist_udAry", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_ctclist_changAry = new SQLControl(Server, DB, "ctclist_changeAry", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_ctclist_ctcAry = new SQLControl(Server, DB, "ctclist_ctcAry", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_ctclist_noteAry = new SQLControl(Server, DB, "ctclist_noteAry", UserName, Password, Port, SSLMode);

            List<Table> tables = new List<Table>();
            Table table_ctclist = new Table("ctclist");
            table_ctclist.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_ctclist.AddColumnList("病歷號", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("病人姓名", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("領藥號", Table.StringType.VARCHAR, 10, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("藥局代碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("就醫序號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("處方箋序號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("處方狀態", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("年齡", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("Regimen名稱", Table.StringType.TEXT, 300, Table.IndexType.None);
            table_ctclist.AddColumnList("主分類", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_ctclist.AddColumnList("次分類", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_ctclist.AddColumnList("生日", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("性別", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("病房", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("病床", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("藥囑開始日期", Table.DateType.DATETIME, 100, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("藥囑結束日期", Table.DateType.DATETIME, 100, Table.IndexType.INDEX);
            table_ctclist.AddColumnList("開立醫師", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("審核醫師", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("CTC分類代號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("CTC分類中文", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_ctclist.AddColumnList("科別", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("身高", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("體重", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("BSA", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("藥品狀態", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_ctclist.AddColumnList("Performance_status", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table_ctclist.AddColumnList("細胞型態", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("檢查驗資料", Table.StringType.VARCHAR, 500, Table.IndexType.None);
            table_ctclist.AddColumnList("XRAY文字", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("EKG文字", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("Pulmdata", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_ctclist.AddColumnList("診斷", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist.AddColumnList("癌症用藥途徑", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            if (!sQLControl_ctclist.IsTableCreat()) sQLControl_ctclist.CreatTable(table_ctclist);
            else sQLControl_ctclist.CheckAllColumnName(table_ctclist, true);
            tables.Add(table_ctclist);

            Table table_ctclist_udAry = new Table("ctclist_udAry");
            table_ctclist_udAry.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_ctclist_udAry.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_ctclist_udAry.AddColumnList("病歷號", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_ctclist_udAry.AddColumnList("藥局代碼", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("醫囑序號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("藥囑序號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("服藥順序", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("REGIMEN編號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("藥碼", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_ctclist_udAry.AddColumnList("藥囑名稱", Table.StringType.TEXT, 500, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("自費", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("劑量", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("頻次", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("途徑", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("天數", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("開始時間", Table.DateType.DATETIME, 100, Table.IndexType.INDEX);
            table_ctclist_udAry.AddColumnList("結束時間", Table.DateType.DATETIME, 100, Table.IndexType.INDEX);
            table_ctclist_udAry.AddColumnList("藥品外型外觀", Table.StringType.VARCHAR, 100, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("藥品使用", Table.StringType.VARCHAR, 100, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("藥品下方註記", Table.StringType.TEXT, 500, Table.IndexType.None);
            table_ctclist_udAry.AddColumnList("藥品圖片網址", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            if (!sQLControl_ctclist_udAry.IsTableCreat()) sQLControl_ctclist_udAry.CreatTable(table_ctclist_udAry);
            else sQLControl_ctclist_udAry.CheckAllColumnName(table_ctclist_udAry, true);
            tables.Add(table_ctclist_udAry);

            Table table_ctclist_changeAry = new Table("ctclist_changeAry");
            table_ctclist_changeAry.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_ctclist_changeAry.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_ctclist_changeAry.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table_ctclist_changeAry.AddColumnList("變異時間", Table.DateType.DATETIME, 100, Table.IndexType.INDEX);
            table_ctclist_changeAry.AddColumnList("變異原因", Table.StringType.TEXT, 500, Table.IndexType.None);
            table_ctclist_changeAry.AddColumnList("變異內容", Table.StringType.TEXT, 500, Table.IndexType.None);
            if (!sQLControl_ctclist_changAry.IsTableCreat()) sQLControl_ctclist_changAry.CreatTable(table_ctclist_changeAry);
            else sQLControl_ctclist_changAry.CheckAllColumnName(table_ctclist_changeAry, true);
            tables.Add(table_ctclist_changeAry);

            Table table_ctclist_ctcAry = new Table("ctclist_ctcAry");
            table_ctclist_ctcAry.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_ctclist_ctcAry.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_ctclist_ctcAry.AddColumnList("歷經第幾次化療", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_ctclist_ctcAry.AddColumnList("開立日期", Table.DateType.DATETIME, 100, Table.IndexType.INDEX);
            table_ctclist_ctcAry.AddColumnList("Regimen名稱", Table.StringType.TEXT, 500, Table.IndexType.None);
            if (!sQLControl_ctclist_ctcAry.IsTableCreat()) sQLControl_ctclist_ctcAry.CreatTable(table_ctclist_ctcAry);
            else sQLControl_ctclist_ctcAry.CheckAllColumnName(table_ctclist_ctcAry, true);
            tables.Add(table_ctclist_ctcAry);

            Table table_ctclist_noteAry = new Table("ctclist_noteAry");
            table_ctclist_noteAry.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_ctclist_noteAry.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_ctclist_noteAry.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 300, Table.IndexType.None);
            table_ctclist_noteAry.AddColumnList("調整與注意事項", Table.StringType.TEXT, 500, Table.IndexType.None);
            if (!sQLControl_ctclist_noteAry.IsTableCreat()) sQLControl_ctclist_noteAry.CreatTable(table_ctclist_noteAry);
            else sQLControl_ctclist_noteAry.CheckAllColumnName(table_ctclist_noteAry, true);
            tables.Add(table_ctclist_noteAry);

            return tables.JsonSerializationt(true);
        }
        private string CheckCreatTable_udnoectc(ServerSettingClass serverSettingClass)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            SQLControl sQLControl_udnoectc = new SQLControl(Server, DB, "udnoectc", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_udnoectc_orders = new SQLControl(Server, DB, "udnoectc_orders", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_udnoectc_ctcvars = new SQLControl(Server, DB, "udnoectc_ctcvars", UserName, Password, Port, SSLMode);


            List<Table> tables = new List<Table>();
            Table table_udnoectc = new Table("udnoectc");
            table_udnoectc.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_udnoectc.AddColumnList("病房", Table.StringType.VARCHAR, 10, Table.IndexType.INDEX);
            table_udnoectc.AddColumnList("床號", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc.AddColumnList("病人姓名", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table_udnoectc.AddColumnList("病歷號", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_udnoectc.AddColumnList("生日", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc.AddColumnList("性別", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc.AddColumnList("身高", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc.AddColumnList("體重", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc.AddColumnList("診斷", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc.AddColumnList("科別", Table.StringType.VARCHAR, 20, Table.IndexType.INDEX);
            table_udnoectc.AddColumnList("開立醫師", Table.StringType.VARCHAR, 30, Table.IndexType.INDEX);
            table_udnoectc.AddColumnList("過敏記錄", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc.AddColumnList("RegimenName", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc.AddColumnList("天數順序", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc.AddColumnList("診別", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc.AddColumnList("就醫序號", Table.StringType.VARCHAR, 10, Table.IndexType.INDEX);
            table_udnoectc.AddColumnList("醫囑序號", Table.StringType.VARCHAR, 10, Table.IndexType.INDEX);
            table_udnoectc.AddColumnList("化學治療前檢核項目", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            if (!sQLControl_udnoectc.IsTableCreat()) sQLControl_udnoectc.CreatTable(table_udnoectc);
            else sQLControl_udnoectc.CheckAllColumnName(table_udnoectc, true);
            tables.Add(table_udnoectc);


            Table table_udnoectc_orders = new Table("udnoectc_orders");
            table_udnoectc_orders.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_udnoectc_orders.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table_udnoectc_orders.AddColumnList("藥囑序號", Table.StringType.VARCHAR, 10, Table.IndexType.INDEX);
            table_udnoectc_orders.AddColumnList("服藥順序", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("藥碼", Table.StringType.VARCHAR, 10, Table.IndexType.INDEX);
            table_udnoectc_orders.AddColumnList("藥名", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("警示", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("劑量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("途徑", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("頻次", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("儲位1", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("儲位2", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("數量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table_udnoectc_orders.AddColumnList("處方開始時間", Table.DateType.DATETIME, 10, Table.IndexType.INDEX);
            table_udnoectc_orders.AddColumnList("處方結束時間", Table.DateType.DATETIME, 10, Table.IndexType.INDEX);
            if (!sQLControl_udnoectc_orders.IsTableCreat()) sQLControl_udnoectc_orders.CreatTable(table_udnoectc_orders);
            else sQLControl_udnoectc_orders.CheckAllColumnName(table_udnoectc_orders, true);
            tables.Add(table_udnoectc_orders);


            Table table_udnoectc_ctcvars = new Table("udnoectc_ctcvars");
            table_udnoectc_ctcvars.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table_udnoectc_ctcvars.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);       
            table_udnoectc_ctcvars.AddColumnList("藥名", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc_ctcvars.AddColumnList("變異時間", Table.DateType.DATETIME, 10, Table.IndexType.None);
            table_udnoectc_ctcvars.AddColumnList("變異原因", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc_ctcvars.AddColumnList("變異內容", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table_udnoectc_ctcvars.AddColumnList("說明", Table.StringType.VARCHAR, 200, Table.IndexType.None);

            if (!sQLControl_udnoectc_ctcvars.IsTableCreat()) sQLControl_udnoectc_ctcvars.CreatTable(table_udnoectc_ctcvars);
            else sQLControl_udnoectc_ctcvars.CheckAllColumnName(table_udnoectc_ctcvars, true);
            tables.Add(table_udnoectc_ctcvars);

            return tables.JsonSerializationt(true);
        }
    }
}
