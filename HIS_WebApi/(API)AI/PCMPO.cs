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
using System.Drawing;
using SkiaSharp;
using System.IO;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_TextVision
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCMPO : ControllerBase
    {
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        private static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string fileDirectory = $"{currentDirectory}/log/";
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
                string API_AI = "http://220.135.128.247:3100";
                string API = GetServerAPI("Main", "網頁", "API01");
                string time = DateTime.Now.ToString("HHmmss");
                string project = "PO_Vision";
                string today = $"{DateTime.Now.ToString("yyyyMMdd")}";
                string file = $"{today}{time}";

                List<textVisionClass> input_textVision = returnData.Data.ObjToClass<List<textVisionClass>>();
                List<Task> tasks = new List<Task>();           
                returnData return_textVisionClass = textVisionClass.ai_analyze(API_AI, input_textVision);

                tasks.Add(Task.Run(new Action(delegate
                {
                    if (return_textVisionClass.Result == "False")
                    {
                        file = $"NG{today}{time}.jpg";
                    }

                    string base64 = input_textVision[0].圖片;
                    string pre = "data:image/jpeg;base64,";
                    base64 = base64.Replace(pre, "");

                    string folderPath = Path.Combine(fileDirectory, project);
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    string filePath = Path.Combine(folderPath, file);
                    byte[] imageBytes = Convert.FromBase64String(base64);
                    SKMemoryStream stream = new SKMemoryStream(imageBytes);
                    SKBitmap bitmap = SKBitmap.Decode(stream);
                    using (SKImage image = SKImage.FromBitmap(bitmap)) // 明確類型為 SKImage
                    {
                        using (SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100)) // 明確類型為 SKData
                        {
                            using (System.IO.FileStream fileStream = System.IO.File.OpenWrite(filePath)) // 明確類型為 FileStream
                            {
                                data.SaveTo(fileStream);
                            }
                        }
                    }
                })));
                List<textVisionClass> textVisionClass_AI = new List<textVisionClass>();
                List<positionClass> positionClasses = new List<positionClass>();
                if (return_textVisionClass.Result == "False")
                {
                    Task.WhenAll(tasks).Wait();
                    file = $"NG{file}";
                    returnData.Code = -200;
                    returnData.Result = $"辨識失敗 檔案名稱{file}";
                    Logger.Log(file, project, returnData.JsonSerializationt());
                    return returnData.JsonSerializationt(true);
                }
                else
                {
                    textVisionClass_AI = return_textVisionClass.Data.ObjToClass<List<textVisionClass>>();
                    textVisionClass textVision = textVisionClass_AI[0];
                    inspectionClass.content content = new inspectionClass.content();
                    if (textVision.單號.StringIsEmpty() == false)
                    {
                        content = inspectionClass.content_get_by_PON(API, textVision.單號);
                        if (content == null)
                        {
                            file = $"NG{file}";
                            returnData.Code = -200;
                            returnData.Result = $"查無對應單號資料 檔案名稱{file}";
                            Logger.Log(file, project, returnData.JsonSerializationt());
                            return returnData.JsonSerializationt(true);
                        }
                    }

                    tasks.Add(Task.Run(new Action(delegate
                    {                                                                                 
                        if (content.藥品名稱.StringIsEmpty())
                        {
                            textVision.藥名 = content.Sub_content[0].藥品名稱;
                        }
                        else
                        {
                            textVision.藥名 = content.藥品名稱;
                        }
                        textVision.藥品碼 = content.藥品碼;
                        textVision.批號 = content.Sub_content[0].批號;
                        textVision.效期 = content.Sub_content[0].效期;
                        textVision.數量 = content.應收數量;
                        List<medClass> medClasses = medClass.get_med_clouds_by_name(API, textVision.藥名);
                        if(medClasses.Count > 0)
                        {
                            textVision.中文名 = medClasses[0].中文名稱;
                        }                                                   
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        if (textVision.批號位置.StringIsEmpty() == false)
                        {
                            string[] position = textVision.批號位置.Split(";");
                            (string width, string height, string center) = GetSquare(position);
                            positionClass positionClass = new positionClass
                            {
                                高 = height,
                                寬 = width,
                                中心 = center,
                                信心分數 = textVision.批號信心分數,
                                keyWord = "batch_num",
                            };
                            positionClasses.LockAdd(positionClass);
                        }
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        if (textVision.中文名位置.StringIsEmpty() == false)
                        {
                            string[] position = textVision.中文名位置.Split(";");
                            (string width, string height, string center) = GetSquare(position);
                            positionClass positionClass = new positionClass
                            {
                                高 = height,
                                寬 = width,
                                中心 = center,
                                信心分數 = textVision.中文名信心分數,
                                keyWord = "cht_name",
                            };
                            positionClasses.LockAdd(positionClass);
                        }
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        if (textVision.效期位置.StringIsEmpty() == false)
                        {
                            string[] position = textVision.效期位置.Split(";");
                            (string width, string height, string center) = GetSquare(position);
                            positionClass positionClass = new positionClass
                            {
                                高 = height,
                                寬 = width,
                                中心 = center,
                                信心分數 = textVision.效期信心分數,
                                keyWord = "expirydate",
                            };
                            positionClasses.LockAdd(positionClass);

                        }
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        if (textVision.藥名位置.StringIsEmpty() == false)
                        {
                            string[] position = textVision.藥名位置.Split(";");
                            (string width, string height, string center) = GetSquare(position);
                            positionClass positionClass = new positionClass
                            {
                                高 = height,
                                寬 = width,
                                中心 = center,
                                信心分數 = textVision.藥名信心分數,
                                keyWord = "name",
                            };
                            positionClasses.LockAdd(positionClass);

                        }
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        if (textVision.單號位置.StringIsEmpty() == false)
                        {
                            string[] position = textVision.單號位置.Split(";");
                            (string width, string height, string center) = GetSquare(position);
                            positionClass positionClass = new positionClass
                            {
                                高 = height,
                                寬 = width,
                                中心 = center,
                                信心分數 = textVision.單號信心分數,
                                keyWord = "po",
                            };
                            positionClasses.LockAdd(positionClass);
                        }
                    })));
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        if (textVision.數量位置.StringIsEmpty() == false)
                        {
                            string[] position = textVision.數量位置.Split(";");
                            (string width, string height, string center) = GetSquare(position);
                            positionClass positionClass = new positionClass
                            {
                                高 = height,
                                寬 = width,
                                中心 = center,
                                信心分數 = textVision.數量信心分數,
                                keyWord = "qty",
                            };
                            positionClasses.LockAdd(positionClass);
                        }
                    })));
                    Task.WhenAll(tasks).Wait();
                    textVision.識別位置 = positionClasses;
                    textVision.圖片 = input_textVision[0].圖片;
                    textVisionClass_AI[0] = textVision;
                }
                
                returnData.Code = 200;
                returnData.Data = textVisionClass_AI;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Method = "PCMPO/analyze";
                Logger.Log(file, project, returnData.JsonSerializationt());
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

        private (string width, string height, string center) GetSquare(string[] position)
        {
            int xMax = position[2].Split(",")[0].StringToInt32();
            int yMax = position[2].Split(",")[1].StringToInt32();

            int xMin = position[0].Split(",")[0].StringToInt32();
            int yMin = position[0].Split(",")[1].StringToInt32();

            string width = (xMax - xMin).ToString();
            string height = (yMax - yMin).ToString();

            double centerX = (xMax + xMin) / 2.0;
            double centerY = (yMax + yMin) / 2.0;

            return (width, height, $"{centerX},{centerY}");
        }
        private (string Server, string DB, string UserName, string Password, uint Port) GetServerInfo(string Name, string Type, string Content)
        {
            List<ServerSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            ServerSettingClass serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return (serverSettingClass.Server, serverSettingClass.DBName, serverSettingClass.User, serverSettingClass.Password, (uint)serverSettingClass.Port.StringToInt32());
        }
        private string GetServerAPI(string Name, string Type, string Content)
        {
            List<ServerSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            ServerSettingClass serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return serverSettingClass.Server;
        }



    }
}
