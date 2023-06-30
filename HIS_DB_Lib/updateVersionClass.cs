using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;

namespace HIS_DB_Lib
{
    public enum enum_updateVersion
    {
        GUID,
        program_name,
        version,
        filepath
    }

    public class updateVersionClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("program_name")]
        public string program_name { get; set; }
        [JsonPropertyName("version")]
        public string version { get; set; }
        [JsonPropertyName("filepath")]
        public string filepath { get; set; }

        static public object[] ClassToSQL(updateVersionClass _class)
        {
            object[] value = new object[Enum.GetNames(typeof(enum_updateVersion)).Length];
            Type enumType = typeof(enum_updateVersion);

            foreach (var field in enumType.GetFields())
            {
                if (field.FieldType.IsEnum)
                {
                    string enumName = field.Name;
                    int enumIndex = (int)field.GetValue(null);

                    // 使用 enumIndex 來填入對應的屬性值
                    value[enumIndex] = typeof(updateVersionClass).GetProperty(enumName)?.GetValue(_class);
                }
            }

            return value;
        }
        static public List<object[]> ClassToSQL(List<updateVersionClass> _classes)
        {
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < _classes.Count; i++)
            {
                object[] value = ClassToSQL(_classes[i]);
                list_value.Add(value);
            }

            return list_value;
        }
        static public updateVersionClass SQLToClass(object[] values)
        {
            updateVersionClass obj = new updateVersionClass();
            Type enumType = typeof(enum_updateVersion);

            foreach (var field in enumType.GetFields())
            {
                if (field.FieldType.IsEnum)
                {
                    string enumName = field.Name;
                    int enumIndex = (int)field.GetValue(null);

                    // 使用 enumIndex 來取得對應的屬性值
                    object value = values[enumIndex];

                    // 使用 enumName 來取得對應的屬性
                    var property = typeof(updateVersionClass).GetProperty(enumName);

                    // 將值填入對應的屬性
                    property?.SetValue(obj, value);
                }
            }

            return obj;
        }
        static public List<updateVersionClass> SQLToClass(List<object[]> values)
        {
            List<updateVersionClass> list_value = new List<updateVersionClass>();
            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                updateVersionClass _class = SQLToClass(values[i]);
                list_value.Add(_class);
            }

            return list_value;
        }
        static public updateVersionClass ObjToClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<updateVersionClass>();
        }
        static public List<updateVersionClass> ObjToListClass(object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<List<updateVersionClass>>();
        }
    }
}
