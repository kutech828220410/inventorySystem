using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Basic;


namespace HIS_DB_Lib
{
    public enum enum_login_session
    {
        GUID,
        ID,
        Name,
        Employer,
        loginTime,
        verifyTime,
    }

    public class sessionClass
    {
        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Employer")]
        public string Employer { get; set; }
        [JsonPropertyName("loginTime")]
        public string loginTime { get; set; }
        [JsonPropertyName("verifyTime")]
        public string verifyTime { get; set; }
        [JsonPropertyName("check_sec")]
        public string check_sec { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; }
        [JsonPropertyName("level")]
        public string level { get; set; }
        [JsonPropertyName("UID")]
        public string UID { get; set; }
        [JsonPropertyName("BARCODE")]
        public string BARCODE { get; set; }

        public List<string> Permissions { get => permissions; set => permissions = value; }
        private List<string> permissions = new List<string>();


    }
}
