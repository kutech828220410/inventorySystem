using Basic;
using H_Pannel_lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace HIS_DB_Lib
{
    [EnumDescription("medMap")]
    public enum enum_medMap
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("位置,VARCHAR,10,NONE")]
        位置
    }
    [EnumDescription("medMap_section")]
    public enum enum_medMap_section
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("位置,VARCHAR,10,NONE")]
        位置
    }
    [EnumDescription("medMap_shelf")]
    public enum enum_medMap_shelf
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("位置,VARCHAR,10,NONE")]
        位置,
        [Description("寬度,VARCHAR,10,NONE")]
        寬度,
        [Description("高度,VARCHAR,10,NONE")]
        高度,
        [Description("燈條IP,VARCHAR,20,NONE")]
        燈條IP,
        [Description("serverName,VARCHAR,20,NONE")]
        serverName,
        [Description("serverType,VARCHAR,20,NONE")]
        serverType,
    }
    [EnumDescription("medMap_drawer")]
    public enum enum_medMap_drawer
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("位置,VARCHAR,10,NONE")]
        位置,
        [Description("寬度,VARCHAR,10,NONE")]
        寬度,
        [Description("高度,VARCHAR,10,NONE")]
        高度,
        [Description("抽屜IP,VARCHAR,20,NONE")]
        抽屜IP,
        [Description("serverName,VARCHAR,20,NONE")]
        serverName,
        [Description("serverType,VARCHAR,20,NONE")]
        serverType,
    }
    [EnumDescription("medMap_box")]
    public enum enum_medMap_box
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("Master_GUID,VARCHAR,50,INDEX")]
        Master_GUID,
        [Description("位置,VARCHAR,10,NONE")]
        位置,
        [Description("寬度,VARCHAR,10,NONE")]
        寬度,
        [Description("高度,VARCHAR,10,NONE")]
        高度,
        [Description("藥盒IP,VARCHAR,20,NONE")]
        藥盒IP,
        [Description("serverName,VARCHAR,20,NONE")]
        serverName,
        [Description("serverType,VARCHAR,20,NONE")]
        serverType,
    }
    /// <summary>
    /// 藥品地圖_父容器
    /// </summary>
    public class medMapClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [JsonPropertyName("position")]
        public string 位置 { get; set; }
        public sys_serverSettingClass sys_ServerSetting { get; set; }
        public List<medMap_sectionClass> medMap_Section {  get; set; }
    }
    /// <summary>
    /// 藥品地圖_子容器
    /// </summary>
    public class medMap_sectionClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [JsonPropertyName("position")]
        public string 位置 { get; set; }
        public List<medMap_shelfClass> shelf { get; set; }
        public List<medMap_drawerClass> drawer { get; set; }

    }
    /// <summary>
    /// 藥品地圖_層架
    /// </summary>
    public class medMap_shelfClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [JsonPropertyName("position")]
        public string 位置 { get; set; }
        /// <summary>
        /// 寬度
        /// </summary>
        [JsonPropertyName("width")]
        public string 寬度 { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [JsonPropertyName("height")]
        public string 高度 { get; set; }
        /// <summary>
        /// 燈條IP
        /// </summary>
        [JsonPropertyName("ip_light")]
        public string 燈條IP { get; set; }
        /// <summary>
        /// serverName
        /// </summary>
        [JsonPropertyName("serverName")]
        public string serverName { get; set; }
        /// <summary>
        /// serverType
        /// </summary>
        [JsonPropertyName("serverType")]
        public string serverType { get; set; }
        public List<medMap_boxClass> medMapBox { get; set; }
        public RowsLED rowsLED { get; set; }

    }
    /// <summary>
    /// 藥品地圖_抽屜
    /// </summary>
    public class medMap_drawerClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [JsonPropertyName("position")]
        public string 位置 { get; set; }
        /// <summary>
        /// 寬度
        /// </summary>
        [JsonPropertyName("width")]
        public string 寬度 { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [JsonPropertyName("height")]
        public string 高度 { get; set; }
        /// <summary>
        /// 抽屜IP
        /// </summary>
        [JsonPropertyName("ip_drawer")]
        public string 抽屜IP { get; set; }
        /// <summary>
        /// serverName
        /// </summary>
        [JsonPropertyName("serverName")]
        public string serverName { get; set; }
        /// <summary>
        /// serverType
        /// </summary>
        [JsonPropertyName("serverType")]
        public string serverType { get; set; }
        public Drawer drawer { get; set; }
        public Storage storage { get; set; }

    }
    /// <summary>
    /// 藥品地圖_藥盒
    /// </summary>
    public class medMap_boxClass
    {
        /// <summary>
        /// 唯一KEY
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        /// <summary>
        /// Master_GUID
        /// </summary>
        [JsonPropertyName("Master_GUID")]
        public string Master_GUID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [JsonPropertyName("position")]
        public string 位置 { get; set; }
        /// <summary>
        /// 寬度
        /// </summary>
        [JsonPropertyName("width")]
        public string 寬度 { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [JsonPropertyName("height")]
        public string 高度 { get; set; }
        /// <summary>
        /// 藥盒IP
        /// </summary>
        [JsonPropertyName("ip_box")]
        public string 藥盒IP { get; set; }
        /// <summary>
        /// serverName
        /// </summary>
        [JsonPropertyName("serverName")]
        public string serverName { get; set; }
        /// <summary>
        /// serverType
        /// </summary>
        [JsonPropertyName("serverType")]
        public string serverType { get; set; }
        public Storage storage { get; set; }


    }
}
