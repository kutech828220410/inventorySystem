using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;
using MyPrinterlib;
using MyOffice;
using HIS_DB_Lib;

namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private void Function_取得本地儲位()
        {
            this.List_本地儲位 = storageUI_EPD_266.SQL_GetAllStorage();
        }
        private List<object[]> Function_取得備藥通知(DateTime dt_start, DateTime dt_end)
        {
            string url = $"{API_Server}/api/ChemotherapyRxScheduling/get_udnoectc_by_ctdate_st_end";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = $"{dt_start.ToDateString()} 00:00:00,{dt_end.ToDateString()} 23:59:59";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json.JsonDeserializet<returnData>();

            List<udnoectc> udnoectcs = returnData.Data.ObjToListClass<udnoectc>();
            List<object[]> list_udnoectc = udnoectcs.ClassToSQL<udnoectc, enum_udnoectc>();
            return list_udnoectc;
        }


        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            return GetFigurePath(rect, radius, 0);
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius, int offset)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0) radius = 1;
            path.StartFigure();
            path.AddArc(rect.X + 0, rect.Y + 0, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width + offset - radius, rect.Y + 0, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width + offset - radius, rect.Y + rect.Height + offset - radius, radius, radius, 0, 90);
            path.AddArc(rect.X + 0, rect.Y + rect.Height + offset - radius,  radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
        public void DrawRoundShadow(Graphics g, RectangleF rect, Color ShadowColor, float radius, int width)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int penWidth = 3;
            int index = 1;
            using (Pen pen = new Pen(ShadowColor, penWidth))
            {
                int color_temp_R = ShadowColor.R;
                int offset_color_R = (254 - ShadowColor.R) / (width + penWidth);

                int color_temp_G = ShadowColor.G;
                int offset_color_G = (254 - ShadowColor.G) / (width + penWidth);

                int color_temp_B = ShadowColor.B;
                int offset_color_B = (254 - ShadowColor.B) / (width + penWidth);

                for (int i = -penWidth; i < width; i++)
                {
                    color_temp_R += offset_color_R;
                    color_temp_G += offset_color_G;
                    color_temp_B += offset_color_B;
                    pen.Color = Color.FromArgb(255, color_temp_R, color_temp_G, color_temp_B);
                    using (GraphicsPath pathBorder = this.GetFigurePath(rect, radius, i))
                    {
                        g.DrawPath(pen, pathBorder);
                    }
                }
            }
        }
    }
}
