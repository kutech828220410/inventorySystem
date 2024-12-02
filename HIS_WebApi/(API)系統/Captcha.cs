using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace HIS_WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaptchaController : ControllerBase
    {
        private static readonly char[] Chars = "23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz".ToCharArray();
        private static readonly Random Random = new Random();

        [HttpGet("image")]
        public IActionResult GetCaptchaImage()
        {
            try
            {
                // 生成驗證碼
                var captchaCode = GenerateCaptchaCode();

                // 將驗證碼保存到 Session 或內存中（可選）

                // 生成圖片
                using var image = new Image<Rgba32>(100, 40, Color.White);
                var font = SystemFonts.CreateFont("Arial", 20);

                image.Mutate(ctx =>
                {
                    // 繪製文字
                    ctx.DrawText(captchaCode, font, Color.Black, new PointF(10, 5));

                    // 添加干擾線
                    for (int i = 0; i < 5; i++)
                    {
                        ctx.DrawLine(Pens.Solid(Color.Gray, 1), new[]
                        {
                            new PointF(Random.Next(100), Random.Next(40)),
                            new PointF(Random.Next(100), Random.Next(40))
                        });
                    }

                    // 添加隨機噪點
                    AddNoisePoints(ctx, 100, 40);
                });

                // 將圖片直接返回給前端
                using var stream = new MemoryStream();
                image.Save(stream, PngFormat.Instance);
                return File(stream.ToArray(), "image/png");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"生成驗證碼圖片失敗: {ex.Message}" });
            }
        }

        [HttpGet("generate")]
        public IActionResult GenerateCaptcha()
        {
            try
            {
                var captchaCode = GenerateCaptchaCode();

                using var image = new Image<Rgba32>(100, 40, Color.White);
                var font = SystemFonts.CreateFont("Arial", 20);

                image.Mutate(ctx =>
                {
                    ctx.DrawText(captchaCode, font, Color.Black, new PointF(10, 5));
                    for (int i = 0; i < 5; i++)
                    {
                        ctx.DrawLine(Pens.Solid(Color.Gray, 1), new[]
                        {
                    new PointF(Random.Next(100), Random.Next(40)),
                    new PointF(Random.Next(100), Random.Next(40))
                });
                    }
                });

                using var stream = new MemoryStream();
                image.Save(stream, PngFormat.Instance);
                var base64Image = Convert.ToBase64String(stream.ToArray());

                return Ok(new
                {
                    CaptchaCode = captchaCode,
                    CaptchaImage = $"data:image/png;base64,{base64Image}"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"生成驗證碼失敗: {ex.Message}" });
            }
        }

        private static string GenerateCaptchaCode()
        {
            var chars = "23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz";
            var random = new Random();
            var code = new char[4];
            for (int i = 0; i < 4; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }
            return new string(code);
        }

        private static void AddNoisePoints(IImageProcessingContext ctx, int width, int height)
        {
            for (int i = 0; i < 30; i++)
            {
                ctx.DrawLine(Pens.Solid(Color.Gray, 1), new[]
                {
                    new PointF(Random.Next(width), Random.Next(height)),
                    new PointF(Random.Next(width) + 1, Random.Next(height) + 1)
                });
            }
        }
    }
}
