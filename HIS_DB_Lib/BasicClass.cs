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
        private string _server = "";
        private string _dbName = "";
        private string _tableName = "";
        private string _result = "";
        private string _value = "";
        private string _timeTaken = "";

        public object Data { get => _data; set => _data = value; }
        public int Code { get => _code; set => _code = value; }
        public string Result { get => _result; set => _result = value; }
        public string Value { get => _value; set => _value = value; }
        public string TimeTaken { get => _timeTaken; set => _timeTaken = value; }
        public string Server { get => _server; set => _server = value; }
        public string DbName { get => _dbName; set => _dbName = value; }
        public string TableName { get => _tableName; set => _tableName = value; }
    }
}
