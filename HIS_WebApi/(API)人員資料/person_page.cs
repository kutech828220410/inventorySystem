﻿using System;
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
    public class person_page : ControllerBase
    {

        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(personPageClass))]
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        /// 
        [Route("init")]
        [HttpPost]
        public string POST_init(returnData returnData)
        {
            try
            {
                List<sys_serverSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                else serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");

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
        /// <summary>
        /// 取得全部人員資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data][personPageClass陣列]</returns>
        [HttpPost]
        public string Get([FromBody] returnData returnData)
        {
            return get_all(returnData);
        }
        /// <summary>
        /// 取得全部人員資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     }
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data][personPageClass陣列]</returns>
        [Route("get_all")]
        [HttpPost]
        public string get_all([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_all";
            try
            {

                List<sys_serverSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                else serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");

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
        /// <summary>
        /// 以ID搜尋人員資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[ID]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("serch_by_id")]
        [HttpPost]
        public string POST_serch_by_id([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "serch_by_id";
            try
            {

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                    else sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string ID = returnData.Value;
                if (ID.StringIsEmpty())
                {

                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_personPage = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_personPage.GetRowsByDefult(null, (int)enum_人員資料.ID, ID);
                if (list_value.Count > 0)
                {
                    returnData.Data = list_value[0].SQLToClass<personPageClass, enum_人員資料>();
                }
                else
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
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
        /// <summary>
        /// 以姓名搜尋人員資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        
        ///     },
        ///     "Value" : "[姓名]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("serch_by_name")]
        [HttpPost]
        public string POST_serch_by_name([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "serch_by_name";
            try
            {

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                    else sys_serverSettingClasses = sys_serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "人員資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }
                string 姓名 = returnData.Value;
                if (姓名.StringIsEmpty())
                {

                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_personPage = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_personPage.GetRowsByDefult(null, (int)enum_人員資料.姓名, 姓名);
                if (list_value.Count > 0)
                {
                    returnData.Data = list_value[0].SQLToClass<personPageClass, enum_人員資料>();
                }
                else
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料";
                    return returnData.JsonSerializationt();
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
        /// <summary>
        /// 新增及修改人員資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///        [personPageClass陣列]
        ///     },
        ///     "Value" : "[ID]"
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("add")]
        [HttpPost]
        public string POST_add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "serch_by_add";
            try
            {

                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                if (returnData.ServerType.StringIsEmpty() || returnData.ServerName.StringIsEmpty()) sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                string Server = sys_serverSettingClasses[0].Server;
                string DB = sys_serverSettingClasses[0].DBName;
                string UserName = sys_serverSettingClasses[0].User;
                string Password = sys_serverSettingClasses[0].Password;
                uint Port = (uint)sys_serverSettingClasses[0].Port.StringToInt32();
                SQLControl sQLControl_personPage = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);
                List<object[]> list_value = sQLControl_personPage.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_src = returnData.Data.ObjToListSQL<personPageClass, enum_人員資料>();
                List<object[]> list_add = new List<object[]>();
                List<object[]> list_replace = new List<object[]>();
                for (int i = 0; i < list_src.Count; i++)
                {
                    string ID = list_src[i][(int)enum_人員資料.ID].ObjectToString();
                    list_value_buf = list_value.GetRows((int)enum_人員資料.ID, ID);
                    if (list_value_buf.Count > 0)
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
        /// <summary>
        /// 刪除人員資料
        /// </summary>
        /// <remarks>
        ///  --------------------------------------------<br/> 
        /// 以下為範例JSON範例
        /// <code>
        ///   {
        ///     "ValueAry" : ["GUID1;GUID2"]
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[returnData.Data]</returns>
        [Route("delete")]
        [HttpPost]
        public string delete([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "delete";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ValueAry不可為空";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"ValueAry應為[\"GUID1,GUID2\"]";
                    return returnData.JsonSerializationt();
                }
                string[] GUIDs = returnData.ValueAry[0].Split(";");
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "藥檔資料");
                string API = HIS_WebApi.Method.GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl_personPage = new SQLControl(Server, DB, "person_page", UserName, Password, Port, SSLMode);

                List<object[]> list_value = sQLControl_personPage.GetAllRows(null);
                List<object[]> list_value_buf = new List<object[]>();
                List<object[]> list_delete = new List<object[]>();
                for (int i = 0; i < GUIDs.Length; i++)
                {
                    string GUID = GUIDs[i];
                    list_value_buf = list_value.GetRows((int)enum_人員資料.GUID, GUID);
                    if (list_value_buf.Count > 0)
                    {
                        list_delete.Add(list_value_buf[0]);
                    }
                }

                if (list_delete.Count > 0) sQLControl_personPage.DeleteExtra(null, list_delete);

                returnData.Code = 200;
                returnData.Result = $"更動人員資料成功,刪除<{list_delete.Count}>筆!";
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


        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass)
        {
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

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
            table.AddColumnList("指紋辨識", Table.StringType.VARCHAR, 1000, Table.IndexType.None);
            table.AddColumnList("指紋ID", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("開門權限", Table.StringType.VARCHAR, 300, Table.IndexType.None);

            table.Server = Server;
            table.DBName = DB;
            table.Username = UserName;
            table.Password = Password;
            table.Port = Port.ToString();

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
