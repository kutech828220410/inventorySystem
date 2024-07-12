using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using SQLUI;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using HIS_DB_Lib;
namespace 智能藥庫系統
{
    public partial class Main_Form : Form
    {
        static public List<Storage> List_EPD266_雲端資料 = new List<Storage>();
        static public List<Drawer> List_EPD583_雲端資料 = new List<Drawer>();
        public enum enum_儲位資訊
        {
            GUID,
            IP,
            TYPE,
            效期,
            批號,
            庫存,
            異動量,
            Value,
        }

        static public void Function_儲位亮燈(string 藥品碼, Color color)
        {
            List<string> list_lock_IP = new List<string>();
            Function_儲位亮燈(藥品碼, color, ref list_lock_IP);
        }
        static public void Function_儲位亮燈(string 藥品碼, Color color, ref List<string> list_lock_IP)
        {
            List<object> list_Device = Function_從雲端資料取得儲位(藥品碼);
            List<Task> taskList = new List<Task>();
            List<string> list_IP = new List<string>();
            List<string> list_IP_buf = new List<string>();
            for (int i = 0; i < list_Device.Count; i++)
            {
                Device device = list_Device[i] as Device;
                string IP = device.IP;

                list_IP_buf = (from value in list_IP
                               where value == IP
                               select value).ToList();
                if (list_IP_buf.Count > 0) continue;
                Console.WriteLine($"{DateTime.Now.ToDateTimeString()} - (儲位亮燈) {IP} {device.DeviceType.GetEnumName()} Color:{color.ToColorString()}");
                if (device != null)
                {
                    if (device.DeviceType == DeviceType.EPD266 || device.DeviceType == DeviceType.EPD266_lock)
                    {
                        Storage storage = list_Device[i] as Storage;
                        if (storage != null)
                        {
                            taskList.Add(Task.Run(() =>
                            {
                                _storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);
                            }));
                            list_IP.Add(IP);
                            if (device.DeviceType == DeviceType.EPD266_lock) list_lock_IP.Add(IP);
                        }
                    }
                    else if (device.DeviceType == DeviceType.EPD290 || device.DeviceType == DeviceType.EPD290_lock)
                    {
                        Storage storage = list_Device[i] as Storage;
                        if (storage != null)
                        {
                            taskList.Add(Task.Run(() =>
                            {
                                _storageUI_EPD_266.Set_Stroage_LED_UDP(storage, color);
                            }));
                            list_IP.Add(IP);
                            if (device.DeviceType == DeviceType.EPD266_lock) list_lock_IP.Add(IP);
                        }
                    }
                    else if (device.DeviceType == DeviceType.EPD730 || device.DeviceType == DeviceType.EPD730_lock)
                    {
                        Box box = list_Device[i] as Box;
                        if (box != null)
                        {
                            taskList.Add(Task.Run(() =>
                            {
                                Drawer drawer = _drawerUI_EPD_583.SQL_GetDrawer(box);
                                if (drawer != null)
                                {
                                    _drawerUI_EPD_583.Set_Drawer_LED_UDP(drawer, box, color);
                                }
                              
                            }));
                            list_IP.Add(IP);
                        }
                    }

                }
            }




            //Task allTask = Task.WhenAll(taskList);
            //allTask.Wait();
        }
        static public List<object> Function_從雲端資料取得儲位(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            List<Box> boxes = List_EPD583_雲端資料.SortByCode(藥品碼);
            List<Storage> storages = List_EPD266_雲端資料.SortByCode(藥品碼);
            for (int i = 0; i < boxes.Count; i++)
            {
                list_value.Add(boxes[i]);
            }
         
            for (int i = 0; i < storages.Count; i++)
            {
                list_value.Add(storages[i]);
            }
          
            return list_value;
        }
        static public void Function_從SQL取得儲位到雲端資料()
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                Console.WriteLine($"開始SQL讀取儲位資料到雲端!");
                List<Task> taskList = new List<Task>();

                taskList.Add(Task.Run(() =>
                {
                    MyTimer myTimer1 = new MyTimer();
                    myTimer1.StartTickTime(50000);
                    List_EPD266_雲端資料 = _storageUI_EPD_266.SQL_GetAllStorage();
                    Console.WriteLine($"讀取EPD266資料! 耗時 :{myTimer1.GetTickTime().ToString("0.000")} ");

                }));
                taskList.Add(Task.Run(() =>
                {
                    MyTimer myTimer1 = new MyTimer();
                    myTimer1.StartTickTime(50000);
                    List_EPD583_雲端資料 = _drawerUI_EPD_583.SQL_GetAllDrawers();
                    Console.WriteLine($"讀取EPD583資料! 耗時 :{myTimer1.GetTickTime().ToString("0.000")} ");

                }));

                
                Task allTask = Task.WhenAll(taskList);
                allTask.Wait();
                Console.WriteLine($"SQL讀取儲位資料到雲端結束! 耗時 : {myTimer.GetTickTime().ToString("0.000")}");
            }
            catch
            {

            }

        }
        static public List<object> Function_從SQL取得儲位到雲端資料(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            List<Storage> storages = List_EPD266_雲端資料.SortByCode(藥品碼);
            List<Box> boxes = List_EPD583_雲端資料.SortByCode(藥品碼);

            for (int i = 0; i < storages.Count; i++)
            {
                Storage storage = _storageUI_EPD_266.SQL_GetStorage(storages[i]);
                List_EPD266_雲端資料.Add_NewStorage(storage);
                list_value.Add(storage);
            }
            for (int i = 0; i < boxes.Count; i++)
            {
                Box box = _drawerUI_EPD_583.SQL_GetBox(boxes[i]);
                List_EPD583_雲端資料.Add_NewDrawer(box);
                list_value.Add(box);
            }
            return list_value;
        }
        static public void Function_從雲端資料取得儲位(string 藥品碼, ref List<string> TYPE, ref List<object> values)
        {
            List<object> list_value = Function_從雲端資料取得儲位(藥品碼);
            TYPE.Clear();
            values.Clear();
            for (int i = 0; i < list_value.Count; i++)
            {
                if (list_value[i] is Device)
                {
                    Device device = (Device)list_value[i];
                    values.Add(list_value[i]);
                    TYPE.Add(device.DeviceType.GetEnumName());
                }

            }
        }
        static public List<object[]> Function_取得異動儲位資訊從雲端資料(string 藥品碼, int 異動量, string 效期, string IP)
        {
            List<object> 儲位 = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            Function_從雲端資料取得儲位(Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref 儲位);
            List<object[]> 儲位資訊_buf = new List<object[]>();
            List<object[]> 儲位資訊 = new List<object[]>();
            for (int k = 0; k < 儲位.Count; k++)
            {
                object value_device = 儲位[k];
                object[] value = new object[new enum_儲位資訊().GetLength()];
                if (value_device is Device)
                {
                    Device device = (Device)value_device;
                    for (int i = 0; i < device.List_Validity_period.Count; i++)
                    {
                        if (device.List_Validity_period[i] == 效期 && device.IP == IP)
                        {
                            value[(int)enum_儲位資訊.IP] = device.IP;
                            value[(int)enum_儲位資訊.TYPE] = 儲位_TYPE[k];
                            value[(int)enum_儲位資訊.效期] = device.List_Validity_period[i];
                            value[(int)enum_儲位資訊.批號] = device.List_Lot_number[i];
                            value[(int)enum_儲位資訊.庫存] = device.List_Inventory[i];
                            value[(int)enum_儲位資訊.異動量] = 異動量.ToString();
                            value[(int)enum_儲位資訊.Value] = value_device;
                            儲位資訊.Add(value);
                            break;
                        }
                    }
                }
            }
            return 儲位資訊;
        }
        static public List<object[]> Function_取得異動儲位資訊從雲端資料(string 藥品碼, int 異動量, string 效期)
        {
            List<object> 儲位 = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            Function_從雲端資料取得儲位(Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref 儲位);
            List<object[]> 儲位資訊_buf = new List<object[]>();
            List<object[]> 儲位資訊 = new List<object[]>();
            for (int k = 0; k < 儲位.Count; k++)
            {
                object value_device = 儲位[k];
                object[] value = new object[new enum_儲位資訊().GetLength()];
                if (value_device is Device)
                {
                    Device device = (Device)value_device;
                    for (int i = 0; i < device.List_Validity_period.Count; i++)
                    {
                        if (device.List_Validity_period[i] == 效期)
                        {
                            value[(int)enum_儲位資訊.IP] = device.IP;
                            value[(int)enum_儲位資訊.TYPE] = 儲位_TYPE[k];
                            value[(int)enum_儲位資訊.效期] = device.List_Validity_period[i];
                            value[(int)enum_儲位資訊.批號] = device.List_Lot_number[i];
                            value[(int)enum_儲位資訊.庫存] = device.List_Inventory[i];
                            value[(int)enum_儲位資訊.異動量] = 異動量.ToString();
                            value[(int)enum_儲位資訊.Value] = value_device;
                            儲位資訊.Add(value);
                            break;
                        }
                    }
                }
            }
            return 儲位資訊;
        }
        static public List<object[]> Function_取得異動儲位資訊從雲端資料(string 藥品碼, int 異動量)
        {
            List<object> 儲位 = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            Function_從雲端資料取得儲位(Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref 儲位);
            List<object[]> 儲位資訊_buf = new List<object[]>();
            List<object[]> 儲位資訊 = new List<object[]>();
            if (儲位.Count == 0) return 儲位資訊_buf;


            for (int k = 0; k < 儲位.Count; k++)
            {
                object value_device = 儲位[k];
                if (value_device is Device)
                {
                    Device device = (Device)value_device;
                    for (int i = 0; i < device.List_Validity_period.Count; i++)
                    {
                        object[] value = new object[new enum_儲位資訊().GetLength()];
                        value[(int)enum_儲位資訊.IP] = device.IP;
                        value[(int)enum_儲位資訊.TYPE] = 儲位_TYPE[k];
                        value[(int)enum_儲位資訊.效期] = device.List_Validity_period[i];
                        value[(int)enum_儲位資訊.批號] = device.List_Lot_number[i];
                        value[(int)enum_儲位資訊.庫存] = device.List_Inventory[i];
                        value[(int)enum_儲位資訊.異動量] = "0";
                        value[(int)enum_儲位資訊.Value] = value_device;
                        儲位資訊.Add(value);
                    }
                }
            }
            儲位資訊 = 儲位資訊.OrderBy(r => DateTime.Parse(r[(int)enum_儲位資訊.效期].ToDateString())).ToList();

            if (異動量 == 0) return 儲位資訊;
            int 使用數量 = 異動量;
            int 庫存數量 = 0;
            int 剩餘庫存數量 = 0;
            for (int i = 0; i < 儲位資訊.Count; i++)
            {
                庫存數量 = 儲位資訊[i][(int)enum_儲位資訊.庫存].ObjectToString().StringToInt32();
                if ((使用數量 < 0 && 庫存數量 > 0) || (使用數量 > 0 && 庫存數量 >= 0))
                {
                    剩餘庫存數量 = 庫存數量 + 使用數量;
                    if (剩餘庫存數量 >= 0)
                    {
                        儲位資訊[i][(int)enum_儲位資訊.異動量] = (使用數量).ToString();
                        儲位資訊_buf.Add(儲位資訊[i]);
                        break;
                    }
                    else
                    {
                        儲位資訊[i][(int)enum_儲位資訊.異動量] = (庫存數量 * -1).ToString();
                        使用數量 = 剩餘庫存數量;
                        儲位資訊_buf.Add(儲位資訊[i]);
                    }
                }
            }

            return 儲位資訊_buf;
        }
        static public void Function_庫存異動至雲端資料(object[] 儲位資訊)
        {
            object Value = 儲位資訊[(int)enum_儲位資訊.Value];
            string 效期 = 儲位資訊[(int)enum_儲位資訊.效期].ObjectToString();
            string 異動量 = 儲位資訊[(int)enum_儲位資訊.異動量].ObjectToString();
            if (Value is Storage)
            {
                Storage storage = (Storage)Value;
                storage = List_EPD266_雲端資料.SortByIP(storage.IP);
                if (storage != null)
                {
                    storage.效期庫存異動(效期, 異動量, false);
                    List_EPD266_雲端資料.Add_NewStorage(storage);
                    return;
                }
            }


        }
        static public int Function_從雲端資料取得庫存(string 藥品碼)
        {
            int 庫存 = 0;
            List<object> list_value = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            Function_從雲端資料取得儲位(Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref list_value);

            for (int i = 0; i < list_value.Count; i++)
            {
                if (list_value[i] is Device)
                {
                    庫存 += ((Device)list_value[i]).Inventory.StringToInt32();
                }
            }
            return 庫存;
        }
        static public string Function_藥品碼檢查(string Code)
        {
            if (Code.Length < 5) Code = "0" + Code;
            if (Code.Length < 5) Code = "0" + Code;
            if (Code.Length < 5) Code = "0" + Code;
            if (Code.Length < 5) Code = "0" + Code;
            return Code;
        }
    }
}
