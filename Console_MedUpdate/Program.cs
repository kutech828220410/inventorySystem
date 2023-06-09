﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLUI;
using HIS_DB_Lib;
using H_Pannel_lib;
using Basic;
using MyUI;
namespace Console_MedUpdate
{
    class Program
    {
        static private string DBConfigFileName = "C://Console_MedUpdate//DBConfig.txt";
  
        public class DBConfigClass
        {
            private SQL_DataGridView.ConnentionClass dB_Basic = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_Medicine_Cloud = new SQL_DataGridView.ConnentionClass();
            private string medApiURL = "";

            public SQL_DataGridView.ConnentionClass DB_Basic { get => dB_Basic; set => dB_Basic = value; } 
            public SQL_DataGridView.ConnentionClass DB_Medicine_Cloud { get => dB_Medicine_Cloud; set => dB_Medicine_Cloud = value; }         
            public string MedApiURL { get => medApiURL; set => medApiURL = value; }

           
        }
        static DBConfigClass dBConfigClass = new DBConfigClass();
        static public bool LoadDBConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($"{DBConfigFileName}");
            Console.WriteLine($"路徑 : {DBConfigFileName} 開始讀取...." );
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
            if (LoadDBConfig() == false)
            {
                Console.WriteLine("按下任意鍵以退出程式...");
                Console.ReadKey(); // 等待使用者按下任意鍵

                // 退出程式
                Environment.Exit(0);
            }
            try
            {
                Console.WriteLine($"{dBConfigClass.JsonSerializationt(true)}");
                SQLControl sQLControlw_藥品資料_藥檔資料 = new SQLControl(
               dBConfigClass.DB_Basic.IP,
               dBConfigClass.DB_Basic.DataBaseName,
               dBConfigClass.DB_Basic.UserName,
               dBConfigClass.DB_Basic.Password,
               dBConfigClass.DB_Basic.Port
               );
                sQLControlw_藥品資料_藥檔資料.TableName = dBConfigClass.DB_Basic.TableName;
                SQLControl sQLControlw_雲端藥檔 = new SQLControl(
                 dBConfigClass.DB_Basic.IP,
                    dBConfigClass.DB_Medicine_Cloud.DataBaseName,
                    dBConfigClass.DB_Medicine_Cloud.UserName,
                    dBConfigClass.DB_Medicine_Cloud.Password,
                    dBConfigClass.DB_Medicine_Cloud.Port
                    );
                sQLControlw_雲端藥檔.TableName = dBConfigClass.DB_Medicine_Cloud.TableName;

                List<object[]> list_本地藥檔 = sQLControlw_藥品資料_藥檔資料.GetAllRows(null);
                List<object[]> list_雲端藥檔 = sQLControlw_雲端藥檔.GetAllRows(null);
                List<object[]> list_雲端藥檔_buf = new List<object[]>();
                List<object[]> list_本地藥檔_replace = new List<object[]>();
        
                string url = dBConfigClass.MedApiURL;
                if (!url.StringIsEmpty())
                {
                    MyTimer myTimer = new MyTimer();
                    myTimer.StartTickTime(50000);
                    string response = Basic.Net.WEBApiGet($"{url}?Code");
                    if (response == "OK")
                    {
                        Console.WriteLine($"HIS填入成功! , response:{response},耗時{myTimer.ToString()}ms");
                    }
                    else
                    {
                        Console.WriteLine($"HIS填入失敗! , response:{response},耗時{myTimer.ToString()}ms");
                    }
                }
                Console.WriteLine($"藥檔共<{list_本地藥檔.Count}>筆資料‧");
                for (int i = 0; i < list_本地藥檔.Count; i++)
                {
                    string 藥品碼 = list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                    list_雲端藥檔_buf = list_雲端藥檔.GetRows((int)enum_雲端藥檔.藥品碼, 藥品碼);
                    if (list_雲端藥檔_buf.Count > 0)
                    {
                        if (i == 74)
                        {
                            
                        }
                        bool replace = false;
                        if (list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString() != list_雲端藥檔_buf[0][(int)enum_雲端藥檔.藥品名稱].ObjectToString()) replace = true;
                        if (list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品中文名稱].ObjectToString() != list_雲端藥檔_buf[0][(int)enum_雲端藥檔.中文名稱].ObjectToString()) replace = true;
                        if (list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString() != list_雲端藥檔_buf[0][(int)enum_雲端藥檔.包裝單位].ObjectToString()) replace = true;
                        if (list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品條碼].ObjectToString() != list_雲端藥檔_buf[0][(int)enum_雲端藥檔.藥品條碼1].ObjectToString()) replace = true;
                        if (list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString() != list_雲端藥檔_buf[0][(int)enum_雲端藥檔.警訊藥品].ObjectToString()) replace = true;
                        if (list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.管制級別].ObjectToString() != list_雲端藥檔_buf[0][(int)enum_雲端藥檔.管制級別].ObjectToString()) replace = true;

                        list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品名稱] = list_雲端藥檔_buf[0][(int)enum_雲端藥檔.藥品名稱];
                        list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品學名] = list_雲端藥檔_buf[0][(int)enum_雲端藥檔.藥品學名];
                        list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品中文名稱] = list_雲端藥檔_buf[0][(int)enum_雲端藥檔.中文名稱];
                        list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.包裝單位] = list_雲端藥檔_buf[0][(int)enum_雲端藥檔.包裝單位];
                        list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.藥品條碼] = list_雲端藥檔_buf[0][(int)enum_雲端藥檔.藥品條碼1];
                        list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.警訊藥品] = list_雲端藥檔_buf[0][(int)enum_雲端藥檔.警訊藥品];
                        list_本地藥檔[i][(int)enum_藥品資料_藥檔資料.管制級別] = list_雲端藥檔_buf[0][(int)enum_雲端藥檔.管制級別];
                        if (replace)
                        {
                            list_本地藥檔_replace.Add(list_本地藥檔[i]);
                        }

                    }
                }
                Console.WriteLine($"須更新<{list_本地藥檔_replace.Count}>筆資料‧");

                sQLControlw_藥品資料_藥檔資料.UpdateByDefulteExtra(null, list_本地藥檔_replace);
                Console.WriteLine($"完成!");
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine("按下任意鍵以退出程式...");
                Console.ReadKey(); // 等待使用者按下任意鍵

                // 退出程式
                Environment.Exit(0);
            }
        }
    }
}
