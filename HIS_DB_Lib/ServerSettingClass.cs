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
    public enum enum_ServerSettingClass
    {
        GUID,
        單位,
        名稱,
        類別,
        程式類別,
        內容,
        Server,
        Port,
        DBName,
        TableName,
        User,
        Password,
    }
    public enum enum_ServerSettingClass_Type
    {
        調劑台,
        藥庫,
    }
    public enum enum_ServerSettingClass_ProgramType
    {
        SQLServer,
        API,
        WEB,
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

        public ServerSettingClass()
        {

        }
        public ServerSettingClass(string 名稱, enum_ServerSettingClass_Type enum_ServerSettingClass_Type , enum_ServerSettingClass_ProgramType enum_ServerSettingClass_ProgramType,string 內容
            , string Server, string Port, string DBName, string TableName, string User, string Password)
        {
            this.名稱 = 名稱;
            this.類別 = enum_ServerSettingClass_Type.GetEnumName();
            this.程式類別 = enum_ServerSettingClass_ProgramType.GetEnumName();
            this.內容 = 內容;
            this.Server = Server;
            this.Port = Port;
            this.DBName = DBName;
            this.TableName = TableName;
            this.User = User;
            this.Password = Password;
        }
        static public object[] ClassToSQL(ServerSettingClass _class)
        {
            object[] value = new object[new enum_ServerSettingClass().GetLength()];
            value[(int)enum_ServerSettingClass.GUID] = _class.GUID;
            value[(int)enum_ServerSettingClass.單位] = _class.單位;
            value[(int)enum_ServerSettingClass.名稱] = _class.名稱;
            value[(int)enum_ServerSettingClass.類別] = _class.類別;
            value[(int)enum_ServerSettingClass.程式類別] = _class.程式類別;
            value[(int)enum_ServerSettingClass.內容] = _class.內容;
            value[(int)enum_ServerSettingClass.Server] = _class.Server;
            value[(int)enum_ServerSettingClass.Port] = _class.Port;
            value[(int)enum_ServerSettingClass.DBName] = _class.DBName;
            value[(int)enum_ServerSettingClass.TableName] = _class.TableName;
            value[(int)enum_ServerSettingClass.User] = _class.User;
            value[(int)enum_ServerSettingClass.Password] = _class.Password;


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
                _class.GUID = value[(int)enum_ServerSettingClass.GUID].ObjectToString();
                _class.單位 = value[(int)enum_ServerSettingClass.單位].ObjectToString();
                _class.名稱 = value[(int)enum_ServerSettingClass.名稱].ObjectToString();
                _class.類別 = value[(int)enum_ServerSettingClass.類別].ObjectToString();
                _class.程式類別 = value[(int)enum_ServerSettingClass.程式類別].ObjectToString();
                _class.內容 = value[(int)enum_ServerSettingClass.內容].ObjectToString();
                _class.Server = value[(int)enum_ServerSettingClass.Server].ObjectToString();
                _class.Port = value[(int)enum_ServerSettingClass.Port].ObjectToString();
                _class.DBName = value[(int)enum_ServerSettingClass.DBName].ObjectToString();
                _class.User = value[(int)enum_ServerSettingClass.User].ObjectToString();
                _class.Password = value[(int)enum_ServerSettingClass.Password].ObjectToString();

                list_values.Add(_class);
            }

            return list_values;
        }
    }
}
