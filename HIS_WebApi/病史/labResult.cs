using Basic;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MyUI;
using SQLUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi.病史
{
    [Route("api/[controller]")]
    [ApiController]
    public class labResult : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "檢測項目物件", typeof(temperatureClass))]
        /// <summary>
        ///初始化檢測項目資料庫
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
        public string init()
        {
            returnData returnData = new returnData();
            try
            {
                return CheckCreatTable();
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"{ex.Message}";
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 新增檢測資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///         "PATCODE": "病歷號",
        ///         "DIACODE":"檢驗項目代碼",
        ///         "INSCODE":"檢驗醫令代碼",
        ///         "EGNAME":"檢驗項目",
        ///         "STATE": "檢驗結果",
        ///         "REPDTTM": "檢驗時間",
        ///     },
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
        public string add([FromBody] returnData returnData)
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
                List<labResultClass> labResultClasses = returnData.Data.ObjToClass<List<labResultClass>>();
                if (labResultClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.Data資料錯誤，須為 [{labResultClass}]!";
                    return returnData.JsonSerializationt();
                }

                List<string> list_病歷號 = labResultClasses.Select(x => x.病歷號).Distinct().ToList();
                string 病歷號 = string.Join(",", list_病歷號.Select(g => $"'{g}'")); 

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "labResult", UserName, Password, Port, SSLMode);
                string command = $@"
                    SELECT *
                    FROM {DB}.labResult t1
                    INNER JOIN (
                        SELECT 病歷號, 檢驗項目代碼, 檢驗項目, MAX(檢驗時間) AS MaxTime
                        FROM {DB}.labResult
                        WHERE 病歷號 IN ({病歷號})
                        GROUP BY 病歷號, 檢驗項目代碼, 檢驗項目
                    ) t2
                    ON t1.病歷號 = t2.病歷號
                    AND t1.檢驗項目代碼 = t2.檢驗項目代碼
                    AND t1.檢驗項目 = t2.檢驗項目
                    AND t1.檢驗時間 = t2.MaxTime;";
                DataTable dataTable = sQLControl.WtrteCommandAndExecuteReader(command);
                List<object[]> list_labResult = dataTable.DataTableToRowList();
                List<labResultClass> dbLabResults = list_labResult.SQLToClass<labResultClass, enum_labResult>();
                List<labResultClass> add_labResult = new List<labResultClass>();


                foreach (labResultClass labResult in labResultClasses)
                {
                    bool isDuplicate = dbLabResults.Any(db =>
                        db.病歷號 == labResult.病歷號 &&
                        db.檢驗項目代碼 == labResult.檢驗項目代碼 &&
                        db.檢驗項目 == labResult.檢驗項目 &&
                        db.檢驗時間.StringToDateTime() == labResult.檢驗時間.StringToDateTime());

                    if (isDuplicate == false)
                    {
                        labResult.GUID = Guid.NewGuid().ToString();
                        labResult.加入時間 = DateTime.Now.ToDateTimeString();
                        add_labResult.Add(labResult);
                    }
                }
                List<object[]> add = add_labResult.ClassToSQL<labResultClass, enum_labResult>();
                if(add.Count > 0) sQLControl.AddRows(null, add);

                returnData.Code = 200;
                returnData.Data = labResultClasses;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add";
                returnData.Result = $"資料寫入共{add_labResult.Count}筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Table 'dbvm.labresult' doesn't exist") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以病歷號取得最新檢測資料
        /// </summary>
        /// <remarks>
        /// <code>
        ///   {
        ///     "Data": 
        ///     {
        ///     },
        ///     "Value": "",
        ///     "ValueAry":["病歷號"]
        ///     "TableName": "",
        ///     "ServerName": "",
        ///     "ServerType": "",
        ///     "TimeTaken": ""
        ///   }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>

        [HttpPost("get_by_PATCODE")]
        public string get_by_PATCODE([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry資料錯誤!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = "returnData.ValueAry，須為 [\"病歷號\"]!";
                    return returnData.JsonSerializationt();
                }

                string 病歷號 = $"'{returnData.ValueAry[0]}'";

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "labResult", UserName, Password, Port, SSLMode);
                string command = $@"
                    SELECT *
                    FROM {DB}.labResult t1
                    INNER JOIN (
                        SELECT 病歷號, 檢驗項目代碼, 檢驗項目, MAX(檢驗時間) AS MaxTime
                        FROM {DB}.labResult
                        WHERE 病歷號 IN ({病歷號})
                        GROUP BY 病歷號, 檢驗項目代碼, 檢驗項目
                    ) t2
                    ON t1.病歷號 = t2.病歷號
                    AND t1.檢驗項目代碼 = t2.檢驗項目代碼
                    AND t1.檢驗項目 = t2.檢驗項目
                    AND t1.檢驗時間 = t2.MaxTime;";
                DataTable dataTable = sQLControl.WtrteCommandAndExecuteReader(command);
                List<object[]> list_labResult = dataTable.DataTableToRowList();
                List<labResultClass> dbLabResults = list_labResult.SQLToClass<labResultClass, enum_labResult>();
                
                returnData.Code = 200;
                returnData.Data = dbLabResults;
                returnData.TimeTaken = myTimerBasic.ToString();
                returnData.Method = "add";
                returnData.Result = $"取得資料共{dbLabResults.Count}筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Table 'dbvm.labresult' doesn't exist") init();
                returnData.Code = -200;
                returnData.Result = ex.Message;
                return returnData.JsonSerializationt(true);
            }
        }
        
        private string CheckCreatTable()
        {
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
            if (sys_serverSettingClasses.Count == 0)
            {
                returnData returnData = new returnData();
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }

            List<Table> tables = new List<Table>();
            tables.Add(MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_labResult()));

            return tables.JsonSerializationt(true);
        }

    }
}
