using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using MyUI;
using SQLUI;
using HIS_DB_Lib;
using H_Pannel_lib;

namespace 智能藥庫系統
{
    public partial class Dialog_庫存查詢 : MyDialog
    {
        public enum ContextMenuStrip_進階搜尋
        {
            近效期,
            低於安全量,
            低於基準量,
        }

        public static bool IsShown = false;
        [EnumDescription("")]
        public enum enum_庫存查詢
        {
            [Description("GUID,VARCHAR,50,PRIMARY")]
            GUID,
            [Description("藥碼,VARCHAR,50,INDEX")]
            藥碼,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("中文名,VARCHAR,15,NONE")]
            中文名,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
            [Description("藥庫庫存,VARCHAR,15,NONE")]
            藥庫庫存,
            [Description("藥庫安全量,VARCHAR,15,NONE")]
            藥庫安全量,
            [Description("藥庫基準量,VARCHAR,15,NONE")]
            藥庫基準量,
            [Description("藥局庫存,VARCHAR,15,NONE")]
            藥局庫存,
            [Description("藥局安全量,VARCHAR,15,NONE")]
            藥局安全量,
            [Description("藥局基準量,VARCHAR,15,NONE")]
            藥局基準量,
  
        }
        [EnumDescription("")]
        public enum enum_效期及批號
        {
            [Description("GUID,VARCHAR,50,PRIMARY")]
            GUID,
            [Description("效期,VARCHAR,50,INDEX")]
            效期,
            [Description("批號,VARCHAR,15,NONE")]
            批號,
            [Description("庫存,VARCHAR,15,NONE")]
            庫存,
        }

        public Dialog_庫存查詢()
        {
            form.Invoke(new Action(delegate { InitializeComponent(); }));

            Reflection.MakeDoubleBuffered(this, true);

            this.TopLevel = true;
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.CanResize = true;

            this.Load += Dialog_庫存查詢_Load;
            this.FormClosing += Dialog_庫存查詢_FormClosing;
            this.ShowDialogEvent += Dialog_庫存查詢_ShowDialogEvent;

            this.rJ_Button_顯示全部.MouseDownEvent += RJ_Button_顯示全部_MouseDownEvent;
            this.rJ_Button_藥碼搜尋.MouseDownEvent += RJ_Button_藥碼搜尋_MouseDownEvent;
            this.rJ_Button_藥名搜尋.MouseDownEvent += RJ_Button_藥名搜尋_MouseDownEvent;
            this.rJ_Button_中文名搜尋.MouseDownEvent += RJ_Button_中文名搜尋_MouseDownEvent;

   

            this.Resize += Dialog_庫存查詢_Resize;
        }

        private void Dialog_庫存查詢_Resize(object sender, EventArgs e)
        {
            int width = 0;
            width = (this.Width - this.panel_效期及批號.Padding.Horizontal - 5) / 2;
            this.panel_藥庫_效期及批號.Width = width;
            this.panel_藥局_效期及批號.Width = width;
        }
        private void Dialog_庫存查詢_Load(object sender, EventArgs e)
        {
            Table table;
            table = new Table(new enum_庫存查詢());
            sqL_DataGridView_庫存查詢.Init(table);
            sqL_DataGridView_庫存查詢.Set_ColumnVisible(false, new enum_庫存查詢().GetEnumNames());
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleLeft, enum_庫存查詢.藥碼);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(280, DataGridViewContentAlignment.MiddleLeft, enum_庫存查詢.藥名);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(280, DataGridViewContentAlignment.MiddleLeft, enum_庫存查詢.中文名);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_庫存查詢.單位);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_庫存查詢.藥庫庫存);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_庫存查詢.藥庫安全量);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_庫存查詢.藥庫基準量);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_庫存查詢.藥局庫存);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_庫存查詢.藥局安全量);
            sqL_DataGridView_庫存查詢.Set_ColumnWidth(70, DataGridViewContentAlignment.MiddleCenter, enum_庫存查詢.藥局基準量);


            sqL_DataGridView_庫存查詢.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_庫存查詢.藥碼);
            sqL_DataGridView_庫存查詢.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_庫存查詢.藥名);
            sqL_DataGridView_庫存查詢.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_庫存查詢.中文名);
            sqL_DataGridView_庫存查詢.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_庫存查詢.單位);
            sqL_DataGridView_庫存查詢.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_庫存查詢.藥庫庫存);
            sqL_DataGridView_庫存查詢.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_庫存查詢.藥局庫存);
            sqL_DataGridView_庫存查詢.RowHeaderPostPaintingEvent += SqL_DataGridView_庫存查詢_RowHeaderPostPaintingEvent;
            sqL_DataGridView_庫存查詢.DataGridRefreshEvent += SqL_DataGridView_庫存查詢_DataGridRefreshEvent;
            sqL_DataGridView_庫存查詢.RowClickEvent += SqL_DataGridView_庫存查詢_RowClickEvent;
            sqL_DataGridView_庫存查詢.RowEnterEvent += SqL_DataGridView_庫存查詢_RowEnterEvent;
            table = new Table(new enum_效期及批號());
            sqL_DataGridView_藥庫_效期及批號.Init(table);
            sqL_DataGridView_藥庫_效期及批號.Set_ColumnVisible(false, new enum_效期及批號().GetEnumNames());
            sqL_DataGridView_藥庫_效期及批號.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.效期);
            sqL_DataGridView_藥庫_效期及批號.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.批號);
            sqL_DataGridView_藥庫_效期及批號.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.庫存);
            sqL_DataGridView_藥庫_效期及批號.RowHeaderPostPaintingEvent += SqL_DataGridView_藥庫_效期及批號_RowHeaderPostPaintingEvent;

        


            table = new Table(new enum_效期及批號());
            sqL_DataGridView_藥局_效期及批號.Init(table);
            sqL_DataGridView_藥局_效期及批號.Set_ColumnVisible(false, new enum_效期及批號().GetEnumNames());
            sqL_DataGridView_藥局_效期及批號.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.效期);
            sqL_DataGridView_藥局_效期及批號.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.批號);
            sqL_DataGridView_藥局_效期及批號.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleCenter, enum_效期及批號.庫存);
            sqL_DataGridView_藥局_效期及批號.RowHeaderPostPaintingEvent += SqL_DataGridView_藥局_效期及批號_RowHeaderPostPaintingEvent;

            int width = 0;
            width = (this.Width - this.panel_效期及批號.Padding.Horizontal - 5) / 2;
            this.panel_藥庫_效期及批號.Width = width;
            this.panel_藥局_效期及批號.Width = width;

            this.plC_RJ_Button_藥庫_效期及批號_新增.MouseDownEvent += PlC_RJ_Button_藥庫_效期及批號_新增_MouseDownEvent;
            this.plC_RJ_Button_藥庫_效期及批號_刪除.MouseDownEvent += PlC_RJ_Button_藥庫_效期及批號_刪除_MouseDownEvent;
            this.plC_RJ_Button_藥庫_效期及批號_修改.MouseDownEvent += PlC_RJ_Button_藥庫_效期及批號_修改_MouseDownEvent;

            this.plC_RJ_Button_藥局_效期及批號_新增.MouseDownEvent += PlC_RJ_Button_藥局_效期及批號_新增_MouseDownEvent;
            this.plC_RJ_Button_藥局_效期及批號_刪除.MouseDownEvent += PlC_RJ_Button_藥局_效期及批號_刪除_MouseDownEvent;
            this.plC_RJ_Button_藥局_效期及批號_修改.MouseDownEvent += PlC_RJ_Button_藥局_效期及批號_修改_MouseDownEvent;

            this.plC_RJ_Button_匯出.MouseDownEvent += PlC_RJ_Button_匯出_MouseDownEvent;
            this.plC_RJ_Button_進階搜尋.MouseDownEvent += PlC_RJ_Button_進階搜尋_MouseDownEvent;
            IsShown = true;
        }

   

        #region Function
        private List<object[]> Function_取得庫存查詢列表()
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();

            List<DeviceBasic> deviceBasics_藥庫 = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_藥庫_buf = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_藥局 = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_藥局_buf = new List<DeviceBasic>();
            List<medClass> medClasses = new List<medClass>();
            List<medClass> medClasses_buf = new List<medClass>();

            List<medClass> medClasses_藥庫 = new List<medClass>();
            List<medClass> medClasses_藥庫_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_medClasses_藥庫 = new Dictionary<string, List<medClass>>();

            List<medClass> medClasses_藥局 = new List<medClass>();
            List<medClass> medClasses_藥局_buf = new List<medClass>();
            Dictionary<string, List<medClass>> keyValuePairs_medClasses_藥局 = new Dictionary<string, List<medClass>>();


            Dictionary<string, List<DeviceBasic>> keyValuePairs_藥庫 = new Dictionary<string, List<DeviceBasic>>();
            Dictionary<string, List<DeviceBasic>> keyValuePairs_藥局 = new Dictionary<string, List<DeviceBasic>>();
            Dictionary<string, List<medClass>> keyValuePairs_medClasses = new Dictionary<string, List<medClass>>();

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(new Action(delegate 
            {
                deviceBasics_藥庫 = deviceApiClass.GetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥庫);
                keyValuePairs_藥庫 = deviceBasics_藥庫.CoverToDictionaryByCode();
            })));
            tasks.Add(Task.Run(new Action(delegate
            {
                deviceBasics_藥局 = deviceApiClass.GetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥局);
                keyValuePairs_藥局 = deviceBasics_藥局.CoverToDictionaryByCode();
            })));
            tasks.Add(Task.Run(new Action(delegate
            {
                medClasses = medClass.get_med_cloud(Main_Form.API_Server);
                keyValuePairs_medClasses = medClass.CoverToDictionaryByCode(medClasses);
            })));
            tasks.Add(Task.Run(new Action(delegate
            {
                medClasses_藥庫 = medClass.get_by_apiserver(Main_Form.API_Server,"ds01","藥庫", medClass.StoreType.藥庫);
                keyValuePairs_medClasses_藥庫 = medClass.CoverToDictionaryByCode(medClasses_藥庫);  
            })));
            tasks.Add(Task.Run(new Action(delegate
            {
                medClasses_藥局 = medClass.get_by_apiserver(Main_Form.API_Server, "ds01", "藥庫", medClass.StoreType.藥局);
                keyValuePairs_medClasses_藥局 = medClass.CoverToDictionaryByCode(medClasses_藥局);
            })));

            Task.WhenAll(tasks).Wait();


            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < medClasses.Count; i++)
            {
                object[] value = new object[new enum_庫存查詢().GetLength()];
                value[(int)enum_庫存查詢.GUID] = medClasses[i].GUID;
                value[(int)enum_庫存查詢.藥碼] = medClasses[i].藥品碼;
                medClasses_buf = medClass.SortDictionaryByCode(keyValuePairs_medClasses, medClasses[i].藥品碼);
                if (medClasses_buf.Count > 0)
                {
                    value[(int)enum_庫存查詢.藥名] = medClasses_buf[0].藥品名稱;
                    value[(int)enum_庫存查詢.中文名] = medClasses_buf[0].中文名稱;
                    if (medClasses_buf[0].包裝單位.StringIsEmpty()) medClasses_buf[0].包裝單位 = "-";
                    value[(int)enum_庫存查詢.單位] = medClasses_buf[0].包裝單位;

                    value[(int)enum_庫存查詢.藥庫庫存] = "-";
                    value[(int)enum_庫存查詢.藥局安全量] = "-";
                    value[(int)enum_庫存查詢.藥局基準量] = "-";
                    value[(int)enum_庫存查詢.藥局庫存] = "-";
                    value[(int)enum_庫存查詢.藥庫安全量] = "-";
                    value[(int)enum_庫存查詢.藥庫基準量] = "-";


                    deviceBasics_藥庫_buf = keyValuePairs_藥庫.SortDictionaryByCode(medClasses[i].藥品碼);
                    deviceBasics_藥局_buf = keyValuePairs_藥局.SortDictionaryByCode(medClasses[i].藥品碼);
                    medClasses_藥庫_buf = medClass.SortDictionaryByCode(keyValuePairs_medClasses_藥庫, medClasses[i].藥品碼);
                    medClasses_藥局_buf = medClass.SortDictionaryByCode(keyValuePairs_medClasses_藥局, medClasses[i].藥品碼);
                    if (deviceBasics_藥庫_buf.Count > 0)
                    {
                        value[(int)enum_庫存查詢.藥庫庫存] = deviceBasics_藥庫_buf[0].Inventory;
                    }
                    if (deviceBasics_藥局_buf.Count > 0)
                    {
                        value[(int)enum_庫存查詢.藥局庫存] = deviceBasics_藥局_buf[0].Inventory;
                    }

                    if (medClasses_藥庫_buf.Count > 0)
                    {
                        value[(int)enum_庫存查詢.藥庫安全量] = medClasses_藥庫_buf[0].安全庫存;
                        value[(int)enum_庫存查詢.藥庫基準量] = medClasses_藥庫_buf[0].基準量;
                    }
                    if (medClasses_藥局_buf.Count > 0)
                    {
                        value[(int)enum_庫存查詢.藥局安全量] = medClasses_藥局_buf[0].安全庫存;
                        value[(int)enum_庫存查詢.藥局基準量] = medClasses_藥局_buf[0].基準量;
                    }
                    list_value.Add(value);
                }


            }

            Console.WriteLine($"取得庫存查詢列表成功,{myTimerBasic}");
            return list_value;
        }
        #endregion
        #region Event
        private void SqL_DataGridView_藥庫_效期及批號_RowHeaderPostPaintingEvent(object sender, Graphics g, Rectangle rect_hedder, Brush brush_background, Pen pen_border)
        {
            Brush brush = brush_background;
            Pen pen = pen_border;

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            Rectangle rectangle;
            DataGridView dataGridView = this.sqL_DataGridView_藥庫_效期及批號.dataGridView;
            DataGridViewColumnCollection columns = dataGridView.Columns;
            using (Brush brush_title_background = new SolidBrush(Color.WhiteSmoke))
            using (Brush brush_hedder = new SolidBrush(Color.Gray))
            {

                g.FillRectangle(brush_hedder, rect_hedder);
                g.DrawRectangle(pen, rect_hedder);

                rectangle = this.sqL_DataGridView_藥庫_效期及批號.GetColumnBounds(enum_效期及批號.效期.GetEnumName(), enum_效期及批號.庫存.GetEnumName());
                g.FillRectangle(brush, rectangle);


                rectangle.Height = rectangle.Height / 2;
                g.FillRectangle(brush_title_background, rectangle);
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "藥庫", new Font("微軟正黑體", 14, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);


                rectangle = this.sqL_DataGridView_藥庫_效期及批號.GetColumnBounds(enum_效期及批號.效期.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "效期", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_藥庫_效期及批號.GetColumnBounds(enum_效期及批號.批號.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "批號", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_藥庫_效期及批號.GetColumnBounds(enum_效期及批號.庫存.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "庫存", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

            }
        }
        private void SqL_DataGridView_藥局_效期及批號_RowHeaderPostPaintingEvent(object sender, Graphics g, Rectangle rect_hedder, Brush brush_background, Pen pen_border)
        {
            Brush brush = brush_background;
            Pen pen = pen_border;

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            Rectangle rectangle;
            DataGridView dataGridView = this.sqL_DataGridView_藥局_效期及批號.dataGridView;
            DataGridViewColumnCollection columns = dataGridView.Columns;
            using (Brush brush_title_background = new SolidBrush(Color.WhiteSmoke))
            using (Brush brush_hedder = new SolidBrush(Color.Gray))
            {

                g.FillRectangle(brush_hedder, rect_hedder);
                g.DrawRectangle(pen, rect_hedder);

                rectangle = this.sqL_DataGridView_藥局_效期及批號.GetColumnBounds(enum_效期及批號.效期.GetEnumName(), enum_效期及批號.庫存.GetEnumName());
                g.FillRectangle(brush, rectangle);


                rectangle.Height = rectangle.Height / 2;
                g.FillRectangle(brush_title_background, rectangle);
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "藥局", new Font("微軟正黑體", 14, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);


                rectangle = this.sqL_DataGridView_藥局_效期及批號.GetColumnBounds(enum_效期及批號.效期.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "效期", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_藥局_效期及批號.GetColumnBounds(enum_效期及批號.批號.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "批號", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_藥局_效期及批號.GetColumnBounds(enum_效期及批號.庫存.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "庫存", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

            }
        }
        private void SqL_DataGridView_庫存查詢_RowHeaderPostPaintingEvent(object sender, Graphics g, Rectangle rect_hedder, Brush brush_background, Pen pen_border)
        {
            Brush brush = brush_background;
            Pen pen = pen_border;

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            Rectangle rectangle;
            DataGridView dataGridView = this.sqL_DataGridView_庫存查詢.dataGridView;
            DataGridViewColumnCollection columns = dataGridView.Columns;
            using (Brush brush_title_background = new SolidBrush(Color.WhiteSmoke))
            using (Brush brush_hedder = new SolidBrush(Color.Gray))
            {

                g.FillRectangle(brush_hedder, rect_hedder);
                g.DrawRectangle(pen, rect_hedder);
                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥碼.GetEnumName(), enum_庫存查詢.藥局基準量.GetEnumName());
                g.FillRectangle(brush, rectangle);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥碼.GetEnumName());
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "藥碼", new Font("微軟正黑體", 15, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥名.GetEnumName());
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "藥名", new Font("微軟正黑體", 15, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.中文名.GetEnumName());
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "中文名", new Font("微軟正黑體", 15, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.單位.GetEnumName());
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "單位", new Font("微軟正黑體", 15, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥庫庫存.GetEnumName(), enum_庫存查詢.藥庫基準量.GetEnumName());
                rectangle.Height = rectangle.Height / 2;
                g.FillRectangle(brush_title_background, rectangle);
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "藥庫", new Font("微軟正黑體", 14, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥庫庫存.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;           
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "庫存", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥庫安全量.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "安全量", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥庫基準量.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "基準量", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);


                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥局庫存.GetEnumName(), enum_庫存查詢.藥局基準量.GetEnumName());
                rectangle.Height = rectangle.Height / 2;
                g.FillRectangle(brush_title_background, rectangle);
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "藥局", new Font("微軟正黑體", 14, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);
                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥局庫存.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "庫存", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);
                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥局安全量.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "安全量", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);
                rectangle = this.sqL_DataGridView_庫存查詢.GetColumnBounds(enum_庫存查詢.藥局基準量.GetEnumName());
                rectangle.Y = rectangle.Height / 2;
                rectangle.Height = rectangle.Height / 2;
                g.DrawRectangle(pen, rectangle);
                DrawingClass.Draw.DrawString(g, "基準量", new Font("微軟正黑體", 12, FontStyle.Bold), rectangle, Color.Black, DataGridViewContentAlignment.MiddleCenter);

            }
        }
        private void Panel_庫存查詢_Header_Paint(object sender, PaintEventArgs e)
        {
          
            
        }
        private void SqL_DataGridView_庫存查詢_DataGridRefreshEvent()
        {
            sqL_DataGridView_藥庫_效期及批號.ClearGrid();
        }
        private void SqL_DataGridView_庫存查詢_RowEnterEvent(object[] RowValue)
        {
            Task.Run(new Action(delegate
            {
                List<object[]> list_value = new List<object[]>();
                string 藥碼 = RowValue[(int)enum_庫存查詢.藥碼].ObjectToString();
                List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥庫);

                if (deviceBasics.Count > 0)
                {
                    List<StockClass> stockClasses = deviceBasics[0].stockClasses;
                    for (int i = 0; i < stockClasses.Count; i++)
                    {
                        if (stockClasses[i].Qty.StringToInt32() <= 0)
                        {
                            continue;
                        }
                        object[] value = new object[new enum_效期及批號().GetLength()];
                        value[(int)enum_效期及批號.效期] = stockClasses[i].Validity_period;
                        if (stockClasses[i].Lot_number.StringIsEmpty()) stockClasses[i].Lot_number = "無";
                        value[(int)enum_效期及批號.批號] = stockClasses[i].Lot_number;
                        value[(int)enum_效期及批號.庫存] = stockClasses[i].Qty;
                        list_value.Add(value);

                    }
                }

                sqL_DataGridView_藥庫_效期及批號.RefreshGrid(list_value);
            }));
            Task.Run(new Action(delegate
            {

                List<object[]> list_value = new List<object[]>();
                string 藥碼 = RowValue[(int)enum_庫存查詢.藥碼].ObjectToString();
                List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥局);

                if (deviceBasics.Count > 0)
                {
                    List<StockClass> stockClasses = deviceBasics[0].stockClasses;
                    for (int i = 0; i < stockClasses.Count; i++)
                    {
                        if (stockClasses[i].Qty.StringToInt32() <= 0)
                        {
                            continue;
                        }
                        object[] value = new object[new enum_效期及批號().GetLength()];
                        value[(int)enum_效期及批號.效期] = stockClasses[i].Validity_period;
                        if (stockClasses[i].Lot_number.StringIsEmpty()) stockClasses[i].Lot_number = "無";
                        value[(int)enum_效期及批號.批號] = stockClasses[i].Lot_number;
                        value[(int)enum_效期及批號.庫存] = stockClasses[i].Qty;
                        list_value.Add(value);

                    }
                }

                sqL_DataGridView_藥局_效期及批號.RefreshGrid(list_value);
            }));
        }
        private void SqL_DataGridView_庫存查詢_RowClickEvent(object[] RowValue)
        {
           
        }
        private void Dialog_庫存查詢_ShowDialogEvent()
        {
            if (IsShown)
            {
                MyDialog.BringDialogToFront(this.Text);
                this.DialogResult = DialogResult.Cancel;
            }
        }
        private void Dialog_庫存查詢_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsShown = false;
        }
        private void PlC_RJ_Button_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
         
        }
        private void RJ_Button_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<object[]> list_value = this.Function_取得庫存查詢列表();
            sqL_DataGridView_庫存查詢.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_藥碼搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<object[]> list_value = this.Function_取得庫存查詢列表();
            string text = rJ_TextBox_藥碼搜尋.Texts;
            if (text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請輸入搜尋條件", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (rJ_RatioButton_搜尋方式_前綴.Checked)
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_庫存查詢.藥碼].ObjectToString().ToUpper().StartsWith(text.ToUpper())
                              select temp).ToList();
            }
            if (rJ_RatioButton_搜尋方式_模糊.Checked)
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_庫存查詢.藥碼].ObjectToString().ToUpper().Contains(text.ToUpper())
                              select temp).ToList();
            }
            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }


            sqL_DataGridView_庫存查詢.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_藥名搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<object[]> list_value = this.Function_取得庫存查詢列表();
            string text = rJ_TextBox_藥名搜尋.Texts;
            if (text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請輸入搜尋條件", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (rJ_RatioButton_搜尋方式_前綴.Checked)
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_庫存查詢.藥名].ObjectToString().ToUpper().StartsWith(text.ToUpper())
                              select temp).ToList();
            }
            if (rJ_RatioButton_搜尋方式_模糊.Checked)
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_庫存查詢.藥名].ObjectToString().ToUpper().Contains(text.ToUpper())
                              select temp).ToList();
            }

            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }


            sqL_DataGridView_庫存查詢.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void RJ_Button_中文名搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            LoadingForm.ShowLoadingForm();
            List<object[]> list_value = this.Function_取得庫存查詢列表();
            string text = rJ_TextBox_中文名搜尋.Texts;
            if (text.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("請輸入搜尋條件", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (rJ_RatioButton_搜尋方式_前綴.Checked)
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_庫存查詢.中文名].ObjectToString().ToUpper().StartsWith(text.ToUpper())
                              select temp).ToList();
            }
            if (rJ_RatioButton_搜尋方式_模糊.Checked)
            {
                list_value = (from temp in list_value
                              where temp[(int)enum_庫存查詢.中文名].ObjectToString().ToUpper().Contains(text.ToUpper())
                              select temp).ToList();
            }

            if (list_value.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("查無資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }


            sqL_DataGridView_庫存查詢.RefreshGrid(list_value);
            LoadingForm.CloseLoadingForm();
        }
        private void PlC_RJ_Button_藥庫_效期及批號_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_庫存查詢 = sqL_DataGridView_庫存查詢.Get_All_Select_RowsValues();
            if (list_庫存查詢.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_庫存查詢[0][(int)enum_庫存查詢.藥碼].ObjectToString();
            this.Invoke(new Action(delegate
            {
                Dialog_效期批號輸入 dialog_效期批號輸入 = new Dialog_效期批號輸入(藥碼);
                if (dialog_效期批號輸入.ShowDialog() != DialogResult.Yes) return;
                string 效期 = dialog_效期批號輸入.效期;
                string 批號 = dialog_效期批號輸入.批號;

                List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥庫);
                if (deviceBasics.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"找無儲位資訊", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                if (deviceBasics[0].取得庫存(效期) > 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"效期:{效期}已存在,無法新增", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入數量");
                if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
                int Value = dialog_NumPannel.Value;
                if (Value <= 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("數量不得小於\"1\"", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                deviceBasics[0].新增效期(效期, 批號, Value.ToString());
                deviceApiClass.SetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥庫, deviceBasics);
                LoadingForm.ShowLoadingForm();
                sqL_DataGridView_庫存查詢.RefreshGrid(Function_取得庫存查詢列表());
                sqL_DataGridView_庫存查詢.On_RowEnter();
                LoadingForm.CloseLoadingForm();
            }));
        }
        private void PlC_RJ_Button_藥庫_效期及批號_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否刪除選取效期?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<object[]> list_庫存查詢 = sqL_DataGridView_庫存查詢.Get_All_Select_RowsValues();
            if (list_庫存查詢.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_庫存查詢[0][(int)enum_庫存查詢.藥碼].ObjectToString();
            List<object[]> list_效期及批號 = sqL_DataGridView_藥庫_效期及批號.Get_All_Select_RowsValues();
            if (list_效期及批號.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 效期 = list_效期及批號[0][(int)enum_效期及批號.效期].ObjectToString();

            this.Invoke(new Action(delegate
            {
                List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥庫);
                if (deviceBasics.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"找無儲位資訊", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }          

                deviceBasics[0].清除效期(效期);

                deviceApiClass.SetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥庫, deviceBasics);
                LoadingForm.ShowLoadingForm();
                sqL_DataGridView_庫存查詢.RefreshGrid(Function_取得庫存查詢列表());
                sqL_DataGridView_庫存查詢.On_RowEnter();
                LoadingForm.CloseLoadingForm();
            }));
        }
        private void PlC_RJ_Button_藥庫_效期及批號_修改_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_庫存查詢 = sqL_DataGridView_庫存查詢.Get_All_Select_RowsValues();
            List<object[]> list_效期及批號 = sqL_DataGridView_藥庫_效期及批號.Get_All_Select_RowsValues();
            if(list_庫存查詢.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"未選取藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (list_效期及批號.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"未選取效期", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }

            string 藥碼 = list_庫存查詢[0][(int)enum_庫存查詢.藥碼].ObjectToString();
            string 藥名 = list_庫存查詢[0][(int)enum_庫存查詢.藥名].ObjectToString();
            string 效期 = list_效期及批號[0][(int)enum_效期及批號.效期].ObjectToString();
            string 批號 = list_效期及批號[0][(int)enum_效期及批號.批號].ObjectToString();
            int 數量 = list_效期及批號[0][(int)enum_效期及批號.庫存].StringToInt32();
            List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥庫);

            this.Invoke(new Action(delegate
            {
                Dialog_批號數量修改 dialog_批號數量修改 = new Dialog_批號數量修改(藥碼, 藥名, 效期, 批號, 數量);
                if (dialog_批號數量修改.ShowDialog() != DialogResult.Yes) return;
                StockClass stockClass = dialog_批號數量修改.Value;
                deviceBasics[0].效期庫存覆蓋(stockClass.Validity_period, stockClass.Lot_number, stockClass.Qty);

                deviceApiClass.SetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥庫, deviceBasics);
                LoadingForm.ShowLoadingForm();
                sqL_DataGridView_庫存查詢.RefreshGrid(Function_取得庫存查詢列表());
                sqL_DataGridView_庫存查詢.On_RowEnter();
                LoadingForm.CloseLoadingForm();
            }));
        }

        private void PlC_RJ_Button_藥局_效期及批號_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_庫存查詢 = sqL_DataGridView_庫存查詢.Get_All_Select_RowsValues();
            if (list_庫存查詢.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_庫存查詢[0][(int)enum_庫存查詢.藥碼].ObjectToString();
            this.Invoke(new Action(delegate
            {
                Dialog_效期批號輸入 dialog_效期批號輸入 = new Dialog_效期批號輸入(藥碼);
                if (dialog_效期批號輸入.ShowDialog() != DialogResult.Yes) return;
                string 效期 = dialog_效期批號輸入.效期;
                string 批號 = dialog_效期批號輸入.批號;

                List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥局);
                if (deviceBasics.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"找無儲位資訊", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }
                if (deviceBasics[0].取得庫存(效期) > 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"效期:{效期}已存在,無法新增", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel("請輸入數量");
                if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
                int Value = dialog_NumPannel.Value;
                if (Value <= 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("數量不得小於\"1\"", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                deviceBasics[0].新增效期(效期, 批號, Value.ToString());
                deviceApiClass.SetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥局, deviceBasics);
                LoadingForm.ShowLoadingForm();
                sqL_DataGridView_庫存查詢.RefreshGrid(Function_取得庫存查詢列表());
                sqL_DataGridView_庫存查詢.On_RowEnter();
                LoadingForm.CloseLoadingForm();
            }));
        }
        private void PlC_RJ_Button_藥局_效期及批號_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            if (MyMessageBox.ShowDialog("是否刪除選取效期?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            List<object[]> list_庫存查詢 = sqL_DataGridView_庫存查詢.Get_All_Select_RowsValues();
            if (list_庫存查詢.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 藥碼 = list_庫存查詢[0][(int)enum_庫存查詢.藥碼].ObjectToString();
            List<object[]> list_效期及批號 = sqL_DataGridView_藥局_效期及批號.Get_All_Select_RowsValues();
            if (list_效期及批號.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm("未選取資料", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            string 效期 = list_效期及批號[0][(int)enum_效期及批號.效期].ObjectToString();

            this.Invoke(new Action(delegate
            {
                List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥局);
                if (deviceBasics.Count == 0)
                {
                    Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"找無儲位資訊", 1500);
                    dialog_AlarmForm.ShowDialog();
                    return;
                }

                deviceBasics[0].清除效期(效期);

                deviceApiClass.SetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥局, deviceBasics);
                LoadingForm.ShowLoadingForm();
                sqL_DataGridView_庫存查詢.RefreshGrid(Function_取得庫存查詢列表());
                sqL_DataGridView_庫存查詢.On_RowEnter();
                LoadingForm.CloseLoadingForm();
            }));
        }
        private void PlC_RJ_Button_藥局_效期及批號_修改_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_庫存查詢 = sqL_DataGridView_庫存查詢.Get_All_Select_RowsValues();
            List<object[]> list_效期及批號 = sqL_DataGridView_藥局_效期及批號.Get_All_Select_RowsValues();
            if (list_庫存查詢.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"未選取藥品", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            if (list_效期及批號.Count == 0)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"未選取效期", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }

            string 藥碼 = list_庫存查詢[0][(int)enum_庫存查詢.藥碼].ObjectToString();
            string 藥名 = list_庫存查詢[0][(int)enum_庫存查詢.藥名].ObjectToString();
            string 效期 = list_效期及批號[0][(int)enum_效期及批號.效期].ObjectToString();
            string 批號 = list_效期及批號[0][(int)enum_效期及批號.批號].ObjectToString();
            int 數量 = list_效期及批號[0][(int)enum_效期及批號.庫存].StringToInt32();
            List<DeviceBasic> deviceBasics = deviceApiClass.Get_StoreHouse_DeviceBasicsByCodes(Main_Form.API_Server, "ds01", "藥庫", 藥碼, deviceApiClass.StoreType.藥局);

            this.Invoke(new Action(delegate
            {
                Dialog_批號數量修改 dialog_批號數量修改 = new Dialog_批號數量修改(藥碼, 藥名, 效期, 批號, 數量);
                if (dialog_批號數量修改.ShowDialog() != DialogResult.Yes) return;
                StockClass stockClass = dialog_批號數量修改.Value;
                deviceBasics[0].效期庫存覆蓋(stockClass.Validity_period, stockClass.Lot_number, stockClass.Qty);

                deviceApiClass.SetDeviceBasics(Main_Form.API_Server, "ds01", "藥庫", deviceApiClass.StoreType.藥局, deviceBasics);
                LoadingForm.ShowLoadingForm();
                sqL_DataGridView_庫存查詢.RefreshGrid(Function_取得庫存查詢列表());
                sqL_DataGridView_庫存查詢.On_RowEnter();
                LoadingForm.CloseLoadingForm();
            }));
        }

        private void PlC_RJ_Button_進階搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_進階搜尋());
            dialog_ContextMenuStrip.TitleText = "進階搜尋";
            if (dialog_ContextMenuStrip.ShowDialog() != DialogResult.Yes) return;
        }
        #endregion


    }
}
