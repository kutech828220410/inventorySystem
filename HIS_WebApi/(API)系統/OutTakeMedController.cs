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
            string result = "";
            try
            {
                string json = returnData.Data.JsonSerializationt();
                List<class_OutTakeMed_data> data = returnData.Data.ObjToClass<List<class_OutTakeMed_data>>();
                if (data == null)
                {
                    result = "-1";
                }
                if (data.Count == 0)
                {
                    result = "-1";
                }
                if (data.Count == 1)
                {
                    result = mul_med_take(returnData.ServerName, data);
                }
                else
                {
                    result = mul_med_take(returnData.ServerName, data);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            finally
            {
                string json_out = returnData.JsonSerializationt(true);
                Logger.LogAddLine($"OutTakeMed");
                Logger.Log($"OutTakeMed", $"result : {result} \n{json_out}");
                Logger.LogAddLine($"OutTakeMed");
            }
            return result;
        }
        [Route("{value}")]
        [HttpPost]
        public string Post([FromBody] List<class_OutTakeMed_data> data, string value)
        {
            string result = "";
            try
            {
                if (data == null)
                {
                    result = "-1";
                }
                if (data.Count == 0)
                {
                    result = "-1";
                }
                if (data.Count == 1)
                {
                    result = mul_med_take(value, data);
                }
                else
                {
                    result = mul_med_take(value, data);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            finally
            {
                string json_out = data.JsonSerializationt(true);
                Logger.LogAddLine($"OutTakeMed");
                Logger.Log($"OutTakeMed", $"value : {value} , result : {result} \n{json_out}");
                Logger.LogAddLine($"OutTakeMed");
            }

            return result;

        }
        [Route("light_on")]
        [HttpPost]
        public string POST_light_on(returnData returnData)
        {
            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();

                returnData.Method = "POST_light_on";
                string input_str = returnData.Value;
                if (input_str.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = "Value空白,請輸入[藥碼,R,G,B,亮燈時間]!";
                    return returnData.JsonSerializationt();
                }
                string[] input_str_Ary = input_str.Split(",");
                if (input_str_Ary.Length != 5)
                {
                    returnData.Code = -200;
                    returnData.Result = "Value格式錯誤,請輸入[藥碼,R,G,B,亮燈時間]!";
                    return returnData.JsonSerializationt();
                }
                List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{API_Server}");
                serverSettingClasses = serverSettingClasses.MyFind(returnData.ServerName, returnData.ServerType, "一般資料");
                if (serverSettingClasses.Count == 0)
                {
                    returnData.Code = -200;
                    returnData.Result = "找無serverSettingClass資料!";
                    return returnData.JsonSerializationt();
                }
                ServerSettingClass serverSettingClass = serverSettingClasses[0];
                string IP = serverSettingClass.Server;
                string DataBaseName = serverSettingClass.DBName;
                string UserName = serverSettingClass.User;
                string Password = serverSettingClass.Password;
                uint Port = (uint)serverSettingClass.Port.StringToInt32();
                SQLControl sQLControl_take_medicine_stack = new SQLControl(IP, DataBaseName, "take_medicine_stack_new", UserName, Password, Port, SSLMode);
                string 藥碼 = input_str_Ary[0];
                byte R = (byte)(input_str_Ary[1].StringToInt32());
                byte G = (byte)(input_str_Ary[2].StringToInt32());
                byte B = (byte)(input_str_Ary[3].StringToInt32());
                int time = input_str_Ary[4].StringToInt32();
                object[] value = new object[new enum_取藥堆疊母資料().GetLength()];
                value[(int)enum_取藥堆疊母資料.GUID] = Guid.NewGuid();
                value[(int)enum_取藥堆疊母資料.序號] = DateTime.Now.ToDateTimeString_6();
                value[(int)enum_取藥堆疊母資料.藥品碼] = 藥碼;
                value[(int)enum_取藥堆疊母資料.調劑台名稱] = "儲位亮燈";

                value[(int)enum_取藥堆疊母資料.開方時間] = DateTime.Now.ToDateTimeString();
                value[(int)enum_取藥堆疊母資料.操作時間] = DateTime.Now.ToDateTimeString();
                value[(int)enum_取藥堆疊母資料.顏色] = Color.FromArgb(R, G, B).ToColorString();
                value[(int)enum_取藥堆疊母資料.狀態] = "None";
                value[(int)enum_取藥堆疊母資料.總異動量] = time;




                sQLControl_take_medicine_stack.AddRow(null, value);

                returnData.Data = "";
                returnData.Code = 200;
                returnData.Result = $"亮燈完成! 藥碼:{input_str_Ary[0]},Color({input_str_Ary[1]},{input_str_Ary[2]},{input_str_Ary[3]})";
                returnData.TimeTaken = myTimerBasic.ToString();
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }

        }

        #region Function
        private string single_med_take(string name, List<class_OutTakeMed_data> data)
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
            SQLControl sQLControl_devicelist = new SQLControl(devicelist_IP, devicelist_database, "devicelist", UserName, Password, serverSettingClass_人員資料.Port.StringToUInt32(), SSLMode);


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
            //取藥亮燈(不亮燈)
            if (data[0].功能類型 == "1" || data[0].功能類型 == "-1" || data[0].功能類型 == "2")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }
                if (data[0].PRI_KEY.StringIsEmpty()) data[0].PRI_KEY = Guid.NewGuid().ToString();
                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.動作 = enum_交易記錄查詢動作.系統領藥;
                takeMedicineStackClass.藥袋序號 = data[0].PRI_KEY;
                takeMedicineStackClass.藥品碼 = data[0].藥品碼;
                takeMedicineStackClass.藥品名稱 = list_device[0].Name;
                takeMedicineStackClass.單位 = list_device[0].Package;
                takeMedicineStackClass.病歷號 = data[0].病歷號;
                takeMedicineStackClass.病人姓名 = data[0].病人姓名;
                takeMedicineStackClass.開方時間 = data[0].開方時間;
                takeMedicineStackClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.操作人 = data[0].操作人;
                takeMedicineStackClass.顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                takeMedicineStackClass.類別 = data[0].類別;
                takeMedicineStackClass.床號 = data[0].床號;
                takeMedicineStackClass.領藥號 = data[0].領藥號;
                takeMedicineStackClass.總異動量 = data[0].交易量;
                takeMedicineStackClass.收支原因 = data[0].收支原因;
                if (data[0].功能類型 == "-1") takeMedicineStackClass.顏色 = Color.Black.ToColorString();
                if (data[0].功能類型 == "1")
                {
                    if (takeMedicineStackClass.總異動量.StringToInt32() != 0)
                    {
                        List<object[]> list_trading = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥袋序號, data[0].PRI_KEY);
                        if (list_trading.Count > 0) return "-4";
                    }
                    else
                    {
                        takeMedicineStackClass.藥袋序號 = Guid.NewGuid().ToString();
                    }
                }
                else if (data[0].功能類型 == "-1")
                {
                    List<object[]> list_trading = sQLControl_trading.GetRowsByDefult(null, (int)enum_交易記錄查詢資料.藥袋序號, data[0].PRI_KEY);
                    if (list_trading.Count > 0) return "-4";
                }
                this.Function_取藥堆疊資料_新增母資料(serverSettingClass, 設備名稱, takeMedicineStackClass);
                return $"OK";
            }
            else if (data[0].功能類型 == "0")
            {
                return $"OK";
            }
            //清除指定電腦名稱資料(滅燈)
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
            //入庫亮燈(不亮燈)
            else if (data[0].功能類型 == "5" || data[0].功能類型 == "-5")
            {
                string 設備名稱 = data[0].電腦名稱;
                List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
                if (list_take_medicine_stack.Count > 0)
                {
                    sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                }
                if (data[0].PRI_KEY.StringIsEmpty()) data[0].PRI_KEY = Guid.NewGuid().ToString();

                takeMedicineStackClass takeMedicineStackClass = new takeMedicineStackClass();
                takeMedicineStackClass.動作 = enum_交易記錄查詢動作.系統入庫;
                takeMedicineStackClass.藥袋序號 = data[0].PRI_KEY;
                takeMedicineStackClass.藥品碼 = data[0].藥品碼;
                takeMedicineStackClass.總異動量 = data[0].交易量;
                takeMedicineStackClass.藥品名稱 = list_device[0].Name;
                takeMedicineStackClass.單位 = list_device[0].Package;
                takeMedicineStackClass.病歷號 = "";
                takeMedicineStackClass.病人姓名 = "";
                takeMedicineStackClass.開方時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.操作時間 = DateTime.Now.ToDateTimeString_6();
                takeMedicineStackClass.操作人 = data[0].操作人;
                takeMedicineStackClass.顏色 = list_devicelist_buf[0][(int)enum_設備資料.顏色].ObjectToString();
                if (data[0].功能類型 == "-5") takeMedicineStackClass.顏色 = Color.Black.ToColorString();
                takeMedicineStackClass.類別 = data[0].類別;
                takeMedicineStackClass.床號 = "";
                if (data[0].效期.Check_Date_String() == false) data[0].效期 = "2050/12/31";

                takeMedicineStackClass.效期 = data[0].效期;
                takeMedicineStackClass.批號 = data[0].批號;
                takeMedicineStackClass.領藥號 = data[0].領藥號;
                takeMedicineStackClass.收支原因 = data[0].收支原因;
                this.Function_取藥堆疊資料_新增母資料(serverSettingClass, 設備名稱, takeMedicineStackClass);
                return $"OK";
            }

            else
            {
                return $"-3";
            }
        }
        /// <summary>
        /// op_type : 1(取藥亮燈) -1(取藥扣帳) 2(取藥亮燈並扣帳) -3(加藥) -4(退藥) -5(入庫扣帳) -6(撥入) -7(撥出) -8(調入) -9(調出)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string mul_med_take(string name, List<class_OutTakeMed_data> data)
        {
            returnData returnData = new returnData();
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            for (int i = 0; i < data.Count; i++)
            {
                returnData.Method = "mul_med_take";
                if (!data[i].交易量.StringIsInt32())
                {
                    returnData.Code = -200;
                    returnData.Result = $"交易量(value)不得為<0>";
                    return returnData.JsonSerializationt(true);
                }
                if (data[i].藥品碼.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"藥品碼(code)空白";
                    return returnData.JsonSerializationt(true);
                }
                if (data[i].操作人.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"操作人(operator)空白";
                    return returnData.JsonSerializationt(true);
                }
                if (data[i].電腦名稱.StringIsEmpty())
                {
                    data[i].電腦名稱 = "System";
                    //returnData.Code = -200;
                    //returnData.Result = $"電腦名稱(MC_name)空白";
                    //return returnData.JsonSerializationt(true);
                }
                if (data[i].功能類型.StringIsEmpty())
                {
                    returnData.Code = -200;
                    returnData.Result = $"類別(op_type)空白";
                    return returnData.JsonSerializationt(true);
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
            SQLControl sQLControl_devicelist = new SQLControl(devicelist_IP, devicelist_database, "devicelist", UserName, Password, serverSettingClass_人員資料.Port.StringToUInt32(), SSLMode);

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

            string 設備名稱 = data[0].電腦名稱;
            List<takeMedicineStackClass> takeMedicineStackClasses = new List<takeMedicineStackClass>();
            List<object[]> list_take_medicine_stack = sQLControl_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
            if (list_take_medicine_stack.Count > 0)
            {
                sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
            }
            for (int i = 0; i < data.Count; i++)
            {
                string date_str = $"{data[i].日期} {data[i].時間}";
                if (date_str.Check_Date_String())
                {
                    data[i].開方時間 = date_str;
                }

                //清除指定電腦名稱資料(滅燈)
                if (data[0].功能類型 == "-2")
                {

                    if (list_take_medicine_stack.Count > 0)
                    {
                        sQLControl_take_medicine_stack.DeleteExtra(null, list_take_medicine_stack);
                    }
                    returnData.Code = 200;
                    returnData.Result = $"清除指定電腦名稱資料(滅燈)成功";
                    return returnData.JsonSerializationt(true);
                }
                if (data[i].功能類型 == "1" || data[i].功能類型 == "-1" || data[i].功能類型 == "2")
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
                        if (list_trading.Count > 0)
                        {
                            returnData.Code = -200;
                            returnData.Result = $"有重複領取序號,PRI_KEY:{data[i].PRI_KEY}";
                            return returnData.JsonSerializationt(true);
                        }
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
                    string 收支原因 = data[i].收支原因;
                    if (藥品名稱 != null) 藥品名稱 = 藥品名稱.Trim();
                    if (單位 != null) 單位 = 單位.Trim();
                    if (病歷號 != null) 病歷號 = 病歷號.Trim();
                    if (病人姓名 != null) 病人姓名 = 病人姓名.Trim();
                    if (操作人 != null) 操作人 = 操作人.Trim();
                    if (床號 != null) 床號 = 床號.Trim();
                    if (領藥號 != null) 領藥號 = 領藥號.Trim();
                    if (類別 != null) 類別 = 類別.Trim();
                    takeMedicineStackClass takeMedicineStack = new takeMedicineStackClass();
                    takeMedicineStack.GUID = Guid.NewGuid().ToString();
                    takeMedicineStack.序號 = DateTime.Now.ToDateTimeString_6();
                    takeMedicineStack.動作 = enum_交易記錄查詢動作.系統領藥;
                    takeMedicineStack.調劑台名稱 = 設備名稱;
                    takeMedicineStack.藥袋序號 = PRI_KEY;
                    takeMedicineStack.總異動量 = 總異動量.ToString();
                    takeMedicineStack.藥品碼 = 藥品碼;
                    takeMedicineStack.藥品名稱 = 藥品名稱;
                    takeMedicineStack.單位 = 單位;
                    takeMedicineStack.病歷號 = 病歷號;
                    takeMedicineStack.病人姓名 = 病人姓名;
                    takeMedicineStack.開方時間 = 開方時間;
                    takeMedicineStack.操作時間 = 操作時間;
                    takeMedicineStack.操作人 = 操作人;
                    takeMedicineStack.顏色 = 顏色;
                    if (data[i].功能類型 == "-1") takeMedicineStack.顏色 = Color.Black.ToColorString();
                    takeMedicineStack.類別 = 類別;
                    takeMedicineStack.床號 = 床號;
                    takeMedicineStack.領藥號 = 領藥號;
                    takeMedicineStack.收支原因 = 收支原因;
                    takeMedicineStackClasses.Add(takeMedicineStack);
                }
                if (data[i].功能類型 == "-3" || data[i].功能類型 == "-4")
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
                        if (list_trading.Count > 0)
                        {
                            returnData.Code = -200;
                            returnData.Result = $"有重複領取序號,PRI_KEY:{data[i].PRI_KEY}";
                            return returnData.JsonSerializationt(true);
                        }
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
                    string 收支原因 = data[i].收支原因;
                    string 效期 = data[i].效期;
                    string 批號 = data[i].批號;
                    if (效期.Check_Date_String() == false) 效期 = "";
                    if (收支原因.StringIsEmpty() == false) 收支原因 += "\n";
                    if (data[i].加退藥來源.StringIsEmpty() == false) 收支原因 += $"[加退藥來源]:{data[i].加退藥來源}\n";
                    if (data[i].護理站.StringIsEmpty() == false) 收支原因 += $"[護理站]:{data[i].護理站}";
                    if (藥品名稱 != null) 藥品名稱 = 藥品名稱.Trim();
                    if (單位 != null) 單位 = 單位.Trim();
                    if (病歷號 != null) 病歷號 = 病歷號.Trim();
                    if (病人姓名 != null) 病人姓名 = 病人姓名.Trim();
                    if (操作人 != null) 操作人 = 操作人.Trim();
                    if (床號 != null) 床號 = 床號.Trim();
                    if (領藥號 != null) 領藥號 = 領藥號.Trim();
                    if (類別 != null) 類別 = 類別.Trim();
                    takeMedicineStackClass takeMedicineStack = new takeMedicineStackClass();
                    takeMedicineStack.GUID = Guid.NewGuid().ToString();
                    takeMedicineStack.序號 = DateTime.Now.ToDateTimeString_6();
                    if (data[i].功能類型 == "-3") takeMedicineStack.動作 = enum_交易記錄查詢動作.系統加藥;
                    if (data[i].功能類型 == "-4") takeMedicineStack.動作 = enum_交易記錄查詢動作.系統退藥;
                    takeMedicineStack.調劑台名稱 = 設備名稱;
                    takeMedicineStack.藥袋序號 = PRI_KEY;
                    takeMedicineStack.總異動量 = 總異動量.ToString();
                    takeMedicineStack.藥品碼 = 藥品碼;
                    takeMedicineStack.藥品名稱 = 藥品名稱;
                    takeMedicineStack.單位 = 單位;
                    takeMedicineStack.病歷號 = 病歷號;
                    takeMedicineStack.病人姓名 = 病人姓名;
                    takeMedicineStack.開方時間 = 開方時間;
                    takeMedicineStack.操作時間 = 操作時間;
                    takeMedicineStack.操作人 = 操作人;
                    takeMedicineStack.顏色 = 顏色;
                    takeMedicineStack.顏色 = Color.Black.ToColorString();
                    takeMedicineStack.類別 = 類別;
                    takeMedicineStack.床號 = 床號;
                    takeMedicineStack.領藥號 = 領藥號;
                    takeMedicineStack.收支原因 = 收支原因;
                    takeMedicineStack.效期 = 效期;
                    takeMedicineStack.批號 = 批號;
                    takeMedicineStackClasses.Add(takeMedicineStack);
                }
                if (data[i].功能類型 == "5" || data[i].功能類型 == "-5")
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
                        if (list_trading.Count > 0)
                        {
                            returnData.Code = -200;
                            returnData.Result = $"有重複領取序號,PRI_KEY:{data[i].PRI_KEY}";
                            return returnData.JsonSerializationt(true);
                        }
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
                    string 收支原因 = data[i].收支原因;
                    string 效期 = data[i].效期;
                    string 批號 = data[i].批號;
                    if (效期.Check_Date_String() == false) 效期 = "";
                    if (藥品名稱 != null) 藥品名稱 = 藥品名稱.Trim();
                    if (單位 != null) 單位 = 單位.Trim();
                    if (病歷號 != null) 病歷號 = 病歷號.Trim();
                    if (病人姓名 != null) 病人姓名 = 病人姓名.Trim();
                    if (操作人 != null) 操作人 = 操作人.Trim();
                    if (床號 != null) 床號 = 床號.Trim();
                    if (領藥號 != null) 領藥號 = 領藥號.Trim();
                    if (類別 != null) 類別 = 類別.Trim();
                    takeMedicineStackClass takeMedicineStack = new takeMedicineStackClass();
                    takeMedicineStack.GUID = Guid.NewGuid().ToString();
                    takeMedicineStack.序號 = DateTime.Now.ToDateTimeString_6();
                    takeMedicineStack.動作 = enum_交易記錄查詢動作.系統入庫;
                    takeMedicineStack.調劑台名稱 = 設備名稱;
                    takeMedicineStack.藥袋序號 = PRI_KEY;
                    takeMedicineStack.總異動量 = 總異動量.ToString();
                    takeMedicineStack.藥品碼 = 藥品碼;
                    takeMedicineStack.藥品名稱 = 藥品名稱;
                    takeMedicineStack.單位 = 單位;
                    takeMedicineStack.病歷號 = 病歷號;
                    takeMedicineStack.病人姓名 = 病人姓名;
                    takeMedicineStack.開方時間 = 開方時間;
                    takeMedicineStack.操作時間 = 操作時間;
                    takeMedicineStack.操作人 = 操作人;
                    takeMedicineStack.顏色 = 顏色;
                    if (data[i].功能類型 == "-5") takeMedicineStack.顏色 = Color.Black.ToColorString();
                    takeMedicineStack.類別 = 類別;
                    takeMedicineStack.床號 = 床號;
                    takeMedicineStack.領藥號 = 領藥號;
                    takeMedicineStack.收支原因 = 收支原因;
                    takeMedicineStack.效期 = 效期;
                    takeMedicineStack.批號 = 批號;

                    takeMedicineStackClasses.Add(takeMedicineStack);
                }
                if (data[i].功能類型 == "-6" || data[i].功能類型 == "-7")
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
                        if (list_trading.Count > 0)
                        {
                            returnData.Code = -200;
                            returnData.Result = $"有重複領取序號,PRI_KEY:{data[i].PRI_KEY}";
                            return returnData.JsonSerializationt(true);
                        }
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
                    string 收支原因 = data[i].收支原因;
                    string 效期 = data[i].效期;
                    string 批號 = data[i].批號;
                    if (效期.Check_Date_String() == false) 效期 = "";
                    if (收支原因.StringIsEmpty() == false) 收支原因 += "\n";
                    if (data[i].來源庫別.StringIsEmpty() == false) 收支原因 += $"[來源庫別]:{data[i].來源庫別}";
                    if (藥品名稱 != null) 藥品名稱 = 藥品名稱.Trim();
                    if (單位 != null) 單位 = 單位.Trim();
                    if (病歷號 != null) 病歷號 = 病歷號.Trim();
                    if (病人姓名 != null) 病人姓名 = 病人姓名.Trim();
                    if (操作人 != null) 操作人 = 操作人.Trim();
                    if (床號 != null) 床號 = 床號.Trim();
                    if (領藥號 != null) 領藥號 = 領藥號.Trim();
                    if (類別 != null) 類別 = 類別.Trim();
                    takeMedicineStackClass takeMedicineStack = new takeMedicineStackClass();
                    takeMedicineStack.GUID = Guid.NewGuid().ToString();
                    takeMedicineStack.序號 = DateTime.Now.ToDateTimeString_6();
                    if (data[i].功能類型 == "-6") takeMedicineStack.動作 = enum_交易記錄查詢動作.系統撥入;
                    if (data[i].功能類型 == "-7") takeMedicineStack.動作 = enum_交易記錄查詢動作.系統撥出;
                    takeMedicineStack.調劑台名稱 = 設備名稱;
                    takeMedicineStack.藥袋序號 = PRI_KEY;
                    takeMedicineStack.總異動量 = 總異動量.ToString();
                    takeMedicineStack.藥品碼 = 藥品碼;
                    takeMedicineStack.藥品名稱 = 藥品名稱;
                    takeMedicineStack.單位 = 單位;
                    takeMedicineStack.病歷號 = 病歷號;
                    takeMedicineStack.病人姓名 = 病人姓名;
                    takeMedicineStack.開方時間 = 開方時間;
                    takeMedicineStack.操作時間 = 操作時間;
                    takeMedicineStack.操作人 = 操作人;
                    takeMedicineStack.顏色 = 顏色;
                    takeMedicineStack.顏色 = Color.Black.ToColorString();
                    takeMedicineStack.類別 = 類別;
                    takeMedicineStack.床號 = 床號;
                    takeMedicineStack.領藥號 = 領藥號;
                    takeMedicineStack.收支原因 = 收支原因;
                    takeMedicineStack.效期 = 效期;
                    takeMedicineStack.批號 = 批號;
                    takeMedicineStackClasses.Add(takeMedicineStack);
                }
                if (data[i].功能類型 == "-8" || data[i].功能類型 == "-9")
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
                        if (list_trading.Count > 0)
                        {
                            returnData.Code = -200;
                            returnData.Result = $"有重複領取序號,PRI_KEY:{data[i].PRI_KEY}";
                            return returnData.JsonSerializationt(true);
                        }
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
                    string 收支原因 = data[i].收支原因;
                    string 效期 = data[i].效期;
                    string 批號 = data[i].批號;
                    if (效期.Check_Date_String() == false) 效期 = "";
                    if (收支原因.StringIsEmpty() == false) 收支原因 += "\n";
                    if (data[i].來源庫別.StringIsEmpty() == false) 收支原因 += $"[來源庫別]:{data[i].來源庫別}";
                    if (藥品名稱 != null) 藥品名稱 = 藥品名稱.Trim();
                    if (單位 != null) 單位 = 單位.Trim();
                    if (病歷號 != null) 病歷號 = 病歷號.Trim();
                    if (病人姓名 != null) 病人姓名 = 病人姓名.Trim();
                    if (操作人 != null) 操作人 = 操作人.Trim();
                    if (床號 != null) 床號 = 床號.Trim();
                    if (領藥號 != null) 領藥號 = 領藥號.Trim();
                    if (類別 != null) 類別 = 類別.Trim();
                    takeMedicineStackClass takeMedicineStack = new takeMedicineStackClass();
                    takeMedicineStack.GUID = Guid.NewGuid().ToString();
                    takeMedicineStack.序號 = DateTime.Now.ToDateTimeString_6();
                    if (data[i].功能類型 == "-8") takeMedicineStack.動作 = enum_交易記錄查詢動作.系統調入;
                    if (data[i].功能類型 == "-9") takeMedicineStack.動作 = enum_交易記錄查詢動作.系統調出;
                    takeMedicineStack.調劑台名稱 = 設備名稱;
                    takeMedicineStack.藥袋序號 = PRI_KEY;
                    takeMedicineStack.總異動量 = 總異動量.ToString();
                    takeMedicineStack.藥品碼 = 藥品碼;
                    takeMedicineStack.藥品名稱 = 藥品名稱;
                    takeMedicineStack.單位 = 單位;
                    takeMedicineStack.病歷號 = 病歷號;
                    takeMedicineStack.病人姓名 = 病人姓名;
                    takeMedicineStack.開方時間 = 開方時間;
                    takeMedicineStack.操作時間 = 操作時間;
                    takeMedicineStack.操作人 = 操作人;
                    takeMedicineStack.顏色 = 顏色;
                    takeMedicineStack.顏色 = Color.Black.ToColorString();
                    takeMedicineStack.類別 = 類別;
                    takeMedicineStack.床號 = 床號;
                    takeMedicineStack.領藥號 = 領藥號;
                    takeMedicineStack.收支原因 = 收支原因;
                    takeMedicineStack.效期 = 效期;
                    takeMedicineStack.批號 = 批號;
                    takeMedicineStackClasses.Add(takeMedicineStack);
                }
            }
            Function_取藥堆疊資料_新增母資料(serverSettingClass, 設備名稱, takeMedicineStackClasses);
            returnData.Code = 200;
            returnData.TimeTaken = $"{myTimerBasic}";
            returnData.Result = $"OK,共新增<{takeMedicineStackClasses.Count}筆資料!>";
            return returnData.JsonSerializationt(true);
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
            SQLControl sQLControl_EPD1020_serialize = new SQLControl(IP, DataBaseName, "epd1020_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_EPD266_serialize = new SQLControl(IP, DataBaseName, "epd266_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RowsLED_serialize = new SQLControl(IP, DataBaseName, "rowsled_jsonstring", UserName, Password, Port, SSLMode);
            SQLControl sQLControl_RFID_Device_serialize = new SQLControl(IP, DataBaseName, "rfid_device_jsonstring", UserName, Password, Port, SSLMode);


            List<object[]> list_EPD583 = sQLControl_EPD583_serialize.GetAllRows(null);
            List<object[]> list_EPD1020 = sQLControl_EPD1020_serialize.GetAllRows(null);
            List<object[]> list_EPD266 = sQLControl_EPD266_serialize.GetAllRows(null);
            List<object[]> list_RowsLED = sQLControl_RowsLED_serialize.GetAllRows(null);
            List<object[]> list_RFID_Device = sQLControl_RFID_Device_serialize.GetAllRows(null);
            Console.WriteLine($"從SQL取得所有儲位資料,耗時{myTimer.ToString()}ms");
            List<DeviceBasic> deviceBasics = new List<DeviceBasic>();
            List<DeviceBasic> deviceBasics_buf = new List<DeviceBasic>();

            if (list_EPD1020.Count > 0) deviceBasics.LockAdd(DrawerMethod.GetAllDeviceBasic(list_EPD1020));
            if (list_EPD583.Count > 0) deviceBasics.LockAdd(DrawerMethod.GetAllDeviceBasic(list_EPD583));
            if (list_EPD266.Count > 0) deviceBasics.LockAdd(StorageMethod.GetAllDeviceBasic(list_EPD266));
            if (list_RowsLED.Count > 0) deviceBasics.LockAdd(RowsLEDMethod.GetAllDeviceBasic(list_RowsLED));
            if (list_RFID_Device.Count > 0) deviceBasics.LockAdd(RFIDMethod.GetAllDeviceBasic(list_RFID_Device));
            Console.WriteLine($"反編譯取得所有儲位資料,耗時{myTimer.ToString()}ms");
            deviceBasics_buf = (from value in deviceBasics
                                where value.Code.StringIsEmpty() == false
                                select value).ToList();

            return deviceBasics_buf;
        }
        private int Function_取得儲位庫存(string 藥品碼, List<Device> devices)
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
        private bool Function_取藥堆疊資料_新增母資料(ServerSettingClass serverSettingClass, string 設備名稱, List<takeMedicineStackClass> takeMedicineStackClasses)
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
            SQLControl sQLControl_sub_take_medicine_stack = new SQLControl(server, DataBaseName, "take_medicine_substack_new", UserName, Password, Port, SSLMode);

            //sQLControl_take_medicine_stack.DeleteByDefult(null, (int)enum_取藥堆疊母資料.調劑台名稱, 設備名稱);
            while (true)
            {
                List<object[]> list_temp = sQLControl_sub_take_medicine_stack.GetRowsByDefult(null, (int)enum_取藥堆疊子資料.調劑台名稱, 設備名稱);
                if (list_temp.Count == 0) break;
            }
            for (int i = 0; i < takeMedicineStackClasses.Count; i++)
            {
                if (takeMedicineStackClasses[i].GUID == null) takeMedicineStackClasses[i].GUID = Guid.NewGuid().ToString();
                if (takeMedicineStackClasses[i].GUID == "") takeMedicineStackClasses[i].GUID = Guid.NewGuid().ToString();
                takeMedicineStackClasses[i].調劑台名稱 = 設備名稱;
                if (takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.系統入庫) takeMedicineStackClasses[i].狀態 = enum_取藥堆疊母資料_狀態.新增效期;
                else if (takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.系統撥入) takeMedicineStackClasses[i].狀態 = enum_取藥堆疊母資料_狀態.新增效期;
                else if (takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.系統調入) takeMedicineStackClasses[i].狀態 = enum_取藥堆疊母資料_狀態.新增效期;
                else if (takeMedicineStackClasses[i].動作 == enum_交易記錄查詢動作.系統退藥) takeMedicineStackClasses[i].狀態 = enum_取藥堆疊母資料_狀態.新增效期;
                else takeMedicineStackClasses[i].狀態 = enum_取藥堆疊母資料_狀態.等待刷新;
            }
            List<object[]> list_add = takeMedicineStackClasses.ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>();
            for (int i = 0; i < list_add.Count; i++)
            {
                list_add[i][(int)enum_取藥堆疊母資料.動作] = list_add[i][(int)enum_取藥堆疊母資料.動作].GetEnumName();
                list_add[i][(int)enum_取藥堆疊母資料.狀態] = list_add[i][(int)enum_取藥堆疊母資料.狀態].GetEnumName();
            }
            sQLControl_take_medicine_stack.AddRows(null, list_add);

            return true;

        }
        private bool Function_取藥堆疊資料_新增母資料(ServerSettingClass serverSettingClass, string 設備名稱, takeMedicineStackClass takeMedicineStackClass)
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


            takeMedicineStackClass.調劑台名稱 = 設備名稱;
            if (takeMedicineStackClass.GUID == null) takeMedicineStackClass.GUID = Guid.NewGuid().ToString();
            else if (takeMedicineStackClass.GUID == "") takeMedicineStackClass.GUID = Guid.NewGuid().ToString();
            else if (takeMedicineStackClass.動作 == enum_交易記錄查詢動作.系統入庫) takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.新增效期;
            else if (takeMedicineStackClass.動作 == enum_交易記錄查詢動作.系統撥入) takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.新增效期;
            else if (takeMedicineStackClass.動作 == enum_交易記錄查詢動作.系統調入) takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.新增效期;
            else if (takeMedicineStackClass.動作 == enum_交易記錄查詢動作.系統退藥) takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.新增效期;
            else takeMedicineStackClass.狀態 = enum_取藥堆疊母資料_狀態.等待刷新;
            object[] value = takeMedicineStackClass.ClassToSQL<takeMedicineStackClass, enum_取藥堆疊母資料>();
            value[(int)enum_取藥堆疊母資料.動作] = value[(int)enum_取藥堆疊母資料.動作].GetEnumName();
            value[(int)enum_取藥堆疊母資料.狀態] = value[(int)enum_取藥堆疊母資料.狀態].GetEnumName();

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
            table.AddColumnList("序號", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("調劑台名稱", Table.StringType.VARCHAR, 50, Table.IndexType.None);
            table.AddColumnList("IP", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("操作人", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("藥師證字號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("動作", Table.StringType.VARCHAR, 30, Table.IndexType.None);
            table.AddColumnList("作業模式", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("藥袋序號", Table.StringType.VARCHAR, 200, Table.IndexType.None);
            table.AddColumnList("領藥號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
            table.AddColumnList("病房號", Table.StringType.VARCHAR, 20, Table.IndexType.None);
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
            table.AddColumnList("序號", Table.StringType.VARCHAR, 200, Table.IndexType.None);
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
