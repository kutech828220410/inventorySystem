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
using System.IO;
using System.Reflection;
using System.Globalization;
using SkiaSharp;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCMPO : ControllerBase
    {
        //static private string API_Server = "http://220.135.128.247:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        private static string project = "PO_vision";
        private static string Message = "---------------------------------------------------------------------------";
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(200, "textVisionClass物件", typeof(textVisionClass))]
        [HttpPost("init_textVision")]
        public string init_textVision([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "VM端");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_textVision());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        [HttpPost("init_sub_textVision")]
        public string init_sub_textVision([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_sub_textVision());
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(sys_serverSettingClasses[0], new enum_med_code_srch());
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                return returnData.JsonSerializationt(true);
            }

        }
        private string CheckCreatTable(sys_serverSettingClass sys_serverSettingClass, Enum enumInstance)
        {
            string Server = sys_serverSettingClass.Server;
            string DB = sys_serverSettingClass.DBName;
            string UserName = sys_serverSettingClass.User;
            string Password = sys_serverSettingClass.Password;
            uint Port = (uint)sys_serverSettingClass.Port.StringToInt32();

            Table table = MethodClass.CheckCreatTable(sys_serverSettingClass, enumInstance);
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
        [HttpPost("OLDanalyze")]
        public string OLDanalyze([FromBody] returnData returnData)
        {
            string file = $"{DateTime.Now.ToString("yyyyMMdd")}{DateTime.Now.ToString("HHmmss")}";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "analyze";
            try
            {
                string API_AI = "http://127.0.0.1:3020";
                string API = GetServerAPI("Main", "網頁", "API01");
                string project = "PO_Vision";

                List<textVisionClass> input_textVision = returnData.Data.ObjToClass<List<textVisionClass>>();
                List<Task> tasks = new List<Task>();
                returnData return_textVisionClass = textVisionClass.ai_analyze(API_AI, input_textVision);
                if (return_textVisionClass == null)
                {
                    string picfile = "";
                    picfile = $"NG{file}";
                    returnData.Code = -200;
                    returnData.Result = $"AI未連線 檔案名稱{picfile}";
                    Logger.Log(file, project, returnData.JsonSerializationt());
                    return returnData.JsonSerializationt(true);
                }
                tasks.Add(Task.Run(new Action(delegate
                {
                    string picfile = "";
                    if (return_textVisionClass.Result == "False")
                    {
                        picfile = "NG" + file + ".jpg";
                    }
                    else
                    {
                        picfile = file + ".jpg";
                    }
                    string base64 = input_textVision[0].圖片;

                    SavePic(picfile, base64, "Po_vision");
                    //string folderPath = Path.Combine(fileDirectory, project);
                    //if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    //string filePath = Path.Combine(folderPath, picfile);
                    //byte[] imageBytes = Convert.FromBase64String(base64);
                    //SKMemoryStream stream = new SKMemoryStream(imageBytes);
                    //SKBitmap bitmap = SKBitmap.Decode(stream);
                    //using (SKImage image = SKImage.FromBitmap(bitmap)) // 明確類型為 SKImage
                    //{
                    //    using (SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100)) // 明確類型為 SKData
                    //    {
                    //        using (System.IO.FileStream fileStream = System.IO.File.OpenWrite(filePath)) // 明確類型為 FileStream
                    //        {
                    //            data.SaveTo(fileStream);
                    //        }
                    //    }
                    //}
                })));
                List<textVisionClass> textVisionClass_AI = new List<textVisionClass>();
                List<positionClass> positionClasses = new List<positionClass>();

                if (return_textVisionClass.Result == "False")
                {
                    Task.WhenAll(tasks).Wait();
                    string picfile = "";
                    picfile = $"NG{file}";
                    returnData.Code = -200;
                    returnData.Result = $"辨識失敗 檔案名稱{picfile}";
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
                        content = inspectionClass.content_get_by_PON(API, textVision.驗收單號, textVision.單號);
                        if (content == null)
                        {
                            string picfile = "";
                            picfile = $"NG{file}";
                            returnData.Code = -200;
                            returnData.Result = $"查無對應單號{textVision.單號}資料 檔案名稱{picfile}";
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
                        if (medClasses.Count > 0)
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
                returnData.Result = $"辨識成功 檔案名稱{file}";
                returnData.Method = "PCMPO/analyze";
                Logger.Log(file, project, returnData.JsonSerializationt());
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
        /// 執行文字辨識
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///             "GUID":
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
            returnData.Method = "api/pcmpo/analyze";
            try
            {
                if (returnData.ValueAry.Count == 0)
                {
                    returnData.Result = "returnData.ValueAry無傳入資料";
                    returnData.Code = -200;
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Result = "returnData.ValueAry應為[\"GUID\"]";
                    returnData.Code = -200;
                    return returnData.JsonSerializationt(true);
                }

                string GUID = returnData.ValueAry[0];
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API_AI = GetServerAPI("Main", "網頁", "po_vision_api");
                string API = GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<object[]> getDateByGuid = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                List<textVisionClass> textVisionClasses = getDateByGuid.SQLToClass<textVisionClass, enum_textVision>();
                if (textVisionClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"查無此 GUID {GUID}";
                    Logger.Log(project, returnData.JsonSerializationt());
                    Logger.Log(project, Message);
                    return returnData.JsonSerializationt(true);
                }

                List<object[]> update_textVisionClass = new List<object[]>();
                returnData return_textVisionClass = textVisionClass.ai_analyze(API_AI, textVisionClasses);

                if (return_textVisionClass == null)
                {
                    returnData.Result = $"AI連線失敗 url:{API_AI}";
                    returnData.Code = -200;
                    textVisionClasses[0].Code = "-3";
                    textVisionClasses[0].Result = returnData.Result;

                    update_textVisionClass = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                    sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);

                    returnData.Data = clearLongData(textVisionClasses[0]);
                    return returnData.JsonSerializationt(true);
                }
                textVisionClass textVision = return_textVisionClass.Data.ObjToClass<List<textVisionClass>>()[0];
                textVision.GUID = textVisionClasses[0].GUID;
                textVision.批次ID = textVisionClasses[0].批次ID;
                textVision.操作時間 = textVisionClasses[0].操作時間;
                textVision.確認 = textVisionClasses[0].確認;
                textVision.驗收單號 = textVisionClasses[0].驗收單號;
                textVision.Code = textVisionClasses[0].Code;
                textVision.Result = textVisionClasses[0].Result;
                textVision.操作者ID = textVisionClasses[0].操作者ID;
                textVision.操作者姓名 = textVisionClasses[0].操作者姓名;
                textVision.圖片 = textVisionClasses[0].圖片;
                textVision.PRI_KEY = $"{textVision.驗收單號}_{textVision.單號}";
                //if (textVision.效期.StringIsEmpty())
                //{
                //    textVision.效期 = textVisionClasses[0].效期;
                //}
                if (return_textVisionClass.Result == "False") // 批號或效期壞辨識失敗
                {
                    if(textVision.效期.StringIsEmpty()) textVision.效期 = textVisionClasses[0].效期;
                    string base64 = textVision.圖片;
                    string fileName = "";
                    if (textVision.單號.StringIsEmpty())
                    {
                        fileName = $"{DateTime.Now.ToString("yyyyMMdd")}{DateTime.Now.ToString("HHmmss")}.txt";
                        SavePic(base64, project);
                    }
                    else
                    {
                        fileName = $"{textVision.單號}.txt";
                        SavePic(textVision.單號, base64, project);
                    }

                    returnData.Data = textVision;
                    Logger.Log(fileName, project, returnData.JsonSerializationt());
                }

                //換API
                if (returnData.Value == "Y")
                {
                    returnData.Code = 200;
                    returnData.TimeTaken = $"{myTimerBasic}";
                    returnData.Result = $"AI 辨識完成";
                    returnData.Data = textVision;
                    return returnData.JsonSerializationt(true);
                }
                else
                {
                    returnData returnData_poNum = textVisionClass.analyze_by_po_num(API, textVision);
                    return returnData_poNum.JsonSerializationt(true);
                }



                //return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 執行文字辨識
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///             "GUID":
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("analyze_by_po_num")]
        public string analyze_by_po_num([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "api/pcmpo/analyze_by_po_num";
            try
            {
                textVisionClass textVision = returnData.Data.ObjToClass<textVisionClass>();
                if (textVision == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);

                List<object[]> update_textVisionClass = new List<object[]>();
                List<positionClass> positionClasses = new List<positionClass>();
                inspectionClass.content content = new inspectionClass.content();
                if (textVision.單號.StringIsEmpty())
                {
                    textVision.Code = "-1";
                    textVision.Result = "辨識單號失敗";
                    if (textVision.效期.StringIsEmpty() == false) textVision = EditExpirydate(textVision);
                    update_textVisionClass = new List<textVisionClass>() { textVision }.ClassToSQL<textVisionClass, enum_textVision>();
                    sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);

                    returnData.Code = 200;
                    returnData.Data = clearLongData(textVision);
                    returnData.Result = textVision.Result;
                    Logger.Log(project, returnData.JsonSerializationt(true));
                    Logger.Log(project, Message);
                    return returnData.JsonSerializationt(true);

                }

                List<textVisionClass> textVisions = textVisionClass.get_by_pri_key(API, textVision.PRI_KEY);

                if (textVisions != null)
                {
                    if (textVisions.Count > 1)
                    {
                        returnData.Result = "單號重複儲存，請確認";
                        returnData.Code = -200;
                        return returnData.JsonSerializationt(true);
                    }
                    if (textVisions[0].確認 == "已確認") //單號已經辨識過
                    {
                        returnData.Code = 200;
                        returnData.Result = $"此單號已辨識過 單號 {textVision.單號}";

                        textVision.Code = "-4";
                        textVision.Result = returnData.Result;

                        returnData.Data = clearLongData(textVision);
                        return returnData.JsonSerializationt(true);
                    }
                    else if (textVisions[0].確認 == "未確認" && textVisions[0].批次ID == textVision.批次ID && textVisions[0].GUID != textVision.GUID) //同一批上傳兩張一樣的
                    {
                        returnData.Code = 200;
                        returnData.Result = $"此單號已上傳過 單號 {textVision.單號}";

                        textVision.Code = "-5";
                        textVision.Result = returnData.Result;

                        //update_textVisionClass = new List<textVisionClass>() { textVision }.ClassToSQL<textVisionClass, enum_textVision>();
                        //sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);

                        returnData.Data = clearLongData(textVision);
                        return returnData.JsonSerializationt(true);
                    }
                    else if (textVisions[0].確認 == "未確認" && textVisions[0].批次ID != textVision.批次ID)
                    {
                        string GUID_delete = textVisions[0].GUID;
                        textVisionClass.delete_by_GUID(API, GUID_delete);
                    }
                }

                content = inspectionClass.content_get_by_PON(API, textVision.驗收單號, textVision.單號);
                if (content == null)
                {
                    returnData.Code = 200;
                    returnData.Result = $"查無對應單號資料 單號 {textVision.驗收單號}-{textVision.單號}";

                    textVision.Code = "-2";
                    textVision.Result = returnData.Result;                  
                    if (textVision.效期.StringIsEmpty() == false) textVision = EditExpirydate(textVision);
                    update_textVisionClass = new List<textVisionClass>() { textVision }.ClassToSQL<textVisionClass, enum_textVision>();
                    sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);

                    returnData.Data = clearLongData(textVision);
                    Logger.Log(project, returnData.JsonSerializationt());
                    Logger.Log(project, Message);
                    return returnData.JsonSerializationt(true);
                }


                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (content.藥品名稱.StringIsEmpty() == false) textVision.藥名 = content.藥品名稱;
                    
                    textVision = EditExpirydate(textVision);                   
                    textVision.藥品碼 = content.藥品碼;
                    textVision.數量 = content.應收數量;
                    textVision.Master_GUID = content.GUID;

                    List<medClass> medClasses = new List<medClass>();
                    if (textVision.藥品碼.StringIsEmpty() == false)
                    {
                        medClass medClass = medClass.get_med_clouds_by_code(API, textVision.藥品碼);
                        medClasses = new List<medClass>() { medClass };
                    }
                    else
                    {
                        if (textVision.藥名.StringIsEmpty() == false)
                        {
                            medClasses = medClass.get_med_clouds_by_name(API, textVision.藥名);
                        }
                    }

                    if (medClasses.Count > 0 && medClasses[0] != null)
                    {
                        if (medClasses[0].中文名稱.StringIsEmpty() == false)
                        {
                            textVision.中文名 = medClasses[0].中文名稱;
                        }
                        else
                        {
                            textVision.中文名 = medClasses[0].藥品學名;
                        }
                    }
                    else
                    {                      
                        textVision.中文名 = Regex.Replace(textVision.中文名, @"^[0-9A-Za-z]+", "");
                        textVision.中文名 = Regex.Replace(textVision.中文名, @"（.*?）", "");
                    }
                })));
                Dictionary<string, (string Position, string Confidence, string Label)> dic_textVision = toDicByPosition(textVision);
                foreach (string key in dic_textVision.Keys)
                {
                    tasks.Add(Task.Run(new Action(delegate
                    {
                        positionClass position = GetPosition(dic_textVision[key].Position, dic_textVision[key].Confidence, dic_textVision[key].Label);
                        positionClasses.LockAdd(position);

                    })));
                }

                Task.WhenAll(tasks).Wait();
                textVision.識別位置 = positionClasses;

                returnData.Code = 200;
                returnData.Result = $"辨識成功";

                textVision.Code = "200";
                textVision.Result = returnData.Result;

                update_textVisionClass = new List<textVisionClass>() { textVision }.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);

                returnData.Data = clearLongData(textVision);
                returnData.TimeTaken = $"{myTimerBasic}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID取得資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///                 "GUID",
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_by_GUID")]
        public string get_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_GUID";
            try
            {
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textvision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得<{textVisionClasses.Count}>筆";
                returnData.Data = textVisionClasses;
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
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
        ///         "ValueAry":
        ///         [
        ///                 "GUID",
        ///                 "po_num"
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_by_GUID_poNum")]
        public string update_by_GUID_poNum([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_by_GUID_poNum";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\",\"單號\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                string 單號 = returnData.ValueAry[1];

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textvision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                textVisionClass textVision = textVisionClasses[0];
                textVision.單號 = 單號;
                textVision.PRI_KEY = $"{textVision.驗收單號}-{textVision.單號}";
                returnData returnData_poNum = textVisionClass.analyze_by_po_num(API, textVision);
                return returnData_poNum.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以GUID更新圖片資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///                 "GUID",
        ///                 "base64"
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update_pic_by_GUID")]
        public string update_pic_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "update_pic_by_GUID";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"GUID\",\"base64\"]";
                    return returnData.JsonSerializationt(true);
                }
                string GUID = returnData.ValueAry[0];
                string 圖片 = returnData.ValueAry[1];

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textvision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                textVisionClasses[0].圖片 = 圖片;
                List<object[]> Update_textVision = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.UpdateByDefulteExtra(null, Update_textVision);

                returnData.Code = 200;
                //returnData.Data = sub_textVisionClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"編輯<{Update_textVision.Count}>筆";
                //Logger.Log(project, returnData.JsonSerializationt());
                //Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
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
                List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
                List<sys_serverSettingClass> sys_serverSettingClass_main = sys_serverSettingClasses.MyFind("Main", "網頁", "人員資料");
                if (sys_serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料";
                    return returnData.JsonSerializationt();
                }

                string Server = sys_serverSettingClass_main[0].Server;
                string DB = sys_serverSettingClass_main[0].DBName;
                string UserName = sys_serverSettingClass_main[0].User;
                string Password = sys_serverSettingClass_main[0].Password;
                uint Port = (uint)sys_serverSettingClass_main[0].Port.StringToInt32();

                List<sys_serverSettingClass> sys_serverSettingClass_API = sys_serverSettingClasses.MyFind("Main", "網頁", "API01");
                string API = sys_serverSettingClass_API[0].Server;

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
                if (medClass == null)
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
        [HttpPost("delete_by_GUID")]
        public string delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "delete_by_GUID";
            try
            {
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string GUID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                //SQLControl sQLControl_sub_textVision = new SQLControl(Server, DB, "sub_textVision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                //List<object[]> list_sub_textVision = sQLControl_sub_textVision.GetRowsByDefult(null, (int)enum_sub_textVision.Master_GUID, GUID);

                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }

                sQLControl_textVision.DeleteExtra(null, list_textVision);
                //sQLControl_sub_textVision.DeleteExtra(null, list_sub_textVision);

                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"成功刪除<{list_textVision.Count}>筆資料";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 以PRI_KEY(驗收單號-請購單號)取得資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///             "po_num"
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("get_by_pri_key")]
        public string get_by_pri_key([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_pri_key";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"驗收單號-請購單號\"]";
                    return returnData.JsonSerializationt(true);
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string PRI_KEY = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.PRI_KEY, PRI_KEY);
                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
               

                returnData.Code = 200;
                returnData.Data = clearLongData(textVisionClasses[0]);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得驗收-請購單號 : {PRI_KEY} 資料";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }
        }
        /// <summary>
        /// 資料預儲存
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
        ///                 "batch_id":"日期時間"
        ///                 "IC_SN":"驗收單號"
        ///             }
        ///         ]
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("preSave")]
        public string preSave([FromBody] returnData returnData)
        {
            //string file = $"{DateTime.Now.ToString("yyyyMMdd")}{DateTime.Now.ToString("HHmmss")}";
            returnData.Method = "api/PCMPO/preSave";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<textVisionClass> input_textVision = returnData.Data.ObjToClass<List<textVisionClass>>();
                if (input_textVision == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textvision", UserName, Password, Port, SSLMode);

                foreach (textVisionClass textVisionClass in input_textVision)
                {
                    textVisionClass.GUID = Guid.NewGuid().ToString();
                    textVisionClass.操作時間 = DateTime.Now.ToDateTimeString();
                    textVisionClass.確認 = "未確認";
                    textVisionClass.效期 = DateTime.MinValue.ToDateTimeString();
                    textVisionClass.Code = "-200";
                    textVisionClass.Result = "未辨識";
                    if (textVisionClass.圖片.StringIsEmpty())
                    {
                        string filPath = @"C:\Users\Administrator\Desktop\測試單據\20250116-1_1140114013-08.jpg";
                        byte[] imageBytes = System.IO.File.ReadAllBytes(filPath);
                        string base64 = Convert.ToBase64String(imageBytes);
                        base64 = $"data:image/jpeg;base64,{base64}";
                        textVisionClass.圖片 = base64;
                    }

                }

                List<object[]> list_textVision = input_textVision.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.AddRows(null, list_textVision);

                returnData.Code = 200;
                returnData.Data = clearLongData(input_textVision[0]);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"請購單資料預儲存成功 GUID:{input_textVision[0].GUID}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }


        }
        /// <summary>
        /// 資料更新
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "Data":
        ///         
        ///             {
        ///                 "GUID":""
        ///                 "qty":"數量"
        ///                 "batch_num":"批號"
        ///                 "expirydate":"效期"
        ///             }
        ///         
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("update")]
        public string update([FromBody] returnData returnData)
        {
            returnData.Method = "api/PCMPO/update";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                textVisionClass input_textVision = returnData.Data.ObjToClass<textVisionClass>();
                if (input_textVision == null)
                {
                    returnData.Code = -200;
                    returnData.Result = $"傳入Data資料異常";
                    return returnData.JsonSerializationt();
                }
                string GUID = input_textVision.GUID;
                string 數量 = input_textVision.數量;
                string 批號 = input_textVision.批號;
                string 效期 = input_textVision.效期;

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textvision", UserName, Password, Port, SSLMode);
                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                if (數量.StringIsEmpty() == false) textVisionClasses[0].數量 = 數量;
                if (批號.StringIsEmpty() == false) textVisionClasses[0].批號 = 批號;
                if (效期.StringIsEmpty() == false) textVisionClasses[0].效期 = 效期;


                List<object[]> update_textVision = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.UpdateByDefulteExtra(null, update_textVision);
                returnData.Code = 200;
                returnData.Data = textVisionClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"請購單資料更新成功 批號:{批號} 效期:{效期} 數量:{數量}";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = $"Exception : {ex.Message}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);
            }


        }
        /// <summary>
        /// 以操作者ID找出最新的一批資料 "Y" Data 回傳完整資料/ "N" Data不回傳資料
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///                 "op_id"
        ///         ],
        ///         "Value":"Y"/"N" 
        ///         
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("preCheck")]
        public string preCheck([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "preCheck";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"操作者ID\"]";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.Value == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.Value 空白，內容應為\"Y\" or \"N\"";
                    return returnData.JsonSerializationt();
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API_Server = GetServerAPI("Main", "網頁", "API01");
                string 操作者ID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.操作者ID, 操作者ID);
                //List<object[]> list_textVision = sQLControl_textVision.GetAllRows(null);

                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                //textVisionClasses = textVisionClasses.Where(temp => temp.操作者ID == 操作者ID).ToList();
                Dictionary<string, List<textVisionClass>> dicTextVision = textVisionClass.ToDicByBatchID(textVisionClasses);
                if (dicTextVision.Count == 0)
                {
                    returnData.Code = 202; //操作者最新的一批資料中沒有未確認資料
                    returnData.Result = $"id : {操作者ID} 未含有未確認資料";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt(true);
                }
                string maxBatchID = dicTextVision.Keys.Max();
                List<textVisionClass> textVisions = textVisionClass.GetValueByBatchID(dicTextVision, maxBatchID);
                bool flag = false;
                List<textVisionClass> textVision_buff = new List<textVisionClass>();
                //List<returnDataClass> returnDataClasses = new List<returnDataClass>();
                for (int i = 0; i < textVisions.Count; i++)
                {
                    if (textVisions[i].確認 == "未確認")
                    {
                        flag = true;
                        textVisions[i].圖片 = "";
                        textVisions[i].Log = "";
                        //returnDataClass returnDataClass = new returnDataClass()
                        //{
                        //    Code = textVisions[i].Code.StringToInt32(),
                        //    Result = textVisions[i].Code,
                        //    Data = new List<textVisionClass>() { textVisions[i] } 
                        //};
                        textVision_buff.Add(textVisions[i]);
                        //returnDataClasses.Add(returnDataClass);
                    }
                }
                if (flag == false)
                {
                    returnData.Code = 202; //操作者最新的一批資料中沒有未確認資料
                    returnData.Result = $"id : {操作者ID}, 批次ID : {maxBatchID}  資料共{textVisions.Count}筆，未含有未確認資料";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.Value == "N" && flag == true)
                {
                    returnData.Code = 201; //操作者最新的一批資料中有未確認資料
                    returnData.Result = $"id : {操作者ID}, 批次ID : {maxBatchID}  資料共{textVisions.Count}筆，含有未確認資料{textVision_buff.Count}筆";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt(true);
                }
                if (returnData.Value == "Y" && flag == true)
                {
                    returnData.Code = 201; //操作者最新的一批資料中有未確認資料
                    returnData.Data = textVision_buff;
                    returnData.Result = $"id : {操作者ID}, 批次ID : {maxBatchID}  資料共{textVisions.Count}筆，，含有未確認資料{textVision_buff.Count}筆";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt(true);
                }
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);

            }
        }
        /// <summary>
        /// 將辨識資料送出確認
        /// </summary>
        /// <remarks>
        /// 以下為JSON範例
        /// <code>
        ///     {
        ///         "ValueAry":
        ///         [
        ///                 "batchID",
        ///                 "GUID1;GUID2;GUID3"
        ///         ],
        ///         
        ///         
        ///     }
        /// </code>
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [HttpPost("Check")]
        public string Check([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "Check";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 2)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"BatchID\",\"GUID1;GUID2\"]";
                    return returnData.JsonSerializationt(true);
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API_Server = GetServerAPI("Main", "網頁", "API01");
                string 批次ID = returnData.ValueAry[0];
                string[] GUIDs = returnData.ValueAry[1].Split(";");
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.批次ID, 批次ID);
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                List<textVisionClass> update_textVisionClass = new List<textVisionClass>();
                for (int i = 0; i < textVisionClasses.Count; i++)
                {
                    string GUID = textVisionClasses[i].GUID;
                    if (GUIDs.Contains(GUID))
                    {
                        textVisionClasses[i].確認 = "已確認";
                        update_textVisionClass.Add(textVisionClasses[i]);
                    }
                }
                List<inspectionClass.sub_content> sub_Contents = new List<inspectionClass.sub_content>();
                
                List<object[]> list_update_textVisionClass = update_textVisionClass.ClassToSQL<textVisionClass, enum_textVision>();
                if (list_update_textVisionClass.Count > 0) sQLControl_textVision.UpdateByDefulteExtra(null, list_update_textVisionClass);

                
                
                //tasks.Add(Task.Run(new Action(delegate 
                //{
                //    foreach(var textVisionClass in update_textVisionClass)
                //    {
                //        inspectionClass.sub_content sub_Content = new inspectionClass.sub_content
                //        {
                //            Master_GUID = textVisionClass.Master_GUID,
                //            效期 = textVisionClass.效期,
                //            批號 = textVisionClass.批號,
                //            實收數量 = textVisionClass.數量,
                //            操作人 = textVisionClass.操作者姓名,
                //        };
                //        sub_Contents.Add(sub_Content);
                //    }   
                //})));
                //Task.WhenAll(tasks).Wait();
                //foreach (var item in sub_Contents)
                //{
                //    returnData returnData_out = inspectionClass.returnData_sub_content_add(API_Server, item);
                //    Logger.Log($"{project}/sub_content_add", returnData_out.JsonSerializationt(true));
                //    if (returnData_out.Code != 200 || returnData_out.Data == null) return returnData_out.JsonSerializationt(true);
                //}
                
                returnData.Code = 200;
                returnData.Data = clearLongData(update_textVisionClass);
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"更新請購單資料共{update_textVisionClass.Count}筆";
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);

            }
        }
        [HttpPost("recover")]
        public string recover([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "recover";
            try
            {
                if (returnData.ValueAry == null)
                {
                    returnData.Data = -200;
                    returnData.Result = "returnData.ValueAry 空白，請輸入對應欄位資料!";
                    return returnData.JsonSerializationt();
                }
                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry 內容應為[\"操作者ID\"]";
                    return returnData.JsonSerializationt(true);
                }
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "VM端");
                string API_Server = GetServerAPI("Main", "網頁", "API01");
                string 操作者ID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.操作者ID, 操作者ID);
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                Dictionary<string, List<textVisionClass>> dicTextVision = textVisionClass.ToDicByBatchID(textVisionClasses);
                string maxBatchID = dicTextVision.Keys.Max();
                List<textVisionClass> textVisions = textVisionClass.GetValueByBatchID(dicTextVision, maxBatchID);
                List<textVisionClass> result = new List<textVisionClass>();
                for (int i = 0; i < textVisions.Count; i++)
                {
                    if (!textVisions[i].Log.StringIsEmpty())
                    {
                        returnData returnData_analyze = textVisionClass.analyze(API_Server, textVisions[i].GUID);
                        if (returnData_analyze.Code == 200)
                        {
                            List<textVisionClass> textVision = returnData_analyze.Data.ObjToClass<List<textVisionClass>>();
                            result.Add(textVision[0]);
                        }
                    }
                    else
                    {
                        result.Add(textVisions[i]);
                    }
                }
                returnData.Code = 200;
                returnData.Data = result;
                returnData.Result = $"操作者ID : {操作者ID}  批次ID: {maxBatchID}共{textVisions.Count}筆，辨識完成";
                returnData.TimeTaken = $"{myTimerBasic}";
                Logger.Log(project, returnData.JsonSerializationt());
                Logger.Log(project, Message);
                return returnData.JsonSerializationt(true);

            }
            catch (Exception ex)
            {
                returnData.Code = -200;
                returnData.Result = ex.Message;
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
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return (sys_serverSettingClass.Server, sys_serverSettingClass.DBName, sys_serverSettingClass.User, sys_serverSettingClass.Password, (uint)sys_serverSettingClass.Port.StringToInt32());
        }
        private string GetServerAPI(string Name, string Type, string Content)
        {
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return sys_serverSettingClass.Server;
        }
        private positionClass GetPosition(string 位置, string 信心分數, string keyword)
        {
            string[] position = 位置.Split(";");
            (string width, string height, string center) = GetSquare(position);
            positionClass positionClass = new positionClass
            {
                高 = height,
                寬 = width,
                中心 = center,
                信心分數 = 信心分數,
                keyWord = keyword,
            };
            return positionClass;
        }
        private List<textVisionClass> clearLongData(textVisionClass textVisionClass)
        {
            textVisionClass.圖片 = "";
            textVisionClass.Log = "";
            return new List<textVisionClass>() { textVisionClass };
        }
        private List<textVisionClass> clearLongData(List<textVisionClass> textVisionClasses)
        {
            foreach(var textVisionClass in textVisionClasses)
            {
                textVisionClass.圖片 = "";
                textVisionClass.Log = "";
            }
            
            return textVisionClasses;
        }
        private Dictionary<string, (string Position, string Confidence, string Label)> toDicByPosition(textVisionClass textVisionClass)
        {
            Dictionary<string, (string Position, string Confidence, string Label)> dic = new Dictionary<string, (string Position, string Confidence, string Label)>();
            List<(string Position, string Confidence, string Label)> fields = new List<(string Position, string Confidence, string Label)>
            {
                (textVisionClass.批號位置, textVisionClass.批號信心分數, "batch_num"),
                (textVisionClass.中文名位置, textVisionClass.中文名信心分數, "cht_name"),
                (textVisionClass.效期位置, textVisionClass.效期信心分數, "expirydate"),
                (textVisionClass.藥名位置, textVisionClass.藥名信心分數, "name"),
                (textVisionClass.單號位置, textVisionClass.單號信心分數, "po"),
                (textVisionClass.數量位置, textVisionClass.數量信心分數, "qty")

            };

            foreach (var field in fields)
            {
                if (field.Position.StringIsEmpty() == false) dic[field.Position] = field;
            }
            return dic;
        }
        private void SavePic(string base64, string folderName)
        {
            string fileName = $"{DateTime.Now.ToString("yyyyMMdd")}{DateTime.Now.ToString("HHmmss")}";
            SavePic(fileName, base64, folderName);
        }
        private void SavePic(string fileName, string base64, string folderName)
        {
            // 將檔名副檔名改為 .jpg
            fileName = Path.ChangeExtension(fileName, ".jpg");

            // 移除 Base64 字串前綴
            string pre = "data:image/jpeg;base64,";
            base64 = base64.Replace(pre, "");

            // 取得目前執行檔所在目錄，並組合出 log/目標資料夾路徑
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileDirectory = Path.Combine(currentDirectory, "log");
            string targetFolder = string.IsNullOrEmpty(folderName) ? "PO_vision" : folderName;
            string folderPath = Path.Combine(fileDirectory, targetFolder);

            // 確認目錄是否存在，不存在則建立
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filepath = Path.Combine(folderPath, fileName);

            try
            {
                // 轉換 Base64 字串為位元組陣列
                byte[] imageBytes = Convert.FromBase64String(base64);

                // 使用 MemoryStream 與 ImageSharp 載入圖片
                using (var ms = new MemoryStream(imageBytes))
                {
                    using (var image = SixLabors.ImageSharp.Image.Load(ms))
                    {
                        // 若在 Docker 容器中運行，可調整檔案路徑格式
                        if (ContainerChecker.IsRunningInDocker())
                        {
                            filepath = filepath.Replace("\\", "/");
                            string _fileName = Path.GetFileName(filepath);
                            string directoryPath = Path.GetDirectoryName(filepath);
                            string _folderName = Path.GetFileName(directoryPath);
                            filepath = $"/app/log/{_folderName}/{_fileName}";
                            Console.WriteLine($"SavePic ..filepath : {filepath}");
                        }
                        // 開啟檔案流，並使用 JpegEncoder 儲存圖片
                        var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 100 };
                        using (FileStream fileStream = System.IO.File.OpenWrite(filepath))
                        {
                            image.Save(fileStream, encoder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"儲存圖片發生錯誤: {ex.Message}");
                throw;
            }
        }
        private textVisionClass EditExpirydate(textVisionClass textVisionClass)
        {
            string[] formats = { "MM/dd/yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "yyyy.MM.dd", "yyyy/MM/dd HH:mm:ss" }; // 可擴展格式

            if (DateTime.TryParseExact(textVisionClass.效期, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                textVisionClass.效期 = date.ToString("yyyy/MM/dd");
            }
            else
            {
                textVisionClass.效期 = DateTime.MinValue.ToDateTimeString();
            }
            return textVisionClass;
        }
        


    }
}
