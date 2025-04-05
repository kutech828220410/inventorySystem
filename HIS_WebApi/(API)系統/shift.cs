using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using MySql.Data.MySqlClient;
using SQLUI;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using H_Pannel_lib;
using HIS_WebApi._API_藥品資料;
using K4os.Compression.LZ4.Internal;
using MyOffice;
using MySqlX.XDevAPI.Relational;
using NPOI.HPSF;
using NPOI.Util;
using System.Xml;


namespace HIS_WebApi._API_系統
{
    [Route("api/[controller]")]
    [ApiController]
    public class shift : ControllerBase
    {
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Route("init")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(shiftClass))]
        [HttpPost]
        public string init([FromBody] returnData returnData)
        {
            try
            {
                returnData.TableName = "shift";
                return CheckCreatTable(returnData);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 取得所有班別內容
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[medGroupClasses]</returns>
        [Route("get_all")]
        [HttpPost]
        public string get_all([FromBody] returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                init(returnData);
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "人員資料");                       
                SQLControl sQLControl = new SQLControl(Server, DB, "shift", UserName, Password, Port, SSLMode);
                List<object[]> list_shift = sQLControl.GetAllRows(null);
                List<shiftClass> shiftClasses = list_shift.SQLToClass<shiftClass,enum_shift>();

                if (shiftClasses.Count == 0) 
                {
                    shiftClasses = AddDefaultData(sQLControl);
                }
                shiftClasses.Sort(new shiftClass.ICP_By_name());
                returnData.Code = 200;
                returnData.Data = shiftClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "get_all";
                returnData.Result = $"取得班別資料成功,共{shiftClasses.Count}!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        /// <summary>
        /// 更新班別時間
        /// </summary>
        /// <remarks>
        /// {
        ///     "ValueAry":["班別","開始時間","結束時間"]
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns>[medGroupClasses]</returns>
        [Route("update")]
        [HttpPost]
        public string update([FromBody] returnData returnData)
        {
            try
            {               
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                if(returnData.ValueAry == null)
                {
                    returnData.Code = -200;
                    returnData.Result = "ValueAry無資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 3)
                {
                    returnData.Code = -200;
                    returnData.Result = "ValueAry應為[\"班別\",\"開始時間\",\"結束時間\"]!";
                    return returnData.JsonSerializationt();
                }
                string 班別 = returnData.ValueAry[0];
                string 開始時間 = returnData.ValueAry[1];
                string 結束時間 = returnData.ValueAry[2];
                bool isValid = IsValidTimeRange(開始時間, 結束時間, out string error);

                if (isValid == false)
                {
                    returnData.Code = -200;
                    returnData.Result = error;
                    return returnData.JsonSerializationt();
                }
                
                (string Server, string DB, string UserName, string Password, uint Port) = Method.GetServerInfo("Main", "網頁", "人員資料");
                SQLControl sQLControl = new SQLControl(Server, DB, "shift", UserName, Password, Port, SSLMode);
                //string command = $@"UPDATE dbvm.shift SET start_time = '{開始時間}', end_time = '{結束時間}'WHERE shift_name = '{班別}';";
                List<object[]> list_shift = sQLControl.GetRowsByDefult(null, (int)enum_shift.班別, 班別);
                shiftClass shiftClasses = list_shift.SQLToClass<shiftClass, enum_shift>().FirstOrDefault();
                if (shiftClasses == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"班別:{班別}不存在!";
                    return returnData.JsonSerializationt();
                }
                shiftClasses.開始時間 = 開始時間;
                shiftClasses.結束時間 = 結束時間;
                List<object[]> list_shift_update = new List<shiftClass> { shiftClasses }.ClassToSQL<shiftClass, enum_shift>();
                sQLControl.UpdateByDefulteExtra(null, list_shift_update);

                returnData.Code = 200;
                returnData.Data = new List<shiftClass> { shiftClasses };
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "get_all";
                returnData.Result = $"更新{班別}時間成功!";

                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        private string CheckCreatTable(returnData returnData)
        {
            string TableName = returnData.TableName;
            SQLUI.Table table = new SQLUI.Table(TableName);
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
            if (sys_serverSettingClasses.Count == 0)
            {
                return $"找無Server資料!";
            }
            table = MethodClass.CheckCreatTable(sys_serverSettingClasses[0], new enum_shift());

            return table.JsonSerializationt(true);
        }
        private List<shiftClass> AddDefaultData(SQLControl sQLControl)
        {
            List<object[]> add_shift = default_data.ClassToSQL<shiftClass, enum_shift>();
            sQLControl.AddRows(null,add_shift);
            return default_data;
        }
        private List<shiftClass> default_data = new List<shiftClass>
        {
            new shiftClass
            {
                GUID = Guid.NewGuid().ToString(),
                班別 = "早班",
                開始時間 = "08:00:00",
                結束時間 = "15:59:59"
            },
            new shiftClass
            {
                GUID = Guid.NewGuid().ToString(),
                班別 = "小夜班",
                開始時間 = "16:00:00",
                結束時間 = "23:59:59"
            },
            new shiftClass
            {
                GUID = Guid.NewGuid().ToString(),
                班別 = "大夜班",
                開始時間 = "00:00:00",
                結束時間 = "07:59:59"
            }
        };
        public static bool IsValidTimeRange(string startTime, string endTime, out string error)
        {
            error = "";

            if (!TimeSpan.TryParse(startTime, out TimeSpan start))
            {
                error = "開始時間格式錯誤 應為 \"HH:mm:ss\"";
                return false;
            }

            if (!TimeSpan.TryParse(endTime, out TimeSpan end))
            {
                error = "結束時間格式錯誤  應為 \"HH:mm:ss\"";
                return false;
            }

            if (start > end)
            {
                error = "開始時間不能大於結束時間";
                return false;
            }

            return true;
        }




    }
}
