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

namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
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
    }
}
