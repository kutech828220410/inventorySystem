using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.Text.Json;
using H_Pannel_lib;

namespace HIS_DB_Lib
{
    public class deviceApiClass
    {
        public enum StoreType
        {
            藥庫,
            藥局,
            調劑台
        }
        static public List<DeviceBasic> GetDeviceBasics(string API_Server, string ServerName, string ServerType, StoreType storeType)
        {
            string url = $"{API_Server}/api/device/all";

            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;
            string tableName = "";
            if (storeType == StoreType.藥庫)
            {
                tableName = "firstclass_device_jsonstring";
            }
            if (storeType == StoreType.藥局)
            {
                tableName = "sd0_device_jsonstring";
            }

            returnData.TableName = tableName;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_result = json_out.JsonDeserializet<returnData>();
            if (returnData_result.Code != 200)
            {
                return null;
            }
            Console.WriteLine($"{returnData_result}");
            List<DeviceBasic> deviceBasics = returnData_result.Data.ObjToClass<List<DeviceBasic>>();
            return deviceBasics;
        }
        static public void SetDeviceBasics(string API_Server, string ServerName, string ServerType, StoreType storeType , List<DeviceBasic> deviceBasics)
        {
            string url = $"{API_Server}/api/device/update_device";

            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;
            string tableName = "";
            if (storeType == StoreType.藥庫)
            {
                tableName = "firstclass_device_jsonstring";
            }
            if (storeType == StoreType.藥局)
            {
                tableName = "sd0_device_jsonstring";
            }
            returnData.Data = deviceBasics;
            returnData.TableName = tableName;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_result = json_out.JsonDeserializet<returnData>();
            if (returnData_result.Code != 200)
            {
                return;
            }
            Console.WriteLine($"{returnData_result}");
        }
        static public List<DeviceBasic> GetDeviceBasicsByCode(string API_Server, string ServerName, string ServerType, string Code, StoreType storeType)
        {
            List<string> Codes = new List<string>();
            Codes.Add(Code);
            return GetDeviceBasicsByCode(API_Server, ServerName, ServerType, Codes , storeType);
        }
        static public List<DeviceBasic> GetDeviceBasicsByCode(string API_Server, string ServerName, string ServerType, List<string> Codes, StoreType storeType)
        {
            string url = $"{API_Server}/api/device/get_by_code";

            returnData returnData = new returnData();
            returnData.ServerName = ServerName;
            returnData.ServerType = ServerType;
            string text = "";
            for (int i = 0; i < Codes.Count; i++)
            {
                text += Codes[i];
                if (i != Codes.Count - 1) text += ",";
            }
            returnData.ValueAry.Add(text);
            string tableName = "";
            if (storeType == StoreType.藥庫)
            {
                tableName = "firstclass_device_jsonstring";
            }
            if (storeType == StoreType.藥局)
            {
                tableName = "sd0_device_jsonstring";
            }

            returnData.TableName = tableName;

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_result = json_out.JsonDeserializet<returnData>();
            if (returnData_result.Code != 200)
            {
                return null;
            }
            Console.WriteLine($"{returnData_result}");
            List<DeviceBasic> deviceBasics = returnData_result.Data.ObjToClass<List<DeviceBasic>>();
            return deviceBasics;
        }
    }
}
