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
    [EnumDescription("drugStotreArea")]
    public enum enum_drugStotreArea
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("IP,VARCHAR,20,NONE")]
        IP,
        [Description("Port,VARCHAR,10,NONE")]
        Port,
        [Description("Num,VARCHAR,10,NONE")]
        Num,
        [Description("序號,VARCHAR,10,NONE")]
        序號,
        [Description("名稱,VARCHAR,50,NONE")]
        名稱,
        [Description("狀態,VARCHAR,10,NONE")]
        狀態,
        [Description("燈號更新旗標,VARCHAR,10,NONE")]
        燈號更新旗標,
    }
    public class drugStotreArea
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [JsonPropertyName("IP")]
        public string IP { get; set; }
        /// <summary>
        /// Port
        /// </summary>
        [JsonPropertyName("port")]
        public string Port { get; set; }
        /// <summary>
        /// 腳位號碼
        /// </summary>
        [JsonPropertyName("num")]
        public string PIN_NUM { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        [JsonPropertyName("sn")]
        public string 序號 { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [JsonPropertyName("name")]
        public string 名稱 { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("state")]
        public string 狀態 { get; set; }
        /// <summary>
        /// 燈號更新旗標
        /// </summary>
        [JsonPropertyName("flag_light")]
        public string 燈號更新旗標 { get; set; }
        static public SQLUI.Table init(string API_Server)
        {
            string url = $"{API_Server}/api/drugStotreArea/init";

            returnData returnData = new returnData();
            returnData.ServerName = "ds01";
            returnData.ServerType = "藥庫";
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }
        static public SQLUI.Table init(string API_Server , string serverName , string serverType)
        {
            string url = $"{API_Server}/api/drugStotreArea/init";

            returnData returnData = new returnData();
            returnData.ServerName = serverName;
            returnData.ServerType = serverType;
            string json_in = returnData.JsonSerializationt();
            string json_out = Net.WEBApiPostJson(url, json_in);
            SQLUI.Table table = json_out.JsonDeserializet<SQLUI.Table>();
            return table;
        }


    }

}
