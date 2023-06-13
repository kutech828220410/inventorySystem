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
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class sessionController : Controller
    {
        static private string IP = ConfigurationManager.AppSettings["VM_Server"];
        static private string DataBaseName = ConfigurationManager.AppSettings["VM_DB"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];  
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        private SQLControl sQLControl_login_session = new SQLControl(IP, DataBaseName, "login_session", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_person_page = new SQLControl(IP, DataBaseName, "person_page", UserName, Password, Port, SSLMode);


        [Route("login")]
        [HttpPost]
        public string POST_login([FromBody] returnData returnData)
        {
            Check_Table();
            sessionClass data = HIS_DB_Lib.sessionClass.ObjToClass(returnData.Data);
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
            if (list_login_session.Count > 0)
            {
                returnData.Code = -3;
                returnData.Result = "已登入帳號,需登出!";
                return returnData.JsonSerializationt();
            }

            sessionClass sessionClass = new sessionClass();
            sessionClass.ID = value[(int)enum_login_session.ID].ObjectToString();
            sessionClass.Name = value[(int)enum_login_session.Name].ObjectToString();
            sessionClass.Employer = value[(int)enum_login_session.Employer].ObjectToString();
            sessionClass.verifyTime = value[(int)enum_login_session.verifyTime].ObjectToString();
            sessionClass.loginTime = value[(int)enum_login_session.loginTime].ObjectToString();

            returnData.Data = sessionClass;
         
            returnData.Code = 200;
            returnData.Result = "登入成功!";
            return returnData.JsonSerializationt();
        }

        [Route("check")]
        [HttpPost]
        public string POST_check([FromBody] returnData returnData)
        {
            Check_Table();

            sessionClass sessionClass = HIS_DB_Lib.sessionClass.ObjToClass(returnData.Data);
            List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
            list_login_session = list_login_session.GetRows((int)enum_login_session.ID, sessionClass.ID);
            if (list_login_session.Count == 0)
            {
                returnData.Result = "其他裝置登入,即將登出!";
                returnData.Code = -2;
                return returnData.JsonSerializationt();
            }
            DateTime loginTime = sessionClass.loginTime.StringToDateTime();
            DateTime verifyTime = list_login_session[0][(int)enum_login_session.verifyTime].StringToDateTime();
            
           
            TimeSpan timeDifference = DateTime.Now.Subtract(verifyTime);
            double secondsDifference = timeDifference.TotalSeconds;
            if (secondsDifference >= sessionClass.check_sec.StringToInt32())
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
        public string POST_logout([FromBody] returnData returnData)
        {
            Check_Table();
             sessionClass sessionClass = HIS_DB_Lib.sessionClass.ObjToClass(returnData.Data);
            List<object[]> list_login_session = sQLControl_login_session.GetAllRows(null);
            list_login_session = list_login_session.GetRows((int)enum_login_session.ID, sessionClass.ID);
            if (list_login_session.Count > 0)
            {
                sQLControl_login_session.DeleteExtra(null, list_login_session);
            }
            returnData.Code = 200;
            returnData.Result = $"ID :{sessionClass.ID} ,清除session成功!";
            return returnData.JsonSerializationt();
        }
        public void Check_Table()
        {
      
            if (sQLControl_login_session.IsTableCreat() == false)
            {
                Table table = new Table("login_session");
                table.AddColumnList("GUID", Table.StringType.VARCHAR, 50, Table.IndexType.PRIMARY);
                table.AddColumnList("ID", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("Name", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("Employer", Table.StringType.VARCHAR, 50, Table.IndexType.None);
                table.AddColumnList("loginTime", Table.DateType.DATETIME, 50, Table.IndexType.None);
                table.AddColumnList("verlifyTime", Table.DateType.DATETIME, 50, Table.IndexType.None);
                sQLControl_login_session.CreatTable(table);
            }
        }
    }
}
