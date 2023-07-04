using Basic;
using HIS_DB_Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOffice;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_ServerAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var localIpAddress = HttpContext.Connection.LocalIpAddress?.ToString();
            var localPort = HttpContext.Connection.LocalPort;
            var protocol = HttpContext.Request.IsHttps ? "https" : "http";
            returnData returnData = new returnData();
            returnData.Code = 200;
            returnData.Result = $"Api test sucess!{protocol}://{localIpAddress}:{localPort}";

            return returnData.JsonSerializationt(true);
        }
        [Route("excel")]
        [HttpPost]
        public async Task<string> POST_excel([FromForm] IFormFile file, [FromForm] string IC_NAME, [FromForm] string PON ,[FromForm] string CT)
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile == null)
            {
                throw new Exception("文件不能為空");
            }
            string extension = Path.GetExtension(formFile.FileName); // 获取文件的扩展名
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string json = "";
            inspectionClass.creat creat = new inspectionClass.creat();
            creat.驗收名稱 = IC_NAME;
            creat.請購單號 = PON;
            creat.建表人 = CT;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                json = ExcelClass.NPOI_LoadSheetToJson(memoryStream.ToArray(), extension);

                SheetClass sheetClass = json.JsonDeserializet<SheetClass>();
                // 在这里可以对 memoryStream 进行操作，例如读取或写入数据
                for (int i = 0; i < sheetClass.Rows.Count; i++)
                {
                    if (i == 0) continue;
                    inspectionClass.content content = new inspectionClass.content();
                    string 請購單號 = sheetClass.Rows[i].Cell[0].Text;
                    string 代碼 = sheetClass.Rows[i].Cell[1].Text;
                    string 採購數量 = sheetClass.Rows[i].Cell[5].Text;
                    string 已交貨數量 = sheetClass.Rows[i].Cell[6].Text;
                    string 廠牌 = sheetClass.Rows[i].Cell[8].Text;

                    content.請購單號 = 請購單號;
                    content.料號 = 代碼;
                    content.應收數量 = (採購數量.StringToInt32() - 已交貨數量.StringToInt32()).ToString();
                    content.廠牌 = 廠牌;
                    creat.Contents.Add(content);
                }
            }
            returnData returnData = new returnData();
            returnData.ServerName = "DS01";
            returnData.ServerType = "藥庫";
            returnData.TableName = "medicine_page_firstclass";
            returnData.Data = creat;

            return Basic.Net.WEBApiPostJson("http://220.135.128.247:4433/api/inspection/creat_add", returnData.JsonSerializationt());
        }
    }
}
