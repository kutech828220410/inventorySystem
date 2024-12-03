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
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Text.Json.Serialization;
using SkiaSharp;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class medCount : ControllerBase
    {
        private static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string fileDirectory = $"{currentDirectory}/log/";

        public class BoundingBox
        {
            [JsonPropertyName("x_min")]
            public float XMin { get; set; }

            [JsonPropertyName("y_min")]
            public float YMin { get; set; }

            [JsonPropertyName("x_max")]
            public float XMax { get; set; }

            [JsonPropertyName("y_max")]
            public float YMax { get; set; }

            [JsonPropertyName("confidence")]
            public float Confidence { get; set; }

            [JsonPropertyName("class_id")]
            public int ClassId { get; set; }

            [JsonPropertyName("class_name")]
            public string ClassName { get; set; } // 類別名稱，可選

            public BoundingBox(float xMin, float yMin, float xMax, float yMax, float confidence, int classId, string className = null)
            {
                XMin = xMin;
                YMin = yMin;
                XMax = xMax;
                YMax = yMax;
                Confidence = confidence;
                ClassId = classId;
                ClassName = className;
            }
        }



        [HttpPost("medCountAnalyze")]
        [HttpPost("Analyze")]
        public string medCountAnalyze([FromBody] returnData returnData)
        {
            string log_task_1 = "";
            string log_task_2 = "";
            string file = $"{DateTime.Now.ToString("yyyyMMdd")}{DateTime.Now.ToString("HHmmss")}";
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                List<Task> tasks = new List<Task>();
                List<medCountClass> out_medCountClass = new List<medCountClass>();
                List<medCountClass> json_in = returnData.Data.ObjToClass<List<medCountClass>>();
                string project = "Pill_recognition";
                //tasks.Add(Task.Run(new Action(delegate
                //{
                //    MyTimerBasic myTimerBasic_task = new MyTimerBasic();
                //    string API = GetServerAPI("Main", "網頁", "ai_medCount_api");
                //    log_task_1 += $" 取得API,{myTimerBasic_task}\n";
                //    List<medCountClass> medCountClasses = medCountClass.ai_medCount(API, json_in);
                //    log_task_1 += $" ai_medCount,{myTimerBasic_task}\n";

                //    if (medCountClasses != null)
                //    {
                //        for (int i = 0; i < medCountClasses.Count; i++)
                //        {
                //            List<positionClass> positionClasses = new List<positionClass>();
                //            for (int j = 0; j < medCountClasses[i].AI結果.Count; j++)
                //            {
                //                aiCountResult aiCountResult = medCountClasses[i].AI結果[j];
                //                string[] position = aiCountResult.座標.Split(";");
                //                (string width, string height, string center) = GetSquare(position);
                //                positionClass positionClass = new positionClass
                //                {
                //                    高 = height,
                //                    寬 = width,
                //                    中心 = center,
                //                    信心分數 = aiCountResult.信心分數
                //                };
                //                positionClasses.Add(positionClass);
                //            }
                //            medCountClass medCountClass = new medCountClass
                //            {
                //                藥名 = medCountClasses[i].藥名,
                //                種類 = medCountClasses[i].種類,
                //                數量 = medCountClasses[i].數量,
                //                識別位置 = positionClasses
                //            };
                //            out_medCountClass.Add(medCountClass);
                //        }
                //    }
                //    log_task_1 += $" done,{myTimerBasic_task}\n";

                //})));
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
                
                //if (out_medCountClass.Count == 0)
                //{
                //    returnData.Code = -200;
                //    returnData.Result = $"AI辨識失敗 檔案名稱{file}";
                //    Logger.Log(file, project, returnData.JsonSerializationt());
                //    return returnData.JsonSerializationt(true);
                //}
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = out_medCountClass;
                returnData.Result = $"藥物數粒辨識成功 檔案名稱{file},{log_task_1}";
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

        [HttpPost("AnalyzeEx0")]
        public async Task<IActionResult> AnalyzeEx0()
        {
            var response = new returnData();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                // 讀取圖片數據
                using var memoryStream = new MemoryStream();
                await Request.Body.CopyToAsync(memoryStream);
                if (memoryStream.Length == 0)
                {
                    response.Code = 400;
                    response.Result = "圖片數據為空";
                    return Content(response.JsonSerializationt(), "application/json");
                }

                // 將圖片轉換為張量
                string base64String = ConvertMemoryStreamToBase64(memoryStream);
                var inputTensor = PreprocessImage(base64String);

                // 獲取模型會話
                var session = ModelManager.GetModel("medcount");

                // 執行推論
                var inputContainer = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("images", inputTensor)
                };

                using var results = session.Run(inputContainer);
                var resultTensor = results.First().AsTensor<float>();
                var resultArray = resultTensor.ToArray();
                // 解碼推理結果為座標和信心分數
                var decodedResults = FilterValidResults(resultArray , new string[] { } , 0.7F);

                // 返回處理結果
                response.Code = 200;
                response.Result = "推論完成！";
                response.Data = decodedResults;
                response.TimeTaken = $"{stopwatch.ElapsedMilliseconds} ms";

                return Content(response.JsonSerializationt(), "application/json");
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Result = "推論失敗！";
                response.Data = ex.Message;
                return Content(response.JsonSerializationt(), "application/json");
            }
            finally
            {
                stopwatch.Stop();
            }
        }
        private List<BoundingBox> FilterValidResults(float[] resultArray, string[] classNames, float confidenceThreshold = 0.5f)
        {
            var validDetections = new List<BoundingBox>();

            int numDetections = resultArray.Length / 5; // 總檢測數量 (42000 / 5 = 8400)

            for (int i = 0; i < numDetections; i++)
            {
                // 每個檢測的起始索引
                int offset = i * 5;

                // 提取屬性
                float xMin = resultArray[offset];
                float yMin = resultArray[offset + 1];
                float xMax = resultArray[offset + 2];
                float yMax = resultArray[offset + 3];
                float confidence = resultArray[offset + 4]; // 對置信分數進行正規化
                // 篩選條件：置信分數 > 閾值，且邊界框合法
                if (confidence > confidenceThreshold && xMin < xMax && yMin < yMax)
                {
                    validDetections.Add(new BoundingBox(xMin, yMin, xMax, yMax, confidence, 0, ""));
                }
            }

            return validDetections;
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

        private static string ConvertMemoryStreamToBase64(MemoryStream memoryStream)
        {
            // 確保 MemoryStream 的位置重置到起始點
            memoryStream.Position = 0;

            // 將 MemoryStream 內容讀取為 byte[]
            byte[] buffer = memoryStream.ToArray();

            // 將 byte[] 轉換為 Base64 字串
            return Convert.ToBase64String(buffer);
        }
        private static DenseTensor<float> PreprocessImage(string base64Image)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            const int inputWidth = 640;
            const int inputHeight = 640;

            // 解碼 Base64 並加載圖片
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            string imageByte_time = myTimerBasic.ToString();

            using var image = SKBitmap.Decode(imageBytes);
            string Decode_time = myTimerBasic.ToString();

            // 確保格式為 RGB8888 並縮放圖片到模型輸入大小
            using var resizedImage = image.Resize(new SKImageInfo(inputWidth, inputHeight, SKColorType.Rgb888x), SKFilterQuality.High);
            if (resizedImage == null)
            {
                throw new Exception("Failed to resize image.");
            }

            // 初始化張量
            var tensor = new DenseTensor<float>(new[] { 1, 3, inputHeight, inputWidth });

            // 填充張量數據
            unsafe
            {
                var pixels = (byte*)resizedImage.GetPixels().ToPointer();
                int bytesPerPixel = 3; // RGB8888 固定為 3

                fixed (float* tensorData = tensor.Buffer.Span)
                {
                    for (int y = 0; y < inputHeight; y++)
                    {
                        for (int x = 0; x < inputWidth; x++)
                        {
                            int offset = (y * inputWidth + x) * bytesPerPixel;

                            tensorData[0 * inputHeight * inputWidth + y * inputWidth + x] = pixels[offset + 0] / 255.0f; // R
                            tensorData[1 * inputHeight * inputWidth + y * inputWidth + x] = pixels[offset + 1] / 255.0f; // G
                            tensorData[2 * inputHeight * inputWidth + y * inputWidth + x] = pixels[offset + 2] / 255.0f; // B
                        }
                    }
                }
            }

            string DenseTensor_time = myTimerBasic.ToString();
            return tensor;
        }


    }
}
