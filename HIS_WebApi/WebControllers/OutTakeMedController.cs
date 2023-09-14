using Basic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SQLUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using H_Pannel_lib;
using System.Drawing;
using System.Diagnostics;
using MyUI;
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutTakeMedController : ControllerBase
    {


        public enum enum_設備資料
        {
            GUID,
            名稱,
            顏色,
            備註,
        }
       
        static private string API_Server = "http://127.0.0.1:4433/api/serversetting";
        static private string name = ConfigurationManager.AppSettings["name"];

        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        MyTimer myTimer = new MyTimer(50000);


        [Route("init")]
        [HttpPost]
        public string GET_init([FromBody] returnData returnData)
        {
            try
            {
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = $"找無Server資料!";
                    return returnData.JsonSerializationt();
                }
                return CheckCreatTable(serverSettingClasses[0]);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }

        [Route("statu")]
        [HttpGet()]
        public string Get_statu()
        {
            string jsonString = "";
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
       

            return jsonString;
        }

        [Route("Sample")]
        [HttpGet()]
        public string Get_Sample()
        {
            string jsonString = "";
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            List<class_OutTakeMed_data> list_class_OutTakeMed_data = new List<class_OutTakeMed_data>();
            class_OutTakeMed_data class_OutTakeMed_Data01 = new class_OutTakeMed_data();
            class_OutTakeMed_Data01.PRI_KEY = Guid.NewGuid().ToString();
            class_OutTakeMed_Data01.電腦名稱 = "PC001";
            class_OutTakeMed_Data01.成本中心 = "1";
            class_OutTakeMed_Data01.來源庫別 = "UD1F";
            class_OutTakeMed_Data01.藥品碼 = "25003";
            class_OutTakeMed_Data01.類別 = "F";
            class_OutTakeMed_Data01.交易量 = "-1";
            class_OutTakeMed_Data01.操作人 = "王曉明";
            class_OutTakeMed_Data01.ID = "HS001";
            class_OutTakeMed_Data01.病人姓名 = "章大同";
            class_OutTakeMed_Data01.床號 = "34-06061";
            class_OutTakeMed_Data01.病歷號 = "00000000";
            class_OutTakeMed_Data01.開方時間 = DateTime.Now.ToDateTimeString();
            class_OutTakeMed_Data01.功能類型 = "1";
            list_class_OutTakeMed_data.Add(class_OutTakeMed_Data01);

            class_OutTakeMed_data class_OutTakeMed_Data02 = new class_OutTakeMed_data();
            class_OutTakeMed_Data02.PRI_KEY = Guid.NewGuid().ToString();
            class_OutTakeMed_Data02.電腦名稱 = "PC001";
            class_OutTakeMed_Data02.成本中心 = "1";
            class_OutTakeMed_Data02.來源庫別 = "UD1F";
            class_OutTakeMed_Data02.藥品碼 = "25004";
            class_OutTakeMed_Data02.類別 = "F";
            class_OutTakeMed_Data02.交易量 = "-1";
            class_OutTakeMed_Data02.操作人 = "王曉明";
            class_OutTakeMed_Data02.ID = "HS001";
            class_OutTakeMed_Data02.病人姓名 = "章大同";
            class_OutTakeMed_Data02.床號 = "34-06061";
            class_OutTakeMed_Data02.病歷號 = "00000000";
            class_OutTakeMed_Data02.開方時間 = DateTime.Now.ToDateTimeString();
            class_OutTakeMed_Data02.功能類型 = "1";
            list_class_OutTakeMed_data.Add(class_OutTakeMed_Data02);

            jsonString = list_class_OutTakeMed_data.JsonSerializationt(true);

            return jsonString;
        }

        [Route("new")]
        [HttpPost]
        public string Post([FromBody] returnData returnData)
        {
            try
            {
                string json = returnData.Data.JsonSerializationt();
                List<class_OutTakeMed_data> data = json.JsonDeserializet<List<class_OutTakeMed_data>>();

                if (data == null)
                {
                    return "-1";
                }
                if (data.Count == 0)
                {
                    return "-1";
                }
                if (data.Count == 1)
                {
                    return single_med_take(returnData.ServerName, data);
                }
                else
                {
                    return mul_med_take(returnData.ServerName, data);
                }
            }
            catch(Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }
        [Route("{value}")]
        [HttpPost]
        public string Post([FromBody] List<class_OutTakeMed_data> data , string value)
        {

          
            if (data == null)
            {
                return "-1";
            }
            if (data.Count == 0)
            {
                return "-1";
            }
            if (data.Count == 1)
            {
                return single_med_take(value, data);
            }
            else
            {
                return mul_med_take(value, data);
            }
        
        }


        #region Function
        private string single_med_take(string name ,List<class_OutTakeMed_data> data)
        {


            
            if (!data[0].交易量.StringIsInt32())
            {
                return "-1";
            }
            if (data[0].藥品碼.StringIsEmpty())
            {
                return "-1";
            }
            if (data[0].操作人.StringIsEmpty())
            {
                return "-1";
            }
            if (data[0].電腦名稱.StringIsEmpty())
            {
                return "-1";
            }
            if (!data[0].開方時間.Check_Date_String())
            {
                data[0].開方時間 = DateTime.Now.ToDateTimeString();
            }
            if (data[0].功能類型 == "A")
            {
                string ProcessName = "調劑台管理系統";
                Process[] process = Process.GetProcesses();
                int num = 0;
                for (int i = 0; i < process.Length; i++)
                {
                    if (process[i].ProcessName == ProcessName) num++;
                }
                if (num >= 1) return "OK";
                else return "NG";
            }
            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            ServerSettingClass serverSettingClass = serverSettingClasses.MyFind(name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.本地端);
            ServerSettingClass serverSettingClass_人員資料 = serverSettingClasses.MyFind(name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.VM端);
            if (serverSettingClass == null)
            {
                return "serverSettingClass[一般資料] is null!";
            }
            if (serverSettingClass_人員資料 == null)
            {
                return "serverSettingClass[人員資料] is null!";
            }
            string IP = serverSettingClass.Server;
            string DataBaseName = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();


            string devicelist_IP = serverSettingClass_人員資料.Server;
            string devicelist_database = serverSettingClass_人員資料.DBName;

            SQLControl sQLControl_trading = new SQLControl(IP, DataBaseName, "trading", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_take_medicine_stack = new SQLControl(IP, DataBaseName, "take_medicine_stack_new", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_devicelist = new SQLControl(devicelist_IP, devicelist_database, "devicelist", UserName, Password, Port, SSLMode);


            List<object[]> list_devicelist = sQLControl_devicelist.GetAllRows(null);
            List<object[]> list_devicelist_buf = new List<object[]>();
            list_devicelist_buf = list_devicelist.GetRows((int)enum_設備資料.名稱, data[0].電腦名稱);
            if (list_devicelist_buf.Count == 0)
            {
                object[] value = new object[new enum_設備資料().GetLength()];
                value[(int)enum_設備資料.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_設備資料.名稱] = data[0].電腦名稱;
                Color color = this.Function_取得顏色(list_devicelist.Count);
                value[(int)enum_設備資料.顏色] = color.ToColorString();
                sQLControl_devicelist.AddRow(null, value);
                list_devicelist_buf.Add(value);
            }

            List<DeviceBasic> devices = this.Function_讀取儲位(name);
            List<DeviceBasic> list_device = devices.SortByCode(data[0].藥品碼);
            if (list_device.Count == 0)
            {
                return "-2";
            }
            //取藥亮燈
            if (data[0].功能類型 == "1")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }
                if (data[0].PRI_KEY.StringIsEmpty()) data[0].PRI_KEY = Guid.NewGuid().ToString();
                string PRI_KEY = data[0].PRI_KEY;
                string 藥品碼 = data[0].藥品碼;
                int 總異動量 = data[0].交易量.StringToInt32();
                if (總異動量 != 0)
                {
                    List<object[]> list_trading = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥袋序號, data[0].PRI_KEY);
                    if (list_trading.Count > 0) return "-4";
                }
                else
                {
                    PRI_KEY = Guid.NewGuid().ToString();
                }


                string 藥品名稱 = list_device[0].Name;
                string 單位 = list_device[0].Package;
                string 病歷號 = data[0].病歷號;
                string 病人姓名 = data[0].病人姓名;
                string 開方時間 = data[0].開方時間;
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 操作人 = data[0].操作人;
                string 顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                string 類別 = data[0].類別;
                string 床號 = data[0].床號;
                string 領藥號 = data[0].領藥號;

                this.Function_取藥堆疊資料_取藥新增(serverSettingClass, 設備名稱, 藥品碼, 藥品名稱, PRI_KEY, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, 操作人, 操作時間, 顏色, 總異動量);
                return $"OK";
            }
            else if (data[0].功能類型 == "0")
            {
                return $"OK";
            }
            //取藥不亮燈
            else if (data[0].功能類型 == "-1")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);

                }
                if (data[0].PRI_KEY.StringIsEmpty()) data[0].PRI_KEY = Guid.NewGuid().ToString();
                string PRI_KEY = data[0].PRI_KEY;
                string 藥品碼 = data[0].藥品碼;

                List<object[]> list_trading = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥袋序號, data[0].PRI_KEY);
                if (list_trading.Count > 0) return "-4";



                string 藥品名稱 = list_device[0].Name;
                string 單位 = list_device[0].Package;
                string 病歷號 = data[0].病歷號;
                string 病人姓名 = data[0].病人姓名;
                string 開方時間 = data[0].開方時間;
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 操作人 = data[0].操作人;
                string 顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                int 總異動量 = data[0].交易量.StringToInt32();
                string 類別 = data[0].類別;
                string 床號 = data[0].床號;
                string 領藥號 = data[0].領藥號;

                this.Function_取藥堆疊資料_取藥新增(serverSettingClass, 設備名稱, 藥品碼, 藥品名稱, PRI_KEY, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, 操作人, 操作時間, Color.Black.ToColorString(), 總異動量);
                return $"OK";
            }
            else if (data[0].功能類型 == "-2")
            {
                List<object[]> list_take_medicine_stack = new List<object[]>();
                list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, data[0].電腦名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }
                return "OK";
            }
            else if (data[0].功能類型 == "5")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }
                if (data[0].PRI_KEY.StringIsEmpty()) data[0].PRI_KEY = Guid.NewGuid().ToString();
     

                string PRI_KEY = data[0].PRI_KEY;
                string 藥品碼 = data[0].藥品碼;
                int 總異動量 = data[0].交易量.StringToInt32();
                string 藥品名稱 = list_device[0].Name;
                string 單位 = list_device[0].Package;
                string 病歷號 = "";
                string 病人姓名 = "";
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 操作人 = data[0].操作人;
                string 顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                string 類別 = data[0].類別;
                string 床號 = "";
                if (data[0].效期.Check_Date_String() == false) data[0].效期 = "2050/12/31";

                string 效期 = data[0].效期;
                string 批號 = data[0].批號;
                string 領藥號 = data[0].領藥號;
                
                this.Function_取藥堆疊資料_入庫新增(serverSettingClass, 設備名稱, 藥品碼, 藥品名稱, PRI_KEY, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, 操作人, 操作時間, 顏色, 總異動量, 效期, 批號);
                return $"OK";
            }
            else if (data[0].功能類型 == "-5")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }
                if (data[0].PRI_KEY.StringIsEmpty()) data[0].PRI_KEY = Guid.NewGuid().ToString();


                string PRI_KEY = data[0].PRI_KEY;
                string 藥品碼 = data[0].藥品碼;
                int 總異動量 = data[0].交易量.StringToInt32();
                string 藥品名稱 = list_device[0].Name;
                string 單位 = list_device[0].Package;
                string 病歷號 = "";
                string 病人姓名 = "";
                string 開方時間 = DateTime.Now.ToDateTimeString_6();
                string 操作時間 = DateTime.Now.ToDateTimeString_6();
                string 操作人 = data[0].操作人;
                string 顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                string 類別 = data[0].類別;
                string 床號 = "";
                if (data[0].效期.Check_Date_String() == false) data[0].效期 = "2050/12/31";
                string 效期 = data[0].效期;
                string 批號 = data[0].批號;
                string 領藥號 = data[0].領藥號;

                this.Function_取藥堆疊資料_入庫新增(serverSettingClass, 設備名稱, 藥品碼, 藥品名稱, PRI_KEY, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, 操作人, 操作時間, Color.Black.ToColorString(), 總異動量, 效期, 批號);
                return $"OK";
            }
            else
            {
                return $"-3";
            }
        }
        private string mul_med_take(string name, List<class_OutTakeMed_data> data)
        {
            for (int i = 0; i < data.Count; i++)
            {

                if (!data[i].交易量.StringIsInt32())
                {
                    return "-1";
                }
                if (data[i].藥品碼.StringIsEmpty())
                {
                    return "-1";
                }
                if (data[i].操作人.StringIsEmpty())
                {
                    return "-1";
                }
                if (data[i].電腦名稱.StringIsEmpty())
                {
                    return "-1";
                }
                if (!data[i].開方時間.Check_Date_String())
                {
                    data[i].開方時間 = DateTime.Now.ToDateTimeString();
                }
            }

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            ServerSettingClass serverSettingClass = serverSettingClasses.MyFind(name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.本地端);
            ServerSettingClass serverSettingClass_人員資料 = serverSettingClasses.MyFind(name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.VM端);
            if (serverSettingClass == null)
            {
                return "serverSettingClass[一般資料] is null!";
            }
            if (serverSettingClass_人員資料 == null)
            {
                return "serverSettingClass[人員資料] is null!";
            }
            string IP = serverSettingClass.Server;
            string DataBaseName = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();


            string devicelist_IP = serverSettingClass_人員資料.Server;
            string devicelist_database = serverSettingClass_人員資料.DBName;

            SQLControl sQLControl_trading = new SQLControl(IP, DataBaseName, "trading", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_take_medicine_stack = new SQLControl(IP, DataBaseName, "take_medicine_stack_new", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_devicelist = new SQLControl(devicelist_IP, devicelist_database, "devicelist", UserName, Password, Port, SSLMode);

            List<object[]> list_devicelist = sQLControl_devicelist.GetAllRows(null);
            List<object[]> list_devicelist_buf = new List<object[]>();
            list_devicelist_buf = list_devicelist.GetRows((int)enum_設備資料.名稱, data[0].電腦名稱);
            if (list_devicelist_buf.Count == 0)
            {
                object[] value = new object[new enum_設備資料().GetLength()];
                value[(int)enum_設備資料.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_設備資料.名稱] = data[0].電腦名稱;
                Color color = this.Function_取得顏色(list_devicelist.Count);
                value[(int)enum_設備資料.顏色] = color.ToColorString();
                sQLControl_devicelist.AddRow(null, value);
                list_devicelist_buf.Add(value);
            }
            List<DeviceBasic> devices = this.Function_讀取儲位(name);
         
            if (data[0].功能類型 == "1")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }


                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].PRI_KEY.StringIsEmpty()) data[i].PRI_KEY = Guid.NewGuid().ToString();
                    string PRI_KEY = data[i].PRI_KEY;
                    string 藥品碼 = data[i].藥品碼;
                    List<DeviceBasic> list_device = devices.SortByCode(data[i].藥品碼);
                    if (list_device.Count == 0) continue;
                    int 總異動量 = data[i].交易量.StringToInt32();
                    if (總異動量 != 0)
                    {
                        List<object[]> list_trading = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥袋序號, data[i].PRI_KEY);
                        if (list_trading.Count > 0) return "-4";
                    }
                    else
                    {
                        PRI_KEY = Guid.NewGuid().ToString();
                    }
                    string 藥品名稱 = list_device[0].Name;
                    string 單位 = list_device[0].Package;
                    string 病歷號 = data[i].病歷號;
                    string 病人姓名 = data[i].病人姓名;
                    string 開方時間 = data[i].開方時間;
                    string 操作時間 = DateTime.Now.ToDateTimeString_6();
                    string 操作人 = data[i].操作人;
                    string 顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                    string 類別 = data[i].類別;
                    string 床號 = data[i].床號;
                    string 領藥號 = data[i].領藥號;
                    this.Function_取藥堆疊資料_取藥新增(serverSettingClass ,設備名稱, 藥品碼, 藥品名稱, PRI_KEY, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, 操作人, 操作時間, 顏色, 總異動量);
                   
                }
                return $"OK";
            }
            else if (data[0].功能類型 == "A1")
            {
                string 設備名稱 = data[0].電腦名稱;

                while(true)
                {
                    List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                    if (list_take_medicine_stack.Count == 0) break;
                    if (list_take_medicine_stack.Count > 0)
                    {
                        sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                    }
                }
               


                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].PRI_KEY.StringIsEmpty()) data[i].PRI_KEY = Guid.NewGuid().ToString();
                    string PRI_KEY = data[i].PRI_KEY;
                    string 藥品碼 = data[i].藥品碼;
                    List<DeviceBasic> list_device = devices.SortByCode(data[i].藥品碼);
                    if (list_device.Count == 0) continue;
                    int 總異動量 = data[i].交易量.StringToInt32();
                    if (總異動量 != 0)
                    {
                        //List<object[]> list_trading = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥袋序號, data[i].PRI_KEY);
                        //if (list_trading.Count > 0) return "-4";
                    }
                    else
                    {
                        PRI_KEY = Guid.NewGuid().ToString();
                    }
                    string 藥品名稱 = list_device[0].Name;
                    string 單位 = list_device[0].Package;
                    string 病歷號 = data[i].病歷號;
                    string 病人姓名 = data[i].病人姓名;
                    string 開方時間 = data[i].開方時間;
                    string 操作時間 = DateTime.Now.ToDateTimeString_6();
                    string 操作人 = data[i].操作人;
                    string 顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                    string 類別 = data[i].類別;
                    string 床號 = data[i].床號;
                    string 領藥號 = data[i].領藥號;
                    this.Function_取藥堆疊資料_取藥新增(serverSettingClass, 設備名稱, 藥品碼, 藥品名稱, PRI_KEY, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, 操作人, 操作時間, 顏色, 總異動量);

                }
                return $"OK";
            }
            else if (data[0].功能類型 == "-1")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].PRI_KEY.StringIsEmpty()) data[i].PRI_KEY = Guid.NewGuid().ToString();
                    string PRI_KEY = data[i].PRI_KEY;
                    string 藥品碼 = data[i].藥品碼;
                    List<DeviceBasic> list_device = devices.SortByCode(data[i].藥品碼);
                    if (list_device.Count == 0) continue;
                    int 總異動量 = data[i].交易量.StringToInt32();
                    if (總異動量 != 0)
                    {
                        List<object[]> list_trading = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥袋序號, data[i].PRI_KEY);
                        if (list_trading.Count > 0) return "-4";
                    }
                    else
                    {
                        PRI_KEY = Guid.NewGuid().ToString();
                    }
                    string 藥品名稱 = list_device[0].Name;
                    string 單位 = list_device[0].Package;
                    string 病歷號 = data[i].病歷號;
                    string 病人姓名 = data[i].病人姓名;
                    string 開方時間 = data[i].開方時間;
                    string 操作時間 = DateTime.Now.ToDateTimeString_6();
                    string 操作人 = data[i].操作人;
                    string 顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                    string 類別 = data[i].類別;
                    string 床號 = data[i].床號;
                    string 領藥號 = data[i].領藥號;

                    this.Function_取藥堆疊資料_取藥新增(serverSettingClass, 設備名稱, 藥品碼, 藥品名稱, PRI_KEY, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, 操作人, 操作時間, Color.Black.ToColorString(), 總異動量);
                }
               
                return $"OK";
            }
            else
            {
                return $"-3";
            }
        }
    
        private List<DeviceBasic> Function_讀取儲位(string name)
        {

            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
            ServerSettingClass serverSettingClass = serverSettingClasses.MyFind(name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.一般資料);
            ServerSettingClass serverSettingClass_人員資料 = serverSettingClasses.MyFind(name, enum_ServerSetting_Type.調劑台, enum_ServerSetting_調劑台.人員資料);
            if (serverSettingClass == null)
            {
                
            }
            if (serverSettingClass_人員資料 == null)
            {
               
            }
            string IP = serverSettingClass.Server;
            string DataBaseName = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();


            string devicelist_IP = serverSettingClass_人員資料.Server;
            string devicelist_database = serverSettingClass_人員資料.DBName;

            SQLControl sQLControl_EPD583_serialize = new SQLControl(IP, DataBaseName, "epd583_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DataBaseName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DataBaseName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(IP, DataBaseName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);


            List<object[]> list_EPD583 = sQLControl_EPD583_serialize.GetAllRows(null);
            List<object[]> list_EPD266 = sQLControl_EPD266_serialize.GetAllRows(null);
            List<object[]> list_RowsLED = sQLControl_RowsLED_serialize.GetAllRows(null);
            List<object[]> list_RFID_Device = sQLControl_RFID_Device_serialize.GetAllRows(null);
            Console.WriteLine($"從SQL取得所有儲位資料,耗時{myTimer.ToString()}ms");
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();
            deviceBasics.LockAdd(DrawerMethod.GetAllDeviceBasic(list_EPD583));
            deviceBasics.LockAdd(StorageMethod.GetAllDeviceBasic(list_EPD266));
            deviceBasics.LockAdd(RowsLEDMethod.GetAllDeviceBasic(list_RowsLED));
            deviceBasics.LockAdd(RFIDMethod.GetAllDeviceBasic(list_RFID_Device));
            Console.WriteLine($"反編譯取得所有儲位資料,耗時{myTimer.ToString()}ms");
            deviceBasics_buf = (from value in deviceBasics
                                where value.Code.StringIsEmpty() == false
                                select value).ToList();
            
            return deviceBasics_buf;
        }
        private int Function_取得儲位庫存(string 藥品碼 , List<Device> devices)
        {
            int 庫存 = 0;
            for (int k = 0; k < devices.Count; k++)
            {
                if (devices[k] is Device)
                {
                    Device device = devices[k] as Device;
                    庫存 += device.Inventory.StringToInt32();
                }
            }
            return 庫存;
        }
        private bool Function_取藥堆疊資料_取藥新增(ServerSettingClass serverSettingClass ,string 設備名稱, string 藥品碼, string 藥品名稱, string 藥袋序號, string 領藥號, string 類別, string 單位, string 病歷號, string 病人姓名, string 床號, string 開方時間, string 操作人, string 操作時間, string 顏色, int 總異動量)
        {
            return this.Function_取藥堆疊資料_新增母資料(serverSettingClass ,Guid.NewGuid().ToString(), 設備名稱, enum_交易記錄查詢動作.系統領藥, 藥品碼, 藥品名稱, 藥袋序號, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, "", 操作人, 操作時間, 顏色, 總異動量, "", "");
        }
        private bool Function_取藥堆疊資料_入庫新增(ServerSettingClass serverSettingClass, string 設備名稱, string 藥品碼, string 藥品名稱, string 藥袋序號, string 領藥號, string 類別, string 單位, string 病歷號, string 病人姓名, string 床號, string 開方時間, string 操作人, string 操作時間, string 顏色, int 總異動量, string 效期, string 批號)
        {
            return this.Function_取藥堆疊資料_新增母資料(serverSettingClass, Guid.NewGuid().ToString(), 設備名稱, enum_交易記錄查詢動作.系統入庫, 藥品碼, 藥品名稱, 藥袋序號, 領藥號, 類別, 單位, 病歷號, 病人姓名, 床號, 開方時間, "", 操作人, 操作時間, 顏色, 總異動量, 效期, 批號);
        }
        private bool Function_取藥堆疊資料_新增母資料(ServerSettingClass serverSettingClass ,string GUID, string 設備名稱, enum_交易記錄查詢動作 _enum_交易記錄查詢動作, string 藥品碼, string 藥品名稱, string 藥袋序號, string 領藥號, string 類別, string 單位, string 病歷號, string 病人姓名, string 床號, string 開方時間, string IP, string 操作人, string 操作時間, string 顏色, int 總異動量, string 效期, string 批號)
        {
            if (serverSettingClass == null)
            {
                return false;
            }
   
            string server = serverSettingClass.Server;
            string DataBaseName = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();



            SQLControl sQLControl_take_medicine_stack = new SQLControl(server, DataBaseName, "take_medicine_stack_new", UserName, Password, Port, SSLMode);

            object[] value = new object[ new enum_取藥堆疊母資料().GetLength()];
            value[(int)enum_取藥堆疊母資料.GUID] = GUID;
            value[(int)enum_取藥堆疊母資料.序號] = DateTime.Now.ToDateTimeString_6();
            value[(int)enum_取藥堆疊母資料.調劑台名稱] = 設備名稱;
            value[(int)enum_取藥堆疊母資料.操作人] = 操作人;
            value[(int)enum_取藥堆疊母資料.IP] = "";
          
            value[(int)enum_取藥堆疊母資料.動作] = _enum_交易記錄查詢動作.GetEnumName();
            value[(int)enum_取藥堆疊母資料.藥袋序號] = 藥袋序號;
            value[(int)enum_取藥堆疊母資料.藥品碼] = 藥品碼;
            value[(int)enum_取藥堆疊母資料.藥品名稱] = 藥品名稱;
            value[(int)enum_取藥堆疊母資料.領藥號] = 領藥號;
            value[(int)enum_取藥堆疊母資料.類別] = 類別;
            value[(int)enum_取藥堆疊母資料.單位] = 單位;
            value[(int)enum_取藥堆疊母資料.病歷號] = 病歷號;
            value[(int)enum_取藥堆疊母資料.病人姓名] = 病人姓名;
            value[(int)enum_取藥堆疊母資料.床號] = 床號;
            value[(int)enum_取藥堆疊母資料.開方時間] = 開方時間;
            value[(int)enum_取藥堆疊母資料.操作時間] = 操作時間;
            value[(int)enum_取藥堆疊母資料.顏色] = 顏色;
         
          
            if (_enum_交易記錄查詢動作 == enum_交易記錄查詢動作.系統入庫) value[(int)enum_取藥堆疊母資料.狀態] = "新增效期";
            value[(int)enum_取藥堆疊母資料.庫存量] = "0";
            value[(int)enum_取藥堆疊母資料.總異動量] = 總異動量.ToString();
            value[(int)enum_取藥堆疊母資料.結存量] = "0";
            value[(int)enum_取藥堆疊母資料.效期] = 效期;
            value[(int)enum_取藥堆疊母資料.批號] = 批號;
            //List<object[]> list_value = sQLControl_take_medicine_stack.GetAllRows(null);
            //list_value = list_value.GetRows((int)enum_取藥堆疊母資料.藥品碼, 藥品碼);
            //list_value = list_value.GetRows((int)enum_取藥堆疊母資料.病歷號, 病歷號);
            //list_value = list_value.GetRows((int)enum_取藥堆疊母資料.開方時間, 開方時間);
            //if (list_value.Count == 0)
            //{
            //    sQLControl_take_medicine_stack.AddRow(null, value);
            //    return true;
            //}
            //return false;

            sQLControl_take_medicine_stack.AddRow(null, value);
            return true;
         

        }
        private Color Function_取得顏色(int index)
        {
            index = index % 7;
            if (index == 0)
            {
                return Color.Red;
            }
            else if (index == 1)
            { 
                return Color.Orange;
            }
            else if (index == 2)
            {
                return Color.Green;
            }
            else if (index == 3)
            {
                return Color.Green;
            }
            else if (index == 4)
            {
                return Color.Blue;
            }
            else if (index == 5)
            {
                return Color.Purple;
            }
            else if (index == 6)
            {
                return Color.White;
            }
            else return Color.Red;
        }
        #endregion
        private string CheckCreatTable(ServerSettingClass serverSettingClass)
        {

            string Server = serverSettingClass.Server;
            string DB = serverSettingClass.DBName;
            string UserName = serverSettingClass.User;
            string Password = serverSettingClass.Password;
            uint Port = (uint)serverSettingClass.Port.StringToInt32();
            List<Table> tables = new List<Table>();
            SQLControl sQLControl_take_medicine_stack_new = new SQLControl(Server, DB, "take_medicine_stack_new", UserName, Password, Port, SSLMode);
            Table table = new Table("take_medicine_stack_new");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("Order_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.INDEX);
            table.AddColumnList("序號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("調劑台名稱", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("IP", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("操作人", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("藥師證字號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("動作", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table.AddColumnList("作業模式", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("藥袋序號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("領藥號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("類別", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("藥品名稱", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("單位", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("病歷號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("病人姓名", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("床號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("頻次", Table.StringType.VARCHAR, 15, Table.IndexType.None);
            table.AddColumnList("操作時間", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("開方時間", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("顏色", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("狀態", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("庫存量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("總異動量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("結存量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("盤點量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("效期", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("批號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("備註", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("收支原因", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("診別", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            if (!sQLControl_take_medicine_stack_new.IsTableCreat()) sQLControl_take_medicine_stack_new.CreatTable(table);
            else sQLControl_take_medicine_stack_new.CheckAllColumnName(table, true);
            tables.Add(table);
            SQLControl sQLControl_take_medicine_substack_new = new SQLControl(Server, DB, "take_medicine_substack_new", UserName, Password, Port, SSLMode);
            table = new Table("take_medicine_substack_new");
            table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
            table.AddColumnList("Master_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("Device_GUID", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("序號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("調劑台名稱", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("藥品碼", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("IP", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("Num", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("TYPE", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("效期", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("批號", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("異動量", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("致能", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("流程作業完成", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("配藥完成", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("調劑結束", Table.StringType.VARCHAR, 10, Table.IndexType.None);
            table.AddColumnList("已入帳", Table.StringType.VARCHAR, 10, Table.IndexType.None);        
            if (!sQLControl_take_medicine_substack_new.IsTableCreat()) sQLControl_take_medicine_substack_new.CreatTable(table);
            else sQLControl_take_medicine_substack_new.CheckAllColumnName(table, true);
            tables.Add(table);

            return tables.JsonSerializationt(true);
        }
    }
}
