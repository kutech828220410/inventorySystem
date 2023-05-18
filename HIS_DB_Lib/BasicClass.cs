using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_DB_Lib
{
    public class returnData
    {
        private List<object> _data = new List<object>();
        private int _code = 0;
        private string _result = "";

        public List<object> Data { get => _data; set => _data = value; }
        public int Code { get => _code; set => _code = value; }
        public string Result { get => _result; set => _result = value; }
    }
}
