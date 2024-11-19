using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLUI;
using Basic;
using HIS_DB_Lib;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_TextVision
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCMPO : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "textVisionClass物件", typeof(textVisionClass))]
        [HttpPost("init_textVision")]
        public string init_textVision([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0], new enum_textVision());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        [HttpPost("init_med_code_srch")]
        public string init_med_code_srch([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                serverSettingClasses = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0], new enum_med_code_srch());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        private string CheckCreatTable(ServerSettingClass serverSettingClass, Enum enumInstance)
        {
            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(serverSettingClass, enumInstance);
            return table.JsonSerializationt(true);
        }
        /// <summary>
        /// 執行文字辨識
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":
        ///         [
        ///             {
        ///                 "op_id":
        ///                 "op_name":
        ///                 "bsse64":
        ///             }
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("analyze")]
        public string analyze([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "analyze";
            try 
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClass_main = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = serverSettingClass_main[0].Server;
                string DB = serverSettingClass_main[0].DBName;
                string UserName = serverSettingClass_main[0].User;
                string Password = serverSettingClass_main[0].Password;
                uint Port = (uint)serverSettingClass_main[0].Port.StringToInt32();

                List<ServerSettingClass> serverSettingClass_API = serverSettingClasses.MyFind("Main", "網頁", "API01");
                string API = serverSettingClass_API[0].Server;
                //string API_AI = API.Substring(0, API.Length - 4) + "3000";
                string API_AI = "http://192.168.43.249:3100";
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<textVisionClass> input_textVision = returnData.Data.ObjToClass<List<textVisionClass>>();
                input_textVision[0].GUID = Guid.NewGuid().ToString();
                input_textVision[0].操作時間 = DateTime.Now.ToDateTimeString();

                List<object[]> sql_textVision = input_textVision.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.AddRows(null, sql_textVision);
                returnData returnData_AI = textVisionClass.ai_analyze(API_AI,input_textVision);
                if(returnData_AI == null)
                {
                    returnData.Code = 200;
                    returnData.Result = $"AI辨識未啟動";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClass_AI = returnData_AI.Data.ObjToClass<List<textVisionClass>>();
                if (returnData_AI.Result == "False" )
                {
                    returnData.Code = -200;
                    returnData.Result = $"辨識失敗";
                    return returnData.JsonSerializationt(true);
                }
                if(textVisionClass_AI[0].中文名 != null)
                {
                    string pattern1 = @"^[A-Za-z0-9]+";
                    textVisionClass_AI[0].中文名 = Regex.Replace(textVisionClass_AI[0].中文名, pattern1, "");
                }
                if (textVisionClass_AI[0].藥名 != null)
                {
                    string pattern2 = @"^\d+";
                    textVisionClass_AI[0].藥名 = Regex.Replace(textVisionClass_AI[0].藥名, pattern2, "");
                }
                    

                string 藥名 = textVisionClass_AI[0].藥名;
                string 中文名 = textVisionClass_AI[0].中文名;
                List<medClass> medClasses = medClass.get_med_clouds_by_name(API, 藥名);
                if (medClasses.Count == 0)
                {
                    SQLControl sQLControl_medCodeSrch = new SQLControl(Server, DB, "med_code_srch", UserName, Password, Port, SSLMode);
                    List<object[]> list_medCodeSrchClass = sQLControl_medCodeSrch.GetAllRows(null);
                    List<medCodeSrchClass> medCodeSrchClasses = list_medCodeSrchClass.SQLToClass<medCodeSrchClass, enum_med_code_srch>();
                    List < medCodeSrchClass > buff_medCodeSrch = medCodeSrchClasses
                    .Where(temp => temp.辨識中文名 == 中文名 || temp.辨識藥名 == 藥名).ToList();
                    if (buff_medCodeSrch.Count == 1)
                    {            
                        textVisionClass_AI[0].藥品碼 = buff_medCodeSrch[0].藥品碼;
                        textVisionClass_AI[0].藥名 = buff_medCodeSrch[0].藥名;
                        textVisionClass_AI[0].中文名 = buff_medCodeSrch[0].中文名;
                    }
                    else
                    {
                        textVisionClass_AI[0].藥品碼 = "";
                    }
                }
                else
                {
                    textVisionClass_AI[0].藥品碼 = medClasses[0].藥品碼;
                    textVisionClass_AI[0].藥名 = medClasses[0].藥品學名;
                    textVisionClass_AI[0].中文名 = medClasses[0].中文名稱;
                }

                textVisionClass_AI[0].操作者ID = input_textVision[0].操作者ID;
                textVisionClass_AI[0].操作者姓名 = input_textVision[0].操作者姓名;
                textVisionClass_AI[0].操作時間 = input_textVision[0].操作時間;
                textVisionClass_AI[0].圖片 = input_textVision[0].圖片;
                //textVisionClass_AI[0].批號信心分數 = textVisionClass_AI[0].批號信心分數.Substring(0, 5);
                //textVisionClass_AI[0].效期信心分數 = textVisionClass_AI[0].效期信心分數.Substring(0, 5);
                //textVisionClass_AI[0].單號信心分數 = textVisionClass_AI[0].單號信心分數.Substring(0, 5);
                //textVisionClass_AI[0].藥名信心分數 = textVisionClass_AI[0].藥名信心分數.Substring(0, 5);
                //textVisionClass_AI[0].中文名信心分數 = textVisionClass_AI[0].中文名信心分數.Substring(0, 5);

                List<object[]> obj_textVisionClass = textVisionClass_AI.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.UpdateByDefulteExtra(null, obj_textVisionClass);

                obj_textVisionClass = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, textVisionClass_AI[0].GUID);
                textVisionClass_AI = obj_textVisionClass.SQLToClass<textVisionClass, enum_textVision>();

                returnData.Code = 200;
                returnData.Data = textVisionClass_AI;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "";
                returnData.Result = $"";
                return returnData.JsonSerializationt(true);
            }
            catch(Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }
            

        }
        /// <summary>
        /// 更新文字辨識資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":
        ///         [
        ///             {
        ///                 "GUID":"",
        ///                 "name":"",
        ///                 "cht_name":"",
        ///                 "batch_num":"",
        ///                 "po_num":
        ///                 "qty":"",
        ///                 "expirydate":"",
        ///                 "code":"" 
        ///             }
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_textvision")]
        public string update_textvision([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_textvision";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClass_main = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = serverSettingClass_main[0].Server;
                string DB = serverSettingClass_main[0].DBName;
                string UserName = serverSettingClass_main[0].User;
                string Password = serverSettingClass_main[0].Password;
                uint Port = (uint)serverSettingClass_main[0].Port.StringToInt32();

                if (returnData.Data == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.Data 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);

                List<textVisionClass> input_textVision = returnData.Data.ObjToClass<List<textVisionClass>>();
                string GUID = input_textVision[0].GUID;
                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();

                if (textVisionClasses[0].批號 != input_textVision[0].批號) textVisionClasses[0].批號 = input_textVision[0].批號;
                if (textVisionClasses[0].單號 != input_textVision[0].單號) textVisionClasses[0].單號 = input_textVision[0].單號;
                if (textVisionClasses[0].數量 != input_textVision[0].數量) textVisionClasses[0].數量 = input_textVision[0].數量;
                if (textVisionClasses[0].效期 != input_textVision[0].效期) textVisionClasses[0].效期 = input_textVision[0].效期;
                if (textVisionClasses[0].藥品碼 != input_textVision[0].藥品碼) textVisionClasses[0].藥品碼 = input_textVision[0].藥品碼;
                if (textVisionClasses[0].藥名 != input_textVision[0].藥名) textVisionClasses[0].藥名 = input_textVision[0].藥名;
                if (textVisionClasses[0].中文名 != input_textVision[0].中文名) textVisionClasses[0].中文名 = input_textVision[0].中文名;

                List<object[]> Update_textVision = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.UpdateByDefulteExtra(null, Update_textVision);

                returnData.Code = 200;
                returnData.Data = textVisionClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"編輯<{Update_textVision.Count}>筆";
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
        /// 更新檢索表
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":
        ///         [
        ///            {
        ///                 "Master_GUID":""
        ///                 "recog_cht_name":""
        ///                 "recog_name":""
        ///                 "code":""
        ///            }
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        [HttpPost("update_med_code_srch")]
        public string update_med_code_srch([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_med_code_srch";
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClass_main = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = serverSettingClass_main[0].Server;
                string DB = serverSettingClass_main[0].DBName;
                string UserName = serverSettingClass_main[0].User;
                string Password = serverSettingClass_main[0].Password;
                uint Port = (uint)serverSettingClass_main[0].Port.StringToInt32();

                List<ServerSettingClass> serverSettingClass_API = serverSettingClasses.MyFind("Main", "網頁", "API01");
                string API = serverSettingClass_API[0].Server;

                if (returnData.Data == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.Date 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                List<medCodeSrchClass> input_medCodeSrch = returnData.Data.ObjToClass<List<medCodeSrchClass>>();

                string 辨識中文名 = input_medCodeSrch[0].辨識中文名;
                string 辨識藥名 = input_medCodeSrch[0].辨識藥名;
                string code = input_medCodeSrch[0].藥品碼;

                SQLControl sQLControl_medCodeSrch = new SQLControl(Server, DB, "med_code_srch", UserName, Password, Port, SSLMode);
                List<object[]> list_medCodeSrchClass = sQLControl_medCodeSrch.GetAllRows(null);
                List<medCodeSrchClass> medCodeSrchClasses = list_medCodeSrchClass.SQLToClass<medCodeSrchClass, enum_med_code_srch>();
                List<medCodeSrchClass> buff_medCodeSrch = medCodeSrchClasses
                    .Where(temp => temp.辨識中文名 == 辨識中文名 || temp.辨識藥名 == 辨識藥名).ToList();
                if (buff_medCodeSrch.Count != 0)
                {
                    returnData.Data = "";
                    returnData.Code = 200;
                    returnData.Result = "藥名已存在";
                    return returnData.JsonSerializationt(true);
                }
                medClass medClass = medClass.get_med_clouds_by_code(API, code);
                if(medClass == null)
                {
                    returnData.Code = 200;
                    returnData.Result = "藥碼不存在";
                    return returnData.JsonSerializationt(true);
                }

                input_medCodeSrch[0].GUID = Guid.NewGuid().ToString();
                input_medCodeSrch[0].藥名 = medClass.藥品名稱;
                input_medCodeSrch[0].中文名 = medClass.中文名稱;
                input_medCodeSrch[0].操作時間 = DateTime.Now.ToDateTimeString();

                List<object[]> add_medCodeSrchClass = input_medCodeSrch.ClassToSQL<medCodeSrchClass, enum_med_code_srch>();
                sQLControl_medCodeSrch.AddRows(null, add_medCodeSrchClass);

                returnData.Data = input_medCodeSrch;
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"新增<{add_medCodeSrchClass.Count}>筆";
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
        /// 以GUID刪除textVision資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///             "GUID"
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("delete_textVision")]
        public string delete_textVision([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<ServerSettingClass> serverSettingClass_main = serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = serverSettingClass_main[0].Server;
                string DB = serverSettingClass_main[0].DBName;
                string UserName = serverSettingClass_main[0].User;
                string Password = serverSettingClass_main[0].Password;
                uint Port = (uint)serverSettingClass_main[0].Port.StringToInt32();


                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\"]";
                    return returnData.JsonSerializationt(true);
                }

                string GUID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                sQLControl_textVision.DeleteExtra(null, list_textVision);

                returnData.Code = 200;
                returnData.Data = list_textVision;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "delete";
                returnData.Result = $"刪除<{list_textVision.Count}>筆";
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
}
