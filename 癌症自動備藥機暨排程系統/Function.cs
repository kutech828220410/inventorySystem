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
using H_Pannel_lib;
namespace 癌症自動備藥機暨排程系統
{
    public partial class Main_Form : Form
    {
        private List<Storage> Function_取得本地儲位()
        {
            return List_EPD266_本地資料 = storageUI_EPD_266.SQL_GetAllStorage();
        }
        static public int Function_從SQL取得庫存(string 藥品碼)
        {
            int 庫存 = 0;
            List<object> list_value = Function_從SQL取得儲位到本地資料(藥品碼);
            for (int i = 0; i < list_value.Count; i++)
            {

                if (list_value[i] is Device)
                {
                    Device device = list_value[i] as Device;
                    if (device != null)
                    {
                        庫存 += device.Inventory.StringToInt32();
                    }
                }

            }
            return 庫存;
        }
        static public List<object> Function_從SQL取得儲位到本地資料(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            List<Storage> storages = List_EPD266_本地資料.SortByCode(藥品碼);



            for (int i = 0; i < storages.Count; i++)
            {
                Storage storage = _storageUI_EPD_266.SQL_GetStorage(storages[i]);
                List_EPD266_本地資料.Add_NewStorage(storage);
                list_value.Add(storage);
            }

            return list_value;
        }

        private List<medClass> Function_取得藥檔資料()
        {
            string url = $"{API_Server}/api/MED_page/get_by_apiserver";
            returnData returnData = new returnData();
            returnData.ServerType = enum_ServerSetting_Type.癌症備藥機.GetEnumName();
            returnData.ServerName = $"{dBConfigClass.Name}";
            returnData.TableName = "medicine_page";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData returnData_result = json.JsonDeserializet<returnData>();
            List<medClass> medClasses = returnData_result.Data.ObjToClass<List<medClass>>();

            return medClasses;
        }
        private List<medClass> Function_取得有儲位藥檔資料()
        {
            List<medClass> medClasses = Function_取得藥檔資料();
            List<medClass> medClasses_buf = new List<medClass>();
            List<medClass> medClasses_result = new List<medClass>();
            List<Storage> storages = Function_取得本地儲位();
            storages.Sort(new Icp_Storage());
            List<string> codes = (from temp in storages
                                  select temp.Code).Distinct().ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                medClasses_buf = (from temp in medClasses
                                  where temp.藥品碼 == codes[i]
                                  select temp).ToList();
                if (medClasses_buf.Count > 0)
                {
                    medClasses_result.Add(medClasses_buf[0]);
                }
            }

            return medClasses_result;
        }


        static public string Function_ReadBacodeScanner01()
        {
            string text = MySerialPort_Scanner01.ReadString();
            if (text == null) return null;
            text = text.Replace("\0", "");
            if (text.StringIsEmpty()) return null;
            if (text.Length <= 2 || text.Length > 200) return null;
            if (text.Substring(text.Length - 2, 2) != "\r\n") return null;
            MySerialPort_Scanner01.ClearReadByte();
            text = text.Replace("\r\n", "");
            return text;
        }
        static public string Function_ReadBacodeScanner02()
        {
            string text = MySerialPort_Scanner01.ReadString();
            if (text == null) return null;
            text = text.Replace("\0", "");
            if (text.StringIsEmpty()) return null;
            if (text.Length <= 2 || text.Length > 200) return null;
            if (text.Substring(text.Length - 2, 2) != "\r\n") return null;
            MySerialPort_Scanner02.ClearReadByte();
            text = text.Replace("\r\n", "");
            return text;
        }
    }

    public class Icp_Storage : IComparer<Storage>
    {
        public int Compare(Storage x, Storage y)
        {
            string 藥品碼_0 = x.Code;
            string 藥品碼_1 = y.Code;
            return 藥品碼_0.CompareTo(藥品碼_1);
        }
    }

}
