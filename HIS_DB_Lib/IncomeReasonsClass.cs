using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Reflection;
using SQLUI;
namespace HIS_DB_Lib
{
    [EnumDescription("incomeReasons")]
    public enum enum_incomeReasons
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("類別,VARCHAR,50,NONE")]
        類別,
        [Description("原因,TEXT,50,NONE")]
        原因,
        [Description("新增時間,DATETIME,50,NONE")]
        新增時間
    }
    public class incomeReasonsClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("TYPE")]
        public string 類別 { get; set; }
        [JsonPropertyName("RSN")]
        public string 原因 { get; set; }
        [JsonPropertyName("CT_TIME")]
        public string 新增時間 { get; set; }

        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/incomeReasons/init";

            returnData returnData = new returnData();
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            List<SQLUI.Table> tables = json_out.JsonDeserializet<List<SQLUI.Table>>();
            SQLUI.Table table = SQLUI.TableMethod.GetTable(tables, new enum_medPic());
            return table;
        }
        static public List<incomeReasonsClass> get_all(string API_Server)
        {

            string url = $"{API_Server}/api/incomeReasons/data";

            returnData returnData = new returnData();

            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return null;
            }
            if (returnData_out.Data == null)
            {
                return null;
            }
            Console.WriteLine($"{returnData_out}");

            List<incomeReasonsClass> incomeReasonsClasses = returnData_out.Data.ObjToClass<List<incomeReasonsClass>>();
            return incomeReasonsClasses;
        }
        static public void add(string API_Server, incomeReasonsClass incomeReasonsClass)
        {
            List<incomeReasonsClass> incomeReasonsClasses = new List<incomeReasonsClass>();
            incomeReasonsClasses.Add(incomeReasonsClass);
            add(API_Server, incomeReasonsClasses);
        }
        static public void add(string API_Server, List<incomeReasonsClass>  incomeReasonsClasses)
        {

            string url = $"{API_Server}/api/incomeReasons/add";
            returnData returnData = new returnData();
            returnData.Data = incomeReasonsClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return;
            }
            if (returnData_out.Data == null)
            {
                return;
            }
            Console.WriteLine($"{returnData_out}");

        }
        static public void update(string API_Server, incomeReasonsClass incomeReasonsClass)
        {
            List<incomeReasonsClass> incomeReasonsClasses = new List<incomeReasonsClass>();
            incomeReasonsClasses.Add(incomeReasonsClass);
            update(API_Server, incomeReasonsClasses);
        }
        static public void update(string API_Server, List<incomeReasonsClass> incomeReasonsClasses)
        {

            string url = $"{API_Server}/api/incomeReasons/update";
            returnData returnData = new returnData();
            returnData.Data = incomeReasonsClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return;
            }
            if (returnData_out.Data == null)
            {
                return;
            }
            Console.WriteLine($"{returnData_out}");

        }
        static public void delete(string API_Server, incomeReasonsClass incomeReasonsClass)
        {
            List<incomeReasonsClass> incomeReasonsClasses = new List<incomeReasonsClass>();
            incomeReasonsClasses.Add(incomeReasonsClass);
            delete(API_Server, incomeReasonsClasses);
        }
        static public void delete(string API_Server, List<incomeReasonsClass> incomeReasonsClasses)
        {

            string url = $"{API_Server}/api/incomeReasons/delete";
            returnData returnData = new returnData();
            returnData.Data = incomeReasonsClasses;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            returnData returnData_out = json_out.JsonDeserializet<returnData>();
            if (returnData_out == null)
            {
                return;
            }
            if (returnData_out.Data == null)
            {
                return;
            }
            Console.WriteLine($"{returnData_out}");

        }
    }
}
