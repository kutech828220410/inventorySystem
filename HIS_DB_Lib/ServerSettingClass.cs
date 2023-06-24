using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLUI;
using Basic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;
using HIS_DB_Lib;

namespace HIS_DB_Lib
{
    public enum enum_ServerSetting
    {
        GUID,
        單位,
        設備名稱,
        類別,
        程式類別,
        內容,
        Server,
        Port,
        DBName,
        TableName,
        User,
        Password,
        Value,
    }
    public enum enum_ServerSetting_Type
    {
        網頁,
        調劑台,
        藥庫,
    }
    public enum enum_ServerSetting_ProgramType
    {
        SQLServer,
        API,
        WEB,
    }
    public enum enum_ServerSetting_網頁
    {
        人員資料,
        API01,
        API02,
        API_Session,
        Website,
    }

    public enum enum_ServerSetting_調劑台
    {
        一般資料,
        人員資料,
        藥檔資料,
        醫囑資料,
        API_本地端,
        API_VM端,
        API_藥檔資料,
        API_儲位資料,
        API_交易紀錄資料,
        API01,
        API02,
        Order_API,
        Med_API,
        Website
    }
    public enum enum_ServerSetting_藥庫
    {
        一般資料,
        人員資料,
        藥檔資料,
        批次過帳資料,
        API_本地端,
        API_VM端,
        API_藥檔資料,
        API_儲位資料,
        API_交易紀錄資料,
        API01,
        API02,
        Website
    }
    public static class ServerSettingClassMethod
    {
        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, string Type, string Content)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == Type
                                                                 where value.名稱 == Name
                                                                 where value.內容 == Content
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }
        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, string Type)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == Type
                                                                 where value.名稱 == Name
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }

        public static ServerSettingClass MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, enum_ServerSetting_Type _enum_ServerSetting_Type, enum_ServerSetting_網頁 enum_ServerSetting_網頁)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 where value.名稱 == Name
                                                                 where value.內容 == enum_ServerSetting_網頁.GetEnumName()
                                                                 select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                return serverSettingClasses_buf[0];
            }
            else
            {
                return null;
            }
        }

        public static ServerSettingClass MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, enum_ServerSetting_Type _enum_ServerSetting_Type, enum_ServerSetting_調劑台 _enum_ServerSetting_調劑台)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 where value.名稱 == Name
                                                                 where value.內容 == _enum_ServerSetting_調劑台.GetEnumName()
                                                                 select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                return serverSettingClasses_buf[0];
            }
            else
            {
                return null;
            }
        }
        public static ServerSettingClass MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, enum_ServerSetting_Type _enum_ServerSetting_Type, enum_ServerSetting_藥庫 _enum_ServerSetting_藥庫)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 where value.名稱 == Name
                                                                 where value.內容 == _enum_ServerSetting_藥庫.GetEnumName()
                                                                 select value).ToList();
            if (serverSettingClasses_buf.Count > 0)
            {
                return serverSettingClasses_buf[0];
            }
            else
            {
                return null;
            }
        }

        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, enum_ServerSetting_Type _enum_ServerSetting_Type)
        {
            if (serverSettingClasses == null) return new List<ServerSettingClass>();
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.名稱 == Name
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }
        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, enum_ServerSetting_Type _enum_ServerSetting_Type , enum_ServerSetting_網頁 _enum_ServerSetting_網頁)
        {
            if (serverSettingClasses == null) return new List<ServerSettingClass>();
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 where value.內容 == _enum_ServerSetting_網頁.GetEnumName()
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }
        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, enum_ServerSetting_Type _enum_ServerSetting_Type, enum_ServerSetting_藥庫 _enum_ServerSetting_藥庫)
        {
            if (serverSettingClasses == null) return new List<ServerSettingClass>();
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 where value.內容 == _enum_ServerSetting_藥庫.GetEnumName()
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }
        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, enum_ServerSetting_Type _enum_ServerSetting_Type)
        {
            if (serverSettingClasses == null) return new List<ServerSettingClass>();
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }
        public static List<ServerSettingClass> WebApiGet(string url)
        {
            string jsonstring = Basic.Net.WEBApiGet(url);
            if (jsonstring.StringIsEmpty()) return null;
            Console.WriteLine(jsonstring);
            returnData returnData = jsonstring.JsonDeserializet<returnData>();

            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.ObjToListClass(returnData.Data);
            return serverSettingClasses;
        }

    }
    public class ServerSettingClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("Employer")]
        public string 單位 { get; set; }    
        [JsonPropertyName("name")]
        public string 名稱 { get; set; }
        [JsonPropertyName("type")]
        public string 類別 { get; set; }
        [JsonPropertyName("porgram_type")]
        public string 程式類別 { get; set; }
        [JsonPropertyName("content")]
        public string 內容 { get; set; }
        [JsonPropertyName("server")]
        public string Server { get; set; }
        [JsonPropertyName("Port")]
        public string Port { get; set; }
        [JsonPropertyName("DBName")]
        public string DBName { get; set; }
        [JsonPropertyName("tableName")]
        public string TableName { get; set; }
        [JsonPropertyName("user")]
        public string User { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public ServerSettingClass()
        {

        }
        public ServerSettingClass(string 名稱, enum_ServerSetting_Type enum_ServerSetting_Type, enum_ServerSetting_ProgramType enum_ServerSetting_ProgramType, enum_ServerSetting_網頁 enum_ServerSetting_網頁
          , string Server, string Port, string DBName, string TableName, string User, string Password)
        {
            this.名稱 = 名稱;
            this.類別 = enum_ServerSetting_Type.GetEnumName();
            this.程式類別 = enum_ServerSetting_ProgramType.GetEnumName();
            this.內容 = enum_ServerSetting_網頁.GetEnumName();
            this.Server = Server;
            this.Port = Port;
            this.DBName = DBName;
            this.TableName = TableName;
            this.User = User;
            this.Password = Password;
        }
        public ServerSettingClass(string 名稱, enum_ServerSetting_Type enum_ServerSetting_Type , enum_ServerSetting_ProgramType enum_ServerSetting_ProgramType, enum_ServerSetting_調劑台 _enum_ServerSetting_調劑台
            , string Server, string Port, string DBName, string TableName, string User, string Password)
        {
            this.名稱 = 名稱;
            this.類別 = enum_ServerSetting_Type.GetEnumName();
            this.程式類別 = enum_ServerSetting_ProgramType.GetEnumName();
            this.內容 = _enum_ServerSetting_調劑台.GetEnumName();
            this.Server = Server;
            this.Port = Port;
            this.DBName = DBName;
            this.TableName = TableName;
            this.User = User;
            this.Password = Password;
        }
        public ServerSettingClass(string 名稱, enum_ServerSetting_Type enum_ServerSetting_Type, enum_ServerSetting_ProgramType enum_ServerSetting_ProgramType, enum_ServerSetting_藥庫 _enum_ServerSetting_藥庫
            , string Server, string Port, string DBName, string TableName, string User, string Password)
        {
            this.名稱 = 名稱;
            this.類別 = enum_ServerSetting_Type.GetEnumName();
            this.程式類別 = enum_ServerSetting_ProgramType.GetEnumName();
            this.內容 = _enum_ServerSetting_藥庫.GetEnumName();
            this.Server = Server;
            this.Port = Port;
            this.DBName = DBName;
            this.TableName = TableName;
            this.User = User;
            this.Password = Password;
        }
        static public object[] ClassToSQL(ServerSettingClass _class)
        {
            object[] value = new object[new enum_ServerSetting().GetLength()];
            value[(int)enum_ServerSetting.GUID] = _class.GUID;
            value[(int)enum_ServerSetting.單位] = _class.單位;
            value[(int)enum_ServerSetting.設備名稱] = _class.名稱;
            value[(int)enum_ServerSetting.類別] = _class.類別;
            value[(int)enum_ServerSetting.程式類別] = _class.程式類別;
            value[(int)enum_ServerSetting.內容] = _class.內容;
            value[(int)enum_ServerSetting.Server] = _class.Server;
            value[(int)enum_ServerSetting.Port] = _class.Port;
            value[(int)enum_ServerSetting.DBName] = _class.DBName;
            value[(int)enum_ServerSetting.TableName] = _class.TableName;
            value[(int)enum_ServerSetting.User] = _class.User;
            value[(int)enum_ServerSetting.Password] = _class.Password;
            value[(int)enum_ServerSetting.Value] = _class.Value;


            return value;
        }
        static public List<object[]> ClassToListSQL(List<ServerSettingClass> _class)
        {
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < _class.Count; i++)
            {
                object[] value = ClassToSQL(_class[i]);
                list_value.Add(value);
            }
            return list_value;
        }

        static public ServerSettingClass ObjToClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<ServerSettingClass>();
        }
        static public List<ServerSettingClass> ObjToListClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<List<ServerSettingClass>>();
        }

        static public List<ServerSettingClass> SQLToClass(List<object[]> values)
        {
            List<ServerSettingClass> list_values = new List<ServerSettingClass>();
            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                ServerSettingClass _class = new ServerSettingClass();
                _class.GUID = value[(int)enum_ServerSetting.GUID].ObjectToString();
                _class.單位 = value[(int)enum_ServerSetting.單位].ObjectToString();
                _class.名稱 = value[(int)enum_ServerSetting.設備名稱].ObjectToString();
                _class.類別 = value[(int)enum_ServerSetting.類別].ObjectToString();
                _class.程式類別 = value[(int)enum_ServerSetting.程式類別].ObjectToString();
                _class.內容 = value[(int)enum_ServerSetting.內容].ObjectToString();
                _class.Server = value[(int)enum_ServerSetting.Server].ObjectToString();
                _class.Port = value[(int)enum_ServerSetting.Port].ObjectToString();
                _class.DBName = value[(int)enum_ServerSetting.DBName].ObjectToString();
                _class.User = value[(int)enum_ServerSetting.User].ObjectToString();
                _class.Password = value[(int)enum_ServerSetting.Password].ObjectToString();
                _class.Value = value[(int)enum_ServerSetting.Value].ObjectToString();

                list_values.Add(_class);
            }

            return list_values;
        }
    }
}
