using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;

namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class sessionController : Controller
    {
        static private string DataBaseName = ConfigurationManager.AppSettings["person_page_database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        private SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_person_page = new SQLControl(IP, DataBaseName, "person_page", UserName, Password, Port, SSLMode);

        public class returnData
        {
            private List<object> _data = new List<object>();
            private int _code = 0;
            private string _result = "";

            public List<object> Data { get => _data; set => _data = value; }
            public int Code { get => _code; set => _code = value; }
            public string Result { get => _result; set => _result = value; }
        }
        public class class_output_login_session_data
        {
            [JsonPropertyName("GUID")]
            public string GUID { get; set; }
            [JsonPropertyName("ID")]
            public string ID { get; set; }
            [JsonPropertyName("Name")]
            public string Name { get; set; }
            [JsonPropertyName("Employer")]
            public string Employer { get; set; }
            [JsonPropertyName("loginTime")]
            public string loginTime { get; set; }
            [JsonPropertyName("verifyTime")]
            public string verifyTime { get; set; }

        }
        public class class_input_login_session_data
        {
            [JsonPropertyName("ID")]
            public string ID { get; set; }
            [JsonPropertyName("Password")]
            public string Password { get; set; }
        }



        public class class_input_check_session_data
        {
            [JsonPropertyName("ID")]
            public string ID { get; set; }
            [JsonPropertyName("loginTime")]
            public string loginTime { get; set; }
            [JsonPropertyName("check_sec")]
            public string check_sec { get; set; }
        }

        public class class_input_logout_session_data
        {
            [JsonPropertyName("ID")]
            public string ID { get; set; }
            [JsonPropertyName("loginTime")]
            public string loginTime { get; set; }
        }

        public enum enum_login_session
        {
            GUID,
            ID,
            Name,
            Employer,
            loginTime,
            verifyTime,
        }
        [HttpPost]
        public string Post([FromBody] class_input_login_session_data data)
        {
            returnData returnData = new returnData();
            List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
            List<object[]> list_person_page = sQLControl_person_page.GetAllRows(null);
            List<object[]> list_login_session_add = new List<object[]>();
            List<object[]> list_login_session_replace = new List<object[]>();

            list_person_page = list_person_page.GetRows((int)enum_person_page.ID, data.ID);
            if (list_person_page.Count == 0)
            {
                returnData.Code = -1;
                returnData.Result = "找無此帳號!";
                return returnData.JsonSerializationt();
            }
            if (list_person_page[0][(int)enum_person_page.密碼].ObjectToString() != data.Password)
            {
                returnData.Code = -2;
                returnData.Result = "密碼錯誤!";
                return returnData.JsonSerializationt();
            }
            list_login_session = list_login_session.GetRows((int)enum_login_session.ID, data.ID);
           
            object[] value = new object[new enum_login_session().GetLength()];
            if (list_login_session.Count == 0)
            {
                value = new object[new enum_login_session().GetLength()];
                value[(int)enum_login_session.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_login_session.ID] = list_person_page[0][(int)enum_person_page.ID].ObjectToString();
                value[(int)enum_login_session.Name] = list_person_page[0][(int)enum_person_page.姓名].ObjectToString();
                value[(int)enum_login_session.Employer] = list_person_page[0][(int)enum_person_page.單位].ObjectToString();
                value[(int)enum_login_session.verifyTime] = DateTime.Now.ToDateTimeString();
                value[(int)enum_login_session.loginTime] = DateTime.Now.ToDateTimeString();
                list_login_session_add.Add(value);
            }
            else
            {
                value = list_login_session[0];
                value[(int)enum_login_session.ID] = list_person_page[0][(int)enum_person_page.ID].ObjectToString();
                value[(int)enum_login_session.Name] = list_person_page[0][(int)enum_person_page.姓名].ObjectToString();
                value[(int)enum_login_session.Employer] = list_person_page[0][(int)enum_person_page.單位].ObjectToString();
                value[(int)enum_login_session.verifyTime] = DateTime.Now.ToDateTimeString();
                value[(int)enum_login_session.loginTime] = DateTime.Now.ToDateTimeString();
                list_login_session_replace.Add(value);
            }
            if (list_login_session_add.Count > 0) sQLControl_login_session.AddRows(null, list_login_session_add);
            if (list_login_session_replace.Count > 0) sQLControl_login_session.UpdateByDefulteExtra(null, list_login_session_replace);

            returnData.Code = 200;
            returnData.Result = "";
            class_output_login_session_data class_Output_Login_Session_Data = new class_output_login_session_data();
            class_Output_Login_Session_Data.ID = value[(int)enum_login_session.ID].ObjectToString();
            class_Output_Login_Session_Data.Name = value[(int)enum_login_session.Name].ObjectToString();
            class_Output_Login_Session_Data.Employer = value[(int)enum_login_session.Employer].ObjectToString();
            class_Output_Login_Session_Data.verifyTime = value[(int)enum_login_session.verifyTime].ObjectToString();
            class_Output_Login_Session_Data.loginTime = value[(int)enum_login_session.loginTime].ObjectToString();

            returnData.Data.Add(class_Output_Login_Session_Data);


            if (list_login_session.Count > 0)
            {
                returnData.Code = -3;
                returnData.Result = "已登入帳號,需登出!";
                return returnData.JsonSerializationt();
            }
            return returnData.JsonSerializationt();
        }

        [Route("check")]
        [HttpPost]
        public string check_Post([FromBody]class_input_check_session_data class_Input_Check_Session_Data)
        {
            returnData returnData = new returnData();
            List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
            list_login_session = list_login_session.GetRows((int)enum_login_session.ID, class_Input_Check_Session_Data.ID);
            if (list_login_session.Count == 0)
            {
                returnData.Result = "其他裝置登入,即將登出!";
                returnData.Code = -2;
                return returnData.JsonSerializationt();
            }
            DateTime loginTime = class_Input_Check_Session_Data.loginTime.StringToDateTime();
            DateTime verifyTime = list_login_session[0][(int)enum_login_session.verifyTime].StringToDateTime();
            
           
            TimeSpan timeDifference = DateTime.Now.Subtract(verifyTime);
            double secondsDifference = timeDifference.TotalSeconds;
            if (secondsDifference >= class_Input_Check_Session_Data.check_sec.StringToInt32())
            {
                returnData.Result = "驗證逾時,即將登出!";
                returnData.Code = -1;
                return returnData.JsonSerializationt();
            }
            list_login_session[0][(int)enum_login_session.verifyTime] = DateTime.Now.ToDateTimeString();
            sQLControl_login_session.UpdateByDefulteExtra(null, list_login_session);
            returnData.Result = "驗證成功!";
            returnData.Code = 200;
            return returnData.JsonSerializationt();
        }

        [Route("logout")]
        [HttpPost]
        public string logout_Post([FromBody] class_input_logout_session_data class_Input_Logout_Session_Data)
        {
            returnData returnData = new returnData();
            List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
            list_login_session = list_login_session.GetRows((int)enum_login_session.ID, class_Input_Logout_Session_Data.ID);
            if (list_login_session.Count > 0)
            {
                sQLControl_login_session.DeleteExtra(null, list_login_session);
            }
            returnData.Code = 200;
            returnData.Result = $"ID :{class_Input_Logout_Session_Data.ID} ,清除session成功!";
            return returnData.JsonSerializationt();
        }
    }
}
