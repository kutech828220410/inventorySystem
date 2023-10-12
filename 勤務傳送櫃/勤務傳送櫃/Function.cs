﻿using System;
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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using HIS_DB_Lib;
namespace 勤務傳送櫃
{
    public partial class Form1 : Form
    {
        public string Function_藥品碼檢查(string Code)
        {

            return Code;
        }
        public List<medClass> Function_搜尋Barcode(string barcode)
        {
            string url = $"{dBConfigClass.Api_URL}/api/MED_page/serch_by_BarCode";
            returnData returnData = new returnData(url);
            returnData.ServerName = dBConfigClass.Name;
            returnData.ServerType = enum_ServerSetting_Type.調劑台.GetEnumName();
            returnData.TableName = "medicine_page_cloud";
            returnData.Value = barcode;
            string json = returnData.ApiPostJson();
            if (returnData.ResultData == null)
            {
                MyMessageBox.ShowDialog("API連結失敗,請檢查網路或設定!");
                return new List<medClass>();
            }
            List<medClass> medClasses = returnData.ResultData.Data.ObjToListClass<medClass>();
            return medClasses;
        }
        private List<object[]> Function_醫令資料_API呼叫(string barcode)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<OrderClass> orderClasses = this.Function_醫令資料_API呼叫(dBConfigClass.OrderApiURL, barcode);
            List<object[]> list_value = orderClasses.ClassToSQL<OrderClass, enum_醫囑資料>();
       
            Console.Write($"醫令資料搜尋共<{list_value.Count}>筆,耗時{myTimer.ToString()}ms\n");
            return list_value;
        }
        private List<OrderClass> Function_醫令資料_API呼叫(string url, string barcode)
        {
            barcode = barcode.Replace("\r\n", "");
            barcode = Uri.EscapeDataString(barcode);
            List<OrderClass> orderClasses = new List<OrderClass>();
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string apitext = $"{url}{barcode}";

            Console.Write($"Call api : {apitext}\n");
            string jsonString = Basic.Net.WEBApiGet(apitext);
            Console.Write($"{jsonString}\n");
            Console.Write($"耗時 {myTimer.ToString()}ms\n");
            if (jsonString.StringIsEmpty())
            {
                this.voice.SpeakOnTask("網路異常");
                MyMessageBox.ShowDialog($"呼叫串接資料失敗!請檢查網路連線...");
                return orderClasses;
            }
            returnData returnData = jsonString.JsonDeserializet<returnData>();
            if (returnData == null)
            {
                this.voice.SpeakOnTask("藥單條碼錯誤");
                MyMessageBox.ShowDialog(jsonString);
                return new List<OrderClass>();
            }
            if (returnData.Code != 200)
            {
                MyMessageBox.ShowDialog($"{returnData.Result}");
                return new List<OrderClass>();

            }
            orderClasses = returnData.Data.ObjToListClass<OrderClass>();
            if (orderClasses == null)
            {
                Console.WriteLine($"串接資料傳回格式錯誤!");
                this.voice.SpeakOnTask("資料錯誤");
                orderClasses = new List<OrderClass>();

            }

            return orderClasses;
        }
    }
}
