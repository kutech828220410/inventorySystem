using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_DB_Lib
{
    public class returnData
    {
        private object _data = new object();
        private int _code = 0;
        private string _result = "";
        private string _value = "";
        private string _method = "";
        private string _timeTaken = "";
        private string _server = "";
        private uint _port = 0;
        private string _userName = "";
        private string _password = "";

        private string _dbName = "";
        private string _tableName = "";
        private string _serverName = "";
        private string _serverType = "";
        private string _serverContent = "";


        public object Data { get => _data; set => _data = value; }
        public int Code { get => _code; set => _code = value; }
        public string Result { get => _result; set => _result = value; }
        public string Value { get => _value; set => _value = value; }
        public string TimeTaken { get => _timeTaken; set => _timeTaken = value; }
        public string Method { get => _method; set => _method = value; }
        public string Server { get => _server; set => _server = value; }
        public string DbName { get => _dbName; set => _dbName = value; }
        public string TableName { get => _tableName; set => _tableName = value; }
        public string ServerType { get => _serverType; set => _serverType = value; }
        public string ServerName { get => _serverName; set => _serverName = value; }
        public string ServerContent { get => _serverContent; set => _serverContent = value; }
        public uint Port { get => _port; set => _port = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
