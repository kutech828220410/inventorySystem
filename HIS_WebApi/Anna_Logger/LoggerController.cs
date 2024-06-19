using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLUI;
using Basic;
using HIS_DB_Lib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi.Anna_Logger
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        [HttpGet("test")]
        public string test()
        {
            try
            {
                return "連線成功!";
            }
            catch (Exception ex)
            {
                return $"Exception : {ex.Message}";
            }
        }
        /// <summary>
        ///初始化Logger資料庫
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[""]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "loggerClass物件", typeof(loggerClass))]
        public string init([FromBody] returnData returnData)
        {
            try
            {
                List<Table> tables = TableManager.CheckCreateTable();
                Table table = tables.GetTable(new enum_logger());
                if (table == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "table 空白，請檢查使用者!";
                    return returnData.JsonSerializationt();
                }
                //returnData.Result = "創建表單成功";
                //returnData.Method = "init";
                //returnData.Data = table;

                return table.JsonSerializationt();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        ///取得所有Logger資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///     {
        ///         "ValueAry":[項目]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_all")]
        public string POST_get_all([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_all";
            try
            {
                Table table = new Table(new enum_logger());
                string tableName = table.TableName;
                SQLControl sqlControl = new SQLControl("127.0.0.1", "anna_test", "user", "66437068");
                List<object[]> row_value = sqlControl.GetAllRows(tableName);
                List<loggerClass> loggerClasses = row_value.SQLToClass<loggerClass, enum_logger>();

                loggerClasses.Sort(new loggerClass.ICP_By_OP_Time());
                returnData.Result = $"取得資料共<{loggerClasses.Count}>筆";
                returnData.Data = loggerClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":
        ///         [
        ///             [loggerClass]
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add")]
        public string POST_add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                Table table = new Table(new enum_logger());
                SQLControl sqlControl = new SQLControl("127.0.0.1", "anna_test", "user", "66437068");
                List<loggerClass> profile_sql_add = new List<loggerClass>();
                List<loggerClass> profile_input = returnData.Data.ObjToClass<List<loggerClass>>();

                for (int i = 0; i < profile_input.Count; i++)
                {
                    string GUID = Guid.NewGuid().ToString();

                    loggerClass loggerClass = profile_input[i];
                    loggerClass.GUID = GUID;
                    loggerClass.操作時間 = DateTime.Now.ToDateTimeString();
                    profile_sql_add.Add(loggerClass);

                }

                List<object[]> list_profile_add = new List<object[]>();
                list_profile_add = profile_sql_add.ClassToSQL<loggerClass, enum_logger>();
                if (list_profile_add.Count > 0) sqlControl.AddRows(table.TableName, list_profile_add);
                returnData.Data = "";
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "add";
                returnData.Result = $"新增<{list_profile_add.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        /// <summary>
        ///以操作者姓名取得Logger資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///     {
        ///         "ValueAry": 
        ///         [
        ///            "操作者姓名"
        ///         ]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_log_by_opname")]
        public string POST_get_log_by_opname([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();

            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.ValueAry 資料結構異常";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.ValueAry 需為 [操作者姓名]";
                    return returnData.JsonSerializationt();
                }
                string 操作者姓名 = returnData.ValueAry[0];

                Table table = new Table(new enum_logger());
                string tableName = table.TableName;


                SQLControl sQLControl = new SQLControl("127.0.0.1", "anna_test", "user", "66437068");

                List<object[]> rows_value = sQLControl.GetRowsByDefult(tableName, (int)enum_logger.操作者姓名, 操作者姓名);
                if (rows_value.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料 操作者姓名 : {操作者姓名}";
                    return returnData.JsonSerializationt();
                }

                List<loggerClass> loggerClasses = rows_value.SQLToClass<loggerClass, enum_logger>();

                loggerClasses.Sort(new loggerClass.ICP_By_OP_Time());

                returnData.Result = $"取得資料共<{loggerClasses.Count}>筆";
                returnData.Data = loggerClasses;
                returnData.Method = "get_log_by_opname";
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        /// <summary>
        ///以操作時間取得Logger資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[起始時間, 結束時間]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_log_by_datetime_st_end")]
        public string POST_get_log_by_datetime_st_end([FromBody] returnData returnData)
        {

            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnDate.ValueAry 空白，請輸入時間!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnDate.ValueAry 需為[起始時間,結束時間]";
                    return returnData.JsonSerializationt();
                }
                Table table = new Table(new enum_logger());
                string tableName = table.TableName;

                string 起始時間 = returnData.ValueAry[0];
                string 結束時間 = returnData.ValueAry[1];

                DateTime date_st = 起始時間.StringToDateTime();
                DateTime date_end = 結束時間.StringToDateTime();

                SQLControl sQLControl = new SQLControl("127.0.0.1", "anna_test", "user", "66437068");
                List<object[]> rows_value = sQLControl.GetRowsByBetween(tableName, (int)enum_logger.操作時間, date_st.ToDateString(), date_end.ToDateString());
                List<loggerClass> loggerClasses = rows_value.SQLToClass<loggerClass, enum_logger>();

                var sortedLoggerClasses = (from data in loggerClasses
                                           let parseDateTime = DateTime.Parse(data.操作時間)
                                           orderby parseDateTime
                                           select data).ToList();

                returnData.Result = $"取得資料共<{loggerClasses.Count}>筆";
                returnData.Data = sortedLoggerClasses;
                returnData.Method = "get_log_by_datetime_st_end";
                returnData.TimeTaken = $"myTimerBasic";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        ///以項目刪除Logger資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":[項目]
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("delete_by_item")]
        public string Post_delete([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnDate.ValueAry 空白，請輸入時間!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnDate.ValueAry 需為[項目]";
                    return returnData.JsonSerializationt();
                }
                Table table = new Table(new enum_logger());
                SQLControl sQLControl = new SQLControl("127.0.0.1", "anna_test", "user", "66437068");

                string value = returnData.ValueAry[0];
                string tableName = table.TableName;
                List<object[]> row_values = sQLControl.GetRowsByDefult(tableName, (int)enum_logger.項目, value);
                if (row_values.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無資料 項目 : {value}";
                    return returnData.JsonSerializationt();
                }

                sQLControl.DeleteExtra(tableName, row_values);
                returnData.Result = $"刪除資料共<{row_values.Count}>筆";
                returnData.Data = "";
                returnData.Method = "delete_by_item";
                returnData.TimeTaken = $"myTimerBasic";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }


        }


    }

    public static class TableMethod
    {
        public static Table GetTable(this List<Table> tables, Enum _enum)
        {
            string name = _enum.GetEnumDescription();
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].TableName == name) return tables[i];
            }
            return null;
        }
    }
    static public class TableManager
    {
        static public List<Table> CheckCreateTable()
        {
            List<Table> tables = new List<Table>();
            tables.Add(CheckCreateTable("127.0.0.1", "anna_test", "user", "66437068", 3306, new enum_logger()));
            return tables;
        }


        static public Table CheckCreateTable(string server, string db, string user, string password, uint port, Enum Enum)
        {
            Table table = new Table(Enum);

            string Server = server;
            string DB = db;
            string UserName = user;
            string Password = password;
            uint Port = port;
            table.Server = Server;
            table.DBName = DB;
            table.Username = UserName;
            table.Password = Password;
            table.Port = Port.ToString();

            SQLControl sQLControl = new SQLControl(Server, DB, UserName, Password, Port, MySql.Data.MySqlClient.MySqlSslMode.Disabled);

            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
            else sQLControl.CheckAllColumnName(table, true);
            return table;
        }

    }
}
