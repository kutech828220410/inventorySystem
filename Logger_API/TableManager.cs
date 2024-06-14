using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLUI;
using LoggerClassLibrary;

namespace Logger_API
{
    static public class TableManager
    {
        static public List<Table> CheckCreateTable()
        {
            List<Table> tables = new List<Table>();
            tables.Add(CheckCreateTable("127.0.0.1", "anna_test", "user", "66437068", 3306, new enum_logger()));
            return tables;
        }


        static public Table CheckCreateTable(string server, string db, string user, string password, uint port, Enum Enum)
        {
            Table table = new Table(Enum);

            string Server = server;
            string DB = db;
            string UserName = user;
            string Password = password;
            uint Port = port;
            table.Server = Server;
            table.DBName = DB;
            table.Username = UserName;
            table.Password = Password;
            table.Port = Port.ToString();

            SQLControl sQLControl = new SQLControl(Server, DB, UserName, Password, Port, MySql.Data.MySqlClient.MySqlSslMode.Disabled);

            if (!sQLControl.IsTableCreat()) sQLControl.CreatTable(table);
            else sQLControl.CheckAllColumnName(table, true);
            return table;
        }
    }
}
