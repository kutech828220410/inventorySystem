using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;
using System.Reflection;

namespace HIS_DB_Lib
{
    [EnumDescription("ServerSetting")]
    public enum enum_ServerSetting
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("單位,VARCHAR,200,None")]
        單位,
        [Description("設備名稱,VARCHAR,200,None")]
        設備名稱,
        [Description("類別,VARCHAR,200,None")]
        類別,
        [Description("程式類別,VARCHAR,200,INDEX")]
        程式類別,
        [Description("內容,VARCHAR,200,INDEX")]
        內容,
        [Description("Server,VARCHAR,200,None")]
        Server,
        [Description("Port,VARCHAR,200,None")]
        Port,
        [Description("DBName,VARCHAR,200,None")]
        DBName,
        [Description("TableName,VARCHAR,200,None")]
        TableName,
        [Description("User,VARCHAR,200,None")]
        User,
        [Description("Password,VARCHAR,200,None")]
        Password,
        [Description("Value,TEXT,65535,None")]
        Value,
    }
    public enum enum_ServerSetting_Type
    {
        網頁,
        調劑台,
        藥庫,
        傳送櫃,
        癌症備藥機,
        更新資訊,
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
        API_Login,
        Website,
    }

    public enum enum_ServerSetting_調劑台
    {
        一般資料,
        人員資料,
        藥檔資料,
        醫囑資料,
        儲位資料,
        交易紀錄資料,
        本地端,
        VM端,
        API01,
        API02,
        Order_API,
        Med_API,
        Website,
        update,
        功能,
    }
    public enum enum_ServerSetting_藥庫
    {
        一般資料,
        人員資料,
        藥檔資料,
        批次過帳資料,
        儲位資料,
        交易紀錄資料,
        本地端,
        VM端,
        API_inspection_excel,
        API01,
        API02,
        Website,
        update,
        功能,
    }
    public static class ServerSettingClassMethod
    {
        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, string Type, string Content)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == Type
                                                                 where value.設備名稱.ToUpper() == Name.ToUpper()
                                                                 where value.內容 == Content
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }
        public static List<ServerSettingClass> MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, string Type)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == Type
                                                                 where value.設備名稱.ToUpper() == Name.ToUpper()
                                                                 select value).ToList();
            return serverSettingClasses_buf;
        }

        public static ServerSettingClass MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, enum_ServerSetting_Type _enum_ServerSetting_Type, enum_ServerSetting_網頁 enum_ServerSetting_網頁)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 where value.設備名稱.ToUpper() == Name.ToUpper()
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
                                                                 where value.設備名稱.ToUpper() == Name.ToUpper()
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
                                                                 where value.設備名稱.ToUpper() == Name.ToUpper()
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
       
        public static ServerSettingClass MyFind(this List<ServerSettingClass> serverSettingClasses, string Name, enum_ServerSetting_Type _enum_ServerSetting_Type, string Content)
        {
            if (serverSettingClasses == null) return null;
            List<ServerSettingClass> serverSettingClasses_buf = (from value in serverSettingClasses
                                                                 where value.類別 == _enum_ServerSetting_Type.GetEnumName()
                                                                 where value.設備名稱.ToUpper() == Name.ToUpper()
                                                                 where value.內容 == Content
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
                                                                 where value.設備名稱.ToUpper() == Name.ToUpper()
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

        public static List<ServerSettingClass> SetValue(this List<ServerSettingClass> serverSettingClasses, ServerSettingClass serverSettingClass)
        {
            for(int i = 0; i < serverSettingClasses.Count; i ++)
            {
                if (serverSettingClasses[i].類別 != serverSettingClass.類別) continue;
                if (serverSettingClasses[i].設備名稱 != serverSettingClass.設備名稱) continue;
                if (serverSettingClasses[i].內容 != serverSettingClass.內容) continue;
                serverSettingClasses[i] = serverSettingClass;
            }
            return serverSettingClasses;
        }
        public static List<ServerSettingClass> WebApiGet(string url)
        {
            string jsonstring = Basic.Net.WEBApiGet(url);
            if (jsonstring.StringIsEmpty()) return null;
            Console.WriteLine(jsonstring);
            returnData returnData = jsonstring.JsonDeserializet<returnData>();

            List<ServerSettingClass> serverSettingClasses = returnData.Data.ObjToListClass<ServerSettingClass>();
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
        public string 設備名稱 { get; set; }
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
            this.設備名稱 = 名稱;
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
            this.設備名稱 = 名稱;
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
            this.設備名稱 = 名稱;
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
        
    }
}
