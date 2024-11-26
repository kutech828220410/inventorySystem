using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using System.Drawing;
using SkiaSharp;
using System.IO;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_AI
{
    [Route("api/[controller]")]
    [ApiController]
    public class medCount : ControllerBase
    {
        private static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string fileDirectory = $"{currentDirectory}/log/";
        [HttpPost("medCountAnalyze")]
        public string medCountAnalyze([FromBody] returnData returnData)
        {
            string file = $"{DateTime.Now.ToString("yyyyMMdd")}{DateTime.Now.ToString("HHmmss")}";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<Task> tasks = new List<Task>();
                List<medCountClass> out_medCountClass = new List<medCountClass>();
                List<medCountClass> json_in = returnData.Data.ObjToClass<List<medCountClass>>();
                string project = "Pill_recognition";
                tasks.Add(Task.Run(new Action(delegate
                {
                    string API = GetServerAPI("Main", "網頁", "med_cart_vm_api");
                    List<medCountClass> medCountClasses = medCountClass.ai_medCount(API, json_in);
                    if (medCountClasses != null)
                    {
                        for (int i = 0; i < medCountClasses.Count; i++)
                        {
                            List<positionClass> positionClasses = new List<positionClass>();
                            for (int j = 0; j < medCountClasses[i].AI結果.Count; j++)
                            {
                                aiCountResult aiCountResult = medCountClasses[i].AI結果[j];
                                string[] position = aiCountResult.座標.Split(";");
                                (string width, string height, string center) = GetSquare(position);
                                positionClass positionClass = new positionClass
                                {
                                    高 = height,
                                    寬 = width,
                                    中心 = center,
                                    信心分數 = aiCountResult.信心分數
                                };
                                positionClasses.Add(positionClass);
                            }
                            medCountClass medCountClass = new medCountClass
                            {
                                藥名 = medCountClasses[i].藥名,
                                種類 = medCountClasses[i].種類,
                                數量 = medCountClasses[i].數量,
                                識別位置 = positionClasses
                            };
                            out_medCountClass.Add(medCountClass);
                        }
                    }
                })));
                tasks.Add(Task.Run(new Action(delegate
                {
                    if (returnData.Value == "True")
                    {
                        string picfile = file + ".jpg";
                        string base64 = json_in[0].圖片;
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
                    }
                })));
                
                Task.WhenAll(tasks).Wait();
                
                if (out_medCountClass.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"AI辨識失敗 檔案名稱{file}";
                    Logger.Log(file, project, returnData.JsonSerializationt());
                    return returnData.JsonSerializationt(true);
                }
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = out_medCountClass;
                returnData.Result = $"藥物數粒辨識成功 檔案名稱{file}";
                Logger.Log(file, project, returnData.JsonSerializationt());
;               return returnData.JsonSerializationt(true);
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
