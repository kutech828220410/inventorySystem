using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;
using MyOffice;
using NPOI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class deviceController : Controller
    {
        static private string API_Server = ConfigurationManager.AppSettings["API_Server"];

        static private string device_Server = ConfigurationManager.AppSettings["device_Server"];
        static private string device_DB = ConfigurationManager.AppSettings["device_DB"];
        static private string device_TableName = ConfigurationManager.AppSettings["device_TableName"];

        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        [Route("test")]
        [HttpGet]
        public string GET_test(string Code)
        {
            if (Code.StringIsEmpty()) return null;
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<DeviceBasic> deviceBasics = Function_Get_device(device_Server, device_DB, device_TableName, UserName, Password, Port);
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics_buf = deviceBasics.SortByCode($"{Code}");
            for (int i = 0; i < deviceBasics_buf.Count; i++)
            {
                DeviceType deviceType = deviceBasics_buf[i].DeviceType;
                if (deviceType == DeviceType.EPD290 || deviceType == DeviceType.EPD290_lock)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics_buf[i].IP, 0, Get_EPD290_LEDBytes(Color.Red));
                }
                if (deviceType == DeviceType.EPD266 || deviceType == DeviceType.EPD266_lock)
                {

                    H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics_buf[i].IP, 0, Get_EPD266_LEDBytes(Color.Red));
                }
                if (deviceType == DeviceType.RowsLED)
                {
                    SQLControl sQLControl_RowsLED_serialize = new SQLControl(device_Server, device_DB, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
                    RowsLED rowsLED = RowsLEDMethod.SQL_GetDevice(sQLControl_RowsLED_serialize, deviceBasics_buf[i].IP);
                    if (rowsLED == null) continue;
                    RowsDevice rowsDevice = rowsLED.SortByGUID(deviceBasics_buf[i].GUID);
                    if (rowsDevice == null) continue;
                    int maxLED = rowsLED.Maximum;
                    byte[] LED_Bytes = new byte[maxLED];
                    Get_Rows_LEDBytes(ref LED_Bytes, rowsDevice.StartLED, rowsDevice.EndLED, Color.Red);
                    H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics_buf[i].IP, 0, LED_Bytes);

                }
                if (deviceType == DeviceType.EPD583 || deviceType == DeviceType.EPD583_lock)
                {
                    SQLControl sQLControl_EPD583_serialize = new SQLControl(device_Server, device_DB, "epd583_jsonstring", UserName, Password, Port, SSLMode);
                    Drawer drawer = DrawerMethod.SQL_GetDevice(sQLControl_EPD583_serialize, deviceBasics_buf[i].IP);
                    if (drawer == null) continue;
                    Box box = drawer.GetBox(deviceBasics_buf[i].GUID);
                    if (box == null) continue;
                    WS2812_Init();
                    Set_LED_UDP(Startup.uDP_Class, drawer, box, Color.Red);

                }
            }
            returnData returnData = new returnData();
            returnData.Code = 200;
            returnData.Result = $"Device取得成功!TableName : {device_TableName}";
            returnData.TimeTaken = myTimer.ToString();
            returnData.Data = deviceBasics_buf;
            return returnData.JsonSerializationt(true);
        }

        [Route("all")]
        [HttpGet]
        public string GET_all()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<DeviceBasic> deviceBasics = Function_Get_device(device_Server, device_DB, device_TableName, UserName, Password, Port);
            returnData.Code = 200;
            returnData.Result = $"Device取得成功!TableName : {device_TableName}";
            returnData.TimeTaken = myTimer.ToString();
            returnData.Data = deviceBasics;
            return returnData.JsonSerializationt(true);
        }
        [Route("all")]
        [HttpPost]
        public string POST_all(returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "API_儲位資料");

                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }

                List<DeviceBasic> deviceBasics = Function_Get_device(serverSettingClasses[0]);
                returnData.Code = 200;
                returnData.Result = $"Device取得成功!TableName : {device_TableName}";
                returnData.Data = deviceBasics;
                return returnData.JsonSerializationt();
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Value = $"{e.Message}";
                return returnData.JsonSerializationt();
            }


        }
        [Route("light_web")]
        [HttpPost]
        public string POST_light_web(returnData returnData)
        {
            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "API02");

            if (serverSettingClasses.Count == 0)
            {
                returnData.Code = -200;
                returnData.Result = $"找無Server資料!";
                return returnData.JsonSerializationt();
            }
            string url = $"{serverSettingClasses[0].Server}/api/device/light";
            return Basic.Net.WEBApiPostJson(url, returnData.JsonSerializationt());
        }
        [Route("light")]
        [HttpPost]
        public string POST_light(returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData.Method = "light";
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Color color = returnData.Value.ToColor();
                string json_in = returnData.Data.JsonSerializationt();
                List<DeviceBasic> deviceBasics = json_in.JsonDeserializet<List<DeviceBasic>>();
                for (int i = 0; i < deviceBasics.Count; i++)
                {
                    DeviceType deviceType = deviceBasics[i].DeviceType;
                    if (deviceType == DeviceType.EPD290 || deviceType == DeviceType.EPD290_lock)
                    {
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, Get_EPD290_LEDBytes(color));
                    }
                    if (deviceType == DeviceType.EPD266 || deviceType == DeviceType.EPD266_lock)
                    {
                      
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, Get_EPD266_LEDBytes(color));
                    }
                    if (deviceType == DeviceType.RowsLED)
                    {
                        SQLControl sQLControl_RowsLED_serialize = new SQLControl(device_Server, device_DB, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
                        RowsLED rowsLED = RowsLEDMethod.SQL_GetDevice(sQLControl_RowsLED_serialize, deviceBasics[i].IP);
                        if (rowsLED == null) continue;
                        RowsDevice rowsDevice = rowsLED.SortByGUID(deviceBasics[i].GUID);
                        if (rowsDevice == null) continue;
                        int maxLED = rowsLED.Maximum;
                        byte[] LED_Bytes = new byte[maxLED];
                        Get_Rows_LEDBytes(ref LED_Bytes, rowsDevice.StartLED, rowsDevice.EndLED, color);
                        H_Pannel_lib.Communication.Set_WS2812_Buffer(Startup.uDP_Class, deviceBasics[i].IP, 0, LED_Bytes);

                    }
                    if (deviceType == DeviceType.EPD583 || deviceType == DeviceType.EPD583_lock)
                    {
                        SQLControl sQLControl_EPD583_serialize = new SQLControl(device_Server, device_DB, "epd583_jsonstring", UserName, Password, Port, SSLMode);
                        Drawer drawer = DrawerMethod.SQL_GetDevice(sQLControl_EPD583_serialize, deviceBasics[i].IP);
                        if (drawer == null) continue;
                        Box box = drawer.GetBox(deviceBasics[i].GUID);
                        if (box == null) continue;
                        WS2812_Init();
                        Set_LED_UDP(Startup.uDP_Class, drawer, box, color);

                    }
                }
                returnData.Code = 200;
                returnData.Result = $"設備亮燈完成!";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();

            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = $"{e.Message}";
                returnData.TimeTaken = myTimer.ToString();
                return returnData.JsonSerializationt();
            }
            finally
            {

            }
        }


        public List<DeviceBasic> Function_Get_device()
        {
            return Function_Get_device(device_Server, device_DB, device_TableName, UserName, Password, Port);
        }
        public List<DeviceBasic> Function_Get_device(returnData returnData)
        {
            return Function_Get_device(returnData.Server, returnData.DbName, returnData.TableName, returnData.UserName, returnData.Password, returnData.Port);
        }
        public List<DeviceBasic> Function_Get_device(ServerSettingClass serverSettingClass)
        {
            string IP = serverSettingClass.Server;
            string DBName = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            string TableName = "";
            if (serverSettingClass.類別 == "藥庫")
            {
                TableName = "firstclass_device_jsonstring";
            }
            SQLControl sQLControl_device = new SQLControl(IP, DBName, TableName, UserName, Password, Port, SSLMode);
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            if (TableName.StringIsEmpty() == false)
            {
                deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);
            }
            else
            {
                deviceBasics = Function_讀取儲位(IP, DBName, UserName, Password, Port);
            }
       
            return deviceBasics;
        }
        public List<DeviceBasic> Function_Get_device(string IP, string DBName, string TableName, string UserName, string Password, uint Port)
        {
            SQLControl sQLControl_device = new SQLControl(IP, DBName, TableName, UserName, Password, Port, SSLMode);
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            if (TableName.StringIsEmpty() == false)
            {
                deviceBasics = DeviceBasicMethod.SQL_GetAllDeviceBasic(sQLControl_device);
            }
            else
            {
                deviceBasics = Function_讀取儲位(IP, DBName, UserName, Password, Port);
            }

            return deviceBasics;
        }

        private List<DeviceBasic> Function_讀取儲位(string IP, string DBName, string UserName, string Password, uint Port)
        {
    
            SQLControl sQLControl_EPD583_serialize = new SQLControl(IP, DBName, "epd583_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DBName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DBName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(IP, DBName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_WT32_serialize = new SQLControl(IP, DBName, "WT32_Jsonstring", UserName, Password, Port, SSLMode);

            
            List<object[]> list_EPD583 = new List<object[]>();
            List<object[]> list_EPD266 = new List<object[]>();
            List<object[]> list_RowsLED = new List<object[]>();
            List<object[]> list_RFID_Device = new List<object[]>();
            List<object[]> list_WT32 = new List<object[]>();
            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() =>
            {
                list_EPD583 = sQLControl_EPD583_serialize.GetAllRows(null);
            }));
            taskList.Add(Task.Run(() =>
            {
               list_EPD266 = sQLControl_EPD266_serialize.GetAllRows(null);
            }));
            taskList.Add(Task.Run(() =>
            {
               list_RowsLED = sQLControl_RowsLED_serialize.GetAllRows(null);
            }));
            taskList.Add(Task.Run(() =>
            {
               list_RFID_Device = sQLControl_RFID_Device_serialize.GetAllRows(null);
            }));
            taskList.Add(Task.Run(() =>
            {
                list_WT32 = sQLControl_WT32_serialize.GetAllRows(null);
            }));
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics.LockAdd(DrawerMethod.GetAllDeviceBasic(list_EPD583));
            deviceBasics.LockAdd(StorageMethod.GetAllDeviceBasic(list_EPD266));
            deviceBasics.LockAdd(RowsLEDMethod.GetAllDeviceBasic(list_RowsLED));
            deviceBasics.LockAdd(RFIDMethod.GetAllDeviceBasic(list_RFID_Device));
            deviceBasics.LockAdd(StorageMethod.GetAllDeviceBasic(list_WT32));

            deviceBasics_buf = (from value in deviceBasics
                                where value.Code.StringIsEmpty() == false
                                select value).ToList();

            return deviceBasics_buf;
        }

        static public byte[] Get_EPD290_LEDBytes(Color color)
        {
            byte[] LED_Bytes = new byte[10 * 3];

            for (int i = 0; i < 10; i++)
            {
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }
        static public byte[] Get_EPD266_LEDBytes(Color color)
        {
            byte[] LED_Bytes = new byte[10 * 3];

            for (int i = 0; i < 10; i++)
            {
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }
        static public byte[] Get_Rows_LEDBytes(ref byte[] LED_Bytes, int startNum, int EndNum, Color color)
        {
            for (int i = startNum; i < EndNum; i++)
            {
                if (i > LED_Bytes.Length) break;
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }

        #region Drawer Function
        static public int NumOfLED = 450;
        static private int Drawer_NumOf_H_Line = 4;
        static private int Drawer_NumOf_V_Line = 8;
        static private int NumOfLED_Pannel = 42;
        static private int NumOfLED_Drawer = 450 - NumOfLED_Pannel;

        static public int Pannel_Width
        {
            get
            {
                return 648;
            }
        }
        static public int Pannel_Height
        {
            get
            {
                return 480;
            }
        }

        static public bool Set_LED_UDP(UDP_Class uDP_Class, string IP, byte[] LED_Bytes)
        {
            if (uDP_Class != null)
            {
                return Communication.Set_WS2812_Buffer(uDP_Class, IP, 0, LED_Bytes);
            }
            return false;
        }
        static public bool Set_LED_UDP(UDP_Class uDP_Class, Drawer drawer, Box box, Color color)
        {
            List<Box> boxes = new List<Box>();
            boxes.Add(box);
            return Set_LED_UDP(uDP_Class, drawer, boxes, color);
        }
        static public bool Set_LED_UDP(UDP_Class uDP_Class, Drawer drawer, List<Box> boxes, Color color)
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].IP == drawer.IP)
                {
                    drawer.LED_Bytes = Set_LEDBytes(drawer, boxes[i], color);
                }
            }
            drawer.LED_Bytes = Set_Pannel_LEDBytes(drawer, color);
            return Set_LED_UDP(uDP_Class, drawer.IP, drawer.LED_Bytes);
        }
        static public byte[] Set_Pannel_LEDBytes(Drawer drawer, Color color)
        {
            if (drawer.IsAllLight == false)
            {
                return Set_LEDBytes(drawer, color);
            }
            return Set_Pannel_LEDBytes(ref drawer.LED_Bytes, color);
        }
        static public byte[] Set_Pannel_LEDBytes(ref byte[] LED_Bytes, Color color)
        {
            for (int i = NumOfLED_Drawer; i < NumOfLED; i++)
            {
                if (i * 3 > LED_Bytes.Length) return LED_Bytes;
                LED_Bytes[i * 3 + 0] = color.R;
                LED_Bytes[i * 3 + 1] = color.G;
                LED_Bytes[i * 3 + 2] = color.B;
            }
            return LED_Bytes;
        }
        static public byte[] Set_LEDBytes(Drawer drawer, Color color)
        {
            for (int i = 0; i < NumOfLED; i++)
            {
                drawer.LED_Bytes[i * 3 + 0] = color.R;
                drawer.LED_Bytes[i * 3 + 1] = color.G;
                drawer.LED_Bytes[i * 3 + 2] = color.B;
            }
            return drawer.LED_Bytes;
        }
        static public byte[] Set_LEDBytes(Drawer drawer, Box box, Color color)
        {
            return Set_LEDBytes(drawer, box.Column, box.Row, color);
        }
        static public byte[] Set_LEDBytes(Drawer drawer, int col, int row, Color color)
        {
            if (col >= drawer.Boxes.Count) return drawer.LED_Bytes;
            for (int i = 0; i < drawer.Boxes.Count; i++)
            {
                if (row >= drawer.Boxes[i].Length) return drawer.LED_Bytes;
            }
            Rectangle rect = Get_Box_Combine(drawer, col, row);
            int width = rect.Width / (Pannel_Width / Drawer_NumOf_H_Line);
            int height = rect.Height / (Pannel_Height / Drawer_NumOf_V_Line);
            return Set_LEDBytes(col, row, width, height, ref drawer.LED_Bytes, color);
        }
        static public byte[] Set_LEDBytes(int col_x, int row_y, int width, int height, ref byte[] LEDBytes, Color color)
        {
            for (int i = 0; i < width; i++)
            {
                Set_Drawer_H_Leds(col_x + i + row_y * Drawer_NumOf_H_Line, ref LEDBytes, color);
                Set_Drawer_H_Leds(col_x + i + (height + row_y) * Drawer_NumOf_H_Line, ref LEDBytes, color);

            }
            for (int k = 0; k < height; k++)
            {
                Set_Drawer_V_Leds(row_y + k + col_x * Drawer_NumOf_V_Line, ref LEDBytes, color);
                Set_Drawer_V_Leds(row_y + k + (width + col_x) * Drawer_NumOf_V_Line, ref LEDBytes, color);
            }
            return LEDBytes;
        }
        static public void Set_Drawer_H_Leds(int col, ref byte[] LEDBytes, Color color)
        {
            for (int i = 0; i < List_Drawer_H_Line_Leds[col].Length; i++)
            {
                LEDBytes[List_Drawer_H_Line_Leds[col][i] * 3 + 0] = color.R;
                LEDBytes[List_Drawer_H_Line_Leds[col][i] * 3 + 1] = color.G;
                LEDBytes[List_Drawer_H_Line_Leds[col][i] * 3 + 2] = color.B;
            }
        }
        static public void Set_Drawer_V_Leds(int row, ref byte[] LEDBytes, Color color)
        {
            for (int i = 0; i < List_Drawer_V_Line_Leds[row].Length; i++)
            {
                LEDBytes[List_Drawer_V_Line_Leds[row][i] * 3 + 0] = color.R;
                LEDBytes[List_Drawer_V_Line_Leds[row][i] * 3 + 1] = color.G;
                LEDBytes[List_Drawer_V_Line_Leds[row][i] * 3 + 2] = color.B;
            }
        }
        static public Rectangle Get_Box_Combine(Drawer drawer, int col, int row)
        {
            if (col > drawer.Boxes.Count) return new Rectangle();
            if (row > drawer.Boxes[col].Length) return new Rectangle();
            return Get_Box_Combine(drawer, drawer.Boxes[col][row]);
        }
        static public Rectangle Get_Box_Combine(Drawer drawer, Box box)
        {
            Box _box;
            if (box.Slave)
            {
                _box = drawer.Boxes[box.MasterBox_Column][box.MasterBox_Row];
            }
            else
            {
                _box = box;
            }
            int X0 = _box.X;
            int Y0 = _box.Y;
            int X1 = X0 + _box.Width;
            int Y1 = Y0 + _box.Height;
            int X_temp = 0;
            int Y_temp = 0;
            int SlaveBox_Column = _box.SlaveBox_Column;
            int SlaveBox_Row = _box.SlaveBox_Row;
            int SlaveBox_Column_temp = SlaveBox_Column;
            int SlaveBox_Row_temp = SlaveBox_Row;
            while (true)
            {
                if (SlaveBox_Column_temp == -1 && SlaveBox_Row_temp == -1) break;
                SlaveBox_Column = SlaveBox_Column_temp;
                SlaveBox_Row = SlaveBox_Row_temp;
                X_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].X + drawer.Boxes[SlaveBox_Column][SlaveBox_Row].Width;
                Y_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].Y + drawer.Boxes[SlaveBox_Column][SlaveBox_Row].Height;
                if (X_temp > X1) X1 = X_temp;
                if (Y_temp > Y1) Y1 = Y_temp;
                SlaveBox_Column_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].SlaveBox_Column;
                SlaveBox_Row_temp = drawer.Boxes[SlaveBox_Column][SlaveBox_Row].SlaveBox_Row;
            }
            Rectangle rectangle = new Rectangle(X0, Y0, X1 - X0, Y1 - Y0);
            return rectangle;


        }
        #region LED_Line

        private static List<int[]> List_Drawer_H_Line_Leds = new List<int[]>();
        private static List<int[]> List_Drawer_V_Line_Leds = new List<int[]>();
        private static int[] Line_H_00 = new int[] { 376 + 0, 376 + 1, 376 + 2, 376 + 3, 376 + 4, 376 + 5, 376 + 6, 376 + 7 };
        private static int[] Line_H_01 = new int[] { 384 + 0, 384 + 1, 384 + 2, 384 + 3, 384 + 4, 384 + 5, 384 + 6, 384 + 7 };
        private static int[] Line_H_02 = new int[] { 392 + 0, 392 + 1, 392 + 2, 392 + 3, 392 + 4, 392 + 5, 392 + 6, 392 + 7 };
        private static int[] Line_H_03 = new int[] { 400 + 0, 400 + 1, 400 + 2, 400 + 3, 400 + 4, 400 + 5, 400 + 6, 400 + 7 };

        private static int[] Line_H_04 = new int[] { 368 + 0, 368 + 1, 368 + 2, 368 + 3, 368 + 4, 368 + 5, 368 + 6, 368 + 7 };
        private static int[] Line_H_05 = new int[] { 360 + 0, 360 + 1, 360 + 2, 360 + 3, 360 + 4, 360 + 5, 360 + 6, 360 + 7 };
        private static int[] Line_H_06 = new int[] { 352 + 0, 352 + 1, 352 + 2, 352 + 3, 352 + 4, 352 + 5, 352 + 6, 352 + 7 };
        private static int[] Line_H_07 = new int[] { 344 + 0, 344 + 1, 344 + 2, 344 + 3, 344 + 4, 344 + 5, 344 + 6, 344 + 7 };

        private static int[] Line_H_08 = new int[] { 312 + 0, 312 + 1, 312 + 2, 312 + 3, 312 + 4, 312 + 5, 312 + 6, 312 + 7 };
        private static int[] Line_H_09 = new int[] { 320 + 0, 320 + 1, 320 + 2, 320 + 3, 320 + 4, 320 + 5, 320 + 6, 320 + 7 };
        private static int[] Line_H_10 = new int[] { 328 + 0, 328 + 1, 328 + 2, 328 + 3, 328 + 4, 328 + 5, 328 + 6, 328 + 7 };
        private static int[] Line_H_11 = new int[] { 336 + 0, 336 + 1, 336 + 2, 336 + 3, 336 + 4, 336 + 5, 336 + 6, 336 + 7 };

        private static int[] Line_H_12 = new int[] { 304 + 0, 304 + 1, 304 + 2, 304 + 3, 304 + 4, 304 + 5, 304 + 6, 304 + 7 };
        private static int[] Line_H_13 = new int[] { 296 + 0, 296 + 1, 296 + 2, 296 + 3, 296 + 4, 296 + 5, 296 + 6, 296 + 7 };
        private static int[] Line_H_14 = new int[] { 288 + 0, 288 + 1, 288 + 2, 288 + 3, 288 + 4, 288 + 5, 288 + 6, 288 + 7 };
        private static int[] Line_H_15 = new int[] { 280 + 0, 280 + 1, 280 + 2, 280 + 3, 280 + 4, 280 + 5, 280 + 6, 280 + 7 };

        private static int[] Line_H_16 = new int[] { 248 + 0, 248 + 1, 248 + 2, 248 + 3, 248 + 4, 248 + 5, 248 + 6, 248 + 7 };
        private static int[] Line_H_17 = new int[] { 256 + 0, 256 + 1, 256 + 2, 256 + 3, 256 + 4, 256 + 5, 256 + 6, 256 + 7 };
        private static int[] Line_H_18 = new int[] { 264 + 0, 264 + 1, 264 + 2, 264 + 3, 264 + 4, 264 + 5, 264 + 6, 264 + 7 };
        private static int[] Line_H_19 = new int[] { 272 + 0, 272 + 1, 272 + 2, 272 + 3, 272 + 4, 272 + 5, 272 + 6, 272 + 7 };

        private static int[] Line_H_20 = new int[] { 240 + 0, 240 + 1, 240 + 2, 240 + 3, 240 + 4, 240 + 5, 240 + 6, 240 + 7 };
        private static int[] Line_H_21 = new int[] { 232 + 0, 232 + 1, 232 + 2, 232 + 3, 232 + 4, 232 + 5, 232 + 6, 232 + 7 };
        private static int[] Line_H_22 = new int[] { 224 + 0, 224 + 1, 224 + 2, 224 + 3, 224 + 4, 224 + 5, 224 + 6, 224 + 7 };
        private static int[] Line_H_23 = new int[] { 216 + 0, 216 + 1, 216 + 2, 216 + 3, 216 + 4, 216 + 5, 216 + 6, 216 + 7 };

        private static int[] Line_H_24 = new int[] { 184 + 0, 184 + 1, 184 + 2, 184 + 3, 184 + 4, 184 + 5, 184 + 6, 184 + 7 };
        private static int[] Line_H_25 = new int[] { 192 + 0, 192 + 1, 192 + 2, 192 + 3, 192 + 4, 192 + 5, 192 + 6, 192 + 7 };
        private static int[] Line_H_26 = new int[] { 200 + 0, 200 + 1, 200 + 2, 200 + 3, 200 + 4, 200 + 5, 200 + 6, 200 + 7 };
        private static int[] Line_H_27 = new int[] { 208 + 0, 208 + 1, 208 + 2, 208 + 3, 208 + 4, 208 + 5, 208 + 6, 208 + 7 };

        private static int[] Line_H_28 = new int[] { 176 + 0, 176 + 1, 176 + 2, 176 + 3, 176 + 4, 176 + 5, 176 + 6, 176 + 7 };
        private static int[] Line_H_29 = new int[] { 168 + 0, 168 + 1, 168 + 2, 168 + 3, 168 + 4, 168 + 5, 168 + 6, 168 + 7 };
        private static int[] Line_H_30 = new int[] { 160 + 0, 160 + 1, 160 + 2, 160 + 3, 160 + 4, 160 + 5, 160 + 6, 160 + 7 };
        private static int[] Line_H_31 = new int[] { 152 + 0, 152 + 1, 152 + 2, 152 + 3, 152 + 4, 152 + 5, 152 + 6, 152 + 7 };

        private static int[] Line_H_32 = new int[] { 120 + 0, 120 + 1, 120 + 2, 120 + 3, 120 + 4, 120 + 5, 120 + 6, 120 + 7 };
        private static int[] Line_H_33 = new int[] { 128 + 0, 128 + 1, 128 + 2, 128 + 3, 128 + 4, 128 + 5, 128 + 6, 128 + 7 };
        private static int[] Line_H_34 = new int[] { 136 + 0, 136 + 1, 136 + 2, 136 + 3, 136 + 4, 136 + 5, 136 + 6, 136 + 7 };
        private static int[] Line_H_35 = new int[] { 144 + 0, 144 + 1, 144 + 2, 144 + 3, 144 + 4, 144 + 5, 144 + 6, 144 + 7 };


        private static int[] Line_V_00 = new int[] { 96 + (0 * 3) + 0, 96 + (0 * 3) + 1, 96 + (0 * 3) + 2 };
        private static int[] Line_V_01 = new int[] { 96 + (1 * 3) + 0, 96 + (1 * 3) + 1, 96 + (1 * 3) + 2 };
        private static int[] Line_V_02 = new int[] { 96 + (2 * 3) + 0, 96 + (2 * 3) + 1, 96 + (2 * 3) + 2 };
        private static int[] Line_V_03 = new int[] { 96 + (3 * 3) + 0, 96 + (3 * 3) + 1, 96 + (3 * 3) + 2 };
        private static int[] Line_V_04 = new int[] { 96 + (4 * 3) + 0, 96 + (4 * 3) + 1, 96 + (4 * 3) + 2 };
        private static int[] Line_V_05 = new int[] { 96 + (5 * 3) + 0, 96 + (5 * 3) + 1, 96 + (5 * 3) + 2 };
        private static int[] Line_V_06 = new int[] { 96 + (6 * 3) + 0, 96 + (6 * 3) + 1, 96 + (6 * 3) + 2 };
        private static int[] Line_V_07 = new int[] { 96 + (7 * 3) + 0, 96 + (7 * 3) + 1, 96 + (7 * 3) + 2 };

        private static int[] Line_V_08 = new int[] { 95 - (0 * 3) - 0, 95 - (0 * 3) - 1, 95 - (0 * 3) - 2 };
        private static int[] Line_V_09 = new int[] { 95 - (1 * 3) - 0, 95 - (1 * 3) - 1, 95 - (1 * 3) - 2 };
        private static int[] Line_V_10 = new int[] { 95 - (2 * 3) - 0, 95 - (2 * 3) - 1, 95 - (2 * 3) - 2 };
        private static int[] Line_V_11 = new int[] { 95 - (3 * 3) - 0, 95 - (3 * 3) - 1, 95 - (3 * 3) - 2 };
        private static int[] Line_V_12 = new int[] { 95 - (4 * 3) - 0, 95 - (4 * 3) - 1, 95 - (4 * 3) - 2 };
        private static int[] Line_V_13 = new int[] { 95 - (5 * 3) - 0, 95 - (5 * 3) - 1, 95 - (5 * 3) - 2 };
        private static int[] Line_V_14 = new int[] { 95 - (6 * 3) - 0, 95 - (6 * 3) - 1, 95 - (6 * 3) - 2 };
        private static int[] Line_V_15 = new int[] { 95 - (7 * 3) - 0, 95 - (7 * 3) - 1, 95 - (7 * 3) - 2 };

        private static int[] Line_V_16 = new int[] { 48 + (0 * 3) + 0, 48 + (0 * 3) + 1, 48 + (0 * 3) + 2 };
        private static int[] Line_V_17 = new int[] { 48 + (1 * 3) + 0, 48 + (1 * 3) + 1, 48 + (1 * 3) + 2 };
        private static int[] Line_V_18 = new int[] { 48 + (2 * 3) + 0, 48 + (2 * 3) + 1, 48 + (2 * 3) + 2 };
        private static int[] Line_V_19 = new int[] { 48 + (3 * 3) + 0, 48 + (3 * 3) + 1, 48 + (3 * 3) + 2 };
        private static int[] Line_V_20 = new int[] { 48 + (4 * 3) + 0, 48 + (4 * 3) + 1, 48 + (4 * 3) + 2 };
        private static int[] Line_V_21 = new int[] { 48 + (5 * 3) + 0, 48 + (5 * 3) + 1, 48 + (5 * 3) + 2 };
        private static int[] Line_V_22 = new int[] { 48 + (6 * 3) + 0, 48 + (6 * 3) + 1, 48 + (6 * 3) + 2 };
        private static int[] Line_V_23 = new int[] { 48 + (7 * 3) + 0, 48 + (7 * 3) + 1, 48 + (7 * 3) + 2 };

        private static int[] Line_V_24 = new int[] { 47 - (0 * 3) - 0, 47 - (0 * 3) - 1, 47 - (0 * 3) - 2 };
        private static int[] Line_V_25 = new int[] { 47 - (1 * 3) - 0, 47 - (1 * 3) - 1, 47 - (1 * 3) - 2 };
        private static int[] Line_V_26 = new int[] { 47 - (2 * 3) - 0, 47 - (2 * 3) - 1, 47 - (2 * 3) - 2 };
        private static int[] Line_V_27 = new int[] { 47 - (3 * 3) - 0, 47 - (3 * 3) - 1, 47 - (3 * 3) - 2 };
        private static int[] Line_V_28 = new int[] { 47 - (4 * 3) - 0, 47 - (4 * 3) - 1, 47 - (4 * 3) - 2 };
        private static int[] Line_V_29 = new int[] { 47 - (5 * 3) - 0, 47 - (5 * 3) - 1, 47 - (5 * 3) - 2 };
        private static int[] Line_V_30 = new int[] { 47 - (6 * 3) - 0, 47 - (6 * 3) - 1, 47 - (6 * 3) - 2 };
        private static int[] Line_V_31 = new int[] { 47 - (7 * 3) - 0, 47 - (7 * 3) - 1, 47 - (7 * 3) - 2 };

        private static int[] Line_V_32 = new int[] { 00 + (0 * 3) + 0, 00 + (0 * 3) + 1, 00 + (0 * 3) + 2 };
        private static int[] Line_V_33 = new int[] { 00 + (1 * 3) + 0, 00 + (1 * 3) + 1, 00 + (1 * 3) + 2 };
        private static int[] Line_V_34 = new int[] { 00 + (2 * 3) + 0, 00 + (2 * 3) + 1, 00 + (2 * 3) + 2 };
        private static int[] Line_V_35 = new int[] { 00 + (3 * 3) + 0, 00 + (3 * 3) + 1, 00 + (3 * 3) + 2 };
        private static int[] Line_V_36 = new int[] { 00 + (4 * 3) + 0, 00 + (4 * 3) + 1, 00 + (4 * 3) + 2 };
        private static int[] Line_V_37 = new int[] { 00 + (5 * 3) + 0, 00 + (5 * 3) + 1, 00 + (5 * 3) + 2 };
        private static int[] Line_V_38 = new int[] { 00 + (6 * 3) + 0, 00 + (6 * 3) + 1, 00 + (6 * 3) + 2 };
        private static int[] Line_V_39 = new int[] { 00 + (7 * 3) + 0, 00 + (7 * 3) + 1, 00 + (7 * 3) + 2 };
        static private void WS2812_Init()
        {
            List_Drawer_H_Line_Leds.Clear();
            List_Drawer_V_Line_Leds.Clear();

            List_Drawer_H_Line_Leds.Add(Line_H_00);
            List_Drawer_H_Line_Leds.Add(Line_H_01);
            List_Drawer_H_Line_Leds.Add(Line_H_02);
            List_Drawer_H_Line_Leds.Add(Line_H_03);

            List_Drawer_H_Line_Leds.Add(Line_H_04);
            List_Drawer_H_Line_Leds.Add(Line_H_05);
            List_Drawer_H_Line_Leds.Add(Line_H_06);
            List_Drawer_H_Line_Leds.Add(Line_H_07);

            List_Drawer_H_Line_Leds.Add(Line_H_08);
            List_Drawer_H_Line_Leds.Add(Line_H_09);
            List_Drawer_H_Line_Leds.Add(Line_H_10);
            List_Drawer_H_Line_Leds.Add(Line_H_11);

            List_Drawer_H_Line_Leds.Add(Line_H_12);
            List_Drawer_H_Line_Leds.Add(Line_H_13);
            List_Drawer_H_Line_Leds.Add(Line_H_14);
            List_Drawer_H_Line_Leds.Add(Line_H_15);

            List_Drawer_H_Line_Leds.Add(Line_H_16);
            List_Drawer_H_Line_Leds.Add(Line_H_17);
            List_Drawer_H_Line_Leds.Add(Line_H_18);
            List_Drawer_H_Line_Leds.Add(Line_H_19);

            List_Drawer_H_Line_Leds.Add(Line_H_20);
            List_Drawer_H_Line_Leds.Add(Line_H_21);
            List_Drawer_H_Line_Leds.Add(Line_H_22);
            List_Drawer_H_Line_Leds.Add(Line_H_23);

            List_Drawer_H_Line_Leds.Add(Line_H_24);
            List_Drawer_H_Line_Leds.Add(Line_H_25);
            List_Drawer_H_Line_Leds.Add(Line_H_26);
            List_Drawer_H_Line_Leds.Add(Line_H_27);

            List_Drawer_H_Line_Leds.Add(Line_H_28);
            List_Drawer_H_Line_Leds.Add(Line_H_29);
            List_Drawer_H_Line_Leds.Add(Line_H_30);
            List_Drawer_H_Line_Leds.Add(Line_H_31);

            List_Drawer_H_Line_Leds.Add(Line_H_32);
            List_Drawer_H_Line_Leds.Add(Line_H_33);
            List_Drawer_H_Line_Leds.Add(Line_H_34);
            List_Drawer_H_Line_Leds.Add(Line_H_35);

            List_Drawer_V_Line_Leds.Add(Line_V_00);
            List_Drawer_V_Line_Leds.Add(Line_V_01);
            List_Drawer_V_Line_Leds.Add(Line_V_02);
            List_Drawer_V_Line_Leds.Add(Line_V_03);
            List_Drawer_V_Line_Leds.Add(Line_V_04);

            List_Drawer_V_Line_Leds.Add(Line_V_05);
            List_Drawer_V_Line_Leds.Add(Line_V_06);
            List_Drawer_V_Line_Leds.Add(Line_V_07);
            List_Drawer_V_Line_Leds.Add(Line_V_08);
            List_Drawer_V_Line_Leds.Add(Line_V_09);

            List_Drawer_V_Line_Leds.Add(Line_V_10);
            List_Drawer_V_Line_Leds.Add(Line_V_11);
            List_Drawer_V_Line_Leds.Add(Line_V_12);
            List_Drawer_V_Line_Leds.Add(Line_V_13);
            List_Drawer_V_Line_Leds.Add(Line_V_14);

            List_Drawer_V_Line_Leds.Add(Line_V_15);
            List_Drawer_V_Line_Leds.Add(Line_V_16);
            List_Drawer_V_Line_Leds.Add(Line_V_17);
            List_Drawer_V_Line_Leds.Add(Line_V_18);
            List_Drawer_V_Line_Leds.Add(Line_V_19);

            List_Drawer_V_Line_Leds.Add(Line_V_20);
            List_Drawer_V_Line_Leds.Add(Line_V_21);
            List_Drawer_V_Line_Leds.Add(Line_V_22);
            List_Drawer_V_Line_Leds.Add(Line_V_23);
            List_Drawer_V_Line_Leds.Add(Line_V_24);

            List_Drawer_V_Line_Leds.Add(Line_V_25);
            List_Drawer_V_Line_Leds.Add(Line_V_26);
            List_Drawer_V_Line_Leds.Add(Line_V_27);
            List_Drawer_V_Line_Leds.Add(Line_V_28);
            List_Drawer_V_Line_Leds.Add(Line_V_29);

            List_Drawer_V_Line_Leds.Add(Line_V_30);
            List_Drawer_V_Line_Leds.Add(Line_V_31);
            List_Drawer_V_Line_Leds.Add(Line_V_32);
            List_Drawer_V_Line_Leds.Add(Line_V_33);
            List_Drawer_V_Line_Leds.Add(Line_V_34);

            List_Drawer_V_Line_Leds.Add(Line_V_35);
            List_Drawer_V_Line_Leds.Add(Line_V_36);
            List_Drawer_V_Line_Leds.Add(Line_V_37);
            List_Drawer_V_Line_Leds.Add(Line_V_38);
            List_Drawer_V_Line_Leds.Add(Line_V_39);
        }
        #endregion
        #endregion
    }
}
