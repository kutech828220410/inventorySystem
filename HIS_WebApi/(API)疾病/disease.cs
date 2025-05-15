using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HIS_DB_Lib;
using Basic;
using MySql.Data.MySqlClient;
using SQLUI;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using H_Pannel_lib;
using Org.BouncyCastle.Bcpg.OpenPgp;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using System.Text.RegularExpressions;
using System.Data;
using MyUI;
using System.IO;
using HIS_DB_Lib;
using System.Net;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Drawing;
using MyOffice;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using NPOI.SS.Formula.Functions;
using Google.Protobuf.WellKnownTypes;
using SkiaSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIS_WebApi._API_疾病
{
    [Route("api/[controller]")]
    [ApiController]
    public class disease : ControllerBase
    {
        static string API_Server = Method.GetServerAPI("Main", "網頁", "API01");
        static private MySqlSslMode SSLMode = MySqlSslMode.None;
        /// <summary>
        /// 初始化資料庫
        /// </summary>
        /// <remarks>
        /// {
        ///     
        /// }
        /// </remarks>
        /// <param name="returnData">共用傳遞資料結構</param>
        /// <returns></returns>
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(1, "", typeof(diseaseClass))]
        [HttpPost("init")]
        public string init()
        {
            try
            {
                return CheckCreatTable(null, new enum_disease());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("load")]
        public string load([FromBody] returnData returnData)
        {
            try
            {

                string filePath = (@"./ICD10-CM.csv");
                //byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                List<object[]> list_value = new List<object[]>();
                List<diseaseClass> diseaseClasses = new List<diseaseClass>();

                using (TextFieldParser parser = new TextFieldParser(filePath, Encoding.UTF8))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.HasFieldsEnclosedInQuotes = true;

                    bool isFirstLine = true;
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (isFirstLine)
                        {
                            isFirstLine = false; // 跳過標題列
                            continue;
                        }
                        list_value.Add(fields);
                    }
                }
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "disease", UserName, Password, Port, SSLMode);
                List<object[]> sqlData = sQLControl.GetAllRows(null);
                List<diseaseClass> diseases = sqlData.SQLToClass<diseaseClass, enum_disease>();
                Dictionary<string, List<diseaseClass>> dict = diseaseClass.ToDictByICD(diseases);
                for (int i = 0; i < list_value.Count; i++)
                {
                    diseaseClass diseaseClass = diseaseClass.GetByICD(dict, list_value[i][(int)enum_disease_EXCEL.code].ObjectToString()).FirstOrDefault();
                    if (diseaseClass != null) continue;
                    
                    diseaseClass add_diseaseClass = new diseaseClass
                    {
                        GUID = Guid.NewGuid().ToString(),
                        疾病代碼 = list_value[i][(int)enum_disease_EXCEL.code].ObjectToString(),
                        中文說明 = list_value[i][(int)enum_disease_EXCEL.中文名稱].ObjectToString(),
                        英文說明 = list_value[i][(int)enum_disease_EXCEL.英文名稱].ObjectToString()
                    };
                    diseaseClasses.Add(add_diseaseClass);
                }
                init();
                
                List<object[]> add = diseaseClasses.ClassToSQL<diseaseClass, enum_disease>();
                //sQLControl.AddRows(null,add);
                
                returnData.Data = diseaseClasses;
                return returnData.JsonSerializationt(true);
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("load_txt")]
        public string load_txt([FromBody] returnData returnData)
        {
            try
            {
                string loadText = Basic.MyFileStream.LoadFileAllText(@"./ICD10-CM.txt", "utf-8");
                List<diseaseClass> list_value = loadText.JsonDeserializet<List<diseaseClass>>();

                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");
                SQLControl sQLControl = new SQLControl(Server, DB, "disease", UserName, Password, Port, SSLMode);
                List<object[]> sqlData = sQLControl.GetAllRows(null);
                List<diseaseClass> diseases = sqlData.SQLToClass<diseaseClass, enum_disease>();
                Dictionary<string, List<diseaseClass>> dict = diseaseClass.ToDictByICD(diseases);
                List<diseaseClass> diseaseClasses = new List<diseaseClass>();

                for (int i = 0; i < list_value.Count; i++)
                {
                    diseaseClass diseaseClass = diseaseClass.GetByICD(dict, list_value[i].疾病代碼).FirstOrDefault();
                    if (diseaseClass != null) continue;

                    diseaseClass add_diseaseClass = new diseaseClass
                    {
                        GUID = Guid.NewGuid().ToString(),
                        疾病代碼 = list_value[i].疾病代碼,
                        中文說明 = list_value[i].中文說明,
                        英文說明 = list_value[i].英文說明
                    };
                    diseaseClasses.Add(add_diseaseClass);
                }

                List<object[]> add = diseaseClasses.ClassToSQL<diseaseClass, enum_disease>();
                sQLControl.AddRows(null, add);

                returnData.Data = diseaseClasses;
                return returnData.JsonSerializationt(true);


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("get_by_ICD")]
        public string get_by_ICD([FromBody] returnData returnData)
        {
            MyTimerBasic myTimerBasic = new MyTimerBasic();
            returnData.Method = "get_by_ICD";
            try
            {
                returnData.RequestUrl = Method.GetRequestPath(HttpContext, includeQuery: false);

                if (returnData.ValueAry.Count != 1)
                {
                    returnData.Code = -200;
                    returnData.Result = $"returnData.ValueAry應該為[\"疾病代碼\"]";
                    return returnData.JsonSerializationt();
                }
                string 疾病代碼 = returnData.ValueAry[0];
                string[] 疾病代碼_Array = 疾病代碼.Split(";");
                (string Server, string DB, string UserName, string Password, uint Port) = HIS_WebApi.Method.GetServerInfo("Main", "網頁", "VM端");

                string TableName = "disease";
                SQLControl sQLControl = new SQLControl(Server, DB, TableName, UserName, Password, Port, SSLMode);                            
                //string command = $"SELECT * FROM {DB}.{TableName} WHERE 疾病代碼  = '{疾病代碼}';"; 
                string command = $"SELECT * FROM {DB}.{TableName} WHERE"; 

                for (int i = 0; i < 疾病代碼_Array.Length; i++)
                {
                    if (i == 0)
                    {
                        command += $" 疾病代碼  = '{疾病代碼_Array[i]}'";
                    }
                    else if(i == 疾病代碼_Array.Length-1)
                    {
                        command += $" OR 疾病代碼 = '{疾病代碼_Array[i]}';";
                    }
                    else
                    {
                        command += $" OR 疾病代碼 = '{疾病代碼_Array[i]}'";
                    }
                }
                DataTable dataTable = sQLControl.WtrteCommandAndExecuteReader(command);           
                List<object[]> list_diseaseClass = dataTable.DataTableToRowList();
                List<diseaseClass> diseaseClasses = list_diseaseClass.SQLToClass<diseaseClass, enum_disease>();
                if (diseaseClasses == null) diseaseClasses = new List<diseaseClass>();
                if (diseaseClasses.Count == 0)
                {

                    string icd10_url = $"https://clinicaltables.nlm.nih.gov/api/icd10cm/v3/search?df=code,name&terms={疾病代碼}";

                    string icd10_json = Basic.Net.WEBApiGet(icd10_url);
                    CodeDescription iCD10Data = CodeDescription.ParseCodeDescription(icd10_json);
                    diseaseClass diseaseClass = new diseaseClass();
                    diseaseClass.GUID = Guid.NewGuid().ToString();
                    diseaseClass.疾病代碼 = iCD10Data.Code;
                    diseaseClass.英文說明 = iCD10Data.Description;

                    diseaseClasses.Add(diseaseClass);
                    List<object[]> add = diseaseClasses.ClassToSQL<diseaseClass, enum_disease>();
                    sQLControl.AddRows(null, add);
                    returnData.Data = diseaseClasses;

                }
                else
                {
                    returnData.Data = diseaseClasses;
                }
                returnData.Code = 200;
                returnData.TimeTaken = $"{myTimerBasic}";
                returnData.Result = $"取得疾病資料共<{diseaseClasses.Count}>筆";
                return returnData.JsonSerializationt(true);
            }
            catch (Exception e)
            {
                returnData.Code = -200;
                returnData.Result = e.Message;
                return returnData.JsonSerializationt();
            }
        }

        private string CheckCreatTable(string tableName, System.Enum Enum)
        {
            if (tableName == null)
            {
                tableName = Enum.GetEnumDescription();
            }
            SQLUI.Table table = new SQLUI.Table(tableName);
            List<sys_serverSettingClass> sys_serverSettingClasses = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClasses = sys_serverSettingClasses.MyFind("Main", "網頁", "藥檔資料");
            if (sys_serverSettingClasses.Count == 0)
            {
                return $"找無Server資料!";
            }
            table = MethodClass.CheckCreatTable(sys_serverSettingClasses[0], Enum);

            return table.JsonSerializationt(true);
        }

        public class CodeDescription
        {
            public string Code { get; set; }
            public string Description { get; set; }

            public CodeDescription() { }

            public CodeDescription(string code, string description)
            {
                Code = code;
                Description = description;
            }

            // 若來源為 string[] 陣列
            public CodeDescription(string[] array)
            {
                if (array?.Length == 2)
                {
                    Code = array[0];
                    Description = array[1];
                }
            }

            public static CodeDescription ParseCodeDescription(string json)
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;
                    var lastArray = root[3]; // 第四個元素是 [["R05.9", "Cough, unspecified"]]

                    if (lastArray.GetArrayLength() > 0)
                    {
                        var item = lastArray[0];
                        return new CodeDescription
                        {
                            Code = item[0].GetString(),
                            Description = item[1].GetString()
                        };
                    }
                }
                return null;
            }
        }
    }
}
