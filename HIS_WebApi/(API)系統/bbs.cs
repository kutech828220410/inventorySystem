using Basic;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SQLUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_系統
{
    [Route("api/[controller]")]
    [ApiController]
    public class bbs : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        private string tableName = "bbs";

        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "bbsClass物件", typeof(bbs))]
        /// <summary>
        ///初始化資料庫
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
        public async Task<string> init([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "init";
            try
            {
                return await CheckCreatTable();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return await returnData.JsonSerializationtAsync(true);
            }
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     
        ///         {
        ///             bbsClass物件
        ///         }
        ///     
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<string> add([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                await init(returnData);
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤!";
                    return returnData.JsonSerializationt();
                }
                bbsClass bbsClass = returnData.Data.ObjToClass<bbsClass>();
                if (bbsClass == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data資料錯誤，須為 bbsClass物件!";
                    return returnData.JsonSerializationt();
                }
                if(bbsClass.公告開始時間.StringIsEmpty() || bbsClass.公告結束時間.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "公告開始時間、公告結束時間不得為空";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = await HIS_WebApi.Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                bbsClass.GUID = Guid.NewGuid().ToString();
                bbsClass.建立時間 = DateTime.Now.ToDateTimeString();
                object[] add = bbsClass.ClassToSQL<bbsClass, enum_bbs>();               
                await sQLControl.AddRowAsync(null, add);


                returnData.Code = 200;
                returnData.Data = bbsClass;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add";
                returnData.Result = $"資料寫入成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     
        ///         {
        ///             bbsClass物件
        ///         }
        ///     
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<string> update([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤!";
                    return returnData.JsonSerializationt();
                }
                bbsClass bbsClass_input = returnData.Data.ObjToClass<bbsClass>();
                if (bbsClass_input == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data資料錯誤，須為 bbsClass物件!";
                    return returnData.JsonSerializationt();
                }
                if (bbsClass_input.公告開始時間.StringIsEmpty() || bbsClass_input.公告結束時間.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "公告開始時間、公告結束時間不得為空";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = await HIS_WebApi.Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl.GetRowsByDefultAsync(null,(int)enum_bbs.GUID, bbsClass_input.GUID);
                if(objects.Count() == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無此GUID({bbsClass_input.GUID}) 資料";
                    return returnData.JsonSerializationt();
                }
                bbsClass bbs = objects[0].SQLToClass<bbsClass, enum_bbs>();
                if (bbsClass_input.主旨.StringIsEmpty() == false && bbsClass_input.主旨 != bbs.主旨) bbs.主旨 = bbsClass_input.主旨;
                if (bbsClass_input.內容.StringIsEmpty() == false && bbsClass_input.內容 != bbs.內容) bbs.內容 = bbsClass_input.內容;
                if (bbsClass_input.重要程度.StringIsEmpty() == false && bbsClass_input.重要程度 != bbs.重要程度) bbs.重要程度 = bbsClass_input.重要程度;
                if (bbsClass_input.建立人員科別.StringIsEmpty() == false && bbsClass_input.建立人員科別 != bbs.建立人員科別) bbs.建立人員科別 = bbsClass_input.建立人員科別;
                if (bbsClass_input.建立人員姓名.StringIsEmpty() == false && bbsClass_input.建立人員姓名 != bbs.建立人員姓名) bbs.建立人員姓名 = bbsClass_input.建立人員姓名;
                if (bbsClass_input.建立人員姓名.StringIsEmpty() == false && bbsClass_input.建立人員姓名 != bbs.建立人員姓名) bbs.建立人員姓名 = bbsClass_input.建立人員姓名;          
                if (bbsClass_input.公告開始時間.StringIsEmpty() == false && bbsClass_input.公告開始時間 != bbs.公告開始時間) bbs.公告開始時間 = bbsClass_input.公告開始時間;
                if (bbsClass_input.公告結束時間.StringIsEmpty() == false && bbsClass_input.公告結束時間 != bbs.公告結束時間) bbs.公告結束時間 = bbsClass_input.公告結束時間;
                bbs.建立時間 = DateTime.Now.ToDateTimeString();


                object[] update = bbs.ClassToSQL<bbsClass, enum_bbs>();
                await sQLControl.UpdateRowAsync(null, update);


                returnData.Code = 200;
                returnData.Data = bbs;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "update";
                returnData.Result = $"資料更新成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     
        ///         {
        ///             bbsClass物件
        ///         }
        ///     
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<string> delete([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.Data == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.Data資料錯誤!";
                    return returnData.JsonSerializationt();
                }
                bbsClass bbsClass_input = returnData.Data.ObjToClass<bbsClass>();
                if (bbsClass_input == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data資料錯誤，須為 bbsClass物件!";
                    return returnData.JsonSerializationt();
                }
                (string Server, string DB, string UserName, string Password, uint Port) = await HIS_WebApi.Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                List<object[]> objects = await sQLControl.GetRowsByDefultAsync(null, (int)enum_bbs.GUID, bbsClass_input.GUID);
                if (objects.Count() == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無此GUID({bbsClass_input.GUID}) 資料";
                    return returnData.JsonSerializationt();
                }
                bbsClass bbs = objects[0].SQLToClass<bbsClass, enum_bbs>();
      
                object[] delete = bbs.ClassToSQL<bbsClass, enum_bbs>();
                await sQLControl.DeleteRowAsync(null, delete);


                returnData.Code = 200;
                returnData.Data = bbs;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "delete";
                returnData.Result = $"資料刪除成功!";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 取得公告時間區間內的資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     [
        ///         {
        ///         }
        ///     ]
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_by_startendtime")]
        public async Task<string> get_by_startendtime([FromBody] returnData returnData, CancellationToken ct = default)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                await init(returnData);
                string now = DateTime.Now.ToDateTimeString();
        
                (string Server, string DB, string UserName, string Password, uint Port) = await HIS_WebApi.Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);
                string command = @$"SELECT * FROM {DB}.{tableName}
                                    WHERE 公告開始時間 <= '{now}' 
                                    AND 公告結束時間 >= '{now}' ;";
                List<object[]> objects = await sQLControl.WriteCommandAsync(command,ct);
                List<bbsClass> bbsClasses = objects.SQLToClass<bbsClass, enum_bbs>();
                bbsClasses.Sort(new bbsClass.ICP_By_ct_time());

                returnData.Code = 200;
                returnData.Data = bbsClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_by_startendtime";
                returnData.Result = $"取得公告時間內資料，共{bbsClasses.Count}筆!";
                return await returnData.JsonSerializationtAsync(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return await returnData.JsonSerializationtAsync(true);
            }
        }
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     [
        ///         {
        ///         }
        ///     ]
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_all")]
        public async Task<string> get_all([FromBody] returnData returnData, CancellationToken ct = default)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                (string Server, string DB, string UserName, string Password, uint Port) = await HIS_WebApi.Method.GetServerInfoAsync("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, tableName, UserName, Password, Port, SSLMode);

                List<object[]> objects = await sQLControl.GetAllRowsAsync(null);
                List<bbsClass> bbsClasses = objects.SQLToClass<bbsClass, enum_bbs>();
                bbsClasses.Sort(new bbsClass.ICP_By_ct_time());

                returnData.Code = 200;
                returnData.Data = bbsClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_by_startendtime";
                returnData.Result = $"取得資料，共{bbsClasses.Count}筆!";
                return await returnData.JsonSerializationtAsync(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return await returnData.JsonSerializationtAsync(true);
            }
        }
        /// <summary>
        /// 取得公告事件優先程級
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     [
        ///         {
        ///         }
        ///     ]
        ///     "Value": "",
        ///     "ValueAry":[]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_priority")]
        public async Task<string> get_priority([FromBody] returnData returnData, CancellationToken ct = default)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<string> level = System.Enum.GetNames(typeof(enum_bbs_ReportLevel)).ToList();
                returnData.Code = 200;
                returnData.Data = level;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "get_priority";
                returnData.Result = $"取得優先層級!";
                return await returnData.JsonSerializationtAsync(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return await returnData.JsonSerializationtAsync(true);
            }
        }


        private async Task<string> CheckCreatTable()
        {
            List<sys_serverSettingClass> sys_serverSettingClasses = await ServerSettingController.GetAllServerSettingasync("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData returnData = new returnData();
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }

            List<Table> tables = new List<Table>();

            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_bbs()));

            return tables.JsonSerializationt(true);
        }

    }
}
