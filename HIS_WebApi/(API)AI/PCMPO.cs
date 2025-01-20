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

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCMPO : ControllerBase
    {
        //static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        private static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string fileDirectory = $"{currentDirectory}/log/";
        private static string project = "PO_vision";
        private static string Message = "---------------------------------------------------------------------------";
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
        [HttpPost("init_sub_textVision")]
        public string init_sub_textVision([FromBody] returnData returnData)
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
                return CheckCreatTable(serverSettingClasses[0], new enum_sub_textVision());
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
                    string pre = "data:image/jpeg;base64,";
                    base64 = base64.Replace(pre, "");

                    string folderPath = Path.Combine(fileDirectory, project);
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    string filePath = Path.Combine(folderPath, picfile);
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
                        content = inspectionClass.content_get_by_PON(API, textVision.單號);
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
                returnData.Result = $"辨識成功 檔案名稱{file}";
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string API_AI = GetServerAPI("Main", "網頁", "po_vision_api");
                string API = GetServerAPI("Main", "網頁", "API01");
                SQLControl sQLControl_sub_textVision = new SQLControl(Server, DB, "sub_textVision", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<object[]> getPicByMaster = sQLControl_sub_textVision.GetRowsByDefult(null, (int)enum_sub_textVision.Master_GUID, GUID);
                List<object[]> getDateByGuid = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);

                List<sub_textVisionClass> sub_textVisionClasses = getPicByMaster.SQLToClass<sub_textVisionClass, enum_sub_textVision>();
                List<textVisionClass> textVisionClasses = getDateByGuid.SQLToClass<textVisionClass, enum_textVision>();

                string 圖片 = sub_textVisionClasses[0].圖片;
                textVisionClasses[0].圖片 = 圖片;
                List<object[]> update_textVisionClass = new List<object[]>();
                returnData return_textVisionClass = textVisionClass.ai_analyze(API_AI, textVisionClasses);
                if(return_textVisionClass == null)
                {
                    returnData.Result = "AI連線失敗";
                    returnData.Code = -3;
                    returnData.Data = textVisionClasses;
                    textVisionClasses[0].Code = returnData.Code.ToString();
                    textVisionClasses[0].Result = returnData.Result;
                    textVisionClasses[0].圖片 = "";
                    textVisionClasses[0].效期 = DateTime.MinValue.ToDateTimeString();

                    update_textVisionClass = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                    sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);
                    Logger.Log(project, returnData.JsonSerializationt());
                    Logger.Log(project, Message);
                    return returnData.JsonSerializationt(true);
                }
                if (return_textVisionClass.Result == "False")
                {
                    returnData.Result = "AI辨識失敗";
                    returnData.Code = -1;
                    returnData.Data = textVisionClasses;
                    textVisionClasses[0].效期 = DateTime.MinValue.ToDateTimeString();
                    textVisionClasses[0].Code = returnData.Code.ToString();
                    textVisionClasses[0].Result = returnData.Result;

                    textVisionClasses[0].圖片 = "";
                    update_textVisionClass = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                    sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);
                    Logger.Log(project, returnData.JsonSerializationt());
                    Logger.Log(project, Message);
                    return returnData.JsonSerializationt(true);
                }

                List<positionClass> positionClasses = new List<positionClass>();
                //List<textVisionClass> textVisionClass_AI = return_textVisionClass.Data.ObjToClass<List<textVisionClass>>();
                textVisionClass textVision = return_textVisionClass.Data.ObjToClass<List<textVisionClass>>()[0];
                inspectionClass.content content = new inspectionClass.content();
                if (textVision.單號.StringIsEmpty() == false)
                {
                    content = inspectionClass.content_get_by_PON(API, textVision.單號);
                    if (content == null)
                    {
                        returnData.Code = -2;
                        returnData.Result = $"查無對應單號資料 單號 {textVision.單號}";
                        returnData.Data = new List<textVisionClass> { textVision } ;
                        textVisionClasses[0].Code = returnData.Code.ToString();
                        textVisionClasses[0].Result = returnData.Result;
                        textVisionClasses[0].效期 = DateTime.MinValue.ToDateTimeString();

                        update_textVisionClass = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                        sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);
                        Logger.Log(project, returnData.JsonSerializationt());
                        Logger.Log(project, Message);

                        return returnData.JsonSerializationt(true);
                    }
                }
                else
                {
                    List<textVisionClass> textVisions = textVisionClass.get_by_po_num(API, textVision.單號);
                    if(textVisions.Count > 0)
                    {
                        //textVisionClass.delete_by_GUID(API, GUID);
                        returnData.Code = -4;
                        returnData.Result = $"此單號已辨識過 單號 {textVision.單號}";
                        return returnData.JsonSerializationt(true);

                    }
                }

                List<Task> tasks = new List<Task>();
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
                    textVision.操作者ID = textVisionClasses[0].操作者ID;
                    textVision.操作者姓名 = textVisionClasses[0].操作者姓名;
                    textVision.操作時間 = textVisionClasses[0].操作時間;
                    //textVision.圖片 = textVisionClasses[0].圖片;
                    textVision.批次ID = textVisionClasses[0].批次ID;
                    textVision.PRI_KEY = textVision.PRI_KEY;

                    textVision.確認 = "未確認";

                    //List<object[]> update_textVisionClass = textVisionClass_AI.ClassToSQL<textVisionClass, enum_textVision>();
                    //sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (textVision.批號位置.StringIsEmpty() == false)
                    {
                        positionClass positionClass_batch = GetPosition(textVision.批號位置, textVision.批號信心分數, "batch_num");
                        positionClasses.LockAdd(positionClass_batch);
                    }                  
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (textVision.中文名位置.StringIsEmpty() == false) 
                    {
                        positionClass positionClass_cht = GetPosition(textVision.中文名位置, textVision.中文名信心分數, "cht_name");
                        positionClasses.LockAdd(positionClass_cht);
                    }

                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (textVision.效期位置.StringIsEmpty() == false)
                    {
                        positionClass positionClass_expiry = GetPosition(textVision.效期位置, textVision.效期信心分數, "expirydate");
                        positionClasses.LockAdd(positionClass_expiry);
                    }
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (textVision.藥名位置.StringIsEmpty() == false)
                    {
                        positionClass positionClass_name = GetPosition(textVision.藥名位置, textVision.藥名信心分數, "name");                 
                        positionClasses.LockAdd(positionClass_name);

                    }
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (textVision.單號位置.StringIsEmpty() == false)
                    {
                        positionClass positionClass_po = GetPosition(textVision.單號位置, textVision.單號信心分數, "po");
                        positionClasses.LockAdd(positionClass_po);
                    }
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (textVision.數量位置.StringIsEmpty() == false)
                    {
                        positionClass positionClass_qty = GetPosition(textVision.數量位置, textVision.數量信心分數, "qty");
                        positionClasses.LockAdd(positionClass_qty);
                    }
                })));
                Task.WhenAll(tasks).Wait();
                textVision.識別位置 = positionClasses;
                //textVision.圖片 = textVisionClasses[0].圖片;
                //textVisionClass_AI[0] = textVision;
                
                returnData.Code = 200;
                returnData.Data = new List<textVisionClass>() { textVision };
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"辨識成功";
                textVision.Code = returnData.Code.ToString();
                textVision.Result = returnData.Result;
                update_textVisionClass = new List<textVisionClass>() { textVision }.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.UpdateByDefulteExtra(null, update_textVisionClass);
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
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
                inspectionClass.content content = inspectionClass.content_get_by_PON(API, 單號);
                List<object[]> Update_textVision = new List<object[]>();
                if (content == null)
                {
                    returnData.Code = -2;
                    returnData.Result = $"查無對應單號資料 單號 {單號}";
                    textVisionClasses[0].Code = returnData.Code.ToString();
                    textVisionClasses[0].Result = returnData.Result;
                    Update_textVision = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                    sQLControl_textVision.UpdateByDefulteExtra(null, Update_textVision);
                    Logger.Log(project, returnData.JsonSerializationt());
                    Logger.Log(project, Message);

                    return returnData.JsonSerializationt(true);
                }
                textVisionClass textVision = textVisionClasses[0];
                textVision.單號 = 單號;
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
                textVisionClasses[0] = textVision;

                

                returnData.Code = 200;
                returnData.Data = textVisionClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"編輯<{textVisionClasses.Count}>筆";
                textVisionClasses[0].Code = returnData.Code.ToString();
                textVisionClasses[0].Result = returnData.Result;
                Update_textVision = textVisionClasses.ClassToSQL<textVisionClass, enum_textVision>();
                sQLControl_textVision.UpdateByDefulteExtra(null, Update_textVision);
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string API = GetServerAPI("Main", "網頁", "API01");

                SQLControl sQLControl_sub_textVision = new SQLControl(Server, DB, "sub_textvision", UserName, Password, Port, SSLMode);

                List<object[]> list_sub_textVision = sQLControl_sub_textVision.GetRowsByDefult(null, (int)enum_sub_textVision.Master_GUID, GUID);
                if (list_sub_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<sub_textVisionClass> sub_textVisionClasses = list_sub_textVision.SQLToClass<sub_textVisionClass, enum_sub_textVision>();
                sub_textVisionClasses[0].圖片 = 圖片;
                List<object[]> Update_sub_textVision = sub_textVisionClasses.ClassToSQL<sub_textVisionClass, enum_sub_textVision>();
                sQLControl_sub_textVision.UpdateByDefulteExtra(null, Update_sub_textVision);

                returnData.Code = 200;
                //returnData.Data = sub_textVisionClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"編輯<{Update_sub_textVision.Count}>筆";
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string GUID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_sub_textVision = new SQLControl(Server, DB, "sub_textVision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                List<object[]> list_sub_textVision = sQLControl_sub_textVision.GetRowsByDefult(null, (int)enum_sub_textVision.Master_GUID, GUID);

                if (list_textVision.Count == 0 || list_sub_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }

                sQLControl_textVision.DeleteExtra(null, list_textVision);
                sQLControl_sub_textVision.DeleteExtra(null, list_sub_textVision);

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
        /// 以GUID取得資料
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string GUID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_sub_textVision = new SQLControl(Server, DB, "sub_textVision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.GUID, GUID);
                List<object[]> list_sub_textVision = sQLControl_sub_textVision.GetRowsByDefult(null, (int)enum_sub_textVision.Master_GUID, GUID);

                if (list_textVision.Count == 0 || list_sub_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                List<sub_textVisionClass> sub_textVisionClasses = list_sub_textVision.SQLToClass<sub_textVisionClass, enum_sub_textVision>();
                textVisionClasses[0].圖片 = sub_textVisionClasses[0].圖片;
                returnData.Code = 200;
                returnData.Data = textVisionClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得GUID : {GUID} 資料";
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
        /// 以單號取得資料
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
        [HttpPost("get_by_po_num")]
        public string get_by_po_num([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_po_num";
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
                    returnData.Result = $"returnData.ValueAry 內容應為[\"po_num\"]";
                    return returnData.JsonSerializationt(true);
                }

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string 單號 = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);

                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.單號, 單號);
                if (list_textVision.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "查無資料";
                    return returnData.JsonSerializationt(true);
                }
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();

                returnData.Code = 200;
                returnData.Data = textVisionClasses;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得單號 : {單號} 資料";
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
      
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textvision", UserName, Password, Port, SSLMode);
                SQLControl sQLControl_sub_textVision = new SQLControl(Server, DB, "sub_textVision", UserName, Password, Port, SSLMode);
                List<sub_textVisionClass> sub_textVisionClasses = new List<sub_textVisionClass>();

                foreach (textVisionClass textVisionClass in input_textVision)
                {
                    textVisionClass.GUID = Guid.NewGuid().ToString();
                    textVisionClass.操作時間 = DateTime.Now.ToDateTimeString();
                    string 圖片 = textVisionClass.圖片;
                    textVisionClass.圖片 = "";
                    sub_textVisionClass sub_TextVisionClass = new sub_textVisionClass 
                    {
                        GUID = Guid.NewGuid().ToString(),
                        Master_GUID = textVisionClass.GUID,
                        圖片 = 圖片
                    };
                    sub_textVisionClasses.Add(sub_TextVisionClass);
                }

                List<object[]> list_textVision = input_textVision.ClassToSQL<textVisionClass, enum_textVision>();
                List<object[]> list_sub_textVision = sub_textVisionClasses.ClassToSQL<sub_textVisionClass, enum_sub_textVision>();

                sQLControl_textVision.AddRows(null, list_textVision);
                sQLControl_sub_textVision.AddRows(null, list_sub_textVision);
                
                returnData.Code = 200;
                returnData.Data = input_textVision;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"請購單資料預儲存成功";
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string API_Server = GetServerAPI("Main", "網頁", "API01");
                string 操作者ID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.操作者ID, 操作者ID);
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                Dictionary<string, List<textVisionClass>> dicTextVision = textVisionClass.ToDicByBatchID(textVisionClasses);
                if(dicTextVision.Count == 0)
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
                List<returnDataClass> returnDataClasses = new List<returnDataClass>();
                for (int i = 0; i < textVisions.Count; i++)
                {
                    if (textVisions[i].確認 == "未確認") 
                    {
                        flag = true;
                        returnDataClass returnDataClass = new returnDataClass()
                        {
                            Code = textVisions[i].Code.StringToInt32(),
                            Result = textVisions[i].Code,
                            Data = new List<textVisionClass>() { textVisions[i] } 
                        };
                        textVision_buff.Add(textVisions[i]);
                        returnDataClasses.Add(returnDataClass);
                    } 
                }
                if(flag == false)
                {
                    returnData.Code = 202; //操作者最新的一批資料中沒有未確認資料
                    returnData.Result = $"id : {操作者ID}, 批次ID : {maxBatchID}  資料共{textVisions.Count}筆，未含有未確認資料";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt(true);
                }
                if(returnData.Value == "N" && flag == true)
                {
                    returnData.Code = 201; //操作者最新的一批資料中有未確認資料
                    returnData.Result = $"id : {操作者ID}, 批次ID : {maxBatchID}  資料共{textVisions.Count}筆，含有未確認資料{textVision_buff.Count}筆";
                    returnData.TimeTaken = $"{myTimerBasic}";
                    return returnData.JsonSerializationt(true);
                }
                if(returnData.Value == "Y" && flag == true)
                {
                    returnData.Code = 201; //操作者最新的一批資料中有未確認資料
                    returnData.Data = returnDataClasses;
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

                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
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
                List<object[]> list_update_textVisionClass = update_textVisionClass.ClassToSQL<textVisionClass, enum_textVision>();
                if (list_update_textVisionClass.Count > 0) sQLControl_textVision.UpdateByDefulteExtra(null, list_update_textVisionClass);

                returnData.Code = 200;
                returnData.Data = textVisionClasses;
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
                (string Server, string DB, string UserName, string Password, uint Port) = GetServerInfo("Main", "網頁", "藥檔資料");
                string API_Server = GetServerAPI("Main", "網頁", "API01");
                string 操作者ID = returnData.ValueAry[0];
                SQLControl sQLControl_textVision = new SQLControl(Server, DB, "textVision", UserName, Password, Port, SSLMode);
                List<object[]> list_textVision = sQLControl_textVision.GetRowsByDefult(null, (int)enum_textVision.操作者ID, 操作者ID);
                List<textVisionClass> textVisionClasses = list_textVision.SQLToClass<textVisionClass, enum_textVision>();
                Dictionary<string, List<textVisionClass>> dicTextVision = textVisionClass.ToDicByBatchID(textVisionClasses);
                string maxBatchID = dicTextVision.Keys.Max();
                List<textVisionClass> textVisions = textVisionClass.GetValueByBatchID(dicTextVision, maxBatchID);
                List<textVisionClass> result = new List<textVisionClass>();
                for(int i = 0; i < textVisions.Count; i++)
                {
                    if (!textVisions[i].Log.StringIsEmpty())
                    {
                        returnData returnData_analyze = textVisionClass.analyze(API_Server, textVisions[i].GUID);
                        if(returnData_analyze.Code == 200)
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
            catch(Exception ex)
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




    }
}
