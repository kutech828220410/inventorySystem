using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_AI
{
    [Route("api/[controller]")]
    [ApiController]
    public class medCount : ControllerBase
    {
        [HttpPost("medCountAnalyze")]
        public string medCountAnalyze([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            try
            {
                string API = "http://192.168.15.113:3000";
                List<medCountClass> json_in = returnData.Data.ObjToClass<List<medCountClass>>();
                List<medCountClass> medCountClasses = returnData.Data.ObjToClass<List<medCountClass>>();
                //List<medCountClass> medCountClasses = medCountClass.ai_medCount(API, json_in);
                List<medCountClass> out_medCountClass = new List<medCountClass>();
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
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Data = out_medCountClass;
                returnData.Result = $"藥物數粒辨識成功";
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

    }
}
