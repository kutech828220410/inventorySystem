using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLUI;
using HIS_DB_Lib;
using H_Pannel_lib;
using Basic;
using System.IO;
using System.Reflection;

namespace batch_ds_stockRecord
{
    class Program
    {
        static public string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static private string DBConfigFileName = $"{currentDirectory}//DBConfig.txt";
        public class DBConfigClass
        {
            private string name = "";
            private string api_Server = "http://127.0.0.1:4433";
            public string Api_Server { get => api_Server; set => api_Server = value; }
            public string Name { get => name; set => name = value; }
        }
        static DBConfigClass dBConfigClass = new DBConfigClass();

        static public bool LoadDBConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($"{DBConfigFileName}");
            Console.WriteLine($"路徑 : {DBConfigFileName} 開始讀取");
            Console.WriteLine($"-------------------------------------------------------------------------");
            if (jsonstr.StringIsEmpty())
            {
                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(new DBConfigClass(), true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{DBConfigFileName}", list_jsonstring))
                {
                    Console.WriteLine($"建立{DBConfigFileName}檔案失敗!");
                    return false;
                }
                Console.WriteLine($"未建立參數文件!請至子目錄設定{DBConfigFileName}");
                return false;
            }
            else
            {
                dBConfigClass = Basic.Net.JsonDeserializet<DBConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(dBConfigClass, true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{DBConfigFileName}", list_jsonstring))
                {
                    Console.WriteLine($"建立{DBConfigFileName}檔案失敗!");
                    return false;
                }

            }
            return true;

        }

        static void Main(string[] args)
        {
            string e_msg = "\n";
            try
            {
               
                LoadDBConfig();
                List<medClassBasic> medClassBasics = new List<medClassBasic>();
                List<medClassBasic> medClassBasics_藥庫 = new List<medClassBasic>();
                List<medClassBasic> medClassBasics_藥局 = new List<medClassBasic>();
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                string API_Server = $"{dBConfigClass.Api_Server}";
                string json_in = "";
                string json_out = "";
                returnData returnData = new returnData();
                returnData.ServerName = dBConfigClass.Name;
                returnData.ServerType = "藥庫";
                returnData.TableName = "medicine_page_phar";
                json_in = returnData.JsonSerializationt();
                json_out = Basic.Net.WEBApiPostJson($"{API_Server}/api/med_page/get_by_apiserver", json_in);
                returnData = json_out.JsonDeserializet<returnData>();
                if (returnData == null)
                {
                    e_msg += $"[{API_Server}/api/med_page/get_by_apiserver] 取得庫存失敗";
                    return;   
                }
                e_msg += $"[{API_Server}/api/med_page/get_by_apiserver] 取得庫存 ,Result:{returnData.Result}\n";
                if (returnData.Code != 200) return;
           
                medClassBasics = returnData.Data.ObjToClass<List<medClassBasic>>();
                if (medClassBasics == null)
                {
                    e_msg += $"[{API_Server}/api/med_page/get_by_apiserver] 取得庫存失敗";
                    return;
                }
                Console.WriteLine($"取得庫存完成,{myTimerBasic}");
                Console.WriteLine($"-------------------------------------------------------------------------");

                //寫入庫存紀錄(藥庫)
                foreach (medClassBasic temp in medClassBasics)
                {
                    medClassBasic medClassBasic = new medClassBasic();
                    medClassBasic.藥品碼 = temp.藥品碼;
                    medClassBasic.藥品名稱 = temp.藥品名稱;
                    medClassBasic.總庫存 = temp.藥庫庫存;
                    medClassBasics_藥庫.Add(medClassBasic);
                }
                returnData.ServerName = dBConfigClass.Name;
                returnData.ServerType = "藥庫";
                returnData.Data = medClassBasics_藥庫;
                returnData.ValueAry = new List<string> { $"藥庫", $"{returnData.ServerName}" };
                json_in = returnData.JsonSerializationt();
                json_out = Basic.Net.WEBApiPostJson($"{API_Server}/api/stockRecord/add_record", json_in);
                returnData = json_out.JsonDeserializet<returnData>();
                if (returnData == null)
                {
                    e_msg += $"[{API_Server}/api/stockRecord/add_record] 寫入庫存紀錄(藥庫)";
                    return;
                }
                e_msg += $"[{API_Server}/api/stockRecord/add_record] 寫入庫存紀錄(藥庫) ,Result:{returnData.Result}\n";
                if (returnData.Code != 200) return;
                Console.WriteLine($"寫入庫存紀錄(藥庫)完成,{myTimerBasic}");
                Console.WriteLine($"-------------------------------------------------------------------------");
                //寫入庫存紀錄(藥局)
                foreach (medClassBasic temp in medClassBasics)
                {
                    medClassBasic medClassBasic = new medClassBasic();
                    medClassBasic.藥品碼 = temp.藥品碼;
                    medClassBasic.藥品名稱 = temp.藥品名稱;
                    medClassBasic.總庫存 = temp.藥局庫存;
                    medClassBasics_藥局.Add(medClassBasic);
                }
                returnData.ServerName = dBConfigClass.Name;
                returnData.ServerType = "藥庫";
                returnData.Data = medClassBasics_藥局;
                returnData.ValueAry = new List<string> { $"藥局", $"{returnData.ServerName}" };
                json_in = returnData.JsonSerializationt();
                json_out = Basic.Net.WEBApiPostJson($"{API_Server}/api/stockRecord/add_record", json_in);
                returnData = json_out.JsonDeserializet<returnData>();
                if (returnData == null)
                {
                    e_msg += $"[{API_Server}/api/stockRecord/add_record] 寫入庫存紀錄(藥局)";
                    return;
                }
                e_msg += $"[{API_Server}/api/stockRecord/add_record] 寫入庫存紀錄(藥局) ,Result:{returnData.Result}\n";
                if (returnData.Code != 200) return;
               
                Console.WriteLine($"寫入庫存紀錄(藥局)完成,{myTimerBasic}");
                Console.WriteLine($"-------------------------------------------------------------------------");
                e_msg += "【OK】";
                System.Threading.Thread.Sleep(2000);

            }
            catch (Exception e)
            {
                e_msg += $"Exception {e.Message}";
            }
            finally
            {
                Logger.LogAddLine("batch_ds_stockRecord");
                Logger.Log("batch_ds_stockRecord", $"{e_msg}");
                Logger.LogAddLine("batch_ds_stockRecord");
            }


        }
    }
}
