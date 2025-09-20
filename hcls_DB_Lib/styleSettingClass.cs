using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;
using System.ComponentModel;

namespace hcls_DB_Lib
{
    [EnumDescription("sytle_setting")]
    public enum enum_sytle_setting
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("代碼,VARCHAR,50,NONE")]
        代碼,
        [Description("名稱,VARCHAR,300,NONE")]
        名稱,
        [Description("台號,VARCHAR,50,NONE")]
        台號,
        [Description("寬度,VARCHAR,50,NONE")]
        寬度,
        [Description("標題名稱,VARCHAR,50,NONE")]
        標題名稱,
        [Description("標題字體,VARCHAR,50,NONE")]
        標題字體,
        [Description("標題文字寬度,VARCHAR,50,NONE")]
        標題文字寬度,
        [Description("標題字體顏色,VARCHAR,50,NONE")]
        標題字體顏色,
        [Description("標題背景顏色,VARCHAR,50,NONE")]
        標題背景顏色,
        [Description("標題高度,VARCHAR,50,NONE")]
        標題高度,
        [Description("英文標題高度,VARCHAR,50,NONE")]
        英文標題高度,
        [Description("英文標題字體,VARCHAR,50,NONE")]
        英文標題字體,
        [Description("叫號號碼,VARCHAR,50,NONE")]
        叫號號碼,
        [Description("叫號字體,VARCHAR,50,NONE")]
        叫號字體,
        [Description("叫號文字寬度,VARCHAR,50,NONE")]
        叫號文字寬度,
        [Description("叫號邊界距離,VARCHAR,50,NONE")]
        叫號邊界距離,
        [Description("叫號邊框圓角,VARCHAR,50,NONE")]
        叫號邊框圓角,
        [Description("叫號字體顏色,VARCHAR,50,NONE")]
        叫號字體顏色,
        [Description("叫號背景顏色,VARCHAR,50,NONE")]
        叫號背景顏色,
        [Description("叫號備註高度,VARCHAR,50,NONE")]
        叫號備註高度,
        [Description("叫號備註字體,VARCHAR,50,NONE")]
        叫號備註字體,

    }

    /// <summary>
    /// 風格樣式設定 (Style Setting)  
    /// 包含：基本資訊、標題設定、叫號設定
    /// </summary>
    public class styleSettingClass
    {
        /// <summary>唯一識別碼 (GUID)</summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }

        /// <summary>代碼</summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>名稱</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>台號 (Station Number)</summary>
        [JsonPropertyName("station_no")]
        public string StationNo { get; set; }

        /// <summary>寬度</summary>
        [JsonPropertyName("width")]
        public string Width { get; set; }

        /// <summary>標題設定</summary>
        [JsonPropertyName("title")]
        public TitleSetting Title { get; set; } = new TitleSetting();

        /// <summary>叫號設定</summary>
        [JsonPropertyName("call")]
        public CallSetting Call { get; set; } = new CallSetting();


        public int CaculateCallHeight(int panel_height , int marquee_height)
        {

            return (int)(panel_height - (Title.TitleHeight.StringToDouble() + Title.EngTitleHeight.StringToDouble() + marquee_height));

        }
    }

    /// <summary>
    /// 標題相關設定 (Title Setting)
    /// </summary>
    public class TitleSetting
    {
        /// <summary>標題名稱</summary>
        [JsonPropertyName("title_name")]
        public string TitleName { get; set; }

        /// <summary>標題字體</summary>
        [JsonPropertyName("title_font")]
        public string TitleFont { get; set; }

        /// <summary>標題文字寬度</summary>
        [JsonPropertyName("title_text_width")]
        public string TitleTextWidth { get; set; }

        /// <summary>標題字體顏色 (Color HEX or ARGB)</summary>
        [JsonPropertyName("title_font_color")]
        public string TitleFontColor { get; set; }

        /// <summary>標題背景顏色 (Color HEX or ARGB)</summary>
        [JsonPropertyName("title_back_color")]
        public string TitleBackColor { get; set; }

        /// <summary>標題高度</summary>
        [JsonPropertyName("title_height")]
        public string TitleHeight { get; set; }

        /// <summary>英文標題高度</summary>
        [JsonPropertyName("eng_title_height")]
        public string EngTitleHeight { get; set; }

        /// <summary>英文標題字體</summary>
        [JsonPropertyName("eng_title_font")]
        public string EngTitleFont { get; set; }
    }

    /// <summary>
    /// 叫號相關設定 (Call Setting)
    /// </summary>
    public class CallSetting
    {
        /// <summary>叫號號碼</summary>
        [JsonPropertyName("call_number")]
        public string CallNumber { get; set; }

        /// <summary>叫號字體</summary>
        [JsonPropertyName("call_font")]
        public string CallFont { get; set; }

        /// <summary>叫號文字寬度</summary>
        [JsonPropertyName("call_text_width")]
        public string CallTextWidth { get; set; }

        /// <summary>叫號邊界距離</summary>
        [JsonPropertyName("call_margin")]
        public string CallMargin { get; set; }

        /// <summary>叫號邊框圓角</summary>
        [JsonPropertyName("call_border_radius")]
        public string CallBorderRadius { get; set; }

        /// <summary>叫號字體顏色 (Color HEX or ARGB)</summary>
        [JsonPropertyName("call_font_color")]
        public string CallFontColor { get; set; }

        /// <summary>叫號背景顏色 (Color HEX or ARGB)</summary>
        [JsonPropertyName("call_back_color")]
        public string CallBackColor { get; set; }

        /// <summary>叫號備註高度</summary>
        [JsonPropertyName("call_note_height")]
        public string CallNoteHeight { get; set; }

        /// <summary>叫號備註字體</summary>
        [JsonPropertyName("call_note_font")]
        public string CallNoteFont { get; set; }


    }
    public static class styleSettingConverter
    {
        /// <summary>
        /// 將 styleSettingClass 轉換為 object[]
        /// </summary>
        public static object[] StyleSettingToObject(this styleSettingClass item)
        {
            object[] row = new object[Enum.GetValues(typeof(enum_sytle_setting)).Length];

            row[(int)enum_sytle_setting.GUID] = item.GUID;
            row[(int)enum_sytle_setting.代碼] = item.Code;
            row[(int)enum_sytle_setting.名稱] = item.Name;
            row[(int)enum_sytle_setting.台號] = item.StationNo;
            row[(int)enum_sytle_setting.寬度] = item.Width;

            // 標題設定
            row[(int)enum_sytle_setting.標題名稱] = item.Title.TitleName;
            row[(int)enum_sytle_setting.標題字體] = item.Title.TitleFont;
            row[(int)enum_sytle_setting.標題文字寬度] = item.Title.TitleTextWidth;
            row[(int)enum_sytle_setting.標題字體顏色] = item.Title.TitleFontColor;
            row[(int)enum_sytle_setting.標題背景顏色] = item.Title.TitleBackColor;
            row[(int)enum_sytle_setting.標題高度] = item.Title.TitleHeight;
            row[(int)enum_sytle_setting.英文標題高度] = item.Title.EngTitleHeight;
            row[(int)enum_sytle_setting.英文標題字體] = item.Title.EngTitleFont;

            // 叫號設定
            row[(int)enum_sytle_setting.叫號號碼] = item.Call.CallNumber;
            row[(int)enum_sytle_setting.叫號字體] = item.Call.CallFont;
            row[(int)enum_sytle_setting.叫號文字寬度] = item.Call.CallTextWidth;
            row[(int)enum_sytle_setting.叫號邊界距離] = item.Call.CallMargin;
            row[(int)enum_sytle_setting.叫號邊框圓角] = item.Call.CallBorderRadius;
            row[(int)enum_sytle_setting.叫號字體顏色] = item.Call.CallFontColor;
            row[(int)enum_sytle_setting.叫號背景顏色] = item.Call.CallBackColor;
            row[(int)enum_sytle_setting.叫號備註高度] = item.Call.CallNoteHeight;
            row[(int)enum_sytle_setting.叫號備註字體] = item.Call.CallNoteFont;

            return row;
        }

        /// <summary>
        /// 將 object[] 轉換為 styleSettingClass
        /// </summary>
        public static styleSettingClass ObjectToStyleSetting(this object[] row)
        {
            styleSettingClass item = new styleSettingClass
            {
                GUID = row[(int)enum_sytle_setting.GUID]?.ToString(),
                Code = row[(int)enum_sytle_setting.代碼]?.ToString(),
                Name = row[(int)enum_sytle_setting.名稱]?.ToString(),
                StationNo = row[(int)enum_sytle_setting.台號]?.ToString(),
                Width = row[(int)enum_sytle_setting.寬度]?.ToString(),

                Title = new TitleSetting
                {
                    TitleName = row[(int)enum_sytle_setting.標題名稱]?.ToString(),
                    TitleFont = row[(int)enum_sytle_setting.標題字體]?.ToString(),
                    TitleTextWidth = row[(int)enum_sytle_setting.標題文字寬度]?.ToString(),
                    TitleFontColor = row[(int)enum_sytle_setting.標題字體顏色]?.ToString(),
                    TitleBackColor = row[(int)enum_sytle_setting.標題背景顏色]?.ToString(),
                    TitleHeight = row[(int)enum_sytle_setting.標題高度]?.ToString(),
                    EngTitleHeight = row[(int)enum_sytle_setting.英文標題高度]?.ToString(),
                    EngTitleFont = row[(int)enum_sytle_setting.英文標題字體]?.ToString(),
                },

                Call = new CallSetting
                {
                    CallNumber = row[(int)enum_sytle_setting.叫號號碼]?.ToString(),
                    CallFont = row[(int)enum_sytle_setting.叫號字體]?.ToString(),
                    CallTextWidth = row[(int)enum_sytle_setting.叫號文字寬度]?.ToString(),
                    CallMargin = row[(int)enum_sytle_setting.叫號邊界距離]?.ToString(),
                    CallBorderRadius = row[(int)enum_sytle_setting.叫號邊框圓角]?.ToString(),
                    CallFontColor = row[(int)enum_sytle_setting.叫號字體顏色]?.ToString(),
                    CallBackColor = row[(int)enum_sytle_setting.叫號背景顏色]?.ToString(),
                    CallNoteHeight = row[(int)enum_sytle_setting.叫號備註高度]?.ToString(),
                    CallNoteFont = row[(int)enum_sytle_setting.叫號備註字體]?.ToString(),
                }
            };

            return item;
        }
    }
}
