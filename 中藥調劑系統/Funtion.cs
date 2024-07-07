using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;
using H_Pannel_lib;


namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        public class LightOn
        {
            public string IP { get; set; }
            public string Code { get; set; }
            public Color color { get; set; }

            public LightOn(string IP, Color color)
            {
                this.IP = IP;
                this.color = color;
            }
        }

        static public List<RowsLED> List_RowsLED_本地資料 = new List<RowsLED>();
        static public List<Storage> List_EPD266_本地資料 = new List<Storage>();
        static public List<object> Function_從本地資料取得儲位(string 藥品碼)
        {
            藥品碼 = ReplaceHyphenWithStar(藥品碼);
            List<object> list_value = new List<object>();
            List<RowsDevice> rowsDevices = List_RowsLED_本地資料.SortByCode(藥品碼);
            List<Storage> storages_epd266 = List_EPD266_本地資料.SortByCode(藥品碼);
            for (int i = 0; i < rowsDevices.Count; i++)
            {
                list_value.Add(rowsDevices[i]);
            }
            for (int i = 0; i < storages_epd266.Count; i++)
            {
                list_value.Add(storages_epd266[i]);
            }
            return list_value;
        }
        static public void Function_從SQL取得儲位到本地資料()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            Console.WriteLine($"開始SQL讀取儲位資料到本地!");
            List<Task> taskList = new List<Task>();
                  
            taskList.Add(Task.Run(() =>
            {
                MyTimer myTimer2 = new MyTimer();
                myTimer2.StartTickTime(50000);
                List_RowsLED_本地資料 = _rowsLEDUI.SQL_GetAllRowsLED();
                Console.WriteLine($"讀取RowsLED資料! 耗時 :{myTimer2.GetTickTime().ToString("0.000")} ");
            }));
            taskList.Add(Task.Run(() =>
            {
                MyTimer myTimer2 = new MyTimer();
                myTimer2.StartTickTime(50000);
                List_EPD266_本地資料 = _storageUI_EPD_266.SQL_GetAllStorage();
                Console.WriteLine($"讀取EPD266資料! 耗時 :{myTimer2.GetTickTime().ToString("0.000")} ");
            }));
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();


            Console.WriteLine($"SQL讀取儲位資料到本地結束! 耗時 : {myTimer.GetTickTime().ToString("0.000")}");
        }
        static public void Function_儲位亮燈(List<string> Codes, Color color)
        {
            List<LightOn> lightOns = new List<LightOn>();
            List<LightOn> LightOns_buf = new List<LightOn>();
            for (int i = 0; i < Codes.Count; i++)
            {
                Console.WriteLine($"儲位亮燈 : 藥碼 : {Codes[i]} 顏色 : {color.ToColorString()}");
                if (Codes[i].StringIsEmpty()) return;
                Codes[i] = ReplaceHyphenWithStar(Codes[i]);
                List<object> list_Device = Function_從本地資料取得儲位(Codes[i]);
                for (int k = 0; k < list_Device.Count; k++)
                {
                    Device device = list_Device[k] as Device;
                    if (device != null)
                    {
                        if (device.DeviceType == DeviceType.RowsLED)
                        {
                            RowsDevice rowsDevice = list_Device[k] as RowsDevice;
                            RowsLED rowsLED = List_RowsLED_本地資料.SortByIP(device.IP);
                            if (rowsDevice != null && rowsLED != null)
                            {
                                rowsLED.LED_Bytes = RowsLEDUI.Get_Rows_LEDBytes(ref rowsLED.LED_Bytes, rowsDevice, color);
                                LightOns_buf = (from temp in lightOns
                                                where temp.IP == device.IP
                                                select temp).ToList();
                                if (LightOns_buf.Count == 0)
                                {
                                    lightOns.Add(new LightOn(device.IP, color));
                                }
                            }
                        }
                        if (device.DeviceType == DeviceType.EPD290 || device.DeviceType == DeviceType.EPD266|| device.DeviceType == DeviceType.EPD290_lock || device.DeviceType == DeviceType.EPD266_lock)
                        {
                            Storage storage = List_EPD266_本地資料.SortByIP(device.IP);
                            if (storage != null)
                            {
                                if (LightOns_buf.Count == 0)
                                {
                                    lightOns.Add(new LightOn(device.IP, color));
                                }
                                //_storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);
                            }
                        }
                    }
                }
            }
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < lightOns.Count; i++)
            {
                LightOn lightOn = lightOns[i];

                tasks.Add(Task.Run(new Action(delegate 
                {
                    RowsLED rowsLED = List_RowsLED_本地資料.SortByIP(lightOn.IP);
                    Storage storage = List_EPD266_本地資料.SortByIP(lightOn.IP);
                    if (rowsLED != null)
                    {
                        _rowsLEDUI.Set_Rows_LED_UDP(rowsLED);
                    }
                    if (storage != null)
                    {
                        _storageUI_EPD_266.Set_Stroage_LED_UDP(storage, lightOn.color);
                    }
                })));
            }

            Task.WhenAll(tasks).Wait();
        }
        static public void Function_儲位亮燈(string 藥品碼, Color color)
        {
            if (藥品碼.StringIsEmpty()) return;
            藥品碼 = ReplaceHyphenWithStar(藥品碼);
            Console.WriteLine($"儲位亮燈 : 藥碼 : {藥品碼} 顏色 : {color.ToColorString()}");
            List<object> list_Device = Function_從本地資料取得儲位(藥品碼);
            for (int i = 0; i < list_Device.Count; i++)
            {
                Device device = list_Device[i] as Device;
                if (device != null)
                {
                    if (device.DeviceType == DeviceType.RowsLED)
                    {
                        RowsDevice rowsDevice = list_Device[i] as RowsDevice;
                        RowsLED rowsLED = List_RowsLED_本地資料.SortByIP(rowsDevice.IP);
                        if (rowsDevice != null && rowsLED != null)
                        {
                            rowsLED.LED_Bytes = RowsLEDUI.Get_Rows_LEDBytes(ref rowsLED.LED_Bytes, rowsDevice, color);

                            _rowsLEDUI.Set_Rows_LED_UDP(rowsLED);
                        }
                    }
                    if (device.DeviceType == DeviceType.EPD290 || device.DeviceType == DeviceType.EPD266)
                    {
                        Storage storage = List_EPD266_本地資料.SortByIP(device.IP);
                        if (storage != null )
                        {
                            _storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);
                        }
                    }
                }
            }
        }
        static public void Function_全部滅燈()
        {
            List<RowsLED> rowsLEDs = _rowsLEDUI.SQL_GetAllRowsLED();
            List<Storage> storages = _storageUI_EPD_266.SQL_GetAllStorage();


            List<Task> tasks = new List<Task>();

            for (int i = 0; i < rowsLEDs.Count; i++)
            {
                RowsLED rowsLED = rowsLEDs[i];
                tasks.Add(Task.Run(new Action(delegate
                {
                    _rowsLEDUI.Set_Rows_LED_Clear_UDP(rowsLED);                   
                })));
            }
            for (int i = 0; i < storages.Count; i++)
            {
                Storage storage = storages[i];
                tasks.Add(Task.Run(new Action(delegate
                {
                    _storageUI_EPD_266.Set_Stroage_LED_UDP(storage, Color.Black);
                })));
            }
            Task.WhenAll(tasks).Wait();
        }
        static public List<OrderClass> Funtion_醫令資料_API呼叫(string barcode)
        {
            barcode = barcode.Replace("\r\n", "");
            barcode = Uri.EscapeDataString(barcode);
            List<OrderClass> orderClasses = new List<OrderClass>();
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string apitext = $"{dBConfigClass.OrderApiURL}{barcode}";
            string jsonString = Basic.Net.WEBApiGet(apitext);

            if (jsonString.StringIsEmpty())
            {
                Dialog_AlarmForm dialog_錯誤提示 = new Dialog_AlarmForm($"呼叫串接資料失敗!請檢查網路連線", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog($"呼叫串接資料失敗!請檢查網路連線...");
                return orderClasses;
            }

            returnData returnData = jsonString.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                Dialog_AlarmForm dialog_錯誤提示 = new Dialog_AlarmForm($"藥單條碼錯誤:{jsonString}", 2000);
                dialog_錯誤提示.ShowDialog();
                //MyMessageBox.ShowDialog(jsonString);
                return new List<OrderClass>();
            }
            if (returnData.Code != 200)
            {
                //Dialog_AlarmForm dialog_錯誤提示 = new Dialog_AlarmForm($"{returnData.Result}", 2000);
                //dialog_錯誤提示.ShowDialog();
                return null;

            }
            orderClasses = returnData.Data.ObjToListClass<OrderClass>();
            if (orderClasses == null)
            {
                Console.WriteLine($"串接資料傳回格式錯誤!");
                orderClasses = new List<OrderClass>();

            }

            return orderClasses;
        }
        public static string ReplaceHyphenWithStar(string input)
        {
            string pattern = "-[a-zA-Z0-9]";
            string replacement = "";
            input = System.Text.RegularExpressions.Regex.Replace(input, pattern, replacement);
            input = $"{input}*";
            return input;
        }
    }
}
